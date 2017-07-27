using System;
using System.IO;

public class MakeSynthesisParams : ITransferable
{
	public ushort productItemType;

	public long amount;

	public MakeSynthesisParams()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.productItemType = br.ReadUInt16();
		this.amount = br.ReadInt64();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.productItemType);
		bw.Write(this.amount);
	}
}