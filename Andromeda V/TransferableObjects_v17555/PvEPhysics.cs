using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

public class PvEPhysics : PlayerObjectPhysics, ITransferable
{
	public const int BORN_DISARMED_TIME_IN_MILLISECONDS = 15000;

	public Dictionary<GameObjectPhysics, float> aggroCounter = new Dictionary<GameObjectPhysics, float>();

	private static float[] aggroCoefficients;

	public PvEInfo typeData;

	public byte level;

	public byte agressionType;

	public int experience;

	public PveSpawnZoneState spawnZoneState = null;

	public PvEGroup @group;

	public PvEGroup.PvEGroupPosition groupPosition;

	public float groupDisplacementX;

	public float groupDisplacementZ;

	public long id;

	public float birthX;

	public float birthY;

	public float birthZ;

	[NonSerialized]
	public PlayerObjectPhysics normalAggressionTrackPlr;

	public uint nbIdNormalAggressionTrackPlr;

	public uint nbIdLastShotBy;

	public uint nbIdShootingAt;

	public long followedPlayerId;

	public int typeIndex;

	public float enterCombatLocationX;

	public float enterCombatLocationZ;

	public TargetOwnership tag = null;

	public GameObjectPhysics currentAggroTarget = null;

	public uint currentAggroPlayerNbId;

	public PvEPhysics.PvECommandType pveCommand = PvEPhysics.PvECommandType.Idling;

	public short routeType;

	public float[,] sentryPoints;

	public byte curSentryPointIndex;

	public Action<PvEPhysics> DecisionTaken;

	public bool isDisarmedBorn = false;

	public bool isNowBorn = false;

	public DateTime bornTime;

	private int routeLength;

	private bool pveIsStunned = false;

	private bool pveIsDisarmed = false;

	private bool pveIsShocked = false;

	private Victor3 reactionMoveDestination;

	private PvEPhysics.PvECommandType preReactCommand;

	public bool isGettingBackTheRoute;

	public bool isMinion;

	public float minPlayerDistanceRange = 10f;

	public float maxPlayerDistanceRange = 13.5f;

	public byte rocketeerPower;

	public byte shieldingPower;

	public byte repairMasterPower;

	public byte minionsPower;

	public byte enforcerPower;

	public byte stormerPower;

	public byte disablerPower;

	public byte unstoppablePower;

	public byte ultimateEnforcerPower;

	public byte remedyPower;

	public byte powerBreakerPower;

	public byte shieldFortressPower;

	public byte ultimateRocketeerPower;

	public byte repairingDronesPower;

	static PvEPhysics()
	{
		PvEPhysics.aggroCoefficients = null;
		PvEPhysics.aggroCoefficients = new float[] { 1f, 1f, 1.2f, 2f, 0.5f, 1f, 0.7f };
	}

	public PvEPhysics()
	{
		this.tag = new TargetOwnership(this);
	}

	public void AddAggro(GameObjectPhysics target, float value)
	{
		if ((this.pveCommand == PvEPhysics.PvECommandType.Agressive ? false : this.pveCommand != PvEPhysics.PvECommandType.ReturnHome))
		{
			this.pveCommand = PvEPhysics.PvECommandType.Agressive;
		}
		lock (this.aggroCounter)
		{
			if (!this.aggroCounter.ContainsKey(target))
			{
				this.aggroCounter.Add(target, value);
			}
			else
			{
				Dictionary<GameObjectPhysics, float> item = this.aggroCounter;
				Dictionary<GameObjectPhysics, float> gameObjectPhysics = item;
				GameObjectPhysics gameObjectPhysic = target;
				item[gameObjectPhysic] = gameObjectPhysics[gameObjectPhysic] + value;
			}
		}
	}

