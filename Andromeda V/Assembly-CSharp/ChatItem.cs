using System;
using UnityEngine;

public class ChatItem
{
	public const int MAX_CHAT_MESSAGES = 100;

	public const float minY = 24f;

	public const float maxY = 230f;

	public const float maxX = 286f;

	public const float minX = 12f;

	public const float spaceWidth = 2.9f;

	public const float maxWidth = 274f;

	public string text;

	public DateTime time;

	public float height;

	public float timeHeight;

	public Color colorName;

	public Color colorText;

	public bool canBeChated;

	public string playerName;

	public long playerId;

	public string receiverName;

	public long recieverID;

	public float y;

	public GuiLabel lblName;

	public GuiLabel lblText;

	public GuiButton btnPrivateChat;

	public bool isSpam;

	public bool isDuplicate;

	public bool sendByAdmin;

	public bool isSystemMessage;

	public byte senderFactionId;

	public bool isCustomMessage;

	public ChatItem()
	{
	}
}