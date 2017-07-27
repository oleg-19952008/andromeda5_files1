using System;
using System.IO;

public class ShootingPlayer : ITransferable
{
	public float shooterX;

	public float shooterZ;

	public float shooterRotationW;

	public float shooterRotationX;

	public float shooterRotationY;

	public float shooterRotationZ;

	public long targetId;

	public WeaponSlot[] shootingSlots;

	public AmmoType[] availableAmmo;

	public long shooterId;

	public ShootingPlayer()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		int i;
		this.shooterX = br.ReadSingle();
		this.shooterZ = br.ReadSingle();
		this.shooterRotationW = br.ReadSingle();
		this.shooterRotationX = br.ReadSingle();
		this.shooterRotationY = br.ReadSingle();
		this.shooterRotationZ = br.ReadSingle();
		this.targetId = br.ReadInt64();
		int num = br.ReadInt32();
		this.shootingSlots = new WeaponSlot[num];
		for (i = 0; i < num; i++)
		{
			this.shootingSlots[i] = new WeaponSlot();
			this.shootingSlots[i].Deserialize(br);
		}
		num = br.ReadInt32();
		this.availableAmmo = new AmmoType[num];
		for (i = 0; i < num; i++)
		{
			this.availableAmmo[i] = new AmmoType();
			this.availableAmmo[i].Deserialize(br);
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		int i;
		bw.Write(this.shooterX);
		bw.Write(this.shooterZ);
		bw.Write(this.shooterRotationW);
		bw.Write(this.shooterRotationX);
		bw.Write(this.shooterRotationY);
		bw.Write(this.shooterRotationZ);
		bw.Write(this.targetId);
		int length = (int)this.shootingSlots.Length;
		bw.Write(length);
		for (i = 0; i < length; i++)
		{
			this.shootingSlots[i].Serialize(bw);
		}
		length = (int)this.availableAmmo.Length;
		bw.Write(length);
		for (i = 0; i < length; i++)
		{
			this.availableAmmo[i].Serialize(bw);
		}
		bw.Write(this.shooterId);
	}
}