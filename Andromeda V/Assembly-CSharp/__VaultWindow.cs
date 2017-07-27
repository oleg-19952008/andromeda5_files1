using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TransferableObjects;
using UnityEngine;

public class __VaultWindow : GuiWindow
{
	public static int _maxAmmoDamage;

	public static int _maxWeaponDamage;

	public static int _maxCooldown;

	public static int _maxRange;

	public static int _maxPenetration;

	public static int _maxWeaponTargeting;

	public static int _maxEngineBonus;

	public static int _maxShieldBonus;

	public static int _maxCorpusBonus;

	private int inventorySlotCnt;

	private int vaultSlotCnt;

	private GuiLabel playerName;

	private GuiLabel playerHonor;

	private GuiTexture playerFraction;

	private GuiTexture playerHonorIcon;

	private static List<GuiElement> forDelete;

	private List<GuiElement> lockedInventorySlots = new List<GuiElement>();

	private List<GuiElement> lockedVaultSlots = new List<GuiElement>();

	private GuiWindow expandModalWindow;

	private UniversalTransportContainer expandSlotData;

	static __VaultWindow()
	{
		__VaultWindow.forDelete = new List<GuiElement>();
	}

	public __VaultWindow()
	{
		__VaultWindow.CalculateMaxStats();
	}

