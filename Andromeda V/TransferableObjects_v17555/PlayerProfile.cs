using System;
using System.IO;

public class PlayerProfile : ITransferable
{
	public byte friendState;

	public string userName;

	public byte fractionId;

	public string avatarUrl;

	public short galaxyId;

	public bool isOnline;

	public bool isInParty;

	public int expRank;

	public long honor;

	public long playTime;

	public DateTime lastPlayTimeUpdate;

	public int level;

	public byte accessLevel;

	public int questsDone;

	public int dailyDone;

	public int achievementsDone;

	public int alienKills;

	public int playersKills;

	public byte[] achievements;

	public PlayerProfile()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.userName = br.ReadString();
		this.fractionId = br.ReadByte();
		this.avatarUrl = br.ReadString();
		this.galaxyId = br.ReadInt16();
		this.isOnline = br.ReadBoolean();
		this.isInParty = br.ReadBoolean();
		this.expRank = br.ReadInt32();
		this.honor = br.ReadInt64();
		this.playTime = br.ReadInt64();
		this.level = br.ReadInt32();
		this.accessLevel = br.ReadByte();
		this.questsDone = br.ReadInt32();
		this.dailyDone = br.ReadInt32();
		this.achievementsDone = br.ReadInt32();
		this.alienKills = br.ReadInt32();
		this.playersKills = br.ReadInt32();
		int num = br.ReadInt32();
		if (num != -1)
		{
			this.achievements = br.ReadBytes(num);
		}
		else
		{
			this.achievements = null;
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.userName ?? "");
		bw.Write(this.fractionId);
		bw.Write(this.avatarUrl ?? "");
		bw.Write(this.galaxyId);
		bw.Write(this.isOnline);
		bw.Write(this.isInParty);
		bw.Write(this.expRank);
		bw.Write(this.honor);
		bw.Write(this.playTime);
		bw.Write(this.level);
		bw.Write(this.accessLevel);
		bw.Write(this.questsDone);
		bw.Write(this.dailyDone);
		bw.Write(this.achievementsDone);
		bw.Write(this.alienKills);
		bw.Write(this.playersKills);
		if (this.achievements != null)
		{
			bw.Write((int)this.achievements.Length);
			bw.Write(this.achievements, 0, (int)this.achievements.Length);
		}
		else
		{
			bw.Write(-1);
		}
	}
}