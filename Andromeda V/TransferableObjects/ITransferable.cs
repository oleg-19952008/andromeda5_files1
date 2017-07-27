using System;
using System.IO;

public interface ITransferable
{
	void Deserialize(BinaryReader br);

	void Serialize(BinaryWriter bw);
}