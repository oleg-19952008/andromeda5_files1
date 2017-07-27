using System;
using System.IO;

public class ShipConfiguration : ITransferable
{
	public short mapIndex;

	public int hitPoints;

	public int hitPointsMax;

	public float shield;

	public int shieldMax;

	public int cargoMax;

	public int targeting;

	public int targetingForLaser;

	public int targetingForPlasma;

	public int targetingForIon;

	public float avoidanceMax;

	public float currentAvoidance;

	public string assetName;

	public WeaponSlot[] weaponSlots;

	public int skillDamage;

	public float maxRotationSpeed;

	public float floatUpSpeed;

	public string shipName = "Boar";

	public string playerName = "Dave Mustain";

	public short playerLevel;

	public float acceleration;

	public float backAcceleration;

	public float currentVelocity;

	public float velocityMax;

	public float mass;

	public float distanceToStartDecelerate = 3f;

	[NonSerialized]
	public PlayerItems playerItems;

	public float dmgPercentForPlayer;

	public float dmgPercentForAlien;

	public float dmgPercentForAllWeapon;

	public int dmgFlatBonusForEachLaser;

	public int dmgFlatBonusForEachPlasma;

	public int dmgFlatBonusForEachIon;

	public float dmgPercentBonusForEachLaser;

	public float dmgPercentBonusForEachPlasma;

	public float dmgPercentBonusForEachIon;

	public float laserCooldown = 0f;

	public float plasmaCooldown = 0f;

	public float ionCooldown = 0f;

	public float fasterMining = 0f;

	public float shieldRepairPerSec = 0f;

	public float experienceGain = 0f;

	public float damageReductionItems = 0f;

	public float resilience = 0f;

	public int sellBonus = 0;

	public byte fusionPriceOff = 0;

	public byte ammoCreationBonus = 0;

	public byte epIncomeBonus = 0;

	public bool cargoBooster = false;

	public bool damageBooster = false;

	public bool miningBooster = false;

	public bool experienceBooster = false;

	public float criticalEnergyDrop;

	public float criticalEnergy;

	public float criticalEnergyMax;

	public float maxBoostedSpeed;

	public float speedBoostConsumption;

	public bool haveLaserDamageFlat = false;

	public bool havePlasmaDamageFlat = false;

	public bool haveIonDamageFlat = false;

	public bool haveLaserDamagePercentage = false;

	public bool havePlasmaDamagePercentage = false;

	public bool haveIonDamagePercentage = false;

	public bool haveTotalDamagePercentage = false;

	public bool haveCorpusFlat = false;

	public bool haveCorpusPercentage = false;

	public bool haveShieldFlat = false;

	public bool haveShieldPercentage = false;

	public bool haveEndurancePercentage = false;

	public bool haveShieldPowerFlat = false;

	public bool haveShieldPowerPercentage = false;

	public bool haveTargetingFlat = false;

	public bool haveTargetingPercentage = false;

	public bool haveAvoidanceFlat = false;

	public bool haveAvoidancePercentage = false;

	public float preCalculatedDamageBonusPercent;

	public float alienSpecialistBonusPercent;

	public float bountySpecialistBonusPercent;

	public float findWeakSpotChancePercent;

	public float defenceRate;

	public float attackRate;

	public float sunderArmorBonusPercent;

	public float mistShroundBonusPercent;

	public ShipConfiguration()
	{
	}

