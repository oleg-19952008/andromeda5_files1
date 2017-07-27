using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

public class PlayerBelongings : ITransferable
{
	public string playerName = "";

	public short guildInvitesCount = -1;

	public short playerTalentClass;

	public int lastPurchaseId;

	public int selectedShipId;

	public PlayerShipNet[] playerShips;

	public PlayerItems playerItems;

	public List<int> unlockedPortals;

	public long nextFreeTransformerUsage;

	public byte transformerState;

	public byte dailyMissionsDone;

	public bool isDailyMissionsRewardCollected;

	public PlayerObjectives playerObjectives;

	public ActiveSkillBarConfig skillConfig;

	public RecrutedPlayers referals = new RecrutedPlayers();

	public SortedList<int, PlayerQuest> playerQuests;

	public byte playerInventorySlots;

	public byte playerVaultSlots;

	public short playerLevel;

	public byte cuLevel;

	public bool isAutoFireOn;

	public bool isAutoMiningOn;

	public bool isShowMoreDetailsOn;

	public short zoomLevel;

	public byte playerAccessLevel;

	public byte receivedDailyRewards;

	public byte socialOptionsStatus = 0;

	public SortedList<int, PlayerPendingAward> playerAwards;

	public List<KeyboardShortcutPair> playerKeyboardShortcuts;

	public bool factionGalaxyXpGain;

	public bool factionGalaxySellBonus;

	public byte factionWarBattleBoost;

	public byte factionWarUtilityBoost;

	public DateTime factionWarDayEnd;

	public DayOfWeek factionWarDay;

	public bool isWarInProgress;

	public byte factionWarRerollBonus = 0;

	public byte factionWarXpBonus = 0;

	public byte councilRank = 0;

	public byte myBattleBoostVote = 0;

	public byte myUtilityBoostVote = 0;

	public ushort councilSkillSelected = 0;

	public bool day1Participation = false;

	public bool day2Participation = false;

	public bool day3Participation = false;

	public bool day4Participation = false;

	public bool day5Participation = false;

	public bool day6Participation = false;

	public bool rewardForDayProgressCollected1 = false;

	public bool rewardForDayProgressCollected2 = false;

	public bool rewardForDayProgressCollected3 = false;

	public bool weeklyRewardCollected = false;

	public byte lastWeekPendingReward = 0;

	public byte warCommendationsBought = 0;

	public int factionWarDayScore;

	public DateTime penaltyTime = DateTime.MinValue;

	public DateTime nextPvPRoundTime = DateTime.MinValue;

	public short penaltyDuration = 0;

	public bool firstWinBonusRecived = false;

	public byte pvpGamePoolCapacity;

	public DateTime oldestPvPGameStartTime;

	public DateTime signingPvPGameTime = DateTime.MinValue;

	public DateTime boostAutoMinerExpireTime = DateTime.MinValue;

	public DateTime boostDamageExpireTime = DateTime.MinValue;

	public DateTime boostCargoExpireTime = DateTime.MinValue;

	public DateTime boostExperienceExpireTime = DateTime.MinValue;

	public DateTime nextFreeGiftTime = DateTime.MinValue;

	public DateTime laserDamageFlatExpireTime = DateTime.MinValue;

	public DateTime plasmaDamageFlatExpireTime = DateTime.MinValue;

	public DateTime ionDamageFlatExpireTime = DateTime.MinValue;

	public DateTime laserDamagePercentageExpireTime = DateTime.MinValue;

	public DateTime plasmaDamagePercentageExpireTime = DateTime.MinValue;

	public DateTime ionDamagePercentageExpireTime = DateTime.MinValue;

	public DateTime totalDamagePercentageExpireTime = DateTime.MinValue;

	public DateTime corpusFlatExpireTime = DateTime.MinValue;

	public DateTime corpusPercentageExpireTime = DateTime.MinValue;

	public DateTime shieldFlatExpireTime = DateTime.MinValue;

	public DateTime shieldPercentageExpireTime = DateTime.MinValue;

	public DateTime endurancePercentageExpireTime = DateTime.MinValue;

	public DateTime shieldPowerFlatExpireTime = DateTime.MinValue;

	public DateTime shieldPowerPercentageExpireTime = DateTime.MinValue;

	public DateTime targetingFlatExpireTime = DateTime.MinValue;

	public DateTime targetingPercentageExpireTime = DateTime.MinValue;

	public DateTime avoidanceFlatExpireTime = DateTime.MinValue;

	public DateTime avoidancePercentageExpireTime = DateTime.MinValue;

	public List<short> visitedNPCs;

	public bool haveExtendetCorpusOne;

	public bool haveExtendetShielOne;

	public bool haveExtendetEngineOne;

	public bool haveExtendetAnyOne;

	public bool haveExtendetExtraOne;

	public bool haveExtendetExtraTwo;

	public bool FactionBoostFusion
	{
		get
		{
			return this.factionWarUtilityBoost == 202;
		}
	}

	public bool FactionBoostIncome
	{
		get
		{
			return this.factionWarUtilityBoost == 203;
		}
	}

	public bool FactionBoostMining
	{
		get
		{
			return this.factionWarUtilityBoost == 201;
		}
	}

	public bool FactionBoostPvEDamage
	{
		get
		{
			return this.factionWarBattleBoost == 103;
		}
	}

	public bool FactionBoostPvPDamage
	{
		get
		{
			return this.factionWarBattleBoost == 101;
		}
	}

	public bool FactionBoostResilience
	{
		get
		{
			return this.factionWarBattleBoost == 102;
		}
	}

	public int FactionWarWeeklyChalangeParticipation
	{
		get
		{
			int num = 0;
			if (this.day1Participation)
			{
				num++;
			}
			if (this.day2Participation)
			{
				num++;
			}
			if (this.day3Participation)
			{
				num++;
			}
			if (this.day4Participation)
			{
				num++;
			}
			if (this.day5Participation)
			{
				num++;
			}
			if (this.day6Participation)
			{
				num++;
			}
			return num;
		}
	}

	public short FirstFreeSkillSlot
	{
		get
		{
			short num;
			lock (this.skillConfig.skillSlots)
			{
				short num1 = 0;
				while (num1 < 21)
				{
					if (this.skillConfig.skillSlots.ContainsKey(num1))
					{
						num1 = (short)(num1 + 1);
					}
					else
					{
						num = num1;
						return num;
					}
				}
				throw new Exception("Out of free skill slots!");
			}
			return num;
		}
	}

	public bool HaveAutoMinerBooster
	{
		get
		{
			return this.boostAutoMinerExpireTime > StaticData.now;
		}
	}

	public bool HaveAvoidanceFlat
	{
		get
		{
			return this.avoidanceFlatExpireTime > StaticData.now;
		}
	}

	public bool HaveAvoidancePercentage
	{
		get
		{
			return this.avoidancePercentageExpireTime > StaticData.now;
		}
	}

	public bool HaveCargoBooster
	{
		get
		{
			return this.boostCargoExpireTime > StaticData.now;
		}
	}

	public bool HaveCorpusFlat
	{
		get
		{
			return this.corpusFlatExpireTime > StaticData.now;
		}
	}

	public bool HaveCorpusPercentage
	{
		get
		{
			return this.corpusPercentageExpireTime > StaticData.now;
		}
	}

	public bool HaveDamageBooster
	{
		get
		{
			return this.boostDamageExpireTime > StaticData.now;
		}
	}

	public bool HaveEndurancePercentage
	{
		get
		{
			return this.endurancePercentageExpireTime > StaticData.now;
		}
	}

	public bool HaveExperienceBooster
	{
		get
		{
			return this.boostExperienceExpireTime > StaticData.now;
		}
	}

	public bool HaveIonDamageFlat
	{
		get
		{
			return this.ionDamageFlatExpireTime > StaticData.now;
		}
	}

	public bool HaveIonDamagePercentage
	{
		get
		{
			return this.ionDamagePercentageExpireTime > StaticData.now;
		}
	}

	public bool HaveLaserDamageFlat
	{
		get
		{
			return this.laserDamageFlatExpireTime > StaticData.now;
		}
	}

	public bool HaveLaserDamagePercentage
	{
		get
		{
			return this.laserDamagePercentageExpireTime > StaticData.now;
		}
	}

	public bool HavePlasmaDamageFlat
	{
		get
		{
			return this.plasmaDamageFlatExpireTime > StaticData.now;
		}
	}

	public bool HavePlasmaDamagePercentage
	{
		get
		{
			return this.plasmaDamagePercentageExpireTime > StaticData.now;
		}
	}

	public bool HaveShieldFlat
	{
		get
		{
			return this.shieldFlatExpireTime > StaticData.now;
		}
	}

	public bool HaveShieldPercentage
	{
		get
		{
			return this.shieldPercentageExpireTime > StaticData.now;
		}
	}

	public bool HaveShieldPowerFlat
	{
		get
		{
			return this.shieldPowerFlatExpireTime > StaticData.now;
		}
	}

	public bool HaveShieldPowerPercentage
	{
		get
		{
			return this.shieldPowerPercentageExpireTime > StaticData.now;
		}
	}

	public bool HaveTargetingFlat
	{
		get
		{
			return this.targetingFlatExpireTime > StaticData.now;
		}
	}

	public bool HaveTargetingPercentage
	{
		get
		{
			return this.targetingPercentageExpireTime > StaticData.now;
		}
	}

	public bool HaveTotalDamagePercentage
	{
		get
		{
			return this.totalDamagePercentageExpireTime > StaticData.now;
		}
	}

	public PlayerShipNet SelectedShip
	{
		get
		{
			PlayerShipNet playerShipNet = (
				from ps in this.playerShips
				where ps.ShipID == this.selectedShipId
				select ps).First<PlayerShipNet>();
			return playerShipNet;
		}
	}

	public PlayerBelongings()
	{
	}

	public OparationResult AddAmmo(int ammoType, int amount)
	{
		OparationResult oparationResult = OparationResult.Error;
		if (amount > this.AllowedAmmo(ammoType))
		{
			oparationResult = OparationResult.BadParameters;
			throw new Exception("Invalid ammo amount");
		}
		SortedList<byte, short> nums = this.FindAmmoSlots(ammoType);
		int item = amount;
		foreach (byte key in nums.Keys)
		{
			if (nums[key] < item)
			{
				SlotItem slotItem = (
					from t in this.playerItems.slotItems
					where (t.ItemType != ammoType || t.Slot != key ? false : t.SlotType == 1)
					select t).First<SlotItem>();
				slotItem.Amount = slotItem.Amount + nums[key];
				item = item - nums[key];
			}
			else
			{
				SlotItem slotItem1 = (
					from t in this.playerItems.slotItems
					where (t.ItemType != ammoType || t.Slot != key ? false : t.SlotType == 1)
					select t).First<SlotItem>();
				slotItem1.Amount = slotItem1.Amount + item;
				item = 0;
				oparationResult = OparationResult.OK;
				break;
			}
		}
		if (item > 0)
		{
			this.playerItems.AddSlotItem((ushort)ammoType, (long)item, (int)this.FirstFreeInventorySlot(), 1, 0);
			oparationResult = OparationResult.OK;
		}
		return oparationResult;
	}

	public void AddExperience(long value)
	{
		int count = 50 + this.unlockedPortals.Count * 2;
		if (this.playerLevel < count)
		{
			if (this.playerItems.GetAmountAt(PlayerItems.TypeExperience) + value <= StaticData.levelsType[count].tottalExp)
			{
				this.playerItems.Add(PlayerItems.TypeExperience, value);
			}
			else
			{
				this.playerItems.Set(PlayerItems.TypeExperience, StaticData.levelsType[count].tottalExp);
			}
		}
	}

	public void AddHonor(long value)
	{
		this.playerItems.Add(PlayerItems.TypeHonor, value);
		if (this.playerItems.Honor < (long)0)
		{
			this.playerItems.Set(PlayerItems.TypeHonor, (long)0);
		}
	}

	public OparationResult AddStackableItem(int itemType, int amount)
	{
		OparationResult oparationResult;
		OparationResult oparationResult1 = OparationResult.Error;
		int num = this.AllowedPileSize(itemType);
		if (num >= 1)
		{
			amount = Math.Min(amount, num);
			SortedList<byte, short> nums = this.FindStackPile(itemType);
			int item = amount;
			foreach (byte key in nums.Keys)
			{
				if (nums[key] < item)
				{
					SlotItem slotItem = (
						from t in this.playerItems.slotItems
						where (t.ItemType != itemType || t.Slot != key ? false : t.SlotType == 1)
						select t).First<SlotItem>();
					slotItem.Amount = slotItem.Amount + nums[key];
					item = item - nums[key];
				}
				else
				{
					SlotItem slotItem1 = (
						from t in this.playerItems.slotItems
						where (t.ItemType != itemType || t.Slot != key ? false : t.SlotType == 1)
						select t).First<SlotItem>();
					slotItem1.Amount = slotItem1.Amount + item;
					item = 0;
					oparationResult1 = OparationResult.OK;
					break;
				}
			}
			if (item > 0)
			{
				this.playerItems.AddSlotItem((ushort)itemType, (long)item, (int)this.FirstFreeInventorySlot(), 1, 0);
				oparationResult1 = OparationResult.OK;
			}
			oparationResult = oparationResult1;
		}
		else
		{
			oparationResult = OparationResult.BadParameters;
		}
		return oparationResult;
	}

	public int AllowedAmmo(int ammoType)
	{
		int amount = 0;
		SlotItem[] array = (
			from a in this.playerItems.slotItems
			where (a.ItemType != ammoType ? false : a.SlotType == 1)
			select a).ToArray<SlotItem>();
		SlotItem[] slotItemArray = array;
		for (int i = 0; i < (int)slotItemArray.Length; i++)
		{
			SlotItem slotItem = slotItemArray[i];
			amount = amount + (5000 - slotItem.Amount);
		}
		if (this.HaveAFreeSlot())
		{
			amount = amount + 5000;
		}
		return amount;
	}

	public int AllowedPileSize(int itemType)
	{
		int amount = 0;
		SlotItem[] array = (
			from a in this.playerItems.slotItems
			where (a.ItemType != itemType ? false : a.SlotType == 1)
			select a).ToArray<SlotItem>();
		SlotItem[] slotItemArray = array;
		for (int i = 0; i < (int)slotItemArray.Length; i++)
		{
			SlotItem slotItem = slotItemArray[i];
			amount = amount + (5000 - slotItem.Amount);
		}
		amount = amount + 5000 * (this.playerInventorySlots - (
			from t in this.playerItems.slotItems
			where t.SlotType == 1
			select t).Count<SlotItem>());
		return amount;
	}

