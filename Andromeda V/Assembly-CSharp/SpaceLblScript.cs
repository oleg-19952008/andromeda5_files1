using System;
using UnityEngine;

public class SpaceLblScript : MonoBehaviour
{
	private float lifeTime;

	public float fontSize;

	private TextMesh tm;

	public SpaceLblScript()
	{
	}

	private void Start()
	{
		this.tm = base.get_gameObject().GetComponent<TextMesh>();
	}

	private void Update()
	{
		SpaceLblScript _deltaTime = this;
		_deltaTime.lifeTime = _deltaTime.lifeTime + Time.get_deltaTime();
		if (this.lifeTime <= 0.5f)
		{
			this.tm.set_fontSize((int)(this.fontSize + this.fontSize * this.lifeTime * 2f));
		}
		else if (this.lifeTime >= 1.5f)
		{
			Object.Destroy(base.get_gameObject());
		}
		else
		{
			this.tm.set_fontSize((int)(this.fontSize * 2f - this.fontSize * (this.lifeTime - 0.5f)));
		}
	}
}