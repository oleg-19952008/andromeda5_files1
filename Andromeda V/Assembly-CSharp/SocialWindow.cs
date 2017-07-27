using System;
using UnityEngine;

public class SocialWindow : GuiWindow
{
	private GuiLabel interactionTitle;

	public SocialWindow()
	{
	}

	public override void Create()
	{
		base.SetBackgroundTexture("ConfigWnd", "SocialCircleDefault");
		base.PutToCenter();
		this.boundries.set_y((float)Screen.get_height() * 1.07f / 2f - this.boundries.get_height() / 2f);
		this.isHidden = false;
		this.zOrder = 210;
		this.interactionTitle = new GuiLabel()
		{
			boundries = new Rect(151f, 45f, 210f, 32f),
			Alignment = 4,
			text = string.Empty,
			Font = GuiLabel.FontBold,
			FontSize = 16
		};
		base.AddGuiElement(this.interactionTitle);
		for (int i = 0; i < 11; i++)
		{
			GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
			guiButtonFixed.SetTexture("FrameworkGUI", "empty");
			guiButtonFixed.boundries = new Rect(313f, 103f, 50f, 50f);
			guiButtonFixed.Caption = string.Empty;
			guiButtonFixed.hoverParam = i;
			guiButtonFixed.eventHandlerParam.customData = i;
			guiButtonFixed.Hovered = new Action<object, bool>(this, SocialWindow.OnSocioalBtnHoverd);
			guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, SocialWindow.OnSocialBtnClicked);
			switch (i)
			{
				case 0:
				{
					guiButtonFixed.X = 149f;
					guiButtonFixed.Y = 101f;
					break;
				}
				case 1:
				{
					guiButtonFixed.X = 313f;
					guiButtonFixed.Y = 101f;
					break;
				}
				case 2:
				{
					guiButtonFixed.X = 368f;
					guiButtonFixed.Y = 168f;
					break;
				}
				case 3:
				{
					guiButtonFixed.X = 381f;
					guiButtonFixed.Y = 251f;
					break;
				}
				case 4:
				{
					guiButtonFixed.X = 346f;
					guiButtonFixed.Y = 330f;
					break;
				}
				case 5:
				{
					guiButtonFixed.X = 273f;
					guiButtonFixed.Y = 376f;
					break;
				}
				case 6:
				{
					guiButtonFixed.X = 189f;
					guiButtonFixed.Y = 376f;
					break;
				}
				case 7:
				{
					guiButtonFixed.X = 116f;
					guiButtonFixed.Y = 330f;
					break;
				}
				case 8:
				{
					guiButtonFixed.X = 81f;
					guiButtonFixed.Y = 251f;
					break;
				}
				case 9:
				{
					guiButtonFixed.X = 94f;
					guiButtonFixed.Y = 168f;
					break;
				}
				case 10:
				{
					guiButtonFixed.X = 230f;
					guiButtonFixed.Y = 76f;
					break;
				}
			}
			base.AddGuiElement(guiButtonFixed);
		}
	}

	private void OnSocialBtnClicked(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void SocialWindow::OnSocialBtnClicked(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnSocialBtnClicked(EventHandlerParam)
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

	private void OnSocioalBtnHoverd(object obj, bool state)
	{
		int num = (int)obj;
		string str = string.Format("SocialCircle_{0}", num);
		string str1 = string.Format("key_social_interaction_{0}", num);
		if (!state)
		{
			base.SetBackgroundTexture("ConfigWnd", "SocialCircleDefault");
			this.interactionTitle.text = string.Empty;
		}
		else
		{
			base.SetBackgroundTexture("ConfigWnd", str);
			this.interactionTitle.text = StaticData.Translate(str1);
		}
	}
}