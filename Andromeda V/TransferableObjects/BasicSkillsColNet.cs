using System;
using System.IO;

public class BasicSkillsColNet : ITransferable
{
	public BasicSkillNet[] items;

	public BasicSkillsColNet()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.items = new BasicSkillNet[br.ReadInt32()];
		for (int i = 0; i < (int)this.items.Length; i++)
		{
			this.items[i] = new BasicSkillNet();
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