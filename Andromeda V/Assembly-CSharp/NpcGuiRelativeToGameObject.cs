using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NpcGuiRelativeToGameObject : MonoBehaviour
{
	private const float WINDOW_WIDTH = 260f;

	private Vector3 screenPos;

	public NpcObjectPhysics npc;

	private AudioSource npcExpression;

	private GuiButtonFixed backButton;

	private GuiButtonFixed talkToNpcButton;

	private bool fullNpcInfoOnScreen;

	private GuiWindow bigInfoWindow;

	private GuiWindow npcWindow;

	private GuiWindow spaceWindow;

	public NpcGuiRelativeToGameObject()
	{
	}

	private void HideFullNpcInfo()
	{
		if (this.bigInfoWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.bigInfoWindow.handler);
			this.bigInfoWindow = null;
			this.fullNpcInfoOnScreen = false;
		}
	}

	private void HideMainMenu()
	{
		if (this.npcWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.npcWindow.handler);
			this.npcWindow = null;
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
		this.HideFullNpcInfo();
		this.HideMainMenu();
	}

	private void OnOpenQuestkBtnClicked(EventHandlerParam prm)
	{
		if (!NetworkScript.player.shipScript.isInControl)
		{
			return;
		}
		try
		{
			QuestTrackerWindow.OpenQuestInfo((int)prm.customData);
		}
		catch (Exception exception)
		{
			Debug.LogError(string.Format("OnTalkBtnClicked() {0}", exception));
		}
	}

	private void OnPowerUpsShopClick(object prm)
	{
		if (AndromedaGui.mainWnd != null)
		{
			PowerUpsWindow.npc = this.npc;
			MainScreenWindow mainScreenWindow = AndromedaGui.mainWnd;
			EventHandlerParam eventHandlerParam = new EventHandlerParam()
			{
				customData = (byte)32
			};
			mainScreenWindow.OnWindowBtnClicked(eventHandlerParam);
		}
	}

	private void OnTalkBtnClicked(object prm)
	{
		// 
		// Current member / type: System.Void NpcGuiRelativeToGameObject::OnTalkBtnClicked(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnTalkBtnClicked(System.Object)
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

	public void Populate()
	{
		if (this.npcWindow != null)
		{
			this.HideMainMenu();
			this.ShowMainMenu();
		}
	}

	private void SetBtnActionToBack()
	{
		this.backButton.Clicked = new Action<EventHandlerParam>(this, NpcGuiRelativeToGameObject.OnBackBtnClicked);
		if (NetworkScript.player != null && NetworkScript.player.shipScript)
		{
			NetworkScript.player.shipScript.popUpAction = new Action<object>(this, NpcGuiRelativeToGameObject.OnBackBtnClicked);
		}
	}

	private void ShowFullNpcInfo()
	{
		if (this.bigInfoWindow != null)
		{
			return;
		}
		this.fullNpcInfoOnScreen = true;
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
			text = StaticData.Translate(this.npc.npcName),
			Alignment = 4,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		this.bigInfoWindow.AddGuiElement(guiLabel);
		GuiAnimatedText guiAnimatedText = new GuiAnimatedText(StaticData.Translate(this.npc.description), new Action(this, NpcGuiRelativeToGameObject.SetBtnActionToBack))
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
		NpcGuiRelativeToGameObject.<ShowMainMenu>c__AnonStorey54 variable = null;
		NpcGuiRelativeToGameObject.<ShowMainMenu>c__AnonStorey55 variable1 = null;
		if (this.npcWindow != null)
		{
			return;
		}
		List<NewQuest> list = new List<NewQuest>();
		IEnumerator<int> enumerator = NetworkScript.player.playerBelongings.playerQuests.get_Keys().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				enumerator.get_Current();
				NewQuest newQuest = Enumerable.FirstOrDefault<NewQuest>(Enumerable.Where<NewQuest>(Enumerable.Union<NewQuest>(StaticData.allQuests, StaticData.allDailyQuests), new Func<NewQuest, bool>(variable, (NewQuest t) => t.id == this.questId)));
				if (newQuest == null)
				{
					continue;
				}
				List<NewQuestObjective>.Enumerator enumerator1 = newQuest.objectives.GetEnumerator();
				try
				{
					while (enumerator1.MoveNext())
					{
						NewQuestObjective current = enumerator1.get_Current();
						if (current.type != 36 || current.IsComplete(NetworkScript.player) || current.targetParam2 != this.npc.npcKey || current.parentObjectiveId != 0 && !Enumerable.First<NewQuestObjective>(Enumerable.Where<NewQuestObjective>(newQuest.objectives, new Func<NewQuestObjective, bool>(variable1, (NewQuestObjective t) => t.id == this.objective.parentObjectiveId))).IsComplete(NetworkScript.player))
						{
							continue;
						}
						list.Add(newQuest);
						break;
					}
				}
				finally
				{
					enumerator1.Dispose();
				}
			}
		}
		finally
		{
			if (enumerator == null)
			{
			}
			enumerator.Dispose();
		}
		this.npcWindow = new GuiWindow();
		if (!this.npc.isPowerUpSeller)
		{
			this.npcWindow.boundries = new Rect(0f, 0f, (float)(60 + list.get_Count() * 45), 60f);
		}
		else
		{
			this.npcWindow.boundries = new Rect(0f, 0f, (float)(130 + list.get_Count() * 45), 60f);
		}
		this.npcWindow.isHidden = false;
		this.npcWindow.zOrder = 50;
		AndromedaGui.gui.AddWindow(this.npcWindow);
		float single = 0f;
		if (this.npc.isPowerUpSeller)
		{
			GuiTexture guiTexture = new GuiTexture();
			guiTexture.SetTexture("ActionButtons", "buttonsLink");
			guiTexture.X = 45f;
			guiTexture.Y = 4f;
			this.npcWindow.AddGuiElement(guiTexture);
			GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
			guiButtonFixed.SetTexture("ActionButtons", "btnPowerUps");
			guiButtonFixed.Caption = string.Empty;
			guiButtonFixed.X = 0f;
			guiButtonFixed.Y = 0f;
			guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, NpcGuiRelativeToGameObject.OnPowerUpsShopClick);
			this.npcWindow.AddGuiElement(guiButtonFixed);
			single = 70f;
		}
		this.talkToNpcButton = new GuiButtonFixed();
		this.talkToNpcButton.SetTexture("ActionButtons", "btnTalkToNpc");
		this.talkToNpcButton.Caption = string.Empty;
		this.talkToNpcButton.X = single;
		this.talkToNpcButton.Y = 0f;
		this.talkToNpcButton.Clicked = new Action<EventHandlerParam>(this, NpcGuiRelativeToGameObject.OnTalkBtnClicked);
		this.npcWindow.AddGuiElement(this.talkToNpcButton);
		if (AndromedaGui.mainWnd.kb != null)
		{
			GuiLabel guiLabel = new GuiLabel()
			{
				boundries = new Rect(this.talkToNpcButton.X + 10f, 35f, 40f, 15f),
				Alignment = 6,
				Font = GuiLabel.FontBold,
				FontSize = 14,
				text = AndromedaGui.mainWnd.kb.GetCommandKeyCodeShort(KeyboardCommand.UseKey)
			};
			this.npcWindow.AddGuiElement(guiLabel);
		}
		if (!this.fullNpcInfoOnScreen)
		{
			this.talkToNpcButton.Clicked = new Action<EventHandlerParam>(this, NpcGuiRelativeToGameObject.OnTalkBtnClicked);
		}
		else
		{
			this.talkToNpcButton.Clicked = new Action<EventHandlerParam>(this, NpcGuiRelativeToGameObject.OnBackBtnClicked);
		}
		if (NetworkScript.player != null && NetworkScript.player.shipScript)
		{
			NetworkScript.player.shipScript.popUpAction = new Action<object>(this, NpcGuiRelativeToGameObject.OnTalkBtnClicked);
		}
		int num = 0;
		foreach (NewQuest newQuest1 in list)
		{
			string str = (newQuest1.type != 2 ? string.Format("key_quest_{0}_name", newQuest1.id) : "key_quest_daily_mission_name");
			GuiButtonFixed rect = new GuiButtonFixed();
			rect.SetTexture("ActionButtons", "btnNpcQuestNml");
			rect.boundries = new Rect(single + 65f + (float)(num * 45), 0f, 40f, 40f);
			rect.Caption = string.Empty;
			EventHandlerParam eventHandlerParam = new EventHandlerParam()
			{
				customData = string.Concat(string.Format("({0}) ", newQuest1.level), StaticData.Translate(str)),
				customData2 = rect
			};
			rect.tooltipWindowParam = eventHandlerParam;
			rect.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			rect.eventHandlerParam.customData = newQuest1.id;
			rect.Clicked = new Action<EventHandlerParam>(this, NpcGuiRelativeToGameObject.OnOpenQuestkBtnClicked);
			this.npcWindow.AddGuiElement(rect);
			num++;
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
		guiTexture.SetTexture("MinimapWindow", "npc_icon");
		guiTexture.X = 0f;
		guiTexture.Y = 0f;
		this.spaceWindow.AddGuiElement(guiTexture);
		string str = StaticData.Translate(this.npc.npcName);
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
		if (!this.npc.IsObjectInRange(NetworkScript.player.vessel))
		{
			this.HideMainMenu();
			this.HideFullNpcInfo();
		}
		else
		{
			this.ShowMainMenu();
			this.npcWindow.boundries.set_x((float)(Screen.get_width() / 2 - 195));
			this.npcWindow.boundries.set_y((float)(Screen.get_height() - 160));
			if (!this.fullNpcInfoOnScreen)
			{
				this.HideFullNpcInfo();
			}
			else
			{
				this.ShowFullNpcInfo();
				this.bigInfoWindow.boundries.set_x((float)(Screen.get_width() / 2 - 195));
				this.bigInfoWindow.boundries.set_y((float)(Screen.get_height() - 260));
			}
		}
	}
}