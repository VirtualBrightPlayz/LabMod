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
	[MonoModPatch("global::CharacterClassManager")]
	class CharClassManPatch : CharacterClassManager
	{
		public class SpawnLateHelper
		{
			public CharacterClassManager ccm;
			public RoleType rt;
			public RoleType rtspawn;
		}

		public extern void orig_SetRandomRoles();
		public void SetRandomRoles()
		{
			bool stop = false;
			LabModPreRoundStart.TriggerEvent(this);
			LabModRoundStart.TriggerEvent(this, out stop);
			if (!stop)
				orig_SetRandomRoles();
			LabModPostRoundStart.TriggerEvent(this);
		}

		public System.Collections.IEnumerator EndRoundSoon(float sec)
		{
			yield return new WaitForSeconds(sec);
			PlayerManager.localPlayer.GetComponent<PlayerStats>().Roundrestart();
		}

		public System.Collections.IEnumerator SpawnLate(SpawnLateHelper h)
		{
			yield return new WaitForSeconds(0.5f);
			h.ccm.SetClassID(h.rt);
			h.ccm.GetComponent<PlayerStats>().health = h.ccm.Classes.SafeGet(h.rt).maxHP;
			h.ccm.GetComponent<PlyMovementSync>().OnPlayerClassChange(SpawnpointManager.GetRandomPosition(h.rtspawn).transform.position, 0f);
		}
	}
}
