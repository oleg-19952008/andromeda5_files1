using System;
using System.IO;

public class ShipsTypeNet : ITransferable
{
	public string shipClass;

	public string shipName;

	public string assetName;

	public short levelRestriction;

	public short tier;

	public short upgrade;

	public short targeting;

	public short avoidance;

	public short speed;

	public short sortIndex;

	public short repairPrice;

	public int id;

	public int corpus;

	public int cargo;

	public int shield;

	public int price;

	public float acceleration = 10f;

	public float backAcceleration = 10f;

	public float distanceDecelerate = 5f;

	public float rotationSpeed = 230f;

	public CashType cashType;

	public string CashType2
	{
		get
		{
			return this.cashType.ToString();
		}
		set
		{
			this.cashType = (CashType)Enum.Parse(typeof(CashType), value, false);
		}
	}

	public ShipsTypeNet()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.shipClass = br.ReadString();
		this.shipName = br.ReadString();
		this.assetName = br.ReadString();
		this.levelRestriction = br.ReadInt16();
		this.tier = br.ReadInt16();
		this.upgrade = br.ReadInt16();
		this.targeting = br.ReadInt16();
		this.avoidance = br.ReadInt16();
		this.speed = br.ReadInt16();
		this.sortIndex = br.ReadInt16();
		this.repairPrice = br.ReadInt16();
		this.id = br.ReadInt32();
		this.corpus = br.ReadInt32();
		this.cargo = br.ReadInt32();
		this.shield = br.ReadInt32();
		this.price = br.ReadInt32();
		this.acceleration = br.ReadSingle();
		this.backAcceleration = br.ReadSingle();
		this.distanceDecelerate = br.ReadSingle();
		this.rotationSpeed = br.ReadSingle();
		this.cashType = (CashType)br.ReadInt32();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.shipClass ?? "");
		bw.Write(this.shipName ?? "");
		bw.Write(this.assetName ?? "");
		bw.Write(this.levelRestriction);
		bw.Write(this.tier);
		bw.Write(this.upgrade);
		bw.Write(this.targeting);
		bw.Write(this.avoidance);
		bw.Write(this.speed);
		bw.Write(this.sortIndex);
		bw.Write(this.repairPrice);
		bw.Write(this.id);
		bw.Write(this.corpus);
		bw.Write(this.cargo);
		bw.Write(this.shield);
		bw.Write(this.price);
		bw.Write(this.acceleration);
		bw.Write(this.backAcceleration);
		bw.Write(this.distanceDecelerate);
		bw.Write(this.rotationSpeed);
		bw.Write((int)this.cashType);
	}
}