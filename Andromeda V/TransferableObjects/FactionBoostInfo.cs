using System;
using System.Collections.Generic;

public class FactionBoostInfo
{
	public byte boostId;

	public bool isBattle;

	public string iconAssetName;

	public int price;

	public float posX;

	public float posY;

	public static SortedDictionary<byte, FactionBoostInfo> battleBoosts;

	public static SortedDictionary<byte, FactionBoostInfo> utilityBoosts;

	public string boostInfoText
	{
		get
		{
			string str;
			byte num = this.boostId;
			switch (num)
			{
				case 101:
				{
					str = string.Format(StaticData.Translate("key_faction_war_boost_battle_1_info"), 20f);
					break;
				}
				case 102:
				{
					str = string.Format(StaticData.Translate("key_faction_war_boost_battle_2_info"), 20f);
					break;
				}
				case 103:
				{
					str = string.Format(StaticData.Translate("key_faction_war_boost_battle_3_info"), 20f);
					break;
				}
				default:
				{
					switch (num)
					{
						case 201:
						{
							str = string.Format(StaticData.Translate("key_faction_war_boost_utility_1_info"), 50f, 25f);
							break;
						}
						case 202:
						{
							str = string.Format(StaticData.Translate("key_faction_war_boost_utility_2_info"), 20f, 10f);
							break;
						}
						case 203:
						{
							str = string.Format(StaticData.Translate("key_faction_war_boost_utility_3_info"), 10f);
							break;
						}
						default:
						{
							str = "";
							break;
						}
					}
					break;
				}
			}
			return str;
		}
	}

	static FactionBoostInfo()
	{
		FactionBoostInfo.battleBoosts = new SortedDictionary<byte, FactionBoostInfo>();
		FactionBoostInfo.utilityBoosts = new SortedDictionary<byte, FactionBoostInfo>();
		FactionBoostInfo factionBoostInfo = new FactionBoostInfo()
		{
			boostId = 101,
			isBattle = true,
			iconAssetName = "boosterPvP",
			price = 200000000,
			posX = 225f,
			posY = 207f
		};
		FactionBoostInfo factionBoostInfo1 = factionBoostInfo;
		FactionBoostInfo factionBoostInfo2 = new FactionBoostInfo()
		{
			boostId = 102,
			isBattle = true,
			iconAssetName = "boosterResilience",
			price = 240000000,
			posX = 225f,
			posY = 297f
		};
		FactionBoostInfo factionBoostInfo3 = factionBoostInfo2;
		FactionBoostInfo factionBoostInfo4 = new FactionBoostInfo()
		{
			boostId = 103,
			isBattle = true,
			iconAssetName = "boosterPvE",
			price = 200000000,
			posX = 225f,
			posY = 387f
		};
		FactionBoostInfo factionBoostInfo5 = factionBoostInfo4;
		FactionBoostInfo factionBoostInfo6 = new FactionBoostInfo()
		{
			boostId = 201,
			isBattle = false,
			iconAssetName = "boosterMining",
			price = 240000000,
			posX = 570f,
			posY = 207f
		};
		FactionBoostInfo factionBoostInfo7 = factionBoostInfo6;
		FactionBoostInfo factionBoostInfo8 = new FactionBoostInfo()
		{
			boostId = 202,
			isBattle = false,
			iconAssetName = "boosterFusion",
			price = 200000000,
			posX = 570f,
			posY = 297f
		};
		FactionBoostInfo factionBoostInfo9 = factionBoostInfo8;
		FactionBoostInfo factionBoostInfo10 = new FactionBoostInfo()
		{
			boostId = 203,
			isBattle = false,
			iconAssetName = "boosterIncome",
			price = 200000000,
			posX = 570f,
			posY = 387f
		};
		FactionBoostInfo factionBoostInfo11 = factionBoostInfo10;
		FactionBoostInfo.battleBoosts.Add(factionBoostInfo1.boostId, factionBoostInfo1);
		FactionBoostInfo.battleBoosts.Add(factionBoostInfo3.boostId, factionBoostInfo3);
		FactionBoostInfo.battleBoosts.Add(factionBoostInfo5.boostId, factionBoostInfo5);
		FactionBoostInfo.utilityBoosts.Add(factionBoostInfo7.boostId, factionBoostInfo7);
		FactionBoostInfo.utilityBoosts.Add(factionBoostInfo9.boostId, factionBoostInfo9);
		FactionBoostInfo.utilityBoosts.Add(factionBoostInfo11.boostId, factionBoostInfo11);
	}

	public FactionBoostInfo()
	{
	}
}