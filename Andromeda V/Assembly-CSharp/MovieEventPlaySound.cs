using System;
using UnityEngine;

public class MovieEventPlaySound : MovieEvent
{
	public AudioClip clip;

	public Vector3 position;

	public MovieEventPlaySound()
	{
	}

	public override void Execute()
	{
		AudioSource.PlayClipAtPoint(this.clip, this.position);
	}
}