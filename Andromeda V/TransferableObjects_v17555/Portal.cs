using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Portal : ITransferable
{
	public int portalId;

	public string uiName;

	public string assetName;

	public short galaxyKey;

	public SortedList<ushort, short> parts;

	public Portal()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		int i;
		this.portalId = br.ReadInt32();
		this.uiName = br.ReadString();
		this.assetName = br.ReadString();
		this.galaxyKey = br.ReadInt16();
		int num = br.ReadInt32();
		ushort[] numArray = new ushort[num];
		for (i = 0; i < num; i++)
		{
			numArray[i] = br.ReadUInt16();
		}
		short[] numArray1 = new short[num];
		for (i = 0; i < num; i++)
		{
			numArray1[i] = br.ReadInt16();
		}
		this.parts = new SortedList<ushort, short>();
		for (i = 0; i < num; i++)
		{
			this.parts.Add(numArray[i], numArray1[i]);
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		int i;
		bw.Write(this.portalId);
		bw.Write(this.uiName ?? "");
		bw.Write(this.assetName ?? "");
		bw.Write(this.galaxyKey);
		ushort[] array = this.parts.Keys.ToArray<ushort>();
		short[] numArray = this.parts.Values.ToArray<short>();
		bw.Write((int)array.Length);
		for (i = 0; i < (int)array.Length; i++)
		{
			bw.Write(array[i]);
		}
		for (i = 0; i < (int)array.Length; i++)
		{
			bw.Write(numArray[i]);
		}
	}
}