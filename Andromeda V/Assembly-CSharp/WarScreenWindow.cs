using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TransferableObjects;
using UnityEngine;

public class WarScreenWindow : GuiWindow
{
	private WarScreenTab selectedTab = WarScreenTab.WeeklyChallenge;

	private WarScreenSubsection selectedSubsection;

	private CouncilShopSubsection selectedCouncilSubsection;

	private List<GuiElement> forDelete = new List<GuiElement>();

	private List<GuiElement> forDeleteLeftSide = new List<GuiElement>();

	private List<GuiElement> forRearDelete = new List<GuiElement>();

	private List<GuiElement> forActiveBoostsDelete = new List<GuiElement>();

	private List<WarScreenWindow.FactionBoostVoteElements> forBattle = new List<WarScreenWindow.FactionBoostVoteElements>();

	private List<WarScreenWindow.FactionBoostVoteElements> forUtility = new List<WarScreenWindow.FactionBoostVoteElements>();

	public List<KeyValuePair<short, long>> galaxyVote = new List<KeyValuePair<short, long>>();

	public List<KeyValuePair<string, long>> topCouncilCandidats = new List<KeyValuePair<string, long>>();

	public List<FactionCouncilMember> factionOneCouncil = new List<FactionCouncilMember>();

	public List<FactionCouncilMember> factionTwoCouncil = new List<FactionCouncilMember>();

	public bool isCouncilsReceived;

	public static long factionOneScore;

	public static long factionTwoScore;

	public static long factionOneCurrentDayScore;

	public static long factionTwoCurrentDayScore;

	public static byte weeklyLoosingFaction;

	public static int weeklyLoosingFactionBonusPercent;

	public static byte dailyLoosingFaction;

	public static int dailyLoosingFactionBonusPercent;

	public long factionBank;

	public int paidAdPrice;

	public string paidAdNickName = string.Empty;

	public string paidAdSlogan = string.Empty;

	public string yourFactionToYou;

	public string yourFactionToEnemy;

	public string enemyFactionToYou;

	public byte voteForBattleOne;

	public byte voteForBattleTwo;

	public byte voteForBattleThree;

	public byte voteForBattleVeto;

	public byte voteForUtilityOne;

	public byte voteForUtilityTwo;

	public byte voteForUtilityThree;

	public byte voteForUtilityVeto;

	private bool requestVoteForPlayerInfo = true;

	private bool requestVoteForGalaxyInfo = true;

	private bool requestFactionBank = true;

	private bool requestFactionMessages = true;

	private bool isWaitingRefresh;

	private GuiWindow dialogWindow;

	private GuiTextBox tb_vote_for_player_nickname;

	private GuiTextBox tb_donation;

	private GuiTextBox tb_paid_ad_popup_slogan;

	private GuiLabel faction1scoreLbl;

	private GuiLabel faction2scoreLbl;

	private GuiLabel loosingFactionOneBonus;

	private GuiLabel loosingFactionTwoBonus;

	private GuiLabel warCommendationLbl;

	private GuiButtonResizeable btnGetMoreWc;

	private GuiLabel warCommendationVoteLbl;

	private GuiButtonFixed btnVoteWcMinus;

	private GuiButtonFixed btnVoteWcPlus;

	private GuiLabel factionOneMessageLbl;

	private GuiLabel factionTwoMessageLbl;

	private GuiButtonResizeable btnWeeklyChallenge;

	private GuiButtonResizeable btnCouncil;

	private GuiButtonResizeable btnShop;

	private GuiLabel factionBankLbl;

	private GuiButtonFixed infoBtn;

	private bool isInfoOnScren;

	private int selectedGalaxyId;

	private GuiButtonResizeable btnVoteForGalaxy;

	private List<WarScreenWindow.GalaxyVoteElements> galaxyVoteElementsForUpdate = new List<WarScreenWindow.GalaxyVoteElements>();

	private GuiTextBox tbMessage;

	private GuiButtonFixed btnEditSaveMessage;

	private bool messageToYourFaction = true;

	private int tmp;

	public bool selectCouncilSkillComandSend;

	private GuiButtonResizeable btnApplyChanges;

	private GuiButtonFixed battleBoostClearVoteBtn;

	private GuiButtonFixed utilityBoostClearVoteBtn;

	private GuiButtonFixed battleBoostVetoBtn;

	private GuiButtonFixed utilityBoostVetoBtn;

	private GuiLabel battleBoostVetoLbl;

	private GuiLabel utilityBoostVetoLbl;

	static WarScreenWindow()
	{
	}

	public WarScreenWindow()
	{
	}

	private void ClearContent()
	{
		foreach (GuiElement guiElement in this.forDeleteLeftSide)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.forDeleteLeftSide.Clear();
		foreach (GuiElement guiElement1 in this.forDelete)
		{
			base.RemoveGuiElement(guiElement1);
		}
		this.forDelete.Clear();
		foreach (GuiElement guiElement2 in this.forRearDelete)
		{
			base.RemoveGuiElement(guiElement2);
		}
		this.forRearDelete.Clear();
		foreach (GuiElement guiElement3 in this.forActiveBoostsDelete)
		{
			base.RemoveGuiElement(guiElement3);
		}
		this.forActiveBoostsDelete.Clear();
	}

