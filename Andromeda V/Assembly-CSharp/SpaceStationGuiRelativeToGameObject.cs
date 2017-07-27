using System;
using UnityEngine;

public class SpaceStationGuiRelativeToGameObject : MonoBehaviour
{
	private const float WINDOW_WIDTH = 260f;

	public StarBaseNet starBase;

	private Vector3 screenPos;

	private GuiWindow spaceWindow;

	private GuiWindow starBaseWindow;

	private GuiWindow bigInfoWindow;

	private GuiButtonFixed infoButton;

	private GuiButtonFixed backButton;

	private bool fullStarBaseInfoOnScreen;

	private bool isEnterBaseStarted;

	public SpaceStationGuiRelativeToGameObject()
	{
	}

	private void HideFullStarBaseInfo()
	{
		if (this.bigInfoWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.bigInfoWindow.handler);
			this.bigInfoWindow = null;
			this.fullStarBaseInfoOnScreen = false;
		}
	}

	private void HideMainMenu()
	{
		if (this.starBaseWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.starBaseWindow.handler);
			this.starBaseWindow = null;
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
		this.HideFullStarBaseInfo();
		this.HideMainMenu();
	}

	private void OnEnterBaseBtnClicked(object prm)
	{
		// 
		// Current member / type: System.Void SpaceStationGuiRelativeToGameObject::OnEnterBaseBtnClicked(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnEnterBaseBtnClicked(System.Object)
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..( , Int32 , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:строка 78
		//    в ..( , Int32& , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:строка 33
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 57
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 481
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 83
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
			this.fullStarBaseInfoOnScreen = true;
		}
		catch (Exception exception)
		{
			Debug.LogError(string.Format("OnTalkBtnClicked() {0}", exception));
		}
	}

	public void Populate()
	{
		if (this.starBaseWindow != null)
		{
			this.HideMainMenu();
			this.ShowMainMenu();
		}
	}

	private void SetBtnActionToBack()
	{
		this.backButton.Clicked = new Action<EventHandlerParam>(this, SpaceStationGuiRelativeToGameObject.OnBackBtnClicked);
		if (NetworkScript.player != null && NetworkScript.player.shipScript)
		{
			NetworkScript.player.shipScript.popUpAction = new Action<object>(this, SpaceStationGuiRelativeToGameObject.OnBackBtnClicked);
		}
	}

	private void ShowFullStarBaseInfo()
	{
		if (this.bigInfoWindow != null)
		{
			return;
		}
		this.fullStarBaseInfoOnScreen = true;
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
			text = StaticData.Translate(this.starBase.starBaseName),
			Alignment = 4,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		this.bigInfoWindow.AddGuiElement(guiLabel);
		GuiAnimatedText guiAnimatedText = new GuiAnimatedText(StaticData.Translate("key_starbase_info_description"), new Action(this, SpaceStationGuiRelativeToGameObject.SetBtnActionToBack))
		{
			boundries = new Rect(7f, 17f, 290f, 70f),
			FontSize = 11,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor,
			Alignment = 1
		};
		this.bigInfoWindow.AddGuiElement(guiAnimatedText);
		if (NetworkScript.player.vessel.fractionId != this.starBase.fractionId)
		{
			GuiTexture guiTexture = new GuiTexture();
			guiTexture.SetTexture("ActionButtons", "warningFrame");
			guiTexture.boundries = new Rect(7f, 69f, 290f, 26f);
			this.bigInfoWindow.AddGuiElement(guiTexture);
			GuiLabel guiLabel1 = new GuiLabel()
			{
				boundries = new Rect(9f, 71f, 285f, 22f),
				text = StaticData.Translate("key_starbase_info_other_fraction"),
				Alignment = 4,
				FontSize = 12,
				TextColor = GuiNewStyleBar.redColor,
				Font = GuiLabel.FontBold
			};
			this.bigInfoWindow.AddGuiElement(guiLabel1);
		}
		this.backButton.Clicked = new Action<EventHandlerParam>(guiAnimatedText, GuiAnimatedText.ShowAll);
		if (NetworkScript.player != null && NetworkScript.player.shipScript)
		{
			NetworkScript.player.shipScript.popUpAction = new Action<object>(guiAnimatedText, GuiAnimatedText.ShowAll);
		}
	}

	private void ShowMainMenu()
	{
		if (this.starBaseWindow != null)
		{
			return;
		}
		this.starBaseWindow = new GuiWindow()
		{
			boundries = new Rect(0f, 0f, 130f, 60f),
			isHidden = false,
			zOrder = 50
		};
		AndromedaGui.gui.AddWindow(this.starBaseWindow);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("ActionButtons", "buttonsLink");
		guiTexture.X = 45f;
		guiTexture.Y = 4f;
		this.starBaseWindow.AddGuiElement(guiTexture);
		this.infoButton = new GuiButtonFixed();
		this.infoButton.SetTexture("ActionButtons", "btnInfo");
		this.infoButton.Caption = string.Empty;
		this.infoButton.X = 0f;
		this.infoButton.Y = 0f;
		if (!this.fullStarBaseInfoOnScreen)
		{
			this.infoButton.Clicked = new Action<EventHandlerParam>(this, SpaceStationGuiRelativeToGameObject.OnInfoBtnClicked);
		}
		else
		{
			this.infoButton.Clicked = new Action<EventHandlerParam>(this, SpaceStationGuiRelativeToGameObject.OnBackBtnClicked);
		}
		this.starBaseWindow.AddGuiElement(this.infoButton);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("ActionButtons", "btnEnterBase");
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.X = 70f;
		guiButtonFixed.Y = 0f;
		guiButtonFixed.Clicked = null;
		this.starBaseWindow.AddGuiElement(guiButtonFixed);
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
			this.starBaseWindow.AddGuiElement(guiLabel);
		}
		if (NetworkScript.player.vessel.fractionId != this.starBase.fractionId)
		{
			guiButtonFixed.isEnabled = false;
			if (NetworkScript.player != null && NetworkScript.player.shipScript)
			{
				NetworkScript.player.shipScript.popUpAction = new Action<object>(this, SpaceStationGuiRelativeToGameObject.OnInfoBtnClicked);
			}
		}
		else
		{
			guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, SpaceStationGuiRelativeToGameObject.OnEnterBaseBtnClicked);
			if (NetworkScript.player != null && NetworkScript.player.shipScript)
			{
				NetworkScript.player.shipScript.popUpAction = new Action<object>(this, SpaceStationGuiRelativeToGameObject.OnEnterBaseBtnClicked);
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
			zOrder = 40
		};
		AndromedaGui.gui.AddWindow(this.spaceWindow);
		GuiTexture guiTexture = new GuiTexture();
		if (this.starBase.fractionId == NetworkScript.player.vessel.fractionId)
		{
			guiTexture.SetTexture("MinimapWindow", "starbase_icon");
		}
		else
		{
			guiTexture.SetTexture("MinimapWindow", "starbase_icon_denied");
		}
		guiTexture.X = 0f;
		guiTexture.Y = 0f;
		this.spaceWindow.AddGuiElement(guiTexture);
		string str = StaticData.Translate(this.starBase.starBaseName);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(61f, 1f, 180f, 60f),
			text = str,
			Alignment = 3,
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = Color.get_black()
		};
		this.spaceWindow.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(60f, 0f, 180f, 60f),
			text = str,
			Alignment = 3,
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor
		};
		this.spaceWindow.AddGuiElement(guiLabel1);
		this.spaceWindow.boundries.set_width(65f + guiLabel1.TextWidth);
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
		if (!this.starBase.IsObjectInRange(NetworkScript.player.vessel))
		{
			this.HideMainMenu();
			this.HideFullStarBaseInfo();
		}
		else
		{
			if (this.isEnterBaseStarted)
			{
				this.HideMainMenu();
				this.HideFullStarBaseInfo();
				return;
			}
			this.ShowMainMenu();
			this.starBaseWindow.boundries.set_x((float)(Screen.get_width() / 2 - 195));
			this.starBaseWindow.boundries.set_y((float)(Screen.get_height() - 160));
			if (!this.fullStarBaseInfoOnScreen)
			{
				this.HideFullStarBaseInfo();
			}
			else
			{
				this.ShowFullStarBaseInfo();
				this.bigInfoWindow.boundries.set_x((float)(Screen.get_width() / 2 - 195));
				this.bigInfoWindow.boundries.set_y((float)(Screen.get_height() - 260));
			}
		}
	}
}