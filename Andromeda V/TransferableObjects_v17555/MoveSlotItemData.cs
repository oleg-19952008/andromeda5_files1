using System;
using System.IO;

public class MoveSlotItemData : ITransferable
{
	public byte srcSlot;

	public byte dstSlot;

	public ushort srcSlotType;

	public ushort dstSlotType;

	public int srcShipId;

	public int dstShipId;

	public bool srcIsActive;

	public bool dstIsActive;

	public bool isSwap;

	public MoveSlotItemData()
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
		this.srcIsActive = br.ReadBoolean();
		this.dstIsActive = br.ReadBoolean();
		this.isSwap = br.ReadBoolean();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.srcSlot);
		bw.Write(this.dstSlot);
		bw.Write(this.srcSlotType);
		bw.Write(this.dstSlotType);
		bw.Write(this.srcShipId);
		bw.Write(this.dstShipId);
		bw.Write(this.srcIsActive);
		bw.Write(this.dstIsActive);
		bw.Write(this.isSwap);
	}
}