using System;

public enum TransferContext
{
	None,
	GuildSendWhole,
	GuildOverviewExternalRequest,
	GuildOverviewExternalResponse,
	GuildOverviewInternal,
	GuildOverviewNone,
	GuildInvitationsList,
	GuildInvitationsListToNonMember,
	GuildHistory,
	GuildMembers,
	GuildVault,
	GuildRanks,
	GuildCreateTry,
	GuildSaveTry,
	GuildErrorsCreate,
	GuildErrorsSave,
	GuildErrorCreateAlreadyMember,
	GuildErrorAcceptInvite,
	GuildErrorRemoveMember,
	GuildError,
	GuildErrorDelete,
	GuildErrorsEditDetails,
	GuildErrorsDeposit,
	GuildBankUpdate,
	GuildHistoryUpdate,
	GuildCommonUpdate,
	GuildUpdateWhole,
	GuildLeave,
	GuildRemoveMember,
	GuildUpgrade,
	GuildInvite,
	GuildInvited,
	GuildInviteError,
	EpContributors,
	GuildInviteRemove,
	GuildInviteReject,
	GuildInviteAccept,
	GuildCurrentUserRank,
	GuildRankChange,
	GuildRankAdd,
	GuildRankDelete,
	GuildRankUpdate,
	GuildEpInfoRequest,
	GuildEpInfoResponse,
	FractionOverview,
	GameMapOverview,
	GuildsRanking,
	InitialRequestV1,
	MultiKill,
	ExpandShipSlots,
	RerollItem,
	SocialIteraction,
	UnlockedPortals,
	Transformer,
	TransformerReward,
	ChangeTransformerState,
	DespawnPve,
	QuestEngine,
	CheckpointAction,
	TalkToNpc,
	BringToNpc,
	GetDailyQuests,
	GetDailyMissionsReward,
	ServerMessage,
	SavePlayerSkills,
	CancelGalaxyJump,
	UpdatePendingAwards,
	ClaimPendingAward,
	CriticalHit,
	EnergyBarFull,
	SpeedChange,
	ReciveQuestInfo,
	InitializeJump,
	UpdateAccessLevel,
	UpdateDailyQuestsDone,
	UpdateKeyboard,
	MiningStationProgressUpdate,
	UpdatePvPLeagueRank,
	UpdatePvPLeagueWinners,
	UpdatePvPRoundTime,
	UpdatePvPStartTimePool,
	UpdatePvPSignedPlayers,
	UpdateShipStatsAppearance,
	UpdatePlayerSpeed,
	UpdatePlayerStun,
	UpdateSelectedPoP,
	InstanceStatsCheck,
	SendGiftRequest,
	UpgradeGuardianSkillTree,
	ResetGuardianSkillTree,
	ActivatePoIDamageReductionBoost,
	VoteForPlayer,
	VoteForGalaxy,
	DonateForFaction,
	FactionWarPlayerList,
	FactionBank,
	UpdateFactionWarPaidAd,
	FactionWarVoteForPlayerDay,
	UpdateFactionWarParticipation,
	UpdateFactionWarStage,
	PlayerDailyScore,
	FactionsCouncils,
	FactionBoostsVotes,
	VoteForFactionBoost,
	GetGalaxyVote,
	WeeklyRewardsUpdate,
	FactionMessages,
	EpsOverview,
	ErrorCode,
	FactionGalaxyOwnership,
	UpdatePlayerDisarm,
	UpdatePlayerShock,
	CouncilMemberSelecktSkill,
	PartyMemberStatsUpdate,
	DeviceInfo,
	DeletePrivateMessage,
	OpenGameMessages,
	UpdateGameMessages,
	NewAnnouncementReceived,
	WarCommendationsReceived,
	UpdateWarCommendationsBought
}