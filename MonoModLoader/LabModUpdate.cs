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
        public static List<Type> types_update = new List<Type>();

        public static void Init()
        {
            types_update = new List<Type>(AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(ILabModUpdate).IsAssignableFrom(p) && !p.IsInterface));
        }

        public static void TriggerEvent()
        {
            //Huge thanks to all the smarter people who pointed out that this will lag the hell out of the server
            //var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(ILabModUpdate).IsAssignableFrom(p) && !p.IsInterface);
            foreach (var type in types_update)
            {
                if (LabMod.GetObjectOfType(type) == null)
                    continue;
                ((ILabModUpdate)LabMod.GetObjectOfType(type)).Event();
            }
        }
    }
}
