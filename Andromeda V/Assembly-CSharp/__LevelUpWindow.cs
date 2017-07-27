using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class __LevelUpWindow : GuiWindow
{
	private GuiTexture mainTipImg;

	private GuiTexture congratsBorder;

	private GuiTexture firstSeparator;

	private GuiTexture secondSeparator;

	private GuiButtonResizeable btnGotIt;

	private GuiLabel congratsLblMain;

	private GuiLabel congratsLbl1;

	private GuiLabel congratsLbl2;

	private GuiLabel congratsValue1;

	private GuiLabel congratsValue2;

	private GuiLabel tipLbl;

	private GuiLabel tipValue;

	private GuiLabel actionLbl;

	private GuiLabel actionValue;

	public __LevelUpWindow()
	{
	}

	public override void Create()
	{
		if (NetworkScript.player == null || NetworkScript.player.playerBelongings == null)
		{
			return;
		}
		__LevelUpWindow.Infomation[] infomationArray = __LevelUpWindow.Infomation.levelInformation;
		if (__LevelUpWindow.<>f__am$cacheE == null)
		{
			__LevelUpWindow.<>f__am$cacheE = new Func<__LevelUpWindow.Infomation, bool>(null, (__LevelUpWindow.Infomation l) => l.level == NetworkScript.player.playerBelongings.playerLevel);
		}
		__LevelUpWindow.Infomation infomation = Enumerable.FirstOrDefault<__LevelUpWindow.Infomation>(Enumerable.Where<__LevelUpWindow.Infomation>((IEnumerable<__LevelUpWindow.Infomation>)infomationArray, __LevelUpWindow.<>f__am$cacheE));
		if (infomation == null)
		{
			return;
		}
		base.SetBackgroundTexture("GUI", "level_up_window");
		base.PutToCenter();
		this.zOrder = 210;
		this.isHidden = false;
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(34f, 45f, 400f, 52f),
			Font = GuiLabel.FontBold,
			Alignment = 7,
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 24,
			text = StaticData.Translate("key_new_level_up_screen_congrats")
		};
		base.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(188f, 155f, 80f, 80f),
			Font = GuiLabel.FontBold,
			Alignment = 4,
			TextColor = GuiNewStyleBar.blackColorTransperant,
			FontSize = 42,
			text = infomation.level.ToString()
		};
		base.AddGuiElement(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(34f, 280f, 400f, 28f),
			Font = GuiLabel.FontBold,
			Alignment = 4,
			FontSize = 18,
			text = StaticData.Translate("key_notification_window_lbl_reward")
		};
		base.AddGuiElement(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(463f, 280f, 400f, 28f),
			Font = GuiLabel.FontBold,
			Alignment = 4,
			FontSize = 18,
			text = StaticData.Translate(infomation.titleLbl)
		};
		base.AddGuiElement(guiLabel3);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			boundries = new Rect(463f, 326f, 400f, 125f),
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 14,
			text = StaticData.Translate(infomation.descriptionLbl)
		};
		base.AddGuiElement(guiLabel4);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("GUI", string.Format("level_up_{0}", infomation.level));
		guiTexture.X = 471f;
		guiTexture.Y = 64f;
		base.AddGuiElement(guiTexture);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.boundries.set_x(140f);
		guiButtonResizeable.boundries.set_y(470f);
		guiButtonResizeable.boundries.set_width(175f);
		guiButtonResizeable.Caption = StaticData.Translate("key_level_up_screen_got_it").ToUpper();
		guiButtonResizeable.FontSize = 14;
		guiButtonResizeable.Alignment = 4;
		guiButtonResizeable.SetSmallBlueTexture();
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, __LevelUpWindow.OnOkBtnClicked);
		base.AddGuiElement(guiButtonResizeable);
		GuiButtonResizeable action = new GuiButtonResizeable();
		action.boundries.set_x(585f);
		action.boundries.set_y(470f);
		action.boundries.set_width(175f);
		action.Caption = StaticData.Translate(infomation.btnLbl);
		action.FontSize = 14;
		action.Alignment = 4;
		action.SetSmallBlueTexture();
		action.eventHandlerParam.customData = infomation.level;
		action.Clicked = new Action<EventHandlerParam>(this, __LevelUpWindow.OnProceedClicked);
		base.AddGuiElement(action);
		float textWidth = 14f;
		float single = 350f;
		float single1 = 5f;
		LevelsInfo item = StaticData.levelsType.get_Item(infomation.level);
		List<GuiElement> list = new List<GuiElement>();
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("GUI", "icon_notification_skillPoint");
		rect.boundries = new Rect(textWidth, single, 32f, 32f);
		base.AddGuiElement(rect);
		list.Add(rect);
		textWidth = textWidth + (32f + single1);
		GuiLabel guiLabel5 = new GuiLabel()
		{
			boundries = new Rect(textWidth, single, 100f, 32f),
			Alignment = 3,
			FontSize = 12,
			TextColor = GuiNewStyleBar.orangeColor,
			Font = GuiLabel.FontBold,
			text = string.Format("{0} {1}", StaticData.Translate("key_skills_tree_skill_points"), 1)
		};
		guiLabel5.boundries.set_width(guiLabel5.TextWidth);
		base.AddGuiElement(guiLabel5);
		list.Add(guiLabel5);
		textWidth = textWidth + (guiLabel5.TextWidth + single1);
		if (item.novaReward != 0)
		{
			GuiTexture guiTexture1 = new GuiTexture();
			guiTexture1.SetTexture("GUI", "icon_notification_nova");
			guiTexture1.boundries = new Rect(textWidth, single, 32f, 32f);
			base.AddGuiElement(guiTexture1);
			list.Add(guiTexture1);
			textWidth = textWidth + (32f + single1);
			GuiLabel guiLabel6 = new GuiLabel()
			{
				boundries = new Rect(textWidth, single, 100f, 32f),
				Alignment = 3,
				FontSize = 12,
				TextColor = GuiNewStyleBar.orangeColor,
				Font = GuiLabel.FontBold,
				text = string.Format(StaticData.Translate("key_nova_shop_exchange_nova"), item.novaReward)
			};
			guiLabel6.boundries.set_width(guiLabel6.TextWidth);
			base.AddGuiElement(guiLabel6);
			list.Add(guiLabel6);
			textWidth = textWidth + (guiLabel6.TextWidth + single1);
		}
		if (item.cashReward != 0)
		{
			GuiTexture rect1 = new GuiTexture();
			rect1.SetTexture("GUI", "icon_notification_cash");
			rect1.boundries = new Rect(textWidth, single, 32f, 32f);
			base.AddGuiElement(rect1);
			list.Add(rect1);
			textWidth = textWidth + (32f + single1);
			GuiLabel guiLabel7 = new GuiLabel()
			{
				boundries = new Rect(textWidth, single, 100f, 32f),
				Alignment = 3,
				FontSize = 12,
				TextColor = GuiNewStyleBar.orangeColor,
				Font = GuiLabel.FontBold,
				text = string.Format(StaticData.Translate("key_nova_shop_exchange_cash"), item.cashReward)
			};
			guiLabel7.boundries.set_width(guiLabel7.TextWidth);
			base.AddGuiElement(guiLabel7);
			list.Add(guiLabel7);
			textWidth = textWidth + (guiLabel7.TextWidth + single1);
		}
		if (item.neuronReward != 0)
		{
			GuiTexture guiTexture2 = new GuiTexture();
			guiTexture2.SetTexture("GUI", "icon_notification_neuronModule");
			guiTexture2.boundries = new Rect(textWidth, single, 32f, 32f);
			base.AddGuiElement(guiTexture2);
			list.Add(guiTexture2);
			textWidth = textWidth + (32f + single1);
			GuiLabel guiLabel8 = new GuiLabel()
			{
				boundries = new Rect(textWidth, single, 100f, 32f),
				Alignment = 3,
				FontSize = 12,
				TextColor = GuiNewStyleBar.orangeColor,
				Font = GuiLabel.FontBold,
				text = string.Format("{0} {1}", StaticData.Translate("key_skills_tree_neuron_modules"), item.neuronReward)
			};
			guiLabel8.boundries.set_width(guiLabel8.TextWidth);
			base.AddGuiElement(guiLabel8);
			list.Add(guiLabel8);
			textWidth = textWidth + (guiLabel8.TextWidth + single1);
		}
		if (item.itemTypeReward != 0)
		{
			GuiTexture rect2 = new GuiTexture();
			rect2.SetTexture("GUI", "icon_notification_booster");
			rect2.boundries = new Rect(textWidth, single, 32f, 32f);
			base.AddGuiElement(rect2);
			list.Add(rect2);
			textWidth = textWidth + (32f + single1);
			if (PlayerItems.IsBooster(item.itemTypeReward))
			{
				GuiLabel guiLabel9 = new GuiLabel()
				{
					boundries = new Rect(textWidth, single, 100f, 32f),
					Alignment = 3,
					FontSize = 12,
					TextColor = GuiNewStyleBar.orangeColor,
					Font = GuiLabel.FontBold,
					text = StaticData.Translate(StaticData.allTypes.get_Item(item.itemTypeReward).uiName)
				};
				guiLabel9.boundries.set_width(guiLabel9.TextWidth);
				base.AddGuiElement(guiLabel9);
				list.Add(guiLabel9);
				textWidth = textWidth + (guiLabel9.TextWidth + single1);
			}
		}
		float single2 = (450f - textWidth) / 2f;
		foreach (GuiElement guiElement in list)
		{
			GuiElement x = guiElement;
			x.X = x.X + single2;
		}
	}

	private void GoToBase()
	{
		Minimap.OnAutoPilotClick(null);
		AndromedaGui.mainWnd.OnCloseWindowCallback = null;
		AndromedaGui.mainWnd.CloseActiveWindow();
	}

	private void OnOkBtnClicked(object prm)
	{
		if (NetworkScript.player.shipScript != null)
		{
			NetworkScript.player.shipScript.isGuiClosed = true;
		}
		AndromedaGui.mainWnd.CloseActiveWindow();
	}

	private void OnProceedClicked(EventHandlerParam prm)
	{
		int num = (int)prm.customData;
		switch (num)
		{
			case 2:
			{
				this.OpenMenu(2);
				break;
			}
			case 3:
			{
				this.GoToBase();
				break;
			}
			case 4:
			{
				this.OpenMenu(0);
				break;
			}
			case 5:
			{
				this.OpenMenu(17);
				break;
			}
			case 6:
			{
				this.OpenMenu(11);
				break;
			}
			case 7:
			{
				this.OpenMenu(7);
				break;
			}
			case 8:
			{
				this.OpenMenu(15);
				break;
			}
			case 9:
			{
				this.OpenMenu(18);
				break;
			}
			case 10:
			{
				this.OpenMenu(7);
				break;
			}
			case 15:
			{
				this.OpenMenu(7);
				break;
			}
			case 20:
			{
				this.OpenDailyQuests();
				break;
			}
			default:
			{
				if (num == 30)
				{
					this.OpenMenu(22);
					break;
				}
				else
				{
					AndromedaGui.mainWnd.CloseActiveWindow();
					break;
				}
			}
		}
		if (NetworkScript.player.shipScript != null)
		{
			NetworkScript.player.shipScript.isGuiClosed = true;
		}
	}

	private void OpenDailyQuests()
	{
	}

	private void OpenMenu(byte index)
	{
		AndromedaGui.mainWnd.OnWindowBtnClicked(new EventHandlerParam()
		{
			customData = index
		});
		AndromedaGui.mainWnd.OnCloseWindowCallback = null;
	}

	public class Infomation
	{
		public int level;

		public string titleLbl;

		public string descriptionLbl;

		public string btnLbl;

		public static __LevelUpWindow.Infomation[] levelInformation;

		static Infomation()
		{
			__LevelUpWindow.Infomation[] infomationArray = new __LevelUpWindow.Infomation[12];
			__LevelUpWindow.Infomation infomation = new __LevelUpWindow.Infomation()
			{
				level = 2,
				titleLbl = "key_new_level_up_screen_level_skills_title",
				descriptionLbl = "key_new_level_up_screen_level_skills_description",
				btnLbl = "key_new_level_up_screen_level_skills_btn"
			};
			infomationArray[0] = infomation;
			infomation = new __LevelUpWindow.Infomation()
			{
				level = 3,
				titleLbl = "key_new_level_up_screen_level_base_title",
				descriptionLbl = "key_new_level_up_screen_level_base_description",
				btnLbl = "key_new_level_up_screen_level_base_btn"
			};
			infomationArray[1] = infomation;
			infomation = new __LevelUpWindow.Infomation()
			{
				level = 4,
				titleLbl = "key_new_level_up_screen_level_fusion_title",
				descriptionLbl = "key_new_level_up_screen_level_fusion_description",
				btnLbl = "key_new_level_up_screen_level_fusion_btn"
			};
			infomationArray[2] = infomation;
			infomation = new __LevelUpWindow.Infomation()
			{
				level = 5,
				titleLbl = "key_new_level_up_screen_level_profile_title",
				descriptionLbl = "key_new_level_up_screen_level_profile_description",
				btnLbl = "key_new_level_up_screen_level_profile_btn"
			};
			infomationArray[3] = infomation;
			infomation = new __LevelUpWindow.Infomation()
			{
				level = 6,
				titleLbl = "key_new_level_up_screen_level_boosters_title",
				descriptionLbl = "key_new_level_up_screen_level_boosters_description",
				btnLbl = "key_new_level_up_screen_level_boosters_btn"
			};
			infomationArray[4] = infomation;
			infomation = new __LevelUpWindow.Infomation()
			{
				level = 7,
				titleLbl = "key_new_level_up_screen_level_universe_map_title",
				descriptionLbl = "key_new_level_up_screen_level_universe_map_description",
				btnLbl = "key_new_level_up_screen_level_universe_map_btn"
			};
			infomationArray[5] = infomation;
			infomation = new __LevelUpWindow.Infomation()
			{
				level = 8,
				titleLbl = "key_new_level_up_screen_level_pvp_title",
				descriptionLbl = "key_new_level_up_screen_level_pvp_description",
				btnLbl = "key_new_level_up_screen_level_pvp_btn"
			};
			infomationArray[6] = infomation;
			infomation = new __LevelUpWindow.Infomation()
			{
				level = 9,
				titleLbl = "key_new_level_up_screen_level_guild_title",
				descriptionLbl = "key_new_level_up_screen_level_guild_description",
				btnLbl = "key_new_level_up_screen_level_guild_btn"
			};
			infomationArray[7] = infomation;
			infomation = new __LevelUpWindow.Infomation()
			{
				level = 10,
				titleLbl = "key_new_level_up_screen_level_instances_title",
				descriptionLbl = "key_new_level_up_screen_level_instances_description",
				btnLbl = "key_new_level_up_screen_level_instances_btn"
			};
			infomationArray[8] = infomation;
			infomation = new __LevelUpWindow.Infomation()
			{
				level = 15,
				titleLbl = "key_new_level_up_screen_level_extraction_point_title",
				descriptionLbl = "key_new_level_up_screen_level_extraction_point_description",
				btnLbl = "key_new_level_up_screen_level_extraction_point_btn"
			};
			infomationArray[9] = infomation;
			infomation = new __LevelUpWindow.Infomation()
			{
				level = 20,
				titleLbl = "key_new_level_up_screen_level_daily_quests_title",
				descriptionLbl = "key_new_level_up_screen_level_daily_quests_description",
				btnLbl = "key_new_level_up_screen_level_daily_quests_btn"
			};
			infomationArray[10] = infomation;
			infomation = new __LevelUpWindow.Infomation()
			{
				level = 30,
				titleLbl = "key_new_level_up_screen_level_transformer_title",
				descriptionLbl = "key_new_level_up_screen_level_transformer_description",
				btnLbl = "key_new_level_up_screen_level_transformer_btn"
			};
			infomationArray[11] = infomation;
			__LevelUpWindow.Infomation.levelInformation = infomationArray;
		}

		public Infomation()
		{
		}
	}
}