using System;
using System.IO;

public class GalaxyJumpParam : ITransferable
{
	public int sourceGalaxyId;

	public int destinationGalaxyId;

	public uint jumpingPlayerNbId;

	public bool isSpecificCoordinatesSet;

	public float x;

	public float z;

	public byte paymentCurrency;

	public byte portalId;

	public byte factionGalaxyJump;

	public GalaxyJumpParam()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.sourceGalaxyId = br.ReadInt32();
		this.destinationGalaxyId = br.ReadInt32();
		this.jumpingPlayerNbId = br.ReadUInt32();
		this.isSpecificCoordinatesSet = br.ReadBoolean();
		this.x = br.ReadSingle();
		this.z = br.ReadSingle();
		this.paymentCurrency = br.ReadByte();
		this.portalId = br.ReadByte();
		this.factionGalaxyJump = br.ReadByte();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.sourceGalaxyId);
		bw.Write(this.destinationGalaxyId);
		bw.Write(this.jumpingPlayerNbId);
		bw.Write(this.isSpecificCoordinatesSet);
		bw.Write(this.x);
		bw.Write(this.z);
		bw.Write(this.paymentCurrency);
		bw.Write(this.portalId);
		bw.Write(this.factionGalaxyJump);
	}
}