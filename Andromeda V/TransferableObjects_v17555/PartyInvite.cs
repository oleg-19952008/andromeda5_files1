using System;
using System.IO;

public class PartyInvite : ITransferable
{
	public DateTime timeToDie;

	public int timeToLive;

	public long player;

	public int level;

	public string name;

	public string avatarUrl;

	public object object1;

	public object window;

	public PartyInvite()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.timeToLive = br.ReadInt32();
		this.timeToDie = StaticData.now.AddMilliseconds((double)this.timeToLive);
		this.player = br.ReadInt64();
		this.level = br.ReadInt32();
		this.name = br.ReadString();
		this.avatarUrl = br.ReadString();
	}

	public void Serialize(BinaryWriter bw)
	{
		TimeSpan timeSpan = this.timeToDie - StaticData.now;
		this.timeToLive = (int)timeSpan.TotalMilliseconds;
		if (this.timeToLive < 0)
		{
			this.timeToLive = 1;
		}
		bw.Write(this.timeToLive);
		bw.Write(this.player);
		bw.Write(this.level);
		bw.Write(this.name);
		bw.Write(this.avatarUrl);
	}
}