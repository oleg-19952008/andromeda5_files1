using System;
using System.IO;

public class ShipInfo : ITransferable
{
	public string id;

	public float maxSpeed;

	public float acceleration;

	public float backAcceleration;

	public float maxSpeedAngular;

	public float distanceToStop;

	public float distanceToDecelerate;

	public float floatUpSpeed;

	public float mass;

	public float hitPoints;

	public float shield;

	public int maxAmmoNormal;

	public int maxAmmoLaser;

	public int maxAmmoPlasma;

	public int maxAmmoIon;

	public ShipInfo()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.id = br.ReadString();
		this.maxSpeed = br.ReadSingle();
		this.acceleration = br.ReadSingle();
		this.backAcceleration = br.ReadSingle();
		this.maxSpeedAngular = br.ReadSingle();
		this.distanceToStop = br.ReadSingle();
		this.distanceToDecelerate = br.ReadSingle();
		this.floatUpSpeed = br.ReadSingle();
		this.mass = br.ReadSingle();
		this.hitPoints = br.ReadSingle();
		this.shield = br.ReadSingle();
		this.maxAmmoNormal = br.ReadInt32();
		this.maxAmmoLaser = br.ReadInt32();
		this.maxAmmoPlasma = br.ReadInt32();
		this.maxAmmoIon = br.ReadInt32();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.id);
		bw.Write(this.maxSpeed);
		bw.Write(this.acceleration);
		bw.Write(this.backAcceleration);
		bw.Write(this.maxSpeedAngular);
		bw.Write(this.distanceToStop);
		bw.Write(this.distanceToDecelerate);
		bw.Write(this.floatUpSpeed);
		bw.Write(this.mass);
		bw.Write(this.hitPoints);
		bw.Write(this.shield);
		bw.Write(this.maxAmmoNormal);
		bw.Write(this.maxAmmoLaser);
		bw.Write(this.maxAmmoPlasma);
		bw.Write(this.maxAmmoIon);
	}
}