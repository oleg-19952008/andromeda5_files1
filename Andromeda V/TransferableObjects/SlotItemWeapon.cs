using System;
using System.Collections.Generic;
using System.IO;

public class SlotItemWeapon : SlotItem
{
	private byte _upgradeDamage;

	private byte _upgradeRange;

	private byte _upgradeCooldown;

	private byte _upgradePenetration;

	private byte _upgradeTargeting;

	private ushort _ammoType;

	public ushort AmmoType
	{
		get
		{
			return this._ammoType;
		}
		set
		{
			this._ammoType = value;
		}
	}

	public int CooldownTotal
	{
		get
		{
			WeaponsTypeNet item = (WeaponsTypeNet)StaticData.allTypes[base.ItemType];
			return (item.upgrades[this._upgradeCooldown].cooldown - base.BonusThree < 500 ? 500 : item.upgrades[this._upgradeCooldown].cooldown - base.BonusThree);
		}
	}

	public int DamageTotal
	{
		get
		{
			WeaponsTypeNet item = (WeaponsTypeNet)StaticData.allTypes[base.ItemType];
			int bonusOne = item.upgrades[this._upgradeDamage].damage + base.BonusOne;
			return bonusOne;
		}
	}

	public int PenetrationTotal
	{
		get
		{
			WeaponsTypeNet item = (WeaponsTypeNet)StaticData.allTypes[base.ItemType];
			int num = Mathf.Min((int)(item.upgrades[this._upgradePenetration].penetration + base.BonusFour), 100);
			return num;
		}
	}

	public int RangeTotal
	{
		get
		{
			WeaponsTypeNet item = (WeaponsTypeNet)StaticData.allTypes[base.ItemType];
			int bonusTwo = item.upgrades[this._upgradeRange].range + base.BonusTwo;
			return bonusTwo;
		}
	}

	public int TargetingTotal
	{
		get
		{
			WeaponsTypeNet item = (WeaponsTypeNet)StaticData.allTypes[base.ItemType];
			int bonusFive = item.upgrades[this._upgradeTargeting].targeting + base.BonusFive;
			return bonusFive;
		}
	}

	public byte UpgradeCooldown
	{
		get
		{
			return this._upgradeCooldown;
		}
		set
		{
			this._upgradeCooldown = value;
		}
	}

	public byte UpgradeDamage
	{
		get
		{
			return this._upgradeDamage;
		}
		set
		{
			this._upgradeDamage = value;
		}
	}

	public byte UpgradeDone
	{
		get
		{
			byte num = (byte)(this._upgradeDamage + this._upgradeRange + this._upgradeCooldown + this._upgradePenetration + this._upgradeTargeting);
			return num;
		}
	}

	public byte UpgradePenetration
	{
		get
		{
			return this._upgradePenetration;
		}
		set
		{
			this._upgradePenetration = value;
		}
	}

	public byte UpgradeRange
	{
		get
		{
			return this._upgradeRange;
		}
		set
		{
			this._upgradeRange = value;
		}
	}

	public byte UpgradeTargeting
	{
		get
		{
			return this._upgradeTargeting;
		}
		set
		{
			this._upgradeTargeting = value;
		}
	}

	public SlotItemWeapon()
	{
	}

	public override void Deserialize(BinaryReader br)
	{
		base.Deserialize(br);
		this._ammoType = br.ReadUInt16();
		this._upgradeTargeting = br.ReadByte();
		this._upgradePenetration = br.ReadByte();
		this._upgradeCooldown = br.ReadByte();
		this._upgradeRange = br.ReadByte();
		this._upgradeDamage = br.ReadByte();
	}

	public override void Serialize(BinaryWriter bw)
	{
		base.Serialize(bw);
		bw.Write(this._ammoType);
		bw.Write(this._upgradeTargeting);
		bw.Write(this._upgradePenetration);
		bw.Write(this._upgradeCooldown);
		bw.Write(this._upgradeRange);
		bw.Write(this._upgradeDamage);
	}
}