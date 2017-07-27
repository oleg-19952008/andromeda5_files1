using System;
using System.IO;

public class EnterBaseParams : ITransferable
{
	public uint baseId;

	public uint enteringPlrNbID;

	public byte enteringDoor;

	public EnterBaseParams()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.baseId = br.ReadUInt32();
		this.enteringPlrNbID = br.ReadUInt32();
		this.enteringDoor = br.ReadByte();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.baseId);
		bw.Write(this.enteringPlrNbID);
		bw.Write(this.enteringDoor);
	}
}