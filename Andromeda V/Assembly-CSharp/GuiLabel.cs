using System;
using UnityEngine;

public class GuiLabel : GuiElement
{
	private static UnityEngine.Font fontLarge;

	private static UnityEngine.Font fontMedium;

	private static UnityEngine.Font fontBold;

	public string text;

	private GUIStyle style;

	private float angel;

	public TextAnchor Alignment
	{
		get
		{
			return this.style.get_alignment();
		}
		set
		{
			this.style.set_alignment(value);
		}
	}

	public TextClipping Clipping
	{
		set
		{
			this.style.set_clipping(value);
		}
	}

	public UnityEngine.Font Font
	{
		set
		{
			this.style.set_font(value);
		}
	}

	public static UnityEngine.Font FontBold
	{
		get
		{
			if (GuiLabel.fontBold == null)
			{
				GuiLabel.fontBold = (UnityEngine.Font)Resources.Load(GuiLabel.GetFontName(GuiLabelFontType.Bold), typeof(UnityEngine.Font));
			}
			return GuiLabel.fontBold;
		}
		set
		{
			GuiLabel.fontBold = value;
		}
	}

	public static UnityEngine.Font FontLarge
	{
		get
		{
			if (GuiLabel.fontLarge == null)
			{
				GuiLabel.fontLarge = (UnityEngine.Font)Resources.Load(GuiLabel.GetFontName(GuiLabelFontType.Large), typeof(UnityEngine.Font));
			}
			return GuiLabel.fontLarge;
		}
		set
		{
			GuiLabel.fontLarge = value;
		}
	}

	public static UnityEngine.Font FontMedium
	{
		get
		{
			if (GuiLabel.fontMedium == null)
			{
				GuiLabel.fontMedium = (UnityEngine.Font)Resources.Load(GuiLabel.GetFontName(GuiLabelFontType.Medium), typeof(UnityEngine.Font));
			}
			return GuiLabel.fontMedium;
		}
		set
		{
			GuiLabel.fontMedium = value;
		}
	}

	public int FontSize
	{
		get
		{
			return this.style.get_fontSize();
		}
		set
		{
			this.style.set_fontSize(value);
		}
	}

	public bool Italic
	{
		set
		{
			if (!value)
			{
				this.style.set_fontStyle(0);
			}
			else
			{
				this.style.set_fontStyle(2);
			}
		}
	}

	public Color TextColor
	{
		get
		{
			return this.style.get_normal().get_textColor();
		}
		set
		{
			this.style.get_normal().set_textColor(value);
		}
	}

	public float TextHeight
	{
		get
		{
			if (!this.style.get_wordWrap())
			{
				return this.boundries.get_height();
			}
			return this.style.CalcHeight(new GUIContent(this.text), this.boundries.get_width());
		}
	}

	public float TextWidth
	{
		get
		{
			Vector2 vector2 = this.style.CalcSize(new GUIContent(this.text));
			return vector2.x;
		}
	}

	public bool WordWrap
	{
		set
		{
			this.style.set_wordWrap(value);
		}
	}

	public GuiLabel()
	{
		this.style = new GUIStyle();
		this.style.set_font(GuiLabel.FontMedium);
		this.style.get_normal().set_textColor(Color.get_white());
		this.style.set_fontSize(10);
		this.style.set_wordWrap(true);
	}

	public override void DrawGuiElement()
	{
		if (this.angel == 0f)
		{
			GUI.Label(this.boundries, this.text, this.style);
		}
		else
		{
			GUIUtility.RotateAroundPivot(this.angel, new Vector2(base.X, base.Y));
			GUI.Label(this.boundries, this.text, this.style);
			GUIUtility.RotateAroundPivot(-this.angel, new Vector2(base.X, base.Y));
		}
	}

	public static string GetFontName(GuiLabelFontType fontType)
	{
		switch (fontType)
		{
			case GuiLabelFontType.Large:
			{
				return "Play-Bold";
			}
			case GuiLabelFontType.Medium:
			{
				return "Play-Regular";
			}
			case GuiLabelFontType.Bold:
			{
				return "Play-Bold";
			}
		}
		return "Play-Regular";
	}

	public void SetAngel(float v)
	{
		this.angel = v;
	}
}