using System;
using System.IO;

public class LoadWeaponParams : ITransferable
{
	public ushort weaponAmmoType;

	public int weaponID;

	[NonSerialized]
	public int dbID;

	public LoadWeaponParams()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.weaponAmmoType = br.ReadUInt16();
		this.weaponID = br.ReadInt32();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.weaponAmmoType);
		bw.Write(this.weaponID);
	}
}