using System;
using System.Collections.Generic;
using UnityEngine;

public class __AmmoFactory
{
	private static List<GuiElement> forDelete;

	private static SortedList<ushort, Vector2> ammoTypePossitions;

	private static GuiTextureAnimated fuseAnimation;

	public static __FusionWindow wnd;

	private static GuiTexture txEqu1Frame;

	private static GuiTexture txEqu2Frame;

	private static GuiTexture txEqu3Frame;

	private static GuiTexture txEqu4Frame;

	private static GuiTexture txMethylFrame;

	private static GuiTexture txWaterFrame;

	private static GuiTexture txAcetone1Frame;

	private static GuiTexture txAcetone2Frame;

	private static GuiButtonFixed btnSolar;

	private static GuiButtonFixed btnFusion;

	private static GuiButtonFixed btnColdFusion;

	private static GuiButtonFixed btnSulfurFusion;

	private static GuiTexture txLineEq1Solar;

	private static GuiTexture txLineEq2Fusion;

	private static GuiTexture txLineEq3FusionCold;

	private static GuiTexture txLineEq4FusionSulfur;

	private static GuiTexture txLineWaterSolar;

	private static GuiTexture txLineMethylFusion;

	private static GuiTexture txLineAceton1FusionCold;

	private static GuiTexture txLineAceton2FusionSulfur;

	public static ushort AmmoTypeOnScreen;

	private static float deltaTime;

	private static GuiLabel warningLbl;

	private static bool isFadeingOut;

	private static int blinkCnt;

	static __AmmoFactory()
	{
	}

	private __AmmoFactory()
	{
	}

	private static void CleanupOnHoverOut()
	{
	}

	public static void Clear()
	{
		foreach (GuiElement guiElement in __AmmoFactory.forDelete)
		{
			__AmmoFactory.wnd.RemoveGuiElement(guiElement);
		}
	}

