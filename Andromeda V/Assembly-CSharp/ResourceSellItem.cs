using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSellItem
{
	private GuiLabel lblResName;

	private GuiLabel lblSelectedQty;

	private GuiLabel lblPrice;

	private GuiLabel lblTotalQty;

	private GuiTexture hexagonTx;

	private GuiTexture resourceTx;

	private GuiTexture shadowTx;

	private GuiTexture sliderLineTx;

	private GuiButtonFixed btnPlus;

	private GuiButtonFixed btnMinus;

	private GuiHorizontalSlider sliderQty;

	private ushort resourceId;

	private int maxQty;

	public ResourceSellItem()
	{
	}

	public void CreateSellItem(ushort resId, float possitionX, float possitionY, GuiWindow wnd)
	{
		this.resourceId = resId;
		if (resId != PlayerItems.TypeEquilibrium)
		{
			this.maxQty = (int)NetworkScript.player.playerBelongings.playerItems.GetMinetalQty(resId);
		}
		else
		{
			this.maxQty = (int)NetworkScript.player.playerBelongings.playerItems.GetAmountAt(resId);
		}
		this.lblResName = new GuiLabel()
		{
			boundries = new Rect(possitionX + 122f, possitionY + 22f, 105f, 35f),
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 3,
			FontSize = 14,
			text = StaticData.Translate(StaticData.allTypes.get_Item(resId).uiName)
		};
		wnd.AddGuiElement(this.lblResName);
		this.hexagonTx = new GuiTexture()
		{
			boundries = new Rect(possitionX + 47f, possitionY + 3f, 72f, 71f)
		};
		this.hexagonTx.SetTextureKeepSize("FusionWindow", "FusionHexagonBlue");
		this.hexagonTx.hoverParam = resId;
		this.hexagonTx.Hovered = new Action<object, bool>(null, __InBaseResourceTrader.OnResHover);
		wnd.AddGuiElement(this.hexagonTx);
		this.resourceTx = new GuiTexture()
		{
			boundries = new Rect(possitionX + 52f, possitionY + 8f, 62f, 62f)
		};
		this.resourceTx.SetTextureKeepSize("MineralsAvatars", StaticData.allTypes.get_Item(resId).assetName);
		wnd.AddGuiElement(this.resourceTx);
		this.shadowTx = new GuiTexture()
		{
			boundries = new Rect(possitionX + 47f, possitionY + 3f, 72f, 71f)
		};
		this.shadowTx.SetTextureKeepSize("NewGUI", "ResourceTradeHexagonShadow");
		wnd.AddGuiElement(this.shadowTx);
		this.sliderLineTx = new GuiTexture()
		{
			boundries = new Rect(possitionX + 28f, possitionY + 81f, 108f, 3f)
		};
		this.sliderLineTx.SetTextureKeepSize("NewGUI", "ResourceSlideLine");
		wnd.AddGuiElement(this.sliderLineTx);
		this.btnMinus = new GuiButtonFixed();
		this.btnMinus.SetTexture("NewGUI", "btnSliderMinus");
		this.btnMinus.X = possitionX + 2f;
		this.btnMinus.Y = possitionY + 75f;
		this.btnMinus.Caption = string.Empty;
		this.btnMinus.Clicked = new Action<EventHandlerParam>(this, ResourceSellItem.OnMinusBtnClicked);
		this.btnMinus.SetLeftClickSound("Sounds", "minus");
		wnd.AddGuiElement(this.btnMinus);
		this.btnPlus = new GuiButtonFixed();
		this.btnPlus.SetTexture("NewGUI", "btnSliderPlus");
		this.btnPlus.X = possitionX + 136f;
		this.btnPlus.Y = possitionY + 75f;
		this.btnPlus.Caption = string.Empty;
		this.btnPlus.Clicked = new Action<EventHandlerParam>(this, ResourceSellItem.OnPlusBtnClicked);
		this.btnPlus.SetLeftClickSound("Sounds", "plus");
		wnd.AddGuiElement(this.btnPlus);
		this.sliderQty = new GuiHorizontalSlider()
		{
			MAX = (float)this.maxQty,
			MIN = 0f,
			Width = 86f,
			CurrentValue = 0f,
			X = possitionX + 39f,
			Y = possitionY + 78f
		};
		this.sliderQty.SetSliderTumb("NewGUI", "resSliderThumb");
		this.sliderQty.SetEmptySliderTexture();
		this.sliderQty.refreshData = new Action(this, ResourceSellItem.RefreshSlider);
		wnd.AddGuiElement(this.sliderQty);
		this.lblSelectedQty = new GuiLabel()
		{
			boundries = new Rect(possitionX + 28f, possitionY + 91f, 108f, 12f),
			Alignment = 4,
			text = string.Format(StaticData.Translate("key_resource_trader_resource_amount"), this.sliderQty.CurrentValueInt)
		};
		wnd.AddGuiElement(this.lblSelectedQty);
		this.lblPrice = new GuiLabel()
		{
			boundries = new Rect(possitionX + 28f, possitionY + 104f, 108f, 12f),
			Alignment = 4,
			TextColor = GuiNewStyleBar.orangeColor,
			text = string.Format(StaticData.Translate("key_resource_trader_resource_cash"), this.sliderQty.CurrentValueInt * PlayerItems.CalculateSellPrice(this.resourceId, NetworkScript.player.cfg.sellBonus))
		};
		wnd.AddGuiElement(this.lblPrice);
		this.lblTotalQty = new GuiLabel()
		{
			boundries = new Rect(possitionX + 48f, possitionY + 32f, 70f, 15f),
			Alignment = 4,
			text = this.maxQty.ToString("##,##0")
		};
		wnd.AddGuiElement(this.lblTotalQty);
	}

	public int CurentAmount()
	{
		return this.sliderQty.CurrentValueInt;
	}

	public int CurentSellPrice()
	{
		return this.sliderQty.CurrentValueInt * PlayerItems.CalculateSellPrice(this.resourceId, NetworkScript.player.cfg.sellBonus);
	}

	private void OnMinusBtnClicked(object prm)
	{
		if (this.sliderQty.MAX - this.sliderQty.MIN >= 10f)
		{
			GuiHorizontalSlider currentValue = this.sliderQty;
			currentValue.CurrentValue = currentValue.CurrentValue - (this.sliderQty.MAX - this.sliderQty.MIN) / 10f;
		}
		else
		{
			GuiHorizontalSlider guiHorizontalSlider = this.sliderQty;
			guiHorizontalSlider.CurrentValue = guiHorizontalSlider.CurrentValue - 1f;
		}
		if (this.sliderQty.CurrentValue < this.sliderQty.MIN)
		{
			this.sliderQty.CurrentValue = this.sliderQty.MIN;
		}
	}

	private void OnPlusBtnClicked(object prm)
	{
		if (this.sliderQty.MAX - this.sliderQty.MIN >= 10f)
		{
			GuiHorizontalSlider currentValue = this.sliderQty;
			currentValue.CurrentValue = currentValue.CurrentValue + (this.sliderQty.MAX - this.sliderQty.MIN) / 10f;
		}
		else
		{
			GuiHorizontalSlider guiHorizontalSlider = this.sliderQty;
			guiHorizontalSlider.CurrentValue = guiHorizontalSlider.CurrentValue + 1f;
		}
		if (this.sliderQty.CurrentValue > this.sliderQty.MAX)
		{
			this.sliderQty.CurrentValue = this.sliderQty.MAX;
		}
	}

	private void PopulateInfo()
	{
		this.lblSelectedQty.text = string.Format(StaticData.Translate("key_resource_trader_resource_amount"), this.sliderQty.CurrentValueInt);
		this.lblPrice.text = string.Format(StaticData.Translate("key_resource_trader_resource_cash"), this.sliderQty.CurrentValueInt * PlayerItems.CalculateSellPrice(this.resourceId, NetworkScript.player.cfg.sellBonus));
	}

	public void RefreshData()
	{
		if (this.resourceId != PlayerItems.TypeEquilibrium)
		{
			this.maxQty = (int)NetworkScript.player.playerBelongings.playerItems.GetMinetalQty(this.resourceId);
		}
		else
		{
			this.maxQty = (int)NetworkScript.player.playerBelongings.playerItems.GetAmountAt(this.resourceId);
		}
		this.lblTotalQty.text = this.maxQty.ToString("##,##0");
		this.sliderQty.CurrentValue = 0f;
		this.sliderQty.MAX = (float)this.maxQty;
	}

	private void RefreshSlider()
	{
		this.PopulateInfo();
		__InBaseResourceTrader.PopulateData();
	}

	public void SetMaxValue()
	{
		this.sliderQty.CurrentValue = this.sliderQty.MAX;
	}

	public int TotalAmount()
	{
		return this.maxQty;
	}

	public int TotalSellPrice()
	{
		return this.maxQty * PlayerItems.CalculateSellPrice(this.resourceId, NetworkScript.player.cfg.sellBonus);
	}
}