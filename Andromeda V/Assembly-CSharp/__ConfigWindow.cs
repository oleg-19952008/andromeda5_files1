using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TransferableObjects;
using UnityEngine;

public class __ConfigWindow : GuiWindow
{
	private bool IsMobile;

	private int inventoryScrollIndex;

	private int vaultScrollIndex;

	private int inventorySlotCnt;

	private int vaultSlotCnt;

	private GuiTexture tab1Texture;

	private GuiButton tab1Button;

	private GuiButton tab2Button;

	private GuiButton tab3Button;

	private GuiButtonFixed scrollUpButton;

	private GuiButtonFixed scrollDownButton;

	private GuiLabel scrollLabel;

	private GuiButtonFixed sellButton;

	private GuiButtonFixed disintegrateButton;

	private GuiTexture shipTexture;

	private GuiLabel shipStatsLbl;

	private List<GuiElement> forDelete = new List<GuiElement>();

	private List<GuiElement> mainSlots = new List<GuiElement>();

	private List<GuiElement> forExpandedShipSlots = new List<GuiElement>();

	public __ConfigWindow.InventoryFilter selectedFilter;

	public __ConfigWindow.ConfigWindowTab selectedTab;

	private GuiLabel laserSlotsLabel;

	private GuiLabel plasmaSlotsLabel;

	private GuiLabel ionSlotsLabel;

	private SortedList<int, GuiTexture> ammoSlots = new SortedList<int, GuiTexture>();

	private SortedList<int, GuiButton> ammoButtons = new SortedList<int, GuiButton>();

	private UniversalTransportContainer buyShipSlotData;

	private ExpandSlotDialog expandShipSlotWindow;

	private List<GuiElement> lockedInventorySlots = new List<GuiElement>();

	private List<GuiElement> lockedVaultSlots = new List<GuiElement>();

	private ExpandSlotDialog expandSlotWindow;

	public __ConfigWindow()
	{
	}

	private string AssignTooltip(out bool isBuyBtnEnable)
	{
		string empty = string.Empty;
		SortedList<SelectedCurrency, int> sortedList = new SortedList<SelectedCurrency, int>();
		sortedList = NetworkScript.player.playerBelongings.GetExpandShipSlotSellInfo();
		if (sortedList.get_Count() == 1 && sortedList.get_Keys().Contains(0) && NetworkScript.player.playerBelongings.CanBuyNextExpandedSlotNew(0))
		{
			isBuyBtnEnable = true;
			string str = StaticData.Translate("key_extra_slots_first_slot");
			int item = sortedList.get_Item(0);
			empty = string.Format(str, item.ToString());
		}
		else if (sortedList.get_Count() <= 1 || !sortedList.get_Keys().Contains(1) || !sortedList.get_Keys().Contains(2) || !NetworkScript.player.playerBelongings.CanBuyNextExpandedSlotNew(1) && !NetworkScript.player.playerBelongings.CanBuyNextExpandedSlotNew(2))
		{
			isBuyBtnEnable = false;
			if (sortedList.get_Count() == 1 && !NetworkScript.player.playerBelongings.CanBuyNextExpandedSlotNew(0))
			{
				string str1 = StaticData.Translate("key_buy_ship_slot_not_enough_cash");
				int num = sortedList.get_Item(0);
				empty = string.Format(str1, num.ToString());
			}
			else if (sortedList.get_Count() > 1 && !NetworkScript.player.playerBelongings.CanBuyNextExpandedSlotNew(1) && !NetworkScript.player.playerBelongings.CanBuyNextExpandedSlotNew(2))
			{
				string str2 = StaticData.Translate("key_buy_ship_slot_not_enough_eqnova");
				string str3 = sortedList.get_Item(1).ToString();
				int item1 = sortedList.get_Item(2);
				empty = string.Format(str2, str3, item1.ToString());
			}
		}
		else
		{
			isBuyBtnEnable = true;
			string str4 = StaticData.Translate("key_buy_shipt_slot_tooltip");
			string str5 = sortedList.get_Item(1).ToString();
			int num1 = sortedList.get_Item(2);
			empty = string.Format(str4, str5, num1.ToString());
		}
		return empty;
	}

	private void CalculateDamageBase(ShipConfiguration cfg, out int dmgBase)
	{
		dmgBase = 0;
		WeaponSlot[] weaponSlotArray = cfg.weaponSlots;
		if (__ConfigWindow.<>f__am$cache21 == null)
		{
			__ConfigWindow.<>f__am$cache21 = new Func<WeaponSlot, bool>(null, (WeaponSlot ws) => (ws.get_WeaponStatus() == 1 ? false : ws.get_WeaponStatus() != 0));
		}
		WeaponSlot[] array = Enumerable.ToArray<WeaponSlot>(Enumerable.Where<WeaponSlot>(weaponSlotArray, __ConfigWindow.<>f__am$cache21));
		for (int i = 0; i < (int)array.Length; i++)
		{
			WeaponSlot weaponSlot = array[i];
			WeaponsTypeNet item = (WeaponsTypeNet)StaticData.allTypes.get_Item(weaponSlot.weaponTierId);
			dmgBase = dmgBase + item.upgrades[0].damage;
		}
	}

	private void CalculateDamageTotal(ShipConfiguration cfg, out int dmgTotal)
	{
		dmgTotal = 0;
		float single = 0f;
		WeaponSlot[] weaponSlotArray = cfg.weaponSlots;
		if (__ConfigWindow.<>f__am$cache20 == null)
		{
			__ConfigWindow.<>f__am$cache20 = new Func<WeaponSlot, bool>(null, (WeaponSlot ws) => ws.isActive);
		}
		WeaponSlot[] array = Enumerable.ToArray<WeaponSlot>(Enumerable.Where<WeaponSlot>(weaponSlotArray, __ConfigWindow.<>f__am$cache20));
		for (int i = 0; i < (int)array.Length; i++)
		{
			WeaponSlot weaponSlot = array[i];
			single = (!cfg.damageBooster ? (float)weaponSlot.skillDamage * (1f + cfg.dmgPercentForAlien / 100f) * ((float)((AmmoNet)StaticData.allTypes.get_Item(weaponSlot.selectedAmmoItemType)).damage / 100f) : (float)weaponSlot.skillDamage * (1f + cfg.dmgPercentForAlien / 100f) * ((float)((AmmoNet)StaticData.allTypes.get_Item(weaponSlot.selectedAmmoItemType)).damage / 100f) * 1.3f);
			dmgTotal = dmgTotal + (int)single;
		}
	}

	private EWeaponStatus CalculateWeaponStatus(SlotItem item)
	{
		if (item == null)
		{
			return 1;
		}
		SlotItemWeapon slotItemWeapon = (SlotItemWeapon)item;
		if (NetworkScript.player.playerBelongings.playerItems.GetAmmoQty(slotItemWeapon.get_AmmoType()) < (long)1)
		{
			return 5;
		}
		return 3;
	}

