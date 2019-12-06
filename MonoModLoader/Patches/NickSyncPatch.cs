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
	[MonoModPatch("global::NicknameSync")]
	class NickSyncPatch : NicknameSync
	{
		public extern void orig_SetNick(string nick);
		public void SetNick(string nick)
		{
			orig_SetNick(nick);
			LabModPlayerJoin.TriggerEvent(this.GetComponent<CharacterClassManager>());
		}
	}
}
