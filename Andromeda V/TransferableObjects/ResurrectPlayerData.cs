using System;
using System.IO;

public class ResurrectPlayerData : ITransferable
{
	public PlayerObjectPhysics p;

	public PlayerBelongings playerBelongings;

	public ResurrectPlayerData()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.playerBelongings = new PlayerBelongings();
		this.playerBelongings.Deserialize(br);
		this.p = new PlayerObjectPhysics();
		this.p.Deserialize(br);
	}

	public void Serialize(BinaryWriter bw)
	{
		this.playerBelongings.Serialize(bw);
		this.p.Serialize(bw);
	}
}