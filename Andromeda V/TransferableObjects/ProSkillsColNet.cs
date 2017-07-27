using System;
using System.IO;

public class ProSkillsColNet : ITransferable
{
	public ProSkillNet[] items;

	public ProSkillsColNet()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		int num = br.ReadInt32();
		this.items = new ProSkillNet[num];
		for (int i = 0; i < num; i++)
		{
			this.items[i] = new ProSkillNet();
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