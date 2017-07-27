using System;
using System.IO;

public class StarBaseNet : GameObjectPhysics, ITransferable
{
	public string starBaseName;

	public int id;

	public float speed;

	[NonSerialized]
	public Victor3[] route;

	private byte nextRoutePoint;

	[NonSerialized]
	private static float arrivedDistance;

	public byte lastUsedDoor = 0;

	public byte starBaseType;

	public byte fractionId;

	public short starBaseKey;

	public static StarBaseStatic[] staticData;

	public PlayerObjectPhysics[] neighbouringPlayers;

	public float[] doorX
	{
		get
		{
			return StarBaseNet.staticData[this.starBaseType].doorX;
		}
	}

	public float[] doorY
	{
		get
		{
			return StarBaseNet.staticData[this.starBaseType].doorY;
		}
	}

	public float[] doorZ
	{
		get
		{
			return StarBaseNet.staticData[this.starBaseType].doorZ;
		}
	}

	public float TakeZoneMaxX
	{
		get
		{
			return StarBaseNet.staticData[this.starBaseType].TakeZoneMaxX;
		}
	}

	public float TakeZoneMaxZ
	{
		get
		{
			return StarBaseNet.staticData[this.starBaseType].TakeZoneMaxZ;
		}
	}

	public float TakeZoneMinX
	{
		get
		{
			return StarBaseNet.staticData[this.starBaseType].TakeZoneMinX;
		}
	}

	public float TakeZoneMinZ
	{
		get
		{
			return StarBaseNet.staticData[this.starBaseType].TakeZoneMinZ;
		}
	}

	static StarBaseNet()
	{
		StarBaseNet.arrivedDistance = 0.5f;
		StarBaseStatic[] starBaseStaticArray = new StarBaseStatic[2];
		StarBaseStatic starBaseStatic = new StarBaseStatic()
		{
			TakeZoneMaxX = 40f,
			TakeZoneMaxZ = 40f,
			TakeZoneMinX = -40f,
			TakeZoneMinZ = -60f,
			doorX = new float[] { -20.47f, -7.42f, -7.08f, 7.14f, 7.43f, 20.55f, -2.53f, 2.45f },
			doorY = new float[] { -8.49f, -7.36f, -13.58f, -13.58f, -7.36f, -8.49f, 15.48f, 15.48f },
			doorZ = new float[] { -4.61f, -8.64f, -8.64f, -8.64f, -8.64f, -4.61f, -12.58f, -12.57f }
		};
		starBaseStaticArray[0] = starBaseStatic;
		StarBaseStatic starBaseStatic1 = new StarBaseStatic()
		{
			TakeZoneMaxX = 40f,
			TakeZoneMaxZ = -30f,
			TakeZoneMinX = -40f,
			TakeZoneMinZ = -50f
		};
		float[] singleArray = new float[] { -7.6f };
		starBaseStatic1.doorX = singleArray;
		singleArray = new float[] { 12.55f };
		starBaseStatic1.doorY = singleArray;
		singleArray = new float[] { -24.32f };
		starBaseStatic1.doorZ = singleArray;
		starBaseStaticArray[1] = starBaseStatic1;
		StarBaseNet.staticData = starBaseStaticArray;
	}

	public StarBaseNet()
	{
	}

	public override void CalculateObjectMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		Victor3 victor3 = this.route[this.nextRoutePoint];
		Victor3 victor31 = Victor3.MoveTowards(new Victor3(this.x, this.y, this.z), victor3, this.speed * dt);
		dx = victor31.x - this.x;
		dy = victor31.y - this.y;
		dz = victor31.z - this.z;
		if (Victor3.Distance(victor31, victor3) < StarBaseNet.arrivedDistance)
		{
			StarBaseNet starBaseNet = this;
			starBaseNet.nextRoutePoint = (byte)(starBaseNet.nextRoutePoint + 1);
			if (this.nextRoutePoint >= (int)this.route.Length)
			{
				this.nextRoutePoint = 0;
			}
		}
	}

	public override void Deserialize(BinaryReader br)
	{
		this.starBaseName = br.ReadString();
		this.id = br.ReadInt32();
		this.speed = br.ReadSingle();
		int num = br.ReadInt32();
		this.route = new Victor3[num];
		for (int i = 0; i < num; i++)
		{
			Victor3 victor3 = new Victor3()
			{
				x = br.ReadSingle(),
				y = br.ReadSingle(),
				z = br.ReadSingle()
			};
			this.route[i] = victor3;
		}
		this.nextRoutePoint = br.ReadByte();
		this.lastUsedDoor = br.ReadByte();
		this.starBaseType = br.ReadByte();
		this.fractionId = br.ReadByte();
		this.starBaseKey = br.ReadInt16();
		base.Deserialize(br);
	}

	public byte GetNextDoor()
	{
		StarBaseNet starBaseNet = this;
		starBaseNet.lastUsedDoor = (byte)(starBaseNet.lastUsedDoor + 1);
		if (this.lastUsedDoor >= (int)this.doorX.Length)
		{
			this.lastUsedDoor = 0;
		}
		return this.lastUsedDoor;
	}

	public bool IsObjectInRange(GameObjectPhysics obj)
	{
		float takeZoneMinX = this.x + this.TakeZoneMinX;
		float takeZoneMaxX = this.x + this.TakeZoneMaxX;
		float takeZoneMinZ = this.z + this.TakeZoneMinZ;
		float takeZoneMaxZ = this.z + this.TakeZoneMaxZ;
		return (obj.x <= takeZoneMinX || obj.x >= takeZoneMaxX || obj.z <= takeZoneMinZ ? false : obj.z < takeZoneMaxZ);
	}

	public override void Serialize(BinaryWriter bw)
	{
		bw.Write(this.starBaseName ?? "");
		bw.Write(this.id);
		bw.Write(this.speed);
		bw.Write((int)this.route.Length);
		Victor3[] victor3Array = this.route;
		for (int i = 0; i < (int)victor3Array.Length; i++)
		{
			Victor3 victor3 = victor3Array[i];
			bw.Write(victor3.x);
			bw.Write(victor3.y);
			bw.Write(victor3.z);
		}
		bw.Write(this.nextRoutePoint);
		bw.Write(this.lastUsedDoor);
		bw.Write(this.starBaseType);
		bw.Write(this.fractionId);
		bw.Write(this.starBaseKey);
		base.Serialize(bw);
	}
}