using System;
using System.IO;

public class WeaponAmmoTypeChange : ITransferable
{
	public byte slotType;

	public ushort slot;

	public int shipId;

	public ushort ammoType;

	public WeaponAmmoTypeChange()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.slotType = br.ReadByte();
		this.slot = br.ReadUInt16();
		this.shipId = br.ReadInt32();
		this.ammoType = br.ReadUInt16();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.slotType);
		bw.Write(this.slot);
		bw.Write(this.shipId);
		bw.Write(this.ammoType);
	}
}