using System;
using UnityEngine;

public class GuiButtonResizeable : GuiButton
{
	public Texture2D trNormalLeft;

	public Texture2D trNormalRight;

	public Texture2D trNormalMdl;

	public Texture2D trHoverLeft;

	public Texture2D trHoverRight;

	public Texture2D trHoverMdl;

	public Texture2D trClickedLeft;

	public Texture2D trClickedRight;

	public Texture2D trClickedMdl;

	public Texture2D trDisabledLeft;

	public Texture2D trDisabledRight;

	public Texture2D trDisabledMdl;

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

	protected virtual float WidthMiddle
	{
		get
		{
			return this.boundries.get_width() - (float)this.trNormalLeft.get_width() - (float)this.trNormalRight.get_width();
		}
	}

	public GuiButtonResizeable()
	{
		this.boundries = new Rect(0f, 0f, 100f, 30f);
		if (this.trNormalLeft == null)
		{
			this.SetOrangeTexture();
		}
	}

	public override void DrawGuiElement()
	{
		this._state = base.State;
		switch (this._state)
		{
			case GuiButton.BtnState.Normal:
			{
				GUI.DrawTexture(new Rect(this.boundries.get_x(), this.boundries.get_y(), (float)this.trNormalLeft.get_width(), (float)this.trNormalLeft.get_height()), this.trNormalLeft);
				GUI.DrawTexture(new Rect(this.boundries.get_x() + (float)this.trNormalLeft.get_width(), this.boundries.get_y(), this.boundries.get_width() - (float)this.trNormalLeft.get_width() - (float)this.trNormalRight.get_width(), (float)this.trNormalMdl.get_height()), this.trNormalMdl);
				GUI.DrawTexture(new Rect(this.boundries.get_x() + this.boundries.get_width() - (float)this.trNormalRight.get_width(), this.boundries.get_y(), (float)this.trNormalRight.get_width(), (float)this.trNormalRight.get_height()), this.trNormalRight);
				break;
			}
			case GuiButton.BtnState.Hover:
			{
				GUI.DrawTexture(new Rect(this.boundries.get_x(), this.boundries.get_y(), (float)this.trNormalLeft.get_width(), (float)this.trNormalLeft.get_height()), this.trHoverLeft);
				GUI.DrawTexture(new Rect(this.boundries.get_x() + (float)this.trNormalLeft.get_width(), this.boundries.get_y(), this.boundries.get_width() - (float)this.trNormalLeft.get_width() - (float)this.trNormalRight.get_width(), (float)this.trHoverMdl.get_height()), this.trHoverMdl);
				GUI.DrawTexture(new Rect(this.boundries.get_x() + this.boundries.get_width() - (float)this.trNormalRight.get_width(), this.boundries.get_y(), (float)this.trNormalRight.get_width(), (float)this.trNormalRight.get_height()), this.trHoverRight);
				break;
			}
			case GuiButton.BtnState.LeftClicked:
			case GuiButton.BtnState.RightClicked:
			{
				GUI.DrawTexture(new Rect(this.boundries.get_x(), this.boundries.get_y(), (float)this.trNormalLeft.get_width(), (float)this.trNormalLeft.get_height()), this.trClickedLeft);
				GUI.DrawTexture(new Rect(this.boundries.get_x() + (float)this.trNormalLeft.get_width(), this.boundries.get_y(), this.boundries.get_width() - (float)this.trNormalLeft.get_width() - (float)this.trNormalRight.get_width(), (float)this.trClickedMdl.get_height()), this.trClickedMdl);
				GUI.DrawTexture(new Rect(this.boundries.get_x() + this.boundries.get_width() - (float)this.trNormalRight.get_width(), this.boundries.get_y(), (float)this.trNormalRight.get_width(), (float)this.trNormalRight.get_height()), this.trClickedRight);
				break;
			}
			case GuiButton.BtnState.Disabled:
			{
				GUI.DrawTexture(new Rect(this.boundries.get_x(), this.boundries.get_y(), (float)this.trNormalLeft.get_width(), (float)this.trNormalLeft.get_height()), this.trDisabledLeft);
				GUI.DrawTexture(new Rect(this.boundries.get_x() + (float)this.trNormalLeft.get_width(), this.boundries.get_y(), this.boundries.get_width() - (float)this.trNormalLeft.get_width() - (float)this.trNormalRight.get_width(), (float)this.trDisabledMdl.get_height()), this.trDisabledMdl);
				GUI.DrawTexture(new Rect(this.boundries.get_x() + this.boundries.get_width() - (float)this.trNormalRight.get_width(), this.boundries.get_y(), (float)this.trNormalRight.get_width(), (float)this.trNormalRight.get_height()), this.trDisabledRight);
				break;
			}
			default:
			{
				goto case GuiButton.BtnState.Normal;
			}
		}
		base.DrawGuiElement();
	}

