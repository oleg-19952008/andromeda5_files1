using System;
using UnityEngine;

public class GuiElement
{
	internal GuiWindow container;

	public bool isActive;

	public bool isClippingBoundariesContent;

	public byte clippingBoundariesId = 255;

	private Rect containerRect;

	public Rect boundries;

	private Rect __tooltipTexturePos;

	private bool tooltipTexturePosSet;

	private string tooltipText;

	private GUIStyle tooltipStyle;

	public float tooltipTimeout;

	private float tooltipTimer;

	public bool tooltipShown;

	private static Font TTfont;

	public static Texture2D toolTipTopLeft;

	public static Texture2D toolTipTopRight;

	public static Texture2D toolTipTopCenter;

	public static Texture2D toolTipBottomLeft;

	public static Texture2D toolTipBottomCenter;

	public static Texture2D toolTipBottomRight;

	public static Texture2D toolTipMiddleLeft;

	public static Texture2D toolTipMiddleCenter;

	public static Texture2D toolTipMiddleRight;

	private bool isTextureLoaded;

	public GuiElement.DrawTooltipWindow drawTooltipWindowCallback;

	private bool isMouseOver;

	public GuiWindow tooltipWindow;

	public object tooltipWindowParam;

	public float tooltipTapDownTime = 0.5f;

	public object hoverParam;

	private Action<object, bool> _hovered;

	public bool isHovered;

	public bool isHoverAware;

	public Rect ContainerRect
	{
		get
		{
			if (this.container == null)
			{
				return this.containerRect;
			}
			return this.container.boundries;
		}
		set
		{
			this.containerRect = value;
		}
	}

	public Action<object, bool> Hovered
	{
		get
		{
			return this._hovered;
		}
		set
		{
			if (value != null)
			{
				this.isHoverAware = true;
			}
			this._hovered = value;
		}
	}

	public virtual bool IsMouseOver
	{
		get
		{
			bool _height;
			Vector3 _mousePosition = Input.get_mousePosition();
			float single = (float)Screen.get_height() - _mousePosition.y;
			float single1 = _mousePosition.x;
			Rect containerRect = this.ContainerRect;
			float _x = containerRect.get_x() + this.boundries.get_x();
			Rect rect = this.ContainerRect;
			Rect rect1 = new Rect(_x, rect.get_y() + this.boundries.get_y(), this.boundries.get_width(), this.boundries.get_height());
			bool flag = rect1.Contains(new Vector2(single1, single));
			if (this.isClippingBoundariesContent)
			{
				if (this.boundries.get_y() + this.boundries.get_height() <= 0f)
				{
					_height = false;
				}
				else
				{
					float _y = this.boundries.get_y();
					Rect rect2 = this.container.ClippingBoundaris(this.clippingBoundariesId);
					_height = _y < rect2.get_height();
				}
				if (_height)
				{
					float _x1 = this.ContainerRect.get_x();
					Rect rect3 = this.container.ClippingBoundaris(this.clippingBoundariesId);
					float _x2 = _x1 + rect3.get_x() + this.boundries.get_x();
					float single2 = this.ContainerRect.get_x();
					Rect rect4 = this.container.ClippingBoundaris(this.clippingBoundariesId);
					float single3 = Math.Max(_x2, single2 + rect4.get_x());
					float _y1 = this.ContainerRect.get_y();
					Rect rect5 = this.container.ClippingBoundaris(this.clippingBoundariesId);
					float _y2 = _y1 + rect5.get_y() + this.boundries.get_y();
					float _y3 = this.ContainerRect.get_y();
					Rect rect6 = this.container.ClippingBoundaris(this.clippingBoundariesId);
					rect1 = new Rect(single3, Math.Max(_y2, _y3 + rect6.get_y()), Math.Min(this.boundries.get_width(), this.boundries.get_x() + this.boundries.get_width()), Math.Min(this.boundries.get_height(), this.boundries.get_y() + this.boundries.get_height()));
					flag = rect1.Contains(new Vector2(single1, single));
				}
				else
				{
					flag = false;
				}
			}
			if (!flag)
			{
				this.isMouseOver = false;
				if (this.tooltipWindow != null)
				{
					AndromedaGui.gui.RemoveWindow(this.tooltipWindow.handler);
					this.tooltipWindow = null;
					this.tooltipShown = false;
				}
			}
			else if (!this.isMouseOver)
			{
				this.showTooltipWindow();
				this.isMouseOver = true;
			}
			return flag;
		}
	}

