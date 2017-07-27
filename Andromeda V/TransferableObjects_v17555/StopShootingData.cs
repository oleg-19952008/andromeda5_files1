using System;
using System.IO;

public class StopShootingData : ITransferable
{
	public long shooterPlayerId;

	public uint shooterPveNbId;

	public StopShootingData()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.shooterPlayerId = br.ReadInt64();
		this.shooterPveNbId = br.ReadUInt32();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.shooterPlayerId);
		bw.Write(this.shooterPveNbId);
	}
}