using System;
using System.IO;

public class AmmoType : ITransferable
{
	public int sortIndex;

	public byte ammoId;

	public string name;

	public string assetName;

	public int amount;

	public int amountMax;

	public float damageBonus;

	public float maxAccBonus;

	public float AccSpeedBonus;

	public AmmoType()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.sortIndex = br.ReadInt32();
		this.ammoId = br.ReadByte();
		this.name = br.ReadString();
		this.assetName = br.ReadString();
		this.amount = br.ReadInt32();
		this.amountMax = br.ReadInt32();
		this.damageBonus = br.ReadSingle();
		this.maxAccBonus = br.ReadSingle();
		this.AccSpeedBonus = br.ReadSingle();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.sortIndex);
		bw.Write(this.ammoId);
		bw.Write(this.name);
		bw.Write(this.assetName);
		bw.Write(this.amount);
		bw.Write(this.amountMax);
		bw.Write(this.damageBonus);
		bw.Write(this.maxAccBonus);
		bw.Write(this.AccSpeedBonus);
	}
}