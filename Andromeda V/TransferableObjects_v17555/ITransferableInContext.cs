using System;
using System.IO;

public interface ITransferableInContext : ITransferable
{
	void DeserializeInContext(BinaryReader br, TransferContext context);

	void SerializeInContext(BinaryWriter bw, TransferContext context);
}