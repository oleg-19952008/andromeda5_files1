using System;
using System.IO;

public class GalaxiesJumpMap : ITransferable
{
	public int sourceGalaxyId;

	public int destinationGalaxyId;

	public int novaPrice;

	public int equilibriumPrice;

	public GalaxiesJumpMap()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.sourceGalaxyId = br.ReadInt32();
		this.destinationGalaxyId = br.ReadInt32();
		this.novaPrice = br.ReadInt32();
		this.equilibriumPrice = br.ReadInt32();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.sourceGalaxyId);
		bw.Write(this.destinationGalaxyId);
		bw.Write(this.novaPrice);
		bw.Write(this.equilibriumPrice);
	}
}