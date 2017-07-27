using System;
using System.IO;

public class ShipAttachmentNet : ITransferable
{
	public int id;

	public ushort playerItemTypeId;

	public int pShipId;

	public byte slotNumber;

	public byte category;

	public bool isActive;

	public short categorySHORT
	{
		set
		{
			this.category = (byte)value;
		}
	}

	public short PlayerItemTypeIdSHORT
	{
		set
		{
			this.playerItemTypeId = (ushort)value;
		}
	}

	public short slotNumberSHORT
	{
		set
		{
			this.slotNumber = (byte)value;
		}
	}

	public ShipAttachmentNet()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.id = br.ReadInt32();
		this.playerItemTypeId = br.ReadUInt16();
		this.pShipId = br.ReadInt32();
		this.slotNumber = br.ReadByte();
		this.category = br.ReadByte();
		this.isActive = br.ReadBoolean();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.id);
		bw.Write(this.playerItemTypeId);
		bw.Write(this.pShipId);
		bw.Write(this.slotNumber);
		bw.Write(this.category);
		bw.Write(this.isActive);
	}
}