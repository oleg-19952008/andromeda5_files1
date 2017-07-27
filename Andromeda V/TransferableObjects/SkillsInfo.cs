using System;
using System.IO;

public class SkillsInfo : ITransferable
{
	public int skillID;

	public string skillType;

	public string skillName;

	public string assetName;

	public byte maxLevel;

	public int reqSkillPoint;

	public int reqCredits;

	public int reqDeuterium;

	public int reqEquilibrium;

	public string effectDescription;

	public float effectValue;

	public SkillRequirements[] reqSkills;

	public SkillsInfo()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.skillID = br.ReadInt32();
		this.skillType = br.ReadString();
		this.skillName = br.ReadString();
		this.assetName = br.ReadString();
		this.maxLevel = br.ReadByte();
		this.reqSkillPoint = br.ReadInt32();
		this.reqCredits = br.ReadInt32();
		this.reqDeuterium = br.ReadInt32();
		this.reqEquilibrium = br.ReadInt32();
		this.effectDescription = br.ReadString();
		this.effectValue = br.ReadSingle();
		int num = br.ReadInt32();
		this.reqSkills = new SkillRequirements[num];
		for (int i = 0; i < num; i++)
		{
			this.reqSkills[i] = new SkillRequirements();
			this.reqSkills[i].Deserialize(br);
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.skillID);
		bw.Write(this.skillType);
		bw.Write(this.skillName);
		bw.Write(this.assetName ?? "");
		bw.Write(this.maxLevel);
		bw.Write(this.reqSkillPoint);
		bw.Write(this.reqCredits);
		bw.Write(this.reqDeuterium);
		bw.Write(this.reqEquilibrium);
		bw.Write(this.effectDescription);
		bw.Write(this.effectValue);
		int length = (int)this.reqSkills.Length;
		bw.Write(length);
		for (int i = 0; i < length; i++)
		{
			this.reqSkills[i].Serialize(bw);
		}
	}
}