	public ShipConfiguration BuildCfg(Guild guild)
	{
		int i;
		SlotItem[] array;
		int j;
		PlayerShipNet playerShipNet = (
			from s in this.playerShips
			where s.ShipID == this.selectedShipId
			select s).First<PlayerShipNet>();
		if (guild == null)
		{
			playerShipNet.guildResilience = 0;
			playerShipNet.guildExpirience = 0;
			playerShipNet.guildAccuracy = 0;
			playerShipNet.guildEndurance = 0;
		}
		else
		{
			GuildUpgrade guildUpgrade = (
				from t in StaticData.guildUpgradesInfo
				where (t.upgradeType != 2 ? false : t.upgradeLevel == guild.upgradeTwoLevel)
				select t).FirstOrDefault<GuildUpgrade>();
			playerShipNet.guildResilience = (guildUpgrade != null ? guildUpgrade.effectValue : 0);
			GuildUpgrade guildUpgrade1 = (
				from t in StaticData.guildUpgradesInfo
				where (t.upgradeType != 3 ? false : t.upgradeLevel == guild.upgradeThreeLevel)
				select t).FirstOrDefault<GuildUpgrade>();
			playerShipNet.guildExpirience = (guildUpgrade1 != null ? guildUpgrade1.effectValue : 0);
			GuildUpgrade guildUpgrade2 = (
				from t in StaticData.guildUpgradesInfo
				where (t.upgradeType != 4 ? false : t.upgradeLevel == guild.upgradeFourLevel)
				select t).FirstOrDefault<GuildUpgrade>();
			playerShipNet.guildAccuracy = (guildUpgrade2 != null ? guildUpgrade2.effectValue : 0);
			GuildUpgrade guildUpgrade3 = (
				from t in StaticData.guildUpgradesInfo
				where (t.upgradeType != 5 ? false : t.upgradeLevel == guild.upgradeFiveLevel)
				select t).FirstOrDefault<GuildUpgrade>();
			playerShipNet.guildEndurance = (guildUpgrade3 != null ? guildUpgrade3.effectValue : 0);
		}
		ShipsTypeNet shipsTypeNet = (
			from t in StaticData.shipTypes
			where t.id == playerShipNet.shipTypeId
			select t).First<ShipsTypeNet>();
		playerShipNet.ApplyBonuses(this);
		ShipConfiguration shipConfiguration = new ShipConfiguration();
		shipConfiguration = this.CalculateBonus(playerShipNet, this.playerItems);
		shipConfiguration.criticalEnergyMax = (float)Mathf.CeilToInt((float)(shipsTypeNet.corpus + shipsTypeNet.shield) * (0.14f + (float)this.playerLevel * 0.01f));
		shipConfiguration.criticalEnergyDrop = shipConfiguration.criticalEnergyMax / 5f;
		shipConfiguration.speedBoostConsumption = (float)(shipsTypeNet.shield * shipsTypeNet.tier) * 3f / 100f;
		shipConfiguration.maxBoostedSpeed = (playerShipNet.Velocity * 1.75f + 150f + (10f - (float)shipsTypeNet.tier * 2f) * 10f) / PlayerShipNet.SPEED_TRANSFORM;
		shipConfiguration.playerName = this.playerName;
		shipConfiguration.acceleration = shipsTypeNet.acceleration;
		shipConfiguration.assetName = shipsTypeNet.assetName;
		shipConfiguration.backAcceleration = shipsTypeNet.backAcceleration;
		shipConfiguration.distanceToStartDecelerate = shipsTypeNet.distanceDecelerate;
		shipConfiguration.floatUpSpeed = 0.5f;
		shipConfiguration.hitPointsMax = playerShipNet.Corpus;
		shipConfiguration.hitPoints = playerShipNet.CorpusHP;
		if (shipConfiguration.hitPoints > shipConfiguration.hitPointsMax)
		{
			shipConfiguration.hitPoints = shipConfiguration.hitPointsMax;
			playerShipNet.CorpusHP = shipConfiguration.hitPoints;
		}
		shipConfiguration.mass = 100f;
		shipConfiguration.maxRotationSpeed = shipsTypeNet.rotationSpeed;
		shipConfiguration.currentVelocity = playerShipNet.Velocity / PlayerShipNet.SPEED_TRANSFORM;
		shipConfiguration.velocityMax = shipConfiguration.currentVelocity;
		shipConfiguration.shipName = playerShipNet.ShipTitle;
		shipConfiguration.playerName = this.playerName;
		shipConfiguration.shieldMax = playerShipNet.Shield;
		shipConfiguration.shield = (float)playerShipNet.ShieldHP;
		if (shipConfiguration.shield > (float)shipConfiguration.shieldMax)
		{
			shipConfiguration.shield = (float)shipConfiguration.shieldMax;
			playerShipNet.ShieldHP = (int)shipConfiguration.shield;
		}
		shipConfiguration.cargoMax = (int)playerShipNet.MaxCargo;
		shipConfiguration.playerItems = this.playerItems;
		shipConfiguration.playerLevel = this.playerLevel;
		shipConfiguration.avoidanceMax = (float)playerShipNet.Avoidance;
		shipConfiguration.currentAvoidance = shipConfiguration.avoidanceMax;
		shipConfiguration.targeting = playerShipNet.Targeting;
		if (this.playerItems == null)
		{
			GameObjectPhysics.Log("DAMN!");
		}
		SlotItemWeapon[] slotItemWeaponArray = null;
		List<SlotItemWeapon> slotItemWeapons = new List<SlotItemWeapon>();
		ushort[] item = PlayerItems.categoriesMapping[2];
		for (i = 0; i < (int)item.Length; i++)
		{
			ushort num = item[i];
			array = (
				from t in this.playerItems.slotItems
				where (t.ItemType != num ? false : t.ShipId == this.selectedShipId)
				select t).ToArray<SlotItem>();
			for (j = 0; j < (int)array.Length; j++)
			{
				slotItemWeapons.Add((SlotItemWeapon)array[j]);
			}
		}
		slotItemWeaponArray = slotItemWeapons.ToArray();
		SlotItem[] slotItemArray = null;
		List<SlotItem> slotItems = new List<SlotItem>();
		item = PlayerItems.categoriesMapping[1];
		for (i = 0; i < (int)item.Length; i++)
		{
			ushort num1 = item[i];
			array = (
				from t in this.playerItems.slotItems
				where (t.ItemType != num1 ? false : t.SlotType == 1)
				select t).ToArray<SlotItem>();
			for (j = 0; j < (int)array.Length; j++)
			{
				slotItems.Add(array[j]);
			}
		}
		slotItemArray = slotItems.ToArray();
		WeaponSlot[] weaponSlotArray = new WeaponSlot[6];
		for (int k = 0; k < 6; k++)
		{
			WeaponSlot weaponSlot = new WeaponSlot()
			{
				padre = shipConfiguration,
				slotId = (short)k
			};
			SlotItemWeapon slotItemWeapon = (
				from a3 in (IEnumerable<SlotItemWeapon>)slotItemWeaponArray
				where a3.Slot == k
				select a3).FirstOrDefault<SlotItemWeapon>();
			if (slotItemWeapon != null)
			{
				weaponSlot.isActive = slotItemWeapon.IsActive;
				weaponSlot.isAttached = true;
				if (slotItemWeapon.AmmoType != 0)
				{
					weaponSlot.selectedAmmoItemType = slotItemWeapon.AmmoType;
				}
				else
				{
					weaponSlot.selectedAmmoItemType = PlayerItems.TypeAmmoSolarCells;
				}
				WeaponsTypeNet weaponsTypeNet = (WeaponsTypeNet)StaticData.allTypes[slotItemWeapon.ItemType];
				weaponSlot.realReloadTime = (long)(slotItemWeapon.CooldownTotal * 10000);
				weaponSlot.totalShootRange = (float)slotItemWeapon.RangeTotal;
				weaponSlot.totalTargeting = (float)slotItemWeapon.TargetingTotal;
				weaponSlot.weaponPenetration = slotItemWeapon.PenetrationTotal;
				weaponSlot.skillDamage = this.GetWeaponDamage(slotItemWeapon, shipConfiguration);
				if (weaponSlot.isActive)
				{
					ShipConfiguration shipConfiguration1 = shipConfiguration;
					shipConfiguration1.skillDamage = shipConfiguration1.skillDamage + weaponSlot.skillDamage;
				}
				if (!(slotItemWeapon.ItemType == PlayerItems.TypeWeaponLaserTire1 || slotItemWeapon.ItemType == PlayerItems.TypeWeaponLaserTire2 || slotItemWeapon.ItemType == PlayerItems.TypeWeaponLaserTire3 || slotItemWeapon.ItemType == PlayerItems.TypeWeaponLaserTire4 ? false : slotItemWeapon.ItemType != PlayerItems.TypeWeaponLaserTire5))
				{
					ShipConfiguration targetingTotal = shipConfiguration;
					targetingTotal.targetingForLaser = targetingTotal.targetingForLaser + slotItemWeapon.TargetingTotal;
				}
				else if ((slotItemWeapon.ItemType == PlayerItems.TypeWeaponPlasmaTire1 || slotItemWeapon.ItemType == PlayerItems.TypeWeaponPlasmaTire2 || slotItemWeapon.ItemType == PlayerItems.TypeWeaponPlasmaTire3 || slotItemWeapon.ItemType == PlayerItems.TypeWeaponPlasmaTire4 ? false : slotItemWeapon.ItemType != PlayerItems.TypeWeaponPlasmaTire5))
				{
					ShipConfiguration targetingTotal1 = shipConfiguration;
					targetingTotal1.targetingForIon = targetingTotal1.targetingForIon + slotItemWeapon.TargetingTotal;
				}
				else
				{
					ShipConfiguration targetingTotal2 = shipConfiguration;
					targetingTotal2.targetingForPlasma = targetingTotal2.targetingForPlasma + slotItemWeapon.TargetingTotal;
				}
				weaponSlot.weaponTierId = slotItemWeapon.ItemType;
			}
			else
			{
				weaponSlot.selectedAmmoItemType = PlayerItems.TypeAmmoSolarCells;
				weaponSlot.isActive = false;
				weaponSlot.isAttached = false;
			}
			weaponSlotArray[k] = weaponSlot;
		}
		shipConfiguration.weaponSlots = weaponSlotArray;
		this.SetRealWeaponCooldown(shipConfiguration);
		return shipConfiguration;
	}

	public OparationResult BuyAmmo(int ammoType, int amount, int priceCash, int priceNova, int priceViral, SelectedCurrency currency, out int ammoPrice)
	{
		OparationResult oparationResult;
		OparationResult oparationResult1 = OparationResult.Error;
		long cash = this.playerItems.Cash;
		long nova = this.playerItems.Nova;
		long amountAt = this.playerItems.GetAmountAt(PlayerItems.TypeEquilibrium);
		double num = 0;
		double num1 = 0;
		double num2 = 0;
		ammoPrice = 0;
		num = (double)((float)priceCash / 100f * (float)amount);
		num1 = (double)((float)priceNova / 100f * (float)amount);
		num2 = (double)((float)priceViral / 100f * (float)amount);
		switch (currency)
		{
			case SelectedCurrency.Cash:
			{
				if ((num < 0 || (double)cash < num ? false : priceCash != 0))
				{
					break;
				}
				else
				{
					oparationResult = OparationResult.Error;
					return oparationResult;
				}
			}
			case SelectedCurrency.Nova:
			{
				if ((num1 < 0 || (double)nova < num1 ? false : priceNova != 0))
				{
					break;
				}
				else
				{
					oparationResult = OparationResult.Error;
					return oparationResult;
				}
			}
			case SelectedCurrency.Equilibrium:
			{
				if ((num2 < 0 || (double)amountAt < num2 ? false : priceViral != 0))
				{
					break;
				}
				else
				{
					oparationResult = OparationResult.Error;
					return oparationResult;
				}
			}
		}
		oparationResult1 = this.AddAmmo(ammoType, amount);
		if (oparationResult1 == OparationResult.OK)
		{
			if ((priceCash <= 0 ? false : currency == SelectedCurrency.Cash))
			{
				ammoPrice = (int)num;
				this.playerItems.Add(PlayerItems.TypeCash, -(long)num);
			}
			if ((priceNova <= 0 ? false : currency == SelectedCurrency.Nova))
			{
				ammoPrice = (int)num1;
				this.playerItems.Add(PlayerItems.TypeNova, -(long)num1);
			}
			if ((priceViral <= 0 ? false : currency == SelectedCurrency.Equilibrium))
			{
				ammoPrice = (int)num2;
				this.playerItems.Add(PlayerItems.TypeEquilibrium, -(long)num2);
			}
		}
		oparationResult = oparationResult1;
		return oparationResult;
	}

