using System;
using System.IO;

public class ActiveSkillParams : ITransferable
{
	public uint casterNbId;

	public uint targetNbId;

	public float corX;

	public float corY;

	public float corZ;

	public ushort skillId;

	public byte targetType;

	public bool isCriticalHit;

	public ActiveSkillParams()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.casterNbId = br.ReadUInt32();
		this.targetNbId = br.ReadUInt32();
		this.corX = br.ReadSingle();
		this.corY = br.ReadSingle();
		this.corZ = br.ReadSingle();
		this.skillId = br.ReadUInt16();
		this.targetType = br.ReadByte();
		this.isCriticalHit = br.ReadBoolean();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.casterNbId);
		bw.Write(this.targetNbId);
		bw.Write(this.corX);
		bw.Write(this.corY);
		bw.Write(this.corZ);
		bw.Write(this.skillId);
		bw.Write(this.targetType);
		bw.Write(this.isCriticalHit);
	}
}