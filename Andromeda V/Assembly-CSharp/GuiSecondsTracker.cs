using System;

public class GuiSecondsTracker : GuiLabel
{
	private GuiWindow window;

	private DateTime startTime;

	private TimeSpan remainingTime;

	private int duration;

	private Action onTimesEndAction;

	private string custumText;

	public GuiSecondsTracker(int dur, GuiWindow wnd)
	{
		this.startTime = DateTime.get_Now();
		this.duration = dur;
		this.window = wnd;
		this.window.AddGuiElement(this);
	}

	public GuiSecondsTracker(string text, int durationInMiliseconds, GuiWindow wnd)
	{
		this.startTime = DateTime.get_Now();
		this.duration = durationInMiliseconds;
		this.window = wnd;
		this.window.AddGuiElement(this);
		this.custumText = text;
	}

	public override void DrawGuiElement()
	{
		if (this.startTime.AddMilliseconds((double)this.duration) > DateTime.get_Now())
		{
			this.Update();
		}
		base.DrawGuiElement();
		if (this.startTime.AddMilliseconds((double)this.duration) < DateTime.get_Now())
		{
			if (this.onTimesEndAction != null)
			{
				this.onTimesEndAction.Invoke();
			}
			this.window.RemoveGuiElement(this);
		}
	}

	public void SetEndAction(Action callback)
	{
		this.onTimesEndAction = callback;
	}

	private void Update()
	{
		this.remainingTime = this.startTime.AddMilliseconds((double)this.duration) - DateTime.get_Now();
		if (!string.IsNullOrEmpty(this.custumText))
		{
			this.text = string.Format(this.custumText, this.remainingTime.get_TotalSeconds());
		}
		else
		{
			double totalSeconds = this.remainingTime.get_TotalSeconds();
			this.text = totalSeconds.ToString("##0");
		}
	}
}