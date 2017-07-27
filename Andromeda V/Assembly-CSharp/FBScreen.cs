using System;
using System.Collections.Generic;
using UnityEngine;

public class FBScreen
{
	private static bool resizable;

	public static bool FullScreen
	{
		get
		{
			return Screen.get_fullScreen();
		}
		set
		{
			Screen.set_fullScreen(value);
		}
	}

	public static int Height
	{
		get
		{
			return Screen.get_height();
		}
	}

	public static bool Resizable
	{
		get
		{
			return FBScreen.resizable;
		}
	}

	public static int Width
	{
		get
		{
			return Screen.get_width();
		}
	}

	static FBScreen()
	{
	}

	public FBScreen()
	{
	}

	public static FBScreen.Layout.OptionCenterHorizontal CenterHorizontal()
	{
		return new FBScreen.Layout.OptionCenterHorizontal();
	}

	public static FBScreen.Layout.OptionCenterVertical CenterVertical()
	{
		return new FBScreen.Layout.OptionCenterVertical();
	}

	public static FBScreen.Layout.OptionLeft Left(float amount)
	{
		return new FBScreen.Layout.OptionLeft()
		{
			Amount = amount
		};
	}

	public static void SetAspectRatio(int width, int height, params FBScreen.Layout[] layoutParams)
	{
		int _height = Screen.get_height() / height * width;
		Screen.SetResolution(_height, Screen.get_height(), Screen.get_fullScreen());
	}

	private static void SetLayout(IEnumerable<FBScreen.Layout> parameters)
	{
	}

	public static void SetResolution(int width, int height, bool fullscreen, int preferredRefreshRate = 0, params FBScreen.Layout[] layoutParams)
	{
		Screen.SetResolution(width, height, fullscreen, preferredRefreshRate);
	}

	public static void SetUnityPlayerEmbedCSS(string key, string value)
	{
	}

	public static FBScreen.Layout.OptionTop Top(float amount)
	{
		return new FBScreen.Layout.OptionTop()
		{
			Amount = amount
		};
	}

	public class Layout
	{
		public Layout()
		{
		}

		public class OptionCenterHorizontal : FBScreen.Layout
		{
			public OptionCenterHorizontal()
			{
			}
		}

		public class OptionCenterVertical : FBScreen.Layout
		{
			public OptionCenterVertical()
			{
			}
		}

		public class OptionLeft : FBScreen.Layout
		{
			public float Amount;

			public OptionLeft()
			{
			}
		}

		public class OptionTop : FBScreen.Layout
		{
			public float Amount;

			public OptionTop()
			{
			}
		}
	}
}