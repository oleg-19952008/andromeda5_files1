using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

public class PlayerItems : ITransferable
{
	public const int SELL_PRICE_BONUS_FOR_UPGRADE_DONE = 10;

	public const int SELL_PRICE_BONUS_FOR_ITEM_BONUS = 10;

	public const int SELL_PRICE_BONUS_FOR_ITEM_ACCOUNT_BOUND = 5;

	public static byte BOOSTER_EFFECT_TIME_IN_DAYS;

	public static int UNLOCK_PORTAL_PRICE_FOR_PLAYER;

	public static int UNLOCK_PORTAL_PRICE_FOR_GUILD;

	public static int FREE_TRANSFORM_USAGE_INTERVAL;

	public static int FREE_GIFT_SEND_INTERVAL;

	public static int AMPLIFICATION_SKILL_TREE_MIN_LEVEL;

	public static ushort IsStoryQuestTrackerOff;

	public static ushort TypeCarbon;

	public static ushort TypeOxygen;

	public static ushort TypeHydrogen;

	public static ushort TypeDeuterium;

	public static ushort TypeAmmoSolarCells;

	public static ushort TypeAmmoFusionCells;

	public static ushort TypeAmmoColdFusionCells;

	public static ushort TypeAmmoSulfurFusionCells;

	public static ushort TypePlayersKills;

	public static ushort TypeUltralibrium;

	public static ushort TypeCash;

	public static ushort TypeNova;

	public static ushort TypeExperience;

	public static ushort TypeWarCommendation;

	public static ushort TypeNeuron;

	public static ushort TypeMetyl;

	public static ushort TypeCarbonDioxide;

	public static ushort TypeWater;

	public static ushort TypeAceton;

	public static ushort TypeEquilibrium;

	public static ushort TypeHonor;

	public static ushort TypeTalentPoint;

	public static ushort TypeRepairVaucher;

	public static ushort TypeTier2Resources;

	public static ushort TypePortalKey;

	public static ushort TypePortalPillar;

	public static ushort TypePortalCharger;

	public static ushort TypePortalSegment;

	public static ushort TypePortalSchematic;

	public static ushort TypeSoundVolume;

	public static ushort TypeUltraGalaxy1PortalSchematic;

	public static ushort TypeUltraGalaxy1RunicKey1;

	public static ushort TypeUltraGalaxy1RunicKey2;

	public static ushort TypeUltraGalaxy1RunicKey3;

	public static ushort TypeUltraGalaxy1PortalPillar;

	public static ushort TypeUltraGalaxy1PortalCharger;

	public static ushort TypeUltraGalaxy1PortalSegment;

	public static ushort TypeUltraGalaxy2PortalSchematic;

	public static ushort TypeUltraGalaxy2RunicKey1;

	public static ushort TypeUltraGalaxy2RunicKey2;

	public static ushort TypeUltraGalaxy2RunicKey3;

	public static ushort TypeUltraGalaxy2PortalPillar;

	public static ushort TypeUltraGalaxy2PortalCharger;

	public static ushort TypeUltraGalaxy2PortalSegment;

	public static ushort TypeUltraGalaxy3PortalSchematic;

	public static ushort TypeUltraGalaxy3RunicKey1;

	public static ushort TypeUltraGalaxy3RunicKey2;

	public static ushort TypeUltraGalaxy3RunicKey3;

	public static ushort TypeUltraGalaxy3PortalPillar;

	public static ushort TypeUltraGalaxy3PortalCharger;

	public static ushort TypeUltraGalaxy3PortalSegment;

	public static ushort TypeAchievementSpaceDriller;

	public static ushort TypeAchievementVulture;

	public static ushort TypeAchievementBotKiller;

	public static ushort TypeAchievementMerchant;

	public static ushort TypeAchievementAlchemist;

	public static ushort TypeAchievementAmmunition;

	public static ushort TypeAchievementVeteran;

	public static ushort TypeAchievementAce;

	public static ushort TypeAchievementAviator;

	public static ushort TypeAchievementEngineer;

	public static ushort TypeAchievementAdventurer;

	public static ushort TypeAchievementSoldierOfFortune;

	public static ushort TypeAchievementGoldDigger;

	public static ushort TypeAchievementAristocrate;

	public static ushort TypeAchievementInvestor;

	public static ushort TypeAchievementRational;

	public static ushort TypeAchievementNeat;

	public static ushort TypeAchievementStoreKeeper;

	public static ushort TypeAchievementInsecure;

	public static ushort TypeAchievementTrueGrit;

	public static ushort TypeAchievementAssassin;

	public static ushort TypeAchievementGladiator;

	public static ushort TypeAchievementWarMaster;

	public static ushort TypeWeaponLaserTire1;

	public static ushort TypeWeaponLaserTire2;

	public static ushort TypeWeaponLaserTire3;

	public static ushort TypeWeaponLaserTire4;

	public static ushort TypeWeaponLaserTire5;

	public static ushort TypeWeaponPlasmaTire1;

	public static ushort TypeWeaponPlasmaTire2;

	public static ushort TypeWeaponPlasmaTire3;

	public static ushort TypeWeaponPlasmaTire4;

	public static ushort TypeWeaponPlasmaTire5;

	public static ushort TypeWeaponIonTire1;

	public static ushort TypeWeaponIonTire2;

	public static ushort TypeWeaponIonTire3;

	public static ushort TypeWeaponIonTire4;

	public static ushort TypeWeaponIonTire5;

	public static ushort TypeEngineJet1;

	public static ushort TypeEngineJet2;

	public static ushort TypeEngineJet3;

	public static ushort TypeEngineUltra1;

	public static ushort TypeEngineHyper1;

	public static ushort TypeEngineHyper2;

	public static ushort TypeEngineUltra2;

	public static ushort TypeShieldMinor;

	public static ushort TypeShieldMinor2;

	public static ushort TypeShieldBasic;

	public static ushort TypeShieldBasic2;

	public static ushort TypeShieldLight;

	public static ushort TypeShieldLight2;

	public static ushort TypeShieldMedium;

	public static ushort TypeShieldMedium2;

	public static ushort TypeShieldHeavy;

	public static ushort TypeShieldHeavy2;

	public static ushort TypeShieldHeavy3;

	public static ushort TypeShieldLightEmp;

	public static ushort TypeShieldLightEmp2;

	public static ushort TypeShieldLightEmp3;

	public static ushort TypeShieldMedEmp;

	public static ushort TypeShieldMedEmp2;

	public static ushort TypeShieldMedEmp3;

	public static ushort TypeShieldMedEmp4;

	public static ushort TypeShieldAdvEmp;

	public static ushort TypeShieldAdvEmp2;

	public static ushort TypeShieldAdvEmp3;

	public static ushort TypeShieldAdvEmp4;

	public static ushort TypeShieldHeavyEmp;

	public static ushort TypeShieldHeavyEmp2;

	public static ushort TypeShieldHeavyEmp3;

	public static ushort TypeShieldHeavyEmp4;

	public static ushort TypeShieldThorium;

	public static ushort TypeShieldNeutronProtector;

	public static ushort TypeShieldParticleBeam;

	public static ushort TypeShieldIonField;

	public static ushort TypeCorpusMinor;

	public static ushort TypeCorpusMinor2;

	public static ushort TypeCorpusBasic;

	public static ushort TypeCorpusBasic2;

	public static ushort TypeCorpusLight;

	public static ushort TypeCorpusLight2;

	public static ushort TypeCorpusLightSteel;

	public static ushort TypeCorpusLightSteel2;

	public static ushort TypeCorpusSteel;

	public static ushort TypeCorpusSteel2;

	public static ushort TypeCorpusSteel3;

	public static ushort TypeCorpusHeavySteel;

	public static ushort TypeCorpusHeavySteel2;

	public static ushort TypeCorpusHeavySteel3;

	public static ushort TypeCorpusLightAllum;

	public static ushort TypeCorpusLightAllum2;

	public static ushort TypeCorpusLightAllum3;

	public static ushort TypeCorpusLightAllum4;

	public static ushort TypeCorpusMedAllum;

	public static ushort TypeCorpusMedAllum2;

	public static ushort TypeCorpusMedAllum3;

	public static ushort TypeCorpusMedAllum4;

	public static ushort TypeCorpusHeavyAllum;

	public static ushort TypeCorpusHeavyAllum2;

	public static ushort TypeCorpusHeavyAllum3;

	public static ushort TypeCorpusHeavyAllum4;

	public static ushort TypeCorpusAllumShell;

	public static ushort TypeCorpusDefender;

	public static ushort TypeCorpusTitanium;

	public static ushort TypeCorpusAegis;

	public static ushort TypeExtraMolecularCompresor;

	public static ushort TypeExtraNuclearCompresor;

	public static ushort TypeExtraFusionCompresor;

	public static ushort TypeExtraLaserWeaponsModule;

	public static ushort TypeExtraPlasmaWeaponsModule;

	public static ushort TypeExtraIonWeaponsModule;

	public static ushort TypeExtraUltraWeaponsModule;

	public static ushort TypeExtraLaserAimingCPU;

	public static ushort TypeExtraPlasmaAimingCPU;

	public static ushort TypeExtraIonAimingCPU;

	public static ushort TypeExtraUltraAimingCPU;

	public static ushort TypeExtraLightMiningDrill;

	public static ushort TypeExtraUltraMiningDrill;

	public static ushort TypeExtraLaserWeaponsCoolant;

	public static ushort TypeExtraPlasmaWeaponsCoolant;

	public static ushort TypeExtraIonWeaponsCoolant;

	public static ushort TypeExtraUltraWeaponsCoolant;

	public static ushort TypeExtraBasicCPUforShildRegen;

	public static ushort TypeExtraAdvancedCPUforShildRegen;

	public static ushort TypeExtraOverclockedCPUforShildRegen;

	public static ushort TypeShieldHeavyEmp5;

	public static ushort TypeShieldHeavyEmp6;

	public static ushort TypeShieldHeavyEmp7;

	public static ushort TypeCorpusHeavyAllum5;

	public static ushort TypeCorpusHeavyAllum6;

	public static ushort TypeCorpusHeavyAllum7;

	public static ushort TypeEngineHyper3;

	public static ushort TypeEngineHyper4;

	public static ushort TypeEngineHyper5;

	public static ushort TypeEngineHyper6;

	public static ushort TypeEngineHyper7;

	public static ushort TypeExtraUltraNuclearCompresor;

	public static ushort TypeExtraLaserWeaponsModule1;

	public static ushort TypeExtraLaserWeaponsModule2;

	public static ushort TypeExtraLaserWeaponsModule3;

	public static ushort TypeExtraLaserWeaponsModule4;

	public static ushort TypeExtraLaserWeaponsModule5;

	public static ushort TypeExtraPlasmaWeaponsModule1;

	public static ushort TypeExtraPlasmaWeaponsModule2;

	public static ushort TypeExtraPlasmaWeaponsModule3;

	public static ushort TypeExtraPlasmaWeaponsModule4;

	public static ushort TypeExtraPlasmaWeaponsModule5;

	public static ushort TypeExtraIonWeaponsModule1;

	public static ushort TypeExtraIonWeaponsModule2;

	public static ushort TypeExtraIonWeaponsModule3;

	public static ushort TypeExtraIonWeaponsModule4;

	public static ushort TypeExtraIonWeaponsModule5;

	public static ushort TypeExtraLaserWeaponsCoolant1;

	public static ushort TypeExtraLaserWeaponsCoolant2;

	public static ushort TypeExtraLaserWeaponsCoolant3;

	public static ushort TypeExtraLaserWeaponsCoolant4;

	public static ushort TypeExtraLaserWeaponsCoolant5;

	public static ushort TypeExtraPlasmaWeaponsCoolant1;

	public static ushort TypeExtraPlasmaWeaponsCoolant2;

	public static ushort TypeExtraPlasmaWeaponsCoolant3;

	public static ushort TypeExtraPlasmaWeaponsCoolant4;

	public static ushort TypeExtraPlasmaWeaponsCoolant5;

	public static ushort TypeExtraIonWeaponsCoolant1;

	public static ushort TypeExtraIonWeaponsCoolant2;

	public static ushort TypeExtraIonWeaponsCoolant3;

	public static ushort TypeExtraIonWeaponsCoolant4;

	public static ushort TypeExtraIonWeaponsCoolant5;

	public static ushort TypeExtraCPUforShildRegen30;

	public static ushort TypeExtraCPUforShildRegen40;

	public static ushort TypeExtraCPUforShildRegen50;

	public static ushort TypeExtraCPUforShildRegen60;

	public static ushort TypeExtraCPUforShildRegen70;

	public static ushort TypeBoosterDamageFor3Days;

	public static ushort TypeBoosterCargoFor3Days;

	public static ushort TypeBoosterExperienceFor3Days;

	public static ushort TypeBoosterAutominerFor3Days;

	public static ushort TypeBoosterDamageFor1Days;

	public static ushort TypeBoosterCargoFor1Days;

	public static ushort TypeBoosterExperienceFor1Days;

	public static ushort TypeBoosterAutominerFor1Days;

	public static ushort TypeBoosterDamageFor6Days;

	public static ushort TypeBoosterCargoFor6Days;

	public static ushort TypeBoosterExperienceFor6Days;

	public static ushort TypeBoosterAutominerFor6Days;

	public static ushort TypeBoosterPackageDeal;

	public static ushort TypePowerUpForLaserDamageFlat;

	public static ushort TypePowerUpForPlasmaDamageFlat;

	public static ushort TypePowerUpForIonDamageFlat;

	public static ushort TypePowerUpForLaserDamagePercentage;

	public static ushort TypePowerUpForPlasmaDamagePercentage;

	public static ushort TypePowerUpForIonDamagePercentage;

	public static ushort TypePowerUpForTotalDamagePercentage;

	public static ushort TypePowerUpForCorpusFlat;

	public static ushort TypePowerUpForCorpusPercentage;

	public static ushort TypePowerUpForShieldFlat;

	public static ushort TypePowerUpForShieldPercentage;

	public static ushort TypePowerUpForEndurancePercentage;

	public static ushort TypePowerUpForShieldPowerFlat;

	public static ushort TypePowerUpForShieldPowerPercentage;

	public static ushort TypePowerUpForTargetingFlat;

	public static ushort TypePowerUpForTargetingPercentage;

	public static ushort TypePowerUpForAvoidanceFlat;

	public static ushort TypePowerUpForAvoidancePercentage;

	public static ushort TypePowerUpDamagePackageDeal;

	public static ushort TypePowerUpShieldPackageDeal;

	public static ushort TypePowerUpCorpusPackageDeal;

	public static ushort TypePowerUpShieldPowerPackageDeal;

	public static ushort TypePowerUpTargetingPackageDeal;

	public static ushort TypePowerUpAvoidancePackageDeal;

	public static ushort TypeTalentsDefiance;

	public static ushort TypeTalentsAdvancedCorpus;

	public static ushort TypeTalentsFindWeakSpot;

	public static ushort TypeTalentsWeaponMastery;

	public static ushort TypeTalentsEngineBooster;

	public static ushort TypeTalentsImprovedShield;

	public static ushort TypeTalentsTaunt;

	public static ushort TypeTalentsRocketBarrage;

	public static ushort TypeTalentsUnstoppable;

	public static ushort TypeTalentsForceWave;

	public static ushort TypeTalentsShieldFortress;

	public static ushort TypeTalentsLaserDestruction;

	public static ushort TypeTalentsStealth;

	public static ushort TypeTalentsDecoy;

	public static ushort TypeTalentsPowerBreak;

	public static ushort TypeTalentsPowerCut;

	public static ushort TypeTalentsLightSpeed;

	public static ushort TypeTalentsMistShroud;

	public static ushort TypeTalentsRepairingDrones;

	public static ushort TypeTalentsNanoStorm;

	public static ushort TypeTalentsNanoShield;

	public static ushort TypeTalentsRepairField;

	public static ushort TypeTalentsEmpoweredShield;

	public static ushort TypeTalentsNanoTechnology;

	public static ushort TypeTalentsArchiver;

	public static ushort TypeTalentsMerchant;

	public static ushort TypeTalentsSteadyAim;

	public static ushort TypeTalentsSwiftLearner;

	public static ushort TypeTalentsAlienSpecialist;

	public static ushort TypeTalentsVelocity;

	public static ushort TypeTalentsBountySpecialist;

	public static ushort TypeTalentsDamageReduction;

	public static ushort TypeTalentsShortCircuit;

	public static ushort TypeTalentsPulseNova;

	public static ushort TypeTalentsRemedy;

	public static ushort TypeTalentsFocusFire;

	public static ushort TypeTalentsAdvancedShield;

	public static ushort TypeTalentsSunderArmor;

	public static ushort TypeTalentsArmorBreaker;

	public static ushort TypeTalentsRealSteel;

	public static ushort TypeTalentsRocketeer;

	public static ushort TypeTalentsPowerControl;

	public static ushort TypeTalentsMassiveDamage;

	public static ushort TypeTalentsSpeedster;

	public static ushort TypeTalentsFutureTechnology;

	public static ushort TypeTalentsDronePower;

	public static ushort TypeTalentsRepairMaster;

	public static ushort TypeCouncilSkillDisarm;

	public static ushort TypeCouncilSkillSacrifice;

	public static ushort TypeCouncilSkillLifesteal;

	public static ushort TypeNeuronsForArmorBreaker;

	public static ushort TypeNeuronsForRealSteel;

	public static ushort TypeNeuronsForRocketeer;

	public static ushort TypeNeuronsForPowerControl;

	public static ushort TypeNeuronsForMassiveDamage;

	public static ushort TypeNeuronsForSpeedster;

	public static ushort TypeNeuronsForFutureTechnology;

	public static ushort TypeNeuronsForDronePower;

	public static ushort TypeNeuronsForRepairMaster;

	public static ushort TypeNeuronsForDefiance;

	public static ushort TypeNeuronsForAdvancedCorpus;

	public static ushort TypeNeuronsForFindWeakSpot;

	public static ushort TypeNeuronsForWeaponMastery;

	public static ushort TypeNeuronsForEngineBooster;

	public static ushort TypeNeuronsForImprovedShield;

	public static ushort TypeNeuronsForTaunt;

	public static ushort TypeNeuronsForRocketBarrage;

	public static ushort TypeNeuronsForUnstoppable;

	public static ushort TypeNeuronsForForceWave;

	public static ushort TypeNeuronsForShieldFortress;

	public static ushort TypeNeuronsForLaserDestruction;

	public static ushort TypeNeuronsForStealth;

	public static ushort TypeNeuronsForDecoy;

	public static ushort TypeNeuronsForPowerBreak;

	public static ushort TypeNeuronsForPowerCut;

	public static ushort TypeNeuronsForLightSpeed;

	public static ushort TypeNeuronsForMistShroud;

	public static ushort TypeNeuronsForRepairingDrones;

	public static ushort TypeNeuronsForNanoStorm;

	public static ushort TypeNeuronsForNanoShield;

	public static ushort TypeNeuronsForRepairField;

	public static ushort TypeNeuronsForEmpoweredShield;

	public static ushort TypeNeuronsForNanoTechnology;

	public static ushort TypeNeuronsForArchiver;

	public static ushort TypeNeuronsForMerchant;

	public static ushort TypeNeuronsForSteadyAim;

	public static ushort TypeNeuronsForSwiftLearner;

	public static ushort TypeNeuronsForAlienSpecialist;

	public static ushort TypeNeuronsForVelocity;

	public static ushort TypeNeuronsForBountySpecialist;

	public static ushort TypeNeuronsForDamageReduction;

	public static ushort TypeNeuronsForShortCircuit;

	public static ushort TypeNeuronsForPulseNova;

	public static ushort TypeNeuronsForRemedy;

	public static ushort TypeNeuronsForFocusFire;

	public static ushort TypeNeuronsForAdvancedShield;

	public static ushort TypeNeuronsForSunderArmor;

	public static ushort TypeQuestItem01;

	public static ushort TypeQuestItem02;

	public static ushort TypeQuestItem03;

	public static ushort TypeQuestItem04;

	public static ushort TypeQuestItem05;

	public static ushort TypeQuestItem06;

	public static ushort TypeQuestItem07;

	public static ushort TypeQuestItem08;

	public static ushort TypeQuestItem09;

	public static ushort TypeQuestItem10;

	public static ushort TypeQuestItem11;

	public static ushort TypeQuestItem12;

	public static ushort TypeQuestItem13;

	public static ushort TypeQuestItem14;

	public static ushort TypeQuestItem15;

	public static ushort TypeQuestItem16;

	public static ushort TypeQuestItem17;

	public static ushort TypeQuestItem18;

	public static ushort TypeQuestItem19;

	public static ushort TypeQuestItem20;

	public static ushort TypeQuestItem21;

	public static ushort TypeQuestItem22;

	public static ushort TypeQuestItem23;

	public static ushort TypeQuestItem24;

	public static ushort TypeQuestItem25;

	public static ushort TypeQuestItem26;

	public static ushort TypeQuestItem27;

	public static ushort TypeQuestItem28;

	public static ushort TypeQuestItem29;

	public static ushort TypeQuestItem30;

	[NonSerialized]
	public static List<ushort> abilityTypesProtector;

	[NonSerialized]
	public static List<ushort> abilityTypesGuardian;

	[NonSerialized]
	public static List<ushort> abilityTypesDestroyer;

	[NonSerialized]
	public static List<ushort> abilityTypesPassive;

	[NonSerialized]
	public static List<ushort> abilityTypesAmplification;

	public static SortedList<int, List<ushort>> guardianSkills;

	public static SortedList<int, List<ushort>> destroyerSkills;

	public static SortedList<int, List<ushort>> protectorSkills;

	public static SortedList<int, List<ushort>> passiveSkills;

	public static SortedList<int, List<ushort>> amplificationSkillsOne;

	public static SortedList<int, List<ushort>> amplificationSkillsTwo;

	public static SortedList<int, List<ushort>> amplificationSkillsThree;

	public static List<ushort> genericItemsTypes;

	public static SortedList<byte, ushort[]> categoriesMapping;

	public static SortedList<ushort, byte> item2categoryMapping;

	public List<ushort> toAdd = new List<ushort>();

	public List<ushort> toRemove = new List<ushort>();

	public List<ushort> toUpdate = new List<ushort>();

	public List<SlotItem> toUpdateSlotItem = new List<SlotItem>();

	public List<PortalPart> toUpdatePortalPart = new List<PortalPart>();

	[NonSerialized]
	public SortedList<ushort, long> items = new SortedList<ushort, long>();

	public List<SlotItem> slotItems = new List<SlotItem>();

	public List<PortalPart> portalParts = new List<PortalPart>();

	public ushort[] keys;

	public long[] values;

	[NonSerialized]
	public static ushort[] cargoTypes;

	[NonSerialized]
	public static ushort[] ammoTypes;

	[NonSerialized]
	public static ushort[] lootableTypes;

	[NonSerialized]
	public static SortedList<ushort, int> ammoItemId2DbMapping;

	public static SortedList<ushort, int> specialAmounts;

	public static SortedList<ushort, SortedList<ushort, short>> fusionDependancies;

	public static SortedList<ushort, SkillEffectInfo> skillsInformation;

	public List<NovaUpdate> novaUpdates = new List<NovaUpdate>();

	public long[] Ammos
	{
		get
		{
			long[] amountAt = new long[(int)PlayerItems.ammoTypes.Length];
			for (int i = 0; i < (int)PlayerItems.ammoTypes.Length; i++)
			{
				amountAt[i] = this.GetAmountAt(PlayerItems.ammoTypes[i]);
			}
			return amountAt;
		}
		set
		{
			for (int i = 0; i < (int)PlayerItems.ammoTypes.Length; i++)
			{
				this.Set(PlayerItems.ammoTypes[i], value[i]);
			}
		}
	}

	public long Cargo
	{
		get
		{
			long amount = (long)0;
			int num = this.slotItems.Count<SlotItem>();
			for (int i = 0; i < num; i++)
			{
				if (this.slotItems[i].SlotType == 3)
				{
					amount = amount + (long)this.slotItems[i].Amount;
				}
			}
			return amount;
		}
	}

	public long Cash
	{
		get
		{
			return this.GetAmountAt(PlayerItems.TypeCash);
		}
	}

	public long Equilibrium
	{
		get
		{
			return this.GetAmountAt(PlayerItems.TypeEquilibrium);
		}
	}

