using System;
using System.IO;

public class RankingData : ITransferable
{
	public int key;

	public int period;

	public int allDataRows;

	public short rowsPerPage;

	public int firstPosition;

	public NameValuePair[] data;

	public byte trackType;

	public int trackIndex;

	public PvPLeagueMemberInfo[] leagueInfo;

	public RankingData()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		int i;
		this.key = br.ReadInt32();
		this.period = br.ReadInt32();
		short num = br.ReadInt16();
		this.data = new NameValuePair[num];
		for (i = 0; i < num; i++)
		{
			this.data[i] = new NameValuePair();
			this.data[i].name = br.ReadString();
			this.data[i].val = br.ReadInt64();
			this.data[i].fractionId = br.ReadByte();
			this.data[i].guildName = br.ReadString();
			this.data[i].isOnline = br.ReadBoolean();
			this.data[i].level = br.ReadByte();
		}
		this.rowsPerPage = br.ReadInt16();
		this.firstPosition = br.ReadInt32();
		this.trackType = br.ReadByte();
		this.trackIndex = br.ReadInt32();
		this.allDataRows = br.ReadInt32();
		int num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.leagueInfo = new PvPLeagueMemberInfo[num1];
			for (i = 0; i < num1; i++)
			{
				this.leagueInfo[i] = new PvPLeagueMemberInfo();
				this.leagueInfo[i].playerName = br.ReadString();
				this.leagueInfo[i].playerLevel = br.ReadByte();
				this.leagueInfo[i].guildName = br.ReadString();
				this.leagueInfo[i].fractionId = br.ReadByte();
				this.leagueInfo[i].isOnline = br.ReadBoolean();
				this.leagueInfo[i].gameWin = br.ReadInt32();
				this.leagueInfo[i].gameLose = br.ReadInt32();
				this.leagueInfo[i].honorGained = br.ReadInt32();
			}
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		int i;
		bw.Write(this.key);
		bw.Write(this.period);
		short length = (short)((int)this.data.Length);
		bw.Write(length);
		for (i = 0; i < length; i++)
		{
			bw.Write(this.data[i].name);
			bw.Write(this.data[i].val);
			bw.Write(this.data[i].fractionId);
			bw.Write(this.data[i].guildName ?? "");
			bw.Write(this.data[i].isOnline);
			bw.Write(this.data[i].level);
		}
		bw.Write(this.rowsPerPage);
		bw.Write(this.firstPosition);
		bw.Write(this.trackType);
		bw.Write(this.trackIndex);
		bw.Write(this.allDataRows);
		if (this.leagueInfo != null)
		{
			int num = (int)this.leagueInfo.Length;
			bw.Write(num);
			for (i = 0; i < num; i++)
			{
				bw.Write(this.leagueInfo[i].playerName);
				bw.Write(this.leagueInfo[i].playerLevel);
				bw.Write(this.leagueInfo[i].guildName);
				bw.Write(this.leagueInfo[i].fractionId);
				bw.Write(this.leagueInfo[i].isOnline);
				bw.Write(this.leagueInfo[i].gameWin);
				bw.Write(this.leagueInfo[i].gameLose);
				bw.Write(this.leagueInfo[i].honorGained);
			}
		}
		else
		{
			bw.Write(-1);
		}
	}
}