	public void CopyPropsTo(ShipConfiguration copyTarget)
	{
		copyTarget.acceleration = this.acceleration;
		copyTarget.assetName = this.assetName;
		copyTarget.backAcceleration = this.backAcceleration;
		copyTarget.distanceToStartDecelerate = this.distanceToStartDecelerate;
		copyTarget.floatUpSpeed = this.floatUpSpeed;
		copyTarget.hitPoints = this.hitPoints;
		copyTarget.hitPointsMax = this.hitPointsMax;
		copyTarget.mapIndex = this.mapIndex;
		copyTarget.mass = this.mass;
		copyTarget.maxRotationSpeed = this.maxRotationSpeed;
		copyTarget.velocityMax = this.velocityMax;
		copyTarget.currentVelocity = this.currentVelocity;
		copyTarget.playerName = this.playerName;
		copyTarget.shipName = this.shipName;
		copyTarget.shield = this.shield;
		copyTarget.shieldMax = this.shieldMax;
		copyTarget.cargoMax = this.cargoMax;
		copyTarget.targeting = this.targeting;
		copyTarget.targetingForLaser = this.targetingForLaser;
		copyTarget.targetingForPlasma = this.targetingForPlasma;
		copyTarget.targetingForIon = this.targetingForIon;
		copyTarget.avoidanceMax = this.avoidanceMax;
		copyTarget.currentAvoidance = this.currentAvoidance;
		copyTarget.speedBoostConsumption = this.speedBoostConsumption;
		copyTarget.maxBoostedSpeed = this.maxBoostedSpeed;
		copyTarget.dmgFlatBonusForEachLaser = this.dmgFlatBonusForEachLaser;
		copyTarget.dmgFlatBonusForEachPlasma = this.dmgFlatBonusForEachPlasma;
		copyTarget.dmgFlatBonusForEachIon = this.dmgFlatBonusForEachIon;
		copyTarget.dmgPercentBonusForEachLaser = this.dmgPercentBonusForEachLaser;
		copyTarget.dmgPercentBonusForEachPlasma = this.dmgPercentBonusForEachPlasma;
		copyTarget.dmgPercentBonusForEachIon = this.dmgPercentBonusForEachIon;
		copyTarget.dmgPercentForPlayer = this.dmgPercentForPlayer;
		copyTarget.dmgPercentForAlien = this.dmgPercentForAlien;
		copyTarget.dmgPercentForAllWeapon = this.dmgPercentForAllWeapon;
		copyTarget.laserCooldown = this.laserCooldown;
		copyTarget.plasmaCooldown = this.plasmaCooldown;
		copyTarget.ionCooldown = this.ionCooldown;
		copyTarget.fasterMining = this.fasterMining;
		copyTarget.shieldRepairPerSec = this.shieldRepairPerSec;
		copyTarget.experienceGain = this.experienceGain;
		copyTarget.damageReductionItems = this.damageReductionItems;
		copyTarget.resilience = this.resilience;
		copyTarget.sellBonus = this.sellBonus;
		copyTarget.fusionPriceOff = this.fusionPriceOff;
		copyTarget.ammoCreationBonus = this.ammoCreationBonus;
		copyTarget.epIncomeBonus = this.epIncomeBonus;
		copyTarget.playerLevel = this.playerLevel;
		copyTarget.skillDamage = this.skillDamage;
		copyTarget.haveLaserDamageFlat = this.haveLaserDamageFlat;
		copyTarget.havePlasmaDamageFlat = this.havePlasmaDamageFlat;
		copyTarget.haveIonDamageFlat = this.haveIonDamageFlat;
		copyTarget.haveLaserDamagePercentage = this.haveLaserDamagePercentage;
		copyTarget.havePlasmaDamagePercentage = this.havePlasmaDamagePercentage;
		copyTarget.haveIonDamagePercentage = this.haveIonDamagePercentage;
		copyTarget.haveTotalDamagePercentage = this.haveTotalDamagePercentage;
		copyTarget.haveCorpusFlat = this.haveCorpusFlat;
		copyTarget.haveCorpusPercentage = this.haveCorpusPercentage;
		copyTarget.haveShieldFlat = this.haveShieldFlat;
		copyTarget.haveShieldPercentage = this.haveShieldPercentage;
		copyTarget.haveEndurancePercentage = this.haveEndurancePercentage;
		copyTarget.haveShieldPowerFlat = this.haveShieldPowerFlat;
		copyTarget.haveShieldPowerPercentage = this.haveShieldPowerPercentage;
		copyTarget.haveTargetingFlat = this.haveTargetingFlat;
		copyTarget.haveTargetingPercentage = this.haveTargetingPercentage;
		copyTarget.haveAvoidanceFlat = this.haveAvoidanceFlat;
		copyTarget.haveAvoidancePercentage = this.haveAvoidancePercentage;
	}

