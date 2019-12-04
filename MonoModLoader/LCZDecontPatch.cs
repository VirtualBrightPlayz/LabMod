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
	[MonoModPatch("global::DecontaminationLCZ")]
	class LCZDecontPatch : DecontaminationLCZ
	{
		public extern IEnumerator<float> orig__KillPlayersInLCZ();
		public IEnumerator<float> _KillPlayersInLCZ()
		{
			bool stop = false;
			LabModLCZDecont.TriggerEvent(this, out stop);
			if (!stop)
				orig__KillPlayersInLCZ();
			yield return 0f;
		}
	}
}
