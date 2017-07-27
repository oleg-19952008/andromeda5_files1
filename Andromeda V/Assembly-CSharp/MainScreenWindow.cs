using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TransferableObjects;
using UnityEngine;

public class MainScreenWindow
{
	public static GuiWindow playerInfoWindow;

	public GuiWindow quickSlotsWindow;

	public GuiWindow sideMenuWindow;

	public GuiWindow sideMenuOpenWindow;

	public GuiWindow playerStatsWindow;

	public GuiWindow storyTrackerWindow;

	public GuiWindow feedbackButtonWindow;

	public GuiButtonFixed btnShipConfig;

	public GuiButtonFixed btnFushion;

	public GuiButtonFixed btnUniversMap;

	public GuiButtonFixed btnLevelUp;

	public GuiButtonFixed btnTransformer;

	public GuiButtonFixed btnQuests;

	public GuiButtonFixed btnPlayerProfile;

	public GuiButtonFixed btnAutoPilot;

	public GuiButtonFixed btnChat;

	public GuiButtonFixed btnGuild;

	public GuiButtonFixed btnPVP;

	public GuiButtonFixed btnNovaShop;

	public GuiButtonFixed btnRanking;

	public GuiButtonFixed btnSystemMenu;

	public GuiButtonFixed btnOpenMenu;

	public GuiButtonFixed btnSendGift;

	private GuiItemTracker lblCash;

	private GuiItemTracker lblNova;

	private GuiTexture playerAvatar;

	private GuiNewStyleBar barHull;

	private GuiNewStyleBar barShield;

	private GuiNewStyleBar barCargo;

	private GuiNewStyleBar barExp;

	private GuiLabel lblHullVal;

	private GuiLabel lblShieldVal;

	private GuiLabel lblCargoVal;

	private GuiLabel lblPlayerName;

	private GuiLabel lblPlayerLevel;

	public GuiWindow activeWindow;

	public byte activWindowIndex = 255;

	private Type[] windowTypes = new Type[] { typeof(__FusionWindow), typeof(__ConfigWindow), typeof(__SkillsTree), typeof(__MerchantWindow), typeof(__DockWindow), typeof(__VaultWindow), typeof(__SystemWindow), typeof(UniverseMapScreenWindow), typeof(RankingWindow), typeof(__InBaseResourceTrader), typeof(__QuestWindow), typeof(NovaShop), typeof(__ChatWindow), typeof(__ComingSoon), typeof(__LevelUpWindow), typeof(PVPWindow), typeof(EscapeToHydraWindow), typeof(ProfileScreen), typeof(GuildWindow), typeof(NewPoiScreenWindow), typeof(SocialWindow), typeof(CustomMessageWindow), typeof(TransformerWindow), typeof(NewQuestsWindow), typeof(MinimapWindow), typeof(SignUpNowWindow), typeof(DailyRewardsWindow), typeof(FeedbackWindow), typeof(GetFreeNovaWindow), typeof(RateUSWindow), typeof(AvailableBonusesWindow), typeof(InstanceDifficultyWindow), typeof(PowerUpsWindow), typeof(SendGiftsWindow), typeof(WarScreenWindow), typeof(GameMessagesWindow), typeof(CraftingBlueprntsWindow) };

	private GuiTextureAnimated[] weapon_slots_no_ammo;

	private GuiTexture[] weapon_slots;

	private GuiTexture[] weapon_slots_inactive;

	private static GuiElement[] weapon_slots_current;

	private GuiTexture[] skill_slots;

	private GuiTexture[] booster_slots;

	private List<GuiElement> forDelete = new List<GuiElement>();

	private bool[] boosterSlotUsage;

	[NonSerialized]
	public KeyboardShortcuts kb;

	public bool hasMenuOpen;

	private bool isSideMenuOnScreen;

	private bool lastState;

	private GuiTextureAnimated btnAnimation;

	private GuiTextureAnimated btnSideMenuAnimation;

	private GuiTextureAnimated btnLevelUpAnimation;

	private GuiTextureAnimated btnChatAnimation;

	private GuiTextureAnimated btnTransformerAnimation;

	private GuiTextureAnimated cargoFullAnimation;

	private GuiLabel lblHonor;

	private GuiLabel lblHonorTitle;

	private GuiLabel lblPSCash;

	private GuiLabel lblPSNova;

	private GuiLabel lblEq;

	private GuiTexture cashTx;

	private GuiTexture novaTx;

	private GuiTexture eqTx;

	private GuiButtonFixed btnClose;

	private GuiItemTracker novaItemTracker;

	private GuiItemTracker cashItemTracker;

	private GuiItemTracker viralItemTracker;

	private GuiItemTracker ultralibriumItemTracker;

	public GuiLabel fpsMeter;

	private GuiNewStyleBar xpBar;

	private GuiNewStyleBar cargoBar;

	private GuiLabel levelLbl;

	private GuiLabel levelValueLbl;

	private GuiLabel cargoLbl;

	private GuiLabel cargoValueLbl;

	public Texture2D feedbackTexture;

	private bool isCargoFull;

	private bool areAllValuesAquirred;

	private string playerNameSD;

	private string playerLevelSD;

	private string cargoValueSD;

	private short playerLevelValueToCheck;

	private long cargoValueToCheck;

	private GuiTexture dot;

	private GuiWindow backgroundWindow;

	public Action OnCloseWindowCallback;

	public GuiWindow questTrackerWindow;

	private GuiTexture notificationBg;

	private GuiLabel notificationCounter;

	private GuiWindow pendingRewardNotificationWindow;

	private float leftSideMenuX;

	private float rightSideMenuX;

	private bool isMenuOn = true;

	private static float animationTime;

	public bool hasGuildInvintation;

	private GuiWindow pvpDominationScoreWindow;

	private GuiLabel yourTeamScore;

	private GuiLabel enemyTeamScore;

	private GuiTexture[] miningStationIcons;

	private GuiTexture yourTeamBar;

	private GuiTexture enemyTeamBar;

	private GuiWindow freezeTimeWindow;

	private GuiWindow slowIndicationWindow;

	private GuiWindow stunIndicationWindow;

	private List<GuiElement> stunElementsForDelete = new List<GuiElement>();

	private GuiWindow disarmIndicationWindow;

	private List<GuiElement> disarmElementsForDelete = new List<GuiElement>();

	private GuiWindow shockIndicationWindow;

	private List<GuiElement> shockElementsForDelete = new List<GuiElement>();

	private List<GuiElement> energyElementsForDelete = new List<GuiElement>();

	private bool isCriticalIndicationOnScreen;

	private GuiWindow instanceStatsWindow;

	private GuiLabel instanceProgress;

	private GuiLabel difficultyLbl;

	private GuiWindow factionWarIndicationWindow;

	private GuiWindow factionWarNotificationWindow;

	private GuiWindow messagesIndicationWindow;

	static MainScreenWindow()
	{
		MainScreenWindow.animationTime = 0.8f;
		MainScreenWindow.weapon_slots_current = new GuiElement[6];
		for (int i = 0; i < 6; i++)
		{
			MainScreenWindow.weapon_slots_current[i] = null;
		}
	}

	public MainScreenWindow()
	{
	}

	private void CastSkillFromSlot(EventHandlerParam prm)
	{
		if (prm.button != EventHandlerParam.MouseButton.Right)
		{
			NetworkScript.player.shipScript.StartCastingSkill((int)prm.customData);
		}
	}

