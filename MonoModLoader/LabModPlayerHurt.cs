#pragma warning disable CS0626 // orig_ method is marked external and has no attributes on it.
using MonoMod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RemoteAdmin;

namespace LabMod.Events
{
    public interface ILabModPlayerHurt : ILabModEvent
    {
        bool Event(PlayerStats stats, PlayerStats.HitInfo info, GameObject go);
    }

    public class LabModPlayerHurt
    {
        public static void TriggerEvent(PlayerStats stats, PlayerStats.HitInfo info, GameObject go, out bool stop)
        {
            bool shouldstop = false;
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(ILabModPlayerHurt).IsAssignableFrom(p) && !p.IsInterface);
            foreach (var type in types)
            {
                if (LabMod.GetObjectOfType(type) == null)
                    continue;
                if (!((ILabModPlayerHurt)LabMod.GetObjectOfType(type)).Event(stats, info, go))
                {
                    shouldstop = true;
                }
            }
            stop = shouldstop;
        }
    }
}
