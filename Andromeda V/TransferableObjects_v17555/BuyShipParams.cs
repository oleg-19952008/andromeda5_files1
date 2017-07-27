using System;
using System.IO;

public class BuyShipParams : ITransferable
{
	public string nickName = "";

	public int shipID;

	public SelectedCurrency paymentCurrency;

	public BuyShipParams()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.nickName = br.ReadString();
		this.shipID = br.ReadInt32();
		this.paymentCurrency = (SelectedCurrency)br.ReadByte();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.nickName);
		bw.Write(this.shipID);
		bw.Write((byte)this.paymentCurrency);
	}
}