using System;
using System.IO;

public class GeneratorNet : PlayerItemTypesData, ITransferable
{
	public short bonusValue;

	public GeneratorNet()
	{
	}

	public override void Deserialize(BinaryReader br)
	{
		this.bonusValue = br.ReadInt16();
		base.Deserialize(br);
	}

	public override void Serialize(BinaryWriter bw)
	{
		bw.Write(this.bonusValue);
		base.Serialize(bw);
	}
}