using System;
using System.IO;

public class LearnedActiveSkillData : ITransferable
{
	public ActiveSkillBarConfig skillSlots;

	public PlayerItems playerItems;

	public LearnedActiveSkillData()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.skillSlots = new ActiveSkillBarConfig();
		this.skillSlots.Deserialize(br);
		this.playerItems = new PlayerItems();
		this.playerItems.Deserialize(br);
	}

	public void Serialize(BinaryWriter bw)
	{
		this.skillSlots.Serialize(bw);
		this.playerItems.Serialize(bw);
	}
}