using System;
using System.Collections.Generic;
using System.IO;

public class ValidationErrors : ITransferableInContext, ITransferable
{
	public TransferContext context;

	public List<KeyValuePair<int, ErrorCode>> errors;

	public ValidationErrors()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		int num = br.ReadInt32();
		if (num != -1)
		{
			this.errors = new List<KeyValuePair<int, ErrorCode>>(num);
			for (int i = 0; i < num; i++)
			{
				int num1 = br.ReadInt32();
				ErrorCode errorCode = (ErrorCode)br.ReadInt16();
				this.errors.Add(new KeyValuePair<int, ErrorCode>(num1, errorCode));
			}
		}
		else
		{
			this.errors = null;
		}
	}

	public void DeserializeInContext(BinaryReader br, TransferContext context)
	{
		this.Deserialize(br);
	}

	public void Serialize(BinaryWriter bw)
	{
		if (this.errors != null)
		{
			bw.Write(this.errors.Count);
			foreach (KeyValuePair<int, ErrorCode> error in this.errors)
			{
				bw.Write(error.Key);
				bw.Write((short)error.Value);
			}
		}
		else
		{
			bw.Write(-1);
		}
	}

	public void SerializeInContext(BinaryWriter bw, TransferContext context)
	{
		this.Serialize(bw);
	}
}