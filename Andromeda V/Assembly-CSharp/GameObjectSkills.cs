using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameObjectSkills
{
	public GameObjectPhysics gop;

	public DateTime lastSkillTime;

	public List<ActiveSkillItem> skills = new List<ActiveSkillItem>();

	public bool isNoTargetSkill;

	public GameObjectSkills()
	{
	}

	public void AddSkill(int skillID, float corX, float corY, float corZ, int time, byte lifestealTarget = 0)
	{
		List<ActiveSkillItem> list = this.skills;
		ActiveSkillItem activeSkillItem = new ActiveSkillItem()
		{
			skillId = skillID,
			requestTime = DateTime.get_Now(),
			correctionX = corX,
			correctionY = corY,
			correctionZ = corZ,
			lifeTime = time,
			lifeStealTarget = lifestealTarget
		};
		list.Add(activeSkillItem);
	}

	private void CreateSkill(ActiveSkillItem item, bool noTarget)
	{
		GameObjectSkills.<CreateSkill>c__AnonStorey16 variable = null;
		string empty = string.Empty;
		if (item.skillId != PlayerItems.TypeCouncilSkillLifesteal)
		{
			empty = string.Concat(((TalentsInfo)Enumerable.First<PlayerItemTypesData>(Enumerable.Where<PlayerItemTypesData>(StaticData.allTypes.get_Values(), new Func<PlayerItemTypesData, bool>(variable, (PlayerItemTypesData t) => t.itemType == this.item.skillId)))).assetName, "_pfb");
		}
		else
		{
			byte num = item.lifeStealTarget;
			if (num == 1)
			{
				empty = "SkillLifeStealCaster_pfb";
			}
			else if (num == 2)
			{
				empty = "SkillLifeStealTarget_pfb";
			}
		}
		GameObject gameObject = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab(empty));
		if (!noTarget)
		{
			gameObject.get_transform().set_position(new Vector3(this.gop.x + item.correctionX, this.gop.y + item.correctionY, this.gop.z + item.correctionZ));
		}
		else
		{
			gameObject.get_transform().set_position(new Vector3(item.correctionX, item.correctionY, item.correctionZ));
			item.theSkill = gameObject;
		}
		item.theSkill = gameObject;
	}

	public void Updating()
	{
		DateTime now = DateTime.get_Now();
		for (int i = 0; i < this.skills.get_Count(); i++)
		{
			ActiveSkillItem item = this.skills.get_Item(i);
			if (item.theSkill == null)
			{
				if (this.lastSkillTime.AddMilliseconds(150) < now)
				{
					this.CreateSkill(item, this.isNoTargetSkill);
					this.lastSkillTime = now;
					item.startTime = now;
				}
			}
			else if (this.isNoTargetSkill)
			{
				if (item.startTime.AddMilliseconds((double)item.lifeTime) <= now)
				{
					Object.Destroy(item.theSkill);
					this.skills.RemoveAt(i);
					i--;
				}
			}
			else if (!(item.startTime.AddMilliseconds((double)item.lifeTime) > now) || this.gop.isRemoved)
			{
				Object.Destroy(item.theSkill);
				this.skills.RemoveAt(i);
				i--;
			}
			else if (!this.gop.get_IsPoP() || ((PlayerObjectPhysics)this.gop).isAlive)
			{
				item.theSkill.get_transform().set_position(new Vector3(this.gop.x + item.correctionX, this.gop.y + item.correctionY, this.gop.z + item.correctionZ));
			}
			else if (item.skillId == PlayerItems.TypeTalentsRepairingDrones)
			{
				Object.Destroy(item.theSkill);
				this.skills.RemoveAt(i);
				i--;
			}
		}
	}
}