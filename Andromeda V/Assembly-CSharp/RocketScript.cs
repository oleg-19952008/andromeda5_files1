using System;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
	public bool isStarted;

	public GameObjectPhysics target;

	public ProjectileObject physics;

	public RocketScript()
	{
	}

	public void RemoveTheGameObject(ProjectileObject obj)
	{
		Object.Destroy(base.get_gameObject());
	}

	private void Start()
	{
		AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "fire_ion");
		AudioManager.PlayAudioClip(fromStaticSet, base.get_transform().get_position());
		base.get_gameObject().get_transform().LookAt(new Vector3(this.target.x, this.target.y, this.target.z));
	}

	private void Update()
	{
		if (!this.isStarted)
		{
			return;
		}
		float _deltaTime = Time.get_deltaTime();
		base.get_gameObject().get_transform().LookAt(new Vector3(this.target.x, this.target.y, this.target.z));
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