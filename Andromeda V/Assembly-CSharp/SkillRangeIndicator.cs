using System;
using UnityEngine;

public class SkillRangeIndicator : MonoBehaviour
{
	public PlayerObjectPhysics target;

	public float lifeTime = 1f;

	private float deltaTime;

	public SkillRangeIndicator()
	{
	}

	private void Start()
	{
		if (this.target == null)
		{
			return;
		}
		this.deltaTime = this.lifeTime;
	}

	private void Update()
	{
		SkillRangeIndicator _deltaTime = this;
		_deltaTime.deltaTime = _deltaTime.deltaTime - Time.get_deltaTime();
		if (this.deltaTime > 0f)
		{
			if (this.target == null || this.target.isRemoved || !this.target.isAlive)
			{
				Object.Destroy(base.get_gameObject());
			}
			base.get_gameObject().get_transform().set_position(new Vector3(this.target.x, 0f, this.target.z));
		}
		else
		{
			Object.Destroy(base.get_gameObject());
		}
	}
}