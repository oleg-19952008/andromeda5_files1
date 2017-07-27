using System;

public abstract class MovieEvent
{
	public float time;

	protected MovieEvent()
	{
	}

	public abstract void Execute();
}