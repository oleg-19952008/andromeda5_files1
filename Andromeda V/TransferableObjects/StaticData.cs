using System;
using System.Collections.Generic;
using System.IO;

public class StaticData : ITransferable
{
	public const int PVP_NO_ACTIVITY_GRATIS_DAY = 5;

	public const int MENU_ACCESS_LEVEL_PVP = 8;

	public const int MENU_ACCESS_LEVEL_TRANSFORMER = 30;

	public const int MENU_ACCESS_LEVEL_GUILD = 9;

	public const int MENU_ACCESS_LEVEL_UNIVERS_MAP = 7;

	public const int MENU_ACCESS_LEVEL_SEND_GIFTS = 10;

	public const int MENU_ACCESS_LEVEL_FACTION_WAR = 10;

	public const int CHANGE_NICKNAME_NOVA_PRICE = 1000;

	public const int CHANGE_NICKNAME_EQ_PRICE = 2000;

	private const int CHANGE_FACTION_NOVA_PRICE_LEVEL_0 = 50;

	private const int CHANGE_FACTION_NOVA_PRICE_LEVEL_10 = 2500;

	private const int CHANGE_FACTION_NOVA_PRICE_LEVEL_20 = 7500;

	private const int CHANGE_FACTION_NOVA_PRICE_LEVEL_30 = 25000;

	private const int CHANGE_FACTION_NOVA_PRICE_LEVEL_40 = 25000;

	private const int CHANGE_FACTION_NOVA_PRICE_LEVEL_50 = 25000;

	public const short build = 164;

	public const int ALL_DAILY_MISSIONS_ULTRALIBRIUM_REWARD = 25;

	public static int PVP_WIN_GAME_ULTRALIBRIUM;

	public static int PVP_RETREAT_GAME_HONOR;

	public static int LEVEL_UP_HONOR_REWARD;

	public static int NEW_PLAYER_STARTING_HONOR;

	public static SortedList<PvPLeague, PvPLeageReward[]> pvpRewards;

	public static SortedList<PowerUpCategory, PowerUpInfo[]> powerUps;

	public static FreeGift[] freeGifts;

	public static Dictionary<int, int> poiStartBattleTimeSchedule;

	public static DateTime now;

	public static short serverBuild;

	public static SortedList<string, string> languages;

	public static ShipsTypeNet[] shipTypes;

	public static PvEInfo[] pveTypes;

	public static SortedList<int, LevelsInfo> levelsType;

	public static SlotPriceInfo[] slotPriceInformation;

	public static GalaxiesJumpMap[] galaxyJumpsPrice;

	public static LevelMap[] allGalaxies;

	public static SortedList<ushort, PlayerItemTypesData> allTypes;

	public static NpcObjectPhysics[] allNPCs;

	public static CheckpointObjectPhysics[] allCheckpoints;

	public static PvPGameType[] pvpGameTypes;

	public static ExtractionPointInfo[] allExtractionPoints;

	public static List<GuildUpgrade> guildUpgradesInfo;

	public static Portal[] allPortals;

	public static List<ushort> transformersPersonalRewards;

	public static List<ushort> transformersGuildRewards;

	public static StarBaseItemDistribution[] starBaseItemsDisribution;

	public static NewQuest[] allQuests;

	public static NewQuest[] allDailyQuests;

	public static int firstQuestId;

	public static short questsCount;

	public static byte maxAccessLevel;

	public static SortedList<string, string> translations;

	private static object loadLocker;

