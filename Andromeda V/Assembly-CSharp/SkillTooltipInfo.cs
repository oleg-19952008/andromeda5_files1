using System;
using UnityEngine;

public class SkillTooltipInfo
{
	public Vector2 position;

	public TalentsInfo skill;

	public SkillTooltipInfo(Vector2 p, TalentsInfo s)
	{
		this.position = p;
		this.skill = s;
	}
}