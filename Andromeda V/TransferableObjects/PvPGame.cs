using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

public class PvPGame : ITransferable
{
	public byte winTeam;

	public PvPGameType gameType;

	public PvPGame.PvPGameState state;

	public DateTime destroyTime;

	public DateTime startTime;

	public DateTime freezeUntilTime;

	public List<PvPStatRow> stats = new List<PvPStatRow>();

	public int teamOneScore;

	public int teamTwoScore;

	public int pvpDominationGameLevel;

	public PvPGame()
	{
	}

	public virtual void Deserialize(BinaryReader br)
	{
		short num = br.ReadInt16();
		this.gameType = (
			from w in StaticData.pvpGameTypes
			where w.id == num
			select w).First<PvPGameType>();
		DateTime now = DateTime.Now;
		this.startTime = now.AddMilliseconds((double)br.ReadInt32());
		now = DateTime.Now;
		this.destroyTime = now.AddMilliseconds((double)br.ReadInt32());
		now = DateTime.Now;
		this.freezeUntilTime = now.AddMilliseconds((double)br.ReadInt32());
		this.state = (PvPGame.PvPGameState)br.ReadByte();
		this.winTeam = br.ReadByte();
		int num1 = br.ReadInt32();
		for (int i = 0; i < num1; i++)
		{
			PvPStatRow pvPStatRow = new PvPStatRow()
			{
				honor = br.ReadInt16(),
				state = (PvPPlayerState)br.ReadByte(),
				kills = br.ReadInt16(),
				playerId = br.ReadInt64(),
				playerName = br.ReadString(),
				teamNumber = br.ReadByte(),
				fractionId = br.ReadByte(),
				guildName = br.ReadString(),
				honorChange = br.ReadInt16(),
				rewardCash = br.ReadInt32(),
				rewardUltralibrium = br.ReadInt32(),
				rewardXp = br.ReadInt32(),
				aliensKilded = br.ReadByte(),
				progressPointGaine = br.ReadInt16()
			};
			this.stats.Add(pvPStatRow);
		}
		short num2 = br.ReadInt16();
		if (num2 != -1)
		{
			this.gameType.selectedMap = new PvPMap()
			{
				galaxyId = num2,
				pvpGameId = this.gameType.id
			};
		}
		this.teamOneScore = br.ReadInt32();
		this.teamTwoScore = br.ReadInt32();
	}

	public virtual void Serialize(BinaryWriter bw)
	{
	}

	public enum PvPGameState
	{
		Countdown,
		Playing,
		Finished
	}
}