	public static void CalculateMaxStats()
	{
		IList<PlayerItemTypesData> values = StaticData.allTypes.get_Values();
		if (__VaultWindow.<>f__am$cache14 == null)
		{
			__VaultWindow.<>f__am$cache14 = new Func<PlayerItemTypesData, bool>(null, (PlayerItemTypesData w) => PlayerItems.IsAmmo(w.itemType));
		}
		IEnumerable<PlayerItemTypesData> enumerable = Enumerable.Where<PlayerItemTypesData>(values, __VaultWindow.<>f__am$cache14);
		if (__VaultWindow.<>f__am$cache15 == null)
		{
			__VaultWindow.<>f__am$cache15 = new Func<PlayerItemTypesData, short>(null, (PlayerItemTypesData a) => ((AmmoNet)a).damage);
		}
		__VaultWindow._maxAmmoDamage = Enumerable.Max<PlayerItemTypesData, short>(enumerable, __VaultWindow.<>f__am$cache15);
		IList<PlayerItemTypesData> list = StaticData.allTypes.get_Values();
		if (__VaultWindow.<>f__am$cache16 == null)
		{
			__VaultWindow.<>f__am$cache16 = new Func<PlayerItemTypesData, bool>(null, (PlayerItemTypesData w) => PlayerItems.IsWeapon(w.itemType));
		}
		IEnumerable<PlayerItemTypesData> enumerable1 = Enumerable.Where<PlayerItemTypesData>(list, __VaultWindow.<>f__am$cache16);
		if (__VaultWindow.<>f__am$cache17 == null)
		{
			__VaultWindow.<>f__am$cache17 = new Func<PlayerItemTypesData, short>(null, (PlayerItemTypesData a) => ((WeaponsTypeNet)a).upgrades[0].damage);
		}
		__VaultWindow._maxWeaponDamage = Enumerable.Max<PlayerItemTypesData, short>(enumerable1, __VaultWindow.<>f__am$cache17);
		IList<PlayerItemTypesData> values1 = StaticData.allTypes.get_Values();
		if (__VaultWindow.<>f__am$cache18 == null)
		{
			__VaultWindow.<>f__am$cache18 = new Func<PlayerItemTypesData, bool>(null, (PlayerItemTypesData w) => PlayerItems.IsWeapon(w.itemType));
		}
		IEnumerable<PlayerItemTypesData> enumerable2 = Enumerable.Where<PlayerItemTypesData>(values1, __VaultWindow.<>f__am$cache18);
		if (__VaultWindow.<>f__am$cache19 == null)
		{
			__VaultWindow.<>f__am$cache19 = new Func<PlayerItemTypesData, int>(null, (PlayerItemTypesData a) => ((WeaponsTypeNet)a).upgrades[0].cooldown);
		}
		__VaultWindow._maxCooldown = Enumerable.Max<PlayerItemTypesData>(enumerable2, __VaultWindow.<>f__am$cache19);
		IList<PlayerItemTypesData> list1 = StaticData.allTypes.get_Values();
		if (__VaultWindow.<>f__am$cache1A == null)
		{
			__VaultWindow.<>f__am$cache1A = new Func<PlayerItemTypesData, bool>(null, (PlayerItemTypesData w) => PlayerItems.IsWeapon(w.itemType));
		}
		IEnumerable<PlayerItemTypesData> enumerable3 = Enumerable.Where<PlayerItemTypesData>(list1, __VaultWindow.<>f__am$cache1A);
		if (__VaultWindow.<>f__am$cache1B == null)
		{
			__VaultWindow.<>f__am$cache1B = new Func<PlayerItemTypesData, short>(null, (PlayerItemTypesData a) => ((WeaponsTypeNet)a).upgrades[0].range);
		}
		__VaultWindow._maxRange = Enumerable.Max<PlayerItemTypesData, short>(enumerable3, __VaultWindow.<>f__am$cache1B);
		IList<PlayerItemTypesData> values2 = StaticData.allTypes.get_Values();
		if (__VaultWindow.<>f__am$cache1C == null)
		{
			__VaultWindow.<>f__am$cache1C = new Func<PlayerItemTypesData, bool>(null, (PlayerItemTypesData w) => PlayerItems.IsWeapon(w.itemType));
		}
		IEnumerable<PlayerItemTypesData> enumerable4 = Enumerable.Where<PlayerItemTypesData>(values2, __VaultWindow.<>f__am$cache1C);
		if (__VaultWindow.<>f__am$cache1D == null)
		{
			__VaultWindow.<>f__am$cache1D = new Func<PlayerItemTypesData, short>(null, (PlayerItemTypesData a) => ((WeaponsTypeNet)a).upgrades[0].penetration);
		}
		__VaultWindow._maxPenetration = Enumerable.Max<PlayerItemTypesData, short>(enumerable4, __VaultWindow.<>f__am$cache1D);
		IList<PlayerItemTypesData> list2 = StaticData.allTypes.get_Values();
		if (__VaultWindow.<>f__am$cache1E == null)
		{
			__VaultWindow.<>f__am$cache1E = new Func<PlayerItemTypesData, bool>(null, (PlayerItemTypesData w) => PlayerItems.IsWeapon(w.itemType));
		}
		IEnumerable<PlayerItemTypesData> enumerable5 = Enumerable.Where<PlayerItemTypesData>(list2, __VaultWindow.<>f__am$cache1E);
		if (__VaultWindow.<>f__am$cache1F == null)
		{
			__VaultWindow.<>f__am$cache1F = new Func<PlayerItemTypesData, short>(null, (PlayerItemTypesData a) => ((WeaponsTypeNet)a).upgrades[0].targeting);
		}
		__VaultWindow._maxWeaponTargeting = Enumerable.Max<PlayerItemTypesData, short>(enumerable5, __VaultWindow.<>f__am$cache1F);
		IList<PlayerItemTypesData> values3 = StaticData.allTypes.get_Values();
		if (__VaultWindow.<>f__am$cache20 == null)
		{
			__VaultWindow.<>f__am$cache20 = new Func<PlayerItemTypesData, bool>(null, (PlayerItemTypesData w) => PlayerItems.IsEngine(w.itemType));
		}
		IEnumerable<PlayerItemTypesData> enumerable6 = Enumerable.Where<PlayerItemTypesData>(values3, __VaultWindow.<>f__am$cache20);
		if (__VaultWindow.<>f__am$cache21 == null)
		{
			__VaultWindow.<>f__am$cache21 = new Func<PlayerItemTypesData, short>(null, (PlayerItemTypesData a) => ((GeneratorNet)a).bonusValue);
		}
		__VaultWindow._maxEngineBonus = Enumerable.Max<PlayerItemTypesData, short>(enumerable6, __VaultWindow.<>f__am$cache21);
		IList<PlayerItemTypesData> list3 = StaticData.allTypes.get_Values();
		if (__VaultWindow.<>f__am$cache22 == null)
		{
			__VaultWindow.<>f__am$cache22 = new Func<PlayerItemTypesData, bool>(null, (PlayerItemTypesData w) => PlayerItems.IsShield(w.itemType));
		}
		IEnumerable<PlayerItemTypesData> enumerable7 = Enumerable.Where<PlayerItemTypesData>(list3, __VaultWindow.<>f__am$cache22);
		if (__VaultWindow.<>f__am$cache23 == null)
		{
			__VaultWindow.<>f__am$cache23 = new Func<PlayerItemTypesData, short>(null, (PlayerItemTypesData a) => ((GeneratorNet)a).bonusValue);
		}
		__VaultWindow._maxShieldBonus = Enumerable.Max<PlayerItemTypesData, short>(enumerable7, __VaultWindow.<>f__am$cache23);
		IList<PlayerItemTypesData> values4 = StaticData.allTypes.get_Values();
		if (__VaultWindow.<>f__am$cache24 == null)
		{
			__VaultWindow.<>f__am$cache24 = new Func<PlayerItemTypesData, bool>(null, (PlayerItemTypesData w) => PlayerItems.IsCorpus(w.itemType));
		}
		IEnumerable<PlayerItemTypesData> enumerable8 = Enumerable.Where<PlayerItemTypesData>(values4, __VaultWindow.<>f__am$cache24);
		if (__VaultWindow.<>f__am$cache25 == null)
		{
			__VaultWindow.<>f__am$cache25 = new Func<PlayerItemTypesData, short>(null, (PlayerItemTypesData a) => ((GeneratorNet)a).bonusValue);
		}
		__VaultWindow._maxCorpusBonus = Enumerable.Max<PlayerItemTypesData, short>(enumerable8, __VaultWindow.<>f__am$cache25);
	}

