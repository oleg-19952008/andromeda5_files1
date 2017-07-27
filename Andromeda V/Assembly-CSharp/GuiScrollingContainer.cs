using System;
using System.Collections.Generic;
using UnityEngine;

public class GuiScrollingContainer : GuiElement
{
	private List<GuiElement> elements;

	private List<float> elementsY;

	private GuiWindow wnd;

	private GuiButtonFixed btnUp;

	private GuiButtonFixed btnDown;

	private GuiTexture txSlider_up;

	private GuiTexture txSlider_middle;

	private GuiTexture txSlider_down;

	private GuiTexture txSliderBg;

	private float offsetX;

	private float offsetY;

	private float conteinerX;

	private float conteinerY;

	private float conteinerWidth;

	private float conteinerHeight;

	private float scaleIndex = 1f;

	private bool isNeedScroller;

	private bool isScrolerLoaded;

	public Rect scrollerTumbRect;

	private float invisibePartHeight;

	private float arrowClickStep = 20f;

	public byte scrollerId;

	private float startPos;

	private float ContentHeight
	{
		get
		{
			float item = 0f;
			for (int i = 0; i < this.elements.get_Count(); i++)
			{
				if (this.elementsY.get_Item(i) + this.elements.get_Item(i).boundries.get_height() > item)
				{
					item = this.elementsY.get_Item(i) + this.elements.get_Item(i).boundries.get_height();
				}
			}
			return item;
		}
	}

	public Vector2 InnerMousePosition
	{
		get
		{
			float _height = (float)Screen.get_height() - Input.get_mousePosition().y;
			Vector3 _mousePosition = Input.get_mousePosition();
			Vector2 vector2 = new Vector2(_mousePosition.x - this.wnd.boundries.get_x() - this.conteinerX, _height - this.InvisibleTopPart - this.wnd.boundries.get_y() - this.conteinerY);
			return vector2;
		}
	}

	private float InvisibleTopPart
	{
		get
		{
			float _y = 0f;
			if (this.elements.get_Count() > 0)
			{
				_y = this.elements.get_Item(0).boundries.get_y() - this.elementsY.get_Item(0);
			}
			return _y;
		}
	}

	public bool IsScrolerAvalable
	{
		get
		{
			return this.isScrolerLoaded;
		}
	}

	public float ScrollTumbCenter
	{
		get
		{
			return this.scrollerTumbRect.get_height() / 2f + this.scrollerTumbRect.get_y();
		}
	}

	private float SliderHeight
	{
		get
		{
			this.scaleIndex = this.conteinerHeight / this.ContentHeight;
			return this.scaleIndex * this.txSliderBg.boundries.get_height();
		}
	}

	public GuiScrollingContainer(float x, float y, float width, float height, byte id, GuiWindow window)
	{
		this.conteinerX = x;
		this.conteinerY = y;
		this.conteinerWidth = width;
		this.conteinerHeight = height;
		this.wnd = window;
		this.scrollerId = id;
		this.wnd.SetClippingBoundaries(this.scrollerId, new Rect(this.conteinerX, this.conteinerY, this.conteinerWidth, this.conteinerHeight));
		this.elements = new List<GuiElement>();
		this.elementsY = new List<float>();
	}

	public void AddContent(GuiElement ge)
	{
		ge.isClippingBoundariesContent = true;
		ge.clippingBoundariesId = this.scrollerId;
		this.elements.Add(ge);
		this.elementsY.Add(ge.Y);
		this.wnd.AddClippedGuiElement(ge);
		this.PopulateScroller();
	}

	public bool CheckIfThumbIsDown()
	{
		if (this.IsScrolerAvalable && this.scrollerTumbRect.get_y() + this.scrollerTumbRect.get_height() < this.conteinerHeight + 10f)
		{
			return false;
		}
		return true;
	}

