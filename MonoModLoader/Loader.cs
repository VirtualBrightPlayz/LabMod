#pragma warning disable CS0626 // orig_ method is marked external and has no attributes on it.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoMod;
using LabMod.Events;
using System.IO;
using System.Reflection;

namespace LabMod.Loader
{

	[MonoModPatch("global::ServerConsole")]
	class Loader : ServerConsole
	{
		public static List<Assembly> plugins;

		public extern void orig_Update();
		public void Update()
		{
			LabModUpdate.TriggerEvent();
		}

		public extern void orig_Start();
		public void Start()
		{
			orig_Start();
			//Console.WriteLine(q);
			CustomNetworkManager.Modded = true;
			AddLog("[ModLoader] Start");
			plugins = new List<Assembly>();
			string[] files = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Mods"), "*.dll");
			foreach (var file in files)
			{
				AddLog("[ModLoader] Loading mod: " + file);
				try
				{
					var asm = Assembly.LoadFrom(file);
					foreach (var type in asm.GetTypes())
					{
						if (type.GetMethod("Main") != null)
							type.GetMethod("Main").Invoke(null, new object[0]);
					}
					plugins.Add(asm);
					AppDomain.CurrentDomain.Load(asm.GetName());
					AddLog("[ModLoader] Loaded mod: " + file);
				}
				catch (Exception e)
				{
					AddLog("[ModLoader] Failed to load mod: " + file);
					AddLog("[ModLoader] Fault: " + e.ToString());
				}
			}
			try
			{
				LabModUpdate.Init();
				LabModGameConsoleQuery.Init();
				LabModPlayerHurt.Init();
				LabModPostRoundStart.Init();
				LabModRoundStart.Init();
				LabModPreRoundStart.Init();
				LabModRoundEnd.Init();
				LabModJoinLate.Init();
				LabModPlayerEscape.Init();
				LabModPlayerJoin.Init();
			}
			catch (Exception e)
			{
				AddLog("[ModLoader] Failed to load mod events");
				AddLog("[ModLoader] Fault: " + e.ToString());
			}
		}
	}
}
