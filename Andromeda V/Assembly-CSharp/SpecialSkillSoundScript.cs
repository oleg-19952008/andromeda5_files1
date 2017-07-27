using System;
using UnityEngine;

public class SpecialSkillSoundScript : MonoBehaviour
{
	private DateTime startTime;

	private DateTime iterationTime;

	public SpecialSkillSoundScript()
	{
	}

	private void Start()
	{
		this.startTime = DateTime.get_Now();
		this.iterationTime = this.startTime.AddMilliseconds(900);
	}

	private void Update()
	{
		if (this.iterationTime < DateTime.get_Now())
		{
			string[] strArray = base.get_gameObject().get_name().Split(new char[] { '\u005F' });
			if (strArray[0] != string.Empty)
			{
				AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", strArray[0]);
				AudioManager.PlayAudioClip(fromStaticSet, base.get_transform().get_position());
			}
			this.iterationTime = this.iterationTime.AddMilliseconds(2000);
		}
	}
}