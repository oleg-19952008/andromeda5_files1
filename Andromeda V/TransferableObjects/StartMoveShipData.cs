using System;
using System.IO;

public class StartMoveShipData : ITransferable
{
	public long movingPlayerId;

	public float currentX;

	public float currentZ;

	public float destinationX;

	public float destinationZ;

	public float momentSpeed;

	public float rotationY;

	public bool isStunned;

	public StartMoveShipData()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.movingPlayerId = br.ReadInt64();
		this.currentX = br.ReadSingle();
		this.currentZ = br.ReadSingle();
		this.destinationX = br.ReadSingle();
		this.destinationZ = br.ReadSingle();
		this.momentSpeed = br.ReadSingle();
		this.rotationY = br.ReadSingle();
		this.isStunned = br.ReadBoolean();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.movingPlayerId);
		bw.Write(this.currentX);
		bw.Write(this.currentZ);
		bw.Write(this.destinationX);
		bw.Write(this.destinationZ);
		bw.Write(this.momentSpeed);
		bw.Write(this.rotationY);
		bw.Write(this.isStunned);
	}
}