using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

public class PlayerShipNet : ITransferable
{
	public static float SPEED_TRANSFORM;

	public int playerId;

	public byte shipTypeId;

	public int ShipID;

	public string ShipTitle = "";

	public string ShipName = "";

	public int Corpus;

	public int Shield;

	public float MaxCargo;

	public float CargoBonus;

	public int CorpusBonus;

	public int ShieldBonus;

	public short SpeedBonus;

	public int CorpusHP;

	public int ShieldHP;

	public short Speed;

	public short Targeting;

	public short TargetingBonus;

	public short Avoidance;

	public short AvoidanceBonus;

	public bool selected = false;

	public ShipAttachmentNet[] shipAttachments;

	public int guildEndurance;

	public int guildAccuracy;

	public int guildExpirience;

	public int guildResilience;

	public float shieldPower;

	public float dmgPercentAlien;

	public float dmgPercentWeapon;

	public int dmgFlatBonusLaser;

	public int dmgFlatBonusPlasma;

	public int dmgFlatBonusIon;

	public float dmgPercentBonusLaser;

	public float dmgPercentBonusPlasma;

	public float dmgPercentBonusIon;

	public int CountExtras
	{
		get
		{
			int num = (
				from sa in (IEnumerable<ShipAttachmentNet>)this.shipAttachments
				where sa.category == 5
				select sa).Count<ShipAttachmentNet>();
			return num;
		}
	}

	public int CountGenerators
	{
		get
		{
			int num = (
				from sa in (IEnumerable<ShipAttachmentNet>)this.shipAttachments
				where (sa.category == 3 || sa.category == 4 ? true : sa.category == 6)
				select sa).Count<ShipAttachmentNet>();
			return num;
		}
	}

	public int CountWeapons
	{
		get
		{
			int num = (
				from sa in (IEnumerable<ShipAttachmentNet>)this.shipAttachments
				where sa.category == 2
				select sa).Count<ShipAttachmentNet>();
			return num;
		}
	}

	public float Velocity
	{
		get
		{
			return (this.Speed > 0 ? (float)this.Speed / Mathf.Pow((float)this.Speed, 0.6f) * 40f : 0f);
		}
	}

	static PlayerShipNet()
	{
		PlayerShipNet.SPEED_TRANSFORM = 43.3333f;
	}

	public PlayerShipNet()
	{
	}

	public void ApplyAttachmentBonus(ushort itemType)
	{
		GeneratorNet item;
		if (PlayerItems.item2categoryMapping[itemType] == 4)
		{
			item = (GeneratorNet)StaticData.allTypes[itemType];
			this.ShieldHP += item.bonusValue;
		}
		if (PlayerItems.item2categoryMapping[itemType] == 6)
		{
			item = (GeneratorNet)StaticData.allTypes[itemType];
			this.CorpusHP += item.bonusValue;
		}
	}

	public void ApplyBonuses(PlayerBelongings pb)
	{
		ShipsTypeNet shipsTypeNet = (
			from st in StaticData.shipTypes
			where st.id == this.shipTypeId
			select st).First<ShipsTypeNet>();
		this.CalculateAdditionalBonuses(pb);
		this.CorpusBonus = this.CalculateCorpusBonus(shipsTypeNet, pb);
		this.Corpus = shipsTypeNet.corpus + this.CorpusBonus;
		this.ShieldBonus = this.CalculateShieldBonus(shipsTypeNet, pb);
		this.Shield = shipsTypeNet.shield + this.ShieldBonus;
		this.SpeedBonus = this.CalculateSpeedBonus(shipsTypeNet, pb.playerItems);
		this.Speed = (short)(shipsTypeNet.speed + this.SpeedBonus);
		this.CargoBonus = (float)this.CalculateCargoBonus(shipsTypeNet, pb);
		this.MaxCargo = (float)((short)((float)shipsTypeNet.cargo + this.CargoBonus));
		this.TargetingBonus = this.CalculateTargetingBonus(shipsTypeNet, pb);
		this.Targeting = (short)(shipsTypeNet.targeting + this.TargetingBonus);
		this.AvoidanceBonus = this.CalculateAvoidanceBonus(shipsTypeNet, pb);
		this.Avoidance = (short)(shipsTypeNet.avoidance + this.AvoidanceBonus);
	}

