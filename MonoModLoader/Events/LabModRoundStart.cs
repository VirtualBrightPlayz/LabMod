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
		public static List<Type> types_update = new List<Type>();

		public static void Init()
		{
			types_update = new List<Type>(AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(ILabModRoundStart).IsAssignableFrom(p) && !p.IsInterface));
		}

		public static void TriggerEvent(CharacterClassManager ccm, out bool stop)
		{
			bool shouldstop = false;
			foreach (var type in types_update)
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
		public static List<Type> types_update = new List<Type>();

		public static void Init()
		{
			types_update = new List<Type>(AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(ILabModPreRoundStart).IsAssignableFrom(p) && !p.IsInterface));
		}

		public static void TriggerEvent(CharacterClassManager ccm)
		{
			foreach (var type in types_update)
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
		public static List<Type> types_update = new List<Type>();

		public static void Init()
		{
			types_update = new List<Type>(AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(ILabModPostRoundStart).IsAssignableFrom(p) && !p.IsInterface));
		}

		public static void TriggerEvent(CharacterClassManager ccm)
		{
			foreach (var type in types_update)
			{
				if (LabMod.GetObjectOfType(type) == null)
					continue;
				((ILabModPostRoundStart)LabMod.GetObjectOfType(type)).Event(ccm);
			}
		}
	}
}
