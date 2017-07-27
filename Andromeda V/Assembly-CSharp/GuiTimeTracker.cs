using System;
using UnityEngine;

public class GuiTimeTracker : GuiLabel
{
	private GuiWindow window;

	private DateTime startTime;

	private TimeSpan remainingTime;

	private int duration;

	private Action onTimesEndAction;

	private GuiScrollingContainer scroller;

	public GuiTimeTracker(int dur, GuiWindow wnd)
	{
		this.startTime = DateTime.get_Now();
		this.duration = dur;
		this.window = wnd;
		this.window.AddGuiElement(this);
	}

	public GuiTimeTracker(int dur, GuiScrollingContainer scr, Rect pos)
	{
		this.startTime = DateTime.get_Now();
		this.duration = dur;
		this.scroller = scr;
		this.boundries = pos;
		scr.AddContent(this);
	}

	public override void DrawGuiElement()
	{
		if (this.startTime.AddSeconds((double)this.duration) > DateTime.get_Now())
		{
			this.Update();
		}
		base.DrawGuiElement();
		if (this.startTime.AddSeconds((double)this.duration) < DateTime.get_Now())
		{
			if (this.onTimesEndAction != null)
			{
				this.onTimesEndAction.Invoke();
				this.onTimesEndAction = null;
			}
			if (this.window != null)
			{
				this.window.RemoveGuiElement(this);
			}
			else if (this.scroller != null)
			{
				this.scroller.RemoveContent(this);
			}
		}
	}

	public void SetEndAction(Action callback)
	{
		this.onTimesEndAction = callback;
	}

	private void Update()
	{
		this.remainingTime = this.startTime.AddSeconds((double)this.duration) - DateTime.get_Now();
		int totalSeconds = (int)this.remainingTime.get_TotalSeconds();
		if (totalSeconds <= 3600)
		{
			this.text = string.Format("{0:#00}:{1:#00}", totalSeconds / 60, totalSeconds % 60);
		}
		else
		{
			this.text = string.Format("{0}:{1:#00}:{2:#00}", totalSeconds / 3600, totalSeconds % 3600 / 60, totalSeconds % 60);
		}
	}
}