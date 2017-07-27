using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TransferableObjects;
using UnityEngine;

public class __MerchantWindow : GuiWindow
{
	public __MerchantWindow.MerchantInventoryTab selectedInventoryTab;

	private bool IsMobile;

	public __MerchantWindow.MerchantTab selectedMerchantTab = __MerchantWindow.MerchantTab.Shields;

	private __MerchantWindow.MenuOption selectedOption;

	private int inventorySlotCnt;

	private GuiButtonFixed filterAllButton;

	private GuiButtonFixed filterWeaponsButton;

	private GuiButtonFixed filterAmmoButton;

	private GuiButtonFixed filterStructureButton;

	private GuiButtonFixed filterExtrasButton;

	public int InventoryScrollingDirection;

	public int MerchantScrollDirection;

	public float ScrollTimer;

	private GuiLabel laserSlotsLabel;

	private GuiLabel plasmaSlotsLabel;

	private GuiLabel ionSlotsLabel;

	private GuiLabel selectedSectionLbl;

	private GuiTexture shipTexture;

	private GuiLabel shipStatsLbl;

	private GuiTexture merchantTabTexture;

	private GuiTexture inventoryTabTexture;

	private GuiButtonFixed tab1Button;

	private GuiButtonFixed tab2Button;

	private GuiButtonFixed tab3Button;

	private GuiButtonFixed tab4Button;

	private GuiButtonFixed tab5Button;

	private GuiButtonFixed tab6Button;

	private GuiButton inventoryTab1Button;

	private GuiButton inventoryTab2Button;

	private GuiButtonFixed btnNextShip;

	private GuiButtonFixed btnPreviousShip;

	private GuiTexture txSelectedShip;

	private GuiButtonResizeable btnSelectShip;

	private int currentShipIndex;

	private string currentShipAssetName;

	private GuiLabel lblCurrentShipName;

	private PlayerShipNet currentShip;

	private GuiLabel lblDamageVal;

	private GuiLabel lblCorpusVal;

	private GuiLabel lblShieldVal;

	private GuiLabel lblShieldRegenVal;

	private GuiLabel lblAvoidanceVal;

	private GuiLabel lblTargetingVal;

	private GuiLabel lblSpeedVal;

	private GuiLabel lblCargoVal;

	private List<GuiElement> forDelete = new List<GuiElement>();

	private List<GuiElement> mainSlots = new List<GuiElement>();

	private List<GuiElement> forExpandedShipSlots = new List<GuiElement>();

	private float tabBtnOffsetY = 155f;

	private GuiButtonFixed btnMerchant;

	private GuiButtonFixed btnGambler;

	private GuiButtonFixed btnReroll;

	private GuiTexture rerollDropZone;

	private GuiLabel itemRerolEmptyBoxInfo;

	private bool isQualityGambleOn;

	private GuiWindow dialogWindow;

	private SortedList<int, GuiTexture> ammoSlots = new SortedList<int, GuiTexture>();

	private SortedList<int, GuiButton> ammoButtons = new SortedList<int, GuiButton>();

	private int onDisplayShipId;

	private UniversalTransportContainer buyShipSlotData;

	private ExpandSlotDialog expandShipSlotWindow;

	private List<GuiElement> lockedInventorySlots = new List<GuiElement>();

	private ExpandSlotDialog expandSlotWindow;

	private List<GuiElement> rerollSectionForDelete = new List<GuiElement>();

	private List<GuiElement> beforeSectionForDelete = new List<GuiElement>();

	private List<GuiElement> afterSectionForDelete = new List<GuiElement>();

	private int selectedSlotItem = -1;

	private SortedList<int, int> selectedBonusesInDropdowns = new SortedList<int, int>();

	private SortedList<int, bool> selectedMaximizeInCheckboxes = new SortedList<int, bool>();

	private SortedList<int, GuiDropdown> activeDropdowns = new SortedList<int, GuiDropdown>();

	private SortedList<int, GuiCheckbox> activeCheckboxes = new SortedList<int, GuiCheckbox>();

	private List<GuiElement> bonusValuesForDelete = new List<GuiElement>();

	private byte wantedBonusRandom;

	private byte wantedBonusOne;

	private byte wantedBonusTwo;

	private byte wantedBonusThree;

	private byte wantedBonusFour;

	private byte wantedBonusFive;

	private byte maximaizedBonusRandom;

	private byte maximaizedBonusOne;

	private byte maximaizedBonusTwo;

	private byte maximaizedBonusThree;

	private byte maximaizedBonusFour;

	private byte maximaizedBonusFive;

	private double rerollPrice;

	private double rerollPriceWithEQ;

	private bool SellAllAvailable
	{
		get
		{
			List<SlotItem> list = NetworkScript.player.playerBelongings.playerItems.slotItems;
			if (__MerchantWindow.<>f__am$cache56 == null)
			{
				__MerchantWindow.<>f__am$cache56 = new Func<SlotItem, bool>(null, (SlotItem si) => (si.get_SlotType() != 1 || PlayerItems.IsStackable(si.get_ItemType()) || PlayerItems.IsPortalPart(si.get_ItemType()) ? false : !si.isAccountBound));
			}
			return Enumerable.Count<SlotItem>(Enumerable.Where<SlotItem>(list, __MerchantWindow.<>f__am$cache56)) > 0;
		}
	}

	private int SellAllPrice
	{
		get
		{
			int num = 0;
			List<SlotItem> list = NetworkScript.player.playerBelongings.playerItems.slotItems;
			if (__MerchantWindow.<>f__am$cache57 == null)
			{
				__MerchantWindow.<>f__am$cache57 = new Func<SlotItem, bool>(null, (SlotItem si) => (si.get_SlotType() != 1 || PlayerItems.IsStackable(si.get_ItemType()) || PlayerItems.IsPortalPart(si.get_ItemType()) ? false : !si.isAccountBound));
			}
			foreach (SlotItem slotItem in Enumerable.ToList<SlotItem>(Enumerable.Where<SlotItem>(list, __MerchantWindow.<>f__am$cache57)))
			{
				num = num + PlayerItems.CalculateSlotItemSellPrice(slotItem, NetworkScript.player.vessel.cfg.sellBonus);
			}
			return num;
		}
	}

	public __MerchantWindow()
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

