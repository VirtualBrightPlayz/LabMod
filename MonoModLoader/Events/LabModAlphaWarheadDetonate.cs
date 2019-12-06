using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMod.Events
{
	public interface ILabModAlphaWarheadDetonate : ILabModEvent
	{
		void Event(AlphaWarheadController ccm);
	}

	public class LabModAlphaWarheadDetonate
	{
		public static List<Type> types_update = new List<Type>();

		public static void Init()
		{
			types_update = new List<Type>(AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => typeof(ILabModAlphaWarheadDetonate).IsAssignableFrom(p) && !p.IsInterface));
		}

		public static void TriggerEvent(AlphaWarheadController ccm)
		{
			foreach (var type in types_update)
			{
				if (LabMod.GetObjectOfType(type) == null)
					continue;
				((ILabModAlphaWarheadDetonate)LabMod.GetObjectOfType(type)).Event(ccm);
			}
		}
	}
}
