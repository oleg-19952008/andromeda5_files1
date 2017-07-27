using System;
using UnityEngine;

public class GuiDotBar : GuiElement
{
	private Texture2D _emptyDot;

	private Texture2D _fillDot;

	public float maximum;

	public float current;

	public Texture2D SetEmpty
	{
		set
		{
			this._emptyDot = value;
			this.boundries.set_height((float)this._emptyDot.get_height());
		}
	}

	public Texture2D SetFill
	{
		set
		{
			this._fillDot = value;
		}
	}

	public GuiDotBar()
	{
		this.current = 0f;
		this.maximum = 10f;
		this._emptyDot = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "emptryDot");
		this._fillDot = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "fillDot");
		this.boundries.set_height((float)this._emptyDot.get_height());
		this.boundries.set_width(this.maximum * (float)this._emptyDot.get_width() + 8f * (this.maximum - 1f));
	}

	public override void DrawGuiElement()
	{
		for (int i = 0; (float)i < this.maximum; i++)
		{
			GUI.DrawTexture(new Rect(this.boundries.get_x() + (float)(i * (this._emptyDot.get_width() + 8)), this.boundries.get_y(), (float)this._emptyDot.get_width(), (float)this._emptyDot.get_height()), this._emptyDot);
		}
		for (int j = 0; (float)j < this.current; j++)
		{
			GUI.DrawTexture(new Rect(this.boundries.get_x() + (float)(j * (this._emptyDot.get_width() + 8)), this.boundries.get_y(), (float)this._emptyDot.get_width(), (float)this._emptyDot.get_height()), this._fillDot);
		}
	}
}