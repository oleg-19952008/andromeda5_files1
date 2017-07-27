using System;
using UnityEngine;

public class DefenceTurretScript : MonoBehaviour
{
	public DefenceTurret turret;

	public DefenceTurretScript()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (this.turret == null || this.turret.isRemoved)
		{
			return;
		}
		float _deltaTime = Time.get_deltaTime();
		float single = 0f;
		float single1 = 0f;
		float single2 = 0f;
		this.turret.CalculateObjectMovement(_deltaTime, ref single, ref single1, ref single2);
		DefenceTurret defenceTurret = this.turret;
		defenceTurret.x = defenceTurret.x + single;
		DefenceTurret defenceTurret1 = this.turret;
		defenceTurret1.y = defenceTurret1.y + single1;
		DefenceTurret defenceTurret2 = this.turret;
		defenceTurret2.z = defenceTurret2.z + single2;
		NetworkScript.ApplyTurretPhysicsToGameObject(this.turret, base.get_gameObject());
	}
}