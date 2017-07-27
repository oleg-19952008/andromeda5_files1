using System;
using System.Collections.Generic;
using System.IO;

public class ResourceTradeData : ITransferable
{
	public SortedList<ushort, int> sellList;

	public ResourceTradeData()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		int num = br.ReadInt32();
		if (num > 0)
		{
			this.sellList = new SortedList<ushort, int>();
			for (int i = 0; i < num; i++)
			{
				ushort num1 = br.ReadUInt16();
				int num2 = br.ReadInt32();
				this.sellList.Add(num1, num2);
			}
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		if (this.sellList != null)
		{
			bw.Write(this.sellList.Count);
		}
		else
		{
			bw.Write(0);
		}
		foreach (ushort key in this.sellList.Keys)
		{
			bw.Write(key);
			bw.Write(this.sellList[key]);
		}
	}
}