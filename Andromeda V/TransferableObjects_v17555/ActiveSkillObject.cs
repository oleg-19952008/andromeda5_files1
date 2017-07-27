using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

public class ActiveSkillObject : ProjectileObject, ITransferable
{
	public const float MAX_AVOIDANCE_REDUCE = 0.3f;

	public const float MAX_SLOW_REDUCTION = 0.3f;

	public const int STEALTH_SPEED_MODIFIER = 40;

	public PlayerObjectPhysics caster;

	public Location targetLocation;

	public int skillId;

	public uint casterNeibId;

	public int x_value;

	public int y_value;

	public DateTime castTime;

	public DateTime endTime;

	public DateTime iterationTime;

	public int timeSinceStarted;

	public int duration;

	public int animationLifetime;

	public int timeBetwenIteration;

	public short skillSlotId;

	public long nextCastTime;

	public Action<ActiveSkillObject> RemoveSkillObject;

	public Action<PlayerObjectPhysics> Notify;

	public Action<PlayerObjectPhysics, PlayerObjectPhysics, int, int> Slow;

	public Action<ActiveSkillObject> GetClosePlayers;

	public Victor3 rotation;

	public float acceleration = 20f;

	public float maxSpeed = 130f;

	public float originalValue;

	public SortedList<uint, PlayerObjectPhysics> playerOnTheRoad = new SortedList<uint, PlayerObjectPhysics>();

	public SortedList<uint, PlayerObjectPhysics> hitedPlayerOnTheRoad = new SortedList<uint, PlayerObjectPhysics>();

	private bool forCriticalQuestCount = false;

	public ActiveSkillObject()
	{
	}

	public override void CalculateObjectMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		if (this.skillId == PlayerItems.TypeTalentsSunderArmor)
		{
			this.SunderArmorCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == PlayerItems.TypeTalentsTaunt)
		{
			this.TauntCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == PlayerItems.TypeTalentsFocusFire)
		{
			this.FocusFireCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == PlayerItems.TypeTalentsRocketBarrage)
		{
			this.RocketBarrageCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == PlayerItems.TypeTalentsShieldFortress)
		{
			this.ShieldFortressCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == PlayerItems.TypeTalentsDecoy)
		{
			this.DecoyCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == PlayerItems.TypeTalentsPowerBreak)
		{
			this.PowerBreakCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == PlayerItems.TypeTalentsLightSpeed)
		{
			this.LightSpeedCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == PlayerItems.TypeTalentsMistShroud)
		{
			this.MistShroudCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == PlayerItems.TypeTalentsLaserDestruction)
		{
			this.LaserDestructionCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == PlayerItems.TypeTalentsPulseNova)
		{
			this.PulseNovaCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == PlayerItems.TypeTalentsRemedy)
		{
			this.RemedyCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == PlayerItems.TypeTalentsUnstoppable)
		{
			this.UnstoppableCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == PlayerItems.TypeTalentsStealth)
		{
			this.StealthCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == PlayerItems.TypeTalentsNanoStorm)
		{
			this.NanoStormCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == PlayerItems.TypeTalentsPowerCut)
		{
			this.PowerCutCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == PlayerItems.TypeTalentsForceWave)
		{
			this.ForceWaveCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == PlayerItems.TypeTalentsRepairingDrones)
		{
			this.RepairingDronesCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == PlayerItems.TypeTalentsNanoShield)
		{
			this.NanoShieldCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == PlayerItems.TypeTalentsRepairField)
		{
			this.RepairFieldCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == PlayerItems.TypeTalentsShortCircuit)
		{
			this.ShortCircuitCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == PlayerItems.TypeCouncilSkillDisarm)
		{
			this.DisarmCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == PlayerItems.TypeCouncilSkillSacrifice)
		{
			this.SacrificeCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == PlayerItems.TypeCouncilSkillLifesteal)
		{
			this.LifestealCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == 666)
		{
			this.ShockCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == 777)
		{
			this.DisarmedCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == 888)
		{
			this.StunCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
		else if (this.skillId == 999)
		{
			this.SlowCalculateMovement(dt, ref dx, ref dy, ref dz);
		}
	}

