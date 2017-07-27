using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TransferableObjects;
using UnityEngine;

public class SendGiftsWindow : GuiWindow
{
	public static string receiverName;

	public static int receiverLevel;

	public static bool isOnReceiverSelectScreen;

	private SendGiftsWindow.SelectedGiftsTab selectedTab;

	private GuiScrollingContainer giftsScroller;

	private PowerUpCategory selectedCategory;

	private List<GuiElement> forDelete = new List<GuiElement>();

	private GuiLabel receiver;

	private GuiButtonFixed btnChangereceiver;

	private GuiTextBox newEntryUserName;

	private GuiLabel addingToListResponseLbl;

	private ushort selectedGift;

	private GuiWindow dialogWindow;

	private GuiTextBox giftTitleBox;

	private bool sendGiftAnonymusly;

	static SendGiftsWindow()
	{
		SendGiftsWindow.receiverName = string.Empty;
		SendGiftsWindow.receiverLevel = 0;
		SendGiftsWindow.isOnReceiverSelectScreen = false;
	}

	public SendGiftsWindow()
	{
	}

	private void AddToFriedns(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void SendGiftsWindow::AddToFriedns(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void AddToFriedns(EventHandlerParam)
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

	private void Changereceiver(EventHandlerParam prm)
	{
		SendGiftsWindow.isOnReceiverSelectScreen = true;
		this.btnChangereceiver.isEnabled = false;
		playWebGame.udp.ExecuteCommand(172, null);
		this.ClearContent();
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(355f, 162f, 455f, 70f),
			text = StaticData.Translate("key_send_gifts_select_receiver_description"),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 4
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("PowerUpsWindow", "separator");
		guiTexture.boundries = new Rect(210f, 244f, 574f, 1f);
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(206f, 250f, 40f, 30f),
			text = "#",
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel1);
		this.forDelete.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(guiLabel1.boundries.get_xMax() + 10f, 250f, 350f, 30f),
			text = StaticData.Translate("key_send_gifts_select_receiver_player").ToUpper(),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel2);
		this.forDelete.Add(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(guiLabel2.boundries.get_xMax() + 10f, 250f, 170f, 30f),
			text = StaticData.Translate("key_send_gifts_select_receiver_option").ToUpper(),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel3);
		this.forDelete.Add(guiLabel3);
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("PowerUpsWindow", "separator");
		rect.boundries = new Rect(210f, 284f, 574f, 1f);
		base.AddGuiElement(rect);
		this.forDelete.Add(rect);
		this.giftsScroller = new GuiScrollingContainer(210f, 284f, 605f, 240f, 1, this);
		this.giftsScroller.SetArrowStep(40f);
		base.AddGuiElement(this.giftsScroller);
		this.forDelete.Add(this.giftsScroller);
		this.DrawFreinds(Enumerable.ToArray<PlayerProfile>(NetworkScript.player.myFriends.get_Values()), this.giftsScroller);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("PowerUpsWindow", "separator");
		guiTexture1.boundries = new Rect(210f, 524f, 574f, 1f);
		base.AddGuiElement(guiTexture1);
		this.forDelete.Add(guiTexture1);
		this.newEntryUserName = new GuiTextBox()
		{
			boundries = new Rect(220f, 535f, 200f, 30f),
			Alignment = 3,
			FontSize = 14,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.newEntryUserName);
		this.forDelete.Add(this.newEntryUserName);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.boundries.set_x(430f);
		guiButtonResizeable.boundries.set_y(540f);
		guiButtonResizeable.boundries.set_width(150f);
		guiButtonResizeable.FontSize = 12;
		guiButtonResizeable.Caption = StaticData.Translate("key_profile_screen_add_friend_list").ToUpper();
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = this.newEntryUserName.text,
			customData2 = true
		};
		guiButtonResizeable.eventHandlerParam = eventHandlerParam;
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, SendGiftsWindow.AddToFriedns);
		guiButtonResizeable.SetSmallBlueTexture();
		guiButtonResizeable.Alignment = 1;
		guiButtonResizeable.MarginTop = 5;
		base.AddGuiElement(guiButtonResizeable);
		this.forDelete.Add(guiButtonResizeable);
		this.addingToListResponseLbl = new GuiLabel()
		{
			boundries = new Rect(590f, 535f, 200f, 40f),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			TextColor = GuiNewStyleBar.redColor,
			text = string.Empty
		};
		base.AddGuiElement(this.addingToListResponseLbl);
		this.forDelete.Add(this.addingToListResponseLbl);
	}

	private void ClearContent()
	{
		if (this.giftsScroller != null)
		{
			this.giftsScroller.Claer();
		}
		foreach (GuiElement guiElement in this.forDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.forDelete.Clear();
		this.giftsScroller = null;
	}

	public override void Create()
	{
		if (NetworkScript.player == null || NetworkScript.player.playerBelongings == null)
		{
			return;
		}
		base.SetBackgroundTexture("SendGiftsWindow", "frame");
		base.PutToCenter();
		this.zOrder = 210;
		this.isHidden = false;
		AndromedaGui.mainWnd.OnCloseWindowCallback = new Action(this, SendGiftsWindow.OnWindowClose);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("PowerUpsWindow", "top");
		guiTexture.X = 191f;
		guiTexture.Y = 91f;
		base.AddGuiElement(guiTexture);
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("QuestTrackerAvatars", "NPC_James");
		rect.boundries = new Rect(228f, 111f, 100f, 100f);
		base.AddGuiElement(rect);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(282f, 26f, 460f, 38f),
			text = StaticData.Translate("key_send_gifts_window_title"),
			FontSize = 22,
			Alignment = 4,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.aquamarineColor
		};
		base.AddGuiElement(guiLabel);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("FrameworkGUI", "buttonMidBlue");
		guiButtonFixed.Caption = StaticData.Translate("key_send_gifts_tab_free");
		guiButtonFixed.X = 350f;
		guiButtonFixed.Y = 105f;
		guiButtonFixed.groupId = 30;
		guiButtonFixed.behaviourKeepClicked = true;
		guiButtonFixed.eventHandlerParam.customData = SendGiftsWindow.SelectedGiftsTab.Free;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, SendGiftsWindow.OnTabChange);
		Color color = GuiNewStyleBar.blackColorTransperant60;
		Color color1 = color;
		guiButtonFixed.textColorClick = color;
		Color color2 = color1;
		color1 = color2;
		guiButtonFixed.textColorHover = color2;
		guiButtonFixed.textColorNormal = color1;
		guiButtonFixed.MarginTop = -2;
		guiButtonFixed.Alignment = 4;
		base.AddGuiElement(guiButtonFixed);
		GuiButtonFixed action = new GuiButtonFixed();
		action.SetTexture("FrameworkGUI", "buttonMidBlue");
		action.Caption = StaticData.Translate("key_send_gifts_tab_powerups");
		action.X = 500f;
		action.Y = 105f;
		action.groupId = 30;
		action.behaviourKeepClicked = true;
		action.eventHandlerParam.customData = SendGiftsWindow.SelectedGiftsTab.PowerUps;
		action.Clicked = new Action<EventHandlerParam>(this, SendGiftsWindow.OnTabChange);
		Color color3 = GuiNewStyleBar.blackColorTransperant60;
		color1 = color3;
		action.textColorClick = color3;
		Color color4 = color1;
		color1 = color4;
		action.textColorHover = color4;
		action.textColorNormal = color1;
		action.MarginTop = -2;
		action.Alignment = 4;
		base.AddGuiElement(action);
		GuiButtonFixed guiButtonFixed1 = new GuiButtonFixed();
		guiButtonFixed1.SetTexture("FrameworkGUI", "buttonMidBlue");
		guiButtonFixed1.Caption = StaticData.Translate("key_send_gifts_tab_boosters");
		guiButtonFixed1.X = 650f;
		guiButtonFixed1.Y = 105f;
		guiButtonFixed1.groupId = 30;
		guiButtonFixed1.behaviourKeepClicked = true;
		guiButtonFixed1.eventHandlerParam.customData = SendGiftsWindow.SelectedGiftsTab.Boosters;
		guiButtonFixed1.Clicked = new Action<EventHandlerParam>(this, SendGiftsWindow.OnTabChange);
		Color color5 = GuiNewStyleBar.blackColorTransperant60;
		color1 = color5;
		guiButtonFixed1.textColorClick = color5;
		Color color6 = color1;
		color1 = color6;
		guiButtonFixed1.textColorHover = color6;
		guiButtonFixed1.textColorNormal = color1;
		guiButtonFixed1.MarginTop = -2;
		guiButtonFixed1.Alignment = 4;
		base.AddGuiElement(guiButtonFixed1);
		this.receiver = new GuiLabel()
		{
			boundries = new Rect(300f, 597f, 270f, 28f),
			text = string.Format(StaticData.Translate("key_send_gifts_receiver"), (!string.IsNullOrEmpty(SendGiftsWindow.receiverName) ? string.Concat(SendGiftsWindow.receiverName, string.Format(" [{0}]", SendGiftsWindow.receiverLevel)) : StaticData.Translate("key_send_gifts_receiver_none"))),
			FontSize = 12,
			Alignment = 3,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.receiver);
		this.btnChangereceiver = new GuiButtonFixed();
		this.btnChangereceiver.SetTexture("FrameworkGUI", "buttonRightBlue");
		this.btnChangereceiver.Caption = StaticData.Translate("key_send_gifts_change_receiver");
		this.btnChangereceiver.X = 586f;
		this.btnChangereceiver.Y = 593f;
		this.btnChangereceiver.Clicked = new Action<EventHandlerParam>(this, SendGiftsWindow.Changereceiver);
		GuiButtonFixed guiButtonFixed2 = this.btnChangereceiver;
		GuiButtonFixed guiButtonFixed3 = this.btnChangereceiver;
		GuiButtonFixed guiButtonFixed4 = this.btnChangereceiver;
		GuiButtonFixed guiButtonFixed5 = this.btnChangereceiver;
		Color color7 = GuiNewStyleBar.blackColorTransperant60;
		color1 = color7;
		guiButtonFixed5.textColorDisabled = color7;
		Color color8 = color1;
		color1 = color8;
		guiButtonFixed4.textColorClick = color8;
		Color color9 = color1;
		color1 = color9;
		guiButtonFixed3.textColorHover = color9;
		guiButtonFixed2.textColorNormal = color1;
		this.btnChangereceiver.MarginTop = -2;
		this.btnChangereceiver.Alignment = 4;
		base.AddGuiElement(this.btnChangereceiver);
		guiButtonFixed.IsClicked = this.selectedTab == SendGiftsWindow.SelectedGiftsTab.Free;
		action.IsClicked = this.selectedTab == SendGiftsWindow.SelectedGiftsTab.PowerUps;
		guiButtonFixed1.IsClicked = this.selectedTab == SendGiftsWindow.SelectedGiftsTab.Boosters;
	}

	private void DrawBooster(ushort boosterType, float offsetY, GuiScrollingContainer scroller)
	{
		PlayerItemTypesData item = StaticData.allTypes.get_Item(boosterType);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("SendGiftsWindow", "giftBackground");
		guiTexture.X = 10f;
		guiTexture.Y = 10f + offsetY;
		scroller.AddContent(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture()
		{
			X = 10f,
			Y = 21f + offsetY
		};
		guiTexture1.SetItemTexture(item.itemType);
		guiTexture1.SetSize(60f, 40f);
		scroller.AddContent(guiTexture1);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(80f, 12f + offsetY, 375f, 16f),
			text = StaticData.Translate(item.uiName).ToUpper(),
			FontSize = 12,
			Alignment = 3,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor
		};
		scroller.AddContent(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(80f, guiLabel.Y + 20f, 375f, 16f),
			text = StaticData.Translate(item.description),
			FontSize = 12,
			Alignment = 3,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor
		};
		scroller.AddContent(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(80f, guiLabel1.Y + 20f, 375f, 16f),
			text = string.Format(StaticData.Translate("key_powerup_duration_in_hours"), 72),
			FontSize = 12,
			Alignment = 3,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor
		};
		scroller.AddContent(guiLabel2);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.SetSmallOrangeTexture();
		guiButtonResizeable.Width = 120f;
		guiButtonResizeable.X = 453f;
		guiButtonResizeable.Y = 10f + offsetY;
		guiButtonResizeable.FontSize = 12;
		guiButtonResizeable._marginLeft = 30;
		guiButtonResizeable.Alignment = 3;
		guiButtonResizeable.Caption = string.Format(StaticData.Translate("key_send_gift_btn_send_with_price"), item.priceNova);
		guiButtonResizeable.eventHandlerParam.customData = item.itemType;
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, SendGiftsWindow.OnSendButtonCliecked);
		guiButtonResizeable.isEnabled = NetworkScript.player.playerBelongings.playerItems.get_Nova() >= (long)item.priceNova;
		scroller.AddContent(guiButtonResizeable);
		GuiTexture guiTexture2 = new GuiTexture();
		guiTexture2.SetTexture("NewGUI", "icon_white_nova");
		guiTexture2.X = 460f;
		guiTexture2.Y = 10f + offsetY;
		scroller.AddContent(guiTexture2);
		GuiTexture guiTexture3 = new GuiTexture();
		guiTexture3.SetTexture("PowerUpsWindow", "separator");
		guiTexture3.X = 0f;
		guiTexture3.Y = offsetY + 82f;
		guiTexture3.boundries.set_height(1f);
		scroller.AddContent(guiTexture3);
	}

	private void DrawFreeGifts(GuiScrollingContainer scroller, bool isAvailabble = false)
	{
		float single = 82f;
		for (int i = 0; i < (int)StaticData.freeGifts.Length; i++)
		{
			PlayerItemTypesData item = StaticData.allTypes.get_Item(StaticData.freeGifts[i].itemType);
			GuiTexture guiTexture = new GuiTexture();
			guiTexture.SetTexture("SendGiftsWindow", "giftBackground");
			guiTexture.X = 10f;
			guiTexture.Y = 10f + single * (float)i;
			scroller.AddContent(guiTexture);
			GuiTexture guiTexture1 = new GuiTexture()
			{
				X = 10f,
				Y = 21f + single * (float)i
			};
			guiTexture1.SetItemTexture(item.itemType);
			guiTexture1.SetSize(60f, 40f);
			scroller.AddContent(guiTexture1);
			GuiLabel guiLabel = new GuiLabel()
			{
				boundries = new Rect(80f, 12f + single * (float)i, 375f, 16f),
				text = StaticData.Translate(item.uiName),
				FontSize = 12,
				Alignment = 3,
				Font = GuiLabel.FontBold,
				TextColor = GuiNewStyleBar.blueColor
			};
			scroller.AddContent(guiLabel);
			GuiLabel guiLabel1 = new GuiLabel()
			{
				boundries = new Rect(80f, guiLabel.Y + 20f, 375f, 16f),
				text = StaticData.freeGifts[i].amount.ToString("##,##0"),
				FontSize = 12,
				Alignment = 3,
				Font = GuiLabel.FontBold,
				TextColor = GuiNewStyleBar.blueColor
			};
			scroller.AddContent(guiLabel1);
			GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
			guiButtonResizeable.SetSmallBlueTexture();
			guiButtonResizeable.Width = 120f;
			guiButtonResizeable.X = 453f;
			guiButtonResizeable.Y = 10f + single * (float)i;
			guiButtonResizeable.FontSize = 12;
			guiButtonResizeable.Alignment = 4;
			guiButtonResizeable.Caption = StaticData.Translate("key_send_gifts_btn_send");
			guiButtonResizeable.eventHandlerParam.customData = item.itemType;
			guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, SendGiftsWindow.OnSendButtonCliecked);
			guiButtonResizeable.isEnabled = isAvailabble;
			scroller.AddContent(guiButtonResizeable);
			GuiTexture guiTexture2 = new GuiTexture();
			guiTexture2.SetTexture("PowerUpsWindow", "separator");
			guiTexture2.X = 0f;
			guiTexture2.Y = 82f + single * (float)i;
			guiTexture2.boundries.set_height(1f);
			scroller.AddContent(guiTexture2);
		}
	}

	private void DrawFreinds(PlayerProfile[] receivers, GuiScrollingContainer scroller)
	{
		int length = (int)receivers.Length;
		float single = 40f;
		for (int i = 0; i < length; i++)
		{
			GuiLabel guiLabel = new GuiLabel()
			{
				boundries = new Rect(0f, 5f + (float)i * single, 40f, 30f),
				text = (i + 1).ToString(),
				FontSize = 14,
				Font = GuiLabel.FontBold,
				TextColor = GuiNewStyleBar.blueColor,
				Alignment = 3
			};
			scroller.AddContent(guiLabel);
			GuiTexture guiTexture = new GuiTexture();
			guiTexture.SetTexture("FrameworkGUI", (!receivers[i].isOnline ? "iconOffline" : "iconOnline"));
			guiTexture.X = guiLabel.boundries.get_xMax() + 10f;
			guiTexture.Y = 12f + (float)i * single;
			scroller.AddContent(guiTexture);
			GuiLabel guiLabel1 = new GuiLabel()
			{
				boundries = new Rect(guiLabel.boundries.get_xMax() + 10f + 18f, 5f + (float)i * single, 350f, 30f),
				text = string.Format("{0} [{1}]", receivers[i].userName, receivers[i].level),
				FontSize = 14,
				Font = GuiLabel.FontBold,
				TextColor = GuiNewStyleBar.blueColor,
				Alignment = 3
			};
			scroller.AddContent(guiLabel1);
			GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
			guiButtonResizeable.SetSmallBlueTexture();
			guiButtonResizeable.Width = 145f;
			guiButtonResizeable.X = guiLabel1.boundries.get_xMax() + 10f;
			guiButtonResizeable.Y = 7f + (float)i * single;
			guiButtonResizeable.FontSize = 12;
			guiButtonResizeable.Alignment = 4;
			guiButtonResizeable.Caption = StaticData.Translate("key_send_gifts_select_receiver_select");
			guiButtonResizeable.eventHandlerParam.customData = receivers[i].userName;
			guiButtonResizeable.eventHandlerParam.customData2 = receivers[i].level;
			guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, SendGiftsWindow.OnreceiverSelect);
			guiButtonResizeable.isEnabled = receivers[i].level >= 10;
			scroller.AddContent(guiButtonResizeable);
			GuiTexture rect = new GuiTexture();
			rect.SetTexture("PowerUpsWindow", "separator");
			rect.boundries = new Rect(0f, 40f + (float)i * single, 574f, 1f);
			scroller.AddContent(rect);
		}
	}

	private void DrawPowerUps(PowerUpInfo[] powerUps, GuiScrollingContainer scroller)
	{
		float single = 82f;
		int length = (int)powerUps.Length;
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("SendGiftsWindow", "packageDeal");
		guiButtonFixed.X = 5f;
		guiButtonFixed.Y = 13f;
		guiButtonFixed.FontSize = 14;
		guiButtonFixed.MarginTop = -2;
		guiButtonFixed.Alignment = 4;
		guiButtonFixed.textColorNormal = GuiNewStyleBar.goldColor;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, SendGiftsWindow.OnSendButtonCliecked);
		scroller.AddContent(guiButtonFixed);
		int item = 0;
		switch (this.selectedCategory)
		{
			case 0:
			{
				guiButtonFixed.eventHandlerParam.customData = PlayerItems.TypePowerUpDamagePackageDeal;
				item = StaticData.allTypes.get_Item(PlayerItems.TypePowerUpDamagePackageDeal).priceNova;
				break;
			}
			case 1:
			{
				guiButtonFixed.eventHandlerParam.customData = PlayerItems.TypePowerUpCorpusPackageDeal;
				item = StaticData.allTypes.get_Item(PlayerItems.TypePowerUpCorpusPackageDeal).priceNova;
				break;
			}
			case 2:
			{
				guiButtonFixed.eventHandlerParam.customData = PlayerItems.TypePowerUpShieldPackageDeal;
				item = StaticData.allTypes.get_Item(PlayerItems.TypePowerUpShieldPackageDeal).priceNova;
				break;
			}
			case 3:
			{
				guiButtonFixed.eventHandlerParam.customData = PlayerItems.TypePowerUpShieldPowerPackageDeal;
				item = StaticData.allTypes.get_Item(PlayerItems.TypePowerUpShieldPowerPackageDeal).priceNova;
				break;
			}
			case 4:
			{
				guiButtonFixed.eventHandlerParam.customData = PlayerItems.TypePowerUpTargetingPackageDeal;
				item = StaticData.allTypes.get_Item(PlayerItems.TypePowerUpTargetingPackageDeal).priceNova;
				break;
			}
			case 5:
			{
				guiButtonFixed.eventHandlerParam.customData = PlayerItems.TypePowerUpAvoidancePackageDeal;
				item = StaticData.allTypes.get_Item(PlayerItems.TypePowerUpAvoidancePackageDeal).priceNova;
				break;
			}
		}
		guiButtonFixed.Caption = string.Format(StaticData.Translate("key_send_gift_package_deal"), 20, item).ToUpper();
		guiButtonFixed.isEnabled = NetworkScript.player.playerBelongings.playerItems.get_Nova() >= (long)item;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("PowerUpsWindow", "separator");
		guiTexture.X = 0f;
		guiTexture.Y = single;
		guiTexture.boundries.set_height(1f);
		scroller.AddContent(guiTexture);
		for (int i = 0; i < length; i++)
		{
			GuiTexture guiTexture1 = new GuiTexture();
			guiTexture1.SetTexture("PowerUpsWindow", "separator");
			guiTexture1.X = 0f;
			guiTexture1.Y = single * (float)(i + 2);
			guiTexture1.boundries.set_height(1f);
			scroller.AddContent(guiTexture1);
			GuiTexture guiTexture2 = new GuiTexture();
			guiTexture2.SetTexture("PowerUpsWindow", powerUps[i].assetName);
			guiTexture2.X = 10f;
			guiTexture2.Y = 16f + single * (float)(i + 1);
			scroller.AddContent(guiTexture2);
			GuiLabel guiLabel = new GuiLabel()
			{
				boundries = new Rect(80f, 12f + single * (float)(i + 1), 375f, 16f),
				text = string.Format(StaticData.Translate(powerUps[i].name), NetworkScript.player.playerBelongings.GetPowerUpEffectValue(powerUps[i].powerUpType)),
				FontSize = 12,
				Alignment = 3,
				Font = GuiLabel.FontBold,
				TextColor = GuiNewStyleBar.blueColor
			};
			scroller.AddContent(guiLabel);
			GuiLabel guiLabel1 = new GuiLabel()
			{
				boundries = new Rect(80f, 36f + single * (float)(i + 1), 375f, 16f),
				text = string.Format(StaticData.Translate("key_powerup_duration_in_hours"), powerUps[i].durationInHours),
				FontSize = 12,
				Alignment = 3,
				Font = GuiLabel.FontBold,
				TextColor = GuiNewStyleBar.blueColor
			};
			scroller.AddContent(guiLabel1);
			GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
			guiButtonResizeable.SetSmallOrangeTexture();
			guiButtonResizeable.Width = 120f;
			guiButtonResizeable.X = 453f;
			guiButtonResizeable.Y = 10f + single * (float)(i + 1);
			guiButtonResizeable.FontSize = 12;
			guiButtonResizeable._marginLeft = 30;
			guiButtonResizeable.Alignment = 3;
			guiButtonResizeable.Caption = string.Format(StaticData.Translate("key_send_gift_btn_send_with_price"), powerUps[i].priceInNova);
			guiButtonResizeable.eventHandlerParam.customData = powerUps[i].powerUpType;
			guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, SendGiftsWindow.OnSendButtonCliecked);
			guiButtonResizeable.isEnabled = NetworkScript.player.playerBelongings.playerItems.get_Nova() >= (long)powerUps[i].priceInNova;
			scroller.AddContent(guiButtonResizeable);
			GuiTexture guiTexture3 = new GuiTexture();
			guiTexture3.SetTexture("NewGUI", "icon_white_nova");
			guiTexture3.X = 460f;
			guiTexture3.Y = 10f + single * (float)(i + 1);
			scroller.AddContent(guiTexture3);
		}
	}

	private void OnAnonymuslyClick(bool state)
	{
		this.sendGiftAnonymusly = state;
	}

	private void OnBoostersClicked()
	{
		float single = 82f;
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(355f, 162f, 455f, 70f),
			text = string.Format(StaticData.Translate("key_send_gifts_description"), 10),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 4
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		this.giftsScroller = new GuiScrollingContainer(210f, 244f, 605f, 328f, 1, this);
		this.giftsScroller.SetArrowStep(82f);
		base.AddGuiElement(this.giftsScroller);
		this.forDelete.Add(this.giftsScroller);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("PowerUpsWindow", "separator");
		guiTexture.X = 0f;
		guiTexture.Y = 0f;
		guiTexture.boundries.set_height(1f);
		this.giftsScroller.AddContent(guiTexture);
		int item = StaticData.allTypes.get_Item(PlayerItems.TypeBoosterPackageDeal).priceNova;
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("SendGiftsWindow", "packageDeal");
		guiButtonFixed.X = 5f;
		guiButtonFixed.Y = 13f;
		guiButtonFixed.FontSize = 14;
		guiButtonFixed.MarginTop = -2;
		guiButtonFixed.Alignment = 4;
		guiButtonFixed.textColorNormal = GuiNewStyleBar.goldColor;
		guiButtonFixed.Caption = string.Format(StaticData.Translate("key_send_gift_package_deal"), 20, item).ToUpper();
		guiButtonFixed.eventHandlerParam.customData = PlayerItems.TypeBoosterPackageDeal;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, SendGiftsWindow.OnSendButtonCliecked);
		guiButtonFixed.isEnabled = NetworkScript.player.playerBelongings.playerItems.get_Nova() >= (long)item;
		this.giftsScroller.AddContent(guiButtonFixed);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("PowerUpsWindow", "separator");
		guiTexture1.X = 0f;
		guiTexture1.Y = single;
		guiTexture1.boundries.set_height(1f);
		this.giftsScroller.AddContent(guiTexture1);
		this.DrawBooster(PlayerItems.TypeBoosterAutominerFor3Days, 82f, this.giftsScroller);
		this.DrawBooster(PlayerItems.TypeBoosterCargoFor3Days, 164f, this.giftsScroller);
		this.DrawBooster(PlayerItems.TypeBoosterDamageFor3Days, 246f, this.giftsScroller);
		this.DrawBooster(PlayerItems.TypeBoosterExperienceFor3Days, 328f, this.giftsScroller);
	}

	private void OnCategoryClick(EventHandlerParam prm)
	{
		if (prm == null)
		{
			return;
		}
		this.giftsScroller.Claer();
		this.selectedCategory = (int)prm.customData;
		switch (this.selectedCategory)
		{
			case 0:
			{
				this.DrawPowerUps(StaticData.powerUps.get_Item(0), this.giftsScroller);
				break;
			}
			case 1:
			{
				this.DrawPowerUps(StaticData.powerUps.get_Item(1), this.giftsScroller);
				break;
			}
			case 2:
			{
				this.DrawPowerUps(StaticData.powerUps.get_Item(2), this.giftsScroller);
				break;
			}
			case 3:
			{
				this.DrawPowerUps(StaticData.powerUps.get_Item(3), this.giftsScroller);
				break;
			}
			case 4:
			{
				this.DrawPowerUps(StaticData.powerUps.get_Item(4), this.giftsScroller);
				break;
			}
			case 5:
			{
				this.DrawPowerUps(StaticData.powerUps.get_Item(5), this.giftsScroller);
				break;
			}
		}
	}

	private void OnFreeGiftsClicked()
	{
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(355f, 162f, 455f, 70f),
			text = string.Format(StaticData.Translate("key_send_gifts_description"), 10),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 4
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("PowerUpsWindow", "separator");
		guiTexture.boundries = new Rect(210f, 244f, 574f, 1f);
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(210f, 245f, 575f, 60f),
			text = string.Format(StaticData.Translate("key_send_gifts_free_info"), 24),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel1);
		this.forDelete.Add(guiLabel1);
		TimeSpan timeSpan = NetworkScript.player.playerBelongings.nextFreeGiftTime - StaticData.now;
		if (timeSpan.get_TotalSeconds() > 1)
		{
			GuiLabel guiLabel2 = new GuiLabel()
			{
				boundries = new Rect(210f, 245f, 520f, 60f),
				text = StaticData.Translate("key_send_gifts_next_free_time"),
				FontSize = 12,
				Alignment = 5,
				Font = GuiLabel.FontBold,
				TextColor = GuiNewStyleBar.blueColor
			};
			base.AddGuiElement(guiLabel2);
			this.forDelete.Add(guiLabel2);
			GuiTimeTracker guiTimeTracker = new GuiTimeTracker((int)timeSpan.get_TotalSeconds(), this)
			{
				FontSize = 12,
				TextColor = GuiNewStyleBar.blueColor,
				Font = GuiLabel.FontBold,
				boundries = new Rect(730f, 245f, 55f, 60f),
				Alignment = 5
			};
			this.forDelete.Add(guiTimeTracker);
		}
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("PowerUpsWindow", "separator");
		rect.boundries = new Rect(210f, 310f, 574f, 1f);
		base.AddGuiElement(rect);
		this.forDelete.Add(rect);
		this.giftsScroller = new GuiScrollingContainer(210f, 310f, 605f, 328f, 1, this);
		this.giftsScroller.SetArrowStep(82f);
		base.AddGuiElement(this.giftsScroller);
		this.forDelete.Add(this.giftsScroller);
		this.DrawFreeGifts(this.giftsScroller, timeSpan.get_TotalSeconds() < 1);
	}

	private void OnPowerUpsClicked()
	{
		float single = 0f;
		float single1 = 0f;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("PowerUpsWindow", "separator");
		guiTexture.boundries = new Rect(210f, 244f, 574f, 1f);
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(single + 210f, single1 + 245f, 575f, 60f),
			text = string.Format(StaticData.Translate("key_send_gifts_description"), 10),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 4
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("PowerUpsWindow", "separator");
		rect.boundries = new Rect(210f, 326f, 574f, 1f);
		base.AddGuiElement(rect);
		this.forDelete.Add(rect);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("PowerUpsWindow", "tabDamage");
		guiButtonFixed.SetTextureClicked("PowerUpsWindow", "tabDamageHvr");
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.X = single + 365f;
		guiButtonFixed.Y = single1 + 169f;
		guiButtonFixed.groupId = 32;
		guiButtonFixed.behaviourKeepClicked = true;
		guiButtonFixed.eventHandlerParam.customData = (PowerUpCategory)0;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, SendGiftsWindow.OnCategoryClick);
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_powerup_tab_damage"),
			customData2 = guiButtonFixed
		};
		guiButtonFixed.tooltipWindowParam = eventHandlerParam;
		guiButtonFixed.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(guiButtonFixed);
		this.forDelete.Add(guiButtonFixed);
		GuiButtonFixed empty = new GuiButtonFixed();
		empty.SetTexture("PowerUpsWindow", "tabCorpus");
		empty.SetTextureClicked("PowerUpsWindow", "tabCorpusHvr");
		empty.Caption = string.Empty;
		empty.X = single + 365f + 75f;
		empty.Y = single1 + 169f;
		empty.groupId = 32;
		empty.behaviourKeepClicked = true;
		empty.eventHandlerParam.customData = (PowerUpCategory)1;
		empty.Clicked = new Action<EventHandlerParam>(this, SendGiftsWindow.OnCategoryClick);
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_powerup_tab_corpus"),
			customData2 = empty
		};
		empty.tooltipWindowParam = eventHandlerParam;
		empty.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(empty);
		this.forDelete.Add(empty);
		GuiButtonFixed action = new GuiButtonFixed();
		action.SetTexture("PowerUpsWindow", "tabShield");
		action.SetTextureClicked("PowerUpsWindow", "tabShieldHvr");
		action.Caption = string.Empty;
		action.X = single + 365f + 150f;
		action.Y = single1 + 169f;
		action.groupId = 32;
		action.behaviourKeepClicked = true;
		action.eventHandlerParam.customData = (PowerUpCategory)2;
		action.Clicked = new Action<EventHandlerParam>(this, SendGiftsWindow.OnCategoryClick);
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_powerup_tab_shield"),
			customData2 = action
		};
		action.tooltipWindowParam = eventHandlerParam;
		action.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(action);
		this.forDelete.Add(action);
		GuiButtonFixed drawTooltipWindow = new GuiButtonFixed();
		drawTooltipWindow.SetTexture("PowerUpsWindow", "tabShieldPower");
		drawTooltipWindow.SetTextureClicked("PowerUpsWindow", "tabShieldPowerHvr");
		drawTooltipWindow.Caption = string.Empty;
		drawTooltipWindow.X = single + 365f + 225f;
		drawTooltipWindow.Y = single1 + 169f;
		drawTooltipWindow.groupId = 32;
		drawTooltipWindow.behaviourKeepClicked = true;
		drawTooltipWindow.eventHandlerParam.customData = (PowerUpCategory)3;
		drawTooltipWindow.Clicked = new Action<EventHandlerParam>(this, SendGiftsWindow.OnCategoryClick);
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_powerup_tab_shieldpower"),
			customData2 = drawTooltipWindow
		};
		drawTooltipWindow.tooltipWindowParam = eventHandlerParam;
		drawTooltipWindow.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(drawTooltipWindow);
		this.forDelete.Add(drawTooltipWindow);
		GuiButtonFixed guiButtonFixed1 = new GuiButtonFixed();
		guiButtonFixed1.SetTexture("PowerUpsWindow", "tabTarget");
		guiButtonFixed1.SetTextureClicked("PowerUpsWindow", "tabTargetHvr");
		guiButtonFixed1.Caption = string.Empty;
		guiButtonFixed1.X = single + 365f + 300f;
		guiButtonFixed1.Y = single1 + 169f;
		guiButtonFixed1.groupId = 32;
		guiButtonFixed1.behaviourKeepClicked = true;
		guiButtonFixed1.eventHandlerParam.customData = (PowerUpCategory)4;
		guiButtonFixed1.Clicked = new Action<EventHandlerParam>(this, SendGiftsWindow.OnCategoryClick);
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_powerup_tab_targeting"),
			customData2 = guiButtonFixed1
		};
		guiButtonFixed1.tooltipWindowParam = eventHandlerParam;
		guiButtonFixed1.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(guiButtonFixed1);
		this.forDelete.Add(guiButtonFixed1);
		GuiButtonFixed empty1 = new GuiButtonFixed();
		empty1.SetTexture("PowerUpsWindow", "tabAvoidance");
		empty1.SetTextureClicked("PowerUpsWindow", "tabAvoidanceHvr");
		empty1.Caption = string.Empty;
		empty1.X = single + 365f + 375f;
		empty1.Y = single1 + 169f;
		empty1.groupId = 32;
		empty1.behaviourKeepClicked = true;
		empty1.eventHandlerParam.customData = (PowerUpCategory)5;
		empty1.Clicked = new Action<EventHandlerParam>(this, SendGiftsWindow.OnCategoryClick);
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_powerup_tab_avoidance"),
			customData2 = empty1
		};
		empty1.tooltipWindowParam = eventHandlerParam;
		empty1.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(empty1);
		this.forDelete.Add(empty1);
		this.giftsScroller = new GuiScrollingContainer(single + 210f, single1 + 326f, 605f, 247f, 1, this);
		this.giftsScroller.SetArrowStep(82f);
		base.AddGuiElement(this.giftsScroller);
		this.forDelete.Add(this.giftsScroller);
		guiButtonFixed.IsClicked = this.selectedCategory == 0;
		empty.IsClicked = this.selectedCategory == 1;
		action.IsClicked = this.selectedCategory == 2;
		drawTooltipWindow.IsClicked = this.selectedCategory == 3;
		guiButtonFixed1.IsClicked = this.selectedCategory == 4;
		empty1.IsClicked = this.selectedCategory == 5;
	}

	private void OnreceiverSelect(EventHandlerParam prm)
	{
		if (prm == null)
		{
			return;
		}
		SendGiftsWindow.receiverName = (string)prm.customData;
		SendGiftsWindow.receiverLevel = (int)prm.customData2;
		this.receiver.text = string.Format(StaticData.Translate("key_send_gifts_receiver"), string.Concat(SendGiftsWindow.receiverName, string.Format(" [{0}]", SendGiftsWindow.receiverLevel)));
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = this.selectedTab
		};
		this.OnTabChange(eventHandlerParam);
		if (this.selectedGift != 0)
		{
			this.ShowConfirm();
		}
	}

	private void OnSendButtonCliecked(EventHandlerParam prm)
	{
		this.selectedGift = (ushort)prm.customData;
		if (!string.IsNullOrEmpty(SendGiftsWindow.receiverName))
		{
			this.ShowConfirm();
		}
		else
		{
			this.Changereceiver(null);
		}
	}

	private void OnSendCancel(object prm)
	{
		AndromedaGui.gui.RemoveWindow(this.dialogWindow.handler);
		AndromedaGui.gui.activeToolTipId = -1;
		this.dialogWindow = null;
	}

	private void OnSendConfirm(object prm)
	{
		// 
		// Current member / type: System.Void SendGiftsWindow::OnSendConfirm(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnSendConfirm(System.Object)
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

	private void OnTabChange(EventHandlerParam prm)
	{
		if (prm == null)
		{
			return;
		}
		this.ClearContent();
		SendGiftsWindow.isOnReceiverSelectScreen = false;
		this.btnChangereceiver.isEnabled = true;
		this.selectedTab = (int)prm.customData;
		switch (this.selectedTab)
		{
			case SendGiftsWindow.SelectedGiftsTab.Free:
			{
				this.OnFreeGiftsClicked();
				break;
			}
			case SendGiftsWindow.SelectedGiftsTab.PowerUps:
			{
				this.OnPowerUpsClicked();
				break;
			}
			case SendGiftsWindow.SelectedGiftsTab.Boosters:
			{
				this.OnBoostersClicked();
				break;
			}
		}
	}

	private void OnWindowClose()
	{
		SendGiftsWindow.receiverName = string.Empty;
		SendGiftsWindow.receiverLevel = 0;
		SendGiftsWindow.isOnReceiverSelectScreen = false;
	}

	public void PopulateAddToListResponse(byte returnCode, string userName)
	{
		if (!SendGiftsWindow.isOnReceiverSelectScreen)
		{
			return;
		}
		if (this.newEntryUserName != null)
		{
			this.newEntryUserName.text = string.Empty;
		}
		switch (returnCode)
		{
			case 0:
			{
				this.addingToListResponseLbl.TextColor = GuiNewStyleBar.greenColor;
				this.addingToListResponseLbl.text = string.Format(StaticData.Translate("key_profile_screen_return_code_ok"), userName);
				break;
			}
			case 1:
			{
				this.addingToListResponseLbl.TextColor = GuiNewStyleBar.redColor;
				this.addingToListResponseLbl.text = string.Format(StaticData.Translate("key_profile_screen_return_code_not_found"), userName);
				break;
			}
			case 2:
			{
				this.addingToListResponseLbl.TextColor = GuiNewStyleBar.redColor;
				this.addingToListResponseLbl.text = string.Format(StaticData.Translate("key_profile_screen_return_code_already"), userName);
				break;
			}
		}
	}

	public void PopulateScreen()
	{
		// 
		// Current member / type: System.Void SendGiftsWindow::PopulateScreen()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void PopulateScreen()
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

	private void ShowConfirm()
	{
		this.dialogWindow = new GuiWindow()
		{
			isModal = true
		};
		this.dialogWindow.SetBackgroundTexture("FrameworkGUI", "dialogBoxFrame");
		this.dialogWindow.isHidden = false;
		this.dialogWindow.zOrder = 220;
		this.dialogWindow.PutToCenter();
		this.dialogWindow.isModal = true;
		this.dialogWindow.isHidden = false;
		AndromedaGui.gui.AddWindow(this.dialogWindow);
		AndromedaGui.gui.activeToolTipId = this.dialogWindow.handler;
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("SendGiftsWindow", "btnCancel");
		guiButtonFixed.X = 294f;
		guiButtonFixed.Y = 131f;
		guiButtonFixed.Caption = StaticData.Translate("key_send_gifts_confirm_cancel");
		guiButtonFixed.FontSize = 12;
		guiButtonFixed.Alignment = 4;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, SendGiftsWindow.OnSendCancel);
		this.dialogWindow.AddGuiElement(guiButtonFixed);
		GuiButtonFixed action = new GuiButtonFixed();
		action.SetTexture("SendGiftsWindow", "btnSend");
		action.boundries.set_x(60f);
		action.boundries.set_y(131f);
		action.Caption = StaticData.Translate("key_send_gifts_confirm_send");
		action.FontSize = 12;
		action.Alignment = 4;
		action.Clicked = new Action<EventHandlerParam>(this, SendGiftsWindow.OnSendConfirm);
		this.dialogWindow.AddGuiElement(action);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(30f, 39f, 200f, 20f),
			text = StaticData.Translate("key_send_gifts_confirm_gift_lbl"),
			Alignment = 3,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		this.dialogWindow.AddGuiElement(guiLabel);
		GuiLabel fontBold = new GuiLabel()
		{
			boundries = new Rect(30f, 60f, 200f, 20f)
		};
		if (!PlayerItems.IsPowerUp(this.selectedGift) || PlayerItems.IsPackageDeal(this.selectedGift))
		{
			fontBold.text = StaticData.Translate(StaticData.allTypes.get_Item(this.selectedGift).uiName);
		}
		else
		{
			fontBold.text = string.Format(StaticData.Translate(StaticData.allTypes.get_Item(this.selectedGift).uiName), PlayerItems.GetPowerUpEffectValue(this.selectedGift, SendGiftsWindow.receiverLevel));
		}
		fontBold.Alignment = 3;
		fontBold.FontSize = 14;
		fontBold.Font = GuiLabel.FontBold;
		fontBold.TextColor = GuiNewStyleBar.blueColor;
		this.dialogWindow.AddGuiElement(fontBold);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(30f, 81f, 200f, 20f),
			text = StaticData.Translate("key_send_gifts_confirm_receiver_lbl"),
			Alignment = 3,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		this.dialogWindow.AddGuiElement(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(30f, 102f, 200f, 20f),
			text = string.Concat(SendGiftsWindow.receiverName, string.Format(" [{0}]", SendGiftsWindow.receiverLevel)),
			Alignment = 3,
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor
		};
		this.dialogWindow.AddGuiElement(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(245f, 40f, 220f, 20f),
			text = string.Format(StaticData.Translate("key_send_gifts_confirm_gift_title"), 20),
			Alignment = 3,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		this.dialogWindow.AddGuiElement(guiLabel3);
		this.giftTitleBox = new GuiTextBox()
		{
			boundries = new Rect(245f, 58f, 220f, 30f),
			Alignment = 3,
			FontSize = 14,
			TextColor = GuiNewStyleBar.blueColor,
			Validate = new Action(this, SendGiftsWindow.ValidateGiftTitle)
		};
		this.dialogWindow.AddGuiElement(this.giftTitleBox);
		GuiCheckbox guiCheckbox = new GuiCheckbox()
		{
			X = 245f,
			Y = 96f,
			isActive = true,
			OnCheckboxSelected = new Action<bool>(this, SendGiftsWindow.OnAnonymuslyClick)
		};
		this.dialogWindow.AddGuiElement(guiCheckbox);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			boundries = new Rect(270f, 98f, 195f, 20f),
			text = StaticData.Translate("key_send_gifts_confirm_anonymously"),
			Alignment = 3,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		this.dialogWindow.AddGuiElement(guiLabel4);
	}

	private void ValidateGiftTitle()
	{
		if (this.giftTitleBox.text.get_Length() > 20)
		{
			this.giftTitleBox.text = this.giftTitleBox.text.Substring(0, 20);
		}
	}

	private enum SelectedGiftsTab
	{
		Free,
		PowerUps,
		Boosters
	}
}