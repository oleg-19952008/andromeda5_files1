using System;
using System.Collections.Generic;
using UnityEngine;

public class __InBaseResourceTrader : GuiWindow
{
	private static ResourceSellItem hydrogen;

	private static ResourceSellItem oxygen;

	private static ResourceSellItem carbon;

	private static ResourceSellItem water;

	private static ResourceSellItem methyl;

	private static ResourceSellItem carbonDioxide;

	private static ResourceSellItem deuterium;

	private static ResourceSellItem acetone;

	private static ResourceSellItem equilibrium;

	private static GuiLabel lblSell;

	private static GuiLabel lblSellAll;

	private static GuiLabel lblInfoBoxDescription;

	private static GuiLabel lblResourceName;

	private static GuiLabel lblResourcePrice;

	private GuiLabel lblCargo;

	private GuiBar barCargo;

	private static GuiButtonFixed btnSell;

	private static GuiButtonFixed btnSellAll;

	private static GuiTexture txResource;

	private static string defoult;

	static __InBaseResourceTrader()
	{
		__InBaseResourceTrader.defoult = StaticData.Translate("key_resource_trader_info_default");
	}

	public __InBaseResourceTrader()
	{
	}

	private static int CalculateCurentPrice()
	{
		return __InBaseResourceTrader.hydrogen.CurentSellPrice() + __InBaseResourceTrader.oxygen.CurentSellPrice() + __InBaseResourceTrader.carbon.CurentSellPrice() + __InBaseResourceTrader.water.CurentSellPrice() + __InBaseResourceTrader.methyl.CurentSellPrice() + __InBaseResourceTrader.carbonDioxide.CurentSellPrice() + __InBaseResourceTrader.deuterium.CurentSellPrice() + __InBaseResourceTrader.acetone.CurentSellPrice() + __InBaseResourceTrader.equilibrium.CurentSellPrice();
	}

	private static int CalculateTotalPrice()
	{
		return __InBaseResourceTrader.hydrogen.TotalSellPrice() + __InBaseResourceTrader.oxygen.TotalSellPrice() + __InBaseResourceTrader.carbon.TotalSellPrice() + __InBaseResourceTrader.water.TotalSellPrice() + __InBaseResourceTrader.methyl.TotalSellPrice() + __InBaseResourceTrader.carbonDioxide.TotalSellPrice() + __InBaseResourceTrader.deuterium.TotalSellPrice() + __InBaseResourceTrader.acetone.TotalSellPrice();
	}

