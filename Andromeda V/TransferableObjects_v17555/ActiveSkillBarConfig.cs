using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

public class ActiveSkillBarConfig : ITransferable
{
	public SortedList<int, ActiveSkillSlot> skillSlots;

	public ActiveSkillBarConfig()
	{
	}

	public ActiveSkillSlot AddSkill(ushort skill, int slot, PlayerBelongings pb)
	{
		ActiveSkillSlot activeSkillSlot;
		lock (this.skillSlots)
		{
			if (!this.skillSlots.ContainsKey(slot))
			{
				ActiveSkillSlot activeSkillSlot1 = new ActiveSkillSlot()
				{
					cooldown = pb.playerItems.GetSkillCooldown(skill),
					isConfigured = true,
					nextCastTime = (long)0,
					range = ((TalentsInfo)(
						from t in StaticData.allTypes.Values
						where t.itemType == skill
						select t).First<PlayerItemTypesData>()).range,
					skillId = skill,
					slotId = (short)slot
				};
				this.skillSlots.Add(activeSkillSlot1.slotId, activeSkillSlot1);
				activeSkillSlot = activeSkillSlot1;
			}
			else
			{
				activeSkillSlot = null;
			}
		}
		return activeSkillSlot;
	}

	public void Deserialize(BinaryReader br)
	{
		this.skillSlots = new SortedList<int, ActiveSkillSlot>();
		int num = br.ReadInt32();
		for (int i = 0; i < num; i++)
		{
			ActiveSkillSlot activeSkillSlot = (ActiveSkillSlot)TransferablesFramework.DeserializeITransferable(br);
			this.skillSlots.Add(activeSkillSlot.slotId, activeSkillSlot);
		}
	}

	public void MoveSkill(int dstSlot, int srcSlot, out ActiveSkillSlot srcOut, out ActiveSkillSlot dstOut)
	{
		ActiveSkillSlot item = this.skillSlots[srcSlot];
		srcOut = item;
		ActiveSkillSlot activeSkillSlot = null;
		if (this.skillSlots.ContainsKey(dstSlot))
		{
			activeSkillSlot = this.skillSlots[dstSlot];
		}
		if (activeSkillSlot != null)
		{
			short num = item.slotId;
			item.slotId = (short)dstSlot;
			activeSkillSlot.slotId = num;
			this.skillSlots.Remove(srcSlot);
			this.skillSlots.Remove(dstSlot);
			this.skillSlots.Add(item.slotId, item);
			this.skillSlots.Add(activeSkillSlot.slotId, activeSkillSlot);
			dstOut = activeSkillSlot;
		}
		else
		{
			item.slotId = (short)dstSlot;
			this.skillSlots.Remove(srcSlot);
			this.skillSlots.Add(item.slotId, item);
			dstOut = null;
		}
	}

	public void RemoveSkill(ushort skill)
	{
		lock (this.skillSlots)
		{
			ActiveSkillSlot activeSkillSlot = (
				from s in this.skillSlots.Values
				where s.skillId == skill
				select s).FirstOrDefault<ActiveSkillSlot>();
			if (activeSkillSlot != null)
			{
				this.skillSlots.Remove(activeSkillSlot.slotId);
			}
			else
			{
				return;
			}
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.skillSlots.Count);
		foreach (ActiveSkillSlot value in this.skillSlots.Values)
		{
			TransferablesFramework.SerializeITransferable(bw, value, TransferContext.None);
		}
	}

	public void SetCooldownAndRange(PlayerBelongings pb)
	{
		foreach (ActiveSkillSlot value in this.skillSlots.Values)
		{
			value.cooldown = pb.playerItems.GetSkillCooldown((ushort)value.skillId);
			value.range = ((TalentsInfo)(
				from t in StaticData.allTypes.Values
				where t.itemType == value.skillId
				select t).First<PlayerItemTypesData>()).range;
		}
	}
}