	public void CalculateAdditionalBonuses(PlayerBelongings pb)
	{
		int item;
		int num;
		int num1;
		float bonusFive = 0f;
		float single = 0f;
		float powerUpEffectValue = 0f;
		int bonusFour = 0;
		int bonusFive1 = 0;
		int powerUpEffectValue1 = 0;
		float single1 = 0f;
		float powerUpEffectValue2 = 0f;
		float single2 = 0f;
		SlotItem[] array = (
			from t in pb.playerItems.slotItems
			where t.ShipId == this.ShipID
			select t).ToArray<SlotItem>();
		SlotItem[] slotItemArray = array;
		for (int i = 0; i < (int)slotItemArray.Length; i++)
		{
			SlotItem slotItem = slotItemArray[i];
			if ((PlayerItems.IsShield(slotItem.ItemType) || PlayerItems.IsCorpus(slotItem.ItemType) || PlayerItems.IsEngine(slotItem.ItemType) ? true : PlayerItems.IsExtraOther(slotItem.ItemType)))
			{
				bonusFive += (float)slotItem.BonusFive;
			}
			if (PlayerItems.IsForShieldRegen(slotItem.ItemType))
			{
				bonusFive += (float)((ExtrasNet)StaticData.allTypes[slotItem.ItemType]).efValue;
			}
			if (PlayerItems.IsExtraCooldown(slotItem.ItemType))
			{
				bonusFour += slotItem.BonusFour;
				bonusFive1 += slotItem.BonusFive;
			}
			if (PlayerItems.IsExtraDamage(slotItem.ItemType))
			{
				bonusFour += slotItem.BonusThree;
				bonusFive1 += slotItem.BonusFour;
				powerUpEffectValue1 += slotItem.BonusFive;
			}
			if ((slotItem.ItemType == PlayerItems.TypeExtraLaserWeaponsModule || slotItem.ItemType == PlayerItems.TypeExtraLaserWeaponsModule1 || slotItem.ItemType == PlayerItems.TypeExtraLaserWeaponsModule2 || slotItem.ItemType == PlayerItems.TypeExtraLaserWeaponsModule3 || slotItem.ItemType == PlayerItems.TypeExtraLaserWeaponsModule4 ? true : slotItem.ItemType == PlayerItems.TypeExtraLaserWeaponsModule5))
			{
				item = ((ExtrasNet)StaticData.allTypes[slotItem.ItemType]).efValue;
				single1 += (float)item;
			}
			if ((slotItem.ItemType == PlayerItems.TypeExtraPlasmaWeaponsModule || slotItem.ItemType == PlayerItems.TypeExtraPlasmaWeaponsModule1 || slotItem.ItemType == PlayerItems.TypeExtraPlasmaWeaponsModule2 || slotItem.ItemType == PlayerItems.TypeExtraPlasmaWeaponsModule3 || slotItem.ItemType == PlayerItems.TypeExtraPlasmaWeaponsModule4 ? true : slotItem.ItemType == PlayerItems.TypeExtraPlasmaWeaponsModule5))
			{
				item = ((ExtrasNet)StaticData.allTypes[slotItem.ItemType]).efValue;
				powerUpEffectValue2 += (float)item;
			}
			if ((slotItem.ItemType == PlayerItems.TypeExtraIonWeaponsModule || slotItem.ItemType == PlayerItems.TypeExtraIonWeaponsModule1 || slotItem.ItemType == PlayerItems.TypeExtraIonWeaponsModule2 || slotItem.ItemType == PlayerItems.TypeExtraIonWeaponsModule3 || slotItem.ItemType == PlayerItems.TypeExtraIonWeaponsModule4 ? true : slotItem.ItemType == PlayerItems.TypeExtraIonWeaponsModule5))
			{
				item = ((ExtrasNet)StaticData.allTypes[slotItem.ItemType]).efValue;
				single2 += (float)item;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraUltraWeaponsModule)
			{
				item = ((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraUltraWeaponsModule]).efValue;
				single1 += (float)item;
				powerUpEffectValue2 += (float)item;
				single2 += (float)item;
			}
		}
		if (pb.HaveLaserDamageFlat)
		{
			bonusFour += pb.GetPowerUpEffectValue(PlayerItems.TypePowerUpForLaserDamageFlat);
		}
		if (pb.HaveLaserDamagePercentage)
		{
			single1 += (float)pb.GetPowerUpEffectValue(PlayerItems.TypePowerUpForLaserDamagePercentage);
		}
		if (pb.HavePlasmaDamageFlat)
		{
			bonusFive1 += pb.GetPowerUpEffectValue(PlayerItems.TypePowerUpForPlasmaDamageFlat);
		}
		if (pb.HavePlasmaDamagePercentage)
		{
			powerUpEffectValue2 += (float)pb.GetPowerUpEffectValue(PlayerItems.TypePowerUpForPlasmaDamagePercentage);
		}
		if (pb.HaveIonDamageFlat)
		{
			powerUpEffectValue1 += pb.GetPowerUpEffectValue(PlayerItems.TypePowerUpForIonDamageFlat);
		}
		if (pb.HaveIonDamagePercentage)
		{
			single2 += (float)pb.GetPowerUpEffectValue(PlayerItems.TypePowerUpForIonDamagePercentage);
		}
		if (pb.HaveTotalDamagePercentage)
		{
			powerUpEffectValue += (float)pb.GetPowerUpEffectValue(PlayerItems.TypePowerUpForTotalDamagePercentage);
		}
		pb.playerItems.GetSkillEffect(PlayerItems.TypeTalentsEmpoweredShield, out num, out num1);
		bonusFive += (float)(num + num1);
		pb.playerItems.GetSkillEffect(PlayerItems.TypeTalentsAlienSpecialist, out num, out num1);
		single = (float)(num + num1);
		pb.playerItems.GetSkillEffect(PlayerItems.TypeTalentsWeaponMastery, out num, out num1);
		powerUpEffectValue += (float)(num + num1);
		this.dmgPercentAlien = single;
		this.dmgPercentWeapon = powerUpEffectValue;
		this.dmgFlatBonusLaser = bonusFour;
		this.dmgFlatBonusPlasma = bonusFive1;
		this.dmgFlatBonusIon = powerUpEffectValue1;
		this.dmgPercentBonusLaser = single1;
		this.dmgPercentBonusPlasma = powerUpEffectValue2;
		this.dmgPercentBonusIon = single2;
		this.shieldPower = bonusFive;
	}

