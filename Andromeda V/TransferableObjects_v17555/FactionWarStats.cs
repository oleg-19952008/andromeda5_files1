using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

public class FactionWarStats
{
	public const int KILL_PLAYER_DIFFERENCE_LEVEL = 10;

	public const int KILL_PLAYER_MAX_SCORE = 1200;

	public const int KILL_PLAYER_NORMAL_SCORE = 600;

	public const int KILL_PLAYER_MIN_SCORE = 0;

	public const int FUSE_EQUILIBRIUM_TIER_1_MAX_LEVEL = 20;

	public const int FUSE_EQUILIBRIUM_TIER_2_MAX_LEVEL = 30;

	public const int FUSE_EQUILIBRIUM_TIER_3_MAX_LEVEL = 40;

	public const int FUSE_EQUILIBRIUM_TIER_4_MAX_LEVEL = 50;

	public const int FUSE_EQUILIBRIUM_TIER_5_MAX_LEVEL = 56;

	public const int FUSE_EQUILIBRIUM_TIER_1_SCORE = 400;

	public const int FUSE_EQUILIBRIUM_TIER_2_SCORE = 200;

	public const int FUSE_EQUILIBRIUM_TIER_3_SCORE = 100;

	public const int FUSE_EQUILIBRIUM_TIER_4_SCORE = 75;

	public const int FUSE_EQUILIBRIUM_TIER_5_SCORE = 50;

	public const int INSTANCE_NORMAL_SCORE = 600;

	public const int INSTANCE_HARD_SCORE = 1200;

	public const int INSTANCE_INSANE_SCORE = 2400;

	public const int DOMINATION_WIN_SCORE = 3000;

	public const int DOMINATION_DRAW_SCORE = 2000;

	public const int DOMINATION_LOSE_SCORE = 1000;

	public const int MIN_DONATION_FEE = 1000;

	public const int MIN_COUNCIL_LEVEL = 20;

	public const int MAX_COUNCIL_DAYS_OFFLINE = 7;

	public const byte FACTION_BOOST_BATTLE_CLEAR_SELECTION = 100;

	public const byte FACTION_BOOST_BATTLE_ONE = 101;

	public const byte FACTION_BOOST_BATTLE_TWO = 102;

	public const byte FACTION_BOOST_BATTLE_THREE = 103;

	public const byte FACTION_BOOST_BATTLE_VETO = 104;

	public const byte FACTION_BOOST_UTILITY_CLEAR_SELECTION = 200;

	public const byte FACTION_BOOST_UTILITY_ONE = 201;

	public const byte FACTION_BOOST_UTILITY_TWO = 202;

	public const byte FACTION_BOOST_UTILITY_THREE = 203;

	public const byte FACTION_BOOST_UTILITY_VETO = 204;

	public const float FACTION_BOOST_PVP_VALUE = 20f;

	public const float FACTION_BOOST_RESILIENCE_VALUE = 20f;

	public const float FACTION_BOOST_PVE_VALUE = 20f;

	public const float FACTION_BOOST_MINING_VALUE = 50f;

	public const float FACTION_BOOST_CARGO_VALUE = 25f;

	public const float FACTION_BOOST_AMMO_CREATION_VALUE = 10f;

	public const float FACTION_BOOST_FUSE_PRICE_OFF_VALUE = 20f;

	public const float FACTION_BOOST_INCOME_VALUE = 10f;

	public const int ULTRALIBRIUM_REWARDS_FOR_DAY_PROGRESS_STEP_1 = 5;

	public const int ULTRALIBRIUM_REWARDS_FOR_DAY_PROGRESS_STEP_2 = 15;

	public const int ULTRALIBRIUM_REWARDS_FOR_DAY_PROGRESS_STEP_3 = 25;

	public const int DAY_REWARDS_REQUIREMENT_STEP_1 = 3000;

	public const int DAY_REWARDS_REQUIREMENT_STEP_2 = 6000;

	public const int DAY_REWARDS_REQUIREMENT_STEP_3 = 12000;

	public const int WAR_DAILY_SCORE_PLAYER_LIMIT = 60000;

	public const int ULTRALIBRIUM_REWARDS_FOR_WEEK_PARTICIPATION_STEP_1 = 5;

	public const int ULTRALIBRIUM_REWARDS_FOR_WEEK_PARTICIPATION_STEP_2 = 15;