	public long Honor
	{
		get
		{
			return this.GetAmountAt(PlayerItems.TypeHonor);
		}
	}

	public long Nova
	{
		get
		{
			return this.GetAmountAt(PlayerItems.TypeNova);
		}
	}

	public long Ultralibrium
	{
		get
		{
			return this.GetAmountAt(PlayerItems.TypeUltralibrium);
		}
	}

	public long WarCommendation
	{
		get
		{
			return this.GetAmountAt(PlayerItems.TypeWarCommendation);
		}
	}

	static PlayerItems()
	{
		PlayerItems.BOOSTER_EFFECT_TIME_IN_DAYS = 3;
		PlayerItems.UNLOCK_PORTAL_PRICE_FOR_PLAYER = 200;
		PlayerItems.UNLOCK_PORTAL_PRICE_FOR_GUILD = 1000;
		PlayerItems.FREE_TRANSFORM_USAGE_INTERVAL = 24;
		PlayerItems.FREE_GIFT_SEND_INTERVAL = 24;
		PlayerItems.AMPLIFICATION_SKILL_TREE_MIN_LEVEL = 50;
		PlayerItems.TypePlayersKills = 10;
		PlayerItems.TypeUltralibrium = 25;
		PlayerItems.TypeCash = 11;
		PlayerItems.TypeNova = 12;
		PlayerItems.TypeExperience = 13;
		PlayerItems.TypeNeuron = 14;
		PlayerItems.TypeWarCommendation = 15;
		PlayerItems.TypeHonor = 20;
		PlayerItems.TypeTalentPoint = 21;
		PlayerItems.TypeRepairVaucher = 22;
		PlayerItems.TypeEquilibrium = 53;
		PlayerItems.TypeTier2Resources = 54;
		PlayerItems.TypePortalKey = 55;
		PlayerItems.TypePortalPillar = 56;
		PlayerItems.TypePortalCharger = 57;
		PlayerItems.TypePortalSegment = 58;
		PlayerItems.TypePortalSchematic = 59;
		PlayerItems.TypeSoundVolume = 100;
		PlayerItems.IsStoryQuestTrackerOff = 900;
		PlayerItems.TypeAchievementSpaceDriller = 200;
		PlayerItems.TypeAchievementVulture = 201;
		PlayerItems.TypeAchievementBotKiller = 202;
		PlayerItems.TypeAchievementMerchant = 203;
		PlayerItems.TypeAchievementAlchemist = 204;
		PlayerItems.TypeAchievementAmmunition = 205;
		PlayerItems.TypeAchievementVeteran = 206;
		PlayerItems.TypeAchievementAce = 207;
		PlayerItems.TypeAchievementAviator = 208;
		PlayerItems.TypeAchievementEngineer = 209;
		PlayerItems.TypeAchievementAdventurer = 210;
		PlayerItems.TypeAchievementSoldierOfFortune = 211;
		PlayerItems.TypeAchievementGoldDigger = 212;
		PlayerItems.TypeAchievementAristocrate = 213;
		PlayerItems.TypeAchievementInvestor = 214;
		PlayerItems.TypeAchievementRational = 215;
		PlayerItems.TypeAchievementNeat = 216;
		PlayerItems.TypeAchievementStoreKeeper = 217;
		PlayerItems.TypeAchievementInsecure = 218;
		PlayerItems.TypeAchievementTrueGrit = 219;
		PlayerItems.TypeAchievementAssassin = 220;
		PlayerItems.TypeAchievementGladiator = 221;
		PlayerItems.TypeAchievementWarMaster = 222;
		PlayerItems.TypeWeaponLaserTire1 = 4000;
		PlayerItems.TypeWeaponLaserTire2 = 4001;
		PlayerItems.TypeWeaponLaserTire3 = 4002;
		PlayerItems.TypeWeaponLaserTire4 = 4003;
		PlayerItems.TypeWeaponLaserTire5 = 4004;
		PlayerItems.TypeWeaponPlasmaTire1 = 4005;
		PlayerItems.TypeWeaponPlasmaTire2 = 4006;
		PlayerItems.TypeWeaponPlasmaTire3 = 4007;
		PlayerItems.TypeWeaponPlasmaTire4 = 4008;
		PlayerItems.TypeWeaponPlasmaTire5 = 4009;
		PlayerItems.TypeWeaponIonTire1 = 4010;
		PlayerItems.TypeWeaponIonTire2 = 4011;
		PlayerItems.TypeWeaponIonTire3 = 4012;
		PlayerItems.TypeWeaponIonTire4 = 4013;
		PlayerItems.TypeWeaponIonTire5 = 4014;
		PlayerItems.TypeAmmoSolarCells = 6000;
		PlayerItems.TypeAmmoFusionCells = 6001;
		PlayerItems.TypeAmmoColdFusionCells = 6002;
		PlayerItems.TypeAmmoSulfurFusionCells = 6003;
		PlayerItems.TypeEngineJet1 = 3000;
		PlayerItems.TypeEngineJet2 = 3001;
		PlayerItems.TypeEngineJet3 = 3002;
		PlayerItems.TypeEngineUltra1 = 3005;
		PlayerItems.TypeEngineHyper1 = 3003;
		PlayerItems.TypeEngineHyper2 = 3004;
		PlayerItems.TypeEngineUltra2 = 3006;
		PlayerItems.TypeCarbon = 11001;
		PlayerItems.TypeOxygen = 11002;
		PlayerItems.TypeHydrogen = 11000;
		PlayerItems.TypeDeuterium = 11003;
		PlayerItems.TypeMetyl = 11004;
		PlayerItems.TypeCarbonDioxide = 11005;
		PlayerItems.TypeWater = 11006;
		PlayerItems.TypeAceton = 11007;
		PlayerItems.TypeShieldMinor = 2000;
		PlayerItems.TypeShieldMinor2 = 2001;
		PlayerItems.TypeShieldBasic = 2002;
		PlayerItems.TypeShieldBasic2 = 2003;
		PlayerItems.TypeShieldLight = 2004;
		PlayerItems.TypeShieldLight2 = 2005;
		PlayerItems.TypeShieldMedium = 2006;
		PlayerItems.TypeShieldMedium2 = 2007;
		PlayerItems.TypeShieldHeavy = 2008;
		PlayerItems.TypeShieldHeavy2 = 2009;
		PlayerItems.TypeShieldHeavy3 = 2010;
		PlayerItems.TypeShieldLightEmp = 2011;
		PlayerItems.TypeShieldLightEmp2 = 2012;
		PlayerItems.TypeShieldLightEmp3 = 2013;
		PlayerItems.TypeShieldMedEmp = 2014;
		PlayerItems.TypeShieldMedEmp2 = 2015;
		PlayerItems.TypeShieldMedEmp3 = 2016;
		PlayerItems.TypeShieldMedEmp4 = 2017;
		PlayerItems.TypeShieldAdvEmp = 2018;
		PlayerItems.TypeShieldAdvEmp2 = 2019;
		PlayerItems.TypeShieldAdvEmp3 = 2020;
		PlayerItems.TypeShieldAdvEmp4 = 2021;
		PlayerItems.TypeShieldHeavyEmp = 2022;
		PlayerItems.TypeShieldHeavyEmp2 = 2023;
		PlayerItems.TypeShieldHeavyEmp3 = 2024;
		PlayerItems.TypeShieldHeavyEmp4 = 2025;
		PlayerItems.TypeShieldThorium = 2026;
		PlayerItems.TypeShieldNeutronProtector = 2027;
		PlayerItems.TypeShieldParticleBeam = 2028;
		PlayerItems.TypeShieldIonField = 2029;
		PlayerItems.TypeCorpusMinor = 1000;
		PlayerItems.TypeCorpusMinor2 = 1001;
		PlayerItems.TypeCorpusBasic = 1002;
		PlayerItems.TypeCorpusBasic2 = 1003;
		PlayerItems.TypeCorpusLight = 1004;
		PlayerItems.TypeCorpusLight2 = 1005;
		PlayerItems.TypeCorpusLightSteel = 1006;
		PlayerItems.TypeCorpusLightSteel2 = 1007;
		PlayerItems.TypeCorpusSteel = 1008;
		PlayerItems.TypeCorpusSteel2 = 1009;
		PlayerItems.TypeCorpusSteel3 = 1010;
		PlayerItems.TypeCorpusHeavySteel = 1011;
		PlayerItems.TypeCorpusHeavySteel2 = 1012;
		PlayerItems.TypeCorpusHeavySteel3 = 1013;
		PlayerItems.TypeCorpusLightAllum = 1014;
		PlayerItems.TypeCorpusLightAllum2 = 1015;
		PlayerItems.TypeCorpusLightAllum3 = 1016;
		PlayerItems.TypeCorpusLightAllum4 = 1017;
		PlayerItems.TypeCorpusMedAllum = 1018;
		PlayerItems.TypeCorpusMedAllum2 = 1019;
		PlayerItems.TypeCorpusMedAllum3 = 1020;
		PlayerItems.TypeCorpusMedAllum4 = 1021;
		PlayerItems.TypeCorpusHeavyAllum = 1022;
		PlayerItems.TypeCorpusHeavyAllum2 = 1023;
		PlayerItems.TypeCorpusHeavyAllum3 = 1024;
		PlayerItems.TypeCorpusHeavyAllum4 = 1025;
		PlayerItems.TypeCorpusAllumShell = 1026;
		PlayerItems.TypeCorpusDefender = 1027;
		PlayerItems.TypeCorpusTitanium = 1028;
		PlayerItems.TypeCorpusAegis = 1029;
		PlayerItems.TypeExtraMolecularCompresor = 5000;
		PlayerItems.TypeExtraNuclearCompresor = 5002;
		PlayerItems.TypeExtraFusionCompresor = 5001;
		PlayerItems.TypeExtraLaserWeaponsModule = 5003;
		PlayerItems.TypeExtraPlasmaWeaponsModule = 5004;
		PlayerItems.TypeExtraIonWeaponsModule = 5005;
		PlayerItems.TypeExtraUltraWeaponsModule = 5006;
		PlayerItems.TypeExtraLaserAimingCPU = 5007;
		PlayerItems.TypeExtraPlasmaAimingCPU = 5008;
		PlayerItems.TypeExtraIonAimingCPU = 5009;
		PlayerItems.TypeExtraUltraAimingCPU = 5010;
		PlayerItems.TypeExtraLightMiningDrill = 5011;
		PlayerItems.TypeExtraUltraMiningDrill = 5012;
		PlayerItems.TypeExtraLaserWeaponsCoolant = 5013;
		PlayerItems.TypeExtraPlasmaWeaponsCoolant = 5014;
		PlayerItems.TypeExtraIonWeaponsCoolant = 5015;
		PlayerItems.TypeExtraUltraWeaponsCoolant = 5016;
		PlayerItems.TypeExtraBasicCPUforShildRegen = 5017;
		PlayerItems.TypeExtraAdvancedCPUforShildRegen = 5018;
		PlayerItems.TypeExtraOverclockedCPUforShildRegen = 5019;
		PlayerItems.TypeShieldHeavyEmp5 = 2031;
		PlayerItems.TypeShieldHeavyEmp6 = 2032;
		PlayerItems.TypeShieldHeavyEmp7 = 2033;
		PlayerItems.TypeCorpusHeavyAllum5 = 1031;
		PlayerItems.TypeCorpusHeavyAllum6 = 1032;
		PlayerItems.TypeCorpusHeavyAllum7 = 1033;
		PlayerItems.TypeEngineHyper3 = 3007;
		PlayerItems.TypeEngineHyper4 = 3008;
		PlayerItems.TypeEngineHyper5 = 3009;
		PlayerItems.TypeEngineHyper6 = 3010;
		PlayerItems.TypeEngineHyper7 = 3011;
		PlayerItems.TypeExtraUltraNuclearCompresor = 5020;
		PlayerItems.TypeExtraIonWeaponsCoolant1 = 5021;
		PlayerItems.TypeExtraIonWeaponsCoolant2 = 5022;
		PlayerItems.TypeExtraIonWeaponsCoolant3 = 5023;
		PlayerItems.TypeExtraIonWeaponsCoolant4 = 5024;
		PlayerItems.TypeExtraIonWeaponsCoolant5 = 5025;
		PlayerItems.TypeExtraIonWeaponsModule1 = 5026;
		PlayerItems.TypeExtraIonWeaponsModule2 = 5027;
		PlayerItems.TypeExtraIonWeaponsModule3 = 5028;
		PlayerItems.TypeExtraIonWeaponsModule4 = 5029;
		PlayerItems.TypeExtraIonWeaponsModule5 = 5030;
		PlayerItems.TypeExtraLaserWeaponsCoolant1 = 5031;
		PlayerItems.TypeExtraLaserWeaponsCoolant2 = 5032;
		PlayerItems.TypeExtraLaserWeaponsCoolant3 = 5033;
		PlayerItems.TypeExtraLaserWeaponsCoolant4 = 5034;
		PlayerItems.TypeExtraLaserWeaponsCoolant5 = 5035;
		PlayerItems.TypeExtraLaserWeaponsModule1 = 5036;
		PlayerItems.TypeExtraLaserWeaponsModule2 = 5037;
		PlayerItems.TypeExtraLaserWeaponsModule3 = 5038;
		PlayerItems.TypeExtraLaserWeaponsModule4 = 5039;
		PlayerItems.TypeExtraLaserWeaponsModule5 = 5040;
		PlayerItems.TypeExtraPlasmaWeaponsCoolant1 = 5041;
		PlayerItems.TypeExtraPlasmaWeaponsCoolant2 = 5042;
		PlayerItems.TypeExtraPlasmaWeaponsCoolant3 = 5043;
		PlayerItems.TypeExtraPlasmaWeaponsCoolant4 = 5044;
		PlayerItems.TypeExtraPlasmaWeaponsCoolant5 = 5045;
		PlayerItems.TypeExtraPlasmaWeaponsModule1 = 5046;
		PlayerItems.TypeExtraPlasmaWeaponsModule2 = 5047;
		PlayerItems.TypeExtraPlasmaWeaponsModule3 = 5048;
		PlayerItems.TypeExtraPlasmaWeaponsModule4 = 5049;
		PlayerItems.TypeExtraPlasmaWeaponsModule5 = 5050;
		PlayerItems.TypeExtraCPUforShildRegen30 = 5051;
		PlayerItems.TypeExtraCPUforShildRegen40 = 5052;
		PlayerItems.TypeExtraCPUforShildRegen50 = 5053;
		PlayerItems.TypeExtraCPUforShildRegen60 = 5054;
		PlayerItems.TypeExtraCPUforShildRegen70 = 5055;
		PlayerItems.TypeBoosterDamageFor3Days = 7000;
		PlayerItems.TypeBoosterCargoFor3Days = 7001;
		PlayerItems.TypeBoosterExperienceFor3Days = 7002;
		PlayerItems.TypeBoosterAutominerFor3Days = 7003;
		PlayerItems.TypeBoosterDamageFor1Days = 7010;
		PlayerItems.TypeBoosterCargoFor1Days = 7011;
		PlayerItems.TypeBoosterExperienceFor1Days = 7012;
		PlayerItems.TypeBoosterAutominerFor1Days = 7013;
		PlayerItems.TypeBoosterDamageFor6Days = 7020;
		PlayerItems.TypeBoosterCargoFor6Days = 7021;
		PlayerItems.TypeBoosterExperienceFor6Days = 7022;
		PlayerItems.TypeBoosterAutominerFor6Days = 7023;
		PlayerItems.TypeBoosterPackageDeal = 7101;
		PlayerItems.TypePowerUpForLaserDamageFlat = 7500;
		PlayerItems.TypePowerUpForPlasmaDamageFlat = 7501;
		PlayerItems.TypePowerUpForIonDamageFlat = 7502;
		PlayerItems.TypePowerUpForLaserDamagePercentage = 7503;
		PlayerItems.TypePowerUpForPlasmaDamagePercentage = 7504;
		PlayerItems.TypePowerUpForIonDamagePercentage = 7505;
		PlayerItems.TypePowerUpForTotalDamagePercentage = 7506;
		PlayerItems.TypePowerUpForCorpusFlat = 7507;
		PlayerItems.TypePowerUpForCorpusPercentage = 7508;
		PlayerItems.TypePowerUpForShieldFlat = 7509;
		PlayerItems.TypePowerUpForShieldPercentage = 7510;
		PlayerItems.TypePowerUpForEndurancePercentage = 7511;
		PlayerItems.TypePowerUpForShieldPowerFlat = 7512;
		PlayerItems.TypePowerUpForShieldPowerPercentage = 7513;
		PlayerItems.TypePowerUpForTargetingFlat = 7514;
		PlayerItems.TypePowerUpForTargetingPercentage = 7515;
		PlayerItems.TypePowerUpForAvoidanceFlat = 7516;
		PlayerItems.TypePowerUpForAvoidancePercentage = 7517;
		PlayerItems.TypePowerUpDamagePackageDeal = 7601;
		PlayerItems.TypePowerUpShieldPackageDeal = 7602;
		PlayerItems.TypePowerUpCorpusPackageDeal = 7603;
		PlayerItems.TypePowerUpShieldPowerPackageDeal = 7604;
		PlayerItems.TypePowerUpTargetingPackageDeal = 7605;
		PlayerItems.TypePowerUpAvoidancePackageDeal = 7606;
		PlayerItems.TypeTalentsSunderArmor = 8001;
		PlayerItems.TypeTalentsDefiance = 8002;
		PlayerItems.TypeTalentsTaunt = 8003;
		PlayerItems.TypeTalentsRocketBarrage = 8004;
		PlayerItems.TypeTalentsAdvancedCorpus = 8005;
		PlayerItems.TypeTalentsFocusFire = 8006;
		PlayerItems.TypeTalentsAdvancedShield = 8007;
		PlayerItems.TypeTalentsUnstoppable = 8008;
		PlayerItems.TypeTalentsForceWave = 8009;
		PlayerItems.TypeTalentsShieldFortress = 8010;
		PlayerItems.TypeTalentsLaserDestruction = 8011;
		PlayerItems.TypeTalentsFindWeakSpot = 8012;
		PlayerItems.TypeTalentsStealth = 8013;
		PlayerItems.TypeTalentsDecoy = 8014;
		PlayerItems.TypeTalentsWeaponMastery = 8015;
		PlayerItems.TypeTalentsPowerBreak = 8016;
		PlayerItems.TypeTalentsPowerCut = 8017;
		PlayerItems.TypeTalentsEngineBooster = 8018;
		PlayerItems.TypeTalentsLightSpeed = 8019;
		PlayerItems.TypeTalentsMistShroud = 8020;
		PlayerItems.TypeTalentsRepairingDrones = 8021;
		PlayerItems.TypeTalentsImprovedShield = 8022;
		PlayerItems.TypeTalentsNanoStorm = 8023;
		PlayerItems.TypeTalentsNanoShield = 8024;
		PlayerItems.TypeTalentsRepairField = 8025;
		PlayerItems.TypeTalentsEmpoweredShield = 8026;
		PlayerItems.TypeTalentsShortCircuit = 8027;
		PlayerItems.TypeTalentsNanoTechnology = 8028;
		PlayerItems.TypeTalentsPulseNova = 8029;
		PlayerItems.TypeTalentsRemedy = 8030;
		PlayerItems.TypeTalentsArchiver = 8031;
		PlayerItems.TypeTalentsMerchant = 8032;
		PlayerItems.TypeTalentsSteadyAim = 8033;
		PlayerItems.TypeTalentsSwiftLearner = 8034;
		PlayerItems.TypeTalentsAlienSpecialist = 8035;
		PlayerItems.TypeTalentsVelocity = 8036;
		PlayerItems.TypeTalentsBountySpecialist = 8037;
		PlayerItems.TypeTalentsDamageReduction = 8038;
		PlayerItems.TypeTalentsArmorBreaker = 8039;
		PlayerItems.TypeTalentsRealSteel = 8040;
		PlayerItems.TypeTalentsRocketeer = 8041;
		PlayerItems.TypeTalentsPowerControl = 8042;
		PlayerItems.TypeTalentsMassiveDamage = 8043;
		PlayerItems.TypeTalentsSpeedster = 8044;
		PlayerItems.TypeTalentsFutureTechnology = 8045;
		PlayerItems.TypeTalentsDronePower = 8046;
		PlayerItems.TypeTalentsRepairMaster = 8047;
		PlayerItems.TypeCouncilSkillDisarm = 8101;
		PlayerItems.TypeCouncilSkillSacrifice = 8102;
		PlayerItems.TypeCouncilSkillLifesteal = 8103;
		PlayerItems.TypeNeuronsForArmorBreaker = 9039;
		PlayerItems.TypeNeuronsForRealSteel = 9040;
		PlayerItems.TypeNeuronsForRocketeer = 9041;
		PlayerItems.TypeNeuronsForPowerControl = 9042;
		PlayerItems.TypeNeuronsForMassiveDamage = 9043;
		PlayerItems.TypeNeuronsForSpeedster = 9044;
		PlayerItems.TypeNeuronsForFutureTechnology = 9045;
		PlayerItems.TypeNeuronsForDronePower = 9046;
		PlayerItems.TypeNeuronsForRepairMaster = 9047;
		PlayerItems.TypeNeuronsForSunderArmor = 9001;
		PlayerItems.TypeNeuronsForDefiance = 9002;
		PlayerItems.TypeNeuronsForTaunt = 9003;
		PlayerItems.TypeNeuronsForRocketBarrage = 9004;
		PlayerItems.TypeNeuronsForAdvancedCorpus = 9005;
		PlayerItems.TypeNeuronsForFocusFire = 9006;
		PlayerItems.TypeNeuronsForAdvancedShield = 9007;
		PlayerItems.TypeNeuronsForUnstoppable = 9008;
		PlayerItems.TypeNeuronsForForceWave = 9009;
		PlayerItems.TypeNeuronsForShieldFortress = 9010;
		PlayerItems.TypeNeuronsForLaserDestruction = 9011;
		PlayerItems.TypeNeuronsForFindWeakSpot = 9012;
		PlayerItems.TypeNeuronsForStealth = 9013;
		PlayerItems.TypeNeuronsForDecoy = 9014;
		PlayerItems.TypeNeuronsForWeaponMastery = 9015;
		PlayerItems.TypeNeuronsForPowerBreak = 9016;
		PlayerItems.TypeNeuronsForPowerCut = 9017;
		PlayerItems.TypeNeuronsForEngineBooster = 9018;
		PlayerItems.TypeNeuronsForLightSpeed = 9019;
		PlayerItems.TypeNeuronsForMistShroud = 9020;
		PlayerItems.TypeNeuronsForRepairingDrones = 9021;
		PlayerItems.TypeNeuronsForImprovedShield = 9022;
		PlayerItems.TypeNeuronsForNanoStorm = 9023;
		PlayerItems.TypeNeuronsForNanoShield = 9024;
		PlayerItems.TypeNeuronsForRepairField = 9025;
		PlayerItems.TypeNeuronsForEmpoweredShield = 9026;
		PlayerItems.TypeNeuronsForShortCircuit = 9027;
		PlayerItems.TypeNeuronsForNanoTechnology = 9028;
		PlayerItems.TypeNeuronsForPulseNova = 9029;
		PlayerItems.TypeNeuronsForRemedy = 9030;
		PlayerItems.TypeNeuronsForArchiver = 9031;
		PlayerItems.TypeNeuronsForMerchant = 9032;
		PlayerItems.TypeNeuronsForSteadyAim = 9033;
		PlayerItems.TypeNeuronsForSwiftLearner = 9034;
		PlayerItems.TypeNeuronsForAlienSpecialist = 9035;
		PlayerItems.TypeNeuronsForVelocity = 9036;
		PlayerItems.TypeNeuronsForBountySpecialist = 9037;
		PlayerItems.TypeNeuronsForDamageReduction = 9038;
		PlayerItems.TypeUltraGalaxy1PortalSchematic = 12010;
		PlayerItems.TypeUltraGalaxy1RunicKey1 = 12011;
		PlayerItems.TypeUltraGalaxy1RunicKey2 = 12012;
		PlayerItems.TypeUltraGalaxy1RunicKey3 = 12013;
		PlayerItems.TypeUltraGalaxy1PortalPillar = 12014;
		PlayerItems.TypeUltraGalaxy1PortalCharger = 12015;
		PlayerItems.TypeUltraGalaxy1PortalSegment = 12016;
		PlayerItems.TypeUltraGalaxy2PortalSchematic = 12020;
		PlayerItems.TypeUltraGalaxy2RunicKey1 = 12021;
		PlayerItems.TypeUltraGalaxy2RunicKey2 = 12022;
		PlayerItems.TypeUltraGalaxy2RunicKey3 = 12023;
		PlayerItems.TypeUltraGalaxy2PortalPillar = 12024;
		PlayerItems.TypeUltraGalaxy2PortalCharger = 12025;
		PlayerItems.TypeUltraGalaxy2PortalSegment = 12026;
		PlayerItems.TypeUltraGalaxy3PortalSchematic = 12030;
		PlayerItems.TypeUltraGalaxy3RunicKey1 = 12031;
		PlayerItems.TypeUltraGalaxy3RunicKey2 = 12032;
		PlayerItems.TypeUltraGalaxy3RunicKey3 = 12033;
		PlayerItems.TypeUltraGalaxy3PortalPillar = 12034;
		PlayerItems.TypeUltraGalaxy3PortalCharger = 12035;
		PlayerItems.TypeUltraGalaxy3PortalSegment = 12036;
		PlayerItems.TypeQuestItem01 = 10000;
		PlayerItems.TypeQuestItem02 = 10001;
		PlayerItems.TypeQuestItem03 = 10002;
		PlayerItems.TypeQuestItem04 = 10003;
		PlayerItems.TypeQuestItem05 = 10004;
		PlayerItems.TypeQuestItem06 = 10005;
		PlayerItems.TypeQuestItem07 = 10006;
		PlayerItems.TypeQuestItem08 = 10007;
		PlayerItems.TypeQuestItem09 = 10008;
		PlayerItems.TypeQuestItem10 = 10009;
		PlayerItems.TypeQuestItem11 = 10010;
		PlayerItems.TypeQuestItem12 = 10011;
		PlayerItems.TypeQuestItem13 = 10012;
		PlayerItems.TypeQuestItem14 = 10013;
		PlayerItems.TypeQuestItem15 = 10014;
		PlayerItems.TypeQuestItem16 = 10015;
		PlayerItems.TypeQuestItem17 = 10016;
		PlayerItems.TypeQuestItem18 = 10017;
		PlayerItems.TypeQuestItem19 = 10018;
		PlayerItems.TypeQuestItem20 = 10019;
		PlayerItems.TypeQuestItem21 = 10020;
		PlayerItems.TypeQuestItem22 = 10021;
		PlayerItems.TypeQuestItem23 = 10022;
		PlayerItems.TypeQuestItem24 = 10023;
		PlayerItems.TypeQuestItem25 = 10024;
		PlayerItems.TypeQuestItem26 = 10025;
		PlayerItems.TypeQuestItem27 = 10026;
		PlayerItems.TypeQuestItem28 = 10027;
		PlayerItems.TypeQuestItem29 = 10028;
		PlayerItems.TypeQuestItem30 = 10029;
		PlayerItems.guardianSkills = new SortedList<int, List<ushort>>();
		SortedList<int, List<ushort>> nums = PlayerItems.guardianSkills;
		List<ushort> nums1 = new List<ushort>()
		{
			PlayerItems.TypeTalentsSunderArmor,
			PlayerItems.TypeTalentsDefiance
		};
		nums.Add(1, nums1);
		SortedList<int, List<ushort>> nums2 = PlayerItems.guardianSkills;
		List<ushort> nums3 = new List<ushort>()
		{
			PlayerItems.TypeTalentsTaunt,
			PlayerItems.TypeTalentsRocketBarrage
		};
		nums2.Add(2, nums3);
		SortedList<int, List<ushort>> nums4 = PlayerItems.guardianSkills;
		List<ushort> nums5 = new List<ushort>()
		{
			PlayerItems.TypeTalentsAdvancedCorpus,
			PlayerItems.TypeTalentsFocusFire
		};
		nums4.Add(3, nums5);
		SortedList<int, List<ushort>> nums6 = PlayerItems.guardianSkills;
		List<ushort> nums7 = new List<ushort>()
		{
			PlayerItems.TypeTalentsAdvancedShield,
			PlayerItems.TypeTalentsUnstoppable
		};
		nums6.Add(4, nums7);
		PlayerItems.guardianSkills.Add(5, new List<ushort>()
		{
			PlayerItems.TypeTalentsPowerCut
		});
		PlayerItems.guardianSkills.Add(6, new List<ushort>()
		{
			PlayerItems.TypeTalentsShieldFortress
		});
		PlayerItems.destroyerSkills = new SortedList<int, List<ushort>>();
		SortedList<int, List<ushort>> nums8 = PlayerItems.destroyerSkills;
		List<ushort> nums9 = new List<ushort>()
		{
			PlayerItems.TypeTalentsLaserDestruction,
			PlayerItems.TypeTalentsFindWeakSpot
		};
		nums8.Add(1, nums9);
		SortedList<int, List<ushort>> nums10 = PlayerItems.destroyerSkills;
		List<ushort> nums11 = new List<ushort>()
		{
			PlayerItems.TypeTalentsStealth,
			PlayerItems.TypeTalentsDecoy
		};
		nums10.Add(2, nums11);
		SortedList<int, List<ushort>> nums12 = PlayerItems.destroyerSkills;
		List<ushort> nums13 = new List<ushort>()
		{
			PlayerItems.TypeTalentsEngineBooster,
			PlayerItems.TypeTalentsPowerBreak
		};
		nums12.Add(3, nums13);
		SortedList<int, List<ushort>> nums14 = PlayerItems.destroyerSkills;
		List<ushort> nums15 = new List<ushort>()
		{
			PlayerItems.TypeTalentsLightSpeed,
			PlayerItems.TypeTalentsWeaponMastery
		};
		nums14.Add(4, nums15);
		PlayerItems.destroyerSkills.Add(5, new List<ushort>()
		{
			PlayerItems.TypeTalentsForceWave
		});
		PlayerItems.destroyerSkills.Add(6, new List<ushort>()
		{
			PlayerItems.TypeTalentsMistShroud
		});
		PlayerItems.protectorSkills = new SortedList<int, List<ushort>>();
		SortedList<int, List<ushort>> nums16 = PlayerItems.protectorSkills;
		List<ushort> nums17 = new List<ushort>()
		{
			PlayerItems.TypeTalentsRepairingDrones,
			PlayerItems.TypeTalentsImprovedShield
		};
		nums16.Add(1, nums17);
		SortedList<int, List<ushort>> nums18 = PlayerItems.protectorSkills;
		List<ushort> nums19 = new List<ushort>()
		{
			PlayerItems.TypeTalentsNanoStorm,
			PlayerItems.TypeTalentsNanoShield
		};
		nums18.Add(2, nums19);
		SortedList<int, List<ushort>> nums20 = PlayerItems.protectorSkills;
		List<ushort> nums21 = new List<ushort>()
		{
			PlayerItems.TypeTalentsRepairField,
			PlayerItems.TypeTalentsEmpoweredShield
		};
		nums20.Add(3, nums21);
		SortedList<int, List<ushort>> nums22 = PlayerItems.protectorSkills;
		List<ushort> nums23 = new List<ushort>()
		{
			PlayerItems.TypeTalentsShortCircuit,
			PlayerItems.TypeTalentsNanoTechnology
		};
		nums22.Add(4, nums23);
		PlayerItems.protectorSkills.Add(5, new List<ushort>()
		{
			PlayerItems.TypeTalentsPulseNova
		});
		PlayerItems.protectorSkills.Add(6, new List<ushort>()
		{
			PlayerItems.TypeTalentsRemedy
		});
		PlayerItems.passiveSkills = new SortedList<int, List<ushort>>();
		SortedList<int, List<ushort>> nums24 = PlayerItems.passiveSkills;
		List<ushort> nums25 = new List<ushort>()
		{
			PlayerItems.TypeTalentsArchiver,
			PlayerItems.TypeTalentsMerchant,
			PlayerItems.TypeTalentsSteadyAim
		};
		nums24.Add(1, nums25);
		SortedList<int, List<ushort>> nums26 = PlayerItems.passiveSkills;
		List<ushort> nums27 = new List<ushort>()
		{
			PlayerItems.TypeTalentsSwiftLearner,
			PlayerItems.TypeTalentsAlienSpecialist
		};
		nums26.Add(2, nums27);
		SortedList<int, List<ushort>> nums28 = PlayerItems.passiveSkills;
		List<ushort> nums29 = new List<ushort>()
		{
			PlayerItems.TypeTalentsVelocity,
			PlayerItems.TypeTalentsBountySpecialist
		};
		nums28.Add(3, nums29);
		PlayerItems.passiveSkills.Add(4, new List<ushort>()
		{
			PlayerItems.TypeTalentsDamageReduction
		});
		PlayerItems.amplificationSkillsOne = new SortedList<int, List<ushort>>()
		{
			{ 1, new List<ushort>()
			{
				PlayerItems.TypeTalentsArmorBreaker
			} },
			{ 2, new List<ushort>()
			{
				PlayerItems.TypeTalentsRealSteel
			} },
			{ 3, new List<ushort>()
			{
				PlayerItems.TypeTalentsRocketeer
			} }
		};
		PlayerItems.amplificationSkillsTwo = new SortedList<int, List<ushort>>()
		{
			{ 1, new List<ushort>()
			{
				PlayerItems.TypeTalentsPowerControl
			} },
			{ 2, new List<ushort>()
			{
				PlayerItems.TypeTalentsMassiveDamage
			} },
			{ 3, new List<ushort>()
			{
				PlayerItems.TypeTalentsSpeedster
			} }
		};
		PlayerItems.amplificationSkillsThree = new SortedList<int, List<ushort>>()
		{
			{ 1, new List<ushort>()
			{
				PlayerItems.TypeTalentsFutureTechnology
			} },
			{ 2, new List<ushort>()
			{
				PlayerItems.TypeTalentsDronePower
			} },
			{ 3, new List<ushort>()
			{
				PlayerItems.TypeTalentsRepairMaster
			} }
		};
		ushort[] typeCarbon = new ushort[] { PlayerItems.TypeCarbon, PlayerItems.TypeHydrogen, PlayerItems.TypeDeuterium, PlayerItems.TypeOxygen, PlayerItems.TypeAceton, PlayerItems.TypeCarbonDioxide, PlayerItems.TypeMetyl, PlayerItems.TypeWater };
		PlayerItems.cargoTypes = typeCarbon;
		typeCarbon = new ushort[] { PlayerItems.TypeAmmoSolarCells, PlayerItems.TypeAmmoFusionCells, PlayerItems.TypeAmmoColdFusionCells, PlayerItems.TypeAmmoSulfurFusionCells };
		PlayerItems.ammoTypes = typeCarbon;
		typeCarbon = new ushort[] { PlayerItems.TypeTalentsArchiver, PlayerItems.TypeTalentsMerchant, PlayerItems.TypeTalentsSteadyAim, PlayerItems.TypeTalentsSwiftLearner, PlayerItems.TypeTalentsAlienSpecialist, PlayerItems.TypeTalentsVelocity, PlayerItems.TypeTalentsBountySpecialist, PlayerItems.TypeTalentsDamageReduction };
		PlayerItems.abilityTypesPassive = new List<ushort>(typeCarbon);
		typeCarbon = new ushort[] { PlayerItems.TypeTalentsRepairingDrones, PlayerItems.TypeTalentsImprovedShield, PlayerItems.TypeTalentsNanoStorm, PlayerItems.TypeTalentsNanoShield, PlayerItems.TypeTalentsRepairField, PlayerItems.TypeTalentsEmpoweredShield, PlayerItems.TypeTalentsShortCircuit, PlayerItems.TypeTalentsNanoTechnology, PlayerItems.TypeTalentsPulseNova, PlayerItems.TypeTalentsRemedy };
		PlayerItems.abilityTypesProtector = new List<ushort>(typeCarbon);
		typeCarbon = new ushort[] { PlayerItems.TypeTalentsSunderArmor, PlayerItems.TypeTalentsDefiance, PlayerItems.TypeTalentsTaunt, PlayerItems.TypeTalentsRocketBarrage, PlayerItems.TypeTalentsAdvancedCorpus, PlayerItems.TypeTalentsFocusFire, PlayerItems.TypeTalentsAdvancedShield, PlayerItems.TypeTalentsUnstoppable, PlayerItems.TypeTalentsPowerCut, PlayerItems.TypeTalentsShieldFortress };
		PlayerItems.abilityTypesGuardian = new List<ushort>(typeCarbon);
		typeCarbon = new ushort[] { PlayerItems.TypeTalentsLaserDestruction, PlayerItems.TypeTalentsFindWeakSpot, PlayerItems.TypeTalentsStealth, PlayerItems.TypeTalentsDecoy, PlayerItems.TypeTalentsEngineBooster, PlayerItems.TypeTalentsPowerBreak, PlayerItems.TypeTalentsLightSpeed, PlayerItems.TypeTalentsWeaponMastery, PlayerItems.TypeTalentsLightSpeed, PlayerItems.TypeTalentsForceWave, PlayerItems.TypeTalentsMistShroud };
		PlayerItems.abilityTypesDestroyer = new List<ushort>(typeCarbon);
		typeCarbon = new ushort[] { PlayerItems.TypeTalentsArmorBreaker, PlayerItems.TypeTalentsRealSteel, PlayerItems.TypeTalentsRocketeer, PlayerItems.TypeTalentsPowerControl, PlayerItems.TypeTalentsMassiveDamage, PlayerItems.TypeTalentsSpeedster, PlayerItems.TypeTalentsFutureTechnology, PlayerItems.TypeTalentsDronePower, PlayerItems.TypeTalentsRepairMaster };
		PlayerItems.abilityTypesAmplification = new List<ushort>(typeCarbon);
		List<ushort> nums30 = new List<ushort>()
		{
			10,
			11,
			12,
			13,
			14,
			15,
			20,
			21,
			22,
			25,
			53,
			54,
			55,
			56,
			57,
			58,
			100,
			900
		};
		PlayerItems.genericItemsTypes = nums30;
		PlayerItems.ammoItemId2DbMapping = new SortedList<ushort, int>()
		{
			{ PlayerItems.TypeAmmoSolarCells, 4 },
			{ PlayerItems.TypeAmmoFusionCells, 5 },
			{ PlayerItems.TypeAmmoColdFusionCells, 6 },
			{ PlayerItems.TypeAmmoSulfurFusionCells, 7 }
		};
		PlayerItems.categoriesMapping = new SortedList<byte, ushort[]>();
		SortedList<byte, ushort[]> nums31 = PlayerItems.categoriesMapping;
		typeCarbon = new ushort[] { PlayerItems.TypeAmmoSolarCells, PlayerItems.TypeAmmoFusionCells, PlayerItems.TypeAmmoColdFusionCells, PlayerItems.TypeAmmoSulfurFusionCells };
		nums31.Add(1, typeCarbon);
		SortedList<byte, ushort[]> nums32 = PlayerItems.categoriesMapping;
		typeCarbon = new ushort[] { PlayerItems.TypeWeaponLaserTire1, PlayerItems.TypeWeaponLaserTire2, PlayerItems.TypeWeaponLaserTire3, PlayerItems.TypeWeaponLaserTire4, PlayerItems.TypeWeaponLaserTire5, PlayerItems.TypeWeaponPlasmaTire1, PlayerItems.TypeWeaponPlasmaTire2, PlayerItems.TypeWeaponPlasmaTire3, PlayerItems.TypeWeaponPlasmaTire4, PlayerItems.TypeWeaponPlasmaTire5, PlayerItems.TypeWeaponIonTire1, PlayerItems.TypeWeaponIonTire2, PlayerItems.TypeWeaponIonTire3, PlayerItems.TypeWeaponIonTire4, PlayerItems.TypeWeaponIonTire5 };
		nums32.Add(2, typeCarbon);
		SortedList<byte, ushort[]> nums33 = PlayerItems.categoriesMapping;
		typeCarbon = new ushort[] { PlayerItems.TypeEngineJet1, PlayerItems.TypeEngineJet2, PlayerItems.TypeEngineJet3, PlayerItems.TypeEngineHyper1, PlayerItems.TypeEngineHyper2, PlayerItems.TypeEngineHyper3, PlayerItems.TypeEngineHyper4, PlayerItems.TypeEngineHyper5, PlayerItems.TypeEngineHyper6, PlayerItems.TypeEngineHyper7, PlayerItems.TypeEngineUltra1, PlayerItems.TypeEngineUltra2 };
		nums33.Add(3, typeCarbon);
		SortedList<byte, ushort[]> nums34 = PlayerItems.categoriesMapping;
		typeCarbon = new ushort[] { PlayerItems.TypeShieldMinor, PlayerItems.TypeShieldMinor2, PlayerItems.TypeShieldBasic, PlayerItems.TypeShieldBasic2, PlayerItems.TypeShieldLight, PlayerItems.TypeShieldLight2, PlayerItems.TypeShieldMedium, PlayerItems.TypeShieldMedium2, PlayerItems.TypeShieldHeavy, PlayerItems.TypeShieldHeavy2, PlayerItems.TypeShieldHeavy3, PlayerItems.TypeShieldLightEmp, PlayerItems.TypeShieldLightEmp2, PlayerItems.TypeShieldLightEmp3, PlayerItems.TypeShieldMedEmp, PlayerItems.TypeShieldMedEmp2, PlayerItems.TypeShieldMedEmp3, PlayerItems.TypeShieldMedEmp4, PlayerItems.TypeShieldAdvEmp, PlayerItems.TypeShieldAdvEmp2, PlayerItems.TypeShieldAdvEmp3, PlayerItems.TypeShieldAdvEmp4, PlayerItems.TypeShieldHeavyEmp, PlayerItems.TypeShieldHeavyEmp2, PlayerItems.TypeShieldHeavyEmp3, PlayerItems.TypeShieldHeavyEmp4, PlayerItems.TypeShieldHeavyEmp5, PlayerItems.TypeShieldHeavyEmp6, PlayerItems.TypeShieldHeavyEmp7, PlayerItems.TypeShieldThorium, PlayerItems.TypeShieldNeutronProtector, PlayerItems.TypeShieldParticleBeam, PlayerItems.TypeShieldIonField };
		nums34.Add(4, typeCarbon);
		SortedList<byte, ushort[]> nums35 = PlayerItems.categoriesMapping;
		typeCarbon = new ushort[] { PlayerItems.TypeExtraMolecularCompresor, PlayerItems.TypeExtraNuclearCompresor, PlayerItems.TypeExtraFusionCompresor, PlayerItems.TypeExtraLaserWeaponsModule, PlayerItems.TypeExtraPlasmaWeaponsModule, PlayerItems.TypeExtraIonWeaponsModule, PlayerItems.TypeExtraUltraWeaponsModule, PlayerItems.TypeExtraLaserAimingCPU, PlayerItems.TypeExtraPlasmaAimingCPU, PlayerItems.TypeExtraIonAimingCPU, PlayerItems.TypeExtraUltraAimingCPU, PlayerItems.TypeExtraLightMiningDrill, PlayerItems.TypeExtraUltraMiningDrill, PlayerItems.TypeExtraLaserWeaponsCoolant, PlayerItems.TypeExtraPlasmaWeaponsCoolant, PlayerItems.TypeExtraIonWeaponsCoolant, PlayerItems.TypeExtraUltraWeaponsCoolant, PlayerItems.TypeExtraBasicCPUforShildRegen, PlayerItems.TypeExtraAdvancedCPUforShildRegen, PlayerItems.TypeExtraOverclockedCPUforShildRegen, PlayerItems.TypeExtraUltraNuclearCompresor, PlayerItems.TypeExtraLaserWeaponsModule1, PlayerItems.TypeExtraLaserWeaponsModule2, PlayerItems.TypeExtraLaserWeaponsModule3, PlayerItems.TypeExtraLaserWeaponsModule4, PlayerItems.TypeExtraLaserWeaponsModule5, PlayerItems.TypeExtraPlasmaWeaponsModule1, PlayerItems.TypeExtraPlasmaWeaponsModule2, PlayerItems.TypeExtraPlasmaWeaponsModule3, PlayerItems.TypeExtraPlasmaWeaponsModule4, PlayerItems.TypeExtraPlasmaWeaponsModule5, PlayerItems.TypeExtraIonWeaponsModule1, PlayerItems.TypeExtraIonWeaponsModule2, PlayerItems.TypeExtraIonWeaponsModule3, PlayerItems.TypeExtraIonWeaponsModule4, PlayerItems.TypeExtraIonWeaponsModule5, PlayerItems.TypeExtraLaserWeaponsCoolant1, PlayerItems.TypeExtraLaserWeaponsCoolant2, PlayerItems.TypeExtraLaserWeaponsCoolant3, PlayerItems.TypeExtraLaserWeaponsCoolant4, PlayerItems.TypeExtraLaserWeaponsCoolant5, PlayerItems.TypeExtraPlasmaWeaponsCoolant1, PlayerItems.TypeExtraPlasmaWeaponsCoolant2, PlayerItems.TypeExtraPlasmaWeaponsCoolant3, PlayerItems.TypeExtraPlasmaWeaponsCoolant4, PlayerItems.TypeExtraPlasmaWeaponsCoolant5, PlayerItems.TypeExtraIonWeaponsCoolant1, PlayerItems.TypeExtraIonWeaponsCoolant2, PlayerItems.TypeExtraIonWeaponsCoolant3, PlayerItems.TypeExtraIonWeaponsCoolant4, PlayerItems.TypeExtraIonWeaponsCoolant5, PlayerItems.TypeExtraCPUforShildRegen30, PlayerItems.TypeExtraCPUforShildRegen40, PlayerItems.TypeExtraCPUforShildRegen50, PlayerItems.TypeExtraCPUforShildRegen60, PlayerItems.TypeExtraCPUforShildRegen70 };
		nums35.Add(5, typeCarbon);
		SortedList<byte, ushort[]> nums36 = PlayerItems.categoriesMapping;
		typeCarbon = new ushort[] { PlayerItems.TypeCorpusMinor, PlayerItems.TypeCorpusMinor2, PlayerItems.TypeCorpusBasic, PlayerItems.TypeCorpusBasic2, PlayerItems.TypeCorpusLight, PlayerItems.TypeCorpusLight2, PlayerItems.TypeCorpusLightSteel, PlayerItems.TypeCorpusLightSteel2, PlayerItems.TypeCorpusSteel, PlayerItems.TypeCorpusSteel2, PlayerItems.TypeCorpusSteel3, PlayerItems.TypeCorpusHeavySteel, PlayerItems.TypeCorpusHeavySteel2, PlayerItems.TypeCorpusHeavySteel3, PlayerItems.TypeCorpusLightAllum, PlayerItems.TypeCorpusLightAllum2, PlayerItems.TypeCorpusLightAllum3, PlayerItems.TypeCorpusLightAllum4, PlayerItems.TypeCorpusMedAllum, PlayerItems.TypeCorpusMedAllum2, PlayerItems.TypeCorpusMedAllum3, PlayerItems.TypeCorpusMedAllum4, PlayerItems.TypeCorpusHeavyAllum, PlayerItems.TypeCorpusHeavyAllum2, PlayerItems.TypeCorpusHeavyAllum3, PlayerItems.TypeCorpusHeavyAllum4, PlayerItems.TypeCorpusHeavyAllum5, PlayerItems.TypeCorpusHeavyAllum6, PlayerItems.TypeCorpusHeavyAllum7, PlayerItems.TypeCorpusAllumShell, PlayerItems.TypeCorpusDefender, PlayerItems.TypeCorpusTitanium, PlayerItems.TypeCorpusAegis };
		nums36.Add(6, typeCarbon);
		PlayerItems.item2categoryMapping = new SortedList<ushort, byte>()
		{
			{ PlayerItems.TypeAmmoSolarCells, 1 },
			{ PlayerItems.TypeAmmoFusionCells, 1 },
			{ PlayerItems.TypeAmmoColdFusionCells, 1 },
			{ PlayerItems.TypeAmmoSulfurFusionCells, 1 },
			{ PlayerItems.TypeWeaponLaserTire1, 2 },
			{ PlayerItems.TypeWeaponLaserTire2, 2 },
			{ PlayerItems.TypeWeaponLaserTire3, 2 },
			{ PlayerItems.TypeWeaponLaserTire4, 2 },
			{ PlayerItems.TypeWeaponLaserTire5, 2 },
			{ PlayerItems.TypeWeaponPlasmaTire1, 2 },
			{ PlayerItems.TypeWeaponPlasmaTire2, 2 },
			{ PlayerItems.TypeWeaponPlasmaTire3, 2 },
			{ PlayerItems.TypeWeaponPlasmaTire4, 2 },
			{ PlayerItems.TypeWeaponPlasmaTire5, 2 },
			{ PlayerItems.TypeWeaponIonTire1, 2 },
			{ PlayerItems.TypeWeaponIonTire2, 2 },
			{ PlayerItems.TypeWeaponIonTire3, 2 },
			{ PlayerItems.TypeWeaponIonTire4, 2 },
			{ PlayerItems.TypeWeaponIonTire5, 2 },
			{ PlayerItems.TypeEngineHyper1, 3 },
			{ PlayerItems.TypeEngineHyper2, 3 },
			{ PlayerItems.TypeEngineHyper3, 3 },
			{ PlayerItems.TypeEngineHyper4, 3 },
			{ PlayerItems.TypeEngineHyper5, 3 },
			{ PlayerItems.TypeEngineHyper6, 3 },
			{ PlayerItems.TypeEngineHyper7, 3 },
			{ PlayerItems.TypeEngineJet1, 3 },
			{ PlayerItems.TypeEngineJet2, 3 },
			{ PlayerItems.TypeEngineJet3, 3 },
			{ PlayerItems.TypeEngineUltra1, 3 },
			{ PlayerItems.TypeEngineUltra2, 3 },
			{ PlayerItems.TypeShieldMinor, 4 },
			{ PlayerItems.TypeShieldMinor2, 4 },
			{ PlayerItems.TypeShieldBasic, 4 },
			{ PlayerItems.TypeShieldBasic2, 4 },
			{ PlayerItems.TypeShieldLight, 4 },
			{ PlayerItems.TypeShieldLight2, 4 },
			{ PlayerItems.TypeShieldMedium, 4 },
			{ PlayerItems.TypeShieldMedium2, 4 },
			{ PlayerItems.TypeShieldHeavy, 4 },
			{ PlayerItems.TypeShieldHeavy2, 4 },
			{ PlayerItems.TypeShieldHeavy3, 4 },
			{ PlayerItems.TypeShieldLightEmp, 4 },
			{ PlayerItems.TypeShieldLightEmp2, 4 },
			{ PlayerItems.TypeShieldLightEmp3, 4 },
			{ PlayerItems.TypeShieldMedEmp, 4 },
			{ PlayerItems.TypeShieldMedEmp2, 4 },
			{ PlayerItems.TypeShieldMedEmp3, 4 },
			{ PlayerItems.TypeShieldMedEmp4, 4 },
			{ PlayerItems.TypeShieldAdvEmp, 4 },
			{ PlayerItems.TypeShieldAdvEmp2, 4 },
			{ PlayerItems.TypeShieldAdvEmp3, 4 },
			{ PlayerItems.TypeShieldAdvEmp4, 4 },
			{ PlayerItems.TypeShieldHeavyEmp, 4 },
			{ PlayerItems.TypeShieldHeavyEmp2, 4 },
			{ PlayerItems.TypeShieldHeavyEmp3, 4 },
			{ PlayerItems.TypeShieldHeavyEmp4, 4 },
			{ PlayerItems.TypeShieldHeavyEmp5, 4 },
			{ PlayerItems.TypeShieldHeavyEmp6, 4 },
			{ PlayerItems.TypeShieldHeavyEmp7, 4 },
			{ PlayerItems.TypeShieldThorium, 4 },
			{ PlayerItems.TypeShieldNeutronProtector, 4 },
			{ PlayerItems.TypeShieldParticleBeam, 4 },
			{ PlayerItems.TypeShieldIonField, 4 },
			{ PlayerItems.TypeCorpusMinor, 6 },
			{ PlayerItems.TypeCorpusMinor2, 6 },
			{ PlayerItems.TypeCorpusBasic, 6 },
			{ PlayerItems.TypeCorpusBasic2, 6 },
			{ PlayerItems.TypeCorpusLight, 6 },
			{ PlayerItems.TypeCorpusLight2, 6 },
			{ PlayerItems.TypeCorpusLightSteel, 6 },
			{ PlayerItems.TypeCorpusLightSteel2, 6 },
			{ PlayerItems.TypeCorpusSteel, 6 },
			{ PlayerItems.TypeCorpusSteel2, 6 },
			{ PlayerItems.TypeCorpusSteel3, 6 },
			{ PlayerItems.TypeCorpusHeavySteel, 6 },
			{ PlayerItems.TypeCorpusHeavySteel2, 6 },
			{ PlayerItems.TypeCorpusHeavySteel3, 6 },
			{ PlayerItems.TypeCorpusLightAllum, 6 },
			{ PlayerItems.TypeCorpusLightAllum2, 6 },
			{ PlayerItems.TypeCorpusLightAllum3, 6 },
			{ PlayerItems.TypeCorpusLightAllum4, 6 },
			{ PlayerItems.TypeCorpusMedAllum, 6 },
			{ PlayerItems.TypeCorpusMedAllum2, 6 },
			{ PlayerItems.TypeCorpusMedAllum3, 6 },
			{ PlayerItems.TypeCorpusMedAllum4, 6 },
			{ PlayerItems.TypeCorpusHeavyAllum, 6 },
			{ PlayerItems.TypeCorpusHeavyAllum2, 6 },
			{ PlayerItems.TypeCorpusHeavyAllum3, 6 },
			{ PlayerItems.TypeCorpusHeavyAllum4, 6 },
			{ PlayerItems.TypeCorpusHeavyAllum5, 6 },
			{ PlayerItems.TypeCorpusHeavyAllum6, 6 },
			{ PlayerItems.TypeCorpusHeavyAllum7, 6 },
			{ PlayerItems.TypeCorpusAllumShell, 6 },
			{ PlayerItems.TypeCorpusDefender, 6 },
			{ PlayerItems.TypeCorpusTitanium, 6 },
			{ PlayerItems.TypeCorpusAegis, 6 },
			{ PlayerItems.TypeExtraMolecularCompresor, 5 },
			{ PlayerItems.TypeExtraNuclearCompresor, 5 },
			{ PlayerItems.TypeExtraFusionCompresor, 5 },
			{ PlayerItems.TypeExtraLaserWeaponsModule, 5 },
			{ PlayerItems.TypeExtraPlasmaWeaponsModule, 5 },
			{ PlayerItems.TypeExtraIonWeaponsModule, 5 },
			{ PlayerItems.TypeExtraUltraWeaponsModule, 5 },
			{ PlayerItems.TypeExtraLaserAimingCPU, 5 },
			{ PlayerItems.TypeExtraPlasmaAimingCPU, 5 },
			{ PlayerItems.TypeExtraIonAimingCPU, 5 },
			{ PlayerItems.TypeExtraUltraAimingCPU, 5 },
			{ PlayerItems.TypeExtraLightMiningDrill, 5 },
			{ PlayerItems.TypeExtraUltraMiningDrill, 5 },
			{ PlayerItems.TypeExtraLaserWeaponsCoolant, 5 },
			{ PlayerItems.TypeExtraPlasmaWeaponsCoolant, 5 },
			{ PlayerItems.TypeExtraIonWeaponsCoolant, 5 },
			{ PlayerItems.TypeExtraUltraWeaponsCoolant, 5 },
			{ PlayerItems.TypeExtraBasicCPUforShildRegen, 5 },
			{ PlayerItems.TypeExtraAdvancedCPUforShildRegen, 5 },
			{ PlayerItems.TypeExtraOverclockedCPUforShildRegen, 5 },
			{ PlayerItems.TypeExtraUltraNuclearCompresor, 5 },
			{ PlayerItems.TypeExtraLaserWeaponsModule1, 5 },
			{ PlayerItems.TypeExtraLaserWeaponsModule2, 5 },
			{ PlayerItems.TypeExtraLaserWeaponsModule3, 5 },
			{ PlayerItems.TypeExtraLaserWeaponsModule4, 5 },
			{ PlayerItems.TypeExtraLaserWeaponsModule5, 5 },
			{ PlayerItems.TypeExtraPlasmaWeaponsModule1, 5 },
			{ PlayerItems.TypeExtraPlasmaWeaponsModule2, 5 },
			{ PlayerItems.TypeExtraPlasmaWeaponsModule3, 5 },
			{ PlayerItems.TypeExtraPlasmaWeaponsModule4, 5 },
			{ PlayerItems.TypeExtraPlasmaWeaponsModule5, 5 },
			{ PlayerItems.TypeExtraIonWeaponsModule1, 5 },
			{ PlayerItems.TypeExtraIonWeaponsModule2, 5 },
			{ PlayerItems.TypeExtraIonWeaponsModule3, 5 },
			{ PlayerItems.TypeExtraIonWeaponsModule4, 5 },
			{ PlayerItems.TypeExtraIonWeaponsModule5, 5 },
			{ PlayerItems.TypeExtraLaserWeaponsCoolant1, 5 },
			{ PlayerItems.TypeExtraLaserWeaponsCoolant2, 5 },
			{ PlayerItems.TypeExtraLaserWeaponsCoolant3, 5 },
			{ PlayerItems.TypeExtraLaserWeaponsCoolant4, 5 },
			{ PlayerItems.TypeExtraLaserWeaponsCoolant5, 5 },
			{ PlayerItems.TypeExtraPlasmaWeaponsCoolant1, 5 },
			{ PlayerItems.TypeExtraPlasmaWeaponsCoolant2, 5 },
			{ PlayerItems.TypeExtraPlasmaWeaponsCoolant3, 5 },
			{ PlayerItems.TypeExtraPlasmaWeaponsCoolant4, 5 },
			{ PlayerItems.TypeExtraPlasmaWeaponsCoolant5, 5 },
			{ PlayerItems.TypeExtraIonWeaponsCoolant1, 5 },
			{ PlayerItems.TypeExtraIonWeaponsCoolant2, 5 },
			{ PlayerItems.TypeExtraIonWeaponsCoolant3, 5 },
			{ PlayerItems.TypeExtraIonWeaponsCoolant4, 5 },
			{ PlayerItems.TypeExtraIonWeaponsCoolant5, 5 },
			{ PlayerItems.TypeExtraCPUforShildRegen30, 5 },
			{ PlayerItems.TypeExtraCPUforShildRegen40, 5 },
			{ PlayerItems.TypeExtraCPUforShildRegen50, 5 },
			{ PlayerItems.TypeExtraCPUforShildRegen60, 5 },
			{ PlayerItems.TypeExtraCPUforShildRegen70, 5 },
			{ PlayerItems.TypeUltraGalaxy1PortalSchematic, 7 },
			{ PlayerItems.TypeUltraGalaxy1RunicKey1, 7 },
			{ PlayerItems.TypeUltraGalaxy1RunicKey2, 7 },
			{ PlayerItems.TypeUltraGalaxy1RunicKey3, 7 },
			{ PlayerItems.TypeUltraGalaxy1PortalCharger, 7 },
			{ PlayerItems.TypeUltraGalaxy1PortalPillar, 7 },
			{ PlayerItems.TypeUltraGalaxy1PortalSegment, 7 },
			{ PlayerItems.TypeUltraGalaxy2PortalSchematic, 7 },
			{ PlayerItems.TypeUltraGalaxy2RunicKey1, 7 },
			{ PlayerItems.TypeUltraGalaxy2RunicKey2, 7 },
			{ PlayerItems.TypeUltraGalaxy2RunicKey3, 7 },
			{ PlayerItems.TypeUltraGalaxy2PortalCharger, 7 },
			{ PlayerItems.TypeUltraGalaxy2PortalPillar, 7 },
			{ PlayerItems.TypeUltraGalaxy2PortalSegment, 7 },
			{ PlayerItems.TypeUltraGalaxy3PortalSchematic, 7 },
			{ PlayerItems.TypeUltraGalaxy3RunicKey1, 7 },
			{ PlayerItems.TypeUltraGalaxy3RunicKey2, 7 },
			{ PlayerItems.TypeUltraGalaxy3RunicKey3, 7 },
			{ PlayerItems.TypeUltraGalaxy3PortalCharger, 7 },
			{ PlayerItems.TypeUltraGalaxy3PortalPillar, 7 },
			{ PlayerItems.TypeUltraGalaxy3PortalSegment, 7 }
		};
		PlayerItems.InitFusionDependancies();
		PlayerItems.InitSpecialAmounts();
		PlayerItems.InitSkillInformation();
	}