	private short CalculateAvoidanceBonus(ShipsTypeNet shipType, PlayerBelongings pb)
	{
		int num;
		int num1;
		double bonusFour = 0;
		float powerUpEffectValue = 0f;
		SlotItem[] array = (
			from t in pb.playerItems.slotItems
			where t.ShipId == this.ShipID
			select t).ToArray<SlotItem>();
		SlotItem[] slotItemArray = array;
		for (int i = 0; i < (int)slotItemArray.Length; i++)
		{
			SlotItem slotItem = slotItemArray[i];
			if (!(PlayerItems.IsShield(slotItem.ItemType) ? false : !PlayerItems.IsCorpus(slotItem.ItemType)))
			{
				bonusFour += (double)slotItem.BonusFour;
			}
			else if (!(PlayerItems.IsExtraCargoMining(slotItem.ItemType) || PlayerItems.IsExtraDamage(slotItem.ItemType) ? false : !PlayerItems.IsEngine(slotItem.ItemType)))
			{
				bonusFour += (double)slotItem.BonusTwo;
			}
			else if (PlayerItems.IsExtraOther(slotItem.ItemType))
			{
				bonusFour += (double)slotItem.BonusThree;
			}
		}
		bonusFour += (double)((float)(shipType.avoidance * this.guildAccuracy) / 100f);
		pb.playerItems.GetSkillEffect(PlayerItems.TypeTalentsNanoTechnology, out num, out num1);
		bonusFour += (double)(num + num1);
		pb.playerItems.GetSkillEffect(PlayerItems.TypeTalentsDefiance, out num, out num1);
		bonusFour += (double)((float)(shipType.avoidance * (num + num1)) / 100f);
		if (pb.HaveAvoidanceFlat)
		{
			bonusFour += (double)pb.GetPowerUpEffectValue(PlayerItems.TypePowerUpForAvoidanceFlat);
		}
		if (pb.HaveAvoidancePercentage)
		{
			powerUpEffectValue += (float)pb.GetPowerUpEffectValue(PlayerItems.TypePowerUpForAvoidancePercentage);
		}
		bonusFour = bonusFour + ((double)shipType.avoidance + bonusFour) * (double)powerUpEffectValue / 100;
		return (short)bonusFour;
	}

