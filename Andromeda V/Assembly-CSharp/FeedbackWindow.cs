using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FeedbackWindow : GuiWindow
{
	private List<GuiElement> forDelete = new List<GuiElement>();

	private GuiTextBox tbLongDescription;

	private GuiTextBox tbShortDescription;

	private GuiButtonResizeable btnSend;

	public GuiButtonFixed btnClose;

	private GuiLabel shortDescriptionErr;

	private GuiLabel longDescriptionErr;

	private GuiTexture shot;

	private GuiTexture screenshotFrame;

	private GuiCheckbox cbAttachScreenshot;

	public FeedbackWindow()
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
			if (base.HasGuiElement(this.shot))
			{
				base.RemoveGuiElement(this.shot);
			}
			this.forDelete.Clear();
		}
	}

	public override void Create()
	{
		base.SetBackgroundTexture("NewGUI", "novashop_frame");
		base.PutToCenter();
		this.zOrder = 210;
		this.isHidden = false;
		GuiLabel guiLabel = new GuiLabel()
		{
			text = StaticData.Translate("key_feedback_capture"),
			FontSize = 18,
			Alignment = 4,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			boundries = new Rect(this.boundries.get_width() / 2f - 150f, 60f, 300f, 30f)
		};
		base.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			text = StaticData.Translate("key_feedback_quick_report_form"),
			FontSize = 14,
			Alignment = 3,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(95f, 140f, 300f, 20f)
		};
		base.AddGuiElement(guiLabel1);
		this.forDelete.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			text = StaticData.Translate("key_feedback_short_description"),
			FontSize = 12,
			Alignment = 3,
			TextColor = Color.get_white(),
			boundries = new Rect(guiLabel1.boundries.get_x(), guiLabel1.boundries.get_y() + 40f, 300f, 20f)
		};
		base.AddGuiElement(guiLabel2);
		this.forDelete.Add(guiLabel2);
		this.shortDescriptionErr = new GuiLabel()
		{
			text = string.Empty,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			Alignment = 3,
			TextColor = GuiNewStyleBar.redColor,
			boundries = new Rect(guiLabel1.boundries.get_x(), guiLabel1.boundries.get_y() + 20f, 300f, 20f)
		};
		base.AddGuiElement(this.shortDescriptionErr);
		this.forDelete.Add(this.shortDescriptionErr);
		this.tbShortDescription = new GuiTextBox()
		{
			boundries = new Rect(guiLabel1.boundries.get_x(), guiLabel2.boundries.get_y() + 20f, 300f, 30f),
			Alignment = 0
		};
		this.tbShortDescription.SetFrameTexture("NewGUI", "guildTB");
		this.tbShortDescription.FontSize = 14;
		this.tbShortDescription.TextColor = GuiNewStyleBar.blueColor;
		this.tbShortDescription.Validate = new Action(this, FeedbackWindow.ValidateInputShort);
		base.AddGuiElement(this.tbShortDescription);
		this.forDelete.Add(this.tbShortDescription);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			text = StaticData.Translate("key_feedback_long_description"),
			FontSize = 12,
			Alignment = 3,
			TextColor = Color.get_white(),
			boundries = new Rect(guiLabel1.boundries.get_x(), this.tbShortDescription.boundries.get_y() + 70f, 300f, 20f)
		};
		base.AddGuiElement(guiLabel3);
		this.forDelete.Add(guiLabel3);
		this.longDescriptionErr = new GuiLabel()
		{
			text = string.Empty,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			Alignment = 3,
			TextColor = GuiNewStyleBar.redColor,
			boundries = new Rect(guiLabel1.boundries.get_x(), this.tbShortDescription.boundries.get_y() + 50f, 300f, 20f)
		};
		base.AddGuiElement(this.longDescriptionErr);
		this.forDelete.Add(this.longDescriptionErr);
		this.tbLongDescription = new GuiTextBox()
		{
			boundries = new Rect(guiLabel1.boundries.get_x(), guiLabel3.boundries.get_y() + 20f, 300f, 30f),
			Alignment = 0
		};
		this.tbLongDescription.SetFrameTexture("NewGUI", "GuildTA");
		this.tbLongDescription.FontSize = 14;
		this.tbLongDescription.TextColor = GuiNewStyleBar.blueColor;
		this.tbLongDescription.isMultiLine = true;
		this.tbLongDescription.Validate = new Action(this, FeedbackWindow.ValidateInputLong);
		base.AddGuiElement(this.tbLongDescription);
		this.forDelete.Add(this.tbLongDescription);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			text = StaticData.Translate("key_feedback_attention"),
			FontSize = 14,
			Alignment = 3,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(530f, guiLabel1.boundries.get_y(), 300f, 20f)
		};
		base.AddGuiElement(guiLabel4);
		this.forDelete.Add(guiLabel4);
		GuiLabel guiLabel5 = new GuiLabel()
		{
			boundries = new Rect(guiLabel4.boundries.get_x(), guiLabel4.boundries.get_y() + 25f, 193f, 155f),
			text = StaticData.Translate("key_feedback_info"),
			FontSize = 12,
			TextColor = Color.get_white()
		};
		base.AddGuiElement(guiLabel5);
		this.forDelete.Add(guiLabel5);
		this.btnSend = new GuiButtonResizeable();
		this.btnSend.boundries.set_width(180f);
		this.btnSend.boundries.set_y(470f);
		this.btnSend.boundries.set_x(this.boundries.get_width() / 2f - this.btnSend.boundries.get_width() / 2f);
		this.btnSend.Caption = StaticData.Translate("key_feedback_send");
		this.btnSend.FontSize = 12;
		this.btnSend.Alignment = 4;
		this.btnSend.SetOrangeTexture();
		this.btnSend.Clicked = new Action<EventHandlerParam>(this, FeedbackWindow.OnSendFeedback);
		base.AddGuiElement(this.btnSend);
		this.forDelete.Add(this.btnSend);
	}

	public void DrawFeedbackResult(bool hasError)
	{
		this.CleanWindow();
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTextureKeepSize("NewGUI", (!hasError ? "sentReport" : "sentReportNotOK"));
		guiTexture.boundries = new Rect(this.boundries.get_width() / 2f - 75f + 10f, 150f, 150f, 100f);
		guiTexture.isActive = true;
		base.AddGuiElement(guiTexture);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = StaticData.Translate((!hasError ? "key_feedback_report_ok" : "key_feedback_report_not_ok")),
			FontSize = 16,
			Alignment = 4,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(this.boundries.get_width() / 2f - 200f, 310f, 400f, 20f)
		};
		base.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			text = StaticData.Translate((!hasError ? "key_feedback_thank_you" : "key_feedback_try_again")),
			FontSize = 16,
			Alignment = 4,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(this.boundries.get_width() / 2f - 250f, 350f, 500f, 20f)
		};
		base.AddGuiElement(guiLabel1);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.boundries.set_width(180f);
		guiButtonResizeable.boundries.set_y(470f);
		guiButtonResizeable.boundries.set_x(this.boundries.get_width() / 2f - guiButtonResizeable.boundries.get_width() / 2f);
		guiButtonResizeable.Caption = StaticData.Translate("key_feedback_back");
		guiButtonResizeable.FontSize = 12;
		guiButtonResizeable.Alignment = 4;
		guiButtonResizeable.SetBlueTexture();
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, FeedbackWindow.OnBackToGame);
		base.AddGuiElement(guiButtonResizeable);
	}

	private void OnAtachBtnClick(object prm)
	{
		this.cbAttachScreenshot.Selected = !this.cbAttachScreenshot.Selected;
		this.OnCheckBoxAttachedClicked(this.cbAttachScreenshot.Selected);
	}

	private void OnBackToGame(EventHandlerParam prm)
	{
		if (NetworkScript.player.shipScript != null)
		{
			NetworkScript.player.shipScript.isGuiClosed = true;
		}
		if (!NetworkScript.isInBase)
		{
			AndromedaGui.mainWnd.CloseActiveWindow();
		}
		else if (this.btnClose != null && this.btnClose.Clicked != null)
		{
			this.OnClose();
			this.btnClose.Clicked.Invoke(null);
		}
	}

	private void OnCheckBoxAttachedClicked(bool prm)
	{
		if (this.shot != null)
		{
			if (!prm)
			{
				this.shot.isActive = false;
				base.RemoveGuiElement(this.shot);
			}
			else
			{
				this.shot.isActive = true;
				if (this.screenshotFrame != null)
				{
					base.RemoveGuiElement(this.screenshotFrame);
					base.RemoveGuiElement(this.shot);
					base.AddGuiElement(this.shot);
					this.forDelete.Add(this.shot);
					base.AddGuiElement(this.screenshotFrame);
					this.forDelete.Add(this.screenshotFrame);
				}
			}
		}
	}

	public override void OnClose()
	{
		Object.Destroy(this.shot.GetTexture2D());
	}

	private void OnSendFeedback(EventHandlerParam prm)
	{
		byte[] pNG;
		bool flag = false;
		this.shortDescriptionErr.text = string.Empty;
		this.longDescriptionErr.text = string.Empty;
		if (this.tbShortDescription.text.get_Length() < 5)
		{
			flag = true;
			this.shortDescriptionErr.text = StaticData.Translate("key_feedback_short_description_err");
		}
		if (this.tbLongDescription.text.get_Length() < 5)
		{
			flag = true;
			this.longDescriptionErr.text = StaticData.Translate("key_feedback_long_description_err");
		}
		if (flag)
		{
			return;
		}
		this.btnSend.isEnabled = false;
		if (this.shot == null || !this.shot.isActive)
		{
			pNG = null;
		}
		else
		{
			pNG = this.shot.GetTexture2D().EncodeToPNG();
		}
		byte[] numArray = pNG;
		if (!NetworkScript.isInBase && NetworkScript.player.shipScript != null)
		{
			NetworkScript.player.shipScript.StartCoroutine(this.SendFeedback(this.tbLongDescription.text, this.tbShortDescription.text, numArray));
		}
		else if (NetworkScript.isInBase)
		{
			InBaseScript component = GameObject.Find("InBaseScript").GetComponent<InBaseScript>();
			component.StartCoroutine(this.SendFeedback(this.tbLongDescription.text, this.tbShortDescription.text, numArray));
		}
	}

	public void PopulateScreenShot(Texture2D screenShot)
	{
		if (screenShot == null)
		{
			Debug.Log("No 2DTexture screenshot passed to feedback window");
		}
		else
		{
			this.cbAttachScreenshot = new GuiCheckbox()
			{
				X = 525f,
				Y = 235f,
				Selected = true,
				isActive = true,
				OnCheckboxSelected = new Action<bool>(this, FeedbackWindow.OnCheckBoxAttachedClicked)
			};
			base.AddGuiElement(this.cbAttachScreenshot);
			this.forDelete.Add(this.cbAttachScreenshot);
			GuiButton guiButton = new GuiButton()
			{
				Caption = string.Empty,
				boundries = new Rect(525f, 255f, 290f, 170f),
				Clicked = new Action<EventHandlerParam>(this, FeedbackWindow.OnAtachBtnClick)
			};
			base.AddGuiElement(guiButton);
			this.forDelete.Add(guiButton);
			GuiLabel guiLabel = new GuiLabel()
			{
				text = StaticData.Translate("key_feedback_attached_screenshot"),
				FontSize = 12,
				Alignment = 3,
				TextColor = Color.get_white(),
				boundries = new Rect(550f, 236f, 300f, 20f)
			};
			base.AddGuiElement(guiLabel);
			this.forDelete.Add(guiLabel);
			this.shot = new GuiTexture();
			this.shot.SetTextureKeepSize(screenShot);
			this.shot.boundries = new Rect(530f, 260f, 280f, 161f);
			this.shot.isActive = true;
			base.AddGuiElement(this.shot);
			this.forDelete.Add(guiLabel);
			this.screenshotFrame = new GuiTexture();
			this.screenshotFrame.SetTextureKeepSize("NewGUI", "attachementFrame");
			this.screenshotFrame.boundries = new Rect(525f, 247f, 302f, 180f);
			base.AddGuiElement(this.screenshotFrame);
			this.forDelete.Add(this.screenshotFrame);
		}
	}

	[DebuggerHidden]
	private IEnumerator SendFeedback(string longDescription, string shortDescription, byte[] shotToByte)
	{
		FeedbackWindow.<SendFeedback>c__Iterator7 variable = null;
		return variable;
	}

	private void ValidateInputLong()
	{
		if (this.tbLongDescription.text.get_Length() > 300)
		{
			this.tbLongDescription.text = this.tbLongDescription.text.Substring(0, 300);
		}
	}

	private void ValidateInputShort()
	{
		if (this.tbShortDescription.text.get_Length() > 20)
		{
			this.tbShortDescription.text = this.tbShortDescription.text.Substring(0, 20);
		}
	}
}