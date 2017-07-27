using System;
using System.IO;

public class LevelsInfo : ITransferable
{
	public byte level;

	public long tottalExp;

	public int cashReward;

	public int novaReward;

	public ushort neuronReward;

	public ushort itemTypeReward;

	public LevelsInfo()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.level = br.ReadByte();
		this.tottalExp = br.ReadInt64();
		this.cashReward = br.ReadInt32();
		this.novaReward = br.ReadInt32();
		this.neuronReward = br.ReadUInt16();
		this.itemTypeReward = br.ReadUInt16();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.level);
		bw.Write(this.tottalExp);
		bw.Write(this.cashReward);
		bw.Write(this.novaReward);
		bw.Write(this.neuronReward);
		bw.Write(this.itemTypeReward);
	}
}