using System;
using System.Collections.Generic;
using System.IO;

public class WeaponSlot : ITransferable
{
	public int skillDamage;

	public int weaponPenetration;

	public long realReloadTime;

	public ushort totalDamageHull;

	public ushort totalDamageShield;

	public float totalShootRange;

	public float totalTargeting;

	public short slotId;

	public int timeToNextShot;

	public long lastShotTime;

	public bool isActive;

	public bool isAttached;

	public ushort selectedAmmoItemType;

	public ushort weaponTierId;

	public ShipConfiguration padre;

	public EWeaponStatus WeaponStatus
	{
		get
		{
			EWeaponStatus eWeaponStatu;
			if (!this.isAttached)
			{
				eWeaponStatu = EWeaponStatus.NoWeapon;
			}
			else if (!this.isActive)
			{
				eWeaponStatu = EWeaponStatus.WeaponOff;
			}
			else if (!this.IsOutOfAmmo())
			{
				eWeaponStatu = (this.lastShotTime + this.realReloadTime <= DateTime.Now.Ticks ? EWeaponStatus.ReadyToShoot : EWeaponStatus.Reloading);
			}
			else
			{
				eWeaponStatu = EWeaponStatus.OutOfAmmo;
			}
			return eWeaponStatu;
		}
	}

	public WeaponSlot()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.realReloadTime = br.ReadInt64();
		this.totalDamageHull = br.ReadUInt16();
		this.totalDamageShield = br.ReadUInt16();
		this.totalShootRange = br.ReadSingle();
		this.totalTargeting = br.ReadSingle();
		this.slotId = br.ReadInt16();
		this.timeToNextShot = br.ReadInt32();
		this.lastShotTime = br.ReadInt64();
		this.isActive = br.ReadBoolean();
		this.isAttached = br.ReadBoolean();
		this.selectedAmmoItemType = br.ReadUInt16();
		this.weaponTierId = br.ReadUInt16();
	}

	private bool IsOutOfAmmo()
	{
		bool flag;
		int count = this.padre.playerItems.slotItems.Count;
		int num = 0;
		while (true)
		{
			if (num >= count)
			{
				flag = true;
				break;
			}
			else if ((this.padre.playerItems.slotItems[num].ItemType != this.selectedAmmoItemType || this.padre.playerItems.slotItems[num].SlotType != 1 ? true : this.padre.playerItems.slotItems[num].Amount <= 0))
			{
				num++;
			}
			else
			{
				flag = false;
				break;
			}
		}
		return flag;
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.realReloadTime);
		bw.Write(this.totalDamageHull);
		bw.Write(this.totalDamageShield);
		bw.Write(this.totalShootRange);
		bw.Write(this.totalTargeting);
		bw.Write(this.slotId);
		bw.Write(this.timeToNextShot);
		bw.Write(this.lastShotTime);
		bw.Write(this.isActive);
		bw.Write(this.isAttached);
		bw.Write(this.selectedAmmoItemType);
		bw.Write(this.weaponTierId);
	}
}