	private short CalculateCargoBonus(ShipsTypeNet shipType, PlayerBelongings pb)
	{
		int num;
		int num1;
		double bonusThree = 0;
		float item = 0f;
		SlotItem[] array = (
			from t in pb.playerItems.slotItems
			where t.ShipId == this.ShipID
			select t).ToArray<SlotItem>();
		SlotItem[] slotItemArray = array;
		for (int i = 0; i < (int)slotItemArray.Length; i++)
		{
			SlotItem slotItem = slotItemArray[i];
			if (PlayerItems.IsForExtraCargoSpace(slotItem.ItemType))
			{
				item += (float)((ExtrasNet)StaticData.allTypes[slotItem.ItemType]).efValue;
			}
			if ((PlayerItems.IsEngine(slotItem.ItemType) ? true : PlayerItems.IsExtraCargoMining(slotItem.ItemType)))
			{
				bonusThree += (double)slotItem.BonusThree;
				item += (float)slotItem.BonusFour;
			}
		}
		if (pb.FactionBoostMining)
		{
			item += 25f;
		}
		pb.playerItems.GetSkillEffect(PlayerItems.TypeTalentsArchiver, out num, out num1);
		bonusThree += (double)((float)shipType.cargo * ((float)(num + num1) + item) / 100f);
		if (pb.HaveCargoBooster)
		{
			bonusThree += (double)shipType.cargo;
		}
		return (short)bonusThree;
	}

	private int CalculateCorpusBonus(ShipsTypeNet shipType, PlayerBelongings pb)
	{
		int num;
		int num1;
		double item = 0;
		SlotItem[] array = (
			from t in pb.playerItems.slotItems
			where t.ShipId == this.ShipID
			select t).ToArray<SlotItem>();
		float bonusTwo = 0f;
		SlotItem[] slotItemArray = array;
		for (int i = 0; i < (int)slotItemArray.Length; i++)
		{
			SlotItem slotItem = slotItemArray[i];
			if (PlayerItems.IsCorpus(slotItem.ItemType))
			{
				item += (double)((GeneratorNet)StaticData.allTypes[slotItem.ItemType]).bonusValue;
				item += (double)slotItem.BonusOne;
				bonusTwo += (float)slotItem.BonusTwo;
			}
		}
		bonusTwo += (float)this.guildEndurance;
		if (pb.HaveCorpusFlat)
		{
			item += (double)pb.GetPowerUpEffectValue(PlayerItems.TypePowerUpForCorpusFlat);
		}
		if (pb.HaveCorpusPercentage)
		{
			bonusTwo += (float)pb.GetPowerUpEffectValue(PlayerItems.TypePowerUpForCorpusPercentage);
		}
		if (pb.HaveEndurancePercentage)
		{
			bonusTwo += (float)pb.GetPowerUpEffectValue(PlayerItems.TypePowerUpForEndurancePercentage);
		}
		item += (double)((float)shipType.corpus * bonusTwo / 100f);
		pb.playerItems.GetSkillEffect(PlayerItems.TypeTalentsAdvancedCorpus, out num, out num1);
		item = item + (item + (double)shipType.corpus) * (double)(num + num1) / 100;
		return (int)item;
	}