	public const int ULTRALIBRIUM_REWARDS_FOR_WEEK_PARTICIPATION_STEP_3 = 25;

	public const int ULTRALIBRIUM_REWARDS_FOR_WEEK_PARTICIPATION_STEP_4 = 50;

	public const int ULTRALIBRIUM_REWARDS_FOR_WEEK_PARTICIPATION_STEP_5 = 75;

	public const int ULTRALIBRIUM_REWARDS_FOR_WEEK_PARTICIPATION_STEP_6 = 100;

	public const int PAID_AD_STARTING_FEE = 10;

	public const int PAID_AD_NEXT_AD_FEE_RAISE = 10;

	public const int LOOSING_FACTION_BONUS_PER_GALAXY = 10;

	public const int FACTION_GALAXY_XP_BONUS_PERCENT = 20;

	public const int FACTION_GALAXY_RESOURCE_BONUS_PERCENT = 100;

	public const int FACTION_GALAXY_SELL_BONUS_PERCENT = 100;

	public const int FACTION_GALAXY_RESEARCH_POINT_BONUS_PERCENT = 50;

	public const int FACTION_GALAXY_DEFFENDING_BONUS_PERCENT = 100;

	public const int FACTION_WAR_WINNER_REROLL_BONUS = 20;

	public const int FACTION_WAR_WINNER_XP_BONUS = 25;

	public const int FACTION_WAR_LOOSER_REROLL_BONUS = 10;

	public const int FACTION_WAR_LOOSER_XP_BONUS = 100;

	private const int COUNCIL_SKILL_PRICE_CASH_T2 = 500000;

	private const int COUNCIL_SKILL_PRICE_EQUILIBRIUM_T2 = 750;

	private const int COUNCIL_SKILL_PRICE_CASH_T3 = 2500000;

	private const int COUNCIL_SKILL_PRICE_EQUILIBRIUM_T3 = 1000;

	private const int COUNCIL_SKILL_PRICE_CASH_T4 = 7500000;

	private const int COUNCIL_SKILL_PRICE_EQUILIBRIUM_T4 = 1500;

	private const int COUNCIL_SKILL_PRICE_CASH_T5 = 15000000;

	private const int COUNCIL_SKILL_PRICE_EQUILIBRIUM_T5 = 2500;

	public const int WAR_COMMENDATION_LIMIT = 100;

	public const int WAR_COMMENDATION_FOR_DAILY_QUEST_DONE = 1;

	public const int WAR_COMMENDATION_FOR_DAILY_WAR_PROGRESS_DONE = 2;

	public const int WAR_COMMENDATION_FOR_DAY_WAR_PARTICIPATION = 3;

	private SortedList<int, long> candidatesCouncil = new SortedList<int, long>();

	private SortedList<int, long> factionDonators = new SortedList<int, long>();

	public SortedList<short, long> galaxyElection = new SortedList<short, long>();

	public SortedList<int, FactionCouncilMember> theCouncil = new SortedList<int, FactionCouncilMember>();

	public byte factionId;

	public float weeklyScoreBonus = 0f;

	public float dailyScoreBonus = 0f;

	public long killPlayerScore;

	public long dominationGameScore;

	public long fuseEquilibriumScore;

	public long clearInstanceScore;

	public long extractionPointScore;

	public long currentDayScore;

	public long treasury;

	public int paidAdPlayerId;

	public string paidAdNickname = "";

	public string paidAdSlogan = "";

	public int nextPaidAdPrice = 0;

	public byte activeBattleBoost = 0;

	public byte activeUtilityBoost = 0;

	public DayOfWeek lastActivateBoostCheckTime;

	public short mostVotedGalaxy = 0;

	public string messageToYourFaction = "";

	public string messageToEnemyFaction = "";

	public static SortedList<DayOfWeek, List<ScoreType>> dayOfWeek;

	public static DayOfWeek bonusIncomDay;

	public static DayOfWeek pvpDominationByFactionDay;

	public static DayOfWeek voteForPlayerDay;

	public static DayOfWeek voteForGalaxyDay;

	public static DayOfWeek resetWeeklyChalangeProgress;

	public static List<DayOfWeek> boostActivationDays;

	public static List<DayOfWeek> boostVoteDays;

	public int AdPrice
	{
		get
		{
			return (this.nextPaidAdPrice == 0 ? 10 : this.nextPaidAdPrice);
		}
	}

	public long DayScore
	{
		get
		{
			return this.currentDayScore;
		}
	}

