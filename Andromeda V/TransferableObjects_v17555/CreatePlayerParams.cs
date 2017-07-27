using System;
using System.IO;

public class CreatePlayerParams : ITransferable
{
	public string shipName;

	public int raceId;

	public int universeId;

	public long playId;

	[NonSerialized]
	public int loginID;

	public CreatePlayerParams()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.shipName = br.ReadString();
		this.raceId = br.ReadInt32();
		this.universeId = br.ReadInt32();
		this.playId = br.ReadInt64();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.shipName);
		bw.Write(this.raceId);
		bw.Write(this.universeId);
		bw.Write(this.playId);
	}
}