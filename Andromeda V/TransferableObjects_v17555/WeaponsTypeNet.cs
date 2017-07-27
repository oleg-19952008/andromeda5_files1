using System;
using System.IO;

public class WeaponsTypeNet : PlayerItemTypesData, ITransferable
{
	public WeaponUpgradesNet[] upgrades = new WeaponUpgradesNet[0];

	public string weaponClass;

	public short weaponTire;

	public short damage;

	public short penetration;

	public short targeting;

	public float range;

	public int cooldown;

	public WeaponsTypeNet()
	{
	}

	public override void Deserialize(BinaryReader br)
	{
		int num = br.ReadInt32();
		this.upgrades = new WeaponUpgradesNet[num];
		for (int i = 0; i < num; i++)
		{
			this.upgrades[i] = new WeaponUpgradesNet();
			this.upgrades[i].Deserialize(br);
		}
		this.weaponClass = br.ReadString();
		this.weaponTire = br.ReadInt16();
		this.damage = br.ReadInt16();
		this.penetration = br.ReadInt16();
		this.targeting = br.ReadInt16();
		this.range = br.ReadSingle();
		this.cooldown = br.ReadInt32();
		base.Deserialize(br);
	}

	public override void Serialize(BinaryWriter bw)
	{
		bw.Write((int)this.upgrades.Length);
		for (int i = 0; i < (int)this.upgrades.Length; i++)
		{
			this.upgrades[i].Serialize(bw);
		}
		bw.Write(this.weaponClass ?? "");
		bw.Write(this.weaponTire);
		bw.Write(this.damage);
		bw.Write(this.penetration);
		bw.Write(this.targeting);
		bw.Write(this.range);
		bw.Write(this.cooldown);
		base.Serialize(bw);
	}
}