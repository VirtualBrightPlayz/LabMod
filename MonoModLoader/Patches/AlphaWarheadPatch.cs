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
	[MonoModPatch("global::AlphaWarheadController")]
	class AlphaWarheadPatch : AlphaWarheadController
	{
		public extern void orig_Detonate();
		public void Detonate()
		{
			LabModAlphaWarheadDetonate.TriggerEvent(this);
			orig_Detonate();
		}
	}
}
