using System;
using System.IO;

public class RegisterResult : ITransferable
{
	public bool isSuccessful;

	public string username = "";

	public int playerId;

	public string[] errorTexts = new string[0];

	public int[] errorFields = new int[0];

	public RegisterResult()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.isSuccessful = br.ReadBoolean();
		this.username = br.ReadString();
		int num = br.ReadInt32();
		this.errorFields = new int[num];
		this.errorTexts = new string[num];
		for (int i = 0; i < num; i++)
		{
			this.errorFields[i] = br.ReadInt32();
			this.errorTexts[i] = br.ReadString();
		}
		this.playerId = br.ReadInt32();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.isSuccessful);
		bw.Write(this.username);
		bw.Write((int)this.errorTexts.Length);
		for (int i = 0; i < (int)this.errorTexts.Length; i++)
		{
			bw.Write(this.errorFields[i]);
			bw.Write(this.errorTexts[i]);
		}
		bw.Write(this.playerId);
	}
}