	public void Claer()
	{
		foreach (GuiElement element in this.elements)
		{
			this.wnd.RemoveGuiElement(element);
		}
		this.elements.Clear();
		this.elementsY.Clear();
		if (this.btnUp != null)
		{
			this.wnd.RemoveGuiElement(this.btnUp);
			this.btnUp = null;
		}
		if (this.btnDown != null)
		{
			this.wnd.RemoveGuiElement(this.btnDown);
			this.btnDown = null;
		}
		if (this.txSliderBg != null)
		{
			this.wnd.RemoveGuiElement(this.txSliderBg);
			this.txSliderBg = null;
		}
		if (this.txSlider_up != null)
		{
			this.wnd.RemoveGuiElement(this.txSlider_up);
			this.txSlider_up = null;
		}
		if (this.txSlider_middle != null)
		{
			this.wnd.RemoveGuiElement(this.txSlider_middle);
			this.txSlider_middle = null;
		}
		if (this.txSlider_down != null)
		{
			this.wnd.RemoveGuiElement(this.txSlider_down);
			this.txSlider_down = null;
		}
		this.isScrolerLoaded = false;
		this.PopulateScroller();
	}

	public override void DrawGuiElement()
	{
		if (!this.isNeedScroller)
		{
			return;
		}
		if (this.container.isLockedByScroll == null || this.container.isLockedByScroll == this)
		{
			float _mousePosition = Event.get_current().get_mousePosition().x;
			Vector2 vector2 = Event.get_current().get_mousePosition();
			Vector2 vector21 = new Vector2(_mousePosition, vector2.y);
			if (this.MousePositionOverTumb(vector21))
			{
				if (Input.GetMouseButtonDown(0))
				{
					this.startPos = this.scrollerTumbRect.get_y() - vector21.y;
				}
				if (Input.GetMouseButton(0))
				{
					this.MooveTumb(this.startPos, vector21.y);
				}
			}
			else if (this.MousePositionOverScoller(vector21) && Input.GetMouseButton(0))
			{
				this.MooveToCenter(vector21.y);
			}
			if (this.isActive && !Input.GetMouseButton(0))
			{
				this.isActive = false;
				this.container.isLockedByScroll = null;
			}
		}
	}

	private void InitScroller()
	{
		this.btnUp = new GuiButtonFixed()
		{
			Caption = string.Empty
		};
		this.btnUp.SetTexture("FrameworkGUI", "BtnQuestArrowUp");
		this.btnUp.X = this.conteinerX + this.conteinerWidth - this.btnUp.boundries.get_width();
		this.btnUp.Y = this.conteinerY;
		this.btnUp.Clicked = new Action<EventHandlerParam>(this, GuiScrollingContainer.OnUpBtnClicked);
		this.wnd.AddGuiElement(this.btnUp);
		this.btnDown = new GuiButtonFixed()
		{
			Caption = string.Empty
		};
		this.btnDown.SetTexture("FrameworkGUI", "BtnQuestArrowDown");
		this.btnDown.X = this.conteinerX + this.conteinerWidth - this.btnUp.boundries.get_width();
		this.btnDown.Y = this.conteinerY + this.conteinerHeight - this.btnDown.boundries.get_height();
		this.btnDown.Clicked = new Action<EventHandlerParam>(this, GuiScrollingContainer.OnDownBtnClicked);
		this.wnd.AddGuiElement(this.btnDown);
		this.txSliderBg = new GuiTexture()
		{
			boundries = new Rect(this.conteinerX + this.conteinerWidth - this.btnUp.boundries.get_width(), this.conteinerY + this.btnUp.boundries.get_height(), this.btnDown.boundries.get_width(), this.conteinerHeight - this.btnUp.boundries.get_height() - this.btnDown.boundries.get_height())
		};
		this.txSliderBg.SetTextureKeepSize("FrameworkGUI", "scrollerBg");
		this.wnd.AddGuiElement(this.txSliderBg);
		this.scrollerTumbRect = this.txSliderBg.boundries;
		this.txSlider_up = new GuiTexture();
		this.txSlider_up.SetTexture("FrameworkGUI", "scrollerTumb_up");
		this.txSlider_up.X = this.txSliderBg.X;
		this.txSlider_up.Y = this.txSliderBg.Y;
		this.wnd.AddGuiElement(this.txSlider_up);
		this.txSlider_middle = new GuiTexture();
		this.txSlider_middle.SetTexture("FrameworkGUI", "scrollerTumb_middle");
		this.txSlider_middle.X = this.txSliderBg.X;
		this.txSlider_middle.Y = this.txSliderBg.Y + this.txSlider_up.boundries.get_height();
		this.wnd.AddGuiElement(this.txSlider_middle);
		this.txSlider_down = new GuiTexture();
		this.txSlider_down.SetTexture("FrameworkGUI", "scrollerTumb_down");
		this.txSlider_down.X = this.txSliderBg.X;
		this.txSlider_down.Y = this.txSliderBg.Y + this.txSlider_up.boundries.get_height() + this.txSlider_middle.boundries.get_height();
		this.wnd.AddGuiElement(this.txSlider_down);
		this.isScrolerLoaded = true;
	}

