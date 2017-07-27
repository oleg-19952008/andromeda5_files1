using System;
using System.IO;

public class ImAliveMessage : ITransferable
{
	public long playerId;

	public int mapIndex;

	public ImAliveMessage()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.playerId = br.ReadInt64();
		this.mapIndex = br.ReadInt32();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.playerId);
		bw.Write(this.mapIndex);
	}
}