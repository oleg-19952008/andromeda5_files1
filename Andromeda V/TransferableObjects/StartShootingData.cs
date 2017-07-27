using System;
using System.IO;

public class StartShootingData : ITransferable
{
	public long shooterPlayerId;

	public long targetPlayerId;

	public uint targetNbId;

	public byte targetType;

	public StartShootingData()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.shooterPlayerId = br.ReadInt64();
		this.targetPlayerId = br.ReadInt64();
		this.targetNbId = br.ReadUInt32();
		this.targetType = br.ReadByte();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.shooterPlayerId);
		bw.Write(this.targetPlayerId);
		bw.Write(this.targetNbId);
		bw.Write(this.targetType);
	}
}