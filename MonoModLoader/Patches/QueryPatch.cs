#pragma warning disable CS0626 // orig_ method is marked external and has no attributes on it.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MonoMod;
using RemoteAdmin;
using LabMod.Events;

namespace LabMod
{
    [MonoModPatch("global::RemoteAdmin.QueryProcessor")]
    class QueryPatch : QueryProcessor
    {
        public extern void orig_ProcessGameConsoleQuery(string query, bool encrypted);
        public void ProcessGameConsoleQuery(string query, bool encrypted)
        {
            bool stop = false;
            LabModGameConsoleQuery.TriggerEvent(this, query, encrypted, out stop);
            if (!stop)
                orig_ProcessGameConsoleQuery(query, encrypted);
        }
    }
}
