using System;
using UnityEngine;

public class ActiveSkillMovementScript : MonoBehaviour
{
	public bool isStarted;

	public PlayerObjectPhysics caster;

	public GameObjectPhysics target;

	public ActiveSkillObject physics;

	public ActiveSkillMovementScript()
	{
	}

	public void RemoveTheGameObject(ProjectileObject obj)
	{
		Object.Destroy(base.get_gameObject());
	}

	private void Start()
	{
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
		ActiveSkillObject activeSkillObject = this.physics;
		activeSkillObject.x = activeSkillObject.x + single;
		ActiveSkillObject activeSkillObject1 = this.physics;
		activeSkillObject1.y = activeSkillObject1.y + single1;
		ActiveSkillObject activeSkillObject2 = this.physics;
		activeSkillObject2.z = activeSkillObject2.z + single2;
		NetworkScript.ApplyPhysicsToGameObject(this.physics, base.get_gameObject());
	}
}