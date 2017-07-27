using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameMessagesWindow : GuiWindow
{
	private GameMessageType selectedType = 1;

	private GuiScrollingContainer scroller;

	private List<GuiElement> forDelete = new List<GuiElement>();

	public GameMessagesWindow()
	{
	}

	private void ConfirmExternalLink(EventHandlerParam prm)
	{
		if (prm == null || prm.customData == null)
		{
			return;
		}
		AndromedaGui.gui.RemoveWindow(AndromedaGui.gui.activeToolTipId);
		AndromedaGui.gui.activeToolTipId = -1;
		Application.OpenURL((string)prm.customData);
	}

	public override void Create()
	{
		base.SetBackgroundTexture("PoiScreenWindow", "frame");
		base.PutToCenter();
		this.zOrder = 210;
		this.isHidden = false;
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(147f, 8f, 618f, 36f),
			Alignment = 4,
			text = StaticData.Translate("key_game_message_window_title"),
			FontSize = 24,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel);
		this.scroller = new GuiScrollingContainer(10f, 55f, 885f, 460f, 1, this);
		base.AddGuiElement(this.scroller);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.SetTexture("PoiScreenWindow", "tab_");
		guiButtonResizeable.X = 630f;
		guiButtonResizeable.Y = 10f;
		guiButtonResizeable.boundries.set_width(100f);
		guiButtonResizeable.groupId = 1;
		guiButtonResizeable.behaviourKeepClicked = true;
		guiButtonResizeable.Caption = StaticData.Translate("key_game_message_type_announcement");
		guiButtonResizeable.FontSize = 12;
		guiButtonResizeable.Alignment = 4;
		guiButtonResizeable.SetRegularFont();
		guiButtonResizeable.SetColor(GuiNewStyleBar.blueColor);
		guiButtonResizeable.eventHandlerParam.customData = (GameMessageType)1;
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, GameMessagesWindow.OnChangeTypeClicked);
		guiButtonResizeable.IsClicked = this.selectedType == 1;
		base.AddGuiElement(guiButtonResizeable);
		GuiButtonResizeable action = new GuiButtonResizeable();
		action.SetTexture("PoiScreenWindow", "tab_");
		action.X = 740f;
		action.Y = 10f;
		action.boundries.set_width(100f);
		action.groupId = 1;
		action.behaviourKeepClicked = true;
		action.Caption = StaticData.Translate("key_game_message_type_private");
		action.FontSize = 12;
		action.Alignment = 4;
		action.SetRegularFont();
		action.SetColor(GuiNewStyleBar.blueColor);
		action.eventHandlerParam.customData = (GameMessageType)0;
		action.Clicked = new Action<EventHandlerParam>(this, GameMessagesWindow.OnChangeTypeClicked);
		action.IsClicked = this.selectedType == 0;
		base.AddGuiElement(action);
	}

	private void OnChangeTypeClicked(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void GameMessagesWindow::OnChangeTypeClicked(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnChangeTypeClicked(EventHandlerParam)
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

	private void OnDeleteConfirm(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void GameMessagesWindow::OnDeleteConfirm(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnDeleteConfirm(EventHandlerParam)
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

	private void OnDeletePrivateMessageClick(EventHandlerParam prm)
	{
		string str = StaticData.Translate("key_delete_pm_pop_up_title");
		string str1 = StaticData.Translate("key_delete_pm_pop_up_info");
		string empty = string.Empty;
		string str2 = StaticData.Translate("key_dock_my_ships_select_ship_yes");
		string str3 = StaticData.Translate("key_dock_my_ships_select_ship_no");
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = (int)prm.customData
		};
		NewPopUpWindow.CreatePopUpWindow(str, str1, empty, str2, str3, out Inventory.dialogWindow, eventHandlerParam, new Action<EventHandlerParam>(this, GameMessagesWindow.OnDeleteConfirm), null);
	}

	private void OnExternalLinkClick(EventHandlerParam prm)
	{
		string str = StaticData.Translate("key_external_link_pop_up_title");
		string str1 = string.Format(StaticData.Translate("key_external_link_pop_up_info"), (string)prm.customData);
		string str2 = StaticData.Translate("key_dock_my_ships_select_ship_yes");
		string str3 = StaticData.Translate("key_dock_my_ships_select_ship_no");
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = (string)prm.customData
		};
		NewPopUpWindow.CreatePurchasePopUpWindow(str, str1, str2, str3, out Inventory.dialogWindow, eventHandlerParam, new Action<EventHandlerParam>(this, GameMessagesWindow.ConfirmExternalLink));
	}

	public void PopulateScreen()
	{
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = this.selectedType
		};
		this.OnChangeTypeClicked(eventHandlerParam);
	}
}