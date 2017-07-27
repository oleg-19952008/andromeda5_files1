using System;
using UnityEngine;

public class GuidingArrowScript : MonoBehaviour
{
	public GameObject me;

	public GameObject targetObject;

	public Vector3 targetLocation;

	public bool isLocationSet;

	private Material material;

	private Color color;

	private float scale;

	private float range = 4.5f;

	public GuidingArrowScript()
	{
	}

	public void SetRange(float r)
	{
		this.range = r;
	}

	private void Start()
	{
		this.material = base.get_gameObject().get_renderer().get_material();
		this.color = this.material.get_color();
	}

	private void Update()
	{
		if (this.me == null || this.targetObject == null && !this.isLocationSet)
		{
			Object.Destroy(base.get_gameObject());
			return;
		}
		base.get_gameObject().get_transform().set_position(this.me.get_transform().get_position());
		float single = 0f;
		float single1 = 0f;
		if (this.targetObject == null)
		{
			float single2 = this.targetLocation.z;
			Vector3 _position = this.me.get_transform().get_position();
			single = single2 - _position.z;
			float single3 = this.targetLocation.x;
			Vector3 vector3 = this.me.get_transform().get_position();
			single1 = single3 - vector3.x;
		}
		else
		{
			float _position1 = this.targetObject.get_transform().get_position().z;
			Vector3 vector31 = this.me.get_transform().get_position();
			single = _position1 - vector31.z;
			float _position2 = this.targetObject.get_transform().get_position().x;
			Vector3 vector32 = this.me.get_transform().get_position();
			single1 = _position2 - vector32.x;
		}
		float single4 = Mathf.Atan2(single1, single);
		single4 = single4 * 57.2957726f + 90f;
		base.get_gameObject().get_transform().set_eulerAngles(new Vector3(-90f, single4, 0f));
		float single5 = 0f;
		single5 = (this.targetObject == null ? Vector3.Distance(this.me.get_transform().get_position(), this.targetLocation) : Vector3.Distance(this.me.get_transform().get_position(), this.targetObject.get_transform().get_position()));
		if (single5 <= this.range)
		{
			this.scale = 0f;
		}
		else
		{
			this.scale = (single5 - this.range) / 5f;
		}
		this.material.set_color(new Color(this.color.r, this.color.g, this.color.b, this.scale));
	}
}