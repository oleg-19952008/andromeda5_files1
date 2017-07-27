using System;
using System.IO;

public class HyperJumpNet : GameObjectPhysics, ITransferable
{
	[NonSerialized]
	public static float TakeZoneMinX;

	[NonSerialized]
	public static float TakeZoneMaxX;

	[NonSerialized]
	public static float TakeZoneMinZ;

	[NonSerialized]
	public static float TakeZoneMaxZ;

	public int id;

	public int galaxySrc;

	public int galaxyDst;

	public float dstX;

	public float dstY;

	public float dstZ;

	public int fractionId;

	static HyperJumpNet()
	{
		HyperJumpNet.TakeZoneMinX = -20f;
		HyperJumpNet.TakeZoneMaxX = 20f;
		HyperJumpNet.TakeZoneMinZ = -20f;
		HyperJumpNet.TakeZoneMaxZ = 20f;
	}

	public HyperJumpNet()
	{
	}

	public override void Deserialize(BinaryReader br)
	{
		this.id = br.ReadInt32();
		this.fractionId = br.ReadInt32();
		this.galaxySrc = br.ReadInt32();
		this.galaxyDst = br.ReadInt32();
		this.dstX = br.ReadSingle();
		this.dstY = br.ReadSingle();
		this.dstZ = br.ReadSingle();
		base.Deserialize(br);
	}

	public bool IsObjectInRange(GameObjectPhysics obj)
	{
		float takeZoneMinX = this.x + HyperJumpNet.TakeZoneMinX;
		float takeZoneMaxX = this.x + HyperJumpNet.TakeZoneMaxX;
		float takeZoneMinZ = this.z + HyperJumpNet.TakeZoneMinZ;
		float takeZoneMaxZ = this.z + HyperJumpNet.TakeZoneMaxZ;
		return (obj.x <= takeZoneMinX || obj.x >= takeZoneMaxX || obj.z <= takeZoneMinZ ? false : obj.z < takeZoneMaxZ);
	}

	public override void Serialize(BinaryWriter bw)
	{
		bw.Write(this.id);
		bw.Write(this.fractionId);
		bw.Write(this.galaxySrc);
		bw.Write(this.galaxyDst);
		bw.Write(this.dstX);
		bw.Write(this.dstY);
		bw.Write(this.dstZ);
		base.Serialize(bw);
	}
}