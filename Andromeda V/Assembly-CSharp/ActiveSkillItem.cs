using System;
using UnityEngine;

public class ActiveSkillItem
{
	public int skillId;

	public GameObject theSkill;

	public DateTime requestTime;

	public DateTime startTime;

	public int lifeTime;

	public float correctionX;

	public float correctionY;

	public float correctionZ;

	public byte lifeStealTarget;

	public ActiveSkillItem()
	{
	}
}