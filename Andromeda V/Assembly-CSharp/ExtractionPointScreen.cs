using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TransferableObjects;
using UnityEngine;

public class ExtractionPointScreen : GuiWindow
{
	private GuiButton tabBtnOverview;

	private GuiButton tabBtnFortify;

	private GuiTexture mainTabTexture;

	private GuiTexture leftCornerFrame;

	private GuiLabel windowTitle;

	private GuiLabel topContributorsLbl;

	private ExtractionPoint pointInRange;

	private ExtractionPointInfo pointInfo;

	private List<GuiElement> forDelete = new List<GuiElement>();

	private List<GuiElement> forDelete2 = new List<GuiElement>();

	private List<GuiButtonResizeable> buttonsOnScreen = new List<GuiButtonResizeable>();

	private bool isWaitingRefresh;

	private bool haveGuild;

	public static byte selectedTab;

	private static byte selectedFortifyTab;

	static ExtractionPointScreen()
	{
		ExtractionPointScreen.selectedTab = 1;
		ExtractionPointScreen.selectedFortifyTab = 1;
	}

	public ExtractionPointScreen()
	{
	}

	private bool CanBuildUnit(byte unitType, int population)
	{
		if (unitType != 51 && unitType != 52 && unitType != 53 && unitType != 54 && unitType != 55)
		{
			return this.pointInRange.currentPopulationTowers + population <= this.pointInRange.populationTowers;
		}
		return this.pointInRange.currentPopulationAliens + population <= this.pointInRange.populationAliens;
	}