	public void SetBlueTexture()
	{
		this.trNormalLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "btnBlueLeftNormal");
		this.trHoverLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "btnBlueLeftHover");
		this.trClickedLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "btnBlueLeftNormal");
		this.trDisabledLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "btnBlueLeftDisable");
		this.trNormalRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "btnBlueRightNormal");
		this.trHoverRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "btnBlueRightHover");
		this.trClickedRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "btnBlueRightNormal");
		this.trDisabledRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "btnBlueRightDisable");
		this.trNormalMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "btnBlueMiddleNormal");
		this.trHoverMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "btnBlueMiddleHover");
		this.trClickedMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "btnBlueMiddleNormal");
		this.trDisabledMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "btnBlueMiddleDisable");
		this.boundries.set_height((float)this.trNormalLeft.get_height());
	}

	public void SetDiscardBtn()
	{
		this.SetTexture("PoiScreenWindow", "btnDiscard_");
		this._marginLeft = 32;
		base.SetColor(GuiNewStyleBar.blueButtonsColor);
		base.SetRegularFont();
	}

	public void SetEqBtn()
	{
		this.SetTexture("PoiScreenWindow", "btnEq_");
		this._marginLeft = 32;
		base.SetColor(GuiNewStyleBar.eqBtnColor);
		base.SetRegularFont();
	}

	public void SetGetCommendationTexture()
	{
		this.trNormalLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "btnGetCommenddationNml");
		this.trHoverLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "btnGetCommenddationHvr");
		this.trClickedLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "btnGetCommenddationHvr");
		this.trDisabledLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "btnGetCommenddationDsb");
		this.trNormalRight = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "tab_rightNormal");
		this.trHoverRight = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "tab_rightHover");
		this.trClickedRight = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "tab_rightHover");
		this.trDisabledRight = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "tab_rightDisable");
		this.trNormalMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "tab_middleNormal");
		this.trHoverMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "tab_middleHover");
		this.trClickedMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "tab_middleHover");
		this.trDisabledMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "tab_middleDisable");
		this.boundries.set_height((float)this.trNormalLeft.get_height());
		base.FontSize = 12;
		this._marginLeft = 31;
		base.Alignment = 4;
		base.SetRegularFont();
		base.SetColor(GuiNewStyleBar.blueButtonsColor);
	}

	public void SetNewStyleBlueTexture()
	{
		this.SetTexture("PoiScreenWindow", "tab_");
		base.FontSize = 12;
		base.Alignment = 4;
		base.SetRegularFont();
		base.SetColor(GuiNewStyleBar.blueColor);
	}

	public void SetNewStyleOrangeTexture()
	{
		this.trNormalLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "orange_leftNormal");
		this.trHoverLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "orange_leftHover");
		this.trClickedLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "orange_leftHover");
		this.trDisabledLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "tab_leftDisable");
		this.trNormalRight = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "orange_rightNormal");
		this.trHoverRight = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "orange_rightHover");
		this.trClickedRight = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "orange_rightHover");
		this.trDisabledRight = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "tab_rightDisable");
		this.trNormalMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "orange_middleNormal");
		this.trHoverMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "orange_middleHover");
		this.trClickedMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "orange_middleHover");
		this.trDisabledMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "tab_middleDisable");
		this.boundries.set_height((float)this.trNormalLeft.get_height());
		base.FontSize = 12;
		base.Alignment = 4;
		base.SetRegularFont();
		base.SetColor(GuiNewStyleBar.orangeColor);
	}

	public void SetNewStyleRedTexture()
	{
		this.trNormalLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "red_leftNormal");
		this.trHoverLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "red_leftHover");
		this.trClickedLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "red_leftHover");
		this.trDisabledLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "tab_leftDisable");
		this.trNormalRight = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "red_rightNormal");
		this.trHoverRight = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "red_rightHover");
		this.trClickedRight = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "red_rightHover");
		this.trDisabledRight = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "tab_rightDisable");
		this.trNormalMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "red_middleNormal");
		this.trHoverMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "red_middleHover");
		this.trClickedMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "red_middleHover");
		this.trDisabledMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "tab_middleDisable");
		this.boundries.set_height((float)this.trNormalLeft.get_height());
		base.FontSize = 12;
		base.Alignment = 4;
		base.SetRegularFont();
		base.SetColor(GuiNewStyleBar.redColor);
	}

	public void SetNovaBtn()
	{
		this.SetTexture("PoiScreenWindow", "btnNova_");
		this._marginLeft = 32;
		base.SetColor(GuiNewStyleBar.novaBtnColor);
		base.SetRegularFont();
	}

	public void SetOrangeTexture()
	{
		this.trNormalLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "btnOrangeLeftNormal");
		this.trHoverLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "btnOrangeLeftHover");
		this.trClickedLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "btnOrangeLeftNormal");
		this.trDisabledLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "btnOrangeLeftDisable");
		this.trNormalRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "btnOrangeRightNormal");
		this.trHoverRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "btnOrangeRightHover");
		this.trClickedRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "btnOrangeRightNormal");
		this.trDisabledRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "btnOrangeRightDisable");
		this.trNormalMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "btnOrangeMiddleNormal");
		this.trHoverMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "btnOrangeMiddleHover");
		this.trClickedMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "btnOrangeMiddleNormal");
		this.trDisabledMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "btnOrangeMiddleDisable");
		this.boundries.set_height((float)this.trNormalLeft.get_height());
	}

	public void SetResetBtn()
	{
		this.SetTexture("PoiScreenWindow", "btnNova_");
		this.trNormalLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "btnReset_leftNormal");
		this.trHoverLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "btnReset_leftHover");
		this.trClickedLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "btnReset_leftHover");
		this.trDisabledLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("PoiScreenWindow", "btnReset_leftDisable");
		this._marginLeft = 32;
		base.SetColor(GuiNewStyleBar.novaBtnColor);
		base.SetRegularFont();
	}

	public void SetSaveBtn()
	{
		this.SetTexture("PoiScreenWindow", "btnSave_");
		this._marginLeft = 32;
		base.SetColor(GuiNewStyleBar.blueButtonsColor);
		base.SetRegularFont();
	}

	public void SetSmallBlueTexture()
	{
		this.trNormalLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "button_blue_small_leftNormal");
		this.trHoverLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "button_blue_small_leftHover");
		this.trClickedLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "button_blue_small_leftNormal");
		this.trDisabledLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "button_blue_small_leftDisable");
		this.trNormalRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "button_blue_small_rightNormal");
		this.trHoverRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "button_blue_small_rightHover");
		this.trClickedRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "button_blue_small_rightNormal");
		this.trDisabledRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "button_blue_small_rightDisable");
		this.trNormalMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "button_blue_small_middleNormal");
		this.trHoverMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "button_blue_small_middleHover");
		this.trClickedMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "button_blue_small_middleNormal");
		this.trDisabledMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "button_blue_small_middleDisable");
		this.boundries.set_height((float)this.trNormalLeft.get_height());
	}

	public void SetSmallOrangeTexture()
	{
		this.trNormalLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "button_orange_small_leftNormal");
		this.trHoverLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "button_orange_small_leftHover");
		this.trClickedLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "button_orange_small_leftNormal");
		this.trDisabledLeft = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "button_orange_small_leftDisable");
		this.trNormalRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "button_orange_small_rightNormal");
		this.trHoverRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "button_orange_small_rightHover");
		this.trClickedRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "button_orange_small_rightNormal");
		this.trDisabledRight = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "button_orange_small_rightDisable");
		this.trNormalMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "button_orange_small_middleNormal");
		this.trHoverMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "button_orange_small_middleHover");
		this.trClickedMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "button_orange_small_middleNormal");
		this.trDisabledMdl = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "button_orange_small_middleDisable");
		this.boundries.set_height((float)this.trNormalLeft.get_height());
	}

	public void SetTexture(string bundleName, string assetName)
	{
		if (!playWebGame.TryGetTextureFromStaticSet(bundleName, string.Concat(assetName, "leftNormal"), out this.trNormalLeft))
		{
			throw new Exception(string.Concat("Texture ", assetName, "leftNormal was not found!"));
		}
		if (!playWebGame.TryGetTextureFromStaticSet(bundleName, string.Concat(assetName, "leftHover"), out this.trHoverLeft))
		{
			this.trHoverLeft = this.trNormalLeft;
		}
		if (!playWebGame.TryGetTextureFromStaticSet(bundleName, string.Concat(assetName, "leftPressed"), out this.trClickedLeft))
		{
			this.trClickedLeft = this.trNormalLeft;
		}
		if (!playWebGame.TryGetTextureFromStaticSet(bundleName, string.Concat(assetName, "leftDisable"), out this.trDisabledLeft))
		{
			this.trDisabledLeft = this.trNormalLeft;
		}
		if (!playWebGame.TryGetTextureFromStaticSet(bundleName, string.Concat(assetName, "rightNormal"), out this.trNormalRight))
		{
			throw new Exception(string.Concat("Texture ", assetName, "rightNormal was not found!"));
		}
		if (!playWebGame.TryGetTextureFromStaticSet(bundleName, string.Concat(assetName, "rightHover"), out this.trHoverRight))
		{
			this.trHoverRight = this.trNormalRight;
		}
		if (!playWebGame.TryGetTextureFromStaticSet(bundleName, string.Concat(assetName, "rightPressed"), out this.trClickedRight))
		{
			this.trClickedRight = this.trNormalRight;
		}
		if (!playWebGame.TryGetTextureFromStaticSet(bundleName, string.Concat(assetName, "rightDisable"), out this.trDisabledRight))
		{
			this.trDisabledRight = this.trNormalRight;
		}
		if (!playWebGame.TryGetTextureFromStaticSet(bundleName, string.Concat(assetName, "middleNormal"), out this.trNormalMdl))
		{
			throw new Exception(string.Concat("Texture ", assetName, "middleNormal was not found!"));
		}
		if (!playWebGame.TryGetTextureFromStaticSet(bundleName, string.Concat(assetName, "middleHover"), out this.trHoverMdl))
		{
			this.trHoverMdl = this.trNormalMdl;
		}
		if (!playWebGame.TryGetTextureFromStaticSet(bundleName, string.Concat(assetName, "middlePressed"), out this.trClickedMdl))
		{
			this.trClickedMdl = this.trNormalMdl;
		}
		if (!playWebGame.TryGetTextureFromStaticSet(bundleName, string.Concat(assetName, "middleDisable"), out this.trDisabledMdl))
		{
			this.trDisabledMdl = this.trNormalMdl;
		}
		this.boundries.set_height((float)this.trNormalLeft.get_height());
	}
}