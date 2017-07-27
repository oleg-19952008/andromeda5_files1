using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class __DockWindow : GuiWindow
{
	private const int MAX_ALLOWED_WEAPON_UPGRADES = 15;

	public int InventoryScrollIndex;

	public int InventoryScrollingDirection;

	public float ScrollTimer;

	private GuiButtonFixed inventoryScrollLeftButton;

	private GuiButtonFixed inventoryScrollRightButton;

	private GuiTexture centerFrame;

	private SlotItem selectedItem;

	private GuiTexture selectedItemTexture;

	private GuiLabel selectedItemName;

	private GuiLabel upgradeLevelLabel;

	private GuiLabel damageLabel;

	private GuiLabel cooldownLabel;

	private GuiLabel rangeLabel;

	private GuiLabel penetrationLabel;

	private GuiLabel targetingLabel;

	private GuiLabel damageValLabel;

	private GuiLabel cooldownValLabel;

	private GuiLabel rangeValLabel;

	private GuiLabel penetrationValLabel;

	private GuiLabel targetingValLabel;

	private GuiButtonResizeable damageMinusButton;

	private GuiButtonResizeable damagePlusButton;

	private GuiButtonResizeable cooldownMinusButton;

	private GuiButtonResizeable cooldownPlusButton;

	private GuiButtonResizeable rangeMinusButton;

	private GuiButtonResizeable rangePlusButton;

	private GuiButtonResizeable penetrationMinusButton;

	private GuiButtonResizeable penetrationPlusButton;

	private GuiButtonResizeable targetingMinusButton;

	private GuiButtonResizeable targetingPlusButton;

	private GuiNewDualColorBar damageBar;

	private GuiNewDualColorBar cooldownBar;

	private GuiNewDualColorBar rangeBar;

	private GuiNewDualColorBar penetrationBar;

	private GuiNewDualColorBar targetingBar;

	private GuiButtonResizeable upgradeButton;

	private GuiTexture txWeaponOnCurrentShip;

	private GuiTexture txWeaponOnOtherShip;

	private GuiLabel lblWeaponOnCurrentShip;

	private GuiLabel lblWeaponOnOtherShip;

	public __DockWindow.DockWindowTab selectedTab;

	private GuiTexture mainScreenBG;

	private GuiTexture myShipsNavigation;

	private GuiTexture tx_centerShip;

	private GuiTexture tx_leftShip;

	private GuiTexture tx_rightShip;

	private GuiLabel lbl_title;

	private GuiLabel lbl_remainingUpgrades;

	private GuiLabel lbl_curentShild;

	private GuiLabel lbl_curentShildValue;

	private GuiLabel lbl_curentHull;

	private GuiLabel lbl_curentHullValue;

	private GuiLabel lbl_shipName;

	private GuiLabel lbl_upgradeLvl;

	private GuiLabel lbl_guideName;

	private GuiLabel lbl_guideAdvice;

	private GuiLabel lbl_shild;

	private GuiLabel lbl_shildValue;

	private GuiLabel lbl_hull;

	private GuiLabel lbl_hullValue;

	private GuiLabel lbl_speed;

	private GuiLabel lbl_speedValue;

	private GuiLabel lbl_avoidance;

	private GuiLabel lbl_avoidanceValue;

	private GuiLabel lbl_targeting;

	private GuiLabel lbl_targetingValue;

	private GuiLabel lbl_cargo;

	private GuiLabel lbl_cargoValue;

	private GuiLabel lblPlasmaSlots;

	private GuiLabel lblLaserSlots;

	private GuiLabel lblIonSlots;

	private GuiButton btnMyShips;

	private GuiButton btnNewShip;

	private GuiButton btnWeapons;

	private GuiButtonFixed btnRightArrow;

	private GuiButtonFixed btnLeftArrow;

	private GuiButtonFixed btnPlus;

	private GuiButtonFixed btnMinus;

	private GuiButtonResizeable btnNewPlus;

	private GuiButtonResizeable btnNewMinus;

	private GuiButtonFixed btnLeftBox;

	private GuiButtonFixed btnRightBox;

	private GuiButtonResizeable btnUpgrade;

	private GuiButtonResizeable btnRepair;

	private GuiNewStyleBar bar_curentShield;

	private GuiNewStyleBar bar_curentHull;

	private GuiNewDualColorBar bar_shield;

	private GuiNewDualColorBar bar_hull;

	private GuiNewDualColorBar bar_speed;

	private GuiNewDualColorBar bar_avoidance;

	private GuiNewDualColorBar bar_targeting;

	private GuiNewDualColorBar bar_cargo;

	private GuiTexture[] remainingUpgrades = new GuiTexture[15];

	private GuiButtonResizeable btnSelectShip;

	private GuiTexture curentShipFlag;

	private List<GuiElement> forDelete = new List<GuiElement>();

	private int displayedShipIndex;

	private int desirableUpgrades;

	private int desirableUpgradesPrice;

	private int repairPrice;

	private PlayerShipNet displayedShip;

	private int shipUpgradeLevel;

	private GuiLabel repairPriceLbl;

	private GuiTextureAnimated repairBtnAnimationLeft;

	private GuiTextureAnimated repairBtnAnimationRight;

	private GuiTextureAnimated repairBtnAnimationMiddle;

	private int exchangeRate;

	private float colorAlpha = 1f;

	private float colorAlphaHelper = -1f;

	private List<GuiElement> weaponSlots;

	private int possitionX = 603;

	private int possitionY = 449;

	private GuiButtonResizeable btnBuyNewShipNova;

	private GuiButtonResizeable btnBuyNewShipCash;

	private GuiLabel levelRestrictionLbl_1;

	private GuiLabel levelRestrictionLbl_2;

	private GuiButtonFixed newShipRightArrow;

	private GuiButtonFixed newShipLeftArrow;

	private GuiButtonFixed newShipRightBox;

	private GuiButtonFixed newShipLeftBox;

	private GuiLabel lblNewShipPriceCash;

	private GuiLabel lblNewShipPriceNova;

	private GuiLabel lblNewShipName;

	private GuiTexture middleShip;

	private GuiTexture leftShip;

	private GuiTexture rightShip;

	private GuiTexture ownershipStamp;

	private GuiLabel lbl_newShip_shildValue;

	private GuiLabel lbl_newShip_hullValue;

	private GuiLabel lbl_newShip_speedValue;

	private GuiLabel lbl_newShip_avoidanceValue;

	private GuiLabel lbl_newShip_targetingValue;

	private GuiLabel lbl_newShip_cargoValue;

	private GuiNewDualColorBar bar_newShip_shield;

	private GuiNewDualColorBar bar_newShip_hull;

	private GuiNewDualColorBar bar_newShip_speed;

	private GuiNewDualColorBar bar_newShip_avoidance;

	private GuiNewDualColorBar bar_newShip_targeting;

	private GuiNewDualColorBar bar_newShip_cargo;

	private int MAX_SHIELD;

	private int MAX_CORPUS;

	private int MAX_AVOIDANCE;

	private int MAX_TARGETING;

	private int MAX_SPEED;

	private int MAX_CARGO;

	private List<GuiElement> newShipForDelete = new List<GuiElement>();

	private int newShipIndex;

	private ShipsTypeNet[] newShips;

	private ShipsTypeNet selectedNewShip;

	private bool isOwnThisShip;

	private GuiTexture cashIcon;

	private GuiTexture novaIcon;

	private GuiLabel lblPriceCash;

	private GuiLabel lblPriceNova;

	private GuiWindow dialogWindow;

	private int lastPurchaseId = -1;

	private bool IsSelectNewShipAllowed
	{
		get
		{
			return (this.lastPurchaseId == NetworkScript.player.playerBelongings.lastPurchaseId ? false : NetworkScript.player.playerBelongings.lastPurchaseId != 0);
		}
	}

	public __DockWindow()
	{
	}

	private void ClearContent()
	{
		this.ClearWeaponSlotsConfig();
		foreach (GuiElement guiElement in this.forDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.forDelete.Clear();
		foreach (GuiElement guiElement1 in this.newShipForDelete)
		{
			base.RemoveGuiElement(guiElement1);
		}
		this.newShipForDelete.Clear();
		switch (this.selectedTab)
		{
			case __DockWindow.DockWindowTab.NewShips:
			{
				break;
			}
			case __DockWindow.DockWindowTab.Weapons:
			{
				this.ClearWeapons();
				break;
			}
		}
	}

	private void ClearWeapons()
	{
		base.RemoveGuiElement(this.centerFrame);
		base.RemoveGuiElement(this.selectedItemName);
		base.RemoveGuiElement(this.selectedItemTexture);
		base.RemoveGuiElement(this.inventoryScrollLeftButton);
		base.RemoveGuiElement(this.inventoryScrollRightButton);
		base.RemoveGuiElement(this.upgradeButton);
		base.RemoveGuiElement(this.upgradeLevelLabel);
		base.RemoveGuiElement(this.damageLabel);
		base.RemoveGuiElement(this.cooldownLabel);
		base.RemoveGuiElement(this.rangeLabel);
		base.RemoveGuiElement(this.penetrationLabel);
		base.RemoveGuiElement(this.targetingLabel);
		base.RemoveGuiElement(this.damageMinusButton);
		base.RemoveGuiElement(this.damagePlusButton);
		base.RemoveGuiElement(this.cooldownMinusButton);
		base.RemoveGuiElement(this.cooldownPlusButton);
		base.RemoveGuiElement(this.rangeMinusButton);
		base.RemoveGuiElement(this.rangePlusButton);
		base.RemoveGuiElement(this.penetrationMinusButton);
		base.RemoveGuiElement(this.penetrationPlusButton);
		base.RemoveGuiElement(this.targetingMinusButton);
		base.RemoveGuiElement(this.targetingPlusButton);
		base.RemoveGuiElement(this.damageBar);
		base.RemoveGuiElement(this.cooldownBar);
		base.RemoveGuiElement(this.rangeBar);
		base.RemoveGuiElement(this.penetrationBar);
		base.RemoveGuiElement(this.targetingBar);
		base.RemoveGuiElement(this.damageValLabel);
		base.RemoveGuiElement(this.cooldownValLabel);
		base.RemoveGuiElement(this.rangeValLabel);
		base.RemoveGuiElement(this.penetrationValLabel);
		base.RemoveGuiElement(this.targetingValLabel);
		base.RemoveGuiElement(this.txWeaponOnCurrentShip);
		base.RemoveGuiElement(this.txWeaponOnOtherShip);
		base.RemoveGuiElement(this.lblWeaponOnCurrentShip);
		base.RemoveGuiElement(this.lblWeaponOnOtherShip);
	}

	private void ClearWeaponSlotsConfig()
	{
		this.lblLaserSlots.text = string.Empty;
		this.lblPlasmaSlots.text = string.Empty;
		this.lblIonSlots.text = string.Empty;
		foreach (GuiElement weaponSlot in this.weaponSlots)
		{
			base.RemoveGuiElement(weaponSlot);
		}
		this.weaponSlots.Clear();
	}

	private int CountNewUpgrades()
	{
		if (this.selectedItem == null)
		{
			return 0;
		}
		return (int)(this.damageBar.ValueTwo + this.cooldownBar.ValueTwo + this.rangeBar.ValueTwo + this.penetrationBar.ValueTwo + this.targetingBar.ValueTwo);
	}

	private int CountUpgrades()
	{
		if (this.selectedItem == null)
		{
			return 0;
		}
		return (int)(this.damageBar.ValueOne + this.cooldownBar.ValueOne + this.rangeBar.ValueOne + this.penetrationBar.ValueOne + this.targetingBar.ValueOne);
	}

	private int CountWeapons()
	{
		List<SlotItem> list = NetworkScript.player.playerBelongings.playerItems.slotItems;
		if (__DockWindow.<>f__am$cache9C == null)
		{
			__DockWindow.<>f__am$cache9C = new Func<SlotItem, bool>(null, (SlotItem t) => PlayerItems.IsWeapon(t.get_ItemType()));
		}
		return Enumerable.Count<SlotItem>(Enumerable.Where<SlotItem>(list, __DockWindow.<>f__am$cache9C));
	}

	public override void Create()
	{
		int num = NetworkScript.player.playerBelongings.playerLevel;
		this.exchangeRate = (int)(Math.Ceiling(0.0016 * (double)(num * num) + (double)num) * 5);
		int num1 = 0;
		while (num1 < (int)NetworkScript.player.playerBelongings.playerShips.Length)
		{
			if (NetworkScript.player.playerBelongings.playerShips[num1].ShipID != NetworkScript.player.playerBelongings.selectedShipId)
			{
				num1++;
			}
			else
			{
				this.displayedShipIndex = num1;
				break;
			}
		}
		ShipsTypeNet[] shipsTypeNetArray = StaticData.shipTypes;
		if (__DockWindow.<>f__am$cache9F == null)
		{
			__DockWindow.<>f__am$cache9F = new Func<ShipsTypeNet, bool>(null, (ShipsTypeNet s) => s.upgrade == 0);
		}
		IEnumerable<ShipsTypeNet> enumerable = Enumerable.Where<ShipsTypeNet>(shipsTypeNetArray, __DockWindow.<>f__am$cache9F);
		if (__DockWindow.<>f__am$cacheA0 == null)
		{
			__DockWindow.<>f__am$cacheA0 = new Func<ShipsTypeNet, int>(null, (ShipsTypeNet t) => t.price);
		}
		this.newShips = Enumerable.ToArray<ShipsTypeNet>(Enumerable.OrderBy<ShipsTypeNet, int>(enumerable, __DockWindow.<>f__am$cacheA0));
		int selectedShip = NetworkScript.player.playerBelongings.get_SelectedShip().shipTypeId;
		for (int i = 0; i < (int)this.newShips.Length; i++)
		{
			if (selectedShip >= this.newShips[i].id && selectedShip <= this.newShips[i].id + 10)
			{
				this.newShipIndex = i;
			}
		}
		this.weaponSlots = new List<GuiElement>();
		base.SetBackgroundTexture("NewGUI", "novashop_frame");
		base.PutToCenter();
		this.zOrder = 210;
		this.isHidden = false;
		this.mainScreenBG = new GuiTexture()
		{
			boundries = new Rect(0f, 0f, 904f, 539f)
		};
		this.mainScreenBG.SetTextureKeepSize("NewGUI", "DockWindowTab1");
		base.AddGuiElement(this.mainScreenBG);
		this.lblLaserSlots = new GuiLabel()
		{
			boundries = new Rect(606f, 422f, 80f, 30f),
			text = string.Empty,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.lblLaserSlots);
		this.lblPlasmaSlots = new GuiLabel()
		{
			boundries = new Rect(696f, 422f, 80f, 30f),
			text = string.Empty,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.lblPlasmaSlots);
		this.lblIonSlots = new GuiLabel()
		{
			boundries = new Rect(786f, 422f, 80f, 30f),
			text = string.Empty,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.lblIonSlots);
		this.lbl_title = new GuiLabel()
		{
			boundries = new Rect(0f, 42f, 904f, 30f),
			text = StaticData.Translate("key_dock_my_ships_dock").ToUpper(),
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 18,
			Font = GuiLabel.FontBold,
			Alignment = 1
		};
		base.AddGuiElement(this.lbl_title);
		this.btnNewShip = new GuiButton()
		{
			boundries = new Rect(34f, 52f, 135f, 70f),
			Alignment = 4,
			Caption = StaticData.Translate("key_dock_my_ships_new_ship").ToUpper(),
			textColorClick = GuiNewStyleBar.orangeColor,
			textColorNormal = GuiNewStyleBar.blueColor,
			textColorHover = Color.get_white(),
			FontSize = 12,
			textColorDisabled = Color.get_grey(),
			Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnNewShipClick),
			behaviourKeepClicked = true,
			IsClicked = true
		};
		base.AddGuiElement(this.btnNewShip);
		this.btnWeapons = new GuiButton()
		{
			boundries = new Rect(179f, 52f, 135f, 70f),
			Alignment = 4,
			Caption = StaticData.Translate("key_dock_my_ships_weapon_upgrade").ToUpper(),
			textColorClick = GuiNewStyleBar.orangeColor,
			textColorNormal = GuiNewStyleBar.blueColor,
			textColorHover = Color.get_white(),
			FontSize = 12,
			Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnWeaponsClick),
			behaviourKeepClicked = true
		};
		base.AddGuiElement(this.btnWeapons);
	}

	private void CreateInventorySlots()
	{
		int num = this.CountWeapons();
		List<SlotItem> list = NetworkScript.player.playerBelongings.playerItems.slotItems;
		if (__DockWindow.<>f__am$cache9D == null)
		{
			__DockWindow.<>f__am$cache9D = new Func<SlotItem, bool>(null, (SlotItem t) => PlayerItems.IsWeapon(t.get_ItemType()));
		}
		IEnumerable<SlotItem> enumerable = Enumerable.Where<SlotItem>(list, __DockWindow.<>f__am$cache9D);
		if (__DockWindow.<>f__am$cache9E == null)
		{
			__DockWindow.<>f__am$cache9E = new Func<SlotItem, byte>(null, (SlotItem s) => s.get_SlotType());
		}
		SlotItem[] array = Enumerable.ToArray<SlotItem>(Enumerable.OrderByDescending<SlotItem, byte>(enumerable, __DockWindow.<>f__am$cache9E));
		int inventoryScrollIndex = 0;
		int num1 = 0;
		inventoryScrollIndex = this.InventoryScrollIndex;
		num1 = num;
		float single = 0f;
		int num2 = inventoryScrollIndex;
		int num3 = 0;
		while (num2 < num1)
		{
			if (num3 >= 6)
			{
				single = 371f;
				if (num3 >= 12)
				{
					break;
				}
			}
			if (num2 >= Enumerable.Count<SlotItem>(array))
			{
				GuiTexture guiTexture = new GuiTexture();
				guiTexture.SetTexture("NewGUI", "InventorySlotIdle");
				guiTexture.X = single + 55f + (float)(num3 / 3 * 57);
				guiTexture.Y = (float)(162 + num3 % 3 * 52);
				base.AddGuiElement(guiTexture);
				this.forDelete.Add(guiTexture);
			}
			else
			{
				GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
				guiButtonFixed.SetTexture("ConfigWindow", "InventorySlot");
				guiButtonFixed.X = single + 109f + (float)(num3 / 2 * 56);
				guiButtonFixed.Y = (float)(203 + num3 % 2 * 56);
				guiButtonFixed.Caption = string.Empty;
				guiButtonFixed.behaviourKeepClicked = true;
				guiButtonFixed.groupId = 145;
				guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnItemSelected);
				guiButtonFixed.eventHandlerParam.customData = array[num2];
				base.AddGuiElement(guiButtonFixed);
				this.forDelete.Add(guiButtonFixed);
				if (num3 == 0)
				{
					this.selectedItem = array[num2];
				}
				GuiTexture rect = new GuiTexture();
				rect.SetItemTexture(array[num2].get_ItemType());
				rect.boundries = new Rect(single + 109f + (float)(num3 / 2 * 56), (float)(208 + num3 % 2 * 56), 51f, 35f);
				EventHandlerParam eventHandlerParam = new EventHandlerParam()
				{
					customData = array[num2],
					customData2 = rect.boundries
				};
				rect.tooltipWindowParam = eventHandlerParam;
				rect.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.WeaponsTooltip);
				base.AddGuiElement(rect);
				this.forDelete.Add(rect);
				if (array[num2].get_SlotType() != 1)
				{
					string str = (array[num2].get_ShipId() != NetworkScript.player.playerBelongings.selectedShipId ? "weaponOnBoardBlue" : "weaponOnBoardOrange");
					GuiTexture guiTexture1 = new GuiTexture();
					guiTexture1.SetTexture("NewGUI", str);
					guiTexture1.boundries = new Rect(single + 112f + (float)(num3 / 2 * 56), (float)(230 + num3 % 2 * 56), 20f, 20f);
					base.AddGuiElement(guiTexture1);
					this.forDelete.Add(guiTexture1);
				}
			}
			num2++;
			num3++;
		}
	}

	private void CreateNewShipDialog()
	{
		this.dialogWindow = new GuiWindow()
		{
			isModal = true
		};
		this.dialogWindow.SetBackgroundTexture("ConfigWindow", "proba");
		this.dialogWindow.isHidden = false;
		this.dialogWindow.zOrder = 220;
		this.dialogWindow.PutToCenter();
		AndromedaGui.gui.AddWindow(this.dialogWindow);
		AndromedaGui.gui.activeToolTipId = this.dialogWindow.handler;
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(this.dialogWindow.boundries.get_width() * 0.05f, 40f, this.dialogWindow.boundries.get_width() * 0.9f, 70f),
			text = StaticData.Translate("key_dock_my_ships_select_ship_question").ToUpper(),
			Alignment = 1,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 18
		};
		this.dialogWindow.AddGuiElement(guiLabel);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("ConfigWindow", "X_btn_");
		guiButtonFixed.X = 432f;
		guiButtonFixed.Y = 12f;
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, __DockWindow.DialogClose);
		this.dialogWindow.AddGuiElement(guiButtonFixed);
		GuiButtonFixed upper = new GuiButtonFixed();
		upper.SetTexture("ConfigWindow", "testBtn");
		upper.X = 50f;
		upper.Y = 125f;
		upper.Caption = StaticData.Translate("key_dock_my_ships_select_ship_yes").ToUpper();
		upper.Alignment = 4;
		upper.isEnabled = true;
		upper.eventHandlerParam.customData = (SelectedCurrency)0;
		upper.Clicked = new Action<EventHandlerParam>(this, __DockWindow.DialogYesBtnClicked);
		this.dialogWindow.AddGuiElement(upper);
		GuiButtonFixed action = new GuiButtonFixed();
		action.SetTexture("ConfigWindow", "testBtn");
		action.X = 290f;
		action.Y = 125f;
		action.Caption = StaticData.Translate("key_dock_my_ships_select_ship_no").ToUpper();
		action.Alignment = 4;
		action.isEnabled = true;
		action.eventHandlerParam.customData = (SelectedCurrency)1;
		action.Clicked = new Action<EventHandlerParam>(this, __DockWindow.DialogNoBtnClicked);
		this.dialogWindow.AddGuiElement(action);
	}

	private void CreateNewShipTab()
	{
		ShipsTypeNet[] shipsTypeNetArray = StaticData.shipTypes;
		if (__DockWindow.<>f__am$cacheA2 == null)
		{
			__DockWindow.<>f__am$cacheA2 = new Func<ShipsTypeNet, bool>(null, (ShipsTypeNet s) => s.upgrade == 0);
		}
		IEnumerable<ShipsTypeNet> enumerable = Enumerable.Where<ShipsTypeNet>(shipsTypeNetArray, __DockWindow.<>f__am$cacheA2);
		if (__DockWindow.<>f__am$cacheA3 == null)
		{
			__DockWindow.<>f__am$cacheA3 = new Func<ShipsTypeNet, int>(null, (ShipsTypeNet t) => t.price);
		}
		this.newShips = Enumerable.ToArray<ShipsTypeNet>(Enumerable.OrderBy<ShipsTypeNet, int>(enumerable, __DockWindow.<>f__am$cacheA3));
		this.selectedNewShip = this.newShips[this.newShipIndex];
		ShipsTypeNet[] shipsTypeNetArray1 = this.newShips;
		for (int i = 0; i < (int)shipsTypeNetArray1.Length; i++)
		{
			ShipsTypeNet shipsTypeNet = shipsTypeNetArray1[i];
			if (shipsTypeNet.shield > this.MAX_SHIELD)
			{
				this.MAX_SHIELD = shipsTypeNet.shield;
			}
			if (shipsTypeNet.corpus > this.MAX_CORPUS)
			{
				this.MAX_CORPUS = shipsTypeNet.corpus;
			}
			if (shipsTypeNet.targeting > this.MAX_TARGETING)
			{
				this.MAX_TARGETING = shipsTypeNet.targeting;
			}
			if (shipsTypeNet.cargo > this.MAX_CARGO)
			{
				this.MAX_CARGO = shipsTypeNet.cargo;
			}
			if (shipsTypeNet.avoidance > this.MAX_AVOIDANCE)
			{
				this.MAX_AVOIDANCE = shipsTypeNet.avoidance;
			}
			if (shipsTypeNet.speed > this.MAX_SPEED)
			{
				this.MAX_SPEED = shipsTypeNet.speed;
			}
		}
		GuiTexture guiTexture = new GuiTexture()
		{
			boundries = new Rect(36f, 137f, 847f, 384f)
		};
		guiTexture.SetTextureKeepSize("NewGUI", "inbase_frame_3parts");
		base.AddGuiElement(guiTexture);
		this.newShipForDelete.Add(guiTexture);
		this.newShipLeftArrow = new GuiButtonFixed();
		this.newShipLeftArrow.SetTexture("NewGUI", "inbase_vertscroll_left_");
		this.newShipLeftArrow.X = 48f;
		this.newShipLeftArrow.Y = 214f;
		this.newShipLeftArrow.Caption = string.Empty;
		this.newShipLeftArrow.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnNewShipLeftClicked);
		this.newShipLeftArrow.SetLeftClickSound("Sounds", "minus");
		base.AddGuiElement(this.newShipLeftArrow);
		this.newShipForDelete.Add(this.newShipLeftArrow);
		this.newShipRightArrow = new GuiButtonFixed();
		this.newShipRightArrow.SetTexture("NewGUI", "inbase_vertscroll_right_");
		this.newShipRightArrow.X = 837f;
		this.newShipRightArrow.Y = 214f;
		this.newShipRightArrow.Caption = string.Empty;
		this.newShipRightArrow.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnNewShipRightClicked);
		this.newShipRightArrow.SetLeftClickSound("Sounds", "plus");
		base.AddGuiElement(this.newShipRightArrow);
		this.newShipForDelete.Add(this.newShipRightArrow);
		this.newShipLeftBox = new GuiButtonFixed();
		this.newShipLeftBox.SetTexture("FrameworkGUI", "empty");
		this.newShipLeftBox.boundries = new Rect(101f, 192f, 177f, 140f);
		this.newShipLeftBox.Caption = string.Empty;
		this.newShipLeftBox.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnNewShipLeftClicked);
		this.newShipLeftBox.SetLeftClickSound("Sounds", "minus");
		base.AddGuiElement(this.newShipLeftBox);
		this.newShipForDelete.Add(this.newShipLeftBox);
		this.newShipRightBox = new GuiButtonFixed();
		this.newShipRightBox.SetTexture("FrameworkGUI", "empty");
		this.newShipRightBox.boundries = new Rect(641f, 192f, 177f, 140f);
		this.newShipRightBox.Caption = string.Empty;
		this.newShipRightBox.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnNewShipRightClicked);
		this.newShipRightBox.SetLeftClickSound("Sounds", "plus");
		base.AddGuiElement(this.newShipRightBox);
		this.newShipForDelete.Add(this.newShipRightBox);
		this.lblNewShipName = new GuiLabel()
		{
			boundries = new Rect(299f, 110f, 322f, 24f),
			text = StaticData.Translate(this.selectedNewShip.shipName),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			Alignment = 4
		};
		base.AddGuiElement(this.lblNewShipName);
		this.newShipForDelete.Add(this.lblNewShipName);
		this.leftShip = new GuiTexture()
		{
			boundries = new Rect(100f, 200f, 177f, 121f)
		};
		this.leftShip.SetTextureKeepSize("FrameworkGUI", "empty");
		base.AddGuiElement(this.leftShip);
		this.newShipForDelete.Add(this.leftShip);
		this.middleShip = new GuiTexture()
		{
			boundries = new Rect(359f, 144f, 204f, 140f)
		};
		this.middleShip.SetTextureKeepSize("FrameworkGUI", "empty");
		base.AddGuiElement(this.middleShip);
		this.newShipForDelete.Add(this.middleShip);
		this.rightShip = new GuiTexture()
		{
			boundries = new Rect(640f, 200f, 177f, 121f)
		};
		this.rightShip.SetTextureKeepSize("FrameworkGUI", "empty");
		base.AddGuiElement(this.rightShip);
		this.newShipForDelete.Add(this.rightShip);
		this.PopulateShipsGUINew();
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
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, __DockWindow.SelectShipWithEquip);
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
		upper.Clicked = new Action<EventHandlerParam>(this, __DockWindow.SelectShipWithoutEquip);
		this.dialogWindow.AddGuiElement(upper);
	}

	private void CreateUpgradeBars()
	{
		this.upgradeButton = new GuiButtonResizeable();
		this.upgradeButton.SetSmallBlueTexture();
		this.upgradeButton.MarginTop = 5;
		this.upgradeButton.Alignment = 1;
		this.upgradeButton.Y = 447f;
		this.upgradeButton.X = 650f;
		this.upgradeButton.Width = 194f;
		this.upgradeButton.Caption = string.Format(StaticData.Translate("key_dock_weapons_upgrade_price"), 0);
		this.upgradeButton.FontSize = 12;
		this.upgradeButton.isEnabled = false;
		this.upgradeButton.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnUpgrade);
		base.AddGuiElement(this.upgradeButton);
		this.upgradeLevelLabel = new GuiLabel()
		{
			boundries = new Rect(362f, 460f, 194f, 17f),
			text = StaticData.Translate("key_dock_weapons_upgrade_available").ToUpper(),
			Alignment = 1,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(this.upgradeLevelLabel);
		for (int i = 0; i < 15; i++)
		{
			this.remainingUpgrades[i] = new GuiTexture();
			this.remainingUpgrades[i].SetTexture("NewGUI", "dot_empty");
			this.remainingUpgrades[i].X = (float)(353 + i * 14);
			this.remainingUpgrades[i].Y = 485f;
			base.AddGuiElement(this.remainingUpgrades[i]);
			this.forDelete.Add(this.remainingUpgrades[i]);
		}
		this.damageLabel = new GuiLabel()
		{
			boundries = new Rect(366f, 250f, 185f, 14f),
			Alignment = 3,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = StaticData.Translate("key_dock_weapons_upgrade_dmg").ToUpper()
		};
		base.AddGuiElement(this.damageLabel);
		this.cooldownLabel = new GuiLabel()
		{
			boundries = new Rect(366f, 290f, 185f, 14f),
			Alignment = 3,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = StaticData.Translate("key_dock_weapons_upgrade_cooldown").ToUpper()
		};
		base.AddGuiElement(this.cooldownLabel);
		this.rangeLabel = new GuiLabel()
		{
			boundries = new Rect(366f, 330f, 185f, 14f),
			Alignment = 3,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = StaticData.Translate("key_dock_weapons_upgrade_range").ToUpper()
		};
		base.AddGuiElement(this.rangeLabel);
		this.penetrationLabel = new GuiLabel()
		{
			boundries = new Rect(366f, 370f, 185f, 14f),
			Alignment = 3,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = StaticData.Translate("key_dock_weapons_upgrade_penetration").ToUpper()
		};
		base.AddGuiElement(this.penetrationLabel);
		this.targetingLabel = new GuiLabel()
		{
			boundries = new Rect(366f, 410f, 185f, 14f),
			Alignment = 3,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			text = StaticData.Translate("key_dock_weapons_upgrade_targeting").ToUpper()
		};
		base.AddGuiElement(this.targetingLabel);
		this.damageMinusButton = new GuiButtonResizeable();
		this.damageMinusButton.SetSmallOrangeTexture();
		this.damageMinusButton.Caption = "-";
		this.damageMinusButton._marginLeft = -3;
		this.damageMinusButton.MarginTop = -2;
		this.damageMinusButton.Alignment = 4;
		this.damageMinusButton.FontSize = 22;
		this.damageMinusButton.Y = 252f;
		this.damageMinusButton.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnDamageDecrease);
		this.damageMinusButton.SetLeftClickSound("Sounds", "minus");
		this.cooldownMinusButton = new GuiButtonResizeable();
		this.cooldownMinusButton.SetSmallOrangeTexture();
		this.cooldownMinusButton.Caption = "-";
		this.cooldownMinusButton._marginLeft = -3;
		this.cooldownMinusButton.MarginTop = -2;
		this.cooldownMinusButton.Alignment = 4;
		this.cooldownMinusButton.FontSize = 22;
		this.cooldownMinusButton.Y = 292f;
		this.cooldownMinusButton.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnCooldownDecrease);
		this.cooldownMinusButton.SetLeftClickSound("Sounds", "minus");
		this.rangeMinusButton = new GuiButtonResizeable();
		this.rangeMinusButton.SetSmallOrangeTexture();
		this.rangeMinusButton.Caption = "-";
		this.rangeMinusButton._marginLeft = -3;
		this.rangeMinusButton.MarginTop = -2;
		this.rangeMinusButton.Alignment = 4;
		this.rangeMinusButton.FontSize = 22;
		this.rangeMinusButton.Y = 332f;
		this.rangeMinusButton.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnRangeDecrease);
		this.rangeMinusButton.SetLeftClickSound("Sounds", "minus");
		this.penetrationMinusButton = new GuiButtonResizeable();
		this.penetrationMinusButton.SetSmallOrangeTexture();
		this.penetrationMinusButton.Caption = "-";
		this.penetrationMinusButton._marginLeft = -3;
		this.penetrationMinusButton.MarginTop = -2;
		this.penetrationMinusButton.Alignment = 4;
		this.penetrationMinusButton.FontSize = 22;
		this.penetrationMinusButton.Y = 372f;
		this.penetrationMinusButton.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnPenetrationDecrease);
		this.penetrationMinusButton.SetLeftClickSound("Sounds", "minus");
		this.targetingMinusButton = new GuiButtonResizeable();
		this.targetingMinusButton.SetSmallOrangeTexture();
		this.targetingMinusButton.Caption = "-";
		this.targetingMinusButton._marginLeft = -3;
		this.targetingMinusButton.MarginTop = -2;
		this.targetingMinusButton.Alignment = 4;
		this.targetingMinusButton.FontSize = 22;
		this.targetingMinusButton.Y = 412f;
		this.targetingMinusButton.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnTargetingDecrease);
		this.targetingMinusButton.SetLeftClickSound("Sounds", "minus");
		this.damagePlusButton = new GuiButtonResizeable();
		this.damagePlusButton.SetSmallOrangeTexture();
		this.damagePlusButton.Caption = "+";
		this.damagePlusButton._marginLeft = -3;
		this.damagePlusButton.MarginTop = -2;
		this.damagePlusButton.Alignment = 4;
		this.damagePlusButton.FontSize = 22;
		this.damagePlusButton.X = 556f;
		this.damagePlusButton.Y = 252f;
		this.damagePlusButton.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnDamageIncrease);
		this.damagePlusButton.SetLeftClickSound("Sounds", "plus");
		this.cooldownPlusButton = new GuiButtonResizeable();
		this.cooldownPlusButton.SetSmallOrangeTexture();
		this.cooldownPlusButton.Caption = "+";
		this.cooldownPlusButton._marginLeft = -3;
		this.cooldownPlusButton.MarginTop = -2;
		this.cooldownPlusButton.Alignment = 4;
		this.cooldownPlusButton.FontSize = 22;
		this.cooldownPlusButton.X = 556f;
		this.cooldownPlusButton.Y = 292f;
		this.cooldownPlusButton.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnCooldownIncrease);
		this.cooldownPlusButton.SetLeftClickSound("Sounds", "plus");
		this.rangePlusButton = new GuiButtonResizeable();
		this.rangePlusButton.SetSmallOrangeTexture();
		this.rangePlusButton.Caption = "+";
		this.rangePlusButton._marginLeft = -3;
		this.rangePlusButton.MarginTop = -2;
		this.rangePlusButton.Alignment = 4;
		this.rangePlusButton.FontSize = 22;
		this.rangePlusButton.X = 556f;
		this.rangePlusButton.Y = 332f;
		this.rangePlusButton.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnRangeIncrease);
		this.rangePlusButton.SetLeftClickSound("Sounds", "plus");
		this.penetrationPlusButton = new GuiButtonResizeable();
		this.penetrationPlusButton.SetSmallOrangeTexture();
		this.penetrationPlusButton.Caption = "+";
		this.penetrationPlusButton._marginLeft = -3;
		this.penetrationPlusButton.MarginTop = -2;
		this.penetrationPlusButton.Alignment = 4;
		this.penetrationPlusButton.FontSize = 22;
		this.penetrationPlusButton.X = 556f;
		this.penetrationPlusButton.Y = 372f;
		this.penetrationPlusButton.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnPenetrationIncrease);
		this.penetrationPlusButton.SetLeftClickSound("Sounds", "plus");
		this.targetingPlusButton = new GuiButtonResizeable();
		this.targetingPlusButton.SetSmallOrangeTexture();
		this.targetingPlusButton.Caption = "+";
		this.targetingPlusButton._marginLeft = -3;
		this.targetingPlusButton.MarginTop = -2;
		this.targetingPlusButton.Alignment = 4;
		this.targetingPlusButton.FontSize = 22;
		this.targetingPlusButton.X = 556f;
		this.targetingPlusButton.Y = 412f;
		this.targetingPlusButton.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnTargetingIncrease);
		this.targetingPlusButton.SetLeftClickSound("Sounds", "plus");
		this.damageMinusButton.Width = 33f;
		this.damageMinusButton.X = 328f;
		this.cooldownMinusButton.Width = 33f;
		this.cooldownMinusButton.X = 328f;
		this.rangeMinusButton.Width = 33f;
		this.rangeMinusButton.X = 328f;
		this.penetrationMinusButton.Width = 33f;
		this.penetrationMinusButton.X = 328f;
		this.targetingMinusButton.Width = 33f;
		this.targetingMinusButton.X = 328f;
		this.damagePlusButton.Width = 33f;
		this.cooldownPlusButton.Width = 33f;
		this.rangePlusButton.Width = 33f;
		this.penetrationPlusButton.Width = 33f;
		this.targetingPlusButton.Width = 33f;
		base.AddGuiElement(this.damageMinusButton);
		base.AddGuiElement(this.cooldownMinusButton);
		base.AddGuiElement(this.rangeMinusButton);
		base.AddGuiElement(this.penetrationMinusButton);
		base.AddGuiElement(this.targetingMinusButton);
		base.AddGuiElement(this.damagePlusButton);
		base.AddGuiElement(this.cooldownPlusButton);
		base.AddGuiElement(this.rangePlusButton);
		base.AddGuiElement(this.penetrationPlusButton);
		base.AddGuiElement(this.targetingPlusButton);
		this.damageBar = new GuiNewDualColorBar()
		{
			ValueOne = 0f,
			ValueTwo = 0f,
			Max = 10f,
			X = 364f,
			Y = 263f,
			Width = 189f
		};
		base.AddGuiElement(this.damageBar);
		this.cooldownBar = new GuiNewDualColorBar()
		{
			ValueOne = 0f,
			ValueTwo = 0f,
			Max = 10f,
			X = 364f,
			Y = 304f,
			Width = 189f
		};
		base.AddGuiElement(this.cooldownBar);
		this.rangeBar = new GuiNewDualColorBar()
		{
			ValueOne = 0f,
			ValueTwo = 0f,
			Max = 10f,
			X = 364f,
			Y = 344f,
			Width = 190f
		};
		base.AddGuiElement(this.rangeBar);
		this.penetrationBar = new GuiNewDualColorBar()
		{
			ValueOne = 0f,
			ValueTwo = 0f,
			Max = 10f,
			X = 364f,
			Y = 384f,
			Width = 190f
		};
		base.AddGuiElement(this.penetrationBar);
		this.targetingBar = new GuiNewDualColorBar()
		{
			ValueOne = 0f,
			ValueTwo = 0f,
			Max = 10f,
			X = 364f,
			Y = 424f,
			Width = 190f
		};
		base.AddGuiElement(this.targetingBar);
		this.damageValLabel = new GuiLabel()
		{
			boundries = new Rect(366f, 250f, 185f, 14f),
			text = string.Empty,
			Alignment = 5,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(this.damageValLabel);
		this.cooldownValLabel = new GuiLabel()
		{
			boundries = new Rect(366f, 290f, 185f, 14f),
			text = string.Empty,
			Alignment = 5,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(this.cooldownValLabel);
		this.rangeValLabel = new GuiLabel()
		{
			boundries = new Rect(366f, 330f, 185f, 14f),
			text = string.Empty,
			Alignment = 5,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(this.rangeValLabel);
		this.penetrationValLabel = new GuiLabel()
		{
			boundries = new Rect(366f, 370f, 185f, 14f),
			text = string.Empty,
			Alignment = 5,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(this.penetrationValLabel);
		this.targetingValLabel = new GuiLabel()
		{
			boundries = new Rect(366f, 410f, 185f, 14f),
			text = string.Empty,
			Alignment = 5,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(this.targetingValLabel);
		this.RefreshInventoryScrollButtons();
	}

	private void CreateWeapons()
	{
		this.centerFrame = new GuiTexture()
		{
			boundries = new Rect(36f, 137f, 847f, 384f)
		};
		this.centerFrame.SetTextureKeepSize("NewGUI", "inbase_frame_3parts");
		base.AddGuiElement(this.centerFrame);
		this.selectedItemName = new GuiLabel()
		{
			boundries = new Rect(299f, 111f, 320f, 24f),
			text = StaticData.Translate("key_dock_weapons_upgrade_no_selection"),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			Alignment = 4
		};
		base.AddGuiElement(this.selectedItemName);
		this.selectedItemTexture = new GuiTexture()
		{
			boundries = new Rect(378f, 140f, 160f, 110f)
		};
		this.selectedItemTexture.SetTextureKeepSize("FrameworkGUI", "empty");
		base.AddGuiElement(this.selectedItemTexture);
		this.inventoryScrollLeftButton = new GuiButtonFixed();
		this.inventoryScrollLeftButton.SetTexture("NewGUI", "inbase_vertscroll_left_");
		this.inventoryScrollLeftButton.X = 48f;
		this.inventoryScrollLeftButton.Y = 214f;
		this.inventoryScrollLeftButton.Caption = string.Empty;
		this.inventoryScrollLeftButton.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnScrollInventoryLeft);
		this.inventoryScrollLeftButton.SetLeftClickSound("Sounds", "minus");
		base.AddGuiElement(this.inventoryScrollLeftButton);
		this.inventoryScrollRightButton = new GuiButtonFixed();
		this.inventoryScrollRightButton.SetTexture("NewGUI", "inbase_vertscroll_right_");
		this.inventoryScrollRightButton.X = 837f;
		this.inventoryScrollRightButton.Y = 214f;
		this.inventoryScrollRightButton.Caption = string.Empty;
		this.inventoryScrollRightButton.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnScrollInventoryRight);
		this.inventoryScrollRightButton.SetLeftClickSound("Sounds", "plus");
		base.AddGuiElement(this.inventoryScrollRightButton);
		this.txWeaponOnCurrentShip = new GuiTexture()
		{
			boundries = new Rect(65f, 429f, 25f, 25f)
		};
		this.txWeaponOnCurrentShip.SetTextureKeepSize("NewGUI", "weaponOnBoardOrange");
		base.AddGuiElement(this.txWeaponOnCurrentShip);
		this.txWeaponOnOtherShip = new GuiTexture()
		{
			boundries = new Rect(65f, 466f, 25f, 25f)
		};
		this.txWeaponOnOtherShip.SetTextureKeepSize("NewGUI", "weaponOnBoardBlue");
		base.AddGuiElement(this.txWeaponOnOtherShip);
		this.lblWeaponOnCurrentShip = new GuiLabel()
		{
			boundries = new Rect(95f, 432f, 190f, 17f),
			text = StaticData.Translate("key_dock_weapons_on_current_ship"),
			FontSize = 12,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 3
		};
		base.AddGuiElement(this.lblWeaponOnCurrentShip);
		this.lblWeaponOnOtherShip = new GuiLabel()
		{
			boundries = new Rect(95f, 470f, 190f, 17f),
			text = StaticData.Translate("key_dock_weapons_on_other_ship"),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 3
		};
		base.AddGuiElement(this.lblWeaponOnOtherShip);
		this.CreateUpgradeBars();
		this.CreateInventorySlots();
		this.PopulateItemPanel();
		this.RefreshPlusMinusButtons();
	}

	private void DialogClose(object prm)
	{
		AndromedaGui.gui.RemoveWindow(this.dialogWindow.handler);
		AndromedaGui.gui.activeToolTipId = -1;
		this.dialogWindow = null;
	}

	private void DialogNoBtnClicked(object prm)
	{
		this.DialogClose(null);
		this.PopulateShipsGUINew();
	}

	private void DialogYesBtnClicked(object prm)
	{
		// 
		// Current member / type: System.Void __DockWindow::DialogYesBtnClicked(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void DialogYesBtnClicked(System.Object)
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

	private void DrawMyShipGui()
	{
		this.desirableUpgrades = 0;
		this.desirableUpgradesPrice = 0;
		this.ownershipStamp = new GuiTexture()
		{
			boundries = new Rect(300f, 210f, 91f, 91f)
		};
		this.ownershipStamp.SetTextureKeepSize("NewGUI", "alreadyOwned");
		base.AddGuiElement(this.ownershipStamp);
		this.forDelete.Add(this.ownershipStamp);
		this.lbl_upgradeLvl = new GuiLabel()
		{
			boundries = new Rect(500f, 110f, 120f, 24f),
			text = string.Empty,
			FontSize = 14,
			Font = GuiLabel.FontBold,
			Alignment = 5,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.lbl_upgradeLvl);
		this.forDelete.Add(this.lbl_upgradeLvl);
		this.lbl_shild = new GuiLabel()
		{
			boundries = new Rect(315f, 296f, 138f, 17f),
			text = StaticData.Translate("key_dock_my_ships_shield").ToUpper(),
			Alignment = 3,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.lbl_shild);
		this.forDelete.Add(this.lbl_shild);
		this.lbl_hull = new GuiLabel()
		{
			boundries = new Rect(465f, 296f, 138f, 17f),
			text = StaticData.Translate("key_dock_my_ships_corpus").ToUpper(),
			Alignment = 3,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.lbl_hull);
		this.forDelete.Add(this.lbl_hull);
		this.lbl_speed = new GuiLabel()
		{
			boundries = new Rect(315f, 326f, 138f, 17f),
			text = StaticData.Translate("key_dock_my_ships_speed").ToUpper(),
			Alignment = 3,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.lbl_speed);
		this.forDelete.Add(this.lbl_speed);
		this.lbl_avoidance = new GuiLabel()
		{
			boundries = new Rect(465f, 326f, 138f, 17f),
			text = StaticData.Translate("key_dock_my_ships_avoidance").ToUpper(),
			Alignment = 3,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.lbl_avoidance);
		this.forDelete.Add(this.lbl_avoidance);
		this.lbl_targeting = new GuiLabel()
		{
			boundries = new Rect(315f, 356f, 138f, 17f),
			text = StaticData.Translate("key_dock_my_ships_targetng").ToUpper(),
			Alignment = 3,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.lbl_targeting);
		this.forDelete.Add(this.lbl_targeting);
		this.lbl_cargo = new GuiLabel()
		{
			boundries = new Rect(465f, 356f, 138f, 17f),
			text = StaticData.Translate("key_dock_my_ships_cargo").ToUpper(),
			Alignment = 3,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.lbl_cargo);
		this.forDelete.Add(this.lbl_cargo);
		this.lbl_shildValue = new GuiLabel()
		{
			boundries = new Rect(315f, 296f, 138f, 17f),
			text = string.Empty,
			Alignment = 5,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(this.lbl_shildValue);
		this.forDelete.Add(this.lbl_shildValue);
		this.lbl_hullValue = new GuiLabel()
		{
			boundries = new Rect(465f, 296f, 138f, 17f),
			text = string.Empty,
			Alignment = 5,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(this.lbl_hullValue);
		this.forDelete.Add(this.lbl_hullValue);
		this.lbl_speedValue = new GuiLabel()
		{
			boundries = new Rect(315f, 326f, 138f, 17f),
			text = string.Empty,
			Alignment = 5,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(this.lbl_speedValue);
		this.forDelete.Add(this.lbl_speedValue);
		this.lbl_avoidanceValue = new GuiLabel()
		{
			boundries = new Rect(465f, 326f, 138f, 17f),
			text = string.Empty,
			Alignment = 5,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(this.lbl_avoidanceValue);
		this.forDelete.Add(this.lbl_avoidanceValue);
		this.lbl_targetingValue = new GuiLabel()
		{
			boundries = new Rect(315f, 356f, 138f, 17f),
			text = string.Empty,
			Alignment = 5,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(this.lbl_targetingValue);
		this.forDelete.Add(this.lbl_targetingValue);
		this.lbl_cargoValue = new GuiLabel()
		{
			boundries = new Rect(465f, 356f, 138f, 17f),
			text = string.Empty,
			Alignment = 5,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(this.lbl_cargoValue);
		this.forDelete.Add(this.lbl_cargoValue);
		this.bar_shield = new GuiNewDualColorBar()
		{
			ValueOne = 0f,
			ValueTwo = 0f,
			Max = 10f,
			X = 313f,
			Y = 311f,
			Width = 142f
		};
		base.AddGuiElement(this.bar_shield);
		this.forDelete.Add(this.bar_shield);
		this.bar_hull = new GuiNewDualColorBar()
		{
			ValueOne = 0f,
			ValueTwo = 0f,
			Max = 10f,
			X = 463f,
			Y = 311f,
			Width = 142f
		};
		base.AddGuiElement(this.bar_hull);
		this.forDelete.Add(this.bar_hull);
		this.bar_speed = new GuiNewDualColorBar()
		{
			ValueOne = 0f,
			ValueTwo = 0f,
			Max = 10f,
			X = 313f,
			Y = 341f,
			Width = 142f
		};
		base.AddGuiElement(this.bar_speed);
		this.forDelete.Add(this.bar_speed);
		this.bar_avoidance = new GuiNewDualColorBar()
		{
			ValueOne = 0f,
			ValueTwo = 0f,
			Max = 10f,
			X = 463f,
			Y = 341f,
			Width = 142f
		};
		base.AddGuiElement(this.bar_avoidance);
		this.forDelete.Add(this.bar_avoidance);
		this.bar_targeting = new GuiNewDualColorBar()
		{
			ValueOne = 0f,
			ValueTwo = 0f,
			Max = 10f,
			X = 313f,
			Y = 371f,
			Width = 142f
		};
		base.AddGuiElement(this.bar_targeting);
		this.forDelete.Add(this.bar_targeting);
		this.bar_cargo = new GuiNewDualColorBar()
		{
			ValueOne = 0f,
			ValueTwo = 0f,
			Max = 10f,
			X = 463f,
			Y = 371f,
			Width = 142f
		};
		base.AddGuiElement(this.bar_cargo);
		this.forDelete.Add(this.bar_cargo);
		this.btnSelectShip = new GuiButtonResizeable();
		this.btnSelectShip.SetSmallOrangeTexture();
		this.btnSelectShip.X = 397f;
		this.btnSelectShip.Y = 263f;
		this.btnSelectShip.Width = 125f;
		this.btnSelectShip.Caption = StaticData.Translate("key_dock_my_ships_select_ship").ToUpper();
		this.btnSelectShip.FontSize = 14;
		this.btnSelectShip.MarginTop = 4;
		this.btnSelectShip.Alignment = 1;
		this.btnSelectShip.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnSelectShipClicked);
		base.AddGuiElement(this.btnSelectShip);
		this.forDelete.Add(this.btnSelectShip);
		this.curentShipFlag = new GuiTexture()
		{
			boundries = new Rect(555f, 150f, 59f, 59f)
		};
		this.curentShipFlag.SetTextureKeepSize("FrameworkGUI", "empty");
		base.AddGuiElement(this.curentShipFlag);
		this.forDelete.Add(this.curentShipFlag);
		this.btnUpgrade = new GuiButtonResizeable();
		this.btnUpgrade.SetSmallBlueTexture();
		this.btnUpgrade.Y = 475f;
		this.btnUpgrade.MarginTop = 5;
		this.btnUpgrade.Alignment = 1;
		this.btnUpgrade.X = 362f;
		this.btnUpgrade.Width = 194f;
		this.btnUpgrade.Caption = string.Empty;
		this.btnUpgrade.FontSize = 12;
		this.btnUpgrade.isEnabled = false;
		this.btnUpgrade.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnUpgradeButtonClicked);
		base.AddGuiElement(this.btnUpgrade);
		this.forDelete.Add(this.btnUpgrade);
		this.btnRepair = new GuiButtonResizeable();
		this.btnRepair.SetSmallBlueTexture();
		this.btnRepair.Y = 475f;
		this.btnRepair.MarginTop = 5;
		this.btnRepair.Alignment = 1;
		this.btnRepair.X = 89f;
		this.btnRepair.Width = 194f;
		this.btnRepair.Caption = string.Empty;
		this.btnRepair.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnRepairButtonClicked);
		this.btnRepair.FontSize = 12;
		base.AddGuiElement(this.btnRepair);
		this.forDelete.Add(this.btnRepair);
		this.repairPriceLbl = new GuiLabel()
		{
			boundries = this.btnRepair.boundries
		};
		ref Rect rectPointer = ref this.repairPriceLbl.boundries;
		rectPointer.set_x(rectPointer.get_x() - 2f);
		this.repairPriceLbl.Alignment = 4;
		this.repairPriceLbl.Font = GuiLabel.FontBold;
		this.repairPriceLbl.FontSize = 12;
		this.repairPriceLbl.text = string.Empty;
		base.AddGuiElement(this.repairPriceLbl);
		this.forDelete.Add(this.repairPriceLbl);
		if ((float)NetworkScript.player.playerBelongings.get_SelectedShip().CorpusHP < (float)NetworkScript.player.playerBelongings.get_SelectedShip().Corpus * 0.3f)
		{
			this.PutRepairAnimation();
		}
		this.btnNewMinus = new GuiButtonResizeable();
		this.btnNewMinus.SetSmallOrangeTexture();
		this.btnNewMinus.Y = 431f;
		this.btnNewMinus.X = 348f;
		this.btnNewMinus.Width = 33f;
		this.btnNewMinus._marginLeft = -3;
		this.btnNewMinus.MarginTop = -2;
		this.btnNewMinus.Caption = "-";
		this.btnNewMinus.Alignment = 4;
		this.btnNewMinus.FontSize = 22;
		this.btnNewMinus.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnMinusButtonClicked);
		base.AddGuiElement(this.btnNewMinus);
		this.forDelete.Add(this.btnNewMinus);
		this.btnNewPlus = new GuiButtonResizeable();
		this.btnNewPlus.SetSmallOrangeTexture();
		this.btnNewPlus.Y = 431f;
		this.btnNewPlus.Width = 33f;
		this.btnNewPlus._marginLeft = -3;
		this.btnNewPlus.MarginTop = -2;
		this.btnNewPlus.X = 537f;
		this.btnNewPlus.Caption = "+";
		this.btnNewPlus.Alignment = 4;
		this.btnNewPlus.FontSize = 22;
		this.btnNewPlus.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnPlusButtonClicked);
		base.AddGuiElement(this.btnNewPlus);
		this.forDelete.Add(this.btnNewPlus);
		this.lbl_remainingUpgrades = new GuiLabel()
		{
			boundries = new Rect(362f, 400f, 194f, 17f),
			text = StaticData.Translate("key_dock_my_ships_upgrade_available").ToUpper(),
			Alignment = 4,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(this.lbl_remainingUpgrades);
		this.forDelete.Add(this.lbl_remainingUpgrades);
		for (int i = 0; i < 10; i++)
		{
			this.remainingUpgrades[i] = new GuiTexture();
			this.remainingUpgrades[i].SetTexture("NewGUI", "dot_empty");
			this.remainingUpgrades[i].X = (float)(388 + i * 14);
			this.remainingUpgrades[i].Y = 435f;
			base.AddGuiElement(this.remainingUpgrades[i]);
			this.forDelete.Add(this.remainingUpgrades[i]);
		}
		this.lbl_curentShild = new GuiLabel()
		{
			boundries = new Rect(55f, 424f, 80f, 17f),
			text = StaticData.Translate("key_dock_my_ships_shield").ToUpper(),
			Alignment = 5,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.lbl_curentShild);
		this.forDelete.Add(this.lbl_curentShild);
		this.lbl_curentHull = new GuiLabel()
		{
			boundries = new Rect(55f, 445f, 80f, 17f),
			text = StaticData.Translate("key_dock_my_ships_corpus").ToUpper(),
			Alignment = 5,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.lbl_curentHull);
		this.forDelete.Add(this.lbl_curentHull);
		this.bar_curentShield = new GuiNewStyleBar();
		this.bar_curentShield.SetCustumSizeBlueBar(102);
		this.bar_curentShield.X = 138f;
		this.bar_curentShield.Y = 425f;
		this.bar_curentShield.current = 50f;
		base.AddGuiElement(this.bar_curentShield);
		this.forDelete.Add(this.bar_curentShield);
		this.bar_curentHull = new GuiNewStyleBar();
		this.bar_curentHull.SetCustumSizeBlueBar(102);
		this.bar_curentHull.X = 138f;
		this.bar_curentHull.Y = 445f;
		this.bar_curentHull.current = 87f;
		base.AddGuiElement(this.bar_curentHull);
		this.forDelete.Add(this.bar_curentHull);
		this.lbl_curentShildValue = new GuiLabel()
		{
			boundries = new Rect(241f, 425f, 80f, 17f),
			text = string.Empty,
			Alignment = 3,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		base.AddGuiElement(this.lbl_curentShildValue);
		this.forDelete.Add(this.lbl_curentShildValue);
		this.lbl_curentHullValue = new GuiLabel()
		{
			boundries = new Rect(241f, 445f, 80f, 17f),
			text = string.Empty,
			Alignment = 3,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		base.AddGuiElement(this.lbl_curentHullValue);
		this.forDelete.Add(this.lbl_curentHullValue);
	}

	private void DrawNewShipsGui()
	{
		int num = this.selectedNewShip.levelRestriction;
		if (NetworkScript.player.playerBelongings.playerLevel >= num)
		{
			this.btnBuyNewShipCash = new GuiButtonResizeable();
			this.btnBuyNewShipCash.SetBlueTexture();
			this.btnBuyNewShipCash.X = 50f;
			this.btnBuyNewShipCash.Y = 466f;
			this.btnBuyNewShipCash.Width = 160f;
			this.btnBuyNewShipCash.Caption = StaticData.Translate("key_dock_my_ships_buy_ship").ToUpper();
			this.btnBuyNewShipCash.FontSize = 14;
			this.btnBuyNewShipCash.MarginTop = 12;
			this.btnBuyNewShipCash.Alignment = 1;
			this.btnBuyNewShipCash.eventHandlerParam.customData = (SelectedCurrency)0;
			this.btnBuyNewShipCash.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnBuyShipClicked);
			base.AddGuiElement(this.btnBuyNewShipCash);
			this.newShipForDelete.Add(this.btnBuyNewShipCash);
			this.btnBuyNewShipNova = new GuiButtonResizeable();
			this.btnBuyNewShipNova.SetOrangeTexture();
			this.btnBuyNewShipNova.X = 708f;
			this.btnBuyNewShipNova.Y = 466f;
			this.btnBuyNewShipNova.Width = 160f;
			this.btnBuyNewShipNova.Caption = StaticData.Translate("key_dock_my_ships_buy_ship").ToUpper();
			this.btnBuyNewShipNova.FontSize = 14;
			this.btnBuyNewShipNova.MarginTop = 12;
			this.btnBuyNewShipNova.Alignment = 1;
			this.btnBuyNewShipNova.eventHandlerParam.customData = (SelectedCurrency)1;
			this.btnBuyNewShipNova.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnBuyShipClicked);
			base.AddGuiElement(this.btnBuyNewShipNova);
			this.newShipForDelete.Add(this.btnBuyNewShipNova);
		}
		else
		{
			this.levelRestrictionLbl_1 = new GuiLabel()
			{
				boundries = new Rect(50f, 466f, 220f, 32f),
				text = string.Format(StaticData.Translate("key_tooltip_level_restriction"), num),
				TextColor = GuiNewStyleBar.redColor,
				FontSize = 16,
				Font = GuiLabel.FontBold,
				Alignment = 3
			};
			base.AddGuiElement(this.levelRestrictionLbl_1);
			this.newShipForDelete.Add(this.levelRestrictionLbl_1);
			this.levelRestrictionLbl_2 = new GuiLabel()
			{
				boundries = new Rect(648f, 466f, 220f, 32f),
				text = string.Format(StaticData.Translate("key_tooltip_level_restriction"), num),
				TextColor = GuiNewStyleBar.redColor,
				FontSize = 16,
				Font = GuiLabel.FontBold,
				Alignment = 5
			};
			base.AddGuiElement(this.levelRestrictionLbl_2);
			this.newShipForDelete.Add(this.levelRestrictionLbl_2);
		}
		this.cashIcon = new GuiTexture()
		{
			boundries = new Rect(54f, 438f, 20f, 20f)
		};
		this.cashIcon.SetTextureKeepSize("FrameworkGUI", "res_cash");
		base.AddGuiElement(this.cashIcon);
		this.newShipForDelete.Add(this.cashIcon);
		this.novaIcon = new GuiTexture()
		{
			boundries = new Rect(846f, 438f, 20f, 20f)
		};
		this.novaIcon.SetTextureKeepSize("FrameworkGUI", "res_nova");
		base.AddGuiElement(this.novaIcon);
		this.newShipForDelete.Add(this.novaIcon);
		this.lblNewShipPriceCash = new GuiLabel()
		{
			boundries = new Rect(76f, 438f, 236f, 17f),
			text = this.selectedNewShip.price.ToString("##,##0"),
			FontSize = 17,
			Font = GuiLabel.FontBold,
			Alignment = 3
		};
		base.AddGuiElement(this.lblNewShipPriceCash);
		this.newShipForDelete.Add(this.lblNewShipPriceCash);
		this.lblNewShipPriceNova = new GuiLabel()
		{
			boundries = new Rect(607f, 438f, 236f, 17f)
		};
		GuiLabel str = this.lblNewShipPriceNova;
		float single = (float)this.selectedNewShip.price * 0.9f / (float)this.exchangeRate;
		str.text = single.ToString("##,##0");
		this.lblNewShipPriceNova.FontSize = 17;
		this.lblNewShipPriceNova.Font = GuiLabel.FontBold;
		this.lblNewShipPriceNova.Alignment = 5;
		base.AddGuiElement(this.lblNewShipPriceNova);
		this.newShipForDelete.Add(this.lblNewShipPriceNova);
		this.lblPriceCash = new GuiLabel()
		{
			boundries = new Rect(52f, 415f, 260f, 17f),
			text = StaticData.Translate("key_dock_my_ships_buy_cash").ToUpper(),
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 15,
			Alignment = 3
		};
		base.AddGuiElement(this.lblPriceCash);
		this.newShipForDelete.Add(this.lblPriceCash);
		this.lblPriceNova = new GuiLabel()
		{
			boundries = new Rect(607f, 415f, 260f, 17f),
			text = StaticData.Translate("key_dock_my_ships_buy_nova").ToUpper(),
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 15,
			Alignment = 5
		};
		base.AddGuiElement(this.lblPriceNova);
		this.newShipForDelete.Add(this.lblPriceNova);
	}

	private void DrawNewShipStats()
	{
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(315f, 296f, 138f, 17f),
			text = StaticData.Translate("key_dock_my_ships_shield").ToUpper(),
			Alignment = 3,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel);
		this.newShipForDelete.Add(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(465f, 296f, 138f, 17f),
			text = StaticData.Translate("key_dock_my_ships_corpus").ToUpper(),
			Alignment = 3,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel1);
		this.newShipForDelete.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(315f, 326f, 138f, 17f),
			text = StaticData.Translate("key_dock_my_ships_speed").ToUpper(),
			Alignment = 3,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel2);
		this.newShipForDelete.Add(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(465f, 326f, 138f, 17f),
			text = StaticData.Translate("key_dock_my_ships_avoidance").ToUpper(),
			Alignment = 3,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel3);
		this.newShipForDelete.Add(guiLabel3);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			boundries = new Rect(315f, 356f, 138f, 17f),
			text = StaticData.Translate("key_dock_my_ships_targetng").ToUpper(),
			Alignment = 3,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel4);
		this.newShipForDelete.Add(guiLabel4);
		GuiLabel guiLabel5 = new GuiLabel()
		{
			boundries = new Rect(465f, 356f, 138f, 17f),
			text = StaticData.Translate("key_dock_my_ships_cargo").ToUpper(),
			Alignment = 3,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel5);
		this.newShipForDelete.Add(guiLabel5);
		this.lbl_newShip_shildValue = new GuiLabel()
		{
			boundries = new Rect(315f, 296f, 138f, 17f),
			text = this.selectedNewShip.shield.ToString("##,##0"),
			Alignment = 5,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(this.lbl_newShip_shildValue);
		this.newShipForDelete.Add(this.lbl_newShip_shildValue);
		this.lbl_newShip_hullValue = new GuiLabel()
		{
			boundries = new Rect(465f, 296f, 138f, 17f),
			text = this.selectedNewShip.corpus.ToString("##,##0"),
			Alignment = 5,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(this.lbl_newShip_hullValue);
		this.newShipForDelete.Add(this.lbl_newShip_hullValue);
		this.lbl_newShip_speedValue = new GuiLabel()
		{
			boundries = new Rect(315f, 326f, 138f, 17f),
			text = this.selectedNewShip.speed.ToString("##,##0"),
			Alignment = 5,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(this.lbl_newShip_speedValue);
		this.newShipForDelete.Add(this.lbl_newShip_speedValue);
		this.lbl_newShip_avoidanceValue = new GuiLabel()
		{
			boundries = new Rect(465f, 326f, 138f, 17f),
			text = this.selectedNewShip.avoidance.ToString("##,##0"),
			Alignment = 5,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(this.lbl_newShip_avoidanceValue);
		this.newShipForDelete.Add(this.lbl_newShip_avoidanceValue);
		this.lbl_newShip_targetingValue = new GuiLabel()
		{
			boundries = new Rect(315f, 356f, 138f, 17f),
			text = this.selectedNewShip.targeting.ToString("##,##0"),
			Alignment = 5,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(this.lbl_newShip_targetingValue);
		this.newShipForDelete.Add(this.lbl_newShip_targetingValue);
		this.lbl_newShip_cargoValue = new GuiLabel()
		{
			boundries = new Rect(465f, 356f, 138f, 17f),
			text = this.selectedNewShip.cargo.ToString("##,##0"),
			Alignment = 5,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(this.lbl_newShip_cargoValue);
		this.newShipForDelete.Add(this.lbl_newShip_cargoValue);
		this.bar_newShip_shield = new GuiNewDualColorBar()
		{
			ValueOne = 0f,
			ValueTwo = 0f,
			Max = (float)this.MAX_SHIELD,
			X = 313f,
			Y = 311f,
			Width = 142f
		};
		base.AddGuiElement(this.bar_newShip_shield);
		this.newShipForDelete.Add(this.bar_newShip_shield);
		this.bar_newShip_hull = new GuiNewDualColorBar()
		{
			ValueOne = 0f,
			ValueTwo = 0f,
			Max = (float)this.MAX_CORPUS,
			X = 463f,
			Y = 311f,
			Width = 142f
		};
		base.AddGuiElement(this.bar_newShip_hull);
		this.newShipForDelete.Add(this.bar_newShip_hull);
		this.bar_newShip_speed = new GuiNewDualColorBar()
		{
			ValueOne = 0f,
			ValueTwo = 0f,
			Max = (float)this.MAX_SPEED,
			X = 313f,
			Y = 341f,
			Width = 142f
		};
		base.AddGuiElement(this.bar_newShip_speed);
		this.newShipForDelete.Add(this.bar_newShip_speed);
		this.bar_newShip_avoidance = new GuiNewDualColorBar()
		{
			ValueOne = 0f,
			ValueTwo = 0f,
			Max = (float)this.MAX_AVOIDANCE,
			X = 463f,
			Y = 341f,
			Width = 142f
		};
		base.AddGuiElement(this.bar_newShip_avoidance);
		this.newShipForDelete.Add(this.bar_newShip_avoidance);
		this.bar_newShip_targeting = new GuiNewDualColorBar()
		{
			ValueOne = 0f,
			ValueTwo = 0f,
			Max = (float)this.MAX_TARGETING,
			X = 313f,
			Y = 371f,
			Width = 142f
		};
		base.AddGuiElement(this.bar_newShip_targeting);
		this.newShipForDelete.Add(this.bar_newShip_targeting);
		this.bar_newShip_cargo = new GuiNewDualColorBar()
		{
			ValueOne = 0f,
			ValueTwo = 0f,
			Max = (float)this.MAX_CARGO,
			X = 463f,
			Y = 371f,
			Width = 142f
		};
		base.AddGuiElement(this.bar_newShip_cargo);
		this.newShipForDelete.Add(this.bar_newShip_cargo);
	}

	private void DrawShipsCommon()
	{
		GuiTexture guiTexture = new GuiTexture()
		{
			boundries = new Rect(36f, 137f, 847f, 384f)
		};
		guiTexture.SetTextureKeepSize("NewGUI", "inbase_frame_3parts");
		base.AddGuiElement(guiTexture);
		this.newShipForDelete.Add(guiTexture);
		this.newShipLeftArrow = new GuiButtonFixed();
		this.newShipLeftArrow.SetTexture("NewGUI", "inbase_vertscroll_left_");
		this.newShipLeftArrow.X = 48f;
		this.newShipLeftArrow.Y = 214f;
		this.newShipLeftArrow.Caption = string.Empty;
		this.newShipLeftArrow.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnNewShipLeftClicked);
		this.newShipLeftArrow.SetLeftClickSound("Sounds", "minus");
		base.AddGuiElement(this.newShipLeftArrow);
		this.newShipForDelete.Add(this.newShipLeftArrow);
		this.newShipRightArrow = new GuiButtonFixed();
		this.newShipRightArrow.SetTexture("NewGUI", "inbase_vertscroll_right_");
		this.newShipRightArrow.X = 837f;
		this.newShipRightArrow.Y = 214f;
		this.newShipRightArrow.Caption = string.Empty;
		this.newShipRightArrow.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnNewShipRightClicked);
		this.newShipRightArrow.SetLeftClickSound("Sounds", "plus");
		base.AddGuiElement(this.newShipRightArrow);
		this.newShipForDelete.Add(this.newShipRightArrow);
		this.newShipLeftBox = new GuiButtonFixed();
		this.newShipLeftBox.SetTexture("FrameworkGUI", "empty");
		this.newShipLeftBox.boundries = new Rect(101f, 192f, 177f, 140f);
		this.newShipLeftBox.Caption = string.Empty;
		this.newShipLeftBox.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnNewShipLeftClicked);
		this.newShipLeftBox.SetLeftClickSound("Sounds", "minus");
		base.AddGuiElement(this.newShipLeftBox);
		this.newShipForDelete.Add(this.newShipLeftBox);
		this.newShipRightBox = new GuiButtonFixed();
		this.newShipRightBox.SetTexture("FrameworkGUI", "empty");
		this.newShipRightBox.boundries = new Rect(641f, 192f, 177f, 140f);
		this.newShipRightBox.Caption = string.Empty;
		this.newShipRightBox.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnNewShipRightClicked);
		this.newShipRightBox.SetLeftClickSound("Sounds", "plus");
		base.AddGuiElement(this.newShipRightBox);
		this.newShipForDelete.Add(this.newShipRightBox);
		this.lbl_shipName = new GuiLabel()
		{
			boundries = new Rect(299f, 110f, 180f, 24f),
			text = string.Empty,
			FontSize = 16,
			Font = GuiLabel.FontBold,
			Alignment = 3
		};
		base.AddGuiElement(this.lbl_shipName);
		this.forDelete.Add(this.lbl_shipName);
		this.lbl_upgradeLvl = new GuiLabel()
		{
			boundries = new Rect(500f, 110f, 120f, 24f),
			text = string.Empty,
			FontSize = 14,
			Font = GuiLabel.FontBold,
			Alignment = 5,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.lbl_upgradeLvl);
		this.forDelete.Add(this.lbl_upgradeLvl);
		this.lblNewShipName = new GuiLabel()
		{
			boundries = new Rect(175f, 110f, 322f, 24f),
			text = StaticData.Translate(this.selectedNewShip.shipName),
			FontSize = 16,
			Font = GuiLabel.FontBold,
			Alignment = 4
		};
		base.AddGuiElement(this.lblNewShipName);
		this.newShipForDelete.Add(this.lblNewShipName);
		this.leftShip = new GuiTexture()
		{
			boundries = new Rect(100f, 200f, 177f, 121f)
		};
		this.leftShip.SetTextureKeepSize("FrameworkGUI", "empty");
		base.AddGuiElement(this.leftShip);
		this.newShipForDelete.Add(this.leftShip);
		this.middleShip = new GuiTexture()
		{
			boundries = new Rect(359f, 144f, 204f, 140f)
		};
		this.middleShip.SetTextureKeepSize("FrameworkGUI", "empty");
		base.AddGuiElement(this.middleShip);
		this.newShipForDelete.Add(this.middleShip);
		this.rightShip = new GuiTexture()
		{
			boundries = new Rect(640f, 200f, 177f, 121f)
		};
		this.rightShip.SetTextureKeepSize("FrameworkGUI", "empty");
		base.AddGuiElement(this.rightShip);
		this.newShipForDelete.Add(this.rightShip);
		if (this.newShipIndex <= 0)
		{
			this.leftShip.SetTextureKeepSize("FrameworkGUI", "empty");
		}
		else
		{
			string str = this.newShips[this.newShipIndex - 1].assetName;
			this.leftShip.SetTextureKeepSize("ShipsAvatars", str);
		}
		if (this.newShipIndex >= Enumerable.Count<ShipsTypeNet>(this.newShips) - 1)
		{
			this.rightShip.SetTextureKeepSize("FrameworkGUI", "empty");
		}
		else
		{
			string str1 = this.newShips[this.newShipIndex + 1].assetName;
			this.rightShip.SetTextureKeepSize("ShipsAvatars", str1);
		}
		string str2 = this.selectedNewShip.assetName;
		this.middleShip.SetTextureKeepSize("ShipsAvatars", str2);
		this.newShipLeftArrow.isEnabled = (this.newShipIndex != 0 ? true : false);
		this.newShipRightArrow.isEnabled = (this.newShipIndex >= Enumerable.Count<ShipsTypeNet>(this.newShips) - 1 ? false : true);
		this.newShipLeftBox.isEnabled = this.newShipLeftArrow.isEnabled;
		this.newShipRightBox.isEnabled = this.newShipRightArrow.isEnabled;
	}

	private void DrawSlot1()
	{
		GuiTexture guiTexture = new GuiTexture()
		{
			boundries = new Rect((float)this.possitionX, (float)this.possitionY, 47f, 47f)
		};
		guiTexture.SetTextureKeepSize("NewGUI", "inbase_weapon_slot");
		base.AddGuiElement(guiTexture);
		this.weaponSlots.Add(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture()
		{
			boundries = new Rect(guiTexture.X + 4f, guiTexture.Y + 4f, 37f, 37f)
		};
		guiTexture1.SetTextureKeepSize("NewGUI", "LaserSlot_icon");
		base.AddGuiElement(guiTexture1);
		this.weaponSlots.Add(guiTexture1);
	}

	private void DrawSlot2()
	{
		GuiTexture guiTexture = new GuiTexture()
		{
			boundries = new Rect((float)(this.possitionX + 90), (float)this.possitionY, 47f, 47f)
		};
		guiTexture.SetTextureKeepSize("NewGUI", "inbase_weapon_slot");
		base.AddGuiElement(guiTexture);
		this.weaponSlots.Add(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture()
		{
			boundries = new Rect(guiTexture.X + 4f, guiTexture.Y + 4f, 37f, 37f)
		};
		guiTexture1.SetTextureKeepSize("NewGUI", "PlasmaSlot_icon");
		base.AddGuiElement(guiTexture1);
		this.weaponSlots.Add(guiTexture1);
	}

	private void DrawSlot3()
	{
		GuiTexture guiTexture = new GuiTexture()
		{
			boundries = new Rect((float)(this.possitionX + 40), (float)this.possitionY, 47f, 47f)
		};
		guiTexture.SetTextureKeepSize("NewGUI", "inbase_weapon_slot");
		base.AddGuiElement(guiTexture);
		this.weaponSlots.Add(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture()
		{
			boundries = new Rect(guiTexture.X + 4f, guiTexture.Y + 4f, 37f, 37f)
		};
		guiTexture1.SetTextureKeepSize("NewGUI", "LaserSlot_icon");
		base.AddGuiElement(guiTexture1);
		this.weaponSlots.Add(guiTexture1);
	}

	private void DrawSlot4()
	{
		GuiTexture guiTexture = new GuiTexture()
		{
			boundries = new Rect((float)(this.possitionX + 180), (float)this.possitionY, 47f, 47f)
		};
		guiTexture.SetTextureKeepSize("NewGUI", "inbase_weapon_slot");
		base.AddGuiElement(guiTexture);
		this.weaponSlots.Add(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture()
		{
			boundries = new Rect(guiTexture.X + 4f, guiTexture.Y + 4f, 37f, 37f)
		};
		guiTexture1.SetTextureKeepSize("NewGUI", "IonSlot_icon");
		base.AddGuiElement(guiTexture1);
		this.weaponSlots.Add(guiTexture1);
	}

	private void DrawSlot5()
	{
		GuiTexture guiTexture = new GuiTexture()
		{
			boundries = new Rect((float)(this.possitionX + 130), (float)this.possitionY, 47f, 47f)
		};
		guiTexture.SetTextureKeepSize("NewGUI", "inbase_weapon_slot");
		base.AddGuiElement(guiTexture);
		this.weaponSlots.Add(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture()
		{
			boundries = new Rect(guiTexture.X + 4f, guiTexture.Y + 4f, 37f, 37f)
		};
		guiTexture1.SetTextureKeepSize("NewGUI", "PlasmaSlot_icon");
		base.AddGuiElement(guiTexture1);
		this.weaponSlots.Add(guiTexture1);
	}

	private void DrawSlot6()
	{
		GuiTexture guiTexture = new GuiTexture()
		{
			boundries = new Rect((float)(this.possitionX + 220), (float)this.possitionY, 47f, 47f)
		};
		guiTexture.SetTextureKeepSize("NewGUI", "inbase_weapon_slot");
		base.AddGuiElement(guiTexture);
		this.weaponSlots.Add(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture()
		{
			boundries = new Rect(guiTexture.X + 4f, guiTexture.Y + 4f, 37f, 37f)
		};
		guiTexture1.SetTextureKeepSize("NewGUI", "IonSlot_icon");
		base.AddGuiElement(guiTexture1);
		this.weaponSlots.Add(guiTexture1);
	}

	private void DrawWeaponsConfig(string shipType)
	{
		int num = 0;
		this.ClearWeaponSlotsConfig();
		string str = shipType;
		if (str != null)
		{
			if (__DockWindow.<>f__switch$map3 == null)
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
				__DockWindow.<>f__switch$map3 = dictionary;
			}
			if (__DockWindow.<>f__switch$map3.TryGetValue(str, ref num))
			{
				switch (num)
				{
					case 0:
					{
						this.lblLaserSlots.text = StaticData.Translate("key_dock_my_ships_laser_slots");
						this.lblPlasmaSlots.text = string.Empty;
						this.lblIonSlots.text = string.Empty;
						this.DrawSlot1();
						break;
					}
					case 1:
					{
						this.lblLaserSlots.text = StaticData.Translate("key_dock_my_ships_laser_slots");
						this.lblPlasmaSlots.text = StaticData.Translate("key_dock_my_ships_plasma_slots");
						this.lblIonSlots.text = StaticData.Translate("key_dock_my_ships_ion_slots");
						this.DrawSlot1();
						this.DrawSlot2();
						this.DrawSlot4();
						break;
					}
					case 2:
					{
						this.lblLaserSlots.text = StaticData.Translate("key_dock_my_ships_laser_slots");
						this.lblPlasmaSlots.text = StaticData.Translate("key_dock_my_ships_plasma_slots");
						this.lblIonSlots.text = string.Empty;
						this.DrawSlot1();
						this.DrawSlot2();
						break;
					}
					case 3:
					{
						this.lblLaserSlots.text = StaticData.Translate("key_dock_my_ships_laser_slots");
						this.lblPlasmaSlots.text = StaticData.Translate("key_dock_my_ships_plasma_slots");
						this.lblIonSlots.text = StaticData.Translate("key_dock_my_ships_ion_slots");
						this.DrawSlot1();
						this.DrawSlot2();
						this.DrawSlot4();
						this.DrawSlot5();
						this.DrawSlot6();
						break;
					}
					case 4:
					{
						this.lblLaserSlots.text = string.Empty;
						this.lblPlasmaSlots.text = StaticData.Translate("key_dock_my_ships_plasma_slots");
						this.lblIonSlots.text = StaticData.Translate("key_dock_my_ships_ion_slots");
						this.DrawSlot2();
						this.DrawSlot4();
						this.DrawSlot6();
						break;
					}
					case 5:
					{
						this.lblLaserSlots.text = StaticData.Translate("key_dock_my_ships_laser_slots");
						this.lblPlasmaSlots.text = StaticData.Translate("key_dock_my_ships_plasma_slots");
						this.lblIonSlots.text = string.Empty;
						this.DrawSlot1();
						this.DrawSlot2();
						this.DrawSlot3();
						break;
					}
					case 6:
					{
						this.lblLaserSlots.text = StaticData.Translate("key_dock_my_ships_laser_slots");
						this.lblPlasmaSlots.text = StaticData.Translate("key_dock_my_ships_plasma_slots");
						this.lblIonSlots.text = StaticData.Translate("key_dock_my_ships_ion_slots");
						this.DrawSlot1();
						this.DrawSlot2();
						this.DrawSlot3();
						this.DrawSlot4();
						this.DrawSlot6();
						break;
					}
					case 7:
					{
						this.lblLaserSlots.text = StaticData.Translate("key_dock_my_ships_laser_slots");
						this.lblPlasmaSlots.text = StaticData.Translate("key_dock_my_ships_plasma_slots");
						this.lblIonSlots.text = StaticData.Translate("key_dock_my_ships_ion_slots");
						this.DrawSlot1();
						this.DrawSlot2();
						this.DrawSlot3();
						this.DrawSlot4();
						break;
					}
					case 8:
					{
						this.lblLaserSlots.text = StaticData.Translate("key_dock_my_ships_laser_slots");
						this.lblPlasmaSlots.text = StaticData.Translate("key_dock_my_ships_plasma_slots");
						this.lblIonSlots.text = StaticData.Translate("key_dock_my_ships_ion_slots");
						this.DrawSlot1();
						this.DrawSlot2();
						this.DrawSlot4();
						this.DrawSlot5();
						break;
					}
					case 9:
					{
						this.lblLaserSlots.text = StaticData.Translate("key_dock_my_ships_laser_slots");
						this.lblPlasmaSlots.text = StaticData.Translate("key_dock_my_ships_plasma_slots");
						this.lblIonSlots.text = string.Empty;
						this.DrawSlot1();
						this.DrawSlot2();
						this.DrawSlot5();
						break;
					}
					case 10:
					{
						this.lblLaserSlots.text = StaticData.Translate("key_dock_my_ships_laser_slots");
						this.lblPlasmaSlots.text = StaticData.Translate("key_dock_my_ships_plasma_slots");
						this.lblIonSlots.text = StaticData.Translate("key_dock_my_ships_ion_slots");
						this.DrawSlot1();
						this.DrawSlot2();
						this.DrawSlot3();
						this.DrawSlot4();
						this.DrawSlot6();
						break;
					}
					case 11:
					{
						this.lblLaserSlots.text = StaticData.Translate("key_dock_my_ships_laser_slots");
						this.lblPlasmaSlots.text = StaticData.Translate("key_dock_my_ships_plasma_slots");
						this.lblIonSlots.text = StaticData.Translate("key_dock_my_ships_ion_slots");
						this.DrawSlot1();
						this.DrawSlot2();
						this.DrawSlot3();
						this.DrawSlot4();
						this.DrawSlot5();
						this.DrawSlot6();
						break;
					}
					case 12:
					{
						this.lblLaserSlots.text = string.Empty;
						this.lblPlasmaSlots.text = StaticData.Translate("key_dock_my_ships_plasma_slots");
						this.lblIonSlots.text = string.Empty;
						this.DrawSlot2();
						this.DrawSlot5();
						break;
					}
				}
			}
		}
	}

	public void DropHandler(AndromedaGuiDragDropPlace src, AndromedaGuiDragDropPlace dst)
	{
		this.ClearContent();
		this.CreateWeapons();
	}

	private void OnBuyShipClicked(EventHandlerParam prm)
	{
		this.OnSelectCurrencySelect(prm);
	}

	private void OnCooldownDecrease(EventHandlerParam parm)
	{
		GuiNewDualColorBar valueTwo = this.cooldownBar;
		valueTwo.ValueTwo = valueTwo.ValueTwo - 1f;
		if (this.cooldownBar.ValueTwo < this.cooldownBar.ValueOne)
		{
			this.cooldownBar.ValueTwo = this.cooldownBar.ValueOne;
		}
		this.RefreshPlusMinusButtons();
	}

	private void OnCooldownIncrease(EventHandlerParam parm)
	{
		if (this.CountNewUpgrades() >= 15)
		{
			return;
		}
		GuiNewDualColorBar valueTwo = this.cooldownBar;
		valueTwo.ValueTwo = valueTwo.ValueTwo + 1f;
		if (this.cooldownBar.ValueTwo > 10f)
		{
			this.cooldownBar.ValueTwo = 10f;
		}
		this.RefreshPlusMinusButtons();
	}

	private void OnDamageDecrease(EventHandlerParam parm)
	{
		GuiNewDualColorBar valueTwo = this.damageBar;
		valueTwo.ValueTwo = valueTwo.ValueTwo - 1f;
		if (this.damageBar.ValueTwo < this.damageBar.ValueOne)
		{
			this.damageBar.ValueTwo = this.damageBar.ValueOne;
		}
		this.RefreshPlusMinusButtons();
	}

	private void OnDamageIncrease(EventHandlerParam parm)
	{
		if (this.CountNewUpgrades() >= 15)
		{
			return;
		}
		GuiNewDualColorBar valueTwo = this.damageBar;
		valueTwo.ValueTwo = valueTwo.ValueTwo + 1f;
		if (this.damageBar.ValueTwo > 10f)
		{
			this.damageBar.ValueTwo = 10f;
		}
		this.RefreshPlusMinusButtons();
	}

	public void OnItemSelected(EventHandlerParam parm)
	{
		this.selectedItem = (SlotItem)parm.customData;
		this.PopulateItemPanel();
		this.RefreshPlusMinusButtons();
	}

	private void OnMinusButtonClicked(object prm)
	{
		__DockWindow.<OnMinusButtonClicked>c__AnonStorey8D variable = null;
		__DockWindow.<OnMinusButtonClicked>c__AnonStorey8E variable1 = null;
		__DockWindow _DockWindow = this;
		_DockWindow.desirableUpgrades = _DockWindow.desirableUpgrades - 1;
		if (this.desirableUpgrades <= 0)
		{
			this.desirableUpgrades = 0;
		}
		Enumerable.First<ShipsTypeNet>(Enumerable.Where<ShipsTypeNet>(StaticData.shipTypes, new Func<ShipsTypeNet, bool>(variable, (ShipsTypeNet s) => s.id == this.<>f__this.displayedShip.shipTypeId)));
		if (this.desirableUpgrades <= 0)
		{
			this.desirableUpgradesPrice = 0;
		}
		else
		{
			this.desirableUpgradesPrice = 0;
			for (int i = 1; i <= this.desirableUpgrades; i++)
			{
				__DockWindow _DockWindow1 = this;
				_DockWindow1.desirableUpgradesPrice = _DockWindow1.desirableUpgradesPrice + Enumerable.First<ShipsTypeNet>(Enumerable.Where<ShipsTypeNet>(StaticData.shipTypes, new Func<ShipsTypeNet, bool>(variable1, (ShipsTypeNet s) => (!(s.shipClass == this.<>f__ref$141.myShip.shipClass) || s.tier != this.<>f__ref$141.myShip.tier ? false : s.upgrade == this.i + this.<>f__ref$141.myShip.upgrade)))).price;
			}
		}
		this.PopulateUpdateSection();
	}

	private void OnNewShipClick(object prm)
	{
		this.ClearContent();
		this.selectedTab = __DockWindow.DockWindowTab.NewShips;
		this.mainScreenBG.SetTextureKeepSize("NewGUI", "DockWindowTab1");
		this.CreateNewShipTab();
	}

	private void OnNewShipLeftClicked(object prm)
	{
		if (this.newShipIndex > 0)
		{
			__DockWindow _DockWindow = this;
			_DockWindow.newShipIndex = _DockWindow.newShipIndex - 1;
			this.selectedNewShip = this.newShips[this.newShipIndex];
			this.PopulateShipsGUINew();
		}
	}

	private void OnNewShipRightClicked(object prm)
	{
		if (this.newShipIndex < Enumerable.Count<ShipsTypeNet>(this.newShips) - 1)
		{
			__DockWindow _DockWindow = this;
			_DockWindow.newShipIndex = _DockWindow.newShipIndex + 1;
			this.selectedNewShip = this.newShips[this.newShipIndex];
			this.PopulateShipsGUINew();
		}
	}

	private void OnPenetrationDecrease(EventHandlerParam parm)
	{
		GuiNewDualColorBar valueTwo = this.penetrationBar;
		valueTwo.ValueTwo = valueTwo.ValueTwo - 1f;
		if (this.penetrationBar.ValueTwo < this.penetrationBar.ValueOne)
		{
			this.penetrationBar.ValueTwo = this.penetrationBar.ValueOne;
		}
		this.RefreshPlusMinusButtons();
	}

	private void OnPenetrationIncrease(EventHandlerParam parm)
	{
		if (this.CountNewUpgrades() >= 15)
		{
			return;
		}
		GuiNewDualColorBar valueTwo = this.penetrationBar;
		valueTwo.ValueTwo = valueTwo.ValueTwo + 1f;
		if (this.penetrationBar.ValueTwo > 10f)
		{
			this.penetrationBar.ValueTwo = 10f;
		}
		this.RefreshPlusMinusButtons();
	}

	private void OnPlusButtonClicked(object prm)
	{
		__DockWindow.<OnPlusButtonClicked>c__AnonStorey8B variable = null;
		__DockWindow.<OnPlusButtonClicked>c__AnonStorey8C variable1 = null;
		__DockWindow _DockWindow = this;
		_DockWindow.desirableUpgrades = _DockWindow.desirableUpgrades + 1;
		if (this.desirableUpgrades + this.shipUpgradeLevel >= 10)
		{
			this.desirableUpgrades = 10 - this.shipUpgradeLevel;
		}
		Enumerable.First<ShipsTypeNet>(Enumerable.Where<ShipsTypeNet>(StaticData.shipTypes, new Func<ShipsTypeNet, bool>(variable, (ShipsTypeNet s) => s.id == this.<>f__this.displayedShip.shipTypeId)));
		if (this.desirableUpgrades <= 0)
		{
			this.desirableUpgradesPrice = 0;
		}
		else
		{
			this.desirableUpgradesPrice = 0;
			for (int i = 1; i <= this.desirableUpgrades; i++)
			{
				__DockWindow _DockWindow1 = this;
				_DockWindow1.desirableUpgradesPrice = _DockWindow1.desirableUpgradesPrice + Enumerable.First<ShipsTypeNet>(Enumerable.Where<ShipsTypeNet>(StaticData.shipTypes, new Func<ShipsTypeNet, bool>(variable1, (ShipsTypeNet s) => (!(s.shipClass == this.<>f__ref$139.myShip.shipClass) || s.tier != this.<>f__ref$139.myShip.tier ? false : s.upgrade == this.i + this.<>f__ref$139.myShip.upgrade)))).price;
			}
		}
		this.PopulateUpdateSection();
	}

	private void OnRangeDecrease(EventHandlerParam parm)
	{
		GuiNewDualColorBar valueTwo = this.rangeBar;
		valueTwo.ValueTwo = valueTwo.ValueTwo - 1f;
		if (this.rangeBar.ValueTwo < this.rangeBar.ValueOne)
		{
			this.rangeBar.ValueTwo = this.rangeBar.ValueOne;
		}
		this.RefreshPlusMinusButtons();
	}

	private void OnRangeIncrease(EventHandlerParam parm)
	{
		if (this.CountNewUpgrades() >= 15)
		{
			return;
		}
		GuiNewDualColorBar valueTwo = this.rangeBar;
		valueTwo.ValueTwo = valueTwo.ValueTwo + 1f;
		if (this.rangeBar.ValueTwo > 10f)
		{
			this.rangeBar.ValueTwo = 10f;
		}
		this.RefreshPlusMinusButtons();
	}

	private void OnRepairButtonClicked(object prm)
	{
		// 
		// Current member / type: System.Void __DockWindow::OnRepairButtonClicked(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnRepairButtonClicked(System.Object)
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

	public void OnScrollInventoryLeft(EventHandlerParam parm)
	{
		if (this.CountWeapons() == 0)
		{
			return;
		}
		__DockWindow inventoryScrollIndex = this;
		inventoryScrollIndex.InventoryScrollIndex = inventoryScrollIndex.InventoryScrollIndex - 2;
		this.RefreshInventoryScrollButtons();
		this.ClearContent();
		this.CreateWeapons();
	}

	public void OnScrollInventoryRight(EventHandlerParam parm)
	{
		if (this.CountWeapons() == 0)
		{
			return;
		}
		__DockWindow inventoryScrollIndex = this;
		inventoryScrollIndex.InventoryScrollIndex = inventoryScrollIndex.InventoryScrollIndex + 2;
		this.RefreshInventoryScrollButtons();
		this.ClearContent();
		this.CreateWeapons();
	}

	private void OnSelectCurrencySelect(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void __DockWindow::OnSelectCurrencySelect(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnSelectCurrencySelect(EventHandlerParam)
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

	private void OnSelectShipClicked(object prm)
	{
		this.CreateSelecktShipDialogue(this.displayedShip.ShipID);
	}

	private void OnTargetingDecrease(EventHandlerParam parm)
	{
		GuiNewDualColorBar valueTwo = this.targetingBar;
		valueTwo.ValueTwo = valueTwo.ValueTwo - 1f;
		if (this.targetingBar.ValueTwo < this.targetingBar.ValueOne)
		{
			this.targetingBar.ValueTwo = this.targetingBar.ValueOne;
		}
		this.RefreshPlusMinusButtons();
	}

	private void OnTargetingIncrease(EventHandlerParam parm)
	{
		if (this.CountNewUpgrades() >= 15)
		{
			return;
		}
		GuiNewDualColorBar valueTwo = this.targetingBar;
		valueTwo.ValueTwo = valueTwo.ValueTwo + 1f;
		if (this.targetingBar.ValueTwo > 10f)
		{
			this.targetingBar.ValueTwo = 10f;
		}
		this.RefreshPlusMinusButtons();
	}

	private void OnUpgrade(EventHandlerParam parm)
	{
		// 
		// Current member / type: System.Void __DockWindow::OnUpgrade(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnUpgrade(EventHandlerParam)
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

	private void OnUpgradeButtonClicked(object prm)
	{
		// 
		// Current member / type: System.Void __DockWindow::OnUpgradeButtonClicked(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnUpgradeButtonClicked(System.Object)
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

	private void OnWeaponsClick(object prm)
	{
		this.ClearContent();
		this.selectedItem = null;
		this.selectedTab = __DockWindow.DockWindowTab.Weapons;
		this.mainScreenBG.SetTextureKeepSize("NewGUI", "DockWindowTab2");
		this.CreateWeapons();
	}

	private void PopulateArrowButtons()
	{
		int length = (int)NetworkScript.player.playerBelongings.playerShips.Length;
		this.btnLeftArrow.isEnabled = (this.displayedShipIndex <= 0 ? false : length > 1);
		this.btnRightArrow.isEnabled = (this.displayedShipIndex >= length - 1 ? false : length > 1);
		this.btnLeftBox.isEnabled = this.btnLeftArrow.isEnabled;
		this.btnRightBox.isEnabled = this.btnRightArrow.isEnabled;
	}

	private void PopulateItemPanel()
	{
		if (this.selectedItem == null)
		{
			return;
		}
		this.selectedItemName.text = this.selectedItem.UIName();
		this.selectedItemTexture.SetTexture("WeaponsAvatars", this.selectedItem.AssetName());
		this.selectedItemTexture.boundries = new Rect(378f, 140f, 160f, 110f);
		SlotItemWeapon slotItemWeapon = (SlotItemWeapon)Enumerable.First<SlotItem>(Enumerable.Where<SlotItem>(NetworkScript.player.playerBelongings.playerItems.slotItems, new Func<SlotItem, bool>(this, (SlotItem t) => (t.get_Slot() != this.selectedItem.get_Slot() || t.get_SlotType() != this.selectedItem.get_SlotType() ? false : t.get_ShipId() == this.selectedItem.get_ShipId()))));
		int upgradeCooldown = slotItemWeapon.get_UpgradeCooldown() + slotItemWeapon.get_UpgradeDamage() + slotItemWeapon.get_UpgradePenetration() + slotItemWeapon.get_UpgradeRange() + slotItemWeapon.get_UpgradeTargeting();
		for (int i = 14; i >= upgradeCooldown; i--)
		{
			this.remainingUpgrades[i].SetTexture("NewGUI", "dot_empty");
		}
		GuiNewDualColorBar guiNewDualColorBar = this.damageBar;
		float upgradeDamage = (float)slotItemWeapon.get_UpgradeDamage();
		this.damageBar.ValueTwo = upgradeDamage;
		guiNewDualColorBar.ValueOne = upgradeDamage;
		GuiNewDualColorBar guiNewDualColorBar1 = this.cooldownBar;
		upgradeDamage = (float)slotItemWeapon.get_UpgradeCooldown();
		this.cooldownBar.ValueTwo = upgradeDamage;
		guiNewDualColorBar1.ValueOne = upgradeDamage;
		GuiNewDualColorBar guiNewDualColorBar2 = this.rangeBar;
		upgradeDamage = (float)slotItemWeapon.get_UpgradeRange();
		this.rangeBar.ValueTwo = upgradeDamage;
		guiNewDualColorBar2.ValueOne = upgradeDamage;
		GuiNewDualColorBar guiNewDualColorBar3 = this.penetrationBar;
		upgradeDamage = (float)slotItemWeapon.get_UpgradePenetration();
		this.penetrationBar.ValueTwo = upgradeDamage;
		guiNewDualColorBar3.ValueOne = upgradeDamage;
		GuiNewDualColorBar guiNewDualColorBar4 = this.targetingBar;
		upgradeDamage = (float)slotItemWeapon.get_UpgradeTargeting();
		this.targetingBar.ValueTwo = upgradeDamage;
		guiNewDualColorBar4.ValueOne = upgradeDamage;
	}

	private void PopulateNewShip()
	{
		this.isOwnThisShip = Enumerable.FirstOrDefault<PlayerShipNet>(Enumerable.Where<PlayerShipNet>(NetworkScript.player.playerBelongings.playerShips, new Func<PlayerShipNet, bool>(this, (PlayerShipNet t) => (t.shipTypeId < this.selectedNewShip.id ? false : t.shipTypeId <= this.selectedNewShip.id + 10)))) != null;
		this.lblNewShipName.text = StaticData.Translate(this.selectedNewShip.shipName);
		if (this.newShipIndex <= 0)
		{
			this.leftShip.SetTextureKeepSize("FrameworkGUI", "empty");
		}
		else
		{
			string str = this.newShips[this.newShipIndex - 1].assetName;
			this.leftShip.SetTextureKeepSize("ShipsAvatars", str);
		}
		if (this.newShipIndex >= Enumerable.Count<ShipsTypeNet>(this.newShips) - 1)
		{
			this.rightShip.SetTextureKeepSize("FrameworkGUI", "empty");
		}
		else
		{
			string str1 = this.newShips[this.newShipIndex + 1].assetName;
			this.rightShip.SetTextureKeepSize("ShipsAvatars", str1);
		}
		string str2 = this.selectedNewShip.assetName;
		this.middleShip.SetTextureKeepSize("ShipsAvatars", str2);
		this.lblNewShipPriceCash.text = this.selectedNewShip.price.ToString("##,##0");
		GuiLabel guiLabel = this.lblNewShipPriceNova;
		double num = (double)this.selectedNewShip.price * 0.9 / (double)this.exchangeRate;
		guiLabel.text = num.ToString("##,##0");
		this.lbl_newShip_shildValue.text = this.selectedNewShip.shield.ToString("##,##0");
		this.lbl_newShip_hullValue.text = this.selectedNewShip.corpus.ToString("##,##0");
		this.lbl_newShip_speedValue.text = this.selectedNewShip.speed.ToString("##,##0");
		this.lbl_newShip_avoidanceValue.text = this.selectedNewShip.avoidance.ToString("##,##0");
		this.lbl_newShip_targetingValue.text = this.selectedNewShip.targeting.ToString("##,##0");
		this.lbl_newShip_cargoValue.text = this.selectedNewShip.cargo.ToString("##,##0");
		this.bar_newShip_shield.ValueOne = (float)this.selectedNewShip.shield;
		this.bar_newShip_hull.ValueOne = (float)this.selectedNewShip.corpus;
		this.bar_newShip_speed.ValueOne = (float)this.selectedNewShip.speed;
		this.bar_newShip_avoidance.ValueOne = (float)this.selectedNewShip.avoidance;
		this.bar_newShip_targeting.ValueOne = (float)this.selectedNewShip.targeting;
		this.bar_newShip_cargo.ValueOne = (float)this.selectedNewShip.cargo;
		this.newShipLeftArrow.isEnabled = (this.newShipIndex != 0 ? true : false);
		this.newShipRightArrow.isEnabled = (this.newShipIndex >= Enumerable.Count<ShipsTypeNet>(this.newShips) - 1 ? false : true);
		this.newShipLeftBox.isEnabled = this.newShipLeftArrow.isEnabled;
		this.newShipRightBox.isEnabled = this.newShipRightArrow.isEnabled;
		if (!this.isOwnThisShip)
		{
			this.ownershipStamp.SetTextureKeepSize("FrameworkGUI", "empty");
		}
		else
		{
			this.ownershipStamp.SetTextureKeepSize("NewGUI", "alreadyOwned");
		}
		this.possitionX = 328;
		this.lblLaserSlots.X = 332f;
		this.lblPlasmaSlots.X = 422f;
		this.lblIonSlots.X = 512f;
		this.DrawWeaponsConfig(this.selectedNewShip.shipName);
	}

	private void PopulateRepairSection()
	{
		__DockWindow.<PopulateRepairSection>c__AnonStorey88 variable = null;
		this.RemoveRepairAnimation();
		PlayerShipNet playerShipNet = Enumerable.FirstOrDefault<PlayerShipNet>(Enumerable.Where<PlayerShipNet>(NetworkScript.player.playerBelongings.playerShips, new Func<PlayerShipNet, bool>(variable, (PlayerShipNet t) => (t.shipTypeId < this.<>f__this.selectedNewShip.id ? false : t.shipTypeId <= this.<>f__this.selectedNewShip.id + 10))));
		if (playerShipNet == null)
		{
			Debug.Log(" PopulateRepairSection displayedShip null");
			return;
		}
		this.repairPrice = Enumerable.First<ShipsTypeNet>(Enumerable.Where<ShipsTypeNet>(StaticData.shipTypes, new Func<ShipsTypeNet, bool>(variable, (ShipsTypeNet t) => t.id == this.displayedShip.shipTypeId))).repairPrice;
		if (playerShipNet.ShieldHP >= playerShipNet.Shield && playerShipNet.CorpusHP >= playerShipNet.Corpus)
		{
			this.btnRepair.isEnabled = false;
		}
		else if (NetworkScript.player.playerBelongings.playerItems.get_Cash() >= (long)this.repairPrice)
		{
			this.btnRepair.isEnabled = true;
			this.btnRepair.ToolTipText = null;
		}
		else
		{
			this.btnRepair.isEnabled = true;
		}
		if ((float)playerShipNet.CorpusHP >= (float)playerShipNet.Corpus * 0.3f)
		{
			this.RemoveRepairAnimation();
		}
		else
		{
			this.PutRepairAnimation();
		}
		this.repairPriceLbl.text = string.Format(StaticData.Translate("key_dock_my_ships_repair"), this.repairPrice);
		this.lbl_curentShildValue.text = string.Concat(playerShipNet.ShieldHP.ToString(), "/", playerShipNet.Shield.ToString());
		this.lbl_curentHullValue.text = string.Concat(playerShipNet.CorpusHP.ToString(), "/", playerShipNet.Corpus.ToString());
		this.bar_curentShield.current = (float)playerShipNet.ShieldHP;
		this.bar_curentShield.maximum = (float)playerShipNet.Shield;
		this.bar_curentHull.current = (float)playerShipNet.CorpusHP;
		this.bar_curentHull.maximum = (float)playerShipNet.Corpus;
	}

	private void PopulateSelectShip()
	{
		if (this.displayedShip.ShipID != NetworkScript.player.playerBelongings.selectedShipId)
		{
			int num = this.selectedNewShip.levelRestriction;
			if (this.btnSelectShip == null)
			{
				this.btnSelectShip = new GuiButtonResizeable();
				this.btnSelectShip.SetSmallOrangeTexture();
				this.btnSelectShip.X = 397f;
				this.btnSelectShip.Y = 263f;
				this.btnSelectShip.Width = 125f;
				this.btnSelectShip.Caption = StaticData.Translate("key_dock_my_ships_select_ship");
				this.btnSelectShip.FontSize = 14;
				this.btnSelectShip.MarginTop = 4;
				this.btnSelectShip.Alignment = 1;
				this.btnSelectShip.Clicked = new Action<EventHandlerParam>(this, __DockWindow.OnSelectShipClicked);
				base.AddGuiElement(this.btnSelectShip);
				this.forDelete.Add(this.btnSelectShip);
			}
			this.btnSelectShip.isEnabled = num <= NetworkScript.player.playerBelongings.playerLevel;
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
			this.curentShipFlag.SetTextureKeepSize("FrameworkGUI", "empty");
		}
		else
		{
			if (this.btnSelectShip != null)
			{
				base.RemoveGuiElement(this.btnSelectShip);
				this.btnSelectShip = null;
			}
			this.curentShipFlag.SetTextureKeepSize("NewGUI", "curentShipStamp");
		}
	}

	private void PopulateShipDataNew()
	{
		this.displayedShip = Enumerable.FirstOrDefault<PlayerShipNet>(Enumerable.Where<PlayerShipNet>(NetworkScript.player.playerBelongings.playerShips, new Func<PlayerShipNet, bool>(this, (PlayerShipNet t) => (t.shipTypeId < this.selectedNewShip.id ? false : t.shipTypeId <= this.selectedNewShip.id + 10))));
		if (this.displayedShip == null)
		{
			Debug.Log("displayedShip null");
			return;
		}
		this.shipUpgradeLevel = Enumerable.First<ShipsTypeNet>(Enumerable.Where<ShipsTypeNet>(StaticData.shipTypes, new Func<ShipsTypeNet, bool>(this, (ShipsTypeNet s) => s.id == this.displayedShip.shipTypeId))).upgrade;
		this.lbl_upgradeLvl.text = string.Format(StaticData.Translate("key_dock_my_ships_upgrade_level"), this.shipUpgradeLevel);
		this.lbl_shildValue.text = this.displayedShip.Shield.ToString("##,##0");
		this.lbl_hullValue.text = this.displayedShip.Corpus.ToString("##,##0");
		this.lbl_speedValue.text = this.displayedShip.Speed.ToString("##,##0");
		this.lbl_avoidanceValue.text = this.displayedShip.Avoidance.ToString("##,##0");
		this.lbl_targetingValue.text = this.displayedShip.Targeting.ToString("##,##0");
		this.lbl_cargoValue.text = this.displayedShip.MaxCargo.ToString("##,##0");
		this.bar_shield.ValueOne = (float)this.shipUpgradeLevel;
		this.bar_hull.ValueOne = (float)this.shipUpgradeLevel;
		this.bar_speed.ValueOne = (float)this.shipUpgradeLevel;
		this.bar_avoidance.ValueOne = (float)this.shipUpgradeLevel;
		this.bar_targeting.ValueOne = (float)this.shipUpgradeLevel;
		this.bar_cargo.ValueOne = (float)this.shipUpgradeLevel;
		this.btnUpgrade.isEnabled = false;
		this.PopulateRepairSection();
		this.PopulateUpdateSection();
		this.PopulateSelectShip();
		this.possitionX = 603;
		this.lblLaserSlots.X = 606f;
		this.lblPlasmaSlots.X = 696f;
		this.lblIonSlots.X = 786f;
		this.DrawWeaponsConfig(this.displayedShip.ShipTitle);
	}

	private void PopulateShipsGUINew()
	{
		ShipsTypeNet[] shipsTypeNetArray = StaticData.shipTypes;
		if (__DockWindow.<>f__am$cacheA4 == null)
		{
			__DockWindow.<>f__am$cacheA4 = new Func<ShipsTypeNet, bool>(null, (ShipsTypeNet s) => s.upgrade == 0);
		}
		IEnumerable<ShipsTypeNet> enumerable = Enumerable.Where<ShipsTypeNet>(shipsTypeNetArray, __DockWindow.<>f__am$cacheA4);
		if (__DockWindow.<>f__am$cacheA5 == null)
		{
			__DockWindow.<>f__am$cacheA5 = new Func<ShipsTypeNet, int>(null, (ShipsTypeNet t) => t.price);
		}
		this.newShips = Enumerable.ToArray<ShipsTypeNet>(Enumerable.OrderBy<ShipsTypeNet, int>(enumerable, __DockWindow.<>f__am$cacheA5));
		this.selectedNewShip = this.newShips[this.newShipIndex];
		this.isOwnThisShip = Enumerable.FirstOrDefault<PlayerShipNet>(Enumerable.Where<PlayerShipNet>(NetworkScript.player.playerBelongings.playerShips, new Func<PlayerShipNet, bool>(this, (PlayerShipNet t) => (t.shipTypeId < this.selectedNewShip.id ? false : t.shipTypeId <= this.selectedNewShip.id + 10)))) != null;
		this.ClearContent();
		this.DrawShipsCommon();
		if (!this.isOwnThisShip)
		{
			this.DrawNewShipsGui();
			this.DrawNewShipStats();
			this.PopulateNewShip();
		}
		else
		{
			this.DrawMyShipGui();
			this.PopulateShipDataNew();
		}
	}

	private void PopulateUpdateSection()
	{
		__DockWindow.<PopulateUpdateSection>c__AnonStorey89 variable = null;
		if (this.desirableUpgrades <= 0)
		{
			this.btnUpgrade.isEnabled = false;
		}
		else
		{
			this.btnUpgrade.isEnabled = true;
			if (NetworkScript.player.playerBelongings.playerItems.get_Cash() < (long)this.desirableUpgradesPrice)
			{
				this.btnUpgrade.isEnabled = false;
			}
		}
		this.bar_shield.ValueTwo = (float)(this.desirableUpgrades + this.shipUpgradeLevel);
		this.bar_hull.ValueTwo = (float)(this.desirableUpgrades + this.shipUpgradeLevel);
		this.bar_speed.ValueTwo = (float)(this.desirableUpgrades + this.shipUpgradeLevel);
		this.bar_avoidance.ValueTwo = (float)(this.desirableUpgrades + this.shipUpgradeLevel);
		this.bar_targeting.ValueTwo = (float)(this.desirableUpgrades + this.shipUpgradeLevel);
		this.bar_cargo.ValueTwo = (float)(this.desirableUpgrades + this.shipUpgradeLevel);
		this.btnUpgrade.Caption = string.Format(StaticData.Translate("key_dock_weapons_upgrade_price"), this.desirableUpgradesPrice);
		for (int i = 0; i < 10; i++)
		{
			if (i >= this.shipUpgradeLevel + this.desirableUpgrades)
			{
				this.remainingUpgrades[i].SetTextureKeepSize("NewGUI", "dot_empty");
			}
			else
			{
				this.remainingUpgrades[i].SetTextureKeepSize("NewGUI", "dot_full_blue");
			}
		}
		if (this.desirableUpgrades != 0)
		{
			this.btnNewMinus.isEnabled = true;
		}
		else
		{
			this.btnNewMinus.isEnabled = false;
		}
		if (this.shipUpgradeLevel + this.desirableUpgrades != 10)
		{
			this.btnNewPlus.isEnabled = true;
		}
		else
		{
			this.btnNewPlus.isEnabled = false;
		}
		if (this.desirableUpgrades <= 0)
		{
			this.lbl_shildValue.text = this.displayedShip.Shield.ToString("##,##0");
			this.lbl_shildValue.TextColor = Color.get_white();
			this.lbl_hullValue.text = this.displayedShip.Corpus.ToString("##,##0");
			this.lbl_hullValue.TextColor = Color.get_white();
			this.lbl_speedValue.text = this.displayedShip.Speed.ToString("##,##0");
			this.lbl_speedValue.TextColor = Color.get_white();
			this.lbl_avoidanceValue.text = this.displayedShip.Avoidance.ToString("##,##0");
			this.lbl_avoidanceValue.TextColor = Color.get_white();
			this.lbl_targetingValue.text = this.displayedShip.Targeting.ToString("##,##0");
			this.lbl_targetingValue.TextColor = Color.get_white();
			this.lbl_cargoValue.text = this.displayedShip.MaxCargo.ToString("##,##0");
			this.lbl_cargoValue.TextColor = Color.get_white();
		}
		else
		{
			ShipsTypeNet shipsTypeNet = Enumerable.First<ShipsTypeNet>(Enumerable.Where<ShipsTypeNet>(StaticData.shipTypes, new Func<ShipsTypeNet, bool>(variable, (ShipsTypeNet s) => s.id == this.<>f__this.displayedShip.shipTypeId)));
			ShipsTypeNet shipsTypeNet1 = Enumerable.First<ShipsTypeNet>(Enumerable.Where<ShipsTypeNet>(StaticData.shipTypes, new Func<ShipsTypeNet, bool>(variable, (ShipsTypeNet s) => (!(s.shipClass == this.myShip.shipClass) || s.tier != this.myShip.tier ? false : s.upgrade == this.<>f__this.desirableUpgrades + this.myShip.upgrade))));
			this.lbl_shildValue.text = string.Format("{0:##,##0} + {1:##,##0}", this.displayedShip.Shield, shipsTypeNet1.shield - shipsTypeNet.shield);
			this.lbl_shildValue.TextColor = GuiNewStyleBar.orangeColor;
			this.lbl_hullValue.text = string.Format("{0:##,##0} + {1:##,##0}", this.displayedShip.Corpus, shipsTypeNet1.corpus - shipsTypeNet.corpus);
			this.lbl_hullValue.TextColor = GuiNewStyleBar.orangeColor;
			this.lbl_speedValue.text = string.Format("{0:##,##0} + {1:##,##0}", this.displayedShip.Speed, (int)(shipsTypeNet1.speed - shipsTypeNet.speed));
			this.lbl_speedValue.TextColor = GuiNewStyleBar.orangeColor;
			this.lbl_avoidanceValue.text = string.Format("{0:##,##0} + {1:##,##0}", this.displayedShip.Avoidance, (int)(shipsTypeNet1.avoidance - shipsTypeNet.avoidance));
			this.lbl_avoidanceValue.TextColor = GuiNewStyleBar.orangeColor;
			this.lbl_targetingValue.text = string.Format("{0:##,##0} + {1:##,##0}", this.displayedShip.Targeting, (int)(shipsTypeNet1.targeting - shipsTypeNet.targeting));
			this.lbl_targetingValue.TextColor = GuiNewStyleBar.orangeColor;
			this.lbl_cargoValue.text = string.Format("{0:##,##0} + {1:##,##0}", this.displayedShip.MaxCargo, shipsTypeNet1.cargo - shipsTypeNet.cargo);
			this.lbl_cargoValue.TextColor = GuiNewStyleBar.orangeColor;
		}
	}

	private void PopulateWeaponStats()
	{
		if (this.selectedItem == null)
		{
			return;
		}
		SlotItemWeapon slotItemWeapon = (SlotItemWeapon)Enumerable.First<SlotItem>(Enumerable.Where<SlotItem>(NetworkScript.player.playerBelongings.playerItems.slotItems, new Func<SlotItem, bool>(this, (SlotItem t) => (t.get_Slot() != this.selectedItem.get_Slot() || t.get_SlotType() != this.selectedItem.get_SlotType() ? false : t.get_ShipId() == this.selectedItem.get_ShipId()))));
		WeaponsTypeNet item = (WeaponsTypeNet)StaticData.allTypes.get_Item(slotItemWeapon.get_ItemType());
		int num = item.upgrades[(int)this.damageBar.ValueOne].damage;
		int num1 = item.upgrades[(int)this.damageBar.ValueTwo].damage;
		this.damageValLabel.text = (num == num1 ? slotItemWeapon.get_DamageTotal().ToString() : string.Format(StaticData.Translate("key_dock_weapons_upgrade_stats_all"), slotItemWeapon.get_DamageTotal(), num1 - num));
		this.damageValLabel.TextColor = (this.damageBar.ValueOne != this.damageBar.ValueTwo ? GuiNewStyleBar.orangeColor : Color.get_white());
		int num2 = item.upgrades[(int)this.cooldownBar.ValueOne].cooldown;
		int num3 = item.upgrades[(int)this.cooldownBar.ValueTwo].cooldown;
		this.cooldownValLabel.text = (num2 == num3 ? string.Format(StaticData.Translate("key_dock_weapons_upgrade_stats_cooldown_base"), (float)slotItemWeapon.get_CooldownTotal() * 0.001f) : string.Format(StaticData.Translate("key_dock_weapons_upgrade_stats_cooldown"), (float)slotItemWeapon.get_CooldownTotal() * 0.001f, (float)(num2 - num3) * 0.001f));
		this.cooldownValLabel.TextColor = (this.cooldownBar.ValueOne != this.cooldownBar.ValueTwo ? GuiNewStyleBar.orangeColor : Color.get_white());
		int num4 = item.upgrades[(int)this.rangeBar.ValueOne].range;
		int num5 = item.upgrades[(int)this.rangeBar.ValueTwo].range;
		this.rangeValLabel.text = (num4 == num5 ? slotItemWeapon.get_RangeTotal().ToString() : string.Format(StaticData.Translate("key_dock_weapons_upgrade_stats_all"), slotItemWeapon.get_RangeTotal(), num5 - num4));
		this.rangeValLabel.TextColor = (this.rangeBar.ValueOne != this.rangeBar.ValueTwo ? GuiNewStyleBar.orangeColor : Color.get_white());
		int num6 = item.upgrades[(int)this.penetrationBar.ValueOne].penetration;
		int num7 = item.upgrades[(int)this.penetrationBar.ValueTwo].penetration;
		this.penetrationValLabel.text = (num6 == num7 ? slotItemWeapon.get_PenetrationTotal().ToString() : string.Format(StaticData.Translate("key_dock_weapons_upgrade_stats_all"), slotItemWeapon.get_PenetrationTotal(), num7 - num6));
		this.penetrationValLabel.TextColor = (this.penetrationBar.ValueOne != this.penetrationBar.ValueTwo ? GuiNewStyleBar.orangeColor : Color.get_white());
		int num8 = item.upgrades[(int)this.targetingBar.ValueOne].targeting;
		int num9 = item.upgrades[(int)this.targetingBar.ValueTwo].targeting;
		this.targetingValLabel.text = (num8 == num9 ? slotItemWeapon.get_TargetingTotal().ToString() : string.Format(StaticData.Translate("key_dock_weapons_upgrade_stats_all"), slotItemWeapon.get_TargetingTotal(), num9 - num8));
		this.targetingValLabel.TextColor = (this.targetingBar.ValueOne != this.targetingBar.ValueTwo ? GuiNewStyleBar.orangeColor : Color.get_white());
	}

	private void PutRepairAnimation()
	{
		base.RemoveGuiElement(this.repairPriceLbl);
		this.repairBtnAnimationLeft = new GuiTextureAnimated();
		this.repairBtnAnimationLeft.Init("BlinkingSmallButtonLeft", "BlinkingSmallButtonLeft", "BlinkingSmallButtonLeft/btn_left_");
		this.repairBtnAnimationLeft.rotationTime = 1.5f;
		this.repairBtnAnimationLeft.boundries = new Rect(this.btnRepair.boundries.get_x(), this.btnRepair.boundries.get_y(), 10f, 25f);
		base.AddGuiElement(this.repairBtnAnimationLeft);
		this.forDelete.Add(this.repairBtnAnimationLeft);
		this.repairBtnAnimationMiddle = new GuiTextureAnimated();
		this.repairBtnAnimationMiddle.Init("BlinkingSmallButtonMiddle", "BlinkingSmallButtonMiddle", "BlinkingSmallButtonMiddle/btn_center_");
		this.repairBtnAnimationMiddle.rotationTime = 1.5f;
		this.repairBtnAnimationMiddle.boundries = new Rect(this.btnRepair.boundries.get_x() + 10f, this.btnRepair.boundries.get_y(), this.btnRepair.boundries.get_width() - 20f, 25f);
		base.AddGuiElement(this.repairBtnAnimationMiddle);
		this.forDelete.Add(this.repairBtnAnimationMiddle);
		this.repairBtnAnimationRight = new GuiTextureAnimated();
		this.repairBtnAnimationRight.Init("BlinkingSmallButtonRight", "BlinkingSmallButtonRight", "BlinkingSmallButtonRight/btn_right_");
		this.repairBtnAnimationRight.rotationTime = 1.5f;
		this.repairBtnAnimationRight.boundries = new Rect(this.btnRepair.boundries.get_x() + this.btnRepair.boundries.get_width() - 10f, this.btnRepair.boundries.get_y(), 10f, 25f);
		base.AddGuiElement(this.repairBtnAnimationRight);
		this.forDelete.Add(this.repairBtnAnimationRight);
		this.repairPriceLbl.TextColor = new Color(0.0431f, 0.1803f, 0.3058f);
		base.AddGuiElement(this.repairPriceLbl);
	}

	private void RefreshInventoryScrollButtons()
	{
		int num = this.CountWeapons();
		this.inventoryScrollLeftButton.isEnabled = this.InventoryScrollIndex != 0;
		this.inventoryScrollRightButton.isEnabled = this.InventoryScrollIndex < num - 12;
	}

	private void RefreshPlusMinusButtons()
	{
		if (this.selectedItem == null)
		{
			this.damageMinusButton.isEnabled = false;
			this.damagePlusButton.isEnabled = false;
			this.cooldownMinusButton.isEnabled = false;
			this.cooldownPlusButton.isEnabled = false;
			this.rangeMinusButton.isEnabled = false;
			this.rangePlusButton.isEnabled = false;
			this.penetrationMinusButton.isEnabled = false;
			this.penetrationPlusButton.isEnabled = false;
			this.targetingMinusButton.isEnabled = false;
			this.targetingPlusButton.isEnabled = false;
			this.upgradeButton.isEnabled = false;
			return;
		}
		if (this.CountNewUpgrades() < 15)
		{
			this.damagePlusButton.isEnabled = true;
			this.cooldownPlusButton.isEnabled = true;
			this.rangePlusButton.isEnabled = true;
			this.penetrationPlusButton.isEnabled = true;
			this.targetingPlusButton.isEnabled = true;
		}
		else
		{
			this.damagePlusButton.isEnabled = false;
			this.cooldownPlusButton.isEnabled = false;
			this.rangePlusButton.isEnabled = false;
			this.penetrationPlusButton.isEnabled = false;
			this.targetingPlusButton.isEnabled = false;
		}
		if (this.damageBar.ValueTwo >= 10f)
		{
			this.damagePlusButton.isEnabled = false;
		}
		if (this.cooldownBar.ValueTwo >= 10f)
		{
			this.cooldownPlusButton.isEnabled = false;
		}
		if (this.rangeBar.ValueTwo >= 10f)
		{
			this.rangePlusButton.isEnabled = false;
		}
		if (this.penetrationBar.ValueTwo >= 10f)
		{
			this.penetrationPlusButton.isEnabled = false;
		}
		if (this.targetingBar.ValueTwo >= 10f)
		{
			this.targetingPlusButton.isEnabled = false;
		}
		if (this.damageBar.ValueTwo - this.damageBar.ValueOne != 0f)
		{
			this.damageMinusButton.isEnabled = true;
		}
		else
		{
			this.damageMinusButton.isEnabled = false;
		}
		if (this.cooldownBar.ValueTwo - this.cooldownBar.ValueOne != 0f)
		{
			this.cooldownMinusButton.isEnabled = true;
		}
		else
		{
			this.cooldownMinusButton.isEnabled = false;
		}
		if (this.rangeBar.ValueTwo - this.rangeBar.ValueOne != 0f)
		{
			this.rangeMinusButton.isEnabled = true;
		}
		else
		{
			this.rangeMinusButton.isEnabled = false;
		}
		if (this.penetrationBar.ValueTwo - this.penetrationBar.ValueOne != 0f)
		{
			this.penetrationMinusButton.isEnabled = true;
		}
		else
		{
			this.penetrationMinusButton.isEnabled = false;
		}
		if (this.targetingBar.ValueTwo - this.targetingBar.ValueOne != 0f)
		{
			this.targetingMinusButton.isEnabled = true;
		}
		else
		{
			this.targetingMinusButton.isEnabled = false;
		}
		if (this.CountUpgrades() - this.CountNewUpgrades() != 0)
		{
			this.upgradeButton.isEnabled = true;
		}
		else
		{
			this.upgradeButton.isEnabled = false;
		}
		int num = this.CountNewUpgrades();
		int num1 = this.CountUpgrades();
		for (int i = 0; i < 15; i++)
		{
			if (i < num1)
			{
				this.remainingUpgrades[i].SetTexture("NewGUI", "dot_full_blue");
			}
			else if (i >= num)
			{
				this.remainingUpgrades[i].SetTexture("NewGUI", "dot_empty");
			}
			else
			{
				this.remainingUpgrades[i].SetTexture("NewGUI", "dot_full_orange");
			}
		}
		if (this.selectedItem == null)
		{
			return;
		}
		SlotItemWeapon slotItemWeapon = (SlotItemWeapon)this.selectedItem;
		long num2 = (long)0;
		long num3 = (long)0;
		for (int j = num1; j < num; j++)
		{
			WeaponsTypeNet item = (WeaponsTypeNet)StaticData.allTypes.get_Item(slotItemWeapon.get_ItemType());
			num2 = num2 + (j >= 9 ? item.upgrades[9].price : item.upgrades[j].price);
		}
		this.upgradeButton.Caption = string.Format(StaticData.Translate("key_dock_weapons_upgrade_price"), num2);
		this.upgradeButton.isEnabled = (NetworkScript.player.playerBelongings.playerItems.get_Cash() <= num2 ? false : num2 > (long)0);
		this.PopulateWeaponStats();
	}

	private void RemoveRepairAnimation()
	{
		if (this.repairBtnAnimationLeft != null)
		{
			base.RemoveGuiElement(this.repairBtnAnimationLeft);
			this.repairBtnAnimationLeft = null;
		}
		if (this.repairBtnAnimationMiddle != null)
		{
			base.RemoveGuiElement(this.repairBtnAnimationMiddle);
			this.repairBtnAnimationMiddle = null;
		}
		if (this.repairBtnAnimationRight != null)
		{
			base.RemoveGuiElement(this.repairBtnAnimationRight);
			this.repairBtnAnimationRight = null;
		}
		this.repairPriceLbl.TextColor = Color.get_white();
	}

	private void SelectShipWithEquip(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void __DockWindow::SelectShipWithEquip(EventHandlerParam)
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
		// Current member / type: System.Void __DockWindow::SelectShipWithoutEquip(EventHandlerParam)
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

	private void UpdateRepairLbl()
	{
		__DockWindow _deltaTime = this;
		_deltaTime.colorAlpha = _deltaTime.colorAlpha + Time.get_deltaTime() * this.colorAlphaHelper * 0.9f;
		if (this.colorAlpha < 0.2f)
		{
			this.colorAlpha = 0.2f;
			this.colorAlphaHelper = 1f;
		}
		else if (this.colorAlpha > 1f)
		{
			this.colorAlpha = 1f;
			this.colorAlphaHelper = -1f;
		}
	}

	private GuiWindow WeaponsTooltip(object prm)
	{
		GuiWindow guiWindow = new GuiWindow();
		guiWindow.SetBackgroundTexture("ConfigWindow", "InventoryTooltipBackground");
		Rect rect = (Rect)((EventHandlerParam)prm).customData2;
		float _x = rect.get_x() + 50f;
		Rect rect1 = (Rect)((EventHandlerParam)prm).customData2;
		float _y = rect1.get_y() + 2f;
		guiWindow.boundries = new Rect(_x, _y, guiWindow.boundries.get_width(), guiWindow.boundries.get_height());
		guiWindow.zOrder = 230;
		guiWindow.ignoreClickEvents = true;
		guiWindow.isHidden = false;
		(new AndromedaGuiDragDropPlace()).CreateNewSlotItemTooltip((SlotItem)((EventHandlerParam)prm).customData, guiWindow);
		return guiWindow;
	}

	public enum DockWindowTab
	{
		MyShips,
		NewShips,
		Weapons
	}
}