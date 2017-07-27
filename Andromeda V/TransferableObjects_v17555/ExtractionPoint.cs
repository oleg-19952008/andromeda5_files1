using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

public class ExtractionPoint : GameObjectPhysics, ITransferableInContext, ITransferable
{
	public const float BATTLE_CONTRIBUTION_POINTS_ON_CAPTURE = 1000f;

	public const float BATTLE_CONTRIBUTION_CAP = 2000f;

	public const float INVULNERABE_MODE_DURATION_IN_MINUTES = 114f;

	public const float VULNERABE_MODE_DURATION_IN_MINUTES = 30f;

	public const float INCOME_INTERVAL_IN_MINUTES = 60f;

	public const float DAMAGE_REDUCTION_BOOST_VALUE = 50f;

	public const int DEFENDING_UPDATE_INTERVAL_IN_SECONDS = 10;

	public const int DEFENDING_MINIMUM_TIME_FOR_REWARD_IN_SECONDS = 300;

	public const int DEFENDING_REWARD_AMOUNT = 5;

	public const int RESEARCH_POINT_GAIN = 4;

	public const int GUARDIAN_SKILLS_ACCESS_MINIMUM_CONTRIBUTION = 5000;

	public const int GUARDIAN_SKILLS_MAX_POINTS = 60;

	public const int GUARDIAN_SKILLS_RESET_PRICE = 200;

	public const int DAMAGE_REDUCTION_BOOST_PRICE = 200;

	public const int DAMAGE_REDUCTION_BOOST_DURATION_IN_MINUTES = 10;

	public const int BATTLE_CONTRIBUTION_FOR_A_MINUTE_DEFENDING = 5;

	public const int BONUS_INCOME_STEP_ONE_TARGET = 12000;

	public const int BONUS_INCOME_STEP_TWO_TARGET = 24000;

	public const int BONUS_INCOME_STEP_ONE_VALUE = 50;

	public const int BONUS_INCOME_STEP_TWO_VALUE = 100;

	public const int AVAILABLE_POPULATION_IN_VULNERABLE_MODE = 100;

	public const int INVEST_COOLDOWN_IN_SECONDS = 3;

	public short pointId;

	public float health;

	public int regenePerSec;

	public byte ownerFraction;

	private bool isDamaged;

	public short currentPopulationAliens;

	public short currentPopulationTowers;

	public short freeResearchPoints;

	public int hitPoints;

	public float incomeGeneration;

	public byte bonusIncome;

	public int tottalContribution;

	public byte magicCoefficient;

	public int populationAliens;

	public int populationTowers;

	public byte availablePopulationAliens;

	public byte availablePopulationTowers;

	public string name;

	public byte upgradesHitPoints;

	public byte upgradesUltralibrium;

	public byte upgradesPopulation;

	public byte upgradesBarracks;

	public byte upgradesAlien1;

	public byte upgradesAlien2;

	public byte upgradesAlien3;

	public byte upgradesAlien4;

	public byte upgradesAlien5;

	public byte upgradesTurret1;

	public byte upgradesTurret2;

	public byte upgradesTurret3;

	public byte upgradesTurret4;

	public byte upgradesTurret5;

	private static int RANGE_OF_ACTION;

	public ExtractionPointState state;

	public List<Contributor> topTenContributors;

	public SortedList<int, BattleContributor> temporaryContributors = new SortedList<int, BattleContributor>();

	public SortedList<int, int> defendersCountByKind = new SortedList<int, int>()
	{
		{ 51, 0 },
		{ 52, 0 },
		{ 53, 0 },
		{ 54, 0 },
		{ 55, 0 },
		{ 101, 0 },
		{ 102, 0 },
		{ 103, 0 },
		{ 104, 0 },
		{ 105, 0 }
	};

	public bool isVulnerable;

	public DateTime establishingControlTime = DateTime.MinValue;

	public DateTime vulnerableEndTime = DateTime.MinValue;

	public DateTime invulnerableModeEndTime = DateTime.MinValue;

	public DateTime damageReductionBoostEndTime = DateTime.MinValue;

