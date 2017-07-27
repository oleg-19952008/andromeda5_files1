using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TransferableObjects;
using UnityEngine;

public class TransformerWindow : GuiWindow
{
	public static byte selectedTab;

	private byte selectedCurrency = 3;

	private byte selectedIntensity = 1;

	private byte selectedPortalIndex;

	private byte rewardScrollIndex;

	private GuiTexture mainTabTexture;

	private GuiLabel lblleftSideTitle;

	private GuiButtonResizeable btnTransform;

	private GuiButtonResizeable btnUseUltralibrium;

	private GuiButtonResizeable btnUseNova;

	private List<GuiElement> forDeleteBC = new List<GuiElement>();

	private GuiScrollingContainer galaxiesScroller;

	private List<GuiElement> forDeletePortalInfo = new List<GuiElement>();

	private List<GuiElement> forDeleteBank = new List<GuiElement>();

	private List<GuiElement> forDeleteRBP = new List<GuiElement>();

	private GuiTextureAnimated freeSpinBtnAnimationLeft1;

	private GuiTextureAnimated freeSpinBtnAnimationRight1;

	private GuiTextureAnimated freeSpinBtnAnimationMiddle1;

	private GuiTextureAnimated freeSpinBtnAnimationLeft2;

	private GuiTextureAnimated freeSpinBtnAnimationRight2;

	private GuiTextureAnimated freeSpinBtnAnimationMiddle2;

	private UniversalTransportContainer buyShipSlotData;

	private List<GuiElement> lockedInventorySlots = new List<GuiElement>();

	private int inventoryScrollIndex;

	private int inventorySlotCnt;

	private GuiButtonFixed scrollLeftButton;

	private GuiButtonFixed scrollRightButton;

	private GuiLabel scrollLabel;

	private ExpandSlotDialog expandSlotWindow;

	static TransformerWindow()
	{
		TransformerWindow.selectedTab = 1;
	}

	public TransformerWindow()
	{
	}

	private void ConfirmExpand(object prm)
	{
		playWebGame.udp.ExecuteCommand(PureUdpClient.CommandExpandSlots, this.buyShipSlotData, 49);
		int num = 0;
		NetworkScript.player.playerBelongings.ExpandInventory(ref num);
		this.inventorySlotCnt = NetworkScript.player.playerBelongings.playerInventorySlots;
		this.RefreshScrollButtons();
		Inventory.ClearSlots(this);
		this.CreateInventorySlots();
		this.CreateTrash();
		Inventory.DrawPlaces(this);
		this.UpdateDropZones();
		this.expandSlotWindow.Cancel();
	}

