using System;
using System.IO;

public class UpgradeWeaponParams : ITransferable
{
	public int shipId;

	public byte damageUp;

	public byte cooldownUp;

	public byte rangeUp;

	public byte penetrationUp;

	public byte targetingUp;

	public int slotId;

	public byte slotType;

	public UpgradeWeaponParams()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.shipId = br.ReadInt32();
		this.damageUp = br.ReadByte();
		this.cooldownUp = br.ReadByte();
		this.rangeUp = br.ReadByte();
		this.penetrationUp = br.ReadByte();
		this.targetingUp = br.ReadByte();
		this.slotId = br.ReadInt32();
		this.slotType = br.ReadByte();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.shipId);
		bw.Write(this.damageUp);
		bw.Write(this.cooldownUp);
		bw.Write(this.rangeUp);
		bw.Write(this.penetrationUp);
		bw.Write(this.targetingUp);
		bw.Write(this.slotId);
		bw.Write(this.slotType);
	}
}