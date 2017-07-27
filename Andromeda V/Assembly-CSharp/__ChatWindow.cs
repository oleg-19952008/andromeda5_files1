using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class __ChatWindow : GuiWindow
{
	private const int FONT_SIZE = 12;

	private static string factionChatAssetName;

	public static bool isOpen;

	public static bool isCalledFromNewScene;

	public bool firstPrivateChat = true;

	public DateTime nextBlinkerPrivate;

	private BlinkFx fxSmallChat;

	public bool IsPrivateChatStarted;

	public static __ChatWindow wnd;

	private static ChatItems universeChat;

	private static ChatItems partyChat;

	private static ChatItems guildChat;

	private static ChatItems factionChat;

	private static ChatItems languageChat;

	private static ChatItems privateChat;

	public static ChatItems activeChat;

	public static ChatItems lastChatChecked;

	public static GuiButtonFixed btnPrivateChat;

	public static GuiButtonFixed btnUniverseChat;

	public static GuiButtonFixed btnFactionChat;

	public static GuiButtonFixed btnGuildChat;

	public static GuiButtonFixed btnPartyChat;

	public static GuiButtonFixed btnShowHide;

	public static GuiButtonFixed btnLanguageChat;

	public static GuiButtonFixed infoIcon;

	public static GuiButtonFixed btnChangeFraction;

	public static GuiLabel dummyLabel;

	private GuiButtonFixed btnDummtToolTip;

	private GuiButton btnName;

	public static GuiTexture txLanguageChat;

	public GuiTexture frame_bottom;

	public GuiWindow labelMenu;

	public GuiScrollingContainer chatScroll;

	public List<DateTime> timeStampMessagesUniverse;

	public List<DateTime> timeStampMessagesPrivate;

	public List<DateTime> timeStampMessagesGuild;

	public List<DateTime> timeStampMessagesParty;

	public List<DateTime> timeStampMessagesFaction;

	public List<DateTime> timeStampMessagesLanguage;

	public GuiTextBox tbSendMessage;

	private GuiButtonFixed btnSend;

	private List<GuiElement> stuffToRemove = new List<GuiElement>();

	private List<GuiElement> labelMenuCrapToRemove = new List<GuiElement>();

	private bool isPlayerChatAdmin;

	private static bool isbtnNameClicked;

	private static bool isbtnLabelMenuCancelClicked;

	private static bool isbtnLabelMenuProfileClicked;

	private static bool isbtnLabelMenuReportClicked;

	private static bool isbtnLabelMenuPrivateChatClicked;

	public static bool isNotOpenHasUnreadMessage;

	public static bool isMessageRecieved;

	public static bool isSmallChat;

	public bool spamAlert;

	public static bool firstLabelMenu;

	public bool isLabelMenuOpen;

	public static float chatScrolY;

	public string LangIconNml;

	public string LangIconClk;

	public string LangIconHvr;

	public string fractionIcon;

	private byte adminSenderFraction;

	private string message = string.Empty;

	private ChatMessage onChatResponce;

	public long privateChatPlayerID;

	public static Dictionary<int, float> scrollerPosition;

	public static List<float> labelPositionY;

	private static SortedList<long, ChatItems> privateChats;

	private GuiLabel lblReciever;

	private GuiButtonFixed btnReceiverList;

	private GuiScrollingContainer receiversScollerContainer;

	private static List<string> receiversList;

	private GuiButtonFixed btnReceiverName;

	private GuiLabel lblDummy = new GuiLabel();

	private GuiTexture txReceiverContainer;

	public GuiButtonResizeable btnDropDownList;

	public string playerID;

	private bool isSentMessage;

	public bool isRussianServer = true;

	private static bool isOnFocus;

	static __ChatWindow()
	{
		__ChatWindow.isOpen = true;
		__ChatWindow.isCalledFromNewScene = false;
		__ChatWindow.wnd = null;
		ChatItems chatItem = new ChatItems()
		{
			items = new List<ChatItem>(),
			onScreen = new List<ChatItem>(),
			chatType = 1
		};
		__ChatWindow.universeChat = chatItem;
		chatItem = new ChatItems()
		{
			items = new List<ChatItem>(),
			onScreen = new List<ChatItem>(),
			chatType = 3
		};
		__ChatWindow.partyChat = chatItem;
		chatItem = new ChatItems()
		{
			items = new List<ChatItem>(),
			onScreen = new List<ChatItem>(),
			chatType = 2
		};
		__ChatWindow.guildChat = chatItem;
		chatItem = new ChatItems()
		{
			items = new List<ChatItem>(),
			onScreen = new List<ChatItem>(),
			chatType = 5
		};
		__ChatWindow.factionChat = chatItem;
		chatItem = new ChatItems()
		{
			items = new List<ChatItem>(),
			onScreen = new List<ChatItem>(),
			chatType = 6
		};
		__ChatWindow.languageChat = chatItem;
		chatItem = new ChatItems()
		{
			items = new List<ChatItem>(),
			onScreen = new List<ChatItem>(),
			chatType = 4
		};
		__ChatWindow.privateChat = chatItem;
		__ChatWindow.isbtnNameClicked = false;
		__ChatWindow.isbtnLabelMenuCancelClicked = false;
		__ChatWindow.isbtnLabelMenuProfileClicked = false;
		__ChatWindow.isbtnLabelMenuReportClicked = false;
		__ChatWindow.isbtnLabelMenuPrivateChatClicked = false;
		__ChatWindow.isNotOpenHasUnreadMessage = false;
		__ChatWindow.isMessageRecieved = false;
		__ChatWindow.isSmallChat = false;
		__ChatWindow.firstLabelMenu = true;
		__ChatWindow.labelPositionY = new List<float>();
		__ChatWindow.privateChats = new SortedList<long, ChatItems>();
		__ChatWindow.receiversList = new List<string>();
		__ChatWindow.isOnFocus = false;
	}

	public __ChatWindow()
	{
		this.customOnGUIAction = new Action(this, __ChatWindow.OnTrackEnterKey);
	}

	public void AddAndFocusPersonalChat(long playerId)
	{
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = playerId,
			customData2 = NetworkScript.clientSideClientsList.get_Item(playerId).vessel.playerName
		};
		this.OnbtnLabelMenuStartChat(eventHandlerParam);
	}

	private static void AddChatItem(ChatItems items, ChatMessage data)
	{
		List<ChatItem> list = items.items;
		ChatItem chatItem = new ChatItem()
		{
			text = data.text,
			time = DateTime.get_Now(),
			playerName = data.senderName,
			playerId = data.senderId,
			sendByAdmin = data.sendByAdmin,
			isSystemMessage = data.isSystemMessage,
			senderFactionId = data.senderFractionId
		};
		list.Insert(0, chatItem);
		items.y = 0f;
	}

	public void AddCustomMessage(ChatType chatRoom, string message, Color messageColor)
	{
		ChatItem chatItem = new ChatItem()
		{
			text = message,
			time = DateTime.get_Now(),
			playerName = NetworkScript.player.playerBelongings.playerName,
			playerId = NetworkScript.player.playId,
			colorName = messageColor,
			colorText = messageColor,
			senderFactionId = NetworkScript.player.vessel.fractionId,
			isCustomMessage = true
		};
		ChatItem chatItem1 = chatItem;
		ChatItems chatItem2 = null;
		switch (chatRoom)
		{
			case 1:
			{
				__ChatWindow.universeChat.items.Insert(0, chatItem1);
				chatItem2 = __ChatWindow.universeChat;
				break;
			}
			case 2:
			{
				__ChatWindow.guildChat.items.Insert(0, chatItem1);
				chatItem2 = __ChatWindow.guildChat;
				break;
			}
			case 3:
			{
				__ChatWindow.partyChat.items.Insert(0, chatItem1);
				chatItem2 = __ChatWindow.partyChat;
				break;
			}
			case 4:
			{
				break;
			}
			case 5:
			{
				__ChatWindow.factionChat.items.Insert(0, chatItem1);
				chatItem2 = __ChatWindow.factionChat;
				break;
			}
			case 6:
			{
				__ChatWindow.languageChat.items.Insert(0, chatItem1);
				chatItem2 = __ChatWindow.languageChat;
				break;
			}
			default:
			{
				goto case 4;
			}
		}
		this.BuildChatRoom(chatItem2, chatItem2.chatType == __ChatWindow.activeChat.chatType);
	}

	private void AddDuplicateMessage()
	{
		// 
		// Current member / type: System.Void __ChatWindow::AddDuplicateMessage()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void AddDuplicateMessage()
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

	private void AddNewReceiver(string playerName)
	{
		if (!__ChatWindow.receiversList.Contains(playerName))
		{
			__ChatWindow.receiversList.Add(playerName);
		}
		if (!__ChatWindow.isSmallChat && __ChatWindow.btnPrivateChat != null && __ChatWindow.btnPrivateChat.IsClicked)
		{
			this.InitReceiverWeb();
			if (this.btnDropDownList != null)
			{
				this.btnDropDownList.Caption = __ChatWindow.privateChat.receiverName;
			}
		}
	}

	private static ChatItems AddNonPrivateMessage(ChatMessage data)
	{
		bool flag = true;
		if (data.chatType == 1)
		{
			if (!__ChatWindow.btnUniverseChat.IsClicked)
			{
				flag = false;
			}
			__ChatWindow.wnd.ManageSystemMessage(1, data);
			__ChatWindow.wnd.BuildChatRoom(__ChatWindow.universeChat, flag);
			return __ChatWindow.universeChat;
		}
		if (data.chatType == 2)
		{
			if (!__ChatWindow.btnGuildChat.IsClicked)
			{
				flag = false;
			}
			__ChatWindow.AddChatItem(__ChatWindow.guildChat, data);
			__ChatWindow.wnd.BuildChatRoom(__ChatWindow.guildChat, flag);
			return __ChatWindow.guildChat;
		}
		if (data.chatType == 5)
		{
			if (!__ChatWindow.btnFactionChat.IsClicked)
			{
				flag = false;
			}
			__ChatWindow.AddChatItem(__ChatWindow.factionChat, data);
			__ChatWindow.wnd.BuildChatRoom(__ChatWindow.factionChat, flag);
			return __ChatWindow.factionChat;
		}
		if (data.chatType != 6 || __ChatWindow.wnd.isRussianServer)
		{
			if (!__ChatWindow.btnPartyChat.IsClicked)
			{
				flag = false;
			}
			__ChatWindow.AddChatItem(__ChatWindow.partyChat, data);
			__ChatWindow.wnd.BuildChatRoom(__ChatWindow.partyChat, flag);
			return __ChatWindow.partyChat;
		}
		if (!__ChatWindow.btnLanguageChat.IsClicked)
		{
			flag = false;
		}
		__ChatWindow.wnd.ManageSystemMessage(6, data);
		__ChatWindow.wnd.BuildChatRoom(__ChatWindow.languageChat, flag);
		return __ChatWindow.languageChat;
	}

	public void AddSpamMessage(string message)
	{
		// 
		// Current member / type: System.Void __ChatWindow::AddSpamMessage(System.String)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void AddSpamMessage(System.String)
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

	private void AssignBackgroundTexure(ChatType chatType)
	{
		string empty = string.Empty;
		switch (chatType)
		{
			case 1:
			{
				empty = "frame_main_U";
				break;
			}
			case 2:
			{
				empty = "frame_main_GD";
				break;
			}
			case 3:
			{
				empty = "frame_main_P";
				break;
			}
			case 4:
			{
				empty = "frame_main_PR";
				break;
			}
			case 5:
			{
				empty = "frame_main_G";
				break;
			}
			case 6:
			{
				empty = "frame_main_L";
				break;
			}
		}
		if (__ChatWindow.btnLanguageChat == null || this.isRussianServer)
		{
			empty = string.Concat(empty, "_RU");
		}
		base.SetBackgroundTexture("chat", empty);
	}

	public void AssignLastChat()
	{
		if (__ChatWindow.btnUniverseChat.IsClicked || __ChatWindow.activeChat.chatType == 1)
		{
			__ChatWindow.lastChatChecked = __ChatWindow.universeChat;
		}
		if (__ChatWindow.btnFactionChat.IsClicked || __ChatWindow.activeChat.chatType == 5)
		{
			__ChatWindow.lastChatChecked = __ChatWindow.factionChat;
		}
		if (!this.isRussianServer && __ChatWindow.btnLanguageChat.IsClicked || __ChatWindow.activeChat.chatType == 6)
		{
			__ChatWindow.lastChatChecked = __ChatWindow.languageChat;
		}
		if (__ChatWindow.btnPartyChat.IsClicked || __ChatWindow.activeChat.chatType == 3)
		{
			__ChatWindow.lastChatChecked = __ChatWindow.partyChat;
		}
		if (__ChatWindow.btnGuildChat.IsClicked || __ChatWindow.activeChat.chatType == 2)
		{
			__ChatWindow.lastChatChecked = __ChatWindow.guildChat;
		}
		if (__ChatWindow.btnPrivateChat.IsClicked || __ChatWindow.activeChat.chatType == 4)
		{
			__ChatWindow.lastChatChecked = __ChatWindow.privateChat;
		}
	}

	private void BuildChatRoom(ChatItems Chat, bool isClearNeeded)
	{
		EventHandlerParam eventHandlerParam;
		float single = 194f;
		if (Chat.callToInsertToChatRoom)
		{
			this.InsertToChatRoom(Chat);
		}
		else
		{
			if (isClearNeeded)
			{
				this.chatScroll.Claer();
			}
			if (Chat.items.get_Count() > 0)
			{
				foreach (ChatItem item in Chat.items)
				{
					GuiLabel guiLabel = new GuiLabel();
					this.btnName = new GuiButton();
					GuiTexture guiTexture = new GuiTexture();
					GuiLabel fontMedium = new GuiLabel();
					__ChatWindow.dummyLabel = new GuiLabel()
					{
						FontSize = 12,
						Alignment = 6,
						WordWrap = true,
						Font = GuiLabel.FontMedium
					};
					this.btnName.FontSize = 12;
					guiLabel.FontSize = 12;
					guiLabel.Font = GuiLabel.FontMedium;
					guiLabel.WordWrap = true;
					fontMedium.FontSize = 12;
					fontMedium.Font = GuiLabel.FontMedium;
					this.btnName.boundries = new Rect(6f, 0f, 260f, 14f);
					guiLabel.boundries = new Rect(6f, 0f, 260f, 14f);
					fontMedium.boundries = new Rect(6f, 0f, 260f, 14f);
					if (Chat.chatType != 4 || item.playerId != NetworkScript.player.vessel.playerId)
					{
						fontMedium.text = string.Format("({0:HH:mm}){1}: ", item.time, item.playerName);
						__ChatWindow.dummyLabel.text = this.btnName.Caption;
						guiLabel.text = string.Concat(new string(' ', (int)(fontMedium.TextWidth / 2.9f) + 1), item.text);
						if (item.sendByAdmin)
						{
							this.btnName.boundries.set_width(260f);
							guiTexture.SetTexture("chat", "admin_icon");
							guiLabel.boundries = new Rect(6f, 0f, 260f, 14f);
							eventHandlerParam = new EventHandlerParam()
							{
								customData = StaticData.Translate("key_chat_admin"),
								customData2 = guiTexture
							};
							guiTexture.tooltipWindowParam = eventHandlerParam;
							guiTexture.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
							guiTexture.isHoverAware = true;
							guiTexture.boundries.set_width(14f);
							guiTexture.boundries.set_height(14f);
							this.btnName.boundries.set_x(guiLabel.boundries.get_x());
							fontMedium.text = string.Concat(string.Format("({0:HH:mm})", item.time), new string(' ', 5), item.playerName, ":");
							__ChatWindow.dummyLabel.text = string.Format("({0:HH:mm})", item.time);
							guiTexture.boundries.set_x(__ChatWindow.dummyLabel.TextWidth + 7f);
							__ChatWindow.dummyLabel.text = this.btnName.Caption;
							guiLabel.text = string.Concat(new string(' ', (int)(fontMedium.TextWidth / 2.9f)), item.text);
						}
						if (item.isSystemMessage)
						{
							item.colorText = Color.get_red();
							item.colorName = Color.get_red();
						}
						else if (NetworkScript.player.vessel.fractionId != item.senderFactionId)
						{
							item.colorText = GuiNewStyleBar.blueColor;
							item.colorName = GuiNewStyleBar.purpleColor;
						}
					}
					else
					{
						if (item.receiverName == null || item.receiverName == " ")
						{
							item.receiverName = __ChatWindow.activeChat.receiverName;
							item.recieverID = __ChatWindow.activeChat.recieverID;
						}
						fontMedium.text = string.Format("({0:HH:mm}){1}->{2}: ", item.time, item.playerName, item.receiverName);
						__ChatWindow.dummyLabel.Clipping = 1;
						__ChatWindow.dummyLabel.text = this.btnName.Caption;
						item.timeHeight = __ChatWindow.dummyLabel.TextHeight;
						guiLabel.text = new string(' ', (int)(fontMedium.TextWidth / 2.9f));
						GuiLabel guiLabel1 = guiLabel;
						guiLabel1.text = string.Concat(guiLabel1.text, item.text);
						if (item.sendByAdmin)
						{
							this.btnName.boundries.set_width(260f);
							guiTexture.SetTexture("chat", "admin_icon");
							guiLabel.boundries = new Rect(6f, 0f, 260f, 14f);
							eventHandlerParam = new EventHandlerParam()
							{
								customData = StaticData.Translate("key_chat_admin"),
								customData2 = guiTexture
							};
							guiTexture.tooltipWindowParam = eventHandlerParam;
							guiTexture.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
							guiTexture.isHoverAware = true;
							guiTexture.boundries.set_width(14f);
							guiTexture.boundries.set_height(14f);
							this.btnName.boundries.set_x(guiLabel.boundries.get_x());
							string str = string.Concat(item.playerName, "->", item.receiverName, ":");
							fontMedium.text = string.Concat(string.Format("({0:HH:mm})", item.time), new string(' ', 5), str);
							__ChatWindow.dummyLabel.text = string.Format("({0:HH:mm})", item.time);
							guiTexture.boundries.set_x(__ChatWindow.dummyLabel.TextWidth + 7f);
							__ChatWindow.dummyLabel.text = this.btnName.Caption;
							guiLabel.text = new string(' ', (int)(fontMedium.TextWidth / 2.9f));
							GuiLabel guiLabel2 = guiLabel;
							guiLabel2.text = string.Concat(guiLabel2.text, item.text);
						}
					}
					if (NetworkScript.player.playId != item.playerId)
					{
						if (!item.isSystemMessage && NetworkScript.player.vessel.fractionId == item.senderFactionId)
						{
							item.colorText = GuiNewStyleBar.blueColor;
							item.colorName = GuiNewStyleBar.orangeColor;
						}
						this.btnName.textColorNormal = item.colorName;
						this.btnName.textColorHover = Color.get_white();
						this.btnName.Clicked = new Action<EventHandlerParam>(this, __ChatWindow.BuildLabelMenuWnd);
						this.btnName.eventHandlerParam.customData2 = __ChatWindow.dummyLabel.TextWidth + 2f;
						this.btnName.eventHandlerParam.customData = item;
						GuiButton guiButton = this.btnName;
						HoverParamPair hoverParamPair = new HoverParamPair()
						{
							guiLbl = fontMedium,
							originalColor = item.colorName
						};
						guiButton.hoverParam = hoverParamPair;
						fontMedium.TextColor = item.colorName;
						this.btnName.Hovered = new Action<object, bool>(this, __ChatWindow.OnBtnNameHover);
					}
					else
					{
						if (!item.isCustomMessage)
						{
							item.colorText = Color.get_green();
							item.colorName = Color.get_green();
						}
						fontMedium.TextColor = item.colorName;
					}
					if (item.isSpam)
					{
						item.colorText = Color.get_yellow();
						item.colorName = Color.get_yellow();
						this.btnName.Caption = string.Empty;
						fontMedium.text = string.Empty;
						this.btnName.boundries.set_width((float)(this.btnName.Caption.get_Length() * 6));
						guiLabel.text = string.Format("({0:HH:mm}){1} ", item.time, item.text);
					}
					if (item.isCustomMessage)
					{
						this.btnName.Caption = string.Empty;
						fontMedium.text = string.Empty;
						this.btnName.boundries.set_width((float)(this.btnName.Caption.get_Length() * 6));
						guiLabel.text = string.Format("({0:HH:mm}){1} ", item.time, item.text);
					}
					if (item.isDuplicate)
					{
						item.colorText = Color.get_yellow();
						item.colorName = Color.get_yellow();
						this.btnName.Caption = string.Empty;
						fontMedium.text = string.Empty;
						this.btnName.boundries.set_width((float)(this.btnName.Caption.get_Length() * 6));
						guiLabel.text = string.Format("({0:HH:mm}){1} ", item.time, StaticData.Translate("key_chat_you_have_already_said_that"));
					}
					this.btnName.Caption = string.Empty;
					base.RemoveGuiElement(__ChatWindow.dummyLabel);
					this.btnName.Caption = " ";
					guiLabel.FontSize = 12;
					guiLabel.WordWrap = true;
					item.timeHeight = guiLabel.TextHeight;
					item.height = guiLabel.TextHeight;
					single = single - item.height;
					guiLabel.TextColor = item.colorText;
					guiLabel.Y = single;
					this.btnName.Y = single;
					item.y = single;
					fontMedium.Y = single;
					guiTexture.boundries.set_y(item.y);
					if (single <= 10f)
					{
						Chat.callToInsertToChatRoom = true;
						Chat.firstLineWithScroller = true;
					}
					if (Chat.chatType == 1 && __ChatWindow.btnUniverseChat.IsClicked)
					{
						this.chatScroll.AddContent(fontMedium);
						this.chatScroll.AddContent(guiLabel);
						if (item.sendByAdmin)
						{
							this.chatScroll.AddContent(guiTexture);
						}
						if (NetworkScript.player.playId != item.playerId)
						{
							this.chatScroll.AddContent(this.btnName);
						}
					}
					if (Chat.chatType == 2 && __ChatWindow.btnGuildChat.IsClicked)
					{
						this.chatScroll.AddContent(fontMedium);
						this.chatScroll.AddContent(guiLabel);
						if (item.sendByAdmin)
						{
							this.chatScroll.AddContent(guiTexture);
						}
						if (NetworkScript.player.playId != item.playerId)
						{
							this.chatScroll.AddContent(this.btnName);
						}
					}
					if (Chat.chatType == 3 && __ChatWindow.btnPartyChat.IsClicked)
					{
						this.chatScroll.AddContent(fontMedium);
						this.chatScroll.AddContent(guiLabel);
						if (item.sendByAdmin)
						{
							this.chatScroll.AddContent(guiTexture);
						}
						if (NetworkScript.player.playId != item.playerId)
						{
							this.chatScroll.AddContent(this.btnName);
						}
					}
					if (Chat.chatType == 5 && __ChatWindow.btnFactionChat.IsClicked)
					{
						this.chatScroll.AddContent(fontMedium);
						this.chatScroll.AddContent(guiLabel);
						if (item.sendByAdmin)
						{
							this.chatScroll.AddContent(guiTexture);
						}
						if (NetworkScript.player.playId != item.playerId)
						{
							this.chatScroll.AddContent(this.btnName);
						}
					}
					if (Chat.chatType == 6 && __ChatWindow.btnLanguageChat != null && __ChatWindow.btnLanguageChat.IsClicked)
					{
						this.chatScroll.AddContent(fontMedium);
						this.chatScroll.AddContent(guiLabel);
						if (item.sendByAdmin)
						{
							this.chatScroll.AddContent(guiTexture);
						}
						if (NetworkScript.player.playId != item.playerId)
						{
							this.chatScroll.AddContent(this.btnName);
						}
					}
					if (Chat.chatType != 4 || !__ChatWindow.btnPrivateChat.IsClicked)
					{
						continue;
					}
					this.chatScroll.AddContent(fontMedium);
					this.chatScroll.AddContent(guiLabel);
					if (item.sendByAdmin)
					{
						this.chatScroll.AddContent(guiTexture);
					}
					if (NetworkScript.player.playId == item.playerId)
					{
						continue;
					}
					this.chatScroll.AddContent(this.btnName);
				}
			}
		}
	}

	public void BuildLabelMenuWnd(EventHandlerParam prm)
	{
		this.isLabelMenuOpen = true;
		__ChatWindow.isbtnNameClicked = true;
		if (!__ChatWindow.firstLabelMenu)
		{
			this.OnbtnLabelMenuCancelClick(prm);
		}
		else
		{
			__ChatWindow.firstLabelMenu = false;
		}
		if (!__ChatWindow.isSmallChat && this.btnDropDownList != null && this.receiversScollerContainer != null)
		{
			base.RemoveGuiElement(this.receiversScollerContainer);
			this.receiversScollerContainer = null;
		}
		ChatItem chatItem = (ChatItem)prm.customData;
		long num = chatItem.playerId;
		string str = chatItem.playerName;
		if (chatItem.isSystemMessage)
		{
			return;
		}
		if (prm.button != EventHandlerParam.MouseButton.Right)
		{
			string str1 = "key_chat_report_player";
			string str2 = "key_chat_private";
			string str3 = "key_chat_cancel";
			string str4 = string.Format(StaticData.Translate("key_chat_admin_ban"), str, 4);
			string str5 = string.Format(StaticData.Translate("key_chat_admin_mute"), str, 4);
			string str6 = string.Format(StaticData.Translate("key_chat_admin_slap"), str);
			string str7 = "key_chat_party";
			this.labelMenu = new GuiWindow()
			{
				isHidden = false,
				zOrder = 120
			};
			this.labelMenu.SetBackgroundTexture("chat", "ActionFrame");
			this.labelMenu.boundries.set_width(117f);
			this.labelMenu.boundries.set_height(70f);
			this.labelMenu.boundries.set_x(this.boundries.get_x() + 150f);
			float _height = (float)Screen.get_height();
			Vector3 _mousePosition = Input.get_mousePosition();
			this.labelMenu.boundries.set_y(_height - _mousePosition.y - this.labelMenu.boundries.get_height() / 2f);
			GuiButtonFixed guiButtonFixed = null;
			GuiButtonFixed drawTooltipWindow = new GuiButtonFixed()
			{
				Caption = string.Empty
			};
			EventHandlerParam eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_main_screen_btn_profile"),
				customData2 = drawTooltipWindow
			};
			drawTooltipWindow.tooltipWindowParam = eventHandlerParam;
			drawTooltipWindow.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			drawTooltipWindow.Clicked = new Action<EventHandlerParam>(this, __ChatWindow.OnProfileClick);
			eventHandlerParam = new EventHandlerParam()
			{
				customData = str
			};
			drawTooltipWindow.eventHandlerParam = eventHandlerParam;
			if (!this.isRussianServer)
			{
				guiButtonFixed = new GuiButtonFixed()
				{
					Caption = string.Empty
				};
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate(str1),
					customData2 = guiButtonFixed
				};
				guiButtonFixed.tooltipWindowParam = eventHandlerParam;
				guiButtonFixed.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, __ChatWindow.OnReportClick);
				eventHandlerParam = new EventHandlerParam()
				{
					customData = num
				};
				guiButtonFixed.eventHandlerParam = eventHandlerParam;
				guiButtonFixed.isEnabled = true;
			}
			GuiButtonFixed action = new GuiButtonFixed()
			{
				Caption = string.Empty
			};
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate(str2),
				customData2 = action
			};
			action.tooltipWindowParam = eventHandlerParam;
			action.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			action.Clicked = new Action<EventHandlerParam>(this, __ChatWindow.OnbtnLabelMenuStartChat);
			eventHandlerParam = new EventHandlerParam()
			{
				customData = num,
				customData2 = str
			};
			action.eventHandlerParam = eventHandlerParam;
			action.isEnabled = true;
			GuiButtonFixed guiButtonFixed1 = new GuiButtonFixed()
			{
				Caption = string.Empty
			};
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate(str3),
				customData2 = guiButtonFixed1
			};
			guiButtonFixed1.tooltipWindowParam = eventHandlerParam;
			guiButtonFixed1.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			guiButtonFixed1.Clicked = new Action<EventHandlerParam>(this, __ChatWindow.OnbtnLabelMenuCancelClick);
			GuiButtonFixed drawTooltipWindow1 = new GuiButtonFixed()
			{
				Caption = string.Empty
			};
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate(str7),
				customData2 = drawTooltipWindow1
			};
			drawTooltipWindow1.tooltipWindowParam = eventHandlerParam;
			drawTooltipWindow1.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			eventHandlerParam = new EventHandlerParam()
			{
				customData = str
			};
			drawTooltipWindow1.eventHandlerParam = eventHandlerParam;
			drawTooltipWindow1.Clicked = new Action<EventHandlerParam>(this, __ChatWindow.SendPartyInvite);
			drawTooltipWindow1.isEnabled = true;
			drawTooltipWindow.X = 5f;
			drawTooltipWindow.Y = 5f;
			drawTooltipWindow.SetTexture("chat", "btnFriend");
			if (!this.isRussianServer)
			{
				guiButtonFixed.X = 32f;
				guiButtonFixed.Y = 5f;
				guiButtonFixed.SetTexture("chat", "btnReport");
			}
			action.X = (float)((this.isRussianServer || guiButtonFixed == null ? 32 : 59));
			action.Y = 5f;
			action.SetTexture("chat", "btnPrivateLabelMenuNml");
			action.SetTextureHover("chat", "btnLabeMenuPrivateClk");
			action.SetTextureClicked("chat", "btnLabeMenuPrivateClk");
			guiButtonFixed1.X = (float)((this.isRussianServer || guiButtonFixed == null ? 59 : 86));
			guiButtonFixed1.Y = 5f;
			guiButtonFixed1.SetTexture("chat", "btnCloseNewNml");
			guiButtonFixed1.SetTextureClicked("chat", "btnCloseNewClk");
			guiButtonFixed1.SetTextureHover("chat", "btnCloseNewClk");
			if (NetworkScript.player.isChatAdmin)
			{
				this.labelMenu.boundries.set_width(117f);
				GuiButtonFixed action1 = new GuiButtonFixed()
				{
					Caption = string.Empty
				};
				eventHandlerParam = new EventHandlerParam()
				{
					customData = str4,
					customData2 = action1
				};
				action1.tooltipWindowParam = eventHandlerParam;
				action1.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				action1.Clicked = new Action<EventHandlerParam>(this, __ChatWindow.OnbtnLabelMenuChatAdminCommand);
				eventHandlerParam = new EventHandlerParam()
				{
					customData = 0,
					customData2 = str
				};
				action1.eventHandlerParam = eventHandlerParam;
				GuiButtonFixed guiButtonFixed2 = new GuiButtonFixed()
				{
					Caption = string.Empty
				};
				eventHandlerParam = new EventHandlerParam()
				{
					customData = str5,
					customData2 = guiButtonFixed2
				};
				guiButtonFixed2.tooltipWindowParam = eventHandlerParam;
				guiButtonFixed2.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				guiButtonFixed2.Clicked = new Action<EventHandlerParam>(this, __ChatWindow.OnbtnLabelMenuChatAdminCommand);
				eventHandlerParam = new EventHandlerParam()
				{
					customData = 1,
					customData2 = str
				};
				guiButtonFixed2.eventHandlerParam = eventHandlerParam;
				GuiButtonFixed drawTooltipWindow2 = new GuiButtonFixed()
				{
					Caption = string.Empty
				};
				eventHandlerParam = new EventHandlerParam()
				{
					customData = str6,
					customData2 = guiButtonFixed2
				};
				drawTooltipWindow2.tooltipWindowParam = eventHandlerParam;
				drawTooltipWindow2.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				drawTooltipWindow2.Clicked = new Action<EventHandlerParam>(this, __ChatWindow.OnbtnLabelMenuChatAdminCommand);
				eventHandlerParam = new EventHandlerParam()
				{
					customData = 2,
					customData2 = str
				};
				drawTooltipWindow2.eventHandlerParam = eventHandlerParam;
				action1.SetTexture("chat", "btnBlockNml");
				action1.SetTextureHover("chat", "btnBlockHvr");
				action1.X = 5f;
				action1.Y = 32f;
				guiButtonFixed2.SetTexture("chat", "btnBlockNml");
				guiButtonFixed2.SetTextureHover("chat", "btnBlockHvr");
				guiButtonFixed2.X = 32f;
				guiButtonFixed2.Y = 32f;
				drawTooltipWindow2.SetTexture("chat", "btnBlockNml");
				drawTooltipWindow2.SetTextureHover("chat", "btnBlockHvr");
				drawTooltipWindow2.X = 59f;
				drawTooltipWindow2.Y = 32f;
				this.labelMenu.AddGuiElement(action1);
				this.labelMenu.AddGuiElement(guiButtonFixed2);
				this.labelMenu.AddGuiElement(drawTooltipWindow2);
			}
			this.labelMenu.AddGuiElement(drawTooltipWindow);
			if (!this.isRussianServer && guiButtonFixed != null)
			{
				this.labelMenu.AddGuiElement(guiButtonFixed);
			}
			this.labelMenu.AddGuiElement(action);
			this.labelMenu.AddGuiElement(guiButtonFixed1);
			AndromedaGui.gui.AddWindow(this.labelMenu);
			__ChatWindow.isOnFocus = false;
			__ChatWindow.wnd.LooseChatFocus();
		}
		else
		{
			__ChatWindow.isOnFocus = true;
			__ChatWindow.wnd.GainChatFocus();
			GuiTextBox guiTextBox = this.tbSendMessage;
			guiTextBox.text = string.Concat(guiTextBox.text, str);
			this.preDrawHandler = new Action<object>(this, __ChatWindow.SetCursorToEnd);
		}
	}

	private void CheckForDuplicateMessage(string txt)
	{
		float single = 1f;
		bool flag = false;
		if (__ChatWindow.activeChat.chatType == 1)
		{
			foreach (ChatItem item in __ChatWindow.universeChat.items)
			{
				if ((float)(DateTime.get_Now() - item.time).get_Minutes() >= single || !(item.playerName == NetworkScript.player.vessel.playerName))
				{
					flag = false;
				}
				else if (txt != item.text)
				{
					flag = false;
				}
				else
				{
					flag = true;
					this.AddDuplicateMessage();
					__ChatWindow.isOnFocus = false;
					__ChatWindow.wnd.LooseChatFocus();
					break;
				}
			}
			if (!flag)
			{
				this.SendMessage(txt);
			}
		}
		if (__ChatWindow.activeChat.chatType == 2)
		{
			foreach (ChatItem chatItem in __ChatWindow.guildChat.items)
			{
				if ((float)(DateTime.get_Now() - chatItem.time).get_Minutes() >= single || !(chatItem.playerName == NetworkScript.player.vessel.playerName))
				{
					flag = false;
				}
				else if (txt != chatItem.text)
				{
					flag = false;
				}
				else
				{
					flag = true;
					this.AddDuplicateMessage();
					__ChatWindow.isOnFocus = false;
					__ChatWindow.wnd.LooseChatFocus();
					break;
				}
			}
			if (!flag)
			{
				this.SendMessage(txt);
			}
		}
		if (__ChatWindow.activeChat.chatType == 3)
		{
			foreach (ChatItem item1 in __ChatWindow.partyChat.items)
			{
				if ((float)(DateTime.get_Now() - item1.time).get_Minutes() >= single || !(item1.playerName == NetworkScript.player.vessel.playerName))
				{
					flag = false;
				}
				else if (txt != item1.text)
				{
					flag = false;
				}
				else
				{
					flag = true;
					this.AddDuplicateMessage();
					__ChatWindow.isOnFocus = false;
					__ChatWindow.wnd.LooseChatFocus();
					break;
				}
			}
			if (!flag)
			{
				this.SendMessage(txt);
			}
		}
		if (__ChatWindow.activeChat.chatType == 4)
		{
			foreach (ChatItem chatItem1 in __ChatWindow.privateChat.items)
			{
				if ((float)(DateTime.get_Now() - chatItem1.time).get_Minutes() >= single || !(chatItem1.playerName == NetworkScript.player.vessel.playerName))
				{
					flag = false;
				}
				else if (txt != chatItem1.text)
				{
					flag = false;
				}
				else
				{
					flag = true;
					this.AddDuplicateMessage();
					__ChatWindow.isOnFocus = false;
					__ChatWindow.wnd.LooseChatFocus();
					break;
				}
			}
			if (!flag)
			{
				this.SendMessage(txt);
			}
		}
		if (__ChatWindow.activeChat.chatType == 5)
		{
			foreach (ChatItem item2 in __ChatWindow.factionChat.items)
			{
				if ((float)(DateTime.get_Now() - item2.time).get_Minutes() >= single || !(item2.playerName == NetworkScript.player.vessel.playerName))
				{
					flag = false;
				}
				else if (txt != item2.text)
				{
					flag = false;
				}
				else
				{
					flag = true;
					this.AddDuplicateMessage();
					__ChatWindow.isOnFocus = false;
					__ChatWindow.wnd.LooseChatFocus();
					break;
				}
			}
			if (!flag)
			{
				this.SendMessage(txt);
			}
		}
		if (__ChatWindow.activeChat.chatType == 6)
		{
			foreach (ChatItem chatItem2 in __ChatWindow.languageChat.items)
			{
				if ((float)(DateTime.get_Now() - chatItem2.time).get_Minutes() >= single || !(chatItem2.playerName == NetworkScript.player.vessel.playerName))
				{
					flag = false;
				}
				else if (txt != chatItem2.text)
				{
					flag = false;
				}
				else
				{
					flag = true;
					this.AddDuplicateMessage();
					__ChatWindow.isOnFocus = false;
					__ChatWindow.wnd.LooseChatFocus();
					break;
				}
			}
			if (!flag)
			{
				this.SendMessage(txt);
			}
		}
	}

	private bool CheckForSpam(string txt)
	{
		if (__ChatWindow.activeChat.chatType == 1)
		{
			this.timeStampMessagesUniverse.Add(DateTime.get_Now());
			if (this.timeStampMessagesUniverse.get_Count() <= 3)
			{
				return false;
			}
			if ((this.timeStampMessagesUniverse.get_Item(this.timeStampMessagesUniverse.get_Count() - 1) - this.timeStampMessagesUniverse.get_Item(this.timeStampMessagesUniverse.get_Count() - 3)).get_Seconds() < 5)
			{
				this.spamAlert = true;
				return true;
			}
			this.spamAlert = false;
			this.timeStampMessagesUniverse.Clear();
			return false;
		}
		if (__ChatWindow.activeChat.chatType == 4)
		{
			this.timeStampMessagesPrivate.Add(DateTime.get_Now());
			if (this.timeStampMessagesPrivate.get_Count() <= 3)
			{
				return false;
			}
			if ((this.timeStampMessagesPrivate.get_Item(this.timeStampMessagesPrivate.get_Count() - 1) - this.timeStampMessagesPrivate.get_Item(this.timeStampMessagesPrivate.get_Count() - 3)).get_Seconds() < 5)
			{
				this.spamAlert = true;
				return true;
			}
			this.spamAlert = false;
			this.timeStampMessagesPrivate.Clear();
			return false;
		}
		if (__ChatWindow.activeChat.chatType == 2)
		{
			this.timeStampMessagesGuild.Add(DateTime.get_Now());
			if (this.timeStampMessagesGuild.get_Count() <= 3)
			{
				return false;
			}
			if ((this.timeStampMessagesGuild.get_Item(this.timeStampMessagesGuild.get_Count() - 1) - this.timeStampMessagesGuild.get_Item(this.timeStampMessagesGuild.get_Count() - 3)).get_Seconds() < 5)
			{
				this.spamAlert = true;
				return true;
			}
			this.spamAlert = false;
			this.timeStampMessagesGuild.Clear();
			return false;
		}
		if (__ChatWindow.activeChat.chatType == 3)
		{
			this.timeStampMessagesParty.Add(DateTime.get_Now());
			if (this.timeStampMessagesParty.get_Count() <= 3)
			{
				return false;
			}
			if ((this.timeStampMessagesParty.get_Item(this.timeStampMessagesParty.get_Count() - 1) - this.timeStampMessagesParty.get_Item(this.timeStampMessagesParty.get_Count() - 3)).get_Seconds() < 5)
			{
				this.spamAlert = true;
				return true;
			}
			this.spamAlert = false;
			this.timeStampMessagesParty.Clear();
			return false;
		}
		if (__ChatWindow.activeChat.chatType == 5)
		{
			this.timeStampMessagesFaction.Add(DateTime.get_Now());
			if (this.timeStampMessagesFaction.get_Count() <= 3)
			{
				return false;
			}
			if ((this.timeStampMessagesFaction.get_Item(this.timeStampMessagesFaction.get_Count() - 1) - this.timeStampMessagesFaction.get_Item(this.timeStampMessagesFaction.get_Count() - 3)).get_Seconds() >= 5)
			{
				this.spamAlert = false;
				this.timeStampMessagesFaction.Clear();
				return false;
			}
			this.spamAlert = true;
		}
		if (__ChatWindow.activeChat.chatType != 6)
		{
			return false;
		}
		this.timeStampMessagesLanguage.Add(DateTime.get_Now());
		if (this.timeStampMessagesLanguage.get_Count() <= 3)
		{
			return false;
		}
		if ((this.timeStampMessagesLanguage.get_Item(this.timeStampMessagesLanguage.get_Count() - 1) - this.timeStampMessagesLanguage.get_Item(this.timeStampMessagesLanguage.get_Count() - 3)).get_Seconds() < 5)
		{
			this.spamAlert = true;
			return true;
		}
		this.spamAlert = false;
		this.timeStampMessagesLanguage.Clear();
		return false;
	}

	private void CheckLanguage()
	{
		if (NetworkScript.player.language != string.Empty)
		{
			this.LangIconClk = string.Concat("btnLanguage", NetworkScript.player.language.ToUpper(), "_Clk");
		}
		else if (this.LangIconClk == string.Empty || this.LangIconNml == string.Empty || this.LangIconHvr == string.Empty)
		{
			this.LangIconClk = "btnLanguageEN_Clk";
		}
	}

	private void CheckLastChat()
	{
		if (__ChatWindow.lastChatChecked.chatType == 1)
		{
			__ChatWindow.btnUniverseChat.IsClicked = true;
			__ChatWindow.universeChat.hasUnreadMessages = false;
		}
		else if (__ChatWindow.lastChatChecked.chatType == 2)
		{
			__ChatWindow.btnGuildChat.IsClicked = true;
			__ChatWindow.guildChat.hasUnreadMessages = false;
		}
		else if (__ChatWindow.lastChatChecked.chatType == 3)
		{
			__ChatWindow.btnPartyChat.IsClicked = true;
			__ChatWindow.partyChat.hasUnreadMessages = false;
		}
		else if (__ChatWindow.lastChatChecked.chatType == 4)
		{
			__ChatWindow.btnPrivateChat.IsClicked = true;
			__ChatWindow.privateChat.hasUnreadMessages = false;
		}
		else if (__ChatWindow.lastChatChecked.chatType == 5)
		{
			__ChatWindow.btnFactionChat.IsClicked = true;
			__ChatWindow.factionChat.hasUnreadMessages = false;
		}
		else if (__ChatWindow.lastChatChecked.chatType != 6 || __ChatWindow.btnLanguageChat == null)
		{
			__ChatWindow.btnUniverseChat.IsClicked = true;
			__ChatWindow.universeChat.hasUnreadMessages = false;
			__ChatWindow.lastChatChecked = __ChatWindow.activeChat;
		}
		else
		{
			__ChatWindow.btnLanguageChat.IsClicked = true;
			__ChatWindow.languageChat.hasUnreadMessages = false;
		}
	}

	public bool CheckMousePosition()
	{
		float _height = (float)Screen.get_height() - Input.get_mousePosition().y;
		Rect rect = new Rect(this.boundries.get_x(), this.boundries.get_y(), this.boundries.get_width(), this.boundries.get_height());
		if (__ChatWindow.isSmallChat)
		{
			return false;
		}
		if (rect.Contains(new Vector3(Input.get_mousePosition().x, _height)))
		{
			return true;
		}
		return false;
	}

	public void CheckPartyAndGuild()
	{
		// 
		// Current member / type: System.Void __ChatWindow::CheckPartyAndGuild()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void CheckPartyAndGuild()
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

	private void CheckReceiver(string playerName, long playerId)
	{
		if (!__ChatWindow.receiversList.Contains(playerName))
		{
			__ChatWindow.receiversList.Add(playerName);
			if (__ChatWindow.privateChat.receiverName == null)
			{
				__ChatWindow.privateChat.receiverName = playerName;
				__ChatWindow.privateChat.recieverID = playerId;
				if (__ChatWindow.btnPrivateChat.IsClicked)
				{
					this.ManageReceiverGUI();
				}
			}
		}
	}

	public override void Create()
	{
		__ChatWindow.factionChatAssetName = string.Format("chat_faction_{0}_", NetworkScript.player.vessel.fractionId);
		if (playWebGame.GAME_TYPE != "ru")
		{
			this.isRussianServer = false;
		}
		else
		{
			this.isRussianServer = true;
		}
		this.isPlayerChatAdmin = (!NetworkScript.player.isChatAdmin ? false : true);
		this.fractionIcon = string.Format("fraction{0}Icon", NetworkScript.player.vessel.fractionId);
		this.adminSenderFraction = NetworkScript.player.vessel.fractionId;
		this.CheckLanguage();
		if (this.chatScroll != null)
		{
			this.SaveScrollerPosition(__ChatWindow.activeChat);
		}
		this.timeStampMessagesPrivate = new List<DateTime>();
		this.timeStampMessagesUniverse = new List<DateTime>();
		this.timeStampMessagesGuild = new List<DateTime>();
		this.timeStampMessagesParty = new List<DateTime>();
		this.timeStampMessagesFaction = new List<DateTime>();
		this.timeStampMessagesLanguage = new List<DateTime>();
		if (__ChatWindow.lastChatChecked == null)
		{
			__ChatWindow.lastChatChecked = new ChatItems();
		}
		EventHandlerParam eventHandlerParam = new EventHandlerParam();
		base.SetBackgroundTexture("chat", "frame_main_Small3_Chat");
		this.boundries.set_x((float)Screen.get_width() - this.boundries.get_width());
		this.boundries.set_y((float)Screen.get_height() - this.boundries.get_height() - 25f);
		this.boundries.set_height(this.boundries.get_height() + 25f);
		this.frame_bottom = new GuiTexture();
		this.frame_bottom.SetTexture("chat", "frame_bottom");
		this.frame_bottom.X = 0f;
		this.frame_bottom.Y = this.boundries.get_height() - 25f;
		base.AddGuiElement(this.frame_bottom);
		this.isHidden = false;
		this.zOrder = 110;
		if (__ChatWindow.activeChat == null)
		{
			__ChatWindow.activeChat = __ChatWindow.universeChat;
		}
		this.tbSendMessage = new GuiTextBox()
		{
			controlName = "tbChatSend",
			boundries = new Rect(8f, 222f, 230f, 30f),
			Alignment = 3,
			FontSize = 14,
			TextColor = GuiNewStyleBar.blueColor
		};
		this.tbSendMessage.SetFrameTexture("chat", "tbTransperant");
		this.tbSendMessage.Validate = new Action(this, __ChatWindow.ValideteInput);
		this.tbSendMessage.Confirm = new Action<object>(this, __ChatWindow.TextBoxSend);
		base.AddGuiElement(this.tbSendMessage);
		if (this.chatScroll == null)
		{
			this.chatScroll = new GuiScrollingContainer(3f, 30f, 288f, 194f, 1, this);
			this.chatScroll.SetArrowStep(14f);
			base.AddGuiElement(this.chatScroll);
		}
		if (!__ChatWindow.isSmallChat)
		{
			eventHandlerParam.customData = true;
			this.CreateMainScreen(eventHandlerParam);
		}
		else
		{
			this.CreateSmallScreen(eventHandlerParam);
		}
		this.CheckPartyAndGuild();
		this.PopulateChatRoom(__ChatWindow.activeChat);
		this.InitialiseBlinkFX();
		this.CheckLastChat();
	}

	private void CreateMainScreen(EventHandlerParam p)
	{
		if (this.chatScroll != null)
		{
			this.SaveScrollerPosition(__ChatWindow.activeChat);
		}
		base.RemoveGuiElement(__ChatWindow.btnShowHide);
		base.RemoveGuiElement(__ChatWindow.infoIcon);
		base.RemoveGuiElement(__ChatWindow.txLanguageChat);
		base.RemoveGuiElement(__ChatWindow.btnUniverseChat);
		base.RemoveGuiElement(__ChatWindow.btnFactionChat);
		base.RemoveGuiElement(__ChatWindow.btnLanguageChat);
		base.RemoveGuiElement(__ChatWindow.btnGuildChat);
		base.RemoveGuiElement(__ChatWindow.btnPartyChat);
		base.RemoveGuiElement(__ChatWindow.btnPrivateChat);
		base.RemoveGuiElement(this.btnSend);
		if (this.isPlayerChatAdmin)
		{
			base.RemoveGuiElement(__ChatWindow.btnChangeFraction);
		}
		if (__ChatWindow.activeChat == null)
		{
			__ChatWindow.activeChat = __ChatWindow.universeChat;
		}
		__ChatWindow.isSmallChat = false;
		base.SetBackgroundTexture("chat", "frame_main_U");
		this.boundries.set_x((float)Screen.get_width() - this.boundries.get_width());
		this.boundries.set_y((float)Screen.get_height() - this.boundries.get_height() - 25f);
		this.boundries.set_height(this.boundries.get_height() + 25f);
		__ChatWindow.btnShowHide = new GuiButtonFixed();
		__ChatWindow.btnShowHide.SetTexture("chat", "btnHideNml");
		__ChatWindow.btnShowHide.SetTextureHover("chat", "btnHideClk");
		__ChatWindow.btnShowHide.SetTextureClicked("chat", "btnHideClk");
		__ChatWindow.btnShowHide.Caption = string.Empty;
		__ChatWindow.btnShowHide.eventHandlerParam = new EventHandlerParam();
		__ChatWindow.btnShowHide.Clicked = new Action<EventHandlerParam>(this, __ChatWindow.CreateSmallScreen);
		this.btnSend = new GuiButtonFixed()
		{
			Caption = string.Empty,
			Clicked = new Action<EventHandlerParam>(this, __ChatWindow.OnSendClicked)
		};
		__ChatWindow.txLanguageChat = new GuiTexture();
		__ChatWindow.btnUniverseChat = new GuiButtonFixed()
		{
			Caption = string.Empty,
			Alignment = 4
		};
		GuiButtonFixed guiButtonFixed = __ChatWindow.btnUniverseChat;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_chat_universe"),
			customData2 = __ChatWindow.btnUniverseChat
		};
		guiButtonFixed.tooltipWindowParam = eventHandlerParam;
		__ChatWindow.btnUniverseChat.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		__ChatWindow.btnUniverseChat.behaviourKeepClicked = true;
		__ChatWindow.btnUniverseChat.groupId = 1;
		GuiButtonFixed guiButtonFixed1 = __ChatWindow.btnUniverseChat;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = 0
		};
		guiButtonFixed1.eventHandlerParam = eventHandlerParam;
		__ChatWindow.btnUniverseChat.Clicked = new Action<EventHandlerParam>(this, __ChatWindow.SwitchChatRoom);
		__ChatWindow.btnFactionChat = new GuiButtonFixed()
		{
			Caption = string.Empty
		};
		GuiButtonFixed guiButtonFixed2 = __ChatWindow.btnFactionChat;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_chat_faction"),
			customData2 = __ChatWindow.btnFactionChat
		};
		guiButtonFixed2.tooltipWindowParam = eventHandlerParam;
		__ChatWindow.btnFactionChat.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		__ChatWindow.btnFactionChat.behaviourKeepClicked = true;
		__ChatWindow.btnFactionChat.groupId = 1;
		__ChatWindow.btnFactionChat.Clicked = new Action<EventHandlerParam>(this, __ChatWindow.SwitchChatRoom);
		GuiButtonFixed guiButtonFixed3 = __ChatWindow.btnFactionChat;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = 3
		};
		guiButtonFixed3.eventHandlerParam = eventHandlerParam;
		if (!this.isRussianServer)
		{
			__ChatWindow.btnLanguageChat = new GuiButtonFixed()
			{
				Caption = string.Empty
			};
			GuiButtonFixed guiButtonFixed4 = __ChatWindow.btnLanguageChat;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_chat_language"),
				customData2 = __ChatWindow.btnLanguageChat
			};
			guiButtonFixed4.tooltipWindowParam = eventHandlerParam;
			__ChatWindow.btnLanguageChat.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			__ChatWindow.btnLanguageChat.behaviourKeepClicked = true;
			__ChatWindow.btnLanguageChat.Clicked = new Action<EventHandlerParam>(this, __ChatWindow.SwitchChatRoom);
			__ChatWindow.btnLanguageChat.groupId = 1;
			GuiButtonFixed guiButtonFixed5 = __ChatWindow.btnLanguageChat;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = 4
			};
			guiButtonFixed5.eventHandlerParam = eventHandlerParam;
		}
		__ChatWindow.btnGuildChat = new GuiButtonFixed()
		{
			Caption = string.Empty
		};
		GuiButtonFixed guiButtonFixed6 = __ChatWindow.btnGuildChat;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_chat_guild"),
			customData2 = __ChatWindow.btnGuildChat
		};
		guiButtonFixed6.tooltipWindowParam = eventHandlerParam;
		__ChatWindow.btnGuildChat.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		__ChatWindow.btnGuildChat.behaviourKeepClicked = true;
		__ChatWindow.btnGuildChat.Clicked = new Action<EventHandlerParam>(this, __ChatWindow.SwitchChatRoom);
		__ChatWindow.btnGuildChat.groupId = 1;
		GuiButtonFixed guiButtonFixed7 = __ChatWindow.btnGuildChat;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = 1
		};
		guiButtonFixed7.eventHandlerParam = eventHandlerParam;
		__ChatWindow.btnGuildChat.isEnabled = false;
		__ChatWindow.btnPartyChat = new GuiButtonFixed()
		{
			Caption = string.Empty
		};
		GuiButtonFixed guiButtonFixed8 = __ChatWindow.btnPartyChat;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_chat_party"),
			customData2 = __ChatWindow.btnPartyChat
		};
		guiButtonFixed8.tooltipWindowParam = eventHandlerParam;
		__ChatWindow.btnPartyChat.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		__ChatWindow.btnPartyChat.behaviourKeepClicked = true;
		__ChatWindow.btnPartyChat.Clicked = new Action<EventHandlerParam>(this, __ChatWindow.SwitchChatRoom);
		__ChatWindow.btnPartyChat.groupId = 1;
		GuiButtonFixed guiButtonFixed9 = __ChatWindow.btnPartyChat;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = 2
		};
		guiButtonFixed9.eventHandlerParam = eventHandlerParam;
		__ChatWindow.btnPartyChat.isEnabled = false;
		__ChatWindow.btnPrivateChat = new GuiButtonFixed()
		{
			Caption = string.Empty
		};
		GuiButtonFixed guiButtonFixed10 = __ChatWindow.btnPrivateChat;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_chat_private"),
			customData2 = __ChatWindow.btnPrivateChat
		};
		guiButtonFixed10.tooltipWindowParam = eventHandlerParam;
		__ChatWindow.btnPrivateChat.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		__ChatWindow.btnPrivateChat.behaviourKeepClicked = true;
		__ChatWindow.btnPrivateChat.Clicked = new Action<EventHandlerParam>(this, __ChatWindow.SwitchChatRoom);
		__ChatWindow.btnPrivateChat.groupId = 1;
		GuiButtonFixed guiButtonFixed11 = __ChatWindow.btnPrivateChat;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = 5
		};
		guiButtonFixed11.eventHandlerParam = eventHandlerParam;
		this.btnSend.SetTexture("chat", "btnSendNew2Nml");
		this.btnSend.SetTextureHover("chat", "btnSendNew2Clk");
		this.btnSend.SetTextureClicked("chat", "btnSendNew2Clk");
		this.btnSend.X = 229f;
		this.btnSend.Y = 232f;
		__ChatWindow.wnd = this;
		if (!__ChatWindow.universeChat.hasUnreadMessages)
		{
			__ChatWindow.btnUniverseChat.SetTexture("chat", "btnUniverseNml");
		}
		else
		{
			__ChatWindow.btnUniverseChat.SetTexture("chat", "btnUniverseHvr");
		}
		__ChatWindow.btnUniverseChat.SetTextureClicked("chat", "btnUniverseClk");
		__ChatWindow.btnUniverseChat.SetTextureHover("chat", "btnUniverseClk");
		__ChatWindow.btnUniverseChat.X = 11f;
		__ChatWindow.btnUniverseChat.Y = 3f;
		if (!__ChatWindow.factionChat.hasUnreadMessages)
		{
			__ChatWindow.btnFactionChat.SetTexture("chat", string.Concat(__ChatWindow.factionChatAssetName, "nml"));
		}
		else
		{
			__ChatWindow.btnFactionChat.SetTexture("chat", string.Concat(__ChatWindow.factionChatAssetName, "clk"));
		}
		__ChatWindow.btnFactionChat.SetTextureClicked("chat", string.Concat(__ChatWindow.factionChatAssetName, "hvr"));
		__ChatWindow.btnFactionChat.SetTextureHover("chat", string.Concat(__ChatWindow.factionChatAssetName, "hvr"));
		__ChatWindow.btnFactionChat.X = 36f;
		__ChatWindow.btnFactionChat.Y = 5f;
		__ChatWindow.txLanguageChat.SetTexture("chat", this.LangIconClk);
		__ChatWindow.txLanguageChat.X = 58f;
		__ChatWindow.txLanguageChat.Y = 6f;
		if (!this.isRussianServer)
		{
			if (!__ChatWindow.languageChat.hasUnreadMessages)
			{
				__ChatWindow.btnLanguageChat.SetTexture("chat", "btnLanguage_Nml");
			}
			else
			{
				__ChatWindow.btnLanguageChat.SetTexture("chat", "btnLanguage_Hvr");
			}
			__ChatWindow.btnLanguageChat.SetTextureClicked("chat", "btnLanguage_Clk");
			__ChatWindow.btnLanguageChat.SetTextureHover("chat", "btnLanguage_Clk");
			__ChatWindow.btnLanguageChat.X = 58f;
			__ChatWindow.btnLanguageChat.Y = 6f;
		}
		if (!__ChatWindow.guildChat.hasUnreadMessages)
		{
			__ChatWindow.btnGuildChat.SetTexture("chat", "btnGuildNml");
		}
		else
		{
			__ChatWindow.btnGuildChat.SetTexture("chat", "btnGuildHvr");
		}
		__ChatWindow.btnGuildChat.SetTextureClicked("chat", "btnGuildClk");
		__ChatWindow.btnGuildChat.SetTextureHover("chat", "btnGuildClk");
		__ChatWindow.btnGuildChat.SetTextureDisabled("chat", "btnGuildDsb");
		if (this.isRussianServer)
		{
			__ChatWindow.btnGuildChat.X = 59f;
		}
		else
		{
			__ChatWindow.btnGuildChat.X = 84f;
		}
		__ChatWindow.btnGuildChat.Y = 3f;
		if (!__ChatWindow.partyChat.hasUnreadMessages)
		{
			__ChatWindow.btnPartyChat.SetTexture("chat", "btnPartyNml");
		}
		else
		{
			__ChatWindow.btnPartyChat.SetTexture("chat", "btnPartyHvr");
		}
		__ChatWindow.btnPartyChat.SetTextureClicked("chat", "btnPartyClk");
		__ChatWindow.btnPartyChat.SetTextureHover("chat", "btnPartyClk");
		__ChatWindow.btnPartyChat.SetTextureDisabled("chat", "btnPartyDsb");
		if (this.isRussianServer)
		{
			__ChatWindow.btnPartyChat.X = 84f;
		}
		else
		{
			__ChatWindow.btnPartyChat.X = 108f;
		}
		__ChatWindow.btnPartyChat.Y = 3f;
		if (!__ChatWindow.privateChat.hasUnreadMessages)
		{
			__ChatWindow.btnPrivateChat.SetTexture("chat", "btnPrivateNml");
		}
		else
		{
			__ChatWindow.btnPrivateChat.SetTexture("chat", "btnPrivateHvr");
		}
		__ChatWindow.btnPrivateChat.SetTextureClicked("chat", "btnPrivateClk");
		__ChatWindow.btnPrivateChat.SetTextureHover("chat", "btnPrivateClk");
		if (this.isRussianServer)
		{
			__ChatWindow.btnPrivateChat.X = 108f;
		}
		else
		{
			__ChatWindow.btnPrivateChat.X = 131f;
		}
		__ChatWindow.btnPrivateChat.Y = 3f;
		__ChatWindow.btnShowHide.X = this.btnSend.X + this.btnSend.boundries.get_width() + 1f;
		__ChatWindow.btnShowHide.Y = 232f;
		base.AddGuiElement(__ChatWindow.btnShowHide);
		string empty = string.Empty;
		empty = (this.isRussianServer ? string.Concat(new string[] { "/g - ", StaticData.Translate("key_chat_guild"), "\n /u - ", StaticData.Translate("key_chat_universe"), "\n /f - ", StaticData.Translate("key_chat_faction"), "\n /faction - ", StaticData.Translate("key_chat_faction"), "\n /p - ", StaticData.Translate("key_chat_party"), "\n /", StaticData.Translate("key_chat_player"), " - ", StaticData.Translate("key_chat_private"), "\n /friend ", StaticData.Translate("key_chat_player"), " - ", StaticData.Translate("key_profile_screen_add_friend_list"), "\n /ignore ", StaticData.Translate("key_chat_player"), " - ", StaticData.Translate("key_profile_screen_tooltips_add_blacklist"), "\n /invite ", StaticData.Translate("key_chat_player"), " - ", StaticData.Translate("key_profile_screen_tooltips_invite") }) : string.Concat(new string[] { "/g - ", StaticData.Translate("key_chat_guild"), "\n /u - ", StaticData.Translate("key_chat_universe"), "\n /f - ", StaticData.Translate("key_chat_faction"), "\n /faction - ", StaticData.Translate("key_chat_faction"), "\n /lang - ", StaticData.Translate("key_chat_language"), "\n /p - ", StaticData.Translate("key_chat_party"), "\n /", StaticData.Translate("key_chat_player"), " - ", StaticData.Translate("key_chat_private"), "\n /friend ", StaticData.Translate("key_chat_player"), " - ", StaticData.Translate("key_profile_screen_add_friend_list"), "\n /ignore ", StaticData.Translate("key_chat_player"), " - ", StaticData.Translate("key_profile_screen_tooltips_add_blacklist"), "\n /report ", StaticData.Translate("key_chat_player"), " - ", StaticData.Translate("key_chat_report_player"), "\n /invite ", StaticData.Translate("key_chat_player"), " - ", StaticData.Translate("key_profile_screen_tooltips_invite") }));
		if (NetworkScript.player.isChatAdmin)
		{
			StringBuilder stringBuilder = new StringBuilder(empty);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine(string.Format(StaticData.Translate("key_chat_admin_ban_player"), 4));
			stringBuilder.AppendLine(string.Format(StaticData.Translate("key_chat_admin_mute_player"), 4));
			stringBuilder.AppendLine(StaticData.Translate("key_chat_admin_system_u"));
			empty = stringBuilder.ToString();
		}
		__ChatWindow.infoIcon = new GuiButtonFixed()
		{
			Caption = string.Empty
		};
		GuiButtonFixed guiButtonFixed12 = __ChatWindow.infoIcon;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = empty,
			customData2 = __ChatWindow.infoIcon
		};
		guiButtonFixed12.tooltipWindowParam = eventHandlerParam;
		__ChatWindow.infoIcon.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		__ChatWindow.infoIcon.isHoverAware = true;
		__ChatWindow.infoIcon.SetTexture("chat", "info_icon2");
		__ChatWindow.infoIcon.X = 248f;
		__ChatWindow.infoIcon.Y = 2f;
		__ChatWindow.infoIcon.boundries.set_width(22f);
		__ChatWindow.infoIcon.boundries.set_height(22f);
		base.AddGuiElement(__ChatWindow.infoIcon);
		if (this.isPlayerChatAdmin)
		{
			__ChatWindow.btnChangeFraction = new GuiButtonFixed()
			{
				Caption = string.Empty
			};
			GuiButtonFixed guiButtonFixed13 = __ChatWindow.btnChangeFraction;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_chat_change_sender_fraction"),
				customData2 = __ChatWindow.infoIcon
			};
			guiButtonFixed13.tooltipWindowParam = eventHandlerParam;
			__ChatWindow.btnChangeFraction.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			__ChatWindow.btnChangeFraction.isHoverAware = true;
			__ChatWindow.btnChangeFraction.SetTexture("FrameworkGUI", this.fractionIcon);
			__ChatWindow.btnChangeFraction.X = 225f;
			__ChatWindow.btnChangeFraction.Y = 2f;
			__ChatWindow.btnChangeFraction.boundries.set_width(23f);
			__ChatWindow.btnChangeFraction.boundries.set_height(18f);
			GuiButtonFixed guiButtonFixed14 = __ChatWindow.btnChangeFraction;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = this.fractionIcon
			};
			guiButtonFixed14.eventHandlerParam = eventHandlerParam;
			__ChatWindow.btnChangeFraction.Clicked = new Action<EventHandlerParam>(this, __ChatWindow.OnChangeFraction);
			base.AddGuiElement(__ChatWindow.btnChangeFraction);
		}
		if (!this.isRussianServer)
		{
			base.AddGuiElement(__ChatWindow.txLanguageChat);
		}
		__ChatWindow.wnd.boundries.set_y((float)Screen.get_height() - __ChatWindow.wnd.boundries.get_height() + 2f);
		base.AddGuiElement(this.btnSend);
		base.AddGuiElement(__ChatWindow.btnUniverseChat);
		base.AddGuiElement(__ChatWindow.btnFactionChat);
		if (!this.isRussianServer)
		{
			base.AddGuiElement(__ChatWindow.btnLanguageChat);
		}
		base.AddGuiElement(__ChatWindow.btnGuildChat);
		base.AddGuiElement(__ChatWindow.btnPartyChat);
		base.AddGuiElement(__ChatWindow.btnPrivateChat);
		this.InitialiseBlinkFX();
		this.InitReceiverWeb();
		this.ManageChangeFractionButton();
		this.CheckPartyAndGuild();
		this.CheckLastChat();
		this.PopulateChatRoom(__ChatWindow.activeChat);
	}

	public void CreateSmallScreen(EventHandlerParam p)
	{
		this.AssignLastChat();
		this.SaveScrollerPosition(__ChatWindow.activeChat);
		__ChatWindow.isSmallChat = true;
		base.RemoveGuiElement(__ChatWindow.btnUniverseChat);
		base.RemoveGuiElement(__ChatWindow.btnFactionChat);
		base.RemoveGuiElement(__ChatWindow.btnLanguageChat);
		base.RemoveGuiElement(__ChatWindow.btnGuildChat);
		base.RemoveGuiElement(__ChatWindow.btnPartyChat);
		base.RemoveGuiElement(__ChatWindow.btnPrivateChat);
		base.RemoveGuiElement(__ChatWindow.infoIcon);
		base.RemoveGuiElement(this.btnSend);
		base.RemoveGuiElement(__ChatWindow.btnShowHide);
		base.RemoveGuiElement(__ChatWindow.infoIcon);
		base.RemoveGuiElement(__ChatWindow.txLanguageChat);
		if (this.isPlayerChatAdmin)
		{
			base.RemoveGuiElement(__ChatWindow.btnChangeFraction);
		}
		base.SetBackgroundTexture("chat", "frame_main_Small3_Chat");
		this.boundries.set_x((float)Screen.get_width() - this.boundries.get_width());
		this.boundries.set_y((float)Screen.get_height() - this.boundries.get_height() - 25f);
		this.boundries.set_height(this.boundries.get_height() + 25f);
		this.btnSend = new GuiButtonFixed();
		this.btnSend.SetTexture("chat", "btnSendNew2Nml");
		this.btnSend.SetTextureHover("chat", "btnSendNew2Clk");
		this.btnSend.SetTextureClicked("chat", "btnSendNew2Clk");
		this.btnSend.Caption = string.Empty;
		this.btnSend.X = 229f;
		this.btnSend.Y = 232f;
		this.btnSend.Clicked = new Action<EventHandlerParam>(this, __ChatWindow.OnSendClicked);
		base.AddGuiElement(this.btnSend);
		__ChatWindow.btnShowHide = new GuiButtonFixed();
		__ChatWindow.btnShowHide.SetTexture("chat", "btnShowNml");
		__ChatWindow.btnShowHide.SetTextureHover("chat", "btnShowClk");
		__ChatWindow.btnShowHide.SetTextureClicked("chat", "btnShowClk");
		__ChatWindow.btnShowHide.Caption = string.Empty;
		__ChatWindow.btnShowHide.X = this.btnSend.X + this.btnSend.boundries.get_width() + 1f;
		__ChatWindow.btnShowHide.Y = 232f;
		__ChatWindow.btnShowHide.eventHandlerParam = new EventHandlerParam();
		__ChatWindow.btnShowHide.Clicked = new Action<EventHandlerParam>(this, __ChatWindow.CreateMainScreen);
		base.AddGuiElement(__ChatWindow.btnShowHide);
		this.PopulateChatRoom(__ChatWindow.activeChat);
		__ChatWindow.wnd.boundries.set_y((float)Screen.get_height() - __ChatWindow.wnd.boundries.get_height() + 2f);
		this.InitReceiverWeb();
	}

	public void GainChatFocus()
	{
		AndromedaGui.gui.RequestFocusOnControl("tbChatSend");
		__ChatWindow.isOnFocus = true;
	}

	private string GetReportPlayerString()
	{
		switch (__ChatWindow.activeChat.chatType)
		{
			case 1:
			{
				return this.RetrieveLastTwentyItems(__ChatWindow.universeChat.items);
			}
			case 2:
			{
				return this.RetrieveLastTwentyItems(__ChatWindow.guildChat.items);
			}
			case 3:
			{
				return this.RetrieveLastTwentyItems(__ChatWindow.partyChat.items);
			}
			case 4:
			{
				return this.RetrieveLastTwentyItems(__ChatWindow.privateChat.items);
			}
			case 5:
			{
				return this.RetrieveLastTwentyItems(__ChatWindow.factionChat.items);
			}
			case 6:
			{
				return this.RetrieveLastTwentyItems(__ChatWindow.languageChat.items);
			}
			default:
			{
				return this.RetrieveLastTwentyItems(__ChatWindow.privateChat.items);
			}
		}
	}

	private void InitialiseBlinkFX()
	{
		GuiTexture guiTexture = new GuiTexture();
		DateTime now = DateTime.get_Now();
		this.fxSmallChat = new BlinkFx()
		{
			btn = __ChatWindow.btnShowHide,
			tx = guiTexture,
			isBlinkedNow = false
		};
		now = now.AddMilliseconds(100);
		this.fxSmallChat.nextSwitchTime = now;
		guiTexture.SetTexture("chat", "btnShowNml");
		this.fxSmallChat.btnTextureNormalName = "btnShowNml";
		this.fxSmallChat.btnTextureBlinkedName = "btnShowHvr";
	}

	public static void InitOnSceneStart()
	{
		if (NetworkScript.player.vessel.isGuest)
		{
			return;
		}
		__ChatWindow.InitStatics();
		AndromedaGui.gui.OnMouseDown = new Action<GuiWindow>(null, __ChatWindow.OnMouseDown);
		if (__ChatWindow.isOpen || NetworkScript.isInBase && __ChatWindow.wnd == null)
		{
			__ChatWindow.OpenTheWindow(false);
		}
	}

	private void InitReceiverListGUI()
	{
		if (this.lblReciever != null || this.btnReceiverList != null)
		{
			this.btnReceiverList.FontSize = 12;
		}
		else
		{
			this.lblReciever = new GuiLabel();
			this.lblReciever.boundries.set_x(550f);
			this.lblReciever.boundries.set_y(20f);
			this.lblReciever.FontSize = 12;
			this.lblReciever.text = string.Concat(StaticData.Translate("key_chat_receiver"), " :");
			base.AddGuiElement(this.lblReciever);
			this.btnReceiverList = new GuiButtonFixed()
			{
				Caption = string.Empty,
				FontSize = 12,
				Alignment = 4,
				X = this.lblReciever.X + this.lblReciever.TextWidth + 5f,
				Y = 14f
			};
			this.btnReceiverList.boundries.set_width(160f);
			this.btnReceiverList.boundries.set_height(55f);
			this.btnReceiverList.SetTexture("iPad/Chat", "btnReceiverList");
			this.btnReceiverList.Clicked = new Action<EventHandlerParam>(this, __ChatWindow.OnBtnReceiverClicked);
			base.AddGuiElement(this.btnReceiverList);
		}
		if (this.lblDummy == null)
		{
			this.lblDummy = new GuiLabel();
		}
	}

	private void InitReceiverWeb()
	{
		if (__ChatWindow.isSmallChat || __ChatWindow.btnPrivateChat == null || !__ChatWindow.btnPrivateChat.IsClicked || __ChatWindow.receiversList == null || __ChatWindow.receiversList.get_Count() <= 0 || string.IsNullOrEmpty(__ChatWindow.privateChat.receiverName))
		{
			base.RemoveGuiElement(this.btnDropDownList);
			if (this.btnDropDownList != null)
			{
				this.btnDropDownList.isActive = false;
			}
			if (this.receiversScollerContainer != null)
			{
				base.RemoveGuiElement(this.receiversScollerContainer);
				this.receiversScollerContainer = null;
			}
		}
		else
		{
			if (this.btnDropDownList == null)
			{
				this.btnDropDownList = new GuiButtonResizeable();
				this.btnDropDownList.SetTexture("chat", "btnDropDown_");
				this.btnDropDownList.X = 154f;
				this.btnDropDownList.Y = 5f;
				this.btnDropDownList.Width = 94f;
				this.btnDropDownList.Caption = __ChatWindow.privateChat.receiverName;
				this.btnDropDownList.FontSize = 12;
				this.btnDropDownList.textColorNormal = Color.get_white();
				this.btnDropDownList.textColorHover = Color.get_white();
				this.btnDropDownList.Alignment = 4;
				this.btnDropDownList.Clicked = new Action<EventHandlerParam>(this, __ChatWindow.OnDropDownClicked);
				this.lblDummy.FontSize = this.btnDropDownList.FontSize;
				this.lblDummy.text = this.btnDropDownList.Caption;
				if (this.lblDummy.TextWidth >= this.btnDropDownList.boundries.get_width() - 2f)
				{
					this.btnDropDownList.Caption = string.Concat(this.btnDropDownList.Caption.Remove(10, this.btnDropDownList.Caption.get_Length() - 10), "..");
				}
			}
			if (!this.btnDropDownList.isActive)
			{
				base.AddGuiElement(this.btnDropDownList);
				this.btnDropDownList.isActive = true;
			}
		}
	}

	private static void InitStatics()
	{
		if (__ChatWindow.scrollerPosition == null)
		{
			__ChatWindow.scrollerPosition = new Dictionary<int, float>();
			__ChatWindow.scrollerPosition.Add(0, 0f);
			__ChatWindow.scrollerPosition.Add(1, 0f);
			__ChatWindow.scrollerPosition.Add(2, 0f);
			__ChatWindow.scrollerPosition.Add(3, 0f);
			__ChatWindow.scrollerPosition.Add(4, 0f);
			__ChatWindow.scrollerPosition.Add(5, 0f);
		}
		if (__ChatWindow.labelPositionY.get_Count() < 1)
		{
			__ChatWindow.labelPositionY.Add(194f);
			__ChatWindow.labelPositionY.Add(194f);
			__ChatWindow.labelPositionY.Add(194f);
			__ChatWindow.labelPositionY.Add(194f);
			__ChatWindow.labelPositionY.Add(194f);
			__ChatWindow.labelPositionY.Add(194f);
		}
	}

	private void InsertToChatRoom(ChatItems Chat)
	{
		string str;
		EventHandlerParam eventHandlerParam;
		int num = 0;
		GuiLabel guiLabel = new GuiLabel();
		GuiButton guiButton = new GuiButton();
		GuiTexture guiTexture = new GuiTexture();
		GuiLabel fontMedium = new GuiLabel();
		__ChatWindow.dummyLabel = new GuiLabel()
		{
			FontSize = 12,
			Alignment = 6
		};
		guiLabel.FontSize = 12;
		guiButton.FontSize = 12;
		fontMedium.FontSize = 12;
		fontMedium.Font = GuiLabel.FontMedium;
		guiLabel.boundries = new Rect(6f, 0f, 260f, 14f);
		guiButton.boundries = new Rect(6f, 0f, 260f, 14f);
		fontMedium.boundries = new Rect(6f, 0f, 260f, 14f);
		if (Chat.chatType == 1)
		{
			num = 0;
		}
		if (Chat.chatType == 2)
		{
			num = 3;
		}
		if (Chat.chatType == 3)
		{
			num = 4;
		}
		if (Chat.chatType == 4)
		{
			num = 5;
		}
		if (Chat.chatType == 6)
		{
			num = 2;
		}
		if (Chat.chatType == 5)
		{
			num = 1;
		}
		if (Chat.chatType != 4 || Chat.items.get_Item(0).playerId != NetworkScript.player.vessel.playerId)
		{
			fontMedium.text = string.Format("({0:HH:mm}){1}: ", Chat.items.get_Item(0).time, Chat.items.get_Item(0).playerName);
			__ChatWindow.dummyLabel.text = guiButton.Caption;
			guiLabel.text = string.Concat(new string(' ', (int)(fontMedium.TextWidth / 2.9f) + 1), Chat.items.get_Item(0).text);
			if (Chat.items.get_Item(0).sendByAdmin)
			{
				guiButton.boundries.set_width(260f);
				guiTexture.SetTexture("chat", "admin_icon");
				guiLabel.boundries = new Rect(6f, 0f, 260f, 14f);
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_chat_admin"),
					customData2 = guiTexture
				};
				guiTexture.tooltipWindowParam = eventHandlerParam;
				guiTexture.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				guiTexture.isHoverAware = true;
				guiTexture.boundries.set_width(14f);
				guiTexture.boundries.set_height(14f);
				guiButton.boundries.set_x(guiLabel.boundries.get_x());
				str = string.Concat(Chat.items.get_Item(0).playerName, ":");
				fontMedium.text = string.Concat(string.Format("({0:HH:mm})", Chat.items.get_Item(0).time), new string(' ', 5), str);
				__ChatWindow.dummyLabel.text = string.Format("({0:HH:mm})", Chat.items.get_Item(0).time);
				guiTexture.boundries.set_x(__ChatWindow.dummyLabel.TextWidth + 7f);
				guiLabel.text = string.Concat(new string(' ', (int)(fontMedium.TextWidth / 2.9f)), Chat.items.get_Item(0).text);
			}
			if (Chat.items.get_Item(0).isSystemMessage)
			{
				Chat.items.get_Item(0).colorText = Color.get_red();
				Chat.items.get_Item(0).colorName = Color.get_red();
			}
			else if (NetworkScript.player.vessel.fractionId != Chat.items.get_Item(0).senderFactionId)
			{
				Chat.items.get_Item(0).colorText = GuiNewStyleBar.blueColor;
				Chat.items.get_Item(0).colorName = GuiNewStyleBar.purpleColor;
			}
		}
		else
		{
			if (Chat.items.get_Item(0).receiverName == null || Chat.items.get_Item(0).receiverName == " ")
			{
				Chat.items.get_Item(0).receiverName = __ChatWindow.activeChat.receiverName;
			}
			fontMedium.text = string.Format("({0:HH:mm}){1}->{2}: ", Chat.items.get_Item(0).time, Chat.items.get_Item(0).playerName, Chat.items.get_Item(0).receiverName);
			__ChatWindow.dummyLabel.text = guiButton.Caption;
			guiLabel.text = new string(' ', (int)(fontMedium.TextWidth / 2.9f));
			GuiLabel guiLabel1 = guiLabel;
			guiLabel1.text = string.Concat(guiLabel1.text, Chat.items.get_Item(0).text);
			if (Chat.items.get_Item(0).sendByAdmin)
			{
				guiButton.boundries.set_width(260f);
				guiTexture.SetTexture("chat", "admin_icon");
				guiLabel.boundries = new Rect(6f, 0f, 260f, 14f);
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_chat_admin"),
					customData2 = guiTexture
				};
				guiTexture.tooltipWindowParam = eventHandlerParam;
				guiTexture.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				guiTexture.isHoverAware = true;
				guiTexture.boundries.set_width(14f);
				guiTexture.boundries.set_height(14f);
				guiButton.boundries.set_x(guiLabel.boundries.get_x());
				str = string.Concat(Chat.items.get_Item(0).playerName, "->", Chat.items.get_Item(0).receiverName, ":");
				fontMedium.text = string.Concat(string.Format("({0:HH:mm})", Chat.items.get_Item(0).time), new string(' ', 5), str);
				__ChatWindow.dummyLabel.text = string.Format("({0:HH:mm})", Chat.items.get_Item(0).time);
				guiTexture.boundries.set_x(__ChatWindow.dummyLabel.TextWidth + 7f);
				__ChatWindow.dummyLabel.text = guiButton.Caption;
				guiLabel.text = string.Concat(new string(' ', (int)(fontMedium.TextWidth / 2.9f)), Chat.items.get_Item(0).text);
			}
		}
		if (NetworkScript.player.playId != Chat.items.get_Item(0).playerId)
		{
			if (!Chat.items.get_Item(0).isSystemMessage && NetworkScript.player.vessel.fractionId == Chat.items.get_Item(0).senderFactionId)
			{
				Chat.items.get_Item(0).colorText = GuiNewStyleBar.blueColor;
				Chat.items.get_Item(0).colorName = GuiNewStyleBar.orangeColor;
			}
			guiButton.textColorNormal = Chat.items.get_Item(0).colorName;
			guiButton.textColorHover = Color.get_white();
			guiButton.Clicked = new Action<EventHandlerParam>(this, __ChatWindow.BuildLabelMenuWnd);
			guiButton.eventHandlerParam.customData = Chat.items.get_Item(0);
			guiButton.eventHandlerParam.customData2 = fontMedium.TextWidth;
			HoverParamPair hoverParamPair = new HoverParamPair()
			{
				guiLbl = fontMedium,
				originalColor = Chat.items.get_Item(0).colorName
			};
			guiButton.hoverParam = hoverParamPair;
			fontMedium.TextColor = Chat.items.get_Item(0).colorName;
			guiButton.Hovered = new Action<object, bool>(this, __ChatWindow.OnBtnNameHover);
		}
		else
		{
			if (!Chat.items.get_Item(0).isCustomMessage)
			{
				Chat.items.get_Item(0).colorText = Color.get_green();
				Chat.items.get_Item(0).colorName = Color.get_green();
			}
			fontMedium.TextColor = Chat.items.get_Item(0).colorName;
		}
		if (Chat.items.get_Item(0).isSpam)
		{
			Chat.items.get_Item(0).colorText = Color.get_yellow();
			Chat.items.get_Item(0).colorName = Color.get_yellow();
			guiButton.Caption = string.Empty;
			fontMedium.text = string.Empty;
			guiButton.boundries.set_width((float)(guiButton.Caption.get_Length() * 6));
			guiLabel.text = string.Format("({0:HH:mm}){1} ", Chat.items.get_Item(0).time, Chat.items.get_Item(0).text);
		}
		if (Chat.items.get_Item(0).isCustomMessage)
		{
			guiButton.Caption = string.Empty;
			fontMedium.text = string.Empty;
			guiButton.boundries.set_width((float)(guiButton.Caption.get_Length() * 6));
			guiLabel.text = string.Format("({0:HH:mm}){1} ", Chat.items.get_Item(0).time, Chat.items.get_Item(0).text);
		}
		if (Chat.items.get_Item(0).isDuplicate)
		{
			Chat.items.get_Item(0).colorText = Color.get_yellow();
			Chat.items.get_Item(0).colorName = Color.get_yellow();
			guiButton.Caption = string.Empty;
			fontMedium.text = string.Empty;
			guiButton.boundries.set_width((float)(guiButton.Caption.get_Length() * 6));
			guiLabel.text = string.Format("({0:HH:mm}){1} ", Chat.items.get_Item(0).time, StaticData.Translate("key_chat_you_have_already_said_that"));
		}
		guiLabel.TextColor = Chat.items.get_Item(0).colorText;
		guiLabel.boundries = new Rect(6f, 0f, 260f, 14f);
		guiButton.Caption = string.Empty;
		guiLabel.FontSize = 12;
		guiLabel.WordWrap = true;
		Chat.items.get_Item(0).timeHeight = guiLabel.TextHeight;
		Chat.items.get_Item(0).height = guiLabel.TextHeight;
		guiLabel.boundries.set_height(guiLabel.TextHeight);
		guiLabel.Y = __ChatWindow.labelPositionY.get_Item(num);
		Chat.items.get_Item(0).y = __ChatWindow.labelPositionY.get_Item(num);
		guiButton.Y = __ChatWindow.labelPositionY.get_Item(num);
		fontMedium.Y = __ChatWindow.labelPositionY.get_Item(num);
		guiTexture.boundries.set_y(Chat.items.get_Item(0).y);
		List<float> list = __ChatWindow.labelPositionY;
		List<float> list1 = list;
		int num1 = num;
		float item = list1.get_Item(num1);
		list.set_Item(num1, item + guiLabel.TextHeight);
		if (Chat.chatType == 1 && __ChatWindow.btnUniverseChat.IsClicked)
		{
			if (Chat.items.get_Item(0).sendByAdmin)
			{
				this.chatScroll.AddContent(guiTexture);
			}
			this.ManageChatScrollInsert(guiButton, guiLabel, fontMedium, Chat);
		}
		if (Chat.chatType == 2 && __ChatWindow.btnGuildChat.IsClicked)
		{
			if (Chat.items.get_Item(0).sendByAdmin)
			{
				this.chatScroll.AddContent(guiTexture);
			}
			this.ManageChatScrollInsert(guiButton, guiLabel, fontMedium, Chat);
		}
		if (Chat.chatType == 3 && __ChatWindow.btnPartyChat.IsClicked)
		{
			if (Chat.items.get_Item(0).sendByAdmin)
			{
				this.chatScroll.AddContent(guiTexture);
			}
			this.ManageChatScrollInsert(guiButton, guiLabel, fontMedium, Chat);
		}
		if (Chat.chatType == 5 && __ChatWindow.btnFactionChat.IsClicked)
		{
			if (Chat.items.get_Item(0).sendByAdmin)
			{
				this.chatScroll.AddContent(guiTexture);
			}
			this.ManageChatScrollInsert(guiButton, guiLabel, fontMedium, Chat);
		}
		if (Chat.chatType == 6 && __ChatWindow.btnLanguageChat != null && __ChatWindow.btnLanguageChat.IsClicked)
		{
			if (Chat.items.get_Item(0).sendByAdmin)
			{
				this.chatScroll.AddContent(guiTexture);
			}
			this.ManageChatScrollInsert(guiButton, guiLabel, fontMedium, Chat);
		}
		if (Chat.chatType == 4 && __ChatWindow.btnPrivateChat.IsClicked)
		{
			if (Chat.items.get_Item(0).sendByAdmin)
			{
				this.chatScroll.AddContent(guiTexture);
			}
			this.ManageChatScrollInsert(guiButton, guiLabel, fontMedium, Chat);
		}
		base.RemoveGuiElement(__ChatWindow.dummyLabel);
	}

	private void LooseChatFocus()
	{
		AndromedaGui.gui.RequestFocusOnControl(string.Empty);
		__ChatWindow.isOnFocus = false;
	}

	private void ManageChangeFractionButton()
	{
		if (this.isPlayerChatAdmin && __ChatWindow.btnChangeFraction != null)
		{
			if (__ChatWindow.isSmallChat || __ChatWindow.activeChat.chatType != 5)
			{
				__ChatWindow.btnChangeFraction.isActive = false;
				base.RemoveGuiElement(__ChatWindow.btnChangeFraction);
			}
			else
			{
				__ChatWindow.btnChangeFraction.isActive = true;
				base.RemoveGuiElement(__ChatWindow.btnChangeFraction);
				base.AddGuiElement(__ChatWindow.btnChangeFraction);
			}
		}
	}

	private void ManageChatScrollInsert(GuiButton btnName, GuiLabel lblMessage, GuiLabel lblname, ChatItems Chat)
	{
		if (!this.chatScroll.CheckIfThumbIsDown())
		{
			this.chatScroll.AddContent(btnName);
			this.chatScroll.AddContent(lblMessage);
			this.chatScroll.AddContent(lblname);
			if (Chat.items.get_Item(0).playerName == NetworkScript.player.playerBelongings.playerName)
			{
				this.chatScroll.MoveToEndOfContainer();
			}
		}
		else
		{
			this.chatScroll.AddContent(btnName);
			this.chatScroll.AddContent(lblMessage);
			this.chatScroll.AddContent(lblname);
			this.chatScroll.SetArrowStep(lblMessage.TextHeight);
			this.chatScroll.MultipleClickDownArrow(1);
		}
		if (Chat.firstLineWithScroller)
		{
			this.chatScroll.MoveToEndOfContainer();
			Chat.firstLineWithScroller = false;
		}
	}

	private void ManageReceiverGUI()
	{
		if (!__ChatWindow.btnPrivateChat.IsClicked || __ChatWindow.privateChat.receiverName == null)
		{
			this.RemoveReceiverListGUI();
		}
		else
		{
			this.InitReceiverListGUI();
			this.btnReceiverList.Caption = __ChatWindow.privateChat.receiverName;
			this.lblDummy.FontSize = this.btnReceiverList.FontSize;
			this.lblDummy.text = __ChatWindow.privateChat.receiverName;
			if (this.lblDummy.TextWidth >= this.btnReceiverList.boundries.get_width() - 2f)
			{
				this.btnReceiverList.Caption = string.Concat(this.btnReceiverList.Caption.Remove(10, this.btnReceiverList.Caption.get_Length() - 10), "..");
			}
		}
	}

	private void ManageSystemMessage(ChatType chatType, ChatMessage message)
	{
		ChatMessage chatMessage = new ChatMessage();
		if (message.isSystemMessage)
		{
			chatMessage.chatType = chatType;
			chatMessage.isSystemMessage = true;
			chatMessage.sendByAdmin = false;
			chatMessage.senderName = string.Format("({0})", StaticData.Translate("key_chat_admin"));
			chatMessage.text = message.text;
			chatMessage.senderFractionId = message.senderFractionId;
		}
		__ChatWindow.AddChatItem((chatType != 1 ? __ChatWindow.languageChat : __ChatWindow.universeChat), (!message.isSystemMessage ? message : chatMessage));
	}

	public void OnbtnLabelMenuCancelClick(EventHandlerParam prm)
	{
		if (this.labelMenu != null)
		{
			AndromedaGui.gui.RemoveWindow(this.labelMenu.handler);
			__ChatWindow.isbtnLabelMenuCancelClicked = true;
			this.isLabelMenuOpen = false;
		}
	}

	private void OnbtnLabelMenuChatAdminCommand(EventHandlerParam prm)
	{
		int num = (int)prm.customData;
		string str = (string)prm.customData2;
		switch (num)
		{
			case 0:
			{
				this.tbSendMessage.text = string.Concat("/ban ", str, " 4");
				break;
			}
			case 1:
			{
				this.tbSendMessage.text = string.Concat("/mute ", str, " 4");
				break;
			}
			case 2:
			{
				this.tbSendMessage.text = string.Concat("/slap ", str, " 100");
				break;
			}
		}
		this.OnbtnLabelMenuCancelClick(null);
		AndromedaGui.gui.RequestFocusOnControl("tbChatSend");
		__ChatWindow.isOnFocus = true;
	}

	private void OnbtnLabelMenuStartChat(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void __ChatWindow::OnbtnLabelMenuStartChat(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnbtnLabelMenuStartChat(EventHandlerParam)
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

	private void OnBtnNameHover(object prm, bool state)
	{
		if (prm == null)
		{
			return;
		}
		HoverParamPair _white = (HoverParamPair)prm;
		if (!state)
		{
			_white.guiLbl.TextColor = _white.originalColor;
		}
		else
		{
			_white.guiLbl.TextColor = Color.get_white();
		}
	}

	private void OnBtnReceiverClicked(EventHandlerParam prm)
	{
		this.OnbtnLabelMenuCancelClick(null);
		if (this.receiversScollerContainer == null)
		{
			if (__ChatWindow.receiversList.get_Count() <= 0)
			{
				return;
			}
			this.lblDummy = new GuiLabel();
			int num = 2;
			foreach (string str in __ChatWindow.receiversList)
			{
				if (str == __ChatWindow.privateChat.receiverName)
				{
					continue;
				}
				if (this.receiversScollerContainer == null)
				{
					this.receiversScollerContainer = new GuiScrollingContainer(this.btnReceiverList.X, this.btnReceiverList.Y + this.btnReceiverList.boundries.get_height() + 2f, 194f, 255f, 2, __ChatWindow.wnd);
					this.receiversScollerContainer.SetArrowStep(40f);
					base.AddGuiElement(this.receiversScollerContainer);
				}
				if (this.txReceiverContainer == null)
				{
					this.txReceiverContainer = new GuiTexture()
					{
						X = this.btnReceiverList.X - 2f,
						Y = this.btnReceiverList.Y + this.btnReceiverList.boundries.get_height() + 2f
					};
					this.txReceiverContainer.SetTexture("iPad/Chat", "txrRecevierContainer");
					base.AddGuiElement(this.txReceiverContainer);
				}
				this.btnReceiverName = new GuiButtonFixed()
				{
					Caption = str,
					FontSize = 12
				};
				this.lblDummy.text = str;
				this.lblDummy.FontSize = this.btnReceiverName.FontSize;
				this.btnReceiverName.Y = (float)num;
				this.btnReceiverName.X = 2f;
				this.btnReceiverName.boundries.set_width(160f);
				this.btnReceiverName.boundries.set_height(40f);
				this.btnReceiverName.SetTexture("iPad/Chat", "btnReceiverList");
				this.btnReceiverName.Alignment = 4;
				this.btnReceiverName.textColorNormal = GuiNewStyleBar.blueColor;
				this.btnReceiverName.textColorClick = Color.get_white();
				this.btnReceiverName.eventHandlerParam = new EventHandlerParam()
				{
					customData = str
				};
				this.btnReceiverName.Clicked = new Action<EventHandlerParam>(this, __ChatWindow.OnReceiverClicked);
				if (this.lblDummy.TextWidth >= this.btnReceiverName.boundries.get_width() - 2f)
				{
					this.btnReceiverName.Caption = string.Concat(this.btnReceiverName.Caption.Remove(10, this.btnReceiverName.Caption.get_Length() - 10), "..");
				}
				this.receiversScollerContainer.AddContent(this.btnReceiverName);
				num = num + 42;
			}
			this.lblDummy = null;
		}
		else
		{
			base.RemoveGuiElement(this.receiversScollerContainer);
			this.receiversScollerContainer = null;
			base.RemoveGuiElement(this.txReceiverContainer);
			this.txReceiverContainer = null;
		}
	}

	private void OnChangeFraction(EventHandlerParam prm)
	{
		if (!((string)prm.customData).Contains("1"))
		{
			this.fractionIcon = string.Format("fraction{0}Icon", 1);
			this.adminSenderFraction = 1;
			__ChatWindow.btnChangeFraction.X = 225f;
			__ChatWindow.btnChangeFraction.boundries.set_width(23f);
		}
		else
		{
			this.fractionIcon = string.Format("fraction{0}Icon", 2);
			this.adminSenderFraction = 2;
			__ChatWindow.btnChangeFraction.X = 225f;
			__ChatWindow.btnChangeFraction.boundries.set_width(24f);
		}
		__ChatWindow.btnChangeFraction.SetTexture("FrameworkGUI", this.fractionIcon);
		__ChatWindow.btnChangeFraction.eventHandlerParam = new EventHandlerParam()
		{
			customData = this.fractionIcon
		};
	}

	private void OnCloseBtnClick(object prm)
	{
		AndromedaGui.gui.RemoveWindow(__ChatWindow.wnd.handler);
		__ChatWindow.isOpen = false;
	}

	private void OnDropDownClicked(EventHandlerParam p)
	{
		this.OnbtnLabelMenuCancelClick(null);
		if (this.receiversScollerContainer == null)
		{
			int num = 0;
			this.receiversScollerContainer = new GuiScrollingContainer(this.btnDropDownList.X, this.btnDropDownList.Y + this.btnDropDownList.boundries.get_height() + 2f, 115f, 150f, 2, __ChatWindow.wnd);
			this.receiversScollerContainer.SetArrowStep(15f);
			base.AddGuiElement(this.receiversScollerContainer);
			if (__ChatWindow.receiversList != null && __ChatWindow.receiversList.get_Count() > 0)
			{
				foreach (string str in __ChatWindow.receiversList)
				{
					GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
					guiButtonResizeable.SetTexture("chat", "btnSelect_");
					guiButtonResizeable.X = 0f;
					guiButtonResizeable.Y = (float)(15 * num);
					guiButtonResizeable.Width = 94f;
					guiButtonResizeable.Caption = str;
					guiButtonResizeable.FontSize = 12;
					guiButtonResizeable.textColorNormal = Color.get_white();
					guiButtonResizeable.textColorHover = Color.get_white();
					guiButtonResizeable.Alignment = 4;
					guiButtonResizeable.isEnabled = true;
					guiButtonResizeable.eventHandlerParam = new EventHandlerParam()
					{
						customData = str
					};
					guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, __ChatWindow.OnReceiverSelect);
					num++;
					this.lblDummy.FontSize = guiButtonResizeable.FontSize;
					this.lblDummy.text = guiButtonResizeable.Caption;
					if (this.lblDummy.TextWidth >= guiButtonResizeable.boundries.get_width() - 4f)
					{
						guiButtonResizeable.Caption = string.Concat(guiButtonResizeable.Caption.Remove(10, guiButtonResizeable.Caption.get_Length() - 10), "..");
					}
					this.receiversScollerContainer.AddContent(guiButtonResizeable);
				}
			}
		}
		else
		{
			base.RemoveGuiElement(this.receiversScollerContainer);
			this.receiversScollerContainer = null;
		}
	}

	public static void OnMouseDown(GuiWindow wndClicked)
	{
		if (__ChatWindow.wnd == null || AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activeWindow != null)
		{
			return;
		}
		if (wndClicked == null || wndClicked.handler != __ChatWindow.wnd.handler)
		{
			__ChatWindow.isOnFocus = false;
		}
		else
		{
			if (wndClicked.handler == __ChatWindow.wnd.handler)
			{
				__ChatWindow.isOnFocus = true;
				__ChatWindow.wnd.GainChatFocus();
			}
			if (__ChatWindow.btnShowHide.isHovered)
			{
				__ChatWindow.isOnFocus = false;
				__ChatWindow.wnd.LooseChatFocus();
			}
			if (__ChatWindow.isbtnNameClicked)
			{
				__ChatWindow.isOnFocus = false;
				__ChatWindow.wnd.LooseChatFocus();
				__ChatWindow.isbtnNameClicked = false;
			}
			if (__ChatWindow.isbtnLabelMenuCancelClicked)
			{
				__ChatWindow.isOnFocus = false;
				__ChatWindow.wnd.LooseChatFocus();
				__ChatWindow.isbtnLabelMenuCancelClicked = false;
			}
			if (__ChatWindow.isbtnLabelMenuProfileClicked)
			{
				__ChatWindow.isOnFocus = false;
				__ChatWindow.wnd.LooseChatFocus();
				__ChatWindow.isbtnLabelMenuProfileClicked = false;
			}
			if (__ChatWindow.isbtnLabelMenuReportClicked)
			{
				__ChatWindow.isOnFocus = false;
				__ChatWindow.wnd.LooseChatFocus();
				__ChatWindow.isbtnLabelMenuReportClicked = false;
			}
			if (__ChatWindow.isbtnLabelMenuPrivateChatClicked)
			{
				__ChatWindow.isOnFocus = true;
				__ChatWindow.wnd.GainChatFocus();
				__ChatWindow.isbtnLabelMenuPrivateChatClicked = false;
			}
			if (!__ChatWindow.isSmallChat && __ChatWindow.btnPrivateChat != null && __ChatWindow.btnPrivateChat.IsClicked && __ChatWindow.wnd.btnDropDownList != null && __ChatWindow.wnd.receiversScollerContainer != null && !__ChatWindow.wnd.btnDropDownList.IsMouseOver && !__ChatWindow.wnd.receiversScollerContainer.IsMouseOver)
			{
				__ChatWindow.wnd.RemoveGuiElement(__ChatWindow.wnd.receiversScollerContainer);
				__ChatWindow.wnd.receiversScollerContainer = null;
			}
		}
	}

	private void OnProfileClick(EventHandlerParam p)
	{
		NetworkScript.RequestUserProfile((string)p.customData);
		this.OnbtnLabelMenuCancelClick(null);
	}

	private void OnReceiverClicked(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void __ChatWindow::OnReceiverClicked(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnReceiverClicked(EventHandlerParam)
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

	private void OnReceiverSelect(EventHandlerParam itemParam)
	{
		// 
		// Current member / type: System.Void __ChatWindow::OnReceiverSelect(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnReceiverSelect(EventHandlerParam)
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

	private void OnReportClick(EventHandlerParam p)
	{
		// 
		// Current member / type: System.Void __ChatWindow::OnReportClick(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnReportClick(EventHandlerParam)
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

	private void OnSendClicked(EventHandlerParam p)
	{
		string str = this.tbSendMessage.text;
		if (this.tbSendMessage.text.get_Length() > 0 && this.tbSendMessage.text != new string(' ', this.tbSendMessage.text.get_Length()) && this.tbSendMessage.text != " " && !string.IsNullOrEmpty(this.tbSendMessage.text))
		{
			if (str.get_Length() > 100)
			{
				str = str.Substring(0, 100);
			}
			if (str.StartsWith("/"))
			{
				this.SplitAndSend(str);
			}
			else if (!NetworkScript.player.isChatAdmin && this.CheckForSpam(str))
			{
				this.AddSpamMessage(StaticData.Translate("key_chat_please_do_not_spam"));
				__ChatWindow.isOnFocus = false;
				__ChatWindow.wnd.LooseChatFocus();
			}
			else if (NetworkScript.player.isChatAdmin || __ChatWindow.activeChat.items.get_Count() <= 0)
			{
				this.SendMessage(str);
			}
			else
			{
				this.CheckForDuplicateMessage(str);
			}
		}
	}

	public static void OnStartChatResponse(GenericData data)
	{
		if (data.str1.Contains("exist"))
		{
			__ChatWindow.btnPrivateChat.IsClicked = true;
			string[] strArray = data.str1.Split(new char[] { ' ' });
			string str = string.Format(StaticData.Translate("key_chat_player_not_exist"), strArray[1]);
			__ChatWindow.wnd.AddSpamMessage(str);
			__ChatWindow.wnd.tbSendMessage.text = string.Empty;
			__ChatWindow.isOnFocus = false;
			__ChatWindow.wnd.LooseChatFocus();
		}
		else if (data.str1.Contains("online"))
		{
			__ChatWindow.btnPrivateChat.IsClicked = true;
			string[] strArray1 = data.str1.Split(new char[] { ' ' });
			string str1 = string.Format(StaticData.Translate("key_chat_player_not_online"), strArray1[1]);
			__ChatWindow.wnd.AddSpamMessage(str1);
			__ChatWindow.wnd.tbSendMessage.text = string.Empty;
			__ChatWindow.isOnFocus = false;
			__ChatWindow.wnd.LooseChatFocus();
			__ChatWindow.wnd.UpdateReceiverListWhenOffline(strArray1[1]);
		}
		else if (data.str1.Contains("invited"))
		{
			__ChatWindow.btnPrivateChat.IsClicked = true;
			string[] strArray2 = data.str1.Split(new char[] { ' ' });
			string str2 = string.Format(StaticData.Translate("key_chat_player_cannot_invite"), strArray2[1]);
			__ChatWindow.wnd.AddSpamMessage(str2);
			__ChatWindow.wnd.tbSendMessage.text = string.Empty;
			__ChatWindow.isOnFocus = false;
			__ChatWindow.wnd.LooseChatFocus();
		}
		else if (data.str1.Contains("banned"))
		{
			__ChatWindow.wnd.AddSpamMessage(StaticData.Translate("key_chat_banned_ok"));
			__ChatWindow.wnd.tbSendMessage.text = string.Empty;
			__ChatWindow.isOnFocus = false;
			__ChatWindow.wnd.LooseChatFocus();
		}
		else if (data.str1.Contains("mutted"))
		{
			__ChatWindow.wnd.AddSpamMessage(StaticData.Translate("key_chat_muted_ok"));
			__ChatWindow.wnd.tbSendMessage.text = string.Empty;
			__ChatWindow.isOnFocus = false;
			__ChatWindow.wnd.LooseChatFocus();
		}
		else if (data.str1.Contains("slapped"))
		{
			__ChatWindow.wnd.AddSpamMessage(StaticData.Translate("key_chat_slapped_ok"));
			__ChatWindow.wnd.tbSendMessage.text = string.Empty;
			__ChatWindow.isOnFocus = false;
			__ChatWindow.wnd.LooseChatFocus();
		}
		else if (data.str1.Contains("correct"))
		{
			__ChatWindow.wnd.AddSpamMessage(StaticData.Translate("key_chat_command_notfull"));
			__ChatWindow.wnd.tbSendMessage.text = string.Empty;
			__ChatWindow.isOnFocus = false;
			__ChatWindow.wnd.LooseChatFocus();
		}
		else if (!data.str1.Contains("report"))
		{
			__ChatWindow.btnPrivateChat.IsClicked = true;
			__ChatWindow.activeChat.recieverID = data.long1;
			__ChatWindow.activeChat.receiverName = data.str1;
			__ChatWindow.privateChat = __ChatWindow.activeChat;
			__ChatWindow.wnd.tbSendMessage.text = string.Empty;
			if (!string.IsNullOrEmpty(__ChatWindow.wnd.message) && __ChatWindow.wnd.message != string.Empty && __ChatWindow.wnd.message != " ")
			{
				__ChatWindow.wnd.SendMessage(__ChatWindow.wnd.message);
				__ChatWindow.wnd.message = null;
			}
			__ChatWindow.wnd.AddNewReceiver(data.str1);
		}
		else
		{
			__ChatWindow.wnd.AddSpamMessage(StaticData.Translate("key_chat_report_ok"));
			__ChatWindow.wnd.tbSendMessage.text = string.Empty;
			__ChatWindow.isOnFocus = false;
			__ChatWindow.wnd.LooseChatFocus();
		}
	}

	private void OnTrackEnterKey()
	{
		this.UpdateBlinkButtons();
		if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activeWindow != null)
		{
			return;
		}
		if (Event.get_current().get_keyCode() == 13 && Event.get_current().get_type() == 4)
		{
			if (this.tbSendMessage.text == string.Empty && __ChatWindow.isOnFocus)
			{
				this.LooseChatFocus();
			}
			else if (!__ChatWindow.isOnFocus && this.tbSendMessage.text == string.Empty)
			{
				this.GainChatFocus();
			}
			if (this.tbSendMessage.text != string.Empty && !__ChatWindow.isOnFocus)
			{
				this.GainChatFocus();
			}
		}
	}

	public static void OpenTheWindow(bool requiresFocus)
	{
		if (NetworkScript.player == null)
		{
			return;
		}
		if (__ChatWindow.wnd != null)
		{
			__ChatWindow.wnd = new __ChatWindow();
			__ChatWindow.isSmallChat = false;
			__ChatWindow.wnd.Create();
			AndromedaGui.gui.AddWindow(__ChatWindow.wnd);
		}
		else
		{
			__ChatWindow.wnd = new __ChatWindow();
			__ChatWindow.wnd.Create();
			if (__ChatWindow.isOpen)
			{
				AndromedaGui.gui.AddWindow(__ChatWindow.wnd);
			}
		}
		__ChatWindow.wnd.boundries.set_y((float)Screen.get_height() - __ChatWindow.wnd.boundries.get_height() + 2f);
		if (__ChatWindow.isNotOpenHasUnreadMessage && !NetworkScript.isInBase)
		{
			AndromedaGui.mainWnd.StartChatAnimation();
		}
	}

	private void PopulateChatRoom(ChatItems Chat)
	{
		string str;
		EventHandlerParam eventHandlerParam;
		this.chatScroll.Claer();
		if (Chat.items.get_Count() != 0)
		{
			foreach (ChatItem item in Chat.items)
			{
				GuiLabel guiLabel = new GuiLabel();
				GuiButton guiButton = new GuiButton();
				GuiTexture guiTexture = new GuiTexture();
				GuiLabel fontMedium = new GuiLabel();
				__ChatWindow.dummyLabel = new GuiLabel()
				{
					FontSize = 12,
					Alignment = 6
				};
				fontMedium.FontSize = 12;
				fontMedium.Font = GuiLabel.FontMedium;
				guiButton.FontSize = 12;
				guiLabel.FontSize = 12;
				guiButton.boundries = new Rect(6f, 0f, 260f, 14f);
				guiLabel.boundries = new Rect(6f, 0f, 260f, 14f);
				fontMedium.boundries = new Rect(6f, 0f, 260f, 14f);
				if (Chat.chatType != 4 || item.playerId != NetworkScript.player.vessel.playerId)
				{
					fontMedium.text = string.Format("({0:HH:mm}){1}: ", item.time, item.playerName);
					guiLabel.text = string.Concat(new string(' ', (int)(fontMedium.TextWidth / 2.9f) + 1), item.text);
					if (item.sendByAdmin)
					{
						guiButton.boundries.set_width(260f);
						guiTexture.SetTexture("chat", "admin_icon");
						guiLabel.boundries = new Rect(6f, 0f, 260f, 22f);
						eventHandlerParam = new EventHandlerParam()
						{
							customData = StaticData.Translate("key_chat_admin"),
							customData2 = guiTexture
						};
						guiTexture.tooltipWindowParam = eventHandlerParam;
						guiTexture.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
						guiTexture.isHoverAware = true;
						guiTexture.boundries.set_width(14f);
						guiTexture.boundries.set_height(14f);
						guiButton.boundries.set_x(guiLabel.boundries.get_x());
						str = string.Concat(item.playerName, ":");
						fontMedium.text = string.Concat(string.Format("({0:HH:mm})", item.time), new string(' ', 5), str);
						__ChatWindow.dummyLabel.text = string.Format("({0:HH:mm})", item.time);
						guiTexture.boundries.set_x(__ChatWindow.dummyLabel.TextWidth + 7f);
						guiLabel.text = string.Concat(new string(' ', (int)(fontMedium.TextWidth / 2.9f)), item.text);
					}
					if (item.isSystemMessage)
					{
						item.colorText = Color.get_red();
						item.colorName = Color.get_red();
					}
					else if (NetworkScript.player.vessel.fractionId != item.senderFactionId)
					{
						item.colorText = GuiNewStyleBar.blueColor;
						item.colorName = GuiNewStyleBar.purpleColor;
					}
				}
				else
				{
					fontMedium.text = string.Format("({0:HH:mm}){1}->{2}: ", item.time, item.playerName, item.receiverName);
					__ChatWindow.dummyLabel.text = guiButton.Caption;
					guiLabel.text = new string(' ', (int)(fontMedium.TextWidth / 2.9f));
					GuiLabel guiLabel1 = guiLabel;
					guiLabel1.text = string.Concat(guiLabel1.text, item.text);
					if (item.sendByAdmin)
					{
						guiButton.boundries.set_width(260f);
						guiTexture.SetTexture("chat", "admin_icon");
						guiLabel.boundries = new Rect(6f, 0f, 260f, 14f);
						eventHandlerParam = new EventHandlerParam()
						{
							customData = StaticData.Translate("key_chat_admin"),
							customData2 = guiTexture
						};
						guiTexture.tooltipWindowParam = eventHandlerParam;
						guiTexture.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
						guiTexture.isHoverAware = true;
						guiTexture.boundries.set_width(14f);
						guiTexture.boundries.set_height(14f);
						guiButton.boundries.set_x(guiLabel.boundries.get_x());
						str = string.Concat(item.playerName, "->", item.receiverName, ":");
						fontMedium.text = string.Concat(string.Format("({0:HH:mm})", item.time), new string(' ', 5), str);
						__ChatWindow.dummyLabel.text = string.Format("({0:HH:mm})", item.time);
						guiTexture.boundries.set_x(__ChatWindow.dummyLabel.TextWidth + 7f);
						__ChatWindow.dummyLabel.text = guiButton.Caption;
						str = __ChatWindow.dummyLabel.text;
						guiLabel.text = string.Concat(new string(' ', (int)(fontMedium.TextWidth / 2.9f)), item.text);
					}
				}
				if (NetworkScript.player.playId != item.playerId)
				{
					if (!item.isSystemMessage && NetworkScript.player.vessel.fractionId == item.senderFactionId)
					{
						item.colorText = GuiNewStyleBar.blueColor;
						item.colorName = GuiNewStyleBar.orangeColor;
					}
					guiButton.Clicked = new Action<EventHandlerParam>(this, __ChatWindow.BuildLabelMenuWnd);
					guiButton.eventHandlerParam.customData = item;
					guiButton.eventHandlerParam.customData2 = __ChatWindow.dummyLabel.TextWidth;
					HoverParamPair hoverParamPair = new HoverParamPair()
					{
						guiLbl = fontMedium,
						originalColor = item.colorName
					};
					guiButton.hoverParam = hoverParamPair;
					fontMedium.TextColor = item.colorName;
					guiButton.Hovered = new Action<object, bool>(this, __ChatWindow.OnBtnNameHover);
				}
				else
				{
					if (!item.isCustomMessage)
					{
						item.colorText = Color.get_green();
						item.colorName = Color.get_green();
					}
					fontMedium.TextColor = item.colorName;
				}
				if (item.isSpam)
				{
					item.colorText = Color.get_yellow();
					item.colorName = Color.get_yellow();
					guiButton.Caption = string.Empty;
					fontMedium.text = string.Empty;
					guiButton.boundries.set_width((float)(guiButton.Caption.get_Length() * 6));
					guiLabel.text = string.Format("({0:HH:mm}){1} ", item.time, item.text);
				}
				if (item.isCustomMessage)
				{
					guiButton.Caption = string.Empty;
					fontMedium.text = string.Empty;
					guiButton.boundries.set_width((float)(guiButton.Caption.get_Length() * 6));
					guiLabel.text = string.Format("({0:HH:mm}){1} ", item.time, item.text);
				}
				if (item.isDuplicate)
				{
					item.colorText = Color.get_yellow();
					item.colorName = Color.get_yellow();
					guiButton.Caption = string.Empty;
					fontMedium.text = string.Empty;
					guiButton.boundries.set_width((float)(guiButton.Caption.get_Length() * 6));
					guiLabel.text = string.Format("({0:HH:mm}){1} ", item.time, StaticData.Translate("key_chat_you_have_already_said_that"));
				}
				item.timeHeight = guiLabel.TextHeight;
				item.height = guiLabel.TextHeight;
				guiLabel.TextColor = item.colorText;
				guiLabel.WordWrap = true;
				guiButton.Caption = string.Empty;
				guiButton.boundries.set_height(guiLabel.TextHeight);
				guiLabel.boundries.set_height(guiButton.boundries.get_height());
				guiLabel.Y = item.y;
				guiButton.Y = item.y;
				fontMedium.Y = item.y;
				guiTexture.boundries.set_y(guiLabel.Y);
				this.chatScroll.AddContent(fontMedium);
				this.chatScroll.AddContent(guiLabel);
				if (item.sendByAdmin)
				{
					this.chatScroll.AddContent(guiTexture);
				}
				if (NetworkScript.player.playId == item.playerId)
				{
					continue;
				}
				this.chatScroll.AddContent(guiButton);
			}
		}
		base.RemoveGuiElement(__ChatWindow.dummyLabel);
		this.PositionScroller(Chat);
	}

	public void PopulateData(PlayerProfile data)
	{
		GuiButtonFixed guiButtonFixed = __ChatWindow.btnPartyChat;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = (!data.isInParty ? StaticData.Translate("key_chat_you_have_no_party_mate") : StaticData.Translate("key_chat_party")),
			customData2 = __ChatWindow.btnPartyChat
		};
		guiButtonFixed.tooltipWindowParam = eventHandlerParam;
		__ChatWindow.btnPartyChat.isEnabled = (!data.isInParty ? false : true);
		GuiButtonFixed guiButtonFixed1 = __ChatWindow.btnGuildChat;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = (NetworkScript.player.guild == null ? StaticData.Translate("key_chat_you_have_no_guild_mate") : StaticData.Translate("key_chat_guild")),
			customData2 = __ChatWindow.btnGuildChat
		};
		guiButtonFixed1.tooltipWindowParam = eventHandlerParam;
		__ChatWindow.btnGuildChat.isEnabled = (NetworkScript.player.guild == null ? false : true);
	}

	private void PositionScroller(ChatItems Chat)
	{
		if (__ChatWindow.scrollerPosition != null)
		{
			switch (Chat.chatType)
			{
				case 1:
				{
					if (__ChatWindow.scrollerPosition.get_Item(0) != 0f)
					{
						this.chatScroll.MooveTumb(0f, __ChatWindow.scrollerPosition.get_Item(0));
					}
					else
					{
						this.chatScroll.MoveToEndOfContainer();
					}
					break;
				}
				case 2:
				{
					if (__ChatWindow.scrollerPosition.get_Item(3) != 0f)
					{
						this.chatScroll.MooveTumb(0f, __ChatWindow.scrollerPosition.get_Item(3));
					}
					else
					{
						this.chatScroll.MoveToEndOfContainer();
					}
					break;
				}
				case 3:
				{
					if (__ChatWindow.scrollerPosition.get_Item(4) != 0f)
					{
						this.chatScroll.MooveTumb(0f, __ChatWindow.scrollerPosition.get_Item(4));
					}
					else
					{
						this.chatScroll.MoveToEndOfContainer();
					}
					break;
				}
				case 4:
				{
					if (__ChatWindow.scrollerPosition.get_Item(5) != 0f)
					{
						this.chatScroll.MooveTumb(0f, __ChatWindow.scrollerPosition.get_Item(5));
					}
					else
					{
						this.chatScroll.MoveToEndOfContainer();
					}
					break;
				}
				case 5:
				{
					if (__ChatWindow.scrollerPosition.get_Item(1) != 0f)
					{
						this.chatScroll.MooveTumb(0f, __ChatWindow.scrollerPosition.get_Item(1));
					}
					else
					{
						this.chatScroll.MoveToEndOfContainer();
					}
					break;
				}
				case 6:
				{
					if (__ChatWindow.scrollerPosition.get_Item(2) != 0f)
					{
						this.chatScroll.MooveTumb(0f, __ChatWindow.scrollerPosition.get_Item(2));
					}
					else
					{
						this.chatScroll.MoveToEndOfContainer();
					}
					break;
				}
			}
		}
	}

	public void ProcessFriendResponce(byte resultCode, string userName)
	{
		this.AddSpamMessage(this.tbSendMessage.text);
		switch (resultCode)
		{
			case 0:
			{
				this.AddSpamMessage(string.Format(StaticData.Translate("key_profile_screen_return_code_ok"), userName));
				break;
			}
			case 1:
			{
				this.AddSpamMessage(string.Format(StaticData.Translate("key_profile_screen_return_code_not_found"), userName));
				break;
			}
			case 2:
			{
				this.AddSpamMessage(string.Format(StaticData.Translate("key_profile_screen_return_code_already"), userName));
				break;
			}
		}
		this.tbSendMessage.text = string.Empty;
	}

	public static void ReceiveChatMessage(ChatMessage data)
	{
		if (NetworkScript.player != null && NetworkScript.player.vessel != null && NetworkScript.player.vessel.isGuest)
		{
			return;
		}
		int num = 0;
		if (NetworkScript.player.isChatDND)
		{
			num++;
		}
		if (data.chatType == 4)
		{
			num = num + 2;
		}
		if (__ChatWindow.wnd != null)
		{
			num = num + 4;
		}
		if (data.chatType != 4)
		{
			num = num + 8;
		}
		else if (__ChatWindow.privateChats.ContainsKey(data.senderId))
		{
			num = num + 8;
		}
		if (__ChatWindow.wnd != null && __ChatWindow.activeChat != null && data.chatType == __ChatWindow.activeChat.chatType)
		{
			if (data.chatType != 4)
			{
				num = num + 16;
			}
			else if (__ChatWindow.activeChat.playerId == data.senderId)
			{
				num = num + 16;
			}
		}
		switch (num)
		{
			case 0:
			case 1:
			case 4:
			case 5:
			case 16:
			case 17:
			case 18:
			case 19:
			case 20:
			case 21:
			case 22:
			{
				break;
			}
			case 2:
			{
				break;
			}
			case 3:
			case 7:
			{
				break;
			}
			case 6:
			{
				if (data.text.StartsWith("//playerMutedForAnother#") && data.senderId == (long)-1)
				{
					string[] strArray = data.text.Split(new char[] { '#' });
					__ChatWindow.wnd.AddSpamMessage(string.Concat(StaticData.Translate("key_chat_youre_muted_until"), " ", StaticData.now + TimeSpan.Parse(strArray[1])));
				}
				else if (!data.text.StartsWith("//playerNotOnline#") || data.senderId != (long)-1)
				{
					bool flag = false;
					if (!__ChatWindow.btnPrivateChat.IsClicked)
					{
						flag = false;
						__ChatWindow.privateChat.hasUnreadMessages = true;
						__ChatWindow.btnPrivateChat.SetTextureNormal("chat", "btnPrivateHvr");
						__ChatWindow.wnd.InitialiseBlinkFX();
					}
					else
					{
						flag = true;
					}
					if (!__ChatWindow.isOpen && !NetworkScript.isInBase)
					{
						__ChatWindow.isNotOpenHasUnreadMessage = true;
						AndromedaGui.mainWnd.StartChatAnimation();
					}
					__ChatWindow.AddChatItem(__ChatWindow.privateChat, data);
					__ChatWindow.wnd.BuildChatRoom(__ChatWindow.privateChat, flag);
					if (string.IsNullOrEmpty(__ChatWindow.privateChat.receiverName))
					{
						__ChatWindow.privateChat.receiverName = data.senderName;
						__ChatWindow.privateChat.recieverID = data.senderId;
						__ChatWindow.receiversList.Add(data.senderName);
						if (!__ChatWindow.isSmallChat && __ChatWindow.btnPrivateChat != null && __ChatWindow.btnPrivateChat.IsClicked)
						{
							__ChatWindow.wnd.InitReceiverWeb();
						}
					}
					__ChatWindow.wnd.AddNewReceiver(data.senderName);
				}
				else
				{
					__ChatWindow.wnd.AddSpamMessage(string.Format(StaticData.Translate("key_chat_player_not_online"), data.senderName));
					__ChatWindow.wnd.UpdateReceiverListWhenOffline(data.senderName);
				}
				break;
			}
			case 8:
			{
				goto case 29;
			}
			case 9:
			{
				break;
			}
			case 10:
			{
				break;
			}
			case 11:
			{
				break;
			}
			case 12:
			case 13:
			{
				__ChatWindow.AddNonPrivateMessage(data);
				if (data.chatType == 1 && !__ChatWindow.btnUniverseChat.IsClicked)
				{
					__ChatWindow.universeChat.hasUnreadMessages = true;
					__ChatWindow.btnUniverseChat.SetTextureNormal("chat", "btnUniverseHvr");
				}
				if (data.chatType == 2 && !__ChatWindow.btnGuildChat.IsClicked)
				{
					__ChatWindow.guildChat.hasUnreadMessages = true;
					__ChatWindow.btnGuildChat.SetTextureNormal("chat", "btnGuildHvr");
				}
				if (data.chatType == 3 && !__ChatWindow.btnPartyChat.IsClicked)
				{
					__ChatWindow.partyChat.hasUnreadMessages = true;
					__ChatWindow.btnPartyChat.SetTextureNormal("chat", "btnPartyHvr");
				}
				if (data.chatType == 5 && !__ChatWindow.btnFactionChat.IsClicked)
				{
					__ChatWindow.factionChat.hasUnreadMessages = true;
					__ChatWindow.btnFactionChat.SetTextureNormal("chat", string.Concat(__ChatWindow.factionChatAssetName, "clk"));
				}
				if (data.chatType == 6 && __ChatWindow.btnLanguageChat != null && !__ChatWindow.btnLanguageChat.IsClicked)
				{
					__ChatWindow.languageChat.hasUnreadMessages = true;
					__ChatWindow.btnLanguageChat.SetTextureNormal("chat", "btnLanguage_Hvr");
				}
				break;
			}
			case 14:
			{
				break;
			}
			case 15:
			{
				break;
			}
			case 23:
			{
				Debug.Log(string.Concat("Unsupported receive chat message case ", num.ToString()));
				break;
			}
			case 24:
			{
				break;
			}
			case 25:
			{
				break;
			}
			case 26:
			case 27:
			{
				break;
			}
			case 28:
			case 29:
			{
				if (!data.text.StartsWith("//playerMutedForAnother#") || data.senderId != (long)-1)
				{
					__ChatWindow.AddNonPrivateMessage(data);
				}
				else
				{
					string[] strArray1 = data.text.Split(new char[] { '#' });
					__ChatWindow.wnd.AddSpamMessage(string.Concat(StaticData.Translate("key_chat_youre_muted_until"), " ", StaticData.now + TimeSpan.Parse(strArray1[1])));
				}
				if (data.chatType == 1 && !__ChatWindow.btnUniverseChat.IsClicked)
				{
					__ChatWindow.universeChat.hasUnreadMessages = true;
					__ChatWindow.btnUniverseChat.SetTextureNormal("chat", "btnUniverseHvr");
				}
				if (data.chatType == 2 && !__ChatWindow.btnGuildChat.IsClicked)
				{
					__ChatWindow.guildChat.hasUnreadMessages = true;
					__ChatWindow.btnGuildChat.SetTextureNormal("chat", "btnGuildHvr");
				}
				if (data.chatType == 3 && !__ChatWindow.btnPartyChat.IsClicked)
				{
					__ChatWindow.partyChat.hasUnreadMessages = true;
					__ChatWindow.btnPartyChat.SetTextureNormal("chat", "btnPartyHvr");
				}
				if (data.chatType == 5 && !__ChatWindow.btnFactionChat.IsClicked)
				{
					__ChatWindow.factionChat.hasUnreadMessages = true;
					__ChatWindow.btnFactionChat.SetTextureNormal("chat", string.Concat(__ChatWindow.factionChatAssetName, "clk"));
				}
				if (data.chatType == 6 && __ChatWindow.btnLanguageChat != null && !__ChatWindow.btnLanguageChat.IsClicked)
				{
					__ChatWindow.languageChat.hasUnreadMessages = true;
					__ChatWindow.btnLanguageChat.SetTextureNormal("chat", "btnLanguage_Hvr");
				}
				break;
			}
			case 30:
			case 31:
			{
				break;
			}
			default:
			{
				goto case 23;
			}
		}
	}

	private void RemoveChatItems()
	{
		if (__ChatWindow.activeChat == null)
		{
			return;
		}
		foreach (ChatItem chatItem in __ChatWindow.activeChat.onScreen)
		{
			base.RemoveGuiElement(chatItem.lblName);
			base.RemoveGuiElement(chatItem.lblText);
			if (chatItem.btnPrivateChat == null)
			{
				continue;
			}
			base.RemoveGuiElement(chatItem.btnPrivateChat);
		}
		foreach (GuiElement guiElement in this.stuffToRemove)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.stuffToRemove.Clear();
		__ChatWindow.activeChat.onScreen.Clear();
	}

	private void RemoveLabelMenuCrap()
	{
		foreach (GuiElement guiElement in this.labelMenuCrapToRemove)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.labelMenuCrapToRemove.Clear();
	}

	private void RemoveReceiverListGUI()
	{
		if (this.lblReciever != null)
		{
			base.RemoveGuiElement(this.lblReciever);
			this.lblReciever = null;
		}
		if (this.btnReceiverList != null)
		{
			base.RemoveGuiElement(this.btnReceiverList);
			this.btnReceiverList = null;
		}
		if (this.txReceiverContainer != null)
		{
			base.RemoveGuiElement(this.txReceiverContainer);
			this.txReceiverContainer = null;
		}
		if (this.receiversScollerContainer != null)
		{
			base.RemoveGuiElement(this.receiversScollerContainer);
			this.receiversScollerContainer = null;
		}
	}

	private string RetrieveLastTwentyItems(List<ChatItem> items)
	{
		int num = (items.get_Count() <= 20 ? items.get_Count() : 20);
		string empty = string.Empty;
		if (num >= 1)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i <= num - 1; i++)
			{
				if (!string.IsNullOrEmpty(items.get_Item(i).receiverName))
				{
					stringBuilder.Append(string.Concat("##", string.Format("({0:HH:mm}){1}->{2}: {3}", new object[] { items.get_Item(i).time, items.get_Item(i).playerName, items.get_Item(i).receiverName, items.get_Item(i).text })));
				}
				else
				{
					stringBuilder.Append(string.Concat("##", string.Format("({0:HH:mm}){1}: {2}", items.get_Item(i).time, items.get_Item(i).playerName, items.get_Item(i).text)));
				}
			}
			empty = stringBuilder.ToString();
		}
		return empty;
	}

	public void SaveScrollerPosition(ChatItems Chat)
	{
		if (__ChatWindow.scrollerPosition == null)
		{
			__ChatWindow.scrollerPosition = new Dictionary<int, float>();
		}
		switch (Chat.chatType)
		{
			case 1:
			{
				__ChatWindow.scrollerPosition.set_Item(0, (this.chatScroll.CheckIfThumbIsDown() ? 0f : this.chatScroll.scrollerTumbRect.get_y()));
				break;
			}
			case 2:
			{
				__ChatWindow.scrollerPosition.set_Item(3, (this.chatScroll.CheckIfThumbIsDown() ? 0f : this.chatScroll.scrollerTumbRect.get_y()));
				break;
			}
			case 3:
			{
				__ChatWindow.scrollerPosition.set_Item(4, (this.chatScroll.CheckIfThumbIsDown() ? 0f : this.chatScroll.scrollerTumbRect.get_y()));
				break;
			}
			case 4:
			{
				__ChatWindow.scrollerPosition.set_Item(5, (this.chatScroll.CheckIfThumbIsDown() ? 0f : this.chatScroll.scrollerTumbRect.get_y()));
				break;
			}
			case 5:
			{
				__ChatWindow.scrollerPosition.set_Item(1, (this.chatScroll.CheckIfThumbIsDown() ? 0f : this.chatScroll.scrollerTumbRect.get_y()));
				break;
			}
			case 6:
			{
				__ChatWindow.scrollerPosition.set_Item(2, (this.chatScroll.CheckIfThumbIsDown() ? 0f : this.chatScroll.scrollerTumbRect.get_y()));
				break;
			}
		}
	}

	private void SendMessage(string txt)
	{
		// 
		// Current member / type: System.Void __ChatWindow::SendMessage(System.String)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void SendMessage(System.String)
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

	private void SendPartyInvite(EventHandlerParam p)
	{
		// 
		// Current member / type: System.Void __ChatWindow::SendPartyInvite(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void SendPartyInvite(EventHandlerParam)
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

	private void SetCursorToEnd(object prm)
	{
		if (this.tbSendMessage == null)
		{
			return;
		}
		TextEditor stateObject = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.get_keyboardControl());
		if (stateObject != null)
		{
			stateObject.pos = this.tbSendMessage.text.get_Length() + 1;
			stateObject.selectPos = this.tbSendMessage.text.get_Length() + 1;
		}
		this.preDrawHandler = null;
	}

	private void SplitAndSend(string txt)
	{
		// 
		// Current member / type: System.Void __ChatWindow::SplitAndSend(System.String)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void SplitAndSend(System.String)
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
		//    в ..(IfStatement ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 359
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 55
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
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
		//    в ..(IfStatement ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 359
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 55
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
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
		//    в ..(IfStatement ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 359
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 55
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
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

	public void StartChatByUsername(string name)
	{
		// 
		// Current member / type: System.Void __ChatWindow::StartChatByUsername(System.String)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void StartChatByUsername(System.String)
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

	private void SwitchChatRoom(EventHandlerParam p)
	{
		this.SaveScrollerPosition(__ChatWindow.activeChat);
		this.OnbtnLabelMenuCancelClick(null);
		__ChatWindow.isbtnLabelMenuCancelClicked = false;
		this.RemoveLabelMenuCrap();
		this.RemoveChatItems();
		switch ((int)p.customData)
		{
			case 0:
			{
				if (!__ChatWindow.isSmallChat)
				{
					this.AssignBackgroundTexure(1);
					__ChatWindow.btnUniverseChat.SetTextureNormal("chat", "btnUniverseNml");
					this.boundries.set_x((float)Screen.get_width() - this.boundries.get_width());
					this.boundries.set_y((float)Screen.get_height() - this.boundries.get_height() - 25f);
					this.boundries.set_height(this.boundries.get_height() + 25f);
				}
				__ChatWindow.activeChat = __ChatWindow.universeChat;
				__ChatWindow.universeChat.hasUnreadMessages = false;
				__ChatWindow.lastChatChecked = __ChatWindow.universeChat;
				break;
			}
			case 1:
			{
				if (!__ChatWindow.isSmallChat)
				{
					this.AssignBackgroundTexure(2);
					__ChatWindow.btnGuildChat.SetTextureNormal("chat", "btnGuildNml");
					this.boundries.set_x((float)Screen.get_width() - this.boundries.get_width());
					this.boundries.set_y((float)Screen.get_height() - this.boundries.get_height() - 25f);
					this.boundries.set_height(this.boundries.get_height() + 25f);
				}
				__ChatWindow.activeChat = __ChatWindow.guildChat;
				__ChatWindow.guildChat.hasUnreadMessages = false;
				__ChatWindow.lastChatChecked = __ChatWindow.guildChat;
				break;
			}
			case 2:
			{
				if (!__ChatWindow.isSmallChat)
				{
					this.AssignBackgroundTexure(3);
					__ChatWindow.btnPartyChat.SetTextureNormal("chat", "btnPartyNml");
					this.boundries.set_x((float)Screen.get_width() - this.boundries.get_width());
					this.boundries.set_y((float)Screen.get_height() - this.boundries.get_height() - 25f);
					this.boundries.set_height(this.boundries.get_height() + 25f);
				}
				__ChatWindow.activeChat = __ChatWindow.partyChat;
				__ChatWindow.partyChat.hasUnreadMessages = false;
				__ChatWindow.lastChatChecked = __ChatWindow.partyChat;
				break;
			}
			case 3:
			{
				if (!__ChatWindow.isSmallChat)
				{
					this.AssignBackgroundTexure(5);
					__ChatWindow.btnFactionChat.SetTextureNormal("chat", string.Concat(__ChatWindow.factionChatAssetName, "nml"));
					this.boundries.set_x((float)Screen.get_width() - this.boundries.get_width());
					this.boundries.set_y((float)Screen.get_height() - this.boundries.get_height() - 25f);
					this.boundries.set_height(this.boundries.get_height() + 25f);
				}
				__ChatWindow.activeChat = __ChatWindow.factionChat;
				__ChatWindow.factionChat.hasUnreadMessages = false;
				__ChatWindow.lastChatChecked = __ChatWindow.factionChat;
				break;
			}
			case 4:
			{
				if (!this.isRussianServer && !__ChatWindow.isSmallChat)
				{
					this.AssignBackgroundTexure(6);
					__ChatWindow.btnLanguageChat.SetTextureNormal("chat", "btnLanguage_Nml");
					this.boundries.set_x((float)Screen.get_width() - this.boundries.get_width());
					this.boundries.set_y((float)Screen.get_height() - this.boundries.get_height() - 25f);
					this.boundries.set_height(this.boundries.get_height() + 25f);
				}
				__ChatWindow.activeChat = __ChatWindow.languageChat;
				__ChatWindow.languageChat.hasUnreadMessages = false;
				__ChatWindow.lastChatChecked = __ChatWindow.languageChat;
				break;
			}
			case 5:
			{
				if (!__ChatWindow.isSmallChat)
				{
					this.AssignBackgroundTexure(4);
					__ChatWindow.btnPrivateChat.SetTextureNormal("chat", "btnPrivateNml");
					this.boundries.set_x((float)Screen.get_width() - this.boundries.get_width());
					this.boundries.set_y((float)Screen.get_height() - this.boundries.get_height() - 25f);
					this.boundries.set_height(this.boundries.get_height() + 25f);
				}
				__ChatWindow.activeChat = __ChatWindow.privateChat;
				__ChatWindow.privateChat.hasUnreadMessages = false;
				__ChatWindow.lastChatChecked = __ChatWindow.privateChat;
				break;
			}
		}
		this.PopulateChatRoom(__ChatWindow.activeChat);
		this.InitReceiverWeb();
		this.ManageChangeFractionButton();
		__ChatWindow.wnd.boundries.set_y((float)Screen.get_height() - __ChatWindow.wnd.boundries.get_height() + 2f);
	}

	private void TextBoxSend(object obj)
	{
		if (this.tbSendMessage.text.get_Length() > 0 && this.tbSendMessage.text != new string(' ', this.tbSendMessage.text.get_Length()) && this.tbSendMessage.text != " " && __ChatWindow.isOnFocus)
		{
			this.OnSendClicked(null);
		}
		AndromedaGui.gui.RequestFocusOnControl(string.Empty);
		__ChatWindow.isOnFocus = false;
	}

	private void UpdateBlinkButtons()
	{
		if (__ChatWindow.privateChat.hasUnreadMessages && __ChatWindow.isSmallChat)
		{
			this.fxSmallChat.Update();
		}
	}

	private void UpdateReceiverListWhenOffline(string playerNickName)
	{
		if (__ChatWindow.receiversList.Contains(playerNickName))
		{
			__ChatWindow.receiversList.Remove(playerNickName);
			this.InitReceiverWeb();
			if (__ChatWindow.receiversList.get_Count() == 0)
			{
				base.RemoveGuiElement(this.btnDropDownList);
				if (this.btnDropDownList != null)
				{
					this.btnDropDownList.isActive = false;
				}
				if (this.receiversScollerContainer != null)
				{
					base.RemoveGuiElement(this.receiversScollerContainer);
					this.receiversScollerContainer = null;
				}
			}
		}
	}

	private void ValideteInput()
	{
		if (this.tbSendMessage.text.get_Length() > 100)
		{
			this.tbSendMessage.text = this.tbSendMessage.text.Substring(0, 100);
		}
	}
}