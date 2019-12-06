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
    [MonoModPatch("global::PlayerStats")]
    class PlrStatPatch : PlayerStats
    {
        public class SpawnLateHelper
        {
            public CharacterClassManager ccm;
            public RoleType rt;
        }

        public extern bool orig_HurtPlayer(PlayerStats.HitInfo info, GameObject go);
        public bool HurtPlayer(PlayerStats.HitInfo info, GameObject go)
        {
            bool stop = false;
            LabModPlayerHurt.TriggerEvent(this, info, go, out stop);
            if (!stop)
                return orig_HurtPlayer(info, go);
            return false;
        }

        public System.Collections.IEnumerator SpawnLate(SpawnLateHelper h)
        {
            yield return new WaitForSeconds(0.5f);
            h.ccm.SetClassIDAdv(h.rt, false);
            h.ccm.GetComponent<PlyMovementSync>().OnPlayerClassChange(h.ccm.DeathPosition, 0f);
        }
    }
}
