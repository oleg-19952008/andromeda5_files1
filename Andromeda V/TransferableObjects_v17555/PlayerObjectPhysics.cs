using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

public class PlayerObjectPhysics : GameObjectPhysics, ITransferable
{
	public static int TALENT_RETRAIN_PRICE;

	[NonSerialized]
	public static float enteringBaseSpeed;

	public static long MIN_COOLDOWN_TIME;

	public static byte AMMO_SLOW_TIME_DURATION;

	public static float miningRange;

	public static float SHIELD_RESTORE_RATE;

	public bool isInControl = true;

	public byte teamNumber = 0;

	public string playerName;

	public string guildTag;

	public string shipName;

	public DateTime timeOfLastCombat = DateTime.MinValue;

	public string playerAvatarUrl = "";

	public long playerId;

	[NonSerialized]
	public PlayerData playerData;

	public ShipConfiguration cfg;

	public object resurrectLocker = new object();

	[NonSerialized]
	public GameObjectPhysics shootingAt;

	[NonSerialized]
	public GameObjectPhysics shootedBy;

	[NonSerialized]
	public PlayerObjectPhysics lockedBy;

	[NonSerialized]
	public GameObjectPhysics shortCircuitTarget;

	public Action<int> EnterInBaseDoor;

	public Action<long> deactivateBooster;

	public Action<long> deactivatePowerUps;

	public DateTime AnimationStartTime = DateTime.MinValue;

	public DateTime timeStartedExitHJ;

	public bool isAlive = true;

	public bool isShooting;

	public byte moveState = 0;

	public byte fractionId;

	public float destinationX;

	public float destinationY;

	public float destinationZ;

	public float distanceToStop;

	public float speed = 0f;

	public int rotationState;

	public float rotationX;

	public float rotationY;

	public float rotationZ;

	public float angularVelocity;

	public float rotationDone;

	public float neededRotation;

	public float destinationAngle;

	public uint miningMineralNbId;

	public bool isInParty = false;

	[NonSerialized]
	public Mineral miningMineral;

	public PvPPlayerState pvpState = PvPPlayerState.None;

	public static RandomGenerator rnd;

	public PvPLeague playerLeague = PvPLeague.Bronze;

	public bool inPvPRank = false;

	public bool isGuest = false;

	public DateTime timeToKickFromInstance;

	public bool isWaitingForInstanceKick = false;

	public float timeToFinishMining;

	public byte miningState;

	public byte enteringBaseState;

	[NonSerialized]
	public static float rotationXdistance;

	[NonSerialized]
	public DateTime resurrectTime;

	public Action onEnterBaseLastMove;

	[NonSerialized]
	public Action<PlayerObjectPhysics> enteredInBase;

	[NonSerialized]
	public Action openDoor;

	public Action closeDoor;

	public Action StoppingMove;

	private bool didOpenDoor;

	[NonSerialized]
	public StarBaseNet enteringBase;

	public byte enteringBaseDoor;

	public int enteringBaseId;

	public bool isBouncing;

	public bool isSpeedBoostActivated;

	public float _halfShipSize = 3f;

	private float bounceDistance = 1f;

	private float bounceDistanceClose = 0.4f;

	public GameObjectPhysics castingAt;

	public bool isCastingSkill = false;

	public short castingSkillSlot = -1;

	[NonSerialized]
	public Action<PlayerObjectPhysics> KIA;

	[NonSerialized]
	public Action<PlayerObjectPhysics> LevelingUp;

	[NonSerialized]
	public Action HitButAlive;

	[NonSerialized]
	public Action HitByVessel;

	public int lastVisitedBase;

	public bool isSlowedFromAmmo = false;

	public DateTime timeOfSlowEnding = DateTime.MinValue;

	public bool isImmuneToCrowd = false;

	public bool isStunned = false;

	public bool isShortCircuitCaster = false;

	public bool isInStealthMode = false;

	public bool isDisarmed = false;

	public bool isShocked = false;

	public uint selectedPoPnbId;

	public bool IsInCombat
	{
		get
		{
			bool flag;
			flag = (!(this.timeOfLastCombat.AddSeconds(5) >= StaticData.now) ? false : true);
			return flag;
		}
	}

	public bool IsPve
	{
		get
		{
			return this.playerId == (long)0;
		}
	}

	static PlayerObjectPhysics()
	{
		PlayerObjectPhysics.TALENT_RETRAIN_PRICE = 300;
		PlayerObjectPhysics.enteringBaseSpeed = 20f;
		PlayerObjectPhysics.MIN_COOLDOWN_TIME = (long)5000000;
		PlayerObjectPhysics.AMMO_SLOW_TIME_DURATION = 2;
		PlayerObjectPhysics.miningRange = 8f;
		PlayerObjectPhysics.SHIELD_RESTORE_RATE = 3.4f;
		PlayerObjectPhysics.rotationXdistance = 3f;
	}

	public PlayerObjectPhysics()
	{
	}

	public void AbortMining()
	{
		if (this.miningState != 0)
		{
			if (this.miningMineral != null)
			{
				this.miningMineral.miningPlayer = null;
				this.miningMineral.miningPlayerId = (long)0;
			}
			this.miningState = 0;
			this.miningMineral = null;
			this.miningMineralNbId = 0;
		}
	}

	public void ActivateSpeedBoost()
	{
		this.isSpeedBoostActivated = true;
		this.ChangeSpeed(this.cfg.maxBoostedSpeed, true);
	}

	protected void AddRotationY(float val)
	{
		this.rotationY += val;
		if (this.rotationY >= 360f)
		{
			this.rotationY -= 360f;
			this.destinationAngle -= 360f;
		}
		else if (this.rotationY < 0f)
		{
			this.rotationY += 360f;
			this.destinationAngle += 360f;
		}
	}

	private void BounceFromCollision()
	{
		this.StartMove();
		if ((this.destinationX != this.x ? true : this.destinationZ != this.z))
		{
			this.isBouncing = true;
		}
	}

	private void CalcMoveEnterBase(float dt, ref float dx, ref float dy, ref float dz)
	{
		switch (this.enteringBaseState)
		{
			case 0:
			case 3:
			{
				this.CalcMoveEnterBase0(dt, out dx, out dy, out dz);
				break;
			}
			case 1:
			{
				this.CalcMoveEnterBase1(dt, out dx, out dy, out dz);
				break;
			}
			case 2:
			{
				this.CalcMoveEnterBase2(dt, out dx, out dy, out dz);
				break;
			}
			case 4:
			{
				this.CalcMoveEnterBasePrepare(dt, out dx, out dy, out dz);
				break;
			}
			default:
			{
				goto case 3;
			}
		}
	}

	private void CalcMoveEnterBase0(float dt, out float dx, out float dy, out float dz)
	{
		if (this.y > this.enteringBase.y + this.enteringBase.doorY[this.enteringBaseDoor])
		{
			if (this.y < (this.enteringBase.y + this.enteringBase.doorY[this.enteringBaseDoor]) / 2f + 5f)
			{
				this.StartRotate();
			}
			if ((double)Math.Abs(this.enteringBase.x + this.enteringBase.doorX[this.enteringBaseDoor] - this.x) <= 0.2)
			{
				dx = 0f;
			}
			else
			{
				dx = (float)Math.Sign(this.enteringBase.x + this.enteringBase.doorX[this.enteringBaseDoor] - this.x) * dt * (PlayerObjectPhysics.enteringBaseSpeed - 10f);
			}
			dz = 0f;
			dy = -dt * PlayerObjectPhysics.enteringBaseSpeed;
			this.ManageRotation(dt);
			float single = this.enteringBase.y + this.enteringBase.doorY[this.enteringBaseDoor];
			if (Math.Abs(this.y) < PlayerObjectPhysics.rotationXdistance)
			{
				this.rotationX = 40f * Math.Abs(this.y) / PlayerObjectPhysics.rotationXdistance;
			}
			else if (Math.Abs(this.y) <= Math.Abs(this.enteringBase.y + this.enteringBase.doorY[this.enteringBaseDoor]) - PlayerObjectPhysics.rotationXdistance)
			{
				this.rotationX = 40f;
			}
			else
			{
				this.rotationX = (Math.Abs(this.enteringBase.y + this.enteringBase.doorY[this.enteringBaseDoor]) - Math.Abs(this.y)) / PlayerObjectPhysics.rotationXdistance * 40f;
			}
			if (!this.didOpenDoor)
			{
				this.didOpenDoor = true;
				if (this.openDoor != null)
				{
					this.openDoor();
				}
			}
		}
		else
		{
			dy = this.enteringBase.y + this.enteringBase.doorY[this.enteringBaseDoor] - this.y;
			dx = 0f;
			dz = 0f;
			this.enteringBaseState = 1;
			this.destinationX = (float)(Math.Sign(this.enteringBase.x + this.enteringBase.doorX[this.enteringBaseDoor] - this.x) * 100) + this.x;
			this.destinationY = this.y;
			this.destinationZ = this.enteringBase.z + this.enteringBase.doorZ[this.enteringBaseDoor];
			this.StartRotate();
		}
	}

	private void CalcMoveEnterBase1(float dt, out float dx, out float dy, out float dz)
	{
		if (Math.Abs(this.enteringBase.x + this.enteringBase.doorX[this.enteringBaseDoor] - this.x) >= 0.2f)
		{
			dx = (float)Math.Sign(this.enteringBase.x + this.enteringBase.doorX[this.enteringBaseDoor] - this.x) * dt * PlayerObjectPhysics.enteringBaseSpeed;
			dz = 0f;
			dy = this.enteringBase.y + this.enteringBase.doorY[this.enteringBaseDoor] - this.y;
			this.ManageRotation(dt);
		}
		else
		{
			dy = this.enteringBase.y + this.enteringBase.doorY[this.enteringBaseDoor] - this.y;
			dx = this.enteringBase.x + this.enteringBase.doorX[this.enteringBaseDoor] - this.x;
			dz = 0f;
			this.destinationX = this.enteringBase.x + this.enteringBase.doorX[this.enteringBaseDoor];
			this.destinationY = this.enteringBase.y + this.enteringBase.doorY[this.enteringBaseDoor];
			this.destinationZ = this.z + 20f;
			this.StartRotate();
			this.didOpenDoor = false;
			this.enteringBaseState = 2;
			if (this.onEnterBaseLastMove != null)
			{
				this.onEnterBaseLastMove();
			}
		}
	}

	private void CalcMoveEnterBase2(float dt, out float dx, out float dy, out float dz)
	{
		if (this.z <= this.enteringBase.z + this.enteringBase.doorZ[this.enteringBaseDoor] + 2f)
		{
			dz = dt * PlayerObjectPhysics.enteringBaseSpeed;
			dx = this.enteringBase.x + this.enteringBase.doorX[this.enteringBaseDoor] - this.x;
			dy = this.enteringBase.y + this.enteringBase.doorY[this.enteringBaseDoor] - this.y;
			this.ManageRotation(dt);
		}
		else
		{
			dx = 0f;
			dy = 0f;
			dz = 0f;
			this.moveState = 11;
			this.enteringBaseState = 3;
			this.didOpenDoor = false;
			if (this.closeDoor != null)
			{
				this.closeDoor();
			}
			this.playerData.state = ServerState.InBase;
			this.lastVisitedBase = this.enteringBase.id;
			if (this.enteredInBase != null)
			{
				this.enteredInBase(this);
			}
		}
	}