	public override void Create()
	{
		// 
		// Current member / type: System.Void TransformerWindow::Create()
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
		foreach (GuiElement guiElement in this.forDeleteBank)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.forDeleteBank.Clear();
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(33f, 427f, 170f, 24f),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 11,
			text = string.Empty
		};
		base.AddGuiElement(guiLabel);
		this.forDeleteBank.Add(guiLabel);
		GuiTexture guiTexture = new GuiTexture()
		{
			boundries = new Rect(46f, 469f, 24f, 26f)
		};
		guiTexture.SetTextureKeepSize("NewGUI", "ultralibrium_icon_big");
		base.AddGuiElement(guiTexture);
		this.forDeleteBank.Add(guiTexture);
		GuiLabel str = new GuiLabel()
		{
			boundries = new Rect(42f, 470f, 145f, 24f),
			Font = GuiLabel.FontBold,
			FontSize = 12,
			Alignment = 5,
			TextColor = GuiNewStyleBar.greenColor,
			text = string.Empty
		};
		base.AddGuiElement(str);
		this.forDeleteBank.Add(str);
		GuiTexture guiTexture1 = new GuiTexture()
		{
			boundries = new Rect(45f, 502f, 28f, 20f)
		};
		guiTexture1.SetTextureKeepSize("NewGUI", "nova_icon_big");
		base.AddGuiElement(guiTexture1);
		this.forDeleteBank.Add(guiTexture1);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(42f, 500f, 145f, 24f),
			Font = GuiLabel.FontBold,
			FontSize = 12,
			Alignment = 5,
			TextColor = GuiNewStyleBar.orangeColor,
			text = string.Empty
		};
		base.AddGuiElement(guiLabel1);
		this.forDeleteBank.Add(guiLabel1);
		if (TransformerWindow.selectedTab != 2)
		{
			guiLabel.text = StaticData.Translate("ep_screen_fortify_own_money");
			long ultralibrium = NetworkScript.player.playerBelongings.playerItems.get_Ultralibrium();
			str.text = ultralibrium.ToString("##,##0");
			long nova = NetworkScript.player.playerBelongings.playerItems.get_Nova();
			guiLabel1.text = nova.ToString("##,##0");
		}
		else
		{
			guiLabel.text = StaticData.Translate("ep_screen_fortify_guild_bank");
			str.text = NetworkScript.player.guild.bankUltralibrium.ToString("##,##0");
			guiLabel1.text = NetworkScript.player.guild.bankNova.ToString("##,##0");
		}
	}

	private void CreateContent()
	{
		foreach (GuiElement guiElement in this.forDeleteBC)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.forDeleteBC.Clear();
		this.CreateBank();
		this.CreateRightButtonPanel();
	}

	private void CreateForUnder30Level()
	{
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(278f, 39f, 550f, 30f),
			text = StaticData.Translate("key_transformer_main_lbl"),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 16
		};
		base.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(214f, 143f, 670f, 325f),
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 28,
			Alignment = 4,
			text = string.Format(StaticData.Translate("key_transformer_low_level"), 30)
		};
		base.AddGuiElement(guiLabel1);
	}

	private void CreateGalaxiesList()
	{
		if (this.galaxiesScroller != null)
		{
			this.galaxiesScroller.Claer();
			base.RemoveGuiElement(this.galaxiesScroller);
			this.galaxiesScroller = null;
		}
		this.galaxiesScroller = new GuiScrollingContainer(29f, 252f, 175f, 170f, 1, this);
		this.galaxiesScroller.SetArrowStep(34f);
		base.AddGuiElement(this.galaxiesScroller);
		for (int i = 0; i < (int)StaticData.allPortals.Length; i++)
		{
			GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
			guiButtonFixed.SetTexture("NewGUI", "BtnQuestNPC");
			guiButtonFixed.X = 2f;
			guiButtonFixed.Y = (float)(0 + 34 * i);
			guiButtonFixed.Caption = StaticData.Translate(StaticData.allPortals[i].uiName);
			guiButtonFixed.textColorNormal = GuiNewStyleBar.blueColor;
			guiButtonFixed.textColorDisabled = Color.get_grey();
			guiButtonFixed.textColorClick = GuiNewStyleBar.orangeColor;
			guiButtonFixed.textColorHover = Color.get_white();
			guiButtonFixed._marginLeft = 30;
			guiButtonFixed.groupId = 123;
			guiButtonFixed.behaviourKeepClicked = true;
			guiButtonFixed.eventHandlerParam.customData = StaticData.allPortals[i].portalId;
			guiButtonFixed.eventHandlerParam.customData2 = (byte)i;
			guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, TransformerWindow.CreatePortalInfo);
			guiButtonFixed.IsClicked = this.selectedPortalIndex == i;
			guiButtonFixed.groupId = 124;
			guiButtonFixed.MarginTop = -2;
			guiButtonFixed.Alignment = 3;
			guiButtonFixed.FontSize = 14;
			this.galaxiesScroller.AddContent(guiButtonFixed);
			GuiTexture guiTexture = new GuiTexture();
			if (!NetworkScript.player.playerBelongings.unlockedPortals.Contains(StaticData.allPortals[i].portalId))
			{
				guiTexture.SetTexture("NewGUI", "IconPadlock");
				guiTexture.boundries = new Rect(guiButtonFixed.X + 9f, guiButtonFixed.Y + 8f, 15f, 15f);
			}
			else
			{
				guiTexture.SetTexture("MinimapWindow", "portal_myfraction");
				guiTexture.boundries = new Rect(guiButtonFixed.X, guiButtonFixed.Y, 30f, 30f);
			}
			this.galaxiesScroller.AddContent(guiTexture);
		}
	}

	private void CreateInventorySlots()
	{
		TransformerWindow.<CreateInventorySlots>c__AnonStorey74 variable = null;
		foreach (GuiElement lockedInventorySlot in this.lockedInventorySlots)
		{
			base.RemoveGuiElement(lockedInventorySlot);
		}
		this.lockedInventorySlots.Clear();
		List<SlotItem> list = NetworkScript.player.playerBelongings.playerItems.slotItems;
		if (TransformerWindow.<>f__am$cache22 == null)
		{
			TransformerWindow.<>f__am$cache22 = new Func<SlotItem, bool>(null, (SlotItem t) => t.get_SlotType() == 1);
		}
		IEnumerable<SlotItem> enumerable = Enumerable.Where<SlotItem>(list, TransformerWindow.<>f__am$cache22);
		int num = this.inventoryScrollIndex;
		int num1 = this.inventorySlotCnt;
		ItemLocation itemLocation = 1;
		int num2 = num;
		int num3 = 0;
		while (num2 < num1)
		{
			if (num3 < 16)
			{
				AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = new AndromedaGuiDragDropPlace()
				{
					slot = (byte)num2
				};
				Inventory.places.Add(andromedaGuiDragDropPlace);
				andromedaGuiDragDropPlace.txFrameIdle = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotIdle");
				andromedaGuiDragDropPlace.position = new Vector2((float)(638 + num3 / 4 * 56), (float)(305 + num3 % 4 * 56));
				andromedaGuiDragDropPlace.dropZonePosition = andromedaGuiDragDropPlace.position;
				SlotItem slotItem = Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable, new Func<SlotItem, bool>(variable, (SlotItem si) => si.get_Slot() == this.i)));
				andromedaGuiDragDropPlace.isEmpty = slotItem == null;
				andromedaGuiDragDropPlace.item = slotItem;
				andromedaGuiDragDropPlace.txFrameDragSource = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotHvr");
				andromedaGuiDragDropPlace.txFrameDropTarget = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotActive");
				andromedaGuiDragDropPlace.txFrameDropTargetHover = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotHvr");
				andromedaGuiDragDropPlace.idleItemTextureSize = new Vector2(51f, 35f);
				andromedaGuiDragDropPlace.location = 1;
				num2++;
				num3++;
			}
			else
			{
				break;
			}
		}
		if (num1 - num < 16 && itemLocation != 3)
		{
			int num4 = num1 - num;
			this.DrawExpandInventorySlots((float)(635 + num4 / 4 * 56), (float)(303 + num4 % 4 * 56), itemLocation);
		}
	}

	private void CreatePortalInfo(EventHandlerParam prm)
	{
		TransformerWindow.<CreatePortalInfo>c__AnonStorey6F variable = null;
		TransformerWindow.<CreatePortalInfo>c__AnonStorey70 variable1 = null;
		EventHandlerParam eventHandlerParam;
		int num;
		this.selectedPortalIndex = (byte)prm.customData2;
		int num1 = (int)prm.customData;
		Portal portal = Enumerable.First<Portal>(Enumerable.Where<Portal>(StaticData.allPortals, new Func<Portal, bool>(variable, (Portal t) => t.portalId == this.portalId)));
		int num2 = 0;
		foreach (GuiElement guiElement in this.forDeletePortalInfo)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.forDeletePortalInfo.Clear();
		this.lblleftSideTitle.text = StaticData.Translate(portal.uiName);
		this.DrawPortal(num1);
		Inventory.window = this;
		Inventory.isRightClickActionEnable = true;
		Inventory.ConfigWndAfterRightClkAction = null;
		Inventory.isVaultMenuOpen = false;
		Inventory.isItemRerollMenuOpen = false;
		Inventory.isGuildVaultMenuOpen = false;
		Inventory.activatePortalPartDrop = new Action<int>(this, TransformerWindow.OnPortalPartZoneDrop);
		Inventory.secondaryDropHandler = new Action<AndromedaGuiDragDropPlace, AndromedaGuiDragDropPlace>(this, TransformerWindow.DropHandler);
		Inventory.closeStackablePopUp = new Action(this, TransformerWindow.Populate);
		Inventory.ClearSlots(this);
		this.CreateInventorySlots();
		this.CreateTrash();
		Inventory.DrawPlaces(this);
		this.UpdateDropZones();
		for (int i = 0; i < portal.parts.get_Count(); i++)
		{
			ushort item = portal.parts.get_Keys().get_Item(i);
			float single = (float)(220 + i / 4 * 330);
			float single1 = (float)(178 + i % 4 * 90);
			PortalPart portalPart = Enumerable.FirstOrDefault<PortalPart>(Enumerable.Where<PortalPart>(NetworkScript.player.playerBelongings.playerItems.portalParts, new Func<PortalPart, bool>(variable1, (PortalPart t) => (t.portalId != this.<>f__ref$111.portalId ? false : t.partTypeId == this.partType))));
			if (portalPart == null)
			{
				num = 0;
			}
			else
			{
				num = portalPart.partAmount;
			}
			int num3 = num;
			num3 = Math.Min(num3, portal.parts.get_Item(item));
			num2 = num2 + num3;
			GuiTexture guiTexture = new GuiTexture();
			guiTexture.SetTexture("FrameworkGUI", "empty");
			guiTexture.boundries = new Rect(single, single1 - 55f, 50f, 70f);
			eventHandlerParam = new EventHandlerParam()
			{
				customData = string.Format("{3}\n{0}  {1}  {2}", new object[] { num3, StaticData.Translate("key_ship_cfg_tab_separator"), portal.parts.get_Item(item), StaticData.Translate(StaticData.allTypes.get_Item(item).uiName) }),
				customData2 = guiTexture
			};
			guiTexture.tooltipWindowParam = eventHandlerParam;
			guiTexture.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			base.AddGuiElement(guiTexture);
			this.forDeletePortalInfo.Add(guiTexture);
			GuiTexture guiTexture1 = new GuiTexture();
			guiTexture1.SetTexture("ConfigWindow", "InventorySlotIdle");
			guiTexture1.X = single;
			guiTexture1.Y = single1 - 55f;
			base.AddGuiElement(guiTexture1);
			this.forDeletePortalInfo.Add(guiTexture1);
			GuiTexture guiTexture2 = new GuiTexture()
			{
				boundries = new Rect(single, single1 - 48f, 51f, 35f)
			};
			guiTexture2.SetTextureKeepSize("PortalPartsAvatars", StaticData.allTypes.get_Item(item).assetName);
			base.AddGuiElement(guiTexture2);
			this.forDeletePortalInfo.Add(guiTexture2);
			GuiNewStyleBar guiNewStyleBar = new GuiNewStyleBar();
			guiNewStyleBar.SetCustumSizeBlueBar(50);
			guiNewStyleBar.maximum = (float)portal.parts.get_Item(item);
			guiNewStyleBar.current = (float)num3;
			guiNewStyleBar.boundries.set_x(single);
			guiNewStyleBar.boundries.set_y(single1);
			base.AddGuiElement(guiNewStyleBar);
			this.forDeletePortalInfo.Add(guiNewStyleBar);
		}
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(287f, 108f, 245f, 20f),
			text = StaticData.Translate("key_transformer_drag_and_drop"),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 12
		};
		base.AddGuiElement(guiLabel);
		this.forDeletePortalInfo.Add(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(290f, 445f, 245f, 20f),
			text = string.Format(StaticData.Translate("key_transformer_max_level_unlock"), "+2"),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 12
		};
		base.AddGuiElement(guiLabel1);
		this.forDeletePortalInfo.Add(guiLabel1);
		GuiNewStyleBar guiNewStyleBar1 = new GuiNewStyleBar();
		guiNewStyleBar1.SetCustumSizeBlueBar(240);
		GuiNewStyleBar guiNewStyleBar2 = guiNewStyleBar1;
		SortedList<ushort, short> sortedList = portal.parts;
		if (TransformerWindow.<>f__am$cache1F == null)
		{
			TransformerWindow.<>f__am$cache1F = new Func<KeyValuePair<ushort, short>, int>(null, (KeyValuePair<ushort, short> t) => t.get_Value());
		}
		guiNewStyleBar2.maximum = (float)Enumerable.Sum<KeyValuePair<ushort, short>>(sortedList, TransformerWindow.<>f__am$cache1F);
		guiNewStyleBar1.current = (float)num2;
		guiNewStyleBar1.boundries.set_x(217f);
		guiNewStyleBar1.boundries.set_y(478f);
		base.AddGuiElement(guiNewStyleBar1);
		this.forDeletePortalInfo.Add(guiNewStyleBar1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(458f, 474f, 100f, 24f),
			text = string.Format(StaticData.Translate("key_transformer_revealed"), guiNewStyleBar1.current, guiNewStyleBar1.maximum),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		base.AddGuiElement(guiLabel2);
		this.forDeletePortalInfo.Add(guiLabel2);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.SetSmallBlueTexture();
		guiButtonResizeable.X = 230f;
		guiButtonResizeable.Y = 500f;
		guiButtonResizeable.boundries.set_width(310f);
		guiButtonResizeable.MarginTop = -2;
		guiButtonResizeable.Alignment = 4;
		guiButtonResizeable.FontSize = 14;
		guiButtonResizeable.isEnabled = false;
		base.AddGuiElement(guiButtonResizeable);
		this.forDeletePortalInfo.Add(guiButtonResizeable);
		if (!NetworkScript.player.playerBelongings.playerItems.HaveAllPartsForPortal(num1))
		{
			guiButtonResizeable.isEnabled = false;
			if (!NetworkScript.player.playerBelongings.unlockedPortals.Contains(num1))
			{
				GuiTexture x = new GuiTexture();
				x.SetTexture("NewGUI", "ep_ultralibtium");
				x.X = guiButtonResizeable.X + 8f;
				x.Y = guiButtonResizeable.Y + 4f;
				base.AddGuiElement(x);
				this.forDeletePortalInfo.Add(x);
				if (TransformerWindow.selectedTab != 1)
				{
					guiButtonResizeable.Caption = string.Format(StaticData.Translate("key_transformer_btn_unlock_for_guild"), 1000);
					guiButtonResizeable.isEnabled = (!NetworkScript.player.guildMember.rank.canBank ? false : NetworkScript.player.guild.bankUltralibrium >= (long)1000);
				}
				else
				{
					guiButtonResizeable.Caption = string.Format(StaticData.Translate("key_transformer_btn_unlock_for_player"), 200);
				}
			}
			else
			{
				guiButtonResizeable.Caption = StaticData.Translate("key_transformer_btn_jump");
			}
		}
		else if (!NetworkScript.player.playerBelongings.unlockedPortals.Contains(num1))
		{
			GuiTexture y = new GuiTexture();
			y.SetTexture("NewGUI", "ep_ultralibtium");
			y.X = guiButtonResizeable.X + 8f;
			y.Y = guiButtonResizeable.Y + 4f;
			base.AddGuiElement(y);
			this.forDeletePortalInfo.Add(y);
			if (TransformerWindow.selectedTab != 1)
			{
				guiButtonResizeable.Caption = string.Format(StaticData.Translate("key_transformer_btn_unlock_for_guild"), 1000);
				guiButtonResizeable.isEnabled = (!NetworkScript.player.guildMember.rank.canBank ? false : NetworkScript.player.guild.bankUltralibrium >= (long)1000);
				if (!NetworkScript.player.guildMember.rank.canBank)
				{
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_guild_no_bank_permission_tooltip"),
						customData2 = guiButtonResizeable
					};
					guiButtonResizeable.tooltipWindowParam = eventHandlerParam;
					guiButtonResizeable.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				}
			}
			else
			{
				guiButtonResizeable.Caption = string.Format(StaticData.Translate("key_transformer_btn_unlock_for_player"), 200);
				guiButtonResizeable.isEnabled = NetworkScript.player.playerBelongings.playerItems.get_Ultralibrium() >= (long)200;
			}
			guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, TransformerWindow.UnlockPortal);
		}
		else
		{
			guiButtonResizeable.isEnabled = NetworkScript.player.vessel.galaxy.get_galaxyId() < 4000;
			if (NetworkScript.player.vessel.galaxy.galaxyKey != portal.galaxyKey)
			{
				guiButtonResizeable.Caption = StaticData.Translate("key_transformer_btn_jump");
				guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, TransformerWindow.Jump);
			}
			else
			{
				guiButtonResizeable.Caption = StaticData.Translate("key_transformer_btn_jump_back");
				guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, TransformerWindow.JumpBack);
			}
		}
	}

	private void CreateRightButtonPanel()
	{
		List<ushort> list;
		EventHandlerParam eventHandlerParam;
		GuiButtonResizeable guiButtonResizeable;
		GuiButtonResizeable guiButtonResizeable1;
		bool ticks = false;
		if (ticks && this.selectedIntensity != 1)
		{
			this.selectedIntensity = 1;
		}
		foreach (GuiElement guiElement in this.forDeleteRBP)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.forDeleteRBP.Clear();
		if (TransformerWindow.selectedTab != 1)
		{
			list = StaticData.transformersGuildRewards;
		}
		else
		{
			list = StaticData.transformersPersonalRewards;
			ticks = NetworkScript.player.playerBelongings.nextFreeTransformerUsage < StaticData.now.get_Ticks();
			if (ticks && this.selectedIntensity != 1)
			{
				this.selectedIntensity = 1;
			}
		}
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("ConfigWnd", "ScrollArrowLeft");
		guiButtonFixed.boundries = new Rect(614f, 129f, 15f, 36f);
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, TransformerWindow.OnBtnMinusClicked);
		guiButtonFixed.SetLeftClickSound("Sounds", "minus");
		base.AddGuiElement(guiButtonFixed);
		this.forDeleteRBP.Add(guiButtonFixed);
		GuiButtonFixed rect = new GuiButtonFixed();
		rect.SetTexture("ConfigWnd", "ScrollArrowRight");
		rect.boundries = new Rect(864f, 129f, 15f, 36f);
		rect.Caption = string.Empty;
		rect.Clicked = new Action<EventHandlerParam>(this, TransformerWindow.OnBtnPlusClicked);
		rect.SetLeftClickSound("Sounds", "plus");
		base.AddGuiElement(rect);
		this.forDeleteRBP.Add(rect);
		for (int i = 0; i < Math.Min(6, list.get_Count() - this.rewardScrollIndex); i++)
		{
			int num = i + this.rewardScrollIndex;
			GuiTexture guiTexture = new GuiTexture()
			{
				boundries = new Rect(guiButtonFixed.X + 13f + (float)(i % 6 * 40), (float)(127 + i / 6 * 30), 38f, 38f)
			};
			guiTexture.SetTextureKeepSize("MainScreenWindow", "weapons_slot");
			base.AddGuiElement(guiTexture);
			this.forDeleteRBP.Add(guiTexture);
			GuiTexture drawTooltipWindow = new GuiTexture();
			drawTooltipWindow.SetItemTexture(list.get_Item(num));
			if (StaticData.allTypes.ContainsKey(list.get_Item(num)))
			{
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate(StaticData.allTypes.get_Item(list.get_Item(num)).uiName),
					customData2 = drawTooltipWindow
				};
				drawTooltipWindow.tooltipWindowParam = eventHandlerParam;
				drawTooltipWindow.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			}
			drawTooltipWindow.boundries = new Rect(guiButtonFixed.X + 18f + (float)(i % 6 * 40), (float)(137 + i / 6 * 30), 28f, 18f);
			base.AddGuiElement(drawTooltipWindow);
			this.forDeleteRBP.Add(drawTooltipWindow);
		}
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(612f, 104f, 270f, 20f),
			text = StaticData.Translate("key_transformer_rewards"),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 11
		};
		base.AddGuiElement(guiLabel);
		this.forDeleteRBP.Add(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(691f, 163f, 120f, 20f),
			text = StaticData.Translate("key_transformer_intensity"),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 11
		};
		base.AddGuiElement(guiLabel1);
		this.forDeleteRBP.Add(guiLabel1);
		GuiButtonResizeable action = new GuiButtonResizeable();
		action.SetSmallBlueTexture();
		action.X = 691f;
		action.Y = 185f;
		action.boundries.set_width(32f);
		action.MarginTop = -2;
		action.Caption = "1x";
		action.Alignment = 4;
		action.FontSize = 12;
		action.eventHandlerParam.customData = (byte)1;
		action.Clicked = new Action<EventHandlerParam>(this, TransformerWindow.OnChangeIntensityClicked);
		base.AddGuiElement(action);
		this.forDeleteRBP.Add(action);
		GuiButtonResizeable action1 = new GuiButtonResizeable();
		action1.SetSmallBlueTexture();
		action1.X = 731f;
		action1.Y = 185f;
		action1.boundries.set_width(32f);
		action1.MarginTop = -2;
		action1.Caption = "2x";
		action1.Alignment = 4;
		action1.FontSize = 12;
		action1.eventHandlerParam.customData = (byte)2;
		action1.Clicked = new Action<EventHandlerParam>(this, TransformerWindow.OnChangeIntensityClicked);
		action1.isEnabled = !ticks;
		base.AddGuiElement(action1);
		this.forDeleteRBP.Add(action1);
		GuiButtonResizeable guiButtonResizeable2 = new GuiButtonResizeable();
		guiButtonResizeable2.SetSmallBlueTexture();
		guiButtonResizeable2.X = 771f;
		guiButtonResizeable2.Y = 185f;
		guiButtonResizeable2.boundries.set_width(32f);
		guiButtonResizeable2.MarginTop = -2;
		guiButtonResizeable2.Caption = "4x";
		guiButtonResizeable2.Alignment = 4;
		guiButtonResizeable2.FontSize = 12;
		guiButtonResizeable2.eventHandlerParam.customData = (byte)4;
		guiButtonResizeable2.Clicked = new Action<EventHandlerParam>(this, TransformerWindow.OnChangeIntensityClicked);
		guiButtonResizeable2.isEnabled = !ticks;
		base.AddGuiElement(guiButtonResizeable2);
		this.forDeleteRBP.Add(guiButtonResizeable2);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(618f, 214f, 257f, 20f),
			text = StaticData.Translate("key_transformer_transform"),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 11
		};
		base.AddGuiElement(guiLabel2);
		this.forDeleteRBP.Add(guiLabel2);
		int num1 = (!ticks ? PlayerItems.GetTransformPrice(1, (TransformerWindow.selectedTab != 1 ? 2 : 1), this.selectedIntensity) : 0);
		int num2 = (!ticks ? PlayerItems.GetTransformPrice(3, (TransformerWindow.selectedTab != 1 ? 2 : 1), this.selectedIntensity) : 0);
		this.btnUseUltralibrium = new GuiButtonResizeable();
		this.btnUseUltralibrium.SetSmallBlueTexture();
		this.btnUseUltralibrium.X = 617f;
		this.btnUseUltralibrium.Y = 238f;
		this.btnUseUltralibrium.boundries.set_width(120f);
		this.btnUseUltralibrium.MarginTop = -2;
		this.btnUseUltralibrium.Alignment = 4;
		this.btnUseUltralibrium.FontSize = 12;
		this.btnUseUltralibrium.Caption = (num1 != 0 ? num2.ToString("##,##0") : StaticData.Translate("key_transformer_free_usage"));
		this.btnUseUltralibrium.eventHandlerParam.customData = (SelectedCurrency)3;
		this.btnUseUltralibrium.Clicked = new Action<EventHandlerParam>(this, TransformerWindow.UseTransformer);
		base.AddGuiElement(this.btnUseUltralibrium);
		this.forDeleteRBP.Add(this.btnUseUltralibrium);
		if (TransformerWindow.selectedTab != 1)
		{
			this.btnUseUltralibrium.isEnabled = (NetworkScript.player.guildMember == null || !NetworkScript.player.guildMember.rank.canBank ? false : NetworkScript.player.guild.bankUltralibrium >= (long)num2);
		}
		else
		{
			this.btnUseUltralibrium.isEnabled = NetworkScript.player.playerBelongings.playerItems.get_Ultralibrium() >= (long)num2;
		}
		this.btnUseNova = new GuiButtonResizeable();
		this.btnUseNova.SetSmallBlueTexture();
		this.btnUseNova.X = 758f;
		this.btnUseNova.Y = 238f;
		this.btnUseNova.boundries.set_width(120f);
		this.btnUseNova.MarginTop = -2;
		this.btnUseNova.Alignment = 4;
		this.btnUseNova.FontSize = 12;
		this.btnUseNova.Caption = (num1 != 0 ? num1.ToString("##,##0") : StaticData.Translate("key_transformer_free_usage"));
		this.btnUseNova.eventHandlerParam.customData = (SelectedCurrency)1;
		this.btnUseNova.Clicked = new Action<EventHandlerParam>(this, TransformerWindow.UseTransformer);
		base.AddGuiElement(this.btnUseNova);
		this.forDeleteRBP.Add(this.btnUseNova);
		if (TransformerWindow.selectedTab != 1)
		{
			this.btnUseNova.isEnabled = (!NetworkScript.player.guildMember.rank.canBank ? false : NetworkScript.player.guild.bankNova >= (long)num1);
		}
		else
		{
			this.btnUseNova.isEnabled = NetworkScript.player.playerBelongings.playerItems.get_Nova() >= (long)num1;
		}
		if (num1 == 0)
		{
			this.PutFreeSpinAnimation();
		}
		GuiTexture x = new GuiTexture();
		x.SetTexture("NewGUI", "ep_ultralibtium");
		x.X = this.btnUseUltralibrium.X + 8f;
		x.Y = this.btnUseUltralibrium.Y + 4f;
		base.AddGuiElement(x);
		this.forDeleteRBP.Add(x);
		GuiTexture y = new GuiTexture();
		y.SetTexture("NewGUI", "ep_icon_nova");
		y.X = this.btnUseNova.X + 7f;
		y.Y = this.btnUseNova.Y + 6f;
		base.AddGuiElement(y);
		this.forDeleteRBP.Add(y);
		switch (this.selectedIntensity)
		{
			case 1:
			{
				action.SetSmallOrangeTexture();
				if (TransformerWindow.selectedTab == 2 && NetworkScript.player.guildMember != null && !NetworkScript.player.guildMember.rank.canBank)
				{
					guiButtonResizeable = this.btnUseNova;
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_guild_no_bank_permission_tooltip"),
						customData2 = this.btnUseNova
					};
					guiButtonResizeable.tooltipWindowParam = eventHandlerParam;
					this.btnUseNova.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
					guiButtonResizeable1 = this.btnUseUltralibrium;
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_guild_no_bank_permission_tooltip"),
						customData2 = this.btnUseUltralibrium
					};
					guiButtonResizeable1.tooltipWindowParam = eventHandlerParam;
					this.btnUseUltralibrium.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				}
				return;
			}
			case 2:
			{
				action1.SetSmallOrangeTexture();
				if (TransformerWindow.selectedTab == 2 && NetworkScript.player.guildMember != null && !NetworkScript.player.guildMember.rank.canBank)
				{
					guiButtonResizeable = this.btnUseNova;
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_guild_no_bank_permission_tooltip"),
						customData2 = this.btnUseNova
					};
					guiButtonResizeable.tooltipWindowParam = eventHandlerParam;
					this.btnUseNova.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
					guiButtonResizeable1 = this.btnUseUltralibrium;
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_guild_no_bank_permission_tooltip"),
						customData2 = this.btnUseUltralibrium
					};
					guiButtonResizeable1.tooltipWindowParam = eventHandlerParam;
					this.btnUseUltralibrium.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				}
				return;
			}
			case 3:
			{
				if (TransformerWindow.selectedTab == 2 && NetworkScript.player.guildMember != null && !NetworkScript.player.guildMember.rank.canBank)
				{
					guiButtonResizeable = this.btnUseNova;
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_guild_no_bank_permission_tooltip"),
						customData2 = this.btnUseNova
					};
					guiButtonResizeable.tooltipWindowParam = eventHandlerParam;
					this.btnUseNova.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
					guiButtonResizeable1 = this.btnUseUltralibrium;
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_guild_no_bank_permission_tooltip"),
						customData2 = this.btnUseUltralibrium
					};
					guiButtonResizeable1.tooltipWindowParam = eventHandlerParam;
					this.btnUseUltralibrium.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				}
				return;
			}
			case 4:
			{
				guiButtonResizeable2.SetSmallOrangeTexture();
				if (TransformerWindow.selectedTab == 2 && NetworkScript.player.guildMember != null && !NetworkScript.player.guildMember.rank.canBank)
				{
					guiButtonResizeable = this.btnUseNova;
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_guild_no_bank_permission_tooltip"),
						customData2 = this.btnUseNova
					};
					guiButtonResizeable.tooltipWindowParam = eventHandlerParam;
					this.btnUseNova.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
					guiButtonResizeable1 = this.btnUseUltralibrium;
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_guild_no_bank_permission_tooltip"),
						customData2 = this.btnUseUltralibrium
					};
					guiButtonResizeable1.tooltipWindowParam = eventHandlerParam;
					this.btnUseUltralibrium.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				}
				return;
			}
			default:
			{
				if (TransformerWindow.selectedTab == 2 && NetworkScript.player.guildMember != null && !NetworkScript.player.guildMember.rank.canBank)
				{
					guiButtonResizeable = this.btnUseNova;
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_guild_no_bank_permission_tooltip"),
						customData2 = this.btnUseNova
					};
					guiButtonResizeable.tooltipWindowParam = eventHandlerParam;
					this.btnUseNova.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
					guiButtonResizeable1 = this.btnUseUltralibrium;
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_guild_no_bank_permission_tooltip"),
						customData2 = this.btnUseUltralibrium
					};
					guiButtonResizeable1.tooltipWindowParam = eventHandlerParam;
					this.btnUseUltralibrium.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				}
				return;
			}
		}
	}

	private void CreateTrash()
	{
		AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = new AndromedaGuiDragDropPlace();
		SlotItem slotItem = null;
		andromedaGuiDragDropPlace.location = 14;
		andromedaGuiDragDropPlace.position = new Vector2(560f, 470f);
		andromedaGuiDragDropPlace.dropZonePosition = andromedaGuiDragDropPlace.position;
		andromedaGuiDragDropPlace.txFrameIdle = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "binIdle");
		andromedaGuiDragDropPlace.txFrameDragSource = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "binHvr");
		andromedaGuiDragDropPlace.txFrameDropTarget = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "binActive");
		andromedaGuiDragDropPlace.txFrameDropTargetHover = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "binHvr");
		andromedaGuiDragDropPlace.item = slotItem;
		andromedaGuiDragDropPlace.isEmpty = slotItem == null;
		andromedaGuiDragDropPlace.idleItemTextureSize = new Vector2(49f, 64f);
		Inventory.places.Add(andromedaGuiDragDropPlace);
	}

	private void DrawExpandInventorySlots(float x, float y, ItemLocation location)
	{
		for (int i = 0; i < 4; i++)
		{
			GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
			if (i != 0)
			{
				GuiTexture guiTexture = new GuiTexture();
				guiTexture.SetTexture("ConfigWindow", "inventory_slot_locked");
				guiTexture.boundries = new Rect(x + (float)(i / 4 * 56), y + (float)(i % 4 * 56), 55f, 55f);
				base.AddGuiElement(guiTexture);
				this.lockedInventorySlots.Add(guiTexture);
			}
			else
			{
				guiButtonFixed.SetTexture("ConfigWindow", "inventory_slot_plus");
				guiButtonFixed.boundries = new Rect(x + (float)(i / 4 * 56), y + (float)(i % 4 * 58), 56f, 55f);
				guiButtonFixed.Caption = string.Empty;
				base.AddGuiElement(guiButtonFixed);
				guiButtonFixed.hoverParam = location;
				guiButtonFixed.Hovered = new Action<object, bool>(this, TransformerWindow.OnExpandBtnHover);
				guiButtonFixed.eventHandlerParam.customData = location;
				guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, TransformerWindow.OnExpandBtnClicked);
				guiButtonFixed.tooltipWindowParam = new Rect(x + (float)(i / 4 * 56), y + (float)(i % 4 * 56), 150f, 70f);
				this.lockedInventorySlots.Add(guiButtonFixed);
				guiButtonFixed.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.DrawExpandInventorySlotsTooltip);
			}
		}
		this.RefreshExpandBtnState();
	}

	private GuiWindow DrawExpandInventorySlotsTooltip(object prm)
	{
		Rect rect = (Rect)prm;
		GuiWindow guiWindow = new GuiWindow()
		{
			zOrder = 230,
			ignoreClickEvents = true
		};
		guiWindow.SetBackgroundTexture("ConfigWindow", "boostersTooltip");
		guiWindow.isHidden = false;
		guiWindow.boundries.set_x(rect.get_x() - 45f);
		guiWindow.boundries.set_y(rect.get_y() - 65f);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = StaticData.Translate("key_ship_cfg_expand_inv_lbl").ToUpper(),
			boundries = new Rect(10f, 4f, 130f, 20f),
			FontSize = 10,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 4
		};
		guiWindow.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			text = string.Format(StaticData.Translate("key_ship_cfg_expand_inv_details"), Enumerable.First<SlotPriceInfo>(Enumerable.Where<SlotPriceInfo>(StaticData.slotPriceInformation, new Func<SlotPriceInfo, bool>(this, (SlotPriceInfo s) => (s.slotId != this.inventorySlotCnt + 4 ? false : s.slotType == "Inventory")))).priceNova),
			boundries = new Rect(10f, 24f, 130f, 45f),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		guiWindow.AddGuiElement(guiLabel1);
		return guiWindow;
	}

	private void DrawPortal(int portalId)
	{
		TransformerWindow.<DrawPortal>c__AnonStorey71 variable = null;
		float single = 240f;
		float single1 = 120f;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("Targeting", string.Concat(Enumerable.First<Portal>(Enumerable.Where<Portal>(StaticData.allPortals, new Func<Portal, bool>(variable, (Portal p) => p.portalId == this.portalId))).assetName, "_avatar"));
		guiTexture.X = 67f;
		guiTexture.Y = 109f;
		base.AddGuiElement(guiTexture);
		this.forDeletePortalInfo.Add(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("Portals", string.Format("portal_{0}_silhouette", portalId));
		guiTexture1.X = single;
		guiTexture1.Y = single1;
		base.AddGuiElement(guiTexture1);
		this.forDeletePortalInfo.Add(guiTexture1);
		List<PortalPart> list = Enumerable.ToList<PortalPart>(Enumerable.Where<PortalPart>(NetworkScript.player.playerBelongings.playerItems.portalParts, new Func<PortalPart, bool>(variable, (PortalPart t) => t.portalId == this.portalId)));
		if (Enumerable.FirstOrDefault<PortalPart>(Enumerable.Where<PortalPart>(list, new Func<PortalPart, bool>(variable, (PortalPart t) => t.partTypeId == 12000 + this.portalId * 10))) != null)
		{
			GuiTexture guiTexture2 = new GuiTexture();
			guiTexture2.SetTexture("Portals", string.Format("portal_{0}_{1}", portalId, 12000 + portalId * 10));
			guiTexture2.X = single;
			guiTexture2.Y = single1;
			base.AddGuiElement(guiTexture2);
			this.forDeletePortalInfo.Add(guiTexture2);
		}
		if (Enumerable.FirstOrDefault<PortalPart>(Enumerable.Where<PortalPart>(list, new Func<PortalPart, bool>(variable, (PortalPart t) => t.partTypeId == 12004 + this.portalId * 10))) != null)
		{
			GuiTexture guiTexture3 = new GuiTexture();
			guiTexture3.SetTexture("Portals", string.Format("portal_{0}_{1}", portalId, 12004 + portalId * 10));
			guiTexture3.X = single;
			guiTexture3.Y = single1;
			base.AddGuiElement(guiTexture3);
			this.forDeletePortalInfo.Add(guiTexture3);
		}
		if (Enumerable.FirstOrDefault<PortalPart>(Enumerable.Where<PortalPart>(list, new Func<PortalPart, bool>(variable, (PortalPart t) => t.partTypeId == 12005 + this.portalId * 10))) != null)
		{
			GuiTexture guiTexture4 = new GuiTexture();
			guiTexture4.SetTexture("Portals", string.Format("portal_{0}_{1}", portalId, 12005 + portalId * 10));
			guiTexture4.X = single;
			guiTexture4.Y = single1;
			base.AddGuiElement(guiTexture4);
			this.forDeletePortalInfo.Add(guiTexture4);
		}
		if (Enumerable.FirstOrDefault<PortalPart>(Enumerable.Where<PortalPart>(list, new Func<PortalPart, bool>(variable, (PortalPart t) => t.partTypeId == 12006 + this.portalId * 10))) != null)
		{
			GuiTexture guiTexture5 = new GuiTexture();
			guiTexture5.SetTexture("Portals", string.Format("portal_{0}_{1}", portalId, 12006 + portalId * 10));
			guiTexture5.X = single;
			guiTexture5.Y = single1;
			base.AddGuiElement(guiTexture5);
			this.forDeletePortalInfo.Add(guiTexture5);
		}
		if (Enumerable.FirstOrDefault<PortalPart>(Enumerable.Where<PortalPart>(list, new Func<PortalPart, bool>(variable, (PortalPart t) => t.partTypeId == 12001 + this.portalId * 10))) != null)
		{
			GuiTexture guiTexture6 = new GuiTexture();
			guiTexture6.SetTexture("Portals", string.Format("portal_{0}_{1}", portalId, 12001 + portalId * 10));
			guiTexture6.X = single;
			guiTexture6.Y = single1;
			base.AddGuiElement(guiTexture6);
			this.forDeletePortalInfo.Add(guiTexture6);
		}
		if (Enumerable.FirstOrDefault<PortalPart>(Enumerable.Where<PortalPart>(list, new Func<PortalPart, bool>(variable, (PortalPart t) => t.partTypeId == 12002 + this.portalId * 10))) != null)
		{
			GuiTexture guiTexture7 = new GuiTexture();
			guiTexture7.SetTexture("Portals", string.Format("portal_{0}_{1}", portalId, 12002 + portalId * 10));
			guiTexture7.X = single;
			guiTexture7.Y = single1;
			base.AddGuiElement(guiTexture7);
			this.forDeletePortalInfo.Add(guiTexture7);
		}
		if (Enumerable.FirstOrDefault<PortalPart>(Enumerable.Where<PortalPart>(list, new Func<PortalPart, bool>(variable, (PortalPart t) => t.partTypeId == 12003 + this.portalId * 10))) != null)
		{
			GuiTexture guiTexture8 = new GuiTexture();
			guiTexture8.SetTexture("Portals", string.Format("portal_{0}_{1}", portalId, 12003 + portalId * 10));
			guiTexture8.X = single;
			guiTexture8.Y = single1;
			base.AddGuiElement(guiTexture8);
			this.forDeletePortalInfo.Add(guiTexture8);
		}
		if (NetworkScript.player.playerBelongings.playerItems.HaveAllPartsForPortal(portalId) && NetworkScript.player.playerBelongings.unlockedPortals.Contains(portalId))
		{
			GuiTexture guiTexture9 = new GuiTexture();
			guiTexture9.SetTexture("Portals", string.Format("portal_{0}_activated", portalId));
			guiTexture9.X = single;
			guiTexture9.Y = single1;
			base.AddGuiElement(guiTexture9);
			this.forDeletePortalInfo.Add(guiTexture9);
		}
	}

	public void DropHandler(AndromedaGuiDragDropPlace src, AndromedaGuiDragDropPlace dst)
	{
		this.Populate();
	}

	private void Jump(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void TransformerWindow::Jump(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void Jump(EventHandlerParam)
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

	private void JumpBack(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void TransformerWindow::JumpBack(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void JumpBack(EventHandlerParam)
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

	private void OnBtnMinusClicked(object prm)
	{
		byte num = this.rewardScrollIndex;
		this.rewardScrollIndex = (byte)Math.Max(0, this.rewardScrollIndex - 1);
		if (num != this.rewardScrollIndex)
		{
			this.CreateRightButtonPanel();
		}
	}

	private void OnBtnPlusClicked(object prm)
	{
		List<ushort> list;
		byte num = this.rewardScrollIndex;
		list = (TransformerWindow.selectedTab != 1 ? StaticData.transformersGuildRewards : StaticData.transformersPersonalRewards);
		int num1 = Math.Max(0, list.get_Count() - 6);
		this.rewardScrollIndex = (byte)Math.Min(this.rewardScrollIndex + 1, num1);
		if (num != this.rewardScrollIndex)
		{
			this.CreateRightButtonPanel();
		}
	}

	private void OnChangeIntensityClicked(EventHandlerParam prm)
	{
		this.selectedIntensity = (byte)prm.customData;
		this.CreateRightButtonPanel();
	}

	private void OnChangeTabClicked(EventHandlerParam prm)
	{
		this.selectedCurrency = 3;
		this.selectedIntensity = 1;
		this.rewardScrollIndex = 0;
		TransformerWindow.selectedTab = (byte)prm.customData;
		this.mainTabTexture.SetTexture("ConfigWnd", string.Concat("TransformerTab_", TransformerWindow.selectedTab.ToString()));
		this.CreateContent();
		this.Populate();
	}

	public override void OnClose()
	{
		// 
		// Current member / type: System.Void TransformerWindow::OnClose()
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

	private void OnExpandBtnClicked(EventHandlerParam prm)
	{
		this.buyShipSlotData = new UniversalTransportContainer();
		this.expandSlotWindow = new ExpandSlotDialog()
		{
			ConfirmExpand = new Action<EventHandlerParam>(this, TransformerWindow.ConfirmExpand)
		};
		int num = 0;
		num = (this.inventorySlotCnt <= 36 ? Enumerable.First<SlotPriceInfo>(Enumerable.Where<SlotPriceInfo>(StaticData.slotPriceInformation, new Func<SlotPriceInfo, bool>(this, (SlotPriceInfo s) => (s.slotId != this.inventorySlotCnt + 4 ? false : s.slotType == "Inventory")))).priceNova : 999999999);
		this.buyShipSlotData.wantedSlot = 7;
		this.expandSlotWindow.CreateExpandDialog(num, 1, StaticData.Translate("key_ship_cfg_expand_modal"), AndromedaGui.gui);
	}

	private void OnExpandBtnHover(object prm, bool state)
	{
		if (!state)
		{
			foreach (GuiElement lockedInventorySlot in this.lockedInventorySlots)
			{
				if (!(lockedInventorySlot is GuiTexture))
				{
					continue;
				}
				((GuiTexture)lockedInventorySlot).SetTextureKeepSize("ConfigWindow", "inventory_slot_locked");
			}
		}
		else
		{
			foreach (GuiElement guiElement in this.lockedInventorySlots)
			{
				if (!(guiElement is GuiTexture))
				{
					continue;
				}
				((GuiTexture)guiElement).SetTextureKeepSize("ConfigWindow", "inventory_slot_lockedHvr");
			}
		}
	}

	private void OnPortalPartZoneDrop(int prm)
	{
		// 
		// Current member / type: System.Void TransformerWindow::OnPortalPartZoneDrop(System.Int32)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnPortalPartZoneDrop(System.Int32)
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

	public void OnScrollDown(EventHandlerParam parm)
	{
		TransformerWindow transformerWindow = this;
		transformerWindow.inventoryScrollIndex = transformerWindow.inventoryScrollIndex + 4;
		NetworkScript.player.shipScript.transformerInventoryScrollIndex = this.inventoryScrollIndex;
		this.RefreshScrollButtons();
		Inventory.ClearSlots(this);
		this.CreateInventorySlots();
		this.CreateTrash();
		Inventory.DrawPlaces(this);
		this.UpdateDropZones();
	}

	public void OnScrollUp(EventHandlerParam parm)
	{
		TransformerWindow transformerWindow = this;
		transformerWindow.inventoryScrollIndex = transformerWindow.inventoryScrollIndex - 4;
		NetworkScript.player.shipScript.transformerInventoryScrollIndex = this.inventoryScrollIndex;
		this.RefreshScrollButtons();
		Inventory.ClearSlots(this);
		this.CreateInventorySlots();
		this.CreateTrash();
		Inventory.DrawPlaces(this);
		this.UpdateDropZones();
	}

	public void Populate()
	{
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData2 = this.selectedPortalIndex,
			customData = StaticData.allPortals[this.selectedPortalIndex].portalId
		};
		this.CreatePortalInfo(eventHandlerParam);
	}

	public void PopulateAfterStateChenge()
	{
		List<SlotItem> list = NetworkScript.player.playerBelongings.playerItems.slotItems;
		if (TransformerWindow.<>f__am$cache20 == null)
		{
			TransformerWindow.<>f__am$cache20 = new Func<SlotItem, bool>(null, (SlotItem t) => (!PlayerItems.IsPortalPart(t.get_ItemType()) ? false : t.get_SlotType() == 1));
		}
		IEnumerable<SlotItem> enumerable = Enumerable.Where<SlotItem>(list, TransformerWindow.<>f__am$cache20);
		if (TransformerWindow.<>f__am$cache21 == null)
		{
			TransformerWindow.<>f__am$cache21 = new Func<SlotItem, ushort>(null, (SlotItem s) => s.get_Slot());
		}
		List<SlotItem> list1 = Enumerable.ToList<SlotItem>(Enumerable.OrderBy<SlotItem, ushort>(enumerable, TransformerWindow.<>f__am$cache21));
		if (list1.get_Count() > 0 && list1.get_Item(0).get_Slot() >= 16)
		{
			this.inventoryScrollIndex = (list1.get_Item(0).get_Slot() - 12) / 4 * 4;
			NetworkScript.player.shipScript.transformerInventoryScrollIndex = this.inventoryScrollIndex;
		}
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData2 = this.selectedPortalIndex,
			customData = StaticData.allPortals[this.selectedPortalIndex].portalId
		};
		this.CreatePortalInfo(eventHandlerParam);
	}

	public void PopulateAfterUnlack()
	{
		this.CreateGalaxiesList();
	}

	public void PopulateRightButtonPanel()
	{
		this.CreateRightButtonPanel();
		this.CreateBank();
	}

	private void PutFreeSpinAnimation()
	{
		this.freeSpinBtnAnimationLeft1 = new GuiTextureAnimated();
		this.freeSpinBtnAnimationLeft1.Init("BlinkingSmallButtonLeft", "BlinkingSmallButtonLeft", "BlinkingSmallButtonLeft/btn_left_");
		this.freeSpinBtnAnimationLeft1.rotationTime = 1.5f;
		this.freeSpinBtnAnimationLeft1.boundries = new Rect(this.btnUseNova.boundries.get_x(), this.btnUseNova.boundries.get_y(), 10f, 25f);
		base.AddGuiElement(this.freeSpinBtnAnimationLeft1);
		this.forDeleteRBP.Add(this.freeSpinBtnAnimationLeft1);
		this.freeSpinBtnAnimationMiddle1 = new GuiTextureAnimated();
		this.freeSpinBtnAnimationMiddle1.Init("BlinkingSmallButtonMiddle", "BlinkingSmallButtonMiddle", "BlinkingSmallButtonMiddle/btn_center_");
		this.freeSpinBtnAnimationMiddle1.rotationTime = 1.5f;
		this.freeSpinBtnAnimationMiddle1.boundries = new Rect(this.btnUseNova.boundries.get_x() + 10f, this.btnUseNova.boundries.get_y(), this.btnUseNova.boundries.get_width() - 20f, 25f);
		base.AddGuiElement(this.freeSpinBtnAnimationMiddle1);
		this.forDeleteRBP.Add(this.freeSpinBtnAnimationMiddle1);
		this.freeSpinBtnAnimationRight1 = new GuiTextureAnimated();
		this.freeSpinBtnAnimationRight1.Init("BlinkingSmallButtonRight", "BlinkingSmallButtonRight", "BlinkingSmallButtonRight/btn_right_");
		this.freeSpinBtnAnimationRight1.rotationTime = 1.5f;
		this.freeSpinBtnAnimationRight1.boundries = new Rect(this.btnUseNova.boundries.get_x() + this.btnUseNova.boundries.get_width() - 10f, this.btnUseNova.boundries.get_y(), 10f, 25f);
		base.AddGuiElement(this.freeSpinBtnAnimationRight1);
		this.forDeleteRBP.Add(this.freeSpinBtnAnimationRight1);
		this.freeSpinBtnAnimationLeft2 = new GuiTextureAnimated();
		this.freeSpinBtnAnimationLeft2.Init("BlinkingSmallButtonLeft", "BlinkingSmallButtonLeft", "BlinkingSmallButtonLeft/btn_left_");
		this.freeSpinBtnAnimationLeft2.rotationTime = 1.5f;
		this.freeSpinBtnAnimationLeft2.boundries = new Rect(this.btnUseUltralibrium.boundries.get_x(), this.btnUseUltralibrium.boundries.get_y(), 10f, 25f);
		base.AddGuiElement(this.freeSpinBtnAnimationLeft2);
		this.forDeleteRBP.Add(this.freeSpinBtnAnimationLeft2);
		this.freeSpinBtnAnimationMiddle2 = new GuiTextureAnimated();
		this.freeSpinBtnAnimationMiddle2.Init("BlinkingSmallButtonMiddle", "BlinkingSmallButtonMiddle", "BlinkingSmallButtonMiddle/btn_center_");
		this.freeSpinBtnAnimationMiddle2.rotationTime = 1.5f;
		this.freeSpinBtnAnimationMiddle2.boundries = new Rect(this.btnUseUltralibrium.boundries.get_x() + 10f, this.btnUseUltralibrium.boundries.get_y(), this.btnUseUltralibrium.boundries.get_width() - 20f, 25f);
		base.AddGuiElement(this.freeSpinBtnAnimationMiddle2);
		this.forDeleteRBP.Add(this.freeSpinBtnAnimationMiddle2);
		this.freeSpinBtnAnimationRight2 = new GuiTextureAnimated();
		this.freeSpinBtnAnimationRight2.Init("BlinkingSmallButtonRight", "BlinkingSmallButtonRight", "BlinkingSmallButtonRight/btn_right_");
		this.freeSpinBtnAnimationRight2.rotationTime = 1.5f;
		this.freeSpinBtnAnimationRight2.boundries = new Rect(this.btnUseUltralibrium.boundries.get_x() + this.btnUseUltralibrium.boundries.get_width() - 10f, this.btnUseUltralibrium.boundries.get_y(), 10f, 25f);
		base.AddGuiElement(this.freeSpinBtnAnimationRight2);
		this.forDeleteRBP.Add(this.freeSpinBtnAnimationRight2);
	}

	private void RefreshExpandBtnState()
	{
		int num = 0;
		num = (this.inventorySlotCnt <= 36 ? Enumerable.First<SlotPriceInfo>(Enumerable.Where<SlotPriceInfo>(StaticData.slotPriceInformation, new Func<SlotPriceInfo, bool>(this, (SlotPriceInfo s) => (s.slotId != this.inventorySlotCnt + 4 ? false : s.slotType == "Inventory")))).priceNova : 999999999);
		foreach (GuiElement lockedInventorySlot in this.lockedInventorySlots)
		{
			if (!(lockedInventorySlot is GuiButtonFixed))
			{
				continue;
			}
			((GuiButtonFixed)lockedInventorySlot).isEnabled = NetworkScript.player.playerBelongings.playerItems.get_Nova() >= (long)num;
		}
	}

	private void RefreshScrollButtons()
	{
		GuiLabel guiLabel = this.scrollLabel;
		string[] str = new string[] { this.inventoryScrollIndex.ToString(), "-", default(string), default(string), default(string) };
		int num = Math.Min(this.inventorySlotCnt, this.inventoryScrollIndex + 16);
		str[2] = num.ToString();
		str[3] = StaticData.Translate("key_ship_cfg_tab_separator");
		str[4] = this.inventorySlotCnt.ToString();
		guiLabel.text = string.Concat(str);
		this.scrollLeftButton.isEnabled = this.inventoryScrollIndex != 0;
		this.scrollRightButton.isEnabled = (this.inventorySlotCnt != 40 ? this.inventorySlotCnt - this.inventoryScrollIndex > 14 : this.inventorySlotCnt - this.inventoryScrollIndex > 16);
	}

	private void UnlockPortal(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void TransformerWindow::UnlockPortal(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void UnlockPortal(EventHandlerParam)
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

	private void UpdateDropZones()
	{
		TransformerWindow.<UpdateDropZones>c__AnonStorey73 variable = null;
		int num;
		Portal portal = StaticData.allPortals[this.selectedPortalIndex];
		Texture2D fromStaticSet = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWnd", "DropBoxActive");
		Texture2D texture2D = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWnd", "DropBoxHvr");
		for (int i = 0; i < portal.parts.get_Count(); i++)
		{
			ushort item = portal.parts.get_Keys().get_Item(i);
			float single = (float)(220 + i / 4 * 330);
			float single1 = (float)(178 + i % 4 * 90);
			PortalPart portalPart = Enumerable.FirstOrDefault<PortalPart>(Enumerable.Where<PortalPart>(NetworkScript.player.playerBelongings.playerItems.portalParts, new Func<PortalPart, bool>(variable, (PortalPart t) => (t.portalId != this.<>f__ref$114.portal.portalId ? false : t.partTypeId == this.partType))));
			if (portalPart == null)
			{
				num = 0;
			}
			else
			{
				num = portalPart.partAmount;
			}
			int num1 = num;
			foreach (AndromedaGuiDragDropPlace place in Inventory.places)
			{
				if (!place.isEmpty && place.item.get_ItemType() == item && portal.parts.get_Item(item) > num1)
				{
					place.txItem.hoverParam = place.item;
					place.txItem.AddDropZone(new Vector2(single, single1 - 55f), item * 1000, fromStaticSet, texture2D);
					place.txItem.callbackAttribute = place.item.get_Slot() + place.item.get_SlotType() * 1000;
				}
			}
		}
	}

	private void UseTransformer(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void TransformerWindow::UseTransformer(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void UseTransformer(EventHandlerParam)
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
}