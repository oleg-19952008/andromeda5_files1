using System;
using System.Collections.Generic;
using UnityEngine;

public class __ResourceHex
{
	private GuiTextureAnimated fuseAnimation;

	private GuiTexture txMineral;

	private GuiTexture txUnfusableFrame;

	private GuiButtonFixed btnUnfusableFrame;

	public GuiButtonFixed btnFuseOne;

	public GuiButtonFixed btnFuseMax;

	public GuiLabel lbl_qty;

	public GuiLabel lbl_Label;

	private Vector2 Position;

	public ushort resourceId;

	public string resourceName;

	public string assetName;

	public bool isFusable;

	private __FusionWindow wnd;

	public __ResourceHex(ushort id, Vector2 position, __FusionWindow wnd)
	{
		this.wnd = wnd;
		this.resourceId = id;
		this.resourceName = StaticData.Translate(StaticData.allTypes.get_Item(this.resourceId).uiName);
		this.assetName = StaticData.allTypes.get_Item(this.resourceId).assetName;
		this.Position = position;
		this.isFusable = PlayerItems.fusionDependancies.get_Item(id).get_Count() > 0;
	}

	public void ClearGui(GuiWindow wnd)
	{
		if (this.txUnfusableFrame != null && wnd.HasGuiElement(this.txUnfusableFrame))
		{
			wnd.RemoveGuiElement(this.txUnfusableFrame);
		}
		if (this.btnFuseOne != null && wnd.HasGuiElement(this.btnFuseOne))
		{
			wnd.RemoveGuiElement(this.btnFuseOne);
		}
		if (this.btnFuseMax != null && wnd.HasGuiElement(this.btnFuseMax))
		{
			wnd.RemoveGuiElement(this.btnFuseMax);
		}
		wnd.RemoveGuiElement(this.lbl_qty);
		wnd.RemoveGuiElement(this.lbl_Label);
		wnd.RemoveGuiElement(this.txMineral);
	}

	public void ClearGui()
	{
		this.wnd.forDelete.Remove(this.txMineral);
		this.wnd.RemoveGuiElement(this.txMineral);
		this.wnd.forDelete.Remove(this.lbl_qty);
		this.wnd.RemoveGuiElement(this.lbl_qty);
		this.wnd.forDelete.Remove(this.lbl_Label);
		this.wnd.RemoveGuiElement(this.lbl_Label);
		if (this.txUnfusableFrame != null)
		{
			this.wnd.forDelete.Remove(this.txUnfusableFrame);
			this.wnd.RemoveGuiElement(this.txUnfusableFrame);
		}
		if (this.btnUnfusableFrame != null)
		{
			this.wnd.forDelete.Remove(this.btnUnfusableFrame);
			this.wnd.RemoveGuiElement(this.btnUnfusableFrame);
		}
		if (this.btnFuseOne != null)
		{
			this.wnd.forDelete.Remove(this.btnFuseOne);
			this.wnd.RemoveGuiElement(this.btnFuseOne);
		}
		if (this.btnFuseMax != null)
		{
			this.wnd.forDelete.Remove(this.btnFuseMax);
			this.wnd.RemoveGuiElement(this.btnFuseMax);
		}
	}

