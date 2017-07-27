using System;
using UnityEngine;

public class TalentTooltipInfo
{
	public Vector2 position;

	public TalentsInfo talent;

	public TalentTooltipInfo(Vector2 p, TalentsInfo t)
	{
		this.position = p;
		this.talent = t;
	}
}