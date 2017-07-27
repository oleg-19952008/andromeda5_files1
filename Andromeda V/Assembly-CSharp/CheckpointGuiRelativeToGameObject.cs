using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CheckpointGuiRelativeToGameObject : MonoBehaviour
{
	private const float WINDOW_WIDTH = 260f;

	public CheckpointObjectPhysics checkpoint;

	private Vector3 screenPos;

	private bool isInteractDone;

	private bool isThisCheckpointInteresting;

	private bool isAlreadyDone;

	private GuiWindow spaceWindow;

	private GuiWindow checkpointWindow;

	private GuiWindow bigInfoWindow;

	private GuiButtonFixed infoButton;

	private GuiButtonFixed backButton;

	private GuiTextureAnimated guideArrow;

	private bool fullCheckpointInfoOnScreen;

	public CheckpointGuiRelativeToGameObject()
	{
	}

	private void HideFullCheckpointInfo()
	{
		if (this.bigInfoWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.bigInfoWindow.handler);
			this.bigInfoWindow = null;
			this.fullCheckpointInfoOnScreen = false;
		}
	}

	private void HideMainMenu()
	{
		if (this.checkpointWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.checkpointWindow.handler);
			this.checkpointWindow = null;
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

	private void InitGuide(float x, float y, GuiWindow window)
	{
		if (window == null)
		{
			return;
		}
		this.guideArrow = new GuiTextureAnimated();
		this.guideArrow.Init("Guide Arrow", "GuideArrow", "GuideArrow/arrow");
		this.guideArrow.rotationTime = 0.7f;
		this.guideArrow.boundries.set_x(x);
		this.guideArrow.boundries.set_y(y);
		window.AddGuiElement(this.guideArrow);
	}

	private void OnBackBtnClicked(object prm)
	{
		if (NetworkScript.player.shipScript != null)
		{
			NetworkScript.player.shipScript.isGuiClosed = true;
		}
		this.HideFullCheckpointInfo();
		this.HideMainMenu();
	}

	private void OnCheckpointBtnClicked(object prm)
	{
		// 
		// Current member / type: System.Void CheckpointGuiRelativeToGameObject::OnCheckpointBtnClicked(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnCheckpointBtnClicked(System.Object)
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
			this.fullCheckpointInfoOnScreen = true;
		}
		catch (Exception exception)
		{
			Debug.LogError(string.Format("OnTalkBtnClicked() {0}", exception));
		}
	}

	public void Populate()
	{
		this.isInteractDone = false;
		if (this.checkpointWindow != null)
		{
			this.HideMainMenu();
			this.ShowMainMenu();
		}
	}

	private void SetBtnActionToBack()
	{
		this.backButton.Clicked = new Action<EventHandlerParam>(this, CheckpointGuiRelativeToGameObject.OnBackBtnClicked);
		if (NetworkScript.player != null && NetworkScript.player.shipScript)
		{
			NetworkScript.player.shipScript.popUpAction = new Action<object>(this, CheckpointGuiRelativeToGameObject.OnBackBtnClicked);
		}
	}

	private void ShowFullCheckpointInfo()
	{
		if (this.bigInfoWindow != null)
		{
			return;
		}
		this.fullCheckpointInfoOnScreen = true;
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
			text = StaticData.Translate(this.checkpoint.checkpointName),
			Alignment = 4,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		this.bigInfoWindow.AddGuiElement(guiLabel);
		string empty = string.Empty;
		if (!this.isThisCheckpointInteresting)
		{
			empty = StaticData.Translate("key_checkpoint_state_no_interest");
		}
		else
		{
			empty = (!this.isAlreadyDone ? StaticData.Translate("key_checkpoint_state_have_interest") : StaticData.Translate("key_checkpoint_state_already_done"));
		}
		GuiAnimatedText guiAnimatedText = new GuiAnimatedText(empty, new Action(this, CheckpointGuiRelativeToGameObject.SetBtnActionToBack))
		{
			boundries = new Rect(7f, 17f, 290f, 70f),
			FontSize = 11,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor,
			Alignment = 1
		};
		this.bigInfoWindow.AddGuiElement(guiAnimatedText);
		this.backButton.Clicked = new Action<EventHandlerParam>(guiAnimatedText, GuiAnimatedText.ShowAll);
		if (NetworkScript.player != null && NetworkScript.player.shipScript)
		{
			NetworkScript.player.shipScript.popUpAction = new Action<object>(guiAnimatedText, GuiAnimatedText.ShowAll);
		}
	}

	private void ShowMainMenu()
	{
		CheckpointGuiRelativeToGameObject.<ShowMainMenu>c__AnonStorey19 variable = null;
		if (this.checkpointWindow != null)
		{
			return;
		}
		this.checkpointWindow = new GuiWindow()
		{
			boundries = new Rect(0f, 0f, 130f, 60f),
			isHidden = false,
			zOrder = 50
		};
		AndromedaGui.gui.AddWindow(this.checkpointWindow);
		this.isThisCheckpointInteresting = false;
		this.isAlreadyDone = false;
		IList<PlayerQuest> values = NetworkScript.player.playerBelongings.playerQuests.get_Values();
		if (CheckpointGuiRelativeToGameObject.<>f__am$cacheC == null)
		{
			CheckpointGuiRelativeToGameObject.<>f__am$cacheC = new Func<PlayerQuest, bool>(null, (PlayerQuest t) => !t.isRewordCollected);
		}
		IEnumerable<PlayerQuest> enumerable = Enumerable.Where<PlayerQuest>(values, CheckpointGuiRelativeToGameObject.<>f__am$cacheC);
		if (CheckpointGuiRelativeToGameObject.<>f__am$cacheD == null)
		{
			CheckpointGuiRelativeToGameObject.<>f__am$cacheD = new Func<PlayerQuest, int>(null, (PlayerQuest s) => s.currentQuestId);
		}
		foreach (int list in Enumerable.ToList<int>(Enumerable.Select<PlayerQuest, int>(enumerable, CheckpointGuiRelativeToGameObject.<>f__am$cacheD)))
		{
			foreach (NewQuestObjective objective in Enumerable.First<NewQuest>(Enumerable.Where<NewQuest>(Enumerable.Union<NewQuest>(StaticData.allQuests, StaticData.allDailyQuests), new Func<NewQuest, bool>(variable, (NewQuest t) => t.id == this.qId))).objectives)
			{
				if (objective.type != 32 && objective.type != 33 && objective.type != 34 || objective.targetParam1 != this.checkpoint.checkpointId)
				{
					continue;
				}
				this.isThisCheckpointInteresting = true;
				if (!objective.IsComplete(NetworkScript.player))
				{
					continue;
				}
				this.isAlreadyDone = true;
			}
		}
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("ActionButtons", "buttonsLink");
		guiTexture.X = 45f;
		guiTexture.Y = 4f;
		this.checkpointWindow.AddGuiElement(guiTexture);
		this.infoButton = new GuiButtonFixed();
		this.infoButton.SetTexture("ActionButtons", "btnInfo");
		this.infoButton.Caption = string.Empty;
		this.infoButton.X = 0f;
		this.infoButton.Y = 0f;
		if (!this.fullCheckpointInfoOnScreen)
		{
			this.infoButton.Clicked = new Action<EventHandlerParam>(this, CheckpointGuiRelativeToGameObject.OnInfoBtnClicked);
		}
		else
		{
			this.infoButton.Clicked = new Action<EventHandlerParam>(this, CheckpointGuiRelativeToGameObject.OnBackBtnClicked);
		}
		this.checkpointWindow.AddGuiElement(this.infoButton);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("ActionButtons", "btnUse");
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.X = 70f;
		guiButtonFixed.Y = 0f;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, CheckpointGuiRelativeToGameObject.OnCheckpointBtnClicked);
		this.checkpointWindow.AddGuiElement(guiButtonFixed);
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
			this.checkpointWindow.AddGuiElement(guiLabel);
		}
		if (this.isInteractDone || !this.isThisCheckpointInteresting || this.isAlreadyDone)
		{
			guiButtonFixed.isEnabled = false;
			if (NetworkScript.player != null && NetworkScript.player.shipScript)
			{
				NetworkScript.player.shipScript.popUpAction = new Action<object>(this, CheckpointGuiRelativeToGameObject.OnInfoBtnClicked);
			}
		}
		else if (NetworkScript.player != null && NetworkScript.player.shipScript)
		{
			NetworkScript.player.shipScript.popUpAction = new Action<object>(this, CheckpointGuiRelativeToGameObject.OnCheckpointBtnClicked);
			if (NetworkScript.player.vessel.galaxy.get_galaxyId() == 1000)
			{
				this.InitGuide(75f, 5f, this.checkpointWindow);
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
			zOrder = 50,
			isClickTransparent = true
		};
		AndromedaGui.gui.AddWindow(this.spaceWindow);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("MinimapWindow", "checkpointIcon");
		guiTexture.X = 0f;
		guiTexture.Y = 0f;
		this.spaceWindow.AddGuiElement(guiTexture);
		string str = StaticData.Translate(this.checkpoint.checkpointName);
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
		if (!this.checkpoint.IsObjectInRange(NetworkScript.player.vessel))
		{
			this.HideMainMenu();
			this.HideFullCheckpointInfo();
		}
		else
		{
			this.ShowMainMenu();
			this.checkpointWindow.boundries.set_x((float)(Screen.get_width() / 2 - 195));
			this.checkpointWindow.boundries.set_y((float)(Screen.get_height() - 160));
			if (!this.fullCheckpointInfoOnScreen)
			{
				this.HideFullCheckpointInfo();
			}
			else
			{
				this.ShowFullCheckpointInfo();
				this.bigInfoWindow.boundries.set_x((float)(Screen.get_width() / 2 - 195));
				this.bigInfoWindow.boundries.set_y((float)(Screen.get_height() - 260));
			}
		}
	}
}