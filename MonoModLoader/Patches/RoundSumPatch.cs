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
    [MonoModPatch("global::RoundSummary")]
    class RoundSumPatch : RoundSummary
    {

		public extern void orig_RpcShowRoundSummary(RoundSummary.SumInfo_ClassList list_start, RoundSummary.SumInfo_ClassList list_finish, RoundSummary.LeadingTeam leadingTeam, int e_ds, int e_sc, int scp_kills, int round_cd);
		[MonoModPublic]
		public void RpcShowRoundSummary(RoundSummary.SumInfo_ClassList list_start, RoundSummary.SumInfo_ClassList list_finish, RoundSummary.LeadingTeam leadingTeam, int e_ds, int e_sc, int scp_kills, int round_cd)
        {
            orig_RpcShowRoundSummary(list_start, list_finish, leadingTeam, e_ds, e_sc, scp_kills, round_cd);
            LabModRoundEnd.TriggerEvent(this, list_start, list_finish, leadingTeam, e_ds, e_sc, scp_kills, round_cd);
        }
    }
}
