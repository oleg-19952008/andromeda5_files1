using System;
using System.Collections.Generic;
using System.IO;

public class PlayerBasic : ITransferableInContext, ITransferable
{
	public string userName = "";

	public string nickName;

	public int dbId;

	public byte fractionId;

	public string guildName;

	public byte level;

	public byte accessLevel;

	public string avatarUrl;

	public DateTime lastOnlineTime;

	public bool isOnline;

	public bool isInParty;

	public long playId;

	public long xp;

	public int honor;

	public long playTime;

	public int storyQuestsDone;

	public int normalQuestsDone;

	public int dailyQuestsDone;

	public int achievementsDone;

	public int alienKills;

	public int playersKills;

	public byte[] achievements;

	public SortedList<int, GuildInvitation> guildInvitations = new SortedList<int, GuildInvitation>();

	public Guild guild;

	public GuildMember guildMembership;

	public PvPLeague pvpLeague;

	public int pvpGameWin;

	public int pvpGameLose;

	public int pvpHonorChange;

	public bool pvpFirstWinBonusRecived;

	public DateTime pvpLastGameTime;

	public PlayerBasic()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		throw new NotImplementedException();
	}

	public void DeserializeInContext(BinaryReader br, TransferContext context)
	{
		if (context != TransferContext.GuildInvitationsListToNonMember)
		{
			throw new NotImplementedException();
		}
		int num = br.ReadInt32();
		if (num != -1)
		{
			this.guildInvitations = new SortedList<int, GuildInvitation>(num);
			for (int i = 0; i < num; i++)
			{
				GuildInvitation guildInvitation = new GuildInvitation()
				{
					id = br.ReadInt32(),
					inviter = new PlayerBasic()
					{
						userName = br.ReadString()
					},
					timeExpire = DateTime.FromBinary(br.ReadInt64()),
					message = br.ReadString(),
					guild = new Guild()
					{
						name = br.ReadString(),
						upgradeOneLevel = br.ReadByte(),
						upgradeTwoLevel = br.ReadByte(),
						upgradeThreeLevel = br.ReadByte(),
						upgradeFourLevel = br.ReadByte(),
						upgradeFiveLevel = br.ReadByte()
					}
				};
			}
		}
		else
		{
			this.guildInvitations = null;
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		throw new NotImplementedException();
	}

	public void SerializeInContext(BinaryWriter bw, TransferContext context)
	{
		if (context != TransferContext.GuildInvitationsListToNonMember)
		{
			throw new NotImplementedException();
		}
		if (this.guildInvitations != null)
		{
			bw.Write(this.guildInvitations.Count);
			foreach (GuildInvitation value in this.guildInvitations.Values)
			{
				bw.Write(value.id);
				bw.Write(value.inviter.userName);
				bw.Write(value.timeExpire.Ticks);
				bw.Write(value.message);
				bw.Write(value.guild.name);
				bw.Write(value.guild.upgradeOneLevel);
				bw.Write(value.guild.upgradeTwoLevel);
				bw.Write(value.guild.upgradeThreeLevel);
				bw.Write(value.guild.upgradeFourLevel);
				bw.Write(value.guild.upgradeFiveLevel);
			}
		}
		else
		{
			bw.Write(-1);
		}
	}
}