using System;
using System.IO;

public class PvESpawnZone
{
	public int id;

	public int galaxyId;

	public short x;

	public short z;

	public short width;

	public short height;

	public int pveTypeId;

	public short pveCount;

	public string pveType;

	public PvESpawnZone()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.id = br.ReadInt32();
		this.galaxyId = br.ReadInt32();
		this.x = br.ReadInt16();
		this.z = br.ReadInt16();
		this.width = br.ReadInt16();
		this.height = br.ReadInt16();
		this.pveTypeId = br.ReadInt32();
		this.pveCount = br.ReadInt16();
		this.pveType = br.ReadString();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.id);
		bw.Write(this.galaxyId);
		bw.Write(this.x);
		bw.Write(this.z);
		bw.Write(this.width);
		bw.Write(this.height);
		bw.Write(this.pveTypeId);
		bw.Write(this.pveCount);
		bw.Write(this.pveType);
	}
}