	private void CalcMoveEnterBasePrepare(float dt, out float dx, out float dy, out float dz)
	{
		if (this.z > this.enteringBase.z - 20f)
		{
			this.destinationX = this.enteringBase.x + this.enteringBase.doorX[this.enteringBaseDoor];
			this.destinationY = 0f;
			this.destinationZ = this.enteringBase.z - 22f;
			Victor3 victor3 = Victor3.MoveTowards(new Victor3(this.x, this.y, this.z), new Victor3(this.destinationX, this.destinationY, this.destinationZ), PlayerObjectPhysics.enteringBaseSpeed * dt);
			dx = victor3.x - this.x;
			dy = victor3.y - this.y;
			dz = victor3.z - this.z;
			this.ManageRotation(dt);
		}
		else
		{
			dx = 0f;
			dy = 0f;
			dz = 0f;
			this.enteringBaseState = 0;
		}
	}

	private void CalcMoveExitBase(float dt, ref float dx, ref float dy, ref float dz)
	{
		dx = 0f;
		dy = 0f;
		dz = 0f;
		this.moveState = 0;
		this.rotationState = 0;
		this.rotationX = 0f;
		this.rotationY = 180f;
		this.rotationZ = 0f;
		this.playerData.state = ServerState.OnMap;
	}

