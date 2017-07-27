using System;

public class PvPStatRow
{
	public long playerId;

	public string playerName;

	public string guildName;

	public PvPPlayerState state;

	public short honor;

	public short kills;

	public byte teamNumber;

	public byte fractionId;

	public short honorChange;

	public int rewardCash;

	public int rewardXp;

	public int rewardUltralibrium;

	public byte aliensKilded;

	public short progressPointGaine;

	public PvPStatRow()
	{
	}
}