	public SortedList<byte, ExtractionPoinGuardSkills> guardianSkills = new SortedList<byte, ExtractionPoinGuardSkills>();

	static ExtractionPoint()
	{
		ExtractionPoint.RANGE_OF_ACTION = 40;
	}

	public ExtractionPoint()
	{
	}

	public void ActivateDamageReductionBoost()
	{
		if (!(this.damageReductionBoostEndTime < StaticData.now))
		{
			this.damageReductionBoostEndTime = this.damageReductionBoostEndTime.AddMinutes(10);
		}
		else
		{
			this.damageReductionBoostEndTime = StaticData.now.AddMinutes(10);
		}
	}

	public void AddBatleContribution(int playerId, byte playerFraction, float damageAmount)
	{
		if (playerId != 0)
		{
			if (playerFraction != this.ownerFraction)
			{
				if (!this.temporaryContributors.ContainsKey(playerId))
				{
					SortedList<int, BattleContributor> nums = this.temporaryContributors;
					BattleContributor battleContributor = new BattleContributor()
					{
						playerId = playerId,
						damages = damageAmount
					};
					nums.Add(playerId, battleContributor);
				}
				else
				{
					this.temporaryContributors[playerId].damages += damageAmount;
				}
			}
		}
	}

	public float CalculateDamageIncome(float damageAmount)
	{
		float single = 0f;
		if (this.damageReductionBoostEndTime > StaticData.now)
		{
			single += 50f;
		}
		single += (float)this.currentPopulationTowers;
		single = Mathf.Min(single, 100f);
		return damageAmount * (100f - single) / 100f;
	}

