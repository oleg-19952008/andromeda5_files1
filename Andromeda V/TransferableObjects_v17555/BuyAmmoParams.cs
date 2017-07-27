using System;
using System.IO;

public class BuyAmmoParams : ITransferable
{
	public ushort ammoItemType;

	public float creditsPrice;

	public float novaPrice;

	public int Qty;

	public BuyAmmoParams()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.ammoItemType = br.ReadUInt16();
		this.creditsPrice = br.ReadSingle();
		this.novaPrice = br.ReadSingle();
		this.Qty = br.ReadInt32();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.ammoItemType);
		bw.Write(this.creditsPrice);
		bw.Write(this.novaPrice);
		bw.Write(this.Qty);
	}
}