	private void ClearRearElements()
	{
		foreach (GuiElement guiElement in this.forRearDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.forRearDelete.Clear();
	}

	private void ClearScreen()
	{
		foreach (GuiElement guiElement in this.forDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.forDelete.Clear();
	}

	private void ConfirmBuyWarCommendation(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void WarScreenWindow::ConfirmBuyWarCommendation(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void ConfirmBuyWarCommendation(EventHandlerParam)
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

	public override void Create()
	{
		base.SetBackgroundTexture("PoiScreenWindow", "frame");
		base.PutToCenter();
		this.zOrder = 210;
		this.isHidden = false;
		this.infoBtn = new GuiButtonFixed();
		this.infoBtn.SetTexture("PoiScreenWindow", "btn-info");
		this.infoBtn.X = 0f;
		this.infoBtn.Y = 4f;
		this.infoBtn.Caption = string.Empty;
		this.infoBtn.Clicked = null;
		base.AddGuiElement(this.infoBtn);
		if (NetworkScript.player != null && !NetworkScript.player.isWarInProgress)
		{
			this.selectedTab = WarScreenTab.WarInProgress;
		}
		this.CreateLeftSide();
	}

	private void CreateCouncilShop()
	{
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.SetTexture("PoiScreenWindow", "tab_");
		guiButtonResizeable.X = 217f;
		guiButtonResizeable.Y = 59f;
		guiButtonResizeable.boundries.set_width(170f);
		guiButtonResizeable.groupId = 2;
		guiButtonResizeable.behaviourKeepClicked = true;
		guiButtonResizeable.Caption = StaticData.Translate("key_faction_war_shop_btn_faction_boosts");
		guiButtonResizeable.FontSize = 12;
		guiButtonResizeable.Alignment = 4;
		guiButtonResizeable.SetRegularFont();
		guiButtonResizeable.SetColor(GuiNewStyleBar.blueColor);
		guiButtonResizeable.eventHandlerParam.customData = CouncilShopSubsection.FactionBoosts;
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnCouncilShopBtnClicked);
		guiButtonResizeable.IsClicked = this.selectedCouncilSubsection == CouncilShopSubsection.FactionBoosts;
		base.AddGuiElement(guiButtonResizeable);
		this.forRearDelete.Add(guiButtonResizeable);
		GuiButtonResizeable action = new GuiButtonResizeable();
		action.SetTexture("PoiScreenWindow", "tab_");
		action.X = 390f;
		action.Y = 59f;
		action.boundries.set_width(170f);
		action.groupId = 2;
		action.behaviourKeepClicked = true;
		action.Caption = StaticData.Translate("key_faction_war_shop_btn_council_skills");
		action.FontSize = 12;
		action.Alignment = 4;
		action.SetRegularFont();
		action.SetColor(GuiNewStyleBar.blueColor);
		action.eventHandlerParam.customData = CouncilShopSubsection.CouncilSkills;
		action.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnCouncilShopBtnClicked);
		action.IsClicked = this.selectedCouncilSubsection == CouncilShopSubsection.CouncilSkills;
		base.AddGuiElement(action);
		this.forRearDelete.Add(action);
	}

	private void CreateCouncilSkills()
	{
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("WarScreenWindow", "councilSkillsFrame");
		guiTexture.X = 210f;
		guiTexture.Y = 92f;
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(220f, 102f, 670f, 50f),
			text = StaticData.Translate("key_faction_war_council_skills_info"),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(220f, 170f, 285f, 18f),
			text = StaticData.Translate("key_faction_war_council_skills_for_leaders_lbl"),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel1);
		this.forDelete.Add(guiLabel1);
		GuiLabel str = new GuiLabel()
		{
			boundries = new Rect(751f, 174f, 65f, 18f)
		};
		int num = FactionWarStats.CouncilSkillPriceCash(NetworkScript.player.playerBelongings.playerLevel);
		str.text = num.ToString("N0");
		str.FontSize = 12;
		str.TextColor = GuiNewStyleBar.blueColor;
		str.Alignment = 5;
		base.AddGuiElement(str);
		this.forDelete.Add(str);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("FrameworkGUI", "res_cash");
		guiTexture1.X = 815f;
		guiTexture1.Y = 173f;
		base.AddGuiElement(guiTexture1);
		this.forDelete.Add(guiTexture1);
		GuiLabel str1 = new GuiLabel()
		{
			boundries = new Rect(825f, 174f, 55f, 18f)
		};
		int num1 = FactionWarStats.CouncilSkillPriceEquilibrium(NetworkScript.player.playerBelongings.playerLevel);
		str1.text = num1.ToString("N0");
		str1.FontSize = 12;
		str1.TextColor = GuiNewStyleBar.purpleColor;
		str1.Alignment = 5;
		base.AddGuiElement(str1);
		this.forDelete.Add(str1);
		GuiTexture guiTexture2 = new GuiTexture();
		guiTexture2.SetTexture("FrameworkGUI", "res_equilibrium");
		guiTexture2.X = 880f;
		guiTexture2.Y = 173f;
		base.AddGuiElement(guiTexture2);
		this.forDelete.Add(guiTexture2);
		for (int i = 0; i < 3; i++)
		{
			TalentsInfo item = null;
			int levelEfX = 0;
			int levelEfY = 0;
			int num2 = 0;
			switch (i)
			{
				case 0:
				{
					item = (TalentsInfo)StaticData.allTypes.get_Item(PlayerItems.TypeCouncilSkillDisarm);
					levelEfX = PlayerItems.skillsInformation.get_Item(PlayerItems.TypeCouncilSkillDisarm).levelEf_X;
					levelEfY = PlayerItems.skillsInformation.get_Item(PlayerItems.TypeCouncilSkillDisarm).levelEf_Y;
					break;
				}
				case 1:
				{
					item = (TalentsInfo)StaticData.allTypes.get_Item(PlayerItems.TypeCouncilSkillSacrifice);
					levelEfX = PlayerItems.skillsInformation.get_Item(PlayerItems.TypeCouncilSkillSacrifice).levelEf_X;
					levelEfY = PlayerItems.skillsInformation.get_Item(PlayerItems.TypeCouncilSkillSacrifice).levelEf_Y;
					break;
				}
				case 2:
				{
					item = (TalentsInfo)StaticData.allTypes.get_Item(PlayerItems.TypeCouncilSkillLifesteal);
					levelEfX = PlayerItems.skillsInformation.get_Item(PlayerItems.TypeCouncilSkillLifesteal).levelEf_X;
					levelEfY = PlayerItems.skillsInformation.get_Item(PlayerItems.TypeCouncilSkillLifesteal).levelEf_Y;
					break;
				}
			}
			num2 = item.range;
			bool flag = NetworkScript.player.playerBelongings.councilSkillSelected == item.itemType;
			Color color = GuiNewStyleBar.blueColor;
			GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
			guiButtonFixed.SetTexture("WarScreenWindow", "councilSkillFrame");
			guiButtonFixed.SetTextureClicked("WarScreenWindow", "councilSkillFrameHvr");
			guiButtonFixed.X = (float)(222 + 230 * i);
			guiButtonFixed.Y = 210f;
			guiButtonFixed.Caption = string.Empty;
			guiButtonFixed.groupId = 10;
			guiButtonFixed.eventHandlerParam.customData = item.itemType;
			guiButtonFixed.behaviourKeepClicked = true;
			guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnCouncilSkillSelectClicked);
			guiButtonFixed.IsClicked = NetworkScript.player.playerBelongings.councilSkillSelected == item.itemType;
			base.AddGuiElement(guiButtonFixed);
			this.forDelete.Add(guiButtonFixed);
			GuiTexture x = new GuiTexture();
			x.SetTexture("WarScreenWindow", string.Concat("council", item.assetName));
			x.X = guiButtonFixed.X + 13f;
			x.Y = guiButtonFixed.Y + 12f;
			base.AddGuiElement(x);
			this.forDelete.Add(x);
			GuiLabel guiLabel2 = new GuiLabel()
			{
				boundries = new Rect(guiButtonFixed.X + 85f, guiButtonFixed.Y + 13f, 115f, 28f),
				text = StaticData.Translate(item.uiName),
				FontSize = 12,
				Font = GuiLabel.FontBold,
				TextColor = color,
				Alignment = 0
			};
			base.AddGuiElement(guiLabel2);
			this.forDelete.Add(guiLabel2);
			GuiLabel guiLabel3 = new GuiLabel()
			{
				boundries = new Rect(guiButtonFixed.X + 85f, guiButtonFixed.Y + 69f, 115f, 16f),
				text = string.Format(StaticData.Translate("key_main_screen_skill_tooltip3"), item.cooldown / 1000),
				FontSize = 12,
				Italic = true,
				Alignment = 6,
				TextColor = color
			};
			base.AddGuiElement(guiLabel3);
			this.forDelete.Add(guiLabel3);
			GuiLabel guiLabel4 = new GuiLabel()
			{
				boundries = new Rect(guiButtonFixed.X + 13f, guiButtonFixed.Y + 98f, 190f, 110f),
				text = string.Format(StaticData.Translate(item.description), levelEfX, levelEfY, num2),
				FontSize = 12,
				TextColor = color
			};
			base.AddGuiElement(guiLabel4);
			this.forDelete.Add(guiLabel4);
			if (flag)
			{
				GuiLabel guiLabel5 = new GuiLabel()
				{
					boundries = new Rect(guiButtonFixed.X, guiButtonFixed.Y + 190f, 210f, 16f),
					text = StaticData.Translate("key_council_skill_status_active"),
					FontSize = 12,
					Font = GuiLabel.FontBold,
					Alignment = 4,
					TextColor = GuiNewStyleBar.orangeColor
				};
				base.AddGuiElement(guiLabel5);
				this.forDelete.Add(guiLabel5);
			}
		}
		this.btnApplyChanges = new GuiButtonResizeable();
		this.btnApplyChanges.SetTexture("PoiScreenWindow", "tab_");
		this.btnApplyChanges.X = 222f;
		this.btnApplyChanges.Y = 480f;
		this.btnApplyChanges.boundries.set_width(210f);
		this.btnApplyChanges.Caption = StaticData.Translate("key_faction_war_council_skills_btn_apply_changes");
		this.btnApplyChanges.FontSize = 12;
		this.btnApplyChanges.Alignment = 4;
		this.btnApplyChanges.SetRegularFont();
		this.btnApplyChanges.SetColor(GuiNewStyleBar.blueColor);
		this.btnApplyChanges.isEnabled = false;
		this.btnApplyChanges.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnSelectCouncilSkillClicked);
		base.AddGuiElement(this.btnApplyChanges);
		this.forDelete.Add(this.btnApplyChanges);
	}

	private void CreateFactionBoost()
	{
		// 
		// Current member / type: System.Void WarScreenWindow::CreateFactionBoost()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void CreateFactionBoost()
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

	private void CreateLeftSide()
	{
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("PoiScreenWindow", "frameLeftSeparator");
		guiTexture.X = 0f;
		guiTexture.Y = 0f;
		base.AddGuiElement(guiTexture);
		this.forDeleteLeftSide.Add(guiTexture);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(147f, 8f, 618f, 36f),
			Alignment = 4,
			text = StaticData.Translate("key_faction_war_window_title"),
			FontSize = 24,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel);
		this.forDeleteLeftSide.Add(guiLabel);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("WarScreenWindow", "warIcon");
		guiTexture1.X = 16f;
		guiTexture1.Y = 38f;
		base.AddGuiElement(guiTexture1);
		this.forDeleteLeftSide.Add(guiTexture1);
		GuiTexture drawTooltipWindow = new GuiTexture();
		drawTooltipWindow.SetTexture("WarScreenWindow", "warCommendation");
		drawTooltipWindow.X = 20f;
		drawTooltipWindow.Y = 205f;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_faction_war_commendation_tooltip"),
			customData2 = drawTooltipWindow
		};
		drawTooltipWindow.tooltipWindowParam = eventHandlerParam;
		drawTooltipWindow.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(drawTooltipWindow);
		this.forDeleteLeftSide.Add(drawTooltipWindow);
		this.warCommendationLbl = new GuiLabel()
		{
			boundries = new Rect(60f, 205f, 145f, 26f),
			Alignment = 3,
			text = string.Format("{0:N0}/{1:N0}", NetworkScript.player.playerBelongings.playerItems.get_WarCommendation(), 100),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.warCommendationLbl);
		this.forDeleteLeftSide.Add(this.warCommendationLbl);
		GuiTexture drawTooltipWindow1 = new GuiTexture();
		drawTooltipWindow1.SetTexture("ActionButtons", "btnInfoNml");
		drawTooltipWindow1.X = 170f;
		drawTooltipWindow1.Y = 205f;
		drawTooltipWindow1.SetSize(26f, 26f);
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_faction_war_commendation_info_tooltip"),
			customData2 = drawTooltipWindow1
		};
		drawTooltipWindow1.tooltipWindowParam = eventHandlerParam;
		drawTooltipWindow1.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(drawTooltipWindow1);
		this.forDeleteLeftSide.Add(drawTooltipWindow1);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(20f, 383f, 175f, 14f),
			Alignment = 3,
			text = StaticData.Translate("key_faction_war_active_boosts_lbl"),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel1);
		this.forDeleteLeftSide.Add(guiLabel1);
		GuiTexture guiTexture2 = new GuiTexture();
		guiTexture2.SetTexture("WarScreenWindow", "activeBoostersBgr");
		guiTexture2.X = 15f;
		guiTexture2.Y = 399f;
		base.AddGuiElement(guiTexture2);
		this.forDeleteLeftSide.Add(guiTexture2);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(20f, 443f, 175f, 14f),
			Alignment = 3,
			text = StaticData.Translate("key_faction_war_score_lbl"),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel2);
		this.forDeleteLeftSide.Add(guiLabel2);
		GuiTexture guiTexture3 = new GuiTexture();
		guiTexture3.SetTexture("WarScreenWindow", "score");
		guiTexture3.X = 15f;
		guiTexture3.Y = 459f;
		base.AddGuiElement(guiTexture3);
		this.forDeleteLeftSide.Add(guiTexture3);
		this.faction1scoreLbl = new GuiLabel()
		{
			boundries = new Rect(55f, 491f, 135f, 14f),
			Alignment = 3,
			text = string.Empty,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.faction1scoreLbl);
		this.forDeleteLeftSide.Add(this.faction1scoreLbl);
		this.faction2scoreLbl = new GuiLabel()
		{
			boundries = new Rect(55f, 465f, 135f, 14f),
			Alignment = 3,
			text = string.Empty,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.faction2scoreLbl);
		this.forDeleteLeftSide.Add(this.faction2scoreLbl);
		this.loosingFactionOneBonus = new GuiLabel()
		{
			boundries = new Rect(55f, 491f, 135f, 14f),
			Alignment = 5,
			text = string.Empty,
			FontSize = 10,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.loosingFactionOneBonus);
		this.forDeleteLeftSide.Add(this.loosingFactionOneBonus);
		this.loosingFactionTwoBonus = new GuiLabel()
		{
			boundries = new Rect(55f, 465f, 135f, 14f),
			Alignment = 5,
			text = string.Empty,
			FontSize = 10,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.loosingFactionTwoBonus);
		this.forDeleteLeftSide.Add(this.loosingFactionTwoBonus);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.SetTexture("PoiScreenWindow", "tab_");
		guiButtonResizeable.X = 16f;
		guiButtonResizeable.Y = 238f;
		guiButtonResizeable.boundries.set_width(180f);
		guiButtonResizeable.groupId = 1;
		guiButtonResizeable.behaviourKeepClicked = true;
		guiButtonResizeable.Caption = StaticData.Translate("key_faction_war_btn_war_in_progress");
		guiButtonResizeable.FontSize = 12;
		guiButtonResizeable.Alignment = 4;
		guiButtonResizeable.SetRegularFont();
		guiButtonResizeable.SetColor(GuiNewStyleBar.blueColor);
		guiButtonResizeable.eventHandlerParam.customData = WarScreenTab.WarInProgress;
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnSelectCategoryClicked);
		guiButtonResizeable.IsClicked = this.selectedTab == WarScreenTab.WarInProgress;
		base.AddGuiElement(guiButtonResizeable);
		this.forDeleteLeftSide.Add(guiButtonResizeable);
		this.btnWeeklyChallenge = new GuiButtonResizeable();
		this.btnWeeklyChallenge.SetTexture("PoiScreenWindow", "tab_");
		this.btnWeeklyChallenge.X = 16f;
		this.btnWeeklyChallenge.Y = guiButtonResizeable.Y + 35f;
		this.btnWeeklyChallenge.boundries.set_width(180f);
		this.btnWeeklyChallenge.groupId = 1;
		this.btnWeeklyChallenge.behaviourKeepClicked = true;
		this.btnWeeklyChallenge.Caption = StaticData.Translate("key_faction_war_btn_weekly_challenge");
		this.btnWeeklyChallenge.FontSize = 12;
		this.btnWeeklyChallenge.Alignment = 4;
		this.btnWeeklyChallenge.SetRegularFont();
		this.btnWeeklyChallenge.SetColor(GuiNewStyleBar.blueColor);
		this.btnWeeklyChallenge.eventHandlerParam.customData = WarScreenTab.WeeklyChallenge;
		this.btnWeeklyChallenge.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnSelectCategoryClicked);
		this.btnWeeklyChallenge.IsClicked = this.selectedTab == WarScreenTab.WeeklyChallenge;
		this.btnWeeklyChallenge.isEnabled = NetworkScript.player.isWarInProgress;
		base.AddGuiElement(this.btnWeeklyChallenge);
		this.forDeleteLeftSide.Add(this.btnWeeklyChallenge);
		this.btnCouncil = new GuiButtonResizeable();
		this.btnCouncil.SetTexture("PoiScreenWindow", "tab_");
		this.btnCouncil.X = 16f;
		this.btnCouncil.Y = this.btnWeeklyChallenge.Y + 35f;
		this.btnCouncil.boundries.set_width(180f);
		this.btnCouncil.groupId = 1;
		this.btnCouncil.behaviourKeepClicked = true;
		this.btnCouncil.Caption = StaticData.Translate("key_faction_war_btn_council");
		this.btnCouncil.FontSize = 12;
		this.btnCouncil.Alignment = 4;
		this.btnCouncil.SetRegularFont();
		this.btnCouncil.SetColor(GuiNewStyleBar.blueColor);
		this.btnCouncil.isEnabled = (NetworkScript.player.playerBelongings.factionWarDay == FactionWarStats.voteForPlayerDay ? false : NetworkScript.player.isWarInProgress);
		this.btnCouncil.eventHandlerParam.customData = WarScreenTab.Council;
		this.btnCouncil.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnSelectCategoryClicked);
		this.btnCouncil.IsClicked = this.selectedTab == WarScreenTab.Council;
		base.AddGuiElement(this.btnCouncil);
		this.forDeleteLeftSide.Add(this.btnCouncil);
		this.btnShop = new GuiButtonResizeable();
		this.btnShop.SetTexture("PoiScreenWindow", "tab_");
		this.btnShop.X = 16f;
		this.btnShop.Y = this.btnCouncil.Y + 35f;
		this.btnShop.boundries.set_width(180f);
		this.btnShop.groupId = 1;
		this.btnShop.behaviourKeepClicked = true;
		this.btnShop.Caption = StaticData.Translate("key_faction_war_btn_shop");
		this.btnShop.FontSize = 12;
		this.btnShop.Alignment = 4;
		this.btnShop.SetRegularFont();
		this.btnShop.SetColor(GuiNewStyleBar.blueColor);
		this.btnShop.isEnabled = (NetworkScript.player.playerBelongings.councilRank == 0 ? false : NetworkScript.player.isWarInProgress);
		this.btnShop.eventHandlerParam.customData = WarScreenTab.Shop;
		this.btnShop.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnSelectCategoryClicked);
		this.btnShop.IsClicked = this.selectedTab == WarScreenTab.Shop;
		base.AddGuiElement(this.btnShop);
		this.forDeleteLeftSide.Add(this.btnShop);
		this.UpdateFactionActiveBoosts();
	}

	private void CreateWarInProgress()
	{
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("WarScreenWindow", "warOverviewBackground");
		guiTexture.X = 211f;
		guiTexture.Y = 53f;
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		LevelMap[] levelMapArray = StaticData.allGalaxies;
		if (WarScreenWindow.<>f__am$cache5B == null)
		{
			WarScreenWindow.<>f__am$cache5B = new Func<LevelMap, bool>(null, (LevelMap t) => (t.galaxyKey != 41 ? false : t.get_galaxyId() % 100 == 10));
		}
		LevelMap levelMap = Enumerable.FirstOrDefault<LevelMap>(Enumerable.Where<LevelMap>(levelMapArray, WarScreenWindow.<>f__am$cache5B));
		if (levelMap == null)
		{
			return;
		}
		LevelMap[] levelMapArray1 = StaticData.allGalaxies;
		if (WarScreenWindow.<>f__am$cache5C == null)
		{
			WarScreenWindow.<>f__am$cache5C = new Func<LevelMap, bool>(null, (LevelMap t) => (t.galaxyKey != 42 ? false : t.get_galaxyId() % 100 == 10));
		}
		LevelMap levelMap1 = Enumerable.FirstOrDefault<LevelMap>(Enumerable.Where<LevelMap>(levelMapArray1, WarScreenWindow.<>f__am$cache5C));
		if (levelMap1 == null)
		{
			return;
		}
		LevelMap[] levelMapArray2 = StaticData.allGalaxies;
		if (WarScreenWindow.<>f__am$cache5D == null)
		{
			WarScreenWindow.<>f__am$cache5D = new Func<LevelMap, bool>(null, (LevelMap t) => (t.galaxyKey != 43 ? false : t.get_galaxyId() % 100 == 10));
		}
		LevelMap levelMap2 = Enumerable.FirstOrDefault<LevelMap>(Enumerable.Where<LevelMap>(levelMapArray2, WarScreenWindow.<>f__am$cache5D));
		if (levelMap2 == null)
		{
			return;
		}
		LevelMap[] levelMapArray3 = StaticData.allGalaxies;
		if (WarScreenWindow.<>f__am$cache5E == null)
		{
			WarScreenWindow.<>f__am$cache5E = new Func<LevelMap, bool>(null, (LevelMap t) => (t.galaxyKey != 44 ? false : t.get_galaxyId() % 100 == 10));
		}
		LevelMap levelMap3 = Enumerable.FirstOrDefault<LevelMap>(Enumerable.Where<LevelMap>(levelMapArray3, WarScreenWindow.<>f__am$cache5E));
		if (levelMap3 == null)
		{
			return;
		}
		LevelMap[] levelMapArray4 = StaticData.allGalaxies;
		if (WarScreenWindow.<>f__am$cache5F == null)
		{
			WarScreenWindow.<>f__am$cache5F = new Func<LevelMap, bool>(null, (LevelMap t) => (t.galaxyKey != 45 ? false : t.get_galaxyId() % 100 == 10));
		}
		LevelMap levelMap4 = Enumerable.FirstOrDefault<LevelMap>(Enumerable.Where<LevelMap>(levelMapArray4, WarScreenWindow.<>f__am$cache5F));
		if (levelMap4 == null)
		{
			return;
		}
		this.DrawGalaxyInfo(1, NetworkScript.player.factionGalaxyOwnership.get_Item(41), 260f, 66f, StaticData.Translate(levelMap.nameUI), string.Format(StaticData.Translate("key_faction_galaxy_bonus_xp_gain"), 20), string.Empty);
		this.DrawGalaxyInfo(2, NetworkScript.player.factionGalaxyOwnership.get_Item(42), 476f, 66f, StaticData.Translate(levelMap1.nameUI), string.Format(StaticData.Translate("key_faction_galaxy_bonus_resources_drop"), 100), string.Empty);
		this.DrawGalaxyInfo(3, NetworkScript.player.factionGalaxyOwnership.get_Item(43), 696f, 66f, StaticData.Translate(levelMap2.nameUI), string.Format(StaticData.Translate("key_faction_galaxy_bonus_sell_price"), 100), string.Empty);
		this.DrawGalaxyInfo(4, NetworkScript.player.factionGalaxyOwnership.get_Item(44), 368f, 211f, StaticData.Translate(levelMap3.nameUI), StaticData.Translate("key_faction_galaxy_bonus_better_drop"), string.Empty);
		this.DrawGalaxyInfo(5, NetworkScript.player.factionGalaxyOwnership.get_Item(45), 616f, 211f, StaticData.Translate(levelMap4.nameUI), string.Format(StaticData.Translate("key_faction_galaxy_bonus_research_point"), 50), string.Format(StaticData.Translate("key_faction_galaxy_bonus_defending"), 100));
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("PoiScreenWindow", "btnUniverseMap");
		guiButtonFixed.X = 850f;
		guiButtonFixed.Y = 283f;
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnUniverseMapBtnClicked);
		base.AddGuiElement(guiButtonFixed);
		this.forDelete.Add(guiButtonFixed);
		if (!NetworkScript.player.isWarInProgress)
		{
			GuiButtonFixed empty = new GuiButtonFixed();
			empty.SetTexture("PoiScreenWindow", "btnReward");
			empty.X = 223f;
			empty.Y = 283f;
			empty.Caption = string.Empty;
			empty.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnCollectLastWeekRewardClicked);
			empty.isEnabled = (this.isWaitingRefresh ? false : NetworkScript.player.playerBelongings.lastWeekPendingReward != 0);
			EventHandlerParam eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_faction_war_overview_btn_get_last_week_reward"),
				customData2 = empty
			};
			empty.tooltipWindowParam = eventHandlerParam;
			empty.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			base.AddGuiElement(empty);
			this.forDelete.Add(empty);
			GuiLabel guiLabel = new GuiLabel()
			{
				boundries = new Rect(314f, 403f, 486f, 50f),
				text = StaticData.Translate("key_war_in_progress_war_is_over"),
				FontSize = 14,
				Font = GuiLabel.FontBold,
				TextColor = GuiNewStyleBar.aquamarineColor,
				Alignment = 4
			};
			base.AddGuiElement(guiLabel);
			this.forDelete.Add(guiLabel);
			TimeSpan timeSpan = NetworkScript.player.nextWarStartTime - StaticData.now;
			if (timeSpan.get_TotalSeconds() > 1)
			{
				GuiTimeTracker guiTimeTracker = new GuiTimeTracker((int)timeSpan.get_TotalSeconds(), this)
				{
					FontSize = 14,
					TextColor = GuiNewStyleBar.aquamarineColor,
					Font = GuiLabel.FontBold,
					boundries = new Rect(395f, 463f, 300f, 22f),
					Alignment = 4
				};
				this.forDelete.Add(guiTimeTracker);
			}
			return;
		}
		SortedList<byte, byte> sortedList = NetworkScript.player.factionGalaxyOwnership;
		if (WarScreenWindow.<>f__am$cache60 == null)
		{
			WarScreenWindow.<>f__am$cache60 = new Func<KeyValuePair<byte, byte>, bool>(null, (KeyValuePair<byte, byte> t) => t.get_Value() == 1);
		}
		int num = Enumerable.Count<KeyValuePair<byte, byte>>(Enumerable.Where<KeyValuePair<byte, byte>>(sortedList, WarScreenWindow.<>f__am$cache60));
		SortedList<byte, byte> sortedList1 = NetworkScript.player.factionGalaxyOwnership;
		if (WarScreenWindow.<>f__am$cache61 == null)
		{
			WarScreenWindow.<>f__am$cache61 = new Func<KeyValuePair<byte, byte>, bool>(null, (KeyValuePair<byte, byte> t) => t.get_Value() == 2);
		}
		int num1 = Enumerable.Count<KeyValuePair<byte, byte>>(Enumerable.Where<KeyValuePair<byte, byte>>(sortedList1, WarScreenWindow.<>f__am$cache61));
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(711f, 443f, 56f, 20f),
			text = num.ToString("N0"),
			FontSize = 16,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 5
		};
		base.AddGuiElement(guiLabel1);
		this.forDelete.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(328f, 443f, 56f, 20f),
			text = num1.ToString("N0"),
			FontSize = 16,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel2);
		this.forDelete.Add(guiLabel2);
		if (NetworkScript.player.factionOneAttackGalaxyKey != 0 || NetworkScript.player.factionTwoAttackGalaxyKey != 0)
		{
			GuiTexture guiTexture1 = new GuiTexture();
			guiTexture1.SetTexture("WarScreenWindow", "galaxyUnderAttack");
			guiTexture1.X = 276f;
			guiTexture1.Y = 396f;
			base.AddGuiElement(guiTexture1);
			this.forDelete.Add(guiTexture1);
			GuiLabel guiLabel3 = new GuiLabel()
			{
				boundries = new Rect(340f, 432f, 430f, 20f),
				text = string.Empty,
				FontSize = 12,
				TextColor = GuiNewStyleBar.redColor,
				Alignment = 4
			};
			base.AddGuiElement(guiLabel3);
			this.forDelete.Add(guiLabel3);
			if (NetworkScript.player.factionOneAttackGalaxyKey != 0)
			{
				GuiLabel guiLabel4 = guiLabel3;
				string str = StaticData.Translate("key_war_in_progress_faction_one_attack");
				LevelMap[] levelMapArray5 = StaticData.allGalaxies;
				if (WarScreenWindow.<>f__am$cache62 == null)
				{
					WarScreenWindow.<>f__am$cache62 = new Func<LevelMap, bool>(null, (LevelMap t) => t.galaxyKey == NetworkScript.player.factionOneAttackGalaxyKey);
				}
				guiLabel4.text = string.Format(str, StaticData.Translate(Enumerable.First<LevelMap>(Enumerable.Where<LevelMap>(levelMapArray5, WarScreenWindow.<>f__am$cache62)).nameUI));
			}
			GuiLabel guiLabel5 = new GuiLabel()
			{
				boundries = new Rect(340f, 406f, 430f, 20f),
				text = string.Empty,
				FontSize = 12,
				TextColor = GuiNewStyleBar.redColor,
				Alignment = 4
			};
			base.AddGuiElement(guiLabel5);
			this.forDelete.Add(guiLabel5);
			if (NetworkScript.player.factionTwoAttackGalaxyKey != 0)
			{
				GuiLabel guiLabel6 = guiLabel5;
				string str1 = StaticData.Translate("key_war_in_progress_faction_two_attack");
				LevelMap[] levelMapArray6 = StaticData.allGalaxies;
				if (WarScreenWindow.<>f__am$cache63 == null)
				{
					WarScreenWindow.<>f__am$cache63 = new Func<LevelMap, bool>(null, (LevelMap t) => t.galaxyKey == NetworkScript.player.factionTwoAttackGalaxyKey);
				}
				guiLabel6.text = string.Format(str1, StaticData.Translate(Enumerable.First<LevelMap>(Enumerable.Where<LevelMap>(levelMapArray6, WarScreenWindow.<>f__am$cache63)).nameUI));
			}
		}
		for (int i = 0; i < num; i++)
		{
			GuiTexture guiTexture2 = new GuiTexture();
			guiTexture2.SetTexture("WarScreenWindow", "progressVindesxis");
			guiTexture2.X = (float)(633 - 61 * i);
			guiTexture2.Y = 460f;
			base.AddGuiElement(guiTexture2);
			this.forDelete.Add(guiTexture2);
		}
		for (int j = 0; j < num1; j++)
		{
			GuiTexture guiTexture3 = new GuiTexture();
			guiTexture3.SetTexture("WarScreenWindow", "progressRegia");
			guiTexture3.X = (float)(388 + 61 * j);
			guiTexture3.Y = 460f;
			base.AddGuiElement(guiTexture3);
			this.forDelete.Add(guiTexture3);
		}
		for (int k = num1; k < Math.Max(5 - num, num1); k++)
		{
			GuiTexture guiTexture4 = new GuiTexture();
			guiTexture4.SetTexture("WarScreenWindow", "progressEmpty");
			guiTexture4.X = (float)(388 + 61 * k);
			guiTexture4.Y = 460f;
			base.AddGuiElement(guiTexture4);
			this.forDelete.Add(guiTexture4);
		}
	}

	private void CreateWeeklyChallenge()
	{
		switch (this.selectedSubsection)
		{
			case WarScreenSubsection.Overview:
			{
				this.DrawOverview(NetworkScript.player.playerBelongings.factionWarDay);
				this.DrawYourContribution();
				break;
			}
			case WarScreenSubsection.Day1:
			{
				this.DrawDay1Content();
				break;
			}
			case WarScreenSubsection.Day2:
			{
				this.DrawDay2Content();
				break;
			}
			case WarScreenSubsection.Day3:
			{
				this.DrawDay3Content();
				break;
			}
			case WarScreenSubsection.Day4:
			{
				this.DrawDay4Content();
				break;
			}
			case WarScreenSubsection.Day5:
			{
				this.DrawDay5Content();
				break;
			}
			case WarScreenSubsection.Day6:
			{
				this.DrawDay6Content();
				break;
			}
		}
	}

	private void DrawBestCandidates()
	{
		Color color = GuiNewStyleBar.blueColor;
		long value = (long)0;
		int num = 0;
		foreach (KeyValuePair<string, long> topCouncilCandidat in this.topCouncilCandidats)
		{
			if (topCouncilCandidat.get_Key() == NetworkScript.player.playerBelongings.playerName)
			{
				value = topCouncilCandidat.get_Value();
			}
			if (topCouncilCandidat.get_Value() != 0)
			{
				color = (num != 0 ? GuiNewStyleBar.blueColor : GuiNewStyleBar.orangeColor);
				GuiLabel guiLabel = new GuiLabel()
				{
					boundries = new Rect(502f, (float)(284 + num * 21), 24f, 14f),
					text = string.Format("{0}.", num + 1),
					FontSize = 12,
					TextColor = color,
					Alignment = 3
				};
				base.AddGuiElement(guiLabel);
				this.forDelete.Add(guiLabel);
				GuiLabel guiLabel1 = new GuiLabel()
				{
					boundries = new Rect(525f, (float)(284 + num * 21), 240f, 14f),
					text = topCouncilCandidat.get_Key(),
					FontSize = 12,
					TextColor = color,
					Alignment = 3
				};
				base.AddGuiElement(guiLabel1);
				this.forDelete.Add(guiLabel1);
				GuiLabel guiLabel2 = new GuiLabel()
				{
					boundries = new Rect(770f, (float)(284 + num * 21), 80f, 14f),
					text = topCouncilCandidat.get_Value().ToString("N0"),
					FontSize = 12,
					TextColor = color,
					Alignment = 3
				};
				base.AddGuiElement(guiLabel2);
				this.forDelete.Add(guiLabel2);
				GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
				guiButtonFixed.SetTexture("WarScreenWindow", "btnCoppyName");
				guiButtonFixed.X = 857f;
				guiButtonFixed.Y = (float)(283 + num * 21);
				guiButtonFixed.eventHandlerParam.customData = topCouncilCandidat.get_Key();
				guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnCopyNicknameClicked);
				guiButtonFixed.Caption = string.Empty;
				base.AddGuiElement(guiButtonFixed);
				this.forDelete.Add(guiButtonFixed);
				num++;
			}
		}
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(525f, 494f, 240f, 14f),
			text = NetworkScript.player.playerBelongings.playerName,
			FontSize = 12,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel3);
		this.forDelete.Add(guiLabel3);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			boundries = new Rect(770f, 494f, 80f, 14f),
			text = value.ToString("N0"),
			FontSize = 12,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel4);
		this.forDelete.Add(guiLabel4);
		GuiButtonFixed action = new GuiButtonFixed();
		action.SetTexture("WarScreenWindow", "btnCoppyName");
		action.X = 857f;
		action.Y = 493f;
		action.eventHandlerParam.customData = NetworkScript.player.playerBelongings.playerName;
		action.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnCopyNicknameClicked);
		action.Caption = string.Empty;
		base.AddGuiElement(action);
		this.forDelete.Add(action);
	}

	private void DrawCouncils()
	{
		List<FactionCouncilMember> list = this.factionOneCouncil;
		if (WarScreenWindow.<>f__am$cache59 == null)
		{
			WarScreenWindow.<>f__am$cache59 = new Func<FactionCouncilMember, bool>(null, (FactionCouncilMember t) => t.rank == 1);
		}
		FactionCouncilMember factionCouncilMember = Enumerable.FirstOrDefault<FactionCouncilMember>(Enumerable.Where<FactionCouncilMember>(list, WarScreenWindow.<>f__am$cache59));
		List<FactionCouncilMember> list1 = this.factionTwoCouncil;
		if (WarScreenWindow.<>f__am$cache5A == null)
		{
			WarScreenWindow.<>f__am$cache5A = new Func<FactionCouncilMember, bool>(null, (FactionCouncilMember t) => t.rank == 1);
		}
		FactionCouncilMember factionCouncilMember1 = Enumerable.FirstOrDefault<FactionCouncilMember>(Enumerable.Where<FactionCouncilMember>(list1, WarScreenWindow.<>f__am$cache5A));
		if (factionCouncilMember != null)
		{
			GuiLabel guiLabel = new GuiLabel()
			{
				boundries = new Rect(570f, 247f, 280f, 20f),
				text = (!string.IsNullOrEmpty(factionCouncilMember.guildTag) ? string.Format("[{0}] {1}", factionCouncilMember.guildTag, factionCouncilMember.nickName) : factionCouncilMember.nickName),
				FontSize = 14,
				TextColor = GuiNewStyleBar.blueColor
			};
			base.AddGuiElement(guiLabel);
			this.forDelete.Add(guiLabel);
			GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
			guiButtonFixed.SetTexture("WarScreenWindow", "iconProfile");
			guiButtonFixed.X = 860f;
			guiButtonFixed.Y = 258f;
			guiButtonFixed.eventHandlerParam.customData = factionCouncilMember.nickName;
			guiButtonFixed.Clicked = new Action<EventHandlerParam>(null, WarScreenWindow.OnProfilBtnClick);
			guiButtonFixed.Caption = string.Empty;
			guiButtonFixed.Alignment = 4;
			guiButtonFixed.SetRegularFont();
			guiButtonFixed.FontSize = 10;
			base.AddGuiElement(guiButtonFixed);
			this.forDelete.Add(guiButtonFixed);
		}
		if (factionCouncilMember1 != null)
		{
			GuiLabel guiLabel1 = new GuiLabel()
			{
				boundries = new Rect(225f, 247f, 280f, 20f),
				text = (!string.IsNullOrEmpty(factionCouncilMember1.guildTag) ? string.Format("[{0}] {1}", factionCouncilMember1.guildTag, factionCouncilMember1.nickName) : factionCouncilMember1.nickName),
				FontSize = 14,
				TextColor = GuiNewStyleBar.blueColor
			};
			base.AddGuiElement(guiLabel1);
			this.forDelete.Add(guiLabel1);
			GuiButtonFixed action = new GuiButtonFixed();
			action.SetTexture("WarScreenWindow", "iconProfile");
			action.X = 518f;
			action.Y = 258f;
			action.eventHandlerParam.customData = factionCouncilMember1.nickName;
			action.Clicked = new Action<EventHandlerParam>(null, WarScreenWindow.OnProfilBtnClick);
			action.Caption = string.Empty;
			action.Alignment = 4;
			action.SetRegularFont();
			action.FontSize = 10;
			base.AddGuiElement(action);
			this.forDelete.Add(action);
		}
		int count = this.factionOneCouncil.get_Count();
		for (int i = 0; i < count; i++)
		{
			int num = i + 1;
			if (num < count && this.factionOneCouncil.get_Item(num).rank != 1)
			{
				GuiLabel guiLabel2 = new GuiLabel()
				{
					boundries = new Rect(575f, (float)(326 + i * 21), 25f, 14f),
					text = this.factionOneCouncil.get_Item(num).rank.ToString(),
					FontSize = 12,
					TextColor = GuiNewStyleBar.blueColor
				};
				base.AddGuiElement(guiLabel2);
				this.forDelete.Add(guiLabel2);
				GuiLabel guiLabel3 = new GuiLabel()
				{
					boundries = new Rect(600f, (float)(326 + i * 21), 250f, 14f),
					text = (!string.IsNullOrEmpty(this.factionOneCouncil.get_Item(num).guildTag) ? string.Format("[{0}] {1}", this.factionOneCouncil.get_Item(num).guildTag, this.factionOneCouncil.get_Item(num).nickName) : this.factionOneCouncil.get_Item(num).nickName),
					FontSize = 12,
					TextColor = GuiNewStyleBar.blueColor
				};
				base.AddGuiElement(guiLabel3);
				this.forDelete.Add(guiLabel3);
				GuiButtonFixed rect = new GuiButtonFixed();
				rect.SetTexture("WarScreenWindow", "iconProfile");
				rect.boundries = new Rect(858f, (float)(325 + i * 21), 18f, 15f);
				rect.eventHandlerParam.customData = this.factionOneCouncil.get_Item(num).nickName;
				rect.Clicked = new Action<EventHandlerParam>(null, WarScreenWindow.OnProfilBtnClick);
				rect.Caption = string.Empty;
				rect.Alignment = 4;
				rect.SetRegularFont();
				rect.FontSize = 10;
				base.AddGuiElement(rect);
				this.forDelete.Add(rect);
			}
		}
		int count1 = this.factionTwoCouncil.get_Count();
		for (int j = 0; j < count1; j++)
		{
			int num1 = j + 1;
			if (num1 < count1 && this.factionTwoCouncil.get_Item(num1).rank != 1)
			{
				GuiLabel guiLabel4 = new GuiLabel()
				{
					boundries = new Rect(230f, (float)(326 + j * 21), 25f, 14f),
					text = this.factionTwoCouncil.get_Item(num1).rank.ToString(),
					FontSize = 12,
					TextColor = GuiNewStyleBar.blueColor
				};
				base.AddGuiElement(guiLabel4);
				this.forDelete.Add(guiLabel4);
				GuiLabel guiLabel5 = new GuiLabel()
				{
					boundries = new Rect(255f, (float)(326 + j * 21), 250f, 14f),
					text = (!string.IsNullOrEmpty(this.factionTwoCouncil.get_Item(num1).guildTag) ? string.Format("[{0}] {1}", this.factionTwoCouncil.get_Item(num1).guildTag, this.factionTwoCouncil.get_Item(num1).nickName) : this.factionTwoCouncil.get_Item(num1).nickName),
					FontSize = 12,
					TextColor = GuiNewStyleBar.blueColor
				};
				base.AddGuiElement(guiLabel5);
				this.forDelete.Add(guiLabel5);
				GuiButtonFixed item = new GuiButtonFixed();
				item.SetTexture("WarScreenWindow", "iconProfile");
				item.boundries = new Rect(515f, (float)(325 + j * 21), 18f, 15f);
				item.eventHandlerParam.customData = this.factionTwoCouncil.get_Item(num1).nickName;
				item.Clicked = new Action<EventHandlerParam>(null, WarScreenWindow.OnProfilBtnClick);
				item.Caption = string.Empty;
				item.Alignment = 4;
				item.SetRegularFont();
				item.FontSize = 10;
				base.AddGuiElement(item);
				this.forDelete.Add(item);
			}
		}
	}

	private void DrawDay1Content()
	{
		// 
		// Current member / type: System.Void WarScreenWindow::DrawDay1Content()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void DrawDay1Content()
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

	private void DrawDay2Content()
	{
		// 
		// Current member / type: System.Void WarScreenWindow::DrawDay2Content()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void DrawDay2Content()
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

	private void DrawDay3Content()
	{
		WarScreenWindow.DayInfo[] dayInfoArray = WarScreenWindow.DayInfo.allDays;
		if (WarScreenWindow.<>f__am$cache55 == null)
		{
			WarScreenWindow.<>f__am$cache55 = new Func<WarScreenWindow.DayInfo, bool>(null, (WarScreenWindow.DayInfo t) => t.day == 3);
		}
		this.DrawRegularDayCommonParts(Enumerable.First<WarScreenWindow.DayInfo>(Enumerable.Where<WarScreenWindow.DayInfo>((IEnumerable<WarScreenWindow.DayInfo>)dayInfoArray, WarScreenWindow.<>f__am$cache55)));
	}

	private void DrawDay4Content()
	{
		WarScreenWindow.DayInfo[] dayInfoArray = WarScreenWindow.DayInfo.allDays;
		if (WarScreenWindow.<>f__am$cache56 == null)
		{
			WarScreenWindow.<>f__am$cache56 = new Func<WarScreenWindow.DayInfo, bool>(null, (WarScreenWindow.DayInfo t) => t.day == 4);
		}
		this.DrawRegularDayCommonParts(Enumerable.First<WarScreenWindow.DayInfo>(Enumerable.Where<WarScreenWindow.DayInfo>((IEnumerable<WarScreenWindow.DayInfo>)dayInfoArray, WarScreenWindow.<>f__am$cache56)));
	}

	private void DrawDay5Content()
	{
		WarScreenWindow.DayInfo[] dayInfoArray = WarScreenWindow.DayInfo.allDays;
		if (WarScreenWindow.<>f__am$cache57 == null)
		{
			WarScreenWindow.<>f__am$cache57 = new Func<WarScreenWindow.DayInfo, bool>(null, (WarScreenWindow.DayInfo t) => t.day == 5);
		}
		this.DrawRegularDayCommonParts(Enumerable.First<WarScreenWindow.DayInfo>(Enumerable.Where<WarScreenWindow.DayInfo>((IEnumerable<WarScreenWindow.DayInfo>)dayInfoArray, WarScreenWindow.<>f__am$cache57)));
	}

	private void DrawDay6Content()
	{
		WarScreenWindow.DayInfo[] dayInfoArray = WarScreenWindow.DayInfo.allDays;
		if (WarScreenWindow.<>f__am$cache58 == null)
		{
			WarScreenWindow.<>f__am$cache58 = new Func<WarScreenWindow.DayInfo, bool>(null, (WarScreenWindow.DayInfo t) => t.day == 6);
		}
		this.DrawRegularDayCommonParts(Enumerable.First<WarScreenWindow.DayInfo>(Enumerable.Where<WarScreenWindow.DayInfo>((IEnumerable<WarScreenWindow.DayInfo>)dayInfoArray, WarScreenWindow.<>f__am$cache58)));
	}

	private void DrawDayScoreInfo(DayOfWeek day)
	{
		int num = 0;
		string[] strArray = new string[5];
		string[] str = new string[5];
		string empty = string.Empty;
		string str1 = StaticData.Translate("key_weekly_challenge_day_score_table_title_score");
		switch (day)
		{
			case 3:
			{
				num = 2;
				empty = StaticData.Translate("key_weekly_challenge_day_3_score_table_title");
				strArray[0] = StaticData.Translate("key_weekly_challenge_day_3_score_table_row_1_info");
				strArray[1] = StaticData.Translate("key_weekly_challenge_day_3_score_table_row_2_info");
				str[0] = 3000.ToString("N0");
				str[1] = 1000.ToString("N0");
				break;
			}
			case 4:
			{
				num = 3;
				empty = StaticData.Translate("key_weekly_challenge_day_4_score_table_title");
				strArray[0] = StaticData.Translate("key_weekly_challenge_day_4_score_table_row_1_info");
				strArray[1] = StaticData.Translate("key_weekly_challenge_day_4_score_table_row_2_info");
				strArray[2] = StaticData.Translate("key_weekly_challenge_day_4_score_table_row_3_info");
				str[0] = 600.ToString("N0");
				str[1] = 1200.ToString("N0");
				str[2] = 2400.ToString("N0");
				break;
			}
			case 5:
			{
				num = 5;
				empty = StaticData.Translate("key_weekly_challenge_day_5_score_table_title");
				strArray[0] = string.Format("{0} - {1}", "01", 19);
				strArray[1] = string.Format("{0} - {1}", 20, 29);
				strArray[2] = string.Format("{0} - {1}", 30, 39);
				strArray[3] = string.Format("{0} - {1}", 40, 49);
				strArray[4] = string.Format("{0} - {1}", 50, 56);
				str[0] = 400.ToString("N0");
				str[1] = 200.ToString("N0");
				str[2] = 100.ToString("N0");
				str[3] = 75.ToString("N0");
				str[4] = 50.ToString("N0");
				break;
			}
			case 6:
			{
				num = 2;
				empty = StaticData.Translate("key_weekly_challenge_day_6_score_table_title");
				strArray[0] = string.Format(StaticData.Translate("key_weekly_challenge_day_6_score_table_row_1_info"), 10);
				strArray[1] = string.Format(StaticData.Translate("key_weekly_challenge_day_6_score_table_row_2_info"), 10);
				str[0] = 1200.ToString("N0");
				str[1] = 600.ToString("N0");
				break;
			}
		}
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(235f, 220f, 220f, 16f),
			text = empty,
			FontSize = 12,
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor,
			Clipping = 1
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(472f, 220f, 98f, 16f),
			text = str1,
			FontSize = 12,
			Alignment = 5,
			TextColor = GuiNewStyleBar.blueColor,
			Clipping = 1
		};
		base.AddGuiElement(guiLabel1);
		this.forDelete.Add(guiLabel1);
		for (int i = 0; i < num; i++)
		{
			GuiLabel guiLabel2 = new GuiLabel()
			{
				boundries = new Rect(243f, (float)(241 + i * 21), 220f, 16f),
				text = strArray[i],
				FontSize = 12,
				Alignment = 3,
				TextColor = GuiNewStyleBar.blueColor,
				Clipping = 1
			};
			base.AddGuiElement(guiLabel2);
			this.forDelete.Add(guiLabel2);
			GuiLabel guiLabel3 = new GuiLabel()
			{
				boundries = new Rect(480f, (float)(241 + i * 21), 90f, 16f),
				text = str[i],
				FontSize = 12,
				Alignment = 5,
				TextColor = GuiNewStyleBar.blueColor,
				Clipping = 1
			};
			base.AddGuiElement(guiLabel3);
			this.forDelete.Add(guiLabel3);
		}
	}

	private void DrawFactionCouncils()
	{
		// 
		// Current member / type: System.Void WarScreenWindow::DrawFactionCouncils()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void DrawFactionCouncils()
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

	private void DrawGalaxyInfo(int galaxyId, byte state, float offsetX, float offsetY, string galaxyName, string bonusOneInfo, string bonusTwoInfo = "")
	{
		string str = "FrameworkGUI";
		string str1 = "empty";
		switch (state)
		{
			case 0:
			{
				str = "WarScreenWindow";
				str1 = "contestedStamp";
				break;
			}
			case 1:
			{
				str = "WarScreenWindow";
				str1 = "vindexisStamp";
				break;
			}
			case 2:
			{
				str = "WarScreenWindow";
				str1 = "regiaStamp";
				break;
			}
		}
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture(str, str1);
		guiTexture.boundries = new Rect(offsetX + 34f, offsetY + 42f, 83f, 60f);
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("WarScreenWindow", "galaxyTitleNormalFrame");
		guiTexture1.X = offsetX - 20f;
		guiTexture1.Y = offsetY + 16f;
		base.AddGuiElement(guiTexture1);
		this.forDelete.Add(guiTexture1);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = guiTexture1.boundries,
			text = galaxyName,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 4
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiTexture guiTexture2 = new GuiTexture();
		guiTexture2.SetTexture("WarScreenWindow", "galaxyBonusNormalFrame");
		guiTexture2.X = offsetX - 20f;
		guiTexture2.Y = offsetY + 100f;
		base.AddGuiElement(guiTexture2);
		this.forDelete.Add(guiTexture2);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = guiTexture2.boundries,
			text = bonusOneInfo,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 4
		};
		base.AddGuiElement(guiLabel1);
		this.forDelete.Add(guiLabel1);
		GuiTexture guiTexture3 = null;
		GuiLabel guiLabel2 = null;
		if (!string.IsNullOrEmpty(bonusTwoInfo))
		{
			guiTexture3 = new GuiTexture();
			guiTexture3.SetTexture("WarScreenWindow", "galaxyBonusNormalFrame");
			guiTexture3.X = offsetX - 20f;
			guiTexture3.Y = offsetY + 123f;
			base.AddGuiElement(guiTexture3);
			this.forDelete.Add(guiTexture3);
			guiLabel2 = new GuiLabel()
			{
				boundries = guiTexture3.boundries,
				text = bonusTwoInfo,
				FontSize = 12,
				TextColor = GuiNewStyleBar.blueColor,
				Alignment = 4
			};
			base.AddGuiElement(guiLabel2);
			this.forDelete.Add(guiLabel2);
		}
	}

	private void DrawGalaxyVoteChart()
	{
		WarScreenWindow.<DrawGalaxyVoteChart>c__AnonStorey7F variable = null;
		for (int i = 0; i < this.galaxyVote.get_Count(); i++)
		{
			GuiLabel guiLabel = new GuiLabel()
			{
				boundries = new Rect(508f, (float)(408 + i * 21), 220f, 18f),
				text = string.Format("{0}. {1}", i + 1, StaticData.Translate(Enumerable.First<LevelMap>(Enumerable.Where<LevelMap>(StaticData.allGalaxies, new Func<LevelMap, bool>(variable, (LevelMap t) => t.galaxyKey == this.<>f__this.galaxyVote.get_Item(this.i).get_Key()))).nameUI)),
				FontSize = 12,
				TextColor = GuiNewStyleBar.blueColor,
				Alignment = 3
			};
			base.AddGuiElement(guiLabel);
			this.forDelete.Add(guiLabel);
			GuiLabel guiLabel1 = new GuiLabel()
			{
				boundries = new Rect(740f, (float)(408 + i * 21), 150f, 18f)
			};
			long value = this.galaxyVote.get_Item(i).get_Value();
			guiLabel1.text = string.Format("{0}", value.ToString("N0"));
			guiLabel1.FontSize = 12;
			guiLabel1.TextColor = GuiNewStyleBar.blueColor;
			guiLabel1.Alignment = 3;
			base.AddGuiElement(guiLabel1);
			this.forDelete.Add(guiLabel1);
		}
	}

	private void DrawGalaxyVoteInfo(float offsetX, float offsetY, int galaxyKey, string galaxyName, string bonusOneInfo, string bonusTwoInfo = "")
	{
		WarScreenWindow.GalaxyVoteElements galaxyVoteElement = new WarScreenWindow.GalaxyVoteElements();
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTextureNormal("FrameworkGUI", "empty");
		guiButtonFixed.SetTextureDisabled("FrameworkGUI", "empty");
		guiButtonFixed.SetTextureHover("WarScreenWindow", "galaxyVoteHoverFrame");
		guiButtonFixed.SetTextureClicked("WarScreenWindow", "galaxyVoteActiveFrame");
		guiButtonFixed.boundries = new Rect(offsetX, offsetY, 150f, 150f);
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.behaviourKeepClicked = true;
		guiButtonFixed.groupId = 100;
		guiButtonFixed.eventHandlerParam.customData = galaxyKey;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnGalaxyBtnClicked);
		guiButtonFixed.IsClicked = this.selectedGalaxyId == galaxyKey;
		guiButtonFixed.isEnabled = NetworkScript.player.factionGalaxyOwnership.get_Item((byte)galaxyKey) != NetworkScript.player.vessel.fractionId;
		base.AddGuiElement(guiButtonFixed);
		this.forDelete.Add(guiButtonFixed);
		if (guiButtonFixed.isEnabled)
		{
			guiButtonFixed.hoverParam = galaxyKey;
			guiButtonFixed.Hovered = new Action<object, bool>(this, WarScreenWindow.OnGalaxyBtnHover);
		}
		string str = "FrameworkGUI";
		string str1 = "empty";
		switch (NetworkScript.player.factionGalaxyOwnership.get_Item((byte)galaxyKey))
		{
			case 0:
			{
				str = "WarScreenWindow";
				str1 = "contestedStamp";
				break;
			}
			case 1:
			{
				str = "WarScreenWindow";
				str1 = "vindexisStamp";
				break;
			}
			case 2:
			{
				str = "WarScreenWindow";
				str1 = "regiaStamp";
				break;
			}
		}
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture(str, str1);
		guiTexture.boundries = new Rect(offsetX + 34f, offsetY + 42f, 83f, 60f);
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		GuiTexture x = new GuiTexture();
		x.SetTexture("WarScreenWindow", "galaxyTitleNormalFrame");
		x.X = guiButtonFixed.X - 20f;
		x.Y = guiButtonFixed.Y + 16f;
		base.AddGuiElement(x);
		this.forDelete.Add(x);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = x.boundries,
			text = galaxyName,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 4
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiTexture y = new GuiTexture();
		y.SetTexture("WarScreenWindow", "galaxyBonusNormalFrame");
		y.X = guiButtonFixed.X - 20f;
		y.Y = guiButtonFixed.Y + 100f;
		base.AddGuiElement(y);
		this.forDelete.Add(y);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = y.boundries,
			text = bonusOneInfo,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 4
		};
		base.AddGuiElement(guiLabel1);
		this.forDelete.Add(guiLabel1);
		GuiTexture guiTexture1 = null;
		GuiLabel guiLabel2 = null;
		if (!string.IsNullOrEmpty(bonusTwoInfo))
		{
			guiTexture1 = new GuiTexture();
			guiTexture1.SetTexture("WarScreenWindow", "galaxyBonusNormalFrame");
			guiTexture1.X = guiButtonFixed.X - 20f;
			guiTexture1.Y = guiButtonFixed.Y + 123f;
			base.AddGuiElement(guiTexture1);
			this.forDelete.Add(guiTexture1);
			guiLabel2 = new GuiLabel()
			{
				boundries = guiTexture1.boundries,
				text = bonusTwoInfo,
				FontSize = 12,
				TextColor = GuiNewStyleBar.blueColor,
				Alignment = 4
			};
			base.AddGuiElement(guiLabel2);
			this.forDelete.Add(guiLabel2);
		}
		galaxyVoteElement.galaxyId = galaxyKey;
		galaxyVoteElement.titleFrame = x;
		galaxyVoteElement.titleLbl = guiLabel;
		galaxyVoteElement.bonusOneFrame = y;
		galaxyVoteElement.bonusOneLbl = guiLabel1;
		galaxyVoteElement.bonusTwoFrame = guiTexture1;
		galaxyVoteElement.bonusTwoLbl = guiLabel2;
		this.galaxyVoteElementsForUpdate.Add(galaxyVoteElement);
	}

	private void DrawOverview(DayOfWeek today)
	{
		for (int i = 0; i < (int)WarScreenWindow.DayInfo.allDays.Length; i++)
		{
			WarScreenWindow.DayInfo dayInfo = WarScreenWindow.DayInfo.allDays[i];
			bool flag = dayInfo.day == today;
			Color color = GuiNewStyleBar.blueColor;
			string empty = string.Empty;
			if (!flag)
			{
				empty = (dayInfo.day != null ? "dayFrame" : "frameBottom");
			}
			else
			{
				color = GuiNewStyleBar.aquamarineColor;
				empty = (dayInfo.day != null ? "dayFrameToday" : "frameBottom");
			}
			GuiTexture guiTexture = new GuiTexture();
			guiTexture.SetTexture("WarScreenWindow", empty);
			guiTexture.X = dayInfo.posX;
			guiTexture.Y = dayInfo.posY;
			base.AddGuiElement(guiTexture);
			this.forDelete.Add(guiTexture);
			GuiTexture guiTexture1 = new GuiTexture();
			guiTexture1.SetTexture("WarScreenWindow", "icon");
			guiTexture1.X = dayInfo.posX + 4f;
			guiTexture1.Y = dayInfo.posY + 5f;
			base.AddGuiElement(guiTexture1);
			this.forDelete.Add(guiTexture1);
			GuiTexture guiTexture2 = new GuiTexture();
			guiTexture2.SetTexture("WarScreenWindow", dayInfo.dayIconAssetName);
			guiTexture2.boundries = guiTexture1.boundries;
			base.AddGuiElement(guiTexture2);
			this.forDelete.Add(guiTexture2);
			GuiLabel guiLabel = new GuiLabel()
			{
				boundries = new Rect(dayInfo.posX + 34f, dayInfo.posY + 5f, 270f, 22f),
				text = StaticData.Translate(dayInfo.dayLblText).ToUpper(),
				FontSize = 14,
				TextColor = color,
				Alignment = 3
			};
			base.AddGuiElement(guiLabel);
			this.forDelete.Add(guiLabel);
			if (dayInfo.day != null)
			{
				string str = dayInfo.StatusAssetName(NetworkScript.player.playerBelongings, today);
				GuiTexture guiTexture3 = new GuiTexture();
				guiTexture3.SetTexture("WarScreenWindow", str);
				guiTexture3.X = dayInfo.posX + 310f;
				guiTexture3.Y = dayInfo.posY + 6f;
				base.AddGuiElement(guiTexture3);
				this.forDelete.Add(guiTexture3);
				GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
				guiButtonFixed.SetTexture("WarScreenWindow", "dayBtn");
				guiButtonFixed.X = dayInfo.posX + 296f;
				guiButtonFixed.Y = dayInfo.posY + 37f;
				guiButtonFixed.Caption = string.Empty;
				guiButtonFixed.eventHandlerParam.customData = dayInfo.day;
				guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnSubSelectClicked);
				guiButtonFixed.isEnabled = flag;
				base.AddGuiElement(guiButtonFixed);
				this.forDelete.Add(guiButtonFixed);
				GuiLabel guiLabel1 = new GuiLabel()
				{
					boundries = new Rect(dayInfo.posX + 18f, dayInfo.posY + 43f, 270f, 42f),
					text = StaticData.Translate(dayInfo.dayShortInfoText),
					FontSize = 12,
					TextColor = color
				};
				base.AddGuiElement(guiLabel1);
				this.forDelete.Add(guiLabel1);
				if (flag)
				{
					TimeSpan timeSpan = NetworkScript.player.playerBelongings.factionWarDayEnd - StaticData.now;
					if (timeSpan.get_TotalSeconds() > 1)
					{
						GuiTimeTracker guiTimeTracker = new GuiTimeTracker((int)timeSpan.get_TotalSeconds(), this)
						{
							FontSize = 14,
							TextColor = GuiNewStyleBar.aquamarineColor,
							boundries = new Rect(dayInfo.posX + 34f, dayInfo.posY + 5f, 270f, 22f),
							Alignment = 5
						};
						this.forDelete.Add(guiTimeTracker);
					}
				}
			}
			else if (flag)
			{
				TimeSpan timeSpan1 = NetworkScript.player.playerBelongings.factionWarDayEnd - StaticData.now;
				if (timeSpan1.get_TotalSeconds() > 1)
				{
					GuiTimeTracker guiTimeTracker1 = new GuiTimeTracker((int)timeSpan1.get_TotalSeconds(), this)
					{
						FontSize = 14,
						TextColor = GuiNewStyleBar.aquamarineColor,
						boundries = new Rect(dayInfo.posX + 400f, dayInfo.posY + 5f, 270f, 22f),
						Alignment = 5
					};
					this.forDelete.Add(guiTimeTracker1);
				}
			}
		}
	}

	private void DrawRegularDayCommonParts(WarScreenWindow.DayInfo day)
	{
		EventHandlerParam eventHandlerParam;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("WarScreenWindow", "regularDaysFrame");
		guiTexture.X = 209f;
		guiTexture.Y = 53f;
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(214f, 58f, 400f, 16f),
			text = StaticData.Translate(day.dayLblText),
			FontSize = 16,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(226f, 89f, 620f, 100f),
			text = StaticData.Translate(day.dayDescriptionText),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel1);
		this.forDelete.Add(guiLabel1);
		int num = 1;
		int num1 = 3;
		if (day.day == FactionWarStats.bonusIncomDay)
		{
			num = 2;
			num1 = 6;
		}
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(226f, 89f, 620f, 100f),
			text = string.Format(StaticData.Translate("key_weekly_challenge_extraction_income"), num, num1),
			FontSize = 12,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 6
		};
		base.AddGuiElement(guiLabel2);
		this.forDelete.Add(guiLabel2);
		TimeSpan timeSpan = NetworkScript.player.playerBelongings.factionWarDayEnd - StaticData.now;
		if (timeSpan.get_TotalSeconds() > 1)
		{
			GuiTimeTracker guiTimeTracker = new GuiTimeTracker((int)timeSpan.get_TotalSeconds(), this)
			{
				FontSize = 14,
				TextColor = GuiNewStyleBar.blueColor,
				boundries = new Rect(790f, 58f, 100f, 22f),
				Alignment = 5
			};
			this.forDelete.Add(guiTimeTracker);
		}
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("PoiScreenWindow", "btnBack");
		guiButtonFixed.X = 850f;
		guiButtonFixed.Y = 95f;
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnSubSelectClicked);
		base.AddGuiElement(guiButtonFixed);
		this.forDelete.Add(guiButtonFixed);
		this.DrawDayScoreInfo(day.day);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(609f, 237f, 258f, 16f),
			text = StaticData.Translate("key_faction_war_regular_day_your_progress_lbl"),
			FontSize = 12,
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel3);
		this.forDelete.Add(guiLabel3);
		bool flag = false;
		int num2 = 0;
		if (NetworkScript.player.playerBelongings.rewardForDayProgressCollected3)
		{
			num2 = 12000;
		}
		else if (NetworkScript.player.playerBelongings.rewardForDayProgressCollected2)
		{
			num2 = 12000;
			flag = NetworkScript.player.playerBelongings.factionWarDayScore >= num2;
		}
		else if (!NetworkScript.player.playerBelongings.rewardForDayProgressCollected1)
		{
			num2 = 3000;
			flag = NetworkScript.player.playerBelongings.factionWarDayScore >= num2;
		}
		else
		{
			num2 = 6000;
			flag = NetworkScript.player.playerBelongings.factionWarDayScore >= num2;
		}
		GuiLabel guiLabel4 = new GuiLabel()
		{
			boundries = new Rect(609f, 237f, 258f, 16f),
			text = string.Format("{0:N0}/{1:N0}", NetworkScript.player.playerBelongings.factionWarDayScore, num2),
			FontSize = 12,
			Alignment = 5,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel4);
		this.forDelete.Add(guiLabel4);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("WarScreenWindow", "progressUnderBgr");
		guiTexture1.X = 607f;
		guiTexture1.Y = 258f;
		base.AddGuiElement(guiTexture1);
		this.forDelete.Add(guiTexture1);
		if (NetworkScript.player.playerBelongings.factionWarDayScore > 0)
		{
			float single = 0f;
			single = (NetworkScript.player.playerBelongings.factionWarDayScore >= num2 ? 100f : 100f * (float)NetworkScript.player.playerBelongings.factionWarDayScore / (float)num2);
			single = 2.4f * single;
			GuiTexture guiTexture2 = new GuiTexture();
			guiTexture2.SetTexture("WarScreenWindow", "progressBarLeft");
			guiTexture2.X = 615f;
			guiTexture2.Y = 265f;
			base.AddGuiElement(guiTexture2);
			this.forDelete.Add(guiTexture2);
			GuiTexture rect = new GuiTexture();
			rect.SetTexture("WarScreenWindow", "progressBarMid");
			rect.boundries = new Rect(619f, 265f, single, 24f);
			base.AddGuiElement(rect);
			this.forDelete.Add(rect);
			GuiTexture _xMax = new GuiTexture();
			_xMax.SetTexture("WarScreenWindow", "progressBarRight");
			_xMax.X = rect.boundries.get_xMax();
			_xMax.Y = 265f;
			base.AddGuiElement(_xMax);
			this.forDelete.Add(_xMax);
		}
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.SetTexture("PoiScreenWindow", "tab_");
		guiButtonResizeable.X = 678f;
		guiButtonResizeable.Y = 308f;
		guiButtonResizeable.boundries.set_width(120f);
		guiButtonResizeable.Caption = StaticData.Translate("key_faction_war_regular_day_btn_get_reward");
		guiButtonResizeable.FontSize = 12;
		guiButtonResizeable.Alignment = 4;
		guiButtonResizeable.SetRegularFont();
		guiButtonResizeable.SetColor(GuiNewStyleBar.blueColor);
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnGetDayRewardClicked);
		guiButtonResizeable.isEnabled = (!flag ? false : !this.isWaitingRefresh);
		base.AddGuiElement(guiButtonResizeable);
		this.forDelete.Add(guiButtonResizeable);
		GuiLabel guiLabel5 = new GuiLabel()
		{
			boundries = new Rect(214f, 374f, 400f, 16f),
			text = StaticData.Translate("key_faction_war_regular_day_faction_score_lbl"),
			FontSize = 16,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel5);
		this.forDelete.Add(guiLabel5);
		GuiLabel guiLabel6 = new GuiLabel()
		{
			boundries = new Rect(326f, 441f, 205f, 22f),
			text = WarScreenWindow.factionTwoCurrentDayScore.ToString("N0"),
			FontSize = 18,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel6);
		this.forDelete.Add(guiLabel6);
		GuiLabel guiLabel7 = new GuiLabel()
		{
			boundries = new Rect(564f, 441f, 205f, 22f),
			text = WarScreenWindow.factionOneCurrentDayScore.ToString("N0"),
			FontSize = 18,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 5
		};
		base.AddGuiElement(guiLabel7);
		this.forDelete.Add(guiLabel7);
		int num3 = 0;
		if (WarScreenWindow.weeklyLoosingFaction == 1)
		{
			num3 = num3 + WarScreenWindow.weeklyLoosingFactionBonusPercent;
		}
		if (WarScreenWindow.dailyLoosingFaction == 1)
		{
			num3 = num3 + WarScreenWindow.dailyLoosingFactionBonusPercent;
		}
		if (num3 != 0)
		{
			GuiLabel drawTooltipWindow = new GuiLabel()
			{
				boundries = new Rect(307f, 482f, 496f, 16f),
				FontSize = 12,
				TextColor = GuiNewStyleBar.blueColor,
				Alignment = 5,
				isHoverAware = true,
				text = string.Format(StaticData.Translate("key_faction_war_loosing_faction_bonus"), num3)
			};
			GuiLabel guiLabel8 = drawTooltipWindow;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = string.Format(StaticData.Translate("key_faction_war_score_bonuses"), (WarScreenWindow.weeklyLoosingFaction != 1 ? 0 : WarScreenWindow.weeklyLoosingFactionBonusPercent), (WarScreenWindow.dailyLoosingFaction != 1 ? 0 : WarScreenWindow.dailyLoosingFactionBonusPercent)),
				customData2 = drawTooltipWindow
			};
			guiLabel8.tooltipWindowParam = eventHandlerParam;
			drawTooltipWindow.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			drawTooltipWindow.boundries.set_width(drawTooltipWindow.TextWidth);
			drawTooltipWindow.X = 803f - drawTooltipWindow.TextWidth;
			base.AddGuiElement(drawTooltipWindow);
			this.forDelete.Add(drawTooltipWindow);
		}
		int num4 = 0;
		if (WarScreenWindow.weeklyLoosingFaction == 2)
		{
			num4 = num4 + WarScreenWindow.weeklyLoosingFactionBonusPercent;
		}
		if (WarScreenWindow.dailyLoosingFaction == 2)
		{
			num4 = num4 + WarScreenWindow.dailyLoosingFactionBonusPercent;
		}
		if (num4 != 0)
		{
			GuiLabel drawTooltipWindow1 = new GuiLabel()
			{
				boundries = new Rect(307f, 482f, 496f, 16f),
				FontSize = 12,
				TextColor = GuiNewStyleBar.blueColor,
				Alignment = 3,
				isHoverAware = true,
				text = string.Format(StaticData.Translate("key_faction_war_loosing_faction_bonus"), num4)
			};
			GuiLabel guiLabel9 = drawTooltipWindow1;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = string.Format(StaticData.Translate("key_faction_war_score_bonuses"), (WarScreenWindow.weeklyLoosingFaction != 2 ? 0 : WarScreenWindow.weeklyLoosingFactionBonusPercent), (WarScreenWindow.dailyLoosingFaction != 2 ? 0 : WarScreenWindow.dailyLoosingFactionBonusPercent)),
				customData2 = drawTooltipWindow1
			};
			guiLabel9.tooltipWindowParam = eventHandlerParam;
			drawTooltipWindow1.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			drawTooltipWindow1.boundries.set_width(drawTooltipWindow1.TextWidth);
			base.AddGuiElement(drawTooltipWindow1);
			this.forDelete.Add(drawTooltipWindow1);
		}
	}

	private void DrawSponsoredVote()
	{
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(223f, 207f, 120f, 16f),
			text = StaticData.Translate("key_faction_war_vote_for_player_paid_ad_lbl"),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiLabel x = new GuiLabel()
		{
			boundries = new Rect(230f, 227f, 400f, 16f),
			text = this.paidAdNickName,
			FontSize = 12,
			TextColor = GuiNewStyleBar.orangeColor,
			Font = GuiLabel.FontBold
		};
		x.boundries.set_width(x.TextWidth);
		base.AddGuiElement(x);
		this.forDelete.Add(x);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(230f, 227f, 400f, 16f),
			text = this.paidAdSlogan,
			FontSize = 12
		};
		guiLabel1.boundries.set_width(guiLabel1.TextWidth);
		base.AddGuiElement(guiLabel1);
		this.forDelete.Add(guiLabel1);
		if (!string.IsNullOrEmpty(this.paidAdNickName))
		{
			GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
			guiButtonFixed.SetTexture("WarScreenWindow", "btnCoppyName");
			guiButtonFixed.X = 230f;
			guiButtonFixed.Y = 228f;
			guiButtonFixed.Caption = string.Empty;
			guiButtonFixed.eventHandlerParam.customData = this.paidAdNickName;
			guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnCopyNicknameClicked);
			base.AddGuiElement(guiButtonFixed);
			this.forDelete.Add(guiButtonFixed);
			float _width = 650f - x.boundries.get_width() - guiLabel1.boundries.get_width() - guiButtonFixed.boundries.get_width() - 20f;
			guiButtonFixed.X = 230f + _width / 2f;
			x.X = guiButtonFixed.X + guiButtonFixed.boundries.get_width() + 10f;
			guiLabel1.X = x.X + x.boundries.get_width() + 10f;
		}
		GuiButtonFixed action = new GuiButtonFixed();
		action.SetTexture("WarScreenWindow", "buttonSmall");
		action.X = 811f;
		action.Y = 206f;
		action.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnPaidAdClicked);
		action.Caption = StaticData.Translate("key_faction_war_vote_for_player_paid_ad_btn");
		action.Alignment = 4;
		action.SetRegularFont();
		action.FontSize = 10;
		base.AddGuiElement(action);
		this.forDelete.Add(action);
	}

	private void DrawVoteSection()
	{
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(223f, 290f, 250f, 16f),
			text = StaticData.Translate("key_faction_war_bank_lbl"),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(223f, 290f, 250f, 16f),
			text = string.Concat("$ ", this.factionBank.ToString("N0")),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 5
		};
		base.AddGuiElement(guiLabel1);
		this.forDelete.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(241f, 317f, 228f, 16f),
			text = StaticData.Translate("key_faction_war_vote_for_player_choose_plr_lbl"),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel2);
		this.forDelete.Add(guiLabel2);
		this.tb_vote_for_player_nickname = new GuiTextBox()
		{
			boundries = new Rect(237f, 335f, 235f, 33f)
		};
		this.tb_vote_for_player_nickname.SetFrameTexture("WarScreenWindow", "input");
		this.tb_vote_for_player_nickname.Alignment = 3;
		this.tb_vote_for_player_nickname.borderLeft = 30f;
		this.tb_vote_for_player_nickname.borderRight = 5f;
		this.tb_vote_for_player_nickname.borderTop = 0f;
		this.tb_vote_for_player_nickname.borderBottom = 4f;
		this.tb_vote_for_player_nickname.FontSize = 16;
		this.tb_vote_for_player_nickname.TextColor = GuiNewStyleBar.blueColor;
		base.AddGuiElement(this.tb_vote_for_player_nickname);
		this.forDelete.Add(this.tb_vote_for_player_nickname);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("WarScreenWindow", "iconInputPencil");
		guiTexture.X = 246f;
		guiTexture.Y = 343f;
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("WarScreenWindow", "warCommendation");
		guiTexture1.X = 240f;
		guiTexture1.Y = 383f;
		base.AddGuiElement(guiTexture1);
		this.forDelete.Add(guiTexture1);
		this.warCommendationVoteLbl = new GuiLabel()
		{
			boundries = new Rect(guiTexture1.X + 40f, guiTexture1.Y, 145f, 26f),
			Alignment = 3,
			text = string.Format("{0:N0}/{1:N0}", NetworkScript.player.playerBelongings.playerItems.get_WarCommendation(), 100),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.warCommendationVoteLbl);
		this.forDelete.Add(this.warCommendationVoteLbl);
		this.btnGetMoreWc = new GuiButtonResizeable();
		this.btnGetMoreWc.SetGetCommendationTexture();
		this.btnGetMoreWc.Caption = StaticData.Translate("key_nova_shop_item_btn_buy");
		this.btnGetMoreWc.Width = 120f;
		this.btnGetMoreWc.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnBuyWarCommendationClicked);
		this.btnGetMoreWc.X = 353f;
		this.btnGetMoreWc.Y = guiTexture1.Y - 2f;
		base.AddGuiElement(this.btnGetMoreWc);
		this.forDelete.Add(this.btnGetMoreWc);
		this.btnVoteWcMinus = new GuiButtonFixed();
		this.btnVoteWcMinus.SetTexture("PoiScreenWindow", "btnMinus");
		this.btnVoteWcMinus.Caption = string.Empty;
		this.btnVoteWcMinus.X = 241f;
		this.btnVoteWcMinus.Y = 425f;
		this.btnVoteWcMinus.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnVoteWcBtnMinusClicked);
		base.AddGuiElement(this.btnVoteWcMinus);
		this.forDelete.Add(this.btnVoteWcMinus);
		this.btnVoteWcPlus = new GuiButtonFixed();
		this.btnVoteWcPlus.SetTexture("PoiScreenWindow", "btnPlus");
		this.btnVoteWcPlus.Caption = string.Empty;
		this.btnVoteWcPlus.X = 440f;
		this.btnVoteWcPlus.Y = 425f;
		this.btnVoteWcPlus.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnVoteWcBtnPlusClicked);
		base.AddGuiElement(this.btnVoteWcPlus);
		this.forDelete.Add(this.btnVoteWcPlus);
		this.tb_donation = new GuiTextBox()
		{
			boundries = new Rect(277f, 425f, 155f, 33f)
		};
		this.tb_donation.SetFrameTexture("WarScreenWindow", "input");
		this.tb_donation.Alignment = 3;
		this.tb_donation.borderLeft = 33f;
		this.tb_donation.borderRight = 10f;
		this.tb_donation.borderTop = 0f;
		this.tb_donation.borderBottom = 4f;
		this.tb_donation.FontSize = 16;
		this.tb_donation.TextColor = GuiNewStyleBar.blueColor;
		this.tb_donation.controlName = "tb_donation";
		this.tb_donation.Validate = new Action(this, WarScreenWindow.ValidateWcDonation);
		this.tb_donation.Confirm = new Action<object>(this, WarScreenWindow.OnVoteForPlayerClicked);
		base.AddGuiElement(this.tb_donation);
		this.forDelete.Add(this.tb_donation);
		this.ValidateWcDonation();
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("WarScreenWindow", "warCommendation");
		rect.boundries = new Rect(this.tb_donation.X + 6f, this.tb_donation.Y + 4f, 25f, 20f);
		base.AddGuiElement(rect);
		this.forDelete.Add(rect);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.SetTexture("PoiScreenWindow", "tab_");
		guiButtonResizeable.X = 238f;
		guiButtonResizeable.Y = 467f;
		guiButtonResizeable.boundries.set_width(130f);
		guiButtonResizeable.Caption = StaticData.Translate("key_faction_war_btn_vote");
		guiButtonResizeable.FontSize = 12;
		guiButtonResizeable.Alignment = 4;
		guiButtonResizeable.SetRegularFont();
		guiButtonResizeable.SetColor(GuiNewStyleBar.blueColor);
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnVoteForPlayerClicked);
		base.AddGuiElement(guiButtonResizeable);
		this.forDelete.Add(guiButtonResizeable);
	}

	private void DrawYourContribution()
	{
		// 
		// Current member / type: System.Void WarScreenWindow::DrawYourContribution()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void DrawYourContribution()
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

	private byte GetBoostVotes(int boostId)
	{
		if (boostId == 101)
		{
			return this.voteForBattleOne;
		}
		if (boostId == 102)
		{
			return this.voteForBattleTwo;
		}
		if (boostId == 103)
		{
			return this.voteForBattleThree;
		}
		if (boostId == 104)
		{
			return this.voteForBattleVeto;
		}
		if (boostId == 201)
		{
			return this.voteForUtilityOne;
		}
		if (boostId == 202)
		{
			return this.voteForUtilityTwo;
		}
		if (boostId == 203)
		{
			return this.voteForUtilityThree;
		}
		if (boostId != 204)
		{
			return (byte)0;
		}
		return this.voteForUtilityVeto;
	}

	private void GetFocus()
	{
		AndromedaGui.gui.RequestFocusOnControl("tbMessage");
		this.preDrawHandler = new Action<object>(this, WarScreenWindow.SetCursorToEnd);
	}

	private void OnBuyWarCommendationClicked(object prm)
	{
		long num = 0L;
		int num1 = 0;
		int num2 = 0;
		byte num3 = NetworkScript.player.playerBelongings.warCommendationsBought;
		FactionWarStats.WarCommendationPrice(NetworkScript.player.playerBelongings.playerLevel, num3, ref num, ref num1, ref num2);
		string str = StaticData.Translate("key_war_commendation_pop_up_title");
		string str1 = string.Format(StaticData.Translate("key_war_commendation_pop_up_info"), NetworkScript.player.playerBelongings.warCommendationsBought);
		string str2 = StaticData.Translate("key_dock_my_ships_select_ship_yes");
		string str3 = StaticData.Translate("key_dock_my_ships_select_ship_no");
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = num3
		};
		NewPopUpWindow.CreateUniversalPurchasePopUpWindow(str, str1, num, num1, num2, 0, str2, str3, out Inventory.dialogWindow, eventHandlerParam, new Action<EventHandlerParam>(this, WarScreenWindow.ConfirmBuyWarCommendation));
	}

	private void OnCollectLastWeekRewardClicked(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void WarScreenWindow::OnCollectLastWeekRewardClicked(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnCollectLastWeekRewardClicked(EventHandlerParam)
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

	private void OnCollectWeeklyRewardClicked(EventHandlerParam prm)
	{
		if (NetworkScript.player.playerBelongings.factionWarDay != FactionWarStats.resetWeeklyChalangeProgress)
		{
			return;
		}
		this.isWaitingRefresh = true;
		playWebGame.udp.ExecuteCommand(100, new UniversalTransportContainer(), 105);
		this.PopulateScreen();
	}

	private void OnCopyNicknameClicked(EventHandlerParam prm)
	{
		if (prm == null || prm.customData == null || string.IsNullOrEmpty((string)prm.customData) || this.tb_vote_for_player_nickname == null)
		{
			return;
		}
		this.tb_vote_for_player_nickname.text = (string)prm.customData;
	}

	private void OnCouncilShopBtnClicked(EventHandlerParam prm)
	{
		this.ClearScreen();
		if (prm == null || prm.customData == null || prm.customData as CouncilShopSubsection == CouncilShopSubsection.FactionBoosts)
		{
			return;
		}
		this.selectedCouncilSubsection = (int)prm.customData;
		CouncilShopSubsection councilShopSubsection = this.selectedCouncilSubsection;
		if (councilShopSubsection == CouncilShopSubsection.FactionBoosts)
		{
			this.CreateFactionBoost();
		}
		else if (councilShopSubsection == CouncilShopSubsection.CouncilSkills)
		{
			this.CreateCouncilSkills();
		}
	}

	private void OnCouncilSkillSelectClicked(EventHandlerParam prm)
	{
		if (prm == null || prm.customData == null)
		{
			return;
		}
		ushort num = (ushort)prm.customData;
		if (NetworkScript.player.playerBelongings.councilSkillSelected != num)
		{
			if (this.btnApplyChanges != null)
			{
				this.btnApplyChanges.eventHandlerParam.customData = num;
				this.btnApplyChanges.isEnabled = (this.selectCouncilSkillComandSend || NetworkScript.player.playerBelongings.playerItems.get_Equilibrium() < (long)FactionWarStats.CouncilSkillPriceEquilibrium(NetworkScript.player.playerBelongings.playerLevel) ? false : NetworkScript.player.playerBelongings.playerItems.get_Cash() >= (long)FactionWarStats.CouncilSkillPriceCash(NetworkScript.player.playerBelongings.playerLevel));
			}
		}
		else if (this.btnApplyChanges != null)
		{
			this.btnApplyChanges.eventHandlerParam.customData = 0;
			this.btnApplyChanges.isEnabled = false;
		}
	}

	private void OnDonateForFaction(object prm)
	{
		// 
		// Current member / type: System.Void WarScreenWindow::OnDonateForFaction(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnDonateForFaction(System.Object)
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

	private void OnEditMessageClicked(EventHandlerParam prm)
	{
		if (prm == null || prm.customData == null)
		{
			return;
		}
		GuiLabel empty = (GuiLabel)prm.customData;
		this.tbMessage = new GuiTextBox()
		{
			controlName = "tbMessage",
			boundries = empty.boundries
		};
		this.tbMessage.SetSingleTexture("FrameworkGUI", "empty");
		this.tbMessage.Alignment = 0;
		this.tbMessage.borderTop = 0f;
		this.tbMessage.borderLeft = 0f;
		this.tbMessage.borderRight = 0f;
		this.tbMessage.borderBottom = 0f;
		this.tbMessage.FontSize = 12;
		this.tbMessage.text = empty.text;
		this.tbMessage.isMultiLine = true;
		this.tbMessage.Validate = new Action(this, WarScreenWindow.ValidateFactionMessage);
		base.AddGuiElement(this.tbMessage);
		this.forDelete.Add(this.tbMessage);
		empty.text = string.Empty;
		if (this.btnEditSaveMessage != null)
		{
			this.btnEditSaveMessage.SetTextureNormal("PoiScreenWindow", "btnSave_leftNormal");
			this.btnEditSaveMessage.SetTextureHover("PoiScreenWindow", "btnSave_leftHover");
			this.btnEditSaveMessage.SetTextureDisabled("PoiScreenWindow", "btnSave_leftDisable");
			this.btnEditSaveMessage.SetTextureClicked("PoiScreenWindow", "btnSave_leftHover");
			this.btnEditSaveMessage.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnSaveMessageClicked);
		}
		this.GetFocus();
	}

	private void OnFactionMessageBtnClicked(EventHandlerParam prm)
	{
		AndromedaGui.gui.ClearFocusOnControl();
		if (this.btnEditSaveMessage != null)
		{
			this.btnEditSaveMessage.SetTextureNormal("PoiScreenWindow", "btnEdit_leftNormal");
			this.btnEditSaveMessage.SetTextureHover("PoiScreenWindow", "btnEdit_leftHover");
			this.btnEditSaveMessage.SetTextureDisabled("PoiScreenWindow", "btnEdit_leftDisable");
			this.btnEditSaveMessage.SetTextureClicked("PoiScreenWindow", "btnEdit_leftHover");
			this.btnEditSaveMessage.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnEditMessageClicked);
		}
		if (prm == null || prm.customData == null || prm.customData2 == null)
		{
			return;
		}
		if (this.tb_donation != null)
		{
			base.RemoveGuiElement(this.tbMessage);
			this.tbMessage = null;
		}
		GuiLabel guiLabel = (GuiLabel)prm.customData;
		this.messageToYourFaction = (bool)prm.customData2;
		if (!this.messageToYourFaction)
		{
			guiLabel.text = this.yourFactionToEnemy;
		}
		else
		{
			guiLabel.text = this.yourFactionToYou;
		}
	}

	private void OnGalaxyBtnClicked(EventHandlerParam prm)
	{
		this.selectedGalaxyId = (int)prm.customData;
		this.PopulateGalaxyVoteElements(0);
	}

	private void OnGalaxyBtnHover(object parm, bool state)
	{
		if (!state)
		{
			this.PopulateGalaxyVoteElements(0);
		}
		else
		{
			this.PopulateGalaxyVoteElements((int)parm);
		}
	}

	private void OnGetDayRewardClicked(EventHandlerParam obj)
	{
		// 
		// Current member / type: System.Void WarScreenWindow::OnGetDayRewardClicked(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnGetDayRewardClicked(EventHandlerParam)
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

	private void OnPaidAdClicked(object prm)
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
			text = StaticData.Translate("key_faction_war_paid_ad_popup_title"),
			Alignment = 4
		};
		this.dialogWindow.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(73f, 107f, 480f, 66f),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			text = string.Format(StaticData.Translate("key_faction_war_paid_ad_popup_info"), 10, 10),
			Alignment = 4
		};
		this.dialogWindow.AddGuiElement(guiLabel1);
		this.tb_paid_ad_popup_slogan = new GuiTextBox()
		{
			boundries = new Rect(73f, 180f, 480f, 33f)
		};
		this.tb_paid_ad_popup_slogan.SetFrameTexture("WarScreenWindow", "input");
		this.tb_paid_ad_popup_slogan.Alignment = 3;
		this.tb_paid_ad_popup_slogan.borderLeft = 10f;
		this.tb_paid_ad_popup_slogan.borderRight = 10f;
		this.tb_paid_ad_popup_slogan.borderTop = 0f;
		this.tb_paid_ad_popup_slogan.borderBottom = 4f;
		this.tb_paid_ad_popup_slogan.FontSize = 16;
		this.tb_paid_ad_popup_slogan.TextColor = GuiNewStyleBar.blueColor;
		this.tb_paid_ad_popup_slogan.Validate = new Action(this, WarScreenWindow.ValidateSlogan);
		this.dialogWindow.AddGuiElement(this.tb_paid_ad_popup_slogan);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.SetNovaBtn();
		guiButtonResizeable.X = 77f;
		guiButtonResizeable.Y = 220f;
		guiButtonResizeable.boundries.set_width(175f);
		guiButtonResizeable.Caption = string.Concat(StaticData.Translate("key_faction_war_paid_ad_popup_btn_pay"), " ", this.paidAdPrice.ToString("N0"));
		guiButtonResizeable.FontSize = 14;
		guiButtonResizeable.Alignment = 4;
		guiButtonResizeable.isEnabled = (this.isWaitingRefresh ? false : NetworkScript.player.playerBelongings.playerItems.get_Nova() >= (long)this.paidAdPrice);
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnPayForAdClicked);
		this.dialogWindow.AddGuiElement(guiButtonResizeable);
		GuiButtonResizeable action = new GuiButtonResizeable();
		action.SetDiscardBtn();
		action.X = 374f;
		action.Y = 220f;
		action.boundries.set_width(175f);
		action.Caption = StaticData.Translate("key_faction_war_paid_ad_popup_btn_cancel");
		action.FontSize = 14;
		action.Alignment = 4;
		action.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnPaidAdDialogueCancel);
		this.dialogWindow.AddGuiElement(action);
	}

	private void OnPaidAdDialogueCancel(object prm)
	{
		AndromedaGui.gui.RemoveWindow(this.dialogWindow.handler);
		AndromedaGui.gui.activeToolTipId = -1;
		this.dialogWindow = null;
	}

	private void OnPayForAdClicked(object prm)
	{
		// 
		// Current member / type: System.Void WarScreenWindow::OnPayForAdClicked(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnPayForAdClicked(System.Object)
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

	private static void OnProfilBtnClick(EventHandlerParam prm)
	{
		NetworkScript.RequestUserProfile((string)prm.customData);
	}

	private void OnSaveMessageClicked(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void WarScreenWindow::OnSaveMessageClicked(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnSaveMessageClicked(EventHandlerParam)
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
		this.infoBtn.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnWarInfoClicked);
		this.infoBtn.SetTexture("PoiScreenWindow", "btn-info");
		if (prm == null || prm.customData == null)
		{
			return;
		}
		if (this.isInfoOnScren)
		{
			this.isInfoOnScren = false;
			this.ClearContent();
			this.CreateLeftSide();
		}
		this.ClearScreen();
		this.ClearRearElements();
		this.selectedTab = (int)prm.customData;
		if (prm.customData2 == null)
		{
			this.selectedSubsection = WarScreenSubsection.Overview;
		}
		this.selectedGalaxyId = 0;
		switch (this.selectedTab)
		{
			case WarScreenTab.WarInProgress:
			{
				this.CreateWarInProgress();
				break;
			}
			case WarScreenTab.WeeklyChallenge:
			{
				this.CreateWeeklyChallenge();
				break;
			}
			case WarScreenTab.Council:
			{
				this.DrawFactionCouncils();
				break;
			}
			case WarScreenTab.Shop:
			{
				this.CreateCouncilShop();
				break;
			}
		}
	}

	private void OnSelectCouncilSkillClicked(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void WarScreenWindow::OnSelectCouncilSkillClicked(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnSelectCouncilSkillClicked(EventHandlerParam)
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

	private void OnSubSelectClicked(EventHandlerParam prm)
	{
		this.ClearScreen();
		this.ClearRearElements();
		if (prm == null || prm.customData == null)
		{
			this.requestFactionBank = true;
			this.selectedSubsection = WarScreenSubsection.Overview;
			this.DrawOverview(NetworkScript.player.playerBelongings.factionWarDay);
			this.DrawYourContribution();
			return;
		}
		switch ((int)prm.customData)
		{
			case 0:
			{
				this.selectedSubsection = WarScreenSubsection.Day7;
				this.requestVoteForPlayerInfo = true;
				this.requestVoteForGalaxyInfo = true;
				break;
			}
			case 1:
			{
				this.selectedSubsection = WarScreenSubsection.Day1;
				this.requestVoteForGalaxyInfo = true;
				break;
			}
			case 2:
			{
				this.selectedSubsection = WarScreenSubsection.Day2;
				this.requestVoteForPlayerInfo = true;
				break;
			}
			case 3:
			{
				this.selectedSubsection = WarScreenSubsection.Day3;
				this.requestVoteForPlayerInfo = true;
				this.requestVoteForGalaxyInfo = true;
				break;
			}
			case 4:
			{
				this.selectedSubsection = WarScreenSubsection.Day4;
				this.requestVoteForPlayerInfo = true;
				this.requestVoteForGalaxyInfo = true;
				break;
			}
			case 5:
			{
				this.selectedSubsection = WarScreenSubsection.Day5;
				this.requestVoteForPlayerInfo = true;
				this.requestVoteForGalaxyInfo = true;
				break;
			}
			case 6:
			{
				this.selectedSubsection = WarScreenSubsection.Day6;
				this.requestVoteForPlayerInfo = true;
				this.requestVoteForGalaxyInfo = true;
				break;
			}
		}
		this.selectedSubsection = (int)prm.customData;
		this.CreateWeeklyChallenge();
	}

	private void OnUniverseMapBtnClicked(object prm)
	{
		MainScreenWindow mainScreenWindow = AndromedaGui.mainWnd;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = (byte)7
		};
		mainScreenWindow.OnWindowBtnClicked(eventHandlerParam);
	}

	private void OnVoteForBattleBoost(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void WarScreenWindow::OnVoteForBattleBoost(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnVoteForBattleBoost(EventHandlerParam)
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

	private void OnVoteForGalaxyClicked(object prm)
	{
		// 
		// Current member / type: System.Void WarScreenWindow::OnVoteForGalaxyClicked(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnVoteForGalaxyClicked(System.Object)
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

	private void OnVoteForPlayerClicked(object prm)
	{
		// 
		// Current member / type: System.Void WarScreenWindow::OnVoteForPlayerClicked(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnVoteForPlayerClicked(System.Object)
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

	private void OnVoteForUtilityBoost(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void WarScreenWindow::OnVoteForUtilityBoost(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnVoteForUtilityBoost(EventHandlerParam)
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

	private void OnVoteWcBtnMinusClicked(object prm)
	{
		if (this.tb_donation == null)
		{
			return;
		}
		int num = 0;
		int.TryParse(this.tb_donation.text, ref num);
		num--;
		this.tb_donation.text = num.ToString();
		this.VoteForPlayerWcUpdate();
	}

	private void OnVoteWcBtnPlusClicked(object prm)
	{
		if (this.tb_donation == null)
		{
			return;
		}
		int num = 0;
		int.TryParse(this.tb_donation.text, ref num);
		num++;
		this.tb_donation.text = num.ToString();
		this.VoteForPlayerWcUpdate();
	}

	private void OnWarInfoClicked(object prm)
	{
		this.isInfoOnScren = true;
		this.ClearContent();
		this.infoBtn.SetTexture("PoiScreenWindow", "btnInfoBack");
		this.infoBtn.eventHandlerParam.customData = this.selectedTab;
		this.infoBtn.Clicked = new Action<EventHandlerParam>(this, WarScreenWindow.OnSelectCategoryClicked);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(147f, 8f, 618f, 36f),
			Alignment = 4,
			text = StaticData.Translate("key_new_universe_map_lbl_information"),
			FontSize = 20,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("WarScreenWindow", "factionWarInfo");
		guiTexture.X = 15f;
		guiTexture.Y = 53f;
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(35f, 70f, 845f, 80f),
			text = string.Format(StaticData.Translate("key_war_info_screen_description"), 10),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel1);
		this.forDelete.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(146f, 185f, 745f, 18f),
			text = StaticData.Translate("key_war_info_screen_paragraph_one_title"),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel2);
		this.forDelete.Add(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(146f, 207f, 745f, 70f),
			text = StaticData.Translate("key_war_info_screen_paragraph_one_info"),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel3);
		this.forDelete.Add(guiLabel3);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			boundries = new Rect(146f, 296f, 745f, 18f),
			text = StaticData.Translate("key_war_info_screen_paragraph_two_title"),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel4);
		this.forDelete.Add(guiLabel4);
		GuiLabel guiLabel5 = new GuiLabel()
		{
			boundries = new Rect(146f, 318f, 745f, 70f),
			text = StaticData.Translate("key_war_info_screen_paragraph_two_info"),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel5);
		this.forDelete.Add(guiLabel5);
		GuiLabel guiLabel6 = new GuiLabel()
		{
			boundries = new Rect(146f, 408f, 745f, 18f),
			text = StaticData.Translate("key_war_info_screen_paragraph_three_title"),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel6);
		this.forDelete.Add(guiLabel6);
		GuiLabel guiLabel7 = new GuiLabel()
		{
			boundries = new Rect(146f, 430f, 745f, 70f),
			text = string.Format(StaticData.Translate("key_war_info_screen_paragraph_three_info"), new object[] { 20, 25, 10, 100 }),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel7);
		this.forDelete.Add(guiLabel7);
	}

	public void PopulateBestCandidatesAndBank(List<KeyValuePair<string, long>> listOfPlayers, long bank)
	{
		if (this.isInfoOnScren)
		{
			return;
		}
		this.isWaitingRefresh = false;
		this.requestVoteForPlayerInfo = false;
		this.topCouncilCandidats = listOfPlayers;
		this.factionBank = bank;
		if (this.selectedSubsection == WarScreenSubsection.Day1)
		{
			EventHandlerParam eventHandlerParam = new EventHandlerParam()
			{
				customData = this.selectedSubsection,
				customData2 = false
			};
			this.OnSubSelectClicked(eventHandlerParam);
		}
	}

	public void PopulateFactionBank()
	{
		if (this.isInfoOnScren)
		{
			return;
		}
		this.requestFactionBank = false;
		this.UpdateFactionWarScore();
		if (this.selectedTab == WarScreenTab.WeeklyChallenge && this.selectedSubsection == WarScreenSubsection.Overview)
		{
			EventHandlerParam eventHandlerParam = new EventHandlerParam()
			{
				customData = this.selectedSubsection
			};
			this.OnSubSelectClicked(eventHandlerParam);
		}
		if (this.factionBankLbl != null)
		{
			this.factionBankLbl.text = string.Concat(StaticData.Translate("key_faction_war_bank_lbl"), " $", this.factionBank.ToString("N0"));
		}
	}

	public void PopulateFactionBoostVote()
	{
		if (this.isInfoOnScren)
		{
			return;
		}
		if (this.selectedTab != WarScreenTab.Shop || this.selectedCouncilSubsection != CouncilShopSubsection.FactionBoosts)
		{
			return;
		}
		foreach (WarScreenWindow.FactionBoostVoteElements factionBoostVoteElement in this.forBattle)
		{
			if (factionBoostVoteElement.boostId != NetworkScript.player.playerBelongings.myBattleBoostVote)
			{
				factionBoostVoteElement.voteState.SetTexture("WarScreenWindow", "voteEmpty");
			}
			else
			{
				factionBoostVoteElement.voteState.SetTexture("WarScreenWindow", "voteChecked");
			}
			factionBoostVoteElement.voteCountLbl.text = string.Format(StaticData.Translate("key_faction_war_faction_boosts_votes_lbl"), this.GetBoostVotes((int)factionBoostVoteElement.boostId));
			if (NetworkScript.player.playerBelongings.myBattleBoostVote != 0 && NetworkScript.player.playerBelongings.myBattleBoostVote != 104)
			{
				continue;
			}
			factionBoostVoteElement.voteBtn.IsClicked = false;
		}
		foreach (WarScreenWindow.FactionBoostVoteElements factionBoostVoteElement1 in this.forUtility)
		{
			if (factionBoostVoteElement1.boostId != NetworkScript.player.playerBelongings.myUtilityBoostVote)
			{
				factionBoostVoteElement1.voteState.SetTexture("WarScreenWindow", "voteEmpty");
			}
			else
			{
				factionBoostVoteElement1.voteState.SetTexture("WarScreenWindow", "voteChecked");
			}
			factionBoostVoteElement1.voteCountLbl.text = string.Format(StaticData.Translate("key_faction_war_faction_boosts_votes_lbl"), this.GetBoostVotes((int)factionBoostVoteElement1.boostId));
			if (NetworkScript.player.playerBelongings.myUtilityBoostVote != 0 && NetworkScript.player.playerBelongings.myBattleBoostVote != 204)
			{
				continue;
			}
			factionBoostVoteElement1.voteBtn.IsClicked = false;
		}
		this.battleBoostClearVoteBtn.isEnabled = NetworkScript.player.playerBelongings.myBattleBoostVote != 0;
		this.utilityBoostClearVoteBtn.isEnabled = NetworkScript.player.playerBelongings.myUtilityBoostVote != 0;
		this.battleBoostVetoBtn.isEnabled = NetworkScript.player.playerBelongings.myBattleBoostVote != 104;
		this.utilityBoostVetoBtn.isEnabled = NetworkScript.player.playerBelongings.myUtilityBoostVote != 204;
		this.battleBoostVetoLbl.text = string.Format("{0}: {1}", StaticData.Translate("key_faction_war_boost_veto_lbl"), this.GetBoostVotes(104));
		this.utilityBoostVetoLbl.text = string.Format("{0}: {1}", StaticData.Translate("key_faction_war_boost_veto_lbl"), this.GetBoostVotes(204));
	}

	public void PopulateFactionMessages()
	{
		if (this.isInfoOnScren)
		{
			return;
		}
		this.isWaitingRefresh = false;
		this.requestFactionMessages = false;
		if (this.selectedTab == WarScreenTab.Council)
		{
			EventHandlerParam eventHandlerParam = new EventHandlerParam()
			{
				customData = this.selectedTab
			};
			this.OnSelectCategoryClicked(eventHandlerParam);
		}
	}

	private void PopulateGalaxyVoteElements(int hoveredGalaxyId = 0)
	{
		foreach (WarScreenWindow.GalaxyVoteElements _white in this.galaxyVoteElementsForUpdate)
		{
			if (hoveredGalaxyId != 0 && _white.galaxyId == hoveredGalaxyId && hoveredGalaxyId != this.selectedGalaxyId)
			{
				_white.titleFrame.SetTexture("WarScreenWindow", "galaxyTitleHoverFrame");
				_white.bonusOneFrame.SetTexture("WarScreenWindow", "galaxyBonusHoverFrame");
				_white.titleLbl.TextColor = Color.get_white();
				_white.bonusOneLbl.TextColor = Color.get_white();
				if (_white.bonusTwoLbl != null)
				{
					_white.bonusTwoLbl.TextColor = Color.get_white();
				}
				if (_white.bonusTwoFrame != null)
				{
					_white.bonusTwoFrame.SetTexture("WarScreenWindow", "galaxyBonusHoverFrame");
				}
			}
			else if (_white.galaxyId != this.selectedGalaxyId)
			{
				_white.titleFrame.SetTexture("WarScreenWindow", "galaxyTitleNormalFrame");
				_white.bonusOneFrame.SetTexture("WarScreenWindow", "galaxyBonusNormalFrame");
				_white.titleLbl.TextColor = GuiNewStyleBar.blueColor;
				_white.bonusOneLbl.TextColor = GuiNewStyleBar.blueColor;
				if (_white.bonusTwoLbl != null)
				{
					_white.bonusTwoLbl.TextColor = GuiNewStyleBar.blueColor;
				}
				if (_white.bonusTwoFrame == null)
				{
					continue;
				}
				_white.bonusTwoFrame.SetTexture("WarScreenWindow", "galaxyBonusNormalFrame");
			}
			else
			{
				_white.titleFrame.SetTexture("WarScreenWindow", "galaxyTitleActiveFrame");
				_white.bonusOneFrame.SetTexture("WarScreenWindow", "galaxyBonusActiveFrame");
				_white.titleLbl.TextColor = GuiNewStyleBar.aquamarineColor;
				_white.bonusOneLbl.TextColor = GuiNewStyleBar.aquamarineColor;
				if (_white.bonusTwoLbl != null)
				{
					_white.bonusTwoLbl.TextColor = GuiNewStyleBar.aquamarineColor;
				}
				if (_white.bonusTwoFrame != null)
				{
					_white.bonusTwoFrame.SetTexture("WarScreenWindow", "galaxyBonusActiveFrame");
				}
			}
		}
	}

	public void PopulateOnDayChange()
	{
		if (this.isInfoOnScren)
		{
			return;
		}
		this.requestVoteForPlayerInfo = true;
		this.requestVoteForGalaxyInfo = true;
		this.requestFactionBank = true;
		this.requestFactionMessages = true;
		this.topCouncilCandidats.Clear();
		this.galaxyVote.Clear();
		if (this.dialogWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.dialogWindow.handler);
			AndromedaGui.gui.activeToolTipId = -1;
			this.dialogWindow = null;
		}
		if (NetworkScript.player.playerBelongings.factionWarDay == FactionWarStats.voteForPlayerDay && (this.selectedTab == WarScreenTab.Council || this.selectedTab == WarScreenTab.Shop))
		{
			this.selectedTab = WarScreenTab.WeeklyChallenge;
			this.btnWeeklyChallenge.IsClicked = true;
		}
		this.btnCouncil.isEnabled = (NetworkScript.player.playerBelongings.factionWarDay == FactionWarStats.voteForPlayerDay ? false : NetworkScript.player.isWarInProgress);
		this.btnShop.isEnabled = (NetworkScript.player.playerBelongings.councilRank == 0 ? false : NetworkScript.player.isWarInProgress);
		this.UpdateFactionActiveBoosts();
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = this.selectedTab
		};
		this.OnSelectCategoryClicked(eventHandlerParam);
	}

	public void PopulateScreen()
	{
		if (this.isInfoOnScren)
		{
			return;
		}
		this.isWaitingRefresh = false;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = this.selectedTab,
			customData2 = true
		};
		this.OnSelectCategoryClicked(eventHandlerParam);
	}

	public void PopulateVoteForGalaxyScreen()
	{
		if (this.isInfoOnScren)
		{
			return;
		}
		this.isWaitingRefresh = false;
		this.requestVoteForGalaxyInfo = false;
		if (this.selectedSubsection == WarScreenSubsection.Day2)
		{
			EventHandlerParam eventHandlerParam = new EventHandlerParam()
			{
				customData = this.selectedSubsection,
				customData2 = false
			};
			this.OnSubSelectClicked(eventHandlerParam);
		}
	}

	public void PopulateVoteForPlayerScreen()
	{
		if (this.isInfoOnScren)
		{
			return;
		}
		this.isWaitingRefresh = false;
		this.requestVoteForPlayerInfo = false;
		if (this.selectedSubsection == WarScreenSubsection.Day1)
		{
			EventHandlerParam eventHandlerParam = new EventHandlerParam()
			{
				customData = this.selectedSubsection,
				customData2 = false
			};
			this.OnSubSelectClicked(eventHandlerParam);
		}
	}

	public void PopulateWarCommendation()
	{
		if (this.warCommendationLbl != null)
		{
			this.warCommendationLbl.text = string.Format("{0:N0}/{1:N0}", NetworkScript.player.playerBelongings.playerItems.get_WarCommendation(), 100);
		}
		if (this.warCommendationVoteLbl != null)
		{
			this.warCommendationVoteLbl.text = string.Format("{0:N0}/{1:N0}", NetworkScript.player.playerBelongings.playerItems.get_WarCommendation(), 100);
		}
		if (this.btnGetMoreWc != null && NetworkScript.player.playerBelongings.playerItems.get_WarCommendation() >= (long)100)
		{
			this.btnGetMoreWc.isEnabled = false;
		}
		if (this.selectedTab == WarScreenTab.WeeklyChallenge && (this.selectedSubsection == WarScreenSubsection.Day1 || this.selectedSubsection == WarScreenSubsection.Day2))
		{
			this.ValidateWcDonation();
		}
	}

	private void SetCursorToEnd(object prm)
	{
		if (this.tbMessage == null)
		{
			return;
		}
		AndromedaGui.gui.RequestFocusOnControl("tbMessage");
		TextEditor stateObject = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.get_keyboardControl());
		if (stateObject != null)
		{
			WarScreenWindow warScreenWindow = this;
			warScreenWindow.tmp = warScreenWindow.tmp + 1;
			stateObject.pos = 0;
			stateObject.selectPos = this.tbMessage.text.get_Length() + 1;
		}
		if (this.tmp == 3)
		{
			this.preDrawHandler = null;
			this.tmp = 0;
		}
	}

	private void UpdateFactionActiveBoosts()
	{
		EventHandlerParam eventHandlerParam;
		foreach (GuiElement guiElement in this.forActiveBoostsDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.forActiveBoostsDelete.Clear();
		float single = 24f;
		if (NetworkScript.player.playerBelongings.factionWarBattleBoost != 0 && FactionBoostInfo.battleBoosts.ContainsKey(NetworkScript.player.playerBelongings.factionWarBattleBoost))
		{
			FactionBoostInfo item = FactionBoostInfo.battleBoosts.get_Item(NetworkScript.player.playerBelongings.factionWarBattleBoost);
			GuiTexture guiTexture = new GuiTexture();
			guiTexture.SetTexture("WarScreenWindow", "activeBoosterIcon");
			guiTexture.X = single;
			guiTexture.Y = 407f;
			base.AddGuiElement(guiTexture);
			this.forActiveBoostsDelete.Add(guiTexture);
			GuiTexture drawTooltipWindow = new GuiTexture();
			drawTooltipWindow.SetTexture("WarScreenWindow", item.iconAssetName);
			drawTooltipWindow.boundries = guiTexture.boundries;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = item.get_boostInfoText(),
				customData2 = drawTooltipWindow
			};
			drawTooltipWindow.tooltipWindowParam = eventHandlerParam;
			drawTooltipWindow.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			base.AddGuiElement(drawTooltipWindow);
			this.forActiveBoostsDelete.Add(drawTooltipWindow);
			single = single + 30f;
		}
		if (NetworkScript.player.playerBelongings.factionWarUtilityBoost != 0 && FactionBoostInfo.utilityBoosts.ContainsKey(NetworkScript.player.playerBelongings.factionWarUtilityBoost))
		{
			FactionBoostInfo factionBoostInfo = FactionBoostInfo.utilityBoosts.get_Item(NetworkScript.player.playerBelongings.factionWarUtilityBoost);
			GuiTexture guiTexture1 = new GuiTexture();
			guiTexture1.SetTexture("WarScreenWindow", "activeBoosterIcon");
			guiTexture1.X = single;
			guiTexture1.Y = 407f;
			base.AddGuiElement(guiTexture1);
			this.forActiveBoostsDelete.Add(guiTexture1);
			GuiTexture drawTooltipWindow1 = new GuiTexture();
			drawTooltipWindow1.SetTexture("WarScreenWindow", factionBoostInfo.iconAssetName);
			drawTooltipWindow1.boundries = guiTexture1.boundries;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = factionBoostInfo.get_boostInfoText(),
				customData2 = drawTooltipWindow1
			};
			drawTooltipWindow1.tooltipWindowParam = eventHandlerParam;
			drawTooltipWindow1.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			base.AddGuiElement(drawTooltipWindow1);
			this.forActiveBoostsDelete.Add(drawTooltipWindow1);
		}
	}

	private void UpdateFactionWarScore()
	{
		if (this.faction1scoreLbl != null)
		{
			this.faction1scoreLbl.text = WarScreenWindow.factionOneScore.ToString("N0");
		}
		if (this.faction2scoreLbl != null)
		{
			this.faction2scoreLbl.text = WarScreenWindow.factionTwoScore.ToString("N0");
		}
		if (this.loosingFactionOneBonus != null)
		{
			this.loosingFactionOneBonus.text = string.Empty;
		}
		if (this.loosingFactionTwoBonus != null)
		{
			this.loosingFactionTwoBonus.text = string.Empty;
		}
		int num = 0;
		if (WarScreenWindow.weeklyLoosingFaction == 2)
		{
			num = num + WarScreenWindow.weeklyLoosingFactionBonusPercent;
		}
		if (WarScreenWindow.dailyLoosingFaction == 2)
		{
			num = num + WarScreenWindow.dailyLoosingFactionBonusPercent;
		}
		int num1 = 0;
		if (WarScreenWindow.weeklyLoosingFaction == 1)
		{
			num1 = num1 + WarScreenWindow.weeklyLoosingFactionBonusPercent;
		}
		if (WarScreenWindow.dailyLoosingFaction == 1)
		{
			num1 = num1 + WarScreenWindow.dailyLoosingFactionBonusPercent;
		}
		if (num1 != 0 && this.loosingFactionOneBonus != null)
		{
			this.loosingFactionOneBonus.text = string.Format(StaticData.Translate("key_faction_war_loosing_faction_bonus"), num1);
		}
		if (num != 0 && this.loosingFactionTwoBonus != null)
		{
			this.loosingFactionTwoBonus.text = string.Format(StaticData.Translate("key_faction_war_loosing_faction_bonus"), num);
		}
	}

	private void ValidateDonation()
	{
		if (this.tb_donation == null)
		{
			return;
		}
		long num = (long)0;
		long.TryParse(this.tb_donation.text, ref num);
		this.tb_donation.text = num.ToString();
	}

	private void ValidateFactionMessage()
	{
		if (this.tbMessage == null)
		{
			return;
		}
		if (this.tbMessage.text.get_Length() > 256)
		{
			this.tbMessage.text = this.tbMessage.text.Substring(0, 256);
		}
	}

	private void ValidateSlogan()
	{
		if (this.tb_paid_ad_popup_slogan == null)
		{
			return;
		}
		if (this.tb_paid_ad_popup_slogan.text.get_Length() > 40)
		{
			this.tb_paid_ad_popup_slogan.text = this.tb_paid_ad_popup_slogan.text.Substring(0, 40);
		}
	}

	private void ValidateWcDonation()
	{
		if (this.tb_donation == null)
		{
			return;
		}
		long num = (long)((NetworkScript.player.playerBelongings.playerItems.get_WarCommendation() <= (long)0 ? 0 : 1));
		if (!long.TryParse(this.tb_donation.text, ref num))
		{
			num = (long)((NetworkScript.player.playerBelongings.playerItems.get_WarCommendation() <= (long)0 ? 0 : 1));
		}
		else
		{
			num = (num <= NetworkScript.player.playerBelongings.playerItems.get_WarCommendation() ? num : NetworkScript.player.playerBelongings.playerItems.get_WarCommendation());
		}
		if (num == 0 && NetworkScript.player.playerBelongings.playerItems.get_WarCommendation() > (long)0)
		{
			num = (long)((NetworkScript.player.playerBelongings.playerItems.get_WarCommendation() <= (long)0 ? 0 : 1));
		}
		this.tb_donation.text = num.ToString();
		this.VoteForPlayerWcUpdate();
	}

	private void VoteForPlayerWcUpdate()
	{
		if (this.tb_donation == null)
		{
			return;
		}
		if (this.btnGetMoreWc != null && NetworkScript.player.playerBelongings.playerItems.get_WarCommendation() >= (long)100)
		{
			this.btnGetMoreWc.isEnabled = false;
		}
		int num = (NetworkScript.player.playerBelongings.playerItems.get_WarCommendation() <= (long)0 ? 0 : 1);
		int.TryParse(this.tb_donation.text, ref num);
		if (this.btnVoteWcMinus != null)
		{
			if (num >= 2)
			{
				this.btnVoteWcMinus.isEnabled = true;
			}
			else
			{
				this.btnVoteWcMinus.isEnabled = false;
			}
		}
		if (this.btnVoteWcPlus != null)
		{
			if ((long)num >= NetworkScript.player.playerBelongings.playerItems.get_WarCommendation())
			{
				this.btnVoteWcPlus.isEnabled = false;
			}
			else
			{
				this.btnVoteWcPlus.isEnabled = true;
			}
		}
	}

	private class DayInfo
	{
		public DayOfWeek day;

		public string dayLblText;

		public string dayDescriptionText;

		public string dayShortInfoText;

		public string dayIconAssetName;

		public float posX;

		public float posY;

		public bool justSimple;

		public static WarScreenWindow.DayInfo[] allDays;

		static DayInfo()
		{
			WarScreenWindow.DayInfo[] dayInfoArray = new WarScreenWindow.DayInfo[7];
			WarScreenWindow.DayInfo dayInfo = new WarScreenWindow.DayInfo()
			{
				day = 1,
				dayIconAssetName = "icon-day-1",
				dayLblText = "key_weekly_challenge_day_monday_title",
				dayDescriptionText = "key_weekly_challenge_day_monday_description",
				dayShortInfoText = "key_weekly_challenge_day_monday_info",
				posX = 210f,
				posY = 55f
			};
			dayInfoArray[0] = dayInfo;
			dayInfo = new WarScreenWindow.DayInfo()
			{
				day = 2,
				dayIconAssetName = "icon-day-2",
				dayLblText = "key_weekly_challenge_day_tuesday_title",
				dayDescriptionText = "key_weekly_challenge_day_tuesday_description",
				dayShortInfoText = "key_weekly_challenge_day_tuesday_info",
				posX = 557f,
				posY = 55f
			};
			dayInfoArray[1] = dayInfo;
			dayInfo = new WarScreenWindow.DayInfo()
			{
				day = 3,
				dayIconAssetName = "icon-day-3",
				dayLblText = "key_weekly_challenge_day_wednesday_title",
				dayDescriptionText = "key_weekly_challenge_day_wednesday_description",
				dayShortInfoText = "key_weekly_challenge_day_wednesday_info",
				posX = 210f,
				posY = 151f
			};
			dayInfoArray[2] = dayInfo;
			dayInfo = new WarScreenWindow.DayInfo()
			{
				day = 4,
				dayIconAssetName = "icon-day-4",
				dayLblText = "key_weekly_challenge_day_thursday_title",
				dayDescriptionText = "key_weekly_challenge_day_thursday_description",
				dayShortInfoText = "key_weekly_challenge_day_thursday_info",
				posX = 557f,
				posY = 151f
			};
			dayInfoArray[3] = dayInfo;
			dayInfo = new WarScreenWindow.DayInfo()
			{
				day = 5,
				dayIconAssetName = "icon-day-5",
				dayLblText = "key_weekly_challenge_day_friday_title",
				dayDescriptionText = "key_weekly_challenge_day_friday_description",
				dayShortInfoText = "key_weekly_challenge_day_friday_info",
				posX = 210f,
				posY = 246f
			};
			dayInfoArray[4] = dayInfo;
			dayInfo = new WarScreenWindow.DayInfo()
			{
				day = 6,
				dayIconAssetName = "icon-day-6",
				dayLblText = "key_weekly_challenge_day_saturday_title",
				dayDescriptionText = "key_weekly_challenge_day_saturday_description",
				dayShortInfoText = "key_weekly_challenge_day_saturday_info",
				posX = 557f,
				posY = 246f
			};
			dayInfoArray[5] = dayInfo;
			dayInfo = new WarScreenWindow.DayInfo()
			{
				dayIconAssetName = "icon-day-7",
				dayLblText = "key_weekly_challenge_day_sunday_title",
				dayDescriptionText = "key_weekly_challenge_day_sunday_description",
				dayShortInfoText = "key_weekly_challenge_day_sunday_info",
				posX = 210f,
				posY = 344f,
				justSimple = true
			};
			dayInfoArray[6] = dayInfo;
			WarScreenWindow.DayInfo.allDays = dayInfoArray;
		}

		public DayInfo()
		{
		}

		public string StatusAssetName(PlayerBelongings pb, DayOfWeek today)
		{
			string empty = string.Empty;
			bool flag = false;
			switch (this.day)
			{
				case 1:
				{
					flag = pb.day1Participation;
					break;
				}
				case 2:
				{
					flag = pb.day2Participation;
					break;
				}
				case 3:
				{
					flag = pb.day3Participation;
					break;
				}
				case 4:
				{
					flag = pb.day4Participation;
					break;
				}
				case 5:
				{
					flag = pb.day5Participation;
					break;
				}
				case 6:
				{
					flag = pb.day6Participation;
					break;
				}
			}
			if (this.day == today)
			{
				empty = (!flag ? "statusToday" : "statusDone");
			}
			else if (today == null)
			{
				empty = (!flag ? "statusMissed" : "statusDone");
			}
			else if (this.day >= today)
			{
				empty = "statusUpcoming";
			}
			else
			{
				empty = (!flag ? "statusMissed" : "statusDone");
			}
			return empty;
		}
	}

	private class FactionBoostVoteElements
	{
		public byte boostId;

		public int votes;

		public GuiTexture voteState;

		public GuiLabel voteCountLbl;

		public GuiButtonFixed voteBtn;

		public FactionBoostVoteElements()
		{
		}
	}

	private class GalaxyVoteElements
	{
		public int galaxyId;

		public GuiTexture titleFrame;

		public GuiTexture bonusOneFrame;

		public GuiTexture bonusTwoFrame;

		public GuiLabel titleLbl;

		public GuiLabel bonusOneLbl;

		public GuiLabel bonusTwoLbl;

		public GalaxyVoteElements()
		{
		}
	}
}