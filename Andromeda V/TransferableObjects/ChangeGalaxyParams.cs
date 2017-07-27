using System;
using System.IO;

public class ChangeGalaxyParams : ITransferable
{
	public short oldGalaxy;

	public short newGalaxy;

	public ushort newGalaxyPort;

	public float newX;

	public float newY;

	public float newZ;

	public ChangeGalaxyParams()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.oldGalaxy = br.ReadInt16();
		this.newGalaxy = br.ReadInt16();
		this.newGalaxyPort = br.ReadUInt16();
		this.newX = br.ReadSingle();
		this.newY = br.ReadSingle();
		this.newZ = br.ReadSingle();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.oldGalaxy);
		bw.Write(this.newGalaxy);
		bw.Write(this.newGalaxyPort);
		bw.Write(this.newX);
		bw.Write(this.newY);
		bw.Write(this.newZ);
	}
}