	private void BonusSelected(int i)
	{
		__MerchantWindow.<BonusSelected>c__AnonStorey9D variable = null;
		int num = this.selectedSlotItem % 1000;
		int num1 = this.selectedSlotItem / 1000;
		if (Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(NetworkScript.player.playerBelongings.playerItems.slotItems, new Func<SlotItem, bool>(variable, (SlotItem t) => (t.get_Slot() != this.itemSlot ? false : t.get_SlotType() == (byte)this.itemSlotType)))) == null)
		{
			return;
		}
		this.PopulateBonusValue();
	}

	private void CalculateDamage(ShipConfiguration cfg, out int dmgTotal)
	{
		dmgTotal = 0;
		float single = 0f;
		WeaponSlot[] weaponSlotArray = cfg.weaponSlots;
		if (__MerchantWindow.<>f__am$cache59 == null)
		{
			__MerchantWindow.<>f__am$cache59 = new Func<WeaponSlot, bool>(null, (WeaponSlot ws) => ws.isActive);
		}
		WeaponSlot[] array = Enumerable.ToArray<WeaponSlot>(Enumerable.Where<WeaponSlot>(weaponSlotArray, __MerchantWindow.<>f__am$cache59));
		for (int i = 0; i < (int)array.Length; i++)
		{
			WeaponSlot weaponSlot = array[i];
			single = (!cfg.damageBooster ? (float)weaponSlot.skillDamage * (1f + cfg.dmgPercentForAlien / 100f) * ((float)((AmmoNet)StaticData.allTypes.get_Item(weaponSlot.selectedAmmoItemType)).damage / 100f) : (float)weaponSlot.skillDamage * (1f + cfg.dmgPercentForAlien / 100f) * ((float)((AmmoNet)StaticData.allTypes.get_Item(weaponSlot.selectedAmmoItemType)).damage / 100f) * 1.3f);
			dmgTotal = dmgTotal + (int)single;
		}
	}

	private void CalculateShipDamage(PlayerShipNet pShip, out int dmg)
	{
		__MerchantWindow.<CalculateShipDamage>c__AnonStorey91 variable = null;
		dmg = 0;
		float single = 0f;
		IEnumerator<SlotItem> enumerator = Enumerable.Where<SlotItem>(NetworkScript.player.playerBelongings.playerItems.slotItems, new Func<SlotItem, bool>(variable, (SlotItem t) => (t.get_ShipId() != this.pShip.ShipID ? false : PlayerItems.IsWeapon(t.get_ItemType())))).GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				SlotItem current = enumerator.get_Current();
				int attachedWeaponDamage = pShip.GetAttachedWeaponDamage((SlotItemWeapon)current);
				single = (!NetworkScript.player.playerBelongings.get_HaveDamageBooster() ? (float)attachedWeaponDamage * (1f + pShip.dmgPercentAlien / 100f) * ((float)((AmmoNet)StaticData.allTypes.get_Item(((SlotItemWeapon)current).get_AmmoType())).damage / 100f) : (float)attachedWeaponDamage * (1f + pShip.dmgPercentAlien / 100f) * ((float)((AmmoNet)StaticData.allTypes.get_Item(((SlotItemWeapon)current).get_AmmoType())).damage / 100f) * 1.3f);
				dmg = dmg + (int)single;
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

	private EWeaponStatus CalculateWeaponStatus(SlotItem item)
	{
		__MerchantWindow.<CalculateWeaponStatus>c__AnonStorey96 variable = null;
		if (item == null)
		{
			return 1;
		}
		SlotItemWeapon slotItemWeapon = (SlotItemWeapon)item;
		if (Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(NetworkScript.player.playerBelongings.playerItems.slotItems, new Func<SlotItem, bool>(variable, (SlotItem si) => (si.get_Amount() <= 0 ? false : si.get_ItemType() == this.weapon.get_AmmoType())))) == null)
		{
			return 5;
		}
		return 3;
	}

	private void CancelReroll(EventHandlerParam prm)
	{
		this.DialogClose(null);
	}

	public new void Clear()
	{
		base.RemoveGuiElement(this.shipTexture);
		base.RemoveGuiElement(this.shipStatsLbl);
		base.RemoveGuiElement(this.selectedSectionLbl);
		base.RemoveGuiElement(this.merchantTabTexture);
		base.RemoveGuiElement(this.lblCurrentShipName);
		base.RemoveGuiElement(this.tab1Button);
		base.RemoveGuiElement(this.tab5Button);
		base.RemoveGuiElement(this.tab6Button);
		base.RemoveGuiElement(this.tab2Button);
		base.RemoveGuiElement(this.tab3Button);
		base.RemoveGuiElement(this.tab4Button);
		base.RemoveGuiElement(this.inventoryTabTexture);
		base.RemoveGuiElement(this.inventoryTab1Button);
		base.RemoveGuiElement(this.inventoryTab2Button);
		base.RemoveGuiElement(this.laserSlotsLabel);
		base.RemoveGuiElement(this.plasmaSlotsLabel);
		base.RemoveGuiElement(this.ionSlotsLabel);
		base.RemoveGuiElement(this.btnMerchant);
		base.RemoveGuiElement(this.btnGambler);
		base.RemoveGuiElement(this.btnReroll);
		base.RemoveGuiElement(this.btnNextShip);
		base.RemoveGuiElement(this.btnPreviousShip);
		base.RemoveGuiElement(this.txSelectedShip);
		base.RemoveGuiElement(this.btnSelectShip);
		base.RemoveGuiElement(this.lblCurrentShipName);
		base.RemoveGuiElement(this.lblDamageVal);
		base.RemoveGuiElement(this.lblCorpusVal);
		base.RemoveGuiElement(this.lblShieldVal);
		base.RemoveGuiElement(this.lblAvoidanceVal);
		base.RemoveGuiElement(this.lblTargetingVal);
		base.RemoveGuiElement(this.lblSpeedVal);
		base.RemoveGuiElement(this.lblCargoVal);
		base.RemoveGuiElement(this.itemRerolEmptyBoxInfo);
		this.itemRerolEmptyBoxInfo = null;
		this.ClearAmmoSlots();
		foreach (GuiElement mainSlot in this.mainSlots)
		{
			base.RemoveGuiElement(mainSlot);
		}
		this.mainSlots.Clear();
		foreach (GuiElement guiElement in this.forDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.forDelete.Clear();
		foreach (GuiElement guiElement1 in this.beforeSectionForDelete)
		{
			base.RemoveGuiElement(guiElement1);
		}
		this.beforeSectionForDelete.Clear();
		foreach (GuiElement guiElement2 in this.afterSectionForDelete)
		{
			base.RemoveGuiElement(guiElement2);
		}
		this.afterSectionForDelete.Clear();
		foreach (GuiElement guiElement3 in this.rerollSectionForDelete)
		{
			base.RemoveGuiElement(guiElement3);
		}
		this.rerollSectionForDelete.Clear();
		foreach (GuiElement guiElement4 in this.bonusValuesForDelete)
		{
			base.RemoveGuiElement(guiElement4);
		}
		this.bonusValuesForDelete.Clear();
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

	private void ConfirmExpand(object prm)
	{
		// 
		// Current member / type: System.Void __MerchantWindow::ConfirmExpand(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void ConfirmExpand(System.Object)
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

	private void ConfirmShipSlotExpand(EventHandlerParam prm)
	{
		int num = 0;
		SelectedCurrency selectedCurrency = (int)prm.customData;
		this.buyShipSlotData.paymentCurrency = (int)prm.customData;
		if (NetworkScript.player.playerBelongings.ExpandShipSlotNew(this.buyShipSlotData.wantedSlot, selectedCurrency, ref num))
		{
			playWebGame.udp.ExecuteCommand(PureUdpClient.CommandExpandSlots, this.buyShipSlotData, 49);
		}
		this.ClearAmmoSlots();
		Inventory.ClearSlots(this);
		this.CreateInventorySlots();
		this.CreateExtrasTab();
		this.CreateWeaponsTab();
		this.CreateStructureTab();
		this.CreateMerchantSlots();
		this.DrawExpandShipSlots();
		Inventory.DrawPlaces(this);
		this.expandShipSlotWindow.Cancel();
	}

	private void CrateBonusDropDown(SlotItem slotItem, byte bonusIndex)
	{
		float single = 584f;
		float single1 = 110f;
		switch (bonusIndex)
		{
			case 1:
			{
				single1 = 110f;
				break;
			}
			case 2:
			{
				single1 = 140f;
				break;
			}
			case 3:
			{
				single1 = 170f;
				break;
			}
			case 4:
			{
				single1 = 200f;
				break;
			}
			case 5:
			{
				single1 = 230f;
				break;
			}
		}
		GuiDropdown guiDropdown = new GuiDropdown()
		{
			X = single,
			Y = single1
		};
		guiDropdown.boundries.set_width(180f);
		guiDropdown.AddItem(StaticData.Translate("key_reroll_random"), true);
		guiDropdown.OnItemSelected = new Action<int>(this, __MerchantWindow.BonusSelected);
		if (PlayerItems.IsWeapon(slotItem.get_ItemType()))
		{
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_inventory_damage"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_inventory_range"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_inventory_cooldown"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_inventory_penetration"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_inventory_targeting"), string.Empty), false);
		}
		else if (PlayerItems.IsCorpus(slotItem.get_ItemType()))
		{
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_corpus"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_corpusPercent"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_targeting"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_avoidance"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_shieldRegen"), string.Empty), false);
		}
		else if (PlayerItems.IsShield(slotItem.get_ItemType()))
		{
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_shield"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_shieldPercent"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_targeting"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_avoidance"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_shieldRegen"), string.Empty), false);
		}
		else if (PlayerItems.IsEngine(slotItem.get_ItemType()))
		{
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_speed"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_avoidance"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_cargo"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_cargoPercent"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_shieldRegen"), string.Empty), false);
		}
		else if (PlayerItems.IsExtraCooldown(slotItem.get_ItemType()))
		{
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_laserCooldown"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_plasmaCooldown"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_ionCooldown"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_laserDmg"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_plasmaDmg"), string.Empty), false);
		}
		else if (PlayerItems.IsExtraCargoMining(slotItem.get_ItemType()))
		{
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_targeting"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_avoidance"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_cargo"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_cargoPercent"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_miningSpeed"), string.Empty), false);
		}
		else if (PlayerItems.IsExtraDamage(slotItem.get_ItemType()))
		{
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_targeting"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_avoidance"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_laserDmg"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_plasmaDmg"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_ionDmg"), string.Empty), false);
		}
		else if (PlayerItems.IsExtraOther(slotItem.get_ItemType()))
		{
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_speed"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_targeting"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_avoidance"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_miningSpeed"), string.Empty), false);
			guiDropdown.AddItem(string.Format(StaticData.Translate("key_item_bonus_shieldRegen"), string.Empty), false);
		}
		if (!this.selectedBonusesInDropdowns.ContainsKey((int)bonusIndex))
		{
			this.selectedBonusesInDropdowns.Add((int)bonusIndex, 0);
		}
		else
		{
			guiDropdown.selectedItem = this.selectedBonusesInDropdowns.get_Item((int)bonusIndex);
		}
		base.AddGuiElement(guiDropdown);
		this.rerollSectionForDelete.Add(guiDropdown);
		this.activeDropdowns.Add((int)bonusIndex, guiDropdown);
		GuiCheckbox guiCheckbox = new GuiCheckbox()
		{
			X = single + 339f,
			Y = single1 + 1f
		};
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_reroll_maximize_tooltip"),
			customData2 = guiCheckbox
		};
		guiCheckbox.tooltipWindowParam = eventHandlerParam;
		guiCheckbox.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		if (!this.selectedMaximizeInCheckboxes.ContainsKey((int)bonusIndex))
		{
			this.selectedMaximizeInCheckboxes.Add((int)bonusIndex, false);
		}
		else
		{
			guiCheckbox.Selected = this.selectedMaximizeInCheckboxes.get_Item((int)bonusIndex);
		}
		guiCheckbox.OnCheckboxSelected = new Action<bool>(this, __MerchantWindow.OnMaximizeClick);
		guiCheckbox.isActive = true;
		base.AddGuiElement(guiCheckbox);
		this.rerollSectionForDelete.Add(guiCheckbox);
		this.activeCheckboxes.Add((int)bonusIndex, guiCheckbox);
	}

	public override void Create()
	{
		this.IsMobile = false;
		this.inventorySlotCnt = NetworkScript.player.playerBelongings.playerInventorySlots;
		base.SetBackgroundTexture("NewGUI", "MerchantWndBackground");
		base.PutToCenter();
		this.isHidden = false;
		this.zOrder = 210;
		Inventory.window = this;
		Inventory.isRightClickActionEnable = true;
		Inventory.isVaultMenuOpen = false;
		Inventory.isItemRerollMenuOpen = false;
		Inventory.isGuildVaultMenuOpen = false;
		Inventory.ConfigWndAfterRightClkAction = new Action(this, __MerchantWindow.Refresh);
		this.selectedSectionLbl = new GuiLabel()
		{
			TextColor = GuiNewStyleBar.orangeColor,
			text = StaticData.Translate("key_merchant_lbl_merchant"),
			boundries = new Rect(42f, 50f, 300f, 20f),
			FontSize = 18,
			Font = GuiLabel.FontBold,
			Alignment = 4
		};
		base.AddGuiElement(this.selectedSectionLbl);
		switch (this.selectedOption)
		{
			case __MerchantWindow.MenuOption.Merchant:
			{
				this.selectedSectionLbl.text = StaticData.Translate("key_merchant_lbl_merchant");
				break;
			}
			case __MerchantWindow.MenuOption.Gambler:
			{
				this.selectedSectionLbl.text = StaticData.Translate("key_merchant_lbl_gambler");
				break;
			}
			case __MerchantWindow.MenuOption.Reroll:
			{
				this.selectedSectionLbl.text = StaticData.Translate("key_merchant_lbl_reroll");
				base.SetBackgroundTexture("NewGUI", "item_reroll");
				break;
			}
		}
		this.btnMerchant = new GuiButtonFixed();
		this.btnMerchant.SetTexture("GUI", "btnMerchant");
		this.btnMerchant.boundries = new Rect(45f, this.tabBtnOffsetY - 80f, 94f, 74f);
		GuiButtonFixed guiButtonFixed = this.btnMerchant;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = __MerchantWindow.MenuOption.Merchant
		};
		guiButtonFixed.eventHandlerParam = eventHandlerParam;
		this.btnMerchant.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.OnMenuOptionClick);
		GuiButtonFixed guiButtonFixed1 = this.btnMerchant;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_merchant_tooltips_merchant"),
			customData2 = this.btnMerchant
		};
		guiButtonFixed1.tooltipWindowParam = eventHandlerParam;
		this.btnMerchant.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		this.btnMerchant.Caption = string.Empty;
		if (this.selectedOption != __MerchantWindow.MenuOption.Merchant)
		{
			this.btnMerchant.SetTextureNormal("GUI", "btnMerchantNml");
		}
		else
		{
			this.btnMerchant.SetTextureNormal("GUI", "btnMerchantClk");
		}
		this.btnMerchant.isEnabled = true;
		base.AddGuiElement(this.btnMerchant);
		if (NetworkScript.player.playerBelongings.playerLevel >= 6)
		{
			this.btnGambler = new GuiButtonFixed();
			this.btnGambler.SetTexture("GUI", "btnGambler");
			this.btnGambler.boundries = new Rect(145f, this.tabBtnOffsetY - 80f, 94f, 74f);
			GuiButtonFixed guiButtonFixed2 = this.btnGambler;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = __MerchantWindow.MenuOption.Gambler
			};
			guiButtonFixed2.eventHandlerParam = eventHandlerParam;
			this.btnGambler.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.OnMenuOptionClick);
			GuiButtonFixed guiButtonFixed3 = this.btnGambler;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_merchant_tooltips_gambler"),
				customData2 = this.btnGambler
			};
			guiButtonFixed3.tooltipWindowParam = eventHandlerParam;
			this.btnGambler.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			this.btnGambler.Caption = string.Empty;
			if (this.selectedOption != __MerchantWindow.MenuOption.Gambler)
			{
				this.btnGambler.SetTextureNormal("GUI", "btnGamblerNml");
			}
			else
			{
				this.btnGambler.SetTextureNormal("GUI", "btnGamblerClk");
			}
			base.AddGuiElement(this.btnGambler);
			this.btnReroll = new GuiButtonFixed();
			this.btnReroll.SetTexture("GUI", "btnReroll");
			this.btnReroll.boundries = new Rect(245f, this.tabBtnOffsetY - 80f, 94f, 74f);
			GuiButtonFixed guiButtonFixed4 = this.btnReroll;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = __MerchantWindow.MenuOption.Reroll
			};
			guiButtonFixed4.eventHandlerParam = eventHandlerParam;
			this.btnReroll.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.OnMenuOptionClick);
			GuiButtonFixed guiButtonFixed5 = this.btnReroll;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_merchant_tooltips_reroll"),
				customData2 = this.btnReroll
			};
			guiButtonFixed5.tooltipWindowParam = eventHandlerParam;
			this.btnReroll.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			this.btnReroll.Caption = string.Empty;
			if (this.selectedOption != __MerchantWindow.MenuOption.Reroll)
			{
				this.btnReroll.SetTextureNormal("GUI", "btnRerollNml");
			}
			else
			{
				this.btnReroll.SetTextureNormal("GUI", "btnRerollClk");
			}
			base.AddGuiElement(this.btnReroll);
		}
		Inventory.ClearSlots(this);
		Inventory.secondaryDropHandler = new Action<AndromedaGuiDragDropPlace, AndromedaGuiDragDropPlace>(this, __MerchantWindow.DropHandler);
		Inventory.closeStackablePopUp = new Action(this, __MerchantWindow.Refresh);
		switch (this.selectedOption)
		{
			case __MerchantWindow.MenuOption.Merchant:
			{
				this.DrawShipConfigSection();
				this.DrawItemCategoryTabs();
				this.DrawInventoryTabs();
				break;
			}
			case __MerchantWindow.MenuOption.Gambler:
			{
				this.DrawShipConfigSection();
				this.DrawItemCategoryTabs();
				this.DrawInventoryTabs();
				break;
			}
			case __MerchantWindow.MenuOption.Reroll:
			{
				this.CreateItemRerollMenu();
				break;
			}
		}
		Inventory.DrawPlaces(this);
		if (this.selectedOption == __MerchantWindow.MenuOption.Reroll)
		{
			Texture2D fromStaticSet = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "itemRerollDropZone");
			Texture2D texture2D = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "itemRerollDropZoneHvr");
			foreach (AndromedaGuiDragDropPlace place in Inventory.places)
			{
				if (place.isEmpty || place.item.get_BonusCnt() == 0)
				{
					place.frameTexture.SetTexture("GUI", "FrmInventoryDsb");
					ref Rect rectPointer = ref place.frameTexture.boundries;
					rectPointer.set_x(rectPointer.get_x() - 2f);
					ref Rect rectPointer1 = ref place.frameTexture.boundries;
					rectPointer1.set_y(rectPointer1.get_y() - 2f);
					if (place.txItem != null)
					{
						place.txItem.isUnavailable = true;
						place.txItem.leftClickRightAction = true;
					}
				}
				else
				{
					place.txItem.hoverParam = place.item;
					place.txItem.AddDropZone(new Vector2(359f, 72f), 111222333, fromStaticSet, texture2D);
					place.txItem.callbackAttribute = place.item.get_Slot() + place.item.get_SlotType() * 1000;
					place.txItem.leftClickRightAction = true;
				}
			}
		}
		this.UpdateSectionLabel(this.selectedMerchantTab);
	}

	private void CreateAfterSection()
	{
		Color color;
		string str;
		__MerchantWindow.<CreateAfterSection>c__AnonStoreyA2 variable = null;
		string str1;
		string str2;
		string str3;
		foreach (GuiElement guiElement in this.afterSectionForDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.afterSectionForDelete.Clear();
		int num = this.selectedSlotItem % 1000;
		int num1 = this.selectedSlotItem / 1000;
		SlotItem slotItem = Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(NetworkScript.player.playerBelongings.playerItems.slotItems, new Func<SlotItem, bool>(variable, (SlotItem t) => (t.get_Slot() != this.itemSlot ? false : t.get_SlotType() == (byte)this.itemSlotType))));
		if (slotItem == null)
		{
			return;
		}
		PlayerItemTypesData item = StaticData.allTypes.get_Item(slotItem.get_ItemType());
		Inventory.ItemRarity(slotItem, out str, out color);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(50f, 372f, 260f, 12f),
			text = StaticData.Translate("key_reroll_item_new_lbl"),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		base.AddGuiElement(guiLabel);
		this.afterSectionForDelete.Add(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(48f, 390f, 265f, 20f),
			text = str,
			Alignment = 4,
			TextColor = color,
			Font = GuiLabel.FontBold,
			FontSize = 11
		};
		base.AddGuiElement(guiLabel1);
		this.afterSectionForDelete.Add(guiLabel1);
		GuiTexture guiTexture = new GuiTexture()
		{
			boundries = new Rect(49f, 407f, 100f, 69f)
		};
		guiTexture.SetItemTextureKeepSize(slotItem.get_ItemType());
		base.AddGuiElement(guiTexture);
		this.afterSectionForDelete.Add(guiTexture);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(55f, 477f, 255f, 16f),
			text = string.Format(StaticData.Translate("key_tooltip_level_restriction"), StaticData.allTypes.get_Item(slotItem.get_ItemType()).levelRestriction),
			Alignment = 3,
			TextColor = (StaticData.allTypes.get_Item(slotItem.get_ItemType()).levelRestriction <= NetworkScript.player.playerBelongings.playerLevel ? Color.get_white() : GuiNewStyleBar.redColor),
			Font = GuiLabel.FontBold,
			FontSize = 10
		};
		base.AddGuiElement(guiLabel2);
		this.afterSectionForDelete.Add(guiLabel2);
		int num2 = 10;
		int num3 = 150;
		int num4 = 410;
		int num5 = 165;
		float single = 0f;
		int item1 = 0;
		int item2 = 0;
		int num6 = 0;
		int num7 = 0;
		if (PlayerItems.IsWeapon(slotItem.get_ItemType()))
		{
			WeaponsTypeNet weaponsTypeNet = (WeaponsTypeNet)StaticData.allTypes.get_Item(slotItem.get_ItemType());
			GuiLabel guiLabel3 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)num4, (float)num5, 12f),
				text = StaticData.Translate("key_inventory_damage"),
				FontSize = num2,
				Alignment = 3
			};
			base.AddGuiElement(guiLabel3);
			this.afterSectionForDelete.Add(guiLabel3);
			GuiLabel guiLabel4 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 12), (float)num5, 12f),
				text = StaticData.Translate("key_inventory_cooldown"),
				FontSize = num2,
				Alignment = 3
			};
			base.AddGuiElement(guiLabel4);
			this.afterSectionForDelete.Add(guiLabel4);
			GuiLabel guiLabel5 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 24), (float)num5, 12f),
				text = StaticData.Translate("key_inventory_range"),
				FontSize = num2,
				Alignment = 3
			};
			base.AddGuiElement(guiLabel5);
			this.afterSectionForDelete.Add(guiLabel5);
			GuiLabel guiLabel6 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 36), (float)num5, 12f),
				text = StaticData.Translate("key_inventory_penetration"),
				FontSize = num2,
				Alignment = 3
			};
			base.AddGuiElement(guiLabel6);
			this.afterSectionForDelete.Add(guiLabel6);
			GuiLabel guiLabel7 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 48), (float)num5, 12f),
				text = StaticData.Translate("key_inventory_targeting"),
				FontSize = num2,
				Alignment = 3
			};
			base.AddGuiElement(guiLabel7);
			this.afterSectionForDelete.Add(guiLabel7);
			GuiLabel fontBold = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)num4, (float)num5, 12f)
			};
			if (this.wantedBonusOne > 0)
			{
				item1 = Mineral.enchantsDamageTable.get_Item(slotItem.get_ItemType()).min;
				item2 = Mineral.enchantsDamageTable.get_Item(slotItem.get_ItemType()).max;
			}
			num6 = this.wantedBonusOne * item1;
			num7 = this.wantedBonusOne * item2;
			num6 = num6 + this.maximaizedBonusOne * (item2 - item1);
			int num8 = weaponsTypeNet.upgrades[((SlotItemWeapon)slotItem).get_UpgradeDamage()].damage;
			fontBold.text = (num6 != 0 ? string.Format("{0:##,##0} / {1:##,##0}", num8 + num6, num8 + num7) : num8.ToString("##,##0"));
			fontBold.FontSize = num2;
			fontBold.Font = GuiLabel.FontBold;
			fontBold.TextColor = (num6 != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			fontBold.Alignment = 5;
			base.AddGuiElement(fontBold);
			this.afterSectionForDelete.Add(fontBold);
			item1 = 0;
			item2 = 0;
			num6 = 0;
			num7 = 0;
			if (this.wantedBonusThree > 0)
			{
				item1 = 100;
				item2 = 500;
			}
			num6 = this.wantedBonusThree * item1;
			num7 = this.wantedBonusThree * item2;
			num6 = num6 + this.maximaizedBonusThree * (item2 - item1);
			int num9 = weaponsTypeNet.upgrades[((SlotItemWeapon)slotItem).get_UpgradeCooldown()].cooldown;
			GuiLabel guiLabel8 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 12), (float)num5, 12f),
				text = (num6 != 0 ? string.Format("{0:##,##0} / {1:##,##0}", num9 - num6, num9 - num7) : num9.ToString("##,##0")),
				FontSize = num2,
				Font = GuiLabel.FontBold,
				TextColor = (num6 != 0 ? GuiNewStyleBar.greenColor : Color.get_white()),
				Alignment = 5
			};
			base.AddGuiElement(guiLabel8);
			this.afterSectionForDelete.Add(guiLabel8);
			item1 = 0;
			item2 = 0;
			num6 = 0;
			num7 = 0;
			if (this.wantedBonusTwo > 0)
			{
				item1 = 1;
				item2 = 4;
			}
			num6 = this.wantedBonusTwo * item1;
			num7 = this.wantedBonusTwo * item2;
			num6 = num6 + this.maximaizedBonusTwo * (item2 - item1);
			int num10 = weaponsTypeNet.upgrades[((SlotItemWeapon)slotItem).get_UpgradeRange()].range;
			GuiLabel guiLabel9 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 24), (float)num5, 12f),
				text = (num6 != 0 ? string.Format("{0:##,##0} / {1:##,##0}", num10 + num6, num10 + num7) : num10.ToString("##,##0")),
				FontSize = num2,
				Font = GuiLabel.FontBold,
				TextColor = (num6 != 0 ? GuiNewStyleBar.greenColor : Color.get_white()),
				Alignment = 5
			};
			base.AddGuiElement(guiLabel9);
			this.afterSectionForDelete.Add(guiLabel9);
			item1 = 0;
			item2 = 0;
			num6 = 0;
			num7 = 0;
			if (this.wantedBonusFour > 0)
			{
				item1 = 2;
				item2 = 11;
			}
			num6 = this.wantedBonusFour * item1;
			num7 = this.wantedBonusFour * item2;
			num6 = num6 + this.maximaizedBonusFour * (item2 - item1);
			int num11 = weaponsTypeNet.upgrades[((SlotItemWeapon)slotItem).get_UpgradePenetration()].penetration;
			GuiLabel guiLabel10 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 36), (float)num5, 12f),
				text = (num6 != 0 ? string.Format("{0:##,##0} / {1:##,##0}", num11 + num6, num11 + num7) : num11.ToString("##,##0")),
				FontSize = num2,
				Font = GuiLabel.FontBold,
				TextColor = (num6 != 0 ? GuiNewStyleBar.greenColor : Color.get_white()),
				Alignment = 5
			};
			base.AddGuiElement(guiLabel10);
			this.afterSectionForDelete.Add(guiLabel10);
			item1 = 0;
			item2 = 0;
			num6 = 0;
			num7 = 0;
			if (this.wantedBonusFive > 0)
			{
				int weaponTierByType = this.GetWeaponTierByType(slotItem.get_ItemType());
				item1 = Mineral.enchantsTargetingTable.get_Item(weaponTierByType).min;
				item2 = Mineral.enchantsTargetingTable.get_Item(weaponTierByType).max;
			}
			num6 = this.wantedBonusFive * item1;
			num7 = this.wantedBonusFive * item2;
			num6 = num6 + this.maximaizedBonusFive * (item2 - item1);
			int num12 = weaponsTypeNet.upgrades[((SlotItemWeapon)slotItem).get_UpgradeTargeting()].targeting;
			GuiLabel guiLabel11 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 48), (float)num5, 12f),
				text = (num6 != 0 ? string.Format("{0:##,##0} / {1:##,##0}", num12 + num6, num12 + num7) : num12.ToString("##,##0")),
				FontSize = num2,
				Font = GuiLabel.FontBold,
				TextColor = (num6 != 0 ? GuiNewStyleBar.greenColor : Color.get_white()),
				Alignment = 5
			};
			base.AddGuiElement(guiLabel11);
			this.afterSectionForDelete.Add(guiLabel11);
		}
		else if (PlayerItems.IsCorpus(slotItem.get_ItemType()))
		{
			GuiLabel guiLabel12 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 24), (float)num5, 12f),
				text = StaticData.Translate("key_inventory_corpus"),
				FontSize = num2,
				Alignment = 3
			};
			base.AddGuiElement(guiLabel12);
			this.afterSectionForDelete.Add(guiLabel12);
			GuiLabel fontBold1 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 24), (float)num5, 12f)
			};
			if (this.wantedBonusOne > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(2) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
			}
			num6 = this.wantedBonusOne * item1;
			num7 = this.wantedBonusOne * item2;
			if (this.maximaizedBonusOne > 0)
			{
				num6 = num6 + this.maximaizedBonusOne * (item2 - item1);
			}
			GuiLabel guiLabel13 = fontBold1;
			if (num6 != 0)
			{
				str3 = string.Format("{0:##,##0} / {1:##,##0}", ((GeneratorNet)item).bonusValue + num6, ((GeneratorNet)item).bonusValue + num7);
			}
			else
			{
				int num13 = ((GeneratorNet)item).bonusValue + num6;
				str3 = num13.ToString("##,##0");
			}
			guiLabel13.text = str3;
			fontBold1.FontSize = num2;
			fontBold1.Font = GuiLabel.FontBold;
			fontBold1.TextColor = (num6 != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			fontBold1.Alignment = 5;
			base.AddGuiElement(fontBold1);
			this.afterSectionForDelete.Add(fontBold1);
		}
		else if (PlayerItems.IsShield(slotItem.get_ItemType()))
		{
			GuiLabel guiLabel14 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 24), (float)num5, 12f),
				text = StaticData.Translate("key_inventory_shield"),
				FontSize = num2,
				Alignment = 3
			};
			base.AddGuiElement(guiLabel14);
			this.afterSectionForDelete.Add(guiLabel14);
			GuiLabel fontBold2 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 24), (float)num5, 12f)
			};
			if (this.wantedBonusOne > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(0) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
			}
			num6 = this.wantedBonusOne * item1;
			num7 = this.wantedBonusOne * item2;
			if (this.maximaizedBonusOne > 0)
			{
				num6 = num6 + this.maximaizedBonusOne * (item2 - item1);
			}
			GuiLabel guiLabel15 = fontBold2;
			if (num6 != 0)
			{
				str2 = string.Format("{0:##,##0} / {1:##,##0}", ((GeneratorNet)item).bonusValue + num6, ((GeneratorNet)item).bonusValue + num7);
			}
			else
			{
				int num14 = ((GeneratorNet)item).bonusValue + num6;
				str2 = num14.ToString("##,##0");
			}
			guiLabel15.text = str2;
			fontBold2.FontSize = num2;
			fontBold2.Font = GuiLabel.FontBold;
			fontBold2.TextColor = (num6 != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			fontBold2.Alignment = 5;
			base.AddGuiElement(fontBold2);
			this.afterSectionForDelete.Add(fontBold2);
		}
		else if (PlayerItems.IsEngine(slotItem.get_ItemType()))
		{
			GuiLabel guiLabel16 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 24), (float)num5, 12f),
				text = StaticData.Translate("key_inventory_speed"),
				FontSize = num2,
				Alignment = 3
			};
			base.AddGuiElement(guiLabel16);
			this.afterSectionForDelete.Add(guiLabel16);
			GuiLabel fontBold3 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 24), (float)num5, 12f)
			};
			if (this.wantedBonusOne > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(4) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
			}
			num6 = this.wantedBonusOne * item1;
			num7 = this.wantedBonusOne * item2;
			if (this.maximaizedBonusOne > 0)
			{
				num6 = num6 + this.maximaizedBonusOne * (item2 - item1);
			}
			GuiLabel guiLabel17 = fontBold3;
			if (num6 != 0)
			{
				str1 = string.Format("{0:##,##0} / {1:##,##0}", ((GeneratorNet)item).bonusValue + num6, ((GeneratorNet)item).bonusValue + num7);
			}
			else
			{
				int num15 = ((GeneratorNet)item).bonusValue + num6;
				str1 = num15.ToString("##,##0");
			}
			guiLabel17.text = str1;
			fontBold3.FontSize = num2;
			fontBold3.Font = GuiLabel.FontBold;
			fontBold3.TextColor = (slotItem.get_BonusOne() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			fontBold3.Alignment = 5;
			base.AddGuiElement(fontBold3);
			this.afterSectionForDelete.Add(fontBold3);
		}
		else if (PlayerItems.IsExtra(slotItem.get_ItemType()))
		{
			GuiLabel guiLabel18 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 12), (float)num5, 36f),
				FontSize = num2,
				Alignment = 3
			};
			base.AddGuiElement(guiLabel18);
			this.afterSectionForDelete.Add(guiLabel18);
			GuiLabel guiLabel19 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 12), (float)num5, 36f),
				text = string.Empty,
				Font = GuiLabel.FontBold,
				Alignment = 5
			};
			base.AddGuiElement(guiLabel19);
			this.afterSectionForDelete.Add(guiLabel19);
			if (PlayerItems.IsForExtraCargoSpace(slotItem.get_ItemType()))
			{
				if (this.wantedBonusFour > 0)
				{
					single = BonusesConstant.bonusConstatn.get_Item(7) * (float)item.levelRestriction;
					item1 = (int)Math.Max(1f, single);
					item2 = (int)Math.Max(1f, single * 3f);
				}
				num6 = this.wantedBonusFour * item1;
				num7 = this.wantedBonusFour * item2;
				num6 = num6 + this.maximaizedBonusFour * (item2 - item1);
				guiLabel18.text = StaticData.Translate("key_extra_cargo");
				guiLabel19.text = (num6 != 0 ? string.Format("+{0:##,##0}% / {1:##,##0}% ", ((ExtrasNet)item).efValue + num6, ((ExtrasNet)item).efValue + num7) : string.Format("+{0:##,##0}%", ((ExtrasNet)item).efValue));
				guiLabel19.TextColor = (num6 != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraLightMiningDrill || slotItem.get_ItemType() == PlayerItems.TypeExtraUltraMiningDrill)
			{
				if (this.wantedBonusFive > 0)
				{
					single = BonusesConstant.bonusConstatn.get_Item(15) * (float)item.levelRestriction;
					item1 = (int)Math.Max(1f, single);
					item2 = (int)Math.Max(1f, single * 3f);
				}
				num6 = this.wantedBonusFive * item1;
				num7 = this.wantedBonusFive * item2;
				num6 = num6 + this.maximaizedBonusFive * (item2 - item1);
				guiLabel18.text = StaticData.Translate("key_extra_faster_mining");
				guiLabel19.text = (num6 != 0 ? string.Format("+{0:##,##0}% / {1:##,##0}%", ((ExtrasNet)item).efValue + num6, ((ExtrasNet)item).efValue + num7) : string.Format("+{0:##,##0}%", ((ExtrasNet)item).efValue));
				guiLabel19.TextColor = (num6 != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			}
			else if (PlayerItems.IsForShieldRegen(slotItem.get_ItemType()))
			{
				if (this.wantedBonusFive > 0)
				{
					single = BonusesConstant.bonusConstatn.get_Item(16) * (float)item.levelRestriction;
					item1 = (int)Math.Max(1f, single);
					item2 = (int)Math.Max(1f, single * 3f);
				}
				num6 = this.wantedBonusFive * item1;
				num7 = this.wantedBonusFive * item2;
				num6 = num6 + this.maximaizedBonusFive * (item2 - item1);
				guiLabel18.text = StaticData.Translate("key_extra_shield_regen");
				guiLabel19.text = (num6 != 0 ? string.Format("+{0:##,##0} / {1:##,##0}", ((ExtrasNet)item).efValue + num6, ((ExtrasNet)item).efValue + num7) : string.Format("+{0:##,##0}", ((ExtrasNet)item).efValue));
				guiLabel19.TextColor = (num6 != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			}
			else if (PlayerItems.IsForLaserCooldown(slotItem.get_ItemType()))
			{
				if (this.wantedBonusOne > 0)
				{
					single = BonusesConstant.bonusConstatn.get_Item(9) * (float)item.levelRestriction;
					item1 = (int)Math.Max(1f, single);
					item2 = (int)Math.Max(1f, single * 3f);
				}
				num6 = this.wantedBonusOne * item1;
				num7 = this.wantedBonusOne * item2;
				num6 = num6 + this.maximaizedBonusOne * (item2 - item1);
				guiLabel18.text = StaticData.Translate("key_extra_cooldown_laser");
				guiLabel19.text = (num6 != 0 ? string.Format("-{0:##,##0} / -{1:##,##0}", ((ExtrasNet)item).efValue + num6 * 50, ((ExtrasNet)item).efValue + num7 * 50) : string.Format("-{0:##,##0}", ((ExtrasNet)item).efValue));
				guiLabel19.TextColor = (num6 != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			}
			else if (PlayerItems.IsForPlasmaCooldown(slotItem.get_ItemType()))
			{
				if (this.wantedBonusTwo > 0)
				{
					single = BonusesConstant.bonusConstatn.get_Item(10) * (float)item.levelRestriction;
					item1 = (int)Math.Max(1f, single);
					item2 = (int)Math.Max(1f, single * 3f);
				}
				num6 = this.wantedBonusTwo * item1;
				num7 = this.wantedBonusTwo * item2;
				num6 = num6 + this.maximaizedBonusTwo * (item2 - item1);
				guiLabel18.text = StaticData.Translate("key_extra_cooldown_plasma");
				guiLabel19.text = (num6 != 0 ? string.Format("-{0:##,##0} / -{1:##,##0}", ((ExtrasNet)item).efValue + num6 * 50, ((ExtrasNet)item).efValue + num7 * 50) : string.Format("-{0:##,##0}", ((ExtrasNet)item).efValue));
				guiLabel19.TextColor = (num6 != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			}
			else if (PlayerItems.IsForIonCooldown(slotItem.get_ItemType()))
			{
				if (this.wantedBonusThree > 0)
				{
					single = BonusesConstant.bonusConstatn.get_Item(11) * (float)item.levelRestriction;
					item1 = (int)Math.Max(1f, single);
					item2 = (int)Math.Max(1f, single * 3f);
				}
				num6 = this.wantedBonusThree * item1;
				num7 = this.wantedBonusThree * item2;
				num6 = num6 + this.maximaizedBonusThree * (item2 - item1);
				guiLabel18.text = StaticData.Translate("key_extra_cooldown_ion");
				guiLabel19.text = (num6 != 0 ? string.Format("-{0:##,##0} / -{1:##,##0}", ((ExtrasNet)item).efValue + num6 * 50, ((ExtrasNet)item).efValue + num7 * 50) : string.Format("-{0:##,##0}", ((ExtrasNet)item).efValue));
				guiLabel19.TextColor = (num6 != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraUltraWeaponsCoolant)
			{
				guiLabel18.text = StaticData.Translate("key_extra_cooldown_all");
				guiLabel19.text = string.Format("-{0:##,##0}", ((ExtrasNet)item).efValue);
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraLaserWeaponsModule)
			{
				guiLabel18.text = StaticData.Translate("key_extra_dmg_laser");
				guiLabel19.text = string.Format("+{0:##,##0}%", ((ExtrasNet)item).efValue);
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraPlasmaWeaponsModule)
			{
				guiLabel18.text = StaticData.Translate("key_extra_dmg_plasma");
				guiLabel19.text = string.Format("+{0:##,##0}%", ((ExtrasNet)item).efValue);
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraIonWeaponsModule)
			{
				guiLabel18.text = StaticData.Translate("key_extra_dmg_ion");
				guiLabel19.text = string.Format("+{0:##,##0}%", ((ExtrasNet)item).efValue);
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraUltraWeaponsModule)
			{
				guiLabel18.text = StaticData.Translate("key_extra_dmg_all");
				guiLabel19.text = string.Format("+{0:##,##0}%", ((ExtrasNet)item).efValue);
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraLaserAimingCPU)
			{
				guiLabel18.text = StaticData.Translate("key_extra_targeting_laser");
				guiLabel19.text = string.Format("+{0:##,##0}", ((ExtrasNet)item).efValue);
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraPlasmaAimingCPU)
			{
				guiLabel18.text = StaticData.Translate("key_extra_targeting_plasma");
				guiLabel19.text = string.Format("+{0:##,##0}", ((ExtrasNet)item).efValue);
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraIonAimingCPU)
			{
				guiLabel18.text = StaticData.Translate("key_extra_targeting_ion");
				guiLabel19.text = string.Format("+{0:##,##0}", ((ExtrasNet)item).efValue);
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraUltraAimingCPU)
			{
				guiLabel18.text = StaticData.Translate("key_extra_targeting_all");
				guiLabel19.text = string.Format("+{0:##,##0}", ((ExtrasNet)item).efValue);
			}
			guiLabel18.boundries.set_width((float)num5 - guiLabel19.TextWidth - 5f);
		}
		string empty = string.Empty;
		if (PlayerItems.IsWeapon(slotItem.get_ItemType()))
		{
			if (this.wantedBonusOne > 0)
			{
				item1 = Mineral.enchantsDamageTable.get_Item(slotItem.get_ItemType()).min;
				item2 = Mineral.enchantsDamageTable.get_Item(slotItem.get_ItemType()).max;
				num6 = this.wantedBonusOne * item1;
				num7 = this.wantedBonusOne * item2;
				num6 = num6 + this.maximaizedBonusOne * (item2 - item1);
				string str4 = string.Format("+{0} {1}", string.Format("{0} / {1}", num6, num7), StaticData.Translate("key_inventory_damage"));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str4), "\n");
			}
			if (this.wantedBonusTwo > 0)
			{
				item1 = 1;
				item2 = 4;
				num6 = this.wantedBonusTwo * item1;
				num7 = this.wantedBonusTwo * item2;
				num6 = num6 + this.maximaizedBonusTwo * (item2 - item1);
				string str5 = string.Format("+{0} {1}", string.Format("{0} / {1}", num6, num7), StaticData.Translate("key_inventory_range"));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str5), "\n");
			}
			if (this.wantedBonusThree > 0)
			{
				item1 = 100;
				item2 = 500;
				num6 = this.wantedBonusThree * item1;
				num7 = this.wantedBonusThree * item2;
				num6 = num6 + this.maximaizedBonusThree * (item2 - item1);
				string str6 = string.Format("-{0} {1}", string.Format("{0} / {1}", num6, num7), StaticData.Translate("key_inventory_cooldown"));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str6), "\n");
			}
			if (this.wantedBonusFour > 0)
			{
				item1 = 2;
				item2 = 11;
				num6 = this.wantedBonusFour * item1;
				num7 = this.wantedBonusFour * item2;
				num6 = num6 + this.maximaizedBonusFour * (item2 - item1);
				string str7 = string.Format("+{0} {1}", string.Format("{0} / {1}", num6, num7), StaticData.Translate("key_inventory_penetration"));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str7), "\n");
			}
			if (this.wantedBonusFive > 0)
			{
				int weaponTierByType1 = this.GetWeaponTierByType(slotItem.get_ItemType());
				item1 = Mineral.enchantsTargetingTable.get_Item(weaponTierByType1).min;
				item2 = Mineral.enchantsTargetingTable.get_Item(weaponTierByType1).max;
				num6 = this.wantedBonusFive * item1;
				num7 = this.wantedBonusFive * item2;
				num6 = num6 + this.maximaizedBonusFive * (item2 - item1);
				string str8 = string.Format("+{0} {1}", string.Format("{0} / {1}", num6, num7), StaticData.Translate("key_inventory_targeting"));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str8), "\n");
			}
		}
		else if (PlayerItems.IsCorpus(slotItem.get_ItemType()))
		{
			if (this.wantedBonusOne > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(2) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusOne * item1;
				num7 = this.wantedBonusOne * item2;
				num6 = num6 + this.maximaizedBonusOne * (item2 - item1);
				string str9 = string.Format(StaticData.Translate("key_item_bonus_corpus"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str9), "\n");
			}
			if (this.wantedBonusTwo > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(3) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusTwo * item1;
				num7 = this.wantedBonusTwo * item2;
				num6 = num6 + this.maximaizedBonusTwo * (item2 - item1);
				string str10 = string.Format(StaticData.Translate("key_item_bonus_corpusPercent"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str10), "\n");
			}
			if (this.wantedBonusThree > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(5) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusThree * item1;
				num7 = this.wantedBonusThree * item2;
				num6 = num6 + this.maximaizedBonusThree * (item2 - item1);
				string str11 = string.Format(StaticData.Translate("key_item_bonus_targeting"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str11), "\n");
			}
			if (this.wantedBonusFour > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(6) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusFour * item1;
				num7 = this.wantedBonusFour * item2;
				num6 = num6 + this.maximaizedBonusFour * (item2 - item1);
				string str12 = string.Format(StaticData.Translate("key_item_bonus_avoidance"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str12), "\n");
			}
			if (this.wantedBonusFive > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(16) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusFive * item1;
				num7 = this.wantedBonusFive * item2;
				num6 = num6 + this.maximaizedBonusFive * (item2 - item1);
				string str13 = string.Format(StaticData.Translate("key_item_bonus_shieldRegen"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str13), "\n");
			}
		}
		else if (PlayerItems.IsShield(slotItem.get_ItemType()))
		{
			if (this.wantedBonusOne > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(0) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusOne * item1;
				num7 = this.wantedBonusOne * item2;
				num6 = num6 + this.maximaizedBonusOne * (item2 - item1);
				string str14 = string.Format(StaticData.Translate("key_item_bonus_shield"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str14), "\n");
			}
			if (this.wantedBonusTwo > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(1) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusTwo * item1;
				num7 = this.wantedBonusTwo * item2;
				num6 = num6 + this.maximaizedBonusTwo * (item2 - item1);
				string str15 = string.Format(StaticData.Translate("key_item_bonus_shieldPercent"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str15), "\n");
			}
			if (this.wantedBonusThree > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(5) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusThree * item1;
				num7 = this.wantedBonusThree * item2;
				num6 = num6 + this.maximaizedBonusThree * (item2 - item1);
				string str16 = string.Format(StaticData.Translate("key_item_bonus_targeting"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str16), "\n");
			}
			if (this.wantedBonusFour > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(6) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusFour * item1;
				num7 = this.wantedBonusFour * item2;
				num6 = num6 + this.maximaizedBonusFour * (item2 - item1);
				string str17 = string.Format(StaticData.Translate("key_item_bonus_avoidance"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str17), "\n");
			}
			if (this.wantedBonusFive > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(16) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusFive * item1;
				num7 = this.wantedBonusFive * item2;
				num6 = num6 + this.maximaizedBonusFive * (item2 - item1);
				string str18 = string.Format(StaticData.Translate("key_item_bonus_shieldRegen"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str18), "\n");
			}
		}
		else if (PlayerItems.IsEngine(slotItem.get_ItemType()))
		{
			if (this.wantedBonusOne > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(4) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusOne * item1;
				num7 = this.wantedBonusOne * item2;
				num6 = num6 + this.maximaizedBonusOne * (item2 - item1);
				string str19 = string.Format(StaticData.Translate("key_item_bonus_speed"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str19), "\n");
			}
			if (this.wantedBonusTwo > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(6) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusTwo * item1;
				num7 = this.wantedBonusTwo * item2;
				num6 = num6 + this.maximaizedBonusTwo * (item2 - item1);
				string str20 = string.Format(StaticData.Translate("key_item_bonus_avoidance"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str20), "\n");
			}
			if (this.wantedBonusThree > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(7) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusThree * item1;
				num7 = this.wantedBonusThree * item2;
				num6 = num6 + this.maximaizedBonusThree * (item2 - item1);
				string str21 = string.Format(StaticData.Translate("key_item_bonus_cargo"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str21), "\n");
			}
			if (this.wantedBonusFour > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(8) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusFour * item1;
				num7 = this.wantedBonusFour * item2;
				num6 = num6 + this.maximaizedBonusFour * (item2 - item1);
				string str22 = string.Format(StaticData.Translate("key_item_bonus_cargoPercent"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str22), "\n");
			}
			if (this.wantedBonusFive > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(16) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusFive * item1;
				num7 = this.wantedBonusFive * item2;
				num6 = num6 + this.maximaizedBonusFive * (item2 - item1);
				string str23 = string.Format(StaticData.Translate("key_item_bonus_shieldRegen"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str23), "\n");
			}
		}
		else if (PlayerItems.IsExtraCooldown(slotItem.get_ItemType()))
		{
			if (this.wantedBonusOne > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(9) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusOne * item1;
				num7 = this.wantedBonusOne * item2;
				num6 = num6 + this.maximaizedBonusOne * (item2 - item1);
				string str24 = string.Format(StaticData.Translate("key_item_bonus_laserCooldown"), string.Format("{0} / {1}", num6 * 50, num7 * 50));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str24), "\n");
			}
			if (this.wantedBonusTwo > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(10) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusTwo * item1;
				num7 = this.wantedBonusTwo * item2;
				num6 = num6 + this.maximaizedBonusTwo * (item2 - item1);
				string str25 = string.Format(StaticData.Translate("key_item_bonus_plasmaCooldown"), string.Format("{0} / {1}", num6 * 50, num7 * 50));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str25), "\n");
			}
			if (this.wantedBonusThree > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(11) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusThree * item1;
				num7 = this.wantedBonusThree * item2;
				num6 = num6 + this.maximaizedBonusThree * (item2 - item1);
				string str26 = string.Format(StaticData.Translate("key_item_bonus_ionCooldown"), string.Format("{0} / {1}", num6 * 50, num7 * 50));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str26), "\n");
			}
			if (this.wantedBonusFour > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(12) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusFour * item1;
				num7 = this.wantedBonusFour * item2;
				num6 = num6 + this.maximaizedBonusFour * (item2 - item1);
				string str27 = string.Format(StaticData.Translate("key_item_bonus_laserDmg"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str27), "\n");
			}
			if (this.wantedBonusFive > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(13) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusFive * item1;
				num7 = this.wantedBonusFive * item2;
				num6 = num6 + this.maximaizedBonusFive * (item2 - item1);
				string str28 = string.Format(StaticData.Translate("key_item_bonus_plasmaDmg"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str28), "\n");
			}
		}
		else if (PlayerItems.IsExtraCargoMining(slotItem.get_ItemType()))
		{
			if (this.wantedBonusOne > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(5) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusOne * item1;
				num7 = this.wantedBonusOne * item2;
				num6 = num6 + this.maximaizedBonusOne * (item2 - item1);
				string str29 = string.Format(StaticData.Translate("key_item_bonus_targeting"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str29), "\n");
			}
			if (this.wantedBonusTwo > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(6) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusTwo * item1;
				num7 = this.wantedBonusTwo * item2;
				num6 = num6 + this.maximaizedBonusTwo * (item2 - item1);
				string str30 = string.Format(StaticData.Translate("key_item_bonus_avoidance"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str30), "\n");
			}
			if (this.wantedBonusThree > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(7) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusThree * item1;
				num7 = this.wantedBonusThree * item2;
				num6 = num6 + this.maximaizedBonusThree * (item2 - item1);
				string str31 = string.Format(StaticData.Translate("key_item_bonus_cargo"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str31), "\n");
			}
			if (this.wantedBonusFour > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(8) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusFour * item1;
				num7 = this.wantedBonusFour * item2;
				num6 = num6 + this.maximaizedBonusFour * (item2 - item1);
				string str32 = string.Format(StaticData.Translate("key_item_bonus_cargoPercent"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str32), "\n");
			}
			if (this.wantedBonusFive > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(15) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusFive * item1;
				num7 = this.wantedBonusFive * item2;
				num6 = num6 + this.maximaizedBonusFive * (item2 - item1);
				string str33 = string.Format(StaticData.Translate("key_item_bonus_miningSpeed"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str33), "\n");
			}
		}
		else if (PlayerItems.IsExtraDamage(slotItem.get_ItemType()))
		{
			if (this.wantedBonusOne > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(5) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusOne * item1;
				num7 = this.wantedBonusOne * item2;
				num6 = num6 + this.maximaizedBonusOne * (item2 - item1);
				string str34 = string.Format(StaticData.Translate("key_item_bonus_targeting"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str34), "\n");
			}
			if (this.wantedBonusTwo > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(6) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusTwo * item1;
				num7 = this.wantedBonusTwo * item2;
				num6 = num6 + this.maximaizedBonusTwo * (item2 - item1);
				string str35 = string.Format(StaticData.Translate("key_item_bonus_avoidance"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str35), "\n");
			}
			if (this.wantedBonusThree > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(12) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusThree * item1;
				num7 = this.wantedBonusThree * item2;
				num6 = num6 + this.maximaizedBonusThree * (item2 - item1);
				string str36 = string.Format(StaticData.Translate("key_item_bonus_laserDmg"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str36), "\n");
			}
			if (this.wantedBonusFour > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(13) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusFour * item1;
				num7 = this.wantedBonusFour * item2;
				num6 = num6 + this.maximaizedBonusFour * (item2 - item1);
				string str37 = string.Format(StaticData.Translate("key_item_bonus_plasmaDmg"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str37), "\n");
			}
			if (this.wantedBonusFive > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(14) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusFive * item1;
				num7 = this.wantedBonusFive * item2;
				num6 = num6 + this.maximaizedBonusFive * (item2 - item1);
				string str38 = string.Format(StaticData.Translate("key_item_bonus_ionDmg"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str38), "\n");
			}
		}
		else if (PlayerItems.IsExtraOther(slotItem.get_ItemType()))
		{
			if (this.wantedBonusOne > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(4) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusOne * item1;
				num7 = this.wantedBonusOne * item2;
				num6 = num6 + this.maximaizedBonusOne * (item2 - item1);
				string str39 = string.Format(StaticData.Translate("key_item_bonus_speed"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str39), "\n");
			}
			if (this.wantedBonusTwo > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(5) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusTwo * item1;
				num7 = this.wantedBonusTwo * item2;
				num6 = num6 + this.maximaizedBonusTwo * (item2 - item1);
				string str40 = string.Format(StaticData.Translate("key_item_bonus_targeting"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str40), "\n");
			}
			if (this.wantedBonusThree > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(6) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusThree * item1;
				num7 = this.wantedBonusThree * item2;
				num6 = num6 + this.maximaizedBonusThree * (item2 - item1);
				string str41 = string.Format(StaticData.Translate("key_item_bonus_avoidance"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str41), "\n");
			}
			if (this.wantedBonusFour > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(15) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusFour * item1;
				num7 = this.wantedBonusFour * item2;
				num6 = num6 + this.maximaizedBonusFour * (item2 - item1);
				string str42 = string.Format(StaticData.Translate("key_item_bonus_miningSpeed"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str42), "\n");
			}
			if (this.wantedBonusFive > 0)
			{
				single = BonusesConstant.bonusConstatn.get_Item(16) * (float)item.levelRestriction;
				item1 = (int)Math.Max(1f, single);
				item2 = (int)Math.Max(1f, single * 3f);
				num6 = this.wantedBonusFive * item1;
				num7 = this.wantedBonusFive * item2;
				num6 = num6 + this.maximaizedBonusFive * (item2 - item1);
				string str43 = string.Format(StaticData.Translate("key_item_bonus_shieldRegen"), string.Format("{0} / {1}", num6, num7));
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str43), "\n");
			}
		}
		GuiLabel guiLabel20 = new GuiLabel()
		{
			boundries = new Rect(55f, 498f, 220f, 67f),
			text = empty,
			Alignment = 3,
			TextColor = GuiNewStyleBar.greenColor,
			Font = GuiLabel.FontBold,
			FontSize = 10
		};
		base.AddGuiElement(guiLabel20);
		this.afterSectionForDelete.Add(guiLabel20);
	}

	private void CreateBeforeSection()
	{
		Color color;
		string str;
		__MerchantWindow.<CreateBeforeSection>c__AnonStoreyA1 variable = null;
		foreach (GuiElement guiElement in this.beforeSectionForDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.beforeSectionForDelete.Clear();
		int num = this.selectedSlotItem % 1000;
		int num1 = this.selectedSlotItem / 1000;
		SlotItem slotItem = Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(NetworkScript.player.playerBelongings.playerItems.slotItems, new Func<SlotItem, bool>(variable, (SlotItem t) => (t.get_Slot() != this.itemSlot ? false : t.get_SlotType() == (byte)this.itemSlotType))));
		if (slotItem == null)
		{
			return;
		}
		PlayerItemTypesData item = StaticData.allTypes.get_Item(slotItem.get_ItemType());
		Inventory.ItemRarity(slotItem, out str, out color);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(50f, 162f, 260f, 12f),
			text = StaticData.Translate("key_reroll_item_current_lbl"),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		base.AddGuiElement(guiLabel);
		this.beforeSectionForDelete.Add(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(48f, 180f, 265f, 20f),
			text = str,
			Alignment = 4,
			TextColor = color,
			Font = GuiLabel.FontBold,
			FontSize = 11
		};
		base.AddGuiElement(guiLabel1);
		this.beforeSectionForDelete.Add(guiLabel1);
		GuiTexture guiTexture = new GuiTexture()
		{
			boundries = new Rect(49f, 197f, 100f, 69f)
		};
		guiTexture.SetItemTextureKeepSize(slotItem.get_ItemType());
		base.AddGuiElement(guiTexture);
		this.beforeSectionForDelete.Add(guiTexture);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(55f, 267f, 255f, 16f),
			text = string.Format(StaticData.Translate("key_tooltip_level_restriction"), StaticData.allTypes.get_Item(slotItem.get_ItemType()).levelRestriction),
			Alignment = 3,
			TextColor = (StaticData.allTypes.get_Item(slotItem.get_ItemType()).levelRestriction <= NetworkScript.player.playerBelongings.playerLevel ? Color.get_white() : GuiNewStyleBar.redColor),
			Font = GuiLabel.FontBold,
			FontSize = 10
		};
		base.AddGuiElement(guiLabel2);
		this.beforeSectionForDelete.Add(guiLabel2);
		int num2 = 10;
		int num3 = 150;
		int num4 = 200;
		int num5 = 165;
		if (PlayerItems.IsWeapon(slotItem.get_ItemType()))
		{
			GuiLabel guiLabel3 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)num4, (float)num5, 12f),
				text = StaticData.Translate("key_inventory_damage"),
				FontSize = num2,
				Alignment = 3
			};
			base.AddGuiElement(guiLabel3);
			this.beforeSectionForDelete.Add(guiLabel3);
			GuiLabel guiLabel4 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 12), (float)num5, 12f),
				text = StaticData.Translate("key_inventory_cooldown"),
				FontSize = num2,
				Alignment = 3
			};
			base.AddGuiElement(guiLabel4);
			this.beforeSectionForDelete.Add(guiLabel4);
			GuiLabel guiLabel5 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 24), (float)num5, 12f),
				text = StaticData.Translate("key_inventory_range"),
				FontSize = num2,
				Alignment = 3
			};
			base.AddGuiElement(guiLabel5);
			this.beforeSectionForDelete.Add(guiLabel5);
			GuiLabel guiLabel6 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 36), (float)num5, 12f),
				text = StaticData.Translate("key_inventory_penetration"),
				FontSize = num2,
				Alignment = 3
			};
			base.AddGuiElement(guiLabel6);
			this.beforeSectionForDelete.Add(guiLabel6);
			GuiLabel guiLabel7 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 48), (float)num5, 12f),
				text = StaticData.Translate("key_inventory_targeting"),
				FontSize = num2,
				Alignment = 3
			};
			base.AddGuiElement(guiLabel7);
			this.beforeSectionForDelete.Add(guiLabel7);
			GuiLabel fontBold = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)num4, (float)num5, 12f)
			};
			int damageTotal = ((SlotItemWeapon)slotItem).get_DamageTotal();
			fontBold.text = damageTotal.ToString("##,##0");
			fontBold.FontSize = num2;
			fontBold.Font = GuiLabel.FontBold;
			fontBold.TextColor = (slotItem.get_BonusOne() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			fontBold.Alignment = 5;
			base.AddGuiElement(fontBold);
			this.beforeSectionForDelete.Add(fontBold);
			GuiLabel str1 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 12), (float)num5, 12f)
			};
			int cooldownTotal = ((SlotItemWeapon)slotItem).get_CooldownTotal();
			str1.text = cooldownTotal.ToString("##,##0");
			str1.FontSize = num2;
			str1.Font = GuiLabel.FontBold;
			str1.TextColor = (slotItem.get_BonusThree() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			str1.Alignment = 5;
			base.AddGuiElement(str1);
			this.beforeSectionForDelete.Add(str1);
			GuiLabel fontBold1 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 24), (float)num5, 12f)
			};
			int rangeTotal = ((SlotItemWeapon)slotItem).get_RangeTotal();
			fontBold1.text = rangeTotal.ToString("##,##0");
			fontBold1.FontSize = num2;
			fontBold1.Font = GuiLabel.FontBold;
			fontBold1.TextColor = (slotItem.get_BonusTwo() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			fontBold1.Alignment = 5;
			base.AddGuiElement(fontBold1);
			this.beforeSectionForDelete.Add(fontBold1);
			GuiLabel str2 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 36), (float)num5, 12f)
			};
			int penetrationTotal = ((SlotItemWeapon)slotItem).get_PenetrationTotal();
			str2.text = penetrationTotal.ToString("##,##0");
			str2.FontSize = num2;
			str2.Font = GuiLabel.FontBold;
			str2.TextColor = (slotItem.get_BonusFour() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			str2.Alignment = 5;
			base.AddGuiElement(str2);
			this.beforeSectionForDelete.Add(str2);
			GuiLabel fontBold2 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 48), (float)num5, 12f)
			};
			int targetingTotal = ((SlotItemWeapon)slotItem).get_TargetingTotal();
			fontBold2.text = targetingTotal.ToString("##,##0");
			fontBold2.FontSize = num2;
			fontBold2.Font = GuiLabel.FontBold;
			fontBold2.TextColor = (slotItem.get_BonusFive() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			fontBold2.Alignment = 5;
			base.AddGuiElement(fontBold2);
			this.beforeSectionForDelete.Add(fontBold2);
		}
		else if (PlayerItems.IsCorpus(slotItem.get_ItemType()))
		{
			GuiLabel guiLabel8 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 24), (float)num5, 12f),
				text = StaticData.Translate("key_inventory_corpus"),
				FontSize = num2,
				Alignment = 3
			};
			base.AddGuiElement(guiLabel8);
			this.beforeSectionForDelete.Add(guiLabel8);
			GuiLabel str3 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 24), (float)num5, 12f)
			};
			int bonusOne = ((GeneratorNet)item).bonusValue + slotItem.get_BonusOne();
			str3.text = bonusOne.ToString("##,##0");
			str3.FontSize = num2;
			str3.Font = GuiLabel.FontBold;
			str3.TextColor = (slotItem.get_BonusOne() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			str3.Alignment = 5;
			base.AddGuiElement(str3);
			this.beforeSectionForDelete.Add(str3);
		}
		else if (PlayerItems.IsShield(slotItem.get_ItemType()))
		{
			GuiLabel guiLabel9 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 24), (float)num5, 12f),
				text = StaticData.Translate("key_inventory_shield"),
				FontSize = num2,
				Alignment = 3
			};
			base.AddGuiElement(guiLabel9);
			this.beforeSectionForDelete.Add(guiLabel9);
			GuiLabel fontBold3 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 24), (float)num5, 12f)
			};
			int bonusOne1 = ((GeneratorNet)item).bonusValue + slotItem.get_BonusOne();
			fontBold3.text = bonusOne1.ToString("##,##0");
			fontBold3.FontSize = num2;
			fontBold3.Font = GuiLabel.FontBold;
			fontBold3.TextColor = (slotItem.get_BonusOne() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			fontBold3.Alignment = 5;
			base.AddGuiElement(fontBold3);
			this.beforeSectionForDelete.Add(fontBold3);
		}
		else if (PlayerItems.IsEngine(slotItem.get_ItemType()))
		{
			GuiLabel guiLabel10 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 24), (float)num5, 12f),
				text = StaticData.Translate("key_inventory_speed"),
				FontSize = num2,
				Alignment = 3
			};
			base.AddGuiElement(guiLabel10);
			this.beforeSectionForDelete.Add(guiLabel10);
			GuiLabel str4 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 24), (float)num5, 12f)
			};
			int bonusOne2 = ((GeneratorNet)item).bonusValue + slotItem.get_BonusOne();
			str4.text = bonusOne2.ToString("##,##0");
			str4.FontSize = num2;
			str4.Font = GuiLabel.FontBold;
			str4.TextColor = (slotItem.get_BonusOne() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			str4.Alignment = 5;
			base.AddGuiElement(str4);
			this.beforeSectionForDelete.Add(str4);
		}
		else if (PlayerItems.IsExtra(slotItem.get_ItemType()))
		{
			GuiLabel guiLabel11 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 12), (float)num5, 36f),
				FontSize = num2,
				Alignment = 3
			};
			base.AddGuiElement(guiLabel11);
			this.beforeSectionForDelete.Add(guiLabel11);
			GuiLabel guiLabel12 = new GuiLabel()
			{
				boundries = new Rect((float)num3, (float)(num4 + 12), (float)num5, 36f),
				text = string.Empty,
				Font = GuiLabel.FontBold,
				Alignment = 5
			};
			base.AddGuiElement(guiLabel12);
			this.beforeSectionForDelete.Add(guiLabel12);
			if (PlayerItems.IsForExtraCargoSpace(slotItem.get_ItemType()))
			{
				guiLabel11.text = StaticData.Translate("key_extra_cargo");
				guiLabel12.text = string.Format("+{0:##,##0}%", ((ExtrasNet)item).efValue + slotItem.get_BonusFour());
				guiLabel12.TextColor = (slotItem.get_BonusFour() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraLightMiningDrill || slotItem.get_ItemType() == PlayerItems.TypeExtraUltraMiningDrill)
			{
				guiLabel11.text = StaticData.Translate("key_extra_faster_mining");
				guiLabel12.text = string.Format("+{0:##,##0}%", ((ExtrasNet)item).efValue + slotItem.get_BonusFive());
				guiLabel12.TextColor = (slotItem.get_BonusFive() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			}
			else if (PlayerItems.IsForShieldRegen(slotItem.get_ItemType()))
			{
				guiLabel11.text = StaticData.Translate("key_extra_shield_regen");
				guiLabel12.text = string.Format("+{0:##,##0}", ((ExtrasNet)item).efValue + slotItem.get_BonusFive());
				guiLabel12.TextColor = (slotItem.get_BonusFive() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			}
			else if (PlayerItems.IsForLaserCooldown(slotItem.get_ItemType()))
			{
				guiLabel11.text = StaticData.Translate("key_extra_cooldown_laser");
				guiLabel12.text = string.Format("-{0:##,##0}", ((ExtrasNet)item).efValue + slotItem.get_BonusOne());
				guiLabel12.TextColor = (slotItem.get_BonusOne() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			}
			else if (PlayerItems.IsForPlasmaCooldown(slotItem.get_ItemType()))
			{
				guiLabel11.text = StaticData.Translate("key_extra_cooldown_plasma");
				guiLabel12.text = string.Format("-{0:##,##0}", ((ExtrasNet)item).efValue + slotItem.get_BonusTwo());
				guiLabel12.TextColor = (slotItem.get_BonusTwo() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			}
			else if (PlayerItems.IsForIonCooldown(slotItem.get_ItemType()))
			{
				guiLabel11.text = StaticData.Translate("key_extra_cooldown_ion");
				guiLabel12.text = string.Format("-{0:##,##0}", ((ExtrasNet)item).efValue + slotItem.get_BonusThree());
				guiLabel12.TextColor = (slotItem.get_BonusThree() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraUltraWeaponsCoolant)
			{
				guiLabel11.text = StaticData.Translate("key_extra_cooldown_all");
				guiLabel12.text = string.Format("-{0:##,##0}", ((ExtrasNet)item).efValue);
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraLaserWeaponsModule)
			{
				guiLabel11.text = StaticData.Translate("key_extra_dmg_laser");
				guiLabel12.text = string.Format("+{0:##,##0}%", ((ExtrasNet)item).efValue);
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraPlasmaWeaponsModule)
			{
				guiLabel11.text = StaticData.Translate("key_extra_dmg_plasma");
				guiLabel12.text = string.Format("+{0:##,##0}%", ((ExtrasNet)item).efValue);
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraIonWeaponsModule)
			{
				guiLabel11.text = StaticData.Translate("key_extra_dmg_ion");
				guiLabel12.text = string.Format("+{0:##,##0}%", ((ExtrasNet)item).efValue);
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraUltraWeaponsModule)
			{
				guiLabel11.text = StaticData.Translate("key_extra_dmg_all");
				guiLabel12.text = string.Format("+{0:##,##0}%", ((ExtrasNet)item).efValue);
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraLaserAimingCPU)
			{
				guiLabel11.text = StaticData.Translate("key_extra_targeting_laser");
				guiLabel12.text = string.Format("+{0:##,##0}", ((ExtrasNet)item).efValue);
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraPlasmaAimingCPU)
			{
				guiLabel11.text = StaticData.Translate("key_extra_targeting_plasma");
				guiLabel12.text = string.Format("+{0:##,##0}", ((ExtrasNet)item).efValue);
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraIonAimingCPU)
			{
				guiLabel11.text = StaticData.Translate("key_extra_targeting_ion");
				guiLabel12.text = string.Format("+{0:##,##0}", ((ExtrasNet)item).efValue);
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraUltraAimingCPU)
			{
				guiLabel11.text = StaticData.Translate("key_extra_targeting_all");
				guiLabel12.text = string.Format("+{0:##,##0}", ((ExtrasNet)item).efValue);
			}
			guiLabel11.boundries.set_width((float)num5 - guiLabel12.TextWidth - 5f);
		}
		string empty = string.Empty;
		if (PlayerItems.IsWeapon(slotItem.get_ItemType()))
		{
			if (slotItem.get_BonusCnt() > 0)
			{
				if (slotItem.get_BonusOne() != 0)
				{
					string str5 = string.Format("+{0} {1}", slotItem.get_BonusOne(), StaticData.Translate("key_inventory_damage"));
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str5), "\n");
				}
				if (slotItem.get_BonusTwo() != 0)
				{
					string str6 = string.Format("+{0} {1}", slotItem.get_BonusTwo(), StaticData.Translate("key_inventory_range"));
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str6), "\n");
				}
				if (slotItem.get_BonusThree() != 0)
				{
					string str7 = string.Format("-{0} {1}", slotItem.get_BonusThree(), StaticData.Translate("key_inventory_cooldown"));
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str7), "\n");
				}
				if (slotItem.get_BonusFour() != 0)
				{
					string str8 = string.Format("+{0} {1}", slotItem.get_BonusFour(), StaticData.Translate("key_inventory_penetration"));
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str8), "\n");
				}
				if (slotItem.get_BonusFive() != 0)
				{
					string str9 = string.Format("+{0} {1}", slotItem.get_BonusFive(), StaticData.Translate("key_inventory_targeting"));
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str9), "\n");
				}
			}
		}
		else if (PlayerItems.IsCorpus(slotItem.get_ItemType()))
		{
			if (slotItem.get_BonusCnt() > 0)
			{
				if (slotItem.get_BonusOne() != 0)
				{
					string str10 = string.Format(StaticData.Translate("key_item_bonus_corpus"), slotItem.get_BonusOne());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str10), "\n");
				}
				if (slotItem.get_BonusTwo() != 0)
				{
					string str11 = string.Format(StaticData.Translate("key_item_bonus_corpusPercent"), slotItem.get_BonusTwo());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str11), "\n");
				}
				if (slotItem.get_BonusThree() != 0)
				{
					string str12 = string.Format(StaticData.Translate("key_item_bonus_targeting"), slotItem.get_BonusThree());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str12), "\n");
				}
				if (slotItem.get_BonusFour() != 0)
				{
					string str13 = string.Format(StaticData.Translate("key_item_bonus_avoidance"), slotItem.get_BonusFour());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str13), "\n");
				}
				if (slotItem.get_BonusFive() != 0)
				{
					string str14 = string.Format(StaticData.Translate("key_item_bonus_shieldRegen"), slotItem.get_BonusFive());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str14), "\n");
				}
			}
		}
		else if (PlayerItems.IsShield(slotItem.get_ItemType()))
		{
			if (slotItem.get_BonusCnt() > 0)
			{
				if (slotItem.get_BonusOne() != 0)
				{
					string str15 = string.Format(StaticData.Translate("key_item_bonus_shield"), slotItem.get_BonusOne());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str15), "\n");
				}
				if (slotItem.get_BonusTwo() != 0)
				{
					string str16 = string.Format(StaticData.Translate("key_item_bonus_shieldPercent"), slotItem.get_BonusTwo());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str16), "\n");
				}
				if (slotItem.get_BonusThree() != 0)
				{
					string str17 = string.Format(StaticData.Translate("key_item_bonus_targeting"), slotItem.get_BonusThree());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str17), "\n");
				}
				if (slotItem.get_BonusFour() != 0)
				{
					string str18 = string.Format(StaticData.Translate("key_item_bonus_avoidance"), slotItem.get_BonusFour());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str18), "\n");
				}
				if (slotItem.get_BonusFive() != 0)
				{
					string str19 = string.Format(StaticData.Translate("key_item_bonus_shieldRegen"), slotItem.get_BonusFive());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str19), "\n");
				}
			}
		}
		else if (PlayerItems.IsEngine(slotItem.get_ItemType()))
		{
			if (slotItem.get_BonusCnt() > 0)
			{
				if (slotItem.get_BonusOne() != 0)
				{
					string str20 = string.Format(StaticData.Translate("key_item_bonus_speed"), slotItem.get_BonusOne());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str20), "\n");
				}
				if (slotItem.get_BonusTwo() != 0)
				{
					string str21 = string.Format(StaticData.Translate("key_item_bonus_avoidance"), slotItem.get_BonusTwo());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str21), "\n");
				}
				if (slotItem.get_BonusThree() != 0)
				{
					string str22 = string.Format(StaticData.Translate("key_item_bonus_cargo"), slotItem.get_BonusThree());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str22), "\n");
				}
				if (slotItem.get_BonusFour() != 0)
				{
					string str23 = string.Format(StaticData.Translate("key_item_bonus_cargoPercent"), slotItem.get_BonusFour());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str23), "\n");
				}
				if (slotItem.get_BonusFive() != 0)
				{
					string str24 = string.Format(StaticData.Translate("key_item_bonus_shieldRegen"), slotItem.get_BonusFive());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str24), "\n");
				}
			}
		}
		else if (PlayerItems.IsExtraCooldown(slotItem.get_ItemType()))
		{
			if (slotItem.get_BonusCnt() > 0)
			{
				if (slotItem.get_BonusOne() != 0)
				{
					string str25 = string.Format(StaticData.Translate("key_item_bonus_laserCooldown"), slotItem.get_BonusOne());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str25), "\n");
				}
				if (slotItem.get_BonusTwo() != 0)
				{
					string str26 = string.Format(StaticData.Translate("key_item_bonus_plasmaCooldown"), slotItem.get_BonusTwo());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str26), "\n");
				}
				if (slotItem.get_BonusThree() != 0)
				{
					string str27 = string.Format(StaticData.Translate("key_item_bonus_ionCooldown"), slotItem.get_BonusThree());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str27), "\n");
				}
				if (slotItem.get_BonusFour() != 0)
				{
					string str28 = string.Format(StaticData.Translate("key_item_bonus_laserDmg"), slotItem.get_BonusFour());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str28), "\n");
				}
				if (slotItem.get_BonusFive() != 0)
				{
					string str29 = string.Format(StaticData.Translate("key_item_bonus_plasmaDmg"), slotItem.get_BonusFive());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str29), "\n");
				}
			}
		}
		else if (PlayerItems.IsExtraCargoMining(slotItem.get_ItemType()))
		{
			if (slotItem.get_BonusCnt() > 0)
			{
				if (slotItem.get_BonusOne() != 0)
				{
					string str30 = string.Format(StaticData.Translate("key_item_bonus_targeting"), slotItem.get_BonusOne());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str30), "\n");
				}
				if (slotItem.get_BonusTwo() != 0)
				{
					string str31 = string.Format(StaticData.Translate("key_item_bonus_avoidance"), slotItem.get_BonusTwo());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str31), "\n");
				}
				if (slotItem.get_BonusThree() != 0)
				{
					string str32 = string.Format(StaticData.Translate("key_item_bonus_cargo"), slotItem.get_BonusThree());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str32), "\n");
				}
				if (slotItem.get_BonusFour() != 0)
				{
					string str33 = string.Format(StaticData.Translate("key_item_bonus_cargoPercent"), slotItem.get_BonusFour());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str33), "\n");
				}
				if (slotItem.get_BonusFive() != 0)
				{
					string str34 = string.Format(StaticData.Translate("key_item_bonus_miningSpeed"), slotItem.get_BonusFive());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str34), "\n");
				}
			}
		}
		else if (PlayerItems.IsExtraDamage(slotItem.get_ItemType()))
		{
			if (slotItem.get_BonusCnt() > 0)
			{
				if (slotItem.get_BonusOne() != 0)
				{
					string str35 = string.Format(StaticData.Translate("key_item_bonus_targeting"), slotItem.get_BonusOne());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str35), "\n");
				}
				if (slotItem.get_BonusTwo() != 0)
				{
					string str36 = string.Format(StaticData.Translate("key_item_bonus_avoidance"), slotItem.get_BonusTwo());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str36), "\n");
				}
				if (slotItem.get_BonusThree() != 0)
				{
					string str37 = string.Format(StaticData.Translate("key_item_bonus_laserDmg"), slotItem.get_BonusThree());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str37), "\n");
				}
				if (slotItem.get_BonusFour() != 0)
				{
					string str38 = string.Format(StaticData.Translate("key_item_bonus_plasmaDmg"), slotItem.get_BonusFour());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str38), "\n");
				}
				if (slotItem.get_BonusFive() != 0)
				{
					string str39 = string.Format(StaticData.Translate("key_item_bonus_ionDmg"), slotItem.get_BonusFive());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str39), "\n");
				}
			}
		}
		else if (PlayerItems.IsExtraOther(slotItem.get_ItemType()) && slotItem.get_BonusCnt() > 0)
		{
			if (slotItem.get_BonusOne() != 0)
			{
				string str40 = string.Format(StaticData.Translate("key_item_bonus_speed"), slotItem.get_BonusOne());
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str40), "\n");
			}
			if (slotItem.get_BonusTwo() != 0)
			{
				string str41 = string.Format(StaticData.Translate("key_item_bonus_targeting"), slotItem.get_BonusTwo());
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str41), "\n");
			}
			if (slotItem.get_BonusThree() != 0)
			{
				string str42 = string.Format(StaticData.Translate("key_item_bonus_avoidance"), slotItem.get_BonusThree());
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str42), "\n");
			}
			if (slotItem.get_BonusFour() != 0)
			{
				string str43 = string.Format(StaticData.Translate("key_item_bonus_miningSpeed"), slotItem.get_BonusFour());
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str43), "\n");
			}
			if (slotItem.get_BonusFive() != 0)
			{
				string str44 = string.Format(StaticData.Translate("key_item_bonus_shieldRegen"), slotItem.get_BonusFive());
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str44), "\n");
			}
		}
		GuiLabel guiLabel13 = new GuiLabel()
		{
			boundries = new Rect(55f, 290f, 240f, 75f),
			text = empty,
			Alignment = 3,
			TextColor = GuiNewStyleBar.greenColor,
			Font = GuiLabel.FontBold,
			FontSize = 10
		};
		base.AddGuiElement(guiLabel13);
		this.beforeSectionForDelete.Add(guiLabel13);
	}

	private void CreateExtrasTab()
	{
		__MerchantWindow.<CreateExtrasTab>c__AnonStorey9B variable = null;
		int num = (!NetworkScript.player.playerBelongings.haveExtendetExtraOne ? 6 : 7);
		if (num == 7 && NetworkScript.player.playerBelongings.haveExtendetExtraTwo)
		{
			num = 8;
		}
		IEnumerable<SlotItem> enumerable = Enumerable.Where<SlotItem>(NetworkScript.player.playerBelongings.playerItems.slotItems, new Func<SlotItem, bool>(this, (SlotItem it) => (it.get_SlotType() != 13 || !PlayerItems.IsExtra(it.get_ItemType()) ? false : it.get_ShipId() == this.onDisplayShipId)));
		for (int i = 0; i < num; i++)
		{
			AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = new AndromedaGuiDragDropPlace();
			SlotItem slotItem = Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable, new Func<SlotItem, bool>(variable, (SlotItem it) => it.get_Slot() == this.i)));
			andromedaGuiDragDropPlace.location = 13;
			andromedaGuiDragDropPlace.position = new Vector2((float)(638 + i / 2 * 46), (float)(256 + i % 2 * 42));
			andromedaGuiDragDropPlace.slot = (byte)i;
			andromedaGuiDragDropPlace.dropZonePosition = andromedaGuiDragDropPlace.position;
			andromedaGuiDragDropPlace.txFrameIdle = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "MerchantSlotIdle");
			andromedaGuiDragDropPlace.txFrameDragSource = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "MerchantSlotHvr");
			andromedaGuiDragDropPlace.txFrameDropTarget = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "MerchantSlotActive");
			andromedaGuiDragDropPlace.txFrameDropTargetHover = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "MerchantSlotHvr");
			andromedaGuiDragDropPlace.glowTexturePrefix = "MerchantSlot";
			andromedaGuiDragDropPlace.item = slotItem;
			andromedaGuiDragDropPlace.isEmpty = slotItem == null;
			andromedaGuiDragDropPlace.shipId = this.onDisplayShipId;
			andromedaGuiDragDropPlace.idleItemTextureSize = new Vector2(36f, 25f);
			Inventory.places.Add(andromedaGuiDragDropPlace);
		}
	}

	private void CreateInventorySlots()
	{
		__MerchantWindow.<CreateInventorySlots>c__AnonStorey94 variable = null;
		IEnumerable<SlotItem> enumerable = null;
		__MerchantWindow.MerchantInventoryTab merchantInventoryTab = this.selectedInventoryTab;
		if (merchantInventoryTab == __MerchantWindow.MerchantInventoryTab.Inventory)
		{
			List<SlotItem> list = NetworkScript.player.playerBelongings.playerItems.slotItems;
			if (__MerchantWindow.<>f__am$cache67 == null)
			{
				__MerchantWindow.<>f__am$cache67 = new Func<SlotItem, bool>(null, (SlotItem t) => t.get_SlotType() == 1);
			}
			enumerable = Enumerable.Where<SlotItem>(list, __MerchantWindow.<>f__am$cache67);
		}
		else if (merchantInventoryTab == __MerchantWindow.MerchantInventoryTab.Cargo)
		{
			List<SlotItem> list1 = NetworkScript.player.playerBelongings.playerItems.slotItems;
			if (__MerchantWindow.<>f__am$cache68 == null)
			{
				__MerchantWindow.<>f__am$cache68 = new Func<SlotItem, bool>(null, (SlotItem t) => t.get_SlotType() == 3);
			}
			enumerable = Enumerable.Where<SlotItem>(list1, __MerchantWindow.<>f__am$cache68);
		}
		int num = 0;
		int num1 = 0;
		merchantInventoryTab = this.selectedInventoryTab;
		if (merchantInventoryTab == __MerchantWindow.MerchantInventoryTab.Inventory)
		{
			num = 0;
			num1 = this.inventorySlotCnt;
		}
		else if (merchantInventoryTab == __MerchantWindow.MerchantInventoryTab.Cargo)
		{
			num = 0;
			num1 = 8;
		}
		foreach (GuiElement lockedInventorySlot in this.lockedInventorySlots)
		{
			base.RemoveGuiElement(lockedInventorySlot);
		}
		this.lockedInventorySlots.Clear();
		int num2 = num;
		int num3 = 0;
		while (num2 < num1)
		{
			if (num3 < 40)
			{
				AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = new AndromedaGuiDragDropPlace()
				{
					slot = (byte)num2
				};
				Inventory.places.Add(andromedaGuiDragDropPlace);
				andromedaGuiDragDropPlace.txFrameIdle = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotIdle");
				andromedaGuiDragDropPlace.position = new Vector2((float)(375 + num3 / 4 * 58), (float)(373 + num3 % 4 * 52));
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
				merchantInventoryTab = this.selectedInventoryTab;
				if (merchantInventoryTab == __MerchantWindow.MerchantInventoryTab.Inventory)
				{
					andromedaGuiDragDropPlace.location = 1;
				}
				else if (merchantInventoryTab == __MerchantWindow.MerchantInventoryTab.Cargo)
				{
					andromedaGuiDragDropPlace.location = 3;
				}
				num2++;
				num3++;
			}
			else
			{
				break;
			}
		}
		if (num1 - num < 40 && this.selectedInventoryTab == __MerchantWindow.MerchantInventoryTab.Inventory)
		{
			int num4 = num1 - num;
			this.DrawExpandSlots((float)(375 + num4 / 4 * 58), (float)(370 + num4 % 4 * 52));
		}
	}

	private void CreateItemRerollDialogue(EventHandlerParam prm)
	{
		__MerchantWindow.<CreateItemRerollDialogue>c__AnonStorey9F variable = null;
		int num = this.selectedSlotItem % 1000;
		int num1 = this.selectedSlotItem / 1000;
		SlotItem slotItem = Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(NetworkScript.player.playerBelongings.playerItems.slotItems, new Func<SlotItem, bool>(variable, (SlotItem t) => (t.get_Slot() != this.itemSlot ? false : t.get_SlotType() == (byte)this.itemSlotType))));
		if (slotItem != null && slotItem.isAccountBound)
		{
			this.OnItemRerollBtnClicked(prm);
			return;
		}
		this.dialogWindow = new GuiWindow()
		{
			isModal = true
		};
		this.dialogWindow.SetBackgroundTexture("FrameworkGUI", "menugui_dialog");
		this.dialogWindow.isHidden = false;
		this.dialogWindow.zOrder = 220;
		this.dialogWindow.PutToCenter();
		AndromedaGui.gui.AddWindow(this.dialogWindow);
		AndromedaGui.gui.activeToolTipId = this.dialogWindow.handler;
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(this.dialogWindow.boundries.get_width() * 0.1f, 60f, this.dialogWindow.boundries.get_width() * 0.8f, 70f),
			text = StaticData.Translate("key_item_reroll_proceed_question"),
			Alignment = 1,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 18
		};
		this.dialogWindow.AddGuiElement(guiLabel);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable()
		{
			Width = 100f,
			Caption = StaticData.Translate("key_dock_my_ships_select_ship_yes").ToUpper(),
			FontSize = 12,
			Alignment = 4,
			X = (this.dialogWindow.boundries.get_width() - 240f) / 2f + 10f,
			Y = 140f,
			eventHandlerParam = prm,
			Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.OnItemRerollBtnClicked)
		};
		this.dialogWindow.AddGuiElement(guiButtonResizeable);
		GuiButtonResizeable upper = new GuiButtonResizeable();
		upper.SetBlueTexture();
		upper.Width = 100f;
		upper.Caption = StaticData.Translate("key_dock_my_ships_select_ship_no").ToUpper();
		upper.FontSize = 12;
		upper.Alignment = 4;
		upper.X = guiButtonResizeable.X + guiButtonResizeable.boundries.get_width() + 20f;
		upper.Y = guiButtonResizeable.Y;
		upper.SetLeftClickSound("FrameworkGUI", "cancel");
		upper.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.CancelReroll);
		this.dialogWindow.AddGuiElement(upper);
	}

	private void CreateItemRerollMenu()
	{
		this.selectedSlotItem = -1;
		Inventory.isItemRerollMenuOpen = true;
		Inventory.isGuildVaultMenuOpen = false;
		Inventory.activateItemReroll = new Action<int>(this, __MerchantWindow.OnItemRerollZoneDrop);
		this.DrawInventoryTabs();
		this.CreateBeforeSection();
		this.CreateAfterSection();
		this.CreateRerollSection();
		GuiTexture guiTexture = new GuiTexture()
		{
			boundries = new Rect(46f, 159f, 287f, 200f)
		};
		guiTexture.SetTextureKeepSize("FrameworkGUI", "empty");
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_reroll_current_tooltip"),
			customData2 = guiTexture
		};
		guiTexture.tooltipWindowParam = eventHandlerParam;
		guiTexture.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		GuiTexture drawTooltipWindow = new GuiTexture()
		{
			boundries = new Rect(46f, 369f, 287f, 200f)
		};
		drawTooltipWindow.SetTextureKeepSize("FrameworkGUI", "empty");
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_reroll_new_tooltip"),
			customData2 = drawTooltipWindow
		};
		drawTooltipWindow.tooltipWindowParam = eventHandlerParam;
		drawTooltipWindow.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(drawTooltipWindow);
		this.forDelete.Add(drawTooltipWindow);
		this.rerollDropZone = new GuiTexture()
		{
			boundries = new Rect(359f, 72f, 220f, 220f)
		};
		this.rerollDropZone.SetTextureKeepSize("FrameworkGUI", "empty");
		GuiTexture guiTexture1 = this.rerollDropZone;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_reroll_box_empty_tooltip"),
			customData2 = this.rerollDropZone
		};
		guiTexture1.tooltipWindowParam = eventHandlerParam;
		this.rerollDropZone.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(this.rerollDropZone);
		this.forDelete.Add(this.rerollDropZone);
		if (this.itemRerolEmptyBoxInfo != null)
		{
			base.RemoveGuiElement(this.itemRerolEmptyBoxInfo);
			this.itemRerolEmptyBoxInfo = null;
		}
		this.itemRerolEmptyBoxInfo = new GuiLabel()
		{
			boundries = new Rect(377f, 92f, 182f, 182f),
			Font = GuiLabel.FontBold,
			FontSize = 14,
			TextColor = GuiNewStyleBar.orangeColor,
			text = StaticData.Translate("key_reroll_item_box_empty"),
			Alignment = 4
		};
		base.AddGuiElement(this.itemRerolEmptyBoxInfo);
	}

	private void CreateMerchantSlots()
	{
		__MerchantWindow.<CreateMerchantSlots>c__AnonStorey93 variable = null;
		List<SlotItem> list = new List<SlotItem>();
		byte num = 2;
		if (this.selectedOption == __MerchantWindow.MenuOption.Merchant)
		{
			num = 0;
		}
		else if (this.selectedOption == __MerchantWindow.MenuOption.Gambler)
		{
			num = 1;
		}
		LevelMap[] levelMapArray = StaticData.allGalaxies;
		if (__MerchantWindow.<>f__am$cache5F == null)
		{
			__MerchantWindow.<>f__am$cache5F = new Func<LevelMap, bool>(null, (LevelMap g) => g.get_galaxyId() == NetworkScript.player.vessel.galaxy.get_galaxyId());
		}
		StarBaseNet[] starBaseNetArray = Enumerable.First<LevelMap>(Enumerable.Where<LevelMap>(levelMapArray, __MerchantWindow.<>f__am$cache5F)).starBases;
		if (__MerchantWindow.<>f__am$cache60 == null)
		{
			__MerchantWindow.<>f__am$cache60 = new Func<StarBaseNet, bool>(null, (StarBaseNet b) => b.id == NetworkScript.player.vessel.lastVisitedBase);
		}
		StarBaseNet starBaseNet = Enumerable.First<StarBaseNet>(Enumerable.Where<StarBaseNet>(starBaseNetArray, __MerchantWindow.<>f__am$cache60));
		short _galaxyId = starBaseNet.starBaseKey;
		if (starBaseNet.starBaseKey == 40)
		{
			_galaxyId = (short)(400 + NetworkScript.player.vessel.galaxy.get_galaxyId() % 100 / 10);
		}
		switch (this.selectedMerchantTab)
		{
			case __MerchantWindow.MerchantTab.Shields:
			{
				List<SlotItem> list1 = new List<SlotItem>();
				IEnumerable<StarBaseItemDistribution> enumerable = Enumerable.Where<StarBaseItemDistribution>(StaticData.starBaseItemsDisribution, new Func<StarBaseItemDistribution, bool>(variable, (StarBaseItemDistribution t) => (t.starBaseKey != this.starBaseKey || !PlayerItems.IsShield(t.itemType) ? false : (t.presentState == this.presentState ? true : t.presentState == 2))));
				if (__MerchantWindow.<>f__am$cache61 == null)
				{
					__MerchantWindow.<>f__am$cache61 = new Func<StarBaseItemDistribution, ushort>(null, (StarBaseItemDistribution s) => s.itemType);
				}
				IEnumerator<ushort> enumerator = Enumerable.Select<StarBaseItemDistribution, ushort>(enumerable, __MerchantWindow.<>f__am$cache61).GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						ushort current = enumerator.get_Current();
						SlotItem slotItem = new SlotItem();
						slotItem.set_ItemType(current);
						slotItem.set_Amount(1);
						list1.Add(slotItem);
					}
				}
				finally
				{
					if (enumerator == null)
					{
					}
					enumerator.Dispose();
				}
				list1 = this.OrderSlotItemsByPrice(list1);
				list.AddRange(list1);
				break;
			}
			case __MerchantWindow.MerchantTab.Corpuses:
			{
				List<SlotItem> list2 = new List<SlotItem>();
				IEnumerable<StarBaseItemDistribution> enumerable1 = Enumerable.Where<StarBaseItemDistribution>(StaticData.starBaseItemsDisribution, new Func<StarBaseItemDistribution, bool>(variable, (StarBaseItemDistribution t) => (t.starBaseKey != this.starBaseKey || !PlayerItems.IsCorpus(t.itemType) ? false : (t.presentState == this.presentState ? true : t.presentState == 2))));
				if (__MerchantWindow.<>f__am$cache62 == null)
				{
					__MerchantWindow.<>f__am$cache62 = new Func<StarBaseItemDistribution, ushort>(null, (StarBaseItemDistribution s) => s.itemType);
				}
				IEnumerator<ushort> enumerator1 = Enumerable.Select<StarBaseItemDistribution, ushort>(enumerable1, __MerchantWindow.<>f__am$cache62).GetEnumerator();
				try
				{
					while (enumerator1.MoveNext())
					{
						ushort current1 = enumerator1.get_Current();
						SlotItem slotItem1 = new SlotItem();
						slotItem1.set_ItemType(current1);
						slotItem1.set_Amount(1);
						list2.Add(slotItem1);
					}
				}
				finally
				{
					if (enumerator1 == null)
					{
					}
					enumerator1.Dispose();
				}
				list2 = this.OrderSlotItemsByPrice(list2);
				list.AddRange(list2);
				break;
			}
			case __MerchantWindow.MerchantTab.Engines:
			{
				List<SlotItem> list3 = new List<SlotItem>();
				IEnumerable<StarBaseItemDistribution> enumerable2 = Enumerable.Where<StarBaseItemDistribution>(StaticData.starBaseItemsDisribution, new Func<StarBaseItemDistribution, bool>(variable, (StarBaseItemDistribution t) => (t.starBaseKey != this.starBaseKey || !PlayerItems.IsEngine(t.itemType) ? false : (t.presentState == this.presentState ? true : t.presentState == 2))));
				if (__MerchantWindow.<>f__am$cache63 == null)
				{
					__MerchantWindow.<>f__am$cache63 = new Func<StarBaseItemDistribution, ushort>(null, (StarBaseItemDistribution s) => s.itemType);
				}
				IEnumerator<ushort> enumerator2 = Enumerable.Select<StarBaseItemDistribution, ushort>(enumerable2, __MerchantWindow.<>f__am$cache63).GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						ushort num1 = enumerator2.get_Current();
						SlotItem slotItem2 = new SlotItem();
						slotItem2.set_ItemType(num1);
						slotItem2.set_Amount(1);
						list3.Add(slotItem2);
					}
				}
				finally
				{
					if (enumerator2 == null)
					{
					}
					enumerator2.Dispose();
				}
				list3 = this.OrderSlotItemsByPrice(list3);
				list.AddRange(list3);
				break;
			}
			case __MerchantWindow.MerchantTab.Extras:
			{
				IEnumerable<StarBaseItemDistribution> enumerable3 = Enumerable.Where<StarBaseItemDistribution>(StaticData.starBaseItemsDisribution, new Func<StarBaseItemDistribution, bool>(variable, (StarBaseItemDistribution t) => (t.starBaseKey != this.starBaseKey || !PlayerItems.IsExtra(t.itemType) ? false : (t.presentState == this.presentState ? true : t.presentState == 2))));
				if (__MerchantWindow.<>f__am$cache64 == null)
				{
					__MerchantWindow.<>f__am$cache64 = new Func<StarBaseItemDistribution, ushort>(null, (StarBaseItemDistribution s) => s.itemType);
				}
				IEnumerator<ushort> enumerator3 = Enumerable.Select<StarBaseItemDistribution, ushort>(enumerable3, __MerchantWindow.<>f__am$cache64).GetEnumerator();
				try
				{
					while (enumerator3.MoveNext())
					{
						ushort current2 = enumerator3.get_Current();
						SlotItem slotItem3 = new SlotItem();
						slotItem3.set_ItemType(current2);
						slotItem3.set_Amount(1);
						list.Add(slotItem3);
					}
				}
				finally
				{
					if (enumerator3 == null)
					{
					}
					enumerator3.Dispose();
				}
				list = this.OrderSlotItemsByPrice(list);
				break;
			}
			case __MerchantWindow.MerchantTab.Ammo:
			{
				int[] numArray = new int[] { 100, 200, 500, 1000, 5000 };
				IEnumerable<StarBaseItemDistribution> enumerable4 = Enumerable.Where<StarBaseItemDistribution>(StaticData.starBaseItemsDisribution, new Func<StarBaseItemDistribution, bool>(variable, (StarBaseItemDistribution t) => (t.starBaseKey != this.starBaseKey || !PlayerItems.IsAmmo(t.itemType) ? false : (t.presentState == this.presentState ? true : t.presentState == 2))));
				if (__MerchantWindow.<>f__am$cache65 == null)
				{
					__MerchantWindow.<>f__am$cache65 = new Func<StarBaseItemDistribution, ushort>(null, (StarBaseItemDistribution s) => s.itemType);
				}
				IEnumerator<ushort> enumerator4 = Enumerable.Select<StarBaseItemDistribution, ushort>(enumerable4, __MerchantWindow.<>f__am$cache65).GetEnumerator();
				try
				{
					while (enumerator4.MoveNext())
					{
						ushort num2 = enumerator4.get_Current();
						int[] numArray1 = numArray;
						for (int i = 0; i < (int)numArray1.Length; i++)
						{
							int num3 = numArray1[i];
							SlotItem slotItem4 = new SlotItem();
							slotItem4.set_ItemType(num2);
							slotItem4.set_Amount(num3);
							list.Add(slotItem4);
						}
					}
				}
				finally
				{
					if (enumerator4 == null)
					{
					}
					enumerator4.Dispose();
				}
				break;
			}
			case __MerchantWindow.MerchantTab.Weapons:
			{
				IEnumerable<StarBaseItemDistribution> enumerable5 = Enumerable.Where<StarBaseItemDistribution>(StaticData.starBaseItemsDisribution, new Func<StarBaseItemDistribution, bool>(variable, (StarBaseItemDistribution t) => (t.starBaseKey != this.starBaseKey || !PlayerItems.IsWeapon(t.itemType) ? false : (t.presentState == this.presentState ? true : t.presentState == 2))));
				if (__MerchantWindow.<>f__am$cache66 == null)
				{
					__MerchantWindow.<>f__am$cache66 = new Func<StarBaseItemDistribution, ushort>(null, (StarBaseItemDistribution s) => s.itemType);
				}
				IEnumerator<ushort> enumerator5 = Enumerable.Select<StarBaseItemDistribution, ushort>(enumerable5, __MerchantWindow.<>f__am$cache66).GetEnumerator();
				try
				{
					while (enumerator5.MoveNext())
					{
						ushort current3 = enumerator5.get_Current();
						SlotItem slotItemWeapon = new SlotItemWeapon();
						slotItemWeapon.set_ItemType(current3);
						slotItemWeapon.set_Amount(1);
						list.Add(slotItemWeapon);
					}
				}
				finally
				{
					if (enumerator5 == null)
					{
					}
					enumerator5.Dispose();
				}
				list = this.OrderSlotItemsByPrice(list);
				break;
			}
		}
		ItemLocation itemLocation = 15;
		if (this.selectedOption == __MerchantWindow.MenuOption.Merchant)
		{
			itemLocation = 15;
			SlotItem[] array = list.ToArray();
			for (int j = 0; j < (int)array.Length; j++)
			{
				SlotItem slotItem5 = array[j];
				if (StaticData.allTypes.get_Item(slotItem5.get_ItemType()).priceCash <= 0 && !PlayerItems.IsAmmo(slotItem5.get_ItemType()))
				{
					list.Remove(slotItem5);
				}
			}
		}
		else if (this.selectedOption == __MerchantWindow.MenuOption.Gambler)
		{
			itemLocation = (!this.isQualityGambleOn ? 16 : 17);
			SlotItem[] slotItemArray = list.ToArray();
			for (int k = 0; k < (int)slotItemArray.Length; k++)
			{
				SlotItem slotItem6 = slotItemArray[k];
				if (StaticData.allTypes.get_Item(slotItem6.get_ItemType()).priceNova <= 0 || PlayerItems.IsAmmo(slotItem6.get_ItemType()))
				{
					list.Remove(slotItem6);
				}
			}
		}
		AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = new AndromedaGuiDragDropPlace();
		Inventory.places.Add(andromedaGuiDragDropPlace);
		andromedaGuiDragDropPlace.txFrameIdle = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "merchantDropZone");
		andromedaGuiDragDropPlace.position = new Vector2(40f, 139f);
		andromedaGuiDragDropPlace.dropZonePosition = andromedaGuiDragDropPlace.position;
		andromedaGuiDragDropPlace.isEmpty = true;
		andromedaGuiDragDropPlace.item = null;
		andromedaGuiDragDropPlace.txFrameDragSource = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "merchantDropZone");
		andromedaGuiDragDropPlace.txFrameDropTarget = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "merchantDropZone");
		andromedaGuiDragDropPlace.txFrameDropTargetHover = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "merchantDropZone");
		andromedaGuiDragDropPlace.idleItemTextureSize = new Vector2(305f, 415f);
		andromedaGuiDragDropPlace.location = 15;
		int num4 = 0;
		int num5 = 0;
		while (num4 < 30)
		{
			if (num5 < 40)
			{
				AndromedaGuiDragDropPlace fromStaticSet = new AndromedaGuiDragDropPlace()
				{
					slot = (byte)num4
				};
				Inventory.places.Add(fromStaticSet);
				fromStaticSet.txFrameIdle = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotIdle");
				fromStaticSet.position = new Vector2((float)(50 + num5 % 5 * 58), this.tabBtnOffsetY + 45f + (float)(num5 / 5 * 57));
				fromStaticSet.dropZonePosition = fromStaticSet.position;
				SlotItem item = null;
				if (num5 < list.get_Count())
				{
					item = list.get_Item(num4);
				}
				fromStaticSet.isEmpty = item == null;
				fromStaticSet.item = item;
				fromStaticSet.txFrameDragSource = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotHvr");
				fromStaticSet.txFrameDropTarget = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotActive");
				fromStaticSet.txFrameDropTargetHover = (Texture2D)playWebGame.assets.GetFromStaticSet("ConfigWindow", "InventorySlotHvr");
				fromStaticSet.idleItemTextureSize = new Vector2(51f, 35f);
				fromStaticSet.location = itemLocation;
				num4++;
				num5++;
			}
			else
			{
				break;
			}
		}
	}

	private void CreateRerollSection()
	{
		Color color;
		string str;
		__MerchantWindow.<CreateRerollSection>c__AnonStorey9C variable = null;
		foreach (GuiElement guiElement in this.rerollSectionForDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.rerollSectionForDelete.Clear();
		int num = this.selectedSlotItem % 1000;
		int num1 = this.selectedSlotItem / 1000;
		SlotItem slotItem = Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(NetworkScript.player.playerBelongings.playerItems.slotItems, new Func<SlotItem, bool>(variable, (SlotItem t) => (t.get_Slot() != this.itemSlot ? false : t.get_SlotType() == (byte)this.itemSlotType))));
		if (slotItem == null)
		{
			return;
		}
		Inventory.ItemRarity(slotItem, out str, out color);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(376f, 89f, 186f, 22f),
			text = str,
			Alignment = 4,
			TextColor = color,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		base.AddGuiElement(guiLabel);
		this.rerollSectionForDelete.Add(guiLabel);
		GuiTexture guiTexture = new GuiTexture()
		{
			boundries = new Rect(378f, 120f, 182f, 125f)
		};
		guiTexture.SetItemTextureKeepSize(slotItem.get_ItemType());
		base.AddGuiElement(guiTexture);
		this.rerollSectionForDelete.Add(guiTexture);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(615f, 59f, 120f, 30f),
			text = StaticData.Translate("key_reroll_bonustype"),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		base.AddGuiElement(guiLabel1);
		this.rerollSectionForDelete.Add(guiLabel1);
		for (int i = slotItem.get_BonusCnt(); i > 0; i--)
		{
			this.CrateBonusDropDown(slotItem, (byte)i);
		}
		this.PopulateBonusValue();
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(784f, 90f, 50f, 16f),
			text = StaticData.Translate("key_reroll_min"),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 10
		};
		base.AddGuiElement(guiLabel2);
		this.rerollSectionForDelete.Add(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(839f, 90f, 50f, 16f),
			text = StaticData.Translate("key_reroll_max"),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 10
		};
		base.AddGuiElement(guiLabel3);
		this.rerollSectionForDelete.Add(guiLabel3);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			boundries = new Rect(894f, 90f, 80f, 16f),
			text = StaticData.Translate("key_reroll_maximize"),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 10
		};
		base.AddGuiElement(guiLabel4);
		this.rerollSectionForDelete.Add(guiLabel4);
	}

	private void CreateSelecktShipDialogue(int selectedShipId)
	{
		this.dialogWindow = new GuiWindow()
		{
			isModal = true
		};
		this.dialogWindow.SetBackgroundTexture("FrameworkGUI", "menugui_dialog");
		this.dialogWindow.isHidden = false;
		this.dialogWindow.zOrder = 220;
		this.dialogWindow.PutToCenter();
		AndromedaGui.gui.AddWindow(this.dialogWindow);
		AndromedaGui.gui.activeToolTipId = this.dialogWindow.handler;
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(this.dialogWindow.boundries.get_width() * 0.1f, 60f, this.dialogWindow.boundries.get_width() * 0.8f, 70f),
			text = StaticData.Translate("key_dock_my_ships_select_ship_question"),
			Alignment = 1,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 18
		};
		this.dialogWindow.AddGuiElement(guiLabel);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable()
		{
			Width = 100f,
			Caption = StaticData.Translate("key_dock_my_ships_select_ship_yes").ToUpper(),
			FontSize = 12,
			Alignment = 4,
			X = (this.dialogWindow.boundries.get_width() - 240f) / 2f + 10f,
			Y = 140f
		};
		guiButtonResizeable.eventHandlerParam.customData = selectedShipId;
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.SelectShipWithEquip);
		this.dialogWindow.AddGuiElement(guiButtonResizeable);
		GuiButtonResizeable upper = new GuiButtonResizeable();
		upper.SetBlueTexture();
		upper.Width = 100f;
		upper.Caption = StaticData.Translate("key_dock_my_ships_select_ship_no").ToUpper();
		upper.FontSize = 12;
		upper.Alignment = 4;
		upper.X = guiButtonResizeable.X + guiButtonResizeable.boundries.get_width() + 20f;
		upper.Y = guiButtonResizeable.Y;
		upper.SetLeftClickSound("FrameworkGUI", "cancel");
		upper.eventHandlerParam.customData = selectedShipId;
		upper.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.SelectShipWithoutEquip);
		this.dialogWindow.AddGuiElement(upper);
	}

	private void CreateStructureTab()
	{
		__MerchantWindow.<CreateStructureTab>c__AnonStorey97 variable = null;
		__MerchantWindow.<CreateStructureTab>c__AnonStorey98 variable1 = null;
		__MerchantWindow.<CreateStructureTab>c__AnonStorey99 variable2 = null;
		__MerchantWindow.<CreateStructureTab>c__AnonStorey9A variable3 = null;
		IEnumerable<SlotItem> enumerable = Enumerable.Where<SlotItem>(NetworkScript.player.playerBelongings.playerItems.slotItems, new Func<SlotItem, bool>(this, (SlotItem it) => (!PlayerItems.IsStructure(it.get_ItemType()) ? false : it.get_ShipId() == this.onDisplayShipId)));
		IEnumerable<SlotItem> enumerable1 = enumerable;
		if (__MerchantWindow.<>f__am$cache98 == null)
		{
			__MerchantWindow.<>f__am$cache98 = new Func<SlotItem, bool>(null, (SlotItem t) => t.get_SlotType() == 11);
		}
		SlotItem[] array = Enumerable.ToArray<SlotItem>(Enumerable.Where<SlotItem>(enumerable1, __MerchantWindow.<>f__am$cache98));
		IEnumerable<SlotItem> enumerable2 = enumerable;
		if (__MerchantWindow.<>f__am$cache99 == null)
		{
			__MerchantWindow.<>f__am$cache99 = new Func<SlotItem, bool>(null, (SlotItem t) => t.get_SlotType() == 10);
		}
		SlotItem[] slotItemArray = Enumerable.ToArray<SlotItem>(Enumerable.Where<SlotItem>(enumerable2, __MerchantWindow.<>f__am$cache99));
		IEnumerable<SlotItem> enumerable3 = enumerable;
		if (__MerchantWindow.<>f__am$cache9A == null)
		{
			__MerchantWindow.<>f__am$cache9A = new Func<SlotItem, bool>(null, (SlotItem t) => t.get_SlotType() == 9);
		}
		SlotItem[] array1 = Enumerable.ToArray<SlotItem>(Enumerable.Where<SlotItem>(enumerable3, __MerchantWindow.<>f__am$cache9A));
		IEnumerable<SlotItem> enumerable4 = enumerable;
		if (__MerchantWindow.<>f__am$cache9B == null)
		{
			__MerchantWindow.<>f__am$cache9B = new Func<SlotItem, bool>(null, (SlotItem t) => t.get_SlotType() == 12);
		}
		SlotItem[] slotItemArray1 = Enumerable.ToArray<SlotItem>(Enumerable.Where<SlotItem>(enumerable4, __MerchantWindow.<>f__am$cache9B));
		int num = (!NetworkScript.player.playerBelongings.haveExtendetCorpusOne ? 2 : 3);
		for (int i = 0; i < num; i++)
		{
			AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = new AndromedaGuiDragDropPlace();
			SlotItem slotItem = Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(array, new Func<SlotItem, bool>(variable, (SlotItem it) => it.get_Slot() == this.i)));
			andromedaGuiDragDropPlace.location = 11;
			andromedaGuiDragDropPlace.position = new Vector2((float)(684 + i * 46), 170f);
			andromedaGuiDragDropPlace.slot = (byte)i;
			andromedaGuiDragDropPlace.dropZonePosition = andromedaGuiDragDropPlace.position;
			andromedaGuiDragDropPlace.txFrameIdle = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "MerchantSlotIdle");
			andromedaGuiDragDropPlace.txFrameDragSource = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "MerchantSlotHvr");
			andromedaGuiDragDropPlace.txFrameDropTarget = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "MerchantSlotActive");
			andromedaGuiDragDropPlace.txFrameDropTargetHover = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "MerchantSlotHvr");
			andromedaGuiDragDropPlace.glowTexturePrefix = "MerchantSlot";
			andromedaGuiDragDropPlace.item = slotItem;
			andromedaGuiDragDropPlace.isEmpty = slotItem == null;
			andromedaGuiDragDropPlace.shipId = this.onDisplayShipId;
			andromedaGuiDragDropPlace.idleItemTextureSize = new Vector2(36f, 25f);
			Inventory.places.Add(andromedaGuiDragDropPlace);
		}
		int num1 = (!NetworkScript.player.playerBelongings.haveExtendetEngineOne ? 2 : 3);
		for (int j = 0; j < num1; j++)
		{
			AndromedaGuiDragDropPlace vector2 = new AndromedaGuiDragDropPlace();
			SlotItem slotItem1 = Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(slotItemArray1, new Func<SlotItem, bool>(variable1, (SlotItem it) => it.get_Slot() == this.i)));
			vector2.location = 12;
			vector2.position = new Vector2((float)(684 + j * 46), 68f);
			vector2.slot = (byte)j;
			vector2.dropZonePosition = vector2.position;
			vector2.txFrameIdle = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "MerchantSlotIdle");
			vector2.txFrameDragSource = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "MerchantSlotHvr");
			vector2.txFrameDropTarget = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "MerchantSlotActive");
			vector2.txFrameDropTargetHover = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "MerchantSlotHvr");
			vector2.glowTexturePrefix = "MerchantSlot";
			vector2.item = slotItem1;
			vector2.isEmpty = slotItem1 == null;
			vector2.shipId = this.onDisplayShipId;
			vector2.idleItemTextureSize = new Vector2(36f, 25f);
			Inventory.places.Add(vector2);
		}
		int num2 = (!NetworkScript.player.playerBelongings.haveExtendetShielOne ? 2 : 3);
		for (int k = 0; k < num2; k++)
		{
			AndromedaGuiDragDropPlace fromStaticSet = new AndromedaGuiDragDropPlace();
			SlotItem slotItem2 = Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(slotItemArray, new Func<SlotItem, bool>(variable2, (SlotItem it) => it.get_Slot() == this.i)));
			fromStaticSet.location = 10;
			fromStaticSet.position = new Vector2((float)(375 + k * 46), 299f);
			fromStaticSet.slot = (byte)k;
			fromStaticSet.dropZonePosition = fromStaticSet.position;
			fromStaticSet.txFrameIdle = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "MerchantSlotIdle");
			fromStaticSet.txFrameDragSource = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "MerchantSlotHvr");
			fromStaticSet.txFrameDropTarget = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "MerchantSlotActive");
			fromStaticSet.txFrameDropTargetHover = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "MerchantSlotHvr");
			fromStaticSet.glowTexturePrefix = "MerchantSlot";
			fromStaticSet.item = slotItem2;
			fromStaticSet.isEmpty = slotItem2 == null;
			fromStaticSet.shipId = this.onDisplayShipId;
			fromStaticSet.idleItemTextureSize = new Vector2(36f, 25f);
			Inventory.places.Add(fromStaticSet);
		}
		int num3 = (!NetworkScript.player.playerBelongings.haveExtendetAnyOne ? 2 : 3);
		for (int l = 0; l < num3; l++)
		{
			AndromedaGuiDragDropPlace andromedaGuiDragDropPlace1 = new AndromedaGuiDragDropPlace();
			SlotItem slotItem3 = Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(array1, new Func<SlotItem, bool>(variable3, (SlotItem it) => it.get_Slot() == this.i)));
			andromedaGuiDragDropPlace1.location = 9;
			andromedaGuiDragDropPlace1.position = new Vector2((float)(375 + l * 46), 235f);
			andromedaGuiDragDropPlace1.slot = (byte)l;
			andromedaGuiDragDropPlace1.dropZonePosition = andromedaGuiDragDropPlace1.position;
			andromedaGuiDragDropPlace1.txFrameIdle = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "MerchantSlotIdle");
			andromedaGuiDragDropPlace1.txFrameDragSource = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "MerchantSlotHvr");
			andromedaGuiDragDropPlace1.txFrameDropTarget = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "MerchantSlotActive");
			andromedaGuiDragDropPlace1.txFrameDropTargetHover = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "MerchantSlotHvr");
			andromedaGuiDragDropPlace1.glowTexturePrefix = "MerchantSlot";
			andromedaGuiDragDropPlace1.item = slotItem3;
			andromedaGuiDragDropPlace1.isEmpty = slotItem3 == null;
			andromedaGuiDragDropPlace1.shipId = this.onDisplayShipId;
			andromedaGuiDragDropPlace1.idleItemTextureSize = new Vector2(36f, 25f);
			Inventory.places.Add(andromedaGuiDragDropPlace1);
		}
	}

	private void CreateWeaponsTab()
	{
		int num = 0;
		this.ClearAmmoSlots();
		this.ammoSlots.Clear();
		this.ammoButtons.Clear();
		IEnumerable<SlotItem> enumerable = Enumerable.Where<SlotItem>(NetworkScript.player.playerBelongings.playerItems.slotItems, new Func<SlotItem, bool>(this, (SlotItem si) => (si.get_ShipId() != this.onDisplayShipId || si.get_SlotType() < 6 || si.get_SlotType() > 8 || si.get_ItemType() < PlayerItems.TypeWeaponLaserTire1 ? false : si.get_ItemType() <= PlayerItems.TypeWeaponIonTire5)));
		string shipTitle = this.currentShip.ShipTitle;
		if (shipTitle != null)
		{
			if (__MerchantWindow.<>f__switch$map4 == null)
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
				__MerchantWindow.<>f__switch$map4 = dictionary;
			}
			if (__MerchantWindow.<>f__switch$map4.TryGetValue(shipTitle, ref num))
			{
				switch (num)
				{
					case 0:
					{
						this.laserSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_laser");
						this.plasmaSlotsLabel.text = string.Empty;
						this.ionSlotsLabel.text = string.Empty;
						IEnumerable<SlotItem> enumerable1 = enumerable;
						if (__MerchantWindow.<>f__am$cache6A == null)
						{
							__MerchantWindow.<>f__am$cache6A = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 0);
						}
						this.DrawWeaponSlot1(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable1, __MerchantWindow.<>f__am$cache6A)));
						break;
					}
					case 1:
					{
						this.laserSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_laser");
						this.plasmaSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_plasma");
						this.ionSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_ion");
						IEnumerable<SlotItem> enumerable2 = enumerable;
						if (__MerchantWindow.<>f__am$cache6B == null)
						{
							__MerchantWindow.<>f__am$cache6B = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 0);
						}
						this.DrawWeaponSlot1(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable2, __MerchantWindow.<>f__am$cache6B)));
						IEnumerable<SlotItem> enumerable3 = enumerable;
						if (__MerchantWindow.<>f__am$cache6C == null)
						{
							__MerchantWindow.<>f__am$cache6C = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 1);
						}
						this.DrawWeaponSlot2(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable3, __MerchantWindow.<>f__am$cache6C)));
						IEnumerable<SlotItem> enumerable4 = enumerable;
						if (__MerchantWindow.<>f__am$cache6D == null)
						{
							__MerchantWindow.<>f__am$cache6D = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 3);
						}
						this.DrawWeaponSlot4(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable4, __MerchantWindow.<>f__am$cache6D)));
						break;
					}
					case 2:
					{
						this.laserSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_laser");
						this.plasmaSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_plasma");
						this.ionSlotsLabel.text = string.Empty;
						IEnumerable<SlotItem> enumerable5 = enumerable;
						if (__MerchantWindow.<>f__am$cache6E == null)
						{
							__MerchantWindow.<>f__am$cache6E = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 0);
						}
						this.DrawWeaponSlot1(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable5, __MerchantWindow.<>f__am$cache6E)));
						IEnumerable<SlotItem> enumerable6 = enumerable;
						if (__MerchantWindow.<>f__am$cache6F == null)
						{
							__MerchantWindow.<>f__am$cache6F = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 1);
						}
						this.DrawWeaponSlot2(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable6, __MerchantWindow.<>f__am$cache6F)));
						break;
					}
					case 3:
					{
						this.laserSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_laser");
						this.plasmaSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_plasma");
						this.ionSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_ion");
						IEnumerable<SlotItem> enumerable7 = enumerable;
						if (__MerchantWindow.<>f__am$cache70 == null)
						{
							__MerchantWindow.<>f__am$cache70 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 0);
						}
						this.DrawWeaponSlot1(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable7, __MerchantWindow.<>f__am$cache70)));
						IEnumerable<SlotItem> enumerable8 = enumerable;
						if (__MerchantWindow.<>f__am$cache71 == null)
						{
							__MerchantWindow.<>f__am$cache71 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 1);
						}
						this.DrawWeaponSlot2(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable8, __MerchantWindow.<>f__am$cache71)));
						IEnumerable<SlotItem> enumerable9 = enumerable;
						if (__MerchantWindow.<>f__am$cache72 == null)
						{
							__MerchantWindow.<>f__am$cache72 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 3);
						}
						this.DrawWeaponSlot4(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable9, __MerchantWindow.<>f__am$cache72)));
						IEnumerable<SlotItem> enumerable10 = enumerable;
						if (__MerchantWindow.<>f__am$cache73 == null)
						{
							__MerchantWindow.<>f__am$cache73 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 4);
						}
						this.DrawWeaponSlot5(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable10, __MerchantWindow.<>f__am$cache73)));
						IEnumerable<SlotItem> enumerable11 = enumerable;
						if (__MerchantWindow.<>f__am$cache74 == null)
						{
							__MerchantWindow.<>f__am$cache74 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 5);
						}
						this.DrawWeaponSlot6(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable11, __MerchantWindow.<>f__am$cache74)));
						break;
					}
					case 4:
					{
						this.laserSlotsLabel.text = string.Empty;
						this.plasmaSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_plasma");
						this.ionSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_ion");
						IEnumerable<SlotItem> enumerable12 = enumerable;
						if (__MerchantWindow.<>f__am$cache75 == null)
						{
							__MerchantWindow.<>f__am$cache75 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 1);
						}
						this.DrawWeaponSlot2(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable12, __MerchantWindow.<>f__am$cache75)));
						IEnumerable<SlotItem> enumerable13 = enumerable;
						if (__MerchantWindow.<>f__am$cache76 == null)
						{
							__MerchantWindow.<>f__am$cache76 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 3);
						}
						this.DrawWeaponSlot4(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable13, __MerchantWindow.<>f__am$cache76)));
						IEnumerable<SlotItem> enumerable14 = enumerable;
						if (__MerchantWindow.<>f__am$cache77 == null)
						{
							__MerchantWindow.<>f__am$cache77 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 5);
						}
						this.DrawWeaponSlot6(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable14, __MerchantWindow.<>f__am$cache77)));
						break;
					}
					case 5:
					{
						this.laserSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_laser");
						this.plasmaSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_plasma");
						this.ionSlotsLabel.text = string.Empty;
						IEnumerable<SlotItem> enumerable15 = enumerable;
						if (__MerchantWindow.<>f__am$cache78 == null)
						{
							__MerchantWindow.<>f__am$cache78 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 0);
						}
						this.DrawWeaponSlot1(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable15, __MerchantWindow.<>f__am$cache78)));
						IEnumerable<SlotItem> enumerable16 = enumerable;
						if (__MerchantWindow.<>f__am$cache79 == null)
						{
							__MerchantWindow.<>f__am$cache79 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 1);
						}
						this.DrawWeaponSlot2(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable16, __MerchantWindow.<>f__am$cache79)));
						IEnumerable<SlotItem> enumerable17 = enumerable;
						if (__MerchantWindow.<>f__am$cache7A == null)
						{
							__MerchantWindow.<>f__am$cache7A = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 2);
						}
						this.DrawWeaponSlot3(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable17, __MerchantWindow.<>f__am$cache7A)));
						break;
					}
					case 6:
					{
						this.laserSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_laser");
						this.plasmaSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_plasma");
						this.ionSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_ion");
						IEnumerable<SlotItem> enumerable18 = enumerable;
						if (__MerchantWindow.<>f__am$cache7B == null)
						{
							__MerchantWindow.<>f__am$cache7B = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 0);
						}
						this.DrawWeaponSlot1(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable18, __MerchantWindow.<>f__am$cache7B)));
						IEnumerable<SlotItem> enumerable19 = enumerable;
						if (__MerchantWindow.<>f__am$cache7C == null)
						{
							__MerchantWindow.<>f__am$cache7C = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 1);
						}
						this.DrawWeaponSlot2(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable19, __MerchantWindow.<>f__am$cache7C)));
						IEnumerable<SlotItem> enumerable20 = enumerable;
						if (__MerchantWindow.<>f__am$cache7D == null)
						{
							__MerchantWindow.<>f__am$cache7D = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 2);
						}
						this.DrawWeaponSlot3(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable20, __MerchantWindow.<>f__am$cache7D)));
						IEnumerable<SlotItem> enumerable21 = enumerable;
						if (__MerchantWindow.<>f__am$cache7E == null)
						{
							__MerchantWindow.<>f__am$cache7E = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 3);
						}
						this.DrawWeaponSlot4(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable21, __MerchantWindow.<>f__am$cache7E)));
						IEnumerable<SlotItem> enumerable22 = enumerable;
						if (__MerchantWindow.<>f__am$cache7F == null)
						{
							__MerchantWindow.<>f__am$cache7F = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 5);
						}
						this.DrawWeaponSlot6(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable22, __MerchantWindow.<>f__am$cache7F)));
						break;
					}
					case 7:
					{
						this.laserSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_laser");
						this.plasmaSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_plasma");
						this.ionSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_ion");
						IEnumerable<SlotItem> enumerable23 = enumerable;
						if (__MerchantWindow.<>f__am$cache80 == null)
						{
							__MerchantWindow.<>f__am$cache80 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 0);
						}
						this.DrawWeaponSlot1(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable23, __MerchantWindow.<>f__am$cache80)));
						IEnumerable<SlotItem> enumerable24 = enumerable;
						if (__MerchantWindow.<>f__am$cache81 == null)
						{
							__MerchantWindow.<>f__am$cache81 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 1);
						}
						this.DrawWeaponSlot2(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable24, __MerchantWindow.<>f__am$cache81)));
						IEnumerable<SlotItem> enumerable25 = enumerable;
						if (__MerchantWindow.<>f__am$cache82 == null)
						{
							__MerchantWindow.<>f__am$cache82 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 2);
						}
						this.DrawWeaponSlot3(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable25, __MerchantWindow.<>f__am$cache82)));
						IEnumerable<SlotItem> enumerable26 = enumerable;
						if (__MerchantWindow.<>f__am$cache83 == null)
						{
							__MerchantWindow.<>f__am$cache83 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 3);
						}
						this.DrawWeaponSlot4(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable26, __MerchantWindow.<>f__am$cache83)));
						break;
					}
					case 8:
					{
						this.laserSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_laser");
						this.plasmaSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_plasma");
						this.ionSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_ion");
						IEnumerable<SlotItem> enumerable27 = enumerable;
						if (__MerchantWindow.<>f__am$cache84 == null)
						{
							__MerchantWindow.<>f__am$cache84 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 0);
						}
						this.DrawWeaponSlot1(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable27, __MerchantWindow.<>f__am$cache84)));
						IEnumerable<SlotItem> enumerable28 = enumerable;
						if (__MerchantWindow.<>f__am$cache85 == null)
						{
							__MerchantWindow.<>f__am$cache85 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 1);
						}
						this.DrawWeaponSlot2(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable28, __MerchantWindow.<>f__am$cache85)));
						IEnumerable<SlotItem> enumerable29 = enumerable;
						if (__MerchantWindow.<>f__am$cache86 == null)
						{
							__MerchantWindow.<>f__am$cache86 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 3);
						}
						this.DrawWeaponSlot4(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable29, __MerchantWindow.<>f__am$cache86)));
						IEnumerable<SlotItem> enumerable30 = enumerable;
						if (__MerchantWindow.<>f__am$cache87 == null)
						{
							__MerchantWindow.<>f__am$cache87 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 4);
						}
						this.DrawWeaponSlot5(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable30, __MerchantWindow.<>f__am$cache87)));
						break;
					}
					case 9:
					{
						this.laserSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_laser");
						this.plasmaSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_plasma");
						this.ionSlotsLabel.text = string.Empty;
						IEnumerable<SlotItem> enumerable31 = enumerable;
						if (__MerchantWindow.<>f__am$cache88 == null)
						{
							__MerchantWindow.<>f__am$cache88 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 0);
						}
						this.DrawWeaponSlot1(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable31, __MerchantWindow.<>f__am$cache88)));
						IEnumerable<SlotItem> enumerable32 = enumerable;
						if (__MerchantWindow.<>f__am$cache89 == null)
						{
							__MerchantWindow.<>f__am$cache89 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 1);
						}
						this.DrawWeaponSlot2(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable32, __MerchantWindow.<>f__am$cache89)));
						IEnumerable<SlotItem> enumerable33 = enumerable;
						if (__MerchantWindow.<>f__am$cache8A == null)
						{
							__MerchantWindow.<>f__am$cache8A = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 4);
						}
						this.DrawWeaponSlot5(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable33, __MerchantWindow.<>f__am$cache8A)));
						break;
					}
					case 10:
					{
						this.laserSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_laser");
						this.plasmaSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_plasma");
						this.ionSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_ion");
						IEnumerable<SlotItem> enumerable34 = enumerable;
						if (__MerchantWindow.<>f__am$cache8B == null)
						{
							__MerchantWindow.<>f__am$cache8B = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 0);
						}
						this.DrawWeaponSlot1(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable34, __MerchantWindow.<>f__am$cache8B)));
						IEnumerable<SlotItem> enumerable35 = enumerable;
						if (__MerchantWindow.<>f__am$cache8C == null)
						{
							__MerchantWindow.<>f__am$cache8C = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 1);
						}
						this.DrawWeaponSlot2(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable35, __MerchantWindow.<>f__am$cache8C)));
						IEnumerable<SlotItem> enumerable36 = enumerable;
						if (__MerchantWindow.<>f__am$cache8D == null)
						{
							__MerchantWindow.<>f__am$cache8D = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 2);
						}
						this.DrawWeaponSlot3(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable36, __MerchantWindow.<>f__am$cache8D)));
						IEnumerable<SlotItem> enumerable37 = enumerable;
						if (__MerchantWindow.<>f__am$cache8E == null)
						{
							__MerchantWindow.<>f__am$cache8E = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 3);
						}
						this.DrawWeaponSlot4(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable37, __MerchantWindow.<>f__am$cache8E)));
						IEnumerable<SlotItem> enumerable38 = enumerable;
						if (__MerchantWindow.<>f__am$cache8F == null)
						{
							__MerchantWindow.<>f__am$cache8F = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 5);
						}
						this.DrawWeaponSlot6(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable38, __MerchantWindow.<>f__am$cache8F)));
						break;
					}
					case 11:
					{
						this.laserSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_laser");
						this.plasmaSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_plasma");
						this.ionSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_ion");
						IEnumerable<SlotItem> enumerable39 = enumerable;
						if (__MerchantWindow.<>f__am$cache90 == null)
						{
							__MerchantWindow.<>f__am$cache90 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 0);
						}
						this.DrawWeaponSlot1(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable39, __MerchantWindow.<>f__am$cache90)));
						IEnumerable<SlotItem> enumerable40 = enumerable;
						if (__MerchantWindow.<>f__am$cache91 == null)
						{
							__MerchantWindow.<>f__am$cache91 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 1);
						}
						this.DrawWeaponSlot2(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable40, __MerchantWindow.<>f__am$cache91)));
						IEnumerable<SlotItem> enumerable41 = enumerable;
						if (__MerchantWindow.<>f__am$cache92 == null)
						{
							__MerchantWindow.<>f__am$cache92 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 2);
						}
						this.DrawWeaponSlot3(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable41, __MerchantWindow.<>f__am$cache92)));
						IEnumerable<SlotItem> enumerable42 = enumerable;
						if (__MerchantWindow.<>f__am$cache93 == null)
						{
							__MerchantWindow.<>f__am$cache93 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 3);
						}
						this.DrawWeaponSlot4(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable42, __MerchantWindow.<>f__am$cache93)));
						IEnumerable<SlotItem> enumerable43 = enumerable;
						if (__MerchantWindow.<>f__am$cache94 == null)
						{
							__MerchantWindow.<>f__am$cache94 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 4);
						}
						this.DrawWeaponSlot5(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable43, __MerchantWindow.<>f__am$cache94)));
						IEnumerable<SlotItem> enumerable44 = enumerable;
						if (__MerchantWindow.<>f__am$cache95 == null)
						{
							__MerchantWindow.<>f__am$cache95 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 5);
						}
						this.DrawWeaponSlot6(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable44, __MerchantWindow.<>f__am$cache95)));
						break;
					}
					case 12:
					{
						this.laserSlotsLabel.text = string.Empty;
						this.plasmaSlotsLabel.text = StaticData.Translate("key_merdcant_ship_cfg_plasma");
						this.ionSlotsLabel.text = string.Empty;
						IEnumerable<SlotItem> enumerable45 = enumerable;
						if (__MerchantWindow.<>f__am$cache96 == null)
						{
							__MerchantWindow.<>f__am$cache96 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 1);
						}
						this.DrawWeaponSlot2(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable45, __MerchantWindow.<>f__am$cache96)));
						IEnumerable<SlotItem> enumerable46 = enumerable;
						if (__MerchantWindow.<>f__am$cache97 == null)
						{
							__MerchantWindow.<>f__am$cache97 = new Func<SlotItem, bool>(null, (SlotItem sii) => sii.get_Slot() == 4);
						}
						this.DrawWeaponSlot5(Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(enumerable46, __MerchantWindow.<>f__am$cache97)));
						break;
					}
				}
			}
		}
	}

	private void DialogClose(object prm)
	{
		if (this.dialogWindow == null)
		{
			return;
		}
		AndromedaGui.gui.RemoveWindow(this.dialogWindow.handler);
		AndromedaGui.gui.activeToolTipId = -1;
		this.dialogWindow = null;
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
				guiTexture.X = place.position.x + 40f;
				guiTexture.Y = place.position.y + 10f;
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
					EventHandlerParam eventHandlerParam = new EventHandlerParam()
					{
						customData = slotItemWeapon.get_Slot()
					};
					guiButtonFixed.eventHandlerParam = eventHandlerParam;
					AmmoSwichBtnInfo ammoSwichBtnInfo = new AmmoSwichBtnInfo()
					{
						possitionX = guiButtonFixed.X,
						possitionY = guiButtonFixed.Y,
						itemTypeId = slotItemWeapon.get_AmmoType()
					};
					guiButtonFixed.tooltipWindowParam = ammoSwichBtnInfo;
					guiButtonFixed.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(Inventory.DrawAmmoSwitchBtnToolTip);
					guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.OnToggleAmmo);
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
			guiButtonFixed.boundries = new Rect(774f, 168f, 42f, 42f);
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
			guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.OnBuyExpandSlotClick);
			if (this.IsMobile && !flag)
			{
				guiButtonFixed.tooltipTapDownTime = 0f;
			}
			base.AddGuiElement(guiButtonFixed);
			this.forExpandedShipSlots.Add(guiButtonFixed);
		}
		if (!NetworkScript.player.playerBelongings.haveExtendetShielOne)
		{
			GuiButtonFixed rect = new GuiButtonFixed();
			rect.SetTexture("ConfigWindow", "inventory_slot_plus");
			rect.boundries = new Rect(465f, 297f, 42f, 42f);
			rect.Caption = string.Empty;
			rect.isEnabled = flag;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = empty,
				customData2 = rect
			};
			rect.tooltipWindowParam = eventHandlerParam;
			rect.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			eventHandlerParam = new EventHandlerParam()
			{
				customData = (ExtendedSlot)2
			};
			rect.eventHandlerParam = eventHandlerParam;
			rect.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.OnBuyExpandSlotClick);
			if (this.IsMobile && !flag)
			{
				rect.tooltipTapDownTime = 0f;
			}
			base.AddGuiElement(rect);
			this.forExpandedShipSlots.Add(rect);
		}
		if (!NetworkScript.player.playerBelongings.haveExtendetAnyOne)
		{
			GuiButtonFixed drawTooltipWindow = new GuiButtonFixed();
			drawTooltipWindow.SetTexture("ConfigWindow", "inventory_slot_plus");
			drawTooltipWindow.boundries = new Rect(465f, 233f, 42f, 42f);
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
				customData = (ExtendedSlot)4
			};
			drawTooltipWindow.eventHandlerParam = eventHandlerParam;
			drawTooltipWindow.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.OnBuyExpandSlotClick);
			if (this.IsMobile && !flag)
			{
				drawTooltipWindow.tooltipTapDownTime = 0f;
			}
			base.AddGuiElement(drawTooltipWindow);
			this.forExpandedShipSlots.Add(drawTooltipWindow);
		}
		if (!NetworkScript.player.playerBelongings.haveExtendetEngineOne)
		{
			GuiButtonFixed action = new GuiButtonFixed();
			action.SetTexture("ConfigWindow", "inventory_slot_plus");
			action.boundries = new Rect(774f, 66f, 42f, 42f);
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
				customData = (ExtendedSlot)3
			};
			action.eventHandlerParam = eventHandlerParam;
			action.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.OnBuyExpandSlotClick);
			if (this.IsMobile && !flag)
			{
				action.tooltipTapDownTime = 0f;
			}
			base.AddGuiElement(action);
			this.forExpandedShipSlots.Add(action);
		}
		if (!NetworkScript.player.playerBelongings.haveExtendetExtraOne)
		{
			GuiButtonFixed guiButtonFixed1 = new GuiButtonFixed();
			guiButtonFixed1.SetTexture("ConfigWindow", "inventory_slot_plus");
			guiButtonFixed1.boundries = new Rect(774f, 254f, 42f, 42f);
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
				customData = (ExtendedSlot)5
			};
			guiButtonFixed1.eventHandlerParam = eventHandlerParam;
			guiButtonFixed1.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.OnBuyExpandSlotClick);
			if (this.IsMobile && !flag)
			{
				guiButtonFixed1.tooltipTapDownTime = 0f;
			}
			base.AddGuiElement(guiButtonFixed1);
			this.forExpandedShipSlots.Add(guiButtonFixed1);
		}
		if (!NetworkScript.player.playerBelongings.haveExtendetExtraOne)
		{
			empty = StaticData.Translate("key_extra_slots_extra");
		}
		if (!NetworkScript.player.playerBelongings.haveExtendetExtraTwo)
		{
			GuiButtonFixed rect1 = new GuiButtonFixed();
			rect1.SetTexture("ConfigWindow", "inventory_slot_locked");
			if (NetworkScript.player.playerBelongings.haveExtendetExtraOne)
			{
				rect1.SetTexture("ConfigWindow", "inventory_slot_plus");
			}
			rect1.boundries = new Rect(774f, 296f, 42f, 42f);
			rect1.Caption = string.Empty;
			rect1.isEnabled = (!flag ? false : NetworkScript.player.playerBelongings.haveExtendetExtraOne);
			eventHandlerParam = new EventHandlerParam()
			{
				customData = empty,
				customData2 = rect1
			};
			rect1.tooltipWindowParam = eventHandlerParam;
			rect1.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			eventHandlerParam = new EventHandlerParam()
			{
				customData = (ExtendedSlot)6
			};
			rect1.eventHandlerParam = eventHandlerParam;
			rect1.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.OnBuyExpandSlotClick);
			if (this.IsMobile && !flag && !NetworkScript.player.playerBelongings.haveExtendetExtraOne)
			{
				rect1.tooltipTapDownTime = 0f;
			}
			base.AddGuiElement(rect1);
			this.forExpandedShipSlots.Add(rect1);
		}
	}

	private void DrawExpandSlots(float x, float y)
	{
		for (int i = 0; i < 4; i++)
		{
			GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
			if (i != 0)
			{
				GuiTexture guiTexture = new GuiTexture();
				guiTexture.SetTexture("ConfigWindow", "inventory_slot_locked");
				guiTexture.boundries = new Rect(x + (float)(i / 4 * 58), y + (float)(i % 4 * 52), 55f, 55f);
				base.AddGuiElement(guiTexture);
				this.lockedInventorySlots.Add(guiTexture);
			}
			else
			{
				guiButtonFixed.SetTexture("ConfigWindow", "inventory_slot_plus");
				guiButtonFixed.boundries = new Rect(x + (float)(i / 4 * 58), y + (float)(i % 4 * 52), 55f, 55f);
				guiButtonFixed.Caption = string.Empty;
				base.AddGuiElement(guiButtonFixed);
				guiButtonFixed.Hovered = new Action<object, bool>(this, __MerchantWindow.OnExpandBtnHover);
				guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.OnExpandBtnClicked);
				guiButtonFixed.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.DrawExpandSlotsTooltip);
				guiButtonFixed.tooltipWindowParam = new Rect(x + (float)(i / 4 * 58), y + (float)(i % 4 * 52), 150f, 70f);
				this.lockedInventorySlots.Add(guiButtonFixed);
			}
		}
		this.RefreshExpandBtnState();
	}

	private GuiWindow DrawExpandSlotsTooltip(object prm)
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
			text = StaticData.Translate("key_merdcant_expand_inv_lbl").ToUpper(),
			boundries = new Rect(10f, 4f, 130f, 20f),
			FontSize = 10,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 4
		};
		guiWindow.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			text = string.Format(StaticData.Translate("key_merdcant_expand_inv_details"), Enumerable.First<SlotPriceInfo>(Enumerable.Where<SlotPriceInfo>(StaticData.slotPriceInformation, new Func<SlotPriceInfo, bool>(this, (SlotPriceInfo s) => (s.slotId != this.inventorySlotCnt + 4 ? false : s.slotType == "Inventory")))).priceNova),
			boundries = new Rect(10f, 24f, 130f, 45f),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		guiWindow.AddGuiElement(guiLabel1);
		return guiWindow;
	}

	private void DrawInventoryTabs()
	{
		this.inventoryTabTexture = new GuiTexture();
		__MerchantWindow.MerchantInventoryTab merchantInventoryTab = this.selectedInventoryTab;
		if (merchantInventoryTab == __MerchantWindow.MerchantInventoryTab.Inventory)
		{
			this.inventoryTabTexture.SetTexture("NewGUI", "MerchantInventoryTab");
		}
		else if (merchantInventoryTab == __MerchantWindow.MerchantInventoryTab.Cargo)
		{
			this.inventoryTabTexture.SetTexture("NewGUI", "MerchantVaultTab");
		}
		this.inventoryTabTexture.X = 358f;
		this.inventoryTabTexture.Y = 343f;
		base.AddGuiElement(this.inventoryTabTexture);
		this.inventoryTab1Button = new GuiButton()
		{
			boundries = new Rect(358f, 325f, 120f, 60f),
			Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.InventoryTab1Clicked),
			Caption = StaticData.Translate("key_merdcant_tab_inventory"),
			Alignment = 4
		};
		if (this.selectedInventoryTab != __MerchantWindow.MerchantInventoryTab.Inventory)
		{
			this.inventoryTab1Button.textColorHover = GuiNewStyleBar.orangeColor;
			this.inventoryTab1Button.textColorNormal = GuiNewStyleBar.blueColor;
		}
		else
		{
			this.inventoryTab1Button.textColorHover = GuiNewStyleBar.orangeColor;
			this.inventoryTab1Button.textColorNormal = GuiNewStyleBar.orangeColor;
		}
		this.inventoryTab1Button.Alignment = 4;
		base.AddGuiElement(this.inventoryTab1Button);
		this.inventoryTab2Button = new GuiButton()
		{
			boundries = new Rect(490f, 325f, 120f, 60f),
			Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.InventoryTab2Clicked),
			Caption = StaticData.Translate("key_merdcant_tab_cargo"),
			Alignment = 4
		};
		if (this.selectedInventoryTab != __MerchantWindow.MerchantInventoryTab.Cargo)
		{
			this.inventoryTab2Button.textColorHover = GuiNewStyleBar.orangeColor;
			this.inventoryTab2Button.textColorNormal = GuiNewStyleBar.blueColor;
		}
		else
		{
			this.inventoryTab2Button.textColorHover = GuiNewStyleBar.orangeColor;
			this.inventoryTab2Button.textColorNormal = GuiNewStyleBar.orangeColor;
		}
		this.inventoryTab2Button.Alignment = 4;
		base.AddGuiElement(this.inventoryTab2Button);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable()
		{
			X = 840f,
			Y = 341f,
			Width = 150f
		};
		guiButtonResizeable.SetSmallBlueTexture();
		guiButtonResizeable.Alignment = 4;
		guiButtonResizeable.FontSize = 12;
		guiButtonResizeable.SetColor(GuiNewStyleBar.blueButtonsColor);
		guiButtonResizeable.Caption = StaticData.Translate("key_sell_all_inventory_btn");
		guiButtonResizeable.isEnabled = this.SellAllAvailable;
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.OnSellAllClicked);
		base.AddGuiElement(guiButtonResizeable);
		this.forDelete.Add(guiButtonResizeable);
		this.CreateInventorySlots();
	}

	private void DrawItemCategoryTabs()
	{
		this.merchantTabTexture = new GuiTexture();
		switch (this.selectedMerchantTab)
		{
			case __MerchantWindow.MerchantTab.Structures:
			case __MerchantWindow.MerchantTab.Shields:
			{
				this.merchantTabTexture.SetTexture("NewGUI", "MerchantTab1");
				break;
			}
			case __MerchantWindow.MerchantTab.Corpuses:
			{
				this.merchantTabTexture.SetTexture("NewGUI", "MerchantTab2");
				break;
			}
			case __MerchantWindow.MerchantTab.Engines:
			{
				this.merchantTabTexture.SetTexture("NewGUI", "MerchantTab3");
				break;
			}
			case __MerchantWindow.MerchantTab.Extras:
			{
				this.merchantTabTexture.SetTexture("NewGUI", "MerchantTab4");
				break;
			}
			case __MerchantWindow.MerchantTab.Ammo:
			{
				this.merchantTabTexture.SetTexture("NewGUI", "MerchantTab5");
				break;
			}
			case __MerchantWindow.MerchantTab.Weapons:
			{
				this.merchantTabTexture.SetTexture("NewGUI", "MerchantTab6");
				break;
			}
		}
		this.merchantTabTexture.X = 28f;
		this.merchantTabTexture.Y = this.tabBtnOffsetY;
		base.AddGuiElement(this.merchantTabTexture);
		this.tab1Button = new GuiButtonFixed();
		this.tab1Button.SetTexture("NewGUI", "merchant_shield");
		this.tab1Button.boundries = new Rect(50f, this.tabBtnOffsetY, 32f, 32f);
		this.tab1Button.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.Tab1Clicked);
		GuiButtonFixed guiButtonFixed = this.tab1Button;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_merdcant_tooltips_shields"),
			customData2 = this.tab1Button
		};
		guiButtonFixed.tooltipWindowParam = eventHandlerParam;
		this.tab1Button.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		this.tab1Button.Caption = string.Empty;
		if (this.selectedMerchantTab != __MerchantWindow.MerchantTab.Shields)
		{
			this.tab1Button.SetTextureNormal("NewGUI", "merchant_shieldNml");
		}
		else
		{
			this.tab1Button.SetTextureNormal("NewGUI", "merchant_shieldClk");
		}
		base.AddGuiElement(this.tab1Button);
		this.tab5Button = new GuiButtonFixed();
		this.tab5Button.SetTexture("NewGUI", "merchant_corpus");
		this.tab5Button.boundries = new Rect(100f, this.tabBtnOffsetY, 32f, 32f);
		this.tab5Button.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.Tab5Clicked);
		GuiButtonFixed guiButtonFixed1 = this.tab5Button;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_merdcant_tooltips_corpus"),
			customData2 = this.tab5Button
		};
		guiButtonFixed1.tooltipWindowParam = eventHandlerParam;
		this.tab5Button.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		this.tab5Button.Caption = string.Empty;
		if (this.selectedMerchantTab != __MerchantWindow.MerchantTab.Corpuses)
		{
			this.tab5Button.SetTextureNormal("NewGUI", "merchant_corpusNml");
		}
		else
		{
			this.tab5Button.SetTextureNormal("NewGUI", "merchant_corpusClk");
		}
		base.AddGuiElement(this.tab5Button);
		this.tab6Button = new GuiButtonFixed();
		this.tab6Button.SetTexture("NewGUI", "merchant_engine");
		this.tab6Button.boundries = new Rect(150f, this.tabBtnOffsetY, 32f, 32f);
		this.tab6Button.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.Tab6Clicked);
		GuiButtonFixed guiButtonFixed2 = this.tab6Button;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_merdcant_tooltips_engine"),
			customData2 = this.tab6Button
		};
		guiButtonFixed2.tooltipWindowParam = eventHandlerParam;
		this.tab6Button.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		this.tab6Button.Caption = string.Empty;
		if (this.selectedMerchantTab != __MerchantWindow.MerchantTab.Engines)
		{
			this.tab6Button.SetTextureNormal("NewGUI", "merchant_engineNml");
		}
		else
		{
			this.tab6Button.SetTextureNormal("NewGUI", "merchant_engineClk");
		}
		base.AddGuiElement(this.tab6Button);
		this.tab2Button = new GuiButtonFixed();
		this.tab2Button.SetTexture("NewGUI", "merchant_extras");
		this.tab2Button.boundries = new Rect(200f, this.tabBtnOffsetY, 32f, 32f);
		this.tab2Button.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.Tab2Clicked);
		GuiButtonFixed guiButtonFixed3 = this.tab2Button;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_merdcant_tooltips_extras"),
			customData2 = this.tab2Button
		};
		guiButtonFixed3.tooltipWindowParam = eventHandlerParam;
		this.tab2Button.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		this.tab2Button.Caption = string.Empty;
		if (this.selectedMerchantTab != __MerchantWindow.MerchantTab.Extras)
		{
			this.tab2Button.SetTextureNormal("NewGUI", "merchant_extrasNml");
		}
		else
		{
			this.tab2Button.SetTextureNormal("NewGUI", "merchant_extrasClk");
		}
		base.AddGuiElement(this.tab2Button);
		this.tab3Button = new GuiButtonFixed();
		this.tab3Button.SetTexture("NewGUI", "merchant_ammo");
		this.tab3Button.boundries = new Rect(250f, this.tabBtnOffsetY, 32f, 32f);
		this.tab3Button.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.Tab3Clicked);
		GuiButtonFixed guiButtonFixed4 = this.tab3Button;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_merdcant_tooltips_ammo"),
			customData2 = this.tab3Button
		};
		guiButtonFixed4.tooltipWindowParam = eventHandlerParam;
		this.tab3Button.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		this.tab3Button.isEnabled = this.selectedOption == __MerchantWindow.MenuOption.Merchant;
		this.tab3Button.Caption = string.Empty;
		if (this.selectedMerchantTab != __MerchantWindow.MerchantTab.Ammo)
		{
			this.tab3Button.SetTextureNormal("NewGUI", "merchant_ammoNml");
		}
		else
		{
			this.tab3Button.SetTextureNormal("NewGUI", "merchant_ammoClk");
		}
		base.AddGuiElement(this.tab3Button);
		this.tab4Button = new GuiButtonFixed();
		this.tab4Button.SetTexture("NewGUI", "merchant_weapon");
		this.tab4Button.boundries = new Rect(300f, this.tabBtnOffsetY, 32f, 32f);
		this.tab4Button.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.Tab4Clicked);
		GuiButtonFixed guiButtonFixed5 = this.tab4Button;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_merdcant_tooltips_weapons"),
			customData2 = this.tab4Button
		};
		guiButtonFixed5.tooltipWindowParam = eventHandlerParam;
		this.tab4Button.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		this.tab4Button.Caption = string.Empty;
		if (this.selectedMerchantTab != __MerchantWindow.MerchantTab.Weapons)
		{
			this.tab4Button.SetTextureNormal("NewGUI", "merchant_weaponNml");
		}
		else
		{
			this.tab4Button.SetTextureNormal("NewGUI", "merchant_weaponClk");
		}
		base.AddGuiElement(this.tab4Button);
		this.CreateMerchantSlots();
		this.PutAdditionalInfo();
	}

	private void DrawShipConfigSection()
	{
		__MerchantWindow.<DrawShipConfigSection>c__AnonStorey90 variable = null;
		if (this.onDisplayShipId == 0)
		{
			for (int i = 0; i < (int)NetworkScript.player.playerBelongings.playerShips.Length; i++)
			{
				if (NetworkScript.player.playerBelongings.playerShips[i].ShipID == NetworkScript.player.playerBelongings.selectedShipId)
				{
					this.currentShipIndex = i;
				}
			}
			this.currentShip = NetworkScript.player.playerBelongings.playerShips[this.currentShipIndex];
			this.onDisplayShipId = this.currentShip.ShipID;
			this.currentShipAssetName = Enumerable.First<ShipsTypeNet>(Enumerable.Where<ShipsTypeNet>(StaticData.shipTypes, new Func<ShipsTypeNet, bool>(variable, (ShipsTypeNet w) => w.id == this.<>f__this.currentShip.shipTypeId))).assetName;
		}
		this.currentShip.ApplyBonuses(NetworkScript.player.playerBelongings);
		this.shipTexture = new GuiTexture();
		this.shipTexture.SetTexture("CfgMenuBg", this.currentShipAssetName);
		this.shipTexture.boundries = new Rect(460f, 65f, 283f, 242f);
		base.AddGuiElement(this.shipTexture);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(375f, 38f, 105f, 15f),
			text = StaticData.Translate("key_merdcant_ship_cfg_weapons"),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel);
		this.mainSlots.Add(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(684f, 151f, 105f, 15f),
			text = StaticData.Translate("key_merdcant_ship_cfg_corpus"),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel1);
		this.mainSlots.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(378f, 281f, 105f, 15f),
			text = StaticData.Translate("key_merdcant_ship_cfg_shield"),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel2);
		this.mainSlots.Add(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(684f, 49f, 105f, 15f),
			text = StaticData.Translate("key_merdcant_ship_cfg_engine"),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel3);
		this.mainSlots.Add(guiLabel3);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			boundries = new Rect(378f, 218f, 105f, 15f),
			text = StaticData.Translate("key_merdcant_ship_cfg_extended"),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel4);
		this.mainSlots.Add(guiLabel4);
		GuiLabel guiLabel5 = new GuiLabel()
		{
			boundries = new Rect(684f, 240f, 80f, 15f),
			text = StaticData.Translate("key_merdcant_ship_cfg_extras"),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel5);
		this.mainSlots.Add(guiLabel5);
		this.laserSlotsLabel = new GuiLabel()
		{
			boundries = new Rect(376f, 54f, 120f, 12f),
			text = StaticData.Translate("key_merdcant_ship_cfg_laser"),
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.laserSlotsLabel);
		this.plasmaSlotsLabel = new GuiLabel()
		{
			boundries = new Rect(376f, 107f, 120f, 12f),
			text = StaticData.Translate("key_merdcant_ship_cfg_plasma"),
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.plasmaSlotsLabel);
		this.ionSlotsLabel = new GuiLabel()
		{
			boundries = new Rect(376f, 162f, 120f, 20f),
			text = StaticData.Translate("key_merdcant_ship_cfg_ion"),
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.ionSlotsLabel);
		this.txSelectedShip = new GuiTexture()
		{
			boundries = new Rect(848f, 240f, 130f, 89f)
		};
		this.txSelectedShip.SetTextureKeepSize("ShipsAvatars", this.currentShipAssetName);
		base.AddGuiElement(this.txSelectedShip);
		if (this.btnSelectShip != null)
		{
			base.RemoveGuiElement(this.btnSelectShip);
		}
		PlayerShipNet playerShipNet = NetworkScript.player.playerBelongings.playerShips[this.currentShipIndex];
		int num = Enumerable.First<ShipsTypeNet>(Enumerable.Where<ShipsTypeNet>(StaticData.shipTypes, new Func<ShipsTypeNet, bool>(variable, (ShipsTypeNet t) => t.id == this.ship.shipTypeId))).levelRestriction;
		if (playerShipNet.ShipID != NetworkScript.player.playerBelongings.selectedShipId)
		{
			this.btnSelectShip = new GuiButtonResizeable();
			this.btnSelectShip.SetSmallOrangeTexture();
			this.btnSelectShip.X = 852f;
			this.btnSelectShip.Y = 310f;
			this.btnSelectShip.Width = 125f;
			this.btnSelectShip.Caption = StaticData.Translate("key_dock_my_ships_select_ship");
			this.btnSelectShip.FontSize = 14;
			this.btnSelectShip.MarginTop = 4;
			this.btnSelectShip.isEnabled = num <= NetworkScript.player.playerBelongings.playerLevel;
			this.btnSelectShip.Alignment = 1;
			this.btnSelectShip.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.OnSelectShipClicked);
			base.AddGuiElement(this.btnSelectShip);
			if (!this.btnSelectShip.isEnabled)
			{
				GuiButtonResizeable guiButtonResizeable = this.btnSelectShip;
				EventHandlerParam eventHandlerParam = new EventHandlerParam()
				{
					customData = string.Format(StaticData.Translate("key_tooltip_level_restriction"), num),
					customData2 = this.btnSelectShip
				};
				guiButtonResizeable.tooltipWindowParam = eventHandlerParam;
				this.btnSelectShip.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			}
		}
		this.btnPreviousShip = new GuiButtonFixed();
		this.btnPreviousShip.SetTexture("NewGUI", "switch_ship_left");
		this.btnPreviousShip.Caption = string.Empty;
		this.btnPreviousShip.X = 839f;
		this.btnPreviousShip.Y = 242f;
		this.btnPreviousShip.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.OnPreviousShipBtnClicked);
		base.AddGuiElement(this.btnPreviousShip);
		this.btnNextShip = new GuiButtonFixed();
		this.btnNextShip.SetTexture("NewGUI", "switch_ship_right");
		this.btnNextShip.Caption = string.Empty;
		this.btnNextShip.X = 969f;
		this.btnNextShip.Y = 242f;
		this.btnNextShip.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.OnNextShipBtnClicked);
		base.AddGuiElement(this.btnNextShip);
		this.lblCurrentShipName = new GuiLabel()
		{
			boundries = new Rect(847f, 211f, 120f, 17f),
			text = StaticData.Translate(this.currentShip.ShipTitle).ToUpper(),
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(this.lblCurrentShipName);
		this.PopulateChangeShipBtn();
		this.shipStatsLbl = new GuiLabel()
		{
			text = StaticData.Translate("key_merdcant_ship_stats"),
			boundries = new Rect(842f, 25f, 200f, 40f),
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 15,
			Alignment = 3
		};
		base.AddGuiElement(this.shipStatsLbl);
		GuiLabel guiLabel6 = new GuiLabel()
		{
			text = StaticData.Translate("key_merdcant_ship_stats_dmg"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(842f, 64f, 200f, 20f),
			FontSize = 12,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel6);
		this.forDelete.Add(guiLabel6);
		int num1 = 0;
		this.CalculateShipDamage(this.currentShip, out num1);
		this.lblDamageVal = new GuiLabel()
		{
			text = num1.ToString("##,##0"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel6.X, guiLabel6.Y, 145f, 20f),
			FontSize = 12,
			Alignment = 5
		};
		this.lblDamageVal.TextColor = Color.get_white();
		base.AddGuiElement(this.lblDamageVal);
		this.forDelete.Add(this.lblDamageVal);
		GuiLabel guiLabel7 = new GuiLabel()
		{
			text = StaticData.Translate("key_merdcant_ship_stats_corpus"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel6.X, guiLabel6.Y + 16f, 200f, 20f),
			FontSize = 12,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel7);
		this.forDelete.Add(guiLabel7);
		this.lblCorpusVal = new GuiLabel()
		{
			text = this.currentShip.Corpus.ToString("##,##0"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel6.X, guiLabel7.Y, 145f, 20f),
			FontSize = 12,
			Alignment = 5
		};
		this.lblCorpusVal.TextColor = Color.get_white();
		base.AddGuiElement(this.lblCorpusVal);
		this.forDelete.Add(this.lblCorpusVal);
		GuiLabel guiLabel8 = new GuiLabel()
		{
			text = StaticData.Translate("key_merdcant_ship_stats_shield"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel6.X, guiLabel7.Y + 16f, 200f, 20f),
			FontSize = 12,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel8);
		this.forDelete.Add(guiLabel8);
		this.lblShieldVal = new GuiLabel()
		{
			text = this.currentShip.Shield.ToString("##,##0"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel6.X, guiLabel8.Y, 145f, 20f),
			FontSize = 12,
			Alignment = 5
		};
		this.lblShieldVal.TextColor = Color.get_white();
		base.AddGuiElement(this.lblShieldVal);
		this.forDelete.Add(this.lblShieldVal);
		GuiLabel guiLabel9 = new GuiLabel()
		{
			text = StaticData.Translate("key_ship_cfg_shield_power"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel6.X, guiLabel8.Y + 16f, 200f, 20f),
			FontSize = 12,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel9);
		this.forDelete.Add(guiLabel9);
		this.lblShieldRegenVal = new GuiLabel()
		{
			text = this.currentShip.shieldPower.ToString("##,##0"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel6.X, guiLabel9.Y, 145f, 20f),
			FontSize = 12,
			Alignment = 5
		};
		this.lblShieldRegenVal.TextColor = Color.get_white();
		base.AddGuiElement(this.lblShieldRegenVal);
		this.forDelete.Add(this.lblShieldRegenVal);
		GuiLabel guiLabel10 = new GuiLabel()
		{
			text = StaticData.Translate("key_merdcant_ship_stats_avoidance"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel6.X, guiLabel9.Y + 16f, 200f, 20f),
			FontSize = 12,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel10);
		this.forDelete.Add(guiLabel10);
		this.lblAvoidanceVal = new GuiLabel()
		{
			text = this.currentShip.Avoidance.ToString("##,##0"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel6.X, guiLabel10.Y, 145f, 20f),
			FontSize = 12,
			Alignment = 5
		};
		this.lblAvoidanceVal.TextColor = Color.get_white();
		base.AddGuiElement(this.lblAvoidanceVal);
		this.forDelete.Add(this.lblAvoidanceVal);
		GuiLabel guiLabel11 = new GuiLabel()
		{
			text = StaticData.Translate("key_merdcant_ship_stats_targeting"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel6.X, guiLabel10.Y + 16f, 200f, 20f),
			FontSize = 12,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel11);
		this.forDelete.Add(guiLabel11);
		this.lblTargetingVal = new GuiLabel()
		{
			text = this.currentShip.Targeting.ToString("##,##0"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel6.X, guiLabel11.Y, 145f, 20f),
			FontSize = 12,
			Alignment = 5
		};
		this.lblTargetingVal.TextColor = Color.get_white();
		base.AddGuiElement(this.lblTargetingVal);
		this.forDelete.Add(this.lblTargetingVal);
		GuiLabel guiLabel12 = new GuiLabel()
		{
			text = StaticData.Translate("key_merdcant_ship_stats_speed"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel6.X, guiLabel11.Y + 16f, 200f, 20f),
			FontSize = 12,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel12);
		this.forDelete.Add(guiLabel12);
		this.lblSpeedVal = new GuiLabel()
		{
			text = this.currentShip.Speed.ToString("##,##0"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel6.X, guiLabel12.Y, 145f, 20f),
			FontSize = 12,
			Alignment = 5
		};
		this.lblSpeedVal.TextColor = Color.get_white();
		base.AddGuiElement(this.lblSpeedVal);
		this.forDelete.Add(this.lblSpeedVal);
		GuiLabel guiLabel13 = new GuiLabel()
		{
			text = StaticData.Translate("key_merdcant_ship_stats_cargo"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel6.X, guiLabel12.Y + 16f, 200f, 20f),
			FontSize = 12,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel13);
		this.forDelete.Add(guiLabel13);
		this.lblCargoVal = new GuiLabel()
		{
			text = this.currentShip.MaxCargo.ToString("##,##0"),
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiLabel6.X, guiLabel13.Y, 145f, 20f),
			FontSize = 12,
			Alignment = 5
		};
		this.lblCargoVal.TextColor = Color.get_white();
		base.AddGuiElement(this.lblCargoVal);
		this.forDelete.Add(this.lblCargoVal);
		this.CreateWeaponsTab();
		this.CreateStructureTab();
		this.CreateExtrasTab();
		this.DrawExpandShipSlots();
	}

	private void DrawWeaponItemOnShip(AndromedaGuiDragDropPlace place, SlotItem item)
	{
		place.dropZonePosition = place.position;
		place.txFrameIdle = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "MerchantSlotIdle");
		place.txFrameDragSource = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "MerchantSlotHvr");
		place.txFrameDropTarget = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "MerchantSlotActive");
		place.txFrameDropTargetHover = (Texture2D)playWebGame.assets.GetFromStaticSet("NewGUI", "MerchantSlotHvr");
		place.glowTexturePrefix = "MerchantSlot";
		place.item = item;
		place.isEmpty = item == null;
		place.shipId = this.onDisplayShipId;
		place.idleItemTextureSize = new Vector2(36f, 25f);
		Inventory.places.Add(place);
		this.DrawAmmoSlot(place);
	}

	private AndromedaGuiDragDropPlace DrawWeaponSlot1(SlotItem item)
	{
		AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = new AndromedaGuiDragDropPlace()
		{
			position = new Vector2(376f, 65f),
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
			position = new Vector2(376f, 120f),
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
			position = new Vector2(446f, 65f),
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
			position = new Vector2(376f, 175f),
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
			position = new Vector2(446f, 120f),
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
			position = new Vector2(446f, 175f),
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

	private int GetWeaponTierByType(ushort id)
	{
		if (id == PlayerItems.TypeWeaponIonTire1 || id == PlayerItems.TypeWeaponPlasmaTire1 || id == PlayerItems.TypeWeaponLaserTire1)
		{
			return 0;
		}
		if (id == PlayerItems.TypeWeaponIonTire2 || id == PlayerItems.TypeWeaponPlasmaTire2 || id == PlayerItems.TypeWeaponLaserTire2)
		{
			return 1;
		}
		if (id == PlayerItems.TypeWeaponIonTire3 || id == PlayerItems.TypeWeaponPlasmaTire3 || id == PlayerItems.TypeWeaponLaserTire3)
		{
			return 2;
		}
		if (id != PlayerItems.TypeWeaponIonTire4 && id != PlayerItems.TypeWeaponPlasmaTire4 && id != PlayerItems.TypeWeaponLaserTire4)
		{
			return 4;
		}
		return 3;
	}

	private void InventoryTab1Clicked(EventHandlerParam parm)
	{
		this.Clear();
		this.selectedInventoryTab = __MerchantWindow.MerchantInventoryTab.Inventory;
		this.Create();
	}

	private void InventoryTab2Clicked(EventHandlerParam parm)
	{
		this.Clear();
		this.selectedInventoryTab = __MerchantWindow.MerchantInventoryTab.Cargo;
		this.Create();
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
		// Current member / type: System.Void __MerchantWindow::OnBuyExpandSlotClick(EventHandlerParam)
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

	private void OnExpandBtnClicked(EventHandlerParam prm)
	{
		int num = Enumerable.First<SlotPriceInfo>(Enumerable.Where<SlotPriceInfo>(StaticData.slotPriceInformation, new Func<SlotPriceInfo, bool>(this, (SlotPriceInfo s) => (s.slotId != this.inventorySlotCnt + 4 ? false : s.slotType == "Inventory")))).priceNova;
		this.expandSlotWindow = new ExpandSlotDialog()
		{
			ConfirmExpand = new Action<EventHandlerParam>(this, __MerchantWindow.ConfirmExpand)
		};
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

	private void OnItemRerollBtnClicked(object prm)
	{
		// 
		// Current member / type: System.Void __MerchantWindow::OnItemRerollBtnClicked(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnItemRerollBtnClicked(System.Object)
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

	private void OnItemRerollZoneDrop(int prm)
	{
		if (this.itemRerolEmptyBoxInfo != null)
		{
			base.RemoveGuiElement(this.itemRerolEmptyBoxInfo);
			this.itemRerolEmptyBoxInfo = null;
		}
		this.activeDropdowns.Clear();
		this.activeCheckboxes.Clear();
		this.selectedSlotItem = prm;
		this.CreateRerollSection();
		this.CreateBeforeSection();
		this.CreateAfterSection();
		GuiTexture guiTexture = this.rerollDropZone;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_reroll_box_full_tooltip"),
			customData2 = this.rerollDropZone
		};
		guiTexture.tooltipWindowParam = eventHandlerParam;
	}

	private void OnMaximizeClick(bool state)
	{
		this.PopulateBonusValue();
	}

	private void OnMenuOptionClick(EventHandlerParam prm)
	{
		this.selectedOption = (int)prm.customData;
		this.selectedMerchantTab = __MerchantWindow.MerchantTab.Shields;
		this.Clear();
		this.Create();
		this.btnMerchant.SetTextureNormal("GUI", "btnMerchantNml");
		this.btnGambler.SetTextureNormal("GUI", "btnGamblerNml");
		this.btnReroll.SetTextureNormal("GUI", "btnRerollNml");
		switch (this.selectedOption)
		{
			case __MerchantWindow.MenuOption.Merchant:
			{
				this.selectedSectionLbl.text = StaticData.Translate("key_merchant_lbl_merchant");
				this.btnMerchant.SetTextureNormal("GUI", "btnMerchantClk");
				base.SetBackgroundTexture("NewGUI", "MerchantWndBackground");
				this.UpdateSectionLabel(this.selectedMerchantTab);
				break;
			}
			case __MerchantWindow.MenuOption.Gambler:
			{
				this.selectedSectionLbl.text = StaticData.Translate("key_merchant_lbl_gambler");
				this.btnGambler.SetTextureNormal("GUI", "btnGamblerClk");
				base.SetBackgroundTexture("NewGUI", "MerchantWndBackground");
				this.UpdateSectionLabel(this.selectedMerchantTab);
				break;
			}
			case __MerchantWindow.MenuOption.Reroll:
			{
				this.selectedSectionLbl.text = StaticData.Translate("key_merchant_lbl_reroll");
				this.btnReroll.SetTextureNormal("GUI", "btnRerollClk");
				base.SetBackgroundTexture("NewGUI", "item_reroll");
				foreach (GuiElement forExpandedShipSlot in this.forExpandedShipSlots)
				{
					base.RemoveGuiElement(forExpandedShipSlot);
				}
				this.forExpandedShipSlots.Clear();
				break;
			}
		}
	}

	private void OnNextShipBtnClicked(object prm)
	{
		__MerchantWindow _MerchantWindow = this;
		_MerchantWindow.currentShipIndex = _MerchantWindow.currentShipIndex + 1;
		this.currentShipIndex = Math.Min(this.currentShipIndex, (int)NetworkScript.player.playerBelongings.playerShips.Length - 1);
		this.currentShip = NetworkScript.player.playerBelongings.playerShips[this.currentShipIndex];
		this.PopulateChangeShipBtn();
		this.onDisplayShipId = this.currentShip.ShipID;
		this.PopulateCurrentShipStats();
	}

	private void OnPreviousShipBtnClicked(object prm)
	{
		__MerchantWindow _MerchantWindow = this;
		_MerchantWindow.currentShipIndex = _MerchantWindow.currentShipIndex - 1;
		this.currentShipIndex = Math.Max(this.currentShipIndex, 0);
		this.currentShip = NetworkScript.player.playerBelongings.playerShips[this.currentShipIndex];
		this.PopulateChangeShipBtn();
		this.onDisplayShipId = this.currentShip.ShipID;
		this.PopulateCurrentShipStats();
	}

	private void OnQualityGambleToogle(bool state)
	{
		this.isQualityGambleOn = state;
		Inventory.ClearSlots(this);
		Inventory.secondaryDropHandler = new Action<AndromedaGuiDragDropPlace, AndromedaGuiDragDropPlace>(this, __MerchantWindow.DropHandler);
		Inventory.closeStackablePopUp = new Action(this, __MerchantWindow.Refresh);
		this.CreateInventorySlots();
		this.CreateMerchantSlots();
		this.CreateWeaponsTab();
		this.CreateStructureTab();
		this.CreateExtrasTab();
		Inventory.DrawPlaces(this);
	}

	private void OnSelectShipClicked(EventHandlerParam prm)
	{
		this.CreateSelecktShipDialogue(NetworkScript.player.playerBelongings.playerShips[this.currentShipIndex].ShipID);
	}

	private void OnSellAllClicked(object prm)
	{
		string str = StaticData.Translate("key_sell_all_inventory_title");
		string str1 = StaticData.Translate("key_sell_all_inventory_info");
		string str2 = string.Concat(StaticData.Translate("key_sell_all_inventory_price"), string.Format(" $ {0:N0}", this.SellAllPrice));
		string str3 = StaticData.Translate("key_dock_my_ships_select_ship_yes");
		string str4 = StaticData.Translate("key_dock_my_ships_select_ship_no");
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = true
		};
		NewPopUpWindow.CreatePopUpWindow(str, str1, str2, str3, str4, out Inventory.dialogWindow, eventHandlerParam, new Action<EventHandlerParam>(this, __MerchantWindow.OnSellAllConfirm), null);
	}

	private void OnSellAllConfirm(object prm)
	{
		// 
		// Current member / type: System.Void __MerchantWindow::OnSellAllConfirm(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnSellAllConfirm(System.Object)
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

	private void OnToggleAmmo(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void __MerchantWindow::OnToggleAmmo(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnToggleAmmo(EventHandlerParam)
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

	private List<SlotItem> OrderSlotItemsByPrice(List<SlotItem> items)
	{
		List<SlotItem> list = new List<SlotItem>();
		List<__MerchantWindow.SlotItemWithPrice> list1 = new List<__MerchantWindow.SlotItemWithPrice>();
		foreach (SlotItem item in items)
		{
			PlayerItemTypesData playerItemTypesDatum = StaticData.allTypes.get_Item(item.get_ItemType());
			__MerchantWindow.SlotItemWithPrice slotItemWithPrice = new __MerchantWindow.SlotItemWithPrice()
			{
				item = item,
				priceCash = playerItemTypesDatum.priceCash,
				priceNova = playerItemTypesDatum.priceNova,
				priceViral = playerItemTypesDatum.priceViral
			};
			list1.Add(slotItemWithPrice);
		}
		List<__MerchantWindow.SlotItemWithPrice> list2 = list1;
		if (__MerchantWindow.<>f__am$cache5A == null)
		{
			__MerchantWindow.<>f__am$cache5A = new Func<__MerchantWindow.SlotItemWithPrice, bool>(null, (__MerchantWindow.SlotItemWithPrice t) => t.priceCash != 0);
		}
		IEnumerable<__MerchantWindow.SlotItemWithPrice> enumerable = Enumerable.Where<__MerchantWindow.SlotItemWithPrice>(list2, __MerchantWindow.<>f__am$cache5A);
		if (__MerchantWindow.<>f__am$cache5B == null)
		{
			__MerchantWindow.<>f__am$cache5B = new Func<__MerchantWindow.SlotItemWithPrice, int>(null, (__MerchantWindow.SlotItemWithPrice c) => c.priceCash * c.item.get_Amount());
		}
		List<__MerchantWindow.SlotItemWithPrice> list3 = Enumerable.ToList<__MerchantWindow.SlotItemWithPrice>(Enumerable.OrderBy<__MerchantWindow.SlotItemWithPrice, int>(enumerable, __MerchantWindow.<>f__am$cache5B));
		List<__MerchantWindow.SlotItemWithPrice> list4 = list1;
		if (__MerchantWindow.<>f__am$cache5C == null)
		{
			__MerchantWindow.<>f__am$cache5C = new Func<__MerchantWindow.SlotItemWithPrice, bool>(null, (__MerchantWindow.SlotItemWithPrice t) => t.priceCash == 0);
		}
		IEnumerable<__MerchantWindow.SlotItemWithPrice> enumerable1 = Enumerable.Where<__MerchantWindow.SlotItemWithPrice>(list4, __MerchantWindow.<>f__am$cache5C);
		if (__MerchantWindow.<>f__am$cache5D == null)
		{
			__MerchantWindow.<>f__am$cache5D = new Func<__MerchantWindow.SlotItemWithPrice, int>(null, (__MerchantWindow.SlotItemWithPrice n) => n.priceNova * n.item.get_Amount());
		}
		IOrderedEnumerable<__MerchantWindow.SlotItemWithPrice> orderedEnumerable = Enumerable.OrderBy<__MerchantWindow.SlotItemWithPrice, int>(enumerable1, __MerchantWindow.<>f__am$cache5D);
		if (__MerchantWindow.<>f__am$cache5E == null)
		{
			__MerchantWindow.<>f__am$cache5E = new Func<__MerchantWindow.SlotItemWithPrice, int>(null, (__MerchantWindow.SlotItemWithPrice v) => v.priceViral * v.item.get_Amount());
		}
		List<__MerchantWindow.SlotItemWithPrice> list5 = Enumerable.ToList<__MerchantWindow.SlotItemWithPrice>(Enumerable.ThenBy<__MerchantWindow.SlotItemWithPrice, int>(orderedEnumerable, __MerchantWindow.<>f__am$cache5E));
		list1.Clear();
		list1.AddRange(list3);
		list1.AddRange(list5);
		foreach (__MerchantWindow.SlotItemWithPrice slotItemWithPrice1 in list1)
		{
			list.Add(slotItemWithPrice1.item);
		}
		return list;
	}

	private void PopulateBonusValue()
	{
		__MerchantWindow.<PopulateBonusValue>c__AnonStorey9E variable = null;
		foreach (GuiElement guiElement in this.bonusValuesForDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.bonusValuesForDelete.Clear();
		int num = this.selectedSlotItem % 1000;
		int num1 = this.selectedSlotItem / 1000;
		SlotItem slotItem = Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(NetworkScript.player.playerBelongings.playerItems.slotItems, new Func<SlotItem, bool>(variable, (SlotItem t) => (t.get_Slot() != this.itemSlot ? false : t.get_SlotType() == (byte)this.itemSlotType))));
		if (slotItem == null)
		{
			return;
		}
		PlayerItemTypesData item = StaticData.allTypes.get_Item(slotItem.get_ItemType());
		float single = 0f;
		int item1 = 0;
		int item2 = 0;
		for (int i = 1; i <= Enumerable.Count<KeyValuePair<int, GuiDropdown>>(this.activeDropdowns); i++)
		{
			GuiLabel guiLabel = new GuiLabel()
			{
				boundries = new Rect(784f, (float)(84 + i * 30), 50f, 16f),
				text = string.Empty,
				TextColor = GuiNewStyleBar.greenColor,
				Alignment = 4,
				Font = GuiLabel.FontBold,
				FontSize = 10
			};
			base.AddGuiElement(guiLabel);
			this.bonusValuesForDelete.Add(guiLabel);
			GuiLabel str = new GuiLabel()
			{
				boundries = new Rect(839f, (float)(84 + i * 30), 50f, 16f),
				text = string.Empty,
				TextColor = GuiNewStyleBar.greenColor,
				Alignment = 4,
				Font = GuiLabel.FontBold,
				FontSize = 10
			};
			base.AddGuiElement(str);
			this.bonusValuesForDelete.Add(str);
			switch (this.activeDropdowns.get_Item(i).selectedItem)
			{
				case 0:
				{
					item1 = 0;
					item2 = 0;
					break;
				}
				case 1:
				{
					if (PlayerItems.IsWeapon(slotItem.get_ItemType()))
					{
						item1 = Mineral.enchantsDamageTable.get_Item(slotItem.get_ItemType()).min;
						item2 = Mineral.enchantsDamageTable.get_Item(slotItem.get_ItemType()).max;
					}
					else if (PlayerItems.IsCorpus(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(2) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsShield(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(0) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsEngine(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(4) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsExtraCooldown(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(9) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single) * 50;
						item2 = (int)Math.Max(1f, single * 3f) * 50;
					}
					else if (PlayerItems.IsExtraCargoMining(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(5) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsExtraDamage(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(5) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsExtraOther(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(4) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					break;
				}
				case 2:
				{
					if (PlayerItems.IsWeapon(slotItem.get_ItemType()))
					{
						item1 = 1;
						item2 = 4;
					}
					else if (PlayerItems.IsCorpus(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(3) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsShield(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(1) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsEngine(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(6) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsExtraCooldown(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(10) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single) * 50;
						item2 = (int)Math.Max(1f, single * 3f) * 50;
					}
					else if (PlayerItems.IsExtraCargoMining(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(6) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsExtraDamage(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(6) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsExtraOther(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(5) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					break;
				}
				case 3:
				{
					if (PlayerItems.IsWeapon(slotItem.get_ItemType()))
					{
						item1 = 100;
						item2 = 500;
					}
					else if (PlayerItems.IsCorpus(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(5) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsShield(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(5) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsEngine(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(7) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsExtraCooldown(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(11) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single) * 50;
						item2 = (int)Math.Max(1f, single * 3f) * 50;
					}
					else if (PlayerItems.IsExtraCargoMining(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(7) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsExtraDamage(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(12) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsExtraOther(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(6) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					break;
				}
				case 4:
				{
					if (PlayerItems.IsWeapon(slotItem.get_ItemType()))
					{
						item1 = 2;
						item2 = 11;
					}
					else if (PlayerItems.IsCorpus(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(6) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsShield(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(6) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsEngine(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(8) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsExtraCooldown(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(12) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsExtraCargoMining(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(8) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsExtraDamage(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(13) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsExtraOther(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(15) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					break;
				}
				case 5:
				{
					if (PlayerItems.IsWeapon(slotItem.get_ItemType()))
					{
						int weaponTierByType = this.GetWeaponTierByType(slotItem.get_ItemType());
						item1 = Mineral.enchantsTargetingTable.get_Item(weaponTierByType).min;
						item2 = Mineral.enchantsTargetingTable.get_Item(weaponTierByType).max;
					}
					else if (PlayerItems.IsCorpus(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(16) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsShield(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(16) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsEngine(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(16) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsExtraCooldown(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(13) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsExtraCargoMining(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(15) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsExtraDamage(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(14) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					else if (PlayerItems.IsExtraOther(slotItem.get_ItemType()))
					{
						single = BonusesConstant.bonusConstatn.get_Item(16) * (float)item.levelRestriction;
						item1 = (int)Math.Max(1f, single);
						item2 = (int)Math.Max(1f, single * 3f);
					}
					break;
				}
			}
			if (item1 == 0 && item2 == 0)
			{
				guiLabel.text = "?";
				str.text = "?";
			}
			else if (!this.activeCheckboxes.get_Item(i).Selected)
			{
				guiLabel.text = item1.ToString("##,##0");
				str.text = item2.ToString("##,##0");
			}
			else
			{
				guiLabel.text = item2.ToString("##,##0");
				str.text = item2.ToString("##,##0");
			}
		}
		this.wantedBonusRandom = 0;
		this.wantedBonusOne = 0;
		this.wantedBonusTwo = 0;
		this.wantedBonusThree = 0;
		this.wantedBonusFour = 0;
		this.wantedBonusFive = 0;
		IEnumerator<GuiDropdown> enumerator = this.activeDropdowns.get_Values().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				switch (enumerator.get_Current().selectedItem)
				{
					case 0:
					{
						__MerchantWindow _MerchantWindow = this;
						_MerchantWindow.wantedBonusRandom = (byte)(_MerchantWindow.wantedBonusRandom + 1);
						continue;
					}
					case 1:
					{
						__MerchantWindow _MerchantWindow1 = this;
						_MerchantWindow1.wantedBonusOne = (byte)(_MerchantWindow1.wantedBonusOne + 1);
						continue;
					}
					case 2:
					{
						__MerchantWindow _MerchantWindow2 = this;
						_MerchantWindow2.wantedBonusTwo = (byte)(_MerchantWindow2.wantedBonusTwo + 1);
						continue;
					}
					case 3:
					{
						__MerchantWindow _MerchantWindow3 = this;
						_MerchantWindow3.wantedBonusThree = (byte)(_MerchantWindow3.wantedBonusThree + 1);
						continue;
					}
					case 4:
					{
						__MerchantWindow _MerchantWindow4 = this;
						_MerchantWindow4.wantedBonusFour = (byte)(_MerchantWindow4.wantedBonusFour + 1);
						continue;
					}
					case 5:
					{
						__MerchantWindow _MerchantWindow5 = this;
						_MerchantWindow5.wantedBonusFive = (byte)(_MerchantWindow5.wantedBonusFive + 1);
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
		this.maximaizedBonusRandom = 0;
		this.maximaizedBonusOne = 0;
		this.maximaizedBonusTwo = 0;
		this.maximaizedBonusThree = 0;
		this.maximaizedBonusFour = 0;
		this.maximaizedBonusFive = 0;
		IEnumerator<KeyValuePair<int, GuiCheckbox>> enumerator1 = this.activeCheckboxes.GetEnumerator();
		try
		{
			while (enumerator1.MoveNext())
			{
				KeyValuePair<int, GuiCheckbox> current = enumerator1.get_Current();
				if (!current.get_Value().Selected)
				{
					continue;
				}
				switch (this.activeDropdowns.get_Item(current.get_Key()).selectedItem)
				{
					case 0:
					{
						__MerchantWindow _MerchantWindow6 = this;
						_MerchantWindow6.maximaizedBonusRandom = (byte)(_MerchantWindow6.maximaizedBonusRandom + 1);
						continue;
					}
					case 1:
					{
						__MerchantWindow _MerchantWindow7 = this;
						_MerchantWindow7.maximaizedBonusOne = (byte)(_MerchantWindow7.maximaizedBonusOne + 1);
						continue;
					}
					case 2:
					{
						__MerchantWindow _MerchantWindow8 = this;
						_MerchantWindow8.maximaizedBonusTwo = (byte)(_MerchantWindow8.maximaizedBonusTwo + 1);
						continue;
					}
					case 3:
					{
						__MerchantWindow _MerchantWindow9 = this;
						_MerchantWindow9.maximaizedBonusThree = (byte)(_MerchantWindow9.maximaizedBonusThree + 1);
						continue;
					}
					case 4:
					{
						__MerchantWindow _MerchantWindow10 = this;
						_MerchantWindow10.maximaizedBonusFour = (byte)(_MerchantWindow10.maximaizedBonusFour + 1);
						continue;
					}
					case 5:
					{
						__MerchantWindow _MerchantWindow11 = this;
						_MerchantWindow11.maximaizedBonusFive = (byte)(_MerchantWindow11.maximaizedBonusFive + 1);
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
			if (enumerator1 == null)
			{
			}
			enumerator1.Dispose();
		}
		this.rerollPrice = (double)PlayerItems.GetRerollPrice(slotItem.get_ItemType(), (int)slotItem.get_BonusCnt(), (int)(this.wantedBonusOne + this.wantedBonusTwo + this.wantedBonusThree + this.wantedBonusFour + this.wantedBonusFive), (int)(this.maximaizedBonusOne + this.maximaizedBonusTwo + this.maximaizedBonusThree + this.maximaizedBonusFour + this.maximaizedBonusFive), 1, NetworkScript.player.playerBelongings.factionWarRerollBonus);
		this.rerollPriceWithEQ = (double)PlayerItems.GetRerollPrice(slotItem.get_ItemType(), (int)slotItem.get_BonusCnt(), (int)(this.wantedBonusOne + this.wantedBonusTwo + this.wantedBonusThree + this.wantedBonusFour + this.wantedBonusFive), (int)(this.maximaizedBonusOne + this.maximaizedBonusTwo + this.maximaizedBonusThree + this.maximaizedBonusFour + this.maximaizedBonusFive), 2, NetworkScript.player.playerBelongings.factionWarRerollBonus);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.boundries.set_x(780f);
		guiButtonResizeable.boundries.set_y(305f);
		guiButtonResizeable.boundries.set_width(200f);
		guiButtonResizeable.Caption = string.Format(StaticData.Translate("key_reroll_button"), (int)this.rerollPrice);
		guiButtonResizeable.FontSize = 12;
		guiButtonResizeable.Alignment = 3;
		guiButtonResizeable._marginLeft = 30;
		guiButtonResizeable.textColorDisabled = Color.get_white();
		guiButtonResizeable.SetSmallOrangeTexture();
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.CreateItemRerollDialogue);
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = (SelectedCurrency)1
		};
		guiButtonResizeable.eventHandlerParam = eventHandlerParam;
		guiButtonResizeable.isEnabled = (this.rerollPrice <= 0 ? false : (double)NetworkScript.player.playerBelongings.playerItems.get_Nova() >= this.rerollPrice);
		base.AddGuiElement(guiButtonResizeable);
		this.bonusValuesForDelete.Add(guiButtonResizeable);
		GuiTexture guiTexture = new GuiTexture()
		{
			boundries = new Rect(guiButtonResizeable.boundries.get_x() + 10f, guiButtonResizeable.boundries.get_y() + 3f, 20f, 20f)
		};
		guiTexture.SetTextureKeepSize("NewGUI", "icon_white_nova");
		base.AddGuiElement(guiTexture);
		this.bonusValuesForDelete.Add(guiTexture);
		GuiButtonResizeable _white = new GuiButtonResizeable();
		_white.boundries.set_x(370f);
		_white.boundries.set_y(305f);
		_white.boundries.set_width(200f);
		_white.Caption = string.Format(StaticData.Translate("key_reroll_button"), (int)this.rerollPriceWithEQ);
		_white.FontSize = 12;
		_white.Alignment = 3;
		_white._marginLeft = 30;
		_white.textColorDisabled = Color.get_white();
		_white.SetTexture("NewGUI", "button_purple_small_");
		_white.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.CreateItemRerollDialogue);
		eventHandlerParam = new EventHandlerParam()
		{
			customData = (SelectedCurrency)2
		};
		_white.eventHandlerParam = eventHandlerParam;
		_white.isEnabled = (this.rerollPriceWithEQ <= 0 ? false : (double)NetworkScript.player.playerBelongings.playerItems.get_Equilibrium() >= this.rerollPriceWithEQ);
		base.AddGuiElement(_white);
		this.bonusValuesForDelete.Add(_white);
		GuiTexture guiTexture1 = new GuiTexture()
		{
			boundries = new Rect(_white.boundries.get_x() + 10f, _white.boundries.get_y() + 3f, 20f, 20f)
		};
		guiTexture1.SetTextureKeepSize("NewGUI", "icon_white_equilibrium");
		base.AddGuiElement(guiTexture1);
		this.bonusValuesForDelete.Add(guiTexture1);
		this.CreateAfterSection();
	}

	private void PopulateChangeShipBtn()
	{
		this.btnNextShip.isEnabled = (this.currentShipIndex >= (int)NetworkScript.player.playerBelongings.playerShips.Length - 1 ? false : (int)NetworkScript.player.playerBelongings.playerShips.Length > 1);
		this.btnPreviousShip.isEnabled = (this.currentShipIndex <= 0 ? false : (int)NetworkScript.player.playerBelongings.playerShips.Length > 1);
	}

	public void PopulateCurrentShipStats()
	{
		__MerchantWindow.<PopulateCurrentShipStats>c__AnonStorey92 variable = null;
		int num = 0;
		this.CalculateShipDamage(this.currentShip, out num);
		this.lblDamageVal.text = num.ToString("##,##0");
		this.lblCorpusVal.text = this.currentShip.Corpus.ToString("##,##0");
		this.lblShieldVal.text = this.currentShip.Shield.ToString("##,##0");
		this.lblShieldRegenVal.text = this.currentShip.shieldPower.ToString("##,##0");
		this.lblAvoidanceVal.text = this.currentShip.Avoidance.ToString("##,##0");
		this.lblTargetingVal.text = this.currentShip.Targeting.ToString("##,##0");
		this.lblSpeedVal.text = this.currentShip.Speed.ToString("##,##0");
		this.lblCargoVal.text = this.currentShip.MaxCargo.ToString("##,##0");
		this.currentShipAssetName = Enumerable.First<ShipsTypeNet>(Enumerable.Where<ShipsTypeNet>(StaticData.shipTypes, new Func<ShipsTypeNet, bool>(variable, (ShipsTypeNet w) => w.id == this.<>f__this.currentShip.shipTypeId))).assetName;
		this.txSelectedShip.SetTextureKeepSize("ShipsAvatars", this.currentShipAssetName);
		this.lblCurrentShipName.text = StaticData.Translate(this.currentShip.ShipTitle).ToUpper();
		this.shipTexture.SetTextureKeepSize("CfgMenuBg", this.currentShipAssetName);
		Inventory.ClearSlots(this);
		Inventory.secondaryDropHandler = new Action<AndromedaGuiDragDropPlace, AndromedaGuiDragDropPlace>(this, __MerchantWindow.DropHandler);
		Inventory.closeStackablePopUp = new Action(this, __MerchantWindow.Refresh);
		this.CreateInventorySlots();
		this.CreateMerchantSlots();
		this.CreateWeaponsTab();
		this.CreateStructureTab();
		this.CreateExtrasTab();
		Inventory.DrawPlaces(this);
		if (this.btnSelectShip != null)
		{
			base.RemoveGuiElement(this.btnSelectShip);
		}
		PlayerShipNet playerShipNet = NetworkScript.player.playerBelongings.playerShips[this.currentShipIndex];
		int num1 = Enumerable.First<ShipsTypeNet>(Enumerable.Where<ShipsTypeNet>(StaticData.shipTypes, new Func<ShipsTypeNet, bool>(variable, (ShipsTypeNet t) => t.id == this.ship.shipTypeId))).levelRestriction;
		if (playerShipNet.ShipID != NetworkScript.player.playerBelongings.selectedShipId)
		{
			this.btnSelectShip = new GuiButtonResizeable();
			this.btnSelectShip.SetSmallOrangeTexture();
			this.btnSelectShip.X = 852f;
			this.btnSelectShip.Y = 310f;
			this.btnSelectShip.Width = 125f;
			this.btnSelectShip.Caption = StaticData.Translate("key_dock_my_ships_select_ship");
			this.btnSelectShip.FontSize = 14;
			this.btnSelectShip.MarginTop = 4;
			this.btnSelectShip.isEnabled = num1 <= NetworkScript.player.playerBelongings.playerLevel;
			this.btnSelectShip.Alignment = 1;
			this.btnSelectShip.Clicked = new Action<EventHandlerParam>(this, __MerchantWindow.OnSelectShipClicked);
			base.AddGuiElement(this.btnSelectShip);
			if (!this.btnSelectShip.isEnabled)
			{
				GuiButtonResizeable guiButtonResizeable = this.btnSelectShip;
				EventHandlerParam eventHandlerParam = new EventHandlerParam()
				{
					customData = string.Format(StaticData.Translate("key_tooltip_level_restriction"), num1),
					customData2 = this.btnSelectShip
				};
				guiButtonResizeable.tooltipWindowParam = eventHandlerParam;
				this.btnSelectShip.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			}
		}
	}

	private void PutAdditionalInfo()
	{
		switch (this.selectedOption)
		{
			case __MerchantWindow.MenuOption.Merchant:
			{
				GuiLabel guiLabel = new GuiLabel()
				{
					boundries = new Rect(52f, 560f, 283f, 18f),
					text = StaticData.Translate("key_merchant_bottominfo_merchant"),
					Alignment = 3,
					Font = GuiLabel.FontBold,
					FontSize = 10,
					TextColor = GuiNewStyleBar.blueColor
				};
				base.AddGuiElement(guiLabel);
				this.forDelete.Add(guiLabel);
				break;
			}
			case __MerchantWindow.MenuOption.Gambler:
			{
				GuiCheckbox guiCheckbox = new GuiCheckbox()
				{
					X = 49f,
					Y = 535f,
					Selected = this.isQualityGambleOn,
					OnCheckboxSelected = new Action<bool>(this, __MerchantWindow.OnQualityGambleToogle),
					isActive = true
				};
				base.AddGuiElement(guiCheckbox);
				this.forDelete.Add(guiCheckbox);
				GuiLabel guiLabel1 = new GuiLabel()
				{
					boundries = new Rect(72f, 538f, 263f, 18f),
					text = StaticData.Translate("key_merchant_quality_gamble_lbl"),
					Alignment = 3,
					Font = GuiLabel.FontBold,
					FontSize = 12,
					TextColor = GuiNewStyleBar.orangeColor
				};
				base.AddGuiElement(guiLabel1);
				this.forDelete.Add(guiLabel1);
				GuiLabel guiLabel2 = new GuiLabel()
				{
					boundries = new Rect(52f, 560f, 283f, 18f),
					text = StaticData.Translate("key_merchant_bottominfo_gambler"),
					Alignment = 3,
					Font = GuiLabel.FontBold,
					FontSize = 10,
					TextColor = GuiNewStyleBar.blueColor
				};
				base.AddGuiElement(guiLabel2);
				this.forDelete.Add(guiLabel2);
				break;
			}
		}
	}

	private void Refresh()
	{
		this.Clear();
		this.Create();
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

	public void RefreshScreen()
	{
		this.Clear();
		this.Create();
	}

	private void SelectShipWithEquip(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void __MerchantWindow::SelectShipWithEquip(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void SelectShipWithEquip(EventHandlerParam)
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

	private void SelectShipWithoutEquip(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void __MerchantWindow::SelectShipWithoutEquip(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void SelectShipWithoutEquip(EventHandlerParam)
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

	private void Tab1Clicked(EventHandlerParam parm)
	{
		this.Clear();
		this.selectedMerchantTab = __MerchantWindow.MerchantTab.Shields;
		this.Create();
	}

	private void Tab2Clicked(EventHandlerParam parm)
	{
		this.Clear();
		this.selectedMerchantTab = __MerchantWindow.MerchantTab.Extras;
		this.Create();
	}

	private void Tab3Clicked(EventHandlerParam parm)
	{
		this.Clear();
		this.selectedMerchantTab = __MerchantWindow.MerchantTab.Ammo;
		this.Create();
	}

	private void Tab4Clicked(EventHandlerParam parm)
	{
		this.Clear();
		this.selectedMerchantTab = __MerchantWindow.MerchantTab.Weapons;
		this.Create();
	}

	private void Tab5Clicked(EventHandlerParam parm)
	{
		this.Clear();
		this.selectedMerchantTab = __MerchantWindow.MerchantTab.Corpuses;
		this.Create();
	}

	private void Tab6Clicked(EventHandlerParam parm)
	{
		this.Clear();
		this.selectedMerchantTab = __MerchantWindow.MerchantTab.Engines;
		this.Create();
	}

	private void UpdateSectionLabel(__MerchantWindow.MerchantTab selectedTab)
	{
		string empty = string.Empty;
		switch (this.selectedOption)
		{
			case __MerchantWindow.MenuOption.Merchant:
			{
				empty = StaticData.Translate("key_merchant_lbl_merchant");
				break;
			}
			case __MerchantWindow.MenuOption.Gambler:
			{
				empty = StaticData.Translate("key_merchant_lbl_gambler");
				break;
			}
			case __MerchantWindow.MenuOption.Reroll:
			{
				this.selectedSectionLbl.text = StaticData.Translate("key_merchant_lbl_reroll");
				return;
			}
		}
		switch (selectedTab)
		{
			case __MerchantWindow.MerchantTab.Structures:
			case __MerchantWindow.MerchantTab.Shields:
			{
				this.selectedSectionLbl.text = string.Format("{0} - {1}", empty, StaticData.Translate("key_merdcant_tooltips_shields"));
				break;
			}
			case __MerchantWindow.MerchantTab.Corpuses:
			{
				this.selectedSectionLbl.text = string.Format("{0} - {1}", empty, StaticData.Translate("key_merdcant_tooltips_corpus"));
				break;
			}
			case __MerchantWindow.MerchantTab.Engines:
			{
				this.selectedSectionLbl.text = string.Format("{0} - {1}", empty, StaticData.Translate("key_merdcant_tooltips_engine"));
				break;
			}
			case __MerchantWindow.MerchantTab.Extras:
			{
				this.selectedSectionLbl.text = string.Format("{0} - {1}", empty, StaticData.Translate("key_merdcant_tooltips_extras"));
				break;
			}
			case __MerchantWindow.MerchantTab.Ammo:
			{
				this.selectedSectionLbl.text = string.Format("{0} - {1}", empty, StaticData.Translate("key_merdcant_tooltips_ammo"));
				break;
			}
			case __MerchantWindow.MerchantTab.Weapons:
			{
				this.selectedSectionLbl.text = string.Format("{0} - {1}", empty, StaticData.Translate("key_merdcant_tooltips_weapons"));
				break;
			}
		}
	}

	public enum MenuOption
	{
		Merchant,
		Gambler,
		Reroll
	}

	public enum MerchantInventoryTab
	{
		Inventory,
		Cargo
	}

	public enum MerchantTab
	{
		Structures,
		Shields,
		Corpuses,
		Engines,
		Extras,
		Ammo,
		Weapons
	}

	private class SlotItemWithPrice
	{
		public SlotItem item;

		public int priceCash;

		public int priceNova;

		public int priceViral;

		public SlotItemWithPrice()
		{
		}
	}
}