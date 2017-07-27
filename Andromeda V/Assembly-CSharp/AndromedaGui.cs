using System;
using UnityEngine;

public class AndromedaGui
{
	private const float MOBILE_DEVICE_UPDATE_INTERVAL = 0.2f;

	public static GuiFramework gui;

	public static GuiWindow contentMainMenuWnd;

	public static GuiWindow errorWnd;

	private static GuiItemTracker lblCash;

	private static GuiTexture txCash;

	private static GuiItemTracker lblNova;

	private static GuiButtonFixed btnLogOut;

	public static int currentPlayerShipIndex;

	public static GuiTexture bottomTexture;

	public static byte sectionShips;

	public static byte sectionPilot;

	public static byte sectionShipsGenerators;

	public static byte sectionTeam;

	public static Action AfterCreateNavigation;

	private static GameObject minimapGO;

	public static MainScreenWindow mainWnd;

	public static PartyWindow personalStatsWnd;

	public static GuiWindow inBaseActiveWnd;

	public static GuiWindow galaxyJumpWnd;

	private static float deltaTime;

	public static GuiLabel lblAriaText;

	public static GuiButtonResizeable btnNextSubStep;

	private static GuiTexture txMackaBG;

	private static GuiTexture txMacka;

	private static GuiButtonResizeable btnTutorialNext;

	private static GuiButtonResizeable btnSelectShip;

	private static GuiLabel lblWeaponsCount;

	private static GuiLabel lblGeneratorsCount;

	private static GuiLabel lblExtrasCount;

	private static GuiDotBar dotBarWeapons;

	private static GuiDotBar dotBarGenerators;

	private static GuiDotBar dotBarExtras;

	private static GuiLabel lblShieldAmount;

	private static GuiLabel lblShieldAmountBonus;

	private static GuiBar shieldBar;

	private static GuiBar hullBar;

	private static GuiBar speedBar;

	private static GuiBar cargoBar;

	private static GuiLabel lblSpeedAmount;

	private static GuiLabel lblSpeedAmountBonus;

	private static GuiLabel lblCargoAmount;

	private static GuiLabel lblCargoAmountBonus;

	private static GuiLabel lblRepair;

	public static GuiButtonResizeable buyNewShipBtn;

	public static GuiButtonResizeable shipDesignBtn;

	public static GuiButtonFixed btnShipsOverview;

	public static GuiButtonFixed btnShipsShips;

	public static GuiButtonFixed btnShipsWeapons;

	public static GuiButtonFixed btnShipsAmmo;

	public static GuiButtonFixed btnShipsGenerators;

	public static GuiButtonFixed btnShipsGeneratorsShields;

	public static GuiButtonFixed btnShipsGeneratorsCorpus;

	public static GuiButtonFixed btnShipsGeneratorsEngines;

	public static GuiButtonFixed btnShipsExtras;

	public static GuiButtonFixed btnShipsBoosters;

	public static Texture2D progressBarRed;

	public static Texture2D progressBarYellow;

	public static GuiButtonResizeable btnNewShipOk;

	public static GuiButtonResizeable btnNewShipCancel;

	public static GuiLabel lblNewShipConfirmText;

	private static GuiWindow debugConsole;

	public static GuiLabel lblDebugText;

	public static bool isDebugConsoleOn;

	static AndromedaGui()
	{
		AndromedaGui.currentPlayerShipIndex = -1;
		AndromedaGui.deltaTime = 0.2f;
	}

	private AndromedaGui()
	{
	}

	public static void ContentMainMenuWndHidden()
	{
		AndromedaGui.contentMainMenuWnd.isHidden = true;
	}

