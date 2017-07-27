using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TransferableObjects;
using UnityEngine;

public class NewPoiScreenWindow : GuiWindow
{
	private const float SKILL_BAR_WIDTH = 99f;

	private const float SKILLTREE_BAR_WIDTH = 591f;

	private SelectedExtractionPointTab selectedCategoryTab;

	private SelectedWallet wallet = 1;

	private GuiLabel walletEqValue;

	private GuiLabel walletNovaValue;

	private bool isWaitingRefresh;

	private ExtractionPoint pointInRange;

	private ExtractionPointInfo pointInfo;

	private List<GuiElement> upgradesForDelete = new List<GuiElement>();

	private List<GuiElement> leftsSideForDelete = new List<GuiElement>();

	private List<GuiButton> buttonsOnScreen = new List<GuiButton>();

	private GuiButtonResizeable btnPersonal;

	private GuiLabel statValue1;

	private GuiLabel statValue1Bonus;

	private GuiLabel statValue2;

	private GuiLabel statValue3;

	private GuiLabel statValue3additionalInfo;

	private GuiLabel statValue4;

	private GuiLabel statValue4additionalInfo;

	private GuiLabel statValue5;

	private GuiLabel statValue6;

	private GuiLabel investCooldownLbl;

	private GuiButtonFixed infoBtn;

	private bool isInfoOnScren;

	private SortedList<short, byte> wantedGuardianSkills = new SortedList<short, byte>();

	private bool guardianSkillTreeAccess;

	private byte selectedUnitType;

	private GuiWindow dialogWindow;

	private bool CanInvest
	{
		get
		{
			return NetworkScript.player.lastEpInvestTime.AddSeconds(3) < StaticData.now;
		}
	}

	public NewPoiScreenWindow()
	{
	}

