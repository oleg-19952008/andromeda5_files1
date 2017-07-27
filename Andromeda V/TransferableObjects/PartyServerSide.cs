using System;
using System.Collections.Generic;

public class PartyServerSide
{
	public SortedList<int, PartyMemberServerSide> dbToPlayIdsMaping = new SortedList<int, PartyMemberServerSide>();

	public SortedList<int, PlayerData> partyMembersData = new SortedList<int, PlayerData>();

	public List<long> members = new List<long>();

	public PartyLootRules rules = PartyLootRules.FindersKeepers;

	public int id;

	private byte roundRobinIndex = 0;

	public short pvpGameTypeSignedFor = 0;

	private int sortIndex = 0;

	public PartyServerSide()
	{
	}
}