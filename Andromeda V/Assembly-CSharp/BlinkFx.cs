using System;

public class BlinkFx
{
	public string btnTextureNormalName;

	public string btnTextureBlinkedName;

	public DateTime nextSwitchTime;

	public bool isBlinkedNow;

	public GuiTexture tx;

	public GuiButton btn;

	public BlinkFx()
	{
	}

	public void Update()
	{
		if (this.nextSwitchTime <= DateTime.get_Now())
		{
			this.isBlinkedNow = !this.isBlinkedNow;
			this.nextSwitchTime = DateTime.get_Now().AddMilliseconds(400);
			if (!this.isBlinkedNow)
			{
				if (this.tx != null)
				{
					this.tx.SetTexture("chat", this.btnTextureNormalName);
				}
				if (this.btn != null && this.btnTextureNormalName != null && this.btn is GuiButtonFixed)
				{
					((GuiButtonFixed)this.btn).SetTextureNormal("chat", this.btnTextureNormalName);
				}
			}
			else
			{
				if (this.tx != null)
				{
					this.tx.SetTexture("chat", this.btnTextureBlinkedName);
				}
				if (this.btn != null && this.btnTextureBlinkedName != null && this.btn is GuiButtonFixed)
				{
					((GuiButtonFixed)this.btn).SetTextureNormal("chat", this.btnTextureBlinkedName);
				}
			}
		}
	}
}