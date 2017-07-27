using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class QuestTrackerWindow
{
	private static List<int> startingQuestQueue;

	private static GuiWindow questTrackerWindow;

	private static SortedList<int, QuestTrackerWindow.QuestTrackerItem> activeQuests;

	private static int startIndex;

	private static GuiButtonFixed upButton;

	private static GuiButtonFixed downButton;

	private static bool hidingQuestInProgress;

	private static int hidingQuestInProgressId;

	private static bool isObjectivesOn;

	private static int selectedQuestId;

	private static int maxQuestTrackersOnScreen;

	static QuestTrackerWindow()
	{
		QuestTrackerWindow.startingQuestQueue = new List<int>();
		QuestTrackerWindow.activeQuests = new SortedList<int, QuestTrackerWindow.QuestTrackerItem>();
		QuestTrackerWindow.startIndex = 0;
		QuestTrackerWindow.selectedQuestId = 0;
		QuestTrackerWindow.maxQuestTrackersOnScreen = 0;
	}

	private static void AddMinimapPointers(NewQuest quest)
	{
		QuestTrackerWindow.<AddMinimapPointers>c__AnonStorey61 variable = null;
		Minimap.Destination destination;
		if (NetworkScript.isInBase)
		{
			return;
		}
		Minimap.ClearObjectivePointer();
		foreach (NewQuestObjective objective in quest.objectives)
		{
			bool flag = false;
			if (objective.parentObjectiveId != 0)
			{
				NewQuestObjective newQuestObjective = Enumerable.FirstOrDefault<NewQuestObjective>(Enumerable.Where<NewQuestObjective>(quest.objectives, new Func<NewQuestObjective, bool>(variable, (NewQuestObjective o) => o.id == this.objective.parentObjectiveId)));
				if (newQuestObjective == null)
				{
					Debug.Log("parentObjective==null");
				}
				flag = !newQuestObjective.IsComplete(NetworkScript.player);
			}
			if (!flag)
			{
				if (!objective.IsComplete(NetworkScript.player))
				{
					if (NetworkScript.player.vessel.fractionId != 1)
					{
						if (objective.factionTwoPointerGalaxyId == 0)
						{
							continue;
						}
						destination = new Minimap.Destination()
						{
							galaxyId = objective.factionTwoPointerGalaxyId,
							onlyGalaxy = objective.onlyGalaxyPointer,
							x = objective.factionTwoPointerX,
							z = objective.factionTwoPointerZ
						};
						Minimap.AddQuestObjectivePointer(destination);
					}
					else if (objective.factionOnePointerGalaxyId != 0)
					{
						destination = new Minimap.Destination()
						{
							galaxyId = objective.factionOnePointerGalaxyId,
							onlyGalaxy = objective.onlyGalaxyPointer,
							x = objective.factionOnePointerX,
							z = objective.factionOnePointerZ
						};
						Minimap.AddQuestObjectivePointer(destination);
					}
				}
			}
		}
	}

	private static void CalculateMaxQuestsTrackersOnScreen()
	{
		QuestTrackerWindow.maxQuestTrackersOnScreen = Mathf.Max(1, (Screen.get_height() - 415) / 117);
	}

	public static void FinishQuest(int questId)
	{
		if (QuestTrackerWindow.activeQuests.ContainsKey(questId))
		{
			if (!QuestTrackerWindow.activeQuests.get_Item(questId).IsOnScreen)
			{
				QuestTrackerWindow.RemoveFinishedQuest(questId);
			}
			else
			{
				QuestTrackerWindow.hidingQuestInProgress = true;
				QuestTrackerWindow.hidingQuestInProgressId = questId;
				QuestTrackerWindow.activeQuests.get_Item(questId).FinishQuest();
			}
			if (questId == QuestTrackerWindow.selectedQuestId)
			{
				QuestTrackerWindow.selectedQuestId = 0;
				if (!NetworkScript.isInBase)
				{
					Minimap.ClearObjectivePointer();
				}
			}
		}
	}

	private static void HideObjectivesInfo()
	{
		IEnumerator<QuestTrackerWindow.QuestTrackerItem> enumerator = QuestTrackerWindow.activeQuests.get_Values().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				enumerator.get_Current().HideObjectiveInfo();
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

	public static void Initialise()
	{
		QuestTrackerWindow.<Initialise>c__AnonStorey5E variable = null;
		if (NetworkScript.player == null || NetworkScript.player.playerBelongings == null || NetworkScript.player.playerBelongings.playerQuests == null)
		{
			return;
		}
		IList<PlayerQuest> values = NetworkScript.player.playerBelongings.playerQuests.get_Values();
		if (QuestTrackerWindow.<>f__am$cacheB == null)
		{
			QuestTrackerWindow.<>f__am$cacheB = new Func<PlayerQuest, bool>(null, (PlayerQuest t) => !t.isRewordCollected);
		}
		IEnumerable<PlayerQuest> enumerable = Enumerable.Where<PlayerQuest>(values, QuestTrackerWindow.<>f__am$cacheB);
		if (QuestTrackerWindow.<>f__am$cacheC == null)
		{
			QuestTrackerWindow.<>f__am$cacheC = new Func<PlayerQuest, int>(null, (PlayerQuest s) => s.currentQuestId);
		}
		List<int> list = Enumerable.ToList<int>(Enumerable.Select<PlayerQuest, int>(enumerable, QuestTrackerWindow.<>f__am$cacheC));
		if (list.get_Count() < 1)
		{
			return;
		}
		int[] array = Enumerable.ToArray<int>(QuestTrackerWindow.activeQuests.get_Keys());
		for (int i = 0; i < (int)array.Length; i++)
		{
			int num = array[i];
			if (!list.Contains(num))
			{
				QuestTrackerWindow.activeQuests.Remove(num);
			}
		}
		QuestTrackerWindow.startingQuestQueue.Clear();
		foreach (int num1 in list)
		{
			if (Enumerable.FirstOrDefault<NewQuest>(Enumerable.Where<NewQuest>(Enumerable.Union<NewQuest>(StaticData.allQuests, StaticData.allDailyQuests), new Func<NewQuest, bool>(variable, (NewQuest t) => t.id == this.id))) != null)
			{
				if (QuestTrackerWindow.activeQuests.ContainsKey(num1))
				{
					continue;
				}
				QuestTrackerWindow.activeQuests.Add(num1, new QuestTrackerWindow.QuestTrackerItem(num1));
			}
			else
			{
				Debug.LogError(string.Concat("Can't find quest with id ", num1));
			}
		}
		QuestTrackerWindow.CalculateMaxQuestsTrackersOnScreen();
		if (Enumerable.Count<KeyValuePair<int, QuestTrackerWindow.QuestTrackerItem>>(QuestTrackerWindow.activeQuests) > QuestTrackerWindow.maxQuestTrackersOnScreen)
		{
			QuestTrackerWindow.questTrackerWindow = new GuiWindow()
			{
				boundries = new Rect(0f, 82f, 120f, (float)(152 + QuestTrackerWindow.maxQuestTrackersOnScreen * 117)),
				isHidden = false,
				zOrder = 199
			};
			AndromedaGui.gui.AddWindow(QuestTrackerWindow.questTrackerWindow);
			QuestTrackerWindow.upButton = new GuiButtonFixed();
			QuestTrackerWindow.upButton.SetTexture("QuestTracker", "buttonUp");
			QuestTrackerWindow.upButton.X = 0f;
			QuestTrackerWindow.upButton.Y = 0f;
			QuestTrackerWindow.upButton.Caption = string.Empty;
			QuestTrackerWindow.upButton.Clicked = new Action<EventHandlerParam>(null, QuestTrackerWindow.ScrollUp);
			QuestTrackerWindow.questTrackerWindow.AddGuiElement(QuestTrackerWindow.upButton);
			QuestTrackerWindow.downButton = new GuiButtonFixed();
			QuestTrackerWindow.downButton.SetTexture("QuestTracker", "buttonDown");
			QuestTrackerWindow.downButton.X = 0f;
			QuestTrackerWindow.downButton.Y = QuestTrackerWindow.upButton.boundries.get_height() + (float)(QuestTrackerWindow.maxQuestTrackersOnScreen * 117);
			QuestTrackerWindow.downButton.Caption = string.Empty;
			QuestTrackerWindow.downButton.Clicked = new Action<EventHandlerParam>(null, QuestTrackerWindow.ScrollDown);
			QuestTrackerWindow.questTrackerWindow.AddGuiElement(QuestTrackerWindow.downButton);
			QuestTrackerWindow.PopulateScrollArrow();
		}
		if (QuestTrackerWindow.selectedQuestId != 0)
		{
			NewQuest[] newQuestArray = StaticData.allQuests;
			if (QuestTrackerWindow.<>f__am$cacheD == null)
			{
				QuestTrackerWindow.<>f__am$cacheD = new Func<NewQuest, bool>(null, (NewQuest t) => t.id == QuestTrackerWindow.selectedQuestId);
			}
			NewQuest newQuest = Enumerable.FirstOrDefault<NewQuest>(Enumerable.Where<NewQuest>(newQuestArray, QuestTrackerWindow.<>f__am$cacheD));
			if (newQuest != null)
			{
				QuestTrackerWindow.AddMinimapPointers(newQuest);
			}
			else
			{
				QuestTrackerWindow.selectedQuestId = 0;
			}
		}
		QuestTrackerWindow.Redraw(true);
	}

	public static void OpenQuestInfo(int questId)
	{
		if (QuestTrackerWindow.activeQuests.ContainsKey(questId))
		{
			QuestTrackerWindow.activeQuests.get_Item(questId).OpenQuestInfoWindow(null);
		}
	}

	private static void PopulateScrollArrow()
	{
		if (QuestTrackerWindow.upButton == null)
		{
			return;
		}
		QuestTrackerWindow.upButton.isEnabled = QuestTrackerWindow.startIndex > 0;
		QuestTrackerWindow.downButton.isEnabled = QuestTrackerWindow.startIndex < Enumerable.Count<KeyValuePair<int, QuestTrackerWindow.QuestTrackerItem>>(QuestTrackerWindow.activeQuests) - QuestTrackerWindow.maxQuestTrackersOnScreen;
	}

	public static void PutOutOfScreen()
	{
		if (QuestTrackerWindow.questTrackerWindow != null)
		{
			QuestTrackerWindow.questTrackerWindow.boundries.set_x(-QuestTrackerWindow.questTrackerWindow.boundries.get_width());
		}
		QuestTrackerWindow.QuestTrackerItem[] array = Enumerable.ToArray<QuestTrackerWindow.QuestTrackerItem>(QuestTrackerWindow.activeQuests.get_Values());
		for (int i = 0; i < (int)array.Length; i++)
		{
			array[i].GetOutOfScreen();
		}
	}

	private static void Redraw(bool onInit = false)
	{
		QuestTrackerWindow.QuestTrackerItem[] array = Enumerable.ToArray<QuestTrackerWindow.QuestTrackerItem>(QuestTrackerWindow.activeQuests.get_Values());
		if (!onInit)
		{
			QuestTrackerWindow.QuestTrackerItem[] questTrackerItemArray = array;
			for (int i = 0; i < (int)questTrackerItemArray.Length; i++)
			{
				questTrackerItemArray[i].RemoveFromScreen();
			}
		}
		int num = 0;
		int num1 = Math.Min(QuestTrackerWindow.activeQuests.get_Count(), QuestTrackerWindow.maxQuestTrackersOnScreen);
		if (QuestTrackerWindow.startIndex != 0 && QuestTrackerWindow.startIndex + num1 > QuestTrackerWindow.activeQuests.get_Count())
		{
			QuestTrackerWindow.startIndex = 0;
		}
		for (int j = QuestTrackerWindow.startIndex; j < QuestTrackerWindow.startIndex + num1; j++)
		{
			if (QuestTrackerWindow.selectedQuestId == 0)
			{
				QuestTrackerWindow.selectedQuestId = array[j].QuestId;
				NewQuest[] newQuestArray = StaticData.allQuests;
				if (QuestTrackerWindow.<>f__am$cacheF == null)
				{
					QuestTrackerWindow.<>f__am$cacheF = new Func<NewQuest, bool>(null, (NewQuest t) => t.id == QuestTrackerWindow.selectedQuestId);
				}
				QuestTrackerWindow.AddMinimapPointers(Enumerable.First<NewQuest>(Enumerable.Where<NewQuest>(newQuestArray, QuestTrackerWindow.<>f__am$cacheF)));
			}
			if (QuestTrackerWindow.startingQuestQueue.get_Count() <= 0 || !QuestTrackerWindow.startingQuestQueue.Contains(array[j].QuestId))
			{
				array[j].AddToScreen(num, new Action<int>(null, QuestTrackerWindow.RemoveFinishedQuestCallback));
			}
			else
			{
				QuestTrackerWindow.startingQuestQueue.Remove(array[j].QuestId);
				array[j].StartQuest(num, new Action<int>(null, QuestTrackerWindow.RemoveFinishedQuestCallback), QuestTrackerWindow.isObjectivesOn);
			}
			num++;
		}
	}

	private static void RemoveFinishedQuest(int questId)
	{
		if (QuestTrackerWindow.activeQuests.ContainsKey(questId))
		{
			QuestTrackerWindow.activeQuests.Remove(questId);
			NetworkScript.player.playerBelongings.playerQuests.Remove(questId);
		}
		QuestTrackerWindow.UpdateScrollArow();
	}

	private static void RemoveFinishedQuestCallback(int questId)
	{
		if (QuestTrackerWindow.activeQuests.ContainsKey(questId))
		{
			QuestTrackerWindow.activeQuests.Remove(questId);
			NetworkScript.player.playerBelongings.playerQuests.Remove(questId);
		}
		QuestTrackerWindow.hidingQuestInProgress = false;
		QuestTrackerWindow.hidingQuestInProgressId = 0;
		QuestTrackerWindow.UpdateScrollArow();
		QuestTrackerWindow.Redraw(false);
	}

	public static void ReorderGui()
	{
		QuestTrackerWindow.CalculateMaxQuestsTrackersOnScreen();
		QuestTrackerWindow.UpdateScrollArow();
		QuestTrackerWindow.Redraw(false);
	}

	private static void ScrollDown(object prm)
	{
		if (QuestTrackerWindow.hidingQuestInProgressId != 0)
		{
			if (QuestTrackerWindow.activeQuests.ContainsKey(QuestTrackerWindow.hidingQuestInProgressId))
			{
				QuestTrackerWindow.activeQuests.get_Item(QuestTrackerWindow.hidingQuestInProgressId).RemoveFromScreen();
				QuestTrackerWindow.activeQuests.Remove(QuestTrackerWindow.hidingQuestInProgressId);
				NetworkScript.player.playerBelongings.playerQuests.Remove(QuestTrackerWindow.hidingQuestInProgressId);
			}
			QuestTrackerWindow.hidingQuestInProgressId = 0;
			QuestTrackerWindow.hidingQuestInProgress = false;
		}
		QuestTrackerWindow.startIndex = QuestTrackerWindow.startIndex + 1;
		QuestTrackerWindow.startIndex = (QuestTrackerWindow.startIndex < Enumerable.Count<KeyValuePair<int, QuestTrackerWindow.QuestTrackerItem>>(QuestTrackerWindow.activeQuests) - QuestTrackerWindow.maxQuestTrackersOnScreen ? QuestTrackerWindow.startIndex : Enumerable.Count<KeyValuePair<int, QuestTrackerWindow.QuestTrackerItem>>(QuestTrackerWindow.activeQuests) - QuestTrackerWindow.maxQuestTrackersOnScreen);
		QuestTrackerWindow.Redraw(false);
		QuestTrackerWindow.PopulateScrollArrow();
	}

	public static void ScrollQuests()
	{
		if (QuestTrackerWindow.activeQuests.get_Count() <= QuestTrackerWindow.maxQuestTrackersOnScreen)
		{
			return;
		}
		QuestTrackerWindow.startIndex = QuestTrackerWindow.startIndex + 1;
		QuestTrackerWindow.startIndex = (QuestTrackerWindow.startIndex <= Enumerable.Count<KeyValuePair<int, QuestTrackerWindow.QuestTrackerItem>>(QuestTrackerWindow.activeQuests) - QuestTrackerWindow.maxQuestTrackersOnScreen ? QuestTrackerWindow.startIndex : 0);
		QuestTrackerWindow.Redraw(false);
		QuestTrackerWindow.PopulateScrollArrow();
	}

	private static void ScrollUp(object prm)
	{
		if (QuestTrackerWindow.hidingQuestInProgressId != 0)
		{
			if (QuestTrackerWindow.activeQuests.ContainsKey(QuestTrackerWindow.hidingQuestInProgressId))
			{
				QuestTrackerWindow.activeQuests.get_Item(QuestTrackerWindow.hidingQuestInProgressId).RemoveFromScreen();
				QuestTrackerWindow.activeQuests.Remove(QuestTrackerWindow.hidingQuestInProgressId);
				NetworkScript.player.playerBelongings.playerQuests.Remove(QuestTrackerWindow.hidingQuestInProgressId);
			}
			QuestTrackerWindow.hidingQuestInProgressId = 0;
			QuestTrackerWindow.hidingQuestInProgress = false;
		}
		QuestTrackerWindow.startIndex = QuestTrackerWindow.startIndex - 1;
		QuestTrackerWindow.startIndex = (QuestTrackerWindow.startIndex > 0 ? QuestTrackerWindow.startIndex : 0);
		QuestTrackerWindow.Redraw(false);
		QuestTrackerWindow.PopulateScrollArrow();
	}

	public static void SetNewActiveQuest(int questId)
	{
		QuestTrackerWindow.<SetNewActiveQuest>c__AnonStorey60 variable = null;
		if (questId != QuestTrackerWindow.selectedQuestId)
		{
			NewQuest newQuest = Enumerable.FirstOrDefault<NewQuest>(Enumerable.Where<NewQuest>(StaticData.allQuests, new Func<NewQuest, bool>(variable, (NewQuest t) => t.id == this.questId)));
			if (newQuest == null)
			{
				return;
			}
			QuestTrackerWindow.selectedQuestId = questId;
			QuestTrackerWindow.AddMinimapPointers(newQuest);
		}
	}

	private static void ShowObjectivesInfo()
	{
		IEnumerator<QuestTrackerWindow.QuestTrackerItem> enumerator = QuestTrackerWindow.activeQuests.get_Values().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				enumerator.get_Current().ShowObjectiveInfo();
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

	public static void StartHammerEffect()
	{
		if (QuestTrackerWindow.questTrackerWindow != null)
		{
			QuestTrackerWindow.questTrackerWindow.timeHammerFx = 0.5f;
			QuestTrackerWindow.questTrackerWindow.StartHammerEffect(0f, QuestTrackerWindow.questTrackerWindow.boundries.get_y());
		}
		QuestTrackerWindow.QuestTrackerItem[] array = Enumerable.ToArray<QuestTrackerWindow.QuestTrackerItem>(QuestTrackerWindow.activeQuests.get_Values());
		for (int i = 0; i < (int)array.Length; i++)
		{
			array[i].StartHammerEffect();
		}
	}

	private static void StartQuest(int questId)
	{
		// 
		// Current member / type: System.Void QuestTrackerWindow::StartQuest(System.Int32)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void StartQuest(System.Int32)
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

	public static void SwitchObjectiveInfo()
	{
		QuestTrackerWindow.isObjectivesOn = !QuestTrackerWindow.isObjectivesOn;
		if (!QuestTrackerWindow.isObjectivesOn)
		{
			QuestTrackerWindow.HideObjectivesInfo();
		}
		else
		{
			QuestTrackerWindow.ShowObjectivesInfo();
		}
	}

	public static void ToogleQuestObjectivesInfo(int questId)
	{
		if (QuestTrackerWindow.activeQuests.ContainsKey(questId))
		{
			QuestTrackerWindow.activeQuests.get_Item(questId).ToogleObjectivesInfo();
		}
	}

	public static void Update(QuestEngineEnum eventType, int questEngineId)
	{
		if (eventType == 4)
		{
			QuestTrackerWindow.StartQuest(questEngineId);
			return;
		}
		IEnumerator<QuestTrackerWindow.QuestTrackerItem> enumerator = QuestTrackerWindow.activeQuests.get_Values().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				enumerator.get_Current().Poulate(eventType, questEngineId);
			}
		}
		finally
		{
			if (enumerator == null)
			{
			}
			enumerator.Dispose();
		}
		if (QuestTrackerWindow.selectedQuestId != 0)
		{
			NewQuest[] newQuestArray = StaticData.allQuests;
			if (QuestTrackerWindow.<>f__am$cacheE == null)
			{
				QuestTrackerWindow.<>f__am$cacheE = new Func<NewQuest, bool>(null, (NewQuest t) => t.id == QuestTrackerWindow.selectedQuestId);
			}
			NewQuest newQuest = Enumerable.FirstOrDefault<NewQuest>(Enumerable.Where<NewQuest>(newQuestArray, QuestTrackerWindow.<>f__am$cacheE));
			QuestTrackerWindow.AddMinimapPointers(newQuest);
		}
	}

	public static void UpdateBringToObjectivs()
	{
		IEnumerator<QuestTrackerWindow.QuestTrackerItem> enumerator = QuestTrackerWindow.activeQuests.get_Values().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				enumerator.get_Current().PopulateBringToObjectives();
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

	private static void UpdateScrollArow()
	{
		bool flag = Enumerable.Count<KeyValuePair<int, QuestTrackerWindow.QuestTrackerItem>>(QuestTrackerWindow.activeQuests) > QuestTrackerWindow.maxQuestTrackersOnScreen;
		if (QuestTrackerWindow.questTrackerWindow != null)
		{
			if (flag)
			{
				QuestTrackerWindow.questTrackerWindow.boundries = new Rect(0f, 82f, 120f, (float)(152 + QuestTrackerWindow.maxQuestTrackersOnScreen * 117));
				QuestTrackerWindow.downButton.Y = QuestTrackerWindow.upButton.boundries.get_height() + (float)(QuestTrackerWindow.maxQuestTrackersOnScreen * 117);
				QuestTrackerWindow.PopulateScrollArrow();
			}
			else
			{
				QuestTrackerWindow.questTrackerWindow.RemoveGuiElement(QuestTrackerWindow.upButton);
				QuestTrackerWindow.questTrackerWindow.RemoveGuiElement(QuestTrackerWindow.downButton);
				AndromedaGui.gui.RemoveWindow(QuestTrackerWindow.questTrackerWindow.handler);
				QuestTrackerWindow.questTrackerWindow = null;
			}
		}
		else if (flag)
		{
			QuestTrackerWindow.questTrackerWindow = new GuiWindow()
			{
				boundries = new Rect(0f, 82f, 120f, (float)(152 + QuestTrackerWindow.maxQuestTrackersOnScreen * 117)),
				isHidden = false,
				zOrder = 199
			};
			AndromedaGui.gui.AddWindow(QuestTrackerWindow.questTrackerWindow);
			QuestTrackerWindow.upButton = new GuiButtonFixed();
			QuestTrackerWindow.upButton.SetTexture("QuestTracker", "buttonUp");
			QuestTrackerWindow.upButton.X = 0f;
			QuestTrackerWindow.upButton.Y = 0f;
			QuestTrackerWindow.upButton.Caption = string.Empty;
			QuestTrackerWindow.upButton.Clicked = new Action<EventHandlerParam>(null, QuestTrackerWindow.ScrollUp);
			QuestTrackerWindow.questTrackerWindow.AddGuiElement(QuestTrackerWindow.upButton);
			QuestTrackerWindow.downButton = new GuiButtonFixed();
			QuestTrackerWindow.downButton.SetTexture("QuestTracker", "buttonDown");
			QuestTrackerWindow.downButton.X = 0f;
			QuestTrackerWindow.downButton.Y = QuestTrackerWindow.upButton.boundries.get_height() + (float)(QuestTrackerWindow.maxQuestTrackersOnScreen * 117);
			QuestTrackerWindow.downButton.Caption = string.Empty;
			QuestTrackerWindow.downButton.Clicked = new Action<EventHandlerParam>(null, QuestTrackerWindow.ScrollDown);
			QuestTrackerWindow.questTrackerWindow.AddGuiElement(QuestTrackerWindow.downButton);
			QuestTrackerWindow.PopulateScrollArrow();
		}
		if (!flag)
		{
			QuestTrackerWindow.startIndex = 0;
		}
		else if (QuestTrackerWindow.startIndex + QuestTrackerWindow.maxQuestTrackersOnScreen > QuestTrackerWindow.activeQuests.get_Count())
		{
			QuestTrackerWindow.startIndex = QuestTrackerWindow.activeQuests.get_Count() - QuestTrackerWindow.maxQuestTrackersOnScreen;
		}
	}

	private class QuestTrackerItem
	{
		private const float START_QUEST_ANIMATION_TIME = 0.9f;

		private const float PROGRESS_INDICATOR_ANIMATION_TIME = 0.6f;

		private const float ANIMATION_TIME = 0.7f;

		private const float ANIMATION_DESTINATION = 237f;

		private const float OBJECTIVE_PROGRESS_MIN_X = 88f;

		private const float OBJECTIVE_PROGRESS_MAX_X = 325f;

		private const float OBJECTIVE_FRAME_MIN_X = -149f;

		private const float OBJECTIVE_FRAME_MAX_X = 88f;

		private const float WINDOW_MIN_WIDTH = 128f;

		private const float WINDOW_MAX_WIDTH = 364f;

		private const float TEST = 50f;

		private NewQuest quest;

		private GuiWindow holderWindow;

		private GuiTexture background;

		private GuiTexture objectivesBackground;

		private GuiTexture icon;

		private GuiTextureAnimated notificationAnimation;

		private GuiTexture notificationBackground;

		private GuiTexture notificationIcon;

		private GuiTexture progressIndicator;

		private GuiButton openInfoWindowButton;

		private List<GuiElement> objectiveInfoLabels;

		private QuestTrackerWindow.QuestTrackerItemState state;

		private bool isObjectivesOnScreen;

		private bool isGlobalShowObjectivesOn;

		private int progress;

		private int totalObjectiveCount;

		private Action<int> OnFinishQuestAnimationComplete;

		private bool lastState;

		public bool IsOnScreen
		{
			get
			{
				return this.holderWindow != null;
			}
		}

		public int QuestId
		{
			get
			{
				return this.quest.id;
			}
		}

		public QuestTrackerItem(int questId)
		{
			QuestTrackerWindow.QuestTrackerItem.<QuestTrackerItem>c__AnonStorey62 variable = null;
			this.quest = Enumerable.FirstOrDefault<NewQuest>(Enumerable.Where<NewQuest>(Enumerable.Union<NewQuest>(StaticData.allQuests, StaticData.allDailyQuests), new Func<NewQuest, bool>(variable, (NewQuest t) => t.id == this.questId)));
		}

		public void AddToScreen(int index, Action<int> OnRemoveCallback)
		{
			if (this.quest == null)
			{
				return;
			}
			if (this.state == QuestTrackerWindow.QuestTrackerItemState.ShowAnimationInProgress)
			{
				this.state = QuestTrackerWindow.QuestTrackerItemState.NotificationNewQuest;
				if (QuestTrackerWindow.isObjectivesOn)
				{
					this.isObjectivesOnScreen = true;
				}
			}
			this.OnFinishQuestAnimationComplete = OnRemoveCallback;
			this.holderWindow = new GuiWindow()
			{
				boundries = new Rect(0f, (float)(160 + index * 117), 128f, 117f),
				isHidden = false,
				zOrder = 200
			};
			AndromedaGui.gui.AddWindow(this.holderWindow);
			this.holderWindow.customOnGUIAction = new Action(this, QuestTrackerWindow.QuestTrackerItem.OnQuestAvatarHovered);
			this.totalObjectiveCount = Enumerable.Count<NewQuestObjective>(this.quest.objectives);
			this.progress = 0;
			foreach (NewQuestObjective objective in this.quest.objectives)
			{
				if (!objective.IsComplete(NetworkScript.player))
				{
					continue;
				}
				QuestTrackerWindow.QuestTrackerItem questTrackerItem = this;
				questTrackerItem.progress = questTrackerItem.progress + 1;
			}
			this.background = new GuiTexture();
			this.background.SetTexture("QuestTracker", "trackerFrame");
			this.background.X = 0f;
			this.background.Y = 0f;
			this.background.isHoverAware = false;
			this.openInfoWindowButton = new GuiButton()
			{
				boundries = new Rect(0f, 0f, 117f, 117f),
				Alignment = 4,
				FontSize = 32,
				Caption = string.Empty,
				Clicked = new Action<EventHandlerParam>(this, QuestTrackerWindow.QuestTrackerItem.OpenQuestInfoWindow)
			};
			this.objectivesBackground = new GuiTexture();
			this.objectivesBackground.SetTexture("QuestTracker", "objectiveBackground");
			this.objectivesBackground.isHoverAware = false;
			this.objectivesBackground.boundries = new Rect(-149f, 8f, 280f, 100f);
			this.progressIndicator = new GuiTexture();
			this.progressIndicator.SetTexture("QuestTracker", string.Format("progressIdicator_{0}_{1}", this.totalObjectiveCount, this.progress));
			this.progressIndicator.boundries = new Rect(88f, 8f, 40f, 105f);
			this.progressIndicator.isHoverAware = false;
			this.holderWindow.AddGuiElement(this.objectivesBackground);
			this.holderWindow.AddGuiElement(this.background);
			this.holderWindow.AddGuiElement(this.openInfoWindowButton);
			this.holderWindow.AddGuiElement(this.progressIndicator);
			this.icon = new GuiTexture();
			this.icon.SetTexture("FrameworkGUI", "empty");
			this.icon.boundries = new Rect(0f, 0f, 117f, 117f);
			this.icon.isHoverAware = false;
			this.holderWindow.AddGuiElement(this.icon);
			StoryActor storyActor = Enumerable.FirstOrDefault<StoryActor>(Enumerable.Where<StoryActor>(StoryActor.allActor, new Func<StoryActor, bool>(this, (StoryActor t) => t.id == this.quest.actorId)));
			if (storyActor != null)
			{
				if (NetworkScript.player.vessel.galaxy.__galaxyId != 1000)
				{
					this.icon.SetTexture("QuestTrackerAvatars", storyActor.assetName);
				}
				else
				{
					this.icon.SetTexture("TutorialWindow", "Aria");
				}
				this.icon.boundries = new Rect(0f, 0f, 117f, 117f);
			}
			if (this.quest.type == 2)
			{
				if (NetworkScript.player.vessel.fractionId == 1)
				{
					this.icon.SetTexture("QuestTrackerAvatars", "vindexis");
				}
				else if (NetworkScript.player.vessel.fractionId == 2)
				{
					this.icon.SetTexture("QuestTrackerAvatars", "regia");
				}
				this.icon.boundries = new Rect(0f, 0f, 117f, 117f);
			}
			if (!this.isObjectivesOnScreen)
			{
				this.ShowObjectivesLabels(-237f);
			}
			else
			{
				this.progressIndicator.X = 325f;
				this.objectivesBackground.X = 88f;
				this.holderWindow.boundries.set_width(364f);
				this.ShowObjectivesLabels(0f);
			}
			this.ShowNotification();
		}

		private void AnimationFinishQuestStageOne(object prm)
		{
			float single = 41.6666641f;
			if (this.progressIndicator.X > 63f)
			{
				float _deltaTime = Time.get_deltaTime() * single;
				GuiTexture x = this.progressIndicator;
				x.X = x.X - _deltaTime;
				GuiTexture guiTexture = this.objectivesBackground;
				guiTexture.X = guiTexture.X - _deltaTime;
			}
			else
			{
				this.progressIndicator.X = 63f;
				this.objectivesBackground.X = -174f;
				this.holderWindow.preDrawHandler = new Action<object>(this, QuestTrackerWindow.QuestTrackerItem.AnimationFinishQuestStageTwo);
			}
		}

		private void AnimationFinishQuestStageTwo(object prm)
		{
			float _width = this.holderWindow.boundries.get_width() / 0.9f;
			if (this.holderWindow.boundries.get_x() > -this.holderWindow.boundries.get_width())
			{
				float _deltaTime = Time.get_deltaTime() * _width;
				ref Rect rectPointer = ref this.holderWindow.boundries;
				rectPointer.set_x(rectPointer.get_x() - _deltaTime);
			}
			else
			{
				this.holderWindow.boundries.set_x(-this.holderWindow.boundries.get_width());
				this.holderWindow.preDrawHandler = null;
				this.state = QuestTrackerWindow.QuestTrackerItemState.Removed;
				if (this.OnFinishQuestAnimationComplete != null)
				{
					this.OnFinishQuestAnimationComplete.Invoke(this.quest.id);
				}
			}
		}

		private void AnimationHideObjectivesInfo(object prm)
		{
			float single = 338.571442f;
			if (this.progressIndicator.X >= 88f)
			{
				float _deltaTime = Time.get_deltaTime() * single;
				GuiTexture x = this.objectivesBackground;
				x.X = x.X - _deltaTime;
				GuiTexture guiTexture = this.progressIndicator;
				guiTexture.X = guiTexture.X - _deltaTime;
				ref Rect rectPointer = ref this.holderWindow.boundries;
				rectPointer.set_width(rectPointer.get_width() - _deltaTime);
				foreach (GuiElement objectiveInfoLabel in this.objectiveInfoLabels)
				{
					GuiElement guiElement = objectiveInfoLabel;
					guiElement.X = guiElement.X - _deltaTime;
				}
			}
			else
			{
				this.progressIndicator.X = 88f;
				this.objectivesBackground.X = -149f;
				this.holderWindow.boundries.set_width(128f);
				this.HideObjectivesLabels();
				if (this.state != QuestTrackerWindow.QuestTrackerItemState.HideAnimationInProgress)
				{
					this.holderWindow.preDrawHandler = null;
					this.state = QuestTrackerWindow.QuestTrackerItemState.ObjectivesOutOfScreen;
				}
				else
				{
					this.holderWindow.preDrawHandler = new Action<object>(this, QuestTrackerWindow.QuestTrackerItem.AnimationFinishQuestStageOne);
				}
			}
		}

		private void AnimationShowObjectivesInfo(object prm)
		{
			float single = 338.571442f;
			if (this.progressIndicator.X < 325f)
			{
				float _deltaTime = Time.get_deltaTime() * single;
				GuiTexture x = this.objectivesBackground;
				x.X = x.X + _deltaTime;
				GuiTexture guiTexture = this.progressIndicator;
				guiTexture.X = guiTexture.X + _deltaTime;
				ref Rect rectPointer = ref this.holderWindow.boundries;
				rectPointer.set_width(rectPointer.get_width() + _deltaTime);
				foreach (GuiElement objectiveInfoLabel in this.objectiveInfoLabels)
				{
					GuiElement guiElement = objectiveInfoLabel;
					guiElement.X = guiElement.X + _deltaTime;
				}
			}
			else
			{
				this.progressIndicator.X = 325f;
				this.objectivesBackground.X = 88f;
				this.holderWindow.boundries.set_width(364f);
				this.holderWindow.preDrawHandler = null;
				this.state = QuestTrackerWindow.QuestTrackerItemState.ObjectivesOnScreen;
				this.ShowObjectivesLabels(0f);
			}
		}

		private void AnimationStartQuestStageOne(object prm)
		{
			float _width = this.holderWindow.boundries.get_width() / 0.9f;
			if (this.holderWindow.boundries.get_x() < 0f)
			{
				float _deltaTime = Time.get_deltaTime() * _width;
				ref Rect rectPointer = ref this.holderWindow.boundries;
				rectPointer.set_x(rectPointer.get_x() + _deltaTime);
			}
			else
			{
				this.holderWindow.boundries.set_x(0f);
				this.holderWindow.preDrawHandler = new Action<object>(this, QuestTrackerWindow.QuestTrackerItem.AnimationStartQuestStageTwo);
			}
		}

		private void AnimationStartQuestStageTwo(object prm)
		{
			float single = 41.6666641f;
			if (this.progressIndicator.X < 88f)
			{
				float _deltaTime = Time.get_deltaTime() * single;
				GuiTexture x = this.progressIndicator;
				x.X = x.X + _deltaTime;
				GuiTexture guiTexture = this.objectivesBackground;
				guiTexture.X = guiTexture.X + _deltaTime;
			}
			else
			{
				this.progressIndicator.X = 88f;
				this.objectivesBackground.X = -149f;
				this.holderWindow.preDrawHandler = null;
				this.state = QuestTrackerWindow.QuestTrackerItemState.NotificationNewQuest;
				this.isObjectivesOnScreen = false;
				this.ShowNotification();
				if (this.isGlobalShowObjectivesOn)
				{
					this.isObjectivesOnScreen = true;
					this.ShowObjectivesLabels(this.progressIndicator.X - 325f);
					this.holderWindow.preDrawHandler = new Action<object>(this, QuestTrackerWindow.QuestTrackerItem.AnimationShowObjectivesInfo);
				}
			}
		}

		public void FinishQuest()
		{
			this.state = QuestTrackerWindow.QuestTrackerItemState.HideAnimationInProgress;
			if (!this.isObjectivesOnScreen)
			{
				this.holderWindow.preDrawHandler = new Action<object>(this, QuestTrackerWindow.QuestTrackerItem.AnimationFinishQuestStageOne);
			}
			else
			{
				this.holderWindow.preDrawHandler = new Action<object>(this, QuestTrackerWindow.QuestTrackerItem.AnimationHideObjectivesInfo);
			}
		}

		public void GetOutOfScreen()
		{
			if (this.holderWindow == null)
			{
				return;
			}
			this.holderWindow.boundries.set_x(-this.holderWindow.boundries.get_width());
		}

		public void HideObjectiveInfo()
		{
			if (!this.isObjectivesOnScreen)
			{
				return;
			}
			this.isObjectivesOnScreen = false;
			if (this.holderWindow == null)
			{
				return;
			}
			this.holderWindow.preDrawHandler = new Action<object>(this, QuestTrackerWindow.QuestTrackerItem.AnimationHideObjectivesInfo);
		}

		private void HideObjectivesLabels()
		{
			foreach (GuiElement objectiveInfoLabel in this.objectiveInfoLabels)
			{
				this.holderWindow.RemoveGuiElement(objectiveInfoLabel);
			}
			this.objectiveInfoLabels.Clear();
		}

		private void OnQuestAvatarHovered()
		{
			if (this.isObjectivesOnScreen)
			{
				return;
			}
			if (this.state == QuestTrackerWindow.QuestTrackerItemState.HideAnimationInProgress || this.state == QuestTrackerWindow.QuestTrackerItemState.ShowAnimationInProgress)
			{
				return;
			}
			Vector3 _mousePosition = Input.get_mousePosition();
			float _height = (float)Screen.get_height() - _mousePosition.y;
			float single = _mousePosition.x;
			bool flag = this.holderWindow.boundries.Contains(new Vector2(single, _height));
			if (this.lastState == flag)
			{
				return;
			}
			this.lastState = flag;
			if (!this.lastState)
			{
				this.holderWindow.preDrawHandler = new Action<object>(this, QuestTrackerWindow.QuestTrackerItem.AnimationHideObjectivesInfo);
			}
			else
			{
				this.ShowObjectivesLabels(this.progressIndicator.X - 325f);
				this.holderWindow.preDrawHandler = new Action<object>(this, QuestTrackerWindow.QuestTrackerItem.AnimationShowObjectivesInfo);
			}
		}

		public void OpenQuestInfoWindow(object prm)
		{
			if (this.state != QuestTrackerWindow.QuestTrackerItemState.HideAnimationInProgress && this.state != QuestTrackerWindow.QuestTrackerItemState.ShowAnimationInProgress)
			{
				this.RemoveNotification();
				if (QuestInfoWindow.IsOnScreen && this.quest.id == QuestInfoWindow.SelectedQuestId)
				{
					QuestInfoWindow.Close();
					return;
				}
				QuestInfoWindow.Create(this.quest, this.isObjectivesOnScreen, true);
				return;
			}
			Debug.Log(string.Concat(new object[] { "OpenQuestInfoWindow ", this.quest.id, " state ", this.state }));
		}

		public void PopulateBringToObjectives()
		{
			if (this.holderWindow == null)
			{
				return;
			}
			foreach (NewQuestObjective objective in this.quest.objectives)
			{
				if (objective.type != 36 || this.progressIndicator.X - 325f <= -237f)
				{
					continue;
				}
				this.ShowObjectivesLabels(this.progressIndicator.X - 325f);
			}
		}

		public void Poulate(QuestEngineEnum questEvent, int eventObjectId)
		{
			QuestTrackerWindow.QuestTrackerItem.<Poulate>c__AnonStorey63 variable = null;
			if (questEvent == 3 && this.quest.id == eventObjectId)
			{
				this.state = QuestTrackerWindow.QuestTrackerItemState.NotificationFinishQuest;
			}
			if (questEvent == 9 && Enumerable.FirstOrDefault<NewQuestObjective>(Enumerable.Where<NewQuestObjective>(this.quest.objectives, new Func<NewQuestObjective, bool>(variable, (NewQuestObjective t) => t.id == this.eventObjectId))) != null)
			{
				this.state = QuestTrackerWindow.QuestTrackerItemState.NotificationNewObjective;
			}
			if (this.holderWindow == null)
			{
				return;
			}
			QuestEngineEnum questEngineEnum = questEvent;
			switch (questEngineEnum)
			{
				case 1:
				{
					if (Enumerable.FirstOrDefault<NewQuestObjective>(Enumerable.Where<NewQuestObjective>(this.quest.objectives, new Func<NewQuestObjective, bool>(variable, (NewQuestObjective t) => t.id == this.eventObjectId))) == null)
					{
						return;
					}
					if (this.progressIndicator.X - 325f > -237f)
					{
						this.ShowObjectivesLabels(this.progressIndicator.X - 325f);
					}
					break;
				}
				case 2:
				{
					if (Enumerable.FirstOrDefault<NewQuestObjective>(Enumerable.Where<NewQuestObjective>(this.quest.objectives, new Func<NewQuestObjective, bool>(variable, (NewQuestObjective t) => t.id == this.eventObjectId))) == null)
					{
						return;
					}
					this.UpdateProgressIndicator();
					break;
				}
				case 3:
				{
					if (this.quest.id != eventObjectId)
					{
						return;
					}
					this.state = QuestTrackerWindow.QuestTrackerItemState.NotificationFinishQuest;
					this.ShowNotification();
					break;
				}
				default:
				{
					if (questEngineEnum == 9)
					{
						if (Enumerable.FirstOrDefault<NewQuestObjective>(Enumerable.Where<NewQuestObjective>(this.quest.objectives, new Func<NewQuestObjective, bool>(variable, (NewQuestObjective t) => t.id == this.eventObjectId))) == null)
						{
							return;
						}
						this.state = QuestTrackerWindow.QuestTrackerItemState.NotificationNewObjective;
						this.ShowNotification();
						break;
					}
					else
					{
						Debug.Log(string.Concat("QuestTrackerItem.Populate with event: ", questEvent));
						break;
					}
				}
			}
		}

		public void RemoveFromScreen()
		{
			if (this.holderWindow != null)
			{
				if (this.state == QuestTrackerWindow.QuestTrackerItemState.HideAnimationInProgress)
				{
					this.state = QuestTrackerWindow.QuestTrackerItemState.Removed;
				}
				AndromedaGui.gui.RemoveWindow(this.holderWindow.handler);
				this.holderWindow = null;
				if (this.state == QuestTrackerWindow.QuestTrackerItemState.ShowAnimationInProgress)
				{
					if (this.isGlobalShowObjectivesOn)
					{
						this.isObjectivesOnScreen = this.isGlobalShowObjectivesOn;
					}
					this.state = QuestTrackerWindow.QuestTrackerItemState.NotificationNewQuest;
				}
			}
		}

		private void RemoveNotification()
		{
			if (this.notificationAnimation != null)
			{
				this.holderWindow.RemoveGuiElement(this.notificationAnimation);
				this.notificationAnimation = null;
			}
			if (this.notificationIcon != null)
			{
				this.holderWindow.RemoveGuiElement(this.notificationIcon);
				this.notificationIcon = null;
			}
			if (this.notificationBackground != null)
			{
				this.holderWindow.RemoveGuiElement(this.notificationBackground);
				this.notificationBackground = null;
			}
			if (!this.isObjectivesOnScreen)
			{
				this.state = QuestTrackerWindow.QuestTrackerItemState.ObjectivesOutOfScreen;
			}
			else
			{
				this.state = QuestTrackerWindow.QuestTrackerItemState.ObjectivesOnScreen;
			}
		}

		private void ShowNotification()
		{
			if (this.state == QuestTrackerWindow.QuestTrackerItemState.NotificationFinishQuest)
			{
				this.RemoveNotification();
				this.notificationAnimation = new GuiTextureAnimated()
				{
					isHoverAware = true
				};
				this.notificationAnimation.Init("QuestNotificationGreen", "QuestNotificationGreen", "QuestNotificationGreen/green_01");
				this.notificationAnimation.X = 12f;
				this.notificationAnimation.Y = 10f;
				this.notificationAnimation.isHoverAware = false;
				this.notificationAnimation.rotationTime = 2f;
				this.notificationBackground = new GuiTexture();
				this.notificationBackground.SetTexture("QuestTracker", "notoficatoiCoverGreen");
				this.notificationBackground.X = 12f;
				this.notificationBackground.Y = 10f;
				this.notificationBackground.isHoverAware = false;
				this.notificationIcon = new GuiTexture();
				this.notificationIcon.SetTexture("QuestTracker", "notificationGreen");
				this.notificationIcon.X = 12f;
				this.notificationIcon.Y = 10f;
				this.notificationIcon.isHoverAware = false;
				this.holderWindow.AddGuiElement(this.notificationBackground);
				this.holderWindow.AddGuiElement(this.notificationAnimation);
				this.holderWindow.AddGuiElement(this.notificationIcon);
			}
			else if (this.state == QuestTrackerWindow.QuestTrackerItemState.NotificationNewQuest || this.state == QuestTrackerWindow.QuestTrackerItemState.NotificationNewObjective)
			{
				this.RemoveNotification();
				this.notificationAnimation = new GuiTextureAnimated()
				{
					isHoverAware = true
				};
				this.notificationAnimation.Init("QuestNotificationOrange", "QuestNotificationOrange", "QuestNotificationOrange/orange_01");
				this.notificationAnimation.X = 12f;
				this.notificationAnimation.Y = 10f;
				this.notificationAnimation.isHoverAware = false;
				this.notificationAnimation.rotationTime = 2f;
				this.notificationBackground = new GuiTexture();
				this.notificationBackground.SetTexture("QuestTracker", "notoficatoiCoverOrange");
				this.notificationBackground.X = 12f;
				this.notificationBackground.Y = 10f;
				this.notificationBackground.isHoverAware = false;
				this.notificationIcon = new GuiTexture();
				this.notificationIcon.SetTexture("QuestTracker", "notificationOrange");
				this.notificationIcon.X = 12f;
				this.notificationIcon.Y = 10f;
				this.notificationIcon.isHoverAware = false;
				this.holderWindow.AddGuiElement(this.notificationBackground);
				this.holderWindow.AddGuiElement(this.notificationAnimation);
				this.holderWindow.AddGuiElement(this.notificationIcon);
			}
		}

		public void ShowObjectiveInfo()
		{
			if (this.isObjectivesOnScreen)
			{
				return;
			}
			this.isObjectivesOnScreen = true;
			if (this.holderWindow == null)
			{
				return;
			}
			this.ShowObjectivesLabels(this.progressIndicator.X - 325f);
			this.holderWindow.preDrawHandler = new Action<object>(this, QuestTrackerWindow.QuestTrackerItem.AnimationShowObjectivesInfo);
		}

		private void ShowObjectivesLabels(float offsetX)
		{
			QuestTrackerWindow.QuestTrackerItem.<ShowObjectivesLabels>c__AnonStorey64 variable = null;
			if (this.objectiveInfoLabels == null)
			{
				this.objectiveInfoLabels = new List<GuiElement>();
			}
			else
			{
				this.HideObjectivesLabels();
			}
			string str = (this.quest.type != 2 ? StaticData.Translate(string.Format("key_quest_{0}_name", this.quest.id)) : StaticData.Translate("key_quest_daily_mission_name"));
			GuiLabel guiLabel = new GuiLabel()
			{
				boundries = new Rect(117f, 11f, 215f, 18f),
				text = (str.get_Length() <= 26 ? str : string.Concat(str.Substring(0, 23), "...")),
				Font = GuiLabel.FontBold,
				FontSize = 16,
				Clipping = 1,
				TextColor = Color.get_white()
			};
			this.holderWindow.AddGuiElement(guiLabel);
			this.objectiveInfoLabels.Add(guiLabel);
			float single = 35f;
			foreach (NewQuestObjective objective in this.quest.objectives)
			{
				string empty = string.Empty;
				empty = string.Concat("- ", objective.GetObjectiveShortDescription());
				GuiLabel _gray = new GuiLabel()
				{
					boundries = new Rect(117f, single, 170f, 16f),
					text = (empty.get_Length() <= 26 ? empty : string.Concat(empty.Substring(0, 23), "...")),
					FontSize = 14,
					Clipping = 1
				};
				this.holderWindow.AddGuiElement(_gray);
				this.objectiveInfoLabels.Add(_gray);
				if (objective.parentObjectiveId != 0)
				{
					NewQuestObjective newQuestObjective = Enumerable.FirstOrDefault<NewQuestObjective>(Enumerable.Where<NewQuestObjective>(this.quest.objectives, new Func<NewQuestObjective, bool>(variable, (NewQuestObjective o) => o.id == this.obj.parentObjectiveId)));
					if (newQuestObjective == null)
					{
						Debug.Log("parentObjective==null");
					}
					if (!newQuestObjective.IsComplete(NetworkScript.player))
					{
						_gray.TextColor = Color.get_gray();
						_gray.text = StaticData.Translate("key_quest_info_window_locked_objective");
						continue;
					}
				}
				GuiLabel guiLabel1 = new GuiLabel()
				{
					boundries = new Rect(288f, single, 50f, 15f)
				};
				int amountAt = NetworkScript.player.playerBelongings.playerObjectives.GetAmountAt(objective.id);
				guiLabel1.text = string.Format("{0}/{1}", amountAt.ToString("##,##0"), objective.targetAmount.ToString("##,##0"));
				guiLabel1.Alignment = 5;
				guiLabel1.FontSize = 12;
				guiLabel1.Clipping = 1;
				guiLabel1.WordWrap = false;
				if (!objective.IsComplete(NetworkScript.player))
				{
					if (objective.type == 36)
					{
						if (!PlayerItems.IsMineral((ushort)objective.targetParam1))
						{
							GuiLabel str1 = guiLabel1;
							IEnumerable<SlotItem> enumerable = Enumerable.Where<SlotItem>(NetworkScript.player.playerBelongings.playerItems.slotItems, new Func<SlotItem, bool>(variable, (SlotItem t) => (t.get_ItemType() != this.obj.targetParam1 ? false : t.get_SlotType() == 1)));
							if (QuestTrackerWindow.QuestTrackerItem.<>f__am$cache13 == null)
							{
								QuestTrackerWindow.QuestTrackerItem.<>f__am$cache13 = new Func<SlotItem, int>(null, (SlotItem s) => s.get_Amount());
							}
							int num = Enumerable.Sum(Enumerable.Select<SlotItem, int>(enumerable, QuestTrackerWindow.QuestTrackerItem.<>f__am$cache13));
							str1.text = num.ToString("##,##0");
						}
						else
						{
							GuiLabel str2 = guiLabel1;
							IEnumerable<SlotItem> enumerable1 = Enumerable.Where<SlotItem>(NetworkScript.player.playerBelongings.playerItems.slotItems, new Func<SlotItem, bool>(variable, (SlotItem t) => (t.get_ItemType() != this.obj.targetParam1 ? false : t.get_SlotType() == 3)));
							if (QuestTrackerWindow.QuestTrackerItem.<>f__am$cache12 == null)
							{
								QuestTrackerWindow.QuestTrackerItem.<>f__am$cache12 = new Func<SlotItem, int>(null, (SlotItem s) => s.get_Amount());
							}
							int num1 = Enumerable.Sum(Enumerable.Select<SlotItem, int>(enumerable1, QuestTrackerWindow.QuestTrackerItem.<>f__am$cache12));
							str2.text = num1.ToString("##,##0");
						}
					}
					if (objective.targetAmount > 1)
					{
						this.holderWindow.AddGuiElement(guiLabel1);
						this.objectiveInfoLabels.Add(guiLabel1);
					}
				}
				else
				{
					_gray.TextColor = GuiNewStyleBar.orangeColor;
					if (objective.targetAmount != 1)
					{
						guiLabel1.TextColor = GuiNewStyleBar.orangeColor;
						guiLabel1.text = string.Format("{0}/{1}", objective.targetAmount.ToString("##,##0"), objective.targetAmount.ToString("##,##0"));
						this.holderWindow.AddGuiElement(guiLabel1);
						this.objectiveInfoLabels.Add(guiLabel1);
					}
					else
					{
						GuiTexture guiTexture = new GuiTexture();
						guiTexture.SetTexture("QuestTracker", "isComplete");
						guiTexture.boundries = new Rect(322f, single + 2f, 16f, 14f);
						this.holderWindow.AddGuiElement(guiTexture);
						this.objectiveInfoLabels.Add(guiTexture);
					}
				}
				single = single + 20f;
			}
			foreach (GuiElement objectiveInfoLabel in this.objectiveInfoLabels)
			{
				GuiElement x = objectiveInfoLabel;
				x.X = x.X + offsetX;
			}
			this.holderWindow.RemoveGuiElement(this.background);
			this.holderWindow.AddGuiElement(this.background);
			this.holderWindow.RemoveGuiElement(this.icon);
			this.holderWindow.AddGuiElement(this.icon);
			this.holderWindow.RemoveGuiElement(this.openInfoWindowButton);
			this.holderWindow.AddGuiElement(this.openInfoWindowButton);
			if (this.notificationBackground != null)
			{
				this.holderWindow.RemoveGuiElement(this.notificationBackground);
				this.holderWindow.RemoveGuiElement(this.notificationAnimation);
				this.holderWindow.RemoveGuiElement(this.notificationIcon);
				this.holderWindow.AddGuiElement(this.notificationBackground);
				this.holderWindow.AddGuiElement(this.notificationAnimation);
				this.holderWindow.AddGuiElement(this.notificationIcon);
			}
		}

		public void StartHammerEffect()
		{
			if (this.holderWindow == null)
			{
				return;
			}
			this.holderWindow.timeHammerFx = 0.5f;
			this.holderWindow.StartHammerEffect(0f, this.holderWindow.boundries.get_y());
		}

		public void StartQuest(int index, Action<int> OnRemoveCallback, bool globalObjectivesState)
		{
			this.AddToScreen(index, OnRemoveCallback);
			GuiTexture x = this.objectivesBackground;
			x.X = x.X - 25f;
			GuiTexture guiTexture = this.progressIndicator;
			guiTexture.X = guiTexture.X - 25f;
			this.state = QuestTrackerWindow.QuestTrackerItemState.ShowAnimationInProgress;
			this.holderWindow.boundries.set_x(-this.holderWindow.boundries.get_width() - 50f);
			this.holderWindow.preDrawHandler = new Action<object>(this, QuestTrackerWindow.QuestTrackerItem.AnimationStartQuestStageOne);
			this.isGlobalShowObjectivesOn = globalObjectivesState;
		}

		public void ToogleObjectivesInfo()
		{
			if (!this.isObjectivesOnScreen)
			{
				this.ShowObjectivesLabels(this.progressIndicator.X - 325f);
				this.holderWindow.preDrawHandler = new Action<object>(this, QuestTrackerWindow.QuestTrackerItem.AnimationShowObjectivesInfo);
			}
			else
			{
				this.holderWindow.preDrawHandler = new Action<object>(this, QuestTrackerWindow.QuestTrackerItem.AnimationHideObjectivesInfo);
			}
			this.isObjectivesOnScreen = !this.isObjectivesOnScreen;
		}

		private void UpdateProgressIndicator()
		{
			this.totalObjectiveCount = Enumerable.Count<NewQuestObjective>(this.quest.objectives);
			this.progress = 0;
			foreach (NewQuestObjective objective in this.quest.objectives)
			{
				if (!objective.IsComplete(NetworkScript.player))
				{
					continue;
				}
				QuestTrackerWindow.QuestTrackerItem questTrackerItem = this;
				questTrackerItem.progress = questTrackerItem.progress + 1;
			}
			this.progressIndicator.SetTexture("QuestTracker", string.Format("progressIdicator_{0}_{1}", this.totalObjectiveCount, this.progress));
		}
	}

	private enum QuestTrackerItemState
	{
		None,
		ShowAnimationInProgress,
		ObjectivesOnScreen,
		ObjectivesOutOfScreen,
		ObjectivesHideInProgress,
		ObjectivesShowInProgress,
		NotificationNewQuest,
		NotificationNewObjective,
		NotificationFinishQuest,
		HideAnimationInProgress,
		Removed
	}
}