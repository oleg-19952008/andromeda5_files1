using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using TransferableObjects;
using UnityEngine;

public class playWebGame : MonoBehaviour
{
	public const string SERVERS_API = "http://127.0.0.1/andro/index.php?platform=exe&ver=1&output=xml";

	public const string REGISTER_API = "http://andromeda5.com/api/andromeda/registration.php";

	public const string PAYMENTS_API = "http://andromeda5.com/api/andromeda/payments.php";

	public const string TEST_LOGIN_COMMAND = "";

	public const int ASSETS_VERSION = 1;

	public const ushort LOGIN_SERVER_PORT = 14000;

	public const float TIPS_CHANGE_TIME = 5f;

	public static GameServer[] GameServers;

	public static string GAME_TYPE;

	public static string GAME_DOMAIN;

	public static string WORLD_ID;

	public static string LOGIN_SERVER_IP;

	public static string ASSET_URL;

	public static string REGISTER_URL_DIRECT;

	public static string URL_FORGOTTEN_PASSWORD;

	public static string AVATARS_URL;

	public static string ON_QUIT_URL_DIRECT;

	public static string PAYMET_URL;

	public static string AdColonyCustomID;

	public static bool isInitialBoot;

	public static bool loadResFromAssets;

	public static AssetManager assets;

	public static SortedList<string, LoadingAsset> allBundles;

	public static SortedList<string, List<AvatarJob>> loadingAvatars;

	public static SortedList<string, Texture2D> loadedAvatars;

	public static AuthorizeResult authorization;

	public static byte universeId;

	private static PureUdpClient _udp;

	public static GameObject mainMenuGameObject;

	private static bool StartDownload;

	private static bool ServersReady;

	private AsyncOperation loadLevelOperation;

	public static int currentAssetLoad;

	private static int loadLangState;

	public static SortedList<string, string> SupportedLanguages;

	public static string CurrentLanguageKey;

	public static float timeSinceStart;

	private static string[] setInSpace;

	private static string[] setGui;

	private static string[] setInBase;

	private static string[] setTutorial;

	private static string[] setHydra;

	private static string[] setMensa;

	private static string[] setCanisMinor;

	private static string[] setCanisMajor;

	private static string[] setOrion;

	private static string[] setCentaurus;

	private static string[] setCepheus;

	private static string[] setLynx;

	private static string[] setUrsaMinor;

	private static string[] setUrsaMajor;

	private static string[] setTaurus;

	private static string[] setScorpio;

	private static string[] setAndromeda;

	private static string[] setCassiopeia;

	private static string[] setPegasus;

	private static string[] setPerseus;

	private static string[] setInstanceHydraPrime;

	private static string[] setInstanceDorado;

	private static string[] setInstanceProcyonAlpha;

	private static string[] setInstanceSirius;

	private static string[] setInstanceBellatrixHideout;

	private static string[] setInstanceAlphaCentauri;

	private static string[] setInstanceLynxSupercluster;

	private static string[] setInstanceDraco;

	private static string[] setPvpArena1;

	private static string[] setPvpArena2;

	private static string[] setPvpArena3;

	private static string[] setPvpArena4;

	private static string[] setPvpArena5;

	private static string[] setInstanceOwlNebula;

	private static string[] setXenia;

	private static string[] setXerxes;

	private static string[] setUltralibriumGalaxyOne;

	private static string[] setUltralibriumGalaxyTwo;

	private static string[] setUltralibriumGalaxyThree;

	private static string[] setInstanceInvictus;

	private static string[] setInstanceVorax;

	private static string[] setInstanceMagus;

	private static string[] setExtractionPoint;

	private static string[] setPvPDomination;

	private static string[] setFactionGalaxy1;

	private static string[] setFactionGalaxy2;

	private static string[] setFactionGalaxy3;

	private static string[] setFactionGalaxy4;

	private static string[] setFactionGalaxy5;

	private static SortedList<string, string[]> allScenes;

	public static bool isWebPlayer;

	public static bool isSceneReady;

	private static LoadingAsset guiAsset;

	private static Texture2D txBkg;

	public static string message;

	private static GuiWindow wndSplash;

	private static GuiBar barProgress;

	private static GuiTexture txBackground;

	private static GuiTexture txBackgroundBase;

	private static GuiFramework gui;

	private static GuiLabel lblLoading;

	private static GuiLabel lblPercent;

	private static GuiLabel lblCurrentAsset;

	private static GuiLabel lblNote;

	private static GuiLabel lblError;

	private static GuiButtonFixed btnLeft;

	private static GuiButtonFixed btnRight;

	private static GuiTexture tipsPagingDotOne;

	private static GuiTexture tipsPagingDotTwo;

	private static GuiTexture tipsPagingDotThree;

	private static GuiTexture tipsPagingDotFour;

	private static GuiTexture tipsPagingDotFive;

	private static GuiTexture tipsPagingDotSix;

	private static GuiTexture tipsBackground;

	private static GuiTexture tipsImage;

	private static GuiLabel tipsLblOne;

	private static GuiLabel tipsLblTwo;

	private static GuiLabel tipsLblThree;

	private static GuiLabel tipsLblFour;

	private static byte tipsIndex;

	private static float tipChangeTime;

	private static int lastScreenWidth;

	public static bool hadSetHandleLog;

	public static StringBuilder feedbackLogOutput;

	private static int feedbackLogLenght;

	public static int currentJobSizeBytes;

	public static int currentJobLoadedBytes;

	public static float currentJobProgress;

	public static List<LoadingAsset> currentJobs;

	private static AsyncOperation currentlyLoadedScene;

	private static string currentlyLoadedSceneName;

	private static SceneBundleSet currentlyLoadedSet;

	public static bool isAllBundlesLoaded;

	private static int currentLoadingBundle;

	private static GuiTextureAnimated loadingAnimation;

	private static bool doNotUpdateLbls;

	public static bool isPreparingScene;

	public static bool isPreparingSceneBundlesLoaded;

	public static bool isPreparingSceneReadyForAsyncLoad;

	public static string preparedSceneName;

	public static bool isLoadProgressOnScreen;

	public static bool isLoadSceneStarted;

	public static bool isCaching;

	public static string DefaultLanguage
	{
		get
		{
			if (!PlayerPrefs.HasKey("Language"))
			{
				return string.Empty;
			}
			return PlayerPrefs.GetString("Language");
		}
		set
		{
			PlayerPrefs.SetString("Language", value);
		}
	}

	public static PureUdpClient udp
	{
		get
		{
			if (playWebGame._udp == null)
			{
				if (playWebGame.authorization == null)
				{
					throw new Exception("Authorization data is not set!");
				}
				playWebGame._udp = new PureUdpClient(playWebGame.authorization.universeServerIp, playWebGame.authorization.galaxyServerPort, playWebGame.authorization.id);
			}
			return playWebGame._udp;
		}
		set
		{
			playWebGame._udp = null;
		}
	}

