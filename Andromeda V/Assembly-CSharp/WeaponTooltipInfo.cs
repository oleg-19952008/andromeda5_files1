using System;
using UnityEngine;

public class WeaponTooltipInfo
{
	public Vector2 position;

	public WeaponsTypeNet weapon;

	public WeaponSlot weaponSlot;

	public WeaponTooltipInfo(Vector2 p, WeaponsTypeNet w, WeaponSlot ws)
	{
		this.position = p;
		this.weapon = w;
		this.weaponSlot = ws;
	}
}