	public PlayerItems()
	{
	}

	public void Add(ushort key, long amount)
	{
		if (this.items.ContainsKey(key))
		{
			SortedList<ushort, long> item = this.items;
			SortedList<ushort, long> nums = item;
			ushort num = key;
			item[num] = nums[num] + amount;
		}
		else
		{
			this.items.Add(key, amount);
		}
	}

	public void AddCash(long val)
	{
		this.Add(PlayerItems.TypeCash, val);
	}

	public void AddEquilibrium(long val)
	{
		this.Add(PlayerItems.TypeEquilibrium, val);
	}

	public void AddHonor(long val)
	{
		this.Add(PlayerItems.TypeHonor, val);
	}

	public void AddMineral(ushort mineralKey, int ammount)
	{
		if (this.HaveThatResourceInCargoSlots(mineralKey))
		{
			SortedList<byte, short> nums = this.FindCargoSlotsHavingFreeSpace((int)mineralKey);
			foreach (byte key in nums.Keys)
			{
				if (nums[key] < ammount)
				{
					SlotItem slotItem = (
						from t in this.slotItems
						where (t.ItemType != mineralKey || t.Slot != key ? false : t.SlotType == 3)
						select t).FirstOrDefault<SlotItem>();
					if (slotItem != null)
					{
						SlotItem amount = slotItem;
						amount.Amount = amount.Amount + nums[key];
						ammount = ammount - nums[key];
					}
					else
					{
						Console.WriteLine("NB in AddMineral:{0} =>  Sequence contains no elements II", mineralKey);
					}
				}
				else
				{
					SlotItem slotItem1 = (
						from t in this.slotItems
						where (t.ItemType != mineralKey || t.Slot != key ? false : t.SlotType == 3)
						select t).FirstOrDefault<SlotItem>();
					if (slotItem1 != null)
					{
						SlotItem amount1 = slotItem1;
						amount1.Amount = amount1.Amount + ammount;
						break;
					}
					else
					{
						Console.WriteLine("NB in AddMineral:{0} =>  Sequence contains no elements I", mineralKey);
					}
				}
			}
		}
		else if (this.HaveAFreeCargoSlot())
		{
			SlotItem slotItem2 = new SlotItem()
			{
				ItemType = mineralKey,
				Amount = ammount,
				SlotType = 3,
				Slot = this.FirstFreeCargoSlot()
			};
			SlotItem slotItem3 = slotItem2;
			if (slotItem3.Amount > 5000)
			{
				slotItem3.Amount = 5000;
			}
			this.slotItems.Add(slotItem3);
			return;
		}
	}

