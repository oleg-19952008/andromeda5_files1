using System;
using System.IO;

public class WeaponInfo : ITransferable
{
	public string assetName;

	public string weaponName;

	public long reloadTime;

	public float lockRange;

	public float giveUpShootingRange;

	public float lockTime;

	public float lockProbabilityMax;

	public float lockProbabilityMin;

	public int ammoPerShot;

	public float speed;

	public float range;

	public float Targeting;

	public int price;

	public CashType priceCurrency;

	public WeaponInfo()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.assetName = br.ReadString();
		this.weaponName = br.ReadString();
		this.reloadTime = br.ReadInt64();
		this.lockRange = br.ReadSingle();
		this.giveUpShootingRange = br.ReadSingle();
		this.lockTime = br.ReadSingle();
		this.lockProbabilityMax = br.ReadSingle();
		this.lockProbabilityMin = br.ReadSingle();
		this.ammoPerShot = br.ReadInt32();
		this.speed = br.ReadSingle();
		this.range = br.ReadSingle();
		this.Targeting = br.ReadSingle();
		this.price = br.ReadInt32();
		this.priceCurrency = (CashType)br.ReadByte();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.assetName);
		bw.Write(this.weaponName);
		bw.Write(this.reloadTime);
		bw.Write(this.lockRange);
		bw.Write(this.giveUpShootingRange);
		bw.Write(this.lockTime);
		bw.Write(this.lockProbabilityMax);
		bw.Write(this.lockProbabilityMin);
		bw.Write(this.ammoPerShot);
		bw.Write(this.speed);
		bw.Write(this.range);
		bw.Write(this.Targeting);
		bw.Write(this.price);
		bw.Write((byte)this.priceCurrency);
	}
}