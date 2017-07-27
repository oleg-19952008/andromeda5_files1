using System;
using System.IO;

public class ResearchSkillParam : ITransferable
{
	public int skillID;

	public int priceSP;

	public int priceCredits;

	public int priceD;

	public int priceE;

	public ResearchSkillParam()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.skillID = br.ReadInt32();
		this.priceSP = br.ReadInt32();
		this.priceCredits = br.ReadInt32();
		this.priceD = br.ReadInt32();
		this.priceE = br.ReadInt32();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.skillID);
		bw.Write(this.priceSP);
		bw.Write(this.priceCredits);
		bw.Write(this.priceD);
		bw.Write(this.priceE);
	}
}