	public void AddNova(long val)
	{
		this.Add(PlayerItems.TypeNova, val);
	}

	public void AddPortalPart(int portalId, ushort partType, short amount)
	{
		PortalPart portalPart = (
			from p in this.portalParts
			where (p.portalId != portalId ? false : p.partTypeId == partType)
			select p).FirstOrDefault<PortalPart>();
		if (portalPart == null)
		{
			PortalPart portalPart1 = new PortalPart()
			{
				portalId = portalId,
				partTypeId = partType,
				partAmount = amount
			};
			this.portalParts.Add(portalPart1);
		}
		else
		{
			PortalPart portalPart2 = portalPart;
			portalPart2.partAmount = (short)(portalPart2.partAmount + amount);
		}
	}

	public void AddSlotItem(SlotItem item, int slotId, int slotType, int shipId)
	{
		item.Slot = (ushort)slotId;
		item.SlotType = (byte)slotType;
		item.ShipId = shipId;
		item.Amount = (item.Amount != 0 ? item.Amount : 1);
		this.slotItems.Add(item);
	}

	public void AddSlotItem(ushort key, long amount, int slotId, int slotType, int shipId)
	{
		SlotItem slotItem = (
			from t in this.slotItems
			where (t.SlotType != (byte)slotType || t.ItemType != key || t.Slot != (ushort)slotId ? false : t.ShipId == shipId)
			select t).FirstOrDefault<SlotItem>();
		if (slotItem != null)
		{
			SlotItem slotItem1 = slotItem;
			slotItem1.Amount = slotItem1.Amount + (int)amount;
			if (slotItem.Amount > 5000)
			{
				slotItem.Amount = 5000;
			}
		}
		else if (!PlayerItems.IsWeapon(key))
		{
			SlotItem slotItem2 = new SlotItem()
			{
				ItemType = key,
				Slot = (ushort)slotId,
				SlotType = (byte)slotType,
				Amount = (int)amount,
				ShipId = shipId
			};
			this.slotItems.Add(slotItem2);
		}
		else
		{
			SlotItemWeapon slotItemWeapon = new SlotItemWeapon()
			{
				ItemType = key,
				Slot = (ushort)slotId,
				SlotType = (byte)slotType,
				Amount = (int)amount,
				AmmoType = PlayerItems.TypeAmmoSolarCells,
				ShipId = shipId,
				IsActive = true
			};
			this.slotItems.Add(slotItemWeapon);
		}
	}

	public void AddSlotItemWeapon(SlotItemWeapon weapon, int slotId, int slotType, int shipId)
	{
		weapon.Slot = (ushort)slotId;
		weapon.SlotType = (byte)slotType;
		weapon.ShipId = shipId;
		weapon.Amount = 1;
		this.slotItems.Add(weapon);
	}

	public void AddUltralibrium(long val)
	{
		this.Add(PlayerItems.TypeUltralibrium, val);
	}

	public void AddWarCommendation(long val)
	{
		if (this.WarCommendation + val < (long)100)
		{
			this.Add(PlayerItems.TypeWarCommendation, val);
		}
		else
		{
			this.Set(PlayerItems.TypeWarCommendation, (long)100);
		}
	}

	public void BringToNpc(ushort key, int amount)
	{
		SlotItem slotItem;
		List<SlotItem> slotItems = new List<SlotItem>();
		SlotItem[] array = (
			from t in this.slotItems
			where (t.ItemType != key ? false : (t.SlotType == 1 ? true : t.SlotType == 3))
			select t into o
			orderby o.Amount
			select o).ToArray<SlotItem>();
		SlotItem[] slotItemArray = array;
		int num = 0;
		while (num < (int)slotItemArray.Length)
		{
			slotItem = slotItemArray[num];
			if (amount >= slotItem.Amount)
			{
				slotItems.Add(slotItem);
				amount = amount - slotItem.Amount;
				slotItem.Amount = 0;
				num++;
			}
			else
			{
				SlotItem slotItem1 = slotItem;
				slotItem1.Amount = slotItem1.Amount - amount;
				break;
			}
		}
		foreach (SlotItem sl in slotItems)
		{
			this.slotItems.Remove(sl);
		}
	}

	public BuyResult BuyItem(ushort itemType, float priceCash, float priceNova, int amount, bool checkOnly)
	{
		BuyResult buyResult;
		if (amount != 0)
		{
			long item = this.items[PlayerItems.TypeCash];
			long num = this.items[PlayerItems.TypeNova];
			double num1 = (double)(priceCash * (float)amount);
			double num2 = (double)(priceNova * (float)amount);
			if (num1 > 0)
			{
				if ((double)item < num1)
				{
					buyResult = BuyResult.NotEnoughCash;
					return buyResult;
				}
			}
			if (num2 > 0)
			{
				if ((double)num < num2)
				{
					buyResult = BuyResult.NotEnoughNova;
					return buyResult;
				}
			}
			if (!checkOnly)
			{
				if (num1 > 0)
				{
					this.Add(PlayerItems.TypeCash, -(long)num1);
				}
				if (num2 > 0)
				{
					this.Add(PlayerItems.TypeNova, -(long)num2);
				}
				if (!PlayerItems.IsBooster(itemType))
				{
					this.Add(itemType, (long)amount);
				}
				else
				{
					long amountAt = this.GetAmountAt(itemType);
					DateTime dateTime = new DateTime(amountAt);
					long num3 = (long)0;
					if ((amountAt == (long)0 ? false : !(dateTime < DateTime.Now)))
					{
						TimeSpan timeSpan = new TimeSpan((int)PlayerItems.BOOSTER_EFFECT_TIME_IN_DAYS, 0, 0, 0);
						this.Add(itemType, timeSpan.Ticks);
					}
					else
					{
						DateTime now = DateTime.Now;
						now = now.AddDays((double)PlayerItems.BOOSTER_EFFECT_TIME_IN_DAYS);
						this.Set(itemType, now.Ticks);
					}
				}
				buyResult = BuyResult.OK;
			}
			else
			{
				buyResult = BuyResult.OK;
			}
		}
		else
		{
			buyResult = BuyResult.OK;
		}
		return buyResult;
	}

	public BuyResult BuySlotItem(ushort itemType, float priceCash, float priceNova, float priceViral, byte slotId, byte slotType, int amount, int shipId, SelectedCurrency currency, bool checkOnly)
	{
		BuyResult buyResult;
		long cash = this.Cash;
		long nova = this.Nova;
		long amountAt = this.GetAmountAt(PlayerItems.TypeEquilibrium);
		double num = 0;
		double num1 = 0;
		double num2 = 0;
		if (!PlayerItems.IsAmmo(itemType))
		{
			num = (double)(priceCash * (float)amount);
			num1 = (double)(priceNova * (float)amount);
			num2 = (double)(priceViral * (float)amount);
		}
		else
		{
			num = (double)(priceCash / 100f * (float)amount);
			num1 = (double)(priceNova / 100f * (float)amount);
			num2 = (double)(priceViral / 100f * (float)amount);
		}
		switch (currency)
		{
			case SelectedCurrency.Cash:
			{
				if (((double)cash < num ? false : priceCash != 0f))
				{
					break;
				}
				else
				{
					buyResult = BuyResult.NotEnoughCash;
					return buyResult;
				}
			}
			case SelectedCurrency.Nova:
			{
				if (((double)nova < num1 ? false : priceNova != 0f))
				{
					break;
				}
				else
				{
					buyResult = BuyResult.NotEnoughNova;
					return buyResult;
				}
			}
			case SelectedCurrency.Equilibrium:
			{
				if (((double)amountAt < num2 ? false : priceViral != 0f))
				{
					break;
				}
				else
				{
					buyResult = BuyResult.NotEnoughViral;
					return buyResult;
				}
			}
		}
		if (!checkOnly)
		{
			if ((priceCash <= 0f || num <= 0 ? false : currency == SelectedCurrency.Cash))
			{
				this.Add(PlayerItems.TypeCash, -(long)num);
			}
			if ((priceNova <= 0f || num1 <= 0 ? false : currency == SelectedCurrency.Nova))
			{
				this.Add(PlayerItems.TypeNova, -(long)num1);
			}
			if ((priceViral <= 0f || num2 <= 0 ? false : currency == SelectedCurrency.Equilibrium))
			{
				this.Add(PlayerItems.TypeEquilibrium, -(long)num2);
			}
			if (slotType != 1)
			{
				this.AddSlotItem(itemType, (long)amount, (int)slotId, (int)slotType, shipId);
			}
			else
			{
				this.AddSlotItem(itemType, (long)amount, (int)slotId, (int)slotType, 0);
			}
			buyResult = BuyResult.OK;
		}
		else
		{
			buyResult = BuyResult.OK;
		}
		return buyResult;
	}

	public static int CalculateSellPrice(ushort itemTypeId, int bonus = 0)
	{
		int num = 0;
		int item = StaticData.allTypes[itemTypeId].priceCash;
		int item1 = StaticData.allTypes[itemTypeId].priceNova;
		int num1 = StaticData.allTypes[itemTypeId].priceViral;
		if (!(PlayerItems.IsMineral(itemTypeId) || itemTypeId == PlayerItems.TypeEquilibrium ? false : !PlayerItems.IsPortalPart(itemTypeId)))
		{
			num = item;
		}
		else if (item > 0)
		{
			num = ((float)item * 0.15f <= 1000000f ? (int)((float)item * 0.15f) : 1000000);
		}
		else if (item1 <= 0)
		{
			GameObjectPhysics.Log("No case for only viral price");
		}
		else
		{
			num = (item1 * 20 <= 1000000 ? item1 * 20 : 1000000);
		}
		if (bonus > 0)
		{
			num = (int)((float)(num * bonus) / 100f);
		}
		return num;
	}