	private int NextAdPrice
	{
		get
		{
			return this.AdPrice + 10;
		}
	}

	public long TotalScore
	{
		get
		{
			long num = this.killPlayerScore + this.dominationGameScore + this.fuseEquilibriumScore + this.clearInstanceScore + this.extractionPointScore;
			return num;
		}
	}

	static FactionWarStats()
	{
		FactionWarStats.dayOfWeek = new SortedList<DayOfWeek, List<ScoreType>>()
		{
			{ DayOfWeek.Monday, new List<ScoreType>() },
			{ DayOfWeek.Tuesday, new List<ScoreType>() },
			{ DayOfWeek.Wednesday, new List<ScoreType>()
			{
				ScoreType.DominationGame,
				ScoreType.ExtractionIncome
			} },
			{ DayOfWeek.Thursday, new List<ScoreType>()
			{
				ScoreType.InstanceClear,
				ScoreType.ExtractionIncome
			} },
			{ DayOfWeek.Friday, new List<ScoreType>()
			{
				ScoreType.EquilibriumFuse,
				ScoreType.ExtractionIncome
			} },
			{ DayOfWeek.Saturday, new List<ScoreType>()
			{
				ScoreType.PlayerKill,
				ScoreType.ExtractionIncome
			} },
			{ DayOfWeek.Sunday, new List<ScoreType>() }
		};
		FactionWarStats.bonusIncomDay = DayOfWeek.Saturday;
		FactionWarStats.pvpDominationByFactionDay = DayOfWeek.Wednesday;
		FactionWarStats.voteForPlayerDay = DayOfWeek.Monday;
		FactionWarStats.voteForGalaxyDay = DayOfWeek.Tuesday;
		FactionWarStats.resetWeeklyChalangeProgress = DayOfWeek.Sunday;
		FactionWarStats.boostActivationDays = new List<DayOfWeek>()
		{
			DayOfWeek.Wednesday,
			DayOfWeek.Thursday,
			DayOfWeek.Friday,
			DayOfWeek.Saturday
		};
		FactionWarStats.boostVoteDays = new List<DayOfWeek>()
		{
			DayOfWeek.Tuesday,
			DayOfWeek.Wednesday,
			DayOfWeek.Thursday,
			DayOfWeek.Friday
		};
	}

	public FactionWarStats(byte _factionId)
	{
		this.factionId = _factionId;
	}

	public FactionWarStats(byte _factionId, long _treasury, short _paidAdPrice, long _scoreKillPlayer, long _scoreDominationMode, long _scoreFuseEq, long _scoreCliearInstance, long _scorePoIIncome, long _scoreCurrentDay)
	{
		this.factionId = _factionId;
		this.treasury = _treasury;
		this.nextPaidAdPrice = _paidAdPrice;
		this.killPlayerScore = _scoreKillPlayer;
		this.dominationGameScore = _scoreDominationMode;
		this.fuseEquilibriumScore = _scoreFuseEq;
		this.clearInstanceScore = _scoreCliearInstance;
		this.extractionPointScore = _scorePoIIncome;
		this.currentDayScore = _scoreCurrentDay;
	}

	public void AddCouncilMember(FactionCouncilMember member)
	{
		if ((this.theCouncil == null || this.theCouncil.Count >= 10 ? false : !this.theCouncil.ContainsKey(member.playerDbId)))
		{
			this.theCouncil.Add(member.playerDbId, member);
		}
	}

	public void AddDominationGameScore(long amount)
	{
		if ((this.weeklyScoreBonus > 0f ? true : this.dailyScoreBonus > 0f))
		{
			amount = (long)(((this.weeklyScoreBonus + this.dailyScoreBonus) / 100f + 1f) * (float)amount);
		}
		this.dominationGameScore += amount;
		this.currentDayScore += amount;
	}

	public void AddExtractionScore(long amount)
	{
		if ((this.weeklyScoreBonus > 0f ? true : this.dailyScoreBonus > 0f))
		{
			amount = (long)(((this.weeklyScoreBonus + this.dailyScoreBonus) / 100f + 1f) * (float)amount);
		}
		this.extractionPointScore += amount;
		this.currentDayScore += amount;
	}

	public void AddFuseScore(long amount)
	{
		if ((this.weeklyScoreBonus > 0f ? true : this.dailyScoreBonus > 0f))
		{
			amount = (long)(((this.weeklyScoreBonus + this.dailyScoreBonus) / 100f + 1f) * (float)amount);
		}
		this.fuseEquilibriumScore += amount;
		this.currentDayScore += amount;
	}