	public override void Create()
	{
		base.SetBackgroundTexture("NewGUI", "ResourceTradeBackground");
		base.PutToCenter();
		this.zOrder = 210;
		this.isHidden = false;
		GuiLabel guiLabel = new GuiLabel()
		{
			text = StaticData.Translate("key_resource_trader_lbl"),
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 18,
			Font = GuiLabel.FontBold,
			boundries = new Rect(280f, 42f, 540f, 35f),
			Alignment = 1
		};
		base.AddGuiElement(guiLabel);
		__InBaseResourceTrader.btnSell = new GuiButtonFixed();
		__InBaseResourceTrader.btnSell.SetTexture("NewGUI", "ResourceTradeButton1");
		__InBaseResourceTrader.btnSell.X = 377f;
		__InBaseResourceTrader.btnSell.Y = 482f;
		__InBaseResourceTrader.btnSell.Caption = StaticData.Translate("key_resource_trader_sell").ToUpper();
		__InBaseResourceTrader.btnSell.textColorHover = Color.get_white();
		__InBaseResourceTrader.btnSell.textColorNormal = GuiNewStyleBar.blueColor;
		__InBaseResourceTrader.btnSell.textColorDisabled = Color.get_grey();
		__InBaseResourceTrader.btnSell.Alignment = 4;
		__InBaseResourceTrader.btnSell.Clicked = new Action<EventHandlerParam>(this, __InBaseResourceTrader.OnSellBtnClicked);
		base.AddGuiElement(__InBaseResourceTrader.btnSell);
		__InBaseResourceTrader.btnSellAll = new GuiButtonFixed();
		__InBaseResourceTrader.btnSellAll.SetTexture("NewGUI", "ResourceTradeButton2");
		__InBaseResourceTrader.btnSellAll.X = 566f;
		__InBaseResourceTrader.btnSellAll.Y = 482f;
		__InBaseResourceTrader.btnSellAll.Caption = StaticData.Translate("key_resource_trader_sell_all").ToUpper();
		__InBaseResourceTrader.btnSellAll.textColorHover = Color.get_white();
		__InBaseResourceTrader.btnSellAll.textColorNormal = GuiNewStyleBar.blueColor;
		__InBaseResourceTrader.btnSellAll.textColorDisabled = Color.get_grey();
		__InBaseResourceTrader.btnSellAll.Alignment = 4;
		__InBaseResourceTrader.btnSellAll.Clicked = new Action<EventHandlerParam>(this, __InBaseResourceTrader.OnSellAllBtnClicked);
		base.AddGuiElement(__InBaseResourceTrader.btnSellAll);
		__InBaseResourceTrader.lblSell = new GuiLabel()
		{
			boundries = new Rect(214f, 509f, 155f, 19f),
			text = string.Empty,
			FontSize = 12,
			Alignment = 4,
			TextColor = GuiNewStyleBar.orangeColor
		};
		base.AddGuiElement(__InBaseResourceTrader.lblSell);
		__InBaseResourceTrader.lblSellAll = new GuiLabel()
		{
			boundries = new Rect(731f, 509f, 155f, 19f),
			text = string.Empty,
			FontSize = 12,
			Alignment = 4,
			TextColor = GuiNewStyleBar.orangeColor
		};
		base.AddGuiElement(__InBaseResourceTrader.lblSellAll);
		__InBaseResourceTrader.hydrogen = new ResourceSellItem();
		__InBaseResourceTrader.hydrogen.CreateSellItem(PlayerItems.TypeHydrogen, 225f, 86f, this);
		__InBaseResourceTrader.oxygen = new ResourceSellItem();
		__InBaseResourceTrader.oxygen.CreateSellItem(PlayerItems.TypeOxygen, 445f, 86f, this);
		__InBaseResourceTrader.carbon = new ResourceSellItem();
		__InBaseResourceTrader.carbon.CreateSellItem(PlayerItems.TypeCarbon, 665f, 86f, this);
		__InBaseResourceTrader.water = new ResourceSellItem();
		__InBaseResourceTrader.water.CreateSellItem(PlayerItems.TypeWater, 225f, 217f, this);
		__InBaseResourceTrader.methyl = new ResourceSellItem();
		__InBaseResourceTrader.methyl.CreateSellItem(PlayerItems.TypeMetyl, 445f, 217f, this);
		__InBaseResourceTrader.carbonDioxide = new ResourceSellItem();
		__InBaseResourceTrader.carbonDioxide.CreateSellItem(PlayerItems.TypeCarbonDioxide, 665f, 217f, this);
		__InBaseResourceTrader.deuterium = new ResourceSellItem();
		__InBaseResourceTrader.deuterium.CreateSellItem(PlayerItems.TypeDeuterium, 225f, 348f, this);
		__InBaseResourceTrader.acetone = new ResourceSellItem();
		__InBaseResourceTrader.acetone.CreateSellItem(PlayerItems.TypeAceton, 445f, 348f, this);
		__InBaseResourceTrader.equilibrium = new ResourceSellItem();
		__InBaseResourceTrader.equilibrium.CreateSellItem(PlayerItems.TypeEquilibrium, 665f, 348f, this);
		__InBaseResourceTrader.txResource = new GuiTexture()
		{
			boundries = new Rect(67f, 109f, 100f, 100f)
		};
		base.AddGuiElement(__InBaseResourceTrader.txResource);
		__InBaseResourceTrader.lblInfoBoxDescription = new GuiLabel()
		{
			boundries = new Rect(32f, 330f, 170f, 200f),
			FontSize = 12,
			Alignment = 1
		};
		base.AddGuiElement(__InBaseResourceTrader.lblInfoBoxDescription);
		__InBaseResourceTrader.lblResourcePrice = new GuiLabel()
		{
			boundries = new Rect(65f, 215f, 100f, 25f),
			Alignment = 4,
			FontSize = 13,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor
		};
		base.AddGuiElement(__InBaseResourceTrader.lblResourcePrice);
		__InBaseResourceTrader.lblResourceName = new GuiLabel()
		{
			boundries = new Rect(43f, 76f, 150f, 30f),
			Alignment = 4,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 15,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(__InBaseResourceTrader.lblResourceName);
		this.lblCargo = new GuiLabel()
		{
			boundries = new Rect(35f, 255f, 160f, 20f),
			text = string.Empty,
			Alignment = 3,
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 12
		};
		base.AddGuiElement(this.lblCargo);
		this.barCargo = new GuiBar()
		{
			boundries = new Rect(35f, 275f, 160f, 20f),
			current = (float)NetworkScript.player.playerBelongings.playerItems.get_Cargo(),
			maximum = (float)NetworkScript.player.cfg.cargoMax,
			fillColor = GuiNewStyleBar.orangeColor
		};
		base.AddGuiElement(this.barCargo);
		this.SetCargoText();
		__InBaseResourceTrader.PopulateData();
		__InBaseResourceTrader.OnResHover((ushort)0, false);
	}

	public static void OnResHover(object parm, bool state)
	{
		ushort num = (ushort)parm;
		if (!state)
		{
			__InBaseResourceTrader.txResource.SetTextureKeepSize("MineralsAvatars", "DefaultMineral");
			__InBaseResourceTrader.lblResourceName.text = string.Empty;
			__InBaseResourceTrader.lblResourcePrice.text = string.Empty;
			__InBaseResourceTrader.lblInfoBoxDescription.text = __InBaseResourceTrader.defoult;
		}
		else
		{
			__InBaseResourceTrader.txResource.SetTextureKeepSize("MineralsAvatars", StaticData.allTypes.get_Item(num).assetName);
			__InBaseResourceTrader.lblResourceName.text = StaticData.Translate(StaticData.allTypes.get_Item(num).uiName);
			__InBaseResourceTrader.lblResourcePrice.text = string.Format(StaticData.Translate("key_resource_trader_resource_price"), PlayerItems.CalculateSellPrice(num, NetworkScript.player.cfg.sellBonus));
			__InBaseResourceTrader.lblInfoBoxDescription.text = StaticData.Translate(StaticData.allTypes.get_Item(num).description);
		}
	}

	private void OnSellAllBtnClicked(object prm)
	{
		// 
		// Current member / type: System.Void __InBaseResourceTrader::OnSellAllBtnClicked(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnSellAllBtnClicked(System.Object)
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

	private void OnSellBtnClicked(object prm)
	{
		// 
		// Current member / type: System.Void __InBaseResourceTrader::OnSellBtnClicked(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnSellBtnClicked(System.Object)
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

	private void PopulateAfterDeal()
	{
		__InBaseResourceTrader.hydrogen.RefreshData();
		__InBaseResourceTrader.oxygen.RefreshData();
		__InBaseResourceTrader.carbon.RefreshData();
		__InBaseResourceTrader.water.RefreshData();
		__InBaseResourceTrader.methyl.RefreshData();
		__InBaseResourceTrader.carbonDioxide.RefreshData();
		__InBaseResourceTrader.deuterium.RefreshData();
		__InBaseResourceTrader.acetone.RefreshData();
		__InBaseResourceTrader.equilibrium.RefreshData();
		__InBaseResourceTrader.PopulateData();
		__InBaseResourceTrader.btnSell.isEnabled = (__InBaseResourceTrader.CalculateCurentPrice() > 0 ? true : false);
		__InBaseResourceTrader.btnSellAll.isEnabled = (__InBaseResourceTrader.CalculateTotalPrice() > 0 ? true : false);
		this.SetCargoText();
	}

	public static void PopulateData()
	{
		__InBaseResourceTrader.lblSell.text = string.Format(StaticData.Translate("key_resource_trader_sell_price"), __InBaseResourceTrader.CalculateCurentPrice());
		__InBaseResourceTrader.lblSellAll.text = string.Format(StaticData.Translate("key_resource_trader_sell_price"), __InBaseResourceTrader.CalculateTotalPrice());
		__InBaseResourceTrader.btnSell.isEnabled = (__InBaseResourceTrader.CalculateCurentPrice() > 0 ? true : false);
		__InBaseResourceTrader.btnSellAll.isEnabled = (__InBaseResourceTrader.CalculateTotalPrice() > 0 ? true : false);
	}

	private void SetCargoText()
	{
		this.lblCargo.text = string.Format(StaticData.Translate("key_fushion_cargo_status"), NetworkScript.player.playerBelongings.playerItems.get_Cargo(), NetworkScript.player.cfg.cargoMax);
		if (this.barCargo != null)
		{
			this.barCargo.current = (float)NetworkScript.player.playerBelongings.playerItems.get_Cargo();
		}
	}
}