using System;
using UnityEngine;

public class HyperJumpEntrance : MonoBehaviour
{
	public GameObject hyperJump;

	public GameObject ship;

	private DateTime startTime;

	private float prevScale = 1f;

	public HyperJumpEntrance()
	{
	}

	private void Start()
	{
		this.startTime = DateTime.get_Now();
	}

	private void Update()
	{
		if (this.hyperJump == null || this.ship == null)
		{
			return;
		}
		if (this.startTime.AddMilliseconds(1000) < DateTime.get_Now())
		{
			Object.Destroy(base.get_gameObject());
			return;
		}
		base.get_gameObject().get_transform().set_position(this.ship.get_transform().get_position());
		float _position = this.hyperJump.get_transform().get_position().z;
		Vector3 vector3 = this.ship.get_transform().get_position();
		float single = _position - vector3.z;
		float _position1 = this.hyperJump.get_transform().get_position().x;
		Vector3 vector31 = this.ship.get_transform().get_position();
		float single1 = _position1 - vector31.x;
		float single2 = Mathf.Atan2(single1, single);
		single2 = single2 * 57.2957726f + 180f;
		base.get_gameObject().get_transform().set_eulerAngles(new Vector3(0f, single2, 0f));
		float single3 = 5f;
		float single4 = Vector3.Distance(this.ship.get_transform().get_position(), this.hyperJump.get_transform().get_position());
		float single5 = single4 / single3;
		Transform _transform = base.get_gameObject().get_transform();
		_transform.set_localScale(_transform.get_localScale() + new Vector3(0f, 0f, single5 - this.prevScale));
		this.prevScale = single5;
	}
}