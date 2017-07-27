using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TransferableObjects;
using UnityEngine;

public class GuildWindow : GuiWindow
{
	public const int MAX_RANKS_PER_GUILD = 20;

	private bool isFirstCreate = true;

	public static int membersCount;

	private GuildWindow.MemberListItem[] members2edit;

	private int editingMemberIndex = -1;

	public List<GuiElement> forDelete = new List<GuiElement>();

	public List<GuiElement> forDeleteFromLeftPanel = new List<GuiElement>();

	private GuiLabel lblGuildName;

	private PlayerData player;

	private Guild guild;

	private int selectedTab = 1;

	private GuiTexture txTab;

	private GuiLabel lblErrorName;

	private GuiLabel lblErrorTitle;

	private GuiLabel lblErrorDescription;

	private GuiLabel lblErrorLeave;

	public static GuiWindow wndPreview;

	public static byte subSection;

	private GuiLabel lblErrorDepositUltralibrium;

	private GuiLabel lblErrorDepositEquilibrium;

	private GuiLabel lblErrorDepositNova;

	private GuiLabel lblErrorDepositMain;

	private GuiWindow dialogWindow;

	private int overviewTabIndex = 1;

	private GuiTextBox tbDepositUltralibrium;

	private GuiTextBox tbDepositEquilibrium;

	private GuiTextBox tbDepositNova;

	private GuiLabel bankLblEquilibrium;

	private GuiLabel bankLblUltralibrium;

	private GuiLabel bankLblNova;

	private GuiLabel leftPanelLblEquilibrium;

	private GuiLabel leftPanelLblUltralibrium;

	private GuiLabel leftPanelLblNova;

	private GuiTexture txOrangeLine;

	private GuiScrollingContainer aScroller;

	private GuiTextBox tbGuildName;

	private GuiTextBox tbGuildTitle;

	private GuiTextBox tbDescription;

	private int hoveredItemIndex = -1;

	private GuiScrollingContainer membersScroller;

	private GuiDialog dlgConfirmRemovePlayer;

	private GuiLabel lblErrorInvitations;

	private GuiTextBox tbInvite;

	private int playerInventorySize;

	private List<GuiElement> lockedInventorySlots = new List<GuiElement>();

	private List<GuiElement> lockedGuildVaultSlots = new List<GuiElement>();

	private UniversalTransportContainer expandSlotData;

	private ExpandSlotDialog expandSlotWindow;

	private GuiWindow expandGuildVaultWindow;

	private GuiCheckbox cbCanInvite;

	private GuiCheckbox cbCanChat;

	private GuiCheckbox cbCanBank;

	private GuiCheckbox cbCanPromote;

	private GuiCheckbox cbCanVault;

	private GuiCheckbox cbCanEditGuild;

	private GuiCheckbox cbIsMaster;

	private GuiTexture txRankEdited;

	private GuiTextBox tbRankName;

	private GuiLabel lblErrRankName;

	private GuildRank editedRank;

	private int editedRankIndex;

	public static string[] rankTextures;

	private bool CanBank
	{
		get
		{
			return (NetworkScript.player.guildMember.rank.canBank ? true : NetworkScript.player.guildMember.rank.isMaster);
		}
	}

	private bool CanEditDetails
	{
		get
		{
			return (NetworkScript.player.guildMember.rank.canEditDetails ? true : NetworkScript.player.guildMember.rank.isMaster);
		}
	}

	private bool CanInvite
	{
		get
		{
			return (NetworkScript.player.guildMember.rank.canInvite ? true : NetworkScript.player.guildMember.rank.isMaster);
		}
	}

	private bool CanPromote
	{
		get
		{
			return (NetworkScript.player.guildMember.rank.canPromote ? true : NetworkScript.player.guildMember.rank.isMaster);
		}
	}

	private bool CanVault
	{
		get
		{
			return (NetworkScript.player.guildMember.rank.canVault ? true : NetworkScript.player.guildMember.rank.isMaster);
		}
	}

	private GuildRank[] OrderedRanks
	{
		get
		{
			if (NetworkScript.player.guildMember.rank.isMaster)
			{
				IList<GuildRank> values = this.guild.ranks.get_Values();
				if (GuildWindow.<>f__am$cache3D == null)
				{
					GuildWindow.<>f__am$cache3D = new Func<GuildRank, short>(null, (GuildRank o) => o.sortIndex);
				}
				return Enumerable.ToArray<GuildRank>(Enumerable.OrderBy<GuildRank, short>(values, GuildWindow.<>f__am$cache3D));
			}
			IList<GuildRank> list = this.guild.ranks.get_Values();
			if (GuildWindow.<>f__am$cache3E == null)
			{
				GuildWindow.<>f__am$cache3E = new Func<GuildRank, bool>(null, (GuildRank w) => !w.isMaster);
			}
			IEnumerable<GuildRank> enumerable = Enumerable.Where<GuildRank>(list, GuildWindow.<>f__am$cache3E);
			if (GuildWindow.<>f__am$cache3F == null)
			{
				GuildWindow.<>f__am$cache3F = new Func<GuildRank, short>(null, (GuildRank o) => o.sortIndex);
			}
			return Enumerable.ToArray<GuildRank>(Enumerable.OrderBy<GuildRank, short>(enumerable, GuildWindow.<>f__am$cache3F));
		}
	}

