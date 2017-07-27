using System;
using System.IO;

public class StartMiningData : ITransferable
{
	public long miningPlayer;

	public long mineralId;

	public StartMiningData()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.miningPlayer = br.ReadInt64();
		this.mineralId = br.ReadInt64();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.miningPlayer);
		bw.Write(this.mineralId);
	}
}