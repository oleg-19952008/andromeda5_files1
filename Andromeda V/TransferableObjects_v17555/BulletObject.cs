using System;
using System.IO;

public class BulletObject : ProjectileObject, ITransferable
{
	public float acceleration = 30f;

	public float maxSpeed = 130f;

	public BulletObject()
	{
	}

	public override void CalculateObjectMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		this.dt = dt;
		float single = 20f;
		this.dt = dt;
		if (this.speed < single)
		{
			this.speed = single;
		}
		BulletObject bulletObject = this;
		bulletObject.speed = bulletObject.speed + this.acceleration * dt;
		if (this.speed > this.maxSpeed)
		{
			this.speed = this.maxSpeed;
		}
		Victor3 victor3 = Victor3.MoveTowards(new Victor3(this.x, this.y, this.z), new Victor3(this.target.x, this.target.y, this.target.z), this.speed * dt);
		base.KeepInBoundary(ref victor3);
		dx = victor3.x - this.x;
		dy = victor3.y - this.y;
		dz = victor3.z - this.z;
		this.DetectAndManageTargetCollision(0f);
	}

	public override void Deserialize(BinaryReader br)
	{
		base.Deserialize(br);
	}

	public override void Serialize(BinaryWriter bw)
	{
		base.Serialize(bw);
	}
}