	private int CalculateShieldBonus(ShipsTypeNet shipType, PlayerBelongings pb)
	{
		int num;
		int num1;
		double item = 0;
		SlotItem[] array = (
			from t in pb.playerItems.slotItems
			where t.ShipId == this.ShipID
			select t).ToArray<SlotItem>();
		float bonusTwo = 0f;
		SlotItem[] slotItemArray = array;
		for (int i = 0; i < (int)slotItemArray.Length; i++)
		{
			SlotItem slotItem = slotItemArray[i];
			if (PlayerItems.IsShield(slotItem.ItemType))
			{
				item += (double)((GeneratorNet)StaticData.allTypes[slotItem.ItemType]).bonusValue;
				item += (double)slotItem.BonusOne;
				bonusTwo += (float)slotItem.BonusTwo;
			}
		}
		pb.playerItems.GetSkillEffect(PlayerItems.TypeTalentsImprovedShield, out num, out num1);
		bonusTwo += (float)(num + num1);
		bonusTwo += (float)this.guildEndurance;
		if (pb.HaveShieldFlat)
		{
			item += (double)pb.GetPowerUpEffectValue(PlayerItems.TypePowerUpForShieldFlat);
		}
		if (pb.HaveShieldPercentage)
		{
			bonusTwo += (float)pb.GetPowerUpEffectValue(PlayerItems.TypePowerUpForShieldPercentage);
		}
		if (pb.HaveEndurancePercentage)
		{
			bonusTwo += (float)pb.GetPowerUpEffectValue(PlayerItems.TypePowerUpForEndurancePercentage);
		}
		item += (double)((float)shipType.shield * bonusTwo / 100f);
		pb.playerItems.GetSkillEffect(PlayerItems.TypeTalentsAdvancedShield, out num, out num1);
		item = item + (item + (double)shipType.shield) * (double)(num + num1) / 100;
		return (int)item;
	}

	private short CalculateSpeedBonus(ShipsTypeNet shipType, PlayerItems items)
	{
		int num;
		int num1;
		double item = 0;
		SlotItem[] array = (
			from t in items.slotItems
			where t.ShipId == this.ShipID
			select t).ToArray<SlotItem>();
		SlotItem[] slotItemArray = array;
		for (int i = 0; i < (int)slotItemArray.Length; i++)
		{
			SlotItem slotItem = slotItemArray[i];
			if (PlayerItems.IsEngine(slotItem.ItemType))
			{
				item += (double)((GeneratorNet)StaticData.allTypes[slotItem.ItemType]).bonusValue;
				item += (double)slotItem.BonusOne;
			}
			if (PlayerItems.IsExtraOther(slotItem.ItemType))
			{
				item += (double)slotItem.BonusOne;
			}
		}
		items.GetSkillEffect(PlayerItems.TypeTalentsVelocity, out num, out num1);
		item += (double)(num + num1);
		items.GetSkillEffect(PlayerItems.TypeTalentsEngineBooster, out num, out num1);
		item += (double)(shipType.speed * (num + num1) / 100);
		return (short)item;
	}

