using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace AndromedaLauncher
{
	[CompilerGenerated]
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	internal class Resource1
	{
		private static System.Resources.ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		internal static Bitmap background_2
		{
			get
			{
				return (Bitmap)Resource1.ResourceManager.GetObject("background_2", Resource1.resourceCulture);
			}
		}

		internal static Bitmap button_active
		{
			get
			{
				return (Bitmap)Resource1.ResourceManager.GetObject("button_active", Resource1.resourceCulture);
			}
		}

		internal static Bitmap button_active2
		{
			get
			{
				return (Bitmap)Resource1.ResourceManager.GetObject("button_active2", Resource1.resourceCulture);
			}
		}

		internal static Bitmap button_hvr
		{
			get
			{
				return (Bitmap)Resource1.ResourceManager.GetObject("button_hvr", Resource1.resourceCulture);
			}
		}

		internal static Bitmap button_inactive
		{
			get
			{
				return (Bitmap)Resource1.ResourceManager.GetObject("button_inactive", Resource1.resourceCulture);
			}
		}

		internal static Bitmap button_inactive2
		{
			get
			{
				return (Bitmap)Resource1.ResourceManager.GetObject("button_inactive2", Resource1.resourceCulture);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resource1.resourceCulture;
			}
			set
			{
				Resource1.resourceCulture = value;
			}
		}

		internal static byte[] font
		{
			get
			{
				return (byte[])Resource1.ResourceManager.GetObject("font", Resource1.resourceCulture);
			}
		}

		internal static Bitmap frame_loading
		{
			get
			{
				return (Bitmap)Resource1.ResourceManager.GetObject("frame_loading", Resource1.resourceCulture);
			}
		}

		internal static Bitmap frame_news
		{
			get
			{
				return (Bitmap)Resource1.ResourceManager.GetObject("frame_news", Resource1.resourceCulture);
			}
		}

		internal static Icon Icon2
		{
			get
			{
				return (Icon)Resource1.ResourceManager.GetObject("Icon2", Resource1.resourceCulture);
			}
		}

		internal static Bitmap loading_bar
		{
			get
			{
				return (Bitmap)Resource1.ResourceManager.GetObject("loading_bar", Resource1.resourceCulture);
			}
		}

		internal static Bitmap menu_separator
		{
			get
			{
				return (Bitmap)Resource1.ResourceManager.GetObject("menu_separator", Resource1.resourceCulture);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static System.Resources.ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resource1.resourceMan, null))
				{
					Resource1.resourceMan = new System.Resources.ResourceManager("AndromedaLauncher.Resource1", typeof(Resource1).Assembly);
				}
				return Resource1.resourceMan;
			}
		}

		internal Resource1()
		{
		}
	}
}