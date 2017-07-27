using System;
using System.Collections.Generic;
using System.IO;

public class DefenceTurret : GameObjectPhysics, ITransferable
{
	public float range;

	public int damage;

	public byte penetration;

	public byte fractionId;

	public byte pvpTeamNumber;

	public long realReloadTime;

	public long lastShotTime;

	public PlayerObjectPhysics target;

	public uint currentTargetNbId;

	public bool isShooting;

	public Action<DefenceTurret> decisionTaken;

	public int rotationState;

	public float rotationX;

	public float rotationY;

	public float rotationZ;

	public float angularVelocity;

	public float rotationDone;

	public float neededRotation;

	public float destinationAngle;

	public DefenceTurret()
	{
	}

	protected void AddRotationY(float val)
	{
		DefenceTurret defenceTurret = this;
		defenceTurret.rotationY = defenceTurret.rotationY + val;
		if (this.rotationY >= 360f)
		{
			DefenceTurret defenceTurret1 = this;
			defenceTurret1.rotationY = defenceTurret1.rotationY - 360f;
			DefenceTurret defenceTurret2 = this;
			defenceTurret2.destinationAngle = defenceTurret2.destinationAngle - 360f;
		}
		else if (this.rotationY < 0f)
		{
			DefenceTurret defenceTurret3 = this;
			defenceTurret3.rotationY = defenceTurret3.rotationY + 360f;
			DefenceTurret defenceTurret4 = this;
			defenceTurret4.destinationAngle = defenceTurret4.destinationAngle + 360f;
		}
	}

	public override void CalculateObjectMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		this.ManageRotation(dt);
		if (!this.isShooting)
		{
			this.ValidateIdling();
		}
		else
		{
			this.ExecuteAgression();
		}
	}

	public void CopyPropsTo(DefenceTurret copyTarget)
	{
		base.CopyPropsTo(copyTarget);
		copyTarget.isShooting = this.isShooting;
		copyTarget.currentTargetNbId = this.currentTargetNbId;
		copyTarget.neededRotation = this.neededRotation;
		copyTarget.rotationState = this.rotationState;
		copyTarget.rotationDone = this.rotationDone;
	}

	public override void Deserialize(BinaryReader br)
	{
		base.Deserialize(br);
		this.fractionId = br.ReadByte();
		this.pvpTeamNumber = br.ReadByte();
		this.currentTargetNbId = br.ReadUInt32();
		this.isShooting = br.ReadBoolean();
	}

	public void ExecuteAgression()
	{
		this.StartRotate();
	}

	public ProjectileObject[] Fusillade(Action<ProjectileObject> CreateProjectileObjCallback)
	{
		ProjectileObject[] array;
		List<ProjectileObject> projectileObjects = new List<ProjectileObject>();
		if (this.target == null)
		{
			array = projectileObjects.ToArray();
		}
		else if ((this.target.isRemoved || !this.target.isAlive || this.target.moveState > 10 ? false : this.target.playerData.state == ServerState.OnMap))
		{
			long ticks = StaticData.now.Ticks;
			if (this.lastShotTime + this.realReloadTime <= ticks)
			{
				if (this.target != null)
				{
					if (GameObjectPhysics.GetDistance(this.target.x, this.x, this.target.z, this.z) > this.range)
					{
						array = projectileObjects.ToArray();
						return array;
					}
					this.lastShotTime = ticks;
					ProjectileObject projectileObject = this.StartProjectile(CreateProjectileObjCallback);
					projectileObjects.Add(projectileObject);
					projectileObject.damageHull = (ushort)((float)this.damage * ((float)this.penetration / 100f));
					projectileObject.damageShield = (ushort)(this.damage - projectileObject.damageHull);
				}
				else
				{
					array = new ProjectileObject[0];
					return array;
				}
			}
			array = projectileObjects.ToArray();
		}
		else
		{
			array = projectileObjects.ToArray();
		}
		return array;
	}

	public void ManageRotation(float dt)
	{
		if (this.rotationState != 0)
		{
			if (this.rotationState == 1)
			{
				float single = dt * this.angularVelocity;
				this.AddRotationY(single);
				DefenceTurret defenceTurret = this;
				defenceTurret.rotationDone = defenceTurret.rotationDone + Math.Abs(single);
				if (this.rotationDone >= Math.Abs(this.neededRotation))
				{
					float single1 = this.rotationY;
					float single2 = this.destinationAngle - single1;
					if (single2 >= 360f)
					{
						single2 = single2 - 360f;
					}
					this.AddRotationY(single2);
					this.rotationState = 0;
					this.rotationDone = 0f;
				}
			}
		}
	}

	public override void Serialize(BinaryWriter bw)
	{
		base.Serialize(bw);
		bw.Write(this.fractionId);
		bw.Write(this.pvpTeamNumber);
		if (this.target == null)
		{
			bw.Write(0);
		}
		else
		{
			bw.Write(this.target.neighbourhoodId);
		}
		bw.Write(this.isShooting);
	}

	private ProjectileObject StartProjectile(Action<ProjectileObject> CreateProjectileObjCallback)
	{
		RocketObject rocketObject = new RocketObject()
		{
			galaxy = this.galaxy,
			assetName = "Weap-Impaler",
			shooter = this,
			target = this.target,
			shooterNeibId = this.neighbourhoodId,
			x = this.x,
			y = this.y + 3f,
			z = this.z
		};
		CreateProjectileObjCallback(rocketObject);
		return rocketObject;
	}

	public void StartRotate()
	{
		float single = 0f;
		float single1 = 0f;
		if (this.target == null)
		{
			single = -1f;
			single1 = 0f;
		}
		else
		{
			single = this.target.z - this.z;
			single1 = this.target.x - this.x;
		}
		float single2 = this.rotationY;
		this.neededRotation = Mathf.Atan2(single1, single);
		DefenceTurret defenceTurret = this;
		defenceTurret.neededRotation = defenceTurret.neededRotation * 57.2957726f;
		DefenceTurret defenceTurret1 = this;
		defenceTurret1.neededRotation = defenceTurret1.neededRotation - single2;
		if (this.neededRotation < -180f)
		{
			DefenceTurret defenceTurret2 = this;
			defenceTurret2.neededRotation = defenceTurret2.neededRotation + 360f;
		}
		this.destinationAngle = single2 + this.neededRotation;
		this.angularVelocity = 200f * Mathf.Sign(this.neededRotation);
		this.rotationDone = 0f;
		this.rotationState = 1;
	}

	public bool ValidateIdling()
	{
		bool flag;
		foreach (PlayerObjectPhysics value in this.nbPlayers.Values)
		{
			if (this.galaxy.isPveMap)
			{
				if ((value.teamNumber == this.pvpTeamNumber || !value.isAlive || value.isRemoved ? true : value.playerData.state != ServerState.OnMap))
				{
					continue;
				}
			}
			else if ((value.fractionId == this.fractionId || !value.isAlive || value.isRemoved ? true : value.playerData.state != ServerState.OnMap))
			{
				continue;
			}
			if (GameObjectPhysics.GetDistance(value.x, this.x, value.z, this.z) <= this.range)
			{
				this.target = value;
				this.isShooting = true;
				if (this.decisionTaken != null)
				{
					this.decisionTaken(this);
				}
				flag = false;
				return flag;
			}
		}
		this.StartRotate();
		flag = true;
		return flag;
	}
}