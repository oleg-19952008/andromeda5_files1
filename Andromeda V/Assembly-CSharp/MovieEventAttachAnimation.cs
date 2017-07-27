using System;
using UnityEngine;

public class MovieEventAttachAnimation : MovieEvent
{
	public AnimationClip clip;

	public string gameObjectName;

	public string clipName;

	public MovieEventAttachAnimation()
	{
	}

	public override void Execute()
	{
		GameObject gameObject = GameObject.Find(this.gameObjectName);
		gameObject.get_animation().AddClip(this.clip, this.clipName);
		gameObject.get_animation().Play(this.clipName);
	}
}