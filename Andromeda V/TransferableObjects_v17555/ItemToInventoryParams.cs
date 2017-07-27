using System;
using System.IO;

public class ItemToInventoryParams : ITransferable
{
	public int itemID;

	[NonSerialized]
	public int dbID;

	public ItemToInventoryParams()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.dbID = br.ReadInt32();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.dbID);
	}
}