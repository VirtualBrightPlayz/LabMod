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
	public interface ILabModGameConsoleQuery : ILabModEvent
	{
		bool Event(QueryProcessor processor, string query, bool encrypted);
	}

	public class LabModGameConsoleQuery
	{
		public static List<Type> types_update = new List<Type>();

		public static void Init()
		{
			types_update = new List<Type>(AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(ILabModGameConsoleQuery).IsAssignableFrom(p) && !p.IsInterface));
		}

		public static void TriggerEvent(QueryProcessor processor, string query, bool encrypted, out bool stop)
		{
			bool shouldstop = false;
			foreach (var type in types_update)
			{
				if (LabMod.GetObjectOfType(type) == null)
					continue;
				if (!((ILabModGameConsoleQuery)LabMod.GetObjectOfType(type)).Event(processor, query, encrypted))
				{
					shouldstop = true;
				}
			}
			stop = shouldstop;
		}
	}
}
