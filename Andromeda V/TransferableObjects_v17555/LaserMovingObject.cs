using System;
using System.IO;

public class LaserMovingObject : ProjectileObject, ITransferable
{
	private bool isFirstMove = true;

	private Victor3 moveFrom;

	private Victor3 moveTo;

	private float distanceToDestroy = 0.3f;

	public LaserMovingObject()
	{
	}

	public override void CalculateObjectMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		if (!this.isRemoved)
		{
			if (this.isFirstMove)
			{
				this.isFirstMove = false;
				this.moveFrom = new Victor3(this.shooter.x, this.shooter.y, this.shooter.z);
				this.moveTo = new Victor3(this.target.x, this.target.y, this.target.z);
			}
			Victor3 victor3 = Victor3.MoveTowards(new Victor3(this.x, this.y, this.z), this.moveTo, dt * this.speed);
			dx = victor3.x - this.x;
			dy = victor3.y - this.y;
			dz = victor3.z - this.z;
			this.DetectAndManageTargetCollision(this.distanceToDestroy);
		}
	}

	public override void Deserialize(BinaryReader br)
	{
		this.isFirstMove = br.ReadBoolean();
		this.moveFrom.Deserialize(br);
		this.moveTo.Deserialize(br);
		this.distanceToDestroy = br.ReadSingle();
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
				float distance = GameObjectPhysics.GetDistance(this.moveTo.x, this.x, this.moveTo.z, this.z);
				if ((!this.target.IsPoP ? false : ((PlayerObjectPhysics)this.target).cfg.shield > 1f))
				{
					distance = Math.Max(distance - ProjectileObject.SHIELD_RADIUS, 0f);
				}
				if ((distance < this.speed * this.dt ? true : distance < customAddDistance))
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
		this.moveFrom.Serialize(bw);
		this.moveTo.Serialize(bw);
		bw.Write(this.distanceToDestroy);
		base.Serialize(bw);
	}
}