using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class InternalXSComunicationClass : ITransferable
{
	public int playerDbId;

	public int dbReccordId;

	public byte commandCode;

	public List<int> listOfInts = new List<int>();

	public InternalXSComunicationClass()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.playerDbId = br.ReadInt32();
		this.dbReccordId = br.ReadInt32();
		this.commandCode = br.ReadByte();
		int num = br.ReadInt32();
		for (int i = 0; i < num; i++)
		{
			this.listOfInts.Add(br.ReadInt32());
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.playerDbId);
		bw.Write(this.dbReccordId);
		bw.Write(this.commandCode);
		int num = this.listOfInts.Count<int>();
		bw.Write(num);
		for (int i = 0; i < num; i++)
		{
			bw.Write(this.listOfInts[i]);
		}
	}
}