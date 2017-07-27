using System;
using System.IO;

public class PvPGameType : ITransferable
{
	public PvPGameMode mode;

	public PvPWinType winType;

	public short id;

	public string name = "";

	public int winParam;

	public short respawnParam;

	public string description = "";

	public string sceneName = "";

	public PvPMap[] pvpMaps;

	public PvPMap selectedMap;

	public PvPGameType()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.mode = (PvPGameMode)br.ReadByte();
		this.winType = (PvPWinType)br.ReadByte();
		this.id = br.ReadInt16();
		this.name = br.ReadString();
		this.winParam = br.ReadInt32();
		this.respawnParam = br.ReadInt16();
		this.description = br.ReadString();
		this.sceneName = br.ReadString();
		short num = br.ReadInt16();
		if (num != -1)
		{
			this.selectedMap = new PvPMap()
			{
				pvpGameId = this.id,
				galaxyId = num
			};
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write((byte)this.mode);
		bw.Write((byte)this.winType);
		bw.Write(this.id);
		bw.Write(this.name ?? "");
		bw.Write(this.winParam);
		bw.Write(this.respawnParam);
		bw.Write(this.description);
		bw.Write(this.sceneName);
		if (this.selectedMap != null)
		{
			bw.Write(this.selectedMap.galaxyId);
		}
		else
		{
			bw.Write((short)-1);
		}
	}
}