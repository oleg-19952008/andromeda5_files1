using System;
using UnityEngine;

public class GuiDialog
{
	public GuiWindow Container;

	public GuiButtonResizeable btnOK;

	public GuiButtonResizeable btnCancel;

	private GuiTexture FrameTexture;

	private GuiLabel lblMessage;

	public Action<object> OkClicked;

	public Action<object> CancelClicked;

	private bool externalContainer;

	public string Text
	{
		set
		{
			this.lblMessage.text = value;
		}
	}

	public GuiDialog()
	{
	}

	public void Create(string msgTx)
	{
		this.Create(msgTx, StaticData.Translate("key_dialog_ok"), StaticData.Translate("key_dialog_cancel"), AndromedaGui.contentMainMenuWnd);
	}

	public void Create(string msgTx, string okTx, string cancelTx, GuiWindow contain)
	{
		this.externalContainer = contain != null;
		if (this.externalContainer)
		{
			this.Container = contain;
		}
		else
		{
			this.Container = new GuiWindow()
			{
				boundries = new Rect((float)(Screen.get_width() / 2 - 265), (float)(Screen.get_height() / 2 - 130), 530f, 260f)
			};
		}
		this.Container.zOrder = 220;
		this.Container.isHidden = false;
		this.FrameTexture = new GuiTexture()
		{
			X = (this.Container.boundries.get_width() - 530f) / 2f,
			Y = (this.Container.boundries.get_height() - 260f) / 2f
		};
		this.FrameTexture.SetTexture("FrameworkGUI", "menugui_dialog");
		this.Container.AddGuiElement(this.FrameTexture);
		this.btnOK = new GuiButtonResizeable()
		{
			Width = 100f,
			Caption = okTx,
			FontSize = 12,
			Alignment = 4,
			X = this.FrameTexture.X + 265f + 10f,
			Y = this.FrameTexture.Y + 130f + 25f,
			Clicked = new Action<EventHandlerParam>(this, GuiDialog.OnOkClicked)
		};
		this.Container.AddGuiElement(this.btnOK);
		if (cancelTx != string.Empty)
		{
			this.btnCancel = new GuiButtonResizeable();
			this.btnCancel.SetBlueTexture();
			this.btnCancel.Width = 100f;
			this.btnCancel.Caption = cancelTx;
			this.btnCancel.FontSize = 12;
			this.btnCancel.Alignment = 4;
			this.btnCancel.X = this.Container.boundries.get_width() / 2f - 1f * this.btnOK.Width - 10f;
			this.btnCancel.Y = this.btnOK.Y;
			this.btnCancel.SetLeftClickSound("FrameworkGUI", "cancel");
			this.btnCancel.Clicked = new Action<EventHandlerParam>(this, GuiDialog.OnCancelClicked);
			this.Container.AddGuiElement(this.btnCancel);
		}
		this.lblMessage = new GuiLabel()
		{
			text = msgTx,
			Alignment = 4,
			boundries = new Rect(this.FrameTexture.X + 45f, this.FrameTexture.Y + 15f, this.FrameTexture.boundries.get_width() - 90f, 155f),
			FontSize = 14
		};
		this.Container.AddGuiElement(this.lblMessage);
		if (!this.externalContainer)
		{
			this.Container.SetBackgroundTexture("FrameworkGUI", "menugui_dialog");
			AndromedaGui.gui.AddWindow(this.Container);
		}
	}

	private void OnCancelClicked(object p)
	{
		if (this.CancelClicked != null)
		{
			this.CancelClicked.Invoke(p);
		}
		if (this.externalContainer)
		{
			this.RemoveGUIItems();
		}
		else
		{
			this.Container.StartHammerEffect(0f - this.Container.boundries.get_width(), this.Container.boundries.get_y());
			this.Container.fxEnded = new Action(this, GuiDialog.RemoveGUIItems);
		}
	}

	private void OnOkClicked(object p)
	{
		if (this.OkClicked != null)
		{
			this.OkClicked.Invoke(p);
		}
	}

	public void RemoveGUIItems()
	{
		this.Container.RemoveGuiElement(this.lblMessage);
		this.Container.RemoveGuiElement(this.btnOK);
		if (this.btnCancel != null)
		{
			this.Container.RemoveGuiElement(this.btnCancel);
		}
		this.Container.RemoveGuiElement(this.FrameTexture);
		if (!this.externalContainer)
		{
			this.Container.isHidden = true;
			AndromedaGui.gui.RemoveWindow(this.Container.handler);
		}
	}
}