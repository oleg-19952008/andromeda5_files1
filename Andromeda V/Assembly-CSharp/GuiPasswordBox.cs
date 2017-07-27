using System;
using UnityEngine;

public class GuiPasswordBox : GuiElement
{
	private Texture2D _textFieldLeft;

	private Texture2D _textFieldCenter;

	private Texture2D _textFieldRight;

	private bool isTextureLoaded;

	private static Font font;

	public string text;

	public string control_name;

	private GUIStyle style;

	public Action Validate;

	public Action Confirm;

	private bool isTextEresed;

	private bool isControlOnFocus;

	private bool isActiveStateTextureLoaded;

	public bool redrawOnFocus = true;

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

	public GuiPasswordBox()
	{
		this.Initialize();
		this.style = new GUIStyle();
		this.style.set_font(GuiLabel.FontMedium);
		this.style.set_fontSize(10);
		this.style.get_normal().set_textColor(Color.get_white());
		this.text = string.Empty;
		this.isTextEresed = false;
		this.isControlOnFocus = false;
		this.isActiveStateTextureLoaded = false;
	}

	public void Clear()
	{
		this.text = string.Empty;
	}

	public override void DrawGuiElement()
	{
		if (!this.isTextureLoaded)
		{
			this.Initialize();
		}
		GUI.DrawTexture(new Rect(this.boundries.get_x(), this.boundries.get_y(), (float)this._textFieldLeft.get_width(), (float)this._textFieldLeft.get_height()), this._textFieldLeft);
		GUI.DrawTexture(new Rect(this.boundries.get_x() + this.boundries.get_width() - (float)this._textFieldRight.get_width(), this.boundries.get_y(), (float)this._textFieldRight.get_width(), (float)this._textFieldRight.get_height()), this._textFieldRight);
		GUI.DrawTexture(new Rect(this.boundries.get_x() + (float)this._textFieldLeft.get_width(), this.boundries.get_y(), this.boundries.get_width() - (float)this._textFieldLeft.get_width() - (float)this._textFieldRight.get_width(), (float)this._textFieldCenter.get_height()), this._textFieldCenter);
		GUI.SetNextControlName(this.control_name);
		string empty = GUI.PasswordField(new Rect(this.boundries.get_x() + 10f, this.boundries.get_y() + 8f, this.boundries.get_width() - 20f, this.boundries.get_height() - 15f), this.text, '*', this.style);
		if (GUI.GetNameOfFocusedControl() != this.control_name)
		{
			this.isControlOnFocus = false;
		}
		else
		{
			if (!this.isTextEresed)
			{
				empty = string.Empty;
				this.isTextEresed = true;
			}
			this.isControlOnFocus = true;
		}
		if (this.isControlOnFocus != this.isActiveStateTextureLoaded)
		{
			if (!this.isControlOnFocus)
			{
				this.isActiveStateTextureLoaded = false;
				if (this.redrawOnFocus)
				{
					this.SetFrameTexture("FrameworkGUI", "blueTextBox");
					this.TextColor = GuiNewStyleBar.blueColor;
				}
			}
			else
			{
				this.isActiveStateTextureLoaded = true;
				if (this.redrawOnFocus)
				{
					this.SetFrameTexture("FrameworkGUI", "orangeTextBox");
					this.TextColor = GuiNewStyleBar.orangeColor;
				}
			}
		}
		if (empty != this.text)
		{
			this.text = empty;
			if (this.Validate != null)
			{
				this.Validate.Invoke();
			}
		}
		if ((Input.GetKeyDown(271) || Input.GetKeyDown(13)) && this.Confirm != null)
		{
			this.Confirm.Invoke();
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