	public void Deserialize(BinaryReader br)
	{
		this.mapIndex = br.ReadInt16();
		this.hitPoints = br.ReadInt32();
		this.hitPointsMax = br.ReadInt32();
		this.shield = br.ReadSingle();
		this.shieldMax = br.ReadInt32();
		this.cargoMax = br.ReadInt32();
		this.assetName = br.ReadString();
		int num = br.ReadInt32();
		this.weaponSlots = new WeaponSlot[num];
		for (int i = 0; i < num; i++)
		{
			this.weaponSlots[i] = new WeaponSlot();
			this.weaponSlots[i].Deserialize(br);
		}
		this.skillDamage = br.ReadInt32();
		this.maxRotationSpeed = br.ReadSingle();
		this.floatUpSpeed = br.ReadSingle();
		this.acceleration = br.ReadSingle();
		this.backAcceleration = br.ReadSingle();
		this.currentVelocity = br.ReadSingle();
		this.velocityMax = br.ReadSingle();
		this.mass = br.ReadSingle();
		this.distanceToStartDecelerate = br.ReadSingle();
		this.shipName = br.ReadString();
		this.dmgPercentForPlayer = br.ReadSingle();
		this.dmgPercentForAlien = br.ReadSingle();
		this.dmgPercentForAllWeapon = br.ReadSingle();
		this.dmgPercentBonusForEachLaser = br.ReadSingle();
		this.dmgPercentBonusForEachPlasma = br.ReadSingle();
		this.dmgPercentBonusForEachIon = br.ReadSingle();
		this.dmgFlatBonusForEachLaser = br.ReadInt32();
		this.dmgFlatBonusForEachPlasma = br.ReadInt32();
		this.dmgFlatBonusForEachIon = br.ReadInt32();
		this.laserCooldown = br.ReadSingle();
		this.plasmaCooldown = br.ReadSingle();
		this.ionCooldown = br.ReadSingle();
		this.fasterMining = br.ReadSingle();
		this.shieldRepairPerSec = br.ReadSingle();
		this.experienceGain = br.ReadSingle();
		this.damageReductionItems = br.ReadSingle();
		this.resilience = br.ReadSingle();
		this.sellBonus = br.ReadInt32();
		this.fusionPriceOff = br.ReadByte();
		this.ammoCreationBonus = br.ReadByte();
		this.epIncomeBonus = br.ReadByte();
		this.playerLevel = br.ReadInt16();
		this.targeting = br.ReadInt32();
		this.targetingForLaser = br.ReadInt32();
		this.targetingForPlasma = br.ReadInt32();
		this.targetingForIon = br.ReadInt32();
		this.avoidanceMax = br.ReadSingle();
		this.currentAvoidance = br.ReadSingle();
		this.speedBoostConsumption = br.ReadSingle();
		this.maxBoostedSpeed = br.ReadSingle();
		this.haveLaserDamageFlat = br.ReadBoolean();
		this.havePlasmaDamageFlat = br.ReadBoolean();
		this.haveIonDamageFlat = br.ReadBoolean();
		this.haveLaserDamagePercentage = br.ReadBoolean();
		this.havePlasmaDamagePercentage = br.ReadBoolean();
		this.haveIonDamagePercentage = br.ReadBoolean();
		this.haveTotalDamagePercentage = br.ReadBoolean();
		this.haveCorpusFlat = br.ReadBoolean();
		this.haveCorpusPercentage = br.ReadBoolean();
		this.haveShieldFlat = br.ReadBoolean();
		this.haveShieldPercentage = br.ReadBoolean();
		this.haveEndurancePercentage = br.ReadBoolean();
		this.haveShieldPowerFlat = br.ReadBoolean();
		this.haveShieldPowerPercentage = br.ReadBoolean();
		this.haveTargetingFlat = br.ReadBoolean();
		this.haveTargetingPercentage = br.ReadBoolean();
		this.haveAvoidanceFlat = br.ReadBoolean();
		this.haveAvoidancePercentage = br.ReadBoolean();
	}

