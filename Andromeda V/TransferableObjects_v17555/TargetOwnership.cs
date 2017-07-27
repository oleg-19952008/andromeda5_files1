using System;

public class TargetOwnership
{
	public TargetOwnership.Type type;

	private PlayerObjectPhysics byPlayer = null;

	private PartyServerSide byParty = null;

	private int playerDbId = 0;

	public PvEPhysics pve = null;

	public int PlayerDbId
	{
		get
		{
			return this.playerDbId;
		}
	}

	public TargetOwnership(PvEPhysics parentPve)
	{
		this.pve = parentPve;
	}

	public TargetOwnership(PlayerData plr)
	{
		this.type = TargetOwnership.Type.Player;
		this.byPlayer = plr.vessel;
		this.playerDbId = plr.dbId;
	}

	public void Clear()
	{
		this.type = TargetOwnership.Type.None;
		this.byPlayer = null;
		this.byParty = null;
		this.playerDbId = 0;
	}

	public PartyServerSide GetParty()
	{
		return this.byParty;
	}

	public PlayerObjectPhysics GetPlayer()
	{
		return this.byPlayer;
	}

	public new TargetOwnership.Type GetType()
	{
		return this.type;
	}

	public enum Type
	{
		None,
		Player,
		Party
	}
}