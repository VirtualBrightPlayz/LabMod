using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LabMod
{
	public class LabMod
	{
		private static List<object> _list = new List<object>();
		public static void RegisterEvent(object obj)
		{
			_list.Add(obj);
		}

		public static object GetObjectOfType(Type t)
		{
			return _list.Find(o => o.GetType() == t);
		}
	}
}
