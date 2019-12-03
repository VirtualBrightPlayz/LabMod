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
    public interface ILabModGameConsoleQuery : ILabModEvent
    {
        bool Event(QueryProcessor processor, string query, bool encrypted);
    }

    public class LabModGameConsoleQuery
    {
        public static void TriggerEvent(QueryProcessor processor, string query, bool encrypted, out bool stop)
        {
            bool shouldstop = false;
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(ILabModGameConsoleQuery).IsAssignableFrom(p) && !p.IsInterface);
            foreach (var type in types)
            {
                if (LabMod.GetObjectOfType(type) == null)
                    continue;
                if (!((ILabModGameConsoleQuery)LabMod.GetObjectOfType(type)).Event(processor, query, encrypted))
                {
                    shouldstop = true;
                }
            }
            stop = shouldstop;
        }
    }
}