	public static void Create()
	{
		__AmmoFactory.wnd.SetCargoText();
		__AmmoFactory.forDelete = new List<GuiElement>();
		__AmmoFactory.PutEmptyWarningLbl(__AmmoFactory.wnd);
		__AmmoFactory.wnd.lbl_InfoBoxValue.text = StaticData.Translate("key_fuishion_ammo_infobox_default");
		PlayerItemTypesData item = StaticData.allTypes.get_Item(PlayerItems.TypeMetyl);
		__AmmoFactory.txEqu1Frame = __AmmoFactory.PutResourceFrame(256, 131);
		__AmmoFactory.PutResourceTexture(__AmmoFactory.txEqu1Frame, item.assetName);
		__AmmoFactory.PutResourceNameLabel(__AmmoFactory.txEqu1Frame, StaticData.Translate(item.uiName));
		__AmmoFactory.PutResourceAmountLabel(__AmmoFactory.txEqu1Frame, NetworkScript.player.playerBelongings.playerItems.GetMinetalQty(PlayerItems.TypeMetyl));
		item = StaticData.allTypes.get_Item(PlayerItems.TypeWater);
		__AmmoFactory.txWaterFrame = __AmmoFactory.PutResourceFrame((int)__AmmoFactory.txEqu1Frame.X + 117, (int)__AmmoFactory.txEqu1Frame.Y);
		__AmmoFactory.PutResourceTexture(__AmmoFactory.txWaterFrame, item.assetName);
		__AmmoFactory.PutResourceNameLabel(__AmmoFactory.txWaterFrame, StaticData.Translate(item.uiName));
		__AmmoFactory.PutResourceAmountLabel(__AmmoFactory.txWaterFrame, NetworkScript.player.playerBelongings.playerItems.GetMinetalQty(PlayerItems.TypeWater));
		item = StaticData.allTypes.get_Item(PlayerItems.TypeEquilibrium);
		__AmmoFactory.txEqu2Frame = __AmmoFactory.PutResourceFrame((int)__AmmoFactory.txWaterFrame.X + 94, (int)__AmmoFactory.txEqu1Frame.Y);
		__AmmoFactory.PutResourceTexture(__AmmoFactory.txEqu2Frame, item.assetName);
		__AmmoFactory.PutResourceNameLabel(__AmmoFactory.txEqu2Frame, StaticData.Translate(item.uiName));
		__AmmoFactory.PutResourceAmountLabel(__AmmoFactory.txEqu2Frame, NetworkScript.player.playerBelongings.playerItems.GetAmountAt(PlayerItems.TypeEquilibrium));
		item = StaticData.allTypes.get_Item(PlayerItems.TypeMetyl);
		__AmmoFactory.txMethylFrame = __AmmoFactory.PutResourceFrame((int)__AmmoFactory.txEqu1Frame.X + 326, (int)__AmmoFactory.txEqu1Frame.Y);
		__AmmoFactory.PutResourceTexture(__AmmoFactory.txMethylFrame, item.assetName);
		__AmmoFactory.PutResourceNameLabel(__AmmoFactory.txMethylFrame, StaticData.Translate(item.uiName));
		__AmmoFactory.PutResourceAmountLabel(__AmmoFactory.txMethylFrame, NetworkScript.player.playerBelongings.playerItems.GetMinetalQty(PlayerItems.TypeMetyl));
		item = StaticData.allTypes.get_Item(PlayerItems.TypeEquilibrium);
		__AmmoFactory.txEqu3Frame = __AmmoFactory.PutResourceFrame((int)__AmmoFactory.txEqu1Frame.X + 413, (int)__AmmoFactory.txEqu1Frame.Y);
		__AmmoFactory.PutResourceTexture(__AmmoFactory.txEqu3Frame, item.assetName);
		__AmmoFactory.PutResourceNameLabel(__AmmoFactory.txEqu3Frame, StaticData.Translate(item.uiName));
		__AmmoFactory.PutResourceAmountLabel(__AmmoFactory.txEqu3Frame, NetworkScript.player.playerBelongings.playerItems.GetAmountAt(PlayerItems.TypeEquilibrium));
		item = StaticData.allTypes.get_Item(PlayerItems.TypeAceton);
		__AmmoFactory.txAcetone1Frame = __AmmoFactory.PutResourceFrame((int)__AmmoFactory.txEqu1Frame.X + 530, (int)__AmmoFactory.txEqu1Frame.Y);
		__AmmoFactory.PutResourceTexture(__AmmoFactory.txAcetone1Frame, item.assetName);
		__AmmoFactory.PutResourceNameLabel(__AmmoFactory.txAcetone1Frame, StaticData.Translate(item.uiName));
		__AmmoFactory.PutResourceAmountLabel(__AmmoFactory.txAcetone1Frame, NetworkScript.player.playerBelongings.playerItems.GetMinetalQty(PlayerItems.TypeAceton));
		item = StaticData.allTypes.get_Item(PlayerItems.TypeEquilibrium);
		__AmmoFactory.txEqu4Frame = __AmmoFactory.PutResourceFrame((int)__AmmoFactory.txEqu1Frame.X + 214, (int)__AmmoFactory.txEqu1Frame.Y + 217);
		__AmmoFactory.PutResourceTexture(__AmmoFactory.txEqu4Frame, item.assetName);
		__AmmoFactory.PutResourceNameLabel(__AmmoFactory.txEqu4Frame, StaticData.Translate(item.uiName));
		__AmmoFactory.PutResourceAmountLabel(__AmmoFactory.txEqu4Frame, NetworkScript.player.playerBelongings.playerItems.GetAmountAt(PlayerItems.TypeEquilibrium));
		item = StaticData.allTypes.get_Item(PlayerItems.TypeAceton);
		__AmmoFactory.txAcetone2Frame = __AmmoFactory.PutResourceFrame((int)__AmmoFactory.txEqu4Frame.X + 117, (int)__AmmoFactory.txEqu4Frame.Y);
		__AmmoFactory.PutResourceTexture(__AmmoFactory.txAcetone2Frame, item.assetName);
		__AmmoFactory.PutResourceNameLabel(__AmmoFactory.txAcetone2Frame, StaticData.Translate(item.uiName));
		__AmmoFactory.PutResourceAmountLabel(__AmmoFactory.txAcetone2Frame, NetworkScript.player.playerBelongings.playerItems.GetMinetalQty(PlayerItems.TypeAceton));
		__AmmoFactory.btnSolar = __AmmoFactory.PutAmmoButton((int)(__AmmoFactory.txEqu1Frame.X + 58f), (int)(__AmmoFactory.txEqu1Frame.Y + 83f), PlayerItems.TypeAmmoSolarCells);
		int x = (int)(__AmmoFactory.btnSolar.X - __AmmoFactory.txEqu1Frame.X);
		__AmmoFactory.PutAmmoTexture(__AmmoFactory.btnSolar, "AmmoSolarCells");
		__AmmoFactory.btnSolar.Hovered = new Action<object, bool>(null, __AmmoFactory.OnSolarHoverOne);
		__AmmoFactory.btnSolar.Clicked = new Action<EventHandlerParam>(null, __AmmoFactory.OnClickedOne);
		__AmmoFactory.btnFusion = __AmmoFactory.PutAmmoButton((int)(__AmmoFactory.txEqu2Frame.X + (float)x), (int)__AmmoFactory.btnSolar.Y, PlayerItems.TypeAmmoFusionCells);
		__AmmoFactory.PutAmmoTexture(__AmmoFactory.btnFusion, "AmmoFusionCells");
		__AmmoFactory.btnFusion.Hovered = new Action<object, bool>(null, __AmmoFactory.OnFusionHoverOne);
		__AmmoFactory.btnFusion.Clicked = new Action<EventHandlerParam>(null, __AmmoFactory.OnClickedOne);
		__AmmoFactory.btnColdFusion = __AmmoFactory.PutAmmoButton((int)(__AmmoFactory.txEqu3Frame.X + (float)x), (int)__AmmoFactory.btnSolar.Y, PlayerItems.TypeAmmoColdFusionCells);
		__AmmoFactory.PutAmmoTexture(__AmmoFactory.btnColdFusion, "AmmoColdCells");
		__AmmoFactory.btnColdFusion.Hovered = new Action<object, bool>(null, __AmmoFactory.OnFusionColdHoverOne);
		__AmmoFactory.btnColdFusion.Clicked = new Action<EventHandlerParam>(null, __AmmoFactory.OnClickedOne);
		__AmmoFactory.btnSulfurFusion = __AmmoFactory.PutAmmoButton((int)(__AmmoFactory.txEqu4Frame.X + (float)x), (int)(__AmmoFactory.txEqu4Frame.Y + 83f), PlayerItems.TypeAmmoSulfurFusionCells);
		__AmmoFactory.PutAmmoTexture(__AmmoFactory.btnSulfurFusion, "AmmoSulfurCells");
		__AmmoFactory.btnSulfurFusion.Hovered = new Action<object, bool>(null, __AmmoFactory.OnFusionSulfurHoverOne);
		__AmmoFactory.btnSulfurFusion.Clicked = new Action<EventHandlerParam>(null, __AmmoFactory.OnClickedOne);
		__AmmoFactory.txLineEq1Solar = __AmmoFactory.PutLineTexture(__AmmoFactory.btnSolar, true);
		__AmmoFactory.txLineWaterSolar = __AmmoFactory.PutLineTexture(__AmmoFactory.btnSolar, false);
		__AmmoFactory.txLineEq2Fusion = __AmmoFactory.PutLineTexture(__AmmoFactory.btnFusion, true);
		__AmmoFactory.txLineMethylFusion = __AmmoFactory.PutLineTexture(__AmmoFactory.btnFusion, false);
		__AmmoFactory.txLineEq3FusionCold = __AmmoFactory.PutLineTexture(__AmmoFactory.btnColdFusion, true);
		__AmmoFactory.txLineAceton1FusionCold = __AmmoFactory.PutLineTexture(__AmmoFactory.btnColdFusion, false);
		__AmmoFactory.txLineEq4FusionSulfur = __AmmoFactory.PutLineTexture(__AmmoFactory.btnSulfurFusion, true);
		__AmmoFactory.txLineAceton2FusionSulfur = __AmmoFactory.PutLineTexture(__AmmoFactory.btnSulfurFusion, false);
		if (__AmmoFactory.ammoTypePossitions != null)
		{
			__AmmoFactory.ammoTypePossitions.Clear();
		}
		else
		{
			__AmmoFactory.ammoTypePossitions = new SortedList<ushort, Vector2>();
		}
		__AmmoFactory.ammoTypePossitions.Add(PlayerItems.TypeAmmoSolarCells, new Vector2(314f, 214f));
		__AmmoFactory.ammoTypePossitions.Add(PlayerItems.TypeAmmoFusionCells, new Vector2(525f, 214f));
		__AmmoFactory.ammoTypePossitions.Add(PlayerItems.TypeAmmoColdFusionCells, new Vector2(727f, 214f));
		__AmmoFactory.ammoTypePossitions.Add(PlayerItems.TypeAmmoSulfurFusionCells, new Vector2(528f, 431f));
	}

