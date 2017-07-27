using System;
using UnityEngine;

public class SetInactiveScript : MonoBehaviour
{
	public float lifeTime;

	private float deltaTime;

	public SetInactiveScript()
	{
	}

	private void Start()
	{
		this.deltaTime = this.lifeTime;
	}

	private void Update()
	{
		SetInactiveScript _deltaTime = this;
		_deltaTime.deltaTime = _deltaTime.deltaTime - Time.get_deltaTime();
		if (this.deltaTime < 0f)
		{
			Object.Destroy(base.get_gameObject());
		}
	}
}