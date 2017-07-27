using System;
using System.IO;

public class StackSlotItemData : ITransferable
{
	public byte srcSlot;

	public byte dstSlot;

	public ushort srcSlotType;

	public ushort dstSlotType;

	public int srcShipId;

	public int dstShipId;

	public ushort transferAmount;

	public StackSlotItemData()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.srcSlot = br.ReadByte();
		this.dstSlot = br.ReadByte();
		this.srcSlotType = br.ReadUInt16();
		this.dstSlotType = br.ReadUInt16();
		this.srcShipId = br.ReadInt32();
		this.dstShipId = br.ReadInt32();
		this.transferAmount = br.ReadUInt16();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.srcSlot);
		bw.Write(this.dstSlot);
		bw.Write(this.srcSlotType);
		bw.Write(this.dstSlotType);
		bw.Write(this.srcShipId);
		bw.Write(this.dstShipId);
		bw.Write(this.transferAmount);
	}
}