	private void ActivateBoostClicked(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void NewPoiScreenWindow::ActivateBoostClicked(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void ActivateBoostClicked(EventHandlerParam)
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

	private bool CanBuildUnit(byte unitType, int population)
	{
		bool flag;
		bool flag1;
		if (unitType != 51 && unitType != 52 && unitType != 53 && unitType != 54 && unitType != 55)
		{
			if (this.pointInRange.vulnerableEndTime <= StaticData.now)
			{
				flag1 = this.pointInRange.currentPopulationTowers + population <= this.pointInRange.populationTowers;
			}
			else
			{
				flag1 = (this.pointInRange.currentPopulationTowers + population > this.pointInRange.populationTowers ? false : population <= this.pointInRange.availablePopulationTowers);
			}
			return flag1;
		}
		if (this.pointInRange.vulnerableEndTime <= StaticData.now)
		{
			flag = this.pointInRange.currentPopulationAliens + population <= this.pointInRange.populationAliens;
		}
		else
		{
			flag = (this.pointInRange.currentPopulationAliens + population > this.pointInRange.populationAliens ? false : population <= this.pointInRange.availablePopulationAliens);
		}
		return flag;
	}

	private bool CanPayFor(SelectedCurrency currency, int price)
	{
		if (this.wallet != 2)
		{
			if (currency == 1)
			{
				return NetworkScript.player.playerBelongings.playerItems.get_Nova() >= (long)price;
			}
			if (currency != 2)
			{
				return false;
			}
			return NetworkScript.player.playerBelongings.playerItems.get_Equilibrium() >= (long)price;
		}
		if (currency == 1)
		{
			return (!NetworkScript.player.guildMember.rank.canBank ? false : NetworkScript.player.guild.bankNova >= (long)price);
		}
		if (currency != 2)
		{
			return false;
		}
		return (!NetworkScript.player.guildMember.rank.canBank ? false : NetworkScript.player.guild.bankEquilib >= (long)price);
	}

	private void ClearContent()
	{
		this.ClearUpgradesContent();
		foreach (GuiElement guiElement in this.leftsSideForDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.leftsSideForDelete.Clear();
	}

	private void ClearUpgradesContent()
	{
		foreach (GuiElement guiElement in this.upgradesForDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.upgradesForDelete.Clear();
	}

	public override void Create()
	{
		base.SetBackgroundTexture("PoiScreenWindow", "frame");
		base.PutToCenter();
		this.zOrder = 210;
		this.isHidden = false;
		this.pointInRange = NetworkScript.player.shipScript.extractionPointInRange;
		if (this.pointInRange == null)
		{
			this.pointInfo = null;
			return;
		}
		this.pointInfo = Enumerable.First<ExtractionPointInfo>(Enumerable.Where<ExtractionPointInfo>(StaticData.allExtractionPoints, new Func<ExtractionPointInfo, bool>(this, (ExtractionPointInfo t) => t.id == this.pointInRange.pointId)));
		this.infoBtn = new GuiButtonFixed();
		this.infoBtn.SetTexture("PoiScreenWindow", "btn-info");
		this.infoBtn.X = 0f;
		this.infoBtn.Y = 4f;
		this.infoBtn.Caption = string.Empty;
		this.infoBtn.Clicked = null;
		base.AddGuiElement(this.infoBtn);
		this.CreateLeftSide();
	}

	private void CreateContributorsList()
	{
		// 
		// Current member / type: System.Void NewPoiScreenWindow::CreateContributorsList()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void CreateContributorsList()
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

	private void CreateDamageReductionBoost(int index)
	{
		float single = 95f;
		bool flag = this.pointInRange.damageReductionBoostEndTime > StaticData.now;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("PoiScreenWindow", "addOnsRowNormal");
		guiTexture.X = 210f;
		guiTexture.Y = 50f + single * (float)index;
		base.AddGuiElement(guiTexture);
		this.upgradesForDelete.Add(guiTexture);
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("PoiScreenWindow", "alien-avatar");
		rect.boundries = new Rect(222f, 78f + single * (float)index, 60f, 60f);
		base.AddGuiElement(rect);
		this.upgradesForDelete.Add(rect);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(217f, 52f + single * (float)index, 300f, 18f),
			Alignment = 3,
			FontSize = 14,
			TextColor = GuiNewStyleBar.blueColor,
			text = StaticData.Translate("key_poi_damage_reduction_boost_name").ToUpper()
		};
		base.AddGuiElement(guiLabel);
		this.upgradesForDelete.Add(guiLabel);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("Targeting", this.pointInRange.assetName);
		guiTexture1.boundries = new Rect(223f, 79f + single * (float)index, 58f, 58f);
		base.AddGuiElement(guiTexture1);
		this.upgradesForDelete.Add(guiTexture1);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(298f, 82f + single * (float)index, 500f, 26f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = string.Format(StaticData.Translate("key_poi_damage_reduction_boost_description"), 50f)
		};
		base.AddGuiElement(guiLabel1);
		this.upgradesForDelete.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(298f, 120f + single * (float)index, 170f, 16f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = StaticData.Translate("key_poi_damage_reduction_boost_duration_lbl")
		};
		base.AddGuiElement(guiLabel2);
		this.upgradesForDelete.Add(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(298f, 120f + single * (float)index, 170f, 16f),
			Alignment = 5,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = string.Format(StaticData.Translate("key_poi_damage_reduction_boost_duration_value"), 10)
		};
		base.AddGuiElement(guiLabel3);
		this.upgradesForDelete.Add(guiLabel3);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			boundries = new Rect(475f, 120f + single * (float)index, 170f, 16f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = (!flag ? StaticData.Translate("key_poi_damage_reduction_boost_deactive_lbl") : StaticData.Translate("key_poi_damage_reduction_boost_active_lbl"))
		};
		base.AddGuiElement(guiLabel4);
		this.upgradesForDelete.Add(guiLabel4);
		GuiLabel guiLabel5 = new GuiLabel()
		{
			boundries = new Rect(475f, 120f + single * (float)index, 170f, 16f),
			Alignment = 5,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = string.Empty
		};
		base.AddGuiElement(guiLabel5);
		this.upgradesForDelete.Add(guiLabel5);
		if (flag)
		{
			TimeSpan timeSpan = this.pointInRange.damageReductionBoostEndTime - StaticData.now;
			long totalSeconds = (long)timeSpan.get_TotalSeconds();
			if (totalSeconds <= (long)60)
			{
				guiLabel5.text = string.Format(StaticData.Translate("key_profile_screen_playtime_value"), 0, 0, 1);
			}
			else
			{
				guiLabel5.text = string.Format(StaticData.Translate("key_profile_screen_playtime_value"), totalSeconds / (long)86400, totalSeconds / (long)3600 % (long)24, totalSeconds / (long)60 % (long)60);
			}
			guiTexture.SetTexture("PoiScreenWindow", "addOnsRowMax");
		}
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.SetNovaBtn();
		guiButtonResizeable.X = 817f;
		guiButtonResizeable.Y = 59f + single * (float)index;
		guiButtonResizeable.boundries.set_width(85f);
		int num = 200 * this.pointInRange.magicCoefficient;
		guiButtonResizeable.Caption = num.ToString("N0");
		guiButtonResizeable.FontSize = 14;
		guiButtonResizeable.Alignment = 4;
		guiButtonResizeable.isEnabled = (!this.CanInvest || this.isWaitingRefresh || !this.CanPayFor(1, 200 * this.pointInRange.magicCoefficient) || !(this.pointInRange.vulnerableEndTime > StaticData.now) ? false : this.pointInRange.damageReductionBoostEndTime < StaticData.now);
		EventHandlerParam eventHandlerParam = new EventHandlerParam();
		NewPoiScreenWindow.PoIParams poIParam = new NewPoiScreenWindow.PoIParams()
		{
			currency = 1
		};
		eventHandlerParam.customData = poIParam;
		guiButtonResizeable.eventHandlerParam = eventHandlerParam;
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, NewPoiScreenWindow.ActivateBoostClicked);
		base.AddGuiElement(guiButtonResizeable);
		this.upgradesForDelete.Add(guiButtonResizeable);
		this.buttonsOnScreen.Add(guiButtonResizeable);
		GuiButtonResizeable str = new GuiButtonResizeable();
		str.SetEqBtn();
		str.X = 817f;
		str.Y = 98f + single * (float)index;
		str.boundries.set_width(85f);
		int num1 = 200 * this.pointInRange.magicCoefficient * 2;
		str.Caption = num1.ToString("N0");
		str.FontSize = 14;
		str.Alignment = 4;
		str.isEnabled = (!this.CanInvest || this.isWaitingRefresh || !this.CanPayFor(2, 200 * this.pointInRange.magicCoefficient * 2) || !(this.pointInRange.vulnerableEndTime > StaticData.now) ? false : this.pointInRange.damageReductionBoostEndTime < StaticData.now);
		eventHandlerParam = new EventHandlerParam();
		poIParam = new NewPoiScreenWindow.PoIParams()
		{
			currency = 2
		};
		eventHandlerParam.customData = poIParam;
		str.eventHandlerParam = eventHandlerParam;
		str.Clicked = new Action<EventHandlerParam>(this, NewPoiScreenWindow.ActivateBoostClicked);
		base.AddGuiElement(str);
		this.upgradesForDelete.Add(str);
		this.buttonsOnScreen.Add(str);
	}

	private void CreateLeftSide()
	{
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("PoiScreenWindow", "frameLeftSeparator");
		guiTexture.X = 0f;
		guiTexture.Y = 0f;
		base.AddGuiElement(guiTexture);
		this.leftsSideForDelete.Add(guiTexture);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(147f, 8f, 618f, 36f),
			Alignment = 4,
			text = StaticData.Translate(this.pointInRange.name).ToUpper(),
			FontSize = 24,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel);
		this.leftsSideForDelete.Add(guiLabel);
		string empty = string.Empty;
		if (this.pointInRange.ownerFraction == 1)
		{
			empty = "logoVindexis";
		}
		else if (this.pointInRange.ownerFraction == 2)
		{
			empty = "logoRegia";
		}
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("WarScreenWindow", empty);
		rect.boundries = new Rect(10f, 65f, 194f, 138f);
		base.AddGuiElement(rect);
		this.leftsSideForDelete.Add(rect);
		this.CreateStatsSection();
		this.CreateWallet();
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.SetTexture("PoiScreenWindow", "tab_");
		guiButtonResizeable.X = 16f;
		guiButtonResizeable.Y = 233f;
		guiButtonResizeable.boundries.set_width(180f);
		guiButtonResizeable.groupId = 1;
		guiButtonResizeable.behaviourKeepClicked = true;
		guiButtonResizeable.Caption = StaticData.Translate("key_ep_screen_btn_contribution");
		guiButtonResizeable.FontSize = 12;
		guiButtonResizeable.Alignment = 4;
		guiButtonResizeable.SetRegularFont();
		guiButtonResizeable.SetColor(GuiNewStyleBar.blueColor);
		guiButtonResizeable.eventHandlerParam.customData = SelectedExtractionPointTab.Contribution;
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, NewPoiScreenWindow.OnSelectCategoryClicked);
		guiButtonResizeable.IsClicked = this.selectedCategoryTab == SelectedExtractionPointTab.Contribution;
		base.AddGuiElement(guiButtonResizeable);
		this.leftsSideForDelete.Add(guiButtonResizeable);
		GuiButtonResizeable y = new GuiButtonResizeable();
		y.SetTexture("PoiScreenWindow", "tab_");
		y.X = 16f;
		y.Y = guiButtonResizeable.Y + 35f;
		y.boundries.set_width(180f);
		y.groupId = 1;
		y.behaviourKeepClicked = true;
		y.Caption = StaticData.Translate("key_ep_screen_btn_addons");
		y.FontSize = 12;
		y.Alignment = 4;
		y.SetRegularFont();
		y.SetColor(GuiNewStyleBar.blueColor);
		y.eventHandlerParam.customData = SelectedExtractionPointTab.AddOns;
		y.Clicked = new Action<EventHandlerParam>(this, NewPoiScreenWindow.OnSelectCategoryClicked);
		y.IsClicked = this.selectedCategoryTab == SelectedExtractionPointTab.AddOns;
		base.AddGuiElement(y);
		this.leftsSideForDelete.Add(y);
		GuiButtonResizeable action = new GuiButtonResizeable();
		action.SetTexture("PoiScreenWindow", "tab_");
		action.X = 16f;
		action.Y = y.Y + 35f;
		action.boundries.set_width(180f);
		action.groupId = 1;
		action.behaviourKeepClicked = true;
		action.Caption = StaticData.Translate("key_ep_screen_btn_aliens");
		action.FontSize = 12;
		action.Alignment = 4;
		action.SetRegularFont();
		action.SetColor(GuiNewStyleBar.blueColor);
		action.eventHandlerParam.customData = SelectedExtractionPointTab.Aliens;
		action.Clicked = new Action<EventHandlerParam>(this, NewPoiScreenWindow.OnSelectCategoryClicked);
		action.IsClicked = this.selectedCategoryTab == SelectedExtractionPointTab.Aliens;
		base.AddGuiElement(action);
		this.leftsSideForDelete.Add(action);
		GuiButtonResizeable guiButtonResizeable1 = new GuiButtonResizeable();
		guiButtonResizeable1.SetTexture("PoiScreenWindow", "tab_");
		guiButtonResizeable1.X = 16f;
		guiButtonResizeable1.Y = action.Y + 35f;
		guiButtonResizeable1.boundries.set_width(180f);
		guiButtonResizeable1.groupId = 1;
		guiButtonResizeable1.behaviourKeepClicked = true;
		guiButtonResizeable1.Caption = StaticData.Translate("key_ep_screen_btn_towers");
		guiButtonResizeable1.FontSize = 12;
		guiButtonResizeable1.Alignment = 4;
		guiButtonResizeable1.SetRegularFont();
		guiButtonResizeable1.SetColor(GuiNewStyleBar.blueColor);
		guiButtonResizeable1.eventHandlerParam.customData = SelectedExtractionPointTab.Towers;
		guiButtonResizeable1.Clicked = new Action<EventHandlerParam>(this, NewPoiScreenWindow.OnSelectCategoryClicked);
		guiButtonResizeable1.IsClicked = this.selectedCategoryTab == SelectedExtractionPointTab.Towers;
		base.AddGuiElement(guiButtonResizeable1);
		this.leftsSideForDelete.Add(guiButtonResizeable1);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(23f, 401f, 170f, 16f),
			Alignment = 3,
			text = StaticData.Translate("key_ep_screen_wallet_lbl"),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel1);
		this.leftsSideForDelete.Add(guiLabel1);
	}

