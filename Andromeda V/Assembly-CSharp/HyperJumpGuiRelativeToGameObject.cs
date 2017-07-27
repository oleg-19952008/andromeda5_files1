using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HyperJumpGuiRelativeToGameObject : MonoBehaviour
{
	private const float WINDOW_WIDTH = 260f;

	public HyperJumpNet jump;

	private Vector3 screenPos;

	private LevelMap dstGalaxy;

	private GuiWindow jumpWindow;

	private GuiWindow bigInfoWindow;

	private GuiWindow spaceWindow;

	private GuiButtonFixed infoButton;

	private bool isHyperJumpInit;

	private bool hyperJumpInfoOnScreen;

	private GuiButtonFixed backButton;

	private HyperJumpGuiRelativeToGameObject.JumpState State
	{
		get
		{
			if (this.jump.fractionId != NetworkScript.player.vessel.fractionId)
			{
				return HyperJumpGuiRelativeToGameObject.JumpState.OtherFaction;
			}
			if (NetworkScript.player.playerBelongings.playerAccessLevel < this.dstGalaxy.accessLevel)
			{
				return HyperJumpGuiRelativeToGameObject.JumpState.LowAccessLevel;
			}
			if (!this.dstGalaxy.isPveMap && (NetworkScript.player.playerBelongings.playerLevel > this.dstGalaxy.reqMaxLevel || NetworkScript.player.playerBelongings.playerLevel < this.dstGalaxy.reqMinLevel))
			{
				return HyperJumpGuiRelativeToGameObject.JumpState.LevelRestriction;
			}
			return HyperJumpGuiRelativeToGameObject.JumpState.Allowed;
		}
	}

	public HyperJumpGuiRelativeToGameObject()
	{
	}

	private void HideFullJumpInfo()
	{
		if (this.bigInfoWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.bigInfoWindow.handler);
			this.bigInfoWindow = null;
			this.hyperJumpInfoOnScreen = false;
		}
	}

	private void HideMainMenu()
	{
		if (this.jumpWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.jumpWindow.handler);
			this.jumpWindow = null;
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

	private void HyperJump()
	{
		// 
		// Current member / type: System.Void HyperJumpGuiRelativeToGameObject::HyperJump()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void HyperJump()
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

	private void OnBackBtnClicked(object prm)
	{
		if (NetworkScript.player.shipScript != null)
		{
			NetworkScript.player.shipScript.isGuiClosed = true;
		}
		this.HideFullJumpInfo();
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
			this.hyperJumpInfoOnScreen = true;
		}
		catch (Exception exception)
		{
			Debug.LogError(string.Format("OnTalkBtnClicked() {0}", exception));
		}
	}

	private void OnJumpBtnClicked(object prm)
	{
		if (!NetworkScript.player.shipScript.isInControl || NetworkScript.player.vessel.pvpState == 2)
		{
			return;
		}
		try
		{
			this.HyperJump();
		}
		catch (Exception exception)
		{
			Debug.LogError(string.Format("OnJumpBtnClicked() {0}", exception));
		}
	}

	public void Populate()
	{
		this.HideFullJumpInfo();
		this.HideMainMenu();
		this.ShowMainMenu();
		this.HideSpaceWindow();
		this.ShowSpaceWindow();
	}

	private void SetBtnActionToBack()
	{
		this.backButton.Clicked = new Action<EventHandlerParam>(this, HyperJumpGuiRelativeToGameObject.OnBackBtnClicked);
		if (NetworkScript.player != null && NetworkScript.player.shipScript)
		{
			NetworkScript.player.shipScript.popUpAction = new Action<object>(this, HyperJumpGuiRelativeToGameObject.OnBackBtnClicked);
		}
	}

	private void ShowFullJumpInfo()
	{
		if (this.bigInfoWindow != null)
		{
			return;
		}
		this.hyperJumpInfoOnScreen = true;
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
			text = string.Concat(StaticData.Translate(this.dstGalaxy.nameUI), string.Format(" ({0} - {1})", this.dstGalaxy.reqMinLevel, this.dstGalaxy.reqMaxLevel)),
			Alignment = 4,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		this.bigInfoWindow.AddGuiElement(guiLabel);
		GuiAnimatedText guiAnimatedText = new GuiAnimatedText(StaticData.Translate(this.dstGalaxy.description), new Action(this, HyperJumpGuiRelativeToGameObject.SetBtnActionToBack))
		{
			boundries = new Rect(7f, 17f, 290f, 70f),
			FontSize = 11,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor,
			Alignment = 1
		};
		this.bigInfoWindow.AddGuiElement(guiAnimatedText);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("ActionButtons", "warningFrame");
		guiTexture.boundries = new Rect(7f, 69f, 290f, 26f);
		this.bigInfoWindow.AddGuiElement(guiTexture);
		GuiLabel empty = new GuiLabel()
		{
			boundries = new Rect(9f, 71f, 285f, 22f),
			text = string.Empty,
			Alignment = 4,
			FontSize = 12,
			TextColor = GuiNewStyleBar.redColor,
			Font = GuiLabel.FontBold
		};
		this.bigInfoWindow.AddGuiElement(empty);
		switch (this.State)
		{
			case HyperJumpGuiRelativeToGameObject.JumpState.Allowed:
			{
				guiTexture.SetTextureKeepSize("FrameworkGUI", "empty");
				empty.text = string.Empty;
				break;
			}
			case HyperJumpGuiRelativeToGameObject.JumpState.OtherFaction:
			{
				empty.text = StaticData.Translate("key_hyperjump_info_other_fraction");
				break;
			}
			case HyperJumpGuiRelativeToGameObject.JumpState.LevelRestriction:
			{
				empty.text = string.Format(StaticData.Translate("key_hyperjump_info_level_restriction"), this.dstGalaxy.reqMinLevel, this.dstGalaxy.reqMaxLevel);
				break;
			}
			case HyperJumpGuiRelativeToGameObject.JumpState.LowAccessLevel:
			{
				empty.text = StaticData.Translate("key_hyperjump_info_low_access_level");
				break;
			}
		}
		this.backButton.Clicked = new Action<EventHandlerParam>(guiAnimatedText, GuiAnimatedText.ShowAll);
		if (NetworkScript.player != null && NetworkScript.player.shipScript)
		{
			NetworkScript.player.shipScript.popUpAction = new Action<object>(guiAnimatedText, GuiAnimatedText.ShowAll);
		}
	}

	private void ShowMainMenu()
	{
		if (this.jumpWindow != null)
		{
			return;
		}
		this.jumpWindow = new GuiWindow()
		{
			boundries = new Rect(0f, 0f, 130f, 60f),
			isHidden = false,
			zOrder = 50
		};
		AndromedaGui.gui.AddWindow(this.jumpWindow);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("ActionButtons", "buttonsLink");
		guiTexture.X = 45f;
		guiTexture.Y = 4f;
		this.jumpWindow.AddGuiElement(guiTexture);
		this.infoButton = new GuiButtonFixed();
		this.infoButton.SetTexture("ActionButtons", "btnInfo");
		this.infoButton.Caption = string.Empty;
		this.infoButton.X = 0f;
		this.infoButton.Y = 0f;
		if (!this.hyperJumpInfoOnScreen)
		{
			this.infoButton.Clicked = new Action<EventHandlerParam>(this, HyperJumpGuiRelativeToGameObject.OnInfoBtnClicked);
		}
		else
		{
			this.infoButton.Clicked = new Action<EventHandlerParam>(this, HyperJumpGuiRelativeToGameObject.OnBackBtnClicked);
		}
		this.jumpWindow.AddGuiElement(this.infoButton);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("ActionButtons", "btnJump");
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.X = 70f;
		guiButtonFixed.Y = 0f;
		guiButtonFixed.Clicked = null;
		this.jumpWindow.AddGuiElement(guiButtonFixed);
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
			this.jumpWindow.AddGuiElement(guiLabel);
		}
		if (this.State != HyperJumpGuiRelativeToGameObject.JumpState.Allowed)
		{
			guiButtonFixed.isEnabled = false;
			if (NetworkScript.player != null && NetworkScript.player.shipScript)
			{
				NetworkScript.player.shipScript.popUpAction = new Action<object>(this, HyperJumpGuiRelativeToGameObject.OnInfoBtnClicked);
			}
		}
		else
		{
			guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, HyperJumpGuiRelativeToGameObject.OnJumpBtnClicked);
			if (NetworkScript.player != null && NetworkScript.player.shipScript)
			{
				NetworkScript.player.shipScript.popUpAction = new Action<object>(this, HyperJumpGuiRelativeToGameObject.OnJumpBtnClicked);
			}
		}
	}

	private void ShowSpaceWindow()
	{
		if (this.spaceWindow != null)
		{
			return;
		}
		if (this.dstGalaxy == null && this.jump != null)
		{
			this.dstGalaxy = Enumerable.FirstOrDefault<LevelMap>(Enumerable.Where<LevelMap>(StaticData.allGalaxies, new Func<LevelMap, bool>(this, (LevelMap t) => (t.get_galaxyId() != this.jump.galaxyDst ? false : t.fraction == this.jump.fractionId))));
			if (this.dstGalaxy == null)
			{
				this.dstGalaxy = Enumerable.FirstOrDefault<LevelMap>(Enumerable.Where<LevelMap>(StaticData.allGalaxies, new Func<LevelMap, bool>(this, (LevelMap t) => t.get_galaxyId() == this.jump.galaxyDst)));
			}
		}
		this.spaceWindow = new GuiWindow()
		{
			boundries = new Rect(0f, 0f, 260f, 60f),
			isHidden = false,
			zOrder = 50,
			isClickTransparent = true
		};
		AndromedaGui.gui.AddWindow(this.spaceWindow);
		GuiTexture guiTexture = new GuiTexture();
		if (this.jump.galaxyDst >= 1000)
		{
			guiTexture.SetTexture("MinimapWindow", "portal_instance");
		}
		else if (this.jump.fractionId != NetworkScript.player.vessel.fractionId)
		{
			guiTexture.SetTexture("MinimapWindow", "portal_denied");
		}
		else
		{
			guiTexture.SetTexture("MinimapWindow", "portal_myfraction");
		}
		guiTexture.X = 0f;
		guiTexture.Y = 0f;
		this.spaceWindow.AddGuiElement(guiTexture);
		string str = StaticData.Translate(this.dstGalaxy.nameUI);
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

	private void Start()
	{
		if (this.jump != null)
		{
			this.dstGalaxy = Enumerable.FirstOrDefault<LevelMap>(Enumerable.Where<LevelMap>(StaticData.allGalaxies, new Func<LevelMap, bool>(this, (LevelMap t) => (t.get_galaxyId() != this.jump.galaxyDst ? false : t.fraction == this.jump.fractionId))));
			if (this.dstGalaxy == null)
			{
				this.dstGalaxy = Enumerable.FirstOrDefault<LevelMap>(Enumerable.Where<LevelMap>(StaticData.allGalaxies, new Func<LevelMap, bool>(this, (LevelMap t) => t.get_galaxyId() == this.jump.galaxyDst)));
			}
		}
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
		if (!this.jump.IsObjectInRange(NetworkScript.player.vessel))
		{
			this.HideMainMenu();
			this.HideFullJumpInfo();
		}
		else
		{
			if (this.isHyperJumpInit)
			{
				this.HideMainMenu();
				this.HideFullJumpInfo();
				return;
			}
			this.ShowMainMenu();
			this.jumpWindow.boundries.set_x((float)(Screen.get_width() / 2 - 195));
			this.jumpWindow.boundries.set_y((float)(Screen.get_height() - 160));
			if (!this.hyperJumpInfoOnScreen)
			{
				this.HideFullJumpInfo();
			}
			else
			{
				this.ShowFullJumpInfo();
				this.bigInfoWindow.boundries.set_x((float)(Screen.get_width() / 2 - 195));
				this.bigInfoWindow.boundries.set_y((float)(Screen.get_height() - 260));
			}
		}
	}

	private enum JumpState
	{
		Allowed,
		OtherFaction,
		LevelRestriction,
		LowAccessLevel
	}
}