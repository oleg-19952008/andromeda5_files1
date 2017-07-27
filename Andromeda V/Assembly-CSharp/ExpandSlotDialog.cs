using System;
using System.Collections.Generic;
using UnityEngine;

public class ExpandSlotDialog
{
	private GuiWindow expandSlotWindow;

	private GuiFramework gui;

	public Action<EventHandlerParam> ConfirmExpand;

	public ExpandSlotDialog()
	{
	}

	public void Cancel()
	{
		this.CancelExpand(null);
	}

	private void CancelExpand(object prm)
	{
		this.gui.RemoveWindow(this.expandSlotWindow.handler);
		this.expandSlotWindow = null;
		AndromedaGui.gui.activeToolTipId = -1;
	}

	public void CreateExpandDialog(int price, SelectedCurrency currency, string text, GuiFramework guiFw)
	{
		this.gui = guiFw;
		this.expandSlotWindow = new GuiWindow()
		{
			isModal = true
		};
		this.expandSlotWindow.SetBackgroundTexture("ConfigWindow", "proba");
		this.expandSlotWindow.isHidden = false;
		this.expandSlotWindow.zOrder = 220;
		this.expandSlotWindow.PutToCenter();
		this.gui.AddWindow(this.expandSlotWindow);
		AndromedaGui.gui.activeToolTipId = this.expandSlotWindow.handler;
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(this.expandSlotWindow.boundries.get_width() * 0.05f, 40f, this.expandSlotWindow.boundries.get_width() * 0.9f, 60f),
			text = text,
			Alignment = 4,
			FontSize = 18
		};
		this.expandSlotWindow.AddGuiElement(guiLabel);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("ConfigWindow", "X_btn_");
		guiButtonFixed.boundries = new Rect(417f, -3f, 58f, 46f);
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, ExpandSlotDialog.CancelExpand);
		guiButtonFixed.SetLeftClickSound("FrameworkGUI", "cancel");
		this.expandSlotWindow.AddGuiElement(guiButtonFixed);
		GuiButtonFixed _width = new GuiButtonFixed();
		_width.SetTexture("ConfigWindow", "testBtn");
		_width.X = (float)((double)this.expandSlotWindow.boundries.get_width() * 0.5 - (double)_width.boundries.get_width() * 0.5);
		_width.Y = 123f;
		_width.Caption = string.Empty;
		if (currency == 1)
		{
			_width.isEnabled = (NetworkScript.player.playerBelongings.playerItems.get_Nova() <= (long)price ? false : price > 0);
		}
		else if (currency == 2)
		{
			_width.isEnabled = (NetworkScript.player.playerBelongings.playerItems.get_Equilibrium() <= (long)price ? false : price > 0);
		}
		else if (currency == null)
		{
			_width.isEnabled = (NetworkScript.player.playerBelongings.playerItems.get_Cash() <= (long)price ? false : price > 0);
		}
		_width.eventHandlerParam.customData = (SelectedCurrency)0;
		_width.Clicked = this.ConfirmExpand;
		this.expandSlotWindow.AddGuiElement(_width);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("FrameworkGUI", "cashIcon");
		guiTexture.SetSize(24f, 24f);
		guiTexture.Y = _width.Y + 8f;
		guiTexture.X = _width.X + _width.boundries.get_width() / 2f - 12f;
		this.expandSlotWindow.AddGuiElement(guiTexture);
		if (currency == 1)
		{
			guiTexture.SetTextureKeepSize("FrameworkGUI", "novaIcon");
		}
		else if (currency == 2)
		{
			guiTexture.SetTextureKeepSize("FrameworkGUI", "eqIcon");
		}
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = _width.boundries,
			Y = 90f,
			text = (price != 0 ? price.ToString("##,##0") : string.Empty),
			Alignment = 4,
			TextColor = GuiNewStyleBar.blueColor
		};
		this.expandSlotWindow.AddGuiElement(guiLabel1);
	}

	public void CreateExpandDialogShipSlot(SortedList<SelectedCurrency, int> Currency_PriceList, string text, GuiFramework guiFw)
	{
		this.gui = guiFw;
		this.expandSlotWindow = new GuiWindow()
		{
			isModal = true
		};
		this.expandSlotWindow.SetBackgroundTexture("ConfigWindow", "proba");
		this.expandSlotWindow.isHidden = false;
		this.expandSlotWindow.zOrder = 220;
		this.expandSlotWindow.PutToCenter();
		this.gui.AddWindow(this.expandSlotWindow);
		AndromedaGui.gui.activeToolTipId = this.expandSlotWindow.handler;
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(this.expandSlotWindow.boundries.get_width() * 0.05f, 40f, this.expandSlotWindow.boundries.get_width() * 0.9f, 60f),
			text = text,
			Alignment = 4,
			FontSize = 18
		};
		this.expandSlotWindow.AddGuiElement(guiLabel);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("ConfigWindow", "X_btn_");
		guiButtonFixed.boundries = new Rect(417f, -3f, 58f, 46f);
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, ExpandSlotDialog.CancelExpand);
		guiButtonFixed.SetLeftClickSound("FrameworkGUI", "cancel");
		this.expandSlotWindow.AddGuiElement(guiButtonFixed);
		GuiButtonFixed empty = new GuiButtonFixed();
		empty.SetTexture("ConfigWindow", "testBtn");
		empty.X = 30f;
		empty.Y = 123f;
		empty.Caption = string.Empty;
		empty.isEnabled = (!Currency_PriceList.get_Keys().Contains(0) || Currency_PriceList.get_Item(0) <= 0 ? false : NetworkScript.player.playerBelongings.playerItems.get_Cash() >= (long)Currency_PriceList.get_Item(0));
		empty.eventHandlerParam.customData = (SelectedCurrency)0;
		empty.Clicked = this.ConfirmExpand;
		this.expandSlotWindow.AddGuiElement(empty);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("FrameworkGUI", "cashIcon");
		guiTexture.SetSize(24f, 24f);
		guiTexture.Y = empty.Y + 8f;
		guiTexture.X = empty.X + empty.boundries.get_width() / 2f - 12f;
		this.expandSlotWindow.AddGuiElement(guiTexture);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = empty.boundries,
			Y = 90f,
			text = (!Currency_PriceList.get_Keys().Contains(0) ? string.Empty : Currency_PriceList.get_Item(0).ToString("##,##0")),
			Alignment = 4,
			TextColor = GuiNewStyleBar.blueColor
		};
		this.expandSlotWindow.AddGuiElement(guiLabel1);
		GuiButtonFixed x = new GuiButtonFixed();
		x.SetTexture("ConfigWindow", "testBtn");
		x.X = empty.X + empty.boundries.get_width() + 10f;
		x.Y = 123f;
		x.Caption = string.Empty;
		x.isEnabled = (!Currency_PriceList.get_Keys().Contains(1) || Currency_PriceList.get_Item(1) <= 0 ? false : NetworkScript.player.playerBelongings.playerItems.get_Nova() >= (long)Currency_PriceList.get_Item(1));
		x.eventHandlerParam.customData = (SelectedCurrency)1;
		x.Clicked = this.ConfirmExpand;
		this.expandSlotWindow.AddGuiElement(x);
		GuiTexture y = new GuiTexture();
		y.SetTextureKeepSize("FrameworkGUI", "novaIcon");
		y.SetSize(24f, 24f);
		y.Y = x.Y + 8f;
		y.X = x.X + x.boundries.get_width() / 2f - 12f;
		this.expandSlotWindow.AddGuiElement(y);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = x.boundries,
			Y = 90f,
			text = (!Currency_PriceList.get_Keys().Contains(1) ? string.Empty : Currency_PriceList.get_Item(1).ToString("##,##0")),
			Alignment = 4,
			TextColor = GuiNewStyleBar.blueColor
		};
		this.expandSlotWindow.AddGuiElement(guiLabel2);
		GuiButtonFixed confirmExpand = new GuiButtonFixed();
		confirmExpand.SetTexture("ConfigWindow", "testBtn");
		confirmExpand.X = x.X + x.boundries.get_width() + 10f;
		confirmExpand.Y = 123f;
		confirmExpand.Caption = string.Empty;
		confirmExpand.isEnabled = (!Currency_PriceList.get_Keys().Contains(2) || Currency_PriceList.get_Item(2) <= 0 ? false : NetworkScript.player.playerBelongings.playerItems.get_Equilibrium() >= (long)Currency_PriceList.get_Item(2));
		confirmExpand.eventHandlerParam.customData = (SelectedCurrency)2;
		confirmExpand.Clicked = this.ConfirmExpand;
		this.expandSlotWindow.AddGuiElement(confirmExpand);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTextureKeepSize("FrameworkGUI", "eqIcon");
		guiTexture1.SetSize(24f, 24f);
		guiTexture1.Y = confirmExpand.Y + 8f;
		guiTexture1.X = confirmExpand.X + confirmExpand.boundries.get_width() / 2f - 12f;
		this.expandSlotWindow.AddGuiElement(guiTexture1);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = confirmExpand.boundries,
			Y = 90f,
			text = (!Currency_PriceList.get_Keys().Contains(2) ? string.Empty : Currency_PriceList.get_Item(2).ToString("##,##0")),
			Alignment = 4,
			TextColor = GuiNewStyleBar.blueColor
		};
		this.expandSlotWindow.AddGuiElement(guiLabel3);
	}
}