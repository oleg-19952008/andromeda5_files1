using System;
using System.IO;

public class GuildSummary : ITransferable
{
	public int id;

	public string name = "";

	public string title = "";

	public int rank;

	public long experience;

	public string avatarUrl = "";

	public DateTime timeEstablished;

	public GuildSummary()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.id = br.ReadInt32();
		this.name = br.ReadString();
		this.title = br.ReadString();
		this.rank = br.ReadInt32();
		this.experience = br.ReadInt64();
		this.avatarUrl = br.ReadString();
		this.timeEstablished = DateTime.FromBinary(br.ReadInt64());
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.id);
		bw.Write(this.name);
		bw.Write(this.title);
		bw.Write(this.rank);
		bw.Write(this.experience);
		bw.Write(this.avatarUrl);
		bw.Write(this.timeEstablished.Ticks);
	}
}