using System;
using UnityEngine;

public class TargetLockScr : MonoBehaviour
{
	public GameObject target;

	private float nextTime;

	public TargetLockScr()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (this.nextTime < Time.get_time())
		{
			this.nextTime = Time.get_time() + 0.05f;
		}
	}
}