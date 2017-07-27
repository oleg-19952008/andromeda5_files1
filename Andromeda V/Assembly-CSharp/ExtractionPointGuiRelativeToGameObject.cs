using System;
using UnityEngine;

public class ExtractionPointGuiRelativeToGameObject : MonoBehaviour
{
	private const float WINDOW_WIDTH = 260f;

	private Vector3 screenPos;

	public ExtractionPoint extractionPoint;

	private bool fullExtractionPointInfoOnScreen;

	private GuiWindow bigInfoWindow;

	private GuiWindow extractionPointWindow;

	private GuiWindow spaceWindow;

	private GuiButtonFixed backButton;

	private GuiButtonFixed infoButton;

	private GuiTexture epBoosterIcon;

	private GuiTexture epStateIcon;

	private GuiLabel extractionPointName;

	private GuiNewStyleBar healtBar;

	public ExtractionPointGuiRelativeToGameObject()
	{
	}

	private void HideFullInfoWindow()
	{
		if (this.bigInfoWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.bigInfoWindow.handler);
			this.bigInfoWindow = null;
			this.fullExtractionPointInfoOnScreen = false;
		}
	}

	private void HideMainMenu()
	{
		if (this.extractionPointWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.extractionPointWindow.handler);
			this.extractionPointWindow = null;
			if (NetworkScript.player != null && NetworkScript.player.shipScript)
			{
				NetworkScript.player.shipScript.popUpAction = null;
			}
		}
	}

	private void HideSpaceWindow()
	{
		if (this.spaceWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.spaceWindow.handler);
			this.spaceWindow = null;
		}
	}

	private void OnBackBtnClicked(object prm)
	{
		if (NetworkScript.player.shipScript != null)
		{
			NetworkScript.player.shipScript.isGuiClosed = true;
		}
		this.HideFullInfoWindow();
		this.HideMainMenu();
	}

	private void OnInfoBtnClicked(object prm)
	{
		if (!NetworkScript.player.shipScript.isInControl)
		{
			return;
		}
		try
		{
			if (NetworkScript.player.shipScript != null)
			{
				NetworkScript.player.shipScript.isGuiClosed = true;
			}
			this.HideMainMenu();
			this.fullExtractionPointInfoOnScreen = true;
		}
		catch (Exception exception)
		{
			Debug.LogError(string.Format("OnTalkBtnClicked() {0}", exception));
		}
	}

	private void OnManageBtnClicked(object prm)
	{
		if (!NetworkScript.player.shipScript.isInControl)
		{
			return;
		}
		try
		{
			this.HideMainMenu();
			MainScreenWindow mainScreenWindow = AndromedaGui.mainWnd;
			EventHandlerParam eventHandlerParam = new EventHandlerParam()
			{
				customData = (byte)19
			};
			mainScreenWindow.OnWindowBtnClicked(eventHandlerParam);
			if (NetworkScript.player.shipScript != null)
			{
				NetworkScript.player.shipScript.isGuiClosed = true;
			}
		}
		catch (Exception exception)
		{
			Debug.LogError(string.Format("OnManageBtnClicked() {0}", exception));
		}
	}

	public void Populate()
	{
		this.HideFullInfoWindow();
		this.HideMainMenu();
		this.ShowMainMenu();
		this.HideSpaceWindow();
		this.ShowSpaceWindow();
	}

	private void SetBtnActionToBack()
	{
		this.backButton.Clicked = new Action<EventHandlerParam>(this, ExtractionPointGuiRelativeToGameObject.OnBackBtnClicked);
		if (NetworkScript.player != null && NetworkScript.player.shipScript)
		{
			NetworkScript.player.shipScript.popUpAction = new Action<object>(this, ExtractionPointGuiRelativeToGameObject.OnBackBtnClicked);
		}
	}

	private void ShowFullExtarctionPointInfo()
	{
		if (this.bigInfoWindow != null)
		{
			return;
		}
		this.fullExtractionPointInfoOnScreen = true;
		this.bigInfoWindow = new GuiWindow()
		{
			boundries = new Rect(0f, 0f, 300f, 100f),
			isHidden = false,
			zOrder = 50
		};
		AndromedaGui.gui.AddWindow(this.bigInfoWindow);
		this.backButton = new GuiButtonFixed();
		this.backButton.SetTextureNormal("ActionButtons", "infoFrame");
		this.backButton.SetTextureHover("ActionButtons", "infoFrame");
		this.backButton.SetTextureClicked("ActionButtons", "infoFrame");
		this.backButton.boundries = new Rect(0f, 0f, 300f, 100f);
		this.backButton.Caption = string.Empty;
		this.bigInfoWindow.AddGuiElement(this.backButton);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(7f, 2f, 290f, 14f),
			text = StaticData.Translate(this.extractionPoint.name),
			Alignment = 4,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		this.bigInfoWindow.AddGuiElement(guiLabel);
		string str = StaticData.Translate("key_extraction_point_information");
		GuiAnimatedText guiAnimatedText = new GuiAnimatedText(str, new Action(this, ExtractionPointGuiRelativeToGameObject.SetBtnActionToBack))
		{
			boundries = new Rect(7f, 17f, 290f, 70f),
			FontSize = 11,
			Alignment = 1,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor
		};
		this.bigInfoWindow.AddGuiElement(guiAnimatedText);
		this.backButton.Clicked = new Action<EventHandlerParam>(guiAnimatedText, GuiAnimatedText.ShowAll);
		if (NetworkScript.player != null && NetworkScript.player.shipScript)
		{
			NetworkScript.player.shipScript.popUpAction = new Action<object>(guiAnimatedText, GuiAnimatedText.ShowAll);
		}
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("FrameworkGUI", "empty");
		guiTexture.boundries = new Rect(7f, 69f, 290f, 26f);
		this.bigInfoWindow.AddGuiElement(guiTexture);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(9f, 71f, 285f, 22f),
			text = string.Empty,
			Alignment = 4,
			FontSize = 12,
			TextColor = GuiNewStyleBar.redColor,
			Font = GuiLabel.FontBold
		};
		this.bigInfoWindow.AddGuiElement(guiLabel1);
		if (this.extractionPoint.ownerFraction != NetworkScript.player.vessel.fractionId)
		{
			guiLabel1.text = StaticData.Translate("key_extraction_info_other_fraction");
			guiTexture.SetTextureKeepSize("ActionButtons", "warningFrame");
		}
	}

	private void ShowMainMenu()
	{
		if (this.extractionPointWindow != null)
		{
			return;
		}
		this.extractionPointWindow = new GuiWindow()
		{
			boundries = new Rect(0f, 0f, 225f, 60f),
			isHidden = false,
			zOrder = 50
		};
		AndromedaGui.gui.AddWindow(this.extractionPointWindow);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("ActionButtons", "buttonsLink");
		guiTexture.X = 45f;
		guiTexture.Y = 4f;
		this.extractionPointWindow.AddGuiElement(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("ActionButtons", "epTimerFrame");
		guiTexture1.X = 105f;
		guiTexture1.Y = 4f;
		this.extractionPointWindow.AddGuiElement(guiTexture1);
		this.infoButton = new GuiButtonFixed();
		this.infoButton.SetTexture("ActionButtons", "btnInfo");
		this.infoButton.Caption = string.Empty;
		this.infoButton.X = 0f;
		this.infoButton.Y = 0f;
		if (!this.fullExtractionPointInfoOnScreen)
		{
			this.infoButton.Clicked = new Action<EventHandlerParam>(this, ExtractionPointGuiRelativeToGameObject.OnInfoBtnClicked);
		}
		else
		{
			this.infoButton.Clicked = new Action<EventHandlerParam>(this, ExtractionPointGuiRelativeToGameObject.OnBackBtnClicked);
		}
		this.extractionPointWindow.AddGuiElement(this.infoButton);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("ActionButtons", "btnEp");
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.X = 70f;
		guiButtonFixed.Y = 0f;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, ExtractionPointGuiRelativeToGameObject.OnManageBtnClicked);
		this.extractionPointWindow.AddGuiElement(guiButtonFixed);
		if (AndromedaGui.mainWnd.kb != null)
		{
			GuiLabel guiLabel = new GuiLabel()
			{
				boundries = new Rect(guiButtonFixed.X + 10f, 35f, 40f, 15f),
				Alignment = 6,
				Font = GuiLabel.FontBold,
				FontSize = 14,
				text = AndromedaGui.mainWnd.kb.GetCommandKeyCodeShort(KeyboardCommand.UseKey)
			};
			this.extractionPointWindow.AddGuiElement(guiLabel);
		}
		if (NetworkScript.player != null && NetworkScript.player.shipScript)
		{
			NetworkScript.player.shipScript.popUpAction = new Action<object>(this, ExtractionPointGuiRelativeToGameObject.OnManageBtnClicked);
		}
		if (this.extractionPoint.ownerFraction != NetworkScript.player.vessel.fractionId)
		{
			guiButtonFixed.isEnabled = false;
			if (NetworkScript.player != null && NetworkScript.player.shipScript)
			{
				NetworkScript.player.shipScript.popUpAction = new Action<object>(this, ExtractionPointGuiRelativeToGameObject.OnInfoBtnClicked);
			}
		}
		this.epBoosterIcon = new GuiTexture();
		this.epBoosterIcon.SetTexture("ActionButtons", "epBoostIcon");
		this.epBoosterIcon.X = 70f;
		this.epBoosterIcon.Y = 0f;
		this.epBoosterIcon.isHoverAware = false;
		this.extractionPointWindow.AddGuiElement(this.epBoosterIcon);
		if (this.extractionPoint.damageReductionBoostEndTime <= StaticData.now)
		{
			this.epBoosterIcon.SetTextureKeepSize("FrameworkGUI", "empty");
		}
		else
		{
			this.epBoosterIcon.SetTexture("ActionButtons", "epBoostIcon");
		}
		this.epStateIcon = new GuiTexture();
		this.epStateIcon.SetTexture("ActionButtons", "epAttackIcon");
		this.epStateIcon.X = 131f;
		this.epStateIcon.Y = 15f;
		this.extractionPointWindow.AddGuiElement(this.epStateIcon);
		if (this.extractionPoint.invulnerableModeEndTime <= StaticData.now)
		{
			this.epStateIcon.SetTexture("ActionButtons", "epAttackIcon");
			TimeSpan timeSpan = this.extractionPoint.vulnerableEndTime - StaticData.now;
			if (timeSpan.get_TotalSeconds() > 1)
			{
				GuiTimeTracker guiTimeTracker = new GuiTimeTracker((int)timeSpan.get_TotalSeconds(), this.extractionPointWindow)
				{
					FontSize = 12,
					TextColor = GuiNewStyleBar.orangeColor,
					boundries = new Rect(160f, 20f, 45f, 16f),
					Alignment = 5
				};
				guiTimeTracker.SetEndAction(new Action(this, ExtractionPointGuiRelativeToGameObject.Populate));
				this.extractionPointWindow.AddGuiElement(guiTimeTracker);
			}
		}
		else
		{
			this.epStateIcon.SetTexture("ActionButtons", "epImmuneIcon");
			TimeSpan timeSpan1 = this.extractionPoint.invulnerableModeEndTime - StaticData.now;
			if (timeSpan1.get_TotalSeconds() > 1)
			{
				GuiTimeTracker guiTimeTracker1 = new GuiTimeTracker((int)timeSpan1.get_TotalSeconds(), this.extractionPointWindow)
				{
					FontSize = 12,
					TextColor = GuiNewStyleBar.aquamarineColor,
					boundries = new Rect(160f, 20f, 45f, 16f),
					Alignment = 5
				};
				guiTimeTracker1.SetEndAction(new Action(this, ExtractionPointGuiRelativeToGameObject.Populate));
				this.extractionPointWindow.AddGuiElement(guiTimeTracker1);
			}
		}
	}

	private void ShowSpaceWindow()
	{
		if (this.spaceWindow != null)
		{
			return;
		}
		this.spaceWindow = new GuiWindow()
		{
			boundries = new Rect(0f, 0f, 260f, 60f),
			isHidden = false,
			isClickTransparent = true,
			zOrder = 50
		};
		AndromedaGui.gui.AddWindow(this.spaceWindow);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("MinimapWindow", string.Format("ep_fraction{0}Icon", this.extractionPoint.ownerFraction));
		guiTexture.X = 0f;
		guiTexture.Y = 0f;
		this.spaceWindow.AddGuiElement(guiTexture);
		string str = StaticData.Translate(this.extractionPoint.name);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(61f, 1f, 180f, 40f),
			text = str,
			Alignment = 3,
			FontSize = 16,
			Font = GuiLabel.FontBold,
			TextColor = Color.get_black()
		};
		this.spaceWindow.AddGuiElement(guiLabel);
		this.extractionPointName = new GuiLabel()
		{
			boundries = new Rect(60f, 0f, 180f, 40f),
			text = str,
			Alignment = 3,
			FontSize = 16,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor
		};
		this.spaceWindow.AddGuiElement(this.extractionPointName);
		this.healtBar = new GuiNewStyleBar()
		{
			X = 60f,
			Y = 30f
		};
		this.healtBar.SetCustumSizeBlueBar((int)this.extractionPointName.TextWidth);
		this.healtBar.maximum = (float)this.extractionPoint.hitPoints;
		this.healtBar.current = this.extractionPoint.health;
		this.spaceWindow.AddGuiElement(this.healtBar);
		this.spaceWindow.preDrawHandler = new Action<object>(this, ExtractionPointGuiRelativeToGameObject.UpdateHealtBar);
		this.spaceWindow.boundries.set_width(65f + this.extractionPointName.TextWidth);
	}

	private void Update()
	{
		this.screenPos = Camera.get_main().WorldToScreenPoint(base.get_transform().get_position());
		if (this.screenPos.x < 10f || this.screenPos.x > (float)(Screen.get_width() - 10) || this.screenPos.y < 10f || this.screenPos.y > (float)(Screen.get_height() - 10))
		{
			this.HideSpaceWindow();
		}
		else
		{
			this.ShowSpaceWindow();
			this.spaceWindow.boundries.set_x(this.screenPos.x + 20f);
			this.spaceWindow.boundries.set_y((float)Screen.get_height() - this.screenPos.y);
		}
		if (NetworkScript.player == null || !this.extractionPoint.IsObjectInRange(NetworkScript.player.vessel))
		{
			this.HideMainMenu();
			this.HideFullInfoWindow();
		}
		else
		{
			this.ShowMainMenu();
			this.extractionPointWindow.boundries.set_x((float)(Screen.get_width() / 2 - 195));
			this.extractionPointWindow.boundries.set_y((float)(Screen.get_height() - 160));
			if (!this.fullExtractionPointInfoOnScreen)
			{
				this.HideFullInfoWindow();
			}
			else
			{
				this.ShowFullExtarctionPointInfo();
				this.bigInfoWindow.boundries.set_x((float)(Screen.get_width() / 2 - 195));
				this.bigInfoWindow.boundries.set_y((float)(Screen.get_height() - 260));
			}
		}
	}

	private void UpdateHealtBar(object prm)
	{
		if (!this.extractionPoint.isVulnerable)
		{
			this.extractionPointName.TextColor = GuiNewStyleBar.blueColor;
		}
		else
		{
			this.extractionPointName.TextColor = GuiNewStyleBar.redColor;
		}
		this.healtBar.maximum = (float)this.extractionPoint.hitPoints;
		this.healtBar.current = this.extractionPoint.health;
	}
}