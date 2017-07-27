using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TransferableObjects;
using UnityEngine;

public class TargetingWnd
{
	public static GuiWindow wndTargeting;

	private static GameObjectPhysics target;

	public static int targetSectionIndex;

	private static GuiLabel lblLoot1;

	private static GuiLabel lblLoot2;

	private static GuiLabel lblLoot3;

	private static GuiLabel lblLoot4;

	private static GuiLabel lblLoot1Amount;

	private static GuiLabel lblLoot2Amount;

	private static GuiLabel lblLoot3Amount;

	private static GuiLabel lblLoot4Amount;

	private static GuiLabel lblTargetHull;

	private static GuiLabel lblTargetShield;

	private static GuiNewStyleBar barTargetHull;

	private static GuiNewStyleBar barTargetShield;

	public static GuiButtonFixed btnInvite;

	public static GuiButtonFixed btnClose;

	public static GuiButtonFixed btnProfile;

	public static GuiButtonFixed btnChat;

	public static GuiButtonFixed btnStopFire;

	public static GuiButtonFixed btnGoToMenu;

	public static byte goToMenuState;

	private static GuiLabel pileName;

	private static GuiLabel ownerLbl;

	private static GuiLabel ownerName;

	private static GuiLabel pileAmount;

	private static GuiLabel pilePrice;

	private static GuiLabel pileDescription;

	private static GuiTexture targetAvatar;

	private static GuiTexture fractionIcon;

	private static GuiTexture pvePresetOne;

	private static GuiTexture pvePresetTwo;

	private static GuiTexture pvePresetThree;

	private static GuiTexture pvePresetFour;

	private static GuiLabel pvePressetOnePower;

	private static GuiLabel pvePressetTwoPower;

	private static GuiLabel pvePressetThreePower;

	private static GuiLabel pvePressetFourPower;

	private static GuiTexture itemGlow;

	private static GuiTexture targetPvP_frame;

	private static GuiTexture targetPvP_xp_frame;

	private static GuiTexture targetPoP_shooting_indication;

	private static GuiTexture targetPvP_avatar;

	private static GuiTexture targetPvP_fraction;

	private static GuiTexture targetPvP_barCorpus;

	private static GuiTexture targetPvP_barShield;

	private static GuiLabel targetPvP_playerName;

	private static GuiLabel targetPvP_playerLevel;

	private static GuiLabel targetPvP_xp;

	private static GuiLabel targetPvP_corpusValue;

	private static GuiLabel targetPvP_corpusValueShadow;

	private static GuiLabel targetPvP_shieldValue;

	private static GuiLabel targetPvP_shieldValueShadow;

	private static GuiButton targetPoP_actionBtn;

	private static GuiWindow targetPoP_option_window;

	private static GuiTexture targetOfTaret_fame;

	private static GuiTexture targetOfTaret_avatar;

	private static GuiTexture targetOfTaret_fraction;

	private static GuiTexture targetOfTaret_barCorpus;

	private static GuiTexture targetOfTaret_barShield;

	private static GuiLabel targetOfTaret_name;

	private static GuiLabel targetOfTaret_lavel;

	private static GuiButton targetOfTaret_selectBtn;

	private static GuiWindow targetOfTaret_window;

	private static GuiButtonFixed stopShoot;

	private static GuiButtonFixed startChat;

	private static GuiButtonFixed viewProfile;

	private static GuiButtonFixed inviteToParty;

	private static GuiTexture stopShootIcon;

	private static GuiTexture startChatIcon;

	private static GuiTexture viewProfileIcon;

	private static GuiTexture inviteToPartyIcon;

	private static uint optionMenuTargetNbId;

	private static string currentTargetAvatar;

	private static PlayerObjectPhysics targetOfTarget;

	private static GuiSecondsTracker timer;

