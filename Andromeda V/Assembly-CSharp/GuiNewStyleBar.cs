using System;
using UnityEngine;

public class GuiNewStyleBar : GuiElement
{
	private Texture2D _barTexture;

	private Texture2D _fill;

	public Color fillColor;

	private bool isFixedSize = true;

	private bool isNewStyle;

	private Texture2D _custumBarTextureLeft;

	private Texture2D _custumBarTextureMidle;

	private Texture2D _custumBarTextureRight;

	private Texture2D _custumBarTextureCenter;

	private int custumSizeBarWidth = 60;

	public static Color cyanColor;

	public static Color blueColor;

	public static Color blueColorDisable;

	public static Color blackColorTransperant;

	public static Color blackColorTransperant60;

	public static Color greenColor;

	public static Color redColor;

	public static Color orangeColor;

	public static Color purpleColor;

	public static Color darkPurpuleColor;

	public static Color aquamarineColor;

	public static Color bronzeColor;

	public static Color silverColor;

	public static Color goldColor;

	public static Color eqBtnColor;

	public static Color novaBtnColor;

	public static Color blueButtonsColor;

	public float maximum;

	public float current;

	public int height = 3;

	static GuiNewStyleBar()
	{
		GuiNewStyleBar.cyanColor = new Color(0.2156f, 1f, 1f);
		GuiNewStyleBar.blueColor = new Color(0.1176f, 0.6862f, 0.8862f);
		GuiNewStyleBar.blueColorDisable = new Color(0.1176f, 0.6862f, 0.8862f, 0.5f);
		GuiNewStyleBar.blackColorTransperant = new Color(0f, 0f, 0f, 0.75f);
		GuiNewStyleBar.blackColorTransperant60 = new Color(0f, 0f, 0f, 0.6f);
		GuiNewStyleBar.greenColor = new Color(0.0862f, 0.796f, 0f);
		GuiNewStyleBar.redColor = new Color(1f, 0.0745f, 0.0745f);
		GuiNewStyleBar.orangeColor = new Color(1f, 0.6901f, 0f);
		GuiNewStyleBar.purpleColor = new Color(0.949f, 0.364f, 0.98f);
		GuiNewStyleBar.darkPurpuleColor = new Color(0.494f, 0.376f, 0.886f);
		GuiNewStyleBar.aquamarineColor = new Color(0.03921f, 0.95686f, 1f);
		GuiNewStyleBar.bronzeColor = new Color(0.9648f, 0.7382f, 0.5f);
		GuiNewStyleBar.silverColor = new Color(0.8281f, 0.8281f, 0.8281f);
		GuiNewStyleBar.goldColor = new Color(0.9218f, 0.7265f, 0.0117f);
		GuiNewStyleBar.eqBtnColor = new Color(0.8789f, 0.5429f, 0.8632f);
		GuiNewStyleBar.novaBtnColor = new Color(1f, 0.8745f, 0.596f);
		GuiNewStyleBar.blueButtonsColor = new Color(0.7686f, 0.9215f, 1f);
	}