	public void MooveToCenter(float mousePossitionY)
	{
		float single = mousePossitionY - (this.scrollerTumbRect.get_y() + this.scrollerTumbRect.get_height() / 2f);
		this.MooveTumb(this.scrollerTumbRect.get_y(), single);
	}

	public void MooveTumb(float firstPoint, float dy)
	{
		float y = dy + firstPoint;
		if (y < this.txSliderBg.Y)
		{
			y = this.txSliderBg.Y;
		}
		if (y > this.txSliderBg.Y + this.txSliderBg.boundries.get_height() - this.scrollerTumbRect.get_height())
		{
			y = this.txSliderBg.Y + this.txSliderBg.boundries.get_height() - this.scrollerTumbRect.get_height();
		}
		this.txSlider_up.Y = y;
		this.txSlider_middle.Y = y + this.txSlider_up.boundries.get_height();
		this.txSlider_down.Y = y + this.txSlider_up.boundries.get_height() + this.txSlider_middle.boundries.get_height();
		this.scrollerTumbRect.set_y(y);
		this.MoveContent(y - this.txSliderBg.Y);
	}

	private bool MousePositionOverScoller(Vector2 pos)
	{
		if (pos.x >= this.txSliderBg.boundries.get_x() && pos.x <= this.txSliderBg.boundries.get_x() + this.txSliderBg.boundries.get_width() && pos.y >= this.txSliderBg.boundries.get_y() && pos.y <= this.txSliderBg.boundries.get_y() + this.txSliderBg.boundries.get_height())
		{
			return true;
		}
		return false;
	}

	private bool MousePositionOverTumb(Vector2 pos)
	{
		if (pos.y < this.scrollerTumbRect.get_y() || pos.y > this.scrollerTumbRect.get_y() + this.scrollerTumbRect.get_height() || !this.isActive && (pos.x < this.scrollerTumbRect.get_x() || pos.x > this.scrollerTumbRect.get_x() + this.scrollerTumbRect.get_width()))
		{
			return false;
		}
		if (Input.GetMouseButton(0))
		{
			this.isActive = true;
			this.container.isLockedByScroll = this;
		}
		return true;
	}

	private void MoveContent(float dy)
	{
		float single = dy / (this.txSliderBg.boundries.get_height() - this.scrollerTumbRect.get_height()) * this.invisibePartHeight;
		for (int i = 0; i < this.elements.get_Count(); i++)
		{
			this.elements.get_Item(i).Y = this.elementsY.get_Item(i) - single;
		}
	}

	public void MoveToEndOfContainer()
	{
		if (this.IsScrolerAvalable)
		{
			this.MooveTumb(this.scrollerTumbRect.get_y(), this.conteinerHeight - this.scrollerTumbRect.get_height());
		}
	}

	public void MultipleClickDownArrow(int i)
	{
		if (!this.isNeedScroller)
		{
			return;
		}
		for (int num = 0; num < i; num++)
		{
			this.OnDownBtnClicked(null);
		}
	}