	static playWebGame()
	{
		playWebGame.GAME_TYPE = "int";
		playWebGame.GAME_DOMAIN = "127.0.0.1";
		playWebGame.WORLD_ID = "1";
		playWebGame.LOGIN_SERVER_IP = "127.0.0.1";
		playWebGame.ASSET_URL = "http://asset.andromeda5.com/assets";
		playWebGame.REGISTER_URL_DIRECT = "http://andromeda5.com/?show=registry";
		playWebGame.URL_FORGOTTEN_PASSWORD = "http://andromeda5.com/?show=forgotten";
		playWebGame.AVATARS_URL = "http://game.andromeda5.com/avatars/int/world_1/{0}.jpg";
		playWebGame.ON_QUIT_URL_DIRECT = "http://game.andromeda5.com/play.php";
		playWebGame.PAYMET_URL = "http://game.andromeda5.com/play.php";
		playWebGame.AdColonyCustomID = string.Empty;
		playWebGame.loadResFromAssets = true;
		playWebGame.authorization = new AuthorizeResult();
		playWebGame._udp = null;
		playWebGame.StartDownload = true;
		playWebGame.ServersReady = false;
		playWebGame.currentAssetLoad = 0;
		playWebGame.timeSinceStart = 0f;
		playWebGame.setInSpace = new string[] { "ShipBarBody3", "ShipBarBlue_pfb", "ShipBarGreen_pfb", "ShipBarOrange_pfb", "Critical_pfb", "NewCritical_pfb", "CriticalCircle_pfb", "SkillLaserCritical_pfb", "SpeedEffect_pfb", "SpeedSkybox_pfb", "DirectionArrows_pfb", "DailyRewardsGui", "LoadingBar_pfb", "pulse", "PVPsearching", "LevelUpAnimation", "SideMenuAnimation", "ChatAnimation", "StoryQuestAnimation", "TransformerNotification", "BringToNpcAnimation", "SkillFrameAnimation", "SkillStarAnimation", "BlinkingSmallButtonLeft", "BlinkingSmallButtonMiddle", "BlinkingSmallButtonRight", "Explosion/explosionEffect_pfb", "InSpaceVoices", "scrollZoom_pfb", "Multikills/MultiKill_1", "Multikills/MultiKill_2", "Multikills/MultiKill_3", "Multikills/MultiKill_4", "Multikills/MultiKill_5", "Multikills/MultiKill_6", "Multikills/MultiKill_7", "Multikills/MultiKill_8", "Multikills/MultiKill_9", "Multikills/Voice", "SocialInteraction/Social_0", "SocialInteraction/Social_1", "SocialInteraction/Social_2", "SocialInteraction/Social_3", "SocialInteraction/Social_4", "SocialInteraction/Social_5", "SocialInteraction/Social_6", "SocialInteraction/Social_7", "SocialInteraction/Social_8", "SocialInteraction/Social_9", "SocialInteraction/Social_10", "Activatable_pfb", "Investigatable_pfb", "Sabotagable_pfb", "Platform_pfb", "platformWithShip_pfb", "ShipExplosion_pfb", "Projectiles/WeapLaserT1_pjc", "Projectiles/WeapLaserT2_pjc", "Projectiles/WeapLaserT3_pjc", "Projectiles/WeapLaserT4_pjc", "Projectiles/WeapLaserT5_pjc", "Projectiles/WeapPlasmaT1_pjc", "Projectiles/WeapPlasmaT2_pjc", "Projectiles/WeapPlasmaT3_pjc", "Projectiles/WeapPlasmaT4_pjc", "Projectiles/WeapPlasmaT5_pjc", "Projectiles/WeapIonT1_pjc", "Projectiles/WeapIonT2_pjc", "Projectiles/WeapIonT3_pjc", "Projectiles/WeapIonT4_pjc", "Projectiles/WeapIonT5_pjc", "PartyArrow1_pfb", "GuidingArrow_pfb", "PartyArrow2_pfb", "PartyArrow3_pfb", "drill", "Ships/ShipTankT1_pfb", "Ships/ShipTankT2_pfb", "Ships/ShipTankT3_pfb", "Ships/ShipTankT4_pfb", "Ships/ShipTankT5_pfb", "Ships/ShipDamageT1_pfb", "Ships/ShipDamageT2_pfb", "Ships/ShipDamageT3_pfb", "Ships/ShipDamageT4_pfb", "Ships/ShipDamageT5_pfb", "Ships/ShipDamageT6_pfb", "Ships/ShipSupportT1_pfb", "Ships/ShipSupportT2_pfb", "Ships/ShipSupportT3_pfb", "Ships/ShipSupportT4_pfb", "Ships/ShipSupportT5_pfb", "Skills", "GlowFX", "Mineables/resCarbon_pfb", "Mineables/resHydrogen_pfb", "Mineables/resDeuterium_pfb", "Mineables/resOxygen_pfb", "ShipBarBody2", "ShipBarHitpoint", "ShipBarShield", "Station1", "station", "portal2pfb", "SpaceLbl", "NewSpaceLbl", "OutlineSpaceLbl", "Targeting", "MineralsAvatars", "mapTarget", "no_ammo", "Shop", "StoryStuff", "MiniMaps", "CfgMenuBg", "StoryQuestCompleted_pfb", "QuestCompleted_pfb", "MovingTargeting", "MovingTargetingPfb", "Station1", "MiningCage_pfb", "MineralBeam", "OuttaMiningRange", "minregion", "Sounds", "Shield1", "HyperJumpBeamPfb", "HyperJumpEnter", "HyperJumpOut", "HyperJumpOutPfb", "GalaxyJumpPfb", "ShootLock", "ShootLockGreen", "ShootLockYellow", "weapon_slot_reload", "Shield_1_Pfb", "weapon_hit1", "Mineables/resShipWreck6_pfb", "Mineables/resShipWreck7_pfb", "Mineables/resShipWreck8_pfb", "Mineables/resShipWreck9_pfb", "Mineables", "LevelUP_pfb", "LevelUPDown", "LevelUpLights", "LevelUPSide", "skillReloading", "ActiveSkill_pfb", "FreeSpaceSkillRange_pfb", "SkillRange_pfb", "SkillRangeWeapons_pfb", "AimCircle_pfb", "SkillRemedy_pfb", "SkillRepairField_pfb", "SkillShortCircuit_pfb", "SkillNanoStorm_pfb", "SkillNanoShield_pfb", "SkillPulseNova_pfb", "PulseNova", "SkillSunderArmor_pfb", "SkillRocketBarrage_pfb", "SkillRepairingDrones_pfb", "SkillTaunt_pfb", "SkillShieldFortress_pfb", "SkillFocusFire_pfb", "SkillForceWave_pfb", "SkillUnstoppable_pfb", "Unstoppable", "SkillDecoy_pjc", "SkillLaserDestruction_pfb", "SkillPowerBreak_pfb", "SkillLightSpeed_pfb", "SkillPowerCut_pjc", "SkillMistShroud_pfb", "MistShroud", "boosterIco", "lampa", "Music", "volkr_pfb", "golgotha_pfb", "AvailableRewardsWindow", "ShipStatsGui", "TargetingGui", "FixedAvatars", "InstanceDifficulty", "PowerUpsWindow", "SendGiftsWindow", "PoiScreenWindow", "WarScreenWindow", "ActionButtons", "UniverseMapScreen", "SkillSacrifice_pfb", "SkillLifeStealTarget_pfb", "SkillLifeStealCaster_pfb", "SkillDisarm_pfb" };
		playWebGame.setGui = new string[] { "Migrate4", "FrameworkGUI", "GUI", "NewGUI", "LoginGui", "ConfigWnd", "CfgMenuBg", "Portals", "BossPresets", "Achievement", "TooltipAnimation", "Sounds", "ShipBarHitpoint", "Targeting", "FusionWindow", "SystemWindow", "ConfigWindow", "MainScreenWindow", "MinimapWindow", "AmmosAvatars", "BoostersAvatars", "CorpusesAvatars", "EnginesAvatars", "ExtrasAvatars", "ShieldsAvatars", "ShipsAvatars", "PortalPartsAvatars", "QuestItemsAvatars", "WeaponsAvatars", "MineralsAvatars", "LoadingAnimation", "ExtractionPointArrow", "chat", "Partners", "QuestInfoWindow", "QuestTracker", "QuestTrackerAvatars", "QuestNotificationGreen", "QuestNotificationOrange", "QuestObjectivesArt", "BringToAnimation", "NpcDialogueWindow", "GalaxiesAvatars", "PartyGUI", "FuseAnimation", "PvPDominationGui" };
		playWebGame.setInBase = new string[] { "Scenes/InBase", "Music", "InBaseAudio", "Screen1", "Screen2", "Screen3", "Shop", "WarScreenWindow", "PoiScreenWindow", "Ships/ShipTankT1_pfb", "Ships/ShipTankT2_pfb", "Ships/ShipTankT3_pfb", "Ships/ShipTankT4_pfb", "Ships/ShipTankT5_pfb", "Ships/ShipDamageT1_pfb", "Ships/ShipDamageT2_pfb", "Ships/ShipDamageT3_pfb", "Ships/ShipDamageT4_pfb", "Ships/ShipDamageT5_pfb", "Ships/ShipDamageT6_pfb", "Ships/ShipSupportT1_pfb", "Ships/ShipSupportT2_pfb", "Ships/ShipSupportT3_pfb", "Ships/ShipSupportT4_pfb", "Ships/ShipSupportT5_pfb" };
		playWebGame.setTutorial = new string[] { "FrameworkGUI", "FusionWindow", "SystemWindow", "ConfigWindow", "MainScreenWindow", "MinimapWindow", "QuestInfoWindow", "PartyGUI", "ShipsAvatars", "QuestTracker", "TutorialWindow", "QuestNotificationGreen", "QuestNotificationOrange", "QuestObjectivesArt", "NpcDialogueWindow", "GuideArrow", "weapon_slot_reload", "no_ammo", "AmmosAvatars", "MineralsAvatars", "WeaponsAvatars", "Shop", "FuseAnimation", "TooltipAnimation", "drill", "ShipBarBody3", "ShipBarBlue_pfb", "ShipBarGreen_pfb", "ShipBarOrange_pfb", "scrollZoom_pfb", "Critical_pfb", "NewCritical_pfb", "CriticalCircle_pfb", "SkillLaserCritical_pfb", "SpeedEffect_pfb", "SpeedSkybox_pfb", "Scenes/Tutorial", "Sounds", "InSpaceVoices", "Ships/ShipTankT5_pfb", "Ships/ShipDamageT1_pfb", "ShipBarBody2", "ShipBarHitpoint", "ShipBarShield", "HyperJumpOut", "HyperJumpOutPfb", "MovingTargetingPfb", "MovingTargeting", "GuidingArrow_pfb", "LevelUPDown", "LevelUpLights", "LevelUPSide", "QuestCompleted_pfb", "SpaceLbl", "NewSpaceLbl", "OutlineSpaceLbl", "DirectionArrows_pfb", "mapTarget", "Pve/NPC_Aria2_pfb", "Pve/AnnihilatorBoss", "ShootLock", "ShootLockGreen", "ShootLockPurple", "Shield_1_Pfb", "Shield1", "Mineables/resShipWreck6_pfb", "Mineables/resShipWreck7_pfb", "Mineables/resShipWreck8_pfb", "Mineables/resShipWreck9_pfb", "Projectiles/WeapLaserT1_pjc", "Projectiles/WeapLaserT4_pjc", "Projectiles/WeapPlasmaT4_pjc", "Projectiles/WeapIonT3_pjc", "weapon_hit1", "MiningCage_pfb", "MineralBeam", "OuttaMiningRange", "minregion", "Explosion/explosionEffect_pfb", "ShipExplosion_pfb", "Tutorial/Aria_tutorial", "Tutorial/Battlecruiser_tutorial", "Tutorial/Capsule_tutorial", "Tutorial/Drone_tutorial", "Tutorial/Shoot_aria", "Tutorial/Shoot_battlecruise", "Tutorial/Laser_torpedo", "Tutorial/Smoke_animation", "TutorialMovieAudio", "Activatable_pfb", "ShipStatsGui", "ActionButtons", "TargetingGui" };
		playWebGame.setHydra = new string[] { "Scenes/Hydra", "NPC/NPC_Vladimir_pfb", "NPC/NPC_Vladimir_audio_assets", "Pve/GrayBully1", "Pve/GrayBully2", "Pve/GreenHustler1", "Pve/GreenHustler2", "Pve/GreenHustler3", "Pve/Parasite4", "Pve/Parasite_Mat1" };
		playWebGame.setMensa = new string[] { "Scenes/Mensa", "NPC/NPC_Darius_pfb", "NPC/NPC_Darius_audio_assets", "Pve/Annihilator1", "Pve/Annihilator_Mat1", "Pve/Flea1", "Pve/GrayBully2", "Pve/GrayBully3", "Pve/GrayBully4", "Pve/GreenHustler4", "Pve/RedHustler1" };
		playWebGame.setCanisMinor = new string[] { "Scenes/CanisMinor", "NPC/NPC_Stalker_pfb", "NPC/NPC_Stalker_audio_assets", "NPC/NPC_LtBrown_pfb", "NPC/NPC_LtBrown_audio_assets", "Pve/Annihilator1", "Pve/Annihilator2", "Pve/Annihilator_Mat1", "Pve/Flea1", "Pve/Flea8", "Pve/GrayBully4", "Pve/GrayBully5", "Pve/GrayBully6", "Pve/Parasite4", "Pve/Parasite_Mat1", "Pve/RedHustler1", "Pve/RedHustler2", "Pve/RedHustler3" };
		playWebGame.setCanisMajor = new string[] { "Scenes/CanisMajor", "NPC/NPC_TedClancey_pfb", "NPC/NPC_TedClancey_audio_assets", "NPC/NPC_Nassor_pfb", "NPC/NPC_Nassor_audio_assets", "Pve/Annihilator4", "Pve/Annihilator_Mat1", "Pve/Droid1", "Pve/Droid2", "Pve/Flea2", "Pve/GrayBully6", "Pve/GrayBully7", "Pve/Parasite3", "Pve/Parasite_Mat1", "Pve/RedHustler3" };
		playWebGame.setOrion = new string[] { "Scenes/Orion", "NPC/NPC_CaribbeanJoe_pfb", "NPC/NPC_CaribbeanJoe_audio_assets", "NPC/NPC_SamHawkins_pfb", "NPC/NPC_SamHawkins_audio_assets", "Pve/Annihilator4", "Pve/Annihilator_Mat1", "Pve/Droid1", "Pve/Droid2", "Pve/Droid3", "Pve/Flea9", "Pve/GoldenBully2", "Pve/GrayBully6", "Pve/GrayBully7", "Pve/Parasite8", "Pve/Parasite_Mat1" };
		playWebGame.setCentaurus = new string[] { "Scenes/Centaurus", "NPC/NPC_EddFinn_pfb", "NPC/NPC_EddFinn_audio_assets", "NPC/NPC_JohnnyDigger_pfb", "NPC/NPC_JohnnyDigger_audio_assets", "Pve/Annihilator5", "Pve/Annihilator_Mat1", "Pve/Droid3", "Pve/Droid4", "Pve/Droid5", "Pve/Flea9", "Pve/Flea15", "Pve/GoldenBully7", "Pve/GrayBully7" };
		playWebGame.setCepheus = new string[] { "Scenes/Cepheus", "NPC/NPC_Walter_pfb", "NPC/NPC_Walter_audio_assets", "NPC/NPC_Reese_pfb", "NPC/NPC_Reese_audio_assets", "Pve/Annihilator5", "Pve/Annihilator_Mat1", "Pve/CristmasShipBoss1", "Pve/Droid4", "Pve/Droid5", "Pve/Droid6", "Pve/Flea4", "Pve/Flea11", "Pve/Flea15", "Pve/Flea17", "Pve/GoldenBully1", "Pve/GoldenBully2", "Pve/GoldenBully7", "Pve/Parasite1", "Pve/Parasite5", "Pve/Parasite_Mat1", "Pve/Serpent_1" };
		playWebGame.setLynx = new string[] { "Scenes/Lynx", "NPC/NPC_James_pfb", "NPC/NPC_James_audio_assets", "NPC/NPC_Thane_pfb", "NPC/NPC_Thane_audio_assets", "Pve/Annihilator6", "Pve/Annihilator_Mat1", "Pve/Droid6", "Pve/Droid8", "Pve/Droid9", "Pve/DroidUltra", "Pve/Flea18", "Pve/Flea3", "Pve/GoldenBully1", "Pve/GoldenBully2", "Pve/GoldenBully7", "Pve/Parasite1", "Pve/Parasite_Mat1" };
		playWebGame.setUrsaMinor = new string[] { "Scenes/UrsaMinor", "NPC/NPC_Loyce_pfb", "NPC/NPC_Loyce_audio_assets", "NPC/NPC_Gabriel_pfb", "NPC/NPC_Gabriel_audio_assets", "Pve/Annihilator7", "Pve/Annihilator_Mat1", "Pve/CristmasShipBoss2", "Pve/Droid9", "Pve/Droid10", "Pve/Droid11", "Pve/Flea10", "Pve/Flea18", "Pve/Flea22", "Pve/Flea23", "Pve/Flea24", "Pve/GoldenBully3", "Pve/GoldenBully4", "Pve/GoldenBully5" };
		playWebGame.setUrsaMajor = new string[] { "Scenes/UrsaMajor", "NPC/NPC_Oleg_pfb", "NPC/NPC_Oleg_audio_assets", "NPC/NPC_MorbidSimon_pfb", "NPC/NPC_MorbidSimon_audio_assets", "Pve/Annihilator7", "Pve/Annihilator_Mat1", "Pve/CristmasShipBoss3", "Pve/Droid10", "Pve/Droid11", "Pve/Droid12", "Pve/Droid13", "Pve/Droid15", "Pve/Flea5", "Pve/Flea16", "Pve/Flea24", "Pve/Flea25", "Pve/GoldenBully4", "Pve/GoldenBully5", "Pve/GoldenBully6", "Pve/ImmobilizerBoss", "Pve/Parasite6", "Pve/Parasite_Mat1", "Pve/Serpent_2", "Pve/Serpent_3" };
		playWebGame.setTaurus = new string[] { "Scenes/Taurus", "NPC/NPC_Eddie_pfb", "NPC/NPC_Eddie_audio_assets", "NPC/NPC_Xena_pfb", "NPC/NPC_Xena_audio_assets", "NPC/NPC_Patton_pfb", "NPC/NPC_Patton_audio_assets", "Pve/Annihilator3", "Pve/Annihilator8", "Pve/Annihilator_Mat1", "Pve/Droid13", "Pve/Droid15", "Pve/Flea12", "Pve/Flea20", "Pve/Flea21", "Pve/Flea23", "Pve/Flea25", "Pve/Flea5", "Pve/Flea6", "Pve/GoldenBully6", "Pve/ImmobilizerEnemy", "Pve/Parasite7", "Pve/Parasite_Mat1", "Pve/DefenceTurret2", "Projectiles/Weap-Impaler_pjc" };
		playWebGame.setScorpio = new string[] { "Scenes/Scorpio", "NPC/NPC_Louise_pfb", "NPC/NPC_Louise_audio_assets", "Pve/Annihilator3", "Pve/Annihilator_Mat1", "Pve/Flea7", "Pve/Flea12", "Pve/Flea13", "Pve/Flea14", "Pve/Flea19", "Pve/Flea20", "Pve/Flea21", "Pve/Flea26", "Pve/Flea27", "Pve/Flea28", "Pve/Parasite2", "Pve/Parasite_Mat1", "Pve/DefenceTurret2", "Projectiles/Weap-Impaler_pjc" };
		playWebGame.setAndromeda = new string[] { "Scenes/Andromeda", "Pve/DefenceTurret2", "Projectiles/Weap-Impaler_pjc" };
		playWebGame.setCassiopeia = new string[] { "Scenes/Cassiopeia", "Pve/DefenceTurret2", "Projectiles/Weap-Impaler_pjc" };
		playWebGame.setPegasus = new string[] { "Scenes/Pegasus", "Pve/DefenceTurret2", "Projectiles/Weap-Impaler_pjc" };
		playWebGame.setPerseus = new string[] { "Scenes/Perseus", "Pve/DefenceTurret2", "Projectiles/Weap-Impaler_pjc" };
		playWebGame.setInstanceHydraPrime = new string[] { "Scenes/InstanceHydraPrime", "Pve/DroidBoss", "Pve/GoldenBully1", "Pve/GoldenBully2", "Pve/GoldenBully7", "Pve/GrayBully7" };
		playWebGame.setInstanceDorado = new string[] { "Scenes/InstanceDorado", "Pve/BuccaneerEnemy", "Pve/BuccaneerUltra", "Pve/DisruptorEnemy", "Pve/Parasite34", "Pve/TransmitterUltra" };
		playWebGame.setInstanceProcyonAlpha = new string[] { "Scenes/InstanceProcyonAlpha", "Pve/Droid32", "Pve/Droid41", "Pve/DroidBoss", "Pve/DroidEnemy", "Pve/DroidUltra" };
		playWebGame.setInstanceSirius = new string[] { "Scenes/InstanceSirius", "Pve/Annihilator3", "Pve/Annihilator6", "Pve/Annihilator7", "Pve/Annihilator8", "Pve/Annihilator_Mat2", "Pve/PveBoss2" };
		playWebGame.setInstanceBellatrixHideout = new string[] { "Scenes/InstanceBellatrixHideout", "Pve/Parasite5", "Pve/Parasite6", "Pve/Parasite_Mat1" };
		playWebGame.setInstanceAlphaCentauri = new string[] { "Scenes/InstanceAlphaCentauri", "Pve/Annihilator1", "Pve/Annihilator2", "Pve/Annihilator4", "Pve/Annihilator_Mat2", "Pve/Annihilator47", "Pve/PveBoss1" };
		playWebGame.setInstanceLynxSupercluster = new string[] { "Scenes/InstanceLynxSupercluster", "Pve/Droid32", "Pve/DroidUltra", "Pve/GoldenBully2", "Pve/GoldenBully3", "Pve/GoldenBully7", "Pve/Annihilator5", "Pve/Annihilator_Mat2" };
		playWebGame.setInstanceDraco = new string[] { "Scenes/InstanceDraco", "Pve/DroidUltra", "Pve/Flea6", "Pve/Flea27", "Pve/Flea28" };
		playWebGame.setPvpArena1 = new string[] { "Scenes/PvpArena1" };
		playWebGame.setPvpArena2 = new string[] { "Scenes/PvpArena2" };
		playWebGame.setPvpArena3 = new string[] { "Scenes/PvpArena3" };
		playWebGame.setPvpArena4 = new string[] { "Scenes/PvpArena4" };
		playWebGame.setPvpArena5 = new string[] { "Scenes/PvpArena5" };
		playWebGame.setInstanceOwlNebula = new string[] { "Scenes/InstanceOwlNebula", "Pve/ImmobilizerBoss" };
		playWebGame.setXenia = new string[] { "Scenes/Xenia" };
		playWebGame.setXerxes = new string[] { "Scenes/Xerxes" };
		playWebGame.setUltralibriumGalaxyOne = new string[] { "Scenes/UltralibriumGalaxyOne", "NPC/NPC_Keon_pfb", "NPC/NPC_Keon_audio_assets", "NPC/NPC_Luther_pfb", "NPC/NPC_Luther_audio_assets", "Pve/UltraPvE1", "Pve/UltraPvE2", "Pve/UltraPvE3", "Pve/UltraPvE4", "Pve/UltraPvE5", "Pve/UltraPvE6", "Pve/UltraPvE7", "Pve/UltraPvE8", "Pve/UltraPvE9", "Pve/UltraPvE_Mat1", "Pve/UltraPvE_Mat2", "Pve/UltraPvESecondary_Mat1", "Pve/Xdroid1", "Pve/Xdroid2", "Pve/Xdroid3", "Pve/Xdroid4", "Pve/Xdroid5", "Pve/Xdroid_Mat1", "Pve/Xparasite1", "Pve/Xparasite2", "Pve/Xparasite_Mat1" };
		playWebGame.setUltralibriumGalaxyTwo = new string[] { "Scenes/UltralibriumGalaxyTwo", "NPC/NPC_Lancer_pfb", "NPC/NPC_Ruby_pfb", "Pve/UltraPvE10", "Pve/UltraPvE11", "Pve/UltraPvE14", "Pve/UltraPvE15", "Pve/UltraPvE16", "Pve/UltraPvE17", "Pve/UltraPvE20", "Pve/UltraPvE21", "Pve/UltraPvE22", "Pve/UltraPvE23", "Pve/UltraPvE_Mat1", "Pve/Xdroid2", "Pve/Xdroid3", "Pve/Xdroid6", "Pve/Xdroid7", "Pve/Xdroid_Mat1", "Pve/Xparasite3", "Pve/Xparasite4", "Pve/Xparasite_Mat1" };
		playWebGame.setUltralibriumGalaxyThree = new string[] { "Scenes/UltralibriumGalaxyThree", "NPC/NPC_Leona_pfb", "NPC/NPC_Skye_pfb", "Pve/UltraPvE5", "Pve/UltraPvE6", "Pve/UltraPvE12", "Pve/UltraPvE13", "Pve/UltraPvE14", "Pve/UltraPvE18", "Pve/UltraPvE19", "Pve/UltraPvE22", "Pve/UltraPvE23", "Pve/UltraPvE24", "Pve/UltraPvE25", "Pve/UltraPvE_Mat1", "Pve/UltraPvESecondary_Mat1", "Pve/Xdroid8", "Pve/Xdroid9", "Pve/Xdroid_Mat1", "Pve/Xparasite5", "Pve/Xparasite6", "Pve/Xparasite_Mat1" };
		playWebGame.setInstanceInvictus = new string[] { "Scenes/InstanceInvictus", "Pve/Xdroid1", "Pve/Xdroid2", "Pve/Xdroid3", "Pve/Xdroid5", "Pve/Xdroid_Mat1" };
		playWebGame.setInstanceVorax = new string[] { "Scenes/InstanceVorax", "Pve/UltraPvE3", "Pve/UltraPvE5", "Pve/UltraPvE6", "Pve/UltraPvE7", "Pve/UltraPvE_Mat2", "Pve/UltraPvESecondary_Mat1" };
		playWebGame.setInstanceMagus = new string[] { "Scenes/InstanceMagus", "Pve/UltraPvE20", "Pve/UltraPvE22", "Pve/UltraPvE23", "Pve/UltraPvE_Mat2" };
		playWebGame.setExtractionPoint = new string[] { "ExtractionPoint_pfb", "BuilderDrone_pfb", "Pve/GrayBully1", "Pve/GrayBully2", "Pve/GrayBully3", "Pve/GrayBully4", "Pve/GrayBully5", "Pve/TurretLevel01_pfb", "Pve/TurretLevel03_pfb", "Pve/TurretLevel04_pfb", "Pve/TurretLevel05_pfb", "Pve/TurretLevel06_pfb", "Pve/Flea3", "Pve/Flea5", "Pve/Flea24", "Pve/Flea25", "Pve/Flea26" };
		playWebGame.setPvPDomination = new string[] { "Scenes/PvPDomination", "MiningStation_pfb", "MiningStationSphere_mat1", "MiningStationSphere_mat2", "MiningStationSphere_mat3", "PvPDominationGui", "PvPDominationAnimation", "Pve/DefenceTurret2", "Projectiles/Weap-Impaler_pjc", "Pve/BlueHustlerBoss", "FrameworkGUI" };
		playWebGame.setFactionGalaxy1 = new string[] { "Scenes/FactionGalaxy1", "Pve/SpecialBully1", "Pve/SpecialBully2", "Pve/SpecialBully3", "Pve/SpecialBully4", "Pve/SpecialBully5", "Pve/SpecialBully6", "Pve/SpecialBully7", "Pve/SpecialBully8", "Pve/SpecialBully_Mat1" };
		playWebGame.setFactionGalaxy2 = new string[] { "Scenes/FactionGalaxy2", "Pve/SpecialBully1", "Pve/SpecialBully2", "Pve/SpecialBully3", "Pve/SpecialBully4", "Pve/SpecialBully5", "Pve/SpecialBully6", "Pve/SpecialBully7", "Pve/SpecialBully8", "Pve/SpecialBully_Mat2" };
		playWebGame.setFactionGalaxy3 = new string[] { "Scenes/FactionGalaxy3", "Pve/SpecialBully1", "Pve/SpecialBully2", "Pve/SpecialBully3", "Pve/SpecialBully4", "Pve/SpecialBully5", "Pve/SpecialBully6", "Pve/SpecialBully7", "Pve/SpecialBully8", "Pve/SpecialBully_Mat3" };
		playWebGame.setFactionGalaxy4 = new string[] { "Scenes/FactionGalaxy4", "Pve/SpecialBully1", "Pve/SpecialBully2", "Pve/SpecialBully3", "Pve/SpecialBully4", "Pve/SpecialBully5", "Pve/SpecialBully6", "Pve/SpecialBully7", "Pve/SpecialBully8", "Pve/SpecialBully_Mat4" };
		playWebGame.setFactionGalaxy5 = new string[] { "Scenes/FactionGalaxy5", "Pve/SpecialBully1", "Pve/SpecialBully2", "Pve/SpecialBully3", "Pve/SpecialBully4", "Pve/SpecialBully5", "Pve/SpecialBully6", "Pve/SpecialBully7", "Pve/SpecialBully8", "Pve/SpecialBully_Mat5" };
		SortedList<string, string[]> sortedList = new SortedList<string, string[]>();
		sortedList.Add("Hydra", playWebGame.setHydra);
		sortedList.Add("Mensa", playWebGame.setMensa);
		sortedList.Add("CanisMinor", playWebGame.setCanisMinor);
		sortedList.Add("CanisMajor", playWebGame.setCanisMajor);
		sortedList.Add("Orion", playWebGame.setOrion);
		sortedList.Add("Centaurus", playWebGame.setCentaurus);
		sortedList.Add("Cepheus", playWebGame.setCepheus);
		sortedList.Add("Lynx", playWebGame.setLynx);
		sortedList.Add("UrsaMinor", playWebGame.setUrsaMinor);
		sortedList.Add("UrsaMajor", playWebGame.setUrsaMajor);
		sortedList.Add("Taurus", Enumerable.ToArray<string>(Enumerable.Union<string>(playWebGame.setTaurus, playWebGame.setExtractionPoint)));
		sortedList.Add("Scorpio", Enumerable.ToArray<string>(Enumerable.Union<string>(playWebGame.setScorpio, playWebGame.setExtractionPoint)));
		sortedList.Add("Andromeda", Enumerable.ToArray<string>(Enumerable.Union<string>(playWebGame.setAndromeda, playWebGame.setExtractionPoint)));
		sortedList.Add("Cassiopeia", Enumerable.ToArray<string>(Enumerable.Union<string>(playWebGame.setCassiopeia, playWebGame.setExtractionPoint)));
		sortedList.Add("Pegasus", Enumerable.ToArray<string>(Enumerable.Union<string>(playWebGame.setPegasus, playWebGame.setExtractionPoint)));
		sortedList.Add("Perseus", Enumerable.ToArray<string>(Enumerable.Union<string>(playWebGame.setPerseus, playWebGame.setExtractionPoint)));
		sortedList.Add("InstanceHydraPrime", playWebGame.setInstanceHydraPrime);
		sortedList.Add("InstanceDorado", playWebGame.setInstanceDorado);
		sortedList.Add("InstanceProcyonAlpha", playWebGame.setInstanceProcyonAlpha);
		sortedList.Add("InstanceSirius", playWebGame.setInstanceSirius);
		sortedList.Add("InstanceBellatrixHideout", playWebGame.setInstanceBellatrixHideout);
		sortedList.Add("InstanceAlphaCentauri", playWebGame.setInstanceAlphaCentauri);
		sortedList.Add("InstanceLynxSupercluster", playWebGame.setInstanceLynxSupercluster);
		sortedList.Add("InstanceDraco", playWebGame.setInstanceDraco);
		sortedList.Add("InstanceOwlNebula", playWebGame.setInstanceOwlNebula);
		sortedList.Add("PvpArena1", playWebGame.setPvpArena1);
		sortedList.Add("PvpArena2", playWebGame.setPvpArena2);
		sortedList.Add("PvpArena3", playWebGame.setPvpArena3);
		sortedList.Add("PvpArena4", playWebGame.setPvpArena4);
		sortedList.Add("PvpArena5", playWebGame.setPvpArena5);
		sortedList.Add("PvPDomination", playWebGame.setPvPDomination);
		sortedList.Add("Xenia", playWebGame.setXenia);
		sortedList.Add("Xerxes", playWebGame.setXerxes);
		sortedList.Add("UltralibriumGalaxyOne", playWebGame.setUltralibriumGalaxyOne);
		sortedList.Add("UltralibriumGalaxyTwo", playWebGame.setUltralibriumGalaxyTwo);
		sortedList.Add("UltralibriumGalaxyThree", playWebGame.setUltralibriumGalaxyThree);
		sortedList.Add("InstanceInvictus", playWebGame.setInstanceInvictus);
		sortedList.Add("InstanceVorax", playWebGame.setInstanceVorax);
		sortedList.Add("InstanceMagus", playWebGame.setInstanceMagus);
		sortedList.Add("FactionGalaxy1", playWebGame.setFactionGalaxy1);
		sortedList.Add("FactionGalaxy2", playWebGame.setFactionGalaxy2);
		sortedList.Add("FactionGalaxy3", playWebGame.setFactionGalaxy3);
		sortedList.Add("FactionGalaxy4", playWebGame.setFactionGalaxy4);
		sortedList.Add("FactionGalaxy5", playWebGame.setFactionGalaxy5);
		playWebGame.allScenes = sortedList;
		playWebGame.isSceneReady = false;
		playWebGame.message = ".";
		playWebGame.tipsIndex = 1;
		playWebGame.tipChangeTime = 0f;
		playWebGame.lastScreenWidth = 0;
		playWebGame.feedbackLogLenght = 80000;
		playWebGame.currentlyLoadedScene = null;
		playWebGame.currentlyLoadedSceneName = null;
		playWebGame.currentlyLoadedSet = null;
		playWebGame.isAllBundlesLoaded = false;
		playWebGame.currentLoadingBundle = 0;
		playWebGame.doNotUpdateLbls = false;
		playWebGame.isPreparingScene = false;
		playWebGame.isPreparingSceneBundlesLoaded = false;
		playWebGame.isPreparingSceneReadyForAsyncLoad = false;
		playWebGame.preparedSceneName = string.Empty;
		playWebGame.isLoadProgressOnScreen = false;
		playWebGame.isLoadSceneStarted = false;
		playWebGame.isCaching = false;
		playWebGame.loadingAvatars = new SortedList<string, List<AvatarJob>>();
		playWebGame.loadedAvatars = new SortedList<string, Texture2D>();
		playWebGame.allBundles = new SortedList<string, LoadingAsset>();
		SortedList<string, LoadingAsset> sortedList1 = playWebGame.allBundles;
		LoadingAsset loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "Migrate4",
			displayName = "Migrate4",
			size = 924,
			assetVersion = 1
		};
		sortedList1.Add("Migrate4", loadingAsset);
		SortedList<string, LoadingAsset> sortedList2 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "Partners",
			displayName = "Partners",
			size = 54961,
			assetVersion = 1
		};
		sortedList2.Add("Partners", loadingAsset);
		SortedList<string, LoadingAsset> sortedList3 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "LoadingAnimation",
			displayName = "LoadingAnimation",
			size = 4149,
			assetVersion = 1
		};
		sortedList3.Add("LoadingAnimation", loadingAsset);
		SortedList<string, LoadingAsset> sortedList4 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "NewGUI",
			displayName = "GUI II",
			size = 4831340,
			assetVersion = 3
		};
		sortedList4.Add("NewGUI", loadingAsset);
		SortedList<string, LoadingAsset> sortedList5 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "LoginGui",
			displayName = "Login GUI",
			size = 1174156,
			assetVersion = 1
		};
		sortedList5.Add("LoginGui", loadingAsset);
		SortedList<string, LoadingAsset> sortedList6 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "Achievement",
			displayName = "Achievements Art",
			size = 135461,
			assetVersion = 1
		};
		sortedList6.Add("Achievement", loadingAsset);
		SortedList<string, LoadingAsset> sortedList7 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "GUI",
			displayName = "GUI",
			size = 1780500,
			assetVersion = 3
		};
		sortedList7.Add("GUI", loadingAsset);
		SortedList<string, LoadingAsset> sortedList8 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "BossPresets",
			displayName = "BossPresets",
			size = 8726,
			assetVersion = 101
		};
		sortedList8.Add("BossPresets", loadingAsset);
		SortedList<string, LoadingAsset> sortedList9 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "pulse",
			displayName = "Party Stuff",
			size = 7993,
			assetVersion = 1
		};
		sortedList9.Add("pulse", loadingAsset);
		SortedList<string, LoadingAsset> sortedList10 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "PVPsearching",
			displayName = "PVP Stuff",
			size = 2633,
			assetVersion = 1
		};
		sortedList10.Add("PVPsearching", loadingAsset);
		SortedList<string, LoadingAsset> sortedList11 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "SideMenuAnimation",
			displayName = "Side Menu Animation",
			size = 4871,
			assetVersion = 1
		};
		sortedList11.Add("SideMenuAnimation", loadingAsset);
		SortedList<string, LoadingAsset> sortedList12 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "LevelUpAnimation",
			displayName = "Level Up Stuff",
			size = 1975,
			assetVersion = 1
		};
		sortedList12.Add("LevelUpAnimation", loadingAsset);
		SortedList<string, LoadingAsset> sortedList13 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "ChatAnimation",
			displayName = "Chat Animation",
			size = 2277,
			assetVersion = 1
		};
		sortedList13.Add("ChatAnimation", loadingAsset);
		SortedList<string, LoadingAsset> sortedList14 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "TransformerNotification",
			displayName = "Transformer Notification Stuff",
			size = 3667,
			assetVersion = 1
		};
		sortedList14.Add("TransformerNotification", loadingAsset);
		SortedList<string, LoadingAsset> sortedList15 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "StoryQuestAnimation",
			displayName = "StoryQuest Stuff",
			size = 2606,
			assetVersion = 1
		};
		sortedList15.Add("StoryQuestAnimation", loadingAsset);
		SortedList<string, LoadingAsset> sortedList16 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "BringToNpcAnimation",
			displayName = "Bring to NPC animation",
			size = 2293,
			assetVersion = 1
		};
		sortedList16.Add("BringToNpcAnimation", loadingAsset);
		SortedList<string, LoadingAsset> sortedList17 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "SkillFrameAnimation",
			displayName = "Skill frame animation",
			size = 8373,
			assetVersion = 1
		};
		sortedList17.Add("SkillFrameAnimation", loadingAsset);
		SortedList<string, LoadingAsset> sortedList18 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "SkillStarAnimation",
			displayName = "Skill star animation",
			size = 1991,
			assetVersion = 1
		};
		sortedList18.Add("SkillStarAnimation", loadingAsset);
		SortedList<string, LoadingAsset> sortedList19 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "Sounds",
			displayName = "Sounds",
			size = 740759,
			assetVersion = 5
		};
		sortedList19.Add("Sounds", loadingAsset);
		SortedList<string, LoadingAsset> sortedList20 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "Music",
			displayName = "Music",
			size = 3094344,
			assetVersion = 1
		};
		sortedList20.Add("Music", loadingAsset);
		SortedList<string, LoadingAsset> sortedList21 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Projectiles/WeapLaserT1_pjc",
			displayName = "WeapLaserT1_pjc",
			size = 7606,
			assetVersion = 1
		};
		sortedList21.Add("Projectiles/WeapLaserT1_pjc", loadingAsset);
		SortedList<string, LoadingAsset> sortedList22 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Projectiles/WeapLaserT4_pjc",
			displayName = "WeapLaserT4_pjc",
			size = 8389,
			assetVersion = 1
		};
		sortedList22.Add("Projectiles/WeapLaserT4_pjc", loadingAsset);
		SortedList<string, LoadingAsset> sortedList23 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Projectiles/WeapIonT3_pjc",
			displayName = "WeapIonT3_pjc",
			size = 8032,
			assetVersion = 1
		};
		sortedList23.Add("Projectiles/WeapIonT3_pjc", loadingAsset);
		SortedList<string, LoadingAsset> sortedList24 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "scrollZoom_pfb",
			displayName = "scrollZoom_pfb",
			size = 28292,
			assetVersion = 1
		};
		sortedList24.Add("scrollZoom_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList25 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "DailyRewardsGui",
			displayName = "DailyRewardsGui",
			size = 412653,
			assetVersion = 2
		};
		sortedList25.Add("DailyRewardsGui", loadingAsset);
		SortedList<string, LoadingAsset> sortedList26 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "ShipBarBody2",
			displayName = "Ship Bar Body",
			size = 4611,
			assetVersion = 1
		};
		sortedList26.Add("ShipBarBody2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList27 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "ShipBarHitpoint",
			displayName = "Ship Bar Hitpoint",
			size = 42215,
			assetVersion = 1
		};
		sortedList27.Add("ShipBarHitpoint", loadingAsset);
		SortedList<string, LoadingAsset> sortedList28 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "ShipBarShield",
			displayName = "Ship Bar Shield",
			size = 42027,
			assetVersion = 1
		};
		sortedList28.Add("ShipBarShield", loadingAsset);
		SortedList<string, LoadingAsset> sortedList29 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "ShipBarBody3",
			displayName = "Ship Bar Body",
			size = 5824,
			assetVersion = 1
		};
		sortedList29.Add("ShipBarBody3", loadingAsset);
		SortedList<string, LoadingAsset> sortedList30 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "ShipBarBlue_pfb",
			displayName = "Ship Bar Blue",
			size = 42163,
			assetVersion = 1
		};
		sortedList30.Add("ShipBarBlue_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList31 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "ShipBarGreen_pfb",
			displayName = "Ship Bar Green",
			size = 42444,
			assetVersion = 1
		};
		sortedList31.Add("ShipBarGreen_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList32 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "ShipBarOrange_pfb",
			displayName = "Ship Bar Orange",
			size = 42094,
			assetVersion = 1
		};
		sortedList32.Add("ShipBarOrange_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList33 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Critical_pfb",
			displayName = "Critical_pfb",
			size = 222506,
			assetVersion = 1
		};
		sortedList33.Add("Critical_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList34 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NewCritical_pfb",
			displayName = "NewCritical_pfb",
			size = 222506,
			assetVersion = 100
		};
		sortedList34.Add("NewCritical_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList35 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "CriticalCircle_pfb",
			displayName = "CriticalCircle_pfb",
			size = 104952,
			assetVersion = 1
		};
		sortedList35.Add("CriticalCircle_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList36 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillLaserCritical_pfb",
			displayName = "SkillLaserCritical_pfb",
			size = 11109,
			assetVersion = 1
		};
		sortedList36.Add("SkillLaserCritical_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList37 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SpeedEffect_pfb",
			displayName = "SpeedEffect",
			size = 182451,
			assetVersion = 1
		};
		sortedList37.Add("SpeedEffect_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList38 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SpeedSkybox_pfb",
			displayName = "SpeedSkybox",
			size = 46345,
			assetVersion = 1
		};
		sortedList38.Add("SpeedSkybox_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList39 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "QuestInfoWindow",
			displayName = "QuestInfoWindow",
			size = 368198,
			assetVersion = 2
		};
		sortedList39.Add("QuestInfoWindow", loadingAsset);
		SortedList<string, LoadingAsset> sortedList40 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "QuestTracker",
			displayName = "QuestTracker",
			size = 76141,
			assetVersion = 1
		};
		sortedList40.Add("QuestTracker", loadingAsset);
		SortedList<string, LoadingAsset> sortedList41 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "QuestTrackerAvatars",
			displayName = "QuestTrackerAvatars",
			size = 1580094,
			assetVersion = 2
		};
		sortedList41.Add("QuestTrackerAvatars", loadingAsset);
		SortedList<string, LoadingAsset> sortedList42 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "QuestNotificationGreen",
			displayName = "QuestNotificationGreen",
			size = 107039,
			assetVersion = 1
		};
		sortedList42.Add("QuestNotificationGreen", loadingAsset);
		SortedList<string, LoadingAsset> sortedList43 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "QuestNotificationOrange",
			displayName = "QuestNotificationOrange",
			size = 107246,
			assetVersion = 1
		};
		sortedList43.Add("QuestNotificationOrange", loadingAsset);
		SortedList<string, LoadingAsset> sortedList44 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "QuestObjectivesArt",
			displayName = "QuestObjectivesArt",
			size = 33564,
			assetVersion = 1
		};
		sortedList44.Add("QuestObjectivesArt", loadingAsset);
		SortedList<string, LoadingAsset> sortedList45 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "BringToAnimation",
			displayName = "BringToAnimation",
			size = 5308,
			assetVersion = 1
		};
		sortedList45.Add("BringToAnimation", loadingAsset);
		SortedList<string, LoadingAsset> sortedList46 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "NpcDialogueWindow",
			displayName = "NpcDialogueWindow",
			size = 66528,
			assetVersion = 1
		};
		sortedList46.Add("NpcDialogueWindow", loadingAsset);
		SortedList<string, LoadingAsset> sortedList47 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "GalaxiesAvatars",
			displayName = "GalaxiesAvatars",
			size = 78048,
			assetVersion = 1
		};
		sortedList47.Add("GalaxiesAvatars", loadingAsset);
		SortedList<string, LoadingAsset> sortedList48 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "PartyGUI",
			displayName = "PartyGUI",
			size = 92634,
			assetVersion = 1
		};
		sortedList48.Add("PartyGUI", loadingAsset);
		SortedList<string, LoadingAsset> sortedList49 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "FrameworkGUI",
			displayName = "FrameworkGUI",
			size = 117689,
			assetVersion = 7
		};
		sortedList49.Add("FrameworkGUI", loadingAsset);
		SortedList<string, LoadingAsset> sortedList50 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "MovingTargetingPfb",
			displayName = "Move Target Cursor",
			size = 6828,
			assetVersion = 1
		};
		sortedList50.Add("MovingTargetingPfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList51 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "MovingTargeting",
			displayName = "Move Target Cursor II",
			size = 70920,
			assetVersion = 1
		};
		sortedList51.Add("MovingTargeting", loadingAsset);
		SortedList<string, LoadingAsset> sortedList52 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "DirectionArrows_pfb",
			displayName = "DirectionArrows_pfb",
			size = 67616,
			assetVersion = 2
		};
		sortedList52.Add("DirectionArrows_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList53 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "no_ammo",
			displayName = "No Ammo Animation",
			size = 11313,
			assetVersion = 1
		};
		sortedList53.Add("no_ammo", loadingAsset);
		SortedList<string, LoadingAsset> sortedList54 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "StoryStuff",
			displayName = "Story Stuff",
			size = 197652,
			assetVersion = 1
		};
		sortedList54.Add("StoryStuff", loadingAsset);
		SortedList<string, LoadingAsset> sortedList55 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "Shop",
			displayName = "Shop",
			size = 58566,
			assetVersion = 2
		};
		sortedList55.Add("Shop", loadingAsset);
		SortedList<string, LoadingAsset> sortedList56 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Shield_1_Pfb",
			displayName = "Shield_1_Pfb",
			size = 14470,
			assetVersion = 1
		};
		sortedList56.Add("Shield_1_Pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList57 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "Shield1",
			displayName = "Shield fhydsfhsdui1",
			size = 187049,
			assetVersion = 1
		};
		sortedList57.Add("Shield1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList58 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Mineables/resShipWreck6_pfb",
			displayName = "Mineables/resShipWreck6_pfb",
			size = 186400,
			assetVersion = 1
		};
		sortedList58.Add("Mineables/resShipWreck6_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList59 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Mineables/resShipWreck7_pfb",
			displayName = "Mineables/resShipWreck7_pfb",
			size = 176760,
			assetVersion = 1
		};
		sortedList59.Add("Mineables/resShipWreck7_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList60 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Mineables/resShipWreck8_pfb",
			displayName = "Mineables/resShipWreck8_pfb",
			size = 172149,
			assetVersion = 1
		};
		sortedList60.Add("Mineables/resShipWreck8_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList61 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Mineables/resShipWreck9_pfb",
			displayName = "Mineables/resShipWreck9_pfb",
			size = 179923,
			assetVersion = 1
		};
		sortedList61.Add("Mineables/resShipWreck9_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList62 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "ShootLock",
			displayName = "ShootLock",
			size = 49008,
			assetVersion = 1
		};
		sortedList62.Add("ShootLock", loadingAsset);
		SortedList<string, LoadingAsset> sortedList63 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "ShootLockGreen",
			displayName = "ShootLockGreen",
			size = 51131,
			assetVersion = 1
		};
		sortedList63.Add("ShootLockGreen", loadingAsset);
		SortedList<string, LoadingAsset> sortedList64 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "drill",
			displayName = "drill",
			size = 79449,
			assetVersion = 1
		};
		sortedList64.Add("drill", loadingAsset);
		SortedList<string, LoadingAsset> sortedList65 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SpaceLbl",
			displayName = "Space Label",
			size = 80693,
			assetVersion = 1
		};
		sortedList65.Add("SpaceLbl", loadingAsset);
		SortedList<string, LoadingAsset> sortedList66 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NewSpaceLbl",
			displayName = "New Space Label",
			size = 80720,
			assetVersion = 1
		};
		sortedList66.Add("NewSpaceLbl", loadingAsset);
		SortedList<string, LoadingAsset> sortedList67 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "OutlineSpaceLbl",
			displayName = "Outline Space Label",
			size = 80914,
			assetVersion = 1
		};
		sortedList67.Add("OutlineSpaceLbl", loadingAsset);
		SortedList<string, LoadingAsset> sortedList68 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "GuideArrow",
			displayName = "GuideArrow",
			size = 11916,
			assetVersion = 1
		};
		sortedList68.Add("GuideArrow", loadingAsset);
		SortedList<string, LoadingAsset> sortedList69 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "ExtractionPointArrow",
			displayName = "ExtractionPointArrow",
			size = 6925,
			assetVersion = 1
		};
		sortedList69.Add("ExtractionPointArrow", loadingAsset);
		SortedList<string, LoadingAsset> sortedList70 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "TooltipAnimation",
			displayName = "TooltipAnimation",
			size = 344135,
			assetVersion = 1
		};
		sortedList70.Add("TooltipAnimation", loadingAsset);
		SortedList<string, LoadingAsset> sortedList71 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "FuseAnimation",
			displayName = "FuseAnimation",
			size = 23107,
			assetVersion = 1
		};
		sortedList71.Add("FuseAnimation", loadingAsset);
		SortedList<string, LoadingAsset> sortedList72 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "BlinkingSmallButtonLeft",
			displayName = "BlinkingSmallButtonLeft",
			size = 2062,
			assetVersion = 1
		};
		sortedList72.Add("BlinkingSmallButtonLeft", loadingAsset);
		SortedList<string, LoadingAsset> sortedList73 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "BlinkingSmallButtonRight",
			displayName = "BlinkingSmallButtonRight",
			size = 2042,
			assetVersion = 1
		};
		sortedList73.Add("BlinkingSmallButtonRight", loadingAsset);
		SortedList<string, LoadingAsset> sortedList74 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "BlinkingSmallButtonMiddle",
			displayName = "BlinkingSmallButtonMiddle",
			size = 1377,
			assetVersion = 1
		};
		sortedList74.Add("BlinkingSmallButtonMiddle", loadingAsset);
		SortedList<string, LoadingAsset> sortedList75 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "mapTarget",
			displayName = "mapTarget",
			size = 4165,
			assetVersion = 1
		};
		sortedList75.Add("mapTarget", loadingAsset);
		SortedList<string, LoadingAsset> sortedList76 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "MiniMaps",
			displayName = "Mini Maps",
			size = 5897387,
			assetVersion = 100
		};
		sortedList76.Add("MiniMaps", loadingAsset);
		SortedList<string, LoadingAsset> sortedList77 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "OuttaMiningRange",
			displayName = "Outta Mining Range",
			size = 6871,
			assetVersion = 1
		};
		sortedList77.Add("OuttaMiningRange", loadingAsset);
		SortedList<string, LoadingAsset> sortedList78 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "weapon_hit1",
			displayName = "weapon_hit1",
			size = 11036,
			assetVersion = 1
		};
		sortedList78.Add("weapon_hit1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList79 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "weapon_slot_reload",
			displayName = "weapon_slot_reload",
			size = 1379,
			assetVersion = 1
		};
		sortedList79.Add("weapon_slot_reload", loadingAsset);
		SortedList<string, LoadingAsset> sortedList80 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/AnnihilatorBoss",
			displayName = "AnnihilatorBoss",
			size = 349424,
			assetVersion = 1
		};
		sortedList80.Add("Pve/AnnihilatorBoss", loadingAsset);
		SortedList<string, LoadingAsset> sortedList81 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/TorpedocarrierEnemy",
			displayName = "TorpedocarrierEnemy",
			size = 453006,
			assetVersion = 1
		};
		sortedList81.Add("Pve/TorpedocarrierEnemy", loadingAsset);
		SortedList<string, LoadingAsset> sortedList82 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/NPC_Aria2_pfb",
			displayName = "NPC_Aria2_pfb",
			size = 464781,
			assetVersion = 1
		};
		sortedList82.Add("Pve/NPC_Aria2_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList83 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "ExtractionPoint_pfb",
			displayName = "Extraction Point",
			size = 2083640,
			assetVersion = 1
		};
		sortedList83.Add("ExtractionPoint_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList84 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "BuilderDrone_pfb",
			displayName = "Builder Drone",
			size = 540957,
			assetVersion = 1
		};
		sortedList84.Add("BuilderDrone_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList85 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/TurretLevel01_pfb",
			displayName = "Turret Level 01",
			size = 520511,
			assetVersion = 1
		};
		sortedList85.Add("Pve/TurretLevel01_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList86 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/TurretLevel03_pfb",
			displayName = "Turret Level 02",
			size = 532210,
			assetVersion = 1
		};
		sortedList86.Add("Pve/TurretLevel03_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList87 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/TurretLevel04_pfb",
			displayName = "Turret Level 03",
			size = 529200,
			assetVersion = 1
		};
		sortedList87.Add("Pve/TurretLevel04_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList88 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/TurretLevel05_pfb",
			displayName = "Turret Level 04",
			size = 541913,
			assetVersion = 1
		};
		sortedList88.Add("Pve/TurretLevel05_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList89 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/TurretLevel06_pfb",
			displayName = "Turret Level 05",
			size = 554693,
			assetVersion = 1
		};
		sortedList89.Add("Pve/TurretLevel06_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList90 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Projectiles/WeapLaserT2_pjc",
			displayName = "WeapLaserT2_pjc",
			size = 7600,
			assetVersion = 1
		};
		sortedList90.Add("Projectiles/WeapLaserT2_pjc", loadingAsset);
		SortedList<string, LoadingAsset> sortedList91 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Projectiles/WeapLaserT3_pjc",
			displayName = "WeapLaserT3_pjc",
			size = 8381,
			assetVersion = 1
		};
		sortedList91.Add("Projectiles/WeapLaserT3_pjc", loadingAsset);
		SortedList<string, LoadingAsset> sortedList92 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Projectiles/WeapLaserT5_pjc",
			displayName = "WeapLaserT5_pjc",
			size = 18280,
			assetVersion = 2
		};
		sortedList92.Add("Projectiles/WeapLaserT5_pjc", loadingAsset);
		SortedList<string, LoadingAsset> sortedList93 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Projectiles/WeapPlasmaT1_pjc",
			displayName = "WeapPlasmaT1_pjc",
			size = 10994,
			assetVersion = 1
		};
		sortedList93.Add("Projectiles/WeapPlasmaT1_pjc", loadingAsset);
		SortedList<string, LoadingAsset> sortedList94 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Projectiles/WeapPlasmaT2_pjc",
			displayName = "WeapPlasmaT2_pjc",
			size = 11005,
			assetVersion = 1
		};
		sortedList94.Add("Projectiles/WeapPlasmaT2_pjc", loadingAsset);
		SortedList<string, LoadingAsset> sortedList95 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Projectiles/WeapPlasmaT3_pjc",
			displayName = "WeapPlasmaT3_pjc",
			size = 10994,
			assetVersion = 1
		};
		sortedList95.Add("Projectiles/WeapPlasmaT3_pjc", loadingAsset);
		SortedList<string, LoadingAsset> sortedList96 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Projectiles/WeapPlasmaT4_pjc",
			displayName = "WeapPlasmaT4_pjc",
			size = 10997,
			assetVersion = 1
		};
		sortedList96.Add("Projectiles/WeapPlasmaT4_pjc", loadingAsset);
		SortedList<string, LoadingAsset> sortedList97 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Projectiles/WeapPlasmaT5_pjc",
			displayName = "WeapPlasmaT5_pjc",
			size = 10998,
			assetVersion = 2
		};
		sortedList97.Add("Projectiles/WeapPlasmaT5_pjc", loadingAsset);
		SortedList<string, LoadingAsset> sortedList98 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Projectiles/WeapIonT1_pjc",
			displayName = "WeapIonT1_pjc",
			size = 8042,
			assetVersion = 1
		};
		sortedList98.Add("Projectiles/WeapIonT1_pjc", loadingAsset);
		SortedList<string, LoadingAsset> sortedList99 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Projectiles/WeapIonT2_pjc",
			displayName = "WeapIonT2_pjc",
			size = 8028,
			assetVersion = 1
		};
		sortedList99.Add("Projectiles/WeapIonT2_pjc", loadingAsset);
		SortedList<string, LoadingAsset> sortedList100 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Projectiles/WeapIonT4_pjc",
			displayName = "WeapIonT4_pjc",
			size = 8025,
			assetVersion = 1
		};
		sortedList100.Add("Projectiles/WeapIonT4_pjc", loadingAsset);
		SortedList<string, LoadingAsset> sortedList101 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Projectiles/WeapIonT5_pjc",
			displayName = "WeapIonT5_pjc",
			size = 8033,
			assetVersion = 2
		};
		sortedList101.Add("Projectiles/WeapIonT5_pjc", loadingAsset);
		SortedList<string, LoadingAsset> sortedList102 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Projectiles/Weap-Impaler_pjc",
			displayName = "Weap-Impaler_pjc",
			size = 64092,
			assetVersion = 1
		};
		sortedList102.Add("Projectiles/Weap-Impaler_pjc", loadingAsset);
		SortedList<string, LoadingAsset> sortedList103 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Tutorial/Aria_tutorial",
			displayName = "Tutorial/Aria_animation",
			size = 467022,
			assetVersion = 1
		};
		sortedList103.Add("Tutorial/Aria_tutorial", loadingAsset);
		SortedList<string, LoadingAsset> sortedList104 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Tutorial/Battlecruiser_tutorial",
			displayName = "Tutorial/Battlecruiser_animation",
			size = 402983,
			assetVersion = 1
		};
		sortedList104.Add("Tutorial/Battlecruiser_tutorial", loadingAsset);
		SortedList<string, LoadingAsset> sortedList105 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Tutorial/Capsule_tutorial",
			displayName = "Tutorial/Capsule_tutorial",
			size = 68371,
			assetVersion = 1
		};
		sortedList105.Add("Tutorial/Capsule_tutorial", loadingAsset);
		SortedList<string, LoadingAsset> sortedList106 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Tutorial/Drone_tutorial",
			displayName = "Tutorial/Drone_tutorial",
			size = 227218,
			assetVersion = 1
		};
		sortedList106.Add("Tutorial/Drone_tutorial", loadingAsset);
		SortedList<string, LoadingAsset> sortedList107 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Tutorial/Shoot_aria",
			displayName = "Tutorial/Shoot_aria",
			size = 9732,
			assetVersion = 1
		};
		sortedList107.Add("Tutorial/Shoot_aria", loadingAsset);
		SortedList<string, LoadingAsset> sortedList108 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Tutorial/Shoot_battlecruise",
			displayName = "Tutorial/Shoot_battlecruise",
			size = 18448,
			assetVersion = 1
		};
		sortedList108.Add("Tutorial/Shoot_battlecruise", loadingAsset);
		SortedList<string, LoadingAsset> sortedList109 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Tutorial/Laser_torpedo",
			displayName = "Tutorial/Laser_torpedo",
			size = 20490,
			assetVersion = 1
		};
		sortedList109.Add("Tutorial/Laser_torpedo", loadingAsset);
		SortedList<string, LoadingAsset> sortedList110 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Tutorial/Smoke_animation",
			displayName = "Tutorial/Smoke_animation",
			size = 11244,
			assetVersion = 1
		};
		sortedList110.Add("Tutorial/Smoke_animation", loadingAsset);
		SortedList<string, LoadingAsset> sortedList111 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "TutorialMovieAudio",
			displayName = "TutorialMovieAudio",
			size = 1223953,
			assetVersion = 1
		};
		sortedList111.Add("TutorialMovieAudio", loadingAsset);
		SortedList<string, LoadingAsset> sortedList112 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Explosion/explosionEffect_pfb",
			displayName = "Explosions (explosionEffect)",
			size = 131016,
			assetVersion = 1
		};
		sortedList112.Add("Explosion/explosionEffect_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList113 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "ShipExplosion_pfb",
			displayName = "Ship Explosion",
			size = 179914,
			assetVersion = 1
		};
		sortedList113.Add("ShipExplosion_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList114 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "ShootLockPurple",
			displayName = "ShootLockPurple",
			size = 59857,
			assetVersion = 1
		};
		sortedList114.Add("ShootLockPurple", loadingAsset);
		SortedList<string, LoadingAsset> sortedList115 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Ships/ShipTankT1_pfb",
			displayName = "Tank T1",
			size = 671318,
			assetVersion = 1
		};
		sortedList115.Add("Ships/ShipTankT1_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList116 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Ships/ShipTankT2_pfb",
			displayName = "Tank T2",
			size = 657507,
			assetVersion = 1
		};
		sortedList116.Add("Ships/ShipTankT2_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList117 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Ships/ShipTankT3_pfb",
			displayName = "Tank T3",
			size = 651105,
			assetVersion = 1
		};
		sortedList117.Add("Ships/ShipTankT3_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList118 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Ships/ShipTankT4_pfb",
			displayName = "Tank T4",
			size = 640064,
			assetVersion = 1
		};
		sortedList118.Add("Ships/ShipTankT4_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList119 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Ships/ShipTankT5_pfb",
			displayName = "Tank T5",
			size = 665760,
			assetVersion = 1
		};
		sortedList119.Add("Ships/ShipTankT5_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList120 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Ships/ShipDamageT1_pfb",
			displayName = "Ship Damage T1",
			size = 690697,
			assetVersion = 1
		};
		sortedList120.Add("Ships/ShipDamageT1_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList121 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Ships/ShipDamageT2_pfb",
			displayName = "Ship Damage T2",
			size = 693929,
			assetVersion = 1
		};
		sortedList121.Add("Ships/ShipDamageT2_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList122 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Ships/ShipDamageT3_pfb",
			displayName = "Ship Damage T3",
			size = 447928,
			assetVersion = 1
		};
		sortedList122.Add("Ships/ShipDamageT3_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList123 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Ships/ShipDamageT4_pfb",
			displayName = "Ship Damage T4",
			size = 686060,
			assetVersion = 1
		};
		sortedList123.Add("Ships/ShipDamageT4_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList124 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Ships/ShipDamageT5_pfb",
			displayName = "Ship Damage T5",
			size = 656121,
			assetVersion = 1
		};
		sortedList124.Add("Ships/ShipDamageT5_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList125 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Ships/ShipDamageT6_pfb",
			displayName = "Ship Damage T6",
			size = 673120,
			assetVersion = 1
		};
		sortedList125.Add("Ships/ShipDamageT6_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList126 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Ships/ShipSupportT1_pfb",
			displayName = "Ship Support T1",
			size = 443254,
			assetVersion = 1
		};
		sortedList126.Add("Ships/ShipSupportT1_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList127 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Ships/ShipSupportT2_pfb",
			displayName = "Ship Support T2",
			size = 607146,
			assetVersion = 1
		};
		sortedList127.Add("Ships/ShipSupportT2_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList128 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Ships/ShipSupportT3_pfb",
			displayName = "Ship Support T3",
			size = 682918,
			assetVersion = 1
		};
		sortedList128.Add("Ships/ShipSupportT3_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList129 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Ships/ShipSupportT4_pfb",
			displayName = "Ship Support T4",
			size = 659968,
			assetVersion = 1
		};
		sortedList129.Add("Ships/ShipSupportT4_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList130 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Ships/ShipSupportT5_pfb",
			displayName = "Ship Support T5",
			size = 655239,
			assetVersion = 1
		};
		sortedList130.Add("Ships/ShipSupportT5_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList131 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "LoadingBar_pfb",
			displayName = "LoadingBar_pfb",
			size = 26495,
			assetVersion = 1
		};
		sortedList131.Add("LoadingBar_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList132 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "Skills",
			displayName = "Skills",
			size = 77185,
			assetVersion = 2
		};
		sortedList132.Add("Skills", loadingAsset);
		SortedList<string, LoadingAsset> sortedList133 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "GlowFX",
			displayName = "GlowFX",
			size = 449639,
			assetVersion = 1
		};
		sortedList133.Add("GlowFX", loadingAsset);
		SortedList<string, LoadingAsset> sortedList134 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Mineables/resCarbon_pfb",
			displayName = "Carbon Mineral",
			size = 117538,
			assetVersion = 2
		};
		sortedList134.Add("Mineables/resCarbon_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList135 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Mineables/resHydrogen_pfb",
			displayName = "Hydrogen Mineral",
			size = 95683,
			assetVersion = 2
		};
		sortedList135.Add("Mineables/resHydrogen_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList136 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Mineables/resDeuterium_pfb",
			displayName = "Deuterium Mineral",
			size = 98202,
			assetVersion = 2
		};
		sortedList136.Add("Mineables/resDeuterium_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList137 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Mineables/resOxygen_pfb",
			displayName = "Oxygen Mineral",
			size = 102323,
			assetVersion = 2
		};
		sortedList137.Add("Mineables/resOxygen_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList138 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Station1",
			displayName = "Space Station 1",
			size = 1528709,
			assetVersion = 1
		};
		sortedList138.Add("Station1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList139 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "station",
			displayName = "station",
			size = 198364,
			assetVersion = 1
		};
		sortedList139.Add("station", loadingAsset);
		SortedList<string, LoadingAsset> sortedList140 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "portal2pfb",
			displayName = "Portal 2",
			size = 222954,
			assetVersion = 1
		};
		sortedList140.Add("portal2pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList141 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "Targeting",
			displayName = "Targeting",
			size = 1880614,
			assetVersion = 100
		};
		sortedList141.Add("Targeting", loadingAsset);
		SortedList<string, LoadingAsset> sortedList142 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "AmmosAvatars",
			displayName = "AmmosAvatars",
			size = 36611,
			assetVersion = 1
		};
		sortedList142.Add("AmmosAvatars", loadingAsset);
		SortedList<string, LoadingAsset> sortedList143 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "BoostersAvatars",
			displayName = "BoostersAvatars",
			size = 36707,
			assetVersion = 1
		};
		sortedList143.Add("BoostersAvatars", loadingAsset);
		SortedList<string, LoadingAsset> sortedList144 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "CorpusesAvatars",
			displayName = "CorpusesAvatars",
			size = 248807,
			assetVersion = 1
		};
		sortedList144.Add("CorpusesAvatars", loadingAsset);
		SortedList<string, LoadingAsset> sortedList145 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "EnginesAvatars",
			displayName = "EnginesAvatars",
			size = 42948,
			assetVersion = 1
		};
		sortedList145.Add("EnginesAvatars", loadingAsset);
		SortedList<string, LoadingAsset> sortedList146 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "ExtrasAvatars",
			displayName = "ExtrasAvatars",
			size = 139360,
			assetVersion = 1
		};
		sortedList146.Add("ExtrasAvatars", loadingAsset);
		SortedList<string, LoadingAsset> sortedList147 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "ShieldsAvatars",
			displayName = "ShieldsAvatars",
			size = 259920,
			assetVersion = 1
		};
		sortedList147.Add("ShieldsAvatars", loadingAsset);
		SortedList<string, LoadingAsset> sortedList148 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "ShipsAvatars",
			displayName = "ShipsAvatars",
			size = 107248,
			assetVersion = 1
		};
		sortedList148.Add("ShipsAvatars", loadingAsset);
		SortedList<string, LoadingAsset> sortedList149 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "PortalPartsAvatars",
			displayName = "PortalPartsAvatars",
			size = 82870,
			assetVersion = 1
		};
		sortedList149.Add("PortalPartsAvatars", loadingAsset);
		SortedList<string, LoadingAsset> sortedList150 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "QuestItemsAvatars",
			displayName = "QuestItemsAvatars",
			size = 94238,
			assetVersion = 1
		};
		sortedList150.Add("QuestItemsAvatars", loadingAsset);
		SortedList<string, LoadingAsset> sortedList151 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "WeaponsAvatars",
			displayName = "WeaponsAvatars",
			size = 94684,
			assetVersion = 1
		};
		sortedList151.Add("WeaponsAvatars", loadingAsset);
		SortedList<string, LoadingAsset> sortedList152 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "MineralsAvatars",
			displayName = "MineralsAvatars",
			size = 53222,
			assetVersion = 1
		};
		sortedList152.Add("MineralsAvatars", loadingAsset);
		SortedList<string, LoadingAsset> sortedList153 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "FusionWindow",
			displayName = "FusionWindow",
			size = 190708,
			assetVersion = 1
		};
		sortedList153.Add("FusionWindow", loadingAsset);
		SortedList<string, LoadingAsset> sortedList154 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "SystemWindow",
			displayName = "SystemWindow",
			size = 111841,
			assetVersion = 1
		};
		sortedList154.Add("SystemWindow", loadingAsset);
		SortedList<string, LoadingAsset> sortedList155 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "MainScreenWindow",
			displayName = "MainScreenWindow",
			size = 155413,
			assetVersion = 101
		};
		sortedList155.Add("MainScreenWindow", loadingAsset);
		SortedList<string, LoadingAsset> sortedList156 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "MinimapWindow",
			displayName = "MinimapWindow",
			size = 33332,
			assetVersion = 4
		};
		sortedList156.Add("MinimapWindow", loadingAsset);
		SortedList<string, LoadingAsset> sortedList157 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "TutorialWindow",
			displayName = "TutorialWindow",
			size = 1225271,
			assetVersion = 3
		};
		sortedList157.Add("TutorialWindow", loadingAsset);
		SortedList<string, LoadingAsset> sortedList158 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "ConfigWindow",
			displayName = "ConfigWindow",
			size = 283461,
			assetVersion = 100
		};
		sortedList158.Add("ConfigWindow", loadingAsset);
		SortedList<string, LoadingAsset> sortedList159 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "CfgMenuBg",
			displayName = "Config Menu Backgrounds",
			size = 1803711,
			assetVersion = 1
		};
		sortedList159.Add("CfgMenuBg", loadingAsset);
		SortedList<string, LoadingAsset> sortedList160 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "MiningCage_pfb",
			displayName = "Mining Lock 3D",
			size = 19159,
			assetVersion = 1
		};
		sortedList160.Add("MiningCage_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList161 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "MineralBeam",
			displayName = "Mineral Beam",
			size = 156031,
			assetVersion = 1
		};
		sortedList161.Add("MineralBeam", loadingAsset);
		SortedList<string, LoadingAsset> sortedList162 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "minregion",
			displayName = "Outta Mining Range Animation",
			size = 179161,
			assetVersion = 1
		};
		sortedList162.Add("minregion", loadingAsset);
		SortedList<string, LoadingAsset> sortedList163 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "ConfigWnd",
			displayName = "ConfigWnd",
			size = 731170,
			assetVersion = 2
		};
		sortedList163.Add("ConfigWnd", loadingAsset);
		SortedList<string, LoadingAsset> sortedList164 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "Portals",
			displayName = "Portals",
			size = 643222,
			assetVersion = 1
		};
		sortedList164.Add("Portals", loadingAsset);
		SortedList<string, LoadingAsset> sortedList165 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/TransmitterBoss",
			displayName = "TransmitterBoss",
			size = 158817,
			assetVersion = 1
		};
		sortedList165.Add("Pve/TransmitterBoss", loadingAsset);
		SortedList<string, LoadingAsset> sortedList166 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/TransmitterEnemy",
			displayName = "TransmitterEnemy",
			size = 154785,
			assetVersion = 1
		};
		sortedList166.Add("Pve/TransmitterEnemy", loadingAsset);
		SortedList<string, LoadingAsset> sortedList167 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/TransmitterUltra",
			displayName = "TransmitterUltra",
			size = 161089,
			assetVersion = 1
		};
		sortedList167.Add("Pve/TransmitterUltra", loadingAsset);
		SortedList<string, LoadingAsset> sortedList168 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/TridentshipBoss",
			displayName = "TridentshipBoss",
			size = 242437,
			assetVersion = 1
		};
		sortedList168.Add("Pve/TridentshipBoss", loadingAsset);
		SortedList<string, LoadingAsset> sortedList169 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/TridentshipEnemy",
			displayName = "TridentshipEnemy",
			size = 242116,
			assetVersion = 1
		};
		sortedList169.Add("Pve/TridentshipEnemy", loadingAsset);
		SortedList<string, LoadingAsset> sortedList170 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/TridentshipUltra",
			displayName = "TridentshipUltra",
			size = 245084,
			assetVersion = 1
		};
		sortedList170.Add("Pve/TridentshipUltra", loadingAsset);
		SortedList<string, LoadingAsset> sortedList171 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Annihilator37",
			displayName = "Annihilator37",
			size = 155133,
			assetVersion = 1
		};
		sortedList171.Add("Pve/Annihilator37", loadingAsset);
		SortedList<string, LoadingAsset> sortedList172 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Annihilator38",
			displayName = "Annihilator38",
			size = 154781,
			assetVersion = 1
		};
		sortedList172.Add("Pve/Annihilator38", loadingAsset);
		SortedList<string, LoadingAsset> sortedList173 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Annihilator39",
			displayName = "Annihilator39",
			size = 154258,
			assetVersion = 1
		};
		sortedList173.Add("Pve/Annihilator39", loadingAsset);
		SortedList<string, LoadingAsset> sortedList174 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Annihilator46",
			displayName = "Annihilator46",
			size = 166863,
			assetVersion = 1
		};
		sortedList174.Add("Pve/Annihilator46", loadingAsset);
		SortedList<string, LoadingAsset> sortedList175 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Annihilator47",
			displayName = "Annihilator47",
			size = 166420,
			assetVersion = 1
		};
		sortedList175.Add("Pve/Annihilator47", loadingAsset);
		SortedList<string, LoadingAsset> sortedList176 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Annihilator48",
			displayName = "Annihilator48",
			size = 167263,
			assetVersion = 1
		};
		sortedList176.Add("Pve/Annihilator48", loadingAsset);
		SortedList<string, LoadingAsset> sortedList177 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/AnnihilatorEnemy",
			displayName = "AnnihilatorEnemy",
			size = 155406,
			assetVersion = 1
		};
		sortedList177.Add("Pve/AnnihilatorEnemy", loadingAsset);
		SortedList<string, LoadingAsset> sortedList178 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/AnnihilatorUltra",
			displayName = "AnnihilatorUltra",
			size = 160281,
			assetVersion = 1
		};
		sortedList178.Add("Pve/AnnihilatorUltra", loadingAsset);
		SortedList<string, LoadingAsset> sortedList179 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/BuccaneerBoss",
			displayName = "BuccaneerBoss",
			size = 157503,
			assetVersion = 1
		};
		sortedList179.Add("Pve/BuccaneerBoss", loadingAsset);
		SortedList<string, LoadingAsset> sortedList180 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/BuccaneerEnemy",
			displayName = "BuccaneerEnemy",
			size = 158786,
			assetVersion = 1
		};
		sortedList180.Add("Pve/BuccaneerEnemy", loadingAsset);
		SortedList<string, LoadingAsset> sortedList181 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/BuccaneerUltra",
			displayName = "BuccaneerUltra",
			size = 157738,
			assetVersion = 1
		};
		sortedList181.Add("Pve/BuccaneerUltra", loadingAsset);
		SortedList<string, LoadingAsset> sortedList182 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/DisruptorEnemy",
			displayName = "DisruptorEnemy",
			size = 228931,
			assetVersion = 1
		};
		sortedList182.Add("Pve/DisruptorEnemy", loadingAsset);
		SortedList<string, LoadingAsset> sortedList183 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Serpent_1",
			displayName = "Serpent 1",
			size = 161477,
			assetVersion = 1
		};
		sortedList183.Add("Pve/Serpent_1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList184 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Serpent_2",
			displayName = "Serpent 2",
			size = 215497,
			assetVersion = 1
		};
		sortedList184.Add("Pve/Serpent_2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList185 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Serpent_3",
			displayName = "Serpent 3",
			size = 409966,
			assetVersion = 1
		};
		sortedList185.Add("Pve/Serpent_3", loadingAsset);
		SortedList<string, LoadingAsset> sortedList186 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/RedHustler1",
			displayName = "RedHustler 1",
			size = 211881,
			assetVersion = 1
		};
		sortedList186.Add("Pve/RedHustler1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList187 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/RedHustler2",
			displayName = "RedHustler 2",
			size = 186998,
			assetVersion = 1
		};
		sortedList187.Add("Pve/RedHustler2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList188 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/RedHustler3",
			displayName = "RedHustler 3",
			size = 172280,
			assetVersion = 1
		};
		sortedList188.Add("Pve/RedHustler3", loadingAsset);
		SortedList<string, LoadingAsset> sortedList189 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/RedHustler4",
			displayName = "RedHustler 4",
			size = 184610,
			assetVersion = 1
		};
		sortedList189.Add("Pve/RedHustler4", loadingAsset);
		SortedList<string, LoadingAsset> sortedList190 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/GreenHustler1",
			displayName = "GreenHustler 1",
			size = 218498,
			assetVersion = 1
		};
		sortedList190.Add("Pve/GreenHustler1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList191 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/GreenHustler2",
			displayName = "GreenHustler 2",
			size = 197387,
			assetVersion = 1
		};
		sortedList191.Add("Pve/GreenHustler2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList192 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/GreenHustler3",
			displayName = "GreenHustler 3",
			size = 180202,
			assetVersion = 1
		};
		sortedList192.Add("Pve/GreenHustler3", loadingAsset);
		SortedList<string, LoadingAsset> sortedList193 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/GreenHustler4",
			displayName = "GreenHustler 4",
			size = 194026,
			assetVersion = 1
		};
		sortedList193.Add("Pve/GreenHustler4", loadingAsset);
		SortedList<string, LoadingAsset> sortedList194 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/GrayBully1",
			displayName = "GrayBully1",
			size = 220943,
			assetVersion = 1
		};
		sortedList194.Add("Pve/GrayBully1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList195 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/GrayBully2",
			displayName = "GrayBully2",
			size = 195460,
			assetVersion = 1
		};
		sortedList195.Add("Pve/GrayBully2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList196 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/GrayBully3",
			displayName = "GrayBully3",
			size = 212052,
			assetVersion = 1
		};
		sortedList196.Add("Pve/GrayBully3", loadingAsset);
		SortedList<string, LoadingAsset> sortedList197 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/GrayBully4",
			displayName = "GrayBully4",
			size = 195609,
			assetVersion = 1
		};
		sortedList197.Add("Pve/GrayBully4", loadingAsset);
		SortedList<string, LoadingAsset> sortedList198 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/GrayBully5",
			displayName = "GrayBully5",
			size = 204888,
			assetVersion = 1
		};
		sortedList198.Add("Pve/GrayBully5", loadingAsset);
		SortedList<string, LoadingAsset> sortedList199 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/GrayBully6",
			displayName = "GrayBully6",
			size = 193603,
			assetVersion = 1
		};
		sortedList199.Add("Pve/GrayBully6", loadingAsset);
		SortedList<string, LoadingAsset> sortedList200 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/GrayBully7",
			displayName = "GrayBully7",
			size = 222912,
			assetVersion = 1
		};
		sortedList200.Add("Pve/GrayBully7", loadingAsset);
		SortedList<string, LoadingAsset> sortedList201 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/GoldenBully1",
			displayName = "GoldenBully1",
			size = 201824,
			assetVersion = 1
		};
		sortedList201.Add("Pve/GoldenBully1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList202 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/GoldenBully2",
			displayName = "GoldenBully2",
			size = 185488,
			assetVersion = 1
		};
		sortedList202.Add("Pve/GoldenBully2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList203 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/GoldenBully3",
			displayName = "GoldenBully3",
			size = 175662,
			assetVersion = 1
		};
		sortedList203.Add("Pve/GoldenBully3", loadingAsset);
		SortedList<string, LoadingAsset> sortedList204 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/GoldenBully4",
			displayName = "GoldenBully4",
			size = 189447,
			assetVersion = 1
		};
		sortedList204.Add("Pve/GoldenBully4", loadingAsset);
		SortedList<string, LoadingAsset> sortedList205 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/GoldenBully5",
			displayName = "GoldenBully5",
			size = 197624,
			assetVersion = 1
		};
		sortedList205.Add("Pve/GoldenBully5", loadingAsset);
		SortedList<string, LoadingAsset> sortedList206 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/GoldenBully6",
			displayName = "GoldenBully6",
			size = 194808,
			assetVersion = 1
		};
		sortedList206.Add("Pve/GoldenBully6", loadingAsset);
		SortedList<string, LoadingAsset> sortedList207 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/GoldenBully7",
			displayName = "GoldenBully7",
			size = 210262,
			assetVersion = 1
		};
		sortedList207.Add("Pve/GoldenBully7", loadingAsset);
		SortedList<string, LoadingAsset> sortedList208 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea1",
			displayName = "Flea1",
			size = 366837,
			assetVersion = 1
		};
		sortedList208.Add("Pve/Flea1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList209 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea2",
			displayName = "Flea2",
			size = 367642,
			assetVersion = 1
		};
		sortedList209.Add("Pve/Flea2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList210 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea3",
			displayName = "Flea3",
			size = 359483,
			assetVersion = 1
		};
		sortedList210.Add("Pve/Flea3", loadingAsset);
		SortedList<string, LoadingAsset> sortedList211 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea4",
			displayName = "Flea4",
			size = 351872,
			assetVersion = 1
		};
		sortedList211.Add("Pve/Flea4", loadingAsset);
		SortedList<string, LoadingAsset> sortedList212 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea5",
			displayName = "Flea5",
			size = 347719,
			assetVersion = 1
		};
		sortedList212.Add("Pve/Flea5", loadingAsset);
		SortedList<string, LoadingAsset> sortedList213 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea6",
			displayName = "Flea6",
			size = 361037,
			assetVersion = 1
		};
		sortedList213.Add("Pve/Flea6", loadingAsset);
		SortedList<string, LoadingAsset> sortedList214 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea7",
			displayName = "Flea7",
			size = 358692,
			assetVersion = 1
		};
		sortedList214.Add("Pve/Flea7", loadingAsset);
		SortedList<string, LoadingAsset> sortedList215 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea8",
			displayName = "Flea8",
			size = 364555,
			assetVersion = 1
		};
		sortedList215.Add("Pve/Flea8", loadingAsset);
		SortedList<string, LoadingAsset> sortedList216 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea9",
			displayName = "Flea9",
			size = 363278,
			assetVersion = 1
		};
		sortedList216.Add("Pve/Flea9", loadingAsset);
		SortedList<string, LoadingAsset> sortedList217 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea10",
			displayName = "Flea10",
			size = 361840,
			assetVersion = 1
		};
		sortedList217.Add("Pve/Flea10", loadingAsset);
		SortedList<string, LoadingAsset> sortedList218 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea11",
			displayName = "Flea11",
			size = 354460,
			assetVersion = 1
		};
		sortedList218.Add("Pve/Flea11", loadingAsset);
		SortedList<string, LoadingAsset> sortedList219 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea12",
			displayName = "Flea12",
			size = 350136,
			assetVersion = 1
		};
		sortedList219.Add("Pve/Flea12", loadingAsset);
		SortedList<string, LoadingAsset> sortedList220 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea13",
			displayName = "Flea13",
			size = 358849,
			assetVersion = 1
		};
		sortedList220.Add("Pve/Flea13", loadingAsset);
		SortedList<string, LoadingAsset> sortedList221 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea14",
			displayName = "Flea14",
			size = 356297,
			assetVersion = 1
		};
		sortedList221.Add("Pve/Flea14", loadingAsset);
		SortedList<string, LoadingAsset> sortedList222 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea15",
			displayName = "Flea15",
			size = 342954,
			assetVersion = 1
		};
		sortedList222.Add("Pve/Flea15", loadingAsset);
		SortedList<string, LoadingAsset> sortedList223 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea16",
			displayName = "Flea16",
			size = 341664,
			assetVersion = 1
		};
		sortedList223.Add("Pve/Flea16", loadingAsset);
		SortedList<string, LoadingAsset> sortedList224 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea17",
			displayName = "Flea17",
			size = 352296,
			assetVersion = 1
		};
		sortedList224.Add("Pve/Flea17", loadingAsset);
		SortedList<string, LoadingAsset> sortedList225 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea18",
			displayName = "Flea18",
			size = 346060,
			assetVersion = 1
		};
		sortedList225.Add("Pve/Flea18", loadingAsset);
		SortedList<string, LoadingAsset> sortedList226 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea19",
			displayName = "Flea19",
			size = 341949,
			assetVersion = 1
		};
		sortedList226.Add("Pve/Flea19", loadingAsset);
		SortedList<string, LoadingAsset> sortedList227 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea20",
			displayName = "Flea20",
			size = 337270,
			assetVersion = 1
		};
		sortedList227.Add("Pve/Flea20", loadingAsset);
		SortedList<string, LoadingAsset> sortedList228 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea21",
			displayName = "Flea21",
			size = 334807,
			assetVersion = 1
		};
		sortedList228.Add("Pve/Flea21", loadingAsset);
		SortedList<string, LoadingAsset> sortedList229 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea22",
			displayName = "Flea22",
			size = 365036,
			assetVersion = 1
		};
		sortedList229.Add("Pve/Flea22", loadingAsset);
		SortedList<string, LoadingAsset> sortedList230 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea23",
			displayName = "Flea23",
			size = 363745,
			assetVersion = 1
		};
		sortedList230.Add("Pve/Flea23", loadingAsset);
		SortedList<string, LoadingAsset> sortedList231 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea24",
			displayName = "Flea24",
			size = 352754,
			assetVersion = 1
		};
		sortedList231.Add("Pve/Flea24", loadingAsset);
		SortedList<string, LoadingAsset> sortedList232 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea25",
			displayName = "Flea25",
			size = 346334,
			assetVersion = 1
		};
		sortedList232.Add("Pve/Flea25", loadingAsset);
		SortedList<string, LoadingAsset> sortedList233 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea26",
			displayName = "Flea26",
			size = 342518,
			assetVersion = 1
		};
		sortedList233.Add("Pve/Flea26", loadingAsset);
		SortedList<string, LoadingAsset> sortedList234 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea27",
			displayName = "Flea27",
			size = 358034,
			assetVersion = 1
		};
		sortedList234.Add("Pve/Flea27", loadingAsset);
		SortedList<string, LoadingAsset> sortedList235 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Flea28",
			displayName = "Flea28",
			size = 356263,
			assetVersion = 1
		};
		sortedList235.Add("Pve/Flea28", loadingAsset);
		SortedList<string, LoadingAsset> sortedList236 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Droid1",
			displayName = "Droid1",
			size = 324298,
			assetVersion = 1
		};
		sortedList236.Add("Pve/Droid1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList237 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Droid2",
			displayName = "Droid2",
			size = 326241,
			assetVersion = 1
		};
		sortedList237.Add("Pve/Droid2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList238 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Droid3",
			displayName = "Droid3",
			size = 323080,
			assetVersion = 1
		};
		sortedList238.Add("Pve/Droid3", loadingAsset);
		SortedList<string, LoadingAsset> sortedList239 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Droid4",
			displayName = "Droid4",
			size = 325903,
			assetVersion = 1
		};
		sortedList239.Add("Pve/Droid4", loadingAsset);
		SortedList<string, LoadingAsset> sortedList240 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Droid5",
			displayName = "Droid5",
			size = 323699,
			assetVersion = 1
		};
		sortedList240.Add("Pve/Droid5", loadingAsset);
		SortedList<string, LoadingAsset> sortedList241 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Droid6",
			displayName = "Droid6",
			size = 326210,
			assetVersion = 1
		};
		sortedList241.Add("Pve/Droid6", loadingAsset);
		SortedList<string, LoadingAsset> sortedList242 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Droid8",
			displayName = "Droid8",
			size = 327585,
			assetVersion = 1
		};
		sortedList242.Add("Pve/Droid8", loadingAsset);
		SortedList<string, LoadingAsset> sortedList243 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Droid9",
			displayName = "Droid9",
			size = 323641,
			assetVersion = 1
		};
		sortedList243.Add("Pve/Droid9", loadingAsset);
		SortedList<string, LoadingAsset> sortedList244 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Droid10",
			displayName = "Droid10",
			size = 327532,
			assetVersion = 1
		};
		sortedList244.Add("Pve/Droid10", loadingAsset);
		SortedList<string, LoadingAsset> sortedList245 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Droid11",
			displayName = "Droid11",
			size = 323868,
			assetVersion = 1
		};
		sortedList245.Add("Pve/Droid11", loadingAsset);
		SortedList<string, LoadingAsset> sortedList246 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Droid12",
			displayName = "Droid12",
			size = 326274,
			assetVersion = 1
		};
		sortedList246.Add("Pve/Droid12", loadingAsset);
		SortedList<string, LoadingAsset> sortedList247 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Droid13",
			displayName = "Droid13",
			size = 324947,
			assetVersion = 1
		};
		sortedList247.Add("Pve/Droid13", loadingAsset);
		SortedList<string, LoadingAsset> sortedList248 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Droid15",
			displayName = "Droid15",
			size = 326372,
			assetVersion = 1
		};
		sortedList248.Add("Pve/Droid15", loadingAsset);
		SortedList<string, LoadingAsset> sortedList249 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Parasite1",
			displayName = "Parasite1",
			size = 29345,
			assetVersion = 1
		};
		sortedList249.Add("Pve/Parasite1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList250 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Parasite2",
			displayName = "Parasite2",
			size = 32206,
			assetVersion = 1
		};
		sortedList250.Add("Pve/Parasite2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList251 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Parasite3",
			displayName = "Parasite3",
			size = 29448,
			assetVersion = 1
		};
		sortedList251.Add("Pve/Parasite3", loadingAsset);
		SortedList<string, LoadingAsset> sortedList252 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Parasite4",
			displayName = "Parasite4",
			size = 27975,
			assetVersion = 1
		};
		sortedList252.Add("Pve/Parasite4", loadingAsset);
		SortedList<string, LoadingAsset> sortedList253 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Parasite5",
			displayName = "Parasite5",
			size = 30372,
			assetVersion = 1
		};
		sortedList253.Add("Pve/Parasite5", loadingAsset);
		SortedList<string, LoadingAsset> sortedList254 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Parasite6",
			displayName = "Parasite6",
			size = 46932,
			assetVersion = 1
		};
		sortedList254.Add("Pve/Parasite6", loadingAsset);
		SortedList<string, LoadingAsset> sortedList255 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Parasite7",
			displayName = "Parasite7",
			size = 38926,
			assetVersion = 1
		};
		sortedList255.Add("Pve/Parasite7", loadingAsset);
		SortedList<string, LoadingAsset> sortedList256 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Parasite8",
			displayName = "Parasite8",
			size = 29897,
			assetVersion = 1
		};
		sortedList256.Add("Pve/Parasite8", loadingAsset);
		SortedList<string, LoadingAsset> sortedList257 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Mat,
			assetName = "Pve/Parasite_Mat1",
			displayName = "Parasites Skin 1",
			size = 589870,
			assetVersion = 1
		};
		sortedList257.Add("Pve/Parasite_Mat1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList258 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Mat,
			assetName = "Pve/Parasite_Mat2",
			displayName = "Parasites Skin 2",
			size = 352000,
			assetVersion = 1
		};
		sortedList258.Add("Pve/Parasite_Mat2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList259 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Annihilator1",
			displayName = "Annihilator1",
			size = 71069,
			assetVersion = 1
		};
		sortedList259.Add("Pve/Annihilator1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList260 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Annihilator2",
			displayName = "Annihilator2",
			size = 65155,
			assetVersion = 1
		};
		sortedList260.Add("Pve/Annihilator2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList261 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Annihilator3",
			displayName = "Annihilator3",
			size = 66879,
			assetVersion = 1
		};
		sortedList261.Add("Pve/Annihilator3", loadingAsset);
		SortedList<string, LoadingAsset> sortedList262 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Annihilator4",
			displayName = "Annihilator4",
			size = 64442,
			assetVersion = 1
		};
		sortedList262.Add("Pve/Annihilator4", loadingAsset);
		SortedList<string, LoadingAsset> sortedList263 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Annihilator5",
			displayName = "Annihilator5",
			size = 44429,
			assetVersion = 1
		};
		sortedList263.Add("Pve/Annihilator5", loadingAsset);
		SortedList<string, LoadingAsset> sortedList264 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Annihilator6",
			displayName = "Annihilator6",
			size = 45867,
			assetVersion = 1
		};
		sortedList264.Add("Pve/Annihilator6", loadingAsset);
		SortedList<string, LoadingAsset> sortedList265 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Annihilator7",
			displayName = "Annihilator7",
			size = 21666,
			assetVersion = 1
		};
		sortedList265.Add("Pve/Annihilator7", loadingAsset);
		SortedList<string, LoadingAsset> sortedList266 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Annihilator8",
			displayName = "Annihilator8",
			size = 24316,
			assetVersion = 1
		};
		sortedList266.Add("Pve/Annihilator8", loadingAsset);
		SortedList<string, LoadingAsset> sortedList267 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Mat,
			assetName = "Pve/Annihilator_Mat1",
			displayName = "Annihilators Skin 1",
			size = 719387,
			assetVersion = 1
		};
		sortedList267.Add("Pve/Annihilator_Mat1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList268 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Mat,
			assetName = "Pve/Annihilator_Mat2",
			displayName = "Annihilators Skin 2",
			size = 451577,
			assetVersion = 1
		};
		sortedList268.Add("Pve/Annihilator_Mat2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList269 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Droid31",
			displayName = "Droid31",
			size = 152877,
			assetVersion = 1
		};
		sortedList269.Add("Pve/Droid31", loadingAsset);
		SortedList<string, LoadingAsset> sortedList270 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Droid32",
			displayName = "Droid32",
			size = 153054,
			assetVersion = 1
		};
		sortedList270.Add("Pve/Droid32", loadingAsset);
		SortedList<string, LoadingAsset> sortedList271 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Droid33",
			displayName = "Droid33",
			size = 153042,
			assetVersion = 1
		};
		sortedList271.Add("Pve/Droid33", loadingAsset);
		SortedList<string, LoadingAsset> sortedList272 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Droid40",
			displayName = "Droid40",
			size = 155915,
			assetVersion = 1
		};
		sortedList272.Add("Pve/Droid40", loadingAsset);
		SortedList<string, LoadingAsset> sortedList273 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Droid41",
			displayName = "Droid41",
			size = 156079,
			assetVersion = 1
		};
		sortedList273.Add("Pve/Droid41", loadingAsset);
		SortedList<string, LoadingAsset> sortedList274 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Droid42",
			displayName = "Droid42",
			size = 155888,
			assetVersion = 1
		};
		sortedList274.Add("Pve/Droid42", loadingAsset);
		SortedList<string, LoadingAsset> sortedList275 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/DroidBoss",
			displayName = "DroidBoss",
			size = 163813,
			assetVersion = 1
		};
		sortedList275.Add("Pve/DroidBoss", loadingAsset);
		SortedList<string, LoadingAsset> sortedList276 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/DroidEnemy",
			displayName = "DroidEnemy",
			size = 148155,
			assetVersion = 1
		};
		sortedList276.Add("Pve/DroidEnemy", loadingAsset);
		SortedList<string, LoadingAsset> sortedList277 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/DroidUltra",
			displayName = "DroidUltra",
			size = 162058,
			assetVersion = 1
		};
		sortedList277.Add("Pve/DroidUltra", loadingAsset);
		SortedList<string, LoadingAsset> sortedList278 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Immoby43",
			displayName = "Immobilizer 43",
			size = 206101,
			assetVersion = 1
		};
		sortedList278.Add("Pve/Immoby43", loadingAsset);
		SortedList<string, LoadingAsset> sortedList279 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Immobilizer44",
			displayName = "Immobilizer44",
			size = 206298,
			assetVersion = 1
		};
		sortedList279.Add("Pve/Immobilizer44", loadingAsset);
		SortedList<string, LoadingAsset> sortedList280 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Immobilizer45",
			displayName = "Immobilizer45",
			size = 206131,
			assetVersion = 1
		};
		sortedList280.Add("Pve/Immobilizer45", loadingAsset);
		SortedList<string, LoadingAsset> sortedList281 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/ImmobilizerBoss",
			displayName = "ImmobilizerBoss",
			size = 195267,
			assetVersion = 1
		};
		sortedList281.Add("Pve/ImmobilizerBoss", loadingAsset);
		SortedList<string, LoadingAsset> sortedList282 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/ImmobilizerEnemy",
			displayName = "ImmobilizerEnemy",
			size = 185136,
			assetVersion = 1
		};
		sortedList282.Add("Pve/ImmobilizerEnemy", loadingAsset);
		SortedList<string, LoadingAsset> sortedList283 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/ImmobilizerUltra",
			displayName = "ImmobilizerUltra",
			size = 193278,
			assetVersion = 1
		};
		sortedList283.Add("Pve/ImmobilizerUltra", loadingAsset);
		SortedList<string, LoadingAsset> sortedList284 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Parasite34",
			displayName = "Parasite34",
			size = 202294,
			assetVersion = 1
		};
		sortedList284.Add("Pve/Parasite34", loadingAsset);
		SortedList<string, LoadingAsset> sortedList285 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Parasite35",
			displayName = "Parasite35",
			size = 202231,
			assetVersion = 1
		};
		sortedList285.Add("Pve/Parasite35", loadingAsset);
		SortedList<string, LoadingAsset> sortedList286 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Parasite36",
			displayName = "Parasite36",
			size = 202200,
			assetVersion = 1
		};
		sortedList286.Add("Pve/Parasite36", loadingAsset);
		SortedList<string, LoadingAsset> sortedList287 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/ParasiteBoss",
			displayName = "ParasiteBoss",
			size = 283710,
			assetVersion = 1
		};
		sortedList287.Add("Pve/ParasiteBoss", loadingAsset);
		SortedList<string, LoadingAsset> sortedList288 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/ParasiteEnemy",
			displayName = "ParasiteEnemy",
			size = 277176,
			assetVersion = 1
		};
		sortedList288.Add("Pve/ParasiteEnemy", loadingAsset);
		SortedList<string, LoadingAsset> sortedList289 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/PveBoss1",
			displayName = "PveBoss1",
			size = 379334,
			assetVersion = 1
		};
		sortedList289.Add("Pve/PveBoss1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList290 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/PveBoss2",
			displayName = "PveBoss2",
			size = 286628,
			assetVersion = 1
		};
		sortedList290.Add("Pve/PveBoss2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList291 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/ParasiteUltra",
			displayName = "ParasiteUltra",
			size = 295279,
			assetVersion = 1
		};
		sortedList291.Add("Pve/ParasiteUltra", loadingAsset);
		SortedList<string, LoadingAsset> sortedList292 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/DefenceTurret2",
			displayName = "DefenceTurret2",
			size = 413707,
			assetVersion = 2
		};
		sortedList292.Add("Pve/DefenceTurret2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList293 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE1",
			displayName = "UltraPvE1",
			size = 20340,
			assetVersion = 1
		};
		sortedList293.Add("Pve/UltraPvE1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList294 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE2",
			displayName = "UltraPvE2",
			size = 22502,
			assetVersion = 1
		};
		sortedList294.Add("Pve/UltraPvE2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList295 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE3",
			displayName = "UltraPvE3",
			size = 25261,
			assetVersion = 1
		};
		sortedList295.Add("Pve/UltraPvE3", loadingAsset);
		SortedList<string, LoadingAsset> sortedList296 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE4",
			displayName = "UltraPvE4",
			size = 24147,
			assetVersion = 1
		};
		sortedList296.Add("Pve/UltraPvE4", loadingAsset);
		SortedList<string, LoadingAsset> sortedList297 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE5",
			displayName = "UltraPvE5",
			size = 30261,
			assetVersion = 1
		};
		sortedList297.Add("Pve/UltraPvE5", loadingAsset);
		SortedList<string, LoadingAsset> sortedList298 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE6",
			displayName = "UltraPvE6",
			size = 27963,
			assetVersion = 1
		};
		sortedList298.Add("Pve/UltraPvE6", loadingAsset);
		SortedList<string, LoadingAsset> sortedList299 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE7",
			displayName = "UltraPvE7",
			size = 29607,
			assetVersion = 1
		};
		sortedList299.Add("Pve/UltraPvE7", loadingAsset);
		SortedList<string, LoadingAsset> sortedList300 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE8",
			displayName = "UltraPvE8",
			size = 24366,
			assetVersion = 1
		};
		sortedList300.Add("Pve/UltraPvE8", loadingAsset);
		SortedList<string, LoadingAsset> sortedList301 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE9",
			displayName = "UltraPvE9",
			size = 27286,
			assetVersion = 1
		};
		sortedList301.Add("Pve/UltraPvE9", loadingAsset);
		SortedList<string, LoadingAsset> sortedList302 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE10",
			displayName = "UltraPvE10",
			size = 26525,
			assetVersion = 1
		};
		sortedList302.Add("Pve/UltraPvE10", loadingAsset);
		SortedList<string, LoadingAsset> sortedList303 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE11",
			displayName = "UltraPvE11",
			size = 28050,
			assetVersion = 1
		};
		sortedList303.Add("Pve/UltraPvE11", loadingAsset);
		SortedList<string, LoadingAsset> sortedList304 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE12",
			displayName = "UltraPvE12",
			size = 24137,
			assetVersion = 1
		};
		sortedList304.Add("Pve/UltraPvE12", loadingAsset);
		SortedList<string, LoadingAsset> sortedList305 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE13",
			displayName = "UltraPvE13",
			size = 51002,
			assetVersion = 1
		};
		sortedList305.Add("Pve/UltraPvE13", loadingAsset);
		SortedList<string, LoadingAsset> sortedList306 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE14",
			displayName = "UltraPvE14",
			size = 26730,
			assetVersion = 1
		};
		sortedList306.Add("Pve/UltraPvE14", loadingAsset);
		SortedList<string, LoadingAsset> sortedList307 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE15",
			displayName = "UltraPvE15",
			size = 33128,
			assetVersion = 1
		};
		sortedList307.Add("Pve/UltraPvE15", loadingAsset);
		SortedList<string, LoadingAsset> sortedList308 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE16",
			displayName = "UltraPvE16",
			size = 20627,
			assetVersion = 1
		};
		sortedList308.Add("Pve/UltraPvE16", loadingAsset);
		SortedList<string, LoadingAsset> sortedList309 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE17",
			displayName = "UltraPvE17",
			size = 18148,
			assetVersion = 1
		};
		sortedList309.Add("Pve/UltraPvE17", loadingAsset);
		SortedList<string, LoadingAsset> sortedList310 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE18",
			displayName = "UltraPvE18",
			size = 37359,
			assetVersion = 1
		};
		sortedList310.Add("Pve/UltraPvE18", loadingAsset);
		SortedList<string, LoadingAsset> sortedList311 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE19",
			displayName = "UltraPvE19",
			size = 29286,
			assetVersion = 1
		};
		sortedList311.Add("Pve/UltraPvE19", loadingAsset);
		SortedList<string, LoadingAsset> sortedList312 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE20",
			displayName = "UltraPvE20",
			size = 15989,
			assetVersion = 1
		};
		sortedList312.Add("Pve/UltraPvE20", loadingAsset);
		SortedList<string, LoadingAsset> sortedList313 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE21",
			displayName = "UltraPvE21",
			size = 15402,
			assetVersion = 1
		};
		sortedList313.Add("Pve/UltraPvE21", loadingAsset);
		SortedList<string, LoadingAsset> sortedList314 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE22",
			displayName = "UltraPvE22",
			size = 15405,
			assetVersion = 1
		};
		sortedList314.Add("Pve/UltraPvE22", loadingAsset);
		SortedList<string, LoadingAsset> sortedList315 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE23",
			displayName = "UltraPvE23",
			size = 21550,
			assetVersion = 1
		};
		sortedList315.Add("Pve/UltraPvE23", loadingAsset);
		SortedList<string, LoadingAsset> sortedList316 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE24",
			displayName = "UltraPvE24",
			size = 20790,
			assetVersion = 1
		};
		sortedList316.Add("Pve/UltraPvE24", loadingAsset);
		SortedList<string, LoadingAsset> sortedList317 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE25",
			displayName = "UltraPvE25",
			size = 26446,
			assetVersion = 1
		};
		sortedList317.Add("Pve/UltraPvE25", loadingAsset);
		SortedList<string, LoadingAsset> sortedList318 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE26",
			displayName = "UltraPvE26",
			size = 21853,
			assetVersion = 1
		};
		sortedList318.Add("Pve/UltraPvE26", loadingAsset);
		SortedList<string, LoadingAsset> sortedList319 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/UltraPvE27",
			displayName = "UltraPvE27",
			size = 40110,
			assetVersion = 1
		};
		sortedList319.Add("Pve/UltraPvE27", loadingAsset);
		SortedList<string, LoadingAsset> sortedList320 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Mat,
			assetName = "Pve/UltraPvE_Mat1",
			displayName = "UltraPvE Skin 1",
			size = 960935,
			assetVersion = 1
		};
		sortedList320.Add("Pve/UltraPvE_Mat1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList321 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Mat,
			assetName = "Pve/UltraPvE_Mat2",
			displayName = "UltraPvE Skin 2",
			size = 1648773,
			assetVersion = 1
		};
		sortedList321.Add("Pve/UltraPvE_Mat2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList322 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Mat,
			assetName = "Pve/UltraPvESecondary_Mat1",
			displayName = "UltraPvE Secondary Skin 1",
			size = 9401,
			assetVersion = 1
		};
		sortedList322.Add("Pve/UltraPvESecondary_Mat1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList323 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Mat,
			assetName = "Pve/UltraPvESecondary_Mat2",
			displayName = "UltraPvE Secondary Skin 2",
			size = 9402,
			assetVersion = 1
		};
		sortedList323.Add("Pve/UltraPvESecondary_Mat2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList324 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Xdroid1",
			displayName = "Xdroid1",
			size = 57580,
			assetVersion = 1
		};
		sortedList324.Add("Pve/Xdroid1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList325 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Xdroid2",
			displayName = "Xdroid2",
			size = 49092,
			assetVersion = 1
		};
		sortedList325.Add("Pve/Xdroid2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList326 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Xdroid3",
			displayName = "Xdroid3",
			size = 67341,
			assetVersion = 1
		};
		sortedList326.Add("Pve/Xdroid3", loadingAsset);
		SortedList<string, LoadingAsset> sortedList327 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Xdroid4",
			displayName = "Xdroid4",
			size = 62778,
			assetVersion = 1
		};
		sortedList327.Add("Pve/Xdroid4", loadingAsset);
		SortedList<string, LoadingAsset> sortedList328 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Xdroid5",
			displayName = "Xdroid5",
			size = 47209,
			assetVersion = 1
		};
		sortedList328.Add("Pve/Xdroid5", loadingAsset);
		SortedList<string, LoadingAsset> sortedList329 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Xdroid6",
			displayName = "Xdroid6",
			size = 74133,
			assetVersion = 1
		};
		sortedList329.Add("Pve/Xdroid6", loadingAsset);
		SortedList<string, LoadingAsset> sortedList330 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Xdroid7",
			displayName = "Xdroid7",
			size = 62584,
			assetVersion = 1
		};
		sortedList330.Add("Pve/Xdroid7", loadingAsset);
		SortedList<string, LoadingAsset> sortedList331 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Xdroid8",
			displayName = "Xdroid8",
			size = 48236,
			assetVersion = 1
		};
		sortedList331.Add("Pve/Xdroid8", loadingAsset);
		SortedList<string, LoadingAsset> sortedList332 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Xdroid9",
			displayName = "Xdroid9",
			size = 38940,
			assetVersion = 1
		};
		sortedList332.Add("Pve/Xdroid9", loadingAsset);
		SortedList<string, LoadingAsset> sortedList333 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Mat,
			assetName = "Pve/Xdroid_Mat1",
			displayName = "Xdroid Skin 1",
			size = 804221,
			assetVersion = 1
		};
		sortedList333.Add("Pve/Xdroid_Mat1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList334 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Xparasite1",
			displayName = "Xparasite1",
			size = 54497,
			assetVersion = 1
		};
		sortedList334.Add("Pve/Xparasite1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList335 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Xparasite2",
			displayName = "Xparasite2",
			size = 58976,
			assetVersion = 1
		};
		sortedList335.Add("Pve/Xparasite2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList336 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Xparasite3",
			displayName = "Xparasite3",
			size = 43775,
			assetVersion = 1
		};
		sortedList336.Add("Pve/Xparasite3", loadingAsset);
		SortedList<string, LoadingAsset> sortedList337 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Xparasite4",
			displayName = "Xparasite4",
			size = 55855,
			assetVersion = 1
		};
		sortedList337.Add("Pve/Xparasite4", loadingAsset);
		SortedList<string, LoadingAsset> sortedList338 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Xparasite5",
			displayName = "Xparasite5",
			size = 39639,
			assetVersion = 1
		};
		sortedList338.Add("Pve/Xparasite5", loadingAsset);
		SortedList<string, LoadingAsset> sortedList339 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/Xparasite6",
			displayName = "Xparasite6",
			size = 50818,
			assetVersion = 1
		};
		sortedList339.Add("Pve/Xparasite6", loadingAsset);
		SortedList<string, LoadingAsset> sortedList340 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Mat,
			assetName = "Pve/Xparasite_Mat1",
			displayName = "Xparasite Skin 1",
			size = 480105,
			assetVersion = 1
		};
		sortedList340.Add("Pve/Xparasite_Mat1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList341 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/CristmasShipBoss1",
			displayName = "CristmasShipBoss1",
			size = 1626249,
			assetVersion = 1
		};
		sortedList341.Add("Pve/CristmasShipBoss1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList342 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/CristmasShipBoss2",
			displayName = "CristmasShipBoss2",
			size = 1603063,
			assetVersion = 1
		};
		sortedList342.Add("Pve/CristmasShipBoss2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList343 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/CristmasShipBoss3",
			displayName = "CristmasShipBoss3",
			size = 1614304,
			assetVersion = 1
		};
		sortedList343.Add("Pve/CristmasShipBoss3", loadingAsset);
		SortedList<string, LoadingAsset> sortedList344 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/SpecialBully1",
			displayName = "SpecialBully1",
			size = 24316,
			assetVersion = 100
		};
		sortedList344.Add("Pve/SpecialBully1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList345 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/SpecialBully2",
			displayName = "SpecialBully2",
			size = 24316,
			assetVersion = 100
		};
		sortedList345.Add("Pve/SpecialBully2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList346 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/SpecialBully3",
			displayName = "SpecialBully3",
			size = 24316,
			assetVersion = 100
		};
		sortedList346.Add("Pve/SpecialBully3", loadingAsset);
		SortedList<string, LoadingAsset> sortedList347 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/SpecialBully4",
			displayName = "SpecialBully4",
			size = 24316,
			assetVersion = 100
		};
		sortedList347.Add("Pve/SpecialBully4", loadingAsset);
		SortedList<string, LoadingAsset> sortedList348 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/SpecialBully5",
			displayName = "SpecialBully5",
			size = 24316,
			assetVersion = 100
		};
		sortedList348.Add("Pve/SpecialBully5", loadingAsset);
		SortedList<string, LoadingAsset> sortedList349 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/SpecialBully6",
			displayName = "SpecialBully6",
			size = 24316,
			assetVersion = 100
		};
		sortedList349.Add("Pve/SpecialBully6", loadingAsset);
		SortedList<string, LoadingAsset> sortedList350 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/SpecialBully7",
			displayName = "SpecialBully7",
			size = 24316,
			assetVersion = 100
		};
		sortedList350.Add("Pve/SpecialBully7", loadingAsset);
		SortedList<string, LoadingAsset> sortedList351 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/SpecialBully8",
			displayName = "SpecialBully8",
			size = 24316,
			assetVersion = 100
		};
		sortedList351.Add("Pve/SpecialBully8", loadingAsset);
		SortedList<string, LoadingAsset> sortedList352 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Mat,
			assetName = "Pve/SpecialBully_Mat1",
			displayName = "SpecialBully Skin 1",
			size = 719387,
			assetVersion = 100
		};
		sortedList352.Add("Pve/SpecialBully_Mat1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList353 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Mat,
			assetName = "Pve/SpecialBully_Mat2",
			displayName = "SpecialBully Skin 2",
			size = 719387,
			assetVersion = 100
		};
		sortedList353.Add("Pve/SpecialBully_Mat2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList354 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Mat,
			assetName = "Pve/SpecialBully_Mat3",
			displayName = "SpecialBully Skin 3",
			size = 719387,
			assetVersion = 100
		};
		sortedList354.Add("Pve/SpecialBully_Mat3", loadingAsset);
		SortedList<string, LoadingAsset> sortedList355 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Mat,
			assetName = "Pve/SpecialBully_Mat4",
			displayName = "SpecialBully Skin 4",
			size = 719387,
			assetVersion = 100
		};
		sortedList355.Add("Pve/SpecialBully_Mat4", loadingAsset);
		SortedList<string, LoadingAsset> sortedList356 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Mat,
			assetName = "Pve/SpecialBully_Mat5",
			displayName = "SpecialBully Skin 5",
			size = 719387,
			assetVersion = 100
		};
		sortedList356.Add("Pve/SpecialBully_Mat5", loadingAsset);
		SortedList<string, LoadingAsset> sortedList357 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "HyperJumpBeamPfb",
			displayName = "HyperJumpBeamPfb",
			size = 7075,
			assetVersion = 1
		};
		sortedList357.Add("HyperJumpBeamPfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList358 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "HyperJumpEnter",
			displayName = "HyperJumpEnter",
			size = 293607,
			assetVersion = 1
		};
		sortedList358.Add("HyperJumpEnter", loadingAsset);
		SortedList<string, LoadingAsset> sortedList359 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "HyperJumpOut",
			displayName = "HyperJumpOut",
			size = 170188,
			assetVersion = 1
		};
		sortedList359.Add("HyperJumpOut", loadingAsset);
		SortedList<string, LoadingAsset> sortedList360 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "HyperJumpOutPfb",
			displayName = "HyperJumpOutPfb",
			size = 6908,
			assetVersion = 1
		};
		sortedList360.Add("HyperJumpOutPfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList361 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "GalaxyJumpPfb",
			displayName = "GalaxyJumpPfb",
			size = 7019,
			assetVersion = 1
		};
		sortedList361.Add("GalaxyJumpPfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList362 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "ShootLockYellow",
			displayName = "ShootLockYellow",
			size = 66606,
			assetVersion = 1
		};
		sortedList362.Add("ShootLockYellow", loadingAsset);
		SortedList<string, LoadingAsset> sortedList363 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "Mineables",
			displayName = "Mineables",
			size = 27953,
			assetVersion = 1
		};
		sortedList363.Add("Mineables", loadingAsset);
		SortedList<string, LoadingAsset> sortedList364 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "LevelUP_pfb",
			displayName = "LevelUP_pfb",
			size = 178370,
			assetVersion = 1
		};
		sortedList364.Add("LevelUP_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList365 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "LevelUPDown",
			displayName = "LevelUPDown",
			size = 65059,
			assetVersion = 1
		};
		sortedList365.Add("LevelUPDown", loadingAsset);
		SortedList<string, LoadingAsset> sortedList366 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "LevelUpLights",
			displayName = "LevelUpLights",
			size = 72258,
			assetVersion = 1
		};
		sortedList366.Add("LevelUpLights", loadingAsset);
		SortedList<string, LoadingAsset> sortedList367 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "LevelUPSide",
			displayName = "LevelUPSide",
			size = 153940,
			assetVersion = 1
		};
		sortedList367.Add("LevelUPSide", loadingAsset);
		SortedList<string, LoadingAsset> sortedList368 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "skillReloading",
			displayName = "skillReloading",
			size = 995,
			assetVersion = 1
		};
		sortedList368.Add("skillReloading", loadingAsset);
		SortedList<string, LoadingAsset> sortedList369 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "ActiveSkill_pfb",
			displayName = "ActiveSkill_pfb",
			size = 1206,
			assetVersion = 1
		};
		sortedList369.Add("ActiveSkill_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList370 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "FreeSpaceSkillRange_pfb",
			displayName = "FreeSpaceSkillRange_pfb",
			size = 77067,
			assetVersion = 1
		};
		sortedList370.Add("FreeSpaceSkillRange_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList371 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillRange_pfb",
			displayName = "SkillRange_pfb",
			size = 77184,
			assetVersion = 2
		};
		sortedList371.Add("SkillRange_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList372 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillRangeWeapons_pfb",
			displayName = "SkillRangeWeapons_pfb",
			size = 44271,
			assetVersion = 1
		};
		sortedList372.Add("SkillRangeWeapons_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList373 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "AimCircle_pfb",
			displayName = "AimCircle_pfb",
			size = 50430,
			assetVersion = 1
		};
		sortedList373.Add("AimCircle_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList374 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillRemedy_pfb",
			displayName = "SkillRemedy_pfb",
			size = 29045,
			assetVersion = 1
		};
		sortedList374.Add("SkillRemedy_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList375 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillRepairField_pfb",
			displayName = "SkillRepairField_pfb",
			size = 131594,
			assetVersion = 1
		};
		sortedList375.Add("SkillRepairField_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList376 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillShortCircuit_pfb",
			displayName = "SkillShortCircuit_pfb",
			size = 57657,
			assetVersion = 1
		};
		sortedList376.Add("SkillShortCircuit_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList377 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillNanoStorm_pfb",
			displayName = "SkillNanoStorm_pfb",
			size = 125280,
			assetVersion = 1
		};
		sortedList377.Add("SkillNanoStorm_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList378 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillNanoShield_pfb",
			displayName = "SkillNanoShield_pfb",
			size = 195763,
			assetVersion = 1
		};
		sortedList378.Add("SkillNanoShield_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList379 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillPulseNova_pfb",
			displayName = "SkillPulseNova_pfb",
			size = 6806,
			assetVersion = 1
		};
		sortedList379.Add("SkillPulseNova_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList380 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "PulseNova",
			displayName = "PulseNova",
			size = 425057,
			assetVersion = 1
		};
		sortedList380.Add("PulseNova", loadingAsset);
		SortedList<string, LoadingAsset> sortedList381 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillSunderArmor_pfb",
			displayName = "SkillSunderArmor_pfb",
			size = 364786,
			assetVersion = 1
		};
		sortedList381.Add("SkillSunderArmor_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList382 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillRocketBarrage_pfb",
			displayName = "SkillRocketBarrage_pfb",
			size = 297377,
			assetVersion = 1
		};
		sortedList382.Add("SkillRocketBarrage_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList383 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillRepairingDrones_pfb",
			displayName = "SkillRepairingDrones_pfb",
			size = 440539,
			assetVersion = 1
		};
		sortedList383.Add("SkillRepairingDrones_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList384 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillTaunt_pfb",
			displayName = "SkillTaunt_pfb",
			size = 237595,
			assetVersion = 1
		};
		sortedList384.Add("SkillTaunt_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList385 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillShieldFortress_pfb",
			displayName = "SkillShieldFortress_pfb",
			size = 22577,
			assetVersion = 1
		};
		sortedList385.Add("SkillShieldFortress_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList386 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillFocusFire_pfb",
			displayName = "SkillFocusFire_pfb",
			size = 28065,
			assetVersion = 1
		};
		sortedList386.Add("SkillFocusFire_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList387 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillForceWave_pfb",
			displayName = "SkillForceWave_pfb",
			size = 1540493,
			assetVersion = 2
		};
		sortedList387.Add("SkillForceWave_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList388 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillUnstoppable_pfb",
			displayName = "SkillUnstoppable_pfb",
			size = 6823,
			assetVersion = 1
		};
		sortedList388.Add("SkillUnstoppable_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList389 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "Unstoppable",
			displayName = "Unstoppable",
			size = 127308,
			assetVersion = 1
		};
		sortedList389.Add("Unstoppable", loadingAsset);
		SortedList<string, LoadingAsset> sortedList390 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillDecoy_pjc",
			displayName = "SkillDecoy_pjc",
			size = 25586,
			assetVersion = 1
		};
		sortedList390.Add("SkillDecoy_pjc", loadingAsset);
		SortedList<string, LoadingAsset> sortedList391 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillLaserDestruction_pfb",
			displayName = "SkillLaserDestruction_pfb",
			size = 10825,
			assetVersion = 1
		};
		sortedList391.Add("SkillLaserDestruction_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList392 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillPowerBreak_pfb",
			displayName = "SkillPowerBreak_pfb",
			size = 228421,
			assetVersion = 1
		};
		sortedList392.Add("SkillPowerBreak_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList393 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillLightSpeed_pfb",
			displayName = "SkillLightSpeed_pfb",
			size = 24685,
			assetVersion = 1
		};
		sortedList393.Add("SkillLightSpeed_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList394 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillPowerCut_pjc",
			displayName = "SkillPowerCut_pjc",
			size = 34116,
			assetVersion = 1
		};
		sortedList394.Add("SkillPowerCut_pjc", loadingAsset);
		SortedList<string, LoadingAsset> sortedList395 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillMistShroud_pfb",
			displayName = "SkillMistShroud_pfb",
			size = 9272,
			assetVersion = 1
		};
		sortedList395.Add("SkillMistShroud_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList396 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "MistShroud",
			displayName = "MistShroud",
			size = 497369,
			assetVersion = 1
		};
		sortedList396.Add("MistShroud", loadingAsset);
		SortedList<string, LoadingAsset> sortedList397 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "GuidingArrow_pfb",
			displayName = "GuidingArrow_pfb",
			size = 12079,
			assetVersion = 1
		};
		sortedList397.Add("GuidingArrow_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList398 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "PartyArrow1_pfb",
			displayName = "PartyArrow1_pfb",
			size = 8874,
			assetVersion = 2
		};
		sortedList398.Add("PartyArrow1_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList399 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "PartyArrow2_pfb",
			displayName = "PartyArrow2_pfb",
			size = 8877,
			assetVersion = 2
		};
		sortedList399.Add("PartyArrow2_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList400 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "PartyArrow3_pfb",
			displayName = "PartyArrow3_pfb",
			size = 8852,
			assetVersion = 2
		};
		sortedList400.Add("PartyArrow3_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList401 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "QuestCompleted_pfb",
			displayName = "QuestCompleted_pfb",
			size = 96053,
			assetVersion = 1
		};
		sortedList401.Add("QuestCompleted_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList402 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "StoryQuestCompleted_pfb",
			displayName = "StoryQuestCompleted_pfb",
			size = 138115,
			assetVersion = 1
		};
		sortedList402.Add("StoryQuestCompleted_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList403 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "Screen1",
			displayName = "Screen1",
			size = 151631,
			assetVersion = 1
		};
		sortedList403.Add("Screen1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList404 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "Screen2",
			displayName = "Screen2",
			size = 75342,
			assetVersion = 1
		};
		sortedList404.Add("Screen2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList405 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "Screen3",
			displayName = "Screen3",
			size = 36942,
			assetVersion = 1
		};
		sortedList405.Add("Screen3", loadingAsset);
		SortedList<string, LoadingAsset> sortedList406 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "InBaseAudio",
			displayName = "InBaseAudio",
			size = 121022,
			assetVersion = 1
		};
		sortedList406.Add("InBaseAudio", loadingAsset);
		SortedList<string, LoadingAsset> sortedList407 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "boosterIco",
			displayName = "boosterIco",
			size = 8515,
			assetVersion = 1
		};
		sortedList407.Add("boosterIco", loadingAsset);
		SortedList<string, LoadingAsset> sortedList408 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "lampa",
			displayName = "lampa",
			size = 5689,
			assetVersion = 1
		};
		sortedList408.Add("lampa", loadingAsset);
		SortedList<string, LoadingAsset> sortedList409 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "InSpaceVoices",
			displayName = "InSpaceVoices",
			size = 326985,
			assetVersion = 1
		};
		sortedList409.Add("InSpaceVoices", loadingAsset);
		SortedList<string, LoadingAsset> sortedList410 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_Aria_pfb",
			displayName = "NPC Aria",
			size = 464471,
			assetVersion = 1
		};
		sortedList410.Add("NPC/NPC_Aria_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList411 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_CaribbeanJoe_pfb",
			displayName = "NPC CaribbeanJoe",
			size = 510907,
			assetVersion = 1
		};
		sortedList411.Add("NPC/NPC_CaribbeanJoe_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList412 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_Darius_pfb",
			displayName = "NPC Darius",
			size = 502250,
			assetVersion = 1
		};
		sortedList412.Add("NPC/NPC_Darius_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList413 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_EddFinn_pfb",
			displayName = "NPC Edd Fin",
			size = 426639,
			assetVersion = 1
		};
		sortedList413.Add("NPC/NPC_EddFinn_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList414 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_Eddie_pfb",
			displayName = "NPC Eddie",
			size = 459592,
			assetVersion = 1
		};
		sortedList414.Add("NPC/NPC_Eddie_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList415 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_Gabriel_pfb",
			displayName = "NPC Gabriel",
			size = 415167,
			assetVersion = 1
		};
		sortedList415.Add("NPC/NPC_Gabriel_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList416 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_Golgotha_pfb",
			displayName = "NPC Golgotha",
			size = 455364,
			assetVersion = 1
		};
		sortedList416.Add("NPC/NPC_Golgotha_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList417 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_James_pfb",
			displayName = "NPC James",
			size = 532815,
			assetVersion = 1
		};
		sortedList417.Add("NPC/NPC_James_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList418 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_JohnnyDigger_pfb",
			displayName = "NPC Johnny Digger",
			size = 494383,
			assetVersion = 1
		};
		sortedList418.Add("NPC/NPC_JohnnyDigger_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList419 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_Keon_pfb",
			displayName = "NPC Keon",
			size = 675014,
			assetVersion = 1
		};
		sortedList419.Add("NPC/NPC_Keon_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList420 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_Louise_pfb",
			displayName = "NPC Louise",
			size = 288665,
			assetVersion = 1
		};
		sortedList420.Add("NPC/NPC_Louise_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList421 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_Loyce_pfb",
			displayName = "NPC Loyce",
			size = 672642,
			assetVersion = 1
		};
		sortedList421.Add("NPC/NPC_Loyce_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList422 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_LtBrown_pfb",
			displayName = "NPC Lt. Brown",
			size = 566358,
			assetVersion = 1
		};
		sortedList422.Add("NPC/NPC_LtBrown_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList423 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_Luther_pfb",
			displayName = "NPC Luther",
			size = 912620,
			assetVersion = 1
		};
		sortedList423.Add("NPC/NPC_Luther_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList424 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_MorbidSimon_pfb",
			displayName = "NPC MorbidSimon",
			size = 496716,
			assetVersion = 1
		};
		sortedList424.Add("NPC/NPC_MorbidSimon_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList425 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_Nassor_pfb",
			displayName = "NPC Nassor",
			size = 233868,
			assetVersion = 1
		};
		sortedList425.Add("NPC/NPC_Nassor_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList426 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_Oleg_pfb",
			displayName = "NPC Oleg",
			size = 171013,
			assetVersion = 1
		};
		sortedList426.Add("NPC/NPC_Oleg_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList427 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_Patton_pfb",
			displayName = "NPC Patton",
			size = 647384,
			assetVersion = 1
		};
		sortedList427.Add("NPC/NPC_Patton_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList428 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_Reese_pfb",
			displayName = "NPC Reese",
			size = 605988,
			assetVersion = 1
		};
		sortedList428.Add("NPC/NPC_Reese_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList429 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_SamHawkins_pfb",
			displayName = "NPC Sam Hawkins",
			size = 544966,
			assetVersion = 1
		};
		sortedList429.Add("NPC/NPC_SamHawkins_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList430 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_Stalker_pfb",
			displayName = "NPC Stalker",
			size = 664453,
			assetVersion = 1
		};
		sortedList430.Add("NPC/NPC_Stalker_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList431 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_TedClancey_pfb",
			displayName = "NPC Ted Clancey",
			size = 446680,
			assetVersion = 1
		};
		sortedList431.Add("NPC/NPC_TedClancey_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList432 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_Thane_pfb",
			displayName = "NPC Thane",
			size = 1648273,
			assetVersion = 1
		};
		sortedList432.Add("NPC/NPC_Thane_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList433 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_Vladimir_pfb",
			displayName = "NPC Vladimir",
			size = 469627,
			assetVersion = 1
		};
		sortedList433.Add("NPC/NPC_Vladimir_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList434 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_Walter_pfb",
			displayName = "NPC Walter",
			size = 198942,
			assetVersion = 1
		};
		sortedList434.Add("NPC/NPC_Walter_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList435 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_Xena_pfb",
			displayName = "NPC Xena",
			size = 869305,
			assetVersion = 1
		};
		sortedList435.Add("NPC/NPC_Xena_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList436 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_Lancer_pfb",
			displayName = "NPC Lancer",
			size = 895464,
			assetVersion = 1
		};
		sortedList436.Add("NPC/NPC_Lancer_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList437 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_Ruby_pfb",
			displayName = "NPC Ruby",
			size = 875976,
			assetVersion = 1
		};
		sortedList437.Add("NPC/NPC_Ruby_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList438 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_Leona_pfb",
			displayName = "NPC Leona",
			size = 1157048,
			assetVersion = 1
		};
		sortedList438.Add("NPC/NPC_Leona_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList439 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "NPC/NPC_Skye_pfb",
			displayName = "NPC Skye",
			size = 669659,
			assetVersion = 1
		};
		sortedList439.Add("NPC/NPC_Skye_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList440 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "NPC/NPC_Darius_audio_assets",
			displayName = "NPC Darius Audio",
			size = 613208,
			assetVersion = 1
		};
		sortedList440.Add("NPC/NPC_Darius_audio_assets", loadingAsset);
		SortedList<string, LoadingAsset> sortedList441 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "NPC/NPC_CaribbeanJoe_audio_assets",
			displayName = "NPC CaribbeanJoe Audio",
			size = 650197,
			assetVersion = 1
		};
		sortedList441.Add("NPC/NPC_CaribbeanJoe_audio_assets", loadingAsset);
		SortedList<string, LoadingAsset> sortedList442 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "NPC/NPC_EddFinn_audio_assets",
			displayName = "NPC EddFinn Audio",
			size = 432336,
			assetVersion = 1
		};
		sortedList442.Add("NPC/NPC_EddFinn_audio_assets", loadingAsset);
		SortedList<string, LoadingAsset> sortedList443 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "NPC/NPC_Eddie_audio_assets",
			displayName = "NPC Eddie Audio",
			size = 393845,
			assetVersion = 1
		};
		sortedList443.Add("NPC/NPC_Eddie_audio_assets", loadingAsset);
		SortedList<string, LoadingAsset> sortedList444 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "NPC/NPC_Gabriel_audio_assets",
			displayName = "NPC Gabriel Audio",
			size = 567747,
			assetVersion = 1
		};
		sortedList444.Add("NPC/NPC_Gabriel_audio_assets", loadingAsset);
		SortedList<string, LoadingAsset> sortedList445 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "NPC/NPC_James_audio_assets",
			displayName = "NPC James Audio",
			size = 460837,
			assetVersion = 1
		};
		sortedList445.Add("NPC/NPC_James_audio_assets", loadingAsset);
		SortedList<string, LoadingAsset> sortedList446 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "NPC/NPC_JohnnyDigger_audio_assets",
			displayName = "NPC JohnnyDigger Audio",
			size = 814583,
			assetVersion = 1
		};
		sortedList446.Add("NPC/NPC_JohnnyDigger_audio_assets", loadingAsset);
		SortedList<string, LoadingAsset> sortedList447 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "NPC/NPC_Keon_audio_assets",
			displayName = "NPC Keon Audio",
			size = 546838,
			assetVersion = 1
		};
		sortedList447.Add("NPC/NPC_Keon_audio_assets", loadingAsset);
		SortedList<string, LoadingAsset> sortedList448 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "NPC/NPC_Louise_audio_assets",
			displayName = "NPC Louise Audio",
			size = 586516,
			assetVersion = 1
		};
		sortedList448.Add("NPC/NPC_Louise_audio_assets", loadingAsset);
		SortedList<string, LoadingAsset> sortedList449 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "NPC/NPC_Loyce_audio_assets",
			displayName = "NPC Loyce Audio",
			size = 579603,
			assetVersion = 1
		};
		sortedList449.Add("NPC/NPC_Loyce_audio_assets", loadingAsset);
		SortedList<string, LoadingAsset> sortedList450 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "NPC/NPC_LtBrown_audio_assets",
			displayName = "NPC LtBrown Audio",
			size = 299781,
			assetVersion = 1
		};
		sortedList450.Add("NPC/NPC_LtBrown_audio_assets", loadingAsset);
		SortedList<string, LoadingAsset> sortedList451 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "NPC/NPC_Luther_audio_assets",
			displayName = "NPC MorbidSimon Audio",
			size = 812152,
			assetVersion = 1
		};
		sortedList451.Add("NPC/NPC_Luther_audio_assets", loadingAsset);
		SortedList<string, LoadingAsset> sortedList452 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "NPC/NPC_MorbidSimon_audio_assets",
			displayName = "NPC Loyce Audio",
			size = 472560,
			assetVersion = 1
		};
		sortedList452.Add("NPC/NPC_MorbidSimon_audio_assets", loadingAsset);
		SortedList<string, LoadingAsset> sortedList453 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "NPC/NPC_Nassor_audio_assets",
			displayName = "NPC Nassor Audio",
			size = 640327,
			assetVersion = 1
		};
		sortedList453.Add("NPC/NPC_Nassor_audio_assets", loadingAsset);
		SortedList<string, LoadingAsset> sortedList454 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "NPC/NPC_Oleg_audio_assets",
			displayName = "NPC Oleg Audio",
			size = 715391,
			assetVersion = 1
		};
		sortedList454.Add("NPC/NPC_Oleg_audio_assets", loadingAsset);
		SortedList<string, LoadingAsset> sortedList455 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "NPC/NPC_Patton_audio_assets",
			displayName = "NPC Patton Audio",
			size = 645785,
			assetVersion = 1
		};
		sortedList455.Add("NPC/NPC_Patton_audio_assets", loadingAsset);
		SortedList<string, LoadingAsset> sortedList456 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "NPC/NPC_Reese_audio_assets",
			displayName = "NPC Reese Audio",
			size = 776092,
			assetVersion = 1
		};
		sortedList456.Add("NPC/NPC_Reese_audio_assets", loadingAsset);
		SortedList<string, LoadingAsset> sortedList457 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "NPC/NPC_SamHawkins_audio_assets",
			displayName = "NPC SamHawkins Audio",
			size = 666718,
			assetVersion = 1
		};
		sortedList457.Add("NPC/NPC_SamHawkins_audio_assets", loadingAsset);
		SortedList<string, LoadingAsset> sortedList458 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "NPC/NPC_Stalker_audio_assets",
			displayName = "NPC Stalker Audio",
			size = 465260,
			assetVersion = 1
		};
		sortedList458.Add("NPC/NPC_Stalker_audio_assets", loadingAsset);
		SortedList<string, LoadingAsset> sortedList459 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "NPC/NPC_TedClancey_audio_assets",
			displayName = "NPC TedClancey Audio",
			size = 640490,
			assetVersion = 1
		};
		sortedList459.Add("NPC/NPC_TedClancey_audio_assets", loadingAsset);
		SortedList<string, LoadingAsset> sortedList460 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "NPC/NPC_Thane_audio_assets",
			displayName = "NPC Thane Audio",
			size = 714843,
			assetVersion = 1
		};
		sortedList460.Add("NPC/NPC_Thane_audio_assets", loadingAsset);
		SortedList<string, LoadingAsset> sortedList461 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "NPC/NPC_Vladimir_audio_assets",
			displayName = "NPC Vladimir Audio",
			size = 897546,
			assetVersion = 1
		};
		sortedList461.Add("NPC/NPC_Vladimir_audio_assets", loadingAsset);
		SortedList<string, LoadingAsset> sortedList462 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "NPC/NPC_Walter_audio_assets",
			displayName = "NPC Walter Audio",
			size = 606296,
			assetVersion = 1
		};
		sortedList462.Add("NPC/NPC_Walter_audio_assets", loadingAsset);
		SortedList<string, LoadingAsset> sortedList463 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "NPC/NPC_Xena_audio_assets",
			displayName = "NPC Xena Audio",
			size = 758137,
			assetVersion = 1
		};
		sortedList463.Add("NPC/NPC_Xena_audio_assets", loadingAsset);
		SortedList<string, LoadingAsset> sortedList464 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Activatable_pfb",
			displayName = "Activatable",
			size = 230879,
			assetVersion = 10
		};
		sortedList464.Add("Activatable_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList465 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Investigatable_pfb",
			displayName = "Investigatable",
			size = 670662,
			assetVersion = 1
		};
		sortedList465.Add("Investigatable_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList466 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Sabotagable_pfb",
			displayName = "Sabotagable",
			size = 417335,
			assetVersion = 1
		};
		sortedList466.Add("Sabotagable_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList467 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Platform_pfb",
			displayName = "Platform",
			size = 572563,
			assetVersion = 1
		};
		sortedList467.Add("Platform_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList468 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "platformWithShip_pfb",
			displayName = "Platform with ship",
			size = 1014757,
			assetVersion = 1
		};
		sortedList468.Add("platformWithShip_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList469 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "volkr_pfb",
			displayName = "volkr_pfb",
			size = 381463,
			assetVersion = 1
		};
		sortedList469.Add("volkr_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList470 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "golgotha_pfb",
			displayName = "golgotha_pfb",
			size = 288948,
			assetVersion = 1
		};
		sortedList470.Add("golgotha_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList471 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "chat",
			displayName = "Chat GUI",
			size = 81719,
			assetVersion = 5
		};
		sortedList471.Add("chat", loadingAsset);
		SortedList<string, LoadingAsset> sortedList472 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Multikills/MultiKill_1",
			displayName = "DoubleKill",
			size = 229103,
			assetVersion = 1
		};
		sortedList472.Add("Multikills/MultiKill_1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList473 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Multikills/MultiKill_2",
			displayName = "TripleKill",
			size = 228692,
			assetVersion = 1
		};
		sortedList473.Add("Multikills/MultiKill_2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList474 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Multikills/MultiKill_3",
			displayName = "MultiKill",
			size = 228389,
			assetVersion = 1
		};
		sortedList474.Add("Multikills/MultiKill_3", loadingAsset);
		SortedList<string, LoadingAsset> sortedList475 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Multikills/MultiKill_4",
			displayName = "UltraKill",
			size = 228893,
			assetVersion = 1
		};
		sortedList475.Add("Multikills/MultiKill_4", loadingAsset);
		SortedList<string, LoadingAsset> sortedList476 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Multikills/MultiKill_5",
			displayName = "KillingSpree",
			size = 228541,
			assetVersion = 1
		};
		sortedList476.Add("Multikills/MultiKill_5", loadingAsset);
		SortedList<string, LoadingAsset> sortedList477 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Multikills/MultiKill_6",
			displayName = "Unstoppable",
			size = 229172,
			assetVersion = 1
		};
		sortedList477.Add("Multikills/MultiKill_6", loadingAsset);
		SortedList<string, LoadingAsset> sortedList478 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Multikills/MultiKill_7",
			displayName = "Devastation",
			size = 228939,
			assetVersion = 1
		};
		sortedList478.Add("Multikills/MultiKill_7", loadingAsset);
		SortedList<string, LoadingAsset> sortedList479 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Multikills/MultiKill_8",
			displayName = "Massacre",
			size = 228931,
			assetVersion = 1
		};
		sortedList479.Add("Multikills/MultiKill_8", loadingAsset);
		SortedList<string, LoadingAsset> sortedList480 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Multikills/MultiKill_9",
			displayName = "GODLIKE",
			size = 229432,
			assetVersion = 1
		};
		sortedList480.Add("Multikills/MultiKill_9", loadingAsset);
		SortedList<string, LoadingAsset> sortedList481 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "Multikills/Voice",
			displayName = "MultiKill Voice",
			size = 1181711,
			assetVersion = 1
		};
		sortedList481.Add("Multikills/Voice", loadingAsset);
		SortedList<string, LoadingAsset> sortedList482 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SocialInteraction/Social_0",
			displayName = "Social Interaction 0",
			size = 306737,
			assetVersion = 1
		};
		sortedList482.Add("SocialInteraction/Social_0", loadingAsset);
		SortedList<string, LoadingAsset> sortedList483 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SocialInteraction/Social_1",
			displayName = "Social Interaction 1",
			size = 309869,
			assetVersion = 1
		};
		sortedList483.Add("SocialInteraction/Social_1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList484 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SocialInteraction/Social_2",
			displayName = "Social Interaction 2",
			size = 305889,
			assetVersion = 1
		};
		sortedList484.Add("SocialInteraction/Social_2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList485 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SocialInteraction/Social_3",
			displayName = "Social Interaction 3",
			size = 311687,
			assetVersion = 1
		};
		sortedList485.Add("SocialInteraction/Social_3", loadingAsset);
		SortedList<string, LoadingAsset> sortedList486 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SocialInteraction/Social_4",
			displayName = "Social Interaction 4",
			size = 315340,
			assetVersion = 1
		};
		sortedList486.Add("SocialInteraction/Social_4", loadingAsset);
		SortedList<string, LoadingAsset> sortedList487 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SocialInteraction/Social_5",
			displayName = "Social Interaction 5",
			size = 308463,
			assetVersion = 1
		};
		sortedList487.Add("SocialInteraction/Social_5", loadingAsset);
		SortedList<string, LoadingAsset> sortedList488 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SocialInteraction/Social_6",
			displayName = "Social Interaction 6",
			size = 318295,
			assetVersion = 1
		};
		sortedList488.Add("SocialInteraction/Social_6", loadingAsset);
		SortedList<string, LoadingAsset> sortedList489 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SocialInteraction/Social_7",
			displayName = "Social Interaction 7",
			size = 316884,
			assetVersion = 1
		};
		sortedList489.Add("SocialInteraction/Social_7", loadingAsset);
		SortedList<string, LoadingAsset> sortedList490 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SocialInteraction/Social_8",
			displayName = "Social Interaction 8",
			size = 311570,
			assetVersion = 1
		};
		sortedList490.Add("SocialInteraction/Social_8", loadingAsset);
		SortedList<string, LoadingAsset> sortedList491 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SocialInteraction/Social_9",
			displayName = "Social Interaction 9",
			size = 304521,
			assetVersion = 1
		};
		sortedList491.Add("SocialInteraction/Social_9", loadingAsset);
		SortedList<string, LoadingAsset> sortedList492 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SocialInteraction/Social_10",
			displayName = "Social Interaction 10",
			size = 304683,
			assetVersion = 1
		};
		sortedList492.Add("SocialInteraction/Social_10", loadingAsset);
		SortedList<string, LoadingAsset> sortedList493 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/Andromeda",
			displayName = "Andromeda Scene",
			isLevel = true,
			size = 4533345,
			assetVersion = 1
		};
		sortedList493.Add("Scenes/Andromeda", loadingAsset);
		SortedList<string, LoadingAsset> sortedList494 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/CanisMajor",
			displayName = "CanisMajor Scene",
			isLevel = true,
			size = 5228548,
			assetVersion = 1
		};
		sortedList494.Add("Scenes/CanisMajor", loadingAsset);
		SortedList<string, LoadingAsset> sortedList495 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/CanisMinor",
			displayName = "CanisMinor Scene",
			isLevel = true,
			size = 3203248,
			assetVersion = 1
		};
		sortedList495.Add("Scenes/CanisMinor", loadingAsset);
		SortedList<string, LoadingAsset> sortedList496 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/Cassiopeia",
			displayName = "Cassiopeia Scene",
			isLevel = true,
			size = 6499903,
			assetVersion = 1
		};
		sortedList496.Add("Scenes/Cassiopeia", loadingAsset);
		SortedList<string, LoadingAsset> sortedList497 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/Centaurus",
			displayName = "Centaurus Scene",
			isLevel = true,
			size = 4550763,
			assetVersion = 1
		};
		sortedList497.Add("Scenes/Centaurus", loadingAsset);
		SortedList<string, LoadingAsset> sortedList498 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/Cepheus",
			displayName = "Cepheus Scene",
			isLevel = true,
			size = 4111502,
			assetVersion = 1
		};
		sortedList498.Add("Scenes/Cepheus", loadingAsset);
		SortedList<string, LoadingAsset> sortedList499 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/Hydra",
			displayName = "Hydra Scene",
			isLevel = true,
			size = 7561084,
			assetVersion = 1
		};
		sortedList499.Add("Scenes/Hydra", loadingAsset);
		SortedList<string, LoadingAsset> sortedList500 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/Mensa",
			displayName = "Mensa Scene",
			isLevel = true,
			size = 5358638,
			assetVersion = 1
		};
		sortedList500.Add("Scenes/Mensa", loadingAsset);
		SortedList<string, LoadingAsset> sortedList501 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/InBase",
			displayName = "InBase Scene",
			isLevel = true,
			size = 5083603,
			assetVersion = 1
		};
		sortedList501.Add("Scenes/InBase", loadingAsset);
		SortedList<string, LoadingAsset> sortedList502 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/InstanceAlphaCentauri",
			displayName = "InstanceAlphaCentauri Scene",
			isLevel = true,
			size = 5835138,
			assetVersion = 1
		};
		sortedList502.Add("Scenes/InstanceAlphaCentauri", loadingAsset);
		SortedList<string, LoadingAsset> sortedList503 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/InstanceBellatrixHideout",
			displayName = "InstanceBellatrixHideout Scene",
			isLevel = true,
			size = 7883395,
			assetVersion = 1
		};
		sortedList503.Add("Scenes/InstanceBellatrixHideout", loadingAsset);
		SortedList<string, LoadingAsset> sortedList504 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/InstanceDorado",
			displayName = "InstanceDorado Scene",
			isLevel = true,
			size = 5737135,
			assetVersion = 1
		};
		sortedList504.Add("Scenes/InstanceDorado", loadingAsset);
		SortedList<string, LoadingAsset> sortedList505 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/InstanceDraco",
			displayName = "InstanceDraco Scene",
			isLevel = true,
			size = 7110261,
			assetVersion = 1
		};
		sortedList505.Add("Scenes/InstanceDraco", loadingAsset);
		SortedList<string, LoadingAsset> sortedList506 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/InstanceHydraPrime",
			displayName = "InstanceHydraPrime Scene",
			isLevel = true,
			size = 4660484,
			assetVersion = 1
		};
		sortedList506.Add("Scenes/InstanceHydraPrime", loadingAsset);
		SortedList<string, LoadingAsset> sortedList507 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/InstanceLynxSupercluster",
			displayName = "InstanceLynxSupercluster Scene",
			isLevel = true,
			size = 3379899,
			assetVersion = 1
		};
		sortedList507.Add("Scenes/InstanceLynxSupercluster", loadingAsset);
		SortedList<string, LoadingAsset> sortedList508 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/InstanceOwlNebula",
			displayName = "InstanceOwlNebula Scene",
			isLevel = true,
			size = 4184241,
			assetVersion = 1
		};
		sortedList508.Add("Scenes/InstanceOwlNebula", loadingAsset);
		SortedList<string, LoadingAsset> sortedList509 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/InstanceProcyonAlpha",
			displayName = "InstanceProcyonAlpha Scene",
			isLevel = true,
			size = 2388665,
			assetVersion = 1
		};
		sortedList509.Add("Scenes/InstanceProcyonAlpha", loadingAsset);
		SortedList<string, LoadingAsset> sortedList510 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/InstanceSirius",
			displayName = "InstanceSirius Scene",
			isLevel = true,
			size = 9803214,
			assetVersion = 1
		};
		sortedList510.Add("Scenes/InstanceSirius", loadingAsset);
		SortedList<string, LoadingAsset> sortedList511 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/Lynx",
			displayName = "Lynx Scene",
			isLevel = true,
			size = 5411931,
			assetVersion = 1
		};
		sortedList511.Add("Scenes/Lynx", loadingAsset);
		SortedList<string, LoadingAsset> sortedList512 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/Orion",
			displayName = "Orion Scene",
			isLevel = true,
			size = 3443783,
			assetVersion = 1
		};
		sortedList512.Add("Scenes/Orion", loadingAsset);
		SortedList<string, LoadingAsset> sortedList513 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/Pegasus",
			displayName = "Pegasus Scene",
			isLevel = true,
			size = 6996356,
			assetVersion = 1
		};
		sortedList513.Add("Scenes/Pegasus", loadingAsset);
		SortedList<string, LoadingAsset> sortedList514 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/Perseus",
			displayName = "Perseus Scene",
			isLevel = true,
			size = 7039757,
			assetVersion = 1
		};
		sortedList514.Add("Scenes/Perseus", loadingAsset);
		SortedList<string, LoadingAsset> sortedList515 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/PvpArena1",
			displayName = "PvpArena1 Scene",
			isLevel = true,
			size = 4187364,
			assetVersion = 1
		};
		sortedList515.Add("Scenes/PvpArena1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList516 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/PvpArena2",
			displayName = "PvpArena2 Scene",
			isLevel = true,
			size = 5580971,
			assetVersion = 1
		};
		sortedList516.Add("Scenes/PvpArena2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList517 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/PvpArena3",
			displayName = "PvpArena3 Scene",
			isLevel = true,
			size = 4118948,
			assetVersion = 1
		};
		sortedList517.Add("Scenes/PvpArena3", loadingAsset);
		SortedList<string, LoadingAsset> sortedList518 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/Scorpio",
			displayName = "Scorpio Scene",
			isLevel = true,
			size = 5056305,
			assetVersion = 1
		};
		sortedList518.Add("Scenes/Scorpio", loadingAsset);
		SortedList<string, LoadingAsset> sortedList519 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/Taurus",
			displayName = "Taurus Scene",
			isLevel = true,
			size = 4898164,
			assetVersion = 1
		};
		sortedList519.Add("Scenes/Taurus", loadingAsset);
		SortedList<string, LoadingAsset> sortedList520 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/Tutorial",
			displayName = "Tutorial Scene",
			isLevel = true,
			size = 4062580,
			assetVersion = 3
		};
		sortedList520.Add("Scenes/Tutorial", loadingAsset);
		SortedList<string, LoadingAsset> sortedList521 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/UrsaMajor",
			displayName = "UrsaMajor Scene",
			isLevel = true,
			size = 7458464,
			assetVersion = 1
		};
		sortedList521.Add("Scenes/UrsaMajor", loadingAsset);
		SortedList<string, LoadingAsset> sortedList522 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/UrsaMinor",
			displayName = "UrsaMinor Scene",
			isLevel = true,
			size = 7122564,
			assetVersion = 1
		};
		sortedList522.Add("Scenes/UrsaMinor", loadingAsset);
		SortedList<string, LoadingAsset> sortedList523 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/UltralibriumGalaxyOne",
			displayName = "UltralibriumGalaxyOne Scene",
			isLevel = true,
			size = 3864910,
			assetVersion = 1
		};
		sortedList523.Add("Scenes/UltralibriumGalaxyOne", loadingAsset);
		SortedList<string, LoadingAsset> sortedList524 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/UltralibriumGalaxyTwo",
			displayName = "UltralibriumGalaxyTwo Scene",
			isLevel = true,
			size = 6806552,
			assetVersion = 1
		};
		sortedList524.Add("Scenes/UltralibriumGalaxyTwo", loadingAsset);
		SortedList<string, LoadingAsset> sortedList525 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/UltralibriumGalaxyThree",
			displayName = "UltralibriumGalaxyThree Scene",
			isLevel = true,
			size = 5414498,
			assetVersion = 1
		};
		sortedList525.Add("Scenes/UltralibriumGalaxyThree", loadingAsset);
		SortedList<string, LoadingAsset> sortedList526 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/InstanceInvictus",
			displayName = "InstanceInvictus Scene",
			isLevel = true,
			size = 5369972,
			assetVersion = 1
		};
		sortedList526.Add("Scenes/InstanceInvictus", loadingAsset);
		SortedList<string, LoadingAsset> sortedList527 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/InstanceVorax",
			displayName = "InstanceVorax Scene",
			isLevel = true,
			size = 8124928,
			assetVersion = 1
		};
		sortedList527.Add("Scenes/InstanceVorax", loadingAsset);
		SortedList<string, LoadingAsset> sortedList528 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/PvpArena4",
			displayName = "PvpArena4 Scene",
			isLevel = true,
			size = 3590518,
			assetVersion = 1
		};
		sortedList528.Add("Scenes/PvpArena4", loadingAsset);
		SortedList<string, LoadingAsset> sortedList529 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/PvpArena5",
			displayName = "PvpArena5 Scene",
			isLevel = true,
			size = 2557592,
			assetVersion = 1
		};
		sortedList529.Add("Scenes/PvpArena5", loadingAsset);
		SortedList<string, LoadingAsset> sortedList530 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/InstanceMagus",
			displayName = "Instance Magus Scene",
			isLevel = true,
			size = 6425594,
			assetVersion = 1
		};
		sortedList530.Add("Scenes/InstanceMagus", loadingAsset);
		SortedList<string, LoadingAsset> sortedList531 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/FactionGalaxy1",
			displayName = "Faction Galaxy 1 Scene",
			isLevel = true,
			size = 1825594,
			assetVersion = 100
		};
		sortedList531.Add("Scenes/FactionGalaxy1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList532 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/FactionGalaxy2",
			displayName = "Faction Galaxy 2 Scene",
			isLevel = true,
			size = 1825594,
			assetVersion = 100
		};
		sortedList532.Add("Scenes/FactionGalaxy2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList533 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/FactionGalaxy3",
			displayName = "Faction Galaxy 3 Scene",
			isLevel = true,
			size = 1825594,
			assetVersion = 100
		};
		sortedList533.Add("Scenes/FactionGalaxy3", loadingAsset);
		SortedList<string, LoadingAsset> sortedList534 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/FactionGalaxy4",
			displayName = "Faction Galaxy 4 Scene",
			isLevel = true,
			size = 1825594,
			assetVersion = 100
		};
		sortedList534.Add("Scenes/FactionGalaxy4", loadingAsset);
		SortedList<string, LoadingAsset> sortedList535 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/FactionGalaxy5",
			displayName = "Faction Galaxy 5 Scene",
			isLevel = true,
			size = 1825594,
			assetVersion = 100
		};
		sortedList535.Add("Scenes/FactionGalaxy5", loadingAsset);
		SortedList<string, LoadingAsset> sortedList536 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "MiningStation_pfb",
			displayName = "MiningStation",
			size = 2083640,
			assetVersion = 1
		};
		sortedList536.Add("MiningStation_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList537 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "MiningStationSphere_mat1",
			displayName = "MiningStationSphere_mat1",
			size = 2083640,
			assetVersion = 1
		};
		sortedList537.Add("MiningStationSphere_mat1", loadingAsset);
		SortedList<string, LoadingAsset> sortedList538 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "MiningStationSphere_mat2",
			displayName = "MiningStationSphere_mat2",
			size = 2083640,
			assetVersion = 1
		};
		sortedList538.Add("MiningStationSphere_mat2", loadingAsset);
		SortedList<string, LoadingAsset> sortedList539 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "MiningStationSphere_mat3",
			displayName = "MiningStationSphere_mat3",
			size = 2083640,
			assetVersion = 1
		};
		sortedList539.Add("MiningStationSphere_mat3", loadingAsset);
		SortedList<string, LoadingAsset> sortedList540 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "PvPDominationGui",
			displayName = "PvPDominationGui",
			size = 58566,
			assetVersion = 1
		};
		sortedList540.Add("PvPDominationGui", loadingAsset);
		SortedList<string, LoadingAsset> sortedList541 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "PvPDominationAnimation",
			displayName = "PvPDominationAnimation",
			size = 4149,
			assetVersion = 1
		};
		sortedList541.Add("PvPDominationAnimation", loadingAsset);
		SortedList<string, LoadingAsset> sortedList542 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "Pve/BlueHustlerBoss",
			displayName = "BlueHustlerBoss",
			size = 194026,
			assetVersion = 1
		};
		sortedList542.Add("Pve/BlueHustlerBoss", loadingAsset);
		SortedList<string, LoadingAsset> sortedList543 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Scene,
			assetName = "Scenes/PvPDomination",
			displayName = "PvPDomination Scene",
			isLevel = true,
			size = 6425594,
			assetVersion = 1
		};
		sortedList543.Add("Scenes/PvPDomination", loadingAsset);
		SortedList<string, LoadingAsset> sortedList544 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "AvailableRewardsWindow",
			displayName = "AvailableRewardsWindow",
			size = 4149,
			assetVersion = 1
		};
		sortedList544.Add("AvailableRewardsWindow", loadingAsset);
		SortedList<string, LoadingAsset> sortedList545 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "ShipStatsGui",
			displayName = "ShipStatsGui",
			size = 24000,
			assetVersion = 102
		};
		sortedList545.Add("ShipStatsGui", loadingAsset);
		SortedList<string, LoadingAsset> sortedList546 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "TargetingGui",
			displayName = "TargetingGui",
			size = 1880614,
			assetVersion = 1
		};
		sortedList546.Add("TargetingGui", loadingAsset);
		SortedList<string, LoadingAsset> sortedList547 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "FixedAvatars",
			displayName = "FixedAvatars",
			size = 24000,
			assetVersion = 1
		};
		sortedList547.Add("FixedAvatars", loadingAsset);
		SortedList<string, LoadingAsset> sortedList548 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "InstanceDifficulty",
			displayName = "InstanceDifficulty",
			size = 64714,
			assetVersion = 1
		};
		sortedList548.Add("InstanceDifficulty", loadingAsset);
		SortedList<string, LoadingAsset> sortedList549 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "PowerUpsWindow",
			displayName = "PowerUpsWindow",
			size = 64714,
			assetVersion = 1
		};
		sortedList549.Add("PowerUpsWindow", loadingAsset);
		SortedList<string, LoadingAsset> sortedList550 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "SendGiftsWindow",
			displayName = "SendGiftsWindow",
			size = 64714,
			assetVersion = 1
		};
		sortedList550.Add("SendGiftsWindow", loadingAsset);
		SortedList<string, LoadingAsset> sortedList551 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "PoiScreenWindow",
			displayName = "PoiScreenWindow",
			size = 64714,
			assetVersion = 103
		};
		sortedList551.Add("PoiScreenWindow", loadingAsset);
		SortedList<string, LoadingAsset> sortedList552 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "WarScreenWindow",
			displayName = "WarScreenWindow",
			size = 58714,
			assetVersion = 102
		};
		sortedList552.Add("WarScreenWindow", loadingAsset);
		SortedList<string, LoadingAsset> sortedList553 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "ActionButtons",
			displayName = "ActionButtons",
			size = 13714,
			assetVersion = 100
		};
		sortedList553.Add("ActionButtons", loadingAsset);
		SortedList<string, LoadingAsset> sortedList554 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			bundleType = BundleType.Set,
			assetName = "UniverseMapScreen",
			displayName = "UniverseMapScreen",
			size = 13714,
			assetVersion = 100
		};
		sortedList554.Add("UniverseMapScreen", loadingAsset);
		SortedList<string, LoadingAsset> sortedList555 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillSacrifice_pfb",
			displayName = "SkillSacrifice_pfb",
			size = 10825,
			assetVersion = 1
		};
		sortedList555.Add("SkillSacrifice_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList556 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillLifeStealTarget_pfb",
			displayName = "SkillLifeStealTarget_pfb",
			size = 10825,
			assetVersion = 1
		};
		sortedList556.Add("SkillLifeStealTarget_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList557 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillLifeStealCaster_pfb",
			displayName = "SkillLifeStealCaster_pfb",
			size = 10825,
			assetVersion = 1
		};
		sortedList557.Add("SkillLifeStealCaster_pfb", loadingAsset);
		SortedList<string, LoadingAsset> sortedList558 = playWebGame.allBundles;
		loadingAsset = new LoadingAsset()
		{
			assetName = "SkillDisarm_pfb",
			displayName = "SkillDisarm_pfb",
			size = 86825,
			assetVersion = 1
		};
		sortedList558.Add("SkillDisarm_pfb", loadingAsset);
	}

	public playWebGame()
	{
	}

	public static void AcquirePreparedScene()
	{
	}

	public static void BuildGuiSplash()
	{
		playWebGame.loadingAnimation = null;
		playWebGame.doNotUpdateLbls = false;
		if (playWebGame.isLoadProgressOnScreen && playWebGame.lastScreenWidth != Screen.get_width())
		{
			playWebGame.UpdateBackgroundImagePosition();
		}
		playWebGame.isLoadProgressOnScreen = true;
		GameObject gameObject = GameObject.Find("GlobalObject");
		if (gameObject != null)
		{
			playWebGame.gui = gameObject.GetComponent<GuiFramework>();
		}
		else
		{
			playWebGame.gui = AndromedaGui.gui;
		}
		if (playWebGame.gui == null)
		{
			return;
		}
		AndromedaGui.SetFonts(playWebGame.assets);
		playWebGame.wndSplash = new GuiWindow()
		{
			boundries = new Rect(0f, 0f, (float)Screen.get_width(), (float)Screen.get_height())
		};
		playWebGame.txBackground = new GuiTexture();
		playWebGame.txBackground.SetTexture(playWebGame.txBkg);
		Texture2D texture2D = new Texture2D(1, 1);
		texture2D.SetPixel(0, 0, Color.get_black());
		texture2D.Apply();
		playWebGame.txBackgroundBase = new GuiTexture();
		playWebGame.txBackgroundBase.SetTexture(texture2D);
		playWebGame.barProgress = new GuiBar((Texture2D)Resources.Load("LoadingGui/SmallBarLeft"), (Texture2D)Resources.Load("LoadingGui/SmallBarFill"), (Texture2D)Resources.Load("LoadingGui/SmallBarRight"), (Texture2D)Resources.Load("LoadingGui/SmallBarMiddle"))
		{
			Width = 400f,
			Y = 200f,
			maximum = (float)playWebGame.assets.size,
			current = playWebGame.assets.Progress
		};
		playWebGame.lblLoading = new GuiLabel()
		{
			text = StaticData.Translate("key_loading_loading_assets").ToUpper(),
			FontSize = 16
		};
		playWebGame.lblLoading.boundries.set_width(400f);
		playWebGame.lblLoading.boundries.set_height(20f);
		playWebGame.lblLoading.Y = 160f;
		playWebGame.lblLoading.Alignment = 1;
		playWebGame.lblPercent = new GuiLabel()
		{
			Y = 180f
		};
		playWebGame.lblPercent.boundries.set_width(400f);
		playWebGame.lblPercent.boundries.set_height(20f);
		playWebGame.lblPercent.FontSize = 16;
		playWebGame.lblPercent.Alignment = 1;
		playWebGame.lblCurrentAsset = new GuiLabel()
		{
			Y = 230f
		};
		playWebGame.lblCurrentAsset.boundries.set_width(400f);
		playWebGame.lblCurrentAsset.boundries.set_height(20f);
		playWebGame.lblCurrentAsset.FontSize = 13;
		playWebGame.lblCurrentAsset.TextColor = new Color(0.36863f, 0.63922f, 0.81967f);
		playWebGame.lblCurrentAsset.Alignment = 1;
		playWebGame.lblNote = new GuiLabel()
		{
			text = StaticData.Translate("key_loading_firs_time_note"),
			Y = 250f
		};
		playWebGame.lblNote.boundries.set_width(600f);
		playWebGame.lblNote.boundries.set_height(20f);
		playWebGame.lblNote.FontSize = 12;
		playWebGame.lblNote.Font = GuiLabel.FontBold;
		playWebGame.lblNote.Alignment = 1;
		playWebGame.lblError = new GuiLabel()
		{
			Y = 300f
		};
		playWebGame.lblError.boundries.set_width(400f);
		playWebGame.lblError.boundries.set_height(300f);
		playWebGame.lblError.FontSize = 12;
		playWebGame.lblError.TextColor = Color.get_red();
		playWebGame.lblCurrentAsset.Alignment = 1;
		playWebGame.tipsIndex = (byte)Random.Range(1, 6);
		playWebGame.tipsBackground = new GuiTexture();
		playWebGame.tipsBackground.SetTexture((Texture2D)Resources.Load("LoadingGui/tips_frame"));
		playWebGame.tipsBackground.Y = 270f;
		playWebGame.tipsPagingDotOne = new GuiTexture();
		playWebGame.tipsPagingDotOne.SetTexture((Texture2D)Resources.Load("LoadingGui/paging_active"));
		playWebGame.tipsPagingDotOne.Y = 300f;
		playWebGame.tipsPagingDotTwo = new GuiTexture();
		playWebGame.tipsPagingDotTwo.SetTexture((Texture2D)Resources.Load("LoadingGui/paging_normal"));
		playWebGame.tipsPagingDotTwo.Y = 300f;
		playWebGame.tipsPagingDotThree = new GuiTexture();
		playWebGame.tipsPagingDotThree.SetTexture((Texture2D)Resources.Load("LoadingGui/paging_normal"));
		playWebGame.tipsPagingDotThree.Y = 300f;
		playWebGame.tipsPagingDotFour = new GuiTexture();
		playWebGame.tipsPagingDotFour.SetTexture((Texture2D)Resources.Load("LoadingGui/paging_normal"));
		playWebGame.tipsPagingDotFour.Y = 300f;
		playWebGame.tipsPagingDotFive = new GuiTexture();
		playWebGame.tipsPagingDotFive.SetTexture((Texture2D)Resources.Load("LoadingGui/paging_normal"));
		playWebGame.tipsPagingDotFive.Y = 300f;
		playWebGame.tipsPagingDotSix = new GuiTexture();
		playWebGame.tipsPagingDotSix.SetTexture((Texture2D)Resources.Load("LoadingGui/paging_normal"));
		playWebGame.tipsPagingDotSix.Y = 300f;
		playWebGame.btnLeft = new GuiButtonFixed();
		playWebGame.btnLeft.SetTextureNormal((Texture2D)Resources.Load("LoadingGui/nav_leftNml"), true);
		playWebGame.btnLeft.SetTextureHover((Texture2D)Resources.Load("LoadingGui/nav_leftHvr"));
		playWebGame.btnLeft.Y = 320f;
		playWebGame.btnLeft.Caption = string.Empty;
		playWebGame.btnLeft.Clicked = new Action<EventHandlerParam>(null, playWebGame.OnBtnLeftClicked);
		playWebGame.btnLeft.isMuted = true;
		playWebGame.btnRight = new GuiButtonFixed();
		playWebGame.btnRight.SetTextureNormal((Texture2D)Resources.Load("LoadingGui/nav_rightNml"), true);
		playWebGame.btnRight.SetTextureHover((Texture2D)Resources.Load("LoadingGui/nav_rightHvr"));
		playWebGame.btnRight.Y = 320f;
		playWebGame.btnRight.Caption = string.Empty;
		playWebGame.btnRight.Clicked = new Action<EventHandlerParam>(null, playWebGame.OnBtnRightClicked);
		playWebGame.btnRight.isMuted = true;
		playWebGame.tipsImage = new GuiTexture();
		playWebGame.tipsImage.SetTexture((Texture2D)Resources.Load(string.Concat("LoadingGui/", string.Format("tip_image_{0}", playWebGame.tipsIndex))));
		playWebGame.tipsImage.Y = 270f;
		playWebGame.tipsLblOne = new GuiLabel()
		{
			boundries = new Rect(0f, 320f, 240f, 40f),
			FontSize = 12,
			text = string.Empty,
			TextColor = GuiNewStyleBar.blueColor,
			Font = GuiLabel.FontBold,
			Alignment = 4
		};
		playWebGame.tipsLblTwo = new GuiLabel()
		{
			boundries = new Rect(0f, 500f, 95f, 40f),
			FontSize = 12,
			text = string.Empty
		};
		playWebGame.tipsLblThree = new GuiLabel()
		{
			boundries = new Rect(0f, 500f, 130f, 40f),
			FontSize = 12,
			text = string.Empty
		};
		playWebGame.tipsLblFour = new GuiLabel()
		{
			boundries = new Rect(0f, 510f, 125f, 40f),
			FontSize = 12,
			text = string.Empty
		};
		playWebGame.UpdateTip();
		playWebGame.UpdateBackgroundImagePosition();
		playWebGame.wndSplash.AddGuiElement(playWebGame.txBackgroundBase);
		playWebGame.wndSplash.AddGuiElement(playWebGame.txBackground);
		playWebGame.wndSplash.AddGuiElement(playWebGame.barProgress);
		playWebGame.wndSplash.AddGuiElement(playWebGame.lblLoading);
		playWebGame.wndSplash.AddGuiElement(playWebGame.lblPercent);
		playWebGame.wndSplash.AddGuiElement(playWebGame.lblCurrentAsset);
		playWebGame.wndSplash.AddGuiElement(playWebGame.lblNote);
		playWebGame.wndSplash.AddGuiElement(playWebGame.lblError);
		playWebGame.wndSplash.AddGuiElement(playWebGame.tipsBackground);
		playWebGame.wndSplash.AddGuiElement(playWebGame.tipsPagingDotOne);
		playWebGame.wndSplash.AddGuiElement(playWebGame.tipsPagingDotTwo);
		playWebGame.wndSplash.AddGuiElement(playWebGame.tipsPagingDotThree);
		playWebGame.wndSplash.AddGuiElement(playWebGame.tipsPagingDotFour);
		playWebGame.wndSplash.AddGuiElement(playWebGame.tipsPagingDotFive);
		playWebGame.wndSplash.AddGuiElement(playWebGame.tipsPagingDotSix);
		playWebGame.wndSplash.AddGuiElement(playWebGame.btnLeft);
		playWebGame.wndSplash.AddGuiElement(playWebGame.btnRight);
		playWebGame.wndSplash.AddGuiElement(playWebGame.tipsImage);
		playWebGame.wndSplash.AddGuiElement(playWebGame.tipsLblOne);
		playWebGame.wndSplash.AddGuiElement(playWebGame.tipsLblTwo);
		playWebGame.wndSplash.AddGuiElement(playWebGame.tipsLblThree);
		playWebGame.wndSplash.AddGuiElement(playWebGame.tipsLblFour);
		playWebGame.wndSplash.isHidden = false;
		playWebGame.wndSplash.zOrder = 255;
		playWebGame.wndSplash.preDrawHandler = new Action<object>(null, playWebGame.UpdatePosition);
		playWebGame.gui.AddWindow(playWebGame.wndSplash);
	}

	public void CallServerApi()
	{
		playWebGame.message = "Loading servers...";
		try
		{
			base.StartCoroutine(this.CallServerApiCoroutine());
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			playWebGame.message = "Error loading servers";
			Debug.LogError(exception.get_Message());
		}
	}

	[DebuggerHidden]
	private IEnumerator CallServerApiCoroutine()
	{
		playWebGame.<CallServerApiCoroutine>c__Iterator15 variable = null;
		return variable;
	}

	public static Texture2D GetAvatarOrStartIt(string urlNamePart, Action<AvatarJob> callback)
	{
		return playWebGame.GetAvatarOrStartIt(urlNamePart, callback, null);
	}

	public static Texture2D GetAvatarOrStartIt(string urlNamePart, Action<AvatarJob> callback, object token)
	{
		playWebGame.<GetAvatarOrStartIt>c__AnonStoreyAF variable = null;
		if (urlNamePart.Contains("FixedAvatar_"))
		{
			return (Texture2D)playWebGame.assets.GetFromStaticSet("FixedAvatars", urlNamePart);
		}
		Texture2D texture2D = null;
		if (playWebGame.loadedAvatars.TryGetValue(urlNamePart, ref texture2D))
		{
			return texture2D;
		}
		List<AvatarJob> list = null;
		AvatarJob avatarJob = new AvatarJob()
		{
			key = urlNamePart,
			job = new WWW(string.Format(playWebGame.authorization.url_avatar, urlNamePart)),
			token = token,
			callback = callback
		};
		AvatarJob avatarJob1 = avatarJob;
		if (!playWebGame.loadingAvatars.TryGetValue(urlNamePart, ref list))
		{
			SortedList<string, List<AvatarJob>> sortedList = playWebGame.loadingAvatars;
			string str = urlNamePart;
			List<AvatarJob> list1 = new List<AvatarJob>();
			list1.Add(avatarJob1);
			sortedList.Add(str, list1);
		}
		else if (Enumerable.FirstOrDefault<AvatarJob>(Enumerable.Where<AvatarJob>(list, new Func<AvatarJob, bool>(variable, (AvatarJob j) => (j.key != this.urlNamePart ? false : (object)j.token == (object)this.token)))) == null)
		{
			list.Add(avatarJob1);
		}
		return null;
	}

	public static void HandleLog(string logString, string stackTrace, LogType type)
	{
		if (playWebGame.feedbackLogOutput != null)
		{
			playWebGame.feedbackLogOutput.AppendLine(logString);
		}
		else
		{
			playWebGame.feedbackLogOutput = new StringBuilder(logString);
		}
		if (playWebGame.feedbackLogOutput.get_Length() > playWebGame.feedbackLogLenght)
		{
			playWebGame.feedbackLogOutput.Remove(0, playWebGame.feedbackLogOutput.get_Length() - playWebGame.feedbackLogLenght);
		}
	}

	public static void InitMixPanel()
	{
	}

	public void JSSetAssetsUrl(string msg)
	{
		try
		{
			playWebGame.ASSET_URL = msg;
		}
		catch (Exception exception)
		{
			Debug.LogError(exception);
		}
	}

	public void JSSetGalaxyId(string msg)
	{
		try
		{
			playWebGame.authorization.galaxyId = short.Parse(msg);
		}
		catch (Exception exception)
		{
			Debug.LogError(exception);
		}
	}

	public void JSSetGameType(string msg)
	{
		try
		{
			playWebGame.GAME_TYPE = msg;
		}
		catch (Exception exception)
		{
			Debug.LogError(exception);
		}
	}

	public void JSSetId(string msg)
	{
		try
		{
			playWebGame.authorization.id = long.Parse(msg);
		}
		catch (Exception exception)
		{
			Debug.LogError(exception);
		}
	}

	public void JSSetIsInBase(string msg)
	{
		try
		{
			if (msg == "1" || msg == "true" || msg == "True")
			{
				playWebGame.authorization.isInBase = true;
			}
			else
			{
				playWebGame.authorization.isInBase = false;
			}
		}
		catch (Exception exception)
		{
			Debug.LogError(exception);
		}
	}

	public void JSSetLoginServerIP(string msg)
	{
		try
		{
			playWebGame.LOGIN_SERVER_IP = msg;
		}
		catch (Exception exception)
		{
			Debug.LogError(exception);
		}
	}

	public void JSSetMadmooId(string msg)
	{
		try
		{
			playWebGame.authorization.madmooId = long.Parse(msg);
		}
		catch (Exception exception)
		{
			Debug.LogError(exception);
		}
	}

	public void JSSetPaymentsPromotion(string msg)
	{
		try
		{
			playWebGame.authorization.payments_promotion = msg == "1";
		}
		catch (Exception exception)
		{
			Debug.LogError(exception);
		}
	}

	public void JSSetPlayerId(string msg)
	{
		try
		{
			playWebGame.authorization.loginId = int.Parse(msg);
		}
		catch (Exception exception)
		{
			Debug.LogError(exception);
		}
	}

	public void JSSetSceneName(string msg)
	{
		try
		{
			playWebGame.authorization.sceneName = (!playWebGame.authorization.isInBase ? msg : "InBase");
		}
		catch (Exception exception)
		{
			Debug.LogError(exception);
		}
	}

	public void JSSetServerIp(string msg)
	{
		try
		{
			playWebGame.authorization.universeServerIp = msg;
		}
		catch (Exception exception)
		{
			Debug.LogError(exception);
		}
	}

	public void JSSetServerPort(string msg)
	{
		try
		{
			playWebGame.authorization.galaxyServerPort = ushort.Parse(msg);
		}
		catch (Exception exception)
		{
			Debug.LogError(exception);
		}
	}

	public void JSSetURLAvatar(string msg)
	{
		try
		{
			playWebGame.authorization.url_avatar = msg;
		}
		catch (Exception exception)
		{
			Debug.LogError(exception);
			playWebGame.authorization.url_avatar = "http://game.andromeda5.com/avatars/int/world_1/no_avatar.jpg";
		}
	}

	public void JSSetURLFacebook(string msg)
	{
		try
		{
			playWebGame.authorization.url_fb = msg;
		}
		catch (Exception exception)
		{
			Debug.LogError(exception);
		}
	}

	public void JSSetURLFeedback(string msg)
	{
		try
		{
			playWebGame.authorization.url_feedback = msg;
		}
		catch (Exception exception)
		{
			Debug.LogError(exception);
		}
	}

	public void JSSetURLLogout(string msg)
	{
		try
		{
			playWebGame.authorization.url_logout = msg;
		}
		catch (Exception exception)
		{
			Debug.LogError(exception);
		}
	}

	public void JSSetURLPayments(string msg)
	{
		try
		{
			playWebGame.authorization.url_payments = msg;
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			Debug.LogError(exception);
			playWebGame.message = string.Concat(playWebGame.message, exception.get_Message());
		}
	}

	public void JSSetURLRecruit(string msg)
	{
		try
		{
			playWebGame.authorization.url_recruit = msg;
		}
		catch (Exception exception)
		{
			Debug.LogError(exception);
		}
	}

	public void JSStartGame(string msg)
	{
		playWebGame.StartDownload = true;
		playWebGame.udp.OnConnectionEstablished = new Action(null, playWebGame.StartReceiveStaticData);
		playWebGame.udp.StartReceiveUdp(new Action<Exception, PlayerData>(null, playWebGame.OnErrorDisconnected));
	}

	public static void LoadScene(string sceneName)
	{
		playWebGame.BuildGuiSplash();
		playWebGame.preparedSceneName = sceneName;
		playWebGame.PrepareScene2(sceneName);
		playWebGame.isPreparingScene = true;
		playWebGame.isPreparingSceneBundlesLoaded = false;
		playWebGame.isPreparingSceneReadyForAsyncLoad = false;
		NotificationManager.Pause();
	}

	public static void LogMixPanel(MixPanelEvents eventName, Dictionary<string, object> CustomProperties = null)
	{
	}

	private static void ManageCaching()
	{
		if (playWebGame.currentLoadingBundle >= playWebGame.allBundles.get_Count())
		{
			return;
		}
		LoadingAsset item = playWebGame.allBundles.get_Values().get_Item(playWebGame.currentLoadingBundle);
		switch (item.state)
		{
			case LoadAssetState.NotStarted:
			{
				for (int i = playWebGame.currentLoadingBundle; i < Math.Min(playWebGame.allBundles.get_Count(), playWebGame.currentLoadingBundle + 4); i++)
				{
					if (playWebGame.allBundles.get_Values().get_Item(i).state == LoadAssetState.NotStarted)
					{
						playWebGame.allBundles.get_Values().get_Item(i).www = WWW.LoadFromCacheOrDownload(string.Concat(playWebGame.ASSET_URL, "/All/", playWebGame.allBundles.get_Values().get_Item(i).assetName, ".unity3d"), playWebGame.allBundles.get_Values().get_Item(i).assetVersion);
						playWebGame.allBundles.get_Values().get_Item(i).state = LoadAssetState.Loading;
					}
				}
				break;
			}
			case LoadAssetState.Loading:
			{
				item.ManageCurrentLoad();
				if (item.state == LoadAssetState.Loading)
				{
					return;
				}
				break;
			}
			case LoadAssetState.Error:
			{
				Debug.Log(string.Concat("LoadAssetState.Error: ", item.assetName));
				break;
			}
			case LoadAssetState.Loaded:
			{
				playWebGame.currentLoadingBundle = playWebGame.currentLoadingBundle + 1;
				if (playWebGame.currentLoadingBundle >= playWebGame.allBundles.get_Count())
				{
					playWebGame.isAllBundlesLoaded = true;
				}
				break;
			}
		}
	}

	private static void ManageSet()
	{
		if (playWebGame.currentlyLoadedSet.isLoaded)
		{
			playWebGame.isPreparingSceneBundlesLoaded = true;
		}
		else
		{
			while (playWebGame.currentlyLoadedSet.currentBundle < (int)playWebGame.currentlyLoadedSet.bundles.Length)
			{
				LoadingAsset loadingAsset = playWebGame.currentlyLoadedSet.bundles[playWebGame.currentlyLoadedSet.currentBundle];
				switch (loadingAsset.state)
				{
					case LoadAssetState.NotStarted:
					{
						loadingAsset.www = WWW.LoadFromCacheOrDownload(string.Concat(playWebGame.ASSET_URL, "/All/", loadingAsset.assetName, ".unity3d"), loadingAsset.assetVersion);
						loadingAsset.state = LoadAssetState.Loading;
						continue;
					}
					case LoadAssetState.Loading:
					{
						loadingAsset.ManageCurrentLoad();
						if (loadingAsset.state == LoadAssetState.Loading)
						{
							return;
						}
						if (loadingAsset.state == LoadAssetState.Error)
						{
							Debug.Log(string.Concat("Problem: ", loadingAsset.www.get_url(), " ", loadingAsset.www.get_error()));
							return;
						}
						continue;
					}
					case LoadAssetState.Error:
					{
						Debug.LogError(string.Format("Error loading bundle {0} : {1}", loadingAsset.www.get_url(), loadingAsset.www.get_error()));
						return;
					}
					case LoadAssetState.Loaded:
					{
						for (int i = playWebGame.currentlyLoadedSet.currentBundle + 1; i < Math.Min((int)playWebGame.currentlyLoadedSet.bundles.Length, i + 20); i++)
						{
							if (playWebGame.currentlyLoadedSet.bundles[i].state == LoadAssetState.NotStarted)
							{
								playWebGame.currentlyLoadedSet.bundles[i].www = WWW.LoadFromCacheOrDownload(string.Concat(playWebGame.ASSET_URL, "/All/", playWebGame.currentlyLoadedSet.bundles[i].assetName, ".unity3d"), playWebGame.currentlyLoadedSet.bundles[i].assetVersion);
								playWebGame.currentlyLoadedSet.bundles[i].state = LoadAssetState.Loading;
							}
						}
						SceneBundleSet sceneBundleSet = playWebGame.currentlyLoadedSet;
						sceneBundleSet.currentBundle = sceneBundleSet.currentBundle + 1;
						if (playWebGame.currentlyLoadedSet.currentBundle >= (int)playWebGame.currentlyLoadedSet.bundles.Length)
						{
							playWebGame.currentlyLoadedSet.isLoaded = true;
							playWebGame.isPreparingSceneBundlesLoaded = true;
						}
						continue;
					}
					default:
					{
						continue;
					}
				}
			}
		}
	}

	public static string Md5Sum(string strToEncrypt)
	{
		byte[] bytes = (new UTF8Encoding()).GetBytes(strToEncrypt);
		byte[] numArray = (new MD5CryptoServiceProvider()).ComputeHash(bytes);
		string empty = string.Empty;
		for (int i = 0; i < (int)numArray.Length; i++)
		{
			empty = string.Concat(empty, Convert.ToString(numArray[i], 16).PadLeft(2, '0'));
		}
		return empty.PadLeft(32, '0');
	}

	private void OnApplicationQuit()
	{
		try
		{
			playWebGame.udp.StopReceiveUdp();
		}
		catch (Exception exception)
		{
		}
		playWebGame.LogMixPanel(MixPanelEvents.ExitGame, null);
	}

	private static void OnBtnLeftClicked(object prm)
	{
		playWebGame.tipsIndex = (byte)(playWebGame.tipsIndex - 1);
		if (playWebGame.tipsIndex < 1)
		{
			playWebGame.tipsIndex = 6;
		}
		playWebGame.UpdateTip();
	}

	private static void OnBtnRightClicked(object prm)
	{
		playWebGame.tipsIndex = (byte)(playWebGame.tipsIndex + 1);
		if (playWebGame.tipsIndex > 6)
		{
			playWebGame.tipsIndex = 1;
		}
		playWebGame.UpdateTip();
	}

	public static void OnErrorDisconnected(Exception ex, PlayerData pd)
	{
		try
		{
			playWebGame._udp.StopReceiveUdp();
		}
		catch (Exception exception)
		{
		}
		Debug.Log("OnErrorDisconnected");
		Debug.LogWarning(ex.ToString());
		playWebGame.LoadScene("Login");
	}

	private void OnGUI()
	{
		switch (playWebGame.loadLangState)
		{
			case 0:
			{
				return;
			}
			case 1:
			{
				return;
			}
			case 2:
			{
				return;
			}
			case 3:
			{
				Debug.Log("Could not connect to game server! Try again later!");
				return;
			}
			default:
			{
				return;
			}
		}
	}

	private static void PrepareScene2(string sceneName)
	{
		if (!playWebGame.loadResFromAssets)
		{
			Application.LoadLevel(sceneName);
		}
		else
		{
			SceneBundleSet sceneBundleSet = null;
			if (sceneName == "Tutorial")
			{
				object[] objArray = new object[] { playWebGame.setTutorial, default(object) };
				objArray[1] = new string[] { "FrameworkGUI" };
				sceneBundleSet = new SceneBundleSet(sceneName, objArray);
			}
			else if (sceneName == "InBase" || sceneName == "InBase_iPad")
			{
				sceneBundleSet = new SceneBundleSet(sceneName, new object[] { playWebGame.setGui, playWebGame.setInBase });
			}
			else if (sceneName != "Login")
			{
				sceneBundleSet = (!playWebGame.allScenes.ContainsKey(sceneName) ? new SceneBundleSet(sceneName, new object[] { playWebGame.setGui, playWebGame.setInSpace }) : new SceneBundleSet(sceneName, new object[] { playWebGame.setGui, playWebGame.setInSpace, playWebGame.allScenes.get_Item(sceneName) }));
			}
			else
			{
				sceneBundleSet = new SceneBundleSet(sceneName, new object[] { new string[] { "GUI", "NewGUI", "LoginGui", "LoadingAnimation", "FrameworkGUI" }, new string[] { "Sounds" } });
			}
			List<LoadingAsset> list = new List<LoadingAsset>();
			list.AddRange(sceneBundleSet.bundles);
			playWebGame.currentJobs = list;
			playWebGame.currentlyLoadedSet = sceneBundleSet;
			sceneBundleSet.currentBundle = 0;
		}
	}

	public static void ResetLoadProgress()
	{
		Debug.Log(string.Concat("ResetLoadProgress ", Application.get_loadedLevelName()));
		playWebGame.isLoadSceneStarted = false;
		playWebGame.isPreparingScene = false;
		playWebGame.isPreparingSceneBundlesLoaded = false;
		playWebGame.isLoadProgressOnScreen = false;
		playWebGame.isPreparingSceneReadyForAsyncLoad = false;
		playWebGame.currentJobs = null;
		playWebGame.gui = null;
	}

	public static bool RunSmallLangPackLoader()
	{
		// 
		// Current member / type: System.Boolean playWebGame::RunSmallLangPackLoader()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Boolean RunSmallLangPackLoader()
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..( , Int32 , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:строка 78
		//    в ..( , Int32& , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:строка 33
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 57
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 481
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 83
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 33
		//    в ..(MethodBody ,  , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 88
		//    в ..(MethodBody , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 70
		//    в Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 95
		//    в Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 58
		//    в ..(ILanguage , MethodDefinition ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:строка 117
		// 
		// mailto: JustDecompilePublicFeedback@telerik.com

	}

	private void ServerOrderedExit()
	{
		playWebGame.message = string.Concat(playWebGame.message, StaticData.Translate("key_loading_server_order_exit"));
	}

	private void ServersLoaded()
	{
		playWebGame.loadLangState = 0;
		playWebGame.RunSmallLangPackLoader();
		playWebGame.currentJobSizeBytes = 1;
		playWebGame.currentJobLoadedBytes = 1;
		playWebGame.currentJobProgress = 0f;
		playWebGame.txBkg = (Texture2D)Resources.Load("splashBkg");
		if (playWebGame.isWebPlayer)
		{
			Caching.Authorize("Andromeda5", "http://andromeda5.com/", (long)-1073741824, "578d1c4b2b5a8e66828b9f46da1647070b3839d201b671111f655c0ef57d0ced5f1a3a7c06d494308904404213bdc1b866e7de2a673fb77d9b402a802b49e41a994f3fb752f49d1585ffc2e687b20b8ee997b09966be2ffcbefc89de914139894badcf99266d71cb8508c8f00f024fd44bfed2c9d3ae3917dab629ac9cd35b64");
		}
		if (!Caching.IsVersionCached(string.Concat(playWebGame.ASSET_URL, "/All/Migrate4.unity3d"), 1))
		{
			Debug.LogWarning("Clearing cache!");
			Caching.CleanCache();
		}
		playWebGame.isSceneReady = false;
		playWebGame.assets = new AssetManager();
		playWebGame.currentAssetLoad = 0;
		GameObjectPhysics.logMethod = new Action<string>(null, Debug.Log);
		playWebGame.message = StaticData.Translate("key_loading_loading_done");
		playWebGame.BuildGuiSplash();
		playWebGame.isLoadProgressOnScreen = true;
		if (playWebGame.isWebPlayer)
		{
			return;
		}
		if (LoginScript.serverVersion != 0 && LoginScript.serverVersion != 164)
		{
			playWebGame.LoadScene("login");
			return;
		}
		if (PlayerPrefs.GetInt("AutoLogin") != 1)
		{
			playWebGame.LoadScene("login");
		}
		else
		{
			playWebGame.LoadScene("Login_Mobile");
		}
	}

	public static void SetServer(int index)
	{
		playWebGame.SetServer(playWebGame.GameServers[index]);
	}

	public static void SetServer(GameServer choosedServer)
	{
		try
		{
			playWebGame.LOGIN_SERVER_IP = choosedServer.login_server;
			playWebGame.ASSET_URL = choosedServer.asset_server;
			Debug.Log(string.Concat("LoginServer: ", playWebGame.LOGIN_SERVER_IP));
			Debug.Log(string.Concat("AssetServer: ", playWebGame.ASSET_URL));
			playWebGame.GAME_TYPE = choosedServer.game_type;
			playWebGame.GAME_DOMAIN = choosedServer.game_domain;
			playWebGame.WORLD_ID = choosedServer.world_id;
			playWebGame.REGISTER_URL_DIRECT = choosedServer.url_register;
			playWebGame.URL_FORGOTTEN_PASSWORD = choosedServer.url_forgotten;
			playWebGame.AVATARS_URL = choosedServer.url_avatars;
			playWebGame.ON_QUIT_URL_DIRECT = choosedServer.url_quit;
			playWebGame.PAYMET_URL = choosedServer.url_payment;
		}
		catch
		{
			Debug.Log("Error choosing game server");
		}
	}

	private static void ShowLoadingAnimation()
	{
		playWebGame.doNotUpdateLbls = true;
		if (playWebGame.loadingAnimation != null)
		{
			playWebGame.lblLoading.text = StaticData.Translate("key_loading_loading_scene");
			playWebGame.lblCurrentAsset.text = playWebGame.currentlyLoadedSet.sceneName;
			playWebGame.lblPercent.text = string.Empty;
			return;
		}
		playWebGame.loadingAnimation = new GuiTextureAnimated();
		playWebGame.loadingAnimation.Init("LoadingAnimation", "LoadingAnimation", "LoadingAnimation/BarAnimation");
		playWebGame.loadingAnimation.rotationTime = 1.5f;
		playWebGame.loadingAnimation.boundries = new Rect(playWebGame.barProgress.boundries.get_x() + 10f, playWebGame.barProgress.boundries.get_y() + 10f, 380f, 6f);
		playWebGame.wndSplash.AddGuiElement(playWebGame.loadingAnimation);
		playWebGame.lblLoading.text = StaticData.Translate("key_loading_loading_scene");
		playWebGame.lblCurrentAsset.text = playWebGame.currentlyLoadedSet.sceneName;
		playWebGame.lblPercent.text = string.Empty;
	}

	private void Start()
	{
		playWebGame.InitMixPanel();
		if (!playWebGame.hadSetHandleLog)
		{
			if (playWebGame.feedbackLogOutput == null)
			{
				playWebGame.feedbackLogOutput = new StringBuilder();
				playWebGame.feedbackLogOutput.AppendLine(string.Format("{0}===================== CLIENT LOG ====================={1}", Environment.get_NewLine(), Environment.get_NewLine()));
			}
			Application.RegisterLogCallback(new Application.LogCallback(null, playWebGame.HandleLog));
			playWebGame.hadSetHandleLog = true;
		}
		Application.set_targetFrameRate(60);
		playWebGame.isWebPlayer = Application.get_isWebPlayer();
		if (!playWebGame.isWebPlayer)
		{
			this.CallServerApi();
		}
		else
		{
			Application.ExternalCall("startGame", new object[0]);
			this.ServersLoaded();
		}
	}

	public static void StartPrepareScene(string sceneName)
	{
		playWebGame.BuildGuiSplash();
		playWebGame.preparedSceneName = sceneName;
		playWebGame.PrepareScene2(sceneName);
		playWebGame.isPreparingScene = true;
		playWebGame.isPreparingSceneBundlesLoaded = false;
		playWebGame.isPreparingSceneReadyForAsyncLoad = false;
		playWebGame.isLoadSceneStarted = false;
	}

	private static void StartReceiveStaticData()
	{
		playWebGame.udp.LoadStaticData();
	}

	public static bool TryGetTextureFromStaticSet(string bundleName, string assetName, out Texture2D tx)
	{
		if (!playWebGame.loadResFromAssets)
		{
			tx = (Texture2D)Resources.Load(string.Concat(bundleName, "/", assetName));
			return tx != null;
		}
		LoadingAsset loadingAsset = null;
		if (!playWebGame.allBundles.TryGetValue(bundleName, ref loadingAsset))
		{
			Debug.LogError(string.Concat("Would not load from bundle ", bundleName));
			tx = null;
			return false;
		}
		if (loadingAsset.www == null)
		{
			Debug.LogError(string.Concat("www not set for ", bundleName, ";", assetName));
		}
		if (!loadingAsset.www.get_assetBundle().Contains(assetName))
		{
			tx = null;
			return false;
		}
		tx = (Texture2D)playWebGame.allBundles.get_Item(bundleName).www.get_assetBundle().Load(assetName);
		return true;
	}

	private void Update()
	{
		playWebGame.timeSinceStart = playWebGame.timeSinceStart + Time.get_deltaTime();
		if (playWebGame.tipsBackground != null)
		{
			playWebGame.tipChangeTime = playWebGame.tipChangeTime + Time.get_deltaTime();
			if (playWebGame.tipChangeTime > 5f)
			{
				playWebGame.tipChangeTime = 0f;
				playWebGame.OnBtnRightClicked(null);
			}
		}
		if (playWebGame.loadLangState != 2 || !playWebGame.ServersReady)
		{
			return;
		}
		if (playWebGame.StartDownload)
		{
			try
			{
				playWebGame.UpdateBundleLoad();
			}
			catch (Exception exception)
			{
				Debug.LogError(exception);
			}
		}
		if (playWebGame._udp != null)
		{
			while (true)
			{
				UdpCommHeader udpCommHeader = playWebGame.udp.ReceiveAsyncMessage();
				if (udpCommHeader == null)
				{
					break;
				}
				if (udpCommHeader.requestType == 28)
				{
					playWebGame.LoadScene(playWebGame.authorization.sceneName);
				}
			}
		}
	}

	private static void UpdateAvatarLoad()
	{
		if (playWebGame.loadingAvatars.get_Count() == 0)
		{
			return;
		}
		List<string> list = new List<string>();
		IEnumerator<string> enumerator = playWebGame.loadingAvatars.get_Keys().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				string current = enumerator.get_Current();
				List<AvatarJob>.Enumerator enumerator1 = playWebGame.loadingAvatars.get_Item(current).GetEnumerator();
				try
				{
					while (enumerator1.MoveNext())
					{
						AvatarJob avatarJob = enumerator1.get_Current();
						if (!avatarJob.job.get_isDone() || avatarJob.job.get_progress() != 1f || !string.IsNullOrEmpty(avatarJob.job.get_error()))
						{
							continue;
						}
						if (!playWebGame.loadedAvatars.ContainsKey(current))
						{
							playWebGame.loadedAvatars.Add(current, avatarJob.job.get_texture());
						}
						if (!list.Contains(current))
						{
							list.Add(current);
						}
						if (avatarJob.callback == null)
						{
							continue;
						}
						avatarJob.callback.Invoke(avatarJob);
					}
				}
				finally
				{
					enumerator1.Dispose();
				}
			}
		}
		finally
		{
			if (enumerator == null)
			{
			}
			enumerator.Dispose();
		}
		foreach (string str in list)
		{
			playWebGame.loadingAvatars.Remove(str);
		}
	}

	private static void UpdateBackgroundImagePosition()
	{
		playWebGame.lastScreenWidth = Screen.get_width();
		playWebGame.txBackground.X = (float)(playWebGame.lastScreenWidth / 2) - playWebGame.txBackground.boundries.get_width() / 2f;
		playWebGame.txBackground.Y = 0f;
		playWebGame.txBackgroundBase.X = 0f;
		playWebGame.txBackgroundBase.Y = 0f;
		playWebGame.txBackgroundBase.boundries.set_width((float)playWebGame.lastScreenWidth);
		playWebGame.txBackgroundBase.boundries.set_height((float)Screen.get_height());
		playWebGame.barProgress.X = (float)(playWebGame.lastScreenWidth / 2 - 200);
		if (playWebGame.loadingAnimation != null)
		{
			playWebGame.loadingAnimation.X = playWebGame.barProgress.X + 10f;
		}
		playWebGame.wndSplash.boundries.set_width((float)playWebGame.lastScreenWidth);
		playWebGame.wndSplash.boundries.set_height((float)Screen.get_height());
		playWebGame.lblLoading.X = playWebGame.barProgress.X;
		playWebGame.lblPercent.X = playWebGame.barProgress.X;
		playWebGame.lblCurrentAsset.X = playWebGame.barProgress.X;
		playWebGame.lblNote.X = playWebGame.barProgress.X - 100f;
		playWebGame.lblError.X = playWebGame.barProgress.X;
		playWebGame.tipsBackground.X = ((float)playWebGame.lastScreenWidth - playWebGame.tipsBackground.boundries.get_width()) / 2f;
		playWebGame.tipsImage.X = ((float)playWebGame.lastScreenWidth - playWebGame.tipsImage.boundries.get_width()) / 2f;
		playWebGame.btnLeft.X = playWebGame.tipsImage.boundries.get_x() - playWebGame.btnLeft.boundries.get_width();
		playWebGame.btnRight.X = playWebGame.tipsImage.boundries.get_x() + playWebGame.tipsImage.boundries.get_width();
		playWebGame.tipsPagingDotOne.X = (float)(playWebGame.lastScreenWidth / 2 - 60);
		playWebGame.tipsPagingDotTwo.X = (float)(playWebGame.lastScreenWidth / 2 - 40);
		playWebGame.tipsPagingDotThree.X = (float)(playWebGame.lastScreenWidth / 2 - 20);
		playWebGame.tipsPagingDotFour.X = (float)(playWebGame.lastScreenWidth / 2);
		playWebGame.tipsPagingDotFive.X = (float)(playWebGame.lastScreenWidth / 2 + 20);
		playWebGame.tipsPagingDotSix.X = (float)(playWebGame.lastScreenWidth / 2 + 40);
		playWebGame.tipsLblOne.X = (float)(playWebGame.lastScreenWidth / 2 - 120);
		switch (playWebGame.tipsIndex)
		{
			case 1:
			{
				playWebGame.tipsLblTwo.boundries.set_x((float)(playWebGame.lastScreenWidth / 2 - 280));
				playWebGame.tipsLblTwo.boundries.set_y(500f);
				playWebGame.tipsLblTwo.boundries.set_width(95f);
				playWebGame.tipsLblTwo.boundries.set_height(26f);
				playWebGame.tipsLblTwo.Alignment = 5;
				playWebGame.tipsLblThree.boundries.set_x((float)(playWebGame.lastScreenWidth / 2 - 80));
				playWebGame.tipsLblThree.boundries.set_y(500f);
				playWebGame.tipsLblThree.boundries.set_width(130f);
				playWebGame.tipsLblThree.Alignment = 3;
				playWebGame.tipsLblFour.boundries.set_x((float)(playWebGame.lastScreenWidth / 2 + 155));
				playWebGame.tipsLblFour.boundries.set_y(510f);
				playWebGame.tipsLblFour.boundries.set_width(125f);
				playWebGame.tipsLblFour.Alignment = 3;
				break;
			}
			case 2:
			{
				playWebGame.tipsLblTwo.boundries.set_x((float)(playWebGame.lastScreenWidth / 2 - 35));
				playWebGame.tipsLblTwo.boundries.set_y(500f);
				playWebGame.tipsLblTwo.boundries.set_width(120f);
				playWebGame.tipsLblTwo.boundries.set_height(26f);
				playWebGame.tipsLblTwo.Alignment = 5;
				playWebGame.tipsLblThree.boundries.set_x((float)(playWebGame.lastScreenWidth / 2 + 190));
				playWebGame.tipsLblThree.boundries.set_y(500f);
				playWebGame.tipsLblThree.boundries.set_width(90f);
				playWebGame.tipsLblThree.Alignment = 3;
				break;
			}
			case 3:
			{
				playWebGame.tipsLblTwo.boundries.set_x((float)(playWebGame.lastScreenWidth / 2 - 280));
				playWebGame.tipsLblTwo.boundries.set_y(500f);
				playWebGame.tipsLblTwo.boundries.set_width(115f);
				playWebGame.tipsLblTwo.boundries.set_height(26f);
				playWebGame.tipsLblTwo.Alignment = 5;
				playWebGame.tipsLblThree.boundries.set_x((float)(playWebGame.lastScreenWidth / 2 - 57));
				playWebGame.tipsLblThree.boundries.set_y(500f);
				playWebGame.tipsLblThree.boundries.set_width(115f);
				playWebGame.tipsLblThree.Alignment = 3;
				break;
			}
			case 4:
			{
				playWebGame.tipsLblTwo.boundries.set_x((float)(playWebGame.lastScreenWidth / 2 - 275));
				playWebGame.tipsLblTwo.boundries.set_y(570f);
				playWebGame.tipsLblTwo.boundries.set_width(350f);
				playWebGame.tipsLblTwo.boundries.set_height(26f);
				playWebGame.tipsLblTwo.Alignment = 4;
				break;
			}
			case 5:
			{
				playWebGame.tipsLblTwo.boundries.set_x((float)(playWebGame.lastScreenWidth / 2 + 30));
				playWebGame.tipsLblTwo.boundries.set_y(350f);
				playWebGame.tipsLblTwo.boundries.set_width(230f);
				playWebGame.tipsLblTwo.boundries.set_height(115f);
				playWebGame.tipsLblTwo.Alignment = 3;
				break;
			}
			case 6:
			{
				playWebGame.tipsLblTwo.boundries.set_x((float)(playWebGame.lastScreenWidth / 2 - 265));
				playWebGame.tipsLblTwo.boundries.set_y(515f);
				playWebGame.tipsLblTwo.boundries.set_width(530f);
				playWebGame.tipsLblTwo.boundries.set_height(90f);
				playWebGame.tipsLblTwo.Alignment = 4;
				break;
			}
		}
	}

	public static void UpdateBundleLoad()
	{
		playWebGame.timeSinceStart = playWebGame.timeSinceStart + Time.get_deltaTime();
		StaticData.now = DateTime.get_Now();
		playWebGame.UpdateAvatarLoad();
		if (playWebGame.isPreparingScene && !playWebGame.isPreparingSceneReadyForAsyncLoad)
		{
			playWebGame.isPreparingSceneReadyForAsyncLoad = Application.CanStreamedLevelBeLoaded(playWebGame.currentlyLoadedSet.sceneName);
		}
		if (playWebGame.isPreparingScene && playWebGame.isPreparingSceneBundlesLoaded && playWebGame.isPreparingSceneReadyForAsyncLoad && !playWebGame.isLoadSceneStarted)
		{
			playWebGame.currentlyLoadedSet.StartLoadLevel();
			playWebGame.currentlyLoadedScene = playWebGame.currentlyLoadedSet.sceneOperation;
			playWebGame.currentlyLoadedSceneName = playWebGame.currentlyLoadedSet.sceneName;
			playWebGame.isLoadSceneStarted = true;
		}
		if (playWebGame.isLoadProgressOnScreen)
		{
			playWebGame.UpdateProgress();
		}
		for (int i = 0; i < 2; i++)
		{
			if (playWebGame.isAllBundlesLoaded)
			{
				return;
			}
			if (playWebGame.currentlyLoadedSet == null)
			{
				playWebGame.ManageCaching();
			}
			else
			{
				playWebGame.ManageSet();
			}
		}
	}

	private static void UpdatePosition(object prm)
	{
		if (playWebGame.lastScreenWidth != Screen.get_width())
		{
			playWebGame.UpdateBackgroundImagePosition();
		}
	}

	public static void UpdateProgress()
	{
		try
		{
			if (playWebGame.currentJobs != null)
			{
				if (playWebGame.lblLoading != null && playWebGame.lblCurrentAsset != null && playWebGame.lblPercent != null && playWebGame.barProgress != null)
				{
					if (!playWebGame.doNotUpdateLbls)
					{
						playWebGame.currentJobSizeBytes = 0;
						playWebGame.currentJobLoadedBytes = 0;
						playWebGame.currentJobProgress = 0f;
						if (playWebGame.isAllBundlesLoaded)
						{
							playWebGame.lblLoading.text = string.Format(StaticData.Translate("key_loading_game_is_cached"), 1);
						}
						else if (playWebGame.currentlyLoadedScene == null && playWebGame.currentlyLoadedSet == null)
						{
							playWebGame.lblLoading.text = StaticData.Translate("key_loading_caching_game");
							playWebGame.lblCurrentAsset.text = playWebGame.allBundles.get_Values().get_Item(Math.Min(playWebGame.currentLoadingBundle, playWebGame.allBundles.get_Count() - 1)).displayName;
						}
						else if (playWebGame.currentlyLoadedScene == null || playWebGame.currentlyLoadedSet != null)
						{
							playWebGame.lblLoading.text = StaticData.Translate("key_loading_loading_objects");
							playWebGame.lblCurrentAsset.text = playWebGame.currentlyLoadedSet.bundles[Math.Min(playWebGame.currentlyLoadedSet.currentBundle, (int)playWebGame.currentlyLoadedSet.bundles.Length - 1)].displayName;
						}
						else
						{
							playWebGame.lblLoading.text = StaticData.Translate("key_loading_loading_scene");
							playWebGame.lblCurrentAsset.text = playWebGame.currentlyLoadedSceneName;
						}
						foreach (LoadingAsset currentJob in playWebGame.currentJobs)
						{
							if (currentJob.www != null)
							{
								playWebGame.currentJobLoadedBytes = playWebGame.currentJobLoadedBytes + (int)(currentJob.www.get_progress() * (float)currentJob.size);
							}
							playWebGame.currentJobSizeBytes = playWebGame.currentJobSizeBytes + currentJob.size;
						}
						playWebGame.currentJobProgress = (float)playWebGame.currentJobLoadedBytes / (float)playWebGame.currentJobSizeBytes;
						!(playWebGame.currentlyLoadedSet.sceneName != "Login");
						playWebGame.barProgress.current = (float)playWebGame.currentJobLoadedBytes;
						playWebGame.barProgress.maximum = (float)playWebGame.currentJobSizeBytes;
						playWebGame.lblPercent.text = playWebGame.currentJobProgress.ToString("#0.0%");
					}
					else
					{
						playWebGame.lblLoading.text = StaticData.Translate("key_loading_loading_scene");
						playWebGame.lblCurrentAsset.text = playWebGame.currentlyLoadedSet.sceneName;
						playWebGame.lblPercent.text = string.Format("{0:##0.0}%", Application.GetStreamProgressForLevel(playWebGame.currentlyLoadedSet.sceneName) * 100f);
					}
				}
			}
		}
		catch (Exception exception)
		{
		}
	}

	private static void UpdateTip()
	{
		playWebGame.tipChangeTime = 0f;
		playWebGame.tipsPagingDotOne.SetTexture((Texture2D)Resources.Load("LoadingGui/paging_normal"));
		playWebGame.tipsPagingDotTwo.SetTexture((Texture2D)Resources.Load("LoadingGui/paging_normal"));
		playWebGame.tipsPagingDotThree.SetTexture((Texture2D)Resources.Load("LoadingGui/paging_normal"));
		playWebGame.tipsPagingDotFour.SetTexture((Texture2D)Resources.Load("LoadingGui/paging_normal"));
		playWebGame.tipsPagingDotFive.SetTexture((Texture2D)Resources.Load("LoadingGui/paging_normal"));
		playWebGame.tipsPagingDotSix.SetTexture((Texture2D)Resources.Load("LoadingGui/paging_normal"));
		switch (playWebGame.tipsIndex)
		{
			case 1:
			{
				playWebGame.tipsPagingDotOne.SetTexture((Texture2D)Resources.Load("LoadingGui/paging_active"));
				playWebGame.tipsImage.SetTexture((Texture2D)Resources.Load("LoadingGui/tip_image_1"));
				playWebGame.tipsLblOne.text = StaticData.Translate("key_loading_tip_1_title");
				playWebGame.tipsLblTwo.text = StaticData.Translate("key_loading_tip_1_text_1");
				playWebGame.tipsLblThree.text = StaticData.Translate("key_loading_tip_1_text_2");
				playWebGame.tipsLblFour.text = StaticData.Translate("key_loading_tip_1_text_3");
				break;
			}
			case 2:
			{
				playWebGame.tipsPagingDotTwo.SetTexture((Texture2D)Resources.Load("LoadingGui/paging_active"));
				playWebGame.tipsImage.SetTexture((Texture2D)Resources.Load("LoadingGui/tip_image_2"));
				playWebGame.tipsLblOne.text = StaticData.Translate("key_loading_tip_2_title");
				playWebGame.tipsLblTwo.text = StaticData.Translate("key_loading_tip_2_text_1");
				playWebGame.tipsLblThree.text = StaticData.Translate("key_loading_tip_2_text_2");
				playWebGame.tipsLblFour.text = string.Empty;
				break;
			}
			case 3:
			{
				playWebGame.tipsPagingDotThree.SetTexture((Texture2D)Resources.Load("LoadingGui/paging_active"));
				playWebGame.tipsImage.SetTexture((Texture2D)Resources.Load("LoadingGui/tip_image_3"));
				playWebGame.tipsLblOne.text = StaticData.Translate("key_loading_tip_3_title");
				playWebGame.tipsLblTwo.text = StaticData.Translate("key_loading_tip_3_text_1");
				playWebGame.tipsLblThree.text = StaticData.Translate("key_loading_tip_3_text_2");
				playWebGame.tipsLblFour.text = string.Empty;
				break;
			}
			case 4:
			{
				playWebGame.tipsPagingDotFour.SetTexture((Texture2D)Resources.Load("LoadingGui/paging_active"));
				playWebGame.tipsImage.SetTexture((Texture2D)Resources.Load("LoadingGui/tip_image_4"));
				playWebGame.tipsLblOne.text = StaticData.Translate("key_loading_tip_4_title");
				playWebGame.tipsLblTwo.text = StaticData.Translate("key_loading_tip_4_text_1");
				playWebGame.tipsLblThree.text = string.Empty;
				playWebGame.tipsLblFour.text = string.Empty;
				break;
			}
			case 5:
			{
				playWebGame.tipsPagingDotFive.SetTexture((Texture2D)Resources.Load("LoadingGui/paging_active"));
				playWebGame.tipsImage.SetTexture((Texture2D)Resources.Load("LoadingGui/tip_image_5"));
				playWebGame.tipsLblOne.text = StaticData.Translate("key_loading_tip_5_title");
				playWebGame.tipsLblTwo.text = StaticData.Translate("key_loading_tip_5_text_1");
				playWebGame.tipsLblThree.text = string.Empty;
				playWebGame.tipsLblFour.text = string.Empty;
				break;
			}
			case 6:
			{
				playWebGame.tipsPagingDotSix.SetTexture((Texture2D)Resources.Load("LoadingGui/paging_active"));
				playWebGame.tipsImage.SetTexture((Texture2D)Resources.Load("LoadingGui/tip_image_6"));
				playWebGame.tipsLblOne.text = StaticData.Translate("key_loading_tip_6_title");
				playWebGame.tipsLblTwo.text = StaticData.Translate("key_loading_tip_6_text_1");
				playWebGame.tipsLblThree.text = string.Empty;
				playWebGame.tipsLblFour.text = string.Empty;
				break;
			}
		}
		playWebGame.UpdateBackgroundImagePosition();
	}
}