using System;
using UnityEngine;

public class LevelUpAnimationScript : MonoBehaviour
{
	public PlayerObjectPhysics target;

	private DateTime startTime;

	public LevelUpAnimationScript()
	{
	}

	private void Start()
	{
		AudioClip fromStaticSet;
		if (this.target == null)
		{
			Debug.Log("LevelUpAnimationScript.target = null");
			return;
		}
		this.startTime = DateTime.get_Now();
		if (!base.get_gameObject().get_name().Contains("LevelUP_pfb"))
		{
			fromStaticSet = (!base.get_gameObject().get_name().Contains("StoryQuestCompleted_pfb") ? (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "quest_complete") : (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "story_quest_complete"));
		}
		else
		{
			fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "02_levelup");
		}
		AudioManager.PlayAudioClip(fromStaticSet, base.get_transform().get_position());
	}

	private void Update()
	{
		if (this.target == null || this.target.isRemoved || !this.target.isAlive || this.startTime.AddMilliseconds(1400) < DateTime.get_Now())
		{
			Object.Destroy(base.get_gameObject());
		}
		else
		{
			base.get_gameObject().get_transform().set_position(new Vector3(this.target.x, 1.5f, this.target.z));
		}
	}
}