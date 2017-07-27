using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TransferableObjects;
using UnityEngine;

public class InBaseScript : MonoBehaviour
{
	private const float shipSecondsToRotate = 40f;

	public static GuiWindow activeWindow;

	private static GuiWindow currencyWindow;

	private static GuiWindow controlPanelWindow;

	private static GuiWindow feedbackButtonWindow;

	private byte activWindowIndex = 255;

	private bool hasMenuOpen;

	private Type[] windowTypes = new Type[] { typeof(__MerchantWindow), typeof(__DockWindow), typeof(__VaultWindow), typeof(NovaShop), typeof(__InBaseResourceTrader), typeof(FeedbackWindow), typeof(GetFreeNovaWindow) };

	private Animation[] novaShopAnims;

	private bool isNovaShopAnimLoaded;

	private bool isMouseOverNovaShop;

	private bool isNovaShopPalying;

	private Animation[] dockAnims;

	private bool isDockAnimLoaded;

	private bool isMouseOverDock;

	private bool isDockPalying;

	private Animation[] merchantAnims;

	private bool isMerchantAnimLoaded;

	private bool isMouseOverMerchant;

	private bool isMerchantPalying;

	private Animation[] vaultAnims;

	private bool isVaultAnimLoaded;

	private bool isMouseOverVault;

	private bool isVaultPalying;

	private Animation[] sellResAnims;

	private bool isSellResAnimLoaded;

	private bool isMouseOverSellRes;

	private bool isSellResPalying;

	private bool isEnterInDock;

	private bool isInDock;

	public static bool isUILoaded;

	private bool isAllAnimationLoaded;

	private Animation[] innerDockAnims;

	private bool isInnerDockAnimLoaded;

	private bool isMouseOverInnerDock;

	private bool isInnerDockPalying;

	private GameObject dockText2GO;

	private GameObject labelDock2GO;

	private GuiButton btnExit;

	private static Color lblWhiteColor;

	private bool isRepairCheckDone;

	private GuiTexture backBtnTexture;

	private GuiButton backBtn;

	private bool levelRestrictionApplied;

	private float initialAnimationTime = 3f;

	private bool isMobile;

	private KeyboardShortcuts kb;

	private GuiItemTracker novaItemTracker;

	private GuiItemTracker cashItemTracker;

	private GuiItemTracker viralItemTracker;

	private GuiItemTracker ultralibriumItemTracker;

	public Texture2D feedbackTexture;

	private Texture2D fadeOutTexture;

	private Color fadeColor;

	private Vector3 dockDoorPosition = Vector3.get_zero();

	private float shipCurrentRotation;

	private GameObject ship;

	private float oldMousePosition;

	private float xDeltaPosition;

	private float currentMousePosition;

	private float mouseScroll;

	private float cameraOffset;

	private Vector3 initialCameraPosition = new Vector3(-7.356002f, 4.401086f, -20.38148f);

	private bool isMovingTowardInitialPosition;

	private float tapDownTimer;

	private float moveCameraStep;

	private float moveCameraTime = 0.5f;

	private bool isFingerMoved;

	private bool isGuiMatter;

	public static ShipInBaseData[] shipLocations;

	static InBaseScript()
	{
		InBaseScript.activeWindow = null;
		InBaseScript.currencyWindow = null;
		InBaseScript.controlPanelWindow = null;
		InBaseScript.feedbackButtonWindow = null;
		InBaseScript.isUILoaded = false;
		InBaseScript.lblWhiteColor = new Color(0.6549f, 0.8862f, 0.9333f);
		ShipInBaseData[] shipInBaseDataArray = new ShipInBaseData[16];
		ShipInBaseData shipInBaseDatum = new ShipInBaseData()
		{
			x = -8.87f,
			y = -7f,
			z = -47.2f,
			scale = 17f,
			assetName = "ShipDamageT1"
		};
		shipInBaseDataArray[0] = shipInBaseDatum;
		shipInBaseDatum = new ShipInBaseData()
		{
			x = -7.91f,
			y = -7f,
			z = -45.5f,
			scale = 19.5f,
			assetName = "ShipDamageT2"
		};
		shipInBaseDataArray[1] = shipInBaseDatum;
		shipInBaseDatum = new ShipInBaseData()
		{
			x = -8.11f,
			y = -5.2f,
			z = -46.8f,
			scale = 0.45f,
			assetName = "ShipDamageT3"
		};
		shipInBaseDataArray[2] = shipInBaseDatum;
		shipInBaseDatum = new ShipInBaseData()
		{
			x = -7.3f,
			y = -6.3f,
			z = -46.5f,
			scale = 0.5f,
			assetName = "ShipDamageT4"
		};
		shipInBaseDataArray[3] = shipInBaseDatum;
		shipInBaseDatum = new ShipInBaseData()
		{
			x = -8.5f,
			y = -7f,
			z = -47f,
			scale = 25f,
			assetName = "ShipDamageT5"
		};
		shipInBaseDataArray[4] = shipInBaseDatum;
		shipInBaseDatum = new ShipInBaseData()
		{
			x = -7.64f,
			y = -7f,
			z = -46f,
			scale = 3f,
			assetName = "ShipSupportT1"
		};
		shipInBaseDataArray[5] = shipInBaseDatum;
		shipInBaseDatum = new ShipInBaseData()
		{
			x = -7.45f,
			y = -6f,
			z = -46f,
			scale = 2.7f,
			assetName = "ShipSupportT2"
		};
		shipInBaseDataArray[6] = shipInBaseDatum;
		shipInBaseDatum = new ShipInBaseData()
		{
			x = -8.5f,
			y = -6f,
			z = -47.7f,
			scale = 1.95f,
			assetName = "ShipSupportT3"
		};
		shipInBaseDataArray[7] = shipInBaseDatum;
		shipInBaseDatum = new ShipInBaseData()
		{
			x = -8f,
			y = -4.5f,
			z = -46f,
			scale = 0.75f,
			assetName = "ShipSupportT4"
		};
		shipInBaseDataArray[8] = shipInBaseDatum;
		shipInBaseDatum = new ShipInBaseData()
		{
			x = -8f,
			y = -4.5f,
			z = -45f,
			scale = 1.7f,
			assetName = "ShipSupportT5"
		};
		shipInBaseDataArray[9] = shipInBaseDatum;
		shipInBaseDatum = new ShipInBaseData()
		{
			x = -7.24f,
			y = -6f,
			z = -47f,
			scale = 3f,
			assetName = "ShipTankT1"
		};
		shipInBaseDataArray[10] = shipInBaseDatum;
		shipInBaseDatum = new ShipInBaseData()
		{
			x = -7.75f,
			y = -5f,
			z = -47f,
			scale = 3f,
			assetName = "ShipTankT2"
		};
		shipInBaseDataArray[11] = shipInBaseDatum;
		shipInBaseDatum = new ShipInBaseData()
		{
			x = -7.6f,
			y = -6.5f,
			z = -46.3f,
			scale = 1.2f,
			assetName = "ShipTankT3"
		};
		shipInBaseDataArray[12] = shipInBaseDatum;
		shipInBaseDatum = new ShipInBaseData()
		{
			x = -8f,
			y = -9f,
			z = -46f,
			scale = 1.5f,
			assetName = "ShipTankT4"
		};
		shipInBaseDataArray[13] = shipInBaseDatum;
		shipInBaseDatum = new ShipInBaseData()
		{
			x = -8.8f,
			y = -5.7f,
			z = -45f,
			scale = 19f,
			assetName = "ShipTankT5"
		};
		shipInBaseDataArray[14] = shipInBaseDatum;
		shipInBaseDatum = new ShipInBaseData()
		{
			x = -8.5f,
			y = -6f,
			z = -47f,
			scale = 28f,
			assetName = "ShipDamageT6"
		};
		shipInBaseDataArray[15] = shipInBaseDatum;
		InBaseScript.shipLocations = shipInBaseDataArray;
	}