	public void AddInstanceScore(long amount)
	{
		if ((this.weeklyScoreBonus > 0f ? true : this.dailyScoreBonus > 0f))
		{
			amount = (long)(((this.weeklyScoreBonus + this.dailyScoreBonus) / 100f + 1f) * (float)amount);
		}
		this.clearInstanceScore += amount;
		this.currentDayScore += amount;
	}

	public void AddKillPlayerScore(long amount)
	{
		if ((this.weeklyScoreBonus > 0f ? true : this.dailyScoreBonus > 0f))
		{
			amount = (long)(((this.weeklyScoreBonus + this.dailyScoreBonus) / 100f + 1f) * (float)amount);
		}
		this.killPlayerScore += amount;
		this.currentDayScore += amount;
	}

	public void ClearCandidatesCouncil()
	{
		lock (this.candidatesCouncil)
		{
			this.candidatesCouncil.Clear();
		}
	}

	public void ClearCoucilMembers()
	{
		lock (this.theCouncil)
		{
			this.theCouncil.Clear();
		}
	}

	public void ClearFactionMessage()
	{
		this.messageToEnemyFaction = "";
		this.messageToYourFaction = "";
	}

	public static int CouncilSkillPriceCash(int level)
	{
		int num;
		switch (Mathf.FloorToInt((float)(level / 10)))
		{
			case 2:
			{
				num = 500000;
				break;
			}
			case 3:
			{
				num = 2500000;
				break;
			}
			case 4:
			{
				num = 7500000;
				break;
			}
			default:
			{
				num = 15000000;
				break;
			}
		}
		return num;
	}

	public static int CouncilSkillPriceEquilibrium(int level)
	{
		int num;
		switch (Mathf.FloorToInt((float)(level / 10)))
		{
			case 2:
			{
				num = 750;
				break;
			}
			case 3:
			{
				num = 1000;
				break;
			}
			case 4:
			{
				num = 1500;
				break;
			}
			default:
			{
				num = 2500;
				break;
			}
		}
		return num;
	}

	public void Donate(PlayerData plr, long donation)
	{
		if (!this.factionDonators.ContainsKey(plr.dbId))
		{
			this.factionDonators.Add(plr.dbId, donation);
		}
		else
		{
			SortedList<int, long> item = this.factionDonators;
			SortedList<int, long> nums = item;
			int num = plr.dbId;
			item[num] = nums[num] + donation;
		}
		this.treasury += donation;
	}

	public static int FuseScoreMultiplier(int playerLevel)
	{
		int num;
		if (playerLevel >= 50)
		{
			num = 50;
		}
		else if (playerLevel >= 40)
		{
			num = 75;
		}
		else if (playerLevel < 30)
		{
			num = (playerLevel < 20 ? 400 : 200);
		}
		else
		{
			num = 100;
		}
		return num;
	}

	public List<KeyValuePair<int, long>> GetBestCouncilCandidates(int plrDbId)
	{
		List<KeyValuePair<int, long>> keyValuePairs = new List<KeyValuePair<int, long>>();
		keyValuePairs = (
			from t in this.candidatesCouncil
			orderby t.Value descending
			select t).Take<KeyValuePair<int, long>>(10).ToList<KeyValuePair<int, long>>();
		if (!this.candidatesCouncil.ContainsKey(plrDbId))
		{
			keyValuePairs.Add(new KeyValuePair<int, long>(plrDbId, (long)0));
		}
		else
		{
			keyValuePairs.Add(new KeyValuePair<int, long>(plrDbId, this.candidatesCouncil[plrDbId]));
		}
		return keyValuePairs;
	}

	public int[] GetTopSuppertedCandidates()
	{
		int[] array = (
			from s in (
				from t in this.candidatesCouncil
				orderby t.Value descending
				select t).Take<KeyValuePair<int, long>>(10)
			select s.Key).ToArray<int>();
		return array;
	}

	public bool GetTopSuppertedGalaxy()
	{
		this.mostVotedGalaxy = 0;
		long item = (long)0;
		foreach (short key in this.galaxyElection.Keys)
		{
			if (this.galaxyElection[key] > item)
			{
				item = this.galaxyElection[key];
				this.mostVotedGalaxy = key;
			}
		}
		return this.mostVotedGalaxy != 0;
	}

