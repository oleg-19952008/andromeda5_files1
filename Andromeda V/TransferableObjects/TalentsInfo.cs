using System;
using System.IO;

public class TalentsInfo : PlayerItemTypesData, ITransferable
{
	public short talentsClass;

	public byte maxLevel;

	public int reqSpendSkillPoint;

	public bool isStatic;

	public int cooldown;

	public short range;

	public string nextLevelInfo;

	public string neuronBonusDesc;

	public TalentsInfo()
	{
	}

	public override void Deserialize(BinaryReader br)
	{
		this.talentsClass = br.ReadInt16();
		this.maxLevel = br.ReadByte();
		this.reqSpendSkillPoint = br.ReadInt32();
		this.isStatic = br.ReadBoolean();
		this.cooldown = br.ReadInt32();
		this.range = br.ReadInt16();
		this.nextLevelInfo = br.ReadString();
		this.neuronBonusDesc = br.ReadString();
		base.Deserialize(br);
	}

	public override void Serialize(BinaryWriter bw)
	{
		bw.Write(this.talentsClass);
		bw.Write(this.maxLevel);
		bw.Write(this.reqSpendSkillPoint);
		bw.Write(this.isStatic);
		bw.Write(this.cooldown);
		bw.Write(this.range);
		bw.Write(this.nextLevelInfo);
		bw.Write(this.neuronBonusDesc);
		base.Serialize(bw);
	}
}