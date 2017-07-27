using System;
using System.IO;

public class HyperJumpParams : ITransferable
{
	public int hyperJumpId;

	public long jumpingPlayerId;

	public bool isGalaxyJump;

	public InstanceDifficulty difficulty = InstanceDifficulty.Normal;

	public HyperJumpParams()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.hyperJumpId = br.ReadInt32();
		this.jumpingPlayerId = br.ReadInt64();
		this.isGalaxyJump = br.ReadBoolean();
		this.difficulty = (InstanceDifficulty)br.ReadByte();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.hyperJumpId);
		bw.Write(this.jumpingPlayerId);
		bw.Write(this.isGalaxyJump);
		bw.Write((byte)this.difficulty);
	}
}