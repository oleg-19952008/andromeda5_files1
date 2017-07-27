using System;

public class EventHandlerParam
{
	public EventHandlerParam.MouseButton button;

	public object customData;

	public object customData2;

	public EventHandlerParam()
	{
	}

	public enum MouseButton
	{
		Left,
		Right
	}
}