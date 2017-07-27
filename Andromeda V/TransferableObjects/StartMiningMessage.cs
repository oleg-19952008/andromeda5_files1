using System;
using System.IO;

public class StartMiningMessage : ITransferable
{
	public long miningPlayerId;

	public uint mineralNbId;

	public StartMiningMessage()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.miningPlayerId = br.ReadInt64();
		this.mineralNbId = br.ReadUInt32();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.miningPlayerId);
		bw.Write(this.mineralNbId);
	}
}