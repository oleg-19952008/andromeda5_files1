using System;
using System.IO;

public class KeyboardShortcutPair : ITransferable
{
	public byte commandIndex;

	public long dbKeyCode;

	public KeyboardShortcutPair()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.commandIndex = br.ReadByte();
		this.dbKeyCode = br.ReadInt64();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.commandIndex);
		bw.Write(this.dbKeyCode);
	}
}