using System;
using UnityEngine;

public class AnimationAutoLoop : MonoBehaviour
{
	public AnimationAutoLoop()
	{
	}

	private void Start()
	{
		base.get_animation().set_wrapMode(2);
	}

	private void Update()
	{
	}
}