	static StaticData()
	{
		StaticData.PVP_WIN_GAME_ULTRALIBRIUM = 2;
		StaticData.PVP_RETREAT_GAME_HONOR = -3;
		StaticData.LEVEL_UP_HONOR_REWARD = 50;
		StaticData.NEW_PLAYER_STARTING_HONOR = 200;
		StaticData.now = DateTime.Now;
		StaticData.serverBuild = 0;
		StaticData.allDailyQuests = new NewQuest[0];
		StaticData.translations = new SortedList<string, string>();
		StaticData.loadLocker = new object();
		StaticData.pvpRewards = new SortedList<PvPLeague, PvPLeageReward[]>();
		PvPLeageReward[] pvPLeageRewardArray = new PvPLeageReward[10];
		PvPLeageReward pvPLeageReward = new PvPLeageReward()
		{
			league = PvPLeague.Bronze,
			rank = 1,
			rewardAmount = 87,
			rewardType = PlayerItems.TypeEquilibrium
		};
		pvPLeageRewardArray[0] = pvPLeageReward;
		PvPLeageReward pvPLeageReward1 = new PvPLeageReward()
		{
			league = PvPLeague.Bronze,
			rank = 2,
			rewardAmount = 62,
			rewardType = PlayerItems.TypeEquilibrium
		};
		pvPLeageRewardArray[1] = pvPLeageReward1;
		PvPLeageReward pvPLeageReward2 = new PvPLeageReward()
		{
			league = PvPLeague.Bronze,
			rank = 3,
			rewardAmount = 37,
			rewardType = PlayerItems.TypeEquilibrium
		};
		pvPLeageRewardArray[2] = pvPLeageReward2;
		PvPLeageReward pvPLeageReward3 = new PvPLeageReward()
		{
			league = PvPLeague.Bronze,
			rank = 4,
			rewardAmount = 25,
			rewardType = PlayerItems.TypeEquilibrium
		};
		pvPLeageRewardArray[3] = pvPLeageReward3;
		PvPLeageReward pvPLeageReward4 = new PvPLeageReward()
		{
			league = PvPLeague.Bronze,
			rank = 5,
			rewardAmount = 12,
			rewardType = PlayerItems.TypeEquilibrium
		};
		pvPLeageRewardArray[4] = pvPLeageReward4;
		PvPLeageReward pvPLeageReward5 = new PvPLeageReward()
		{
			league = PvPLeague.Bronze,
			rank = 6,
			rewardAmount = 5,
			rewardType = PlayerItems.TypeEquilibrium
		};
		pvPLeageRewardArray[5] = pvPLeageReward5;
		PvPLeageReward pvPLeageReward6 = new PvPLeageReward()
		{
			league = PvPLeague.Bronze,
			rank = 7,
			rewardAmount = 5,
			rewardType = PlayerItems.TypeEquilibrium
		};
		pvPLeageRewardArray[6] = pvPLeageReward6;
		PvPLeageReward pvPLeageReward7 = new PvPLeageReward()
		{
			league = PvPLeague.Bronze,
			rank = 8,
			rewardAmount = 5,
			rewardType = PlayerItems.TypeEquilibrium
		};
		pvPLeageRewardArray[7] = pvPLeageReward7;
		PvPLeageReward pvPLeageReward8 = new PvPLeageReward()
		{
			league = PvPLeague.Bronze,
			rank = 9,
			rewardAmount = 5,
			rewardType = PlayerItems.TypeEquilibrium
		};
		pvPLeageRewardArray[8] = pvPLeageReward8;
		PvPLeageReward pvPLeageReward9 = new PvPLeageReward()
		{
			league = PvPLeague.Bronze,
			rank = 10,
			rewardAmount = 5,
			rewardType = PlayerItems.TypeEquilibrium
		};
		pvPLeageRewardArray[9] = pvPLeageReward9;
		PvPLeageReward[] pvPLeageRewardArray1 = new PvPLeageReward[10];
		PvPLeageReward pvPLeageReward10 = new PvPLeageReward()
		{
			league = PvPLeague.Silver,
			rank = 1,
			rewardAmount = 210,
			rewardType = PlayerItems.TypeEquilibrium
		};
		pvPLeageRewardArray1[0] = pvPLeageReward10;
		PvPLeageReward pvPLeageReward11 = new PvPLeageReward()
		{
			league = PvPLeague.Silver,
			rank = 2,
			rewardAmount = 150,
			rewardType = PlayerItems.TypeEquilibrium
		};
		pvPLeageRewardArray1[1] = pvPLeageReward11;
		PvPLeageReward pvPLeageReward12 = new PvPLeageReward()
		{
			league = PvPLeague.Silver,
			rank = 3,
			rewardAmount = 90,
			rewardType = PlayerItems.TypeEquilibrium
		};
		pvPLeageRewardArray1[2] = pvPLeageReward12;
		PvPLeageReward pvPLeageReward13 = new PvPLeageReward()
		{
			league = PvPLeague.Silver,
			rank = 4,
			rewardAmount = 60,
			rewardType = PlayerItems.TypeEquilibrium
		};
		pvPLeageRewardArray1[3] = pvPLeageReward13;
		PvPLeageReward pvPLeageReward14 = new PvPLeageReward()
		{
			league = PvPLeague.Silver,
			rank = 5,
			rewardAmount = 30,
			rewardType = PlayerItems.TypeEquilibrium
		};
		pvPLeageRewardArray1[4] = pvPLeageReward14;
		PvPLeageReward pvPLeageReward15 = new PvPLeageReward()
		{
			league = PvPLeague.Silver,
			rank = 6,
			rewardAmount = 12,
			rewardType = PlayerItems.TypeEquilibrium
		};
		pvPLeageRewardArray1[5] = pvPLeageReward15;
		PvPLeageReward pvPLeageReward16 = new PvPLeageReward()
		{
			league = PvPLeague.Silver,
			rank = 7,
			rewardAmount = 12,
			rewardType = PlayerItems.TypeEquilibrium
		};
		pvPLeageRewardArray1[6] = pvPLeageReward16;
		PvPLeageReward pvPLeageReward17 = new PvPLeageReward()
		{
			league = PvPLeague.Silver,
			rank = 8,
			rewardAmount = 12,
			rewardType = PlayerItems.TypeEquilibrium
		};
		pvPLeageRewardArray1[7] = pvPLeageReward17;
		PvPLeageReward pvPLeageReward18 = new PvPLeageReward()
		{
			league = PvPLeague.Silver,
			rank = 9,
			rewardAmount = 12,
			rewardType = PlayerItems.TypeEquilibrium
		};
		pvPLeageRewardArray1[8] = pvPLeageReward18;
		PvPLeageReward pvPLeageReward19 = new PvPLeageReward()
		{
			league = PvPLeague.Silver,
			rank = 10,
			rewardAmount = 12,
			rewardType = PlayerItems.TypeEquilibrium
		};
		pvPLeageRewardArray1[9] = pvPLeageReward19;
		PvPLeageReward[] pvPLeageRewardArray2 = new PvPLeageReward[10];
		PvPLeageReward pvPLeageReward20 = new PvPLeageReward()
		{
			league = PvPLeague.Gold,
			rank = 1,
			rewardAmount = 350,
			rewardType = PlayerItems.TypeNova
		};
		pvPLeageRewardArray2[0] = pvPLeageReward20;
		PvPLeageReward pvPLeageReward21 = new PvPLeageReward()
		{
			league = PvPLeague.Gold,
			rank = 2,
			rewardAmount = 250,
			rewardType = PlayerItems.TypeNova
		};
		pvPLeageRewardArray2[1] = pvPLeageReward21;
		PvPLeageReward pvPLeageReward22 = new PvPLeageReward()
		{
			league = PvPLeague.Gold,
			rank = 3,
			rewardAmount = 150,
			rewardType = PlayerItems.TypeNova
		};
		pvPLeageRewardArray2[2] = pvPLeageReward22;
		PvPLeageReward pvPLeageReward23 = new PvPLeageReward()
		{
			league = PvPLeague.Gold,
			rank = 4,
			rewardAmount = 100,
			rewardType = PlayerItems.TypeNova
		};
		pvPLeageRewardArray2[3] = pvPLeageReward23;
		PvPLeageReward pvPLeageReward24 = new PvPLeageReward()
		{
			league = PvPLeague.Gold,
			rank = 5,
			rewardAmount = 50,
			rewardType = PlayerItems.TypeNova
		};
		pvPLeageRewardArray2[4] = pvPLeageReward24;
		PvPLeageReward pvPLeageReward25 = new PvPLeageReward()
		{
			league = PvPLeague.Gold,
			rank = 6,
			rewardAmount = 20,
			rewardType = PlayerItems.TypeNova
		};
		pvPLeageRewardArray2[5] = pvPLeageReward25;
		PvPLeageReward pvPLeageReward26 = new PvPLeageReward()
		{
			league = PvPLeague.Gold,
			rank = 7,
			rewardAmount = 20,
			rewardType = PlayerItems.TypeNova
		};
		pvPLeageRewardArray2[6] = pvPLeageReward26;
		PvPLeageReward pvPLeageReward27 = new PvPLeageReward()
		{
			league = PvPLeague.Gold,
			rank = 8,
			rewardAmount = 20,
			rewardType = PlayerItems.TypeNova
		};
		pvPLeageRewardArray2[7] = pvPLeageReward27;
		PvPLeageReward pvPLeageReward28 = new PvPLeageReward()
		{
			league = PvPLeague.Gold,
			rank = 9,
			rewardAmount = 20,
			rewardType = PlayerItems.TypeNova
		};
		pvPLeageRewardArray2[8] = pvPLeageReward28;
		PvPLeageReward pvPLeageReward29 = new PvPLeageReward()
		{
			league = PvPLeague.Gold,
			rank = 10,
			rewardAmount = 20,
			rewardType = PlayerItems.TypeNova
		};
		pvPLeageRewardArray2[9] = pvPLeageReward29;
		StaticData.pvpRewards.Add(PvPLeague.Bronze, pvPLeageRewardArray);
		StaticData.pvpRewards.Add(PvPLeague.Silver, pvPLeageRewardArray1);
		StaticData.pvpRewards.Add(PvPLeague.Gold, pvPLeageRewardArray2);
		StaticData.languages = new SortedList<string, string>();
		StaticData.slotPriceInformation = new SlotPriceInfo[20];
		SlotPriceInfo[] slotPriceInfoArray = StaticData.slotPriceInformation;
		SlotPriceInfo slotPriceInfo = new SlotPriceInfo()
		{
			slotId = 20,
			priceNova = 500,
			slotType = "Inventory"
		};
		slotPriceInfoArray[0] = slotPriceInfo;
		SlotPriceInfo[] slotPriceInfoArray1 = StaticData.slotPriceInformation;
		SlotPriceInfo slotPriceInfo1 = new SlotPriceInfo()
		{
			slotId = 24,
			priceNova = 1000,
			slotType = "Inventory"
		};
		slotPriceInfoArray1[1] = slotPriceInfo1;
		SlotPriceInfo[] slotPriceInfoArray2 = StaticData.slotPriceInformation;
		SlotPriceInfo slotPriceInfo2 = new SlotPriceInfo()
		{
			slotId = 28,
			priceNova = 2000,
			slotType = "Inventory"
		};
		slotPriceInfoArray2[2] = slotPriceInfo2;
		SlotPriceInfo[] slotPriceInfoArray3 = StaticData.slotPriceInformation;
		SlotPriceInfo slotPriceInfo3 = new SlotPriceInfo()
		{
			slotId = 32,
			priceNova = 3000,
			slotType = "Inventory"
		};
		slotPriceInfoArray3[3] = slotPriceInfo3;
		SlotPriceInfo[] slotPriceInfoArray4 = StaticData.slotPriceInformation;
		SlotPriceInfo slotPriceInfo4 = new SlotPriceInfo()
		{
			slotId = 36,
			priceNova = 4000,
			slotType = "Inventory"
		};
		slotPriceInfoArray4[4] = slotPriceInfo4;
		SlotPriceInfo[] slotPriceInfoArray5 = StaticData.slotPriceInformation;
		SlotPriceInfo slotPriceInfo5 = new SlotPriceInfo()
		{
			slotId = 40,
			priceNova = 5000,
			slotType = "Inventory"
		};
		slotPriceInfoArray5[5] = slotPriceInfo5;
		SlotPriceInfo[] slotPriceInfoArray6 = StaticData.slotPriceInformation;
		SlotPriceInfo slotPriceInfo6 = new SlotPriceInfo()
		{
			slotId = 16,
			priceNova = 500,
			slotType = "Vault"
		};
		slotPriceInfoArray6[6] = slotPriceInfo6;
		SlotPriceInfo[] slotPriceInfoArray7 = StaticData.slotPriceInformation;
		SlotPriceInfo slotPriceInfo7 = new SlotPriceInfo()
		{
			slotId = 20,
			priceNova = 1000,
			slotType = "Vault"
		};
		slotPriceInfoArray7[7] = slotPriceInfo7;
		SlotPriceInfo[] slotPriceInfoArray8 = StaticData.slotPriceInformation;
		SlotPriceInfo slotPriceInfo8 = new SlotPriceInfo()
		{
			slotId = 24,
			priceNova = 2000,
			slotType = "Vault"
		};
		slotPriceInfoArray8[8] = slotPriceInfo8;
		SlotPriceInfo[] slotPriceInfoArray9 = StaticData.slotPriceInformation;
		SlotPriceInfo slotPriceInfo9 = new SlotPriceInfo()
		{
			slotId = 28,
			priceNova = 3000,
			slotType = "Vault"
		};
		slotPriceInfoArray9[9] = slotPriceInfo9;
		SlotPriceInfo[] slotPriceInfoArray10 = StaticData.slotPriceInformation;
		SlotPriceInfo slotPriceInfo10 = new SlotPriceInfo()
		{
			slotId = 32,
			priceNova = 4000,
			slotType = "Vault"
		};
		slotPriceInfoArray10[10] = slotPriceInfo10;
		SlotPriceInfo[] slotPriceInfoArray11 = StaticData.slotPriceInformation;
		SlotPriceInfo slotPriceInfo11 = new SlotPriceInfo()
		{
			slotId = 36,
			priceNova = 5000,
			slotType = "Vault"
		};
		slotPriceInfoArray11[11] = slotPriceInfo11;
		SlotPriceInfo[] slotPriceInfoArray12 = StaticData.slotPriceInformation;
		SlotPriceInfo slotPriceInfo12 = new SlotPriceInfo()
		{
			slotId = 40,
			priceNova = 6000,
			slotType = "Vault"
		};
		slotPriceInfoArray12[12] = slotPriceInfo12;
		SlotPriceInfo[] slotPriceInfoArray13 = StaticData.slotPriceInformation;
		SlotPriceInfo slotPriceInfo13 = new SlotPriceInfo()
		{
			slotId = 16,
			priceNova = 500,
			priceEqulibrium = 500,
			slotType = "GuildVault"
		};
		slotPriceInfoArray13[13] = slotPriceInfo13;
		SlotPriceInfo[] slotPriceInfoArray14 = StaticData.slotPriceInformation;
		SlotPriceInfo slotPriceInfo14 = new SlotPriceInfo()
		{
			slotId = 20,
			priceNova = 1000,
			priceEqulibrium = 1000,
			slotType = "GuildVault"
		};
		slotPriceInfoArray14[14] = slotPriceInfo14;
		SlotPriceInfo[] slotPriceInfoArray15 = StaticData.slotPriceInformation;
		SlotPriceInfo slotPriceInfo15 = new SlotPriceInfo()
		{
			slotId = 24,
			priceNova = 1500,
			priceEqulibrium = 1500,
			slotType = "GuildVault"
		};
		slotPriceInfoArray15[15] = slotPriceInfo15;
		SlotPriceInfo[] slotPriceInfoArray16 = StaticData.slotPriceInformation;
		SlotPriceInfo slotPriceInfo16 = new SlotPriceInfo()
		{
			slotId = 28,
			priceNova = 2000,
			priceEqulibrium = 2000,
			slotType = "GuildVault"
		};
		slotPriceInfoArray16[16] = slotPriceInfo16;
		SlotPriceInfo[] slotPriceInfoArray17 = StaticData.slotPriceInformation;
		SlotPriceInfo slotPriceInfo17 = new SlotPriceInfo()
		{
			slotId = 32,
			priceNova = 3000,
			priceEqulibrium = 3000,
			slotType = "GuildVault"
		};
		slotPriceInfoArray17[17] = slotPriceInfo17;
		SlotPriceInfo[] slotPriceInfoArray18 = StaticData.slotPriceInformation;
		SlotPriceInfo slotPriceInfo18 = new SlotPriceInfo()
		{
			slotId = 36,
			priceNova = 4000,
			priceEqulibrium = 4000,
			slotType = "GuildVault"
		};
		slotPriceInfoArray18[18] = slotPriceInfo18;
		SlotPriceInfo[] slotPriceInfoArray19 = StaticData.slotPriceInformation;
		SlotPriceInfo slotPriceInfo19 = new SlotPriceInfo()
		{
			slotId = 40,
			priceNova = 5000,
			priceEqulibrium = 5000,
			slotType = "GuildVault"
		};
		slotPriceInfoArray19[19] = slotPriceInfo19;
		StaticData.powerUps = new SortedList<PowerUpCategory, PowerUpInfo[]>();
		PowerUpInfo[] powerUpInfoArray = new PowerUpInfo[7];
		PowerUpInfo powerUpInfo = new PowerUpInfo()
		{
			category = PowerUpCategory.Damage,
			name = "key_powerup_laser_dmg_flat",
			durationInHours = 4,
			powerUpType = PlayerItems.TypePowerUpForLaserDamageFlat,
			assetName = "DamageLaserFlat",
			priceInNova = 25,
			priceInViral = 25
		};
		powerUpInfoArray[0] = powerUpInfo;
		PowerUpInfo powerUpInfo1 = new PowerUpInfo()
		{
			category = PowerUpCategory.Damage,
			name = "key_powerup_plasma_dmg_flat",
			durationInHours = 4,
			powerUpType = PlayerItems.TypePowerUpForPlasmaDamageFlat,
			assetName = "DamagePlasmaFlat",
			priceInNova = 50,
			priceInViral = 50
		};
		powerUpInfoArray[1] = powerUpInfo1;
		PowerUpInfo powerUpInfo2 = new PowerUpInfo()
		{
			category = PowerUpCategory.Damage,
			name = "key_powerup_ion_dmg_flat",
			durationInHours = 4,
			powerUpType = PlayerItems.TypePowerUpForIonDamageFlat,
			assetName = "DamageIonFlat",
			priceInNova = 75,
			priceInViral = 75
		};
		powerUpInfoArray[2] = powerUpInfo2;
		PowerUpInfo powerUpInfo3 = new PowerUpInfo()
		{
			category = PowerUpCategory.Damage,
			name = "key_powerup_laser_dmg_percentage",
			durationInHours = 8,
			powerUpType = PlayerItems.TypePowerUpForLaserDamagePercentage,
			assetName = "DamageLaserPercentage",
			priceInNova = 50,
			priceInViral = 50
		};
		powerUpInfoArray[3] = powerUpInfo3;
		PowerUpInfo powerUpInfo4 = new PowerUpInfo()
		{
			category = PowerUpCategory.Damage,
			name = "key_powerup_plasma_dmg_percentage",
			durationInHours = 8,
			powerUpType = PlayerItems.TypePowerUpForPlasmaDamagePercentage,
			assetName = "DamagePlasmaPercentage",
			priceInNova = 100,
			priceInViral = 100
		};
		powerUpInfoArray[4] = powerUpInfo4;
		PowerUpInfo powerUpInfo5 = new PowerUpInfo()
		{
			category = PowerUpCategory.Damage,
			name = "key_powerup_ion_dmg_percentage",
			durationInHours = 8,
			powerUpType = PlayerItems.TypePowerUpForIonDamagePercentage,
			assetName = "DamageIonPercentage",
			priceInNova = 150,
			priceInViral = 150
		};
		powerUpInfoArray[5] = powerUpInfo5;
		PowerUpInfo powerUpInfo6 = new PowerUpInfo()
		{
			category = PowerUpCategory.Damage,
			name = "key_powerup_total_dmg_percentage",
			durationInHours = 12,
			powerUpType = PlayerItems.TypePowerUpForTotalDamagePercentage,
			assetName = "DamageTotalPercentage",
			priceInNova = 300,
			priceInViral = 0
		};
		powerUpInfoArray[6] = powerUpInfo6;
		StaticData.powerUps.Add(PowerUpCategory.Damage, powerUpInfoArray);
		PowerUpInfo[] powerUpInfoArray1 = new PowerUpInfo[3];
		PowerUpInfo powerUpInfo7 = new PowerUpInfo()
		{
			category = PowerUpCategory.Corpus,
			name = "key_powerup_corpus_flat",
			durationInHours = 4,
			powerUpType = PlayerItems.TypePowerUpForCorpusFlat,
			assetName = "CorpusFlat",
			priceInNova = 50,
			priceInViral = 50
		};
		powerUpInfoArray1[0] = powerUpInfo7;
		PowerUpInfo powerUpInfo8 = new PowerUpInfo()
		{
			category = PowerUpCategory.Corpus,
			name = "key_powerup_corpus_percentage",
			durationInHours = 8,
			powerUpType = PlayerItems.TypePowerUpForCorpusPercentage,
			assetName = "CorpusPercentage",
			priceInNova = 100,
			priceInViral = 100
		};
		powerUpInfoArray1[1] = powerUpInfo8;
		PowerUpInfo powerUpInfo9 = new PowerUpInfo()
		{
			category = PowerUpCategory.Corpus,
			name = "key_powerup_endurance_percentage",
			durationInHours = 12,
			powerUpType = PlayerItems.TypePowerUpForEndurancePercentage,
			assetName = "EndurancePercentage",
			priceInNova = 200,
			priceInViral = 0
		};
		powerUpInfoArray1[2] = powerUpInfo9;
		StaticData.powerUps.Add(PowerUpCategory.Corpus, powerUpInfoArray1);
		PowerUpInfo[] powerUpInfoArray2 = new PowerUpInfo[3];
		PowerUpInfo powerUpInfo10 = new PowerUpInfo()
		{
			category = PowerUpCategory.Shield,
			name = "key_powerup_shield_flat",
			durationInHours = 4,
			powerUpType = PlayerItems.TypePowerUpForShieldFlat,
			assetName = "ShieldFlat",
			priceInNova = 50,
			priceInViral = 50
		};
		powerUpInfoArray2[0] = powerUpInfo10;
		PowerUpInfo powerUpInfo11 = new PowerUpInfo()
		{
			category = PowerUpCategory.Shield,
			name = "key_powerup_shield_percentage",
			durationInHours = 8,
			powerUpType = PlayerItems.TypePowerUpForShieldPercentage,
			assetName = "ShieldPercentage",
			priceInNova = 100,
			priceInViral = 100
		};
		powerUpInfoArray2[1] = powerUpInfo11;
		PowerUpInfo powerUpInfo12 = new PowerUpInfo()
		{
			category = PowerUpCategory.Shield,
			name = "key_powerup_endurance_percentage",
			durationInHours = 12,
			powerUpType = PlayerItems.TypePowerUpForEndurancePercentage,
			assetName = "EndurancePercentage",
			priceInNova = 200,
			priceInViral = 0
		};
		powerUpInfoArray2[2] = powerUpInfo12;
		StaticData.powerUps.Add(PowerUpCategory.Shield, powerUpInfoArray2);
		PowerUpInfo[] powerUpInfoArray3 = new PowerUpInfo[2];
		PowerUpInfo powerUpInfo13 = new PowerUpInfo()
		{
			category = PowerUpCategory.ShieldPower,
			name = "key_powerup_shield_power_flat",
			durationInHours = 4,
			powerUpType = PlayerItems.TypePowerUpForShieldPowerFlat,
			assetName = "ShieldPowerFlat",
			priceInNova = 50,
			priceInViral = 50
		};
		powerUpInfoArray3[0] = powerUpInfo13;
		PowerUpInfo powerUpInfo14 = new PowerUpInfo()
		{
			category = PowerUpCategory.ShieldPower,
			name = "key_powerup_shield_power_percentage",
			durationInHours = 8,
			powerUpType = PlayerItems.TypePowerUpForShieldPowerPercentage,
			assetName = "ShieldPowerPercentage",
			priceInNova = 300,
			priceInViral = 0
		};
		powerUpInfoArray3[1] = powerUpInfo14;
		StaticData.powerUps.Add(PowerUpCategory.ShieldPower, powerUpInfoArray3);
		PowerUpInfo[] powerUpInfoArray4 = new PowerUpInfo[2];
		PowerUpInfo powerUpInfo15 = new PowerUpInfo()
		{
			category = PowerUpCategory.Targeting,
			name = "key_powerup_targeting_flat",
			durationInHours = 4,
			powerUpType = PlayerItems.TypePowerUpForTargetingFlat,
			assetName = "TargetingFlat",
			priceInNova = 50,
			priceInViral = 50
		};
		powerUpInfoArray4[0] = powerUpInfo15;
		PowerUpInfo powerUpInfo16 = new PowerUpInfo()
		{
			category = PowerUpCategory.Targeting,
			name = "key_powerup_targeting_percentage",
			durationInHours = 8,
			powerUpType = PlayerItems.TypePowerUpForTargetingPercentage,
			assetName = "TargetingPercentage",
			priceInNova = 100,
			priceInViral = 100
		};
		powerUpInfoArray4[1] = powerUpInfo16;
		StaticData.powerUps.Add(PowerUpCategory.Targeting, powerUpInfoArray4);
		PowerUpInfo[] powerUpInfoArray5 = new PowerUpInfo[2];
		PowerUpInfo powerUpInfo17 = new PowerUpInfo()
		{
			category = PowerUpCategory.Avoidance,
			name = "key_powerup_avoidance_flat",
			durationInHours = 4,
			powerUpType = PlayerItems.TypePowerUpForAvoidanceFlat,
			assetName = "AvoidanceFlat",
			priceInNova = 50,
			priceInViral = 50
		};
		powerUpInfoArray5[0] = powerUpInfo17;
		PowerUpInfo powerUpInfo18 = new PowerUpInfo()
		{
			category = PowerUpCategory.Avoidance,
			name = "key_powerup_avoidance_percentage",
			durationInHours = 8,
			powerUpType = PlayerItems.TypePowerUpForAvoidancePercentage,
			assetName = "AvoidancePercentage",
			priceInNova = 100,
			priceInViral = 100
		};
		powerUpInfoArray5[1] = powerUpInfo18;
		StaticData.powerUps.Add(PowerUpCategory.Avoidance, powerUpInfoArray5);
		StaticData.freeGifts = new FreeGift[3];
		FreeGift[] freeGiftArray = StaticData.freeGifts;
		FreeGift freeGift = new FreeGift()
		{
			itemType = PlayerItems.TypeCash,
			amount = 1000
		};
		freeGiftArray[0] = freeGift;
		FreeGift[] freeGiftArray1 = StaticData.freeGifts;
		FreeGift freeGift1 = new FreeGift()
		{
			itemType = PlayerItems.TypeAmmoColdFusionCells,
			amount = 200
		};
		freeGiftArray1[1] = freeGift1;
		FreeGift[] freeGiftArray2 = StaticData.freeGifts;
		FreeGift freeGift2 = new FreeGift()
		{
			itemType = PlayerItems.TypeAmmoSulfurFusionCells,
			amount = 200
		};
		freeGiftArray2[2] = freeGift2;
	}

