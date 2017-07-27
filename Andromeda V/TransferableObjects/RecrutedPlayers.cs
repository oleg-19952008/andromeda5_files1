using System;
using System.Collections.Generic;
using System.IO;

public class RecrutedPlayers : ITransferable
{
	public List<Recruit> referals = new List<Recruit>();

	public RecrutedPlayers()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		int num = br.ReadInt32();
		this.referals.Clear();
		for (int i = 0; i < num; i++)
		{
			Recruit recruit = new Recruit()
			{
				userName = br.ReadString(),
				level = br.ReadInt32(),
				viral = br.ReadInt32()
			};
			this.referals.Add(recruit);
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.referals.Count);
		for (int i = 0; i < this.referals.Count; i++)
		{
			bw.Write(this.referals[i].userName);
			bw.Write(this.referals[i].level);
			bw.Write(this.referals[i].viral);
		}
	}
}