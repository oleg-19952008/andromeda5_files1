using System;
using System.Collections;
using System.IO;
using TransferableObjects;

public class LevelMap : ITransferable
{
	public StarBaseNet[] starBases;

	public NpcObjectPhysics[] npcPopulation;

	public HyperJumpNet[] hyperJumps;

	public ExtractionPoint[] extractionPoints;

	public CheckpointObjectPhysics[] checkpoints;

	[NonSerialized]
	public short minX;

	[NonSerialized]
	public short minZ;

	[NonSerialized]
	public short maxX;

	[NonSerialized]
	public short maxZ;

	public short reqMinLevel;

	public short reqMaxLevel;

	public string description;

	public byte accessLevel;

	public bool isPveMap = true;

	public short galaxyKey;

	public short __galaxyId;

	public string nameUI;

	public ushort width;

	public ushort height;

	public short fraction;

	public string scenename;

	public byte universeId;

	public string minimapAssetName;

	public ushort broadcastPort;

	public ushort commandListenPort;

	public bool isCollisionAware;

	public float collisionsMapStep;

	public bool isInstance;

	public byte[] collisionsMapZipped;

	public BitArray[] collisionsMap;

	public int instanceMapOrigin;

	public short commandListenPortS
	{
		set
		{
			this.commandListenPort = (ushort)value;
		}
	}

	public short galaxyId
	{
		get
		{
			return this.__galaxyId;
		}
		set
		{
			this.__galaxyId = value;
		}
	}

	public ushort Height
	{
		get
		{
			return this.height;
		}
		set
		{
			this.height = value;
			this.maxZ = (short)(this.height / 2);
			this.minZ = (short)(-this.maxZ);
		}
	}

	public short heightS
	{
		set
		{
			this.Height = (ushort)value;
		}
	}

	public bool IsFactionGalaxy
	{
		get
		{
			return this.__galaxyId > 4000;
		}
	}

	public ushort Width
	{
		get
		{
			return this.width;
		}
		set
		{
			this.width = value;
			this.maxX = (short)(this.width / 2);
			this.minX = (short)(-this.maxX);
		}
	}

	public short widthS
	{
		set
		{
			this.Width = (ushort)value;
		}
	}

	public LevelMap()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		int i;
		this.starBases = new StarBaseNet[br.ReadInt32()];
		for (i = 0; i < (int)this.starBases.Length; i++)
		{
			this.starBases[i] = new StarBaseNet();
			this.starBases[i].Deserialize(br);
		}
		this.npcPopulation = new NpcObjectPhysics[br.ReadInt32()];
		for (i = 0; i < (int)this.npcPopulation.Length; i++)
		{
			this.npcPopulation[i] = new NpcObjectPhysics();
			this.npcPopulation[i].Deserialize(br);
		}
		this.hyperJumps = new HyperJumpNet[br.ReadInt32()];
		for (i = 0; i < (int)this.hyperJumps.Length; i++)
		{
			this.hyperJumps[i] = new HyperJumpNet();
			this.hyperJumps[i].Deserialize(br);
		}
		this.extractionPoints = new ExtractionPoint[br.ReadInt32()];
		for (i = 0; i < (int)this.extractionPoints.Length; i++)
		{
			this.extractionPoints[i] = new ExtractionPoint();
			this.extractionPoints[i].Deserialize(br);
		}
		this.checkpoints = new CheckpointObjectPhysics[br.ReadInt32()];
		for (i = 0; i < (int)this.checkpoints.Length; i++)
		{
			this.checkpoints[i] = new CheckpointObjectPhysics();
			this.checkpoints[i].Deserialize(br);
		}
		this.galaxyKey = br.ReadInt16();
		this.galaxyId = br.ReadInt16();
		this.nameUI = br.ReadString();
		this.width = br.ReadUInt16();
		this.height = br.ReadUInt16();
		this.scenename = br.ReadString();
		this.universeId = br.ReadByte();
		this.minimapAssetName = br.ReadString();
		this.broadcastPort = br.ReadUInt16();
		this.commandListenPort = br.ReadUInt16();
		this.isPveMap = br.ReadBoolean();
		this.reqMinLevel = br.ReadInt16();
		this.reqMaxLevel = br.ReadInt16();
		this.description = br.ReadString();
		this.fraction = br.ReadInt16();
		this.accessLevel = br.ReadByte();
	}

	public bool IsUltraGalaxyInstance()
	{
		bool flag;
		HyperJumpNet[] hyperJumpNetArray = this.hyperJumps;
		int num = 0;
		while (true)
		{
			if (num >= (int)hyperJumpNetArray.Length)
			{
				flag = false;
				break;
			}
			else if (hyperJumpNetArray[num].galaxyDst <= 2999)
			{
				num++;
			}
			else
			{
				flag = true;
				break;
			}
		}
		return flag;
	}

	public void Serialize(BinaryWriter bw)
	{
		int i;
		bw.Write((int)this.starBases.Length);
		for (i = 0; i < (int)this.starBases.Length; i++)
		{
			this.starBases[i].Serialize(bw);
		}
		bw.Write((int)this.npcPopulation.Length);
		for (i = 0; i < (int)this.npcPopulation.Length; i++)
		{
			this.npcPopulation[i].Serialize(bw);
		}
		bw.Write((int)this.hyperJumps.Length);
		for (i = 0; i < (int)this.hyperJumps.Length; i++)
		{
			this.hyperJumps[i].Serialize(bw);
		}
		bw.Write((int)this.extractionPoints.Length);
		for (i = 0; i < (int)this.extractionPoints.Length; i++)
		{
			this.extractionPoints[i].Serialize(bw);
		}
		bw.Write((int)this.checkpoints.Length);
		for (i = 0; i < (int)this.checkpoints.Length; i++)
		{
			this.checkpoints[i].Serialize(bw);
		}
		bw.Write(this.galaxyKey);
		bw.Write(this.galaxyId);
		bw.Write(this.nameUI);
		bw.Write(this.width);
		bw.Write(this.height);
		bw.Write(this.scenename);
		bw.Write(this.universeId);
		bw.Write(this.minimapAssetName);
		bw.Write(this.broadcastPort);
		bw.Write(this.commandListenPort);
		bw.Write(this.isPveMap);
		bw.Write(this.reqMinLevel);
		bw.Write(this.reqMaxLevel);
		if (this.description != null)
		{
			bw.Write(this.description);
		}
		else
		{
			bw.Write("");
		}
		bw.Write(this.fraction);
		bw.Write(this.accessLevel);
	}

	public void UnzipCollisionsMap()
	{
		if (this.collisionsMapZipped != null)
		{
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(this.collisionsMapZipped));
			int num = binaryReader.ReadInt32();
			binaryReader.ReadInt32();
			BitArray[] bitArrays = new BitArray[num];
			for (int i = 0; i < num; i++)
			{
				byte[] numArray = binaryReader.ReadBytes(binaryReader.ReadInt32());
				bitArrays[i] = new BitArray(Zip.Unarchive(numArray));
			}
			this.collisionsMap = bitArrays;
		}
		else
		{
			this.collisionsMap = null;
		}
	}
}