	public static int InstanceScoreMultiplier(int playerLevel, InstanceDifficulty difficulty, int instanceMaxLevel)
	{
		int num;
		if (playerLevel <= instanceMaxLevel)
		{
			switch (difficulty)
			{
				case InstanceDifficulty.Normal:
				{
					num = 600;
					break;
				}
				case InstanceDifficulty.Hard:
				{
					num = 1200;
					break;
				}
				case InstanceDifficulty.Insane:
				{
					num = 2400;
					break;
				}
				default:
				{
					num = 0;
					break;
				}
			}
		}
		else
		{
			num = 0;
		}
		return num;
	}

	public static int KillPlayerScoreMultiplier(int shooterLevel, int targetLevel)
	{
		int num;
		if ((shooterLevel + 10 <= targetLevel ? true : targetLevel <= shooterLevel - 10))
		{
			num = (targetLevel <= shooterLevel + 10 ? 0 : 1200);
		}
		else
		{
			num = 600;
		}
		return num;
	}

	public void RemovePlayerFromCandidateCouncil(int plrId)
	{
		if (this.candidatesCouncil.ContainsKey(plrId))
		{
			this.candidatesCouncil.Remove(plrId);
		}
	}

	public void ResetDailyScore()
	{
		this.currentDayScore = (long)0;
	}

	public void ResetPaidAd()
	{
		this.paidAdPlayerId = 0;
		this.nextPaidAdPrice = 0;
		this.paidAdSlogan = "";
		this.paidAdNickname = "";
	}

	public void ResetWeeklyScore()
	{
		this.killPlayerScore = (long)0;
		this.dominationGameScore = (long)0;
		this.fuseEquilibriumScore = (long)0;
		this.clearInstanceScore = (long)0;
		this.extractionPointScore = (long)0;
		this.currentDayScore = (long)0;
		this.mostVotedGalaxy = 0;
		this.galaxyElection.Clear();
	}

	public void SetCandidatesCouncil(SortedList<int, long> list)
	{
		this.candidatesCouncil = list;
	}

	public void SetFactionDonators(SortedList<int, long> list)
	{
		this.factionDonators = list;
	}

	public override string ToString()
	{
		return "";
	}

	public bool UpdatePaidAd(PlayerData plr, string slogan, int price)
	{
		bool flag;
		if (price != this.AdPrice)
		{
			flag = false;
		}
		else
		{
			this.paidAdPlayerId = plr.dbId;
			this.paidAdNickname = plr.playerBelongings.playerName;
			this.paidAdSlogan = slogan;
			this.nextPaidAdPrice = this.NextAdPrice;
			flag = true;
		}
		return flag;
	}

	public void VoteForGalaxy(PlayerData plr, short galaxyId, long vote)
	{
		if (!this.galaxyElection.ContainsKey(galaxyId))
		{
			this.galaxyElection.Add(galaxyId, vote);
		}
		else
		{
			SortedList<short, long> item = this.galaxyElection;
			SortedList<short, long> nums = item;
			short num = galaxyId;
			item[num] = nums[num] + vote;
		}
	}

	public void VoteForPlayer(PlayerData plr, PlayerBasic candidate, long vote)
	{
		if (!this.candidatesCouncil.ContainsKey(candidate.dbId))
		{
			this.candidatesCouncil.Add(candidate.dbId, vote);
		}
		else
		{
			SortedList<int, long> item = this.candidatesCouncil;
			SortedList<int, long> nums = item;
			int num = candidate.dbId;
			item[num] = nums[num] + vote;
		}
	}

	public static void WarCommendationPrice(int playerLevel, byte bought, out long cash, out int eqilibrium, out int ultralibrium)
	{
		cash = (long)0;
		eqilibrium = 0;
		ultralibrium = 0;
		cash = (long)Math.Round(Math.Pow(2, (double)Mathf.FloorToInt((float)playerLevel / 10f)) * 250 * (double)Mathf.Pow(2f, (float)bought));
		eqilibrium = (bought < 4 ? 0 : (int)Math.Round((double)((float)(Mathf.FloorToInt((float)playerLevel / 10f) * 25) * Mathf.Pow(1.5f, (float)(bought - 4)))));
		ultralibrium = (bought < 9 ? 0 : (int)Math.Round((double)(15f * Mathf.Pow(1.5f, (float)(bought - 9)))));
	}
}