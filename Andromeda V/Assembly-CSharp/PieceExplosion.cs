using System;
using UnityEngine;

public class PieceExplosion : MonoBehaviour
{
	public float DeathTime = 7f;

	public PieceExplosion()
	{
	}

	private void Awake()
	{
		PieceExplosion deathTime = this;
		deathTime.DeathTime = deathTime.DeathTime + Time.get_time();
	}

	private void Update()
	{
		if (this.DeathTime < Time.get_time())
		{
			Object.Destroy(base.get_gameObject());
		}
	}
}