	public override void CalculateObjectMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		float single;
		float single1;
		float single2;
		if ((this.isOnClientSide ? false : !this.IsPve))
		{
			this.CheckBoosterStatus();
		}
		if (!this.isStunned)
		{
			float single3 = Math.Min(dt, 0.04f);
			if (this.destinationAngle - this.rotationY > 5f)
			{
				PlayerObjectPhysics playerObjectPhysic = this;
				playerObjectPhysic.rotationZ = playerObjectPhysic.rotationZ - 120f * single3;
				if (this.rotationZ < -36f)
				{
					single2 = -36f;
				}
				else
				{
					single2 = (this.rotationZ > 36f ? 36f : this.rotationZ);
				}
				this.rotationZ = single2;
			}
			else if (this.destinationAngle - this.rotationY >= -5f)
			{
				if (this.rotationZ > 1f)
				{
					single = this.rotationZ - Math.Min(55f * single3, this.rotationZ);
				}
				else
				{
					single = (this.rotationZ < -1f ? this.rotationZ + Math.Min(55f * single3, -this.rotationZ) : this.rotationZ);
				}
				this.rotationZ = single;
			}
			else
			{
				PlayerObjectPhysics playerObjectPhysic1 = this;
				playerObjectPhysic1.rotationZ = playerObjectPhysic1.rotationZ + 120f * single3;
				if (this.rotationZ < -36f)
				{
					single1 = -36f;
				}
				else
				{
					single1 = (this.rotationZ > 36f ? 36f : this.rotationZ);
				}
				this.rotationZ = single1;
			}
			if ((this.moveState != 15 ? false : this.timeStartedExitHJ != DateTime.MinValue))
			{
				if (this.timeStartedExitHJ.AddMilliseconds(1000) < StaticData.now)
				{
					this.moveState = 0;
					this.timeStartedExitHJ = DateTime.MinValue;
				}
			}
			if (this.moveState == 12)
			{
				this.CalcMoveExitBase(dt, ref dx, ref dy, ref dz);
			}
			else if (this.moveState == 10)
			{
				this.CalcMoveEnterBase(dt, ref dx, ref dy, ref dz);
			}
			else if (this.moveState != 11)
			{
				if ((!this.isAlive ? false : this.moveState != 14))
				{
					if (this.y != 0f)
					{
						this.y = 0f;
					}
					this.ManageShield(dt);
					this.ManageCriticalEnergy(dt);
				}
				this.ManageRotation(dt);
				if (this.moveState != 0)
				{
					if (this.isSpeedBoostActivated)
					{
						this.ManageSpeedBoostConsumption(dt);
					}
					if (this.moveState == 1)
					{
						PlayerObjectPhysics playerObjectPhysic2 = this;
						playerObjectPhysic2.speed = playerObjectPhysic2.speed + this.cfg.acceleration * dt;
						if (this.speed > this.cfg.currentVelocity)
						{
							this.speed = this.cfg.currentVelocity;
							this.moveState = 2;
						}
					}
					Victor3 victor3 = Victor3.MoveTowards(new Victor3(this.x, this.y, this.z), new Victor3(this.destinationX, this.destinationY, this.destinationZ), this.speed * dt);
					base.KeepInBoundary(ref victor3);
					dx = victor3.x - this.x;
					dy = victor3.y - this.y;
					dz = victor3.z - this.z;
					float distance = GameObjectPhysics.GetDistance(this.destinationX, victor3.x, this.destinationZ, victor3.z);
					if ((this.moveState == 1 ? true : this.moveState == 2))
					{
						if (distance < this.cfg.distanceToStartDecelerate)
						{
							this.moveState = 3;
							if (this.isSpeedBoostActivated)
							{
								this.isSpeedBoostActivated = false;
							}
							if (this.speed >= 5f)
							{
								this.cfg.backAcceleration = 5f;
							}
							else
							{
								this.cfg.backAcceleration = this.speed;
							}
							if (this.StoppingMove != null)
							{
								this.StoppingMove();
							}
							this.isBouncing = false;
						}
					}
					if (this.moveState == 3)
					{
						if ((!this.IsPve ? true : distance + 0.5f <= this.speed))
						{
							PlayerObjectPhysics playerObjectPhysic3 = this;
							playerObjectPhysic3.speed = playerObjectPhysic3.speed - this.cfg.backAcceleration * dt;
						}
						if (this.speed < 0f)
						{
							this.speed = 0f;
						}
						if (this.speed < this.cfg.floatUpSpeed)
						{
							this.speed = 0f;
							this.moveState = 0;
						}
						this.isBouncing = false;
					}
					if (!this.IsPve)
					{
						this.CheckCollisionsMapNew(ref dx, ref dy, ref dz);
					}
					else
					{
						this.CheckCollisionsMap(ref dx, ref dy, ref dz);
					}
				}
				else
				{
					dx = 0f;
					dy = 0f;
					dz = 0f;
					if (this.isSpeedBoostActivated)
					{
						this.isSpeedBoostActivated = false;
					}
				}
			}
			else
			{
				dx = 0f;
				dy = 0f;
				dz = 0f;
			}
		}
		else
		{
			dx = 0f;
			dy = 0f;
			dz = 0f;
			this.destinationX = this.x;
			this.destinationY = this.y;
			this.destinationZ = this.z;
			this.speed = 0f;
			this.moveState = 0;
			if (this.isSpeedBoostActivated)
			{
				this.isSpeedBoostActivated = false;
			}
		}
	}

	private void CalculateRealDamage(WeaponSlot ws, out int dmgCorpus, out int dmgShiel)
	{
		dmgCorpus = 0;
		dmgShiel = 0;
		float single = 0f;
		if ((!this.shootingAt.IsPoP ? true : ((PlayerObjectPhysics)this.shootingAt).IsPve))
		{
			single = (!this.cfg.damageBooster ? (float)ws.skillDamage * (1f + this.cfg.dmgPercentForAlien / 100f) * ((float)((AmmoNet)StaticData.allTypes[ws.selectedAmmoItemType]).damage / 100f) : (float)ws.skillDamage * (1f + this.cfg.dmgPercentForAlien / 100f) * ((float)((AmmoNet)StaticData.allTypes[ws.selectedAmmoItemType]).damage / 100f) * 1.3f);
		}
		else
		{
			single = (float)ws.skillDamage * (1f + this.cfg.dmgPercentForPlayer / 100f) * ((float)((AmmoNet)StaticData.allTypes[ws.selectedAmmoItemType]).damage / 100f);
		}
		if (!this.shootingAt.IsPoP)
		{
			dmgShiel = (int)single;
		}
		else
		{
			dmgCorpus = (int)(single * (float)ws.weaponPenetration / 100f);
			dmgShiel = (int)(single - single * (float)ws.weaponPenetration / 100f);
		}
	}

	public bool CanCastSkillThisTarget(GameObjectPhysics target, int skillId)
	{
		bool flag;
		bool flag1;
		bool flag2;
		bool flag3;
		bool flag4;
		if (!this.isShocked)
		{
			flag1 = true;
		}
		else if (skillId == PlayerItems.TypeTalentsRepairField || skillId == PlayerItems.TypeTalentsRemedy || skillId == PlayerItems.TypeTalentsNanoShield || skillId == PlayerItems.TypeTalentsMistShroud || skillId == PlayerItems.TypeTalentsShieldFortress || skillId == PlayerItems.TypeTalentsUnstoppable)
		{
			flag1 = false;
		}
		else
		{
			flag1 = (skillId != PlayerItems.TypeTalentsRepairingDrones ? true : !this.CanHeal(target));
		}
		if (flag1)
		{
			if (skillId == PlayerItems.TypeCouncilSkillDisarm || skillId == PlayerItems.TypeCouncilSkillSacrifice || skillId == PlayerItems.TypeCouncilSkillLifesteal)
			{
				flag2 = (this.playerData == null || this.playerData.playerBelongings == null ? false : this.playerData.playerBelongings.councilRank != 0);
			}
			else
			{
				flag2 = true;
			}
			if (!flag2)
			{
				flag = false;
			}
			else if (!(skillId != PlayerItems.TypeCouncilSkillSacrifice ? true : (float)this.cfg.hitPoints <= (float)this.cfg.hitPointsMax * 0.15f))
			{
				flag = true;
			}
			else if (!(skillId != PlayerItems.TypeCouncilSkillLifesteal ? true : !this.CanShootThisTarget(target)))
			{
				flag = true;
			}
			else if (!(skillId == PlayerItems.TypeTalentsPulseNova || skillId == PlayerItems.TypeTalentsRocketBarrage ? false : skillId != PlayerItems.TypeTalentsForceWave))
			{
				flag = true;
			}
			else if (!(skillId == PlayerItems.TypeTalentsUnstoppable || skillId == PlayerItems.TypeTalentsShieldFortress || skillId == PlayerItems.TypeTalentsLightSpeed || skillId == PlayerItems.TypeTalentsMistShroud || skillId == PlayerItems.TypeTalentsRepairField || skillId == PlayerItems.TypeTalentsTaunt ? false : skillId != PlayerItems.TypeCouncilSkillDisarm))
			{
				flag = true;
			}
			else if ((skillId != PlayerItems.TypeTalentsStealth || this.IsInCombat ? true : this.pvpState == PvPPlayerState.Playing))
			{
				if (target == null)
				{
					flag3 = false;
				}
				else
				{
					flag3 = (!target.IsPoP ? true : ((PlayerObjectPhysics)target).isAlive);
				}
				if (!flag3)
				{
					flag = false;
				}
				else if (skillId == PlayerItems.TypeTalentsRepairingDrones)
				{
					flag = true;
				}
				else if (!(skillId == PlayerItems.TypeTalentsNanoStorm || skillId == PlayerItems.TypeTalentsNanoShield ? false : skillId != PlayerItems.TypeTalentsRemedy))
				{
					if (!(this.pvpState != PvPPlayerState.Playing ? true : !target.IsPoP))
					{
						flag = this.teamNumber == ((PlayerObjectPhysics)target).teamNumber;
					}
					else if (!target.IsPoP)
					{
						flag = ((((ExtractionPoint)target).ownerFraction != this.fractionId ? true : ((ExtractionPoint)target).state != ExtractionPointState.InControl) ? false : true);
					}
					else
					{
						flag = ((PlayerObjectPhysics)target).fractionId == this.fractionId;
					}
				}
				else if ((skillId == PlayerItems.TypeTalentsSunderArmor || skillId == PlayerItems.TypeTalentsFocusFire || skillId == PlayerItems.TypeTalentsLaserDestruction || skillId == PlayerItems.TypeTalentsDecoy || skillId == PlayerItems.TypeTalentsPowerBreak || skillId == PlayerItems.TypeTalentsPowerCut ? false : skillId != PlayerItems.TypeTalentsShortCircuit))
				{
					flag = false;
				}
				else if (!(this.pvpState != PvPPlayerState.Playing ? true : !target.IsPoP))
				{
					flag = this.teamNumber != ((PlayerObjectPhysics)target).teamNumber;
				}
				else if (!target.IsPoP)
				{
					ExtractionPoint extractionPoint = (ExtractionPoint)target;
					if (extractionPoint.state != ExtractionPointState.InControl || extractionPoint.ownerFraction != this.fractionId)
					{
						flag4 = (extractionPoint.state != ExtractionPointState.BeingCaptured ? true : extractionPoint.ownerFraction == this.fractionId);
					}
					else
					{
						flag4 = false;
					}
					flag = (flag4 ? true : false);
				}
				else
				{
					flag = ((PlayerObjectPhysics)target).fractionId != this.fractionId;
				}
			}
			else
			{
				flag = true;
			}
		}
		else
		{
			flag = false;
		}
		return flag;
	}

	public bool CanHeal(GameObjectPhysics gop)
	{
		bool flag;
		if (gop is ExtractionPoint)
		{
			ExtractionPoint extractionPoint = (ExtractionPoint)gop;
			flag = (extractionPoint.state != ExtractionPointState.InControl ? false : extractionPoint.ownerFraction == this.fractionId);
		}
		else if (!(gop is PlayerObjectPhysics))
		{
			flag = false;
		}
		else
		{
			PlayerObjectPhysics playerObjectPhysic = (PlayerObjectPhysics)gop;
			flag = (this.pvpState != PvPPlayerState.Playing ? this.fractionId == playerObjectPhysic.fractionId : this.teamNumber == playerObjectPhysic.teamNumber);
		}
		return flag;
	}

	public bool CanShootThisTarget(GameObjectPhysics gop)
	{
		bool flag;
		if (!(gop == null ? false : !gop.isRemoved))
		{
			flag = false;
		}
		else if (gop.galaxy.galaxyId != this.galaxy.galaxyId)
		{
			flag = false;
		}
		else if (gop is ExtractionPoint)
		{
			ExtractionPoint extractionPoint = (ExtractionPoint)gop;
			flag = (extractionPoint.ownerFraction == this.fractionId || extractionPoint.health <= 0f ? false : extractionPoint.invulnerableModeEndTime < StaticData.now);
		}
		else if (!(gop is PlayerObjectPhysics))
		{
			flag = false;
		}
		else
		{
			PlayerObjectPhysics playerObjectPhysic = (PlayerObjectPhysics)gop;
			if (playerObjectPhysic.moveState != 10)
			{
				flag = (this.pvpState != PvPPlayerState.Playing ? this.fractionId != playerObjectPhysic.fractionId : this.teamNumber != playerObjectPhysic.teamNumber);
			}
			else
			{
				flag = false;
			}
		}
		return flag;
	}

	public ActiveSkillObject CastSkill(Action<ActiveSkillObject> CreateActiveSkillObjCallback, bool isCritical = false)
	{
		ActiveSkillObject activeSkillObject;
		bool flag;
		bool flag1;
		if ((this.castingSkillSlot == -1 ? false : this.castingAt != null))
		{
			int item = this.playerData.playerBelongings.skillConfig.skillSlots[this.castingSkillSlot].skillId;
			if ((this.isStunned || this.isDisarmed ? item == PlayerItems.TypeTalentsUnstoppable : true))
			{
				if (!this.isShocked)
				{
					flag = true;
				}
				else if (item == PlayerItems.TypeTalentsRepairField || item == PlayerItems.TypeTalentsRemedy || item == PlayerItems.TypeTalentsNanoShield || item == PlayerItems.TypeTalentsMistShroud || item == PlayerItems.TypeTalentsShieldFortress || item == PlayerItems.TypeTalentsUnstoppable)
				{
					flag = false;
				}
				else
				{
					flag = (item != PlayerItems.TypeTalentsRepairingDrones ? true : this.castingAt.neighbourhoodId != this.neighbourhoodId);
				}
				if (!flag)
				{
					activeSkillObject = null;
				}
				else if ((this.playerData.playerBelongings.skillConfig.skillSlots[this.castingSkillSlot].skillId != PlayerItems.TypeCouncilSkillSacrifice ? true : (float)this.cfg.hitPoints >= (float)this.cfg.hitPointsMax * 0.15f))
				{
					ActiveSkillSlot activeSkillSlot = this.playerData.playerBelongings.skillConfig.skillSlots[this.castingSkillSlot];
					if (activeSkillSlot.isConfigured)
					{
						long ticks = StaticData.now.Ticks;
						if (activeSkillSlot.nextCastTime > ticks)
						{
							this.isCastingSkill = false;
							this.castingAt = null;
							this.castingSkillSlot = -1;
							activeSkillObject = null;
						}
						else
						{
							if (activeSkillSlot.skillId != PlayerItems.TypeTalentsStealth)
							{
								flag1 = true;
							}
							else
							{
								flag1 = (this.IsInCombat ? false : this.pvpState != PvPPlayerState.Playing);
							}
							if (!flag1)
							{
								this.isCastingSkill = false;
								this.castingAt = null;
								this.castingSkillSlot = -1;
								activeSkillObject = null;
							}
							else if (GameObjectPhysics.GetDistance(this.castingAt.x, this.x, this.castingAt.z, this.z) <= (float)activeSkillSlot.range)
							{
								activeSkillSlot.nextCastTime = ticks + (long)activeSkillSlot.cooldown * (long)10000;
								ActiveSkillObject activeSkillObject1 = this.StartActiveSkill(activeSkillSlot, CreateActiveSkillObjCallback);
								activeSkillObject1.skillSlotId = activeSkillSlot.slotId;
								activeSkillObject1.nextCastTime = activeSkillSlot.nextCastTime;
								this.isCastingSkill = false;
								this.castingAt = null;
								this.castingSkillSlot = -1;
								activeSkillObject = activeSkillObject1;
							}
							else
							{
								this.isCastingSkill = false;
								this.castingAt = null;
								this.castingSkillSlot = -1;
								activeSkillObject = null;
							}
						}
					}
					else
					{
						this.isCastingSkill = false;
						this.castingAt = null;
						this.castingSkillSlot = -1;
						activeSkillObject = null;
					}
				}
				else
				{
					activeSkillObject = null;
				}
			}
			else
			{
				activeSkillObject = null;
			}
		}
		else
		{
			this.isCastingSkill = false;
			this.castingAt = null;
			this.castingSkillSlot = -1;
			activeSkillObject = null;
		}
		return activeSkillObject;
	}

	public void ChangeAmmoType(WeaponAmmoTypeChange data)
	{
		SlotItemWeapon slotItemWeapon = (SlotItemWeapon)(
			from si in this.playerData.playerBelongings.playerItems.slotItems
			where (si.SlotType != data.slotType || si.Slot != data.slot ? false : si.ShipId == data.shipId)
			select si).FirstOrDefault<SlotItem>();
		if (slotItemWeapon != null)
		{
			slotItemWeapon.AmmoType = data.ammoType;
			this.cfg.weaponSlots[data.slot].selectedAmmoItemType = data.ammoType;
		}
	}

	public void ChangeDisarmState(bool goDisarm)
	{
		this.isDisarmed = goDisarm;
	}

	public void ChangeShockState(bool goShocked)
	{
		this.isShocked = goShocked;
	}

	public void ChangeSpeed(float newSpeed, bool isSpeedBoostActivation = false)
	{
		this.cfg.currentVelocity = newSpeed;
		if (this.moveState == 2)
		{
			this.moveState = 1;
		}
	}

	public void ChangeStunState(bool goStun)
	{
		this.isStunned = goStun;
	}

	private void CheckBoosterStatus()
	{
		if ((this.cfg.cargoBooster || this.cfg.damageBooster || this.cfg.miningBooster ? true : this.cfg.experienceBooster))
		{
			if (this.cfg.cargoBooster)
			{
				if (!this.playerData.playerBelongings.HaveCargoBooster)
				{
					this.cfg.cargoBooster = false;
					if (this.deactivateBooster != null)
					{
						this.deactivateBooster(this.playerId);
					}
				}
			}
			if (this.cfg.damageBooster)
			{
				if (!this.playerData.playerBelongings.HaveDamageBooster)
				{
					this.cfg.damageBooster = false;
					if (this.deactivateBooster != null)
					{
						this.deactivateBooster(this.playerId);
					}
				}
			}
			if (this.cfg.miningBooster)
			{
				if (!this.playerData.playerBelongings.HaveAutoMinerBooster)
				{
					this.cfg.miningBooster = false;
					if (this.deactivateBooster != null)
					{
						this.deactivateBooster(this.playerId);
					}
				}
			}
			if (this.cfg.experienceBooster)
			{
				if (!this.playerData.playerBelongings.HaveExperienceBooster)
				{
					this.cfg.experienceBooster = false;
					if (this.deactivateBooster != null)
					{
						this.deactivateBooster(this.playerId);
					}
				}
			}
		}
	}

	public bool CheckCollisionsMap()
	{
		bool flag;
		if (!this.galaxy.isCollisionAware)
		{
			flag = true;
		}
		else if (this.CheckColumn(this.x - this._halfShipSize, this.z))
		{
			flag = false;
		}
		else if (this.CheckColumn(this.x + this._halfShipSize, this.z))
		{
			flag = false;
		}
		else if (!this.CheckRow(this.z - this._halfShipSize, this.x))
		{
			flag = (!this.CheckRow(this.z + this._halfShipSize, this.x) ? true : false);
		}
		else
		{
			flag = false;
		}
		return flag;
	}

	public void CheckCollisionsMap(ref float dx, ref float dy, ref float dz)
	{
		bool flag;
		if (!this.galaxy.isCollisionAware)
		{
			flag = false;
		}
		else
		{
			flag = (!this.IsPve ? true : ((PvEPhysics)this).pveCommand != PvEPhysics.PvECommandType.ReturnHome);
		}
		if (flag)
		{
			float single = this.x + dx;
			float single1 = this.z + dz;
			if (this.CheckColumn(single - this._halfShipSize, single1))
			{
				this.StopShipAfterCollision(ref dx, ref dy, ref dz);
			}
			else if (this.CheckColumn(single + this._halfShipSize, single1))
			{
				this.StopShipAfterCollision(ref dx, ref dy, ref dz);
			}
			else if (this.CheckRow(single1 - this._halfShipSize, single))
			{
				this.StopShipAfterCollision(ref dx, ref dy, ref dz);
			}
			else if (this.CheckRow(single1 + this._halfShipSize, single))
			{
				this.StopShipAfterCollision(ref dx, ref dy, ref dz);
			}
		}
	}

	public void CheckCollisionsMapNew(ref float dx, ref float dy, ref float dz)
	{
		bool flag;
		if (this.galaxy.isCollisionAware)
		{
			float single = this.x + dx;
			float single1 = this.z + dz;
			if ((this.CheckColumn(single - this._halfShipSize, this.z) || this.CheckColumn(single - (this._halfShipSize - 0.5f), this.z) ? true : this.CheckColumn(single - (this._halfShipSize - 1f), this.z)))
			{
				flag = false;
				if ((this.CheckColumn(single + this.bounceDistance + this._halfShipSize, this.z) ? false : !this.CheckColumn(single + this.bounceDistanceClose + this._halfShipSize, this.z)))
				{
					flag = true;
					this.destinationX = single + this.bounceDistance;
					this.destinationZ = this.z;
				}
				if ((this.rotationY <= 270f ? false : this.rotationY < 360f))
				{
					if (!(this.CheckRow(single1 + this.bounceDistance + this._halfShipSize, single) ? true : this.CheckRow(single1 + this.bounceDistanceClose + this._halfShipSize, single)))
					{
						flag = true;
						this.destinationZ = single1 + this.bounceDistance;
					}
					else if ((this.CheckRow(single1 - this.bounceDistance - this._halfShipSize, single) ? false : !this.CheckRow(single1 - this.bounceDistanceClose - this._halfShipSize, single)))
					{
						flag = true;
						this.destinationZ = single1 - this.bounceDistance;
					}
				}
				if ((this.rotationY <= 180f ? false : this.rotationY < 270f))
				{
					if (!(this.CheckRow(single1 - this.bounceDistance - this._halfShipSize, single) ? true : this.CheckRow(single1 - this.bounceDistanceClose - this._halfShipSize, single)))
					{
						flag = true;
						this.destinationZ = single1 - this.bounceDistance;
					}
					else if ((this.CheckRow(single1 + this.bounceDistance + this._halfShipSize, single) ? false : !this.CheckRow(single1 + this.bounceDistanceClose + this._halfShipSize, single)))
					{
						flag = true;
						this.destinationZ = single1 + this.bounceDistance;
					}
				}
				if (!flag)
				{
					this.destinationX = this.x;
					this.destinationZ = this.z;
				}
				this.BounceFromCollision();
			}
			if ((this.CheckColumn(single + this._halfShipSize, single1) || this.CheckColumn(single + (this._halfShipSize - 0.5f), single1) ? true : this.CheckColumn(single + (this._halfShipSize - 1f), single1)))
			{
				flag = false;
				if ((this.CheckColumn(single - this.bounceDistance - this._halfShipSize, this.z) ? false : !this.CheckColumn(single - this.bounceDistanceClose - this._halfShipSize, this.z)))
				{
					flag = true;
					this.destinationX = single - this.bounceDistance;
					this.destinationZ = this.z;
				}
				if ((this.rotationY < 0f ? false : this.rotationY < 90f))
				{
					if (!(this.CheckRow(single1 + this.bounceDistance + this._halfShipSize, single) ? true : this.CheckRow(single1 + this.bounceDistanceClose + this._halfShipSize, single)))
					{
						flag = true;
						this.destinationZ = single1 + this.bounceDistance;
					}
					else if ((this.CheckRow(single1 - this.bounceDistance - this._halfShipSize, single) ? false : !this.CheckRow(single1 - this.bounceDistanceClose - this._halfShipSize, single)))
					{
						flag = true;
						this.destinationZ = single1 - this.bounceDistance;
					}
				}
				if ((this.rotationY <= 90f ? false : this.rotationY < 180f))
				{
					if (!(this.CheckRow(single1 - this.bounceDistance - this._halfShipSize, single) ? true : this.CheckRow(single1 - this.bounceDistanceClose - this._halfShipSize, single)))
					{
						flag = true;
						this.destinationZ = single1 - this.bounceDistance;
					}
					else if ((this.CheckRow(single1 + this.bounceDistance + this._halfShipSize, single) ? false : !this.CheckRow(single1 + this.bounceDistanceClose + this._halfShipSize, single)))
					{
						flag = true;
						this.destinationZ = single1 + this.bounceDistance;
					}
				}
				if (!flag)
				{
					this.destinationX = this.x;
					this.destinationZ = this.z;
				}
				this.BounceFromCollision();
			}
			if ((this.CheckRow(single1 - this._halfShipSize, single) || this.CheckRow(single1 - (this._halfShipSize - 0.5f), single) ? true : this.CheckRow(single1 - (this._halfShipSize - 1f), single)))
			{
				flag = false;
				if ((this.CheckRow(single1 + this.bounceDistance + this._halfShipSize, this.x) ? false : !this.CheckRow(single1 + this.bounceDistanceClose + this._halfShipSize, this.x)))
				{
					flag = true;
					this.destinationZ = single1 + this.bounceDistance;
					this.destinationX = this.x;
				}
				if ((this.rotationY <= 90f ? false : this.rotationY < 180f))
				{
					if (!(this.CheckColumn(single + this.bounceDistance + this._halfShipSize, single1) ? true : this.CheckColumn(single + this.bounceDistanceClose + this._halfShipSize, single1)))
					{
						flag = true;
						this.destinationX = single + this.bounceDistance;
					}
					else if ((this.CheckColumn(single - this.bounceDistance - this._halfShipSize, single1) ? false : !this.CheckColumn(single - this.bounceDistanceClose - this._halfShipSize, single1)))
					{
						flag = true;
						this.destinationX = single - this.bounceDistance;
					}
				}
				if ((this.rotationY <= 180f ? false : this.rotationY < 270f))
				{
					if (!(this.CheckColumn(single - this.bounceDistance - this._halfShipSize, single1) ? true : this.CheckColumn(single - this.bounceDistanceClose - this._halfShipSize, single1)))
					{
						flag = true;
						this.destinationX = single - this.bounceDistance;
					}
					else if ((this.CheckColumn(single + this.bounceDistance + this._halfShipSize, single1) ? false : !this.CheckColumn(single + this.bounceDistanceClose + this._halfShipSize, single1)))
					{
						flag = true;
						this.destinationX = single + this.bounceDistance;
					}
				}
				if (!flag)
				{
					this.destinationX = this.x;
					this.destinationZ = this.z;
				}
				this.BounceFromCollision();
			}
			if ((this.CheckRow(single1 + this._halfShipSize, single) || this.CheckRow(single1 + (this._halfShipSize - 0.5f), single) ? true : this.CheckRow(single1 + (this._halfShipSize - 1f), single)))
			{
				flag = false;
				if ((this.CheckRow(single1 - this.bounceDistance - this._halfShipSize, this.x) ? false : !this.CheckRow(single1 - this.bounceDistanceClose - this._halfShipSize, this.x)))
				{
					flag = true;
					this.destinationZ = single1 - this.bounceDistance;
					this.destinationX = this.x;
				}
				if ((this.rotationY <= 0f ? false : this.rotationY < 90f))
				{
					if (!(this.CheckColumn(single + this.bounceDistance + this._halfShipSize, single1) ? true : this.CheckColumn(single + this.bounceDistanceClose + this._halfShipSize, single1)))
					{
						flag = true;
						this.destinationX = single + this.bounceDistance;
					}
					else if ((this.CheckColumn(single - this.bounceDistance - this._halfShipSize, single1) ? false : !this.CheckColumn(single - this.bounceDistanceClose - this._halfShipSize, single1)))
					{
						flag = true;
						this.destinationX = single - this.bounceDistance;
					}
				}
				if ((this.rotationY <= 270f ? false : this.rotationY < 360f))
				{
					if (!(this.CheckColumn(single - this.bounceDistance - this._halfShipSize, single1) ? true : this.CheckColumn(single - this.bounceDistanceClose - this._halfShipSize, single1)))
					{
						flag = true;
						this.destinationX = single - this.bounceDistance;
					}
					else if ((this.CheckColumn(single + this.bounceDistance + this._halfShipSize, single1) ? false : !this.CheckColumn(single + this.bounceDistanceClose + this._halfShipSize, single1)))
					{
						flag = true;
						this.destinationX = single + this.bounceDistance;
					}
				}
				if (!flag)
				{
					this.destinationX = this.x;
					this.destinationZ = this.z;
				}
				this.BounceFromCollision();
			}
		}
	}

	private bool CheckColumn(float xEdge, float zCenter)
	{
		bool flag;
		List<int> nums = this.DetermineStepValues((float)this.galaxy.minZ, this.galaxy.collisionsMapStep, zCenter, this._halfShipSize, (int)this.galaxy.collisionsMap.Length);
		int num = (int)((xEdge - (float)this.galaxy.minX) / this.galaxy.collisionsMapStep);
		if ((num >= this.galaxy.collisionsMap[0].Length ? false : num >= 0))
		{
			foreach (int num1 in nums)
			{
				try
				{
					if (num1 <= (int)this.galaxy.collisionsMap.Length)
					{
						BitArray bitArrays = this.galaxy.collisionsMap[num1];
						if (bitArrays.Get(num))
						{
							flag = true;
							return flag;
						}
						else if (!(num + 1 >= this.galaxy.collisionsMap[0].Length ? true : !bitArrays.Get(num + 1)))
						{
							flag = true;
							return flag;
						}
						else if ((num - 1 <= 0 ? false : bitArrays.Get(num - 1)))
						{
							flag = true;
							return flag;
						}
					}
					else
					{
						flag = true;
						return flag;
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					GameObjectPhysics.Log(string.Concat("Error at checkColumn.Index : ", num1));
					GameObjectPhysics.Log(string.Concat("Error at checkColumn.Error : ", exception.ToString()));
					flag = false;
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

	public void CheckForLeveling()
	{
		if (StaticData.levelsType.ContainsKey(this.cfg.playerLevel + 1))
		{
			if (this.cfg.playerItems.GetAmountAt(PlayerItems.TypeExperience) >= StaticData.levelsType[this.cfg.playerLevel + 1].tottalExp)
			{
				ShipConfiguration shipConfiguration = this.cfg;
				shipConfiguration.playerLevel = (short)(shipConfiguration.playerLevel + 1);
				PlayerBelongings playerBelonging = this.playerData.playerBelongings;
				playerBelonging.playerLevel = (short)(playerBelonging.playerLevel + 1);
				if (this.LevelingUp != null)
				{
					this.LevelingUp(this);
				}
			}
		}
	}

	private bool CheckRow(float zEdge, float xCenter)
	{
		int num = 0;
		bool flag;
		List<int> nums = this.DetermineStepValues((float)this.galaxy.minX, this.galaxy.collisionsMapStep, xCenter, this._halfShipSize, this.galaxy.collisionsMap[0].Length);
		int num1 = (int)((zEdge - (float)this.galaxy.minZ) / this.galaxy.collisionsMapStep);
		if ((num1 >= (int)this.galaxy.collisionsMap.Length ? false : num1 >= 0))
		{
			BitArray bitArrays = this.galaxy.collisionsMap[num1];
			foreach (int n in nums)
			{
				if (bitArrays.Get(n))
				{
					flag = true;
					return flag;
				}
			}
			if (num1 + 1 < (int)this.galaxy.collisionsMap.Length)
			{
				BitArray bitArrays1 = this.galaxy.collisionsMap[num1 + 1];
				foreach (int num2 in nums)
				{
					if (bitArrays1.Get(num2))
					{
						flag = true;
						return flag;
					}
				}
			}
			if (num1 - 1 > 0)
			{
				BitArray bitArrays2 = this.galaxy.collisionsMap[num1 - 1];
				foreach (int num3 in nums)
				{
					if (bitArrays2.Get(num3))
					{
						flag = true;
						return flag;
					}
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

	public void CopyPropsTo(PlayerObjectPhysics copyTarget)
	{
		base.CopyPropsTo(copyTarget);
		copyTarget.isInControl = this.isInControl;
		copyTarget.teamNumber = this.teamNumber;
		copyTarget.playerName = this.playerName;
		copyTarget.angularVelocity = this.angularVelocity;
		copyTarget.isStunned = this.isStunned;
		copyTarget.isDisarmed = this.isDisarmed;
		copyTarget.isShocked = this.isShocked;
		copyTarget.destinationAngle = this.destinationAngle;
		copyTarget.destinationX = this.destinationX;
		copyTarget.destinationY = this.destinationY;
		copyTarget.destinationZ = this.destinationZ;
		copyTarget.distanceToStop = this.distanceToStop;
		copyTarget.isAlive = this.isAlive;
		copyTarget.isShooting = this.isShooting;
		copyTarget.moveState = this.moveState;
		copyTarget.fractionId = this.fractionId;
		copyTarget.neededRotation = this.neededRotation;
		copyTarget.playerId = this.playerId;
		copyTarget.rotationDone = this.rotationDone;
		copyTarget.rotationState = this.rotationState;
		copyTarget.rotationX = this.rotationX;
		copyTarget.rotationY = this.rotationY;
		copyTarget.speed = this.speed;
		copyTarget.lastVisitedBase = this.lastVisitedBase;
		copyTarget.enteringBaseDoor = this.enteringBaseDoor;
		copyTarget.enteringBaseState = this.enteringBaseState;
		copyTarget.isInParty = this.isInParty;
		copyTarget.isSlowedFromAmmo = this.isSlowedFromAmmo;
	}

	public override void Deserialize(BinaryReader br)
	{
		this.playerAvatarUrl = br.ReadString();
		this.teamNumber = br.ReadByte();
		this.playerName = br.ReadString();
		this.guildTag = br.ReadString();
		this.shipName = br.ReadString();
		this.playerId = br.ReadInt64();
		this.isAlive = br.ReadBoolean();
		this.isShooting = br.ReadBoolean();
		this.moveState = br.ReadByte();
		this.fractionId = br.ReadByte();
		this.destinationX = br.ReadSingle();
		this.destinationY = br.ReadSingle();
		this.destinationZ = br.ReadSingle();
		this.distanceToStop = br.ReadSingle();
		this.speed = br.ReadSingle();
		this.rotationState = br.ReadInt32();
		this.rotationX = br.ReadSingle();
		this.rotationY = br.ReadSingle();
		this.rotationZ = br.ReadSingle();
		this.angularVelocity = br.ReadSingle();
		this.rotationDone = br.ReadSingle();
		this.neededRotation = br.ReadSingle();
		this.destinationAngle = br.ReadSingle();
		this.miningMineralNbId = br.ReadUInt32();
		this.timeToFinishMining = br.ReadSingle();
		this.miningState = br.ReadByte();
		this.enteringBaseState = br.ReadByte();
		this.didOpenDoor = br.ReadBoolean();
		this.enteringBaseDoor = br.ReadByte();
		this.enteringBaseId = br.ReadInt32();
		this.lastVisitedBase = br.ReadInt32();
		long num = br.ReadInt64();
		this.timeOfLastCombat = StaticData.now.AddMilliseconds((double)(-num));
		num = br.ReadInt64();
		this.timeOfSlowEnding = StaticData.now.AddMilliseconds((double)(-num));
		this.isSlowedFromAmmo = br.ReadBoolean();
		base.Deserialize(br);
		this.cfg = (ShipConfiguration)TransferablesFramework.DeserializeITransferable(br);
		this.isImmuneToCrowd = br.ReadBoolean();
		this.isStunned = br.ReadBoolean();
		this.isDisarmed = br.ReadBoolean();
		this.isShocked = br.ReadBoolean();
		this.isShortCircuitCaster = br.ReadBoolean();
		this.isInStealthMode = br.ReadBoolean();
		this.isInParty = br.ReadBoolean();
		this.isInControl = br.ReadBoolean();
		this.isSpeedBoostActivated = br.ReadBoolean();
		this.playerLeague = (PvPLeague)br.ReadByte();
		this.inPvPRank = br.ReadBoolean();
		this.isGuest = br.ReadBoolean();
		this.pvpState = (PvPPlayerState)br.ReadByte();
		this.selectedPoPnbId = br.ReadUInt32();
	}

	private List<int> DetermineStepValues(float minMapCoordinate, float step, float checkCenter, float halfSize, int maxIndex)
	{
		List<int> nums;
		List<int> nums1 = new List<int>();
		float single = checkCenter - halfSize;
		float single1 = checkCenter + halfSize;
		float single2 = (single - minMapCoordinate) / step;
		int num = (int)single2;
		if (single2 > 0.03f + (float)num)
		{
			num++;
		}
		while (true)
		{
			if (num < 0)
			{
				num++;
			}
			else if (num >= maxIndex)
			{
				nums = nums1;
				break;
			}
			else if ((float)num * step + minMapCoordinate <= single1)
			{
				int num1 = num;
				num = num1 + 1;
				nums1.Add(num1);
			}
			else
			{
				nums = nums1;
				break;
			}
		}
		return nums;
	}

	public ProjectileObject[] Fusillade(Action<ProjectileObject> CreateProjectileObjCallback)
	{
		ProjectileObject[] array;
		bool flag;
		List<ProjectileObject> projectileObjects = new List<ProjectileObject>();
		if (this.shootingAt == null)
		{
			array = projectileObjects.ToArray();
		}
		else if (this.CanShootThisTarget(this.shootingAt))
		{
			if (this.isStunned || this.isDisarmed || this.shootingAt.neighbourhoodId == this.neighbourhoodId)
			{
				flag = false;
			}
			else
			{
				flag = (!this.shootingAt.IsPoP ? true : !((PlayerObjectPhysics)this.shootingAt).isInStealthMode);
			}
			if (flag)
			{
				int num = -1;
				WeaponSlot[] weaponSlotArray = this.cfg.weaponSlots;
				for (int i = 0; i < (int)weaponSlotArray.Length; i++)
				{
					WeaponSlot weaponSlot = weaponSlotArray[i];
					num++;
					if ((weaponSlot.WeaponStatus == EWeaponStatus.NotSupported || weaponSlot.WeaponStatus == EWeaponStatus.NoWeapon || weaponSlot.WeaponStatus == EWeaponStatus.OutOfAmmo ? false : weaponSlot.WeaponStatus != EWeaponStatus.WeaponOff))
					{
						if (weaponSlot.isActive)
						{
							long ticks = StaticData.now.Ticks;
							weaponSlot.realReloadTime = Math.Max(weaponSlot.realReloadTime, PlayerObjectPhysics.MIN_COOLDOWN_TIME);
							if (weaponSlot.lastShotTime + weaponSlot.realReloadTime <= StaticData.now.Ticks)
							{
								if (this.shootingAt != null)
								{
									if (GameObjectPhysics.GetDistance(this.shootingAt.x, this.x, this.shootingAt.z, this.z) > weaponSlot.totalShootRange)
									{
										goto Label1;
									}
									if (this.cfg.playerItems.GetAmmoQty(weaponSlot.selectedAmmoItemType) > (long)0)
									{
										weaponSlot.lastShotTime = ticks;
										this.cfg.playerItems.SpendAmmo(weaponSlot.selectedAmmoItemType);
										if ((!this.IsPve ? true : ((PvEPhysics)this).DecisionTaken != null))
										{
											ProjectileObject projectileObject = this.StartProjectile(weaponSlot, ticks, num, CreateProjectileObjCallback);
											projectileObject.weaponSlotId = weaponSlot.slotId;
											projectileObject.selectedAmmoType = weaponSlot.selectedAmmoItemType;
											projectileObjects.Add(projectileObject);
											this.timeOfLastCombat = StaticData.now;
											if (!this.IsPve)
											{
												this.CalculateRealDamage(weaponSlot, out projectileObject.damageHull, out projectileObject.damageShield);
											}
											else
											{
												PvEPhysics pvEPhysic = (PvEPhysics)this;
												projectileObject.damageHull = (ushort)((float)pvEPhysic.typeData.damage * ((float)pvEPhysic.typeData.penetration / 100f));
												projectileObject.damageShield = (ushort)(pvEPhysic.typeData.damage - projectileObject.damageHull);
											}
										}
										else
										{
											array = new ProjectileObject[0];
											return array;
										}
									}
								}
								else
								{
									array = new ProjectileObject[0];
									return array;
								}
							}
						}
					}
                    Label1:;
				}
				array = projectileObjects.ToArray();
			}
			else
			{
				array = projectileObjects.ToArray();
			}
		}
		else
		{
			array = projectileObjects.ToArray();
		}
		return array;
	}

	protected virtual ActiveSkillObject IDontKnowWhatImDoing(ushort skillType, object aBearInTheWoods)
	{
		ActiveSkillObject activeSkillObject = new ActiveSkillObject();
		this.playerData.playerBelongings.playerItems.GetSkillEffect(skillType, out activeSkillObject.x_value, out activeSkillObject.y_value);
		activeSkillObject.skillId = skillType;
		activeSkillObject.caster = this;
		return activeSkillObject;
	}

	public bool IsMiningInRange(Mineral mineral)
	{
		float distance = GameObjectPhysics.GetDistance(mineral.x, this.x, mineral.z, this.z);
		return distance <= PlayerObjectPhysics.miningRange;
	}

	private void ManageCriticalEnergy(float dt)
	{
		if ((this.cfg.criticalEnergy <= 0f ? false : !this.IsInCombat))
		{
			ShipConfiguration shipConfiguration = this.cfg;
			shipConfiguration.criticalEnergy = shipConfiguration.criticalEnergy - dt * this.cfg.criticalEnergyDrop;
			if (this.cfg.criticalEnergy < 0f)
			{
				this.cfg.criticalEnergy = 0f;
			}
		}
	}

	public void ManageRotation(float dt)
	{
		if (this.rotationState != 0)
		{
			if (this.rotationState == 1)
			{
				float single = dt * this.angularVelocity;
				this.AddRotationY(single);
				this.rotationDone += Math.Abs(single);
				if (this.rotationDone >= Math.Abs(this.neededRotation))
				{
					float single1 = this.rotationY;
					float single2 = this.destinationAngle - single1;
					if (single2 >= 360f)
					{
						single2 -= 360f;
					}
					this.AddRotationY(single2);
					this.rotationState = 0;
					this.rotationDone = 0f;
				}
			}
		}
	}

	private void ManageShield(float dt)
	{
		if (((float)this.cfg.shieldMax <= this.cfg.shield ? false : !this.IsInCombat))
		{
			ShipConfiguration shipConfiguration = this.cfg;
			shipConfiguration.shield = shipConfiguration.shield + dt * (PlayerObjectPhysics.SHIELD_RESTORE_RATE + this.cfg.shieldRepairPerSec);
			if (this.cfg.shield > (float)this.cfg.shieldMax)
			{
				this.cfg.shield = (float)this.cfg.shieldMax;
			}
			if ((this.IsPve || this.playerData == null ? false : this.playerData.playerBelongings != null))
			{
				this.playerData.playerBelongings.SelectedShip.ShieldHP = (int)this.cfg.shield;
			}
		}
	}

	private void ManageSpeedBoostConsumption(float dt)
	{
		if (this.cfg.shield <= 0f)
		{
			this.isSpeedBoostActivated = false;
		}
		else
		{
			this.timeOfLastCombat = StaticData.now;
			ShipConfiguration shipConfiguration = this.cfg;
			shipConfiguration.shield = shipConfiguration.shield - dt * this.cfg.speedBoostConsumption;
			if (this.cfg.shield <= 0f)
			{
				this.cfg.shield = 0f;
				this.isSpeedBoostActivated = false;
			}
		}
		if ((this.IsPve || this.playerData == null ? false : this.playerData.playerBelongings != null))
		{
			this.playerData.playerBelongings.SelectedShip.ShieldHP = (int)this.cfg.shield;
		}
	}

	public override void Serialize(BinaryWriter bw)
	{
		TimeSpan timeSpan;
		bw.Write(this.playerName ?? "");
		bw.Write(this.guildTag ?? "");
		bw.Write(this.shipName ?? "");
		bw.Write(this.playerId);
		bw.Write(this.isAlive);
		bw.Write(this.isShooting);
		bw.Write(this.moveState);
		bw.Write(this.fractionId);
		bw.Write(this.destinationX);
		bw.Write(this.destinationY);
		bw.Write(this.destinationZ);
		bw.Write(this.distanceToStop);
		bw.Write(this.speed);
		bw.Write(this.rotationState);
		bw.Write(this.rotationX);
		bw.Write(this.rotationY);
		bw.Write(this.rotationZ);
		bw.Write(this.angularVelocity);
		bw.Write(this.rotationDone);
		bw.Write(this.neededRotation);
		bw.Write(this.destinationAngle);
		bw.Write(this.miningMineralNbId);
		bw.Write(this.timeToFinishMining);
		bw.Write(this.miningState);
		bw.Write(this.enteringBaseState);
		bw.Write(this.didOpenDoor);
		bw.Write(this.enteringBaseDoor);
		bw.Write(this.enteringBaseId);
		bw.Write(this.lastVisitedBase);
		if (!(this.timeOfLastCombat == DateTime.MinValue))
		{
			timeSpan = StaticData.now - this.timeOfLastCombat;
			bw.Write((long)timeSpan.TotalMilliseconds);
		}
		else
		{
			bw.Write((long)10000);
		}
		if (!(this.timeOfSlowEnding == DateTime.MinValue))
		{
			timeSpan = StaticData.now - this.timeOfSlowEnding;
			bw.Write((long)timeSpan.TotalMilliseconds);
		}
		else
		{
			bw.Write((long)10000);
		}
		bw.Write(this.isSlowedFromAmmo);
		base.Serialize(bw);
		TransferablesFramework.SerializeITransferable(bw, this.cfg, TransferContext.None);
		bw.Write(this.isImmuneToCrowd);
		bw.Write(this.isStunned);
		bw.Write(this.isDisarmed);
		bw.Write(this.isShocked);
		bw.Write(this.isShortCircuitCaster);
		bw.Write(this.isInStealthMode);
		bw.Write(this.isInParty);
		bw.Write(this.isInControl);
		bw.Write(this.isSpeedBoostActivated);
		bw.Write((byte)this.playerLeague);
		bw.Write(this.inPvPRank);
		bw.Write(this.isGuest);
		bw.Write(this.selectedPoPnbId);
	}

	public ActiveSkillObject StartActiveSkill(ActiveSkillSlot slot, Action<ActiveSkillObject> CreateActiveSkillObjCallback)
	{
		ActiveSkillObject xValue;
		ActiveSkillObject activeSkillObject;
		Location location;
		ActiveSkillObject activeSkillObject1;
		if (slot.skillId == PlayerItems.TypeTalentsSunderArmor)
		{
			this.timeOfLastCombat = StaticData.now;
			xValue = this.IDontKnowWhatImDoing(PlayerItems.TypeTalentsSunderArmor, null);
			xValue.x = this.castingAt.x;
			xValue.y = this.castingAt.y;
			xValue.z = this.castingAt.z;
			xValue.target = this.castingAt;
			xValue.timeBetwenIteration = 0;
			xValue.castTime = StaticData.now;
			xValue.duration = 10000;
			xValue.animationLifetime = 2300;
			xValue.endTime = xValue.castTime.AddMilliseconds((double)xValue.duration);
			xValue.iterationTime = xValue.castTime.AddMilliseconds(666);
			xValue.assetName = "SunderArmor";
			CreateActiveSkillObjCallback(xValue);
			this.castingAt.AddActivatedSkill(xValue);
			activeSkillObject1 = xValue;
		}
		else if (slot.skillId == PlayerItems.TypeTalentsTaunt)
		{
			xValue = this.IDontKnowWhatImDoing(PlayerItems.TypeTalentsTaunt, null);
			xValue.x = this.x;
			xValue.y = this.y;
			xValue.z = this.z;
			xValue.target = this;
			xValue.timeBetwenIteration = 0;
			xValue.assetName = "Taunt";
			xValue.animationLifetime = 1300;
			CreateActiveSkillObjCallback(xValue);
			base.AddActivatedSkill(xValue);
			activeSkillObject1 = xValue;
		}
		else if (slot.skillId == PlayerItems.TypeTalentsFocusFire)
		{
			this.timeOfLastCombat = StaticData.now;
			xValue = this.IDontKnowWhatImDoing(PlayerItems.TypeTalentsFocusFire, null);
			xValue.x = this.x;
			xValue.y = this.y;
			xValue.z = this.z;
			xValue.target = this.castingAt;
			xValue.assetName = "FocusFire";
			CreateActiveSkillObjCallback(xValue);
			this.castingAt.AddActivatedSkill(xValue);
			activeSkillObject1 = xValue;
		}
		else if (slot.skillId == PlayerItems.TypeTalentsDecoy)
		{
			this.timeOfLastCombat = StaticData.now;
			xValue = this.IDontKnowWhatImDoing(PlayerItems.TypeTalentsDecoy, null);
			xValue.x = this.x;
			xValue.y = this.y;
			xValue.z = this.z;
			xValue.target = this.castingAt;
			xValue.assetName = "Decoy";
			CreateActiveSkillObjCallback(xValue);
			this.castingAt.AddActivatedSkill(xValue);
			activeSkillObject1 = xValue;
		}
		else if (slot.skillId == PlayerItems.TypeTalentsRocketBarrage)
		{
			this.timeOfLastCombat = StaticData.now;
			xValue = this.IDontKnowWhatImDoing(PlayerItems.TypeTalentsRocketBarrage, null);
			((Location)this.castingAt).radius = 7f;
			xValue.x = this.castingAt.x;
			xValue.y = 0f;
			xValue.z = this.castingAt.z;
			xValue.targetLocation = (Location)this.castingAt;
			xValue.timeBetwenIteration = 2000;
			xValue.castTime = StaticData.now;
			xValue.duration = xValue.x_value * 1000;
			xValue.animationLifetime = xValue.duration;
			xValue.endTime = xValue.castTime.AddMilliseconds((double)xValue.duration);
			xValue.iterationTime = xValue.castTime.AddMilliseconds(1000);
			xValue.assetName = "RocketBarrage";
			CreateActiveSkillObjCallback(xValue);
			activeSkillObject1 = xValue;
		}
		else if (slot.skillId == PlayerItems.TypeTalentsShieldFortress)
		{
			xValue = this.IDontKnowWhatImDoing(PlayerItems.TypeTalentsShieldFortress, null);
			xValue.x = this.x;
			xValue.y = this.y;
			xValue.z = this.z;
			xValue.target = this;
			xValue.timeBetwenIteration = 0;
			xValue.castTime = StaticData.now;
			xValue.duration = (xValue.x_value + xValue.y_value) * 1000;
			xValue.animationLifetime = (xValue.duration > 1230 ? xValue.duration : 1230);
			xValue.endTime = xValue.castTime.AddMilliseconds((double)xValue.duration);
			xValue.iterationTime = xValue.castTime;
			xValue.assetName = "ShieldFortress";
			CreateActiveSkillObjCallback(xValue);
			base.AddActivatedSkill(xValue);
			activeSkillObject1 = xValue;
		}
		else if (slot.skillId == PlayerItems.TypeTalentsPowerBreak)
		{
			this.timeOfLastCombat = StaticData.now;
			xValue = this.IDontKnowWhatImDoing(PlayerItems.TypeTalentsPowerBreak, null);
			xValue.x = this.castingAt.x;
			xValue.y = this.castingAt.y;
			xValue.z = this.castingAt.z;
			xValue.target = this.castingAt;
			xValue.castTime = StaticData.now;
			xValue.duration = xValue.x_value * 1000;
			xValue.animationLifetime = 1500;
			xValue.endTime = xValue.castTime.AddMilliseconds((double)xValue.duration);
			xValue.iterationTime = xValue.castTime;
			xValue.assetName = "PowerBreak";
			CreateActiveSkillObjCallback(xValue);
			this.castingAt.AddActivatedSkill(xValue);
			activeSkillObject1 = xValue;
		}
		else if (slot.skillId == PlayerItems.TypeTalentsLightSpeed)
		{
			xValue = this.IDontKnowWhatImDoing(PlayerItems.TypeTalentsLightSpeed, null);
			xValue.x = this.x;
			xValue.y = this.y;
			xValue.z = this.z;
			xValue.target = this;
			xValue.castTime = StaticData.now;
			xValue.duration = xValue.y_value * 1000;
			xValue.animationLifetime = 2000;
			xValue.endTime = xValue.castTime.AddMilliseconds((double)xValue.duration);
			xValue.iterationTime = xValue.castTime;
			xValue.assetName = "LightSpeed";
			CreateActiveSkillObjCallback(xValue);
			base.AddActivatedSkill(xValue);
			activeSkillObject1 = xValue;
		}
		else if (slot.skillId == PlayerItems.TypeTalentsMistShroud)
		{
			xValue = this.IDontKnowWhatImDoing(PlayerItems.TypeTalentsMistShroud, null);
			xValue.x = this.x;
			xValue.y = this.y;
			xValue.z = this.z;
			xValue.target = this;
			xValue.timeBetwenIteration = 0;
			xValue.castTime = StaticData.now;
			xValue.duration = xValue.y_value * 1000;
			xValue.animationLifetime = xValue.duration;
			xValue.endTime = xValue.castTime.AddMilliseconds((double)xValue.duration);
			xValue.iterationTime = xValue.castTime;
			xValue.assetName = "MistShroud";
			CreateActiveSkillObjCallback(xValue);
			base.AddActivatedSkill(xValue);
			activeSkillObject1 = xValue;
		}
		else if (slot.skillId == PlayerItems.TypeTalentsLaserDestruction)
		{
			this.timeOfLastCombat = StaticData.now;
			xValue = this.IDontKnowWhatImDoing(PlayerItems.TypeTalentsLaserDestruction, null);
			xValue.x = this.castingAt.x;
			xValue.y = this.castingAt.y;
			xValue.z = this.castingAt.z;
			xValue.target = this.castingAt;
			xValue.castTime = StaticData.now;
			xValue.assetName = "LaserDestruction";
			xValue.animationLifetime = 1200;
			CreateActiveSkillObjCallback(xValue);
			activeSkillObject1 = xValue;
		}
		else if (slot.skillId == PlayerItems.TypeTalentsPulseNova)
		{
			this.timeOfLastCombat = StaticData.now;
			xValue = this.IDontKnowWhatImDoing(PlayerItems.TypeTalentsPulseNova, null);
			((Location)this.castingAt).radius = 10f;
			xValue.x = this.castingAt.x;
			xValue.y = 0f;
			xValue.z = this.castingAt.z;
			xValue.targetLocation = (Location)this.castingAt;
			xValue.assetName = "PulseNova";
			xValue.animationLifetime = 900;
			CreateActiveSkillObjCallback(xValue);
			activeSkillObject1 = xValue;
		}
		else if (slot.skillId == PlayerItems.TypeTalentsRemedy)
		{
			this.timeOfLastCombat = StaticData.now;
			xValue = this.IDontKnowWhatImDoing(PlayerItems.TypeTalentsRemedy, null);
			xValue.x = this.castingAt.x;
			xValue.y = this.castingAt.y;
			xValue.z = this.castingAt.z;
			xValue.target = this.castingAt;
			xValue.timeBetwenIteration = 0;
			xValue.castTime = StaticData.now;
			xValue.duration = xValue.y_value * 1000;
			xValue.endTime = xValue.castTime.AddMilliseconds((double)xValue.duration);
			xValue.iterationTime = xValue.castTime;
			xValue.assetName = "Remedy";
			xValue.animationLifetime = 3000;
			CreateActiveSkillObjCallback(xValue);
			this.castingAt.AddActivatedSkill(xValue);
			activeSkillObject1 = xValue;
		}
		else if (slot.skillId == PlayerItems.TypeTalentsUnstoppable)
		{
			this.timeOfLastCombat = StaticData.now;
			xValue = this.IDontKnowWhatImDoing(PlayerItems.TypeTalentsUnstoppable, null);
			xValue.x = this.x;
			xValue.y = this.y;
			xValue.z = this.z;
			xValue.target = this;
			xValue.castTime = StaticData.now;
			xValue.duration = (xValue.x_value + xValue.y_value) * 1000;
			xValue.animationLifetime = 1000;
			xValue.endTime = xValue.castTime.AddMilliseconds((double)xValue.duration);
			xValue.iterationTime = xValue.castTime;
			xValue.assetName = "Unstoppable";
			CreateActiveSkillObjCallback(xValue);
			base.AddActivatedSkill(xValue);
			activeSkillObject1 = xValue;
		}
		else if (slot.skillId == PlayerItems.TypeTalentsStealth)
		{
			xValue = this.IDontKnowWhatImDoing(PlayerItems.TypeTalentsStealth, null);
			xValue.x = this.x;
			xValue.y = this.y;
			xValue.z = this.z;
			xValue.target = this;
			xValue.castTime = StaticData.now;
			xValue.duration = (xValue.x_value + xValue.y_value) * 1000;
			xValue.endTime = xValue.castTime.AddMilliseconds((double)xValue.duration);
			xValue.iterationTime = xValue.castTime;
			xValue.assetName = "Stealth";
			activeSkillObject = (
				from s in (IEnumerable<ActiveSkillObject>)((PlayerObjectPhysics)this.castingAt).activatedSkillsSafe
				where s.skillId == PlayerItems.TypeTalentsStealth
				select s).FirstOrDefault<ActiveSkillObject>();
			if (activeSkillObject == null)
			{
				CreateActiveSkillObjCallback(xValue);
				this.castingAt.AddActivatedSkill(xValue);
			}
			else
			{
				activeSkillObject.iterationTime = (activeSkillObject.iterationTime != activeSkillObject.castTime ? xValue.endTime : xValue.castTime);
				activeSkillObject.castTime = xValue.castTime;
				activeSkillObject.endTime = xValue.endTime;
				xValue.neighbourhoodId = activeSkillObject.neighbourhoodId;
			}
			activeSkillObject1 = xValue;
		}
		else if (slot.skillId == PlayerItems.TypeTalentsNanoStorm)
		{
			this.timeOfLastCombat = StaticData.now;
			xValue = this.IDontKnowWhatImDoing(PlayerItems.TypeTalentsNanoStorm, null);
			location = new Location()
			{
				radius = 10f
			};
			xValue.targetLocation = location;
			xValue.target = this.castingAt;
			xValue.x = this.castingAt.x;
			xValue.y = this.castingAt.y;
			xValue.z = this.castingAt.z;
			xValue.timeBetwenIteration = 1000;
			xValue.castTime = StaticData.now;
			xValue.duration = 5000;
			xValue.animationLifetime = xValue.duration;
			xValue.endTime = xValue.castTime.AddMilliseconds((double)xValue.duration);
			xValue.iterationTime = xValue.castTime;
			xValue.assetName = "NanoStorm";
			CreateActiveSkillObjCallback(xValue);
			this.castingAt.AddActivatedSkill(xValue);
			activeSkillObject1 = xValue;
		}
		else if (slot.skillId == PlayerItems.TypeTalentsPowerCut)
		{
			this.timeOfLastCombat = StaticData.now;
			xValue = this.IDontKnowWhatImDoing(PlayerItems.TypeTalentsPowerCut, null);
			xValue.x = this.x;
			xValue.y = this.y;
			xValue.z = this.z;
			xValue.target = this.castingAt;
			xValue.duration = 5000;
			xValue.assetName = "PowerCut";
			this.castingAt.AddActivatedSkill(xValue);
			CreateActiveSkillObjCallback(xValue);
			activeSkillObject1 = xValue;
		}
		else if (slot.skillId == PlayerItems.TypeTalentsForceWave)
		{
			this.timeOfLastCombat = StaticData.now;
			xValue = this.IDontKnowWhatImDoing(PlayerItems.TypeTalentsForceWave, null);
			location = new Location();
			((Location)this.castingAt).radius = 5f;
			xValue.x = this.castingAt.x;
			xValue.y = 0f;
			xValue.z = this.castingAt.z;
			xValue.targetLocation = (Location)this.castingAt;
			xValue.x = this.x;
			xValue.y = this.y;
			xValue.z = this.z;
			xValue.castTime = StaticData.now;
			xValue.duration = xValue.x_value * 1000;
			xValue.endTime = xValue.castTime.AddMilliseconds((double)xValue.duration);
			xValue.iterationTime = xValue.castTime;
			xValue.assetName = "ForceWave";
			CreateActiveSkillObjCallback(xValue);
			activeSkillObject1 = xValue;
		}
		else if (slot.skillId == PlayerItems.TypeTalentsRepairingDrones)
		{
			this.timeOfLastCombat = StaticData.now;
			xValue = this.IDontKnowWhatImDoing(PlayerItems.TypeTalentsRepairingDrones, null);
			xValue.x = this.castingAt.x;
			xValue.y = this.castingAt.y;
			xValue.z = this.castingAt.z;
			xValue.target = this.castingAt;
			xValue.timeBetwenIteration = 2000;
			xValue.castTime = StaticData.now;
			if (!this.IsPve)
			{
				xValue.duration = 12000;
			}
			xValue.animationLifetime = xValue.duration;
			xValue.endTime = xValue.castTime.AddMilliseconds((double)xValue.duration);
			xValue.iterationTime = xValue.castTime.AddMilliseconds(1000);
			xValue.assetName = "RepairingDrones";
			CreateActiveSkillObjCallback(xValue);
			this.castingAt.AddActivatedSkill(xValue);
			activeSkillObject1 = xValue;
		}
		else if (slot.skillId == PlayerItems.TypeTalentsNanoShield)
		{
			xValue = this.IDontKnowWhatImDoing(PlayerItems.TypeTalentsNanoShield, null);
			xValue.x = this.castingAt.x;
			xValue.y = this.castingAt.y;
			xValue.z = this.castingAt.z;
			xValue.duration = 1000;
			xValue.animationLifetime = xValue.duration;
			xValue.target = this.castingAt;
			xValue.originalValue = (float)(this.cfg.skillDamage * (xValue.x_value + xValue.y_value)) / 100f;
			xValue.assetName = "NanoShield";
			activeSkillObject = (
				from s in (IEnumerable<ActiveSkillObject>)this.castingAt.activatedSkillsSafe
				where s.skillId == PlayerItems.TypeTalentsNanoShield
				select s).FirstOrDefault<ActiveSkillObject>();
			if (activeSkillObject == null)
			{
				CreateActiveSkillObjCallback(xValue);
				this.castingAt.AddActivatedSkill(xValue);
			}
			else
			{
				activeSkillObject.originalValue = xValue.originalValue;
				xValue.neighbourhoodId = activeSkillObject.neighbourhoodId;
			}
			activeSkillObject1 = xValue;
		}
		else if (slot.skillId == PlayerItems.TypeTalentsRepairField)
		{
			this.timeOfLastCombat = StaticData.now;
			xValue = this.IDontKnowWhatImDoing(PlayerItems.TypeTalentsRepairField, null);
			location = new Location()
			{
				radius = 10f
			};
			if ((!this.IsPve ? false : !xValue.caster.nbPVEs.ContainsKey(this.neighbourhoodId)))
			{
				xValue.caster.nbPVEs.Add(this.neighbourhoodId, (PvEPhysics)this);
			}
			xValue.targetLocation = location;
			xValue.target = this.castingAt;
			xValue.x = this.castingAt.x;
			xValue.y = this.castingAt.y;
			xValue.z = this.castingAt.z;
			xValue.castTime = StaticData.now;
			xValue.animationLifetime = 2100;
			xValue.endTime = xValue.castTime.AddMilliseconds((double)xValue.duration);
			xValue.iterationTime = xValue.castTime;
			xValue.assetName = "RepairField";
			CreateActiveSkillObjCallback(xValue);
			activeSkillObject1 = xValue;
		}
		else if (slot.skillId == PlayerItems.TypeTalentsShortCircuit)
		{
			this.timeOfLastCombat = StaticData.now;
			xValue = this.IDontKnowWhatImDoing(PlayerItems.TypeTalentsShortCircuit, null);
			xValue.x = this.castingAt.x;
			xValue.y = this.castingAt.y;
			xValue.z = this.castingAt.z;
			xValue.target = this.castingAt;
			xValue.timeBetwenIteration = 1000;
			xValue.castTime = StaticData.now;
			xValue.animationLifetime = 1350;
			xValue.duration = 5000;
			xValue.endTime = xValue.castTime.AddMilliseconds((double)xValue.duration);
			xValue.iterationTime = xValue.castTime;
			xValue.assetName = "ShortCircuit";
			CreateActiveSkillObjCallback(xValue);
			this.castingAt.AddActivatedSkill(xValue);
			xValue.caster.isShortCircuitCaster = true;
			xValue.caster.shortCircuitTarget = xValue.target;
			activeSkillObject1 = xValue;
		}
		else if (slot.skillId == PlayerItems.TypeCouncilSkillDisarm)
		{
			xValue = this.IDontKnowWhatImDoing(PlayerItems.TypeCouncilSkillDisarm, null);
			xValue.x = this.x;
			xValue.y = this.y;
			xValue.z = this.z;
			xValue.target = this;
			xValue.assetName = "Disarm";
			xValue.animationLifetime = 1000;
			CreateActiveSkillObjCallback(xValue);
			base.AddActivatedSkill(xValue);
			activeSkillObject1 = xValue;
		}
		else if (slot.skillId == PlayerItems.TypeCouncilSkillSacrifice)
		{
			this.timeOfLastCombat = StaticData.now;
			xValue = this.IDontKnowWhatImDoing(PlayerItems.TypeCouncilSkillSacrifice, null);
			xValue.x = this.x;
			xValue.y = this.y;
			xValue.z = this.z;
			xValue.target = this;
			xValue.assetName = "Sacrifice";
			xValue.animationLifetime = 1000;
			CreateActiveSkillObjCallback(xValue);
			base.AddActivatedSkill(xValue);
			activeSkillObject1 = xValue;
		}
		else if (slot.skillId != PlayerItems.TypeCouncilSkillLifesteal)
		{
			activeSkillObject1 = null;
		}
		else
		{
			this.timeOfLastCombat = StaticData.now;
			xValue = this.IDontKnowWhatImDoing(PlayerItems.TypeCouncilSkillLifesteal, null);
			xValue.x = this.castingAt.x;
			xValue.y = this.castingAt.y;
			xValue.z = this.castingAt.z;
			xValue.target = this.castingAt;
			xValue.castTime = StaticData.now;
			xValue.assetName = "Lifesteal";
			xValue.animationLifetime = 1200;
			CreateActiveSkillObjCallback(xValue);
			activeSkillObject1 = xValue;
		}
		return activeSkillObject1;
	}

	public void StartEnterBase(StarBaseNet basse, byte door)
	{
		if ((this.moveState == 11 ? false : this.moveState != 10))
		{
			this.isInStealthMode = false;
			base.ClearActivatedSkills();
			this.ChangeSpeed(this.cfg.velocityMax, false);
			this.enteringBaseId = basse.id;
			this.enteringBase = basse;
			this.enteringBaseDoor = door;
			this.isShooting = false;
			this.shootingAt = null;
			this.moveState = 10;
			this.enteringBaseState = 4;
			this.destinationZ = this.z + 10f;
			this.destinationX = (float)(Math.Sign(this.enteringBase.x + this.enteringBase.doorX[this.enteringBaseDoor] - this.x) * 100) + this.x;
			this.destinationY = this.y;
			this.StartRotate();
		}
	}

	public void StartExitBase()
	{
		this.isAlive = true;
		this.moveState = 12;
		this.x = this.enteringBase.x;
		this.y = 0f;
		this.z = this.enteringBase.z - 30f;
		this.destinationAngle = 180f;
		this.enteringBase = null;
	}

	public void StartHyperJump(HyperJumpNet hj)
	{
		this.moveState = 14;
		if (this.isOnClientSide)
		{
			this.destinationX = hj.x;
			this.destinationZ = hj.z;
			this.cfg.currentVelocity = 600f;
			this.cfg.acceleration = 30f;
			this.cfg.maxRotationSpeed = 200f;
			this.StartRotate();
			this.StartMove();
		}
	}

	public void StartMove()
	{
		if ((!this.IsPve ? true : ((PvEPhysics)this).minPlayerDistanceRange >= 10f))
		{
			float distance = GameObjectPhysics.GetDistance(this.x, this.destinationX, this.z, this.destinationZ);
			if (distance >= 7f)
			{
				this.cfg.distanceToStartDecelerate = 5f;
			}
			else
			{
				this.cfg.distanceToStartDecelerate = Math.Max(distance - 2.5f, 1.5f);
			}
			this.moveState = 1;
		}
		else
		{
			this.moveState = 1;
		}
	}

	private ProjectileObject StartProjectile(WeaponSlot ws, long now, int weaponSlotIndex, Action<ProjectileObject> CreateProjectileObjCallback)
	{
		LaserMovingObject laserMovingObject;
		BulletObject bulletObject;
		ProjectileObject projectileObject;
		string item = StaticData.allTypes[ws.weaponTierId].assetName;
		if (item == "WeapLaserT1")
		{
			laserMovingObject = new LaserMovingObject()
			{
				galaxy = this.galaxy,
				assetName = item,
				shooter = this,
				target = this.shootingAt,
				x = this.x,
				y = this.y,
				z = this.z - 2f + (float)weaponSlotIndex * 0.9f,
				speed = 40f
			};
			CreateProjectileObjCallback(laserMovingObject);
			projectileObject = laserMovingObject;
		}
		else if (item == "WeapLaserT2")
		{
			laserMovingObject = new LaserMovingObject()
			{
				galaxy = this.galaxy,
				assetName = item,
				shooter = this,
				target = this.shootingAt,
				x = this.x,
				y = this.y,
				z = this.z - 2f + (float)weaponSlotIndex * 0.9f,
				speed = 40f
			};
			CreateProjectileObjCallback(laserMovingObject);
			projectileObject = laserMovingObject;
		}
		else if (item == "WeapLaserT3")
		{
			laserMovingObject = new LaserMovingObject()
			{
				galaxy = this.galaxy,
				assetName = item,
				shooter = this,
				target = this.shootingAt,
				x = this.x,
				y = this.y,
				z = this.z - 2f + (float)weaponSlotIndex * 0.9f,
				speed = 40f
			};
			CreateProjectileObjCallback(laserMovingObject);
			projectileObject = laserMovingObject;
		}
		else if (item == "WeapLaserT4")
		{
			laserMovingObject = new LaserMovingObject()
			{
				galaxy = this.galaxy,
				assetName = item,
				shooter = this,
				target = this.shootingAt,
				x = this.x,
				y = this.y,
				z = this.z - 2f + (float)weaponSlotIndex * 0.9f,
				speed = 40f
			};
			CreateProjectileObjCallback(laserMovingObject);
			projectileObject = laserMovingObject;
		}
		else if (item == "WeapLaserT5")
		{
			laserMovingObject = new LaserMovingObject()
			{
				galaxy = this.galaxy,
				assetName = item,
				shooter = this,
				target = this.shootingAt,
				x = this.x,
				y = this.y,
				z = this.z - 2f + (float)weaponSlotIndex * 0.9f,
				speed = 40f
			};
			CreateProjectileObjCallback(laserMovingObject);
			projectileObject = laserMovingObject;
		}
		else if (item == "WeapPlasmaT1")
		{
			bulletObject = new BulletObject()
			{
				galaxy = this.galaxy,
				assetName = item,
				shooter = this,
				target = this.shootingAt,
				shooterNeibId = this.neighbourhoodId,
				x = this.x,
				y = this.y,
				z = this.z - 2f + (float)weaponSlotIndex * 0.9f
			};
			CreateProjectileObjCallback(bulletObject);
			projectileObject = bulletObject;
		}
		else if (item == "WeapPlasmaT2")
		{
			bulletObject = new BulletObject()
			{
				galaxy = this.galaxy,
				assetName = item,
				shooter = this,
				target = this.shootingAt,
				shooterNeibId = this.neighbourhoodId,
				x = this.x,
				y = this.y,
				z = this.z - 2f + (float)weaponSlotIndex * 0.9f
			};
			CreateProjectileObjCallback(bulletObject);
			projectileObject = bulletObject;
		}
		else if (item == "WeapPlasmaT3")
		{
			bulletObject = new BulletObject()
			{
				galaxy = this.galaxy,
				assetName = item,
				shooter = this,
				target = this.shootingAt,
				shooterNeibId = this.neighbourhoodId,
				x = this.x,
				y = this.y,
				z = this.z - 2f + (float)weaponSlotIndex * 0.9f
			};
			CreateProjectileObjCallback(bulletObject);
			projectileObject = bulletObject;
		}
		else if (item == "WeapPlasmaT4")
		{
			bulletObject = new BulletObject()
			{
				galaxy = this.galaxy,
				assetName = item,
				shooter = this,
				target = this.shootingAt,
				shooterNeibId = this.neighbourhoodId,
				x = this.x,
				y = this.y,
				z = this.z - 2f + (float)weaponSlotIndex * 0.9f
			};
			CreateProjectileObjCallback(bulletObject);
			projectileObject = bulletObject;
		}
		else if (item == "WeapPlasmaT5")
		{
			bulletObject = new BulletObject()
			{
				galaxy = this.galaxy,
				assetName = item,
				shooter = this,
				target = this.shootingAt,
				shooterNeibId = this.neighbourhoodId,
				x = this.x,
				y = this.y,
				z = this.z - 2f + (float)weaponSlotIndex * 0.9f
			};
			CreateProjectileObjCallback(bulletObject);
			projectileObject = bulletObject;
		}
		else if (item == "WeapIonT1")
		{
			bulletObject = new BulletObject()
			{
				galaxy = this.galaxy,
				assetName = item,
				shooter = this,
				target = this.shootingAt,
				shooterNeibId = this.neighbourhoodId,
				x = this.x,
				y = this.y,
				z = this.z - 2f + (float)weaponSlotIndex * 0.9f
			};
			CreateProjectileObjCallback(bulletObject);
			projectileObject = bulletObject;
		}
		else if (item == "WeapIonT2")
		{
			bulletObject = new BulletObject()
			{
				galaxy = this.galaxy,
				assetName = item,
				shooter = this,
				target = this.shootingAt,
				shooterNeibId = this.neighbourhoodId,
				x = this.x,
				y = this.y,
				z = this.z - 2f + (float)weaponSlotIndex * 0.9f
			};
			CreateProjectileObjCallback(bulletObject);
			projectileObject = bulletObject;
		}
		else if (item == "WeapIonT3")
		{
			bulletObject = new BulletObject()
			{
				galaxy = this.galaxy,
				assetName = item,
				shooter = this,
				target = this.shootingAt,
				shooterNeibId = this.neighbourhoodId,
				x = this.x,
				y = this.y,
				z = this.z - 2f + (float)weaponSlotIndex * 0.9f
			};
			CreateProjectileObjCallback(bulletObject);
			projectileObject = bulletObject;
		}
		else if (item == "WeapIonT4")
		{
			bulletObject = new BulletObject()
			{
				galaxy = this.galaxy,
				assetName = item,
				shooter = this,
				target = this.shootingAt,
				shooterNeibId = this.neighbourhoodId,
				x = this.x,
				y = this.y,
				z = this.z - 2f + (float)weaponSlotIndex * 0.9f
			};
			CreateProjectileObjCallback(bulletObject);
			projectileObject = bulletObject;
		}
		else if (!(item == "WeapIonT5"))
		{
			projectileObject = null;
		}
		else
		{
			bulletObject = new BulletObject()
			{
				galaxy = this.galaxy,
				assetName = item,
				shooter = this,
				target = this.shootingAt,
				shooterNeibId = this.neighbourhoodId,
				x = this.x,
				y = this.y,
				z = this.z - 2f + (float)weaponSlotIndex * 0.9f
			};
			CreateProjectileObjCallback(bulletObject);
			projectileObject = bulletObject;
		}
		return projectileObject;
	}

	public void StartRotate()
	{
		float single = 0f;
		float single1 = 0f;
		if (!this.IsPve)
		{
		}
		if (this.moveState != 10)
		{
			single = this.destinationZ - this.z;
			single1 = this.destinationX - this.x;
		}
		else
		{
			single = this.enteringBase.z + this.enteringBase.doorZ[this.enteringBaseDoor] - this.z;
			single1 = this.enteringBase.x + this.enteringBase.doorX[this.enteringBaseDoor] - this.x;
		}
		float single2 = this.rotationY;
		this.neededRotation = Mathf.Atan2(single1, single);
		this.neededRotation *= 57.2957726f;
		this.neededRotation -= single2;
		if (this.neededRotation < -180f)
		{
			this.neededRotation += 360f;
		}
		this.destinationAngle = single2 + this.neededRotation;
		this.angularVelocity = this.cfg.maxRotationSpeed * Mathf.Sign(this.neededRotation);
		this.rotationDone = 0f;
		this.rotationState = 1;
	}

	private void StopShipAfterCollision(ref float dx, ref float dy, ref float dz)
	{
		dx = 0f;
		dy = 0f;
		dz = 0f;
		this.moveState = 0;
	}

	public override string ToString()
	{
		object[] objArray = new object[] { this.x, this.y, this.z, this.moveState, this.isRemoved, this.isAlive, this.playerData.state, this.timeStartedExitHJ };
		return string.Format("X={0} Y={1} Z={2} MoveState={3} IsRemoved={4} IsAlive={5} State={6} timeStartedExitHJ={7}", objArray);
	}

	public void TurnOnOffWeapon(int weaponSlotId, bool isActive)
	{
		WeaponSlot ticks = (
			from ws in this.cfg.weaponSlots
			where ws.slotId ==weaponSlotId
			select ws).FirstOrDefault<WeaponSlot>();
		if (ticks != null)
		{
			SlotItemWeapon slotItemWeapon = (SlotItemWeapon)(
				from t in this.playerData.playerBelongings.playerItems.slotItems
				where (t.ItemType != ticks.weaponTierId || t.ShipId != this.playerData.playerBelongings.selectedShipId ? false : t.Slot == weaponSlotId)
				select t).FirstOrDefault<SlotItem>();
			slotItemWeapon.IsActive = isActive;
			ticks.isActive = isActive;
			if (!isActive)
			{
				this.cfg.skillDamage -= ticks.skillDamage;
			}
			else
			{
				ticks.lastShotTime = StaticData.now.Ticks;
				this.cfg.skillDamage += ticks.skillDamage;
			}
			this.playerData.cfg.skillDamage = this.cfg.skillDamage;
		}
	}

	public class ActiveSkill
	{
		public ushort skillType;

		public DateTime startedAt;

		public DateTime toStopAt;

		public byte stacks;

		public List<PlayerObjectPhysics> casters;

		public ActiveSkill()
		{
		}
	}
}