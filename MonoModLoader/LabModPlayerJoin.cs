using LabMod.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMod.Events
{
	public interface ILabModPlayerJoin : ILabModEvent
	{
		void Event(CharacterClassManager ccm);
	}

	public class LabModPlayerJoin
	{
		public static List<Type> types_update = new List<Type>();

		public static void Init()
		{
			types_update = new List<Type>(AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(ILabModPlayerJoin).IsAssignableFrom(p) && !p.IsInterface));
		}

		public static void TriggerEvent(CharacterClassManager ccm)
		{
			foreach (var type in types_update)
			{
				if (LabMod.GetObjectOfType(type) == null)
					continue;
				((ILabModPlayerJoin)LabMod.GetObjectOfType(type)).Event(ccm);
			}
		}
	}
}
