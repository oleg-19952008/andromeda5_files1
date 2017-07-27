using System;

public class GuildRank
{
	public int id;

	public int guildId;

	public string name = "";

	public bool canInvite;

	public bool canPromote;

	public bool isMaster;

	public bool canVault;

	public bool canChat;

	public bool canBank;

	public bool canEditDetails;

	public short sortIndex = 100;

	public short avatarIndex;

	public int RankIndex
	{
		get
		{
			int num = 0;
			if (this.isMaster)
			{
				num += 256;
			}
			if (this.canPromote)
			{
				num += 128;
			}
			if (this.canEditDetails)
			{
				num += 64;
			}
			if (this.canVault)
			{
				num += 32;
			}
			if (this.canChat)
			{
				num += 16;
			}
			if (this.canBank)
			{
				num += 8;
			}
			if (this.canInvite)
			{
				num += 4;
			}
			return num;
		}
	}

	public GuildRank()
	{
	}
}