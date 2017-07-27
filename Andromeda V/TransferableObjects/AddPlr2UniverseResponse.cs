using System;
using System.IO;

public class AddPlr2UniverseResponse : ITransferable
{
	public bool isSuccessful;

	public long oldPlayId;

	public ushort mapPort;

	public bool isInBase;

	public int galaxyId;

	public byte returnCode;

	public AddPlr2UniverseResponse()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.isSuccessful = br.ReadBoolean();
		this.oldPlayId = br.ReadInt64();
		this.mapPort = br.ReadUInt16();
		this.isInBase = br.ReadBoolean();
		this.galaxyId = br.ReadInt32();
		this.returnCode = br.ReadByte();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.isSuccessful);
		bw.Write(this.oldPlayId);
		bw.Write(this.mapPort);
		bw.Write(this.isInBase);
		bw.Write(this.galaxyId);
		bw.Write(this.returnCode);
	}

	public override string ToString()
	{
		object[] objArray = new object[] { this.isSuccessful, this.oldPlayId, this.mapPort, this.isInBase, this.galaxyId, this.returnCode };
		return string.Format("AddPlr2UniverseResponse isSuccessful {0} oldPlayId {1} mapPort {2} isInBase {3} galaxyId {4} returnCode {5}", objArray);
	}
}