	public override void CalculateObjectMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		switch (this.pveCommand)
		{
			case PvEPhysics.PvECommandType.Idling:
			{
				this.ExecuteIdling();
				break;
			}
			case PvEPhysics.PvECommandType.Agressive:
			{
				this.ExecuteAgression();
				break;
			}
			case PvEPhysics.PvECommandType.ReturnHome:
			{
				this.ExecuteReturnHome();
				break;
			}
			case PvEPhysics.PvECommandType.TriggeredMove:
			{
				this.ExecuteTriggeredMove();
				break;
			}
		}
		base.CalculateObjectMovement(dt, ref dx, ref dy, ref dz);
	}

	public void ClearAggro(GameObjectPhysics player)
	{
		lock (this.aggroCounter)
		{
			if (this.aggroCounter.ContainsKey(player))
			{
				this.aggroCounter[player] = 0f;
			}
			this.aggroCounter.Remove(player);
		}
	}

	public void CopyPropsTo(PvEPhysics copyTarget)
	{
		base.CopyPropsTo(copyTarget);
		copyTarget.level = this.level;
		copyTarget.agressionType = this.agressionType;
		copyTarget.experience = this.experience;
		copyTarget.isNowBorn = this.isNowBorn;
		copyTarget.id = this.id;
		copyTarget.@group = this.@group;
		copyTarget.groupPosition = this.groupPosition;
		copyTarget.birthX = this.birthX;
		copyTarget.birthY = this.birthY;
		copyTarget.birthZ = this.birthZ;
		copyTarget.typeIndex = this.typeIndex;
		copyTarget.curSentryPointIndex = this.curSentryPointIndex;
		copyTarget.sentryPoints = (float[,])this.sentryPoints.Clone();
		copyTarget.pveCommand = this.pveCommand;
		copyTarget.followedPlayerId = this.followedPlayerId;
		copyTarget.pveCommand = this.pveCommand;
		copyTarget.nbIdLastShotBy = this.nbIdLastShotBy;
		copyTarget.nbIdNormalAggressionTrackPlr = this.nbIdNormalAggressionTrackPlr;
		copyTarget.sentryPoints = this.sentryPoints;
		copyTarget.currentAggroTarget = this.currentAggroTarget;
		copyTarget.currentAggroPlayerNbId = this.currentAggroPlayerNbId;
		copyTarget.isStunned = this.isStunned;
		copyTarget.isDisarmed = this.isDisarmed;
		copyTarget.isShocked = this.isShocked;
		copyTarget.isMinion = this.isMinion;
		copyTarget.minPlayerDistanceRange = this.minPlayerDistanceRange;
		copyTarget.maxPlayerDistanceRange = this.maxPlayerDistanceRange;
		copyTarget.rocketeerPower = this.rocketeerPower;
		copyTarget.shieldingPower = this.shieldingPower;
		copyTarget.repairMasterPower = this.repairMasterPower;
		copyTarget.minionsPower = this.minionsPower;
		copyTarget.enforcerPower = this.enforcerPower;
		copyTarget.stormerPower = this.stormerPower;
		copyTarget.disablerPower = this.disablerPower;
		copyTarget.selectedPoPnbId = this.selectedPoPnbId;
		copyTarget.nbIdShootingAt = this.nbIdShootingAt;
		copyTarget.unstoppablePower = this.unstoppablePower;
		copyTarget.ultimateEnforcerPower = this.ultimateEnforcerPower;
		copyTarget.remedyPower = this.remedyPower;
		copyTarget.powerBreakerPower = this.powerBreakerPower;
		copyTarget.shieldFortressPower = this.shieldFortressPower;
		copyTarget.ultimateRocketeerPower = this.ultimateRocketeerPower;
		copyTarget.repairingDronesPower = this.repairingDronesPower;
	}

	public override void Deserialize(BinaryReader br)
	{
		this.routeType = br.ReadInt16();
		this.isNowBorn = br.ReadBoolean();
		this.typeIndex = br.ReadInt32();
		this.level = br.ReadByte();
		this.agressionType = br.ReadByte();
		this.experience = br.ReadInt32();
		this.typeData = StaticData.pveTypes[this.typeIndex];
		this.@group = new PvEGroup();
		if (br.ReadInt32() != -1)
		{
			this.@group.Deserialize(br);
		}
		else
		{
			this.@group = null;
		}
		this.groupPosition = (PvEGroup.PvEGroupPosition)br.ReadInt32();
		this.id = br.ReadInt64();
		this.birthX = br.ReadSingle();
		this.birthY = br.ReadSingle();
		this.birthZ = br.ReadSingle();
		this.followedPlayerId = br.ReadInt64();
		this.pveCommand = (PvEPhysics.PvECommandType)br.ReadInt32();
		if (this.routeType == 0)
		{
			this.sentryPoints = new float[0, 0];
		}
		else
		{
			int num = br.ReadInt32();
			int num1 = br.ReadInt32();
			this.sentryPoints = new float[num, num1];
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num1; j++)
				{
					this.sentryPoints[i, j] = br.ReadSingle();
				}
			}
		}
		this.curSentryPointIndex = br.ReadByte();
		this.nbIdLastShotBy = br.ReadUInt32();
		this.nbIdShootingAt = br.ReadUInt32();
		this.nbIdNormalAggressionTrackPlr = br.ReadUInt32();
		this.currentAggroPlayerNbId = br.ReadUInt32();
		this.enterCombatLocationX = br.ReadSingle();
		this.enterCombatLocationZ = br.ReadSingle();
		this.isMinion = br.ReadBoolean();
		this.minPlayerDistanceRange = br.ReadSingle();
		this.maxPlayerDistanceRange = br.ReadSingle();
		this.rocketeerPower = br.ReadByte();
		this.shieldingPower = br.ReadByte();
		this.repairMasterPower = br.ReadByte();
		this.minionsPower = br.ReadByte();
		this.enforcerPower = br.ReadByte();
		this.stormerPower = br.ReadByte();
		this.disablerPower = br.ReadByte();
		this.unstoppablePower = br.ReadByte();
		this.ultimateEnforcerPower = br.ReadByte();
		this.remedyPower = br.ReadByte();
		this.powerBreakerPower = br.ReadByte();
		this.shieldFortressPower = br.ReadByte();
		this.ultimateRocketeerPower = br.ReadByte();
		this.repairingDronesPower = br.ReadByte();
		this.cfg = new ShipConfiguration();
		this.cfg.Deserialize(br);
		base.Deserialize(br);
	}

	public void ExecuteAgression()
	{
		base.StartRotate();
		if (this.currentAggroTarget != null)
		{
			this.destinationX = this.currentAggroTarget.x;
			this.destinationY = this.currentAggroTarget.y;
			this.destinationZ = this.currentAggroTarget.z;
		}
	}

	public void ExecuteIdling()
	{
		if (this.ValidateIdling())
		{
			if (this.routeType != 0)
			{
				if (this.DecisionTaken != null)
				{
					if ((GameObjectPhysics.GetDistance(this.destinationX, this.x, this.destinationZ, this.z) <= 0.5f ? true : this.moveState == 0))
					{
						switch (this.routeType)
						{
							case 1:
							{
								this.curSentryPointIndex = (byte)PlayerObjectPhysics.rnd.Next(0, this.routeLength);
								break;
							}
							case 2:
							{
								PvEPhysics pvEPhysic = this;
								pvEPhysic.curSentryPointIndex = (byte)(pvEPhysic.curSentryPointIndex + 1);
								if (this.curSentryPointIndex >= this.routeLength)
								{
									this.curSentryPointIndex = 0;
								}
								break;
							}
							case 3:
							{
								if (!this.isGettingBackTheRoute)
								{
									PvEPhysics pvEPhysic1 = this;
									pvEPhysic1.curSentryPointIndex = (byte)(pvEPhysic1.curSentryPointIndex + 1);
									if (this.curSentryPointIndex >= this.routeLength)
									{
										this.isGettingBackTheRoute = true;
										this.curSentryPointIndex = (byte)(this.routeLength - 2);
									}
								}
								else
								{
									PvEPhysics pvEPhysic2 = this;
									pvEPhysic2.curSentryPointIndex = (byte)(pvEPhysic2.curSentryPointIndex - 1);
									if (this.curSentryPointIndex == 255)
									{
										this.isGettingBackTheRoute = false;
										this.curSentryPointIndex = 1;
									}
								}
								break;
							}
						}
						this.destinationX = this.sentryPoints[this.curSentryPointIndex, 0];
						this.destinationZ = this.sentryPoints[this.curSentryPointIndex, 2];
						base.StartRotate();
						base.StartMove();
						this.DecisionTaken(this);
					}
				}
			}
		}
	}

	public void ExecuteIdlingOld()
	{
		if (this.ValidateIdling())
		{
			if (this.DecisionTaken != null)
			{
				if (Victor3.Distance(new Victor3(this.x, this.y, this.z), new Victor3(this.destinationX, this.destinationY, this.destinationZ)) <= 0.5f)
				{
					this.curSentryPointIndex = (byte)PlayerObjectPhysics.rnd.Next(0, this.routeLength);
					if (this.curSentryPointIndex >= this.routeLength)
					{
						this.curSentryPointIndex = 0;
					}
					if (this.groupPosition != PvEGroup.PvEGroupPosition.Leader)
					{
						this.SetDestination(this.@group.pveLeader.destinationX + this.groupDisplacementX, 0f, this.@group.pveLeader.destinationZ + this.groupDisplacementZ);
					}
					else
					{
						this.SetDestination(this.sentryPoints[this.curSentryPointIndex, 0], 0f, this.sentryPoints[this.curSentryPointIndex, 2]);
					}
					base.StartRotate();
					base.StartMove();
					this.DecisionTaken(this);
				}
			}
		}
	}

	public void ExecuteReturnHome()
	{
		if (!this.ValidateReturnHome())
		{
			this.pveCommand = PvEPhysics.PvECommandType.Idling;
			this.StartIdling();
		}
	}

	private void ExecuteTriggeredMove()
	{
		if (GameObjectPhysics.GetDistance(this.reactionMoveDestination.x, this.x, this.reactionMoveDestination.z, this.z) < 0.45f)
		{
			this.pveCommand = this.preReactCommand;
			this.currentAggroTarget = null;
		}
	}

	public GameObjectPhysics FindHighestAggroTarget()
	{
		GameObjectPhysics gameObjectPhysic;
		bool flag;
		if (this.aggroCounter.Count != 0)
		{
			GameObjectPhysics gameObjectPhysic1 = (
				from t in this.aggroCounter
				orderby t.Value descending
				select t into s
				select s.Key).FirstOrDefault<GameObjectPhysics>();
			if (gameObjectPhysic1 == null)
			{
				gameObjectPhysic = null;
			}
			else if ((!gameObjectPhysic1.IsPoP || ((PlayerObjectPhysics)gameObjectPhysic1).IsPve ? true : this.nbPlayers.ContainsKey(gameObjectPhysic1.neighbourhoodId)))
			{
				if (gameObjectPhysic1.isRemoved)
				{
					flag = false;
				}
				else if (!gameObjectPhysic1.IsPoP || ((PlayerObjectPhysics)gameObjectPhysic1).isAlive && ((PlayerObjectPhysics)gameObjectPhysic1).moveState <= 10 && !((PlayerObjectPhysics)gameObjectPhysic1).isInStealthMode)
				{
					flag = (base.IsPve ? true : ((PlayerObjectPhysics)gameObjectPhysic1).playerData.state == ServerState.OnMap);
				}
				else
				{
					flag = false;
				}
				if (flag)
				{
					gameObjectPhysic = gameObjectPhysic1;
				}
				else
				{
					lock (this.aggroCounter)
					{
						this.aggroCounter.Remove(gameObjectPhysic1);
					}
					gameObjectPhysic = this.FindHighestAggroTarget();
				}
			}
			else
			{
				lock (this.aggroCounter)
				{
					this.aggroCounter.Remove(gameObjectPhysic1);
				}
				gameObjectPhysic = this.FindHighestAggroTarget();
			}
		}
		else
		{
			gameObjectPhysic = null;
		}
		return gameObjectPhysic;
	}

	public float FindHighestAggroValue()
	{
		float single = (
			from plr in this.aggroCounter
			orderby plr.Value descending
			select plr.Value).FirstOrDefault<float>();
		return single;
	}

	public GameObjectPhysics GetPlayerAtAgroPosition(int position)
	{
		GameObjectPhysics gameObjectPhysic;
		if (this.aggroCounter.Count != 0)
		{
			GameObjectPhysics[] array = (
				from plr in this.aggroCounter
				orderby plr.Value descending
				select plr.Key).ToArray<GameObjectPhysics>();
			if ((int)array.Length > position)
			{
				gameObjectPhysic = array[position];
			}
			else
			{
				gameObjectPhysic = null;
			}
		}
		else
		{
			gameObjectPhysic = null;
		}
		return gameObjectPhysic;
	}

	public void InitializeSentry()
	{
		if (this.routeType != 0)
		{
			this.routeLength = this.sentryPoints.GetLength(0);
			if (this.routeType != 1)
			{
				this.curSentryPointIndex = 0;
			}
			else
			{
				this.curSentryPointIndex = (byte)PlayerObjectPhysics.rnd.Next(0, this.routeLength);
			}
			this.destinationX = this.sentryPoints[this.curSentryPointIndex, 0];
			this.destinationY = this.sentryPoints[this.curSentryPointIndex, 1];
			this.destinationZ = this.sentryPoints[this.curSentryPointIndex, 2];
			this.moveState = 1;
		}
		else
		{
			this.routeLength = 0;
			this.destinationX = this.birthX;
			this.destinationY = this.birthY;
			this.destinationZ = this.birthZ;
			this.moveState = 1;
		}
	}

	public bool IsPlayerInRange(PlayerObjectPhysics plr)
	{
		bool flag;
		if (!(!this.isAlive ? false : !this.isRemoved))
		{
			flag = false;
		}
		else if ((plr == null || !plr.isAlive ? false : !plr.isRemoved))
		{
			flag = (GameObjectPhysics.GetDistance(plr.x, this.x, plr.z, this.z) >= (float)this.typeData.range ? false : true);
		}
		else
		{
			flag = false;
		}
		return flag;
	}

	public void RemoveAggroTarget(GameObjectPhysics target)
	{
		if (this.aggroCounter.ContainsKey(target))
		{
			this.aggroCounter.Remove(target);
		}
	}

	public void ResetTag()
	{
		this.tag.Clear();
	}

	public override void Serialize(BinaryWriter bw)
	{
		bw.Write(this.routeType);
		bw.Write(this.isNowBorn);
		bw.Write(this.typeIndex);
		bw.Write(this.level);
		bw.Write(this.agressionType);
		bw.Write(this.experience);
		if (this.@group != null)
		{
			bw.Write(0);
			this.@group.Serialize(bw);
		}
		else
		{
			bw.Write(-1);
		}
		bw.Write((int)this.groupPosition);
		bw.Write(this.id);
		bw.Write(this.birthX);
		bw.Write(this.birthY);
		bw.Write(this.birthZ);
		bw.Write(this.followedPlayerId);
		bw.Write((int)this.pveCommand);
		if (this.routeType != 0)
		{
			bw.Write(this.sentryPoints.GetLength(0));
			bw.Write(this.sentryPoints.GetLength(1));
			for (int i = 0; i < this.sentryPoints.GetLength(0); i++)
			{
				for (int j = 0; j < this.sentryPoints.GetLength(1); j++)
				{
					bw.Write(this.sentryPoints[i, j]);
				}
			}
		}
		bw.Write(this.curSentryPointIndex);
		bw.Write(this.nbIdLastShotBy);
		bw.Write(this.nbIdShootingAt);
		bw.Write(this.nbIdNormalAggressionTrackPlr);
		bw.Write(this.currentAggroPlayerNbId);
		bw.Write(this.enterCombatLocationX);
		bw.Write(this.enterCombatLocationZ);
		bw.Write(this.isMinion);
		bw.Write(this.minPlayerDistanceRange);
		bw.Write(this.maxPlayerDistanceRange);
		bw.Write(this.rocketeerPower);
		bw.Write(this.shieldingPower);
		bw.Write(this.repairMasterPower);
		bw.Write(this.minionsPower);
		bw.Write(this.enforcerPower);
		bw.Write(this.stormerPower);
		bw.Write(this.disablerPower);
		bw.Write(this.unstoppablePower);
		bw.Write(this.ultimateEnforcerPower);
		bw.Write(this.remedyPower);
		bw.Write(this.powerBreakerPower);
		bw.Write(this.shieldFortressPower);
		bw.Write(this.ultimateRocketeerPower);
		bw.Write(this.repairingDronesPower);
		this.cfg.Serialize(bw);
		base.Serialize(bw);
	}

	public void SetDestination(float positionsX, float positionsY, float positionsZ)
	{
		this.destinationX = positionsX;
		this.destinationY = positionsY;
		this.destinationZ = positionsZ;
	}

	public void StartFollowPlayer(GameObjectPhysics target)
	{
		this.pveCommand = PvEPhysics.PvECommandType.Agressive;
		this.SetDestination(target.x, 0f, target.z);
		base.StartRotate();
		base.StartMove();
		if (this.DecisionTaken != null)
		{
			this.DecisionTaken(this);
		}
	}

	public void StartIdling()
	{
		this.isShooting = false;
		this.shootingAt = null;
		this.normalAggressionTrackPlr = null;
		this.nbIdLastShotBy = 0;
		this.nbIdNormalAggressionTrackPlr = 0;
		this.nbIdShootingAt = 0;
		this.pveCommand = PvEPhysics.PvECommandType.Idling;
		this.moveState = 1;
		this.curSentryPointIndex = 1;
		if (this.routeType != 0)
		{
			this.destinationX = this.sentryPoints[this.curSentryPointIndex, 0];
			this.destinationY = this.sentryPoints[this.curSentryPointIndex, 1];
			this.destinationZ = this.sentryPoints[this.curSentryPointIndex, 2];
		}
		else
		{
			this.destinationX = this.birthX;
			this.destinationY = 0f;
			this.destinationZ = this.birthZ;
		}
		base.StartRotate();
		if (this.DecisionTaken != null)
		{
			this.DecisionTaken(this);
		}
	}

	public void StartReturnHome()
	{
		lock (this.aggroCounter)
		{
			this.aggroCounter.Clear();
		}
		this.isImmuneToAllIncomingDamage = true;
		this.cfg.hitPoints = this.cfg.hitPointsMax;
		this.cfg.shield = (float)this.cfg.shieldMax;
		this.isShooting = false;
		this.shootingAt = null;
		this.SetDestination(this.enterCombatLocationX, 0f, this.enterCombatLocationZ);
		base.StartRotate();
		base.StartMove();
		this.pveCommand = PvEPhysics.PvECommandType.ReturnHome;
		if (this.DecisionTaken != null)
		{
			this.DecisionTaken(this);
		}
	}

	public void StartShootingAt(GameObjectPhysics target)
	{
		this.isShooting = true;
		this.shootingAt = target;
		this.nbIdShootingAt = target.neighbourhoodId;
		this.selectedPoPnbId = this.nbIdShootingAt;
		if (this.DecisionTaken != null)
		{
			this.DecisionTaken(this);
		}
	}

	public void StartTriggeredMove(Victor3 location)
	{
		this.preReactCommand = this.pveCommand;
		this.pveCommand = PvEPhysics.PvECommandType.TriggeredMove;
		this.reactionMoveDestination = location;
		this.destinationX = location.x;
		this.destinationY = 0f;
		this.destinationZ = location.z;
		this.moveState = 1;
		base.StartMove();
		base.StartRotate();
		this.DecisionTaken(this);
	}

	public bool ValidateIdling()
	{
		bool flag;
		if (this.typeData.agression != 0)
		{
			flag = (this.typeData.agression != 1 ? true : this.ValidateIdlingAggressive());
		}
		else
		{
			flag = this.ValidateIdlingNormal();
		}
		return flag;
	}

	public bool ValidateIdlingAggressive()
	{
		PlayerObjectPhysics value = null;
		bool flag;
		List<PlayerObjectPhysics> playerObjectPhysics = new List<PlayerObjectPhysics>();
		foreach (PlayerObjectPhysics va in this.nbPlayers.Values)
		{
			playerObjectPhysics.Add(va);
		}
		foreach (PlayerObjectPhysics playerObjectPhysic in this.nbPVEs.Values)
		{
			playerObjectPhysics.Add(playerObjectPhysic);
		}
		foreach (PlayerObjectPhysics playerObjectPhysic1 in playerObjectPhysics)
		{
			if ((playerObjectPhysic1.fractionId == this.fractionId || !playerObjectPhysic1.isAlive || playerObjectPhysic1.isRemoved ? false : !playerObjectPhysic1.isInStealthMode))
			{
				if ((base.IsPve ? true : playerObjectPhysic1.playerData.state == ServerState.OnMap))
				{
					if (GameObjectPhysics.GetDistance(playerObjectPhysic1.x, this.x, playerObjectPhysic1.z, this.z) <= (float)this.typeData.range)
					{
						this.pveCommand = PvEPhysics.PvECommandType.Agressive;
						flag = false;
						return flag;
					}
				}
			}
		}
		flag = true;
		return flag;
	}

	public bool ValidateIdlingNormal()
	{
		return true;
	}

	public bool ValidateReturnHome()
	{
		bool flag;
		flag = (GameObjectPhysics.GetDistance(this.x, this.enterCombatLocationX, this.z, this.enterCombatLocationZ) > 0.5f ? true : false);
		return flag;
	}

	public enum AggroCoefficientType
	{
		Normal,
		NormalSkill,
		SunderArmor,
		FocusFire,
		Decoy,
		Taunt,
		Healing,
		__COEFFICIENT_TYPES_COUNT__
	}

	public enum PvECommandType
	{
		Idling,
		Agressive,
		ReturnHome,
		TriggeredMove
	}
}