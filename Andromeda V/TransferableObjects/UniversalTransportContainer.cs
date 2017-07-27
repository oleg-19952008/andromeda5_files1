using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class UniversalTransportContainer : ITransferableInContext, ITransferable
{
	public GenericData fractionOneInfo;

	public GenericData fractionTwoInfo;

	public short galaxyId;

	public List<ExtractionPoint> extractionPointsOnMap;

	public List<Guild> guilds;

	public long playerId;

	public byte killCount;

	public int expReward;

	public ExtendedSlot wantedSlot;

	public byte wantedBonusRandom;

	public byte wantedBonusOne;

	public byte wantedBonusTwo;

	public byte wantedBonusThree;

	public byte wantedBonusFour;

	public byte wantedBonusFive;

	public byte maximaizedBonusRandom;

	public byte maximaizedBonusOne;

	public byte maximaizedBonusTwo;

	public byte maximaizedBonusThree;

	public byte maximaizedBonusFour;

	public byte maximaizedBonusFive;

	public ushort rerollItemType;

	public ushort rerollItemSlotId;

	public SelectedCurrency paymentCurrency;

	public byte wantedGuildUpgradeType;

	public byte socialInteractionIndex;

	public string socialInteractionCustomText;

	public List<int> unlockedPortals;

	public bool isForGuild;

	public byte intensity;

	public ushort transformerRewardType;

	public int transformerRewardAmount;

	public byte transformerState;

	public uint neighbourhoodId;

	public uint selectedPoPnbId;

	public QuestEngineEnum questEnginCommand;

	public PlayerObjectives playerObjectives;

	public int questEngineId;

	public int checkpointId;

	public int npcKey;

	public NewQuest[] questList;

	public byte serverMessageIndex;

	public SortedList<ushort, byte> wantedSkills;

	public SortedList<int, PlayerPendingAward> pendindAwards;

	public int pendingAwardId;

	public byte receivedDailyRewards;

	public long criticalHitPlayerId;

	public uint criticalHitTargetNbId;

	public float criticalHitBonus;

	public byte criticalComboCnt;

	public float newSpeed;

	public bool isBoostActive;

	public Jump jumpType;

	public short destinationGalaxyId;

	public byte playerAccessLevel;

	public KeyboardShortcutPair keyboardUpdatePair;

	public PvPLeague playerLeague;

	public bool inPvPRank;

	public PvPLeagueRewardetPlayer[] pvpRewardedPlayers;

	public DateTime nextPvPRoundTime;

	public byte pvpGamePoolCapacity;

	public string signedPlayerName;

	public byte signedPlayerLevel;

	public short signedPlayersCount;

	public bool isShowMoreDetailsOn;

	public bool isStunned;

	public bool isDisarmed;

	public bool isShocked;

	public ushort councilSkillId;

	public int instanceGalaxyId;

	public InstanceDifficulty selectedDificulty;

	public InstanceStatus instanceStatus;

	public short instanceKillTarget;

	public short instanceKillProgress;

	public ushort selectedGiftType;

	public string receiverNickname;

	public string giftTitle;

	public bool isFreeGift;

	public bool isAnonymously;

	public short pointId;

	public byte poiUnityType;

	public SortedList<short, byte> wantedGuardianSkills;

	public SelectedWallet wallet;

	public string candidateName;

	public short selectedGalaxyId;

	public long voteDonation;

	public List<KeyValuePair<string, long>> factionCouncils;

	public string paidAdNickName;

	public string paidAdSlogan;

	public int nextPaidAdPrice;

	public DateTime factionWarDayEndTime;

	public DayOfWeek factionWarDay;

	public byte councilRank = 0;

	public ushort selectedCouncilSkillId = 0;

	public bool day1Participation = false;

	public bool day2Participation = false;

	public bool day3Participation = false;

	public bool day4Participation = false;

	public bool day5Participation = false;

	public bool day6Participation = false;

	public byte myVoteForFactionBoost = 0;

	public int factionWarDayScore;

	public bool dailyReward1Collected = false;

	public bool dailyReward2Collected = false;

	public bool dailyReward3Collected = false;

	public bool weeklyRewardCollected = false;

	public byte lastWeekPendingReward = 0;

	public long factionOneScore;

	public long factionTwoScore;

	public long factionOneDayScore;

	public long factionTwoDayScore;

	public byte loosingFaction;

	public byte loosingFactionBonusPercent;

	public byte dailyLoosingFaction;

	public short dailyLoosingFactionBonusPercent;

	public List<FactionCouncilMember> factionOneCouncil;

	public List<FactionCouncilMember> factionTwoCouncil;

	public byte battleBoost1Votes = 0;

	public byte battleBoost2Votes = 0;

	public byte battleBoost3Votes = 0;

	public byte battleBoostVeto = 0;

	public byte utilityBoost1Votes = 0;

	public byte utilityBoost2Votes = 0;

	public byte utilityBoost3Votes = 0;

	public byte utilityBoostVeto = 0;

	public byte myUtilityBoostVote = 0;

	public byte myBattleBoostVote = 0;

	public List<KeyValuePair<short, long>> galaxyVote;

	public string yourFactionToYou = "";

	public string yourFactionToEnemy = "";

	public string enemyFactionToYou = "";

	public byte errorCodeIndex;

	public List<ExtractionPointStateInfo> epsStateInfo = new List<ExtractionPointStateInfo>();

	public SortedList<byte, byte> allFactionGalaxiesOwnership;

	public byte factionOneMostWantedGalaxy;

	public byte factionTwoMostWantedGalaxy;

	public bool isWarInProgress;

	public DateTime nextWarStartTime;

	public GuildRank myGuildRank;

	public PartyMemberInfo partyMemberInfo;

	public string deviceName;

	public string deviceType;

	public string deviceModel;

	public string deviceId;

	public int messageId;

	public List<GameMessage> playerGameMessages;

	public int gameMsgId;

	public int privateMsgId;

	public UniversalTransportContainer()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		GameObjectPhysics.Log("UNSUPPORTED DESERIALIZE!");
		throw new NotImplementedException();
	}

	private void DeserializeGuildRanking(BinaryReader br)
	{
		int num = br.ReadInt32();
		this.guilds = new List<Guild>();
		for (int i = 0; i < num; i++)
		{
			Guild guild = new Guild()
			{
				id = br.ReadInt32(),
				fractionId = br.ReadByte(),
				name = br.ReadString(),
				title = br.ReadString()
			};
			int num1 = br.ReadInt32();
			guild.members = new SortedList<int, PlayerBasic>();
			for (int j = 1; j <= num1; j++)
			{
				guild.members.Add(j, new PlayerBasic());
			}
			guild.bankUltralibrium = br.ReadInt64();
			guild.upgradeOneLevel = br.ReadByte();
			guild.upgradeTwoLevel = br.ReadByte();
			guild.upgradeThreeLevel = br.ReadByte();
			guild.upgradeFourLevel = br.ReadByte();
			guild.upgradeFiveLevel = br.ReadByte();
			this.guilds.Add(guild);
		}
	}

	public void DeserializeInContext(BinaryReader br, TransferContext context)
	{
		int num;
		int i;
		byte num1;
		string str;
		long num2;
		FactionCouncilMember factionCouncilMember;
		switch (context)
		{
			case TransferContext.GuildUpgrade:
			{
				this.paymentCurrency = (SelectedCurrency)br.ReadByte();
				this.wantedGuildUpgradeType = br.ReadByte();
				return;
			}
			case TransferContext.GuildInvite:
			case TransferContext.GuildInvited:
			case TransferContext.GuildInviteError:
			case TransferContext.EpContributors:
			case TransferContext.GuildInviteRemove:
			case TransferContext.GuildInviteReject:
			case TransferContext.GuildInviteAccept:
			case TransferContext.GuildCurrentUserRank:
			case TransferContext.GuildRankChange:
			case TransferContext.GuildRankAdd:
			case TransferContext.GuildRankDelete:
			case TransferContext.GuildEpInfoRequest:
			case TransferContext.GuildEpInfoResponse:
			case TransferContext.InitialRequestV1:
			case TransferContext.GetDailyMissionsReward:
			case TransferContext.MiningStationProgressUpdate:
			{
				return;
			}
			case TransferContext.GuildRankUpdate:
			{
				this.myGuildRank = new GuildRank()
				{
					canBank = br.ReadBoolean(),
					canChat = br.ReadBoolean(),
					canEditDetails = br.ReadBoolean(),
					canInvite = br.ReadBoolean(),
					canPromote = br.ReadBoolean(),
					canVault = br.ReadBoolean()
				};
				return;
			}
			case TransferContext.FractionOverview:
			{
				this.fractionOneInfo = new GenericData();
				this.fractionOneInfo.Deserialize(br);
				this.fractionTwoInfo = new GenericData();
				this.fractionTwoInfo.Deserialize(br);
				return;
			}
			case TransferContext.GameMapOverview:
			{
				this.galaxyId = br.ReadInt16();
				num = br.ReadInt32();
				if (num != -1)
				{
					this.extractionPointsOnMap = new List<ExtractionPoint>();
					for (i = 0; i < num; i++)
					{
						ExtractionPoint extractionPoint = new ExtractionPoint();
						extractionPoint.DeserializeInContext(br, TransferContext.GameMapOverview);
						this.extractionPointsOnMap.Add(extractionPoint);
					}
				}
				return;
			}
			case TransferContext.GuildsRanking:
			{
				this.DeserializeGuildRanking(br);
				return;
			}
			case TransferContext.MultiKill:
			{
				this.playerId = br.ReadInt64();
				this.killCount = br.ReadByte();
				this.expReward = br.ReadInt32();
				return;
			}
			case TransferContext.ExpandShipSlots:
			{
				this.wantedSlot = (ExtendedSlot)br.ReadByte();
				this.paymentCurrency = (SelectedCurrency)br.ReadByte();
				return;
			}
			case TransferContext.RerollItem:
			{
				this.wantedBonusRandom = br.ReadByte();
				this.wantedBonusOne = br.ReadByte();
				this.wantedBonusTwo = br.ReadByte();
				this.wantedBonusThree = br.ReadByte();
				this.wantedBonusFour = br.ReadByte();
				this.wantedBonusFive = br.ReadByte();
				this.maximaizedBonusRandom = br.ReadByte();
				this.maximaizedBonusOne = br.ReadByte();
				this.maximaizedBonusTwo = br.ReadByte();
				this.maximaizedBonusThree = br.ReadByte();
				this.maximaizedBonusFour = br.ReadByte();
				this.maximaizedBonusFive = br.ReadByte();
				this.rerollItemType = br.ReadUInt16();
				this.rerollItemSlotId = br.ReadUInt16();
				this.paymentCurrency = (SelectedCurrency)br.ReadByte();
				return;
			}
			case TransferContext.SocialIteraction:
			{
				this.playerId = br.ReadInt64();
				this.socialInteractionIndex = br.ReadByte();
				this.socialInteractionCustomText = br.ReadString();
				return;
			}
			case TransferContext.UnlockedPortals:
			{
				int num3 = br.ReadInt32();
				this.unlockedPortals = new List<int>();
				for (i = 0; i < num3; i++)
				{
					this.unlockedPortals.Add(br.ReadInt32());
				}
				return;
			}
			case TransferContext.Transformer:
			{
				this.isForGuild = br.ReadBoolean();
				this.intensity = br.ReadByte();
				this.paymentCurrency = (SelectedCurrency)br.ReadByte();
				return;
			}
			case TransferContext.TransformerReward:
			{
				this.transformerRewardType = br.ReadUInt16();
				this.transformerRewardAmount = br.ReadInt32();
				return;
			}
			case TransferContext.ChangeTransformerState:
			{
				this.transformerState = br.ReadByte();
				return;
			}
			case TransferContext.DespawnPve:
			{
				this.neighbourhoodId = br.ReadUInt32();
				return;
			}
			case TransferContext.QuestEngine:
			{
				this.questEnginCommand = (QuestEngineEnum)br.ReadByte();
				this.questEngineId = br.ReadInt32();
				this.playerObjectives = new PlayerObjectives();
				this.playerObjectives.Deserialize(br);
				return;
			}
			case TransferContext.CheckpointAction:
			{
				this.checkpointId = br.ReadInt32();
				return;
			}
			case TransferContext.TalkToNpc:
			{
				this.npcKey = br.ReadInt32();
				return;
			}
			case TransferContext.BringToNpc:
			{
				this.questEngineId = br.ReadInt32();
				this.checkpointId = br.ReadInt32();
				this.npcKey = br.ReadInt32();
				return;
			}
			case TransferContext.GetDailyQuests:
			{
				int num4 = br.ReadInt32();
				this.questList = new NewQuest[num4];
				for (i = 0; i < num4; i++)
				{
					NewQuest newQuest = new NewQuest();
					newQuest.Deserialize(br);
					this.questList[i] = newQuest;
				}
				return;
			}
			case TransferContext.ServerMessage:
			{
				this.serverMessageIndex = br.ReadByte();
				return;
			}
			case TransferContext.SavePlayerSkills:
			{
				int num5 = br.ReadInt32();
				this.wantedSkills = new SortedList<ushort, byte>();
				if (num5 != -1)
				{
					for (i = 0; i < num5; i++)
					{
						this.wantedSkills.Add(br.ReadUInt16(), br.ReadByte());
					}
				}
				return;
			}
			case TransferContext.CancelGalaxyJump:
			{
				this.playerId = br.ReadInt64();
				return;
			}
			case TransferContext.UpdatePendingAwards:
			{
				int num6 = br.ReadInt32();
				this.pendindAwards = new SortedList<int, PlayerPendingAward>();
				if (num6 != -1)
				{
					for (i = 0; i < num6; i++)
					{
						PlayerPendingAward playerPendingAward = new PlayerPendingAward();
						playerPendingAward.Deserialize(br);
						this.pendindAwards.Add(playerPendingAward.rewardId, playerPendingAward);
					}
				}
				this.receivedDailyRewards = br.ReadByte();
				return;
			}
			case TransferContext.ClaimPendingAward:
			{
				this.pendingAwardId = br.ReadInt32();
				return;
			}
			case TransferContext.CriticalHit:
			{
				this.criticalHitPlayerId = br.ReadInt64();
				this.criticalHitTargetNbId = br.ReadUInt32();
				this.criticalHitBonus = br.ReadSingle();
				return;
			}
			case TransferContext.EnergyBarFull:
			{
				this.criticalComboCnt = br.ReadByte();
				return;
			}
			case TransferContext.SpeedChange:
			{
				this.playerId = br.ReadInt64();
				this.newSpeed = br.ReadSingle();
				this.isBoostActive = br.ReadBoolean();
				return;
			}
			case TransferContext.ReciveQuestInfo:
			{
				this.questList = new NewQuest[1];
				NewQuest newQuest1 = new NewQuest();
				newQuest1.Deserialize(br);
				this.questList[0] = newQuest1;
				return;
			}
			case TransferContext.InitializeJump:
			{
				this.jumpType = (Jump)br.ReadByte();
				this.destinationGalaxyId = br.ReadInt16();
				return;
			}
			case TransferContext.UpdateAccessLevel:
			{
				this.playerAccessLevel = br.ReadByte();
				return;
			}
			case TransferContext.UpdateDailyQuestsDone:
			{
				this.questEnginCommand = (QuestEngineEnum)br.ReadByte();
				this.questEngineId = br.ReadInt32();
				return;
			}
			case TransferContext.UpdateKeyboard:
			{
				this.keyboardUpdatePair = new KeyboardShortcutPair();
				this.keyboardUpdatePair.Deserialize(br);
				return;
			}
			case TransferContext.UpdatePvPLeagueRank:
			{
				this.playerId = br.ReadInt64();
				this.playerLeague = (PvPLeague)br.ReadByte();
				this.inPvPRank = br.ReadBoolean();
				return;
			}
			case TransferContext.UpdatePvPLeagueWinners:
			{
				int num7 = br.ReadInt32();
				if (num7 != -1)
				{
					this.pvpRewardedPlayers = new PvPLeagueRewardetPlayer[num7];
					for (i = 0; i < num7; i++)
					{
						this.pvpRewardedPlayers[i] = new PvPLeagueRewardetPlayer();
						this.pvpRewardedPlayers[i].league = (PvPLeague)br.ReadByte();
						this.pvpRewardedPlayers[i].rankPossition = br.ReadByte();
						this.pvpRewardedPlayers[i].nickName = br.ReadString();
						this.pvpRewardedPlayers[i].fractionId = br.ReadByte();
					}
				}
				return;
			}
			case TransferContext.UpdatePvPRoundTime:
			{
				int num8 = br.ReadInt32();
				if (num8 != -1)
				{
					this.nextPvPRoundTime = StaticData.now.AddSeconds((double)num8);
				}
				else
				{
					this.nextPvPRoundTime = DateTime.MinValue;
				}
				return;
			}
			case TransferContext.UpdatePvPStartTimePool:
			{
				this.pvpGamePoolCapacity = br.ReadByte();
				if (this.pvpGamePoolCapacity == 0)
				{
					this.nextPvPRoundTime = DateTime.MinValue;
				}
				else
				{
					this.nextPvPRoundTime = StaticData.now.AddSeconds((double)br.ReadInt32());
				}
				return;
			}
			case TransferContext.UpdatePvPSignedPlayers:
			{
				this.signedPlayerName = br.ReadString();
				this.signedPlayerLevel = br.ReadByte();
				this.signedPlayersCount = br.ReadInt16();
				return;
			}
			case TransferContext.UpdateShipStatsAppearance:
			{
				this.isShowMoreDetailsOn = br.ReadBoolean();
				return;
			}
			case TransferContext.UpdatePlayerSpeed:
			{
				this.newSpeed = br.ReadSingle();
				this.playerId = br.ReadInt64();
				return;
			}
			case TransferContext.UpdatePlayerStun:
			{
				this.isStunned = br.ReadBoolean();
				this.playerId = br.ReadInt64();
				return;
			}
			case TransferContext.UpdateSelectedPoP:
			{
				this.neighbourhoodId = br.ReadUInt32();
				this.selectedPoPnbId = br.ReadUInt32();
				return;
			}
			case TransferContext.InstanceStatsCheck:
			{
				this.instanceGalaxyId = br.ReadInt32();
				this.selectedDificulty = (InstanceDifficulty)br.ReadByte();
				this.instanceStatus = (InstanceStatus)br.ReadByte();
				this.instanceKillTarget = br.ReadInt16();
				this.instanceKillProgress = br.ReadInt16();
				return;
			}
			case TransferContext.SendGiftRequest:
			{
				this.selectedGiftType = br.ReadUInt16();
				this.receiverNickname = br.ReadString();
				this.giftTitle = br.ReadString();
				this.isFreeGift = br.ReadBoolean();
				this.isAnonymously = br.ReadBoolean();
				return;
			}
			case TransferContext.UpgradeGuardianSkillTree:
			{
				this.pointId = br.ReadInt16();
				this.poiUnityType = br.ReadByte();
				int num9 = br.ReadInt32();
				if (num9 != -1)
				{
					this.wantedGuardianSkills = new SortedList<short, byte>();
					for (int j = 0; j < num9; j++)
					{
						short num10 = br.ReadInt16();
						num1 = br.ReadByte();
						this.wantedGuardianSkills.Add(num10, num1);
					}
				}
				else
				{
					this.wantedGuardianSkills = null;
				}
				return;
			}
			case TransferContext.ResetGuardianSkillTree:
			{
				this.pointId = br.ReadInt16();
				this.poiUnityType = br.ReadByte();
				this.wallet = (SelectedWallet)br.ReadByte();
				return;
			}
			case TransferContext.ActivatePoIDamageReductionBoost:
			{
				this.pointId = br.ReadInt16();
				this.wallet = (SelectedWallet)br.ReadByte();
				this.paymentCurrency = (SelectedCurrency)br.ReadByte();
				return;
			}
			case TransferContext.VoteForPlayer:
			{
				this.candidateName = br.ReadString();
				this.voteDonation = br.ReadInt64();
				return;
			}
			case TransferContext.VoteForGalaxy:
			{
				this.selectedGalaxyId = br.ReadInt16();
				this.voteDonation = br.ReadInt64();
				return;
			}
			case TransferContext.DonateForFaction:
			{
				this.voteDonation = br.ReadInt64();
				return;
			}
			case TransferContext.FactionWarPlayerList:
			{
				int num11 = br.ReadInt32();
				this.factionCouncils = new List<KeyValuePair<string, long>>();
				if (num11 == -1)
				{
					this.factionCouncils = new List<KeyValuePair<string, long>>();
				}
				else
				{
					for (i = 0; i < num11; i++)
					{
						str = br.ReadString();
						num2 = br.ReadInt64();
						this.factionCouncils.Add(new KeyValuePair<string, long>(str, num2));
					}
				}
				this.voteDonation = br.ReadInt64();
				return;
			}
			case TransferContext.FactionBank:
			{
				this.voteDonation = br.ReadInt64();
				this.factionOneScore = br.ReadInt64();
				this.factionTwoScore = br.ReadInt64();
				this.factionOneDayScore = br.ReadInt64();
				this.factionTwoDayScore = br.ReadInt64();
				this.loosingFaction = br.ReadByte();
				this.loosingFactionBonusPercent = br.ReadByte();
				this.dailyLoosingFaction = br.ReadByte();
				this.dailyLoosingFactionBonusPercent = br.ReadInt16();
				return;
			}
			case TransferContext.UpdateFactionWarPaidAd:
			{
				this.paidAdNickName = br.ReadString();
				this.paidAdSlogan = br.ReadString();
				this.nextPaidAdPrice = br.ReadInt32();
				return;
			}
			case TransferContext.FactionWarVoteForPlayerDay:
			{
				int num12 = br.ReadInt32();
				this.factionCouncils = new List<KeyValuePair<string, long>>();
				if (num12 == -1)
				{
					this.factionCouncils = new List<KeyValuePair<string, long>>();
				}
				else
				{
					for (i = 0; i < num12; i++)
					{
						str = br.ReadString();
						num2 = br.ReadInt64();
						this.factionCouncils.Add(new KeyValuePair<string, long>(str, num2));
					}
				}
				this.voteDonation = br.ReadInt64();
				this.paidAdNickName = br.ReadString();
				this.paidAdSlogan = br.ReadString();
				this.nextPaidAdPrice = br.ReadInt32();
				return;
			}
			case TransferContext.UpdateFactionWarParticipation:
			{
				this.councilRank = br.ReadByte();
				this.selectedCouncilSkillId = br.ReadUInt16();
				this.day1Participation = br.ReadBoolean();
				this.day2Participation = br.ReadBoolean();
				this.day3Participation = br.ReadBoolean();
				this.day4Participation = br.ReadBoolean();
				this.day5Participation = br.ReadBoolean();
				this.day6Participation = br.ReadBoolean();
				this.factionWarDayScore = br.ReadInt32();
				this.weeklyRewardCollected = br.ReadBoolean();
				this.lastWeekPendingReward = br.ReadByte();
				return;
			}
			case TransferContext.UpdateFactionWarStage:
			{
				this.factionWarDay = (DayOfWeek)br.ReadByte();
				this.myBattleBoostVote = br.ReadByte();
				this.myUtilityBoostVote = br.ReadByte();
				int num13 = br.ReadInt32();
				if (num13 != -1)
				{
					this.factionWarDayEndTime = StaticData.now.AddSeconds((double)num13);
				}
				else
				{
					this.factionWarDayEndTime = DateTime.MinValue;
				}
				return;
			}
			case TransferContext.PlayerDailyScore:
			{
				this.factionWarDayScore = br.ReadInt32();
				this.dailyReward1Collected = br.ReadBoolean();
				this.dailyReward2Collected = br.ReadBoolean();
				this.dailyReward3Collected = br.ReadBoolean();
				this.factionOneScore = br.ReadInt64();
				this.factionOneDayScore = br.ReadInt64();
				this.factionTwoScore = br.ReadInt64();
				this.factionTwoDayScore = br.ReadInt64();
				this.loosingFaction = br.ReadByte();
				this.loosingFactionBonusPercent = br.ReadByte();
				this.dailyLoosingFaction = br.ReadByte();
				this.dailyLoosingFactionBonusPercent = br.ReadInt16();
				return;
			}
			case TransferContext.FactionsCouncils:
			{
				this.factionOneCouncil = new List<FactionCouncilMember>();
				this.factionTwoCouncil = new List<FactionCouncilMember>();
				int num14 = br.ReadInt32();
				if (num14 != -1)
				{
					for (i = 0; i < num14; i++)
					{
						factionCouncilMember = new FactionCouncilMember()
						{
							rank = br.ReadByte(),
							guildTag = br.ReadString(),
							nickName = br.ReadString()
						};
						this.factionOneCouncil.Add(factionCouncilMember);
					}
				}
				num14 = br.ReadInt32();
				if (num14 != -1)
				{
					for (i = 0; i < num14; i++)
					{
						factionCouncilMember = new FactionCouncilMember()
						{
							rank = br.ReadByte(),
							guildTag = br.ReadString(),
							nickName = br.ReadString()
						};
						this.factionTwoCouncil.Add(factionCouncilMember);
					}
				}
				return;
			}
			case TransferContext.FactionBoostsVotes:
			{
				this.battleBoost1Votes = br.ReadByte();
				this.battleBoost2Votes = br.ReadByte();
				this.battleBoost3Votes = br.ReadByte();
				this.battleBoostVeto = br.ReadByte();
				this.utilityBoost1Votes = br.ReadByte();
				this.utilityBoost2Votes = br.ReadByte();
				this.utilityBoost3Votes = br.ReadByte();
				this.utilityBoostVeto = br.ReadByte();
				this.myBattleBoostVote = br.ReadByte();
				this.myUtilityBoostVote = br.ReadByte();
				return;
			}
			case TransferContext.VoteForFactionBoost:
			{
				this.myVoteForFactionBoost = br.ReadByte();
				return;
			}
			case TransferContext.GetGalaxyVote:
			{
				int num15 = br.ReadInt32();
				this.galaxyVote = new List<KeyValuePair<short, long>>();
				if (num15 != 0)
				{
					for (i = 0; i < num15; i++)
					{
						short num16 = br.ReadInt16();
						KeyValuePair<short, long> keyValuePair = new KeyValuePair<short, long>(num16, br.ReadInt64());
						this.galaxyVote.Add(keyValuePair);
					}
				}
				return;
			}
			case TransferContext.WeeklyRewardsUpdate:
			{
				this.weeklyRewardCollected = br.ReadBoolean();
				this.lastWeekPendingReward = br.ReadByte();
				return;
			}
			case TransferContext.FactionMessages:
			{
				this.yourFactionToYou = br.ReadString();
				this.yourFactionToEnemy = br.ReadString();
				this.enemyFactionToYou = br.ReadString();
				return;
			}
			case TransferContext.EpsOverview:
			{
				int num17 = br.ReadInt32();
				this.epsStateInfo = new List<ExtractionPointStateInfo>();
				for (i = 0; i < num17; i++)
				{
					ExtractionPointStateInfo extractionPointStateInfo = new ExtractionPointStateInfo()
					{
						epId = br.ReadInt32(),
						isVulnerable = br.ReadBoolean(),
						until = StaticData.now.AddSeconds((double)br.ReadInt32()),
						ownerFaction = br.ReadByte(),
						contributors = br.ReadByte(),
						yourIncome = br.ReadInt16(),
						tottalViralIncome = br.ReadInt16(),
						guildIncome = br.ReadByte(),
						tottalGuildIncome = br.ReadByte()
					};
					this.epsStateInfo.Add(extractionPointStateInfo);
				}
				return;
			}
			case TransferContext.ErrorCode:
			{
				this.errorCodeIndex = br.ReadByte();
				return;
			}
			case TransferContext.FactionGalaxyOwnership:
			{
				this.allFactionGalaxiesOwnership = new SortedList<byte, byte>();
				int num18 = br.ReadByte();
				for (i = 0; i < num18; i++)
				{
					byte num19 = br.ReadByte();
					num1 = br.ReadByte();
					this.allFactionGalaxiesOwnership.Add(num19, num1);
				}
				this.factionOneMostWantedGalaxy = br.ReadByte();
				this.factionTwoMostWantedGalaxy = br.ReadByte();
				this.isWarInProgress = br.ReadBoolean();
				int num20 = br.ReadInt32();
				if (num20 != -1)
				{
					this.nextWarStartTime = StaticData.now.AddSeconds((double)num20);
				}
				else
				{
					this.nextWarStartTime = DateTime.MinValue;
				}
				return;
			}
			case TransferContext.UpdatePlayerDisarm:
			{
				this.isDisarmed = br.ReadBoolean();
				this.playerId = br.ReadInt64();
				return;
			}
			case TransferContext.UpdatePlayerShock:
			{
				this.isShocked = br.ReadBoolean();
				this.playerId = br.ReadInt64();
				return;
			}
			case TransferContext.CouncilMemberSelecktSkill:
			{
				this.councilSkillId = br.ReadUInt16();
				return;
			}
			case TransferContext.PartyMemberStatsUpdate:
			{
				this.partyMemberInfo = new PartyMemberInfo()
				{
					playerId = br.ReadInt64(),
					coordinateX = br.ReadSingle(),
					coordinateZ = br.ReadSingle(),
					galaxyId = br.ReadInt16(),
					shieldPercent = br.ReadSingle(),
					corpusPercent = br.ReadSingle()
				};
				return;
			}
			case TransferContext.DeviceInfo:
			{
				this.deviceName = br.ReadString();
				this.deviceType = br.ReadString();
				this.deviceModel = br.ReadString();
				this.deviceId = br.ReadString();
				return;
			}
			case TransferContext.DeletePrivateMessage:
			{
				this.messageId = br.ReadInt32();
				return;
			}
			case TransferContext.OpenGameMessages:
			{
				this.gameMsgId = br.ReadInt32();
				this.privateMsgId = br.ReadInt32();
				return;
			}
			case TransferContext.UpdateGameMessages:
			case TransferContext.NewAnnouncementReceived:
			{
				num = br.ReadInt32();
				this.playerGameMessages = new List<GameMessage>();
				for (i = 0; i < num; i++)
				{
					GameMessage gameMessage = new GameMessage()
					{
						id = br.ReadInt32(),
						isNew = br.ReadBoolean(),
						link = br.ReadString(),
						senderName = br.ReadString(),
						text = br.ReadString(),
						title = br.ReadString(),
						type = (GameMessageType)br.ReadByte()
					};
					int num21 = br.ReadInt32();
					gameMessage.reciveTime = StaticData.now.AddSeconds((double)num21);
					this.playerGameMessages.Add(gameMessage);
				}
				return;
			}
			case TransferContext.WarCommendationsReceived:
			{
				this.errorCodeIndex = br.ReadByte();
				return;
			}
			case TransferContext.UpdateWarCommendationsBought:
			{
				this.errorCodeIndex = br.ReadByte();
				return;
			}
			default:
			{
				return;
			}
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		GameObjectPhysics.Log("UNSUPPORTED Serialize!");
		throw new NotImplementedException();
	}

	private void SerializeGuildRanking(BinaryWriter bw)
	{
		bw.Write(this.guilds.Count);
		foreach (Guild guild in this.guilds)
		{
			bw.Write(guild.id);
			bw.Write(guild.fractionId);
			bw.Write(guild.name);
			bw.Write(guild.title);
			bw.Write(guild.members.Count);
			bw.Write(guild.bankUltralibrium);
			bw.Write(guild.upgradeOneLevel);
			bw.Write(guild.upgradeTwoLevel);
			bw.Write(guild.upgradeThreeLevel);
			bw.Write(guild.upgradeFourLevel);
			bw.Write(guild.upgradeFiveLevel);
		}
	}

	public void SerializeInContext(BinaryWriter bw, TransferContext context)
	{
		int i;
		KeyValuePair<string, long> factionCouncil = new KeyValuePair<string, long>();
		int count;
		TimeSpan item;
		switch (context)
		{
			case TransferContext.GuildUpgrade:
			{
				bw.Write((byte)this.paymentCurrency);
				bw.Write(this.wantedGuildUpgradeType);
				return;
			}
			case TransferContext.GuildInvite:
			case TransferContext.GuildInvited:
			case TransferContext.GuildInviteError:
			case TransferContext.EpContributors:
			case TransferContext.GuildInviteRemove:
			case TransferContext.GuildInviteReject:
			case TransferContext.GuildInviteAccept:
			case TransferContext.GuildCurrentUserRank:
			case TransferContext.GuildRankChange:
			case TransferContext.GuildRankAdd:
			case TransferContext.GuildRankDelete:
			case TransferContext.GuildEpInfoRequest:
			case TransferContext.GuildEpInfoResponse:
			case TransferContext.InitialRequestV1:
			case TransferContext.GetDailyMissionsReward:
			case TransferContext.MiningStationProgressUpdate:
			{
				return;
			}
			case TransferContext.GuildRankUpdate:
			{
				bw.Write(this.myGuildRank.canBank);
				bw.Write(this.myGuildRank.canChat);
				bw.Write(this.myGuildRank.canEditDetails);
				bw.Write(this.myGuildRank.canInvite);
				bw.Write(this.myGuildRank.canPromote);
				bw.Write(this.myGuildRank.canVault);
				return;
			}
			case TransferContext.FractionOverview:
			{
				this.fractionOneInfo.Serialize(bw);
				this.fractionTwoInfo.Serialize(bw);
				return;
			}
			case TransferContext.GameMapOverview:
			{
				bw.Write(this.galaxyId);
				if (this.extractionPointsOnMap != null)
				{
					bw.Write(this.extractionPointsOnMap.Count);
					for (i = 0; i < this.extractionPointsOnMap.Count; i++)
					{
						this.extractionPointsOnMap[i].SerializeInContext(bw, TransferContext.GameMapOverview);
					}
				}
				else
				{
					bw.Write(-1);
				}
				return;
			}
			case TransferContext.GuildsRanking:
			{
				this.SerializeGuildRanking(bw);
				return;
			}
			case TransferContext.MultiKill:
			{
				bw.Write(this.playerId);
				bw.Write(this.killCount);
				bw.Write(this.expReward);
				return;
			}
			case TransferContext.ExpandShipSlots:
			{
				bw.Write((byte)this.wantedSlot);
				bw.Write((byte)this.paymentCurrency);
				return;
			}
			case TransferContext.RerollItem:
			{
				bw.Write(this.wantedBonusRandom);
				bw.Write(this.wantedBonusOne);
				bw.Write(this.wantedBonusTwo);
				bw.Write(this.wantedBonusThree);
				bw.Write(this.wantedBonusFour);
				bw.Write(this.wantedBonusFive);
				bw.Write(this.maximaizedBonusRandom);
				bw.Write(this.maximaizedBonusOne);
				bw.Write(this.maximaizedBonusTwo);
				bw.Write(this.maximaizedBonusThree);
				bw.Write(this.maximaizedBonusFour);
				bw.Write(this.maximaizedBonusFive);
				bw.Write(this.rerollItemType);
				bw.Write(this.rerollItemSlotId);
				bw.Write((byte)this.paymentCurrency);
				return;
			}
			case TransferContext.SocialIteraction:
			{
				bw.Write(this.playerId);
				bw.Write(this.socialInteractionIndex);
				bw.Write(this.socialInteractionCustomText ?? "");
				return;
			}
			case TransferContext.UnlockedPortals:
			{
				bw.Write(this.unlockedPortals.Count);
				for (i = 0; i < this.unlockedPortals.Count; i++)
				{
					bw.Write(this.unlockedPortals[i]);
				}
				return;
			}
			case TransferContext.Transformer:
			{
				bw.Write(this.isForGuild);
				bw.Write(this.intensity);
				bw.Write((byte)this.paymentCurrency);
				return;
			}
			case TransferContext.TransformerReward:
			{
				bw.Write(this.transformerRewardType);
				bw.Write(this.transformerRewardAmount);
				return;
			}
			case TransferContext.ChangeTransformerState:
			{
				bw.Write(this.transformerState);
				return;
			}
			case TransferContext.DespawnPve:
			{
				bw.Write(this.neighbourhoodId);
				return;
			}
			case TransferContext.QuestEngine:
			{
				bw.Write((byte)this.questEnginCommand);
				bw.Write(this.questEngineId);
				this.playerObjectives.Serialize(bw);
				return;
			}
			case TransferContext.CheckpointAction:
			{
				bw.Write(this.checkpointId);
				return;
			}
			case TransferContext.TalkToNpc:
			{
				bw.Write(this.npcKey);
				return;
			}
			case TransferContext.BringToNpc:
			{
				bw.Write(this.questEngineId);
				bw.Write(this.checkpointId);
				bw.Write(this.npcKey);
				return;
			}
			case TransferContext.GetDailyQuests:
			{
				int length = (int)this.questList.Length;
				bw.Write(length);
				for (i = 0; i < length; i++)
				{
					this.questList[i].Serialize(bw);
				}
				return;
			}
			case TransferContext.ServerMessage:
			{
				bw.Write(this.serverMessageIndex);
				return;
			}
			case TransferContext.SavePlayerSkills:
			{
				if (this.wantedSkills == null)
				{
					bw.Write(-1);
				}
				else
				{
					int num = this.wantedSkills.Count;
					bw.Write(num);
					for (i = 0; i < num; i++)
					{
						bw.Write(this.wantedSkills.Keys[i]);
						bw.Write(this.wantedSkills.Values[i]);
					}
				}
				return;
			}
			case TransferContext.CancelGalaxyJump:
			{
				bw.Write(this.playerId);
				return;
			}
			case TransferContext.UpdatePendingAwards:
			{
				if (this.pendindAwards == null)
				{
					bw.Write(-1);
				}
				else
				{
					bw.Write(this.pendindAwards.Count);
					foreach (PlayerPendingAward value in this.pendindAwards.Values)
					{
						value.Serialize(bw);
					}
				}
				bw.Write(this.receivedDailyRewards);
				return;
			}
			case TransferContext.ClaimPendingAward:
			{
				bw.Write(this.pendingAwardId);
				return;
			}
			case TransferContext.CriticalHit:
			{
				bw.Write(this.criticalHitPlayerId);
				bw.Write(this.criticalHitTargetNbId);
				bw.Write(this.criticalHitBonus);
				return;
			}
			case TransferContext.EnergyBarFull:
			{
				bw.Write(this.criticalComboCnt);
				return;
			}
			case TransferContext.SpeedChange:
			{
				bw.Write(this.playerId);
				bw.Write(this.newSpeed);
				bw.Write(this.isBoostActive);
				return;
			}
			case TransferContext.ReciveQuestInfo:
			{
				if ((this.questList == null ? false : this.questList[0] != null))
				{
					this.questList[0].Serialize(bw);
				}
				return;
			}
			case TransferContext.InitializeJump:
			{
				bw.Write((byte)this.jumpType);
				bw.Write(this.destinationGalaxyId);
				return;
			}
			case TransferContext.UpdateAccessLevel:
			{
				bw.Write(this.playerAccessLevel);
				return;
			}
			case TransferContext.UpdateDailyQuestsDone:
			{
				bw.Write((byte)this.questEnginCommand);
				bw.Write(this.questEngineId);
				return;
			}
			case TransferContext.UpdateKeyboard:
			{
				if (this.keyboardUpdatePair != null)
				{
					this.keyboardUpdatePair.Serialize(bw);
				}
				return;
			}
			case TransferContext.UpdatePvPLeagueRank:
			{
				bw.Write(this.playerId);
				bw.Write((byte)this.playerLeague);
				bw.Write(this.inPvPRank);
				return;
			}
			case TransferContext.UpdatePvPLeagueWinners:
			{
				if (this.pvpRewardedPlayers != null)
				{
					int length1 = (int)this.pvpRewardedPlayers.Length;
					bw.Write(length1);
					for (i = 0; i < length1; i++)
					{
						bw.Write((byte)this.pvpRewardedPlayers[i].league);
						bw.Write(this.pvpRewardedPlayers[i].rankPossition);
						bw.Write(this.pvpRewardedPlayers[i].nickName ?? "");
						bw.Write(this.pvpRewardedPlayers[i].fractionId);
					}
				}
				else
				{
					bw.Write(-1);
				}
				return;
			}
			case TransferContext.UpdatePvPRoundTime:
			{
				if (!(this.nextPvPRoundTime != DateTime.MinValue))
				{
					bw.Write(-1);
				}
				else
				{
					item = this.nextPvPRoundTime - StaticData.now;
					bw.Write((int)item.TotalSeconds);
				}
				return;
			}
			case TransferContext.UpdatePvPStartTimePool:
			{
				bw.Write(this.pvpGamePoolCapacity);
				if (this.pvpGamePoolCapacity != 0)
				{
					item = this.nextPvPRoundTime - StaticData.now;
					bw.Write((int)item.TotalSeconds);
				}
				return;
			}
			case TransferContext.UpdatePvPSignedPlayers:
			{
				bw.Write(this.signedPlayerName ?? "");
				bw.Write(this.signedPlayerLevel);
				bw.Write(this.signedPlayersCount);
				return;
			}
			case TransferContext.UpdateShipStatsAppearance:
			{
				bw.Write(this.isShowMoreDetailsOn);
				return;
			}
			case TransferContext.UpdatePlayerSpeed:
			{
				bw.Write(this.newSpeed);
				bw.Write(this.playerId);
				return;
			}
			case TransferContext.UpdatePlayerStun:
			{
				bw.Write(this.isStunned);
				bw.Write(this.playerId);
				return;
			}
			case TransferContext.UpdateSelectedPoP:
			{
				bw.Write(this.neighbourhoodId);
				bw.Write(this.selectedPoPnbId);
				return;
			}
			case TransferContext.InstanceStatsCheck:
			{
				bw.Write(this.instanceGalaxyId);
				bw.Write((byte)this.selectedDificulty);
				bw.Write((byte)this.instanceStatus);
				bw.Write(this.instanceKillTarget);
				bw.Write(this.instanceKillProgress);
				return;
			}
			case TransferContext.SendGiftRequest:
			{
				bw.Write(this.selectedGiftType);
				bw.Write(this.receiverNickname ?? "");
				bw.Write(this.giftTitle ?? "");
				bw.Write(this.isFreeGift);
				bw.Write(this.isAnonymously);
				return;
			}
			case TransferContext.UpgradeGuardianSkillTree:
			{
				bw.Write(this.pointId);
				bw.Write(this.poiUnityType);
				if (this.wantedGuardianSkills == null)
				{
					bw.Write(-1);
				}
				else
				{
					int count1 = this.wantedGuardianSkills.Count;
					bw.Write(count1);
					for (int j = 0; j < count1; j++)
					{
						KeyValuePair<short, byte> keyValuePair = this.wantedGuardianSkills.ElementAt<KeyValuePair<short, byte>>(j);
						bw.Write(keyValuePair.Key);
						bw.Write(keyValuePair.Value);
					}
				}
				return;
			}
			case TransferContext.ResetGuardianSkillTree:
			{
				bw.Write(this.pointId);
				bw.Write(this.poiUnityType);
				bw.Write((byte)this.wallet);
				return;
			}
			case TransferContext.ActivatePoIDamageReductionBoost:
			{
				bw.Write(this.pointId);
				bw.Write((byte)this.wallet);
				bw.Write((byte)this.paymentCurrency);
				return;
			}
			case TransferContext.VoteForPlayer:
			{
				bw.Write(this.candidateName ?? "");
				bw.Write(this.voteDonation);
				return;
			}
			case TransferContext.VoteForGalaxy:
			{
				bw.Write(this.selectedGalaxyId);
				bw.Write(this.voteDonation);
				return;
			}
			case TransferContext.DonateForFaction:
			{
				bw.Write(this.voteDonation);
				return;
			}
			case TransferContext.FactionWarPlayerList:
			{
				if (this.factionCouncils == null)
				{
					bw.Write(-1);
				}
				else
				{
					bw.Write(this.factionCouncils.Count);
					foreach (KeyValuePair<string, long> factionCounl in this.factionCouncils)
					{
						bw.Write(factionCounl.Key ?? "");
						bw.Write(factionCounl.Value);
					}
				}
				bw.Write(this.voteDonation);
				return;
			}
			case TransferContext.FactionBank:
			{
				bw.Write(this.voteDonation);
				bw.Write(this.factionOneScore);
				bw.Write(this.factionTwoScore);
				bw.Write(this.factionOneDayScore);
				bw.Write(this.factionTwoDayScore);
				bw.Write(this.loosingFaction);
				bw.Write(this.loosingFactionBonusPercent);
				bw.Write(this.dailyLoosingFaction);
				bw.Write(this.dailyLoosingFactionBonusPercent);
				return;
			}
			case TransferContext.UpdateFactionWarPaidAd:
			{
				bw.Write(this.paidAdNickName ?? "");
				bw.Write(this.paidAdSlogan ?? "");
				bw.Write(this.nextPaidAdPrice);
				return;
			}
			case TransferContext.FactionWarVoteForPlayerDay:
			{
				if (this.factionCouncils == null)
				{
					bw.Write(-1);
				}
				else
				{
					bw.Write(this.factionCouncils.Count);
					foreach (KeyValuePair<string, long> factionCouncil1 in this.factionCouncils)
					{
						bw.Write(factionCouncil1.Key ?? "");
						bw.Write(factionCouncil1.Value);
					}
				}
				bw.Write(this.voteDonation);
				bw.Write(this.paidAdNickName ?? "");
				bw.Write(this.paidAdSlogan ?? "");
				bw.Write(this.nextPaidAdPrice);
				return;
			}
			case TransferContext.UpdateFactionWarParticipation:
			{
				bw.Write(this.councilRank);
				bw.Write(this.selectedCouncilSkillId);
				bw.Write(this.day1Participation);
				bw.Write(this.day2Participation);
				bw.Write(this.day3Participation);
				bw.Write(this.day4Participation);
				bw.Write(this.day5Participation);
				bw.Write(this.day6Participation);
				bw.Write(this.factionWarDayScore);
				bw.Write(this.weeklyRewardCollected);
				bw.Write(this.lastWeekPendingReward);
				return;
			}
			case TransferContext.UpdateFactionWarStage:
			{
				bw.Write((byte)this.factionWarDay);
				bw.Write(this.myBattleBoostVote);
				bw.Write(this.myUtilityBoostVote);
				if (!(this.factionWarDayEndTime != DateTime.MinValue))
				{
					bw.Write(-1);
				}
				else
				{
					item = this.factionWarDayEndTime - StaticData.now;
					bw.Write((int)item.TotalSeconds);
				}
				return;
			}
			case TransferContext.PlayerDailyScore:
			{
				bw.Write(this.factionWarDayScore);
				bw.Write(this.dailyReward1Collected);
				bw.Write(this.dailyReward2Collected);
				bw.Write(this.dailyReward3Collected);
				bw.Write(this.factionOneScore);
				bw.Write(this.factionOneDayScore);
				bw.Write(this.factionTwoScore);
				bw.Write(this.factionTwoDayScore);
				bw.Write(this.loosingFaction);
				bw.Write(this.loosingFactionBonusPercent);
				bw.Write(this.dailyLoosingFaction);
				bw.Write(this.dailyLoosingFactionBonusPercent);
				return;
			}
			case TransferContext.FactionsCouncils:
			{
				if (this.factionOneCouncil == null)
				{
					bw.Write(-1);
				}
				else
				{
					count = this.factionOneCouncil.Count;
					bw.Write(count);
					for (i = 0; i < count; i++)
					{
						bw.Write(this.factionOneCouncil[i].rank);
						bw.Write(this.factionOneCouncil[i].guildTag ?? "");
						bw.Write(this.factionOneCouncil[i].nickName ?? "");
					}
				}
				if (this.factionTwoCouncil == null)
				{
					bw.Write(-1);
				}
				else
				{
					count = this.factionTwoCouncil.Count;
					bw.Write(count);
					for (i = 0; i < count; i++)
					{
						bw.Write(this.factionTwoCouncil[i].rank);
						bw.Write(this.factionTwoCouncil[i].guildTag ?? "");
						bw.Write(this.factionTwoCouncil[i].nickName ?? "");
					}
				}
				return;
			}
			case TransferContext.FactionBoostsVotes:
			{
				bw.Write(this.battleBoost1Votes);
				bw.Write(this.battleBoost2Votes);
				bw.Write(this.battleBoost3Votes);
				bw.Write(this.battleBoostVeto);
				bw.Write(this.utilityBoost1Votes);
				bw.Write(this.utilityBoost2Votes);
				bw.Write(this.utilityBoost3Votes);
				bw.Write(this.utilityBoostVeto);
				bw.Write(this.myBattleBoostVote);
				bw.Write(this.myUtilityBoostVote);
				return;
			}
			case TransferContext.VoteForFactionBoost:
			{
				bw.Write(this.myVoteForFactionBoost);
				return;
			}
			case TransferContext.GetGalaxyVote:
			{
				if (this.galaxyVote != null)
				{
					int num1 = this.galaxyVote.Count;
					bw.Write(num1);
					for (i = 0; i < num1; i++)
					{
						KeyValuePair<short, long> item1 = this.galaxyVote[i];
						bw.Write(item1.Key);
						item1 = this.galaxyVote[i];
						bw.Write(item1.Value);
					}
				}
				else
				{
					bw.Write(-1);
				}
				return;
			}
			case TransferContext.WeeklyRewardsUpdate:
			{
				bw.Write(this.weeklyRewardCollected);
				bw.Write(this.lastWeekPendingReward);
				return;
			}
			case TransferContext.FactionMessages:
			{
				bw.Write(this.yourFactionToYou);
				bw.Write(this.yourFactionToEnemy);
				bw.Write(this.enemyFactionToYou);
				return;
			}
			case TransferContext.EpsOverview:
			{
				int count2 = this.epsStateInfo.Count;
				bw.Write(count2);
				for (i = 0; i < count2; i++)
				{
					bw.Write(this.epsStateInfo[i].epId);
					bw.Write(this.epsStateInfo[i].isVulnerable);
					item = this.epsStateInfo[i].until - StaticData.now;
					bw.Write((int)item.TotalSeconds);
					bw.Write(this.epsStateInfo[i].ownerFaction);
					bw.Write(this.epsStateInfo[i].contributors);
					bw.Write(this.epsStateInfo[i].yourIncome);
					bw.Write(this.epsStateInfo[i].tottalViralIncome);
					bw.Write(this.epsStateInfo[i].guildIncome);
					bw.Write(this.epsStateInfo[i].tottalGuildIncome);
				}
				return;
			}
			case TransferContext.ErrorCode:
			{
				bw.Write(this.errorCodeIndex);
				return;
			}
			case TransferContext.FactionGalaxyOwnership:
			{
				int num2 = (this.allFactionGalaxiesOwnership != null ? this.allFactionGalaxiesOwnership.Count : 0);
				bw.Write((byte)num2);
				for (i = 0; i < num2; i++)
				{
					bw.Write(this.allFactionGalaxiesOwnership.Keys[i]);
					bw.Write(this.allFactionGalaxiesOwnership.Values[i]);
				}
				bw.Write(this.factionOneMostWantedGalaxy);
				bw.Write(this.factionTwoMostWantedGalaxy);
				bw.Write(this.isWarInProgress);
				if (!(this.nextWarStartTime != DateTime.MinValue))
				{
					bw.Write(-1);
				}
				else
				{
					item = this.nextWarStartTime - StaticData.now;
					bw.Write((int)item.TotalSeconds);
				}
				return;
			}
			case TransferContext.UpdatePlayerDisarm:
			{
				bw.Write(this.isDisarmed);
				bw.Write(this.playerId);
				return;
			}
			case TransferContext.UpdatePlayerShock:
			{
				bw.Write(this.isShocked);
				bw.Write(this.playerId);
				return;
			}
			case TransferContext.CouncilMemberSelecktSkill:
			{
				bw.Write(this.councilSkillId);
				return;
			}
			case TransferContext.PartyMemberStatsUpdate:
			{
				bw.Write(this.partyMemberInfo.playerId);
				bw.Write(this.partyMemberInfo.coordinateX);
				bw.Write(this.partyMemberInfo.coordinateZ);
				bw.Write(this.partyMemberInfo.galaxyId);
				bw.Write(this.partyMemberInfo.shieldPercent);
				bw.Write(this.partyMemberInfo.corpusPercent);
				return;
			}
			case TransferContext.DeviceInfo:
			{
				bw.Write(this.deviceName ?? "");
				bw.Write(this.deviceType ?? "");
				bw.Write(this.deviceModel ?? "");
				bw.Write(this.deviceId ?? "");
				return;
			}
			case TransferContext.DeletePrivateMessage:
			{
				bw.Write(this.messageId);
				return;
			}
			case TransferContext.OpenGameMessages:
			{
				bw.Write(this.gameMsgId);
				bw.Write(this.privateMsgId);
				return;
			}
			case TransferContext.UpdateGameMessages:
			case TransferContext.NewAnnouncementReceived:
			{
				int num3 = this.playerGameMessages.Count<GameMessage>();
				bw.Write(num3);
				for (i = 0; i < num3; i++)
				{
					bw.Write(this.playerGameMessages[i].id);
					bw.Write(this.playerGameMessages[i].isNew);
					bw.Write(this.playerGameMessages[i].link ?? "");
					bw.Write(this.playerGameMessages[i].senderName ?? "");
					bw.Write(this.playerGameMessages[i].text ?? "");
					bw.Write(this.playerGameMessages[i].title ?? "");
					bw.Write((byte)this.playerGameMessages[i].type);
					item = this.playerGameMessages[i].reciveTime - StaticData.now;
					bw.Write((int)item.TotalSeconds);
				}
				return;
			}
			case TransferContext.WarCommendationsReceived:
			{
				bw.Write(this.errorCodeIndex);
				return;
			}
			case TransferContext.UpdateWarCommendationsBought:
			{
				bw.Write(this.errorCodeIndex);
				return;
			}
			default:
			{
				return;
			}
		}
	}
}