using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TransferableObjects;
using UnityEngine;

public class PVPWindow : GuiWindow
{
	private List<GuiElement> pvpStatsForDelete = new List<GuiElement>();

	private GuiWindow dialogWindow;

	public static short signedPlayers;

	public RankingData rankingData;

	private List<GuiElement> pvpDominationGuiForDelete = new List<GuiElement>();

	private byte selectedScreen;

	private bool isWinnersListRequested;

	public PVPWindow()
	{
	}

	private void ClearPvPDominationGui()
	{
		foreach (GuiElement guiElement in this.pvpDominationGuiForDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.pvpDominationGuiForDelete.Clear();
	}

	public override void Create()
	{
		if (NetworkScript.player.vessel.pvpState == 2)
		{
			return;
		}
		base.SetBackgroundTexture("PvPDominationGui", "pvpScoreFrame");
		base.PutToCenter();
		this.zOrder = 210;
		this.isHidden = false;
		if ((NetworkScript.player.vessel.pvpState == 3 || NetworkScript.player.vessel.pvpState == 5 || NetworkScript.player.vessel.pvpState == 4) && NetworkScript.player.pvpGame != null)
		{
			if (NetworkScript.player.pvpGame.gameType.winType == null)
			{
				this.CreatePvPWindowInDominationGame();
			}
			return;
		}
		if (NetworkScript.player.vessel.pvpState != null && NetworkScript.player.vessel.pvpState != 1)
		{
			return;
		}
		this.CreatePvPDomiantionSignUp(null);
	}

	private void CreatePvPDomiantionSignUp(object prm = null)
	{
		this.selectedScreen = 0;
		base.SetBackgroundTexture("PvPDominationGui", "pvp_domination_frame");
		base.PutToCenter();
		this.zOrder = 210;
		this.isHidden = false;
		this.ClearPvPDominationGui();
		PvPGameType pvPGameType = Enumerable.First<PvPGameType>(StaticData.pvpGameTypes);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("PvPDominationGui", "pvpSighnupBackground");
		guiTexture.X = 179f;
		guiTexture.Y = 118f;
		base.AddGuiElement(guiTexture);
		this.pvpDominationGuiForDelete.Add(guiTexture);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(277f, 25f, 466f, 36f),
			text = StaticData.Translate("key_pvp_window_title_signup"),
			FontSize = 20,
			Alignment = 4,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor
		};
		base.AddGuiElement(guiLabel);
		this.pvpDominationGuiForDelete.Add(guiLabel);
		this.CreatePvPLeagueBtns(0);
		this.DrawPvPGameRewards(!NetworkScript.player.playerBelongings.firstWinBonusRecived);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("PvPDominationGui", "btnPvpSignupGreen");
		guiButtonFixed.X = 221f;
		guiButtonFixed.Y = 511f;
		guiButtonFixed.Caption = StaticData.Translate("key_pvp_btn_caption_signup");
		guiButtonFixed.Alignment = 4;
		guiButtonFixed.eventHandlerParam.customData = pvPGameType;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, PVPWindow.SignForPvP);
		base.AddGuiElement(guiButtonFixed);
		this.pvpDominationGuiForDelete.Add(guiButtonFixed);
		DateTime dateTime = NetworkScript.player.playerBelongings.penaltyTime.AddMinutes((double)NetworkScript.player.playerBelongings.penaltyDuration);
		if (dateTime > StaticData.now)
		{
			guiButtonFixed.isEnabled = false;
			GuiTexture guiTexture1 = new GuiTexture();
			guiTexture1.SetTexture("PvPDominationGui", "warning");
			guiTexture1.X = 192f;
			guiTexture1.Y = 464f;
			base.AddGuiElement(guiTexture1);
			this.pvpDominationGuiForDelete.Add(guiTexture1);
			GuiLabel guiLabel1 = new GuiLabel()
			{
				boundries = new Rect(198f, 470f, 380f, 40f),
				text = StaticData.Translate("key_pvp_signup_deserter_penalty"),
				FontSize = 12,
				Font = GuiLabel.FontBold,
				TextColor = GuiNewStyleBar.redColor,
				Alignment = 1
			};
			guiLabel1.boundries.set_height(guiLabel1.TextHeight);
			base.AddGuiElement(guiLabel1);
			this.pvpDominationGuiForDelete.Add(guiLabel1);
			TimeSpan timeSpan = dateTime - StaticData.now;
			GuiTimeTracker guiTimeTracker = new GuiTimeTracker((int)timeSpan.get_TotalSeconds(), this)
			{
				FontSize = 14,
				TextColor = GuiNewStyleBar.redColor,
				Font = GuiLabel.FontBold,
				boundries = new Rect(198f, 472f + guiLabel1.TextHeight, 380f, 40f),
				Alignment = 1
			};
			guiTimeTracker.SetEndAction(new Action(this, PVPWindow.OnPenaltyEnd));
			this.pvpDominationGuiForDelete.Add(guiTimeTracker);
		}
		if (NetworkScript.player.pvpGameTypeSignedFor != 0)
		{
			guiButtonFixed.SetTextureNormal("PvPDominationGui", "btnPvpSignupOrangeNml");
			guiButtonFixed.SetTextureHover("PvPDominationGui", "btnPvpSignupOrangeHvr");
			guiButtonFixed.SetTextureClicked("PvPDominationGui", "btnPvpSignupOrangeNml");
			guiButtonFixed.Caption = StaticData.Translate("key_pvp_btn_caption_signout");
			guiButtonFixed.Alignment = 4;
			guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, PVPWindow.SignOutFromPvP);
			GuiLabel guiLabel2 = new GuiLabel()
			{
				boundries = new Rect(388f, 468f, 180f, 40f),
				text = string.Format(StaticData.Translate("key_pvp_lbl_avarage_waiting_time"), "- ", " -"),
				Alignment = 5,
				FontSize = 14,
				Font = GuiLabel.FontBold,
				TextColor = GuiNewStyleBar.aquamarineColor
			};
			base.AddGuiElement(guiLabel2);
			this.pvpDominationGuiForDelete.Add(guiLabel2);
			int totalSeconds = 0;
			if (NetworkScript.player.playerBelongings.pvpGamePoolCapacity != 0)
			{
				TimeSpan now = DateTime.get_Now() - NetworkScript.player.playerBelongings.oldestPvPGameStartTime;
				totalSeconds = (int)(now.get_TotalSeconds() / (double)NetworkScript.player.playerBelongings.pvpGamePoolCapacity * 2.5);
			}
			else
			{
				totalSeconds = 3600;
			}
			if (totalSeconds > 3600)
			{
				totalSeconds = 3600;
			}
			if (totalSeconds < 60)
			{
				totalSeconds = 60;
			}
			string str = StaticData.Translate("key_pvp_lbl_avarage_waiting_time");
			int num = totalSeconds / 60;
			guiLabel2.text = string.Format(str, num.ToString("00"), "00");
			TimeSpan now1 = DateTime.get_Now() - NetworkScript.player.playerBelongings.signingPvPGameTime;
			GuiTimer guiTimer = new GuiTimer((int)now1.get_TotalSeconds())
			{
				boundries = new Rect(208f, 468f, 180f, 40f),
				Font = GuiLabel.FontBold,
				TextColor = GuiNewStyleBar.aquamarineColor,
				Alignment = 3,
				FontSize = 14
			};
			base.AddGuiElement(guiTimer);
			this.pvpDominationGuiForDelete.Add(guiTimer);
		}
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("MiniMaps", pvPGameType.sceneName);
		rect.boundries = new Rect(605f, 148f, 200f, 133f);
		base.AddGuiElement(rect);
		this.pvpDominationGuiForDelete.Add(rect);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(605f, 297f, 200f, 220f),
			text = string.Format(StaticData.Translate(pvPGameType.description), pvPGameType.winParam),
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(guiLabel3);
		this.pvpDominationGuiForDelete.Add(guiLabel3);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			boundries = new Rect(605f, 525f, 200f, 30f),
			text = string.Format(StaticData.Translate("key_pvp_signed_up_screen_queue_size"), PVPWindow.signedPlayers.ToString("##,##0")),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor,
			Alignment = 4
		};
		base.AddGuiElement(guiLabel4);
		this.pvpDominationGuiForDelete.Add(guiLabel4);
		GuiLabel guiLabel5 = new GuiLabel()
		{
			boundries = new Rect(209f, 160f, 370f, 28f),
			text = StaticData.Translate(pvPGameType.name).ToUpper(),
			Alignment = 4,
			FontSize = 18,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor
		};
		base.AddGuiElement(guiLabel5);
		this.pvpDominationGuiForDelete.Add(guiLabel5);
		GuiLabel guiLabel6 = new GuiLabel()
		{
			boundries = new Rect(304f, 199f, 180f, 28f),
			text = this.GetGamePlayerLimit(pvPGameType),
			Alignment = 4,
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor
		};
		base.AddGuiElement(guiLabel6);
		this.pvpDominationGuiForDelete.Add(guiLabel6);
		List<GuiElement> list = new List<GuiElement>();
		if (NetworkScript.party != null && NetworkScript.party.members.get_Count() == 4)
		{
			GuiTexture guiTexture2 = new GuiTexture();
			guiTexture2.SetTexture("PvPDominationGui", "warning");
			guiTexture2.X = 192f;
			guiTexture2.Y = 464f;
			base.AddGuiElement(guiTexture2);
			this.pvpDominationGuiForDelete.Add(guiTexture2);
			list.Add(guiTexture2);
			GuiLabel guiLabel7 = new GuiLabel()
			{
				boundries = new Rect(198f, 470f, 380f, 40f),
				text = StaticData.Translate("key_pvp_sign_up_error_party_member_cnt"),
				FontSize = 12,
				Font = GuiLabel.FontBold,
				TextColor = GuiNewStyleBar.redColor
			};
			base.AddGuiElement(guiLabel7);
			this.pvpDominationGuiForDelete.Add(guiLabel7);
			list.Add(guiLabel7);
			guiButtonFixed.isEnabled = false;
		}
		if (prm != null && prm is string)
		{
			foreach (GuiElement guiElement in list)
			{
				base.RemoveGuiElement(guiElement);
				this.pvpDominationGuiForDelete.Remove(guiElement);
			}
			list.Clear();
			string str1 = (string)prm;
			if (!string.IsNullOrEmpty(str1))
			{
				GuiTexture guiTexture3 = new GuiTexture();
				guiTexture3.SetTexture("PvPDominationGui", "warning");
				guiTexture3.X = 192f;
				guiTexture3.Y = 464f;
				base.AddGuiElement(guiTexture3);
				this.pvpDominationGuiForDelete.Add(guiTexture3);
				GuiLabel guiLabel8 = new GuiLabel()
				{
					boundries = new Rect(198f, 470f, 380f, 40f),
					text = str1,
					FontSize = 12,
					Font = GuiLabel.FontBold,
					TextColor = GuiNewStyleBar.redColor
				};
				base.AddGuiElement(guiLabel8);
				this.pvpDominationGuiForDelete.Add(guiLabel8);
				guiButtonFixed.isEnabled = false;
			}
		}
	}

	private void CreatePvPDominationInfo(object prm = null)
	{
		this.ClearPvPDominationGui();
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("PvPDominationGui", "btnBack");
		guiButtonFixed.X = 185f;
		guiButtonFixed.Y = 25f;
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, PVPWindow.OnBackBtnClicked);
		base.AddGuiElement(guiButtonFixed);
		this.pvpDominationGuiForDelete.Add(guiButtonFixed);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(277f, 25f, 466f, 36f),
			text = StaticData.Translate("key_pvp_window_title_info"),
			FontSize = 20,
			Alignment = 4,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor
		};
		base.AddGuiElement(guiLabel);
		this.pvpDominationGuiForDelete.Add(guiLabel);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("PvPDominationGui", "infoBackground");
		guiTexture.X = 184f;
		guiTexture.Y = 118f;
		base.AddGuiElement(guiTexture);
		this.pvpDominationGuiForDelete.Add(guiTexture);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(229f, 106f, 560f, 70f),
			text = StaticData.Translate("key_pvp_info_screen_details"),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor
		};
		base.AddGuiElement(guiLabel1);
		this.pvpDominationGuiForDelete.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(225f, 259f, 405f, 30f),
			text = StaticData.Translate("key_pvp_info_screen_rewards_chart"),
			FontSize = 14,
			Alignment = 3,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor
		};
		base.AddGuiElement(guiLabel2);
		this.pvpDominationGuiForDelete.Add(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(225f, 530f, 572f, 30f),
			text = StaticData.Translate("key_pvp_info_screen_next_round_time"),
			FontSize = 16,
			Alignment = 4,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor
		};
		guiLabel3.boundries.set_width(guiLabel3.TextWidth);
		base.AddGuiElement(guiLabel3);
		this.pvpDominationGuiForDelete.Add(guiLabel3);
		TimeSpan timeSpan = NetworkScript.player.playerBelongings.nextPvPRoundTime - StaticData.now;
		if (timeSpan.get_TotalSeconds() > 1)
		{
			GuiTimeTracker guiTimeTracker = new GuiTimeTracker((int)timeSpan.get_TotalSeconds(), this)
			{
				FontSize = 16,
				TextColor = GuiNewStyleBar.aquamarineColor,
				Font = GuiLabel.FontBold,
				boundries = new Rect(225f, 530f, 65f, 30f),
				Alignment = 5
			};
			guiTimeTracker.SetEndAction(new Action(this, PVPWindow.OnPenaltyEnd));
			this.pvpDominationGuiForDelete.Add(guiTimeTracker);
			float textWidth = (572f - guiLabel3.TextWidth - 65f) / 2f;
			guiLabel3.X = 225f + textWidth;
			guiTimeTracker.X = guiLabel3.X + guiLabel3.boundries.get_width();
		}
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("PvPDominationGui", "tab_bronzeClk");
		guiTexture1.X = 214f;
		guiTexture1.Y = 194f;
		base.AddGuiElement(guiTexture1);
		this.pvpDominationGuiForDelete.Add(guiTexture1);
		GuiTexture guiTexture2 = new GuiTexture();
		guiTexture2.SetTexture("PvPDominationGui", "arrows");
		guiTexture2.X = 397f;
		guiTexture2.Y = 212f;
		base.AddGuiElement(guiTexture2);
		this.pvpDominationGuiForDelete.Add(guiTexture2);
		GuiTexture guiTexture3 = new GuiTexture();
		guiTexture3.SetTexture("PvPDominationGui", "tab_silverClk");
		guiTexture3.X = 424f;
		guiTexture3.Y = 194f;
		base.AddGuiElement(guiTexture3);
		this.pvpDominationGuiForDelete.Add(guiTexture3);
		GuiTexture guiTexture4 = new GuiTexture();
		guiTexture4.SetTexture("PvPDominationGui", "arrows");
		guiTexture4.X = 605f;
		guiTexture4.Y = 212f;
		base.AddGuiElement(guiTexture4);
		this.pvpDominationGuiForDelete.Add(guiTexture4);
		GuiTexture guiTexture5 = new GuiTexture();
		guiTexture5.SetTexture("PvPDominationGui", "tab_goldClk");
		guiTexture5.X = 631f;
		guiTexture5.Y = 194f;
		base.AddGuiElement(guiTexture5);
		this.pvpDominationGuiForDelete.Add(guiTexture5);
		GuiLabel fontBold = new GuiLabel()
		{
			boundries = guiTexture1.boundries
		};
		GuiLabel x = fontBold;
		x.X = x.X + 1f;
		GuiLabel y = fontBold;
		y.Y = y.Y + 1f;
		fontBold.text = StaticData.Translate("key_pvp_league_title_bronze");
		fontBold.FontSize = 14;
		fontBold.Alignment = 4;
		fontBold.Font = GuiLabel.FontBold;
		fontBold.TextColor = GuiNewStyleBar.bronzeColor;
		base.AddGuiElement(fontBold);
		this.pvpDominationGuiForDelete.Add(fontBold);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			boundries = guiTexture1.boundries,
			text = StaticData.Translate("key_pvp_league_title_bronze"),
			FontSize = 14,
			Alignment = 4,
			Font = GuiLabel.FontBold,
			TextColor = new Color(0.2617f, 0.1171f, 0.0078f)
		};
		base.AddGuiElement(guiLabel4);
		this.pvpDominationGuiForDelete.Add(guiLabel4);
		GuiLabel fontBold1 = new GuiLabel()
		{
			boundries = guiTexture3.boundries
		};
		GuiLabel x1 = fontBold1;
		x1.X = x1.X + 1f;
		GuiLabel y1 = fontBold1;
		y1.Y = y1.Y + 1f;
		fontBold1.text = StaticData.Translate("key_pvp_league_title_silver");
		fontBold1.FontSize = 14;
		fontBold1.Alignment = 4;
		fontBold1.Font = GuiLabel.FontBold;
		fontBold1.TextColor = GuiNewStyleBar.silverColor;
		base.AddGuiElement(fontBold1);
		this.pvpDominationGuiForDelete.Add(fontBold1);
		GuiLabel guiLabel5 = new GuiLabel()
		{
			boundries = guiTexture3.boundries,
			text = StaticData.Translate("key_pvp_league_title_silver"),
			FontSize = 14,
			Alignment = 4,
			Font = GuiLabel.FontBold,
			TextColor = new Color(0.0781f, 0.0781f, 0.0781f)
		};
		base.AddGuiElement(guiLabel5);
		this.pvpDominationGuiForDelete.Add(guiLabel5);
		GuiLabel fontBold2 = new GuiLabel()
		{
			boundries = guiTexture5.boundries
		};
		GuiLabel x2 = fontBold2;
		x2.X = x2.X + 1f;
		GuiLabel y2 = fontBold2;
		y2.Y = y2.Y + 1f;
		fontBold2.text = StaticData.Translate("key_pvp_league_title_gold");
		fontBold2.FontSize = 14;
		fontBold2.Alignment = 4;
		fontBold2.Font = GuiLabel.FontBold;
		fontBold2.TextColor = GuiNewStyleBar.goldColor;
		base.AddGuiElement(fontBold2);
		this.pvpDominationGuiForDelete.Add(fontBold2);
		GuiLabel guiLabel6 = new GuiLabel()
		{
			boundries = guiTexture5.boundries,
			text = StaticData.Translate("key_pvp_league_title_gold"),
			FontSize = 14,
			Alignment = 4,
			Font = GuiLabel.FontBold,
			TextColor = new Color(0.3046f, 0.2265f, 0.0664f)
		};
		base.AddGuiElement(guiLabel6);
		this.pvpDominationGuiForDelete.Add(guiLabel6);
		GuiButtonFixed action = new GuiButtonFixed();
		action.SetTexture("PvPDominationGui", "btnViewWinners");
		action.X = 647f;
		action.Y = 257f;
		action.FontSize = 13;
		action.textColorNormal = GuiNewStyleBar.blackColorTransperant;
		action.Alignment = 4;
		action.eventHandlerParam.customData = action;
		action.Clicked = new Action<EventHandlerParam>(this, PVPWindow.OnViewWinnersClick);
		base.AddGuiElement(action);
		this.pvpDominationGuiForDelete.Add(action);
		if (!this.isWinnersListRequested)
		{
			action.Caption = StaticData.Translate("key_pvp_info_screen_view_winners");
			this.DrawLeagueRewards();
		}
		else
		{
			action.Caption = StaticData.Translate("key_pvp_score_board_lbl_rewards");
			if (prm is PvPLeagueRewardetPlayer[])
			{
				this.DrawLeagueWinners((PvPLeagueRewardetPlayer[])prm);
			}
		}
	}

	private void CreatePvPLeagueBtns(byte selectedOneIndex = 0)
	{
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("PvPDominationGui", "buttonInfo");
		guiButtonFixed.X = 185f;
		guiButtonFixed.Y = 25f;
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, PVPWindow.CreatePvPDominationInfo);
		base.AddGuiElement(guiButtonFixed);
		this.pvpDominationGuiForDelete.Add(guiButtonFixed);
		GuiButtonFixed action = new GuiButtonFixed();
		action.SetTexture("PvPDominationGui", "tab_bronze");
		action.X = 257f;
		action.Y = 78f;
		action.MarginTop = 1;
		action._marginLeft = 1;
		action.Caption = StaticData.Translate("key_pvp_league_title_bronze");
		action.textColorNormal = GuiNewStyleBar.bronzeColor;
		action.FontSize = 14;
		action.Alignment = 4;
		action.eventHandlerParam.customData = (PvPLeague)1;
		action.Clicked = new Action<EventHandlerParam>(this, PVPWindow.OnPvPLeagueClicked);
		base.AddGuiElement(action);
		this.pvpDominationGuiForDelete.Add(action);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = action.boundries,
			text = StaticData.Translate("key_pvp_league_title_bronze"),
			FontSize = 14,
			Alignment = 4,
			Font = GuiLabel.FontBold,
			TextColor = new Color(0.2617f, 0.1171f, 0.0078f)
		};
		base.AddGuiElement(guiLabel);
		this.pvpDominationGuiForDelete.Add(guiLabel);
		GuiButtonFixed guiButtonFixed1 = new GuiButtonFixed();
		guiButtonFixed1.SetTexture("PvPDominationGui", "tab_silver");
		guiButtonFixed1.X = 424f;
		guiButtonFixed1.Y = 78f;
		guiButtonFixed1.MarginTop = 1;
		guiButtonFixed1._marginLeft = 1;
		guiButtonFixed1.Caption = StaticData.Translate("key_pvp_league_title_silver");
		guiButtonFixed1.textColorNormal = GuiNewStyleBar.silverColor;
		guiButtonFixed1.FontSize = 14;
		guiButtonFixed1.Alignment = 4;
		guiButtonFixed1.eventHandlerParam.customData = (PvPLeague)2;
		guiButtonFixed1.Clicked = new Action<EventHandlerParam>(this, PVPWindow.OnPvPLeagueClicked);
		base.AddGuiElement(guiButtonFixed1);
		this.pvpDominationGuiForDelete.Add(guiButtonFixed1);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = guiButtonFixed1.boundries,
			text = StaticData.Translate("key_pvp_league_title_silver"),
			FontSize = 14,
			Alignment = 4,
			Font = GuiLabel.FontBold,
			TextColor = new Color(0.0781f, 0.0781f, 0.0781f)
		};
		base.AddGuiElement(guiLabel1);
		this.pvpDominationGuiForDelete.Add(guiLabel1);
		GuiButtonFixed action1 = new GuiButtonFixed();
		action1.SetTexture("PvPDominationGui", "tab_gold");
		action1.X = 591f;
		action1.Y = 78f;
		action1.MarginTop = 1;
		action1._marginLeft = 1;
		action1.Caption = StaticData.Translate("key_pvp_league_title_gold");
		action1.FontSize = 14;
		action1.textColorNormal = GuiNewStyleBar.goldColor;
		action1.Alignment = 4;
		action1.eventHandlerParam.customData = (PvPLeague)3;
		action1.Clicked = new Action<EventHandlerParam>(this, PVPWindow.OnPvPLeagueClicked);
		base.AddGuiElement(action1);
		this.pvpDominationGuiForDelete.Add(action1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = action1.boundries,
			text = StaticData.Translate("key_pvp_league_title_gold"),
			FontSize = 14,
			Alignment = 4,
			Font = GuiLabel.FontBold,
			TextColor = new Color(0.3046f, 0.2265f, 0.0664f)
		};
		base.AddGuiElement(guiLabel2);
		this.pvpDominationGuiForDelete.Add(guiLabel2);
		switch (selectedOneIndex)
		{
			case 1:
			{
				action.SetTextureNormal("PvPDominationGui", "tab_bronzeClk");
				action.SetTextureHover("PvPDominationGui", "tab_bronzeClk");
				break;
			}
			case 2:
			{
				guiButtonFixed1.SetTextureNormal("PvPDominationGui", "tab_silverClk");
				guiButtonFixed1.SetTextureHover("PvPDominationGui", "tab_silverClk");
				break;
			}
			case 3:
			{
				action1.SetTextureNormal("PvPDominationGui", "tab_goldClk");
				action1.SetTextureHover("PvPDominationGui", "tab_goldClk");
				break;
			}
		}
		if (NetworkScript.player.vessel.inPvPRank)
		{
			GuiTexture guiTexture = new GuiTexture();
			guiTexture.SetTexture("PvPDominationGui", "icon_player");
			guiTexture.Y = 92f;
			base.AddGuiElement(guiTexture);
			this.pvpDominationGuiForDelete.Add(guiTexture);
			switch (NetworkScript.player.vessel.playerLeague)
			{
				case 1:
				{
					guiTexture.X = 285f;
					break;
				}
				case 2:
				{
					guiTexture.X = 452f;
					break;
				}
				case 3:
				{
					guiTexture.X = 619f;
					break;
				}
			}
		}
	}

	private void CreatePvPLeagueHeader()
	{
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(212f, 125f, 60f, 30f),
			text = StaticData.Translate("key_pvp_header_lbl_rank"),
			Alignment = 3,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor
		};
		base.AddGuiElement(guiLabel);
		this.pvpDominationGuiForDelete.Add(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(282f, 125f, 220f, 30f),
			text = StaticData.Translate("key_pvp_header_lbl_player"),
			Alignment = 3,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor
		};
		base.AddGuiElement(guiLabel1);
		this.pvpDominationGuiForDelete.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(512f, 125f, 70f, 30f),
			text = StaticData.Translate("key_pvp_header_lbl_guild"),
			Alignment = 3,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor
		};
		base.AddGuiElement(guiLabel2);
		this.pvpDominationGuiForDelete.Add(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(592f, 125f, 100f, 30f),
			text = StaticData.Translate("key_pvp_header_lbl_balance"),
			Alignment = 3,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor
		};
		base.AddGuiElement(guiLabel3);
		this.pvpDominationGuiForDelete.Add(guiLabel3);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			boundries = new Rect(702f, 125f, 90f, 30f),
			text = StaticData.Translate("key_pvp_header_lbl_honor_change"),
			Alignment = 3,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor
		};
		base.AddGuiElement(guiLabel4);
		this.pvpDominationGuiForDelete.Add(guiLabel4);
	}

	private void CreatePvPWindowInDominationGame()
	{
		base.SetBackgroundTexture("PvPDominationGui", "pvpScoreFrame");
		base.PutToCenter();
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(289f, 46f, 440f, 38f),
			FontSize = 28,
			Font = GuiLabel.FontBold,
			Alignment = 4
		};
		if (NetworkScript.player.pvpGame.state == 1)
		{
			guiLabel.text = StaticData.Translate("key_pvp_window_title_match_in_progress");
			guiLabel.TextColor = GuiNewStyleBar.aquamarineColor;
		}
		else if (NetworkScript.player.pvpGame.winTeam == 0)
		{
			guiLabel.text = StaticData.Translate("key_main_screen_pvp_draw");
			guiLabel.TextColor = Color.get_white();
		}
		else if (NetworkScript.player.vessel.teamNumber != NetworkScript.player.pvpGame.winTeam)
		{
			guiLabel.text = StaticData.Translate("key_pvp_window_title_defeat");
			guiLabel.TextColor = GuiNewStyleBar.redColor;
		}
		else
		{
			guiLabel.text = StaticData.Translate("key_pvp_window_title_victory");
			guiLabel.TextColor = GuiNewStyleBar.greenColor;
		}
		base.AddGuiElement(guiLabel);
		this.pvpStatsForDelete.Add(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(235f, 130f, 135f, 30f),
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor,
			FontSize = 12,
			text = StaticData.Translate("key_pvp_header_lbl_player"),
			Alignment = 3
		};
		base.AddGuiElement(guiLabel1);
		this.pvpStatsForDelete.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(guiLabel1.X + guiLabel1.boundries.get_width() + 5f, 130f, 70f, 30f),
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor,
			FontSize = 12,
			text = StaticData.Translate("key_pvp_header_lbl_guild"),
			Alignment = 3
		};
		base.AddGuiElement(guiLabel2);
		this.pvpStatsForDelete.Add(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(guiLabel2.X + guiLabel2.boundries.get_width() + 5f, 130f, 70f, 30f),
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor,
			FontSize = 12,
			text = StaticData.Translate("key_pvp_header_lbl_kills"),
			Alignment = 3
		};
		base.AddGuiElement(guiLabel3);
		this.pvpStatsForDelete.Add(guiLabel3);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			boundries = new Rect(guiLabel3.X + guiLabel3.boundries.get_width() + 5f, 130f, 70f, 30f),
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor,
			FontSize = 12,
			text = StaticData.Translate("key_pvp_header_lbl_honor"),
			Alignment = 3
		};
		base.AddGuiElement(guiLabel4);
		this.pvpStatsForDelete.Add(guiLabel4);
		GuiLabel guiLabel5 = new GuiLabel()
		{
			boundries = new Rect(guiLabel4.X + guiLabel4.boundries.get_width() + 5f, 130f, 70f, 30f),
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor,
			FontSize = 12,
			text = StaticData.Translate("key_pvp_header_lbl_aliens_killed"),
			Alignment = 3
		};
		base.AddGuiElement(guiLabel5);
		this.pvpStatsForDelete.Add(guiLabel5);
		GuiLabel guiLabel6 = new GuiLabel()
		{
			boundries = new Rect(guiLabel5.X + guiLabel5.boundries.get_width() + 5f, 130f, 70f, 30f),
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor,
			FontSize = 12,
			text = StaticData.Translate("key_pvp_header_lbl_contribution"),
			Alignment = 3
		};
		base.AddGuiElement(guiLabel6);
		this.pvpStatsForDelete.Add(guiLabel6);
		GuiLabel guiLabel7 = new GuiLabel()
		{
			boundries = new Rect(guiLabel6.X + guiLabel6.boundries.get_width() + 5f, 130f, 70f, 30f),
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor,
			FontSize = 12,
			text = StaticData.Translate("key_pvp_header_lbl_honor_change"),
			Alignment = 3
		};
		base.AddGuiElement(guiLabel7);
		this.pvpStatsForDelete.Add(guiLabel7);
		List<PvPStatRow> list = NetworkScript.player.pvpGame.stats;
		if (PVPWindow.<>f__am$cache7 == null)
		{
			PVPWindow.<>f__am$cache7 = new Func<PvPStatRow, bool>(null, (PvPStatRow t) => t.teamNumber == NetworkScript.player.vessel.teamNumber);
		}
		IEnumerable<PvPStatRow> enumerable = Enumerable.Where<PvPStatRow>(list, PVPWindow.<>f__am$cache7);
		if (PVPWindow.<>f__am$cache8 == null)
		{
			PVPWindow.<>f__am$cache8 = new Func<PvPStatRow, short>(null, (PvPStatRow s) => s.kills);
		}
		IOrderedEnumerable<PvPStatRow> orderedEnumerable = Enumerable.OrderByDescending<PvPStatRow, short>(enumerable, PVPWindow.<>f__am$cache8);
		if (PVPWindow.<>f__am$cache9 == null)
		{
			PVPWindow.<>f__am$cache9 = new Func<PvPStatRow, short>(null, (PvPStatRow t) => t.honor);
		}
		PvPStatRow[] array = Enumerable.ToArray<PvPStatRow>(Enumerable.ThenBy<PvPStatRow, short>(orderedEnumerable, PVPWindow.<>f__am$cache9));
		int num = 0;
		PvPStatRow[] pvPStatRowArray = array;
		for (int i = 0; i < (int)pvPStatRowArray.Length; i++)
		{
			PvPStatRow pvPStatRow = pvPStatRowArray[i];
			Color _white = Color.get_white();
			if (pvPStatRow.playerId == NetworkScript.player.playId)
			{
				_white = GuiNewStyleBar.blueColor;
				GuiTexture guiTexture = new GuiTexture();
				guiTexture.SetTexture("PvPDominationGui", "activeRow");
				guiTexture.X = 203f;
				guiTexture.Y = (float)(170 + num * 31);
				base.AddGuiElement(guiTexture);
				this.pvpStatsForDelete.Add(guiTexture);
			}
			if (pvPStatRow.state == 2)
			{
				_white = Color.get_gray();
			}
			else if (pvPStatRow.state == 5)
			{
				_white = GuiNewStyleBar.redColor;
			}
			string str = string.Format("fraction{0}Icon", pvPStatRow.fractionId);
			GuiTexture guiTexture1 = new GuiTexture();
			guiTexture1.SetTexture("FrameworkGUI", str);
			guiTexture1.X = 205f;
			guiTexture1.Y = (float)(175 + num * 31);
			base.AddGuiElement(guiTexture1);
			this.pvpStatsForDelete.Add(guiTexture1);
			GuiLabel guiLabel8 = new GuiLabel()
			{
				boundries = new Rect(235f, (float)(175 + num * 31), 135f, 18f),
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = pvPStatRow.playerName,
				Alignment = 3,
				TextColor = _white
			};
			base.AddGuiElement(guiLabel8);
			this.pvpStatsForDelete.Add(guiLabel8);
			GuiLabel guiLabel9 = new GuiLabel()
			{
				boundries = new Rect(guiLabel8.X + guiLabel8.boundries.get_width() + 5f, (float)(175 + num * 31), 70f, 18f),
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = pvPStatRow.guildName,
				Alignment = 3,
				TextColor = _white
			};
			base.AddGuiElement(guiLabel9);
			this.pvpStatsForDelete.Add(guiLabel9);
			GuiLabel guiLabel10 = new GuiLabel()
			{
				boundries = new Rect(guiLabel9.X + guiLabel9.boundries.get_width() + 5f, (float)(175 + num * 31), 70f, 18f),
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = pvPStatRow.kills.ToString(),
				Alignment = 3,
				TextColor = _white
			};
			base.AddGuiElement(guiLabel10);
			this.pvpStatsForDelete.Add(guiLabel10);
			GuiLabel guiLabel11 = new GuiLabel()
			{
				boundries = new Rect(guiLabel10.X + guiLabel10.boundries.get_width() + 5f, (float)(175 + num * 31), 70f, 18f),
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = pvPStatRow.honor.ToString(),
				Alignment = 3,
				TextColor = _white
			};
			base.AddGuiElement(guiLabel11);
			this.pvpStatsForDelete.Add(guiLabel11);
			GuiLabel guiLabel12 = new GuiLabel()
			{
				boundries = new Rect(guiLabel11.X + guiLabel11.boundries.get_width() + 5f, (float)(175 + num * 31), 70f, 18f),
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = pvPStatRow.aliensKilded.ToString(),
				Alignment = 3,
				TextColor = _white
			};
			base.AddGuiElement(guiLabel12);
			this.pvpStatsForDelete.Add(guiLabel12);
			GuiLabel guiLabel13 = new GuiLabel()
			{
				boundries = new Rect(guiLabel12.X + guiLabel12.boundries.get_width() + 5f, (float)(175 + num * 31), 70f, 18f),
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = pvPStatRow.progressPointGaine.ToString(),
				Alignment = 3,
				TextColor = _white
			};
			base.AddGuiElement(guiLabel13);
			this.pvpStatsForDelete.Add(guiLabel13);
			GuiLabel guiLabel14 = new GuiLabel()
			{
				boundries = new Rect(guiLabel13.X + guiLabel13.boundries.get_width() + 5f, (float)(175 + num * 31), 70f, 18f),
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = (pvPStatRow.honorChange != 0 ? pvPStatRow.honorChange.ToString() : "-"),
				Alignment = 3
			};
			if (pvPStatRow.honorChange > 0)
			{
				guiLabel14.text = string.Format("+{0}", pvPStatRow.honorChange);
				guiLabel14.TextColor = GuiNewStyleBar.greenColor;
			}
			else if (pvPStatRow.honorChange >= 0)
			{
				guiLabel14.TextColor = _white;
			}
			else
			{
				guiLabel14.TextColor = GuiNewStyleBar.redColor;
			}
			base.AddGuiElement(guiLabel14);
			this.pvpStatsForDelete.Add(guiLabel14);
			num++;
		}
		List<PvPStatRow> list1 = NetworkScript.player.pvpGame.stats;
		if (PVPWindow.<>f__am$cacheA == null)
		{
			PVPWindow.<>f__am$cacheA = new Func<PvPStatRow, bool>(null, (PvPStatRow t) => t.teamNumber != NetworkScript.player.vessel.teamNumber);
		}
		IEnumerable<PvPStatRow> enumerable1 = Enumerable.Where<PvPStatRow>(list1, PVPWindow.<>f__am$cacheA);
		if (PVPWindow.<>f__am$cacheB == null)
		{
			PVPWindow.<>f__am$cacheB = new Func<PvPStatRow, short>(null, (PvPStatRow s) => s.kills);
		}
		IOrderedEnumerable<PvPStatRow> orderedEnumerable1 = Enumerable.OrderByDescending<PvPStatRow, short>(enumerable1, PVPWindow.<>f__am$cacheB);
		if (PVPWindow.<>f__am$cacheC == null)
		{
			PVPWindow.<>f__am$cacheC = new Func<PvPStatRow, short>(null, (PvPStatRow t) => t.honor);
		}
		PvPStatRow[] array1 = Enumerable.ToArray<PvPStatRow>(Enumerable.ThenBy<PvPStatRow, short>(orderedEnumerable1, PVPWindow.<>f__am$cacheC));
		num = 0;
		float single = 130f;
		PvPStatRow[] pvPStatRowArray1 = array1;
		for (int j = 0; j < (int)pvPStatRowArray1.Length; j++)
		{
			PvPStatRow pvPStatRow1 = pvPStatRowArray1[j];
			Color _gray = Color.get_white();
			if (pvPStatRow1.state == 2)
			{
				_gray = Color.get_gray();
			}
			else if (pvPStatRow1.state == 5)
			{
				_gray = GuiNewStyleBar.redColor;
			}
			string str1 = string.Format("fraction{0}Icon", pvPStatRow1.fractionId);
			GuiTexture guiTexture2 = new GuiTexture();
			guiTexture2.SetTexture("FrameworkGUI", str1);
			guiTexture2.X = 205f;
			guiTexture2.Y = (float)(175 + num * 31) + single;
			base.AddGuiElement(guiTexture2);
			this.pvpStatsForDelete.Add(guiTexture2);
			GuiLabel guiLabel15 = new GuiLabel()
			{
				boundries = new Rect(235f, (float)(175 + num * 31) + single, 135f, 18f),
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = pvPStatRow1.playerName,
				Alignment = 3,
				TextColor = _gray
			};
			base.AddGuiElement(guiLabel15);
			this.pvpStatsForDelete.Add(guiLabel15);
			GuiLabel guiLabel16 = new GuiLabel()
			{
				boundries = new Rect(guiLabel15.X + guiLabel15.boundries.get_width() + 5f, (float)(175 + num * 31) + single, 70f, 18f),
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = pvPStatRow1.guildName,
				Alignment = 3,
				TextColor = _gray
			};
			base.AddGuiElement(guiLabel16);
			this.pvpStatsForDelete.Add(guiLabel16);
			GuiLabel guiLabel17 = new GuiLabel()
			{
				boundries = new Rect(guiLabel16.X + guiLabel16.boundries.get_width() + 5f, (float)(175 + num * 31) + single, 70f, 18f),
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = pvPStatRow1.kills.ToString(),
				Alignment = 3,
				TextColor = _gray
			};
			base.AddGuiElement(guiLabel17);
			this.pvpStatsForDelete.Add(guiLabel17);
			GuiLabel guiLabel18 = new GuiLabel()
			{
				boundries = new Rect(guiLabel17.X + guiLabel17.boundries.get_width() + 5f, (float)(175 + num * 31) + single, 70f, 18f),
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = pvPStatRow1.honor.ToString(),
				Alignment = 3,
				TextColor = _gray
			};
			base.AddGuiElement(guiLabel18);
			this.pvpStatsForDelete.Add(guiLabel18);
			GuiLabel guiLabel19 = new GuiLabel()
			{
				boundries = new Rect(guiLabel18.X + guiLabel18.boundries.get_width() + 5f, (float)(175 + num * 31) + single, 70f, 18f),
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = pvPStatRow1.aliensKilded.ToString(),
				Alignment = 3,
				TextColor = _gray
			};
			base.AddGuiElement(guiLabel19);
			this.pvpStatsForDelete.Add(guiLabel19);
			GuiLabel guiLabel20 = new GuiLabel()
			{
				boundries = new Rect(guiLabel19.X + guiLabel19.boundries.get_width() + 5f, (float)(175 + num * 31) + single, 70f, 18f),
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = pvPStatRow1.progressPointGaine.ToString(),
				Alignment = 3,
				TextColor = _gray
			};
			base.AddGuiElement(guiLabel20);
			this.pvpStatsForDelete.Add(guiLabel20);
			GuiLabel guiLabel21 = new GuiLabel()
			{
				boundries = new Rect(guiLabel20.X + guiLabel20.boundries.get_width() + 5f, (float)(175 + num * 31) + single, 70f, 18f),
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = (pvPStatRow1.honorChange != 0 ? pvPStatRow1.honorChange.ToString() : "-"),
				Alignment = 3
			};
			if (pvPStatRow1.honorChange > 0)
			{
				guiLabel21.text = string.Format("+{0}", pvPStatRow1.honorChange);
				guiLabel21.TextColor = GuiNewStyleBar.greenColor;
			}
			else if (pvPStatRow1.honorChange >= 0)
			{
				guiLabel21.TextColor = _gray;
			}
			else
			{
				guiLabel21.TextColor = GuiNewStyleBar.redColor;
			}
			base.AddGuiElement(guiLabel21);
			this.pvpStatsForDelete.Add(guiLabel21);
			num++;
		}
		List<PvPStatRow> list2 = NetworkScript.player.pvpGame.stats;
		if (PVPWindow.<>f__am$cacheD == null)
		{
			PVPWindow.<>f__am$cacheD = new Func<PvPStatRow, bool>(null, (PvPStatRow r) => r.playerId == NetworkScript.player.playId);
		}
		PvPStatRow pvPStatRow2 = Enumerable.First<PvPStatRow>(Enumerable.Where<PvPStatRow>(list2, PVPWindow.<>f__am$cacheD));
		GuiLabel guiLabel22 = new GuiLabel()
		{
			boundries = new Rect(258f, 517f, 160f, 20f),
			Font = GuiLabel.FontBold,
			FontSize = 16,
			text = StaticData.Translate("key_pvp_score_board_lbl_rewards"),
			Alignment = 3,
			TextColor = GuiNewStyleBar.aquamarineColor
		};
		base.AddGuiElement(guiLabel22);
		this.pvpStatsForDelete.Add(guiLabel22);
		guiLabel22.boundries.set_width(guiLabel22.TextWidth);
		GuiTexture x = new GuiTexture();
		x.SetTexture("PvPDominationGui", "cash");
		x.X = 1f;
		x.Y = 519f;
		base.AddGuiElement(x);
		this.pvpStatsForDelete.Add(x);
		GuiLabel x1 = new GuiLabel()
		{
			boundries = new Rect(258f, 517f, 160f, 20f),
			Font = GuiLabel.FontBold,
			FontSize = 14,
			text = (pvPStatRow2.rewardCash == 0 ? "-" : pvPStatRow2.rewardCash.ToString("##,##0")),
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(x1);
		this.pvpStatsForDelete.Add(x1);
		x1.boundries.set_width(x1.TextWidth);
		GuiTexture x2 = new GuiTexture();
		x2.SetTexture("PvPDominationGui", "ult");
		x2.X = 1f;
		x2.Y = 519f;
		base.AddGuiElement(x2);
		this.pvpStatsForDelete.Add(x2);
		GuiLabel x3 = new GuiLabel()
		{
			boundries = new Rect(258f, 517f, 160f, 20f),
			Font = GuiLabel.FontBold,
			FontSize = 14,
			text = (pvPStatRow2.rewardUltralibrium == 0 ? "-" : pvPStatRow2.rewardUltralibrium.ToString("##,##0")),
			Alignment = 3,
			TextColor = GuiNewStyleBar.greenColor
		};
		base.AddGuiElement(x3);
		this.pvpStatsForDelete.Add(x3);
		x3.boundries.set_width(x3.TextWidth);
		GuiTexture guiTexture3 = new GuiTexture();
		guiTexture3.SetTexture("PvPDominationGui", "xp");
		guiTexture3.X = 1f;
		guiTexture3.Y = 519f;
		base.AddGuiElement(guiTexture3);
		this.pvpStatsForDelete.Add(guiTexture3);
		GuiLabel x4 = new GuiLabel()
		{
			boundries = new Rect(258f, 517f, 160f, 20f),
			Font = GuiLabel.FontBold,
			FontSize = 14,
			text = (pvPStatRow2.rewardXp == 0 ? "-" : pvPStatRow2.rewardXp.ToString("##,##0")),
			Alignment = 3,
			TextColor = GuiNewStyleBar.orangeColor
		};
		base.AddGuiElement(x4);
		this.pvpStatsForDelete.Add(x4);
		x4.boundries.set_width(x4.TextWidth);
		float _width = 490f - guiLabel22.boundries.get_width() - x1.boundries.get_width() - x3.boundries.get_width() - x1.boundries.get_width() - x.boundries.get_width() - x2.boundries.get_width() - guiTexture3.boundries.get_width();
		float single1 = _width / 5f;
		guiLabel22.X = 258f + single1;
		x.X = guiLabel22.X + guiLabel22.boundries.get_width() + single1;
		x1.X = x.X + x.boundries.get_width() + 5f;
		x2.X = x1.X + x1.boundries.get_width() + single1;
		x3.X = x2.X + x2.boundries.get_width() + 5f;
		guiTexture3.X = x3.X + x3.boundries.get_width() + single1;
		x4.X = guiTexture3.X + guiTexture3.boundries.get_width() + 5f;
		if (NetworkScript.player.pvpGame.gameType.selectedMap != null && NetworkScript.player.pvpGame.gameType.selectedMap.galaxyId != NetworkScript.player.vessel.galaxy.__galaxyId)
		{
			return;
		}
		PvPPlayerState pvPPlayerState = NetworkScript.player.vessel.pvpState;
		if (pvPPlayerState == 3)
		{
			GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
			guiButtonFixed.SetTexture("PvPDominationGui", "buttonRetreat");
			guiButtonFixed.Alignment = 4;
			guiButtonFixed.Caption = StaticData.Translate("key_pvp_retreat");
			guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, PVPWindow.OnRetreatClicked);
			guiButtonFixed.X = 342f;
			guiButtonFixed.Y = 427f;
			base.AddGuiElement(guiButtonFixed);
			this.pvpStatsForDelete.Add(guiButtonFixed);
		}
		else if (pvPPlayerState == 4)
		{
			GuiButtonFixed empty = new GuiButtonFixed();
			empty.SetTexture("PvPDominationGui", "buttonOk");
			empty.Alignment = 4;
			empty.Caption = string.Empty;
			empty.Clicked = new Action<EventHandlerParam>(this, PVPWindow.OnBackToSpaceClicked);
			empty.X = 342f;
			empty.Y = 427f;
			base.AddGuiElement(empty);
			this.pvpStatsForDelete.Add(empty);
			TimeSpan timeSpan = NetworkScript.player.pvpGame.destroyTime - StaticData.now;
			int totalSeconds = (int)timeSpan.get_TotalSeconds() + 1;
			GuiSecondsTracker guiSecondsTracker = new GuiSecondsTracker(StaticData.Translate("key_pvp_btn_caption_back_to_space"), totalSeconds * 1000, this)
			{
				boundries = new Rect(342f, 427f, 333f, 65f),
				FontSize = 14,
				Alignment = 4,
				Font = GuiLabel.FontBold
			};
			this.pvpStatsForDelete.Add(guiSecondsTracker);
		}
	}

	private void DrawLeagueMembers()
	{
		// 
		// Current member / type: System.Void PVPWindow::DrawLeagueMembers()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void DrawLeagueMembers()
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..(TypeReference ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\IntegerTypesHierarchyBuilder.cs:строка 35
		//    в ..(BinaryExpression , VariableReference ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\ClassHierarchyBuilder.cs:строка 497
		//    в ..(Expression , VariableReference ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\ClassHierarchyBuilder.cs:строка 370
		//    в ..(Instruction , VariableReference ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\ClassHierarchyBuilder.cs:строка 340
		//    в ..(Int32 , ClassHierarchyNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\IntegerTypesHierarchyBuilder.cs:строка 89
		//    в ..(HashSet`1 ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\ClassHierarchyBuilder.cs:строка 73
		//    в ..(HashSet`1 ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\IntegerTypeInferer.cs:строка 23
		//    в Telerik.JustDecompiler.Decompiler.TypeInference.TypeInferer.(HashSet`1 ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\TypeInferer.cs:строка 342
		//    в Telerik.JustDecompiler.Decompiler.TypeInference.TypeInferer.() в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\TypeInferer.cs:строка 329
		//    в Telerik.JustDecompiler.Decompiler.ExpressionDecompilerStep.(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\ExpressionDecompilerStep.cs:строка 88
		//    в ..(MethodBody ,  , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 88
		//    в ..(MethodBody , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 70
		//    в Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 95
		//    в Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 58
		//    в ..(ILanguage , MethodDefinition ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:строка 117
		// 
		// mailto: JustDecompilePublicFeedback@telerik.com

	}

	private void DrawLeagueRewards()
	{
		for (int i = 0; i < 10; i++)
		{
			PvPLeageReward item = StaticData.pvpRewards.get_Item(1)[i];
			GuiLabel guiLabel = new GuiLabel()
			{
				boundries = new Rect(227f, (float)(300 + i * 20), 170f, 16f)
			};
			int num = i + 1;
			guiLabel.text = string.Concat(num.ToString(), ".");
			guiLabel.FontSize = 12;
			guiLabel.Alignment = 3;
			guiLabel.Font = GuiLabel.FontBold;
			base.AddGuiElement(guiLabel);
			this.pvpDominationGuiForDelete.Add(guiLabel);
			GuiTexture guiTexture = new GuiTexture();
			guiTexture.SetTexture("Shop", StaticData.allTypes.get_Item(item.rewardType).assetName);
			guiTexture.X = 245f;
			guiTexture.Y = (float)(299 + i * 20);
			guiTexture.SetSize(26f, 18f);
			base.AddGuiElement(guiTexture);
			this.pvpDominationGuiForDelete.Add(guiTexture);
			GuiLabel guiLabel1 = new GuiLabel()
			{
				boundries = new Rect(275f, (float)(300 + i * 20), 115f, 16f),
				text = item.rewardAmount.ToString("##,##0"),
				FontSize = 12,
				Alignment = 3,
				Font = GuiLabel.FontBold,
				TextColor = GuiNewStyleBar.purpleColor
			};
			base.AddGuiElement(guiLabel1);
			this.pvpDominationGuiForDelete.Add(guiLabel1);
			PvPLeageReward pvPLeageReward = StaticData.pvpRewards.get_Item(2)[i];
			GuiLabel fontBold = new GuiLabel()
			{
				boundries = new Rect(427f, (float)(300 + i * 20), 170f, 16f)
			};
			int num1 = i + 1;
			fontBold.text = string.Concat(num1.ToString(), ".");
			fontBold.FontSize = 12;
			fontBold.Alignment = 3;
			fontBold.Font = GuiLabel.FontBold;
			base.AddGuiElement(fontBold);
			this.pvpDominationGuiForDelete.Add(fontBold);
			GuiTexture guiTexture1 = new GuiTexture();
			guiTexture1.SetTexture("Shop", StaticData.allTypes.get_Item(pvPLeageReward.rewardType).assetName);
			guiTexture1.X = 445f;
			guiTexture1.Y = (float)(299 + i * 20);
			guiTexture1.SetSize(26f, 18f);
			base.AddGuiElement(guiTexture1);
			this.pvpDominationGuiForDelete.Add(guiTexture1);
			GuiLabel guiLabel2 = new GuiLabel()
			{
				boundries = new Rect(475f, (float)(300 + i * 20), 115f, 16f),
				text = pvPLeageReward.rewardAmount.ToString("##,##0"),
				FontSize = 12,
				Alignment = 3,
				Font = GuiLabel.FontBold,
				TextColor = GuiNewStyleBar.purpleColor
			};
			base.AddGuiElement(guiLabel2);
			this.pvpDominationGuiForDelete.Add(guiLabel2);
			PvPLeageReward item1 = StaticData.pvpRewards.get_Item(3)[i];
			GuiLabel fontBold1 = new GuiLabel()
			{
				boundries = new Rect(627f, (float)(300 + i * 20), 170f, 16f)
			};
			int num2 = i + 1;
			fontBold1.text = string.Concat(num2.ToString(), ".");
			fontBold1.FontSize = 12;
			fontBold1.Alignment = 3;
			fontBold1.Font = GuiLabel.FontBold;
			base.AddGuiElement(fontBold1);
			this.pvpDominationGuiForDelete.Add(fontBold1);
			GuiTexture guiTexture2 = new GuiTexture();
			guiTexture2.SetTexture("Shop", StaticData.allTypes.get_Item(item1.rewardType).assetName);
			guiTexture2.X = 645f;
			guiTexture2.Y = (float)(299 + i * 20);
			guiTexture2.SetSize(26f, 18f);
			base.AddGuiElement(guiTexture2);
			this.pvpDominationGuiForDelete.Add(guiTexture2);
			GuiLabel guiLabel3 = new GuiLabel()
			{
				boundries = new Rect(675f, (float)(300 + i * 20), 115f, 16f),
				text = item1.rewardAmount.ToString("##,##0"),
				FontSize = 12,
				Alignment = 3,
				Font = GuiLabel.FontBold,
				TextColor = GuiNewStyleBar.orangeColor
			};
			base.AddGuiElement(guiLabel3);
			this.pvpDominationGuiForDelete.Add(guiLabel3);
		}
	}

	private void DrawLeagueWinners(PvPLeagueRewardetPlayer[] winners)
	{
		PVPWindow.<DrawLeagueWinners>c__AnonStorey56 variable = null;
		for (int i = 0; i < 10; i++)
		{
			PvPLeagueRewardetPlayer pvPLeagueRewardetPlayer = Enumerable.FirstOrDefault<PvPLeagueRewardetPlayer>(Enumerable.Where<PvPLeagueRewardetPlayer>(winners, new Func<PvPLeagueRewardetPlayer, bool>(variable, (PvPLeagueRewardetPlayer t) => (t.league != 1 ? false : t.rankPossition == this.i + 1))));
			if (pvPLeagueRewardetPlayer != null)
			{
				GuiLabel guiLabel = new GuiLabel()
				{
					boundries = new Rect(227f, (float)(300 + i * 20), 170f, 16f),
					text = string.Concat(pvPLeagueRewardetPlayer.rankPossition.ToString(), "."),
					FontSize = 12,
					Alignment = 3,
					Font = GuiLabel.FontBold
				};
				base.AddGuiElement(guiLabel);
				this.pvpDominationGuiForDelete.Add(guiLabel);
				string str = string.Format("fraction{0}Icon", pvPLeagueRewardetPlayer.fractionId);
				GuiTexture guiTexture = new GuiTexture();
				guiTexture.SetTexture("FrameworkGUI", str);
				guiTexture.X = 245f;
				guiTexture.Y = (float)(299 + i * 20);
				guiTexture.SetSize(26f, 18f);
				base.AddGuiElement(guiTexture);
				this.pvpDominationGuiForDelete.Add(guiTexture);
				GuiLabel guiLabel1 = new GuiLabel()
				{
					boundries = new Rect(275f, (float)(300 + i * 20), 115f, 16f),
					text = pvPLeagueRewardetPlayer.nickName,
					FontSize = 12,
					Alignment = 3,
					Font = GuiLabel.FontBold,
					TextColor = GuiNewStyleBar.aquamarineColor
				};
				base.AddGuiElement(guiLabel1);
				this.pvpDominationGuiForDelete.Add(guiLabel1);
			}
			PvPLeagueRewardetPlayer pvPLeagueRewardetPlayer1 = Enumerable.FirstOrDefault<PvPLeagueRewardetPlayer>(Enumerable.Where<PvPLeagueRewardetPlayer>(winners, new Func<PvPLeagueRewardetPlayer, bool>(variable, (PvPLeagueRewardetPlayer t) => (t.league != 2 ? false : t.rankPossition == this.i + 1))));
			if (pvPLeagueRewardetPlayer1 != null)
			{
				GuiLabel guiLabel2 = new GuiLabel()
				{
					boundries = new Rect(427f, (float)(300 + i * 20), 170f, 16f),
					text = string.Concat(pvPLeagueRewardetPlayer1.rankPossition.ToString(), "."),
					FontSize = 12,
					Alignment = 3,
					Font = GuiLabel.FontBold
				};
				base.AddGuiElement(guiLabel2);
				this.pvpDominationGuiForDelete.Add(guiLabel2);
				string str1 = string.Format("fraction{0}Icon", pvPLeagueRewardetPlayer1.fractionId);
				GuiTexture guiTexture1 = new GuiTexture();
				guiTexture1.SetTexture("FrameworkGUI", str1);
				guiTexture1.X = 445f;
				guiTexture1.Y = (float)(299 + i * 20);
				guiTexture1.SetSize(26f, 18f);
				base.AddGuiElement(guiTexture1);
				this.pvpDominationGuiForDelete.Add(guiTexture1);
				GuiLabel guiLabel3 = new GuiLabel()
				{
					boundries = new Rect(475f, (float)(300 + i * 20), 115f, 16f),
					text = pvPLeagueRewardetPlayer1.nickName,
					FontSize = 12,
					Alignment = 3,
					Font = GuiLabel.FontBold,
					TextColor = GuiNewStyleBar.aquamarineColor
				};
				base.AddGuiElement(guiLabel3);
				this.pvpDominationGuiForDelete.Add(guiLabel3);
			}
			PvPLeagueRewardetPlayer pvPLeagueRewardetPlayer2 = Enumerable.FirstOrDefault<PvPLeagueRewardetPlayer>(Enumerable.Where<PvPLeagueRewardetPlayer>(winners, new Func<PvPLeagueRewardetPlayer, bool>(variable, (PvPLeagueRewardetPlayer t) => (t.league != 3 ? false : t.rankPossition == this.i + 1))));
			if (pvPLeagueRewardetPlayer2 != null)
			{
				GuiLabel guiLabel4 = new GuiLabel()
				{
					boundries = new Rect(627f, (float)(300 + i * 20), 170f, 16f),
					text = string.Concat(pvPLeagueRewardetPlayer2.rankPossition.ToString(), "."),
					FontSize = 12,
					Alignment = 3,
					Font = GuiLabel.FontBold
				};
				base.AddGuiElement(guiLabel4);
				this.pvpDominationGuiForDelete.Add(guiLabel4);
				string str2 = string.Format("fraction{0}Icon", pvPLeagueRewardetPlayer2.fractionId);
				GuiTexture guiTexture2 = new GuiTexture();
				guiTexture2.SetTexture("FrameworkGUI", str2);
				guiTexture2.X = 645f;
				guiTexture2.Y = (float)(299 + i * 20);
				guiTexture2.SetSize(26f, 18f);
				base.AddGuiElement(guiTexture2);
				this.pvpDominationGuiForDelete.Add(guiTexture2);
				GuiLabel guiLabel5 = new GuiLabel()
				{
					boundries = new Rect(675f, (float)(300 + i * 20), 115f, 16f),
					text = pvPLeagueRewardetPlayer2.nickName,
					FontSize = 12,
					Alignment = 3,
					Font = GuiLabel.FontBold,
					TextColor = GuiNewStyleBar.aquamarineColor
				};
				base.AddGuiElement(guiLabel5);
				this.pvpDominationGuiForDelete.Add(guiLabel5);
			}
		}
	}

	private void DrawPvPGameRewards(bool firstWinBonus = false)
	{
		float single = 60f;
		float single1 = 41f;
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(205f, 295f, 370f, 28f),
			text = StaticData.Translate("key_pvp_score_board_lbl_rewards"),
			FontSize = 14,
			Alignment = 4,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor
		};
		base.AddGuiElement(guiLabel);
		this.pvpDominationGuiForDelete.Add(guiLabel);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("Shop", "Honor");
		guiTexture.boundries = new Rect(200f, 335f, single, single1);
		guiTexture.isHoverAware = true;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_pvp_reward_tooltip_honor"),
			customData2 = guiTexture
		};
		guiTexture.tooltipWindowParam = eventHandlerParam;
		guiTexture.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(guiTexture);
		this.pvpDominationGuiForDelete.Add(guiTexture);
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("Shop", "Xp");
		rect.boundries = new Rect(295f, 335f, single, single1);
		rect.isHoverAware = true;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_pvp_reward_tooltip_xp"),
			customData2 = rect
		};
		rect.tooltipWindowParam = eventHandlerParam;
		rect.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(rect);
		this.pvpDominationGuiForDelete.Add(rect);
		GuiTexture drawTooltipWindow = new GuiTexture();
		drawTooltipWindow.SetTexture("Shop", "Ultralibrium");
		drawTooltipWindow.boundries = new Rect(390f, 335f, single, single1);
		drawTooltipWindow.isHoverAware = true;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_pvp_reward_tooltip_ultralibrium"),
			customData2 = drawTooltipWindow
		};
		drawTooltipWindow.tooltipWindowParam = eventHandlerParam;
		drawTooltipWindow.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(drawTooltipWindow);
		this.pvpDominationGuiForDelete.Add(drawTooltipWindow);
		GuiTexture x = new GuiTexture();
		x.SetTexture("Shop", "Cash");
		x.boundries = new Rect(485f, 335f, single, single1);
		x.isHoverAware = true;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_pvp_reward_tooltip_cash"),
			customData2 = x
		};
		x.tooltipWindowParam = eventHandlerParam;
		x.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(x);
		this.pvpDominationGuiForDelete.Add(x);
		float single2 = (370f - 4f * single - 15f) / 2f;
		guiTexture.X = guiLabel.X + single2;
		rect.X = guiTexture.X + single + 5f;
		drawTooltipWindow.X = rect.X + single + 5f;
		x.X = drawTooltipWindow.X + single + 5f;
		if (firstWinBonus)
		{
			GuiTexture guiTexture1 = new GuiTexture();
			guiTexture1.SetTexture("PvPDominationGui", "separator");
			guiTexture1.X = 193f;
			guiTexture1.Y = 390f;
			base.AddGuiElement(guiTexture1);
			this.pvpDominationGuiForDelete.Add(guiTexture1);
			GuiLabel guiLabel1 = new GuiLabel()
			{
				boundries = new Rect(205f, 380f, 370f, 20f),
				text = StaticData.Translate("key_pvp_lbl_first_win"),
				FontSize = 14,
				Alignment = 4,
				Font = GuiLabel.FontBold,
				TextColor = GuiNewStyleBar.aquamarineColor
			};
			base.AddGuiElement(guiLabel1);
			this.pvpDominationGuiForDelete.Add(guiLabel1);
			GuiTexture rect1 = new GuiTexture();
			rect1.SetTexture("Shop", "Xp");
			rect1.boundries = new Rect(295f, 420f, single, single1);
			rect1.isHoverAware = true;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_pvp_reward_tooltip_xp"),
				customData2 = rect1
			};
			rect1.tooltipWindowParam = eventHandlerParam;
			rect1.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			base.AddGuiElement(rect1);
			this.pvpDominationGuiForDelete.Add(rect1);
			GuiTexture drawTooltipWindow1 = new GuiTexture();
			drawTooltipWindow1.SetTexture("Shop", "Ultralibrium");
			drawTooltipWindow1.boundries = new Rect(390f, 420f, single, single1);
			drawTooltipWindow1.isHoverAware = true;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_pvp_reward_tooltip_ultralibrium"),
				customData2 = drawTooltipWindow1
			};
			drawTooltipWindow1.tooltipWindowParam = eventHandlerParam;
			drawTooltipWindow1.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			base.AddGuiElement(drawTooltipWindow1);
			this.pvpDominationGuiForDelete.Add(drawTooltipWindow1);
			GuiTexture x1 = new GuiTexture();
			x1.SetTexture("Shop", "Cash");
			x1.boundries = new Rect(485f, 420f, single, single1);
			x1.isHoverAware = true;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_pvp_reward_tooltip_cash"),
				customData2 = x1
			};
			x1.tooltipWindowParam = eventHandlerParam;
			x1.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			base.AddGuiElement(x1);
			this.pvpDominationGuiForDelete.Add(x1);
			single2 = (370f - 3f * single - 10f) / 2f;
			rect1.X = guiLabel.X + single2;
			drawTooltipWindow1.X = rect1.X + single + 5f;
			x1.X = drawTooltipWindow1.X + single + 5f;
		}
	}

	private void FillPvPRankingData()
	{
		PvPLeague pvPLeague = this.rankingData.key;
		this.ClearPvPDominationGui();
		string empty = string.Empty;
		switch (pvPLeague)
		{
			case 1:
			{
				this.selectedScreen = 1;
				empty = "key_pvp_league_window_title_bronze";
				break;
			}
			case 2:
			{
				this.selectedScreen = 2;
				empty = "key_pvp_league_window_title_silver";
				break;
			}
			case 3:
			{
				this.selectedScreen = 3;
				empty = "key_pvp_league_window_title_gold";
				break;
			}
		}
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(277f, 25f, 466f, 36f),
			text = StaticData.Translate(empty),
			FontSize = 20,
			Alignment = 4,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor
		};
		base.AddGuiElement(guiLabel);
		this.pvpDominationGuiForDelete.Add(guiLabel);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("PvPDominationGui", "rankingTable");
		guiTexture.X = 185f;
		guiTexture.Y = 157f;
		base.AddGuiElement(guiTexture);
		this.pvpDominationGuiForDelete.Add(guiTexture);
		this.CreatePvPLeagueBtns((byte)pvPLeague);
		this.CreatePvPLeagueHeader();
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("PvPDominationGui", "btnPvpSignupGreen");
		guiButtonFixed.X = 344f;
		guiButtonFixed.Y = 511f;
		guiButtonFixed.Caption = StaticData.Translate("key_pvp_btn_caption_play_pvp");
		guiButtonFixed.Alignment = 4;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, PVPWindow.CreatePvPDomiantionSignUp);
		base.AddGuiElement(guiButtonFixed);
		this.pvpDominationGuiForDelete.Add(guiButtonFixed);
		GuiButtonFixed action = new GuiButtonFixed();
		action.SetTexture("PvPDominationGui", "pageFirst");
		action.X = 336f;
		action.Y = 473f;
		action.Caption = string.Empty;
		action.Alignment = 4;
		action.Clicked = new Action<EventHandlerParam>(this, PVPWindow.OnFirstClick);
		base.AddGuiElement(action);
		this.pvpDominationGuiForDelete.Add(action);
		GuiButtonFixed guiButtonFixed1 = new GuiButtonFixed();
		guiButtonFixed1.SetTexture("PvPDominationGui", "pagePrev");
		guiButtonFixed1.X = 377f;
		guiButtonFixed1.Y = 473f;
		guiButtonFixed1.Caption = string.Empty;
		guiButtonFixed1.Alignment = 4;
		guiButtonFixed1.Clicked = new Action<EventHandlerParam>(this, PVPWindow.OnPrevClick);
		base.AddGuiElement(guiButtonFixed1);
		this.pvpDominationGuiForDelete.Add(guiButtonFixed1);
		GuiButtonFixed empty1 = new GuiButtonFixed();
		empty1.SetTexture("PvPDominationGui", "pageNext");
		empty1.X = 603f;
		empty1.Y = 473f;
		empty1.Caption = string.Empty;
		empty1.Alignment = 4;
		empty1.Clicked = new Action<EventHandlerParam>(this, PVPWindow.OnNextClick);
		base.AddGuiElement(empty1);
		this.pvpDominationGuiForDelete.Add(empty1);
		GuiButtonFixed action1 = new GuiButtonFixed();
		action1.SetTexture("PvPDominationGui", "pageLast");
		action1.X = 645f;
		action1.Y = 473f;
		action1.Caption = string.Empty;
		action1.Alignment = 4;
		action1.Clicked = new Action<EventHandlerParam>(this, PVPWindow.OnLastClick);
		base.AddGuiElement(action1);
		this.pvpDominationGuiForDelete.Add(action1);
		this.DrawLeagueMembers();
	}

	private string GetGamePlayerLimit(PvPGameType game)
	{
		string empty = string.Empty;
		switch (game.mode)
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
		return empty;
	}

	private void OnBackBtnClicked(object prm)
	{
		EventHandlerParam eventHandlerParam;
		this.isWinnersListRequested = false;
		switch (this.selectedScreen)
		{
			case 0:
			{
				this.CreatePvPDomiantionSignUp(null);
				break;
			}
			case 1:
			{
				eventHandlerParam = new EventHandlerParam()
				{
					customData = (PvPLeague)1
				};
				this.OnPvPLeagueClicked(eventHandlerParam);
				break;
			}
			case 2:
			{
				eventHandlerParam = new EventHandlerParam()
				{
					customData = (PvPLeague)2
				};
				this.OnPvPLeagueClicked(eventHandlerParam);
				break;
			}
			case 3:
			{
				eventHandlerParam = new EventHandlerParam()
				{
					customData = (PvPLeague)3
				};
				this.OnPvPLeagueClicked(eventHandlerParam);
				break;
			}
		}
	}

	private void OnBackToSpaceClicked(object prm)
	{
		playWebGame.udp.ExecuteCommand(158, null);
	}

	private void OnFirstClick(object prm)
	{
		// 
		// Current member / type: System.Void PVPWindow::OnFirstClick(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnFirstClick(System.Object)
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..(Expression , Instruction ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\FixBinaryExpressionsStep.cs:строка 291
		//    в ..(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\FixBinaryExpressionsStep.cs:строка 48
		//    в Telerik.JustDecompiler.Decompiler.ExpressionDecompilerStep.(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\ExpressionDecompilerStep.cs:строка 93
		//    в ..(MethodBody ,  , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 88
		//    в ..(MethodBody , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 70
		//    в Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 95
		//    в Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 58
		//    в ..(ILanguage , MethodDefinition ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:строка 117
		// 
		// mailto: JustDecompilePublicFeedback@telerik.com

	}

	private void OnLastClick(object prm)
	{
		// 
		// Current member / type: System.Void PVPWindow::OnLastClick(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnLastClick(System.Object)
		// 
		// Очередь пуста.
		//    в System.ThrowHelper.ThrowInvalidOperationException(ExceptionResource resource)
		//    в Telerik.JustDecompiler.Decompiler.TypeInference.TypeInferer.(ICollection`1 ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\TypeInferer.cs:строка 525
		//    в Telerik.JustDecompiler.Decompiler.TypeInference.TypeInferer.() в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\TypeInferer.cs:строка 445
		//    в Telerik.JustDecompiler.Decompiler.TypeInference.TypeInferer.() в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\TypeInferer.cs:строка 363
		//    в Telerik.JustDecompiler.Decompiler.TypeInference.TypeInferer.() в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\TypeInferer.cs:строка 307
		//    в Telerik.JustDecompiler.Decompiler.ExpressionDecompilerStep.(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\ExpressionDecompilerStep.cs:строка 88
		//    в ..(MethodBody ,  , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 88
		//    в ..(MethodBody , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 70
		//    в Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 95
		//    в Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 58
		//    в ..(ILanguage , MethodDefinition ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:строка 117
		// 
		// mailto: JustDecompilePublicFeedback@telerik.com

	}

	private void OnNextClick(object prm)
	{
		// 
		// Current member / type: System.Void PVPWindow::OnNextClick(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnNextClick(System.Object)
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..(Expression , Instruction ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\FixBinaryExpressionsStep.cs:строка 291
		//    в ..(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\FixBinaryExpressionsStep.cs:строка 48
		//    в Telerik.JustDecompiler.Decompiler.ExpressionDecompilerStep.(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\ExpressionDecompilerStep.cs:строка 93
		//    в ..(MethodBody ,  , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 88
		//    в ..(MethodBody , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 70
		//    в Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 95
		//    в Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 58
		//    в ..(ILanguage , MethodDefinition ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:строка 117
		// 
		// mailto: JustDecompilePublicFeedback@telerik.com

	}

	private void OnPenaltyEnd()
	{
		this.CreatePvPDomiantionSignUp(null);
	}

	private void OnPrevClick(object prm)
	{
		// 
		// Current member / type: System.Void PVPWindow::OnPrevClick(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnPrevClick(System.Object)
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..( , Int32 , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:строка 78
		//    в ..( , Int32& , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:строка 33
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 57
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 33
		//    в ..(MethodBody ,  , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 88
		//    в ..(MethodBody , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 70
		//    в Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 95
		//    в Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 58
		//    в ..(ILanguage , MethodDefinition ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:строка 117
		// 
		// mailto: JustDecompilePublicFeedback@telerik.com

	}

	private void OnPvPLeagueClicked(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void PVPWindow::OnPvPLeagueClicked(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnPvPLeagueClicked(EventHandlerParam)
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..( , Int32 , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:строка 78
		//    в ..( , Int32& , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:строка 33
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 57
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 33
		//    в ..(MethodBody ,  , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 88
		//    в ..(MethodBody , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 70
		//    в Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 95
		//    в Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 58
		//    в ..(ILanguage , MethodDefinition ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:строка 117
		// 
		// mailto: JustDecompilePublicFeedback@telerik.com

	}

	private void OnRetreatCancel(object prm)
	{
		AndromedaGui.gui.RemoveWindow(this.dialogWindow.handler);
		AndromedaGui.gui.activeToolTipId = -1;
		this.dialogWindow = null;
	}

	private void OnRetreatClicked(object prm)
	{
		this.dialogWindow = new GuiWindow()
		{
			isModal = true
		};
		this.dialogWindow.SetBackgroundTexture("FrameworkGUI", "dialogBoxFrame");
		this.dialogWindow.isHidden = false;
		this.dialogWindow.zOrder = 220;
		this.dialogWindow.PutToCenter();
		this.dialogWindow.isHidden = false;
		AndromedaGui.gui.AddWindow(this.dialogWindow);
		AndromedaGui.gui.activeToolTipId = this.dialogWindow.handler;
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("FrameworkGUI", "buttonLeftRed");
		guiButtonFixed.X = 33f;
		guiButtonFixed.Y = 127f;
		guiButtonFixed.Caption = StaticData.Translate("key_pvp_retreat_dialog_ok");
		guiButtonFixed.FontSize = 12;
		guiButtonFixed.Alignment = 4;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, PVPWindow.OnRetreatConfirm);
		this.dialogWindow.AddGuiElement(guiButtonFixed);
		GuiButtonFixed action = new GuiButtonFixed();
		action.SetTexture("FrameworkGUI", "buttonRightBlue");
		action.boundries.set_x(319f);
		action.boundries.set_y(127f);
		action.Caption = StaticData.Translate("key_pvp_retreat_dialog_cancel");
		action.FontSize = 12;
		action.Alignment = 4;
		action.Clicked = new Action<EventHandlerParam>(this, PVPWindow.OnRetreatCancel);
		this.dialogWindow.AddGuiElement(action);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(34f, 34f, 430f, 90f),
			text = string.Format(StaticData.Translate("key_pvp_retreat_warning_question"), -StaticData.PVP_RETREAT_GAME_HONOR),
			Alignment = 4,
			FontSize = 14
		};
		this.dialogWindow.AddGuiElement(guiLabel);
	}

	private void OnRetreatConfirm(object prm)
	{
		AndromedaGui.gui.RemoveWindow(this.dialogWindow.handler);
		this.dialogWindow = null;
		playWebGame.udp.ExecuteCommand(157, null);
	}

	private void OnTrackMeClick(object prm)
	{
		// 
		// Current member / type: System.Void PVPWindow::OnTrackMeClick(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnTrackMeClick(System.Object)
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..( , Int32 , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:строка 78
		//    в ..( , Int32& , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:строка 33
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 57
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 33
		//    в ..(MethodBody ,  , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 88
		//    в ..(MethodBody , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 70
		//    в Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 95
		//    в Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 58
		//    в ..(ILanguage , MethodDefinition ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:строка 117
		// 
		// mailto: JustDecompilePublicFeedback@telerik.com

	}

	private void OnViewWinnersClick(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void PVPWindow::OnViewWinnersClick(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnViewWinnersClick(EventHandlerParam)
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..( , Int32 , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:строка 78
		//    в ..( , Int32& , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:строка 33
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 57
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(IfStatement ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 359
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 55
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 33
		//    в ..(MethodBody ,  , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 88
		//    в ..(MethodBody , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 70
		//    в Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 95
		//    в Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 58
		//    в ..(ILanguage , MethodDefinition ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:строка 117
		// 
		// mailto: JustDecompilePublicFeedback@telerik.com

	}

	public void PopulatePlayePvPScreen(string errorMsg = "")
	{
		if (this.selectedScreen != 0)
		{
			return;
		}
		this.CreatePvPDomiantionSignUp(errorMsg);
	}

	public void PopulatePvPLeagueRanking()
	{
		if (this.rankingData == null || this.rankingData.leagueInfo == null || this.rankingData.key != this.selectedScreen)
		{
			return;
		}
		this.FillPvPRankingData();
	}

	public void PopulatePvPWindowInArenaGame()
	{
		foreach (GuiElement guiElement in this.pvpStatsForDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.pvpStatsForDelete.Clear();
		if (NetworkScript.player.pvpGame.gameType.winType == null)
		{
			this.CreatePvPWindowInDominationGame();
		}
	}

	public void PopulatePvPWinners(PvPLeagueRewardetPlayer[] winers)
	{
		if (this.isWinnersListRequested)
		{
			this.CreatePvPDominationInfo(winers);
		}
	}

	private void SignForPvP(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void PVPWindow::SignForPvP(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void SignForPvP(EventHandlerParam)
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..( , Int32 , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:строка 78
		//    в ..( , Int32& , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:строка 33
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 57
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 33
		//    в ..(MethodBody ,  , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 88
		//    в ..(MethodBody , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 70
		//    в Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 95
		//    в Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 58
		//    в ..(ILanguage , MethodDefinition ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:строка 117
		// 
		// mailto: JustDecompilePublicFeedback@telerik.com

	}

	private void SignOutFromPvP(EventHandlerParam prm)
	{
		NetworkScript.player.pvpGameTypeSignedFor = 0;
		playWebGame.udp.ExecuteCommand(155, null);
		AndromedaGui.mainWnd.StopGameSearchingAnimation();
		this.CreatePvPDomiantionSignUp(null);
	}
}