using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

public abstract class ProjectileObject : GameObjectPhysics, ITransferable
{
	public GameObjectPhysics shooter;

	public GameObjectPhysics target;

	public int damageHull;

	public int damageShield;

	public short weaponSlotId;

	public ushort selectedAmmoType;

	public static float SHIELD_RADIUS;

	[NonSerialized]
	public Action<ProjectileObject> RemoveProjectile;

	public TakeDamageDelegate TakeDamage;

	public float speed;

	protected float dt;

	public uint shooterNeibId;

	public uint targetNeibId;

	public bool IsHitting
	{
		get
		{
			bool flag;
			if (!this.shooter.IsDT)
			{
				float single = (this.target.IsPoP ? ((PlayerObjectPhysics)this.target).cfg.currentAvoidance / 2f : 0f);
				float single1 = (float)((PlayerObjectPhysics)this.shooter).cfg.targeting;
				WeaponSlot weaponSlot = (
					from ws in ((PlayerObjectPhysics)this.shooter).cfg.weaponSlots
					where ws.slotId == this.weaponSlotId
					select ws).First<WeaponSlot>();
				if ((weaponSlot.weaponTierId == PlayerItems.TypeWeaponLaserTire1 || weaponSlot.weaponTierId == PlayerItems.TypeWeaponLaserTire2 || weaponSlot.weaponTierId == PlayerItems.TypeWeaponLaserTire3 || weaponSlot.weaponTierId == PlayerItems.TypeWeaponLaserTire4 ? false : weaponSlot.weaponTierId != PlayerItems.TypeWeaponLaserTire5))
				{
					single1 = ((weaponSlot.weaponTierId == PlayerItems.TypeWeaponPlasmaTire1 || weaponSlot.weaponTierId == PlayerItems.TypeWeaponPlasmaTire2 || weaponSlot.weaponTierId == PlayerItems.TypeWeaponPlasmaTire3 || weaponSlot.weaponTierId == PlayerItems.TypeWeaponPlasmaTire4 ? false : weaponSlot.weaponTierId != PlayerItems.TypeWeaponPlasmaTire5) ? single1 + (float)((PlayerObjectPhysics)this.shooter).cfg.targetingForIon : single1 + (float)((PlayerObjectPhysics)this.shooter).cfg.targetingForPlasma);
				}
				else
				{
					single1 = single1 + (float)((PlayerObjectPhysics)this.shooter).cfg.targetingForLaser;
				}
				single1 = single1 + (
					from ws in ((PlayerObjectPhysics)this.shooter).cfg.weaponSlots
					where ws.slotId == this.weaponSlotId
					select ws).First<WeaponSlot>().totalTargeting;
				if (single1 >= 3f * single)
				{
					flag = PlayerObjectPhysics.rnd.MeasureChance(95f);
				}
				else if (single < 3f * single1)
				{
					flag = (single1 < single ? PlayerObjectPhysics.rnd.MeasureChance(50f - (single - single1) / single1 * 22.5f) : PlayerObjectPhysics.rnd.MeasureChance(50f + (single1 - single) / single * 22.5f));
				}
				else
				{
					flag = PlayerObjectPhysics.rnd.MeasureChance(5f);
				}
			}
			else
			{
				flag = true;
			}
			return flag;
		}
	}

	static ProjectileObject()
	{
		ProjectileObject.SHIELD_RADIUS = 1f;
	}

	protected ProjectileObject()
	{
	}

	public override void Deserialize(BinaryReader br)
	{
		this.shooterNeibId = br.ReadUInt32();
		this.targetNeibId = br.ReadUInt32();
		this.damageHull = br.ReadInt32();
		this.damageShield = br.ReadInt32();
		this.speed = br.ReadSingle();
		this.weaponSlotId = br.ReadInt16();
		this.selectedAmmoType = br.ReadUInt16();
		base.Deserialize(br);
	}

	protected virtual void DetectAndManageTargetCollision(float customAddDistance = 0f)
	{
		if (!this.isOnClientSide)
		{
			if (!this.isRemoved)
			{
				float distance = GameObjectPhysics.GetDistance(this.target.x, this.x, this.target.z, this.z);
				if ((!this.target.IsPoP ? false : ((PlayerObjectPhysics)this.target).cfg.shield > 1f))
				{
					distance = Math.Max(distance - ProjectileObject.SHIELD_RADIUS, 0f);
				}
				if ((distance < this.speed * this.dt ? true : distance < customAddDistance))
				{
					if (this.TakeDamage != null)
					{
						this.TakeDamage(this.shooter, this.target, this, this.damageHull, this.damageShield, this.IsHitting, false, 0, false);
					}
					this.RemoveProjectile(this);
				}
			}
		}
	}

	public override void Serialize(BinaryWriter bw)
	{
		uint num;
		uint num1;
		BinaryWriter binaryWriter = bw;
		if (this.shooter == null)
		{
			num =0;
		}
		else
		{
			num = this.shooter.neighbourhoodId;
		}
		binaryWriter.Write(num);
		BinaryWriter binaryWriter1 = bw;
		if (this.target == null)
		{
			num1 = 0;
		}
		else
		{
			num1 = this.target.neighbourhoodId;
		}
		binaryWriter1.Write(num1);
		bw.Write(this.damageHull);
		bw.Write(this.damageShield);
		bw.Write(this.speed);
		bw.Write(this.weaponSlotId);
		bw.Write(this.selectedAmmoType);
		base.Serialize(bw);
	}
}