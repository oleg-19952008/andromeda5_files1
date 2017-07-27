using System;
using System.IO;

public class GenericData : ITransferable
{
	public int int1;

	public int int2;

	public int int3;

	public int int4;

	public int int5;

	public long long1;

	public bool bool1;

	public string str1 = "";

	public GenericData()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.int1 = br.ReadInt32();
		this.int2 = br.ReadInt32();
		this.int3 = br.ReadInt32();
		this.int4 = br.ReadInt32();
		this.int5 = br.ReadInt32();
		this.long1 = br.ReadInt64();
		this.bool1 = br.ReadBoolean();
		this.str1 = AuthorizeRequest.SafeReadString(br);
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.int1);
		bw.Write(this.int2);
		bw.Write(this.int3);
		bw.Write(this.int4);
		bw.Write(this.int5);
		bw.Write(this.long1);
		bw.Write(this.bool1);
		AuthorizeRequest.SafeWriteString(this.str1, bw);
	}
}