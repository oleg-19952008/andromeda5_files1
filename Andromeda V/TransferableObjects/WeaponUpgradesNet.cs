using System;
using System.IO;

public class WeaponUpgradesNet : ITransferable
{
	public long price;

	public ushort cashType;

	public short damage;

	public short range;

	public int cooldown;

	public short penetration;

	public short targeting;

	public ushort weaponType;

	public short upgradeLevel;

	public WeaponUpgradesNet()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.price = br.ReadInt64();
		this.cashType = br.ReadUInt16();
		this.damage = br.ReadInt16();
		this.range = br.ReadInt16();
		this.cooldown = br.ReadInt32();
		this.penetration = br.ReadInt16();
		this.targeting = br.ReadInt16();
		this.weaponType = br.ReadUInt16();
		this.upgradeLevel = br.ReadInt16();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.price);
		bw.Write(this.cashType);
		bw.Write(this.damage);
		bw.Write(this.range);
		bw.Write(this.cooldown);
		bw.Write(this.penetration);
		bw.Write(this.targeting);
		bw.Write(this.weaponType);
		bw.Write(this.upgradeLevel);
	}
}