using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TransferableObjects;
using UnityEngine;

public class RankingWindow : GuiWindow
{
	private GuiButtonFixed btnClose;

	private GuiTexture mainScreenBG;

	public List<GuiElement> forDelete = new List<GuiElement>();

	private GuiDropdown ddlType;

	private GuiDropdown ddlPeriod;

	private GuiLabel headerType;

	private GuiLabel lblTypeDescription;

	public bool isInPlayersMode = true;

	private List<GuiElement> toRemoveCommon = new List<GuiElement>();

	private int period;

	private int criteria;

	public static RankingData data;

	public static List<Guild> guilds;

	public int guildFirstPos;

	public RankingWindow()
	{
	}

	private void CleanCommon()
	{
		foreach (GuiElement guiElement in this.toRemoveCommon)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.toRemoveCommon.Clear();
	}

	private void CleanGuildsData()
	{
		foreach (GuiElement guiElement in this.forDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.forDelete.Clear();
	}

	private void CleanPlayersData()
	{
		foreach (GuiElement guiElement in this.forDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.forDelete.Clear();
	}

	public override void Create()
	{
		this.CleanPlayersData();
		base.SetBackgroundTexture("NewGUI", "novashop_frame");
		base.PutToCenter();
		this.zOrder = 210;
		this.isHidden = false;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("NewGUI", "ranking_frame1");
		guiTexture.X = 34f;
		guiTexture.Y = 40f;
		base.AddGuiElement(guiTexture);
		this.lblTypeDescription = new GuiLabel()
		{
			boundries = new Rect(54f, 325f, 193f, 155f),
			text = StaticData.Translate("key_ranking_exp_table_descr"),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor
		};
		base.AddGuiElement(this.lblTypeDescription);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("NewGUI", "ranking_avatar_frame");
		guiTexture1.X = 92f;
		guiTexture1.Y = 88f;
		base.AddGuiElement(guiTexture1);
		GuiTexture guiTexture2 = new GuiTexture()
		{
			boundries = new Rect(98f, 111f, 101f, 69f)
		};
		guiTexture2.SetTextureKeepSize("ShipsAvatars", NetworkScript.player.cfg.assetName);
		base.AddGuiElement(guiTexture2);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(55f, 61f, 190f, 20f),
			text = string.Format("{0} [{1}]", NetworkScript.player.playerBelongings.playerName, NetworkScript.player.playerBelongings.playerLevel),
			Font = GuiLabel.FontBold,
			FontSize = 14,
			Alignment = 4
		};
		base.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(55f, 215f, 190f, 20f),
			text = StaticData.Translate("key_ranking_level"),
			Font = GuiLabel.FontBold,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(55f, 215f, 190f, 20f),
			text = NetworkScript.player.playerBelongings.playerLevel.ToString("##,##0"),
			Font = GuiLabel.FontBold,
			FontSize = 12,
			Alignment = 5
		};
		base.AddGuiElement(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(55f, 245f, 160f, 20f),
			text = StaticData.Translate("key_ranking_fraction"),
			Font = GuiLabel.FontBold,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel3);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			boundries = new Rect(55f, 245f, 160f, 20f),
			text = (NetworkScript.player.vessel.fractionId != 1 ? StaticData.Translate("key_login_reg_fraction_two") : StaticData.Translate("key_login_reg_fraction_one")),
			Font = GuiLabel.FontBold,
			FontSize = 12,
			Alignment = 5
		};
		base.AddGuiElement(guiLabel4);
		GuiLabel guiLabel5 = new GuiLabel()
		{
			boundries = new Rect(55f, 275f, 190f, 20f),
			text = StaticData.Translate("key_ranking_guild"),
			Font = GuiLabel.FontBold,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel5);
		GuiLabel fontBold = new GuiLabel()
		{
			boundries = new Rect(55f, 275f, 190f, 20f)
		};
		if (NetworkScript.player.guild != null)
		{
			fontBold.text = NetworkScript.player.guild.name;
		}
		else
		{
			fontBold.text = "N/A";
		}
		fontBold.Font = GuiLabel.FontBold;
		fontBold.FontSize = 12;
		fontBold.Alignment = 5;
		base.AddGuiElement(fontBold);
		string str = string.Format("fraction{0}Icon", NetworkScript.player.vessel.fractionId);
		GuiTexture guiTexture3 = new GuiTexture();
		guiTexture3.SetTexture("FrameworkGUI", str);
		guiTexture3.X = 220f;
		guiTexture3.Y = 244f;
		base.AddGuiElement(guiTexture3);
		GuiTexture guiTexture4 = new GuiTexture()
		{
			boundries = new Rect(55f, 239f, 190f, 1f)
		};
		guiTexture4.SetTextureKeepSize("NewGUI", "table_line");
		base.AddGuiElement(guiTexture4);
		GuiTexture guiTexture5 = new GuiTexture()
		{
			boundries = new Rect(55f, 269f, 190f, 1f)
		};
		guiTexture5.SetTextureKeepSize("NewGUI", "table_line");
		base.AddGuiElement(guiTexture5);
		GuiTexture guiTexture6 = new GuiTexture()
		{
			boundries = new Rect(290f, 113f, 570f, 1f)
		};
		guiTexture6.SetTextureKeepSize("NewGUI", "table_line_header");
		base.AddGuiElement(guiTexture6);
		GuiLabel guiLabel6 = new GuiLabel()
		{
			boundries = new Rect(706f, 38f, 150f, 15f),
			text = StaticData.Translate("key_ranking_type"),
			Font = GuiLabel.FontBold,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel6);
		if (!this.isInPlayersMode)
		{
			this.CreateGuilds();
		}
		else
		{
			this.CreatePlayers();
		}
	}

	private void CreateGuilds()
	{
		this.CleanPlayersData();
		this.CleanCommon();
		this.lblTypeDescription.text = StaticData.Translate("key_ranking_exp_table_descr_guild");
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.SetSmallOrangeTexture();
		guiButtonResizeable.SetSmallOrangeTexture();
		guiButtonResizeable.X = 420f;
		guiButtonResizeable.Y = 58f;
		guiButtonResizeable.Width = 110f;
		guiButtonResizeable.Caption = StaticData.Translate("key_ranking_guilds");
		guiButtonResizeable.FontSize = 14;
		guiButtonResizeable.textColorNormal = Color.get_white();
		guiButtonResizeable.isHoverAware = false;
		guiButtonResizeable.Alignment = 4;
		base.AddGuiElement(guiButtonResizeable);
		this.toRemoveCommon.Add(guiButtonResizeable);
		GuiButtonResizeable _white = new GuiButtonResizeable();
		_white.SetSmallBlueTexture();
		_white.X = 290f;
		_white.Y = 58f;
		_white.Width = 110f;
		_white.Caption = StaticData.Translate("key_ranking_players");
		_white.FontSize = 14;
		_white.textColorNormal = Color.get_white();
		_white.textColorHover = Color.get_white();
		_white.Alignment = 4;
		_white.Clicked = new Action<EventHandlerParam>(this, RankingWindow.OnPlayersClicked);
		base.AddGuiElement(_white);
		this.toRemoveCommon.Add(_white);
		this.ddlType = new GuiDropdown()
		{
			X = 706f,
			Y = 58f
		};
		this.ddlType.boundries.set_width(150f);
		this.ddlType.AddItem(StaticData.Translate("key_ranking_order_ultra"), true);
		this.ddlType.AddItem(StaticData.Translate("key_ranking_order_level"), false);
		this.ddlType.AddItem(StaticData.Translate("key_ranking_order_members"), false);
		this.ddlType.OnItemSelected = new Action<int>(this, RankingWindow.OnGuildOrderChanged);
		base.AddGuiElement(this.ddlType);
		this.toRemoveCommon.Add(this.ddlType);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(290f, 95f, 60f, 14f),
			text = "#",
			Font = GuiLabel.FontBold,
			FontSize = 12,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel);
		this.toRemoveCommon.Add(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(370f, 95f, 120f, 14f),
			text = StaticData.Translate("key_ranking_guild2"),
			Font = GuiLabel.FontBold,
			FontSize = 12,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel1);
		this.toRemoveCommon.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(550f, 95f, 80f, 14f),
			text = StaticData.Translate("key_tag"),
			Font = GuiLabel.FontBold,
			FontSize = 12,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel2);
		this.toRemoveCommon.Add(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(615f, 95f, 80f, 14f),
			text = StaticData.Translate("key_members"),
			Font = GuiLabel.FontBold,
			FontSize = 12,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel3);
		this.toRemoveCommon.Add(guiLabel3);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			boundries = new Rect(690f, 95f, 100f, 14f),
			text = StaticData.Translate("key_ultralibrium"),
			Font = GuiLabel.FontBold,
			FontSize = 12,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel4);
		this.toRemoveCommon.Add(guiLabel4);
		GuiLabel guiLabel5 = new GuiLabel()
		{
			boundries = new Rect(810f, 95f, 80f, 14f),
			text = StaticData.Translate("key_ranking_options"),
			Font = GuiLabel.FontBold,
			FontSize = 12,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel5);
		this.toRemoveCommon.Add(guiLabel5);
		playWebGame.udp.ExecuteCommand(189, null);
	}

	private void CreatePlayers()
	{
		// 
		// Current member / type: System.Void RankingWindow::CreatePlayers()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void CreatePlayers()
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

	public override void OnClose()
	{
		// 
		// Current member / type: System.Void RankingWindow::OnClose()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnClose()
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

	private void OnFirst(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void RankingWindow::OnFirst(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnFirst(EventHandlerParam)
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

	private void OnFirstGuild(EventHandlerParam prm)
	{
		this.guildFirstPos = 0;
		this.RedrawDataGuilds();
	}

	private void OnGuildOrderChanged(int val)
	{
		switch (val)
		{
			case 0:
			{
				List<Guild> list = RankingWindow.guilds;
				if (RankingWindow.<>f__am$cacheE == null)
				{
					RankingWindow.<>f__am$cacheE = new Func<Guild, long>(null, (Guild o) => o.bankUltralibrium);
				}
				IOrderedEnumerable<Guild> orderedEnumerable = Enumerable.OrderByDescending<Guild, long>(list, RankingWindow.<>f__am$cacheE);
				if (RankingWindow.<>f__am$cacheF == null)
				{
					RankingWindow.<>f__am$cacheF = new Func<Guild, string>(null, (Guild o2) => o2.name);
				}
				RankingWindow.guilds = Enumerable.ToList<Guild>(Enumerable.ThenBy<Guild, string>(orderedEnumerable, RankingWindow.<>f__am$cacheF));
				break;
			}
			case 1:
			{
				List<Guild> list1 = RankingWindow.guilds;
				if (RankingWindow.<>f__am$cache10 == null)
				{
					RankingWindow.<>f__am$cache10 = new Func<Guild, byte>(null, (Guild o) => o.get_Level());
				}
				IOrderedEnumerable<Guild> orderedEnumerable1 = Enumerable.OrderByDescending<Guild, byte>(list1, RankingWindow.<>f__am$cache10);
				if (RankingWindow.<>f__am$cache11 == null)
				{
					RankingWindow.<>f__am$cache11 = new Func<Guild, string>(null, (Guild o2) => o2.name);
				}
				RankingWindow.guilds = Enumerable.ToList<Guild>(Enumerable.ThenBy<Guild, string>(orderedEnumerable1, RankingWindow.<>f__am$cache11));
				break;
			}
			case 2:
			{
				List<Guild> list2 = RankingWindow.guilds;
				if (RankingWindow.<>f__am$cache12 == null)
				{
					RankingWindow.<>f__am$cache12 = new Func<Guild, int>(null, (Guild o) => o.members.get_Count());
				}
				IOrderedEnumerable<Guild> orderedEnumerable2 = Enumerable.OrderByDescending<Guild, int>(list2, RankingWindow.<>f__am$cache12);
				if (RankingWindow.<>f__am$cache13 == null)
				{
					RankingWindow.<>f__am$cache13 = new Func<Guild, string>(null, (Guild o2) => o2.name);
				}
				RankingWindow.guilds = Enumerable.ToList<Guild>(Enumerable.ThenBy<Guild, string>(orderedEnumerable2, RankingWindow.<>f__am$cache13));
				break;
			}
		}
		this.RedrawDataGuilds();
	}

	private void OnGuildsClicked(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void RankingWindow::OnGuildsClicked(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnGuildsClicked(EventHandlerParam)
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

	private void OnLast(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void RankingWindow::OnLast(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnLast(EventHandlerParam)
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

	private void OnLastGuild(EventHandlerParam prm)
	{
		int num = (RankingWindow.guilds.get_Count() % 15 == 0 ? 0 : 1) + RankingWindow.guilds.get_Count() / 15;
		this.guildFirstPos = (num - 1) * 15;
		this.RedrawDataGuilds();
	}

	private void OnNext(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void RankingWindow::OnNext(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnNext(EventHandlerParam)
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

	private void OnNextGuild(EventHandlerParam prm)
	{
		RankingWindow rankingWindow = this;
		rankingWindow.guildFirstPos = rankingWindow.guildFirstPos + 15;
		this.RedrawDataGuilds();
	}

	private void OnPeriodChanged(int newPeriod)
	{
		// 
		// Current member / type: System.Void RankingWindow::OnPeriodChanged(System.Int32)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnPeriodChanged(System.Int32)
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

	private void OnPlayersClicked(EventHandlerParam prm)
	{
		this.isInPlayersMode = true;
		this.CreatePlayers();
	}

	private void OnPrev(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void RankingWindow::OnPrev(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnPrev(EventHandlerParam)
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

	private void OnPrevGuilds(EventHandlerParam prm)
	{
		RankingWindow rankingWindow = this;
		rankingWindow.guildFirstPos = rankingWindow.guildFirstPos - 15;
		this.RedrawDataGuilds();
	}

	private void OnSendMessageClicked(EventHandlerParam prm)
	{
		if (__ChatWindow.wnd == null)
		{
			__ChatWindow.OpenTheWindow(true);
		}
		__ChatWindow.wnd.StartChatByUsername((string)prm.customData);
	}

	private void OnTrackMe(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void RankingWindow::OnTrackMe(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnTrackMe(EventHandlerParam)
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

	private void OnTypeChanged(int newType)
	{
		// 
		// Current member / type: System.Void RankingWindow::OnTypeChanged(System.Int32)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnTypeChanged(System.Int32)
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

	private void OnViewGuildClick(EventHandlerParam param)
	{
		playWebGame.udp.ExecuteCommand(179, (Guild)param.customData, 2);
	}

	private void OnViewProfilClick(EventHandlerParam prm)
	{
		ProfileScreen.playerUserName = (string)prm.customData;
		MainScreenWindow mainScreenWindow = AndromedaGui.mainWnd;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = (byte)17
		};
		mainScreenWindow.OnWindowBtnClicked(eventHandlerParam);
	}

	public void RedrawDataGuilds()
	{
		this.CleanGuildsData();
		if (RankingWindow.guilds == null)
		{
			GuiLabel guiLabel = new GuiLabel()
			{
				FontSize = 16,
				boundries = new Rect(400f, 250f, 280f, 40f),
				Alignment = 4,
				text = StaticData.Translate("key_ranking_receiving_data"),
				TextColor = GuiNewStyleBar.blueColor
			};
			base.AddGuiElement(guiLabel);
			this.forDelete.Add(guiLabel);
			return;
		}
		int num = 115;
		int num1 = 24;
		for (int i = 0; i < Math.Min(RankingWindow.guilds.get_Count() - this.guildFirstPos, 15); i++)
		{
			int num2 = i + this.guildFirstPos;
			Guild item = RankingWindow.guilds.get_Item(num2);
			int num3 = num + i * num1;
			int num4 = 290;
			int num5 = 12;
			Color _white = Color.get_white();
			GuiLabel guiLabel1 = new GuiLabel()
			{
				text = (num2 + 1).ToString("##,##0"),
				TextColor = _white,
				FontSize = num5,
				Font = GuiLabel.FontBold,
				boundries = new Rect((float)num4, (float)num3, 60f, (float)num1),
				Alignment = 3
			};
			base.AddGuiElement(guiLabel1);
			this.forDelete.Add(guiLabel1);
			string str = string.Format("fraction{0}Icon", item.fractionId);
			GuiTexture guiTexture = new GuiTexture();
			guiTexture.SetTexture("FrameworkGUI", str);
			guiTexture.X = (float)(num4 + 75);
			guiTexture.Y = (float)(num3 + 3);
			base.AddGuiElement(guiTexture);
			this.forDelete.Add(guiTexture);
			GuiLabel guiLabel2 = new GuiLabel()
			{
				text = string.Format("{0} [{1}]", item.name, item.get_Level()),
				TextColor = _white,
				FontSize = num5,
				Font = GuiLabel.FontBold,
				boundries = new Rect((float)(num4 + 100), (float)num3, 140f, (float)num1),
				Alignment = 3
			};
			base.AddGuiElement(guiLabel2);
			this.forDelete.Add(guiLabel2);
			GuiLabel guiLabel3 = new GuiLabel()
			{
				text = item.title,
				TextColor = _white,
				FontSize = num5,
				Font = GuiLabel.FontBold,
				boundries = new Rect((float)(num4 + 260), (float)num3, 100f, (float)num1),
				Alignment = 3
			};
			base.AddGuiElement(guiLabel3);
			this.forDelete.Add(guiLabel3);
			GuiLabel guiLabel4 = new GuiLabel()
			{
				text = item.members.get_Count().ToString(),
				TextColor = _white,
				FontSize = num5,
				Font = GuiLabel.FontBold,
				boundries = new Rect((float)(num4 + 343), (float)num3, 100f, (float)num1),
				Alignment = 3
			};
			base.AddGuiElement(guiLabel4);
			this.forDelete.Add(guiLabel4);
			GuiLabel guiLabel5 = new GuiLabel()
			{
				text = item.bankUltralibrium.ToString("##,##0"),
				TextColor = _white,
				FontSize = num5,
				Font = GuiLabel.FontBold,
				boundries = new Rect((float)(num4 + 375 + 40), (float)num3, 100f, (float)num1),
				Alignment = 3
			};
			base.AddGuiElement(guiLabel5);
			this.forDelete.Add(guiLabel5);
			GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
			guiButtonFixed.SetTexture("NewGUI", "option_ViewProfile");
			guiButtonFixed.Caption = string.Empty;
			guiButtonFixed.X = (float)(num4 + 530);
			guiButtonFixed.Y = (float)(num3 + 3);
			guiButtonFixed.ToolTipText = StaticData.Translate("key_ranking_view_profile");
			guiButtonFixed.eventHandlerParam = new EventHandlerParam()
			{
				customData = item
			};
			guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, RankingWindow.OnViewGuildClick);
			base.AddGuiElement(guiButtonFixed);
			this.forDelete.Add(guiButtonFixed);
			GuiTexture guiTexture1 = new GuiTexture()
			{
				boundries = new Rect((float)num4, (float)(num3 + 25), 570f, 1f)
			};
			guiTexture1.SetTextureKeepSize("NewGUI", "table_line");
			base.AddGuiElement(guiTexture1);
			this.forDelete.Add(guiTexture1);
		}
		int num6 = this.guildFirstPos / 15;
		int num7 = (RankingWindow.guilds.get_Count() % 15 == 0 ? 0 : 1) + RankingWindow.guilds.get_Count() / 15;
		GuiLabel guiLabel6 = new GuiLabel()
		{
			boundries = new Rect(500f, 496f, 130f, 23f),
			text = string.Format("{0:###,##0} / {1:###,##0}", num6 + 1, num7),
			TextColor = Color.get_white(),
			FontSize = 13,
			Font = GuiLabel.FontBold,
			Alignment = 4
		};
		base.AddGuiElement(guiLabel6);
		this.forDelete.Add(guiLabel6);
		int num8 = num7 - 1;
		if (num6 > 0)
		{
			GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
			guiButtonResizeable.SetTexture("NewGUI", "btnLeft_");
			guiButtonResizeable.Caption = StaticData.Translate("key_ranking_prev");
			guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, RankingWindow.OnPrevGuilds);
			guiButtonResizeable.FontSize = 12;
			guiButtonResizeable.Alignment = 3;
			guiButtonResizeable._marginLeft = 20;
			guiButtonResizeable.X = 390f;
			guiButtonResizeable.Y = 496f;
			guiButtonResizeable.Width = 130f;
			base.AddGuiElement(guiButtonResizeable);
			this.forDelete.Add(guiButtonResizeable);
			GuiButtonFixed empty = new GuiButtonFixed();
			empty.SetTexture("NewGUI", "btnLeftArrow");
			empty.X = 343f;
			empty.Y = 496f;
			empty.Caption = string.Empty;
			empty.Clicked = new Action<EventHandlerParam>(this, RankingWindow.OnFirstGuild);
			base.AddGuiElement(empty);
			this.forDelete.Add(empty);
		}
		if (num6 < num8)
		{
			GuiButtonResizeable action = new GuiButtonResizeable();
			action.SetTexture("NewGUI", "btnRight_");
			action.Caption = StaticData.Translate("key_ranking_next");
			action.Clicked = new Action<EventHandlerParam>(this, RankingWindow.OnNextGuild);
			action.FontSize = 12;
			action.Alignment = 5;
			action.MarginRight = 20;
			action.X = 611f;
			action.Y = 496f;
			action.Width = 130f;
			base.AddGuiElement(action);
			this.forDelete.Add(action);
			GuiButtonFixed guiButtonFixed1 = new GuiButtonFixed();
			guiButtonFixed1.SetTexture("NewGUI", "btnRightArrow");
			guiButtonFixed1.X = 745f;
			guiButtonFixed1.Y = 496f;
			guiButtonFixed1.Caption = string.Empty;
			guiButtonFixed1.Clicked = new Action<EventHandlerParam>(this, RankingWindow.OnLastGuild);
			base.AddGuiElement(guiButtonFixed1);
			this.forDelete.Add(guiButtonFixed1);
		}
		base.MoveToTop(this.ddlType);
	}

	public void RedrawDataPlayers()
	{
		// 
		// Current member / type: System.Void RankingWindow::RedrawDataPlayers()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void RedrawDataPlayers()
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..(TypeReference ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\IntegerTypesHierarchyBuilder.cs:строка 35
		//    в ..(BinaryExpression , VariableReference ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\ClassHierarchyBuilder.cs:строка 497
		//    в ..(Expression , VariableReference ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\ClassHierarchyBuilder.cs:строка 370
		//    в ..(Instruction , VariableReference ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\ClassHierarchyBuilder.cs:строка 340
		//    в ..(Int32 , ClassHierarchyNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\IntegerTypesHierarchyBuilder.cs:строка 89
		//    в ..(HashSet`1 ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\ClassHierarchyBuilder.cs:строка 73
		//    в ..(HashSet`1 ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\IntegerTypeInferer.cs:строка 23
		//    в Telerik.JustDecompiler.Decompiler.TypeInference.TypeInferer.(HashSet`1 ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\TypeInferer.cs:строка 342
		//    в Telerik.JustDecompiler.Decompiler.TypeInference.TypeInferer.() в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\TypeInference\TypeInferer.cs:строка 329
		//    в Telerik.JustDecompiler.Decompiler.ExpressionDecompilerStep.(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\ExpressionDecompilerStep.cs:строка 88
		//    в ..(MethodBody ,  , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 88
		//    в ..(MethodBody , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 70
		//    в Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 95
		//    в Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 58
		//    в ..(ILanguage , MethodDefinition ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:строка 117
		// 
		// mailto: JustDecompilePublicFeedback@telerik.com

	}
}