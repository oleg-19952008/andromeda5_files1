using System;
using System.IO;

public class RenameShipParams : ITransferable
{
	public int playerShipId;

	public string newName;

	public RenameShipParams()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.playerShipId = br.ReadInt32();
		this.newName = br.ReadString();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.playerShipId);
		bw.Write(this.newName);
	}
}