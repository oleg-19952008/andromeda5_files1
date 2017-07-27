using System;
using System.IO;

public class RocketObject : ProjectileObject, ITransferable
{
	[NonSerialized]
	public Victor3 rotation;

	public float acceleration = 30f;

	public float maxSpeed = 130f;

	public RocketObject()
	{
	}

	public override void CalculateObjectMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		float single = 20f;
		this.dt = dt;
		if (this.speed < single)
		{
			this.speed = single;
		}
		RocketObject rocketObject = this;
		rocketObject.speed = rocketObject.speed + this.acceleration * dt;
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
		this.acceleration = br.ReadSingle();
		this.maxSpeed = br.ReadSingle();
		base.Deserialize(br);
	}

	protected override void DetectAndManageTargetCollision(float customAddDistance = 0f)
	{
		base.DetectAndManageTargetCollision(1f);
	}

	public override void Serialize(BinaryWriter bw)
	{
		bw.Write(this.acceleration);
		bw.Write(this.maxSpeed);
		base.Serialize(bw);
	}
}