using System;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
	public AudioSource source;

	public ChangeMusic()
	{
	}

	private void Update()
	{
		if (this.source == null || GuiFramework.masterVolume <= 0.02f || GuiFramework.musicVolume <= 0.02f)
		{
			return;
		}
		if (!this.source.get_isPlaying())
		{
			AudioManager.ChangeTrack();
		}
	}
}