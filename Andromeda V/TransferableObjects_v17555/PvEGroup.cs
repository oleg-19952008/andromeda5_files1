using System;
using System.IO;

public class PvEGroup : ITransferable
{
	[NonSerialized]
	public PvEPhysics pveLeader;

	public long pveLeaderId;

	[NonSerialized]
	public PvEPhysics pveFrontLeft;

	public long pveFrontLeftId;

	[NonSerialized]
	public PvEPhysics pveFrontRight;

	public long pveFrontRightId;

	[NonSerialized]
	public PvEPhysics pveBackLeft;

	public long pveBackLeftId;

	[NonSerialized]
	public PvEPhysics pveBackRight;

	public long pveBackRightId;

	public PvEGroup()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.pveLeaderId = br.ReadInt64();
		this.pveFrontLeftId = br.ReadInt64();
		this.pveFrontRightId = br.ReadInt64();
		this.pveBackLeftId = br.ReadInt64();
		this.pveBackRightId = br.ReadInt64();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.pveLeaderId);
		bw.Write(this.pveFrontLeftId);
		bw.Write(this.pveFrontRightId);
		bw.Write(this.pveBackLeftId);
		bw.Write(this.pveBackRightId);
	}

	public enum PvEGroupPosition
	{
		Leader = 1,
		FrontLeft = 2,
		FrontRight = 3,
		BackLeft = 4,
		BackRight = 5
	}
}