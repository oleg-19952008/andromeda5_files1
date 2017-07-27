using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RateUSWindow : GuiWindow
{
	private List<GuiElement> forDelete = new List<GuiElement>();

	private GuiTextBox tbLongDescription;

	public GuiButtonFixed btnClose;

	public RateUSWindow()
	{
	}

	private void CleanWindow()
	{
		if (this.forDelete != null && this.forDelete.get_Count() > 0)
		{
			foreach (GuiElement guiElement in this.forDelete)
			{
				base.RemoveGuiElement(guiElement);
			}
			this.forDelete.Clear();
		}
	}

	public override void Create()
	{
		base.SetBackgroundTexture("iPad/RateUs", "rate_us_frame");
		base.PutToCenter();
		this.zOrder = 210;
		this.isHidden = false;
		GuiLabel guiLabel = new GuiLabel()
		{
			text = StaticData.Translate("key_rateus_title"),
			FontSize = 25,
			Alignment = 4,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			boundries = new Rect(this.boundries.get_width() / 2f - 150f, 30f, 300f, 30f)
		};
		base.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			text = StaticData.Translate("key_rateus_enjoy_the_game"),
			FontSize = 18,
			Alignment = 4,
			Font = GuiLabel.FontBold,
			TextColor = Color.get_white(),
			boundries = new Rect(200f, 105f, 300f, 20f)
		};
		base.AddGuiElement(guiLabel1);
		this.forDelete.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			text = StaticData.Translate("key_rateus_description"),
			FontSize = 12,
			Alignment = 4,
			TextColor = Color.get_white(),
			boundries = new Rect(200f, 140f, 300f, 50f)
		};
		base.AddGuiElement(guiLabel2);
		this.forDelete.Add(guiLabel2);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTextureKeepSize("iPad/RateUs", "starsWithHolder");
		guiTexture.boundries = new Rect(207f, 200f, 287f, 57f);
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			text = StaticData.Translate("key_rateus_how_would_you"),
			FontSize = 18,
			Alignment = 4,
			Font = GuiLabel.FontBold,
			TextColor = Color.get_white(),
			boundries = new Rect(200f, 280f, 300f, 20f)
		};
		base.AddGuiElement(guiLabel3);
		this.forDelete.Add(guiLabel3);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("FrameworkGUI", "empty");
		guiButtonFixed.boundries = new Rect(270f, 200f, 120f, 57f);
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, RateUSWindow.OnBadFeedbackClcik);
		base.AddGuiElement(guiButtonFixed);
		this.forDelete.Add(guiButtonFixed);
		GuiButtonFixed rect = new GuiButtonFixed();
		rect.SetTexture("FrameworkGUI", "empty");
		rect.boundries = new Rect(390f, 200f, 50f, 57f);
		rect.Caption = string.Empty;
		rect.Clicked = new Action<EventHandlerParam>(this, RateUSWindow.OnFiveStarClick);
		base.AddGuiElement(rect);
		this.forDelete.Add(rect);
		GuiButtonFixed _black = new GuiButtonFixed();
		_black.SetTexture("iPad/RateUs", "btnBlue");
		_black.boundries.set_x(205f);
		_black.boundries.set_y(320f);
		_black.Caption = StaticData.Translate("key_rateus_btn_1to4stars");
		_black.FontSize = 18;
		_black.Alignment = 4;
		_black.textColorNormal = Color.get_black();
		_black.Clicked = new Action<EventHandlerParam>(this, RateUSWindow.OnBadFeedbackClcik);
		base.AddGuiElement(_black);
		this.forDelete.Add(_black);
		GuiButtonFixed action = new GuiButtonFixed();
		action.SetTexture("iPad/RateUs", "btnGreen");
		action.boundries.set_x(350f);
		action.boundries.set_y(320f);
		action.Caption = StaticData.Translate("key_rateus_btn_5stars");
		action.FontSize = 18;
		action.Alignment = 4;
		action.textColorNormal = Color.get_black();
		action.Clicked = new Action<EventHandlerParam>(this, RateUSWindow.OnFiveStarClick);
		base.AddGuiElement(action);
		this.forDelete.Add(action);
	}

	public void OnBadFeedbackClcik(EventHandlerParam prm)
	{
		this.CleanWindow();
		GuiLabel guiLabel = new GuiLabel()
		{
			text = StaticData.Translate("key_rate_us_bad_description"),
			FontSize = 12,
			Alignment = 3,
			TextColor = Color.get_white(),
			boundries = new Rect(200f, 100f, 300f, 20f)
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		this.tbLongDescription = new GuiTextBox()
		{
			boundries = new Rect(200f, 150f, 300f, 30f),
			Alignment = 0
		};
		this.tbLongDescription.SetFrameTexture("NewGUI", "GuildTA");
		this.tbLongDescription.FontSize = 14;
		this.tbLongDescription.TextColor = GuiNewStyleBar.blueColor;
		this.tbLongDescription.isMultiLine = true;
		this.tbLongDescription.Validate = new Action(this, RateUSWindow.ValidateInputLong);
		base.AddGuiElement(this.tbLongDescription);
		this.forDelete.Add(this.tbLongDescription);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable()
		{
			boundries = new Rect(250f, 350f, 180f, 42f),
			Caption = StaticData.Translate("key_feedback_send"),
			FontSize = 12,
			Alignment = 4
		};
		guiButtonResizeable.SetOrangeTexture();
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, RateUSWindow.OnSendBadFeedback);
		base.AddGuiElement(guiButtonResizeable);
		this.forDelete.Add(guiButtonResizeable);
	}

	private void OnFiveStarClick(EventHandlerParam prm)
	{
		Application.OpenURL("market://details?id=bg.xssoftware.andromeda");
		AndromedaGui.mainWnd.CloseActiveWindow();
	}

	private void OnSendBadFeedback(EventHandlerParam prm)
	{
		NetworkScript.player.shipScript.StartCoroutine(this.SendRateUsFeedback(this.tbLongDescription.text));
	}

	[DebuggerHidden]
	private IEnumerator SendRateUsFeedback(string Description)
	{
		RateUSWindow.<SendRateUsFeedback>c__Iterator8 variable = null;
		return variable;
	}

	private void ValidateInputLong()
	{
		if (this.tbLongDescription.text.get_Length() > 300)
		{
			this.tbLongDescription.text = this.tbLongDescription.text.Substring(0, 300);
		}
	}
}