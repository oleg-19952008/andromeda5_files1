using System;
using System.IO;

public class StopMiningData : ITransferable
{
	public long miningPlayer;

	public long mineralId;

	public bool finished;

	public StopMiningData()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.miningPlayer = br.ReadInt64();
		this.mineralId = br.ReadInt64();
		this.finished = br.ReadBoolean();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.miningPlayer);
		bw.Write(this.mineralId);
		bw.Write(this.finished);
	}
}