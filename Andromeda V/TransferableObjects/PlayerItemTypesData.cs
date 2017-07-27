using System;
using System.IO;

public class PlayerItemTypesData : ITransferable
{
	public ushort itemType;

	public string uiName;

	public string assetName;

	public string description;

	public int priceCash;

	public int priceNova;

	public int priceViral;

	public short levelRestriction;

	public int sortIndex;

	public PlayerItemTypesData()
	{
	}

	public virtual void Deserialize(BinaryReader br)
	{
		this.itemType = br.ReadUInt16();
		this.uiName = br.ReadString();
		this.assetName = br.ReadString();
		this.description = br.ReadString();
		this.priceCash = br.ReadInt32();
		this.priceNova = br.ReadInt32();
		this.priceViral = br.ReadInt32();
		this.levelRestriction = br.ReadInt16();
		this.sortIndex = br.ReadInt32();
	}

	public virtual void Serialize(BinaryWriter bw)
	{
		bw.Write(this.itemType);
		bw.Write(this.uiName ?? "");
		bw.Write(this.assetName ?? "");
		bw.Write(this.description ?? "");
		bw.Write(this.priceCash);
		bw.Write(this.priceNova);
		bw.Write(this.priceViral);
		bw.Write(this.levelRestriction);
		bw.Write(this.sortIndex);
	}
}