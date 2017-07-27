using System;
using System.Collections.Generic;
using UnityEngine;

public class GuiButton : GuiElement
{
	private AudioClip _leftClickSound;

	private AudioClip _rightClickSound;

	public bool isMuted;

	public bool clickEventOnUp;

	public int _marginLeft;

	private int _marginRight;

	private int _marginTop;

	public EventHandlerParam eventHandlerParam = new EventHandlerParam();

	private bool isClicked;

	public bool behaviourKeepClicked;

	public byte groupId;

	public Action<EventHandlerParam> Clicked;

	protected GUIStyle style = new GUIStyle();

	public Color textColorNormal = new Color(1f, 1f, 1f, 1f);

	public Color textColorHover = new Color(1f, 1f, 1f, 1f);

	public Color textColorClick = new Color(1f, 1f, 1f, 1f);

	public Color textColorDisabled = new Color(1f, 1f, 1f, 1f);

	protected string _caption = StaticData.Translate("key_btn_default");

	public bool isEnabled = true;

	protected GuiButton.BtnState _state;

	public GuiFramework.CommonSounds SoundClick;

	public GuiFramework.CommonSounds SoundHover = GuiFramework.CommonSounds.Hover;

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

	public string Caption
	{
		get
		{
			return this._caption;
		}
		set
		{
			this._caption = value;
		}
	}

	public UnityEngine.Font Font
	{
		set
		{
			this.style.set_font(value);
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
			this.style.set_wordWrap(true);
		}
	}

	public bool IsClicked
	{
		get
		{
			return this.isClicked;
		}
		set
		{
			if (!this.behaviourKeepClicked)
			{
				throw new InvalidOperationException("Cannot set IsClicked on a button not having behaviourKeepClicked set to true!");
			}
			if (this.isClicked == value)
			{
				return;
			}
			if (!value)
			{
				this.isClicked = false;
				return;
			}
			this.isClicked = true;
			if (this.container != null)
			{
				foreach (GuiButton buttonGroup in this.container.GetButtonGroup(this.groupId))
				{
					if ((object)buttonGroup != (object)this)
					{
						buttonGroup.isClicked = false;
					}
				}
			}
			if (this.Clicked != null)
			{
				this.Clicked.Invoke(this.eventHandlerParam);
			}
		}
	}

	public AudioClip LeftClickSound
	{
		get
		{
			if (this._leftClickSound != null)
			{
				return this._leftClickSound;
			}
			return (AudioClip)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "buttonClick");
		}
	}

	public int MarginRight
	{
		set
		{
			this._marginRight = value;
		}
	}

	public int MarginTop
	{
		set
		{
			this._marginTop = value;
		}
	}

	public AudioClip RightClickSound
	{
		get
		{
			if (this._rightClickSound != null)
			{
				return this._rightClickSound;
			}
			return (AudioClip)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "buttonClick");
		}
	}

	public GuiButton.BtnState State
	{
		get
		{
			if (!this.isEnabled)
			{
				return GuiButton.BtnState.Disabled;
			}
			if (this.behaviourKeepClicked && this.isClicked)
			{
				return GuiButton.BtnState.LeftClicked;
			}
			if (!this.isHovered)
			{
				return GuiButton.BtnState.Normal;
			}
			if (Input.GetMouseButton(0))
			{
				this.eventHandlerParam.button = EventHandlerParam.MouseButton.Left;
				return GuiButton.BtnState.LeftClicked;
			}
			if (!Input.GetMouseButton(1))
			{
				return GuiButton.BtnState.Hover;
			}
			this.eventHandlerParam.button = EventHandlerParam.MouseButton.Right;
			return GuiButton.BtnState.RightClicked;
		}
	}

	public GuiButton()
	{
		this.style.set_margin(new RectOffset(10, 10, 1, 1));
		this.style.set_font(GuiLabel.FontBold);
		this.isHoverAware = true;
	}

	public override void DrawGuiElement()
	{
		this._state = this.State;
		switch (this._state)
		{
			case GuiButton.BtnState.Normal:
			{
				this.style.get_normal().set_textColor(this.textColorNormal);
				break;
			}
			case GuiButton.BtnState.Hover:
			{
				this.style.get_normal().set_textColor(this.textColorHover);
				break;
			}
			case GuiButton.BtnState.LeftClicked:
			case GuiButton.BtnState.RightClicked:
			{
				this.style.get_normal().set_textColor(this.textColorClick);
				break;
			}
			case GuiButton.BtnState.Disabled:
			{
				this.style.get_normal().set_textColor(this.textColorDisabled);
				break;
			}
			default:
			{
				goto case GuiButton.BtnState.Normal;
			}
		}
		Rect rect = new Rect(this.boundries.get_x() + (float)this._marginLeft, this.boundries.get_y() + (float)this._marginTop, this.boundries.get_width() - (float)this._marginRight - (float)this._marginLeft, this.boundries.get_height());
		GUI.Label(rect, this._caption, this.style);
	}

	public void SetColor(Color newColor)
	{
		Color color = newColor;
		Color color1 = color;
		this.textColorDisabled = color;
		Color color2 = color1;
		color1 = color2;
		this.textColorClick = color2;
		Color color3 = color1;
		color1 = color3;
		this.textColorHover = color3;
		this.textColorNormal = color1;
	}

	public void SetLeftClickSound(string bundleName, string clipName)
	{
		this._leftClickSound = (AudioClip)playWebGame.assets.GetFromStaticSet(bundleName, clipName);
	}

	public void SetRegularFont()
	{
		this.style.set_margin(new RectOffset(10, 10, 1, 1));
		this.style.set_font(GuiLabel.FontMedium);
	}

	public void SetRightClickSound(string bundleName, string clipName)
	{
		this._rightClickSound = (AudioClip)playWebGame.assets.GetFromStaticSet(bundleName, clipName);
	}

	public enum BtnState
	{
		Normal,
		Hover,
		LeftClicked,
		RightClicked,
		Disabled
	}
}