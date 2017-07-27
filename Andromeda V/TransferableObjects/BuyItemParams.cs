using System;
using System.IO;

public class BuyItemParams : ITransferable
{
	public ushort itemType;

	public int qty;

	public SelectedCurrency currency;

	public byte slotId;

	public ItemLocation slotType = ItemLocation.Inventory;

	public int shipId = 0;

	public ItemLocation itemSrc;

	public BuyItemParams()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.itemType = br.ReadUInt16();
		this.qty = br.ReadInt32();
		this.currency = (SelectedCurrency)br.ReadByte();
		this.slotId = br.ReadByte();
		this.slotType = (ItemLocation)br.ReadByte();
		this.shipId = br.ReadInt32();
		this.itemSrc = (ItemLocation)br.ReadByte();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.itemType);
		bw.Write(this.qty);
		bw.Write((byte)this.currency);
		bw.Write(this.slotId);
		bw.Write((byte)this.slotType);
		bw.Write(this.shipId);
		bw.Write((byte)this.itemSrc);
	}
}