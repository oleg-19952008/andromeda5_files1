using System;
using System.IO;

public class ItemToShipParams : ITransferable
{
	public int itemID;

	public short slotID;

	public int shipID;

	[NonSerialized]
	public int dbID;

	public string slotTYPE;

	public ItemToShipParams()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.itemID = br.ReadInt32();
		this.slotID = br.ReadInt16();
		this.shipID = br.ReadInt32();
		this.slotTYPE = br.ReadString();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.itemID);
		bw.Write(this.slotID);
		bw.Write(this.shipID);
		bw.Write(this.slotTYPE);
	}
}