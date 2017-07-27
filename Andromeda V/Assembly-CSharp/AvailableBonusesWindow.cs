using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AvailableBonusesWindow : GuiWindow
{
	public AvailableBonusesWindow()
	{
	}

	public override void Create()
	{
		if (NetworkScript.player == null || NetworkScript.player.playerBelongings == null)
		{
			return;
		}
		this.boundries = new Rect(0f, 0f, 500f, 215f);
		base.PutToHorizontalCenter();
		this.boundries.set_y(175f);
		this.zOrder = 210;
		this.isHidden = false;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("FrameworkGUI", "dialogBoxFrame");
		guiTexture.boundries = new Rect(0f, 15f, 500f, 200f);
		base.AddGuiElement(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("AvailableRewardsWindow", "separator");
		guiTexture1.X = 30f;
		guiTexture1.Y = 77f;
		base.AddGuiElement(guiTexture1);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(60f, 50f, 380f, 30f),
			FontSize = 18,
			text = StaticData.Translate("key_available_rewards_window_title"),
			Font = GuiLabel.FontBold,
			Alignment = 4,
			TextColor = GuiNewStyleBar.aquamarineColor
		};
		base.AddGuiElement(guiLabel);
		float single = 87f;
		float single1 = 97f;
		List<GuiElement> list = new List<GuiElement>();
		if (NetworkScript.player.vessel.galaxy.get_galaxyId() != 1000 && NetworkScript.player.vessel.isGuest)
		{
			GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
			guiButtonFixed.SetTexture("AvailableRewardsWindow", "rewardForSignUp");
			guiButtonFixed.Caption = string.Empty;
			guiButtonFixed.X = single;
			guiButtonFixed.Y = single1;
			guiButtonFixed.Clicked = new Action<EventHandlerParam>(null, AvailableBonusesWindow.OnGoToSignUpClicked);
			base.AddGuiElement(guiButtonFixed);
			list.Add(guiButtonFixed);
		}
		if (Enumerable.Count<KeyValuePair<int, PlayerPendingAward>>(NetworkScript.player.playerBelongings.playerAwards) > 0)
		{
			GuiButtonFixed empty = new GuiButtonFixed();
			empty.SetTexture("AvailableRewardsWindow", "rewardPendingReward");
			empty.Caption = string.Empty;
			empty.X = single;
			empty.Y = single1;
			empty.Clicked = new Action<EventHandlerParam>(null, AvailableBonusesWindow.OnGoToPendingRewardClicked);
			base.AddGuiElement(empty);
			list.Add(empty);
		}
		if (!NetworkScript.player.playerBelongings.firstWinBonusRecived && NetworkScript.player.playerBelongings.playerLevel >= 8)
		{
			GuiButtonFixed action = new GuiButtonFixed();
			action.SetTexture("AvailableRewardsWindow", "rewardFirstWin");
			action.Caption = string.Empty;
			action.X = single;
			action.Y = single1;
			action.Clicked = new Action<EventHandlerParam>(null, AvailableBonusesWindow.OnGoToPvPClicked);
			base.AddGuiElement(action);
			list.Add(action);
		}
		float count = (float)((380 - list.get_Count() * 68) / (list.get_Count() + 1));
		int num = 0;
		foreach (GuiElement guiElement in list)
		{
			guiElement.X = 60f + count * (float)(num + 1) + (float)(num * 68);
			num++;
		}
	}

	private static void OnGoToPendingRewardClicked(object prm)
	{
		MainScreenWindow mainScreenWindow = AndromedaGui.mainWnd;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = (byte)17
		};
		mainScreenWindow.OnWindowBtnClicked(eventHandlerParam);
	}

	private static void OnGoToPvPClicked(object prm)
	{
		MainScreenWindow mainScreenWindow = AndromedaGui.mainWnd;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = (byte)15
		};
		mainScreenWindow.OnWindowBtnClicked(eventHandlerParam);
	}

	private static void OnGoToSignUpClicked(object prm)
	{
		MainScreenWindow mainScreenWindow = AndromedaGui.mainWnd;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = (byte)25
		};
		mainScreenWindow.OnWindowBtnClicked(eventHandlerParam);
	}
}