	public void CloseActiveWindow()
	{
		if (this.activeWindow == null)
		{
			this.activWindowIndex = 255;
			return;
		}
		if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute && this.activWindowIndex != 20 && this.activWindowIndex != 21)
		{
			AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "menu_open_close");
			AudioManager.PlayGUISound(fromStaticSet);
		}
		if (this.OnCloseWindowCallback != null)
		{
			this.OnCloseWindowCallback.Invoke();
		}
		if (this.activWindowIndex == 17)
		{
			ProfileScreen.playerUserName = string.Empty;
		}
		this.activeWindow.OnClose();
		AndromedaGui.gui.RemoveWindow(this.activeWindow.handler);
		this.activeWindow = null;
		this.activWindowIndex = 255;
		this.hasMenuOpen = false;
		if (this.backgroundWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.backgroundWindow.handler);
			this.backgroundWindow = null;
		}
		if (NetworkScript.player != null && NetworkScript.player.shipScript != null)
		{
			NetworkScript.player.shipScript.isGuiClosed = true;
		}
	}

	private void CloseMainMenu(object prm)
	{
		Vector2 _mousePosition = Input.get_mousePosition();
		if (!this.sideMenuWindow.boundries.Contains(_mousePosition) && _mousePosition.x < (float)Screen.get_width())
		{
			MainScreenWindow.animationTime = 0.8f;
			this.sideMenuWindow.preDrawHandler = new Action<object>(this, MainScreenWindow.HideMenu);
			this.sideMenuOpenWindow.preDrawHandler = new Action<object>(this, MainScreenWindow.SideMenuMousePosition);
		}
	}

	public void Create()
	{
		this.kb = new KeyboardShortcuts();
		this.CreateSideMenu();
		this.CreateQuickSlotsMenu();
		this.CreateTopGUIWindow();
		QuestTrackerWindow.Initialise();
		QuestInfoWindow.Initialise();
		QuestInfoWindow.SetMuteActions(new Action(this, MainScreenWindow.MuteItemTrackers), new Action(this, MainScreenWindow.UnmuteItemTrackers));
		this.ShowFactionWarNotificationWindow();
		this.DrawPendingRewardsNotification();
		this.CreateMessagesWindow();
		if (NetworkScript.player.vessel.galaxy.get_galaxyId() != 1000 && __ChatWindow.wnd == null && !NetworkScript.player.vessel.isGuest)
		{
			__ChatWindow.OpenTheWindow(false);
		}
		if (NetworkScript.player.vessel.galaxy.get_galaxyId() != 1000 && !NetworkScript.player.vessel.isGuest && playWebGame.GAME_TYPE != "ru")
		{
			this.CreateFeedbackButton();
		}
	}

	private void CreateCloseButton()
	{
		if (this.activeWindow is __LevelUpWindow)
		{
			return;
		}
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed()
		{
			boundries = new Rect(847f, 12f, 58f, 46f)
		};
		guiButtonFixed.SetTexture("ConfigWindow", "X_btn_");
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, MainScreenWindow.OnCloseBtnClick);
		guiButtonFixed.isMuted = true;
		if (this.activeWindow is __ConfigWindow)
		{
			guiButtonFixed.boundries = new Rect(860f, 12f, 58f, 46f);
		}
		else if (this.activeWindow is __MerchantWindow)
		{
			guiButtonFixed.boundries = new Rect(950f, 13f, 58f, 46f);
		}
		else if (this.activeWindow is MinimapWindow)
		{
			guiButtonFixed.SetTexture("FrameworkGUI", "empty");
			guiButtonFixed.boundries = new Rect(952f, 0f, 72f, 72f);
		}
		else if (this.activeWindow is PVPWindow)
		{
			if ((NetworkScript.player.vessel.pvpState == 3 || NetworkScript.player.vessel.pvpState == 5 || NetworkScript.player.vessel.pvpState == 4) && NetworkScript.player.pvpGame != null)
			{
				guiButtonFixed.SetTexture("PvPDominationGui", "pvpClose");
				guiButtonFixed.boundries = new Rect(757f, 44f, 75f, 32f);
			}
			else
			{
				guiButtonFixed.SetTexture("PvPDominationGui", "pvpClose");
				guiButtonFixed.boundries = new Rect(758f, 25f, 75f, 32f);
			}
		}
		else if (this.activeWindow is SignUpNowWindow)
		{
			guiButtonFixed.SetTexture("LoginGui", "btnClose");
			guiButtonFixed.X = 473f;
			guiButtonFixed.Y = 101f;
		}
		else if (this.activeWindow is DailyRewardsWindow)
		{
			guiButtonFixed.SetTexture("DailyRewardsGui", "btnClose");
			guiButtonFixed.X = 682f;
			guiButtonFixed.Y = 23f;
		}
		else if (this.activeWindow is GetFreeNovaWindow)
		{
			guiButtonFixed.SetTexture("QuestInfoWindow", "buttonClose");
			guiButtonFixed.X = 697f;
			guiButtonFixed.Y = -2f;
		}
		else if (this.activeWindow is RateUSWindow)
		{
			guiButtonFixed.SetTexture("DailyRewardsGui", "btnClose");
			guiButtonFixed.X = 489f;
			guiButtonFixed.Y = 21f;
		}
		else if (this.activeWindow is __SystemWindow)
		{
			((__SystemWindow)this.activeWindow).btnClose = guiButtonFixed;
		}
		else if (this.activeWindow is AvailableBonusesWindow)
		{
			guiButtonFixed.SetTexture("FrameworkGUI", "dialogBoxFrameClose");
			guiButtonFixed.X = 418f;
			guiButtonFixed.Y = 0f;
		}
		else if (this.activeWindow is InstanceDifficultyWindow)
		{
			guiButtonFixed.SetTexture("InstanceDifficulty", "close");
			guiButtonFixed.X = 378f;
			guiButtonFixed.Y = 0f;
		}
		else if (this.activeWindow is PowerUpsWindow || this.activeWindow is SendGiftsWindow)
		{
			guiButtonFixed.SetTexture("PvPDominationGui", "pvpClose");
			guiButtonFixed.boundries = new Rect(757f, 25f, 75f, 32f);
		}
		else if (this.activeWindow is NewPoiScreenWindow || this.activeWindow is WarScreenWindow || this.activeWindow is UniverseMapScreenWindow || this.activeWindow is GameMessagesWindow || this.activeWindow is CraftingBlueprntsWindow)
		{
			guiButtonFixed.SetTexture("PoiScreenWindow", "btn-close");
			guiButtonFixed.boundries = new Rect(844f, 4f, 70f, 34f);
		}
		this.activeWindow.AddGuiElement(guiButtonFixed);
	}

	public void CreateCountdownWindow(int deltaTime)
	{
		this.btnPVP.isEnabled = false;
		this.btnUniversMap.isEnabled = false;
		if (this.btnTransformer != null)
		{
			this.btnTransformer.isEnabled = false;
		}
		if (this.activWindowIndex == 15 || this.activWindowIndex == 7 || this.activWindowIndex == 22)
		{
			this.CloseActiveWindow();
		}
		this.StopTransformerAnimation();
		this.StopGameSearchingAnimation();
		TargetingWnd.CreateCountdownWindow(deltaTime);
	}

	public void CreateFeedbackButton()
	{
		if (NetworkScript.player.vessel.isGuest)
		{
			return;
		}
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed()
		{
			Caption = string.Empty
		};
		guiButtonFixed.SetTexture("FrameworkGUI", "empty");
		guiButtonFixed.boundries = new Rect(120f, 0f, 52f, 37f);
		GuiButtonFixed drawTooltipWindow = new GuiButtonFixed()
		{
			Caption = string.Empty
		};
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = this.kb.GetCommandTooltip(KeyboardCommand.Feedback),
			customData2 = guiButtonFixed
		};
		drawTooltipWindow.tooltipWindowParam = eventHandlerParam;
		drawTooltipWindow.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		drawTooltipWindow.SetTexture("FrameworkGUI", "btnFeedback");
		drawTooltipWindow.Clicked = new Action<EventHandlerParam>(this, MainScreenWindow.TakeScreenShot);
		drawTooltipWindow.boundries = new Rect(0f, 0f, 52f, 37f);
		this.feedbackButtonWindow = new GuiWindow();
		this.feedbackButtonWindow.SetBackgroundTexture("FrameworkGUI", "empty");
		this.feedbackButtonWindow.boundries.set_height(drawTooltipWindow.boundries.get_height());
		this.feedbackButtonWindow.boundries.set_width(drawTooltipWindow.boundries.get_width());
		this.feedbackButtonWindow.boundries.set_y(0f);
		this.feedbackButtonWindow.boundries.set_x(this.playerStatsWindow.boundries.get_x() + this.playerStatsWindow.boundries.get_width() + 15f);
		this.feedbackButtonWindow.zOrder = 228;
		AndromedaGui.gui.AddWindow(this.feedbackButtonWindow);
		this.feedbackButtonWindow.isHidden = false;
		this.feedbackButtonWindow.AddGuiElement(drawTooltipWindow);
	}

	public void CreateMessagesWindow()
	{
		if (this.messagesIndicationWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.messagesIndicationWindow.handler);
			this.messagesIndicationWindow = null;
		}
		if (NetworkScript.player == null || NetworkScript.player.playerBelongings == null)
		{
			Debug.Log("CreateMessagesWindow return");
			return;
		}
		if (NetworkScript.player.vessel.pvpState == 3)
		{
			Debug.Log("CreateMessagesWindow return");
			return;
		}
		if (NetworkScript.player.vessel != null && NetworkScript.player.vessel.galaxy != null && NetworkScript.player.vessel.galaxy.get_galaxyId() == 1000)
		{
			Debug.Log("CreateMessagesWindow return");
			return;
		}
		this.messagesIndicationWindow = new GuiWindow()
		{
			boundries = new Rect((float)(Screen.get_width() - 82), 92f, 72f, 72f),
			isHidden = false,
			zOrder = 205
		};
		AndromedaGui.gui.AddWindow(this.messagesIndicationWindow);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed()
		{
			boundries = new Rect(0f, 0f, 72f, 72f)
		};
		guiButtonFixed.SetTexture("WarScreenWindow", "btnWar");
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.eventHandlerParam.customData = (byte)35;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, MainScreenWindow.OnWindowBtnClicked);
		this.messagesIndicationWindow.AddGuiElement(guiButtonFixed);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("PoiScreenWindow", "announcementIcon");
		guiTexture.boundries = new Rect(4f, 4f, 64f, 64f);
		this.messagesIndicationWindow.AddGuiElement(guiTexture);
		List<GameMessage> list = NetworkScript.player.myGameMessages;
		if (MainScreenWindow.<>f__am$cache77 == null)
		{
			MainScreenWindow.<>f__am$cache77 = new Func<GameMessage, bool>(null, (GameMessage t) => (t.type != null ? false : t.id > NetworkScript.player.lastSeenPrivateMessageId));
		}
		int num = Enumerable.Count<GameMessage>(Enumerable.Where<GameMessage>(list, MainScreenWindow.<>f__am$cache77));
		List<GameMessage> list1 = NetworkScript.player.myGameMessages;
		if (MainScreenWindow.<>f__am$cache78 == null)
		{
			MainScreenWindow.<>f__am$cache78 = new Func<GameMessage, bool>(null, (GameMessage t) => (t.type != 1 ? false : t.id > NetworkScript.player.lastSeenGameMessageId));
		}
		int num1 = Enumerable.Count<GameMessage>(Enumerable.Where<GameMessage>(list1, MainScreenWindow.<>f__am$cache78));
		if (num1 > 0 || num + num1 == 0)
		{
			guiTexture.SetTextureKeepSize("PoiScreenWindow", "announcementIcon");
		}
		else
		{
			guiTexture.SetTextureKeepSize("PoiScreenWindow", "privaetIcon");
		}
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(12f, 12f, 52f, 52f),
			text = (num + num1 != 0 ? (num + num1).ToString("N0") : string.Empty),
			Alignment = 4,
			FontSize = 16,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blackColorTransperant
		};
		this.messagesIndicationWindow.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(10f, 10f, 52f, 52f),
			text = (num + num1 != 0 ? (num + num1).ToString("N0") : string.Empty),
			Alignment = 4,
			FontSize = 16,
			Font = GuiLabel.FontBold,
			TextColor = Color.get_white()
		};
		this.messagesIndicationWindow.AddGuiElement(guiLabel1);
	}

	private void CreatePlayerInfo()
	{
		MainScreenWindow.playerInfoWindow = new GuiWindow();
		MainScreenWindow.playerInfoWindow.boundries.set_x(0f);
		MainScreenWindow.playerInfoWindow.boundries.set_y(0f);
		MainScreenWindow.playerInfoWindow.SetBackgroundTexture("NewGUI", "playerinfo");
		ref Rect rectPointer = ref MainScreenWindow.playerInfoWindow.boundries;
		rectPointer.set_width(rectPointer.get_width() + 5f);
		this.playerAvatar = new GuiTexture();
		this.playerAvatar.SetTexture("Targeting", "unknown");
		this.playerAvatar.boundries.set_x(10f);
		this.playerAvatar.boundries.set_y(10f);
		this.playerAvatar.SetSize(60f, 60f);
		MainScreenWindow.playerInfoWindow.AddGuiElement(this.playerAvatar);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("FrameworkGUI", "empty");
		guiButtonFixed.boundries = new Rect(0f, 0f, 225f, 75f);
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, MainScreenWindow.OnSelfSelectClicked);
		MainScreenWindow.playerInfoWindow.AddGuiElement(guiButtonFixed);
		this.barShield = new GuiNewStyleBar();
		this.barShield.SetCustumSizeBlueBar(70);
		this.barShield.current = 45f;
		this.barShield.boundries.set_x(85f);
		this.barShield.boundries.set_y(24f);
		MainScreenWindow.playerInfoWindow.AddGuiElement(this.barShield);
		this.barHull = new GuiNewStyleBar();
		this.barHull.SetCustumSizeGreenBar(70);
		this.barHull.current = 20f;
		this.barHull.boundries.set_x(85f);
		this.barHull.boundries.set_y(39f);
		MainScreenWindow.playerInfoWindow.AddGuiElement(this.barHull);
		this.barCargo = new GuiNewStyleBar();
		this.barCargo.SetCustumSizeOrangeBar(70);
		this.barCargo.current = 20f;
		this.barCargo.boundries.set_x(85f);
		this.barCargo.boundries.set_y(55f);
		MainScreenWindow.playerInfoWindow.AddGuiElement(this.barCargo);
		this.lblPlayerName = new GuiLabel()
		{
			boundries = new Rect(85f, 9f, 115f, 17f),
			Alignment = 3,
			text = string.Empty,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		MainScreenWindow.playerInfoWindow.AddGuiElement(this.lblPlayerName);
		this.lblPlayerLevel = new GuiLabel()
		{
			boundries = new Rect(85f, 9f, 30f, 17f),
			Alignment = 3,
			text = string.Empty,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor
		};
		MainScreenWindow.playerInfoWindow.AddGuiElement(this.lblPlayerLevel);
		this.lblHullVal = new GuiLabel()
		{
			boundries = new Rect(this.barHull.boundries.get_x() + 70f, this.barHull.boundries.get_y(), 70f, 17f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.greenColor,
			Font = GuiLabel.FontBold,
			text = string.Empty
		};
		MainScreenWindow.playerInfoWindow.AddGuiElement(this.lblHullVal);
		this.lblShieldVal = new GuiLabel()
		{
			boundries = new Rect(this.barShield.boundries.get_x() + 70f, this.barShield.boundries.get_y(), 70f, 17f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor,
			Font = GuiLabel.FontBold,
			text = string.Empty
		};
		MainScreenWindow.playerInfoWindow.AddGuiElement(this.lblShieldVal);
		this.lblCargoVal = new GuiLabel()
		{
			boundries = new Rect(this.barCargo.boundries.get_x() + 70f, this.barCargo.boundries.get_y(), 100f, 17f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.orangeColor,
			Font = GuiLabel.FontBold,
			text = string.Empty
		};
		MainScreenWindow.playerInfoWindow.AddGuiElement(this.lblCargoVal);
		AndromedaGui.gui.AddWindow(MainScreenWindow.playerInfoWindow);
		MainScreenWindow.playerInfoWindow.isHidden = false;
	}

	private void CreatePvPDominationWindow()
	{
		this.pvpDominationScoreWindow = new GuiWindow();
		this.pvpDominationScoreWindow.SetBackgroundTexture("PvPDominationGui", "scoreFrameBack");
		this.pvpDominationScoreWindow.isHidden = false;
		this.pvpDominationScoreWindow.zOrder = 227;
		this.pvpDominationScoreWindow.boundries.set_x((float)(Screen.get_width() / 2 - 233));
		this.pvpDominationScoreWindow.boundries.set_y(27f);
		AndromedaGui.gui.AddWindow(this.pvpDominationScoreWindow);
		this.yourTeamBar = new GuiTexture();
		this.yourTeamBar.SetTexture("PvPDominationGui", "scoreBarGreen");
		this.yourTeamBar.X = 175f;
		this.yourTeamBar.Y = 20f;
		this.yourTeamBar.boundries.set_width(0f);
		this.pvpDominationScoreWindow.AddGuiElement(this.yourTeamBar);
		this.enemyTeamBar = new GuiTexture();
		this.enemyTeamBar.SetTexture("PvPDominationGui", "scoreBarRed");
		this.enemyTeamBar.X = 290f;
		this.enemyTeamBar.Y = 20f;
		this.enemyTeamBar.boundries.set_width(0f);
		this.pvpDominationScoreWindow.AddGuiElement(this.enemyTeamBar);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("PvPDominationGui", "scoreFrameTop");
		guiTexture.X = 0f;
		guiTexture.Y = 0f;
		this.pvpDominationScoreWindow.AddGuiElement(guiTexture);
		this.miningStationIcons = new GuiTexture[3];
		for (int i = 0; i < 3; i++)
		{
			GuiTexture guiTexture1 = new GuiTexture();
			guiTexture1.SetTexture("PvPDominationGui", "iconDefault");
			guiTexture1.X = (float)(190 + i * 32);
			guiTexture1.Y = 20f;
			this.pvpDominationScoreWindow.AddGuiElement(guiTexture1);
			this.miningStationIcons[i] = guiTexture1;
		}
		this.yourTeamScore = new GuiLabel()
		{
			boundries = new Rect(18f, 18f, 37f, 18f),
			Alignment = 4,
			FontSize = 12,
			text = "0",
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.greenColor
		};
		this.pvpDominationScoreWindow.AddGuiElement(this.yourTeamScore);
		this.enemyTeamScore = new GuiLabel()
		{
			boundries = new Rect(411f, 18f, 37f, 18f),
			Alignment = 4,
			FontSize = 12,
			text = "0",
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.redColor
		};
		this.pvpDominationScoreWindow.AddGuiElement(this.enemyTeamScore);
	}

	public void CreateQuickSlotsMenu()
	{
		this.quickSlotsWindow = new GuiWindow();
		this.quickSlotsWindow.SetBackgroundTexture("MainScreenWindow", "quickslots");
		this.quickSlotsWindow.PutToHorizontalCenter();
		this.quickSlotsWindow.boundries.set_y((float)Screen.get_height() - this.quickSlotsWindow.boundries.get_height());
		this.fpsMeter = new GuiLabel()
		{
			boundries = new Rect(29f, 63f, 36f, 20f),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			text = string.Empty
		};
		this.quickSlotsWindow.AddGuiElement(this.fpsMeter);
		AndromedaGui.gui.AddWindow(this.quickSlotsWindow);
		this.quickSlotsWindow.isHidden = false;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("MainScreenWindow", "quickSlotsBarsFrame");
		guiTexture.X = 32f;
		guiTexture.Y = 34f;
		this.quickSlotsWindow.AddGuiElement(guiTexture);
		this.levelLbl = new GuiLabel()
		{
			boundries = new Rect(34f, 38f, 49f, 12f),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 10,
			TextColor = GuiNewStyleBar.blueColor,
			text = string.Empty
		};
		this.quickSlotsWindow.AddGuiElement(this.levelLbl);
		this.levelValueLbl = new GuiLabel()
		{
			boundries = new Rect(214f, 38f, 29f, 12f),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 10,
			TextColor = GuiNewStyleBar.blueColor,
			text = string.Empty
		};
		this.quickSlotsWindow.AddGuiElement(this.levelValueLbl);
		this.cargoLbl = new GuiLabel()
		{
			boundries = new Rect(250f, 38f, 49f, 12f),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 10,
			TextColor = GuiNewStyleBar.orangeColor,
			text = StaticData.Translate("key_quickslots_cargo_lbl")
		};
		this.quickSlotsWindow.AddGuiElement(this.cargoLbl);
		this.cargoValueLbl = new GuiLabel()
		{
			boundries = new Rect(431f, 38f, 29f, 12f),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 10,
			TextColor = GuiNewStyleBar.orangeColor,
			text = string.Empty
		};
		this.quickSlotsWindow.AddGuiElement(this.cargoValueLbl);
		this.xpBar = new GuiNewStyleBar();
		this.xpBar.SetCustumSizeBlueBar(120);
		this.xpBar.X = 88f;
		this.xpBar.Y = 36f;
		this.xpBar.maximum = 100f;
		this.xpBar.current = 50f;
		this.quickSlotsWindow.AddGuiElement(this.xpBar);
		this.cargoBar = new GuiNewStyleBar();
		this.cargoBar.SetCustumSizeOrangeBar(120);
		this.cargoBar.X = 305f;
		this.cargoBar.Y = 36f;
		this.cargoBar.maximum = 100f;
		this.cargoBar.current = 50f;
		this.quickSlotsWindow.AddGuiElement(this.cargoBar);
		this.isCargoFull = false;
		this.quickSlotsWindow.customOnGUIAction = new Action(this, MainScreenWindow.QuickSlotWindowHandler);
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("FrameworkGUI", "empty");
		rect.boundries = new Rect(26f, 34f, 220f, 20f);
		rect.isHoverAware = true;
		rect.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.DrawExperienceBarTooltip);
		this.quickSlotsWindow.AddGuiElement(rect);
		this.weapon_slots_no_ammo = new GuiTextureAnimated[6];
		for (int i = 0; i < 6; i++)
		{
			this.weapon_slots_no_ammo[i] = new GuiTextureAnimated();
			this.weapon_slots_no_ammo[i].boundries.set_x((float)(45 + i * 34));
			this.weapon_slots_no_ammo[i].boundries.set_y(0f);
			this.weapon_slots_no_ammo[i].Init("no_ammo", "no_ammo", "no_ammo/weapons_slot_no_ammo");
			this.weapon_slots_no_ammo[i].rotationTime = 0.333f;
		}
		this.weapon_slots = new GuiTexture[6];
		for (int j = 0; j < 6; j++)
		{
			if (MainScreenWindow.weapon_slots_current[j] != null)
			{
				try
				{
					this.quickSlotsWindow.RemoveGuiElement(MainScreenWindow.weapon_slots_current[j]);
				}
				catch (Exception exception)
				{
				}
			}
			this.weapon_slots[j] = new GuiTexture();
			this.weapon_slots[j].boundries.set_x((float)(45 + j * 34));
			this.weapon_slots[j].boundries.set_y(0f);
			this.weapon_slots[j].SetTexture("MainScreenWindow", "weapons_slot");
			this.quickSlotsWindow.AddGuiElement(this.weapon_slots[j]);
			MainScreenWindow.weapon_slots_current[j] = this.weapon_slots[j];
		}
		this.weapon_slots_inactive = new GuiTexture[6];
		for (int k = 0; k < 6; k++)
		{
			this.weapon_slots_inactive[k] = new GuiTexture();
			this.weapon_slots_inactive[k].boundries.set_x((float)(45 + k * 34));
			this.weapon_slots_inactive[k].boundries.set_y(0f);
			this.weapon_slots_inactive[k].SetTexture("FrameworkGUI", "empty");
			this.weapon_slots_inactive[k].SetSize(38f, 38f);
		}
		this.DrawAttachedWeapons();
		this.booster_slots = new GuiTexture[4];
		for (int l = 0; l < 4; l++)
		{
			this.booster_slots[l] = new GuiTexture();
			this.booster_slots[l].boundries.set_x((float)(309 + l * 34));
			this.booster_slots[l].boundries.set_y(0f);
			this.booster_slots[l].SetTexture("MainScreenWindow", "weapons_slot");
			this.quickSlotsWindow.AddGuiElement(this.booster_slots[l]);
		}
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed()
		{
			Caption = string.Empty
		};
		guiButtonFixed.SetTexture("FrameworkGUI", "empty");
		guiButtonFixed.eventHandlerParam.customData = (byte)11;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, MainScreenWindow.OnWindowBtnClicked);
		guiButtonFixed.boundries = new Rect(309f, 0f, 136f, 34f);
		this.quickSlotsWindow.AddGuiElement(guiButtonFixed);
		this.DrawActiveBoosters();
		this.skill_slots = new GuiTexture[9];
		for (int m = 0; m < 9; m++)
		{
			this.skill_slots[m] = new GuiTexture();
			this.skill_slots[m].boundries.set_x((float)(68 + m * 40));
			this.skill_slots[m].boundries.set_y(54f);
			this.skill_slots[m].SetTexture("SystemWindow", "skills_box");
			this.quickSlotsWindow.AddGuiElement(this.skill_slots[m]);
		}
		IEnumerator<ActiveSkillSlot> enumerator = NetworkScript.player.playerBelongings.skillConfig.skillSlots.get_Values().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				ActiveSkillSlot current = enumerator.get_Current();
				this.DrawSkillInBar(current.skillId, current.slotId);
			}
		}
		finally
		{
			if (enumerator == null)
			{
			}
			enumerator.Dispose();
		}
		for (int n = 0; n < 9; n++)
		{
			GuiLabel guiLabel = new GuiLabel()
			{
				boundries = new Rect((float)(68 + 40 * n), 82f, 35f, 10f),
				text = this.kb.GetCommandKeyCodeShort((KeyboardCommand)(17 + n)),
				Alignment = 6,
				FontSize = 12
			};
			this.quickSlotsWindow.AddGuiElement(guiLabel);
		}
		IEnumerator<ActiveSkillSlot> enumerator1 = NetworkScript.player.playerBelongings.skillConfig.skillSlots.get_Values().GetEnumerator();
		try
		{
			while (enumerator1.MoveNext())
			{
				this.ReloadSkillSlot(enumerator1.get_Current());
			}
		}
		finally
		{
			if (enumerator1 == null)
			{
			}
			enumerator1.Dispose();
		}
		if (this.isCriticalIndicationOnScreen)
		{
			this.RemoveCriticalIndication();
			this.ShowCriticalIndication();
		}
	}

	private void CreateSideMenu()
	{
		object obj;
		if (Application.get_loadedLevelName() == "Tutorial")
		{
			this.isSideMenuOnScreen = true;
		}
		else
		{
			this.sideMenuOpenWindow = new GuiWindow();
			this.sideMenuOpenWindow.SetBackgroundTexture("MainScreenWindow", "showMenu_frame");
			this.sideMenuOpenWindow.PutToVerticalCenter();
			this.sideMenuOpenWindow.boundries.set_x((float)(Screen.get_width() - 73));
			this.btnOpenMenu = new GuiButtonFixed()
			{
				boundries = new Rect(0f, 0f, 73f, 146f)
			};
			this.btnOpenMenu.SetTexture("MainScreenWindow", "showMenu");
			this.btnOpenMenu.Caption = string.Empty;
			this.btnOpenMenu.isMuted = true;
			this.sideMenuOpenWindow.AddGuiElement(this.btnOpenMenu);
			AndromedaGui.gui.AddWindow(this.sideMenuOpenWindow);
			this.sideMenuOpenWindow.isHidden = false;
		}
		this.sideMenuWindow = new GuiWindow();
		this.sideMenuWindow.SetBackgroundTexture("MainScreenWindow", "sidemenu");
		this.sideMenuWindow.PutToVerticalCenter();
		obj = (Application.get_loadedLevelName() == "Tutorial" ? Screen.get_width() - 163 : Screen.get_width());
		this.sideMenuWindow.boundries.set_x((float)obj);
		this.sideMenuWindow.zOrder = (byte)((Application.get_loadedLevelName() == "Tutorial" ? 200 : 220));
		if (Application.get_loadedLevelName() != "Tutorial")
		{
			this.sideMenuWindow.customOnGUIAction = new Action(this, MainScreenWindow.SideMenuMenager);
		}
		this.btnPlayerProfile = new GuiButtonFixed()
		{
			boundries = new Rect(29f, 115f, 30f, 30f)
		};
		this.btnPlayerProfile.SetTexture("MainScreenWindow", "player");
		this.btnPlayerProfile.Caption = string.Empty;
		this.btnPlayerProfile.eventHandlerParam.customData = (byte)17;
		this.btnPlayerProfile.Clicked = new Action<EventHandlerParam>(this, MainScreenWindow.OnWindowBtnClicked);
		this.btnPlayerProfile.isMuted = true;
		this.btnPlayerProfile.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.SmartTooltipPlayerProfile);
		this.sideMenuWindow.AddGuiElement(this.btnPlayerProfile);
		this.btnNovaShop = new GuiButtonFixed()
		{
			boundries = new Rect(94f, 115f, 30f, 30f)
		};
		this.btnNovaShop.SetTexture("MainScreenWindow", "novaShop");
		this.btnNovaShop.Caption = string.Empty;
		this.btnNovaShop.Clicked = new Action<EventHandlerParam>(this, MainScreenWindow.OnWindowBtnClicked);
		this.btnNovaShop.isMuted = true;
		this.btnNovaShop.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.CreateTooltipNovaShop);
		this.btnNovaShop.eventHandlerParam.customData = (byte)11;
		this.sideMenuWindow.AddGuiElement(this.btnNovaShop);
		this.btnShipConfig = new GuiButtonFixed()
		{
			boundries = new Rect(29f, 160f, 30f, 30f)
		};
		this.btnShipConfig.SetTexture("MainScreenWindow", "shipConfig");
		this.btnShipConfig.Caption = string.Empty;
		this.btnShipConfig.eventHandlerParam.customData = (byte)1;
		this.btnShipConfig.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.CreateTooltipShipConfig);
		this.btnShipConfig.Clicked = new Action<EventHandlerParam>(this, MainScreenWindow.OnWindowBtnClicked);
		this.btnShipConfig.isMuted = true;
		this.sideMenuWindow.AddGuiElement(this.btnShipConfig);
		this.btnLevelUp = new GuiButtonFixed()
		{
			boundries = new Rect(94f, 160f, 30f, 30f)
		};
		this.btnLevelUp.SetTexture("MainScreenWindow", "levelUp");
		this.btnLevelUp.Caption = string.Empty;
		this.btnLevelUp.eventHandlerParam.customData = (byte)2;
		this.btnLevelUp.Clicked = new Action<EventHandlerParam>(this, MainScreenWindow.OnWindowBtnClicked);
		this.btnLevelUp.isMuted = true;
		this.btnLevelUp.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.CreateTooltipLevelUp);
		this.sideMenuWindow.AddGuiElement(this.btnLevelUp);
		this.btnFushion = new GuiButtonFixed()
		{
			boundries = new Rect(29f, 205f, 30f, 30f)
		};
		this.btnFushion.SetTexture("MainScreenWindow", "fushion");
		this.btnFushion.Caption = string.Empty;
		this.btnFushion.eventHandlerParam.customData = (byte)0;
		this.btnFushion.Clicked = new Action<EventHandlerParam>(this, MainScreenWindow.OnWindowBtnClicked);
		this.btnFushion.isMuted = true;
		this.btnFushion.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.CreateTooltipFusion);
		this.sideMenuWindow.AddGuiElement(this.btnFushion);
		this.btnUniversMap = new GuiButtonFixed()
		{
			boundries = new Rect(94f, 205f, 30f, 30f)
		};
		this.btnUniversMap.SetTexture("MainScreenWindow", "universMap");
		this.btnUniversMap.Caption = string.Empty;
		this.btnUniversMap.Clicked = new Action<EventHandlerParam>(this, MainScreenWindow.OnWindowBtnClicked);
		this.btnUniversMap.isMuted = true;
		this.btnUniversMap.isEnabled = NetworkScript.player.playerBelongings.playerLevel >= 7;
		this.btnUniversMap.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.SmartTooltipUniverseMap);
		this.btnUniversMap.eventHandlerParam.customData = (byte)7;
		this.sideMenuWindow.AddGuiElement(this.btnUniversMap);
		this.btnGuild = new GuiButtonFixed()
		{
			boundries = new Rect(29f, 251f, 30f, 30f)
		};
		this.btnGuild.SetTexture("MainScreenWindow", "guild");
		this.btnGuild.Caption = string.Empty;
		this.btnGuild.Clicked = new Action<EventHandlerParam>(this, MainScreenWindow.OnWindowBtnClicked);
		this.btnGuild.isMuted = true;
		this.btnGuild.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.SmartTooltipGuild);
		this.btnGuild.eventHandlerParam.customData = (byte)18;
		this.btnGuild.isEnabled = NetworkScript.player.playerBelongings.playerLevel >= 9;
		this.SetGuildButtonAlerted();
		this.sideMenuWindow.AddGuiElement(this.btnGuild);
		this.btnRanking = new GuiButtonFixed()
		{
			boundries = new Rect(94f, 251f, 30f, 30f)
		};
		this.btnRanking.SetTexture("MainScreenWindow", "ranking");
		this.btnRanking.Caption = string.Empty;
		this.btnRanking.Clicked = new Action<EventHandlerParam>(this, MainScreenWindow.OnWindowBtnClicked);
		this.btnRanking.isMuted = true;
		this.btnRanking.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.SmartTooltipRanking);
		this.btnRanking.eventHandlerParam.customData = (byte)8;
		this.sideMenuWindow.AddGuiElement(this.btnRanking);
		this.btnPVP = new GuiButtonFixed()
		{
			boundries = new Rect(29f, 296f, 30f, 30f)
		};
		this.btnPVP.SetTexture("MainScreenWindow", "pvp");
		this.btnPVP.Caption = string.Empty;
		this.btnPVP.Clicked = new Action<EventHandlerParam>(this, MainScreenWindow.OnWindowBtnClicked);
		this.btnPVP.isMuted = true;
		this.btnPVP.isEnabled = NetworkScript.player.playerBelongings.playerLevel >= 8;
		this.btnPVP.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.SmartTooltipPVP);
		this.btnPVP.eventHandlerParam.customData = (byte)15;
		this.sideMenuWindow.AddGuiElement(this.btnPVP);
		this.btnChat = new GuiButtonFixed()
		{
			boundries = new Rect(94f, 296f, 30f, 30f)
		};
		this.btnChat.SetTexture("MainScreenWindow", "chatLog");
		this.btnChat.Caption = string.Empty;
		if (!NetworkScript.player.vessel.isGuest)
		{
			this.btnChat.Clicked = new Action<EventHandlerParam>(null, MainScreenWindow.OnChatClicked);
		}
		else
		{
			this.btnChat.Clicked = new Action<EventHandlerParam>(this, MainScreenWindow.OnWindowBtnClicked);
		}
		this.btnChat.isMuted = true;
		this.btnChat.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.CreateTooltipChat);
		this.btnChat.eventHandlerParam.customData = (!NetworkScript.player.vessel.isGuest ? 12 : 25);
		this.sideMenuWindow.AddGuiElement(this.btnChat);
		this.btnTransformer = new GuiButtonFixed()
		{
			boundries = new Rect(29f, 342f, 30f, 30f)
		};
		this.btnTransformer.SetTexture("MainScreenWindow", "transformer");
		this.btnTransformer.Caption = string.Empty;
		this.btnTransformer.eventHandlerParam.customData = (byte)22;
		this.btnTransformer.Clicked = new Action<EventHandlerParam>(this, MainScreenWindow.OnWindowBtnClicked);
		this.btnTransformer.isMuted = true;
		this.btnTransformer.isEnabled = NetworkScript.player.playerBelongings.playerLevel >= 30;
		this.btnTransformer.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.SmartTooltipTransformer);
		this.sideMenuWindow.AddGuiElement(this.btnTransformer);
		this.btnSendGift = new GuiButtonFixed()
		{
			boundries = new Rect(29f, 384f, 30f, 30f)
		};
		this.btnSendGift.SetTexture("MainScreenWindow", "sendGift");
		this.btnSendGift.Caption = string.Empty;
		this.btnSendGift.eventHandlerParam.customData = (byte)33;
		this.btnSendGift.Clicked = new Action<EventHandlerParam>(this, MainScreenWindow.OnWindowBtnClicked);
		this.btnSendGift.isMuted = true;
		this.btnSendGift.isEnabled = NetworkScript.player.playerBelongings.playerLevel >= 10;
		this.btnSendGift.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.SmartTooltipSendGifts);
		this.sideMenuWindow.AddGuiElement(this.btnSendGift);
		this.btnSystemMenu = new GuiButtonFixed()
		{
			boundries = new Rect(94f, 342f, 30f, 30f)
		};
		this.btnSystemMenu.SetTexture("MainScreenWindow", "system");
		this.btnSystemMenu.Caption = string.Empty;
		this.btnSystemMenu.Clicked = new Action<EventHandlerParam>(this, MainScreenWindow.OnWindowBtnClicked);
		this.btnSystemMenu.isMuted = true;
		this.btnSystemMenu.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.CreateTooltipSystem);
		this.btnSystemMenu.eventHandlerParam.customData = (byte)6;
		this.sideMenuWindow.AddGuiElement(this.btnSystemMenu);
		if (playWebGame.authorization.payments_promotion)
		{
			GuiTexture guiTexture = new GuiTexture();
			guiTexture.SetTexture("MainScreenWindow", "PromoBadge");
			guiTexture.boundries = new Rect(131f, 89f, 38f, 38f);
			this.sideMenuWindow.AddGuiElement(guiTexture);
		}
		AndromedaGui.gui.AddWindow(this.sideMenuWindow);
	}

	private GuiWindow CreateTooltipChat(object parm)
	{
		GuiWindow guiWindow = new GuiWindow()
		{
			zOrder = 230,
			ignoreClickEvents = true
		};
		guiWindow.SetBackgroundTexture("FrameworkGUI", "tooltipLineLeft");
		guiWindow.isHidden = false;
		guiWindow.boundries.set_x(-36f);
		guiWindow.boundries.set_y(296f);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = this.kb.GetCommandTooltip(KeyboardCommand.Chat),
			boundries = new Rect(1f, 9f, 125f, 30f),
			Alignment = 3,
			FontSize = 10,
			Font = GuiLabel.FontBold
		};
		guiWindow.AddGuiElement(guiLabel);
		return guiWindow;
	}

	private GuiWindow CreateTooltipFusion(object parm)
	{
		GuiWindow guiWindow = new GuiWindow()
		{
			zOrder = 230,
			ignoreClickEvents = true
		};
		guiWindow.SetBackgroundTexture("FrameworkGUI", "tooltipLineLeft");
		guiWindow.isHidden = false;
		guiWindow.boundries.set_x(-101f);
		guiWindow.boundries.set_y(205f);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = this.kb.GetCommandTooltip(KeyboardCommand.Fusion),
			boundries = new Rect(1f, 9f, 125f, 30f),
			Alignment = 3,
			FontSize = 10,
			Font = GuiLabel.FontBold
		};
		guiWindow.AddGuiElement(guiLabel);
		return guiWindow;
	}

	private GuiWindow CreateTooltipGuild(object parm)
	{
		GuiWindow guiWindow = new GuiWindow()
		{
			zOrder = 230,
			ignoreClickEvents = true
		};
		guiWindow.SetBackgroundTexture("FrameworkGUI", "tooltipLineLeft");
		guiWindow.isHidden = false;
		guiWindow.boundries.set_x(-101f);
		guiWindow.boundries.set_y(251f);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = this.kb.GetCommandTooltip(KeyboardCommand.Guild),
			boundries = new Rect(1f, 9f, 125f, 30f),
			Alignment = 3,
			FontSize = 10,
			Font = GuiLabel.FontBold
		};
		guiWindow.AddGuiElement(guiLabel);
		return guiWindow;
	}

	private GuiWindow CreateTooltipLevelUp(object parm)
	{
		GuiWindow guiWindow = new GuiWindow()
		{
			zOrder = 230,
			ignoreClickEvents = true
		};
		guiWindow.SetBackgroundTexture("FrameworkGUI", "tooltipLineLeft");
		guiWindow.isHidden = false;
		guiWindow.boundries.set_x(-36f);
		guiWindow.boundries.set_y(160f);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = this.kb.GetCommandTooltip(KeyboardCommand.Skills),
			boundries = new Rect(1f, 9f, 125f, 30f),
			Alignment = 3,
			FontSize = 10,
			Font = GuiLabel.FontBold
		};
		guiWindow.AddGuiElement(guiLabel);
		return guiWindow;
	}

	private GuiWindow CreateTooltipNovaShop(object parm)
	{
		GuiWindow guiWindow = new GuiWindow()
		{
			zOrder = 230,
			ignoreClickEvents = true
		};
		guiWindow.SetBackgroundTexture("FrameworkGUI", "tooltipLineLeft");
		guiWindow.isHidden = false;
		guiWindow.boundries.set_x(-36f);
		guiWindow.boundries.set_y(115f);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = this.kb.GetCommandTooltip(KeyboardCommand.NovaShop),
			boundries = new Rect(1f, 9f, 125f, 30f),
			Alignment = 3,
			FontSize = 10,
			Font = GuiLabel.FontBold
		};
		guiWindow.AddGuiElement(guiLabel);
		return guiWindow;
	}

	private GuiWindow CreateTooltipPlayerProfile(object parm)
	{
		GuiWindow guiWindow = new GuiWindow()
		{
			zOrder = 230,
			ignoreClickEvents = true
		};
		guiWindow.SetBackgroundTexture("FrameworkGUI", "tooltipLineLeft");
		guiWindow.isHidden = false;
		guiWindow.boundries.set_x(-101f);
		guiWindow.boundries.set_y(115f);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = this.kb.GetCommandTooltip(KeyboardCommand.Profile),
			boundries = new Rect(1f, 9f, 125f, 30f),
			Alignment = 3,
			FontSize = 10,
			Font = GuiLabel.FontBold
		};
		guiWindow.AddGuiElement(guiLabel);
		return guiWindow;
	}

	private GuiWindow CreateTooltipPVP(object parm)
	{
		GuiWindow guiWindow = new GuiWindow()
		{
			zOrder = 230,
			ignoreClickEvents = true
		};
		guiWindow.SetBackgroundTexture("FrameworkGUI", "tooltipLineLeft");
		guiWindow.isHidden = false;
		guiWindow.boundries.set_x(-101f);
		guiWindow.boundries.set_y(296f);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = this.kb.GetCommandTooltip(KeyboardCommand.PvP),
			boundries = new Rect(1f, 9f, 125f, 30f),
			Alignment = 3,
			FontSize = 10,
			Font = GuiLabel.FontBold
		};
		guiWindow.AddGuiElement(guiLabel);
		return guiWindow;
	}

	private GuiWindow CreateTooltipRanking(object parm)
	{
		GuiWindow guiWindow = new GuiWindow()
		{
			zOrder = 230,
			ignoreClickEvents = true
		};
		guiWindow.SetBackgroundTexture("FrameworkGUI", "tooltipLineLeft");
		guiWindow.isHidden = false;
		guiWindow.boundries.set_x(-36f);
		guiWindow.boundries.set_y(251f);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = this.kb.GetCommandTooltip(KeyboardCommand.Ranking),
			boundries = new Rect(1f, 9f, 125f, 30f),
			Alignment = 3,
			FontSize = 10,
			Font = GuiLabel.FontBold
		};
		guiWindow.AddGuiElement(guiLabel);
		return guiWindow;
	}

	private GuiWindow CreateTooltipSendGifts(object parm)
	{
		GuiWindow guiWindow = new GuiWindow()
		{
			zOrder = 230,
			ignoreClickEvents = true
		};
		guiWindow.SetBackgroundTexture("FrameworkGUI", "tooltipLineLeft");
		guiWindow.isHidden = false;
		guiWindow.boundries.set_x(-101f);
		guiWindow.boundries.set_y(385f);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = this.kb.GetCommandTooltip(KeyboardCommand.Gifts),
			boundries = new Rect(1f, 9f, 125f, 30f),
			Alignment = 3,
			FontSize = 10,
			Font = GuiLabel.FontBold
		};
		guiWindow.AddGuiElement(guiLabel);
		return guiWindow;
	}

	private GuiWindow CreateTooltipShipConfig(object parm)
	{
		GuiWindow guiWindow = new GuiWindow()
		{
			zOrder = 230,
			ignoreClickEvents = true
		};
		guiWindow.SetBackgroundTexture("FrameworkGUI", "tooltipLineLeft");
		guiWindow.isHidden = false;
		guiWindow.boundries.set_x(-101f);
		guiWindow.boundries.set_y(160f);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = this.kb.GetCommandTooltip(KeyboardCommand.ShipConfig),
			boundries = new Rect(1f, 9f, 125f, 30f),
			Alignment = 3,
			FontSize = 10,
			Font = GuiLabel.FontBold
		};
		guiWindow.AddGuiElement(guiLabel);
		return guiWindow;
	}

	private GuiWindow CreateTooltipSystem(object parm)
	{
		GuiWindow guiWindow = new GuiWindow()
		{
			zOrder = 230,
			ignoreClickEvents = true
		};
		guiWindow.SetBackgroundTexture("FrameworkGUI", "tooltipLineLeft");
		guiWindow.isHidden = false;
		guiWindow.boundries.set_x(-36f);
		guiWindow.boundries.set_y(342f);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = StaticData.Translate("key_main_screen_btn_system"),
			boundries = new Rect(1f, 9f, 125f, 30f),
			Alignment = 3,
			FontSize = 10,
			Font = GuiLabel.FontBold
		};
		guiWindow.AddGuiElement(guiLabel);
		return guiWindow;
	}

	private GuiWindow CreateTooltipTransformer(object parm)
	{
		GuiWindow guiWindow = new GuiWindow()
		{
			zOrder = 230,
			ignoreClickEvents = true
		};
		guiWindow.SetBackgroundTexture("FrameworkGUI", "tooltipLineLeft");
		guiWindow.isHidden = false;
		guiWindow.boundries.set_x(-101f);
		guiWindow.boundries.set_y(342f);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = this.kb.GetCommandTooltip(KeyboardCommand.Transformer),
			boundries = new Rect(1f, 9f, 125f, 30f),
			Alignment = 3,
			FontSize = 10,
			Font = GuiLabel.FontBold
		};
		guiWindow.AddGuiElement(guiLabel);
		return guiWindow;
	}

	private GuiWindow CreateTooltipUniverseMap(object parm)
	{
		GuiWindow guiWindow = new GuiWindow()
		{
			zOrder = 230,
			ignoreClickEvents = true
		};
		guiWindow.SetBackgroundTexture("FrameworkGUI", "tooltipLineLeft");
		guiWindow.isHidden = false;
		guiWindow.boundries.set_x(-36f);
		guiWindow.boundries.set_y(205f);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = this.kb.GetCommandTooltip(KeyboardCommand.UniverseMap),
			boundries = new Rect(1f, 9f, 125f, 30f),
			Alignment = 3,
			FontSize = 10,
			Font = GuiLabel.FontBold
		};
		guiWindow.AddGuiElement(guiLabel);
		return guiWindow;
	}

	private void CreateTopGUIWindow()
	{
		this.playerStatsWindow = new GuiWindow();
		this.playerStatsWindow.SetBackgroundTexture("MainScreenWindow", "topGUI");
		this.playerStatsWindow.boundries = new Rect((float)(Screen.get_width() / 2 - 238), 0f, 476f, 42f);
		this.playerStatsWindow.zOrder = 228;
		float single = -4f;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("FrameworkGUI", "res_nova");
		guiTexture.X = 23f;
		guiTexture.Y = 11f + single;
		this.playerStatsWindow.AddGuiElement(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("FrameworkGUI", "res_cash");
		guiTexture1.X = 112f;
		guiTexture1.Y = 11f + single;
		this.playerStatsWindow.AddGuiElement(guiTexture1);
		GuiTexture guiTexture2 = new GuiTexture();
		guiTexture2.SetTexture("FrameworkGUI", "res_equilibrium");
		guiTexture2.X = 275f;
		guiTexture2.Y = 12f + single;
		this.playerStatsWindow.AddGuiElement(guiTexture2);
		GuiTexture guiTexture3 = new GuiTexture();
		guiTexture3.SetTexture("FrameworkGUI", "res_ultralibrium");
		guiTexture3.X = 370f;
		guiTexture3.Y = 14f + single;
		this.playerStatsWindow.AddGuiElement(guiTexture3);
		string str = (NetworkScript.player.vessel.fractionId != 1 ? "fraction2Icon" : "fraction1Icon");
		GuiTexture guiTexture4 = new GuiTexture();
		guiTexture4.SetTexture("FrameworkGUI", str);
		guiTexture4.X = 225f;
		guiTexture4.Y = 12f + single;
		this.playerStatsWindow.AddGuiElement(guiTexture4);
		this.novaItemTracker = new GuiItemTracker(PlayerItems.TypeNova)
		{
			boundries = new Rect(45f, 14f + single, 70f, 14f),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			SetColor = GuiNewStyleBar.orangeColor
		};
		this.playerStatsWindow.AddGuiElement(this.novaItemTracker);
		this.cashItemTracker = new GuiItemTracker(PlayerItems.TypeCash)
		{
			boundries = new Rect(132f, 14f + single, 80f, 14f),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			SetColor = GuiNewStyleBar.blueColor
		};
		this.playerStatsWindow.AddGuiElement(this.cashItemTracker);
		this.viralItemTracker = new GuiItemTracker(PlayerItems.TypeEquilibrium)
		{
			boundries = new Rect(295f, 14f + single, 65f, 14f),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			SetColor = GuiNewStyleBar.purpleColor
		};
		this.playerStatsWindow.AddGuiElement(this.viralItemTracker);
		this.ultralibriumItemTracker = new GuiItemTracker(PlayerItems.TypeUltralibrium)
		{
			boundries = new Rect(390f, 14f + single, 80f, 14f),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			SetColor = GuiNewStyleBar.greenColor
		};
		this.playerStatsWindow.AddGuiElement(this.ultralibriumItemTracker);
		if (NetworkScript.player.vessel.galaxy.get_galaxyId() != 1000)
		{
			GuiButtonFixed guiButtonFixed = new GuiButtonFixed()
			{
				Caption = string.Empty
			};
			guiButtonFixed.SetTexture("FrameworkGUI", "empty");
			guiButtonFixed.eventHandlerParam.customData = (byte)11;
			guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, MainScreenWindow.OnWindowBtnClicked);
			guiButtonFixed.boundries = new Rect(0f, 0f, this.playerStatsWindow.boundries.get_width(), this.playerStatsWindow.boundries.get_height());
			this.playerStatsWindow.AddGuiElement(guiButtonFixed);
		}
		this.playerStatsWindow.isHidden = false;
		AndromedaGui.gui.AddWindow(this.playerStatsWindow);
	}

	private void DrawActiveBoosters()
	{
		int num;
		this.boosterSlotUsage = new bool[4];
		for (int i = 0; i < 4; i++)
		{
			this.boosterSlotUsage[i] = false;
		}
		if (NetworkScript.player.cfg.cargoBooster)
		{
			num = this.FirstFreeBoosterSlot();
			GuiTexture guiTexture = new GuiTexture();
			guiTexture.boundries.set_x(3f + this.booster_slots[num].X);
			guiTexture.boundries.set_y(3f);
			guiTexture.SetTexture("boosterIco", "BoosterIonCompressor");
			guiTexture.boundries.set_width(32f);
			guiTexture.boundries.set_height(32f);
			guiTexture.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.DrawActiveBoostersTooltip);
			guiTexture.tooltipWindowParam = PlayerItems.TypeBoosterCargoFor1Days;
			this.quickSlotsWindow.AddGuiElement(guiTexture);
			this.boosterSlotUsage[num] = true;
			this.forDelete.Add(guiTexture);
		}
		if (NetworkScript.player.cfg.damageBooster)
		{
			num = this.FirstFreeBoosterSlot();
			GuiTexture drawTooltipWindow = new GuiTexture();
			drawTooltipWindow.boundries.set_x(3f + this.booster_slots[num].X);
			drawTooltipWindow.boundries.set_y(3f);
			drawTooltipWindow.SetTexture("boosterIco", "BoosterWeaponEnhancer");
			drawTooltipWindow.boundries.set_width(32f);
			drawTooltipWindow.boundries.set_height(32f);
			drawTooltipWindow.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.DrawActiveBoostersTooltip);
			drawTooltipWindow.tooltipWindowParam = PlayerItems.TypeBoosterDamageFor1Days;
			this.quickSlotsWindow.AddGuiElement(drawTooltipWindow);
			this.boosterSlotUsage[num] = true;
			this.forDelete.Add(drawTooltipWindow);
		}
		if (NetworkScript.player.cfg.miningBooster)
		{
			num = this.FirstFreeBoosterSlot();
			GuiTexture typeBoosterAutominerFor1Days = new GuiTexture();
			typeBoosterAutominerFor1Days.boundries.set_x(3f + this.booster_slots[num].X);
			typeBoosterAutominerFor1Days.boundries.set_y(3f);
			typeBoosterAutominerFor1Days.SetTexture("boosterIco", "BoosterIonGatherer");
			typeBoosterAutominerFor1Days.boundries.set_width(32f);
			typeBoosterAutominerFor1Days.boundries.set_height(32f);
			typeBoosterAutominerFor1Days.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.DrawActiveBoostersTooltip);
			typeBoosterAutominerFor1Days.tooltipWindowParam = PlayerItems.TypeBoosterAutominerFor1Days;
			this.quickSlotsWindow.AddGuiElement(typeBoosterAutominerFor1Days);
			this.boosterSlotUsage[num] = true;
			this.forDelete.Add(typeBoosterAutominerFor1Days);
		}
		if (NetworkScript.player.cfg.experienceBooster)
		{
			num = this.FirstFreeBoosterSlot();
			GuiTexture typeBoosterExperienceFor1Days = new GuiTexture();
			typeBoosterExperienceFor1Days.boundries.set_x(3f + this.booster_slots[num].X);
			typeBoosterExperienceFor1Days.boundries.set_y(3f);
			typeBoosterExperienceFor1Days.SetTexture("boosterIco", "BoosterBarterBooster");
			typeBoosterExperienceFor1Days.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.DrawActiveBoostersTooltip);
			typeBoosterExperienceFor1Days.tooltipWindowParam = PlayerItems.TypeBoosterExperienceFor1Days;
			typeBoosterExperienceFor1Days.boundries.set_width(32f);
			typeBoosterExperienceFor1Days.boundries.set_height(32f);
			this.quickSlotsWindow.AddGuiElement(typeBoosterExperienceFor1Days);
			this.boosterSlotUsage[num] = true;
			this.forDelete.Add(typeBoosterExperienceFor1Days);
		}
	}

	private GuiWindow DrawActiveBoostersTooltip(object prm)
	{
		ushort num = (ushort)prm;
		GuiWindow guiWindow = new GuiWindow()
		{
			zOrder = 230,
			ignoreClickEvents = true
		};
		guiWindow.SetBackgroundTexture("ConfigWindow", "boostersTooltip");
		guiWindow.isHidden = false;
		guiWindow.boundries.set_x(302f);
		guiWindow.boundries.set_y(-45f);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = StaticData.Translate(StaticData.allTypes.get_Item(num).uiName).ToUpper(),
			boundries = new Rect(10f, 4f, 130f, 12f),
			FontSize = 10,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 4
		};
		guiWindow.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			text = StaticData.Translate(StaticData.allTypes.get_Item(num).description),
			boundries = new Rect(10f, 18f, 130f, 50f),
			FontSize = 10,
			TextColor = GuiNewStyleBar.blueColor
		};
		guiWindow.AddGuiElement(guiLabel1);
		return guiWindow;
	}

	private void DrawAttachedWeapons()
	{
		for (int i = 0; i < 6; i++)
		{
			if (NetworkScript.player.cfg.weaponSlots[i].get_WeaponStatus() > 1)
			{
				int num = NetworkScript.player.cfg.weaponSlots[i].weaponTierId;
				WeaponsTypeNet item = (WeaponsTypeNet)StaticData.allTypes.get_Item((ushort)num);
				GuiTexture guiTexture = new GuiTexture();
				guiTexture.boundries.set_x((float)(50 + i * 34));
				guiTexture.boundries.set_y(8f);
				guiTexture.SetItemTexture(item.itemType);
				guiTexture.boundries.set_width(30f);
				guiTexture.boundries.set_height(20.5f);
				if (NetworkScript.player.cfg.weaponSlots[i].isActive)
				{
					this.weapon_slots_inactive[i].SetTexture("FrameworkGUI", "empty");
					if (this.quickSlotsWindow.HasGuiElement(this.weapon_slots_inactive[i]))
					{
						this.quickSlotsWindow.RemoveGuiElement(this.weapon_slots_inactive[i]);
					}
					this.quickSlotsWindow.AddGuiElement(guiTexture);
					this.forDelete.Add(guiTexture);
				}
				else
				{
					this.quickSlotsWindow.AddGuiElement(guiTexture);
					this.forDelete.Add(guiTexture);
					this.weapon_slots_inactive[i].SetTexture("MainScreenWindow", "weapons_slot_inactiveBG");
					if (this.quickSlotsWindow.HasGuiElement(this.weapon_slots_inactive[i]))
					{
						this.quickSlotsWindow.RemoveGuiElement(this.weapon_slots_inactive[i]);
					}
					this.quickSlotsWindow.AddGuiElement(this.weapon_slots_inactive[i]);
				}
				GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
				guiButtonFixed.SetTexture("FrameworkGUI", "empty");
				guiButtonFixed.boundries = this.weapon_slots[i].boundries;
				guiButtonFixed.Caption = string.Empty;
				guiButtonFixed.eventHandlerParam.customData = i;
				guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, MainScreenWindow.TurningOnOffWeapon);
				guiButtonFixed.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.DrawAttachedWeaponTooltip);
				guiButtonFixed.tooltipWindowParam = new WeaponTooltipInfo(new Vector2(guiTexture.boundries.get_x(), guiTexture.boundries.get_y()), item, NetworkScript.player.cfg.weaponSlots[i]);
				this.quickSlotsWindow.AddGuiElement(guiButtonFixed);
				this.forDelete.Add(guiButtonFixed);
			}
		}
		this.RefreshOutOfAmmo();
	}

	private GuiWindow DrawAttachedWeaponTooltip(object parm)
	{
		string str;
		Color color;
		MainScreenWindow.<DrawAttachedWeaponTooltip>c__AnonStorey37 variable = null;
		WeaponTooltipInfo weaponTooltipInfo = (WeaponTooltipInfo)parm;
		WeaponsTypeNet weaponsTypeNet = weaponTooltipInfo.weapon;
		WeaponSlot weaponSlot = weaponTooltipInfo.weaponSlot;
		AmmoNet item = (AmmoNet)StaticData.allTypes.get_Item(weaponSlot.selectedAmmoItemType);
		int num = 0;
		int num1 = NetworkScript.player.playerBelongings.selectedShipId;
		ItemLocation itemLocation = 1;
		SlotItem slotItem = null;
		Vector2 vector2 = weaponTooltipInfo.position;
		if (weaponsTypeNet.itemType >= PlayerItems.TypeWeaponLaserTire1 && weaponsTypeNet.itemType <= PlayerItems.TypeWeaponLaserTire5)
		{
			itemLocation = 6;
		}
		if (weaponsTypeNet.itemType >= PlayerItems.TypeWeaponPlasmaTire1 && weaponsTypeNet.itemType <= PlayerItems.TypeWeaponPlasmaTire5)
		{
			itemLocation = 7;
		}
		if (weaponsTypeNet.itemType >= PlayerItems.TypeWeaponIonTire1 && weaponsTypeNet.itemType <= PlayerItems.TypeWeaponIonTire5)
		{
			itemLocation = 8;
		}
		slotItem = Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(NetworkScript.player.playerBelongings.playerItems.slotItems, new Func<SlotItem, bool>(variable, (SlotItem t) => (t.get_ShipId() != this.playerShipId || t.get_SlotType() != (byte)this.weaponSlotType ? false : t.get_Slot() == this.weaponSlot.slotId))));
		GuiWindow guiWindow = new GuiWindow();
		guiWindow.SetBackgroundTexture("MainScreenWindow", "WeaponTooltipInspace");
		guiWindow.boundries.set_x(vector2.x);
		guiWindow.boundries.set_y(vector2.y - guiWindow.boundries.get_height());
		guiWindow.isHidden = false;
		if (slotItem == null)
		{
			Debug.LogWarning(string.Format("DrawAttachedWeaponTooltip item == null t.ShipId:{0} t.SlotType:{1} t.Slot:{2}", num1, itemLocation, weaponSlot.slotId));
			return guiWindow;
		}
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetItemTexture(weaponsTypeNet.itemType);
		guiTexture.boundries = new Rect(9f, 47f, 46f, 31.56f);
		guiWindow.AddGuiElement(guiTexture);
		Inventory.ItemRarity(slotItem, out str, out color);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = str,
			TextColor = color,
			boundries = new Rect(10f, 2f, 265f, 24f),
			Alignment = 4,
			FontSize = 11,
			Font = GuiLabel.FontBold
		};
		guiWindow.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			text = StaticData.Translate("key_main_screen_wp_tooltip_status"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(0f, 26f, 135f, 12f),
			Alignment = 5
		};
		guiWindow.AddGuiElement(guiLabel1);
		string empty = string.Empty;
		Color _grey = GuiNewStyleBar.greenColor;
		switch (weaponSlot.get_WeaponStatus())
		{
			case 2:
			{
				empty = StaticData.Translate("key_main_screen_weapon_off");
				_grey = Color.get_grey();
				break;
			}
			case 3:
			{
				empty = StaticData.Translate("key_main_screen_weapon_active");
				_grey = GuiNewStyleBar.greenColor;
				break;
			}
			case 4:
			{
				empty = StaticData.Translate("key_main_screen_weapon_reloading");
				_grey = GuiNewStyleBar.orangeColor;
				break;
			}
		}
		GuiLabel guiLabel2 = new GuiLabel()
		{
			text = string.Concat(" ", empty),
			TextColor = _grey,
			boundries = new Rect(135f, 26f, 150f, 12f),
			Alignment = 3
		};
		guiWindow.AddGuiElement(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			text = StaticData.Translate("key_main_screen_wp_tooltip_damage"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(60f, 40f, 70f, 10f),
			Alignment = 3
		};
		guiWindow.AddGuiElement(guiLabel3);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			text = StaticData.Translate("key_main_screen_wp_tooltip_targeting"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(60f, 50f, 70f, 10f),
			Alignment = 3
		};
		guiWindow.AddGuiElement(guiLabel4);
		GuiLabel guiLabel5 = new GuiLabel()
		{
			text = StaticData.Translate("key_main_screen_wp_tooltip_cooldown"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(60f, 60f, 70f, 10f),
			Alignment = 3
		};
		guiWindow.AddGuiElement(guiLabel5);
		GuiLabel guiLabel6 = new GuiLabel()
		{
			text = StaticData.Translate("key_main_screen_wp_tooltip_range"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(60f, 70f, 70f, 10f),
			Alignment = 3
		};
		guiWindow.AddGuiElement(guiLabel6);
		GuiLabel guiLabel7 = new GuiLabel()
		{
			text = StaticData.Translate("key_main_screen_wp_tooltip_upgrade"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(60f, 80f, 70f, 10f),
			Alignment = 3
		};
		guiWindow.AddGuiElement(guiLabel7);
		float single = 0f;
		int num2 = 0;
		int num3 = 0;
		single = (!NetworkScript.player.cfg.damageBooster ? (float)weaponSlot.skillDamage * (1f + NetworkScript.player.cfg.dmgPercentForAlien / 100f) * ((float)((AmmoNet)StaticData.allTypes.get_Item(weaponSlot.selectedAmmoItemType)).damage / 100f) : (float)weaponSlot.skillDamage * (1f + NetworkScript.player.cfg.dmgPercentForAlien / 100f) * ((float)((AmmoNet)StaticData.allTypes.get_Item(weaponSlot.selectedAmmoItemType)).damage / 100f) * 1.3f);
		num2 = (int)(single * (float)weaponSlot.weaponPenetration / 100f);
		num3 = (int)(single - single * (float)weaponSlot.weaponPenetration / 100f);
		GuiLabel guiLabel8 = new GuiLabel()
		{
			text = num3.ToString(),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(135f, 40f, 70f, 10f),
			Alignment = 3
		};
		guiWindow.AddGuiElement(guiLabel8);
		GuiLabel guiLabel9 = new GuiLabel()
		{
			text = "/",
			boundries = new Rect(135f + guiLabel8.TextWidth, 40f, 70f, 10f),
			Alignment = 3
		};
		guiWindow.AddGuiElement(guiLabel9);
		GuiLabel guiLabel10 = new GuiLabel()
		{
			text = num2.ToString(),
			TextColor = GuiNewStyleBar.orangeColor,
			boundries = new Rect(guiLabel9.boundries.get_x() + guiLabel9.TextWidth, 40f, 70f, 10f),
			Alignment = 3
		};
		guiWindow.AddGuiElement(guiLabel10);
		int num4 = 0;
		if (weaponsTypeNet.itemType == PlayerItems.TypeWeaponLaserTire1 || weaponsTypeNet.itemType == PlayerItems.TypeWeaponLaserTire2 || weaponsTypeNet.itemType == PlayerItems.TypeWeaponLaserTire3 || weaponsTypeNet.itemType == PlayerItems.TypeWeaponLaserTire4 || weaponsTypeNet.itemType == PlayerItems.TypeWeaponLaserTire5)
		{
			num4 = NetworkScript.player.cfg.targetingForLaser;
		}
		else if (weaponsTypeNet.itemType == PlayerItems.TypeWeaponPlasmaTire1 || weaponsTypeNet.itemType == PlayerItems.TypeWeaponPlasmaTire2 || weaponsTypeNet.itemType == PlayerItems.TypeWeaponPlasmaTire3 || weaponsTypeNet.itemType == PlayerItems.TypeWeaponPlasmaTire4 || weaponsTypeNet.itemType == PlayerItems.TypeWeaponPlasmaTire5)
		{
			num4 = NetworkScript.player.cfg.targetingForPlasma;
		}
		else if (weaponsTypeNet.itemType == PlayerItems.TypeWeaponIonTire1 || weaponsTypeNet.itemType == PlayerItems.TypeWeaponIonTire2 || weaponsTypeNet.itemType == PlayerItems.TypeWeaponIonTire3 || weaponsTypeNet.itemType == PlayerItems.TypeWeaponIonTire4 || weaponsTypeNet.itemType == PlayerItems.TypeWeaponIonTire5)
		{
			num4 = NetworkScript.player.cfg.targetingForIon;
		}
		GuiLabel guiLabel11 = new GuiLabel()
		{
			text = num4.ToString(),
			boundries = new Rect(135f, 50f, 70f, 10f),
			Alignment = 3
		};
		guiWindow.AddGuiElement(guiLabel11);
		GuiLabel guiLabel12 = new GuiLabel()
		{
			text = string.Format(StaticData.Translate("key_main_screen_wp_tooltip_sec"), Math.Max((float)((double)weaponSlot.realReloadTime / 10000000), 0.5f)),
			boundries = new Rect(135f, 60f, 70f, 10f),
			Alignment = 3
		};
		guiWindow.AddGuiElement(guiLabel12);
		GuiLabel guiLabel13 = new GuiLabel()
		{
			text = weaponSlot.totalShootRange.ToString(),
			boundries = new Rect(135f, 70f, 70f, 10f),
			Alignment = 3
		};
		guiWindow.AddGuiElement(guiLabel13);
		GuiLabel guiLabel14 = new GuiLabel()
		{
			text = num.ToString(),
			boundries = new Rect(135f, 80f, 70f, 10f),
			Alignment = 3
		};
		guiWindow.AddGuiElement(guiLabel14);
		GuiDualColorBar guiDualColorBar = new GuiDualColorBar((float)weaponSlot.totalDamageShield, (float)(weaponSlot.totalDamageShield + weaponSlot.totalDamageHull), 820f, GuiNewStyleBar.blueColor, GuiNewStyleBar.orangeColor)
		{
			value1 = (float)num3,
			value2 = (float)(num3 + num2),
			Width = 70f,
			maximum = 820f,
			X = 205f,
			Y = 40f
		};
		guiWindow.AddGuiElement(guiDualColorBar);
		GuiNewStyleBar guiNewStyleBar = new GuiNewStyleBar()
		{
			X = 205f,
			Y = 50f
		};
		guiNewStyleBar.SetCustumSize(70, Color.get_white());
		guiNewStyleBar.current = weaponSlot.totalTargeting;
		guiNewStyleBar.maximum = 60f;
		guiWindow.AddGuiElement(guiNewStyleBar);
		GuiNewStyleBar guiNewStyleBar1 = new GuiNewStyleBar()
		{
			X = 205f,
			Y = 60f
		};
		guiNewStyleBar1.SetCustumSize(70, Color.get_white());
		guiNewStyleBar1.current = 3.5f - (float)((double)weaponSlot.realReloadTime / 10000000);
		guiNewStyleBar1.maximum = 3f;
		guiWindow.AddGuiElement(guiNewStyleBar1);
		GuiNewStyleBar guiNewStyleBar2 = new GuiNewStyleBar()
		{
			X = 205f,
			Y = 70f
		};
		guiNewStyleBar2.SetCustumSize(70, Color.get_white());
		guiNewStyleBar2.current = weaponSlot.totalShootRange;
		guiNewStyleBar2.maximum = 28f;
		guiWindow.AddGuiElement(guiNewStyleBar2);
		GuiNewStyleBar guiNewStyleBar3 = new GuiNewStyleBar()
		{
			X = 205f,
			Y = 80f
		};
		guiNewStyleBar3.SetCustumSize(70, Color.get_white());
		guiNewStyleBar3.current = (float)num;
		guiNewStyleBar3.maximum = 15f;
		guiWindow.AddGuiElement(guiNewStyleBar3);
		GuiLabel guiLabel15 = new GuiLabel()
		{
			text = StaticData.Translate(item.uiName),
			TextColor = GuiNewStyleBar.orangeColor,
			boundries = new Rect(0f, 96f, 285f, 16f),
			Alignment = 4,
			FontSize = 12
		};
		guiWindow.AddGuiElement(guiLabel15);
		GuiLabel guiLabel16 = new GuiLabel()
		{
			text = StaticData.Translate("key_main_screen_wp_tooltip_damage"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(60f, 120f, 70f, 10f),
			Alignment = 3
		};
		guiWindow.AddGuiElement(guiLabel16);
		GuiLabel guiLabel17 = new GuiLabel()
		{
			text = StaticData.Translate("key_main_screen_wp_tooltip_speciality"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(60f, 140f, 70f, 10f),
			Alignment = 3
		};
		guiWindow.AddGuiElement(guiLabel17);
		GuiLabel guiLabel18 = new GuiLabel()
		{
			text = string.Concat(item.damage.ToString(), "%"),
			boundries = new Rect(135f, 120f, 70f, 10f),
			Alignment = 3
		};
		guiWindow.AddGuiElement(guiLabel18);
		GuiLabel guiLabel19 = new GuiLabel()
		{
			text = StaticData.Translate("key_main_screen_wp_tooltip_none"),
			boundries = new Rect(135f, 140f, 70f, 10f),
			Alignment = 3
		};
		guiWindow.AddGuiElement(guiLabel19);
		GuiNewStyleBar guiNewStyleBar4 = new GuiNewStyleBar()
		{
			X = 205f,
			Y = 120f
		};
		guiNewStyleBar4.SetCustumSize(70, Color.get_white());
		guiNewStyleBar4.current = (float)item.damage;
		guiNewStyleBar4.maximum = 120f;
		guiWindow.AddGuiElement(guiNewStyleBar4);
		GuiTexture rect = new GuiTexture();
		rect.SetItemTexture(item.itemType);
		rect.boundries = new Rect(9f, 120f, 46f, 31.56f);
		guiWindow.AddGuiElement(rect);
		return guiWindow;
	}

	private GuiWindow DrawExperienceBarTooltip(object prm)
	{
		int num = NetworkScript.player.cfg.playerLevel;
		long amountAt = NetworkScript.player.playerBelongings.playerItems.GetAmountAt(13);
		long item = amountAt;
		if (StaticData.levelsType.ContainsKey(num + 1))
		{
			item = StaticData.levelsType.get_Item(num + 1).tottalExp;
		}
		long item1 = StaticData.levelsType.get_Item(NetworkScript.player.cfg.playerLevel).tottalExp;
		float single = 0f;
		if (amountAt < item)
		{
			single = (float)(amountAt - item1) / (float)(item - item1);
		}
		single = single * 100f;
		GuiWindow guiWindow = new GuiWindow()
		{
			zOrder = 230,
			ignoreClickEvents = true
		};
		guiWindow.SetBackgroundTexture("MainScreenWindow", "XpTooltipInSpace");
		guiWindow.isHidden = false;
		guiWindow.boundries.set_x(50f);
		guiWindow.boundries.set_y(-28f);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = string.Format(StaticData.Translate("key_main_screen_exp_bar1"), NetworkScript.player.cfg.playerLevel).ToUpper(),
			boundries = new Rect(142f, 0f, 110f, 20f),
			FontSize = 13,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 7
		};
		guiWindow.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			text = string.Format(StaticData.Translate("key_main_screen_exp_bar2"), amountAt, Mathf.FloorToInt(single)),
			boundries = new Rect(10f, 20f, 370f, 20f),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 4
		};
		guiWindow.AddGuiElement(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			text = string.Format(StaticData.Translate("key_main_screen_exp_bar3"), item),
			boundries = new Rect(10f, 40f, 370f, 20f),
			FontSize = 12,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 4
		};
		guiWindow.AddGuiElement(guiLabel2);
		return guiWindow;
	}

	public void DrawPendingRewardsNotification()
	{
		if (this.sideMenuWindow == null)
		{
			return;
		}
		if (NetworkScript.player == null || NetworkScript.player.playerBelongings == null)
		{
			return;
		}
		if (NetworkScript.player.vessel.pvpState == 3)
		{
			return;
		}
		if (NetworkScript.player.vessel != null && NetworkScript.player.vessel.galaxy != null && NetworkScript.player.vessel.galaxy.get_galaxyId() == 1000)
		{
			return;
		}
		this.sideMenuWindow.RemoveGuiElement(this.notificationBg);
		this.sideMenuWindow.RemoveGuiElement(this.notificationCounter);
		if (Enumerable.Count<KeyValuePair<int, PlayerPendingAward>>(NetworkScript.player.playerBelongings.playerAwards) <= 0)
		{
			return;
		}
		this.sideMenuWindow.RemoveGuiElement(this.notificationBg);
		this.sideMenuWindow.RemoveGuiElement(this.notificationCounter);
		this.notificationBg = new GuiTexture();
		this.notificationBg.SetTexture("MainScreenWindow", "notification-menu");
		this.notificationBg.X = 43f;
		this.notificationBg.Y = 102f;
		this.sideMenuWindow.AddGuiElement(this.notificationBg);
		this.notificationCounter = new GuiLabel()
		{
			boundries = new Rect(44f, 103f, 26f, 14f),
			Alignment = 4,
			TextColor = GuiNewStyleBar.aquamarineColor,
			FontSize = 10
		};
		GuiLabel str = this.notificationCounter;
		int num = Enumerable.Count<KeyValuePair<int, PlayerPendingAward>>(NetworkScript.player.playerBelongings.playerAwards);
		str.text = num.ToString("N0");
		this.sideMenuWindow.AddGuiElement(this.notificationCounter);
	}

	private void DrawSkillInBar(int skillId, int position)
	{
		MainScreenWindow.<DrawSkillInBar>c__AnonStorey36 variable = null;
		if (position > 8)
		{
			return;
		}
		TalentsInfo talentsInfo = (TalentsInfo)Enumerable.First<PlayerItemTypesData>(Enumerable.Where<PlayerItemTypesData>(StaticData.allTypes.get_Values(), new Func<PlayerItemTypesData, bool>(variable, (PlayerItemTypesData t) => t.itemType == this.skillId)));
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("Skills", talentsInfo.assetName);
		guiTexture.boundries = new Rect((float)(68 + position * 40), 54f, 36f, 36f);
		this.quickSlotsWindow.AddGuiElement(guiTexture);
		string empty = string.Empty;
		switch (this.GetTalentType(talentsInfo))
		{
			case 0:
			{
				empty = "SkillsTooltipRedTriangleSmall";
				break;
			}
			case 1:
			{
				empty = "SkillsTooltipBlueTriangleSmall";
				break;
			}
			case 2:
			{
				empty = "SkillsTooltipGreenTriangleSmall";
				break;
			}
			default:
			{
				empty = "SkillsTooltipRedTriangleSmall";
				break;
			}
		}
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("NewGUI", empty);
		rect.boundries = new Rect((float)(98 + position * 40), 54f, 7f, 7f);
		this.quickSlotsWindow.AddGuiElement(rect);
		GuiButton guiButton = new GuiButton()
		{
			boundries = new Rect((float)(68 + position * 40), 54f, 36f, 36f),
			Caption = string.Empty,
			FontSize = 10
		};
		guiButton.eventHandlerParam.customData = position;
		guiButton.Clicked = new Action<EventHandlerParam>(this, MainScreenWindow.CastSkillFromSlot);
		guiButton.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.DrawTalentTooltip);
		guiButton.tooltipWindowParam = new TalentTooltipInfo(new Vector2((float)(68 + position * 40), 54f), talentsInfo);
		this.quickSlotsWindow.AddGuiElement(guiButton);
	}

	private GuiWindow DrawTalentTooltip(object parm)
	{
		TalentTooltipInfo talentTooltipInfo = (TalentTooltipInfo)parm;
		GuiWindow guiWindow = new GuiWindow();
		guiWindow.SetBackgroundTexture("SystemWindow", "SkillTooltipInspace");
		guiWindow.isHidden = false;
		guiWindow.boundries.set_x(talentTooltipInfo.position.x);
		guiWindow.boundries.set_y(talentTooltipInfo.position.y - 100f);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = StaticData.Translate(talentTooltipInfo.talent.uiName),
			boundries = new Rect(0f, 0f, 190f, 20f),
			FontSize = 12,
			Alignment = 4,
			TextColor = GuiNewStyleBar.orangeColor
		};
		guiWindow.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			text = MainScreenWindow.GetActiveSkillTooltip(talentTooltipInfo.talent.itemType),
			boundries = new Rect(4f, 24f, 182f, 60f),
			Alignment = 0
		};
		guiWindow.AddGuiElement(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(0f, 78f, 175f, 15f),
			Alignment = 8,
			text = string.Format(StaticData.Translate("key_main_screen_skill_tooltip3"), Mathf.FloorToInt((float)NetworkScript.player.playerBelongings.playerItems.GetSkillCooldown(talentTooltipInfo.talent.itemType) * 0.001f)),
			TextColor = GuiNewStyleBar.blueColor,
			Italic = true,
			FontSize = 11
		};
		guiWindow.AddGuiElement(guiLabel2);
		GuiTexture guiTexture = new GuiTexture();
		string empty = string.Empty;
		switch (this.GetTalentType(talentTooltipInfo.talent))
		{
			case 0:
			{
				empty = "SkillsTooltipRedTriangle";
				break;
			}
			case 1:
			{
				empty = "SkillsTooltipBlueTriangle";
				break;
			}
			case 2:
			{
				empty = "SkillsTooltipGreenTriangle";
				break;
			}
			default:
			{
				empty = "SkillsTooltipRedTriangle";
				break;
			}
		}
		guiTexture.SetTexture("NewGUI", empty);
		guiTexture.boundries = new Rect(170f, 0f, 18f, 18f);
		guiWindow.AddGuiElement(guiTexture);
		return guiWindow;
	}

	private void FinishOwnerChange(GuiTextureAnimated animation)
	{
		this.pvpDominationScoreWindow.RemoveGuiElement(animation);
	}

	private void FinishReloading(GuiTextureAnimated tx)
	{
		if (tx != null)
		{
			this.quickSlotsWindow.RemoveGuiElement(tx);
		}
		if (this.isCriticalIndicationOnScreen)
		{
			this.RemoveCriticalIndication();
			this.ShowCriticalIndication();
		}
	}

	private int FirstFreeBoosterSlot()
	{
		for (int i = 0; i < 4; i++)
		{
			if (!this.boosterSlotUsage[i])
			{
				return i;
			}
		}
		return -1;
	}

	public static string GetActiveSkillTooltip(ushort skill)
	{
		int num = 0;
		int num1 = 0;
		if (skill == PlayerItems.TypeCouncilSkillDisarm || skill == PlayerItems.TypeCouncilSkillSacrifice || skill == PlayerItems.TypeCouncilSkillLifesteal)
		{
			TalentsInfo item = (TalentsInfo)StaticData.allTypes.get_Item(skill);
			int levelEfX = PlayerItems.skillsInformation.get_Item(skill).levelEf_X;
			int levelEfY = PlayerItems.skillsInformation.get_Item(skill).levelEf_Y;
			return string.Format(StaticData.Translate(item.description), levelEfX, levelEfY, item.range);
		}
		NetworkScript.player.playerBelongings.playerItems.GetSkillEffect(skill, ref num, ref num1);
		int num2 = NetworkScript.player.cfg.skillDamage;
		string empty = string.Empty;
		if (skill == PlayerItems.TypeTalentsRepairingDrones || skill == PlayerItems.TypeTalentsNanoStorm || skill == PlayerItems.TypeTalentsNanoShield || skill == PlayerItems.TypeTalentsShortCircuit || skill == PlayerItems.TypeTalentsPulseNova || skill == PlayerItems.TypeTalentsLaserDestruction || skill == PlayerItems.TypeTalentsDecoy)
		{
			empty = string.Format(StaticData.Translate(StaticData.allTypes.get_Item(skill).description).Replace(StaticData.Translate("key_main_screen_skill_tooltip1"), string.Concat(" ", StaticData.Translate("key_main_screen_skill_tooltip2"))), num, num1, (int)((float)((num + num1) * num2) / 100f));
		}
		else if (skill == PlayerItems.TypeTalentsPowerBreak || skill == PlayerItems.TypeTalentsPowerCut || skill == PlayerItems.TypeTalentsSunderArmor || skill == PlayerItems.TypeTalentsRocketBarrage || skill == PlayerItems.TypeTalentsForceWave)
		{
			empty = string.Format(StaticData.Translate(StaticData.allTypes.get_Item(skill).description).Replace(StaticData.Translate("key_main_screen_skill_tooltip1"), string.Concat(" ", StaticData.Translate("key_main_screen_skill_tooltip2"))), num, (int)((float)(num1 * num2) / 100f), num + num1);
		}
		else
		{
			empty = (skill != PlayerItems.TypeTalentsFocusFire ? NetworkScript.player.playerBelongings.playerItems.GetSkillDescription(skill, (byte)NetworkScript.player.playerBelongings.playerItems.GetAmountAt(skill)) : string.Format(StaticData.Translate(StaticData.allTypes.get_Item(skill).description).Replace(StaticData.Translate("key_main_screen_skill_tooltip1"), string.Concat(" ", StaticData.Translate("key_main_screen_skill_tooltip2"))), num, num1, (int)((float)((num + num1) * num2) / 100f)));
		}
		return empty;
	}

	private int GetTalentType(TalentsInfo talent)
	{
		if (talent.itemType == PlayerItems.TypeTalentsSunderArmor)
		{
			return 0;
		}
		if (talent.itemType == PlayerItems.TypeTalentsTaunt)
		{
			return 0;
		}
		if (talent.itemType == PlayerItems.TypeTalentsFocusFire)
		{
			return 0;
		}
		if (talent.itemType == PlayerItems.TypeTalentsRocketBarrage)
		{
			return 0;
		}
		if (talent.itemType == PlayerItems.TypeTalentsUnstoppable)
		{
			return 1;
		}
		if (talent.itemType == PlayerItems.TypeTalentsForceWave)
		{
			return 0;
		}
		if (talent.itemType == PlayerItems.TypeTalentsShieldFortress)
		{
			return 1;
		}
		if (talent.itemType == PlayerItems.TypeTalentsLaserDestruction)
		{
			return 0;
		}
		if (talent.itemType == PlayerItems.TypeTalentsStealth)
		{
			return 1;
		}
		if (talent.itemType == PlayerItems.TypeTalentsDecoy)
		{
			return 0;
		}
		if (talent.itemType == PlayerItems.TypeTalentsPowerBreak)
		{
			return 0;
		}
		if (talent.itemType == PlayerItems.TypeTalentsPowerCut)
		{
			return 0;
		}
		if (talent.itemType == PlayerItems.TypeTalentsLightSpeed)
		{
			return 1;
		}
		if (talent.itemType == PlayerItems.TypeTalentsMistShroud)
		{
			return 1;
		}
		if (talent.itemType == PlayerItems.TypeTalentsRepairingDrones)
		{
			return 2;
		}
		if (talent.itemType == PlayerItems.TypeTalentsNanoStorm)
		{
			return 0;
		}
		if (talent.itemType == PlayerItems.TypeTalentsNanoShield)
		{
			return 2;
		}
		if (talent.itemType == PlayerItems.TypeTalentsRepairField)
		{
			return 1;
		}
		if (talent.itemType == PlayerItems.TypeTalentsShortCircuit)
		{
			return 0;
		}
		if (talent.itemType == PlayerItems.TypeTalentsPulseNova)
		{
			return 0;
		}
		if (talent.itemType == PlayerItems.TypeTalentsRemedy)
		{
			return 2;
		}
		return 0;
	}

	private void HideMenu(object p)
	{
		float _width = this.sideMenuWindow.boundries.get_width() / MainScreenWindow.animationTime;
		if (this.sideMenuWindow.boundries.get_x() <= (float)Screen.get_width())
		{
			ref Rect rectPointer = ref this.sideMenuWindow.boundries;
			rectPointer.set_x(rectPointer.get_x() + Time.get_deltaTime() * _width);
		}
		else
		{
			this.sideMenuWindow.boundries.set_x((float)Screen.get_width());
			this.sideMenuWindow.preDrawHandler = null;
			this.isSideMenuOnScreen = false;
		}
	}

	private void HideMenuForForBtnUnlock(object p)
	{
		float _width = this.sideMenuWindow.boundries.get_width() / MainScreenWindow.animationTime;
		if (this.sideMenuWindow.boundries.get_x() <= (float)Screen.get_width())
		{
			ref Rect rectPointer = ref this.sideMenuWindow.boundries;
			rectPointer.set_x(rectPointer.get_x() + Time.get_deltaTime() * _width);
		}
		else
		{
			this.sideMenuWindow.boundries.set_x((float)Screen.get_width());
			this.sideMenuWindow.preDrawHandler = new Action<object>(this, MainScreenWindow.ShowMenu);
			short num = NetworkScript.player.playerBelongings.playerLevel;
			switch (num)
			{
				case 7:
				{
					this.btnUniversMap.isEnabled = true;
					break;
				}
				case 8:
				{
					this.btnPVP.isEnabled = true;
					break;
				}
				case 9:
				{
					this.btnGuild.isEnabled = true;
					break;
				}
				case 10:
				{
					this.btnSendGift.isEnabled = true;
					break;
				}
				default:
				{
					if (num == 30)
					{
						this.btnTransformer.isEnabled = true;
						break;
					}
					else
					{
						break;
					}
				}
			}
		}
	}

	private void HideQuestTracker(object p)
	{
		if (this.questTrackerWindow != null)
		{
			float _width = this.questTrackerWindow.boundries.get_width() / MainScreenWindow.animationTime;
			if (this.questTrackerWindow.boundries.get_x() >= -this.questTrackerWindow.boundries.get_width())
			{
				ref Rect rectPointer = ref this.questTrackerWindow.boundries;
				rectPointer.set_x(rectPointer.get_x() - Time.get_deltaTime() * _width);
			}
			else
			{
				this.questTrackerWindow.boundries.set_x(-this.questTrackerWindow.boundries.get_width());
				this.questTrackerWindow.preDrawHandler = null;
			}
		}
	}

	public void HideSideMenus()
	{
		if (this.sideMenuWindow != null)
		{
			this.sideMenuWindow.isHidden = true;
		}
		if (this.quickSlotsWindow != null)
		{
			this.quickSlotsWindow.isHidden = true;
		}
		if (MainScreenWindow.playerInfoWindow != null)
		{
			MainScreenWindow.playerInfoWindow.isHidden = true;
		}
		if (this.playerStatsWindow != null)
		{
			this.playerStatsWindow.isHidden = true;
		}
	}

	private void HideStoryQuestTracker(object p)
	{
		if (this.storyTrackerWindow != null)
		{
			float _width = this.storyTrackerWindow.boundries.get_width() / MainScreenWindow.animationTime;
			if (this.storyTrackerWindow.boundries.get_x() <= (float)Screen.get_width())
			{
				ref Rect rectPointer = ref this.storyTrackerWindow.boundries;
				rectPointer.set_x(rectPointer.get_x() + Time.get_deltaTime() * _width);
			}
			else
			{
				this.storyTrackerWindow.boundries.set_x((float)Screen.get_width());
				this.storyTrackerWindow.preDrawHandler = null;
			}
		}
	}

	public void HideWindow()
	{
		if (MainScreenWindow.playerInfoWindow != null)
		{
			MainScreenWindow.playerInfoWindow.isHidden = true;
		}
		if (this.quickSlotsWindow != null)
		{
			this.quickSlotsWindow.isHidden = true;
		}
		if (this.sideMenuWindow != null)
		{
			this.sideMenuWindow.isHidden = true;
		}
	}

	public void InitFreezeTimeWindow()
	{
		if (NetworkScript.player == null || NetworkScript.player.pvpGame == null || NetworkScript.player.pvpGame.gameType.winType != null)
		{
			return;
		}
		this.freezeTimeWindow = new GuiWindow();
		this.freezeTimeWindow.SetBackgroundTexture("PvPDominationGui", "countdown");
		this.freezeTimeWindow.PutToHorizontalCenter();
		this.freezeTimeWindow.boundries.set_y(175f);
		this.freezeTimeWindow.isHidden = false;
		AndromedaGui.gui.AddWindow(this.freezeTimeWindow);
		TimeSpan timeSpan = NetworkScript.player.pvpGame.freezeUntilTime - StaticData.now;
		int totalSeconds = (int)timeSpan.get_TotalSeconds() + 1;
		GuiSecondsTracker guiSecondsTracker = new GuiSecondsTracker(StaticData.Translate("key_pvp_freeze_time_window"), totalSeconds * 1000, this.freezeTimeWindow)
		{
			boundries = new Rect(0f, 0f, 600f, 95f),
			FontSize = 14,
			TextColor = GuiNewStyleBar.aquamarineColor,
			Alignment = 4,
			Font = GuiLabel.FontBold
		};
	}

	public void MuteItemTrackers()
	{
		if (this.novaItemTracker != null)
		{
			this.novaItemTracker.muteSoundForNextChange = true;
		}
		if (this.cashItemTracker != null)
		{
			this.cashItemTracker.muteSoundForNextChange = true;
		}
		if (this.viralItemTracker != null)
		{
			this.viralItemTracker.muteSoundForNextChange = true;
		}
		if (this.ultralibriumItemTracker != null)
		{
			this.ultralibriumItemTracker.muteSoundForNextChange = true;
		}
	}

	private static void OnAvatarLoaded(AvatarJob job)
	{
	}

	public static void OnChatClicked(EventHandlerParam p)
	{
		if (!__ChatWindow.isOpen)
		{
			if (!NetworkScript.isInBase)
			{
				AndromedaGui.mainWnd.StopChatAnimation();
			}
			__ChatWindow.isNotOpenHasUnreadMessage = false;
			__ChatWindow.OpenTheWindow(false);
			__ChatWindow.isOpen = true;
		}
		else if (!__ChatWindow.isSmallChat)
		{
			EventHandlerParam eventHandlerParam = new EventHandlerParam()
			{
				customData = true
			};
			__ChatWindow.wnd.CreateSmallScreen(eventHandlerParam);
		}
		else
		{
			__ChatWindow.wnd.AssignLastChat();
			AndromedaGui.gui.RemoveWindow(__ChatWindow.wnd.handler);
			__ChatWindow.isOpen = false;
		}
	}

	private void OnCloseBtnClick(object prm)
	{
		if (NetworkScript.player.shipScript != null)
		{
			NetworkScript.player.shipScript.isGuiClosed = true;
		}
		this.CloseActiveWindow();
	}

	private void OnExitBase(EventHandlerParam parm)
	{
		if (NetworkScript.player.state == 80)
		{
			GameObject gameObject = GameObject.Find("FlyIn");
			if (gameObject != null)
			{
				Object.Destroy(gameObject);
			}
			playWebGame.udp.ExecuteCommand(22, null);
		}
	}

	public void OnSelfSelectClicked(object prm)
	{
		if (NetworkScript.player != null && NetworkScript.player.shipScript != null)
		{
			NetworkScript.player.shipScript.ManageSelectObjectRequest(NetworkScript.player.gameObject, NetworkScript.player.vessel);
		}
	}

	private void OnStartSkillMoveClicked(EventHandlerParam parm)
	{
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = (byte)6
		};
		this.OnWindowBtnClicked(eventHandlerParam);
	}

	public void OnWindowBtnClicked(EventHandlerParam prm)
	{
		if (prm.customData == null || AndromedaGui.galaxyJumpWnd != null)
		{
			return;
		}
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("menu_id", prm.customData);
		playWebGame.LogMixPanel(MixPanelEvents.OpenMenu, dictionary);
		if (QuestInfoWindow.Close())
		{
			QuestInfoWindow.Close();
		}
		byte num = (byte)prm.customData;
		if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute && num != 20 && num != 21)
		{
			AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "menu_open_close");
			AudioManager.PlayGUISound(fromStaticSet);
		}
		if (this.activeWindow != null)
		{
			this.activeWindow.OnClose();
			AndromedaGui.gui.RemoveWindow(this.activeWindow.handler);
			if (this.backgroundWindow != null)
			{
				AndromedaGui.gui.RemoveWindow(this.backgroundWindow.handler);
				this.backgroundWindow = null;
			}
			if (num == this.activWindowIndex)
			{
				if (this.OnCloseWindowCallback != null)
				{
					this.OnCloseWindowCallback.Invoke();
				}
				this.activeWindow = null;
				this.activWindowIndex = 255;
				this.hasMenuOpen = false;
				QuestInfoWindow.ResetActiveWindow();
				return;
			}
		}
		if (prm.customData2 != null)
		{
			if (num == 17)
			{
				ProfileScreen.selectedTabIndex = (byte)prm.customData2;
			}
			else if (num == 22)
			{
				TransformerWindow.selectedTab = (byte)prm.customData2;
			}
		}
		this.activeWindow = (GuiWindow)this.windowTypes[num].GetConstructors()[0].Invoke(null);
		this.activeWindow.isHidden = true;
		this.activeWindow.Create();
		float _y = this.activeWindow.boundries.get_y();
		this.activeWindow.timeHammerFx = 0.5f;
		this.activeWindow.amplitudeHammerShake = 20f;
		this.activeWindow.moveToShakeRatio = 0.6f;
		this.activeWindow.boundries.set_y(-this.activeWindow.boundries.get_height());
		this.activeWindow.isHidden = false;
		this.activeWindow.StartHammerEffect(this.activeWindow.boundries.get_x(), _y);
		this.CreateCloseButton();
		AndromedaGui.gui.AddWindow(this.activeWindow);
		this.activWindowIndex = num;
		this.hasMenuOpen = true;
		if (num != 20)
		{
			this.backgroundWindow = new GuiWindow()
			{
				boundries = new Rect(0f, 0f, (float)Screen.get_width(), (float)Screen.get_height()),
				isHidden = false,
				zOrder = 209
			};
			this.dot = new GuiTexture();
			this.dot.SetTexture("FrameworkGUI", "gui_tooltip_background");
			this.dot.boundries = this.backgroundWindow.boundries;
			this.backgroundWindow.AddGuiElement(this.dot);
			AndromedaGui.gui.AddWindow(this.backgroundWindow);
		}
		QuestInfoWindow.SetCloseAciveWindow(new Action(this, MainScreenWindow.CloseActiveWindow));
	}

	public void OpenFeedbackWindow()
	{
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = (byte)27
		};
		this.OnWindowBtnClicked(eventHandlerParam);
		if (this.activeWindow is FeedbackWindow)
		{
			((FeedbackWindow)this.activeWindow).PopulateScreenShot(this.feedbackTexture);
		}
	}

	private GuiWindow OpenMainMenu(object prm)
	{
		MainScreenWindow.animationTime = 0.5f;
		this.sideMenuOpenWindow.preDrawHandler = null;
		this.sideMenuWindow.preDrawHandler = new Action<object>(this, MainScreenWindow.ShowMenu);
		return null;
	}

	public void Populate()
	{
		this.RefreshInformationIcons();
	}

	public void PopulateInstanceStatsWindow(short killProgress = 0, short killTarget = 0, InstanceDifficulty difficulty = 0)
	{
		if (NetworkScript.player == null || NetworkScript.player.shipScript == null || NetworkScript.player.shipScript.comm.galaxy == null || NetworkScript.player.shipScript.comm.galaxy.__galaxyId >= 2000 && NetworkScript.player.shipScript.comm.galaxy.__galaxyId <= 1000)
		{
			if (this.instanceStatsWindow != null)
			{
				AndromedaGui.gui.RemoveWindow(this.instanceStatsWindow.handler);
				this.instanceStatsWindow = null;
			}
			return;
		}
		if (this.instanceStatsWindow != null)
		{
			this.instanceProgress.text = string.Format(StaticData.Translate("key_instance_difficulty_lbl_kills"), killProgress, killTarget);
			switch (difficulty)
			{
				case 0:
				{
					this.difficultyLbl.text = StaticData.Translate("key_instance_difficulty_btn_normal");
					this.difficultyLbl.TextColor = GuiNewStyleBar.blueColor;
					break;
				}
				case 1:
				{
					this.difficultyLbl.text = StaticData.Translate("key_instance_difficulty_btn_hard");
					this.difficultyLbl.TextColor = GuiNewStyleBar.orangeColor;
					break;
				}
				case 2:
				{
					this.difficultyLbl.text = StaticData.Translate("key_instance_difficulty_btn_insane");
					this.difficultyLbl.TextColor = GuiNewStyleBar.redColor;
					break;
				}
			}
		}
		else
		{
			this.instanceStatsWindow = new GuiWindow();
			this.instanceStatsWindow.SetBackgroundTexture("InstanceDifficulty", "top");
			this.instanceStatsWindow.isHidden = false;
			this.instanceStatsWindow.zOrder = 227;
			this.instanceStatsWindow.boundries.set_x((float)(Screen.get_width() / 2 - 233));
			this.instanceStatsWindow.boundries.set_y(27f);
			AndromedaGui.gui.AddWindow(this.instanceStatsWindow);
			GuiTexture guiTexture = new GuiTexture();
			guiTexture.SetTexture("MinimapWindow", "portal_instance");
			guiTexture.boundries = new Rect(16f, 16f, 24f, 24f);
			this.instanceStatsWindow.AddGuiElement(guiTexture);
			GuiLabel guiLabel = new GuiLabel()
			{
				boundries = new Rect(40f, 18f, 130f, 20f),
				text = StaticData.Translate(NetworkScript.player.shipScript.comm.galaxy.nameUI),
				Font = GuiLabel.FontBold,
				FontSize = 12,
				Alignment = 3
			};
			this.instanceStatsWindow.AddGuiElement(guiLabel);
			this.difficultyLbl = new GuiLabel()
			{
				boundries = new Rect(315f, 18f, 130f, 20f),
				Font = GuiLabel.FontBold,
				FontSize = 14,
				Alignment = 5
			};
			this.instanceStatsWindow.AddGuiElement(this.difficultyLbl);
			switch (difficulty)
			{
				case 0:
				{
					this.difficultyLbl.text = StaticData.Translate("key_instance_difficulty_btn_normal");
					this.difficultyLbl.TextColor = GuiNewStyleBar.blueColor;
					break;
				}
				case 1:
				{
					this.difficultyLbl.text = StaticData.Translate("key_instance_difficulty_btn_hard");
					this.difficultyLbl.TextColor = GuiNewStyleBar.orangeColor;
					break;
				}
				case 2:
				{
					this.difficultyLbl.text = StaticData.Translate("key_instance_difficulty_btn_insane");
					this.difficultyLbl.TextColor = GuiNewStyleBar.redColor;
					break;
				}
			}
			this.instanceProgress = new GuiLabel()
			{
				boundries = new Rect(177f, 18f, 130f, 20f),
				Font = GuiLabel.FontBold,
				FontSize = 12,
				Alignment = 4,
				text = string.Format(StaticData.Translate("key_instance_difficulty_lbl_kills"), killProgress, killTarget)
			};
			this.instanceStatsWindow.AddGuiElement(this.instanceProgress);
		}
	}

	public void PopulatePlayerInfo(PlayerObjectPhysics plr)
	{
		if (this.lblPlayerName == null)
		{
			return;
		}
		if (!this.areAllValuesAquirred)
		{
			this.playerNameSD = StaticData.Translate("key_guest_player");
			this.playerLevelSD = StaticData.Translate("key_main_screen_player_info_lvl");
			this.cargoValueSD = StaticData.Translate("key_main_screen_player_info_cargo");
			this.lblPlayerName.text = (!plr.isGuest ? plr.playerName : this.playerNameSD);
			this.lblPlayerLevel.boundries.set_x(this.lblPlayerName.boundries.get_x() + this.lblPlayerName.TextWidth + 5f);
			this.lblPlayerLevel.text = string.Format(this.playerLevelSD, plr.cfg.playerLevel);
			this.playerLevelValueToCheck = plr.cfg.playerLevel;
			this.lblCargoVal.text = string.Format(this.cargoValueSD, (long)plr.cfg.cargoMax - plr.cfg.playerItems.get_Cargo());
			this.cargoValueToCheck = (long)plr.cfg.cargoMax - plr.cfg.playerItems.get_Cargo();
			this.areAllValuesAquirred = true;
		}
		if (plr.isGuest && this.lblPlayerName.text != this.playerNameSD || !plr.isGuest && this.lblPlayerName.text != plr.playerName)
		{
			this.lblPlayerName.text = (!plr.isGuest ? plr.playerName : this.playerNameSD);
			this.lblPlayerLevel.boundries.set_x(this.lblPlayerName.boundries.get_x() + this.lblPlayerName.TextWidth + 5f);
		}
		if (this.playerLevelValueToCheck != plr.cfg.playerLevel)
		{
			this.lblPlayerLevel.text = string.Format(this.playerLevelSD, plr.cfg.playerLevel);
			this.playerLevelValueToCheck = plr.cfg.playerLevel;
		}
		Texture2D avatarOrStartIt = playWebGame.GetAvatarOrStartIt(plr.playerAvatarUrl, new Action<AvatarJob>(null, MainScreenWindow.OnAvatarLoaded));
		if (avatarOrStartIt != null)
		{
			this.playerAvatar.SetTextureKeepSize(avatarOrStartIt);
		}
		else
		{
			this.playerAvatar.SetTextureKeepSize("ShipsAvatars", plr.cfg.assetName);
		}
		this.barHull.maximum = (float)plr.cfg.hitPointsMax;
		this.barHull.current = (float)plr.cfg.hitPoints;
		this.barShield.maximum = (float)plr.cfg.shieldMax;
		this.barShield.current = plr.cfg.shield;
		float item = 0f;
		if (StaticData.levelsType.ContainsKey(plr.cfg.playerLevel))
		{
			item = (float)StaticData.levelsType.get_Item(plr.cfg.playerLevel).tottalExp;
			if (!StaticData.levelsType.ContainsKey(plr.cfg.playerLevel + 1))
			{
				this.barExp.maximum = item;
			}
			else
			{
				this.barExp.maximum = (float)StaticData.levelsType.get_Item(plr.cfg.playerLevel + 1).tottalExp - item;
			}
		}
		this.barExp.current = (float)NetworkScript.player.playerBelongings.playerItems.GetAmountAt(13) - item;
		this.barCargo.maximum = (float)plr.cfg.cargoMax;
		this.barCargo.current = (float)plr.cfg.playerItems.get_Cargo();
		if (this.barCargo.current / this.barCargo.maximum > 0.9f && !this.isCargoFull)
		{
			this.barCargo.SetCustumSizeRedBar(70);
			this.lblCargoVal.TextColor = GuiNewStyleBar.redColor;
			this.StartCargoFullAnimation();
			this.isCargoFull = true;
		}
		else if (this.barCargo.current / this.barCargo.maximum < 0.9f && this.isCargoFull)
		{
			this.barCargo.SetCustumSizeOrangeBar(70);
			this.lblCargoVal.TextColor = GuiNewStyleBar.orangeColor;
			this.StopCargoFullAnimation();
			this.isCargoFull = false;
		}
		if ((long)plr.cfg.cargoMax - plr.cfg.playerItems.get_Cargo() != this.cargoValueToCheck)
		{
			this.lblCargoVal.text = string.Format(this.cargoValueSD, (long)plr.cfg.cargoMax - plr.cfg.playerItems.get_Cargo());
			this.cargoValueToCheck = (long)plr.cfg.cargoMax - plr.cfg.playerItems.get_Cargo();
		}
		this.lblHullVal.text = plr.cfg.hitPoints.ToString("###,##0");
		this.lblShieldVal.text = plr.cfg.shield.ToString("###,##0");
	}

	public void PopulatePvPDominationGame(uint nbId = 0)
	{
		if (NetworkScript.player.pvpGame == null)
		{
			if (this.pvpDominationScoreWindow != null)
			{
				AndromedaGui.gui.RemoveWindow(this.pvpDominationScoreWindow.handler);
				this.pvpDominationScoreWindow = null;
			}
			return;
		}
		if (this.pvpDominationScoreWindow == null)
		{
			this.CreatePvPDominationWindow();
		}
		float single = 0f;
		float single1 = 0f;
		if (NetworkScript.player.vessel.teamNumber == 1)
		{
			single = (float)NetworkScript.player.pvpGame.teamOneScore;
			single1 = (float)NetworkScript.player.pvpGame.teamTwoScore;
		}
		else if (NetworkScript.player.vessel.teamNumber == 2)
		{
			single = (float)NetworkScript.player.pvpGame.teamTwoScore;
			single1 = (float)NetworkScript.player.pvpGame.teamOneScore;
		}
		this.yourTeamScore.text = single.ToString("##,##0");
		this.enemyTeamScore.text = single1.ToString("##,##0");
		this.yourTeamBar.boundries.set_width(105f * single / (float)NetworkScript.player.pvpGame.gameType.winParam);
		this.yourTeamBar.boundries.set_x(175f - this.yourTeamBar.boundries.get_width());
		this.enemyTeamBar.boundries.set_width(105f * single1 / (float)NetworkScript.player.pvpGame.gameType.winParam);
		IList<GameObjectPhysics> values = NetworkScript.player.shipScript.comm.gameObjects.get_Values();
		if (MainScreenWindow.<>f__am$cache76 == null)
		{
			MainScreenWindow.<>f__am$cache76 = new Func<GameObjectPhysics, bool>(null, (GameObjectPhysics t) => t is MiningStation);
		}
		GameObjectPhysics[] array = Enumerable.ToArray<GameObjectPhysics>(Enumerable.Where<GameObjectPhysics>(values, MainScreenWindow.<>f__am$cache76));
		string empty = string.Empty;
		for (int i = 0; i < (int)array.Length; i++)
		{
			int ownerTeam = ((MiningStation)array[i]).get_OwnerTeam();
			if (ownerTeam != 0)
			{
				empty = (ownerTeam != NetworkScript.player.vessel.teamNumber ? "iconRed" : "iconGreen");
			}
			else
			{
				empty = "iconDefault";
			}
			this.miningStationIcons[i].SetTextureKeepSize("PvPDominationGui", empty);
			if (array[i].neighbourhoodId == nbId)
			{
				GuiTextureAnimated guiTextureAnimated = new GuiTextureAnimated()
				{
					isHoverAware = true
				};
				guiTextureAnimated.Init("PvPDominationAnimation", "PvPDominationAnimation", "PvPDominationAnimation/frame01");
				guiTextureAnimated.X = (float)(183 + 33 * i);
				guiTextureAnimated.Y = 19f;
				guiTextureAnimated.rotationTime = 1f;
				guiTextureAnimated.finishDraw = new Action<GuiTextureAnimated>(this, MainScreenWindow.FinishOwnerChange);
				this.pvpDominationScoreWindow.AddGuiElement(guiTextureAnimated);
			}
		}
	}

	private void QuickSlotWindowHandler()
	{
		if (NetworkScript.player == null || NetworkScript.player.vessel == null)
		{
			return;
		}
		long item = (long)0;
		long num = (long)0;
		int num1 = NetworkScript.player.playerBelongings.playerLevel;
		this.levelLbl.text = string.Format(StaticData.Translate("key_quickslots_level_lbl"), num1);
		if (StaticData.levelsType.ContainsKey(num1))
		{
			item = StaticData.levelsType.get_Item(num1).tottalExp;
			num = (!StaticData.levelsType.ContainsKey(num1 + 1) ? item : StaticData.levelsType.get_Item(num1 + 1).tottalExp);
		}
		float amountAt = 0f;
		if (num != item)
		{
			amountAt = (float)(NetworkScript.player.playerBelongings.playerItems.GetAmountAt(13) - item) / (float)(num - item);
		}
		amountAt = amountAt * 100f;
		if (amountAt >= 100f)
		{
			amountAt = 99f;
		}
		this.xpBar.current = amountAt;
		this.xpBar.maximum = 100f;
		this.levelValueLbl.text = string.Format("{0} %", Mathf.FloorToInt(amountAt));
		this.cargoBar.maximum = (float)NetworkScript.player.vessel.cfg.cargoMax;
		this.cargoBar.current = (float)NetworkScript.player.playerBelongings.playerItems.get_Cargo();
		float single = this.cargoBar.current / this.cargoBar.maximum;
		this.cargoValueLbl.text = string.Format("{0:##0} %", single * 100f);
		if (single > 0.9f && !this.isCargoFull)
		{
			this.cargoBar.SetCustumSizeRedBar(120);
			this.cargoValueLbl.TextColor = GuiNewStyleBar.redColor;
			this.cargoLbl.TextColor = GuiNewStyleBar.redColor;
			this.isCargoFull = true;
		}
		else if (single < 0.9f && this.isCargoFull)
		{
			this.cargoBar.SetCustumSizeOrangeBar(120);
			this.cargoValueLbl.TextColor = GuiNewStyleBar.orangeColor;
			this.cargoLbl.TextColor = GuiNewStyleBar.orangeColor;
			this.isCargoFull = false;
		}
	}

	public void ReCreateQuickSlotsMenu()
	{
		if (this.quickSlotsWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.quickSlotsWindow.handler);
			this.quickSlotsWindow = null;
			this.RemoveSlowIndication();
			this.RemoveStunIndication();
			this.RemoveShockIndication();
		}
		this.CreateQuickSlotsMenu();
	}

	public void RefreshInformationIcons()
	{
		foreach (GuiElement guiElement in this.forDelete)
		{
			this.quickSlotsWindow.RemoveGuiElement(guiElement);
		}
		this.forDelete.Clear();
		this.DrawActiveBoosters();
		this.DrawAttachedWeapons();
	}

	public void RefreshMenuBtnState()
	{
		MainScreenWindow.animationTime = 1.2f;
		if (this.sideMenuWindow != null)
		{
			this.sideMenuWindow.preDrawHandler = new Action<object>(this, MainScreenWindow.HideMenuForForBtnUnlock);
		}
	}

	public void RefreshOutOfAmmo()
	{
		WeaponSlot[] weaponSlotArray = NetworkScript.player.cfg.weaponSlots;
		for (int i = 0; i < (int)weaponSlotArray.Length; i++)
		{
			WeaponSlot weaponSlot = weaponSlotArray[i];
			this.quickSlotsWindow.RemoveGuiElement(MainScreenWindow.weapon_slots_current[weaponSlot.slotId]);
			MainScreenWindow.weapon_slots_current[weaponSlot.slotId] = this.weapon_slots[weaponSlot.slotId];
			this.quickSlotsWindow.AddGuiElementAtBottom(this.weapon_slots[weaponSlot.slotId]);
			if (weaponSlot.isActive && weaponSlot.get_WeaponStatus() == 5)
			{
				this.quickSlotsWindow.RemoveGuiElement(MainScreenWindow.weapon_slots_current[weaponSlot.slotId]);
				MainScreenWindow.weapon_slots_current[weaponSlot.slotId] = this.weapon_slots_no_ammo[weaponSlot.slotId];
				this.quickSlotsWindow.AddGuiElement(this.weapon_slots_no_ammo[weaponSlot.slotId]);
			}
		}
	}

	public void ReloadSkillSlot(ActiveSkillSlot slot)
	{
		if (slot.get_SkillStatus() == 4 && slot.slotId < 9)
		{
			long num = slot.nextCastTime;
			DateTime now = DateTime.get_Now();
			float ticks = (float)((double)(num - now.get_Ticks()) / 10000000);
			GuiTextureAnimated guiTextureAnimated = new GuiTextureAnimated();
			guiTextureAnimated.Init("Skills", "skillReloading", "skillReloading");
			guiTextureAnimated.rotationTime = ticks + 0.2f;
			guiTextureAnimated.finishDraw = new Action<GuiTextureAnimated>(this, MainScreenWindow.FinishReloading);
			guiTextureAnimated.boundries.set_x((float)(69 + slot.slotId * 40));
			guiTextureAnimated.boundries.set_y(55f);
			guiTextureAnimated.boundries.set_width(35f);
			guiTextureAnimated.boundries.set_height(35f);
			this.quickSlotsWindow.AddGuiElement(guiTextureAnimated);
			int num1 = Mathf.CeilToInt(ticks) * 1000;
			GuiSecondsTracker guiSecondsTracker = new GuiSecondsTracker(num1, this.quickSlotsWindow)
			{
				boundries = new Rect((float)(68 + slot.slotId * 40), 54f, 35f, 35f),
				Alignment = 4,
				FontSize = 12
			};
		}
	}

	public void ReloadSkillSlotIpad(ActiveSkillSlot slot)
	{
		int num = 0;
		switch (slot.slotId)
		{
			case 0:
			{
				num = 18;
				break;
			}
			case 1:
			{
				num = 92;
				break;
			}
			case 2:
			{
				num = 174;
				break;
			}
			case 3:
			{
				num = 255;
				break;
			}
			case 4:
			{
				num = 337;
				break;
			}
			case 5:
			{
				num = 419;
				break;
			}
			case 6:
			{
				num = 501;
				break;
			}
			case 7:
			{
				num = 582;
				break;
			}
			case 8:
			{
				num = 665;
				break;
			}
		}
		if (slot.get_SkillStatus() == 4 && slot.slotId < 9)
		{
			long num1 = slot.nextCastTime;
			DateTime now = DateTime.get_Now();
			float ticks = (float)((double)(num1 - now.get_Ticks()) / 10000000);
			GuiTextureAnimated guiTextureAnimated = new GuiTextureAnimated();
			guiTextureAnimated.Init("Skills", "skillReloading", "skillReloading");
			guiTextureAnimated.rotationTime = ticks;
			guiTextureAnimated.finishDraw = new Action<GuiTextureAnimated>(this, MainScreenWindow.FinishReloading);
			guiTextureAnimated.boundries.set_x((float)num);
			guiTextureAnimated.boundries.set_y(48f);
			guiTextureAnimated.boundries.set_width(56f);
			guiTextureAnimated.boundries.set_height(56f);
			this.quickSlotsWindow.AddGuiElement(guiTextureAnimated);
			int num2 = Mathf.CeilToInt(ticks) * 1000;
			GuiSecondsTracker guiSecondsTracker = new GuiSecondsTracker(num2, this.quickSlotsWindow)
			{
				boundries = new Rect((float)num, 48f, 56f, 56f),
				Alignment = 4,
				FontSize = 14
			};
		}
	}

	public void ReloadWeapon(WeaponSlot ws)
	{
		if (ws.get_WeaponStatus() == 4)
		{
			GuiTextureAnimated guiTextureAnimated = new GuiTextureAnimated();
			guiTextureAnimated.Init("Reloading", "weapon_slot_reload", "weapon_slot_reload_01");
			guiTextureAnimated.rotationTime = (float)((double)ws.realReloadTime / 10000000);
			guiTextureAnimated.finishDraw = new Action<GuiTextureAnimated>(this, MainScreenWindow.FinishReloading);
			guiTextureAnimated.boundries.set_x(this.weapon_slots[ws.slotId].boundries.get_x() + 3f);
			guiTextureAnimated.boundries.set_y(this.weapon_slots[ws.slotId].boundries.get_y() + 3f);
			this.quickSlotsWindow.AddGuiElement(guiTextureAnimated);
			this.weapon_slots[ws.slotId].SetTexture("MainScreenWindow", "weapons_slot");
		}
		this.RefreshOutOfAmmo();
	}

	public void RemoveCriticalIndication()
	{
		if (this.quickSlotsWindow != null || this.isCriticalIndicationOnScreen)
		{
			this.isCriticalIndicationOnScreen = false;
			foreach (GuiElement guiElement in this.energyElementsForDelete)
			{
				this.quickSlotsWindow.RemoveGuiElement(guiElement);
			}
		}
		this.energyElementsForDelete.Clear();
	}

	public void RemoveDisarmIndication()
	{
		if (this.disarmIndicationWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.disarmIndicationWindow.handler);
			this.disarmIndicationWindow = null;
			if (this.quickSlotsWindow != null)
			{
				foreach (GuiElement guiElement in this.disarmElementsForDelete)
				{
					this.quickSlotsWindow.RemoveGuiElement(guiElement);
				}
			}
			this.disarmElementsForDelete.Clear();
		}
	}

	public void RemoveFreezeTimeWindow()
	{
		if (this.freezeTimeWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.freezeTimeWindow.handler);
			this.freezeTimeWindow = null;
		}
	}

	private void RemoveQuestTrackerBlink(GuiTextureAnimated tx)
	{
		if (tx != null)
		{
			this.questTrackerWindow.RemoveGuiElement(tx);
		}
	}

	public void RemoveShockIndication()
	{
		if (this.shockIndicationWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.shockIndicationWindow.handler);
			this.shockIndicationWindow = null;
			if (this.quickSlotsWindow != null)
			{
				foreach (GuiElement guiElement in this.shockElementsForDelete)
				{
					this.quickSlotsWindow.RemoveGuiElement(guiElement);
				}
			}
			this.shockElementsForDelete.Clear();
		}
	}

	public void RemoveSlowIndication()
	{
		if (this.slowIndicationWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.slowIndicationWindow.handler);
			this.slowIndicationWindow = null;
		}
	}

	public void RemoveStunIndication()
	{
		if (this.stunIndicationWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.stunIndicationWindow.handler);
			this.stunIndicationWindow = null;
			if (this.quickSlotsWindow != null)
			{
				foreach (GuiElement guiElement in this.stunElementsForDelete)
				{
					this.quickSlotsWindow.RemoveGuiElement(guiElement);
				}
			}
			this.stunElementsForDelete.Clear();
		}
	}

	public void ReOrderGui()
	{
		if (this.quickSlotsWindow != null)
		{
			this.quickSlotsWindow.PutToHorizontalCenter();
			this.quickSlotsWindow.boundries.set_y((float)Screen.get_height() - this.quickSlotsWindow.boundries.get_height());
		}
		if (this.sideMenuWindow != null)
		{
			this.sideMenuWindow.PutToVerticalCenter();
			if (!this.isSideMenuOnScreen)
			{
				this.sideMenuWindow.boundries.set_x((float)Screen.get_width());
			}
			else
			{
				this.sideMenuWindow.boundries.set_x((float)(Screen.get_width() - 163));
			}
		}
		if (this.playerStatsWindow != null)
		{
			this.playerStatsWindow.boundries = new Rect((float)(Screen.get_width() / 2 - 238), 0f, 476f, 42f);
		}
		if (this.storyTrackerWindow != null)
		{
			this.storyTrackerWindow.boundries = new Rect((float)(Screen.get_width() - 248), 0f, 248f, 77f);
		}
		if (MainScreenWindow.playerInfoWindow != null)
		{
			MainScreenWindow.playerInfoWindow.boundries.set_x(0f);
			MainScreenWindow.playerInfoWindow.boundries.set_y(0f);
		}
		if (this.activeWindow != null)
		{
			this.activeWindow.PutToCenter();
		}
		if (this.questTrackerWindow != null)
		{
			this.questTrackerWindow.boundries.set_x(0f);
			this.questTrackerWindow.boundries.set_y((float)(Screen.get_height() - 280));
		}
		if (__ChatWindow.wnd != null)
		{
			__ChatWindow.wnd.boundries.set_x((float)Screen.get_width() - __ChatWindow.wnd.boundries.get_width());
			__ChatWindow.wnd.boundries.set_y((float)Screen.get_height() - __ChatWindow.wnd.boundries.get_height() + 2f);
		}
		if (this.pendingRewardNotificationWindow != null)
		{
			this.pendingRewardNotificationWindow.boundries = new Rect((float)(Screen.get_width() - 80), 0f, 80f, 80f);
		}
		if (this.sideMenuOpenWindow != null)
		{
			this.sideMenuOpenWindow.PutToVerticalCenter();
			this.sideMenuOpenWindow.boundries.set_x((float)(Screen.get_width() - 73));
		}
		if (this.feedbackButtonWindow != null)
		{
			this.feedbackButtonWindow.boundries.set_x(this.playerStatsWindow.boundries.get_x() + this.playerStatsWindow.boundries.get_width() + 15f);
		}
		if (this.freezeTimeWindow != null)
		{
			this.freezeTimeWindow.PutToHorizontalCenter();
			this.freezeTimeWindow.boundries.set_y(175f);
		}
		if (this.pvpDominationScoreWindow != null)
		{
			this.pvpDominationScoreWindow.boundries.set_x((float)(Screen.get_width() / 2 - 233));
			this.pvpDominationScoreWindow.boundries.set_y(29f);
		}
		if (this.instanceStatsWindow != null)
		{
			this.instanceStatsWindow.boundries.set_x((float)(Screen.get_width() / 2 - 233));
			this.instanceStatsWindow.boundries.set_y(29f);
		}
		if (this.backgroundWindow != null && this.dot != null)
		{
			this.backgroundWindow.boundries = new Rect(0f, 0f, (float)Screen.get_width(), (float)Screen.get_height());
			this.dot.boundries = this.backgroundWindow.boundries;
		}
		if (this.factionWarIndicationWindow != null)
		{
			this.factionWarIndicationWindow.boundries = new Rect((float)(Screen.get_width() - 82), 10f, 72f, 72f);
		}
		if (this.factionWarNotificationWindow != null)
		{
			this.factionWarNotificationWindow.boundries = new Rect((float)(Screen.get_width() - 72 - 82), 10f, 85f, 47f);
		}
		if (this.messagesIndicationWindow != null)
		{
			this.messagesIndicationWindow.boundries = new Rect((float)(Screen.get_width() - 82), 92f, 72f, 72f);
		}
	}

	public void SetGuildButtonAlerted()
	{
		if (this.btnGuild != null)
		{
			this.hasGuildInvintation = NetworkScript.player.playerBelongings.guildInvitesCount > 0;
			this.btnGuild.SetTextureNormal("MainScreenWindow", (!this.hasGuildInvintation ? "guildNml" : "guildAlert"));
		}
	}

	public void ShowCriticalIndication()
	{
		if (this.quickSlotsWindow == null || this.isCriticalIndicationOnScreen)
		{
			return;
		}
		this.isCriticalIndicationOnScreen = true;
		for (int i = 0; i < 9; i++)
		{
			if (NetworkScript.player.playerBelongings.skillConfig.skillSlots.ContainsKey(i) && NetworkScript.player.playerBelongings.skillConfig.skillSlots.get_Item(i).get_SkillStatus() != 4)
			{
				ushort item = (ushort)NetworkScript.player.playerBelongings.skillConfig.skillSlots.get_Item(i).skillId;
				if (item == PlayerItems.TypeTalentsSunderArmor || item == PlayerItems.TypeTalentsFocusFire || item == PlayerItems.TypeTalentsRocketBarrage || item == PlayerItems.TypeTalentsForceWave || item == PlayerItems.TypeTalentsLaserDestruction || item == PlayerItems.TypeTalentsDecoy || item == PlayerItems.TypeTalentsPowerBreak || item == PlayerItems.TypeTalentsPowerCut || item == PlayerItems.TypeTalentsRepairingDrones || item == PlayerItems.TypeTalentsNanoStorm || item == PlayerItems.TypeTalentsRepairField || item == PlayerItems.TypeTalentsShortCircuit || item == PlayerItems.TypeTalentsPulseNova || item == PlayerItems.TypeTalentsRemedy)
				{
					GuiTexture guiTexture = new GuiTexture();
					guiTexture.boundries.set_x((float)(68 + i * 40));
					guiTexture.boundries.set_y(54f);
					guiTexture.SetTexture("MainScreenWindow", "skills_box_critical");
					this.quickSlotsWindow.AddGuiElement(guiTexture);
					this.energyElementsForDelete.Add(guiTexture);
				}
			}
		}
	}

	public void ShowDisarmIndication()
	{
		if (this.disarmIndicationWindow != null)
		{
			return;
		}
		this.RemoveSlowIndication();
		this.disarmIndicationWindow = new GuiWindow()
		{
			boundries = new Rect(0f, 0f, 240f, 60f)
		};
		this.disarmIndicationWindow.boundries.set_x((float)(Screen.get_width() / 2 - 5));
		this.disarmIndicationWindow.boundries.set_y((float)(Screen.get_height() - 155));
		this.disarmIndicationWindow.isHidden = false;
		this.disarmIndicationWindow.zOrder = 49;
		this.disarmIndicationWindow.isClickTransparent = true;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("MainScreenWindow", "disarm");
		guiTexture.X = 0f;
		guiTexture.Y = 0f;
		this.disarmIndicationWindow.AddGuiElement(guiTexture);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(55f, 29f, 176f, 20f),
			Alignment = 3,
			text = StaticData.Translate("key_notification_disarmed"),
			FontSize = 16,
			Font = GuiLabel.FontBold,
			TextColor = Color.get_black()
		};
		this.disarmIndicationWindow.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(54f, 28f, 176f, 20f),
			Alignment = 3,
			text = StaticData.Translate("key_notification_disarmed"),
			FontSize = 16,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.redColor
		};
		this.disarmIndicationWindow.AddGuiElement(guiLabel1);
		AndromedaGui.gui.AddWindow(this.disarmIndicationWindow);
		if (this.quickSlotsWindow == null)
		{
			return;
		}
		for (int i = 0; i < 6; i++)
		{
			if (NetworkScript.player.cfg.weaponSlots[i].get_WeaponStatus() != null && NetworkScript.player.cfg.weaponSlots[i].get_WeaponStatus() != 1)
			{
				GuiTexture guiTexture1 = new GuiTexture();
				guiTexture1.boundries.set_x((float)(45 + i * 34));
				guiTexture1.boundries.set_y(0f);
				guiTexture1.SetTexture("MainScreenWindow", "weapons_slot_stunned");
				this.quickSlotsWindow.AddGuiElement(guiTexture1);
				this.disarmElementsForDelete.Add(guiTexture1);
			}
		}
		for (int j = 0; j < 9; j++)
		{
			if (NetworkScript.player.playerBelongings.skillConfig.skillSlots.ContainsKey(j))
			{
				if ((ushort)NetworkScript.player.playerBelongings.skillConfig.skillSlots.get_Item(j).skillId != PlayerItems.TypeTalentsUnstoppable)
				{
					GuiTexture guiTexture2 = new GuiTexture();
					guiTexture2.boundries.set_x((float)(68 + j * 40));
					guiTexture2.boundries.set_y(54f);
					guiTexture2.SetTexture("MainScreenWindow", "skills_box_stunned");
					this.quickSlotsWindow.AddGuiElement(guiTexture2);
					this.disarmElementsForDelete.Add(guiTexture2);
				}
			}
		}
	}

	public void ShowFactionWarGiftsNotification()
	{
		if (this.factionWarIndicationWindow == null)
		{
			return;
		}
		if (this.factionWarNotificationWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.factionWarNotificationWindow.handler);
			this.factionWarNotificationWindow = null;
		}
		int num = 0;
		if (NetworkScript.player.playerBelongings.lastWeekPendingReward != 0)
		{
			num++;
		}
		if (NetworkScript.player.playerBelongings.factionWarDay == FactionWarStats.resetWeeklyChalangeProgress && !NetworkScript.player.playerBelongings.weeklyRewardCollected && NetworkScript.player.playerBelongings.get_FactionWarWeeklyChalangeParticipation() > 0)
		{
			num++;
		}
		if (NetworkScript.player.playerBelongings.factionWarDayScore > 0)
		{
			if (NetworkScript.player.playerBelongings.factionWarDayScore >= 3000 && !NetworkScript.player.playerBelongings.rewardForDayProgressCollected1)
			{
				num++;
			}
			if (NetworkScript.player.playerBelongings.factionWarDayScore >= 6000 && !NetworkScript.player.playerBelongings.rewardForDayProgressCollected2)
			{
				num++;
			}
			if (NetworkScript.player.playerBelongings.factionWarDayScore >= 12000 && !NetworkScript.player.playerBelongings.rewardForDayProgressCollected3)
			{
				num++;
			}
		}
		if (num == 0)
		{
			return;
		}
		this.factionWarNotificationWindow = new GuiWindow()
		{
			boundries = new Rect((float)(Screen.get_width() - 72 - 82), 10f, 85f, 47f),
			isHidden = false,
			isClickTransparent = true,
			zOrder = 204
		};
		AndromedaGui.gui.AddWindow(this.factionWarNotificationWindow);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("WarScreenWindow", "warNotificationGiftLeft");
		guiTexture.X = 0f;
		guiTexture.Y = 5f;
		this.factionWarNotificationWindow.AddGuiElement(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("WarScreenWindow", "warNotificationGiftRight");
		guiTexture1.X = 45f;
		guiTexture1.Y = 0f;
		this.factionWarNotificationWindow.AddGuiElement(guiTexture1);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(40f, 16f, 26f, 16f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 14,
			Font = GuiLabel.FontBold,
			text = num.ToString("N0")
		};
		this.factionWarNotificationWindow.AddGuiElement(guiLabel);
	}

	public void ShowFactionWarNotificationWindow()
	{
		if (this.factionWarIndicationWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.factionWarIndicationWindow.handler);
			this.factionWarIndicationWindow = null;
		}
		if (NetworkScript.player == null || NetworkScript.player.playerBelongings == null)
		{
			return;
		}
		if (NetworkScript.player.playerBelongings.playerLevel < 10)
		{
			return;
		}
		if (NetworkScript.player.vessel.pvpState == 3)
		{
			return;
		}
		if (NetworkScript.player.vessel != null && NetworkScript.player.vessel.galaxy != null && NetworkScript.player.vessel.galaxy.get_galaxyId() == 1000)
		{
			return;
		}
		this.factionWarIndicationWindow = new GuiWindow()
		{
			boundries = new Rect((float)(Screen.get_width() - 82), 10f, 72f, 72f),
			isHidden = false,
			zOrder = 205
		};
		AndromedaGui.gui.AddWindow(this.factionWarIndicationWindow);
		string str = "warIcon";
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed()
		{
			boundries = new Rect(0f, 0f, 72f, 72f)
		};
		guiButtonFixed.SetTexture("WarScreenWindow", "btnWar");
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.eventHandlerParam.customData = (byte)34;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, MainScreenWindow.OnWindowBtnClicked);
		this.factionWarIndicationWindow.AddGuiElement(guiButtonFixed);
		switch (NetworkScript.player.playerBelongings.factionWarDay)
		{
			case 0:
			{
				str = "day-7";
				break;
			}
			case 1:
			{
				str = "day-1";
				break;
			}
			case 2:
			{
				str = "day-2";
				break;
			}
			case 3:
			{
				str = "day-3";
				break;
			}
			case 4:
			{
				str = "day-4";
				break;
			}
			case 5:
			{
				str = "day-5";
				break;
			}
			case 6:
			{
				str = "day-6";
				break;
			}
		}
		if (!NetworkScript.player.isWarInProgress)
		{
			str = "day-0";
		}
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("WarScreenWindow", str);
		guiTexture.boundries = new Rect(0f, 0f, 72f, 72f);
		this.factionWarIndicationWindow.AddGuiElement(guiTexture);
		this.ShowFactionWarGiftsNotification();
	}

	private void ShowMenu(object p)
	{
		float _width = this.sideMenuWindow.boundries.get_width() / MainScreenWindow.animationTime;
		if (this.sideMenuWindow.boundries.get_x() > (float)(Screen.get_width() - 163))
		{
			ref Rect rectPointer = ref this.sideMenuWindow.boundries;
			rectPointer.set_x(rectPointer.get_x() - Time.get_deltaTime() * _width);
		}
		else
		{
			this.sideMenuWindow.boundries.set_x((float)(Screen.get_width() - 163));
			this.sideMenuWindow.preDrawHandler = null;
			this.isSideMenuOnScreen = true;
		}
	}

	public void ShowPendingAwardNotification()
	{
		if (this.pendingRewardNotificationWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.pendingRewardNotificationWindow.handler);
			this.pendingRewardNotificationWindow = null;
		}
		if (NetworkScript.player == null || NetworkScript.player.playerBelongings == null)
		{
			return;
		}
		if (NetworkScript.player.vessel.pvpState == 3)
		{
			return;
		}
		if (NetworkScript.player.vessel != null && NetworkScript.player.vessel.galaxy != null && NetworkScript.player.vessel.galaxy.get_galaxyId() == 1000)
		{
			return;
		}
		if (Enumerable.Count<KeyValuePair<int, PlayerPendingAward>>(NetworkScript.player.playerBelongings.playerAwards) <= 0 && !NetworkScript.player.vessel.isGuest)
		{
			return;
		}
		this.pendingRewardNotificationWindow = new GuiWindow()
		{
			boundries = new Rect((float)(Screen.get_width() - 80), 0f, 80f, 80f)
		};
		this.pendingRewardNotificationWindow.SetBackgroundTexture("FrameworkGUI", "pendingAwardNotification");
		this.pendingRewardNotificationWindow.isHidden = false;
		this.pendingRewardNotificationWindow.zOrder = 205;
		AndromedaGui.gui.AddWindow(this.pendingRewardNotificationWindow);
		GuiButton guiButton = new GuiButton()
		{
			boundries = new Rect(0f, 0f, 100f, 100f),
			Caption = string.Empty
		};
		guiButton.eventHandlerParam.customData = (byte)30;
		guiButton.Clicked = new Action<EventHandlerParam>(this, MainScreenWindow.OnWindowBtnClicked);
		this.pendingRewardNotificationWindow.AddGuiElement(guiButton);
	}

	private void ShowQuestTracker(object p)
	{
		if (this.questTrackerWindow == null)
		{
			return;
		}
		float _width = this.questTrackerWindow.boundries.get_width() / MainScreenWindow.animationTime;
		if (this.questTrackerWindow.boundries.get_x() < 0f)
		{
			ref Rect rectPointer = ref this.questTrackerWindow.boundries;
			rectPointer.set_x(rectPointer.get_x() + Time.get_deltaTime() * _width);
		}
		else
		{
			this.questTrackerWindow.boundries.set_x(0f);
			this.questTrackerWindow.preDrawHandler = null;
		}
	}

	public void ShowShockIndication()
	{
		if (this.shockIndicationWindow != null)
		{
			return;
		}
		this.RemoveSlowIndication();
		this.shockIndicationWindow = new GuiWindow()
		{
			boundries = new Rect(0f, 0f, 240f, 60f)
		};
		this.shockIndicationWindow.boundries.set_x((float)(Screen.get_width() / 2 - 5));
		this.shockIndicationWindow.boundries.set_y((float)(Screen.get_height() - 155));
		this.shockIndicationWindow.isHidden = false;
		this.shockIndicationWindow.zOrder = 49;
		this.shockIndicationWindow.isClickTransparent = true;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("MainScreenWindow", "shock");
		guiTexture.X = 0f;
		guiTexture.Y = 0f;
		this.shockIndicationWindow.AddGuiElement(guiTexture);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(55f, 29f, 176f, 20f),
			Alignment = 3,
			text = StaticData.Translate("key_notification_shocked"),
			FontSize = 16,
			Font = GuiLabel.FontBold,
			TextColor = Color.get_black()
		};
		this.shockIndicationWindow.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(54f, 28f, 176f, 20f),
			Alignment = 3,
			text = StaticData.Translate("key_notification_shocked"),
			FontSize = 16,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.goldColor
		};
		this.shockIndicationWindow.AddGuiElement(guiLabel1);
		AndromedaGui.gui.AddWindow(this.shockIndicationWindow);
		if (this.quickSlotsWindow == null)
		{
			return;
		}
		for (int i = 0; i < 9; i++)
		{
			if (NetworkScript.player.playerBelongings.skillConfig.skillSlots.ContainsKey(i))
			{
				ushort item = (ushort)NetworkScript.player.playerBelongings.skillConfig.skillSlots.get_Item(i).skillId;
				if (item == PlayerItems.TypeTalentsRepairField || item == PlayerItems.TypeTalentsRemedy || item == PlayerItems.TypeTalentsNanoShield || item == PlayerItems.TypeTalentsMistShroud || item == PlayerItems.TypeTalentsShieldFortress || item == PlayerItems.TypeTalentsUnstoppable)
				{
					GuiTexture guiTexture1 = new GuiTexture();
					guiTexture1.boundries.set_x((float)(68 + i * 40));
					guiTexture1.boundries.set_y(54f);
					guiTexture1.SetTexture("MainScreenWindow", "skills_box_stunned");
					this.quickSlotsWindow.AddGuiElement(guiTexture1);
					this.shockElementsForDelete.Add(guiTexture1);
				}
			}
		}
	}

	public void ShowSideMenus()
	{
		if (this.sideMenuWindow != null)
		{
			this.sideMenuWindow.isHidden = false;
		}
		if (this.quickSlotsWindow != null)
		{
			this.quickSlotsWindow.isHidden = false;
		}
		if (MainScreenWindow.playerInfoWindow != null)
		{
			MainScreenWindow.playerInfoWindow.isHidden = false;
		}
		if (this.playerStatsWindow != null)
		{
			this.playerStatsWindow.isHidden = false;
		}
	}

	public void ShowSlowIndication()
	{
		if (this.slowIndicationWindow != null || this.stunIndicationWindow != null)
		{
			return;
		}
		this.slowIndicationWindow = new GuiWindow()
		{
			boundries = new Rect(0f, 0f, 240f, 60f)
		};
		this.slowIndicationWindow.boundries.set_x((float)(Screen.get_width() / 2 - 5));
		this.slowIndicationWindow.boundries.set_y((float)(Screen.get_height() - 155));
		this.slowIndicationWindow.isHidden = false;
		this.slowIndicationWindow.zOrder = 49;
		this.slowIndicationWindow.isClickTransparent = true;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("MainScreenWindow", "slow");
		guiTexture.X = 0f;
		guiTexture.Y = 0f;
		this.slowIndicationWindow.AddGuiElement(guiTexture);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(55f, 29f, 176f, 20f),
			Alignment = 3,
			text = StaticData.Translate("key_notification_slowed"),
			FontSize = 16,
			Font = GuiLabel.FontBold,
			TextColor = Color.get_black()
		};
		this.slowIndicationWindow.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(54f, 28f, 176f, 20f),
			Alignment = 3,
			text = StaticData.Translate("key_notification_slowed"),
			FontSize = 16,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.goldColor
		};
		this.slowIndicationWindow.AddGuiElement(guiLabel1);
		AndromedaGui.gui.AddWindow(this.slowIndicationWindow);
	}

	private void ShowStoryQuestTracker(object p)
	{
		if (this.storyTrackerWindow == null)
		{
			return;
		}
		float _width = this.storyTrackerWindow.boundries.get_width() / MainScreenWindow.animationTime;
		if (this.storyTrackerWindow.boundries.get_x() > (float)Screen.get_width() - this.storyTrackerWindow.boundries.get_width())
		{
			ref Rect rectPointer = ref this.storyTrackerWindow.boundries;
			rectPointer.set_x(rectPointer.get_x() - Time.get_deltaTime() * _width);
		}
		else
		{
			this.storyTrackerWindow.boundries.set_x((float)Screen.get_width() - this.storyTrackerWindow.boundries.get_width());
			this.storyTrackerWindow.preDrawHandler = null;
		}
	}

	public void ShowStunIndication()
	{
		if (this.stunIndicationWindow != null)
		{
			return;
		}
		this.RemoveSlowIndication();
		this.stunIndicationWindow = new GuiWindow()
		{
			boundries = new Rect(0f, 0f, 240f, 60f)
		};
		this.stunIndicationWindow.boundries.set_x((float)(Screen.get_width() / 2 - 5));
		this.stunIndicationWindow.boundries.set_y((float)(Screen.get_height() - 155));
		this.stunIndicationWindow.isHidden = false;
		this.stunIndicationWindow.zOrder = 49;
		this.stunIndicationWindow.isClickTransparent = true;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("MainScreenWindow", "stun");
		guiTexture.X = 0f;
		guiTexture.Y = 0f;
		this.stunIndicationWindow.AddGuiElement(guiTexture);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(55f, 29f, 176f, 20f),
			Alignment = 3,
			text = StaticData.Translate("key_notification_stunned"),
			FontSize = 16,
			Font = GuiLabel.FontBold,
			TextColor = Color.get_black()
		};
		this.stunIndicationWindow.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(54f, 28f, 176f, 20f),
			Alignment = 3,
			text = StaticData.Translate("key_notification_stunned"),
			FontSize = 16,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.redColor
		};
		this.stunIndicationWindow.AddGuiElement(guiLabel1);
		AndromedaGui.gui.AddWindow(this.stunIndicationWindow);
		if (this.quickSlotsWindow == null)
		{
			return;
		}
		for (int i = 0; i < 6; i++)
		{
			if (NetworkScript.player.cfg.weaponSlots[i].get_WeaponStatus() != null && NetworkScript.player.cfg.weaponSlots[i].get_WeaponStatus() != 1)
			{
				GuiTexture guiTexture1 = new GuiTexture();
				guiTexture1.boundries.set_x((float)(45 + i * 34));
				guiTexture1.boundries.set_y(0f);
				guiTexture1.SetTexture("MainScreenWindow", "weapons_slot_stunned");
				this.quickSlotsWindow.AddGuiElement(guiTexture1);
				this.stunElementsForDelete.Add(guiTexture1);
			}
		}
		for (int j = 0; j < 9; j++)
		{
			if (NetworkScript.player.playerBelongings.skillConfig.skillSlots.ContainsKey(j))
			{
				if ((ushort)NetworkScript.player.playerBelongings.skillConfig.skillSlots.get_Item(j).skillId != PlayerItems.TypeTalentsUnstoppable)
				{
					GuiTexture guiTexture2 = new GuiTexture();
					guiTexture2.boundries.set_x((float)(68 + j * 40));
					guiTexture2.boundries.set_y(54f);
					guiTexture2.SetTexture("MainScreenWindow", "skills_box_stunned");
					this.quickSlotsWindow.AddGuiElement(guiTexture2);
					this.stunElementsForDelete.Add(guiTexture2);
				}
			}
		}
	}

	public void ShowWindow()
	{
		if (MainScreenWindow.playerInfoWindow != null)
		{
			MainScreenWindow.playerInfoWindow.isHidden = false;
		}
		if (this.quickSlotsWindow != null)
		{
			this.quickSlotsWindow.isHidden = false;
		}
		if (this.sideMenuWindow != null)
		{
			this.sideMenuWindow.isHidden = false;
		}
	}

	private void SideMenuMenager()
	{
		Vector3 _mousePosition = Input.get_mousePosition();
		float _height = (float)Screen.get_height() - _mousePosition.y;
		float single = _mousePosition.x;
		bool flag = false;
		if (this.sideMenuWindow.boundries.get_x() >= (float)Screen.get_width())
		{
			flag = (this.sideMenuOpenWindow.boundries.Contains(new Vector2(single, _height)) ? true : single > (float)(Screen.get_width() - 3));
		}
		else
		{
			flag = (this.sideMenuOpenWindow.boundries.Contains(new Vector2(single, _height)) || this.sideMenuWindow.boundries.Contains(new Vector2(single, _height)) ? true : single > (float)(Screen.get_width() - 3));
		}
		if (this.btnLevelUpAnimation == null && this.btnChatAnimation == null && this.btnTransformerAnimation == null && !this.hasGuildInvintation)
		{
			if (this.btnSideMenuAnimation != null)
			{
				this.StopSideMenuAnimation();
			}
		}
		else if (this.btnSideMenuAnimation == null)
		{
			this.StartSideMenuAnimation();
		}
		if (this.lastState == flag)
		{
			return;
		}
		this.lastState = flag;
		if (!this.lastState)
		{
			this.sideMenuWindow.preDrawHandler = new Action<object>(this, MainScreenWindow.HideMenu);
		}
		else
		{
			this.sideMenuWindow.preDrawHandler = new Action<object>(this, MainScreenWindow.ShowMenu);
		}
	}

	private void SideMenuMousePosition(object prm)
	{
		if (Input.get_mousePosition().x >= (float)(Screen.get_width() - 2))
		{
			this.OpenMainMenu(prm);
		}
		if (this.btnLevelUpAnimation == null && this.btnChatAnimation == null && this.btnTransformerAnimation == null && !this.hasGuildInvintation)
		{
			if (this.btnSideMenuAnimation != null)
			{
				this.StopSideMenuAnimation();
			}
		}
		else if (this.btnSideMenuAnimation == null)
		{
			this.StartSideMenuAnimation();
		}
	}

	private GuiWindow SmartTooltipGuild(object p)
	{
		if (NetworkScript.player.playerBelongings.playerLevel >= 9)
		{
			return this.CreateTooltipGuild(null);
		}
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = string.Concat(this.kb.GetCommandTooltip(KeyboardCommand.Guild), "\n", string.Format(StaticData.Translate("key_main_screen_btn_low_level"), 9)),
			customData2 = this.btnGuild
		};
		return UniversalTooltip.CreateTooltip(eventHandlerParam);
	}

	private GuiWindow SmartTooltipPlayerProfile(object p)
	{
		return this.CreateTooltipPlayerProfile(null);
	}

	private GuiWindow SmartTooltipPVP(object p)
	{
		if (NetworkScript.player.playerBelongings.playerLevel >= 8)
		{
			return this.CreateTooltipPVP(null);
		}
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = string.Concat(this.kb.GetCommandTooltip(KeyboardCommand.PvP), "\n", string.Format(StaticData.Translate("key_main_screen_btn_low_level"), 8)),
			customData2 = this.btnPVP
		};
		return UniversalTooltip.CreateTooltip(eventHandlerParam);
	}

	private GuiWindow SmartTooltipRanking(object p)
	{
		return this.CreateTooltipRanking(null);
	}

	private GuiWindow SmartTooltipSendGifts(object p)
	{
		if (NetworkScript.player.playerBelongings.playerLevel >= 10)
		{
			return this.CreateTooltipSendGifts(null);
		}
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = string.Concat(this.kb.GetCommandTooltip(KeyboardCommand.Gifts), "\n", string.Format(StaticData.Translate("key_main_screen_btn_low_level"), 10)),
			customData2 = this.btnSendGift
		};
		return UniversalTooltip.CreateTooltip(eventHandlerParam);
	}

	private GuiWindow SmartTooltipTransformer(object p)
	{
		if (NetworkScript.player.playerBelongings.playerLevel >= 30)
		{
			return this.CreateTooltipTransformer(null);
		}
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = string.Concat(this.kb.GetCommandTooltip(KeyboardCommand.Transformer), "\n", string.Format(StaticData.Translate("key_main_screen_btn_low_level"), 30)),
			customData2 = this.btnTransformer
		};
		return UniversalTooltip.CreateTooltip(eventHandlerParam);
	}

	private GuiWindow SmartTooltipUniverseMap(object p)
	{
		if (NetworkScript.player.playerBelongings.playerLevel >= 7)
		{
			return this.CreateTooltipUniverseMap(null);
		}
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = string.Concat(this.kb.GetCommandTooltip(KeyboardCommand.UniverseMap), "\n", string.Format(StaticData.Translate("key_main_screen_btn_low_level"), 7)),
			customData2 = this.btnUniversMap
		};
		return UniversalTooltip.CreateTooltip(eventHandlerParam);
	}

	public void StartCargoFullAnimation()
	{
		if (this.cargoFullAnimation != null)
		{
			return;
		}
		this.cargoFullAnimation = new GuiTextureAnimated()
		{
			isHoverAware = true
		};
		this.cargoFullAnimation.Init("CargoAnimation", "CargoAnimation", "CargoAnimation/CargoBar_01");
		this.cargoFullAnimation.X = 70f;
		this.cargoFullAnimation.Y = 39f;
		this.cargoFullAnimation.rotationTime = 1f;
		MainScreenWindow.playerInfoWindow.AddGuiElement(this.cargoFullAnimation);
	}

	public void StartChatAnimation()
	{
		if (this.btnChatAnimation != null)
		{
			return;
		}
		this.btnChatAnimation = new GuiTextureAnimated()
		{
			isHoverAware = true
		};
		this.btnChatAnimation.Init("ChatAnimation", "ChatAnimation", "ChatAnimation/chat_01");
		this.btnChatAnimation.X = 94f;
		this.btnChatAnimation.Y = 296f;
		this.btnChatAnimation.rotationTime = 1f;
		if (this.sideMenuWindow != null)
		{
			this.sideMenuWindow.AddGuiElement(this.btnChatAnimation);
			this.btnChat.SetTextureNormal("FrameworkGUI", "empty");
			this.btnChat.SetTextureHover("NewGUI", "ChatAnimationHover");
		}
	}

	public void StartGameSearchingAnimation()
	{
		if (this.btnAnimation != null)
		{
			AndromedaGui.mainWnd.sideMenuWindow.RemoveGuiElement(this.btnAnimation);
		}
		this.btnAnimation = new GuiTextureAnimated();
		this.btnAnimation.Init("PVPsearching", "PVPsearching", "PVPsearching/pvpLoad_001");
		this.btnAnimation.X = 29f;
		this.btnAnimation.Y = 296f;
		this.btnAnimation.rotationTime = 1f;
		this.sideMenuWindow.AddGuiElement(this.btnAnimation);
		this.btnPVP.SetTextureNormal("NewGUI", "socialTransperant");
		this.btnPVP.SetTextureHover("NewGUI", "waitingForArenaGameHover");
	}

	public void StartLevelUpAnimation()
	{
		if (this.btnLevelUpAnimation != null)
		{
			return;
		}
		this.btnLevelUpAnimation = new GuiTextureAnimated()
		{
			isHoverAware = true
		};
		this.btnLevelUpAnimation.Init("LevelUpAnimation", "LevelUpAnimation", "LevelUpAnimation/levelUp_01");
		this.btnLevelUpAnimation.X = 94f;
		this.btnLevelUpAnimation.Y = 160f;
		this.btnLevelUpAnimation.rotationTime = 1f;
		if (this.sideMenuWindow != null)
		{
			this.sideMenuWindow.AddGuiElement(this.btnLevelUpAnimation);
			this.btnLevelUp.SetTextureNormal("FrameworkGUI", "empty");
			this.btnLevelUp.SetTextureHover("ConfigWnd", "levelUPAnimationHover");
		}
	}

	public void StartSideMenuAnimation()
	{
		if (this.btnSideMenuAnimation != null)
		{
			return;
		}
		this.btnSideMenuAnimation = new GuiTextureAnimated()
		{
			isHoverAware = true
		};
		this.btnSideMenuAnimation.Init("SideMenuAnimation", "SideMenuAnimation", "SideMenuAnimation/sideMenu_01");
		this.btnSideMenuAnimation.X = 0f;
		this.btnSideMenuAnimation.Y = 0f;
		this.btnSideMenuAnimation.rotationTime = 1f;
		if (this.sideMenuOpenWindow != null)
		{
			this.sideMenuOpenWindow.AddGuiElement(this.btnSideMenuAnimation);
			this.btnOpenMenu.SetTextureNormal("FrameworkGUI", "empty");
		}
	}

	public void StartTransformerAnimation()
	{
		if (this.btnTransformerAnimation != null || this.btnTransformer == null || !this.btnTransformer.isEnabled)
		{
			return;
		}
		this.btnTransformerAnimation = new GuiTextureAnimated()
		{
			isHoverAware = true
		};
		this.btnTransformerAnimation.Init("TransformerNotification", "TransformerNotification", "TransformerNotification/TransformerButton_01");
		this.btnTransformerAnimation.X = 29f;
		this.btnTransformerAnimation.Y = 342f;
		this.btnTransformerAnimation.rotationTime = 1f;
		this.sideMenuWindow.AddGuiElement(this.btnTransformerAnimation);
		this.btnTransformer.SetTextureNormal("FrameworkGUI", "empty");
		this.btnTransformer.SetTextureHover("ConfigWnd", "TransformerAnimationHover");
	}

	public void StopCargoFullAnimation()
	{
		if (this.cargoFullAnimation == null)
		{
			return;
		}
		MainScreenWindow.playerInfoWindow.RemoveGuiElement(this.cargoFullAnimation);
		this.cargoFullAnimation = null;
	}

	public void StopChatAnimation()
	{
		if (this.btnChatAnimation == null)
		{
			return;
		}
		this.btnChat.SetTexture("MainScreenWindow", "chatLogNml");
		this.btnChat.SetTextureHover("MainScreenWindow", "chatLogHvr");
		this.sideMenuWindow.RemoveGuiElement(this.btnChatAnimation);
		this.btnChatAnimation = null;
	}

	public void StopGameSearchingAnimation()
	{
		if (this.btnAnimation == null)
		{
			return;
		}
		this.sideMenuWindow.RemoveGuiElement(this.btnAnimation);
		this.btnPVP.SetTextureNormal("MainScreenWindow", "pvpNml");
		this.btnPVP.SetTextureHover("MainScreenWindow", "pvpHvr");
	}

	public void StopLevelUpAnimation()
	{
		if (this.btnLevelUpAnimation == null)
		{
			return;
		}
		this.btnLevelUp.SetTexture("MainScreenWindow", "levelUp");
		this.sideMenuWindow.RemoveGuiElement(this.btnLevelUpAnimation);
		this.btnLevelUpAnimation = null;
	}

	public void StopSideMenuAnimation()
	{
		if (this.btnSideMenuAnimation == null)
		{
			return;
		}
		this.btnOpenMenu.SetTexture("MainScreenWindow", "showMenu");
		this.sideMenuOpenWindow.RemoveGuiElement(this.btnSideMenuAnimation);
		this.btnSideMenuAnimation = null;
	}

	public void StopTransformerAnimation()
	{
		if (this.btnTransformerAnimation == null || this.btnTransformer == null)
		{
			return;
		}
		this.btnTransformer.SetTexture("MainScreenWindow", "transformer");
		this.sideMenuWindow.RemoveGuiElement(this.btnTransformerAnimation);
		this.btnTransformerAnimation = null;
	}

	public void TakeScreenShot(EventHandlerParam prm)
	{
		if (NetworkScript.player.shipScript != null && !(this.activeWindow is FeedbackWindow))
		{
			NetworkScript.player.shipScript.StartCoroutine(this.TakeScreenshotToTexture());
		}
	}

	[DebuggerHidden]
	private IEnumerator TakeScreenshotToTexture()
	{
		MainScreenWindow.<TakeScreenshotToTexture>c__IteratorF variable = null;
		return variable;
	}

	private void TurningOnOffWeapon(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void MainScreenWindow::TurningOnOffWeapon(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void TurningOnOffWeapon(EventHandlerParam)
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

	public void UnmuteItemTrackers()
	{
		if (this.novaItemTracker != null)
		{
			this.novaItemTracker.muteSoundForNextChange = false;
		}
		if (this.cashItemTracker != null)
		{
			this.cashItemTracker.muteSoundForNextChange = false;
		}
		if (this.viralItemTracker != null)
		{
			this.viralItemTracker.muteSoundForNextChange = false;
		}
		if (this.ultralibriumItemTracker != null)
		{
			this.ultralibriumItemTracker.muteSoundForNextChange = false;
		}
	}
}