	public new void Clear()
	{
		base.RemoveGuiElement(this.shipTexture);
		base.RemoveGuiElement(this.shipStatsLbl);
		base.RemoveGuiElement(this.tab1Texture);
		base.RemoveGuiElement(this.tab1Button);
		base.RemoveGuiElement(this.tab2Button);
		base.RemoveGuiElement(this.tab3Button);
		base.RemoveGuiElement(this.scrollUpButton);
		base.RemoveGuiElement(this.scrollDownButton);
		base.RemoveGuiElement(this.scrollLabel);
		base.RemoveGuiElement(this.laserSlotsLabel);
		base.RemoveGuiElement(this.plasmaSlotsLabel);
		base.RemoveGuiElement(this.ionSlotsLabel);
		this.ClearAmmoSlots();
		foreach (GuiElement mainSlot in this.mainSlots)
		{
			base.RemoveGuiElement(mainSlot);
		}
		foreach (GuiElement guiElement in this.forDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
	}

	private void ClearAmmoSlots()
	{
		IEnumerator<GuiTexture> enumerator = this.ammoSlots.get_Values().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				base.RemoveGuiElement(enumerator.get_Current());
			}
		}
		finally
		{
			if (enumerator == null)
			{
			}
			enumerator.Dispose();
		}
		IEnumerator<GuiButton> enumerator1 = this.ammoButtons.get_Values().GetEnumerator();
		try
		{
			while (enumerator1.MoveNext())
			{
				base.RemoveGuiElement(enumerator1.get_Current());
			}
		}
		finally
		{
			if (enumerator1 == null)
			{
			}
			enumerator1.Dispose();
		}
	}

	private void ClearShipSlots()
	{
		foreach (GuiElement mainSlot in this.mainSlots)
		{
			base.RemoveGuiElement(mainSlot);
		}
		this.mainSlots.Clear();
	}

	private void ConfirmExpand(object prm)
	{
		playWebGame.udp.ExecuteCommand(PureUdpClient.CommandExpandSlots, this.buyShipSlotData, 49);
		int num = 0;
		if (this.buyShipSlotData.wantedSlot != 8)
		{
			NetworkScript.player.playerBelongings.ExpandInventory(ref num);
		}
		else
		{
			NetworkScript.player.playerBelongings.ExpandVault(ref num);
		}
		this.vaultSlotCnt = NetworkScript.player.playerBelongings.playerVaultSlots;
		this.inventorySlotCnt = NetworkScript.player.playerBelongings.playerInventorySlots;
		this.RefreshScrollButtons();
		this.ClearAmmoSlots();
		Inventory.ClearSlots(this);
		this.CreateInventorySlots();
		this.CreateExtrasTab();
		this.CreateWeaponsTab();
		this.CreateStructureTab();
		this.CreateTrash();
		Inventory.DrawPlaces(this);
		this.expandSlotWindow.Cancel();
	}

	private void ConfirmShipSlotExpand(EventHandlerParam prm)
	{
		int num = 0;
		SelectedCurrency selectedCurrency = (int)prm.customData;
		this.buyShipSlotData.paymentCurrency = (int)prm.customData;
		if (NetworkScript.player.playerBelongings.ExpandShipSlotNew(this.buyShipSlotData.wantedSlot, selectedCurrency, ref num))
		{
			playWebGame.udp.ExecuteCommand(PureUdpClient.CommandExpandSlots, this.buyShipSlotData, 49);
		}
		this.RefreshScrollButtons();
		this.ClearAmmoSlots();
		Inventory.ClearSlots(this);
		this.CreateInventorySlots();
		this.CreateExtrasTab();
		this.CreateWeaponsTab();
		this.CreateStructureTab();
		this.CreateTrash();
		this.DrawExpandShipSlots();
		Inventory.DrawPlaces(this);
		this.expandShipSlotWindow.Cancel();
	}

	public override void Create()
	{
		int num;
		this.IsMobile = false;
		this.inventorySlotCnt = NetworkScript.player.playerBelongings.playerInventorySlots;
		this.vaultSlotCnt = NetworkScript.player.playerBelongings.playerVaultSlots;
		ShipsTypeNet[] shipsTypeNetArray = StaticData.shipTypes;
		if (__ConfigWindow.<>f__am$cache1F == null)
		{
			__ConfigWindow.<>f__am$cache1F = new Func<ShipsTypeNet, bool>(null, (ShipsTypeNet t) => t.id == NetworkScript.player.playerBelongings.get_SelectedShip().shipTypeId);
		}
		ShipsTypeNet shipsTypeNet = Enumerable.FirstOrDefault<ShipsTypeNet>(Enumerable.Where<ShipsTypeNet>(shipsTypeNetArray, __ConfigWindow.<>f__am$cache1F));
		if (shipsTypeNet == null)
		{
			return;
		}
		base.SetBackgroundTexture("ConfigWindow", "ConfigWndBackground");
		base.PutToCenter();
		this.isHidden = false;
		this.zOrder = 210;
		Inventory.window = this;
		Inventory.isVaultMenuOpen = false;
		Inventory.isItemRerollMenuOpen = false;
		Inventory.isGuildVaultMenuOpen = false;
		Inventory.isRightClickActionEnable = true;
		Inventory.ConfigWndAfterRightClkAction = new Action(this, __ConfigWindow.Refresh);
		this.shipTexture = new GuiTexture();
		if (NetworkScript.player.vessel.galaxy.__galaxyId != 1000)
		{
			this.shipTexture.SetTexture("CfgMenuBg", NetworkScript.player.cfg.assetName);
		}
		else
		{
			this.shipTexture.SetTexture("TutorialWindow", "ShipTankT5");
		}
		this.shipTexture.X = 400f;
		this.shipTexture.Y = 90f;
		base.AddGuiElement(this.shipTexture);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("ConfigWindow", "btnWarBonuses");
		guiButtonFixed.X = 486f;
		guiButtonFixed.Y = 52f;
		guiButtonFixed.Caption = StaticData.Translate("key_new_universe_map_btn_faction_war");
		guiButtonFixed.FontSize = 12;
		guiButtonFixed._marginLeft = 10;
		guiButtonFixed.Alignment = 3;
		guiButtonFixed.SetColor(GuiNewStyleBar.aquamarineColor);
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, __ConfigWindow.OnFactionWarBtnClicked);
		guiButtonFixed.isEnabled = NetworkScript.player.playerBelongings.playerLevel >= 10;
		base.AddGuiElement(guiButtonFixed);
		this.forDelete.Add(guiButtonFixed);
		int num1 = 1;
		if (NetworkScript.player.playerBelongings.factionWarRerollBonus == 10)
		{
			this.DrawFactionWarbonus(StaticData.Translate("key_faction_war_bonus_better_item_drop"), num1);
			num1++;
		}
		if (NetworkScript.player.playerBelongings.factionWarRerollBonus != 0)
		{
			this.DrawFactionWarbonus(string.Format(StaticData.Translate("key_faction_war_bonus_reroll"), NetworkScript.player.playerBelongings.factionWarRerollBonus), num1);
			num1++;
		}
		if (NetworkScript.player.playerBelongings.factionWarXpBonus != 0)
		{
			this.DrawFactionWarbonus(string.Format(StaticData.Translate("key_faction_war_bonus_xp"), NetworkScript.player.playerBelongings.factionWarXpBonus), num1);
			num1++;
		}
		IEnumerator<KeyValuePair<byte, byte>> enumerator = NetworkScript.player.factionGalaxyOwnership.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<byte, byte> current = enumerator.get_Current();
				if (current.get_Value() != NetworkScript.player.vessel.fractionId)
				{
					continue;
				}
				switch (current.get_Key())
				{
					case 41:
					{
						this.DrawFactionWarbonus(string.Format(StaticData.Translate("key_faction_galaxy_bonus_xp_gain"), 20), num1);
						num1++;
						continue;
					}
					case 42:
					{
						this.DrawFactionWarbonus(string.Format(StaticData.Translate("key_faction_galaxy_bonus_resources_drop"), 100), num1);
						num1++;
						continue;
					}
					case 43:
					{
						this.DrawFactionWarbonus(string.Format(StaticData.Translate("key_faction_galaxy_bonus_sell_price"), 100), num1);
						num1++;
						continue;
					}
					case 44:
					{
						this.DrawFactionWarbonus(StaticData.Translate("key_faction_galaxy_bonus_better_drop"), num1);
						num1++;
						continue;
					}
					case 45:
					{
						this.DrawFactionWarbonus(string.Format(StaticData.Translate("key_faction_galaxy_bonus_research_point"), 50), num1);
						num1++;
						this.DrawFactionWarbonus(string.Format(StaticData.Translate("key_faction_galaxy_bonus_defending"), 100), num1);
						num1++;
						continue;
					}
					default:
					{
						continue;
					}
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
		this.shipStatsLbl = new GuiLabel()
		{
			text = StaticData.Translate("key_ship_cfg_stats"),
			boundries = new Rect(60f, 40f, 200f, 20f),
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 16,
			Font = GuiLabel.FontBold,
			Alignment = 4
		};
		base.AddGuiElement(this.shipStatsLbl);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = StaticData.Translate("key_ship_cfg_dmg"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(60f, 65f, 200f, 20f),
			FontSize = 12,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		int num2 = NetworkScript.player.vessel.cfg.skillDamage;
		this.CalculateDamageTotal(NetworkScript.player.cfg, out num);
		GuiLabel _white = new GuiLabel()
		{
			text = num.ToString("##,##0"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel.X, guiLabel.Y, 200f, 20f),
			FontSize = 12,
			Alignment = 5
		};
		_white.TextColor = Color.get_white();
		_white.isHoverAware = true;
		__ConfigWindow.ShipStatsHoverParam shipStatsHoverParam = new __ConfigWindow.ShipStatsHoverParam()
		{
			baseStat = (float)num2,
			bonusStat = (float)(num - num2),
			lable = StaticData.Translate("key_ship_cfg_dmg")
		};
		_white.tooltipWindowParam = shipStatsHoverParam;
		_white.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.DrawShipStatsTooltiop);
		base.AddGuiElement(_white);
		this.forDelete.Add(_white);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			text = StaticData.Translate("key_ship_cfg_corpus"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel.X, guiLabel.Y + 16f, 200f, 20f),
			FontSize = 12,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel1);
		this.forDelete.Add(guiLabel1);
		GuiLabel drawTooltipWindow = new GuiLabel()
		{
			text = NetworkScript.player.cfg.hitPointsMax.ToString("##,##0"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel.X, guiLabel1.Y, 200f, 20f),
			FontSize = 12,
			Alignment = 5
		};
		drawTooltipWindow.TextColor = Color.get_white();
		drawTooltipWindow.isHoverAware = true;
		shipStatsHoverParam = new __ConfigWindow.ShipStatsHoverParam()
		{
			baseStat = (float)shipsTypeNet.corpus,
			bonusStat = (float)(NetworkScript.player.cfg.hitPointsMax - shipsTypeNet.corpus),
			lable = StaticData.Translate("key_ship_cfg_corpus")
		};
		drawTooltipWindow.tooltipWindowParam = shipStatsHoverParam;
		drawTooltipWindow.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.DrawShipStatsTooltiop);
		base.AddGuiElement(drawTooltipWindow);
		this.forDelete.Add(drawTooltipWindow);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			text = StaticData.Translate("key_ship_cfg_shield"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel.X, guiLabel1.Y + 16f, 200f, 20f),
			FontSize = 12,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel2);
		this.forDelete.Add(guiLabel2);
		GuiLabel _white1 = new GuiLabel()
		{
			text = NetworkScript.player.cfg.shieldMax.ToString("##,##0"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel.X, guiLabel2.Y, 200f, 20f),
			FontSize = 12,
			Alignment = 5
		};
		_white1.TextColor = Color.get_white();
		_white1.isHoverAware = true;
		shipStatsHoverParam = new __ConfigWindow.ShipStatsHoverParam()
		{
			baseStat = (float)shipsTypeNet.shield,
			bonusStat = (float)(NetworkScript.player.cfg.shieldMax - shipsTypeNet.shield),
			lable = StaticData.Translate("key_ship_cfg_shield")
		};
		_white1.tooltipWindowParam = shipStatsHoverParam;
		_white1.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.DrawShipStatsTooltiop);
		base.AddGuiElement(_white1);
		this.forDelete.Add(_white1);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			text = StaticData.Translate("key_ship_cfg_shield_power"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel.X, _white1.Y + 16f, 200f, 20f),
			FontSize = 12,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel3);
		this.forDelete.Add(guiLabel3);
		GuiLabel drawTooltipWindow1 = new GuiLabel()
		{
			text = NetworkScript.player.cfg.shieldRepairPerSec.ToString("##,##0"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel.X, guiLabel3.Y, 200f, 20f),
			FontSize = 12,
			Alignment = 5
		};
		drawTooltipWindow1.TextColor = Color.get_white();
		drawTooltipWindow1.isHoverAware = true;
		shipStatsHoverParam = new __ConfigWindow.ShipStatsHoverParam()
		{
			baseStat = NetworkScript.player.cfg.shieldRepairPerSec,
			bonusStat = NetworkScript.player.cfg.damageReductionItems,
			lable = "shield_power"
		};
		drawTooltipWindow1.tooltipWindowParam = shipStatsHoverParam;
		drawTooltipWindow1.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.DrawShipStatsTooltiop);
		base.AddGuiElement(drawTooltipWindow1);
		this.forDelete.Add(drawTooltipWindow1);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			text = StaticData.Translate("key_ship_cfg_avoidance"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel.X, guiLabel3.Y + 16f, 200f, 20f),
			FontSize = 12,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel4);
		this.forDelete.Add(guiLabel4);
		GuiLabel _white2 = new GuiLabel()
		{
			text = NetworkScript.player.cfg.avoidanceMax.ToString("##,##0"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel.X, guiLabel4.Y, 200f, 20f),
			FontSize = 12,
			Alignment = 5
		};
		_white2.TextColor = Color.get_white();
		_white2.isHoverAware = true;
		shipStatsHoverParam = new __ConfigWindow.ShipStatsHoverParam()
		{
			baseStat = (float)shipsTypeNet.avoidance,
			bonusStat = NetworkScript.player.cfg.avoidanceMax - (float)shipsTypeNet.avoidance,
			lable = StaticData.Translate("key_ship_cfg_avoidance")
		};
		_white2.tooltipWindowParam = shipStatsHoverParam;
		_white2.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.DrawShipStatsTooltiop);
		base.AddGuiElement(_white2);
		this.forDelete.Add(_white2);
		GuiLabel guiLabel5 = new GuiLabel()
		{
			text = StaticData.Translate("key_ship_cfg_targeting"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel.X, guiLabel4.Y + 16f, 200f, 20f),
			FontSize = 12,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel5);
		this.forDelete.Add(guiLabel5);
		GuiLabel drawTooltipWindow2 = new GuiLabel()
		{
			text = NetworkScript.player.cfg.targeting.ToString("##,##0"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel.X, guiLabel5.Y, 200f, 20f),
			FontSize = 12,
			Alignment = 5
		};
		drawTooltipWindow2.TextColor = Color.get_white();
		drawTooltipWindow2.isHoverAware = true;
		shipStatsHoverParam = new __ConfigWindow.ShipStatsHoverParam()
		{
			baseStat = (float)shipsTypeNet.targeting,
			bonusStat = (float)(NetworkScript.player.cfg.targeting - shipsTypeNet.targeting),
			lable = StaticData.Translate("key_ship_cfg_targeting")
		};
		drawTooltipWindow2.tooltipWindowParam = shipStatsHoverParam;
		drawTooltipWindow2.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.DrawShipStatsTooltiop);
		base.AddGuiElement(drawTooltipWindow2);
		this.forDelete.Add(drawTooltipWindow2);
		GuiLabel guiLabel6 = new GuiLabel()
		{
			text = StaticData.Translate("key_ship_cfg_speed"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel.X, guiLabel5.Y + 16f, 200f, 20f),
			FontSize = 12,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel6);
		this.forDelete.Add(guiLabel6);
		GuiLabel _white3 = new GuiLabel()
		{
			text = NetworkScript.player.playerBelongings.get_SelectedShip().Speed.ToString("##,##0"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel.X, guiLabel6.Y, 200f, 20f),
			FontSize = 12,
			Alignment = 5
		};
		_white3.TextColor = Color.get_white();
		_white3.isHoverAware = true;
		shipStatsHoverParam = new __ConfigWindow.ShipStatsHoverParam()
		{
			baseStat = (float)shipsTypeNet.speed,
			bonusStat = (float)(NetworkScript.player.playerBelongings.get_SelectedShip().Speed - shipsTypeNet.speed),
			lable = StaticData.Translate("key_ship_cfg_speed")
		};
		_white3.tooltipWindowParam = shipStatsHoverParam;
		_white3.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.DrawShipStatsTooltiop);
		base.AddGuiElement(_white3);
		this.forDelete.Add(_white3);
		GuiLabel guiLabel7 = new GuiLabel()
		{
			text = StaticData.Translate("key_ship_cfg_cargo"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel.X, guiLabel6.Y + 16f, 200f, 20f),
			FontSize = 12,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel7);
		this.forDelete.Add(guiLabel7);
		GuiLabel drawTooltipWindow3 = new GuiLabel()
		{
			text = NetworkScript.player.cfg.cargoMax.ToString("##,##0"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel.X, guiLabel7.Y, 200f, 20f),
			FontSize = 12,
			Alignment = 5
		};
		drawTooltipWindow3.TextColor = Color.get_white();
		drawTooltipWindow3.isHoverAware = true;
		shipStatsHoverParam = new __ConfigWindow.ShipStatsHoverParam()
		{
			baseStat = (float)shipsTypeNet.cargo,
			bonusStat = (float)(NetworkScript.player.cfg.cargoMax - shipsTypeNet.cargo),
			lable = StaticData.Translate("key_ship_cfg_cargo")
		};
		drawTooltipWindow3.tooltipWindowParam = shipStatsHoverParam;
		drawTooltipWindow3.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.DrawShipStatsTooltiop);
		base.AddGuiElement(drawTooltipWindow3);
		this.forDelete.Add(drawTooltipWindow3);
		this.tab1Texture = new GuiTexture();
		switch (this.selectedTab)
		{
			case __ConfigWindow.ConfigWindowTab.Inventory:
			{
				this.tab1Texture.SetTexture("ConfigWindow", "ConfigWindowTab1");
				break;
			}
			case __ConfigWindow.ConfigWindowTab.Cargo:
			{
				this.tab1Texture.SetTexture("ConfigWindow", "ConfigWindowTab2");
				break;
			}
			case __ConfigWindow.ConfigWindowTab.Vault:
			{
				this.tab1Texture.SetTexture("ConfigWindow", "ConfigWindowTab3");
				break;
			}
		}
		this.tab1Texture.X = 0f;
		this.tab1Texture.Y = 0f;
		base.AddGuiElement(this.tab1Texture);
		this.tab1Button = new GuiButton()
		{
			boundries = new Rect(34f, 230f, 75f, 20f),
			Clicked = new Action<EventHandlerParam>(this, __ConfigWindow.Tab1Clicked),
			Caption = StaticData.Translate("key_ship_cfg_tab_inventory"),
			FontSize = 12
		};
		if (this.selectedTab != __ConfigWindow.ConfigWindowTab.Inventory)
		{
			this.tab1Button.textColorHover = GuiNewStyleBar.orangeColor;
			this.tab1Button.textColorNormal = GuiNewStyleBar.blueColor;
		}
		else
		{
			this.tab1Button.textColorHover = GuiNewStyleBar.orangeColor;
			this.tab1Button.textColorNormal = GuiNewStyleBar.orangeColor;
		}
		this.tab1Button.Alignment = 4;
		base.AddGuiElement(this.tab1Button);
		this.tab2Button = new GuiButton()
		{
			boundries = new Rect(115f, 230f, 75f, 20f),
			Clicked = new Action<EventHandlerParam>(this, __ConfigWindow.Tab2Clicked),
			Caption = StaticData.Translate("key_ship_cfg_tab_cargo"),
			FontSize = 12
		};
		if (this.selectedTab != __ConfigWindow.ConfigWindowTab.Cargo)
		{
			this.tab2Button.textColorHover = GuiNewStyleBar.orangeColor;
			this.tab2Button.textColorNormal = GuiNewStyleBar.blueColor;
		}
		else
		{
			this.tab2Button.textColorHover = GuiNewStyleBar.orangeColor;
			this.tab2Button.textColorNormal = GuiNewStyleBar.orangeColor;
		}
		this.tab2Button.Alignment = 4;
		base.AddGuiElement(this.tab2Button);
		this.tab3Button = new GuiButton()
		{
			boundries = new Rect(195f, 230f, 75f, 20f),
			Clicked = new Action<EventHandlerParam>(this, __ConfigWindow.Tab3Clicked),
			Caption = StaticData.Translate("key_ship_cfg_tab_vault"),
			FontSize = 12
		};
		if (this.selectedTab != __ConfigWindow.ConfigWindowTab.Vault)
		{
			this.tab3Button.textColorHover = GuiNewStyleBar.orangeColor;
			this.tab3Button.textColorNormal = GuiNewStyleBar.blueColor;
		}
		else
		{
			this.tab3Button.textColorHover = GuiNewStyleBar.orangeColor;
			this.tab3Button.textColorNormal = GuiNewStyleBar.orangeColor;
		}
		this.tab3Button.Alignment = 4;
		base.AddGuiElement(this.tab3Button);
		this.scrollUpButton = new GuiButtonFixed();
		this.scrollUpButton.SetTexture("ConfigWindow", "ScrollArrowUp");
		this.scrollUpButton.X = 42f;
		this.scrollUpButton.Y = 252f;
		this.scrollUpButton.Caption = string.Empty;
		this.scrollUpButton.Clicked = new Action<EventHandlerParam>(this, __ConfigWindow.OnScrollUp);
		this.scrollUpButton.SetLeftClickSound("Sounds", "minus");
		base.AddGuiElement(this.scrollUpButton);
		this.scrollDownButton = new GuiButtonFixed();
		this.scrollDownButton.SetTexture("ConfigWindow", "ScrollArrowDown");
		this.scrollDownButton.X = 42f;
		this.scrollDownButton.Y = 507f;
		this.scrollDownButton.Caption = string.Empty;
		this.scrollDownButton.Clicked = new Action<EventHandlerParam>(this, __ConfigWindow.OnScrollDown);
		this.scrollDownButton.SetLeftClickSound("Sounds", "plus");
		base.AddGuiElement(this.scrollDownButton);
		this.scrollLabel = new GuiLabel()
		{
			text = string.Format("0-16{0}72", StaticData.Translate("key_ship_cfg_tab_separator")),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(44f, 254f, 100f, 30f),
			FontSize = 10,
			Alignment = 3
		};
		base.AddGuiElement(this.scrollLabel);
		this.RefreshScrollButtons();
		this.laserSlotsLabel = new GuiLabel()
		{
			boundries = new Rect(302f, 70f, 120f, 12f),
			text = StaticData.Translate("key_ship_cfg_slots_laser"),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.laserSlotsLabel);
		this.plasmaSlotsLabel = new GuiLabel()
		{
			boundries = new Rect(302f, 140f, 120f, 12f),
			text = StaticData.Translate("key_ship_cfg_slots_plasma"),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.plasmaSlotsLabel);
		this.ionSlotsLabel = new GuiLabel()
		{
			boundries = new Rect(302f, 210f, 120f, 12f),
			text = StaticData.Translate("key_ship_cfg_slots_ion"),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.ionSlotsLabel);
		Inventory.secondaryDropHandler = new Action<AndromedaGuiDragDropPlace, AndromedaGuiDragDropPlace>(this, __ConfigWindow.DropHandler);
		Inventory.closeStackablePopUp = new Action(this, __ConfigWindow.Refresh);
		Inventory.ClearSlots(this);
		this.CreateInventorySlots();
		this.CreateWeaponsTab();
		this.CreateStructureTab();
		this.CreateExtrasTab();
		this.CreateTrash();
		this.DrawExpandShipSlots();
		Inventory.DrawPlaces(this);
		GuiLabel guiLabel8 = new GuiLabel()
		{
			boundries = new Rect(300f, 42f, 105f, 15f),
			text = StaticData.Translate("key_ship_cfg_lbl_weapons"),
			FontSize = 15,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel8);
		this.mainSlots.Add(guiLabel8);
		GuiLabel guiLabel9 = new GuiLabel()
		{
			boundries = new Rect(700f, 194f, 105f, 15f),
			text = StaticData.Translate("key_ship_cfg_lbl_corpus"),
			FontSize = 15,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel9);
		this.mainSlots.Add(guiLabel9);
		GuiLabel guiLabel10 = new GuiLabel()
		{
			boundries = new Rect(300f, 375f, 105f, 15f),
			text = StaticData.Translate("key_ship_cfg_lbl_shiel"),
			FontSize = 15,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel10);
		this.mainSlots.Add(guiLabel10);
		GuiLabel guiLabel11 = new GuiLabel()
		{
			boundries = new Rect(700f, 59f, 105f, 15f),
			text = StaticData.Translate("key_ship_cfg_lbl_engine"),
			FontSize = 15,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel11);
		this.mainSlots.Add(guiLabel11);
		GuiLabel guiLabel12 = new GuiLabel()
		{
			boundries = new Rect(300f, 285f, 105f, 15f),
			text = StaticData.Translate("key_ship_cfg_lbl_extended"),
			FontSize = 15,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel12);
		this.mainSlots.Add(guiLabel12);
		GuiLabel guiLabel13 = new GuiLabel()
		{
			boundries = new Rect(700f, 378f, 105f, 15f),
			text = StaticData.Translate("key_ship_cfg_lbl_extras"),
			FontSize = 15,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel13);
		this.mainSlots.Add(guiLabel13);
	}

	private void CreateExtrasTab()
	{
		__ConfigWindow.<CreateExtrasTab>c__AnonStorey87 variable = null;
		int num = (!NetworkScript.player.playerBelongings.haveExtendetExtraOne ? 6 : 7);
		if (num == 7 && NetworkScript.player.playerBelongings.haveExtendetExtraTwo)
		{
			num = 8;
		}
		List<SlotItem> list = NetworkScript.player.playerBelongings.playerItems.slotItems;
		if (__ConfigWindow.<>f__am$cache59 == null)
		{
			__ConfigWindow.<>f__am$cache59 = new Func<SlotItem, bool>(null, (SlotItem it) => (it.get_SlotType() != 13 || !PlayerItems.IsExtra(it.get_ItemType()) ? false : it.get_ShipId() == NetworkScript.player.playerBelongings.selectedShipId));
		}
		IEnumerable<SlotItem> enumerable = Enumerable.Where<SlotItem>(list, __ConfigWindow.<>f__am$cache59);
		for (int i = 0; i < num; i++)
		{
			AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = new AndromedaGuiDragDropPlace();
			SlotItem slotItem = Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable, new Func<SlotItem, bool>(variable, (SlotItem it) => it.get_Slot() == this.i)));
			andromedaGuiDragDropPlace.location = 13;
			andromedaGuiDragDropPlace.position = new Vector2((float)(640 + i / 2 * 60), (float)(400 + i % 2 * 56));
			andromedaGuiDragDropPlace.slot = (byte)i;
			andromedaGuiDragDropPlace.dropZonePosition = andromedaGuiDragDropPlace.position;
			andromedaGuiDragDropPlace.txFrameIdle = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotIdle");
			andromedaGuiDragDropPlace.txFrameDragSource = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotHvr");
			andromedaGuiDragDropPlace.txFrameDropTarget = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotActive");
			andromedaGuiDragDropPlace.txFrameDropTargetHover = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotHvr");
			andromedaGuiDragDropPlace.item = slotItem;
			andromedaGuiDragDropPlace.isEmpty = slotItem == null;
			andromedaGuiDragDropPlace.shipId = NetworkScript.player.playerBelongings.selectedShipId;
			andromedaGuiDragDropPlace.idleItemTextureSize = new Vector2(51f, 35f);
			Inventory.places.Add(andromedaGuiDragDropPlace);
		}
	}

	private void CreateInventorySlots()
	{
		__ConfigWindow.<CreateInventorySlots>c__AnonStorey80 variable = null;
		foreach (GuiElement lockedInventorySlot in this.lockedInventorySlots)
		{
			base.RemoveGuiElement(lockedInventorySlot);
		}
		this.lockedInventorySlots.Clear();
		foreach (GuiElement lockedVaultSlot in this.lockedVaultSlots)
		{
			base.RemoveGuiElement(lockedVaultSlot);
		}
		this.lockedVaultSlots.Clear();
		IEnumerable<SlotItem> enumerable = null;
		switch (this.selectedTab)
		{
			case __ConfigWindow.ConfigWindowTab.Inventory:
			{
				List<SlotItem> list = NetworkScript.player.playerBelongings.playerItems.slotItems;
				if (__ConfigWindow.<>f__am$cache22 == null)
				{
					__ConfigWindow.<>f__am$cache22 = new Func<SlotItem, bool>(null, (SlotItem t) => t.get_SlotType() == 1);
				}
				enumerable = Enumerable.Where<SlotItem>(list, __ConfigWindow.<>f__am$cache22);
				break;
			}
			case __ConfigWindow.ConfigWindowTab.Cargo:
			{
				List<SlotItem> list1 = NetworkScript.player.playerBelongings.playerItems.slotItems;
				if (__ConfigWindow.<>f__am$cache23 == null)
				{
					__ConfigWindow.<>f__am$cache23 = new Func<SlotItem, bool>(null, (SlotItem t) => t.get_SlotType() == 3);
				}
				enumerable = Enumerable.Where<SlotItem>(list1, __ConfigWindow.<>f__am$cache23);
				break;
			}
			case __ConfigWindow.ConfigWindowTab.Vault:
			{
				List<SlotItem> list2 = NetworkScript.player.playerBelongings.playerItems.slotItems;
				if (__ConfigWindow.<>f__am$cache24 == null)
				{
					__ConfigWindow.<>f__am$cache24 = new Func<SlotItem, bool>(null, (SlotItem t) => t.get_SlotType() == 2);
				}
				enumerable = Enumerable.Where<SlotItem>(list2, __ConfigWindow.<>f__am$cache24);
				break;
			}
		}
		int num = 0;
		int num1 = 0;
		ItemLocation itemLocation = 1;
		switch (this.selectedTab)
		{
			case __ConfigWindow.ConfigWindowTab.Inventory:
			{
				num = this.inventoryScrollIndex;
				num1 = this.inventorySlotCnt;
				itemLocation = 1;
				break;
			}
			case __ConfigWindow.ConfigWindowTab.Cargo:
			{
				num = 0;
				num1 = 8;
				itemLocation = 3;
				break;
			}
			case __ConfigWindow.ConfigWindowTab.Vault:
			{
				num = this.vaultScrollIndex;
				num1 = this.vaultSlotCnt;
				itemLocation = 2;
				break;
			}
		}
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
				andromedaGuiDragDropPlace.position = new Vector2((float)(44 + num3 % 4 * 58), (float)(282 + num3 / 4 * 58));
				andromedaGuiDragDropPlace.dropZonePosition = andromedaGuiDragDropPlace.position;
				SlotItem slotItem = Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable, new Func<SlotItem, bool>(variable, (SlotItem si) => si.get_Slot() == this.i)));
				andromedaGuiDragDropPlace.isEmpty = slotItem == null;
				andromedaGuiDragDropPlace.item = slotItem;
				andromedaGuiDragDropPlace.txFrameDragSource = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotHvr");
				andromedaGuiDragDropPlace.txFrameDropTarget = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotActive");
				andromedaGuiDragDropPlace.txFrameDropTargetHover = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotHvr");
				andromedaGuiDragDropPlace.idleItemTextureSize = new Vector2(51f, 35f);
				if (slotItem != null && StaticData.allTypes.get_Item(slotItem.get_ItemType()) != null && StaticData.allTypes.get_Item(slotItem.get_ItemType()).levelRestriction > NetworkScript.player.playerBelongings.playerLevel)
				{
					andromedaGuiDragDropPlace.txFrameIdle = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotRed");
				}
				switch (this.selectedTab)
				{
					case __ConfigWindow.ConfigWindowTab.Inventory:
					{
						andromedaGuiDragDropPlace.location = 1;
						break;
					}
					case __ConfigWindow.ConfigWindowTab.Cargo:
					{
						andromedaGuiDragDropPlace.location = 3;
						break;
					}
					case __ConfigWindow.ConfigWindowTab.Vault:
					{
						andromedaGuiDragDropPlace.location = 2;
						break;
					}
				}
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
			this.DrawExpandInventorySlots((float)(42 + num4 % 4 * 58), (float)(280 + num4 / 4 * 58), itemLocation);
		}
	}

	private void CreateStructureTab()
	{
		__ConfigWindow.<CreateStructureTab>c__AnonStorey83 variable = null;
		__ConfigWindow.<CreateStructureTab>c__AnonStorey84 variable1 = null;
		__ConfigWindow.<CreateStructureTab>c__AnonStorey85 variable2 = null;
		__ConfigWindow.<CreateStructureTab>c__AnonStorey86 variable3 = null;
		List<SlotItem> list = NetworkScript.player.playerBelongings.playerItems.slotItems;
		if (__ConfigWindow.<>f__am$cache54 == null)
		{
			__ConfigWindow.<>f__am$cache54 = new Func<SlotItem, bool>(null, (SlotItem it) => (!PlayerItems.IsStructure(it.get_ItemType()) ? false : it.get_ShipId() == NetworkScript.player.playerBelongings.selectedShipId));
		}
		IEnumerable<SlotItem> enumerable = Enumerable.Where<SlotItem>(list, __ConfigWindow.<>f__am$cache54);
		IEnumerable<SlotItem> enumerable1 = enumerable;
		if (__ConfigWindow.<>f__am$cache55 == null)
		{
			__ConfigWindow.<>f__am$cache55 = new Func<SlotItem, bool>(null, (SlotItem t) => t.get_SlotType() == 11);
		}
		SlotItem[] array = Enumerable.ToArray<SlotItem>(Enumerable.Where<SlotItem>(enumerable1, __ConfigWindow.<>f__am$cache55));
		IEnumerable<SlotItem> enumerable2 = enumerable;
		if (__ConfigWindow.<>f__am$cache56 == null)
		{
			__ConfigWindow.<>f__am$cache56 = new Func<SlotItem, bool>(null, (SlotItem t) => t.get_SlotType() == 10);
		}
		SlotItem[] slotItemArray = Enumerable.ToArray<SlotItem>(Enumerable.Where<SlotItem>(enumerable2, __ConfigWindow.<>f__am$cache56));
		IEnumerable<SlotItem> enumerable3 = enumerable;
		if (__ConfigWindow.<>f__am$cache57 == null)
		{
			__ConfigWindow.<>f__am$cache57 = new Func<SlotItem, bool>(null, (SlotItem t) => t.get_SlotType() == 9);
		}
		SlotItem[] array1 = Enumerable.ToArray<SlotItem>(Enumerable.Where<SlotItem>(enumerable3, __ConfigWindow.<>f__am$cache57));
		IEnumerable<SlotItem> enumerable4 = enumerable;
		if (__ConfigWindow.<>f__am$cache58 == null)
		{
			__ConfigWindow.<>f__am$cache58 = new Func<SlotItem, bool>(null, (SlotItem t) => t.get_SlotType() == 12);
		}
		SlotItem[] slotItemArray1 = Enumerable.ToArray<SlotItem>(Enumerable.Where<SlotItem>(enumerable4, __ConfigWindow.<>f__am$cache58));
		int num = (!NetworkScript.player.playerBelongings.haveExtendetCorpusOne ? 2 : 3);
		for (int i = 0; i < num; i++)
		{
			AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = new AndromedaGuiDragDropPlace();
			SlotItem slotItem = Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(array, new Func<SlotItem, bool>(variable, (SlotItem it) => it.get_Slot() == this.i)));
			andromedaGuiDragDropPlace.location = 11;
			andromedaGuiDragDropPlace.position = new Vector2((float)(700 + i * 60), 218f);
			andromedaGuiDragDropPlace.slot = (byte)i;
			andromedaGuiDragDropPlace.dropZonePosition = andromedaGuiDragDropPlace.position;
			andromedaGuiDragDropPlace.txFrameIdle = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotIdle");
			andromedaGuiDragDropPlace.txFrameDragSource = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotHvr");
			andromedaGuiDragDropPlace.txFrameDropTarget = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotActive");
			andromedaGuiDragDropPlace.txFrameDropTargetHover = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotHvr");
			andromedaGuiDragDropPlace.item = slotItem;
			andromedaGuiDragDropPlace.isEmpty = slotItem == null;
			andromedaGuiDragDropPlace.shipId = NetworkScript.player.playerBelongings.selectedShipId;
			andromedaGuiDragDropPlace.idleItemTextureSize = new Vector2(51f, 35f);
			Inventory.places.Add(andromedaGuiDragDropPlace);
		}
		int num1 = (!NetworkScript.player.playerBelongings.haveExtendetEngineOne ? 2 : 3);
		for (int j = 0; j < num1; j++)
		{
			AndromedaGuiDragDropPlace vector2 = new AndromedaGuiDragDropPlace();
			SlotItem slotItem1 = Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(slotItemArray1, new Func<SlotItem, bool>(variable1, (SlotItem it) => it.get_Slot() == this.i)));
			vector2.location = 12;
			vector2.position = new Vector2((float)(700 + j * 60), 83f);
			vector2.slot = (byte)j;
			vector2.dropZonePosition = vector2.position;
			vector2.txFrameIdle = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotIdle");
			vector2.txFrameDragSource = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotHvr");
			vector2.txFrameDropTarget = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotActive");
			vector2.txFrameDropTargetHover = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotHvr");
			vector2.item = slotItem1;
			vector2.isEmpty = slotItem1 == null;
			vector2.shipId = NetworkScript.player.playerBelongings.selectedShipId;
			vector2.idleItemTextureSize = new Vector2(51f, 35f);
			Inventory.places.Add(vector2);
		}
		int num2 = (!NetworkScript.player.playerBelongings.haveExtendetShielOne ? 2 : 3);
		for (int k = 0; k < num2; k++)
		{
			AndromedaGuiDragDropPlace fromStaticSet = new AndromedaGuiDragDropPlace();
			SlotItem slotItem2 = Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(slotItemArray, new Func<SlotItem, bool>(variable2, (SlotItem it) => it.get_Slot() == this.i)));
			fromStaticSet.location = 10;
			fromStaticSet.position = new Vector2((float)(302 + k * 60), 400f);
			fromStaticSet.slot = (byte)k;
			fromStaticSet.dropZonePosition = fromStaticSet.position;
			fromStaticSet.txFrameIdle = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotIdle");
			fromStaticSet.txFrameDragSource = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotHvr");
			fromStaticSet.txFrameDropTarget = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotActive");
			fromStaticSet.txFrameDropTargetHover = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotHvr");
			fromStaticSet.item = slotItem2;
			fromStaticSet.isEmpty = slotItem2 == null;
			fromStaticSet.shipId = NetworkScript.player.playerBelongings.selectedShipId;
			fromStaticSet.idleItemTextureSize = new Vector2(51f, 35f);
			Inventory.places.Add(fromStaticSet);
		}
		int num3 = (!NetworkScript.player.playerBelongings.haveExtendetAnyOne ? 2 : 3);
		for (int l = 0; l < num3; l++)
		{
			AndromedaGuiDragDropPlace andromedaGuiDragDropPlace1 = new AndromedaGuiDragDropPlace();
			SlotItem slotItem3 = Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(array1, new Func<SlotItem, bool>(variable3, (SlotItem it) => it.get_Slot() == this.i)));
			andromedaGuiDragDropPlace1.location = 9;
			andromedaGuiDragDropPlace1.position = new Vector2((float)(302 + l * 60), 310f);
			andromedaGuiDragDropPlace1.slot = (byte)l;
			andromedaGuiDragDropPlace1.dropZonePosition = andromedaGuiDragDropPlace1.position;
			andromedaGuiDragDropPlace1.txFrameIdle = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotIdle");
			andromedaGuiDragDropPlace1.txFrameDragSource = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotHvr");
			andromedaGuiDragDropPlace1.txFrameDropTarget = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotActive");
			andromedaGuiDragDropPlace1.txFrameDropTargetHover = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotHvr");
			andromedaGuiDragDropPlace1.item = slotItem3;
			andromedaGuiDragDropPlace1.isEmpty = slotItem3 == null;
			andromedaGuiDragDropPlace1.shipId = NetworkScript.player.playerBelongings.selectedShipId;
			andromedaGuiDragDropPlace1.idleItemTextureSize = new Vector2(51f, 35f);
			Inventory.places.Add(andromedaGuiDragDropPlace1);
		}
	}

	private void CreateTrash()
	{
		AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = new AndromedaGuiDragDropPlace();
		SlotItem slotItem = null;
		andromedaGuiDragDropPlace.location = 14;
		andromedaGuiDragDropPlace.position = new Vector2(291f, 471f);
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

	private void CreateWeaponsTab()
	{
		__ConfigWindow.<CreateWeaponsTab>c__AnonStorey82 variable = null;
		int num = 0;
		this.ammoSlots.Clear();
		this.ammoButtons.Clear();
		int num1 = NetworkScript.player.playerBelongings.selectedShipId;
		IEnumerable<SlotItem> enumerable = Enumerable.Where<SlotItem>(NetworkScript.player.playerBelongings.playerItems.slotItems, new Func<SlotItem, bool>(variable, (SlotItem si) => (si.get_ShipId() != this.shipId || si.get_SlotType() < 6 || si.get_SlotType() > 8 || si.get_ItemType() < PlayerItems.TypeWeaponLaserTire1 ? false : si.get_ItemType() <= PlayerItems.TypeWeaponIonTire5)));
		string shipTitle = NetworkScript.player.playerBelongings.get_SelectedShip().ShipTitle;
		if (shipTitle != null)
		{
			if (__ConfigWindow.<>f__switch$map2 == null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(16);
				dictionary.Add("key_ship_type_Mosquito", 0);
				dictionary.Add("key_ship_type_Serpent", 1);
				dictionary.Add("key_ship_type_Vindicator", 2);
				dictionary.Add("key_ship_type_Locust", 3);
				dictionary.Add("key_ship_type_Mantis", 4);
				dictionary.Add("key_ship_type_Crusader", 5);
				dictionary.Add("key_ship_type_Destroyer", 6);
				dictionary.Add("key_ship_type_Boar", 7);
				dictionary.Add("key_ship_type_Ravager", 8);
				dictionary.Add("key_ship_type_Cormorant", 9);
				dictionary.Add("key_ship_type_Sentinel", 10);
				dictionary.Add("key_ship_type_Vulture", 11);
				dictionary.Add("key_ship_type_Red_Dragon", 11);
				dictionary.Add("key_ship_type_Tiger", 11);
				dictionary.Add("key_ship_type_Nemesis", 11);
				dictionary.Add("key_ship_type_Viper", 12);
				__ConfigWindow.<>f__switch$map2 = dictionary;
			}
			if (__ConfigWindow.<>f__switch$map2.TryGetValue(shipTitle, ref num))
			{
				switch (num)
				{
					case 0:
					{
						this.laserSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_laser");
						this.plasmaSlotsLabel.text = string.Empty;
						this.ionSlotsLabel.text = string.Empty;
						IEnumerable<SlotItem> enumerable1 = enumerable;
						if (__ConfigWindow.<>f__am$cache26 == null)
						{
							__ConfigWindow.<>f__am$cache26 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 0);
						}
						this.DrawWeaponSlot1(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable1, __ConfigWindow.<>f__am$cache26)));
						break;
					}
					case 1:
					{
						this.laserSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_laser");
						this.plasmaSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_plasma");
						this.ionSlotsLabel.text = "Ion Slots";
						IEnumerable<SlotItem> enumerable2 = enumerable;
						if (__ConfigWindow.<>f__am$cache27 == null)
						{
							__ConfigWindow.<>f__am$cache27 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 0);
						}
						this.DrawWeaponSlot1(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable2, __ConfigWindow.<>f__am$cache27)));
						IEnumerable<SlotItem> enumerable3 = enumerable;
						if (__ConfigWindow.<>f__am$cache28 == null)
						{
							__ConfigWindow.<>f__am$cache28 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 1);
						}
						this.DrawWeaponSlot2(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable3, __ConfigWindow.<>f__am$cache28)));
						IEnumerable<SlotItem> enumerable4 = enumerable;
						if (__ConfigWindow.<>f__am$cache29 == null)
						{
							__ConfigWindow.<>f__am$cache29 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 3);
						}
						this.DrawWeaponSlot4(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable4, __ConfigWindow.<>f__am$cache29)));
						break;
					}
					case 2:
					{
						this.laserSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_laser");
						this.plasmaSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_plasma");
						this.ionSlotsLabel.text = string.Empty;
						IEnumerable<SlotItem> enumerable5 = enumerable;
						if (__ConfigWindow.<>f__am$cache2A == null)
						{
							__ConfigWindow.<>f__am$cache2A = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 0);
						}
						this.DrawWeaponSlot1(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable5, __ConfigWindow.<>f__am$cache2A)));
						IEnumerable<SlotItem> enumerable6 = enumerable;
						if (__ConfigWindow.<>f__am$cache2B == null)
						{
							__ConfigWindow.<>f__am$cache2B = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 1);
						}
						this.DrawWeaponSlot2(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable6, __ConfigWindow.<>f__am$cache2B)));
						break;
					}
					case 3:
					{
						this.laserSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_laser");
						this.plasmaSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_plasma");
						this.ionSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_ion");
						IEnumerable<SlotItem> enumerable7 = enumerable;
						if (__ConfigWindow.<>f__am$cache2C == null)
						{
							__ConfigWindow.<>f__am$cache2C = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 0);
						}
						this.DrawWeaponSlot1(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable7, __ConfigWindow.<>f__am$cache2C)));
						IEnumerable<SlotItem> enumerable8 = enumerable;
						if (__ConfigWindow.<>f__am$cache2D == null)
						{
							__ConfigWindow.<>f__am$cache2D = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 1);
						}
						this.DrawWeaponSlot2(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable8, __ConfigWindow.<>f__am$cache2D)));
						IEnumerable<SlotItem> enumerable9 = enumerable;
						if (__ConfigWindow.<>f__am$cache2E == null)
						{
							__ConfigWindow.<>f__am$cache2E = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 3);
						}
						this.DrawWeaponSlot4(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable9, __ConfigWindow.<>f__am$cache2E)));
						IEnumerable<SlotItem> enumerable10 = enumerable;
						if (__ConfigWindow.<>f__am$cache2F == null)
						{
							__ConfigWindow.<>f__am$cache2F = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 4);
						}
						this.DrawWeaponSlot5(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable10, __ConfigWindow.<>f__am$cache2F)));
						IEnumerable<SlotItem> enumerable11 = enumerable;
						if (__ConfigWindow.<>f__am$cache30 == null)
						{
							__ConfigWindow.<>f__am$cache30 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 5);
						}
						this.DrawWeaponSlot6(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable11, __ConfigWindow.<>f__am$cache30)));
						break;
					}
					case 4:
					{
						this.laserSlotsLabel.text = string.Empty;
						this.plasmaSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_plasma");
						this.ionSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_ion");
						IEnumerable<SlotItem> enumerable12 = enumerable;
						if (__ConfigWindow.<>f__am$cache31 == null)
						{
							__ConfigWindow.<>f__am$cache31 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 1);
						}
						this.DrawWeaponSlot2(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable12, __ConfigWindow.<>f__am$cache31)));
						IEnumerable<SlotItem> enumerable13 = enumerable;
						if (__ConfigWindow.<>f__am$cache32 == null)
						{
							__ConfigWindow.<>f__am$cache32 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 3);
						}
						this.DrawWeaponSlot4(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable13, __ConfigWindow.<>f__am$cache32)));
						IEnumerable<SlotItem> enumerable14 = enumerable;
						if (__ConfigWindow.<>f__am$cache33 == null)
						{
							__ConfigWindow.<>f__am$cache33 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 5);
						}
						this.DrawWeaponSlot6(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable14, __ConfigWindow.<>f__am$cache33)));
						break;
					}
					case 5:
					{
						this.laserSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_laser");
						this.plasmaSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_plasma");
						this.ionSlotsLabel.text = string.Empty;
						IEnumerable<SlotItem> enumerable15 = enumerable;
						if (__ConfigWindow.<>f__am$cache34 == null)
						{
							__ConfigWindow.<>f__am$cache34 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 0);
						}
						this.DrawWeaponSlot1(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable15, __ConfigWindow.<>f__am$cache34)));
						IEnumerable<SlotItem> enumerable16 = enumerable;
						if (__ConfigWindow.<>f__am$cache35 == null)
						{
							__ConfigWindow.<>f__am$cache35 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 1);
						}
						this.DrawWeaponSlot2(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable16, __ConfigWindow.<>f__am$cache35)));
						IEnumerable<SlotItem> enumerable17 = enumerable;
						if (__ConfigWindow.<>f__am$cache36 == null)
						{
							__ConfigWindow.<>f__am$cache36 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 2);
						}
						this.DrawWeaponSlot3(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable17, __ConfigWindow.<>f__am$cache36)));
						break;
					}
					case 6:
					{
						this.laserSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_laser");
						this.plasmaSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_plasma");
						this.ionSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_ion");
						IEnumerable<SlotItem> enumerable18 = enumerable;
						if (__ConfigWindow.<>f__am$cache37 == null)
						{
							__ConfigWindow.<>f__am$cache37 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 0);
						}
						this.DrawWeaponSlot1(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable18, __ConfigWindow.<>f__am$cache37)));
						IEnumerable<SlotItem> enumerable19 = enumerable;
						if (__ConfigWindow.<>f__am$cache38 == null)
						{
							__ConfigWindow.<>f__am$cache38 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 1);
						}
						this.DrawWeaponSlot2(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable19, __ConfigWindow.<>f__am$cache38)));
						IEnumerable<SlotItem> enumerable20 = enumerable;
						if (__ConfigWindow.<>f__am$cache39 == null)
						{
							__ConfigWindow.<>f__am$cache39 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 2);
						}
						this.DrawWeaponSlot3(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable20, __ConfigWindow.<>f__am$cache39)));
						IEnumerable<SlotItem> enumerable21 = enumerable;
						if (__ConfigWindow.<>f__am$cache3A == null)
						{
							__ConfigWindow.<>f__am$cache3A = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 3);
						}
						this.DrawWeaponSlot4(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable21, __ConfigWindow.<>f__am$cache3A)));
						IEnumerable<SlotItem> enumerable22 = enumerable;
						if (__ConfigWindow.<>f__am$cache3B == null)
						{
							__ConfigWindow.<>f__am$cache3B = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 5);
						}
						this.DrawWeaponSlot6(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable22, __ConfigWindow.<>f__am$cache3B)));
						break;
					}
					case 7:
					{
						this.laserSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_laser");
						this.plasmaSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_plasma");
						this.ionSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_ion");
						IEnumerable<SlotItem> enumerable23 = enumerable;
						if (__ConfigWindow.<>f__am$cache3C == null)
						{
							__ConfigWindow.<>f__am$cache3C = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 0);
						}
						this.DrawWeaponSlot1(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable23, __ConfigWindow.<>f__am$cache3C)));
						IEnumerable<SlotItem> enumerable24 = enumerable;
						if (__ConfigWindow.<>f__am$cache3D == null)
						{
							__ConfigWindow.<>f__am$cache3D = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 1);
						}
						this.DrawWeaponSlot2(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable24, __ConfigWindow.<>f__am$cache3D)));
						IEnumerable<SlotItem> enumerable25 = enumerable;
						if (__ConfigWindow.<>f__am$cache3E == null)
						{
							__ConfigWindow.<>f__am$cache3E = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 2);
						}
						this.DrawWeaponSlot3(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable25, __ConfigWindow.<>f__am$cache3E)));
						IEnumerable<SlotItem> enumerable26 = enumerable;
						if (__ConfigWindow.<>f__am$cache3F == null)
						{
							__ConfigWindow.<>f__am$cache3F = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 3);
						}
						this.DrawWeaponSlot4(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable26, __ConfigWindow.<>f__am$cache3F)));
						break;
					}
					case 8:
					{
						this.laserSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_laser");
						this.plasmaSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_plasma");
						this.ionSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_ion");
						IEnumerable<SlotItem> enumerable27 = enumerable;
						if (__ConfigWindow.<>f__am$cache40 == null)
						{
							__ConfigWindow.<>f__am$cache40 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 0);
						}
						this.DrawWeaponSlot1(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable27, __ConfigWindow.<>f__am$cache40)));
						IEnumerable<SlotItem> enumerable28 = enumerable;
						if (__ConfigWindow.<>f__am$cache41 == null)
						{
							__ConfigWindow.<>f__am$cache41 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 1);
						}
						this.DrawWeaponSlot2(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable28, __ConfigWindow.<>f__am$cache41)));
						IEnumerable<SlotItem> enumerable29 = enumerable;
						if (__ConfigWindow.<>f__am$cache42 == null)
						{
							__ConfigWindow.<>f__am$cache42 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 3);
						}
						this.DrawWeaponSlot4(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable29, __ConfigWindow.<>f__am$cache42)));
						IEnumerable<SlotItem> enumerable30 = enumerable;
						if (__ConfigWindow.<>f__am$cache43 == null)
						{
							__ConfigWindow.<>f__am$cache43 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 4);
						}
						this.DrawWeaponSlot5(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable30, __ConfigWindow.<>f__am$cache43)));
						break;
					}
					case 9:
					{
						this.laserSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_laser");
						this.plasmaSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_plasma");
						this.ionSlotsLabel.text = string.Empty;
						IEnumerable<SlotItem> enumerable31 = enumerable;
						if (__ConfigWindow.<>f__am$cache44 == null)
						{
							__ConfigWindow.<>f__am$cache44 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 0);
						}
						this.DrawWeaponSlot1(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable31, __ConfigWindow.<>f__am$cache44)));
						IEnumerable<SlotItem> enumerable32 = enumerable;
						if (__ConfigWindow.<>f__am$cache45 == null)
						{
							__ConfigWindow.<>f__am$cache45 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 1);
						}
						this.DrawWeaponSlot2(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable32, __ConfigWindow.<>f__am$cache45)));
						IEnumerable<SlotItem> enumerable33 = enumerable;
						if (__ConfigWindow.<>f__am$cache46 == null)
						{
							__ConfigWindow.<>f__am$cache46 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 4);
						}
						this.DrawWeaponSlot5(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable33, __ConfigWindow.<>f__am$cache46)));
						break;
					}
					case 10:
					{
						this.laserSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_laser");
						this.plasmaSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_plasma");
						this.ionSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_ion");
						IEnumerable<SlotItem> enumerable34 = enumerable;
						if (__ConfigWindow.<>f__am$cache47 == null)
						{
							__ConfigWindow.<>f__am$cache47 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 0);
						}
						this.DrawWeaponSlot1(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable34, __ConfigWindow.<>f__am$cache47)));
						IEnumerable<SlotItem> enumerable35 = enumerable;
						if (__ConfigWindow.<>f__am$cache48 == null)
						{
							__ConfigWindow.<>f__am$cache48 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 1);
						}
						this.DrawWeaponSlot2(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable35, __ConfigWindow.<>f__am$cache48)));
						IEnumerable<SlotItem> enumerable36 = enumerable;
						if (__ConfigWindow.<>f__am$cache49 == null)
						{
							__ConfigWindow.<>f__am$cache49 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 2);
						}
						this.DrawWeaponSlot3(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable36, __ConfigWindow.<>f__am$cache49)));
						IEnumerable<SlotItem> enumerable37 = enumerable;
						if (__ConfigWindow.<>f__am$cache4A == null)
						{
							__ConfigWindow.<>f__am$cache4A = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 3);
						}
						this.DrawWeaponSlot4(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable37, __ConfigWindow.<>f__am$cache4A)));
						IEnumerable<SlotItem> enumerable38 = enumerable;
						if (__ConfigWindow.<>f__am$cache4B == null)
						{
							__ConfigWindow.<>f__am$cache4B = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 5);
						}
						this.DrawWeaponSlot6(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable38, __ConfigWindow.<>f__am$cache4B)));
						break;
					}
					case 11:
					{
						this.laserSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_laser");
						this.plasmaSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_plasma");
						this.ionSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_ion");
						IEnumerable<SlotItem> enumerable39 = enumerable;
						if (__ConfigWindow.<>f__am$cache4C == null)
						{
							__ConfigWindow.<>f__am$cache4C = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 0);
						}
						this.DrawWeaponSlot1(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable39, __ConfigWindow.<>f__am$cache4C)));
						IEnumerable<SlotItem> enumerable40 = enumerable;
						if (__ConfigWindow.<>f__am$cache4D == null)
						{
							__ConfigWindow.<>f__am$cache4D = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 1);
						}
						this.DrawWeaponSlot2(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable40, __ConfigWindow.<>f__am$cache4D)));
						IEnumerable<SlotItem> enumerable41 = enumerable;
						if (__ConfigWindow.<>f__am$cache4E == null)
						{
							__ConfigWindow.<>f__am$cache4E = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 2);
						}
						this.DrawWeaponSlot3(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable41, __ConfigWindow.<>f__am$cache4E)));
						IEnumerable<SlotItem> enumerable42 = enumerable;
						if (__ConfigWindow.<>f__am$cache4F == null)
						{
							__ConfigWindow.<>f__am$cache4F = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 3);
						}
						this.DrawWeaponSlot4(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable42, __ConfigWindow.<>f__am$cache4F)));
						IEnumerable<SlotItem> enumerable43 = enumerable;
						if (__ConfigWindow.<>f__am$cache50 == null)
						{
							__ConfigWindow.<>f__am$cache50 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 4);
						}
						this.DrawWeaponSlot5(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable43, __ConfigWindow.<>f__am$cache50)));
						IEnumerable<SlotItem> enumerable44 = enumerable;
						if (__ConfigWindow.<>f__am$cache51 == null)
						{
							__ConfigWindow.<>f__am$cache51 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 5);
						}
						this.DrawWeaponSlot6(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable44, __ConfigWindow.<>f__am$cache51)));
						break;
					}
					case 12:
					{
						this.laserSlotsLabel.text = string.Empty;
						this.plasmaSlotsLabel.text = StaticData.Translate("key_ship_cfg_slots_plasma");
						this.ionSlotsLabel.text = string.Empty;
						IEnumerable<SlotItem> enumerable45 = enumerable;
						if (__ConfigWindow.<>f__am$cache52 == null)
						{
							__ConfigWindow.<>f__am$cache52 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 1);
						}
						this.DrawWeaponSlot2(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable45, __ConfigWindow.<>f__am$cache52)));
						IEnumerable<SlotItem> enumerable46 = enumerable;
						if (__ConfigWindow.<>f__am$cache53 == null)
						{
							__ConfigWindow.<>f__am$cache53 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 4);
						}
						this.DrawWeaponSlot5(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable46, __ConfigWindow.<>f__am$cache53)));
						break;
					}
				}
			}
		}
	}

	private void DrawAmmoSlot(AndromedaGuiDragDropPlace place)
	{
		SlotItem slotItem = place.item;
		GuiTexture guiTexture = new GuiTexture();
		string str = "InventoryAmmoSlotActive";
		EWeaponStatus eWeaponStatu = this.CalculateWeaponStatus(slotItem);
		bool flag = false;
		SlotItemWeapon slotItemWeapon = null;
		switch (eWeaponStatu)
		{
			case 0:
			case 1:
			case 2:
			{
				str = "InventoryAmmoSlotActive";
				goto case 4;
			}
			case 3:
			{
				str = "InventoryAmmoSlotActive";
				flag = true;
				slotItemWeapon = (SlotItemWeapon)slotItem;
				goto case 4;
			}
			case 4:
			{
				guiTexture.SetTexture("ConfigWindow", str);
				guiTexture.X = place.position.x + 50f;
				guiTexture.Y = place.position.y + 19f;
				base.AddGuiElement(guiTexture);
				this.ammoSlots.Add((int)place.slot, guiTexture);
				if (flag)
				{
					GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
					string item = StaticData.allTypes.get_Item(slotItemWeapon.get_AmmoType()).assetName;
					guiButtonFixed.SetTexture("AmmosAvatars", item);
					guiButtonFixed.X = guiTexture.X;
					guiButtonFixed.Y = guiTexture.Y + 4f;
					guiButtonFixed.boundries.set_width(32f);
					guiButtonFixed.boundries.set_height(22f);
					guiButtonFixed.Caption = string.Empty;
					AmmoSwichBtnInfo ammoSwichBtnInfo = new AmmoSwichBtnInfo()
					{
						possitionX = guiButtonFixed.X,
						possitionY = guiButtonFixed.Y,
						itemTypeId = slotItemWeapon.get_AmmoType()
					};
					guiButtonFixed.tooltipWindowParam = ammoSwichBtnInfo;
					guiButtonFixed.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(Inventory.DrawAmmoSwitchBtnToolTip);
					EventHandlerParam eventHandlerParam = new EventHandlerParam()
					{
						customData = slotItemWeapon.get_Slot()
					};
					guiButtonFixed.eventHandlerParam = eventHandlerParam;
					guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, __ConfigWindow.OnToggleAmmo);
					this.ammoButtons.Add((int)place.slot, guiButtonFixed);
					base.AddGuiElement(guiButtonFixed);
				}
				return;
			}
			case 5:
			{
				str = "InventoryAmmoSlotOutOfAmmo";
				flag = true;
				slotItemWeapon = (SlotItemWeapon)slotItem;
				goto case 4;
			}
			default:
			{
				goto case 4;
			}
		}
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
				guiTexture.boundries = new Rect(x + (float)(i % 4 * 58), y + (float)(i / 4 * 58), 55f, 55f);
				base.AddGuiElement(guiTexture);
				if (location != 2)
				{
					this.lockedInventorySlots.Add(guiTexture);
				}
				else
				{
					this.lockedVaultSlots.Add(guiTexture);
				}
			}
			else
			{
				guiButtonFixed.SetTexture("ConfigWindow", "inventory_slot_plus");
				guiButtonFixed.boundries = new Rect(x + (float)(i % 4 * 58), y + (float)(i / 4 * 58), 55f, 55f);
				guiButtonFixed.Caption = string.Empty;
				base.AddGuiElement(guiButtonFixed);
				guiButtonFixed.hoverParam = location;
				guiButtonFixed.Hovered = new Action<object, bool>(this, __ConfigWindow.OnExpandBtnHover);
				guiButtonFixed.eventHandlerParam.customData = location;
				guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, __ConfigWindow.OnExpandBtnClicked);
				guiButtonFixed.tooltipWindowParam = new Rect(x + (float)(i % 4 * 58), y + (float)(i / 4 * 58), 150f, 70f);
				if (location != 2)
				{
					this.lockedInventorySlots.Add(guiButtonFixed);
					guiButtonFixed.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.DrawExpandInventorySlotsTooltip);
				}
				else
				{
					this.lockedVaultSlots.Add(guiButtonFixed);
					guiButtonFixed.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.DrawExpandVaultSlotsTooltip);
				}
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

	private void DrawExpandShipSlots()
	{
		SelectedCurrency selectedCurrency = new SelectedCurrency();
		int num = 0;
		EventHandlerParam eventHandlerParam;
		foreach (GuiElement forExpandedShipSlot in this.forExpandedShipSlots)
		{
			base.RemoveGuiElement(forExpandedShipSlot);
		}
		this.forExpandedShipSlots.Clear();
		bool flag = false;
		int nextExtendedSlotLevelRequirement = NetworkScript.player.playerBelongings.GetNextExtendedSlotLevelRequirement();
		NetworkScript.player.playerBelongings.GetNextExtendedSlotPrice(ref selectedCurrency, ref num);
		string empty = string.Empty;
		empty = (NetworkScript.player.playerBelongings.playerLevel >= nextExtendedSlotLevelRequirement ? this.AssignTooltip(out flag) : string.Format(StaticData.Translate("key_extra_slots_level"), nextExtendedSlotLevelRequirement));
		if (!NetworkScript.player.playerBelongings.haveExtendetCorpusOne)
		{
			GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
			guiButtonFixed.SetTexture("ConfigWindow", "inventory_slot_plus");
			guiButtonFixed.X = 818f;
			guiButtonFixed.Y = 216f;
			guiButtonFixed.Caption = string.Empty;
			guiButtonFixed.isEnabled = flag;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = empty,
				customData2 = guiButtonFixed
			};
			guiButtonFixed.tooltipWindowParam = eventHandlerParam;
			guiButtonFixed.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			eventHandlerParam = new EventHandlerParam()
			{
				customData = (ExtendedSlot)1
			};
			guiButtonFixed.eventHandlerParam = eventHandlerParam;
			guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, __ConfigWindow.OnBuyExpandSlotClick);
			if (this.IsMobile && !flag)
			{
				guiButtonFixed.tooltipTapDownTime = 0f;
			}
			base.AddGuiElement(guiButtonFixed);
			this.forExpandedShipSlots.Add(guiButtonFixed);
		}
		if (!NetworkScript.player.playerBelongings.haveExtendetShielOne)
		{
			GuiButtonFixed drawTooltipWindow = new GuiButtonFixed();
			drawTooltipWindow.SetTexture("ConfigWindow", "inventory_slot_plus");
			drawTooltipWindow.X = 420f;
			drawTooltipWindow.Y = 398f;
			drawTooltipWindow.Caption = string.Empty;
			drawTooltipWindow.isEnabled = flag;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = empty,
				customData2 = drawTooltipWindow
			};
			drawTooltipWindow.tooltipWindowParam = eventHandlerParam;
			drawTooltipWindow.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			eventHandlerParam = new EventHandlerParam()
			{
				customData = (ExtendedSlot)2
			};
			drawTooltipWindow.eventHandlerParam = eventHandlerParam;
			drawTooltipWindow.Clicked = new Action<EventHandlerParam>(this, __ConfigWindow.OnBuyExpandSlotClick);
			if (this.IsMobile && !flag)
			{
				drawTooltipWindow.tooltipTapDownTime = 0f;
			}
			base.AddGuiElement(drawTooltipWindow);
			this.forExpandedShipSlots.Add(drawTooltipWindow);
		}
		if (!NetworkScript.player.playerBelongings.haveExtendetAnyOne)
		{
			GuiButtonFixed action = new GuiButtonFixed();
			action.SetTexture("ConfigWindow", "inventory_slot_plus");
			action.X = 420f;
			action.Y = 308f;
			action.Caption = string.Empty;
			action.isEnabled = flag;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = empty,
				customData2 = action
			};
			action.tooltipWindowParam = eventHandlerParam;
			action.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			eventHandlerParam = new EventHandlerParam()
			{
				customData = (ExtendedSlot)4
			};
			action.eventHandlerParam = eventHandlerParam;
			action.Clicked = new Action<EventHandlerParam>(this, __ConfigWindow.OnBuyExpandSlotClick);
			if (this.IsMobile && !flag)
			{
				action.tooltipTapDownTime = 0f;
			}
			base.AddGuiElement(action);
			this.forExpandedShipSlots.Add(action);
		}
		if (!NetworkScript.player.playerBelongings.haveExtendetEngineOne)
		{
			GuiButtonFixed guiButtonFixed1 = new GuiButtonFixed();
			guiButtonFixed1.SetTexture("ConfigWindow", "inventory_slot_plus");
			guiButtonFixed1.X = 818f;
			guiButtonFixed1.Y = 81f;
			guiButtonFixed1.Caption = string.Empty;
			guiButtonFixed1.isEnabled = flag;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = empty,
				customData2 = guiButtonFixed1
			};
			guiButtonFixed1.tooltipWindowParam = eventHandlerParam;
			guiButtonFixed1.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			eventHandlerParam = new EventHandlerParam()
			{
				customData = (ExtendedSlot)3
			};
			guiButtonFixed1.eventHandlerParam = eventHandlerParam;
			guiButtonFixed1.Clicked = new Action<EventHandlerParam>(this, __ConfigWindow.OnBuyExpandSlotClick);
			if (this.IsMobile && !flag)
			{
				guiButtonFixed1.tooltipTapDownTime = 0f;
			}
			base.AddGuiElement(guiButtonFixed1);
			this.forExpandedShipSlots.Add(guiButtonFixed1);
		}
		if (!NetworkScript.player.playerBelongings.haveExtendetExtraOne)
		{
			GuiButtonFixed empty1 = new GuiButtonFixed();
			empty1.SetTexture("ConfigWindow", "inventory_slot_plus");
			empty1.X = 818f;
			empty1.Y = 398f;
			empty1.Caption = string.Empty;
			empty1.isEnabled = flag;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = empty,
				customData2 = empty1
			};
			empty1.tooltipWindowParam = eventHandlerParam;
			empty1.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			eventHandlerParam = new EventHandlerParam()
			{
				customData = (ExtendedSlot)5
			};
			empty1.eventHandlerParam = eventHandlerParam;
			empty1.Clicked = new Action<EventHandlerParam>(this, __ConfigWindow.OnBuyExpandSlotClick);
			if (this.IsMobile && !flag)
			{
				empty1.tooltipTapDownTime = 0f;
			}
			base.AddGuiElement(empty1);
			this.forExpandedShipSlots.Add(empty1);
		}
		if (!NetworkScript.player.playerBelongings.haveExtendetExtraOne)
		{
			empty = StaticData.Translate("key_extra_slots_extra");
		}
		if (!NetworkScript.player.playerBelongings.haveExtendetExtraTwo)
		{
			GuiButtonFixed drawTooltipWindow1 = new GuiButtonFixed();
			drawTooltipWindow1.SetTexture("ConfigWindow", "inventory_slot_locked");
			if (NetworkScript.player.playerBelongings.haveExtendetExtraOne)
			{
				drawTooltipWindow1.SetTexture("ConfigWindow", "inventory_slot_plus");
			}
			drawTooltipWindow1.X = 818f;
			drawTooltipWindow1.Y = 454f;
			drawTooltipWindow1.Caption = string.Empty;
			drawTooltipWindow1.isEnabled = (!flag ? false : NetworkScript.player.playerBelongings.haveExtendetExtraOne);
			eventHandlerParam = new EventHandlerParam()
			{
				customData = empty,
				customData2 = drawTooltipWindow1
			};
			drawTooltipWindow1.tooltipWindowParam = eventHandlerParam;
			drawTooltipWindow1.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			eventHandlerParam = new EventHandlerParam()
			{
				customData = (ExtendedSlot)6
			};
			drawTooltipWindow1.eventHandlerParam = eventHandlerParam;
			drawTooltipWindow1.Clicked = new Action<EventHandlerParam>(this, __ConfigWindow.OnBuyExpandSlotClick);
			if (this.IsMobile && !flag && !NetworkScript.player.playerBelongings.haveExtendetExtraOne)
			{
				drawTooltipWindow1.tooltipTapDownTime = 0f;
			}
			base.AddGuiElement(drawTooltipWindow1);
			this.forExpandedShipSlots.Add(drawTooltipWindow1);
		}
	}

	private GuiWindow DrawExpandVaultSlotsTooltip(object prm)
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
			text = StaticData.Translate("key_ship_cfg_expand_vault_lbl").ToUpper(),
			boundries = new Rect(10f, 4f, 130f, 20f),
			FontSize = 10,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 4
		};
		guiWindow.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			text = string.Format(StaticData.Translate("key_ship_cfg_expand_vault_details"), Enumerable.First<SlotPriceInfo>(Enumerable.Where<SlotPriceInfo>(StaticData.slotPriceInformation, new Func<SlotPriceInfo, bool>(this, (SlotPriceInfo s) => (s.slotId != this.vaultSlotCnt + 4 ? false : s.slotType == "Vault")))).priceNova),
			boundries = new Rect(10f, 24f, 130f, 45f),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		guiWindow.AddGuiElement(guiLabel1);
		return guiWindow;
	}

	private void DrawFactionWarbonus(string bonustText, int index)
	{
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("ConfigWindow", "shipConfigBonusFrame");
		guiTexture.X = 486f;
		guiTexture.Y = (float)(52 + 30 * index);
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = bonustText,
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(495f, guiTexture.Y + 5f, 190f, 20f),
			FontSize = 12
		};
		guiLabel.TextColor = GuiNewStyleBar.aquamarineColor;
		guiLabel.Alignment = 3;
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
	}

	private GuiWindow DrawShipStatsTooltiop(object prm)
	{
		__ConfigWindow.ShipStatsHoverParam shipStatsHoverParam = (__ConfigWindow.ShipStatsHoverParam)prm;
		bool flag = shipStatsHoverParam.lable == "shield_power";
		GuiWindow guiWindow = new GuiWindow()
		{
			zOrder = 230,
			ignoreClickEvents = true
		};
		guiWindow.SetBackgroundTexture("ConfigWindow", "shipStatsTooltip");
		guiWindow.isHidden = false;
		guiWindow.boundries.set_x(40f);
		guiWindow.boundries.set_y(197f);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = shipStatsHoverParam.baseStat.ToString("##,##0"),
			boundries = new Rect(10f, 15f, 100f, 12f),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 5
		};
		guiWindow.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			text = (!flag ? string.Format(StaticData.Translate("key_ship_cfg_stats_base"), shipStatsHoverParam.lable) : StaticData.Translate("key_ship_cfg_shield_regen")),
			boundries = new Rect(10f, 3f, 100f, 12f),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 5
		};
		guiWindow.AddGuiElement(guiLabel1);
		if (!flag)
		{
			GuiLabel guiLabel2 = new GuiLabel()
			{
				text = "+",
				FontSize = 14,
				boundries = new Rect(110f, 14f, 17f, 14f),
				Alignment = 4
			};
			guiWindow.AddGuiElement(guiLabel2);
		}
		GuiLabel guiLabel3 = new GuiLabel()
		{
			text = (!flag ? shipStatsHoverParam.bonusStat.ToString("##,##0") : string.Format("{0:##,##0}%", shipStatsHoverParam.bonusStat)),
			boundries = new Rect(130f, 15f, 105f, 12f),
			FontSize = 12,
			TextColor = GuiNewStyleBar.purpleColor,
			Alignment = 3
		};
		guiWindow.AddGuiElement(guiLabel3);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			text = (!flag ? string.Format(StaticData.Translate("key_ship_cfg_stats_bonus"), shipStatsHoverParam.lable) : StaticData.Translate("key_ship_cfg_damage_reduction")),
			boundries = new Rect(130f, 3f, 105f, 12f),
			FontSize = 12,
			TextColor = GuiNewStyleBar.purpleColor,
			Alignment = 3
		};
		guiWindow.AddGuiElement(guiLabel4);
		return guiWindow;
	}

	private void DrawWeaponItemOnShip(AndromedaGuiDragDropPlace place, SlotItem item)
	{
		place.dropZonePosition = place.position;
		place.txFrameIdle = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotIdle");
		place.txFrameDragSource = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotHvr");
		place.txFrameDropTarget = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotActive");
		place.txFrameDropTargetHover = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotHvr");
		place.item = item;
		place.isEmpty = item == null;
		place.shipId = NetworkScript.player.playerBelongings.selectedShipId;
		place.idleItemTextureSize = new Vector2(51f, 35f);
		Inventory.places.Add(place);
		this.DrawAmmoSlot(place);
	}

	private AndromedaGuiDragDropPlace DrawWeaponSlot1(SlotItem item)
	{
		AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = new AndromedaGuiDragDropPlace()
		{
			position = new Vector2(302f, 85f),
			location = 6,
			slot = 0
		};
		this.DrawWeaponItemOnShip(andromedaGuiDragDropPlace, item);
		return andromedaGuiDragDropPlace;
	}

	private AndromedaGuiDragDropPlace DrawWeaponSlot2(SlotItem item)
	{
		AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = new AndromedaGuiDragDropPlace()
		{
			position = new Vector2(302f, 155f),
			location = 7,
			slot = 1
		};
		this.DrawWeaponItemOnShip(andromedaGuiDragDropPlace, item);
		return andromedaGuiDragDropPlace;
	}

	private AndromedaGuiDragDropPlace DrawWeaponSlot3(SlotItem item)
	{
		AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = new AndromedaGuiDragDropPlace()
		{
			position = new Vector2(402f, 85f),
			location = 6,
			slot = 2
		};
		this.DrawWeaponItemOnShip(andromedaGuiDragDropPlace, item);
		return andromedaGuiDragDropPlace;
	}

	private AndromedaGuiDragDropPlace DrawWeaponSlot4(SlotItem item)
	{
		AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = new AndromedaGuiDragDropPlace()
		{
			position = new Vector2(302f, 225f),
			location = 8,
			slot = 3
		};
		this.DrawWeaponItemOnShip(andromedaGuiDragDropPlace, item);
		return andromedaGuiDragDropPlace;
	}

	private AndromedaGuiDragDropPlace DrawWeaponSlot5(SlotItem item)
	{
		AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = new AndromedaGuiDragDropPlace()
		{
			position = new Vector2(402f, 155f),
			location = 7,
			slot = 4
		};
		this.DrawWeaponItemOnShip(andromedaGuiDragDropPlace, item);
		return andromedaGuiDragDropPlace;
	}

	private AndromedaGuiDragDropPlace DrawWeaponSlot6(SlotItem item)
	{
		AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = new AndromedaGuiDragDropPlace()
		{
			position = new Vector2(402f, 225f),
			location = 8,
			slot = 5
		};
		this.DrawWeaponItemOnShip(andromedaGuiDragDropPlace, item);
		return andromedaGuiDragDropPlace;
	}

	public void DropHandler(AndromedaGuiDragDropPlace src, AndromedaGuiDragDropPlace dst)
	{
		this.Clear();
		this.Create();
	}

	private AndromedaGuiDragDropPlace GetEquippedWeaponDDPlace(ushort slotNr)
	{
		__ConfigWindow.<GetEquippedWeaponDDPlace>c__AnonStorey81 variable = null;
		return Enumerable.First<AndromedaGuiDragDropPlace>(Enumerable.Where<AndromedaGuiDragDropPlace>(Inventory.places, new Func<AndromedaGuiDragDropPlace, bool>(variable, (AndromedaGuiDragDropPlace p) => (p.item == null || p.location != 8 && p.location != 6 && p.location != 7 ? false : this.slotNr == p.slot))));
	}

	public void Hide(GuiFramework gui)
	{
	}

	private bool IsStructureSlot(byte slotType)
	{
		switch (slotType)
		{
			case 9:
			{
				return true;
			}
			case 10:
			{
				return true;
			}
			case 11:
			{
				return true;
			}
			case 12:
			{
				return true;
			}
		}
		return false;
	}

	private void OnBuyExpandSlotClick(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void __ConfigWindow::OnBuyExpandSlotClick(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnBuyExpandSlotClick(EventHandlerParam)
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

	private void OnCloseBtnClick(object prm)
	{
		AndromedaGui.mainWnd.CloseActiveWindow();
	}

	private void OnExpandBtnClicked(EventHandlerParam prm)
	{
		this.buyShipSlotData = new UniversalTransportContainer();
		ItemLocation itemLocation = (int)prm.customData;
		this.expandSlotWindow = new ExpandSlotDialog()
		{
			ConfirmExpand = new Action<EventHandlerParam>(this, __ConfigWindow.ConfirmExpand)
		};
		int num = 0;
		int num1 = 0;
		num = (this.inventorySlotCnt <= 36 ? Enumerable.First<SlotPriceInfo>(Enumerable.Where<SlotPriceInfo>(StaticData.slotPriceInformation, new Func<SlotPriceInfo, bool>(this, (SlotPriceInfo s) => (s.slotId != this.inventorySlotCnt + 4 ? false : s.slotType == "Inventory")))).priceNova : 999999999);
		num1 = (this.vaultSlotCnt <= 36 ? Enumerable.First<SlotPriceInfo>(Enumerable.Where<SlotPriceInfo>(StaticData.slotPriceInformation, new Func<SlotPriceInfo, bool>(this, (SlotPriceInfo s) => (s.slotId != this.vaultSlotCnt + 4 ? false : s.slotType == "Vault")))).priceNova : 999999999);
		if (itemLocation != 2)
		{
			this.buyShipSlotData.wantedSlot = 7;
			this.expandSlotWindow.CreateExpandDialog(num, 1, StaticData.Translate("key_ship_cfg_expand_modal"), AndromedaGui.gui);
		}
		else
		{
			this.buyShipSlotData.wantedSlot = 8;
			this.expandSlotWindow.CreateExpandDialog(num1, 1, StaticData.Translate("key_ship_cfg_expand_modal"), AndromedaGui.gui);
		}
	}

	private void OnExpandBtnHover(object prm, bool state)
	{
		ItemLocation itemLocation = (int)prm;
		if (state)
		{
			if (itemLocation != 2)
			{
				foreach (GuiElement lockedInventorySlot in this.lockedInventorySlots)
				{
					if (!(lockedInventorySlot is GuiTexture))
					{
						continue;
					}
					((GuiTexture)lockedInventorySlot).SetTextureKeepSize("ConfigWindow", "inventory_slot_lockedHvr");
				}
			}
			else
			{
				foreach (GuiElement lockedVaultSlot in this.lockedVaultSlots)
				{
					if (!(lockedVaultSlot is GuiTexture))
					{
						continue;
					}
					((GuiTexture)lockedVaultSlot).SetTextureKeepSize("ConfigWindow", "inventory_slot_lockedHvr");
				}
			}
		}
		else if (itemLocation != 2)
		{
			foreach (GuiElement guiElement in this.lockedInventorySlots)
			{
				if (!(guiElement is GuiTexture))
				{
					continue;
				}
				((GuiTexture)guiElement).SetTextureKeepSize("ConfigWindow", "inventory_slot_locked");
			}
		}
		else
		{
			foreach (GuiElement lockedVaultSlot1 in this.lockedVaultSlots)
			{
				if (!(lockedVaultSlot1 is GuiTexture))
				{
					continue;
				}
				((GuiTexture)lockedVaultSlot1).SetTextureKeepSize("ConfigWindow", "inventory_slot_locked");
			}
		}
	}

	private void OnFactionWarBtnClicked(object prm)
	{
		MainScreenWindow mainScreenWindow = AndromedaGui.mainWnd;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = (byte)34
		};
		mainScreenWindow.OnWindowBtnClicked(eventHandlerParam);
	}

	public void OnScrollDown(EventHandlerParam parm)
	{
		if (this.selectedTab == __ConfigWindow.ConfigWindowTab.Inventory)
		{
			__ConfigWindow _ConfigWindow = this;
			_ConfigWindow.inventoryScrollIndex = _ConfigWindow.inventoryScrollIndex + 4;
		}
		if (this.selectedTab == __ConfigWindow.ConfigWindowTab.Vault)
		{
			__ConfigWindow _ConfigWindow1 = this;
			_ConfigWindow1.vaultScrollIndex = _ConfigWindow1.vaultScrollIndex + 4;
		}
		this.RefreshScrollButtons();
		this.ClearAmmoSlots();
		Inventory.ClearSlots(this);
		this.CreateInventorySlots();
		this.CreateExtrasTab();
		this.CreateWeaponsTab();
		this.CreateStructureTab();
		this.CreateTrash();
		Inventory.DrawPlaces(this);
	}

	public void OnScrollUp(EventHandlerParam parm)
	{
		if (this.selectedTab == __ConfigWindow.ConfigWindowTab.Inventory)
		{
			__ConfigWindow _ConfigWindow = this;
			_ConfigWindow.inventoryScrollIndex = _ConfigWindow.inventoryScrollIndex - 4;
		}
		if (this.selectedTab == __ConfigWindow.ConfigWindowTab.Vault)
		{
			__ConfigWindow _ConfigWindow1 = this;
			_ConfigWindow1.vaultScrollIndex = _ConfigWindow1.vaultScrollIndex - 4;
		}
		this.RefreshScrollButtons();
		this.ClearAmmoSlots();
		Inventory.ClearSlots(this);
		this.CreateInventorySlots();
		this.CreateExtrasTab();
		this.CreateWeaponsTab();
		this.CreateStructureTab();
		this.CreateTrash();
		Inventory.DrawPlaces(this);
	}

	private void OnToggleAmmo(EventHandlerParam prm)
	{
		List<ushort> list = new List<ushort>();
		ushort[] numArray = PlayerItems.ammoTypes;
		for (int i = 0; i < (int)numArray.Length; i++)
		{
			ushort num = numArray[i];
			if (NetworkScript.player.playerBelongings.playerItems.GetAmmoQty(num) > (long)0)
			{
				list.Add(num);
			}
		}
		if (list.get_Count() == 0)
		{
			this.ShowNoAmmoWarning();
			return;
		}
		SlotItemWeapon equippedWeaponDDPlace = (SlotItemWeapon)this.GetEquippedWeaponDDPlace((ushort)prm.customData).item;
		int num1 = 0;
		if (!list.Contains(equippedWeaponDDPlace.get_AmmoType()))
		{
			num1 = 0;
		}
		else
		{
			if (list.get_Count() == 1)
			{
				return;
			}
			int num2 = list.IndexOf(equippedWeaponDDPlace.get_AmmoType());
			num1 = (list.get_Count() - 1 != num2 ? num2 + 1 : 0);
		}
		equippedWeaponDDPlace.set_AmmoType(list.get_Item(num1));
		__ConfigWindow.SetAmmoTypeOnEquippedWeapon(equippedWeaponDDPlace.get_AmmoType(), equippedWeaponDDPlace.get_Slot(), equippedWeaponDDPlace.get_SlotType());
		this.Clear();
		this.Create();
		AndromedaGui.mainWnd.RefreshInformationIcons();
	}

	public void Refresh()
	{
		this.Clear();
		this.Create();
	}

	private void RefreshExpandBtnState()
	{
		int num = 0;
		int num1 = 0;
		num = (this.inventorySlotCnt <= 36 ? Enumerable.First<SlotPriceInfo>(Enumerable.Where<SlotPriceInfo>(StaticData.slotPriceInformation, new Func<SlotPriceInfo, bool>(this, (SlotPriceInfo s) => (s.slotId != this.inventorySlotCnt + 4 ? false : s.slotType == "Inventory")))).priceNova : 999999999);
		num1 = (this.vaultSlotCnt <= 36 ? Enumerable.First<SlotPriceInfo>(Enumerable.Where<SlotPriceInfo>(StaticData.slotPriceInformation, new Func<SlotPriceInfo, bool>(this, (SlotPriceInfo s) => (s.slotId != this.vaultSlotCnt + 4 ? false : s.slotType == "Vault")))).priceNova : 999999999);
		foreach (GuiElement lockedInventorySlot in this.lockedInventorySlots)
		{
			if (!(lockedInventorySlot is GuiButtonFixed))
			{
				continue;
			}
			((GuiButtonFixed)lockedInventorySlot).isEnabled = NetworkScript.player.playerBelongings.playerItems.get_Nova() >= (long)num;
		}
		foreach (GuiElement lockedVaultSlot in this.lockedVaultSlots)
		{
			if (!(lockedVaultSlot is GuiButtonFixed))
			{
				continue;
			}
			((GuiButtonFixed)lockedVaultSlot).isEnabled = NetworkScript.player.playerBelongings.playerItems.get_Nova() >= (long)num1;
		}
	}

	private void RefreshScrollButtons()
	{
		if (this.selectedTab == __ConfigWindow.ConfigWindowTab.Inventory)
		{
			GuiLabel guiLabel = this.scrollLabel;
			string[] str = new string[] { this.inventoryScrollIndex.ToString(), "-", default(string), default(string), default(string) };
			int num = Math.Min(this.inventorySlotCnt, this.inventoryScrollIndex + 16);
			str[2] = num.ToString();
			str[3] = StaticData.Translate("key_ship_cfg_tab_separator");
			str[4] = this.inventorySlotCnt.ToString();
			guiLabel.text = string.Concat(str);
			this.scrollUpButton.isEnabled = this.inventoryScrollIndex != 0;
			this.scrollDownButton.isEnabled = (this.inventorySlotCnt != 40 ? this.inventorySlotCnt - this.inventoryScrollIndex > 14 : this.inventorySlotCnt - this.inventoryScrollIndex > 16);
		}
		if (this.selectedTab == __ConfigWindow.ConfigWindowTab.Vault)
		{
			GuiLabel guiLabel1 = this.scrollLabel;
			string[] strArray = new string[] { this.vaultScrollIndex.ToString(), "-", default(string), default(string), default(string) };
			int num1 = Math.Min(this.vaultSlotCnt, this.vaultScrollIndex + 16);
			strArray[2] = num1.ToString();
			strArray[3] = StaticData.Translate("key_ship_cfg_tab_separator");
			strArray[4] = this.vaultSlotCnt.ToString();
			guiLabel1.text = string.Concat(strArray);
			this.scrollUpButton.isEnabled = this.vaultScrollIndex != 0;
			this.scrollDownButton.isEnabled = (this.vaultSlotCnt != 40 ? this.vaultSlotCnt - this.vaultScrollIndex > 14 : this.vaultSlotCnt - this.vaultScrollIndex > 16);
		}
		if (this.selectedTab == __ConfigWindow.ConfigWindowTab.Cargo)
		{
			this.scrollLabel.text = string.Empty;
			this.scrollUpButton.isEnabled = false;
			this.scrollDownButton.isEnabled = false;
		}
	}

	public static void SetAmmoTypeOnEquippedWeapon(ushort ammoType, ushort slotNumber, byte slotType)
	{
		// 
		// Current member / type: System.Void __ConfigWindow::SetAmmoTypeOnEquippedWeapon(System.UInt16,System.UInt16,System.Byte)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void SetAmmoTypeOnEquippedWeapon(System.UInt16,System.UInt16,System.Byte)
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

	private void ShowNoAmmoWarning()
	{
		base.ShowWarninMessage(StaticData.Translate("key_ship_config_no_ammo_warning"), new Rect(455f, 35f, 245f, 40f), 24, 2);
	}

	public void ShowNoDestinationSlotFoundWarning()
	{
		base.ShowWarninMessage(StaticData.Translate("key_ship_config_no_dst_slot_found_warning"), new Rect(455f, 35f, 245f, 40f), 24, 2);
	}

	private void Tab1Clicked(EventHandlerParam parm)
	{
		this.Clear();
		this.selectedTab = __ConfigWindow.ConfigWindowTab.Inventory;
		this.inventoryScrollIndex = 0;
		this.Create();
	}

	private void Tab2Clicked(EventHandlerParam parm)
	{
		this.Clear();
		this.selectedTab = __ConfigWindow.ConfigWindowTab.Cargo;
		this.inventoryScrollIndex = 0;
		this.Create();
	}

	private void Tab3Clicked(EventHandlerParam parm)
	{
		this.Clear();
		this.selectedTab = __ConfigWindow.ConfigWindowTab.Vault;
		this.inventoryScrollIndex = 0;
		this.Create();
	}

	public enum ConfigWindowTab
	{
		Inventory,
		Cargo,
		Vault
	}

	public enum InventoryFilter
	{
		All,
		Weapons,
		Ammo,
		Structure,
		Extras
	}

	private class ShipStatsHoverParam
	{
		public float baseStat;

		public float bonusStat;

		public string lable;

		public ShipStatsHoverParam()
		{
		}
	}
}