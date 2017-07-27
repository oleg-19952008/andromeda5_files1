using System;
using System.IO;

public class Location : GameObjectPhysics, ITransferable
{
	public float radius;

	public Location()
	{
	}

	public override void Deserialize(BinaryReader br)
	{
		base.Deserialize(br);
		this.radius = br.ReadSingle();
	}

	public override void Serialize(BinaryWriter bw)
	{
		base.Serialize(bw);
		bw.Write(this.radius);
	}
}