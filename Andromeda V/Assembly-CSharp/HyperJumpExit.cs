using System;
using UnityEngine;

public class HyperJumpExit : MonoBehaviour
{
	private DateTime startTime;

	public HyperJumpExit()
	{
	}

	private void Start()
	{
		this.startTime = DateTime.get_Now();
	}

	private void Update()
	{
		if (this.startTime.AddMilliseconds(1000) < DateTime.get_Now())
		{
			Object.Destroy(base.get_gameObject());
		}
	}
}