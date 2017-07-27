using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class __SkillsTree : GuiWindow
{
	private static __SkillsTree.SkillTreeItem[][] skillTrees;

	private static int investedSkillPoints;

	private static SortedList<ushort, __SkillsTree.SkillItem> playerSkillsOnScreen;

	public static GuiWindow window;

	private static int currentTab;

	private static List<GuiElement> forDelete;

	private static GuiButtonResizeable btnRetrain;

	private static GuiButtonResizeable btnSave;

	private static GuiButtonResizeable btnDiscard;

	private static GuiDialog dlgConfirmRetrain;

	private static GuiDialog dlgConfirmSave;

	private static ushort lastHoveredTalentType;

	private static GuiLabel infoLabel1;

	private static GuiLabel infoLabel2;

	private static GuiLabel infoLabel3;

	static __SkillsTree()
	{
		__SkillsTree.SkillTreeItem[][] skillTreeItemArray = new __SkillsTree.SkillTreeItem[5][];
		__SkillsTree.SkillTreeItem[] skillTreeItemArray1 = new __SkillsTree.SkillTreeItem[10];
		__SkillsTree.SkillTreeItem skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsSunderArmor,
			x = 308,
			y = 116
		};
		skillTreeItemArray1[0] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsDefiance,
			x = 582,
			y = 116
		};
		skillTreeItemArray1[1] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsTaunt,
			x = 308,
			y = 189
		};
		skillTreeItemArray1[2] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsRocketBarrage,
			x = 582,
			y = 189
		};
		skillTreeItemArray1[3] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsAdvancedCorpus,
			x = 308,
			y = 259
		};
		skillTreeItemArray1[4] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsFocusFire,
			x = 582,
			y = 259
		};
		skillTreeItemArray1[5] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsAdvancedShield,
			x = 308,
			y = 329
		};
		skillTreeItemArray1[6] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsUnstoppable,
			x = 582,
			y = 329
		};
		skillTreeItemArray1[7] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsPowerCut,
			x = 441,
			y = 399
		};
		skillTreeItemArray1[8] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsShieldFortress,
			x = 441,
			y = 469
		};
		skillTreeItemArray1[9] = skillTreeItem;
		skillTreeItemArray[0] = skillTreeItemArray1;
		__SkillsTree.SkillTreeItem[] skillTreeItemArray2 = new __SkillsTree.SkillTreeItem[10];
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsLaserDestruction,
			x = 308,
			y = 116
		};
		skillTreeItemArray2[0] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsFindWeakSpot,
			x = 582,
			y = 116
		};
		skillTreeItemArray2[1] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsStealth,
			x = 308,
			y = 189
		};
		skillTreeItemArray2[2] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsDecoy,
			x = 582,
			y = 189
		};
		skillTreeItemArray2[3] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsEngineBooster,
			x = 308,
			y = 259
		};
		skillTreeItemArray2[4] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsPowerBreak,
			x = 582,
			y = 259
		};
		skillTreeItemArray2[5] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsLightSpeed,
			x = 308,
			y = 329
		};
		skillTreeItemArray2[6] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsWeaponMastery,
			x = 582,
			y = 329
		};
		skillTreeItemArray2[7] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsForceWave,
			x = 441,
			y = 399
		};
		skillTreeItemArray2[8] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsMistShroud,
			x = 441,
			y = 469
		};
		skillTreeItemArray2[9] = skillTreeItem;
		skillTreeItemArray[1] = skillTreeItemArray2;
		__SkillsTree.SkillTreeItem[] skillTreeItemArray3 = new __SkillsTree.SkillTreeItem[10];
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsRepairingDrones,
			x = 308,
			y = 116
		};
		skillTreeItemArray3[0] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsImprovedShield,
			x = 582,
			y = 116
		};
		skillTreeItemArray3[1] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsNanoStorm,
			x = 308,
			y = 189
		};
		skillTreeItemArray3[2] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsNanoShield,
			x = 582,
			y = 189
		};
		skillTreeItemArray3[3] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsRepairField,
			x = 308,
			y = 259
		};
		skillTreeItemArray3[4] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsEmpoweredShield,
			x = 582,
			y = 259
		};
		skillTreeItemArray3[5] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsShortCircuit,
			x = 308,
			y = 329
		};
		skillTreeItemArray3[6] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsNanoTechnology,
			x = 582,
			y = 329
		};
		skillTreeItemArray3[7] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsPulseNova,
			x = 441,
			y = 399
		};
		skillTreeItemArray3[8] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsRemedy,
			x = 441,
			y = 469
		};
		skillTreeItemArray3[9] = skillTreeItem;
		skillTreeItemArray[2] = skillTreeItemArray3;
		__SkillsTree.SkillTreeItem[] skillTreeItemArray4 = new __SkillsTree.SkillTreeItem[8];
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsArchiver,
			x = 224,
			y = 129
		};
		skillTreeItemArray4[0] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsMerchant,
			x = 444,
			y = 129
		};
		skillTreeItemArray4[1] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsSteadyAim,
			x = 664,
			y = 129
		};
		skillTreeItemArray4[2] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsSwiftLearner,
			x = 302,
			y = 239
		};
		skillTreeItemArray4[3] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsAlienSpecialist,
			x = 583,
			y = 239
		};
		skillTreeItemArray4[4] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsVelocity,
			x = 302,
			y = 349
		};
		skillTreeItemArray4[5] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsBountySpecialist,
			x = 583,
			y = 349
		};
		skillTreeItemArray4[6] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsDamageReduction,
			x = 441,
			y = 459
		};
		skillTreeItemArray4[7] = skillTreeItem;
		skillTreeItemArray[3] = skillTreeItemArray4;
		__SkillsTree.SkillTreeItem[] skillTreeItemArray5 = new __SkillsTree.SkillTreeItem[9];
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsArmorBreaker,
			x = 224,
			y = 129
		};
		skillTreeItemArray5[0] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsPowerControl,
			x = 444,
			y = 129
		};
		skillTreeItemArray5[1] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsFutureTechnology,
			x = 664,
			y = 129
		};
		skillTreeItemArray5[2] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsRealSteel,
			x = 224,
			y = 239
		};
		skillTreeItemArray5[3] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsMassiveDamage,
			x = 444,
			y = 239
		};
		skillTreeItemArray5[4] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsDronePower,
			x = 664,
			y = 239
		};
		skillTreeItemArray5[5] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsRocketeer,
			x = 224,
			y = 349
		};
		skillTreeItemArray5[6] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsSpeedster,
			x = 444,
			y = 349
		};
		skillTreeItemArray5[7] = skillTreeItem;
		skillTreeItem = new __SkillsTree.SkillTreeItem()
		{
			skillType = PlayerItems.TypeTalentsRepairMaster,
			x = 664,
			y = 349
		};
		skillTreeItemArray5[8] = skillTreeItem;
		skillTreeItemArray[4] = skillTreeItemArray5;
		__SkillsTree.skillTrees = skillTreeItemArray;
		__SkillsTree.investedSkillPoints = 0;
		__SkillsTree.window = null;
		__SkillsTree.currentTab = 0;
		__SkillsTree.forDelete = new List<GuiElement>();
		__SkillsTree.lastHoveredTalentType = 65535;
	}

	public __SkillsTree()
	{
	}

	private static void AddSkillToList(List<ushort> result, SortedList<int, List<ushort>> skillsList)
	{
		int length = (int)Enumerable.ToArray<int>(skillsList.get_Keys()).Length;
		while (length > 0)
		{
			bool flag = false;
			foreach (ushort item in skillsList.get_Item(length))
			{
				if (!__SkillsTree.CanInvestSkillPoint(item))
				{
					continue;
				}
				flag = true;
				result.Add(item);
			}
			if (!flag)
			{
				length--;
			}
			else
			{
				break;
			}
		}
	}

	private static void BuyNeuron(ushort talentType)
	{
		// 
		// Current member / type: System.Void __SkillsTree::BuyNeuron(System.UInt16)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void BuyNeuron(System.UInt16)
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

	private static bool CanInvestSkillPoint(ushort skillId)
	{
		if (NetworkScript.player.playerBelongings.playerItems.GetAmountAt(PlayerItems.TypeTalentPoint) - (long)__SkillsTree.investedSkillPoints <= (long)0)
		{
			return false;
		}
		SortedList<int, List<ushort>> sortedList = null;
		TalentsInfo item = (TalentsInfo)StaticData.allTypes.get_Item(skillId);
		if (__SkillsTree.playerSkillsOnScreen.ContainsKey(skillId) && __SkillsTree.playerSkillsOnScreen.get_Item(skillId).wantedLevel > 0)
		{
			if (__SkillsTree.playerSkillsOnScreen.get_Item(skillId).wantedLevel >= item.maxLevel)
			{
				return false;
			}
		}
		else if (NetworkScript.player.playerBelongings.playerItems.GetAmountAt(skillId) >= (long)item.maxLevel)
		{
			return false;
		}
		switch (item.talentsClass)
		{
			case 1:
			{
				sortedList = PlayerItems.guardianSkills;
				break;
			}
			case 2:
			{
				sortedList = PlayerItems.destroyerSkills;
				break;
			}
			case 3:
			{
				sortedList = PlayerItems.protectorSkills;
				break;
			}
			case 4:
			{
				sortedList = PlayerItems.passiveSkills;
				break;
			}
			case 5:
			{
				sortedList = PlayerItems.amplificationSkillsOne;
				break;
			}
			case 6:
			{
				sortedList = PlayerItems.amplificationSkillsTwo;
				break;
			}
			case 7:
			{
				sortedList = PlayerItems.amplificationSkillsThree;
				break;
			}
		}
		int num = 0;
		int num1 = 0;
		IEnumerator<int> enumerator = sortedList.get_Keys().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				int current = enumerator.get_Current();
				if (!sortedList.get_Item(current).Contains(skillId))
				{
					continue;
				}
				num1 = current;
				break;
			}
		}
		finally
		{
			if (enumerator == null)
			{
			}
			enumerator.Dispose();
		}
		for (int i = 1; i < num1; i++)
		{
			foreach (ushort item1 in sortedList.get_Item(i))
			{
				int amountAt = (int)NetworkScript.player.playerBelongings.playerItems.GetAmountAt(item1);
				int num2 = (!__SkillsTree.playerSkillsOnScreen.ContainsKey(item1) ? 0 : __SkillsTree.playerSkillsOnScreen.get_Item(item1).wantedLevel);
				num = (num2 <= 0 ? num + amountAt + num2 : num + num2);
			}
		}
		return item.reqSpendSkillPoint <= num;
	}

	public static new void Clear()
	{
		foreach (GuiElement guiElement in __SkillsTree.forDelete)
		{
			__SkillsTree.window.RemoveGuiElement(guiElement);
		}
		__SkillsTree.forDelete.Clear();
	}

	public override void Create()
	{
		__SkillsTree.dlgConfirmRetrain = null;
		__SkillsTree.dlgConfirmSave = null;
		__SkillsTree.investedSkillPoints = 0;
		__SkillsTree.playerSkillsOnScreen = new SortedList<ushort, __SkillsTree.SkillItem>();
		__SkillsTree.window = this;
		__SkillsTree.window.SetBackgroundTexture("NewGUI", "WndSkillTree");
		__SkillsTree.window.PutToCenter();
		__SkillsTree.window.isHidden = false;
		__SkillsTree.window.zOrder = 210;
		__SkillsTree.CreateCurrentTab();
	}

	private static void CreateCurrentTab()
	{
		int num;
		EventHandlerParam eventHandlerParam;
		__SkillsTree.<CreateCurrentTab>c__AnonStoreyA3 variable = null;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("NewGUI", string.Concat("SkillsTab", __SkillsTree.currentTab.ToString()));
		guiTexture.X = 210f;
		guiTexture.Y = 71f;
		__SkillsTree.window.AddGuiElement(guiTexture);
		__SkillsTree.forDelete.Add(guiTexture);
		__SkillsTree.infoLabel1 = new GuiLabel()
		{
			boundries = new Rect(35f, 330f, 160f, 195f),
			WordWrap = true,
			FontSize = 12
		};
		__SkillsTree.window.AddGuiElement(__SkillsTree.infoLabel1);
		__SkillsTree.forDelete.Add(__SkillsTree.infoLabel1);
		switch (__SkillsTree.currentTab)
		{
			case 0:
			{
				__SkillsTree.infoLabel1.text = StaticData.Translate("key_skills_tree_guardian_info");
				break;
			}
			case 1:
			{
				__SkillsTree.infoLabel1.text = StaticData.Translate("key_skills_tree_destroyer_info");
				break;
			}
			case 2:
			{
				__SkillsTree.infoLabel1.text = StaticData.Translate("key_skills_tree_protector_info");
				break;
			}
			case 3:
			{
				__SkillsTree.infoLabel1.text = StaticData.Translate("key_skills_tree_passive_info");
				break;
			}
			case 4:
			{
				__SkillsTree.infoLabel1.text = StaticData.Translate("key_skills_tree_amplification_info");
				break;
			}
		}
		string[] strArray = new string[] { StaticData.Translate("key_skills_tree_lbl_guardian"), StaticData.Translate("key_skills_tree_lbl_destroyer"), StaticData.Translate("key_skills_tree_lbl_protector"), StaticData.Translate("key_skills_tree_lbl_passive"), StaticData.Translate("key_skills_tree_lbl_amplification") };
		PlayerItems playerItem = NetworkScript.player.playerBelongings.playerItems;
		for (int i = 0; i < (int)strArray.Length; i++)
		{
			if (__SkillsTree.currentTab != i)
			{
				GuiButton guiButton = new GuiButton()
				{
					Caption = strArray[i],
					boundries = new Rect((float)(205 + i * 133), 54f, 148f, 66f),
					textColorNormal = GuiNewStyleBar.blueColor,
					textColorHover = Color.get_white(),
					FontSize = 12,
					Alignment = 4
				};
				eventHandlerParam = new EventHandlerParam()
				{
					customData = i
				};
				guiButton.eventHandlerParam = eventHandlerParam;
				guiButton.Clicked = new Action<EventHandlerParam>(null, __SkillsTree.OnTabChangeClick);
				__SkillsTree.window.AddGuiElement(guiButton);
				__SkillsTree.forDelete.Add(guiButton);
			}
			else
			{
				GuiLabel guiLabel = new GuiLabel()
				{
					text = strArray[i],
					boundries = new Rect((float)(205 + i * 133), 74f, 148f, 24f),
					FontSize = 12,
					Font = GuiLabel.FontBold,
					TextColor = GuiNewStyleBar.orangeColor,
					Alignment = 4
				};
				__SkillsTree.window.AddGuiElement(guiLabel);
				__SkillsTree.forDelete.Add(guiLabel);
			}
		}
		GuiLabel guiLabel1 = new GuiLabel()
		{
			TextColor = GuiNewStyleBar.blueColor,
			text = StaticData.Translate("key_skills_tree_skill_points"),
			boundries = new Rect(35f, 243f, 160f, 22f),
			Alignment = 3,
			FontSize = 12,
			hoverParam = 1,
			Hovered = new Action<object, bool>(null, __SkillsTree.OnSkillPointOrNueronHover)
		};
		__SkillsTree.window.AddGuiElement(guiLabel1);
		__SkillsTree.forDelete.Add(guiLabel1);
		GuiTexture x = new GuiTexture();
		x.SetTexture("NewGUI", "skillPointIcon");
		x.X = guiLabel1.X + guiLabel1.TextWidth + 2f;
		x.Y = guiLabel1.Y;
		__SkillsTree.window.AddGuiElement(x);
		__SkillsTree.forDelete.Add(x);
		int amountAt = (int)NetworkScript.player.playerBelongings.playerItems.GetAmountAt(PlayerItems.TypeTalentPoint) - __SkillsTree.investedSkillPoints;
		if (amountAt > 0)
		{
			x.SetTextureKeepSize("FrameworkGUI", "empty");
			GuiTextureAnimated guiTextureAnimated = new GuiTextureAnimated();
			guiTextureAnimated.Init("SkillStarAnimation", "SkillStarAnimation", "SkillStarAnimation/skillPointIconAnimation001");
			guiTextureAnimated.rotationTime = 1.2f;
			guiTextureAnimated.X = x.X;
			guiTextureAnimated.Y = x.Y;
			__SkillsTree.window.AddGuiElement(guiTextureAnimated);
			__SkillsTree.forDelete.Add(guiTextureAnimated);
		}
		GuiLabel guiLabel2 = new GuiLabel()
		{
			TextColor = Color.get_white(),
			text = amountAt.ToString(),
			Alignment = 3,
			boundries = guiLabel1.boundries
		};
		guiLabel2.boundries.set_x(guiLabel1.X + guiLabel1.TextWidth + 20f);
		guiLabel2.boundries.set_width(guiLabel1.boundries.get_width() - guiLabel1.TextWidth - 20f);
		guiLabel2.FontSize = 12;
		__SkillsTree.window.AddGuiElement(guiLabel2);
		__SkillsTree.forDelete.Add(guiLabel2);
		guiLabel1 = new GuiLabel()
		{
			TextColor = GuiNewStyleBar.blueColor,
			text = StaticData.Translate("key_skills_tree_neuron_modules"),
			boundries = new Rect(35f, 259f, 160f, 22f),
			Alignment = 3,
			FontSize = 12,
			hoverParam = 2,
			Hovered = new Action<object, bool>(null, __SkillsTree.OnSkillPointOrNueronHover)
		};
		guiLabel1.boundries.set_width(guiLabel1.TextWidth);
		__SkillsTree.window.AddGuiElement(guiLabel1);
		__SkillsTree.forDelete.Add(guiLabel1);
		GuiTexture y = new GuiTexture();
		y.SetTexture("NewGUI", "neuronIcon");
		y.X = guiLabel1.X + guiLabel1.TextWidth + 2f;
		y.Y = guiLabel1.Y;
		y.hoverParam = 2;
		y.Hovered = new Action<object, bool>(null, __SkillsTree.OnSkillPointOrNueronHover);
		__SkillsTree.window.AddGuiElement(y);
		__SkillsTree.forDelete.Add(y);
		GuiLabel str = new GuiLabel()
		{
			TextColor = Color.get_white()
		};
		long amountAt1 = playerItem.GetAmountAt(PlayerItems.TypeNeuron);
		str.text = amountAt1.ToString();
		str.Alignment = 3;
		str.boundries = new Rect(y.X + y.boundries.get_width() + 2f, 259f, 160f, 22f);
		str.FontSize = 12;
		str.boundries.set_width(str.TextWidth);
		str.Hovered = new Action<object, bool>(null, __SkillsTree.OnSkillPointOrNueronHover);
		str.hoverParam = 2;
		__SkillsTree.window.AddGuiElement(str);
		__SkillsTree.forDelete.Add(str);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed()
		{
			X = 155f,
			Y = 275f
		};
		guiButtonFixed.SetTexture("FusionWindow", "get_neurons");
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(null, __SkillsTree.OnGetMoreNeuronClicked);
		guiButtonFixed.Hovered = new Action<object, bool>(null, __SkillsTree.OnSkillPointOrNueronHover);
		guiButtonFixed.hoverParam = 3;
		__SkillsTree.window.AddGuiElement(guiButtonFixed);
		__SkillsTree.forDelete.Add(guiButtonFixed);
		guiLabel1 = new GuiLabel()
		{
			text = string.Format(StaticData.Translate("key_skills_tree_player_level"), NetworkScript.player.cfg.playerLevel),
			TextColor = Color.get_white(),
			FontSize = 14,
			boundries = new Rect(20f, 213f, 180f, 30f),
			Alignment = 1
		};
		__SkillsTree.window.AddGuiElement(guiLabel1);
		__SkillsTree.forDelete.Add(guiLabel1);
		guiLabel1 = new GuiLabel()
		{
			text = (!NetworkScript.player.vessel.isGuest ? NetworkScript.player.playerBelongings.playerName : StaticData.Translate("key_guest_player")),
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 15,
			boundries = new Rect(42f, 72f, 148f, 24f),
			Alignment = 4
		};
		__SkillsTree.window.AddGuiElement(guiLabel1);
		__SkillsTree.forDelete.Add(guiLabel1);
		bool flag = playerItem.GetAmountAt(PlayerItems.TypeNeuron) > (long)0;
		List<ushort> mostAdvancedSkills = __SkillsTree.GetMostAdvancedSkills();
		__SkillsTree.SkillTreeItem[] skillTreeItemArray = __SkillsTree.skillTrees[__SkillsTree.currentTab];
		for (int j = 0; j < (int)skillTreeItemArray.Length; j++)
		{
			__SkillsTree.SkillTreeItem skillTreeItem = skillTreeItemArray[j];
			TalentsInfo talentsInfo = (TalentsInfo)Enumerable.First<PlayerItemTypesData>(Enumerable.Where<PlayerItemTypesData>(StaticData.allTypes.get_Values(), new Func<PlayerItemTypesData, bool>(variable, (PlayerItemTypesData t) => t.itemType == this.skill.skillType)));
			if (!__SkillsTree.playerSkillsOnScreen.ContainsKey(talentsInfo.itemType))
			{
				SortedList<ushort, __SkillsTree.SkillItem> sortedList = __SkillsTree.playerSkillsOnScreen;
				ushort num1 = talentsInfo.itemType;
				__SkillsTree.SkillItem skillItem = new __SkillsTree.SkillItem()
				{
					skillType = talentsInfo.itemType,
					realLevel = (int)playerItem.GetAmountAt(talentsInfo.itemType),
					wantedLevel = (int)playerItem.GetAmountAt(talentsInfo.itemType)
				};
				sortedList.Add(num1, skillItem);
			}
			GuiLabel fontBold = new GuiLabel()
			{
				text = StaticData.Translate(talentsInfo.uiName),
				X = (float)(skillTreeItem.x + 54),
				Y = (float)(skillTreeItem.y + 4),
				FontSize = 12
			};
			fontBold.boundries.set_width(120f);
			fontBold.boundries.set_height(24f);
			fontBold.Alignment = 3;
			fontBold.TextColor = GuiNewStyleBar.blueColor;
			fontBold.Font = GuiLabel.FontBold;
			__SkillsTree.window.AddGuiElement(fontBold);
			__SkillsTree.forDelete.Add(fontBold);
			for (int k = 0; k < talentsInfo.maxLevel; k++)
			{
				GuiTexture guiTexture1 = new GuiTexture();
				if ((long)k < playerItem.GetAmountAt(talentsInfo.itemType))
				{
					guiTexture1.SetTexture("NewGUI", "fillStarIcon");
				}
				else if (k >= __SkillsTree.playerSkillsOnScreen.get_Item(talentsInfo.itemType).wantedLevel)
				{
					guiTexture1.SetTexture("NewGUI", "blankStarIcon");
				}
				else
				{
					guiTexture1.SetTexture("GUI", "skillPointIcon_white");
				}
				guiTexture1.X = (float)(skillTreeItem.x + 55 + k * 24);
				guiTexture1.Y = (float)(skillTreeItem.y + 25);
				__SkillsTree.window.AddGuiElement(guiTexture1);
				__SkillsTree.forDelete.Add(guiTexture1);
			}
			bool flag1 = __SkillsTree.CanInvestSkillPoint(talentsInfo.itemType);
			if (__SkillsTree.currentTab == 4)
			{
				flag1 = (!flag1 ? false : NetworkScript.player.playerBelongings.playerLevel >= PlayerItems.AMPLIFICATION_SKILL_TREE_MIN_LEVEL);
			}
			long amountAt2 = playerItem.GetAmountAt(talentsInfo.itemType);
			if (amountAt2 >= (long)talentsInfo.maxLevel)
			{
				GuiTexture action = new GuiTexture()
				{
					X = (float)(skillTreeItem.x + 38),
					Y = (float)skillTreeItem.y
				};
				action.SetTexture("NewGUI", "SkillTreeRightFrameFull");
				action.Hovered = new Action<object, bool>(null, __SkillsTree.OnTalentNeuronHovered);
				action.hoverParam = talentsInfo.itemType;
				__SkillsTree.window.AddGuiElement(action);
				__SkillsTree.forDelete.Add(action);
			}
			else if (__SkillsTree.playerSkillsOnScreen.get_Item(talentsInfo.itemType).wantedLevel >= talentsInfo.maxLevel)
			{
				GuiTexture action1 = new GuiTexture()
				{
					X = (float)(skillTreeItem.x + 38),
					Y = (float)skillTreeItem.y
				};
				action1.SetTexture("NewGUI", "SkillTreeRightFrameHvr");
				action1.Hovered = new Action<object, bool>(null, __SkillsTree.OnTalentNeuronHovered);
				action1.hoverParam = talentsInfo.itemType;
				__SkillsTree.window.AddGuiElement(action1);
				__SkillsTree.forDelete.Add(action1);
			}
			else if (flag1)
			{
				GuiButtonFixed empty = new GuiButtonFixed()
				{
					X = (float)(skillTreeItem.x + 38),
					Y = (float)skillTreeItem.y
				};
				empty.SetTexture("NewGUI", "SkillTreeRightFrame");
				empty.Caption = string.Empty;
				empty.Clicked = new Action<EventHandlerParam>(null, __SkillsTree.OnTalentClicked);
				empty.SetLeftClickSound("Sounds", "button2");
				eventHandlerParam = new EventHandlerParam()
				{
					customData = talentsInfo.itemType
				};
				empty.eventHandlerParam = eventHandlerParam;
				empty.Hovered = new Action<object, bool>(null, __SkillsTree.OnTalentNeuronHovered);
				empty.hoverParam = talentsInfo.itemType;
				__SkillsTree.window.AddGuiElement(empty);
				__SkillsTree.forDelete.Add(empty);
				if (NetworkScript.player.playerBelongings.playerItems.GetAmountAt(PlayerItems.TypeTalentPoint) > (long)0 && mostAdvancedSkills.Contains(talentsInfo.itemType))
				{
					GuiTextureAnimated guiTextureAnimated1 = new GuiTextureAnimated();
					guiTextureAnimated1.Init("SkillFrameAnimation", "SkillFrameAnimation", "SkillFrameAnimation/SkillTreeItemFrameAnimaion1");
					guiTextureAnimated1.rotationTime = 0.8f;
					guiTextureAnimated1.X = empty.X;
					guiTextureAnimated1.Y = empty.Y;
					__SkillsTree.window.AddGuiElement(guiTextureAnimated1);
					__SkillsTree.forDelete.Add(guiTextureAnimated1);
				}
			}
			else
			{
				GuiTexture guiTexture2 = new GuiTexture()
				{
					X = (float)(skillTreeItem.x + 38),
					Y = (float)skillTreeItem.y
				};
				guiTexture2.SetTexture("NewGUI", "SkillTreeRightFrameDsb");
				guiTexture2.Hovered = new Action<object, bool>(null, __SkillsTree.OnTalentNeuronHovered);
				guiTexture2.hoverParam = talentsInfo.itemType;
				__SkillsTree.window.AddGuiElement(guiTexture2);
				__SkillsTree.forDelete.Add(guiTexture2);
				fontBold.TextColor = Color.get_gray();
			}
			GuiTexture guiTexture3 = new GuiTexture()
			{
				boundries = new Rect((float)(skillTreeItem.x + 2), (float)skillTreeItem.y, 48f, 48f)
			};
			guiTexture3.SetTextureKeepSize("Skills", talentsInfo.assetName);
			__SkillsTree.window.AddGuiElement(guiTexture3);
			__SkillsTree.forDelete.Add(guiTexture3);
			GuiButtonFixed guiButtonFixed1 = new GuiButtonFixed()
			{
				X = (float)(skillTreeItem.x - 10),
				Y = (float)(skillTreeItem.y - 10)
			};
			guiButtonFixed1.SetTexture("NewGUI", "SkillTreeLeftFrame");
			guiButtonFixed1.Caption = string.Empty;
			guiButtonFixed1.eventHandlerParam.customData = skillTreeItem.skillType;
			guiButtonFixed1.Clicked = new Action<EventHandlerParam>(null, __SkillsTree.ShowSkillInfo);
			guiButtonFixed1.Hovered = new Action<object, bool>(null, __SkillsTree.OnTalentNeuronHovered);
			guiButtonFixed1.hoverParam = talentsInfo.itemType;
			__SkillsTree.window.AddGuiElement(guiButtonFixed1);
			__SkillsTree.forDelete.Add(guiButtonFixed1);
			long num2 = playerItem.GetAmountAt((ushort)(talentsInfo.itemType + 1000));
			__SkillsTree.DrawState drawState = __SkillsTree.DrawState.Disabled;
			if (num2 == (long)5)
			{
				drawState = __SkillsTree.DrawState.Full;
			}
			else if (flag && amountAt2 > (long)0)
			{
				drawState = __SkillsTree.DrawState.Enabled;
			}
			switch (drawState)
			{
				case __SkillsTree.DrawState.Disabled:
				{
					GuiTexture action2 = new GuiTexture()
					{
						X = (float)(skillTreeItem.x + 170),
						Y = (float)skillTreeItem.y
					};
					action2.SetTexture("NewGUI", "SkillTreeNeuronFrameDsb");
					action2.Hovered = new Action<object, bool>(null, __SkillsTree.OnTalentNeuronHovered);
					action2.hoverParam = talentsInfo.itemType;
					__SkillsTree.window.AddGuiElement(action2);
					__SkillsTree.forDelete.Add(action2);
					break;
				}
				case __SkillsTree.DrawState.Enabled:
				{
					GuiButtonFixed empty1 = new GuiButtonFixed()
					{
						X = (float)(skillTreeItem.x + 170),
						Y = (float)skillTreeItem.y
					};
					empty1.SetTexture("NewGUI", "SkillTreeNeuronFrame");
					empty1.Caption = string.Empty;
					eventHandlerParam = new EventHandlerParam()
					{
						customData = (ushort)(talentsInfo.itemType + 1000)
					};
					empty1.eventHandlerParam = eventHandlerParam;
					empty1.Clicked = new Action<EventHandlerParam>(null, __SkillsTree.OnNeuronClicked);
					empty1.SetLeftClickSound("Sounds", "button2");
					empty1.Hovered = new Action<object, bool>(null, __SkillsTree.OnTalentNeuronHovered);
					empty1.hoverParam = talentsInfo.itemType;
					__SkillsTree.window.AddGuiElement(empty1);
					__SkillsTree.forDelete.Add(empty1);
					GuiButtonFixed guiButtonFixed2 = new GuiButtonFixed()
					{
						X = (float)(skillTreeItem.x + 190),
						Y = (float)(skillTreeItem.y + 30)
					};
					guiButtonFixed2.SetTexture("NewGUI", "AddNeuron");
					guiButtonFixed2.Caption = string.Empty;
					eventHandlerParam = new EventHandlerParam()
					{
						customData = (ushort)(talentsInfo.itemType + 1000)
					};
					guiButtonFixed2.eventHandlerParam = eventHandlerParam;
					guiButtonFixed2.Clicked = new Action<EventHandlerParam>(null, __SkillsTree.OnNeuronClicked);
					guiButtonFixed2.SetLeftClickSound("Sounds", "button2");
					guiButtonFixed2.Hovered = new Action<object, bool>(null, __SkillsTree.OnTalentNeuronHovered);
					guiButtonFixed2.hoverParam = talentsInfo.itemType;
					__SkillsTree.window.AddGuiElement(guiButtonFixed2);
					__SkillsTree.forDelete.Add(guiButtonFixed2);
					break;
				}
				case __SkillsTree.DrawState.Full:
				{
					GuiTexture action3 = new GuiTexture()
					{
						X = (float)(skillTreeItem.x + 170),
						Y = (float)skillTreeItem.y
					};
					action3.SetTexture("NewGUI", "SkillTreeNeuronFrameFull");
					action3.Hovered = new Action<object, bool>(null, __SkillsTree.OnTalentNeuronHovered);
					action3.hoverParam = talentsInfo.itemType;
					__SkillsTree.window.AddGuiElement(action3);
					__SkillsTree.forDelete.Add(action3);
					break;
				}
			}
			int amountAt3 = (int)playerItem.GetAmountAt((ushort)(talentsInfo.itemType + 1000));
			if (amountAt3 != 0)
			{
				GuiTexture guiTexture4 = new GuiTexture()
				{
					X = (float)(skillTreeItem.x + 170),
					Y = (float)skillTreeItem.y
				};
				guiTexture4.SetTexture("NewGUI", string.Concat("SkillTreeNeurons", amountAt3.ToString()));
				__SkillsTree.window.AddGuiElement(guiTexture4);
				__SkillsTree.forDelete.Add(guiTexture4);
			}
		}
		switch (__SkillsTree.currentTab)
		{
			case 0:
			case 1:
			case 2:
			{
				__SkillsTree.DrawBarrierLine(175, 5);
				__SkillsTree.DrawBarrierLine(245, 10);
				__SkillsTree.DrawBarrierLine(315, 15);
				__SkillsTree.DrawBarrierLine(385, 20);
				__SkillsTree.DrawBarrierLine(455, 25);
				break;
			}
			case 3:
			{
				__SkillsTree.DrawBarrierLine(210, 5);
				__SkillsTree.DrawBarrierLine(320, 10);
				__SkillsTree.DrawBarrierLine(430, 15);
				break;
			}
			case 4:
			{
				for (int l = 0; l < 6; l++)
				{
					GuiTexture guiTexture5 = new GuiTexture();
					guiTexture5.SetTexture("NewGUI", "SkillsDownArrow");
					guiTexture5.X = (float)(321 + 220 * (l / 2));
					guiTexture5.Y = (float)(179 + 110 * (l % 2));
					__SkillsTree.window.AddGuiElement(guiTexture5);
					__SkillsTree.forDelete.Add(guiTexture5);
				}
				GuiLabel guiLabel3 = new GuiLabel()
				{
					boundries = new Rect(224f, 103f, 645f, 25f),
					text = string.Format(StaticData.Translate("key_skills_tree_amplification_level"), PlayerItems.AMPLIFICATION_SKILL_TREE_MIN_LEVEL),
					Font = GuiLabel.FontBold,
					FontSize = 14,
					TextColor = GuiNewStyleBar.orangeColor,
					Alignment = 0
				};
				__SkillsTree.window.AddGuiElement(guiLabel3);
				__SkillsTree.forDelete.Add(guiLabel3);
				break;
			}
		}
		guiLabel1 = new GuiLabel()
		{
			text = StaticData.Translate("key_skills_tree_menu_title").ToUpper(),
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 18,
			Font = GuiLabel.FontBold,
			boundries = new Rect(280f, 42f, 540f, 35f),
			Alignment = 1
		};
		__SkillsTree.window.AddGuiElement(guiLabel1);
		__SkillsTree.forDelete.Add(guiLabel1);
		string str1 = string.Format(StaticData.Translate("key_skills_tree_btn_rearange"), PlayerObjectPhysics.TALENT_RETRAIN_PRICE);
		__SkillsTree.btnRetrain = new GuiButtonResizeable();
		__SkillsTree.btnRetrain.SetOrangeTexture();
		__SkillsTree.btnRetrain.X = 230f;
		__SkillsTree.btnRetrain.Y = 488f;
		__SkillsTree.btnRetrain.boundries.set_width(200f);
		GuiButtonResizeable guiButtonResizeable = __SkillsTree.btnRetrain;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = string.Format(StaticData.Translate("key_skills_tree_btn_rearange_tooltip"), PlayerObjectPhysics.TALENT_RETRAIN_PRICE),
			customData2 = __SkillsTree.btnRetrain
		};
		guiButtonResizeable.tooltipWindowParam = eventHandlerParam;
		__SkillsTree.btnRetrain.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		__SkillsTree.btnRetrain.Caption = str1.ToUpper();
		__SkillsTree.btnRetrain.FontSize = 12;
		__SkillsTree.btnRetrain.Alignment = 4;
		__SkillsTree.btnRetrain.Clicked = new Action<EventHandlerParam>(null, __SkillsTree.OnRetrainClicked);
		if (playerItem.get_Nova() >= (long)PlayerObjectPhysics.TALENT_RETRAIN_PRICE)
		{
			num = (__SkillsTree.currentTab != 4 ? playerItem.SpentTalentPoints(__SkillsTree.currentTab + 1) : playerItem.SpentTalentPoints(5) + playerItem.SpentTalentPoints(6) + playerItem.SpentTalentPoints(7));
			if (num < 1)
			{
				GuiButtonResizeable guiButtonResizeable1 = __SkillsTree.btnRetrain;
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_skills_tree_btn_tooltip_no_spend_point"),
					customData2 = __SkillsTree.btnRetrain
				};
				guiButtonResizeable1.tooltipWindowParam = eventHandlerParam;
				__SkillsTree.btnRetrain.isEnabled = false;
			}
		}
		else
		{
			GuiButtonResizeable guiButtonResizeable2 = __SkillsTree.btnRetrain;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = string.Format(StaticData.Translate("key_skills_tree_btn_tooltip_no_money"), PlayerObjectPhysics.TALENT_RETRAIN_PRICE),
				customData2 = __SkillsTree.btnRetrain
			};
			guiButtonResizeable2.tooltipWindowParam = eventHandlerParam;
			__SkillsTree.btnRetrain.isEnabled = false;
		}
		__SkillsTree.window.AddGuiElement(__SkillsTree.btnRetrain);
		__SkillsTree.forDelete.Add(__SkillsTree.btnRetrain);
		__SkillsTree.btnSave = new GuiButtonResizeable();
		__SkillsTree.btnSave.SetBlueTexture();
		__SkillsTree.btnSave.X = 655f;
		__SkillsTree.btnSave.Y = 488f;
		__SkillsTree.btnSave.boundries.set_width(110f);
		__SkillsTree.btnSave.Caption = StaticData.Translate("key_skills_tree_btn_save");
		__SkillsTree.btnSave.FontSize = 12;
		__SkillsTree.btnSave.Alignment = 4;
		__SkillsTree.btnSave.Clicked = new Action<EventHandlerParam>(null, __SkillsTree.OnSaveBtnClicked);
		__SkillsTree.btnSave.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		__SkillsTree.btnSave.isEnabled = __SkillsTree.investedSkillPoints > 0;
		__SkillsTree.window.AddGuiElement(__SkillsTree.btnSave);
		__SkillsTree.forDelete.Add(__SkillsTree.btnSave);
		GuiTexture x1 = new GuiTexture();
		x1.SetTexture("GUI", "icon_save");
		x1.X = __SkillsTree.btnSave.X + 10f;
		x1.Y = __SkillsTree.btnSave.Y + 10f;
		__SkillsTree.window.AddGuiElement(x1);
		__SkillsTree.forDelete.Add(x1);
		if (!__SkillsTree.btnSave.isEnabled)
		{
			GuiButtonResizeable guiButtonResizeable3 = __SkillsTree.btnSave;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_skills_tree_btn_tooltip_no_invested_point"),
				customData2 = __SkillsTree.btnSave
			};
			guiButtonResizeable3.tooltipWindowParam = eventHandlerParam;
		}
		else
		{
			GuiButtonFixed rect = new GuiButtonFixed();
			rect.SetTexture("FrameworkGUI", "empty");
			rect.boundries = new Rect(835f, 0f, 72f, 72f);
			rect.Caption = string.Empty;
			rect.eventHandlerParam.customData = 100;
			rect.Clicked = new Action<EventHandlerParam>(null, __SkillsTree.ShowSaveDiscardDialogue);
			__SkillsTree.window.AddGuiElementAtBottom(rect);
			__SkillsTree.forDelete.Add(rect);
			GuiButtonResizeable guiButtonResizeable4 = __SkillsTree.btnSave;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_skills_tree_btn_tooltip_save"),
				customData2 = __SkillsTree.btnSave
			};
			guiButtonResizeable4.tooltipWindowParam = eventHandlerParam;
		}
		__SkillsTree.btnDiscard = new GuiButtonResizeable();
		__SkillsTree.btnDiscard.SetTexture("NewGUI", "btnRed_");
		__SkillsTree.btnDiscard.X = 770f;
		__SkillsTree.btnDiscard.Y = 488f;
		__SkillsTree.btnDiscard.boundries.set_width(110f);
		__SkillsTree.btnDiscard.Caption = StaticData.Translate("key_skills_tree_btn_discard");
		__SkillsTree.btnDiscard.FontSize = 12;
		__SkillsTree.btnDiscard.Alignment = 4;
		__SkillsTree.btnDiscard.Clicked = new Action<EventHandlerParam>(null, __SkillsTree.OnDiscardBtnClicked);
		__SkillsTree.btnDiscard.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		__SkillsTree.btnDiscard.isEnabled = __SkillsTree.investedSkillPoints > 0;
		__SkillsTree.window.AddGuiElement(__SkillsTree.btnDiscard);
		__SkillsTree.forDelete.Add(__SkillsTree.btnDiscard);
		GuiTexture y1 = new GuiTexture();
		y1.SetTexture("GUI", "icon_discard");
		y1.X = __SkillsTree.btnDiscard.X + 10f;
		y1.Y = __SkillsTree.btnDiscard.Y + 10f;
		__SkillsTree.window.AddGuiElement(y1);
		__SkillsTree.forDelete.Add(y1);
		if (!__SkillsTree.btnDiscard.isEnabled)
		{
			GuiButtonResizeable guiButtonResizeable5 = __SkillsTree.btnDiscard;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_skills_tree_btn_tooltip_no_invested_point"),
				customData2 = __SkillsTree.btnDiscard
			};
			guiButtonResizeable5.tooltipWindowParam = eventHandlerParam;
		}
		else
		{
			GuiButtonResizeable guiButtonResizeable6 = __SkillsTree.btnDiscard;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_skills_tree_btn_tooltip_discard"),
				customData2 = __SkillsTree.btnDiscard
			};
			guiButtonResizeable6.tooltipWindowParam = eventHandlerParam;
		}
		string str2 = NetworkScript.player.cfg.assetName;
		GuiTexture guiTexture6 = new GuiTexture();
		guiTexture6.SetTexture("ShipsAvatars", str2);
		guiTexture6.X = 61f;
		guiTexture6.Y = 110f;
		guiTexture6.SetSize(104f, 71f);
		__SkillsTree.window.AddGuiElement(guiTexture6);
		__SkillsTree.forDelete.Add(guiTexture6);
		guiLabel1 = new GuiLabel()
		{
			text = StaticData.Translate("key_skills_tree_infobox"),
			boundries = new Rect(22f, 300f, 82f, 20f),
			Alignment = 1,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 14
		};
		__SkillsTree.window.AddGuiElement(guiLabel1);
		__SkillsTree.forDelete.Add(guiLabel1);
	}

	private static void DrawBarrierLine(int y, int points)
	{
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("NewGUI", "SkillLine");
		guiTexture.X = 230f;
		guiTexture.Y = (float)y;
		__SkillsTree.window.AddGuiElement(guiTexture);
		__SkillsTree.forDelete.Add(guiTexture);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = string.Format(StaticData.Translate("key_skills_tree_player_skill_point"), points),
			hoverParam = points,
			Hovered = new Action<object, bool>(null, __SkillsTree.OnBarrierLineHover),
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			boundries = new Rect(guiTexture.X, guiTexture.Y + 7f, 120f, 25f)
		};
		__SkillsTree.window.AddGuiElement(guiLabel);
		__SkillsTree.forDelete.Add(guiLabel);
	}

	private static List<ushort> GetMostAdvancedSkills()
	{
		List<ushort> list = new List<ushort>();
		if (NetworkScript.player.playerBelongings.playerItems.GetAmountAt(PlayerItems.TypeTalentPoint) < (long)1)
		{
			return list;
		}
		SortedList<int, List<ushort>> sortedList = new SortedList<int, List<ushort>>();
		switch (__SkillsTree.currentTab)
		{
			case 0:
			{
				__SkillsTree.AddSkillToList(list, PlayerItems.guardianSkills);
				break;
			}
			case 1:
			{
				__SkillsTree.AddSkillToList(list, PlayerItems.destroyerSkills);
				break;
			}
			case 2:
			{
				__SkillsTree.AddSkillToList(list, PlayerItems.protectorSkills);
				break;
			}
			case 3:
			{
				__SkillsTree.AddSkillToList(list, PlayerItems.passiveSkills);
				break;
			}
			case 4:
			{
				__SkillsTree.AddSkillToList(list, PlayerItems.amplificationSkillsOne);
				__SkillsTree.AddSkillToList(list, PlayerItems.amplificationSkillsTwo);
				__SkillsTree.AddSkillToList(list, PlayerItems.amplificationSkillsThree);
				break;
			}
		}
		return list;
	}

	private static void InvestTalentPoint(ushort talentType)
	{
		if (!__SkillsTree.playerSkillsOnScreen.ContainsKey(talentType))
		{
			return;
		}
		__SkillsTree.SkillItem item = __SkillsTree.playerSkillsOnScreen.get_Item(talentType);
		item.wantedLevel = item.wantedLevel + 1;
		__SkillsTree.investedSkillPoints = __SkillsTree.investedSkillPoints + 1;
	}

	private static void ManageInfoBoxes(ushort talentType, bool isHover)
	{
		if (!isHover)
		{
			if (__SkillsTree.lastHoveredTalentType != talentType)
			{
				return;
			}
			switch (__SkillsTree.currentTab)
			{
				case 0:
				{
					__SkillsTree.infoLabel1.text = StaticData.Translate("key_skills_tree_guardian_info");
					break;
				}
				case 1:
				{
					__SkillsTree.infoLabel1.text = StaticData.Translate("key_skills_tree_destroyer_info");
					break;
				}
				case 2:
				{
					__SkillsTree.infoLabel1.text = StaticData.Translate("key_skills_tree_protector_info");
					break;
				}
				case 3:
				{
					__SkillsTree.infoLabel1.text = StaticData.Translate("key_skills_tree_passive_info");
					break;
				}
				case 4:
				{
					__SkillsTree.infoLabel1.text = StaticData.Translate("key_skills_tree_amplification_info");
					break;
				}
			}
		}
		else
		{
			__SkillsTree.lastHoveredTalentType = talentType;
			byte item = 0;
			if (__SkillsTree.playerSkillsOnScreen != null && __SkillsTree.playerSkillsOnScreen.ContainsKey(talentType))
			{
				item = (byte)__SkillsTree.playerSkillsOnScreen.get_Item(talentType).wantedLevel;
			}
			if (talentType == PlayerItems.TypeTalentsNanoStorm || talentType == PlayerItems.TypeTalentsShortCircuit || talentType == PlayerItems.TypeTalentsTaunt || talentType == PlayerItems.TypeTalentsFocusFire)
			{
				__SkillsTree.infoLabel1.text = string.Concat(NetworkScript.player.playerBelongings.playerItems.GetSkillDescription(talentType, item), "\n\n\n\n", NetworkScript.player.playerBelongings.playerItems.GetNeuronlInfo(talentType));
			}
			else
			{
				__SkillsTree.infoLabel1.text = string.Concat(new string[] { NetworkScript.player.playerBelongings.playerItems.GetSkillDescription(talentType, item), "\n\n", NetworkScript.player.playerBelongings.playerItems.GetLevelInfo(talentType), "\n\n", NetworkScript.player.playerBelongings.playerItems.GetNeuronlInfo(talentType) });
			}
		}
	}

	private static void OnBarrierLineHover(object prm, bool isHover)
	{
		if (!isHover)
		{
			switch (__SkillsTree.currentTab)
			{
				case 0:
				{
					__SkillsTree.infoLabel1.text = StaticData.Translate("key_skills_tree_guardian_info");
					break;
				}
				case 1:
				{
					__SkillsTree.infoLabel1.text = StaticData.Translate("key_skills_tree_destroyer_info");
					break;
				}
				case 2:
				{
					__SkillsTree.infoLabel1.text = StaticData.Translate("key_skills_tree_protector_info");
					break;
				}
				case 3:
				{
					__SkillsTree.infoLabel1.text = StaticData.Translate("key_skills_tree_passive_info");
					break;
				}
			}
		}
		else
		{
			__SkillsTree.infoLabel1.text = string.Format(StaticData.Translate("key_skills_tree_barrier_line_tooltip"), prm);
		}
	}

	private static void OnConfirmSaveClickedNo(object p)
	{
		__SkillsTree.dlgConfirmSave.RemoveGUIItems();
		__SkillsTree.dlgConfirmSave = null;
		__SkillsTree.OnDiscardBtnClicked(null);
		if ((int)((EventHandlerParam)p).customData != 100)
		{
			__SkillsTree.OnTabChangeClick((EventHandlerParam)p);
		}
		else
		{
			if (NetworkScript.player.shipScript != null)
			{
				NetworkScript.player.shipScript.isGuiClosed = true;
			}
			AndromedaGui.mainWnd.CloseActiveWindow();
		}
	}

	private static void OnConfirmSaveClickedYes(object p)
	{
		__SkillsTree.dlgConfirmSave.RemoveGUIItems();
		__SkillsTree.dlgConfirmSave = null;
		__SkillsTree.OnSaveBtnClicked(null);
		if ((int)((EventHandlerParam)p).customData != 100)
		{
			__SkillsTree.OnTabChangeClick((EventHandlerParam)p);
		}
		else
		{
			if (NetworkScript.player.shipScript != null)
			{
				NetworkScript.player.shipScript.isGuiClosed = true;
			}
			AndromedaGui.mainWnd.CloseActiveWindow();
		}
	}

	private static void OnDiscardBtnClicked(object prm)
	{
		if (__SkillsTree.dlgConfirmSave != null || __SkillsTree.dlgConfirmRetrain != null)
		{
			return;
		}
		__SkillsTree.investedSkillPoints = 0;
		__SkillsTree.playerSkillsOnScreen.Clear();
		__SkillsTree.Clear();
		__SkillsTree.CreateCurrentTab();
	}

	public void OnEscKeyPressed()
	{
		if (__SkillsTree.investedSkillPoints == 0)
		{
			AndromedaGui.mainWnd.CloseActiveWindow();
			return;
		}
		__SkillsTree.ShowSaveDiscardDialogue(new EventHandlerParam()
		{
			customData = 100
		});
	}

	private static void OnGetMoreNeuronClicked(object prm)
	{
		MainScreenWindow mainScreenWindow = AndromedaGui.mainWnd;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = (byte)11
		};
		mainScreenWindow.OnWindowBtnClicked(eventHandlerParam);
	}

	private static void OnNeuronClicked(EventHandlerParam p)
	{
		if (__SkillsTree.dlgConfirmSave != null || __SkillsTree.dlgConfirmRetrain != null)
		{
			return;
		}
		__SkillsTree.BuyNeuron((ushort)p.customData);
		__SkillsTree.Clear();
		__SkillsTree.CreateCurrentTab();
	}

	private static void OnRetrainClicked(EventHandlerParam p)
	{
		if (__SkillsTree.dlgConfirmSave != null || __SkillsTree.dlgConfirmRetrain != null)
		{
			return;
		}
		__SkillsTree.btnRetrain.isEnabled = false;
		if (__SkillsTree.dlgConfirmSave != null)
		{
			__SkillsTree.dlgConfirmSave.RemoveGUIItems();
			__SkillsTree.dlgConfirmSave = null;
		}
		__SkillsTree.dlgConfirmRetrain = new GuiDialog();
		__SkillsTree.dlgConfirmRetrain.Create(string.Format(StaticData.Translate("key_skills_tree_retrain_question"), PlayerObjectPhysics.TALENT_RETRAIN_PRICE), StaticData.Translate("key_skills_tree_retrain_ok"), StaticData.Translate("key_skills_tree_retrain_cancel"), __SkillsTree.window);
		__SkillsTree.dlgConfirmRetrain.OkClicked = new Action<object>(null, __SkillsTree.OnTalentRetrainConfirmed);
		__SkillsTree.dlgConfirmRetrain.CancelClicked = new Action<object>(null, __SkillsTree.OnTalentRetrainCancelled);
	}

	private static void OnSaveBtnClicked(object prm)
	{
		// 
		// Current member / type: System.Void __SkillsTree::OnSaveBtnClicked(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnSaveBtnClicked(System.Object)
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

	private static void OnSkillPointOrNueronHover(object prm, bool isHover)
	{
		if (!isHover)
		{
			__SkillsTree.infoLabel1.boundries = new Rect(30f, 330f, 172f, 205f);
			switch (__SkillsTree.currentTab)
			{
				case 0:
				{
					__SkillsTree.infoLabel1.text = StaticData.Translate("key_skills_tree_guardian_info");
					break;
				}
				case 1:
				{
					__SkillsTree.infoLabel1.text = StaticData.Translate("key_skills_tree_destroyer_info");
					break;
				}
				case 2:
				{
					__SkillsTree.infoLabel1.text = StaticData.Translate("key_skills_tree_protector_info");
					break;
				}
				case 3:
				{
					__SkillsTree.infoLabel1.text = StaticData.Translate("key_skills_tree_passive_info");
					break;
				}
				case 4:
				{
					__SkillsTree.infoLabel1.text = StaticData.Translate("key_skills_tree_amplification_info");
					break;
				}
			}
		}
		else if ((int)prm == 1)
		{
			__SkillsTree.infoLabel1.text = StaticData.Translate("key_skills_tree_tooltip_skill_point");
		}
		else if ((int)prm == 2)
		{
			__SkillsTree.infoLabel1.text = StaticData.Translate("key_skills_tree_tooltip_neurone");
		}
		else if ((int)prm == 3)
		{
			__SkillsTree.infoLabel1.text = StaticData.Translate("key_skills_tree_tooltip_get_neuron");
		}
	}

	private static void OnTabChangeClick(EventHandlerParam p)
	{
		if (__SkillsTree.dlgConfirmSave != null || __SkillsTree.dlgConfirmRetrain != null)
		{
			return;
		}
		if (__SkillsTree.investedSkillPoints != 0)
		{
			__SkillsTree.ShowSaveDiscardDialogue(p);
			return;
		}
		__SkillsTree.investedSkillPoints = 0;
		__SkillsTree.playerSkillsOnScreen.Clear();
		__SkillsTree.Clear();
		__SkillsTree.currentTab = (int)p.customData;
		__SkillsTree.CreateCurrentTab();
	}

	private static void OnTalentClicked(EventHandlerParam p)
	{
		if (__SkillsTree.dlgConfirmSave != null || __SkillsTree.dlgConfirmRetrain != null)
		{
			return;
		}
		__SkillsTree.InvestTalentPoint((ushort)p.customData);
		__SkillsTree.Clear();
		__SkillsTree.CreateCurrentTab();
	}

	private static void OnTalentNeuronHovered(object talantIndex, bool isHover)
	{
		__SkillsTree.ManageInfoBoxes((ushort)talantIndex, isHover);
	}

	private static void OnTalentRetrainCancelled(object prm)
	{
		__SkillsTree.btnRetrain.isEnabled = true;
		__SkillsTree.dlgConfirmRetrain.RemoveGUIItems();
		__SkillsTree.dlgConfirmRetrain = null;
	}

	private static void OnTalentRetrainConfirmed(object prm)
	{
		// 
		// Current member / type: System.Void __SkillsTree::OnTalentRetrainConfirmed(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnTalentRetrainConfirmed(System.Object)
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

	private static void ShowSaveDiscardDialogue(EventHandlerParam prm)
	{
		if (__SkillsTree.dlgConfirmRetrain != null)
		{
			__SkillsTree.btnRetrain.isEnabled = true;
			__SkillsTree.dlgConfirmRetrain.RemoveGUIItems();
			__SkillsTree.dlgConfirmRetrain = null;
		}
		__SkillsTree.btnSave.isEnabled = false;
		__SkillsTree.btnDiscard.isEnabled = false;
		if (__SkillsTree.dlgConfirmSave != null)
		{
			__SkillsTree.dlgConfirmSave.RemoveGUIItems();
		}
		__SkillsTree.dlgConfirmSave = new GuiDialog();
		__SkillsTree.dlgConfirmSave.Create(string.Format(StaticData.Translate("key_skills_tree_save_dialogue"), PlayerObjectPhysics.TALENT_RETRAIN_PRICE), StaticData.Translate("key_skills_tree_btn_save"), StaticData.Translate("key_skills_tree_btn_discard"), __SkillsTree.window);
		__SkillsTree.dlgConfirmSave.OkClicked = new Action<object>(null, __SkillsTree.OnConfirmSaveClickedYes);
		__SkillsTree.dlgConfirmSave.btnOK.eventHandlerParam = prm;
		__SkillsTree.dlgConfirmSave.CancelClicked = new Action<object>(null, __SkillsTree.OnConfirmSaveClickedNo);
		__SkillsTree.dlgConfirmSave.btnCancel.eventHandlerParam = prm;
	}

	private static void ShowSkillInfo(EventHandlerParam prm)
	{
		if (__SkillsTree.dlgConfirmSave != null || __SkillsTree.dlgConfirmRetrain != null)
		{
			return;
		}
		ushort num = (ushort)prm.customData;
		byte item = 0;
		if (__SkillsTree.playerSkillsOnScreen != null && __SkillsTree.playerSkillsOnScreen.ContainsKey(num))
		{
			item = (byte)__SkillsTree.playerSkillsOnScreen.get_Item(num).wantedLevel;
		}
		if (num == PlayerItems.TypeTalentsNanoStorm || num == PlayerItems.TypeTalentsShortCircuit || num == PlayerItems.TypeTalentsDecoy || num == PlayerItems.TypeTalentsTaunt || num == PlayerItems.TypeTalentsFocusFire)
		{
			__SkillsTree.infoLabel1.text = string.Concat(NetworkScript.player.playerBelongings.playerItems.GetSkillDescription(num, item), "\n\n\n\n", NetworkScript.player.playerBelongings.playerItems.GetNeuronlInfo(num));
		}
		else
		{
			__SkillsTree.infoLabel1.text = string.Concat(new string[] { NetworkScript.player.playerBelongings.playerItems.GetSkillDescription(num, item), "\n\n", NetworkScript.player.playerBelongings.playerItems.GetLevelInfo(num), "\n\n", NetworkScript.player.playerBelongings.playerItems.GetNeuronlInfo(num) });
		}
	}

	private enum DrawState
	{
		Disabled,
		Enabled,
		Full,
		FullInProgress
	}

	private class SkillItem
	{
		public ushort skillType;

		public int realLevel;

		public int wantedLevel;

		public SkillItem()
		{
		}
	}

	private class SkillTreeItem
	{
		public ushort skillType;

		public int x;

		public int y;

		public SkillTreeItem()
		{
		}
	}
}