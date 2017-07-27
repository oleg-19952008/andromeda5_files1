using System;
using UnityEngine;

public class ActiveSkillScript : MonoBehaviour
{
	public ActiveSkillObject skill;

	public ActiveSkillScript()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (this.skill == null)
		{
			return;
		}
		if (this.skill.isRemoved)
		{
			return;
		}
		float _deltaTime = Time.get_deltaTime();
		float single = 0f;
		float single1 = 0f;
		float single2 = 0f;
		this.skill.CalculateObjectMovement(_deltaTime, ref single, ref single1, ref single2);
	}
}