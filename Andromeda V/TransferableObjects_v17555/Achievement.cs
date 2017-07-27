using System;

public class Achievement
{
	public int id;

	public string name;

	public string description;

	public AchievementType type;

	public int[] levels;

	public ushort targetType;

	public static Achievement[] allAchievement;

	static Achievement()
	{
		Achievement[] achievementArray = new Achievement[22];
		Achievement achievement = new Achievement()
		{
			id = 1,
			targetType = PlayerItems.TypeAchievementSpaceDriller,
			name = "key_achievement_space_driller_name",
			description = "key_achievement_space_driller_dsc",
			type = AchievementType.General,
			levels = new int[] { 200, 1000, 5000, 25000, 125000 }
		};
		achievementArray[0] = achievement;
		Achievement achievement1 = new Achievement()
		{
			id = 2,
			targetType = PlayerItems.TypeAchievementVulture,
			name = "key_achievement_vulture_name",
			description = "key_achievement_vulture_dsc",
			type = AchievementType.General,
			levels = new int[] { 1000, 5000, 25000, 125000, 1000000 }
		};
		achievementArray[1] = achievement1;
		Achievement achievement2 = new Achievement()
		{
			id = 3,
			targetType = PlayerItems.TypeAchievementBotKiller,
			name = "key_achievement_bot_killer_name",
			description = "key_achievement_bot_killer_dsc",
			type = AchievementType.General,
			levels = new int[] { 50, 100, 250, 1000, 5000 }
		};
		achievementArray[2] = achievement2;
		Achievement achievement3 = new Achievement()
		{
			id = 4,
			targetType = PlayerItems.TypeAchievementMerchant,
			name = "key_achievement_merchant_name",
			description = "key_achievement_merchant_dsc",
			type = AchievementType.General,
			levels = new int[] { 1000, 5000, 25000, 125000, 1000000 }
		};
		achievementArray[3] = achievement3;
		Achievement achievement4 = new Achievement()
		{
			id = 5,
			targetType = PlayerItems.TypeAchievementAlchemist,
			name = "key_achievement_alchemist_name",
			description = "key_achievement_alchemist_dsc",
			type = AchievementType.General,
			levels = new int[] { 200, 1000, 5000, 25000, 125000 }
		};
		achievementArray[4] = achievement4;
		Achievement achievement5 = new Achievement()
		{
			id = 6,
			targetType = PlayerItems.TypeAchievementAmmunition,
			name = "key_achievement_ammunition_name",
			description = "key_achievement_ammunition_dsc",
			type = AchievementType.General,
			levels = new int[] { 40, 100, 250, 1000, 5000 }
		};
		achievementArray[5] = achievement5;
		Achievement achievement6 = new Achievement()
		{
			id = 7,
			targetType = PlayerItems.TypeAchievementVeteran,
			name = "key_achievement_veteran_name",
			description = "key_achievement_veteran_dsc",
			type = AchievementType.General,
			levels = new int[] { 10, 20, 30, 40, 50 }
		};
		achievementArray[6] = achievement6;
		Achievement achievement7 = new Achievement()
		{
			id = 8,
			targetType = PlayerItems.TypeAchievementAce,
			name = "key_achievement_ace_name",
			description = "key_achievement_ace_dsc",
			type = AchievementType.General,
			levels = new int[] { 3, 6, 9, 12, 15 }
		};
		achievementArray[7] = achievement7;
		Achievement achievement8 = new Achievement()
		{
			id = 9,
			targetType = PlayerItems.TypeAchievementAviator,
			name = "key_achievement_aviator_name",
			description = "key_achievement_aviator_dsc",
			type = AchievementType.General,
			levels = new int[] { 2, 4, 6, 8, 10 }
		};
		achievementArray[8] = achievement8;
		Achievement achievement9 = new Achievement()
		{
			id = 10,
			targetType = PlayerItems.TypeAchievementEngineer,
			name = "key_achievement_engineer_name",
			description = "key_achievement_engineer_dsc",
			type = AchievementType.General,
			levels = new int[] { 2, 4, 7, 10, 15 }
		};
		achievementArray[9] = achievement9;
		Achievement achievement10 = new Achievement()
		{
			id = 23,
			targetType = PlayerItems.TypeAchievementWarMaster,
			name = "key_achievement_soldier_of_fortune_name",
			description = "key_achievement_soldier_of_fortune_faction_war_dsc",
			type = AchievementType.General,
			levels = new int[] { 5, 10, 25, 100, 500 }
		};
		achievementArray[10] = achievement10;
		Achievement achievement11 = new Achievement()
		{
			id = 12,
			targetType = PlayerItems.TypeAchievementSoldierOfFortune,
			name = "key_achievement_adventurer_name",
			description = "key_achievement_soldier_of_fortune_dsc",
			type = AchievementType.General,
			levels = new int[] { 5, 25, 50, 10, 250 }
		};
		achievementArray[11] = achievement11;
		Achievement achievement12 = new Achievement()
		{
			id = 13,
			targetType = PlayerItems.TypeAchievementGoldDigger,
			name = "key_achievement_gold_digger_name",
			description = "key_achievement_gold_digger_dsc",
			type = AchievementType.General,
			levels = new int[] { 5, 20, 100, 500, 1000 }
		};
		achievementArray[12] = achievement12;
		Achievement achievement13 = new Achievement()
		{
			id = 14,
			targetType = PlayerItems.TypeAchievementAristocrate,
			name = "key_achievement_aristocrate_name",
			description = "key_achievement_aristocrate_dsc",
			type = AchievementType.Nova,
			levels = new int[] { 500, 1500, 5000, 20000, 100000 }
		};
		achievementArray[13] = achievement13;
		Achievement achievement14 = new Achievement()
		{
			id = 15,
			targetType = PlayerItems.TypeAchievementInvestor,
			name = "key_achievement_investor_name",
			description = "key_achievement_investor_dsc",
			type = AchievementType.Nova,
			levels = new int[] { 5, 10, 20, 30, 50 }
		};
		achievementArray[14] = achievement14;
		Achievement achievement15 = new Achievement()
		{
			id = 16,
			targetType = PlayerItems.TypeAchievementRational,
			name = "key_achievement_rational_name",
			description = "key_achievement_rational_dsc",
			type = AchievementType.Nova,
			levels = new int[] { 5, 10, 20, 30, 50 }
		};
		achievementArray[15] = achievement15;
		Achievement achievement16 = new Achievement()
		{
			id = 17,
			targetType = PlayerItems.TypeAchievementNeat,
			name = "key_achievement_neat_name",
			description = "key_achievement_neat_dsc",
			type = AchievementType.Nova,
			levels = new int[] { 16, 20, 24, 28, 32 }
		};
		achievementArray[16] = achievement16;
		Achievement achievement17 = new Achievement()
		{
			id = 18,
			targetType = PlayerItems.TypeAchievementStoreKeeper,
			name = "key_achievement_store_keeper_name",
			description = "key_achievement_store_keeper_dsc",
			type = AchievementType.Nova,
			levels = new int[] { 16, 20, 24, 28, 32 }
		};
		achievementArray[17] = achievement17;
		Achievement achievement18 = new Achievement()
		{
			id = 19,
			targetType = PlayerItems.TypeAchievementInsecure,
			name = "key_achievement_insecure_name",
			description = "key_achievement_insecure_dsc",
			type = AchievementType.Nova,
			levels = new int[] { 1, 3, 5, 10, 20 }
		};
		achievementArray[18] = achievement18;
		Achievement achievement19 = new Achievement()
		{
			id = 20,
			targetType = PlayerItems.TypeAchievementTrueGrit,
			name = "key_achievement_true_grit_name",
			description = "key_achievement_true_grit_dsc",
			type = AchievementType.PvP,
			levels = new int[] { 10, 20, 50, 250, 1000 }
		};
		achievementArray[19] = achievement19;
		Achievement achievement20 = new Achievement()
		{
			id = 21,
			targetType = PlayerItems.TypeAchievementAssassin,
			name = "key_achievement_assassin_name",
			description = "key_achievement_assassin_dsc",
			type = AchievementType.PvP,
			levels = new int[] { 10, 20, 50, 250, 1000 }
		};
		achievementArray[20] = achievement20;
		Achievement achievement21 = new Achievement()
		{
			id = 22,
			targetType = PlayerItems.TypeAchievementGladiator,
			name = "key_achievement_gladiator_name",
			description = "key_achievement_gladiator_domination_dsc",
			type = AchievementType.PvP,
			levels = new int[] { 5, 10, 25, 100, 500 }
		};
		achievementArray[21] = achievement21;
		Achievement.allAchievement = achievementArray;
	}

	public Achievement()
	{
	}
}