	public Rect ToolTipPos
	{
		set
		{
			this.__tooltipTexturePos = value;
			this.tooltipTexturePosSet = true;
		}
	}

	public string ToolTipText
	{
		set
		{
			this.tooltipText = value;
		}
	}

	public float X
	{
		get
		{
			return this.boundries.get_x();
		}
		set
		{
			this.boundries.set_x(value);
		}
	}

	public float Y
	{
		get
		{
			return this.boundries.get_y();
		}
		set
		{
			this.boundries.set_y(value);
		}
	}

	public GuiElement()
	{
		if (GuiElement.TTfont == null)
		{
			GuiElement.TTfont = GuiLabel.FontMedium;
		}
		this.tooltipStyle = new GUIStyle();
		this.tooltipStyle.set_font(GuiElement.TTfont);
		this.tooltipStyle.set_fontSize(10);
		this.tooltipStyle.set_alignment(4);
		this.tooltipStyle.get_normal().set_textColor(Color.get_white());
		this.tooltipStyle.set_wordWrap(true);
	}

	public virtual void DrawGuiElement()
	{
	}

	public void DrawToolTip()
	{
		Rect _TooltipTexturePos;
		if (!this.tooltipShown)
		{
			this.tooltipShown = true;
			this.tooltipTimer = this.tooltipTimeout;
		}
		else if (this.tooltipTimer > 0f)
		{
			GuiElement _smoothDeltaTime = this;
			_smoothDeltaTime.tooltipTimer = _smoothDeltaTime.tooltipTimer - Time.get_smoothDeltaTime();
			if (this.tooltipTimer > 0f)
			{
				return;
			}
			this.tooltipTimer = 0f;
			this.tooltipShown = true;
		}
		if (this.tooltipText != null && this.IsMouseOver && GuiFramework.draggingObject == null)
		{
			if (!this.isTextureLoaded)
			{
				this.LoadtToolTipTexture();
			}
			if (this.tooltipTexturePosSet)
			{
				_TooltipTexturePos = this.__tooltipTexturePos;
			}
			else
			{
				Vector2 vector2 = this.tooltipStyle.CalcSize(new GUIContent(this.tooltipText));
				_TooltipTexturePos = new Rect(this.boundries.get_x() - (vector2.x - this.boundries.get_width()) / 2f - 15f, this.boundries.get_y() - vector2.y - 38f, vector2.x + 30f, vector2.y + 12f);
			}
			if (_TooltipTexturePos.get_height() < (float)(GuiElement.toolTipTopLeft.get_height() + GuiElement.toolTipBottomLeft.get_height() + GuiElement.toolTipMiddleLeft.get_height()))
			{
				_TooltipTexturePos.set_height((float)(GuiElement.toolTipTopLeft.get_height() + GuiElement.toolTipBottomLeft.get_height() + GuiElement.toolTipMiddleLeft.get_height()));
			}
			if (_TooltipTexturePos.get_width() < (float)(GuiElement.toolTipTopLeft.get_width() + GuiElement.toolTipTopRight.get_width() + GuiElement.toolTipTopCenter.get_width()))
			{
				_TooltipTexturePos.set_width((float)(GuiElement.toolTipTopLeft.get_width() + GuiElement.toolTipTopRight.get_width() + GuiElement.toolTipTopCenter.get_width()));
			}
			GUI.DrawTexture(new Rect(_TooltipTexturePos.get_x(), _TooltipTexturePos.get_y(), (float)GuiElement.toolTipTopLeft.get_width(), (float)GuiElement.toolTipTopLeft.get_height()), GuiElement.toolTipTopLeft);
			GUI.DrawTexture(new Rect(_TooltipTexturePos.get_x() + _TooltipTexturePos.get_width() - (float)GuiElement.toolTipTopRight.get_width(), _TooltipTexturePos.get_y(), (float)GuiElement.toolTipTopRight.get_width(), (float)GuiElement.toolTipTopRight.get_height()), GuiElement.toolTipTopRight);
			GUI.DrawTexture(new Rect(_TooltipTexturePos.get_x() + (float)GuiElement.toolTipTopLeft.get_width(), _TooltipTexturePos.get_y(), _TooltipTexturePos.get_width() - (float)GuiElement.toolTipTopLeft.get_width() - (float)GuiElement.toolTipTopRight.get_width(), (float)GuiElement.toolTipTopLeft.get_height()), GuiElement.toolTipTopCenter);
			GUI.DrawTexture(new Rect(_TooltipTexturePos.get_x(), _TooltipTexturePos.get_y() + _TooltipTexturePos.get_height() - (float)GuiElement.toolTipBottomLeft.get_height(), (float)GuiElement.toolTipBottomLeft.get_width(), (float)GuiElement.toolTipBottomLeft.get_height()), GuiElement.toolTipBottomLeft);
			GUI.DrawTexture(new Rect(_TooltipTexturePos.get_x() + (float)GuiElement.toolTipBottomLeft.get_width(), _TooltipTexturePos.get_y() + _TooltipTexturePos.get_height() - (float)GuiElement.toolTipBottomLeft.get_height(), _TooltipTexturePos.get_width() - (float)GuiElement.toolTipBottomLeft.get_width() - (float)GuiElement.toolTipBottomRight.get_width(), (float)GuiElement.toolTipBottomCenter.get_height()), GuiElement.toolTipBottomCenter);
			GUI.DrawTexture(new Rect(_TooltipTexturePos.get_x() + _TooltipTexturePos.get_width() - (float)GuiElement.toolTipBottomRight.get_width(), _TooltipTexturePos.get_y() + _TooltipTexturePos.get_height() - (float)GuiElement.toolTipBottomLeft.get_height(), (float)GuiElement.toolTipBottomRight.get_width(), (float)GuiElement.toolTipBottomRight.get_height()), GuiElement.toolTipBottomRight);
			GUI.DrawTexture(new Rect(_TooltipTexturePos.get_x(), _TooltipTexturePos.get_y() + (float)GuiElement.toolTipTopLeft.get_height(), (float)GuiElement.toolTipMiddleLeft.get_width(), _TooltipTexturePos.get_height() - (float)GuiElement.toolTipTopLeft.get_height() - (float)GuiElement.toolTipBottomLeft.get_height()), GuiElement.toolTipMiddleLeft);
			GUI.DrawTexture(new Rect(_TooltipTexturePos.get_x() + (float)GuiElement.toolTipMiddleLeft.get_width(), _TooltipTexturePos.get_y() + (float)GuiElement.toolTipTopLeft.get_height(), _TooltipTexturePos.get_width() - (float)GuiElement.toolTipMiddleLeft.get_width() - (float)GuiElement.toolTipMiddleRight.get_width(), _TooltipTexturePos.get_height() - (float)GuiElement.toolTipTopLeft.get_height() - (float)GuiElement.toolTipBottomLeft.get_height()), GuiElement.toolTipMiddleCenter);
			GUI.DrawTexture(new Rect(_TooltipTexturePos.get_x() + _TooltipTexturePos.get_width() - (float)GuiElement.toolTipMiddleRight.get_width(), _TooltipTexturePos.get_y() + (float)GuiElement.toolTipTopLeft.get_height(), (float)GuiElement.toolTipMiddleRight.get_width(), _TooltipTexturePos.get_height() - (float)GuiElement.toolTipTopLeft.get_height() - (float)GuiElement.toolTipBottomLeft.get_height()), GuiElement.toolTipMiddleRight);
			GUI.Label(new Rect(_TooltipTexturePos.get_x() + 10f, _TooltipTexturePos.get_y(), _TooltipTexturePos.get_width() - 20f, _TooltipTexturePos.get_height()), this.tooltipText, this.tooltipStyle);
		}
	}

