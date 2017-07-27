using System;
using System.IO;

public class AddPlr2UniverseRequest : ITransferable
{
	public long playerId;

	public int dbId;

	public int loginId;

	public short galaxyId;

	public bool isInBase;

	public AddPlr2UniverseRequest()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.playerId = br.ReadInt64();
		this.dbId = br.ReadInt32();
		this.loginId = br.ReadInt32();
		this.galaxyId = br.ReadInt16();
		this.isInBase = br.ReadBoolean();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.playerId);
		bw.Write(this.dbId);
		bw.Write(this.loginId);
		bw.Write(this.galaxyId);
		bw.Write(this.isInBase);
	}
}