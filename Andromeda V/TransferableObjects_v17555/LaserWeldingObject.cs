using System;
using System.IO;

public class LaserWeldingObject : ProjectileObject, ITransferable
{
	private bool isFirstMove = true;

	private long laserStartTime;

	public float range = 100f;

	public LaserWeldingObject()
	{
	}

	public override void CalculateObjectMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		if (this.isFirstMove)
		{
			this.laserStartTime = DateTime.Now.Ticks;
			this.isFirstMove = false;
		}
		dx = 0f;
		dy = 0f;
		dz = 0f;
		this.DetectAndManageTargetCollision(0f);
	}

	public override void Deserialize(BinaryReader br)
	{
		this.isFirstMove = br.ReadBoolean();
		this.laserStartTime = br.ReadInt64();
		this.range = br.ReadSingle();
		base.Deserialize(br);
	}

	protected override void DetectAndManageTargetCollision(float customAddDistance = 0f)
	{
		bool flag;
		if (!this.isOnClientSide)
		{
			if (this.isRemoved)
			{
				flag = false;
			}
			else
			{
				flag = (!this.target.IsPoP ? true : ((PlayerObjectPhysics)this.target).isAlive);
			}
			if (flag)
			{
				float distance = GameObjectPhysics.GetDistance(this.target.x, this.shooter.x, this.target.z, this.shooter.z);
				if ((!this.target.IsPoP ? false : ((PlayerObjectPhysics)this.target).cfg.shield > 1f))
				{
					distance = Math.Max(distance - ProjectileObject.SHIELD_RADIUS, 0f);
				}
				if (distance > this.range)
				{
					this.RemoveProjectile(this);
				}
				else if (DateTime.Now.Ticks > this.laserStartTime + (long)(this.speed * 1E+07f / 10f))
				{
					if (this.TakeDamage != null)
					{
						this.TakeDamage(this.shooter, this.target, this, this.damageHull, this.damageShield, base.IsHitting, false, 0, false);
					}
					this.RemoveProjectile(this);
				}
			}
		}
	}

	public override void Serialize(BinaryWriter bw)
	{
		bw.Write(this.isFirstMove);
		bw.Write(this.laserStartTime);
		bw.Write(this.range);
		base.Serialize(bw);
	}
}