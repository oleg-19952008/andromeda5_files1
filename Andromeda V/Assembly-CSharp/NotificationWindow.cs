using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NotificationWindow
{
	public static int NOTIFICATION_TIME_ON_SCREEN;

	public static int SYSTEM_NOTIFICATION_TIME_ON_SCREEN;

	private static List<string> notificationsQueue;

	static NotificationWindow()
	{
		NotificationWindow.NOTIFICATION_TIME_ON_SCREEN = 5000;
		NotificationWindow.SYSTEM_NOTIFICATION_TIME_ON_SCREEN = 10000;
		NotificationWindow.notificationsQueue = new List<string>();
	}

	public NotificationWindow()
	{
	}

	public static GuiWindow CreateLevelUpNotification(int playerLevel, Action<EventHandlerParam> onClick, Notification notify)
	{
		LevelsInfo item = StaticData.levelsType.get_Item(playerLevel);
		GuiWindow guiWindow = new GuiWindow();
		guiWindow.SetBackgroundTexture("FrameworkGUI", "notification_frame");
		guiWindow.PutToHorizontalCenter();
		guiWindow.boundries.set_y((float)Screen.get_height());
		guiWindow.zOrder = 230;
		guiWindow.isHidden = false;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("FrameworkGUI", "item_frame");
		guiTexture.X = -1f;
		guiTexture.Y = 4f;
		guiWindow.AddGuiElement(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("GUI", "notification_medal");
		guiTexture1.X = 11f;
		guiTexture1.Y = 0f;
		guiWindow.AddGuiElement(guiTexture1);
		float textWidth = 105f;
		float single = 3f;
		List<GuiElement> list = new List<GuiElement>();
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("GUI", "icon_notification_skillPoint");
		rect.boundries = new Rect(textWidth, 39f, 32f, 32f);
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = string.Format("{0} {1}", StaticData.Translate("key_skills_tree_skill_points"), 1),
			customData2 = rect
		};
		rect.tooltipWindowParam = eventHandlerParam;
		rect.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		guiWindow.AddGuiElement(rect);
		list.Add(rect);
		textWidth = textWidth + (32f + single);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(textWidth, 39f, 100f, 32f),
			Alignment = 3,
			FontSize = 12,
			TextColor = GuiNewStyleBar.orangeColor,
			Font = GuiLabel.FontBold,
			text = string.Format("x {0}", 1)
		};
		guiLabel.boundries.set_width(guiLabel.TextWidth);
		guiWindow.AddGuiElement(guiLabel);
		list.Add(guiLabel);
		textWidth = textWidth + (guiLabel.TextWidth + single);
		if (item.novaReward != 0)
		{
			GuiTexture drawTooltipWindow = new GuiTexture();
			drawTooltipWindow.SetTexture("GUI", "icon_notification_nova");
			drawTooltipWindow.boundries = new Rect(textWidth, 39f, 32f, 32f);
			eventHandlerParam = new EventHandlerParam()
			{
				customData = string.Format(StaticData.Translate("key_nova_shop_exchange_nova"), item.novaReward),
				customData2 = drawTooltipWindow
			};
			drawTooltipWindow.tooltipWindowParam = eventHandlerParam;
			drawTooltipWindow.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			guiWindow.AddGuiElement(drawTooltipWindow);
			list.Add(drawTooltipWindow);
			textWidth = textWidth + (32f + single);
			GuiLabel guiLabel1 = new GuiLabel()
			{
				boundries = new Rect(textWidth, 39f, 100f, 32f),
				Alignment = 3,
				FontSize = 12,
				TextColor = GuiNewStyleBar.orangeColor,
				Font = GuiLabel.FontBold,
				text = string.Format("x {0}", item.novaReward)
			};
			guiLabel1.boundries.set_width(guiLabel1.TextWidth);
			guiWindow.AddGuiElement(guiLabel1);
			list.Add(guiLabel1);
			textWidth = textWidth + (guiLabel1.TextWidth + single);
		}
		if (item.cashReward != 0)
		{
			GuiTexture rect1 = new GuiTexture();
			rect1.SetTexture("GUI", "icon_notification_cash");
			rect1.boundries = new Rect(textWidth, 39f, 32f, 32f);
			eventHandlerParam = new EventHandlerParam()
			{
				customData = string.Format(StaticData.Translate("key_nova_shop_exchange_cash"), item.cashReward),
				customData2 = rect1
			};
			rect1.tooltipWindowParam = eventHandlerParam;
			rect1.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			guiWindow.AddGuiElement(rect1);
			list.Add(rect1);
			textWidth = textWidth + (32f + single);
			GuiLabel guiLabel2 = new GuiLabel()
			{
				boundries = new Rect(textWidth, 39f, 100f, 32f),
				Alignment = 3,
				FontSize = 12,
				TextColor = GuiNewStyleBar.orangeColor,
				Font = GuiLabel.FontBold,
				text = string.Format("x {0}", item.cashReward)
			};
			guiLabel2.boundries.set_width(guiLabel2.TextWidth);
			guiWindow.AddGuiElement(guiLabel2);
			list.Add(guiLabel2);
			textWidth = textWidth + (guiLabel2.TextWidth + single);
		}
		if (item.neuronReward != 0)
		{
			GuiTexture drawTooltipWindow1 = new GuiTexture();
			drawTooltipWindow1.SetTexture("GUI", "icon_notification_neuronModule");
			drawTooltipWindow1.boundries = new Rect(textWidth, 39f, 32f, 32f);
			eventHandlerParam = new EventHandlerParam()
			{
				customData = string.Format("{0} {1}", StaticData.Translate("key_skills_tree_neuron_modules"), item.neuronReward),
				customData2 = drawTooltipWindow1
			};
			drawTooltipWindow1.tooltipWindowParam = eventHandlerParam;
			drawTooltipWindow1.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			guiWindow.AddGuiElement(drawTooltipWindow1);
			list.Add(drawTooltipWindow1);
			textWidth = textWidth + (32f + single);
			GuiLabel guiLabel3 = new GuiLabel()
			{
				boundries = new Rect(textWidth, 39f, 100f, 32f),
				Alignment = 3,
				FontSize = 12,
				TextColor = GuiNewStyleBar.orangeColor,
				Font = GuiLabel.FontBold,
				text = string.Format("x {0}", item.neuronReward)
			};
			guiLabel3.boundries.set_width(guiLabel3.TextWidth);
			guiWindow.AddGuiElement(guiLabel3);
			list.Add(guiLabel3);
			textWidth = textWidth + (guiLabel3.TextWidth + single);
		}
		if (item.itemTypeReward != 0)
		{
			GuiTexture guiTexture2 = new GuiTexture();
			guiTexture2.SetTexture("GUI", "icon_notification_booster");
			guiTexture2.boundries = new Rect(textWidth, 39f, 32f, 32f);
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate(StaticData.allTypes.get_Item(item.itemTypeReward).uiName),
				customData2 = guiTexture2
			};
			guiTexture2.tooltipWindowParam = eventHandlerParam;
			guiTexture2.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			guiWindow.AddGuiElement(guiTexture2);
			list.Add(guiTexture2);
			textWidth = textWidth + (32f + single);
			if (PlayerItems.IsBooster(item.itemTypeReward))
			{
				GuiLabel guiLabel4 = new GuiLabel()
				{
					boundries = new Rect(textWidth, 39f, 100f, 32f),
					Alignment = 3,
					FontSize = 12,
					TextColor = GuiNewStyleBar.orangeColor,
					Font = GuiLabel.FontBold,
					text = string.Format("x {0}d", 1)
				};
				guiLabel4.boundries.set_width(guiLabel4.TextWidth);
				guiWindow.AddGuiElement(guiLabel4);
				list.Add(guiLabel4);
				textWidth = textWidth + (guiLabel4.TextWidth + single);
			}
		}
		float single1 = (380f - textWidth) / 2f;
		foreach (GuiElement guiElement in list)
		{
			GuiElement x = guiElement;
			x.X = x.X + single1;
		}
		GuiLabel guiLabel5 = new GuiLabel()
		{
			boundries = new Rect(24f, 22f, 56f, 56f),
			Alignment = 4,
			FontSize = 26,
			TextColor = GuiNewStyleBar.blackColorTransperant,
			Font = GuiLabel.FontBold,
			text = playerLevel.ToString()
		};
		guiWindow.AddGuiElement(guiLabel5);
		GuiLabel guiLabel6 = new GuiLabel()
		{
			boundries = new Rect(105f, 9f, 265f, 20f),
			Alignment = 4,
			FontSize = 14,
			Font = GuiLabel.FontBold,
			text = StaticData.Translate("key_new_level_up_screen_congrats")
		};
		guiWindow.AddGuiElement(guiLabel6);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("FrameworkGUI", "empty");
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.eventHandlerParam.customData = notify;
		guiButtonFixed.Clicked = onClick;
		guiButtonFixed.boundries = new Rect(0f, 0f, guiWindow.boundries.get_width(), guiWindow.boundries.get_height());
		guiWindow.AddGuiElement(guiButtonFixed);
		return guiWindow;
	}

	public static GuiWindow CreateNotificationWindow(string title, string name, byte level, string assetName, Action<EventHandlerParam> onClick, Notification notify, bool playSound = true)
	{
		GuiWindow guiWindow = new GuiWindow();
		guiWindow.SetBackgroundTexture("FrameworkGUI", "notification_frame");
		guiWindow.PutToHorizontalCenter();
		guiWindow.boundries.set_y((float)Screen.get_height());
		guiWindow.zOrder = 230;
		guiWindow.isHidden = false;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("FrameworkGUI", "item_frame");
		guiTexture.X = -1f;
		guiTexture.Y = 4f;
		guiWindow.AddGuiElement(guiTexture);
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("Achievement", assetName);
		rect.boundries = new Rect(11f, 6f, 80f, 80f);
		guiWindow.AddGuiElement(rect);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(105f, 9f, 265f, 20f),
			Alignment = 4,
			FontSize = 14,
			Font = GuiLabel.FontBold,
			text = title
		};
		guiWindow.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(105f, 38f, 265f, 36f),
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			text = name
		};
		guiWindow.AddGuiElement(guiLabel1);
		if (playSound)
		{
			AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("InSpaceVoices", "achievement_unlocked ");
			AudioManager.PlayGUISound(fromStaticSet);
		}
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("FrameworkGUI", "empty");
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.eventHandlerParam.customData = notify;
		guiButtonFixed.Clicked = onClick;
		guiButtonFixed.boundries = new Rect(0f, 0f, guiWindow.boundries.get_width(), guiWindow.boundries.get_height());
		guiWindow.AddGuiElement(guiButtonFixed);
		return guiWindow;
	}

	public static GuiWindow CreateObjectiveDoneNotification(NewQuestObjective objective, Action<EventHandlerParam> onClick, Notification notify)
	{
		NotificationWindow.<CreateObjectiveDoneNotification>c__AnonStorey52 variable = null;
		GuiWindow guiWindow = new GuiWindow();
		guiWindow.SetBackgroundTexture("FrameworkGUI", "notification_frame");
		guiWindow.PutToHorizontalCenter();
		guiWindow.boundries.set_y((float)Screen.get_height());
		guiWindow.zOrder = 230;
		guiWindow.isHidden = false;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("FrameworkGUI", "item_frame");
		guiTexture.X = -1f;
		guiTexture.Y = 4f;
		guiWindow.AddGuiElement(guiTexture);
		GuiTexture rect = new GuiTexture();
		switch (objective.type)
		{
			case 0:
			{
				rect.SetTexture("TutorialWindow", "equipWeapon");
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			case 1:
			case 2:
			case 3:
			case 4:
			case 9:
			{
				if (objective.targetParam1 != PlayerItems.TypeNova)
				{
					rect.SetTexture("MineralsAvatars", StaticData.allTypes.get_Item((ushort)objective.targetParam1).assetName);
					rect.boundries = new Rect(12f, 7f, 80f, 80f);
				}
				else
				{
					rect.SetItemTexture((ushort)objective.targetParam1);
					rect.boundries = new Rect(12f, 20f, 80f, 55f);
				}
				break;
			}
			case 5:
			case 6:
			case 10:
			case 25:
			case 26:
			case 27:
			case 28:
			{
				rect.SetItemTexture((ushort)objective.targetParam1);
				rect.boundries = new Rect(12f, 20f, 80f, 55f);
				break;
			}
			case 7:
			case 8:
			{
				if (objective.targetParam1 == 0)
				{
					rect.SetTexture("QuestObjectivesArt", "DefaultPve");
					rect.boundries = new Rect(20f, 15f, 64f, 64f);
				}
				else
				{
					if (NetworkScript.player.vessel.galaxy.get_galaxyId() != 1000)
					{
						rect.SetTexture("Targeting", TargetingWnd.GetPveAvatarName(Enumerable.First<PvEInfo>(Enumerable.Where<PvEInfo>(StaticData.pveTypes, new Func<PvEInfo, bool>(variable, (PvEInfo t) => t.pveKey == this.objective.targetParam1))).assetName));
					}
					else
					{
						rect.SetTexture("TutorialWindow", "AnnihilatorBoss");
					}
					rect.boundries = new Rect(12f, 7f, 80f, 80f);
				}
				break;
			}
			case 11:
			case 12:
			case 13:
			case 14:
			{
				rect.SetTexture("QuestObjectivesArt", "DefaultPvp");
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			case 15:
			case 16:
			case 29:
			{
				rect.SetTexture("ShipsAvatars", Enumerable.First<ShipsTypeNet>(Enumerable.Where<ShipsTypeNet>(StaticData.shipTypes, new Func<ShipsTypeNet, bool>(variable, (ShipsTypeNet t) => t.id == this.objective.targetParam1))).assetName);
				rect.boundries = new Rect(12f, 20f, 80f, 55f);
				break;
			}
			case 17:
			case 18:
			case 21:
			case 22:
			case 31:
			{
				rect.SetTexture("QuestObjectivesArt", "DefaultExtractionPoint");
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			case 19:
			case 20:
			{
				rect.SetTexture("QuestObjectivesArt", "DefaultMultikill");
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			case 23:
			{
				rect.SetItemTexture(PlayerItems.TypeUltralibrium);
				rect.boundries = new Rect(12f, 20f, 80f, 55f);
				break;
			}
			case 24:
			{
				rect.SetTexture("QuestObjectivesArt", "DefaultPvpDeathmatch");
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			case 30:
			{
				rect.SetTexture("QuestObjectivesArt", "DefaultGuild");
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			case 32:
			case 33:
			case 34:
			{
				rect.SetTexture("QuestObjectivesArt", "DefaultCheckpoint");
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			case 35:
			{
				rect.SetTexture("QuestTrackerAvatars", StaticData.Translate(Enumerable.First<NpcObjectPhysics>(Enumerable.Where<NpcObjectPhysics>(StaticData.allNPCs, new Func<NpcObjectPhysics, bool>(variable, (NpcObjectPhysics t) => t.npcKey == this.objective.targetParam2))).assetName));
				rect.boundries = new Rect(12f, 7f, 80f, 80f);
				break;
			}
			case 36:
			case 78:
			{
				if (!PlayerItems.IsMineral((ushort)objective.targetParam1))
				{
					rect.SetItemTexture((ushort)objective.targetParam1);
					rect.boundries = new Rect(12f, 20f, 80f, 55f);
				}
				else
				{
					rect.SetTexture("MineralsAvatars", StaticData.allTypes.get_Item((ushort)objective.targetParam1).assetName);
					rect.boundries = new Rect(12f, 7f, 80f, 80f);
				}
				break;
			}
			case 37:
			{
				rect.SetTexture("QuestObjectivesArt", "DefaultSpaceStation");
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			case 38:
			{
				rect.SetTexture("QuestObjectivesArt", "DefaultSkills");
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			case 39:
			{
				rect.SetTexture("Skills", StaticData.allTypes.get_Item((ushort)objective.targetParam1).assetName);
				rect.boundries = new Rect(24f, 19f, 56f, 56f);
				break;
			}
			case 40:
			case 41:
			{
				rect.SetTexture("QuestObjectivesArt", "DefaultPvpWithSkills");
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			case 42:
			case 43:
			{
				rect.SetTexture("QuestObjectivesArt", "DefaultPvp");
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			case 44:
			case 45:
			case 50:
			case 51:
			{
				rect.SetTexture("QuestObjectivesArt", "DefaultPveWithSkills");
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			case 46:
			case 47:
			case 52:
			case 53:
			{
				rect.SetTexture("QuestObjectivesArt", "DefaultPve");
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			case 48:
			case 49:
			case 54:
			case 55:
			{
				rect.SetTexture("QuestObjectivesArt", "DefaultPveWithCritical");
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			case 56:
			case 57:
			case 62:
			case 63:
			{
				rect.SetTexture("QuestObjectivesArt", "DefaultPveBossWithSkills");
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			case 58:
			case 59:
			case 64:
			case 65:
			{
				rect.SetTexture("QuestObjectivesArt", "DefaultPveBoss");
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			case 60:
			case 61:
			case 66:
			case 67:
			{
				rect.SetTexture("QuestObjectivesArt", "DefaultPveBossWithCritical");
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			case 68:
			case 69:
			{
				rect.SetTexture("QuestObjectivesArt", "DefaultCriticalStrike");
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			case 70:
			case 71:
			case 74:
			case 75:
			{
				rect.SetTexture("QuestObjectivesArt", "DefaultHeal");
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			case 72:
			case 73:
			{
				rect.SetTexture("QuestObjectivesArt", "DefaultPartyHeal");
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			case 76:
			{
				ushort typeCash = PlayerItems.TypeCash;
				switch (objective.targetParam1)
				{
					case 0:
					{
						typeCash = PlayerItems.TypeCash;
						break;
					}
					case 1:
					{
						typeCash = PlayerItems.TypeNova;
						break;
					}
					case 2:
					{
						typeCash = PlayerItems.TypeEquilibrium;
						break;
					}
					case 3:
					{
						typeCash = PlayerItems.TypeUltralibrium;
						break;
					}
				}
				rect.SetItemTexture(typeCash);
				rect.boundries = new Rect(12f, 20f, 80f, 55f);
				break;
			}
			case 77:
			{
				rect.SetItemTexture(PlayerItems.TypeCash);
				rect.boundries = new Rect(12f, 20f, 80f, 55f);
				break;
			}
			case 79:
			{
				rect.SetTexture("Targeting", string.Concat(Enumerable.First<LevelMap>(Enumerable.Where<LevelMap>(StaticData.allGalaxies, new Func<LevelMap, bool>(variable, (LevelMap t) => t.galaxyKey == this.objective.targetGalaxyKey))).minimapAssetName, "_avatar"));
				rect.boundries = new Rect(12f, 7f, 80f, 80f);
				break;
			}
			case 80:
			{
				rect.SetTexture("QuestObjectivesArt", "DefaultSpeedBoost");
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			case 81:
			{
				if (objective.targetParam1 == 0)
				{
					rect.SetTexture("QuestObjectivesArt", "DefaultBuyShip");
					rect.boundries = new Rect(20f, 15f, 64f, 64f);
				}
				else
				{
					rect.SetTexture("ShipsAvatars", Enumerable.First<ShipsTypeNet>(Enumerable.Where<ShipsTypeNet>(StaticData.shipTypes, new Func<ShipsTypeNet, bool>(variable, (ShipsTypeNet t) => t.id == this.objective.targetParam1))).assetName);
					rect.boundries = new Rect(12f, 20f, 80f, 55f);
				}
				break;
			}
			case 82:
			{
				string str = "DefaultBuyItems";
				switch (objective.targetParam1)
				{
					case 1:
					{
						str = "ItemCategoryWeapons";
						break;
					}
					case 2:
					{
						str = "ItemCategoryCorpus";
						break;
					}
					case 3:
					{
						str = "ItemCategoryShield";
						break;
					}
					case 4:
					{
						str = "ItemCategoryEngine";
						break;
					}
					case 5:
					{
						str = "ItemCategoryExtras";
						break;
					}
				}
				rect.SetTexture("QuestObjectivesArt", str);
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			case 83:
			{
				string str1 = "DefaultGambleItems";
				switch (objective.targetParam1)
				{
					case 1:
					{
						str1 = "ItemCategoryWeapons";
						break;
					}
					case 2:
					{
						str1 = "ItemCategoryCorpus";
						break;
					}
					case 3:
					{
						str1 = "ItemCategoryShield";
						break;
					}
					case 4:
					{
						str1 = "ItemCategoryEngine";
						break;
					}
					case 5:
					{
						str1 = "ItemCategoryExtras";
						break;
					}
				}
				rect.SetTexture("QuestObjectivesArt", str1);
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			case 84:
			{
				rect.SetTexture("QuestObjectivesArt", "DefaultItemSell");
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			case 85:
			{
				rect.SetTexture("QuestObjectivesArt", "DefaultWeaponUpgrade");
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			case 86:
			{
				rect.SetTexture("QuestObjectivesArt", "DefaultShipUpgrade");
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			case 87:
			{
				rect.SetTexture("QuestObjectivesArt", "DefaultUpgrade");
				rect.boundries = new Rect(20f, 15f, 64f, 64f);
				break;
			}
			default:
			{
				rect.SetTexture("QuestInfoWindow", "objectiveLocked");
				rect.boundries = new Rect(12f, 7f, 80f, 80f);
				break;
			}
		}
		guiWindow.AddGuiElement(rect);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(103f, 9f, 267f, 19f),
			text = StaticData.Translate("key_notification_window_objective_done"),
			Font = GuiLabel.FontBold,
			FontSize = 14,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 4
		};
		guiWindow.AddGuiElement(guiLabel);
		string empty = string.Empty;
		empty = (!objective.haveCustomText ? objective.GetObjectiveDescription() : string.Format(StaticData.Translate(string.Format("key_quest_objective_{0}_custom_description", objective.id)), objective.targetAmount));
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(103f, 38f, 267f, 36f),
			text = empty,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			Alignment = 4
		};
		guiWindow.AddGuiElement(guiLabel1);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("FrameworkGUI", "empty");
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.eventHandlerParam.customData = notify;
		guiButtonFixed.Clicked = onClick;
		guiButtonFixed.boundries = new Rect(0f, 0f, guiWindow.boundries.get_width(), guiWindow.boundries.get_height());
		guiWindow.AddGuiElement(guiButtonFixed);
		return guiWindow;
	}

	public static GuiWindow CreateSystemNotification(string title, string info, string assetName, Action<EventHandlerParam> onClick, Notification notify)
	{
		GuiWindow guiWindow = new GuiWindow();
		guiWindow.SetBackgroundTexture("FrameworkGUI", "notification_frame");
		guiWindow.PutToHorizontalCenter();
		guiWindow.boundries.set_y((float)Screen.get_height());
		guiWindow.zOrder = 230;
		guiWindow.isHidden = false;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("FrameworkGUI", "item_frame");
		guiTexture.X = -1f;
		guiTexture.Y = 4f;
		guiWindow.AddGuiElement(guiTexture);
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("WarScreenWindow", "day-1");
		rect.boundries = new Rect(15f, 10f, 72f, 72f);
		guiWindow.AddGuiElement(rect);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(105f, 9f, 265f, 20f),
			TextColor = GuiNewStyleBar.redColor,
			Alignment = 4,
			FontSize = 14,
			Font = GuiLabel.FontBold,
			text = title
		};
		guiWindow.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(105f, 38f, 265f, 36f),
			TextColor = GuiNewStyleBar.redColor,
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			text = info
		};
		guiWindow.AddGuiElement(guiLabel1);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("FrameworkGUI", "empty");
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.eventHandlerParam.customData = notify;
		guiButtonFixed.Clicked = onClick;
		guiButtonFixed.boundries = new Rect(0f, 0f, guiWindow.boundries.get_width(), guiWindow.boundries.get_height());
		guiWindow.AddGuiElement(guiButtonFixed);
		return guiWindow;
	}

	public static GuiWindow CreateTransformerNotification(ushort rewardType, int rewardAmount, Action<EventHandlerParam> onClick, Notification notify)
	{
		PlayerItemTypesData item = StaticData.allTypes.get_Item(rewardType);
		GuiWindow guiWindow = new GuiWindow();
		guiWindow.SetBackgroundTexture("FrameworkGUI", "notification_frame");
		guiWindow.PutToHorizontalCenter();
		guiWindow.boundries.set_y((float)Screen.get_height());
		guiWindow.zOrder = 230;
		guiWindow.isHidden = false;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("FrameworkGUI", "item_frame");
		guiTexture.X = -1f;
		guiTexture.Y = 4f;
		guiWindow.AddGuiElement(guiTexture);
		GuiTexture rect = new GuiTexture();
		if (item.itemType == PlayerItems.TypeWarCommendation)
		{
			rect.SetTexture("WarScreenWindow", "warCommendationNotification");
			rect.boundries = new Rect(13f, 17f, 75f, 60f);
		}
		else if (!PlayerItems.IsMineral(item.itemType))
		{
			rect.SetItemTexture(item.itemType);
			rect.boundries = new Rect(13f, 22f, 75f, 51f);
		}
		else
		{
			rect.SetTexture("MineralsAvatars", item.assetName);
			rect.boundries = new Rect(15f, 10f, 72f, 72f);
		}
		guiWindow.AddGuiElement(rect);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(105f, 9f, 265f, 20f),
			Alignment = 4,
			FontSize = 14,
			Font = GuiLabel.FontBold,
			text = StaticData.Translate("key_notification_window_lbl_reward")
		};
		guiWindow.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(105f, 38f, 265f, 36f),
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			text = string.Format(StaticData.Translate("key_transformer_notification_reward"), rewardAmount, StaticData.Translate(item.uiName))
		};
		guiWindow.AddGuiElement(guiLabel1);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("FrameworkGUI", "empty");
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.eventHandlerParam.customData = notify;
		guiButtonFixed.Clicked = onClick;
		guiButtonFixed.boundries = new Rect(0f, 0f, guiWindow.boundries.get_width(), guiWindow.boundries.get_height());
		guiWindow.AddGuiElement(guiButtonFixed);
		return guiWindow;
	}

	public static GuiWindow StartBalloonNotification(PopupType popupType, int position, float timeToLive, string textToDisplay, Texture2D iconToDisplay = null, int destinationOffset = 0, Action<EventHandlerParam> onClickAction = null, EventHandlerParam onClickParam = null)
	{
		Rect rect;
		NotificationWindow.<StartBalloonNotification>c__AnonStorey53 variable = null;
		Vector2 _zero = Vector2.get_zero();
		GuiWindow guiWindow = new GuiWindow()
		{
			zOrder = 240
		};
		guiWindow.SetBackgroundTexture("MainScreenWindow", "balloonNotification");
		guiWindow.isHidden = false;
		switch (popupType)
		{
			case PopupType.ShowFromLeft:
			{
				position = position - guiWindow.backgroundTexture.get_height() / 2;
				guiWindow.boundries = new Rect((float)(-guiWindow.backgroundTexture.get_width()), (float)position, (float)guiWindow.backgroundTexture.get_width(), (float)guiWindow.backgroundTexture.get_height());
				_zero = new Vector2((float)(0 + destinationOffset), (float)position);
				break;
			}
			case PopupType.ShowFromRight:
			{
				position = position - guiWindow.backgroundTexture.get_height() / 2;
				guiWindow.boundries = new Rect((float)(Screen.get_width() + guiWindow.backgroundTexture.get_width()), (float)position, (float)guiWindow.backgroundTexture.get_width(), (float)guiWindow.backgroundTexture.get_height());
				_zero = new Vector2((float)(Screen.get_width() - guiWindow.backgroundTexture.get_width() + destinationOffset), (float)position);
				break;
			}
			case PopupType.ShowFromTop:
			{
				position = position - guiWindow.backgroundTexture.get_width() / 2;
				guiWindow.boundries = new Rect((float)position, (float)(-guiWindow.backgroundTexture.get_height()), (float)guiWindow.backgroundTexture.get_width(), (float)guiWindow.backgroundTexture.get_height());
				_zero = new Vector2((float)position, (float)(0 + destinationOffset));
				break;
			}
			case PopupType.ShowFromBottom:
			{
				position = position - guiWindow.backgroundTexture.get_width() / 2;
				guiWindow.boundries = new Rect((float)position, (float)(Screen.get_height() + guiWindow.backgroundTexture.get_height()), (float)guiWindow.backgroundTexture.get_width(), (float)guiWindow.backgroundTexture.get_height());
				_zero = new Vector2((float)position, (float)(Screen.get_height() - guiWindow.backgroundTexture.get_height() + destinationOffset));
				break;
			}
		}
		if (iconToDisplay == null)
		{
			rect = new Rect(10f, 5f, guiWindow.boundries.get_width() - 10f, guiWindow.boundries.get_height() - 10f);
		}
		else
		{
			GuiTexture guiTexture = new GuiTexture()
			{
				boundries = new Rect(8f, guiWindow.boundries.get_height() / 2f - 15f, 20f, 30f)
			};
			guiTexture.SetTextureKeepSize(iconToDisplay);
			guiWindow.AddGuiElement(guiTexture);
			rect = new Rect(35f, 5f, guiWindow.boundries.get_width() - 50f, guiWindow.boundries.get_height() - 10f);
		}
		GuiLabel guiLabel = new GuiLabel()
		{
			text = textToDisplay,
			boundries = rect,
			WordWrap = true,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 3
		};
		guiWindow.AddGuiElement(guiLabel);
		guiWindow.customOnGUIAction = new Action(variable, () => {
			this.timeToLive = this.timeToLive - Time.get_deltaTime();
			if (this.timeToLive <= 0f)
			{
				AndromedaGui.gui.RemoveWindow(this.ballonNotfWnd.handler);
				NotificationWindow.notificationsQueue.Remove(this.textToDisplay);
				if (NotificationWindow.notificationsQueue.get_Count() > 0)
				{
					NotificationWindow.StartPvPNotification(Enumerable.First<string>(NotificationWindow.notificationsQueue));
				}
			}
		});
		if (onClickAction == null)
		{
			onClickAction = new Action<EventHandlerParam>(variable, (EventHandlerParam EventHandlerParam) => {
				AndromedaGui.gui.RemoveWindow(this.ballonNotfWnd.handler);
				if (NetworkScript.player != null && NetworkScript.player.shipScript != null)
				{
					NetworkScript.player.shipScript.isGuiClosed = true;
				}
			});
		}
		GuiButton guiButton = new GuiButton()
		{
			boundries = new Rect(0f, 0f, guiWindow.boundries.get_width(), guiWindow.boundries.get_height()),
			Clicked = onClickAction,
			eventHandlerParam = (onClickParam == null ? guiButton.eventHandlerParam : onClickParam),
			Caption = string.Empty,
			Alignment = 4,
			isEnabled = true
		};
		guiWindow.AddGuiElement(guiButton);
		AndromedaGui.gui.AddWindow(guiWindow);
		guiWindow.timeHammerFx = 0.5f;
		guiWindow.amplitudeHammerShake = 20f;
		guiWindow.moveToShakeRatio = 0.6f;
		guiWindow.StartHammerEffect(_zero.x, _zero.y);
		return guiWindow;
	}

	public static void StartPvPNotification(string textToDisplay)
	{
		if (NotificationWindow.notificationsQueue.get_Count() >= 1 && !NotificationWindow.notificationsQueue.Contains(textToDisplay))
		{
			NotificationWindow.notificationsQueue.Add(textToDisplay);
			return;
		}
		Texture2D fromStaticSet = (Texture2D)playWebGame.assets.GetFromStaticSet("MainScreenWindow", "pvpIcon");
		NotificationWindow.StartBalloonNotification(PopupType.ShowFromRight, Screen.get_height() - 208, 5f, textToDisplay, fromStaticSet, -24, null, null);
		if (!NotificationWindow.notificationsQueue.Contains(textToDisplay))
		{
			NotificationWindow.notificationsQueue.Add(textToDisplay);
		}
	}
}