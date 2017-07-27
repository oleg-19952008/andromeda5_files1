using System;
using UnityEngine;

public class StationScript : MonoBehaviour
{
	public StarBaseNet starBase;

	public StationScript()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (this.starBase == null)
		{
			return;
		}
		if (this.starBase.isRemoved)
		{
			return;
		}
		float _deltaTime = Time.get_deltaTime();
		float single = 0f;
		float single1 = 0f;
		float single2 = 0f;
		this.starBase.CalculateObjectMovement(_deltaTime, ref single, ref single1, ref single2);
		StarBaseNet starBaseNet = this.starBase;
		starBaseNet.x = starBaseNet.x + single;
		StarBaseNet starBaseNet1 = this.starBase;
		starBaseNet1.y = starBaseNet1.y + single1;
		StarBaseNet starBaseNet2 = this.starBase;
		starBaseNet2.z = starBaseNet2.z + single2;
		NetworkScript.ApplyPhysicsToGameObject(this.starBase, base.get_gameObject());
	}
}