	public ShipConfiguration CalculateBonus(PlayerShipNet pShip, PlayerItems pItems)
	{
		int item;
		int num;
		int num1;
		ShipConfiguration shipConfiguration = new ShipConfiguration();
		float single = 0f;
		float single1 = 0f;
		float powerUpEffectValue = 0f;
		int bonusFour = 0;
		int bonusFive = 0;
		int bonusFive1 = 0;
		float powerUpEffectValue1 = 0f;
		float powerUpEffectValue2 = 0f;
		float single2 = 0f;
		int item1 = 0;
		int item2 = 0;
		int num2 = 0;
		float bonusOne = 0f;
		float bonusTwo = 0f;
		float bonusThree = 0f;
		float bonusFour1 = 0f;
		float bonusFive2 = 0f;
		float single3 = 0f;
		float single4 = 0f;
		float single5 = 0f;
		int num3 = 100;
		float single6 = 0f;
		float single7 = 0f;
		float single8 = 0f;
		SlotItem[] array = (
			from t in pItems.slotItems
			where t.ShipId == pShip.ShipID
			select t).ToArray<SlotItem>();
		SlotItem[] slotItemArray = array;
		for (int i = 0; i < (int)slotItemArray.Length; i++)
		{
			SlotItem slotItem = slotItemArray[i];
			if ((PlayerItems.IsShield(slotItem.ItemType) || PlayerItems.IsCorpus(slotItem.ItemType) || PlayerItems.IsEngine(slotItem.ItemType) ? true : PlayerItems.IsExtraOther(slotItem.ItemType)))
			{
				bonusFive2 = bonusFive2 + (float)slotItem.BonusFive;
			}
			if (PlayerItems.IsExtraCargoMining(slotItem.ItemType))
			{
				bonusFour1 = bonusFour1 + (float)slotItem.BonusFive;
			}
			if (PlayerItems.IsExtraOther(slotItem.ItemType))
			{
				bonusFour1 = bonusFour1 + (float)slotItem.BonusFour;
			}
			if (PlayerItems.IsExtraCooldown(slotItem.ItemType))
			{
				bonusOne = bonusOne + (float)slotItem.BonusOne;
				bonusTwo = bonusTwo + (float)slotItem.BonusTwo;
				bonusThree = bonusThree + (float)slotItem.BonusThree;
				bonusFour = bonusFour + slotItem.BonusFour;
				bonusFive = bonusFive + slotItem.BonusFive;
			}
			if (PlayerItems.IsExtraDamage(slotItem.ItemType))
			{
				bonusFour = bonusFour + slotItem.BonusThree;
				bonusFive = bonusFive + slotItem.BonusFour;
				bonusFive1 = bonusFive1 + slotItem.BonusFive;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraBasicCPUforShildRegen)
			{
				bonusFive2 = bonusFive2 + (float)((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraBasicCPUforShildRegen]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraAdvancedCPUforShildRegen)
			{
				bonusFive2 = bonusFive2 + (float)((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraAdvancedCPUforShildRegen]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraOverclockedCPUforShildRegen)
			{
				bonusFive2 = bonusFive2 + (float)((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraOverclockedCPUforShildRegen]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraCPUforShildRegen30)
			{
				bonusFive2 = bonusFive2 + (float)((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraCPUforShildRegen30]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraCPUforShildRegen40)
			{
				bonusFive2 = bonusFive2 + (float)((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraCPUforShildRegen40]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraCPUforShildRegen50)
			{
				bonusFive2 = bonusFive2 + (float)((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraCPUforShildRegen50]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraCPUforShildRegen60)
			{
				bonusFive2 = bonusFive2 + (float)((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraCPUforShildRegen60]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraCPUforShildRegen70)
			{
				bonusFive2 = bonusFive2 + (float)((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraCPUforShildRegen70]).efValue;
			}
			if ((slotItem.ItemType == PlayerItems.TypeExtraLaserWeaponsModule || slotItem.ItemType == PlayerItems.TypeExtraLaserWeaponsModule1 || slotItem.ItemType == PlayerItems.TypeExtraLaserWeaponsModule2 || slotItem.ItemType == PlayerItems.TypeExtraLaserWeaponsModule3 || slotItem.ItemType == PlayerItems.TypeExtraLaserWeaponsModule4 ? true : slotItem.ItemType == PlayerItems.TypeExtraLaserWeaponsModule5))
			{
				item = ((ExtrasNet)StaticData.allTypes[slotItem.ItemType]).efValue;
				powerUpEffectValue1 = powerUpEffectValue1 + (float)item;
			}
			if ((slotItem.ItemType == PlayerItems.TypeExtraPlasmaWeaponsModule || slotItem.ItemType == PlayerItems.TypeExtraPlasmaWeaponsModule1 || slotItem.ItemType == PlayerItems.TypeExtraPlasmaWeaponsModule2 || slotItem.ItemType == PlayerItems.TypeExtraPlasmaWeaponsModule3 || slotItem.ItemType == PlayerItems.TypeExtraPlasmaWeaponsModule4 ? true : slotItem.ItemType == PlayerItems.TypeExtraPlasmaWeaponsModule5))
			{
				item = ((ExtrasNet)StaticData.allTypes[slotItem.ItemType]).efValue;
				powerUpEffectValue2 = powerUpEffectValue2 + (float)item;
			}
			if ((slotItem.ItemType == PlayerItems.TypeExtraIonWeaponsModule || slotItem.ItemType == PlayerItems.TypeExtraIonWeaponsModule1 || slotItem.ItemType == PlayerItems.TypeExtraIonWeaponsModule2 || slotItem.ItemType == PlayerItems.TypeExtraIonWeaponsModule3 || slotItem.ItemType == PlayerItems.TypeExtraIonWeaponsModule4 ? true : slotItem.ItemType == PlayerItems.TypeExtraIonWeaponsModule5))
			{
				item = ((ExtrasNet)StaticData.allTypes[slotItem.ItemType]).efValue;
				single2 = single2 + (float)item;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraUltraWeaponsModule)
			{
				item = ((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraUltraWeaponsModule]).efValue;
				powerUpEffectValue1 = powerUpEffectValue1 + (float)item;
				powerUpEffectValue2 = powerUpEffectValue2 + (float)item;
				single2 = single2 + (float)item;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraLaserAimingCPU)
			{
				item1 = item1 + ((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraLaserAimingCPU]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraPlasmaAimingCPU)
			{
				item2 = item2 + ((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraPlasmaAimingCPU]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraIonAimingCPU)
			{
				num2 = num2 + ((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraIonAimingCPU]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraUltraAimingCPU)
			{
				item = ((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraUltraAimingCPU]).efValue;
				item1 = item1 + item;
				item2 = item2 + item;
				num2 = num2 + item;
			}
			if ((slotItem.ItemType == PlayerItems.TypeExtraLightMiningDrill ? true : slotItem.ItemType == PlayerItems.TypeExtraUltraMiningDrill))
			{
				bonusFour1 = bonusFour1 + (float)((ExtrasNet)StaticData.allTypes[slotItem.ItemType]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraLaserWeaponsCoolant)
			{
				bonusOne = bonusOne + (float)((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraLaserWeaponsCoolant]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraLaserWeaponsCoolant1)
			{
				bonusOne = bonusOne + (float)((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraLaserWeaponsCoolant1]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraLaserWeaponsCoolant2)
			{
				bonusOne = bonusOne + (float)((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraLaserWeaponsCoolant2]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraLaserWeaponsCoolant3)
			{
				bonusOne = bonusOne + (float)((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraLaserWeaponsCoolant3]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraLaserWeaponsCoolant4)
			{
				bonusOne = bonusOne + (float)((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraLaserWeaponsCoolant4]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraLaserWeaponsCoolant5)
			{
				bonusOne = bonusOne + (float)((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraLaserWeaponsCoolant5]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraPlasmaWeaponsCoolant)
			{
				bonusTwo = bonusTwo + (float)((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraPlasmaWeaponsCoolant]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraPlasmaWeaponsCoolant1)
			{
				bonusTwo = bonusTwo + (float)((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraPlasmaWeaponsCoolant1]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraPlasmaWeaponsCoolant2)
			{
				bonusTwo = bonusTwo + (float)((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraPlasmaWeaponsCoolant2]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraPlasmaWeaponsCoolant3)
			{
				bonusTwo = bonusTwo + (float)((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraPlasmaWeaponsCoolant3]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraPlasmaWeaponsCoolant4)
			{
				bonusTwo = bonusTwo + (float)((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraPlasmaWeaponsCoolant4]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraPlasmaWeaponsCoolant5)
			{
				bonusTwo = bonusTwo + (float)((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraPlasmaWeaponsCoolant5]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraIonWeaponsCoolant)
			{
				bonusThree = bonusThree + (float)((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraIonWeaponsCoolant]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraIonWeaponsCoolant1)
			{
				bonusThree = bonusThree + (float)((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraIonWeaponsCoolant1]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraIonWeaponsCoolant2)
			{
				bonusThree = bonusThree + (float)((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraIonWeaponsCoolant2]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraIonWeaponsCoolant3)
			{
				bonusThree = bonusThree + (float)((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraIonWeaponsCoolant3]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraIonWeaponsCoolant4)
			{
				bonusThree = bonusThree + (float)((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraIonWeaponsCoolant4]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraIonWeaponsCoolant5)
			{
				bonusThree = bonusThree + (float)((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraIonWeaponsCoolant5]).efValue;
			}
			if (slotItem.ItemType == PlayerItems.TypeExtraUltraWeaponsCoolant)
			{
				item = ((ExtrasNet)StaticData.allTypes[PlayerItems.TypeExtraUltraWeaponsCoolant]).efValue;
				bonusOne = bonusOne + (float)item;
				bonusTwo = bonusTwo + (float)item;
				bonusThree = bonusThree + (float)item;
			}
		}
		pItems.GetSkillEffect(PlayerItems.TypeTalentsBountySpecialist, out num, out num1);
		single = (float)(num + num1);
		pItems.GetSkillEffect(PlayerItems.TypeTalentsAlienSpecialist, out num, out num1);
		single1 = (float)(num + num1);
		pItems.GetSkillEffect(PlayerItems.TypeTalentsWeaponMastery, out num, out num1);
		powerUpEffectValue = powerUpEffectValue + (float)(num + num1);
		pItems.GetSkillEffect(PlayerItems.TypeTalentsMerchant, out num, out num1);
		num3 = num3 + num + num1;
		pItems.GetSkillEffect(PlayerItems.TypeTalentsDamageReduction, out num, out num1);
		single5 = single5 + (float)(num + num1);
		pItems.GetSkillEffect(PlayerItems.TypeTalentsSwiftLearner, out num, out num1);
		single3 = single3 + (float)(num + num1);
		if (this.FactionBoostResilience)
		{
			single5 = single5 + 20f;
		}
		if (this.FactionBoostPvPDamage)
		{
			single = single + 20f;
		}
		if (this.FactionBoostPvEDamage)
		{
			single1 = single1 + 20f;
		}
		if (this.FactionBoostMining)
		{
			bonusFour1 = bonusFour1 + 50f;
		}
		if (this.FactionBoostFusion)
		{
			single6 = single6 + 20f;
			single7 = single7 + 10f;
		}
		if (this.FactionBoostIncome)
		{
			single8 = single8 + 10f;
		}
		single5 = single5 + (float)pShip.guildResilience;
		single3 = single3 + (float)pShip.guildExpirience;
		if (this.HaveExperienceBooster)
		{
			single3 = single3 + 100f;
		}
		single3 = single3 + (float)this.factionWarXpBonus;
		if (this.factionGalaxyXpGain)
		{
			single3 = single3 + 20f;
		}
		if (this.factionGalaxySellBonus)
		{
			num3 = num3 + 100;
		}
		pItems.GetSkillEffect(PlayerItems.TypeTalentsEmpoweredShield, out num, out num1);
		bonusFive2 = bonusFive2 + (float)(num + num1);
		if (this.HaveShieldPowerFlat)
		{
			bonusFive2 = bonusFive2 + (float)this.GetPowerUpEffectValue(PlayerItems.TypePowerUpForShieldPowerFlat);
		}
		if (this.HaveShieldPowerPercentage)
		{
			bonusFive2 = bonusFive2 + bonusFive2 * (float)this.GetPowerUpEffectValue(PlayerItems.TypePowerUpForShieldPowerPercentage) / 100f;
		}
		single4 = bonusFive2 / (bonusFive2 + 150f + (float)(8 * this.playerLevel)) * 100f;
		if (this.HaveLaserDamageFlat)
		{
			bonusFour = bonusFour + this.GetPowerUpEffectValue(PlayerItems.TypePowerUpForLaserDamageFlat);
		}
		if (this.HaveLaserDamagePercentage)
		{
			powerUpEffectValue1 = powerUpEffectValue1 + (float)this.GetPowerUpEffectValue(PlayerItems.TypePowerUpForLaserDamagePercentage);
		}
		if (this.HavePlasmaDamageFlat)
		{
			bonusFive = bonusFive + this.GetPowerUpEffectValue(PlayerItems.TypePowerUpForPlasmaDamageFlat);
		}
		if (this.HavePlasmaDamagePercentage)
		{
			powerUpEffectValue2 = powerUpEffectValue2 + (float)this.GetPowerUpEffectValue(PlayerItems.TypePowerUpForPlasmaDamagePercentage);
		}
		if (this.HaveIonDamageFlat)
		{
			bonusFive1 = bonusFive1 + this.GetPowerUpEffectValue(PlayerItems.TypePowerUpForIonDamageFlat);
		}
		if (this.HaveIonDamagePercentage)
		{
			single2 = single2 + (float)this.GetPowerUpEffectValue(PlayerItems.TypePowerUpForIonDamagePercentage);
		}
		if (this.HaveTotalDamagePercentage)
		{
			powerUpEffectValue = powerUpEffectValue + (float)this.GetPowerUpEffectValue(PlayerItems.TypePowerUpForTotalDamagePercentage);
		}
		shipConfiguration.dmgPercentForPlayer = single;
		shipConfiguration.dmgPercentForAlien = single1;
		shipConfiguration.dmgPercentForAllWeapon = powerUpEffectValue;
		shipConfiguration.dmgPercentBonusForEachLaser = powerUpEffectValue1;
		shipConfiguration.dmgPercentBonusForEachPlasma = powerUpEffectValue2;
		shipConfiguration.dmgPercentBonusForEachIon = single2;
		shipConfiguration.dmgFlatBonusForEachLaser = bonusFour;
		shipConfiguration.dmgFlatBonusForEachPlasma = bonusFive;
		shipConfiguration.dmgFlatBonusForEachIon = bonusFive1;
		shipConfiguration.targetingForLaser = item1;
		shipConfiguration.targetingForPlasma = item2;
		shipConfiguration.targetingForIon = num2;
		shipConfiguration.laserCooldown = bonusOne;
		shipConfiguration.plasmaCooldown = bonusTwo;
		shipConfiguration.ionCooldown = bonusThree;
		shipConfiguration.fasterMining = bonusFour1;
		shipConfiguration.shieldRepairPerSec = bonusFive2;
		shipConfiguration.experienceGain = single3;
		shipConfiguration.damageReductionItems = single4;
		shipConfiguration.resilience = single5;
		shipConfiguration.sellBonus = num3;
		shipConfiguration.fusionPriceOff = (byte)single6;
		shipConfiguration.ammoCreationBonus = (byte)single7;
		shipConfiguration.epIncomeBonus = (byte)single8;
		shipConfiguration.cargoBooster = this.HaveCargoBooster;
		shipConfiguration.miningBooster = this.HaveAutoMinerBooster;
		shipConfiguration.damageBooster = this.HaveDamageBooster;
		shipConfiguration.experienceBooster = this.HaveExperienceBooster;
		this.isAutoMiningOn = shipConfiguration.miningBooster;
		shipConfiguration.haveLaserDamageFlat = this.HaveLaserDamageFlat;
		shipConfiguration.havePlasmaDamageFlat = this.HavePlasmaDamageFlat;
		shipConfiguration.haveIonDamageFlat = this.HaveIonDamageFlat;
		shipConfiguration.haveLaserDamagePercentage = this.HaveLaserDamagePercentage;
		shipConfiguration.havePlasmaDamagePercentage = this.HavePlasmaDamagePercentage;
		shipConfiguration.haveIonDamagePercentage = this.HaveIonDamagePercentage;
		shipConfiguration.haveTotalDamagePercentage = this.HaveTotalDamagePercentage;
		shipConfiguration.haveCorpusFlat = this.HaveCorpusFlat;
		shipConfiguration.haveCorpusPercentage = this.HaveCorpusPercentage;
		shipConfiguration.haveShieldFlat = this.HaveShieldFlat;
		shipConfiguration.haveShieldPercentage = this.HaveShieldPercentage;
		shipConfiguration.haveEndurancePercentage = this.HaveEndurancePercentage;
		shipConfiguration.haveShieldPowerFlat = this.HaveShieldPowerFlat;
		shipConfiguration.haveShieldPowerPercentage = this.HaveShieldPowerPercentage;
		shipConfiguration.haveTargetingFlat = this.HaveTargetingFlat;
		shipConfiguration.haveTargetingPercentage = this.HaveTargetingPercentage;
		shipConfiguration.haveAvoidanceFlat = this.HaveAvoidanceFlat;
		shipConfiguration.haveAvoidancePercentage = this.HaveAvoidancePercentage;
		return shipConfiguration;
	}

	public bool CanBuyNextExpandedSlotNew(SelectedCurrency currency)
	{
		bool cash;
		if (this.playerLevel >= this.GetNextExtendedSlotLevelRequirement())
		{
			int priceOfShipSlotItem = this.GetPriceOfShipSlotItem(currency);
			if (currency == SelectedCurrency.Cash)
			{
				cash = this.playerItems.Cash >= (long)priceOfShipSlotItem;
				return cash;
			}
			else if (currency == SelectedCurrency.Nova)
			{
				cash = this.playerItems.Nova >= (long)priceOfShipSlotItem;
				return cash;
			}
			else if (currency == SelectedCurrency.Equilibrium)
			{
				cash = this.playerItems.Equilibrium >= (long)priceOfShipSlotItem;
				return cash;
			}
		}
		cash = false;
		return cash;
	}

	public bool CanInvestPartInPortal(int portalId, ushort partType)
	{
		bool flag;
		Portal portal = (
			from p in StaticData.allPortals
			where p.portalId == portalId
			select p).FirstOrDefault<Portal>();
		if (portal == null)
		{
			flag = false;
		}
		else if (portal.parts.ContainsKey(partType))
		{
			PortalPart portalPart = (
				from p in this.playerItems.portalParts
				where (p.portalId != portalId ? false : p.partTypeId == partType)
				select p).FirstOrDefault<PortalPart>();
			flag = ((portalPart == null ? true : portal.parts[partType] > portalPart.partAmount) ? true : false);
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
		long cargoCapacity = CargoCapacity - this.playerItems.Cargo;
		if ((mineral.items == null ? true : mineral.resourceQuantities.Count != 0))
		{
			if ((mineral.items != null ? false : mineral.resourceQuantities.Count > 0))
			{
				if (cargoCapacity <= (long)0)
				{
					foreach (KeyValuePair<ushort, int> resourceQuantity in mineral.resourceQuantities)
					{
						if (!PlayerItems.cargoTypes.Contains<ushort>(resourceQuantity.Key))
						{
							flag = true;
							return flag;
						}
					}
				}
				else
				{
					flag = true;
					return flag;
				}
			}
			flag = false;
		}
		else
		{
			foreach (SlotItem item in mineral.items)
			{
				if (PlayerItems.IsStackable(item.ItemType))
				{
					if (this.AllowedPileSize((int)item.ItemType) > 0)
					{
						flag = true;
						return flag;
					}
				}
			}
			flag = this.HaveAFreeSlot();
		}
		return flag;
	}

	public bool CanSkipQuest(NewQuest quest, out int skipPrice)
	{
		bool nova;
		if (quest.type != QuestTypeEnum.Daily)
		{
			skipPrice = quest.GetSkipPrice(this.playerLevel);
			nova = this.playerItems.Nova >= (long)skipPrice;
		}
		else
		{
			skipPrice = 0;
			nova = false;
		}
		return nova;
	}

	public void ChangeZoomLeve(short lvl)
	{
		if ((lvl < 0 ? false : lvl <= 12))
		{
			this.zoomLevel = lvl;
		}
	}

	public MakeSynthesisResult CreateAmmoFromMineral(ushort ammoKey, int packetsCount, out int equilibriumUsed, byte creatAmmoBonus = 0)
	{
		ushort key = 0;
		MakeSynthesisResult makeSynthesisResult;
		equilibriumUsed = 0;
		int item = PlayerItems.specialAmounts[ammoKey];
		int num = (int)((float)(packetsCount * item) * (1f + (float)creatAmmoBonus / 100f));
		MakeSynthesisResult makeSynthesisResult1 = MakeSynthesisResult.Unknown;
		if (num <= this.AllowedAmmo((int)ammoKey))
		{
			SortedList<ushort, short> nums = PlayerItems.fusionDependancies[ammoKey];
			foreach (ushort k in nums.Keys)
			{
				if (k == PlayerItems.TypeEquilibrium)
				{
					if ((long)(nums[k] * packetsCount) > this.playerItems.Equilibrium)
					{
						makeSynthesisResult = MakeSynthesisResult.NotEnoughMinerals;
						return makeSynthesisResult;
					}
				}
				else if ((long)(nums[key] * packetsCount) > this.playerItems.GetMinetalQty(key))
				{
					makeSynthesisResult = MakeSynthesisResult.NotEnoughMinerals;
					return makeSynthesisResult;
				}
			}
			foreach (ushort key1 in nums.Keys)
			{
				if (key1 != PlayerItems.TypeEquilibrium)
				{
					this.playerItems.UseMineral(key1, nums[key1] * packetsCount);
				}
				else
				{
					equilibriumUsed = nums[key1] * packetsCount;
					this.playerItems.AddEquilibrium((long)(equilibriumUsed * -1));
				}
			}
			if (this.AddAmmo((int)ammoKey, num) == OparationResult.OK)
			{
				makeSynthesisResult1 = MakeSynthesisResult.OK;
			}
			makeSynthesisResult = makeSynthesisResult1;
		}
		else
		{
			makeSynthesisResult = MakeSynthesisResult.Unknown;
		}
		return makeSynthesisResult;
	}

	public void Deserialize(BinaryReader br)
	{
		int i;
		this.guildInvitesCount = br.ReadInt16();
		this.isAutoFireOn = br.ReadBoolean();
		this.isAutoMiningOn = br.ReadBoolean();
		this.isShowMoreDetailsOn = br.ReadBoolean();
		this.zoomLevel = br.ReadInt16();
		this.playerAccessLevel = br.ReadByte();
		this.cuLevel = br.ReadByte();
		this.socialOptionsStatus = br.ReadByte();
		this.playerName = br.ReadString();
		this.playerTalentClass = br.ReadInt16();
		this.lastPurchaseId = br.ReadInt32();
		this.selectedShipId = br.ReadInt32();
		int num = br.ReadInt32();
		this.playerShips = new PlayerShipNet[num];
		for (i = 0; i < num; i++)
		{
			this.playerShips[i] = new PlayerShipNet();
			this.playerShips[i].Deserialize(br);
		}
		this.playerObjectives = new PlayerObjectives();
		this.playerObjectives.Deserialize(br);
		this.playerItems = new PlayerItems();
		this.playerItems.Deserialize(br);
		this.skillConfig = new ActiveSkillBarConfig();
		this.skillConfig.Deserialize(br);
		this.playerInventorySlots = br.ReadByte();
		this.playerVaultSlots = br.ReadByte();
		this.playerLevel = br.ReadInt16();
		int num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.penaltyTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.penaltyTime = DateTime.MinValue;
		}
		this.penaltyDuration = br.ReadInt16();
		this.firstWinBonusRecived = br.ReadBoolean();
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.nextPvPRoundTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.nextPvPRoundTime = DateTime.MinValue;
		}
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.oldestPvPGameStartTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.oldestPvPGameStartTime = DateTime.MinValue;
		}
		this.pvpGamePoolCapacity = br.ReadByte();
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.signingPvPGameTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.signingPvPGameTime = DateTime.MinValue;
		}
		this.visitedNPCs = new List<short>();
		int num2 = br.ReadInt32();
		for (i = 0; i < num2; i++)
		{
			short num3 = br.ReadInt16();
			this.visitedNPCs.Add(num3);
		}
		num = br.ReadInt32();
		if (num != -1)
		{
			this.playerQuests = new SortedList<int, PlayerQuest>();
			for (i = 0; i < num; i++)
			{
				int num4 = br.ReadInt32();
				PlayerQuest playerQuest = new PlayerQuest();
				playerQuest.Deserialize(br);
				this.playerQuests.Add(num4, playerQuest);
			}
		}
		else
		{
			this.playerQuests = null;
		}
		this.haveExtendetCorpusOne = br.ReadBoolean();
		this.haveExtendetShielOne = br.ReadBoolean();
		this.haveExtendetEngineOne = br.ReadBoolean();
		this.haveExtendetAnyOne = br.ReadBoolean();
		this.haveExtendetExtraOne = br.ReadBoolean();
		this.haveExtendetExtraTwo = br.ReadBoolean();
		num = br.ReadInt32();
		this.unlockedPortals = new List<int>();
		for (i = 0; i < num; i++)
		{
			this.unlockedPortals.Add(br.ReadInt32());
		}
		long num5 = br.ReadInt64();
		this.nextFreeTransformerUsage = DateTime.Now.Ticks + num5;
		this.transformerState = br.ReadByte();
		this.dailyMissionsDone = br.ReadByte();
		this.isDailyMissionsRewardCollected = br.ReadBoolean();
		this.referals.Deserialize(br);
		this.receivedDailyRewards = br.ReadByte();
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.boostAutoMinerExpireTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.boostAutoMinerExpireTime = DateTime.MinValue;
		}
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.boostCargoExpireTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.boostCargoExpireTime = DateTime.MinValue;
		}
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.boostDamageExpireTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.boostDamageExpireTime = DateTime.MinValue;
		}
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.boostExperienceExpireTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.boostExperienceExpireTime = DateTime.MinValue;
		}
		this.playerKeyboardShortcuts = new List<KeyboardShortcutPair>();
		int num6 = br.ReadInt32();
		for (i = 0; i < num6; i++)
		{
			KeyboardShortcutPair keyboardShortcutPair = new KeyboardShortcutPair();
			keyboardShortcutPair.Deserialize(br);
			this.playerKeyboardShortcuts.Add(keyboardShortcutPair);
		}
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.laserDamageFlatExpireTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.laserDamageFlatExpireTime = DateTime.MinValue;
		}
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.plasmaDamageFlatExpireTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.plasmaDamageFlatExpireTime = DateTime.MinValue;
		}
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.ionDamageFlatExpireTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.ionDamageFlatExpireTime = DateTime.MinValue;
		}
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.laserDamagePercentageExpireTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.laserDamagePercentageExpireTime = DateTime.MinValue;
		}
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.plasmaDamagePercentageExpireTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.plasmaDamagePercentageExpireTime = DateTime.MinValue;
		}
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.ionDamagePercentageExpireTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.ionDamagePercentageExpireTime = DateTime.MinValue;
		}
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.totalDamagePercentageExpireTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.totalDamagePercentageExpireTime = DateTime.MinValue;
		}
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.corpusFlatExpireTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.corpusFlatExpireTime = DateTime.MinValue;
		}
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.corpusPercentageExpireTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.corpusPercentageExpireTime = DateTime.MinValue;
		}
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.shieldFlatExpireTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.shieldFlatExpireTime = DateTime.MinValue;
		}
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.shieldPercentageExpireTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.shieldPercentageExpireTime = DateTime.MinValue;
		}
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.endurancePercentageExpireTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.endurancePercentageExpireTime = DateTime.MinValue;
		}
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.shieldPowerFlatExpireTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.shieldPowerFlatExpireTime = DateTime.MinValue;
		}
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.shieldPowerPercentageExpireTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.shieldPowerPercentageExpireTime = DateTime.MinValue;
		}
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.targetingFlatExpireTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.targetingFlatExpireTime = DateTime.MinValue;
		}
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.targetingPercentageExpireTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.targetingPercentageExpireTime = DateTime.MinValue;
		}
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.avoidanceFlatExpireTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.avoidanceFlatExpireTime = DateTime.MinValue;
		}
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.avoidancePercentageExpireTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.avoidancePercentageExpireTime = DateTime.MinValue;
		}
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.nextFreeGiftTime = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.nextFreeGiftTime = DateTime.MinValue;
		}
		num1 = br.ReadInt32();
		if (num1 != -1)
		{
			this.factionWarDayEnd = StaticData.now.AddSeconds((double)num1);
		}
		else
		{
			this.factionWarDayEnd = DateTime.MinValue;
		}
		this.factionWarRerollBonus = br.ReadByte();
		this.factionWarXpBonus = br.ReadByte();
		this.factionWarDay = (DayOfWeek)br.ReadByte();
		this.isWarInProgress = br.ReadBoolean();
		this.councilRank = br.ReadByte();
		this.myBattleBoostVote = br.ReadByte();
		this.myUtilityBoostVote = br.ReadByte();
		this.councilSkillSelected = br.ReadUInt16();
		this.day1Participation = br.ReadBoolean();
		this.day2Participation = br.ReadBoolean();
		this.day3Participation = br.ReadBoolean();
		this.day4Participation = br.ReadBoolean();
		this.day5Participation = br.ReadBoolean();
		this.day6Participation = br.ReadBoolean();
		this.factionWarDayScore = br.ReadInt32();
		this.rewardForDayProgressCollected1 = br.ReadBoolean();
		this.rewardForDayProgressCollected2 = br.ReadBoolean();
		this.rewardForDayProgressCollected3 = br.ReadBoolean();
		this.factionWarBattleBoost = br.ReadByte();
		this.factionWarUtilityBoost = br.ReadByte();
		this.weeklyRewardCollected = br.ReadBoolean();
		this.lastWeekPendingReward = br.ReadByte();
		this.factionGalaxyXpGain = br.ReadBoolean();
		this.factionGalaxySellBonus = br.ReadBoolean();
		this.warCommendationsBought = br.ReadByte();
	}

	public BuyResult ExchangeNovaForCash(int qty)
	{
		BuyResult buyResult;
		if (qty >= 1)
		{
			int num = (int)(Math.Ceiling(0.0016 * (double)(this.playerLevel * this.playerLevel) + (double)this.playerLevel) * 5);
			if (this.playerItems.Nova < (long)qty)
			{
				buyResult = BuyResult.NotEnoughNova;
			}
			else
			{
				this.playerItems.AddCash((long)(qty * num));
				this.playerItems.AddNova((long)(-qty));
				List<NovaUpdate> novaUpdates = this.playerItems.novaUpdates;
				NovaUpdate novaUpdate = new NovaUpdate()
				{
					amount = (long)(-qty),
					text = "GAME: Exchange for cash",
					currencyType = SelectedCurrency.Nova,
					itemType = PlayerItems.TypeCash,
					serviceType = ServiceType.NovaExchange
				};
				novaUpdates.Add(novaUpdate);
				buyResult = BuyResult.OK;
			}
		}
		else
		{
			buyResult = BuyResult.NotEnoughNova;
		}
		return buyResult;
	}

	public UpgradeResult ExpandInventory(out int price)
	{
		UpgradeResult upgradeResult;
		price = -1;
		if ((this.playerInventorySlots > 36 ? false : this.playerInventorySlots >= 16))
		{
			SlotPriceInfo slotPriceInfo = (
				from t in StaticData.slotPriceInformation
				where (t.slotId != this.playerInventorySlots + 4 ? false : t.slotType == "Inventory")
				select t).First<SlotPriceInfo>();
			if (slotPriceInfo.priceCash > 0)
			{
				if (this.playerItems.Cash < (long)slotPriceInfo.priceCash)
				{
					upgradeResult = UpgradeResult.NotEnoughCash;
					return upgradeResult;
				}
				this.playerItems.AddCash((long)(-slotPriceInfo.priceCash));
				PlayerBelongings playerBelonging = this;
				playerBelonging.playerInventorySlots = (byte)(playerBelonging.playerInventorySlots + 4);
			}
			else if (slotPriceInfo.priceNova > 0)
			{
				if (this.playerItems.Nova < (long)slotPriceInfo.priceNova)
				{
					upgradeResult = UpgradeResult.NotEnoughNova;
					return upgradeResult;
				}
				price = slotPriceInfo.priceNova;
				this.playerItems.AddNova((long)(-slotPriceInfo.priceNova));
				List<NovaUpdate> novaUpdates = this.playerItems.novaUpdates;
				NovaUpdate novaUpdate = new NovaUpdate()
				{
					amount = (long)(-slotPriceInfo.priceNova),
					text = "GAME: Expand Inventory with Nova",
					currencyType = SelectedCurrency.Nova,
					itemType = 0,
					serviceType = ServiceType.ExpandInventory
				};
				novaUpdates.Add(novaUpdate);
				PlayerBelongings playerBelonging1 = this;
				playerBelonging1.playerInventorySlots = (byte)(playerBelonging1.playerInventorySlots + 4);
			}
			else if (slotPriceInfo.priceEqulibrium > 0)
			{
				if (this.playerItems.GetAmountAt(PlayerItems.TypeEquilibrium) < (long)slotPriceInfo.priceEqulibrium)
				{
					upgradeResult = UpgradeResult.NotEnoughViral;
					return upgradeResult;
				}
				this.playerItems.Add(PlayerItems.TypeEquilibrium, (long)(-slotPriceInfo.priceEqulibrium));
				PlayerBelongings playerBelonging2 = this;
				playerBelonging2.playerInventorySlots = (byte)(playerBelonging2.playerInventorySlots + 4);
				List<NovaUpdate> novaUpdates1 = this.playerItems.novaUpdates;
				NovaUpdate novaUpdate1 = new NovaUpdate()
				{
					amount = (long)(-slotPriceInfo.priceEqulibrium),
					text = "GAME: Expand Inventory with Equilibrium",
					currencyType = SelectedCurrency.Equilibrium,
					itemType = 0,
					serviceType = ServiceType.ExpandInventory
				};
				novaUpdates1.Add(novaUpdate1);
			}
			upgradeResult = UpgradeResult.OK;
		}
		else
		{
			upgradeResult = UpgradeResult.InvalidParam;
		}
		return upgradeResult;
	}

	public bool ExpandShipSlotNew(ExtendedSlot slotType, SelectedCurrency currency, out int expandPrice)
	{
		bool flag;
		bool flag1;
		bool flag2;
		int priceOfShipSlotItem = this.GetPriceOfShipSlotItem(currency);
		expandPrice = priceOfShipSlotItem;
		if (!this.CanBuyNextExpandedSlotNew(currency))
		{
			flag2 = false;
		}
		else
		{
			flag2 = (slotType != ExtendedSlot.ExtraTwo ? true : this.haveExtendetExtraOne);
		}
		if (flag2)
		{
			if (!(currency != SelectedCurrency.Cash || priceOfShipSlotItem <= 0 ? true : this.playerItems.Cash < (long)priceOfShipSlotItem))
			{
				this.playerItems.AddCash((long)(-priceOfShipSlotItem));
			}
			else if (!(currency != SelectedCurrency.Nova || priceOfShipSlotItem <= 0 ? true : this.playerItems.Nova < (long)priceOfShipSlotItem))
			{
				this.playerItems.AddNova((long)(-priceOfShipSlotItem));
				List<NovaUpdate> novaUpdates = this.playerItems.novaUpdates;
				NovaUpdate novaUpdate = new NovaUpdate()
				{
					amount = (long)(-priceOfShipSlotItem),
					text = string.Format("GAME:BuyShipSlotExpand {0} with Nova", slotType),
					currencyType = SelectedCurrency.Nova,
					itemType = 0,
					serviceType = ServiceType.ExpandShipSlot
				};
				novaUpdates.Add(novaUpdate);
			}
			else if ((currency != SelectedCurrency.Equilibrium || priceOfShipSlotItem <= 0 ? false : this.playerItems.Equilibrium >= (long)priceOfShipSlotItem))
			{
				this.playerItems.AddEquilibrium((long)(-priceOfShipSlotItem));
				List<NovaUpdate> novaUpdates1 = this.playerItems.novaUpdates;
				NovaUpdate novaUpdate1 = new NovaUpdate()
				{
					amount = (long)(-priceOfShipSlotItem),
					text = string.Format("GAME:BuyShipSlotExpand {0} with Equilibrium", slotType),
					currencyType = SelectedCurrency.Equilibrium,
					itemType = 0,
					serviceType = ServiceType.ExpandShipSlot
				};
				novaUpdates1.Add(novaUpdate1);
			}
			if (slotType == ExtendedSlot.CorpusOne)
			{
				bool num = true;
                flag1 = (bool)num;
				this.haveExtendetCorpusOne = (bool)num;
				flag = flag1;
			}
			else if (slotType == ExtendedSlot.ShieldOne)
			{
                bool num1 = true;
                flag1 = (bool)num1;
				this.haveExtendetShielOne = (bool)num1;
				flag = flag1;
			}
			else if (slotType == ExtendedSlot.AnyOne)
			{
                bool num2 = true;
                flag1 = (bool)num2;
				this.haveExtendetAnyOne = (bool)num2;
				flag = flag1;
			}
			else if (slotType == ExtendedSlot.EngineOne)
			{
                bool num3 = true;
                flag1 = (bool)num3;
				this.haveExtendetEngineOne = (bool)num3;
				flag = flag1;
			}
			else if (slotType == ExtendedSlot.ExtraOne)
			{
                bool num4 = true;
                flag1 = (bool)num4;
				this.haveExtendetExtraOne = (bool)num4;
				flag = flag1;
			}
			else if (slotType != ExtendedSlot.ExtraTwo)
			{
				flag = false;
			}
			else
			{
                bool num5 = true;
				flag1 = num5;
				this.haveExtendetExtraTwo = (bool)num5;
				flag = flag1;
			}
		}
		else
		{
			flag = false;
		}
		return flag;
	}

	public UpgradeResult ExpandVault(out int price)
	{
		UpgradeResult upgradeResult;
		price = -1;
		if ((this.playerVaultSlots > 36 ? false : this.playerVaultSlots >= 12))
		{
			SlotPriceInfo slotPriceInfo = (
				from t in StaticData.slotPriceInformation
				where (t.slotId != this.playerVaultSlots + 4 ? false : t.slotType == "Vault")
				select t).First<SlotPriceInfo>();
			if (slotPriceInfo.priceCash > 0)
			{
				if (this.playerItems.Cash < (long)slotPriceInfo.priceCash)
				{
					upgradeResult = UpgradeResult.NotEnoughCash;
					return upgradeResult;
				}
				this.playerItems.AddCash((long)(-slotPriceInfo.priceCash));
				PlayerBelongings playerBelonging = this;
				playerBelonging.playerVaultSlots = (byte)(playerBelonging.playerVaultSlots + 4);
			}
			else if (slotPriceInfo.priceNova > 0)
			{
				if (this.playerItems.Nova < (long)slotPriceInfo.priceNova)
				{
					upgradeResult = UpgradeResult.NotEnoughNova;
					return upgradeResult;
				}
				price = slotPriceInfo.priceNova;
				this.playerItems.AddNova((long)(-slotPriceInfo.priceNova));
				PlayerBelongings playerBelonging1 = this;
				playerBelonging1.playerVaultSlots = (byte)(playerBelonging1.playerVaultSlots + 4);
				List<NovaUpdate> novaUpdates = this.playerItems.novaUpdates;
				NovaUpdate novaUpdate = new NovaUpdate()
				{
					amount = (long)(-slotPriceInfo.priceNova),
					text = "GAME: Expand Vault",
					currencyType = SelectedCurrency.Nova,
					itemType = 0,
					serviceType = ServiceType.ExpandVault
				};
				novaUpdates.Add(novaUpdate);
			}
			else if (slotPriceInfo.priceEqulibrium > 0)
			{
				if (this.playerItems.GetAmountAt(PlayerItems.TypeEquilibrium) < (long)slotPriceInfo.priceEqulibrium)
				{
					upgradeResult = UpgradeResult.NotEnoughViral;
					return upgradeResult;
				}
				this.playerItems.Add(PlayerItems.TypeEquilibrium, (long)(-slotPriceInfo.priceEqulibrium));
				PlayerBelongings playerBelonging2 = this;
				playerBelonging2.playerVaultSlots = (byte)(playerBelonging2.playerVaultSlots + 4);
				List<NovaUpdate> novaUpdates1 = this.playerItems.novaUpdates;
				NovaUpdate novaUpdate1 = new NovaUpdate()
				{
					amount = (long)(-slotPriceInfo.priceEqulibrium),
					text = "GAME: Expand Vault",
					currencyType = SelectedCurrency.Equilibrium,
					itemType = 0,
					serviceType = ServiceType.ExpandVault
				};
				novaUpdates1.Add(novaUpdate1);
			}
			upgradeResult = UpgradeResult.OK;
		}
		else
		{
			upgradeResult = UpgradeResult.InvalidParam;
		}
		return upgradeResult;
	}

	public SortedList<byte, short> FindAmmoSlots(int ammoType)
	{
		SortedList<byte, short> nums = new SortedList<byte, short>();
		SlotItem[] array = (
			from a in this.playerItems.slotItems
			where (a.ItemType != ammoType ? false : a.SlotType == 1)
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

	public SortedList<byte, short> FindStackPile(int itemType)
	{
		SortedList<byte, short> nums = new SortedList<byte, short>();
		SlotItem[] array = (
			from a in this.playerItems.slotItems
			where (a.ItemType != itemType ? false : a.SlotType == 1)
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

	public byte FirstFreeInventorySlot()
	{
		int i;
		byte num = 255;
		int num1 = this.playerInventorySlots;
		bool[] flagArray = new bool[num1];
		for (i = 0; i < num1; i++)
		{
			flagArray[i] = false;
		}
		SlotItem[] array = (
			from t in this.playerItems.slotItems
			where (t.SlotType != 1 ? false : t.Slot < num1)
			orderby t.Slot
			select t).ToArray<SlotItem>();
		SlotItem[] slotItemArray = array;
		for (int j = 0; j < (int)slotItemArray.Length; j++)
		{
			flagArray[slotItemArray[j].Slot] = true;
		}
		i = 0;
		while (i < num1)
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

	public byte FirstFreeVaultSlot()
	{
		int i;
		byte num = 255;
		bool[] flagArray = new bool[this.playerVaultSlots];
		for (i = 0; i < this.playerVaultSlots; i++)
		{
			flagArray[i] = false;
		}
		SlotItem[] array = (
			from t in this.playerItems.slotItems
			where t.SlotType == 2
			orderby t.Slot
			select t).ToArray<SlotItem>();
		SlotItem[] slotItemArray = array;
		for (int j = 0; j < (int)slotItemArray.Length; j++)
		{
			flagArray[slotItemArray[j].Slot] = true;
		}
		i = 0;
		while (i < this.playerVaultSlots)
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

	public SortedList<SelectedCurrency, int> GetExpandShipSlotSellInfo()
	{
		SortedList<SelectedCurrency, int> selectedCurrencies = new SortedList<SelectedCurrency, int>();
		switch (this.GetExtendetSlotsCount())
		{
			case 0:
			{
				selectedCurrencies.Add(SelectedCurrency.Cash, this.GetPriceOfShipSlotItem(SelectedCurrency.Cash));
				break;
			}
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			{
				selectedCurrencies.Add(SelectedCurrency.Nova, this.GetPriceOfShipSlotItem(SelectedCurrency.Nova));
				selectedCurrencies.Add(SelectedCurrency.Equilibrium, this.GetPriceOfShipSlotItem(SelectedCurrency.Equilibrium));
				break;
			}
		}
		return selectedCurrencies;
	}

	public int GetExtendetSlotsCount()
	{
		int num = 0;
		if (this.haveExtendetCorpusOne)
		{
			num++;
		}
		if (this.haveExtendetShielOne)
		{
			num++;
		}
		if (this.haveExtendetEngineOne)
		{
			num++;
		}
		if (this.haveExtendetAnyOne)
		{
			num++;
		}
		if (this.haveExtendetExtraOne)
		{
			num++;
		}
		if (this.haveExtendetExtraTwo)
		{
			num++;
		}
		return num;
	}

	public int GetNextExtendedSlotLevelRequirement()
	{
		int num = 51;
		switch (this.GetExtendetSlotsCount())
		{
			case 0:
			{
				num = 35;
				break;
			}
			case 1:
			{
				num = 38;
				break;
			}
			case 2:
			{
				num = 41;
				break;
			}
			case 3:
			{
				num = 44;
				break;
			}
			case 4:
			{
				num = 47;
				break;
			}
			case 5:
			{
				num = 50;
				break;
			}
		}
		return num;
	}

	public void GetNextExtendedSlotPrice(out SelectedCurrency currency, out int price)
	{
		currency = SelectedCurrency.Nova;
		price = 99999;
		switch (this.GetExtendetSlotsCount())
		{
			case 0:
			{
				currency = SelectedCurrency.Cash;
				price = 3000000;
				break;
			}
			case 1:
			{
				currency = SelectedCurrency.Nova;
				price = 10000;
				break;
			}
			case 2:
			{
				currency = SelectedCurrency.Nova;
				price = 15000;
				break;
			}
			case 3:
			{
				currency = SelectedCurrency.Nova;
				price = 22500;
				break;
			}
			case 4:
			{
				currency = SelectedCurrency.Nova;
				price = 35000;
				break;
			}
			case 5:
			{
				currency = SelectedCurrency.Nova;
				price = 50000;
				break;
			}
		}
	}

	public int GetPowerUpEffectValue(ushort powerUpType)
	{
		int num = 0;
		if (powerUpType == PlayerItems.TypePowerUpForLaserDamageFlat)
		{
			num = 5 * (int)Math.Floor((double)((float)this.playerLevel / 10f));
		}
		else if (powerUpType == PlayerItems.TypePowerUpForPlasmaDamageFlat)
		{
			num = 10 * (int)Math.Floor((double)((float)this.playerLevel / 10f));
		}
		else if (powerUpType == PlayerItems.TypePowerUpForIonDamageFlat)
		{
			num = 20 * (int)Math.Floor((double)((float)this.playerLevel / 10f));
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
			num = (int)(Math.Pow(2, Math.Floor((double)((float)this.playerLevel / 10f)) - 1) * 150);
		}
		else if (powerUpType == PlayerItems.TypePowerUpForCorpusPercentage)
		{
			num = 50;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForShieldFlat)
		{
			num = (int)(Math.Pow(2, Math.Floor((double)((float)this.playerLevel / 10f)) - 1) * 150);
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
			num = (int)Math.Floor((double)((float)this.playerLevel / 10f)) * 50;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForShieldPowerPercentage)
		{
			num = 15;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForTargetingFlat)
		{
			num = (int)Math.Floor((double)((float)this.playerLevel / 10f)) * 50;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForTargetingPercentage)
		{
			num = 30;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForAvoidanceFlat)
		{
			num = (int)Math.Floor((double)((float)this.playerLevel / 10f)) * 50;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForAvoidancePercentage)
		{
			num = 30;
		}
		return num;
	}

	public DateTime GetPowerUpExpireTime(ushort powerUpType)
	{
		DateTime minValue = DateTime.MinValue;
		if (powerUpType == PlayerItems.TypePowerUpForLaserDamageFlat)
		{
			minValue = this.laserDamageFlatExpireTime;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForPlasmaDamageFlat)
		{
			minValue = this.plasmaDamageFlatExpireTime;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForIonDamageFlat)
		{
			minValue = this.ionDamageFlatExpireTime;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForLaserDamagePercentage)
		{
			minValue = this.laserDamagePercentageExpireTime;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForPlasmaDamagePercentage)
		{
			minValue = this.plasmaDamagePercentageExpireTime;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForIonDamagePercentage)
		{
			minValue = this.ionDamagePercentageExpireTime;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForTotalDamagePercentage)
		{
			minValue = this.totalDamagePercentageExpireTime;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForCorpusFlat)
		{
			minValue = this.corpusFlatExpireTime;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForCorpusPercentage)
		{
			minValue = this.corpusPercentageExpireTime;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForShieldFlat)
		{
			minValue = this.shieldFlatExpireTime;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForShieldPercentage)
		{
			minValue = this.shieldPercentageExpireTime;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForEndurancePercentage)
		{
			minValue = this.endurancePercentageExpireTime;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForShieldPowerFlat)
		{
			minValue = this.shieldPowerFlatExpireTime;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForShieldPowerPercentage)
		{
			minValue = this.shieldPowerPercentageExpireTime;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForTargetingFlat)
		{
			minValue = this.targetingFlatExpireTime;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForTargetingPercentage)
		{
			minValue = this.targetingPercentageExpireTime;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForAvoidanceFlat)
		{
			minValue = this.avoidanceFlatExpireTime;
		}
		else if (powerUpType == PlayerItems.TypePowerUpForAvoidancePercentage)
		{
			minValue = this.avoidancePercentageExpireTime;
		}
		return minValue;
	}

	public int GetPriceOfShipSlotItem(SelectedCurrency currency)
	{
		int num = 0;
		switch (this.GetExtendetSlotsCount())
		{
			case 0:
			{
				if (currency == SelectedCurrency.Cash)
				{
					num = 3000000;
				}
				else if (currency == SelectedCurrency.Equilibrium)
				{
					num = 6000000;
				}
				break;
			}
			case 1:
			{
				if (currency == SelectedCurrency.Nova)
				{
					num = 10000;
				}
				else if (currency == SelectedCurrency.Equilibrium)
				{
					num = 20000;
				}
				break;
			}
			case 2:
			{
				if (currency == SelectedCurrency.Nova)
				{
					num = 15000;
				}
				else if (currency == SelectedCurrency.Equilibrium)
				{
					num = 30000;
				}
				break;
			}
			case 3:
			{
				if (currency == SelectedCurrency.Nova)
				{
					num = 22500;
				}
				else if (currency == SelectedCurrency.Equilibrium)
				{
					num = 45000;
				}
				break;
			}
			case 4:
			{
				if (currency == SelectedCurrency.Nova)
				{
					num = 35000;
				}
				else if (currency == SelectedCurrency.Equilibrium)
				{
					num = 70000;
				}
				break;
			}
			case 5:
			{
				if (currency == SelectedCurrency.Nova)
				{
					num = 50000;
				}
				else if (currency == SelectedCurrency.Equilibrium)
				{
					num = 100000;
				}
				break;
			}
		}
		return num;
	}

	public int GetWeaponDamage(SlotItemWeapon weapon, ShipConfiguration cfg)
	{
		float damageTotal = 0f;
		damageTotal = (float)weapon.DamageTotal;
		if (!(weapon.ItemType == PlayerItems.TypeWeaponLaserTire1 || weapon.ItemType == PlayerItems.TypeWeaponLaserTire2 || weapon.ItemType == PlayerItems.TypeWeaponLaserTire3 || weapon.ItemType == PlayerItems.TypeWeaponLaserTire4 ? false : weapon.ItemType != PlayerItems.TypeWeaponLaserTire5))
		{
			damageTotal = damageTotal * (1f + cfg.dmgPercentBonusForEachLaser / 100f) * (1f + cfg.dmgPercentForAllWeapon / 100f) + (float)cfg.dmgFlatBonusForEachLaser;
		}
		else if (!(weapon.ItemType == PlayerItems.TypeWeaponPlasmaTire1 || weapon.ItemType == PlayerItems.TypeWeaponPlasmaTire2 || weapon.ItemType == PlayerItems.TypeWeaponPlasmaTire3 || weapon.ItemType == PlayerItems.TypeWeaponPlasmaTire4 ? false : weapon.ItemType != PlayerItems.TypeWeaponPlasmaTire5))
		{
			damageTotal = damageTotal * (1f + cfg.dmgPercentBonusForEachPlasma / 100f) * (1f + cfg.dmgPercentForAllWeapon / 100f) + (float)cfg.dmgFlatBonusForEachPlasma;
		}
		else if ((weapon.ItemType == PlayerItems.TypeWeaponIonTire1 || weapon.ItemType == PlayerItems.TypeWeaponIonTire2 || weapon.ItemType == PlayerItems.TypeWeaponIonTire3 || weapon.ItemType == PlayerItems.TypeWeaponIonTire4 ? true : weapon.ItemType == PlayerItems.TypeWeaponIonTire5))
		{
			damageTotal = damageTotal * (1f + cfg.dmgPercentBonusForEachIon / 100f) * (1f + cfg.dmgPercentForAllWeapon / 100f) + (float)cfg.dmgFlatBonusForEachIon;
		}
		return (int)damageTotal;
	}

	public bool HaveAFreeSlot()
	{
		bool flag = (
			from t in this.playerItems.slotItems
			where t.SlotType == 1
			select t).Count<SlotItem>() < this.playerInventorySlots;
		return flag;
	}

	public bool HaveAFreeVaultSlot()
	{
		bool flag = (
			from t in this.playerItems.slotItems
			where t.SlotType == 2
			select t).Count<SlotItem>() < this.playerVaultSlots;
		return flag;
	}

	public bool HaveEnoughMoneyToBuy(int shipTypeId, SelectedCurrency currency)
	{
		bool cash = false;
		ShipsTypeNet shipsTypeNet = (
			from s in StaticData.shipTypes
			where s.id == shipTypeId
			select s).First<ShipsTypeNet>();
		switch (currency)
		{
			case SelectedCurrency.Cash:
			{
				cash = this.playerItems.Cash >= (long)shipsTypeNet.price;
				break;
			}
			case SelectedCurrency.Nova:
			{
				cash = (double)this.playerItems.Nova >= (double)shipsTypeNet.price * 0.9 / (Math.Ceiling(0.0016 * (double)(this.playerLevel * this.playerLevel) + (double)this.playerLevel) * 5);
				break;
			}
			case SelectedCurrency.Equilibrium:
			{
				Console.WriteLine("Implement viral price");
				break;
			}
		}
		return cash;
	}

	public bool IsBuyItemAllowed(ushort itemType, byte slotId, byte slotType, int qty, int shipId)
	{
		bool flag;
		int num;
		if (!PlayerItems.IsSlotable(itemType))
		{
			flag = true;
			return flag;
		}
		else if (PlayerItems.IsStackable(itemType))
		{
			flag = true;
		}
		else if ((
			from t in this.playerItems.slotItems
			where (t.Slot != slotId || t.SlotType != slotType ? false : t.ShipId == shipId)
			select t).FirstOrDefault<SlotItem>() == null)
		{
			if ((StaticData.allTypes[itemType].levelRestriction <= this.playerLevel ? true : slotType == 1))
			{
				goto Label1;
			}
			flag = true;
			return flag;
		}
		else
		{
			flag = false;
		}
		return flag;
	Label1:
		num = (this.haveExtendetCorpusOne ? 3 : 2);
		int num1 = num;
		int num2 = (this.haveExtendetShielOne ? 3 : 2);
		int num3 = (this.haveExtendetEngineOne ? 3 : 2);
		int num4 = (this.haveExtendetAnyOne ? 3 : 2);
		int num5 = (this.haveExtendetExtraOne ? 7 : 6);
		if ((!this.haveExtendetExtraOne ? false : this.haveExtendetExtraTwo))
		{
			num5 = 8;
		}
		if (!(slotType != 11 ? true : slotId < num1))
		{
			flag = false;
			return flag;
		}
		else if (!(slotType != 10 ? true : slotId < num2))
		{
			flag = false;
			return flag;
		}
		else if (!(slotType != 9 ? true : slotId < num4))
		{
			flag = false;
			return flag;
		}
		else if (!(slotType != 12 ? true : slotId < num3))
		{
			flag = false;
			return flag;
		}
		else if (!(slotType != 13 ? true : slotId < num5))
		{
			flag = false;
			return flag;
		}
		else if (!(slotType != 1 ? true : slotId < this.playerInventorySlots))
		{
			flag = false;
			return flag;
		}
		else if (!(slotType != 2 ? true : slotId < this.playerVaultSlots))
		{
			flag = false;
			return flag;
		}
		else if (!(slotType != 6 ? true : PlayerItems.IsWeaponLaser(itemType)))
		{
			flag = false;
			return flag;
		}
		else if (!(slotType != 7 ? true : PlayerItems.IsWeaponPlasma(itemType)))
		{
			flag = false;
			return flag;
		}
		else if ((slotType != 8 ? true : PlayerItems.IsWeaponIon(itemType)))
		{
			flag = true;
			return flag;
		}
		else
		{
			flag = false;
			return flag;
		}
	}

	public bool IsMoveItemAllowed(MoveSlotItemData data)
	{
		bool flag;
		SlotItem slotItem = (
			from si in this.playerItems.slotItems
			where (si.SlotType != data.srcSlotType || si.Slot != data.srcSlot ? false : si.ShipId == data.srcShipId)
			select si).FirstOrDefault<SlotItem>();
		if (slotItem == null)
		{
			flag = false;
		}
		else if ((StaticData.allTypes[slotItem.ItemType].levelRestriction <= this.playerLevel || data.dstSlotType == 1 || data.dstSlotType == 14 || data.dstSlotType == 2 ? false : data.dstSlotType != 15))
		{
			flag = false;
		}
		else
		{
			int num = (this.haveExtendetCorpusOne ? 3 : 2);
			int num1 = (this.haveExtendetShielOne ? 3 : 2);
			int num2 = (this.haveExtendetEngineOne ? 3 : 2);
			int num3 = (this.haveExtendetAnyOne ? 3 : 2);
			int num4 = (this.haveExtendetExtraOne ? 7 : 6);
			if ((!this.haveExtendetExtraOne ? false : this.haveExtendetExtraTwo))
			{
				num4 = 8;
			}
			if (!(data.dstSlotType != 11 ? true : data.dstSlot < num))
			{
				flag = false;
			}
			else if (!(data.dstSlotType != 10 ? true : data.dstSlot < num1))
			{
				flag = false;
			}
			else if (!(data.dstSlotType != 9 ? true : data.dstSlot < num3))
			{
				flag = false;
			}
			else if (!(data.dstSlotType != 12 ? true : data.dstSlot < num2))
			{
				flag = false;
			}
			else if (!(data.dstSlotType != 13 ? true : data.dstSlot < num4))
			{
				flag = false;
			}
			else if (!(data.dstSlotType != 1 ? true : data.dstSlot < this.playerInventorySlots))
			{
				flag = false;
			}
			else if (!(data.dstSlotType != 2 ? true : data.dstSlot < this.playerVaultSlots))
			{
				flag = false;
			}
			else if (!(data.dstSlotType != 6 ? true : PlayerItems.IsWeaponLaser(slotItem.ItemType)))
			{
				flag = false;
			}
			else if ((data.dstSlotType != 7 ? true : PlayerItems.IsWeaponPlasma(slotItem.ItemType)))
			{
				flag = ((data.dstSlotType != 8 ? true : PlayerItems.IsWeaponIon(slotItem.ItemType)) ? true : false);
			}
			else
			{
				flag = false;
			}
		}
		return flag;
	}

	public bool IsNpcVisited(short npcId)
	{
		return this.visitedNPCs.Contains(npcId);
	}

	public int MaxPortalPartForInvest(int portalId, ushort partType)
	{
		int num;
		int item = 0;
		Portal portal = (
			from p in StaticData.allPortals
			where p.portalId == portalId
			select p).FirstOrDefault<Portal>();
		PortalPart portalPart = (
			from p in this.playerItems.portalParts
			where (p.portalId != portalId ? false : p.partTypeId == partType)
			select p).FirstOrDefault<PortalPart>();
		if ((portal == null ? false : portal.parts.ContainsKey(partType)))
		{
			if (portalPart == null)
			{
				item = portal.parts[partType];
			}
			else
			{
				item = Math.Max(portal.parts[partType] - portalPart.partAmount, 0);
			}
			num = item;
		}
		else
		{
			Console.WriteLine("tmpPortal({1}) == null || !tmpPortal.parts.ContainsKey({0})", partType, portalId);
			num = item;
		}
		return num;
	}

	public bool MineMineral(Mineral mineral, long CargoCapacity, SortedList<ushort, int> collectedMinerals, List<SlotItem> collectedItems, ShipConfiguration _cfg)
	{
		bool flag = true;
		long cargoCapacity = CargoCapacity - this.playerItems.Cargo;
		foreach (KeyValuePair<ushort, int> resourceQuantity in mineral.resourceQuantities)
		{
			ushort key = resourceQuantity.Key;
			int value = resourceQuantity.Value;
			if (!PlayerItems.cargoTypes.Contains<ushort>(key))
			{
				if (key == PlayerItems.TypeNova)
				{
					this.playerItems.AddNova((long)value);
					List<NovaUpdate> novaUpdates = this.playerItems.novaUpdates;
					NovaUpdate novaUpdate = new NovaUpdate()
					{
						amount = (long)value,
						text = string.Format("GAME: Collect {0} Nova from LOOT", value),
						currencyType = SelectedCurrency.Nova,
						serviceType = ServiceType.FromLoot
					};
					novaUpdates.Add(novaUpdate);
				}
				else if (key != PlayerItems.TypeEquilibrium)
				{
					this.playerItems.Add(key, (long)value);
				}
				else
				{
					this.playerItems.AddEquilibrium((long)value);
					List<NovaUpdate> novaUpdates1 = this.playerItems.novaUpdates;
					NovaUpdate novaUpdate1 = new NovaUpdate()
					{
						amount = (long)value,
						text = string.Format("GAME: Collect {0} Equilibrium from LOOT", value),
						currencyType = SelectedCurrency.Equilibrium,
						serviceType = ServiceType.FromLoot
					};
					novaUpdates1.Add(novaUpdate1);
				}
				collectedMinerals.Add(key, value);
			}
			else
			{
				long num = Math.Min(cargoCapacity, (long)value);
				if (num < (long)value)
				{
				}
				cargoCapacity = cargoCapacity - num;
				value = value - (ushort)num;
				if (num > (long)0)
				{
					this.playerItems.AddMineral(key, (int)num);
					collectedMinerals.Add(key, (int)num);
				}
				if (cargoCapacity <= (long)0)
				{
					break;
				}
			}
		}
		if (mineral.items != null)
		{
			foreach (SlotItem item in mineral.items)
			{
				if (PlayerItems.IsStackable(item.ItemType))
				{
					int num1 = Math.Min(item.Amount, this.AllowedPileSize((int)item.ItemType));
					if (num1 > 0)
					{
						flag = true;
						if (this.AddStackableItem((int)item.ItemType, num1) == OparationResult.OK)
						{
							item.Amount = num1;
							collectedItems.Add(item);
						}
					}
				}
				else if (this.HaveAFreeSlot())
				{
					flag = true;
					if (!PlayerItems.IsWeapon(item.ItemType))
					{
						if ((PlayerItems.IsCorpus(item.ItemType) || PlayerItems.IsShield(item.ItemType) || PlayerItems.IsEngine(item.ItemType) ? false : !PlayerItems.IsExtra(item.ItemType)))
						{
							this.playerItems.AddSlotItem(item.ItemType, (long)item.Amount, (int)this.FirstFreeInventorySlot(), 1, 0);
						}
						else
						{
							this.playerItems.AddSlotItem(item, (int)this.FirstFreeInventorySlot(), 1, 0);
						}
						collectedItems.Add(item);
					}
					else
					{
						this.playerItems.AddSlotItemWeapon((SlotItemWeapon)item, (int)this.FirstFreeInventorySlot(), 1, 0);
						collectedItems.Add((SlotItemWeapon)item);
					}
				}
			}
		}
		return flag;
	}

	public void MovingOldStufToNewShip(int oldShipId, int newShipId)
	{
		List<SlotItem> list = (
			from si in this.playerItems.slotItems
			where si.ShipId == oldShipId
			select si).ToList<SlotItem>();
		PlayerShipNet playerShipNet = (
			from ps in this.playerShips
			where ps.ShipID == oldShipId
			select ps).FirstOrDefault<PlayerShipNet>();
		PlayerShipNet playerShipNet1 = (
			from ps in this.playerShips
			where ps.ShipID == newShipId
			select ps).FirstOrDefault<PlayerShipNet>();
		if ((playerShipNet == null ? false : playerShipNet1 != null))
		{
			WeaponSlotBluePrint weaponSlotBluePrint = (
				from bp in WeaponSlotBluePrint.allShipBluePrint
				where bp.shipType == playerShipNet1.ShipTitle
				select bp).First<WeaponSlotBluePrint>();
			foreach (SlotItem slotItem in list)
			{
				SlotItem slotItem1 = (
					from t in this.playerItems.slotItems
					where (t.ShipId != newShipId || t.Slot != slotItem.Slot ? false : t.SlotType == slotItem.SlotType)
					select t).FirstOrDefault<SlotItem>();
				bool flag = slotItem1 != null;
				if (!PlayerItems.IsWeapon(slotItem.ItemType))
				{
					MoveSlotItemData moveSlotItemDatum = new MoveSlotItemData()
					{
						srcSlotType = slotItem.SlotType,
						srcSlot = (byte)slotItem.Slot,
						srcShipId = slotItem.ShipId,
						dstShipId = newShipId,
						dstSlotType = slotItem.SlotType,
						dstSlot = (byte)slotItem.Slot,
						isSwap = flag
					};
					this.playerItems.MoveSlotItem(moveSlotItemDatum);
				}
				else
				{
					switch (slotItem.Slot)
					{
						case 0:
						{
							if (weaponSlotBluePrint.isSlot1Allowed)
							{
								MoveSlotItemData moveSlotItemDatum1 = new MoveSlotItemData()
								{
									srcSlotType = slotItem.SlotType,
									srcSlot = (byte)slotItem.Slot,
									srcShipId = slotItem.ShipId,
									dstShipId = newShipId,
									dstSlotType = slotItem.SlotType,
									dstSlot = (byte)slotItem.Slot,
									isSwap = flag
								};
								this.playerItems.MoveSlotItem(moveSlotItemDatum1);
							}
							break;
						}
						case 1:
						{
							if (weaponSlotBluePrint.isSlot2Allowed)
							{
								MoveSlotItemData moveSlotItemDatum2 = new MoveSlotItemData()
								{
									srcSlotType = slotItem.SlotType,
									srcSlot = (byte)slotItem.Slot,
									srcShipId = slotItem.ShipId,
									dstShipId = newShipId,
									dstSlotType = slotItem.SlotType,
									dstSlot = (byte)slotItem.Slot,
									isSwap = flag
								};
								this.playerItems.MoveSlotItem(moveSlotItemDatum2);
							}
							break;
						}
						case 2:
						{
							if (weaponSlotBluePrint.isSlot3Allowed)
							{
								MoveSlotItemData moveSlotItemDatum3 = new MoveSlotItemData()
								{
									srcSlotType = slotItem.SlotType,
									srcSlot = (byte)slotItem.Slot,
									srcShipId = slotItem.ShipId,
									dstShipId = newShipId,
									dstSlotType = slotItem.SlotType,
									dstSlot = (byte)slotItem.Slot,
									isSwap = flag
								};
								this.playerItems.MoveSlotItem(moveSlotItemDatum3);
							}
							break;
						}
						case 3:
						{
							if (weaponSlotBluePrint.isSlot4Allowed)
							{
								MoveSlotItemData moveSlotItemDatum4 = new MoveSlotItemData()
								{
									srcSlotType = slotItem.SlotType,
									srcSlot = (byte)slotItem.Slot,
									srcShipId = slotItem.ShipId,
									dstShipId = newShipId,
									dstSlotType = slotItem.SlotType,
									dstSlot = (byte)slotItem.Slot,
									isSwap = flag
								};
								this.playerItems.MoveSlotItem(moveSlotItemDatum4);
							}
							break;
						}
						case 4:
						{
							if (weaponSlotBluePrint.isSlot5Allowed)
							{
								MoveSlotItemData moveSlotItemDatum5 = new MoveSlotItemData()
								{
									srcSlotType = slotItem.SlotType,
									srcSlot = (byte)slotItem.Slot,
									srcShipId = slotItem.ShipId,
									dstShipId = newShipId,
									dstSlotType = slotItem.SlotType,
									dstSlot = (byte)slotItem.Slot,
									isSwap = flag
								};
								this.playerItems.MoveSlotItem(moveSlotItemDatum5);
							}
							break;
						}
						case 5:
						{
							if (weaponSlotBluePrint.isSlot6Allowed)
							{
								MoveSlotItemData moveSlotItemDatum6 = new MoveSlotItemData()
								{
									srcSlotType = slotItem.SlotType,
									srcSlot = (byte)slotItem.Slot,
									srcShipId = slotItem.ShipId,
									dstShipId = newShipId,
									dstSlotType = slotItem.SlotType,
									dstSlot = (byte)slotItem.Slot,
									isSwap = flag
								};
								this.playerItems.MoveSlotItem(moveSlotItemDatum6);
							}
							break;
						}
					}
				}
				playerShipNet.ApplyBonuses(this);
				playerShipNet1.ApplyBonuses(this);
			}
		}
		else
		{
			Console.WriteLine("MovingOldStufToNewShip -> oldShip == null || newShip == null ---> player:{0}[{1}]", this.playerName, this.playerLevel);
		}
	}

	public bool PayForGalaxyJump(int fromGalaxyId, int toGalaxyId, byte currency, out int jumpPrice)
	{
		bool flag;
		int num = 9999;
		GalaxiesJumpMap galaxiesJumpMap = (
			from t in StaticData.galaxyJumpsPrice
			where (t.destinationGalaxyId != toGalaxyId ? false : t.sourceGalaxyId == fromGalaxyId)
			select t).FirstOrDefault<GalaxiesJumpMap>();
		if (galaxiesJumpMap != null)
		{
			num = (currency != 1 ? galaxiesJumpMap.equilibriumPrice : galaxiesJumpMap.novaPrice);
		}
		jumpPrice = num;
		if (currency == 1)
		{
			if (this.playerItems.Nova >= (long)num)
			{
				this.playerItems.AddNova((long)(-1 * num));
				List<NovaUpdate> novaUpdates = this.playerItems.novaUpdates;
				NovaUpdate novaUpdate = new NovaUpdate()
				{
					amount = (long)(-1 * num),
					text = "GAME: Hyper jump with Nova",
					currencyType = SelectedCurrency.Nova,
					itemType = 0,
					serviceType = ServiceType.HyperJump
				};
				novaUpdates.Add(novaUpdate);
				flag = true;
			}
			else
			{
				flag = false;
			}
		}
		else if (currency != 2)
		{
			flag = false;
		}
		else if (this.playerItems.GetAmountAt(PlayerItems.TypeEquilibrium) >= (long)num)
		{
			this.playerItems.Add(PlayerItems.TypeEquilibrium, (long)(-num));
			List<NovaUpdate> novaUpdates1 = this.playerItems.novaUpdates;
			NovaUpdate novaUpdate1 = new NovaUpdate()
			{
				amount = (long)(-1 * num),
				text = "GAME: Hyper jump with Equilibrium",
				currencyType = SelectedCurrency.Equilibrium,
				itemType = 0,
				serviceType = ServiceType.HyperJump
			};
			novaUpdates1.Add(novaUpdate1);
			flag = true;
		}
		else
		{
			flag = false;
		}
		return flag;
	}

	public bool PayForNewShip(int newShipTypeId, SelectedCurrency currency, out int price)
	{
		bool flag;
		price = -1;
		ShipsTypeNet shipsTypeNet = (
			from s in StaticData.shipTypes
			where s.id == newShipTypeId
			select s).FirstOrDefault<ShipsTypeNet>();
		if (shipsTypeNet == null)
		{
			Console.WriteLine("Undefined ship Type");
			GameObjectPhysics.Log("Undefined ship Type");
			flag = false;
		}
		else if (this.HaveEnoughMoneyToBuy(newShipTypeId, currency))
		{
			switch (currency)
			{
				case SelectedCurrency.Cash:
				{
					if (shipsTypeNet.price > 0)
					{
						this.playerItems.AddCash((long)(-shipsTypeNet.price));
					}
					break;
				}
				case SelectedCurrency.Nova:
				{
					long num = (long)((double)shipsTypeNet.price * 0.9 / (Math.Ceiling(0.0016 * (double)(this.playerLevel * this.playerLevel) + (double)this.playerLevel) * 5));
					price = (int)num;
					if (num > (long)0)
					{
						this.playerItems.AddNova(-num);
					}
					List<NovaUpdate> novaUpdates = this.playerItems.novaUpdates;
					NovaUpdate novaUpdate = new NovaUpdate()
					{
						amount = -num,
						text = "GAME: Buy Ship",
						currencyType = SelectedCurrency.Nova,
						itemType = (ushort)newShipTypeId,
						serviceType = ServiceType.Ship
					};
					novaUpdates.Add(novaUpdate);
					break;
				}
				case SelectedCurrency.Equilibrium:
				{
					Console.WriteLine("Implement viral price");
					break;
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

	public questState QuestStatus(int questId)
	{
		questState _questState;
		if (!this.playerQuests.ContainsKey(questId))
		{
			_questState = questState.notAccepted;
		}
		else
		{
			PlayerQuest item = this.playerQuests[questId];
			if (!item.inProgress)
			{
				_questState = (!item.isRewordCollected ? questState.isCompleate : questState.isRewardTaken);
			}
			else
			{
				_questState = questState.inProgress;
			}
		}
		return _questState;
	}

	public void ResetFactionWarWeeklyProgress()
	{
		byte factionWarWeeklyChalangeParticipation;
		if (!this.weeklyRewardCollected)
		{
			factionWarWeeklyChalangeParticipation = (byte)this.FactionWarWeeklyChalangeParticipation;
		}
		else
		{
			factionWarWeeklyChalangeParticipation = 0;
		}
		this.lastWeekPendingReward = factionWarWeeklyChalangeParticipation;
		this.day1Participation = false;
		this.day2Participation = false;
		this.day3Participation = false;
		this.day4Participation = false;
		this.day5Participation = false;
		this.day6Participation = false;
		this.weeklyRewardCollected = false;
		this.councilRank = 0;
		this.councilSkillSelected = 0;
	}

	public void ResetSkillConfig()
	{
		if ((this.skillConfig == null ? false : this.playerItems != null))
		{
			ActiveSkillSlot[] array = this.skillConfig.skillSlots.Values.ToArray<ActiveSkillSlot>();
			for (int i = 0; i < (int)array.Length; i++)
			{
				ActiveSkillSlot activeSkillSlot = array[i];
				this.skillConfig.RemoveSkill((ushort)activeSkillSlot.skillId);
				this.playerItems.Set((ushort)activeSkillSlot.skillId, (long)0);
			}
		}
	}

	public void RetrainSkillTree(SkillType skillTree)
	{
		ushort num = 0;
		if (this.playerItems.Nova >= (long)PlayerObjectPhysics.TALENT_RETRAIN_PRICE)
		{
			long amountAt = (long)0;
			switch (skillTree)
			{
				case SkillType.Guardian:
				{
					foreach (ushort n in PlayerItems.abilityTypesGuardian)
					{
						amountAt = amountAt + this.playerItems.GetAmountAt(n);
						this.playerItems.Set(num, (long)0);
						this.skillConfig.RemoveSkill(n);
					}
					break;
				}
				case SkillType.Destroyer:
				{
					foreach (ushort num1 in PlayerItems.abilityTypesDestroyer)
					{
						amountAt = amountAt + this.playerItems.GetAmountAt(num1);
						this.playerItems.Set(num1, (long)0);
						this.skillConfig.RemoveSkill(num1);
					}
					break;
				}
				case SkillType.Protector:
				{
					foreach (ushort num2 in PlayerItems.abilityTypesProtector)
					{
						amountAt = amountAt + this.playerItems.GetAmountAt(num2);
						this.playerItems.Set(num2, (long)0);
						this.skillConfig.RemoveSkill(num2);
					}
					break;
				}
				case SkillType.Passive:
				{
					foreach (ushort num3 in PlayerItems.abilityTypesPassive)
					{
						amountAt = amountAt + this.playerItems.GetAmountAt(num3);
						this.playerItems.Set(num3, (long)0);
						this.skillConfig.RemoveSkill(num3);
					}
					break;
				}
				case SkillType.Amplification:
				{
					foreach (ushort num4 in PlayerItems.abilityTypesAmplification)
					{
						amountAt = amountAt + this.playerItems.GetAmountAt(num4);
						this.playerItems.Set(num4, (long)0);
						this.skillConfig.RemoveSkill(num4);
					}
					break;
				}
			}
			this.playerItems.AddNova((long)(-PlayerObjectPhysics.TALENT_RETRAIN_PRICE));
			List<NovaUpdate> novaUpdates = this.playerItems.novaUpdates;
			NovaUpdate novaUpdate = new NovaUpdate()
			{
				amount = (long)(-PlayerObjectPhysics.TALENT_RETRAIN_PRICE),
				text = "GAME: Skill Reset",
				currencyType = SelectedCurrency.Nova,
				itemType = 0,
				serviceType = ServiceType.SkillsReset
			};
			novaUpdates.Add(novaUpdate);
			this.playerItems.Add(PlayerItems.TypeTalentPoint, amountAt);
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		int i;
		TimeSpan timeSpan;
		bw.Write(this.guildInvitesCount);
		bw.Write(this.isAutoFireOn);
		bw.Write(this.isAutoMiningOn);
		bw.Write(this.isShowMoreDetailsOn);
		bw.Write(this.zoomLevel);
		bw.Write(this.playerAccessLevel);
		bw.Write(this.cuLevel);
		bw.Write(this.socialOptionsStatus);
		bw.Write(this.playerName ?? "");
		bw.Write(this.playerTalentClass);
		bw.Write(this.lastPurchaseId);
		bw.Write(this.selectedShipId);
		bw.Write((int)this.playerShips.Length);
		for (i = 0; i < (int)this.playerShips.Length; i++)
		{
			this.playerShips[i].Serialize(bw);
		}
		this.playerObjectives.Serialize(bw);
		this.playerItems.Serialize(bw);
		this.skillConfig.Serialize(bw);
		bw.Write(this.playerInventorySlots);
		bw.Write(this.playerVaultSlots);
		bw.Write(this.playerLevel);
		if (!(this.penaltyTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.penaltyTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		bw.Write(this.penaltyDuration);
		bw.Write(this.firstWinBonusRecived);
		if (!(this.nextPvPRoundTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.nextPvPRoundTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		if (!(this.oldestPvPGameStartTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.oldestPvPGameStartTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		bw.Write(this.pvpGamePoolCapacity);
		if (!(this.signingPvPGameTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.signingPvPGameTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		bw.Write(this.visitedNPCs.Count);
		foreach (short visitedNPC in this.visitedNPCs)
		{
			bw.Write(visitedNPC);
		}
		if (this.playerQuests != null)
		{
			bw.Write(this.playerQuests.Count);
			foreach (int d in this.playerQuests.Keys)
			{
				bw.Write(d);
				this.playerQuests[d].Serialize(bw);
			}
		}
		else
		{
			bw.Write(-1);
		}
		bw.Write(this.haveExtendetCorpusOne);
		bw.Write(this.haveExtendetShielOne);
		bw.Write(this.haveExtendetEngineOne);
		bw.Write(this.haveExtendetAnyOne);
		bw.Write(this.haveExtendetExtraOne);
		bw.Write(this.haveExtendetExtraTwo);
		bw.Write(this.unlockedPortals.Count);
		for (i = 0; i < this.unlockedPortals.Count; i++)
		{
			bw.Write(this.unlockedPortals[i]);
		}
		bw.Write(this.nextFreeTransformerUsage - StaticData.now.Ticks);
		bw.Write(this.transformerState);
		bw.Write(this.dailyMissionsDone);
		bw.Write(this.isDailyMissionsRewardCollected);
		this.referals.Serialize(bw);
		bw.Write(this.receivedDailyRewards);
		if (!(this.boostAutoMinerExpireTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.boostAutoMinerExpireTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		if (!(this.boostCargoExpireTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.boostCargoExpireTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		if (!(this.boostDamageExpireTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.boostDamageExpireTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		if (!(this.boostExperienceExpireTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.boostExperienceExpireTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		bw.Write(this.playerKeyboardShortcuts.Count);
		foreach (KeyboardShortcutPair playerKeyboardShortcut in this.playerKeyboardShortcuts)
		{
			playerKeyboardShortcut.Serialize(bw);
		}
		if (!(this.laserDamageFlatExpireTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.laserDamageFlatExpireTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		if (!(this.plasmaDamageFlatExpireTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.plasmaDamageFlatExpireTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		if (!(this.ionDamageFlatExpireTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.ionDamageFlatExpireTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		if (!(this.laserDamagePercentageExpireTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.laserDamagePercentageExpireTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		if (!(this.plasmaDamagePercentageExpireTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.plasmaDamagePercentageExpireTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		if (!(this.ionDamagePercentageExpireTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.ionDamagePercentageExpireTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		if (!(this.totalDamagePercentageExpireTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.totalDamagePercentageExpireTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		if (!(this.corpusFlatExpireTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.corpusFlatExpireTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		if (!(this.corpusPercentageExpireTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.corpusPercentageExpireTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		if (!(this.shieldFlatExpireTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.shieldFlatExpireTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		if (!(this.shieldPercentageExpireTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.shieldPercentageExpireTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		if (!(this.endurancePercentageExpireTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.endurancePercentageExpireTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		if (!(this.shieldPowerFlatExpireTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.shieldPowerFlatExpireTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		if (!(this.shieldPowerPercentageExpireTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.shieldPowerPercentageExpireTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		if (!(this.targetingFlatExpireTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.targetingFlatExpireTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		if (!(this.targetingPercentageExpireTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.targetingPercentageExpireTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		if (!(this.avoidanceFlatExpireTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.avoidanceFlatExpireTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		if (!(this.avoidancePercentageExpireTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.avoidancePercentageExpireTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		if (!(this.nextFreeGiftTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.nextFreeGiftTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		if (!(this.factionWarDayEnd != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			timeSpan = this.factionWarDayEnd - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
		bw.Write(this.factionWarRerollBonus);
		bw.Write(this.factionWarXpBonus);
		bw.Write((byte)this.factionWarDay);
		bw.Write(this.isWarInProgress);
		bw.Write(this.councilRank);
		bw.Write(this.myBattleBoostVote);
		bw.Write(this.myUtilityBoostVote);
		bw.Write(this.councilSkillSelected);
		bw.Write(this.day1Participation);
		bw.Write(this.day2Participation);
		bw.Write(this.day3Participation);
		bw.Write(this.day4Participation);
		bw.Write(this.day5Participation);
		bw.Write(this.day6Participation);
		bw.Write(this.factionWarDayScore);
		bw.Write(this.rewardForDayProgressCollected1);
		bw.Write(this.rewardForDayProgressCollected2);
		bw.Write(this.rewardForDayProgressCollected3);
		bw.Write(this.factionWarBattleBoost);
		bw.Write(this.factionWarUtilityBoost);
		bw.Write(this.weeklyRewardCollected);
		bw.Write(this.lastWeekPendingReward);
		bw.Write(this.factionGalaxyXpGain);
		bw.Write(this.factionGalaxySellBonus);
		bw.Write(this.warCommendationsBought);
	}

	public void SetNextFreeGiftTime()
	{
		this.nextFreeGiftTime = StaticData.now.AddHours((double)PlayerItems.FREE_GIFT_SEND_INTERVAL);
	}

	public void SetNextFreeTransformUsage()
	{
		DateTime dateTime = StaticData.now.AddHours((double)PlayerItems.FREE_TRANSFORM_USAGE_INTERVAL);
		this.nextFreeTransformerUsage = dateTime.Ticks;
	}

	public void SetRealWeaponCooldown(ShipConfiguration cfg)
	{
		for (int i = 0; i < 6; i++)
		{
			if ((cfg.weaponSlots[i].weaponTierId == PlayerItems.TypeWeaponLaserTire1 || cfg.weaponSlots[i].weaponTierId == PlayerItems.TypeWeaponLaserTire2 || cfg.weaponSlots[i].weaponTierId == PlayerItems.TypeWeaponLaserTire3 || cfg.weaponSlots[i].weaponTierId == PlayerItems.TypeWeaponLaserTire4 ? true : cfg.weaponSlots[i].weaponTierId == PlayerItems.TypeWeaponLaserTire5))
			{
				WeaponSlot weaponSlot = cfg.weaponSlots[i];
				weaponSlot.realReloadTime = weaponSlot.realReloadTime + (long)cfg.laserCooldown * (long)-10000;
			}
			if ((cfg.weaponSlots[i].weaponTierId == PlayerItems.TypeWeaponPlasmaTire1 || cfg.weaponSlots[i].weaponTierId == PlayerItems.TypeWeaponPlasmaTire2 || cfg.weaponSlots[i].weaponTierId == PlayerItems.TypeWeaponPlasmaTire3 || cfg.weaponSlots[i].weaponTierId == PlayerItems.TypeWeaponPlasmaTire4 ? true : cfg.weaponSlots[i].weaponTierId == PlayerItems.TypeWeaponPlasmaTire5))
			{
				WeaponSlot weaponSlot1 = cfg.weaponSlots[i];
				weaponSlot1.realReloadTime = weaponSlot1.realReloadTime + (long)cfg.plasmaCooldown * (long)-10000;
			}
			if ((cfg.weaponSlots[i].weaponTierId == PlayerItems.TypeWeaponIonTire1 || cfg.weaponSlots[i].weaponTierId == PlayerItems.TypeWeaponIonTire2 || cfg.weaponSlots[i].weaponTierId == PlayerItems.TypeWeaponIonTire3 || cfg.weaponSlots[i].weaponTierId == PlayerItems.TypeWeaponIonTire4 ? true : cfg.weaponSlots[i].weaponTierId == PlayerItems.TypeWeaponIonTire5))
			{
				WeaponSlot weaponSlot2 = cfg.weaponSlots[i];
				weaponSlot2.realReloadTime = weaponSlot2.realReloadTime + (long)cfg.ionCooldown * (long)-10000;
			}
		}
	}

	public void ToogleAutoFire(bool isOn)
	{
		if (this.isAutoFireOn != isOn)
		{
			this.isAutoFireOn = isOn;
		}
	}

	public void ToogleShowMoreDetails(bool isOn)
	{
		if (this.isShowMoreDetailsOn != isOn)
		{
			this.isShowMoreDetailsOn = isOn;
		}
	}

	public void ToogleStoryQuestTracker(bool isOff)
	{
		if (!isOff)
		{
			this.playerItems.Set(PlayerItems.IsStoryQuestTrackerOff, (long)0);
		}
		else
		{
			this.playerItems.Set(PlayerItems.IsStoryQuestTrackerOff, (long)1);
		}
	}

	public questState TutorialQuestStatus(int questId)
	{
		questState _questState;
		if (!this.playerQuests.ContainsKey(questId))
		{
			_questState = questState.notAccepted;
		}
		else
		{
			PlayerQuest item = this.playerQuests[questId];
			if (!item.inProgress)
			{
				_questState = (!item.isRewordCollected ? questState.isCompleate : questState.isRewardTaken);
			}
			else
			{
				_questState = questState.inProgress;
			}
		}
		return _questState;
	}

	public void UpgradeShip(int shipId, int targetShipTypeId, int price)
	{
		ShipsTypeNet shipsTypeNet = (
			from s in StaticData.shipTypes
			where s.id == targetShipTypeId
			select s).First<ShipsTypeNet>();
		this.playerItems.AddCash((long)(-price));
		PlayerShipNet shield = (
			from ps in this.playerShips
			where ps.ShipID == shipId
			select ps).First<PlayerShipNet>();
		shield.shipTypeId = (byte)targetShipTypeId;
		shield.Shield = (short)shipsTypeNet.shield;
		shield.Corpus = (short)shipsTypeNet.corpus;
		shield.ApplyBonuses(this);
		shield.ShieldHP = shield.Shield;
		shield.CorpusHP = shield.Corpus;
	}

	public void VisitNPC(short npcId)
	{
		if (!this.IsNpcVisited(npcId))
		{
			this.visitedNPCs.Add(npcId);
		}
	}
}