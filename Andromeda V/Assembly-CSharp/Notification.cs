using System;

public class Notification
{
	public string title;

	public string text;

	public string assetName;

	public byte level;

	public GuiWindow theWindow;

	public DateTime requestTime;

	public DateTime startTime;

	public Action<EventHandlerParam> onClickAction;

	public byte notificationType;

	public ushort itemType;

	public int amount;

	public NewQuestObjective questObjective;

	public Notification()
	{
	}
}