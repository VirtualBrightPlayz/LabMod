using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMod.Events
{
	public interface ILabModCISpawn : ILabModEvent
	{
		void Event(MTFRespawn ccm);
	}

	public class LabModCISpawn
	{
		public static List<Type> types_update = new List<Type>();

		public static void Init()
		{
			types_update = new List<Type>(AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(ILabModCISpawn).IsAssignableFrom(p) && !p.IsInterface));
		}

		public static void TriggerEvent(MTFRespawn ccm)
		{
			foreach (var type in types_update)
			{
				if (LabMod.GetObjectOfType(type) == null)
					continue;
				((ILabModCISpawn)LabMod.GetObjectOfType(type)).Event(ccm);
			}
		}
	}
}