	static GuildWindow()
	{
		GuildWindow.membersCount = 0;
		GuildWindow.subSection = 0;
		GuildWindow.rankTextures = new string[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
	}

	public GuildWindow()
	{
	}

	private void CancelGuildVaultExpand(object prm)
	{
		AndromedaGui.gui.RemoveWindow(this.expandGuildVaultWindow.handler);
		this.expandGuildVaultWindow = null;
		AndromedaGui.gui.activeToolTipId = -1;
	}

	private bool CanInviteToParty(PlayerBasic plr)
	{
		GuildWindow.<CanInviteToParty>c__AnonStorey21 variable = null;
		if (!plr.isOnline || plr.isInParty || plr.userName == NetworkScript.player.playerBelongings.playerName || NetworkScript.player.galaxyId >= 2000 || plr.fractionId != NetworkScript.player.vessel.fractionId || NetworkScript.partyInvitees.get_Count() >= 3 || Enumerable.FirstOrDefault<PartyInvite>(Enumerable.Where<PartyInvite>(NetworkScript.partyInvitees.get_Values(), new Func<PartyInvite, bool>(variable, (PartyInvite i) => i.name == this.plr.userName))) != null)
		{
			return false;
		}
		if (NetworkScript.party != null && NetworkScript.party.members != null)
		{
			if (NetworkScript.party.members.get_Count() > 0)
			{
				if (NetworkScript.party.members.get_Item(0).playerId != NetworkScript.player.playId)
				{
					return false;
				}
				if (Enumerable.FirstOrDefault<PartyMemberClientSide>(Enumerable.Where<PartyMemberClientSide>(NetworkScript.party.members, new Func<PartyMemberClientSide, bool>(variable, (PartyMemberClientSide w) => w.playerName == this.plr.userName))) != null)
				{
					return false;
				}
			}
			if (NetworkScript.party.members.get_Count() >= 4)
			{
				return false;
			}
		}
		return true;
	}

	private void ClearDetailsErrors()
	{
		if (this.lblErrorName != null)
		{
			this.lblErrorName.text = string.Empty;
		}
		if (this.lblErrorTitle != null)
		{
			this.lblErrorTitle.text = string.Empty;
		}
		if (this.lblErrorDescription != null)
		{
			this.lblErrorDescription.text = string.Empty;
		}
		if (this.lblErrorLeave != null)
		{
			this.lblErrorLeave.text = string.Empty;
		}
		if (this.lblErrRankName != null)
		{
			this.lblErrRankName.text = string.Empty;
		}
	}

	private void ConfirmExpand(EventHandlerParam prm)
	{
		SelectedCurrency selectedCurrency = (int)prm.customData;
		Debug.Log(selectedCurrency);
		if (this.expandSlotData == null || this.expandSlotData.wantedSlot != 7)
		{
			this.expandSlotData.paymentCurrency = selectedCurrency;
			playWebGame.udp.ExecuteCommand(PureUdpClient.CommandExpandSlots, this.expandSlotData, 49);
			this.CancelGuildVaultExpand(null);
		}
		else
		{
			int num = 0;
			NetworkScript.player.playerBelongings.ExpandInventory(ref num);
			playWebGame.udp.ExecuteCommand(PureUdpClient.CommandExpandSlots, this.expandSlotData, 49);
			this.expandSlotWindow.Cancel();
			this.DeleteAll();
			this.Create();
		}
	}

	public static int CountStringOccurrences(string text, string pattern)
	{
		int num = 0;
		int length = 0;
		while (true)
		{
			int num1 = text.IndexOf(pattern, length);
			length = num1;
			if (num1 == -1)
			{
				break;
			}
			length = length + pattern.get_Length();
			num++;
		}
		return num;
	}

	public override void Create()
	{
		// 
		// Current member / type: System.Void GuildWindow::Create()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void Create()
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

	private void CreateBank()
	{
		float single = 585f;
		float single1 = 115f;
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(single, single1, 280f, 30f),
			Alignment = 4,
			text = StaticData.Translate("key_guild_bank"),
			FontSize = 16,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiTexture guiTexture = new GuiTexture()
		{
			X = single,
			Y = single1 + 31f
		};
		guiTexture.SetTexture("NewGUI", "blueDot");
		guiTexture.boundries.set_width(280f);
		guiTexture.boundries.set_height(1f);
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture()
		{
			boundries = new Rect(single, single1 + 60f, 70f, 70f)
		};
		guiTexture1.SetTextureKeepSize("NewGUI", "guild_bank_group");
		base.AddGuiElement(guiTexture1);
		this.forDelete.Add(guiTexture1);
		GuiTexture guiTexture2 = new GuiTexture()
		{
			boundries = new Rect(single, single1 + 150f, 70f, 70f)
		};
		guiTexture2.SetTextureKeepSize("NewGUI", "guild_bank_group");
		base.AddGuiElement(guiTexture2);
		this.forDelete.Add(guiTexture2);
		GuiTexture guiTexture3 = new GuiTexture()
		{
			boundries = new Rect(single, single1 + 240f, 70f, 70f)
		};
		guiTexture3.SetTextureKeepSize("NewGUI", "guild_bank_group");
		base.AddGuiElement(guiTexture3);
		this.forDelete.Add(guiTexture3);
		GuiTexture guiTexture4 = new GuiTexture()
		{
			X = single + 14f,
			Y = single1 + 75f
		};
		guiTexture4.SetTexture("NewGUI", "ultralibrium_icon_big");
		base.AddGuiElement(guiTexture4);
		this.forDelete.Add(guiTexture4);
		GuiTexture guiTexture5 = new GuiTexture()
		{
			X = single + 14f,
			Y = single1 + 165f
		};
		guiTexture5.SetTexture("NewGUI", "equilibrium_icon_big");
		base.AddGuiElement(guiTexture5);
		this.forDelete.Add(guiTexture5);
		GuiTexture guiTexture6 = new GuiTexture()
		{
			X = single + 10f,
			Y = single1 + 260f
		};
		guiTexture6.SetTexture("NewGUI", "nova_icon_big");
		base.AddGuiElement(guiTexture6);
		this.forDelete.Add(guiTexture6);
		this.bankLblUltralibrium = new GuiLabel()
		{
			boundries = new Rect(single + 80f, single1 + 70f, 160f, 20f),
			Alignment = 5,
			FontSize = 16,
			text = this.guild.bankUltralibrium.ToString("##,##0"),
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.greenColor
		};
		base.AddGuiElement(this.bankLblUltralibrium);
		this.forDelete.Add(this.bankLblUltralibrium);
		this.bankLblEquilibrium = new GuiLabel()
		{
			boundries = new Rect(single + 80f, single1 + 160f, 160f, 20f),
			Alignment = 5,
			FontSize = 16,
			text = this.guild.bankEquilib.ToString("##,##0"),
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.purpleColor
		};
		base.AddGuiElement(this.bankLblEquilibrium);
		this.forDelete.Add(this.bankLblEquilibrium);
		this.bankLblNova = new GuiLabel()
		{
			boundries = new Rect(single + 80f, single1 + 250f, 160f, 20f),
			Alignment = 5,
			FontSize = 16,
			text = this.guild.bankNova.ToString("##,##0"),
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor
		};
		base.AddGuiElement(this.bankLblNova);
		this.forDelete.Add(this.bankLblNova);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.SetSmallBlueTexture();
		guiButtonResizeable.boundries = new Rect(single + 75f, single1 + 100f, 30f, 25f);
		guiButtonResizeable.MarginTop = -2;
		guiButtonResizeable._marginLeft = -2;
		guiButtonResizeable.Alignment = 4;
		guiButtonResizeable.FontSize = 26;
		guiButtonResizeable.Caption = "-";
		guiButtonResizeable.textColorNormal = Color.get_white();
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = (byte)0,
			customData2 = false
		};
		guiButtonResizeable.eventHandlerParam = eventHandlerParam;
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnBankDepossiteSmallBtnClicked);
		base.AddGuiElement(guiButtonResizeable);
		this.forDelete.Add(guiButtonResizeable);
		GuiButtonResizeable rect = new GuiButtonResizeable();
		rect.SetSmallBlueTexture();
		rect.boundries = new Rect(single + 75f, single1 + 190f, 30f, 25f);
		rect.MarginTop = -2;
		rect._marginLeft = -2;
		rect.FontSize = 26;
		rect.Caption = "-";
		rect.textColorNormal = Color.get_white();
		rect.Alignment = 4;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = (byte)1,
			customData2 = false
		};
		rect.eventHandlerParam = eventHandlerParam;
		rect.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnBankDepossiteSmallBtnClicked);
		base.AddGuiElement(rect);
		this.forDelete.Add(rect);
		GuiButtonResizeable _white = new GuiButtonResizeable();
		_white.SetSmallBlueTexture();
		_white.MarginTop = -2;
		_white._marginLeft = -2;
		_white.boundries = new Rect(single + 75f, single1 + 280f, 30f, 25f);
		_white.FontSize = 26;
		_white.Caption = "-";
		_white.textColorNormal = Color.get_white();
		_white.Alignment = 4;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = (byte)2,
			customData2 = false
		};
		_white.eventHandlerParam = eventHandlerParam;
		_white.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnBankDepossiteSmallBtnClicked);
		base.AddGuiElement(_white);
		this.forDelete.Add(_white);
		GuiButtonResizeable action = new GuiButtonResizeable();
		action.SetSmallBlueTexture();
		action.MarginTop = -2;
		action._marginLeft = -2;
		action.boundries = new Rect(single + 250f, single1 + 100f, 30f, 25f);
		action.FontSize = 26;
		action.Caption = "+";
		action.textColorNormal = Color.get_white();
		action.Alignment = 4;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = (byte)0,
			customData2 = true
		};
		action.eventHandlerParam = eventHandlerParam;
		action.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnBankDepossiteSmallBtnClicked);
		base.AddGuiElement(action);
		this.forDelete.Add(action);
		GuiButtonResizeable guiButtonResizeable1 = new GuiButtonResizeable();
		guiButtonResizeable1.SetSmallBlueTexture();
		guiButtonResizeable1.MarginTop = -2;
		guiButtonResizeable1._marginLeft = -2;
		guiButtonResizeable1.boundries = new Rect(single + 250f, single1 + 190f, 30f, 25f);
		guiButtonResizeable1.FontSize = 26;
		guiButtonResizeable1.Caption = "+";
		guiButtonResizeable1.textColorNormal = Color.get_white();
		guiButtonResizeable1.Alignment = 4;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = (byte)1,
			customData2 = true
		};
		guiButtonResizeable1.eventHandlerParam = eventHandlerParam;
		guiButtonResizeable1.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnBankDepossiteSmallBtnClicked);
		base.AddGuiElement(guiButtonResizeable1);
		this.forDelete.Add(guiButtonResizeable1);
		GuiButtonResizeable rect1 = new GuiButtonResizeable();
		rect1.SetSmallBlueTexture();
		rect1.MarginTop = -2;
		rect1._marginLeft = -2;
		rect1.boundries = new Rect(single + 250f, single1 + 280f, 30f, 25f);
		rect1.FontSize = 26;
		rect1.Caption = "+";
		rect1.textColorNormal = Color.get_white();
		rect1.Alignment = 4;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = (byte)2,
			customData2 = true
		};
		rect1.eventHandlerParam = eventHandlerParam;
		rect1.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnBankDepossiteSmallBtnClicked);
		base.AddGuiElement(rect1);
		this.forDelete.Add(rect1);
		this.tbDepositUltralibrium = new GuiTextBox();
		this.tbDepositUltralibrium.SetFrameTexture("NewGUI", "guildTB");
		this.tbDepositUltralibrium.boundries = new Rect(single + 105f, single1 + 93f, 145f, 30f);
		this.tbDepositUltralibrium.FontSize = 14;
		this.tbDepositUltralibrium.Alignment = 8;
		this.tbDepositUltralibrium.TextColor = GuiNewStyleBar.blueColor;
		this.tbDepositUltralibrium.text = "0";
		base.AddGuiElement(this.tbDepositUltralibrium);
		this.forDelete.Add(this.tbDepositUltralibrium);
		this.tbDepositEquilibrium = new GuiTextBox();
		this.tbDepositEquilibrium.SetFrameTexture("NewGUI", "guildTB");
		this.tbDepositEquilibrium.boundries = new Rect(single + 105f, single1 + 183f, 145f, 30f);
		this.tbDepositEquilibrium.FontSize = 14;
		this.tbDepositEquilibrium.Alignment = 8;
		this.tbDepositEquilibrium.TextColor = GuiNewStyleBar.blueColor;
		this.tbDepositEquilibrium.text = "0";
		base.AddGuiElement(this.tbDepositEquilibrium);
		this.forDelete.Add(this.tbDepositEquilibrium);
		this.tbDepositNova = new GuiTextBox();
		this.tbDepositNova.SetFrameTexture("NewGUI", "guildTB");
		this.tbDepositNova.boundries = new Rect(single + 105f, single1 + 273f, 145f, 30f);
		this.tbDepositNova.FontSize = 14;
		this.tbDepositNova.Alignment = 8;
		this.tbDepositNova.TextColor = GuiNewStyleBar.blueColor;
		this.tbDepositNova.text = "0";
		base.AddGuiElement(this.tbDepositNova);
		this.forDelete.Add(this.tbDepositNova);
		this.lblErrorDepositUltralibrium = new GuiLabel()
		{
			boundries = new Rect(single + 75f, single1 + 127f, 205f, 16f),
			TextColor = GuiNewStyleBar.redColor,
			text = string.Empty,
			Alignment = 4
		};
		base.AddGuiElement(this.lblErrorDepositUltralibrium);
		this.forDelete.Add(this.lblErrorDepositUltralibrium);
		this.lblErrorDepositEquilibrium = new GuiLabel()
		{
			boundries = new Rect(single + 75f, single1 + 217f, 205f, 16f),
			TextColor = GuiNewStyleBar.redColor,
			text = string.Empty,
			Alignment = 4
		};
		base.AddGuiElement(this.lblErrorDepositEquilibrium);
		this.forDelete.Add(this.lblErrorDepositEquilibrium);
		this.lblErrorDepositNova = new GuiLabel()
		{
			boundries = new Rect(single + 75f, single1 + 307f, 205f, 16f),
			TextColor = GuiNewStyleBar.redColor,
			text = string.Empty,
			Alignment = 4
		};
		base.AddGuiElement(this.lblErrorDepositNova);
		this.forDelete.Add(this.lblErrorDepositNova);
		this.lblErrorDepositMain = new GuiLabel()
		{
			boundries = new Rect(single + 75f, single1 + 380f, 205f, 16f),
			TextColor = GuiNewStyleBar.redColor,
			text = string.Empty,
			Alignment = 4
		};
		base.AddGuiElement(this.lblErrorDepositMain);
		this.forDelete.Add(this.lblErrorDepositMain);
		GuiButtonResizeable guiButtonResizeable2 = new GuiButtonResizeable()
		{
			Caption = StaticData.Translate("key_guild_deposit"),
			textColorNormal = Color.get_white(),
			Alignment = 4,
			Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnDepositClicked),
			boundries = new Rect(single + 105f, single1 + 350f, 145f, 30f)
		};
		guiButtonResizeable2.SetTexture("NewGUI", "guildBtnBlue");
		guiButtonResizeable2.MarginTop = -3;
		base.AddGuiElement(guiButtonResizeable2);
		this.forDelete.Add(guiButtonResizeable2);
	}

	private void CreateBtnCommon(int index, string caption)
	{
		GuiButton guiButton = new GuiButton()
		{
			boundries = new Rect(this.txTab.X + 20f - 132f + (float)(132 * index), this.txTab.Y + 8f - 7f - 26f, 120f, 70f),
			Caption = StaticData.Translate(caption),
			FontSize = 16,
			Alignment = 4,
			MarginTop = 5,
			Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnBtnCommonClicked),
			textColorHover = Color.get_white(),
			textColorNormal = GuiNewStyleBar.blueColor,
			textColorDisabled = GuiNewStyleBar.blueColorDisable
		};
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = index
		};
		guiButton.eventHandlerParam = eventHandlerParam;
		base.AddGuiElement(guiButton);
		this.forDelete.Add(guiButton);
		if (index == 5)
		{
			if (NetworkScript.player.guildMember.rank.isMaster)
			{
				guiButton.tooltipWindowParam = null;
				guiButton.drawTooltipWindowCallback = null;
			}
			else
			{
				guiButton.textColorDisabled = Color.get_gray();
				guiButton.isEnabled = false;
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_guild_no_ranks_permission_tooltip"),
					customData2 = guiButton
				};
				guiButton.tooltipWindowParam = eventHandlerParam;
				guiButton.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			}
		}
		if (index == 4 && !NetworkScript.player.guildMember.rank.isMaster && !NetworkScript.player.guildMember.rank.canVault)
		{
			if (NetworkScript.player.guildMember.rank.isMaster || NetworkScript.player.guildMember.rank.canVault)
			{
				guiButton.tooltipWindowParam = null;
				guiButton.drawTooltipWindowCallback = null;
			}
			else
			{
				guiButton.textColorDisabled = Color.get_gray();
				guiButton.isEnabled = false;
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_guild_no_vault_permission_tooltip"),
					customData2 = guiButton
				};
				guiButton.tooltipWindowParam = eventHandlerParam;
				guiButton.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			}
		}
	}

	private void CreateBtnInvitations()
	{
		this.CreateBtnCommon(3, "key_guild_btn_invitations");
	}

	private void CreateBtnMembers()
	{
		this.CreateBtnCommon(2, "key_guild_btn_members");
	}

	private void CreateBtnOverview()
	{
		this.CreateBtnCommon(1, "key_guild_btn_overview");
	}

	private void CreateBtnRanks()
	{
		this.CreateBtnCommon(5, "key_guild_btn_ranks");
	}

	private void CreateBtnVault()
	{
		this.CreateBtnCommon(4, "key_guild_btn_vault");
	}

	private void CreateExpandGuildVaultDialog()
	{
		SlotPriceInfo slotPriceInfo = Enumerable.FirstOrDefault<SlotPriceInfo>(Enumerable.Where<SlotPriceInfo>(StaticData.slotPriceInformation, new Func<SlotPriceInfo, bool>(this, (SlotPriceInfo t) => (t.slotId != this.guild.vaultSlots + 4 ? false : t.slotType == "GuildVault"))));
		if (slotPriceInfo == null)
		{
			return;
		}
		this.expandGuildVaultWindow = new GuiWindow()
		{
			isModal = true
		};
		this.expandGuildVaultWindow.SetBackgroundTexture("ConfigWindow", "proba");
		this.expandGuildVaultWindow.isHidden = false;
		this.expandGuildVaultWindow.zOrder = 220;
		this.expandGuildVaultWindow.PutToCenter();
		AndromedaGui.gui.AddWindow(this.expandGuildVaultWindow);
		AndromedaGui.gui.activeToolTipId = this.expandGuildVaultWindow.handler;
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("ConfigWindow", "X_btn_");
		guiButtonFixed.X = 417f;
		guiButtonFixed.Y = -3f;
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, GuildWindow.CancelGuildVaultExpand);
		guiButtonFixed.SetLeftClickSound("FrameworkGUI", "cancel");
		this.expandGuildVaultWindow.AddGuiElement(guiButtonFixed);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(this.expandGuildVaultWindow.boundries.get_width() * 0.05f, 30f, this.expandGuildVaultWindow.boundries.get_width() * 0.9f, 60f),
			text = StaticData.Translate("key_guild_vault_expand_modal"),
			Alignment = 4,
			FontSize = 16
		};
		this.expandGuildVaultWindow.AddGuiElement(guiLabel);
		int num = 0;
		if (slotPriceInfo.priceEqulibrium > 0)
		{
			num++;
		}
		if (slotPriceInfo.priceNova > 0)
		{
			num++;
		}
		if (slotPriceInfo.priceUltralibrium > 0)
		{
			num++;
		}
		float _width = (this.expandGuildVaultWindow.boundries.get_width() - 60f - (float)(Math.Max(num - 1, 0) * 10)) / (float)num;
		float single = 30f;
		if (slotPriceInfo.priceEqulibrium > 0)
		{
			GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
			guiButtonResizeable.SetSmallBlueTexture();
			guiButtonResizeable.X = single;
			guiButtonResizeable.Y = 135f;
			guiButtonResizeable.Width = _width;
			guiButtonResizeable.isEnabled = this.guild.bankEquilib >= (long)slotPriceInfo.priceEqulibrium;
			guiButtonResizeable.MarginTop = -3;
			guiButtonResizeable.Alignment = 4;
			guiButtonResizeable.Caption = slotPriceInfo.priceEqulibrium.ToString("##,##0");
			guiButtonResizeable.eventHandlerParam.customData = (SelectedCurrency)2;
			guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, GuildWindow.ConfirmExpand);
			this.expandGuildVaultWindow.AddGuiElement(guiButtonResizeable);
			GuiTexture guiTexture = new GuiTexture();
			guiTexture.SetTexture("FrameworkGUI", "res_equilibrium");
			guiTexture.Y = guiButtonResizeable.Y + 2f;
			guiTexture.X = guiButtonResizeable.X + 10f;
			this.expandGuildVaultWindow.AddGuiElement(guiTexture);
			single = single + (_width + 10f);
		}
		if (slotPriceInfo.priceNova > 0)
		{
			GuiButtonResizeable str = new GuiButtonResizeable();
			str.SetSmallBlueTexture();
			str.X = single;
			str.Y = 135f;
			str.Width = _width;
			str.isEnabled = this.guild.bankNova >= (long)slotPriceInfo.priceNova;
			str.MarginTop = -3;
			str.Alignment = 4;
			str.Caption = slotPriceInfo.priceNova.ToString("##,##0");
			str.eventHandlerParam.customData = (SelectedCurrency)1;
			str.Clicked = new Action<EventHandlerParam>(this, GuildWindow.ConfirmExpand);
			this.expandGuildVaultWindow.AddGuiElement(str);
			GuiTexture y = new GuiTexture();
			y.SetTexture("FrameworkGUI", "res_nova");
			y.Y = str.Y + 2f;
			y.X = str.X + 10f;
			this.expandGuildVaultWindow.AddGuiElement(y);
			single = single + (_width + 10f);
		}
		if (slotPriceInfo.priceUltralibrium > 0)
		{
			GuiButtonResizeable action = new GuiButtonResizeable();
			action.SetSmallBlueTexture();
			action.X = single;
			action.Y = 135f;
			action.Width = _width;
			action.isEnabled = this.guild.bankUltralibrium >= (long)slotPriceInfo.priceUltralibrium;
			action.MarginTop = -3;
			action.Alignment = 4;
			action.Caption = slotPriceInfo.priceUltralibrium.ToString("##,##0");
			action.eventHandlerParam.customData = (SelectedCurrency)3;
			action.Clicked = new Action<EventHandlerParam>(this, GuildWindow.ConfirmExpand);
			this.expandGuildVaultWindow.AddGuiElement(action);
			GuiTexture x = new GuiTexture();
			x.SetTexture("FrameworkGUI", "res_ultralibrium");
			x.Y = action.Y + 2f;
			x.X = action.X + 10f;
			this.expandGuildVaultWindow.AddGuiElement(x);
		}
	}

	private void CreateGuildDetailsEditPanel()
	{
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(this.txTab.X + 20f + 10f, this.txTab.Y + 10f + 80f, 100f, 25f),
			Alignment = 0,
			text = StaticData.Translate("key_guild_name"),
			FontSize = 14,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		this.lblErrorName = new GuiLabel()
		{
			boundries = new Rect(guiLabel.boundries.get_x() + 100f, guiLabel.boundries.get_y() + 2f, 200f, 16f),
			TextColor = GuiNewStyleBar.redColor,
			text = string.Empty,
			FontSize = 10,
			Alignment = 0
		};
		base.AddGuiElement(this.lblErrorName);
		this.forDelete.Add(this.lblErrorName);
		this.tbGuildName = new GuiTextBox()
		{
			boundries = new Rect(240f, guiLabel.Y + 16f, 200f, 30f),
			Alignment = 0,
			FontSize = 14,
			TextColor = GuiNewStyleBar.blueColor,
			text = this.guild.name
		};
		base.AddGuiElement(this.tbGuildName);
		this.forDelete.Add(this.tbGuildName);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(guiLabel.X, guiLabel.Y + 60f, 100f, 25f),
			Alignment = 0,
			text = StaticData.Translate("key_guild_edit_tag"),
			FontSize = 14,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel1);
		this.forDelete.Add(guiLabel1);
		this.lblErrorTitle = new GuiLabel()
		{
			boundries = new Rect(guiLabel1.boundries.get_x() + 100f, guiLabel1.boundries.get_y() + 2f, 200f, 16f),
			TextColor = GuiNewStyleBar.redColor,
			text = string.Empty,
			FontSize = 10,
			Alignment = 0
		};
		base.AddGuiElement(this.lblErrorTitle);
		this.forDelete.Add(this.lblErrorTitle);
		this.tbGuildTitle = new GuiTextBox()
		{
			boundries = new Rect(240f, guiLabel1.Y + 16f, 200f, 30f),
			Alignment = 0,
			FontSize = 14,
			TextColor = GuiNewStyleBar.blueColor,
			text = this.guild.title
		};
		base.AddGuiElement(this.tbGuildTitle);
		this.forDelete.Add(this.tbGuildTitle);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(guiLabel.X, guiLabel1.Y + 60f, 150f, 30f),
			Alignment = 0,
			text = StaticData.Translate("key_guild_edit_description"),
			FontSize = 14,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel2);
		this.forDelete.Add(guiLabel2);
		this.lblErrorDescription = new GuiLabel()
		{
			boundries = new Rect(guiLabel2.boundries.get_x() + 100f, guiLabel2.boundries.get_y() + 2f, 200f, 16f),
			TextColor = GuiNewStyleBar.redColor,
			text = string.Empty,
			FontSize = 10,
			Alignment = 0
		};
		base.AddGuiElement(this.lblErrorDescription);
		this.forDelete.Add(this.lblErrorDescription);
		this.tbDescription = new GuiTextBox()
		{
			boundries = new Rect(238f, guiLabel2.Y + 16f, 323f, 126f)
		};
		this.tbDescription.SetFrameTexture("NewGUI", "GuildTA");
		this.tbDescription.Alignment = 0;
		this.tbDescription.FontSize = 12;
		this.tbDescription.TextColor = GuiNewStyleBar.blueColor;
		this.tbDescription.text = this.guild.description;
		this.tbDescription.isMultiLine = true;
		base.AddGuiElement(this.tbDescription);
		this.forDelete.Add(this.tbDescription);
	}

	private void CreateGuildVault()
	{
		foreach (GuiElement lockedGuildVaultSlot in this.lockedGuildVaultSlots)
		{
			base.RemoveGuiElement(lockedGuildVaultSlot);
		}
		this.lockedGuildVaultSlots.Clear();
		foreach (GuiElement lockedInventorySlot in this.lockedInventorySlots)
		{
			base.RemoveGuiElement(lockedInventorySlot);
		}
		this.lockedInventorySlots.Clear();
		Inventory.window = this;
		Inventory.isRightClickActionEnable = true;
		Inventory.isVaultMenuOpen = false;
		Inventory.isItemRerollMenuOpen = false;
		Inventory.isGuildVaultMenuOpen = true;
		Inventory.secondaryDropHandler = null;
		Inventory.closeStackablePopUp = new Action(this, GuildWindow.CreateGuildVault);
		Inventory.ConfigWndAfterRightClkAction = new Action(this, GuildWindow.CreateGuildVault);
		Inventory.ClearSlots(this);
		this.DrawGuildVault();
		this.DrawPlayerInventory();
		Inventory.DrawPlaces(this);
		foreach (AndromedaGuiDragDropPlace place in Inventory.places)
		{
			if (place.isEmpty || !place.item.isAccountBound)
			{
				if (place.isEmpty || place.location != 18 || StaticData.allTypes.get_Item(place.item.get_ItemType()).levelRestriction <= NetworkScript.player.playerBelongings.playerLevel)
				{
					continue;
				}
				place.frameTexture.SetTexture("GUI", "FrmInventoryDsb");
				ref Rect rectPointer = ref place.frameTexture.boundries;
				rectPointer.set_x(rectPointer.get_x() - 2f);
				ref Rect rectPointer1 = ref place.frameTexture.boundries;
				rectPointer1.set_y(rectPointer1.get_y() - 2f);
				if (place.txItem != null)
				{
					place.txItem.isUnavailable = true;
					place.txItem.RightClickAction = null;
					place.txItem.rightClickActionParam = null;
				}
			}
			else
			{
				place.frameTexture.SetTexture("GUI", "FrmInventoryDsb");
				ref Rect rectPointer2 = ref place.frameTexture.boundries;
				rectPointer2.set_x(rectPointer2.get_x() - 2f);
				ref Rect rectPointer3 = ref place.frameTexture.boundries;
				rectPointer3.set_y(rectPointer3.get_y() - 2f);
				if (place.txItem != null)
				{
					place.txItem.isUnavailable = true;
					place.txItem.RightClickAction = null;
					place.txItem.rightClickActionParam = null;
				}
			}
		}
	}

	private void CreateLoadingData()
	{
		GuiLabel guiLabel = new GuiLabel()
		{
			text = StaticData.Translate("key_guild_loading_data"),
			FontSize = 20,
			Alignment = 4,
			boundries = new Rect(0f, 0f, this.boundries.get_width(), this.boundries.get_height())
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
	}

	private void CreateMegaButton(string caption, float x, string iconName, Action<EventHandlerParam> onClick, bool isEnabled)
	{
		GuiButton guiButton = new GuiButton();
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButton.boundries = new Rect(x, this.txTab.Y + 55f, 90f, 25f);
		guiButton.Caption = StaticData.Translate(caption);
		guiButton.FontSize = 16;
		guiButton.Alignment = 0;
		guiButton.MarginTop = 0;
		guiButton.Clicked = onClick;
		guiButton.textColorHover = Color.get_white();
		guiButton.textColorNormal = GuiNewStyleBar.blueColor;
		guiButton.textColorDisabled = GuiNewStyleBar.blueColorDisable;
		guiButton.Hovered = new Action<object, bool>(this, GuildWindow.OnBtnInfoHover);
		GuildWindow.HoverPatcher hoverPatcher = new GuildWindow.HoverPatcher()
		{
			icon = guiButtonFixed,
			text = guiButton,
			iconName = iconName
		};
		guiButton.hoverParam = hoverPatcher;
		guiButton.isEnabled = isEnabled;
		if (!isEnabled)
		{
			guiButton.textColorDisabled = Color.get_gray();
		}
		base.AddGuiElement(guiButton);
		this.forDelete.Add(guiButton);
		guiButtonFixed.X = guiButton.X - 25f;
		guiButtonFixed.Y = guiButton.Y;
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.Clicked = onClick;
		guiButtonFixed.SetTexture("NewGUI", iconName);
		guiButtonFixed.Hovered = new Action<object, bool>(this, GuildWindow.OnBtnInfoHover);
		guiButtonFixed.hoverParam = guiButton.hoverParam;
		guiButtonFixed.isEnabled = isEnabled;
		base.AddGuiElement(guiButtonFixed);
		this.forDelete.Add(guiButtonFixed);
	}

	private void CreateMegaTexture(string assetName, GuiLabel anchor)
	{
		GuiTexture guiTexture = new GuiTexture()
		{
			X = anchor.X - 25f,
			Y = this.txTab.Y + 55f
		};
		guiTexture.SetTexture("NewGUI", string.Concat(assetName, "Clk"));
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
	}

	private void CreateMember()
	{
		GuiTexture guiTexture = new GuiTexture();
		Texture2D avatarOrStartIt = playWebGame.GetAvatarOrStartIt(this.guild.avatarUrl, new Action<AvatarJob>(null, GuildWindow.OnAvatarLoaded), guiTexture);
		if (avatarOrStartIt != null)
		{
			guiTexture.boundries = new Rect(61f, 91f, 100f, 100f);
			guiTexture.SetTexture2D(avatarOrStartIt);
			guiTexture.SetSize(100f, 100f);
		}
		else
		{
			guiTexture.SetTexture((Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "unknown"));
			guiTexture.boundries = new Rect(61f, 91f, 100f, 100f);
			guiTexture.SetSize(100f, 100f);
		}
		base.AddGuiElement(guiTexture);
		this.forDeleteFromLeftPanel.Add(guiTexture);
		this.txTab = new GuiTexture();
		this.txTab.SetTexture("NewGUI", string.Concat("GuildTab5_", this.selectedTab.ToString()));
		this.txTab.X = 205f;
		this.txTab.Y = 66f;
		base.AddGuiElement(this.txTab);
		this.forDeleteFromLeftPanel.Add(this.txTab);
		this.CreateMemberCommonPanel();
		this.lblGuildName.text = this.guild.name;
		if (GuildWindow.subSection == 1)
		{
			this.OnUpgradesBtnClicked(null);
			return;
		}
		switch (this.selectedTab)
		{
			case 1:
			{
				this.CreateMemberOverview();
				break;
			}
			case 2:
			{
				this.CreateMemberMembers();
				break;
			}
			case 3:
			{
				this.CreateMemberInvitations();
				break;
			}
			case 4:
			{
				this.CreateMemberVault();
				break;
			}
			case 5:
			{
				this.CreateMemberRanks();
				break;
			}
			default:
			{
				throw new Exception("Unsupported guild member tab index!");
			}
		}
	}

	private void CreateMemberCommonPanel()
	{
		this.CreateMemberCommonPanelItem(StaticData.Translate("key_guild_btn_members"), string.Concat(GuildWindow.membersCount.ToString(), " / ", Enumerable.First<GuildUpgrade>(Enumerable.Where<GuildUpgrade>(StaticData.guildUpgradesInfo, new Func<GuildUpgrade, bool>(this, (GuildUpgrade t) => (t.upgradeType != 1 ? false : t.upgradeLevel == this.guild.upgradeOneLevel)))).effectValue.ToString()), 245);
		string str = StaticData.Translate("key_guild_level");
		byte level = this.guild.get_Level();
		this.CreateMemberCommonPanelItem(str, level.ToString(), 210);
		GuiTexture guiTexture = new GuiTexture()
		{
			boundries = new Rect(34f, 304f, 25f, 27f)
		};
		guiTexture.SetTextureKeepSize("NewGUI", "ultralibrium_icon_big");
		base.AddGuiElement(guiTexture);
		this.forDeleteFromLeftPanel.Add(guiTexture);
		this.leftPanelLblUltralibrium = new GuiLabel()
		{
			boundries = new Rect(guiTexture.X, guiTexture.Y - 5f, 125f, 30f),
			Font = GuiLabel.FontBold,
			FontSize = 12,
			Alignment = 5,
			TextColor = GuiNewStyleBar.greenColor,
			text = this.guild.bankUltralibrium.ToString("##,##0")
		};
		base.AddGuiElement(this.leftPanelLblUltralibrium);
		this.forDeleteFromLeftPanel.Add(this.leftPanelLblUltralibrium);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.SetSmallBlueTexture();
		guiButtonResizeable.MarginTop = -2;
		guiButtonResizeable._marginLeft = -2;
		guiButtonResizeable.FontSize = 26;
		guiButtonResizeable.boundries = new Rect(guiTexture.X + 135f, guiTexture.Y, 30f, 25f);
		guiButtonResizeable.Caption = "+";
		guiButtonResizeable.Alignment = 4;
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnGoToBankBtnClicked);
		base.AddGuiElement(guiButtonResizeable);
		this.forDeleteFromLeftPanel.Add(guiButtonResizeable);
		GuiTexture x = new GuiTexture()
		{
			boundries = new Rect(34f, 340f, 26f, 28f)
		};
		x.SetTextureKeepSize("NewGUI", "equilibrium_icon_big");
		x.X = guiTexture.X;
		x.Y = guiTexture.Y + 35f;
		base.AddGuiElement(x);
		this.forDeleteFromLeftPanel.Add(x);
		this.leftPanelLblEquilibrium = new GuiLabel()
		{
			boundries = new Rect(x.X, x.Y - 5f, 125f, 30f),
			Font = GuiLabel.FontBold,
			FontSize = 12,
			Alignment = 5,
			TextColor = GuiNewStyleBar.purpleColor,
			text = this.guild.bankEquilib.ToString("##,##0")
		};
		base.AddGuiElement(this.leftPanelLblEquilibrium);
		this.forDeleteFromLeftPanel.Add(this.leftPanelLblEquilibrium);
		GuiButtonResizeable rect = new GuiButtonResizeable();
		rect.SetSmallBlueTexture();
		rect.MarginTop = -2;
		rect._marginLeft = -2;
		rect.FontSize = 26;
		rect.boundries = new Rect(guiTexture.X + 135f, x.Y, 30f, 25f);
		rect.Caption = "+";
		rect.Alignment = 4;
		rect.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnGoToBankBtnClicked);
		base.AddGuiElement(rect);
		this.forDeleteFromLeftPanel.Add(rect);
		GuiTexture y = new GuiTexture()
		{
			boundries = new Rect(33f, 379f, 28f, 20f)
		};
		y.SetTextureKeepSize("NewGUI", "nova_icon_big");
		y.X = guiTexture.X - 1f;
		y.Y = guiTexture.Y + 72f;
		base.AddGuiElement(y);
		this.forDeleteFromLeftPanel.Add(y);
		this.leftPanelLblNova = new GuiLabel()
		{
			boundries = new Rect(y.X, y.Y - 2f, 125f, 30f),
			Font = GuiLabel.FontBold,
			FontSize = 12,
			Alignment = 5,
			TextColor = GuiNewStyleBar.orangeColor,
			text = this.guild.bankNova.ToString("##,##0")
		};
		base.AddGuiElement(this.leftPanelLblNova);
		this.forDeleteFromLeftPanel.Add(this.leftPanelLblNova);
		GuiButtonResizeable action = new GuiButtonResizeable();
		action.SetSmallBlueTexture();
		action.MarginTop = -2;
		action._marginLeft = -2;
		action.FontSize = 26;
		action.boundries = new Rect(guiTexture.X + 135f, y.Y - 2f, 30f, 25f);
		action.Caption = "+";
		action.Alignment = 4;
		action.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnGoToBankBtnClicked);
		base.AddGuiElement(action);
		this.forDeleteFromLeftPanel.Add(action);
		GuiButtonResizeable guiButtonResizeable1 = new GuiButtonResizeable()
		{
			FontSize = 14,
			Caption = StaticData.Translate("key_guild_upgrade"),
			textColorNormal = Color.get_white(),
			Alignment = 4,
			Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnUpgradesBtnClicked),
			boundries = new Rect(30f, 415f, 170f, 25f)
		};
		guiButtonResizeable1.SetSmallBlueTexture();
		guiButtonResizeable1.MarginTop = -2;
		base.AddGuiElement(guiButtonResizeable1);
		this.forDelete.Add(guiButtonResizeable1);
		GuiButtonResizeable rect1 = new GuiButtonResizeable()
		{
			FontSize = 14,
			Caption = StaticData.Translate("key_guild_btn_transformer"),
			textColorNormal = Color.get_white(),
			Alignment = 4,
			Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnTransformerBtnClicked)
		};
		rect1.SetSmallBlueTexture();
		rect1.boundries = new Rect(30f, 452f, 170f, 25f);
		rect1.MarginTop = -2;
		base.AddGuiElement(rect1);
		this.forDelete.Add(rect1);
		GuiButtonResizeable guiButtonResizeable2 = new GuiButtonResizeable()
		{
			FontSize = 14,
			Caption = StaticData.Translate("key_guild_leave"),
			textColorNormal = Color.get_white(),
			Alignment = 4,
			Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnLeaveClicked),
			boundries = new Rect(30f, 490f, 170f, 25f)
		};
		guiButtonResizeable2.SetTexture("NewGUI", "guildBtnRed");
		guiButtonResizeable2.MarginTop = -2;
		base.AddGuiElement(guiButtonResizeable2);
		this.forDelete.Add(guiButtonResizeable2);
		this.lblErrorLeave = new GuiLabel()
		{
			boundries = new Rect(guiButtonResizeable2.X, guiButtonResizeable2.Y - 25f, guiButtonResizeable2.boundries.get_width(), 25f),
			Alignment = 7,
			TextColor = GuiNewStyleBar.redColor,
			text = string.Empty,
			FontSize = 10
		};
		this.lblErrorLeave.Alignment = 1;
		base.AddGuiElement(this.lblErrorLeave);
		this.forDelete.Add(this.lblErrorLeave);
	}

	private void CreateMemberCommonPanelItem(string label, string val, int y)
	{
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(30f, (float)y, 170f, 30f),
			Alignment = 3,
			text = label,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel);
		this.forDeleteFromLeftPanel.Add(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = guiLabel.boundries,
			Alignment = 5,
			text = val,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = Color.get_white()
		};
		base.AddGuiElement(guiLabel1);
		this.forDeleteFromLeftPanel.Add(guiLabel1);
		GuiTexture guiTexture = new GuiTexture()
		{
			X = guiLabel.X,
			Y = guiLabel.Y + 30f
		};
		guiTexture.SetTexture("NewGUI", "blueDot");
		guiTexture.boundries.set_height(1f);
		guiTexture.boundries.set_width(170f);
		base.AddGuiElement(guiTexture);
		this.forDeleteFromLeftPanel.Add(guiTexture);
	}

	private static void CreateMemberCommonPanelItemPreview(string label, string val, int y)
	{
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(20f, (float)y, 110f, 25f),
			Alignment = 0,
			text = label,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		GuildWindow.wndPreview.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(guiLabel.X + 60f, guiLabel.Y, 90f, 25f),
			Alignment = 2,
			text = val,
			FontSize = 14,
			TextColor = Color.get_white()
		};
		GuildWindow.wndPreview.AddGuiElement(guiLabel1);
		GuiTexture guiTexture = new GuiTexture()
		{
			X = guiLabel.X,
			Y = guiLabel.Y + 22f
		};
		guiTexture.SetTexture("NewGUI", "GuildInfoSeparator");
		guiTexture.boundries.set_height(1f);
		guiTexture.boundries.set_width(149f);
		GuildWindow.wndPreview.AddGuiElement(guiTexture);
	}

	private void CreateMemberEditInfo()
	{
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(this.txTab.X + 180f, this.txTab.Y + 55f, 120f, 25f),
			Alignment = 0,
			text = StaticData.Translate("key_guild_edit"),
			FontSize = 16,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor
		};
		guiLabel.Alignment = 0;
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		this.CreateMegaTexture("btnEdit", guiLabel);
		this.CreateMemberOverviewSubButtons();
		this.CreateMegaButton("key_guild_show_log", this.txTab.X + 300f, "btnLog", new Action<EventHandlerParam>(this, GuildWindow.OnBtnLogClicked), true);
		this.CreateMegaButton("key_guild_info", this.txTab.X + 60f, "btnInfo", new Action<EventHandlerParam>(this, GuildWindow.OnBtnInfoClicked), true);
		this.CreateGuildDetailsEditPanel();
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.boundries.set_x(245f);
		guiButtonResizeable.boundries.set_y(this.tbDescription.Y + 155f);
		guiButtonResizeable.boundries.set_width(148f);
		guiButtonResizeable.Caption = StaticData.Translate("key_guild_save");
		guiButtonResizeable.FontSize = 12;
		guiButtonResizeable.Alignment = 1;
		guiButtonResizeable.MarginTop = 5;
		guiButtonResizeable.SetSmallBlueTexture();
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnBtnSave);
		base.AddGuiElement(guiButtonResizeable);
		this.forDelete.Add(guiButtonResizeable);
	}

	private void CreateMemberInvitations()
	{
		bool flag = NetworkScript.player.guildMember.rank.isMaster | NetworkScript.player.guildMember.rank.canInvite;
		this.txOrangeLine = new GuiTexture()
		{
			X = this.txTab.X + 20f,
			Y = this.txTab.Y + 415f
		};
		this.txOrangeLine.SetTexture("NewGUI", "GuildInfoSeparator");
		this.txOrangeLine.boundries.set_height(1f);
		this.txOrangeLine.boundries.set_width(630f);
		base.AddGuiElement(this.txOrangeLine);
		this.forDelete.Add(this.txOrangeLine);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(this.txTab.X + 20f + 132f + 132f, this.txTab.Y + 6f, 120f, 25f),
			Alignment = 1,
			text = StaticData.Translate("key_guild_btn_invitations"),
			FontSize = 16,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor
		};
		guiLabel.Alignment = 1;
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		this.CreateBtnOverview();
		this.CreateBtnMembers();
		this.CreateBtnVault();
		this.CreateBtnRanks();
		GuiScrollingContainer guiScrollingContainer = new GuiScrollingContainer(210f, this.txTab.Y + 48f, 635f, 365f, 27, this);
		guiScrollingContainer.SetArrowStep(20f);
		base.AddGuiElement(guiScrollingContainer);
		this.forDelete.Add(guiScrollingContainer);
		int num = 0;
		for (int i = 0; i < this.guild.invitations.get_Count(); i++)
		{
			GuildInvitation item = this.guild.invitations.get_Values().get_Item(i);
			PlayerBasic playerBasic = item.invitee;
			GuiTexture guiTexture = new GuiTexture()
			{
				X = (float)(10 + 300 * (i % 2)),
				Y = (float)num
			};
			guiTexture.SetTexture("NewGUI", "playerListFrame");
			guiScrollingContainer.AddContent(guiTexture);
			GuiTexture guiTexture1 = new GuiTexture()
			{
				boundries = new Rect(guiTexture.X + 3f, guiTexture.Y + 3f, 49f, 49f)
			};
			Texture2D avatarOrStartIt = playWebGame.GetAvatarOrStartIt(playerBasic.avatarUrl, new Action<AvatarJob>(this, GuildWindow.OnAvatarLoadedInvite), guiTexture1);
			if (avatarOrStartIt == null)
			{
				guiTexture1.SetTextureKeepSize("FrameworkGUI", "unknown");
				avatarOrStartIt = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "unknown");
			}
			guiTexture1.SetTextureKeepSize(avatarOrStartIt);
			guiScrollingContainer.AddContent(guiTexture1);
			GuiTexture x = new GuiTexture();
			x.SetTexture("FrameworkGUI", string.Format("fraction{0}Icon", playerBasic.fractionId));
			x.X = guiTexture.X + 59f;
			x.Y = guiTexture.Y + 3f;
			guiScrollingContainer.AddContent(x);
			GuiLabel guiLabel1 = new GuiLabel()
			{
				boundries = new Rect(87f + guiTexture.X, 3f + guiTexture.Y, 175f, 20f),
				text = string.Format("{0} ({1})", playerBasic.nickName, playerBasic.level),
				FontSize = 12,
				Font = GuiLabel.FontBold
			};
			guiScrollingContainer.AddContent(guiLabel1);
			GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
			guiButtonFixed.SetTexture("NewGUI", "option_ViewProfile");
			guiButtonFixed.Caption = string.Empty;
			guiButtonFixed.X = guiTexture.X + 61f;
			guiButtonFixed.Y = guiTexture.Y + 33f;
			EventHandlerParam eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_profile_screen_tooltips_view_profile"),
				customData2 = guiButtonFixed
			};
			guiButtonFixed.tooltipWindowParam = eventHandlerParam;
			guiButtonFixed.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			eventHandlerParam = new EventHandlerParam()
			{
				customData = playerBasic.nickName
			};
			guiButtonFixed.eventHandlerParam = eventHandlerParam;
			guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, GuildWindow.ViewPlayerProfile);
			guiScrollingContainer.AddContent(guiButtonFixed);
			if (flag)
			{
				GuiButtonFixed empty = new GuiButtonFixed();
				empty.SetTexture("NewGUI", "person_close");
				empty.Caption = string.Empty;
				empty.X = guiTexture.X + 263f;
				empty.Y = guiTexture.Y + 6f;
				eventHandlerParam = new EventHandlerParam()
				{
					customData = item.id
				};
				empty.eventHandlerParam = eventHandlerParam;
				empty.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnInvitationRemove);
				guiScrollingContainer.AddContent(empty);
			}
			num = num + 75 * (i % 2);
		}
		if (flag)
		{
			this.tbInvite = new GuiTextBox()
			{
				boundries = new Rect(this.txOrangeLine.X, this.txOrangeLine.Y + 7f, 150f, 25f)
			};
			this.tbInvite.SetFrameTexture("NewGUI", "guildTB");
			this.tbInvite.FontSize = 14;
			this.tbInvite.TextColor = GuiNewStyleBar.blueColor;
			base.AddGuiElement(this.tbInvite);
			this.forDelete.Add(this.tbInvite);
			GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
			guiButtonResizeable.SetSmallBlueTexture();
			guiButtonResizeable.Y = this.tbInvite.Y + 6f;
			guiButtonResizeable.X = this.tbInvite.boundries.get_xMax() + 12f;
			guiButtonResizeable.Width = 120f;
			guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnInvite);
			guiButtonResizeable.Caption = StaticData.Translate("key_guild_invite");
			guiButtonResizeable.Alignment = 4;
			base.AddGuiElement(guiButtonResizeable);
			this.forDelete.Add(guiButtonResizeable);
			this.lblErrorInvitations = new GuiLabel()
			{
				boundries = new Rect(guiButtonResizeable.X + 140f, guiButtonResizeable.Y, 180f, 16f),
				TextColor = GuiNewStyleBar.redColor,
				text = string.Empty,
				FontSize = 10,
				Alignment = 0
			};
			base.AddGuiElement(this.lblErrorInvitations);
			this.forDelete.Add(this.lblErrorInvitations);
		}
	}

	private void CreateMemberLog()
	{
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(this.txTab.X + 300f, this.txTab.Y + 55f, 120f, 25f),
			Alignment = 0,
			text = StaticData.Translate("key_guild_show_log"),
			FontSize = 16,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor
		};
		guiLabel.Alignment = 0;
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		this.CreateMegaTexture("btnLog", guiLabel);
		this.CreateMemberOverviewSubButtons();
		this.CreateMegaButton("key_guild_info", this.txTab.X + 60f, "btnInfo", new Action<EventHandlerParam>(this, GuildWindow.OnBtnInfoClicked), true);
		this.CreateMegaButton("key_guild_edit", this.txTab.X + 180f, "btnEdit", new Action<EventHandlerParam>(this, GuildWindow.OnBtnEditClicked), NetworkScript.player.guildMember.rank.isMaster | NetworkScript.player.guildMember.rank.canEditDetails);
		this.aScroller = new GuiScrollingContainer(this.txOrangeLine.X + 10f, this.txOrangeLine.Y + 25f, 320f, 340f, 7, this);
		this.aScroller.SetArrowStep(16f);
		base.AddGuiElement(this.aScroller);
		this.forDelete.Add(this.aScroller);
		int num = 0;
		List<GuildLogRecord> list = this.guild.log;
		if (GuildWindow.<>f__am$cache40 == null)
		{
			GuildWindow.<>f__am$cache40 = new Func<GuildLogRecord, DateTime>(null, (GuildLogRecord o) => o.eventTime);
		}
		IEnumerator<GuildLogRecord> enumerator = Enumerable.OrderByDescending<GuildLogRecord, DateTime>(list, GuildWindow.<>f__am$cache40).GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				GuildLogRecord current = enumerator.get_Current();
				GuiLabel guiLabel1 = new GuiLabel()
				{
					boundries = new Rect(0f, (float)num, 90f, 16f),
					FontSize = 11,
					Alignment = 0,
					text = current.eventTime.ToString("dd/MM/yy")
				};
				this.aScroller.AddContent(guiLabel1);
				GuiLabel drawTooltipWindow = new GuiLabel()
				{
					isHoverAware = true,
					boundries = new Rect(55f, (float)num, 240f, 16f),
					Clipping = 1,
					TextColor = GuiNewStyleBar.blueColor,
					FontSize = 11,
					Alignment = 0
				};
				string log = current.get_Log();
				drawTooltipWindow.text = log;
				if (log.get_Length() <= 40)
				{
					drawTooltipWindow.drawTooltipWindowCallback = null;
				}
				else
				{
					EventHandlerParam eventHandlerParam = new EventHandlerParam()
					{
						customData = log,
						customData2 = drawTooltipWindow
					};
					drawTooltipWindow.tooltipWindowParam = eventHandlerParam;
					drawTooltipWindow.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
					drawTooltipWindow.text = string.Concat(log.Substring(0, 40), "...");
				}
				this.aScroller.AddContent(drawTooltipWindow);
				num = num + 16;
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

	private void CreateMemberMembers()
	{
		float scrollTumbCenter = 0f;
		if (this.membersScroller != null)
		{
			scrollTumbCenter = this.membersScroller.ScrollTumbCenter;
		}
		this.hoveredItemIndex = -1;
		this.editingMemberIndex = -1;
		this.members2edit = new GuildWindow.MemberListItem[100];
		this.customOnGUIAction = new Action(this, GuildWindow.OnDrawMembers);
		this.txOrangeLine = new GuiTexture()
		{
			X = this.txTab.X + 20f,
			Y = this.txTab.Y + 65f
		};
		this.txOrangeLine.SetTexture("NewGUI", "pvp_ranking_spacer_header");
		this.txOrangeLine.boundries.set_height(1f);
		this.txOrangeLine.boundries.set_width(630f);
		base.AddGuiElement(this.txOrangeLine);
		this.forDelete.Add(this.txOrangeLine);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(this.txTab.X + 20f + 132f, this.txTab.Y + 6f, 120f, 25f),
			Alignment = 1,
			text = StaticData.Translate("key_guild_btn_members"),
			FontSize = 16,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor
		};
		guiLabel.Alignment = 1;
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		this.CreateBtnOverview();
		this.CreateBtnInvitations();
		this.CreateBtnVault();
		this.CreateBtnRanks();
		this.PutOrangeHeadLabel("#", this.txTab.X + 20f);
		this.PutOrangeHeadLabel(StaticData.Translate("key_username"), this.txTab.X + 50f);
		this.PutOrangeHeadLabel(StaticData.Translate("key_guild_rank2"), this.txTab.X + 240f);
		this.PutOrangeHeadLabel(StaticData.Translate("key_guild_join_date"), this.txTab.X + 390f);
		this.PutOrangeHeadLabel(StaticData.Translate("key_guild_options"), this.txTab.X + 510f);
		this.membersScroller = new GuiScrollingContainer(this.txOrangeLine.X, this.txOrangeLine.Y + 5f, 655f, 390f, 27, this)
		{
			clippingBoundariesId = 107
		};
		this.membersScroller.SetArrowStep(20f);
		base.AddGuiElement(this.membersScroller);
		this.forDelete.Add(this.membersScroller);
		this.members2edit = new GuildWindow.MemberListItem[this.guild.members.get_Count()];
		int num = 0;
		int num1 = 0;
		while (num1 < this.guild.members.get_Count())
		{
			PlayerBasic item = this.guild.members.get_Values().get_Item(num1);
			GuildMember guildMember = item.guildMembership;
			if (guildMember != null)
			{
				bool flag = NetworkScript.player.guildMember.rank.isMaster | NetworkScript.player.guildMember.rank.canPromote & !guildMember.rank.isMaster;
				GuiLabel guiLabel1 = new GuiLabel()
				{
					boundries = new Rect(5f, (float)num, 80f, 25f),
					text = string.Format("{0}", num1 + 1),
					FontSize = 12,
					Font = GuiLabel.FontBold,
					TextColor = GuiNewStyleBar.blueColor,
					Alignment = 3
				};
				this.membersScroller.AddContent(guiLabel1);
				GuiButton guiButton = new GuiButton()
				{
					Caption = string.Format("{0} ({1})", item.nickName, item.level),
					boundries = new Rect(25f, (float)(num - 2), 160f, 30f),
					FontSize = 12,
					textColorNormal = GuiNewStyleBar.orangeColor,
					textColorHover = Color.get_white(),
					Alignment = 3
				};
				EventHandlerParam eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_profile_screen_tooltips_view_profile"),
					customData2 = guiButton
				};
				guiButton.tooltipWindowParam = eventHandlerParam;
				guiButton.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				eventHandlerParam = new EventHandlerParam()
				{
					customData = item.nickName
				};
				guiButton.eventHandlerParam = eventHandlerParam;
				guiButton.Clicked = new Action<EventHandlerParam>(this, GuildWindow.ViewPlayerProfile);
				this.membersScroller.AddContent(guiButton);
				GuiLabel guiLabel2 = new GuiLabel()
				{
					boundries = new Rect(370f, (float)num, 90f, 30f),
					text = string.Format("{0:dd/MM/yy HH:mm}", guildMember.memberSince),
					FontSize = 12,
					TextColor = Color.get_white(),
					Alignment = 3
				};
				this.membersScroller.AddContent(guiLabel2);
				GuiLabel guiLabel3 = new GuiLabel()
				{
					boundries = new Rect(220f, (float)num, 260f, 30f),
					text = guildMember.rank.name,
					FontSize = 12,
					Font = GuiLabel.FontBold,
					TextColor = Color.get_white(),
					Alignment = 3
				};
				this.membersScroller.AddContent(guiLabel3);
				GuiButtonFixed guiButtonFixed = null;
				if (flag)
				{
					guiButtonFixed = new GuiButtonFixed();
					guiButtonFixed.SetTexture("NewGUI", "btnEdit");
					guiButtonFixed.X = guiLabel3.X - 25f;
					guiButtonFixed.Y = (float)(num + 4);
					guiButtonFixed.Caption = string.Empty;
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_guild_tooltip_change_rank"),
						customData2 = guiButtonFixed
					};
					guiButtonFixed.tooltipWindowParam = eventHandlerParam;
					guiButtonFixed.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
					guiButtonFixed.isEnabled = true;
					guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnEditMemberRankClick);
					eventHandlerParam = new EventHandlerParam()
					{
						customData = num1
					};
					guiButtonFixed.eventHandlerParam = eventHandlerParam;
				}
				GuiDropdown guiDropdown = new GuiDropdown()
				{
					X = guiLabel3.X,
					Y = (float)num,
					Width = 140f
				};
				GuildRank[] orderedRanks = this.OrderedRanks;
				for (int i = 0; i < (int)orderedRanks.Length; i++)
				{
					GuildRank guildRank = orderedRanks[i];
					bool flag1 = false;
					if (guildRank.id == item.guildMembership.rank.id)
					{
						flag1 = true;
					}
					guiDropdown.AddItem(guildRank.name, flag1);
				}
				guiDropdown.OnItemSelected = new Action<int>(this, GuildWindow.OnMemberRankChanged);
				GuiTexture guiTexture = new GuiTexture()
				{
					boundries = new Rect(483f, (float)(num + 6), 24f, 24f)
				};
				if (!item.isOnline)
				{
					guiTexture.SetTexture("NewGUI", "option_StatusOffline");
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_profile_screen_tooltips_offline"),
						customData2 = guiTexture
					};
					guiTexture.tooltipWindowParam = eventHandlerParam;
				}
				else
				{
					guiTexture.SetTexture("NewGUI", "option_StatusOnline");
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_profile_screen_tooltips_online"),
						customData2 = guiTexture
					};
					guiTexture.tooltipWindowParam = eventHandlerParam;
				}
				guiTexture.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				this.membersScroller.AddContent(guiTexture);
				GuiButtonFixed empty = new GuiButtonFixed();
				empty.SetTexture("NewGUI", "option_ViewProfile");
				empty.Caption = string.Empty;
				empty.X = guiTexture.X + 32f;
				empty.Y = (float)(num + 6);
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_profile_screen_tooltips_view_profile"),
					customData2 = empty
				};
				empty.tooltipWindowParam = eventHandlerParam;
				empty.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				eventHandlerParam = new EventHandlerParam()
				{
					customData = item.nickName
				};
				empty.eventHandlerParam = eventHandlerParam;
				empty.Clicked = new Action<EventHandlerParam>(this, GuildWindow.ViewPlayerProfile);
				this.membersScroller.AddContent(empty);
				GuiButtonFixed x = new GuiButtonFixed();
				x.SetTexture("NewGUI", "startChat");
				x.Caption = string.Empty;
				x.X = empty.X + 32f;
				x.Y = empty.Y;
				eventHandlerParam = new EventHandlerParam()
				{
					customData = item.nickName
				};
				x.eventHandlerParam = eventHandlerParam;
				x.Clicked = new Action<EventHandlerParam>(this, GuildWindow.StartChatWith);
				this.membersScroller.AddContent(x);
				if (!item.isOnline || !(item.nickName != NetworkScript.player.playerBelongings.playerName))
				{
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_profile_screen_tooltips_no_chat"),
						customData2 = x
					};
					x.tooltipWindowParam = eventHandlerParam;
					x.isEnabled = false;
				}
				else
				{
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_profile_screen_tooltips_chat"),
						customData2 = x
					};
					x.tooltipWindowParam = eventHandlerParam;
					x.isEnabled = true;
				}
				x.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				GuiButtonFixed y = new GuiButtonFixed();
				y.SetTexture("NewGUI", "button_party");
				y.X = x.X + 32f;
				y.Y = x.Y + 1f;
				y.Caption = string.Empty;
				y.isEnabled = false;
				this.membersScroller.AddContent(y);
				if (!this.CanInviteToParty(item))
				{
					y.isEnabled = false;
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_profile_screen_tooltips_no_invite"),
						customData2 = y
					};
					y.tooltipWindowParam = eventHandlerParam;
				}
				else
				{
					y.isEnabled = true;
					eventHandlerParam = new EventHandlerParam()
					{
						customData = item.nickName
					};
					y.eventHandlerParam = eventHandlerParam;
					y.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnInviteToParty);
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_profile_screen_tooltips_invite"),
						customData2 = y
					};
					y.tooltipWindowParam = eventHandlerParam;
				}
				y.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				GuiButtonFixed drawTooltipWindow = null;
				if (flag)
				{
					drawTooltipWindow = new GuiButtonFixed();
					drawTooltipWindow.SetTexture("NewGUI", "btnDel");
					drawTooltipWindow.X = y.X + 30f;
					drawTooltipWindow.Y = x.Y + 1f;
					drawTooltipWindow.Caption = string.Empty;
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_guild_tooltip_kick"),
						customData2 = drawTooltipWindow
					};
					drawTooltipWindow.tooltipWindowParam = eventHandlerParam;
					drawTooltipWindow.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
					drawTooltipWindow.isEnabled = true;
					drawTooltipWindow.Clicked = new Action<EventHandlerParam>(this, GuildWindow.ShowRemoveMemberConfirmation);
					eventHandlerParam = new EventHandlerParam()
					{
						customData = item
					};
					drawTooltipWindow.eventHandlerParam = eventHandlerParam;
				}
				GuiTexture guiTexture1 = new GuiTexture()
				{
					X = 0f,
					Y = (float)(num + 30)
				};
				guiTexture1.SetTexture("NewGUI", "GuildInfoSeparator");
				guiTexture1.boundries.set_height(1f);
				guiTexture1.boundries.set_width(630f);
				this.membersScroller.AddContent(guiTexture1);
				if (flag)
				{
					GuildWindow.MemberListItem[] memberListItemArray = this.members2edit;
					GuildWindow.MemberListItem memberListItem = new GuildWindow.MemberListItem()
					{
						lblRank = guiLabel3,
						btnEdit = guiButtonFixed,
						ddlRank = guiDropdown,
						userName = item.userName,
						btnDelete = drawTooltipWindow,
						offsetY = (float)num
					};
					memberListItemArray[num1] = memberListItem;
				}
				num = num + 30;
				num1++;
			}
			else
			{
				break;
			}
		}
		if (scrollTumbCenter > 0f)
		{
			this.membersScroller.MooveToCenter(scrollTumbCenter);
		}
	}

	private void CreateMemberOverview()
	{
		this.txOrangeLine = new GuiTexture()
		{
			X = this.txTab.X + 20f,
			Y = this.txTab.Y + 80f
		};
		this.txOrangeLine.SetTexture("NewGUI", "GuildInfoSeparator");
		this.txOrangeLine.boundries.set_height(1f);
		base.AddGuiElement(this.txOrangeLine);
		this.forDelete.Add(this.txOrangeLine);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(this.txTab.X + 20f, this.txTab.Y + 6f, 120f, 25f),
			Alignment = 1,
			text = StaticData.Translate("key_guild_btn_overview"),
			FontSize = 16,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor
		};
		guiLabel.Alignment = 1;
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		this.CreateBtnMembers();
		this.CreateBtnInvitations();
		this.CreateBtnVault();
		this.CreateBtnRanks();
		this.CreateMemberOverviewSubButtons();
		switch (this.overviewTabIndex)
		{
			case 1:
			{
				this.CreateMemberOverviewInfo();
				break;
			}
			case 2:
			{
				this.CreateMemberEditInfo();
				break;
			}
			case 3:
			{
				this.CreateMemberLog();
				break;
			}
			default:
			{
				throw new Exception("Invalid overview tab index!");
			}
		}
		this.CreateBank();
	}

	private void CreateMemberOverviewInfo()
	{
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(this.txTab.X + 60f, this.txTab.Y + 55f, 120f, 25f),
			Alignment = 0,
			text = StaticData.Translate("key_guild_info"),
			FontSize = 16,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor
		};
		guiLabel.Alignment = 0;
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		this.CreateMegaTexture("btnInfo", guiLabel);
		this.CreateMegaButton("key_guild_edit", this.txTab.X + 180f, "btnEdit", new Action<EventHandlerParam>(this, GuildWindow.OnBtnEditClicked), NetworkScript.player.guildMember.rank.isMaster | NetworkScript.player.guildMember.rank.canEditDetails);
		this.CreateMegaButton("key_guild_show_log", this.txTab.X + 300f, "btnLog", new Action<EventHandlerParam>(this, GuildWindow.OnBtnLogClicked), true);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(this.txOrangeLine.X + 10f, this.txOrangeLine.Y + 10f, 100f, 25f),
			Alignment = 0,
			text = StaticData.Translate("key_guild_name"),
			FontSize = 14,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel1);
		this.forDelete.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(guiLabel1.X + 7f, guiLabel1.Y + 16f, 200f, 30f),
			Alignment = 0,
			FontSize = 13,
			TextColor = Color.get_white(),
			text = this.guild.name
		};
		base.AddGuiElement(guiLabel2);
		this.forDelete.Add(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(guiLabel1.X, guiLabel1.Y + 60f, 100f, 25f),
			Alignment = 0,
			text = StaticData.Translate("key_guild_edit_tag"),
			FontSize = 14,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel3);
		this.forDelete.Add(guiLabel3);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			boundries = new Rect(guiLabel1.X + 7f, guiLabel3.Y + 16f, 200f, 30f),
			Alignment = 0,
			FontSize = 13,
			TextColor = Color.get_white(),
			text = string.Concat("[", this.guild.title, "]")
		};
		base.AddGuiElement(guiLabel4);
		this.forDelete.Add(guiLabel4);
		GuiLabel guiLabel5 = new GuiLabel()
		{
			boundries = new Rect(guiLabel1.X, guiLabel3.Y + 60f, 150f, 30f),
			Alignment = 0,
			text = StaticData.Translate("key_guild_edit_description"),
			FontSize = 14,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel5);
		this.forDelete.Add(guiLabel5);
		GuiLabel guiLabel6 = new GuiLabel()
		{
			boundries = new Rect(guiLabel1.X + 5f, guiLabel5.Y + 16f, 323f, 126f),
			Alignment = 0,
			FontSize = 13,
			TextColor = Color.get_white(),
			text = this.guild.description
		};
		base.AddGuiElement(guiLabel6);
		this.forDelete.Add(guiLabel6);
	}

	private void CreateMemberOverviewSubButtons()
	{
	}

	private void CreateMemberRanks()
	{
		// 
		// Current member / type: System.Void GuildWindow::CreateMemberRanks()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void CreateMemberRanks()
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
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 431
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 71
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

	private void CreateMemberVault()
	{
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(this.txTab.X + 20f + 132f + 132f + 132f, this.txTab.Y + 6f, 120f, 25f),
			Alignment = 1,
			text = StaticData.Translate("key_guild_btn_vault"),
			FontSize = 16,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor
		};
		guiLabel.Alignment = 1;
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("NewGUI", "blueDot");
		guiTexture.boundries = new Rect(212f, 313f, 667f, 1f);
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("ConfigWnd", "vaultIconGuild");
		rect.boundries = new Rect(212f, 161f, 93f, 93f);
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_tooltip_guild_vault"),
			customData2 = rect
		};
		rect.tooltipWindowParam = eventHandlerParam;
		rect.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(rect);
		this.forDelete.Add(rect);
		GuiTexture drawTooltipWindow = new GuiTexture();
		drawTooltipWindow.SetTexture("ConfigWnd", "vaultIconShip");
		drawTooltipWindow.boundries = new Rect(212f, 380f, 93f, 93f);
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_tooltip_inventory"),
			customData2 = drawTooltipWindow
		};
		drawTooltipWindow.tooltipWindowParam = eventHandlerParam;
		drawTooltipWindow.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(drawTooltipWindow);
		this.forDelete.Add(drawTooltipWindow);
		this.CreateBtnOverview();
		this.CreateBtnMembers();
		this.CreateBtnInvitations();
		this.CreateBtnRanks();
		this.RequestVaultItems();
	}

	private void CreateNonMember()
	{
		// 
		// Current member / type: System.Void GuildWindow::CreateNonMember()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void CreateNonMember()
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

	private void CreateNonMemberTabCreate()
	{
		this.guild = new Guild();
		GuiButton guiButton = new GuiButton()
		{
			boundries = new Rect(this.txTab.X + 280f - 132f, this.txTab.Y + 8f - 7f - 26f, 120f, 70f),
			Caption = StaticData.Translate("key_guild_btn_invitations"),
			FontSize = 16,
			Alignment = 4,
			MarginTop = 5,
			Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnInvitationsClicked),
			textColorHover = Color.get_white(),
			textColorNormal = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiButton);
		this.forDelete.Add(guiButton);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(this.txTab.X + 40f - 20f, this.txTab.Y + 6f, 120f, 25f),
			Alignment = 0,
			text = StaticData.Translate("key_guild_btn_create"),
			FontSize = 16,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor
		};
		guiLabel.Alignment = 1;
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		this.guild.name = string.Empty;
		this.guild.title = string.Empty;
		this.guild.description = string.Empty;
		this.CreateGuildDetailsEditPanel();
		bool item = (long)Guild.guildUpgrades.get_Item(1).priceNova <= this.player.playerBelongings.playerItems.get_Nova();
		bool flag = (long)Guild.guildUpgrades.get_Item(1).priceEquilibrium <= this.player.playerBelongings.playerItems.GetAmountAt(PlayerItems.TypeEquilibrium);
		bool flag1 = NetworkScript.player.playerBelongings.playerLevel >= 9;
		if (Guild.guildUpgrades.get_Item(1).priceNova >= 0)
		{
			GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
			guiButtonResizeable.boundries.set_x(245f);
			guiButtonResizeable.boundries.set_y(this.tbDescription.Y + 155f);
			guiButtonResizeable.boundries.set_width(148f);
			guiButtonResizeable.Caption = StaticData.Translate("key_guild_btn_create");
			guiButtonResizeable.FontSize = 12;
			guiButtonResizeable.MarginTop = 5;
			guiButtonResizeable.Alignment = 1;
			guiButtonResizeable.SetSmallBlueTexture();
			guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnBtnCreateWithNovaClicked);
			guiButtonResizeable.isEnabled = item & flag1;
			base.AddGuiElement(guiButtonResizeable);
			this.forDelete.Add(guiButtonResizeable);
			GuiTexture guiTexture = new GuiTexture()
			{
				X = guiButtonResizeable.X + 45f,
				Y = guiButtonResizeable.Y + 30f
			};
			guiTexture.SetTexture("FrameworkGUI", "res_nova");
			base.AddGuiElement(guiTexture);
			this.forDelete.Add(guiTexture);
			GuiLabel guiLabel1 = new GuiLabel()
			{
				boundries = new Rect(guiTexture.X + 22f, guiTexture.Y + 2f, 100f, 30f),
				Alignment = 0,
				text = Guild.guildUpgrades.get_Item(1).priceNova.ToString("###,###,##0"),
				FontSize = 14,
				TextColor = (!item ? Color.get_red() : Color.get_white())
			};
			base.AddGuiElement(guiLabel1);
			this.forDelete.Add(guiLabel1);
		}
		if (Guild.guildUpgrades.get_Item(1).priceEquilibrium >= 0)
		{
			GuiButtonResizeable action = new GuiButtonResizeable();
			action.boundries.set_x(405f);
			action.boundries.set_y(this.tbDescription.Y + 155f);
			action.boundries.set_width(148f);
			action.Caption = StaticData.Translate("key_guild_btn_create");
			action.FontSize = 12;
			action.MarginTop = 5;
			action.Alignment = 1;
			action.SetSmallBlueTexture();
			action.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnBtnCreateWithEquilibClicked);
			action.isEnabled = flag & flag1;
			base.AddGuiElement(action);
			this.forDelete.Add(action);
			GuiTexture guiTexture1 = new GuiTexture()
			{
				X = action.X + 45f,
				Y = action.Y + 32f
			};
			guiTexture1.SetTexture("FrameworkGUI", "res_equilibrium");
			base.AddGuiElement(guiTexture1);
			this.forDelete.Add(guiTexture1);
			GuiLabel guiLabel2 = new GuiLabel()
			{
				boundries = new Rect(guiTexture1.X + 22f, guiTexture1.Y, 100f, 30f),
				Alignment = 0,
				text = Guild.guildUpgrades.get_Item(1).priceEquilibrium.ToString("###,###,##0"),
				FontSize = 14,
				TextColor = (!flag ? Color.get_red() : Color.get_white())
			};
			base.AddGuiElement(guiLabel2);
			this.forDelete.Add(guiLabel2);
		}
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(this.txTab.X + 400f, this.txTab.Y + 100f, 240f, 400f),
			Alignment = 0,
			text = StaticData.Translate("key_guild_create_explain"),
			FontSize = 13,
			TextColor = Color.get_white()
		};
		base.AddGuiElement(guiLabel3);
		this.forDelete.Add(guiLabel3);
	}

	private void CreateNonMemberTabInvitations()
	{
		GuiButton guiButton = new GuiButton()
		{
			boundries = new Rect(this.txTab.X + 40f - 20f, this.txTab.Y + 8f - 7f - 26f, 120f, 70f),
			Caption = StaticData.Translate("key_guild_btn_create"),
			FontSize = 16,
			Alignment = 4,
			MarginTop = 5,
			Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnBtnTabCreateClicked),
			textColorHover = Color.get_white(),
			textColorNormal = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiButton);
		this.forDelete.Add(guiButton);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(this.txTab.X + 148f, this.txTab.Y + 6f, 120f, 25f),
			Alignment = 1,
			text = StaticData.Translate("key_guild_btn_invitations"),
			FontSize = 16,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor
		};
		guiLabel.Alignment = 1;
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		if (this.guild == null || this.guild.invitations == null || this.guild.invitations.get_Count() < 1)
		{
			GuiLabel guiLabel1 = new GuiLabel()
			{
				text = StaticData.Translate("key_guild_no_invitations"),
				FontSize = 20,
				boundries = new Rect(this.txTab.boundries.get_x() + 10f, this.txTab.boundries.get_y() + 40f, this.txTab.boundries.get_width() - 20f, this.txTab.boundries.get_height() - 80f),
				Alignment = 4
			};
			base.AddGuiElement(guiLabel1);
			this.forDelete.Add(guiLabel1);
		}
		else
		{
			this.aScroller = new GuiScrollingContainer(210f, this.txTab.Y + 48f, 635f, 365f, 27, this);
			this.aScroller.SetArrowStep(20f);
			base.AddGuiElement(this.aScroller);
			this.forDelete.Add(this.aScroller);
			int num = 0;
			for (int i = 0; i < this.guild.invitations.get_Count(); i++)
			{
				GuildInvitation item = this.guild.invitations.get_Values().get_Item(i);
				Guild guild = item.guild;
				GuiTexture guiTexture = new GuiTexture()
				{
					boundries = new Rect(10f, (float)(num + 5), 49f, 49f)
				};
				Texture2D avatarOrStartIt = playWebGame.GetAvatarOrStartIt(item.guild.avatarUrl, new Action<AvatarJob>(this, GuildWindow.OnAvatarLoadedInvite), guiTexture);
				if (avatarOrStartIt == null)
				{
					guiTexture.SetTextureKeepSize("FrameworkGUI", "unknown");
					avatarOrStartIt = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "unknown");
				}
				guiTexture.SetTextureKeepSize(avatarOrStartIt);
				this.aScroller.AddContent(guiTexture);
				GuiTexture x = new GuiTexture();
				x.SetTexture("FrameworkGUI", string.Format("fraction{0}Icon", guild.fractionId));
				x.X = guiTexture.X + 59f;
				x.Y = guiTexture.Y + 18f;
				this.aScroller.AddContent(x);
				GuiLabel str = new GuiLabel()
				{
					boundries = new Rect(guiTexture.X + 87f + 165f, 19f + guiTexture.Y, 140f, 20f)
				};
				DateTime dateTime = item.timeExpire.AddDays(-1);
				str.text = dateTime.ToString("dd-MM-yyyy HH:mm");
				str.FontSize = 14;
				str.TextColor = GuiNewStyleBar.blueColor;
				str.Font = GuiLabel.FontBold;
				str.Alignment = 0;
				this.aScroller.AddContent(str);
				GuiButton action = new GuiButton()
				{
					boundries = new Rect(guiTexture.X + 87f, 15f + guiTexture.Y, 200f, 20f),
					Caption = string.Format("{0} [{1}]", guild.name, guild.get_Level()),
					FontSize = 16,
					Alignment = 0
				};
				EventHandlerParam eventHandlerParam = new EventHandlerParam()
				{
					customData = guild.id
				};
				action.eventHandlerParam = eventHandlerParam;
				action.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnGiuldPreviewClicked);
				action.textColorNormal = GuiNewStyleBar.blueColor;
				action.textColorHover = Color.get_white();
				this.aScroller.AddContent(action);
				GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable()
				{
					Y = (float)(num + 20)
				};
				guiButtonResizeable.SetSmallBlueTexture();
				guiButtonResizeable.X = 395f;
				guiButtonResizeable.Caption = StaticData.Translate("key_guild_accept");
				eventHandlerParam = new EventHandlerParam()
				{
					customData = item
				};
				guiButtonResizeable.eventHandlerParam = eventHandlerParam;
				guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnInviteAccept);
				guiButtonResizeable.Alignment = 4;
				this.aScroller.AddContent(guiButtonResizeable);
				GuiButtonResizeable guiButtonResizeable1 = new GuiButtonResizeable()
				{
					Y = (float)(num + 20)
				};
				guiButtonResizeable1.SetTexture("NewGUI", "guildBtnRed");
				guiButtonResizeable1.X = 510f;
				guiButtonResizeable1.Caption = StaticData.Translate("key_guild_reject");
				eventHandlerParam = new EventHandlerParam()
				{
					customData = item
				};
				guiButtonResizeable1.eventHandlerParam = eventHandlerParam;
				guiButtonResizeable1.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnInviteReject);
				guiButtonResizeable1.Alignment = 4;
				this.aScroller.AddContent(guiButtonResizeable1);
				GuiTexture guiTexture1 = new GuiTexture()
				{
					X = 10f,
					Y = (float)(num + 60)
				};
				guiTexture1.SetTexture("NewGUI", "GuildInfoSeparator");
				guiTexture1.boundries.set_height(1f);
				guiTexture1.boundries.set_width(600f);
				this.aScroller.AddContent(guiTexture1);
				num = num + 80;
			}
		}
	}

	public static void CreatePreviewWindow(Guild guild)
	{
		GuildWindow.<CreatePreviewWindow>c__AnonStorey1E variable = null;
		Debug.Log("CreatePreviewWindow");
		if (GuildWindow.wndPreview != null)
		{
			AndromedaGui.gui.RemoveWindow(GuildWindow.wndPreview.handler);
		}
		GuildWindow.wndPreview = new GuiWindow()
		{
			zOrder = 249,
			isModal = true
		};
		GuildWindow.wndPreview.SetBackgroundTexture("NewGUI", "preview-pop-up");
		GuildWindow.wndPreview.PutToCenter();
		GuiTexture guiTexture = new GuiTexture();
		Texture2D avatarOrStartIt = playWebGame.GetAvatarOrStartIt(guild.avatarUrl, new Action<AvatarJob>(null, GuildWindow.OnAvatarLoaded2), guiTexture);
		if (avatarOrStartIt != null)
		{
			guiTexture.boundries = new Rect(45f, 52f, 100f, 100f);
			guiTexture.SetTexture2D(avatarOrStartIt);
			guiTexture.SetSize(100f, 100f);
		}
		else
		{
			guiTexture.SetTexture((Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "unknown"));
			guiTexture.boundries = new Rect(45f, 52f, 100f, 100f);
			guiTexture.SetSize(100f, 100f);
		}
		GuildWindow.wndPreview.AddGuiElement(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("FrameworkGUI", string.Format("fraction{0}Icon", guild.fractionId));
		guiTexture1.X = 20f;
		guiTexture1.Y = 20f;
		GuildWindow.wndPreview.AddGuiElement(guiTexture1);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = string.Format("[{1}] {0}", guild.name, guild.title),
			FontSize = 20,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 0,
			boundries = new Rect(50f, 18f, 250f, 20f)
		};
		GuildWindow.wndPreview.AddGuiElement(guiLabel);
		string str = StaticData.Translate("key_guild_btn_members");
		int count = guild.members.get_Count();
		GuildWindow.CreateMemberCommonPanelItemPreview(str, string.Concat(count.ToString(), "/", Enumerable.First<GuildUpgrade>(Enumerable.Where<GuildUpgrade>(StaticData.guildUpgradesInfo, new Func<GuildUpgrade, bool>(variable, (GuildUpgrade t) => (t.upgradeType != 1 ? false : t.upgradeLevel == this.guild.upgradeOneLevel)))).effectValue.ToString()), 180);
		GuildWindow.CreateMemberCommonPanelItemPreview(StaticData.Translate("key_guild_founded"), guild.createdOn.ToString("dd-MM-yyyy"), 213);
		string str1 = StaticData.Translate("key_guild_level");
		byte level = guild.get_Level();
		GuildWindow.CreateMemberCommonPanelItemPreview(str1, level.ToString(), 246);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.boundries.set_width(130f);
		guiButtonResizeable.boundries.set_y(130f);
		guiButtonResizeable.boundries.set_x(GuildWindow.wndPreview.boundries.get_width() / 2f - guiButtonResizeable.boundries.get_width() / 2f);
		guiButtonResizeable.boundries.set_y(GuildWindow.wndPreview.boundries.get_height() - 35f);
		guiButtonResizeable.Caption = StaticData.Translate("key_dialog_ok");
		guiButtonResizeable.FontSize = 12;
		guiButtonResizeable.Alignment = 1;
		guiButtonResizeable.MarginTop = 5;
		guiButtonResizeable.SetSmallBlueTexture();
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(null, GuildWindow.OnPreviewClose);
		GuildWindow.wndPreview.AddGuiElement(guiButtonResizeable);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			text = StaticData.Translate("key_guild_info"),
			FontSize = 18,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 1,
			boundries = new Rect(270f, 18f, 370f, 20f)
		};
		GuildWindow.wndPreview.AddGuiElement(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			text = guild.description,
			FontSize = 14,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 0,
			boundries = new Rect(270f, 40f, 370f, 170f)
		};
		GuildWindow.wndPreview.AddGuiElement(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			text = StaticData.Translate("key_guild_btn_members").ToUpper(),
			FontSize = 18,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 1,
			boundries = new Rect(270f, 218f, 370f, 20f)
		};
		GuildWindow.wndPreview.AddGuiElement(guiLabel3);
		GuildWindow.PutOrangeHeadLabelPreview(StaticData.Translate("key_username"), 270f);
		GuildWindow.PutOrangeHeadLabelPreview(StaticData.Translate("key_guild_rank2"), 400f);
		GuildWindow.PutOrangeHeadLabelPreview(StaticData.Translate("key_guild_join_date"), 520f);
		GuiTexture guiTexture2 = new GuiTexture()
		{
			X = 270f,
			Y = 260f
		};
		guiTexture2.SetTexture("NewGUI", "pvp_ranking_spacer_header");
		guiTexture2.boundries.set_height(1f);
		guiTexture2.boundries.set_width(370f);
		GuildWindow.wndPreview.AddGuiElement(guiTexture2);
		GuiScrollingContainer guiScrollingContainer = new GuiScrollingContainer(270f, 265f, 400f, 162f, 117, GuildWindow.wndPreview);
		guiScrollingContainer.SetArrowStep(21f);
		GuildWindow.wndPreview.AddGuiElement(guiScrollingContainer);
		int num = 0;
		IEnumerator<PlayerBasic> enumerator = guild.members.get_Values().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				PlayerBasic current = enumerator.get_Current();
				GuiLabel guiLabel4 = new GuiLabel()
				{
					boundries = new Rect(0f, (float)num, 125f, 16f),
					TextColor = GuiNewStyleBar.blueColor,
					FontSize = 14,
					Alignment = 0,
					text = current.nickName
				};
				guiScrollingContainer.AddContent(guiLabel4);
				GuiLabel guiLabel5 = new GuiLabel()
				{
					boundries = new Rect(130f, (float)num, 105f, 16f),
					TextColor = GuiNewStyleBar.blueColor,
					FontSize = 14,
					Alignment = 0,
					text = current.guildMembership.rank.name
				};
				guiScrollingContainer.AddContent(guiLabel5);
				GuiLabel guiLabel6 = new GuiLabel()
				{
					boundries = new Rect(250f, (float)num, 145f, 16f),
					TextColor = GuiNewStyleBar.blueColor,
					FontSize = 14,
					Alignment = 0,
					text = current.guildMembership.memberSince.ToString("dd-MM-yyyy HH:mm")
				};
				guiScrollingContainer.AddContent(guiLabel6);
				num = num + 21;
			}
		}
		finally
		{
			if (enumerator == null)
			{
			}
			enumerator.Dispose();
		}
		GuildWindow.wndPreview.isHidden = false;
		AndromedaGui.gui.AddWindow(GuildWindow.wndPreview);
		AndromedaGui.gui.activeToolTipId = GuildWindow.wndPreview.handler;
	}

	private void DeleteAll()
	{
		foreach (GuiElement guiElement in this.forDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.forDelete.Clear();
		foreach (GuiElement guiElement1 in this.forDeleteFromLeftPanel)
		{
			base.RemoveGuiElement(guiElement1);
		}
		this.forDeleteFromLeftPanel.Clear();
		Inventory.ClearSlots(this);
	}

	private void DisplayRankErrors(ValidationErrors errors)
	{
		GuiLabel errorText = this.lblErrRankName;
		KeyValuePair<int, ErrorCode> item = errors.errors.get_Item(0);
		errorText.text = this.GetErrorText(item.get_Value());
	}

	public void DoGuildInsufficientMasters(ITransferable data, TransferContext ctx)
	{
		this.Create();
		this.lblErrorLeave.text = this.GetErrorText(5);
	}

	private void DrawGuildUpgrade(int index)
	{
		GuildWindow.<DrawGuildUpgrade>c__AnonStorey1F variable = null;
		byte num;
		bool flag = (NetworkScript.player.guildMember.rank.isMaster ? true : NetworkScript.player.guildMember.rank.canBank);
		bool flag1 = false;
		string str = "{0:##,##0}%";
		switch (index)
		{
			case 0:
			{
				num = this.guild.upgradeOneLevel;
				str = "{0:##,##0}";
				break;
			}
			case 1:
			{
				num = this.guild.upgradeTwoLevel;
				break;
			}
			case 2:
			{
				num = this.guild.upgradeThreeLevel;
				break;
			}
			case 3:
			{
				num = this.guild.upgradeFourLevel;
				break;
			}
			case 4:
			{
				num = this.guild.upgradeFiveLevel;
				break;
			}
			default:
			{
				num = 0;
				break;
			}
		}
		byte num1 = (byte)(num + 1);
		if (num1 > 10)
		{
			flag1 = true;
			num1 = 10;
		}
		GuildUpgrade guildUpgrade = Enumerable.FirstOrDefault<GuildUpgrade>(Enumerable.Where<GuildUpgrade>(StaticData.guildUpgradesInfo, new Func<GuildUpgrade, bool>(variable, (GuildUpgrade t) => (t.upgradeType != this.index + 1 ? false : t.upgradeLevel == this.upgradeLevel))));
		GuildUpgrade guildUpgrade1 = Enumerable.FirstOrDefault<GuildUpgrade>(Enumerable.Where<GuildUpgrade>(StaticData.guildUpgradesInfo, new Func<GuildUpgrade, bool>(variable, (GuildUpgrade t) => (t.upgradeType != this.index + 1 ? false : t.upgradeLevel == this.nextUpgradeLevel))));
		float single = 225f;
		float single1 = (float)(85 + index * 90);
		GuiTexture guiTexture = new GuiTexture()
		{
			boundries = new Rect(single + 45f, single1 - 3f, 64f, 64f)
		};
		guiTexture.SetTextureKeepSize("NewGUI", guildUpgrade1.assetName);
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		for (int i = 0; i < 10; i++)
		{
			GuiTexture guiTexture1 = new GuiTexture()
			{
				boundries = new Rect(single + 475f + (float)(i * 16), single1 - 2f, 17f, 16f)
			};
			if (i >= num)
			{
				guiTexture1.SetTextureKeepSize("NewGUI", "blankStarIcon");
			}
			else
			{
				guiTexture1.SetTextureKeepSize("NewGUI", "fillStarIcon");
			}
			base.AddGuiElement(guiTexture1);
			this.forDelete.Add(guiTexture1);
		}
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(single, single1 + 52f, 150f, 16f),
			Alignment = 1,
			FontSize = 13,
			Font = GuiLabel.FontBold,
			text = StaticData.Translate(guildUpgrade1.uiName)
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(single + 150f, single1, 310f, 16f),
			FontSize = 12,
			Clipping = 1,
			text = StaticData.Translate(guildUpgrade1.description)
		};
		base.AddGuiElement(guiLabel1);
		this.forDelete.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(single + 150f, single1 + 15f, 150f, 16f),
			FontSize = 12,
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor,
			text = StaticData.Translate("ep_screen_fortify_current")
		};
		base.AddGuiElement(guiLabel2);
		this.forDelete.Add(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(single + 150f, single1 + 15f, 150f, 16f),
			FontSize = 12,
			Alignment = 5,
			Font = GuiLabel.FontBold,
			text = (guildUpgrade == null ? string.Format(str, 0) : string.Format(str, guildUpgrade.effectValue))
		};
		base.AddGuiElement(guiLabel3);
		this.forDelete.Add(guiLabel3);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			boundries = new Rect(single + 310f, single1 + 15f, 150f, 16f),
			FontSize = 12,
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor,
			text = StaticData.Translate("ep_screen_fortify_next_level")
		};
		base.AddGuiElement(guiLabel4);
		this.forDelete.Add(guiLabel4);
		GuiLabel guiLabel5 = new GuiLabel()
		{
			boundries = new Rect(single + 310f, single1 + 15f, 150f, 16f),
			FontSize = 12,
			Alignment = 5,
			TextColor = GuiNewStyleBar.orangeColor,
			Font = GuiLabel.FontBold,
			text = string.Format(str, guildUpgrade1.effectValue)
		};
		base.AddGuiElement(guiLabel5);
		this.forDelete.Add(guiLabel5);
		if (flag1)
		{
			return;
		}
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.SetSmallBlueTexture();
		guiButtonResizeable.FontSize = 14;
		guiButtonResizeable.X = single + 148f;
		guiButtonResizeable.Y = single1 + 40f;
		guiButtonResizeable.Width = 150f;
		guiButtonResizeable.Caption = guildUpgrade1.priceUltralibrium.ToString("##,##0");
		guiButtonResizeable.Alignment = 4;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = guildUpgrade1.upgradeType,
			customData2 = (SelectedCurrency)3
		};
		guiButtonResizeable.eventHandlerParam = eventHandlerParam;
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnBuyUpgradeClicked);
		guiButtonResizeable.isEnabled = (!flag || this.guild.bankUltralibrium < (long)guildUpgrade1.priceUltralibrium ? false : !flag1);
		base.AddGuiElement(guiButtonResizeable);
		this.forDelete.Add(guiButtonResizeable);
		if (guiButtonResizeable.isEnabled)
		{
			guiButtonResizeable.drawTooltipWindowCallback = null;
		}
		else
		{
			if (flag)
			{
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_guild_no_money_tooltip"),
					customData2 = guiButtonResizeable
				};
				guiButtonResizeable.tooltipWindowParam = eventHandlerParam;
			}
			else
			{
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_guild_no_bank_permission_tooltip"),
					customData2 = guiButtonResizeable
				};
				guiButtonResizeable.tooltipWindowParam = eventHandlerParam;
			}
			guiButtonResizeable.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		}
		GuiTexture guiTexture2 = new GuiTexture();
		guiTexture2.SetTexture("FrameworkGUI", "res_ultralibrium");
		guiTexture2.X = single + 158f;
		guiTexture2.Y = single1 + 44f;
		base.AddGuiElement(guiTexture2);
		this.forDelete.Add(guiTexture2);
		GuiButtonResizeable action = new GuiButtonResizeable();
		action.SetSmallBlueTexture();
		action.FontSize = 14;
		action.X = single + 148f + 160f;
		action.Y = single1 + 40f;
		action.Width = 150f;
		action.Caption = guildUpgrade1.priceEquilibrium.ToString("##,##0");
		action.Alignment = 4;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = guildUpgrade1.upgradeType,
			customData2 = (SelectedCurrency)2
		};
		action.eventHandlerParam = eventHandlerParam;
		action.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnBuyUpgradeClicked);
		action.isEnabled = (!flag || this.guild.bankEquilib < (long)guildUpgrade1.priceEquilibrium ? false : !flag1);
		base.AddGuiElement(action);
		this.forDelete.Add(action);
		if (action.isEnabled)
		{
			action.drawTooltipWindowCallback = null;
		}
		else
		{
			if (flag)
			{
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_guild_no_money_tooltip"),
					customData2 = action
				};
				action.tooltipWindowParam = eventHandlerParam;
			}
			else
			{
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_guild_no_bank_permission_tooltip"),
					customData2 = action
				};
				action.tooltipWindowParam = eventHandlerParam;
			}
			action.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		}
		GuiTexture guiTexture3 = new GuiTexture();
		guiTexture3.SetTexture("FrameworkGUI", "res_equilibrium");
		guiTexture3.X = single + 316f;
		guiTexture3.Y = single1 + 42f;
		base.AddGuiElement(guiTexture3);
		this.forDelete.Add(guiTexture3);
		GuiButtonResizeable drawTooltipWindow = new GuiButtonResizeable();
		drawTooltipWindow.SetSmallBlueTexture();
		drawTooltipWindow.FontSize = 14;
		drawTooltipWindow.X = single + 148f + 160f + 160f;
		drawTooltipWindow.Y = single1 + 40f;
		drawTooltipWindow.Width = 170f;
		drawTooltipWindow.Caption = guildUpgrade1.priceNova.ToString("##,##0");
		drawTooltipWindow.Alignment = 4;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = guildUpgrade1.upgradeType,
			customData2 = (SelectedCurrency)1
		};
		drawTooltipWindow.eventHandlerParam = eventHandlerParam;
		drawTooltipWindow.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnBuyUpgradeClicked);
		drawTooltipWindow.isEnabled = (!flag || this.guild.bankNova < (long)guildUpgrade1.priceNova ? false : !flag1);
		base.AddGuiElement(drawTooltipWindow);
		this.forDelete.Add(drawTooltipWindow);
		if (drawTooltipWindow.isEnabled)
		{
			drawTooltipWindow.drawTooltipWindowCallback = null;
		}
		else
		{
			if (flag)
			{
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_guild_no_money_tooltip"),
					customData2 = drawTooltipWindow
				};
				drawTooltipWindow.tooltipWindowParam = eventHandlerParam;
			}
			else
			{
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_guild_no_bank_permission_tooltip"),
					customData2 = drawTooltipWindow
				};
				drawTooltipWindow.tooltipWindowParam = eventHandlerParam;
			}
			drawTooltipWindow.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		}
		GuiTexture guiTexture4 = new GuiTexture();
		guiTexture4.SetTexture("FrameworkGUI", "res_nova");
		guiTexture4.X = single + 476f;
		guiTexture4.Y = single1 + 42f;
		base.AddGuiElement(guiTexture4);
		this.forDelete.Add(guiTexture4);
	}

	private void DrawGuildVault()
	{
		GuildWindow.<DrawGuildVault>c__AnonStorey22 variable = null;
		for (int i = 0; i < 40; i++)
		{
			if (i < this.guild.vaultSlots)
			{
				AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = new AndromedaGuiDragDropPlace()
				{
					slot = (byte)i
				};
				Inventory.places.Add(andromedaGuiDragDropPlace);
				andromedaGuiDragDropPlace.txFrameIdle = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotIdle");
				andromedaGuiDragDropPlace.position = new Vector2((float)(308 + i / 4 * 58), (float)(100 + i % 4 * 52));
				andromedaGuiDragDropPlace.dropZonePosition = andromedaGuiDragDropPlace.position;
				SlotItem slotItem = Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(this.guild.guildItems, new Func<SlotItem, bool>(variable, (SlotItem t) => t.get_Slot() == this.i)));
				andromedaGuiDragDropPlace.isEmpty = slotItem == null;
				andromedaGuiDragDropPlace.item = slotItem;
				andromedaGuiDragDropPlace.txFrameDragSource = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotHvr");
				andromedaGuiDragDropPlace.txFrameDropTarget = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotActive");
				andromedaGuiDragDropPlace.txFrameDropTargetHover = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotHvr");
				andromedaGuiDragDropPlace.idleItemTextureSize = new Vector2(51f, 35f);
				andromedaGuiDragDropPlace.location = 18;
			}
			else if (i != this.guild.vaultSlots)
			{
				GuiTexture guiTexture = new GuiTexture();
				guiTexture.SetTexture("ConfigWindow", "inventory_slot_locked");
				guiTexture.boundries = new Rect((float)(306 + i / 4 * 58), (float)(100 + i % 4 * 52), 55f, 55f);
				base.AddGuiElement(guiTexture);
				this.lockedGuildVaultSlots.Add(guiTexture);
				this.forDelete.Add(guiTexture);
			}
			else
			{
				GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
				guiButtonFixed.SetTexture("ConfigWindow", "inventory_slot_plus");
				guiButtonFixed.boundries = new Rect((float)(306 + i / 4 * 58), (float)(100 + i % 4 * 52), 55f, 55f);
				guiButtonFixed.Caption = string.Empty;
				base.AddGuiElement(guiButtonFixed);
				guiButtonFixed.hoverParam = true;
				guiButtonFixed.Hovered = new Action<object, bool>(this, GuildWindow.OnExpandHover);
				EventHandlerParam eventHandlerParam = new EventHandlerParam()
				{
					customData = true
				};
				guiButtonFixed.eventHandlerParam = eventHandlerParam;
				guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnExpandBtnClicked);
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_expand_guild_vault"),
					customData2 = guiButtonFixed
				};
				guiButtonFixed.tooltipWindowParam = eventHandlerParam;
				guiButtonFixed.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				this.lockedGuildVaultSlots.Add(guiButtonFixed);
				this.forDelete.Add(guiButtonFixed);
			}
		}
	}

	private void DrawPlayerInventory()
	{
		GuildWindow.<DrawPlayerInventory>c__AnonStorey23 variable = null;
		List<SlotItem> list = NetworkScript.player.playerBelongings.playerItems.slotItems;
		if (GuildWindow.<>f__am$cache42 == null)
		{
			GuildWindow.<>f__am$cache42 = new Func<SlotItem, bool>(null, (SlotItem t) => t.get_SlotType() == 1);
		}
		List<SlotItem> list1 = Enumerable.ToList<SlotItem>(Enumerable.Where<SlotItem>(list, GuildWindow.<>f__am$cache42));
		this.playerInventorySize = NetworkScript.player.playerBelongings.playerInventorySlots;
		for (int i = 0; i < 40; i++)
		{
			if (i < this.playerInventorySize)
			{
				AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = new AndromedaGuiDragDropPlace()
				{
					slot = (byte)i
				};
				Inventory.places.Add(andromedaGuiDragDropPlace);
				andromedaGuiDragDropPlace.txFrameIdle = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotIdle");
				andromedaGuiDragDropPlace.position = new Vector2((float)(308 + i / 4 * 58), (float)(320 + i % 4 * 52));
				andromedaGuiDragDropPlace.dropZonePosition = andromedaGuiDragDropPlace.position;
				SlotItem slotItem = Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(list1, new Func<SlotItem, bool>(variable, (SlotItem si) => si.get_Slot() == this.i)));
				andromedaGuiDragDropPlace.isEmpty = slotItem == null;
				andromedaGuiDragDropPlace.item = slotItem;
				andromedaGuiDragDropPlace.txFrameDragSource = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotHvr");
				andromedaGuiDragDropPlace.txFrameDropTarget = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotActive");
				andromedaGuiDragDropPlace.txFrameDropTargetHover = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotHvr");
				andromedaGuiDragDropPlace.idleItemTextureSize = new Vector2(51f, 35f);
				andromedaGuiDragDropPlace.location = 1;
			}
			else if (i != this.playerInventorySize || this.playerInventorySize >= 40)
			{
				GuiTexture guiTexture = new GuiTexture();
				guiTexture.SetTexture("ConfigWindow", "inventory_slot_locked");
				guiTexture.boundries = new Rect((float)(306 + i / 4 * 58), (float)(320 + i % 4 * 52), 55f, 55f);
				base.AddGuiElement(guiTexture);
				this.lockedInventorySlots.Add(guiTexture);
				this.forDelete.Add(guiTexture);
			}
			else
			{
				GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
				guiButtonFixed.SetTexture("ConfigWindow", "inventory_slot_plus");
				guiButtonFixed.boundries = new Rect((float)(306 + i / 4 * 58), (float)(320 + i % 4 * 52), 55f, 55f);
				guiButtonFixed.Caption = string.Empty;
				base.AddGuiElement(guiButtonFixed);
				guiButtonFixed.hoverParam = false;
				guiButtonFixed.Hovered = new Action<object, bool>(this, GuildWindow.OnExpandHover);
				EventHandlerParam eventHandlerParam = new EventHandlerParam()
				{
					customData = false
				};
				guiButtonFixed.eventHandlerParam = eventHandlerParam;
				guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnExpandBtnClicked);
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_expand_inventory"),
					customData2 = guiButtonFixed
				};
				guiButtonFixed.tooltipWindowParam = eventHandlerParam;
				guiButtonFixed.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				this.lockedInventorySlots.Add(guiButtonFixed);
				this.forDelete.Add(guiButtonFixed);
			}
		}
	}

	private void DropHandler(AndromedaGuiDragDropPlace src, AndromedaGuiDragDropPlace dst)
	{
		base.Clear();
		this.Create();
	}

	private string GetDepositValidationErrorText(ErrorCode error)
	{
		ErrorCode errorCode = error;
		switch (errorCode)
		{
			case 9:
			{
				return StaticData.Translate("key_error_zero_value");
			}
			case 13:
			{
				return StaticData.Translate("key_error_insufficient_money");
			}
			case 14:
			{
				return StaticData.Translate("key_error_negative_value");
			}
			default:
			{
				if (errorCode == 2)
				{
					break;
				}
				else
				{
					if (errorCode != 3)
					{
						throw new Exception("Unsupported error type!");
					}
					return StaticData.Translate("key_error_invalid_number");
				}
			}
		}
		return StaticData.Translate("key_error_empty");
	}

	private string GetErrorText(ErrorCode error)
	{
		switch (error)
		{
			case 0:
			{
				return StaticData.Translate("key_guild_error_name_reserved");
			}
			case 1:
			{
				return StaticData.Translate("key_guild_error_tag_name_reserved");
			}
			case 2:
			{
				return StaticData.Translate("key_error_empty_string");
			}
			case 3:
			{
				return StaticData.Translate("key_error_invalid_value");
			}
			case 4:
			case 6:
			case 7:
			case 8:
			case 9:
			case 11:
			case 14:
			case 19:
			case 20:
			{
				return StaticData.Translate("key_error_unexpected");
			}
			case 5:
			{
				return StaticData.Translate("key_guild_error_out_of_masters");
			}
			case 10:
			{
				return StaticData.Translate("key_guild_error_out_of_masters");
			}
			case 12:
			{
				return StaticData.Translate("key_guild_error_rank_has_players");
			}
			case 13:
			{
				return StaticData.Translate("key_error_insufficient_money");
			}
			case 15:
			{
				return StaticData.Translate("key_error_too_long");
			}
			case 16:
			{
				return StaticData.Translate("key_error_too_short");
			}
			case 17:
			{
				return StaticData.Translate("key_error_unexisting_player");
			}
			case 18:
			{
				return StaticData.Translate("key_guild_error_already_in_guild");
			}
			case 21:
			{
				return StaticData.Translate("key_guild_error_invitations_limit");
			}
			case 22:
			{
				return StaticData.Translate("key_error_not_the_same_fraction");
			}
			case 23:
			{
				return string.Format(StaticData.Translate("key_error_low_level_player"), 8);
			}
			default:
			{
				return StaticData.Translate("key_error_unexpected");
			}
		}
	}

	private void GetRankFromScreen()
	{
		this.editedRank.name = this.tbRankName.text;
		this.editedRank.canBank = this.cbCanBank.Selected;
		this.editedRank.canEditDetails = this.cbCanEditGuild.Selected;
		this.editedRank.canInvite = this.cbCanInvite.Selected;
		this.editedRank.canChat = this.cbCanChat.Selected;
		this.editedRank.canPromote = this.cbCanPromote.Selected;
		this.editedRank.canVault = this.cbCanVault.Selected;
		this.editedRank.isMaster = this.cbIsMaster.Selected;
	}

	private bool IsLastMasterInGuild(string plrNickName)
	{
		GuildWindow.<IsLastMasterInGuild>c__AnonStorey20 variable = null;
		GuildMember guildMember = null;
		if (plrNickName != NetworkScript.player.vessel.playerName)
		{
			IEnumerable<KeyValuePair<int, PlayerBasic>> enumerable = Enumerable.Where<KeyValuePair<int, PlayerBasic>>(this.guild.members, new Func<KeyValuePair<int, PlayerBasic>, bool>(variable, (KeyValuePair<int, PlayerBasic> p) => p.get_Value().nickName == this.plrNickName));
			if (GuildWindow.<>f__am$cache41 == null)
			{
				GuildWindow.<>f__am$cache41 = new Func<KeyValuePair<int, PlayerBasic>, PlayerBasic>(null, (KeyValuePair<int, PlayerBasic> s) => s.get_Value());
			}
			PlayerBasic playerBasic = Enumerable.FirstOrDefault<PlayerBasic>(Enumerable.Select<KeyValuePair<int, PlayerBasic>, PlayerBasic>(enumerable, GuildWindow.<>f__am$cache41));
			if (playerBasic == null)
			{
				return true;
			}
			guildMember = playerBasic.guildMembership;
		}
		else
		{
			guildMember = NetworkScript.player.guildMember;
		}
		if (guildMember.rank.isMaster && this.guild._mastersCount < 2 && this.guild.members.get_Count() > 1)
		{
			return true;
		}
		return false;
	}

	private static void OnAvatarLoaded(AvatarJob job)
	{
		GuiTexture rect = (GuiTexture)job.token;
		rect.boundries = new Rect(61f, 91f, 100f, 100f);
		rect.SetSize(100f, 100f);
		rect.SetTextureKeepSize(job.job.get_texture());
	}

	private static void OnAvatarLoaded2(AvatarJob job)
	{
		GuiTexture guiTexture = (GuiTexture)job.token;
		guiTexture.SetSize(100f, 100f);
		guiTexture.SetTextureKeepSize(job.job.get_texture());
	}

	private void OnAvatarLoadedInvite(AvatarJob job)
	{
		((GuiTexture)job.token).SetTextureKeepSize(job.job.get_texture());
	}

	public void OnBackBtnClicked(object prm)
	{
		GuildWindow.subSection = 0;
		this.selectedTab = 1;
		this.overviewTabIndex = 1;
		this.Create();
	}

	private void OnBankDepossiteSmallBtnClicked(EventHandlerParam prm)
	{
		int num = 0;
		int num1 = 0;
		int num2 = 0;
		byte num3 = (byte)prm.customData;
		if (!(bool)prm.customData2)
		{
			switch (num3)
			{
				case 0:
				{
					if (!int.TryParse(this.tbDepositUltralibrium.text, ref num))
					{
						this.tbDepositUltralibrium.text = 0.ToString();
					}
					else if (num - 50 <= 0)
					{
						this.tbDepositUltralibrium.text = 0.ToString();
					}
					else if ((long)(num - 50) <= NetworkScript.player.playerBelongings.playerItems.get_Ultralibrium())
					{
						this.tbDepositUltralibrium.text = (num - 50).ToString();
					}
					else
					{
						GuiTextBox str = this.tbDepositUltralibrium;
						long ultralibrium = NetworkScript.player.playerBelongings.playerItems.get_Ultralibrium();
						str.text = ultralibrium.ToString();
					}
					break;
				}
				case 1:
				{
					if (!int.TryParse(this.tbDepositEquilibrium.text, ref num1))
					{
						this.tbDepositEquilibrium.text = 0.ToString();
					}
					else if (num1 - 50 <= 0)
					{
						this.tbDepositEquilibrium.text = 0.ToString();
					}
					else if ((long)(num1 - 50) <= NetworkScript.player.playerBelongings.playerItems.get_Equilibrium())
					{
						this.tbDepositEquilibrium.text = (num1 - 50).ToString();
					}
					else
					{
						GuiTextBox guiTextBox = this.tbDepositEquilibrium;
						long equilibrium = NetworkScript.player.playerBelongings.playerItems.get_Equilibrium();
						guiTextBox.text = equilibrium.ToString();
					}
					break;
				}
				case 2:
				{
					if (!int.TryParse(this.tbDepositNova.text, ref num2))
					{
						this.tbDepositNova.text = 0.ToString();
					}
					else if (num2 - 50 <= 0)
					{
						this.tbDepositNova.text = 0.ToString();
					}
					else if ((long)(num2 - 50) <= NetworkScript.player.playerBelongings.playerItems.get_Nova())
					{
						this.tbDepositNova.text = (num2 - 50).ToString();
					}
					else
					{
						GuiTextBox str1 = this.tbDepositNova;
						long nova = NetworkScript.player.playerBelongings.playerItems.get_Nova();
						str1.text = nova.ToString();
					}
					break;
				}
			}
		}
		else
		{
			switch (num3)
			{
				case 0:
				{
					if (int.TryParse(this.tbDepositUltralibrium.text, ref num) && num > 0)
					{
						if (NetworkScript.player.playerBelongings.playerItems.get_Ultralibrium() < (long)(num + 50) || num < 0)
						{
							GuiTextBox guiTextBox1 = this.tbDepositUltralibrium;
							long ultralibrium1 = NetworkScript.player.playerBelongings.playerItems.get_Ultralibrium();
							guiTextBox1.text = ultralibrium1.ToString();
						}
						else
						{
							this.tbDepositUltralibrium.text = (num + 50).ToString();
						}
					}
					else if (NetworkScript.player.playerBelongings.playerItems.get_Ultralibrium() < (long)50)
					{
						GuiTextBox str2 = this.tbDepositUltralibrium;
						long ultralibrium2 = NetworkScript.player.playerBelongings.playerItems.get_Ultralibrium();
						str2.text = ultralibrium2.ToString();
					}
					else
					{
						this.tbDepositUltralibrium.text = 50.ToString();
					}
					break;
				}
				case 1:
				{
					if (int.TryParse(this.tbDepositEquilibrium.text, ref num1) && num1 > 0)
					{
						if (NetworkScript.player.playerBelongings.playerItems.get_Equilibrium() < (long)(num1 + 50))
						{
							GuiTextBox guiTextBox2 = this.tbDepositEquilibrium;
							long equilibrium1 = NetworkScript.player.playerBelongings.playerItems.get_Equilibrium();
							guiTextBox2.text = equilibrium1.ToString();
						}
						else
						{
							this.tbDepositEquilibrium.text = (num1 + 50).ToString();
						}
					}
					else if (NetworkScript.player.playerBelongings.playerItems.get_Equilibrium() < (long)50)
					{
						GuiTextBox str3 = this.tbDepositEquilibrium;
						long equilibrium2 = NetworkScript.player.playerBelongings.playerItems.get_Equilibrium();
						str3.text = equilibrium2.ToString();
					}
					else
					{
						this.tbDepositEquilibrium.text = 50.ToString();
					}
					break;
				}
				case 2:
				{
					if (int.TryParse(this.tbDepositNova.text, ref num2) && num2 > 0)
					{
						if (NetworkScript.player.playerBelongings.playerItems.get_Nova() < (long)(num2 + 50))
						{
							GuiTextBox guiTextBox3 = this.tbDepositNova;
							long nova1 = NetworkScript.player.playerBelongings.playerItems.get_Nova();
							guiTextBox3.text = nova1.ToString();
						}
						else
						{
							this.tbDepositNova.text = (num2 + 50).ToString();
						}
					}
					else if (NetworkScript.player.playerBelongings.playerItems.get_Nova() < (long)50)
					{
						GuiTextBox str4 = this.tbDepositNova;
						long nova2 = NetworkScript.player.playerBelongings.playerItems.get_Nova();
						str4.text = nova2.ToString();
					}
					else
					{
						this.tbDepositNova.text = 50.ToString();
					}
					break;
				}
			}
		}
	}

	private void OnBtnCommonClicked(EventHandlerParam p)
	{
		int num = (int)p.customData;
		switch (num)
		{
			case 1:
			{
				this.RequestOverviewMember();
				break;
			}
			case 2:
			{
				this.membersScroller = null;
				this.RequestMembersMember();
				break;
			}
			case 3:
			{
				this.RequestInvitationsMember();
				break;
			}
			case 4:
			{
				this.RequestVaultItems();
				break;
			}
			case 5:
			{
				this.RequestRanksMember();
				break;
			}
		}
		this.selectedTab = num;
		this.Create();
	}

	private void OnBtnCreateWithEquilibClicked(EventHandlerParam p)
	{
		this.guild.createCurrency = PlayerItems.TypeEquilibrium;
		this.RequestCreateGuild();
	}

	private void OnBtnCreateWithNovaClicked(EventHandlerParam p)
	{
		this.guild.createCurrency = PlayerItems.TypeNova;
		this.RequestCreateGuild();
	}

	private void OnBtnEditClicked(EventHandlerParam p)
	{
		this.overviewTabIndex = 2;
		this.Create();
	}

	private void OnBtnInfoClicked(EventHandlerParam p)
	{
		this.overviewTabIndex = 1;
		this.Create();
		this.RequestOverviewMember();
	}

	private void OnBtnInfoHover(object prm, bool isIn)
	{
		GuildWindow.HoverPatcher _white = (GuildWindow.HoverPatcher)prm;
		if (!isIn)
		{
			_white.text.textColorNormal = GuiNewStyleBar.blueColor;
			_white.icon.SetTextureNormal("NewGUI", string.Concat(_white.iconName, "Nml"));
		}
		else
		{
			_white.text.textColorNormal = Color.get_white();
			_white.icon.SetTextureNormal("NewGUI", string.Concat(_white.iconName, "Hvr"));
		}
	}

	private void OnBtnLogClicked(EventHandlerParam p)
	{
		// 
		// Current member / type: System.Void GuildWindow::OnBtnLogClicked(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnBtnLogClicked(EventHandlerParam)
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

	private void OnBtnSave(EventHandlerParam p)
	{
		this.lblErrorDepositNova.text = string.Empty;
		this.lblErrorDepositEquilibrium.text = string.Empty;
		this.lblErrorDepositUltralibrium.text = string.Empty;
		this.lblErrorDepositMain.text = string.Empty;
		this.ClearDetailsErrors();
		ValidationErrors validationError = this.ValidateDetails();
		if (validationError.errors.get_Count() > 0)
		{
			this.ShowDetailsErrors(validationError);
			return;
		}
		this.guild.name = this.tbGuildName.text;
		this.guild.title = this.tbGuildTitle.text;
		this.guild.description = this.tbDescription.text;
		playWebGame.udp.ExecuteCommand(179, this.guild, 13);
	}

	private void OnBtnTabCreateClicked(EventHandlerParam p)
	{
		this.selectedTab = 1;
		this.guild = new Guild();
		this.Create();
	}

	private void OnBuyUpgradeClicked(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void GuildWindow::OnBuyUpgradeClicked(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnBuyUpgradeClicked(EventHandlerParam)
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

	private void OnCancelKickPlayer(object obj)
	{
		if (this.dlgConfirmRemovePlayer != null)
		{
			this.dlgConfirmRemovePlayer.RemoveGUIItems();
			this.dlgConfirmRemovePlayer = null;
		}
		this.ignoreClickEvents = false;
	}

	private void OnCancelNew(EventHandlerParam p)
	{
		this.guild.ranks.Remove(0);
		this.editedRankIndex = 0;
		this.Create();
	}

	public override void OnClose()
	{
		GuildWindow.subSection = 0;
		playWebGame.udp.ExecuteCommand(166, null);
	}

	private void OnDepositClicked(EventHandlerParam p)
	{
		this.ClearDetailsErrors();
		this.lblErrorDepositNova.text = string.Empty;
		this.lblErrorDepositMain.text = string.Empty;
		this.lblErrorDepositEquilibrium.text = string.Empty;
		this.lblErrorDepositUltralibrium.text = string.Empty;
		ValidationErrors validationError = this.ValidateDeposit();
		if (validationError.errors.get_Count() > 0)
		{
			foreach (KeyValuePair<int, ErrorCode> error in validationError.errors)
			{
				switch (error.get_Key())
				{
					case 0:
					{
						this.VisualizeValidationError(this.lblErrorDepositUltralibrium, this.GetDepositValidationErrorText(error.get_Value()));
						continue;
					}
					case 1:
					{
						this.VisualizeValidationError(this.lblErrorDepositEquilibrium, this.GetDepositValidationErrorText(error.get_Value()));
						continue;
					}
					case 2:
					{
						this.VisualizeValidationError(this.lblErrorDepositNova, this.GetDepositValidationErrorText(error.get_Value()));
						continue;
					}
					case 3:
					{
						this.VisualizeValidationError(this.lblErrorDepositMain, this.GetDepositValidationErrorText(error.get_Value()));
						continue;
					}
					default:
					{
						continue;
					}
				}
			}
			return;
		}
		GenericData genericDatum = new GenericData();
		if (this.tbDepositUltralibrium.text == string.Empty)
		{
			genericDatum.int1 = 0;
		}
		else
		{
			genericDatum.int1 = int.Parse(this.tbDepositUltralibrium.text);
		}
		if (this.tbDepositEquilibrium.text == string.Empty)
		{
			genericDatum.int2 = 0;
		}
		else
		{
			genericDatum.int2 = int.Parse(this.tbDepositEquilibrium.text);
		}
		if (this.tbDepositNova.text == string.Empty)
		{
			genericDatum.int3 = 0;
		}
		else
		{
			genericDatum.int3 = int.Parse(this.tbDepositNova.text);
		}
		this.tbDepositUltralibrium.text = "0";
		this.tbDepositEquilibrium.text = "0";
		this.tbDepositNova.text = "0";
		playWebGame.udp.ExecuteCommand(182, genericDatum);
	}

	private void OnDrawMembers()
	{
		Vector2 innerMousePosition = this.membersScroller.InnerMousePosition;
		if (innerMousePosition.x < 2f)
		{
			this.RemoveCurrentHoverElement();
			return;
		}
		if (innerMousePosition.x > 620f)
		{
			this.RemoveCurrentHoverElement();
			return;
		}
		if (innerMousePosition.y < 0f)
		{
			this.RemoveCurrentHoverElement();
			return;
		}
		int num = (int)(innerMousePosition.y / 30f);
		if (num < 0)
		{
			this.RemoveCurrentHoverElement();
			return;
		}
		if (num >= (int)this.members2edit.Length)
		{
			this.RemoveCurrentHoverElement();
			return;
		}
		if (this.members2edit[num] == null)
		{
			this.RemoveCurrentHoverElement();
			return;
		}
		if (this.hoveredItemIndex == num)
		{
			return;
		}
		if (this.hoveredItemIndex > -1)
		{
			this.RemoveCurrentHoverElement();
		}
		GuildWindow.MemberListItem memberListItem = this.members2edit[num];
		memberListItem.btnDelete.Y = memberListItem.offsetY + 7f;
		this.membersScroller.SpecialAddContent(memberListItem.btnDelete);
		if (num != this.editingMemberIndex)
		{
			memberListItem.btnEdit.Y = memberListItem.offsetY + 5f;
			this.membersScroller.SpecialAddContent(memberListItem.btnEdit);
		}
		this.hoveredItemIndex = num;
	}

	private void OnEditMemberRankClick(EventHandlerParam p)
	{
		int num = (int)p.customData;
		if (this.editingMemberIndex != -1)
		{
			GuildWindow.MemberListItem memberListItem = this.members2edit[this.editingMemberIndex];
			this.membersScroller.RemoveContent(memberListItem.ddlRank);
			memberListItem.lblRank.Y = memberListItem.offsetY;
			this.membersScroller.SpecialAddContent(memberListItem.lblRank);
		}
		this.editingMemberIndex = num;
		GuildWindow.MemberListItem memberListItem1 = this.members2edit[this.editingMemberIndex];
		memberListItem1.ddlRank.Y = memberListItem1.offsetY + 4f;
		this.membersScroller.SpecialAddContent(memberListItem1.ddlRank);
		this.membersScroller.RemoveContent(memberListItem1.btnEdit);
		this.membersScroller.RemoveContent(memberListItem1.lblRank);
	}

	private void OnExpandBtnClicked(EventHandlerParam prm)
	{
		this.expandSlotData = new UniversalTransportContainer();
		bool flag = (bool)prm.customData;
		int num = 0;
		SlotPriceInfo slotPriceInfo = null;
		if (!flag)
		{
			slotPriceInfo = Enumerable.FirstOrDefault<SlotPriceInfo>(Enumerable.Where<SlotPriceInfo>(StaticData.slotPriceInformation, new Func<SlotPriceInfo, bool>(this, (SlotPriceInfo s) => (s.slotId != this.playerInventorySize + 4 ? false : s.slotType == "Inventory"))));
			this.expandSlotData.wantedSlot = 7;
		}
		else
		{
			slotPriceInfo = Enumerable.FirstOrDefault<SlotPriceInfo>(Enumerable.Where<SlotPriceInfo>(StaticData.slotPriceInformation, new Func<SlotPriceInfo, bool>(this, (SlotPriceInfo s) => (s.slotId != this.guild.vaultSlots + 4 ? false : s.slotType == "GuildVault"))));
			this.expandSlotData.wantedSlot = 9;
		}
		num = (slotPriceInfo == null ? 999999999 : slotPriceInfo.priceNova);
		this.expandSlotWindow = new ExpandSlotDialog()
		{
			ConfirmExpand = new Action<EventHandlerParam>(this, GuildWindow.ConfirmExpand)
		};
		if (this.expandSlotData.wantedSlot == 7)
		{
			this.expandSlotWindow.CreateExpandDialog(num, 1, StaticData.Translate("key_ship_cfg_expand_modal"), AndromedaGui.gui);
		}
		else if (this.expandSlotData.wantedSlot == 9)
		{
			this.CreateExpandGuildVaultDialog();
		}
	}

	private void OnExpandHover(object prm, bool state)
	{
		bool flag = (bool)prm;
		List<GuiElement> list = null;
		string empty = string.Empty;
		list = (!flag ? this.lockedInventorySlots : this.lockedGuildVaultSlots);
		empty = (!state ? "inventory_slot_locked" : "inventory_slot_lockedHvr");
		for (int i = 1; i < 4; i++)
		{
			((GuiTexture)list.get_Item(i)).SetTextureKeepSize("ConfigWindow", empty);
		}
	}

	private void OnGiuldPreviewClicked(EventHandlerParam p)
	{
		// 
		// Current member / type: System.Void GuildWindow::OnGiuldPreviewClicked(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnGiuldPreviewClicked(EventHandlerParam)
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

	private void OnGoToBankBtnClicked(object prm)
	{
		if (this.selectedTab != 1 || GuildWindow.subSection != 0)
		{
			GuildWindow.subSection = 0;
			this.selectedTab = 1;
			this.overviewTabIndex = 1;
			this.Create();
		}
	}

	private void OnInvitationRemove(EventHandlerParam p)
	{
		// 
		// Current member / type: System.Void GuildWindow::OnInvitationRemove(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnInvitationRemove(EventHandlerParam)
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

	private void OnInvitationsClicked(EventHandlerParam obj)
	{
		// 
		// Current member / type: System.Void GuildWindow::OnInvitationsClicked(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnInvitationsClicked(EventHandlerParam)
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

	private void OnInvite(EventHandlerParam p)
	{
		// 
		// Current member / type: System.Void GuildWindow::OnInvite(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnInvite(EventHandlerParam)
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

	private void OnInviteAccept(EventHandlerParam p)
	{
		// 
		// Current member / type: System.Void GuildWindow::OnInviteAccept(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnInviteAccept(EventHandlerParam)
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

	private void OnInviteReject(EventHandlerParam p)
	{
		// 
		// Current member / type: System.Void GuildWindow::OnInviteReject(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnInviteReject(EventHandlerParam)
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

	private void OnInviteToParty(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void GuildWindow::OnInviteToParty(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnInviteToParty(EventHandlerParam)
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

	private void OnLeaveCancel(EventHandlerParam p)
	{
		AndromedaGui.gui.RemoveWindow(this.dialogWindow.handler);
		this.dialogWindow = null;
		this.ClearDetailsErrors();
	}

	private void OnLeaveCancel()
	{
		AndromedaGui.gui.RemoveWindow(this.dialogWindow.handler);
		AndromedaGui.gui.activeToolTipId = -1;
	}

	private void OnLeaveClicked(EventHandlerParam p)
	{
		this.dialogWindow = new GuiWindow()
		{
			isModal = true
		};
		this.dialogWindow.SetBackgroundTexture("ConfigWindow", "proba");
		this.dialogWindow.isHidden = false;
		this.dialogWindow.zOrder = 220;
		this.dialogWindow.PutToCenter();
		this.dialogWindow.isHidden = false;
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.boundries.set_x(100f);
		guiButtonResizeable.boundries.set_y(130f);
		guiButtonResizeable.boundries.set_width(130f);
		guiButtonResizeable.Caption = StaticData.Translate("key_guild_leave2");
		guiButtonResizeable.FontSize = 12;
		guiButtonResizeable.Alignment = 1;
		guiButtonResizeable.MarginTop = 5;
		guiButtonResizeable.SetSmallBlueTexture();
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnLeaveConfirm);
		this.dialogWindow.AddGuiElement(guiButtonResizeable);
		AndromedaGui.gui.AddWindow(this.dialogWindow);
		AndromedaGui.gui.activeToolTipId = this.dialogWindow.handler;
		GuiButtonResizeable action = new GuiButtonResizeable();
		action.boundries.set_x(250f);
		action.boundries.set_y(130f);
		action.boundries.set_width(130f);
		action.Caption = StaticData.Translate("key_guild_cancel");
		action.FontSize = 12;
		action.Alignment = 1;
		action.MarginTop = 5;
		action.SetSmallBlueTexture();
		action.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnLeaveCancel);
		this.dialogWindow.AddGuiElement(action);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(this.dialogWindow.boundries.get_width() * 0.05f, 45f, this.dialogWindow.boundries.get_width() * 0.9f, 60f),
			text = StaticData.Translate("key_guild_leave_warning"),
			Alignment = 4,
			FontSize = 18
		};
		this.dialogWindow.AddGuiElement(guiLabel);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("ConfigWindow", "X_btn_");
		guiButtonFixed.X = 417f;
		guiButtonFixed.Y = -3f;
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnLeaveCancel);
		this.dialogWindow.AddGuiElement(guiButtonFixed);
		if (this.IsLastMasterInGuild(NetworkScript.player.vessel.playerName))
		{
			guiButtonResizeable.isEnabled = false;
			GuiLabel guiLabel1 = new GuiLabel()
			{
				text = StaticData.Translate("key_guild_error_out_of_masters"),
				FontSize = 12,
				Font = GuiLabel.FontBold,
				TextColor = Color.get_red(),
				Alignment = 4,
				boundries = new Rect(this.dialogWindow.boundries.get_width() / 2f - 170f, 30f, 340f, 30f)
			};
			this.dialogWindow.AddGuiElement(guiLabel1);
		}
	}

	private void OnLeaveConfirm(EventHandlerParam p)
	{
		AndromedaGui.gui.RemoveWindow(this.dialogWindow.handler);
		this.dialogWindow = null;
		this.ClearDetailsErrors();
		playWebGame.udp.ExecuteCommand(179, null, 27);
	}

	private void OnMemberRankChanged(int index)
	{
		// 
		// Current member / type: System.Void GuildWindow::OnMemberRankChanged(System.Int32)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnMemberRankChanged(System.Int32)
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

	private void OnOverviewClicked(EventHandlerParam obj)
	{
		this.selectedTab = 1;
		this.overviewTabIndex = 1;
		this.Create();
	}

	private static void OnPreviewClose(EventHandlerParam p)
	{
		AndromedaGui.gui.RemoveWindow(GuildWindow.wndPreview.handler);
		AndromedaGui.gui.activeToolTipId = -1;
		GuildWindow.wndPreview = null;
	}

	private void OnRankDelete(EventHandlerParam p)
	{
		// 
		// Current member / type: System.Void GuildWindow::OnRankDelete(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnRankDelete(EventHandlerParam)
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

	private void OnRankIconLeftClick(EventHandlerParam p)
	{
		GuildRank guildRank = this.editedRank;
		guildRank.avatarIndex = (short)(guildRank.avatarIndex - 1);
		if (this.editedRank.avatarIndex < 0)
		{
			this.editedRank.avatarIndex = 11;
		}
		this.txRankEdited.SetTexture("NewGUI", string.Concat("guildRank", this.editedRank.avatarIndex.ToString()));
	}

	private void OnRankIconRightClick(EventHandlerParam p)
	{
		GuildRank guildRank = this.editedRank;
		guildRank.avatarIndex = (short)(guildRank.avatarIndex + 1);
		if (this.editedRank.avatarIndex >= 12)
		{
			this.editedRank.avatarIndex = 0;
		}
		this.txRankEdited.SetTexture("NewGUI", string.Concat("guildRank", this.editedRank.avatarIndex.ToString()));
	}

	private void OnRankNew(EventHandlerParam p)
	{
		// 
		// Current member / type: System.Void GuildWindow::OnRankNew(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnRankNew(EventHandlerParam)
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

	private void OnRankSave(EventHandlerParam p)
	{
		// 
		// Current member / type: System.Void GuildWindow::OnRankSave(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnRankSave(EventHandlerParam)
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

	private void OnRankSelected4Edit(EventHandlerParam p)
	{
		this.PurifyRanks();
		this.editedRankIndex = (int)p.customData;
		this.Create();
	}

	private void OnRemoveMember(object prm)
	{
		// 
		// Current member / type: System.Void GuildWindow::OnRemoveMember(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnRemoveMember(System.Object)
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

	private void OnTransformerBtnClicked(object prm)
	{
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = (byte)22
		};
		if (!NetworkScript.player.guildMember.rank.canBank)
		{
			eventHandlerParam.customData2 = (byte)1;
		}
		else
		{
			eventHandlerParam.customData2 = (byte)2;
		}
		AndromedaGui.mainWnd.OnWindowBtnClicked(eventHandlerParam);
	}

	private void OnUpgradesBtnClicked(object prm)
	{
		this.customOnGUIAction = null;
		Inventory.ClearSlots(this);
		GuildWindow.subSection = 1;
		foreach (GuiElement guiElement in this.forDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.forDelete.Clear();
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(275f, 35f, 567f, 30f),
			text = StaticData.Translate("key_guilds_menu_lbl_upgrades"),
			FontSize = 21,
			Font = GuiLabel.FontBold,
			Alignment = 1,
			TextColor = GuiNewStyleBar.orangeColor
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		this.txTab.SetTexture("NewGUI", "guild_upgrades_frame");
		this.txTab.X = 206f;
		this.txTab.Y = 67f;
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.SetSmallBlueTexture();
		guiButtonResizeable.FontSize = 15;
		guiButtonResizeable.X = 30f;
		guiButtonResizeable.Y = 505f;
		guiButtonResizeable.Width = 170f;
		guiButtonResizeable.Caption = StaticData.Translate("btn_back");
		guiButtonResizeable.textColorNormal = Color.get_white();
		guiButtonResizeable.Alignment = 4;
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, GuildWindow.OnBackBtnClicked);
		base.AddGuiElement(guiButtonResizeable);
		this.forDelete.Add(guiButtonResizeable);
		for (int i = 0; i < 5; i++)
		{
			this.DrawGuildUpgrade(i);
		}
	}

	private void PopulateBankAmounts()
	{
		if (this.bankLblEquilibrium != null)
		{
			this.bankLblEquilibrium.text = this.guild.bankEquilib.ToString("##,##0");
		}
		if (this.bankLblUltralibrium != null)
		{
			this.bankLblUltralibrium.text = this.guild.bankUltralibrium.ToString("##,##0");
		}
		if (this.bankLblNova != null)
		{
			this.bankLblNova.text = this.guild.bankNova.ToString("##,##0");
		}
		this.leftPanelLblEquilibrium.text = this.guild.bankEquilib.ToString("##,##0");
		this.leftPanelLblUltralibrium.text = this.guild.bankUltralibrium.ToString("##,##0");
		this.leftPanelLblNova.text = this.guild.bankNova.ToString("##,##0");
		if (GuildWindow.subSection == 1)
		{
			this.PopulateGuildUpgradeMenu(this.guild);
		}
	}

	public void PopulateGuildUpgradeMenu(Guild update)
	{
		this.guild.upgradeOneLevel = update.upgradeOneLevel;
		this.guild.upgradeTwoLevel = update.upgradeTwoLevel;
		this.guild.upgradeThreeLevel = update.upgradeThreeLevel;
		this.guild.upgradeFourLevel = update.upgradeFourLevel;
		this.guild.upgradeFiveLevel = update.upgradeFiveLevel;
		if (GuildWindow.subSection != 1)
		{
			return;
		}
		foreach (GuiElement guiElement in this.forDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.forDelete.Clear();
		this.OnUpgradesBtnClicked(null);
	}

	private void PurifyRanks()
	{
		if (this.guild.ranks.ContainsKey(0))
		{
			this.guild.ranks.Remove(0);
		}
	}

	private void PutOrangeHeadLabel(string text, float x)
	{
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(x, this.txTab.Y + 45f, 120f, 19f),
			Alignment = 0,
			text = text.ToUpper(),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
	}

	private static void PutOrangeHeadLabelPreview(string text, float x)
	{
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(x, 240f, 120f, 19f),
			Alignment = 0,
			text = text,
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor
		};
		GuildWindow.wndPreview.AddGuiElement(guiLabel);
	}

	private GuiCheckbox PutRankSingleCheckBox(int x, int y, string caption, GuiScrollingContainer aScroller, bool val, bool isActive)
	{
		GuiCheckbox guiCheckbox = new GuiCheckbox()
		{
			X = (float)x,
			Y = (float)y,
			Selected = val,
			isActive = isActive
		};
		aScroller.AddContent(guiCheckbox);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect((float)(x + 25), (float)(y + 2), 90f, 20f),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 0,
			text = caption
		};
		aScroller.AddContent(guiLabel);
		return guiCheckbox;
	}

	public void ReceivedGuildData(ITransferable data, TransferContext ctx)
	{
		TransferContext transferContext = ctx;
		switch (transferContext)
		{
			case 14:
			{
				if (this.selectedTab == 1)
				{
					this.ShowDetailsErrors((ValidationErrors)data);
				}
				return;
			}
			case 15:
			{
				if (this.selectedTab == 1 && this.overviewTabIndex == 2)
				{
					this.ShowDetailsErrors((ValidationErrors)data);
				}
				return;
			}
			case 19:
			{
				this.ShowGeneralErrors((ValidationErrors)data);
				return;
			}
			default:
			{
				if (transferContext == 32)
				{
					break;
				}
				else
				{
					goto Label0;
				}
			}
		}
		if (this.selectedTab == 3)
		{
			this.ShowInvitationsErrors((ValidationErrors)data);
		}
		return;
	Label0:
		Guild guild = (Guild)data;
		transferContext = ctx;
		switch (transferContext)
		{
			case 1:
			case 4:
			{
			Label1:
				GuildWindow.membersCount = guild.members.get_Count();
				this.guild = guild;
				this.selectedTab = 1;
				this.overviewTabIndex = 1;
				NetworkScript.player.guild = this.guild;
				this.ReCreate();
				break;
			}
			case 5:
			{
				NetworkScript.player.guildMember = null;
				NetworkScript.player.guild = null;
				this.guild = new Guild();
				this.selectedTab = 1;
				this.ReCreate();
				break;
			}
			case 6:
			{
				this.guild.invitations = guild.invitations;
				this.selectedTab = 3;
				this.Create();
				break;
			}
			case 7:
			{
				this.guild = guild;
				this.selectedTab = 2;
				NetworkScript.player.playerBelongings.guildInvitesCount = (short)guild.invitations.get_Count();
				AndromedaGui.mainWnd.SetGuildButtonAlerted();
				this.ReCreate();
				break;
			}
			case 9:
			{
				this.guild.ranks = guild.ranks;
				this.guild.members = guild.members;
				GuildWindow.membersCount = guild.members.get_Count();
				this.guild.id = guild.id;
				this.guild._mastersCount = guild._mastersCount;
				this.selectedTab = 2;
				this.Create();
				break;
			}
			case 10:
			{
				this.guild.vaultSlots = guild.vaultSlots;
				this.guild.guildItems = guild.guildItems;
				NetworkScript.player.guild.guildItems = guild.guildItems;
				NetworkScript.player.guild.vaultSlots = guild.vaultSlots;
				if (this.selectedTab == 4)
				{
					this.CreateGuildVault();
				}
				break;
			}
			case 11:
			{
				this.guild.members = guild.members;
				GuildWindow.membersCount = guild.members.get_Count();
				this.guild.ranks = guild.ranks;
				this.guild.id = guild.id;
				this.selectedTab = 5;
				this.editedRankIndex = 0;
				this.Create();
				break;
			}
			default:
			{
				switch (transferContext)
				{
					case 23:
					{
						this.guild.bankUltralibrium = guild.bankUltralibrium;
						this.guild.bankNova = guild.bankNova;
						this.guild.bankEquilib = guild.bankEquilib;
						NetworkScript.player.guild.bankNova = this.guild.bankNova;
						NetworkScript.player.guild.bankEquilib = this.guild.bankEquilib;
						NetworkScript.player.guild.bankUltralibrium = this.guild.bankUltralibrium;
						this.PopulateBankAmounts();
						break;
					}
					case 24:
					{
						this.guild.log.Clear();
						foreach (GuildLogRecord guildLogRecord in guild.log)
						{
							this.guild.log.Add(guildLogRecord);
						}
						if (this.selectedTab == 1 && this.overviewTabIndex == 3)
						{
							this.Create();
						}
						break;
					}
					case 25:
					{
						GuildWindow.membersCount = guild.members.get_Count();
						this.guild.upgradeOneLevel = guild.upgradeOneLevel;
						this.guild.upgradeTwoLevel = guild.upgradeTwoLevel;
						this.guild.upgradeThreeLevel = guild.upgradeThreeLevel;
						this.guild.upgradeFourLevel = guild.upgradeFourLevel;
						this.guild.upgradeFiveLevel = guild.upgradeFiveLevel;
						this.Create();
						break;
					}
					case 26:
					{
						goto Label1;
					}
					default:
					{
						if (transferContext != 37)
						{
							throw new Exception(string.Concat("Unsupported guild transport context: ", ctx.ToString()));
						}
						if (NetworkScript.player.guildMember == null)
						{
							NetworkScript.player.playerBelongings.guildInvitesCount = 0;
							AndromedaGui.mainWnd.SetGuildButtonAlerted();
							NetworkScript.player.guildMember = new GuildMember();
						}
						NetworkScript.player.guildMember.rank = guild.ranks.get_Values().get_Item(0);
						break;
					}
				}
				break;
			}
		}
	}

	public void ReceivedNonMemberInvitations(Guild guild)
	{
		this.guild = guild;
		this.selectedTab = 2;
		this.Create();
	}

	private void ReCreate()
	{
		if (this.guild == null)
		{
			return;
		}
		this.customOnGUIAction = null;
		this.DeleteAll();
		this.player = NetworkScript.player;
		base.SetBackgroundTexture("NewGUI", "novashop_frame");
		this.zOrder = 210;
		this.isHidden = false;
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(275f, 35f, 567f, 30f),
			text = StaticData.Translate("key_guilds"),
			FontSize = 21,
			Font = GuiLabel.FontBold,
			Alignment = 1,
			TextColor = GuiNewStyleBar.orangeColor
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("NewGUI", "pvp_ranking_left");
		guiTexture.X = 18f;
		guiTexture.Y = 28f;
		base.AddGuiElement(guiTexture);
		this.forDeleteFromLeftPanel.Add(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("FrameworkGUI", (this.player.vessel.fractionId != 1 ? "fraction2Icon" : "fraction1Icon"));
		guiTexture1.X = 40f;
		guiTexture1.Y = 45f;
		base.AddGuiElement(guiTexture1);
		this.forDeleteFromLeftPanel.Add(guiTexture1);
		this.lblGuildName = new GuiLabel()
		{
			text = string.Empty,
			FontSize = 14,
			Alignment = 0,
			boundries = new Rect(69f, 45f, 130f, 20f)
		};
		base.AddGuiElement(this.lblGuildName);
		this.forDeleteFromLeftPanel.Add(this.lblGuildName);
		GuiTexture guiTexture2 = new GuiTexture();
		guiTexture2.SetTexture((Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "GuildFramePic"));
		guiTexture2.X = 60f;
		guiTexture2.Y = 90f;
		base.AddGuiElement(guiTexture2);
		this.forDeleteFromLeftPanel.Add(guiTexture2);
		if (this.guild.id != 0)
		{
			this.CreateMember();
		}
		else
		{
			this.CreateNonMember();
		}
	}

	private void RefreshExpandBtnState()
	{
		int num = 0;
		num = (this.playerInventorySize <= 28 ? Enumerable.First<SlotPriceInfo>(Enumerable.Where<SlotPriceInfo>(StaticData.slotPriceInformation, new Func<SlotPriceInfo, bool>(this, (SlotPriceInfo s) => (s.slotId != this.playerInventorySize + 4 ? false : s.slotType == "Inventory")))).priceNova : 999999999);
		foreach (GuiElement lockedInventorySlot in this.lockedInventorySlots)
		{
			if (!(lockedInventorySlot is GuiButtonFixed))
			{
				continue;
			}
			((GuiButtonFixed)lockedInventorySlot).isEnabled = NetworkScript.player.playerBelongings.playerItems.get_Nova() >= (long)num;
		}
	}

	private void RemoveCurrentHoverElement()
	{
		if (this.hoveredItemIndex > -1)
		{
			this.editingMemberIndex <= -1;
			GuildWindow.MemberListItem memberListItem = this.members2edit[this.hoveredItemIndex];
			this.membersScroller.RemoveContent(memberListItem.btnDelete);
			this.membersScroller.RemoveContent(memberListItem.btnEdit);
			this.hoveredItemIndex = -1;
		}
	}

	public static string RemoveLastNewLines(string txt, int cnt)
	{
		txt = new string(Enumerable.ToArray<char>(Enumerable.Reverse<char>(txt)));
		int num = 0;
		while (num < cnt)
		{
			int num1 = txt.IndexOf("\n");
			if (num1 >= 0)
			{
				txt = txt.Remove(num1, 1);
				num++;
			}
			else
			{
				break;
			}
		}
		return new string(Enumerable.ToArray<char>(Enumerable.Reverse<char>(txt)));
	}

	private void RequestCreateGuild()
	{
		this.ClearDetailsErrors();
		ValidationErrors validationError = this.ValidateDetails();
		if (validationError.errors.get_Count() > 0)
		{
			this.ShowDetailsErrors(validationError);
			return;
		}
		this.guild.name = this.tbGuildName.text;
		this.guild.title = this.tbGuildTitle.text;
		this.guild.description = this.tbDescription.text;
		playWebGame.udp.ExecuteCommand(179, this.guild, 12);
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("guild_name", this.guild.name);
		dictionary.Add("guild_title", this.guild.title);
		dictionary.Add("guild_currency", this.guild.createCurrency);
		playWebGame.LogMixPanel(MixPanelEvents.CreateGuild, dictionary);
	}

	private void RequestInvitationsMember()
	{
		// 
		// Current member / type: System.Void GuildWindow::RequestInvitationsMember()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void RequestInvitationsMember()
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

	private void RequestInvitationsNonMember()
	{
		playWebGame.udp.ExecuteCommand(177, null);
	}

	private void RequestMembersMember()
	{
		// 
		// Current member / type: System.Void GuildWindow::RequestMembersMember()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void RequestMembersMember()
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

	private void RequestOverviewMember()
	{
		// 
		// Current member / type: System.Void GuildWindow::RequestOverviewMember()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void RequestOverviewMember()
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

	private void RequestRanksMember()
	{
		// 
		// Current member / type: System.Void GuildWindow::RequestRanksMember()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void RequestRanksMember()
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

	private void RequestVaultItems()
	{
		// 
		// Current member / type: System.Void GuildWindow::RequestVaultItems()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void RequestVaultItems()
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

	private string ShortenText(string text, int maxLen, GuiLabel lbl)
	{
		if (text == null)
		{
			return null;
		}
		if (text.get_Length() <= maxLen)
		{
			return text;
		}
		lbl.ToolTipText = text;
		lbl.ToolTipPos = new Rect(0f, 0f, 300f, 80f);
		return string.Concat(text.Substring(0, maxLen), "...");
	}

	private void ShowDetailsErrors(ValidationErrors errors)
	{
		GuiLabel[] guiLabelArray = new GuiLabel[] { this.lblErrorName, this.lblErrorTitle, this.lblErrorDescription, this.lblErrorLeave };
		foreach (KeyValuePair<int, ErrorCode> error in errors.errors)
		{
			guiLabelArray[error.get_Key()].text = this.GetErrorText(error.get_Value());
		}
	}

	private void ShowGeneralErrors(ValidationErrors errors)
	{
		foreach (KeyValuePair<int, ErrorCode> error in errors.errors)
		{
			GuiLabel errorText = null;
			if (error.get_Value() != 10)
			{
				throw new Exception(string.Concat(new object[] { "Undefined guild error! key=", error.get_Key(), "field=", error.get_Value().ToString() }));
			}
			int key = error.get_Key();
			if (key == 17)
			{
				errorText = this.lblErrorLeave;
			}
			else
			{
				if (key != 18)
				{
					throw new Exception(string.Concat(new object[] { "Undefined error field! key=", error.get_Key(), "field=", error.get_Value().ToString() }));
				}
				errorText = this.lblErrorLeave;
			}
			errorText.text = this.GetErrorText(error.get_Value());
		}
	}

	private void ShowInvitationsErrors(ValidationErrors errors)
	{
		GuiLabel[] guiLabelArray = new GuiLabel[] { this.lblErrorInvitations };
		foreach (KeyValuePair<int, ErrorCode> error in errors.errors)
		{
			guiLabelArray[error.get_Key()].text = this.GetErrorText(error.get_Value());
		}
	}

	private void ShowRemoveMemberConfirmation(EventHandlerParam prm)
	{
		if (this.dlgConfirmRemovePlayer != null)
		{
			this.dlgConfirmRemovePlayer.RemoveGUIItems();
			this.dlgConfirmRemovePlayer = null;
		}
		PlayerBasic playerBasic = prm.customData as PlayerBasic;
		bool flag = playerBasic.nickName == NetworkScript.player.vessel.playerName;
		string str = (!flag ? string.Format(StaticData.Translate("key_guild_kick_confirmation"), playerBasic.nickName, playerBasic.level) : StaticData.Translate("key_guild_leave_confirmation"));
		string str1 = (!flag ? StaticData.Translate("key_guild_kick_member") : StaticData.Translate("key_guild_leave"));
		this.dlgConfirmRemovePlayer = new GuiDialog();
		this.dlgConfirmRemovePlayer.Create(str, str1, StaticData.Translate("key_dialog_cancel"), null);
		this.dlgConfirmRemovePlayer.OkClicked = new Action<object>(this, GuildWindow.OnRemoveMember);
		this.dlgConfirmRemovePlayer.btnOK.eventHandlerParam.customData = playerBasic.nickName;
		this.dlgConfirmRemovePlayer.CancelClicked = new Action<object>(this, GuildWindow.OnCancelKickPlayer);
		this.ignoreClickEvents = true;
		if (this.IsLastMasterInGuild(playerBasic.nickName))
		{
			this.dlgConfirmRemovePlayer.btnOK.isEnabled = false;
			GuiLabel guiLabel = new GuiLabel()
			{
				text = StaticData.Translate("key_guild_error_out_of_masters"),
				FontSize = 12,
				Font = GuiLabel.FontBold,
				TextColor = Color.get_red(),
				Alignment = 4,
				boundries = new Rect(this.dlgConfirmRemovePlayer.Container.boundries.get_width() / 2f - 210f, 45f, 420f, 30f)
			};
			this.dlgConfirmRemovePlayer.Container.AddGuiElement(guiLabel);
		}
	}

	private void StartChatWith(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void GuildWindow::StartChatWith(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void StartChatWith(EventHandlerParam)
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

	private ValidationErrors ValidateDeposit()
	{
		// 
		// Current member / type: ValidationErrors GuildWindow::ValidateDeposit()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: ValidationErrors ValidateDeposit()
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

	private ValidationErrors ValidateDetails()
	{
		// 
		// Current member / type: ValidationErrors GuildWindow::ValidateDetails()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: ValidationErrors ValidateDetails()
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

	private ValidationErrors ValidateRankAdd()
	{
		// 
		// Current member / type: ValidationErrors GuildWindow::ValidateRankAdd()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: ValidationErrors ValidateRankAdd()
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

	private ValidationErrors ValidateRankDelete()
	{
		// 
		// Current member / type: ValidationErrors GuildWindow::ValidateRankDelete()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: ValidationErrors ValidateRankDelete()
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

	private ValidationErrors ValidateRankEdit()
	{
		// 
		// Current member / type: ValidationErrors GuildWindow::ValidateRankEdit()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: ValidationErrors ValidateRankEdit()
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

	private void ViewPlayerProfile(EventHandlerParam p)
	{
		NetworkScript.RequestUserProfile((string)p.customData);
	}

	private void VisualizeValidationError(GuiLabel lbl, string text)
	{
		if (lbl.text.get_Length() <= 0)
		{
			lbl.text = text;
		}
		else
		{
			GuiLabel guiLabel = lbl;
			guiLabel.text = string.Concat(guiLabel.text, "; ", text);
		}
	}

	private class HoverPatcher
	{
		public GuiButtonFixed icon;

		public GuiButton text;

		public string iconName;

		public HoverPatcher()
		{
		}
	}

	private class MemberListItem
	{
		public float offsetY;

		public GuiButtonFixed btnDelete;

		public GuiLabel lblRank;

		public GuiButtonFixed btnEdit;

		public GuiDropdown ddlRank;

		public string userName;

		public MemberListItem()
		{
		}
	}
}