	private short CalculateTargetingBonus(ShipsTypeNet shipType, PlayerBelongings pb)
	{
		int num;
		int num1;
		double bonusThree = 0;
		float powerUpEffectValue = 0f;
		SlotItem[] array = (
			from t in pb.playerItems.slotItems
			where t.ShipId == this.ShipID
			select t).ToArray<SlotItem>();
		SlotItem[] slotItemArray = array;
		for (int i = 0; i < (int)slotItemArray.Length; i++)
		{
			SlotItem slotItem = slotItemArray[i];
			if (!(PlayerItems.IsShield(slotItem.ItemType) ? false : !PlayerItems.IsCorpus(slotItem.ItemType)))
			{
				bonusThree += (double)slotItem.BonusThree;
			}
			else if (!(PlayerItems.IsExtraCargoMining(slotItem.ItemType) ? false : !PlayerItems.IsExtraDamage(slotItem.ItemType)))
			{
				bonusThree += (double)slotItem.BonusOne;
			}
			else if (PlayerItems.IsExtraOther(slotItem.ItemType))
			{
				bonusThree += (double)slotItem.BonusTwo;
			}
		}
		bonusThree += (double)((float)(shipType.targeting * this.guildAccuracy) / 100f);
		pb.playerItems.GetSkillEffect(PlayerItems.TypeTalentsSteadyAim, out num, out num1);
		bonusThree += (double)(num + num1);
		pb.playerItems.GetSkillEffect(PlayerItems.TypeTalentsFindWeakSpot, out num, out num1);
		bonusThree += (double)(num + num1);
		if (pb.HaveTargetingFlat)
		{
			bonusThree += (double)pb.GetPowerUpEffectValue(PlayerItems.TypePowerUpForTargetingFlat);
		}
		if (pb.HaveTargetingPercentage)
		{
			powerUpEffectValue += (float)pb.GetPowerUpEffectValue(PlayerItems.TypePowerUpForTargetingPercentage);
		}
		bonusThree = bonusThree + ((double)shipType.targeting + bonusThree) * (double)powerUpEffectValue / 100;
		return (short)bonusThree;
	}

	public void CreateAttachment(ushort itemType, PlayerItems items, byte slotNumber, bool isActive)
	{
		ShipAttachmentNet shipAttachmentNet = new ShipAttachmentNet()
		{
			category = PlayerItems.item2categoryMapping[itemType],
			id = -1,
			playerItemTypeId = itemType,
			pShipId = this.ShipID,
			slotNumber = slotNumber,
			isActive = isActive
		};
		ShipAttachmentNet shipAttachmentNet1 = shipAttachmentNet;
		List<ShipAttachmentNet> shipAttachmentNets = new List<ShipAttachmentNet>(this.shipAttachments)
		{
			shipAttachmentNet1
		};
		ShipAttachmentNet shipAttachmentNet2 = (
			from t in this.shipAttachments
			where (t.slotNumber != slotNumber ? false : t.category == 1)
			select t).FirstOrDefault<ShipAttachmentNet>();
		if ((shipAttachmentNet1.category != 2 ? false : shipAttachmentNet2 == null))
		{
			ushort num = 0;
			if (items.GetAmountAt(10) > (long)0)
			{
				num = 10;
			}
			else if (items.GetAmountAt(9) > (long)0)
			{
				num = 9;
			}
			else if (items.GetAmountAt(8) > (long)0)
			{
				num = 8;
			}
			else if (items.GetAmountAt(7) > (long)0)
			{
				num = 7;
			}
			else if (items.GetAmountAt(6) > (long)0)
			{
				num = 6;
			}
			else if (items.GetAmountAt(5) > (long)0)
			{
				num = 5;
			}
			else if (items.GetAmountAt(4) > (long)0)
			{
				num = 4;
			}
			if (num != 0)
			{
				ShipAttachmentNet shipAttachmentNet3 = new ShipAttachmentNet()
				{
					category = 1,
					playerItemTypeId = num,
					pShipId = this.ShipID,
					slotNumber = slotNumber,
					isActive = true
				};
				shipAttachmentNet2 = shipAttachmentNet3;
				shipAttachmentNets.Add(shipAttachmentNet2);
			}
		}
		this.shipAttachments = shipAttachmentNets.ToArray();
		this.ApplyAttachmentBonus(itemType);
	}

