using System;
using UnityEngine;

public class TrackingPoPScript : MonoBehaviour
{
	public PlayerObjectPhysics target;

	public float timeToDestroy = 2f;

	public float deltaY = 1.5f;

	public bool stayAliveOnPopDestroy;

	public TrackingPoPScript()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
		TrackingPoPScript _deltaTime = this;
		_deltaTime.timeToDestroy = _deltaTime.timeToDestroy - Time.get_deltaTime();
		if (this.stayAliveOnPopDestroy)
		{
			if (this.timeToDestroy < 0f)
			{
				Object.Destroy(base.get_gameObject());
			}
			if (this.target != null)
			{
				base.get_gameObject().get_transform().set_position(new Vector3(this.target.x, this.deltaY, this.target.z));
			}
		}
		else if (this.target == null || this.target.isRemoved || !this.target.isAlive || this.timeToDestroy < 0f)
		{
			Object.Destroy(base.get_gameObject());
		}
		else
		{
			base.get_gameObject().get_transform().set_position(new Vector3(this.target.x, this.deltaY, this.target.z));
		}
	}
}