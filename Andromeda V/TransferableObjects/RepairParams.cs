using System;
using System.IO;

public class RepairParams : ITransferable
{
	public static int repairPrice;

	public int shipId;

	public RepairType repairType;

	static RepairParams()
	{
		RepairParams.repairPrice = 1000;
	}

	public RepairParams()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.shipId = br.ReadInt32();
		this.repairType = (RepairType)br.ReadByte();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.shipId);
		bw.Write((byte)this.repairType);
	}
}