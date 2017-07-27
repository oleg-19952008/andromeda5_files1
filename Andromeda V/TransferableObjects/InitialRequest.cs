using System;
using System.IO;

public class InitialRequest : ITransferableInContext, ITransferable
{
	public short version = 0;

	public string chosenLang = "en";

	public short launcherVersion = 3;

	public InitialRequest()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		throw new NotImplementedException();
	}

	public void DeserializeInContext(BinaryReader br, TransferContext context)
	{
		if (context != TransferContext.InitialRequestV1)
		{
			throw new NotImplementedException();
		}
		this.launcherVersion = br.ReadInt16();
		this.version = br.ReadInt16();
		this.chosenLang = br.ReadString();
	}

	public void Serialize(BinaryWriter bw)
	{
		throw new NotImplementedException();
	}

	public void SerializeInContext(BinaryWriter bw, TransferContext context)
	{
		if (context != TransferContext.InitialRequestV1)
		{
			throw new NotImplementedException();
		}
		bw.Write(this.launcherVersion);
		bw.Write(this.version);
		bw.Write(this.chosenLang);
	}
}