	public static int CalculateSlotItemSellPrice(SlotItem slotItem, int bonusForSell = 0)
	{
		float upgradeDone = 0f;
		if (PlayerItems.IsWeapon(slotItem.ItemType))
		{
			upgradeDone = (float)(((SlotItemWeapon)slotItem).UpgradeDone * 10);
		}
		float bonusCnt = 0f;
		if (slotItem.BonusCnt > 0)
		{
			bonusCnt = (float)(slotItem.BonusCnt * 10);
		}
		float num = 0f;
		if (slotItem.isAccountBound)
		{
			num = (float)(Mathf.FloorToInt((float)(StaticData.allTypes[slotItem.ItemType].levelRestriction / 10)) * 5);
		}
		int num1 = 0;
		int item = StaticData.allTypes[slotItem.ItemType].priceCash;
		int item1 = StaticData.allTypes[slotItem.ItemType].priceNova;
		int item2 = StaticData.allTypes[slotItem.ItemType].priceViral;
		if (!(PlayerItems.IsMineral(slotItem.ItemType) || slotItem.ItemType == PlayerItems.TypeEquilibrium ? false : !PlayerItems.IsPortalPart(slotItem.ItemType)))
		{
			num1 = item;
		}
		else if (item > 0)
		{
			num1 = ((float)item * 0.15f <= 1000000f ? (int)((float)item * 0.15f) : 1000000);
		}
		else if (item1 > 0)
		{
			num1 = (item1 * 20 <= 1000000 ? item1 * 20 : 1000000);
		}
		else if (item2 > 0)
		{
			num1 = (item2 * 10 <= 1000000 ? item2 * 10 : 1000000);
		}
		if (bonusForSell > 0)
		{
			num1 = Mathf.CeilToInt((float)num1 * ((float)bonusForSell + num + bonusCnt + upgradeDone) / 100f);
		}
		return num1;
	}

	public bool CanLearn(ushort skillId)
	{
		bool flag;
		if (this.GetAmountAt(PlayerItems.TypeTalentPoint) > (long)0)
		{
			SortedList<int, List<ushort>> nums = null;
			TalentsInfo item = (TalentsInfo)StaticData.allTypes[skillId];
			switch (item.talentsClass)
			{
				case 1:
				{
					nums = PlayerItems.guardianSkills;
					break;
				}
				case 2:
				{
					nums = PlayerItems.destroyerSkills;
					break;
				}
				case 3:
				{
					nums = PlayerItems.protectorSkills;
					break;
				}
				case 4:
				{
					nums = PlayerItems.passiveSkills;
					break;
				}
				case 5:
				{
					nums = PlayerItems.amplificationSkillsOne;
					break;
				}
				case 6:
				{
					nums = PlayerItems.amplificationSkillsTwo;
					break;
				}
				case 7:
				{
					nums = PlayerItems.amplificationSkillsThree;
					break;
				}
			}
			int amountAt = 0;
			int num = 0;
			foreach (int key in nums.Keys)
			{
				if (nums[key].Contains(skillId))
				{
					num = key;
					break;
				}
			}
			for (int i = 1; i < num; i++)
			{
				foreach (ushort item1 in nums[i])
				{
					amountAt = amountAt + (int)this.GetAmountAt(item1);
				}
			}
			flag = (item.reqSpendSkillPoint > amountAt ? false : this.GetAmountAt(skillId) <  item.maxLevel);
		}
		else
		{
			flag = false;
		}
		return flag;
	}

	public bool CanMineMineral(Mineral mineral, long CargoCapacity)
	{
		bool flag;
		if (CargoCapacity - this.Cargo <= (long)0)
		{
			foreach (KeyValuePair<ushort, int> resourceQuantity in mineral.resourceQuantities)
			{
				if (!PlayerItems.cargoTypes.Contains<ushort>(resourceQuantity.Key))
				{
					flag = true;
					return flag;
				}
			}
			flag = false;
		}
		else
		{
			flag = true;
		}
		return flag;
	}

	public void ChangeAmmoType(WeaponAmmoTypeChange data)
	{
		SlotItemWeapon slotItemWeapon = (SlotItemWeapon)(
			from si in this.slotItems
			where (si.SlotType != data.slotType || si.Slot != data.slot ? false : si.ShipId == data.shipId)
			select si).First<SlotItem>();
		slotItemWeapon.AmmoType = data.ammoType;
	}

	public void Deserialize(BinaryReader br)
	{
		int i;
		this.keys = new ushort[br.ReadInt32()];
		for (i = 0; i < (int)this.keys.Length; i++)
		{
			this.keys[i] = br.ReadUInt16();
		}
		this.values = new long[br.ReadInt32()];
		for (i = 0; i < (int)this.keys.Length; i++)
		{
			this.values[i] = br.ReadInt64();
		}
		for (i = 0; i < (int)this.keys.Length; i++)
		{
			this.items.Add(this.keys[i], this.values[i]);
		}
		int num = br.ReadInt32();
		this.slotItems = new List<SlotItem>();
		for (i = 0; i < num; i++)
		{
			SlotItem slotItem = new SlotItem();
			slotItem = (SlotItem)TransferablesFramework.DeserializeITransferable(br);
			this.slotItems.Add(slotItem);
		}
		num = br.ReadInt32();
		this.portalParts = new List<PortalPart>();
		for (i = 0; i < num; i++)
		{
			PortalPart portalPart = new PortalPart();
			portalPart = (PortalPart)TransferablesFramework.DeserializeITransferable(br);
			this.portalParts.Add(portalPart);
		}
	}

	public void DumpSlotItem(ushort slotType, byte slot, int shipId)
	{
		SlotItem slotItem = (
			from si in this.slotItems
			where (si.SlotType != slotType || si.Slot != slot ? false : si.ShipId == shipId)
			select si).First<SlotItem>();
		this.slotItems.Remove(slotItem);
	}

	private SortedList<byte, short> FindCargoSlotsHavingFreeSpace(int cargoType)
	{
		SortedList<byte, short> nums = new SortedList<byte, short>();
		SlotItem[] array = (
			from a in this.slotItems
			where (a.ItemType != cargoType ? false : a.SlotType == 3)
			select a).ToArray<SlotItem>();
		SlotItem[] slotItemArray = array;
		for (int i = 0; i < (int)slotItemArray.Length; i++)
		{
			SlotItem slotItem = slotItemArray[i];
			if (slotItem.Amount < 5000)
			{
				nums.Add((byte)slotItem.Slot, (short)(5000 - slotItem.Amount));
			}
		}
		return nums;
	}

	public byte FirstFreeCargoSlot()
	{
		int i;
		byte num = 255;
		bool[] flagArray = new bool[9];
		for (i = 0; i < 8; i++)
		{
			flagArray[i] = false;
		}
		SlotItem[] array = (
			from t in this.slotItems
			where t.SlotType == 3
			orderby t.Slot
			select t).ToArray<SlotItem>();
		SlotItem[] slotItemArray = array;
		for (int j = 0; j < (int)slotItemArray.Length; j++)
		{
			flagArray[slotItemArray[j].Slot] = true;
		}
		i = 0;
		while (i < 8)
		{
			if (flagArray[i])
			{
				i++;
			}
			else
			{
				num = (byte)i;
				break;
			}
		}
		return num;
	}

	public static void GetAmmoPriceByItemType(ushort itemType, out int priceCash, out int priceNova)
	{
		int item = PlayerItems.ammoItemId2DbMapping[itemType];
		AmmoNet ammoNet = (AmmoNet)StaticData.allTypes[itemType];
		priceCash = 0;
		priceNova = 0;
	}

	public static void GetAmmoPrices(ushort itemType, out int cash, out int nova)
	{
		AmmoNet item = (AmmoNet)StaticData.allTypes[itemType];
		cash = 0;
		nova = 0;
	}

	public long GetAmmoQty(ushort key)
	{
		long amount = (long)0;
		SlotItem[] array = (
			from t in this.slotItems
			where (t.ItemType != key ? false : t.SlotType == 1)
			select t).ToArray<SlotItem>();
		SlotItem[] slotItemArray = array;
		for (int i = 0; i < (int)slotItemArray.Length; i++)
		{
			amount = amount + (long)slotItemArray[i].Amount;
		}
		return amount;
	}

	public long GetAmountAt(ushort key)
	{
		long num;
		num = (this.items.ContainsKey(key) ? this.items[key] : (long)0);
		return num;
	}

	public static int GetBoosterDuration(ushort itemType)
	{
		int num;
		if (!(itemType == PlayerItems.TypeBoosterDamageFor1Days || itemType == PlayerItems.TypeBoosterCargoFor1Days || itemType == PlayerItems.TypeBoosterExperienceFor1Days ? false : itemType != PlayerItems.TypeBoosterAutominerFor1Days))
		{
			num = 24;
		}
		else if ((itemType == PlayerItems.TypeBoosterDamageFor3Days || itemType == PlayerItems.TypeBoosterCargoFor3Days || itemType == PlayerItems.TypeBoosterExperienceFor3Days ? false : itemType != PlayerItems.TypeBoosterAutominerFor3Days))
		{
			num = ((itemType == PlayerItems.TypeBoosterDamageFor6Days || itemType == PlayerItems.TypeBoosterCargoFor6Days || itemType == PlayerItems.TypeBoosterExperienceFor6Days ? false : itemType != PlayerItems.TypeBoosterAutominerFor6Days) ? 0 : 144);
		}
		else
		{
			num = 72;
		}
		return num;
	}

	public static string GetDescription(ushort itemType)
	{
		return StaticData.allTypes[itemType].description;
	}

	public static void GetItemPrices(ushort itemType, out int cash, out int nova, out int viral)
	{
		PlayerItemTypesData item = StaticData.allTypes[itemType];
		cash = item.priceCash;
		nova = item.priceNova;
		viral = item.priceViral;
	}

	public string GetLevelInfo(ushort skillId)
	{
		TalentsInfo talentsInfo = (TalentsInfo)(
			from t in StaticData.allTypes.Values
			where t.itemType == skillId
			select t).First<PlayerItemTypesData>();
		int levelEfX = PlayerItems.skillsInformation[skillId].levelEf_X;
		int levelEfY = PlayerItems.skillsInformation[skillId].levelEf_Y;
		int item = PlayerItems.skillsInformation[skillId].levelCooldown;
		int num = PlayerItems.skillsInformation[skillId].neuronCooldown;
		string str = StaticData.Translate(talentsInfo.nextLevelInfo);
		object[] objArray = new object[] { levelEfX, levelEfY, levelEfX + levelEfY, Math.Abs((float)item / 1000f), Math.Abs((float)num / 1000f) };
		return string.Format(str, objArray);
	}

	public long GetMinetalQty(ushort key)
	{
		long amountAt;
		if (key != PlayerItems.TypeEquilibrium)
		{
			long amount = (long)0;
			SlotItem[] array = (
				from t in this.slotItems
				where (t.ItemType != key ? false : t.SlotType == 3)
				select t).ToArray<SlotItem>();
			SlotItem[] slotItemArray = array;
			for (int i = 0; i < (int)slotItemArray.Length; i++)
			{
				amount = amount + (long)slotItemArray[i].Amount;
			}
			amountAt = amount;
		}
		else
		{
			amountAt = this.GetAmountAt(PlayerItems.TypeEquilibrium);
		}
		return amountAt;
	}

	public string GetNeuronlInfo(ushort skillId)
	{
		TalentsInfo talentsInfo = (TalentsInfo)(
			from t in StaticData.allTypes.Values
			where t.itemType == skillId
			select t).First<PlayerItemTypesData>();
		int neuronEfX = PlayerItems.skillsInformation[skillId].neuronEf_X;
		int neuronEfY = PlayerItems.skillsInformation[skillId].neuronEf_Y;
		int item = PlayerItems.skillsInformation[skillId].levelCooldown;
		int num = PlayerItems.skillsInformation[skillId].neuronCooldown;
		string str = StaticData.Translate(talentsInfo.neuronBonusDesc);
		object[] objArray = new object[] { neuronEfX, neuronEfY, neuronEfX + neuronEfY, Math.Abs((float)item / 1000f), Math.Abs((float)num / 1000f) };
		return string.Format(str, objArray);
	}

	public void GetNewSkillEffect(ushort skillId, byte wantedLevel, out int x, out int y)
	{
		int num = (wantedLevel > 0 ? (int)wantedLevel : (int)this.GetAmountAt(skillId));
		if (wantedLevel != 0)
		{
			x = PlayerItems.skillsInformation[skillId].start_X_Ef + PlayerItems.skillsInformation[skillId].levelEf_X * num + PlayerItems.skillsInformation[skillId].neuronEf_X * (int)this.GetAmountAt((ushort)(skillId + 1000));
			y = PlayerItems.skillsInformation[skillId].start_Y_Ef + PlayerItems.skillsInformation[skillId].levelEf_Y * num + PlayerItems.skillsInformation[skillId].neuronEf_Y * (int)this.GetAmountAt((ushort)(skillId + 1000));
			if (skillId == PlayerItems.TypeTalentsSunderArmor)
			{
				x = (this.GetAmountAt(PlayerItems.TypeTalentsArmorBreaker) > (long)0 ? x + PlayerItems.skillsInformation[PlayerItems.TypeTalentsArmorBreaker].start_X_Ef + PlayerItems.skillsInformation[PlayerItems.TypeTalentsArmorBreaker].levelEf_X * (int)this.GetAmountAt(PlayerItems.TypeTalentsArmorBreaker) + PlayerItems.skillsInformation[PlayerItems.TypeTalentsArmorBreaker].neuronEf_X * (int)this.GetAmountAt(PlayerItems.TypeNeuronsForArmorBreaker) : x);
			}
			else if (!(skillId == PlayerItems.TypeTalentsDefiance || skillId == PlayerItems.TypeTalentsAdvancedCorpus ? false : skillId != PlayerItems.TypeTalentsAdvancedShield))
			{
				x = (this.GetAmountAt(PlayerItems.TypeTalentsRealSteel) > (long)0 ? x + PlayerItems.skillsInformation[PlayerItems.TypeTalentsRealSteel].start_X_Ef + PlayerItems.skillsInformation[PlayerItems.TypeTalentsRealSteel].levelEf_X * (int)this.GetAmountAt(PlayerItems.TypeTalentsRealSteel) + PlayerItems.skillsInformation[PlayerItems.TypeTalentsRealSteel].neuronEf_X * (int)this.GetAmountAt(PlayerItems.TypeNeuronsForRealSteel) : x);
			}
			else if (skillId == PlayerItems.TypeTalentsRocketBarrage)
			{
				x = (this.GetAmountAt(PlayerItems.TypeTalentsRocketeer) > (long)0 ? x + PlayerItems.skillsInformation[PlayerItems.TypeTalentsRocketeer].start_X_Ef + PlayerItems.skillsInformation[PlayerItems.TypeTalentsRocketeer].levelEf_X * (int)this.GetAmountAt(PlayerItems.TypeTalentsRocketeer) + PlayerItems.skillsInformation[PlayerItems.TypeTalentsRocketeer].neuronEf_X * (int)this.GetAmountAt(PlayerItems.TypeNeuronsForRocketeer) : x);
				y = (this.GetAmountAt(PlayerItems.TypeTalentsRocketeer) > (long)0 ? y + PlayerItems.skillsInformation[PlayerItems.TypeTalentsRocketeer].start_Y_Ef + PlayerItems.skillsInformation[PlayerItems.TypeTalentsRocketeer].levelEf_Y * (int)this.GetAmountAt(PlayerItems.TypeTalentsRocketeer) + PlayerItems.skillsInformation[PlayerItems.TypeTalentsRocketeer].neuronEf_Y * (int)this.GetAmountAt(PlayerItems.TypeNeuronsForRocketeer) : y);
			}
			else if (!(skillId == PlayerItems.TypeTalentsPowerBreak ? false : skillId != PlayerItems.TypeTalentsPowerCut))
			{
				y = (this.GetAmountAt(PlayerItems.TypeTalentsPowerControl) > (long)0 ? y + PlayerItems.skillsInformation[PlayerItems.TypeTalentsPowerControl].start_X_Ef + PlayerItems.skillsInformation[PlayerItems.TypeTalentsPowerControl].levelEf_X * (int)this.GetAmountAt(PlayerItems.TypeTalentsPowerControl) + PlayerItems.skillsInformation[PlayerItems.TypeTalentsPowerControl].neuronEf_X * (int)this.GetAmountAt(PlayerItems.TypeNeuronsForPowerControl) : y);
			}
			else if (skillId == PlayerItems.TypeTalentsWeaponMastery)
			{
				x = (this.GetAmountAt(PlayerItems.TypeTalentsMassiveDamage) > (long)0 ? x + PlayerItems.skillsInformation[PlayerItems.TypeTalentsMassiveDamage].start_X_Ef + PlayerItems.skillsInformation[PlayerItems.TypeTalentsMassiveDamage].levelEf_X * (int)this.GetAmountAt(PlayerItems.TypeTalentsMassiveDamage) + PlayerItems.skillsInformation[PlayerItems.TypeTalentsMassiveDamage].neuronEf_X * (int)this.GetAmountAt(PlayerItems.TypeNeuronsForMassiveDamage) : x);
			}
			else if (!(skillId == PlayerItems.TypeTalentsEngineBooster ? false : skillId != PlayerItems.TypeTalentsLightSpeed))
			{
				x = (this.GetAmountAt(PlayerItems.TypeTalentsSpeedster) > (long)0 ? x + PlayerItems.skillsInformation[PlayerItems.TypeTalentsSpeedster].start_X_Ef + PlayerItems.skillsInformation[PlayerItems.TypeTalentsSpeedster].levelEf_X * (int)this.GetAmountAt(PlayerItems.TypeTalentsSpeedster) + PlayerItems.skillsInformation[PlayerItems.TypeTalentsSpeedster].neuronEf_X * (int)this.GetAmountAt(PlayerItems.TypeNeuronsForSpeedster) : x);
			}
			else if (!(skillId == PlayerItems.TypeTalentsEmpoweredShield ? false : skillId != PlayerItems.TypeTalentsNanoTechnology))
			{
				x = (this.GetAmountAt(PlayerItems.TypeTalentsFutureTechnology) > (long)0 ? x + PlayerItems.skillsInformation[PlayerItems.TypeTalentsFutureTechnology].start_X_Ef + PlayerItems.skillsInformation[PlayerItems.TypeTalentsFutureTechnology].levelEf_X * (int)this.GetAmountAt(PlayerItems.TypeTalentsFutureTechnology) + PlayerItems.skillsInformation[PlayerItems.TypeTalentsFutureTechnology].neuronEf_X * (int)this.GetAmountAt(PlayerItems.TypeNeuronsForFutureTechnology) : x);
			}
			else if (skillId == PlayerItems.TypeTalentsRepairingDrones)
			{
				x = (this.GetAmountAt(PlayerItems.TypeTalentsDronePower) > (long)0 ? x + PlayerItems.skillsInformation[PlayerItems.TypeTalentsDronePower].start_X_Ef + PlayerItems.skillsInformation[PlayerItems.TypeTalentsDronePower].levelEf_X * (int)this.GetAmountAt(PlayerItems.TypeTalentsDronePower) + PlayerItems.skillsInformation[PlayerItems.TypeTalentsDronePower].neuronEf_X * (int)this.GetAmountAt(PlayerItems.TypeNeuronsForDronePower) : x);
			}
			else if ((skillId == PlayerItems.TypeTalentsImprovedShield || skillId == PlayerItems.TypeTalentsRepairField ? true : skillId == PlayerItems.TypeTalentsNanoShield))
			{
				x = (this.GetAmountAt(PlayerItems.TypeTalentsRepairMaster) > (long)0 ? x + PlayerItems.skillsInformation[PlayerItems.TypeTalentsRepairMaster].start_X_Ef + PlayerItems.skillsInformation[PlayerItems.TypeTalentsRepairMaster].levelEf_X * (int)this.GetAmountAt(PlayerItems.TypeTalentsRepairMaster) + PlayerItems.skillsInformation[PlayerItems.TypeTalentsRepairMaster].neuronEf_X * (int)this.GetAmountAt(PlayerItems.TypeNeuronsForRepairMaster) : x);
			}
		}
		else
		{
			x = 0;
			y = 0;
		}
	}

	public static int GetPowerUpEffectValue(ushort powerUpType, int playerLevel)
	{
		int num = 0;
		if (powerUpType == PlayerItems.TypePowerUpForLaserDamageFlat)
		{
			num = 5 * (int)Math.Floor((double)((float)playerLevel / 10f));
		}
		else if (powerUpType == PlayerItems.TypePowerUpForPlasmaDamageFlat)
		{
			num = 10 * (int)Math.Floor((double)((float)playerLevel / 10f));
		}
		else if (powerUpType == PlayerItems.TypePowerUpForIonDamageFlat)
		{
			num = 20 * (int)Math.Floor((double)((float)playerLevel / 10f));
		}
		else if (powerUpType == PlayerItems.TypePowerUpForLaserDamagePercentage)
		{
			num = 30;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForPlasmaDamagePercentage)
		{
			num = 30;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForIonDamagePercentage)
		{
			num = 30;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForTotalDamagePercentage)
		{
			num = 30;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForCorpusFlat)
		{
			num = (int)(Math.Pow(2, Math.Floor((double)((float)playerLevel / 10f)) - 1) * 150);
		}
		else if (powerUpType == PlayerItems.TypePowerUpForCorpusPercentage)
		{
			num = 50;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForShieldFlat)
		{
			num = (int)(Math.Pow(2, Math.Floor((double)((float)playerLevel / 10f)) - 1) * 150);
		}
		else if (powerUpType == PlayerItems.TypePowerUpForShieldPercentage)
		{
			num = 50;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForEndurancePercentage)
		{
			num = 50;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForShieldPowerFlat)
		{
			num = (int)Math.Floor((double)((float)playerLevel / 10f)) * 50;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForShieldPowerPercentage)
		{
			num = 15;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForTargetingFlat)
		{
			num = (int)Math.Floor((double)((float)playerLevel / 10f)) * 50;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForTargetingPercentage)
		{
			num = 30;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForAvoidanceFlat)
		{
			num = (int)Math.Floor((double)((float)playerLevel / 10f)) * 50;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForAvoidancePercentage)
		{
			num = 30;
		}
		return num;
	}

	public static string GetRankingTitle(int honor)
	{
		string str;
		if (honor < 800)
		{
			str = StaticData.Translate("key_title_cadet");
		}
		else if (honor < 1050)
		{
			str = StaticData.Translate("key_title_officer");
		}
		else if (honor < 1300)
		{
			str = StaticData.Translate("key_title_lieutenant");
		}
		else if (honor < 1450)
		{
			str = StaticData.Translate("key_title_commander");
		}
		else if (honor < 1550)
		{
			str = StaticData.Translate("key_title_captain");
		}
		else if (honor >= 1650)
		{
			str = (honor >= 1750 ? StaticData.Translate("key_title_marshal") : StaticData.Translate("key_title_admiral"));
		}
		else
		{
			str = StaticData.Translate("key_title_commodore");
		}
		return str;
	}

	public static int GetRerollPrice(ushort itemId, int bonusCnt, int selected, int maximaized, SelectedCurrency currency, byte discountBonus = 0)
	{
		int num;
		int item = StaticData.allTypes[itemId].levelRestriction;
		int num1 = (int)(((double)(10 + 10 * item) * Math.Pow(1.25, (double)bonusCnt) + (double)((45 + 13 * item) * selected) * Math.Pow(Math.Sqrt(2), (double)(maximaized + 1))) * (double)item * (double)bonusCnt / 250 * (double)(1f + (float)maximaized / 50f) * (double)(100 - discountBonus) / 100);
		num = (currency != SelectedCurrency.Equilibrium ? num1 : num1 * 2);
		return num;
	}