	public void CreateGui()
	{
		this.txMineral = new GuiTexture();
		this.txMineral.SetTexture("MineralsAvatars", this.assetName);
		this.txMineral.X = this.Position.x + 10f;
		this.txMineral.Y = this.Position.y + 10f;
		this.txMineral.SetSize(54f, 54f);
		this.wnd.AddGuiElement(this.txMineral);
		this.lbl_qty = new GuiLabel()
		{
			FontSize = 13,
			boundries = new Rect(this.Position.x + 75f, this.Position.y + 15f, 130f, 40f),
			Alignment = 3,
			text = string.Empty
		};
		this.wnd.AddGuiElement(this.lbl_qty);
		this.lbl_Label = new GuiLabel()
		{
			FontSize = 13,
			boundries = new Rect(this.Position.x + 75f + this.lbl_qty.TextWidth, this.Position.y + 15f, 130f - this.lbl_qty.TextWidth, 40f),
			Alignment = 3,
			text = this.resourceName,
			TextColor = GuiNewStyleBar.blueColor
		};
		this.wnd.AddGuiElement(this.lbl_Label);
		if (!this.isFusable)
		{
			this.CreateUnclickableFrame();
		}
		else
		{
			int num = NetworkScript.player.playerBelongings.playerItems.MaxSynthesis(this.resourceId, NetworkScript.player.cfg.fusionPriceOff);
			if (num > 0)
			{
				this.btnFuseOne = new GuiButtonFixed()
				{
					isHoverAware = true,
					boundries = new Rect(this.Position.x, this.Position.y, 72f, 71f)
				};
				this.btnFuseOne.SetTextureNormal("FusionWindow", "FusionHexagonBlue");
				this.btnFuseOne.SetTextureHover("FusionWindow", "FusionHexagonWhite");
				this.btnFuseOne.SetTextureClicked("FusionWindow", "FusionHexagonWhite");
				this.btnFuseOne.Caption = string.Empty;
				GuiButtonFixed guiButtonFixed = this.btnFuseOne;
				EventHandlerParam eventHandlerParam = new EventHandlerParam()
				{
					customData = this.resourceId
				};
				guiButtonFixed.eventHandlerParam = eventHandlerParam;
				this.btnFuseOne.Clicked = new Action<EventHandlerParam>(this, __ResourceHex.Fuse);
				this.btnFuseOne.hoverParam = this.resourceId;
				this.btnFuseOne.Hovered = new Action<object, bool>(this.wnd, __FusionWindow.OnResHover);
				this.wnd.AddGuiElement(this.btnFuseOne);
				this.btnFuseMax = new GuiButtonFixed();
				this.btnFuseMax.SetTexture("FusionWindow", "btnFuseMax");
				this.btnFuseMax.X = this.btnFuseOne.boundries.get_x() + 55f;
				this.btnFuseMax.Y = this.btnFuseOne.boundries.get_y() + 60f;
				this.btnFuseMax.FontSize = 14;
				this.btnFuseMax.MarginTop = -2;
				this.btnFuseMax._marginLeft = 3;
				this.btnFuseMax.Alignment = 3;
				this.btnFuseMax.textColorHover = GuiNewStyleBar.blueColor;
				this.btnFuseMax.Caption = string.Format("+ {0}", num);
				GuiButtonFixed guiButtonFixed1 = this.btnFuseMax;
				eventHandlerParam = new EventHandlerParam()
				{
					customData = this.resourceId
				};
				guiButtonFixed1.eventHandlerParam = eventHandlerParam;
				this.btnFuseMax.Clicked = new Action<EventHandlerParam>(this, __ResourceHex.FuseMax);
				this.btnFuseMax.hoverParam = this.resourceId;
				this.btnFuseMax.Hovered = new Action<object, bool>(this.wnd, __FusionWindow.OnResHover);
				this.wnd.AddGuiElement(this.btnFuseMax);
			}
			else
			{
				this.CreateUnclickableFrame();
			}
		}
		if (NetworkScript.player.playerBelongings.playerItems.GetMinetalQty(this.resourceId) >= (long)0)
		{
			GuiLabel lblQty = this.lbl_qty;
			long minetalQty = NetworkScript.player.playerBelongings.playerItems.GetMinetalQty(this.resourceId);
			lblQty.text = minetalQty.ToString("###,##0");
		}
		else
		{
			this.lbl_qty.text = "0";
		}
		this.lbl_Label.boundries = new Rect(this.Position.x + 80f + this.lbl_qty.TextWidth, this.Position.y + 15f, 130f - this.lbl_qty.TextWidth, 40f);
	}

	public void CreateUnclickableFrame()
	{
		this.txUnfusableFrame = new GuiTexture();
		this.txUnfusableFrame.SetTexture("FusionWindow", "FusionHexagonBlue");
		this.txUnfusableFrame.X = this.Position.x;
		this.txUnfusableFrame.Y = this.Position.y;
		this.txUnfusableFrame.hoverParam = this.resourceId;
		this.txUnfusableFrame.Hovered = new Action<object, bool>(this.wnd, __FusionWindow.OnResHover);
		this.txUnfusableFrame.hoverParam = this.resourceId;
		this.txUnfusableFrame.Hovered = new Action<object, bool>(this.wnd, __FusionWindow.OnResHover);
		this.wnd.AddGuiElement(this.txUnfusableFrame);
	}

	private void FinishFuse(GuiTextureAnimated tx)
	{
		this.wnd.RemoveGuiElement(tx);
	}

	public void Fuse(EventHandlerParam param)
	{
		// 
		// Current member / type: System.Void __ResourceHex::Fuse(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void Fuse(EventHandlerParam)
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

	public void FuseMax(EventHandlerParam param)
	{
		// 
		// Current member / type: System.Void __ResourceHex::FuseMax(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void FuseMax(EventHandlerParam)
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

	public void ReArrangeAfterAmountsChanged()
	{
		this.ClearGui();
		this.CreateGui();
	}

	public void SetBlueFrame()
	{
		if (this.btnFuseOne != null)
		{
			this.btnFuseOne.SetTextureNormal("FusionWindow", "FusionHexagonBlue");
		}
		else
		{
			this.txUnfusableFrame.SetTexture("FusionWindow", "FusionHexagonBlue");
		}
	}

	public void SetRedFrame()
	{
		if (this.btnFuseOne != null)
		{
			this.btnFuseOne.SetTextureNormal("FusionWindow", "FusionHexagonRed");
		}
		else
		{
			this.txUnfusableFrame.SetTexture("FusionWindow", "FusionHexagonRed");
		}
	}

	public void SetWhiteFrame()
	{
		if (this.btnFuseOne != null)
		{
			this.btnFuseOne.SetTextureNormal("FusionWindow", "FusionHexagonWhite");
		}
		else
		{
			this.txUnfusableFrame.SetTextureKeepSize("FusionWindow", "FusionHexagonWhite");
		}
	}
}