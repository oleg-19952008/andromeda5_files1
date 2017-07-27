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
				num = num + 256;
			}
			if (this.canPromote)
			{
				num = num + 128;
			}
			if (this.canEditDetails)
			{
				num = num + 64;
			}
			if (this.canVault)
			{
				num = num + 32;
			}
			if (this.canChat)
			{
				num = num + 16;
			}
			if (this.canBank)
			{
				num = num + 8;
			}
			if (this.canInvite)
			{
				num = num + 4;
			}
			return num;
		}
	}

	public GuildRank()
	{
	}
}