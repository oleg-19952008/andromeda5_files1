using System;

public class DamageUpdateItem
{
	public uint targetNbId;

	public int damageHealth;

	public float damageShield;

	public int healthAfterHit;

	public float shieldAfterHit;

	public float criticalEnergy;

	public bool isKill;

	public bool isAbsorbed = false;

	public bool isSlowedFromAmmo = false;

	public float projX;

	public float projY;

	public float projZ;

	public DamageUpdateItem()
	{
	}
}