	public void PrepareShootTimesToTransport()
	{
		long ticks = StaticData.now.Ticks;
		WeaponSlot[] weaponSlotArray = this.weaponSlots;
		for (int i = 0; i < (int)weaponSlotArray.Length; i++)
		{
			WeaponSlot weaponSlot = weaponSlotArray[i];
			if (weaponSlot.lastShotTime != (long)0)
			{
				weaponSlot.timeToNextShot = (int)((weaponSlot.lastShotTime + weaponSlot.realReloadTime - ticks) / (long)10000);
			}
			else
			{
				weaponSlot.timeToNextShot = 0;
			}
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.mapIndex);
		bw.Write(this.hitPoints);
		bw.Write(this.hitPointsMax);
		bw.Write(this.shield);
		bw.Write(this.shieldMax);
		bw.Write(this.cargoMax);
		bw.Write(this.assetName ?? "");
		int length = (int)this.weaponSlots.Length;
		bw.Write(length);
		for (int i = 0; i < length; i++)
		{
			this.weaponSlots[i].Serialize(bw);
		}
		bw.Write(this.skillDamage);
		bw.Write(this.maxRotationSpeed);
		bw.Write(this.floatUpSpeed);
		bw.Write(this.acceleration);
		bw.Write(this.backAcceleration);
		bw.Write(this.currentVelocity);
		bw.Write(this.velocityMax);
		bw.Write(this.mass);
		bw.Write(this.distanceToStartDecelerate);
		bw.Write(this.shipName);
		bw.Write(this.dmgPercentForPlayer);
		bw.Write(this.dmgPercentForAlien);
		bw.Write(this.dmgPercentForAllWeapon);
		bw.Write(this.dmgPercentBonusForEachLaser);
		bw.Write(this.dmgPercentBonusForEachPlasma);
		bw.Write(this.dmgPercentBonusForEachIon);
		bw.Write(this.dmgFlatBonusForEachLaser);
		bw.Write(this.dmgFlatBonusForEachPlasma);
		bw.Write(this.dmgFlatBonusForEachIon);
		bw.Write(this.laserCooldown);
		bw.Write(this.plasmaCooldown);
		bw.Write(this.ionCooldown);
		bw.Write(this.fasterMining);
		bw.Write(this.shieldRepairPerSec);
		bw.Write(this.experienceGain);
		bw.Write(this.damageReductionItems);
		bw.Write(this.resilience);
		bw.Write(this.sellBonus);
		bw.Write(this.fusionPriceOff);
		bw.Write(this.ammoCreationBonus);
		bw.Write(this.epIncomeBonus);
		bw.Write(this.playerLevel);
		bw.Write(this.targeting);
		bw.Write(this.targetingForLaser);
		bw.Write(this.targetingForPlasma);
		bw.Write(this.targetingForIon);
		bw.Write(this.avoidanceMax);
		bw.Write(this.currentAvoidance);
		bw.Write(this.speedBoostConsumption);
		bw.Write(this.maxBoostedSpeed);
		bw.Write(this.haveLaserDamageFlat);
		bw.Write(this.havePlasmaDamageFlat);
		bw.Write(this.haveIonDamageFlat);
		bw.Write(this.haveLaserDamagePercentage);
		bw.Write(this.havePlasmaDamagePercentage);
		bw.Write(this.haveIonDamagePercentage);
		bw.Write(this.haveTotalDamagePercentage);
		bw.Write(this.haveCorpusFlat);
		bw.Write(this.haveCorpusPercentage);
		bw.Write(this.haveShieldFlat);
		bw.Write(this.haveShieldPercentage);
		bw.Write(this.haveEndurancePercentage);
		bw.Write(this.haveShieldPowerFlat);
		bw.Write(this.haveShieldPowerPercentage);
		bw.Write(this.haveTargetingFlat);
		bw.Write(this.haveTargetingPercentage);
		bw.Write(this.haveAvoidanceFlat);
		bw.Write(this.haveAvoidancePercentage);
	}

	public void SetShootTimesAfterTransport()
	{
		long ticks = StaticData.now.Ticks;
		WeaponSlot[] weaponSlotArray = this.weaponSlots;
		for (int i = 0; i < (int)weaponSlotArray.Length; i++)
		{
			WeaponSlot weaponSlot = weaponSlotArray[i];
			weaponSlot.padre = this;
			if (weaponSlot.timeToNextShot != 0)
			{
				weaponSlot.lastShotTime = ticks + (long)(weaponSlot.timeToNextShot * 10000) - weaponSlot.realReloadTime;
			}
			else
			{
				weaponSlot.lastShotTime = (long)0;
			}
		}
	}
}