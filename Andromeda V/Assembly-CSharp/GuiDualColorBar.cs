using System;
using UnityEngine;

public class GuiDualColorBar : GuiElement
{
	private Texture2D _DCbarLeft;

	private Texture2D _FillCollor1;

	private Texture2D _FillCollor2;

	private Texture2D _DCbarRight;

	private Texture2D _DCbarMiddle;

	private Color color1;

	private Color color2;

	public float maximum;

	public float value1;

	public float value2;

	private bool IsTexture;

	public Texture2D SetLeft
	{
		set
		{
			this._DCbarLeft = value;
			this.boundries.set_height((float)this._DCbarLeft.get_height());
		}
	}

	public Texture2D SetMiddle
	{
		set
		{
			this._DCbarMiddle = value;
		}
	}

	public Texture2D SetRight
	{
		set
		{
			this._DCbarRight = value;
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

	public GuiDualColorBar(float val1, float val2, float max, Color c1, Color c2)
	{
		this.value1 = val1;
		this.value2 = val2;
		this.maximum = max;
		if (this._DCbarLeft == null)
		{
			this._DCbarLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "custumSizeBar_left");
			this._DCbarRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "custumSizeBar_right");
			this._DCbarMiddle = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "custumSizeBar_midle");
			this.color1 = c1;
			this.color2 = c2;
			this._FillCollor1 = new Texture2D(1, 1);
			this._FillCollor1.SetPixel(0, 0, this.color1);
			this._FillCollor1.Apply();
			this._FillCollor2 = new Texture2D(1, 1);
			this._FillCollor2.SetPixel(0, 0, this.color2);
			this._FillCollor2.Apply();
			this.boundries.set_height((float)this._DCbarLeft.get_height());
		}
	}

	public override void DrawGuiElement()
	{
		GUI.DrawTexture(new Rect(this.boundries.get_x(), this.boundries.get_y(), (float)this._DCbarLeft.get_width(), (float)this._DCbarLeft.get_height()), this._DCbarLeft);
		GUI.DrawTexture(new Rect(this.boundries.get_x() + (float)this._DCbarLeft.get_width(), this.boundries.get_y(), this.boundries.get_width() - (float)this._DCbarLeft.get_width() - (float)this._DCbarRight.get_width(), (float)this._DCbarMiddle.get_height()), this._DCbarMiddle);
		GUI.DrawTexture(new Rect(this.boundries.get_x() + this.boundries.get_width() - (float)this._DCbarRight.get_width(), this.boundries.get_y(), (float)this._DCbarRight.get_width(), (float)this._DCbarRight.get_height()), this._DCbarRight);
		if (this.IsTexture)
		{
			GUI.DrawTexture(new Rect(this.boundries.get_x() + (float)this._DCbarLeft.get_width() - 2f, this.boundries.get_y(), (float)(this.value2 / this.maximum) * (this.boundries.get_width() - (float)this._DCbarLeft.get_width() - (float)this._DCbarRight.get_width()) + 4f, (float)this._DCbarLeft.get_height()), this._FillCollor2);
			GUI.DrawTexture(new Rect(this.boundries.get_x() + (float)this._DCbarLeft.get_width() - 2f, this.boundries.get_y(), (float)(this.value1 / this.maximum) * (this.boundries.get_width() - (float)this._DCbarLeft.get_width() - (float)this._DCbarRight.get_width() + 4f), (float)this._DCbarLeft.get_height()), this._FillCollor1);
		}
		else
		{
			GUI.DrawTexture(new Rect(this.boundries.get_x() + 3f, this.boundries.get_y() + 3f, (float)(this.value2 / this.maximum) * (this.boundries.get_width() - 6f), 3f), this._FillCollor2);
			GUI.DrawTexture(new Rect(this.boundries.get_x() + 3f, this.boundries.get_y() + 3f, (float)(this.value1 / this.maximum) * (this.boundries.get_width() - 6f), 3f), this._FillCollor1);
		}
	}
}