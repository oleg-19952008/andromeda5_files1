using System;
using UnityEngine;

public class EscapeToHydraWindow : GuiWindow
{
	private GuiTexture mainTipImg;

	private GuiTexture firstSeparator;

	private GuiTexture secondSeparator;

	private GuiButtonResizeable btnGotIt;

	private GuiLabel tipLbl;

	private GuiLabel tipValue;

	private GuiLabel actionLbl;

	private GuiLabel actionValue;

	public EscapeToHydraWindow()
	{
	}

	public override void Create()
	{
		if (NetworkScript.player == null || NetworkScript.player.playerBelongings == null)
		{
			return;
		}
		base.SetBackgroundTexture("TutorialWindow", "novashop_frame");
		base.PutToCenter();
		this.zOrder = 210;
		this.isHidden = false;
		this.mainTipImg = new GuiTexture();
		this.mainTipImg.SetTexture("TutorialWindow", "escapeToHydra");
		this.mainTipImg.boundries = new Rect(58f, 85f, 360f, 400f);
		base.AddGuiElement(this.mainTipImg);
		this.firstSeparator = new GuiTexture();
		this.firstSeparator.SetTexture("TutorialWindow", "levelup_line");
		this.firstSeparator.boundries = new Rect(458f, 95f, 435f, 1f);
		base.AddGuiElement(this.firstSeparator);
		this.secondSeparator = new GuiTexture();
		this.secondSeparator.SetTexture("TutorialWindow", "levelup_line");
		this.secondSeparator.boundries = new Rect(458f, 275f, 435f, 1f);
		base.AddGuiElement(this.secondSeparator);
		this.btnGotIt = new GuiButtonResizeable();
		this.btnGotIt.boundries.set_x(252f);
		this.btnGotIt.boundries.set_y(480f);
		this.btnGotIt.boundries.set_width(400f);
		this.btnGotIt.Caption = StaticData.Translate("key_tutorial_end_screen_btn");
		this.btnGotIt.FontSize = 16;
		this.btnGotIt.Alignment = 4;
		this.btnGotIt.SetBlueTexture();
		this.btnGotIt.Clicked = new Action<EventHandlerParam>(this, EscapeToHydraWindow.OnGotItClicked);
		base.AddGuiElement(this.btnGotIt);
		this.tipLbl = new GuiLabel()
		{
			text = StaticData.Translate("key_tutorial_end_screen_title"),
			FontSize = 18,
			Font = GuiLabel.FontBold,
			boundries = new Rect(458f, 60f, 425f, 24f),
			Alignment = 3
		};
		base.AddGuiElement(this.tipLbl);
		this.tipValue = new GuiLabel()
		{
			text = StaticData.Translate("key_tutorial_end_screen_info"),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			WordWrap = true,
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(458f, 110f, 425f, 110f),
			Alignment = 0
		};
		base.AddGuiElement(this.tipValue);
		this.actionLbl = new GuiLabel()
		{
			text = StaticData.Translate("key_tutorial_end_screen_action_lbl"),
			FontSize = 18,
			Font = GuiLabel.FontBold,
			boundries = new Rect(458f, 240f, 425f, 24f),
			Alignment = 3
		};
		base.AddGuiElement(this.actionLbl);
		this.actionValue = new GuiLabel()
		{
			text = StaticData.Translate("key_tutorial_end_screen_action_value"),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			WordWrap = true,
			boundries = new Rect(458f, 290f, 425f, 100f)
		};
		this.actionValue.boundries.set_height(this.tipValue.TextHeight);
		this.actionValue.Alignment = 0;
		base.AddGuiElement(this.actionValue);
	}

	private void OnGotItClicked(object prm)
	{
		((__TutorialScript)Object.FindObjectOfType(typeof(__TutorialScript))).GoToHydra();
		AndromedaGui.mainWnd.OnCloseWindowCallback = null;
		AndromedaGui.mainWnd.CloseActiveWindow();
		if (NetworkScript.player.shipScript != null)
		{
			NetworkScript.player.shipScript.isGuiClosed = true;
		}
	}
}