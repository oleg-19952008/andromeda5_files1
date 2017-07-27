using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CheckpointDialogWindow
{
	public static GuiWindow wnd;

	private static GuiButtonFixed btnAct;

	private static CheckpointObjectPhysics checkpoint;

	private static GuiTexture avatar;

	private static GuiTexture avatarFrame;

	private static GuiLabel lblName;

	private static GuiLabel lblDescrption;

	private static bool isShow;

	static CheckpointDialogWindow()
	{
	}

	public CheckpointDialogWindow()
	{
	}

	public static void Create()
	{
		CheckpointDialogWindow.<Create>c__AnonStorey18 variable = null;
		if (CheckpointDialogWindow.checkpoint == null)
		{
			return;
		}
		bool flag = false;
		bool flag1 = false;
		IList<PlayerQuest> values = NetworkScript.player.playerBelongings.playerQuests.get_Values();
		if (CheckpointDialogWindow.<>f__am$cache8 == null)
		{
			CheckpointDialogWindow.<>f__am$cache8 = new Func<PlayerQuest, bool>(null, (PlayerQuest t) => !t.isRewordCollected);
		}
		IEnumerable<PlayerQuest> enumerable = Enumerable.Where<PlayerQuest>(values, CheckpointDialogWindow.<>f__am$cache8);
		if (CheckpointDialogWindow.<>f__am$cache9 == null)
		{
			CheckpointDialogWindow.<>f__am$cache9 = new Func<PlayerQuest, int>(null, (PlayerQuest s) => s.currentQuestId);
		}
		foreach (int list in Enumerable.ToList<int>(Enumerable.Select<PlayerQuest, int>(enumerable, CheckpointDialogWindow.<>f__am$cache9)))
		{
			foreach (NewQuestObjective objective in Enumerable.First<NewQuest>(Enumerable.Where<NewQuest>(Enumerable.Union<NewQuest>(StaticData.allQuests, StaticData.allDailyQuests), new Func<NewQuest, bool>(variable, (NewQuest t) => t.id == this.qId))).objectives)
			{
				if (objective.type != 32 && objective.type != 33 && objective.type != 34 || objective.targetParam1 != CheckpointDialogWindow.checkpoint.checkpointId)
				{
					continue;
				}
				flag = true;
				if (!objective.IsComplete(NetworkScript.player))
				{
					continue;
				}
				flag1 = true;
			}
		}
		CheckpointDialogWindow.btnAct = new GuiButtonFixed();
		CheckpointDialogWindow.btnAct.SetTexture("NewGUI", "targeting_action");
		CheckpointDialogWindow.btnAct.Y = 34f;
		CheckpointDialogWindow.btnAct.X = 297f;
		CheckpointDialogWindow.btnAct.eventHandlerParam.customData = CheckpointDialogWindow.checkpoint.checkpointType;
		CheckpointDialogWindow.btnAct.Clicked = new Action<EventHandlerParam>(null, CheckpointDialogWindow.OnActionBtnClicked);
		switch (CheckpointDialogWindow.checkpoint.checkpointType)
		{
			case 1:
			{
				CheckpointDialogWindow.btnAct.Caption = StaticData.Translate("key_checkpoint_dialog_activate");
				break;
			}
			case 2:
			{
				CheckpointDialogWindow.btnAct.Caption = StaticData.Translate("key_checkpoint_dialog_investigate");
				break;
			}
			case 3:
			{
				CheckpointDialogWindow.btnAct.Caption = StaticData.Translate("key_checkpoint_dialog_sabotage");
				break;
			}
		}
		CheckpointDialogWindow.btnAct.FontSize = 12;
		CheckpointDialogWindow.btnAct.isMuted = true;
		CheckpointDialogWindow.btnAct.textColorHover = Color.get_white();
		CheckpointDialogWindow.btnAct.textColorNormal = GuiNewStyleBar.blueColor;
		CheckpointDialogWindow.btnAct.textColorDisabled = GuiNewStyleBar.blueColorDisable;
		CheckpointDialogWindow.btnAct.Alignment = 4;
		CheckpointDialogWindow.btnAct.isEnabled = (!flag ? false : !flag1);
		CheckpointDialogWindow.wnd.AddGuiElement(CheckpointDialogWindow.btnAct);
		CheckpointDialogWindow.avatar = new GuiTexture();
		CheckpointDialogWindow.avatar.SetTexture("Targeting", CheckpointDialogWindow.checkpoint.assetName);
		CheckpointDialogWindow.avatar.boundries = new Rect(9f, 7f, 60f, 60f);
		CheckpointDialogWindow.wnd.AddGuiElement(CheckpointDialogWindow.avatar);
		CheckpointDialogWindow.avatarFrame = new GuiTexture();
		CheckpointDialogWindow.avatarFrame.SetTexture("NewGUI", "targeting_frame_avatar");
		CheckpointDialogWindow.avatarFrame.boundries = new Rect(9f, 7f, 60f, 60f);
		CheckpointDialogWindow.wnd.AddGuiElement(CheckpointDialogWindow.avatarFrame);
		CheckpointDialogWindow.lblDescrption = new GuiLabel()
		{
			boundries = new Rect(78f, 28f, 215f, 40f),
			text = StaticData.Translate(CheckpointDialogWindow.checkpoint.checkpointDescription),
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12
		};
		CheckpointDialogWindow.wnd.AddGuiElement(CheckpointDialogWindow.lblDescrption);
		if (!flag)
		{
			CheckpointDialogWindow.lblDescrption.text = StaticData.Translate("key_checkpoint_state_no_interest");
		}
		else if (!flag1)
		{
			CheckpointDialogWindow.lblDescrption.text = StaticData.Translate("key_checkpoint_state_have_interest");
		}
		else
		{
			CheckpointDialogWindow.lblDescrption.text = StaticData.Translate("key_checkpoint_state_already_done");
			GuiTexture guiTexture = new GuiTexture();
			guiTexture.SetTexture("QuestTracker", "isComplete");
			guiTexture.X = 355f;
			guiTexture.Y = 40f;
			CheckpointDialogWindow.wnd.AddGuiElement(guiTexture);
			CheckpointDialogWindow.btnAct.Caption = string.Empty;
		}
		CheckpointDialogWindow.lblName = new GuiLabel()
		{
			boundries = new Rect(78f, 6f, 250f, 17f),
			text = StaticData.Translate(CheckpointDialogWindow.checkpoint.checkpointName),
			FontSize = 13,
			Font = GuiLabel.FontBold,
			Alignment = 3
		};
		CheckpointDialogWindow.wnd.AddGuiElement(CheckpointDialogWindow.lblName);
	}

	public static void Hide(GuiFramework gui)
	{
		if (CheckpointDialogWindow.wnd != null && CheckpointDialogWindow.isShow)
		{
			CheckpointDialogWindow.wnd.isHidden = true;
			gui.RemoveWindow(CheckpointDialogWindow.wnd.handler);
			CheckpointDialogWindow.wnd = null;
			CheckpointDialogWindow.isShow = false;
		}
	}

	public static void OnActionBtnClicked(object prm)
	{
		// 
		// Current member / type: System.Void CheckpointDialogWindow::OnActionBtnClicked(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnActionBtnClicked(System.Object)
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

	public static void ReorderGui()
	{
		if (CheckpointDialogWindow.wnd != null)
		{
			CheckpointDialogWindow.wnd.boundries = new Rect((float)((Screen.get_width() - 451) / 2 + 1), 36f, 451f, 76f);
		}
	}

	public static void Show(GuiFramework gui, CheckpointObjectPhysics checkpointInRange, GameObjectPhysics selectedObject)
	{
		if (CheckpointDialogWindow.checkpoint != null && CheckpointDialogWindow.checkpoint.checkpointId != checkpointInRange.checkpointId)
		{
			CheckpointDialogWindow.Hide(gui);
		}
		if (CheckpointDialogWindow.wnd == null)
		{
			CheckpointDialogWindow.wnd = new GuiWindow();
			CheckpointDialogWindow.wnd.SetBackgroundTexture("NewGUI", "targeting_frame_popup");
			CheckpointDialogWindow.wnd.boundries = (selectedObject == null ? new Rect((float)((Screen.get_width() - 451) / 2 + 1), 89f, 451f, 76f) : new Rect((float)((Screen.get_width() - 451) / 2 + 1), 125f, 451f, 76f));
			CheckpointDialogWindow.wnd.isHidden = false;
			CheckpointDialogWindow.wnd.zOrder = 195;
			gui.AddWindow(CheckpointDialogWindow.wnd);
		}
		if (selectedObject == null)
		{
			CheckpointDialogWindow.wnd.boundries = new Rect((float)((Screen.get_width() - 451) / 2 + 1), 36f, 451f, 76f);
		}
		else
		{
			CheckpointDialogWindow.wnd.boundries = new Rect((float)((Screen.get_width() - 451) / 2 + 1), 125f, 451f, 76f);
		}
		if (!CheckpointDialogWindow.isShow)
		{
			CheckpointDialogWindow.checkpoint = checkpointInRange;
			CheckpointDialogWindow.Create();
			CheckpointDialogWindow.isShow = true;
		}
		CheckpointDialogWindow.wnd.isHidden = false;
	}
}