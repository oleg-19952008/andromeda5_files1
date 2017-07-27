using System;
using System.IO;

public class SelectThisShipParams : ITransferable
{
	public int shipID;

	public bool withItemMove = false;

	public SelectThisShipParams()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.shipID = br.ReadInt32();
		this.withItemMove = br.ReadBoolean();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.shipID);
		bw.Write(this.withItemMove);
	}
}