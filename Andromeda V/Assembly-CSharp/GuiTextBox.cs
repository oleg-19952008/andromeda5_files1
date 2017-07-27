using System;
using UnityEngine;

public class GuiTextBox : GuiElement
{
	private Texture2D _textFieldLeft;

	private Texture2D _textFieldCenter;

	private Texture2D _textFieldRight;

	private bool isTextureLoaded;

	public bool isMultiLine;

	private static Font font;

	public string text;

	public string controlName = string.Empty;

	private GUIStyle style;

	public Action Validate;

	public Action<object> Confirm;

	public object confirmParam;

	private bool isControlOnFocus;

	private bool isActiveStateTextureLoaded;

	public float borderLeft = 10f;

	public float borderTop = 7f;

	public float borderRight = 10f;

	public float borderBottom = 7f;

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

	public string EnteredText
	{
		get
		{
			return this.text;
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

	public Color TextColor
	{
		set
		{
			this.style.get_normal().set_textColor(value);
		}
	}

	public GuiTextBox()
	{
		this.Initialize();
		this.style = new GUIStyle();
		this.style.set_font(GuiLabel.FontMedium);
		this.style.set_alignment(5);
		this.style.set_clipping(1);
		this.style.get_normal().set_textColor(Color.get_white());
		this.text = string.Empty;
		this.isControlOnFocus = false;
		this.isActiveStateTextureLoaded = false;
	}

	public void Clear()
	{
		this.text = string.Empty;
	}

	public override void DrawGuiElement()
	{
		if (this.controlName != string.Empty && Event.get_current().Equals(Event.KeyboardEvent("return")) && GUI.GetNameOfFocusedControl() == this.controlName && this.Confirm != null)
		{
			this.Confirm.Invoke(this.confirmParam);
		}
		if (!this.isTextureLoaded)
		{
			this.Initialize();
		}
		GUI.DrawTexture(new Rect(this.boundries.get_x(), this.boundries.get_y(), (float)this._textFieldLeft.get_width(), (float)this._textFieldLeft.get_height()), this._textFieldLeft);
		GUI.DrawTexture(new Rect(this.boundries.get_x() + this.boundries.get_width() - (float)this._textFieldRight.get_width(), this.boundries.get_y(), (float)this._textFieldRight.get_width(), (float)this._textFieldRight.get_height()), this._textFieldRight);
		GUI.DrawTexture(new Rect(this.boundries.get_x() + (float)this._textFieldLeft.get_width(), this.boundries.get_y(), this.boundries.get_width() - (float)this._textFieldLeft.get_width() - (float)this._textFieldRight.get_width(), (float)this._textFieldCenter.get_height()), this._textFieldCenter);
		if (!string.IsNullOrEmpty(this.controlName))
		{
			GUI.SetNextControlName(this.controlName);
		}
		string str = null;
		if (!this.isMultiLine)
		{
			this.style.set_wordWrap(false);
			str = GUI.TextField(new Rect(this.boundries.get_x() + this.borderLeft, this.boundries.get_y() + this.borderTop, this.boundries.get_width() - this.borderLeft - this.borderRight, this.boundries.get_height() - this.borderTop - this.borderBottom), this.text, this.style);
		}
		else
		{
			this.style.set_wordWrap(true);
			str = GUI.TextArea(new Rect(this.boundries.get_x() + this.borderLeft, this.boundries.get_y() + this.borderTop, this.boundries.get_width() - this.borderLeft - this.borderRight, this.boundries.get_height() - this.borderTop - this.borderBottom), this.text, this.style);
		}
		if (this.isControlOnFocus != this.isActiveStateTextureLoaded)
		{
			if (!this.isControlOnFocus)
			{
				this.isActiveStateTextureLoaded = false;
				this.SetFrameTexture("FrameworkGUI", "blueTextBox");
				this.TextColor = GuiNewStyleBar.blueColor;
			}
			else
			{
				this.isActiveStateTextureLoaded = true;
				this.SetFrameTexture("FrameworkGUI", "orangeTextBox");
				this.TextColor = GuiNewStyleBar.orangeColor;
			}
		}
		if (str != this.text)
		{
			this.text = str;
			if (this.Validate != null)
			{
				this.Validate.Invoke();
			}
		}
	}

	private void Initialize()
	{
		this.SetFrameTexture("FrameworkGUI", "blueTextBox");
	}

	public void SetFrameTexture(string bundle, string assetName)
	{
		this._textFieldLeft = (Texture2D)playWebGame.assets.GetFromStaticSet(bundle, string.Concat(assetName, "_left"));
		this._textFieldCenter = (Texture2D)playWebGame.assets.GetFromStaticSet(bundle, string.Concat(assetName, "_middle"));
		this._textFieldRight = (Texture2D)playWebGame.assets.GetFromStaticSet(bundle, string.Concat(assetName, "_right"));
		this.boundries.set_height((float)this._textFieldLeft.get_height());
		this.isTextureLoaded = true;
	}

	public void SetSingleTexture(string bundle, string assetName)
	{
		this._textFieldLeft = (Texture2D)playWebGame.assets.GetFromStaticSet(bundle, assetName);
		this._textFieldCenter = (Texture2D)playWebGame.assets.GetFromStaticSet(bundle, assetName);
		this._textFieldRight = (Texture2D)playWebGame.assets.GetFromStaticSet(bundle, assetName);
		this.isTextureLoaded = true;
	}
}