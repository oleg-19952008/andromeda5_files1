using System;
using System.IO;

public class RegisterData : ITransferable
{
	public string userName = "";

	public string nickName = "";

	public string password = "";

	public int referedPlayer = 0;

	public string eMail = "";

	public byte fraction = 0;

	public byte world = 0;

	public RegisterData()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.userName = br.ReadString();
		this.nickName = br.ReadString();
		this.password = br.ReadString();
		this.referedPlayer = br.ReadInt32();
		this.eMail = br.ReadString();
		this.fraction = br.ReadByte();
		this.world = br.ReadByte();
	}

	public void FromString(string str)
	{
		string[] strArrays = str.Split(new char[] { ';' });
		this.userName = strArrays[0];
		this.nickName = strArrays[1];
		this.password = strArrays[2];
		this.eMail = strArrays[3];
		this.fraction = byte.Parse(strArrays[4]);
		if (!int.TryParse(strArrays[5], out this.referedPlayer))
		{
			this.referedPlayer = 0;
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.userName);
		bw.Write(this.nickName);
		bw.Write(this.password);
		bw.Write(this.referedPlayer);
		bw.Write(this.eMail);
		bw.Write(this.fraction);
		bw.Write(this.world);
	}

	public string TOString()
	{
		object[] objArray = new object[] { this.userName, this.nickName, this.password, this.eMail, this.fraction, this.referedPlayer };
		return string.Format("{0};{1};{2};{3};{4};{5}", objArray);
	}
}