using System;
using UnityEngine;

public class MovieEventPlayAnimation : MovieEvent
{
	public string gameObjectName;

	public string animationName;

	public Action<MovieEventPlayAnimation> PlayAnimationCallback;

	public MovieEventPlayAnimation()
	{
	}

	public override void Execute()
	{
		GameObject gameObject = GameObject.Find(this.gameObjectName);
		gameObject.GetComponent<Animation>().Play(this.animationName);
		if (this.PlayAnimationCallback != null)
		{
			this.PlayAnimationCallback.Invoke(this);
		}
	}
}