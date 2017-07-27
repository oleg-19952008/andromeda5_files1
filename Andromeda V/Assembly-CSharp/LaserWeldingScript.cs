using System;
using UnityEngine;

public class LaserWeldingScript : MonoBehaviour
{
	public GameObjectPhysics target;

	public GameObjectPhysics shooter;

	public bool isStarted;

	public ProjectileObject physics;

	public LineRenderer theLaser;

	public LaserWeldingScript()
	{
	}

	public void RemoveTheGameObject(ProjectileObject obj)
	{
		Object.Destroy(base.get_gameObject());
	}

	private void Start()
	{
		LineRenderer component = base.get_gameObject().GetComponent<LineRenderer>();
		this.theLaser = component;
		component.SetPosition(1, ((GameObject)this.target.gameObject).get_transform().get_position());
		component.SetPosition(0, ((GameObject)this.shooter.gameObject).get_transform().get_position());
	}

	private void Update()
	{
		if (!this.isStarted)
		{
			return;
		}
		if (this.target == null || this.shooter == null || this.target.isRemoved || this.shooter.isRemoved)
		{
			return;
		}
		if (this.target.gameObject == null || this.shooter.gameObject == null)
		{
			return;
		}
		float _deltaTime = Time.get_deltaTime();
		float single = 0f;
		float single1 = 0f;
		float single2 = 0f;
		this.theLaser.SetPosition(1, ((GameObject)this.target.gameObject).get_transform().get_position());
		this.theLaser.SetPosition(0, ((GameObject)this.shooter.gameObject).get_transform().get_position());
		this.physics.CalculateObjectMovement(_deltaTime, ref single, ref single1, ref single2);
	}
}