using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class KeyboardShortcuts
{
	public Dictionary<KeyboardCommand, ShortcutCommandItem> keyboardShortcuts;

	private Event e;

	public Action OnKeyChangedAction;

	private bool isShiftPressed;

	private bool isControlPressed;

	private bool stopListenforKey;

	public GuiButtonResizeable keyBoardUpdateButton;

	private int currentCommandIndex;

	private int currentKeyNumber;

	public KeyboardShortcuts()
	{
		this.InitKeyboardShortcuts();
	}

	public void ChangeKeyListener()
	{
		this.e = Event.get_current();
		if (!this.e.get_isKey() && !this.e.get_isMouse())
		{
			return;
		}
		if (this.e.get_type() == 6)
		{
			return;
		}
		if (this.e.get_modifiers() == 1 || this.e.get_keyCode() == 304 || this.e.get_keyCode() == 303 || this.e.get_shift())
		{
			this.isShiftPressed = true;
		}
		if (this.e.get_modifiers() == 2 || this.e.get_keyCode() == 306 || this.e.get_keyCode() == 305)
		{
			this.isControlPressed = true;
		}
		if (this.e.get_type() == 4 && this.e.get_keyCode() != null || this.e.get_type() == null)
		{
			this.ProcessKeyboardInput(this.e);
		}
		else if (this.e.get_type() == 5 && this.e.get_keyCode() != null || this.e.get_type() == 1)
		{
			if (this.stopListenforKey)
			{
				this.StopListeningForKey();
			}
			if (this.e.get_keyCode() == 27)
			{
				this.LeaveOldKey(null);
				this.StopListeningForKey();
			}
			this.isShiftPressed = false;
			this.isControlPressed = false;
		}
	}

	public string CheckKeyCodeName(string keyCodeName)
	{
		int num = 0;
		string str = keyCodeName;
		try
		{
			if (str.Contains("Alpha"))
			{
				str = Enumerable.ElementAt<char>(str, 5).ToString();
			}
			else if (str.Contains("Keypad"))
			{
				string str1 = str.Substring(6);
				if (str1.Contains("Minus"))
				{
					str1 = string.Concat(StaticData.Translate("key_keyboard_menu_numpad"), " -");
				}
				else if (str1.Contains("Plus"))
				{
					str1 = string.Concat(StaticData.Translate("key_keyboard_menu_numpad"), " +");
				}
				else if (str1.Contains("Divide"))
				{
					str1 = string.Concat(StaticData.Translate("key_keyboard_menu_numpad"), " /");
				}
				else if (!str1.Contains("Multiply"))
				{
					str1 = (!str1.Contains("Period") ? string.Concat(StaticData.Translate("key_keyboard_menu_numpad"), " ", str1) : string.Concat(StaticData.Translate("key_keyboard_menu_numpad"), " ."));
				}
				else
				{
					str1 = string.Concat(StaticData.Translate("key_keyboard_menu_numpad"), " *");
				}
				str = str1;
			}
			else if (str.Contains("Mouse"))
			{
				char chr = Enumerable.ElementAt<char>(str, 5);
				int.TryParse(chr.ToString(), ref num);
				switch (num)
				{
					case 0:
					{
						str = StaticData.Translate("key_keyboard_left_mouse_button");
						break;
					}
					case 1:
					{
						str = StaticData.Translate("key_keyboard_right_mouse_button");
						break;
					}
					case 2:
					{
						str = StaticData.Translate("key_keyboard_middle_mouse_button");
						break;
					}
					default:
					{
						str = string.Concat(StaticData.Translate("key_keyboard_menu_mouse"), " ", num);
						break;
					}
				}
			}
			else if (str.Contains("None"))
			{
				str = "N/A";
			}
			else if (str.Contains("BackQuote"))
			{
				str = "~";
			}
			else if (str.Contains("LeftArrow"))
			{
				str = StaticData.Translate("key_keyboard_menu_left_arrow");
			}
			else if (str.Contains("RightArrow"))
			{
				str = StaticData.Translate("key_keyboard_menu_right_arrow");
			}
			else if (str.Contains("UpArrow"))
			{
				str = StaticData.Translate("key_keyboard_menu_up_arrow");
			}
			else if (str.Contains("DownArrow"))
			{
				str = StaticData.Translate("key_keyboard_menu_down_arrow");
			}
			else if (str.Contains("Equals"))
			{
				str = "=";
			}
			else if (str.Contains("Quote"))
			{
				str = "'";
			}
			else if (str.Contains("Slash"))
			{
				str = "/";
			}
			else if (str.Contains("Period"))
			{
				str = ".";
			}
			else if (str.Contains("Comma"))
			{
				str = ",";
			}
			else if (str.Contains("Minus"))
			{
				str = "-";
			}
			else if (str.Contains("Semicolon"))
			{
				str = ";";
			}
			else if (str.Contains("RightBracket"))
			{
				str = "]";
			}
			else if (str.Contains("LeftBracket"))
			{
				str = "[";
			}
			else if (str.Contains("Backslash"))
			{
				str = "\\";
			}
			else if (str.Contains("Space"))
			{
				str = StaticData.Translate("key_keyboard_menu_space");
			}
		}
		catch (Exception exception)
		{
			Debug.Log(string.Concat("Unable to check name of keycode with error : ", exception.ToString()));
		}
		return str;
	}

	public string GetCommandKeyCodeShort(KeyboardCommand command)
	{
		string empty;
		try
		{
			if (this.keyboardShortcuts.ContainsKey(command))
			{
				ShortcutCommandItem item = this.keyboardShortcuts.get_Item(command);
				string str = this.CheckKeyCodeName(item.keyCodeOne.ToString());
				if (item.isKeyOneUsingShift)
				{
					str = string.Format("{0}+{1}", "s", str);
				}
				if (item.isKeyOneUsingCtrl)
				{
					str = string.Format("{0}+{1}", "c", str);
				}
				if (str.get_Length() > 5)
				{
					str = string.Concat(str.Substring(0, 3), "..");
				}
				empty = str;
			}
			else
			{
				empty = string.Empty;
			}
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			Debug.Log(string.Concat("Unable to get command ", command.ToString(), " tooltip with error : ", exception.ToString()));
			empty = string.Empty;
		}
		return empty;
	}

	public string GetCommandTooltip(KeyboardCommand command)
	{
		string empty;
		try
		{
			if (this.keyboardShortcuts.ContainsKey(command))
			{
				string str = string.Empty;
				ShortcutCommandItem item = this.keyboardShortcuts.get_Item(command);
				if (!string.IsNullOrEmpty(item.tooltipKey))
				{
					string str1 = this.CheckKeyCodeName(item.keyCodeOne.ToString());
					if (item.isKeyOneUsingShift)
					{
						str1 = string.Concat("Shift + ", str1);
					}
					if (item.isKeyOneUsingCtrl)
					{
						str1 = string.Concat("Ctrl + ", str1);
					}
					empty = string.Format(item.tooltipKey, str1);
				}
				else
				{
					empty = item.commandStringKey;
				}
			}
			else
			{
				empty = string.Empty;
			}
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			Debug.Log(string.Concat("Unable to get command ", command.ToString(), " tooltip with error : ", exception.ToString()));
			empty = string.Empty;
		}
		return empty;
	}

	private void InitKeyboardShortcuts()
	{
		this.RestoreDefaultShortcuts(false);
		if (NetworkScript.player == null || NetworkScript.player.playerBelongings == null || NetworkScript.player.playerBelongings.playerKeyboardShortcuts == null)
		{
			Debug.Log("NetworkScript.player.playerBelongings.playerKeyboardShortcuts may be null");
		}
		else
		{
			foreach (KeyboardShortcutPair playerKeyboardShortcut in NetworkScript.player.playerBelongings.playerKeyboardShortcuts)
			{
				KeyboardCommand keyboardCommand = playerKeyboardShortcut.commandIndex;
				if (!this.keyboardShortcuts.ContainsKey(keyboardCommand))
				{
					continue;
				}
				ShortcutCommandItem item = this.keyboardShortcuts.get_Item(keyboardCommand);
				item.dbKey = playerKeyboardShortcut.dbKeyCode;
				this.ReadKeyboardCommandItem(item);
			}
		}
	}

	public bool IsCommandUsed(KeyboardCommand cmd, int detectType = 0)
	{
		bool isPressed;
		try
		{
			if (this.keyboardShortcuts.ContainsKey(cmd))
			{
				switch (detectType)
				{
					case 0:
					{
						isPressed = this.keyboardShortcuts.get_Item(cmd).IsPressed;
						return isPressed;
					}
					case 1:
					{
						isPressed = this.keyboardShortcuts.get_Item(cmd).IsPressedUp;
						return isPressed;
					}
					case 2:
					{
						isPressed = this.keyboardShortcuts.get_Item(cmd).IsPressedDown;
						return isPressed;
					}
				}
				isPressed = false;
			}
			else
			{
				isPressed = false;
			}
		}
		catch (Exception exception)
		{
			Debug.Log(string.Concat("Unable to check if key is pressed with error : ", exception.ToString()));
			isPressed = false;
		}
		return isPressed;
	}

	private bool IsInputTaken(bool isShiftDown, bool isCtrlDown, KeyCode key, out bool isOverridingSecondary)
	{
		KeyboardShortcuts.<IsInputTaken>c__AnonStorey34 variable = null;
		bool isPressed = false;
		isOverridingSecondary = false;
		ShortcutCommandItem item = this.keyboardShortcuts.get_Item((KeyboardCommand)this.currentCommandIndex);
		ShortcutCommandItem[] array = Enumerable.ToArray<ShortcutCommandItem>(Enumerable.Where<ShortcutCommandItem>(this.keyboardShortcuts.get_Values(), new Func<ShortcutCommandItem, bool>(variable, (ShortcutCommandItem p) => ((!this.itemForCheck.isInBase ? p.isInBase : !p.isInBase) ? false : p.command != this.itemForCheck.command))));
		for (int i = 0; i < (int)array.Length; i++)
		{
			isPressed = isPressed | array[i].IsPressed;
		}
		if (!isPressed)
		{
			if (this.currentKeyNumber != 1)
			{
				if (!item.isKeyOneUsingShift && !item.isKeyOneUsingCtrl && !isShiftDown && !isCtrlDown && key == item.keyCodeOne)
				{
					isPressed = true;
				}
				if (item.isKeyOneUsingShift && !item.isKeyOneUsingCtrl && isShiftDown && !isCtrlDown && key == item.keyCodeOne)
				{
					isPressed = true;
				}
				if (item.isKeyOneUsingShift && item.isKeyOneUsingCtrl && isShiftDown && isCtrlDown && key == item.keyCodeOne)
				{
					isPressed = true;
				}
			}
			else
			{
				if (!item.isKeyTwoUsingShift && !item.isKeyTwoUsingCtrl && !isShiftDown && !isCtrlDown && key == item.keyCodeTwo)
				{
					isPressed = false;
					isOverridingSecondary = true;
				}
				if (item.isKeyTwoUsingShift && !item.isKeyTwoUsingCtrl && isShiftDown && !isCtrlDown && key == item.keyCodeTwo)
				{
					isPressed = false;
					isOverridingSecondary = true;
				}
				if (item.isKeyTwoUsingShift && item.isKeyTwoUsingCtrl && isShiftDown && isCtrlDown && key == item.keyCodeTwo)
				{
					isPressed = false;
					isOverridingSecondary = true;
				}
			}
		}
		return isPressed;
	}

	private bool IsUnAllowedKey(KeyCode keyCode)
	{
		if (keyCode != 306 && keyCode != 305 && keyCode != 308 && keyCode != 307 && keyCode != 323 && keyCode != 324 && keyCode != 8)
		{
			return false;
		}
		return true;
	}

	public void LeaveOldKey(GuiButtonResizeable btn = null)
	{
		string str;
		if (this.keyBoardUpdateButton == null)
		{
			return;
		}
		ShortcutCommandItem item = this.keyboardShortcuts.get_Item((KeyboardCommand)this.currentCommandIndex);
		if (this.currentKeyNumber != 1)
		{
			str = this.CheckKeyCodeName(item.keyCodeTwo.ToString());
			if (item.isKeyTwoUsingShift)
			{
				str = string.Concat("Shift + ", str);
			}
			if (item.isKeyTwoUsingCtrl)
			{
				str = string.Concat("Ctrl + ", str);
			}
		}
		else
		{
			str = this.CheckKeyCodeName(item.keyCodeOne.ToString());
			if (item.isKeyOneUsingShift)
			{
				str = string.Concat("Shift + ", str);
			}
			if (item.isKeyOneUsingCtrl)
			{
				str = string.Concat("Ctrl + ", str);
			}
		}
		if (btn == null || (object)btn != (object)this.keyBoardUpdateButton)
		{
			this.keyBoardUpdateButton.SetSmallBlueTexture();
			this.keyBoardUpdateButton.IsClicked = false;
			this.keyBoardUpdateButton.Caption = str;
		}
		NetworkScript.player.shipScript.isChangingKey = false;
		this.isShiftPressed = false;
		this.isControlPressed = false;
		this.stopListenforKey = false;
	}

	public void OnChangeKeyboardShortcutClicked(EventHandlerParam prm)
	{
		this.LeaveOldKey(prm.customData2 as GuiButtonResizeable);
		int num = (int)prm.customData;
		int num1 = 0;
		int num2 = 255;
		this.keyBoardUpdateButton = prm.customData2 as GuiButtonResizeable;
		num1 = (num <= 2000 ? 1 : 2);
		num2 = num - (num <= 2000 ? 1000 : 2000);
		this.currentCommandIndex = num2;
		this.currentKeyNumber = num1;
		this.keyBoardUpdateButton.SetSmallOrangeTexture();
		this.keyBoardUpdateButton.IsClicked = true;
		this.keyBoardUpdateButton.Caption = StaticData.Translate("key_keyboard_press_key");
		((__SystemWindow)AndromedaGui.mainWnd.activeWindow).customOnGUIAction = new Action(this, KeyboardShortcuts.ChangeKeyListener);
		NetworkScript.player.shipScript.isChangingKey = true;
		if (num2 == 41)
		{
			if (KeyboardShortcuts.<>f__am$cache9 == null)
			{
				KeyboardShortcuts.<>f__am$cache9 = new Action(null, () => {
					if (AndromedaGui.mainWnd != null)
					{
						AndromedaGui.gui.RemoveWindow(AndromedaGui.mainWnd.feedbackButtonWindow.handler);
						AndromedaGui.mainWnd.CreateFeedbackButton();
					}
				});
			}
			this.OnKeyChangedAction = KeyboardShortcuts.<>f__am$cache9;
		}
	}

	private void ProcessKeyboardInput(Event e)
	{
		string str;
		KeyCode _keyCode;
		if (e.get_type() != null)
		{
			_keyCode = e.get_keyCode();
		}
		else
		{
			_keyCode = 323 + e.get_button();
		}
		KeyCode keyCode = _keyCode;
		if (keyCode == 306 || keyCode == 305)
		{
			this.stopListenforKey = false;
		}
		else if (keyCode == 323 || keyCode == 324)
		{
			if (((__SystemWindow)AndromedaGui.mainWnd.activeWindow).IsOtherButtonClicked)
			{
				this.StopListeningForKey();
			}
		}
		else if (this.IsUnAllowedKey(keyCode))
		{
			this.keyBoardUpdateButton.SetSmallOrangeTexture();
			this.keyBoardUpdateButton.IsClicked = true;
			this.keyBoardUpdateButton.Caption = StaticData.Translate("key_keyboard_unallowed_keycode");
			this.stopListenforKey = false;
		}
		else if (keyCode == 27)
		{
			ShortcutCommandItem item = this.keyboardShortcuts.get_Item((KeyboardCommand)this.currentCommandIndex);
			if (this.currentKeyNumber != 1)
			{
				str = this.CheckKeyCodeName(item.keyCodeTwo.ToString());
				if (item.isKeyTwoUsingShift)
				{
					str = string.Concat("Shift + ", str);
				}
				if (item.isKeyTwoUsingCtrl)
				{
					str = string.Concat("Ctrl + ", str);
				}
			}
			else
			{
				str = this.CheckKeyCodeName(item.keyCodeOne.ToString());
				if (item.isKeyOneUsingShift)
				{
					str = string.Concat("Shift + ", str);
				}
				if (item.isKeyOneUsingCtrl)
				{
					str = string.Concat("Ctrl + ", str);
				}
			}
			this.keyBoardUpdateButton.SetSmallBlueTexture();
			this.keyBoardUpdateButton.IsClicked = false;
			this.keyBoardUpdateButton.Caption = str;
			this.stopListenforKey = true;
		}
		else if (keyCode != 271 && keyCode != 13 && keyCode != 8)
		{
			bool flag = false;
			if (!this.IsInputTaken(this.isShiftPressed, this.isControlPressed, keyCode, out flag))
			{
				string str1 = this.CheckKeyCodeName(keyCode.ToString());
				if (this.isShiftPressed)
				{
					str1 = string.Concat("Shift + ", str1);
				}
				if (this.isControlPressed)
				{
					str1 = string.Concat("Ctrl + ", str1);
				}
				this.keyBoardUpdateButton.SetSmallBlueTexture();
				this.keyBoardUpdateButton.IsClicked = false;
				this.keyBoardUpdateButton.Caption = str1;
				this.stopListenforKey = true;
				this.UpdateKey(this.currentCommandIndex, this.currentKeyNumber, keyCode, this.isShiftPressed, this.isControlPressed);
				if (flag)
				{
					this.UpdateKey(this.currentCommandIndex, 2, 0, false, false);
					__SystemWindow _SystemWindow = AndromedaGui.mainWnd.activeWindow as __SystemWindow;
					float _y = _SystemWindow.controlsScroller.scrollerTumbRect.get_y();
					_SystemWindow.OnControlsClick(null);
					_SystemWindow.controlsScroller.MooveTumb(0f, _y);
				}
				if (this.currentCommandIndex >= 17 && this.currentCommandIndex <= 25 && this.currentKeyNumber == 1)
				{
					AndromedaGui.mainWnd.ReCreateQuickSlotsMenu();
				}
				if (this.currentCommandIndex == 10 && this.currentKeyNumber == 1 && NetworkScript.player != null && NetworkScript.player.shipScript != null && NetworkScript.player.shipScript.comm != null)
				{
					NetworkScript.player.shipScript.comm.PopulateActionButtons();
				}
			}
			else
			{
				this.keyBoardUpdateButton.SetSmallOrangeTexture();
				this.keyBoardUpdateButton.IsClicked = true;
				this.keyBoardUpdateButton.Caption = StaticData.Translate("key_keyboard_key_already_in_use");
				this.stopListenforKey = false;
			}
		}
		else if (this.currentKeyNumber != 1)
		{
			this.stopListenforKey = true;
			this.keyBoardUpdateButton.SetSmallBlueTexture();
			this.keyBoardUpdateButton.IsClicked = false;
			this.keyBoardUpdateButton.Caption = "N/A";
			this.UpdateKey(this.currentCommandIndex, this.currentKeyNumber, 0, false, false);
		}
		else
		{
			this.keyBoardUpdateButton.SetSmallOrangeTexture();
			this.keyBoardUpdateButton.IsClicked = true;
			this.keyBoardUpdateButton.Caption = StaticData.Translate("key_keyboard_primary_cannot_be_null");
			this.stopListenforKey = false;
		}
	}

	private void ReadKeyboardCommandItem(ShortcutCommandItem item)
	{
		try
		{
			if (item.dbKey != 0)
			{
				item.keyCodeOne = (int)(item.dbKey % (long)10000);
				item.keyCodeTwo = (int)(item.dbKey / (long)10000 % (long)10000);
				if (item.keyCodeOne <= 1000)
				{
					item.isKeyOneUsingCtrl = false;
				}
				else
				{
					item.isKeyOneUsingCtrl = true;
					ShortcutCommandItem shortcutCommandItem = item;
					shortcutCommandItem.keyCodeOne = shortcutCommandItem.keyCodeOne - 1000;
				}
				if (item.keyCodeTwo <= 1000)
				{
					item.isKeyTwoUsingCtrl = false;
				}
				else
				{
					item.isKeyTwoUsingCtrl = true;
					ShortcutCommandItem shortcutCommandItem1 = item;
					shortcutCommandItem1.keyCodeTwo = shortcutCommandItem1.keyCodeTwo - 1000;
				}
				if (item.keyCodeOne <= 500)
				{
					item.isKeyOneUsingShift = false;
				}
				else
				{
					item.isKeyOneUsingShift = true;
					ShortcutCommandItem shortcutCommandItem2 = item;
					shortcutCommandItem2.keyCodeOne = shortcutCommandItem2.keyCodeOne - 500;
				}
				if (item.keyCodeTwo <= 500)
				{
					item.isKeyTwoUsingShift = false;
				}
				else
				{
					item.isKeyTwoUsingShift = true;
					ShortcutCommandItem shortcutCommandItem3 = item;
					shortcutCommandItem3.keyCodeTwo = shortcutCommandItem3.keyCodeTwo - 500;
				}
			}
			else
			{
				Debug.Log("Command item has no db key assigned.Probably item doesn't has its keyboard keycodes changed from the default ones");
			}
		}
		catch (Exception exception)
		{
			Debug.Log(string.Concat("Unable to read keyboard shortcut item with error : ", exception.ToString()));
		}
	}

	public void RestoreDefaultShortcuts(bool sendToServer = false)
	{
		// 
		// Current member / type: System.Void KeyboardShortcuts::RestoreDefaultShortcuts(System.Boolean)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void RestoreDefaultShortcuts(System.Boolean)
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
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 481
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 83
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

	private void StopListeningForKey()
	{
		NetworkScript.player.shipScript.isChangingKey = false;
		((__SystemWindow)AndromedaGui.mainWnd.activeWindow).customOnGUIAction = null;
		this.isShiftPressed = false;
		this.isControlPressed = false;
	}

	private void UpdateKey(int commandIndex, int keyNumber, KeyCode newKeyCode, bool isShiftUsed, bool isCtrlUsed)
	{
		// 
		// Current member / type: System.Void KeyboardShortcuts::UpdateKey(System.Int32,System.Int32,UnityEngine.KeyCode,System.Boolean,System.Boolean)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void UpdateKey(System.Int32,System.Int32,UnityEngine.KeyCode,System.Boolean,System.Boolean)
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
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 481
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 83
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
}