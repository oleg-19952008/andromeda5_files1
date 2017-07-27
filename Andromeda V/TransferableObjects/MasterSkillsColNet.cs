using System;
using System.IO;

public class MasterSkillsColNet : ITransferable
{
	public MasterSkillNet[] items;

	public MasterSkillsColNet()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		int num = br.ReadInt32();
		this.items = new MasterSkillNet[num];
		for (int i = 0; i < num; i++)
		{
			this.items[i] = new MasterSkillNet();
			this.items[i].Deserialize(br);
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write((int)this.items.Length);
		for (int i = 0; i < (int)this.items.Length; i++)
		{
			this.items[i].Serialize(bw);
		}
	}
}