	public void Deserialize(BinaryReader br)
	{
		this.ShipID = br.ReadInt32();
		this.ShipTitle = br.ReadString();
		this.ShipName = br.ReadString();
		this.Corpus = br.ReadInt32();
		this.Shield = br.ReadInt32();
		this.MaxCargo = br.ReadSingle();
		this.CargoBonus = br.ReadSingle();
		this.CorpusBonus = br.ReadInt32();
		this.ShieldBonus = br.ReadInt32();
		this.SpeedBonus = br.ReadInt16();
		this.CorpusHP = br.ReadInt32();
		this.ShieldHP = br.ReadInt32();
		this.Speed = br.ReadInt16();
		this.selected = br.ReadBoolean();
		this.shipTypeId = br.ReadByte();
		this.Targeting = br.ReadInt16();
		this.TargetingBonus = br.ReadInt16();
		this.Avoidance = br.ReadInt16();
		this.AvoidanceBonus = br.ReadInt16();
		this.shieldPower = br.ReadSingle();
		this.dmgPercentAlien = br.ReadSingle();
		this.dmgPercentWeapon = br.ReadSingle();
		this.dmgFlatBonusLaser = br.ReadInt32();
		this.dmgFlatBonusPlasma = br.ReadInt32();
		this.dmgFlatBonusIon = br.ReadInt32();
		this.dmgPercentBonusLaser = br.ReadSingle();
		this.dmgPercentBonusPlasma = br.ReadSingle();
		this.dmgPercentBonusIon = br.ReadSingle();
	}

	public int GetAttachedWeaponDamage(SlotItemWeapon weapon)
	{
		float damageTotal = 0f;
		damageTotal = (float)weapon.DamageTotal;
		if (!(weapon.ItemType == PlayerItems.TypeWeaponLaserTire1 || weapon.ItemType == PlayerItems.TypeWeaponLaserTire2 || weapon.ItemType == PlayerItems.TypeWeaponLaserTire3 || weapon.ItemType == PlayerItems.TypeWeaponLaserTire4 ? false : weapon.ItemType != PlayerItems.TypeWeaponLaserTire5))
		{
			damageTotal = damageTotal * (1f + this.dmgPercentBonusLaser / 100f) * (1f + this.dmgPercentWeapon / 100f) + (float)this.dmgFlatBonusLaser;
		}
		else if (!(weapon.ItemType == PlayerItems.TypeWeaponPlasmaTire1 || weapon.ItemType == PlayerItems.TypeWeaponPlasmaTire2 || weapon.ItemType == PlayerItems.TypeWeaponPlasmaTire3 || weapon.ItemType == PlayerItems.TypeWeaponPlasmaTire4 ? false : weapon.ItemType != PlayerItems.TypeWeaponPlasmaTire5))
		{
			damageTotal = damageTotal * (1f + this.dmgPercentBonusPlasma / 100f) * (1f + this.dmgPercentWeapon / 100f) + (float)this.dmgFlatBonusPlasma;
		}
		else if ((weapon.ItemType == PlayerItems.TypeWeaponIonTire1 || weapon.ItemType == PlayerItems.TypeWeaponIonTire2 || weapon.ItemType == PlayerItems.TypeWeaponIonTire3 || weapon.ItemType == PlayerItems.TypeWeaponIonTire4 ? true : weapon.ItemType == PlayerItems.TypeWeaponIonTire5))
		{
			damageTotal = damageTotal * (1f + this.dmgPercentBonusIon / 100f) * (1f + this.dmgPercentWeapon / 100f) + (float)this.dmgFlatBonusIon;
		}
		return (int)damageTotal;
	}

