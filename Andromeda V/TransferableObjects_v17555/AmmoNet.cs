using System;
using System.IO;

public class AmmoNet : PlayerItemTypesData, ITransferable
{
	public short damage;

	public AmmoNet()
	{
	}

	public override void Deserialize(BinaryReader br)
	{
		this.damage = br.ReadInt16();
		base.Deserialize(br);
	}

	public override void Serialize(BinaryWriter bw)
	{
		bw.Write(this.damage);
		base.Serialize(bw);
	}
}