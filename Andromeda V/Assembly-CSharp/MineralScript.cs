using System;
using UnityEngine;

public class MineralScript : MonoBehaviour
{
	public MineralEx mineral;

	private Rigidbody myRigidbody;

	public MineralScript()
	{
	}

	private void Start()
	{
		this.myRigidbody = base.get_rigidbody();
	}

	private void Update()
	{
		if (this.mineral == null)
		{
			return;
		}
		if (this.mineral.isRemoved)
		{
			return;
		}
		float _deltaTime = Time.get_deltaTime();
		float single = 0f;
		float single1 = 0f;
		float single2 = 0f;
		this.mineral.CalculateObjectMovement(_deltaTime, ref single, ref single1, ref single2);
		MineralEx mineralEx = this.mineral;
		mineralEx.x = mineralEx.x + single;
		MineralEx mineralEx1 = this.mineral;
		mineralEx1.y = mineralEx1.y + single1;
		MineralEx mineralEx2 = this.mineral;
		mineralEx2.z = mineralEx2.z + single2;
		this.myRigidbody.set_position(new Vector3(this.mineral.x, 0f, this.mineral.z));
		Rigidbody rigidbody = this.myRigidbody;
		Quaternion _rotation = this.myRigidbody.get_rotation();
		rigidbody.set_rotation(Quaternion.Euler(_rotation.get_eulerAngles() + new Vector3(this.mineral.rotationSpeedX * _deltaTime, this.mineral.rotationSpeedY * _deltaTime, this.mineral.rotationSpeedZ * _deltaTime)));
	}
}