using System;
using UnityEngine;

public class GuiNewDualColorBar : GuiElement
{
	private Texture2D _custumDCBarTextureLeft;

	private Texture2D _custumDCBarTextureMidle;

	private Texture2D _custumDCBarTextureRight;

	private Texture2D _custumDCBarTextureFillOne;

	private Texture2D _custumDCBarTextureFillTwo;

	private float custumSizeBarWidth;

	private float maximum;

	private float value1;

	private float value2;

	public float Max
	{
		get
		{
			return this.maximum;
		}
		set
		{
			this.maximum = value;
		}
	}

	public float ValueOne
	{
		get
		{
			return this.value1;
		}
		set
		{
			this.value1 = value;
		}
	}

	public float ValueTwo
	{
		get
		{
			return this.value2;
		}
		set
		{
			this.value2 = value;
		}
	}

	public float Width
	{
		get
		{
			return this.custumSizeBarWidth;
		}
		set
		{
			this.custumSizeBarWidth = (value < 20f ? 20f : value);
		}
	}

	public GuiNewDualColorBar()
	{
		this.custumSizeBarWidth = 100f;
		this.value1 = 0f;
		this.value2 = 0f;
		this.maximum = 100f;
		this._custumDCBarTextureLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "bar_small_blue_left");
		this._custumDCBarTextureMidle = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "bar_small_blue_middle");
		this._custumDCBarTextureRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "bar_small_blue_right");
		this._custumDCBarTextureFillOne = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "bar_small_blue_center");
		this._custumDCBarTextureFillTwo = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "bar_small_orange_center");
	}

	public override void DrawGuiElement()
	{
		GUI.DrawTexture(new Rect(this.boundries.get_x(), this.boundries.get_y(), (float)this._custumDCBarTextureLeft.get_width(), (float)this._custumDCBarTextureLeft.get_height()), this._custumDCBarTextureLeft);
		GUI.DrawTexture(new Rect(this.boundries.get_x() + (float)this._custumDCBarTextureLeft.get_width(), this.boundries.get_y(), this.custumSizeBarWidth - (float)this._custumDCBarTextureLeft.get_width() - (float)this._custumDCBarTextureRight.get_width(), (float)this._custumDCBarTextureMidle.get_height()), this._custumDCBarTextureMidle);
		GUI.DrawTexture(new Rect(this.boundries.get_x() + this.custumSizeBarWidth - (float)this._custumDCBarTextureRight.get_width(), this.boundries.get_y(), (float)this._custumDCBarTextureRight.get_width(), (float)this._custumDCBarTextureRight.get_height()), this._custumDCBarTextureRight);
		GUI.DrawTexture(new Rect(this.boundries.get_x() + 6f, this.boundries.get_y(), (float)(Math.Min(this.value1, this.maximum) / this.maximum) * (this.custumSizeBarWidth - 12f), (float)this._custumDCBarTextureFillOne.get_height()), this._custumDCBarTextureFillOne);
		GUI.DrawTexture(new Rect(this.boundries.get_x() + 6f + (float)(Math.Min(this.value1, this.maximum) / this.maximum) * (this.custumSizeBarWidth - 12f), this.boundries.get_y(), (float)(Math.Min(Math.Max(this.value2 - this.value1, 0f), this.maximum) / this.maximum) * (this.custumSizeBarWidth - 12f), (float)this._custumDCBarTextureFillTwo.get_height()), this._custumDCBarTextureFillTwo);
	}

	public void SetCustumTexturesAndSize(string bundel, string bodyTexture, string fiilOne, string fiilTwo, float size)
	{
		this.custumSizeBarWidth = (size <= 20f ? 20f : size);
		this._custumDCBarTextureLeft = (Texture2D)playWebGame.assets.GetFromStaticSet(bundel, string.Concat(bodyTexture, "_left"));
		this._custumDCBarTextureMidle = (Texture2D)playWebGame.assets.GetFromStaticSet(bundel, string.Concat(bodyTexture, "_middle"));
		this._custumDCBarTextureRight = (Texture2D)playWebGame.assets.GetFromStaticSet(bundel, string.Concat(bodyTexture, "_right"));
		this._custumDCBarTextureFillOne = (Texture2D)playWebGame.assets.GetFromStaticSet(bundel, string.Concat(fiilOne, "_center"));
		this._custumDCBarTextureFillTwo = (Texture2D)playWebGame.assets.GetFromStaticSet(bundel, string.Concat(fiilTwo, "_center"));
	}
}