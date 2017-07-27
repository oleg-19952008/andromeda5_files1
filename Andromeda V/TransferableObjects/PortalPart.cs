using System;
using System.IO;

public class PortalPart : ITransferable
{
	public int portalId;

	public ushort partTypeId;

	public short partAmount;

	public PortalPart()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.portalId = br.ReadInt32();
		this.partTypeId = br.ReadUInt16();
		this.partAmount = br.ReadInt16();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.portalId);
		bw.Write(this.partTypeId);
		bw.Write(this.partAmount);
	}
}