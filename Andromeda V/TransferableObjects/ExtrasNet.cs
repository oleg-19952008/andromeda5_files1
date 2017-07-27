using System;
using System.IO;

public class ExtrasNet : PlayerItemTypesData, ITransferable
{
	public string type;

	public int efValue;

	public ExtrasNet()
	{
	}

	public override void Deserialize(BinaryReader br)
	{
		this.type = br.ReadString();
		this.efValue = br.ReadInt32();
		base.Deserialize(br);
	}

	public override void Serialize(BinaryWriter bw)
	{
		bw.Write(this.type ?? "");
		bw.Write(this.efValue);
		base.Serialize(bw);
	}
}