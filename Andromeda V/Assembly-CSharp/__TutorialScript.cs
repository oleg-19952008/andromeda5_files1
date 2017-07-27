using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TransferableObjects;
using UnityEngine;

public class __TutorialScript : MonoBehaviour
{
	private GameObject guidingText;

	private float guidingTextDelta = 4.2f;

	private GuiTextureAnimated guideArrow;

	private GuiWindow wnd;

	private GuiWindow tutorialWindow;

	private GuiButtonResizeable btnSkipTutorial;

	private GuiButtonResizeable btnSkipMovie;

	private bool stepOneInitialized;

	private bool stepTwoInitialized;

	private bool stepThreeInitialized;

	private bool stepFourInitialized;

	private bool stepFiveInitialized;

	private bool isActionSet;

	private bool isPartyArrowInitialize;

	private int currentQuestId = 10000;

	private questState currentQuestState = 1;

	private bool moveCamera;

	private float currentScreenWidth;

	private float currentScreenHeight;

	private Action movieBorderUpdate;

	private MovieDirector moveiScript;

	private GameObject guidePoint;

	private string guidePointRes;

	private Vector3 guidePointCoordinates;

	private bool isQuestIdleTimerStarted;

	private float questIdleTimer;

	private GameObject slideAnimation;

	private bool isCargoArrowShowed;

	private bool isQuestInfoArrowNeeded;

	private bool isRussianServer;

	private bool guideArrowIsSet;

	private GameObject guideArrowTargetObject;

	private GameObject oldPlayer;

	private GameObject oldAria;

	private List<GameObject> forRemove = new List<GameObject>();

	private float cameraSpeed;

	private Vector3 cameraStartingAnimationPoint = new Vector3(100f, 51f, 10f);

	private Vector3 playerDestinationPoint = new Vector3(152f, -1.5f, -12.5f);

	private bool isPlayerInMove;

	private float playerSpeed;

	private bool isMenuOff;

	private float menuAnimationTime = 0.5f;

	private GuiWindow upBorder;

	private GuiWindow downBorder;

	private GuiWindow subtitleWindow;

	private GuiLabel subtitle;

	private GuiTexture fill;

	private GuiTexture fill2;

	private GameObject targetArrow;

	private bool showBoosHint = true;

	private GuiWindow questTrackerArrowWindow;

	private float QUEST_TRACKER_WINDOW_MAX_Y = 317f;

	private float QUEST_TRACKER_WINDOW_X = 35f;

	private float questTrackerDeltaTime;

	private GuiWindow popUpArrowWindow;

	private float POP_UP_WINDOW_MAX_Y = 185f;

	private float POP_UP_WINDOW_X = (float)(Screen.get_width() / 2 + 122);

	private float popUpDeltaTime;

	private GuiWindow smallArrowWindow;

	private GuiTexture smallArrowTexture;

	private GuiLabel text;

	private float smallArrowDeltaTime;

	private GuiWindow smallArrowWindowDown;

	private GuiTexture smallArrowDownTexture;

	private float smallArrowDownDeltaTime;

	private GuiWindow collectRewardArrowWindow;

	private GuiTexture collectRewardArrowTexture;

	private float collectRewardArrowDeltaTime;

	private GuiWindow closeQuestArrowWindow;

	private GuiTexture closeQuestArrowTexture;

	private float closeQuestArrowDeltaTime;

	private GuiTextureAnimated wsArrowOne;

	private GuiTextureAnimated wsArrowTwo;

	private GuiTextureAnimated wsArrowThree;

	private GuiTextureAnimated wsArrowFour;

	private GuiTextureAnimated wsArrowFive;

	private bool isTutorialGuiInitialized;

	public bool isAriaDied;

	private bool isLastPhaseStarted;

	private bool isShipChanged;

	private bool isEscapeToHydraScreenLoaded;

	private bool isSkipTutorialCommandSent;

	private float delayAfterDeath = 2f;

	private float delayBeforShipChange = 2f;

	private float delayBeforStartJumping = 6f;

	private float delayBeforFinishTutorial = 1f;

	private bool isHyperJumpStarted;

	private bool isCheckpointTargetSet;

	public __TutorialScript()
	{
	}

	private void ChangeShip(object prm)
	{
		this.isShipChanged = true;
	}

	private void ClearCheckpointPopUpArrow()
	{
		if (this.isCheckpointTargetSet)
		{
			this.isCheckpointTargetSet = false;
			this.RemoveGuide();
		}
	}