	public GuiNewStyleBar()
	{
		this.isHoverAware = true;
		this.fillColor = GuiNewStyleBar.blueColor;
		this.current = 0f;
		this.maximum = 100f;
		this._barTexture = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "bar_blue");
		this.boundries.set_width((float)this._barTexture.get_width());
		this.boundries.set_height((float)this._barTexture.get_height());
		this._fill = new Texture2D(1, 1);
		this._fill.SetPixel(0, 0, this.fillColor);
		this._fill.Apply();
	}

	public override void DrawGuiElement()
	{
		if (this.maximum < 1f)
		{
			this.maximum = 1f;
		}
		if (this.current > this.maximum)
		{
			this.current = this.maximum;
		}
		if (this.current < 0f)
		{
			this.current = 0f;
		}
		if (!this.isFixedSize)
		{
			GUI.DrawTexture(new Rect(this.boundries.get_x(), this.boundries.get_y(), (float)this._custumBarTextureLeft.get_width(), (float)this._custumBarTextureLeft.get_height()), this._custumBarTextureLeft);
			GUI.DrawTexture(new Rect(this.boundries.get_x() + (float)this._custumBarTextureLeft.get_width(), this.boundries.get_y(), (float)(this.custumSizeBarWidth - this._custumBarTextureLeft.get_width() - this._custumBarTextureRight.get_width()), (float)this._custumBarTextureMidle.get_height()), this._custumBarTextureMidle);
			GUI.DrawTexture(new Rect(this.boundries.get_x() + (float)this.custumSizeBarWidth - (float)this._custumBarTextureRight.get_width(), this.boundries.get_y(), (float)this._custumBarTextureRight.get_width(), (float)this._custumBarTextureRight.get_height()), this._custumBarTextureRight);
			if (!this.isNewStyle)
			{
				GUI.DrawTexture(new Rect(this.boundries.get_x() + 3f, this.boundries.get_y() + 3f, (float)(Math.Min(this.current, this.maximum) / this.maximum) * (float)(this.custumSizeBarWidth - 6), (float)this.height), this._fill);
			}
			else
			{
				GUI.DrawTexture(new Rect(this.boundries.get_x() + 6f, this.boundries.get_y(), (float)(Math.Min(this.current, this.maximum) / this.maximum) * (float)(this.custumSizeBarWidth - 12), (float)this._custumBarTextureCenter.get_height()), this._custumBarTextureCenter);
			}
		}
		else
		{
			GUI.DrawTexture(new Rect(this.boundries.get_x(), this.boundries.get_y(), (float)this._barTexture.get_width(), (float)this._barTexture.get_height()), this._barTexture);
			GUI.DrawTexture(new Rect(this.boundries.get_x() + 3f, this.boundries.get_y() + 3f, (float)(Math.Min(this.current, this.maximum) / this.maximum) * (float)(this._barTexture.get_width() - 6), (float)this.height), this._fill);
		}
	}

	private void LoadCustumSizeTextures()
	{
		this.isFixedSize = false;
		this._custumBarTextureLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "custumSizeBar_left");
		this._custumBarTextureMidle = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "custumSizeBar_midle");
		this._custumBarTextureRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "custumSizeBar_right");
	}

	public void SetBarBlue()
	{
		this._barTexture = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "bar_blue");
		this.boundries.set_width((float)this._barTexture.get_width());
		this.boundries.set_height((float)this._barTexture.get_height());
		this.fillColor = GuiNewStyleBar.blueColor;
		this._fill = new Texture2D(1, 1);
		this._fill.SetPixel(0, 0, this.fillColor);
		this._fill.Apply();
	}

	public void SetBarExp()
	{
		this._barTexture = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "bar_exp");
		this.boundries.set_width((float)this._barTexture.get_width());
		this.boundries.set_height((float)this._barTexture.get_height());
		this.fillColor = Color.get_white();
		this._fill = new Texture2D(1, 1);
		this._fill.SetPixel(0, 0, this.fillColor);
		this._fill.Apply();
	}

	public void SetBarGreen()
	{
		this._barTexture = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "bar_green");
		this.boundries.set_width((float)this._barTexture.get_width());
		this.boundries.set_height((float)this._barTexture.get_height());
		this.fillColor = GuiNewStyleBar.greenColor;
		this._fill = new Texture2D(1, 1);
		this._fill.SetPixel(0, 0, this.fillColor);
		this._fill.Apply();
	}

	public void SetBarOrange()
	{
		this._barTexture = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "bar_orange");
		this.boundries.set_width((float)this._barTexture.get_width());
		this.boundries.set_height((float)this._barTexture.get_height());
		this.fillColor = GuiNewStyleBar.orangeColor;
		this._fill = new Texture2D(1, 1);
		this._fill.SetPixel(0, 0, this.fillColor);
		this._fill.Apply();
	}

	public void SetCustumSize(int width, Color color)
	{
		this.custumSizeBarWidth = (width <= 5 ? 5 : width);
		this.LoadCustumSizeTextures();
		this.fillColor = color;
		this._fill = new Texture2D(1, 1);
		this._fill.SetPixel(0, 0, this.fillColor);
		this._fill.Apply();
		this.boundries.set_width((float)this.custumSizeBarWidth);
	}

	public void SetCustumSizeBlueBar(int size)
	{
		this.isFixedSize = false;
		this.isNewStyle = true;
		this.custumSizeBarWidth = (size <= 20 ? 20 : size);
		this._custumBarTextureLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "bar_small_blue_left");
		this._custumBarTextureMidle = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "bar_small_blue_middle");
		this._custumBarTextureRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "bar_small_blue_right");
		this._custumBarTextureCenter = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "bar_small_blue_center");
	}

	public void SetCustumSizeGreenBar(int size)
	{
		this.isFixedSize = false;
		this.isNewStyle = true;
		this.custumSizeBarWidth = (size <= 20 ? 20 : size);
		this._custumBarTextureLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "bar_small_green_left");
		this._custumBarTextureMidle = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "bar_small_green_middle");
		this._custumBarTextureRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "bar_small_green_right");
		this._custumBarTextureCenter = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "bar_small_green_center");
	}

	public void SetCustumSizeOrangeBar(int size)
	{
		this.isFixedSize = false;
		this.isNewStyle = true;
		this.custumSizeBarWidth = (size <= 20 ? 20 : size);
		this._custumBarTextureLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "bar_small_orange_left");
		this._custumBarTextureMidle = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "bar_small_orange_middle");
		this._custumBarTextureRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "bar_small_orange_right");
		this._custumBarTextureCenter = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "bar_small_orange_center");
	}

	public void SetCustumSizeRedBar(int size)
	{
		this.isFixedSize = false;
		this.isNewStyle = true;
		this.custumSizeBarWidth = (size <= 20 ? 20 : size);
		this._custumBarTextureLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "bar_small_red_left");
		this._custumBarTextureMidle = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "bar_small_red_middle");
		this._custumBarTextureRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "bar_small_red_right");
		this._custumBarTextureCenter = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "bar_small_red_center");
	}

	public void SetTexture(string bundleName, string resourceName)
	{
		this._barTexture = (Texture2D)playWebGame.assets.GetFromStaticSet(bundleName, resourceName);
		if (this._barTexture == null)
		{
			Debug.LogError(string.Concat("Could not load texture ", resourceName ?? "NULL"));
			this._barTexture = new Texture2D(1, 1);
			this._barTexture.SetPixel(0, 0, new Color(0f, 0f, 0f, 0f));
			this._barTexture.Apply();
		}
		this.boundries.set_width((float)this._barTexture.get_width());
		this.boundries.set_height((float)this._barTexture.get_height());
	}
}