	private void CreateSingleUpgrade(byte upgradeType, int index)
	{
		// 
		// Current member / type: System.Void NewPoiScreenWindow::CreateSingleUpgrade(System.Byte,System.Int32)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void CreateSingleUpgrade(System.Byte,System.Int32)
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в Telerik.JustDecompiler.Decompiler.TypeInference.TypeInferer.(Expression ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\TypeInferer.cs:строка 97
		//    в Telerik.JustDecompiler.Decompiler.TypeInference.TypeInferer.(IList`1 , Expression ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\TypeInferer.cs:строка 70
		//    в Telerik.JustDecompiler.Decompiler.TypeInference.TypeInferer.() в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\TypeInferer.cs:строка 56
		//    в Telerik.JustDecompiler.Decompiler.TypeInference.TypeInferer.() в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\TypeInferer.cs:строка 331
		//    в Telerik.JustDecompiler.Decompiler.ExpressionDecompilerStep.(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\ExpressionDecompilerStep.cs:строка 88
		//    в ..(MethodBody ,  , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 88
		//    в ..(MethodBody , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 70
		//    в Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 95
		//    в Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 72
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\RebuildLambdaExpressions.cs:строка 58
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 89
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 320
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 608
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\RebuildLambdaExpressions.cs:строка 118
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 125
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 320
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\RebuildLambdaExpressions.cs:строка 124
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 87
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 320
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\RebuildLambdaExpressions.cs:строка 124
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 87
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(BinaryExpression ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 527
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 97
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 381
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 59
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(IfStatement ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 359
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 55
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(IfStatement ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 359
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 55
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\RebuildLambdaExpressions.cs:строка 130
		//    в ..(MethodBody ,  , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 88
		//    в ..(MethodBody , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 70
		//    в Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 95
		//    в Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 58
		//    в ..(ILanguage , MethodDefinition ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:строка 117
		// 
		// mailto: JustDecompilePublicFeedback@telerik.com

	}

	private void CreateStatsSection()
	{
		// 
		// Current member / type: System.Void NewPoiScreenWindow::CreateStatsSection()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void CreateStatsSection()
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в Telerik.JustDecompiler.Decompiler.TypeInference.TypeInferer.(Expression ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\TypeInferer.cs:строка 97
		//    в Telerik.JustDecompiler.Decompiler.TypeInference.TypeInferer.(IList`1 , Expression ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\TypeInferer.cs:строка 70
		//    в Telerik.JustDecompiler.Decompiler.TypeInference.TypeInferer.() в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\TypeInferer.cs:строка 56
		//    в Telerik.JustDecompiler.Decompiler.TypeInference.TypeInferer.() в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\TypeInferer.cs:строка 331
		//    в Telerik.JustDecompiler.Decompiler.ExpressionDecompilerStep.(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\ExpressionDecompilerStep.cs:строка 88
		//    в ..(MethodBody ,  , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 88
		//    в ..(MethodBody , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 70
		//    в Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 95
		//    в Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 58
		//    в ..(ILanguage , MethodDefinition ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:строка 117
		// 
		// mailto: JustDecompilePublicFeedback@telerik.com

	}

	private void CreateWallet()
	{
		bool flag = (NetworkScript.player.guild == null || NetworkScript.player.guildMember == null || NetworkScript.player.guildMember.rank == null ? false : NetworkScript.player.guildMember.rank.canBank);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("PoiScreenWindow", "wallet");
		guiTexture.X = 15f;
		guiTexture.Y = 459f;
		base.AddGuiElement(guiTexture);
		this.leftsSideForDelete.Add(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("FrameworkGUI", "res_equilibrium");
		guiTexture1.X = 27f;
		guiTexture1.Y = 459f;
		base.AddGuiElement(guiTexture1);
		this.leftsSideForDelete.Add(guiTexture1);
		this.walletEqValue = new GuiLabel()
		{
			boundries = new Rect(46f, 461f, 140f, 16f),
			Alignment = 5,
			text = "eqValue",
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.walletEqValue);
		this.leftsSideForDelete.Add(this.walletEqValue);
		GuiTexture guiTexture2 = new GuiTexture();
		guiTexture2.SetTexture("FrameworkGUI", "res_nova");
		guiTexture2.X = 27f;
		guiTexture2.Y = 488f;
		base.AddGuiElement(guiTexture2);
		this.leftsSideForDelete.Add(guiTexture2);
		this.walletNovaValue = new GuiLabel()
		{
			boundries = new Rect(46f, 490f, 140f, 16f),
			Alignment = 5,
			text = "novaValue",
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.walletNovaValue);
		this.leftsSideForDelete.Add(this.walletNovaValue);
		this.btnPersonal = new GuiButtonResizeable();
		this.btnPersonal.SetTexture("PoiScreenWindow", "tab_");
		this.btnPersonal.X = 16f;
		this.btnPersonal.Y = 423f;
		this.btnPersonal.boundries.set_width(85f);
		this.btnPersonal.groupId = 2;
		this.btnPersonal.behaviourKeepClicked = true;
		this.btnPersonal.Caption = StaticData.Translate("key_ep_screen_btn_own_money");
		this.btnPersonal.FontSize = 12;
		this.btnPersonal.Alignment = 4;
		this.btnPersonal.SetRegularFont();
		this.btnPersonal.SetColor(GuiNewStyleBar.blueColor);
		this.btnPersonal.eventHandlerParam.customData = (SelectedWallet)1;
		this.btnPersonal.Clicked = new Action<EventHandlerParam>(this, NewPoiScreenWindow.OnWalletCategoryClicked);
		this.btnPersonal.IsClicked = this.wallet == 1;
		base.AddGuiElement(this.btnPersonal);
		this.leftsSideForDelete.Add(this.btnPersonal);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.SetTexture("PoiScreenWindow", "tab_");
		guiButtonResizeable.X = 111f;
		guiButtonResizeable.Y = 423f;
		guiButtonResizeable.boundries.set_width(85f);
		guiButtonResizeable.groupId = 2;
		guiButtonResizeable.behaviourKeepClicked = true;
		guiButtonResizeable.Caption = StaticData.Translate("key_ep_screen_btn_guild_money");
		guiButtonResizeable.FontSize = 12;
		guiButtonResizeable.Alignment = 4;
		guiButtonResizeable.SetRegularFont();
		guiButtonResizeable.SetColor(GuiNewStyleBar.blueColor);
		guiButtonResizeable.eventHandlerParam.customData = (SelectedWallet)2;
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, NewPoiScreenWindow.OnWalletCategoryClicked);
		guiButtonResizeable.isEnabled = flag;
		guiButtonResizeable.IsClicked = this.wallet == 2;
		base.AddGuiElement(guiButtonResizeable);
		this.leftsSideForDelete.Add(guiButtonResizeable);
	}

	private void DrawMeInContributionList(Contributor c)
	{
		float single = 470f;
		Color color = GuiNewStyleBar.orangeColor;
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(280f, single, 225f, 18f),
			Alignment = 3,
			TextColor = color,
			FontSize = 12,
			text = c.displayName
		};
		base.AddGuiElement(guiLabel);
		this.upgradesForDelete.Add(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(519f, single, 60f, 18f),
			Alignment = 4,
			TextColor = color,
			FontSize = 12,
			text = c.battleContribution.ToString("N0")
		};
		base.AddGuiElement(guiLabel1);
		this.upgradesForDelete.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(582f, single, 60f, 18f),
			Alignment = 4,
			TextColor = color,
			FontSize = 12,
			text = c.novaContribution.ToString("N0")
		};
		base.AddGuiElement(guiLabel2);
		this.upgradesForDelete.Add(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(645f, single, 60f, 18f),
			Alignment = 4,
			TextColor = color,
			FontSize = 12,
			text = c.viralContribution.ToString("N0")
		};
		base.AddGuiElement(guiLabel3);
		this.upgradesForDelete.Add(guiLabel3);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			boundries = new Rect(718f, single, 90f, 18f),
			Alignment = 4,
			TextColor = color,
			FontSize = 12,
			text = c.tottalContribution.ToString("N0")
		};
		base.AddGuiElement(guiLabel4);
		this.upgradesForDelete.Add(guiLabel4);
		GuiLabel guiLabel5 = new GuiLabel()
		{
			boundries = new Rect(810f, single, 90f, 18f),
			Alignment = 4,
			TextColor = color,
			FontSize = 12,
			text = string.Format(StaticData.Translate("universe_map_fraction_income_value"), c.incomeBonus)
		};
		base.AddGuiElement(guiLabel5);
		this.upgradesForDelete.Add(guiLabel5);
	}

	private bool ItsMe(Contributor c)
	{
		string str = NetworkScript.player.vessel.playerName;
		return c.displayName == str;
	}

	public override void OnClose()
	{
		playWebGame.udp.ExecuteCommand(166, null);
	}

	private void OnDiscardBtnClicked(object prm)
	{
		this.wantedGuardianSkills.Clear();
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = this.selectedUnitType
		};
		this.OnGuardianClick(eventHandlerParam);
	}

	private void OnEpInfoClicked(object prm)
	{
		this.isInfoOnScren = true;
		this.ClearContent();
		this.infoBtn.SetTexture("PoiScreenWindow", "btnInfoBack");
		this.infoBtn.eventHandlerParam.customData = this.selectedCategoryTab;
		this.infoBtn.Clicked = new Action<EventHandlerParam>(this, NewPoiScreenWindow.OnSelectCategoryClicked);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(147f, 8f, 618f, 36f),
			Alignment = 4,
			text = StaticData.Translate("key_new_universe_map_lbl_information"),
			FontSize = 20,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel);
		this.upgradesForDelete.Add(guiLabel);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("PoiScreenWindow", "epInfo");
		guiTexture.X = 15f;
		guiTexture.Y = 53f;
		base.AddGuiElement(guiTexture);
		this.upgradesForDelete.Add(guiTexture);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(35f, 70f, 845f, 80f),
			text = string.Format(StaticData.Translate("key_ep_info_screen_description"), 5),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel1);
		this.upgradesForDelete.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(292f, 185f, 600f, 18f),
			text = StaticData.Translate("key_ep_info_screen_paragraph_one_title"),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel2);
		this.upgradesForDelete.Add(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(292f, 207f, 600f, 70f),
			text = StaticData.Translate("key_ep_info_screen_paragraph_one_info"),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel3);
		this.upgradesForDelete.Add(guiLabel3);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			boundries = new Rect(292f, 296f, 600f, 18f),
			text = StaticData.Translate("key_ep_info_screen_paragraph_two_title"),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel4);
		this.upgradesForDelete.Add(guiLabel4);
		GuiLabel guiLabel5 = new GuiLabel()
		{
			boundries = new Rect(292f, 318f, 600f, 70f),
			text = StaticData.Translate("key_ep_info_screen_paragraph_two_info"),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel5);
		this.upgradesForDelete.Add(guiLabel5);
		GuiLabel guiLabel6 = new GuiLabel()
		{
			boundries = new Rect(292f, 405f, 600f, 92f),
			text = string.Format(StaticData.Translate("key_ep_info_screen_paragraph_three_info"), 5000 * this.pointInfo.magicCoefficient),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel6);
		this.upgradesForDelete.Add(guiLabel6);
	}

	private void OnGuardianClick(EventHandlerParam prm)
	{
		NewPoiScreenWindow.<OnGuardianClick>c__AnonStorey51 variable = null;
		string str;
		this.selectedUnitType = (byte)prm.customData;
		byte num = 0;
		bool flag = false;
		float single = 370f;
		float single1 = 570f;
		this.ClearUpgradesContent();
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("PoiScreenWindow", "skillTreeFrame");
		guiTexture.X = 205f;
		guiTexture.Y = 50f;
		base.AddGuiElement(guiTexture);
		this.upgradesForDelete.Add(guiTexture);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("PoiScreenWindow", "btnBack");
		guiButtonFixed.X = 743f;
		guiButtonFixed.Y = 94f;
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.eventHandlerParam.customData = this.selectedCategoryTab;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, NewPoiScreenWindow.OnSelectCategoryClicked);
		base.AddGuiElement(guiButtonFixed);
		this.upgradesForDelete.Add(guiButtonFixed);
		byte num1 = this.selectedUnitType;
		switch (num1)
		{
			case 51:
			{
				num = this.pointInRange.upgradesAlien1;
				break;
			}
			case 52:
			{
				num = this.pointInRange.upgradesAlien2;
				break;
			}
			case 53:
			{
				num = this.pointInRange.upgradesAlien3;
				break;
			}
			case 54:
			{
				num = this.pointInRange.upgradesAlien4;
				break;
			}
			case 55:
			{
				num = this.pointInRange.upgradesAlien5;
				break;
			}
			default:
			{
				switch (num1)
				{
					case 101:
					{
						num = this.pointInRange.upgradesTurret1;
						break;
					}
					case 102:
					{
						num = this.pointInRange.upgradesTurret2;
						break;
					}
					case 103:
					{
						num = this.pointInRange.upgradesTurret3;
						break;
					}
					case 104:
					{
						num = this.pointInRange.upgradesTurret4;
						break;
					}
					case 105:
					{
						num = this.pointInRange.upgradesTurret5;
						break;
					}
				}
				break;
			}
		}
		ExtractionPointUnit extractionPointUnit = Enumerable.First<ExtractionPointUnit>(Enumerable.Where<ExtractionPointUnit>(this.pointInfo.allUnits, new Func<ExtractionPointUnit, bool>(variable, (ExtractionPointUnit t) => (t.unitType != this.<>f__this.selectedUnitType ? false : t.upgrade == this.upgradeLevel))));
		ExtractionPointUnit extractionPointUnit1 = null;
		flag = num == 5;
		extractionPointUnit1 = (!flag ? Enumerable.First<ExtractionPointUnit>(Enumerable.Where<ExtractionPointUnit>(this.pointInfo.allUnits, new Func<ExtractionPointUnit, bool>(variable, (ExtractionPointUnit t) => (t.unitType != this.<>f__this.selectedUnitType ? false : t.upgrade == this.upgradeLevel + 1)))) : extractionPointUnit);
		for (int i = 0; i < 5; i++)
		{
			GuiTexture rect = new GuiTexture();
			rect.SetTexture("PoiScreenWindow", "progressbar-full");
			rect.boundries = new Rect((float)(762 + i * 19), 58f, 15f, 15f);
			base.AddGuiElement(rect);
			this.upgradesForDelete.Add(rect);
			if (i >= extractionPointUnit.upgrade)
			{
				rect.SetTexture("PoiScreenWindow", "progressbar-empty");
			}
			else
			{
				rect.SetTexture("PoiScreenWindow", "progressbar-full");
			}
		}
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("PoiScreenWindow", "avatar");
		guiTexture1.boundries = new Rect(228f, 90f, 100f, 100f);
		base.AddGuiElement(guiTexture1);
		this.upgradesForDelete.Add(guiTexture1);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(216f, 54f, 325f, 19f),
			Alignment = 3,
			FontSize = 14,
			TextColor = GuiNewStyleBar.blueColor,
			text = StaticData.Translate(extractionPointUnit.name).ToUpper()
		};
		base.AddGuiElement(guiLabel);
		this.upgradesForDelete.Add(guiLabel);
		GuiTexture rect1 = new GuiTexture();
		rect1.SetTexture("Targeting", extractionPointUnit.assetName);
		rect1.boundries = new Rect(229f, 91f, 98f, 98f);
		base.AddGuiElement(rect1);
		this.upgradesForDelete.Add(rect1);
		GuiTexture guiTexture2 = new GuiTexture();
		guiTexture2.SetTexture("PoiScreenWindow", "avatar-number");
		guiTexture2.boundries = new Rect(220f, 83f, 20f, 20f);
		base.AddGuiElement(guiTexture2);
		this.upgradesForDelete.Add(guiTexture2);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(220f, 83f, 20f, 20f),
			Alignment = 4,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12
		};
		int item = this.pointInRange.defendersCountByKind.get_Item((int)extractionPointUnit.unitType);
		guiLabel1.text = item.ToString();
		base.AddGuiElement(guiLabel1);
		this.upgradesForDelete.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(single, 79f, 170f, 19f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = StaticData.Translate("ep_screen_unit_damage")
		};
		base.AddGuiElement(guiLabel2);
		this.upgradesForDelete.Add(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(single, 79f, 170f, 19f),
			Alignment = 5,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = extractionPointUnit.damage.ToString("##,##0")
		};
		base.AddGuiElement(guiLabel3);
		this.upgradesForDelete.Add(guiLabel3);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			boundries = new Rect(single, 99f, 170f, 19f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = StaticData.Translate("ep_screen_unit_cooldown")
		};
		base.AddGuiElement(guiLabel4);
		this.upgradesForDelete.Add(guiLabel4);
		GuiLabel str1 = new GuiLabel()
		{
			boundries = new Rect(single, 99f, 170f, 19f),
			Alignment = 5,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12
		};
		float single2 = (float)extractionPointUnit.cooldown * 0.001f;
		str1.text = single2.ToString("#0.00");
		base.AddGuiElement(str1);
		this.upgradesForDelete.Add(str1);
		GuiLabel guiLabel5 = new GuiLabel()
		{
			boundries = new Rect(single, 119f, 170f, 19f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = StaticData.Translate("ep_screen_unit_hitpoints")
		};
		base.AddGuiElement(guiLabel5);
		this.upgradesForDelete.Add(guiLabel5);
		GuiLabel guiLabel6 = new GuiLabel()
		{
			boundries = new Rect(single, 119f, 170f, 19f),
			Alignment = 5,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = extractionPointUnit.hitPoints.ToString("##,##0")
		};
		base.AddGuiElement(guiLabel6);
		this.upgradesForDelete.Add(guiLabel6);
		GuiLabel guiLabel7 = new GuiLabel()
		{
			boundries = new Rect(single, 139f, 170f, 19f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = StaticData.Translate("ep_screen_unit_population")
		};
		base.AddGuiElement(guiLabel7);
		this.upgradesForDelete.Add(guiLabel7);
		GuiLabel guiLabel8 = new GuiLabel()
		{
			boundries = new Rect(single, 139f, 170f, 19f),
			Alignment = 5,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = extractionPointUnit.population.ToString("##,##0")
		};
		base.AddGuiElement(guiLabel8);
		this.upgradesForDelete.Add(guiLabel8);
		GuiLabel guiLabel9 = new GuiLabel()
		{
			boundries = new Rect(single, 159f, 170f, 19f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = StaticData.Translate("ep_screen_unit_range")
		};
		base.AddGuiElement(guiLabel9);
		this.upgradesForDelete.Add(guiLabel9);
		GuiLabel guiLabel10 = new GuiLabel()
		{
			boundries = new Rect(single, 159f, 170f, 19f),
			Alignment = 5,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = extractionPointUnit.range.ToString("##,##0")
		};
		base.AddGuiElement(guiLabel10);
		this.upgradesForDelete.Add(guiLabel10);
		GuiLabel guiLabel11 = new GuiLabel()
		{
			boundries = new Rect(single, 179f, 170f, 19f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = StaticData.Translate("ep_screen_unit_penetration")
		};
		base.AddGuiElement(guiLabel11);
		this.upgradesForDelete.Add(guiLabel11);
		GuiLabel guiLabel12 = new GuiLabel()
		{
			boundries = new Rect(single, 179f, 170f, 19f),
			Alignment = 5,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = extractionPointUnit.penetration.ToString("##,##0")
		};
		base.AddGuiElement(guiLabel12);
		this.upgradesForDelete.Add(guiLabel12);
		GuiLabel guiLabel13 = new GuiLabel()
		{
			boundries = new Rect(single1, 79f, 165f, 19f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = (!flag ? StaticData.Translate("ep_screen_fortify_update_to") : string.Empty)
		};
		base.AddGuiElement(guiLabel13);
		this.upgradesForDelete.Add(guiLabel13);
		GuiLabel guiLabel14 = new GuiLabel()
		{
			boundries = new Rect(single1, 79f, 165f, 19f),
			Alignment = 5,
			TextColor = (!flag ? GuiNewStyleBar.blueColor : GuiNewStyleBar.greenColor),
			FontSize = 12,
			text = (!flag ? extractionPointUnit1.damage.ToString("N0") : StaticData.Translate("key_ep_screen_upgrade_level_max"))
		};
		base.AddGuiElement(guiLabel14);
		this.upgradesForDelete.Add(guiLabel14);
		GuiLabel guiLabel15 = new GuiLabel()
		{
			boundries = new Rect(single1, 99f, 165f, 19f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = (!flag ? StaticData.Translate("ep_screen_fortify_update_to") : string.Empty)
		};
		base.AddGuiElement(guiLabel15);
		this.upgradesForDelete.Add(guiLabel15);
		GuiLabel guiLabel16 = new GuiLabel()
		{
			boundries = new Rect(single1, 99f, 165f, 19f),
			Alignment = 5,
			TextColor = (!flag ? GuiNewStyleBar.blueColor : GuiNewStyleBar.greenColor),
			FontSize = 12
		};
		GuiLabel guiLabel17 = guiLabel16;
		if (!flag)
		{
			float single3 = (float)extractionPointUnit1.cooldown * 0.001f;
			str = single3.ToString("#0.00");
		}
		else
		{
			str = StaticData.Translate("key_ep_screen_upgrade_level_max");
		}
		guiLabel17.text = str;
		base.AddGuiElement(guiLabel16);
		this.upgradesForDelete.Add(guiLabel16);
		GuiLabel guiLabel18 = new GuiLabel()
		{
			boundries = new Rect(single1, 119f, 165f, 19f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = (!flag ? StaticData.Translate("ep_screen_fortify_update_to") : string.Empty)
		};
		base.AddGuiElement(guiLabel18);
		this.upgradesForDelete.Add(guiLabel18);
		GuiLabel guiLabel19 = new GuiLabel()
		{
			boundries = new Rect(single1, 119f, 165f, 19f),
			Alignment = 5,
			TextColor = (!flag ? GuiNewStyleBar.blueColor : GuiNewStyleBar.greenColor),
			FontSize = 12,
			text = (!flag ? extractionPointUnit1.hitPoints.ToString("N0") : StaticData.Translate("key_ep_screen_upgrade_level_max"))
		};
		base.AddGuiElement(guiLabel19);
		this.upgradesForDelete.Add(guiLabel19);
		GuiLabel guiLabel20 = new GuiLabel()
		{
			boundries = new Rect(single1, 139f, 165f, 19f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = (!flag ? StaticData.Translate("ep_screen_fortify_update_to") : string.Empty)
		};
		base.AddGuiElement(guiLabel20);
		this.upgradesForDelete.Add(guiLabel20);
		GuiLabel guiLabel21 = new GuiLabel()
		{
			boundries = new Rect(single1, 139f, 165f, 19f),
			Alignment = 5,
			TextColor = (!flag ? GuiNewStyleBar.blueColor : GuiNewStyleBar.greenColor),
			FontSize = 12,
			text = (!flag ? extractionPointUnit1.population.ToString("N0") : StaticData.Translate("key_ep_screen_upgrade_level_max"))
		};
		base.AddGuiElement(guiLabel21);
		this.upgradesForDelete.Add(guiLabel21);
		GuiLabel guiLabel22 = new GuiLabel()
		{
			boundries = new Rect(single1, 159f, 165f, 19f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = (!flag ? StaticData.Translate("ep_screen_fortify_update_to") : string.Empty)
		};
		base.AddGuiElement(guiLabel22);
		this.upgradesForDelete.Add(guiLabel22);
		GuiLabel guiLabel23 = new GuiLabel()
		{
			boundries = new Rect(single1, 159f, 165f, 19f),
			Alignment = 5,
			TextColor = (!flag ? GuiNewStyleBar.blueColor : GuiNewStyleBar.greenColor),
			FontSize = 12,
			text = (!flag ? extractionPointUnit1.range.ToString("N0") : StaticData.Translate("key_ep_screen_upgrade_level_max"))
		};
		base.AddGuiElement(guiLabel23);
		this.upgradesForDelete.Add(guiLabel23);
		GuiLabel guiLabel24 = new GuiLabel()
		{
			boundries = new Rect(single1, 179f, 165f, 19f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = (!flag ? StaticData.Translate("ep_screen_fortify_update_to") : string.Empty)
		};
		base.AddGuiElement(guiLabel24);
		this.upgradesForDelete.Add(guiLabel24);
		GuiLabel guiLabel25 = new GuiLabel()
		{
			boundries = new Rect(single1, 179f, 165f, 19f),
			Alignment = 5,
			TextColor = (!flag ? GuiNewStyleBar.blueColor : GuiNewStyleBar.greenColor),
			FontSize = 12,
			text = (!flag ? extractionPointUnit1.penetration.ToString("N0") : StaticData.Translate("key_ep_screen_upgrade_level_max"))
		};
		base.AddGuiElement(guiLabel25);
		this.upgradesForDelete.Add(guiLabel25);
		if (flag)
		{
			GuiTexture rect2 = new GuiTexture();
			rect2.SetTexture("PoiScreenWindow", "maxed");
			rect2.boundries = new Rect(810f, 110f, 50f, 50f);
			base.AddGuiElement(rect2);
			this.upgradesForDelete.Add(rect2);
		}
		else
		{
			ExtractionPointUpgrade extractionPointUpgrade = Enumerable.First<ExtractionPointUpgrade>(Enumerable.Where<ExtractionPointUpgrade>(this.pointInfo.allUpgrades, new Func<ExtractionPointUpgrade, bool>(variable, (ExtractionPointUpgrade u) => (u.upgradeType != this.<>f__this.selectedUnitType ? false : u.upgrade == this.nextLevelUnit.upgrade))));
			GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
			guiButtonResizeable.SetNovaBtn();
			guiButtonResizeable.X = 798f;
			guiButtonResizeable.Y = 102f;
			guiButtonResizeable.boundries.set_width(85f);
			int num2 = extractionPointUpgrade.price * this.pointInRange.magicCoefficient;
			guiButtonResizeable.Caption = num2.ToString("N0");
			guiButtonResizeable.FontSize = 14;
			guiButtonResizeable.Alignment = 4;
			guiButtonResizeable.isEnabled = (!this.CanInvest || this.isWaitingRefresh ? false : this.CanPayFor(1, extractionPointUpgrade.price * this.pointInRange.magicCoefficient));
			EventHandlerParam eventHandlerParam = new EventHandlerParam();
			NewPoiScreenWindow.PoIParams poIParam = new NewPoiScreenWindow.PoIParams()
			{
				type = this.selectedUnitType,
				level = extractionPointUnit1.upgrade,
				currency = 1
			};
			eventHandlerParam.customData = poIParam;
			guiButtonResizeable.eventHandlerParam = eventHandlerParam;
			guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, NewPoiScreenWindow.OnUpgradeBuyClicked);
			base.AddGuiElement(guiButtonResizeable);
			this.upgradesForDelete.Add(guiButtonResizeable);
			this.buttonsOnScreen.Add(guiButtonResizeable);
			GuiButtonResizeable action = new GuiButtonResizeable();
			action.SetEqBtn();
			action.X = 798f;
			action.Y = 149f;
			action.boundries.set_width(85f);
			int num3 = extractionPointUpgrade.price * 2 * this.pointInRange.magicCoefficient;
			action.Caption = num3.ToString("N0");
			action.FontSize = 14;
			action.Alignment = 4;
			action.isEnabled = (!this.CanInvest || this.isWaitingRefresh ? false : this.CanPayFor(2, extractionPointUpgrade.price * 2 * this.pointInRange.magicCoefficient));
			eventHandlerParam = new EventHandlerParam();
			poIParam = new NewPoiScreenWindow.PoIParams()
			{
				type = this.selectedUnitType,
				level = extractionPointUnit1.upgrade,
				currency = 2
			};
			eventHandlerParam.customData = poIParam;
			action.eventHandlerParam = eventHandlerParam;
			action.Clicked = new Action<EventHandlerParam>(this, NewPoiScreenWindow.OnUpgradeBuyClicked);
			base.AddGuiElement(action);
			this.upgradesForDelete.Add(action);
			this.buttonsOnScreen.Add(action);
		}
		float single4 = 0f;
		ExtractionPoinGuardSkills extractionPoinGuardSkill = null;
		int getSavedGuardianPoints = 0;
		GuiLabel guiLabel26 = new GuiLabel()
		{
			boundries = new Rect(833f, 225f, 45f, 16f),
			Alignment = 4,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12
		};
		base.AddGuiElement(guiLabel26);
		this.upgradesForDelete.Add(guiLabel26);
		if (this.pointInRange.guardianSkills.ContainsKey(this.selectedUnitType))
		{
			extractionPoinGuardSkill = this.pointInRange.guardianSkills.get_Item(this.selectedUnitType);
			getSavedGuardianPoints = extractionPoinGuardSkill.get_GetSavedGuardianPoints();
			single4 = 591f * (float)getSavedGuardianPoints / 60f;
		}
		guiLabel26.text = string.Format("{0}/{1}", getSavedGuardianPoints, 60);
		GuiTexture guiTexture3 = new GuiTexture()
		{
			boundries = new Rect(238f + single4, 228f, 591f - single4, 10f)
		};
		guiTexture3.SetTextureKeepSize("PoiScreenWindow", "skill-level-cover");
		base.AddGuiElement(guiTexture3);
		this.upgradesForDelete.Add(guiTexture3);
		this.UpdateGuardioanSkillTree(this.selectedUnitType, getSavedGuardianPoints);
	}

	private void OnResetCancel(object prm)
	{
		AndromedaGui.gui.RemoveWindow(this.dialogWindow.handler);
		AndromedaGui.gui.activeToolTipId = -1;
		this.dialogWindow = null;
	}

	private void OnResetConfirm(object prm)
	{
		// 
		// Current member / type: System.Void NewPoiScreenWindow::OnResetConfirm(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnResetConfirm(System.Object)
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

	private void OnResetSkillClicked(object prm)
	{
		this.dialogWindow = new GuiWindow()
		{
			isModal = true
		};
		this.dialogWindow.SetBackgroundTexture("WarScreenWindow", "popupBackground");
		this.dialogWindow.isHidden = false;
		this.dialogWindow.zOrder = 220;
		this.dialogWindow.PutToCenter();
		this.dialogWindow.isModal = true;
		this.dialogWindow.isHidden = false;
		AndromedaGui.gui.AddWindow(this.dialogWindow);
		AndromedaGui.gui.activeToolTipId = this.dialogWindow.handler;
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(68f, 66f, 490f, 28f),
			FontSize = 16,
			TextColor = GuiNewStyleBar.blueColor,
			text = StaticData.Translate("key_guardian_skill_tree_btn_reset"),
			Alignment = 4
		};
		this.dialogWindow.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(73f, 107f, 480f, 100f),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			text = string.Format(StaticData.Translate("key_guardian_skill_tree_reset_info_text"), 200 * this.pointInfo.magicCoefficient),
			Alignment = 4
		};
		this.dialogWindow.AddGuiElement(guiLabel1);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.SetNovaBtn();
		guiButtonResizeable.X = 77f;
		guiButtonResizeable.Y = 220f;
		guiButtonResizeable.boundries.set_width(175f);
		guiButtonResizeable.Caption = StaticData.Translate("key_guardian_skill_tree_reset_confirm");
		guiButtonResizeable.FontSize = 14;
		guiButtonResizeable.Alignment = 4;
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, NewPoiScreenWindow.OnResetConfirm);
		guiButtonResizeable.isEnabled = (!this.guardianSkillTreeAccess ? false : this.CanPayFor(1, 200 * this.pointInfo.magicCoefficient));
		this.dialogWindow.AddGuiElement(guiButtonResizeable);
		GuiButtonResizeable action = new GuiButtonResizeable();
		action.SetDiscardBtn();
		action.X = 374f;
		action.Y = 220f;
		action.boundries.set_width(175f);
		action.Caption = StaticData.Translate("key_guardian_skill_tree_reset_cancel");
		action.FontSize = 14;
		action.Alignment = 4;
		action.Clicked = new Action<EventHandlerParam>(this, NewPoiScreenWindow.OnResetCancel);
		this.dialogWindow.AddGuiElement(action);
	}

	private void OnSaveBtnClicked(object prm)
	{
		// 
		// Current member / type: System.Void NewPoiScreenWindow::OnSaveBtnClicked(System.Object)
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

	private void OnSelectCategoryClicked(EventHandlerParam prm)
	{
		EventHandlerParam eventHandlerParam;
		this.infoBtn.Clicked = new Action<EventHandlerParam>(this, NewPoiScreenWindow.OnEpInfoClicked);
		this.infoBtn.SetTexture("PoiScreenWindow", "btn-info");
		if (this.isInfoOnScren)
		{
			this.isInfoOnScren = false;
			this.ClearContent();
			this.CreateLeftSide();
		}
		bool flag = false;
		this.wantedGuardianSkills.Clear();
		if (prm == null || prm.customData == null)
		{
			return;
		}
		if (prm.customData2 != null)
		{
			flag = (bool)prm.customData2;
		}
		if (!flag)
		{
			this.selectedUnitType = 0;
		}
		this.ClearUpgradesContent();
		this.selectedCategoryTab = (int)prm.customData;
		switch (this.selectedCategoryTab)
		{
			case SelectedExtractionPointTab.Contribution:
			{
				this.CreateContributorsList();
				break;
			}
			case SelectedExtractionPointTab.AddOns:
			{
				this.CreateSingleUpgrade(1, 0);
				this.CreateSingleUpgrade(2, 1);
				this.CreateSingleUpgrade(3, 2);
				this.CreateSingleUpgrade(4, 3);
				this.CreateDamageReductionBoost(4);
				break;
			}
			case SelectedExtractionPointTab.Aliens:
			{
				if (!ExtractionPoint.IsGuardianAlien(this.selectedUnitType))
				{
					this.CreateSingleUpgrade(51, 0);
					this.CreateSingleUpgrade(52, 1);
					this.CreateSingleUpgrade(53, 2);
					this.CreateSingleUpgrade(54, 3);
					this.CreateSingleUpgrade(55, 4);
				}
				else
				{
					eventHandlerParam = new EventHandlerParam()
					{
						customData = this.selectedUnitType
					};
					this.OnGuardianClick(eventHandlerParam);
				}
				break;
			}
			case SelectedExtractionPointTab.Towers:
			{
				if (!ExtractionPoint.IsGuardianTower(this.selectedUnitType))
				{
					this.CreateSingleUpgrade(101, 0);
					this.CreateSingleUpgrade(102, 1);
					this.CreateSingleUpgrade(103, 2);
					this.CreateSingleUpgrade(104, 3);
					this.CreateSingleUpgrade(105, 4);
				}
				else
				{
					eventHandlerParam = new EventHandlerParam()
					{
						customData = this.selectedUnitType
					};
					this.OnGuardianClick(eventHandlerParam);
				}
				break;
			}
		}
	}

	private void OnUnitBuyClicked(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void NewPoiScreenWindow::OnUnitBuyClicked(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnUnitBuyClicked(EventHandlerParam)
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

	private void OnUpgradeBuyClicked(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void NewPoiScreenWindow::OnUpgradeBuyClicked(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnUpgradeBuyClicked(EventHandlerParam)
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

	private void OnWalletCategoryClicked(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void NewPoiScreenWindow::OnWalletCategoryClicked(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnWalletCategoryClicked(EventHandlerParam)
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

	public void PoiWindowUpdate(List<Contributor> contributionList = null)
	{
		if (this.isInfoOnScren)
		{
			return;
		}
		this.isWaitingRefresh = false;
		this.UpdatePoiStats();
		if (this.wallet == 2)
		{
			if ((NetworkScript.player.guild == null || NetworkScript.player.guildMember == null || NetworkScript.player.guildMember.rank == null ? true : !NetworkScript.player.guildMember.rank.canBank))
			{
				this.wallet = 1;
				this.btnPersonal.IsClicked = true;
				return;
			}
		}
		this.UpdateWallet();
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = this.selectedCategoryTab,
			customData2 = true
		};
		this.OnSelectCategoryClicked(eventHandlerParam);
	}

	private void ReserveResearchPoint(EventHandlerParam prm)
	{
		if (prm == null || prm.customData == null)
		{
			return;
		}
		NewPoiScreenWindow.GuardianSkills guardianSkill = (NewPoiScreenWindow.GuardianSkills)prm.customData;
		byte num = (byte)prm.customData2;
		if (!this.wantedGuardianSkills.ContainsKey(guardianSkill.skillType))
		{
			this.wantedGuardianSkills.Add(guardianSkill.skillType, 1);
		}
		else
		{
			SortedList<short, byte> sortedList = this.wantedGuardianSkills;
			SortedList<short, byte> sortedList1 = sortedList;
			short num1 = guardianSkill.skillType;
			byte item = sortedList1.get_Item(num1);
			sortedList.set_Item(num1, (byte)(item + 1));
		}
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = this.selectedUnitType
		};
		this.OnGuardianClick(eventHandlerParam);
	}

	private void SetWaitingStatus()
	{
		this.isWaitingRefresh = true;
		foreach (GuiButton guiButton in this.buttonsOnScreen)
		{
			guiButton.isEnabled = false;
		}
	}

	public void UpdateContributionList(List<Contributor> contributors)
	{
		// 
		// Current member / type: System.Void NewPoiScreenWindow::UpdateContributionList(System.Collections.Generic.List`1<Contributor>)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void UpdateContributionList(System.Collections.Generic.List<Contributor>)
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

	private void UpdateGuardioanSkillTree(byte unitType, int savedPoints)
	{
		// 
		// Current member / type: System.Void NewPoiScreenWindow::UpdateGuardioanSkillTree(System.Byte,System.Int32)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void UpdateGuardioanSkillTree(System.Byte,System.Int32)
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..(Expression , Instruction ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\FixBinaryExpressionsStep.cs:строка 291
		//    в ..(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\FixBinaryExpressionsStep.cs:строка 48
		//    в Telerik.JustDecompiler.Decompiler.ExpressionDecompilerStep.(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\ExpressionDecompilerStep.cs:строка 93
		//    в ..(MethodBody ,  , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 88
		//    в ..(MethodBody , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 70
		//    в Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 95
		//    в Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 58
		//    в ..(ILanguage , MethodDefinition ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:строка 117
		// 
		// mailto: JustDecompilePublicFeedback@telerik.com

	}

	private void UpdateInvestCooldown(object prm)
	{
		if (this.investCooldownLbl == null)
		{
			return;
		}
		if (NetworkScript.player.lastEpInvestTime.AddSeconds(3) <= StaticData.now)
		{
			this.investCooldownLbl.text = string.Empty;
			this.preDrawHandler = null;
			if (this.selectedCategoryTab != SelectedExtractionPointTab.Contribution)
			{
				this.PoiWindowUpdate(null);
			}
		}
		else
		{
			TimeSpan timeSpan = NetworkScript.player.lastEpInvestTime.AddSeconds(3) - StaticData.now;
			long totalSeconds = (long)timeSpan.get_TotalSeconds() + (long)1;
			this.investCooldownLbl.text = string.Format(StaticData.Translate("key_ep_screen_invest_cooldown"), totalSeconds);
		}
	}

	private void UpdatePoiStats()
	{
		// 
		// Current member / type: System.Void NewPoiScreenWindow::UpdatePoiStats()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void UpdatePoiStats()
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в Telerik.JustDecompiler.Decompiler.TypeInference.TypeInferer.(Expression ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\TypeInferer.cs:строка 97
		//    в Telerik.JustDecompiler.Decompiler.TypeInference.TypeInferer.(IList`1 , Expression ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\TypeInferer.cs:строка 70
		//    в Telerik.JustDecompiler.Decompiler.TypeInference.TypeInferer.() в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\TypeInferer.cs:строка 56
		//    в Telerik.JustDecompiler.Decompiler.TypeInference.TypeInferer.() в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\TypeInferer.cs:строка 331
		//    в Telerik.JustDecompiler.Decompiler.ExpressionDecompilerStep.(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\ExpressionDecompilerStep.cs:строка 88
		//    в ..(MethodBody ,  , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 88
		//    в ..(MethodBody , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 70
		//    в Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 95
		//    в Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 58
		//    в ..(ILanguage , MethodDefinition ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:строка 117
		// 
		// mailto: JustDecompilePublicFeedback@telerik.com

	}

	private void UpdateWallet()
	{
		if (this.wallet != 1)
		{
			this.walletEqValue.text = NetworkScript.player.guild.bankEquilib.ToString("N0");
			this.walletNovaValue.text = NetworkScript.player.guild.bankNova.ToString("N0");
		}
		else
		{
			GuiLabel str = this.walletEqValue;
			long equilibrium = NetworkScript.player.playerBelongings.playerItems.get_Equilibrium();
			str.text = equilibrium.ToString("N0");
			GuiLabel guiLabel = this.walletNovaValue;
			long nova = NetworkScript.player.playerBelongings.playerItems.get_Nova();
			guiLabel.text = nova.ToString("N0");
		}
	}

	private class GuardianSkills
	{
		public short skillType;

		public string assetName;

		public string uiName;

		public int maxLevel;

		public float posX;

		public float posY;

		public static NewPoiScreenWindow.GuardianSkills[] allSkills;

		public float GetLblPositionX
		{
			get
			{
				return this.posX - 19f;
			}
		}

		public float GetLblPositionY
		{
			get
			{
				return (this.posY != 343f ? this.posY + 63f : this.posY - 23f);
			}
		}

		static GuardianSkills()
		{
			NewPoiScreenWindow.GuardianSkills[] guardianSkillsArray = new NewPoiScreenWindow.GuardianSkills[12];
			NewPoiScreenWindow.GuardianSkills guardianSkill = new NewPoiScreenWindow.GuardianSkills()
			{
				skillType = 101,
				maxLevel = 4,
				assetName = "RepairMaster",
				uiName = "key_target_enchant_repair",
				posX = 249f,
				posY = 343f
			};
			guardianSkillsArray[0] = guardianSkill;
			guardianSkill = new NewPoiScreenWindow.GuardianSkills()
			{
				skillType = 102,
				maxLevel = 4,
				assetName = "Disabler",
				uiName = "key_target_enchant_disabler",
				posX = 249f,
				posY = 423f
			};
			guardianSkillsArray[1] = guardianSkill;
			guardianSkill = new NewPoiScreenWindow.GuardianSkills()
			{
				skillType = 501,
				maxLevel = 1,
				assetName = "Remedy",
				uiName = "key_target_remedy",
				posX = 362f,
				posY = 343f
			};
			guardianSkillsArray[2] = guardianSkill;
			guardianSkill = new NewPoiScreenWindow.GuardianSkills()
			{
				skillType = 502,
				maxLevel = 1,
				assetName = "PowerBreaker",
				uiName = "key_target_power_breaker",
				posX = 362f,
				posY = 423f
			};
			guardianSkillsArray[3] = guardianSkill;
			guardianSkill = new NewPoiScreenWindow.GuardianSkills()
			{
				skillType = 201,
				maxLevel = 4,
				assetName = "Shielding",
				uiName = "key_target_enchant_shielding",
				posX = 475f,
				posY = 343f
			};
			guardianSkillsArray[4] = guardianSkill;
			guardianSkill = new NewPoiScreenWindow.GuardianSkills()
			{
				skillType = 202,
				maxLevel = 4,
				assetName = "Rocketeer",
				uiName = "key_target_enchant_rocketeer",
				posX = 475f,
				posY = 423f
			};
			guardianSkillsArray[5] = guardianSkill;
			guardianSkill = new NewPoiScreenWindow.GuardianSkills()
			{
				skillType = 601,
				maxLevel = 1,
				assetName = "ShieldFortress",
				uiName = "key_target_shield_fortress",
				posX = 588f,
				posY = 343f
			};
			guardianSkillsArray[6] = guardianSkill;
			guardianSkill = new NewPoiScreenWindow.GuardianSkills()
			{
				skillType = 602,
				maxLevel = 1,
				assetName = "UltimateRocketeer",
				uiName = "key_target_ultimate_rocketeer",
				posX = 588f,
				posY = 423f
			};
			guardianSkillsArray[7] = guardianSkill;
			guardianSkill = new NewPoiScreenWindow.GuardianSkills()
			{
				skillType = 301,
				maxLevel = 4,
				assetName = "Unstoppable",
				uiName = "key_target_unstoppable",
				posX = 701f,
				posY = 343f
			};
			guardianSkillsArray[8] = guardianSkill;
			guardianSkill = new NewPoiScreenWindow.GuardianSkills()
			{
				skillType = 302,
				maxLevel = 4,
				assetName = "Stormer",
				uiName = "key_target_enchant_stormer",
				posX = 701f,
				posY = 423f
			};
			guardianSkillsArray[9] = guardianSkill;
			guardianSkill = new NewPoiScreenWindow.GuardianSkills()
			{
				skillType = 401,
				maxLevel = 4,
				assetName = "RepairingDrones",
				uiName = "key_target_enchant_repairing_drones",
				posX = 814f,
				posY = 343f
			};
			guardianSkillsArray[10] = guardianSkill;
			guardianSkill = new NewPoiScreenWindow.GuardianSkills()
			{
				skillType = 402,
				maxLevel = 4,
				assetName = "UltimateEnforcer",
				uiName = "key_target_ultimate_enforcer",
				posX = 814f,
				posY = 423f
			};
			guardianSkillsArray[11] = guardianSkill;
			NewPoiScreenWindow.GuardianSkills.allSkills = guardianSkillsArray;
		}

		public GuardianSkills()
		{
		}
	}

	private class PoIParams
	{
		public byte type;

		public byte level;

		public SelectedCurrency currency;

		public PoIParams()
		{
		}
	}
}