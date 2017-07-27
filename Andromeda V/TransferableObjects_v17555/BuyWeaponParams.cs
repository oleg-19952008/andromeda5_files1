using System;
using System.IO;

public class BuyWeaponParams : ITransferable
{
	public int weaponID;

	[NonSerialized]
	public int dbID;

	public BuyWeaponParams()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.weaponID = br.ReadInt32();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.weaponID);
	}
}