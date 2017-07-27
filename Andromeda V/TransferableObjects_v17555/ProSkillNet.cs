using System;
using System.IO;

public class ProSkillNet : ITransferable
{
	public string Name;

	public string AssetName;

	public string Description;

	public byte MaxLvl;

	public byte PlayerLvl;

	public bool CanBuy;

	public byte ReqSkill1;

	public byte ReqLvl1;

	public byte ReqSkill2;

	public byte ReqLvl2;

	public byte Skillpoints;

	public int Price;

	public short _2H;

	public short _CH310CO102H;

	public ProSkillNet()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.Name = br.ReadString();
		this.AssetName = br.ReadString();
		this.Description = br.ReadString();
		this.MaxLvl = br.ReadByte();
		this.PlayerLvl = br.ReadByte();
		this.CanBuy = br.ReadBoolean();
		this.ReqSkill1 = br.ReadByte();
		this.ReqLvl1 = br.ReadByte();
		this.ReqSkill2 = br.ReadByte();
		this.ReqLvl2 = br.ReadByte();
		this.Skillpoints = br.ReadByte();
		this.Price = br.ReadInt32();
		this._2H = br.ReadInt16();
		this._CH310CO102H = br.ReadInt16();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.Name);
		bw.Write(this.AssetName);
		bw.Write(this.Description);
		bw.Write(this.MaxLvl);
		bw.Write(this.PlayerLvl);
		bw.Write(this.CanBuy);
		bw.Write(this.ReqSkill1);
		bw.Write(this.ReqLvl1);
		bw.Write(this.ReqSkill2);
		bw.Write(this.ReqLvl2);
		bw.Write(this.Skillpoints);
		bw.Write(this.Price);
		bw.Write(this._2H);
		bw.Write(this._CH310CO102H);
	}
}