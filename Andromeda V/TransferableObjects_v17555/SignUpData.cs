using System;
using System.IO;

public class SignUpData : ITransferable
{
	public string user = "";

	public string nickName = "";

	public string email = "";

	public string passwordHash = "";

	public bool isFacebook;

	public SelectedCurrency currency;

	public SignUpData()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.user = AuthorizeRequest.SafeReadString(br);
		this.nickName = AuthorizeRequest.SafeReadString(br);
		this.email = AuthorizeRequest.SafeReadString(br);
		this.passwordHash = AuthorizeRequest.SafeReadString(br);
		this.isFacebook = br.ReadBoolean();
		this.currency = (SelectedCurrency)br.ReadByte();
	}

	public void Serialize(BinaryWriter bw)
	{
		AuthorizeRequest.SafeWriteString(this.user, bw);
		AuthorizeRequest.SafeWriteString(this.nickName, bw);
		AuthorizeRequest.SafeWriteString(this.email, bw);
		AuthorizeRequest.SafeWriteString(this.passwordHash, bw);
		bw.Write(this.isFacebook);
		bw.Write((byte)this.currency);
	}
}