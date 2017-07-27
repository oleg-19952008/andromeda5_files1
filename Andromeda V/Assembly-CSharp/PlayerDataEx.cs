using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataEx : PlayerData
{
	[NonSerialized]
	public ShipScript shipScript;

	[NonSerialized]
	public GameObject gameObject;

	public SortedList<string, PlayerProfile> myFriends = new SortedList<string, PlayerProfile>();

	public SortedList<string, PlayerProfile> myBlacklist = new SortedList<string, PlayerProfile>();

	public List<GameMessage> myGameMessages = new List<GameMessage>();

	public PlayerProfile myPlayerProfileInfo;

	public PlayerDataEx()
	{
	}
}