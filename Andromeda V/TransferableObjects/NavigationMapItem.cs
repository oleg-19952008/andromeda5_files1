using System;
using System.IO;

public struct NavigationMapItem : ITransferable
{
	public byte x;

	public byte y;

	public byte kind;

	public void Deserialize(BinaryReader br)
	{
		this.x = br.ReadByte();
		this.y = br.ReadByte();
		this.kind = br.ReadByte();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.x);
		bw.Write(this.y);
		bw.Write(this.kind);
	}
}