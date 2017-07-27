using System;
using System.IO;

public class ShortAndBool : ITransferable
{
	public short theShort;

	public bool theBool;

	public ShortAndBool()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.theShort = br.ReadInt16();
		this.theBool = br.ReadBoolean();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.theShort);
		bw.Write(this.theBool);
	}
}