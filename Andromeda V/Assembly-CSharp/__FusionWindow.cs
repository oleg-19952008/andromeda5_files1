using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class __FusionWindow : GuiWindow
{
	public GuiButton btnCompound;

	public GuiButton btnAmmo;

	private GuiTexture mainScreenBG;

	public GuiTexture txAvatar;

	public GuiLabel lbl_Lelevl1;

	public GuiLabel lbl_Lelevl2;

	public GuiLabel lbl_Lelevl3;

	public GuiLabel lbl_InfoBoxTitle;

	public GuiLabel lbl_InfoBoxValue;

	public GuiLabel lbl_ResName;

	public GuiLabel lbl_Price;

	public GuiLabel lbl_label;

	private GuiLabel lblCargo;

	public List<GuiElement> forDelete;

	private __FusionWindow.FusionTab selectedTab;

	public SortedList<ushort, __ResourceHex> res = new SortedList<ushort, __ResourceHex>();

	public GuiBar barCargo;

	private GuiLabel lblFormulaTitle;

	public List<GuiElement> formulaStuff = new List<GuiElement>();

	public GuiLabel lblFormulaError;

	public GuiLabel lblValue;

	public GuiLabel lblMaxFormula;

	public SortedList<uint, GuiLabel> dependAmountLabels = new SortedList<uint, GuiLabel>();

	private SortedList<uint, int> depAmountLabelX = new SortedList<uint, int>();

	private SortedList<uint, int> depAmountLabelY = new SortedList<uint, int>();

	public List<GuiTexture> fuseLines = new List<GuiTexture>();

	public __FusionWindow()
	{
		this.res.Add(PlayerItems.TypeHydrogen, new __ResourceHex(PlayerItems.TypeHydrogen, new Vector2(221f, 177f), this));
		this.res.Add(PlayerItems.TypeCarbon, new __ResourceHex(PlayerItems.TypeCarbon, new Vector2(221f, 282f), this));
		this.res.Add(PlayerItems.TypeOxygen, new __ResourceHex(PlayerItems.TypeOxygen, new Vector2(221f, 390f), this));
		this.res.Add(PlayerItems.TypeMetyl, new __ResourceHex(PlayerItems.TypeMetyl, new Vector2(440f, 177f), this));
		this.res.Add(PlayerItems.TypeWater, new __ResourceHex(PlayerItems.TypeWater, new Vector2(440f, 282f), this));
		this.res.Add(PlayerItems.TypeCarbonDioxide, new __ResourceHex(PlayerItems.TypeCarbonDioxide, new Vector2(440f, 390f), this));
		this.res.Add(PlayerItems.TypeAceton, new __ResourceHex(PlayerItems.TypeAceton, new Vector2(659f, 390f), this));
		this.res.Add(PlayerItems.TypeDeuterium, new __ResourceHex(PlayerItems.TypeDeuterium, new Vector2(659f, 177f), this));
		this.res.Add(PlayerItems.TypeEquilibrium, new __ResourceHex(PlayerItems.TypeEquilibrium, new Vector2(659f, 282f), this));
		this.depAmountLabelX.Add(PlayerItems.TypeMetyl | PlayerItems.TypeHydrogen << 16, 348);
		this.depAmountLabelY.Add(PlayerItems.TypeMetyl | PlayerItems.TypeHydrogen << 16, 182);
		this.depAmountLabelX.Add(PlayerItems.TypeMetyl | PlayerItems.TypeCarbon << 16, 348);
		this.depAmountLabelY.Add(PlayerItems.TypeMetyl | PlayerItems.TypeCarbon << 16, 254);
		this.depAmountLabelX.Add(PlayerItems.TypeWater | PlayerItems.TypeHydrogen << 16, 348);
		this.depAmountLabelY.Add(PlayerItems.TypeWater | PlayerItems.TypeHydrogen << 16, 242);
		this.depAmountLabelX.Add(PlayerItems.TypeWater | PlayerItems.TypeOxygen << 16, 348);
		this.depAmountLabelY.Add(PlayerItems.TypeWater | PlayerItems.TypeOxygen << 16, 360);
		this.depAmountLabelX.Add(PlayerItems.TypeCarbonDioxide | PlayerItems.TypeCarbon << 16, 348);
		this.depAmountLabelY.Add(PlayerItems.TypeCarbonDioxide | PlayerItems.TypeCarbon << 16, 346);
		this.depAmountLabelX.Add(PlayerItems.TypeCarbonDioxide | PlayerItems.TypeOxygen << 16, 348);
		this.depAmountLabelY.Add(PlayerItems.TypeCarbonDioxide | PlayerItems.TypeOxygen << 16, 449);
		this.depAmountLabelX.Add(PlayerItems.TypeAceton | PlayerItems.TypeMetyl << 16, 622);
		this.depAmountLabelY.Add(PlayerItems.TypeAceton | PlayerItems.TypeMetyl << 16, 341);
		this.depAmountLabelX.Add(PlayerItems.TypeAceton | PlayerItems.TypeCarbonDioxide << 16, 622);
		this.depAmountLabelY.Add(PlayerItems.TypeAceton | PlayerItems.TypeCarbonDioxide << 16, 451);
		this.depAmountLabelX.Add(PlayerItems.TypeEquilibrium | PlayerItems.TypeAceton << 16, 677);
		this.depAmountLabelY.Add(PlayerItems.TypeEquilibrium | PlayerItems.TypeAceton << 16, 371);
		this.depAmountLabelX.Add(PlayerItems.TypeEquilibrium | PlayerItems.TypeDeuterium << 16, 677);
		this.depAmountLabelY.Add(PlayerItems.TypeEquilibrium | PlayerItems.TypeDeuterium << 16, 256);
	}

	public void AfterFuse(ushort resType)
	{
		__FusionWindow.<AfterFuse>c__AnonStorey8F variable = null;
		SortedList<ushort, short> item = PlayerItems.fusionDependancies.get_Item(resType);
		IEnumerator<ushort> enumerator = item.get_Keys().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				ushort current = enumerator.get_Current();
				long minetalQty = NetworkScript.player.playerBelongings.playerItems.GetMinetalQty(current);
				bool flag = minetalQty >= (long)item.get_Item(current);
				__ResourceHex str = this.res.get_Item(current);
				str.lbl_qty.text = minetalQty.ToString("###,##0");
				GuiLabel _red = this.dependAmountLabels.get_Item(resType | current << 16);
				if (!flag)
				{
					_red.TextColor = Color.get_red();
					str.SetRedFrame();
				}
				else
				{
					_red.TextColor = Color.get_white();
					str.SetWhiteFrame();
				}
			}
		}
		finally
		{
			if (enumerator == null)
			{
			}
			enumerator.Dispose();
		}
		List<ushort> list = new List<ushort>();
		IEnumerator<ushort> enumerator1 = PlayerItems.fusionDependancies.get_Keys().GetEnumerator();
		try
		{
			while (enumerator1.MoveNext())
			{
				ushort num = enumerator1.get_Current();
				if (Enumerable.Count<ushort>(Enumerable.Where<ushort>(PlayerItems.fusionDependancies.get_Item(num).get_Keys(), new Func<ushort, bool>(variable, (ushort c) => c == this.resType))) <= 0)
				{
					continue;
				}
				list.Add(num);
			}
		}
		finally
		{
			if (enumerator1 == null)
			{
			}
			enumerator1.Dispose();
		}
		foreach (ushort num1 in list)
		{
			if (PlayerItems.IsMineral(num1))
			{
				this.res.get_Item(num1).ReArrangeAfterAmountsChanged();
			}
		}
		__ResourceHex _ResourceHex = this.res.get_Item(resType);
		GuiLabel lblQty = _ResourceHex.lbl_qty;
		long minetalQty1 = NetworkScript.player.playerBelongings.playerItems.GetMinetalQty(resType);
		lblQty.text = minetalQty1.ToString("###,##0");
		if (NetworkScript.player.playerBelongings.playerItems.MaxSynthesis(resType, NetworkScript.player.cfg.fusionPriceOff) == 0)
		{
			base.RemoveGuiElement(_ResourceHex.btnFuseOne);
			base.RemoveGuiElement(_ResourceHex.btnFuseMax);
			_ResourceHex.CreateUnclickableFrame();
		}
		this.SetCargoText();
		this.RefreshMaxButtons();
	}

	public void AfterFuseMax(ushort resType)
	{
		this.AfterFuse(resType);
		this.OnResHover(resType, false);
	}

	private new void Clear()
	{
		foreach (GuiElement guiElement in this.forDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.forDelete.Clear();
		__FusionWindow.FusionTab fusionTab = this.selectedTab;
		if (fusionTab == __FusionWindow.FusionTab.CompoundTab)
		{
			IEnumerator<__ResourceHex> enumerator = this.res.get_Values().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					enumerator.get_Current().ClearGui(this);
				}
			}
			finally
			{
				if (enumerator == null)
				{
				}
				enumerator.Dispose();
			}
		}
		else if (fusionTab == __FusionWindow.FusionTab.AmmoCreateTab)
		{
			__AmmoFactory.Clear();
		}
		this.RemoveFormulaStuff();
		this.txAvatar.SetTextureKeepSize("MineralsAvatars", "DefaultMineral");
		this.lbl_ResName.text = string.Empty;
		this.lbl_InfoBoxValue.text = StaticData.Translate("key_fushion_compound_infobox_default");
		this.lblValue.text = string.Empty;
	}

	public override void Create()
	{
		this.forDelete = new List<GuiElement>();
		base.SetBackgroundTexture("FusionWindow", "FusionBackGround");
		base.PutToCenter();
		this.zOrder = 210;
		this.mainScreenBG = new GuiTexture()
		{
			boundries = new Rect(210f, 69f, 681f, 467f)
		};
		this.mainScreenBG.SetTextureKeepSize("FusionWindow", "FusionTab1");
		base.AddGuiElement(this.mainScreenBG);
		this.lbl_InfoBoxValue = new GuiLabel()
		{
			boundries = new Rect(35f, 330f, 160f, 195f),
			FontSize = 12
		};
		base.AddGuiElement(this.lbl_InfoBoxValue);
		this.lbl_label = new GuiLabel()
		{
			text = StaticData.Translate("key_fushion_fushion").ToUpper(),
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 18,
			Font = GuiLabel.FontBold,
			boundries = new Rect(280f, 42f, 540f, 35f),
			Alignment = 1
		};
		base.AddGuiElement(this.lbl_label);
		this.lbl_InfoBoxTitle = new GuiLabel()
		{
			boundries = new Rect(22f, 300f, 82f, 20f),
			text = StaticData.Translate("key_fushion_infobox"),
			Alignment = 1,
			FontSize = 14,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.lbl_InfoBoxTitle);
		this.lblValue = new GuiLabel()
		{
			boundries = new Rect(27f, 215f, 175f, 32f),
			text = StaticData.Translate("key_fushion_value"),
			Alignment = 4,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 14
		};
		base.AddGuiElement(this.lblValue);
		this.lblCargo = new GuiLabel()
		{
			boundries = new Rect(35f, 255f, 160f, 20f),
			text = string.Empty
		};
		this.SetCargoText();
		this.lblCargo.Alignment = 3;
		this.lblCargo.TextColor = GuiNewStyleBar.orangeColor;
		this.lblCargo.FontSize = 12;
		base.AddGuiElement(this.lblCargo);
		this.barCargo = new GuiBar()
		{
			boundries = new Rect(35f, 275f, 160f, 20f),
			current = (float)NetworkScript.player.playerBelongings.playerItems.get_Cargo(),
			maximum = (float)NetworkScript.player.cfg.cargoMax,
			fillColor = GuiNewStyleBar.orangeColor
		};
		base.AddGuiElement(this.barCargo);
		this.lbl_ResName = new GuiLabel()
		{
			boundries = new Rect(27f, 73f, 175f, 32f),
			text = string.Empty,
			Alignment = 4,
			FontSize = 15,
			TextColor = GuiNewStyleBar.orangeColor,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(this.lbl_ResName);
		this.txAvatar = new GuiTexture()
		{
			boundries = new Rect(67f, 109f, 100f, 100f)
		};
		base.AddGuiElement(this.txAvatar);
		this.btnCompound = new GuiButton()
		{
			boundries = new Rect(205f, 50f, 150f, 70f),
			Alignment = 4,
			Caption = StaticData.Translate("key_fushion_compound"),
			textColorClick = GuiNewStyleBar.orangeColor,
			textColorNormal = GuiNewStyleBar.blueColor,
			textColorHover = Color.get_white(),
			FontSize = 13,
			behaviourKeepClicked = true,
			IsClicked = this.selectedTab == __FusionWindow.FusionTab.CompoundTab,
			Clicked = new Action<EventHandlerParam>(this, __FusionWindow.OnCompoundClick)
		};
		base.AddGuiElement(this.btnCompound);
		this.btnAmmo = new GuiButton()
		{
			boundries = new Rect(this.btnCompound.boundries.get_x() + 150f, this.btnCompound.boundries.get_y(), 130f, 70f),
			Alignment = 4,
			Caption = StaticData.Translate("key_fushion_ammo_creation"),
			textColorClick = GuiNewStyleBar.orangeColor,
			textColorNormal = GuiNewStyleBar.blueColor,
			textColorHover = Color.get_white(),
			FontSize = 13,
			behaviourKeepClicked = true,
			IsClicked = this.selectedTab == __FusionWindow.FusionTab.AmmoCreateTab,
			Clicked = new Action<EventHandlerParam>(this, __FusionWindow.OnAmmoCreationClick)
		};
		base.AddGuiElement(this.btnAmmo);
		this.isHidden = false;
		if (this.selectedTab != __FusionWindow.FusionTab.CompoundTab)
		{
			this.CreateAmmo();
		}
		else
		{
			this.CreateMineral();
		}
	}

	public void CreateAmmo()
	{
		this.mainScreenBG.SetTextureKeepSize("FusionWindow", "FusionTab2");
		__AmmoFactory.wnd = this;
		__AmmoFactory.Create();
	}

	public void CreateFormulaStuff(ushort resType)
	{
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(35f, 425f, 160f, 20f),
			text = StaticData.Translate("key_fushion_formula").ToUpper(),
			Alignment = 3,
			FontSize = 13,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		this.formulaStuff.Add(guiLabel);
		guiLabel = new GuiLabel()
		{
			boundries = new Rect(35f, 485f, 170f, 30f),
			text = string.Empty,
			Alignment = 0,
			FontSize = 13,
			TextColor = Color.get_red(),
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(guiLabel);
		this.formulaStuff.Add(guiLabel);
		this.lblFormulaError = guiLabel;
		int textWidth = 35;
		SortedList<ushort, short> item = PlayerItems.fusionDependancies.get_Item(resType);
		for (int i = 0; i < item.get_Count(); i++)
		{
			int num = 450;
			ushort item1 = item.get_Keys().get_Item(i);
			short num1 = item.get_Item(item1);
			PlayerItemTypesData playerItemTypesDatum = StaticData.allTypes.get_Item(item1);
			if (i > 0)
			{
				guiLabel = new GuiLabel()
				{
					text = "+",
					FontSize = 14,
					TextColor = Color.get_white(),
					boundries = new Rect((float)textWidth, (float)num, 50f, 32f),
					Alignment = 3,
					Y = (float)num
				};
				base.AddGuiElement(guiLabel);
				this.formulaStuff.Add(guiLabel);
				textWidth = textWidth + (int)(guiLabel.TextWidth + 4f);
			}
			guiLabel = new GuiLabel()
			{
				text = string.Concat(num1.ToString(), "x"),
				FontSize = 14,
				TextColor = Color.get_white(),
				Alignment = 3,
				boundries = new Rect((float)textWidth, (float)num, 50f, 32f)
			};
			base.AddGuiElement(guiLabel);
			this.formulaStuff.Add(guiLabel);
			textWidth = textWidth + (int)(guiLabel.TextWidth + 4f);
			GuiTexture guiTexture = new GuiTexture()
			{
				boundries = new Rect((float)textWidth, (float)num, 32f, 32f)
			};
			guiTexture.SetTextureKeepSize("MineralsAvatars", playerItemTypesDatum.assetName);
			base.AddGuiElement(guiTexture);
			this.formulaStuff.Add(guiTexture);
			textWidth = textWidth + 36;
		}
	}

	public void CreateMineral()
	{
		this.lbl_InfoBoxValue.text = StaticData.Translate("key_fushion_compound_infobox_default");
		this.txAvatar.boundries = new Rect(67f, 109f, 100f, 100f);
		this.txAvatar.SetTextureKeepSize("MineralsAvatars", "DefaultMineral");
		this.lblValue.text = string.Empty;
		this.lbl_Lelevl1 = new GuiLabel()
		{
			boundries = new Rect(210f, 110f, 220f, 35f),
			text = StaticData.Translate("key_fushion_compound_level_1").ToUpper(),
			Alignment = 4,
			FontSize = 13,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor
		};
		base.AddGuiElement(this.lbl_Lelevl1);
		this.forDelete.Add(this.lbl_Lelevl1);
		this.lbl_Lelevl2 = new GuiLabel()
		{
			boundries = new Rect(this.lbl_Lelevl1.boundries.get_x() + 220f, 110f, 220f, 35f),
			text = StaticData.Translate("key_fushion_compound_level_2").ToUpper(),
			Alignment = 4,
			FontSize = 13,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor
		};
		base.AddGuiElement(this.lbl_Lelevl2);
		this.forDelete.Add(this.lbl_Lelevl2);
		this.lbl_Lelevl3 = new GuiLabel()
		{
			boundries = new Rect(this.lbl_Lelevl2.boundries.get_x() + 220f, 110f, 220f, 35f),
			text = StaticData.Translate("key_fushion_compound_level_3").ToUpper(),
			Alignment = 4,
			FontSize = 13,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor
		};
		base.AddGuiElement(this.lbl_Lelevl3);
		this.forDelete.Add(this.lbl_Lelevl3);
		IEnumerator<__ResourceHex> enumerator = this.res.get_Values().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				__ResourceHex current = enumerator.get_Current();
				current.btnFuseMax = null;
				current.CreateGui();
			}
		}
		finally
		{
			if (enumerator == null)
			{
			}
			enumerator.Dispose();
		}
		this.RefreshMaxButtons();
	}

	private void OnAmmoCreationClick(object prm)
	{
		this.Clear();
		__AmmoFactory.wnd = this;
		__AmmoFactory.AmmoTypeOnScreen = 0;
		this.selectedTab = __FusionWindow.FusionTab.AmmoCreateTab;
		if (this.mainScreenBG != null)
		{
			this.mainScreenBG.SetTextureKeepSize("FusionWindow", "FusionTab2");
			this.CreateAmmo();
		}
	}

	private void OnCompoundClick(object prm)
	{
		this.Clear();
		this.selectedTab = __FusionWindow.FusionTab.CompoundTab;
		if (this.mainScreenBG != null)
		{
			this.mainScreenBG.SetTextureKeepSize("FusionWindow", "FusionTab1");
			this.CreateMineral();
		}
	}

	public void OnResHover(object parm, bool state)
	{
		ushort num = (ushort)parm;
		SortedList<ushort, short> item = PlayerItems.fusionDependancies.get_Item(num);
		if (state)
		{
			PlayerItemTypesData playerItemTypesDatum = StaticData.allTypes.get_Item(num);
			if (this.res.get_Item(num).isFusable)
			{
				this.CreateFormulaStuff(num);
			}
			IEnumerator<ushort> enumerator = item.get_Keys().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					ushort current = enumerator.get_Current();
					int num1 = Math.Max((int)((float)item.get_Item(current) * (1f - (float)NetworkScript.player.cfg.fusionPriceOff / 100f)), 1);
					bool minetalQty = NetworkScript.player.playerBelongings.playerItems.GetMinetalQty(current) >= (long)num1;
					string str = null;
					str = (!minetalQty ? "Red" : "Wht");
					PlayerItemTypesData item1 = StaticData.allTypes.get_Item(current);
					GuiTexture guiTexture = new GuiTexture();
					guiTexture.SetTexture("FusionWindow", string.Concat(item1.assetName, playerItemTypesDatum.assetName, "Line", str));
					this.fuseLines.Add(guiTexture);
					this.forDelete.Add(guiTexture);
					base.AddGuiElement(guiTexture);
					GuiLabel guiLabel = new GuiLabel()
					{
						text = num1.ToString(),
						FontSize = 12
					};
					uint num2 = num | current << 16;
					guiLabel.boundries = new Rect((float)this.depAmountLabelX.get_Item(num2), (float)this.depAmountLabelY.get_Item(num2), 30f, 20f);
					if (this.dependAmountLabels.ContainsKey(num2))
					{
						base.RemoveGuiElement(this.dependAmountLabels.get_Item(num2));
						this.dependAmountLabels.Remove(num2);
					}
					this.dependAmountLabels.Add(num2, guiLabel);
					this.forDelete.Add(guiLabel);
					base.AddGuiElement(guiLabel);
					__ResourceHex _ResourceHex = this.res.get_Item(current);
					if (!minetalQty)
					{
						this.lblFormulaError.text = string.Format(StaticData.Translate("key_fushion_compound_formula_err"), StaticData.Translate(StaticData.allTypes.get_Item(current).uiName));
						guiLabel.TextColor = Color.get_red();
						_ResourceHex.SetRedFrame();
					}
					else
					{
						guiLabel.TextColor = Color.get_white();
						_ResourceHex.SetWhiteFrame();
					}
				}
			}
			finally
			{
				if (enumerator == null)
				{
				}
				enumerator.Dispose();
			}
			this.txAvatar.SetTextureKeepSize("MineralsAvatars", playerItemTypesDatum.assetName);
			this.lbl_ResName.text = StaticData.Translate(playerItemTypesDatum.uiName);
			this.lbl_InfoBoxValue.text = StaticData.Translate(PlayerItems.GetDescription(num));
			this.lblValue.text = string.Format(StaticData.Translate("key_fushion_compound_resoure_value"), StaticData.allTypes.get_Item(num).priceCash);
		}
		else
		{
			this.RemoveFormulaStuff();
			this.txAvatar.SetTextureKeepSize("MineralsAvatars", "DefaultMineral");
			this.lbl_ResName.text = string.Empty;
			this.lbl_InfoBoxValue.text = StaticData.Translate("key_fushion_compound_infobox_default");
			this.lblValue.text = string.Empty;
			IEnumerator<GuiLabel> enumerator1 = this.dependAmountLabels.get_Values().GetEnumerator();
			try
			{
				while (enumerator1.MoveNext())
				{
					GuiLabel current1 = enumerator1.get_Current();
					base.RemoveGuiElement(current1);
					this.forDelete.Remove(current1);
				}
			}
			finally
			{
				if (enumerator1 == null)
				{
				}
				enumerator1.Dispose();
			}
			this.dependAmountLabels.Clear();
			IEnumerator<ushort> enumerator2 = item.get_Keys().GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					ushort current2 = enumerator2.get_Current();
					this.res.get_Item(current2).SetBlueFrame();
				}
			}
			finally
			{
				if (enumerator2 == null)
				{
				}
				enumerator2.Dispose();
			}
			foreach (GuiTexture fuseLine in this.fuseLines)
			{
				base.RemoveGuiElement(fuseLine);
			}
			this.fuseLines.Clear();
		}
	}

	public void PopulateScreen()
	{
		this.SetCargoText();
		this.RemoveFormulaStuff();
		this.txAvatar.SetTextureKeepSize("MineralsAvatars", "DefaultMineral");
		this.lbl_ResName.text = string.Empty;
		this.lbl_InfoBoxValue.text = StaticData.Translate("key_fushion_compound_infobox_default");
		this.lblValue.text = string.Empty;
		IEnumerator<GuiLabel> enumerator = this.dependAmountLabels.get_Values().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				GuiLabel current = enumerator.get_Current();
				base.RemoveGuiElement(current);
				this.forDelete.Remove(current);
			}
		}
		finally
		{
			if (enumerator == null)
			{
			}
			enumerator.Dispose();
		}
		this.dependAmountLabels.Clear();
		foreach (GuiTexture fuseLine in this.fuseLines)
		{
			base.RemoveGuiElement(fuseLine);
		}
		this.fuseLines.Clear();
		IEnumerator<__ResourceHex> enumerator1 = this.res.get_Values().GetEnumerator();
		try
		{
			while (enumerator1.MoveNext())
			{
				enumerator1.get_Current().ReArrangeAfterAmountsChanged();
			}
		}
		finally
		{
			if (enumerator1 == null)
			{
			}
			enumerator1.Dispose();
		}
	}

	private void RefreshMaxButtons()
	{
		IEnumerator<__ResourceHex> enumerator = this.res.get_Values().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				__ResourceHex current = enumerator.get_Current();
				int num = NetworkScript.player.playerBelongings.playerItems.MaxSynthesis(current.resourceId, NetworkScript.player.cfg.fusionPriceOff);
				if (current.btnFuseMax != null)
				{
					if (num >= 1)
					{
						continue;
					}
					base.RemoveGuiElement(current.btnFuseMax);
					this.forDelete.Remove(current.btnFuseMax);
					current.btnFuseMax = null;
				}
				else if (num >= 1)
				{
					GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
					guiButtonFixed.SetTexture("FusionWindow", "btnFuseMax");
					guiButtonFixed.X = current.btnFuseOne.boundries.get_x() + 42f;
					guiButtonFixed.Y = current.btnFuseOne.boundries.get_y() + 50f;
					guiButtonFixed.Alignment = 3;
					guiButtonFixed.FontSize = 14;
					guiButtonFixed.MarginTop = -2;
					guiButtonFixed._marginLeft = 3;
					guiButtonFixed.textColorHover = GuiNewStyleBar.blueColor;
					guiButtonFixed.Caption = string.Format("+ {0}", num);
					EventHandlerParam eventHandlerParam = new EventHandlerParam()
					{
						customData = current.resourceId
					};
					guiButtonFixed.eventHandlerParam = eventHandlerParam;
					guiButtonFixed.hoverParam = current.resourceId;
					guiButtonFixed.Hovered = new Action<object, bool>(this, __FusionWindow.OnResHover);
					guiButtonFixed.Clicked = new Action<EventHandlerParam>(current, __ResourceHex.FuseMax);
					base.AddGuiElement(guiButtonFixed);
					current.btnFuseMax = guiButtonFixed;
				}
			}
		}
		finally
		{
			if (enumerator == null)
			{
			}
			enumerator.Dispose();
		}
	}

	public void RemoveFormulaStuff()
	{
		foreach (GuiElement guiElement in this.formulaStuff)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.lblMaxFormula = null;
	}

	public void SetCargoText()
	{
		this.lblCargo.text = string.Format(StaticData.Translate("key_fushion_cargo_status"), NetworkScript.player.playerBelongings.playerItems.get_Cargo(), NetworkScript.player.cfg.cargoMax);
		if (this.barCargo != null)
		{
			this.barCargo.current = (float)NetworkScript.player.playerBelongings.playerItems.get_Cargo();
		}
	}

	private enum FusionTab
	{
		CompoundTab,
		AmmoCreateTab
	}
}