using System;
using UnityEngine;

public class GuiBar : GuiElement
{
	private Texture2D _barLeft;

	private Texture2D _barFill;

	private Texture2D _barRight;

	private Texture2D _barMiddle;

	public Color fillColor;

	public float maximum;

	public float current;

	public Texture2D SetFill
	{
		set
		{
			this._barFill = value;
		}
	}

	public Texture2D SetLeft
	{
		set
		{
			this._barLeft = value;
			this.boundries.set_height((float)this._barLeft.get_height());
		}
	}

	public Texture2D SetMiddle
	{
		set
		{
			this._barMiddle = value;
		}
	}

	public Texture2D SetRight
	{
		set
		{
			this._barRight = value;
		}
	}

	public float Width
	{
		get
		{
			return this.boundries.get_width();
		}
		set
		{
			this.boundries.set_width(value);
		}
	}

	public GuiBar()
	{
		this.fillColor = new Color(0.996f, 0.6796f, 0.0039f, 1f);
		this.current = 0f;
		this.maximum = 100f;
		this._barLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "orangeSmallBarLeft");
		this._barFill = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "orangeSmallBarFill");
		this._barRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "orangeSmallBarRight");
		this._barMiddle = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "orangeSmallBarMiddle");
		this.boundries.set_height((float)this._barLeft.get_height());
	}

	public GuiBar(string bundel, string asset)
	{
		this.fillColor = new Color(0.996f, 0.6796f, 0.0039f, 1f);
		this.current = 0f;
		this.maximum = 100f;
		this._barLeft = (Texture2D)playWebGame.assets.GetFromStaticSet(bundel, string.Concat(asset, "Left"));
		this._barFill = (Texture2D)playWebGame.assets.GetFromStaticSet(bundel, string.Concat(asset, "Fill"));
		this._barRight = (Texture2D)playWebGame.assets.GetFromStaticSet(bundel, string.Concat(asset, "Right"));
		this._barMiddle = (Texture2D)playWebGame.assets.GetFromStaticSet(bundel, string.Concat(asset, "Middle"));
		this.boundries.set_height((float)this._barLeft.get_height());
	}

	public GuiBar(Texture2D Left, Texture2D Fill, Texture2D Right, Texture2D Middle)
	{
		this.fillColor = new Color(0.996f, 0.6796f, 0.0039f, 1f);
		this.current = 0f;
		this.maximum = 100f;
		this._barLeft = Left;
		this._barFill = Fill;
		this._barRight = Right;
		this._barMiddle = Middle;
		this.boundries.set_height((float)this._barLeft.get_height());
	}

	public override void DrawGuiElement()
	{
		GUI.DrawTexture(new Rect(this.boundries.get_x(), this.boundries.get_y(), (float)this._barLeft.get_width(), (float)this._barLeft.get_height()), this._barLeft);
		GUI.DrawTexture(new Rect(this.boundries.get_x() + (float)this._barLeft.get_width(), this.boundries.get_y(), this.boundries.get_width() - (float)this._barLeft.get_width() - (float)this._barRight.get_width(), (float)this._barMiddle.get_height()), this._barMiddle);
		GUI.DrawTexture(new Rect(this.boundries.get_x() + this.boundries.get_width() - (float)this._barRight.get_width(), this.boundries.get_y(), (float)this._barRight.get_width(), (float)this._barRight.get_height()), this._barRight);
		GUI.DrawTexture(new Rect(this.boundries.get_x() + (float)this._barLeft.get_width(), this.boundries.get_y(), (float)(Math.Min(this.current, this.maximum) / this.maximum * (this.boundries.get_width() - (float)this._barLeft.get_width() - (float)this._barRight.get_width())), (float)this._barFill.get_height()), this._barFill);
	}
}