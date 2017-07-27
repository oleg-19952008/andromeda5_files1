using System;
using UnityEngine;

public class GuiCheckbox : GuiElement
{
	private Texture2D checkboxNormal;

	private Texture2D checkboxHover;

	private Texture2D checkboxClicked;

	private bool isSelected;

	public Action<bool> OnCheckboxSelected;

	private bool isAlreadyClicked;

	private float timeout;

	public bool Selected
	{
		get
		{
			return this.isSelected;
		}
		set
		{
			this.isSelected = value;
		}
	}

	public GuiCheckbox.CheckboxState State
	{
		get
		{
			if (!this.IsMouseOver)
			{
				return GuiCheckbox.CheckboxState.Normal;
			}
			if (Input.GetMouseButtonDown(0) && !this.isAlreadyClicked)
			{
				this.isAlreadyClicked = true;
				return GuiCheckbox.CheckboxState.Clicked;
			}
			this.isAlreadyClicked = false;
			return GuiCheckbox.CheckboxState.Hover;
		}
	}

	public GuiCheckbox()
	{
		this.checkboxNormal = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "checkboxNml");
		this.checkboxHover = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "checkboxHvr");
		this.checkboxClicked = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "checkboxClk");
		this.boundries.set_width((float)this.checkboxNormal.get_width());
		this.boundries.set_height((float)this.checkboxNormal.get_height());
	}

	public override void DrawGuiElement()
	{
		GuiCheckbox _fixedDeltaTime = this;
		_fixedDeltaTime.timeout = _fixedDeltaTime.timeout - Time.get_fixedDeltaTime();
		if (this.timeout < 0f)
		{
			this.timeout = 0f;
		}
		switch (this.State)
		{
			case GuiCheckbox.CheckboxState.Hover:
			{
				GUI.DrawTexture(this.boundries, this.checkboxHover);
				break;
			}
			case GuiCheckbox.CheckboxState.Clicked:
			{
				if (this.isActive)
				{
					if (this.timeout == 0f)
					{
						this.isSelected = !this.isSelected;
						this.timeout = 0.1f;
					}
					if (this.OnCheckboxSelected != null)
					{
						this.OnCheckboxSelected.Invoke(this.isSelected);
					}
				}
				break;
			}
		}
		if (!this.isSelected)
		{
			GUI.DrawTexture(this.boundries, this.checkboxNormal);
		}
		else
		{
			GUI.DrawTexture(this.boundries, this.checkboxClicked);
		}
		base.DrawGuiElement();
	}

	public enum CheckboxState
	{
		Normal,
		Hover,
		Clicked
	}
}