	public InBaseScript()
	{
	}

	private void AddBackBtn()
	{
		this.btnExit.Caption = StaticData.Translate("key_in_base_back").ToUpper();
		this.btnExit.textColorNormal = GuiNewStyleBar.blueColor;
		this.btnExit.textColorClick = GuiNewStyleBar.blueColor;
		this.btnExit.textColorHover = Color.get_white();
		this.btnExit.FontSize = 14;
		this.btnExit.Clicked = new Action<EventHandlerParam>(this, InBaseScript.ExitFromDock);
	}

	private void AnimationManager()
	{
		if (!this.isAllAnimationLoaded)
		{
			this.InitAnimation();
		}
		if (!this.isMouseOverNovaShop)
		{
			this.StopNovaShopAnimation();
		}
		else
		{
			this.PlayNovaShopAnimation();
		}
		if (!this.isMouseOverDock)
		{
			this.StopDockAnimation();
		}
		else
		{
			this.PlayDockAnimation();
		}
		if (!this.isMouseOverInnerDock)
		{
			this.StopInnerDockAnimation();
		}
		else
		{
			this.PlayInnerDockAnimation();
		}
		if (!this.isMouseOverMerchant)
		{
			this.StopMerchantAnimation();
		}
		else
		{
			this.PlayMerchantAnimation();
		}
		if (!this.isMouseOverVault)
		{
			this.StopVaultAnimation();
		}
		else
		{
			this.PlayVaultAnimation();
		}
		if (!this.isMouseOverSellRes)
		{
			this.StopSellResAnimation();
		}
		else
		{
			this.PlaySellRespAnimation();
		}
		if (this.isEnterInDock && !Camera.get_main().get_animation().get_isPlaying() && !this.hasMenuOpen)
		{
			EventHandlerParam eventHandlerParam = new EventHandlerParam()
			{
				customData = (byte)1
			};
			this.OnWindowBtnClicked(eventHandlerParam);
			this.isEnterInDock = false;
			this.isInDock = true;
			this.AddBackBtn();
		}
	}

