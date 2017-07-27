using System;
using UnityEngine;

public class LightningBolt : MonoBehaviour
{
	public LightningBolt()
	{
	}

	private void Start()
	{
		Material _material = base.get_renderer().get_material();
		_material.SetFloat("_StartSeed", Random.get_value() * 1000f);
		base.get_renderer().set_material(_material);
	}

	private void Update()
	{
	}
}