	private bool CanPayFor(SelectedCurrency currency, int price)
	{
		if (!this.haveGuild)
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

	private new void Clear()
	{
		foreach (GuiElement guiElement in this.forDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.forDelete.Clear();
		foreach (GuiElement guiElement1 in this.forDelete2)
		{
			base.RemoveGuiElement(guiElement1);
		}
		this.forDelete2.Clear();
	}

	private void ClearRightMenu()
	{
		foreach (GuiElement guiElement in this.forDelete2)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.forDelete2.Clear();
	}

	public override void Create()
	{
		playWebGame.udp.ExecuteCommand(179, new Guild(), 42);
		this.pointInRange = NetworkScript.player.shipScript.extractionPointInRange;
		if (this.pointInRange == null)
		{
			this.pointInfo = null;
			return;
		}
		this.pointInfo = Enumerable.First<ExtractionPointInfo>(Enumerable.Where<ExtractionPointInfo>(StaticData.allExtractionPoints, new Func<ExtractionPointInfo, bool>(this, (ExtractionPointInfo t) => t.id == this.pointInRange.pointId)));
		base.SetBackgroundTexture("NewGUI", "novashop_frame");
		base.PutToCenter();
		this.zOrder = 210;
		this.isHidden = false;
		this.leftCornerFrame = new GuiTexture();
		this.leftCornerFrame.SetTexture("NewGUI", "pvp_ranking_left");
		this.leftCornerFrame.X = 24f;
		this.leftCornerFrame.Y = 26f;
		base.AddGuiElement(this.leftCornerFrame);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = StaticData.Translate(this.pointInRange.name),
			boundries = new Rect(30f, 44f, 174f, 24f),
			Alignment = 3,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		base.AddGuiElement(guiLabel);
		string str = string.Format("fraction{0}Icon", this.pointInRange.ownerFraction);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("FrameworkGUI", str);
		guiTexture.X = 30f;
		guiTexture.Y = 47f;
		base.AddGuiElement(guiTexture);
		float textWidth = (174f - (guiLabel.TextWidth + guiTexture.boundries.get_width())) * 0.5f;
		guiTexture.X = 30f + textWidth;
		guiLabel.X = guiTexture.X + guiTexture.boundries.get_width();
		this.windowTitle = new GuiLabel()
		{
			text = StaticData.Translate("ep_screen_window_title"),
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 18,
			Font = GuiLabel.FontBold,
			boundries = new Rect(280f, 42f, 540f, 35f),
			Alignment = 1
		};
		base.AddGuiElement(this.windowTitle);
		this.mainTabTexture = new GuiTexture()
		{
			boundries = new Rect(0f, 0f, 904f, 539f)
		};
		this.mainTabTexture.SetTexture("NewGUI", "PVPTab1");
		base.AddGuiElement(this.mainTabTexture);
		this.tabBtnOverview = new GuiButton()
		{
			boundries = new Rect(220f, 75f, 130f, 25f),
			Caption = StaticData.Translate("ep_screen_tab_overview"),
			FontSize = 15,
			textColorNormal = GuiNewStyleBar.blueColor,
			textColorDisabled = GuiNewStyleBar.blueColorDisable,
			textColorClick = GuiNewStyleBar.orangeColor,
			textColorHover = Color.get_white(),
			Alignment = 4,
			Clicked = new Action<EventHandlerParam>(this, ExtractionPointScreen.OnOverviewClicked),
			behaviourKeepClicked = true,
			IsClicked = ExtractionPointScreen.selectedTab == 1
		};
		base.AddGuiElement(this.tabBtnOverview);
		this.tabBtnFortify = new GuiButton()
		{
			boundries = new Rect(363f, 75f, 130f, 25f),
			Caption = StaticData.Translate("ep_screen_tab_fortify"),
			FontSize = 15,
			textColorNormal = GuiNewStyleBar.blueColor,
			textColorDisabled = GuiNewStyleBar.blueColorDisable,
			textColorClick = GuiNewStyleBar.orangeColor,
			textColorHover = Color.get_white(),
			Alignment = 4,
			Clicked = new Action<EventHandlerParam>(this, ExtractionPointScreen.OnFortifyClicked),
			behaviourKeepClicked = true,
			IsClicked = ExtractionPointScreen.selectedTab == 2
		};
		base.AddGuiElement(this.tabBtnFortify);
	}

	private void DrawMeInTopTen(Contributor c)
	{
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(275f, 490f, 190f, 30f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.orangeColor,
			Font = GuiLabel.FontBold,
			FontSize = 14,
			text = c.displayName
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(475f, 490f, 70f, 30f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 14,
			text = c.battleContribution.ToString("##,##0")
		};
		base.AddGuiElement(guiLabel1);
		this.forDelete.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(555f, 490f, 70f, 30f),
			Alignment = 4,
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 14,
			text = c.novaContribution.ToString("##,##0")
		};
		base.AddGuiElement(guiLabel2);
		this.forDelete.Add(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(635f, 490f, 70f, 30f),
			Alignment = 4,
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 14,
			text = c.viralContribution.ToString("##,##0")
		};
		base.AddGuiElement(guiLabel3);
		this.forDelete.Add(guiLabel3);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			boundries = new Rect(715f, 490f, 70f, 30f),
			Alignment = 4,
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 14,
			text = c.tottalContribution.ToString("##,##0")
		};
		base.AddGuiElement(guiLabel4);
		this.forDelete.Add(guiLabel4);
		GuiLabel guiLabel5 = new GuiLabel()
		{
			boundries = new Rect(795f, 490f, 70f, 30f),
			Alignment = 4,
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 14,
			text = string.Format(StaticData.Translate("universe_map_fraction_income_value"), c.incomeBonus)
		};
		base.AddGuiElement(guiLabel5);
		this.forDelete.Add(guiLabel5);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("NewGUI", "pvp_ranking_spacer_header");
		guiTexture.boundries = new Rect(235f, 493f, 630f, 1f);
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("NewGUI", "pvp_ranking_spacer_header");
		rect.boundries = new Rect(235f, 517f, 630f, 1f);
		base.AddGuiElement(rect);
		this.forDelete.Add(rect);
	}

	private void DrawSingleUnit(byte unitType, int index)
	{
		ExtractionPointScreen.<DrawSingleUnit>c__AnonStorey1A variable = null;
		bool flag = false;
		byte num = 0;
		byte num1 = unitType;
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
		if (num == 0)
		{
			flag = true;
			num = 1;
		}
		ExtractionPointUnit extractionPointUnit = Enumerable.First<ExtractionPointUnit>(Enumerable.Where<ExtractionPointUnit>(this.pointInfo.allUnits, new Func<ExtractionPointUnit, bool>(variable, (ExtractionPointUnit u) => (u.unitType != this.unitType ? false : u.upgrade == this.unitUpgradeLevel))));
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("NewGUI", "pvp_avatar");
		guiTexture.boundries = new Rect(225f, (float)(110 + 85 * index), 70f, 70f);
		base.AddGuiElement(guiTexture);
		this.forDelete2.Add(guiTexture);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(300f, (float)(110 + 85 * index), 360f, 16f),
			Alignment = 3,
			Font = GuiLabel.FontBold,
			FontSize = 14,
			text = StaticData.Translate(extractionPointUnit.name)
		};
		base.AddGuiElement(guiLabel);
		this.forDelete2.Add(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(300f, (float)(130 + 85 * index), 180f, 16f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			text = StaticData.Translate("ep_screen_unit_damage")
		};
		base.AddGuiElement(guiLabel1);
		this.forDelete2.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(300f, (float)(130 + 85 * index), 180f, 16f),
			Alignment = 5,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			text = extractionPointUnit.damage.ToString("##,##0")
		};
		base.AddGuiElement(guiLabel2);
		this.forDelete2.Add(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(490f, (float)(130 + 85 * index), 180f, 16f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			text = StaticData.Translate("ep_screen_unit_population")
		};
		base.AddGuiElement(guiLabel3);
		this.forDelete2.Add(guiLabel3);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			boundries = new Rect(490f, (float)(130 + 85 * index), 180f, 16f),
			Alignment = 5,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			text = extractionPointUnit.population.ToString("##,##0")
		};
		base.AddGuiElement(guiLabel4);
		this.forDelete2.Add(guiLabel4);
		GuiLabel guiLabel5 = new GuiLabel()
		{
			boundries = new Rect(300f, (float)(147 + 85 * index), 180f, 16f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			text = StaticData.Translate("ep_screen_unit_hitpoints")
		};
		base.AddGuiElement(guiLabel5);
		this.forDelete2.Add(guiLabel5);
		GuiLabel guiLabel6 = new GuiLabel()
		{
			boundries = new Rect(300f, (float)(147 + 85 * index), 180f, 16f),
			Alignment = 5,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			text = extractionPointUnit.hitPoints.ToString("##,##0")
		};
		base.AddGuiElement(guiLabel6);
		this.forDelete2.Add(guiLabel6);
		GuiLabel guiLabel7 = new GuiLabel()
		{
			boundries = new Rect(490f, (float)(147 + 85 * index), 180f, 16f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			text = StaticData.Translate("ep_screen_unit_range")
		};
		base.AddGuiElement(guiLabel7);
		this.forDelete2.Add(guiLabel7);
		GuiLabel guiLabel8 = new GuiLabel()
		{
			boundries = new Rect(490f, (float)(147 + 85 * index), 180f, 16f),
			Alignment = 5,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			text = extractionPointUnit.range.ToString("##,##0")
		};
		base.AddGuiElement(guiLabel8);
		this.forDelete2.Add(guiLabel8);
		GuiLabel guiLabel9 = new GuiLabel()
		{
			boundries = new Rect(300f, (float)(164 + 85 * index), 180f, 16f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			text = StaticData.Translate("ep_screen_unit_cooldown")
		};
		base.AddGuiElement(guiLabel9);
		this.forDelete2.Add(guiLabel9);
		GuiLabel guiLabel10 = new GuiLabel()
		{
			boundries = new Rect(300f, (float)(164 + 85 * index), 180f, 16f),
			Alignment = 5,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			text = string.Format(StaticData.Translate("key_dock_weapons_upgrade_stats_cooldown_base"), (float)extractionPointUnit.cooldown * 0.001f)
		};
		base.AddGuiElement(guiLabel10);
		this.forDelete2.Add(guiLabel10);
		GuiLabel guiLabel11 = new GuiLabel()
		{
			boundries = new Rect(490f, (float)(164 + 85 * index), 180f, 16f),
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			text = StaticData.Translate("ep_screen_unit_penetration")
		};
		base.AddGuiElement(guiLabel11);
		this.forDelete2.Add(guiLabel11);
		GuiLabel guiLabel12 = new GuiLabel()
		{
			boundries = new Rect(490f, (float)(164 + 85 * index), 180f, 16f),
			Alignment = 5,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			text = extractionPointUnit.penetration.ToString("##,##0")
		};
		base.AddGuiElement(guiLabel12);
		this.forDelete2.Add(guiLabel12);
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("NewGUI", "pvp_ranking_spacer");
		rect.boundries = new Rect(225f, (float)(187 + 85 * index), 655f, 1f);
		base.AddGuiElement(rect);
		this.forDelete2.Add(rect);
		GuiLabel guiLabel13 = new GuiLabel()
		{
			boundries = new Rect(700f, (float)(118 + 85 * index), 180f, 52f),
			Alignment = 4,
			TextColor = GuiNewStyleBar.blueColor,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			text = StaticData.Translate("ep_screen_unit_buy_or")
		};
		base.AddGuiElement(guiLabel13);
		this.forDelete2.Add(guiLabel13);
		if (flag)
		{
			guiLabel13.text = StaticData.Translate("ep_screen_unit_reserch_first");
			GuiTexture guiTexture1 = new GuiTexture()
			{
				boundries = new Rect(225f, (float)(110 + 85 * index), 655f, 70f)
			};
			guiTexture1.SetTextureKeepSize("NewGUI", "ownRowBg");
			base.AddGuiElement(guiTexture1);
			this.forDelete2.Add(guiTexture1);
		}
		else
		{
			GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
			guiButtonResizeable.SetSmallOrangeTexture();
			guiButtonResizeable.X = 700f;
			guiButtonResizeable.Y = (float)(113 + 85 * index);
			guiButtonResizeable.boundries.set_width(180f);
			guiButtonResizeable.Caption = string.Format(StaticData.Translate("ep_screen_unit_buy_price"), extractionPointUnit.price);
			guiButtonResizeable.FontSize = 14;
			guiButtonResizeable.Alignment = 4;
			guiButtonResizeable.isEnabled = (this.isWaitingRefresh || !this.CanBuildUnit(extractionPointUnit.unitType, extractionPointUnit.population) ? false : this.CanPayFor(1, extractionPointUnit.price));
			EventHandlerParam eventHandlerParam = new EventHandlerParam();
			ExtractionPointScreen.PoIParams poIParam = new ExtractionPointScreen.PoIParams()
			{
				type = unitType,
				currency = 1
			};
			eventHandlerParam.customData = poIParam;
			guiButtonResizeable.eventHandlerParam = eventHandlerParam;
			guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, ExtractionPointScreen.OnUnitBuyClicked);
			base.AddGuiElement(guiButtonResizeable);
			this.forDelete2.Add(guiButtonResizeable);
			this.buttonsOnScreen.Add(guiButtonResizeable);
			GuiTexture rect1 = new GuiTexture();
			rect1.SetTexture("NewGUI", "ep_icon_nova");
			rect1.boundries = new Rect(708f, (float)(120 + 85 * index), 18f, 13f);
			base.AddGuiElement(rect1);
			this.forDelete2.Add(rect1);
			guiLabel13.text = StaticData.Translate("ep_screen_unit_buy_or");
			GuiButtonResizeable action = new GuiButtonResizeable();
			action.SetTexture("NewGUI", "button_purple_small_");
			action.X = 700f;
			action.Y = (float)(152 + 85 * index);
			action.boundries.set_width(180f);
			action.Caption = string.Format(StaticData.Translate("ep_screen_unit_buy_price"), extractionPointUnit.price * 2);
			action.FontSize = 14;
			action.Alignment = 4;
			action.isEnabled = (this.isWaitingRefresh || !this.CanBuildUnit(extractionPointUnit.unitType, extractionPointUnit.population) ? false : this.CanPayFor(2, extractionPointUnit.price * 2));
			eventHandlerParam = new EventHandlerParam();
			poIParam = new ExtractionPointScreen.PoIParams()
			{
				type = unitType,
				currency = 2
			};
			eventHandlerParam.customData = poIParam;
			action.eventHandlerParam = eventHandlerParam;
			action.Clicked = new Action<EventHandlerParam>(this, ExtractionPointScreen.OnUnitBuyClicked);
			base.AddGuiElement(action);
			this.forDelete2.Add(action);
			this.buttonsOnScreen.Add(action);
			GuiTexture guiTexture2 = new GuiTexture();
			guiTexture2.SetTexture("NewGUI", "ep_icon_eqilibrium");
			guiTexture2.boundries = new Rect(710f, (float)(157 + 85 * index), 15f, 16f);
			base.AddGuiElement(guiTexture2);
			this.forDelete2.Add(guiTexture2);
		}
		GuiTexture rect2 = new GuiTexture();
		rect2.SetTexture("Targeting", extractionPointUnit.assetName);
		rect2.boundries = new Rect(226f, (float)(111 + 85 * index), 68f, 68f);
		base.AddGuiElement(rect2);
		this.forDelete2.Add(rect2);
		GuiTexture guiTexture3 = new GuiTexture();
		guiTexture3.SetTexture("NewGUI", "ep_cnt");
		guiTexture3.boundries = new Rect(225f, (float)(166 + 85 * index), 25f, 14f);
		base.AddGuiElement(guiTexture3);
		this.forDelete2.Add(guiTexture3);
		GuiLabel str = new GuiLabel()
		{
			boundries = new Rect(225f, (float)(166 + 85 * index), 22f, 14f),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 12
		};
		int item = this.pointInRange.defendersCountByKind.get_Item((int)unitType);
		str.text = item.ToString();
		base.AddGuiElement(str);
		this.forDelete2.Add(str);
	}

	private void DrawSingleUpgrade(byte upgradeType, int index)
	{
		ExtractionPointScreen.<DrawSingleUpgrade>c__AnonStorey1B variable = null;
		bool flag = false;
		int num = 0;
		bool flag1 = false;
		float single = 0f;
		byte num1 = 0;
		byte num2 = upgradeType;
		switch (num2)
		{
			case 51:
			{
				num1 = this.pointInRange.upgradesAlien1;
				break;
			}
			case 52:
			{
				num1 = this.pointInRange.upgradesAlien2;
				break;
			}
			case 53:
			{
				num1 = this.pointInRange.upgradesAlien3;
				break;
			}
			case 54:
			{
				num1 = this.pointInRange.upgradesAlien4;
				break;
			}
			case 55:
			{
				num1 = this.pointInRange.upgradesAlien5;
				break;
			}
			default:
			{
				switch (num2)
				{
					case 101:
					{
						num1 = this.pointInRange.upgradesTurret1;
						break;
					}
					case 102:
					{
						num1 = this.pointInRange.upgradesTurret2;
						break;
					}
					case 103:
					{
						num1 = this.pointInRange.upgradesTurret3;
						break;
					}
					case 104:
					{
						num1 = this.pointInRange.upgradesTurret4;
						break;
					}
					case 105:
					{
						num1 = this.pointInRange.upgradesTurret5;
						break;
					}
					default:
					{
						switch (num2)
						{
							case 1:
							{
								num1 = this.pointInRange.upgradesPopulation;
								single = (float)this.pointInRange.populationAliens;
								break;
							}
							case 2:
							{
								num1 = this.pointInRange.upgradesUltralibrium;
								single = this.pointInRange.incomeGeneration;
								break;
							}
							case 3:
							{
								num1 = this.pointInRange.upgradesBarracks;
								single = (float)this.pointInRange.upgradesBarracks;
								break;
							}
							case 4:
							{
								num1 = this.pointInRange.upgradesHitPoints;
								single = (float)this.pointInRange.hitPoints;
								break;
							}
						}
						break;
					}
				}
				break;
			}
		}
		num1 = (byte)(num1 + 1);
		if ((upgradeType == 51 || upgradeType == 101) && this.pointInRange.upgradesBarracks < 1 && num1 < 6)
		{
			flag = true;
			num = 1;
		}
		if ((upgradeType == 52 || upgradeType == 102) && this.pointInRange.upgradesBarracks < 2 && num1 < 6)
		{
			flag = true;
			num = 2;
		}
		if ((upgradeType == 53 || upgradeType == 103) && this.pointInRange.upgradesBarracks < 3 && num1 < 6)
		{
			flag = true;
			num = 3;
		}
		if ((upgradeType == 54 || upgradeType == 104) && this.pointInRange.upgradesBarracks < 4 && num1 < 6)
		{
			flag = true;
			num = 4;
		}
		if ((upgradeType == 55 || upgradeType == 105) && this.pointInRange.upgradesBarracks < 5 && num1 < 6)
		{
			flag = true;
			num = 5;
		}
		if (num1 == 6)
		{
			flag1 = true;
			num1 = 5;
		}
		ExtractionPointUpgrade extractionPointUpgrade = Enumerable.First<ExtractionPointUpgrade>(Enumerable.Where<ExtractionPointUpgrade>(this.pointInfo.allUpgrades, new Func<ExtractionPointUpgrade, bool>(variable, (ExtractionPointUpgrade u) => (u.upgradeType != this.upgradeType ? false : u.upgrade == this.upgradeLevel))));
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("NewGUI", "pvp_avatar");
		guiTexture.boundries = new Rect(225f, (float)(110 + 85 * index), 70f, 70f);
		base.AddGuiElement(guiTexture);
		this.forDelete2.Add(guiTexture);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(300f, (float)(110 + 85 * index), 360f, 16f),
			Alignment = 3,
			Font = GuiLabel.FontBold,
			FontSize = 14,
			text = string.Format(StaticData.Translate("ep_screen_upgrade_name"), StaticData.Translate(extractionPointUpgrade.name), num1)
		};
		base.AddGuiElement(guiLabel);
		this.forDelete2.Add(guiLabel);
		if (upgradeType == 51 || upgradeType == 52 || upgradeType == 53 || upgradeType == 54 || upgradeType == 55 || upgradeType == 101 || upgradeType == 102 || upgradeType == 103 || upgradeType == 104 || upgradeType == 105)
		{
			ExtractionPointUnit extractionPointUnit = new ExtractionPointUnit();
			ExtractionPointUnit extractionPointUnit1 = Enumerable.First<ExtractionPointUnit>(Enumerable.Where<ExtractionPointUnit>(this.pointInfo.allUnits, new Func<ExtractionPointUnit, bool>(variable, (ExtractionPointUnit t) => (t.unitType != this.upgradeType ? false : t.upgrade == this.upgradeLevel))));
			if (num1 != 1)
			{
				extractionPointUnit = (!flag1 ? Enumerable.First<ExtractionPointUnit>(Enumerable.Where<ExtractionPointUnit>(this.pointInfo.allUnits, new Func<ExtractionPointUnit, bool>(variable, (ExtractionPointUnit t) => (t.unitType != this.upgradeType ? false : t.upgrade == this.upgradeLevel - 1)))) : Enumerable.First<ExtractionPointUnit>(Enumerable.Where<ExtractionPointUnit>(this.pointInfo.allUnits, new Func<ExtractionPointUnit, bool>(variable, (ExtractionPointUnit t) => (t.unitType != this.upgradeType ? false : t.upgrade == this.upgradeLevel)))));
			}
			else
			{
				extractionPointUnit.damage = 0f;
				extractionPointUnit.hitPoints = 0f;
				extractionPointUnit.population = 0;
			}
			GuiLabel guiLabel1 = new GuiLabel()
			{
				boundries = new Rect(300f, (float)(130 + 85 * index), 180f, 16f),
				Alignment = 3,
				TextColor = GuiNewStyleBar.blueColor,
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = StaticData.Translate("ep_screen_unit_damage")
			};
			base.AddGuiElement(guiLabel1);
			this.forDelete2.Add(guiLabel1);
			GuiLabel guiLabel2 = new GuiLabel()
			{
				boundries = new Rect(300f, (float)(130 + 85 * index), 180f, 16f),
				Alignment = 5,
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = extractionPointUnit.damage.ToString("##,##0")
			};
			base.AddGuiElement(guiLabel2);
			this.forDelete2.Add(guiLabel2);
			GuiLabel guiLabel3 = new GuiLabel()
			{
				boundries = new Rect(490f, (float)(130 + 85 * index), 180f, 16f),
				Alignment = 3,
				TextColor = GuiNewStyleBar.blueColor,
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = StaticData.Translate("ep_screen_fortify_update_to")
			};
			base.AddGuiElement(guiLabel3);
			this.forDelete2.Add(guiLabel3);
			GuiLabel guiLabel4 = new GuiLabel()
			{
				boundries = new Rect(490f, (float)(130 + 85 * index), 180f, 16f),
				Alignment = 5,
				TextColor = GuiNewStyleBar.orangeColor,
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = extractionPointUnit1.damage.ToString("##,##0")
			};
			base.AddGuiElement(guiLabel4);
			this.forDelete2.Add(guiLabel4);
			GuiLabel guiLabel5 = new GuiLabel()
			{
				boundries = new Rect(300f, (float)(147 + 85 * index), 180f, 16f),
				Alignment = 3,
				TextColor = GuiNewStyleBar.blueColor,
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = StaticData.Translate("ep_screen_unit_hitpoints")
			};
			base.AddGuiElement(guiLabel5);
			this.forDelete2.Add(guiLabel5);
			GuiLabel guiLabel6 = new GuiLabel()
			{
				boundries = new Rect(300f, (float)(147 + 85 * index), 180f, 16f),
				Alignment = 5,
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = extractionPointUnit.hitPoints.ToString("##,##0")
			};
			base.AddGuiElement(guiLabel6);
			this.forDelete2.Add(guiLabel6);
			GuiLabel guiLabel7 = new GuiLabel()
			{
				boundries = new Rect(490f, (float)(147 + 85 * index), 180f, 16f),
				Alignment = 3,
				TextColor = GuiNewStyleBar.blueColor,
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = StaticData.Translate("ep_screen_fortify_update_to")
			};
			base.AddGuiElement(guiLabel7);
			this.forDelete2.Add(guiLabel7);
			GuiLabel guiLabel8 = new GuiLabel()
			{
				boundries = new Rect(490f, (float)(147 + 85 * index), 180f, 16f),
				Alignment = 5,
				TextColor = GuiNewStyleBar.orangeColor,
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = extractionPointUnit1.hitPoints.ToString("##,##0")
			};
			base.AddGuiElement(guiLabel8);
			this.forDelete2.Add(guiLabel8);
			GuiLabel guiLabel9 = new GuiLabel()
			{
				boundries = new Rect(300f, (float)(164 + 85 * index), 180f, 16f),
				Alignment = 3,
				TextColor = GuiNewStyleBar.blueColor,
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = StaticData.Translate("ep_screen_unit_population")
			};
			base.AddGuiElement(guiLabel9);
			this.forDelete2.Add(guiLabel9);
			GuiLabel guiLabel10 = new GuiLabel()
			{
				boundries = new Rect(300f, (float)(164 + 85 * index), 180f, 16f),
				Alignment = 5,
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = extractionPointUnit.population.ToString("##,##0")
			};
			base.AddGuiElement(guiLabel10);
			this.forDelete2.Add(guiLabel10);
			GuiLabel guiLabel11 = new GuiLabel()
			{
				boundries = new Rect(490f, (float)(164 + 85 * index), 180f, 16f),
				Alignment = 3,
				TextColor = GuiNewStyleBar.blueColor,
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = StaticData.Translate("ep_screen_fortify_update_to")
			};
			base.AddGuiElement(guiLabel11);
			this.forDelete2.Add(guiLabel11);
			GuiLabel guiLabel12 = new GuiLabel()
			{
				boundries = new Rect(490f, (float)(164 + 85 * index), 180f, 16f),
				Alignment = 5,
				TextColor = GuiNewStyleBar.orangeColor,
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = extractionPointUnit1.population.ToString("##,##0")
			};
			base.AddGuiElement(guiLabel12);
			this.forDelete2.Add(guiLabel12);
		}
		else if (upgradeType == 1 || upgradeType == 2 || upgradeType == 3 || upgradeType == 4)
		{
			GuiLabel guiLabel13 = new GuiLabel()
			{
				boundries = new Rect(300f, (float)(130 + 85 * index), 360f, 16f),
				Alignment = 3,
				TextColor = GuiNewStyleBar.blueColor,
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = StaticData.Translate(extractionPointUpgrade.description)
			};
			base.AddGuiElement(guiLabel13);
			this.forDelete2.Add(guiLabel13);
			GuiLabel guiLabel14 = new GuiLabel()
			{
				boundries = new Rect(300f, (float)(147 + 85 * index), 180f, 16f),
				Alignment = 3,
				TextColor = GuiNewStyleBar.blueColor,
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = StaticData.Translate("ep_screen_fortify_current")
			};
			base.AddGuiElement(guiLabel14);
			this.forDelete2.Add(guiLabel14);
			GuiLabel guiLabel15 = new GuiLabel()
			{
				boundries = new Rect(300f, (float)(147 + 85 * index), 180f, 16f),
				Alignment = 5,
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = single.ToString("##,##0")
			};
			base.AddGuiElement(guiLabel15);
			this.forDelete2.Add(guiLabel15);
			GuiLabel guiLabel16 = new GuiLabel()
			{
				boundries = new Rect(300f, (float)(164 + 85 * index), 180f, 16f),
				Alignment = 3,
				TextColor = GuiNewStyleBar.blueColor,
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = StaticData.Translate("ep_screen_fortify_next_level")
			};
			base.AddGuiElement(guiLabel16);
			this.forDelete2.Add(guiLabel16);
			GuiLabel str = new GuiLabel()
			{
				boundries = new Rect(300f, (float)(164 + 85 * index), 180f, 16f),
				Alignment = 5,
				TextColor = GuiNewStyleBar.orangeColor,
				Font = GuiLabel.FontBold,
				FontSize = 12
			};
			if (upgradeType != 3)
			{
				str.text = extractionPointUpgrade.@value.ToString("##,##0");
			}
			else
			{
				float single1 = Math.Min(single + 1f, 5f);
				str.text = single1.ToString("##,##0");
			}
			base.AddGuiElement(str);
			this.forDelete2.Add(str);
		}
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("NewGUI", "pvp_ranking_spacer");
		rect.boundries = new Rect(225f, (float)(187 + 85 * index), 655f, 1f);
		base.AddGuiElement(rect);
		this.forDelete2.Add(rect);
		GuiLabel guiLabel17 = new GuiLabel()
		{
			boundries = new Rect(700f, (float)(118 + 85 * index), 180f, 52f),
			Alignment = 4,
			TextColor = GuiNewStyleBar.blueColor,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			text = StaticData.Translate("ep_screen_unit_buy_or")
		};
		base.AddGuiElement(guiLabel17);
		this.forDelete2.Add(guiLabel17);
		if (flag1 || flag)
		{
			GuiTexture guiTexture1 = new GuiTexture()
			{
				boundries = new Rect(225f, (float)(110 + 85 * index), 655f, 70f)
			};
			guiTexture1.SetTextureKeepSize("NewGUI", "ownRowBg");
			base.AddGuiElement(guiTexture1);
			this.forDelete2.Add(guiTexture1);
			if (flag1)
			{
				guiLabel17.text = StaticData.Translate("ep_screen_fortify_max_level");
				guiLabel17.TextColor = GuiNewStyleBar.orangeColor;
			}
			if (flag)
			{
				guiLabel17.text = string.Format(StaticData.Translate("ep_screen_fortify_req_barracks"), num);
			}
		}
		else
		{
			GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
			guiButtonResizeable.SetSmallOrangeTexture();
			guiButtonResizeable.X = 700f;
			guiButtonResizeable.Y = (float)(113 + 85 * index);
			guiButtonResizeable.boundries.set_width(180f);
			guiButtonResizeable.Caption = string.Format(StaticData.Translate("ep_screen_unit_buy_price"), extractionPointUpgrade.price);
			guiButtonResizeable.FontSize = 14;
			guiButtonResizeable.Alignment = 4;
			guiButtonResizeable.isEnabled = (this.isWaitingRefresh ? false : this.CanPayFor(1, extractionPointUpgrade.price));
			EventHandlerParam eventHandlerParam = new EventHandlerParam();
			ExtractionPointScreen.PoIParams poIParam = new ExtractionPointScreen.PoIParams()
			{
				type = upgradeType,
				level = num1,
				currency = 1
			};
			eventHandlerParam.customData = poIParam;
			guiButtonResizeable.eventHandlerParam = eventHandlerParam;
			guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, ExtractionPointScreen.OnUpgradeBuyClicked);
			base.AddGuiElement(guiButtonResizeable);
			this.forDelete2.Add(guiButtonResizeable);
			this.buttonsOnScreen.Add(guiButtonResizeable);
			GuiTexture rect1 = new GuiTexture();
			rect1.SetTexture("NewGUI", "ep_icon_nova");
			rect1.boundries = new Rect(708f, (float)(120 + 85 * index), 18f, 13f);
			base.AddGuiElement(rect1);
			this.forDelete2.Add(rect1);
			GuiButtonResizeable action = new GuiButtonResizeable();
			action.SetTexture("NewGUI", "button_purple_small_");
			action.X = 700f;
			action.Y = (float)(152 + 85 * index);
			action.boundries.set_width(180f);
			action.Caption = string.Format(StaticData.Translate("ep_screen_unit_buy_price"), extractionPointUpgrade.price * 2);
			action.FontSize = 14;
			action.Alignment = 4;
			action.isEnabled = (this.isWaitingRefresh ? false : this.CanPayFor(2, extractionPointUpgrade.price * 2));
			eventHandlerParam = new EventHandlerParam();
			poIParam = new ExtractionPointScreen.PoIParams()
			{
				type = upgradeType,
				level = num1,
				currency = 2
			};
			eventHandlerParam.customData = poIParam;
			action.eventHandlerParam = eventHandlerParam;
			action.Clicked = new Action<EventHandlerParam>(this, ExtractionPointScreen.OnUpgradeBuyClicked);
			base.AddGuiElement(action);
			this.forDelete2.Add(action);
			this.buttonsOnScreen.Add(action);
			GuiTexture guiTexture2 = new GuiTexture();
			guiTexture2.SetTexture("NewGUI", "ep_icon_eqilibrium");
			guiTexture2.boundries = new Rect(710f, (float)(157 + 85 * index), 15f, 16f);
			base.AddGuiElement(guiTexture2);
			this.forDelete2.Add(guiTexture2);
		}
		GuiTexture rect2 = new GuiTexture();
		rect2.SetTexture("Targeting", extractionPointUpgrade.assetName);
		rect2.boundries = new Rect(226f, (float)(111 + 85 * index), 68f, 68f);
		base.AddGuiElement(rect2);
		this.forDelete2.Add(rect2);
	}

