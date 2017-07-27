using System;
using UnityEngine;

public class SpawnSoundScript : MonoBehaviour
{
	public string audioClipName = string.Empty;

	private float lifeTime = 0.5f;

	public SpawnSoundScript()
	{
	}

	private void Start()
	{
		if (this.audioClipName != string.Empty)
		{
			AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", this.audioClipName);
			AudioManager.PlayAudioClip(fromStaticSet, base.get_transform().get_position());
		}
	}

	private void Update()
	{
		if (this.lifeTime > 0f)
		{
			SpawnSoundScript _deltaTime = this;
			_deltaTime.lifeTime = _deltaTime.lifeTime - Time.get_deltaTime();
		}
		else
		{
			Object.Destroy(base.get_gameObject());
		}
	}
}