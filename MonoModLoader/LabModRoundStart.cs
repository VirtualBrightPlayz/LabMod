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
    public interface ILabModRoundStart : ILabModEvent
    {
        bool Event(CharacterClassManager ccm);
    }

    public class LabModRoundStart
    {
        public static void TriggerEvent(CharacterClassManager ccm, out bool stop)
        {
            bool shouldstop = false;
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(ILabModRoundStart).IsAssignableFrom(p) && !p.IsInterface);
            foreach (var type in types)
            {
                if (LabMod.GetObjectOfType(type) == null)
                    continue;
                if (!((ILabModRoundStart)LabMod.GetObjectOfType(type)).Event(ccm))
                {
                    shouldstop = true;
                }
            }
            stop = shouldstop;
        }
    }

    public interface ILabModPreRoundStart : ILabModEvent
    {
        void Event(CharacterClassManager ccm);
    }

    public class LabModPreRoundStart
    {
        public static void TriggerEvent(CharacterClassManager ccm)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(ILabModPreRoundStart).IsAssignableFrom(p) && !p.IsInterface);
            foreach (var type in types)
            {
                if (LabMod.GetObjectOfType(type) == null)
                    continue;
                ((ILabModPreRoundStart)LabMod.GetObjectOfType(type)).Event(ccm);
            }
        }
    }

    public interface ILabModPostRoundStart : ILabModEvent
    {
        void Event(CharacterClassManager ccm);
    }

    public class LabModPostRoundStart
    {
        public static void TriggerEvent(CharacterClassManager ccm)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(ILabModPostRoundStart).IsAssignableFrom(p) && !p.IsInterface);
            foreach (var type in types)
            {
                if (LabMod.GetObjectOfType(type) == null)
                    continue;
                ((ILabModPostRoundStart)LabMod.GetObjectOfType(type)).Event(ccm);
            }
        }
    }
}
