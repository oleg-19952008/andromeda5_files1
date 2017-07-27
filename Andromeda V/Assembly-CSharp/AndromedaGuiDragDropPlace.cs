using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AndromedaGuiDragDropPlace
{
	public static ushort idCounter;

	public ushort id;

	public bool isEmpty;

	public ItemLocation location = 1;

	public SlotItem item;

	public byte slot;

	public int shipId;

	public Vector2 position;

	public Vector2 dragItemTextureSize;

	public Vector2 idleItemTextureSize;

	public Texture2D txFrameIdle;

	public Texture2D txFrameDisabled;

	public Texture2D txFrameDropTarget;

	public Texture2D txFrameDropTargetHover;

	public Texture2D txFrameDragSource;

	public GuiLabel lblAmount;

	public Vector2 dropZonePosition;

	public GuiTextureDraggable txItem;

	public List<AndromedaGuiDragDropPlace> dropTargets = new List<AndromedaGuiDragDropPlace>();

	public string assetBundle = "Shop";

	public Action<SlotItem> selectedCallback;

	public GuiButtonFixed btn;

	public GuiTexture frameTexture;

	public GuiTexture glowTexture;

	public string glowTexturePrefix = "InventorySlot";

	static AndromedaGuiDragDropPlace()
	{
		AndromedaGuiDragDropPlace.idCounter = 555;
	}

	public AndromedaGuiDragDropPlace()
	{
		ushort num = AndromedaGuiDragDropPlace.idCounter;
		AndromedaGuiDragDropPlace.idCounter = (ushort)(num + 1);
		this.id = num;
	}

	private void AddPriceInTooltip(GuiWindow window, int priceCash, int priceNova, int priceViral, int offsetY, bool isSellPrice = false)
	{
		float single = (float)offsetY;
		int num = 0;
		if (priceCash != 0)
		{
			num++;
		}
		if (priceNova != 0)
		{
			num++;
		}
		if (priceViral != 0)
		{
			num++;
		}
		switch (num)
		{
			case 1:
			{
				if (priceCash != 0)
				{
					GuiTexture guiTexture = new GuiTexture()
					{
						boundries = new Rect(0f, single + 1f, 18f, 18f)
					};
					guiTexture.SetTextureKeepSize("FrameworkGUI", "cashIcon");
					window.AddGuiElement(guiTexture);
					GuiLabel guiLabel = new GuiLabel()
					{
						TextColor = GuiNewStyleBar.blueColor,
						FontSize = 18,
						text = priceCash.ToString("##,##0"),
						boundries = new Rect(0f, single, guiLabel.TextWidth + 5f, 18f),
						Alignment = 3
					};
					window.AddGuiElement(guiLabel);
					guiTexture.X = (window.boundries.get_width() - guiLabel.TextWidth - 18f) / 2f;
					if (!isSellPrice)
					{
						float textWidth = 18f + guiLabel.TextWidth;
						float _width = (window.boundries.get_width() - textWidth) / 2f;
						guiTexture.boundries.set_x(_width);
						guiLabel.boundries.set_x(_width + 18f);
					}
					else
					{
						GuiLabel guiLabel1 = new GuiLabel()
						{
							TextColor = GuiNewStyleBar.blueColor,
							FontSize = 14,
							text = string.Concat(StaticData.Translate("key_inventory_sell_price"), ":"),
							boundries = new Rect(0f, single, guiLabel1.TextWidth + 5f, 18f),
							Alignment = 3
						};
						window.AddGuiElement(guiLabel1);
						guiLabel1.boundries.set_width(guiLabel1.TextWidth);
						guiLabel.boundries.set_width(guiLabel.TextWidth);
						float textWidth1 = 18f + guiLabel.TextWidth + guiLabel1.TextWidth;
						float _width1 = (window.boundries.get_width() - textWidth1) / 2f;
						guiLabel1.X = _width1;
						guiTexture.boundries.set_x(_width1 + guiLabel1.boundries.get_width());
						guiLabel.boundries.set_x(_width1 + guiLabel1.boundries.get_width() + 18f);
					}
				}
				else if (priceNova != 0)
				{
					GuiTexture guiTexture1 = new GuiTexture()
					{
						boundries = new Rect(20f, single + 1f, 18f, 18f)
					};
					guiTexture1.SetTextureKeepSize("FrameworkGUI", "novaIcon");
					window.AddGuiElement(guiTexture1);
					GuiLabel guiLabel2 = new GuiLabel()
					{
						TextColor = GuiNewStyleBar.orangeColor,
						FontSize = 18,
						text = priceNova.ToString("##,##0"),
						boundries = new Rect((window.boundries.get_width() - guiLabel2.TextWidth - 18f) / 2f + 18f, single, guiLabel2.TextWidth + 5f, 18f),
						Alignment = 3
					};
					window.AddGuiElement(guiLabel2);
					guiTexture1.X = (window.boundries.get_width() - guiLabel2.TextWidth - 18f) / 2f;
				}
				else if (priceViral != 0)
				{
					GuiTexture guiTexture2 = new GuiTexture()
					{
						boundries = new Rect(20f, single + 1f, 18f, 18f)
					};
					guiTexture2.SetTextureKeepSize("FrameworkGUI", "novaIcon");
					window.AddGuiElement(guiTexture2);
					GuiLabel guiLabel3 = new GuiLabel()
					{
						TextColor = GuiNewStyleBar.purpleColor,
						FontSize = 18,
						text = priceViral.ToString("##,##0"),
						boundries = new Rect((window.boundries.get_width() - guiLabel3.TextWidth - 18f) / 2f + 18f, single, guiLabel3.TextWidth + 5f, 18f),
						Alignment = 3
					};
					window.AddGuiElement(guiLabel3);
					guiTexture2.X = (window.boundries.get_width() - guiLabel3.TextWidth - 18f) / 2f;
				}
				break;
			}
			case 2:
			{
				if (priceCash == 0)
				{
					GuiTexture rect = new GuiTexture();
					GuiTexture rect1 = new GuiTexture();
					GuiLabel str = new GuiLabel();
					GuiLabel str1 = new GuiLabel();
					str.FontSize = 18;
					str1.FontSize = 18;
					rect.boundries = new Rect(0f, single + 1f, 18f, 18f);
					rect1.boundries = new Rect(0f, single + 1f, 18f, 18f);
					str.text = priceNova.ToString("##,##0");
					str1.text = priceViral.ToString("##,##0");
					float single1 = (window.boundries.get_width() - str.TextWidth - str1.TextWidth - 54f) / 2f;
					rect.X = single1;
					rect1.X = single1 + str.TextWidth + 42f;
					rect.SetTextureKeepSize("FrameworkGUI", "novaIcon");
					rect1.SetTextureKeepSize("FrameworkGUI", "eqIcon");
					window.AddGuiElement(rect1);
					window.AddGuiElement(rect);
					str.TextColor = GuiNewStyleBar.orangeColor;
					str.boundries = new Rect(rect.X + 18f, single, str.TextWidth + 5f, 18f);
					str.Alignment = 3;
					window.AddGuiElement(str);
					str1.TextColor = GuiNewStyleBar.purpleColor;
					str1.boundries = new Rect(rect1.X + 18f, single, str1.TextWidth + 5f, 18f);
					str1.Alignment = 3;
					window.AddGuiElement(str1);
					GuiLabel guiLabel4 = new GuiLabel()
					{
						boundries = new Rect(rect1.X - 30f, single, 30f, 18f),
						text = StaticData.Translate("key_inventory_or"),
						Alignment = 4
					};
					window.AddGuiElement(guiLabel4);
				}
				else if (priceNova == 0)
				{
					GuiTexture rect2 = new GuiTexture();
					GuiTexture textWidth2 = new GuiTexture();
					GuiLabel str2 = new GuiLabel();
					GuiLabel str3 = new GuiLabel();
					str2.FontSize = 18;
					str3.FontSize = 18;
					rect2.boundries = new Rect(0f, single + 1f, 18f, 18f);
					textWidth2.boundries = new Rect(0f, single + 1f, 18f, 18f);
					str2.text = priceCash.ToString("##,##0");
					str3.text = priceViral.ToString("##,##0");
					float _width2 = (window.boundries.get_width() - str2.TextWidth - str3.TextWidth - 54f) / 2f;
					rect2.X = _width2;
					textWidth2.X = _width2 + str2.TextWidth + 42f;
					rect2.SetTextureKeepSize("FrameworkGUI", "cashIcon");
					textWidth2.SetTextureKeepSize("FrameworkGUI", "eqIcon");
					window.AddGuiElement(textWidth2);
					window.AddGuiElement(rect2);
					str2.TextColor = GuiNewStyleBar.blueColor;
					str2.boundries = new Rect(rect2.X + 18f, single, str2.TextWidth + 5f, 18f);
					str2.Alignment = 3;
					window.AddGuiElement(str2);
					str3.TextColor = GuiNewStyleBar.purpleColor;
					str3.boundries = new Rect(textWidth2.X + 18f, single, str3.TextWidth + 5f, 18f);
					str3.Alignment = 3;
					window.AddGuiElement(str3);
					GuiLabel guiLabel5 = new GuiLabel()
					{
						boundries = new Rect(textWidth2.X - 30f, single, 30f, 18f),
						text = StaticData.Translate("key_inventory_or"),
						Alignment = 4
					};
					window.AddGuiElement(guiLabel5);
				}
				else
				{
					GuiTexture guiTexture3 = new GuiTexture();
					GuiTexture rect3 = new GuiTexture();
					GuiLabel str4 = new GuiLabel();
					GuiLabel rect4 = new GuiLabel();
					str4.FontSize = 18;
					rect4.FontSize = 18;
					guiTexture3.boundries = new Rect(0f, single + 1f, 18f, 18f);
					rect3.boundries = new Rect(0f, single + 1f, 18f, 18f);
					str4.text = priceCash.ToString("##,##0");
					rect4.text = priceNova.ToString("##,##0");
					float single2 = (window.boundries.get_width() - str4.TextWidth - rect4.TextWidth - 54f) / 2f;
					guiTexture3.X = single2;
					rect3.X = single2 + str4.TextWidth + 42f;
					guiTexture3.SetTextureKeepSize("FrameworkGUI", "cashIcon");
					rect3.SetTextureKeepSize("FrameworkGUI", "novaIcon");
					window.AddGuiElement(rect3);
					window.AddGuiElement(guiTexture3);
					str4.TextColor = GuiNewStyleBar.blueColor;
					str4.boundries = new Rect(guiTexture3.X + 18f, single, str4.TextWidth + 5f, 18f);
					str4.Alignment = 3;
					window.AddGuiElement(str4);
					rect4.TextColor = GuiNewStyleBar.orangeColor;
					rect4.boundries = new Rect(rect3.X + 18f, single, rect4.TextWidth + 5f, 18f);
					rect4.Alignment = 3;
					window.AddGuiElement(rect4);
					GuiLabel guiLabel6 = new GuiLabel()
					{
						boundries = new Rect(rect3.X - 30f, single, 30f, 18f),
						text = StaticData.Translate("key_inventory_or"),
						Alignment = 4
					};
					window.AddGuiElement(guiLabel6);
				}
				break;
			}
			case 3:
			{
				GuiTexture guiTexture4 = new GuiTexture();
				GuiTexture textWidth3 = new GuiTexture();
				GuiTexture textWidth4 = new GuiTexture();
				GuiLabel str5 = new GuiLabel();
				GuiLabel rect5 = new GuiLabel();
				GuiLabel str6 = new GuiLabel();
				str5.FontSize = 18;
				rect5.FontSize = 18;
				str6.FontSize = 18;
				guiTexture4.boundries = new Rect(0f, single + 1f, 18f, 18f);
				textWidth3.boundries = new Rect(0f, single + 1f, 18f, 18f);
				textWidth4.boundries = new Rect(0f, single + 1f, 18f, 18f);
				str5.text = priceCash.ToString("##,##0");
				rect5.text = priceNova.ToString("##,##0");
				str6.text = priceViral.ToString("##,##0");
				float _width3 = (window.boundries.get_width() - str5.TextWidth - str6.TextWidth - rect5.TextWidth - 96f) / 2f;
				guiTexture4.X = _width3;
				textWidth3.X = _width3 + str5.TextWidth + 42f;
				textWidth4.X = _width3 + str5.TextWidth + rect5.TextWidth + 84f;
				guiTexture4.SetTextureKeepSize("FrameworkGUI", "cashIcon");
				textWidth3.SetTextureKeepSize("FrameworkGUI", "novaIcon");
				textWidth4.SetTextureKeepSize("FrameworkGUI", "eqIcon");
				window.AddGuiElement(textWidth4);
				window.AddGuiElement(textWidth3);
				window.AddGuiElement(guiTexture4);
				str5.TextColor = GuiNewStyleBar.blueColor;
				str5.boundries = new Rect(guiTexture4.X + 18f, single, str5.TextWidth + 5f, 18f);
				str5.Alignment = 3;
				window.AddGuiElement(str5);
				rect5.TextColor = GuiNewStyleBar.orangeColor;
				rect5.boundries = new Rect(textWidth3.X + 18f, single, rect5.TextWidth + 5f, 18f);
				rect5.Alignment = 3;
				window.AddGuiElement(rect5);
				str6.TextColor = GuiNewStyleBar.purpleColor;
				str6.boundries = new Rect(textWidth4.X + 18f, single, str6.TextWidth + 5f, 18f);
				str6.Alignment = 3;
				window.AddGuiElement(str6);
				GuiLabel guiLabel7 = new GuiLabel()
				{
					boundries = new Rect(textWidth3.X - 30f, single, 30f, 18f),
					text = StaticData.Translate("key_inventory_or"),
					Alignment = 4
				};
				window.AddGuiElement(guiLabel7);
				GuiLabel guiLabel8 = new GuiLabel()
				{
					boundries = new Rect(textWidth4.X - 30f, single, 30f, 18f),
					text = StaticData.Translate("key_inventory_or"),
					Alignment = 4
				};
				window.AddGuiElement(guiLabel8);
				break;
			}
		}
	}

	public void CreateNewSlotItemTooltip(SlotItem slotItem, GuiWindow wnd)
	{
		Color color;
		string str;
		AndromedaGuiDragDropPlace.<CreateNewSlotItemTooltip>c__AnonStorey29 variable = null;
		wnd.SetBackgroundTexture("ConfigWindow", "tooltipBackground");
		wnd.boundries.set_width(300f);
		wnd.boundries.set_height(220f);
		PlayerItemTypesData item = StaticData.allTypes.get_Item(slotItem.get_ItemType());
		if (this.location == 16 || this.location == 17)
		{
			str = string.Format(StaticData.Translate("key_gambler_uknown_bonus"), StaticData.Translate(StaticData.allTypes.get_Item(slotItem.get_ItemType()).uiName));
			color = GuiNewStyleBar.orangeColor;
		}
		else
		{
			Inventory.ItemRarity(slotItem, out str, out color);
		}
		GuiTextureAnimated guiTextureAnimated = new GuiTextureAnimated();
		guiTextureAnimated.Init("TooltipAnimation", "TooltipAnimation", "TooltipAnimation/frame");
		guiTextureAnimated.rotationTime = 2f;
		guiTextureAnimated.boundries.set_x(0f);
		guiTextureAnimated.boundries.set_y(0f);
		wnd.AddGuiElement(guiTextureAnimated);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(3f, 3f, 294f, 22f),
			FontSize = 11,
			text = str,
			TextColor = color,
			Alignment = 4,
			Font = GuiLabel.FontBold
		};
		wnd.AddGuiElement(guiLabel);
		if (slotItem.isAccountBound)
		{
			GuiLabel guiLabel1 = new GuiLabel()
			{
				boundries = new Rect(15f, 100f, 270f, 12f),
				FontSize = 10,
				text = StaticData.Translate("key_tooltip_account_bound"),
				TextColor = GuiNewStyleBar.blueColor,
				Alignment = 3,
				Font = GuiLabel.FontBold
			};
			wnd.AddGuiElement(guiLabel1);
		}
		GuiTexture guiTexture = new GuiTexture()
		{
			boundries = new Rect(20f, 40f, 75f, 51f)
		};
		guiTexture.SetItemTextureKeepSize(slotItem.get_ItemType());
		wnd.AddGuiElement(guiTexture);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(15f, 112f, 270f, 12f),
			Alignment = 3,
			Font = GuiLabel.FontBold,
			FontSize = 10,
			text = string.Format(StaticData.Translate("key_tooltip_level_restriction"), StaticData.allTypes.get_Item(slotItem.get_ItemType()).levelRestriction),
			TextColor = (StaticData.allTypes.get_Item(slotItem.get_ItemType()).levelRestriction <= NetworkScript.player.playerBelongings.playerLevel ? Color.get_white() : GuiNewStyleBar.redColor)
		};
		wnd.AddGuiElement(guiLabel2);
		if (PlayerItems.IsWeapon(slotItem.get_ItemType()))
		{
			float single = 0f;
			WeaponSlot weaponSlot = null;
			if (slotItem.get_ShipId() != 0 && slotItem.get_ShipId() == NetworkScript.player.playerBelongings.selectedShipId)
			{
				weaponSlot = Enumerable.FirstOrDefault<WeaponSlot>(Enumerable.Where<WeaponSlot>(NetworkScript.player.cfg.weaponSlots, new Func<WeaponSlot, bool>(variable, (WeaponSlot t) => t.slotId == this.slotItem.get_Slot())));
				if (weaponSlot != null)
				{
					single = (!NetworkScript.player.cfg.damageBooster ? (float)weaponSlot.skillDamage * (1f + NetworkScript.player.cfg.dmgPercentForAlien / 100f) * ((float)((AmmoNet)StaticData.allTypes.get_Item(weaponSlot.selectedAmmoItemType)).damage / 100f) : (float)weaponSlot.skillDamage * (1f + NetworkScript.player.cfg.dmgPercentForAlien / 100f) * ((float)((AmmoNet)StaticData.allTypes.get_Item(weaponSlot.selectedAmmoItemType)).damage / 100f) * 1.3f);
				}
				else
				{
					Debug.Log("weaponSlot == null");
				}
			}
			int num = 10;
			int num1 = 115;
			int num2 = 170;
			GuiLabel guiLabel3 = new GuiLabel()
			{
				boundries = new Rect((float)num1, 32f, (float)num2, 12f),
				text = StaticData.Translate("key_inventory_damage"),
				FontSize = num,
				Alignment = 3
			};
			wnd.AddGuiElement(guiLabel3);
			GuiLabel guiLabel4 = new GuiLabel()
			{
				boundries = new Rect((float)num1, 46f, (float)num2, 12f),
				text = StaticData.Translate("key_inventory_cooldown"),
				FontSize = num,
				Alignment = 3
			};
			wnd.AddGuiElement(guiLabel4);
			GuiLabel guiLabel5 = new GuiLabel()
			{
				boundries = new Rect((float)num1, 60f, (float)num2, 12f),
				text = StaticData.Translate("key_inventory_range"),
				FontSize = num,
				Alignment = 3
			};
			wnd.AddGuiElement(guiLabel5);
			GuiLabel guiLabel6 = new GuiLabel()
			{
				boundries = new Rect((float)num1, 74f, (float)num2, 12f),
				text = StaticData.Translate("key_inventory_penetration"),
				FontSize = num,
				Alignment = 3
			};
			wnd.AddGuiElement(guiLabel6);
			GuiLabel guiLabel7 = new GuiLabel()
			{
				boundries = new Rect((float)num1, 88f, (float)num2, 12f),
				text = StaticData.Translate("key_inventory_targeting"),
				FontSize = num,
				Alignment = 3
			};
			wnd.AddGuiElement(guiLabel7);
			GuiLabel fontBold = new GuiLabel()
			{
				boundries = new Rect((float)num1, 32f, (float)num2, 12f)
			};
			int damageTotal = ((SlotItemWeapon)slotItem).get_DamageTotal();
			fontBold.text = damageTotal.ToString("##,##0");
			fontBold.FontSize = num;
			fontBold.Font = GuiLabel.FontBold;
			fontBold.TextColor = (slotItem.get_BonusOne() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			fontBold.Alignment = 5;
			wnd.AddGuiElement(fontBold);
			if (weaponSlot != null)
			{
				fontBold.TextColor = (slotItem.get_BonusOne() != 0 || single != (float)((SlotItemWeapon)slotItem).get_DamageTotal() ? GuiNewStyleBar.greenColor : Color.get_white());
				fontBold.text = ((int)single).ToString("##,##0");
			}
			GuiLabel str1 = new GuiLabel()
			{
				boundries = new Rect((float)num1, 46f, (float)num2, 12f)
			};
			int cooldownTotal = ((SlotItemWeapon)slotItem).get_CooldownTotal();
			str1.text = cooldownTotal.ToString("##,##0");
			str1.FontSize = num;
			str1.Font = GuiLabel.FontBold;
			str1.TextColor = (slotItem.get_BonusThree() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			str1.Alignment = 5;
			wnd.AddGuiElement(str1);
			if (weaponSlot != null)
			{
				float single1 = 0f;
				if (slotItem.get_ItemType() == PlayerItems.TypeWeaponLaserTire1 || slotItem.get_ItemType() == PlayerItems.TypeWeaponLaserTire2 || slotItem.get_ItemType() == PlayerItems.TypeWeaponLaserTire3 || slotItem.get_ItemType() == PlayerItems.TypeWeaponLaserTire4 || slotItem.get_ItemType() == PlayerItems.TypeWeaponLaserTire5)
				{
					single1 = NetworkScript.player.cfg.laserCooldown;
				}
				else if (slotItem.get_ItemType() == PlayerItems.TypeWeaponPlasmaTire1 || slotItem.get_ItemType() == PlayerItems.TypeWeaponPlasmaTire2 || slotItem.get_ItemType() == PlayerItems.TypeWeaponPlasmaTire3 || slotItem.get_ItemType() == PlayerItems.TypeWeaponPlasmaTire4 || slotItem.get_ItemType() == PlayerItems.TypeWeaponPlasmaTire5)
				{
					single1 = NetworkScript.player.cfg.plasmaCooldown;
				}
				else if (slotItem.get_ItemType() == PlayerItems.TypeWeaponIonTire1 || slotItem.get_ItemType() == PlayerItems.TypeWeaponIonTire2 || slotItem.get_ItemType() == PlayerItems.TypeWeaponIonTire3 || slotItem.get_ItemType() == PlayerItems.TypeWeaponIonTire4 || slotItem.get_ItemType() == PlayerItems.TypeWeaponIonTire5)
				{
					single1 = NetworkScript.player.cfg.ionCooldown;
				}
				str1.TextColor = (slotItem.get_BonusThree() != 0 || single1 != 0f ? GuiNewStyleBar.greenColor : Color.get_white());
				float single2 = Math.Max(500f, (float)((SlotItemWeapon)slotItem).get_CooldownTotal() - single1);
				str1.text = single2.ToString("##,##0");
			}
			GuiLabel fontBold1 = new GuiLabel()
			{
				boundries = new Rect((float)num1, 60f, (float)num2, 12f)
			};
			int rangeTotal = ((SlotItemWeapon)slotItem).get_RangeTotal();
			fontBold1.text = rangeTotal.ToString("##,##0");
			fontBold1.FontSize = num;
			fontBold1.Font = GuiLabel.FontBold;
			fontBold1.TextColor = (slotItem.get_BonusTwo() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			fontBold1.Alignment = 5;
			wnd.AddGuiElement(fontBold1);
			GuiLabel str2 = new GuiLabel()
			{
				boundries = new Rect((float)num1, 74f, (float)num2, 12f)
			};
			int penetrationTotal = ((SlotItemWeapon)slotItem).get_PenetrationTotal();
			str2.text = penetrationTotal.ToString("##,##0");
			str2.FontSize = num;
			str2.Font = GuiLabel.FontBold;
			str2.TextColor = (slotItem.get_BonusFour() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			str2.Alignment = 5;
			wnd.AddGuiElement(str2);
			GuiLabel fontBold2 = new GuiLabel()
			{
				boundries = new Rect((float)num1, 88f, (float)num2, 12f)
			};
			int targetingTotal = ((SlotItemWeapon)slotItem).get_TargetingTotal();
			fontBold2.text = targetingTotal.ToString("##,##0");
			fontBold2.FontSize = num;
			fontBold2.Font = GuiLabel.FontBold;
			fontBold2.TextColor = (slotItem.get_BonusFive() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			fontBold2.Alignment = 5;
			wnd.AddGuiElement(fontBold2);
			if (weaponSlot != null)
			{
				int num3 = 0;
				if (slotItem.get_ItemType() == PlayerItems.TypeWeaponLaserTire1 || slotItem.get_ItemType() == PlayerItems.TypeWeaponLaserTire2 || slotItem.get_ItemType() == PlayerItems.TypeWeaponLaserTire3 || slotItem.get_ItemType() == PlayerItems.TypeWeaponLaserTire4 || slotItem.get_ItemType() == PlayerItems.TypeWeaponLaserTire5)
				{
					num3 = NetworkScript.player.cfg.targetingForLaser;
				}
				else if (slotItem.get_ItemType() == PlayerItems.TypeWeaponPlasmaTire1 || slotItem.get_ItemType() == PlayerItems.TypeWeaponPlasmaTire2 || slotItem.get_ItemType() == PlayerItems.TypeWeaponPlasmaTire3 || slotItem.get_ItemType() == PlayerItems.TypeWeaponPlasmaTire4 || slotItem.get_ItemType() == PlayerItems.TypeWeaponPlasmaTire5)
				{
					num3 = NetworkScript.player.cfg.targetingForPlasma;
				}
				else if (slotItem.get_ItemType() == PlayerItems.TypeWeaponIonTire1 || slotItem.get_ItemType() == PlayerItems.TypeWeaponIonTire2 || slotItem.get_ItemType() == PlayerItems.TypeWeaponIonTire3 || slotItem.get_ItemType() == PlayerItems.TypeWeaponIonTire4 || slotItem.get_ItemType() == PlayerItems.TypeWeaponIonTire5)
				{
					num3 = NetworkScript.player.cfg.targetingForIon;
				}
				fontBold2.TextColor = (slotItem.get_BonusFive() != 0 || num3 != ((SlotItemWeapon)slotItem).get_TargetingTotal() ? GuiNewStyleBar.greenColor : Color.get_white());
				fontBold2.text = num3.ToString("##,##0");
			}
		}
		else if (PlayerItems.IsCorpus(slotItem.get_ItemType()))
		{
			int num4 = 10;
			int num5 = 115;
			int num6 = 170;
			GuiLabel guiLabel8 = new GuiLabel()
			{
				boundries = new Rect((float)num5, 60f, (float)num6, 12f),
				text = StaticData.Translate("key_inventory_corpus"),
				FontSize = num4,
				Alignment = 3
			};
			wnd.AddGuiElement(guiLabel8);
			GuiLabel str3 = new GuiLabel()
			{
				boundries = new Rect((float)num5, 60f, (float)num6, 12f)
			};
			int bonusOne = ((GeneratorNet)item).bonusValue + slotItem.get_BonusOne();
			str3.text = bonusOne.ToString("##,##0");
			str3.FontSize = num4;
			str3.Font = GuiLabel.FontBold;
			str3.TextColor = (slotItem.get_BonusOne() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			str3.Alignment = 5;
			wnd.AddGuiElement(str3);
		}
		else if (PlayerItems.IsShield(slotItem.get_ItemType()))
		{
			int num7 = 10;
			int num8 = 115;
			int num9 = 170;
			GuiLabel guiLabel9 = new GuiLabel()
			{
				boundries = new Rect((float)num8, 60f, (float)num9, 12f),
				text = StaticData.Translate("key_inventory_shield"),
				FontSize = num7,
				Alignment = 3
			};
			wnd.AddGuiElement(guiLabel9);
			GuiLabel fontBold3 = new GuiLabel()
			{
				boundries = new Rect((float)num8, 60f, (float)num9, 12f)
			};
			int bonusOne1 = ((GeneratorNet)item).bonusValue + slotItem.get_BonusOne();
			fontBold3.text = bonusOne1.ToString("##,##0");
			fontBold3.FontSize = num7;
			fontBold3.Font = GuiLabel.FontBold;
			fontBold3.TextColor = (slotItem.get_BonusOne() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			fontBold3.Alignment = 5;
			wnd.AddGuiElement(fontBold3);
		}
		else if (PlayerItems.IsEngine(slotItem.get_ItemType()))
		{
			int num10 = 10;
			int num11 = 115;
			int num12 = 170;
			GuiLabel guiLabel10 = new GuiLabel()
			{
				boundries = new Rect((float)num11, 60f, (float)num12, 12f),
				text = StaticData.Translate("key_inventory_speed"),
				FontSize = num10,
				Alignment = 3
			};
			wnd.AddGuiElement(guiLabel10);
			GuiLabel str4 = new GuiLabel()
			{
				boundries = new Rect((float)num11, 60f, (float)num12, 12f)
			};
			int bonusOne2 = ((GeneratorNet)item).bonusValue + slotItem.get_BonusOne();
			str4.text = bonusOne2.ToString("##,##0");
			str4.FontSize = num10;
			str4.Font = GuiLabel.FontBold;
			str4.TextColor = (slotItem.get_BonusOne() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			str4.Alignment = 5;
			wnd.AddGuiElement(str4);
		}
		else if (PlayerItems.IsExtra(slotItem.get_ItemType()))
		{
			int num13 = 10;
			int num14 = 115;
			int num15 = 170;
			GuiLabel guiLabel11 = new GuiLabel()
			{
				boundries = new Rect((float)num14, 54f, (float)num15, 24f),
				FontSize = num13,
				Alignment = 3
			};
			wnd.AddGuiElement(guiLabel11);
			GuiLabel guiLabel12 = new GuiLabel()
			{
				boundries = new Rect((float)num14, 54f, (float)num15, 24f),
				text = string.Empty,
				Font = GuiLabel.FontBold,
				Alignment = 5
			};
			wnd.AddGuiElement(guiLabel12);
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
			else if (PlayerItems.IsForLaserDamage(slotItem.get_ItemType()))
			{
				guiLabel11.text = StaticData.Translate("key_extra_dmg_laser");
				guiLabel12.text = string.Format("+{0:##,##0}%", ((ExtrasNet)item).efValue);
			}
			else if (PlayerItems.IsForPlasmaDamage(slotItem.get_ItemType()))
			{
				guiLabel11.text = StaticData.Translate("key_extra_dmg_plasma");
				guiLabel12.text = string.Format("+{0:##,##0}%", ((ExtrasNet)item).efValue);
			}
			else if (PlayerItems.IsForIonDamage(slotItem.get_ItemType()))
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
			guiLabel11.boundries.set_width((float)num15 - guiLabel12.TextWidth - 5f);
		}
		else if (PlayerItems.IsPortalPart(slotItem.get_ItemType()) || PlayerItems.IsQuestItem(slotItem.get_ItemType()))
		{
			GuiLabel guiLabel13 = new GuiLabel()
			{
				boundries = new Rect(115f, 24f, 170f, 80f),
				TextColor = GuiNewStyleBar.blueColor,
				text = StaticData.Translate(StaticData.allTypes.get_Item(slotItem.get_ItemType()).uiName),
				FontSize = 10,
				Alignment = 4
			};
			wnd.AddGuiElement(guiLabel13);
		}
		GuiLabel guiLabel14 = new GuiLabel()
		{
			boundries = new Rect(15f, 125f, 270f, 70f),
			TextColor = GuiNewStyleBar.greenColor,
			Font = GuiLabel.FontBold,
			FontSize = 10,
			Alignment = 3
		};
		wnd.AddGuiElement(guiLabel14);
		string empty = string.Empty;
		if (this.location == 16)
		{
			empty = StaticData.Translate("key_gambler_bonuses1");
		}
		else if (this.location == 17)
		{
			empty = StaticData.Translate("key_gambler_bonuses2");
		}
		else if (PlayerItems.IsWeapon(slotItem.get_ItemType()))
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
		guiLabel14.text = empty;
		if (PlayerItems.IsQuestItem(slotItem.get_ItemType()))
		{
			return;
		}
		int num16 = item.priceNova;
		if (this.location == 15)
		{
			this.AddPriceInTooltip(wnd, item.priceCash, item.priceNova, item.priceViral, 200, false);
		}
		else if (this.location == 16)
		{
			num16 = (int)((float)num16 * 1.5f);
			this.AddPriceInTooltip(wnd, 0, num16, num16, 200, false);
		}
		else if (this.location != 17)
		{
			int amount = PlayerItems.CalculateSlotItemSellPrice(slotItem, NetworkScript.player.cfg.sellBonus);
			if (PlayerItems.IsUniversalPortalPart(slotItem.get_ItemType()))
			{
				amount = slotItem.get_Amount() * amount;
			}
			this.AddPriceInTooltip(wnd, amount, 0, 0, 200, true);
		}
		else
		{
			num16 = (int)((float)num16 * 2f);
			this.AddPriceInTooltip(wnd, 0, num16, num16, 200, false);
		}
	}

	public GuiWindow ItemInfoTooltip2(object parm)
	{
		if (this.item == null)
		{
			return null;
		}
		int amount = 0;
		int num = 0;
		int amount1 = 0;
		GuiWindow guiWindow = new GuiWindow();
		guiWindow.SetBackgroundTexture("ConfigWindow", "InventoryTooltipBackground");
		float single = this.position.x + 50f;
		float single1 = this.position.y + 2f;
		guiWindow.boundries = new Rect(single, single1, guiWindow.boundries.get_width(), guiWindow.boundries.get_height());
		guiWindow.zOrder = 230;
		guiWindow.ignoreClickEvents = true;
		guiWindow.isHidden = false;
		GuiLabel guiLabel = new GuiLabel()
		{
			WordWrap = true,
			TextColor = Color.get_white(),
			FontSize = 11,
			boundries = new Rect(5f, 10f, 230f, 40f),
			Alignment = 1
		};
		guiWindow.AddGuiElement(guiLabel);
		GuiLabel rect = new GuiLabel()
		{
			TextColor = GuiNewStyleBar.orangeColor,
			boundries = new Rect(5f, 65f, 230f, 25f),
			Alignment = 3
		};
		guiWindow.AddGuiElement(rect);
		GuiLabel empty = new GuiLabel()
		{
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 10,
			Font = GuiLabel.FontBold,
			boundries = new Rect(78f, 150f, 157f, 12f),
			Alignment = 3
		};
		guiWindow.AddGuiElement(empty);
		if (!PlayerItems.item2categoryMapping.ContainsKey(this.item.get_ItemType()) && (this.item.get_ItemType() < 11000 || this.item.get_ItemType() > 11999) && !PlayerItems.IsQuestItem(this.item.get_ItemType()))
		{
			return null;
		}
		if (PlayerItems.IsWeapon(this.item.get_ItemType()) || PlayerItems.IsShield(this.item.get_ItemType()) || PlayerItems.IsCorpus(this.item.get_ItemType()) || PlayerItems.IsEngine(this.item.get_ItemType()) || PlayerItems.IsExtra(this.item.get_ItemType()) || PlayerItems.IsPortalPart(this.item.get_ItemType()) || PlayerItems.IsQuestItem(this.item.get_ItemType()))
		{
			this.CreateNewSlotItemTooltip(this.item, guiWindow);
			return guiWindow;
		}
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("ConfigWindow", "AvatarBox");
		guiTexture.X = 5f;
		guiTexture.Y = 90f;
		guiWindow.AddGuiElement(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture()
		{
			boundries = new Rect(5f, 105f, 64f, 44f)
		};
		guiWindow.AddGuiElement(guiTexture1);
		if (this.item.get_ItemType() >= 11000 && this.item.get_ItemType() <= 11999)
		{
			PlayerItemTypesData item = StaticData.allTypes.get_Item(this.item.get_ItemType());
			guiWindow.SetBackgroundTexture("ConfigWindow", "mineralToolTip");
			guiTexture.X = 3f;
			guiTexture.Y = 3f;
			guiTexture1.SetTextureKeepSize("MineralsAvatars", item.assetName);
			guiTexture1.X = 3f;
			guiTexture1.Y = 23f;
			guiLabel.text = StaticData.Translate("key_inventory_sell_price");
			guiLabel.boundries = new Rect(77f, 32f, 158f, 20f);
			guiLabel.TextColor = GuiNewStyleBar.blueColor;
			guiLabel.Alignment = 4;
			rect.text = StaticData.Translate(item.uiName);
			rect.boundries = new Rect(77f, 8f, 158f, 20f);
			rect.TextColor = GuiNewStyleBar.orangeColor;
			rect.FontSize = 16;
			rect.Alignment = 4;
			empty.text = string.Empty;
			amount = PlayerItems.CalculateSlotItemSellPrice(this.item, NetworkScript.player.cfg.sellBonus);
			num = item.priceNova;
			amount1 = item.priceViral;
			GuiLabel guiLabel1 = new GuiLabel()
			{
				boundries = new Rect(77f, 56f, 158f, 20f),
				text = string.Concat("$ ", amount.ToString("##,##0")),
				TextColor = GuiNewStyleBar.blueColor,
				Alignment = 4
			};
			guiWindow.AddGuiElement(guiLabel1);
			return guiWindow;
		}
		GeneratorNet generatorNet = null;
		switch (PlayerItems.item2categoryMapping.get_Item(this.item.get_ItemType()))
		{
			case 0:
			{
				break;
			}
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			{
				PlayerItemTypesData playerItemTypesDatum = StaticData.allTypes.get_Item(this.item.get_ItemType());
				guiLabel.text = StaticData.Translate(playerItemTypesDatum.description);
				rect.text = StaticData.Translate(playerItemTypesDatum.uiName);
				empty.text = string.Format(StaticData.Translate("key_tooltip_level_restriction"), playerItemTypesDatum.levelRestriction);
				empty.TextColor = (playerItemTypesDatum.levelRestriction <= NetworkScript.player.cfg.playerLevel ? Color.get_white() : Color.get_red());
				amount = playerItemTypesDatum.priceCash;
				num = playerItemTypesDatum.priceNova;
				amount1 = playerItemTypesDatum.priceViral;
				guiTexture1.SetItemTextureKeepSize(playerItemTypesDatum.itemType);
				break;
			}
			default:
			{
				goto case 0;
			}
		}
		if (this.location == 15)
		{
			if (PlayerItems.item2categoryMapping.get_Item(this.item.get_ItemType()) == 1)
			{
				amount = amount * (this.item.get_Amount() / 100);
				num = num * (this.item.get_Amount() / 100);
				amount1 = amount1 * (this.item.get_Amount() / 100);
			}
			this.AddPriceInTooltip(guiWindow, amount, num, amount1, 50, false);
		}
		else
		{
			int num1 = PlayerItems.CalculateSlotItemSellPrice(this.item, NetworkScript.player.cfg.sellBonus);
			num1 = (PlayerItems.item2categoryMapping.get_Item(this.item.get_ItemType()) != 1 ? num1 * this.item.get_Amount() : num1 * (this.item.get_Amount() / 100));
			this.AddPriceInTooltip(guiWindow, num1, 0, 0, 50, true);
		}
		int x = (int)guiTexture1.X + 72;
		int y = (int)guiTexture1.Y - 14;
		__VaultWindow.CalculateMaxStats();
		switch (PlayerItems.item2categoryMapping.get_Item(this.item.get_ItemType()))
		{
			case 0:
			{
				break;
			}
			case 1:
			{
				int amount2 = this.item.get_Amount();
				rect.text = string.Concat(amount2.ToString("##,##0"), " ", rect.text);
				AmmoNet ammoNet = (AmmoNet)StaticData.allTypes.get_Item(this.item.get_ItemType());
				this.PutAmmoStat(x, y, StaticData.Translate("key_inventory_damage"), ammoNet.damage, __VaultWindow._maxAmmoDamage, guiWindow);
				break;
			}
			case 2:
			{
				SlotItemWeapon slotItemWeapon = (SlotItemWeapon)this.item;
				this.PutStatLine(x, y, StaticData.Translate("key_inventory_damage"), slotItemWeapon.get_DamageTotal(), __VaultWindow._maxWeaponDamage, guiWindow);
				this.PutStatLine(x, y + 15, StaticData.Translate("key_inventory_cooldown"), slotItemWeapon.get_CooldownTotal(), __VaultWindow._maxCooldown, guiWindow);
				this.PutStatLine(x, y + 30, StaticData.Translate("key_inventory_range"), slotItemWeapon.get_RangeTotal(), __VaultWindow._maxRange, guiWindow);
				this.PutStatLine(x, y + 45, StaticData.Translate("key_inventory_penetration"), slotItemWeapon.get_PenetrationTotal(), __VaultWindow._maxPenetration, guiWindow);
				this.PutStatLine(x, y + 60, StaticData.Translate("key_inventory_targeting"), slotItemWeapon.get_TargetingTotal(), __VaultWindow._maxWeaponTargeting, guiWindow);
				break;
			}
			case 3:
			{
				generatorNet = (GeneratorNet)StaticData.allTypes.get_Item(this.item.get_ItemType());
				this.PutStatLine(x, y, StaticData.Translate("key_inventory_speed"), generatorNet.bonusValue, __VaultWindow._maxEngineBonus, guiWindow);
				break;
			}
			case 4:
			{
				generatorNet = (GeneratorNet)StaticData.allTypes.get_Item(this.item.get_ItemType());
				this.PutStatLine(x, y, StaticData.Translate("key_inventory_shield"), generatorNet.bonusValue, __VaultWindow._maxShieldBonus, guiWindow);
				break;
			}
			case 5:
			{
				break;
			}
			case 6:
			{
				generatorNet = (GeneratorNet)StaticData.allTypes.get_Item(this.item.get_ItemType());
				this.PutStatLine(x, y, StaticData.Translate("key_inventory_corpus"), generatorNet.bonusValue, __VaultWindow._maxCorpusBonus, guiWindow);
				break;
			}
			default:
			{
				goto case 0;
			}
		}
		return guiWindow;
	}

	private void PutAmmoStat(int baseX, int baseY, string labelText, int val, int maxVal, GuiWindow wnd)
	{
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect((float)baseX, (float)baseY, 70f, 20f),
			FontSize = 10,
			Alignment = 0,
			text = labelText,
			WordWrap = false
		};
		wnd.AddGuiElement(guiLabel);
		GuiNewStyleBar guiNewStyleBar = new GuiNewStyleBar()
		{
			X = (float)(baseX + 71),
			Y = (float)baseY
		};
		guiNewStyleBar.SetCustumSize(54, Color.get_white());
		guiNewStyleBar.maximum = (float)maxVal;
		guiNewStyleBar.current = (float)val;
		wnd.AddGuiElement(guiNewStyleBar);
		guiLabel = new GuiLabel()
		{
			boundries = new Rect((float)(baseX + 126), (float)baseY, 60f, 20f),
			FontSize = 10,
			Alignment = 0,
			text = string.Concat(val.ToString("###,##0"), "%")
		};
		wnd.AddGuiElement(guiLabel);
	}

	private void PutStatLine(int baseX, int baseY, string labelText, int val, int maxVal, GuiWindow wnd)
	{
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect((float)baseX, (float)baseY, 70f, 20f),
			FontSize = 10,
			Alignment = 0,
			text = labelText,
			WordWrap = false
		};
		wnd.AddGuiElement(guiLabel);
		GuiNewStyleBar guiNewStyleBar = new GuiNewStyleBar()
		{
			X = (float)(baseX + 71),
			Y = (float)baseY
		};
		guiNewStyleBar.SetCustumSize(54, Color.get_white());
		guiNewStyleBar.maximum = (float)maxVal;
		guiNewStyleBar.current = (float)val;
		wnd.AddGuiElement(guiNewStyleBar);
		guiLabel = new GuiLabel()
		{
			boundries = new Rect((float)(baseX + 126), (float)baseY, 60f, 20f),
			FontSize = 10,
			Alignment = 0,
			text = val.ToString("###,##0")
		};
		wnd.AddGuiElement(guiLabel);
	}
}