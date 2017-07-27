using System;
using System.Collections.Generic;
using System.IO;

public class PvEInfo : ITransferable
{
	[NonSerialized]
	public List<PveDropChance> drops;

	public short pveKey;

	public int typeIndex;

	public int id;

	public string pveType;

	public string name;

	public int shield;

	public int corpus;

	public short speed;

	public short range;

	public short agression;

	public short cooldown;

	public int damage;

	public short penetration;

	public int exp;

	public short level;

	public string assetName;

	public int respawnTime;

	public short targeting;

	public short avoidance;

	public short chaseRange;

	public short weaponTypeId;

	public PvEInfo()
	{
	}

	public void CopyTo(PvEInfo newOne)
	{
		newOne.drops = this.drops;
		newOne.pveKey = this.pveKey;
		newOne.typeIndex = this.typeIndex;
		newOne.id = this.id;
		newOne.pveType = this.pveType;
		newOne.name = this.name;
		newOne.shield = this.shield;
		newOne.corpus = this.corpus;
		newOne.speed = this.speed;
		newOne.range = this.range;
		newOne.agression = this.agression;
		newOne.cooldown = this.cooldown;
		newOne.damage = this.damage;
		newOne.penetration = this.penetration;
		newOne.exp = this.exp;
		newOne.level = this.level;
		newOne.assetName = this.assetName;
		newOne.respawnTime = this.respawnTime;
		newOne.targeting = this.targeting;
		newOne.avoidance = this.avoidance;
		newOne.chaseRange = this.chaseRange;
		newOne.weaponTypeId = this.weaponTypeId;
	}

	public void Deserialize(BinaryReader br)
	{
		this.pveKey = br.ReadInt16();
		this.typeIndex = br.ReadInt32();
		this.id = br.ReadInt32();
		this.pveType = br.ReadString();
		this.name = br.ReadString();
		this.shield = br.ReadInt32();
		this.corpus = br.ReadInt32();
		this.speed = br.ReadInt16();
		this.range = br.ReadInt16();
		this.agression = br.ReadInt16();
		this.cooldown = br.ReadInt16();
		this.damage = br.ReadInt32();
		this.penetration = br.ReadInt16();
		this.exp = br.ReadInt32();
		this.level = br.ReadInt16();
		this.assetName = br.ReadString();
		this.respawnTime = br.ReadInt32();
		this.targeting = br.ReadInt16();
		this.avoidance = br.ReadInt16();
		this.chaseRange = br.ReadInt16();
		this.weaponTypeId = br.ReadInt16();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.pveKey);
		bw.Write(this.typeIndex);
		bw.Write(this.id);
		bw.Write(this.pveType);
		bw.Write(this.name);
		bw.Write(this.shield);
		bw.Write(this.corpus);
		bw.Write(this.speed);
		bw.Write(this.range);
		bw.Write(this.agression);
		bw.Write(this.cooldown);
		bw.Write(this.damage);
		bw.Write(this.penetration);
		bw.Write(this.exp);
		bw.Write(this.level);
		bw.Write(this.assetName);
		bw.Write(this.respawnTime);
		bw.Write(this.targeting);
		bw.Write(this.avoidance);
		bw.Write(this.chaseRange);
		bw.Write(this.weaponTypeId);
	}
}