using System;
using System.IO;

public class SkillRequirements : ITransferable
{
	public int skillID;

	public int level;

	public SkillRequirements()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.skillID = br.ReadInt32();
		this.level = br.ReadInt32();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.skillID);
		bw.Write(this.level);
	}
}