	private void OnDownBtnClicked(object prm)
	{
		float single = this.arrowClickStep / this.invisibePartHeight;
		float _height = (this.txSliderBg.boundries.get_height() - this.scrollerTumbRect.get_height()) * single;
		this.MooveTumb(this.scrollerTumbRect.get_y(), _height);
	}

	private void OnUpBtnClicked(object prm)
	{
		float single = -this.arrowClickStep / this.invisibePartHeight;
		float _height = (this.txSliderBg.boundries.get_height() - this.scrollerTumbRect.get_height()) * single;
		this.MooveTumb(this.scrollerTumbRect.get_y(), _height);
	}

	private void PopulateScroller()
	{
		this.invisibePartHeight = this.ContentHeight - this.conteinerHeight;
		this.isNeedScroller = this.invisibePartHeight > 0f;
		if (this.isNeedScroller)
		{
			if (!this.isScrolerLoaded)
			{
				this.InitScroller();
			}
			this.PopulateScrollTumb();
			this.wnd.ScrollingContainer(this.scrollerId).onUpdateCall = new Action(this, GuiScrollingContainer.ProvideMouseScrolling);
		}
		else if (this.isScrolerLoaded)
		{
			Debug.LogWarning("Distribute code for scroller remove");
		}
	}

	private void PopulateScrollTumb()
	{
		this.scrollerTumbRect.set_height(Math.Max(this.SliderHeight, this.txSlider_down.boundries.get_height() + this.txSlider_up.boundries.get_height()));
		this.txSlider_middle.boundries.set_height(Math.Max(this.scrollerTumbRect.get_height() - this.txSlider_down.boundries.get_height() - this.txSlider_up.boundries.get_height(), 0f));
		this.txSlider_middle.SetTextureKeepSize("FrameworkGUI", "scrollerTumb_middle");
		this.txSlider_down.Y = this.txSlider_up.Y + this.txSlider_up.boundries.get_height() + this.txSlider_middle.boundries.get_height();
	}

	private void ProvideMouseScrolling()
	{
		if (!this.IsScrolerAvalable)
		{
			return;
		}
		Vector3 _mousePosition = Input.get_mousePosition();
		float _height = (float)Screen.get_height() - _mousePosition.y;
		float single = _mousePosition.x;
		Rect rect = new Rect(this.wnd.boundries.get_x() + this.conteinerX, this.wnd.boundries.get_y() + this.conteinerY, this.conteinerWidth, this.conteinerHeight);
		bool flag = rect.Contains(new Vector2(single, _height));
		float axis = Input.GetAxis("Mouse ScrollWheel");
		if (flag && axis != 0f)
		{
			if (axis <= 0f)
			{
				this.OnDownBtnClicked(null);
				axis = 0f;
			}
			else
			{
				this.OnUpBtnClicked(null);
				axis = 0f;
			}
		}
	}

	public void RemoveContent(GuiElement ge)
	{
		if (!this.elements.Contains(ge))
		{
			return;
		}
		int num = this.elements.IndexOf(ge);
		ge.isClippingBoundariesContent = true;
		ge.clippingBoundariesId = this.scrollerId;
		this.elements.Remove(ge);
		this.elementsY.RemoveAt(num);
		this.wnd.RemoveGuiElement(ge);
		this.PopulateScroller();
	}

	public void SetArrowStep(float x)
	{
		this.arrowClickStep = x;
	}

	public void SpecialAddContent(GuiElement ge)
	{
		float item = 0f;
		if (this.elements.get_Item(0) != null)
		{
			item = this.elementsY.get_Item(0) - this.elements.get_Item(0).Y;
		}
		ge.isClippingBoundariesContent = true;
		ge.clippingBoundariesId = this.scrollerId;
		this.elementsY.Add(ge.Y);
		GuiElement y = ge;
		y.Y = y.Y - item;
		this.elements.Add(ge);
		this.wnd.AddClippedGuiElement(ge);
		this.PopulateScroller();
	}
}