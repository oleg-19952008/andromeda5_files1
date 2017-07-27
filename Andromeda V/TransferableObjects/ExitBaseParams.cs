using System;
using System.IO;

public class ExitBaseParams : ITransferable
{
	public int baseId;

	public int doorNum;

	public long playerId;

	public PlayerObjectPhysics vessel;

	public PlayerBelongings belongings;

	public ExitBaseParams()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.baseId = br.ReadInt32();
		this.doorNum = br.ReadInt32();
		this.playerId = br.ReadInt64();
		this.vessel = new PlayerObjectPhysics();
		this.vessel.Deserialize(br);
		this.belongings = new PlayerBelongings();
		this.belongings.Deserialize(br);
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.baseId);
		bw.Write(this.doorNum);
		bw.Write(this.playerId);
		this.vessel.Serialize(bw);
		this.belongings.Serialize(bw);
	}
}