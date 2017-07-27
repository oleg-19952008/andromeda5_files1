using System;
using UnityEngine;

public class GuiAnimatedText : GuiLabel
{
	private float duration;

	private float delay = 0.05f;

	private string originalText;

	private string displayedText;

	public DateTime activationTime;

	private TimeSpan remainingTime;

	private int characterNumber;

	private float delta;

	private AudioClip soundClip;

	private Action OnCompleteAction;

	private float charPerSec = 25f;

	private bool isLastPhaseStarted;

	public float Speed
	{
		get
		{
			return this.charPerSec;
		}
		set
		{
			this.charPerSec = value;
		}
	}

	public GuiAnimatedText(string holeText, Action action)
	{
		this.originalText = holeText;
		this.activationTime = DateTime.get_Now();
		this.characterNumber = this.originalText.get_Length();
		this.duration = (float)this.characterNumber / this.charPerSec;
		this.delta = (float)this.characterNumber / this.duration;
		this.OnCompleteAction = action;
	}

	public override void DrawGuiElement()
	{
		if (this.activationTime.AddSeconds((double)this.duration) > DateTime.get_Now())
		{
			this.Update();
		}
		else if (this.displayedText != this.originalText)
		{
			this.text = this.originalText;
			this.displayedText = this.text;
		}
		if (!this.isLastPhaseStarted && this.OnCompleteAction != null && this.activationTime.AddSeconds((double)(this.delay + this.duration)) < DateTime.get_Now())
		{
			this.isLastPhaseStarted = true;
			this.OnCompleteAction.Invoke();
		}
		base.DrawGuiElement();
	}

	public void SetText(string newText)
	{
		this.originalText = newText;
		this.activationTime = DateTime.get_Now();
		this.characterNumber = this.originalText.get_Length();
		this.duration = (float)this.characterNumber / this.charPerSec;
		this.delta = (float)this.characterNumber / this.duration;
		this.displayedText = string.Empty;
	}

	public void ShowAll(object prm)
	{
		this.text = this.originalText;
		this.displayedText = this.text;
		this.duration = 0f;
		if (this.OnCompleteAction != null)
		{
			this.OnCompleteAction.Invoke();
		}
	}

	private void Update()
	{
		if (this.displayedText != this.originalText)
		{
			this.remainingTime = DateTime.get_Now() - this.activationTime;
			this.text = this.originalText.Substring(0, (int)Math.Min(this.remainingTime.get_TotalSeconds() * (double)this.delta, (double)this.originalText.get_Length()));
			this.displayedText = this.text;
		}
	}
}