	public static void CreateErrorWindow(string errorText)
	{
		if (AndromedaGui.errorWnd == null)
		{
			AndromedaGui.errorWnd = new GuiWindow();
		}
		else
		{
			AndromedaGui.errorWnd.Clear();
		}
		AndromedaGui.errorWnd.SetBackgroundTexture("FrameworkGUI", "menugui_dialog");
		AndromedaGui.errorWnd.PutToCenter();
		AndromedaGui.errorWnd.boundries.set_y(130f);
		AndromedaGui.errorWnd.isHidden = false;
		AndromedaGui.errorWnd.zOrder = 250;
		AndromedaGui.gui.AddWindow(AndromedaGui.errorWnd);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(65f, 66f, 389f, 90f),
			text = errorText,
			FontSize = 18,
			TextColor = GuiNewStyleBar.redColor,
			Alignment = 4
		};
		AndromedaGui.errorWnd.AddGuiElement(guiLabel);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable()
		{
			Width = 100f,
			Caption = StaticData.Translate("key_login_dialog_ok"),
			FontSize = 12,
			Alignment = 4,
			X = 210f,
			Y = 160f,
			Clicked = new Action<EventHandlerParam>(null, AndromedaGui.OnErrorOkClicked)
		};
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = AndromedaGui.errorWnd.handler
		};
		guiButtonResizeable.eventHandlerParam = eventHandlerParam;
		AndromedaGui.errorWnd.AddGuiElement(guiButtonResizeable);
	}

	public static void CreateGui()
	{
		AndromedaGui.gui = GameObject.Find("GlobalObject").GetComponent<GuiFramework>();
		AndromedaGui.mainWnd = null;
		AndromedaGui.personalStatsWnd = null;
	}

	public static void CreateInBaseStuff()
	{
		if (AndromedaGui.personalStatsWnd == null)
		{
			AndromedaGui.personalStatsWnd = new PartyWindow();
			AndromedaGui.personalStatsWnd.UpdatePartyOffers();
		}
	}

	public static void CreateInSpaceStuff()
	{
		Minimap.Create();
		if (AndromedaGui.personalStatsWnd == null)
		{
			AndromedaGui.personalStatsWnd = new PartyWindow();
			AndromedaGui.personalStatsWnd.CreatePlayerStatsWindow();
			AndromedaGui.personalStatsWnd.UpdatePartyOffers();
		}
		if (AndromedaGui.mainWnd == null)
		{
			AndromedaGui.mainWnd = new MainScreenWindow();
			AndromedaGui.mainWnd.Create();
		}
	}

	private static void CreateWndMainMenu()
	{
		AndromedaGui.contentMainMenuWnd = new GuiWindow();
		AndromedaGui.contentMainMenuWnd.SetBackgroundTexture("GUI", "wndMainContent");
		AndromedaGui.contentMainMenuWnd.PutToHorizontalCenter();
		AndromedaGui.contentMainMenuWnd.boundries.set_y((float)(Screen.get_height() + 130));
		AndromedaGui.contentMainMenuWnd.timeHammerFx = 0.6f;
		AndromedaGui.contentMainMenuWnd.amplitudeHammerShake = 8f;
		AndromedaGui.contentMainMenuWnd.zOrder = 100;
		AndromedaGui.gui.AddWindow(AndromedaGui.contentMainMenuWnd);
		AndromedaGui.DetectSelectedShipIndex();
	}

	private static void DetectSelectedShipIndex()
	{
		if (NetworkScript.player != null)
		{
			int num = NetworkScript.player.playerBelongings.selectedShipId;
			PlayerShipNet[] playerShipNetArray = NetworkScript.player.playerBelongings.playerShips;
			int num1 = 0;
			while (num1 < (int)playerShipNetArray.Length)
			{
				if (playerShipNetArray[num1].ShipID != num)
				{
					num1++;
				}
				else
				{
					AndromedaGui.currentPlayerShipIndex = num1;
					break;
				}
			}
		}
	}

	private static void OnErrorOkClicked(EventHandlerParam prm)
	{
		int num = (int)prm.customData;
		if (AndromedaGui.errorWnd != null)
		{
			AndromedaGui.errorWnd.Clear();
			AndromedaGui.errorWnd = null;
		}
		AndromedaGui.gui.RemoveWindow(num);
	}

	public static void PopulatePlayerInfo()
	{
		if (NetworkScript.player == null || AndromedaGui.mainWnd == null || AndromedaGui.personalStatsWnd == null)
		{
			return;
		}
		AndromedaGui.personalStatsWnd.UpdatePersonalStats();
	}

	public static void PopulateTargetInfo()
	{
		TargetingWnd.PopulateData();
	}

	public static void ReorderGUI()
	{
		TargetingWnd.ReOrderGui();
		if (AndromedaGui.mainWnd != null)
		{
			AndromedaGui.mainWnd.ReOrderGui();
		}
		if (AndromedaGui.personalStatsWnd != null)
		{
			AndromedaGui.personalStatsWnd.ReOrderPartyInvitationGui();
		}
		InBaseScript.ReorderGui();
		Minimap.ReorderGui();
		QuestInfoWindow.ReorderGui();
		QuestTrackerWindow.ReorderGui();
	}

	public static void SetFonts(AssetManager am)
	{
		GuiLabel.FontMedium = (Font)Resources.Load(GuiLabel.GetFontName(GuiLabelFontType.Medium), typeof(Font));
		GuiLabel.FontBold = (Font)Resources.Load(GuiLabel.GetFontName(GuiLabelFontType.Bold), typeof(Font));
		GuiLabel.FontLarge = (Font)Resources.Load(GuiLabel.GetFontName(GuiLabelFontType.Large), typeof(Font));
	}

	public static void ToggleDebugConsole()
	{
		AndromedaGui.isDebugConsoleOn = !AndromedaGui.isDebugConsoleOn;
		if (!AndromedaGui.isDebugConsoleOn)
		{
			AndromedaGui.gui.RemoveWindow(AndromedaGui.debugConsole.handler);
			AndromedaGui.debugConsole.RemoveGuiElement(AndromedaGui.lblDebugText);
			AndromedaGui.debugConsole = null;
			AndromedaGui.lblDebugText = null;
		}
		else
		{
			AndromedaGui.debugConsole = new GuiWindow()
			{
				boundries = new Rect(0f, 0f, 300f, 500f)
			};
			AndromedaGui.lblDebugText = new GuiLabel()
			{
				boundries = new Rect(0f, -500f, 300f, 1000f),
				Alignment = 6,
				FontSize = 8,
				Font = null
			};
			AndromedaGui.debugConsole.AddGuiElement(AndromedaGui.lblDebugText);
			AndromedaGui.gui.AddWindow(AndromedaGui.debugConsole);
			AndromedaGui.debugConsole.isHidden = false;
		}
	}

	public static void TurnOnMenuAfterDeath()
	{
		AndromedaGui.sectionShips = 0;
		AndromedaGui.DetectSelectedShipIndex();
		AndromedaGui.btnShipsOverview.IsClicked = true;
	}
}