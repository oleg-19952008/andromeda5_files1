//using System;
//using System.CodeDom.Compiler;
//using System.Configuration;
//using System.Diagnostics;
//using System.Runtime.CompilerServices;

//namespace AndromedaLauncher
//{
//	[CompilerGenerated]
//	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
//	internal sealed class Settings1 : ApplicationSettingsBase
//	{
//		private static Settings1 defaultInstance;

//		public static Settings1 Default
//		{
//			get
//			{
//				return Settings1.defaultInstance;
//			}
//		}

//		[DebuggerNonUserCode]
//		[DefaultSettingValue("D:\\Orbitron100\\Andromeda5.exe")]
//		[UserScopedSetting]
//		public string GameExecutable
//		{
//			get
//			{
//				return (string)this["GameExecutable"];
//			}
//			set
//			{
//				this["GameExecutable"] = value;
//			}
//		}

//		static Settings1()
//		{
//			Settings1.defaultInstance = (Settings1)SettingsBase.Synchronized(new Settings1());
//		}

//		public Settings1()
//		{
//		}
//	}
//}