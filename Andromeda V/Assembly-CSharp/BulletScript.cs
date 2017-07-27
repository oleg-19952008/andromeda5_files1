using System;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
	public LineRenderer theLaser;

	private float laserStartTime;

	public bool isLaser;

	public bool isStarted;

	public GameObjectPhysics shooter;

	public GameObjectPhysics target;

	public long shootTime;

	public WeaponInfo wi;

	public int ammoSlot;

	public Vector3 shootSlotOffset;

	public ProjectileObject physics;

	private bool isFirstUpdate = true;

	public BulletScript()
	{
	}

	private void Start()
	{
		string empty = string.Empty;
		if (base.get_gameObject().get_name().Contains("WeapIon"))
		{
			empty = "fire_ion";
		}
		else if (base.get_gameObject().get_name().Contains("WeapLaser"))
		{
			empty = "fire_laser";
		}
		else if (base.get_gameObject().get_name().Contains("WeapPlasma"))
		{
			empty = "fire_plasma";
		}
		if (!string.IsNullOrEmpty(empty))
		{
			AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", empty);
			AudioManager.PlayAudioClip(fromStaticSet, base.get_transform().get_position());
		}
	}

	private void Update()
	{
		if (this.isFirstUpdate && this.physics is LaserMovingObject)
		{
			this.isFirstUpdate = false;
			base.get_gameObject().get_transform().LookAt(new Vector3(this.target.x, this.target.y, this.target.z));
		}
		if (!this.isStarted)
		{
			return;
		}
		float _deltaTime = Time.get_deltaTime();
		float single = 0f;
		float single1 = 0f;
		float single2 = 0f;
		this.physics.CalculateObjectMovement(_deltaTime, ref single, ref single1, ref single2);
		ProjectileObject projectileObject = this.physics;
		projectileObject.x = projectileObject.x + single;
		ProjectileObject projectileObject1 = this.physics;
		projectileObject1.y = projectileObject1.y + single1;
		ProjectileObject projectileObject2 = this.physics;
		projectileObject2.z = projectileObject2.z + single2;
		NetworkScript.ApplyPhysicsToGameObject(this.physics, base.get_gameObject());
	}
}