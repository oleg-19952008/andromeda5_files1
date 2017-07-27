using System;
using UnityEngine;

public class MultiKillAnimationScript : MonoBehaviour
{
	public PlayerObjectPhysics target;

	public byte killCnt;

	public string lblText;

	public float timeToDestroy = 2f;

	public bool playSound = true;

	public MultiKillAnimationScript()
	{
	}

	private void Start()
	{
		if (this.target == null)
		{
			Debug.Log("MultiKillAnimationScript.target = null");
			return;
		}
		this.timeToDestroy = 2f;
		if (this.playSound)
		{
			AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Multikills/Voice", string.Format("multikill_voice_{0}", Mathf.Min(this.killCnt - 1, 9)));
			AudioManager.PlayGUISound(fromStaticSet);
		}
	}

	private void Update()
	{
		MultiKillAnimationScript _deltaTime = this;
		_deltaTime.timeToDestroy = _deltaTime.timeToDestroy - Time.get_deltaTime();
		if (this.target == null || this.target.isRemoved || !this.target.isAlive || this.timeToDestroy < 0f)
		{
			Object.Destroy(base.get_gameObject());
		}
		else
		{
			base.get_gameObject().get_transform().set_position(new Vector3(this.target.x, 1.5f, this.target.z));
		}
	}
}