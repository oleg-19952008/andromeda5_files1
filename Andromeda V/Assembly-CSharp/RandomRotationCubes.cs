using System;
using UnityEngine;

public class RandomRotationCubes : MonoBehaviour
{
	private float a;

	private float b;

	private float c;

	public RandomRotationCubes()
	{
	}

	private void Start()
	{
		this.a = (float)Random.Range(5, 30);
		this.b = (float)Random.Range(-30, 15);
		this.c = (float)Random.Range(-7, 10);
	}

	private void Update()
	{
		float _deltaTime = Time.get_deltaTime();
		base.get_gameObject().get_transform().Rotate(new Vector3(this.a * _deltaTime, this.b * _deltaTime, this.c * _deltaTime));
	}
}