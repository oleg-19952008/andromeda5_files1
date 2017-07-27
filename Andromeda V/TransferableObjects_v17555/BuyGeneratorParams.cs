using System;
using System.IO;

public class BuyGeneratorParams : ITransferable
{
	public int generatorID;

	public string generatorType;

	[NonSerialized]
	public int dbID;

	public BuyGeneratorParams()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.generatorID = br.ReadInt32();
		this.generatorType = br.ReadString();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.generatorID);
		bw.Write(this.generatorType);
	}
}