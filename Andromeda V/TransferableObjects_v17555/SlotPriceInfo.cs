using System;
using System.IO;

public class SlotPriceInfo : ITransferable
{
	public byte slotId;

	public string slotType;

	public int priceCash;

	public int priceNova;

	public int priceEqulibrium;

	public int priceUltralibrium;

	public SlotPriceInfo()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.slotId = br.ReadByte();
		this.slotType = br.ReadString();
		this.priceCash = br.ReadInt32();
		this.priceNova = br.ReadInt32();
		this.priceEqulibrium = br.ReadInt32();
		this.priceUltralibrium = br.ReadInt32();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.slotId);
		bw.Write(this.slotType ?? "");
		bw.Write(this.priceCash);
		bw.Write(this.priceNova);
		bw.Write(this.priceEqulibrium);
		bw.Write(this.priceUltralibrium);
	}
}