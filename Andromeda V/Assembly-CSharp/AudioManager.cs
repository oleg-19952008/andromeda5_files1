using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	private static byte lastStartedTrack;

	static AudioManager()
	{
	}

	public AudioManager()
	{
	}

	public static void ChangeTrack(string songName)
	{
		GameObject gameObject = GameObject.Find("Background music");
		AudioClip audioClip = null;
		if (gameObject == null)
		{
			return;
		}
		audioClip = (NetworkScript.player == null || NetworkScript.player.vessel.galaxy.get_galaxyId() != 1000 ? (AudioClip)playWebGame.assets.GetFromStaticSet("Music", songName) : (AudioClip)playWebGame.assets.GetFromStaticSet("TutorialWindow", "1"));
		AudioSource component = gameObject.GetComponent<AudioSource>();
		component.set_clip(audioClip);
		component.set_volume(GuiFramework.musicVolume * GuiFramework.masterVolume);
		component.Play();
	}

	public static void ChangeTrack()
	{
		int num;
		GameObject gameObject = GameObject.Find("Background music");
		AudioClip fromStaticSet = null;
		if (gameObject == null)
		{
			return;
		}
		if (NetworkScript.player != null && NetworkScript.player.vessel.galaxy.get_galaxyId() == 1000)
		{
			fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("TutorialWindow", "1");
		}
		else if (AudioManager.lastStartedTrack != 0)
		{
			do
			{
				num = Random.Range(1, 4);
			}
			while (num == AudioManager.lastStartedTrack);
			AudioManager.lastStartedTrack = (byte)num;
			fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Music", string.Format("{0}", num));
		}
		else
		{
			AudioManager.lastStartedTrack = 2;
			fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Music", "2");
		}
		AudioSource component = gameObject.GetComponent<AudioSource>();
		component.set_clip(fromStaticSet);
		component.set_volume(GuiFramework.musicVolume * GuiFramework.masterVolume);
		component.Play();
	}

	public static GameObject InitBGMusic()
	{
		return AudioManager.InitMusic();
	}

	public static GameObject InitMusic()
	{
		if (GuiFramework.masterVolume <= 0.02f || GuiFramework.musicVolume <= 0.02f || playWebGame.assets == null)
		{
			return null;
		}
		GameObject gameObject = GameObject.Find("Background music");
		if (gameObject != null)
		{
			AudioManager.ChangeTrack();
			return null;
		}
		gameObject = new GameObject("Background music");
		AudioSource audioSource = gameObject.AddComponent<AudioSource>();
		gameObject.AddComponent<ChangeMusic>().source = audioSource;
		AudioManager.ChangeTrack();
		return gameObject;
	}

	public static AudioSource PlayAudioClip(AudioClip clip, Vector3 position)
	{
		if (clip == null)
		{
			return new AudioSource();
		}
		GameObject gameObject = new GameObject("One shot audio");
		gameObject.get_transform().set_position(position);
		AudioSource audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.set_clip(clip);
		audioSource.set_volume(GuiFramework.fxVolume * GuiFramework.masterVolume);
		audioSource.Play();
		audioSource.set_minDistance(15f);
		Object.Destroy(gameObject, clip.get_length());
		return audioSource;
	}

	public static AudioSource PlayGUISound(AudioClip clip)
	{
		if (clip == null)
		{
			return new AudioSource();
		}
		GameObject gameObject = new GameObject("One shot GUI sound");
		gameObject.get_transform().set_position(Camera.get_main().get_transform().get_position());
		gameObject.get_transform().set_parent(Camera.get_main().get_transform());
		AudioSource audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.set_clip(clip);
		audioSource.set_volume(GuiFramework.voiceVolume * GuiFramework.masterVolume);
		audioSource.PlayOneShot(audioSource.get_clip(), audioSource.get_volume());
		Object.Destroy(gameObject, clip.get_length());
		return audioSource;
	}

	public static AudioSource PlayGUISoundLoop(AudioClip clip, float duration)
	{
		if (clip == null)
		{
			return new AudioSource();
		}
		GameObject gameObject = new GameObject("Looping GUI sound");
		AudioSource audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.set_clip(clip);
		audioSource.set_volume(GuiFramework.voiceVolume * GuiFramework.masterVolume * 0.6f);
		audioSource.set_loop(true);
		audioSource.Play();
		Object.Destroy(gameObject, duration);
		return audioSource;
	}

	public static AudioSource PlayGUISoundLoopInf(AudioClip clip)
	{
		if (clip == null)
		{
			return new AudioSource();
		}
		AudioSource audioSource = (new GameObject("Looping GUI sound Inf")).AddComponent<AudioSource>();
		audioSource.set_clip(clip);
		audioSource.set_volume(GuiFramework.voiceVolume * GuiFramework.masterVolume * 0.6f);
		audioSource.set_loop(true);
		audioSource.Play();
		return audioSource;
	}

	public static void SetVolume(float vol)
	{
		GameObject gameObject = GameObject.Find("Background music");
		if (gameObject == null)
		{
			AudioManager.InitMusic();
			return;
		}
		AudioSource component = gameObject.GetComponent<AudioSource>();
		component.set_volume(vol);
		if (component.get_isPlaying())
		{
			if (vol <= 0.02f)
			{
				component.Stop();
			}
		}
		else if (vol > 0.02f)
		{
			component.Play();
		}
	}

	private void Start()
	{
	}

	public static void StopGUISoundLoop()
	{
		GameObject gameObject = GameObject.Find("Looping GUI sound");
		if (gameObject != null)
		{
			gameObject.GetComponent<AudioSource>().Stop();
			Object.Destroy(gameObject);
		}
	}
}