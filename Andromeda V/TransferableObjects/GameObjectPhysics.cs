using System;
using System.Collections.Generic;
using System.IO;

public class GameObjectPhysics : ITransferable
{
	public SortedList<uint, PlayerObjectPhysics> nbPlayers = new SortedList<uint, PlayerObjectPhysics>(300);

	public SortedList<uint, PvEPhysics> nbPVEs = new SortedList<uint, PvEPhysics>(200);

	public SortedList<uint, Mineral> nbMinerals = new SortedList<uint, Mineral>(350);

	public SortedList<uint, ProjectileObject> nbProjectiles = new SortedList<uint, ProjectileObject>(100);

	public SortedList<uint, DefenceTurret> nbDTs = new SortedList<uint, DefenceTurret>(50);

	public SortedList<uint, MiningStation> nbMStations = new SortedList<uint, MiningStation>(5);

	public bool isOnClientSide = false;

	public object gameObject;

	public uint neighbourhoodId;

	[NonSerialized]
	public static Action<string> logMethod;

	public float x;

	public float y;

	public float z;

	[NonSerialized]
	public float speedX;

	[NonSerialized]
	public float speedY;

	[NonSerialized]
	public float speedZ;

	public string assetName;

	[NonSerialized]
	public DateTime lastUpdateTime;

	[NonSerialized]
	public bool isRemoved = false;

	[NonSerialized]
	public LevelMap galaxy;

	public bool isImmuneToAllIncomingDamage = false;

	public List<ActiveSkillObject> _activatedSkills = new List<ActiveSkillObject>();

	public ActiveSkillObject[] activatedSkillsSafe
	{
		get
		{
			ActiveSkillObject[] array;
			lock (this._activatedSkills)
			{
				array = this._activatedSkills.ToArray();
			}
			return array;
		}
	}

	public bool IsDT
	{
		get
		{
			return this is DefenceTurret;
		}
	}

	public bool IsPoP
	{
		get
		{
			return this is PlayerObjectPhysics;
		}
	}

	public GameObjectPhysics()
	{
	}

	public void AddActivatedSkill(ActiveSkillObject skill)
	{
		lock (this._activatedSkills)
		{
			this._activatedSkills.Add(skill);
		}
	}

	public virtual void CalculateObjectMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		dx = this.speedX * dt;
		dy = this.speedY * dt;
		dz = this.speedZ * dt;
	}

	public void ClearActivatedSkills()
	{
		lock (this._activatedSkills)
		{
			this._activatedSkills.Clear();
		}
	}

	public void CopyPropsTo(GameObjectPhysics copyTarget)
	{
		copyTarget.assetName = this.assetName;
		copyTarget.lastUpdateTime = this.lastUpdateTime;
		copyTarget.isImmuneToAllIncomingDamage = this.isImmuneToAllIncomingDamage;
		copyTarget.speedX = this.speedX;
		copyTarget.speedY = this.speedY;
		copyTarget.speedZ = this.speedZ;
		copyTarget.x = this.x;
		copyTarget.y = this.y;
		copyTarget.z = this.z;
	}

	public virtual void Deserialize(BinaryReader br)
	{
		this.x = br.ReadSingle();
		this.y = br.ReadSingle();
		this.z = br.ReadSingle();
		this.assetName = br.ReadString();
		this.neighbourhoodId = br.ReadUInt32();
		this.isImmuneToAllIncomingDamage = br.ReadBoolean();
	}

	public static float GetDistance(float x1, float x2, float z1, float z2)
	{
		double num = (double)(x2 - x1);
		double num1 = (double)(z2 - z1);
		float single = (float)Math.Sqrt(num * num + num1 * num1);
		return single;
	}

	public void KeepInBoundary(ref Victor3 tmp)
	{
		if (tmp.x < (float)this.galaxy.minX)
		{
			tmp.x = (float)this.galaxy.minX;
		}
		if (tmp.x > (float)this.galaxy.maxX)
		{
			tmp.x = (float)this.galaxy.maxX;
		}
		if (tmp.z > (float)this.galaxy.maxZ)
		{
			tmp.z = (float)this.galaxy.maxZ;
		}
		if (tmp.z < (float)this.galaxy.minZ)
		{
			tmp.z = (float)this.galaxy.minZ;
		}
	}

	internal static void Log(string msg)
	{
		if (GameObjectPhysics.logMethod != null)
		{
			GameObjectPhysics.logMethod(msg);
		}
	}

	public void RemoveActivatedSkill(ActiveSkillObject skill)
	{
		lock (this._activatedSkills)
		{
			this._activatedSkills.Remove(skill);
		}
	}

	public virtual void Serialize(BinaryWriter bw)
	{
		bw.Write(this.x);
		bw.Write(this.y);
		bw.Write(this.z);
		bw.Write(this.assetName ?? "");
		bw.Write(this.neighbourhoodId);
		bw.Write(this.isImmuneToAllIncomingDamage);
	}
}