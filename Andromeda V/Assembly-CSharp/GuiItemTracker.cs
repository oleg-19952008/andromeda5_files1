using System;
using UnityEngine;

public class GuiItemTracker : GuiLabel
{
	public ushort itemForTracking;

	public double timeForAnimation;

	public double timeOfVal;

	public double timeOfColor;

	public bool muteSoundForNextChange;

	private byte blinkingCount;

	private bool isBlinkingON = true;

	private bool isShakeON = true;

	private long oldValue;

	private long startValue;

	private DateTime startEffectTime;

	private bool isColorsSet;

	private Color[] positiveColors;

	private Color[] negativeColors;

	private Color originalColor;

	public bool Animatio
	{
		set
		{
			this.isShakeON = value;
		}
	}

	public bool Blining
	{
		set
		{
			this.isBlinkingON = value;
		}
	}

	public byte BlinkingCount
	{
		set
		{
			this.blinkingCount = value;
		}
	}

	public Color SetColor
	{
		set
		{
			this.originalColor = value;
		}
	}

	public GuiItemTracker(ushort itemIndex)
	{
		this.itemForTracking = itemIndex;
		this.oldValue = NetworkScript.player.cfg.playerItems.GetAmountAt(this.itemForTracking);
		this.timeForAnimation = 2;
		this.blinkingCount = 3;
		this.originalColor = Color.get_white();
		this.text = this.oldValue.ToString("##,##0");
	}

	public override void DrawGuiElement()
	{
		if (NetworkScript.player.cfg.playerItems.GetAmountAt(this.itemForTracking) != this.oldValue)
		{
			this.StartEffect(this.oldValue, NetworkScript.player.cfg.playerItems.GetAmountAt(this.itemForTracking));
		}
		this.Update();
		base.DrawGuiElement();
	}

	private void InitCashBlinkers()
	{
		this.positiveColors = new Color[100];
		for (int i = 0; i < (int)this.positiveColors.Length; i++)
		{
			Color _green = Color.get_green();
			if (i % 2 == 0)
			{
				_green = this.originalColor;
			}
			this.positiveColors[i] = _green;
		}
		this.negativeColors = new Color[100];
		for (int j = 0; j < (int)this.negativeColors.Length; j++)
		{
			Color _red = Color.get_red();
			if (j % 2 == 0)
			{
				_red = this.originalColor;
			}
			this.negativeColors[j] = _red;
		}
		this.isColorsSet = true;
	}

	private void StartEffect(long oldV, long newV)
	{
		if (this.muteSoundForNextChange)
		{
			this.muteSoundForNextChange = false;
		}
		else if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
		{
			AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "coinsSound");
			AudioManager.PlayGUISound(fromStaticSet);
		}
		if (this.isBlinkingON)
		{
			if (!this.isColorsSet)
			{
				this.InitCashBlinkers();
			}
			this.timeOfColor = (double)(this.blinkingCount * 2) / this.timeForAnimation;
		}
		if (this.isShakeON)
		{
			this.timeOfVal = (double)Math.Abs(oldV - newV) / this.timeForAnimation;
		}
		this.startValue = oldV;
		this.oldValue = newV;
		this.startEffectTime = DateTime.get_Now();
	}

	private void Update()
	{
		if (DateTime.get_Now() >= this.startEffectTime.AddSeconds(this.timeForAnimation))
		{
			long amountAt = NetworkScript.player.cfg.playerItems.GetAmountAt(this.itemForTracking);
			this.text = amountAt.ToString("##,##0");
			base.TextColor = this.originalColor;
		}
		else
		{
			TimeSpan now = DateTime.get_Now() - this.startEffectTime;
			double totalSeconds = now.get_TotalSeconds();
			if (this.startValue <= NetworkScript.player.cfg.playerItems.GetAmountAt(this.itemForTracking))
			{
				if (this.isBlinkingON)
				{
					base.TextColor = this.positiveColors[(int)(totalSeconds * this.timeOfColor % 100)];
				}
				long num = (long)((double)this.startValue + totalSeconds * this.timeOfVal);
				this.text = num.ToString("##,##0");
			}
			else
			{
				if (this.isBlinkingON)
				{
					base.TextColor = this.negativeColors[(int)(totalSeconds * this.timeOfColor % 100)];
				}
				long num1 = (long)((double)this.startValue - totalSeconds * this.timeOfVal);
				this.text = num1.ToString("##,##0");
			}
		}
	}
}