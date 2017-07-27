using System;
using UnityEngine;

public class ShipShieldScript : MonoBehaviour
{
	public GameObjectPhysics target;

	private DateTime startTime;

	public ShipShieldScript()
	{
	}

	private void Start()
	{
		if (this.target == null)
		{
			Debug.Log("ShipShieldScript.target = null");
			return;
		}
		this.startTime = DateTime.get_Now();
	}

	private void Update()
	{
		if (this.target == null || this.target.isRemoved || this.startTime.AddMilliseconds(300) < DateTime.get_Now() || this.target.get_IsPoP() && !((PlayerObjectPhysics)this.target).isAlive)
		{
			Object.Destroy(base.get_gameObject());
		}
		else
		{
			base.get_gameObject().get_transform().set_position(new Vector3(this.target.x, this.target.y + 1f, this.target.z));
		}
	}
}