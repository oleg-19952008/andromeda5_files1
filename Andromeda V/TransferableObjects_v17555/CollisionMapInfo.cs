using System;
using System.IO;

public class CollisionMapInfo : ITransferable
{
	public float collisionsMapStep;

	public byte[] collisionsMapZipped;

	public short mapId;

	public CollisionMapInfo()
	{
	}

	public virtual void Deserialize(BinaryReader br)
	{
		int num = br.ReadInt32();
		if (num != 0)
		{
			this.collisionsMapZipped = br.ReadBytes(num);
		}
		else
		{
			this.collisionsMapZipped = null;
		}
		this.collisionsMapStep = br.ReadSingle();
		this.mapId = br.ReadInt16();
	}

	public virtual void Serialize(BinaryWriter bw)
	{
		if (this.collisionsMapZipped != null)
		{
			bw.Write((int)this.collisionsMapZipped.Length);
			bw.Write(this.collisionsMapZipped);
		}
		else
		{
			bw.Write(0);
		}
		bw.Write(this.collisionsMapStep);
		bw.Write(this.mapId);
	}
}