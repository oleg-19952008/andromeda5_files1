using System;
using System.Collections.Generic;
using System.IO;

public class SellOrder : ITransferable
{
	public List<ResourceForTrade> items;

	public SellOrder()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.items = new List<ResourceForTrade>();
		int num = br.ReadInt32();
		for (int i = 0; i < num; i++)
		{
			ResourceForTrade resourceForTrade = new ResourceForTrade();
			resourceForTrade.Deserialize(br);
			this.items.Add(resourceForTrade);
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.items.Count);
		foreach (ResourceForTrade item in this.items)
		{
			item.Serialize(bw);
		}
	}
}