	public void DrawTopContributors(List<Contributor> contributors)
	{
		// 
		// Current member / type: System.Void ExtractionPointScreen::DrawTopContributors(System.Collections.Generic.List`1<Contributor>)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void DrawTopContributors(System.Collections.Generic.List<Contributor>)
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

	private bool ItsMe(Contributor c)
	{
		string str = NetworkScript.player.vessel.playerName;
		return c.displayName == str;
	}

	private void OnAddOnsUpgradeClicked(object prm)
	{
		ExtractionPointScreen.selectedFortifyTab = 1;
		this.ClearRightMenu();
		this.DrawSingleUpgrade(1, 0);
		this.DrawSingleUpgrade(2, 1);
		this.DrawSingleUpgrade(3, 2);
		this.DrawSingleUpgrade(4, 3);
	}

	private void OnBankBtnClicked(object prm)
	{
		EventHandlerParam eventHandlerParam;
		if (!this.haveGuild)
		{
			MainScreenWindow mainScreenWindow = AndromedaGui.mainWnd;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = (byte)11
			};
			mainScreenWindow.OnWindowBtnClicked(eventHandlerParam);
		}
		else
		{
			MainScreenWindow mainScreenWindow1 = AndromedaGui.mainWnd;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = (byte)18
			};
			mainScreenWindow1.OnWindowBtnClicked(eventHandlerParam);
		}
	}

	public override void OnClose()
	{
		playWebGame.udp.ExecuteCommand(166, null);
	}

	private void OnFortifyClicked(object prm)
	{
		// 
		// Current member / type: System.Void ExtractionPointScreen::OnFortifyClicked(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnFortifyClicked(System.Object)
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

	private void OnOverviewClicked(object prm)
	{
		// 
		// Current member / type: System.Void ExtractionPointScreen::OnOverviewClicked(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnOverviewClicked(System.Object)
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

	private void OnShipsClicked(object prm)
	{
		ExtractionPointScreen.selectedFortifyTab = 4;
		this.ClearRightMenu();
		this.DrawSingleUnit(51, 0);
		this.DrawSingleUnit(52, 1);
		this.DrawSingleUnit(53, 2);
		this.DrawSingleUnit(54, 3);
		this.DrawSingleUnit(55, 4);
	}

	private void OnShipUpgradeClicked(object prm)
	{
		ExtractionPointScreen.selectedFortifyTab = 2;
		this.ClearRightMenu();
		this.DrawSingleUpgrade(51, 0);
		this.DrawSingleUpgrade(52, 1);
		this.DrawSingleUpgrade(53, 2);
		this.DrawSingleUpgrade(54, 3);
		this.DrawSingleUpgrade(55, 4);
	}

	private void OnTurretsClicked(object prm)
	{
		ExtractionPointScreen.selectedFortifyTab = 5;
		this.ClearRightMenu();
		this.DrawSingleUnit(101, 0);
		this.DrawSingleUnit(102, 1);
		this.DrawSingleUnit(103, 2);
		this.DrawSingleUnit(104, 3);
		this.DrawSingleUnit(105, 4);
	}

	private void OnTurretUpgradeClicked(object prm)
	{
		ExtractionPointScreen.selectedFortifyTab = 3;
		this.ClearRightMenu();
		this.DrawSingleUpgrade(101, 0);
		this.DrawSingleUpgrade(102, 1);
		this.DrawSingleUpgrade(103, 2);
		this.DrawSingleUpgrade(104, 3);
		this.DrawSingleUpgrade(105, 4);
	}

	private void OnUnitBuyClicked(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void ExtractionPointScreen::OnUnitBuyClicked(EventHandlerParam)
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
		// Current member / type: System.Void ExtractionPointScreen::OnUpgradeBuyClicked(EventHandlerParam)
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

	public void Populate()
	{
		this.Clear();
		this.isWaitingRefresh = false;
		if (ExtractionPointScreen.selectedTab == 1)
		{
			this.OnOverviewClicked(null);
		}
		else if (ExtractionPointScreen.selectedTab == 2)
		{
			this.OnFortifyClicked(null);
		}
	}

	private void SetWaitingStatus()
	{
		this.isWaitingRefresh = true;
		foreach (GuiButtonResizeable guiButtonResizeable in this.buttonsOnScreen)
		{
			guiButtonResizeable.isEnabled = false;
		}
	}

	public void UpdateGuildBank(Guild g)
	{
		if (g.id != 0)
		{
			NetworkScript.player.guild.bankUltralibrium = g.bankUltralibrium;
			NetworkScript.player.guild.bankNova = g.bankNova;
			NetworkScript.player.guild.bankEquilib = g.bankEquilib;
			if (ExtractionPointScreen.selectedTab == 2)
			{
				this.OnFortifyClicked(null);
			}
		}
		else
		{
			NetworkScript.player.guild = null;
			NetworkScript.player.guildMember = null;
			this.haveGuild = false;
		}
	}

	public void UpdateGuildData(Guild g)
	{
		// 
		// Current member / type: System.Void ExtractionPointScreen::UpdateGuildData(Guild)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void UpdateGuildData(Guild)
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