	private void CloseActiveWindow()
	{
		if (InBaseScript.activeWindow == null)
		{
			this.activWindowIndex = 255;
			return;
		}
		if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
		{
			AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "menu_OpenClose");
			AudioManager.PlayGUISound(fromStaticSet);
		}
		AndromedaGui.gui.RemoveWindow(InBaseScript.activeWindow.handler);
		AndromedaGui.inBaseActiveWnd = null;
		InBaseScript.activeWindow = null;
		this.activWindowIndex = 255;
		this.hasMenuOpen = false;
	}

	private void CreateCloseButton()
	{
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed()
		{
			boundries = new Rect(847f, 12f, 58f, 46f)
		};
		guiButtonFixed.SetTexture("ConfigWindow", "X_btn_");
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, InBaseScript.OnCloseBtnClick);
		guiButtonFixed.isMuted = true;
		if (InBaseScript.activeWindow is __ConfigWindow)
		{
			guiButtonFixed.boundries = new Rect(860f, 12f, 58f, 46f);
		}
		else if (InBaseScript.activeWindow is __MerchantWindow)
		{
			guiButtonFixed.boundries = new Rect(950f, 13f, 58f, 46f);
		}
		else if (InBaseScript.activeWindow is FeedbackWindow)
		{
			((FeedbackWindow)InBaseScript.activeWindow).btnClose = guiButtonFixed;
		}
		else if (InBaseScript.activeWindow is GetFreeNovaWindow)
		{
			guiButtonFixed.SetTexture("QuestInfoWindow", "buttonClose");
			guiButtonFixed.X = 697f;
			guiButtonFixed.Y = -2f;
		}
		InBaseScript.activeWindow.AddGuiElement(guiButtonFixed);
	}

	private void CreateFeedbackButton()
	{
		if (InBaseScript.currencyWindow == null)
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
			customData = StaticData.Translate("key_feedback_tooltip"),
			customData2 = guiButtonFixed
		};
		drawTooltipWindow.tooltipWindowParam = eventHandlerParam;
		drawTooltipWindow.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		drawTooltipWindow.SetTexture("FrameworkGUI", "btnFeedback");
		drawTooltipWindow.Clicked = new Action<EventHandlerParam>(this, InBaseScript.TakeScreenShot);
		drawTooltipWindow.boundries = new Rect(0f, 0f, 52f, 37f);
		InBaseScript.feedbackButtonWindow = new GuiWindow();
		InBaseScript.feedbackButtonWindow.SetBackgroundTexture("FrameworkGUI", "empty");
		InBaseScript.feedbackButtonWindow.boundries.set_height(drawTooltipWindow.boundries.get_height());
		InBaseScript.feedbackButtonWindow.boundries.set_width(drawTooltipWindow.boundries.get_width());
		InBaseScript.feedbackButtonWindow.boundries.set_y(0f);
		InBaseScript.feedbackButtonWindow.boundries.set_x(InBaseScript.currencyWindow.boundries.get_x() + InBaseScript.currencyWindow.boundries.get_width() + 15f);
		InBaseScript.feedbackButtonWindow.zOrder = 228;
		AndromedaGui.gui.AddWindow(InBaseScript.feedbackButtonWindow);
		InBaseScript.feedbackButtonWindow.isHidden = false;
		InBaseScript.feedbackButtonWindow.AddGuiElement(drawTooltipWindow);
	}

	private void CreateUI()
	{
		InBaseScript.isUILoaded = true;
		InBaseScript.currencyWindow = new GuiWindow();
		InBaseScript.currencyWindow.SetBackgroundTexture("MainScreenWindow", "topGUI");
		InBaseScript.currencyWindow.boundries = new Rect((float)(Screen.get_width() / 2 - 238), 0f, 476f, 42f);
		InBaseScript.currencyWindow.zOrder = 205;
		InBaseScript.currencyWindow.isHidden = false;
		AndromedaGui.gui.AddWindow(InBaseScript.currencyWindow);
		float single = -4f;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("FrameworkGUI", "res_nova");
		guiTexture.X = 23f;
		guiTexture.Y = 11f + single;
		InBaseScript.currencyWindow.AddGuiElement(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("FrameworkGUI", "res_cash");
		guiTexture1.X = 112f;
		guiTexture1.Y = 11f + single;
		InBaseScript.currencyWindow.AddGuiElement(guiTexture1);
		GuiTexture guiTexture2 = new GuiTexture();
		guiTexture2.SetTexture("FrameworkGUI", "res_equilibrium");
		guiTexture2.X = 275f;
		guiTexture2.Y = 12f + single;
		InBaseScript.currencyWindow.AddGuiElement(guiTexture2);
		GuiTexture guiTexture3 = new GuiTexture();
		guiTexture3.SetTexture("FrameworkGUI", "res_ultralibrium");
		guiTexture3.X = 370f;
		guiTexture3.Y = 14f + single;
		InBaseScript.currencyWindow.AddGuiElement(guiTexture3);
		string str = (NetworkScript.player.vessel.fractionId != 1 ? "fraction2Icon" : "fraction1Icon");
		GuiTexture guiTexture4 = new GuiTexture();
		guiTexture4.SetTexture("FrameworkGUI", str);
		guiTexture4.X = 225f;
		guiTexture4.Y = 12f + single;
		InBaseScript.currencyWindow.AddGuiElement(guiTexture4);
		this.novaItemTracker = new GuiItemTracker(PlayerItems.TypeNova)
		{
			boundries = new Rect(45f, 14f + single, 70f, 14f),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			SetColor = GuiNewStyleBar.orangeColor
		};
		InBaseScript.currencyWindow.AddGuiElement(this.novaItemTracker);
		this.cashItemTracker = new GuiItemTracker(PlayerItems.TypeCash)
		{
			boundries = new Rect(132f, 14f + single, 80f, 14f),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			SetColor = GuiNewStyleBar.blueColor
		};
		InBaseScript.currencyWindow.AddGuiElement(this.cashItemTracker);
		this.viralItemTracker = new GuiItemTracker(PlayerItems.TypeEquilibrium)
		{
			boundries = new Rect(295f, 14f + single, 65f, 14f),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			SetColor = GuiNewStyleBar.purpleColor
		};
		InBaseScript.currencyWindow.AddGuiElement(this.viralItemTracker);
		this.ultralibriumItemTracker = new GuiItemTracker(PlayerItems.TypeUltralibrium)
		{
			boundries = new Rect(390f, 14f + single, 80f, 14f),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			SetColor = GuiNewStyleBar.greenColor
		};
		InBaseScript.currencyWindow.AddGuiElement(this.ultralibriumItemTracker);
		InBaseScript.controlPanelWindow = new GuiWindow();
		InBaseScript.controlPanelWindow.SetBackgroundTexture("NewGUI", "BaseUIExit");
		InBaseScript.controlPanelWindow.zOrder = 200;
		InBaseScript.controlPanelWindow.boundries = new Rect(0f, (float)(Screen.get_height() - 95), 180f, 95f);
		InBaseScript.controlPanelWindow.isHidden = false;
		AndromedaGui.gui.AddWindow(InBaseScript.controlPanelWindow);
		this.btnExit = new GuiButton()
		{
			boundries = new Rect(40f, 28f, 138f, 36f),
			Caption = StaticData.Translate("key_in_base_exit").ToUpper(),
			FontSize = 16,
			Alignment = 4,
			textColorNormal = GuiNewStyleBar.orangeColor,
			textColorClick = GuiNewStyleBar.orangeColor,
			textColorHover = Color.get_white(),
			Clicked = new Action<EventHandlerParam>(this, InBaseScript.OnExitBase)
		};
		InBaseScript.controlPanelWindow.AddGuiElement(this.btnExit);
		if (!NetworkScript.player.vessel.isGuest && playWebGame.GAME_TYPE != "ru")
		{
			this.CreateFeedbackButton();
		}
		QuestTrackerWindow.Initialise();
		QuestInfoWindow.Initialise();
		QuestInfoWindow.SetMuteActions(new Action(this, InBaseScript.MuteItemTrackers), new Action(this, InBaseScript.UnmuteItemTrackers));
	}

	private void DrawScreenshotFadeOut()
	{
		if (this.fadeOutTexture == null)
		{
			this.fadeColor = Color.get_white();
			this.fadeOutTexture = new Texture2D(Screen.get_width(), Screen.get_height());
			this.fadeOutTexture.SetPixel(0, 0, this.fadeColor);
			this.fadeOutTexture.Apply();
			GUI.get_skin().get_box().get_normal().set_background(this.fadeOutTexture);
		}
		this.fadeColor.a = this.fadeColor.a - Time.get_deltaTime() / 1.5f;
		GUI.set_color(this.fadeColor);
		GUI.Box(new Rect(0f, 0f, (float)Screen.get_width(), (float)Screen.get_height()), GUIContent.none);
		GUI.set_depth(0);
		if (this.fadeColor.a <= 0f)
		{
			this.fadeColor.a = 1f;
			Object.Destroy(this.fadeOutTexture);
			GUI.get_skin().get_box().get_normal().set_background(null);
			GUI.set_color(new Color(1f, 1f, 1f, 1f));
			GUI.set_depth(1);
			InBaseScript.feedbackButtonWindow.customOnGUIAction = null;
			this.OpenFeedbackWindow();
		}
	}

	private void EnterInDock()
	{
		if (!this.isDockAnimLoaded || this.isEnterInDock || !this.isAllAnimationLoaded)
		{
			return;
		}
		if (this.dockDoorPosition != Vector3.get_zero())
		{
			AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "inbase_EnterDock");
			AudioManager.PlayAudioClip(fromStaticSet, this.dockDoorPosition);
		}
		this.isEnterInDock = true;
		this.cameraOffset = 0f;
		Animation[] animationArray = this.dockAnims;
		for (int i = 0; i < (int)animationArray.Length; i++)
		{
			Animation animation = animationArray[i];
			if (animation.get_name() == "Docks")
			{
				AnimationClip clip = animation.GetClip("DockOpen");
				animation.Play(clip.get_name());
			}
			else if (animation.get_name() != "DoorGlowPfb")
			{
				animation.Stop();
			}
			else
			{
				AnimationClip animationClip = animation.GetClip("GlowOFF");
				animation.Play(animationClip.get_name());
			}
		}
		Camera.get_main().get_animation().get_Item("CameraMovement").set_speed(1f);
		Camera.get_main().get_animation().get_Item("CameraMovement").set_time(0f);
		Camera.get_main().get_animation().Play("CameraMovement");
		this.ShipReset();
	}

	private void ExitFromDock(object prm)
	{
		if (this.hasMenuOpen)
		{
			this.CloseActiveWindow();
		}
		this.cameraOffset = 0f;
		if (Camera.get_main().get_transform().get_position() != this.initialCameraPosition)
		{
			this.isMovingTowardInitialPosition = true;
			this.moveCameraStep = Vector3.Distance(Camera.get_main().get_transform().get_position(), this.initialCameraPosition) / (this.moveCameraTime / Time.get_fixedDeltaTime());
			return;
		}
		if (this.dockDoorPosition != Vector3.get_zero())
		{
			AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "inbase_LeaveDock");
			AudioManager.PlayAudioClip(fromStaticSet, this.dockDoorPosition);
		}
		Camera.get_main().get_animation().get_Item("CameraMovement").set_speed(-1f);
		Camera.get_main().get_animation().get_Item("CameraMovement").set_time(Camera.get_main().get_animation().get_Item("CameraMovement").get_length());
		Camera.get_main().get_animation().Play("CameraMovement");
		this.isInDock = false;
		Animation[] animationArray = this.dockAnims;
		for (int i = 0; i < (int)animationArray.Length; i++)
		{
			Animation animation = animationArray[i];
			if (animation.get_name() == "Docks")
			{
				AnimationClip clip = animation.GetClip("DockClose");
				!(clip == null);
				animation.Play(clip.get_name());
			}
		}
		this.RemoveBackBtn();
		if (this.dockText2GO != null)
		{
			this.dockText2GO.SetActive(false);
			this.isInnerDockPalying = false;
		}
		if (this.labelDock2GO != null)
		{
			this.labelDock2GO.SetActive(false);
		}
	}

	private void InitAnimation()
	{
		if (this.isAllAnimationLoaded)
		{
			return;
		}
		if (!this.isVaultAnimLoaded)
		{
			GameObject gameObject = GameObject.FindWithTag("Vault");
			if (gameObject != null)
			{
				Component[] componentsInChildren = gameObject.GetComponentsInChildren(typeof(Animation));
				this.vaultAnims = new Animation[(int)componentsInChildren.Length];
				for (int i = 0; i < (int)componentsInChildren.Length; i++)
				{
					this.vaultAnims[i] = (Animation)componentsInChildren[i];
				}
				if ((int)this.vaultAnims.Length > 0)
				{
					this.isVaultAnimLoaded = true;
				}
			}
		}
		if (!this.isNovaShopAnimLoaded)
		{
			GameObject gameObject1 = GameObject.FindWithTag("NovaShop");
			if (gameObject1 != null)
			{
				Component[] componentArray = gameObject1.GetComponentsInChildren(typeof(Animation));
				this.novaShopAnims = new Animation[(int)componentArray.Length];
				for (int j = 0; j < (int)componentArray.Length; j++)
				{
					this.novaShopAnims[j] = (Animation)componentArray[j];
				}
				if ((int)this.novaShopAnims.Length > 0)
				{
					this.isNovaShopAnimLoaded = true;
				}
			}
		}
		if (!this.isMerchantAnimLoaded)
		{
			GameObject gameObject2 = GameObject.FindWithTag("Merchant");
			if (gameObject2 != null)
			{
				Component[] componentsInChildren1 = gameObject2.GetComponentsInChildren(typeof(Animation));
				this.merchantAnims = new Animation[(int)componentsInChildren1.Length];
				for (int k = 0; k < (int)componentsInChildren1.Length; k++)
				{
					this.merchantAnims[k] = (Animation)componentsInChildren1[k];
				}
				if ((int)this.merchantAnims.Length > 0)
				{
					this.isMerchantAnimLoaded = true;
				}
			}
		}
		if (!this.isDockAnimLoaded)
		{
			GameObject gameObject3 = GameObject.FindWithTag("Dock");
			if (gameObject3 != null)
			{
				Component[] componentArray1 = gameObject3.GetComponentsInChildren(typeof(Animation));
				this.dockAnims = new Animation[(int)componentArray1.Length];
				for (int l = 0; l < (int)componentArray1.Length; l++)
				{
					this.dockAnims[l] = (Animation)componentArray1[l];
				}
				if ((int)this.dockAnims.Length > 0)
				{
					this.isDockAnimLoaded = true;
				}
			}
		}
		if (!this.isInnerDockAnimLoaded)
		{
			GameObject gameObject4 = GameObject.FindWithTag("DockSlection");
			if (gameObject4 != null)
			{
				Component[] componentsInChildren2 = gameObject4.GetComponentsInChildren(typeof(Animation));
				this.innerDockAnims = new Animation[(int)componentsInChildren2.Length];
				for (int m = 0; m < (int)componentsInChildren2.Length; m++)
				{
					this.innerDockAnims[m] = (Animation)componentsInChildren2[m];
				}
				if ((int)this.innerDockAnims.Length > 0)
				{
					this.isInnerDockAnimLoaded = true;
				}
			}
		}
		if (!this.isSellResAnimLoaded)
		{
			GameObject gameObject5 = GameObject.FindWithTag("SellResources");
			if (gameObject5 != null)
			{
				Component[] componentArray2 = gameObject5.GetComponentsInChildren(typeof(Animation));
				this.sellResAnims = new Animation[(int)componentArray2.Length];
				for (int n = 0; n < (int)componentArray2.Length; n++)
				{
					this.sellResAnims[n] = (Animation)componentArray2[n];
				}
				if ((int)this.sellResAnims.Length > 0)
				{
					this.isSellResAnimLoaded = true;
				}
			}
		}
		if (this.isDockAnimLoaded && this.isVaultAnimLoaded && this.isSellResAnimLoaded && this.isMerchantAnimLoaded && this.isNovaShopAnimLoaded && this.isInnerDockAnimLoaded)
		{
			this.isAllAnimationLoaded = true;
		}
	}

	private void ManageHotKeys()
	{
		EventHandlerParam eventHandlerParam;
		if (this.kb == null)
		{
			return;
		}
		if (this.kb.IsCommandUsed(KeyboardCommand.Chat, 0))
		{
			MainScreenWindow.OnChatClicked(null);
		}
		if (this.isEnterInDock)
		{
			return;
		}
		if (this.kb.IsCommandUsed(KeyboardCommand.Feedback, 0) && InBaseScript.feedbackButtonWindow != null)
		{
			this.TakeScreenShot(null);
		}
		if (Input.GetKeyDown(27))
		{
			if (QuestInfoWindow.Close())
			{
				return;
			}
			if (AndromedaGui.gui.activeToolTipId != -1)
			{
				AndromedaGui.gui.RemoveWindow(AndromedaGui.gui.activeToolTipId);
				AndromedaGui.gui.activeToolTipId = -1;
				if (AndromedaGui.gui.activeTooltipCloseAction != null)
				{
					AndromedaGui.gui.activeTooltipCloseAction.Invoke();
					AndromedaGui.gui.activeTooltipCloseAction = null;
				}
			}
			else if (AndromedaGui.inBaseActiveWnd != null)
			{
				this.OnCloseBtnClick(null);
			}
			else if (!this.isInDock)
			{
				this.OnExitBase(null);
			}
			else
			{
				this.ExitFromDock(null);
			}
		}
		if (this.kb.IsCommandUsed(KeyboardCommand.Dock, 0))
		{
			if (!this.isInDock)
			{
				if (AndromedaGui.inBaseActiveWnd != null)
				{
					this.OnCloseBtnClick(null);
				}
				this.EnterInDock();
			}
			else
			{
				eventHandlerParam = new EventHandlerParam()
				{
					customData = (byte)1
				};
				this.OnWindowBtnClicked(eventHandlerParam);
			}
		}
		if (!this.isInDock)
		{
			if (this.kb.IsCommandUsed(KeyboardCommand.Merchant, 0))
			{
				eventHandlerParam = new EventHandlerParam()
				{
					customData = (byte)0
				};
				this.OnWindowBtnClicked(eventHandlerParam);
			}
			else if (this.kb.IsCommandUsed(KeyboardCommand.NovaShopInBase, 0))
			{
				eventHandlerParam = new EventHandlerParam()
				{
					customData = (byte)3
				};
				this.OnWindowBtnClicked(eventHandlerParam);
			}
			else if (this.kb.IsCommandUsed(KeyboardCommand.SellResources, 0))
			{
				eventHandlerParam = new EventHandlerParam()
				{
					customData = (byte)4
				};
				this.OnWindowBtnClicked(eventHandlerParam);
			}
			else if (this.kb.IsCommandUsed(KeyboardCommand.Vault, 0))
			{
				eventHandlerParam = new EventHandlerParam()
				{
					customData = (byte)2
				};
				this.OnWindowBtnClicked(eventHandlerParam);
			}
		}
		if (this.kb.IsCommandUsed(KeyboardCommand.SwitchQuest, 0))
		{
			QuestTrackerWindow.ScrollQuests();
		}
		if (this.kb.IsCommandUsed(KeyboardCommand.Quests, 0))
		{
			QuestTrackerWindow.SwitchObjectiveInfo();
		}
	}

	private void MouseClickManager()
	{
		RaycastHit raycastHit = new RaycastHit();
		int num = 0;
		EventHandlerParam eventHandlerParam;
		if (this.isEnterInDock)
		{
			return;
		}
		if (AndromedaGui.gui.IIsGuiMatter)
		{
			return;
		}
		if (Physics.Raycast(Camera.get_main().ScreenPointToRay(Input.get_mousePosition()), ref raycastHit, 500f))
		{
			string _tag = raycastHit.get_collider().get_gameObject().get_tag();
			if (_tag != null)
			{
				if (InBaseScript.<>f__switch$map1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(10);
					dictionary.Add("Vault", 0);
					dictionary.Add("VaultText", 0);
					dictionary.Add("NovaShop", 1);
					dictionary.Add("NovaShopText", 1);
					dictionary.Add("Merchant", 2);
					dictionary.Add("MerchantText", 2);
					dictionary.Add("Dock", 3);
					dictionary.Add("DockText", 3);
					dictionary.Add("SellResources", 4);
					dictionary.Add("SellResourcesText", 4);
					InBaseScript.<>f__switch$map1 = dictionary;
				}
				if (InBaseScript.<>f__switch$map1.TryGetValue(_tag, ref num))
				{
					switch (num)
					{
						case 0:
						{
							eventHandlerParam = new EventHandlerParam()
							{
								customData = (byte)2
							};
							this.OnWindowBtnClicked(eventHandlerParam);
							return;
						}
						case 1:
						{
							if (__ChatWindow.wnd == null || !__ChatWindow.wnd.CheckMousePosition())
							{
								eventHandlerParam = new EventHandlerParam()
								{
									customData = (byte)3
								};
								this.OnWindowBtnClicked(eventHandlerParam);
							}
							else
							{
								Debug.Log("Mouse Position is over chat window too so only chat window is managed");
							}
							return;
						}
						case 2:
						{
							if (__ChatWindow.wnd == null || !__ChatWindow.wnd.CheckMousePosition())
							{
								eventHandlerParam = new EventHandlerParam()
								{
									customData = (byte)0
								};
								this.OnWindowBtnClicked(eventHandlerParam);
							}
							else
							{
								Debug.Log("Mouse Position is over chat window too so only chat window is managed");
							}
							return;
						}
						case 3:
						{
							if (!this.isInDock)
							{
								this.EnterInDock();
							}
							return;
						}
						case 4:
						{
							eventHandlerParam = new EventHandlerParam()
							{
								customData = (byte)4
							};
							this.OnWindowBtnClicked(eventHandlerParam);
							return;
						}
					}
				}
			}
		}
	}

	private void MouseOverManager()
	{
		RaycastHit raycastHit = new RaycastHit();
		int num = 0;
		if (this.hasMenuOpen)
		{
			return;
		}
		if (!Physics.Raycast(Camera.get_main().ScreenPointToRay(Input.get_mousePosition()), ref raycastHit, 500f))
		{
			this.isMouseOverNovaShop = false;
			this.isMouseOverVault = false;
			this.isMouseOverDock = false;
			this.isMouseOverMerchant = false;
			this.isMouseOverSellRes = false;
			this.isMouseOverInnerDock = false;
		}
		else
		{
			string _tag = raycastHit.get_collider().get_gameObject().get_tag();
			if (_tag != null)
			{
				if (InBaseScript.<>f__switch$map0 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(12);
					dictionary.Add("Vault", 0);
					dictionary.Add("VaultText", 0);
					dictionary.Add("NovaShop", 1);
					dictionary.Add("NovaShopText", 1);
					dictionary.Add("Merchant", 2);
					dictionary.Add("MerchantText", 2);
					dictionary.Add("Dock", 3);
					dictionary.Add("DockText", 3);
					dictionary.Add("DockSlection", 4);
					dictionary.Add("DockSlectionText", 4);
					dictionary.Add("SellResources", 5);
					dictionary.Add("SellResourcesText", 5);
					InBaseScript.<>f__switch$map0 = dictionary;
				}
				if (InBaseScript.<>f__switch$map0.TryGetValue(_tag, ref num))
				{
					switch (num)
					{
						case 0:
						{
							if (!this.isVaultAnimLoaded)
							{
								this.InitAnimation();
								this.isMouseOverVault = true;
							}
							this.isMouseOverVault = true;
							this.isMouseOverNovaShop = false;
							this.isMouseOverDock = false;
							this.isMouseOverMerchant = false;
							this.isMouseOverSellRes = false;
							this.isMouseOverInnerDock = false;
							break;
						}
						case 1:
						{
							if (!this.isNovaShopAnimLoaded)
							{
								this.InitAnimation();
								this.isMouseOverNovaShop = true;
							}
							this.isMouseOverNovaShop = true;
							this.isMouseOverVault = false;
							this.isMouseOverDock = false;
							this.isMouseOverMerchant = false;
							this.isMouseOverSellRes = false;
							this.isMouseOverInnerDock = false;
							break;
						}
						case 2:
						{
							if (!this.isMerchantAnimLoaded)
							{
								this.InitAnimation();
								this.isMouseOverMerchant = true;
							}
							this.isMouseOverMerchant = true;
							this.isMouseOverVault = false;
							this.isMouseOverNovaShop = false;
							this.isMouseOverDock = false;
							this.isMouseOverSellRes = false;
							this.isMouseOverInnerDock = false;
							break;
						}
						case 3:
						{
							if (!this.isDockAnimLoaded)
							{
								this.InitAnimation();
								this.isMouseOverDock = true;
							}
							this.isMouseOverDock = true;
							this.isMouseOverVault = false;
							this.isMouseOverNovaShop = false;
							this.isMouseOverMerchant = false;
							this.isMouseOverSellRes = false;
							this.isMouseOverInnerDock = false;
							break;
						}
						case 4:
						{
							if (!this.isInnerDockAnimLoaded)
							{
								this.InitAnimation();
								this.isMouseOverInnerDock = true;
							}
							this.isMouseOverInnerDock = true;
							this.isMouseOverDock = false;
							this.isMouseOverVault = false;
							this.isMouseOverNovaShop = false;
							this.isMouseOverMerchant = false;
							this.isMouseOverSellRes = false;
							break;
						}
						case 5:
						{
							if (!this.isSellResAnimLoaded)
							{
								this.InitAnimation();
								this.isMouseOverSellRes = true;
							}
							this.isMouseOverSellRes = true;
							this.isMouseOverNovaShop = false;
							this.isMouseOverVault = false;
							this.isMouseOverDock = false;
							this.isMouseOverMerchant = false;
							this.isMouseOverInnerDock = false;
							break;
						}
					}
				}
			}
		}
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

	private void OnCloseBtnClick(object prm)
	{
		if (this.activWindowIndex == 1)
		{
			if (this.dockText2GO != null)
			{
				this.dockText2GO.SetActive(true);
			}
			if (this.labelDock2GO != null)
			{
				this.labelDock2GO.SetActive(true);
			}
		}
		this.CloseActiveWindow();
	}

	private void OnExitBase(EventHandlerParam parm)
	{
		InBaseScript.<OnExitBase>c__AnonStorey27 variable = null;
		if (NetworkScript.player.vessel.pvpState == 2)
		{
			return;
		}
		if (!this.btnExit.isEnabled)
		{
			return;
		}
		QuestInfoWindow.Close();
		if (QuestInfoWindow.IsOnScreen)
		{
			QuestInfoWindow.Close();
		}
		NetworkScript.isInBase = false;
		short num = playWebGame.authorization.galaxyId;
		playWebGame.LogMixPanel(MixPanelEvents.ExitBase, null);
		playWebGame.udp.ExecuteCommand(22, null);
		playWebGame.LoadScene(Enumerable.First<LevelMap>(Enumerable.Where<LevelMap>(StaticData.allGalaxies, new Func<LevelMap, bool>(variable, (LevelMap g) => g.get_galaxyId() == this.galaxyId))).scenename);
		this.btnExit.isEnabled = false;
	}

	public void OnWindowBtnClicked(EventHandlerParam prm)
	{
		QuestInfoWindow.Close();
		if (prm.customData == null)
		{
			return;
		}
		byte num = (byte)prm.customData;
		if (InBaseScript.activeWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(InBaseScript.activeWindow.handler);
			AndromedaGui.inBaseActiveWnd = null;
			if (num == this.activWindowIndex)
			{
				if (this.activWindowIndex == 1)
				{
					if (this.dockText2GO != null)
					{
						this.dockText2GO.SetActive(true);
					}
					if (this.labelDock2GO != null)
					{
						this.labelDock2GO.SetActive(true);
					}
				}
				InBaseScript.activeWindow = null;
				this.activWindowIndex = 255;
				this.hasMenuOpen = false;
				if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
				{
					AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "menu_OpenClose");
					AudioManager.PlayGUISound(fromStaticSet);
				}
				QuestInfoWindow.ResetActiveWindow();
				return;
			}
		}
		InBaseScript.activeWindow = (GuiWindow)this.windowTypes[num].GetConstructors()[0].Invoke(null);
		InBaseScript.activeWindow.isHidden = true;
		InBaseScript.activeWindow.Create();
		float _y = InBaseScript.activeWindow.boundries.get_y();
		InBaseScript.activeWindow.timeHammerFx = 0.5f;
		InBaseScript.activeWindow.amplitudeHammerShake = 20f;
		InBaseScript.activeWindow.moveToShakeRatio = 0.6f;
		InBaseScript.activeWindow.boundries.set_y(-InBaseScript.activeWindow.boundries.get_height());
		InBaseScript.activeWindow.isHidden = false;
		InBaseScript.activeWindow.StartHammerEffect(InBaseScript.activeWindow.boundries.get_x(), _y);
		this.PlayWindowSound(InBaseScript.activeWindow);
		this.CreateCloseButton();
		AndromedaGui.gui.AddWindow(InBaseScript.activeWindow);
		AndromedaGui.inBaseActiveWnd = InBaseScript.activeWindow;
		this.activWindowIndex = num;
		this.hasMenuOpen = true;
		QuestInfoWindow.SetCloseAciveWindow(new Action(this, InBaseScript.CloseActiveWindow));
	}

	private void OpenDockWindow()
	{
		if (this.tapDownTimer >= 0.4f)
		{
			this.tapDownTimer = 0f;
			return;
		}
		this.tapDownTimer = 0f;
		if (InBaseScript.activeWindow != null)
		{
			return;
		}
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = (byte)1
		};
		this.OnWindowBtnClicked(eventHandlerParam);
	}

	public void OpenFeedbackWindow()
	{
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = (byte)5
		};
		this.OnWindowBtnClicked(eventHandlerParam);
		if (InBaseScript.activeWindow is FeedbackWindow)
		{
			((FeedbackWindow)InBaseScript.activeWindow).PopulateScreenShot(this.feedbackTexture);
		}
	}

	private void PlayDockAnimation()
	{
		if (!this.isDockAnimLoaded || this.isDockPalying || this.isEnterInDock || this.isInDock)
		{
			return;
		}
		Animation[] animationArray = this.dockAnims;
		for (int i = 0; i < (int)animationArray.Length; i++)
		{
			Animation animation = animationArray[i];
			if (animation.get_name() == "labelDock")
			{
				animation.Play();
			}
			else if (animation.get_name() == "DoorGlowPfb")
			{
				AnimationClip clip = animation.GetClip("GlowON");
				animation.Play(clip.get_name());
			}
		}
		this.isDockPalying = true;
	}

	private void PlayInnerDockAnimation()
	{
		if (!this.isInnerDockAnimLoaded || this.isInnerDockPalying || this.dockText2GO == null || !this.dockText2GO.get_activeSelf())
		{
			return;
		}
		Animation[] animationArray = this.innerDockAnims;
		for (int i = 0; i < (int)animationArray.Length; i++)
		{
			Animation animation = animationArray[i];
			if (animation.get_name() == "labelDock2")
			{
				animation.Play();
			}
		}
		this.isInnerDockPalying = true;
	}

	private void PlayMerchantAnimation()
	{
		if (!this.isMerchantAnimLoaded || this.isMerchantPalying)
		{
			return;
		}
		Animation[] animationArray = this.merchantAnims;
		for (int i = 0; i < (int)animationArray.Length; i++)
		{
			Animation animation = animationArray[i];
			if (animation.get_name() == "labelMerchant")
			{
				animation.Play();
			}
			else if (animation.get_name() != "HologramGlow")
			{
				AnimationClip clip = animation.GetClip("HologramAnimation1");
				AnimationClip animationClip = animation.GetClip("HologramAnimation2");
				animation.Play(clip.get_name());
				animation.PlayQueued(animationClip.get_name(), 0);
			}
			else
			{
				AnimationClip clip1 = animation.GetClip("GlowON");
				animation.Play(clip1.get_name());
			}
		}
		this.isMerchantPalying = true;
	}

	private void PlayNovaShopAnimation()
	{
		if (!this.isNovaShopAnimLoaded || this.isNovaShopPalying)
		{
			return;
		}
		Animation[] animationArray = this.novaShopAnims;
		for (int i = 0; i < (int)animationArray.Length; i++)
		{
			Animation animation = animationArray[i];
			if (animation.get_name() != "Glow")
			{
				animation.Play();
			}
			else
			{
				AnimationClip clip = animation.GetClip("GlowON");
				animation.Play(clip.get_name());
			}
		}
		this.isNovaShopPalying = true;
	}

	private void PlaySellRespAnimation()
	{
		if (!this.isSellResAnimLoaded || this.isSellResPalying)
		{
			return;
		}
		Animation[] animationArray = this.sellResAnims;
		for (int i = 0; i < (int)animationArray.Length; i++)
		{
			Animation animation = animationArray[i];
			if (animation.get_name() != "SellResourcesGlow")
			{
				animation.Play();
			}
			else
			{
				AnimationClip clip = animation.GetClip("GlowON");
				animation.Play(clip.get_name());
			}
		}
		this.isSellResPalying = true;
	}

	private void PlayVaultAnimation()
	{
		if (!this.isVaultAnimLoaded || this.isVaultPalying)
		{
			return;
		}
		Animation[] animationArray = this.vaultAnims;
		for (int i = 0; i < (int)animationArray.Length; i++)
		{
			Animation animation = animationArray[i];
			if (animation.get_name() != "labelVault")
			{
				animation.CrossFade(animation.GetClip("LightOn").get_name());
			}
			else
			{
				animation.Play();
			}
		}
		this.isVaultPalying = true;
	}

	private void PlayWindowSound(GuiWindow window)
	{
		AudioClip fromStaticSet = null;
		if (window is __MerchantWindow)
		{
			fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "inbase_Merchant");
		}
		else if (window is __DockWindow && !this.isEnterInDock)
		{
			fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "menu_OpenClose");
		}
		else if (window is __VaultWindow)
		{
			fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "inbase_Vault");
		}
		else if (window is NovaShop)
		{
			fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "inbase_NovaShop");
		}
		else if (window is __InBaseResourceTrader)
		{
			fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "inbase_SellResources");
		}
		if (fromStaticSet == null)
		{
			return;
		}
		AudioManager.PlayGUISound(fromStaticSet);
	}

	private void RemoveBackBtn()
	{
		this.btnExit.Caption = StaticData.Translate("key_in_base_exit").ToUpper();
		this.btnExit.textColorNormal = GuiNewStyleBar.orangeColor;
		this.btnExit.textColorClick = GuiNewStyleBar.orangeColor;
		this.btnExit.textColorHover = Color.get_white();
		this.btnExit.FontSize = 16;
		this.btnExit.Clicked = new Action<EventHandlerParam>(this, InBaseScript.OnExitBase);
	}

	public static void ReorderGui()
	{
		if (InBaseScript.activeWindow != null)
		{
			InBaseScript.activeWindow.PutToCenter();
		}
		if (InBaseScript.currencyWindow != null)
		{
			InBaseScript.currencyWindow.boundries = new Rect((float)(Screen.get_width() / 2 - 238), 0f, 476f, 42f);
		}
		if (InBaseScript.controlPanelWindow != null)
		{
			InBaseScript.controlPanelWindow.boundries = new Rect(0f, (float)(Screen.get_height() - 95), 180f, 95f);
		}
		if (__ChatWindow.wnd != null)
		{
			__ChatWindow.wnd.boundries.set_x((float)Screen.get_width() - __ChatWindow.wnd.boundries.get_width());
			__ChatWindow.wnd.boundries.set_y((float)Screen.get_height() - __ChatWindow.wnd.boundries.get_height() + 2f);
		}
	}

	private void ShipPut()
	{
		// 
		// Current member / type: System.Void InBaseScript::ShipPut()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void ShipPut()
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

	private void ShipRemove()
	{
		Object.Destroy(this.ship);
		this.ship = null;
	}

	public void ShipReset()
	{
		if (this.ship != null)
		{
			this.ShipRemove();
		}
		this.ShipPut();
	}

	private void ShipRotate()
	{
		if (this.isMovingTowardInitialPosition)
		{
			Camera.get_main().get_transform().set_position(Vector3.MoveTowards(Camera.get_main().get_transform().get_position(), this.initialCameraPosition, this.moveCameraStep));
			if (Vector3.Distance(Camera.get_main().get_transform().get_position(), this.initialCameraPosition) <= 0.001f)
			{
				this.isMovingTowardInitialPosition = false;
				Camera.get_main().get_transform().set_position(this.initialCameraPosition);
				this.moveCameraStep = 0f;
				this.ExitFromDock(null);
			}
		}
		if (this.ship == null)
		{
			return;
		}
		if (!this.isInDock)
		{
			return;
		}
		if (Input.GetMouseButtonDown(0))
		{
			if (!AndromedaGui.gui.IIsGuiMatter)
			{
				this.isGuiMatter = false;
			}
			else
			{
				this.isGuiMatter = true;
			}
		}
		if (Input.GetMouseButton(0) && !this.isGuiMatter)
		{
			InBaseScript _deltaTime = this;
			_deltaTime.tapDownTimer = _deltaTime.tapDownTimer + Time.get_deltaTime();
			this.xDeltaPosition = this.oldMousePosition - Input.get_mousePosition().x;
			this.currentMousePosition = Input.get_mousePosition().x;
			if (this.xDeltaPosition > 0f)
			{
				InBaseScript inBaseScript = this;
				inBaseScript.shipCurrentRotation = inBaseScript.shipCurrentRotation + 2f;
			}
			else if (this.xDeltaPosition < 0f)
			{
				InBaseScript inBaseScript1 = this;
				inBaseScript1.shipCurrentRotation = inBaseScript1.shipCurrentRotation - 2f;
			}
			this.oldMousePosition = this.currentMousePosition;
		}
		if (Input.GetMouseButtonUp(0) && !this.isGuiMatter)
		{
			this.OpenDockWindow();
		}
		this.ZoomCamera(Input.GetAxis("Mouse ScrollWheel") * 4f);
		if (!Input.GetMouseButton(0))
		{
			float single = 0.025f * Time.get_deltaTime();
			single = single * 360f;
			InBaseScript inBaseScript2 = this;
			inBaseScript2.shipCurrentRotation = inBaseScript2.shipCurrentRotation + single;
			if (single >= 360f)
			{
				single = single - 360f;
			}
		}
		this.ship.get_transform().set_rotation(Quaternion.Euler(0f, this.shipCurrentRotation, 0f));
	}

	private void Start()
	{
		this.isMobile = false;
		GameObject gameObject = GameObject.Find("GlobalObject");
		this.dockDoorPosition = GameObject.Find("Docks").get_transform().get_position();
		AndromedaGui.gui = gameObject.GetComponent<GuiFramework>();
		this.InitAnimation();
		NetworkScript.isInBase = true;
		InBaseScript.isUILoaded = false;
	}

	private void StopDockAnimation()
	{
		if (!this.isDockPalying || this.isEnterInDock)
		{
			return;
		}
		Animation[] animationArray = this.dockAnims;
		for (int i = 0; i < (int)animationArray.Length; i++)
		{
			Animation animation = animationArray[i];
			if (animation.get_name() == "labelDock")
			{
				animation.Stop();
			}
			else if (animation.get_name() == "DoorGlowPfb")
			{
				AnimationClip clip = animation.GetClip("GlowOFF");
				animation.Play(clip.get_name());
			}
		}
		this.isDockPalying = false;
	}

	private void StopInnerDockAnimation()
	{
		if (!this.isInnerDockPalying)
		{
			return;
		}
		Animation[] animationArray = this.innerDockAnims;
		for (int i = 0; i < (int)animationArray.Length; i++)
		{
			Animation animation = animationArray[i];
			if (animation.get_name() == "labelDock2")
			{
				animation.Stop();
			}
		}
		this.isInnerDockPalying = false;
	}

	private void StopMerchantAnimation()
	{
		if (!this.isMerchantPalying)
		{
			return;
		}
		Animation[] animationArray = this.merchantAnims;
		for (int i = 0; i < (int)animationArray.Length; i++)
		{
			Animation animation = animationArray[i];
			if (animation.get_name() == "labelMerchant")
			{
				animation.Stop();
			}
			else if (animation.get_name() != "HologramGlow")
			{
				animation.CrossFade(animation.GetClip("HologramAnimation3").get_name());
			}
			else
			{
				AnimationClip clip = animation.GetClip("GlowOFF");
				animation.Play(clip.get_name());
			}
		}
		this.isMerchantPalying = false;
	}

	private void StopNovaShopAnimation()
	{
		if (!this.isNovaShopPalying)
		{
			return;
		}
		Animation[] animationArray = this.novaShopAnims;
		for (int i = 0; i < (int)animationArray.Length; i++)
		{
			Animation animation = animationArray[i];
			if (animation.get_name() != "Glow")
			{
				animation.Stop();
			}
			else
			{
				AnimationClip clip = animation.GetClip("GlowOFF");
				animation.Play(clip.get_name());
			}
		}
		this.isNovaShopPalying = false;
	}

	private void StopSellResAnimation()
	{
		if (!this.isSellResPalying)
		{
			return;
		}
		Animation[] animationArray = this.sellResAnims;
		for (int i = 0; i < (int)animationArray.Length; i++)
		{
			Animation animation = animationArray[i];
			if (animation.get_name() != "SellResourcesGlow")
			{
				animation.Stop();
			}
			else
			{
				AnimationClip clip = animation.GetClip("GlowOFF");
				animation.Play(clip.get_name());
			}
		}
		this.isSellResPalying = false;
	}

	private void StopVaultAnimation()
	{
		if (!this.isVaultPalying)
		{
			return;
		}
		Animation[] animationArray = this.vaultAnims;
		for (int i = 0; i < (int)animationArray.Length; i++)
		{
			Animation animation = animationArray[i];
			if (animation.get_name() != "labelVault")
			{
				animation.CrossFade(animation.GetClip("LightOff").get_name());
			}
			else
			{
				animation.Stop();
			}
		}
		this.isVaultPalying = false;
	}

	public void TakeScreenShot(EventHandlerParam prm)
	{
		if (!(InBaseScript.activeWindow is FeedbackWindow))
		{
			base.StartCoroutine(this.TakeScreenshotToTexture());
		}
	}

	[DebuggerHidden]
	private IEnumerator TakeScreenshotToTexture()
	{
		InBaseScript.<TakeScreenshotToTexture>c__Iterator9 variable = null;
		return variable;
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

	private void Update()
	{
		if (NetworkScript.player != null && this.kb == null)
		{
			this.kb = new KeyboardShortcuts();
		}
		playWebGame.UpdateBundleLoad();
		if (!InBaseScript.isUILoaded && NetworkScript.player != null && NetworkScript.buildNeighbourhoodDone)
		{
			this.CreateUI();
			this.UpdateInBaseLbls();
		}
		if (Input.GetMouseButtonDown(0) && !this.hasMenuOpen)
		{
			this.MouseClickManager();
		}
		if (!this.isRepairCheckDone && NetworkScript.player != null && NetworkScript.player.playerBelongings != null)
		{
			if ((double)NetworkScript.player.playerBelongings.get_SelectedShip().CorpusHP < (double)NetworkScript.player.playerBelongings.get_SelectedShip().Corpus * 0.5)
			{
				this.EnterInDock();
			}
			this.isRepairCheckDone = true;
		}
		if (this.isAllAnimationLoaded && NetworkScript.player != null && NetworkScript.player.playerBelongings != null)
		{
			this.AnimationManager();
			this.MouseOverManager();
			this.ManageHotKeys();
		}
		this.ShipRotate();
	}

	private void UpdateInBaseLbls()
	{
		GameObject gameObject = GameObject.Find("SellResourcesText");
		TextMesh component = gameObject.GetComponent<TextMesh>();
		gameObject.GetComponent<MeshRenderer>().get_material().set_color(InBaseScript.lblWhiteColor);
		component.set_text((!this.isMobile ? this.kb.GetCommandTooltip(KeyboardCommand.SellResources).ToUpper() : StaticData.Translate("key_in_base_sell_resources_mobile").ToUpper()));
		component.set_fontSize(50);
		component.set_fontStyle(1);
		gameObject.AddComponent<BoxCollider>();
		GameObject gameObject1 = GameObject.Find("VaultText");
		if (gameObject1 != null)
		{
			component = gameObject1.GetComponent<TextMesh>();
			gameObject1.GetComponent<MeshRenderer>().get_material().set_color(InBaseScript.lblWhiteColor);
			component.set_text((!this.isMobile ? this.kb.GetCommandTooltip(KeyboardCommand.Vault).ToUpper() : StaticData.Translate("key_in_base_vault_mobile").ToUpper()));
			component.set_fontSize(45);
			component.set_fontStyle(1);
			gameObject1.AddComponent<BoxCollider>();
		}
		GameObject gameObject2 = GameObject.Find("DockText");
		if (gameObject2 != null)
		{
			component = gameObject2.GetComponent<TextMesh>();
			gameObject2.GetComponent<MeshRenderer>().get_material().set_color(InBaseScript.lblWhiteColor);
			component.set_text((!this.isMobile ? this.kb.GetCommandTooltip(KeyboardCommand.Dock).ToUpper() : StaticData.Translate("key_in_base_dock_mobile").ToUpper()));
			component.set_fontSize(58);
			component.set_fontStyle(1);
			gameObject2.AddComponent<BoxCollider>();
		}
		GameObject gameObject3 = GameObject.Find("NovaShopText");
		if (gameObject3 != null)
		{
			component = gameObject3.GetComponent<TextMesh>();
			gameObject3.GetComponent<MeshRenderer>().get_material().set_color(InBaseScript.lblWhiteColor);
			component.set_text((!this.isMobile ? this.kb.GetCommandTooltip(KeyboardCommand.NovaShopInBase).ToUpper() : StaticData.Translate("key_in_base_nova_shop_mobile").ToUpper()));
			GameObject gameObject4 = GameObject.Find("Promo");
			if (gameObject4 != null && !playWebGame.authorization.payments_promotion)
			{
				gameObject4.SetActive(false);
			}
			component.set_fontSize(50);
			component.set_fontStyle(1);
			gameObject3.AddComponent<BoxCollider>();
		}
		GameObject gameObject5 = GameObject.Find("MerchantText");
		if (gameObject5 != null)
		{
			component = gameObject5.GetComponent<TextMesh>();
			gameObject5.GetComponent<MeshRenderer>().get_material().set_color(InBaseScript.lblWhiteColor);
			component.set_text((!this.isMobile ? this.kb.GetCommandTooltip(KeyboardCommand.Merchant).ToUpper() : StaticData.Translate("key_in_base_mershant_mobile").ToUpper()));
			component.set_fontSize(36);
			component.set_fontStyle(1);
			gameObject5.AddComponent<BoxCollider>();
		}
		this.dockText2GO = GameObject.Find("DockText2");
		if (this.dockText2GO != null)
		{
			component = this.dockText2GO.GetComponent<TextMesh>();
			this.dockText2GO.GetComponent<MeshRenderer>().get_material().set_color(InBaseScript.lblWhiteColor);
			component.set_text((!this.isMobile ? this.kb.GetCommandTooltip(KeyboardCommand.Dock).ToUpper() : StaticData.Translate("key_in_base_dock_mobile").ToUpper()));
			component.set_fontSize(58);
			component.set_fontStyle(1);
			this.dockText2GO.SetActive(false);
			this.dockText2GO.AddComponent<BoxCollider>();
			this.labelDock2GO = GameObject.Find("labelDock2");
			this.labelDock2GO.SetActive(false);
		}
		if (playWebGame.GAME_TYPE != "ru")
		{
			AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("InBaseAudio", "wellcome_voice");
			AudioManager.PlayGUISound(fromStaticSet);
		}
	}

	private void ZoomCamera(float offset)
	{
		if (offset == 0f)
		{
			return;
		}
		if (offset > 0f && this.cameraOffset >= 12.8f)
		{
			return;
		}
		if (offset < 0f && this.cameraOffset <= -4f)
		{
			return;
		}
		Camera.get_main().get_transform().Translate(0f, 0f, offset);
		InBaseScript inBaseScript = this;
		inBaseScript.cameraOffset = inBaseScript.cameraOffset + offset;
	}
}