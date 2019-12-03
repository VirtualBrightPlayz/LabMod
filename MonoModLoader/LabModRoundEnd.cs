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
    public interface ILabModRoundEnd : ILabModEvent
    {
        void Event(RoundSummary sum, RoundSummary.SumInfo_ClassList list_start, RoundSummary.SumInfo_ClassList list_finish, RoundSummary.LeadingTeam leadingTeam, int e_ds, int e_sc, int scp_kills, int round_cd);
    }

    public class LabModRoundEnd
    {
        public static List<Type> types_update = new List<Type>();

        public static void Init()
        {
            types_update = new List<Type>(AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(ILabModRoundEnd).IsAssignableFrom(p) && !p.IsInterface));
        }

        public static void TriggerEvent(RoundSummary sum, RoundSummary.SumInfo_ClassList list_start, RoundSummary.SumInfo_ClassList list_finish, RoundSummary.LeadingTeam leadingTeam, int e_ds, int e_sc, int scp_kills, int round_cd)
        {
            //bool shouldstop = false;
            foreach (var type in types_update)
            {
                if (LabMod.GetObjectOfType(type) == null)
                    continue;
                ((ILabModRoundEnd)LabMod.GetObjectOfType(type)).Event(sum, list_start, list_finish, leadingTeam, e_ds, e_sc, scp_kills, round_cd);
            }
            //stop = shouldstop;
        }
    }
}
