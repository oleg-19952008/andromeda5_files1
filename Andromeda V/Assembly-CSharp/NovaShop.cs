using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using TransferableObjects;
using UnityEngine;

public class NovaShop : GuiWindow
{
	private GuiButtonFixed btnClose;

	private GuiTexture mainScreenBG;

	private int exchangeRate;

	public List<GuiElement> forDelete;

	private GuiScrollingContainer novaShopScroller;

	private GuiScrollingContainer recruiterScroller;

	private NovaShop.NovaShopScrollerItem cargoBooster1;

	private NovaShop.NovaShopScrollerItem weaponBooster1;

	private NovaShop.NovaShopScrollerItem experienceBooster1;

	private NovaShop.NovaShopScrollerItem miningBooster1;

	private NovaShop.NovaShopScrollerItem cargoBooster3;

	private NovaShop.NovaShopScrollerItem weaponBooster3;

	private NovaShop.NovaShopScrollerItem experienceBooster3;

	private NovaShop.NovaShopScrollerItem miningBooster3;

	private NovaShop.NovaShopScrollerItem cargoBooster6;

	private NovaShop.NovaShopScrollerItem weaponBooster6;

	private NovaShop.NovaShopScrollerItem experienceBooster6;

	private NovaShop.NovaShopScrollerItem miningBooster6;

	private NovaShop.NovaShopScrollerItem neuronModule;

	private List<GuiElement> specialSectionForDelete = new List<GuiElement>();

	private GuiLabel lblExchAmountResult;

	private GuiLabel lblNextLevelNovaRate;

	private GuiLabel lblExchNova;

	private GuiHorizontalSlider sliderQty;

	private GuiButtonResizeable btnExchange;

	private GuiButtonFixed btnMinus;

	private GuiButtonFixed btnPlus;

	private bool isInit;

	public string FeedToId = FB.UserId;

	public string FeedLink = playWebGame.authorization.url_recruit.ToString();

	public string FeedLinkName = "Check out Andromeda 5";

	public string FeedLinkCaption = "Andromeda 5";

	public string FeedLinkDescription = "www.andromeda5.com";

	public string FeedPicture = string.Empty;

	public string FeedMediaSource = string.Empty;

	public string FeedActionName = string.Empty;

	public string FeedActionLink = string.Empty;

	public string FeedReference = string.Empty;

	public bool IncludeFeedProperties;

	private Dictionary<string, string[]> FeedProperties = new Dictionary<string, string[]>();

	public NovaShop()
	{
	}

	private void Callback(FBResult result)
	{
		Debug.Log("Callback Login");
		Debug.Log(string.Concat("user Id : ", FB.UserId));
		Debug.Log(string.Concat("is logged in : ", FB.IsLoggedIn));
		if (result.Error == null || FB.IsLoggedIn)
		{
			this.CallFBFeed();
			return;
		}
		Debug.Log(string.Concat("Login failed.Result : ", result.Error));
	}

	private void CallbackFeed(FBResult result)
	{
		if (result.Error == null)
		{
			return;
		}
		Debug.Log(string.Concat("Feed failed.Result : ", result.Error));
	}

	private void CallFBFeed()
	{
		Debug.Log("CallFBFeed");
		Dictionary<string, string[]> feedProperties = null;
		if (this.IncludeFeedProperties)
		{
			feedProperties = this.FeedProperties;
		}
		string feedLinkCaption = this.FeedLinkCaption;
		string feedLinkName = this.FeedLinkName;
		FB.Feed(string.Empty, this.FeedLink, feedLinkName, feedLinkCaption, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, null, new FacebookDelegate(this.CallbackFeed));
	}

	private void CallFBLogin()
	{
		FB.Login("email,publish_actions", new FacebookDelegate(this.Callback));
	}

