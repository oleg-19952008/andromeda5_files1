using System;

public class GameMessage
{
	public int id;

	public GameMessageType type;

	public string senderName;

	public string title;

	public string text;

	public string link;

	public bool isNew;

	public DateTime reciveTime;

	public GameMessage()
	{
	}
}