using System;
using UnityEngine;

public class GuiHorizontalSlider : GuiElement
{
	private Texture2D _sliderLeft;

	private Texture2D _sliderFill;

	private Texture2D _sliderRight;

	private Texture2D _sliderThumb;

	public Action refreshData;

	private float maximum;

	private float minimum;

	private float val;

	private float oldVal;

	private float scale;

	private int intVal;

	public float CurrentValue
	{
		get
		{
			return this.val;
		}
		set
		{
			this.val = value;
			if ((double)(this.val % (float)((int)this.val)) < 0.5)
			{
				this.intVal = (int)this.val;
			}
			else
			{
				this.intVal = (int)this.val + 1;
			}
		}
	}

	public int CurrentValueInt
	{
		get
		{
			return this.intVal;
		}
		set
		{
			this.intVal = value;
		}
	}

	public float MAX
	{
		get
		{
			return this.maximum;
		}
		set
		{
			this.maximum = value;
			this.CalculateScale();
		}
	}

	public float MIN
	{
		get
		{
			return this.minimum;
		}
		set
		{
			this.minimum = value;
			this.CalculateScale();
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
			this.CalculateScale();
		}
	}

	public GuiHorizontalSlider()
	{
		this._sliderLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "slider_left");
		this._sliderFill = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "slider_middle");
		this._sliderRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "slider_right");
		this._sliderThumb = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "slider_thumb");
		this.boundries.set_height((float)this._sliderThumb.get_height());
		this.boundries.set_width(200f);
		this.minimum = 0f;
		this.maximum = 100f;
		float single = this.minimum;
		float single1 = single;
		this.oldVal = single;
		this.val = single1;
		this.intVal = (int)this.minimum;
		this.CalculateScale();
	}

	public GuiHorizontalSlider(float minVal, float maxVal, float currentVal)
	{
		this._sliderLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "slider_left");
		this._sliderFill = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "slider_middle");
		this._sliderRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "slider_right");
		this._sliderThumb = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "slider_thumb");
		this.boundries.set_height((float)this._sliderThumb.get_height());
		this.boundries.set_width(200f);
		this.minimum = minVal;
		this.maximum = maxVal;
		float single = currentVal;
		float single1 = single;
		this.oldVal = single;
		this.val = single1;
		this.intVal = (int)currentVal;
		this.CalculateScale();
	}

	private void CalculateScale()
	{
		this.scale = this.boundries.get_width() / (this.maximum - this.minimum);
	}

	public override void DrawGuiElement()
	{
		if (this.container.isLockedByScroll == null || this.container.isLockedByScroll == this)
		{
			float _mousePosition = Event.get_current().get_mousePosition().x;
			Vector2 vector2 = Event.get_current().get_mousePosition();
			Vector2 vector21 = new Vector2(_mousePosition, vector2.y);
			if (this.MousePositionCheck(vector21) && Input.GetMouseButton(0))
			{
				this.val = this.minimum + (vector21.x - this.boundries.get_x()) / this.scale;
				if ((double)(this.val % (float)((int)this.val)) < 0.5)
				{
					this.intVal = (int)this.val;
				}
				else
				{
					this.intVal = (int)this.val + 1;
				}
			}
			if (this.isActive && !Input.GetMouseButton(0))
			{
				this.isActive = false;
				this.container.isLockedByScroll = null;
			}
			if (this.oldVal != this.val)
			{
				this.refreshData.Invoke();
				this.oldVal = this.val;
			}
		}
		float single = (this.val - this.minimum) * this.scale;
		GUI.DrawTexture(new Rect(this.boundries.get_x(), this.boundries.get_y() + (this.boundries.get_height() - (float)this._sliderLeft.get_height()) / 2f, (float)this._sliderLeft.get_width(), (float)this._sliderLeft.get_height()), this._sliderLeft);
		GUI.DrawTexture(new Rect(this.boundries.get_x() + (float)this._sliderLeft.get_width(), this.boundries.get_y() + (this.boundries.get_height() - (float)this._sliderLeft.get_height()) / 2f, this.boundries.get_width() - (float)this._sliderLeft.get_width() - (float)this._sliderRight.get_width(), (float)this._sliderFill.get_height()), this._sliderFill);
		GUI.DrawTexture(new Rect(this.boundries.get_x() + this.boundries.get_width() - (float)this._sliderRight.get_width(), this.boundries.get_y() + (this.boundries.get_height() - (float)this._sliderLeft.get_height()) / 2f, (float)this._sliderRight.get_width(), (float)this._sliderRight.get_height()), this._sliderRight);
		GUI.DrawTexture(new Rect(this.boundries.get_x() + single - (float)(this._sliderThumb.get_width() / 2), this.boundries.get_y(), (float)this._sliderThumb.get_width(), (float)this._sliderThumb.get_height()), this._sliderThumb);
	}

	private bool MousePositionCheck(Vector2 pos)
	{
		if (pos.x < this.boundries.get_x() || pos.x > this.boundries.get_x() + this.boundries.get_width() || !this.isActive && (pos.y < this.boundries.get_y() || pos.y > this.boundries.get_y() + this.boundries.get_height()))
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

	public void SetEmptySliderTexture()
	{
		this._sliderLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "_slider_empty");
		this._sliderFill = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "_slider_empty");
		this._sliderRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "_slider_empty");
	}

	public void SetSliderTumb(string bundelName, string assetName)
	{
		this._sliderThumb = (Texture2D)playWebGame.assets.GetFromStaticSet(bundelName, assetName);
		this.boundries.set_height((float)this._sliderThumb.get_height());
	}
}