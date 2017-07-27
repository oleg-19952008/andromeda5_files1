using System;
using System.IO;

public class NavigationMap : ITransferable
{
	public NavigationMapItem[] items;

	public byte mapIndex;

	public NavigationMap()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.items = new NavigationMapItem[br.ReadInt32()];
		for (int i = 0; i < (int)this.items.Length; i++)
		{
			this.items[i] = new NavigationMapItem();
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
		bw.Write(this.mapIndex);
	}
}