	private void LoadtToolTipTexture()
	{
		GuiElement.toolTipTopLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "gui_tooltip_top_left");
		GuiElement.toolTipTopRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "gui_tooltip_top_right");
		GuiElement.toolTipTopCenter = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "gui_tooltip_top_middle");
		GuiElement.toolTipBottomLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "gui_tooltip_bottom_left");
		GuiElement.toolTipBottomCenter = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "gui_tooltip_bottom_middle");
		GuiElement.toolTipBottomRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "gui_tooltip_bottom_right");
		GuiElement.toolTipMiddleLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "gui_tooltip_left_middle");
		GuiElement.toolTipMiddleCenter = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "gui_tooltip_background");
		GuiElement.toolTipMiddleRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "gui_tooltip_right_middle");
		this.isTextureLoaded = true;
	}

	public void showTooltipWindow()
	{
		if (!this.container.isActiveWindow)
		{
			return;
		}
		if (this.drawTooltipWindowCallback != null)
		{
			this.tooltipWindow = this.drawTooltipWindowCallback(this.tooltipWindowParam);
			if (this.tooltipWindow != null)
			{
				if (!this.isClippingBoundariesContent)
				{
					ref Rect rectPointer = ref this.tooltipWindow.boundries;
					rectPointer.set_x(rectPointer.get_x() + this.container.boundries.get_x());
					ref Rect rectPointer1 = ref this.tooltipWindow.boundries;
					rectPointer1.set_y(rectPointer1.get_y() + this.container.boundries.get_y());
				}
				else
				{
					ref Rect rectPointer2 = ref this.tooltipWindow.boundries;
					float _x = rectPointer2.get_x();
					float single = this.container.boundries.get_x();
					Rect rect = this.container.ClippingBoundaris(this.clippingBoundariesId);
					rectPointer2.set_x(_x + (single + rect.get_x()));
					ref Rect rectPointer3 = ref this.tooltipWindow.boundries;
					float _y = rectPointer3.get_y();
					float _y1 = this.container.boundries.get_y();
					Rect rect1 = this.container.ClippingBoundaris(this.clippingBoundariesId);
					rectPointer3.set_y(_y + (_y1 + rect1.get_y()));
				}
				float _width = (float)Screen.get_width() - (this.tooltipWindow.boundries.get_x() + this.tooltipWindow.boundries.get_width());
				if (_width < 0f)
				{
					ref Rect rectPointer4 = ref this.tooltipWindow.boundries;
					rectPointer4.set_x(rectPointer4.get_x() + _width);
				}
				float _height = (float)Screen.get_height() - (this.tooltipWindow.boundries.get_y() + this.tooltipWindow.boundries.get_height());
				if (_height < 0f)
				{
					ref Rect rectPointer5 = ref this.tooltipWindow.boundries;
					rectPointer5.set_y(rectPointer5.get_y() + _height);
				}
				if (this.tooltipWindow.boundries.get_x() < 0f)
				{
					this.tooltipWindow.boundries.set_x(0f);
				}
				if (this.tooltipWindow.boundries.get_y() < 0f)
				{
					this.tooltipWindow.boundries.set_y(0f);
				}
				this.tooltipWindow.zOrder = 230;
				this.tooltipWindow.ignoreClickEvents = true;
				AndromedaGui.gui.AddWindow(this.tooltipWindow);
			}
		}
	}

	public delegate GuiWindow DrawTooltipWindow(object parm);
}