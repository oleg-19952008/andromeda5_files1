using System;

public class MovieEventCustom : MovieEvent
{
	public object parameter;

	public Action<object> Callback;

	public MovieEventCustom()
	{
	}

	public override void Execute()
	{
		this.Callback.Invoke(this.parameter);
	}
}