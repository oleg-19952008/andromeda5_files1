using System;
using System.IO;

public class PartyMemberClientSide : ITransferable
{
	public string playerName;

	public short playerLevel;

	public short galaxyId;

	public uint neighborhoodId;

	public long playerId;

	public string avatarUrl;

	public int corpus;

	public int corpusMax;

	public int shield;

	public int shieldMax;

	public int honor;

	public object barCorpus;

	public object barShield;

	public object labelCorpus;

	public object labelShield;

	public object newShiel;

	public object newCorpus;

	public PartyMemberStatus status = PartyMemberStatus.OK;

	public PartyMemberClientSide()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.playerName = br.ReadString();
		this.playerLevel = br.ReadInt16();
		this.galaxyId = br.ReadInt16();
		this.neighborhoodId = br.ReadUInt32();
		this.playerId = br.ReadInt64();
		this.avatarUrl = br.ReadString();
		this.corpus = br.ReadInt32();
		this.corpusMax = br.ReadInt32();
		this.shield = br.ReadInt32();
		this.shieldMax = br.ReadInt32();
		this.honor = br.ReadInt32();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.playerName);
		bw.Write(this.playerLevel);
		bw.Write(this.galaxyId);
		bw.Write(this.neighborhoodId);
		bw.Write(this.playerId);
		bw.Write(this.avatarUrl);
		bw.Write(this.corpus);
		bw.Write(this.corpusMax);
		bw.Write(this.shield);
		bw.Write(this.shieldMax);
		bw.Write(this.honor);
	}
}