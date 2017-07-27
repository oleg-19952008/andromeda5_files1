using System;
using System.Collections.Generic;
using System.IO;

public class NewQuest : ITransferable
{
	public int id;

	public QuestTypeEnum type;

	public byte level;

	public int actorId;

	public int rewardXp;

	public int rewardCash;

	public List<NewQuestObjective> objectives;

	public List<NewQuestReward> rewards;

	public NewQuest()
	{
	}

	public bool CanCollectReward(PlayerData plr)
	{
		bool flag;
		foreach (NewQuestObjective objective in this.objectives)
		{
			if ((objective.isOptional ? false : !objective.IsComplete(plr)))
			{
				flag = false;
				return flag;
			}
		}
		flag = true;
		return flag;
	}

	public void Deserialize(BinaryReader br)
	{
		int i;
		this.id = br.ReadInt32();
		this.type = (QuestTypeEnum)br.ReadByte();
		this.level = br.ReadByte();
		this.actorId = br.ReadInt32();
		this.rewardXp = br.ReadInt32();
		this.rewardCash = br.ReadInt32();
		int num = br.ReadInt32();
		this.objectives = new List<NewQuestObjective>();
		for (i = 0; i < num; i++)
		{
			NewQuestObjective newQuestObjective = new NewQuestObjective()
			{
				id = br.ReadInt32(),
				parentObjectiveId = br.ReadInt32(),
				type = (ObjectiveTypeEnum)br.ReadByte(),
				targetAmount = br.ReadInt32(),
				targetParam1 = br.ReadInt32(),
				targetParam2 = br.ReadInt32(),
				targetParam3 = br.ReadInt32(),
				targetParam4 = br.ReadInt32(),
				targetParam5 = br.ReadInt32(),
				isOptional = br.ReadBoolean(),
				haveCustomText = br.ReadBoolean(),
				targetGalaxyKey = br.ReadInt32(),
				onlyGalaxyPointer = br.ReadBoolean(),
				factionOnePointerGalaxyId = br.ReadInt32(),
				factionOnePointerX = br.ReadSingle(),
				factionOnePointerZ = br.ReadSingle(),
				factionTwoPointerGalaxyId = br.ReadInt32(),
				factionTwoPointerX = br.ReadSingle(),
				factionTwoPointerZ = br.ReadSingle()
			};
			this.objectives.Add(newQuestObjective);
		}
		num = br.ReadInt32();
		this.rewards = new List<NewQuestReward>();
		for (i = 0; i < num; i++)
		{
			NewQuestReward newQuestReward = new NewQuestReward()
			{
				itemTypeId = br.ReadUInt16(),
				amount = br.ReadInt32()
			};
			this.rewards.Add(newQuestReward);
		}
	}

	public void GetRewardsValue(PlayerData plr, out int xp, out int cash)
	{
		xp = 0;
		cash = 0;
		int num = 0;
		foreach (NewQuestObjective objective in this.objectives)
		{
			if ((!objective.isOptional ? false : objective.IsComplete(plr)))
			{
				num++;
			}
		}
		int num1 = (this.level - plr.playerBelongings.playerLevel) * (this.level - plr.playerBelongings.playerLevel);
		if (plr.playerBelongings.playerLevel >= this.level)
		{
			xp = (int)Math.Max(0f, (float)this.rewardXp * (100f + plr.cfg.experienceGain - (float)num1 * 3f) / 100f * (1f + (float)num * 0.25f));
		}
		else
		{
			xp = (int)((float)this.rewardXp * (100f + plr.cfg.experienceGain + (float)num1 * 3f) / 100f * (1f + (float)num * 0.25f));
		}
		cash = (int)((float)this.rewardCash * (1f + (float)num * 0.25f));
	}

	public int GetSkipPrice(short playerLevel)
	{
		int num;
		int num1 = 0;
		if (this.type != QuestTypeEnum.Tutorial)
		{
			double num2 = (double)(this.level - playerLevel + 6);
			num2 = (num2 > 0 ? num2 : 0);
			num1 = (int)(Math.Pow(num2, 2) * (10 + 0.1 * (double)playerLevel));
			num = num1;
		}
		else
		{
			num = num1;
		}
		return num;
	}

	public void Serialize(BinaryWriter bw)
	{
		int i;
		bw.Write(this.id);
		bw.Write((byte)this.type);
		bw.Write(this.level);
		bw.Write(this.actorId);
		bw.Write(this.rewardXp);
		bw.Write(this.rewardCash);
		int count = this.objectives.Count;
		bw.Write(count);
		for (i = 0; i < count; i++)
		{
			bw.Write(this.objectives[i].id);
			bw.Write(this.objectives[i].parentObjectiveId);
			bw.Write((byte)this.objectives[i].type);
			bw.Write(this.objectives[i].targetAmount);
			bw.Write(this.objectives[i].targetParam1);
			bw.Write(this.objectives[i].targetParam2);
			bw.Write(this.objectives[i].targetParam3);
			bw.Write(this.objectives[i].targetParam4);
			bw.Write(this.objectives[i].targetParam5);
			bw.Write(this.objectives[i].isOptional);
			bw.Write(this.objectives[i].haveCustomText);
			bw.Write(this.objectives[i].targetGalaxyKey);
			bw.Write(this.objectives[i].onlyGalaxyPointer);
			bw.Write(this.objectives[i].factionOnePointerGalaxyId);
			bw.Write(this.objectives[i].factionOnePointerX);
			bw.Write(this.objectives[i].factionOnePointerZ);
			bw.Write(this.objectives[i].factionTwoPointerGalaxyId);
			bw.Write(this.objectives[i].factionTwoPointerX);
			bw.Write(this.objectives[i].factionTwoPointerZ);
		}
		count = (this.rewards != null ? this.rewards.Count : 0);
		bw.Write(count);
		for (i = 0; i < count; i++)
		{
			bw.Write(this.rewards[i].itemTypeId);
			bw.Write(this.rewards[i].amount);
		}
	}
}