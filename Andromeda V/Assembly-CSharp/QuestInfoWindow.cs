using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class QuestInfoWindow
{
	private static NewQuest selectedQuest;

	private static GuiWindow infoWindow;

	private static bool isObjectivesOnscreen;

	private static Action OnCloseInfoWindow;

	private static bool checkBoxState;

	private static GuiWindow dialogWindow;

	private static Action CloseAciveWindow;

	private static Action MuteAction;

	private static Action UnmuteAction;

	public static bool IsOnScreen
	{
		get
		{
			return QuestInfoWindow.infoWindow != null;
		}
	}

	public static Action OnCLose
	{
		set
		{
			QuestInfoWindow.OnCloseInfoWindow = value;
		}
	}

	public static int SelectedQuestId
	{
		get
		{
			return (QuestInfoWindow.selectedQuest == null ? -1 : QuestInfoWindow.selectedQuest.id);
		}
	}

	static QuestInfoWindow()
	{
	}

	public static bool Close()
	{
		if (QuestInfoWindow.infoWindow == null)
		{
			return false;
		}
		if (QuestInfoWindow.dialogWindow != null)
		{
			QuestInfoWindow.DialogClose();
			return true;
		}
		AndromedaGui.gui.RemoveWindow(QuestInfoWindow.infoWindow.handler);
		QuestInfoWindow.infoWindow = null;
		QuestInfoWindow.selectedQuest = null;
		if (QuestInfoWindow.OnCloseInfoWindow != null)
		{
			QuestInfoWindow.OnCloseInfoWindow.Invoke();
			QuestInfoWindow.OnCloseInfoWindow = null;
		}
		return true;
	}

	public static void Create(NewQuest quest, bool showObjectives, bool withHammerEffect)
	{
		if (quest == null)
		{
			return;
		}
		if (QuestInfoWindow.infoWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(QuestInfoWindow.infoWindow.handler);
			QuestInfoWindow.infoWindow = null;
		}
		if (QuestInfoWindow.CloseAciveWindow != null)
		{
			QuestInfoWindow.CloseAciveWindow.Invoke();
			QuestInfoWindow.ResetActiveWindow();
		}
		QuestInfoWindow.selectedQuest = quest;
		QuestInfoWindow.isObjectivesOnscreen = showObjectives;
		QuestInfoWindow.infoWindow = new GuiWindow();
		QuestInfoWindow.infoWindow.SetBackgroundTexture("QuestInfoWindow", "windowFrame");
		QuestInfoWindow.infoWindow.PutToCenter();
		QuestInfoWindow.infoWindow.zOrder = 210;
		QuestInfoWindow.infoWindow.isHidden = false;
		List<StoryActor> list = StoryActor.allActor;
		if (QuestInfoWindow.<>f__am$cache9 == null)
		{
			QuestInfoWindow.<>f__am$cache9 = new Func<StoryActor, bool>(null, (StoryActor t) => t.id == QuestInfoWindow.selectedQuest.actorId);
		}
		StoryActor storyActor = Enumerable.FirstOrDefault<StoryActor>(Enumerable.Where<StoryActor>(list, QuestInfoWindow.<>f__am$cache9));
		if (storyActor != null)
		{
			GuiTexture guiTexture = new GuiTexture();
			if (NetworkScript.player.vessel.galaxy.__galaxyId != 1000)
			{
				guiTexture.SetTexture("QuestTrackerAvatars", storyActor.assetName);
			}
			else
			{
				guiTexture.SetTexture("TutorialWindow", "Aria");
			}
			guiTexture.boundries = new Rect(0f, 0f, 234f, 234f);
			QuestInfoWindow.infoWindow.AddGuiElement(guiTexture);
		}
		if (QuestInfoWindow.selectedQuest.type == 2)
		{
			GuiTexture rect = new GuiTexture();
			if (NetworkScript.player.vessel.fractionId == 1)
			{
				rect.SetTexture("QuestTrackerAvatars", "vindexis");
			}
			else if (NetworkScript.player.vessel.fractionId != 2)
			{
				rect.SetTexture("FrameworkGUI", "empty");
			}
			else
			{
				rect.SetTexture("QuestTrackerAvatars", "regia");
			}
			rect.boundries = new Rect(0f, 0f, 234f, 234f);
			QuestInfoWindow.infoWindow.AddGuiElement(rect);
		}
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(226f, 52f, 490f, 22f),
			text = string.Empty,
			Font = GuiLabel.FontBold,
			FontSize = 20,
			Clipping = 1,
			TextColor = GuiNewStyleBar.aquamarineColor
		};
		QuestInfoWindow.infoWindow.AddGuiElement(guiLabel);
		string str = (quest.type != 2 ? string.Format("key_quest_{0}_name", quest.id) : "key_quest_daily_mission_name");
		string str1 = (quest.type != 2 ? StaticData.Translate(string.Format("key_quest_{0}_description", quest.id)) : string.Format(StaticData.Translate("key_quest_daily_mission_description_new"), 25));
		guiLabel.text = string.Concat(string.Format("({0}) ", QuestInfoWindow.selectedQuest.level), StaticData.Translate(str));
		if (quest.type == 2)
		{
			guiLabel.text = string.Format("{0} {1}/{2}", guiLabel.text, NetworkScript.player.playerBelongings.dailyMissionsDone + 1, 6);
			for (int i = 0; i < 6; i++)
			{
				GuiTexture guiTexture1 = new GuiTexture();
				guiTexture1.SetTexture("QuestInfoWindow", "barFrame");
				guiTexture1.boundries = new Rect((float)(228 + i * 42), 115f, 41f, 18f);
				QuestInfoWindow.infoWindow.AddGuiElement(guiTexture1);
				if (i < NetworkScript.player.playerBelongings.dailyMissionsDone)
				{
					GuiTexture rect1 = new GuiTexture();
					rect1.SetTexture("QuestInfoWindow", "barFill");
					rect1.boundries = new Rect((float)(228 + i * 42), 115f, 41f, 18f);
					QuestInfoWindow.infoWindow.AddGuiElement(rect1);
				}
			}
		}
		GuiAnimatedText guiAnimatedText = new GuiAnimatedText(str1, null)
		{
			boundries = new Rect(226f, 78f, 550f, 51f),
			FontSize = 14,
			Clipping = 1
		};
		QuestInfoWindow.infoWindow.AddGuiElement(guiAnimatedText);
		GuiButton guiButton = new GuiButton()
		{
			boundries = new Rect(226f, 78f, 550f, 51f),
			Caption = string.Empty,
			Clicked = new Action<EventHandlerParam>(guiAnimatedText, GuiAnimatedText.ShowAll)
		};
		QuestInfoWindow.infoWindow.AddGuiElement(guiButton);
		if (QuestInfoWindow.selectedQuest.CanCollectReward(NetworkScript.player))
		{
			GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
			guiButtonFixed.SetTexture("QuestInfoWindow", "buttonGreen");
			guiButtonFixed.X = 522f;
			guiButtonFixed.Y = 151f;
			guiButtonFixed.Caption = StaticData.Translate("key_quest_info_window_collect");
			guiButtonFixed.Alignment = 4;
			guiButtonFixed.Clicked = new Action<EventHandlerParam>(null, QuestInfoWindow.OnCollectRewardClicked);
			guiButtonFixed.textColorNormal = GuiNewStyleBar.blackColorTransperant;
			guiButtonFixed.textColorHover = GuiNewStyleBar.blackColorTransperant;
			guiButtonFixed.textColorDisabled = GuiNewStyleBar.blackColorTransperant;
			QuestInfoWindow.infoWindow.AddGuiElement(guiButtonFixed);
		}
		else if (QuestInfoWindow.selectedQuest.type == 2 || QuestInfoWindow.selectedQuest.type == 4)
		{
			GuiButtonFixed action = new GuiButtonFixed();
			action.SetTexture("QuestInfoWindow", "buttonGreen");
			action.X = 522f;
			action.Y = 151f;
			action.Caption = StaticData.Translate("key_quest_info_window_collect");
			action.Alignment = 4;
			action.Clicked = new Action<EventHandlerParam>(null, QuestInfoWindow.OnCollectRewardClicked);
			action.textColorNormal = GuiNewStyleBar.blackColorTransperant;
			action.textColorHover = GuiNewStyleBar.blackColorTransperant;
			action.textColorDisabled = GuiNewStyleBar.blackColorTransperant;
			action.isEnabled = false;
			QuestInfoWindow.infoWindow.AddGuiElement(action);
		}
		else
		{
			GuiButtonFixed guiButtonFixed1 = new GuiButtonFixed()
			{
				X = 522f,
				Y = 151f,
				Caption = StaticData.Translate("key_quest_info_window_skip"),
				Alignment = 4,
				Clicked = new Action<EventHandlerParam>(null, QuestInfoWindow.OnSkipQuestClicked),
				textColorNormal = GuiNewStyleBar.blackColorTransperant,
				textColorHover = GuiNewStyleBar.blackColorTransperant,
				textColorDisabled = GuiNewStyleBar.blackColorTransperant
			};
			QuestInfoWindow.infoWindow.AddGuiElement(guiButtonFixed1);
			if (quest.GetSkipPrice(NetworkScript.player.playerBelongings.playerLevel) != 0)
			{
				guiButtonFixed1.SetTexture("QuestInfoWindow", "buttonOrange");
			}
			else
			{
				guiButtonFixed1.SetTexture("QuestInfoWindow", "buttonGreen");
			}
		}
		GuiCheckbox guiCheckbox = new GuiCheckbox()
		{
			X = 185f,
			Y = 524f,
			OnCheckboxSelected = new Action<bool>(null, QuestInfoWindow.ToogleObjectivesInfo),
			Selected = QuestInfoWindow.isObjectivesOnscreen,
			isActive = true
		};
		QuestInfoWindow.infoWindow.AddGuiElement(guiCheckbox);
		QuestInfoWindow.checkBoxState = QuestInfoWindow.isObjectivesOnscreen;
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(210f, 525f, 410f, 22f),
			text = StaticData.Translate("key_quest_info_window_show_objective"),
			Font = GuiLabel.FontBold,
			FontSize = 16
		};
		guiLabel1.boundries.set_width(guiLabel1.TextWidth);
		GuiButton x = new GuiButton()
		{
			boundries = new Rect(210f, 525f, 410f, 22f),
			Caption = StaticData.Translate("key_quest_info_window_show_objective")
		};
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = guiCheckbox
		};
		x.eventHandlerParam = eventHandlerParam;
		x.Clicked = new Action<EventHandlerParam>(null, QuestInfoWindow.OnToogleObjectiveInfo);
		x.FontSize = 16;
		x.boundries.set_width(guiLabel1.TextWidth);
		QuestInfoWindow.infoWindow.AddGuiElement(x);
		float _width = 450f - x.boundries.get_width() - 23f;
		guiCheckbox.X = 184f + _width / 2f;
		x.X = guiCheckbox.X + 30f;
		int num = 0;
		foreach (NewQuestObjective objective in quest.objectives)
		{
			QuestInfoWindow.ObjectiveInfoItem objectiveInfoItem = new QuestInfoWindow.ObjectiveInfoItem();
			objective.questId = quest.id;
			objectiveInfoItem.Create(objective, num, QuestInfoWindow.infoWindow, quest.type == 2);
			num++;
		}
		GuiButtonFixed empty = new GuiButtonFixed();
		empty.SetTexture("QuestInfoWindow", "buttonClose");
		empty.X = 723f;
		empty.Y = 0f;
		empty.Caption = string.Empty;
		empty.Clicked = new Action<EventHandlerParam>(null, QuestInfoWindow.OnCloseButtonClicked);
		QuestInfoWindow.infoWindow.AddGuiElement(empty);
		GuiTexture guiTexture2 = new GuiTexture();
		guiTexture2.SetTexture("FrameworkGUI", "cashIcon");
		guiTexture2.boundries = new Rect(183f, 170f, 32f, 32f);
		QuestInfoWindow.infoWindow.AddGuiElement(guiTexture2);
		int num1 = 0;
		int num2 = 0;
		QuestInfoWindow.selectedQuest.GetRewardsValue(NetworkScript.player, ref num1, ref num2);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(213f, 178f, 100f, 20f),
			text = num2.ToString("##,##0"),
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor,
			Font = GuiLabel.FontBold,
			FontSize = 14
		};
		QuestInfoWindow.infoWindow.AddGuiElement(guiLabel2);
		GuiTexture rect2 = new GuiTexture();
		rect2.SetTexture("QuestInfoWindow", "symbol_expirance");
		rect2.boundries = new Rect(306f, 172f, 32f, 28f);
		QuestInfoWindow.infoWindow.AddGuiElement(rect2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(336f, 178f, 100f, 20f),
			text = num1.ToString("##,##0"),
			Alignment = 3,
			TextColor = GuiNewStyleBar.orangeColor,
			Font = GuiLabel.FontBold,
			FontSize = 14
		};
		QuestInfoWindow.infoWindow.AddGuiElement(guiLabel3);
		if (QuestInfoWindow.selectedQuest.rewards != null && QuestInfoWindow.selectedQuest.rewards.get_Count() > 0)
		{
			NewQuestReward item = QuestInfoWindow.selectedQuest.rewards.get_Item(0);
			GuiTexture drawTooltipWindow = new GuiTexture();
			drawTooltipWindow.SetItemTexture(item.itemTypeId);
			drawTooltipWindow.boundries = new Rect(435f, 171f, 46f, 32f);
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate(StaticData.allTypes.get_Item(item.itemTypeId).uiName),
				customData2 = drawTooltipWindow
			};
			drawTooltipWindow.tooltipWindowParam = eventHandlerParam;
			drawTooltipWindow.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			QuestInfoWindow.infoWindow.AddGuiElement(drawTooltipWindow);
			GuiLabel guiLabel4 = new GuiLabel()
			{
				boundries = new Rect(481f, 178f, 50f, 20f),
				text = item.amount.ToString("##,##0"),
				Alignment = 3,
				Font = GuiLabel.FontBold,
				FontSize = 14
			};
			QuestInfoWindow.infoWindow.AddGuiElement(guiLabel4);
			if (item.itemTypeId == PlayerItems.TypeUltralibrium)
			{
				guiLabel4.TextColor = GuiNewStyleBar.greenColor;
			}
		}
		AndromedaGui.gui.AddWindow(QuestInfoWindow.infoWindow);
		if (withHammerEffect)
		{
			float _x = QuestInfoWindow.infoWindow.boundries.get_x();
			float _y = QuestInfoWindow.infoWindow.boundries.get_y();
			QuestInfoWindow.infoWindow.amplitudeHammerShake = 20f;
			QuestInfoWindow.infoWindow.timeHammerFx = 0.5f;
			QuestInfoWindow.infoWindow.moveToShakeRatio = 0.6f;
			QuestInfoWindow.infoWindow.boundries.set_y(-QuestInfoWindow.infoWindow.boundries.get_height());
			QuestInfoWindow.infoWindow.StartHammerEffect(_x, _y);
			QuestTrackerWindow.SetNewActiveQuest(QuestInfoWindow.selectedQuest.id);
		}
	}

	private static void CreateSkipQuestDialog()
	{
		int skipPrice = QuestInfoWindow.selectedQuest.GetSkipPrice(NetworkScript.player.playerBelongings.playerLevel);
		QuestInfoWindow.dialogWindow = new GuiWindow()
		{
			isModal = true
		};
		QuestInfoWindow.dialogWindow.SetBackgroundTexture("FrameworkGUI", "menugui_dialog");
		QuestInfoWindow.dialogWindow.isHidden = false;
		QuestInfoWindow.dialogWindow.zOrder = 220;
		QuestInfoWindow.dialogWindow.PutToCenter();
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(50f, 50f, 420f, 100f),
			text = string.Format(StaticData.Translate("key_quest_info_window_skip_dialogue"), skipPrice),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 18
		};
		QuestInfoWindow.dialogWindow.AddGuiElement(guiLabel);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.SetOrangeTexture();
		guiButtonResizeable.X = 70f;
		guiButtonResizeable.Y = 170f;
		guiButtonResizeable.Width = 160f;
		guiButtonResizeable.Caption = StaticData.Translate("key_quest_info_window_skip_dialogue_yes").ToUpper();
		guiButtonResizeable.Alignment = 4;
		guiButtonResizeable.isEnabled = NetworkScript.player.playerBelongings.playerItems.get_Nova() >= (long)skipPrice;
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(null, QuestInfoWindow.DialogYesBtnClicked);
		QuestInfoWindow.dialogWindow.AddGuiElement(guiButtonResizeable);
		GuiButtonResizeable upper = new GuiButtonResizeable();
		upper.SetBlueTexture();
		upper.X = 290f;
		upper.Y = 170f;
		upper.Width = 160f;
		upper.Caption = StaticData.Translate("key_quest_info_window_skip_dialogue_no").ToUpper();
		upper.Alignment = 4;
		upper.isEnabled = true;
		upper.Clicked = new Action<EventHandlerParam>(null, QuestInfoWindow.DialogNoBtnClicked);
		QuestInfoWindow.dialogWindow.AddGuiElement(upper);
		AndromedaGui.gui.AddWindow(QuestInfoWindow.dialogWindow);
		AndromedaGui.gui.activeToolTipId = QuestInfoWindow.dialogWindow.handler;
		float _x = QuestInfoWindow.dialogWindow.boundries.get_x();
		float _y = QuestInfoWindow.dialogWindow.boundries.get_y();
		QuestInfoWindow.dialogWindow.amplitudeHammerShake = 20f;
		QuestInfoWindow.dialogWindow.timeHammerFx = 0.5f;
		QuestInfoWindow.dialogWindow.moveToShakeRatio = 0.6f;
		QuestInfoWindow.dialogWindow.boundries.set_y(-QuestInfoWindow.dialogWindow.boundries.get_height());
		QuestInfoWindow.dialogWindow.StartHammerEffect(_x, _y);
	}

	private static void DialogClose()
	{
		if (QuestInfoWindow.dialogWindow == null)
		{
			return;
		}
		AndromedaGui.gui.RemoveWindow(QuestInfoWindow.dialogWindow.handler);
		AndromedaGui.gui.activeToolTipId = -1;
		QuestInfoWindow.dialogWindow = null;
	}

	private static void DialogNoBtnClicked(object prm)
	{
		QuestInfoWindow.DialogClose();
	}

	private static void DialogYesBtnClicked(object prm)
	{
		// 
		// Current member / type: System.Void QuestInfoWindow::DialogYesBtnClicked(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void DialogYesBtnClicked(System.Object)
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

	public static void Initialise()
	{
		if (QuestInfoWindow.IsOnScreen)
		{
			QuestInfoWindow.infoWindow = null;
		}
	}

	private static void OnCloseButtonClicked(object prm)
	{
		if (NetworkScript.player.shipScript != null)
		{
			NetworkScript.player.shipScript.isGuiClosed = true;
		}
		if (QuestInfoWindow.OnCloseInfoWindow != null)
		{
			QuestInfoWindow.OnCloseInfoWindow.Invoke();
			QuestInfoWindow.OnCloseInfoWindow = null;
		}
		AndromedaGui.gui.RemoveWindow(QuestInfoWindow.infoWindow.handler);
		QuestInfoWindow.infoWindow = null;
		QuestInfoWindow.selectedQuest = null;
	}

	private static void OnCollectRewardClicked(object prm)
	{
		// 
		// Current member / type: System.Void QuestInfoWindow::OnCollectRewardClicked(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnCollectRewardClicked(System.Object)
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

	private static void OnSkipQuestClicked(object prm)
	{
		QuestInfoWindow.CreateSkipQuestDialog();
	}

	private static void OnToogleObjectiveInfo(EventHandlerParam prm)
	{
		GuiCheckbox guiCheckbox = (GuiCheckbox)prm.customData;
		QuestInfoWindow.ToogleObjectivesInfo(!QuestInfoWindow.checkBoxState);
		guiCheckbox.Selected = QuestInfoWindow.checkBoxState;
	}

	[DebuggerHidden]
	private static IEnumerator PlayCollectRewardSound()
	{
		return new QuestInfoWindow.<PlayCollectRewardSound>c__Iterator12();
	}

	public static void Populate(QuestEngineEnum questEnginCommand, int questEngineId)
	{
		QuestInfoWindow.<Populate>c__AnonStorey5A variable = null;
		if (!QuestInfoWindow.IsOnScreen)
		{
			return;
		}
		if (QuestInfoWindow.selectedQuest == null)
		{
			return;
		}
		if ((questEnginCommand == 2 || questEnginCommand == 9 || questEnginCommand == 1) && Enumerable.FirstOrDefault<NewQuestObjective>(Enumerable.Where<NewQuestObjective>(QuestInfoWindow.selectedQuest.objectives, new Func<NewQuestObjective, bool>(variable, (NewQuestObjective t) => t.id == this.questEngineId))) == null)
		{
			return;
		}
		if (questEnginCommand == 3 && questEngineId != QuestInfoWindow.selectedQuest.id)
		{
			return;
		}
		NewQuest newQuest = QuestInfoWindow.selectedQuest;
		bool flag = QuestInfoWindow.isObjectivesOnscreen;
		QuestInfoWindow.Close();
		QuestInfoWindow.Create(newQuest, flag, false);
	}

	public static void ReorderGui()
	{
		if (QuestInfoWindow.dialogWindow != null)
		{
			QuestInfoWindow.dialogWindow.PutToCenter();
		}
		if (QuestInfoWindow.infoWindow != null)
		{
			QuestInfoWindow.infoWindow.PutToCenter();
		}
	}

	public static void ResetActiveWindow()
	{
		QuestInfoWindow.CloseAciveWindow = null;
	}

	public static void SetCloseAciveWindow(Action action)
	{
		QuestInfoWindow.CloseAciveWindow = action;
	}

	public static void SetMuteActions(Action mute, Action unmute)
	{
		QuestInfoWindow.MuteAction = mute;
		QuestInfoWindow.UnmuteAction = unmute;
	}

	private static void ToogleObjectivesInfo(bool state)
	{
		if (state == QuestInfoWindow.checkBoxState)
		{
			return;
		}
		QuestInfoWindow.checkBoxState = state;
		QuestTrackerWindow.ToogleQuestObjectivesInfo(QuestInfoWindow.selectedQuest.id);
	}

	private class ObjectiveInfoItem
	{
		private NewQuestObjective objective;

		private GuiTexture frameBackground;

		private GuiTexture avatar;

		private GuiButtonFixed infoButton;

		private GuiButtonFixed boostButton;

		private GuiButtonFixed bringToButton;

		private GuiLabel descriptionLabel;

		private GuiLabel optionalInfoLabel;

		private GuiLabel progressLabel;

		private GuiTextureAnimated bringAnimation;

		private bool isObjectiveLocked;

		private bool isInfoOnScreen;

		public ObjectiveInfoItem()
		{
		}

		private static bool CalculateRightDirection(float coordinateX, float coordinateZ, int galaxyId, out Vector3 rightDirection)
		{
			QuestInfoWindow.ObjectiveInfoItem.<CalculateRightDirection>c__AnonStorey5C variable = null;
			rightDirection = new Vector3(0f, 0f, 0f);
			LevelMap levelMap = NetworkScript.player.vessel.galaxy;
			if (levelMap.IsUltraGalaxyInstance() || levelMap.__galaxyId >= 2000 && levelMap.__galaxyId < 3000)
			{
				return false;
			}
			if (galaxyId != levelMap.get_galaxyId())
			{
				short num = (short)galaxyId;
				HyperJumpNet hyperJumpNet = null;
				if (galaxyId < 2000)
				{
					LevelMap[] levelMapArray = StaticData.allGalaxies;
					if (QuestInfoWindow.ObjectiveInfoItem.<>f__am$cache10 == null)
					{
						QuestInfoWindow.ObjectiveInfoItem.<>f__am$cache10 = new Func<LevelMap, bool>(null, (LevelMap t) => (!t.isPveMap ? true : (!t.isPveMap ? false : t.fraction == NetworkScript.player.vessel.fractionId)));
					}
					IEnumerable<LevelMap> enumerable = Enumerable.Where<LevelMap>(levelMapArray, QuestInfoWindow.ObjectiveInfoItem.<>f__am$cache10);
					if (QuestInfoWindow.ObjectiveInfoItem.<>f__am$cache11 == null)
					{
						QuestInfoWindow.ObjectiveInfoItem.<>f__am$cache11 = new Func<LevelMap, short>(null, (LevelMap s) => s.get_galaxyId());
					}
					Enumerable.ToList<short>(Enumerable.Select<LevelMap, short>(enumerable, QuestInfoWindow.ObjectiveInfoItem.<>f__am$cache11));
					List<HyperJumpNet> list = new List<HyperJumpNet>();
					IEnumerator<LevelMap> enumerator = Enumerable.Where<LevelMap>(StaticData.allGalaxies, new Func<LevelMap, bool>(variable, (LevelMap t) => this.allGalaxiesWithAccess.Contains(t.get_galaxyId()))).GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							LevelMap current = enumerator.get_Current();
							List<HyperJumpNet> list1 = list;
							HyperJumpNet[] hyperJumpNetArray = current.hyperJumps;
							if (QuestInfoWindow.ObjectiveInfoItem.<>f__am$cache12 == null)
							{
								QuestInfoWindow.ObjectiveInfoItem.<>f__am$cache12 = new Func<HyperJumpNet, bool>(null, (HyperJumpNet t) => t.fractionId == NetworkScript.player.vessel.fractionId);
							}
							list1.AddRange(Enumerable.Where<HyperJumpNet>(hyperJumpNetArray, QuestInfoWindow.ObjectiveInfoItem.<>f__am$cache12));
						}
					}
					finally
					{
						if (enumerator == null)
						{
						}
						enumerator.Dispose();
					}
					list = Enumerable.ToList<HyperJumpNet>(Enumerable.Where<HyperJumpNet>(list, new Func<HyperJumpNet, bool>(variable, (HyperJumpNet t) => t.galaxyDst == this.<>f__ref$93.galaxyId)));
					hyperJumpNet = Enumerable.FirstOrDefault<HyperJumpNet>(Enumerable.Where<HyperJumpNet>(list, new Func<HyperJumpNet, bool>(variable, (HyperJumpNet t) => t.galaxySrc == this.<>f__ref$93.galaxy.get_galaxyId())));
					if (hyperJumpNet == null)
					{
						if (levelMap.get_galaxyId() > 2999)
						{
							return false;
						}
						num = (short)Enumerable.First<HyperJumpNet>(list).galaxySrc;
					}
					else
					{
						rightDirection.x = hyperJumpNet.x;
						rightDirection.z = hyperJumpNet.z;
					}
				}
				if (num < 1000)
				{
					if (levelMap.get_galaxyId() >= num)
					{
						int _galaxyId = 1000;
						HyperJumpNet[] hyperJumpNetArray1 = levelMap.hyperJumps;
						if (QuestInfoWindow.ObjectiveInfoItem.<>f__am$cache14 == null)
						{
							QuestInfoWindow.ObjectiveInfoItem.<>f__am$cache14 = new Func<HyperJumpNet, bool>(null, (HyperJumpNet t) => t.fractionId == NetworkScript.player.vessel.fractionId);
						}
						IEnumerator<HyperJumpNet> enumerator1 = Enumerable.Where<HyperJumpNet>(hyperJumpNetArray1, QuestInfoWindow.ObjectiveInfoItem.<>f__am$cache14).GetEnumerator();
						try
						{
							while (enumerator1.MoveNext())
							{
								HyperJumpNet current1 = enumerator1.get_Current();
								if (current1.galaxyDst != galaxyId)
								{
									if (current1.galaxyDst >= levelMap.get_galaxyId() || current1.galaxyDst - levelMap.get_galaxyId() >= _galaxyId)
									{
										continue;
									}
									hyperJumpNet = current1;
									_galaxyId = current1.galaxyDst - levelMap.get_galaxyId();
								}
								else
								{
									hyperJumpNet = current1;
									break;
								}
							}
						}
						finally
						{
							if (enumerator1 == null)
							{
							}
							enumerator1.Dispose();
						}
						rightDirection.x = hyperJumpNet.x;
						rightDirection.z = hyperJumpNet.z;
					}
					else
					{
						int _galaxyId1 = 1000;
						HyperJumpNet[] hyperJumpNetArray2 = levelMap.hyperJumps;
						if (QuestInfoWindow.ObjectiveInfoItem.<>f__am$cache13 == null)
						{
							QuestInfoWindow.ObjectiveInfoItem.<>f__am$cache13 = new Func<HyperJumpNet, bool>(null, (HyperJumpNet t) => t.fractionId == NetworkScript.player.vessel.fractionId);
						}
						IEnumerator<HyperJumpNet> enumerator2 = Enumerable.Where<HyperJumpNet>(hyperJumpNetArray2, QuestInfoWindow.ObjectiveInfoItem.<>f__am$cache13).GetEnumerator();
						try
						{
							while (enumerator2.MoveNext())
							{
								HyperJumpNet hyperJumpNet1 = enumerator2.get_Current();
								if (hyperJumpNet1.galaxyDst != num)
								{
									if (hyperJumpNet1.galaxyDst <= levelMap.get_galaxyId() || hyperJumpNet1.galaxyDst - levelMap.get_galaxyId() >= _galaxyId1)
									{
										continue;
									}
									hyperJumpNet = hyperJumpNet1;
									_galaxyId1 = hyperJumpNet1.galaxyDst - levelMap.get_galaxyId();
								}
								else
								{
									hyperJumpNet = hyperJumpNet1;
									break;
								}
							}
						}
						finally
						{
							if (enumerator2 == null)
							{
							}
							enumerator2.Dispose();
						}
						rightDirection.x = hyperJumpNet.x;
						rightDirection.z = hyperJumpNet.z;
					}
				}
			}
			else
			{
				rightDirection.x = coordinateX;
				rightDirection.z = coordinateZ;
			}
			return true;
		}

		public void Create(NewQuestObjective objective, int index, GuiWindow window, bool isDayly = false)
		{
			QuestInfoWindow.ObjectiveInfoItem.<Create>c__AnonStorey5B variable = null;
			if (objective == null)
			{
				return;
			}
			this.objective = objective;
			if (objective.parentObjectiveId != 0)
			{
				NewQuestObjective newQuestObjective = Enumerable.FirstOrDefault<NewQuestObjective>(Enumerable.Where<NewQuestObjective>(Enumerable.First<NewQuest>(Enumerable.Where<NewQuest>(Enumerable.Union<NewQuest>(StaticData.allQuests, StaticData.allDailyQuests), new Func<NewQuest, bool>(variable, (NewQuest t) => t.id == this.objective.questId))).objectives, new Func<NewQuestObjective, bool>(variable, (NewQuestObjective o) => o.id == this.objective.parentObjectiveId)));
				if (newQuestObjective == null)
				{
					Debug.Log("parentObjective==null");
				}
				this.isObjectiveLocked = !newQuestObjective.IsComplete(NetworkScript.player);
			}
			this.frameBackground = new GuiTexture();
			this.frameBackground.SetTexture("QuestInfoWindow", "objectiveFrameInProgress");
			this.frameBackground.X = 16f;
			this.frameBackground.Y = (float)(192 + 92 * index);
			window.AddGuiElement(this.frameBackground);
			this.avatar = new GuiTexture();
			this.avatar.SetTexture("QuestInfoWindow", "objectiveLocked");
			this.avatar.boundries = new Rect(87f, (float)(219 + 92 * index), 80f, 80f);
			window.AddGuiElement(this.avatar);
			if (!this.isObjectiveLocked)
			{
				this.SetObjectiveAvatar();
			}
			this.infoButton = new GuiButtonFixed();
			this.infoButton.SetTexture("QuestInfoWindow", "buttonInfo");
			this.infoButton.X = 30f;
			this.infoButton.Y = (float)(218 + 92 * index);
			this.infoButton.Caption = string.Empty;
			this.infoButton.Clicked = new Action<EventHandlerParam>(this, QuestInfoWindow.ObjectiveInfoItem.ToogleObjectiveInfo);
			this.infoButton.isEnabled = (this.isObjectiveLocked ? false : !isDayly);
			window.AddGuiElement(this.infoButton);
			this.boostButton = new GuiButtonFixed();
			this.boostButton.SetTexture("QuestInfoWindow", "buttonBoost");
			this.boostButton.X = 742f;
			this.boostButton.Y = (float)(218 + 92 * index);
			this.boostButton.Caption = string.Empty;
			this.boostButton.Clicked = new Action<EventHandlerParam>(this, QuestInfoWindow.ObjectiveInfoItem.OnBoostButtonClicked);
			this.boostButton.isEnabled = !this.isObjectiveLocked;
			window.AddGuiElement(this.boostButton);
			this.PopulateBoostButton();
			this.descriptionLabel = new GuiLabel()
			{
				boundries = new Rect(192f, (float)(222 + 92 * index), 520f, 75f),
				text = string.Empty,
				Font = GuiLabel.FontBold,
				FontSize = 20,
				Alignment = 3,
				Clipping = 1,
				TextColor = GuiNewStyleBar.blueColor
			};
			window.AddGuiElement(this.descriptionLabel);
			string empty = string.Empty;
			empty = (!objective.haveCustomText ? objective.GetObjectiveDescription() : string.Format(StaticData.Translate(string.Format("key_quest_objective_{0}_custom_description", objective.id)), objective.targetAmount));
			if (objective.isOptional)
			{
				empty = string.Concat(StaticData.Translate("key_quest_info_window_optional_objective"), " ", empty);
				this.optionalInfoLabel = new GuiLabel()
				{
					boundries = new Rect(192f, (float)(280 + 92 * index), 520f, 14f),
					text = StaticData.Translate("key_quest_info_window_optional_bonus_info"),
					Font = GuiLabel.FontBold,
					FontSize = 12,
					Clipping = 1
				};
				window.AddGuiElement(this.optionalInfoLabel);
			}
			this.descriptionLabel.text = empty;
			this.progressLabel = new GuiLabel()
			{
				boundries = new Rect(89f, (float)(275 + 92 * index), 76f, 22f)
			};
			GuiLabel guiLabel = this.progressLabel;
			int amountAt = NetworkScript.player.playerBelongings.playerObjectives.GetAmountAt(objective.id);
			guiLabel.text = string.Format("{0}/{1}", amountAt.ToString("##,##0"), objective.targetAmount.ToString("##,##0"));
			this.progressLabel.Font = GuiLabel.FontBold;
			this.progressLabel.FontSize = 16;
			this.progressLabel.Alignment = 7;
			this.progressLabel.Clipping = 1;
			this.progressLabel.WordWrap = false;
			window.AddGuiElement(this.progressLabel);
			if (!this.isObjectiveLocked && objective.type == 36)
			{
				this.descriptionLabel.boundries = new Rect(192f, (float)(222 + 92 * index), 470f, 75f);
				if (objective.isOptional)
				{
					this.optionalInfoLabel.boundries = new Rect(192f, (float)(280 + 92 * index), 470f, 14f);
				}
				this.bringToButton = new GuiButtonFixed();
				this.bringToButton.SetTexture("QuestInfoWindow", "buttonBringTo");
				this.bringToButton.X = 668f;
				this.bringToButton.Y = (float)(228 + 92 * index);
				this.bringToButton.Caption = string.Empty;
				this.bringToButton.Clicked = new Action<EventHandlerParam>(this, QuestInfoWindow.ObjectiveInfoItem.OnBringToNpcClicked);
				this.bringToButton.isEnabled = !this.isObjectiveLocked;
				window.AddGuiElement(this.bringToButton);
			}
			if (objective.IsComplete(NetworkScript.player))
			{
				this.descriptionLabel.TextColor = GuiNewStyleBar.greenColor;
				this.frameBackground.SetTexture("QuestInfoWindow", "objectiveFrameComplete");
				this.progressLabel.TextColor = GuiNewStyleBar.greenColor;
				window.RemoveGuiElement(this.boostButton);
				window.RemoveGuiElement(this.bringToButton);
				if (objective.targetAmount == 1)
				{
					window.RemoveGuiElement(this.progressLabel);
				}
			}
			else if (this.isObjectiveLocked)
			{
				this.frameBackground.SetTexture("QuestInfoWindow", "objectiveFrameLocked");
				this.descriptionLabel.text = StaticData.Translate("key_quest_info_window_locked_objective");
				this.descriptionLabel.TextColor = Color.get_gray();
				this.progressLabel.text = string.Empty;
				this.progressLabel.TextColor = Color.get_gray();
			}
			else if (objective.type == 36)
			{
				if (!PlayerItems.IsMineral((ushort)objective.targetParam1))
				{
					GuiLabel str = this.progressLabel;
					IEnumerable<SlotItem> enumerable = Enumerable.Where<SlotItem>(NetworkScript.player.playerBelongings.playerItems.slotItems, new Func<SlotItem, bool>(variable, (SlotItem t) => (t.get_ItemType() != this.objective.targetParam1 ? false : t.get_SlotType() == 1)));
					if (QuestInfoWindow.ObjectiveInfoItem.<>f__am$cacheD == null)
					{
						QuestInfoWindow.ObjectiveInfoItem.<>f__am$cacheD = new Func<SlotItem, int>(null, (SlotItem s) => s.get_Amount());
					}
					int num = Enumerable.Sum(Enumerable.Select<SlotItem, int>(enumerable, QuestInfoWindow.ObjectiveInfoItem.<>f__am$cacheD));
					str.text = num.ToString("##,##0");
				}
				else
				{
					GuiLabel str1 = this.progressLabel;
					IEnumerable<SlotItem> enumerable1 = Enumerable.Where<SlotItem>(NetworkScript.player.playerBelongings.playerItems.slotItems, new Func<SlotItem, bool>(variable, (SlotItem t) => (t.get_ItemType() != this.objective.targetParam1 ? false : t.get_SlotType() == 3)));
					if (QuestInfoWindow.ObjectiveInfoItem.<>f__am$cacheC == null)
					{
						QuestInfoWindow.ObjectiveInfoItem.<>f__am$cacheC = new Func<SlotItem, int>(null, (SlotItem s) => s.get_Amount());
					}
					int num1 = Enumerable.Sum(Enumerable.Select<SlotItem, int>(enumerable1, QuestInfoWindow.ObjectiveInfoItem.<>f__am$cacheC));
					str1.text = num1.ToString("##,##0");
				}
				this.PopulateBringToButton();
				if (this.bringToButton.isEnabled)
				{
					this.bringAnimation = new GuiTextureAnimated()
					{
						isHoverAware = true
					};
					this.bringAnimation.Init("BringToAnimation", "BringToAnimation", "BringToAnimation/bring_01");
					this.bringAnimation.X = 668f;
					this.bringAnimation.Y = (float)(228 + 92 * index);
					this.bringAnimation.rotationTime = 1f;
					window.AddGuiElement(this.bringAnimation);
					this.bringToButton.SetTexture("FrameworkGUI", "empty");
					this.bringToButton.boundries.set_width(60f);
					this.bringToButton.boundries.set_height(60f);
				}
			}
		}

		private void OnBoostButtonClicked(object prm)
		{
			if (NetworkScript.isInBase)
			{
				return;
			}
			Vector3 vector3 = new Vector3(0f, 0f, 0f);
			if (NetworkScript.player.vessel.fractionId != 1)
			{
				if (this.objective.factionTwoPointerGalaxyId == 0)
				{
					return;
				}
				if (QuestInfoWindow.ObjectiveInfoItem.CalculateRightDirection(this.objective.factionTwoPointerX, this.objective.factionTwoPointerZ, this.objective.factionTwoPointerGalaxyId, out vector3))
				{
					Minimap.SetDirection(vector3);
					QuestInfoWindow.OnCloseButtonClicked(null);
				}
			}
			else
			{
				if (this.objective.factionOnePointerGalaxyId == 0)
				{
					return;
				}
				if (QuestInfoWindow.ObjectiveInfoItem.CalculateRightDirection(this.objective.factionOnePointerX, this.objective.factionOnePointerZ, this.objective.factionOnePointerGalaxyId, out vector3))
				{
					Minimap.SetDirection(vector3);
					QuestInfoWindow.OnCloseButtonClicked(null);
				}
			}
		}

		private void OnBringToNpcClicked(EventHandlerParam p)
		{
			// 
			// Current member / type: System.Void QuestInfoWindow/ObjectiveInfoItem::OnBringToNpcClicked(EventHandlerParam)
			// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
			// 
			// Product version: 2017.2.502.1
			// Exception in: System.Void OnBringToNpcClicked(EventHandlerParam)
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

		private void PopulateBoostButton()
		{
			if (NetworkScript.isInBase || NetworkScript.player.vessel.fractionId == 1 && this.objective.factionOnePointerGalaxyId == 0 || NetworkScript.player.vessel.fractionId == 2 && this.objective.factionTwoPointerGalaxyId == 0 || this.isObjectiveLocked)
			{
				this.boostButton.isEnabled = false;
			}
			else if ((NetworkScript.player.vessel.fractionId != 1 || this.objective.factionOnePointerGalaxyId < 3000 || this.objective.factionOnePointerGalaxyId == NetworkScript.player.vessel.galaxy.__galaxyId) && (NetworkScript.player.vessel.fractionId != 2 || this.objective.factionTwoPointerGalaxyId < 3000 || this.objective.factionTwoPointerGalaxyId == NetworkScript.player.vessel.galaxy.__galaxyId) && (NetworkScript.player.vessel.fractionId != 1 || this.objective.factionOnePointerGalaxyId >= 1000 || !NetworkScript.player.vessel.galaxy.IsUltraGalaxyInstance() && NetworkScript.player.vessel.galaxy.__galaxyId < 3000) && (NetworkScript.player.vessel.fractionId != 2 || this.objective.factionTwoPointerGalaxyId >= 1000 || !NetworkScript.player.vessel.galaxy.IsUltraGalaxyInstance() && NetworkScript.player.vessel.galaxy.__galaxyId < 3000))
			{
				this.boostButton.isEnabled = true;
			}
			else
			{
				this.boostButton.isEnabled = false;
			}
		}

		private void PopulateBringToButton()
		{
			if (NetworkScript.isInBase || this.isObjectiveLocked)
			{
				this.bringToButton.isEnabled = false;
				return;
			}
			if (NetworkScript.player.shipScript.npcInRange == null || NetworkScript.player.shipScript.npcInRange.npcKey != this.objective.targetParam2)
			{
				this.bringToButton.isEnabled = false;
				return;
			}
			if (!PlayerItems.IsMineral((ushort)this.objective.targetParam1))
			{
				GuiButtonFixed guiButtonFixed = this.bringToButton;
				IEnumerable<SlotItem> enumerable = Enumerable.Where<SlotItem>(NetworkScript.player.playerBelongings.playerItems.slotItems, new Func<SlotItem, bool>(this, (SlotItem t) => (t.get_ItemType() != this.objective.targetParam1 ? false : t.get_SlotType() == 1)));
				if (QuestInfoWindow.ObjectiveInfoItem.<>f__am$cacheF == null)
				{
					QuestInfoWindow.ObjectiveInfoItem.<>f__am$cacheF = new Func<SlotItem, int>(null, (SlotItem s) => s.get_Amount());
				}
				guiButtonFixed.isEnabled = Enumerable.Sum(Enumerable.Select<SlotItem, int>(enumerable, QuestInfoWindow.ObjectiveInfoItem.<>f__am$cacheF)) >= this.objective.targetAmount;
			}
			else
			{
				GuiButtonFixed guiButtonFixed1 = this.bringToButton;
				IEnumerable<SlotItem> enumerable1 = Enumerable.Where<SlotItem>(NetworkScript.player.playerBelongings.playerItems.slotItems, new Func<SlotItem, bool>(this, (SlotItem t) => (t.get_ItemType() != this.objective.targetParam1 ? false : t.get_SlotType() == 3)));
				if (QuestInfoWindow.ObjectiveInfoItem.<>f__am$cacheE == null)
				{
					QuestInfoWindow.ObjectiveInfoItem.<>f__am$cacheE = new Func<SlotItem, int>(null, (SlotItem s) => s.get_Amount());
				}
				guiButtonFixed1.isEnabled = Enumerable.Sum(Enumerable.Select<SlotItem, int>(enumerable1, QuestInfoWindow.ObjectiveInfoItem.<>f__am$cacheE)) >= this.objective.targetAmount;
			}
		}

		private void SetObjectiveAvatar()
		{
			switch (this.objective.type)
			{
				case 0:
				{
					this.avatar.SetTexture("TutorialWindow", "equipWeapon");
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				case 1:
				case 2:
				case 3:
				case 4:
				case 9:
				{
					if (this.objective.targetParam1 != PlayerItems.TypeNova)
					{
						this.avatar.SetTexture("MineralsAvatars", StaticData.allTypes.get_Item((ushort)this.objective.targetParam1).assetName);
						this.avatar.boundries = new Rect(87f, 27f + this.frameBackground.Y, 80f, 80f);
					}
					else
					{
						this.avatar.SetItemTexture(PlayerItems.TypeNova);
						this.avatar.boundries = new Rect(87f, 42f + this.frameBackground.Y, 80f, 55f);
					}
					break;
				}
				case 5:
				case 6:
				case 10:
				case 25:
				case 26:
				case 27:
				case 28:
				{
					this.avatar.SetItemTexture((ushort)this.objective.targetParam1);
					this.avatar.boundries = new Rect(87f, 42f + this.frameBackground.Y, 80f, 55f);
					break;
				}
				case 7:
				case 8:
				{
					if (this.objective.targetParam1 == 0)
					{
						this.avatar.SetTexture("QuestObjectivesArt", "DefaultPve");
						this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					}
					else
					{
						if (NetworkScript.player.vessel.galaxy.get_galaxyId() != 1000)
						{
							this.avatar.SetTexture("Targeting", TargetingWnd.GetPveAvatarName(Enumerable.First<PvEInfo>(Enumerable.Where<PvEInfo>(StaticData.pveTypes, new Func<PvEInfo, bool>(this, (PvEInfo t) => t.pveKey == this.objective.targetParam1))).assetName));
						}
						else
						{
							this.avatar.SetTexture("TutorialWindow", "AnnihilatorBoss");
						}
						this.avatar.boundries = new Rect(87f, 27f + this.frameBackground.Y, 80f, 80f);
					}
					break;
				}
				case 11:
				case 12:
				case 13:
				case 14:
				{
					this.avatar.SetTexture("QuestObjectivesArt", "DefaultPvp");
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				case 15:
				case 16:
				case 29:
				{
					this.avatar.SetTexture("ShipsAvatars", Enumerable.First<ShipsTypeNet>(Enumerable.Where<ShipsTypeNet>(StaticData.shipTypes, new Func<ShipsTypeNet, bool>(this, (ShipsTypeNet t) => t.id == this.objective.targetParam1))).assetName);
					this.avatar.boundries = new Rect(87f, 42f + this.frameBackground.Y, 80f, 55f);
					break;
				}
				case 17:
				case 18:
				case 21:
				case 22:
				case 31:
				{
					this.avatar.SetTexture("QuestObjectivesArt", "DefaultExtractionPoint");
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				case 19:
				case 20:
				{
					this.avatar.SetTexture("QuestObjectivesArt", "DefaultMultikill");
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				case 23:
				{
					this.avatar.SetItemTexture(PlayerItems.TypeUltralibrium);
					this.avatar.boundries = new Rect(87f, 42f + this.frameBackground.Y, 80f, 55f);
					break;
				}
				case 24:
				{
					this.avatar.SetTexture("QuestObjectivesArt", "DefaultPvpDeathmatch");
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				case 30:
				{
					this.avatar.SetTexture("QuestObjectivesArt", "DefaultGuild");
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				case 32:
				case 33:
				case 34:
				{
					this.avatar.SetTexture("QuestObjectivesArt", "DefaultCheckpoint");
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				case 35:
				{
					this.avatar.SetTexture("QuestTrackerAvatars", StaticData.Translate(Enumerable.First<NpcObjectPhysics>(Enumerable.Where<NpcObjectPhysics>(StaticData.allNPCs, new Func<NpcObjectPhysics, bool>(this, (NpcObjectPhysics t) => t.npcKey == this.objective.targetParam2))).assetName));
					this.avatar.boundries = new Rect(87f, 27f + this.frameBackground.Y, 80f, 80f);
					break;
				}
				case 36:
				case 78:
				{
					if (!PlayerItems.IsMineral((ushort)this.objective.targetParam1))
					{
						this.avatar.SetItemTexture((ushort)this.objective.targetParam1);
						this.avatar.boundries = new Rect(87f, 42f + this.frameBackground.Y, 80f, 55f);
					}
					else
					{
						this.avatar.SetTexture("MineralsAvatars", StaticData.allTypes.get_Item((ushort)this.objective.targetParam1).assetName);
						this.avatar.boundries = new Rect(87f, 27f + this.frameBackground.Y, 80f, 80f);
					}
					break;
				}
				case 37:
				{
					this.avatar.SetTexture("QuestObjectivesArt", "DefaultSpaceStation");
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				case 38:
				{
					this.avatar.SetTexture("QuestObjectivesArt", "DefaultSkills");
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				case 39:
				{
					this.avatar.SetTexture("Skills", StaticData.allTypes.get_Item((ushort)this.objective.targetParam1).assetName);
					this.avatar.boundries = new Rect(99f, 39f + this.frameBackground.Y, 56f, 56f);
					break;
				}
				case 40:
				case 41:
				{
					this.avatar.SetTexture("QuestObjectivesArt", "DefaultPvpWithSkills");
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				case 42:
				case 43:
				{
					this.avatar.SetTexture("QuestObjectivesArt", "DefaultPvp");
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				case 44:
				case 45:
				case 50:
				case 51:
				{
					this.avatar.SetTexture("QuestObjectivesArt", "DefaultPveWithSkills");
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				case 46:
				case 47:
				case 52:
				case 53:
				{
					this.avatar.SetTexture("QuestObjectivesArt", "DefaultPve");
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				case 48:
				case 49:
				case 54:
				case 55:
				{
					this.avatar.SetTexture("QuestObjectivesArt", "DefaultPveWithCritical");
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				case 56:
				case 57:
				case 62:
				case 63:
				{
					this.avatar.SetTexture("QuestObjectivesArt", "DefaultPveBossWithSkills");
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				case 58:
				case 59:
				case 64:
				case 65:
				{
					this.avatar.SetTexture("QuestObjectivesArt", "DefaultPveBoss");
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				case 60:
				case 61:
				case 66:
				case 67:
				{
					this.avatar.SetTexture("QuestObjectivesArt", "DefaultPveBossWithCritical");
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				case 68:
				case 69:
				{
					this.avatar.SetTexture("QuestObjectivesArt", "DefaultCriticalStrike");
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				case 70:
				case 71:
				case 74:
				case 75:
				{
					this.avatar.SetTexture("QuestObjectivesArt", "DefaultHeal");
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				case 72:
				case 73:
				{
					this.avatar.SetTexture("QuestObjectivesArt", "DefaultPartyHeal");
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				case 76:
				{
					ushort typeCash = PlayerItems.TypeCash;
					switch (this.objective.targetParam1)
					{
						case 0:
						{
							typeCash = PlayerItems.TypeCash;
							break;
						}
						case 1:
						{
							typeCash = PlayerItems.TypeNova;
							break;
						}
						case 2:
						{
							typeCash = PlayerItems.TypeEquilibrium;
							break;
						}
						case 3:
						{
							typeCash = PlayerItems.TypeUltralibrium;
							break;
						}
					}
					this.avatar.SetItemTexture(typeCash);
					this.avatar.boundries = new Rect(87f, 42f + this.frameBackground.Y, 80f, 55f);
					break;
				}
				case 77:
				{
					this.avatar.SetItemTexture(PlayerItems.TypeCash);
					this.avatar.boundries = new Rect(87f, 42f + this.frameBackground.Y, 80f, 55f);
					break;
				}
				case 79:
				{
					this.avatar.SetTexture("GalaxiesAvatars", Enumerable.First<LevelMap>(Enumerable.Where<LevelMap>(StaticData.allGalaxies, new Func<LevelMap, bool>(this, (LevelMap t) => t.galaxyKey == this.objective.targetGalaxyKey))).minimapAssetName);
					this.avatar.boundries = new Rect(77f, 17f + this.frameBackground.Y, 100f, 100f);
					break;
				}
				case 80:
				{
					this.avatar.SetTexture("QuestObjectivesArt", "DefaultSpeedBoost");
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				case 81:
				{
					if (this.objective.targetParam1 == 0)
					{
						this.avatar.SetTexture("QuestObjectivesArt", "DefaultBuyShip");
						this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					}
					else
					{
						this.avatar.SetTexture("ShipsAvatars", Enumerable.First<ShipsTypeNet>(Enumerable.Where<ShipsTypeNet>(StaticData.shipTypes, new Func<ShipsTypeNet, bool>(this, (ShipsTypeNet t) => t.id == this.objective.targetParam1))).assetName);
						this.avatar.boundries = new Rect(87f, 42f + this.frameBackground.Y, 80f, 55f);
					}
					break;
				}
				case 82:
				{
					string str = "DefaultBuyItems";
					switch (this.objective.targetParam1)
					{
						case 1:
						{
							str = "ItemCategoryWeapons";
							break;
						}
						case 2:
						{
							str = "ItemCategoryCorpus";
							break;
						}
						case 3:
						{
							str = "ItemCategoryShield";
							break;
						}
						case 4:
						{
							str = "ItemCategoryEngine";
							break;
						}
						case 5:
						{
							str = "ItemCategoryExtras";
							break;
						}
					}
					this.avatar.SetTexture("QuestObjectivesArt", str);
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				case 83:
				{
					string str1 = "DefaultGambleItems";
					switch (this.objective.targetParam1)
					{
						case 1:
						{
							str1 = "ItemCategoryWeapons";
							break;
						}
						case 2:
						{
							str1 = "ItemCategoryCorpus";
							break;
						}
						case 3:
						{
							str1 = "ItemCategoryShield";
							break;
						}
						case 4:
						{
							str1 = "ItemCategoryEngine";
							break;
						}
						case 5:
						{
							str1 = "ItemCategoryExtras";
							break;
						}
					}
					this.avatar.SetTexture("QuestObjectivesArt", str1);
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				case 84:
				{
					this.avatar.SetTexture("QuestObjectivesArt", "DefaultItemSell");
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				case 85:
				{
					this.avatar.SetTexture("QuestObjectivesArt", "DefaultWeaponUpgrade");
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				case 86:
				{
					this.avatar.SetTexture("QuestObjectivesArt", "DefaultShipUpgrade");
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				case 87:
				{
					this.avatar.SetTexture("QuestObjectivesArt", "DefaultUpgrade");
					this.avatar.boundries = new Rect(95f, 35f + this.frameBackground.Y, 64f, 64f);
					break;
				}
				default:
				{
					this.avatar.SetTexture("QuestInfoWindow", "objectiveLocked");
					this.avatar.boundries = new Rect(87f, 27f + this.frameBackground.Y, 80f, 80f);
					break;
				}
			}
		}

		private void ToogleObjectiveInfo(object prm)
		{
			if (this.descriptionLabel == null)
			{
				return;
			}
			if (!this.isInfoOnScreen)
			{
				this.descriptionLabel.text = StaticData.Translate(string.Format("key_quest_objective_{0}_info", this.objective.id));
				if (this.objective.isOptional)
				{
					this.optionalInfoLabel.text = string.Empty;
				}
			}
			else
			{
				string empty = string.Empty;
				empty = (!this.objective.haveCustomText ? this.objective.GetObjectiveDescription() : string.Format(StaticData.Translate(string.Format("key_quest_objective_{0}_custom_description", this.objective.id)), this.objective.targetAmount));
				if (this.objective.isOptional)
				{
					empty = string.Concat(StaticData.Translate("key_quest_info_window_optional_objective"), " ", empty);
					this.optionalInfoLabel.text = StaticData.Translate("key_quest_info_window_optional_bonus_info");
				}
				this.descriptionLabel.text = empty;
			}
			this.isInfoOnScreen = !this.isInfoOnScreen;
		}
	}
}