	public int GetSkillCooldown(ushort skillId)
	{
		int item = 0;
		item = ((TalentsInfo)(
			from t in StaticData.allTypes.Values
			where t.itemType == skillId
			select t).First<PlayerItemTypesData>()).cooldown;
		if (PlayerItems.skillsInformation[skillId].isChangableCooldown)
		{
			item = item + PlayerItems.skillsInformation[skillId].levelCooldown * (int)this.GetAmountAt(skillId) + PlayerItems.skillsInformation[skillId].neuronCooldown * (int)this.GetAmountAt((ushort)(skillId + 1000));
		}
		return item;
	}

	public string GetSkillDescription(ushort skillId, byte wantedLevel)
	{
		int startXEf;
		int startYEf;
		string str = "";
		if (wantedLevel != 0)
		{
			this.GetNewSkillEffect(skillId, wantedLevel, out startXEf, out startYEf);
		}
		else
		{
			startXEf = PlayerItems.skillsInformation[skillId].start_X_Ef + PlayerItems.skillsInformation[skillId].levelEf_X;
			startYEf = PlayerItems.skillsInformation[skillId].start_Y_Ef + PlayerItems.skillsInformation[skillId].levelEf_Y;
		}
		str = string.Format(StaticData.Translate(StaticData.allTypes[skillId].description), startXEf, startYEf, startXEf + startYEf);
		return str;
	}

	public void GetSkillEffect(ushort skillId, out int x, out int y)
	{
		if (this.GetAmountAt(skillId) != (long)0)
		{
			x = PlayerItems.skillsInformation[skillId].start_X_Ef + PlayerItems.skillsInformation[skillId].levelEf_X * (int)this.GetAmountAt(skillId) + PlayerItems.skillsInformation[skillId].neuronEf_X * (int)this.GetAmountAt((ushort)(skillId + 1000));
			y = PlayerItems.skillsInformation[skillId].start_Y_Ef + PlayerItems.skillsInformation[skillId].levelEf_Y * (int)this.GetAmountAt(skillId) + PlayerItems.skillsInformation[skillId].neuronEf_Y * (int)this.GetAmountAt((ushort)(skillId + 1000));
			if (skillId == PlayerItems.TypeTalentsSunderArmor)
			{
				x = (this.GetAmountAt(PlayerItems.TypeTalentsArmorBreaker) > (long)0 ? x + PlayerItems.skillsInformation[PlayerItems.TypeTalentsArmorBreaker].start_X_Ef + PlayerItems.skillsInformation[PlayerItems.TypeTalentsArmorBreaker].levelEf_X * (int)this.GetAmountAt(PlayerItems.TypeTalentsArmorBreaker) + PlayerItems.skillsInformation[PlayerItems.TypeTalentsArmorBreaker].neuronEf_X * (int)this.GetAmountAt(PlayerItems.TypeNeuronsForArmorBreaker) : x);
			}
			else if (!(skillId == PlayerItems.TypeTalentsDefiance || skillId == PlayerItems.TypeTalentsAdvancedCorpus ? false : skillId != PlayerItems.TypeTalentsAdvancedShield))
			{
				x = (this.GetAmountAt(PlayerItems.TypeTalentsRealSteel) > (long)0 ? x + PlayerItems.skillsInformation[PlayerItems.TypeTalentsRealSteel].start_X_Ef + PlayerItems.skillsInformation[PlayerItems.TypeTalentsRealSteel].levelEf_X * (int)this.GetAmountAt(PlayerItems.TypeTalentsRealSteel) + PlayerItems.skillsInformation[PlayerItems.TypeTalentsRealSteel].neuronEf_X * (int)this.GetAmountAt(PlayerItems.TypeNeuronsForRealSteel) : x);
			}
			else if (skillId == PlayerItems.TypeTalentsRocketBarrage)
			{
				x = (this.GetAmountAt(PlayerItems.TypeTalentsRocketeer) > (long)0 ? x + PlayerItems.skillsInformation[PlayerItems.TypeTalentsRocketeer].start_X_Ef + PlayerItems.skillsInformation[PlayerItems.TypeTalentsRocketeer].levelEf_X * (int)this.GetAmountAt(PlayerItems.TypeTalentsRocketeer) + PlayerItems.skillsInformation[PlayerItems.TypeTalentsRocketeer].neuronEf_X * (int)this.GetAmountAt(PlayerItems.TypeNeuronsForRocketeer) : x);
				y = (this.GetAmountAt(PlayerItems.TypeTalentsRocketeer) > (long)0 ? y + PlayerItems.skillsInformation[PlayerItems.TypeTalentsRocketeer].start_Y_Ef + PlayerItems.skillsInformation[PlayerItems.TypeTalentsRocketeer].levelEf_Y * (int)this.GetAmountAt(PlayerItems.TypeTalentsRocketeer) + PlayerItems.skillsInformation[PlayerItems.TypeTalentsRocketeer].neuronEf_Y * (int)this.GetAmountAt(PlayerItems.TypeNeuronsForRocketeer) : y);
			}
			else if (!(skillId == PlayerItems.TypeTalentsPowerBreak ? false : skillId != PlayerItems.TypeTalentsPowerCut))
			{
				y = (this.GetAmountAt(PlayerItems.TypeTalentsPowerControl) > (long)0 ? y + PlayerItems.skillsInformation[PlayerItems.TypeTalentsPowerControl].start_X_Ef + PlayerItems.skillsInformation[PlayerItems.TypeTalentsPowerControl].levelEf_X * (int)this.GetAmountAt(PlayerItems.TypeTalentsPowerControl) + PlayerItems.skillsInformation[PlayerItems.TypeTalentsPowerControl].neuronEf_X * (int)this.GetAmountAt(PlayerItems.TypeNeuronsForPowerControl) : y);
			}
			else if (skillId == PlayerItems.TypeTalentsWeaponMastery)
			{
				x = (this.GetAmountAt(PlayerItems.TypeTalentsMassiveDamage) > (long)0 ? x + PlayerItems.skillsInformation[PlayerItems.TypeTalentsMassiveDamage].start_X_Ef + PlayerItems.skillsInformation[PlayerItems.TypeTalentsMassiveDamage].levelEf_X * (int)this.GetAmountAt(PlayerItems.TypeTalentsMassiveDamage) + PlayerItems.skillsInformation[PlayerItems.TypeTalentsMassiveDamage].neuronEf_X * (int)this.GetAmountAt(PlayerItems.TypeNeuronsForMassiveDamage) : x);
			}
			else if (!(skillId == PlayerItems.TypeTalentsEngineBooster ? false : skillId != PlayerItems.TypeTalentsLightSpeed))
			{
				x = (this.GetAmountAt(PlayerItems.TypeTalentsSpeedster) > (long)0 ? x + PlayerItems.skillsInformation[PlayerItems.TypeTalentsSpeedster].start_X_Ef + PlayerItems.skillsInformation[PlayerItems.TypeTalentsSpeedster].levelEf_X * (int)this.GetAmountAt(PlayerItems.TypeTalentsSpeedster) + PlayerItems.skillsInformation[PlayerItems.TypeTalentsSpeedster].neuronEf_X * (int)this.GetAmountAt(PlayerItems.TypeNeuronsForSpeedster) : x);
			}
			else if (!(skillId == PlayerItems.TypeTalentsEmpoweredShield ? false : skillId != PlayerItems.TypeTalentsNanoTechnology))
			{
				x = (this.GetAmountAt(PlayerItems.TypeTalentsFutureTechnology) > (long)0 ? x + PlayerItems.skillsInformation[PlayerItems.TypeTalentsFutureTechnology].start_X_Ef + PlayerItems.skillsInformation[PlayerItems.TypeTalentsFutureTechnology].levelEf_X * (int)this.GetAmountAt(PlayerItems.TypeTalentsFutureTechnology) + PlayerItems.skillsInformation[PlayerItems.TypeTalentsFutureTechnology].neuronEf_X * (int)this.GetAmountAt(PlayerItems.TypeNeuronsForFutureTechnology) : x);
			}
			else if (skillId == PlayerItems.TypeTalentsRepairingDrones)
			{
				x = (this.GetAmountAt(PlayerItems.TypeTalentsDronePower) > (long)0 ? x + PlayerItems.skillsInformation[PlayerItems.TypeTalentsDronePower].start_X_Ef + PlayerItems.skillsInformation[PlayerItems.TypeTalentsDronePower].levelEf_X * (int)this.GetAmountAt(PlayerItems.TypeTalentsDronePower) + PlayerItems.skillsInformation[PlayerItems.TypeTalentsDronePower].neuronEf_X * (int)this.GetAmountAt(PlayerItems.TypeNeuronsForDronePower) : x);
			}
			else if ((skillId == PlayerItems.TypeTalentsImprovedShield || skillId == PlayerItems.TypeTalentsRepairField ? true : skillId == PlayerItems.TypeTalentsNanoShield))
			{
				x = (this.GetAmountAt(PlayerItems.TypeTalentsRepairMaster) > (long)0 ? x + PlayerItems.skillsInformation[PlayerItems.TypeTalentsRepairMaster].start_X_Ef + PlayerItems.skillsInformation[PlayerItems.TypeTalentsRepairMaster].levelEf_X * (int)this.GetAmountAt(PlayerItems.TypeTalentsRepairMaster) + PlayerItems.skillsInformation[PlayerItems.TypeTalentsRepairMaster].neuronEf_X * (int)this.GetAmountAt(PlayerItems.TypeNeuronsForRepairMaster) : x);
			}
		}
		else
		{
			x = 0;
			y = 0;
		}
	}

	public static int GetTransformPrice(SelectedCurrency currency  , SelectedWallet wallet  , byte intensity = 1)
	{
        //currency = 3;
        //wallet = 1
        int num;
		int num1 = 0;
		switch (intensity)
		{
			case 1:
			{
				num1 = 10;
				if (currency == SelectedCurrency.Nova)
				{
					num1 = num1 * 5;
				}
				if (wallet == SelectedWallet.GuildBank)
				{
					num1 = num1 * 5;
				}
				num = num1;
				return num;
			}
			case 2:
			{
				num1 = 18;
				if (currency == SelectedCurrency.Nova)
				{
					num1 = num1 * 5;
				}
				if (wallet == SelectedWallet.GuildBank)
				{
					num1 = num1 * 5;
				}
				num = num1;
				return num;
			}
			case 3:
			{
				if (currency == SelectedCurrency.Nova)
				{
					num1 = num1 * 5;
				}
				if (wallet == SelectedWallet.GuildBank)
				{
					num1 = num1 * 5;
				}
				num = num1;
				return num;
			}
			case 4:
			{
				num1 = 30;
				if (currency == SelectedCurrency.Nova)
				{
					num1 = num1 * 5;
				}
				if (wallet == SelectedWallet.GuildBank)
				{
					num1 = num1 * 5;
				}
				num = num1;
				return num;
			}
			default:
			{
				if (currency == SelectedCurrency.Nova)
				{
					num1 = num1 * 5;
				}
				if (wallet == SelectedWallet.GuildBank)
				{
					num1 = num1 * 5;
				}
				num = num1;
				return num;
			}
		}
	}

	private bool HaveAFreeCargoSlot()
	{
		bool flag = (
			from t in this.slotItems
			where t.SlotType == 3
			select t).Count<SlotItem>() < 8;
		return flag;
	}

	public bool HaveAllPartsForPortal(int portalId)
	{
		bool flag;
		Portal portal = (
			from p in StaticData.allPortals
			where p.portalId == portalId
			select p).FirstOrDefault<Portal>();
		if (portal != null)
		{
			foreach (ushort key in portal.parts.Keys)
			{
				PortalPart portalPart = (
					from t in this.portalParts
					where (t.portalId != portalId ? false : t.partTypeId == key)
					select t).FirstOrDefault<PortalPart>();
				if (portalPart == null)
				{
					flag = false;
					return flag;
				}
				else if (portalPart.partAmount < portal.parts[key])
				{
					flag = false;
					return flag;
				}
			}
			flag = true;
		}
		else
		{
			flag = false;
		}
		return flag;
	}

	private bool HaveThatResourceInCargoSlots(ushort resKey)
	{
		bool flag = (
			from t in this.slotItems
			where (t.SlotType != 3 ? false : t.ItemType == resKey)
			select t).FirstOrDefault<SlotItem>() != null;
		return flag;
	}

	private static void InitFusionDependancies()
	{
		PlayerItems.fusionDependancies = new SortedList<ushort, SortedList<ushort, short>>();
		SortedList<ushort, short> nums = new SortedList<ushort, short>();
		SortedList<ushort, short> nums1 = new SortedList<ushort, short>();
		SortedList<ushort, short> nums2 = new SortedList<ushort, short>();
		SortedList<ushort, short> nums3 = new SortedList<ushort, short>();
		SortedList<ushort, short> nums4 = new SortedList<ushort, short>();
		SortedList<ushort, short> nums5 = new SortedList<ushort, short>();
		SortedList<ushort, short> nums6 = new SortedList<ushort, short>();
		SortedList<ushort, short> nums7 = new SortedList<ushort, short>();
		SortedList<ushort, short> nums8 = new SortedList<ushort, short>()
		{
			{ PlayerItems.TypeCarbon, 10 },
			{ PlayerItems.TypeHydrogen, 30 }
		};
		SortedList<ushort, short> nums9 = new SortedList<ushort, short>()
		{
			{ PlayerItems.TypeCarbon, 10 },
			{ PlayerItems.TypeOxygen, 20 }
		};
		SortedList<ushort, short> nums10 = new SortedList<ushort, short>()
		{
			{ PlayerItems.TypeHydrogen, 20 },
			{ PlayerItems.TypeOxygen, 10 }
		};
		SortedList<ushort, short> nums11 = new SortedList<ushort, short>()
		{
			{ PlayerItems.TypeMetyl, 5 },
			{ PlayerItems.TypeCarbonDioxide, 5 }
		};
		SortedList<ushort, short> nums12 = new SortedList<ushort, short>()
		{
			{ PlayerItems.TypeAceton, 1 },
			{ PlayerItems.TypeDeuterium, 10 }
		};
		PlayerItems.fusionDependancies.Add(PlayerItems.TypeCarbon, nums4);
		PlayerItems.fusionDependancies.Add(PlayerItems.TypeOxygen, nums5);
		PlayerItems.fusionDependancies.Add(PlayerItems.TypeHydrogen, nums6);
		PlayerItems.fusionDependancies.Add(PlayerItems.TypeDeuterium, nums7);
		PlayerItems.fusionDependancies.Add(PlayerItems.TypeMetyl, nums8);
		PlayerItems.fusionDependancies.Add(PlayerItems.TypeCarbonDioxide, nums9);
		PlayerItems.fusionDependancies.Add(PlayerItems.TypeWater, nums10);
		PlayerItems.fusionDependancies.Add(PlayerItems.TypeAceton, nums11);
		PlayerItems.fusionDependancies.Add(PlayerItems.TypeEquilibrium, nums12);
		PlayerItems.fusionDependancies.Add(PlayerItems.TypeAmmoSolarCells, nums);
		PlayerItems.fusionDependancies.Add(PlayerItems.TypeAmmoFusionCells, nums1);
		PlayerItems.fusionDependancies.Add(PlayerItems.TypeAmmoColdFusionCells, nums2);
		PlayerItems.fusionDependancies.Add(PlayerItems.TypeAmmoSulfurFusionCells, nums3);
		nums.Add(PlayerItems.TypeWater, 1);
		nums.Add(PlayerItems.TypeMetyl, 1);
		nums1.Add(PlayerItems.TypeEquilibrium, 1);
		nums1.Add(PlayerItems.TypeMetyl, 1);
		nums2.Add(PlayerItems.TypeEquilibrium, 1);
		nums2.Add(PlayerItems.TypeAceton, 1);
		nums3.Add(PlayerItems.TypeEquilibrium, 1);
		nums3.Add(PlayerItems.TypeAceton, 1);
	}

