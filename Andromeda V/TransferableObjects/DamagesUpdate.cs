using System;
using System.IO;

public class DamagesUpdate : ITransferable
{
	public uint hitterNbId;

	public DamageUpdateItem[] damages;

	public DamagesUpdate()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.hitterNbId = br.ReadUInt32();
		int num = br.ReadInt32();
		this.damages = new DamageUpdateItem[num];
		for (int i = 0; i < num; i++)
		{
			DamageUpdateItem[] damageUpdateItemArray = this.damages;
			DamageUpdateItem damageUpdateItem = new DamageUpdateItem()
			{
				targetNbId = br.ReadUInt32(),
				damageHealth = br.ReadInt32(),
				damageShield = br.ReadSingle(),
				healthAfterHit = br.ReadInt32(),
				shieldAfterHit = br.ReadSingle(),
				criticalEnergy = br.ReadSingle(),
				isKill = br.ReadBoolean(),
				isAbsorbed = br.ReadBoolean(),
				isSlowedFromAmmo = br.ReadBoolean(),
				projX = br.ReadSingle(),
				projY = br.ReadSingle(),
				projZ = br.ReadSingle()
			};
			damageUpdateItemArray[i] = damageUpdateItem;
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.hitterNbId);
		bw.Write((int)this.damages.Length);
		DamageUpdateItem[] damageUpdateItemArray = this.damages;
		for (int i = 0; i < (int)damageUpdateItemArray.Length; i++)
		{
			DamageUpdateItem damageUpdateItem = damageUpdateItemArray[i];
			bw.Write(damageUpdateItem.targetNbId);
			bw.Write(damageUpdateItem.damageHealth);
			bw.Write(damageUpdateItem.damageShield);
			bw.Write(damageUpdateItem.healthAfterHit);
			bw.Write(damageUpdateItem.shieldAfterHit);
			bw.Write(damageUpdateItem.criticalEnergy);
			bw.Write(damageUpdateItem.isKill);
			bw.Write(damageUpdateItem.isAbsorbed);
			bw.Write(damageUpdateItem.isSlowedFromAmmo);
			bw.Write(damageUpdateItem.projX);
			bw.Write(damageUpdateItem.projY);
			bw.Write(damageUpdateItem.projZ);
		}
	}
}