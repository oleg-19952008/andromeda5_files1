using System;
using UnityEngine;

public class SocialInteractionAnimationScript : MonoBehaviour
{
	public PlayerObjectPhysics target;

	public byte animationIndex;

	public float timeToDestroy = 5f;

	public bool playSound = true;

	public SocialInteractionAnimationScript()
	{
	}

	private void Start()
	{
		if (this.target == null)
		{
			Debug.Log("SocialInteractionAnimationScript.target = null");
			return;
		}
		this.timeToDestroy = 5f;
	}

	private void Update()
	{
		SocialInteractionAnimationScript _deltaTime = this;
		_deltaTime.timeToDestroy = _deltaTime.timeToDestroy - Time.get_deltaTime();
		if (this.target == null || this.target.isRemoved || !this.target.isAlive || this.timeToDestroy < 0f)
		{
			Object.Destroy(base.get_gameObject());
		}
		else
		{
			base.get_gameObject().get_transform().set_position(new Vector3(this.target.x, -6.5f, this.target.z));
		}
	}
}