	private void DecoyCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		int num = 0;
		this.MoveFromCasterToTarget(dt, ref dx, ref dy, ref dz, num, this.target, this.forCriticalQuestCount);
	}

	public override void Deserialize(BinaryReader br)
	{
		this.x_value = br.ReadInt32();
		this.y_value = br.ReadInt32();
		this.skillId = br.ReadInt32();
		this.casterNeibId = br.ReadUInt32();
		if (br.ReadBoolean())
		{
			this.targetLocation = new Location()
			{
				x = br.ReadSingle(),
				y = br.ReadSingle(),
				z = br.ReadSingle(),
				radius = br.ReadSingle()
			};
		}
		this.timeSinceStarted = br.ReadInt32();
		this.duration = br.ReadInt32();
		this.animationLifetime = br.ReadInt32();
		this.timeBetwenIteration = br.ReadInt32();
		this.speed = br.ReadSingle();
		this.acceleration = br.ReadSingle();
		this.maxSpeed = br.ReadSingle();
		this.castTime = new DateTime(StaticData.now.Ticks - (long)this.timeSinceStarted);
		this.endTime = this.castTime.AddMilliseconds((double)this.duration);
		this.iterationTime = this.castTime.AddMilliseconds((double)this.timeBetwenIteration);
		this.originalValue = br.ReadSingle();
		this.skillSlotId = br.ReadInt16();
		this.nextCastTime = StaticData.now.Ticks + br.ReadInt64();
		base.Deserialize(br);
	}

	private void DetectAndManageTargetCollision(int damage, GameObjectPhysics collisionTarget, bool isCriticalHit = false, float customAddDistance = 0.5f)
	{
		if (!this.isOnClientSide)
		{
			if (!this.isRemoved)
			{
				float single = Victor3.Distance(new Victor3(this.x, this.y, this.z), new Victor3(collisionTarget.x, collisionTarget.y, collisionTarget.z));
				if ((single < this.speed * this.dt ? true : single < customAddDistance))
				{
					if (this.skillId != PlayerItems.TypeTalentsForceWave)
					{
						if (this.TakeDamage != null)
						{
							this.TakeDamage(this.caster, this.target, this, 0, damage, true, false, 0, isCriticalHit);
						}
						if ((this.skillId != PlayerItems.TypeTalentsPowerCut ? false : this.Slow != null))
						{
							if (this.target.IsPoP)
							{
								this.Slow(this.caster, (PlayerObjectPhysics)this.target, this.duration, this.x_value);
							}
						}
						if ((this.skillId != PlayerItems.TypeTalentsFocusFire ? false : this.target is PvEPhysics))
						{
							((PvEPhysics)this.target).AddAggro(this.caster, (float)damage);
						}
						if ((this.skillId != PlayerItems.TypeTalentsDecoy ? false : this.target is PvEPhysics))
						{
							((PvEPhysics)this.target).AddAggro(this.caster, (float)(-damage) * 0.5f);
						}
						this.RemoveSkillObject(this);
					}
					else
					{
						this.RemoveSkillObject(this);
					}
				}
			}
		}
	}

	private void DisarmCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		dx = 0f;
		dy = 0f;
		dz = 0f;
		if (this.RemoveSkillObject != null)
		{
			this.RemoveSkillObject(this);
		}
	}

	private void DisarmedCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		if ((this.target == null ? false : !this.target.isRemoved))
		{
			this.x = this.target.x;
			this.y = this.target.y;
			this.z = this.target.z;
			if (this.endTime < StaticData.now)
			{
				this.target.RemoveActivatedSkill(this);
				if (this.RemoveSkillObject != null)
				{
					this.RemoveSkillObject(this);
				}
			}
			else if (this.iterationTime < StaticData.now)
			{
				this.iterationTime = this.endTime;
			}
		}
		else if (this.RemoveSkillObject != null)
		{
			this.RemoveSkillObject(this);
		}
	}

	private void FocusFireCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		int num = 0;
		this.MoveFromCasterToTarget(dt, ref dx, ref dy, ref dz, num, this.target, this.forCriticalQuestCount);
	}

	private void ForceWaveCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		this.MoveFromCasterToTarget(dt, ref dx, ref dy, ref dz, 0, this.targetLocation, false);
		int num = 0;
		if (this.GetClosePlayers != null)
		{
			this.GetClosePlayers(this);
			foreach (uint key in this.playerOnTheRoad.Keys)
			{
				if (!this.hitedPlayerOnTheRoad.ContainsKey(key))
				{
					if (this.TakeDamage != null)
					{
						this.TakeDamage(this.caster, this.playerOnTheRoad[key], this, 0, num, true, false, 0, this.forCriticalQuestCount);
					}
					this.hitedPlayerOnTheRoad.Add(key, this.playerOnTheRoad[key]);
				}
			}
		}
	}

	private void LaserDestructionCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		dx = 0f;
		dy = 0f;
		dz = 0f;
		if (this.TakeDamage != null)
		{
			int xValue = (int)((float)(this.caster.cfg.skillDamage * (this.x_value + this.y_value)) / 100f);
			this.TakeDamage(this.caster, this.target, this, 0, xValue, true, false, 0, this.forCriticalQuestCount);
		}
		if (this.RemoveSkillObject != null)
		{
			this.RemoveSkillObject(this);
		}
	}

	private void LifestealCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		dx = 0f;
		dy = 0f;
		dz = 0f;
		if (this.TakeDamage != null)
		{
			int xValue = (int)((float)(((PlayerObjectPhysics)this.target).cfg.hitPointsMax * this.x_value) / 100f);
			int num = (int)((float)(((PlayerObjectPhysics)this.target).cfg.shieldMax * this.x_value) / 100f);
			this.TakeDamage(this.caster, this.target, this, xValue, num, true, false, 0, false);
			this.TakeDamage(this.caster, this.caster, this, -(xValue + num), 0, true, false, 0, false);
		}
		if (this.RemoveSkillObject != null)
		{
			this.RemoveSkillObject(this);
		}
	}

	private void LightSpeedCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		this.x = this.target.x;
		this.y = this.target.y;
		this.z = this.target.z;
		if (this.endTime < StaticData.now)
		{
			this.target.RemoveActivatedSkill(this);
			if (this.target.IsPoP)
			{
			}
			if (this.RemoveSkillObject != null)
			{
				this.RemoveSkillObject(this);
			}
		}
		else if (this.iterationTime < StaticData.now)
		{
			if (this.target.IsPoP)
			{
			}
			this.iterationTime = this.endTime;
		}
	}

	private void MistShroudCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		float xValue;
		ActiveSkillObject activeSkillObject;
		ActiveSkillObject[] activeSkillObjectArray;
		int i;
		this.x = this.target.x;
		this.y = this.target.y;
		this.z = this.target.z;
		if (this.endTime < StaticData.now)
		{
			this.target.RemoveActivatedSkill(this);
			xValue = 0f;
			activeSkillObjectArray = this.caster.activatedSkillsSafe;
			for (i = 0; i < (int)activeSkillObjectArray.Length; i++)
			{
				activeSkillObject = activeSkillObjectArray[i];
				if (activeSkillObject.skillId == PlayerItems.TypeTalentsSunderArmor)
				{
					xValue += (float)activeSkillObject.x_value;
				}
				else if (activeSkillObject.skillId == PlayerItems.TypeTalentsMistShroud)
				{
					xValue -= (float)activeSkillObject.x_value;
				}
			}
			this.caster.cfg.currentAvoidance = Math.Max(0.3f, 1f - xValue / 100f) * this.caster.cfg.avoidanceMax;
			if (this.RemoveSkillObject != null)
			{
				this.RemoveSkillObject(this);
			}
		}
		else if (this.iterationTime < StaticData.now)
		{
			xValue = 0f;
			activeSkillObjectArray = this.caster.activatedSkillsSafe;
			for (i = 0; i < (int)activeSkillObjectArray.Length; i++)
			{
				activeSkillObject = activeSkillObjectArray[i];
				if (activeSkillObject.skillId == PlayerItems.TypeTalentsSunderArmor)
				{
					xValue += (float)activeSkillObject.x_value;
				}
				else if (activeSkillObject.skillId == PlayerItems.TypeTalentsMistShroud)
				{
					xValue -= (float)activeSkillObject.x_value;
				}
			}
			this.caster.cfg.currentAvoidance = Math.Max(0.3f, 1f - xValue / 100f) * this.caster.cfg.avoidanceMax;
			this.iterationTime = this.endTime;
		}
	}

	private void MoveFromCasterToTarget(float dt, ref float dx, ref float dy, ref float dz, int damage, GameObjectPhysics target, bool isCriticalHit = false)
	{
		float single = 10f;
		if (this.skillId == PlayerItems.TypeTalentsForceWave)
		{
			single = 20f;
			this.acceleration = 30f;
		}
		this.dt = dt;
		if (this.speed < single)
		{
			this.speed = single;
		}
		ActiveSkillObject activeSkillObject = this;
		activeSkillObject.speed = activeSkillObject.speed + this.acceleration * dt;
		if (this.speed > this.maxSpeed)
		{
			this.speed = this.maxSpeed;
		}
		Victor3 victor3 = Victor3.MoveTowards(new Victor3(this.x, this.y, this.z), new Victor3(target.x, target.y, target.z), this.speed * dt);
		base.KeepInBoundary(ref victor3);
		dx = victor3.x - this.x;
		dy = victor3.y - this.y;
		dz = victor3.z - this.z;
		this.DetectAndManageTargetCollision(damage, target, isCriticalHit, 0.5f);
	}

	private void NanoShieldCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		bool flag;
		if (this.target != null)
		{
			this.x = this.target.x;
			this.y = this.target.y;
			this.z = this.target.z;
			if (this.originalValue <= 0f || this.target.isRemoved)
			{
				flag = false;
			}
			else
			{
				flag = (!this.target.IsPoP ? true : ((PlayerObjectPhysics)this.target).isAlive);
			}
			if (!flag)
			{
				this.target.RemoveActivatedSkill(this);
				if (this.RemoveSkillObject != null)
				{
					this.RemoveSkillObject(this);
				}
			}
		}
		else if (this.RemoveSkillObject != null)
		{
			this.RemoveSkillObject(this);
		}
	}

	private void NanoStormCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		this.x = this.target.x;
		this.y = this.target.y;
		this.z = this.target.z;
		if (!this.isOnClientSide)
		{
			this.targetLocation.x = this.target.x;
			this.targetLocation.y = this.target.y;
			this.targetLocation.z = this.target.z;
		}
		if (this.endTime < StaticData.now)
		{
			this.target.RemoveActivatedSkill(this);
			if (this.RemoveSkillObject != null)
			{
				this.RemoveSkillObject(this);
			}
		}
		else if (this.iterationTime < StaticData.now)
		{
			if (this.TakeDamage != null)
			{
				int xValue = (int)((float)(this.caster.cfg.skillDamage * (this.x_value + this.y_value)) / 100f);
				this.TakeDamage(this.caster, this.targetLocation, this, 0, xValue, true, false, 0, this.forCriticalQuestCount);
			}
			this.iterationTime = this.iterationTime.AddMilliseconds((double)this.timeBetwenIteration);
		}
	}

	private void PowerBreakCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		dx = 0f;
		dy = 0f;
		dz = 0f;
		if (this.endTime < StaticData.now)
		{
			this.target.RemoveActivatedSkill(this);
			if (this.RemoveSkillObject != null)
			{
				this.RemoveSkillObject(this);
			}
		}
		else if (this.iterationTime < StaticData.now)
		{
			this.iterationTime = this.endTime;
		}
	}

	private void PowerCutCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		int num = 0;
		this.MoveFromCasterToTarget(dt, ref dx, ref dy, ref dz, num, this.target, this.forCriticalQuestCount);
	}

	private void PulseNovaCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		dx = 0f;
		dy = 0f;
		dz = 0f;
		if (this.TakeDamage != null)
		{
			int xValue = (int)((float)(this.caster.cfg.skillDamage * (this.x_value + this.y_value)) / 100f);
			this.TakeDamage(this.caster, this.targetLocation, this, 0, xValue, true, false, 0, this.forCriticalQuestCount);
		}
		if (this.RemoveSkillObject != null)
		{
			this.RemoveSkillObject(this);
		}
	}

	private void RemedyCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		this.x = this.target.x;
		this.y = this.target.y;
		this.z = this.target.z;
		if (this.endTime < StaticData.now)
		{
			this.target.RemoveActivatedSkill(this);
			bool flag = false;
			ActiveSkillObject[] activeSkillObjectArray = this.target.activatedSkillsSafe;
			for (int i = 0; i < (int)activeSkillObjectArray.Length; i++)
			{
				ActiveSkillObject activeSkillObject = activeSkillObjectArray[i];
				if ((activeSkillObject.skillId == PlayerItems.TypeTalentsRemedy ? true : activeSkillObject.skillId == PlayerItems.TypeTalentsShieldFortress))
				{
					flag = true;
				}
			}
			if ((!(this.target is ExtractionPoint) ? false : ((ExtractionPoint)this.target).invulnerableModeEndTime > StaticData.now))
			{
				flag = true;
			}
			this.target.isImmuneToAllIncomingDamage = flag;
			if (this.RemoveSkillObject != null)
			{
				this.RemoveSkillObject(this);
			}
		}
		else if (this.iterationTime < StaticData.now)
		{
			if (this.TakeDamage != null)
			{
				int num = 1;
				if (!this.target.IsPoP)
				{
					this.TakeDamage(this.caster, this.target, this, (int)((float)(-((ExtractionPoint)this.target).hitPoints * this.x_value) / 100f * (float)num), 0, true, false, 0, this.forCriticalQuestCount);
				}
				else
				{
					this.TakeDamage(this.caster, this.target, this, (int)((float)(-((PlayerObjectPhysics)this.target).cfg.hitPointsMax * this.x_value) / 100f * (float)num), (int)((float)(-((PlayerObjectPhysics)this.target).cfg.shieldMax * this.x_value) / 200f * (float)num), true, false, 0, this.forCriticalQuestCount);
				}
			}
			this.target.isImmuneToAllIncomingDamage = true;
			this.iterationTime = this.endTime;
		}
	}

	private void RepairFieldCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		this.x = this.target.x;
		this.y = this.target.y;
		this.z = this.target.z;
		if (!this.isOnClientSide)
		{
			this.targetLocation.x = this.target.x;
			this.targetLocation.y = this.target.y;
			this.targetLocation.z = this.target.z;
		}
		if (this.TakeDamage != null)
		{
			this.TakeDamage(this.caster, this.targetLocation, this, -1, -1, true, false, 0, this.forCriticalQuestCount);
		}
		if (this.RemoveSkillObject != null)
		{
			this.RemoveSkillObject(this);
		}
	}

	private void RepairingDronesCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		bool flag;
		dx = 0f;
		dy = 0f;
		dz = 0f;
		this.x = this.target.x;
		this.y = this.target.y;
		this.z = this.target.z;
		if (this.target == null || this.target.isRemoved)
		{
			flag = false;
		}
		else
		{
			flag = (!this.target.IsPoP ? true : ((PlayerObjectPhysics)this.target).isAlive);
		}
		if (!flag)
		{
			if (this.RemoveSkillObject != null)
			{
				this.RemoveSkillObject(this);
			}
		}
		else if (this.endTime < StaticData.now)
		{
			this.target.RemoveActivatedSkill(this);
			if (this.RemoveSkillObject != null)
			{
				this.RemoveSkillObject(this);
			}
		}
		else if (this.iterationTime < StaticData.now)
		{
			if (this.TakeDamage != null)
			{
				int xValue = (int)((float)(this.caster.cfg.skillDamage * (this.x_value + this.y_value)) / 100f);
				if (!(this.caster.pvpState != PvPPlayerState.Playing ? true : !this.target.IsPoP))
				{
					if (this.caster.teamNumber == ((PlayerObjectPhysics)this.target).teamNumber)
					{
						this.TakeDamage(this.caster, this.target, this, 0, -xValue, true, false, 0, this.forCriticalQuestCount);
					}
					else
					{
						this.TakeDamage(this.caster, this.target, this, 0, xValue, true, false, 0, this.forCriticalQuestCount);
					}
				}
				else if (this.target.IsPoP)
				{
					if (this.caster.fractionId == ((PlayerObjectPhysics)this.target).fractionId)
					{
						this.TakeDamage(this.caster, this.target, this, 0, -xValue, true, false, 0, this.forCriticalQuestCount);
					}
					else
					{
						this.TakeDamage(this.caster, this.target, this, 0, xValue, true, false, 0, this.forCriticalQuestCount);
					}
				}
				else if (this.caster.fractionId == ((ExtractionPoint)this.target).ownerFraction)
				{
					if (((ExtractionPoint)this.target).state != ExtractionPointState.InControl)
					{
						this.TakeDamage(this.caster, this.target, this, 0, xValue, true, false, 0, this.forCriticalQuestCount);
					}
					else
					{
						this.TakeDamage(this.caster, this.target, this, 0, -xValue, true, false, 0, this.forCriticalQuestCount);
					}
				}
				else if (((ExtractionPoint)this.target).state == ExtractionPointState.InControl)
				{
					this.TakeDamage(this.caster, this.target, this, 0, xValue, true, false, 0, this.forCriticalQuestCount);
				}
			}
			this.iterationTime = this.iterationTime.AddMilliseconds((double)this.timeBetwenIteration);
		}
	}

	private void RocketBarrageCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		dx = 0f;
		dy = 0f;
		dz = 0f;
		if (this.endTime < StaticData.now)
		{
			if (this.RemoveSkillObject != null)
			{
				this.RemoveSkillObject(this);
			}
		}
		else if (this.iterationTime < StaticData.now)
		{
			if (this.TakeDamage != null)
			{
				int yValue = (int)((float)(this.caster.cfg.skillDamage * this.y_value) / 100f);
				this.TakeDamage(this.caster, this.targetLocation, this, 0, yValue, true, false, 0, this.forCriticalQuestCount);
			}
			this.iterationTime = this.iterationTime.AddMilliseconds((double)this.timeBetwenIteration);
		}
	}

	private void SacrificeCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		dx = 0f;
		dy = 0f;
		dz = 0f;
		if (this.RemoveSkillObject != null)
		{
			this.RemoveSkillObject(this);
		}
	}

	public override void Serialize(BinaryWriter bw)
	{
		bw.Write(this.x_value);
		bw.Write(this.y_value);
		bw.Write(this.skillId);
		bw.Write(this.caster.neighbourhoodId);
		if (this.targetLocation == null)
		{
			bw.Write(false);
		}
		else
		{
			bw.Write(true);
			bw.Write(this.targetLocation.x);
			bw.Write(this.targetLocation.y);
			bw.Write(this.targetLocation.z);
			bw.Write(this.targetLocation.radius);
		}
		bw.Write(this.timeSinceStarted);
		bw.Write(this.duration);
		bw.Write(this.animationLifetime);
		bw.Write(this.timeBetwenIteration);
		bw.Write(this.speed);
		bw.Write(this.acceleration);
		bw.Write(this.maxSpeed);
		bw.Write(this.originalValue);
		bw.Write(this.skillSlotId);
		bw.Write(this.nextCastTime - StaticData.now.Ticks);
		base.Serialize(bw);
	}

	private void ShieldFortressCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		this.x = this.target.x;
		this.y = this.target.y;
		this.z = this.target.z;
		if (this.endTime < StaticData.now)
		{
			this.target.RemoveActivatedSkill(this);
			bool flag = false;
			ActiveSkillObject[] activeSkillObjectArray = this.target.activatedSkillsSafe;
			for (int i = 0; i < (int)activeSkillObjectArray.Length; i++)
			{
				ActiveSkillObject activeSkillObject = activeSkillObjectArray[i];
				if ((activeSkillObject.skillId == PlayerItems.TypeTalentsRemedy ? true : activeSkillObject.skillId == PlayerItems.TypeTalentsShieldFortress))
				{
					flag = true;
				}
			}
			this.caster.isImmuneToAllIncomingDamage = flag;
			if (this.RemoveSkillObject != null)
			{
				this.RemoveSkillObject(this);
			}
		}
		else if (this.iterationTime < StaticData.now)
		{
			this.caster.isImmuneToAllIncomingDamage = true;
			this.iterationTime = this.endTime;
		}
	}

	private void ShockCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		if ((this.target == null ? false : !this.target.isRemoved))
		{
			this.x = this.target.x;
			this.y = this.target.y;
			this.z = this.target.z;
			if (this.endTime < StaticData.now)
			{
				this.target.RemoveActivatedSkill(this);
				if (this.RemoveSkillObject != null)
				{
					this.RemoveSkillObject(this);
				}
			}
			else if (this.iterationTime < StaticData.now)
			{
				this.iterationTime = this.endTime;
			}
		}
		else if (this.RemoveSkillObject != null)
		{
			this.RemoveSkillObject(this);
		}
	}

	private void ShortCircuitCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		bool flag;
		if (this.target == null || this.target.isRemoved)
		{
			flag = false;
		}
		else
		{
			flag = (!this.target.IsPoP ? true : ((PlayerObjectPhysics)this.target).isAlive);
		}
		if (!flag)
		{
			this.caster.isShortCircuitCaster = false;
			this.caster.shortCircuitTarget = null;
			if (this.RemoveSkillObject != null)
			{
				this.RemoveSkillObject(this);
			}
		}
		else if (this.endTime < StaticData.now)
		{
			this.caster.isShortCircuitCaster = false;
			this.caster.shortCircuitTarget = null;
			this.target.RemoveActivatedSkill(this);
			if (this.RemoveSkillObject != null)
			{
				this.RemoveSkillObject(this);
			}
		}
		else if (this.iterationTime < StaticData.now)
		{
			if (this.TakeDamage != null)
			{
				int yValue = (int)((float)(this.caster.cfg.skillDamage * (this.y_value + this.x_value)) / 100f);
				this.TakeDamage(this.caster, this.target, this, 0, yValue, true, true, 1010, this.forCriticalQuestCount);
			}
			this.iterationTime = this.iterationTime.AddMilliseconds((double)this.timeBetwenIteration);
		}
	}

	private void SlowCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		this.x = this.target.x;
		this.y = this.target.y;
		this.z = this.target.z;
		if (this.endTime < StaticData.now)
		{
			this.target.RemoveActivatedSkill(this);
			if (this.target.IsPoP)
			{
			}
			if (this.RemoveSkillObject != null)
			{
				this.RemoveSkillObject(this);
			}
		}
		else if (this.iterationTime < StaticData.now)
		{
			if (this.target.IsPoP)
			{
			}
			this.iterationTime = this.endTime;
		}
	}

	private void StealthCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		bool flag;
		this.x = this.target.x;
		this.y = this.target.y;
		this.z = this.target.z;
		if (this.target == null || this.target.isRemoved)
		{
			flag = false;
		}
		else
		{
			flag = (!this.target.IsPoP ? true : ((PlayerObjectPhysics)this.target).isAlive);
		}
		if (!flag)
		{
			if (this.RemoveSkillObject != null)
			{
				this.RemoveSkillObject(this);
			}
		}
		else if (this.endTime < StaticData.now)
		{
			this.target.RemoveActivatedSkill(this);
			if (this.Notify != null)
			{
				this.Notify((PlayerObjectPhysics)this.target);
			}
			if (this.RemoveSkillObject != null)
			{
				this.RemoveSkillObject(this);
			}
		}
		else if (this.iterationTime < StaticData.now)
		{
			((PlayerObjectPhysics)this.target).isInStealthMode = true;
			this.iterationTime = this.endTime;
		}
	}

	private void StunCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		if (this.target != null)
		{
			this.x = this.target.x;
			this.y = this.target.y;
			this.z = this.target.z;
			if (this.endTime < StaticData.now)
			{
				this.target.RemoveActivatedSkill(this);
				if (this.RemoveSkillObject != null)
				{
					this.RemoveSkillObject(this);
				}
			}
			else if (this.iterationTime < StaticData.now)
			{
				this.iterationTime = this.endTime;
			}
		}
		else if (this.RemoveSkillObject != null)
		{
			this.RemoveSkillObject(this);
		}
	}

	private void SunderArmorCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		float xValue;
		ActiveSkillObject activeSkillObject;
		ActiveSkillObject[] activeSkillObjectArray;
		int i;
		this.x = this.target.x;
		this.y = this.target.y;
		this.z = this.target.z;
		if (this.endTime < StaticData.now)
		{
			this.target.RemoveActivatedSkill(this);
			if (this.target.IsPoP)
			{
				xValue = 0f;
				activeSkillObjectArray = this.target.activatedSkillsSafe;
				for (i = 0; i < (int)activeSkillObjectArray.Length; i++)
				{
					activeSkillObject = activeSkillObjectArray[i];
					if (activeSkillObject.skillId == PlayerItems.TypeTalentsSunderArmor)
					{
						xValue += (float)activeSkillObject.x_value;
					}
					else if (activeSkillObject.skillId == PlayerItems.TypeTalentsMistShroud)
					{
						xValue -= (float)activeSkillObject.x_value;
					}
				}
				((PlayerObjectPhysics)this.target).cfg.currentAvoidance = (float)(Math.Max(0.3f, 1f - xValue / 100f) * ((PlayerObjectPhysics)this.target).cfg.avoidanceMax);
			}
			if (this.RemoveSkillObject != null)
			{
				this.RemoveSkillObject(this);
			}
		}
		else if (this.iterationTime < StaticData.now)
		{
			if (this.target.IsPoP)
			{
				xValue = 0f;
				activeSkillObjectArray = this.target.activatedSkillsSafe;
				for (i = 0; i < (int)activeSkillObjectArray.Length; i++)
				{
					activeSkillObject = activeSkillObjectArray[i];
					if (activeSkillObject.skillId == PlayerItems.TypeTalentsSunderArmor)
					{
						xValue += (float)activeSkillObject.x_value;
					}
					else if (activeSkillObject.skillId == PlayerItems.TypeTalentsMistShroud)
					{
						xValue -= (float)activeSkillObject.x_value;
					}
				}
				((PlayerObjectPhysics)this.target).cfg.currentAvoidance = (float)(Math.Max(0.3f, 1f - xValue / 100f) * ((PlayerObjectPhysics)this.target).cfg.avoidanceMax);
			}
			if (this.TakeDamage != null)
			{
				int yValue = (int)((float)this.caster.cfg.skillDamage * ((float)this.y_value / 100f));
				this.TakeDamage(this.caster, this.target, this, 0, yValue, true, false, 0, this.forCriticalQuestCount);
				if (this.target is PvEPhysics)
				{
					((PvEPhysics)this.target).AddAggro(this.caster, (float)((double)yValue * 0.2));
				}
			}
			this.iterationTime = this.endTime;
		}
	}

	private void TauntCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		dx = 0f;
		dy = 0f;
		dz = 0f;
		foreach (PvEPhysics list in this.caster.nbPVEs.Values.ToList<PvEPhysics>())
		{
			if (GameObjectPhysics.GetDistance(this.caster.x, list.x, this.caster.z, list.z) < 20f)
			{
			}
		}
		foreach (PlayerObjectPhysics playerObjectPhysic in this.caster.nbPlayers.Values.ToList<PlayerObjectPhysics>())
		{
			if ((!playerObjectPhysic.isInStealthMode || this.caster.teamNumber == playerObjectPhysic.teamNumber && this.caster.fractionId == playerObjectPhysic.fractionId ? false : GameObjectPhysics.GetDistance(this.caster.x, playerObjectPhysic.x, this.caster.z, playerObjectPhysic.z) < 20f))
			{
				ActiveSkillObject activeSkillObject = (
					from s in (IEnumerable<ActiveSkillObject>)playerObjectPhysic.activatedSkillsSafe
					where s.skillId == PlayerItems.TypeTalentsStealth
					select s).First<ActiveSkillObject>();
				activeSkillObject.Notify((PlayerObjectPhysics)activeSkillObject.target);
				activeSkillObject.RemoveSkillObject(activeSkillObject);
			}
		}
		if (this.RemoveSkillObject != null)
		{
			this.RemoveSkillObject(this);
		}
	}

	private void UnstoppableCalculateMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		ActiveSkillObject activeSkillObject;
		ActiveSkillObject[] activeSkillObjectArray;
		int i;
		this.x = this.target.x;
		this.y = this.target.y;
		this.z = this.target.z;
		if (this.endTime < StaticData.now)
		{
			this.target.RemoveActivatedSkill(this);
			bool flag = false;
			activeSkillObjectArray = this.target.activatedSkillsSafe;
			i = 0;
			while (i < (int)activeSkillObjectArray.Length)
			{
				activeSkillObject = activeSkillObjectArray[i];
				if (activeSkillObject.skillId != PlayerItems.TypeTalentsUnstoppable)
				{
					i++;
				}
				else
				{
					flag = true;
					break;
				}
			}
			if (this.target.IsPoP)
			{
				((PlayerObjectPhysics)this.target).isImmuneToCrowd = flag;
			}
			if (this.RemoveSkillObject != null)
			{
				this.RemoveSkillObject(this);
			}
		}
		else if (this.iterationTime < StaticData.now)
		{
			activeSkillObjectArray = this.target.activatedSkillsSafe;
			for (i = 0; i < (int)activeSkillObjectArray.Length; i++)
			{
				activeSkillObject = activeSkillObjectArray[i];
				if (!(activeSkillObject.skillId == PlayerItems.TypeTalentsPowerBreak ? false : activeSkillObject.skillId != PlayerItems.TypeTalentsForceWave))
				{
					if (activeSkillObject.iterationTime == activeSkillObject.endTime)
					{
						activeSkillObject.RemoveSkillObject(activeSkillObject);
						this.target.RemoveActivatedSkill(activeSkillObject);
					}
				}
				else if ((activeSkillObject.skillId == PlayerItems.TypeTalentsShortCircuit || activeSkillObject.skillId == 999 || activeSkillObject.skillId == 888 || activeSkillObject.skillId == 777 ? true : activeSkillObject.skillId == PlayerItems.TypeTalentsLightSpeed))
				{
					activeSkillObject.RemoveSkillObject(activeSkillObject);
					this.target.RemoveActivatedSkill(activeSkillObject);
				}
			}
			if (this.target.IsPoP)
			{
				PlayerObjectPhysics playerObjectPhysic = (PlayerObjectPhysics)this.target;
				playerObjectPhysic.isImmuneToCrowd = true;
				playerObjectPhysic.isSlowedFromAmmo = false;
			}
			this.iterationTime = this.endTime;
		}
	}
}