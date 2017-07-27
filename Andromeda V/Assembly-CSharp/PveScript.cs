using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PveScript : MonoBehaviour
{
	public PvEPhysicsEx pve;

	public NetworkScript comm;

	private Quaternion relativeAnimatorRotation;

	private Vector3 scaleRatio;

	private DateTime scaleStartTime;

	private bool isScalingOut;

	private GameObject spawnEffect;

	public PveScript()
	{
	}

	public void _Despawn(uint pveId)
	{
		PvEPhysics item = (PvEPhysics)this.comm.gameObjects.get_Item(pveId);
		item.isShooting = false;
		item.isAlive = false;
		this.comm.gameObjects.Remove(pveId);
	}

	public void GotKilled(PlayerObjectPhysics me)
	{
		this.comm.RemoveGameObject(this.pve.neighbourhoodId);
		this.pve.isAlive = false;
		this.pve.speed = 0f;
		this.pve.moveState = 0;
		this.pve.rotationState = 0;
		this.pve.isShooting = false;
		NetworkScript.Expode(base.get_transform().get_position());
		IEnumerator<PlayerDataEx> enumerator = NetworkScript.clientSideClientsList.get_Values().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				PlayerDataEx current = enumerator.get_Current();
				try
				{
					if (current.shipScript.selectedObject == this.pve)
					{
						current.shipScript.selectedObject = null;
						Object.Destroy(current.shipScript._lock);
						current.shipScript._lock = null;
					}
				}
				catch (Exception exception)
				{
				}
				if (current.vessel.shootingAt != this.pve)
				{
					continue;
				}
				current.vessel.isShooting = false;
				current.vessel.shootingAt = null;
				if (current.vessel.isInParty)
				{
					continue;
				}
				string str = string.Format(StaticData.Translate("key_pve_kill_experience"), (float)((PvEPhysics)me).experience * (100f + current.cfg.experienceGain) / 100f);
				NetworkScript.spaceLabelManager.AddMessage(GuiNewStyleBar.orangeColor, str, current.vessel);
			}
		}
		finally
		{
			if (enumerator == null)
			{
			}
			enumerator.Dispose();
		}
	}

	private void Start()
	{
	}

	public void StartScaleOut()
	{
		this.scaleRatio = base.get_gameObject().get_transform().get_localScale();
		this.isScalingOut = true;
		this.scaleStartTime = DateTime.get_Now();
		base.get_gameObject().get_transform().set_localScale(new Vector3(0f, 0f, 0f));
		this.spawnEffect = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("HyperJumpOutPfb"));
		this.spawnEffect.get_transform().set_position(new Vector3(this.pve.x, 1f, this.pve.z));
	}

	private void Update()
	{
		if (this.pve == null)
		{
			Debug.Log("NULL pve property detected. Destroying game object.");
			Object.Destroy(base.get_gameObject());
			return;
		}
		if (!this.pve.isAlive)
		{
			return;
		}
		if (this.pve.isRemoved)
		{
			return;
		}
		float _deltaTime = Time.get_deltaTime();
		float single = 0f;
		float single1 = 0f;
		float single2 = 0f;
		this.pve.CalculateObjectMovement(_deltaTime, ref single, ref single1, ref single2);
		PvEPhysicsEx pvEPhysicsEx = this.pve;
		pvEPhysicsEx.x = pvEPhysicsEx.x + single;
		PvEPhysicsEx pvEPhysicsEx1 = this.pve;
		pvEPhysicsEx1.y = pvEPhysicsEx1.y + single1;
		PvEPhysicsEx pvEPhysicsEx2 = this.pve;
		pvEPhysicsEx2.z = pvEPhysicsEx2.z + single2;
		NetworkScript.ApplyShipPhysicsToGameObject(this.pve, base.get_gameObject());
		if (this.isScalingOut && base.get_gameObject() != null)
		{
			DateTime now = DateTime.get_Now();
			if (!(this.scaleStartTime.AddMilliseconds(1000) > now) || base.get_gameObject().get_transform().get_localScale().x >= this.scaleRatio.x)
			{
				this.isScalingOut = false;
				base.get_gameObject().get_transform().set_localScale(this.scaleRatio);
			}
			else
			{
				float totalSeconds = (float)(now - this.scaleStartTime).get_TotalSeconds();
				float single3 = totalSeconds * (this.scaleRatio.x * 1.1f);
				base.get_gameObject().get_transform().set_localScale(new Vector3(single3, single3, single3));
			}
			if (this.spawnEffect != null)
			{
				this.spawnEffect.get_transform().set_position(new Vector3(this.pve.x, 1f, this.pve.z));
			}
		}
	}
}