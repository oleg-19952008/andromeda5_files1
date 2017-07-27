using System;
using UnityEngine;

public class PartyArrowScript : MonoBehaviour
{
	public GameObject me;

	public GameObject target;

	public PartyMemberInfo memberInfo;

	private Material material;

	private Color color;

	private float scale;

	private float range = 15f;

	public PartyArrowScript()
	{
	}

	private void Start()
	{
		this.material = base.get_gameObject().get_renderer().get_material();
		this.color = this.material.get_color();
	}

	private void Update()
	{
		if (this.me == null || this.target == null && (this.memberInfo == null || this.memberInfo.lastUpdateTime.AddMilliseconds(1500) < StaticData.now || this.memberInfo.galaxyId != NetworkScript.player.vessel.galaxy.__galaxyId))
		{
			Object.Destroy(base.get_gameObject());
			return;
		}
		base.get_gameObject().get_transform().set_position(this.me.get_transform().get_position());
		float single = 0f;
		float single1 = 0f;
		float single2 = Mathf.Atan2(0f, 0f);
		float single3 = 0f;
		if (this.target != null)
		{
			float _position = this.target.get_transform().get_position().z;
			Vector3 vector3 = this.me.get_transform().get_position();
			single = _position - vector3.z;
			float _position1 = this.target.get_transform().get_position().x;
			Vector3 vector31 = this.me.get_transform().get_position();
			single1 = _position1 - vector31.x;
			single3 = Vector3.Distance(this.me.get_transform().get_position(), this.target.get_transform().get_position());
		}
		else if (this.memberInfo != null && this.memberInfo.lastUpdateTime.AddMilliseconds(1500) > StaticData.now)
		{
			float single4 = this.memberInfo.coordinateZ;
			Vector3 _position2 = this.me.get_transform().get_position();
			single = single4 - _position2.z;
			float single5 = this.memberInfo.coordinateX;
			Vector3 vector32 = this.me.get_transform().get_position();
			single1 = single5 - vector32.x;
			single3 = Vector3.Distance(this.me.get_transform().get_position(), new Vector3(this.memberInfo.coordinateX, 0f, this.memberInfo.coordinateZ));
		}
		single2 = Mathf.Atan2(single1, single);
		single2 = single2 * 57.2957726f + 90f;
		base.get_gameObject().get_transform().set_eulerAngles(new Vector3(-90f, single2, 0f));
		if (single3 <= this.range)
		{
			this.scale = 0f;
		}
		else
		{
			this.scale = (single3 - this.range) / 5f;
		}
		this.material.set_color(new Color(this.color.r, this.color.g, this.color.b, this.scale));
	}
}