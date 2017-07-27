using System;
using System.Collections.Generic;

public class ChatItems
{
	public List<ChatItem> items;

	public List<ChatItem> onScreen;

	public float y;

	public float height;

	public long playerId;

	public string playerName;

	public ChatType chatType;

	public string receiverName;

	public long recieverID;

	public bool callToInsertToChatRoom;

	public bool hasUnreadMessages;

	public bool firstLineWithScroller;

	public ChatItems()
	{
	}
}