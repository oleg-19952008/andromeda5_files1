using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActiveSkillManager
{
	private SortedList<uint, GameObjectSkills> collection = new SortedList<uint, GameObjectSkills>();

	public ActiveSkillManager()
	{
	}

	public void AddActiveSkill(int skillID, float corX, float corY, float corZ, int time, GameObjectPhysics gop, byte lifestealTarget = 0)
	{
		GameObjectSkills gameObjectSkill;
		GameObjectSkills item = null;
		if (gop == null)
		{
			if (!this.collection.ContainsKey(0))
			{
				gameObjectSkill = new GameObjectSkills()
				{
					gop = gop
				};
				item = gameObjectSkill;
				item.isNoTargetSkill = true;
				this.collection.Add(0, item);
			}
			else
			{
				item = this.collection.get_Item(0);
			}
			item.AddSkill(skillID, corX, corY, corZ, time, 0);
			return;
		}
		if (this.collection.ContainsKey(gop.neighbourhoodId))
		{
			item = this.collection.get_Item(gop.neighbourhoodId);
		}
		else
		{
			gameObjectSkill = new GameObjectSkills()
			{
				gop = gop
			};
			item = gameObjectSkill;
			this.collection.Add(gop.neighbourhoodId, item);
		}
		item.AddSkill(skillID, corX, corY, corZ, time, lifestealTarget);
	}

	public void Clear()
	{
		IEnumerator<GameObjectSkills> enumerator = this.collection.get_Values().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				GameObjectSkills current = enumerator.get_Current();
				List<ActiveSkillItem>.Enumerator enumerator1 = current.skills.GetEnumerator();
				try
				{
					while (enumerator1.MoveNext())
					{
						Object.Destroy(enumerator1.get_Current().theSkill);
					}
				}
				finally
				{
					enumerator1.Dispose();
				}
				current.skills.Clear();
			}
		}
		finally
		{
			if (enumerator == null)
			{
			}
			enumerator.Dispose();
		}
		this.collection.Clear();
	}

	public void Updating()
	{
		int num = 0;
		GameObjectSkills[] array = Enumerable.ToArray<GameObjectSkills>(this.collection.get_Values());
		while (num < this.collection.get_Count())
		{
			GameObjectSkills gameObjectSkill = array[num];
			if (gameObjectSkill == null)
			{
				return;
			}
			gameObjectSkill.Updating();
			if (gameObjectSkill.skills.get_Count() < 1)
			{
				if (!gameObjectSkill.isNoTargetSkill)
				{
					this.collection.Remove(gameObjectSkill.gop.neighbourhoodId);
				}
				else
				{
					this.collection.Remove(0);
				}
				array = Enumerable.ToArray<GameObjectSkills>(this.collection.get_Values());
				num--;
			}
			num++;
		}
	}
}