	private void ClearSpecialSection()
	{
		if (this.recruiterScroller != null)
		{
			this.recruiterScroller.Claer();
		}
		base.RemoveGuiElement(this.recruiterScroller);
		this.recruiterScroller = null;
		foreach (GuiElement guiElement in this.specialSectionForDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.specialSectionForDelete.Clear();
	}

	private void ConfirmNovaShopExchangeNova(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void NovaShop::ConfirmNovaShopExchangeNova(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void ConfirmNovaShopExchangeNova(EventHandlerParam)
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
		int num = NetworkScript.player.playerBelongings.playerLevel;
		this.exchangeRate = (int)(Math.Ceiling(0.0016 * (double)(num * num) + (double)num) * 5);
		int num1 = (int)(Math.Ceiling(0.0016 * (double)((num + 1) * (num + 1)) + (double)(num + 1)) * 5);
		if (this.novaShopScroller != null)
		{
			this.novaShopScroller.Claer();
			base.RemoveClippingBoundaries(this.novaShopScroller.scrollerId);
		}
		this.forDelete = new List<GuiElement>();
		base.SetBackgroundTexture("NewGUI", "novashop_frame");
		base.PutToCenter();
		this.zOrder = 210;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("NewGUI", "novashop_frame_1");
		guiTexture.X = 39f;
		guiTexture.Y = 40f;
		base.AddGuiElement(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("NewGUI", "ico_nova_big");
		guiTexture1.X = 65f;
		guiTexture1.Y = 65f;
		base.AddGuiElement(guiTexture1);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = StaticData.Translate("key_nova_shop_lbl_nova").ToUpper(),
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 26,
			Font = GuiLabel.FontBold,
			boundries = new Rect(100f, 65f, 215f, 22f),
			Alignment = 3
		};
		base.AddGuiElement(guiLabel);
		GuiTexture guiTexture2 = new GuiTexture();
		guiTexture2.SetTexture("NewGUI", "bullet_small");
		guiTexture2.X = 69f;
		guiTexture2.Y = 102f;
		base.AddGuiElement(guiTexture2);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			text = StaticData.Translate("key_nova_shop_tip1"),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			boundries = new Rect(88f, 100f, 478f, 13f),
			Alignment = 3
		};
		base.AddGuiElement(guiLabel1);
		GuiTexture guiTexture3 = new GuiTexture();
		guiTexture3.SetTexture("NewGUI", "bullet_small");
		guiTexture3.X = 69f;
		guiTexture3.Y = 117f;
		base.AddGuiElement(guiTexture3);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			text = StaticData.Translate("key_nova_shop_tip2"),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			boundries = new Rect(88f, 115f, 478f, 13f),
			Alignment = 3
		};
		base.AddGuiElement(guiLabel2);
		GuiTexture guiTexture4 = new GuiTexture();
		guiTexture4.SetTexture("NewGUI", "bullet_small");
		guiTexture4.X = 69f;
		guiTexture4.Y = 132f;
		base.AddGuiElement(guiTexture4);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			text = StaticData.Translate("key_nova_shop_tip3"),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			boundries = new Rect(88f, 130f, 478f, 13f),
			Alignment = 3
		};
		base.AddGuiElement(guiLabel3);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.boundries.set_x(365f);
		guiButtonResizeable.boundries.set_y(56f);
		guiButtonResizeable.boundries.set_width(205f);
		guiButtonResizeable.Caption = StaticData.Translate("key_nova_shop_btn_get_nova").ToUpper();
		guiButtonResizeable.FontSize = 16;
		guiButtonResizeable.Alignment = 5;
		guiButtonResizeable.MarginRight = 20;
		guiButtonResizeable.SetOrangeTexture();
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, NovaShop.OnGetNovaClicked);
		base.AddGuiElement(guiButtonResizeable);
		GuiTexture guiTexture5 = new GuiTexture()
		{
			boundries = new Rect(guiButtonResizeable.boundries.get_x() + 10f, guiButtonResizeable.boundries.get_y() + 11f, 20f, 20f)
		};
		guiTexture5.SetTextureKeepSize("NewGUI", "icon_white_nova");
		base.AddGuiElement(guiTexture5);
		if (playWebGame.authorization.payments_promotion)
		{
			GuiTexture rect = new GuiTexture();
			rect.SetTexture("ConfigWnd", "PromoBadge");
			rect.boundries = new Rect(543f, 44f, 42f, 42f);
			base.AddGuiElement(rect);
		}
		GuiTexture guiTexture6 = new GuiTexture();
		guiTexture6.SetTexture("NewGUI", "novashop_frame_2");
		guiTexture6.X = 39f;
		guiTexture6.Y = 167f;
		base.AddGuiElement(guiTexture6);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			boundries = new Rect(62f, 187f, 265f, 16f),
			text = StaticData.Translate("key_nova_shop_exchange_lbl"),
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 15,
			Font = GuiLabel.FontBold,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel4);
		GuiTexture guiTexture7 = new GuiTexture();
		guiTexture7.SetTexture("FrameworkGUI", "res_nova");
		guiTexture7.X = 59f;
		guiTexture7.Y = 234f;
		base.AddGuiElement(guiTexture7);
		this.lblExchNova = new GuiLabel()
		{
			text = string.Empty,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			boundries = new Rect(guiTexture7.X + 25f, guiTexture7.Y, 240f, 20f),
			Alignment = 3
		};
		base.AddGuiElement(this.lblExchNova);
		this.lblExchAmountResult = new GuiLabel()
		{
			text = string.Empty,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(guiTexture7.X + 25f, guiTexture7.Y, 240f, 20f),
			Alignment = 5
		};
		base.AddGuiElement(this.lblExchAmountResult);
		this.lblNextLevelNovaRate = new GuiLabel()
		{
			text = string.Format(StaticData.Translate("key_nova_shop_next_lvl_exchange"), num + 1, num1),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			boundries = new Rect(365f, 180f, 205f, 32f),
			Alignment = 3
		};
		if (num < Enumerable.Max(StaticData.levelsType.get_Keys()))
		{
			base.AddGuiElement(this.lblNextLevelNovaRate);
		}
		this.btnExchange = new GuiButtonResizeable();
		this.btnExchange.boundries.set_x(365f);
		this.btnExchange.boundries.set_y(212f);
		this.btnExchange.boundries.set_width(205f);
		this.btnExchange.Caption = StaticData.Translate("key_nova_shop_btn_exchange").ToUpper();
		this.btnExchange.FontSize = 16;
		this.btnExchange.Alignment = 5;
		this.btnExchange.MarginRight = 20;
		this.btnExchange.SetOrangeTexture();
		this.btnExchange.Clicked = new Action<EventHandlerParam>(this, NovaShop.OnExchange);
		base.AddGuiElement(this.btnExchange);
		GuiTexture guiTexture8 = new GuiTexture()
		{
			boundries = new Rect(77f, 221f, 235f, 3f)
		};
		guiTexture8.SetTextureKeepSize("NewGUI", "ResourceSlideLine");
		base.AddGuiElement(guiTexture8);
		this.btnMinus = new GuiButtonFixed();
		this.btnMinus.SetTexture("NewGUI", "btnSliderMinus");
		this.btnMinus.X = 51f;
		this.btnMinus.Y = 215f;
		this.btnMinus.Caption = string.Empty;
		this.btnMinus.Clicked = new Action<EventHandlerParam>(this, NovaShop.OnMinusBtnClicked);
		this.btnMinus.SetLeftClickSound("Sounds", "minus");
		base.AddGuiElement(this.btnMinus);
		this.btnPlus = new GuiButtonFixed();
		this.btnPlus.SetTexture("NewGUI", "btnSliderPlus");
		this.btnPlus.X = 312f;
		this.btnPlus.Y = 215f;
		this.btnPlus.Caption = string.Empty;
		this.btnPlus.Clicked = new Action<EventHandlerParam>(this, NovaShop.OnPlusBtnClicked);
		this.btnPlus.SetLeftClickSound("Sounds", "plus");
		base.AddGuiElement(this.btnPlus);
		this.sliderQty = new GuiHorizontalSlider()
		{
			MAX = (float)NetworkScript.player.playerBelongings.playerItems.GetAmountAt(PlayerItems.TypeNova),
			MIN = Math.Min(1f, this.sliderQty.MAX),
			CurrentValue = this.sliderQty.MIN,
			Y = 218f,
			Width = 213f,
			X = 88f
		};
		this.sliderQty.SetSliderTumb("NewGUI", "resSliderThumb");
		this.sliderQty.SetEmptySliderTexture();
		this.sliderQty.refreshData = new Action(this, NovaShop.OnExchangeAmountChange);
		base.AddGuiElement(this.sliderQty);
		this.DrawEquilibriumSection();
		this.CreateScroller();
		this.neuronModule = new NovaShop.NovaShopScrollerItem();
		this.neuronModule.Create(PlayerItems.TypeNeuron, 0f, 0f, this.novaShopScroller, null, this);
		this.miningBooster1 = new NovaShop.NovaShopScrollerItem();
		this.miningBooster1.Create(PlayerItems.TypeBoosterAutominerFor1Days, 265f, 0f, this.novaShopScroller, null, this);
		this.miningBooster3 = new NovaShop.NovaShopScrollerItem();
		this.miningBooster3.Create(PlayerItems.TypeBoosterAutominerFor3Days, 0f, 115f, this.novaShopScroller, null, this);
		this.miningBooster6 = new NovaShop.NovaShopScrollerItem();
		this.miningBooster6.Create(PlayerItems.TypeBoosterAutominerFor6Days, 265f, 115f, this.novaShopScroller, null, this);
		this.cargoBooster1 = new NovaShop.NovaShopScrollerItem();
		this.cargoBooster1.Create(PlayerItems.TypeBoosterCargoFor1Days, 0f, 230f, this.novaShopScroller, null, this);
		this.cargoBooster3 = new NovaShop.NovaShopScrollerItem();
		this.cargoBooster3.Create(PlayerItems.TypeBoosterCargoFor3Days, 265f, 230f, this.novaShopScroller, null, this);
		this.cargoBooster6 = new NovaShop.NovaShopScrollerItem();
		this.cargoBooster6.Create(PlayerItems.TypeBoosterCargoFor6Days, 0f, 345f, this.novaShopScroller, null, this);
		this.experienceBooster1 = new NovaShop.NovaShopScrollerItem();
		this.experienceBooster1.Create(PlayerItems.TypeBoosterExperienceFor1Days, 265f, 345f, this.novaShopScroller, null, this);
		this.experienceBooster3 = new NovaShop.NovaShopScrollerItem();
		this.experienceBooster3.Create(PlayerItems.TypeBoosterExperienceFor3Days, 0f, 460f, this.novaShopScroller, null, this);
		this.experienceBooster6 = new NovaShop.NovaShopScrollerItem();
		this.experienceBooster6.Create(PlayerItems.TypeBoosterExperienceFor6Days, 265f, 460f, this.novaShopScroller, null, this);
		this.weaponBooster1 = new NovaShop.NovaShopScrollerItem();
		this.weaponBooster1.Create(PlayerItems.TypeBoosterDamageFor1Days, 0f, 575f, this.novaShopScroller, null, this);
		this.weaponBooster3 = new NovaShop.NovaShopScrollerItem();
		this.weaponBooster3.Create(PlayerItems.TypeBoosterDamageFor3Days, 265f, 575f, this.novaShopScroller, null, this);
		this.weaponBooster6 = new NovaShop.NovaShopScrollerItem();
		this.weaponBooster6.Create(PlayerItems.TypeBoosterDamageFor6Days, 0f, 690f, this.novaShopScroller, null, this);
		this.Populate();
		this.isHidden = false;
	}

	private void CreateScroller()
	{
		this.novaShopScroller = new GuiScrollingContainer(41f, 290f, 555f, 230f, 1, this);
		this.novaShopScroller.SetArrowStep(115f);
		base.AddGuiElement(this.novaShopScroller);
		this.forDelete.Add(this.novaShopScroller);
	}

	private void DrawEquilibriumSection()
	{
		this.ClearSpecialSection();
		float single = 0f;
		GuiTexture guiTexture = new GuiTexture();
		single = 0f;
		guiTexture.SetTexture("NewGUI", "novashop_frame_3");
		guiTexture.X = 594f;
		guiTexture.Y = 40f;
		base.AddGuiElement(guiTexture);
		this.specialSectionForDelete.Add(guiTexture);
		if (playWebGame.GAME_TYPE == "ru")
		{
			GuiTexture guiTexture1 = new GuiTexture();
			guiTexture1.SetTexture("Partners", "recruitHover");
			guiTexture1.X = 601f;
			guiTexture1.Y = 47f;
			base.AddGuiElement(guiTexture1);
			this.specialSectionForDelete.Add(guiTexture1);
			return;
		}
		GuiTexture guiTexture2 = new GuiTexture();
		guiTexture2.SetTexture("NewGUI", "ico_equilibrium_big");
		guiTexture2.X = 617f;
		guiTexture2.Y = 63f + single;
		base.AddGuiElement(guiTexture2);
		this.specialSectionForDelete.Add(guiTexture2);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = StaticData.Translate("key_nova_shop_lbl_equilibrium").ToUpper(),
			TextColor = GuiNewStyleBar.purpleColor,
			FontSize = 26,
			Font = GuiLabel.FontBold,
			boundries = new Rect(645f, 65f + single, 215f, 22f),
			Alignment = 3
		};
		base.AddGuiElement(guiLabel);
		this.specialSectionForDelete.Add(guiLabel);
		GuiTexture guiTexture3 = new GuiTexture();
		guiTexture3.SetTexture("NewGUI", "bullet_small");
		guiTexture3.X = 617f;
		guiTexture3.Y = 102f + single;
		base.AddGuiElement(guiTexture3);
		this.specialSectionForDelete.Add(guiTexture3);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			text = StaticData.Translate("key_nova_shop_viral_tip1"),
			FontSize = 12,
			boundries = new Rect(635f, 100f + single, 215f, 55f)
		};
		base.AddGuiElement(guiLabel1);
		this.specialSectionForDelete.Add(guiLabel1);
		GuiTexture guiTexture4 = new GuiTexture();
		guiTexture4.SetTexture("NewGUI", "bullet_small");
		guiTexture4.X = 617f;
		guiTexture4.Y = 160f + single;
		base.AddGuiElement(guiTexture4);
		this.specialSectionForDelete.Add(guiTexture4);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			text = StaticData.Translate("key_nova_shop_viral_tip2"),
			FontSize = 12,
			boundries = new Rect(635f, 158f + single, 215f, 42f)
		};
		base.AddGuiElement(guiLabel2);
		this.specialSectionForDelete.Add(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			text = StaticData.Translate("key_nova_shop_viral_recruits"),
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 14,
			Font = GuiLabel.FontBold,
			boundries = new Rect(616f, 250f + single, 215f, 16f),
			Alignment = 3
		};
		base.AddGuiElement(guiLabel3);
		this.specialSectionForDelete.Add(guiLabel3);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			text = StaticData.Translate("key_nova_shop_viral_player").ToUpper(),
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 14,
			boundries = new Rect(616f, 273f + single, 89f, 16f),
			Alignment = 3
		};
		base.AddGuiElement(guiLabel4);
		this.specialSectionForDelete.Add(guiLabel4);
		GuiLabel guiLabel5 = new GuiLabel()
		{
			text = StaticData.Translate("key_nova_shop_viral_level").ToUpper(),
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 14,
			boundries = new Rect(768f, 273f + single, 40f, 16f),
			Alignment = 3
		};
		base.AddGuiElement(guiLabel5);
		this.specialSectionForDelete.Add(guiLabel5);
		GuiTexture guiTexture5 = new GuiTexture();
		guiTexture5.SetTexture("FrameworkGUI", "res_equilibrium");
		guiTexture5.X = 816f;
		guiTexture5.Y = 270f + single;
		base.AddGuiElement(guiTexture5);
		this.specialSectionForDelete.Add(guiTexture5);
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("NewGUI", "table_line_header");
		rect.boundries = new Rect(616f, 292f + single, 223f, 1f);
		base.AddGuiElement(rect);
		this.specialSectionForDelete.Add(rect);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.boundries.set_x(617f);
		guiButtonResizeable.boundries.set_y(200f + single);
		guiButtonResizeable.boundries.set_width(245f);
		guiButtonResizeable.Caption = StaticData.Translate("key_nova_shop_viral_btn_copy").ToUpper();
		guiButtonResizeable.FontSize = 11;
		guiButtonResizeable.Alignment = 4;
		guiButtonResizeable.SetBlueTexture();
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, NovaShop.OnCopyLinkClicked);
		base.AddGuiElement(guiButtonResizeable);
		this.specialSectionForDelete.Add(guiButtonResizeable);
		this.recruiterScroller = new GuiScrollingContainer(610f, 295f + single, 265f, 215f - single, 2, this);
		this.recruiterScroller.SetArrowStep(20f);
		base.AddGuiElement(this.recruiterScroller);
		this.forDelete.Add(this.recruiterScroller);
		this.specialSectionForDelete.Add(this.recruiterScroller);
		this.DrawRecruiters();
	}

	private void DrawGetNovaSection()
	{
		this.ClearSpecialSection();
		GuiTexture guiTexture = new GuiTexture()
		{
			X = 594f,
			Y = 40f
		};
		base.AddGuiElement(guiTexture);
		this.specialSectionForDelete.Add(guiTexture);
		guiTexture.SetTexture("NewGUI", "novashop_frame_3");
		for (int i = 0; i <= 5; i++)
		{
			GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
			guiButtonFixed.SetTexture("NewGUI", "get_nova_frame_bonus");
			guiButtonFixed.X = 615f;
			guiButtonFixed.Y = (float)(100 + i * 65);
			guiButtonFixed.Caption = string.Empty;
			base.AddGuiElement(guiButtonFixed);
			this.specialSectionForDelete.Add(guiButtonFixed);
			GuiLabel guiLabel = new GuiLabel()
			{
				boundries = new Rect(guiButtonFixed.X + 225f, guiButtonFixed.Y - 5f, 50f, 12f),
				text = string.Format("+{0}%", i * 10),
				FontSize = 14,
				Font = GuiLabel.FontBold
			};
			guiLabel.SetAngel(37f);
			base.AddGuiElement(guiLabel);
			this.specialSectionForDelete.Add(guiLabel);
			if (i == 0)
			{
				guiLabel.text = string.Empty;
				guiButtonFixed.SetTexture("NewGUI", "get_nova_frame");
			}
			else if (i == 5)
			{
				guiLabel.text = string.Format("FREE", new object[0]);
				guiButtonFixed.SetTexture("iPad/NovaShop", "get_nova_frame_free");
			}
			GuiLabel guiLabel1 = new GuiLabel()
			{
				boundries = new Rect(guiButtonFixed.X + 60f, guiButtonFixed.Y + 10f, 180f, 24f),
				Alignment = 3,
				FontSize = 16,
				TextColor = GuiNewStyleBar.orangeColor,
				Font = GuiLabel.FontBold
			};
			base.AddGuiElement(guiLabel1);
			this.specialSectionForDelete.Add(guiLabel1);
			GuiLabel guiLabel2 = new GuiLabel()
			{
				boundries = new Rect(guiButtonFixed.X + 60f, guiButtonFixed.Y + 35f, 180f, 20f),
				Alignment = 3,
				FontSize = 12
			};
			base.AddGuiElement(guiLabel2);
			this.specialSectionForDelete.Add(guiLabel2);
			switch (i)
			{
				case 0:
				{
					guiLabel1.text = "1000 NOVA";
					guiLabel2.text = "1.99$";
					guiButtonFixed.eventHandlerParam.customData = 1000;
					break;
				}
				case 1:
				{
					guiLabel1.text = "2750 NOVA";
					guiLabel2.text = "4.99$";
					guiButtonFixed.eventHandlerParam.customData = 2750;
					break;
				}
				case 2:
				{
					guiLabel1.text = "12000 NOVA";
					guiLabel2.text = "19.99$";
					guiButtonFixed.eventHandlerParam.customData = 12000;
					break;
				}
				case 3:
				{
					guiLabel1.text = "32500 NOVA";
					guiLabel2.text = "49.99$";
					guiButtonFixed.eventHandlerParam.customData = 32500;
					break;
				}
				case 4:
				{
					guiLabel1.text = "63000 NOVA";
					guiLabel2.text = "89.99$";
					guiButtonFixed.eventHandlerParam.customData = 63000;
					break;
				}
				case 5:
				{
					guiLabel1.text = StaticData.Translate("key_get_nova_free");
					GuiButtonFixed y = guiButtonFixed;
					y.Y = y.Y + 20f;
					GuiLabel y1 = guiLabel;
					y1.Y = y1.Y + 20f;
					GuiLabel y2 = guiLabel1;
					y2.Y = y2.Y + 22f;
					guiButtonFixed.eventHandlerParam.customData = 0;
					break;
				}
			}
		}
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("NewGUI", "get_nova_separator");
		guiTexture1.X = 600f;
		guiTexture1.Y = 436f;
		base.AddGuiElement(guiTexture1);
		this.specialSectionForDelete.Add(guiTexture1);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(600f, 426f, 277f, 20f),
			text = "or",
			Alignment = 4,
			FontSize = 14,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(guiLabel3);
		this.specialSectionForDelete.Add(guiLabel3);
	}

	private void DrawRecruiters()
	{
		for (int i = 0; i < NetworkScript.player.playerBelongings.referals.referals.get_Count(); i++)
		{
			GuiLabel guiLabel = new GuiLabel()
			{
				boundries = new Rect(5f, (float)(0 + i * 20), 155f, 20f),
				Alignment = 3,
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = NetworkScript.player.playerBelongings.referals.referals.get_Item(i).userName
			};
			this.recruiterScroller.AddContent(guiLabel);
			GuiLabel guiLabel1 = new GuiLabel()
			{
				boundries = new Rect(163f, (float)(0 + i * 20), 40f, 20f),
				Alignment = 3,
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = NetworkScript.player.playerBelongings.referals.referals.get_Item(i).level.ToString()
			};
			this.recruiterScroller.AddContent(guiLabel1);
			GuiLabel guiLabel2 = new GuiLabel()
			{
				boundries = new Rect(205f, (float)(0 + i * 20), 40f, 20f),
				Alignment = 3,
				Font = GuiLabel.FontBold,
				FontSize = 12,
				text = NetworkScript.player.playerBelongings.referals.referals.get_Item(i).viral.ToString()
			};
			this.recruiterScroller.AddContent(guiLabel2);
			GuiTexture guiTexture = new GuiTexture()
			{
				boundries = new Rect(5f, (float)(19 + i * 20), 225f, 1f)
			};
			guiTexture.SetTextureKeepSize("NewGUI", "table_line");
			this.recruiterScroller.AddContent(guiTexture);
		}
	}

	private void OnCopyLinkClicked(object prm)
	{
		Application.OpenURL(playWebGame.authorization.url_recruit);
	}

	private void OnExchange(EventHandlerParam prm)
	{
		string str = StaticData.Translate("key_novashop_exchange_title");
		string str1 = StaticData.Translate("key_novashop_exchange_info");
		string str2 = this.sliderQty.CurrentValueInt.ToString("N0");
		int currentValueInt = this.sliderQty.CurrentValueInt * this.exchangeRate;
		string str3 = string.Format(str1, str2, currentValueInt.ToString("N0"));
		string str4 = StaticData.Translate("key_dock_my_ships_select_ship_yes");
		string str5 = StaticData.Translate("key_dock_my_ships_select_ship_no");
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = this.sliderQty.CurrentValueInt
		};
		NewPopUpWindow.CreatePurchasePopUpWindow(str, str3, str4, str5, out Inventory.dialogWindow, eventHandlerParam, new Action<EventHandlerParam>(this, NovaShop.ConfirmNovaShopExchangeNova));
	}

	private void OnExchangeAmountChange()
	{
		this.Populate();
	}

	private void OnFBClicked(EventHandlerParam prm)
	{
		playWebGame.udp.ExecuteCommand(PureUdpClient.CommandIamOnBackground, null);
		if (this.isInit)
		{
			if (FB.IsLoggedIn)
			{
				this.CallFBFeed();
			}
			else
			{
				this.CallFBLogin();
			}
			return;
		}
		Debug.Log(string.Concat("App id 0 : ", FBSettings.AppId));
		FB.Init(new InitDelegate(this.OnInitComplete), new HideUnityDelegate(this.OnHideUnity), null);
	}

	private void OnGetNovaClicked(EventHandlerParam prm)
	{
		try
		{
			if (AndromedaGui.mainWnd != null)
			{
				AndromedaGui.mainWnd.CloseActiveWindow();
			}
		}
		catch (NullReferenceException nullReferenceException)
		{
			Debug.Log(nullReferenceException.get_Message());
		}
		if (!playWebGame.isWebPlayer)
		{
			Application.OpenURL(playWebGame.authorization.url_payments);
			return;
		}
		if (playWebGame.GAME_TYPE != "ru")
		{
			Application.ExternalCall("openPayments", new object[0]);
			return;
		}
		Application.ExternalCall("sendNotification", new object[] { "openPayments", 1 });
	}

	private void OnGetNovaSectionClicked(object prm)
	{
		this.DrawGetNovaSection();
	}

	private void OnHideUnity(bool isGameShown)
	{
		Debug.Log(string.Concat("Is game showing? ", isGameShown));
	}

	private void OnInitComplete()
	{
		Debug.Log(string.Concat("FB.Init completed: Is user logged in? ", FB.IsLoggedIn));
		this.isInit = true;
		if (FB.IsLoggedIn)
		{
			this.CallFBFeed();
			return;
		}
		Debug.Log("CallFbLogin");
		this.CallFBLogin();
	}

	private void OnMinusBtnClicked(object prm)
	{
		GuiHorizontalSlider currentValue = this.sliderQty;
		currentValue.CurrentValue = currentValue.CurrentValue - 1f;
		if (this.sliderQty.CurrentValue < this.sliderQty.MIN)
		{
			this.sliderQty.CurrentValue = this.sliderQty.MIN;
		}
	}

	private void OnPlusBtnClicked(object prm)
	{
		GuiHorizontalSlider currentValue = this.sliderQty;
		currentValue.CurrentValue = currentValue.CurrentValue + 1f;
		if (this.sliderQty.CurrentValue > this.sliderQty.MAX)
		{
			this.sliderQty.CurrentValue = this.sliderQty.MAX;
		}
	}

	private void OnTwitterClicked(EventHandlerParam prm)
	{
		playWebGame.udp.ExecuteCommand(PureUdpClient.CommandIamOnBackground, null);
		string str = string.Concat("http://twitter.com/intent/tweet?text=", playWebGame.authorization.url_recruit.ToString());
		Application.OpenURL(str);
	}

	private void OnWinEqSectionClicked(object prm)
	{
		this.DrawEquilibriumSection();
	}

	private void Populate()
	{
		this.btnExchange.isEnabled = NetworkScript.player.playerBelongings.playerItems.GetAmountAt(PlayerItems.TypeNova) > (long)0;
		this.lblExchAmountResult.text = string.Format(StaticData.Translate("key_nova_shop_exchange_cash"), this.sliderQty.CurrentValueInt * this.exchangeRate);
		this.lblExchNova.text = string.Format(StaticData.Translate("key_nova_shop_exchange_nova"), this.sliderQty.CurrentValueInt);
	}

	public void PopulateBoosters()
	{
		float scrollTumbCenter = this.novaShopScroller.ScrollTumbCenter;
		this.neuronModule.Claer();
		this.cargoBooster1.Claer();
		this.miningBooster1.Claer();
		this.experienceBooster1.Claer();
		this.weaponBooster1.Claer();
		this.cargoBooster3.Claer();
		this.miningBooster3.Claer();
		this.experienceBooster3.Claer();
		this.weaponBooster3.Claer();
		this.cargoBooster6.Claer();
		this.miningBooster6.Claer();
		this.experienceBooster6.Claer();
		this.weaponBooster6.Claer();
		this.neuronModule.Create(PlayerItems.TypeNeuron, 0f, 0f, this.novaShopScroller, null, this);
		this.miningBooster1.Create(PlayerItems.TypeBoosterAutominerFor1Days, 265f, 0f, this.novaShopScroller, null, this);
		this.miningBooster3.Create(PlayerItems.TypeBoosterAutominerFor3Days, 0f, 115f, this.novaShopScroller, null, this);
		this.miningBooster6.Create(PlayerItems.TypeBoosterAutominerFor6Days, 265f, 115f, this.novaShopScroller, null, this);
		this.cargoBooster1.Create(PlayerItems.TypeBoosterCargoFor1Days, 0f, 230f, this.novaShopScroller, null, this);
		this.cargoBooster3.Create(PlayerItems.TypeBoosterCargoFor3Days, 265f, 230f, this.novaShopScroller, null, this);
		this.cargoBooster6.Create(PlayerItems.TypeBoosterCargoFor6Days, 0f, 345f, this.novaShopScroller, null, this);
		this.experienceBooster1.Create(PlayerItems.TypeBoosterExperienceFor1Days, 265f, 345f, this.novaShopScroller, null, this);
		this.experienceBooster3.Create(PlayerItems.TypeBoosterExperienceFor3Days, 0f, 460f, this.novaShopScroller, null, this);
		this.experienceBooster6.Create(PlayerItems.TypeBoosterExperienceFor6Days, 265f, 460f, this.novaShopScroller, null, this);
		this.weaponBooster1.Create(PlayerItems.TypeBoosterDamageFor1Days, 0f, 575f, this.novaShopScroller, null, this);
		this.weaponBooster3.Create(PlayerItems.TypeBoosterDamageFor3Days, 265f, 575f, this.novaShopScroller, null, this);
		this.weaponBooster6.Create(PlayerItems.TypeBoosterDamageFor6Days, 0f, 690f, this.novaShopScroller, null, this);
		if (scrollTumbCenter > 0f)
		{
			this.novaShopScroller.MooveToCenter(scrollTumbCenter);
		}
	}

	public class NovaShopScrollerItem
	{
		private GuiLabel lblTitle;

		private GuiLabel lblStatus;

		private GuiLabel lblDescription;

		private GuiLabel lblPeriod;

		private GuiLabel lblPrice;

		private GuiLabel lblNoMoney;

		private GuiTexture avatar;

		private GuiTexture novaIcon;

		private GuiTexture avatarFrame;

		private GuiTexture mainFrame;

		private GuiTexture hoverFrame;

		private GuiButtonFixed btn;

		private GuiButtonResizeable btnActivete;

		private bool isBooster;

		private string itemAssetname;

		private bool isActivated;

		private bool hasMoney;

		private DateTime activetedTo;

		private GuiWindow wnd;

		private GuiScrollingContainer scrl;

		private ushort itemTypeId;

		private NovaShop novaShopWindow;

		public NovaShopScrollerItem()
		{
		}

		public void Claer()
		{
			if (this.scrl == null || this.wnd != null)
			{
				this.wnd.RemoveGuiElement(this.mainFrame);
				this.wnd.RemoveGuiElement(this.avatar);
				this.wnd.RemoveGuiElement(this.avatarFrame);
				this.wnd.RemoveGuiElement(this.lblTitle);
				this.wnd.RemoveGuiElement(this.lblStatus);
				this.wnd.RemoveGuiElement(this.lblDescription);
				this.wnd.RemoveGuiElement(this.lblPeriod);
				this.wnd.RemoveGuiElement(this.novaIcon);
				this.wnd.RemoveGuiElement(this.lblPrice);
				this.wnd.RemoveGuiElement(this.btn);
				this.wnd.RemoveGuiElement(this.hoverFrame);
				this.wnd.RemoveGuiElement(this.lblNoMoney);
				if (this.btnActivete != null)
				{
					this.wnd.RemoveGuiElement(this.btnActivete);
				}
			}
			else
			{
				this.scrl.Claer();
			}
		}

		private void ConfirmNovaShopPurchase(EventHandlerParam prm)
		{
			// 
			// Current member / type: System.Void NovaShop/NovaShopScrollerItem::ConfirmNovaShopPurchase(EventHandlerParam)
			// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
			// 
			// Product version: 2017.2.502.1
			// Exception in: System.Void ConfirmNovaShopPurchase(EventHandlerParam)
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

		public void Create(ushort itemId, float possitionX, float possitionY, GuiScrollingContainer scroller, GuiWindow window, NovaShop nvs)
		{
			PlayerItemTypesData item = StaticData.allTypes.get_Item(itemId);
			this.isBooster = PlayerItems.IsBooster(itemId);
			this.hasMoney = (long)item.priceNova <= NetworkScript.player.playerBelongings.playerItems.get_Nova();
			this.wnd = window;
			this.scrl = scroller;
			this.itemAssetname = item.assetName;
			this.itemTypeId = itemId;
			this.novaShopWindow = nvs;
			if (this.isBooster)
			{
				if (PlayerItems.IsAutoMinerBooster(itemId))
				{
					this.activetedTo = NetworkScript.player.playerBelongings.boostAutoMinerExpireTime;
				}
				else if (PlayerItems.IsCargoBooster(itemId))
				{
					this.activetedTo = NetworkScript.player.playerBelongings.boostCargoExpireTime;
				}
				else if (PlayerItems.IsDamageBooster(itemId))
				{
					this.activetedTo = NetworkScript.player.playerBelongings.boostDamageExpireTime;
				}
				else if (PlayerItems.IsExperienceBooster(itemId))
				{
					this.activetedTo = NetworkScript.player.playerBelongings.boostExperienceExpireTime;
				}
				this.itemAssetname = string.Concat("novashop_", this.itemAssetname);
				if (this.activetedTo > DateTime.get_Now())
				{
					this.isActivated = true;
				}
			}
			this.mainFrame = new GuiTexture()
			{
				X = possitionX,
				Y = possitionY
			};
			this.mainFrame.SetTexture("NewGUI", "novashop_frame_booster");
			if (this.scrl == null || this.wnd != null)
			{
				this.wnd.AddGuiElement(this.mainFrame);
			}
			else
			{
				this.scrl.AddContent(this.mainFrame);
			}
			this.avatar = new GuiTexture()
			{
				X = possitionX + 18f,
				Y = possitionY + 31f
			};
			this.avatar.SetItemTexture(itemId);
			this.avatar.SetSize(78f, 53f);
			if (this.scrl == null || this.wnd != null)
			{
				this.wnd.AddGuiElement(this.avatar);
			}
			else
			{
				this.scrl.AddContent(this.avatar);
			}
			this.avatarFrame = new GuiTexture()
			{
				X = possitionX + 11f,
				Y = possitionY + 24f
			};
			this.avatarFrame.SetTexture("NewGUI", "novashop_frame_image");
			if (this.scrl == null || this.wnd != null)
			{
				this.wnd.AddGuiElement(this.avatarFrame);
			}
			else
			{
				this.scrl.AddContent(this.avatarFrame);
			}
			this.lblTitle = new GuiLabel()
			{
				boundries = new Rect(possitionX + 17f, possitionY + 12f, 240f, 14f),
				text = StaticData.Translate(item.uiName).ToUpper(),
				Font = GuiLabel.FontBold,
				FontSize = 14,
				Alignment = 3
			};
			if (this.scrl == null || this.wnd != null)
			{
				this.wnd.AddGuiElement(this.lblTitle);
			}
			else
			{
				this.scrl.AddContent(this.lblTitle);
			}
			this.lblStatus = new GuiLabel()
			{
				boundries = new Rect(possitionX + 17f, possitionY + 93f, 225f, 14f)
			};
			if (!this.isBooster)
			{
				this.lblStatus.text = string.Format(StaticData.Translate("key_nova_shop_item_you_have"), NetworkScript.player.playerBelongings.playerItems.GetAmountAt(itemId));
			}
			else
			{
				this.lblStatus.text = StaticData.Translate("key_nova_shop_item_status_deactivated").ToUpper();
			}
			this.lblStatus.TextColor = GuiNewStyleBar.blueColor;
			this.lblStatus.Font = GuiLabel.FontBold;
			this.lblStatus.FontSize = 12;
			if (this.scrl == null || this.wnd != null)
			{
				this.wnd.AddGuiElement(this.lblStatus);
			}
			else
			{
				this.scrl.AddContent(this.lblStatus);
			}
			this.lblDescription = new GuiLabel()
			{
				boundries = new Rect(possitionX + 102f, possitionY + 47f, 155f, 42f),
				text = StaticData.Translate(item.description),
				WordWrap = true,
				TextColor = GuiNewStyleBar.blueColor,
				FontSize = 12
			};
			if (this.scrl == null || this.wnd != null)
			{
				this.wnd.AddGuiElement(this.lblDescription);
			}
			else
			{
				this.scrl.AddContent(this.lblDescription);
			}
			int boosterDuration = PlayerItems.GetBoosterDuration(itemId) / 24;
			this.lblPeriod = new GuiLabel()
			{
				boundries = new Rect(possitionX + 102f, possitionY + 30f, 200f, 14f)
			};
			if (!this.isBooster)
			{
				this.lblPeriod.text = StaticData.Translate("key_nova_shop_item_1for");
			}
			else
			{
				this.lblPeriod.text = string.Format(StaticData.Translate("key_nova_shop_item_3days"), boosterDuration);
			}
			this.lblPeriod.TextColor = GuiNewStyleBar.blueColor;
			this.lblPeriod.FontSize = 12;
			this.lblPeriod.Font = GuiLabel.FontBold;
			if (this.scrl == null || this.wnd != null)
			{
				this.wnd.AddGuiElement(this.lblPeriod);
			}
			else
			{
				this.scrl.AddContent(this.lblPeriod);
			}
			float textWidth = this.lblPeriod.TextWidth;
			this.novaIcon = new GuiTexture()
			{
				X = possitionX + 102f + textWidth + 5f,
				Y = possitionY + 27f
			};
			this.novaIcon.SetTexture("FrameworkGUI", "res_nova");
			if (this.scrl == null || this.wnd != null)
			{
				this.wnd.AddGuiElement(this.novaIcon);
			}
			else
			{
				this.scrl.AddContent(this.novaIcon);
			}
			this.lblPrice = new GuiLabel()
			{
				boundries = new Rect(possitionX + 102f + textWidth + 30f, possitionY + 28f, 200f, 14f),
				text = string.Format("{0:##,##0}", item.priceNova),
				TextColor = GuiNewStyleBar.orangeColor,
				FontSize = 14,
				Font = GuiLabel.FontBold
			};
			if (this.scrl == null || this.wnd != null)
			{
				this.wnd.AddGuiElement(this.lblPrice);
			}
			else
			{
				this.scrl.AddContent(this.lblPrice);
			}
			this.btn = new GuiButtonFixed();
			this.btn.SetTexture("FrameworkGUI", "empty");
			this.btn.boundries = new Rect(possitionX + 7f, possitionY + 7f, 258f, 105f);
			this.btn.Caption = string.Empty;
			this.btn.Hovered = new Action<object, bool>(this, NovaShop.NovaShopScrollerItem.OnHover);
			this.btn.Clicked = new Action<EventHandlerParam>(this, NovaShop.NovaShopScrollerItem.OnActivateBoosterClicked);
			if (!this.hasMoney)
			{
				this.btn.Clicked = new Action<EventHandlerParam>(this, NovaShop.NovaShopScrollerItem.OnGetNovaClicked);
			}
			if (this.scrl == null || this.wnd != null)
			{
				this.wnd.AddGuiElement(this.btn);
			}
			else
			{
				this.scrl.AddContent(this.btn);
			}
			this.hoverFrame = new GuiTexture()
			{
				boundries = new Rect(possitionX, possitionY, 272f, 119f)
			};
			this.hoverFrame.SetTextureKeepSize("FrameworkGUI", "empty");
			if (this.scrl == null || this.wnd != null)
			{
				this.wnd.AddGuiElement(this.hoverFrame);
			}
			else
			{
				this.scrl.AddContent(this.hoverFrame);
			}
			this.lblNoMoney = new GuiLabel()
			{
				boundries = new Rect(this.mainFrame.X + 35f, this.mainFrame.Y + 15f, 200f, 30f),
				text = string.Empty,
				TextColor = GuiNewStyleBar.redColor,
				FontSize = 14,
				Alignment = 4,
				Font = GuiLabel.FontBold
			};
			if (this.scrl == null || this.wnd != null)
			{
				this.wnd.AddGuiElement(this.lblNoMoney);
			}
			else
			{
				this.scrl.AddContent(this.lblNoMoney);
			}
			if (this.isBooster && this.isActivated)
			{
				TimeSpan timeSpan = this.activetedTo - StaticData.now;
				long totalSeconds = (long)timeSpan.get_TotalSeconds();
				this.mainFrame.SetTexture("NewGUI", "novashop_frame_booster_active");
				this.avatarFrame.SetTexture("NewGUI", "novashop_frame_image_active");
				if (totalSeconds <= (long)60)
				{
					this.lblStatus.text = string.Format(StaticData.Translate("key_profile_screen_playtime_value"), 0, 0, 1);
				}
				else
				{
					this.lblStatus.text = string.Format(StaticData.Translate("key_profile_screen_playtime_value"), totalSeconds / (long)86400, totalSeconds / (long)3600 % (long)24, totalSeconds / (long)60 % (long)60);
				}
				this.lblStatus.TextColor = Color.get_white();
				this.lblDescription.TextColor = Color.get_white();
				this.lblPeriod.TextColor = Color.get_white();
			}
		}

		private void OnActivateBoosterClicked(object prm)
		{
			// 
			// Current member / type: System.Void NovaShop/NovaShopScrollerItem::OnActivateBoosterClicked(System.Object)
			// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
			// 
			// Product version: 2017.2.502.1
			// Exception in: System.Void OnActivateBoosterClicked(System.Object)
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

		private void OnGetNovaClicked(object prm)
		{
			if (!playWebGame.isWebPlayer)
			{
				Application.OpenURL(playWebGame.authorization.url_payments);
				return;
			}
			if (playWebGame.GAME_TYPE != "ru")
			{
				Application.ExternalCall("openPayments", new object[0]);
				return;
			}
			Application.ExternalCall("sendNotification", new object[] { "openPayments", 1 });
		}

		private void OnHover(object prm, bool state)
		{
			if (!state)
			{
				this.hoverFrame.SetTextureKeepSize("FrameworkGUI", "empty");
				if (this.scrl == null || this.wnd != null)
				{
					this.wnd.RemoveGuiElement(this.btnActivete);
				}
				else
				{
					this.scrl.RemoveContent(this.btnActivete);
				}
				this.lblNoMoney.text = string.Empty;
			}
			else
			{
				this.hoverFrame.SetTextureKeepSize("NewGUI", "novashop_frame_booster_hover");
				this.btnActivete = new GuiButtonResizeable();
				this.btnActivete.boundries.set_x(this.mainFrame.X + 60f);
				this.btnActivete.boundries.set_y(this.mainFrame.Y + 40f);
				this.btnActivete.boundries.set_width(150f);
				if (!this.isBooster)
				{
					this.btnActivete.Caption = StaticData.Translate("key_nova_shop_item_btn_buy").ToUpper();
				}
				else
				{
					this.btnActivete.Caption = StaticData.Translate("key_nova_shop_item_btn_activate").ToUpper();
				}
				this.btnActivete.FontSize = 16;
				this.btnActivete.Alignment = 4;
				this.btnActivete.SetOrangeTexture();
				if (!this.hasMoney)
				{
					this.btnActivete.Caption = StaticData.Translate("key_nova_shop_item_btn_get_nova").ToUpper();
					this.lblNoMoney.text = StaticData.Translate("key_nova_shop_item_not_enought");
					this.btnActivete.SetBlueTexture();
				}
				if (this.scrl == null || this.wnd != null)
				{
					this.wnd.AddGuiElement(this.btnActivete);
				}
				else
				{
					this.scrl.AddContent(this.btnActivete);
				}
			}
		}
	}
}