	private void DestroyAllObjectsAndShowMenu(object prm)
	{
		MainScreenWindow mainScreenWindow = AndromedaGui.mainWnd;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = (byte)16
		};
		mainScreenWindow.OnWindowBtnClicked(eventHandlerParam);
		AndromedaGui.mainWnd.OnCloseWindowCallback = new Action(this, __TutorialScript.GoToHydra);
	}

	private void Explode(object prm)
	{
	}

	public void GoToHydra()
	{
		this.OnSkipTutorialClicked(null);
	}

	public void HideBoostHint()
	{
		if (this.guidingText == null)
		{
			return;
		}
		TextMesh componentInChildren = this.guidingText.GetComponentInChildren<TextMesh>();
		if (componentInChildren != null && componentInChildren.get_text() == StaticData.Translate("key_tutorial_boost_hint"))
		{
			componentInChildren.set_text(string.Empty);
		}
	}

	private void HideGui(object prm)
	{
		NetworkScript.player.shipScript.comm.denyReorderGUI = true;
		if (AndromedaGui.mainWnd.activWindowIndex != 255)
		{
			AndromedaGui.mainWnd.OnCloseWindowCallback = null;
			AndromedaGui.mainWnd.CloseActiveWindow();
			QuestInfoWindow.OnCLose = null;
			QuestInfoWindow.Close();
		}
		if (QuestInfoWindow.IsOnScreen)
		{
			QuestInfoWindow.Close();
		}
		if (this.questTrackerArrowWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.questTrackerArrowWindow.handler);
		}
		if (this.popUpArrowWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.popUpArrowWindow.handler);
		}
		if (this.smallArrowWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.smallArrowWindow.handler);
		}
		if (this.smallArrowWindowDown != null)
		{
			AndromedaGui.gui.RemoveWindow(this.smallArrowWindowDown.handler);
		}
		if (this.collectRewardArrowWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.collectRewardArrowWindow.handler);
		}
		if (this.closeQuestArrowWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.closeQuestArrowWindow.handler);
		}
		AndromedaGui.personalStatsWnd.HidePersonalGui();
		QuestTrackerWindow.FinishQuest(this.currentQuestId);
		if (AndromedaGui.mainWnd.questTrackerWindow != null)
		{
			AndromedaGui.mainWnd.questTrackerWindow.StartMoveBy(-AndromedaGui.mainWnd.questTrackerWindow.boundries.get_width(), 0f, this.menuAnimationTime, false);
		}
		AndromedaGui.mainWnd.sideMenuWindow.StartMoveBy(AndromedaGui.mainWnd.sideMenuWindow.boundries.get_width(), 0f, this.menuAnimationTime, false);
		AndromedaGui.mainWnd.playerStatsWindow.StartMoveBy(0f, -AndromedaGui.mainWnd.playerStatsWindow.boundries.get_height(), this.menuAnimationTime, false);
		AndromedaGui.mainWnd.quickSlotsWindow.StartMoveBy(0f, AndromedaGui.mainWnd.quickSlotsWindow.boundries.get_height(), this.menuAnimationTime, false);
		Minimap.wnd.StartMoveBy(-Minimap.wnd.boundries.get_width(), 0f, this.menuAnimationTime, false);
		GuiFramework.musicVolume = 0.4f;
		AudioManager.ChangeTrack("1");
		if (NetworkScript.player.shipScript.speedEffectShip != null && NetworkScript.player.shipScript.speedEffectCamera != null)
		{
			Object.Destroy(NetworkScript.player.shipScript.speedEffectShip);
			Object.Destroy(NetworkScript.player.shipScript.speedEffectCamera);
		}
	}

	private void HideGuidingText()
	{
		if (this.guidingText == null)
		{
			return;
		}
		TextMesh componentInChildren = this.guidingText.GetComponentInChildren<TextMesh>();
		if (componentInChildren != null && componentInChildren.get_text() != StaticData.Translate("key_tutorial_boost_hint"))
		{
			componentInChildren.set_text(string.Empty);
		}
	}

	private void HideSubtitle(object prm)
	{
		this.subtitle.text = string.Empty;
		this.subtitle.TextColor = Color.get_white();
	}

	private void InitCheckPointPopUpArrow()
	{
		if (this.popUpArrowWindow != null)
		{
			this.UpdatePopUpArrowWindowPosition();
		}
		else
		{
			this.popUpArrowWindow = new GuiWindow();
			this.popUpArrowWindow.SetBackgroundTexture("FrameworkGUI", "arrow_tutorial_small");
			this.popUpArrowWindow.zOrder = 230;
			this.popUpArrowWindow.ignoreClickEvents = true;
			this.popUpArrowWindow.boundries.set_x(this.POP_UP_WINDOW_X);
			this.popUpArrowWindow.boundries.set_y(this.POP_UP_WINDOW_MAX_Y);
			this.popUpArrowWindow.isHidden = false;
			AndromedaGui.gui.AddWindow(this.popUpArrowWindow);
			this.popUpDeltaTime = 0f;
		}
	}

	private void InitCloseQuestArrow(float x, float y)
	{
		if (this.closeQuestArrowWindow != null)
		{
			this.UpdateCloseQuestArrowPosition(x, y);
		}
		else
		{
			this.closeQuestArrowWindow = new GuiWindow()
			{
				ignoreClickEvents = true,
				boundries = new Rect(x, y, 150f, 145f),
				isHidden = false
			};
			this.closeQuestArrowDeltaTime = 0f;
			this.closeQuestArrowTexture = new GuiTexture()
			{
				X = 0f,
				Y = 70f
			};
			this.closeQuestArrowWindow.zOrder = 230;
			this.closeQuestArrowTexture.SetTexture("FrameworkGUI", "arrow_tutorial_small");
			AndromedaGui.gui.AddWindow(this.closeQuestArrowWindow);
			this.closeQuestArrowWindow.AddGuiElement(this.closeQuestArrowTexture);
		}
	}

	private void InitCollectRewardArrow(float x, float y)
	{
		if (this.collectRewardArrowWindow != null)
		{
			this.UpdatecollectRewardArrowPosition(x, y);
		}
		else
		{
			this.collectRewardArrowWindow = new GuiWindow()
			{
				ignoreClickEvents = true,
				boundries = new Rect(x, y, 150f, 145f),
				isHidden = false
			};
			this.collectRewardArrowDeltaTime = 0f;
			this.collectRewardArrowTexture = new GuiTexture()
			{
				X = 0f,
				Y = 70f
			};
			this.collectRewardArrowWindow.zOrder = 230;
			this.collectRewardArrowTexture.SetTexture("FrameworkGUI", "arrow_tutorial_small");
			AndromedaGui.gui.AddWindow(this.collectRewardArrowWindow);
			this.collectRewardArrowWindow.AddGuiElement(this.collectRewardArrowTexture);
		}
	}

	private void InitGuide(float x, float y, GuiWindow window)
	{
		if (window == null)
		{
			return;
		}
		if (this.guideArrow != null)
		{
			if (this.guideArrow.X == x && this.guideArrow.Y == y && (object)this.wnd == (object)window)
			{
				return;
			}
			try
			{
				this.wnd.RemoveGuiElement(this.guideArrow);
			}
			catch
			{
			}
			this.guideArrow = null;
			this.wnd = null;
		}
		this.guideArrow = new GuiTextureAnimated();
		this.guideArrow.Init("Guide Arrow", "GuideArrow", "GuideArrow/arrow");
		this.guideArrow.rotationTime = 0.7f;
		this.guideArrow.boundries.set_x(x);
		this.guideArrow.boundries.set_y(y);
		window.AddGuiElement(this.guideArrow);
		this.wnd = window;
	}

	private void InitGuidingArrow()
	{
		this.targetArrow = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("GuidingArrow_pfb"));
		GuidingArrowScript component = this.targetArrow.GetComponent<GuidingArrowScript>();
		component.me = NetworkScript.player.shipScript.get_gameObject();
		component.targetObject = null;
		component.targetLocation = new Vector3(0f, 0f, 0f);
	}

	private void InitializedStepFive()
	{
		this.OnFinalStageClicked(null);
		this.stepFiveInitialized = true;
		this.guideArrowIsSet = false;
		this.guidePoint.get_gameObject().SetActive(true);
		this.guidePointCoordinates = new Vector3(120f, 0f, 28f);
		this.guidePoint.get_transform().set_position(this.guidePointCoordinates);
		this.isQuestIdleTimerStarted = true;
		NetworkScript.player.shipScript.isGuiClosed = true;
	}

	private void InitializedStepFour()
	{
		this.OnSpawnPVEsClicked(null);
		this.stepFourInitialized = true;
		this.guideArrowIsSet = false;
		this.guidePoint.get_gameObject().SetActive(true);
		this.guidePointCoordinates = new Vector3(64.39f, 0f, 39.37f);
		this.guidePoint.get_transform().set_position(this.guidePointCoordinates);
		this.isQuestIdleTimerStarted = true;
		NetworkScript.player.shipScript.isGuiClosed = true;
	}

	private void InitializedStepOne()
	{
		this.OnMoveAriaClicked(null);
		this.stepOneInitialized = true;
		this.guideArrowIsSet = false;
		Application.ExternalCall("logState", new object[] { 10 });
		this.guidePoint.get_gameObject().SetActive(true);
		this.guidePointCoordinates = new Vector3(-50.3f, 0f, 48.4f);
		this.guidePoint.get_transform().set_position(this.guidePointCoordinates);
		this.isQuestIdleTimerStarted = true;
		this.ShowGuideArrowInSpace();
		NetworkScript.player.shipScript.isGuiClosed = true;
	}

	private void InitializedStepThree()
	{
		AndromedaGui.mainWnd.btnShipConfig.isEnabled = true;
		AndromedaGui.mainWnd.btnFushion.isEnabled = true;
		this.stepThreeInitialized = true;
		this.guideArrowIsSet = false;
		this.RemoveSmallArrow();
		NetworkScript.player.shipScript.isGuiClosed = true;
	}

	private void InitializedStepTwo()
	{
		this.OnSpawnMineralClicked(null);
		this.stepTwoInitialized = true;
		this.guideArrowIsSet = false;
		this.guidePoint.get_gameObject().SetActive(true);
		this.guidePointCoordinates = new Vector3(6.4f, 0f, -3.8f);
		this.guidePoint.get_transform().set_position(this.guidePointCoordinates);
		this.isQuestIdleTimerStarted = true;
		this.RemoveSmallArrow();
		NetworkScript.player.shipScript.isGuiClosed = true;
	}

	private void InitializeGuidingText()
	{
		this.guidingText = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("SpaceLbl"));
		TextMesh componentInChildren = this.guidingText.GetComponentInChildren<TextMesh>();
		this.guidingText.GetComponentInChildren<MeshRenderer>().get_material().set_color(GuiNewStyleBar.orangeColor);
		if (componentInChildren != null)
		{
			componentInChildren.set_text(StaticData.Translate("key_tutorial_guiding_arrow_lbl_default"));
			componentInChildren.set_fontSize(45);
			componentInChildren.set_alignment(1);
		}
	}

	private void InitQuestTrackerArrow()
	{
		if (this.questTrackerArrowWindow != null)
		{
			this.UpdateQuestTrackerArrowWindowPosition();
		}
		else
		{
			this.questTrackerArrowWindow = new GuiWindow();
			this.questTrackerArrowWindow.SetBackgroundTexture("FrameworkGUI", "arrow_tutorial_small");
			this.questTrackerArrowWindow.zOrder = 230;
			this.questTrackerArrowWindow.ignoreClickEvents = true;
			this.questTrackerArrowWindow.boundries.set_x(this.QUEST_TRACKER_WINDOW_X);
			this.questTrackerArrowWindow.boundries.set_y(this.QUEST_TRACKER_WINDOW_MAX_Y);
			this.questTrackerArrowWindow.isHidden = false;
			AndromedaGui.gui.AddWindow(this.questTrackerArrowWindow);
			this.questTrackerDeltaTime = 0f;
		}
	}

	private void InitSmallArrow(float x, float y, string txt, bool refreshText = false)
	{
		if (this.smallArrowWindow != null)
		{
			this.UpdateSmallArrowWindowPosition(x, y);
		}
		else
		{
			this.smallArrowWindow = new GuiWindow()
			{
				ignoreClickEvents = true,
				boundries = new Rect(x, y, 200f, 145f),
				isHidden = false
			};
			this.smallArrowDeltaTime = 0f;
			this.smallArrowTexture = new GuiTexture()
			{
				X = 0f,
				Y = 70f
			};
			this.text = new GuiLabel()
			{
				boundries = new Rect(45f, -20f, 145f, 125f),
				text = txt,
				FontSize = 15,
				Font = GuiLabel.FontBold,
				TextColor = GuiNewStyleBar.orangeColor,
				Alignment = 4
			};
			this.smallArrowWindow.zOrder = 230;
			this.smallArrowTexture.SetTexture("FrameworkGUI", "arrow_tutorial_small");
			this.smallArrowWindow.AddGuiElement(this.text);
			AndromedaGui.gui.AddWindow(this.smallArrowWindow);
			this.smallArrowWindow.AddGuiElement(this.smallArrowTexture);
		}
		if (this.text != null && refreshText && this.text.text != txt)
		{
			this.text.text = txt;
		}
	}

	private void InitSmallArrowDown(float x, float y, string txt)
	{
		if (this.smallArrowWindowDown != null)
		{
			this.UpdateSmallArrowDownWindowPosition(x, y);
		}
		else
		{
			this.smallArrowWindowDown = new GuiWindow()
			{
				ignoreClickEvents = true,
				boundries = new Rect(x, y, 200f, 145f),
				isHidden = false
			};
			this.smallArrowDownDeltaTime = 0f;
			this.smallArrowDownTexture = new GuiTexture()
			{
				X = 0f,
				Y = 70f
			};
			GuiLabel guiLabel = new GuiLabel()
			{
				boundries = new Rect(45f, 10f, 145f, 125f),
				text = txt,
				FontSize = 15,
				Font = GuiLabel.FontBold,
				TextColor = GuiNewStyleBar.orangeColor,
				Alignment = 4
			};
			this.smallArrowWindowDown.zOrder = 230;
			this.smallArrowDownTexture.SetTexture("FrameworkGUI", "arrow_tutorial_small_rotated");
			this.smallArrowWindowDown.AddGuiElement(guiLabel);
			AndromedaGui.gui.AddWindow(this.smallArrowWindowDown);
			this.smallArrowWindowDown.AddGuiElement(this.smallArrowDownTexture);
		}
	}

	private void InitTutorialGUI()
	{
		this.isTutorialGuiInitialized = true;
		this.tutorialWindow = new GuiWindow()
		{
			boundries = new Rect((float)(Screen.get_width() - 225), (float)(Screen.get_height() - 30), 225f, 30f),
			isHidden = false,
			zOrder = 160
		};
		if (this.btnSkipTutorial == null)
		{
			this.btnSkipTutorial = new GuiButtonResizeable();
			this.btnSkipTutorial.SetSmallBlueTexture();
			this.btnSkipTutorial.boundries = new Rect(0f, 0f, 215f, 30f);
			this.btnSkipTutorial.Caption = StaticData.Translate("key_tutorial_btn_skip_tutorial").ToUpper();
			this.btnSkipTutorial.FontSize = 14;
			this.btnSkipTutorial.Alignment = 1;
			this.btnSkipTutorial.MarginTop = 4;
			this.btnSkipTutorial.Clicked = new Action<EventHandlerParam>(this, __TutorialScript.DestroyAllObjectsAndShowMenu);
			this.tutorialWindow.AddGuiElement(this.btnSkipTutorial);
		}
		AndromedaGui.gui.AddWindow(this.tutorialWindow);
		this.OnSpawnAriaClicked(null);
		NewQuest[] newQuestArray = StaticData.allQuests;
		if (__TutorialScript.<>f__am$cache5F == null)
		{
			__TutorialScript.<>f__am$cache5F = new Func<NewQuest, bool>(null, (NewQuest q) => q.id == 10000);
		}
		if (Enumerable.FirstOrDefault<NewQuest>(Enumerable.Where<NewQuest>(newQuestArray, __TutorialScript.<>f__am$cache5F)) != null)
		{
			NewQuest[] newQuestArray1 = StaticData.allQuests;
			if (__TutorialScript.<>f__am$cache60 == null)
			{
				__TutorialScript.<>f__am$cache60 = new Func<NewQuest, bool>(null, (NewQuest q) => q.id == 10000);
			}
			QuestInfoWindow.Create(Enumerable.First<NewQuest>(Enumerable.Where<NewQuest>(newQuestArray1, __TutorialScript.<>f__am$cache60)), false, true);
		}
	}

	private void InitWsArrowFive()
	{
		if (this.wsArrowFive != null || AndromedaGui.mainWnd == null || AndromedaGui.mainWnd.activWindowIndex != 1 || AndromedaGui.mainWnd.activeWindow == null)
		{
			return;
		}
		this.wsArrowFive = new GuiTextureAnimated();
		this.wsArrowFive.Init("Guide Arrow", "GuideArrow", "GuideArrow/arrow");
		this.wsArrowFive.rotationTime = 0.7f;
		this.wsArrowFive.boundries.set_x(442f);
		this.wsArrowFive.boundries.set_y(162f);
		AndromedaGui.mainWnd.activeWindow.AddGuiElement(this.wsArrowFive);
	}

	private void InitWsArrowFour()
	{
		if (this.wsArrowFour != null || AndromedaGui.mainWnd == null || AndromedaGui.mainWnd.activWindowIndex != 1 || AndromedaGui.mainWnd.activeWindow == null)
		{
			return;
		}
		this.wsArrowFour = new GuiTextureAnimated();
		this.wsArrowFour.Init("Guide Arrow", "GuideArrow", "GuideArrow/arrow");
		this.wsArrowFour.rotationTime = 0.7f;
		this.wsArrowFour.boundries.set_x(342f);
		this.wsArrowFour.boundries.set_y(233f);
		AndromedaGui.mainWnd.activeWindow.AddGuiElement(this.wsArrowFour);
	}

	private void InitWsArrowOne()
	{
		if (this.wsArrowOne != null || AndromedaGui.mainWnd == null || AndromedaGui.mainWnd.activWindowIndex != 1 || AndromedaGui.mainWnd.activeWindow == null)
		{
			return;
		}
		this.wsArrowOne = new GuiTextureAnimated();
		this.wsArrowOne.Init("Guide Arrow", "GuideArrow", "GuideArrow/arrow");
		this.wsArrowOne.rotationTime = 0.7f;
		this.wsArrowOne.boundries.set_x(342f);
		this.wsArrowOne.boundries.set_y(92f);
		AndromedaGui.mainWnd.activeWindow.AddGuiElement(this.wsArrowOne);
	}

	private void InitWsArrowThree()
	{
		if (this.wsArrowThree != null || AndromedaGui.mainWnd == null || AndromedaGui.mainWnd.activWindowIndex != 1 || AndromedaGui.mainWnd.activeWindow == null)
		{
			return;
		}
		this.wsArrowThree = new GuiTextureAnimated();
		this.wsArrowThree.Init("Guide Arrow", "GuideArrow", "GuideArrow/arrow");
		this.wsArrowThree.rotationTime = 0.7f;
		this.wsArrowThree.boundries.set_x(442f);
		this.wsArrowThree.boundries.set_y(92f);
		AndromedaGui.mainWnd.activeWindow.AddGuiElement(this.wsArrowThree);
	}

	private void InitWsArrowTwo()
	{
		if (this.wsArrowTwo != null || AndromedaGui.mainWnd == null || AndromedaGui.mainWnd.activWindowIndex != 1 || AndromedaGui.mainWnd.activeWindow == null)
		{
			return;
		}
		this.wsArrowTwo = new GuiTextureAnimated();
		this.wsArrowTwo.Init("Guide Arrow", "GuideArrow", "GuideArrow/arrow");
		this.wsArrowTwo.rotationTime = 0.7f;
		this.wsArrowTwo.boundries.set_x(342f);
		this.wsArrowTwo.boundries.set_y(162f);
		AndromedaGui.mainWnd.activeWindow.AddGuiElement(this.wsArrowTwo);
	}

	private bool IsTutorialStepInitialized(int activeQuestId)
	{
		if (activeQuestId == 10000)
		{
			if (this.stepOneInitialized)
			{
				return true;
			}
			return false;
		}
		if (activeQuestId == 10001)
		{
			if (this.stepTwoInitialized)
			{
				return true;
			}
			return false;
		}
		if (activeQuestId == 10002)
		{
			if (this.stepThreeInitialized)
			{
				return true;
			}
			return false;
		}
		if (activeQuestId == 10003)
		{
			if (this.stepFourInitialized)
			{
				return true;
			}
			return false;
		}
		if (activeQuestId != 10004)
		{
			return false;
		}
		if (this.stepFiveInitialized)
		{
			return true;
		}
		return false;
	}

	public void JumpToHydra()
	{
		// 
		// Current member / type: System.Void __TutorialScript::JumpToHydra()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void JumpToHydra()
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

	private void LateUpdate()
	{
		if (this.guidingText != null)
		{
			this.guidingText.get_transform().set_position(new Vector3(NetworkScript.player.vessel.x, 3f, NetworkScript.player.vessel.z + this.guidingTextDelta * 1.2f));
		}
	}

	private void ManageQuestIdleTimer()
	{
		if (this.isQuestIdleTimerStarted)
		{
			__TutorialScript _deltaTime = this;
			_deltaTime.questIdleTimer = _deltaTime.questIdleTimer - Time.get_deltaTime();
			if (this.questIdleTimer <= 0f)
			{
				this.questIdleTimer = 7f;
				this.ShowGuideArrowInSpace();
			}
		}
	}

	private void ManageSkipMovieKey()
	{
		if ((Input.GetKeyUp(32) || Input.GetKeyDown(27) || Input.GetKeyDown(13)) && NetworkScript.player.vessel.x > 100f)
		{
			this.DestroyAllObjectsAndShowMenu(null);
		}
	}

	private void OnFinalStageClicked(object prm)
	{
		// 
		// Current member / type: System.Void __TutorialScript::OnFinalStageClicked(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnFinalStageClicked(System.Object)
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

	private void OnMoveAriaClicked(object prm)
	{
		// 
		// Current member / type: System.Void __TutorialScript::OnMoveAriaClicked(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnMoveAriaClicked(System.Object)
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

	private void OnSkipTutorialClicked(object prm)
	{
		Object.Destroy(GameObject.Find("Player"));
		Object.Destroy(GameObject.Find("Aria"));
		playWebGame.udp.ExecuteCommand(150, null);
		playWebGame.LogMixPanel(MixPanelEvents.FinishTutorialMobile, null);
	}

	private void OnSpawnAriaClicked(object prm)
	{
		playWebGame.udp.ExecuteCommand(151, new ShortAndBool());
	}

	private void OnSpawnMineralClicked(object prm)
	{
		// 
		// Current member / type: System.Void __TutorialScript::OnSpawnMineralClicked(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnSpawnMineralClicked(System.Object)
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

	private void OnSpawnPVEsClicked(object prm)
	{
		// 
		// Current member / type: System.Void __TutorialScript::OnSpawnPVEsClicked(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnSpawnPVEsClicked(System.Object)
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

	private void PlayMovieSpeech(object audioClipName)
	{
		AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("TutorialMovieAudio", (string)audioClipName);
		AudioManager.PlayGUISound(fromStaticSet);
	}

	private void RemoveCheckPointPopUpArrow()
	{
		if (this.popUpArrowWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.popUpArrowWindow.handler);
			this.popUpArrowWindow = null;
			this.POP_UP_WINDOW_MAX_Y = 185f;
		}
	}

	private void RemoveCloseQuestArrowWindow()
	{
		if (this.closeQuestArrowWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.closeQuestArrowWindow.handler);
			this.closeQuestArrowWindow = null;
		}
	}

	private void RemoveCollectRewardArrowWindow()
	{
		if (this.collectRewardArrowWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.collectRewardArrowWindow.handler);
			this.collectRewardArrowWindow = null;
		}
	}

	private void RemoveGameComponents(object prm)
	{
		this.OnFinalStageClicked(null);
		this.oldPlayer = NetworkScript.player.shipScript.get_gameObject();
		Object.Destroy(this.oldPlayer.GetComponent<Animation>());
		if (NetworkScript.player.shipScript.selectedObject != null)
		{
			NetworkScript.player.shipScript.DeselectCurrentObject();
			TargetingWnd.wndTargeting.isHidden = true;
			TargetingWnd.Remove();
			if (NetworkScript.player.vessel.selectedPoPnbId != 0)
			{
				playWebGame.udp.ExecuteCommand(49, new UniversalTransportContainer(), 56);
				NetworkScript.player.vessel.selectedPoPnbId = 0;
			}
		}
		IList<GameObjectPhysics> values = NetworkScript.player.shipScript.comm.gameObjects.get_Values();
		if (__TutorialScript.<>f__am$cache5E == null)
		{
			__TutorialScript.<>f__am$cache5E = new Func<GameObjectPhysics, bool>(null, (GameObjectPhysics t) => t is PlayerObjectPhysics);
		}
		GameObjectPhysics[] array = Enumerable.ToArray<GameObjectPhysics>(Enumerable.Where<GameObjectPhysics>(values, __TutorialScript.<>f__am$cache5E));
		GameObjectPhysics[] gameObjectPhysicsArray = array;
		for (int i = 0; i < (int)gameObjectPhysicsArray.Length; i++)
		{
			GameObjectPhysics gameObjectPhysic = gameObjectPhysicsArray[i];
			this.forRemove.Add((GameObject)gameObjectPhysic.gameObject);
			NetworkScript.playerNameManager.removePOPName((PlayerObjectPhysics)gameObjectPhysic);
		}
		Object.Destroy(NetworkScript.player.shipScript.myShipBarBody);
		Object.Destroy(NetworkScript.player.shipScript.myShipBarBlue);
		Object.Destroy(NetworkScript.player.shipScript.myShipBarGreen);
		Object.Destroy(NetworkScript.player.shipScript.myShipBarEnergy);
		Object.Destroy(ShipScript.mapTarget);
		Object.Destroy(NetworkScript.player.shipScript.outOfMiningRangeMarker);
		Object.Destroy(NetworkScript.player.shipScript._miningBeam);
		if (this.guidingText != null)
		{
			Object.Destroy(this.guidingText);
		}
		if (this.targetArrow != null)
		{
			Object.Destroy(this.targetArrow);
		}
		Object[] objArray = Object.FindObjectsOfType(typeof(ShipScript));
		for (int j = 0; j < (int)objArray.Length; j++)
		{
			Object.Destroy(objArray[j]);
		}
		objArray = Object.FindObjectsOfType(typeof(PveScript));
		for (int k = 0; k < (int)objArray.Length; k++)
		{
			Object.Destroy(objArray[k]);
		}
		float single = Vector3.Distance(this.oldPlayer.get_transform().get_position(), this.playerDestinationPoint);
		this.playerSpeed = single / 7f;
		this.isPlayerInMove = true;
	}

	private void RemoveGuide()
	{
		if (this.guideArrow == null || this.wnd == null)
		{
			return;
		}
		this.wnd.RemoveGuiElement(this.guideArrow);
		this.guideArrow = null;
		this.wnd = null;
	}

	private void RemoveGuidePointResetQuestTimer()
	{
		this.guidePoint.get_gameObject().SetActive(false);
		this.isQuestIdleTimerStarted = false;
		this.questIdleTimer = 7f;
	}

	private void RemoveOldGameObjects(object prm)
	{
		if (this.oldAria != null)
		{
			Object.Destroy(this.oldAria);
		}
	}

	private void RemoveQuestTrackerArrow()
	{
		if (this.questTrackerArrowWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.questTrackerArrowWindow.handler);
			this.questTrackerArrowWindow = null;
		}
	}

	private void RemoveSmallArrow()
	{
		if (this.smallArrowWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.smallArrowWindow.handler);
			this.smallArrowWindow = null;
		}
	}

	private void RemoveSmallDownArrow()
	{
		if (this.smallArrowWindowDown != null)
		{
			AndromedaGui.gui.RemoveWindow(this.smallArrowWindowDown.handler);
			this.smallArrowWindowDown = null;
		}
	}

	private void RemoveWsArrowFive()
	{
		if (this.wsArrowFive == null || AndromedaGui.mainWnd == null || AndromedaGui.mainWnd.activWindowIndex != 1 || AndromedaGui.mainWnd.activeWindow == null)
		{
			return;
		}
		AndromedaGui.mainWnd.activeWindow.RemoveGuiElement(this.wsArrowFive);
		this.wsArrowFive = null;
	}

	private void RemoveWsArrowFour()
	{
		if (this.wsArrowFour == null || AndromedaGui.mainWnd == null || AndromedaGui.mainWnd.activWindowIndex != 1 || AndromedaGui.mainWnd.activeWindow == null)
		{
			return;
		}
		AndromedaGui.mainWnd.activeWindow.RemoveGuiElement(this.wsArrowFour);
		this.wsArrowFour = null;
	}

	private void RemoveWsArrowOne()
	{
		if (this.wsArrowOne == null || AndromedaGui.mainWnd == null || AndromedaGui.mainWnd.activWindowIndex != 1 || AndromedaGui.mainWnd.activeWindow == null)
		{
			return;
		}
		AndromedaGui.mainWnd.activeWindow.RemoveGuiElement(this.wsArrowOne);
		this.wsArrowOne = null;
	}

	private void RemoveWsArrowThree()
	{
		if (this.wsArrowThree == null || AndromedaGui.mainWnd == null || AndromedaGui.mainWnd.activWindowIndex != 1 || AndromedaGui.mainWnd.activeWindow == null)
		{
			return;
		}
		AndromedaGui.mainWnd.activeWindow.RemoveGuiElement(this.wsArrowThree);
		this.wsArrowThree = null;
	}

	private void RemoveWsArrowTwo()
	{
		if (this.wsArrowTwo == null || AndromedaGui.mainWnd == null || AndromedaGui.mainWnd.activWindowIndex != 1 || AndromedaGui.mainWnd.activeWindow == null)
		{
			return;
		}
		AndromedaGui.mainWnd.activeWindow.RemoveGuiElement(this.wsArrowTwo);
		this.wsArrowTwo = null;
	}

	private void ReorderGUI()
	{
		if (this.tutorialWindow != null)
		{
			this.tutorialWindow.boundries = new Rect((float)(Screen.get_width() - 225), (float)(Screen.get_height() - 30), 225f, 30f);
		}
	}

	private void SetCheckpoinPopUpArrow()
	{
		if (!this.isCheckpointTargetSet)
		{
			this.isCheckpointTargetSet = true;
			this.InitGuide(345f, 25f, CheckpointDialogWindow.wnd);
		}
	}

	private void SetMovieBorderUpdate(object prm)
	{
		this.movieBorderUpdate = new Action(this, __TutorialScript.UpdateMovieBorder);
	}

	private void SetTargetArrowDestination(GameObject go)
	{
		if (this.targetArrow == null)
		{
			return;
		}
		GuidingArrowScript component = this.targetArrow.GetComponent<GuidingArrowScript>();
		component.targetObject = go;
		component.targetLocation = Vector3.get_zero();
		component.isLocationSet = false;
		this.guideArrowIsSet = true;
		this.guideArrowTargetObject = go;
	}

	public void ShowBoostHint()
	{
		if (!this.showBoosHint)
		{
			this.showBoosHint = true;
			return;
		}
		if (this.guidingText == null)
		{
			return;
		}
		if (NetworkScript.player.shipScript.p.cfg.shield < (float)NetworkScript.player.shipScript.p.cfg.shieldMax * 0.1f)
		{
			return;
		}
		TextMesh componentInChildren = this.guidingText.GetComponentInChildren<TextMesh>();
		if (componentInChildren == null || componentInChildren.get_text() != string.Empty)
		{
			return;
		}
		componentInChildren.set_text(StaticData.Translate("key_tutorial_boost_hint"));
		componentInChildren.set_fontSize(45);
		componentInChildren.set_alignment(1);
	}

	private void ShowGuideArrowInSpace()
	{
		if (Vector3.Distance(new Vector3(NetworkScript.player.vessel.x, NetworkScript.player.vessel.y, NetworkScript.player.vessel.z), this.guidePointCoordinates) > 20f)
		{
			this.showBoosHint = false;
			this.slideAnimation = (GameObject)Object.Instantiate((GameObject)playWebGame.assets.GetPrefab("DirectionArrows_pfb"));
			this.slideAnimation.get_transform().set_position(new Vector3(NetworkScript.player.vessel.x, NetworkScript.player.vessel.y, NetworkScript.player.vessel.z));
			this.slideAnimation.get_transform().LookAt(this.guidePointCoordinates);
			SlideToMoveAnimation component = this.slideAnimation.GetComponent<SlideToMoveAnimation>();
			component.theShip = NetworkScript.player.gameObject;
			component.toLookAt = this.guidePointCoordinates;
			this.slideAnimation.get_transform().GetChild(1).get_gameObject().SetActive(false);
		}
	}

	private void ShowMoveBorder(object prm)
	{
		float _height = (float)Screen.get_height() - (float)Screen.get_width() * 0.5f;
		if (_height < 20f)
		{
			_height = 20f;
		}
		this.upBorder = new GuiWindow()
		{
			isHidden = false,
			boundries = new Rect(0f, -_height / 2f, (float)Screen.get_width(), _height / 2f),
			zOrder = 150
		};
		AndromedaGui.gui.AddWindow(this.upBorder);
		Texture2D texture2D = new Texture2D(1, 1);
		texture2D.SetPixel(0, 0, Color.get_black());
		texture2D.Apply();
		this.fill = new GuiTexture();
		this.fill.SetTexture2D(texture2D);
		this.fill.boundries = new Rect(0f, 0f, (float)Screen.get_width(), _height / 2f);
		this.upBorder.AddGuiElement(this.fill);
		this.downBorder = new GuiWindow()
		{
			isHidden = false,
			boundries = new Rect(0f, (float)Screen.get_height(), (float)Screen.get_width(), _height / 2f),
			zOrder = 150
		};
		AndromedaGui.gui.AddWindow(this.downBorder);
		this.fill2 = new GuiTexture();
		this.fill2.SetTexture2D(texture2D);
		this.fill2.boundries = this.fill.boundries;
		this.downBorder.AddGuiElement(this.fill2);
		this.upBorder.StartMoveBy(0f, _height / 2f, 1f, true);
		this.downBorder.StartMoveBy(0f, -_height / 2f, 1f, true);
		this.subtitleWindow = new GuiWindow()
		{
			boundries = new Rect(0f, (float)Screen.get_height() - _height / 2f - 220f, this.currentScreenWidth, 220f),
			isHidden = false
		};
		AndromedaGui.gui.AddWindow(this.subtitleWindow);
		this.subtitle = new GuiLabel()
		{
			boundries = new Rect(this.subtitleWindow.boundries.get_width() * 0.1f, 0f, this.subtitleWindow.boundries.get_width() * 0.8f, 200f),
			Alignment = 7,
			Font = GuiLabel.FontBold,
			FontSize = 48
		};
		this.subtitleWindow.AddGuiElement(this.subtitle);
	}

	private void ShowSubtitle(object prm)
	{
		string str = ((StringAndColor)prm).text;
		Color color = ((StringAndColor)prm).color;
		this.subtitle.text = str;
		this.subtitle.TextColor = color;
	}

	private void SpawnDetonator(object prm)
	{
		AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "explosion_6");
		AudioManager.PlayAudioClip(fromStaticSet, new Vector3(163f, 0.7f, -4.5f));
		GameObject gameObject = null;
		gameObject = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("Explosion/explosionEffect_pfb"), new Vector3(163f, 0.7f, -4.5f), Quaternion.get_identity());
		Object.Destroy(gameObject, 4f);
	}

	private void Start()
	{
		this.currentScreenWidth = (float)Screen.get_width();
		this.currentScreenHeight = (float)Screen.get_height();
		this.moveiScript = GameObject.Find("GlobalObject").GetComponent<MovieDirector>();
		this.guidePointRes = "ShootLockPurple";
		this.guidePoint = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab(this.guidePointRes));
		this.guidePoint.get_transform().set_localScale(new Vector3(1.5f, 1.5f, 1.5f));
		this.isQuestIdleTimerStarted = false;
		this.questIdleTimer = 10f;
		this.isRussianServer = playWebGame.GAME_TYPE == "ru";
	}

	private void StartFinalMovie()
	{
		MovieEvent movieEvent;
		MovieEvent movieEvent1;
		MovieEvent movieEvent2;
		MovieEvent movieEvent3;
		MovieEvent movieEvent4;
		MovieEvent movieEvent5;
		this.isMenuOff = true;
		StringAndColor stringAndColor = new StringAndColor()
		{
			color = GuiNewStyleBar.orangeColor,
			text = StaticData.Translate("key_tutorial_movie_sub1")
		};
		StringAndColor stringAndColor1 = stringAndColor;
		stringAndColor = new StringAndColor()
		{
			color = GuiNewStyleBar.blueColor,
			text = StaticData.Translate("key_tutorial_movie_sub2")
		};
		StringAndColor stringAndColor2 = stringAndColor;
		stringAndColor = new StringAndColor()
		{
			color = GuiNewStyleBar.orangeColor,
			text = StaticData.Translate("key_tutorial_movie_sub3")
		};
		StringAndColor stringAndColor3 = stringAndColor;
		stringAndColor = new StringAndColor()
		{
			color = GuiNewStyleBar.blueColor,
			text = StaticData.Translate("key_tutorial_movie_sub4")
		};
		StringAndColor stringAndColor4 = stringAndColor;
		stringAndColor = new StringAndColor()
		{
			color = GuiNewStyleBar.orangeColor,
			text = StaticData.Translate("key_tutorial_movie_sub5")
		};
		StringAndColor stringAndColor5 = stringAndColor;
		stringAndColor = new StringAndColor()
		{
			color = GuiNewStyleBar.blueColor,
			text = StaticData.Translate("key_tutorial_movie_sub6")
		};
		StringAndColor stringAndColor6 = stringAndColor;
		MovieEventCustom movieEventCustom = new MovieEventCustom()
		{
			Callback = new Action<object>(this, __TutorialScript.RemoveGameComponents)
		};
		MovieEventCustom movieEventCustom1 = movieEventCustom;
		movieEventCustom = new MovieEventCustom()
		{
			Callback = new Action<object>(this, __TutorialScript.HideGui)
		};
		MovieEventCustom movieEventCustom2 = movieEventCustom;
		movieEventCustom = new MovieEventCustom()
		{
			Callback = new Action<object>(this, __TutorialScript.StartMoveCamera)
		};
		MovieEventCustom movieEventCustom3 = movieEventCustom;
		movieEventCustom = new MovieEventCustom()
		{
			Callback = new Action<object>(this, __TutorialScript.ShowMoveBorder),
			time = 0.5f
		};
		MovieEventCustom movieEventCustom4 = movieEventCustom;
		MovieEventPlayAnimation movieEventPlayAnimation = new MovieEventPlayAnimation()
		{
			animationName = "Tutorial_camera2",
			gameObjectName = "Main Camera",
			time = 0.5f
		};
		MovieEventPlayAnimation movieEventPlayAnimation1 = movieEventPlayAnimation;
		movieEventCustom = new MovieEventCustom()
		{
			Callback = new Action<object>(this, __TutorialScript.PlayMovieSpeech),
			parameter = "tutorial_movie_voice_01",
			time = 0.5f
		};
		MovieEventCustom movieEventCustom5 = movieEventCustom;
		movieEventCustom = new MovieEventCustom()
		{
			Callback = new Action<object>(this, __TutorialScript.ShowSubtitle),
			parameter = stringAndColor1,
			time = 0.5f
		};
		MovieEventCustom movieEventCustom6 = movieEventCustom;
		movieEventCustom = new MovieEventCustom()
		{
			Callback = new Action<object>(this, __TutorialScript.SetMovieBorderUpdate),
			time = 1.5f
		};
		MovieEventCustom movieEventCustom7 = movieEventCustom;
		movieEventCustom = new MovieEventCustom()
		{
			Callback = new Action<object>(this, __TutorialScript.ShowSubtitle),
			parameter = stringAndColor2,
			time = 3.5f
		};
		MovieEventCustom movieEventCustom8 = movieEventCustom;
		movieEventCustom = new MovieEventCustom()
		{
			Callback = new Action<object>(this, __TutorialScript.PlayMovieSpeech),
			parameter = "tutorial_movie_voice_02",
			time = 3.5f
		};
		MovieEventCustom movieEventCustom9 = movieEventCustom;
		movieEventCustom = new MovieEventCustom()
		{
			Callback = new Action<object>(this, __TutorialScript.RemoveOldGameObjects),
			time = 4f
		};
		MovieEventCustom movieEventCustom10 = movieEventCustom;
		MovieEventAddGameObject movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "Aria",
			prefabName = "Tutorial/Aria_tutorial",
			position = new Vector3(58f, 0f, 65f),
			rotation = new Vector3(0f, 117f, 0f),
			time = 4f
		};
		MovieEventAddGameObject movieEventAddGameObject1 = movieEventAddGameObject;
		movieEventPlayAnimation = new MovieEventPlayAnimation()
		{
			animationName = "Aria_animation",
			gameObjectName = "Aria",
			time = 4f
		};
		MovieEventPlayAnimation movieEventPlayAnimation2 = movieEventPlayAnimation;
		movieEventCustom = new MovieEventCustom()
		{
			Callback = new Action<object>(this, __TutorialScript.ShowSubtitle),
			parameter = stringAndColor3,
			time = 7.5f
		};
		MovieEventCustom movieEventCustom11 = movieEventCustom;
		movieEventCustom = new MovieEventCustom()
		{
			Callback = new Action<object>(this, __TutorialScript.PlayMovieSpeech),
			parameter = "tutorial_movie_voice_03",
			time = 7.5f
		};
		MovieEventCustom movieEventCustom12 = movieEventCustom;
		movieEventCustom = new MovieEventCustom()
		{
			Callback = new Action<object>(this, __TutorialScript.ShowSubtitle),
			parameter = stringAndColor4,
			time = 10f
		};
		MovieEventCustom movieEventCustom13 = movieEventCustom;
		movieEventCustom = new MovieEventCustom()
		{
			Callback = new Action<object>(this, __TutorialScript.PlayMovieSpeech),
			parameter = "tutorial_movie_voice_04",
			time = 10f
		};
		MovieEventCustom movieEventCustom14 = movieEventCustom;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "LaserAriaTorpedo1",
			prefabName = "Tutorial/Shoot_aria",
			position = new Vector3(164f, -0.3f, -6.5f),
			rotation = new Vector3(0f, -32f, 0f),
			time = 9f
		};
		MovieEventAddGameObject movieEventAddGameObject2 = movieEventAddGameObject;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "LaserAriaTorpedo2",
			prefabName = "Tutorial/Shoot_aria",
			position = new Vector3(164f, -0.3f, -6.5f),
			rotation = new Vector3(0f, -32f, 0f),
			time = 10f
		};
		MovieEventAddGameObject movieEventAddGameObject3 = movieEventAddGameObject;
		movieEventCustom = new MovieEventCustom()
		{
			Callback = new Action<object>(this, __TutorialScript.ShowSubtitle),
			parameter = stringAndColor5,
			time = 13f
		};
		MovieEventCustom movieEventCustom15 = movieEventCustom;
		movieEventCustom = new MovieEventCustom()
		{
			Callback = new Action<object>(this, __TutorialScript.PlayMovieSpeech),
			parameter = "tutorial_movie_voice_05",
			time = 13f
		};
		MovieEventCustom movieEventCustom16 = movieEventCustom;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "LaserTorpedoAria1",
			prefabName = "Tutorial/Laser_torpedo",
			position = new Vector3(176f, 0f, -26f),
			rotation = new Vector3(0f, 147f, 0f),
			time = 10.5f
		};
		MovieEventAddGameObject movieEventAddGameObject4 = movieEventAddGameObject;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "LaserPlayerTorpedo1",
			prefabName = "Tutorial/Shoot_battlecruise",
			position = new Vector3(157f, -1f, -15f),
			rotation = new Vector3(0f, -65f, 0f),
			time = 10.8f
		};
		MovieEventAddGameObject movieEventAddGameObject5 = movieEventAddGameObject;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "LaserPlayerTorpedo2",
			prefabName = "Tutorial/Shoot_battlecruise",
			position = new Vector3(157f, -1f, -15f),
			rotation = new Vector3(0f, -65f, 0f),
			time = 11f
		};
		MovieEventAddGameObject movieEventAddGameObject6 = movieEventAddGameObject;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "LaserTorpedoAria2",
			prefabName = "Tutorial/Laser_torpedo",
			position = new Vector3(176f, 0f, -26f),
			rotation = new Vector3(0f, 147f, 0f),
			time = 11.7f
		};
		MovieEventAddGameObject movieEventAddGameObject7 = movieEventAddGameObject;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "LaserAriaTorpedo3",
			prefabName = "Tutorial/Shoot_aria",
			position = new Vector3(164f, -0.3f, -6.5f),
			rotation = new Vector3(0f, -32f, 0f),
			time = 12f
		};
		MovieEventAddGameObject movieEventAddGameObject8 = movieEventAddGameObject;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "FlameAria",
			prefabName = "Tutorial/Smoke_animation",
			position = new Vector3(164f, 2f, -6.5f),
			rotation = new Vector3(19f, 63.5f, 325f),
			time = 12f
		};
		MovieEventAddGameObject movieEventAddGameObject9 = movieEventAddGameObject;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "LaserTorpedoPlayer1",
			prefabName = "Tutorial/Laser_torpedo",
			position = new Vector3(174f, -0.6f, -26f),
			rotation = new Vector3(0f, 123f, 0f),
			time = 12.3f
		};
		MovieEventAddGameObject movieEventAddGameObject10 = movieEventAddGameObject;
		movieEventCustom = new MovieEventCustom()
		{
			Callback = new Action<object>(this, __TutorialScript.ShowSubtitle),
			parameter = stringAndColor6,
			time = 15.5f
		};
		MovieEventCustom movieEventCustom17 = movieEventCustom;
		movieEventCustom = new MovieEventCustom()
		{
			Callback = new Action<object>(this, __TutorialScript.PlayMovieSpeech),
			parameter = "tutorial_movie_voice_06",
			time = 15.5f
		};
		MovieEventCustom movieEventCustom18 = movieEventCustom;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "LaserTorpedoAria3",
			prefabName = "Tutorial/Laser_torpedo",
			position = new Vector3(176f, 0f, -26f),
			rotation = new Vector3(0f, 147f, 0f),
			time = 12.9f
		};
		MovieEventAddGameObject movieEventAddGameObject11 = movieEventAddGameObject;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "LaserAriaTorpedo4",
			prefabName = "Tutorial/Shoot_aria",
			position = new Vector3(164f, -0.3f, -6.5f),
			rotation = new Vector3(0f, -32f, 0f),
			time = 13f
		};
		MovieEventAddGameObject movieEventAddGameObject12 = movieEventAddGameObject;
		MovieEventRemoveGameObject movieEventRemoveGameObject = new MovieEventRemoveGameObject()
		{
			gameObjectName = "LaserAriaTorpedo",
			time = 13.5f
		};
		MovieEventRemoveGameObject movieEventRemoveGameObject1 = movieEventRemoveGameObject;
		movieEventRemoveGameObject = new MovieEventRemoveGameObject()
		{
			gameObjectName = "LaserTorpedoAria",
			time = 14f
		};
		MovieEventRemoveGameObject movieEventRemoveGameObject2 = movieEventRemoveGameObject;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "LaserPlayerTorpedo3",
			prefabName = "Tutorial/Shoot_battlecruise",
			position = new Vector3(157f, -1f, -15f),
			rotation = new Vector3(0f, -65f, 0f),
			time = 14f
		};
		MovieEventAddGameObject movieEventAddGameObject13 = movieEventAddGameObject;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "LaserTorpedoPlayer2",
			prefabName = "Tutorial/Laser_torpedo",
			position = new Vector3(174f, -0.6f, -26f),
			rotation = new Vector3(0f, 123f, 0f),
			time = 14.1f
		};
		MovieEventAddGameObject movieEventAddGameObject14 = movieEventAddGameObject;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "LaserPlayerTorpedo4",
			prefabName = "Tutorial/Shoot_battlecruise",
			position = new Vector3(157f, -1f, -15f),
			rotation = new Vector3(0f, -65f, 0f),
			time = 14.3f
		};
		MovieEventAddGameObject movieEventAddGameObject15 = movieEventAddGameObject;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "AriaCapsule",
			prefabName = "Tutorial/Capsule_tutorial",
			position = new Vector3(162.5f, -1f, -5.5f),
			rotation = new Vector3(6.6f, -18.6f, -51f),
			time = 14.5f
		};
		MovieEventAddGameObject movieEventAddGameObject16 = movieEventAddGameObject;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "Dron",
			prefabName = "Tutorial/Drone_tutorial",
			position = new Vector3(175f, -1.5f, -25f),
			rotation = new Vector3(0f, -9f, 0f),
			time = 14.5f
		};
		MovieEventAddGameObject movieEventAddGameObject17 = movieEventAddGameObject;
		movieEventCustom = new MovieEventCustom()
		{
			Callback = new Action<object>(this, __TutorialScript.SpawnDetonator),
			time = 15f
		};
		MovieEventCustom movieEventCustom19 = movieEventCustom;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "LaserTorpedoPlayer3",
			prefabName = "Tutorial/Laser_torpedo",
			position = new Vector3(174f, -0.6f, -26f),
			rotation = new Vector3(0f, 123f, 0f),
			time = 15.5f
		};
		MovieEventAddGameObject movieEventAddGameObject18 = movieEventAddGameObject;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "FlamePlayer",
			prefabName = "Tutorial/Smoke_animation",
			position = new Vector3(154.5f, 1f, -12.5f),
			rotation = new Vector3(14f, 25.5f, 337f),
			time = 16.5f
		};
		MovieEventAddGameObject movieEventAddGameObject19 = movieEventAddGameObject;
		movieEventRemoveGameObject = new MovieEventRemoveGameObject()
		{
			gameObjectName = "Aria",
			time = 15.5f
		};
		MovieEventRemoveGameObject movieEventRemoveGameObject3 = movieEventRemoveGameObject;
		movieEventRemoveGameObject = new MovieEventRemoveGameObject()
		{
			gameObjectName = "FlameAria",
			time = 16.5f
		};
		MovieEventRemoveGameObject movieEventRemoveGameObject4 = movieEventRemoveGameObject;
		movieEventCustom = new MovieEventCustom()
		{
			Callback = new Action<object>(this, __TutorialScript.HideSubtitle),
			time = 19.8f
		};
		MovieEventCustom movieEventCustom20 = movieEventCustom;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "LaserTorpedoPlayer4",
			prefabName = "Tutorial/Laser_torpedo",
			position = new Vector3(174f, -0.6f, -26f),
			rotation = new Vector3(0f, 123f, 0f),
			time = 16.9f
		};
		MovieEventAddGameObject movieEventAddGameObject20 = movieEventAddGameObject;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "LaserPlayerTorpedo5",
			prefabName = "Tutorial/Shoot_battlecruise",
			position = new Vector3(157f, -1f, -15f),
			rotation = new Vector3(0f, -65f, 0f),
			time = 17f
		};
		MovieEventAddGameObject movieEventAddGameObject21 = movieEventAddGameObject;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "LaserPlayerTorpedo6",
			prefabName = "Tutorial/Shoot_battlecruise",
			position = new Vector3(157f, -1f, -15f),
			rotation = new Vector3(0f, -65f, 0f),
			time = 17.5f
		};
		MovieEventAddGameObject movieEventAddGameObject22 = movieEventAddGameObject;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "LaserPlayerTorpedo7",
			prefabName = "Tutorial/Shoot_battlecruise",
			position = new Vector3(157f, -1f, -15f),
			rotation = new Vector3(0f, -65f, 0f),
			time = 20f
		};
		MovieEventAddGameObject movieEventAddGameObject23 = movieEventAddGameObject;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "LaserPlayerTorpedo8",
			prefabName = "Tutorial/Shoot_battlecruise",
			position = new Vector3(157f, -1f, -15f),
			rotation = new Vector3(0f, -65f, 0f),
			time = 20.1f
		};
		MovieEventAddGameObject movieEventAddGameObject24 = movieEventAddGameObject;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "LaserTorpedoPlayer5",
			prefabName = "Tutorial/Laser_torpedo",
			position = new Vector3(174f, -0.6f, -26f),
			rotation = new Vector3(0f, 123f, 0f),
			time = 23.2f
		};
		MovieEventAddGameObject movieEventAddGameObject25 = movieEventAddGameObject;
		movieEventRemoveGameObject = new MovieEventRemoveGameObject()
		{
			gameObjectName = "LaserPlayerTorpedo",
			time = 23.5f
		};
		MovieEventRemoveGameObject movieEventRemoveGameObject5 = movieEventRemoveGameObject;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "LaserTorpedoPlayer6",
			prefabName = "Tutorial/Laser_torpedo",
			position = new Vector3(174f, -0.6f, -26f),
			rotation = new Vector3(0f, 123f, 0f),
			time = 23.6f
		};
		MovieEventAddGameObject movieEventAddGameObject26 = movieEventAddGameObject;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "LaserPlayerTorpedo9",
			prefabName = "Tutorial/Shoot_battlecruise",
			position = new Vector3(157f, -1f, -15f),
			rotation = new Vector3(0f, -65f, 0f),
			time = 23.6f
		};
		MovieEventAddGameObject movieEventAddGameObject27 = movieEventAddGameObject;
		movieEventAddGameObject = new MovieEventAddGameObject()
		{
			gameObjectName = "LaserPlayerTorpedo10",
			prefabName = "Tutorial/Shoot_battlecruise",
			position = new Vector3(157f, -1f, -15f),
			rotation = new Vector3(0f, -65f, 0f),
			time = 24f
		};
		MovieEventAddGameObject movieEventAddGameObject28 = movieEventAddGameObject;
		movieEventRemoveGameObject = new MovieEventRemoveGameObject()
		{
			gameObjectName = "LaserTorpedoPlayer",
			time = 24f
		};
		MovieEventRemoveGameObject movieEventRemoveGameObject6 = movieEventRemoveGameObject;
		movieEventPlayAnimation = new MovieEventPlayAnimation()
		{
			animationName = "Take 001",
			gameObjectName = "Player",
			time = 25.5f
		};
		MovieEventPlayAnimation movieEventPlayAnimation3 = movieEventPlayAnimation;
		movieEventCustom = new MovieEventCustom()
		{
			Callback = new Action<object>(this, __TutorialScript.DestroyAllObjectsAndShowMenu),
			time = 30f
		};
		MovieEventCustom movieEventCustom21 = movieEventCustom;
		MovieEvent[] movieEventArray = new MovieEvent[58];
		movieEventArray[0] = movieEventCustom1;
		movieEventArray[1] = movieEventCustom2;
		movieEventArray[2] = movieEventCustom3;
		movieEventArray[3] = movieEventCustom4;
		movieEventArray[4] = movieEventPlayAnimation1;
		MovieEvent[] movieEventArray1 = movieEventArray;
		if (!this.isRussianServer)
		{
			movieEvent = movieEventCustom5;
		}
		else
		{
			movieEvent = null;
		}
		movieEventArray1[5] = movieEvent;
		movieEventArray[6] = movieEventCustom6;
		movieEventArray[7] = movieEventCustom7;
		movieEventArray[8] = movieEventCustom8;
		MovieEvent[] movieEventArray2 = movieEventArray;
		if (!this.isRussianServer)
		{
			movieEvent1 = movieEventCustom9;
		}
		else
		{
			movieEvent1 = null;
		}
		movieEventArray2[9] = movieEvent1;
		movieEventArray[10] = movieEventCustom10;
		movieEventArray[11] = movieEventAddGameObject1;
		movieEventArray[12] = movieEventPlayAnimation2;
		movieEventArray[13] = movieEventCustom11;
		MovieEvent[] movieEventArray3 = movieEventArray;
		if (!this.isRussianServer)
		{
			movieEvent2 = movieEventCustom12;
		}
		else
		{
			movieEvent2 = null;
		}
		movieEventArray3[14] = movieEvent2;
		movieEventArray[15] = movieEventCustom13;
		MovieEvent[] movieEventArray4 = movieEventArray;
		if (!this.isRussianServer)
		{
			movieEvent3 = movieEventCustom14;
		}
		else
		{
			movieEvent3 = null;
		}
		movieEventArray4[16] = movieEvent3;
		movieEventArray[17] = movieEventAddGameObject2;
		movieEventArray[18] = movieEventAddGameObject3;
		movieEventArray[19] = movieEventCustom15;
		MovieEvent[] movieEventArray5 = movieEventArray;
		if (!this.isRussianServer)
		{
			movieEvent4 = movieEventCustom16;
		}
		else
		{
			movieEvent4 = null;
		}
		movieEventArray5[20] = movieEvent4;
		movieEventArray[21] = movieEventAddGameObject4;
		movieEventArray[22] = movieEventAddGameObject5;
		movieEventArray[23] = movieEventAddGameObject6;
		movieEventArray[24] = movieEventAddGameObject7;
		movieEventArray[25] = movieEventAddGameObject8;
		movieEventArray[26] = movieEventAddGameObject9;
		movieEventArray[27] = movieEventAddGameObject10;
		movieEventArray[28] = movieEventCustom17;
		MovieEvent[] movieEventArray6 = movieEventArray;
		if (!this.isRussianServer)
		{
			movieEvent5 = movieEventCustom18;
		}
		else
		{
			movieEvent5 = null;
		}
		movieEventArray6[29] = movieEvent5;
		movieEventArray[30] = movieEventAddGameObject11;
		movieEventArray[31] = movieEventAddGameObject12;
		movieEventArray[32] = movieEventRemoveGameObject1;
		movieEventArray[33] = movieEventRemoveGameObject2;
		movieEventArray[34] = movieEventAddGameObject13;
		movieEventArray[35] = movieEventAddGameObject14;
		movieEventArray[36] = movieEventAddGameObject15;
		movieEventArray[37] = movieEventAddGameObject16;
		movieEventArray[38] = movieEventAddGameObject17;
		movieEventArray[39] = movieEventCustom19;
		movieEventArray[40] = movieEventAddGameObject18;
		movieEventArray[41] = movieEventRemoveGameObject3;
		movieEventArray[42] = movieEventAddGameObject19;
		movieEventArray[43] = movieEventRemoveGameObject4;
		movieEventArray[44] = movieEventCustom20;
		movieEventArray[45] = movieEventAddGameObject20;
		movieEventArray[46] = movieEventAddGameObject21;
		movieEventArray[47] = movieEventAddGameObject22;
		movieEventArray[48] = movieEventAddGameObject23;
		movieEventArray[49] = movieEventAddGameObject24;
		movieEventArray[50] = movieEventAddGameObject25;
		movieEventArray[51] = movieEventRemoveGameObject5;
		movieEventArray[52] = movieEventAddGameObject26;
		movieEventArray[53] = movieEventAddGameObject27;
		movieEventArray[54] = movieEventAddGameObject28;
		movieEventArray[55] = movieEventRemoveGameObject6;
		movieEventArray[56] = movieEventPlayAnimation3;
		movieEventArray[57] = movieEventCustom21;
		this.moveiScript.StartMovie(movieEventArray);
	}

	private void StartMoveCamera(object prm)
	{
		this.moveCamera = true;
		float single = Vector3.Distance(Camera.get_main().get_transform().get_position(), this.cameraStartingAnimationPoint);
		this.cameraSpeed = single / 0.5f;
		this.UpdateCameraPosition();
	}

	private void SwapOldPlayer()
	{
		Object prefab = playWebGame.assets.GetPrefab("Tutorial/Battlecruiser_tutorial");
		GameObject gameObject = (GameObject)Object.Instantiate(prefab);
		gameObject.set_name("Player");
		gameObject.get_transform().set_position(new Vector3(174f, 1f, -23f));
		gameObject.get_transform().set_eulerAngles(new Vector3(0f, 24.5f, 0f));
		Object.Destroy(this.oldPlayer);
	}

	private void Update()
	{
		if (this.moveCamera)
		{
			this.UpdateCameraPosition();
		}
		if (this.isPlayerInMove)
		{
			this.UpdatePlayerPosition();
		}
		if ((float)Screen.get_height() != this.currentScreenHeight || (float)Screen.get_width() != this.currentScreenWidth)
		{
			this.currentScreenHeight = (float)Screen.get_height();
			this.currentScreenWidth = (float)Screen.get_width();
			this.ReorderGUI();
			if (this.movieBorderUpdate != null)
			{
				this.movieBorderUpdate.Invoke();
			}
		}
		if (NetworkScript.player == null || NetworkScript.player.playerBelongings == null || NetworkScript.player.shipScript == null || this.isMenuOff)
		{
			this.ManageSkipMovieKey();
			return;
		}
		this.ManageQuestIdleTimer();
		if (!this.isPartyArrowInitialize)
		{
			IEnumerator<GameObjectPhysics> enumerator = NetworkScript.player.shipScript.comm.gameObjects.get_Values().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					GameObjectPhysics current = enumerator.get_Current();
					if (!(current is PvEPhysics) || !(((PvEPhysics)current).playerName == "key_pve_aria"))
					{
						continue;
					}
					this.oldAria = (GameObject)current.gameObject;
					this.isPartyArrowInitialize = true;
					this.InitializeGuidingText();
					this.InitGuidingArrow();
				}
			}
			finally
			{
				if (enumerator == null)
				{
				}
				enumerator.Dispose();
			}
		}
		questState _questState = NetworkScript.player.playerBelongings.TutorialQuestStatus(this.currentQuestId);
		if (this.guidePointCoordinates.x != 0f && NetworkScript.player.vessel.x > this.guidePointCoordinates.x - 10f && Vector3.Distance(new Vector3(NetworkScript.player.vessel.x, NetworkScript.player.vessel.y, NetworkScript.player.vessel.z), this.guidePointCoordinates) < 8f)
		{
			this.RemoveGuidePointResetQuestTimer();
		}
		else if (!this.guidePoint.get_activeSelf() && _questState != 2)
		{
			this.guidePoint.SetActive(true);
			this.questIdleTimer = 7f;
			this.isQuestIdleTimerStarted = true;
		}
		if (!this.isTutorialGuiInitialized && AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.sideMenuWindow != null)
		{
			AndromedaGui.mainWnd.btnShipConfig.isEnabled = false;
			AndromedaGui.mainWnd.btnFushion.isEnabled = false;
			AndromedaGui.mainWnd.btnUniversMap.isEnabled = false;
			AndromedaGui.mainWnd.btnTransformer.isEnabled = false;
			AndromedaGui.mainWnd.btnLevelUp.isEnabled = false;
			AndromedaGui.mainWnd.btnPlayerProfile.isEnabled = false;
			AndromedaGui.mainWnd.btnChat.isEnabled = false;
			AndromedaGui.mainWnd.btnGuild.isEnabled = false;
			AndromedaGui.mainWnd.btnPVP.isEnabled = false;
			AndromedaGui.mainWnd.btnNovaShop.isEnabled = false;
			AndromedaGui.mainWnd.btnRanking.isEnabled = false;
			this.InitTutorialGUI();
		}
		if (NetworkScript.player.playerBelongings.playerQuests.get_Count() > 0 && Enumerable.First<int>(NetworkScript.player.playerBelongings.playerQuests.get_Keys()) != this.currentQuestId)
		{
			this.isActionSet = false;
			this.currentQuestId = Enumerable.First<int>(NetworkScript.player.playerBelongings.playerQuests.get_Keys());
			if (this.currentQuestId >= 10002)
			{
				AndromedaGui.mainWnd.btnShipConfig.isEnabled = true;
				AndromedaGui.mainWnd.btnFushion.isEnabled = true;
			}
		}
		if (this.currentQuestId == 10001 && NetworkScript.player.playerBelongings.TutorialQuestStatus(this.currentQuestId) == 2 && QuestInfoWindow.IsOnScreen)
		{
			this.isCargoArrowShowed = true;
		}
		if (_questState != this.currentQuestState)
		{
			this.currentQuestState = _questState;
		}
		if (NetworkScript.player.shipScript.isHotKeysActive)
		{
			NetworkScript.player.shipScript.isHotKeysActive = false;
		}
		if (TargetingWnd.wndTargeting != null)
		{
			if (TargetingWnd.btnChat != null)
			{
				TargetingWnd.btnChat.isEnabled = false;
			}
			if (TargetingWnd.btnInvite != null)
			{
				TargetingWnd.btnInvite.isEnabled = false;
			}
			if (TargetingWnd.btnProfile != null)
			{
				TargetingWnd.btnProfile.isEnabled = false;
			}
		}
		if ((_questState != 2 || QuestInfoWindow.IsOnScreen || AndromedaGui.mainWnd.activWindowIndex != 255) && (_questState != 1 || QuestInfoWindow.IsOnScreen || this.IsTutorialStepInitialized(this.currentQuestId) || AndromedaGui.mainWnd.activWindowIndex != 255))
		{
			this.RemoveQuestTrackerArrow();
		}
		else
		{
			this.InitQuestTrackerArrow();
		}
		if (this.currentQuestId != 10000 || _questState != 1 || CheckpointDialogWindow.wnd == null)
		{
			this.ClearCheckpointPopUpArrow();
			this.RemoveCheckPointPopUpArrow();
		}
		else
		{
			this.SetCheckpoinPopUpArrow();
			this.InitCheckPointPopUpArrow();
		}
		if (!QuestInfoWindow.IsOnScreen || this.currentQuestState != 2 && _questState != 1)
		{
			this.RemoveCollectRewardArrowWindow();
			this.RemoveCloseQuestArrowWindow();
		}
		else if (this.currentQuestState == 2)
		{
			this.InitCollectRewardArrow((float)(Screen.get_width() / 2 + 220), (float)(Screen.get_height() / 2 - 110));
			this.isQuestInfoArrowNeeded = false;
		}
		else if (this.currentQuestId == 10000 || this.currentQuestId == 10002 || this.currentQuestId == 10001 && Enumerable.First<NewQuest>(Enumerable.Where<NewQuest>(StaticData.allQuests, new Func<NewQuest, bool>(this, (NewQuest t) => t.id == this.currentQuestId))).objectives.get_Item(0).IsComplete(NetworkScript.player))
		{
			this.InitCloseQuestArrow((float)(Screen.get_width() / 2 + 335), (float)(Screen.get_height() / 2 - 255));
		}
		else
		{
			this.InitCloseQuestArrow((float)(Screen.get_width() / 2 + 335), (float)(Screen.get_height() / 2 - 15));
		}
		if (!QuestInfoWindow.IsOnScreen)
		{
			if (AndromedaGui.mainWnd.activWindowIndex != 16 && AndromedaGui.mainWnd.activWindowIndex != 6 && AndromedaGui.mainWnd.OnCloseWindowCallback != null)
			{
				AndromedaGui.mainWnd.OnCloseWindowCallback = null;
			}
			this.isActionSet = false;
		}
		else if (!this.isActionSet)
		{
			switch (this.currentQuestId)
			{
				case 10000:
				{
					if (this.stepOneInitialized)
					{
						QuestInfoWindow.OnCLose = null;
					}
					else
					{
						QuestInfoWindow.OnCLose = new Action(this, __TutorialScript.InitializedStepOne);
					}
					break;
				}
				case 10001:
				{
					if (this.stepTwoInitialized)
					{
						QuestInfoWindow.OnCLose = null;
					}
					else
					{
						QuestInfoWindow.OnCLose = new Action(this, __TutorialScript.InitializedStepTwo);
					}
					break;
				}
				case 10002:
				{
					if (this.stepThreeInitialized)
					{
						QuestInfoWindow.OnCLose = null;
					}
					else
					{
						QuestInfoWindow.OnCLose = new Action(this, __TutorialScript.InitializedStepThree);
					}
					break;
				}
				case 10003:
				{
					if (this.stepFourInitialized)
					{
						QuestInfoWindow.OnCLose = null;
					}
					else
					{
						QuestInfoWindow.OnCLose = new Action(this, __TutorialScript.InitializedStepFour);
					}
					break;
				}
				case 10004:
				{
					if (this.stepFiveInitialized)
					{
						QuestInfoWindow.OnCLose = null;
					}
					else
					{
						QuestInfoWindow.OnCLose = new Action(this, __TutorialScript.InitializedStepFive);
					}
					break;
				}
			}
			this.isActionSet = true;
		}
		if (AndromedaGui.mainWnd.activWindowIndex != 1 && (this.wsArrowOne != null || this.wsArrowTwo != null || this.wsArrowThree != null || this.wsArrowFour != null || this.wsArrowFive != null))
		{
			this.wsArrowOne = null;
			this.wsArrowTwo = null;
			this.wsArrowThree = null;
			this.wsArrowFour = null;
			this.wsArrowFive = null;
		}
		if (this.currentQuestId == 10001 && NetworkScript.player.playerBelongings.TutorialQuestStatus(this.currentQuestId) == 2)
		{
			if (AndromedaGui.mainWnd.activWindowIndex != 255 || QuestInfoWindow.IsOnScreen || this.isCargoArrowShowed)
			{
				this.RemoveSmallDownArrow();
			}
			else
			{
				this.InitSmallArrowDown((float)(Screen.get_width() / 2 + 65), (float)(Screen.get_height() - 210), StaticData.Translate("key_tutorial_cargo_arrow"));
			}
		}
		if (this.currentQuestId == 10002 && _questState == 1 && !QuestInfoWindow.IsOnScreen && this.IsTutorialStepInitialized(this.currentQuestId))
		{
			if (!Enumerable.First<NewQuest>(Enumerable.Where<NewQuest>(StaticData.allQuests, new Func<NewQuest, bool>(this, (NewQuest t) => t.id == this.currentQuestId))).objectives.get_Item(0).IsComplete(NetworkScript.player))
			{
				if (AndromedaGui.mainWnd.activWindowIndex == 0)
				{
					if (!((__FusionWindow)AndromedaGui.mainWnd.activeWindow).btnAmmo.IsClicked)
					{
						this.InitGuide(398f, 62f, AndromedaGui.mainWnd.activeWindow);
					}
					else
					{
						this.InitGuide(533f, 221f, AndromedaGui.mainWnd.activeWindow);
					}
					this.RemoveSmallArrow();
				}
				else
				{
					this.InitGuide(19f, 195f, AndromedaGui.mainWnd.sideMenuWindow);
				}
			}
			else if (Enumerable.First<NewQuest>(Enumerable.Where<NewQuest>(StaticData.allQuests, new Func<NewQuest, bool>(this, (NewQuest t) => t.id == this.currentQuestId))).objectives.get_Item(1).IsComplete(NetworkScript.player))
			{
				this.RemoveWsArrowOne();
				this.RemoveWsArrowTwo();
				this.RemoveWsArrowThree();
				this.RemoveWsArrowFour();
				this.RemoveWsArrowFive();
			}
			else if (AndromedaGui.mainWnd.activWindowIndex == 1)
			{
				this.RemoveGuide();
				List<SlotItem> list = NetworkScript.player.playerBelongings.playerItems.slotItems;
				if (__TutorialScript.<>f__am$cache58 == null)
				{
					__TutorialScript.<>f__am$cache58 = new Func<SlotItem, bool>(null, (SlotItem s) => (!PlayerItems.IsWeapon(s.get_ItemType()) ? false : s.get_SlotType() == 1));
				}
				if (Enumerable.Count<SlotItem>(Enumerable.Where<SlotItem>(list, __TutorialScript.<>f__am$cache58)) <= 0)
				{
					this.RemoveSmallArrow();
				}
				else
				{
					this.InitSmallArrow(AndromedaGui.mainWnd.activeWindow.boundries.get_x() + 105f, AndromedaGui.mainWnd.activeWindow.boundries.get_y() + 312f, StaticData.Translate("key_tutorial_weapon_arrow"), true);
				}
				if (NetworkScript.player.vessel.cfg.weaponSlots[2].get_WeaponStatus() != 1 && NetworkScript.player.vessel.cfg.weaponSlots[2].selectedAmmoItemType != PlayerItems.TypeAmmoFusionCells)
				{
					this.InitWsArrowThree();
				}
			}
			else if (AndromedaGui.mainWnd.activWindowIndex != 0)
			{
				this.InitGuide(20f, 150f, AndromedaGui.mainWnd.sideMenuWindow);
				this.RemoveSmallArrow();
			}
			else
			{
				this.InitGuide(849f, 10f, AndromedaGui.mainWnd.activeWindow);
			}
		}
		if (this.currentQuestId == 10002)
		{
			NewQuest[] newQuestArray = StaticData.allQuests;
			if (__TutorialScript.<>f__am$cache59 == null)
			{
				__TutorialScript.<>f__am$cache59 = new Func<NewQuest, bool>(null, (NewQuest t) => t.id == 10002);
			}
			if (Enumerable.First<NewQuest>(Enumerable.Where<NewQuest>(newQuestArray, __TutorialScript.<>f__am$cache59)).objectives.get_Item(1).IsComplete(NetworkScript.player))
			{
				if (AndromedaGui.mainWnd.activWindowIndex != 1)
				{
					this.RemoveGuide();
				}
				else
				{
					this.InitGuide(860f, 10f, AndromedaGui.mainWnd.activeWindow);
				}
			}
		}
		if (NetworkScript.player.playerBelongings.TutorialQuestStatus(10002) == 2 || NetworkScript.player.playerBelongings.TutorialQuestStatus(10002) == 3)
		{
			this.RemoveWsArrowOne();
			this.RemoveWsArrowTwo();
			this.RemoveWsArrowThree();
			this.RemoveWsArrowFour();
			this.RemoveWsArrowFive();
		}
		if (this.currentQuestId == 10004 && this.isAriaDied && !this.isLastPhaseStarted)
		{
			if (this.delayAfterDeath <= 0f)
			{
				this.isLastPhaseStarted = true;
				this.Explode(null);
			}
			else
			{
				__TutorialScript _deltaTime = this;
				_deltaTime.delayAfterDeath = _deltaTime.delayAfterDeath - Time.get_deltaTime();
			}
		}
		if (this.isLastPhaseStarted && !this.isShipChanged)
		{
			if (this.delayBeforShipChange <= 0f)
			{
				this.ChangeShip(null);
			}
			else
			{
				__TutorialScript _TutorialScript = this;
				_TutorialScript.delayBeforShipChange = _TutorialScript.delayBeforShipChange - Time.get_deltaTime();
			}
		}
		if (this.isLastPhaseStarted && this.isShipChanged && !this.isEscapeToHydraScreenLoaded)
		{
			if (this.delayBeforStartJumping <= 0f)
			{
				this.isEscapeToHydraScreenLoaded = true;
				MainScreenWindow mainScreenWindow = AndromedaGui.mainWnd;
				EventHandlerParam eventHandlerParam = new EventHandlerParam()
				{
					customData = (byte)16
				};
				mainScreenWindow.OnWindowBtnClicked(eventHandlerParam);
				AndromedaGui.mainWnd.OnCloseWindowCallback = new Action(this, __TutorialScript.GoToHydra);
			}
			else
			{
				__TutorialScript _deltaTime1 = this;
				_deltaTime1.delayBeforStartJumping = _deltaTime1.delayBeforStartJumping - Time.get_deltaTime();
			}
		}
		if (this.isShipChanged && this.isHyperJumpStarted && !this.isSkipTutorialCommandSent)
		{
			if (this.delayBeforFinishTutorial <= 0f)
			{
				this.isSkipTutorialCommandSent = true;
				this.OnSkipTutorialClicked(null);
			}
			else
			{
				__TutorialScript _TutorialScript1 = this;
				_TutorialScript1.delayBeforFinishTutorial = _TutorialScript1.delayBeforFinishTutorial - Time.get_deltaTime();
			}
		}
		if (!this.isMenuOff && NetworkScript.player.vessel.x > 90f)
		{
			this.RemoveGuidePointResetQuestTimer();
			this.StartFinalMovie();
		}
		if (NetworkScript.player.shipScript.p.isSpeedBoostActivated)
		{
			this.HideBoostHint();
		}
		if (this.guidingText != null)
		{
			switch (this.currentQuestId)
			{
				case 10000:
				{
					if (_questState == 2 || _questState == 3)
					{
						this.HideGuidingText();
					}
					else if (NetworkScript.player.vessel.x >= -90f)
					{
						this.UpdateGuidingText(StaticData.Translate("key_tutorial_guiding_arrow_lbl_movement2"), 1);
					}
					else
					{
						this.UpdateGuidingText(StaticData.Translate("key_tutorial_guiding_arrow_lbl_movement1"), 2);
					}
					break;
				}
				case 10001:
				{
					if (_questState == 2 || _questState == 3)
					{
						this.HideGuidingText();
					}
					else if (NetworkScript.player.vessel.x > -62f && NetworkScript.player.vessel.x < -26f)
					{
						this.HideGuidingText();
					}
					else if (NetworkScript.player.vessel.x < 0.15f)
					{
						this.UpdateGuidingText(StaticData.Translate("key_tutorial_guiding_arrow_lbl_movement3"), 2);
					}
					else if (NetworkScript.player.shipScript.selectedObject == null || !(NetworkScript.player.shipScript.selectedObject is Mineral))
					{
						this.UpdateGuidingText(StaticData.Translate("key_tutorial_guiding_arrow_lbl_miningnew"), 1);
					}
					else if (NetworkScript.player.vessel.miningState != 0)
					{
						this.UpdateGuidingText(StaticData.Translate("key_tutorial_guiding_arrow_lbl_mining2"), 1);
					}
					else
					{
						this.UpdateGuidingText(StaticData.Translate("key_tutorial_guiding_arrow_lbl_miningnew"), 2);
					}
					break;
				}
				case 10002:
				{
					this.HideGuidingText();
					break;
				}
				case 10003:
				{
					if (_questState == 2 || _questState == 3)
					{
						this.HideGuidingText();
					}
					else if (NetworkScript.player.vessel.x < 57f)
					{
						this.HideGuidingText();
					}
					else if (NetworkScript.player.shipScript.selectedObject == null || !(NetworkScript.player.shipScript.selectedObject is PvEPhysics))
					{
						this.UpdateGuidingText(StaticData.Translate("key_tutorial_guiding_arrow_lbl_shootingnew"), 1);
					}
					else if (NetworkScript.player.vessel.isShooting)
					{
						this.UpdateGuidingText(StaticData.Translate("key_tutorial_guiding_arrow_lbl_shooting2"), 2);
					}
					else
					{
						this.UpdateGuidingText(StaticData.Translate("key_tutorial_guiding_arrow_lbl_shootingnew"), 1);
					}
					break;
				}
				case 10004:
				{
					if (_questState != 3)
					{
						this.HideGuidingText();
					}
					break;
				}
			}
		}
		if (this.guideArrowTargetObject == null)
		{
			this.guideArrowIsSet = false;
		}
		if (this.guideArrow != null && !this.guideArrowIsSet)
		{
			switch (this.currentQuestId)
			{
				case 10000:
				{
					if (_questState == 2 || _questState == 3)
					{
						this.SetTargetArrowDestination(NetworkScript.player.shipScript.get_gameObject());
					}
					else
					{
						IEnumerator<GameObjectPhysics> enumerator1 = NetworkScript.player.shipScript.comm.gameObjects.get_Values().GetEnumerator();
						try
						{
							while (enumerator1.MoveNext())
							{
								GameObjectPhysics gameObjectPhysic = enumerator1.get_Current();
								if (!(gameObjectPhysic is PvEPhysics) || !(((PvEPhysics)gameObjectPhysic).playerName == "key_pve_aria"))
								{
									continue;
								}
								this.SetTargetArrowDestination((GameObject)((PvEPhysics)gameObjectPhysic).gameObject);
							}
						}
						finally
						{
							if (enumerator1 == null)
							{
							}
							enumerator1.Dispose();
						}
					}
					break;
				}
				case 10001:
				{
					if (_questState != 2 && _questState != 3)
					{
						IList<GameObjectPhysics> values = NetworkScript.player.shipScript.comm.gameObjects.get_Values();
						if (__TutorialScript.<>f__am$cache5A == null)
						{
							__TutorialScript.<>f__am$cache5A = new Func<GameObjectPhysics, bool>(null, (GameObjectPhysics t) => t is Mineral);
						}
						IEnumerable<GameObjectPhysics> enumerable = Enumerable.Where<GameObjectPhysics>(values, __TutorialScript.<>f__am$cache5A);
						if (__TutorialScript.<>f__am$cache5B == null)
						{
							__TutorialScript.<>f__am$cache5B = new Func<GameObjectPhysics, uint>(null, (GameObjectPhysics s) => s.neighbourhoodId);
						}
						GameObjectPhysics gameObjectPhysic1 = Enumerable.FirstOrDefault<GameObjectPhysics>(Enumerable.OrderBy<GameObjectPhysics, uint>(enumerable, __TutorialScript.<>f__am$cache5B));
						if (gameObjectPhysic1 != null)
						{
							this.SetTargetArrowDestination((GameObject)gameObjectPhysic1.gameObject);
						}
					}
					else if (this.guideArrow != null)
					{
						this.SetTargetArrowDestination(NetworkScript.player.shipScript.get_gameObject());
					}
					break;
				}
				case 10002:
				{
					this.SetTargetArrowDestination(NetworkScript.player.shipScript.get_gameObject());
					break;
				}
				case 10003:
				{
					if (_questState != 2 && _questState != 3)
					{
						IList<GameObjectPhysics> values1 = NetworkScript.player.shipScript.comm.gameObjects.get_Values();
						if (__TutorialScript.<>f__am$cache5C == null)
						{
							__TutorialScript.<>f__am$cache5C = new Func<GameObjectPhysics, bool>(null, (GameObjectPhysics t) => (!(t is PvEPhysics) ? false : ((PvEPhysics)t).playerName != "key_pve_aria"));
						}
						IEnumerable<GameObjectPhysics> enumerable1 = Enumerable.Where<GameObjectPhysics>(values1, __TutorialScript.<>f__am$cache5C);
						if (__TutorialScript.<>f__am$cache5D == null)
						{
							__TutorialScript.<>f__am$cache5D = new Func<GameObjectPhysics, uint>(null, (GameObjectPhysics s) => s.neighbourhoodId);
						}
						GameObjectPhysics gameObjectPhysic2 = Enumerable.FirstOrDefault<GameObjectPhysics>(Enumerable.OrderBy<GameObjectPhysics, uint>(enumerable1, __TutorialScript.<>f__am$cache5D));
						if (gameObjectPhysic2 != null)
						{
							this.SetTargetArrowDestination((GameObject)gameObjectPhysic2.gameObject);
						}
					}
					else if (this.guideArrow != null)
					{
						this.SetTargetArrowDestination(NetworkScript.player.shipScript.get_gameObject());
					}
					break;
				}
				case 10004:
				{
					IEnumerator<GameObjectPhysics> enumerator2 = NetworkScript.player.shipScript.comm.gameObjects.get_Values().GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							GameObjectPhysics current1 = enumerator2.get_Current();
							if (!(current1 is PvEPhysics) || !(((PvEPhysics)current1).playerName == "key_pve_aria"))
							{
								continue;
							}
							this.SetTargetArrowDestination((GameObject)((PvEPhysics)current1).gameObject);
						}
					}
					finally
					{
						if (enumerator2 == null)
						{
						}
						enumerator2.Dispose();
					}
					break;
				}
			}
		}
	}

	private void UpdateCameraPosition()
	{
		if (Vector3.Distance(Camera.get_main().get_transform().get_position(), this.cameraStartingAnimationPoint) <= 0.1f)
		{
			this.moveCamera = false;
		}
		else
		{
			Camera.get_main().get_transform().set_position(Vector3.MoveTowards(Camera.get_main().get_transform().get_position(), this.cameraStartingAnimationPoint, this.cameraSpeed * Time.get_deltaTime()));
		}
	}

	private void UpdateCloseQuestArrowPosition(float x, float y)
	{
		if (this.closeQuestArrowWindow == null)
		{
			return;
		}
		__TutorialScript _deltaTime = this;
		_deltaTime.closeQuestArrowDeltaTime = _deltaTime.closeQuestArrowDeltaTime + Time.get_deltaTime();
		this.closeQuestArrowTexture.boundries.set_y((float)(70 - 70 * Math.Abs(Math.Sin((double)(3f * this.closeQuestArrowDeltaTime)))));
		this.closeQuestArrowWindow.boundries.set_x(x);
		this.closeQuestArrowWindow.boundries.set_y(y);
	}

	private void UpdatecollectRewardArrowPosition(float x, float y)
	{
		if (this.collectRewardArrowWindow == null)
		{
			return;
		}
		__TutorialScript _deltaTime = this;
		_deltaTime.collectRewardArrowDeltaTime = _deltaTime.collectRewardArrowDeltaTime + Time.get_deltaTime();
		this.collectRewardArrowTexture.boundries.set_y((float)(70 - 70 * Math.Abs(Math.Sin((double)(3f * this.collectRewardArrowDeltaTime)))));
		this.collectRewardArrowWindow.boundries.set_x(x);
		this.collectRewardArrowWindow.boundries.set_y(y);
	}

	private void UpdateGuidingText(string newText, int numberOfLine)
	{
		if (this.guidingText == null)
		{
			return;
		}
		TextMesh componentInChildren = this.guidingText.GetComponentInChildren<TextMesh>();
		this.guidingTextDelta = (float)numberOfLine * 1.2f + 3f;
		if (componentInChildren != null && componentInChildren.get_text() != StaticData.Translate("key_tutorial_boost_hint"))
		{
			componentInChildren.set_text(newText);
		}
	}

	private void UpdateGuidingText(string newText, int numberOfLine, Color textColor)
	{
		if (this.guidingText == null)
		{
			return;
		}
		TextMesh componentInChildren = this.guidingText.GetComponentInChildren<TextMesh>();
		this.guidingText.GetComponentInChildren<MeshRenderer>().get_material().set_color(textColor);
		this.guidingTextDelta = (float)numberOfLine * 1.2f + 3f;
		if (componentInChildren != null && componentInChildren.get_text() != StaticData.Translate("key_tutorial_boost_hint"))
		{
			componentInChildren.set_text(newText);
		}
	}

	private void UpdateMovieBorder()
	{
		if (this.upBorder == null || this.downBorder == null || this.fill == null || this.fill2 == null)
		{
			return;
		}
		float _height = (float)Screen.get_height() - (float)Screen.get_width() * 0.5f;
		if (_height < 20f)
		{
			_height = 20f;
		}
		this.upBorder.boundries = new Rect(0f, 0f, (float)Screen.get_width(), _height / 2f);
		this.downBorder.boundries = new Rect(0f, (float)Screen.get_height() - _height / 2f, (float)Screen.get_width(), _height / 2f);
		this.fill.boundries = new Rect(0f, 0f, (float)Screen.get_width(), _height / 2f);
		this.fill2.boundries = this.fill.boundries;
		this.subtitleWindow.boundries = new Rect(0f, this.downBorder.boundries.get_y() - 220f, (float)Screen.get_width(), 220f);
		this.subtitle.boundries = new Rect(this.subtitleWindow.boundries.get_width() * 0.1f, 10f, this.subtitleWindow.boundries.get_width() * 0.8f, 200f);
	}

	private void UpdatePlayerPosition()
	{
		if (Vector3.Distance(this.oldPlayer.get_transform().get_position(), this.playerDestinationPoint) <= 0.1f)
		{
			this.isPlayerInMove = false;
			this.SwapOldPlayer();
		}
		else
		{
			this.oldPlayer.get_transform().LookAt(this.playerDestinationPoint);
			this.oldPlayer.get_transform().set_position(Vector3.MoveTowards(this.oldPlayer.get_transform().get_position(), this.playerDestinationPoint, this.playerSpeed * Time.get_deltaTime()));
		}
	}

	private void UpdatePopUpArrowWindowPosition()
	{
		if (this.popUpArrowWindow == null)
		{
			return;
		}
		this.POP_UP_WINDOW_MAX_Y = CheckpointDialogWindow.wnd.boundries.get_y() + 149f;
		this.POP_UP_WINDOW_X = (float)(Screen.get_width() / 2 + 122);
		__TutorialScript _deltaTime = this;
		_deltaTime.popUpDeltaTime = _deltaTime.popUpDeltaTime + Time.get_deltaTime();
		this.popUpArrowWindow.boundries.set_y((float)((double)this.POP_UP_WINDOW_MAX_Y - 100 * Math.Abs(Math.Sin((double)(3f * this.popUpDeltaTime)))));
		this.popUpArrowWindow.boundries.set_x(this.POP_UP_WINDOW_X);
	}

	private void UpdateQuestTrackerArrowWindowPosition()
	{
		if (this.questTrackerArrowWindow == null)
		{
			return;
		}
		this.QUEST_TRACKER_WINDOW_MAX_Y = 317f;
		__TutorialScript _deltaTime = this;
		_deltaTime.questTrackerDeltaTime = _deltaTime.questTrackerDeltaTime + Time.get_deltaTime();
		this.questTrackerArrowWindow.boundries.set_y((float)((double)this.QUEST_TRACKER_WINDOW_MAX_Y - 100 * Math.Abs(Math.Sin((double)(3f * this.questTrackerDeltaTime)))));
		this.questTrackerArrowWindow.boundries.set_x(this.QUEST_TRACKER_WINDOW_X);
	}

	private void UpdateSmallArrowDownWindowPosition(float x, float y)
	{
		if (this.smallArrowWindowDown == null)
		{
			return;
		}
		__TutorialScript _deltaTime = this;
		_deltaTime.smallArrowDownDeltaTime = _deltaTime.smallArrowDownDeltaTime + Time.get_deltaTime();
		this.smallArrowDownTexture.boundries.set_y((float)(70 - 70 * Math.Abs(Math.Sin((double)(3f * this.smallArrowDownDeltaTime)))));
		this.smallArrowWindowDown.boundries.set_x(x);
		this.smallArrowWindowDown.boundries.set_y(y);
	}

	private void UpdateSmallArrowWindowPosition(float x, float y)
	{
		if (this.smallArrowWindow == null)
		{
			return;
		}
		__TutorialScript _deltaTime = this;
		_deltaTime.smallArrowDeltaTime = _deltaTime.smallArrowDeltaTime + Time.get_deltaTime();
		this.smallArrowTexture.boundries.set_y((float)(70 - 70 * Math.Abs(Math.Sin((double)(3f * this.smallArrowDeltaTime)))));
		this.smallArrowWindow.boundries.set_x(x);
		this.smallArrowWindow.boundries.set_y(y);
	}
}