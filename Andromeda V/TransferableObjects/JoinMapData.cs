using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class JoinMapData : ITransferable
{
	public Guild myGuild = new Guild()
	{
		id = 0
	};

	public GuildRank myGuildRank = new GuildRank();

	public SortedList<byte, byte> factionGalaxiesOwnership = new SortedList<byte, byte>();

	public byte factionOneMostWantedGalaxy;

	public byte factionTwoMostWantedGalaxy;

	public bool isWarInProgress;

	public DateTime nextWarStartTime = DateTime.MinValue;

	public string[] friends;

	public string[] blackList;

	public JoiningPlayer myPlayer;

	public GameObjectPhysics[] gameObjects;

	public LevelMap galaxy;

	public PlayerBelongings belongings;

	public PlayerObjectPhysics physics;

	public PartyClientSide party;

	public SortedList<long, PartyInvite> partyInvitees = new SortedList<long, PartyInvite>();

	public SortedList<long, PartyInvite> partyInviters = new SortedList<long, PartyInvite>();

	public List<GameMessage> playerGameMessages = new List<GameMessage>();

	public byte[] collisionsMapZipped;

	public float collisionsMapStep;

	public PvPGame pvpGame;

	public JoinMapData()
	{
	}

	private void c(BinaryWriter bw)
	{
		MemoryStream memoryStream = new MemoryStream();
		BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
		this.physics.Serialize(binaryWriter);
		this.myPlayer.Serialize(binaryWriter);
		byte[] array = memoryStream.ToArray();
		AuthorizeRequest.ApplyXOR(array);
		bw.Write((int)array.Length);
		bw.Write(array, 0, (int)array.Length);
	}

	private void d(BinaryReader br)
	{
		int num = br.ReadInt32();
		byte[] numArray = new byte[num];
		br.Read(numArray, 0, num);
		AuthorizeRequest.ApplyXOR(numArray);
		BinaryReader binaryReader = new BinaryReader(new MemoryStream(numArray));
		this.physics = new PlayerObjectPhysics();
		this.physics.Deserialize(binaryReader);
		this.myPlayer = new JoiningPlayer();
		this.myPlayer.Deserialize(binaryReader);
	}

	public void Deserialize(BinaryReader br)
	{
		int i;
		PartyInvite partyInvite;
		this.myGuild = new Guild();
		this.myGuild.DeserializeInContext(br, TransferContext.GuildOverviewInternal);
		this.myGuildRank = new GuildRank();
		this.DeserializeRank(br, this.myGuildRank);
		this.d(br);
		this.gameObjects = new GameObjectPhysics[br.ReadInt32()];
		for (i = 0; i < (int)this.gameObjects.Length; i++)
		{
			ITransferable transferable = TransferablesFramework.DeserializeITransferable(br);
			this.gameObjects[i] = (GameObjectPhysics)transferable;
		}
		this.galaxy = new LevelMap();
		this.galaxy.Deserialize(br);
		this.belongings = new PlayerBelongings();
		this.belongings.Deserialize(br);
		if (br.ReadByte() != 0)
		{
			this.party = new PartyClientSide();
			this.party.Deserialize(br);
		}
		else
		{
			this.party = null;
		}
		int num = br.ReadInt32();
		this.partyInvitees = new SortedList<long, PartyInvite>();
		for (i = 0; i < num; i++)
		{
			partyInvite = new PartyInvite();
			partyInvite.Deserialize(br);
			this.partyInvitees.Add(partyInvite.player, partyInvite);
		}
		num = br.ReadInt32();
		this.partyInviters = new SortedList<long, PartyInvite>();
		for (i = 0; i < num; i++)
		{
			partyInvite = new PartyInvite();
			partyInvite.Deserialize(br);
			this.partyInviters.Add(partyInvite.player, partyInvite);
		}
		num = br.ReadInt32();
		if (num != 0)
		{
			this.collisionsMapZipped = br.ReadBytes(num);
		}
		else
		{
			this.collisionsMapZipped = null;
		}
		this.collisionsMapStep = br.ReadSingle();
		num = br.ReadInt32();
		if (num == 1)
		{
			this.pvpGame = new PvPGame();
			this.pvpGame.Deserialize(br);
		}
		num = br.ReadInt32();
		if (num != -1)
		{
			this.friends = new string[num];
			for (i = 0; i < num; i++)
			{
				this.friends[i] = br.ReadString();
			}
		}
		else
		{
			this.friends = null;
		}
		num = br.ReadInt32();
		if (num != -1)
		{
			this.blackList = new string[num];
			for (i = 0; i < num; i++)
			{
				this.blackList[i] = br.ReadString();
			}
		}
		else
		{
			this.blackList = null;
		}
		int num1 = br.ReadByte();
		for (i = 0; i < num1; i++)
		{
			byte num2 = br.ReadByte();
			byte num3 = br.ReadByte();
			this.factionGalaxiesOwnership.Add(num2, num3);
		}
		this.factionOneMostWantedGalaxy = br.ReadByte();
		this.factionTwoMostWantedGalaxy = br.ReadByte();
		this.isWarInProgress = br.ReadBoolean();
		int num4 = br.ReadInt32();
		if (num4 != -1)
		{
			this.nextWarStartTime = StaticData.now.AddSeconds((double)num4);
		}
		else
		{
			this.nextWarStartTime = DateTime.MinValue;
		}
		num = br.ReadInt32();
		this.playerGameMessages = new List<GameMessage>();
		for (i = 0; i < num; i++)
		{
			GameMessage gameMessage = new GameMessage()
			{
				id = br.ReadInt32(),
				isNew = br.ReadBoolean(),
				link = br.ReadString(),
				senderName = br.ReadString(),
				text = br.ReadString(),
				title = br.ReadString(),
				type = (GameMessageType)br.ReadByte()
			};
			num4 = br.ReadInt32();
			gameMessage.reciveTime = StaticData.now.AddSeconds((double)num4);
			this.playerGameMessages.Add(gameMessage);
		}
	}

	private void DeserializeRank(BinaryReader br, GuildRank rank)
	{
		rank.id = br.ReadInt32();
		rank.name = br.ReadString();
		rank.isMaster = br.ReadBoolean();
		rank.sortIndex = br.ReadInt16();
		rank.avatarIndex = br.ReadInt16();
		rank.canBank = br.ReadBoolean();
		rank.canEditDetails = br.ReadBoolean();
		rank.canInvite = br.ReadBoolean();
		rank.canChat = br.ReadBoolean();
		rank.canPromote = br.ReadBoolean();
		rank.canVault = br.ReadBoolean();
	}

	public void Serialize(BinaryWriter bw)
	{
		int i;
		PartyInvite value = null;
		string[] strArrays;
		int j;
		TimeSpan item;
		this.myGuild.SerializeInContext(bw, TransferContext.GuildOverviewInternal);
		this.SerializeRank(bw, this.myGuildRank);
		this.c(bw);
		bw.Write((int)this.gameObjects.Length);
		for (i = 0; i < (int)this.gameObjects.Length; i++)
		{
			TransferablesFramework.SerializeITransferable(bw, this.gameObjects[i], TransferContext.None);
		}
		this.galaxy.Serialize(bw);
		this.belongings.Serialize(bw);
		if (this.party != null)
		{
			bw.Write((byte)1);
			this.party.Serialize(bw);
		}
		else
		{
			bw.Write((byte)0);
		}
		bw.Write(this.partyInvitees.Count);
		foreach (PartyInvite v in this.partyInvitees.Values)
		{
			v.Serialize(bw);
		}
		bw.Write(this.partyInviters.Count);
		foreach (PartyInvite partyInvite in this.partyInviters.Values)
		{
			partyInvite.Serialize(bw);
		}
		if (this.collisionsMapZipped != null)
		{
			bw.Write((int)this.collisionsMapZipped.Length);
			bw.Write(this.collisionsMapZipped);
		}
		else
		{
			bw.Write(0);
		}
		bw.Write(this.collisionsMapStep);
		if (this.pvpGame != null)
		{
			bw.Write(1);
			this.pvpGame.Serialize(bw);
		}
		else
		{
			bw.Write(-1);
		}
		if (this.friends != null)
		{
			bw.Write((int)this.friends.Length);
			strArrays = this.friends;
			for (j = 0; j < (int)strArrays.Length; j++)
			{
				bw.Write(strArrays[j]);
			}
		}
		else
		{
			bw.Write(-1);
		}
		if (this.blackList != null)
		{
			bw.Write((int)this.blackList.Length);
			strArrays = this.blackList;
			for (j = 0; j < (int)strArrays.Length; j++)
			{
				bw.Write(strArrays[j]);
			}
		}
		else
		{
			bw.Write(-1);
		}
		int count = this.factionGalaxiesOwnership.Count;
		bw.Write((byte)count);
		for (i = 0; i < count; i++)
		{
			bw.Write(this.factionGalaxiesOwnership.Keys[i]);
			bw.Write(this.factionGalaxiesOwnership.Values[i]);
		}
		bw.Write(this.factionOneMostWantedGalaxy);
		bw.Write(this.factionTwoMostWantedGalaxy);
		bw.Write(this.isWarInProgress);
		if (!(this.nextWarStartTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			item = this.nextWarStartTime - StaticData.now;
			bw.Write((int)item.TotalSeconds);
		}
		int num = this.playerGameMessages.Count<GameMessage>();
		bw.Write(num);
		for (i = 0; i < num; i++)
		{
			bw.Write(this.playerGameMessages[i].id);
			bw.Write(this.playerGameMessages[i].isNew);
			bw.Write(this.playerGameMessages[i].link ?? "");
			bw.Write(this.playerGameMessages[i].senderName ?? "");
			bw.Write(this.playerGameMessages[i].text ?? "");
			bw.Write(this.playerGameMessages[i].title ?? "");
			bw.Write((byte)this.playerGameMessages[i].type);
			item = this.playerGameMessages[i].reciveTime - StaticData.now;
			bw.Write((int)item.TotalSeconds);
		}
	}

	private void SerializeRank(BinaryWriter bw, GuildRank rank)
	{
		bw.Write(rank.id);
		bw.Write(rank.name);
		bw.Write(rank.isMaster);
		bw.Write(rank.sortIndex);
		bw.Write(rank.avatarIndex);
		bw.Write(rank.canBank);
		bw.Write(rank.canEditDetails);
		bw.Write(rank.canInvite);
		bw.Write(rank.canChat);
		bw.Write(rank.canPromote);
		bw.Write(rank.canVault);
	}
}