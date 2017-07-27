using System;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsWindow : GuiWindow
{
	private GuiScrollingContainer powerUpsScroller;

	private PowerUpCategory selectedCategory;

	public static NpcObjectPhysics npc;

	static PowerUpsWindow()
	{
	}

	public PowerUpsWindow()
	{
	}

	private void BuyWithNova(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void PowerUpsWindow::BuyWithNova(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void BuyWithNova(EventHandlerParam)
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

	private void BuyWithViral(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void PowerUpsWindow::BuyWithViral(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void BuyWithViral(EventHandlerParam)
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

	public override void Create()
	{
		if (NetworkScript.player == null || NetworkScript.player.playerBelongings == null)
		{
			return;
		}
		float single = 0f;
		float single1 = 0f;
		base.SetBackgroundTexture("PowerUpsWindow", "frame");
		base.PutToCenter();
		this.zOrder = 210;
		this.isHidden = false;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("PowerUpsWindow", "top");
		guiTexture.X = single + 191f;
		guiTexture.Y = single1 + 91f;
		base.AddGuiElement(guiTexture);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(single + 282f, single1 + 26f, 460f, 38f),
			text = string.Format(StaticData.Translate("key_powerup_window_title"), StaticData.Translate(PowerUpsWindow.npc.npcName)),
			FontSize = 22,
			Alignment = 4,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor
		};
		base.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(single + 362f, single1 + 104f, 420f, 50f),
			text = StaticData.Translate("key_powerup_information"),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel1);
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("QuestTrackerAvatars", PowerUpsWindow.npc.assetName);
		rect.boundries = new Rect(single + 228f, single1 + 111f, 100f, 100f);
		base.AddGuiElement(rect);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("PowerUpsWindow", "tabDamage");
		guiButtonFixed.SetTextureClicked("PowerUpsWindow", "tabDamageHvr");
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.X = single + 365f;
		guiButtonFixed.Y = single1 + 169f;
		guiButtonFixed.groupId = 32;
		guiButtonFixed.behaviourKeepClicked = true;
		guiButtonFixed.eventHandlerParam.customData = (PowerUpCategory)0;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, PowerUpsWindow.OnCategoryClick);
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_powerup_tab_damage"),
			customData2 = guiButtonFixed
		};
		guiButtonFixed.tooltipWindowParam = eventHandlerParam;
		guiButtonFixed.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(guiButtonFixed);
		GuiButtonFixed empty = new GuiButtonFixed();
		empty.SetTexture("PowerUpsWindow", "tabCorpus");
		empty.SetTextureClicked("PowerUpsWindow", "tabCorpusHvr");
		empty.Caption = string.Empty;
		empty.X = single + 365f + 75f;
		empty.Y = single1 + 169f;
		empty.groupId = 32;
		empty.behaviourKeepClicked = true;
		empty.eventHandlerParam.customData = (PowerUpCategory)1;
		empty.Clicked = new Action<EventHandlerParam>(this, PowerUpsWindow.OnCategoryClick);
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_powerup_tab_corpus"),
			customData2 = empty
		};
		empty.tooltipWindowParam = eventHandlerParam;
		empty.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(empty);
		GuiButtonFixed action = new GuiButtonFixed();
		action.SetTexture("PowerUpsWindow", "tabShield");
		action.SetTextureClicked("PowerUpsWindow", "tabShieldHvr");
		action.Caption = string.Empty;
		action.X = single + 365f + 150f;
		action.Y = single1 + 169f;
		action.groupId = 32;
		action.behaviourKeepClicked = true;
		action.eventHandlerParam.customData = (PowerUpCategory)2;
		action.Clicked = new Action<EventHandlerParam>(this, PowerUpsWindow.OnCategoryClick);
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_powerup_tab_shield"),
			customData2 = action
		};
		action.tooltipWindowParam = eventHandlerParam;
		action.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(action);
		GuiButtonFixed drawTooltipWindow = new GuiButtonFixed();
		drawTooltipWindow.SetTexture("PowerUpsWindow", "tabShieldPower");
		drawTooltipWindow.SetTextureClicked("PowerUpsWindow", "tabShieldPowerHvr");
		drawTooltipWindow.Caption = string.Empty;
		drawTooltipWindow.X = single + 365f + 225f;
		drawTooltipWindow.Y = single1 + 169f;
		drawTooltipWindow.groupId = 32;
		drawTooltipWindow.behaviourKeepClicked = true;
		drawTooltipWindow.eventHandlerParam.customData = (PowerUpCategory)3;
		drawTooltipWindow.Clicked = new Action<EventHandlerParam>(this, PowerUpsWindow.OnCategoryClick);
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_powerup_tab_shieldpower"),
			customData2 = drawTooltipWindow
		};
		drawTooltipWindow.tooltipWindowParam = eventHandlerParam;
		drawTooltipWindow.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(drawTooltipWindow);
		GuiButtonFixed guiButtonFixed1 = new GuiButtonFixed();
		guiButtonFixed1.SetTexture("PowerUpsWindow", "tabTarget");
		guiButtonFixed1.SetTextureClicked("PowerUpsWindow", "tabTargetHvr");
		guiButtonFixed1.Caption = string.Empty;
		guiButtonFixed1.X = single + 365f + 300f;
		guiButtonFixed1.Y = single1 + 169f;
		guiButtonFixed1.groupId = 32;
		guiButtonFixed1.behaviourKeepClicked = true;
		guiButtonFixed1.eventHandlerParam.customData = (PowerUpCategory)4;
		guiButtonFixed1.Clicked = new Action<EventHandlerParam>(this, PowerUpsWindow.OnCategoryClick);
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_powerup_tab_targeting"),
			customData2 = guiButtonFixed1
		};
		guiButtonFixed1.tooltipWindowParam = eventHandlerParam;
		guiButtonFixed1.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(guiButtonFixed1);
		GuiButtonFixed empty1 = new GuiButtonFixed();
		empty1.SetTexture("PowerUpsWindow", "tabAvoidance");
		empty1.SetTextureClicked("PowerUpsWindow", "tabAvoidanceHvr");
		empty1.Caption = string.Empty;
		empty1.X = single + 365f + 375f;
		empty1.Y = single1 + 169f;
		empty1.groupId = 32;
		empty1.behaviourKeepClicked = true;
		empty1.eventHandlerParam.customData = (PowerUpCategory)5;
		empty1.Clicked = new Action<EventHandlerParam>(this, PowerUpsWindow.OnCategoryClick);
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_powerup_tab_avoidance"),
			customData2 = empty1
		};
		empty1.tooltipWindowParam = eventHandlerParam;
		empty1.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(empty1);
		this.powerUpsScroller = new GuiScrollingContainer(single + 210f, single1 + 243f, 605f, 240f, 1, this);
		this.powerUpsScroller.SetArrowStep(65f);
		base.AddGuiElement(this.powerUpsScroller);
		guiButtonFixed.IsClicked = this.selectedCategory == 0;
		empty.IsClicked = this.selectedCategory == 1;
		action.IsClicked = this.selectedCategory == 2;
		drawTooltipWindow.IsClicked = this.selectedCategory == 3;
		guiButtonFixed1.IsClicked = this.selectedCategory == 4;
		empty1.IsClicked = this.selectedCategory == 5;
	}

	private void DrawPowerUps(PowerUpInfo[] powerUps, GuiScrollingContainer scroller)
	{
		float single = 82f;
		int length = (int)powerUps.Length;
		for (int i = 0; i < length; i++)
		{
			GuiTexture guiTexture = new GuiTexture();
			guiTexture.SetTexture("PowerUpsWindow", "separator");
			guiTexture.X = 0f;
			guiTexture.Y = single * (float)i;
			scroller.AddContent(guiTexture);
			GuiTexture guiTexture1 = new GuiTexture();
			guiTexture1.SetTexture("PowerUpsWindow", powerUps[i].assetName);
			guiTexture1.X = 10f;
			guiTexture1.Y = 16f + single * (float)i;
			scroller.AddContent(guiTexture1);
			GuiLabel guiLabel = new GuiLabel()
			{
				boundries = new Rect(80f, 12f + single * (float)i, 375f, 16f),
				text = string.Format(StaticData.Translate(powerUps[i].name), NetworkScript.player.playerBelongings.GetPowerUpEffectValue(powerUps[i].powerUpType)),
				FontSize = 12,
				Alignment = 3,
				Font = GuiLabel.FontBold,
				TextColor = GuiNewStyleBar.blueColor
			};
			scroller.AddContent(guiLabel);
			GuiLabel guiLabel1 = new GuiLabel()
			{
				boundries = new Rect(80f, 36f + single * (float)i, 375f, 16f),
				text = string.Format(StaticData.Translate("key_powerup_duration_in_hours"), powerUps[i].durationInHours),
				FontSize = 12,
				Alignment = 3,
				Font = GuiLabel.FontBold,
				TextColor = GuiNewStyleBar.blueColor
			};
			scroller.AddContent(guiLabel1);
			GuiLabel _white = new GuiLabel()
			{
				boundries = new Rect(80f, 56f + single * (float)i, 375f, 16f),
				text = StaticData.Translate("key_nova_shop_item_status_deactivated"),
				FontSize = 12,
				Alignment = 3,
				Font = GuiLabel.FontBold,
				TextColor = GuiNewStyleBar.blueColor
			};
			scroller.AddContent(_white);
			GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
			guiButtonResizeable.SetSmallOrangeTexture();
			guiButtonResizeable.Width = 120f;
			guiButtonResizeable.X = 453f;
			guiButtonResizeable.Y = 10f + single * (float)i;
			guiButtonResizeable.FontSize = 12;
			guiButtonResizeable._marginLeft = 30;
			guiButtonResizeable.Alignment = 3;
			guiButtonResizeable.Caption = string.Format(StaticData.Translate("key_powerup_buy_btn_price"), powerUps[i].priceInNova);
			guiButtonResizeable.eventHandlerParam.customData = powerUps[i].powerUpType;
			guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, PowerUpsWindow.BuyWithNova);
			scroller.AddContent(guiButtonResizeable);
			GuiTexture guiTexture2 = new GuiTexture();
			guiTexture2.SetTexture("NewGUI", "icon_white_nova");
			guiTexture2.X = 460f;
			guiTexture2.Y = 10f + single * (float)i;
			scroller.AddContent(guiTexture2);
			if (powerUps[i].priceInViral != 0)
			{
				GuiButtonResizeable action = new GuiButtonResizeable();
				action.SetSmallBlueTexture();
				action.Width = 120f;
				action.X = 453f;
				action.Y = 48f + single * (float)i;
				action.FontSize = 12;
				action._marginLeft = 30;
				action.Alignment = 3;
				action.Caption = string.Format(StaticData.Translate("key_powerup_buy_btn_price"), powerUps[i].priceInViral);
				action.eventHandlerParam.customData = powerUps[i].powerUpType;
				action.Clicked = new Action<EventHandlerParam>(this, PowerUpsWindow.BuyWithViral);
				scroller.AddContent(action);
				GuiTexture guiTexture3 = new GuiTexture();
				guiTexture3.SetTexture("NewGUI", "icon_white_equilibrium");
				guiTexture3.X = 460f;
				guiTexture3.Y = 48f + single * (float)i;
				scroller.AddContent(guiTexture3);
			}
			TimeSpan powerUpExpireTime = NetworkScript.player.playerBelongings.GetPowerUpExpireTime(powerUps[i].powerUpType) - StaticData.now;
			long totalSeconds = (long)powerUpExpireTime.get_TotalSeconds();
			if (totalSeconds > (long)0)
			{
				_white.TextColor = Color.get_white();
				if (totalSeconds <= (long)60)
				{
					_white.text = string.Format(StaticData.Translate("key_profile_screen_playtime_value"), 0, 0, 1);
				}
				else
				{
					_white.text = string.Format(StaticData.Translate("key_profile_screen_playtime_value"), totalSeconds / (long)86400, totalSeconds / (long)3600 % (long)24, totalSeconds / (long)60 % (long)60);
				}
			}
		}
	}

	private void OnCategoryClick(EventHandlerParam prm)
	{
		if (prm == null)
		{
			return;
		}
		this.powerUpsScroller.Claer();
		this.selectedCategory = (int)prm.customData;
		switch (this.selectedCategory)
		{
			case 0:
			{
				this.DrawPowerUps(StaticData.powerUps.get_Item(0), this.powerUpsScroller);
				break;
			}
			case 1:
			{
				this.DrawPowerUps(StaticData.powerUps.get_Item(1), this.powerUpsScroller);
				break;
			}
			case 2:
			{
				this.DrawPowerUps(StaticData.powerUps.get_Item(2), this.powerUpsScroller);
				break;
			}
			case 3:
			{
				this.DrawPowerUps(StaticData.powerUps.get_Item(3), this.powerUpsScroller);
				break;
			}
			case 4:
			{
				this.DrawPowerUps(StaticData.powerUps.get_Item(4), this.powerUpsScroller);
				break;
			}
			case 5:
			{
				this.DrawPowerUps(StaticData.powerUps.get_Item(5), this.powerUpsScroller);
				break;
			}
		}
	}

	public void Populate()
	{
		float scrollTumbCenter = 0f;
		if (this.powerUpsScroller != null && this.powerUpsScroller.IsScrolerAvalable)
		{
			scrollTumbCenter = this.powerUpsScroller.ScrollTumbCenter;
		}
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = this.selectedCategory
		};
		this.OnCategoryClick(eventHandlerParam);
		if (scrollTumbCenter != 0f)
		{
			this.powerUpsScroller.MooveToCenter(scrollTumbCenter);
		}
	}
}