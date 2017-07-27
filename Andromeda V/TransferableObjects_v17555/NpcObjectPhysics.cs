using System;
using System.IO;

public class NpcObjectPhysics : GameObjectPhysics, ITransferable
{
	private static int RANGE_OF_ACTION;

	public short npcKey;

	public string npcName;

	public float locationX;

	public float locationY;

	public float locationZ;

	public string description;

	public int galaxyId;

	public byte fraction;

	public bool isPowerUpSeller;

	static NpcObjectPhysics()
	{
		NpcObjectPhysics.RANGE_OF_ACTION = 16;
	}

	public NpcObjectPhysics()
	{
	}

	public override void Deserialize(BinaryReader br)
	{
		this.npcKey = br.ReadInt16();
		this.npcName = br.ReadString();
		this.description = br.ReadString();
		this.locationX = br.ReadSingle();
		this.locationY = br.ReadSingle();
		this.locationZ = br.ReadSingle();
		this.galaxyId = br.ReadInt32();
		this.fraction = br.ReadByte();
		this.isPowerUpSeller = br.ReadBoolean();
		base.Deserialize(br);
	}

	public bool IsObjectInRange(GameObjectPhysics target)
	{
		return (target.x <= this.x - (float)NpcObjectPhysics.RANGE_OF_ACTION || target.x >= this.x + (float)NpcObjectPhysics.RANGE_OF_ACTION || target.z <= this.z - (float)NpcObjectPhysics.RANGE_OF_ACTION ? false : target.z < this.z + (float)NpcObjectPhysics.RANGE_OF_ACTION);
	}

	public override void Serialize(BinaryWriter bw)
	{
		bw.Write(this.npcKey);
		bw.Write(this.npcName ?? "");
		bw.Write(this.description ?? "");
		bw.Write(this.locationX);
		bw.Write(this.locationY);
		bw.Write(this.locationZ);
		bw.Write(this.galaxyId);
		bw.Write(this.fraction);
		bw.Write(this.isPowerUpSeller);
		base.Serialize(bw);
	}
}