	public StaticData()
	{
	}

	public static int ChangeFactionPriceInNova(int playerLevel)
	{
		int num;
		if (playerLevel >= 50)
		{
			num = 25000;
		}
		else if (playerLevel >= 40)
		{
			num = 25000;
		}
		else if (playerLevel >= 30)
		{
			num = 25000;
		}
		else if (playerLevel < 20)
		{
			num = (playerLevel < 10 ? 50 : 2500);
		}
		else
		{
			num = 7500;
		}
		return num;
	}

	public void Deserialize(BinaryReader br)
	{
		int i;
		ITransferable transferable;
		lock (StaticData.loadLocker)
		{
			try
			{
				int num = br.ReadInt32();
				StaticData.starBaseItemsDisribution = new StarBaseItemDistribution[num];
				for (i = 0; i < num; i++)
				{
					StaticData.starBaseItemsDisribution[i] = new StarBaseItemDistribution();
					StaticData.starBaseItemsDisribution[i].itemType = br.ReadUInt16();
					StaticData.starBaseItemsDisribution[i].presentState = br.ReadByte();
					StaticData.starBaseItemsDisribution[i].starBaseKey = br.ReadInt16();
				}
				int num1 = br.ReadInt32();
				StaticData.transformersPersonalRewards = new List<ushort>();
				for (i = 0; i < num1; i++)
				{
					StaticData.transformersPersonalRewards.Add(br.ReadUInt16());
				}
				num1 = br.ReadInt32();
				StaticData.transformersGuildRewards = new List<ushort>();
				for (i = 0; i < num1; i++)
				{
					StaticData.transformersGuildRewards.Add(br.ReadUInt16());
				}
				int num2 = br.ReadInt32();
				if (num2 != 0)
				{
					StaticData.allTypes = new SortedList<ushort, PlayerItemTypesData>(1000);
					for (i = 0; i < num2; i++)
					{
						ushort num3 = br.ReadUInt16();
						PlayerItemTypesData playerItemTypesDatum = (PlayerItemTypesData)TransferablesFramework.DeserializeITransferable(br);
						StaticData.allTypes.Add(num3, playerItemTypesDatum);
					}
					StaticData.shipTypes = new ShipsTypeNet[br.ReadInt32()];
					for (i = 0; i < (int)StaticData.shipTypes.Length; i++)
					{
						transferable = TransferablesFramework.DeserializeITransferable(br);
						StaticData.shipTypes[i] = (ShipsTypeNet)transferable;
					}
					StaticData.pveTypes = new PvEInfo[br.ReadInt32()];
					for (i = 0; i < (int)StaticData.pveTypes.Length; i++)
					{
						transferable = TransferablesFramework.DeserializeITransferable(br);
						StaticData.pveTypes[i] = (PvEInfo)transferable;
					}
					StaticData.levelsType = new SortedList<int, LevelsInfo>();
					int num4 = br.ReadInt32();
					for (i = 0; i < num4; i++)
					{
						LevelsInfo levelsInfo = (LevelsInfo)TransferablesFramework.DeserializeITransferable(br);
						StaticData.levelsType.Add((int)levelsInfo.level, levelsInfo);
					}
					StaticData.slotPriceInformation = new SlotPriceInfo[br.ReadInt32()];
					for (i = 0; i < (int)StaticData.slotPriceInformation.Length; i++)
					{
						transferable = TransferablesFramework.DeserializeITransferable(br);
						StaticData.slotPriceInformation[i] = (SlotPriceInfo)transferable;
					}
					StaticData.galaxyJumpsPrice = new GalaxiesJumpMap[br.ReadInt32()];
					for (i = 0; i < (int)StaticData.galaxyJumpsPrice.Length; i++)
					{
						transferable = TransferablesFramework.DeserializeITransferable(br);
						StaticData.galaxyJumpsPrice[i] = (GalaxiesJumpMap)transferable;
					}
					StaticData.allGalaxies = new LevelMap[br.ReadInt32()];
					for (i = 0; i < (int)StaticData.allGalaxies.Length; i++)
					{
						transferable = TransferablesFramework.DeserializeITransferable(br);
						StaticData.allGalaxies[i] = (LevelMap)transferable;
					}
					StaticData.allNPCs = new NpcObjectPhysics[br.ReadInt32()];
					for (i = 0; i < (int)StaticData.allNPCs.Length; i++)
					{
						transferable = TransferablesFramework.DeserializeITransferable(br);
						StaticData.allNPCs[i] = (NpcObjectPhysics)transferable;
					}
					StaticData.allCheckpoints = new CheckpointObjectPhysics[br.ReadInt32()];
					for (i = 0; i < (int)StaticData.allCheckpoints.Length; i++)
					{
						transferable = TransferablesFramework.DeserializeITransferable(br);
						StaticData.allCheckpoints[i] = (CheckpointObjectPhysics)transferable;
					}
					int num5 = br.ReadInt32();
					StaticData.pvpGameTypes = new PvPGameType[num5];
					for (i = 0; i < num5; i++)
					{
						PvPGameType pvPGameType = new PvPGameType();
						pvPGameType.Deserialize(br);
						StaticData.pvpGameTypes[i] = pvPGameType;
					}
					num5 = br.ReadInt32();
					StaticData.allExtractionPoints = new ExtractionPointInfo[num5];
					for (i = 0; i < num5; i++)
					{
						ExtractionPointInfo extractionPointInfo = new ExtractionPointInfo();
						extractionPointInfo.Deserialize(br);
						StaticData.allExtractionPoints[i] = extractionPointInfo;
					}
					num5 = br.ReadInt32();
					StaticData.allPortals = new Portal[num5];
					for (i = 0; i < num5; i++)
					{
						Portal portal = new Portal();
						portal.Deserialize(br);
						StaticData.allPortals[i] = portal;
					}
				}
				InitialPack.DeserializeTranslations(StaticData.translations, br);
				StaticData.serverBuild = br.ReadInt16();
				StaticData.firstQuestId = br.ReadInt32();
				StaticData.questsCount = br.ReadInt16();
				StaticData.maxAccessLevel = br.ReadByte();
				StaticData.guildUpgradesInfo = new List<GuildUpgrade>();
				int num6 = br.ReadInt32();
				if (num6 != -1)
				{
					for (i = 0; i < num6; i++)
					{
						GuildUpgrade guildUpgrade = new GuildUpgrade()
						{
							assetName = br.ReadString(),
							description = br.ReadString(),
							effectValue = br.ReadInt32(),
							priceEquilibrium = br.ReadInt32(),
							priceNova = br.ReadInt32(),
							priceUltralibrium = br.ReadInt32(),
							uiName = br.ReadString(),
							upgradeLevel = br.ReadByte(),
							upgradeType = br.ReadByte()
						};
						StaticData.guildUpgradesInfo.Add(guildUpgrade);
					}
				}
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
			}
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		int i;
		int j;
		int length = (int)StaticData.starBaseItemsDisribution.Length;
		bw.Write(length);
		for (i = 0; i < length; i++)
		{
			bw.Write(StaticData.starBaseItemsDisribution[i].itemType);
			bw.Write(StaticData.starBaseItemsDisribution[i].presentState);
			bw.Write(StaticData.starBaseItemsDisribution[i].starBaseKey);
		}
		bw.Write(StaticData.transformersPersonalRewards.Count);
		for (i = 0; i < StaticData.transformersPersonalRewards.Count; i++)
		{
			bw.Write(StaticData.transformersPersonalRewards[i]);
		}
		bw.Write(StaticData.transformersGuildRewards.Count);
		for (i = 0; i < StaticData.transformersGuildRewards.Count; i++)
		{
			bw.Write(StaticData.transformersGuildRewards[i]);
		}
		bw.Write(StaticData.allTypes.Count);
		foreach (PlayerItemTypesData value in StaticData.allTypes.Values)
		{
			bw.Write(value.itemType);
			TransferablesFramework.SerializeITransferable(bw, value, TransferContext.None);
		}
		bw.Write((int)StaticData.shipTypes.Length);
		ShipsTypeNet[] shipsTypeNetArray = StaticData.shipTypes;
		for (j = 0; j < (int)shipsTypeNetArray.Length; j++)
		{
			TransferablesFramework.SerializeITransferable(bw, shipsTypeNetArray[j], TransferContext.None);
		}
		bw.Write((int)StaticData.pveTypes.Length);
		PvEInfo[] pvEInfoArray = StaticData.pveTypes;
		for (j = 0; j < (int)pvEInfoArray.Length; j++)
		{
			TransferablesFramework.SerializeITransferable(bw, pvEInfoArray[j], TransferContext.None);
		}
		bw.Write(StaticData.levelsType.Count);
		foreach (LevelsInfo levelsInfo in StaticData.levelsType.Values)
		{
			TransferablesFramework.SerializeITransferable(bw, levelsInfo, TransferContext.None);
		}
		bw.Write((int)StaticData.slotPriceInformation.Length);
		SlotPriceInfo[] slotPriceInfoArray = StaticData.slotPriceInformation;
		for (j = 0; j < (int)slotPriceInfoArray.Length; j++)
		{
			TransferablesFramework.SerializeITransferable(bw, slotPriceInfoArray[j], TransferContext.None);
		}
		bw.Write((int)StaticData.galaxyJumpsPrice.Length);
		GalaxiesJumpMap[] galaxiesJumpMapArray = StaticData.galaxyJumpsPrice;
		for (j = 0; j < (int)galaxiesJumpMapArray.Length; j++)
		{
			TransferablesFramework.SerializeITransferable(bw, galaxiesJumpMapArray[j], TransferContext.None);
		}
		bw.Write((int)StaticData.allGalaxies.Length);
		LevelMap[] levelMapArray = StaticData.allGalaxies;
		for (j = 0; j < (int)levelMapArray.Length; j++)
		{
			TransferablesFramework.SerializeITransferable(bw, levelMapArray[j], TransferContext.None);
		}
		bw.Write((int)StaticData.allNPCs.Length);
		NpcObjectPhysics[] npcObjectPhysicsArray = StaticData.allNPCs;
		for (j = 0; j < (int)npcObjectPhysicsArray.Length; j++)
		{
			TransferablesFramework.SerializeITransferable(bw, npcObjectPhysicsArray[j], TransferContext.None);
		}
		bw.Write((int)StaticData.allCheckpoints.Length);
		CheckpointObjectPhysics[] checkpointObjectPhysicsArray = StaticData.allCheckpoints;
		for (j = 0; j < (int)checkpointObjectPhysicsArray.Length; j++)
		{
			TransferablesFramework.SerializeITransferable(bw, checkpointObjectPhysicsArray[j], TransferContext.None);
		}
		bw.Write((int)StaticData.pvpGameTypes.Length);
		PvPGameType[] pvPGameTypeArray = StaticData.pvpGameTypes;
		for (j = 0; j < (int)pvPGameTypeArray.Length; j++)
		{
			pvPGameTypeArray[j].Serialize(bw);
		}
		bw.Write((int)StaticData.allExtractionPoints.Length);
		ExtractionPointInfo[] extractionPointInfoArray = StaticData.allExtractionPoints;
		for (j = 0; j < (int)extractionPointInfoArray.Length; j++)
		{
			extractionPointInfoArray[j].Serialize(bw);
		}
		bw.Write((int)StaticData.allPortals.Length);
		Portal[] portalArray = StaticData.allPortals;
		for (j = 0; j < (int)portalArray.Length; j++)
		{
			portalArray[j].Serialize(bw);
		}
		InitialPack.SerializeTranslations(StaticData.translations, bw);
		bw.Write((short)164);
		bw.Write(StaticData.firstQuestId);
		bw.Write(StaticData.questsCount);
		bw.Write(StaticData.maxAccessLevel);
		if (StaticData.guildUpgradesInfo == null)
		{
			bw.Write(-1);
		}
		else
		{
			bw.Write(StaticData.guildUpgradesInfo.Count);
			foreach (GuildUpgrade guildUpgrade in StaticData.guildUpgradesInfo)
			{
				bw.Write(guildUpgrade.assetName ?? "");
				bw.Write(guildUpgrade.description ?? "");
				bw.Write(guildUpgrade.effectValue);
				bw.Write(guildUpgrade.priceEquilibrium);
				bw.Write(guildUpgrade.priceNova);
				bw.Write(guildUpgrade.priceUltralibrium);
				bw.Write(guildUpgrade.uiName ?? "");
				bw.Write(guildUpgrade.upgradeLevel);
				bw.Write(guildUpgrade.upgradeType);
			}
		}
	}

	public static string Translate(string key)
	{
		string str;
		string str1 = key;
		str = (!StaticData.translations.TryGetValue(key, out str1) ? key : str1);
		return str;
	}

	public static float Velocity(float speed)
	{
		return (speed > 0f ? speed / Mathf.Pow(speed, 0.6f) * 40f : 0f);
	}
}