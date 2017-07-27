using System;

public class GuiTimer : GuiLabel
{
	public DateTime activationTime;

	private TimeSpan deltaTime;

	public GuiTimer(int deltaSec)
	{
		this.activationTime = StaticData.now.AddSeconds((double)(-deltaSec));
	}

	public override void DrawGuiElement()
	{
		this.Update();
		base.DrawGuiElement();
	}

	public void SetDeltaTime(int deltaSec)
	{
		this.activationTime = StaticData.now.AddSeconds((double)(-deltaSec));
	}

	private void Update()
	{
		this.deltaTime = StaticData.now - this.activationTime;
		this.text = string.Format(StaticData.Translate("key_guitimer_lbl"), this.deltaTime.get_Minutes(), this.deltaTime.get_Seconds());
	}
}