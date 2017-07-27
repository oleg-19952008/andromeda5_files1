using System;
using System.IO;

public class ResourceForTrade : ITransferable
{
	public ushort itemType;

	public int amount;

	public float priceCash;

	public float priceNova;

	public ResourceForTrade()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.itemType = br.ReadUInt16();
		this.amount = br.ReadInt32();
		this.priceCash = br.ReadSingle();
		this.priceNova = br.ReadSingle();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.itemType);
		bw.Write(this.amount);
		bw.Write(this.priceCash);
		bw.Write(this.priceNova);
	}
}