	public void RemoveAttachment(byte category, byte slot)
	{
		ShipAttachmentNet shipAttachmentNet = (
			from a in this.shipAttachments
			where (a.category != category ? false : a.slotNumber == slot)
			select a).First<ShipAttachmentNet>();
		List<ShipAttachmentNet> list = this.shipAttachments.ToList<ShipAttachmentNet>();
		list.Remove(shipAttachmentNet);
		this.shipAttachments = list.ToArray();
		this.RemoveAttachmentBonus(shipAttachmentNet.playerItemTypeId);
	}

	public void RemoveAttachmentBonus(ushort itemType)
	{
		GeneratorNet item;
		if (PlayerItems.item2categoryMapping[itemType] == 4)
		{
			item = (GeneratorNet)StaticData.allTypes[itemType];
			this.ShieldHP -= item.bonusValue;
			if (this.ShieldHP < 0)
			{
				this.ShieldHP = 0;
			}
		}
		if (PlayerItems.item2categoryMapping[itemType] == 6)
		{
			item = (GeneratorNet)StaticData.allTypes[itemType];
			this.CorpusHP -= item.bonusValue;
			if (this.CorpusHP <= 0)
			{
				this.CorpusHP = 1;
			}
		}
	}

	public void ReplaceAttachment(byte category, byte slot, ushort newAttachmentItemType, PlayerItems items, bool isActive)
	{
		ShipAttachmentNet shipAttachmentNet = (
			from a in this.shipAttachments
			where (a.category != category ? false : a.slotNumber == slot)
			select a).FirstOrDefault<ShipAttachmentNet>();
		if (shipAttachmentNet != null)
		{
			List<ShipAttachmentNet> list = this.shipAttachments.ToList<ShipAttachmentNet>();
			list.Remove(shipAttachmentNet);
			this.shipAttachments = list.ToArray();
		}
		this.CreateAttachment(newAttachmentItemType, items, slot, isActive);
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.ShipID);
		bw.Write(this.ShipTitle);
		bw.Write(this.ShipName);
		bw.Write(this.Corpus);
		bw.Write(this.Shield);
		bw.Write(this.MaxCargo);
		bw.Write(this.CargoBonus);
		bw.Write(this.CorpusBonus);
		bw.Write(this.ShieldBonus);
		bw.Write(this.SpeedBonus);
		bw.Write(this.CorpusHP);
		bw.Write(this.ShieldHP);
		bw.Write(this.Speed);
		bw.Write(this.selected);
		bw.Write(this.shipTypeId);
		bw.Write(this.Targeting);
		bw.Write(this.TargetingBonus);
		bw.Write(this.Avoidance);
		bw.Write(this.AvoidanceBonus);
		bw.Write(this.shieldPower);
		bw.Write(this.dmgPercentAlien);
		bw.Write(this.dmgPercentWeapon);
		bw.Write(this.dmgFlatBonusLaser);
		bw.Write(this.dmgFlatBonusPlasma);
		bw.Write(this.dmgFlatBonusIon);
		bw.Write(this.dmgPercentBonusLaser);
		bw.Write(this.dmgPercentBonusPlasma);
		bw.Write(this.dmgPercentBonusIon);
	}

	public void SwitchAttachment(short category, short slot, bool isON)
	{
		ShipAttachmentNet shipAttachmentNet = (
			from a in this.shipAttachments
			where (a.category != category ? false : a.slotNumber == slot)
			select a).First<ShipAttachmentNet>();
		List<ShipAttachmentNet> list = this.shipAttachments.ToList<ShipAttachmentNet>();
		list.Remove(shipAttachmentNet);
		shipAttachmentNet.isActive = isON;
		list.Add(shipAttachmentNet);
		this.shipAttachments = list.ToArray();
	}
}