﻿using LabMod.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMod.Events
{
	public interface ILabModLCZDecont : ILabModEvent
	{
		bool Event(DecontaminationLCZ ccm);
	}

	public class LabModLCZDecont
	{
		public static List<Type> types_update = new List<Type>();

		public static void Init()
		{
			types_update = new List<Type>(AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(ILabModLCZDecont).IsAssignableFrom(p) && !p.IsInterface));
		}

		public static void TriggerEvent(DecontaminationLCZ ccm, out bool stop)
		{
			bool should_stop = false;
			foreach (var type in types_update)
			{
				if (LabMod.GetObjectOfType(type) == null)
					continue;
				if (!((ILabModLCZDecont)LabMod.GetObjectOfType(type)).Event(ccm))
					should_stop = true;
			}
			stop = should_stop;
		}
	}
}
