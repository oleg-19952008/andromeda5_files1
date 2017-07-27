using System;
using System.Collections.Generic;
using UnityEngine;

public class GuiDropdown : GuiElement
{
	public List<string> items = new List<string>();

	public int selectedItem;

	private Texture2D bodyNormalLeft;

	private Texture2D bodyNormalMiddle;

	private Texture2D bodyNormalRight;

	private Texture2D bodyHoverLeft;

	private Texture2D bodyHoverMiddle;

	private Texture2D bodyHoverRight;

	private Texture2D listNormalMiddle;

	private Texture2D listHoverMiddle;

	private Texture2D listBorder;

	protected GUIStyle style = new GUIStyle();

	private bool isOpen;

	public bool DisableDrawBackwards;

	public Action<int> OnItemSelected;

	public bool IsOpen
	{
		get
		{
			return this.isOpen;
		}
		set
		{
			this.isOpen = value;
		}
	}

	public GuiDropdown.DropdownState State
	{
		get
		{
			if (!this.IsMouseOver)
			{
				return GuiDropdown.DropdownState.Normal;
			}
			if (Input.GetMouseButtonDown(0))
			{
				return GuiDropdown.DropdownState.Clicked;
			}
			return GuiDropdown.DropdownState.Hover;
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

	public GuiDropdown()
	{
		this.isHoverAware = true;
		this.bodyNormalLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "ddl_BodyNmlLeft");
		this.bodyNormalRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "ddl_BodyNmlRight");
		this.bodyNormalMiddle = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "ddl_BodyNmlMiddle");
		this.bodyHoverLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "ddl_BodyHvrLeft");
		this.bodyHoverMiddle = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "ddl_BodyHvrMiddle");
		this.bodyHoverRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "ddl_BodyHvrRight");
		this.listNormalMiddle = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "ddl_listNormalMiddle");
		this.listHoverMiddle = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "ddl_listHoverMiddle");
		this.listBorder = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "ddl_listBorder");
		this.boundries.set_width(120f);
		this.boundries.set_height((float)this.bodyNormalLeft.get_height());
		this.style.set_fontSize(12);
		this.style.set_font(GuiLabel.FontBold);
		this.style.get_normal().set_textColor(Color.get_white());
		this.style.set_clipping(1);
		this.style.set_wordWrap(true);
	}

	public void AddItem(string item)
	{
		this.items.Add(item);
	}

	public void AddItem(string item, bool isSelected)
	{
		this.items.Add(item);
		if (isSelected)
		{
			this.selectedItem = this.items.get_Count() - 1;
		}
	}

	public override void DrawGuiElement()
	{
		switch (this.State)
		{
			case GuiDropdown.DropdownState.Normal:
			{
				GUI.DrawTexture(new Rect(this.boundries.get_x(), this.boundries.get_y(), (float)this.bodyNormalLeft.get_width(), (float)this.bodyNormalLeft.get_height()), this.bodyNormalLeft);
				GUI.DrawTexture(new Rect(this.boundries.get_x() + (float)this.bodyNormalLeft.get_width(), this.boundries.get_y(), this.boundries.get_width() - (float)this.bodyNormalLeft.get_width() - (float)this.bodyNormalRight.get_width(), (float)this.bodyNormalMiddle.get_height()), this.bodyNormalMiddle);
				GUI.DrawTexture(new Rect(this.boundries.get_x() + this.boundries.get_width() - (float)this.bodyNormalRight.get_width(), this.boundries.get_y(), (float)this.bodyNormalRight.get_width(), (float)this.bodyNormalRight.get_height()), this.bodyNormalRight);
				break;
			}
			case GuiDropdown.DropdownState.Hover:
			{
				GUI.DrawTexture(new Rect(this.boundries.get_x(), this.boundries.get_y(), (float)this.bodyHoverLeft.get_width(), (float)this.bodyHoverLeft.get_height()), this.bodyHoverLeft);
				GUI.DrawTexture(new Rect(this.boundries.get_x() + (float)this.bodyHoverLeft.get_width(), this.boundries.get_y(), this.boundries.get_width() - (float)this.bodyHoverLeft.get_width() - (float)this.bodyHoverRight.get_width(), (float)this.bodyHoverMiddle.get_height()), this.bodyHoverMiddle);
				GUI.DrawTexture(new Rect(this.boundries.get_x() + this.boundries.get_width() - (float)this.bodyHoverRight.get_width(), this.boundries.get_y(), (float)this.bodyHoverRight.get_width(), (float)this.bodyHoverRight.get_height()), this.bodyHoverRight);
				break;
			}
			case GuiDropdown.DropdownState.Clicked:
			{
				GUI.DrawTexture(new Rect(this.boundries.get_x(), this.boundries.get_y(), (float)this.bodyNormalLeft.get_width(), (float)this.bodyNormalLeft.get_height()), this.bodyNormalLeft);
				GUI.DrawTexture(new Rect(this.boundries.get_x() + (float)this.bodyNormalLeft.get_width(), this.boundries.get_y(), this.boundries.get_width() - (float)this.bodyNormalLeft.get_width() - (float)this.bodyNormalRight.get_width(), (float)this.bodyNormalMiddle.get_height()), this.bodyNormalMiddle);
				GUI.DrawTexture(new Rect(this.boundries.get_x() + this.boundries.get_width() - (float)this.bodyNormalRight.get_width(), this.boundries.get_y(), (float)this.bodyNormalRight.get_width(), (float)this.bodyNormalRight.get_height()), this.bodyHoverRight);
				break;
			}
		}
		if (this.selectedItem < this.items.get_Count())
		{
			Rect rect = new Rect(this.boundries);
			rect.set_x(rect.get_x() + 6f);
			rect.set_y(rect.get_y() + 5f);
			if (this.items.get_Item(this.selectedItem).get_Length() <= 22)
			{
				GUI.Label(rect, this.items.get_Item(this.selectedItem), this.style);
			}
			else
			{
				GUI.Label(rect, string.Concat(this.items.get_Item(this.selectedItem).Substring(0, 21), " ..."), this.style);
			}
		}
		if (this.isOpen)
		{
			float _y = this.boundries.get_y() + base.ContainerRect.get_y();
			if (this.isClippingBoundariesContent)
			{
				_y = _y + this.container.ScrollingContainer(this.clippingBoundariesId).boundaris.get_y();
			}
			if (_y <= (float)(Screen.get_height() / 2) || this.DisableDrawBackwards)
			{
				float single = this.boundries.get_y() + this.boundries.get_height();
				Vector3 _mousePosition = Input.get_mousePosition();
				float _height = (float)Screen.get_height() - _mousePosition.y;
				float single1 = _mousePosition.x;
				int num = 0;
				foreach (string item in this.items)
				{
					float single2 = 22f;
					if (item.get_Length() > 18)
					{
						single2 = 36f;
					}
					Rect containerRect = base.ContainerRect;
					float _x = containerRect.get_x() + this.boundries.get_x();
					Rect containerRect1 = base.ContainerRect;
					Rect rect1 = new Rect(_x, containerRect1.get_y() + single, this.boundries.get_width(), single2);
					if (this.isClippingBoundariesContent)
					{
						Rect rect2 = this.container.ScrollingContainer(this.clippingBoundariesId).boundaris;
						rect1.set_x(rect1.get_x() + rect2.get_x());
						rect1.set_y(rect1.get_y() + rect2.get_y());
					}
					Rect rect3 = new Rect(this.boundries.get_x(), single, this.boundries.get_width() - 20f, single2);
					if (rect1.Contains(new Vector2(single1, _height)))
					{
						GUI.DrawTexture(new Rect(rect3.get_x(), rect3.get_y(), 1f, rect3.get_height()), this.listBorder);
						GUI.DrawTexture(new Rect(rect3.get_x() + 1f, rect3.get_y(), rect3.get_width() - 2f, rect3.get_height()), this.listHoverMiddle);
						GUI.DrawTexture(new Rect(rect3.get_x() + rect3.get_width() - 1f, rect3.get_y(), 1f, rect3.get_height()), this.listBorder);
						if (Input.GetMouseButtonDown(0))
						{
							this.selectedItem = num;
							if (this.OnItemSelected != null)
							{
								this.OnItemSelected.Invoke(this.selectedItem);
							}
							this.isOpen = false;
						}
					}
					else
					{
						GUI.DrawTexture(new Rect(rect3.get_x(), rect3.get_y(), 1f, rect3.get_height()), this.listBorder);
						GUI.DrawTexture(new Rect(rect3.get_x() + 1f, rect3.get_y(), rect3.get_width() - 2f, rect3.get_height()), this.listNormalMiddle);
						GUI.DrawTexture(new Rect(rect3.get_x() + rect3.get_width() - 1f, rect3.get_y(), 1f, rect3.get_height()), this.listBorder);
					}
					GUI.Label(new Rect(this.boundries.get_x() + 10f, single + 4f, rect3.get_width() - 15f, rect3.get_height()), item, this.style);
					single = single + single2;
					num++;
				}
				GUI.DrawTexture(new Rect(this.boundries.get_x(), single, this.boundries.get_width() - 20f, 1f), this.listBorder);
			}
			else
			{
				float _y1 = this.boundries.get_y();
				Vector3 vector3 = Input.get_mousePosition();
				float _height1 = (float)Screen.get_height() - vector3.y;
				float single3 = vector3.x;
				int num1 = 0;
				foreach (string str in this.items)
				{
					float single4 = 22f;
					if (str.get_Length() > 18)
					{
						single4 = 36f;
					}
					_y1 = _y1 - single4;
					Rect containerRect2 = base.ContainerRect;
					float _x1 = containerRect2.get_x() + this.boundries.get_x();
					Rect containerRect3 = base.ContainerRect;
					Rect rect4 = new Rect(_x1, containerRect3.get_y() + _y1, this.boundries.get_width(), single4);
					if (this.isClippingBoundariesContent)
					{
						Rect rect5 = this.container.ScrollingContainer(this.clippingBoundariesId).boundaris;
						rect4.set_x(rect4.get_x() + rect5.get_x());
						rect4.set_y(rect4.get_y() + rect5.get_y());
					}
					Rect rect6 = new Rect(this.boundries.get_x(), _y1, this.boundries.get_width() - 20f, single4);
					if (rect4.Contains(new Vector2(single3, _height1)))
					{
						GUI.DrawTexture(new Rect(rect6.get_x(), rect6.get_y(), 1f, rect6.get_height()), this.listBorder);
						GUI.DrawTexture(new Rect(rect6.get_x() + 1f, rect6.get_y(), rect6.get_width() - 2f, rect6.get_height()), this.listHoverMiddle);
						GUI.DrawTexture(new Rect(rect6.get_x() + rect6.get_width() - 1f, rect6.get_y(), 1f, rect6.get_height()), this.listBorder);
						if (Input.GetMouseButtonDown(0))
						{
							this.selectedItem = num1;
							if (this.OnItemSelected != null)
							{
								this.OnItemSelected.Invoke(this.selectedItem);
							}
							this.isOpen = false;
						}
					}
					else
					{
						GUI.DrawTexture(new Rect(rect6.get_x(), rect6.get_y(), 1f, rect6.get_height()), this.listBorder);
						GUI.DrawTexture(new Rect(rect6.get_x() + 1f, rect6.get_y(), rect6.get_width() - 2f, rect6.get_height()), this.listNormalMiddle);
						GUI.DrawTexture(new Rect(rect6.get_x() + rect6.get_width() - 1f, rect6.get_y(), 1f, rect6.get_height()), this.listBorder);
					}
					GUI.Label(new Rect(this.boundries.get_x() + 10f, _y1 + 4f, rect6.get_width() - 15f, rect6.get_height()), str, this.style);
					num1++;
				}
				GUI.DrawTexture(new Rect(this.boundries.get_x(), _y1, this.boundries.get_width() - 20f, 1f), this.listBorder);
			}
		}
		base.DrawGuiElement();
	}

	public void OnClick()
	{
		this.isOpen = !this.isOpen;
	}

	public enum DropdownState
	{
		Normal,
		Hover,
		Clicked
	}
}