	public static void CreateFormulaStuff(ushort resType, bool isOne)
	{
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(35f, 425f, 160f, 20f),
			text = StaticData.Translate("key_fushion_formula").ToUpper(),
			Alignment = 3,
			Font = GuiLabel.FontBold,
			FontSize = 13,
			TextColor = GuiNewStyleBar.orangeColor
		};
		__AmmoFactory.wnd.AddGuiElement(guiLabel);
		__AmmoFactory.forDelete.Add(guiLabel);
		__AmmoFactory.wnd.formulaStuff.Add(guiLabel);
		guiLabel = new GuiLabel()
		{
			boundries = new Rect(35f, 495f, 188f, 20f),
			text = string.Empty,
			Alignment = 0,
			FontSize = 13,
			Font = GuiLabel.FontBold,
			TextColor = Color.get_red()
		};
		__AmmoFactory.wnd.AddGuiElement(guiLabel);
		__AmmoFactory.wnd.formulaStuff.Add(guiLabel);
		__AmmoFactory.wnd.lblFormulaError = guiLabel;
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
				__AmmoFactory.forDelete.Add(guiLabel);
				__AmmoFactory.wnd.AddGuiElement(guiLabel);
				__AmmoFactory.wnd.formulaStuff.Add(guiLabel);
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
			__AmmoFactory.wnd.AddGuiElement(guiLabel);
			__AmmoFactory.wnd.formulaStuff.Add(guiLabel);
			__AmmoFactory.forDelete.Add(guiLabel);
			textWidth = textWidth + (int)(guiLabel.TextWidth + 4f);
			GuiTexture guiTexture = new GuiTexture()
			{
				boundries = new Rect((float)textWidth, (float)num, 32f, 32f)
			};
			guiTexture.SetTextureKeepSize("MineralsAvatars", playerItemTypesDatum.assetName);
			__AmmoFactory.forDelete.Add(guiTexture);
			__AmmoFactory.wnd.AddGuiElement(guiTexture);
			__AmmoFactory.wnd.formulaStuff.Add(guiTexture);
			textWidth = textWidth + 36;
		}
		int num2 = NetworkScript.player.playerBelongings.playerItems.MaxSynthesis(resType, NetworkScript.player.cfg.fusionPriceOff);
		guiLabel = new GuiLabel()
		{
			boundries = new Rect(35f, 483f, 160f, 24f),
			text = StaticData.Translate("key_fushion_produce").ToUpper(),
			Font = GuiLabel.FontBold,
			Alignment = 3,
			FontSize = 13,
			TextColor = GuiNewStyleBar.orangeColor
		};
		__AmmoFactory.wnd.AddGuiElement(guiLabel);
		__AmmoFactory.forDelete.Add(guiLabel);
		__AmmoFactory.wnd.formulaStuff.Add(guiLabel);
		int item2 = 0;
		if (PlayerItems.specialAmounts.ContainsKey(resType))
		{
			item2 = (int)((float)PlayerItems.specialAmounts.get_Item(resType) * (1f + (float)NetworkScript.player.cfg.ammoCreationBonus / 100f));
		}
		if (!isOne)
		{
			item2 = item2 * num2;
		}
		textWidth = 35;
		guiLabel = new GuiLabel()
		{
			FontSize = 13,
			boundries = new Rect(35f, 510f, 170f, 25f),
			Alignment = 0,
			text = string.Concat(item2.ToString("###,##0"), " x ")
		};
		__AmmoFactory.wnd.AddGuiElement(guiLabel);
		__AmmoFactory.forDelete.Add(guiLabel);
		__AmmoFactory.wnd.formulaStuff.Add(guiLabel);
		AmmoNet ammoNet = (AmmoNet)StaticData.allTypes.get_Item(resType);
		textWidth = textWidth + (int)(guiLabel.TextWidth + 4f);
		GuiTexture guiTexture1 = new GuiTexture()
		{
			boundries = new Rect((float)textWidth, 500f, 32f, 32f)
		};
		guiTexture1.SetTextureKeepSize("AmmosAvatars", ammoNet.assetName);
		__AmmoFactory.wnd.AddGuiElement(guiTexture1);
		__AmmoFactory.forDelete.Add(guiTexture1);
		__AmmoFactory.wnd.formulaStuff.Add(guiTexture1);
	}

	private static void FinishFuse(GuiTextureAnimated tx)
	{
		__AmmoFactory.wnd.RemoveGuiElement(tx);
	}

	private static void OnAmmoBtnClickediPad(EventHandlerParam prm)
	{
		__AmmoFactory.Clear();
		__AmmoFactory.Create();
		ushort num = (ushort)prm.customData;
		if (num == __AmmoFactory.AmmoTypeOnScreen)
		{
			__AmmoFactory.OnClickedOne(prm);
		}
		if (num == PlayerItems.TypeAmmoSolarCells)
		{
			__AmmoFactory.OnSolarHoverOne(null, true);
		}
		else if (num == PlayerItems.TypeAmmoFusionCells)
		{
			__AmmoFactory.OnFusionHoverOne(null, true);
		}
		else if (num == PlayerItems.TypeAmmoColdFusionCells)
		{
			__AmmoFactory.OnFusionColdHoverOne(null, true);
		}
		else if (num == PlayerItems.TypeAmmoSulfurFusionCells)
		{
			__AmmoFactory.OnFusionSulfurHoverOne(null, true);
		}
		__AmmoFactory.AmmoTypeOnScreen = num;
	}

	public static void OnClickedOne(EventHandlerParam parm)
	{
		// 
		// Current member / type: System.Void __AmmoFactory::OnClickedOne(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnClickedOne(EventHandlerParam)
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

	private static void OnFusionColdHoverOne(object parm, bool state)
	{
		__AmmoFactory.OnHoverOne(PlayerItems.TypeAmmoColdFusionCells, state, __AmmoFactory.txLineEq3FusionCold, __AmmoFactory.txLineAceton1FusionCold, __AmmoFactory.btnColdFusion, __AmmoFactory.txEqu3Frame, __AmmoFactory.txAcetone1Frame, PlayerItems.TypeEquilibrium, PlayerItems.TypeAceton, true);
	}

	private static void OnFusionColdMaxHover(object parm, bool state)
	{
		__AmmoFactory.OnHoverOne(PlayerItems.TypeAmmoColdFusionCells, state, __AmmoFactory.txLineEq3FusionCold, __AmmoFactory.txLineAceton1FusionCold, __AmmoFactory.btnColdFusion, __AmmoFactory.txEqu3Frame, __AmmoFactory.txAcetone1Frame, PlayerItems.TypeEquilibrium, PlayerItems.TypeAceton, false);
	}

	private static void OnFusionHoverOne(object parm, bool state)
	{
		__AmmoFactory.OnHoverOne(PlayerItems.TypeAmmoFusionCells, state, __AmmoFactory.txLineEq2Fusion, __AmmoFactory.txLineMethylFusion, __AmmoFactory.btnFusion, __AmmoFactory.txEqu2Frame, __AmmoFactory.txMethylFrame, PlayerItems.TypeEquilibrium, PlayerItems.TypeMetyl, true);
	}

	private static void OnFusionMaxHover(object parm, bool state)
	{
		__AmmoFactory.OnHoverOne(PlayerItems.TypeAmmoFusionCells, state, __AmmoFactory.txLineEq2Fusion, __AmmoFactory.txLineMethylFusion, __AmmoFactory.btnFusion, __AmmoFactory.txEqu2Frame, __AmmoFactory.txMethylFrame, PlayerItems.TypeEquilibrium, PlayerItems.TypeMetyl, false);
	}

	private static void OnFusionSulfurHoverOne(object parm, bool state)
	{
		__AmmoFactory.OnHoverOne(PlayerItems.TypeAmmoSulfurFusionCells, state, __AmmoFactory.txLineEq4FusionSulfur, __AmmoFactory.txLineAceton2FusionSulfur, __AmmoFactory.btnSulfurFusion, __AmmoFactory.txEqu4Frame, __AmmoFactory.txAcetone2Frame, PlayerItems.TypeEquilibrium, PlayerItems.TypeAceton, true);
	}

	private static void OnFusionSulfurMaxHover(object parm, bool state)
	{
		__AmmoFactory.OnHoverOne(PlayerItems.TypeAmmoSulfurFusionCells, state, __AmmoFactory.txLineEq4FusionSulfur, __AmmoFactory.txLineAceton2FusionSulfur, __AmmoFactory.btnSulfurFusion, __AmmoFactory.txEqu4Frame, __AmmoFactory.txAcetone2Frame, PlayerItems.TypeEquilibrium, PlayerItems.TypeAceton, false);
	}

	private static void OnHoverOne(ushort ammo, bool state, GuiTexture txLeft, GuiTexture txRight, GuiButtonFixed btn, GuiTexture txDepLeft, GuiTexture txDepRight, ushort depLeft, ushort depRight, bool isOne)
	{
		AmmoNet item = (AmmoNet)StaticData.allTypes.get_Item(ammo);
		SortedList<ushort, short> sortedList = PlayerItems.fusionDependancies.get_Item(item.itemType);
		if (state)
		{
			__AmmoFactory.wnd.txAvatar.boundries = new Rect(67f, 129f, 100f, 68f);
			__AmmoFactory.CreateFormulaStuff(item.itemType, isOne);
			bool minetalQty = NetworkScript.player.playerBelongings.playerItems.GetMinetalQty(depLeft) >= (long)sortedList.get_Item(depLeft);
			bool flag = true;
			if (!minetalQty)
			{
				flag = false;
				txLeft.SetTexture("FusionWindow", "lineLeftRed");
				txDepLeft.SetTexture("FusionWindow", "hex64red");
			}
			else
			{
				txLeft.SetTexture("FusionWindow", "lineLeftWht");
			}
			if (NetworkScript.player.playerBelongings.playerItems.GetMinetalQty(depRight) < (long)sortedList.get_Item(depRight))
			{
				flag = false;
				txRight.SetTexture("FusionWindow", "lineRightRed");
				txDepRight.SetTexture("FusionWindow", "hex64red");
			}
			else
			{
				txRight.SetTexture("FusionWindow", "lineRightWht");
			}
			if (flag)
			{
				btn.isMuted = true;
			}
			else
			{
				btn.SetTextureDisabled("FusionWindow", "hex64red");
				btn.isEnabled = false;
			}
			__AmmoFactory.wnd.txAvatar.SetTextureKeepSize("AmmosAvatars", item.assetName);
			__AmmoFactory.wnd.lbl_ResName.text = StaticData.Translate(item.uiName);
			__AmmoFactory.wnd.lbl_InfoBoxValue.text = StaticData.Translate(PlayerItems.GetDescription(item.itemType));
			__AmmoFactory.wnd.lblValue.text = string.Format(StaticData.Translate("key_fushion_ammo_dmg"), item.damage);
		}
		else
		{
			__AmmoFactory.Clear();
			__AmmoFactory.Create();
		}
	}

	public static void OnMaxClick(EventHandlerParam parm)
	{
		// 
		// Current member / type: System.Void __AmmoFactory::OnMaxClick(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnMaxClick(EventHandlerParam)
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

	private static void OnSolarHoverOne(object parm, bool state)
	{
		__AmmoFactory.OnHoverOne(PlayerItems.TypeAmmoSolarCells, state, __AmmoFactory.txLineEq1Solar, __AmmoFactory.txLineWaterSolar, __AmmoFactory.btnSolar, __AmmoFactory.txEqu1Frame, __AmmoFactory.txWaterFrame, PlayerItems.TypeMetyl, PlayerItems.TypeWater, true);
	}

	private static void OnSolarMaxHover(object parm, bool state)
	{
		__AmmoFactory.OnHoverOne(PlayerItems.TypeAmmoSolarCells, state, __AmmoFactory.txLineEq1Solar, __AmmoFactory.txLineWaterSolar, __AmmoFactory.btnSolar, __AmmoFactory.txEqu1Frame, __AmmoFactory.txWaterFrame, PlayerItems.TypeMetyl, PlayerItems.TypeWater, false);
	}

	private static GuiButtonFixed PutAmmoButton(int x, int y, ushort ammoType)
	{
		AmmoNet item = (AmmoNet)StaticData.allTypes.get_Item(ammoType);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTextureNormal("FusionWindow", "hex64blue");
		guiButtonFixed.SetTextureClicked("FusionWindow", "hex64white");
		guiButtonFixed.SetTextureHover("FusionWindow", "hex64white");
		if (NetworkScript.player.playerBelongings.AllowedAmmo((int)ammoType) < 1)
		{
			guiButtonFixed.SetTextureNormal("FusionWindow", "hex64red");
			guiButtonFixed.SetTextureClicked("FusionWindow", "hex64red");
			guiButtonFixed.SetTextureHover("FusionWindow", "hex64red");
		}
		guiButtonFixed.eventHandlerParam = new EventHandlerParam()
		{
			customData = ammoType
		};
		guiButtonFixed.hoverParam = ammoType;
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.boundries = new Rect((float)x, (float)y, 64f, 64f);
		guiButtonFixed.isEnabled = true;
		guiButtonFixed.isHoverAware = true;
		__AmmoFactory.forDelete.Add(guiButtonFixed);
		__AmmoFactory.wnd.AddGuiElement(guiButtonFixed);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect((float)(x - 60), (float)(y + 70), 180f, 23f),
			text = StaticData.Translate(item.uiName),
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 13,
			Alignment = 1
		};
		__AmmoFactory.forDelete.Add(guiLabel);
		__AmmoFactory.wnd.AddGuiElement(guiLabel);
		guiLabel = new GuiLabel()
		{
			boundries = new Rect((float)(x - 6), (float)(y - 12), 72f, 20f),
			FontSize = 12
		};
		long ammoQty = NetworkScript.player.playerBelongings.playerItems.GetAmmoQty(ammoType);
		guiLabel.text = ammoQty.ToString("###,##0");
		guiLabel.TextColor = Color.get_white();
		guiLabel.Alignment = 1;
		__AmmoFactory.wnd.AddGuiElement(guiLabel);
		__AmmoFactory.forDelete.Add(guiLabel);
		return guiButtonFixed;
	}

	private static GuiTexture PutAmmoTexture(GuiButtonFixed txFrame, string assetName)
	{
		GuiTexture guiTexture = new GuiTexture()
		{
			boundries = new Rect(txFrame.X + 6f, txFrame.Y + 10f, 52f, 35f)
		};
		guiTexture.SetTextureKeepSize("AmmosAvatars", assetName);
		guiTexture.isHoverAware = false;
		__AmmoFactory.wnd.AddGuiElement(guiTexture);
		__AmmoFactory.forDelete.Add(guiTexture);
		return guiTexture;
	}

	private static void PutEmptyWarningLbl(GuiWindow wnd)
	{
		if (__AmmoFactory.warningLbl != null && wnd.HasGuiElement(__AmmoFactory.warningLbl))
		{
			wnd.RemoveGuiElement(__AmmoFactory.warningLbl);
			__AmmoFactory.forDelete.Remove(__AmmoFactory.warningLbl);
		}
		__AmmoFactory.warningLbl = new GuiLabel()
		{
			boundries = new Rect(211f, 300f, 680f, 20f),
			text = string.Empty,
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 20,
			TextColor = GuiNewStyleBar.redColor
		};
		wnd.AddGuiElement(__AmmoFactory.warningLbl);
		__AmmoFactory.forDelete.Add(__AmmoFactory.warningLbl);
	}

	private static GuiTexture PutLineTexture(GuiButtonFixed btn, bool isLeftAligned)
	{
		GuiTexture guiTexture = new GuiTexture()
		{
			X = btn.X + (float)((!isLeftAligned ? 49 : -11)),
			Y = btn.Y - 50f + 16f + 7f
		};
		guiTexture.SetTexture("FusionWindow", (!isLeftAligned ? "lineRightBlu" : "lineLeftBlu"));
		__AmmoFactory.forDelete.Add(guiTexture);
		__AmmoFactory.wnd.AddGuiElement(guiTexture);
		return guiTexture;
	}

	private static GuiLabel PutResourceAmountLabel(GuiTexture txFrame, long amount)
	{
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(txFrame.X - 6f, txFrame.Y - 12f, 72f, 20f),
			FontSize = 12,
			text = amount.ToString("###,##0"),
			TextColor = Color.get_white(),
			Alignment = 1
		};
		__AmmoFactory.wnd.AddGuiElement(guiLabel);
		__AmmoFactory.forDelete.Add(guiLabel);
		return guiLabel;
	}

	private static GuiTexture PutResourceFrame(int x, int y)
	{
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("FusionWindow", "hex64blue");
		guiTexture.X = (float)x;
		guiTexture.Y = (float)y;
		__AmmoFactory.wnd.AddGuiElement(guiTexture);
		__AmmoFactory.forDelete.Add(guiTexture);
		return guiTexture;
	}

	private static GuiLabel PutResourceNameLabel(GuiTexture txFrame, string name)
	{
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(txFrame.X - 15f, txFrame.Y - 26f, 94f, 20f),
			FontSize = 12,
			text = name,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 1
		};
		__AmmoFactory.wnd.AddGuiElement(guiLabel);
		__AmmoFactory.forDelete.Add(guiLabel);
		return guiLabel;
	}

	private static GuiTexture PutResourceTexture(GuiTexture txFrame, string assetName)
	{
		GuiTexture guiTexture = new GuiTexture()
		{
			boundries = new Rect(txFrame.X + 10f, txFrame.Y + 10f, 52f, 52f)
		};
		guiTexture.SetTextureKeepSize("MineralsAvatars", assetName);
		__AmmoFactory.wnd.AddGuiElement(guiTexture);
		__AmmoFactory.forDelete.Add(guiTexture);
		return guiTexture;
	}

	private static void ShowInventoryFullWarning()
	{
		__AmmoFactory.warningLbl.text = StaticData.Translate("key_space_label_inventory_full");
		__AmmoFactory.wnd.customOnGUIAction = new Action(null, __AmmoFactory.UpdateWarningLbl);
		__AmmoFactory.blinkCnt = 0;
		__AmmoFactory.deltaTime = 0f;
		__AmmoFactory.isFadeingOut = false;
	}

	private static void UpdateWarningLbl()
	{
		if (__AmmoFactory.warningLbl == null)
		{
			return;
		}
		if (!__AmmoFactory.isFadeingOut)
		{
			__AmmoFactory.deltaTime = __AmmoFactory.deltaTime + Time.get_deltaTime();
			if (__AmmoFactory.deltaTime >= 1f)
			{
				__AmmoFactory.isFadeingOut = true;
				__AmmoFactory.blinkCnt = __AmmoFactory.blinkCnt + 1;
			}
		}
		else
		{
			__AmmoFactory.deltaTime = __AmmoFactory.deltaTime - Time.get_deltaTime();
			if (__AmmoFactory.deltaTime <= 0f)
			{
				__AmmoFactory.isFadeingOut = false;
				if (__AmmoFactory.blinkCnt >= 3)
				{
					__AmmoFactory.warningLbl.text = string.Empty;
					__AmmoFactory.wnd.customOnGUIAction = null;
				}
			}
		}
		__AmmoFactory.warningLbl.TextColor = new Color(1f, 0.0745f, 0.0745f, __AmmoFactory.deltaTime);
	}
}