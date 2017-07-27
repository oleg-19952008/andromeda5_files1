using System;
using UnityEngine;

public class NewPopUpWindow
{
	public NewPopUpWindow()
	{
	}

	public static void CreatePopUpWindow(string title, string info, string additionalInfo, string cofirmBtnText, string cancelBtnText, out GuiWindow popUpWindow, EventHandlerParam prm = null, Action<EventHandlerParam> OnConfirm = null, Action<EventHandlerParam> OnCancel = null)
	{
		GuiWindow guiWindow = new GuiWindow()
		{
			isModal = true
		};
		guiWindow.SetBackgroundTexture("WarScreenWindow", "popupBackground");
		guiWindow.isHidden = false;
		guiWindow.zOrder = 220;
		guiWindow.PutToCenter();
		guiWindow.isModal = true;
		guiWindow.isHidden = false;
		popUpWindow = guiWindow;
		AndromedaGui.gui.AddWindow(guiWindow);
		AndromedaGui.gui.activeToolTipId = guiWindow.handler;
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(68f, 66f, 490f, 28f),
			FontSize = 16,
			TextColor = GuiNewStyleBar.blueColor,
			text = title,
			Alignment = 4
		};
		guiWindow.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(73f, 107f, 480f, 100f),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			text = info,
			Alignment = 4
		};
		guiWindow.AddGuiElement(guiLabel1);
		if (!string.IsNullOrEmpty(additionalInfo))
		{
			guiLabel1.Alignment = 1;
			GuiLabel guiLabel2 = new GuiLabel()
			{
				boundries = new Rect(73f, 107f, 480f, 100f),
				FontSize = 12,
				TextColor = GuiNewStyleBar.blueColor,
				text = additionalInfo,
				Alignment = 7
			};
			guiWindow.AddGuiElement(guiLabel2);
		}
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.SetNewStyleRedTexture();
		guiButtonResizeable.X = 77f;
		guiButtonResizeable.Y = 220f;
		guiButtonResizeable.boundries.set_width(175f);
		guiButtonResizeable.Caption = cofirmBtnText;
		guiButtonResizeable.FontSize = 14;
		guiButtonResizeable.Alignment = 4;
		guiButtonResizeable.eventHandlerParam = prm;
		guiButtonResizeable.Clicked = OnConfirm;
		guiWindow.AddGuiElement(guiButtonResizeable);
		GuiButtonResizeable guiButtonResizeable1 = new GuiButtonResizeable();
		guiButtonResizeable1.SetNewStyleBlueTexture();
		guiButtonResizeable1.X = 374f;
		guiButtonResizeable1.Y = 220f;
		guiButtonResizeable1.boundries.set_width(175f);
		guiButtonResizeable1.Caption = cancelBtnText;
		guiButtonResizeable1.FontSize = 14;
		guiButtonResizeable1.Alignment = 4;
		guiButtonResizeable1.Clicked = (OnCancel != null ? OnCancel : new Action<EventHandlerParam>(null, NewPopUpWindow.OnCancelBtnClicked));
		guiWindow.AddGuiElement(guiButtonResizeable1);
	}

	public static void CreatePurchasePopUpWindow(string title, string info, string cofirmBtnText, string cancelBtnText, out GuiWindow popUpWindow, EventHandlerParam prm = null, Action<EventHandlerParam> OnConfirm = null)
	{
		GuiWindow guiWindow = new GuiWindow()
		{
			isModal = true
		};
		guiWindow.SetBackgroundTexture("WarScreenWindow", "popupBackground");
		guiWindow.isHidden = false;
		guiWindow.zOrder = 220;
		guiWindow.PutToCenter();
		guiWindow.isModal = true;
		guiWindow.isHidden = false;
		popUpWindow = guiWindow;
		AndromedaGui.gui.AddWindow(guiWindow);
		AndromedaGui.gui.activeToolTipId = guiWindow.handler;
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(68f, 66f, 490f, 28f),
			FontSize = 16,
			TextColor = GuiNewStyleBar.blueColor,
			text = title,
			Alignment = 4
		};
		guiWindow.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(73f, 107f, 480f, 100f),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			text = info,
			Alignment = 4
		};
		guiWindow.AddGuiElement(guiLabel1);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.SetNewStyleOrangeTexture();
		guiButtonResizeable.X = 77f;
		guiButtonResizeable.Y = 220f;
		guiButtonResizeable.boundries.set_width(175f);
		guiButtonResizeable.Caption = cofirmBtnText;
		guiButtonResizeable.FontSize = 14;
		guiButtonResizeable.Alignment = 4;
		guiButtonResizeable.eventHandlerParam = prm;
		guiButtonResizeable.Clicked = OnConfirm;
		guiWindow.AddGuiElement(guiButtonResizeable);
		GuiButtonResizeable action = new GuiButtonResizeable();
		action.SetNewStyleBlueTexture();
		action.X = 374f;
		action.Y = 220f;
		action.boundries.set_width(175f);
		action.Caption = cancelBtnText;
		action.FontSize = 14;
		action.Alignment = 4;
		action.Clicked = new Action<EventHandlerParam>(null, NewPopUpWindow.OnCancelBtnClicked);
		guiWindow.AddGuiElement(action);
	}

	public static void CreateUniversalPurchasePopUpWindow(string title, string info, long priceC, int priceE, int priceU, int priceN, string cofirmBtnText, string cancelBtnText, out GuiWindow popUpWindow, EventHandlerParam prm = null, Action<EventHandlerParam> OnConfirm = null)
	{
		GuiTexture guiTexture = null;
		GuiTexture guiTexture1 = null;
		GuiTexture guiTexture2 = null;
		GuiTexture guiTexture3 = null;
		GuiLabel guiLabel = null;
		GuiLabel x = null;
		GuiLabel guiLabel1 = null;
		GuiLabel x1 = null;
		GuiWindow guiWindow = new GuiWindow()
		{
			isModal = true
		};
		guiWindow.SetBackgroundTexture("WarScreenWindow", "popupBackground");
		guiWindow.isHidden = false;
		guiWindow.zOrder = 220;
		guiWindow.PutToCenter();
		guiWindow.isModal = true;
		guiWindow.isHidden = false;
		popUpWindow = guiWindow;
		AndromedaGui.gui.AddWindow(guiWindow);
		AndromedaGui.gui.activeToolTipId = guiWindow.handler;
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(68f, 66f, 490f, 28f),
			FontSize = 16,
			TextColor = GuiNewStyleBar.blueColor,
			text = title,
			Alignment = 4
		};
		guiWindow.AddGuiElement(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(73f, 107f, 480f, 100f),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			text = info,
			Alignment = 1
		};
		guiWindow.AddGuiElement(guiLabel3);
		float _width = 0f;
		int num = 0;
		float single = 180f;
		if (priceC > (long)0)
		{
			guiTexture = new GuiTexture();
			guiTexture.SetTexture("FrameworkGUI", "res_cash");
			guiTexture.X = 73f;
			guiTexture.Y = single;
			guiWindow.AddGuiElement(guiTexture);
			guiLabel = new GuiLabel()
			{
				boundries = new Rect(guiTexture.X + 20f, single, 100f, guiTexture.boundries.get_height()),
				text = priceC.ToString("N0"),
				FontSize = 12,
				Alignment = 3,
				TextColor = GuiNewStyleBar.blueColor,
				Font = GuiLabel.FontBold
			};
			guiLabel.boundries.set_width(guiLabel.TextWidth);
			guiWindow.AddGuiElement(guiLabel);
			_width = _width + (guiTexture.boundries.get_width() + guiLabel.boundries.get_width());
			num++;
		}
		if (priceE > 0)
		{
			guiTexture1 = new GuiTexture();
			guiTexture1.SetTexture("FrameworkGUI", "res_equilibrium");
			guiTexture1.X = 173f;
			guiTexture1.Y = single;
			guiWindow.AddGuiElement(guiTexture1);
			x = new GuiLabel()
			{
				boundries = new Rect(guiTexture1.X + 20f, single, 100f, guiTexture1.boundries.get_height()),
				text = priceE.ToString("N0"),
				FontSize = 12,
				Alignment = 3,
				TextColor = GuiNewStyleBar.purpleColor,
				Font = GuiLabel.FontBold
			};
			x.boundries.set_width(x.TextWidth);
			guiWindow.AddGuiElement(x);
			_width = _width + (guiTexture1.boundries.get_width() + x.boundries.get_width());
			num++;
		}
		if (priceU > 0)
		{
			guiTexture2 = new GuiTexture();
			guiTexture2.SetTexture("FrameworkGUI", "res_ultralibrium");
			guiTexture2.X = 273f;
			guiTexture2.Y = single + 2f;
			guiWindow.AddGuiElement(guiTexture2);
			guiLabel1 = new GuiLabel()
			{
				boundries = new Rect(guiTexture2.X + 20f, single + 2f, 100f, guiTexture2.boundries.get_height()),
				text = priceU.ToString("N0"),
				FontSize = 12,
				Alignment = 3,
				TextColor = GuiNewStyleBar.greenColor,
				Font = GuiLabel.FontBold
			};
			guiLabel1.boundries.set_width(guiLabel1.TextWidth);
			guiWindow.AddGuiElement(guiLabel1);
			_width = _width + (guiTexture2.boundries.get_width() + guiLabel1.boundries.get_width());
			num++;
		}
		if (priceN > 0)
		{
			guiTexture3 = new GuiTexture();
			guiTexture3.SetTexture("FrameworkGUI", "res_nova");
			guiTexture3.X = 373f;
			guiTexture3.Y = single;
			guiWindow.AddGuiElement(guiTexture3);
			x1 = new GuiLabel()
			{
				boundries = new Rect(guiTexture3.X + 24f, single, 100f, guiTexture3.boundries.get_height()),
				text = priceN.ToString("N0"),
				FontSize = 12,
				Alignment = 3,
				TextColor = GuiNewStyleBar.orangeColor,
				Font = GuiLabel.FontBold
			};
			x1.boundries.set_width(x1.TextWidth);
			guiWindow.AddGuiElement(x1);
			_width = _width + (guiTexture3.boundries.get_width() + x1.boundries.get_width());
			num++;
		}
		float single1 = 490f - _width - (float)((num - 1) * 10) - (float)(num * 4);
		float x2 = 73f + single1 / 2f;
		if (priceC > (long)0)
		{
			guiTexture.X = x2;
			guiLabel.X = guiTexture.X + guiTexture.boundries.get_width() + 4f;
			x2 = guiLabel.X + guiLabel.boundries.get_width() + 10f;
		}
		if (priceE > 0)
		{
			guiTexture1.X = x2;
			x.X = guiTexture1.X + guiTexture1.boundries.get_width() + 4f;
			x2 = x.X + x.boundries.get_width() + 10f;
		}
		if (priceU > 0)
		{
			guiTexture2.X = x2;
			guiLabel1.X = guiTexture2.X + guiTexture2.boundries.get_width() + 4f;
			x2 = guiLabel1.X + guiLabel1.boundries.get_width() + 10f;
		}
		if (priceN > 0)
		{
			guiTexture3.X = x2;
			x1.X = guiTexture3.X + guiTexture3.boundries.get_width() + 4f;
			x2 = x1.X + x1.boundries.get_width() + 10f;
		}
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.SetNewStyleOrangeTexture();
		guiButtonResizeable.X = 77f;
		guiButtonResizeable.Y = 220f;
		guiButtonResizeable.boundries.set_width(175f);
		guiButtonResizeable.Caption = cofirmBtnText;
		guiButtonResizeable.FontSize = 14;
		guiButtonResizeable.Alignment = 4;
		guiButtonResizeable.eventHandlerParam = prm;
		guiButtonResizeable.Clicked = OnConfirm;
		guiButtonResizeable.isEnabled = (NetworkScript.player.playerBelongings.playerItems.get_Cash() < priceC || NetworkScript.player.playerBelongings.playerItems.get_Equilibrium() < (long)priceE || NetworkScript.player.playerBelongings.playerItems.get_Ultralibrium() < (long)priceU ? false : NetworkScript.player.playerBelongings.playerItems.get_Nova() >= (long)priceN);
		guiWindow.AddGuiElement(guiButtonResizeable);
		GuiButtonResizeable action = new GuiButtonResizeable();
		action.SetNewStyleBlueTexture();
		action.X = 374f;
		action.Y = 220f;
		action.boundries.set_width(175f);
		action.Caption = cancelBtnText;
		action.FontSize = 14;
		action.Alignment = 4;
		action.Clicked = new Action<EventHandlerParam>(null, NewPopUpWindow.OnCancelBtnClicked);
		guiWindow.AddGuiElement(action);
	}

	private static void OnCancelBtnClicked(object prm)
	{
		AndromedaGui.gui.RemoveWindow(AndromedaGui.gui.activeToolTipId);
		AndromedaGui.gui.activeToolTipId = -1;
	}
}