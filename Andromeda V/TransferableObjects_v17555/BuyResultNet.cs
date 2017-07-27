using System;
using System.IO;

public class BuyResultNet : ITransferable
{
	public BuyResult buyResult;

	public BuyResultNet()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.buyResult = (BuyResult)br.ReadByte();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write((byte)this.buyResult);
	}
}