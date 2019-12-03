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
    public interface ILabModUpdate : ILabModEvent
    {
        void Event();
    }

    public class LabModUpdate
    {
        public static void TriggerEvent()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(ILabModUpdate).IsAssignableFrom(p) && !p.IsInterface);
            foreach (var type in types)
            {
                if (LabMod.GetObjectOfType(type) == null)
                    continue;
                ((ILabModUpdate)LabMod.GetObjectOfType(type)).Event();
            }
        }
    }
}
