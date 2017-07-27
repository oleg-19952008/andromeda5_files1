using System;
using System.IO;

public class ActiveSkillSlot : ITransferable
{
	public short range;

	public int cooldown;

	public int skillId;

	public short slotId;

	public bool isConfigured;

	public long nextCastTime;

	public EWeaponStatus SkillStatus
	{
		get
		{
			EWeaponStatus eWeaponStatu;
			if (this.isConfigured)
			{
				eWeaponStatu = (this.nextCastTime <= StaticData.now.Ticks ? EWeaponStatus.ReadyToShoot : EWeaponStatus.Reloading);
			}
			else
			{
				eWeaponStatu = EWeaponStatus.NoWeapon;
			}
			return eWeaponStatu;
		}
	}

	public ActiveSkillSlot()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.range = br.ReadInt16();
		this.cooldown = br.ReadInt32();
		this.skillId = br.ReadInt32();
		this.slotId = br.ReadInt16();
		this.isConfigured = br.ReadBoolean();
		long num = br.ReadInt64();
		this.nextCastTime = DateTime.Now.Ticks + num;
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.range);
		bw.Write(this.cooldown);
		bw.Write(this.skillId);
		bw.Write(this.slotId);
		bw.Write(this.isConfigured);
		bw.Write(this.nextCastTime - StaticData.now.Ticks);
	}
}