	private static void InitSkillInformation()
	{
		PlayerItems.skillsInformation = new SortedList<ushort, SkillEffectInfo>();
		SortedList<ushort, SkillEffectInfo> nums = PlayerItems.skillsInformation;
		ushort typeTalentsSunderArmor = PlayerItems.TypeTalentsSunderArmor;
		SkillEffectInfo skillEffectInfo = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 100,
			levelEf_X = 5,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 10,
			isChangableCooldown = true,
			levelCooldown = 1000,
			neuronCooldown = 0
		};
		nums.Add(typeTalentsSunderArmor, skillEffectInfo);
		SortedList<ushort, SkillEffectInfo> nums1 = PlayerItems.skillsInformation;
		ushort typeTalentsDefiance = PlayerItems.TypeTalentsDefiance;
		SkillEffectInfo skillEffectInfo1 = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 0,
			levelEf_X = 15,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 20,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums1.Add(typeTalentsDefiance, skillEffectInfo1);
		SortedList<ushort, SkillEffectInfo> nums2 = PlayerItems.skillsInformation;
		ushort typeTalentsTaunt = PlayerItems.TypeTalentsTaunt;
		SkillEffectInfo skillEffectInfo2 = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 0,
			levelEf_X = 0,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 0,
			isChangableCooldown = true,
			levelCooldown = 0,
			neuronCooldown = -1000
		};
		nums2.Add(typeTalentsTaunt, skillEffectInfo2);
		SortedList<ushort, SkillEffectInfo> nums3 = PlayerItems.skillsInformation;
		ushort typeTalentsRocketBarrage = PlayerItems.TypeTalentsRocketBarrage;
		SkillEffectInfo skillEffectInfo3 = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 75,
			levelEf_X = 2,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 15,
			isChangableCooldown = true,
			levelCooldown = 2000,
			neuronCooldown = 0
		};
		nums3.Add(typeTalentsRocketBarrage, skillEffectInfo3);
		SortedList<ushort, SkillEffectInfo> nums4 = PlayerItems.skillsInformation;
		ushort typeTalentsAdvancedCorpus = PlayerItems.TypeTalentsAdvancedCorpus;
		SkillEffectInfo skillEffectInfo4 = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 0,
			levelEf_X = 3,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 3,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums4.Add(typeTalentsAdvancedCorpus, skillEffectInfo4);
		SortedList<ushort, SkillEffectInfo> nums5 = PlayerItems.skillsInformation;
		ushort typeTalentsFocusFire = PlayerItems.TypeTalentsFocusFire;
		SkillEffectInfo skillEffectInfo5 = new SkillEffectInfo()
		{
			start_X_Ef = 100,
			start_Y_Ef = 0,
			levelEf_X = 0,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 10,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums5.Add(typeTalentsFocusFire, skillEffectInfo5);
		SortedList<ushort, SkillEffectInfo> nums6 = PlayerItems.skillsInformation;
		ushort typeTalentsAdvancedShield = PlayerItems.TypeTalentsAdvancedShield;
		SkillEffectInfo skillEffectInfo6 = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 0,
			levelEf_X = 3,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 3,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums6.Add(typeTalentsAdvancedShield, skillEffectInfo6);
		SortedList<ushort, SkillEffectInfo> nums7 = PlayerItems.skillsInformation;
		ushort typeTalentsUnstoppable = PlayerItems.TypeTalentsUnstoppable;
		SkillEffectInfo skillEffectInfo7 = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 0,
			levelEf_X = 3,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 3,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums7.Add(typeTalentsUnstoppable, skillEffectInfo7);
		SortedList<ushort, SkillEffectInfo> nums8 = PlayerItems.skillsInformation;
		ushort typeTalentsForceWave = PlayerItems.TypeTalentsForceWave;
		SkillEffectInfo skillEffectInfo8 = new SkillEffectInfo()
		{
			start_X_Ef = 6,
			start_Y_Ef = 85,
			levelEf_X = 2,
			levelEf_Y = 15,
			neuronEf_X = 0,
			neuronEf_Y = 0,
			isChangableCooldown = true,
			levelCooldown = 0,
			neuronCooldown = -3000
		};
		nums8.Add(typeTalentsForceWave, skillEffectInfo8);
		SortedList<ushort, SkillEffectInfo> nums9 = PlayerItems.skillsInformation;
		ushort typeTalentsShieldFortress = PlayerItems.TypeTalentsShieldFortress;
		SkillEffectInfo skillEffectInfo9 = new SkillEffectInfo()
		{
			start_X_Ef = 1,
			start_Y_Ef = 0,
			levelEf_X = 1,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 1,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums9.Add(typeTalentsShieldFortress, skillEffectInfo9);
		SortedList<ushort, SkillEffectInfo> nums10 = PlayerItems.skillsInformation;
		ushort typeTalentsRepairingDrones = PlayerItems.TypeTalentsRepairingDrones;
		SkillEffectInfo skillEffectInfo10 = new SkillEffectInfo()
		{
			start_X_Ef = 45,
			start_Y_Ef = 0,
			levelEf_X = 5,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 5,
			isChangableCooldown = true,
			levelCooldown = 2500,
			neuronCooldown = 0
		};
		nums10.Add(typeTalentsRepairingDrones, skillEffectInfo10);
		SortedList<ushort, SkillEffectInfo> nums11 = PlayerItems.skillsInformation;
		ushort typeTalentsImprovedShield = PlayerItems.TypeTalentsImprovedShield;
		SkillEffectInfo skillEffectInfo11 = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 0,
			levelEf_X = 3,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 3,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums11.Add(typeTalentsImprovedShield, skillEffectInfo11);
		SortedList<ushort, SkillEffectInfo> nums12 = PlayerItems.skillsInformation;
		ushort typeTalentsNanoStorm = PlayerItems.TypeTalentsNanoStorm;
		SkillEffectInfo skillEffectInfo12 = new SkillEffectInfo()
		{
			start_X_Ef = 50,
			start_Y_Ef = 0,
			levelEf_X = 0,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 5,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums12.Add(typeTalentsNanoStorm, skillEffectInfo12);
		SortedList<ushort, SkillEffectInfo> nums13 = PlayerItems.skillsInformation;
		ushort typeTalentsNanoShield = PlayerItems.TypeTalentsNanoShield;
		SkillEffectInfo skillEffectInfo13 = new SkillEffectInfo()
		{
			start_X_Ef = 75,
			start_Y_Ef = 0,
			levelEf_X = 25,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 20,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums13.Add(typeTalentsNanoShield, skillEffectInfo13);
		SortedList<ushort, SkillEffectInfo> nums14 = PlayerItems.skillsInformation;
		ushort typeTalentsRepairField = PlayerItems.TypeTalentsRepairField;
		SkillEffectInfo skillEffectInfo14 = new SkillEffectInfo()
		{
			start_X_Ef = 5,
			start_Y_Ef = 0,
			levelEf_X = 2,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 2,
			isChangableCooldown = true,
			levelCooldown = 1000,
			neuronCooldown = 0
		};
		nums14.Add(typeTalentsRepairField, skillEffectInfo14);
		SortedList<ushort, SkillEffectInfo> nums15 = PlayerItems.skillsInformation;
		ushort typeTalentsEmpoweredShield = PlayerItems.TypeTalentsEmpoweredShield;
		SkillEffectInfo skillEffectInfo15 = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 0,
			levelEf_X = 15,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 15,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums15.Add(typeTalentsEmpoweredShield, skillEffectInfo15);
		SortedList<ushort, SkillEffectInfo> nums16 = PlayerItems.skillsInformation;
		ushort typeTalentsShortCircuit = PlayerItems.TypeTalentsShortCircuit;
		SkillEffectInfo skillEffectInfo16 = new SkillEffectInfo()
		{
			start_X_Ef = 80,
			start_Y_Ef = 0,
			levelEf_X = 0,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 5,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums16.Add(typeTalentsShortCircuit, skillEffectInfo16);
		SortedList<ushort, SkillEffectInfo> nums17 = PlayerItems.skillsInformation;
		ushort typeTalentsNanoTechnology = PlayerItems.TypeTalentsNanoTechnology;
		SkillEffectInfo skillEffectInfo17 = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 0,
			levelEf_X = 20,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 25,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums17.Add(typeTalentsNanoTechnology, skillEffectInfo17);
		SortedList<ushort, SkillEffectInfo> nums18 = PlayerItems.skillsInformation;
		ushort typeTalentsPulseNova = PlayerItems.TypeTalentsPulseNova;
		SkillEffectInfo skillEffectInfo18 = new SkillEffectInfo()
		{
			start_X_Ef = 70,
			start_Y_Ef = 0,
			levelEf_X = 30,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 25,
			isChangableCooldown = true,
			levelCooldown = 10000,
			neuronCooldown = 0
		};
		nums18.Add(typeTalentsPulseNova, skillEffectInfo18);
		SortedList<ushort, SkillEffectInfo> nums19 = PlayerItems.skillsInformation;
		ushort typeTalentsRemedy = PlayerItems.TypeTalentsRemedy;
		SkillEffectInfo skillEffectInfo19 = new SkillEffectInfo()
		{
			start_X_Ef = 15,
			start_Y_Ef = 1,
			levelEf_X = 5,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 1,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums19.Add(typeTalentsRemedy, skillEffectInfo19);
		SortedList<ushort, SkillEffectInfo> nums20 = PlayerItems.skillsInformation;
		ushort typeTalentsLaserDestruction = PlayerItems.TypeTalentsLaserDestruction;
		SkillEffectInfo skillEffectInfo20 = new SkillEffectInfo()
		{
			start_X_Ef = 40,
			start_Y_Ef = 0,
			levelEf_X = 20,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 15,
			isChangableCooldown = true,
			levelCooldown = 1000,
			neuronCooldown = 0
		};
		nums20.Add(typeTalentsLaserDestruction, skillEffectInfo20);
		SortedList<ushort, SkillEffectInfo> nums21 = PlayerItems.skillsInformation;
		ushort typeTalentsFindWeakSpot = PlayerItems.TypeTalentsFindWeakSpot;
		SkillEffectInfo skillEffectInfo21 = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 0,
			levelEf_X = 20,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 25,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums21.Add(typeTalentsFindWeakSpot, skillEffectInfo21);
		SortedList<ushort, SkillEffectInfo> nums22 = PlayerItems.skillsInformation;
		ushort typeTalentsStealth = PlayerItems.TypeTalentsStealth;
		SkillEffectInfo skillEffectInfo22 = new SkillEffectInfo()
		{
			start_X_Ef = 5,
			start_Y_Ef = 0,
			levelEf_X = 10,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 15,
			isChangableCooldown = true,
			levelCooldown = 5000,
			neuronCooldown = 0
		};
		nums22.Add(typeTalentsStealth, skillEffectInfo22);
		SortedList<ushort, SkillEffectInfo> nums23 = PlayerItems.skillsInformation;
		ushort typeTalentsDecoy = PlayerItems.TypeTalentsDecoy;
		SkillEffectInfo skillEffectInfo23 = new SkillEffectInfo()
		{
			start_X_Ef = 75,
			start_Y_Ef = 0,
			levelEf_X = 25,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 10,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums23.Add(typeTalentsDecoy, skillEffectInfo23);
		SortedList<ushort, SkillEffectInfo> nums24 = PlayerItems.skillsInformation;
		ushort typeTalentsWeaponMastery = PlayerItems.TypeTalentsWeaponMastery;
		SkillEffectInfo skillEffectInfo24 = new SkillEffectInfo()
		{
			start_X_Ef = 1,
			start_Y_Ef = 0,
			levelEf_X = 1,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 1,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums24.Add(typeTalentsWeaponMastery, skillEffectInfo24);
		SortedList<ushort, SkillEffectInfo> nums25 = PlayerItems.skillsInformation;
		ushort typeTalentsPowerBreak = PlayerItems.TypeTalentsPowerBreak;
		SkillEffectInfo skillEffectInfo25 = new SkillEffectInfo()
		{
			start_X_Ef = 1,
			start_Y_Ef = 75,
			levelEf_X = 1,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 15,
			isChangableCooldown = true,
			levelCooldown = 1000,
			neuronCooldown = 0
		};
		nums25.Add(typeTalentsPowerBreak, skillEffectInfo25);
		SortedList<ushort, SkillEffectInfo> nums26 = PlayerItems.skillsInformation;
		ushort typeTalentsPowerCut = PlayerItems.TypeTalentsPowerCut;
		SkillEffectInfo skillEffectInfo26 = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 100,
			levelEf_X = 20,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 10,
			isChangableCooldown = true,
			levelCooldown = 1000,
			neuronCooldown = 0
		};
		nums26.Add(typeTalentsPowerCut, skillEffectInfo26);
		SortedList<ushort, SkillEffectInfo> nums27 = PlayerItems.skillsInformation;
		ushort typeTalentsEngineBooster = PlayerItems.TypeTalentsEngineBooster;
		SkillEffectInfo skillEffectInfo27 = new SkillEffectInfo()
		{
			start_X_Ef = 2,
			start_Y_Ef = 0,
			levelEf_X = 2,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 2,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums27.Add(typeTalentsEngineBooster, skillEffectInfo27);
		SortedList<ushort, SkillEffectInfo> nums28 = PlayerItems.skillsInformation;
		ushort typeTalentsLightSpeed = PlayerItems.TypeTalentsLightSpeed;
		SkillEffectInfo skillEffectInfo28 = new SkillEffectInfo()
		{
			start_X_Ef = 30,
			start_Y_Ef = 7,
			levelEf_X = 20,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 2,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums28.Add(typeTalentsLightSpeed, skillEffectInfo28);
		SortedList<ushort, SkillEffectInfo> nums29 = PlayerItems.skillsInformation;
		ushort typeTalentsMistShroud = PlayerItems.TypeTalentsMistShroud;
		SkillEffectInfo skillEffectInfo29 = new SkillEffectInfo()
		{
			start_X_Ef = 500,
			start_Y_Ef = 5,
			levelEf_X = 100,
			levelEf_Y = 1,
			neuronEf_X = 0,
			neuronEf_Y = 2,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums29.Add(typeTalentsMistShroud, skillEffectInfo29);
		SortedList<ushort, SkillEffectInfo> nums30 = PlayerItems.skillsInformation;
		ushort typeTalentsArchiver = PlayerItems.TypeTalentsArchiver;
		SkillEffectInfo skillEffectInfo30 = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 0,
			levelEf_X = 10,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 10,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums30.Add(typeTalentsArchiver, skillEffectInfo30);
		SortedList<ushort, SkillEffectInfo> nums31 = PlayerItems.skillsInformation;
		ushort typeTalentsMerchant = PlayerItems.TypeTalentsMerchant;
		SkillEffectInfo skillEffectInfo31 = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 0,
			levelEf_X = 3,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 7,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums31.Add(typeTalentsMerchant, skillEffectInfo31);
		SortedList<ushort, SkillEffectInfo> nums32 = PlayerItems.skillsInformation;
		ushort typeTalentsSteadyAim = PlayerItems.TypeTalentsSteadyAim;
		SkillEffectInfo skillEffectInfo32 = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 0,
			levelEf_X = 20,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 25,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums32.Add(typeTalentsSteadyAim, skillEffectInfo32);
		SortedList<ushort, SkillEffectInfo> nums33 = PlayerItems.skillsInformation;
		ushort typeTalentsSwiftLearner = PlayerItems.TypeTalentsSwiftLearner;
		SkillEffectInfo skillEffectInfo33 = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 0,
			levelEf_X = 5,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 5,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums33.Add(typeTalentsSwiftLearner, skillEffectInfo33);
		SortedList<ushort, SkillEffectInfo> nums34 = PlayerItems.skillsInformation;
		ushort typeTalentsAlienSpecialist = PlayerItems.TypeTalentsAlienSpecialist;
		SkillEffectInfo skillEffectInfo34 = new SkillEffectInfo()
		{
			start_X_Ef = 1,
			start_Y_Ef = 0,
			levelEf_X = 2,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 3,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums34.Add(typeTalentsAlienSpecialist, skillEffectInfo34);
		SortedList<ushort, SkillEffectInfo> nums35 = PlayerItems.skillsInformation;
		ushort typeTalentsVelocity = PlayerItems.TypeTalentsVelocity;
		SkillEffectInfo skillEffectInfo35 = new SkillEffectInfo()
		{
			start_X_Ef = 5,
			start_Y_Ef = 0,
			levelEf_X = 5,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 7,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums35.Add(typeTalentsVelocity, skillEffectInfo35);
		SortedList<ushort, SkillEffectInfo> nums36 = PlayerItems.skillsInformation;
		ushort typeTalentsBountySpecialist = PlayerItems.TypeTalentsBountySpecialist;
		SkillEffectInfo skillEffectInfo36 = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 0,
			levelEf_X = 2,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 2,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums36.Add(typeTalentsBountySpecialist, skillEffectInfo36);
		SortedList<ushort, SkillEffectInfo> nums37 = PlayerItems.skillsInformation;
		ushort typeTalentsDamageReduction = PlayerItems.TypeTalentsDamageReduction;
		SkillEffectInfo skillEffectInfo37 = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 0,
			levelEf_X = 2,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 2,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums37.Add(typeTalentsDamageReduction, skillEffectInfo37);
		SortedList<ushort, SkillEffectInfo> nums38 = PlayerItems.skillsInformation;
		ushort typeTalentsArmorBreaker = PlayerItems.TypeTalentsArmorBreaker;
		SkillEffectInfo skillEffectInfo38 = new SkillEffectInfo()
		{
			start_X_Ef = 5,
			start_Y_Ef = 0,
			levelEf_X = 3,
			levelEf_Y = 0,
			neuronEf_X = 3,
			neuronEf_Y = 0,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums38.Add(typeTalentsArmorBreaker, skillEffectInfo38);
		SortedList<ushort, SkillEffectInfo> nums39 = PlayerItems.skillsInformation;
		ushort typeTalentsRealSteel = PlayerItems.TypeTalentsRealSteel;
		SkillEffectInfo skillEffectInfo39 = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 0,
			levelEf_X = 2,
			levelEf_Y = 0,
			neuronEf_X = 2,
			neuronEf_Y = 0,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums39.Add(typeTalentsRealSteel, skillEffectInfo39);
		SortedList<ushort, SkillEffectInfo> nums40 = PlayerItems.skillsInformation;
		ushort typeTalentsRocketeer = PlayerItems.TypeTalentsRocketeer;
		SkillEffectInfo skillEffectInfo40 = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 0,
			levelEf_X = 0,
			levelEf_Y = 10,
			neuronEf_X = 0,
			neuronEf_Y = 10,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums40.Add(typeTalentsRocketeer, skillEffectInfo40);
		SortedList<ushort, SkillEffectInfo> nums41 = PlayerItems.skillsInformation;
		ushort typeTalentsPowerControl = PlayerItems.TypeTalentsPowerControl;
		SkillEffectInfo skillEffectInfo41 = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 0,
			levelEf_X = 5,
			levelEf_Y = 0,
			neuronEf_X = 5,
			neuronEf_Y = 0,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums41.Add(typeTalentsPowerControl, skillEffectInfo41);
		SortedList<ushort, SkillEffectInfo> nums42 = PlayerItems.skillsInformation;
		ushort typeTalentsMassiveDamage = PlayerItems.TypeTalentsMassiveDamage;
		SkillEffectInfo skillEffectInfo42 = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 0,
			levelEf_X = 2,
			levelEf_Y = 0,
			neuronEf_X = 2,
			neuronEf_Y = 0,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums42.Add(typeTalentsMassiveDamage, skillEffectInfo42);
		SortedList<ushort, SkillEffectInfo> nums43 = PlayerItems.skillsInformation;
		ushort typeTalentsSpeedster = PlayerItems.TypeTalentsSpeedster;
		SkillEffectInfo skillEffectInfo43 = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 0,
			levelEf_X = 4,
			levelEf_Y = 0,
			neuronEf_X = 4,
			neuronEf_Y = 0,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums43.Add(typeTalentsSpeedster, skillEffectInfo43);
		SortedList<ushort, SkillEffectInfo> nums44 = PlayerItems.skillsInformation;
		ushort typeTalentsFutureTechnology = PlayerItems.TypeTalentsFutureTechnology;
		SkillEffectInfo skillEffectInfo44 = new SkillEffectInfo()
		{
			start_X_Ef = 20,
			start_Y_Ef = 0,
			levelEf_X = 15,
			levelEf_Y = 0,
			neuronEf_X = 15,
			neuronEf_Y = 0,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums44.Add(typeTalentsFutureTechnology, skillEffectInfo44);
		SortedList<ushort, SkillEffectInfo> nums45 = PlayerItems.skillsInformation;
		ushort typeTalentsDronePower = PlayerItems.TypeTalentsDronePower;
		SkillEffectInfo skillEffectInfo45 = new SkillEffectInfo()
		{
			start_X_Ef = 10,
			start_Y_Ef = 0,
			levelEf_X = 5,
			levelEf_Y = 0,
			neuronEf_X = 5,
			neuronEf_Y = 0,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums45.Add(typeTalentsDronePower, skillEffectInfo45);
		SortedList<ushort, SkillEffectInfo> nums46 = PlayerItems.skillsInformation;
		ushort typeTalentsRepairMaster = PlayerItems.TypeTalentsRepairMaster;
		SkillEffectInfo skillEffectInfo46 = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 0,
			levelEf_X = 2,
			levelEf_Y = 0,
			neuronEf_X = 2,
			neuronEf_Y = 0,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums46.Add(typeTalentsRepairMaster, skillEffectInfo46);
		SortedList<ushort, SkillEffectInfo> nums47 = PlayerItems.skillsInformation;
		ushort typeCouncilSkillDisarm = PlayerItems.TypeCouncilSkillDisarm;
		SkillEffectInfo skillEffectInfo47 = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 0,
			levelEf_X = 5,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 0,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums47.Add(typeCouncilSkillDisarm, skillEffectInfo47);
		SortedList<ushort, SkillEffectInfo> nums48 = PlayerItems.skillsInformation;
		ushort typeCouncilSkillSacrifice = PlayerItems.TypeCouncilSkillSacrifice;
		SkillEffectInfo skillEffectInfo48 = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 0,
			levelEf_X = 15,
			levelEf_Y = 50,
			neuronEf_X = 0,
			neuronEf_Y = 0,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums48.Add(typeCouncilSkillSacrifice, skillEffectInfo48);
		SortedList<ushort, SkillEffectInfo> nums49 = PlayerItems.skillsInformation;
		ushort typeCouncilSkillLifesteal = PlayerItems.TypeCouncilSkillLifesteal;
		SkillEffectInfo skillEffectInfo49 = new SkillEffectInfo()
		{
			start_X_Ef = 0,
			start_Y_Ef = 0,
			levelEf_X = 10,
			levelEf_Y = 0,
			neuronEf_X = 0,
			neuronEf_Y = 0,
			isChangableCooldown = false,
			levelCooldown = 0,
			neuronCooldown = 0
		};
		nums49.Add(typeCouncilSkillLifesteal, skillEffectInfo49);
	}

	private static void InitSpecialAmounts()
	{
		PlayerItems.specialAmounts = new SortedList<ushort, int>()
		{
			{ PlayerItems.TypeAmmoColdFusionCells, 100 },
			{ PlayerItems.TypeAmmoFusionCells, 500 },
			{ PlayerItems.TypeAmmoSolarCells, 200 },
			{ PlayerItems.TypeAmmoSulfurFusionCells, 100 }
		};
	}

	public static bool IsAmmo(ushort itemType)
	{
		return (itemType < 6000 ? false : itemType <= 6999);
	}

	public static bool IsAutoMinerBooster(ushort itemType)
	{
		return (itemType == PlayerItems.TypeBoosterAutominerFor1Days || itemType == PlayerItems.TypeBoosterAutominerFor3Days ? true : itemType == PlayerItems.TypeBoosterAutominerFor6Days);
	}

	public static bool IsBooster(ushort itemType)
	{
		return (itemType < 7000 ? false : itemType <= 7499);
	}

	public static bool IsCargoBooster(ushort itemType)
	{
		return (itemType == PlayerItems.TypeBoosterCargoFor1Days || itemType == PlayerItems.TypeBoosterCargoFor3Days ? true : itemType == PlayerItems.TypeBoosterCargoFor6Days);
	}

	public static bool IsCorpus(ushort itemType)
	{
		return (itemType < 1000 ? false : itemType <= 1999);
	}

	public static bool IsDamageBooster(ushort itemType)
	{
		return (itemType == PlayerItems.TypeBoosterDamageFor1Days || itemType == PlayerItems.TypeBoosterDamageFor3Days ? true : itemType == PlayerItems.TypeBoosterDamageFor6Days);
	}

	public static bool IsDbSlotableItem(ushort itemType)
	{
		return (PlayerItems.IsEngine(itemType) || PlayerItems.IsCorpus(itemType) || PlayerItems.IsShield(itemType) ? true : PlayerItems.IsExtra(itemType));
	}

	public static bool IsDbStackableItem(ushort itemType)
	{
		return (PlayerItems.IsAmmo(itemType) || PlayerItems.IsPortalPart(itemType) || PlayerItems.IsQuestItem(itemType) ? true : PlayerItems.IsMineral(itemType));
	}

	public static bool IsEngine(ushort itemType)
	{
		return (itemType < 3000 ? false : itemType <= 3999);
	}

	public static bool IsExperienceBooster(ushort itemType)
	{
		return (itemType == PlayerItems.TypeBoosterExperienceFor1Days || itemType == PlayerItems.TypeBoosterExperienceFor3Days ? true : itemType == PlayerItems.TypeBoosterExperienceFor6Days);
	}

	public static bool IsExtra(ushort itemType)
	{
		return (itemType < 5000 ? false : itemType <= 5999);
	}

	public static bool IsExtraCargo(ushort itemType)
	{
		return (itemType == PlayerItems.TypeExtraMolecularCompresor || itemType == PlayerItems.TypeExtraFusionCompresor || itemType == PlayerItems.TypeExtraNuclearCompresor ? true : itemType == PlayerItems.TypeExtraUltraNuclearCompresor);
	}

	public static bool IsExtraCargoMining(ushort itemType)
	{
		return (PlayerItems.IsExtraCargo(itemType) ? true : PlayerItems.IsExtraMining(itemType));
	}

	public static bool IsExtraCooldown(ushort itemType)
	{
		return (PlayerItems.IsForLaserCooldown(itemType) || PlayerItems.IsForPlasmaCooldown(itemType) || PlayerItems.IsForIonCooldown(itemType) ? true : itemType == PlayerItems.TypeExtraUltraWeaponsCoolant);
	}

	public static bool IsExtraDamage(ushort itemType)
	{
		return (PlayerItems.IsForLaserDamage(itemType) || PlayerItems.IsForPlasmaDamage(itemType) || PlayerItems.IsForIonDamage(itemType) ? true : itemType == PlayerItems.TypeExtraUltraWeaponsModule);
	}

	public static bool IsExtraMining(ushort itemType)
	{
		return (itemType == PlayerItems.TypeExtraLightMiningDrill ? true : itemType == PlayerItems.TypeExtraUltraMiningDrill);
	}

	public static bool IsExtraOther(ushort itemType)
	{
		return (itemType == PlayerItems.TypeExtraBasicCPUforShildRegen || itemType == PlayerItems.TypeExtraAdvancedCPUforShildRegen || itemType == PlayerItems.TypeExtraOverclockedCPUforShildRegen || itemType == PlayerItems.TypeExtraLaserAimingCPU || itemType == PlayerItems.TypeExtraCPUforShildRegen30 || itemType == PlayerItems.TypeExtraCPUforShildRegen40 || itemType == PlayerItems.TypeExtraCPUforShildRegen50 || itemType == PlayerItems.TypeExtraCPUforShildRegen60 || itemType == PlayerItems.TypeExtraCPUforShildRegen70 || itemType == PlayerItems.TypeExtraPlasmaAimingCPU || itemType == PlayerItems.TypeExtraIonAimingCPU ? true : itemType == PlayerItems.TypeExtraUltraAimingCPU);
	}

	public static bool IsForExtraCargoSpace(ushort itemType)
	{
		return (itemType == PlayerItems.TypeExtraMolecularCompresor || itemType == PlayerItems.TypeExtraFusionCompresor || itemType == PlayerItems.TypeExtraNuclearCompresor ? true : itemType == PlayerItems.TypeExtraUltraNuclearCompresor);
	}

	public static bool IsForIonCooldown(ushort itemType)
	{
		return (itemType == PlayerItems.TypeExtraIonWeaponsCoolant || itemType == PlayerItems.TypeExtraIonWeaponsCoolant1 || itemType == PlayerItems.TypeExtraIonWeaponsCoolant2 || itemType == PlayerItems.TypeExtraIonWeaponsCoolant3 || itemType == PlayerItems.TypeExtraIonWeaponsCoolant4 ? true : itemType == PlayerItems.TypeExtraIonWeaponsCoolant5);
	}

	public static bool IsForIonDamage(ushort itemType)
	{
		return (itemType == PlayerItems.TypeExtraIonWeaponsModule || itemType == PlayerItems.TypeExtraIonWeaponsModule1 || itemType == PlayerItems.TypeExtraIonWeaponsModule2 || itemType == PlayerItems.TypeExtraIonWeaponsModule3 || itemType == PlayerItems.TypeExtraIonWeaponsModule4 ? true : itemType == PlayerItems.TypeExtraIonWeaponsModule5);
	}

	public static bool IsForLaserCooldown(ushort itemType)
	{
		return (itemType == PlayerItems.TypeExtraLaserWeaponsCoolant || itemType == PlayerItems.TypeExtraLaserWeaponsCoolant1 || itemType == PlayerItems.TypeExtraLaserWeaponsCoolant2 || itemType == PlayerItems.TypeExtraLaserWeaponsCoolant3 || itemType == PlayerItems.TypeExtraLaserWeaponsCoolant4 ? true : itemType == PlayerItems.TypeExtraLaserWeaponsCoolant5);
	}

	public static bool IsForLaserDamage(ushort itemType)
	{
		return (itemType == PlayerItems.TypeExtraLaserWeaponsModule || itemType == PlayerItems.TypeExtraLaserWeaponsModule1 || itemType == PlayerItems.TypeExtraLaserWeaponsModule2 || itemType == PlayerItems.TypeExtraLaserWeaponsModule3 || itemType == PlayerItems.TypeExtraLaserWeaponsModule4 ? true : itemType == PlayerItems.TypeExtraLaserWeaponsModule5);
	}

	public static bool IsForPlasmaCooldown(ushort itemType)
	{
		return (itemType == PlayerItems.TypeExtraPlasmaWeaponsCoolant || itemType == PlayerItems.TypeExtraPlasmaWeaponsCoolant1 || itemType == PlayerItems.TypeExtraPlasmaWeaponsCoolant2 || itemType == PlayerItems.TypeExtraPlasmaWeaponsCoolant3 || itemType == PlayerItems.TypeExtraPlasmaWeaponsCoolant4 ? true : itemType == PlayerItems.TypeExtraPlasmaWeaponsCoolant5);
	}

	public static bool IsForPlasmaDamage(ushort itemType)
	{
		return (itemType == PlayerItems.TypeExtraPlasmaWeaponsModule || itemType == PlayerItems.TypeExtraPlasmaWeaponsModule1 || itemType == PlayerItems.TypeExtraPlasmaWeaponsModule2 || itemType == PlayerItems.TypeExtraPlasmaWeaponsModule3 || itemType == PlayerItems.TypeExtraPlasmaWeaponsModule4 ? true : itemType == PlayerItems.TypeExtraPlasmaWeaponsModule5);
	}

	public static bool IsForShieldRegen(ushort itemType)
	{
		return (itemType == PlayerItems.TypeExtraBasicCPUforShildRegen || itemType == PlayerItems.TypeExtraAdvancedCPUforShildRegen || itemType == PlayerItems.TypeExtraOverclockedCPUforShildRegen || itemType == PlayerItems.TypeExtraCPUforShildRegen30 || itemType == PlayerItems.TypeExtraCPUforShildRegen40 || itemType == PlayerItems.TypeExtraCPUforShildRegen50 || itemType == PlayerItems.TypeExtraCPUforShildRegen60 ? true : itemType == PlayerItems.TypeExtraCPUforShildRegen70);
	}

	public static bool IsGenericItem(ushort itemType)
	{
		return PlayerItems.genericItemsTypes.Contains(itemType);
	}

	public static bool IsInvestedNeuron(ushort itemType)
	{
		return (itemType < 9000 ? false : itemType <= 9999);
	}

	public static bool IsLearnedSkill(ushort itemType)
	{
		return (itemType < 8000 || itemType > 8999 || itemType == PlayerItems.TypeCouncilSkillDisarm || itemType == PlayerItems.TypeCouncilSkillSacrifice ? false : itemType != PlayerItems.TypeCouncilSkillLifesteal);
	}

	public static bool IsMineral(ushort itemType)
	{
		return (itemType < 11000 ? false : itemType <= 11999);
	}

	public static bool IsPackageDeal(ushort itemType)
	{
		return (itemType == PlayerItems.TypeBoosterPackageDeal || itemType == PlayerItems.TypePowerUpDamagePackageDeal || itemType == PlayerItems.TypePowerUpShieldPackageDeal || itemType == PlayerItems.TypePowerUpCorpusPackageDeal || itemType == PlayerItems.TypePowerUpShieldPowerPackageDeal || itemType == PlayerItems.TypePowerUpTargetingPackageDeal ? true : itemType == PlayerItems.TypePowerUpAvoidancePackageDeal);
	}

	public static bool IsPortalPart(ushort itemType)
	{
		return (itemType < 12000 ? false : itemType <= 12999);
	}

	public static bool IsPowerUp(ushort itemType)
	{
		return (itemType < 7500 ? false : itemType <= 7999);
	}

	public static bool IsQuestItem(ushort itemType)
	{
		return (itemType < 10000 ? false : itemType <= 10999);
	}

	public static bool IsShield(ushort itemType)
	{
		return (itemType < 2000 ? false : itemType <= 2999);
	}

	public static bool IsSlotable(ushort itemType)
	{
		return (PlayerItems.IsAmmo(itemType) || PlayerItems.IsEngine(itemType) || PlayerItems.IsMineral(itemType) || PlayerItems.IsShield(itemType) || PlayerItems.IsCorpus(itemType) || PlayerItems.IsWeapon(itemType) || PlayerItems.IsExtra(itemType) || PlayerItems.IsPortalPart(itemType) ? true : PlayerItems.IsQuestItem(itemType));
	}

	public static bool IsStackable(ushort itemType)
	{
		return (PlayerItems.IsAmmo(itemType) || PlayerItems.IsUniversalPortalPart(itemType) ? true : PlayerItems.IsQuestItem(itemType));
	}

	public static bool IsStructure(ushort itemType)
	{
		return (PlayerItems.IsCorpus(itemType) || PlayerItems.IsShield(itemType) ? true : PlayerItems.IsEngine(itemType));
	}

	public static bool IsTalent(ushort itemType)
	{
		return (itemType < 8000 ? false : itemType <= 8999);
	}

	public static bool IsUniquePortalPart(ushort itemType)
	{
		return (itemType == 12010 || itemType == 12011 || itemType == 12012 || itemType == 12013 || itemType == 12020 || itemType == 12021 || itemType == 12022 || itemType == 12023 || itemType == 12030 || itemType == 12031 || itemType == 12032 ? true : itemType == 12033);
	}

	public static bool IsUniversalPortalPart(ushort itemType)
	{
		return (itemType == 12014 || itemType == 12015 || itemType == 12016 || itemType == 12024 || itemType == 12025 || itemType == 12026 || itemType == 12034 || itemType == 12035 ? true : itemType == 12036);
	}

	public static bool IsWeapon(ushort itemType)
	{
		return (PlayerItems.IsWeaponLaser(itemType) || PlayerItems.IsWeaponPlasma(itemType) ? true : PlayerItems.IsWeaponIon(itemType));
	}

	public static bool IsWeaponIon(ushort itemType)
	{
		return (itemType == PlayerItems.TypeWeaponIonTire1 || itemType == PlayerItems.TypeWeaponIonTire2 || itemType == PlayerItems.TypeWeaponIonTire3 || itemType == PlayerItems.TypeWeaponIonTire4 ? true : itemType == PlayerItems.TypeWeaponIonTire5);
	}

	public static bool IsWeaponLaser(ushort itemType)
	{
		return (itemType == PlayerItems.TypeWeaponLaserTire1 || itemType == PlayerItems.TypeWeaponLaserTire2 || itemType == PlayerItems.TypeWeaponLaserTire3 || itemType == PlayerItems.TypeWeaponLaserTire4 ? true : itemType == PlayerItems.TypeWeaponLaserTire5);
	}

	public static bool IsWeaponPlasma(ushort itemType)
	{
		return (itemType == PlayerItems.TypeWeaponPlasmaTire1 || itemType == PlayerItems.TypeWeaponPlasmaTire2 || itemType == PlayerItems.TypeWeaponPlasmaTire3 || itemType == PlayerItems.TypeWeaponPlasmaTire4 ? true : itemType == PlayerItems.TypeWeaponPlasmaTire5);
	}

	public void LearnTalentToLevel(ushort skillId, byte level, out int spendetSkillPoints)
	{
		int amountAt = (int)this.GetAmountAt(skillId);
		spendetSkillPoints = level - amountAt;
		if ((spendetSkillPoints <= 0 ? false : this.GetAmountAt(PlayerItems.TypeTalentPoint) >= (long)spendetSkillPoints))
		{
			if (level <= ((TalentsInfo)(
				from t in StaticData.allTypes.Values
				where t.itemType == skillId
				select t).First<PlayerItemTypesData>()).maxLevel)
			{
				this.Add(PlayerItems.TypeTalentPoint, (long)(-spendetSkillPoints));
				this.Set(skillId, (long)level);
			}
		}
	}

	public MakeSynthesisResult MakeSynthesis(ushort targetProduct, long amount = 1L, byte fusionPriceOff = 0)
	{
		int i;
		ushort item;
		MakeSynthesisResult makeSynthesisResult;
		if (amount >= (long)0)
		{
			SortedList<ushort, long> nums = new SortedList<ushort, long>();
			for (i = 0; i < PlayerItems.fusionDependancies[targetProduct].Count; i++)
			{
				item = PlayerItems.fusionDependancies[targetProduct].Keys[i];
				int num = (int)((float)PlayerItems.fusionDependancies[targetProduct][item] * (1f - (float)fusionPriceOff / 100f));
				nums.Add(item, (long)Math.Max(num, 1));
			}
			i = 0;
			while (i < nums.Count)
			{
				item = nums.Keys[i];
				if (this.GetMinetalQty(item) >= nums[item] * amount)
				{
					i++;
				}
				else
				{
					makeSynthesisResult = MakeSynthesisResult.NotEnoughMinerals;
					return makeSynthesisResult;
				}
			}
			for (i = 0; i < nums.Count; i++)
			{
				item = nums.Keys[i];
				this.UseMineral(item, (int)(nums[item] * amount));
			}
			int num1 = 1;
			num1 = (!PlayerItems.specialAmounts.ContainsKey(targetProduct) ? 1 : PlayerItems.specialAmounts[targetProduct]);
			if (targetProduct != PlayerItems.TypeEquilibrium)
			{
				this.AddMineral(targetProduct, (int)amount * num1);
			}
			else
			{
				this.Add(targetProduct, (long)((int)amount * num1));
			}
			makeSynthesisResult = MakeSynthesisResult.OK;
		}
		else
		{
			makeSynthesisResult = MakeSynthesisResult.NotEnoughMinerals;
		}
		return makeSynthesisResult;
	}

	public int MaxSynthesis(ushort targetProduct, byte fusionPriceOffBonus = 0)
	{
		int i;
		ushort item;
		int num;
		SortedList<ushort, long> nums = new SortedList<ushort, long>();
		for (i = 0; i < PlayerItems.fusionDependancies[targetProduct].Count; i++)
		{
			item = PlayerItems.fusionDependancies[targetProduct].Keys[i];
			int item1 = (int)((float)PlayerItems.fusionDependancies[targetProduct][item] * (1f - (float)fusionPriceOffBonus / 100f));
			nums.Add(item, (long)Math.Max(item1, 1));
		}
		if (nums.Keys.Count >= 1)
		{
			int minetalQty = (int)(this.GetMinetalQty(nums.Keys[0]) / nums[nums.Keys[0]]);
			for (i = 0; i < nums.Count; i++)
			{
				item = nums.Keys[i];
				minetalQty = (int)Math.Min((long)minetalQty, this.GetMinetalQty(item) / nums[item]);
			}
			num = minetalQty;
		}
		else
		{
			num = 0;
		}
		return num;
	}

	public void MoveSlotItem(MoveSlotItemData prms)
	{
		SlotItem slotItem = (
			from si in this.slotItems
			where (si.SlotType != prms.srcSlotType || si.Slot != prms.srcSlot ? false : si.ShipId == prms.srcShipId)
			select si).First<SlotItem>();
		if (!((
			from si in this.slotItems
			where (si.SlotType != prms.dstSlotType || si.Slot != prms.dstSlot ? false : si.ShipId == prms.dstShipId)
			select si).FirstOrDefault<SlotItem>() == null ? true : prms.isSwap))
		{
			Console.WriteLine("MoveSlotItem destination slot mismatch");
		}
		else if (!prms.isSwap)
		{
			slotItem.ShipId = prms.dstShipId;
			slotItem.IsActive = true;
			slotItem.SlotType = (byte)prms.dstSlotType;
			slotItem.Slot = prms.dstSlot;
		}
		else
		{
			SlotItem slot = (
				from si in this.slotItems
				where (si.SlotType != prms.dstSlotType || si.Slot != prms.dstSlot ? false : si.ShipId == prms.dstShipId)
				select si).First<SlotItem>();
			ushort num = slot.Slot;
			slot.Slot = slotItem.Slot;
			slotItem.Slot = num;
			byte slotType = slot.SlotType;
			slot.SlotType = slotItem.SlotType;
			slotItem.SlotType = slotType;
			int shipId = slot.ShipId;
			slot.ShipId = slotItem.ShipId;
			slotItem.ShipId = shipId;
			bool isActive = slot.IsActive;
			slot.IsActive = slotItem.IsActive;
			slotItem.IsActive = isActive;
		}
	}

	public void PrepareToSerialize()
	{
		this.keys = this.items.Keys.ToArray<ushort>();
		this.values = this.items.Values.ToArray<long>();
	}

	private void RegisterPlayerItemToAdd(ushort key)
	{
		lock (this.toAdd)
		{
			if (!this.toAdd.Contains(key))
			{
				this.toAdd.Add(key);
			}
		}
	}

	private void RegisterPlayerItemToUpdate(ushort key)
	{
		lock (this.toUpdate)
		{
			if (!this.toUpdate.Contains(key))
			{
				this.toUpdate.Add(key);
			}
		}
	}

	public void Reload(PlayerItems newItems)
	{
		this.items = newItems.items;
		this.slotItems = newItems.slotItems;
		this.portalParts = newItems.portalParts;
	}

	public RepairResult RepairShip(RepairType repairType, int repairPrice)
	{
		RepairResult repairResult;
		switch (repairType)
		{
			case RepairType.Cash:
			{
				if (this.Cash < (long)repairPrice)
				{
					repairPrice = (int)this.Cash;
				}
				this.AddCash((long)(-repairPrice));
				repairResult = RepairResult.OK;
				break;
			}
			case RepairType.Voucher:
			{
				if (this.GetAmountAt(PlayerItems.TypeRepairVaucher) >= (long)1)
				{
					this.Add(PlayerItems.TypeRepairVaucher, (long)-1);
					repairResult = RepairResult.OK;
					break;
				}
				else
				{
					repairResult = RepairResult.NotEnoughVouchers;
					break;
				}
			}
			default:
			{
				goto case RepairType.Cash;
			}
		}
		return repairResult;
	}

	public void RestoreAfterDeserialize()
	{
		this.items = new SortedList<ushort, long>();
		int length = (int)this.keys.Length;
		for (int i = 0; i < length; i++)
		{
			this.items.Add(this.keys[i], this.values[i]);
		}
	}

	public void SellResources(ushort resId, int qty, out int sellPrice, int bonus = 0)
	{
		sellPrice = 0;
		if ((qty < 0 ? false : (long)qty <= this.GetMinetalQty(resId)))
		{
			if (resId != PlayerItems.TypeEquilibrium)
			{
				this.UseMineral(resId, qty);
			}
			else
			{
				this.Add(PlayerItems.TypeEquilibrium, (long)(-qty));
			}
			sellPrice = PlayerItems.CalculateSellPrice(resId, bonus) * qty;
			this.AddCash((long)sellPrice);
		}
		else
		{
			Console.WriteLine("(!) SellResources bad qty:{0} for res:{1}", qty, resId);
		}
	}

	public void SellSlotItem(ushort slotType, byte slot, int shipId, out int sellPrice, int bonus = 0)
	{
		sellPrice = 0;
		SlotItem slotItem = (
			from si in this.slotItems
			where (si.SlotType != slotType || si.Slot != slot ? false : si.ShipId == shipId)
			select si).First<SlotItem>();
		this.slotItems.Remove(slotItem);
		if (slotItem.Amount > 0)
		{
			int num = PlayerItems.CalculateSlotItemSellPrice(slotItem, bonus);
			if (PlayerItems.IsAmmo(slotItem.ItemType))
			{
				sellPrice = (int)((float)(num * slotItem.Amount) / 100f);
			}
			else if (!PlayerItems.IsStackable(slotItem.ItemType))
			{
				sellPrice = num;
			}
			else
			{
				sellPrice = num * slotItem.Amount;
			}
			this.AddCash((long)sellPrice);
		}
	}

	public void SellStackableItem(ushort slotType, byte slot, int shipId, int amount, out int sellPrice, int bonus = 0)
	{
		sellPrice = 0;
		SlotItem slotItem = (
			from si in this.slotItems
			where (si.SlotType != slotType || si.Slot != slot ? false : si.ShipId == shipId)
			select si).First<SlotItem>();
		if (slotItem.Amount > 0)
		{
			if (amount <= slotItem.Amount)
			{
				if (amount != slotItem.Amount)
				{
					SlotItem slotItem1 = slotItem;
					slotItem1.Amount = slotItem1.Amount - amount;
				}
				else
				{
					this.slotItems.Remove(slotItem);
				}
				int num = PlayerItems.CalculateSlotItemSellPrice(slotItem, bonus);
				if (PlayerItems.IsAmmo(slotItem.ItemType))
				{
					sellPrice = (int)((float)(num * amount) / 100f);
				}
				else if (PlayerItems.IsStackable(slotItem.ItemType))
				{
					sellPrice = num * amount;
				}
				this.AddCash((long)sellPrice);
			}
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		int i;
		bw.Write((int)this.keys.Length);
		for (i = 0; i < (int)this.keys.Length; i++)
		{
			bw.Write(this.keys[i]);
		}
		bw.Write((int)this.values.Length);
		for (i = 0; i < (int)this.keys.Length; i++)
		{
			bw.Write(this.values[i]);
		}
		bw.Write(this.slotItems.Count);
		for (i = 0; i < this.slotItems.Count; i++)
		{
			TransferablesFramework.SerializeITransferable(bw, this.slotItems[i], TransferContext.None);
		}
		bw.Write(this.portalParts.Count);
		for (i = 0; i < this.portalParts.Count; i++)
		{
			TransferablesFramework.SerializeITransferable(bw, this.portalParts[i], TransferContext.None);
		}
	}

	public void Set(ushort key, long val)
	{
		if (this.items.ContainsKey(key))
		{
			this.items[key] = val;
			this.RegisterPlayerItemToUpdate(key);
		}
		else
		{
			this.items.Add(key, val);
			this.RegisterPlayerItemToAdd(key);
		}
	}

	public static void SetAvatar(ushort itemType, out string bundelName, out string assetName)
	{
		bundelName = string.Empty;
		assetName = string.Empty;
		if (StaticData.allTypes.ContainsKey(itemType))
		{
			assetName = StaticData.allTypes[itemType].assetName;
			if (PlayerItems.IsAmmo(itemType))
			{
				bundelName = "AmmosAvatars";
			}
			else if (PlayerItems.IsCorpus(itemType))
			{
				bundelName = "CorpusesAvatars";
			}
			else if (PlayerItems.IsShield(itemType))
			{
				bundelName = "ShieldsAvatars";
			}
			else if (PlayerItems.IsEngine(itemType))
			{
				bundelName = "EnginesAvatars";
			}
			else if (PlayerItems.IsExtra(itemType))
			{
				bundelName = "ExtrasAvatars";
			}
			else if (PlayerItems.IsWeapon(itemType))
			{
				bundelName = "WeaponsAvatars";
			}
			else if (!(PlayerItems.IsPortalPart(itemType) || itemType == PlayerItems.TypePortalKey || itemType == PlayerItems.TypePortalSegment || itemType == PlayerItems.TypePortalPillar || itemType == PlayerItems.TypePortalCharger ? false : itemType != PlayerItems.TypePortalSchematic))
			{
				bundelName = "PortalPartsAvatars";
			}
			else if (PlayerItems.IsQuestItem(itemType))
			{
				bundelName = "QuestItemsAvatars";
			}
			else if (PlayerItems.IsBooster(itemType))
			{
				bundelName = "BoostersAvatars";
			}
			else if (PlayerItems.IsPowerUp(itemType))
			{
				bundelName = "PowerUpsWindow";
			}
			else if (!(itemType == PlayerItems.TypeCash || itemType == PlayerItems.TypeNova || itemType == PlayerItems.TypeUltralibrium || itemType == PlayerItems.TypeEquilibrium || itemType == PlayerItems.TypeTier2Resources || itemType == PlayerItems.TypeNeuron ? false : itemType != PlayerItems.TypeHonor))
			{
				bundelName = "Shop";
			}
			else if (!PlayerItems.IsMineral(itemType))
			{
				bundelName = "Shops";
				assetName = "ops";
			}
			else
			{
				bundelName = "MineralsAvatars";
			}
		}
	}

	public void SpendAmmo(ushort key)
	{
		int i;
		List<SlotItem> slotItems = new List<SlotItem>();
		int num = this.slotItems.Count<SlotItem>();
		for (i = 0; i < num; i++)
		{
			if ((this.slotItems[i].SlotType != 1 ? false : this.slotItems[i].ItemType == key))
			{
				slotItems.Add(this.slotItems[i]);
			}
		}
		if (slotItems.Count<SlotItem>() != 0)
		{
			SlotItem item = slotItems[0];
			num = slotItems.Count<SlotItem>();
			for (i = 0; i < num; i++)
			{
				if (slotItems[i].Amount < item.Amount)
				{
					item = slotItems[i];
				}
			}
			if (item.Amount <= 1)
			{
				item.Amount = 0;
				this.slotItems.Remove(item);
			}
			else
			{
				SlotItem amount = item;
				amount.Amount = amount.Amount - 1;
			}
		}
	}

	public int SpentTalentPoints(int talentClass)
	{
		int amountAt = 0;
		IEnumerable<PlayerItemTypesData> list = (
			from t in StaticData.allTypes.Values
			where (!PlayerItems.IsTalent(t.itemType) ? false : ((TalentsInfo)t).talentsClass == talentClass)
			select t).ToList<PlayerItemTypesData>();
		foreach (TalentsInfo talentsInfo in list)
		{
			amountAt = amountAt + (int)this.GetAmountAt(talentsInfo.itemType);
		}
		return amountAt;
	}

	public void StackSlotItem(StackSlotItemData prms)
	{
		SlotItem slotItem = (
			from si in this.slotItems
			where (si.SlotType != prms.srcSlotType || si.Slot != prms.srcSlot ? false : si.ShipId == prms.srcShipId)
			select si).First<SlotItem>();
		SlotItem slotItem1 = (
			from si in this.slotItems
			where (si.SlotType != prms.dstSlotType || si.Slot != prms.dstSlot ? false : si.ShipId == prms.dstShipId)
			select si).FirstOrDefault<SlotItem>();
		if (slotItem.Amount != prms.transferAmount)
		{
			SlotItem amount = slotItem;
			amount.Amount = amount.Amount - prms.transferAmount;
		}
		else
		{
			this.DumpSlotItem(slotItem.SlotType, (byte)slotItem.Slot, slotItem.ShipId);
		}
		if (slotItem1 == null)
		{
			slotItem1 = new SlotItem()
			{
				Amount = prms.transferAmount,
				SlotType = (byte)prms.dstSlotType,
				Slot = prms.dstSlot,
				ItemType = slotItem.ItemType
			};
			this.AddSlotItem(slotItem1, (int)prms.dstSlot, (int)prms.dstSlotType, prms.dstShipId);
		}
		else
		{
			SlotItem amount1 = slotItem1;
			amount1.Amount = amount1.Amount + prms.transferAmount;
		}
	}

	public void Substract(ushort key, long amount)
	{
		if (!this.items.ContainsKey(key))
		{
			throw new Exception("Should not happen!");
		}
		SortedList<ushort, long> item = this.items;
		SortedList<ushort, long> nums = item;
		ushort num = key;
		item[num] = nums[num] - amount;
		if (this.items[key] < (long)0)
		{
			this.items[key] = (long)0;
		}
	}

	public void UpgradeTalentWithNeurons(int key)
	{
		if (this.GetAmountAt(14) > (long)0)
		{
			if (this.GetAmountAt((ushort)key) < (long)5)
			{
				this.Add(14, (long)-1);
				this.Add((ushort)key, (long)1);
			}
		}
	}

	public UpgradeResult UpgradeWeapon(int damageUp, int cooldownUp, int rangeUp, int penetrationUp, int targetingUp, int slotId, byte slotType, int shipId)
	{
		UpgradeResult upgradeResult;
		SlotItemWeapon slotItemWeapon = (SlotItemWeapon)(
			from s in this.slotItems
			where (s.Slot != slotId || s.SlotType != slotType ? false : s.ShipId == shipId)
			select s).First<SlotItem>();
		int upgradeDamage = slotItemWeapon.UpgradeDamage + slotItemWeapon.UpgradeCooldown + slotItemWeapon.UpgradePenetration + slotItemWeapon.UpgradeRange + slotItemWeapon.UpgradeTargeting;
		int num = damageUp + cooldownUp + rangeUp + penetrationUp + targetingUp;
		long num1 = (long)0;
		if (num + upgradeDamage <= 15)
		{
			long num2 = (long)0;
			for (int i = upgradeDamage; i < num + upgradeDamage; i++)
			{
				WeaponsTypeNet item = (WeaponsTypeNet)StaticData.allTypes[slotItemWeapon.ItemType];
				num1 = num1 + (i < 9 ? item.upgrades[i].price : item.upgrades[9].price);
			}
			if (this.Cash >= num1)
			{
				SlotItemWeapon upgradeDamage1 = slotItemWeapon;
				upgradeDamage1.UpgradeDamage = (byte)(upgradeDamage1.UpgradeDamage + (byte)damageUp);
				SlotItemWeapon upgradeCooldown = slotItemWeapon;
				upgradeCooldown.UpgradeCooldown = (byte)(upgradeCooldown.UpgradeCooldown + (byte)cooldownUp);
				SlotItemWeapon upgradePenetration = slotItemWeapon;
				upgradePenetration.UpgradePenetration = (byte)(upgradePenetration.UpgradePenetration + (byte)penetrationUp);
				SlotItemWeapon upgradeRange = slotItemWeapon;
				upgradeRange.UpgradeRange = (byte)(upgradeRange.UpgradeRange + (byte)rangeUp);
				SlotItemWeapon upgradeTargeting = slotItemWeapon;
				upgradeTargeting.UpgradeTargeting = (byte)(upgradeTargeting.UpgradeTargeting + (byte)targetingUp);
				this.AddCash(-num1);
				upgradeResult = UpgradeResult.OK;
			}
			else
			{
				upgradeResult = UpgradeResult.NotEnoughCash;
			}
		}
		else
		{
			upgradeResult = UpgradeResult.InvalidParam;
		}
		return upgradeResult;
	}

	public void UseMineral(ushort mineralKey, int ammount)
	{
		List<SlotItem> slotItems = new List<SlotItem>();
		if (this.HaveThatResourceInCargoSlots(mineralKey))
		{
			SlotItem[] array = (
				from a in this.slotItems
				where (a.ItemType != mineralKey ? false : a.SlotType == 3)
				select a into t
				orderby t.Amount
				select t).ToArray<SlotItem>();
			SlotItem[] slotItemArray = array;
			int num = 0;
			while (num < (int)slotItemArray.Length)
			{
				SlotItem slotItem = slotItemArray[num];
				if (slotItem.Amount <= ammount)
				{
					ammount = ammount - slotItem.Amount;
					slotItem.Amount = 0;
					slotItems.Add(slotItem);
					num++;
				}
				else
				{
					SlotItem amount = slotItem;
					amount.Amount = amount.Amount - ammount;
					ammount = 0;
					break;
				}
			}
		}
		foreach (SlotItem slotItem1 in slotItems)
		{
			this.slotItems.Remove(slotItem1);
		}
	}
}