	public override void CalculateObjectMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		this.RegenerateHitPoints(dt);
		this.isDamaged = this.health != (float)this.hitPoints;
	}

	public void CopyPropsTo(ExtractionPoint copyTarget)
	{
		base.CopyPropsTo(copyTarget);
		copyTarget.pointId = this.pointId;
		copyTarget.health = this.health;
		copyTarget.regenePerSec = this.regenePerSec;
		copyTarget.ownerFraction = this.ownerFraction;
		copyTarget.state = this.state;
		copyTarget.name = this.name;
		copyTarget.currentPopulationAliens = this.currentPopulationAliens;
		copyTarget.currentPopulationTowers = this.currentPopulationTowers;
		copyTarget.freeResearchPoints = this.freeResearchPoints;
		copyTarget.magicCoefficient = this.magicCoefficient;
		copyTarget.hitPoints = this.hitPoints;
		copyTarget.bonusIncome = this.bonusIncome;
		copyTarget.tottalContribution = this.tottalContribution;
		copyTarget.incomeGeneration = this.incomeGeneration;
		copyTarget.populationAliens = this.populationAliens;
		copyTarget.populationTowers = this.populationTowers;
		copyTarget.availablePopulationAliens = this.availablePopulationAliens;
		copyTarget.availablePopulationTowers = this.availablePopulationTowers;
		copyTarget.upgradesHitPoints = this.upgradesHitPoints;
		copyTarget.upgradesUltralibrium = this.upgradesUltralibrium;
		copyTarget.upgradesPopulation = this.upgradesPopulation;
		copyTarget.upgradesBarracks = this.upgradesBarracks;
		copyTarget.upgradesAlien1 = this.upgradesAlien1;
		copyTarget.upgradesAlien2 = this.upgradesAlien2;
		copyTarget.upgradesAlien3 = this.upgradesAlien3;
		copyTarget.upgradesAlien4 = this.upgradesAlien4;
		copyTarget.upgradesAlien5 = this.upgradesAlien5;
		copyTarget.upgradesTurret1 = this.upgradesTurret1;
		copyTarget.upgradesTurret2 = this.upgradesTurret2;
		copyTarget.upgradesTurret3 = this.upgradesTurret3;
		copyTarget.upgradesTurret4 = this.upgradesTurret4;
		copyTarget.upgradesTurret5 = this.upgradesTurret5;
		copyTarget.establishingControlTime = this.establishingControlTime;
		copyTarget.vulnerableEndTime = this.vulnerableEndTime;
		copyTarget.invulnerableModeEndTime = this.invulnerableModeEndTime;
		copyTarget.damageReductionBoostEndTime = this.damageReductionBoostEndTime;
		copyTarget.defendersCountByKind = this.defendersCountByKind;
		copyTarget.guardianSkills = this.guardianSkills;
	}

	public void DealDamage(int playerId, byte playerFraction, float damageAmount)
	{
		this.health -= damageAmount;
		this.AddBatleContribution(playerId, playerFraction, damageAmount);
	}

	public override void Deserialize(BinaryReader br)
	{
		int i;
		DateTime now;
		this.pointId = br.ReadInt16();
		this.health = br.ReadSingle();
		this.regenePerSec = br.ReadInt32();
		this.ownerFraction = br.ReadByte();
		this.state = (ExtractionPointState)br.ReadByte();
		this.name = br.ReadString();
		this.currentPopulationAliens = br.ReadInt16();
		this.currentPopulationTowers = br.ReadInt16();
		this.freeResearchPoints = br.ReadInt16();
		this.magicCoefficient = br.ReadByte();
		this.hitPoints = br.ReadInt32();
		this.incomeGeneration = br.ReadSingle();
		this.populationAliens = br.ReadInt32();
		this.populationTowers = br.ReadInt32();
		this.availablePopulationAliens = br.ReadByte();
		this.availablePopulationTowers = br.ReadByte();
		this.bonusIncome = br.ReadByte();
		this.tottalContribution = br.ReadInt32();
		this.upgradesHitPoints = br.ReadByte();
		this.upgradesUltralibrium = br.ReadByte();
		this.upgradesPopulation = br.ReadByte();
		this.upgradesBarracks = br.ReadByte();
		this.upgradesAlien1 = br.ReadByte();
		this.upgradesAlien2 = br.ReadByte();
		this.upgradesAlien3 = br.ReadByte();
		this.upgradesAlien4 = br.ReadByte();
		this.upgradesAlien5 = br.ReadByte();
		this.upgradesTurret1 = br.ReadByte();
		this.upgradesTurret2 = br.ReadByte();
		this.upgradesTurret3 = br.ReadByte();
		this.upgradesTurret4 = br.ReadByte();
		this.upgradesTurret5 = br.ReadByte();
		long num = br.ReadInt64();
		if (num != (long)0)
		{
			now = DateTime.Now;
			this.establishingControlTime = now.AddMilliseconds((double)(-num));
		}
		else
		{
			this.establishingControlTime = DateTime.MinValue;
		}
		num = br.ReadInt64();
		if (num != (long)0)
		{
			now = DateTime.Now;
			this.vulnerableEndTime = now.AddMilliseconds((double)(-num));
		}
		else
		{
			this.vulnerableEndTime = DateTime.MinValue;
		}
		num = br.ReadInt64();
		if (num != (long)0)
		{
			now = DateTime.Now;
			this.invulnerableModeEndTime = now.AddMilliseconds((double)(-num));
		}
		else
		{
			this.invulnerableModeEndTime = DateTime.MinValue;
		}
		num = br.ReadInt64();
		if (num != (long)0)
		{
			now = DateTime.Now;
			this.damageReductionBoostEndTime = now.AddMilliseconds((double)(-num));
		}
		else
		{
			this.damageReductionBoostEndTime = DateTime.MinValue;
		}
		int num1 = br.ReadInt32();
		for (i = 0; i < num1; i++)
		{
			int num2 = br.ReadInt32();
			int num3 = br.ReadInt32();
			if (!this.defendersCountByKind.ContainsKey(num2))
			{
				this.defendersCountByKind.Add(num2, num3);
			}
			else
			{
				this.defendersCountByKind[num2] = num3;
			}
		}
		int num4 = br.ReadInt32();
		for (i = 0; i < num4; i++)
		{
			ExtractionPoinGuardSkills extractionPoinGuardSkill = new ExtractionPoinGuardSkills()
			{
				pointId = this.pointId,
				unitType = br.ReadByte()
			};
			int num5 = br.ReadInt32();
			for (int j = 0; j < num5; j++)
			{
				short num6 = br.ReadInt16();
				byte num7 = br.ReadByte();
				extractionPoinGuardSkill.guardianSkills.Add(num6, num7);
			}
			this.guardianSkills.Add(extractionPoinGuardSkill.unitType, extractionPoinGuardSkill);
		}
		base.Deserialize(br);
	}

	public void DeserializeInContext(BinaryReader br, TransferContext context)
	{
		int num;
		int i;
		Contributor contributor;
		TransferContext transferContext = context;
		if (transferContext == TransferContext.EpContributors)
		{
			num = br.ReadInt32();
			this.topTenContributors = new List<Contributor>();
			for (i = 0; i < num; i++)
			{
				contributor = new Contributor()
				{
					battleContribution = br.ReadInt32(),
					fractionId = br.ReadByte(),
					novaContribution = br.ReadInt32(),
					point_id = br.ReadInt32(),
					viralContribution = br.ReadInt32(),
					tottalContribution = br.ReadInt32(),
					incomeBonus = br.ReadInt32(),
					displayName = br.ReadString()
				};
				this.topTenContributors.Add(contributor);
			}
		}
		else if (transferContext == TransferContext.GameMapOverview)
		{
			this.pointId = br.ReadInt16();
			this.x = br.ReadSingle();
			this.z = br.ReadSingle();
			this.state = (ExtractionPointState)br.ReadByte();
			this.ownerFraction = br.ReadByte();
			num = br.ReadInt32();
			if (num != -1)
			{
				this.topTenContributors = new List<Contributor>();
				for (i = 0; i < num; i++)
				{
					contributor = new Contributor()
					{
						tottalContribution = br.ReadInt32(),
						incomeBonus = br.ReadInt32(),
						displayName = br.ReadString()
					};
					this.topTenContributors.Add(contributor);
				}
			}
		}
	}

	public void Heal(float value)
	{
		this.health += value;
		if (this.health >= (float)this.hitPoints)
		{
			this.health = (float)this.hitPoints;
		}
	}

	public static bool IsGuardianAlien(byte defenderType)
	{
		return (defenderType == 51 || defenderType == 52 || defenderType == 53 || defenderType == 54 ? true : defenderType == 55);
	}

	public static bool IsGuardianTower(byte defenderType)
	{
		return (defenderType == 101 || defenderType == 102 || defenderType == 103 || defenderType == 104 ? true : defenderType == 105);
	}

	public bool IsObjectInRange(GameObjectPhysics target)
	{
		bool distance = GameObjectPhysics.GetDistance(this.x, target.x, this.z, target.z) < (float)ExtractionPoint.RANGE_OF_ACTION;
		return distance;
	}

	private void RegenerateHitPoints(float dt)
	{
		if (this.health < 0f)
		{
			this.health = 0f;
		}
		if ((float)this.hitPoints > this.health)
		{
			ExtractionPoint extractionPoint = this;
			extractionPoint.health = extractionPoint.health + dt * (float)this.regenePerSec;
			if (this.health > (float)this.hitPoints)
			{
				this.health = (float)this.hitPoints;
			}
		}
	}

	public override void Serialize(BinaryWriter bw)
	{
		TimeSpan now;
		bw.Write(this.pointId);
		bw.Write(this.health);
		bw.Write(this.regenePerSec);
		bw.Write(this.ownerFraction);
		bw.Write((byte)this.state);
		bw.Write(this.name ?? "");
		bw.Write(this.currentPopulationAliens);
		bw.Write(this.currentPopulationTowers);
		bw.Write(this.freeResearchPoints);
		bw.Write(this.magicCoefficient);
		bw.Write(this.hitPoints);
		bw.Write(this.incomeGeneration);
		bw.Write(this.populationAliens);
		bw.Write(this.populationTowers);
		bw.Write(this.availablePopulationAliens);
		bw.Write(this.availablePopulationTowers);
		bw.Write(this.bonusIncome);
		bw.Write(this.upgradesHitPoints);
		bw.Write(this.upgradesUltralibrium);
		bw.Write(this.upgradesPopulation);
		bw.Write(this.upgradesBarracks);
		bw.Write(this.upgradesAlien1);
		bw.Write(this.upgradesAlien2);
		bw.Write(this.upgradesAlien3);
		bw.Write(this.upgradesAlien4);
		bw.Write(this.upgradesAlien5);
		bw.Write(this.upgradesTurret1);
		bw.Write(this.upgradesTurret2);
		bw.Write(this.upgradesTurret3);
		bw.Write(this.upgradesTurret4);
		bw.Write(this.upgradesTurret5);
		if (!(this.establishingControlTime == DateTime.MinValue))
		{
			now = DateTime.Now - this.establishingControlTime;
			bw.Write((long)now.TotalMilliseconds);
		}
		else
		{
			bw.Write((long)0);
		}
		if ((this.vulnerableEndTime == DateTime.MinValue ? false : !(this.vulnerableEndTime < StaticData.now)))
		{
			now = DateTime.Now - this.vulnerableEndTime;
			bw.Write((long)now.TotalMilliseconds);
		}
		else
		{
			bw.Write((long)0);
		}
		if ((this.invulnerableModeEndTime == DateTime.MinValue ? false : !(this.invulnerableModeEndTime < StaticData.now)))
		{
			now = DateTime.Now - this.invulnerableModeEndTime;
			bw.Write((long)now.TotalMilliseconds);
		}
		else
		{
			bw.Write((long)0);
		}
		if ((this.damageReductionBoostEndTime == DateTime.MinValue ? false : !(this.damageReductionBoostEndTime < StaticData.now)))
		{
			now = DateTime.Now - this.damageReductionBoostEndTime;
			bw.Write((long)now.TotalMilliseconds);
		}
		else
		{
			bw.Write((long)0);
		}
		bw.Write(this.defendersCountByKind.Count);
		foreach (int key in this.defendersCountByKind.Keys)
		{
			bw.Write(key);
			bw.Write(this.defendersCountByKind[key]);
		}
		int count = this.guardianSkills.Count;
		bw.Write(count);
		for (int i = 0; i < count; i++)
		{
			ExtractionPoinGuardSkills value = this.guardianSkills.ElementAt<KeyValuePair<byte, ExtractionPoinGuardSkills>>(i).Value;
			bw.Write(value.unitType);
			int num = value.guardianSkills.Count;
			bw.Write(num);
			for (int j = 0; j < num; j++)
			{
				KeyValuePair<short, byte> keyValuePair = value.guardianSkills.ElementAt<KeyValuePair<short, byte>>(j);
				bw.Write(keyValuePair.Key);
				bw.Write(keyValuePair.Value);
			}
		}
		base.Serialize(bw);
	}

	public void SerializeInContext(BinaryWriter bw, TransferContext context)
	{
		int i;
		TransferContext transferContext = context;
		if (transferContext == TransferContext.EpContributors)
		{
			bw.Write(this.topTenContributors.Count);
			for (i = 0; i < this.topTenContributors.Count; i++)
			{
				bw.Write(this.topTenContributors[i].battleContribution);
				bw.Write(this.topTenContributors[i].fractionId);
				bw.Write(this.topTenContributors[i].novaContribution);
				bw.Write(this.topTenContributors[i].point_id);
				bw.Write(this.topTenContributors[i].viralContribution);
				bw.Write(this.topTenContributors[i].tottalContribution);
				bw.Write(this.topTenContributors[i].incomeBonus);
				bw.Write(this.topTenContributors[i].displayName ?? "");
			}
		}
		else if (transferContext == TransferContext.GameMapOverview)
		{
			bw.Write(this.pointId);
			bw.Write(this.x);
			bw.Write(this.z);
			bw.Write((byte)this.state);
			bw.Write(this.ownerFraction);
			if (this.topTenContributors != null)
			{
				int num = Math.Min(this.topTenContributors.Count, 5);
				bw.Write(num);
				Contributor[] array = (
					from t in this.topTenContributors
					orderby t.tottalContribution descending
					select t).Take<Contributor>(num).ToArray<Contributor>();
				for (i = 0; i < num; i++)
				{
					bw.Write(array[i].tottalContribution);
					bw.Write(array[i].incomeBonus);
					bw.Write(array[i].displayName ?? "");
				}
			}
			else
			{
				bw.Write(-1);
			}
		}
	}
}