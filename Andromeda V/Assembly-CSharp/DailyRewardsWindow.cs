using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DailyRewardsWindow : GuiWindow
{
	public static bool isAlreadyShown;

	public static byte lastDayCnt;

	static DailyRewardsWindow()
	{
	}

	public DailyRewardsWindow()
	{
	}

	public override void Create()
	{
		if (NetworkScript.player == null || NetworkScript.player.playerBelongings == null)
		{
			return;
		}
		base.SetBackgroundTexture("DailyRewardsGui", "windowFrame");
		base.PutToCenter();
		this.zOrder = 210;
		this.isHidden = false;
		int num = NetworkScript.player.playerBelongings.receivedDailyRewards + 1;
		IList<PlayerPendingAward> values = NetworkScript.player.playerBelongings.playerAwards.get_Values();
		if (DailyRewardsWindow.<>f__am$cache2 == null)
		{
			DailyRewardsWindow.<>f__am$cache2 = new Func<PlayerPendingAward, bool>(null, (PlayerPendingAward t) => t.isDaily);
		}
		PlayerPendingAward playerPendingAward = Enumerable.FirstOrDefault<PlayerPendingAward>(Enumerable.Where<PlayerPendingAward>(values, DailyRewardsWindow.<>f__am$cache2));
		if (playerPendingAward == null)
		{
			return;
		}
		DailyRewardsWindow.isAlreadyShown = true;
		DailyRewardsWindow.lastDayCnt = NetworkScript.player.playerBelongings.receivedDailyRewards;
		GuiTexture guiTexture = new GuiTexture()
		{
			boundries = new Rect(41f, 27f, 650f, 100f)
		};
		guiTexture.SetTextureKeepSize("DailyRewardsGui", string.Format("day_{0}", (num >= 7 ? 7 : num)));
		base.AddGuiElement(guiTexture);
		for (int i = 0; i < 7; i++)
		{
			GuiTexture guiTexture1 = new GuiTexture()
			{
				boundries = new Rect((float)(64 + i * 90), 45f, 65f, 45f)
			};
			switch (i)
			{
				case 0:
				{
					guiTexture1.SetTextureKeepSize("Shop", "Cash");
					break;
				}
				case 1:
				{
					guiTexture1.SetTextureKeepSize("AmmosAvatars", "AmmoFusionCells");
					break;
				}
				case 2:
				{
					guiTexture1.SetTextureKeepSize("Shop", "Cash");
					break;
				}
				case 3:
				{
					guiTexture1.SetTextureKeepSize("AmmosAvatars", "AmmoColdCells");
					break;
				}
				case 4:
				{
					guiTexture1.SetTextureKeepSize("AmmosAvatars", "AmmoSulfurCells");
					break;
				}
				case 5:
				{
					guiTexture1.SetTextureKeepSize("Shop", "Ultralibrium");
					break;
				}
				default:
				{
					guiTexture1.SetTextureKeepSize("Shop", "resEquilibrium");
					break;
				}
			}
			base.AddGuiElement(guiTexture1);
			GuiLabel guiLabel = new GuiLabel()
			{
				boundries = new Rect((float)(64 + i * 90), 98f, 70f, 14f),
				text = string.Format(StaticData.Translate("key_daily_reward_day"), i + 1)
			};
			if (num > 7 && i == 6)
			{
				guiLabel.text = string.Format(StaticData.Translate("key_daily_reward_day"), num);
			}
			guiLabel.FontSize = 14;
			guiLabel.Font = GuiLabel.FontBold;
			guiLabel.Alignment = 4;
			base.AddGuiElement(guiLabel);
		}
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(254f, 168f, 420f, 70f),
			text = StaticData.Translate("key_daily_reward_come_back_for_more"),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			Alignment = 4
		};
		base.AddGuiElement(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(254f, 248f, 420f, 25f),
			text = StaticData.Translate("key_daily_reward_will_receive"),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			Alignment = 4
		};
		base.AddGuiElement(guiLabel2);
		GuiTexture guiTexture2 = new GuiTexture()
		{
			boundries = new Rect(400f, 294f, 65f, 45f)
		};
		guiTexture2.SetItemTextureKeepSize(playerPendingAward.itemType);
		base.AddGuiElement(guiTexture2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(400f, 294f, 200f, 20f),
			text = playerPendingAward.amount.ToString("#,##0"),
			FontSize = 32,
			TextColor = GuiNewStyleBar.blueColor,
			Font = GuiLabel.FontBold
		};
		guiLabel3.boundries.set_width(guiLabel3.TextWidth);
		base.AddGuiElement(guiLabel3);
		float _width = 442f - guiTexture2.boundries.get_width() - guiLabel3.boundries.get_width();
		guiTexture2.boundries.set_x(238f + _width / 2f);
		guiLabel3.boundries.set_x(313f + _width / 2f);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed()
		{
			boundries = new Rect(309f, 371f, 314f, 56f)
		};
		guiButtonFixed.SetTexture("DailyRewardsGui", "btnGotIt");
		guiButtonFixed.Caption = StaticData.Translate("key_daily_reward_got_it_btn");
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, DailyRewardsWindow.OnProceedClicked);
		guiButtonFixed.Alignment = 4;
		guiButtonFixed.textColorNormal = Color.get_black();
		guiButtonFixed.textColorHover = Color.get_black();
		base.AddGuiElement(guiButtonFixed);
	}

	private void OnProceedClicked(EventHandlerParam prm)
	{
		this.OpenMenu(17);
		if (NetworkScript.player.shipScript != null)
		{
			NetworkScript.player.shipScript.isGuiClosed = true;
		}
	}

	private void OpenMenu(byte index)
	{
		AndromedaGui.mainWnd.OnWindowBtnClicked(new EventHandlerParam()
		{
			customData = index
		});
		AndromedaGui.mainWnd.OnCloseWindowCallback = null;
	}
}