	private void CancelExpand(object prm)
	{
		AndromedaGui.gui.RemoveWindow(this.expandModalWindow.handler);
		this.expandModalWindow = null;
		AndromedaGui.gui.activeToolTipId = -1;
	}

	private new void Clear()
	{
		foreach (GuiElement guiElement in __VaultWindow.forDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		__VaultWindow.forDelete.Clear();
	}

	private void ConfirmExpand(EventHandlerParam prm)
	{
		if (this.expandSlotData == null)
		{
			return;
		}
		SelectedCurrency selectedCurrency = (int)prm.customData;
		this.expandSlotData.paymentCurrency = selectedCurrency;
		int num = 0;
		if (this.expandSlotData.wantedSlot != 7)
		{
			NetworkScript.player.playerBelongings.ExpandVault(ref num);
		}
		else
		{
			NetworkScript.player.playerBelongings.ExpandInventory(ref num);
		}
		this.vaultSlotCnt = NetworkScript.player.playerBelongings.playerVaultSlots;
		this.inventorySlotCnt = NetworkScript.player.playerBelongings.playerInventorySlots;
		playWebGame.udp.ExecuteCommand(PureUdpClient.CommandExpandSlots, this.expandSlotData, 49);
		this.CancelExpand(null);
		Inventory.ClearSlots(this);
		this.DrawInventorySlots();
		this.DrawVaultSlots();
		Inventory.DrawPlaces();
	}

	public override void Create()
	{
		Inventory.secondaryDropHandler = new Action<AndromedaGuiDragDropPlace, AndromedaGuiDragDropPlace>(this, __VaultWindow.OnItemMoved);
		Inventory.closeStackablePopUp = new Action(this, __VaultWindow.Refresh);
		base.SetBackgroundTexture("NewGUI", "novashop_frame");
		base.PutToCenter();
		this.isHidden = false;
		this.zOrder = 210;
		Inventory.window = this;
		Inventory.isRightClickActionEnable = true;
		Inventory.isVaultMenuOpen = true;
		Inventory.isItemRerollMenuOpen = false;
		Inventory.isGuildVaultMenuOpen = false;
		Inventory.ConfigWndAfterRightClkAction = new Action(this, __VaultWindow.PopulateItemAfterRightClickAction);
		this.inventorySlotCnt = NetworkScript.player.playerBelongings.playerInventorySlots;
		this.vaultSlotCnt = NetworkScript.player.playerBelongings.playerVaultSlots;
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(275f, 35f, 567f, 30f),
			text = StaticData.Translate("key_vault_title"),
			FontSize = 21,
			Font = GuiLabel.FontBold,
			Alignment = 1,
			TextColor = GuiNewStyleBar.orangeColor
		};
		base.AddGuiElement(guiLabel);
		__VaultWindow.forDelete.Add(guiLabel);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("ConfigWnd", "vault_frame");
		guiTexture.boundries = new Rect(206f, 66f, 680f, 464f);
		base.AddGuiElement(guiTexture);
		__VaultWindow.forDelete.Add(guiTexture);
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("ConfigWnd", "vaultIconBase");
		rect.boundries = new Rect(212f, 141f, 93f, 93f);
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_tooltip_player_vault"),
			customData2 = rect
		};
		rect.tooltipWindowParam = eventHandlerParam;
		rect.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(rect);
		__VaultWindow.forDelete.Add(rect);
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
		__VaultWindow.forDelete.Add(drawTooltipWindow);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(30f, 300f, 168f, 100f),
			text = StaticData.Translate("key_vault_desc"),
			Alignment = 1,
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(guiLabel1);
		__VaultWindow.forDelete.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(30f, 400f, 168f, 16f),
			text = StaticData.Translate(NetworkScript.player.cfg.shipName).ToUpper(),
			Alignment = 4,
			FontSize = 14,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(guiLabel2);
		__VaultWindow.forDelete.Add(guiLabel2);
		GuiTexture guiTexture1 = new GuiTexture()
		{
			boundries = new Rect(30f, 416f, 168f, 115f)
		};
		guiTexture1.SetTextureKeepSize("ShipsAvatars", NetworkScript.player.cfg.assetName);
		base.AddGuiElement(guiTexture1);
		__VaultWindow.forDelete.Add(guiTexture1);
		Inventory.places.Clear();
		this.DrawPlayerInfo();
		this.DrawInventorySlots();
		this.DrawVaultSlots();
		Inventory.DrawPlaces();
	}

	private void CreateExpandDialog(bool isVault)
	{
		SlotPriceInfo slotPriceInfo = null;
		slotPriceInfo = (!isVault ? Enumerable.FirstOrDefault<SlotPriceInfo>(Enumerable.Where<SlotPriceInfo>(StaticData.slotPriceInformation, new Func<SlotPriceInfo, bool>(this, (SlotPriceInfo s) => (s.slotId != this.inventorySlotCnt + 4 ? false : s.slotType == "Inventory")))) : Enumerable.FirstOrDefault<SlotPriceInfo>(Enumerable.Where<SlotPriceInfo>(StaticData.slotPriceInformation, new Func<SlotPriceInfo, bool>(this, (SlotPriceInfo s) => (s.slotId != this.vaultSlotCnt + 4 ? false : s.slotType == "Vault")))));
		if (slotPriceInfo == null)
		{
			return;
		}
		this.expandModalWindow = new GuiWindow()
		{
			isModal = true
		};
		this.expandModalWindow.SetBackgroundTexture("ConfigWindow", "proba");
		this.expandModalWindow.isHidden = false;
		this.expandModalWindow.zOrder = 220;
		this.expandModalWindow.PutToCenter();
		AndromedaGui.gui.AddWindow(this.expandModalWindow);
		AndromedaGui.gui.activeToolTipId = this.expandModalWindow.handler;
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("ConfigWindow", "X_btn_");
		guiButtonFixed.X = 417f;
		guiButtonFixed.Y = -3f;
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, __VaultWindow.CancelExpand);
		guiButtonFixed.SetLeftClickSound("FrameworkGUI", "cancel");
		this.expandModalWindow.AddGuiElement(guiButtonFixed);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(this.expandModalWindow.boundries.get_width() * 0.05f, 30f, this.expandModalWindow.boundries.get_width() * 0.9f, 60f)
		};
		if (!isVault)
		{
			guiLabel.text = StaticData.Translate("key_inventory_expand_modal");
		}
		else
		{
			guiLabel.text = StaticData.Translate("key_vault_expand_modal");
		}
		guiLabel.Alignment = 4;
		guiLabel.FontSize = 16;
		this.expandModalWindow.AddGuiElement(guiLabel);
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
		float _width = (this.expandModalWindow.boundries.get_width() - 60f - (float)(Math.Max(num - 1, 0) * 10)) / (float)num;
		float single = 30f;
		if (slotPriceInfo.priceEqulibrium > 0)
		{
			GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
			guiButtonResizeable.SetSmallBlueTexture();
			guiButtonResizeable.X = single;
			guiButtonResizeable.Y = 135f;
			guiButtonResizeable.Width = _width;
			guiButtonResizeable.isEnabled = NetworkScript.player.playerBelongings.playerItems.get_Equilibrium() >= (long)slotPriceInfo.priceEqulibrium;
			guiButtonResizeable.MarginTop = -3;
			guiButtonResizeable.Alignment = 4;
			guiButtonResizeable.Caption = slotPriceInfo.priceEqulibrium.ToString("##,##0");
			guiButtonResizeable.eventHandlerParam.customData = (SelectedCurrency)2;
			guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, __VaultWindow.ConfirmExpand);
			this.expandModalWindow.AddGuiElement(guiButtonResizeable);
			GuiTexture guiTexture = new GuiTexture();
			guiTexture.SetTexture("FrameworkGUI", "res_equilibrium");
			guiTexture.Y = guiButtonResizeable.Y + 2f;
			guiTexture.X = guiButtonResizeable.X + 10f;
			this.expandModalWindow.AddGuiElement(guiTexture);
			single = single + (_width + 10f);
		}
		if (slotPriceInfo.priceNova > 0)
		{
			GuiButtonResizeable nova = new GuiButtonResizeable();
			nova.SetSmallBlueTexture();
			nova.X = single;
			nova.Y = 135f;
			nova.Width = _width;
			nova.isEnabled = NetworkScript.player.playerBelongings.playerItems.get_Nova() >= (long)slotPriceInfo.priceNova;
			nova.MarginTop = -3;
			nova.Alignment = 4;
			nova.Caption = slotPriceInfo.priceNova.ToString("##,##0");
			nova.eventHandlerParam.customData = (SelectedCurrency)1;
			nova.Clicked = new Action<EventHandlerParam>(this, __VaultWindow.ConfirmExpand);
			this.expandModalWindow.AddGuiElement(nova);
			GuiTexture y = new GuiTexture();
			y.SetTexture("FrameworkGUI", "res_nova");
			y.Y = nova.Y + 2f;
			y.X = nova.X + 10f;
			this.expandModalWindow.AddGuiElement(y);
			single = single + (_width + 10f);
		}
		if (slotPriceInfo.priceUltralibrium > 0)
		{
			GuiButtonResizeable ultralibrium = new GuiButtonResizeable();
			ultralibrium.SetSmallBlueTexture();
			ultralibrium.X = single;
			ultralibrium.Y = 135f;
			ultralibrium.Width = _width;
			ultralibrium.isEnabled = NetworkScript.player.playerBelongings.playerItems.get_Ultralibrium() >= (long)slotPriceInfo.priceUltralibrium;
			ultralibrium.MarginTop = -3;
			ultralibrium.Alignment = 4;
			ultralibrium.Caption = slotPriceInfo.priceUltralibrium.ToString("##,##0");
			ultralibrium.eventHandlerParam.customData = (SelectedCurrency)3;
			ultralibrium.Clicked = new Action<EventHandlerParam>(this, __VaultWindow.ConfirmExpand);
			this.expandModalWindow.AddGuiElement(ultralibrium);
			GuiTexture x = new GuiTexture();
			x.SetTexture("FrameworkGUI", "res_ultralibrium");
			x.Y = ultralibrium.Y + 2f;
			x.X = ultralibrium.X + 10f;
			this.expandModalWindow.AddGuiElement(x);
		}
	}

	private void DrawInventorySlots()
	{
		__VaultWindow.<DrawInventorySlots>c__AnonStoreyAE variable = null;
		foreach (GuiElement lockedInventorySlot in this.lockedInventorySlots)
		{
			base.RemoveGuiElement(lockedInventorySlot);
		}
		this.lockedInventorySlots.Clear();
		List<SlotItem> list = NetworkScript.player.playerBelongings.playerItems.slotItems;
		if (__VaultWindow.<>f__am$cache27 == null)
		{
			__VaultWindow.<>f__am$cache27 = new Func<SlotItem, bool>(null, (SlotItem t) => t.get_SlotType() == 1);
		}
		List<SlotItem> list1 = Enumerable.ToList<SlotItem>(Enumerable.Where<SlotItem>(list, __VaultWindow.<>f__am$cache27));
		this.inventorySlotCnt = NetworkScript.player.playerBelongings.playerInventorySlots;
		for (int i = 0; i < 40; i++)
		{
			if (i < this.inventorySlotCnt)
			{
				AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = new AndromedaGuiDragDropPlace()
				{
					slot = (byte)i
				};
				Inventory.places.Add(andromedaGuiDragDropPlace);
				andromedaGuiDragDropPlace.txFrameIdle = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotIdle");
				andromedaGuiDragDropPlace.position = new Vector2((float)(308 + i / 4 * 58), (float)(315 + i % 4 * 52));
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
			else if (i != this.inventorySlotCnt || this.inventorySlotCnt >= 40)
			{
				GuiTexture guiTexture = new GuiTexture();
				guiTexture.SetTexture("ConfigWindow", "inventory_slot_locked");
				guiTexture.boundries = new Rect((float)(306 + i / 4 * 58), (float)(315 + i % 4 * 52), 55f, 55f);
				base.AddGuiElement(guiTexture);
				this.lockedInventorySlots.Add(guiTexture);
				__VaultWindow.forDelete.Add(guiTexture);
			}
			else
			{
				GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
				guiButtonFixed.SetTexture("ConfigWindow", "inventory_slot_plus");
				guiButtonFixed.boundries = new Rect((float)(306 + i / 4 * 58), (float)(315 + i % 4 * 52), 55f, 55f);
				guiButtonFixed.Caption = string.Empty;
				base.AddGuiElement(guiButtonFixed);
				guiButtonFixed.hoverParam = false;
				guiButtonFixed.Hovered = new Action<object, bool>(this, __VaultWindow.OnExpandHover);
				EventHandlerParam eventHandlerParam = new EventHandlerParam()
				{
					customData = false
				};
				guiButtonFixed.eventHandlerParam = eventHandlerParam;
				guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, __VaultWindow.OnExpandBtnClicked);
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_expand_inventory"),
					customData2 = guiButtonFixed
				};
				guiButtonFixed.tooltipWindowParam = eventHandlerParam;
				guiButtonFixed.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				this.lockedInventorySlots.Add(guiButtonFixed);
				__VaultWindow.forDelete.Add(guiButtonFixed);
			}
		}
	}

	private void DrawPlayerInfo()
	{
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("NewGUI", "pvp_ranking_left");
		guiTexture.X = 24f;
		guiTexture.Y = 26f;
		base.AddGuiElement(guiTexture);
		string str = string.Format("fraction{0}Icon", NetworkScript.player.vessel.fractionId);
		this.playerFraction = new GuiTexture();
		this.playerFraction.SetTexture("FrameworkGUI", str);
		this.playerFraction.X = 30f;
		this.playerFraction.Y = 47f;
		base.AddGuiElement(this.playerFraction);
		__VaultWindow.forDelete.Add(this.playerFraction);
		this.playerName = new GuiLabel()
		{
			boundries = new Rect(30f, 44f, 174f, 24f),
			text = string.Format("{0} ({1})", NetworkScript.player.playerBelongings.playerName, NetworkScript.player.playerBelongings.playerLevel),
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			Alignment = 3
		};
		base.AddGuiElement(this.playerName);
		__VaultWindow.forDelete.Add(this.playerName);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("NewGUI", "pvp_avatar");
		guiTexture1.X = 68f;
		guiTexture1.Y = 88f;
		base.AddGuiElement(guiTexture1);
		__VaultWindow.forDelete.Add(guiTexture1);
		GuiTexture guiTexture2 = new GuiTexture()
		{
			boundries = new Rect(69f, 89f, 100f, 100f)
		};
		Texture2D avatarOrStartIt = playWebGame.GetAvatarOrStartIt(NetworkScript.player.vessel.playerAvatarUrl, new Action<AvatarJob>(this, __VaultWindow.SetAvatar), guiTexture2);
		if (avatarOrStartIt == null)
		{
			guiTexture2.SetTextureKeepSize("FrameworkGUI", "unknown");
			avatarOrStartIt = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "unknown");
		}
		guiTexture2.SetTextureKeepSize(avatarOrStartIt);
		base.AddGuiElement(guiTexture2);
		__VaultWindow.forDelete.Add(guiTexture2);
		int amountAt = (int)NetworkScript.player.playerBelongings.playerItems.GetAmountAt(PlayerItems.TypeHonor);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(30f, 193f, 174f, 16f),
			text = PlayerItems.GetRankingTitle(amountAt),
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			Alignment = 4
		};
		base.AddGuiElement(guiLabel);
		__VaultWindow.forDelete.Add(guiLabel);
		this.playerHonorIcon = new GuiTexture();
		this.playerHonorIcon.SetTexture("NewGUI", "res_honor");
		this.playerHonorIcon.X = 35f;
		this.playerHonorIcon.Y = 207f;
		base.AddGuiElement(this.playerHonorIcon);
		__VaultWindow.forDelete.Add(this.playerHonorIcon);
		this.playerHonor = new GuiLabel()
		{
			boundries = new Rect(30f, 210f, 174f, 16f),
			text = amountAt.ToString("##,##0"),
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			Alignment = 3
		};
		base.AddGuiElement(this.playerHonor);
		__VaultWindow.forDelete.Add(this.playerHonor);
		this.RePossitionPlayerStuf();
	}

	private void DrawVaultSlots()
	{
		__VaultWindow.<DrawVaultSlots>c__AnonStoreyAD variable = null;
		foreach (GuiElement lockedVaultSlot in this.lockedVaultSlots)
		{
			base.RemoveGuiElement(lockedVaultSlot);
		}
		this.lockedVaultSlots.Clear();
		List<SlotItem> list = NetworkScript.player.playerBelongings.playerItems.slotItems;
		if (__VaultWindow.<>f__am$cache26 == null)
		{
			__VaultWindow.<>f__am$cache26 = new Func<SlotItem, bool>(null, (SlotItem t) => t.get_SlotType() == 2);
		}
		List<SlotItem> list1 = Enumerable.ToList<SlotItem>(Enumerable.Where<SlotItem>(list, __VaultWindow.<>f__am$cache26));
		this.vaultSlotCnt = NetworkScript.player.playerBelongings.playerVaultSlots;
		for (int i = 0; i < 40; i++)
		{
			if (i < this.vaultSlotCnt)
			{
				AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = new AndromedaGuiDragDropPlace()
				{
					slot = (byte)i
				};
				Inventory.places.Add(andromedaGuiDragDropPlace);
				andromedaGuiDragDropPlace.txFrameIdle = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotIdle");
				andromedaGuiDragDropPlace.position = new Vector2((float)(308 + i / 4 * 58), (float)(80 + i % 4 * 52));
				andromedaGuiDragDropPlace.dropZonePosition = andromedaGuiDragDropPlace.position;
				SlotItem slotItem = Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(list1, new Func<SlotItem, bool>(variable, (SlotItem t) => t.get_Slot() == this.i)));
				andromedaGuiDragDropPlace.isEmpty = slotItem == null;
				andromedaGuiDragDropPlace.item = slotItem;
				andromedaGuiDragDropPlace.txFrameDragSource = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotHvr");
				andromedaGuiDragDropPlace.txFrameDropTarget = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotActive");
				andromedaGuiDragDropPlace.txFrameDropTargetHover = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotHvr");
				andromedaGuiDragDropPlace.idleItemTextureSize = new Vector2(51f, 35f);
				andromedaGuiDragDropPlace.location = 2;
			}
			else if (i != this.vaultSlotCnt || i >= 40)
			{
				GuiTexture guiTexture = new GuiTexture();
				guiTexture.SetTexture("ConfigWindow", "inventory_slot_locked");
				guiTexture.boundries = new Rect((float)(306 + i / 4 * 58), (float)(80 + i % 4 * 52), 55f, 55f);
				base.AddGuiElement(guiTexture);
				this.lockedVaultSlots.Add(guiTexture);
				__VaultWindow.forDelete.Add(guiTexture);
			}
			else
			{
				GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
				guiButtonFixed.SetTexture("ConfigWindow", "inventory_slot_plus");
				guiButtonFixed.boundries = new Rect((float)(306 + i / 4 * 58), (float)(80 + i % 4 * 52), 55f, 55f);
				guiButtonFixed.Caption = string.Empty;
				base.AddGuiElement(guiButtonFixed);
				guiButtonFixed.hoverParam = true;
				guiButtonFixed.Hovered = new Action<object, bool>(this, __VaultWindow.OnExpandHover);
				EventHandlerParam eventHandlerParam = new EventHandlerParam()
				{
					customData = true
				};
				guiButtonFixed.eventHandlerParam = eventHandlerParam;
				guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, __VaultWindow.OnExpandBtnClicked);
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_expand_player_vault"),
					customData2 = guiButtonFixed
				};
				guiButtonFixed.tooltipWindowParam = eventHandlerParam;
				guiButtonFixed.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				this.lockedVaultSlots.Add(guiButtonFixed);
				__VaultWindow.forDelete.Add(guiButtonFixed);
			}
		}
	}

	private void OnExpandBtnClicked(EventHandlerParam prm)
	{
		this.expandSlotData = new UniversalTransportContainer();
		bool flag = (bool)prm.customData;
		if (!flag)
		{
			this.expandSlotData.wantedSlot = 7;
		}
		else
		{
			this.expandSlotData.wantedSlot = 8;
		}
		this.CreateExpandDialog(flag);
	}

	private void OnExpandHover(object prm, bool state)
	{
		bool flag = (bool)prm;
		List<GuiElement> list = null;
		string empty = string.Empty;
		list = (!flag ? this.lockedInventorySlots : this.lockedVaultSlots);
		empty = (!state ? "inventory_slot_locked" : "inventory_slot_lockedHvr");
		for (int i = 1; i < 4; i++)
		{
			((GuiTexture)list.get_Item(i)).SetTextureKeepSize("ConfigWindow", empty);
		}
	}

	private void OnItemMoved(AndromedaGuiDragDropPlace src, AndromedaGuiDragDropPlace dst)
	{
		this.Refresh();
	}

	private void PopulateItemAfterRightClickAction()
	{
		Inventory.ClearSlots(this);
		this.DrawInventorySlots();
		this.DrawVaultSlots();
		Inventory.DrawPlaces();
	}

	private void Refresh()
	{
		Inventory.ClearSlots(this);
		this.DrawInventorySlots();
		this.DrawVaultSlots();
		Inventory.DrawPlaces();
	}

	private void RePossitionPlayerStuf()
	{
		float textWidth = (174f - (this.playerName.TextWidth + this.playerFraction.boundries.get_width())) * 0.5f;
		this.playerFraction.X = 30f + textWidth;
		this.playerName.X = this.playerFraction.X + this.playerFraction.boundries.get_width();
		textWidth = (174f - (this.playerHonor.TextWidth + this.playerHonorIcon.boundries.get_width())) * 0.5f;
		this.playerHonorIcon.X = 30f + textWidth;
		this.playerHonor.X = this.playerHonorIcon.X + this.playerHonorIcon.boundries.get_width();
	}

	private void SetAvatar(AvatarJob prm)
	{
		((GuiTexture)prm.token).SetTextureKeepSize(prm.job.get_texture());
	}
}