using System;
using System.IO;

public class CheckpointObjectPhysics : GameObjectPhysics, ITransferable
{
	private static int RANGE_OF_ACTION;

	public int checkpointId;

	public string checkpointName;

	public string checkpointDescription;

	public short checkpointType;

	public short galaxyKey;

	static CheckpointObjectPhysics()
	{
		CheckpointObjectPhysics.RANGE_OF_ACTION = 15;
	}

	public CheckpointObjectPhysics()
	{
	}

	public override void CalculateObjectMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
	}

	public override void Deserialize(BinaryReader br)
	{
		this.checkpointId = br.ReadInt32();
		this.checkpointName = br.ReadString();
		this.checkpointDescription = br.ReadString();
		this.checkpointType = br.ReadInt16();
		this.galaxyKey = br.ReadInt16();
		base.Deserialize(br);
	}

	public void GetWorldPosition(out int wp_x, out int wp_z)
	{
		wp_x = (int)(this.x + (float)(this.galaxy.width / 2));
		wp_z = (int)(-1f * this.z + (float)(this.galaxy.height / 2));
	}

	public bool IsObjectInRange(GameObjectPhysics target)
	{
		return (target.x <= this.x - (float)CheckpointObjectPhysics.RANGE_OF_ACTION || target.x >= this.x + (float)CheckpointObjectPhysics.RANGE_OF_ACTION || target.z <= this.z - (float)CheckpointObjectPhysics.RANGE_OF_ACTION ? false : target.z < this.z + (float)CheckpointObjectPhysics.RANGE_OF_ACTION);
	}

	public override void Serialize(BinaryWriter bw)
	{
		bw.Write(this.checkpointId);
		bw.Write(this.checkpointName ?? "");
		bw.Write(this.checkpointDescription ?? "");
		bw.Write(this.checkpointType);
		bw.Write(this.galaxyKey);
		base.Serialize(bw);
	}
}