	private static bool IsGoodToChat
	{
		get
		{
			EventHandlerParam eventHandlerParam;
			if (((PlayerObjectPhysics)NetworkScript.player.shipScript.selectedObject).playerId == NetworkScript.player.playId)
			{
				GuiButtonFixed guiButtonFixed = TargetingWnd.btnChat;
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_profile_screen_tooltips_no_chat"),
					customData2 = TargetingWnd.btnChat
				};
				guiButtonFixed.tooltipWindowParam = eventHandlerParam;
				return false;
			}
			GuiButtonFixed guiButtonFixed1 = TargetingWnd.btnChat;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_targeting_btn_start_chat_tooltip"),
				customData2 = TargetingWnd.btnChat
			};
			guiButtonFixed1.tooltipWindowParam = eventHandlerParam;
			return true;
		}
	}

	public static bool IsTargetingWindowAllowed
	{
		get
		{
			return (TargetingWnd.targetSectionIndex == 6 ? false : AndromedaGui.galaxyJumpWnd == null);
		}
	}

	static TargetingWnd()
	{
	}

	public TargetingWnd()
	{
		TargetingWnd.currentTargetAvatar = string.Empty;
	}

	private static void AddInviteToPartyBtn(GuiWindow wnd)
	{
		if (TargetingWnd.btnInvite != null)
		{
			wnd.RemoveGuiElement(TargetingWnd.btnInvite);
		}
		TargetingWnd.btnInvite = new GuiButtonFixed()
		{
			X = 335f,
			Y = 40f
		};
		TargetingWnd.btnInvite.SetTexture("MainScreenWindow", "targeting_party");
		TargetingWnd.btnInvite.Caption = string.Empty;
		TargetingWnd.btnInvite.FontSize = 8;
		GuiButtonFixed guiButtonFixed = TargetingWnd.btnInvite;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_targeting_btn_party_invite"),
			customData2 = TargetingWnd.btnInvite
		};
		guiButtonFixed.tooltipWindowParam = eventHandlerParam;
		TargetingWnd.btnInvite.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		wnd.AddGuiElement(TargetingWnd.btnInvite);
	}

	private static void ChooseMieralDetailsToPopulate(Mineral mineral)
	{
		if (mineral.items != null && mineral.items.get_Count() != 0)
		{
			TargetingWnd.PopulateItem();
		}
		else if (mineral.resourceQuantities.get_Count() != 1)
		{
			TargetingWnd.PopulateLoot();
		}
		else
		{
			TargetingWnd.PopulateMineral();
		}
	}

	public static void CreateCountdownWindow(int delta)
	{
		if (TargetingWnd.targetSectionIndex != 0)
		{
			TargetingWnd.Remove();
			TargetingWnd.RemoveTargetOfTarget();
		}
		TargetingWnd.wndTargeting = new GuiWindow()
		{
			boundries = TargetingWnd.SetTartetingWindowPossition()
		};
		TargetingWnd.wndTargeting.SetBackgroundTexture("NewGUI", "pvp_targeting_frame");
		TargetingWnd.wndTargeting.zOrder = 200;
		PvPGameType[] pvPGameTypeArray = StaticData.pvpGameTypes;
		if (TargetingWnd.<>f__am$cache4C == null)
		{
			TargetingWnd.<>f__am$cache4C = new Func<PvPGameType, bool>(null, (PvPGameType p) => p.id == NetworkScript.player.pvpGameTypeSignedFor);
		}
		PvPGameType pvPGameType = Enumerable.First<PvPGameType>(Enumerable.Where<PvPGameType>(pvPGameTypeArray, TargetingWnd.<>f__am$cache4C));
		string empty = string.Empty;
		switch (pvPGameType.mode)
		{
			case 1:
			{
				empty = StaticData.Translate("key_pvp_1vs1");
				break;
			}
			case 2:
			{
				empty = StaticData.Translate("key_pvp_2vs2");
				break;
			}
			case 3:
			{
				empty = StaticData.Translate("key_pvp_3vs3");
				break;
			}
			case 4:
			{
				empty = StaticData.Translate("key_pvp_4vs4");
				break;
			}
			case 5:
			{
				empty = StaticData.Translate("key_pvp_ffa");
				break;
			}
		}
		string str = string.Format(StaticData.Translate("key_targeting_pvp_start_countdown"), empty, StaticData.Translate(pvPGameType.name));
		TargetingWnd.lblLoot1 = new GuiLabel()
		{
			boundries = new Rect(19f, 19f, 310f, 42f),
			text = str,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			Alignment = 5,
			TextColor = GuiNewStyleBar.orangeColor
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.lblLoot1);
		TargetingWnd.targetAvatar = new GuiTexture();
		TargetingWnd.targetAvatar.SetTexture("NewGUI", "pvp_targeting_clock_active");
		TargetingWnd.targetAvatar.X = 368f;
		TargetingWnd.targetAvatar.Y = 29f;
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetAvatar);
		TargetingWnd.timer = new GuiSecondsTracker(delta, TargetingWnd.wndTargeting);
		TargetingWnd.timer.SetEndAction(new Action(null, TargetingWnd.OnTimeOff));
		TargetingWnd.timer.boundries = new Rect(395f, 27f, 32f, 26f);
		TargetingWnd.timer.Font = GuiLabel.FontBold;
		TargetingWnd.timer.FontSize = 22;
		AndromedaGui.gui.AddWindow(TargetingWnd.wndTargeting);
		TargetingWnd.wndTargeting.isHidden = false;
		TargetingWnd.targetSectionIndex = 6;
	}

	private static void CreateExtractionPointTarger()
	{
		TargetingWnd.wndTargeting = new GuiWindow()
		{
			boundries = TargetingWnd.SetTartetingWindowPossition()
		};
		TargetingWnd.wndTargeting.SetBackgroundTexture("MainScreenWindow", "targeting_frame");
		TargetingWnd.wndTargeting.zOrder = 200;
		TargetingWnd.btnClose = new GuiButtonFixed()
		{
			X = 443f,
			Y = 29f
		};
		TargetingWnd.btnClose.SetTexture("MainScreenWindow", "targeting_close");
		TargetingWnd.btnClose.Caption = string.Empty;
		TargetingWnd.btnClose.Clicked = new Action<EventHandlerParam>(null, TargetingWnd.OnCloseBtnClick);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.btnClose);
		TargetingWnd.targetAvatar = new GuiTexture();
		TargetingWnd.targetAvatar.boundries.set_x(10f);
		TargetingWnd.targetAvatar.boundries.set_y(15f);
		TargetingWnd.targetAvatar.SetSize(58f, 58f);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetAvatar);
		TargetingWnd.fractionIcon = new GuiTexture();
		TargetingWnd.fractionIcon.boundries.set_x(78f);
		TargetingWnd.fractionIcon.boundries.set_y(12f);
		TargetingWnd.fractionIcon.SetSize(27f, 18f);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.fractionIcon);
		TargetingWnd.pileName = new GuiLabel()
		{
			boundries = new Rect(109f, 8f, 330f, 26f),
			Alignment = 3,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.pileName);
		TargetingWnd.pileAmount = new GuiLabel()
		{
			boundries = new Rect(109f, 8f, 330f, 26f),
			Alignment = 5,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.pileAmount);
		TargetingWnd.ownerName = new GuiLabel()
		{
			boundries = new Rect(222f, 40f, 180f, 48f),
			Alignment = 4,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.ownerName);
		TargetingWnd.barTargetHull = new GuiNewStyleBar();
		TargetingWnd.barTargetHull.SetCustumSizeBlueBar(140);
		TargetingWnd.barTargetHull.current = 0f;
		TargetingWnd.barTargetHull.boundries.set_x(79f);
		TargetingWnd.barTargetHull.boundries.set_y(50f);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.barTargetHull);
		TargetingWnd.lblTargetHull = new GuiLabel()
		{
			boundries = new Rect(TargetingWnd.barTargetHull.X, TargetingWnd.barTargetHull.Y - 14f, 140f, 14f),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			text = string.Empty
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.lblTargetHull);
		AndromedaGui.gui.AddWindow(TargetingWnd.wndTargeting);
		TargetingWnd.wndTargeting.isHidden = false;
		TargetingWnd.targetSectionIndex = 7;
	}

	private static void CreateItemTarget()
	{
		TargetingWnd.wndTargeting = new GuiWindow()
		{
			boundries = TargetingWnd.SetTartetingWindowPossition()
		};
		TargetingWnd.wndTargeting.SetBackgroundTexture("MainScreenWindow", "targeting_frame");
		TargetingWnd.wndTargeting.zOrder = 200;
		TargetingWnd.btnClose = new GuiButtonFixed()
		{
			X = 443f,
			Y = 29f
		};
		TargetingWnd.btnClose.SetTexture("MainScreenWindow", "targeting_close");
		TargetingWnd.btnClose.Caption = string.Empty;
		TargetingWnd.btnClose.Clicked = new Action<EventHandlerParam>(null, TargetingWnd.OnCloseBtnClick);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.btnClose);
		TargetingWnd.itemGlow = new GuiTexture();
		TargetingWnd.itemGlow.boundries.set_x(11f);
		TargetingWnd.itemGlow.boundries.set_y(15f);
		TargetingWnd.itemGlow.SetSize(56f, 58f);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.itemGlow);
		TargetingWnd.targetAvatar = new GuiTexture();
		TargetingWnd.targetAvatar.boundries.set_x(10f);
		TargetingWnd.targetAvatar.boundries.set_y(24f);
		TargetingWnd.targetAvatar.SetSize(58f, 40f);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetAvatar);
		TargetingWnd.pileName = new GuiLabel()
		{
			boundries = new Rect(79f, 8f, 360f, 12f),
			Alignment = 3,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.pileName);
		TargetingWnd.ownerLbl = new GuiLabel()
		{
			boundries = new Rect(79f, 22f, 360f, 12f),
			Alignment = 3,
			text = StaticData.Translate("key_owner_lbl"),
			Font = GuiLabel.FontBold,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.ownerLbl);
		TargetingWnd.ownerName = new GuiLabel()
		{
			boundries = new Rect(79f + TargetingWnd.ownerLbl.TextWidth + 3f, 22f, 360f - TargetingWnd.ownerLbl.TextWidth - 3f, 12f),
			Alignment = 3,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.ownerName);
		TargetingWnd.pileAmount = new GuiLabel()
		{
			boundries = new Rect(79f, 8f, 360f, 12f),
			Alignment = 5,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.pileAmount);
		TargetingWnd.pilePrice = new GuiLabel()
		{
			boundries = new Rect(79f, 22f, 360f, 12f),
			Alignment = 5,
			text = string.Empty,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.pilePrice);
		TargetingWnd.pileDescription = new GuiLabel()
		{
			boundries = new Rect(79f, 36f, 360f, 50f),
			text = string.Empty,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 3
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.pileDescription);
		AndromedaGui.gui.AddWindow(TargetingWnd.wndTargeting);
		TargetingWnd.wndTargeting.isHidden = false;
		TargetingWnd.targetSectionIndex = 5;
	}

	private static void CreateLootTarget()
	{
		TargetingWnd.wndTargeting = new GuiWindow()
		{
			boundries = TargetingWnd.SetTartetingWindowPossition()
		};
		TargetingWnd.wndTargeting.SetBackgroundTexture("MainScreenWindow", "targeting_frame");
		TargetingWnd.wndTargeting.zOrder = 200;
		TargetingWnd.btnClose = new GuiButtonFixed()
		{
			X = 443f,
			Y = 29f
		};
		TargetingWnd.btnClose.SetTexture("MainScreenWindow", "targeting_close");
		TargetingWnd.btnClose.Caption = string.Empty;
		TargetingWnd.btnClose.Clicked = new Action<EventHandlerParam>(null, TargetingWnd.OnCloseBtnClick);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.btnClose);
		TargetingWnd.targetAvatar = new GuiTexture();
		TargetingWnd.targetAvatar.boundries.set_x(10f);
		TargetingWnd.targetAvatar.boundries.set_y(15f);
		TargetingWnd.targetAvatar.SetSize(58f, 58f);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetAvatar);
		TargetingWnd.pileName = new GuiLabel()
		{
			boundries = new Rect(79f, 8f, 360f, 12f),
			Alignment = 3,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.pileName);
		TargetingWnd.pileAmount = new GuiLabel()
		{
			boundries = new Rect(79f, 8f, 360f, 12f),
			Alignment = 5,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.pileAmount);
		TargetingWnd.ownerLbl = new GuiLabel()
		{
			boundries = new Rect(79f, 22f, 360f, 12f),
			Alignment = 3,
			text = StaticData.Translate("key_owner_lbl"),
			Font = GuiLabel.FontBold,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.ownerLbl);
		TargetingWnd.ownerName = new GuiLabel()
		{
			boundries = new Rect(79f + TargetingWnd.ownerLbl.TextWidth + 3f, 22f, 360f - TargetingWnd.ownerLbl.TextWidth - 3f, 12f),
			Alignment = 3,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.ownerName);
		TargetingWnd.pilePrice = new GuiLabel()
		{
			boundries = new Rect(79f, 22f, 360f, 12f),
			Alignment = 5,
			text = string.Empty,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.pilePrice);
		TargetingWnd.lblLoot1 = new GuiLabel()
		{
			boundries = new Rect(79f, 38f, 175f, 16f),
			Alignment = 3,
			text = string.Empty,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.lblLoot1);
		TargetingWnd.lblLoot1Amount = new GuiLabel()
		{
			boundries = new Rect(79f, 38f, 175f, 16f),
			Alignment = 3,
			text = string.Empty,
			FontSize = 12,
			TextColor = GuiNewStyleBar.orangeColor
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.lblLoot1Amount);
		TargetingWnd.lblLoot2 = new GuiLabel()
		{
			boundries = new Rect(79f, 56f, 175f, 16f),
			Alignment = 3,
			text = string.Empty,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.lblLoot2);
		TargetingWnd.lblLoot2Amount = new GuiLabel()
		{
			boundries = new Rect(79f, 56f, 175f, 16f),
			Alignment = 3,
			text = string.Empty,
			FontSize = 12,
			TextColor = GuiNewStyleBar.orangeColor
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.lblLoot2Amount);
		TargetingWnd.lblLoot3 = new GuiLabel()
		{
			boundries = new Rect(264f, 38f, 175f, 16f),
			Alignment = 3,
			text = string.Empty,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.lblLoot3);
		TargetingWnd.lblLoot3Amount = new GuiLabel()
		{
			boundries = new Rect(264f, 38f, 175f, 16f),
			Alignment = 3,
			text = string.Empty,
			FontSize = 12,
			TextColor = GuiNewStyleBar.orangeColor
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.lblLoot3Amount);
		TargetingWnd.lblLoot4 = new GuiLabel()
		{
			boundries = new Rect(264f, 56f, 175f, 16f),
			Alignment = 3,
			text = string.Empty,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.lblLoot4);
		TargetingWnd.lblLoot4Amount = new GuiLabel()
		{
			boundries = new Rect(264f, 56f, 175f, 16f),
			Alignment = 3,
			text = string.Empty,
			FontSize = 12,
			TextColor = GuiNewStyleBar.orangeColor
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.lblLoot4Amount);
		AndromedaGui.gui.AddWindow(TargetingWnd.wndTargeting);
		TargetingWnd.wndTargeting.isHidden = false;
		TargetingWnd.targetSectionIndex = 4;
	}

	private static void CreateMineralTarget()
	{
		TargetingWnd.wndTargeting = new GuiWindow()
		{
			boundries = TargetingWnd.SetTartetingWindowPossition()
		};
		TargetingWnd.wndTargeting.SetBackgroundTexture("MainScreenWindow", "targeting_frame");
		TargetingWnd.wndTargeting.zOrder = 200;
		TargetingWnd.btnClose = new GuiButtonFixed()
		{
			X = 443f,
			Y = 29f
		};
		TargetingWnd.btnClose.SetTexture("MainScreenWindow", "targeting_close");
		TargetingWnd.btnClose.Caption = string.Empty;
		TargetingWnd.btnClose.Clicked = new Action<EventHandlerParam>(null, TargetingWnd.OnCloseBtnClick);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.btnClose);
		TargetingWnd.targetAvatar = new GuiTexture();
		TargetingWnd.targetAvatar.boundries.set_x(10f);
		TargetingWnd.targetAvatar.boundries.set_y(15f);
		TargetingWnd.targetAvatar.SetSize(58f, 58f);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetAvatar);
		TargetingWnd.pileName = new GuiLabel()
		{
			boundries = new Rect(79f, 8f, 360f, 12f),
			Alignment = 3,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.pileName);
		TargetingWnd.pileAmount = new GuiLabel()
		{
			boundries = new Rect(79f, 8f, 360f, 12f),
			Alignment = 5,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.pileAmount);
		TargetingWnd.ownerLbl = new GuiLabel()
		{
			boundries = new Rect(79f, 22f, 360f, 12f),
			Alignment = 3,
			text = StaticData.Translate("key_owner_lbl"),
			Font = GuiLabel.FontBold,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.ownerLbl);
		TargetingWnd.ownerName = new GuiLabel()
		{
			boundries = new Rect(79f + TargetingWnd.ownerLbl.TextWidth + 3f, 22f, 360f - TargetingWnd.ownerLbl.TextWidth - 3f, 12f),
			Alignment = 3,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.ownerName);
		TargetingWnd.pilePrice = new GuiLabel()
		{
			boundries = new Rect(79f, 22f, 360f, 12f),
			Alignment = 5,
			text = string.Empty,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.pilePrice);
		TargetingWnd.pileDescription = new GuiLabel()
		{
			boundries = new Rect(79f, 36f, 360f, 50f),
			text = string.Empty,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 3
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.pileDescription);
		AndromedaGui.gui.AddWindow(TargetingWnd.wndTargeting);
		TargetingWnd.wndTargeting.isHidden = false;
		TargetingWnd.targetSectionIndex = 3;
	}

	private static void CreateNewPlayerTarget()
	{
		TargetingWnd.wndTargeting = new GuiWindow();
		TargetingWnd.targetSectionIndex = 1;
		TargetingWnd.wndTargeting.boundries = TargetingWnd.SetPoPTartetingWindowPossition();
		TargetingWnd.wndTargeting.zOrder = 200;
		TargetingWnd.targetPvP_barShield = new GuiTexture();
		TargetingWnd.targetPvP_barShield.SetTexture("TargetingGui", "targetPoPBlueBar");
		TargetingWnd.targetPvP_barShield.boundries = new Rect(91f, 29f, 156f, 14f);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPvP_barShield);
		TargetingWnd.targetPvP_barCorpus = new GuiTexture();
		TargetingWnd.targetPvP_barCorpus.SetTexture("TargetingGui", "targetPoPGreenBar");
		TargetingWnd.targetPvP_barCorpus.boundries = new Rect(91f, 43f, 108f, 7f);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPvP_barCorpus);
		TargetingWnd.targetPvP_frame = new GuiTexture();
		TargetingWnd.targetPvP_frame.SetTexture("TargetingGui", "targetPoPFrame");
		TargetingWnd.targetPvP_frame.boundries = new Rect(0f, 0f, 290f, 71f);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPvP_frame);
		TargetingWnd.btnClose = new GuiButtonFixed()
		{
			X = 256f,
			Y = 1f
		};
		TargetingWnd.btnClose.SetTexture("TargetingGui", "closeTarget");
		TargetingWnd.btnClose.Caption = string.Empty;
		TargetingWnd.btnClose.Clicked = new Action<EventHandlerParam>(null, TargetingWnd.OnCloseBtnClick);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.btnClose);
		TargetingWnd.targetPvP_avatar = new GuiTexture()
		{
			boundries = new Rect(27f, 7f, 56f, 56f)
		};
		TargetingWnd.targetPvP_avatar.SetTextureKeepSize("FrameworkGUI", "empty");
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPvP_avatar);
		TargetingWnd.targetPoP_shooting_indication = new GuiTexture()
		{
			boundries = new Rect(26f, 30f, 34f, 34f)
		};
		TargetingWnd.targetPoP_shooting_indication.SetTextureKeepSize("FrameworkGUI", "empty");
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPoP_shooting_indication);
		TargetingWnd.targetPvP_fraction = new GuiTexture();
		TargetingWnd.targetPvP_fraction.SetTexture("FrameworkGUI", "fraction0Icon");
		TargetingWnd.targetPvP_fraction.boundries = new Rect(5f, 30f, 18f, 12f);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPvP_fraction);
		TargetingWnd.targetPvP_playerName = new GuiLabel()
		{
			boundries = new Rect(90f, 7f, 164f, 16f),
			Alignment = 3,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPvP_playerName);
		TargetingWnd.targetPvP_playerLevel = new GuiLabel()
		{
			boundries = new Rect(7f, 11f, 14f, 14f),
			Alignment = 4,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPvP_playerLevel);
		TargetingWnd.targetPvP_shieldValueShadow = new GuiLabel()
		{
			boundries = new Rect(95f, 28f, 108f, 10f),
			Alignment = 3,
			text = string.Empty,
			FontSize = 10,
			TextColor = Color.get_black()
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPvP_shieldValueShadow);
		TargetingWnd.targetPvP_corpusValueShadow = new GuiLabel()
		{
			boundries = new Rect(95f, 42f, 108f, 10f),
			Alignment = 3,
			text = string.Empty,
			FontSize = 10,
			TextColor = Color.get_black()
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPvP_corpusValueShadow);
		TargetingWnd.targetPvP_shieldValue = new GuiLabel()
		{
			boundries = new Rect(94f, 27f, 108f, 10f),
			Alignment = 3,
			text = string.Empty,
			FontSize = 10
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPvP_shieldValue);
		TargetingWnd.targetPvP_corpusValue = new GuiLabel()
		{
			boundries = new Rect(94f, 41f, 108f, 10f),
			Alignment = 3,
			text = string.Empty,
			FontSize = 10
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPvP_corpusValue);
		TargetingWnd.targetPoP_actionBtn = new GuiButton()
		{
			boundries = new Rect(0f, 0f, 255f, 71f),
			Caption = string.Empty,
			Clicked = new Action<EventHandlerParam>(null, TargetingWnd.OnOptionMenuClick)
		};
		TargetingWnd.targetPoP_actionBtn.eventHandlerParam.customData = null;
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPoP_actionBtn);
		AndromedaGui.gui.AddWindow(TargetingWnd.wndTargeting);
		TargetingWnd.wndTargeting.isHidden = false;
	}

	private static void CreateNewPvETarget()
	{
		TargetingWnd.wndTargeting = new GuiWindow();
		TargetingWnd.targetSectionIndex = 2;
		TargetingWnd.wndTargeting.boundries = TargetingWnd.SetPoPTartetingWindowPossition();
		TargetingWnd.wndTargeting.zOrder = 200;
		float single = 70f;
		TargetingWnd.targetPvP_barShield = new GuiTexture();
		TargetingWnd.targetPvP_barShield.SetTexture("TargetingGui", "targetPoPBlueBar");
		TargetingWnd.targetPvP_barShield.boundries = new Rect(single + 91f, 29f, 156f, 14f);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPvP_barShield);
		TargetingWnd.targetPvP_barCorpus = new GuiTexture();
		TargetingWnd.targetPvP_barCorpus.SetTexture("TargetingGui", "targetPoPGreenBar");
		TargetingWnd.targetPvP_barCorpus.boundries = new Rect(single + 91f, 43f, 108f, 7f);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPvP_barCorpus);
		TargetingWnd.targetPvP_xp_frame = new GuiTexture();
		TargetingWnd.targetPvP_xp_frame.SetTexture("TargetingGui", "targetPoPFrameExp");
		TargetingWnd.targetPvP_xp_frame.boundries = new Rect(single + 85f, 57f, 103f, 14f);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPvP_xp_frame);
		TargetingWnd.targetPvP_frame = new GuiTexture();
		TargetingWnd.targetPvP_frame.SetTexture("TargetingGui", "targetPoPFrame");
		TargetingWnd.targetPvP_frame.boundries = new Rect(single, 0f, 290f, 71f);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPvP_frame);
		TargetingWnd.btnClose = new GuiButtonFixed()
		{
			X = single + 256f,
			Y = 1f
		};
		TargetingWnd.btnClose.SetTexture("TargetingGui", "closeTarget");
		TargetingWnd.btnClose.Caption = string.Empty;
		TargetingWnd.btnClose.Clicked = new Action<EventHandlerParam>(null, TargetingWnd.OnCloseBtnClick);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.btnClose);
		TargetingWnd.targetPvP_avatar = new GuiTexture()
		{
			boundries = new Rect(single + 27f, 7f, 56f, 56f)
		};
		TargetingWnd.targetPvP_avatar.SetTextureKeepSize("FrameworkGUI", "empty");
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPvP_avatar);
		TargetingWnd.targetPoP_shooting_indication = new GuiTexture()
		{
			boundries = new Rect(single + 26f, 30f, 34f, 34f)
		};
		TargetingWnd.targetPoP_shooting_indication.SetTextureKeepSize("FrameworkGUI", "empty");
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPoP_shooting_indication);
		TargetingWnd.targetPvP_fraction = new GuiTexture();
		TargetingWnd.targetPvP_fraction.SetTexture("FrameworkGUI", "fraction0Icon");
		TargetingWnd.targetPvP_fraction.boundries = new Rect(single + 5f, 30f, 18f, 12f);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPvP_fraction);
		TargetingWnd.targetPvP_playerName = new GuiLabel()
		{
			boundries = new Rect(single + 90f, 7f, 164f, 16f),
			Alignment = 3,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPvP_playerName);
		TargetingWnd.targetPvP_playerLevel = new GuiLabel()
		{
			boundries = new Rect(single + 7f, 11f, 14f, 14f),
			Alignment = 4,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPvP_playerLevel);
		TargetingWnd.targetPvP_xp = new GuiLabel()
		{
			boundries = new Rect(single + 95f, 57f, 80f, 10f),
			Alignment = 3,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 10
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPvP_xp);
		TargetingWnd.targetPvP_shieldValueShadow = new GuiLabel()
		{
			boundries = new Rect(single + 95f, 28f, 108f, 10f),
			Alignment = 3,
			text = string.Empty,
			FontSize = 10,
			TextColor = Color.get_black()
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPvP_shieldValueShadow);
		TargetingWnd.targetPvP_corpusValueShadow = new GuiLabel()
		{
			boundries = new Rect(single + 95f, 42f, 108f, 10f),
			Alignment = 3,
			text = string.Empty,
			FontSize = 10,
			TextColor = Color.get_black()
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPvP_corpusValueShadow);
		TargetingWnd.targetPvP_shieldValue = new GuiLabel()
		{
			boundries = new Rect(single + 94f, 27f, 108f, 10f),
			Alignment = 3,
			text = string.Empty,
			FontSize = 10
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPvP_shieldValue);
		TargetingWnd.targetPvP_corpusValue = new GuiLabel()
		{
			boundries = new Rect(single + 94f, 41f, 108f, 10f),
			Alignment = 3,
			text = string.Empty,
			FontSize = 10
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPvP_corpusValue);
		TargetingWnd.CreatePressetIcons(TargetingWnd.wndTargeting);
		TargetingWnd.targetPoP_actionBtn = new GuiButton()
		{
			boundries = new Rect(0f, 0f, 325f, 71f),
			Caption = string.Empty,
			Clicked = new Action<EventHandlerParam>(null, TargetingWnd.OnOptionMenuClick)
		};
		TargetingWnd.targetPoP_actionBtn.eventHandlerParam.customData = null;
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetPoP_actionBtn);
		AndromedaGui.gui.AddWindow(TargetingWnd.wndTargeting);
		TargetingWnd.wndTargeting.isHidden = false;
	}

	private static void CreateOptionMenu(PlayerObjectPhysics pop)
	{
		EventHandlerParam eventHandlerParam;
		bool flag = false;
		bool flag1 = false;
		bool flag2 = false;
		bool flag3 = false;
		if (pop == null)
		{
			return;
		}
		TargetingWnd.optionMenuTargetNbId = pop.neighbourhoodId;
		flag = (!NetworkScript.player.vessel.isShooting || NetworkScript.player.vessel.shootingAt == null || NetworkScript.player.vessel.shootingAt.neighbourhoodId != pop.neighbourhoodId ? false : true);
		if (!pop.get_IsPve())
		{
			flag1 = true;
			flag2 = true;
			flag3 = true;
		}
		byte num = 0;
		if (flag)
		{
			num = (byte)(num + 1);
		}
		if (flag1)
		{
			num = (byte)(num + 1);
		}
		if (flag2)
		{
			num = (byte)(num + 1);
		}
		if (flag3)
		{
			num = (byte)(num + 1);
		}
		if (num == 0)
		{
			return;
		}
		TargetingWnd.targetPoP_option_window = new GuiWindow()
		{
			boundries = TargetingWnd.SetOptionMenuWindowPossition((int)num),
			zOrder = 199
		};
		if (flag)
		{
			TargetingWnd.stopShoot = new GuiButtonFixed();
			TargetingWnd.stopShoot.SetTexture("TargetingGui", "optionsButton");
			TargetingWnd.stopShoot.X = 0f;
			TargetingWnd.stopShoot.Y = 0f;
			TargetingWnd.stopShoot.Clicked = new Action<EventHandlerParam>(null, TargetingWnd.OnStopFireBtnClicked);
			GuiButtonFixed guiButtonFixed = TargetingWnd.stopShoot;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_targeting_stop_fire"),
				customData2 = TargetingWnd.stopShoot
			};
			guiButtonFixed.tooltipWindowParam = eventHandlerParam;
			TargetingWnd.stopShoot.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			TargetingWnd.stopShoot.Caption = string.Empty;
			TargetingWnd.targetPoP_option_window.AddGuiElement(TargetingWnd.stopShoot);
			TargetingWnd.stopShootIcon = new GuiTexture();
			TargetingWnd.stopShootIcon.SetTexture("TargetingGui", "btnStopFire");
			TargetingWnd.stopShootIcon.X = 10f;
			TargetingWnd.stopShootIcon.Y = 14f;
			TargetingWnd.targetPoP_option_window.AddGuiElement(TargetingWnd.stopShootIcon);
		}
		if (flag1)
		{
			TargetingWnd.viewProfile = new GuiButtonFixed();
			TargetingWnd.viewProfile.SetTexture("TargetingGui", "optionsButton");
			TargetingWnd.viewProfile.X = 0f;
			TargetingWnd.viewProfile.Y = (float)((!flag ? 0 : 40));
			TargetingWnd.viewProfile.Caption = string.Empty;
			GuiButtonFixed guiButtonFixed1 = TargetingWnd.viewProfile;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_targeting_btn_profile_tooltip"),
				customData2 = TargetingWnd.viewProfile
			};
			guiButtonFixed1.tooltipWindowParam = eventHandlerParam;
			TargetingWnd.viewProfile.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			GuiButtonFixed guiButtonFixed2 = TargetingWnd.viewProfile;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = pop.playerName
			};
			guiButtonFixed2.eventHandlerParam = eventHandlerParam;
			TargetingWnd.viewProfile.Clicked = new Action<EventHandlerParam>(null, TargetingWnd.OnProfilBtnClick);
			TargetingWnd.targetPoP_option_window.AddGuiElement(TargetingWnd.viewProfile);
			TargetingWnd.viewProfileIcon = new GuiTexture();
			TargetingWnd.viewProfileIcon.SetTexture("TargetingGui", "btnProfile");
			TargetingWnd.viewProfileIcon.X = 10f;
			TargetingWnd.viewProfileIcon.Y = TargetingWnd.viewProfile.Y + 14f;
			TargetingWnd.targetPoP_option_window.AddGuiElement(TargetingWnd.viewProfileIcon);
		}
		if (flag2)
		{
			TargetingWnd.startChat = new GuiButtonFixed();
			TargetingWnd.startChat.SetTexture("TargetingGui", "optionsButton");
			TargetingWnd.startChat.X = 0f;
			TargetingWnd.startChat.Y = (float)((!flag ? 40 : 80));
			TargetingWnd.startChat.Caption = string.Empty;
			TargetingWnd.startChat.isEnabled = pop.playerId != NetworkScript.player.playId;
			GuiButtonFixed guiButtonFixed3 = TargetingWnd.startChat;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = pop.playerId
			};
			guiButtonFixed3.eventHandlerParam = eventHandlerParam;
			TargetingWnd.startChat.Clicked = new Action<EventHandlerParam>(null, TargetingWnd.OnChatBtnClick);
			GuiButtonFixed guiButtonFixed4 = TargetingWnd.startChat;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_targeting_btn_start_chat_tooltip"),
				customData2 = TargetingWnd.startChat
			};
			guiButtonFixed4.tooltipWindowParam = eventHandlerParam;
			TargetingWnd.startChat.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			TargetingWnd.targetPoP_option_window.AddGuiElement(TargetingWnd.startChat);
			TargetingWnd.startChatIcon = new GuiTexture();
			TargetingWnd.startChatIcon.SetTexture("TargetingGui", "btnChat");
			TargetingWnd.startChatIcon.X = 10f;
			TargetingWnd.startChatIcon.Y = TargetingWnd.startChat.Y + 14f;
			TargetingWnd.targetPoP_option_window.AddGuiElement(TargetingWnd.startChatIcon);
		}
		if (flag3)
		{
			TargetingWnd.inviteToParty = new GuiButtonFixed();
			TargetingWnd.inviteToParty.SetTexture("TargetingGui", "optionsButton");
			TargetingWnd.inviteToParty.X = 0f;
			TargetingWnd.inviteToParty.Y = (float)((!flag ? 80 : 120));
			TargetingWnd.inviteToParty.Caption = string.Empty;
			TargetingWnd.inviteToParty.isEnabled = TargetingWnd.IsGoodToInvite(pop, TargetingWnd.inviteToParty);
			GuiButtonFixed guiButtonFixed5 = TargetingWnd.inviteToParty;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = pop.playerId
			};
			guiButtonFixed5.eventHandlerParam = eventHandlerParam;
			TargetingWnd.inviteToParty.Clicked = new Action<EventHandlerParam>(null, NetworkScript.OnPartyInviteClicked);
			GuiButtonFixed guiButtonFixed6 = TargetingWnd.inviteToParty;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_targeting_btn_party_invite"),
				customData2 = TargetingWnd.inviteToParty
			};
			guiButtonFixed6.tooltipWindowParam = eventHandlerParam;
			TargetingWnd.inviteToParty.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			TargetingWnd.targetPoP_option_window.AddGuiElement(TargetingWnd.inviteToParty);
			TargetingWnd.inviteToPartyIcon = new GuiTexture();
			TargetingWnd.inviteToPartyIcon.SetTexture("TargetingGui", "btnParty");
			TargetingWnd.inviteToPartyIcon.X = 10f;
			TargetingWnd.inviteToPartyIcon.Y = TargetingWnd.inviteToParty.Y + 14f;
			TargetingWnd.targetPoP_option_window.AddGuiElement(TargetingWnd.inviteToPartyIcon);
		}
		AndromedaGui.gui.AddWindow(TargetingWnd.targetPoP_option_window);
		TargetingWnd.targetPoP_option_window.isHidden = false;
	}

	private static void CreatePlayerTarget()
	{
		TargetingWnd.wndTargeting = new GuiWindow()
		{
			boundries = TargetingWnd.SetTartetingWindowPossition()
		};
		TargetingWnd.wndTargeting.SetBackgroundTexture("MainScreenWindow", "targeting_frame");
		TargetingWnd.wndTargeting.zOrder = 200;
		TargetingWnd.btnClose = new GuiButtonFixed()
		{
			X = 443f,
			Y = 29f
		};
		TargetingWnd.btnClose.SetTexture("MainScreenWindow", "targeting_close");
		TargetingWnd.btnClose.Caption = string.Empty;
		TargetingWnd.btnClose.Clicked = new Action<EventHandlerParam>(null, TargetingWnd.OnCloseBtnClick);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.btnClose);
		TargetingWnd.targetAvatar = new GuiTexture();
		TargetingWnd.targetAvatar.boundries.set_x(10f);
		TargetingWnd.targetAvatar.boundries.set_y(24f);
		TargetingWnd.targetAvatar.SetSize(58f, 40f);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetAvatar);
		TargetingWnd.fractionIcon = new GuiTexture();
		TargetingWnd.fractionIcon.boundries.set_x(78f);
		TargetingWnd.fractionIcon.boundries.set_y(12f);
		TargetingWnd.fractionIcon.SetSize(27f, 18f);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.fractionIcon);
		TargetingWnd.pileName = new GuiLabel()
		{
			boundries = new Rect(109f, 8f, 330f, 12f),
			Alignment = 3,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.pileName);
		TargetingWnd.pileAmount = new GuiLabel()
		{
			boundries = new Rect(109f, 8f, 330f, 12f),
			Alignment = 5,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.pileAmount);
		TargetingWnd.ownerLbl = new GuiLabel()
		{
			boundries = new Rect(109f, 22f, 330f, 12f),
			Alignment = 3,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.ownerLbl);
		TargetingWnd.ownerName = new GuiLabel()
		{
			boundries = new Rect(109f, 22f, 330f, 12f),
			Alignment = 5,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.ownerName);
		PlayerObjectPhysics playerObjectPhysic = (PlayerObjectPhysics)NetworkScript.player.shipScript.selectedObject;
		TargetingWnd.btnChat = new GuiButtonFixed()
		{
			X = 407f,
			Y = 40f
		};
		TargetingWnd.btnChat.SetTexture("MainScreenWindow", "targeting_chat");
		TargetingWnd.btnChat.Caption = string.Empty;
		TargetingWnd.btnChat.isEnabled = TargetingWnd.IsGoodToChat;
		GuiButtonFixed guiButtonFixed = TargetingWnd.btnChat;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = playerObjectPhysic.playerId
		};
		guiButtonFixed.eventHandlerParam = eventHandlerParam;
		TargetingWnd.btnChat.Clicked = new Action<EventHandlerParam>(null, TargetingWnd.OnChatBtnClick);
		GuiButtonFixed guiButtonFixed1 = TargetingWnd.btnChat;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_targeting_btn_start_chat_tooltip"),
			customData2 = TargetingWnd.btnChat
		};
		guiButtonFixed1.tooltipWindowParam = eventHandlerParam;
		TargetingWnd.btnChat.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.btnChat);
		TargetingWnd.btnProfile = new GuiButtonFixed()
		{
			X = 371f,
			Y = 40f
		};
		TargetingWnd.btnProfile.SetTexture("MainScreenWindow", "targeting_profile");
		TargetingWnd.btnProfile.Caption = string.Empty;
		GuiButtonFixed guiButtonFixed2 = TargetingWnd.btnProfile;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_targeting_btn_profile_tooltip"),
			customData2 = TargetingWnd.btnProfile
		};
		guiButtonFixed2.tooltipWindowParam = eventHandlerParam;
		TargetingWnd.btnProfile.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		GuiButtonFixed guiButtonFixed3 = TargetingWnd.btnProfile;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = playerObjectPhysic.playerName
		};
		guiButtonFixed3.eventHandlerParam = eventHandlerParam;
		TargetingWnd.btnProfile.Clicked = new Action<EventHandlerParam>(null, TargetingWnd.OnProfilBtnClick);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.btnProfile);
		TargetingWnd.btnInvite = new GuiButtonFixed()
		{
			X = 335f,
			Y = 40f
		};
		TargetingWnd.btnInvite.SetTexture("MainScreenWindow", "targeting_party");
		TargetingWnd.btnInvite.Caption = string.Empty;
		TargetingWnd.btnInvite.Clicked = new Action<EventHandlerParam>(null, NetworkScript.OnPartyInviteClicked);
		GuiButtonFixed guiButtonFixed4 = TargetingWnd.btnInvite;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = playerObjectPhysic.playerId
		};
		guiButtonFixed4.eventHandlerParam = eventHandlerParam;
		GuiButtonFixed guiButtonFixed5 = TargetingWnd.btnInvite;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_targeting_btn_party_invite"),
			customData2 = TargetingWnd.btnInvite
		};
		guiButtonFixed5.tooltipWindowParam = eventHandlerParam;
		TargetingWnd.btnInvite.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.btnInvite);
		TargetingWnd.barTargetShield = new GuiNewStyleBar();
		TargetingWnd.barTargetShield.SetCustumSizeBlueBar(70);
		TargetingWnd.barTargetShield.current = 0f;
		TargetingWnd.barTargetShield.boundries.set_x(79f);
		TargetingWnd.barTargetShield.boundries.set_y(40f);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.barTargetShield);
		TargetingWnd.barTargetHull = new GuiNewStyleBar();
		TargetingWnd.barTargetHull.SetCustumSizeGreenBar(70);
		TargetingWnd.barTargetHull.current = 0f;
		TargetingWnd.barTargetHull.boundries.set_x(79f);
		TargetingWnd.barTargetHull.boundries.set_y(57f);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.barTargetHull);
		TargetingWnd.lblTargetShield = new GuiLabel()
		{
			boundries = new Rect(TargetingWnd.barTargetShield.X + 70f, TargetingWnd.barTargetShield.Y, 40f, 17f),
			Alignment = 3,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			text = string.Empty
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.lblTargetShield);
		TargetingWnd.lblTargetHull = new GuiLabel()
		{
			boundries = new Rect(TargetingWnd.barTargetHull.X + 70f, TargetingWnd.barTargetHull.Y, 40f, 17f),
			Alignment = 3,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			TextColor = GuiNewStyleBar.greenColor,
			text = string.Empty
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.lblTargetHull);
		AndromedaGui.gui.AddWindow(TargetingWnd.wndTargeting);
		TargetingWnd.wndTargeting.isHidden = false;
		TargetingWnd.targetSectionIndex = 1;
	}

	private static void CreatePressetIcons(GuiWindow window)
	{
		TargetingWnd.pvePresetOne = new GuiTexture()
		{
			boundries = new Rect(35f, 0f, 34f, 34f)
		};
		TargetingWnd.pvePresetOne.SetTextureKeepSize("FrameworkGUI", "empty");
		window.AddGuiElement(TargetingWnd.pvePresetOne);
		TargetingWnd.pvePressetOnePower = new GuiLabel()
		{
			boundries = TargetingWnd.pvePresetOne.boundries
		};
		TargetingWnd.pvePressetOnePower.boundries.set_width(32f);
		TargetingWnd.pvePressetOnePower.Alignment = 8;
		TargetingWnd.pvePressetOnePower.FontSize = 12;
		TargetingWnd.pvePressetOnePower.Font = GuiLabel.FontBold;
		window.AddGuiElement(TargetingWnd.pvePressetOnePower);
		TargetingWnd.pvePresetTwo = new GuiTexture()
		{
			boundries = new Rect(35f, 35f, 34f, 34f)
		};
		TargetingWnd.pvePresetTwo.SetTextureKeepSize("FrameworkGUI", "empty");
		window.AddGuiElement(TargetingWnd.pvePresetTwo);
		TargetingWnd.pvePressetTwoPower = new GuiLabel()
		{
			boundries = TargetingWnd.pvePresetTwo.boundries
		};
		TargetingWnd.pvePressetTwoPower.boundries.set_width(32f);
		TargetingWnd.pvePressetTwoPower.Alignment = 8;
		TargetingWnd.pvePressetTwoPower.FontSize = 12;
		TargetingWnd.pvePressetTwoPower.Font = GuiLabel.FontBold;
		window.AddGuiElement(TargetingWnd.pvePressetTwoPower);
		TargetingWnd.pvePresetThree = new GuiTexture()
		{
			boundries = new Rect(0f, 0f, 34f, 34f)
		};
		TargetingWnd.pvePresetThree.SetTextureKeepSize("FrameworkGUI", "empty");
		window.AddGuiElement(TargetingWnd.pvePresetThree);
		TargetingWnd.pvePressetThreePower = new GuiLabel()
		{
			boundries = TargetingWnd.pvePresetThree.boundries
		};
		TargetingWnd.pvePressetThreePower.boundries.set_width(32f);
		TargetingWnd.pvePressetThreePower.Alignment = 8;
		TargetingWnd.pvePressetThreePower.FontSize = 12;
		TargetingWnd.pvePressetThreePower.Font = GuiLabel.FontBold;
		window.AddGuiElement(TargetingWnd.pvePressetThreePower);
		TargetingWnd.pvePresetFour = new GuiTexture()
		{
			boundries = new Rect(0f, 35f, 34f, 34f)
		};
		TargetingWnd.pvePresetFour.SetTextureKeepSize("FrameworkGUI", "empty");
		window.AddGuiElement(TargetingWnd.pvePresetFour);
		TargetingWnd.pvePressetFourPower = new GuiLabel()
		{
			boundries = TargetingWnd.pvePresetFour.boundries
		};
		TargetingWnd.pvePressetFourPower.boundries.set_width(32f);
		TargetingWnd.pvePressetFourPower.Alignment = 8;
		TargetingWnd.pvePressetFourPower.FontSize = 12;
		TargetingWnd.pvePressetFourPower.Font = GuiLabel.FontBold;
		window.AddGuiElement(TargetingWnd.pvePressetFourPower);
	}

	private static void CreatePveTarget()
	{
		TargetingWnd.wndTargeting = new GuiWindow()
		{
			boundries = TargetingWnd.SetTartetingWindowPossition()
		};
		TargetingWnd.wndTargeting.SetBackgroundTexture("MainScreenWindow", "targeting_frame");
		TargetingWnd.wndTargeting.zOrder = 200;
		TargetingWnd.btnClose = new GuiButtonFixed()
		{
			X = 443f,
			Y = 29f
		};
		TargetingWnd.btnClose.SetTexture("MainScreenWindow", "targeting_close");
		TargetingWnd.btnClose.Caption = string.Empty;
		TargetingWnd.btnClose.Clicked = new Action<EventHandlerParam>(null, TargetingWnd.OnCloseBtnClick);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.btnClose);
		TargetingWnd.targetAvatar = new GuiTexture();
		TargetingWnd.targetAvatar.boundries.set_x(10f);
		TargetingWnd.targetAvatar.boundries.set_y(15f);
		TargetingWnd.targetAvatar.SetSize(58f, 58f);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.targetAvatar);
		TargetingWnd.fractionIcon = new GuiTexture();
		TargetingWnd.fractionIcon.boundries.set_x(78f);
		TargetingWnd.fractionIcon.boundries.set_y(12f);
		TargetingWnd.fractionIcon.SetSize(27f, 18f);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.fractionIcon);
		TargetingWnd.pileName = new GuiLabel()
		{
			boundries = new Rect(79f, 8f, 360f, 12f),
			Alignment = 3,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.redColor,
			FontSize = 12
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.pileName);
		TargetingWnd.pileAmount = new GuiLabel()
		{
			boundries = new Rect(79f, 8f, 360f, 12f),
			Alignment = 5,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.pileAmount);
		TargetingWnd.ownerLbl = new GuiLabel()
		{
			boundries = new Rect(79f, 22f, 360f, 12f),
			Alignment = 3,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.ownerLbl);
		TargetingWnd.ownerName = new GuiLabel()
		{
			boundries = new Rect(79f, 22f, 360f, 12f),
			Alignment = 5,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.ownerName);
		TargetingWnd.barTargetShield = new GuiNewStyleBar();
		TargetingWnd.barTargetShield.SetCustumSizeBlueBar(70);
		TargetingWnd.barTargetShield.current = 0f;
		TargetingWnd.barTargetShield.boundries.set_x(79f);
		TargetingWnd.barTargetShield.boundries.set_y(40f);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.barTargetShield);
		TargetingWnd.barTargetHull = new GuiNewStyleBar();
		TargetingWnd.barTargetHull.SetCustumSizeGreenBar(70);
		TargetingWnd.barTargetHull.current = 0f;
		TargetingWnd.barTargetHull.boundries.set_x(79f);
		TargetingWnd.barTargetHull.boundries.set_y(57f);
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.barTargetHull);
		TargetingWnd.lblTargetShield = new GuiLabel()
		{
			boundries = new Rect(TargetingWnd.barTargetShield.X + 70f, TargetingWnd.barTargetShield.Y, 40f, 17f),
			Alignment = 3,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			text = string.Empty
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.lblTargetShield);
		TargetingWnd.lblTargetHull = new GuiLabel()
		{
			boundries = new Rect(TargetingWnd.barTargetHull.X + 70f, TargetingWnd.barTargetHull.Y, 40f, 17f),
			Alignment = 3,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			TextColor = GuiNewStyleBar.greenColor,
			text = string.Empty
		};
		TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.lblTargetHull);
		TargetingWnd.CreatePressetIcons(TargetingWnd.wndTargeting);
		AndromedaGui.gui.AddWindow(TargetingWnd.wndTargeting);
		TargetingWnd.wndTargeting.isHidden = false;
		TargetingWnd.targetSectionIndex = 2;
	}

	private static void CreateTargetOfTarget(PlayerObjectPhysics target)
	{
		if (TargetingWnd.targetOfTaret_window != null)
		{
			TargetingWnd.PopulateTargetOfTarget(target);
			return;
		}
		TargetingWnd.targetOfTaret_window = new GuiWindow()
		{
			boundries = TargetingWnd.SetTargetOfTargetWindowPossition(),
			zOrder = 201
		};
		TargetingWnd.targetOfTaret_barShield = new GuiTexture();
		TargetingWnd.targetOfTaret_barShield.SetTexture("TargetingGui", "barBlue");
		TargetingWnd.targetOfTaret_barShield.boundries = new Rect(10f, 22f, 126f, 12f);
		TargetingWnd.targetOfTaret_window.AddGuiElement(TargetingWnd.targetOfTaret_barShield);
		TargetingWnd.targetOfTaret_barCorpus = new GuiTexture();
		TargetingWnd.targetOfTaret_barCorpus.SetTexture("TargetingGui", "barGreen");
		TargetingWnd.targetOfTaret_barCorpus.boundries = new Rect(48f, 33f, 88f, 7f);
		TargetingWnd.targetOfTaret_window.AddGuiElement(TargetingWnd.targetOfTaret_barCorpus);
		TargetingWnd.targetOfTaret_fame = new GuiTexture();
		TargetingWnd.targetOfTaret_fame.SetTexture("TargetingGui", "targetOfTargetPopFrame");
		TargetingWnd.targetOfTaret_fame.boundries = new Rect(0f, 0f, 210f, 57f);
		TargetingWnd.targetOfTaret_window.AddGuiElement(TargetingWnd.targetOfTaret_fame);
		TargetingWnd.targetOfTaret_avatar = new GuiTexture();
		TargetingWnd.targetOfTaret_avatar.SetTexture("FrameworkGUI", "empty");
		TargetingWnd.targetOfTaret_avatar.boundries = new Rect(142f, 5f, 45f, 45f);
		TargetingWnd.targetOfTaret_window.AddGuiElement(TargetingWnd.targetOfTaret_avatar);
		TargetingWnd.targetOfTaret_fraction = new GuiTexture();
		TargetingWnd.targetOfTaret_fraction.SetTexture("FrameworkGUI", "fraction0Icon");
		TargetingWnd.targetOfTaret_fraction.boundries = new Rect(189f, 26f, 18f, 12f);
		TargetingWnd.targetOfTaret_window.AddGuiElement(TargetingWnd.targetOfTaret_fraction);
		TargetingWnd.targetOfTaret_name = new GuiLabel()
		{
			boundries = new Rect(7f, 5f, 128f, 12f),
			Alignment = 3,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			FontSize = 10,
			TextColor = GuiNewStyleBar.blueColor
		};
		TargetingWnd.targetOfTaret_window.AddGuiElement(TargetingWnd.targetOfTaret_name);
		TargetingWnd.targetOfTaret_lavel = new GuiLabel()
		{
			boundries = new Rect(190f, 7f, 14f, 14f),
			Alignment = 4,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			FontSize = 10
		};
		TargetingWnd.targetOfTaret_window.AddGuiElement(TargetingWnd.targetOfTaret_lavel);
		TargetingWnd.targetOfTaret_selectBtn = new GuiButton()
		{
			boundries = new Rect(0f, 0f, 210f, 57f),
			Caption = string.Empty,
			textColorHover = GuiNewStyleBar.orangeColor
		};
		TargetingWnd.targetOfTaret_selectBtn.eventHandlerParam.customData = target;
		TargetingWnd.targetOfTaret_selectBtn.Clicked = new Action<EventHandlerParam>(null, TargetingWnd.OnTargetOfTargetClick);
		TargetingWnd.targetOfTaret_window.AddGuiElement(TargetingWnd.targetOfTaret_selectBtn);
		TargetingWnd.PopulateTargetOfTarget(target);
		AndromedaGui.gui.AddWindow(TargetingWnd.targetOfTaret_window);
		TargetingWnd.targetOfTaret_window.isHidden = false;
	}

	public static Color GetPoPColor(PlayerObjectPhysics pop)
	{
		TargetingWnd.<GetPoPColor>c__AnonStorey6E variable = null;
		Color _white = Color.get_white();
		if (pop.get_IsPve())
		{
			_white = (((PvEPhysics)pop).agressionType != 0 || ((PvEPhysics)pop).pveCommand == 1 ? GuiNewStyleBar.redColor : GuiNewStyleBar.orangeColor);
		}
		else if (pop.neighbourhoodId == NetworkScript.player.vessel.neighbourhoodId)
		{
			_white = Color.get_white();
		}
		else if (NetworkScript.player.pvpGame != null && NetworkScript.player.pvpGame.state == 1)
		{
			_white = (pop.teamNumber != NetworkScript.player.vessel.teamNumber ? GuiNewStyleBar.redColor : Color.get_white());
		}
		else if (NetworkScript.party != null && Enumerable.FirstOrDefault<PartyMemberClientSide>(Enumerable.Where<PartyMemberClientSide>(NetworkScript.party.members, new Func<PartyMemberClientSide, bool>(variable, (PartyMemberClientSide t) => t.playerId == this.pop.playerId))) != null)
		{
			_white = Color.get_white();
		}
		else if (string.IsNullOrEmpty(pop.guildTag) || string.IsNullOrEmpty(NetworkScript.player.vessel.guildTag) || !(pop.guildTag == NetworkScript.player.vessel.guildTag))
		{
			_white = (NetworkScript.player.vessel.fractionId != pop.fractionId ? GuiNewStyleBar.redColor : GuiNewStyleBar.greenColor);
		}
		else
		{
			_white = GuiNewStyleBar.cyanColor;
		}
		return _white;
	}

	public static string GetPveAvatarName(string assetname)
	{
		string empty = string.Empty;
		string[] strArray = assetname.Split(new char[] { ',' });
		if ((int)strArray.Length <= 1)
		{
			empty = assetname;
		}
		else
		{
			string[] strArray1 = strArray[1].Split(new char[] { '\u005F' });
			empty = string.Format("{0}_{1}", strArray[0], strArray1[1]);
			if ((int)strArray.Length > 2)
			{
				string[] strArray2 = strArray[2].Split(new char[] { '\u005F' });
				empty = string.Format("{0}_{1}_{2}", strArray[0], strArray1[1], strArray2[1]);
			}
		}
		return empty;
	}

	private static bool IsGoodToInvite(PlayerObjectPhysics pop, GuiElement btnInvite)
	{
		TargetingWnd.<IsGoodToInvite>c__AnonStorey6D variable = null;
		EventHandlerParam eventHandlerParam;
		if (pop.galaxy.__galaxyId >= 2000 && pop.galaxy.__galaxyId <= 2999)
		{
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("target_window_no_party_pvp"),
				customData2 = btnInvite
			};
			btnInvite.tooltipWindowParam = eventHandlerParam;
			return false;
		}
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_profile_screen_tooltips_no_invite"),
			customData2 = btnInvite
		};
		btnInvite.tooltipWindowParam = eventHandlerParam;
		if (pop.fractionId != NetworkScript.player.vessel.fractionId)
		{
			return false;
		}
		if (NetworkScript.partyInvitees.get_Count() >= 3)
		{
			return false;
		}
		if (pop.playerId == NetworkScript.player.playId)
		{
			return false;
		}
		if (pop.isInParty)
		{
			return false;
		}
		if (NetworkScript.partyInvitees.ContainsKey(pop.playerId))
		{
			return false;
		}
		if (NetworkScript.party != null && NetworkScript.party.members != null)
		{
			if (NetworkScript.party.members.get_Count() > 0)
			{
				if (NetworkScript.party.members.get_Item(0).playerId != NetworkScript.player.playId)
				{
					return false;
				}
				if (Enumerable.FirstOrDefault<PartyMemberClientSide>(Enumerable.Where<PartyMemberClientSide>(NetworkScript.party.members, new Func<PartyMemberClientSide, bool>(variable, (PartyMemberClientSide w) => w.playerId == this.pop.playerId))) != null)
				{
					return false;
				}
				if (NetworkScript.party.members.get_Count() + NetworkScript.partyInvitees.get_Count() >= 4)
				{
					return false;
				}
			}
			if (NetworkScript.party.members.get_Count() >= 4)
			{
				return false;
			}
		}
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_targeting_btn_party_invite"),
			customData2 = btnInvite
		};
		btnInvite.tooltipWindowParam = eventHandlerParam;
		return true;
	}

	private static void OnAvatarLoadedCallback(AvatarJob job)
	{
		if (job.key != TargetingWnd.currentTargetAvatar)
		{
			return;
		}
		if (job.token == null)
		{
			TargetingWnd.targetAvatar.boundries.set_x(10f);
			TargetingWnd.targetAvatar.boundries.set_y(15f);
			TargetingWnd.targetAvatar.SetSize(58f, 58f);
			TargetingWnd.targetAvatar.SetTextureKeepSize(job.job.get_texture());
			TargetingWnd.currentTargetAvatar = string.Empty;
			return;
		}
		((GuiTexture)job.token).boundries.set_x(10f);
		((GuiTexture)job.token).boundries.set_y(15f);
		((GuiTexture)job.token).SetSize(58f, 58f);
		((GuiTexture)job.token).SetTextureKeepSize(job.job.get_texture());
	}

	private static void OnChatBtnClick(EventHandlerParam p)
	{
		if (NetworkScript.player.shipScript != null)
		{
			NetworkScript.player.shipScript.isGuiClosed = true;
		}
		TargetingWnd.RemoveOptionMenu();
		long num = (long)p.customData;
		if (__ChatWindow.wnd == null)
		{
			__ChatWindow.OpenTheWindow(true);
		}
		__ChatWindow.wnd.AddAndFocusPersonalChat(num);
	}

	private static void OnCloseBtnClick(object prm)
	{
		if (NetworkScript.player != null && NetworkScript.player.shipScript != null)
		{
			NetworkScript.player.shipScript.isGuiClosed = true;
			NetworkScript.player.shipScript.DeselectCurrentObject();
			if (NetworkScript.player.shipScript.p.selectedPoPnbId != 0)
			{
				playWebGame.udp.ExecuteCommand(49, new UniversalTransportContainer(), 56);
				NetworkScript.player.shipScript.p.selectedPoPnbId = 0;
			}
		}
	}

	private static void OnGoToMenuBtnClicekd(EventHandlerParam prm)
	{
		AndromedaGui.mainWnd.OnWindowBtnClicked(prm);
	}

	private static void OnOptionMenuClick(EventHandlerParam prm)
	{
		if (TargetingWnd.targetPoP_option_window != null)
		{
			AndromedaGui.gui.RemoveWindow(TargetingWnd.targetPoP_option_window.handler);
			TargetingWnd.targetPoP_option_window = null;
			TargetingWnd.optionMenuTargetNbId = 0;
			return;
		}
		if (prm.customData == null)
		{
			return;
		}
		TargetingWnd.CreateOptionMenu((PlayerObjectPhysics)prm.customData);
	}

	private static void OnProfilBtnClick(EventHandlerParam prm)
	{
		if (NetworkScript.player.shipScript != null)
		{
			NetworkScript.player.shipScript.isGuiClosed = true;
		}
		TargetingWnd.RemoveOptionMenu();
		NetworkScript.RequestUserProfile((string)prm.customData);
	}

	private static void OnStopFireBtnClicked(object prm)
	{
		if (NetworkScript.player.shipScript != null)
		{
			NetworkScript.player.shipScript.isGuiClosed = true;
		}
		NetworkScript.player.shipScript.StopShooting();
		TargetingWnd.RemoveOptionMenu();
	}

	private static void OnTargetOfTargetClick(EventHandlerParam prm)
	{
		if (prm == null || prm.customData == null)
		{
			return;
		}
		if (NetworkScript.player != null && NetworkScript.player.shipScript != null)
		{
			NetworkScript.player.shipScript.isGuiClosed = true;
		}
		GameObjectPhysics gameObjectPhysic = (GameObjectPhysics)prm.customData;
		GameObject gameObject = (GameObject)gameObjectPhysic.gameObject;
		NetworkScript.player.shipScript.ManageSelectObjectRequest(gameObject, gameObjectPhysic);
	}

	private static void OnTimeOff()
	{
		if (TargetingWnd.targetSectionIndex == 6)
		{
			TargetingWnd.Remove();
		}
	}

	public static void PopulateData()
	{
		if (TargetingWnd.targetSectionIndex == 6)
		{
			return;
		}
		if (NetworkScript.player.shipScript.selectedObject != null)
		{
			TargetingWnd.target = NetworkScript.player.shipScript.selectedObject;
			if (TargetingWnd.target is Mineral)
			{
				TargetingWnd.RemoveTargetOfTarget();
				TargetingWnd.RemoveOptionMenu();
				TargetingWnd.ChooseMieralDetailsToPopulate((Mineral)TargetingWnd.target);
				return;
			}
			if (TargetingWnd.target is PvEPhysics)
			{
				TargetingWnd.PopulateNewPVE();
				return;
			}
			if (TargetingWnd.target is PlayerObjectPhysics)
			{
				TargetingWnd.PopulateNewPlayerTarget();
				return;
			}
			if (TargetingWnd.target is ExtractionPoint)
			{
				TargetingWnd.RemoveTargetOfTarget();
				TargetingWnd.RemoveOptionMenu();
				TargetingWnd.PopulateExtractionPoint();
				return;
			}
		}
		else if (TargetingWnd.targetSectionIndex != 0)
		{
			TargetingWnd.RemoveTargetOfTarget();
			TargetingWnd.RemoveOptionMenu();
			TargetingWnd.Remove();
		}
	}

	private static void PopulateExtractionPoint()
	{
		if (TargetingWnd.targetSectionIndex == 0)
		{
			TargetingWnd.CreateExtractionPointTarger();
		}
		if (TargetingWnd.targetSectionIndex != 7)
		{
			AndromedaGui.gui.RemoveWindow(TargetingWnd.wndTargeting.handler);
			TargetingWnd.targetSectionIndex = 0;
			TargetingWnd.CreateExtractionPointTarger();
		}
		TargetingWnd.wndTargeting.boundries = TargetingWnd.SetTartetingWindowPossition();
		ExtractionPoint extractionPoint = (ExtractionPoint)TargetingWnd.target;
		TargetingWnd.barTargetHull.maximum = (float)extractionPoint.hitPoints;
		TargetingWnd.barTargetHull.current = extractionPoint.health;
		string str = string.Format("fraction{0}Icon", extractionPoint.ownerFraction);
		TargetingWnd.lblTargetHull.text = string.Format("{0:#,##0}/{1:#,##0}", extractionPoint.health, extractionPoint.hitPoints);
		TargetingWnd.PopulateStopShootingBtn(extractionPoint, 108f);
		TargetingWnd.targetAvatar.SetTextureKeepSize("Targeting", extractionPoint.assetName);
		TargetingWnd.fractionIcon.SetTextureKeepSize("FrameworkGUI", str);
		TargetingWnd.pileName.text = StaticData.Translate(extractionPoint.name);
		TargetingWnd.pileName.TextColor = (extractionPoint.ownerFraction != NetworkScript.player.vessel.fractionId ? GuiNewStyleBar.redColor : GuiNewStyleBar.greenColor);
		if (extractionPoint.state != null)
		{
			TargetingWnd.pileAmount.text = StaticData.Translate("universe_map_point_being_captured");
			TargetingWnd.pileAmount.TextColor = GuiNewStyleBar.redColor;
		}
		else
		{
			TargetingWnd.pileAmount.text = StaticData.Translate("universe_map_point_in_control");
			TargetingWnd.pileAmount.TextColor = Color.get_white();
		}
		TargetingWnd.pileName.boundries.set_width(330f - TargetingWnd.pileAmount.TextWidth - 10f);
		if (extractionPoint.ownerFraction == NetworkScript.player.vessel.fractionId || extractionPoint.state != 1)
		{
			TargetingWnd.ownerName.text = string.Empty;
		}
		else
		{
			TargetingWnd.ownerName.text = StaticData.Translate("target_window_ep_establishing_control");
		}
	}

	private static void PopulateGoToMenuBtn(Mineral mineral)
	{
		if (NetworkScript.player.playerBelongings.CanMineMineral(mineral, (long)NetworkScript.player.cfg.cargoMax))
		{
			if (TargetingWnd.btnGoToMenu != null)
			{
				TargetingWnd.wndTargeting.RemoveGuiElement(TargetingWnd.btnGoToMenu);
				TargetingWnd.btnGoToMenu = null;
			}
		}
		else if (mineral.items != null && mineral.items.get_Count() != 0)
		{
			TargetingWnd.pileDescription.text = StaticData.Translate("key_space_label_inventory_full");
			TargetingWnd.pileDescription.TextColor = GuiNewStyleBar.orangeColor;
			TargetingWnd.pileDescription.boundries = new Rect(79f, 36f, 215f, 50f);
			if (TargetingWnd.btnGoToMenu == null || !TargetingWnd.wndTargeting.HasGuiElement(TargetingWnd.btnGoToMenu))
			{
				TargetingWnd.btnGoToMenu = new GuiButtonFixed();
				TargetingWnd.btnGoToMenu.SetTexture("NewGUI", "targeting_action");
				TargetingWnd.btnGoToMenu.Y = 44f;
				TargetingWnd.btnGoToMenu.X = 297f;
				TargetingWnd.btnGoToMenu.eventHandlerParam.customData = (byte)1;
				TargetingWnd.btnGoToMenu.Clicked = new Action<EventHandlerParam>(null, TargetingWnd.OnGoToMenuBtnClicekd);
				TargetingWnd.btnGoToMenu.Caption = StaticData.Translate("key_ship_cfg_tab_inventory");
				TargetingWnd.btnGoToMenu.FontSize = 12;
				TargetingWnd.btnGoToMenu.isMuted = true;
				TargetingWnd.btnGoToMenu.textColorNormal = GuiNewStyleBar.blueColor;
				TargetingWnd.btnGoToMenu.Alignment = 4;
				TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.btnGoToMenu);
				TargetingWnd.goToMenuState = 2;
			}
			else if (TargetingWnd.goToMenuState != 2)
			{
				TargetingWnd.btnGoToMenu.eventHandlerParam.customData = (byte)1;
				TargetingWnd.btnGoToMenu.Caption = StaticData.Translate("key_ship_cfg_tab_inventory");
				TargetingWnd.goToMenuState = 2;
			}
		}
		else if (mineral.resourceQuantities.get_Count() != 1)
		{
			TargetingWnd.lblLoot1.text = StaticData.Translate("key_space_label_cargo_full_msg");
			TargetingWnd.lblLoot1.TextColor = GuiNewStyleBar.orangeColor;
			TargetingWnd.lblLoot1.boundries = new Rect(79f, 36f, 215f, 50f);
			TargetingWnd.lblLoot2.text = string.Empty;
			TargetingWnd.lblLoot3.text = string.Empty;
			TargetingWnd.lblLoot4.text = string.Empty;
			TargetingWnd.lblLoot1Amount.text = string.Empty;
			TargetingWnd.lblLoot2Amount.text = string.Empty;
			TargetingWnd.lblLoot3Amount.text = string.Empty;
			TargetingWnd.lblLoot4Amount.text = string.Empty;
			if (TargetingWnd.btnGoToMenu == null || !TargetingWnd.wndTargeting.HasGuiElement(TargetingWnd.btnGoToMenu))
			{
				TargetingWnd.btnGoToMenu = new GuiButtonFixed();
				TargetingWnd.btnGoToMenu.SetTexture("NewGUI", "targeting_action");
				TargetingWnd.btnGoToMenu.Y = 44f;
				TargetingWnd.btnGoToMenu.X = 297f;
				TargetingWnd.btnGoToMenu.eventHandlerParam.customData = (byte)0;
				TargetingWnd.btnGoToMenu.Clicked = new Action<EventHandlerParam>(null, TargetingWnd.OnGoToMenuBtnClicekd);
				TargetingWnd.btnGoToMenu.Caption = StaticData.Translate("key_fushion_fushion");
				TargetingWnd.btnGoToMenu.FontSize = 12;
				TargetingWnd.btnGoToMenu.isMuted = true;
				TargetingWnd.btnGoToMenu.textColorNormal = GuiNewStyleBar.blueColor;
				TargetingWnd.btnGoToMenu.Alignment = 4;
				TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.btnGoToMenu);
				TargetingWnd.goToMenuState = 1;
			}
			else if (TargetingWnd.goToMenuState != 1)
			{
				TargetingWnd.btnGoToMenu.eventHandlerParam.customData = (byte)0;
				TargetingWnd.btnGoToMenu.Caption = StaticData.Translate("key_fushion_fushion");
				TargetingWnd.goToMenuState = 1;
			}
		}
		else
		{
			TargetingWnd.pileDescription.text = StaticData.Translate("key_space_label_cargo_full_msg");
			TargetingWnd.pileDescription.TextColor = GuiNewStyleBar.orangeColor;
			TargetingWnd.pileDescription.boundries = new Rect(79f, 36f, 215f, 50f);
			if (TargetingWnd.btnGoToMenu == null || !TargetingWnd.wndTargeting.HasGuiElement(TargetingWnd.btnGoToMenu))
			{
				TargetingWnd.btnGoToMenu = new GuiButtonFixed();
				TargetingWnd.btnGoToMenu.SetTexture("NewGUI", "targeting_action");
				TargetingWnd.btnGoToMenu.Y = 44f;
				TargetingWnd.btnGoToMenu.X = 297f;
				TargetingWnd.btnGoToMenu.eventHandlerParam.customData = (byte)0;
				TargetingWnd.btnGoToMenu.Clicked = new Action<EventHandlerParam>(null, TargetingWnd.OnGoToMenuBtnClicekd);
				TargetingWnd.btnGoToMenu.Caption = StaticData.Translate("key_fushion_fushion");
				TargetingWnd.btnGoToMenu.FontSize = 12;
				TargetingWnd.btnGoToMenu.isMuted = true;
				TargetingWnd.btnGoToMenu.textColorNormal = GuiNewStyleBar.blueColor;
				TargetingWnd.btnGoToMenu.Alignment = 4;
				TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.btnGoToMenu);
				TargetingWnd.goToMenuState = 1;
			}
			else if (TargetingWnd.goToMenuState != 1)
			{
				TargetingWnd.btnGoToMenu.eventHandlerParam.customData = (byte)0;
				TargetingWnd.btnGoToMenu.Caption = StaticData.Translate("key_fushion_fushion");
				TargetingWnd.goToMenuState = 1;
			}
		}
	}

	private static void PopulateItem()
	{
		if (TargetingWnd.targetSectionIndex == 0)
		{
			TargetingWnd.CreateItemTarget();
		}
		if (TargetingWnd.targetSectionIndex != 5)
		{
			AndromedaGui.gui.RemoveWindow(TargetingWnd.wndTargeting.handler);
			TargetingWnd.targetSectionIndex = 0;
			TargetingWnd.CreateItemTarget();
		}
		TargetingWnd.wndTargeting.boundries = TargetingWnd.SetTartetingWindowPossition();
		Mineral mineral = (Mineral)TargetingWnd.target;
		ushort itemType = mineral.items.get_Item(0).get_ItemType();
		Color _white = Color.get_white();
		string str = StaticData.Translate(StaticData.allTypes.get_Item(itemType).uiName);
		if (PlayerItems.IsWeapon(itemType) || PlayerItems.IsCorpus(itemType) || PlayerItems.IsShield(itemType) || PlayerItems.IsEngine(itemType) || PlayerItems.IsExtra(itemType))
		{
			SlotItem item = mineral.items.get_Item(0);
			Inventory.ItemRarity(item, out str, out _white);
			string str1 = "empty";
			string str2 = "FrameworkGUI";
			switch (mineral.items.get_Item(0).get_BonusCnt())
			{
				case 1:
				case 2:
				{
					str2 = "ConfigWindow";
					str1 = "TargetBlue";
					break;
				}
				case 3:
				case 4:
				{
					str2 = "ConfigWindow";
					str1 = "TargetOrange";
					break;
				}
				case 5:
				{
					str2 = "ConfigWindow";
					str1 = "TargetPurple";
					break;
				}
			}
			TargetingWnd.itemGlow.SetTextureKeepSize(str2, str1);
		}
		TargetingWnd.pileName.text = str;
		TargetingWnd.pileName.TextColor = _white;
		TargetingWnd.targetAvatar.SetItemTextureKeepSize(itemType);
		if (mineral.ownerName == string.Empty)
		{
			TargetingWnd.ownerName.text = StaticData.Translate("key_owner_nobody");
		}
		else if (mineral.ownerName != "Party Loot")
		{
			TargetingWnd.ownerName.text = (!(mineral.ownerName == NetworkScript.player.playerBelongings.playerName) || !NetworkScript.player.vessel.isGuest ? mineral.ownerName : StaticData.Translate("key_guest_player"));
		}
		else
		{
			TargetingWnd.ownerName.text = StaticData.Translate("key_owner_party");
		}
		if (!PlayerItems.IsStackable(itemType))
		{
			TargetingWnd.pileAmount.text = string.Empty;
			if (!PlayerItems.IsPortalPart(itemType))
			{
				GuiLabel guiLabel = TargetingWnd.pilePrice;
				string str3 = StaticData.Translate("key_targeting_value");
				int num = PlayerItems.CalculateSlotItemSellPrice(mineral.items.get_Item(0), NetworkScript.player.vessel.cfg.sellBonus);
				guiLabel.text = string.Concat(str3, " ", num.ToString("##,##0"));
			}
			else
			{
				TargetingWnd.pilePrice.text = string.Empty;
			}
		}
		else
		{
			GuiLabel guiLabel1 = TargetingWnd.pileAmount;
			string str4 = StaticData.Translate("key_targeting_qty");
			int amount = mineral.items.get_Item(0).get_Amount();
			guiLabel1.text = string.Concat(str4, " ", amount.ToString("##,##0"));
			TargetingWnd.pilePrice.text = string.Empty;
		}
		TargetingWnd.pileDescription.text = mineral.items.get_Item(0).Description();
		TargetingWnd.pileDescription.TextColor = GuiNewStyleBar.blueColor;
		TargetingWnd.pileDescription.boundries = new Rect(79f, 36f, 360f, 50f);
		TargetingWnd.PopulateGoToMenuBtn(mineral);
	}

	private static void PopulateLoot()
	{
		if (TargetingWnd.targetSectionIndex == 0)
		{
			TargetingWnd.CreateLootTarget();
		}
		if (TargetingWnd.targetSectionIndex != 4)
		{
			AndromedaGui.gui.RemoveWindow(TargetingWnd.wndTargeting.handler);
			TargetingWnd.targetSectionIndex = 0;
			TargetingWnd.CreateLootTarget();
		}
		TargetingWnd.wndTargeting.boundries = TargetingWnd.SetTartetingWindowPossition();
		Mineral mineral = (Mineral)TargetingWnd.target;
		ushort[] array = Enumerable.ToArray<ushort>(mineral.resourceQuantities.get_Keys());
		int[] numArray = Enumerable.ToArray<int>(mineral.resourceQuantities.get_Values());
		int num = Enumerable.Count<KeyValuePair<ushort, int>>(mineral.resourceQuantities);
		TargetingWnd.lblLoot1.TextColor = GuiNewStyleBar.blueColor;
		TargetingWnd.lblLoot1.boundries = new Rect(79f, 38f, 175f, 16f);
		GuiLabel[] x = new GuiLabel[] { TargetingWnd.lblLoot1, TargetingWnd.lblLoot2, TargetingWnd.lblLoot3, TargetingWnd.lblLoot4 };
		GuiLabel[] str = new GuiLabel[] { TargetingWnd.lblLoot1Amount, TargetingWnd.lblLoot2Amount, TargetingWnd.lblLoot3Amount, TargetingWnd.lblLoot4Amount };
		int num1 = 0;
		int num2 = 0;
		int num3 = 0;
		TargetingWnd.targetAvatar.SetTextureKeepSize("MineralsAvatars", mineral.assetName);
		if (mineral.ownerName == string.Empty)
		{
			TargetingWnd.ownerName.text = StaticData.Translate("key_owner_nobody");
		}
		else if (mineral.ownerName != "Party Loot")
		{
			TargetingWnd.ownerName.text = (!(mineral.ownerName == NetworkScript.player.playerBelongings.playerName) || !NetworkScript.player.vessel.isGuest ? mineral.ownerName : StaticData.Translate("key_guest_player"));
		}
		else
		{
			TargetingWnd.ownerName.text = StaticData.Translate("key_owner_party");
		}
		TargetingWnd.pileName.text = StaticData.Translate("key_targeting_res_pile");
		if (num > Enumerable.Count<GuiLabel>(x))
		{
			Debug.Log("Opa");
			return;
		}
		for (int i = 0; i < num; i++)
		{
			if (numArray[i] != 0)
			{
				num2 = num2 + numArray[i];
				num3 = num3 + PlayerItems.CalculateSellPrice(array[i], NetworkScript.player.vessel.cfg.sellBonus) * numArray[i];
				x[num1].text = StaticData.Translate(StaticData.allTypes.get_Item(array[i]).uiName);
				str[num1].text = numArray[i].ToString("##,##0");
				x[num1].X = str[num1].X + str[num1].TextWidth + 5f;
				num1++;
			}
		}
		TargetingWnd.pileAmount.text = string.Concat(StaticData.Translate("key_targeting_qty"), " ", num2.ToString("##,##0"));
		TargetingWnd.pilePrice.text = string.Concat(StaticData.Translate("key_targeting_value"), " ", num3.ToString("##,##0"));
		TargetingWnd.PopulateGoToMenuBtn(mineral);
	}

	private static void PopulateMineral()
	{
		if (TargetingWnd.targetSectionIndex == 0)
		{
			TargetingWnd.CreateMineralTarget();
		}
		if (TargetingWnd.targetSectionIndex != 3)
		{
			AndromedaGui.gui.RemoveWindow(TargetingWnd.wndTargeting.handler);
			TargetingWnd.targetSectionIndex = 0;
			TargetingWnd.CreateMineralTarget();
		}
		TargetingWnd.wndTargeting.boundries = TargetingWnd.SetTartetingWindowPossition();
		Mineral mineral = (Mineral)TargetingWnd.target;
		ushort num = Enumerable.First<ushort>(mineral.resourceQuantities.get_Keys());
		if (num == PlayerItems.TypeNova || num == PlayerItems.TypeEquilibrium)
		{
			TargetingWnd.targetAvatar.boundries = new Rect(10f, 24f, 58f, 40f);
			TargetingWnd.targetAvatar.SetItemTextureKeepSize(num);
		}
		else
		{
			TargetingWnd.targetAvatar.boundries = new Rect(10f, 15f, 58f, 58f);
			TargetingWnd.targetAvatar.SetTextureKeepSize("MineralsAvatars", StaticData.allTypes.get_Item(num).assetName);
		}
		int item = mineral.resourceQuantities.get_Item(Enumerable.First<ushort>(mineral.resourceQuantities.get_Keys()));
		TargetingWnd.pileName.text = StaticData.Translate(StaticData.allTypes.get_Item(num).uiName);
		TargetingWnd.pileAmount.text = string.Concat(StaticData.Translate("key_targeting_qty"), " ", item.ToString("##,##0"));
		if (mineral.ownerName == string.Empty)
		{
			TargetingWnd.ownerName.text = StaticData.Translate("key_owner_nobody");
		}
		else if (mineral.ownerName != "Party Loot")
		{
			TargetingWnd.ownerName.text = (!(mineral.ownerName == NetworkScript.player.playerBelongings.playerName) || !NetworkScript.player.vessel.isGuest ? mineral.ownerName : StaticData.Translate("key_guest_player"));
		}
		else
		{
			TargetingWnd.ownerName.text = StaticData.Translate("key_owner_party");
		}
		int num1 = PlayerItems.CalculateSellPrice(num, NetworkScript.player.vessel.cfg.sellBonus) * item;
		TargetingWnd.pilePrice.text = string.Concat(StaticData.Translate("key_targeting_value"), " ", num1.ToString("##,##0"));
		TargetingWnd.pileDescription.text = StaticData.Translate(StaticData.allTypes.get_Item(num).description);
		TargetingWnd.pileDescription.TextColor = GuiNewStyleBar.blueColor;
		TargetingWnd.pileDescription.boundries = new Rect(79f, 36f, 360f, 50f);
		TargetingWnd.PopulateGoToMenuBtn(mineral);
	}

	private static void PopulateNewPlayerTarget()
	{
		GameObjectPhysics gameObjectPhysic = null;
		string str;
		if (TargetingWnd.targetSectionIndex == 0)
		{
			TargetingWnd.CreateNewPlayerTarget();
		}
		if (TargetingWnd.targetSectionIndex != 1)
		{
			AndromedaGui.gui.RemoveWindow(TargetingWnd.wndTargeting.handler);
			TargetingWnd.targetSectionIndex = 0;
			TargetingWnd.CreateNewPlayerTarget();
		}
		PlayerObjectPhysics playerObjectPhysic = (PlayerObjectPhysics)TargetingWnd.target;
		if (TargetingWnd.optionMenuTargetNbId != playerObjectPhysic.neighbourhoodId)
		{
			TargetingWnd.RemoveOptionMenu();
		}
		if (playerObjectPhysic.selectedPoPnbId == 0 || playerObjectPhysic.selectedPoPnbId == NetworkScript.player.vessel.neighbourhoodId)
		{
			TargetingWnd.RemoveTargetOfTarget();
		}
		else if (!NetworkScript.player.shipScript.comm.gameObjects.TryGetValue(playerObjectPhysic.selectedPoPnbId, ref gameObjectPhysic))
		{
			TargetingWnd.RemoveTargetOfTarget();
		}
		else
		{
			TargetingWnd.CreateTargetOfTarget((PlayerObjectPhysics)gameObjectPhysic);
		}
		TargetingWnd.PopulateOptionMenu(playerObjectPhysic);
		Texture2D avatarOrStartIt = playWebGame.GetAvatarOrStartIt(playerObjectPhysic.playerAvatarUrl, new Action<AvatarJob>(null, TargetingWnd.OnAvatarLoadedCallback), TargetingWnd.targetPvP_avatar);
		if (avatarOrStartIt != null)
		{
			TargetingWnd.targetPvP_avatar.boundries.set_x(27f);
			TargetingWnd.targetPvP_avatar.boundries.set_y(7f);
			TargetingWnd.targetPvP_avatar.SetSize(56f, 56f);
			TargetingWnd.targetPvP_avatar.SetTextureKeepSize(avatarOrStartIt);
		}
		else
		{
			TargetingWnd.targetPvP_avatar.boundries.set_x(27f);
			TargetingWnd.targetPvP_avatar.boundries.set_y(16f);
			TargetingWnd.targetPvP_avatar.SetSize(56f, 38f);
			TargetingWnd.currentTargetAvatar = playerObjectPhysic.playerAvatarUrl;
			TargetingWnd.targetPvP_avatar.SetTextureKeepSize("ShipsAvatars", playerObjectPhysic.cfg.assetName);
		}
		TargetingWnd.targetPvP_fraction.SetTextureKeepSize("FrameworkGUI", (playerObjectPhysic.fractionId != 1 ? "fraction2Icon" : "fraction1Icon"));
		float single = 1f * (float)playerObjectPhysic.cfg.hitPoints / (float)playerObjectPhysic.cfg.hitPointsMax;
		float single1 = 1f * playerObjectPhysic.cfg.shield / (float)playerObjectPhysic.cfg.shieldMax;
		TargetingWnd.targetPvP_barCorpus.boundries.set_width(single * 108f);
		TargetingWnd.targetPvP_barShield.boundries.set_width(single1 * 156f);
		if (!NetworkScript.player.playerBelongings.isShowMoreDetailsOn)
		{
			GuiLabel targetPvPCorpusValue = TargetingWnd.targetPvP_corpusValue;
			GuiLabel targetPvPCorpusValueShadow = TargetingWnd.targetPvP_corpusValueShadow;
			string empty = string.Empty;
			str = empty;
			targetPvPCorpusValueShadow.text = empty;
			targetPvPCorpusValue.text = str;
			GuiLabel targetPvPShieldValue = TargetingWnd.targetPvP_shieldValue;
			GuiLabel targetPvPShieldValueShadow = TargetingWnd.targetPvP_shieldValueShadow;
			string empty1 = string.Empty;
			str = empty1;
			targetPvPShieldValueShadow.text = empty1;
			targetPvPShieldValue.text = str;
		}
		else
		{
			GuiLabel guiLabel = TargetingWnd.targetPvP_corpusValue;
			GuiLabel targetPvPCorpusValueShadow1 = TargetingWnd.targetPvP_corpusValueShadow;
			string str1 = string.Format("{0:0} / {1:0}", playerObjectPhysic.cfg.hitPoints, playerObjectPhysic.cfg.hitPointsMax);
			str = str1;
			targetPvPCorpusValueShadow1.text = str1;
			guiLabel.text = str;
			GuiLabel targetPvPShieldValue1 = TargetingWnd.targetPvP_shieldValue;
			GuiLabel targetPvPShieldValueShadow1 = TargetingWnd.targetPvP_shieldValueShadow;
			string str2 = string.Format("{0:0} / {1:0}", playerObjectPhysic.cfg.shield, playerObjectPhysic.cfg.shieldMax);
			str = str2;
			targetPvPShieldValueShadow1.text = str2;
			targetPvPShieldValue1.text = str;
		}
		string str3 = (!(playerObjectPhysic.playerName == NetworkScript.player.playerBelongings.playerName) || !NetworkScript.player.vessel.isGuest ? playerObjectPhysic.playerName : StaticData.Translate("key_guest_player"));
		if (str3.get_Length() > 21)
		{
			str3 = string.Concat(str3.Substring(0, 20), "...");
		}
		TargetingWnd.targetPvP_playerName.text = str3;
		TargetingWnd.targetPvP_playerName.TextColor = TargetingWnd.GetPoPColor(playerObjectPhysic);
		TargetingWnd.targetPvP_playerLevel.text = playerObjectPhysic.cfg.playerLevel.ToString();
		TargetingWnd.targetPoP_actionBtn.eventHandlerParam.customData = playerObjectPhysic;
		if (!NetworkScript.player.vessel.isShooting || NetworkScript.player.vessel.shootingAt == null || NetworkScript.player.vessel.shootingAt.neighbourhoodId != playerObjectPhysic.neighbourhoodId)
		{
			TargetingWnd.targetPoP_shooting_indication.SetTextureKeepSize("FrameworkGUI", "empty");
		}
		else
		{
			TargetingWnd.targetPoP_shooting_indication.SetTextureKeepSize("TargetingGui", "fireIndication");
		}
	}

	private static void PopulateNewPVE()
	{
		GameObjectPhysics gameObjectPhysic = null;
		string str;
		if (TargetingWnd.targetSectionIndex == 0)
		{
			TargetingWnd.CreateNewPvETarget();
		}
		if (TargetingWnd.targetSectionIndex != 2)
		{
			AndromedaGui.gui.RemoveWindow(TargetingWnd.wndTargeting.handler);
			TargetingWnd.targetSectionIndex = 0;
			TargetingWnd.CreateNewPvETarget();
		}
		PvEPhysics pvEPhysic = (PvEPhysics)TargetingWnd.target;
		if (TargetingWnd.optionMenuTargetNbId != pvEPhysic.neighbourhoodId)
		{
			TargetingWnd.RemoveOptionMenu();
		}
		if (pvEPhysic.selectedPoPnbId == 0)
		{
			TargetingWnd.RemoveTargetOfTarget();
		}
		else if (!NetworkScript.player.shipScript.comm.gameObjects.TryGetValue(pvEPhysic.selectedPoPnbId, ref gameObjectPhysic))
		{
			TargetingWnd.RemoveTargetOfTarget();
		}
		else if (gameObjectPhysic.get_IsPoP())
		{
			TargetingWnd.CreateTargetOfTarget((PlayerObjectPhysics)gameObjectPhysic);
		}
		TargetingWnd.PopulateOptionMenu(pvEPhysic);
		string pveAvatarName = TargetingWnd.GetPveAvatarName(pvEPhysic.assetName);
		if (NetworkScript.player.vessel.galaxy.get_galaxyId() != 1000)
		{
			TargetingWnd.targetPvP_avatar.SetTextureKeepSize("Targeting", pveAvatarName);
		}
		else
		{
			TargetingWnd.targetPvP_avatar.SetTextureKeepSize("TutorialWindow", "AnnihilatorBoss");
		}
		string str1 = string.Format("fraction{0}Icon", pvEPhysic.fractionId);
		TargetingWnd.targetPvP_fraction.SetTextureKeepSize("FrameworkGUI", str1);
		float single = 1f * (float)pvEPhysic.cfg.hitPoints / (float)pvEPhysic.cfg.hitPointsMax;
		float single1 = 1f * pvEPhysic.cfg.shield / (float)pvEPhysic.cfg.shieldMax;
		TargetingWnd.targetPvP_barCorpus.boundries.set_width(single * 108f);
		TargetingWnd.targetPvP_barShield.boundries.set_width(single1 * 156f);
		if (!NetworkScript.player.playerBelongings.isShowMoreDetailsOn)
		{
			GuiLabel targetPvPCorpusValue = TargetingWnd.targetPvP_corpusValue;
			GuiLabel targetPvPCorpusValueShadow = TargetingWnd.targetPvP_corpusValueShadow;
			string empty = string.Empty;
			str = empty;
			targetPvPCorpusValueShadow.text = empty;
			targetPvPCorpusValue.text = str;
			GuiLabel targetPvPShieldValue = TargetingWnd.targetPvP_shieldValue;
			GuiLabel targetPvPShieldValueShadow = TargetingWnd.targetPvP_shieldValueShadow;
			string empty1 = string.Empty;
			str = empty1;
			targetPvPShieldValueShadow.text = empty1;
			targetPvPShieldValue.text = str;
		}
		else
		{
			GuiLabel guiLabel = TargetingWnd.targetPvP_corpusValue;
			GuiLabel targetPvPCorpusValueShadow1 = TargetingWnd.targetPvP_corpusValueShadow;
			string str2 = string.Format("{0:0} / {1:0}", pvEPhysic.cfg.hitPoints, pvEPhysic.cfg.hitPointsMax);
			str = str2;
			targetPvPCorpusValueShadow1.text = str2;
			guiLabel.text = str;
			GuiLabel targetPvPShieldValue1 = TargetingWnd.targetPvP_shieldValue;
			GuiLabel targetPvPShieldValueShadow1 = TargetingWnd.targetPvP_shieldValueShadow;
			string str3 = string.Format("{0:0} / {1:0}", pvEPhysic.cfg.shield, pvEPhysic.cfg.shieldMax);
			str = str3;
			targetPvPShieldValueShadow1.text = str3;
			targetPvPShieldValue1.text = str;
		}
		TargetingWnd.targetPvP_playerLevel.text = pvEPhysic.level.ToString();
		TargetingWnd.targetPvP_xp.text = string.Format(StaticData.Translate("key_pve_targeting_exp"), pvEPhysic.experience);
		TargetingWnd.PopulatePvePresetIcon(pvEPhysic);
		string str4 = StaticData.Translate(pvEPhysic.playerName);
		if (pvEPhysic.isMinion)
		{
			str4 = string.Format(StaticData.Translate("key_pve_minion "), str4);
		}
		if (str4.get_Length() > 21)
		{
			str4 = string.Concat(str4.Substring(0, 20), "...");
		}
		TargetingWnd.targetPvP_playerName.text = str4;
		TargetingWnd.targetPvP_playerName.TextColor = (pvEPhysic.agressionType != 0 || pvEPhysic.pveCommand == 1 ? GuiNewStyleBar.redColor : GuiNewStyleBar.orangeColor);
		TargetingWnd.targetPoP_actionBtn.eventHandlerParam.customData = pvEPhysic;
		if (!NetworkScript.player.vessel.isShooting || NetworkScript.player.vessel.shootingAt == null || NetworkScript.player.vessel.shootingAt.neighbourhoodId != pvEPhysic.neighbourhoodId)
		{
			TargetingWnd.targetPoP_shooting_indication.SetTextureKeepSize("FrameworkGUI", "empty");
		}
		else
		{
			TargetingWnd.targetPoP_shooting_indication.SetTextureKeepSize("TargetingGui", "fireIndication");
		}
	}

	private static void PopulateOptionMenu(PlayerObjectPhysics pop)
	{
		EventHandlerParam eventHandlerParam;
		if (TargetingWnd.targetPoP_option_window == null)
		{
			return;
		}
		bool flag = false;
		bool flag1 = false;
		bool flag2 = false;
		bool flag3 = false;
		flag = (!NetworkScript.player.vessel.isShooting || NetworkScript.player.vessel.shootingAt == null || NetworkScript.player.vessel.shootingAt.neighbourhoodId != pop.neighbourhoodId ? false : true);
		if (!pop.get_IsPve())
		{
			flag1 = true;
			flag2 = true;
			flag3 = true;
		}
		byte num = 0;
		if (flag)
		{
			num = (byte)(num + 1);
		}
		if (flag1)
		{
			num = (byte)(num + 1);
		}
		if (flag2)
		{
			num = (byte)(num + 1);
		}
		if (flag3)
		{
			num = (byte)(num + 1);
		}
		if (num == 0)
		{
			TargetingWnd.targetPoP_option_window.RemoveGuiElement(TargetingWnd.stopShoot);
			TargetingWnd.targetPoP_option_window.RemoveGuiElement(TargetingWnd.stopShootIcon);
			TargetingWnd.RemoveOptionMenu();
			return;
		}
		TargetingWnd.targetPoP_option_window.boundries = TargetingWnd.SetOptionMenuWindowPossition((int)num);
		if (!flag)
		{
			TargetingWnd.targetPoP_option_window.RemoveGuiElement(TargetingWnd.stopShoot);
			TargetingWnd.targetPoP_option_window.RemoveGuiElement(TargetingWnd.stopShootIcon);
		}
		else if (TargetingWnd.stopShoot == null || !TargetingWnd.targetPoP_option_window.HasGuiElement(TargetingWnd.stopShoot))
		{
			TargetingWnd.stopShoot = new GuiButtonFixed();
			TargetingWnd.stopShoot.SetTexture("TargetingGui", "optionsButton");
			TargetingWnd.stopShoot.X = 0f;
			TargetingWnd.stopShoot.Y = 0f;
			TargetingWnd.stopShoot.Caption = string.Empty;
			TargetingWnd.stopShoot.Clicked = new Action<EventHandlerParam>(null, TargetingWnd.OnStopFireBtnClicked);
			GuiButtonFixed guiButtonFixed = TargetingWnd.stopShoot;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_targeting_stop_fire"),
				customData2 = TargetingWnd.stopShoot
			};
			guiButtonFixed.tooltipWindowParam = eventHandlerParam;
			TargetingWnd.stopShoot.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			TargetingWnd.targetPoP_option_window.AddGuiElement(TargetingWnd.stopShoot);
			TargetingWnd.stopShootIcon = new GuiTexture();
			TargetingWnd.stopShootIcon.SetTexture("TargetingGui", "btnStopFire");
			TargetingWnd.stopShootIcon.X = 10f;
			TargetingWnd.stopShootIcon.Y = 14f;
			TargetingWnd.targetPoP_option_window.AddGuiElement(TargetingWnd.stopShootIcon);
		}
		if (flag1)
		{
			TargetingWnd.viewProfile.Y = (float)((!flag ? 0 : 40));
			TargetingWnd.viewProfileIcon.Y = TargetingWnd.viewProfile.Y + 14f;
		}
		if (flag2)
		{
			TargetingWnd.startChat.Y = (float)((!flag ? 40 : 80));
			TargetingWnd.startChatIcon.Y = TargetingWnd.startChat.Y + 14f;
			TargetingWnd.startChat.isEnabled = pop.playerId != NetworkScript.player.playId;
			GuiButtonFixed guiButtonFixed1 = TargetingWnd.startChat;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = pop.playerId
			};
			guiButtonFixed1.eventHandlerParam = eventHandlerParam;
			TargetingWnd.startChat.Clicked = new Action<EventHandlerParam>(null, TargetingWnd.OnChatBtnClick);
			GuiButtonFixed guiButtonFixed2 = TargetingWnd.startChat;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_targeting_btn_start_chat_tooltip"),
				customData2 = TargetingWnd.startChat
			};
			guiButtonFixed2.tooltipWindowParam = eventHandlerParam;
			TargetingWnd.startChat.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		}
		if (flag3)
		{
			TargetingWnd.inviteToParty.Y = (float)((!flag ? 80 : 120));
			TargetingWnd.inviteToPartyIcon.Y = TargetingWnd.inviteToParty.Y + 14f;
			TargetingWnd.inviteToParty.isEnabled = TargetingWnd.IsGoodToInvite(pop, TargetingWnd.inviteToParty);
			GuiButtonFixed guiButtonFixed3 = TargetingWnd.inviteToParty;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = pop.playerId
			};
			guiButtonFixed3.eventHandlerParam = eventHandlerParam;
			TargetingWnd.inviteToParty.Clicked = new Action<EventHandlerParam>(null, NetworkScript.OnPartyInviteClicked);
		}
	}

	private static void PopulatePlayer()
	{
		GameObjectPhysics gameObjectPhysic = null;
		if (TargetingWnd.targetSectionIndex == 0)
		{
			TargetingWnd.CreatePlayerTarget();
		}
		if (TargetingWnd.targetSectionIndex != 1)
		{
			AndromedaGui.gui.RemoveWindow(TargetingWnd.wndTargeting.handler);
			TargetingWnd.targetSectionIndex = 0;
			TargetingWnd.CreatePlayerTarget();
		}
		TargetingWnd.wndTargeting.boundries = TargetingWnd.SetTartetingWindowPossition();
		PlayerObjectPhysics playerObjectPhysic = (PlayerObjectPhysics)TargetingWnd.target;
		if (playerObjectPhysic.selectedPoPnbId != 0 && NetworkScript.player.shipScript.comm.gameObjects.TryGetValue(playerObjectPhysic.selectedPoPnbId, ref gameObjectPhysic))
		{
			TargetingWnd.targetOfTarget = (PlayerObjectPhysics)gameObjectPhysic;
			if (!(TargetingWnd.targetOfTarget is PvEPhysics))
			{
				Debug.Log(string.Format("{4}[{5}] Corpus {0}/{1} Shield {2}/{3}", new object[] { TargetingWnd.targetOfTarget.cfg.hitPoints, TargetingWnd.targetOfTarget.cfg.hitPointsMax, TargetingWnd.targetOfTarget.cfg.shield, TargetingWnd.targetOfTarget.cfg.shieldMax, TargetingWnd.targetOfTarget.playerName, TargetingWnd.targetOfTarget.cfg.playerLevel }));
			}
			else
			{
				Debug.Log(string.Format("{4}[{5}] Corpus {0}/{1} Shield {2}/{3}", new object[] { TargetingWnd.targetOfTarget.cfg.hitPoints, TargetingWnd.targetOfTarget.cfg.hitPointsMax, TargetingWnd.targetOfTarget.cfg.shield, TargetingWnd.targetOfTarget.cfg.shieldMax, TargetingWnd.targetOfTarget.playerName, TargetingWnd.targetOfTarget.cfg.playerLevel }));
			}
		}
		TargetingWnd.PopulateStopShootingBtn(playerObjectPhysic, 0f);
		Texture2D avatarOrStartIt = playWebGame.GetAvatarOrStartIt(playerObjectPhysic.playerAvatarUrl, new Action<AvatarJob>(null, TargetingWnd.OnAvatarLoadedCallback));
		if (avatarOrStartIt != null)
		{
			TargetingWnd.targetAvatar.boundries.set_x(10f);
			TargetingWnd.targetAvatar.boundries.set_y(15f);
			TargetingWnd.targetAvatar.SetSize(58f, 58f);
			TargetingWnd.targetAvatar.SetTextureKeepSize(avatarOrStartIt);
		}
		else
		{
			TargetingWnd.targetAvatar.boundries.set_x(12f);
			TargetingWnd.targetAvatar.boundries.set_y(16f);
			TargetingWnd.targetAvatar.SetSize(58f, 40f);
			TargetingWnd.currentTargetAvatar = playerObjectPhysic.playerAvatarUrl;
			TargetingWnd.targetAvatar.SetTextureKeepSize("ShipsAvatars", playerObjectPhysic.cfg.assetName);
		}
		TargetingWnd.fractionIcon.SetTextureKeepSize("FrameworkGUI", (playerObjectPhysic.fractionId != 1 ? "fraction2Icon" : "fraction1Icon"));
		TargetingWnd.barTargetHull.maximum = (float)playerObjectPhysic.cfg.hitPointsMax;
		TargetingWnd.barTargetHull.current = (float)playerObjectPhysic.cfg.hitPoints;
		TargetingWnd.barTargetShield.maximum = (float)playerObjectPhysic.cfg.shieldMax;
		TargetingWnd.barTargetShield.current = playerObjectPhysic.cfg.shield;
		TargetingWnd.pileName.text = (!(playerObjectPhysic.playerName == NetworkScript.player.playerBelongings.playerName) || !NetworkScript.player.vessel.isGuest ? playerObjectPhysic.playerName : StaticData.Translate("key_guest_player"));
		TargetingWnd.pileName.TextColor = TargetingWnd.GetPoPColor(playerObjectPhysic);
		TargetingWnd.pileAmount.text = StaticData.Translate(playerObjectPhysic.cfg.shipName);
		TargetingWnd.ownerLbl.text = string.Format(StaticData.Translate("key_targeting_level"), playerObjectPhysic.cfg.playerLevel);
		TargetingWnd.btnChat.isEnabled = TargetingWnd.IsGoodToChat;
		GuiButtonFixed guiButtonFixed = TargetingWnd.btnChat;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = playerObjectPhysic.playerId
		};
		guiButtonFixed.eventHandlerParam = eventHandlerParam;
		GuiButtonFixed guiButtonFixed1 = TargetingWnd.btnProfile;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = playerObjectPhysic.playerName
		};
		guiButtonFixed1.eventHandlerParam = eventHandlerParam;
		GuiLabel guiLabel = TargetingWnd.lblTargetHull;
		int num = playerObjectPhysic.cfg.hitPoints * 100 / playerObjectPhysic.cfg.hitPointsMax;
		guiLabel.text = string.Concat(num.ToString("##0"), " %");
		GuiLabel guiLabel1 = TargetingWnd.lblTargetShield;
		float single = playerObjectPhysic.cfg.shield * 100f / (float)playerObjectPhysic.cfg.shieldMax;
		guiLabel1.text = string.Concat(single.ToString("##0"), " %");
		if (playerObjectPhysic.fractionId == NetworkScript.player.vessel.fractionId)
		{
			if (TargetingWnd.btnInvite == null)
			{
				TargetingWnd.AddInviteToPartyBtn(TargetingWnd.wndTargeting);
			}
			TargetingWnd.btnInvite.isEnabled = TargetingWnd.IsGoodToInvite(playerObjectPhysic, TargetingWnd.btnInvite);
			GuiButtonFixed guiButtonFixed2 = TargetingWnd.btnInvite;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = playerObjectPhysic.playerId
			};
			guiButtonFixed2.eventHandlerParam = eventHandlerParam;
			TargetingWnd.btnInvite.Clicked = new Action<EventHandlerParam>(null, NetworkScript.OnPartyInviteClicked);
		}
		else
		{
			TargetingWnd.RemoveInviteToPartyBtn(TargetingWnd.wndTargeting);
		}
	}

	private static void PopulatePVE()
	{
		GameObjectPhysics gameObjectPhysic = null;
		if (TargetingWnd.targetSectionIndex == 0)
		{
			TargetingWnd.CreatePveTarget();
		}
		if (TargetingWnd.targetSectionIndex != 2)
		{
			AndromedaGui.gui.RemoveWindow(TargetingWnd.wndTargeting.handler);
			TargetingWnd.targetSectionIndex = 0;
			TargetingWnd.CreatePveTarget();
		}
		TargetingWnd.wndTargeting.boundries = TargetingWnd.SetTartetingWindowPossition();
		PvEPhysics pvEPhysic = (PvEPhysics)TargetingWnd.target;
		if (pvEPhysic.selectedPoPnbId != 0 && NetworkScript.player.shipScript.comm.gameObjects.TryGetValue(pvEPhysic.selectedPoPnbId, ref gameObjectPhysic))
		{
			TargetingWnd.targetOfTarget = (PlayerObjectPhysics)gameObjectPhysic;
			if (!(TargetingWnd.targetOfTarget is PvEPhysics))
			{
				Debug.Log(string.Format("{4}[{5}] Corpus {0}/{1} Shield {2}/{3}", new object[] { TargetingWnd.targetOfTarget.cfg.hitPoints, TargetingWnd.targetOfTarget.cfg.hitPointsMax, TargetingWnd.targetOfTarget.cfg.shield, TargetingWnd.targetOfTarget.cfg.shieldMax, TargetingWnd.targetOfTarget.playerName, TargetingWnd.targetOfTarget.cfg.playerLevel }));
			}
			else
			{
				Debug.Log(string.Format("{4}[{5}] Corpus {0}/{1} Shield {2}/{3}", new object[] { TargetingWnd.targetOfTarget.cfg.hitPoints, TargetingWnd.targetOfTarget.cfg.hitPointsMax, TargetingWnd.targetOfTarget.cfg.shield, TargetingWnd.targetOfTarget.cfg.shieldMax, TargetingWnd.targetOfTarget.playerName, TargetingWnd.targetOfTarget.cfg.playerLevel }));
			}
		}
		TargetingWnd.PopulateStopShootingBtn(pvEPhysic, 108f);
		TargetingWnd.PopulatePvePresetIcon(pvEPhysic);
		string pveAvatarName = TargetingWnd.GetPveAvatarName(pvEPhysic.assetName);
		if (NetworkScript.player.vessel.galaxy.get_galaxyId() != 1000)
		{
			TargetingWnd.targetAvatar.SetTextureKeepSize("Targeting", pveAvatarName);
		}
		else
		{
			TargetingWnd.targetAvatar.SetTextureKeepSize("TutorialWindow", "AnnihilatorBoss");
		}
		TargetingWnd.barTargetHull.maximum = (float)pvEPhysic.cfg.hitPointsMax;
		TargetingWnd.barTargetHull.current = (float)pvEPhysic.cfg.hitPoints;
		string str = StaticData.Translate(pvEPhysic.playerName);
		if (!pvEPhysic.isMinion)
		{
			TargetingWnd.pileName.text = str;
		}
		else
		{
			TargetingWnd.pileName.text = string.Format(StaticData.Translate("key_pve_minion "), str);
		}
		TargetingWnd.pileAmount.text = (pvEPhysic.agressionType != 0 ? StaticData.Translate("key_targeting_agressive") : StaticData.Translate("key_targeting_passive"));
		TargetingWnd.pileAmount.TextColor = (pvEPhysic.agressionType != 0 ? GuiNewStyleBar.redColor : GuiNewStyleBar.orangeColor);
		TargetingWnd.ownerLbl.text = string.Format(StaticData.Translate("key_targeting_level"), pvEPhysic.level);
		TargetingWnd.ownerName.text = string.Format(StaticData.Translate("key_targeting_exp"), pvEPhysic.experience);
		TargetingWnd.barTargetShield.maximum = (float)pvEPhysic.cfg.shieldMax;
		TargetingWnd.barTargetShield.current = pvEPhysic.cfg.shield;
		GuiLabel guiLabel = TargetingWnd.lblTargetHull;
		int num = pvEPhysic.cfg.hitPoints * 100 / pvEPhysic.cfg.hitPointsMax;
		guiLabel.text = string.Concat(num.ToString("##0"), " %");
		GuiLabel guiLabel1 = TargetingWnd.lblTargetShield;
		float single = pvEPhysic.cfg.shield * 100f / (float)pvEPhysic.cfg.shieldMax;
		guiLabel1.text = string.Concat(single.ToString("##0"), " %");
		if (pvEPhysic.fractionId == 0)
		{
			TargetingWnd.fractionIcon.SetTextureKeepSize("FrameworkGUI", "empty");
			TargetingWnd.pileName.X = 79f;
			TargetingWnd.ownerLbl.X = 79f;
			TargetingWnd.pileName.TextColor = GuiNewStyleBar.redColor;
		}
		else
		{
			TargetingWnd.fractionIcon.SetTextureKeepSize("FrameworkGUI", (pvEPhysic.fractionId != 1 ? "fraction2Icon" : "fraction1Icon"));
			TargetingWnd.pileName.X = 109f;
			TargetingWnd.ownerLbl.X = 109f;
			TargetingWnd.pileName.TextColor = (pvEPhysic.fractionId != NetworkScript.player.vessel.fractionId ? GuiNewStyleBar.redColor : GuiNewStyleBar.greenColor);
		}
	}

	private static void PopulatePvePresetIcon(PvEPhysics pve)
	{
		TargetingWnd.pvePresetOne.SetTextureKeepSize("FrameworkGUI", "empty");
		TargetingWnd.pvePresetTwo.SetTextureKeepSize("FrameworkGUI", "empty");
		TargetingWnd.pvePresetThree.SetTextureKeepSize("FrameworkGUI", "empty");
		TargetingWnd.pvePresetFour.SetTextureKeepSize("FrameworkGUI", "empty");
		TargetingWnd.pvePresetOne.drawTooltipWindowCallback = null;
		TargetingWnd.pvePresetTwo.drawTooltipWindowCallback = null;
		TargetingWnd.pvePresetThree.drawTooltipWindowCallback = null;
		TargetingWnd.pvePresetFour.drawTooltipWindowCallback = null;
		TargetingWnd.pvePressetOnePower.text = string.Empty;
		TargetingWnd.pvePressetTwoPower.text = string.Empty;
		TargetingWnd.pvePressetThreePower.text = string.Empty;
		TargetingWnd.pvePressetFourPower.text = string.Empty;
		int num = 1;
		if (pve.rocketeerPower != 0)
		{
			if (pve.fractionId == 0)
			{
				TargetingWnd.SetPresetIcon(num, "Rocketeer", pve.rocketeerPower, "key_target_enchant_rocketeer");
			}
			else
			{
				TargetingWnd.SetPresetIcon(num, "alienSkill-Rocketeer", pve.rocketeerPower, "key_target_enchant_rocketeer");
			}
			num++;
		}
		if (pve.shieldingPower != 0)
		{
			if (pve.fractionId == 0)
			{
				TargetingWnd.SetPresetIcon(num, "Shielding", pve.shieldingPower, "key_target_enchant_shielding");
			}
			else
			{
				TargetingWnd.SetPresetIcon(num, "alienSkill-Shielding", pve.shieldingPower, "key_target_enchant_shielding");
			}
			num++;
		}
		if (pve.repairMasterPower != 0)
		{
			if (pve.fractionId == 0)
			{
				TargetingWnd.SetPresetIcon(num, "RepairMaster", pve.repairMasterPower, "key_target_enchant_repair");
			}
			else
			{
				TargetingWnd.SetPresetIcon(num, "alienSkill-RepairMaster", pve.repairMasterPower, "key_target_enchant_repair");
			}
			num++;
		}
		if (pve.minionsPower != 0)
		{
			if (pve.fractionId == 0)
			{
				TargetingWnd.SetPresetIcon(num, "Minions", pve.minionsPower, "key_target_enchant_minions");
			}
			else
			{
				TargetingWnd.SetPresetIcon(num, "alienSkill-Minions", pve.minionsPower, "key_target_enchant_minions");
			}
			num++;
		}
		if (pve.repairingDronesPower != 0)
		{
			TargetingWnd.SetPresetIcon(num, "alienSkill-RepairingDrones", pve.repairingDronesPower, "key_target_enchant_repairing_drones");
			num++;
		}
		if (pve.enforcerPower != 0)
		{
			TargetingWnd.SetPresetIcon(num, "Enforcer", pve.enforcerPower, "key_target_enchant_enforcer");
			num++;
		}
		if (pve.stormerPower != 0)
		{
			if (pve.fractionId == 0)
			{
				TargetingWnd.SetPresetIcon(num, "Stormer", pve.stormerPower, "key_target_enchant_stormer");
			}
			else
			{
				TargetingWnd.SetPresetIcon(num, "alienSkill-Stormer", pve.stormerPower, "key_target_enchant_stormer");
			}
			num++;
		}
		if (pve.disablerPower != 0)
		{
			if (pve.fractionId == 0)
			{
				TargetingWnd.SetPresetIcon(num, "Disabler", pve.disablerPower, "key_target_enchant_disabler");
			}
			else
			{
				TargetingWnd.SetPresetIcon(num, "alienSkill-Disabler", pve.disablerPower, "key_target_enchant_disabler");
			}
			num++;
		}
		if (pve.unstoppablePower != 0)
		{
			TargetingWnd.SetPresetIcon(num, "alienSkill-Unstoppable", pve.unstoppablePower, "key_target_unstoppable");
			num++;
		}
		if (pve.ultimateEnforcerPower != 0)
		{
			TargetingWnd.SetPresetIcon(num, "alienSkill-UltimateEnforcer", pve.ultimateEnforcerPower, "key_target_ultimate_enforcer");
			num++;
		}
		if (pve.remedyPower != 0)
		{
			TargetingWnd.SetPresetIcon(num, "alienSkill-Remedy", pve.remedyPower, "key_target_remedy");
			num++;
		}
		if (pve.powerBreakerPower != 0)
		{
			TargetingWnd.SetPresetIcon(num, "alienSkill-PowerBreaker", pve.powerBreakerPower, "key_target_power_breaker");
			num++;
		}
		if (pve.shieldFortressPower != 0)
		{
			TargetingWnd.SetPresetIcon(num, "alienSkill-ShieldFortress", pve.shieldFortressPower, "key_target_shield_fortress");
			num++;
		}
		if (pve.ultimateRocketeerPower != 0)
		{
			TargetingWnd.SetPresetIcon(num, "alienSkill-UltimateRocketeer", pve.ultimateRocketeerPower, "key_target_ultimate_rocketeer");
			num++;
		}
	}

	private static void PopulateStopShootingBtn(GameObjectPhysics gop, float offsetX)
	{
		if (TargetingWnd.wndTargeting == null || gop == null)
		{
			return;
		}
		if (TargetingWnd.targetSectionIndex == 1 || TargetingWnd.targetSectionIndex == 2 || TargetingWnd.targetSectionIndex == 7)
		{
			if (NetworkScript.player.vessel.isShooting && NetworkScript.player.vessel.shootingAt != null && NetworkScript.player.vessel.shootingAt.neighbourhoodId == gop.neighbourhoodId)
			{
				if (TargetingWnd.btnStopFire == null || !TargetingWnd.wndTargeting.HasGuiElement(TargetingWnd.btnStopFire))
				{
					TargetingWnd.btnStopFire = new GuiButtonFixed()
					{
						X = 297f + offsetX,
						Y = 40f
					};
					TargetingWnd.btnStopFire.SetTexture("MainScreenWindow", "targeting_autofire");
					TargetingWnd.btnStopFire.Caption = string.Empty;
					TargetingWnd.btnStopFire.isEnabled = true;
					TargetingWnd.btnStopFire.Clicked = new Action<EventHandlerParam>(null, TargetingWnd.OnStopFireBtnClicked);
					GuiButtonFixed guiButtonFixed = TargetingWnd.btnStopFire;
					EventHandlerParam eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_targeting_stop_fire"),
						customData2 = TargetingWnd.btnStopFire
					};
					guiButtonFixed.tooltipWindowParam = eventHandlerParam;
					TargetingWnd.btnStopFire.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
					TargetingWnd.wndTargeting.AddGuiElement(TargetingWnd.btnStopFire);
				}
			}
			else if (TargetingWnd.btnStopFire != null)
			{
				TargetingWnd.wndTargeting.RemoveGuiElement(TargetingWnd.btnStopFire);
				TargetingWnd.btnStopFire = null;
			}
		}
	}

	private static void PopulateTargetOfTarget(PlayerObjectPhysics target)
	{
		if (target.get_IsPve())
		{
			string pveAvatarName = TargetingWnd.GetPveAvatarName(target.assetName);
			if (NetworkScript.player.vessel.galaxy.get_galaxyId() != 1000)
			{
				TargetingWnd.targetOfTaret_avatar.SetTextureKeepSize("Targeting", pveAvatarName);
			}
			else
			{
				TargetingWnd.targetOfTaret_avatar.SetTextureKeepSize("TutorialWindow", "AnnihilatorBoss");
			}
			string str = StaticData.Translate(target.playerName);
			if (!((PvEPhysics)target).isMinion)
			{
				TargetingWnd.targetOfTaret_name.text = str;
			}
			else
			{
				TargetingWnd.targetOfTaret_name.text = string.Format(StaticData.Translate("key_pve_minion "), str);
			}
			TargetingWnd.targetOfTaret_name.TextColor = (((PvEPhysics)target).agressionType != 0 ? GuiNewStyleBar.redColor : GuiNewStyleBar.orangeColor);
		}
		else
		{
			Texture2D avatarOrStartIt = playWebGame.GetAvatarOrStartIt(target.playerAvatarUrl, new Action<AvatarJob>(null, TargetingWnd.OnAvatarLoadedCallback), TargetingWnd.targetOfTaret_avatar);
			if (avatarOrStartIt != null)
			{
				TargetingWnd.targetOfTaret_avatar.boundries.set_x(142f);
				TargetingWnd.targetOfTaret_avatar.boundries.set_y(5f);
				TargetingWnd.targetOfTaret_avatar.SetSize(45f, 45f);
				TargetingWnd.targetOfTaret_avatar.SetTextureKeepSize(avatarOrStartIt);
			}
			else
			{
				TargetingWnd.targetOfTaret_avatar.boundries.set_x(142f);
				TargetingWnd.targetOfTaret_avatar.boundries.set_y(12f);
				TargetingWnd.targetOfTaret_avatar.SetSize(45f, 31f);
				TargetingWnd.currentTargetAvatar = target.playerAvatarUrl;
				TargetingWnd.targetOfTaret_avatar.SetTextureKeepSize("ShipsAvatars", target.cfg.assetName);
			}
			TargetingWnd.targetOfTaret_name.text = (!(target.playerName == NetworkScript.player.playerBelongings.playerName) || !NetworkScript.player.vessel.isGuest ? target.playerName : StaticData.Translate("key_guest_player"));
			if (NetworkScript.player.pvpGame == null || NetworkScript.player.pvpGame.state != 1)
			{
				TargetingWnd.targetOfTaret_name.TextColor = (target.fractionId != NetworkScript.player.vessel.fractionId ? GuiNewStyleBar.redColor : GuiNewStyleBar.greenColor);
			}
			else if (target.teamNumber != NetworkScript.player.vessel.teamNumber)
			{
				TargetingWnd.targetOfTaret_name.TextColor = GuiNewStyleBar.redColor;
			}
			else
			{
				TargetingWnd.targetOfTaret_name.TextColor = GuiNewStyleBar.greenColor;
			}
		}
		string str1 = string.Format("fraction{0}Icon", target.fractionId);
		TargetingWnd.targetOfTaret_fraction.SetTextureKeepSize("FrameworkGUI", str1);
		float single = 1f * (float)target.cfg.hitPoints / (float)target.cfg.hitPointsMax;
		float single1 = 1f * target.cfg.shield / (float)target.cfg.shieldMax;
		TargetingWnd.targetOfTaret_barCorpus.boundries.set_width(single * 88f);
		TargetingWnd.targetOfTaret_barShield.boundries.set_width(single1 * 126f);
		TargetingWnd.targetOfTaret_barCorpus.boundries.set_x(136f - TargetingWnd.targetOfTaret_barCorpus.boundries.get_width());
		TargetingWnd.targetOfTaret_barShield.boundries.set_x(136f - TargetingWnd.targetOfTaret_barShield.boundries.get_width());
		if (!target.get_IsPve())
		{
			TargetingWnd.targetOfTaret_lavel.text = target.cfg.playerLevel.ToString();
		}
		else
		{
			TargetingWnd.targetOfTaret_lavel.text = ((PvEPhysics)target).level.ToString();
		}
	}

	public static void Remove()
	{
		TargetingWnd.RemoveTargetOfTarget();
		if (TargetingWnd.wndTargeting != null)
		{
			AndromedaGui.gui.RemoveWindow(TargetingWnd.wndTargeting.handler);
			TargetingWnd.targetSectionIndex = 0;
		}
	}

	private static void RemoveInviteToPartyBtn(GuiWindow wnd)
	{
		if (TargetingWnd.btnInvite != null)
		{
			wnd.RemoveGuiElement(TargetingWnd.btnInvite);
			TargetingWnd.btnInvite = null;
		}
	}

	private static void RemoveOptionMenu()
	{
		if (TargetingWnd.targetPoP_option_window != null)
		{
			AndromedaGui.gui.RemoveWindow(TargetingWnd.targetPoP_option_window.handler);
			TargetingWnd.targetPoP_option_window = null;
			TargetingWnd.optionMenuTargetNbId = 0;
		}
	}

	private static void RemoveTargetOfTarget()
	{
		if (TargetingWnd.targetOfTaret_window != null)
		{
			AndromedaGui.gui.RemoveWindow(TargetingWnd.targetOfTaret_window.handler);
			TargetingWnd.targetOfTaret_window = null;
		}
	}

	public static void ReOrderGui()
	{
		TargetingWnd.RemoveOptionMenu();
		if (TargetingWnd.targetSectionIndex == 1 || TargetingWnd.targetSectionIndex == 2)
		{
			TargetingWnd.wndTargeting.boundries = TargetingWnd.SetPoPTartetingWindowPossition();
		}
		else if (TargetingWnd.wndTargeting != null)
		{
			TargetingWnd.wndTargeting.boundries = TargetingWnd.SetTartetingWindowPossition();
		}
		if (TargetingWnd.targetOfTaret_window != null)
		{
			TargetingWnd.targetOfTaret_window.boundries = TargetingWnd.SetTargetOfTargetWindowPossition();
		}
	}

	private static Rect SetOptionMenuWindowPossition(int btnCnt)
	{
		if (NetworkScript.player == null || (NetworkScript.player.shipScript.comm.galaxy.__galaxyId <= 1000 || NetworkScript.player.shipScript.comm.galaxy.__galaxyId >= 2000) && (NetworkScript.player.vessel == null || NetworkScript.player.vessel.pvpState != 3 && NetworkScript.player.vessel.pvpState != 4 || NetworkScript.player.pvpGame.gameType.selectedMap == null || NetworkScript.player.pvpGame.gameType.selectedMap.galaxyId != NetworkScript.player.vessel.galaxy.__galaxyId))
		{
			return new Rect((float)(Screen.get_width() / 2 - 115), 116f, 46f, (float)(btnCnt * 40 + 5));
		}
		float single = 38f;
		return new Rect((float)(Screen.get_width() / 2 - 115), 116f + single, 46f, (float)(btnCnt * 40 + 5));
	}

	private static Rect SetPoPTartetingWindowPossition()
	{
		if (NetworkScript.player == null || (NetworkScript.player.shipScript.comm.galaxy.__galaxyId <= 1000 || NetworkScript.player.shipScript.comm.galaxy.__galaxyId >= 2000) && (NetworkScript.player.vessel == null || NetworkScript.player.vessel.pvpState != 3 && NetworkScript.player.vessel.pvpState != 4 || NetworkScript.player.pvpGame.gameType.selectedMap == null || NetworkScript.player.pvpGame.gameType.selectedMap.galaxyId != NetworkScript.player.vessel.galaxy.__galaxyId))
		{
			if (TargetingWnd.targetSectionIndex == 1)
			{
				return new Rect((float)(Screen.get_width() / 2 - 145), 45f, 290f, 71f);
			}
			return new Rect((float)(Screen.get_width() / 2 - 215), 45f, 360f, 71f);
		}
		float single = 38f;
		if (TargetingWnd.targetSectionIndex == 1)
		{
			return new Rect((float)(Screen.get_width() / 2 - 145), 45f + single, 290f, 71f);
		}
		return new Rect((float)(Screen.get_width() / 2 - 215), 45f + single, 360f, 71f);
	}

	private static void SetPresetIcon(int index, string assetname, byte power, string tooltipText)
	{
		EventHandlerParam eventHandlerParam;
		switch (index)
		{
			case 1:
			{
				TargetingWnd.pvePresetOne.SetTextureKeepSize("BossPresets", assetname);
				GuiTexture guiTexture = TargetingWnd.pvePresetOne;
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate(tooltipText),
					customData2 = TargetingWnd.pvePresetOne
				};
				guiTexture.tooltipWindowParam = eventHandlerParam;
				TargetingWnd.pvePresetOne.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				TargetingWnd.pvePressetOnePower.text = power.ToString();
				break;
			}
			case 2:
			{
				TargetingWnd.pvePresetTwo.SetTextureKeepSize("BossPresets", assetname);
				GuiTexture guiTexture1 = TargetingWnd.pvePresetTwo;
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate(tooltipText),
					customData2 = TargetingWnd.pvePresetTwo
				};
				guiTexture1.tooltipWindowParam = eventHandlerParam;
				TargetingWnd.pvePresetTwo.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				TargetingWnd.pvePressetTwoPower.text = power.ToString();
				break;
			}
			case 3:
			{
				TargetingWnd.pvePresetThree.SetTextureKeepSize("BossPresets", assetname);
				GuiTexture guiTexture2 = TargetingWnd.pvePresetThree;
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate(tooltipText),
					customData2 = TargetingWnd.pvePresetThree
				};
				guiTexture2.tooltipWindowParam = eventHandlerParam;
				TargetingWnd.pvePresetThree.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				TargetingWnd.pvePressetThreePower.text = power.ToString();
				break;
			}
			case 4:
			{
				TargetingWnd.pvePresetFour.SetTextureKeepSize("BossPresets", assetname);
				GuiTexture guiTexture3 = TargetingWnd.pvePresetFour;
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate(tooltipText),
					customData2 = TargetingWnd.pvePresetFour
				};
				guiTexture3.tooltipWindowParam = eventHandlerParam;
				TargetingWnd.pvePresetFour.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				TargetingWnd.pvePressetFourPower.text = power.ToString();
				break;
			}
		}
	}

	private static Rect SetTargetOfTargetWindowPossition()
	{
		if (NetworkScript.player == null || (NetworkScript.player.shipScript.comm.galaxy.__galaxyId <= 1000 || NetworkScript.player.shipScript.comm.galaxy.__galaxyId >= 2000) && (NetworkScript.player.vessel == null || NetworkScript.player.vessel.pvpState != 3 && NetworkScript.player.vessel.pvpState != 4 || NetworkScript.player.pvpGame.gameType.selectedMap == null || NetworkScript.player.pvpGame.gameType.selectedMap.galaxyId != NetworkScript.player.vessel.galaxy.__galaxyId))
		{
			return new Rect((float)(Screen.get_width() / 2 + 50), 102f, 210f, 57f);
		}
		float single = 38f;
		return new Rect((float)(Screen.get_width() / 2 + 50), 102f + single, 210f, 57f);
	}

	private static Rect SetTartetingWindowPossition()
	{
		if (NetworkScript.player != null && (NetworkScript.player.shipScript.comm.galaxy.__galaxyId > 1000 && NetworkScript.player.shipScript.comm.galaxy.__galaxyId < 2000 || NetworkScript.player.vessel != null && (NetworkScript.player.vessel.pvpState == 3 || NetworkScript.player.vessel.pvpState == 4) && NetworkScript.player.pvpGame.gameType.selectedMap != null && NetworkScript.player.pvpGame.gameType.selectedMap.galaxyId == NetworkScript.player.vessel.galaxy.__galaxyId))
		{
			return new Rect((float)(Screen.get_width() / 2 - 225), 73f, 467f, 100f);
		}
		return new Rect((float)(Screen.get_width() / 2 - 225), 36f, 467f, 100f);
	}
}