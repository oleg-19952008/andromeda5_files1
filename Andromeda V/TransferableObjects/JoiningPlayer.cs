using System;
using System.Collections.Generic;
using System.IO;

public class JoiningPlayer : ITransferable
{
	public long id;

	public short pvpGameTypeSignedFor = 0;

	public string lang;

	public bool isChatAdm;

	public bool isGuest;

	public ShipConfiguration cfg;

	public PlayerObjectPhysics physics;

	public NewQuest[] myDailyMissions;

	public SortedList<int, PlayerPendingAward> playerPendingAwards;

	public ServerState state;

	public int lastSeenGameMessageId;

	public int lastSeenPrivateMessageId;

	public JoiningPlayer()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		int i;
		this.id = br.ReadInt64();
		this.pvpGameTypeSignedFor = br.ReadInt16();
		this.cfg = new ShipConfiguration();
		this.cfg.Deserialize(br);
		this.physics = new PlayerObjectPhysics();
		this.physics.Deserialize(br);
		this.state = (ServerState)br.ReadByte();
		this.lang = br.ReadString();
		this.isChatAdm = br.ReadBoolean();
		this.isGuest = br.ReadBoolean();
		int num = br.ReadInt32();
		this.myDailyMissions = new NewQuest[num];
		for (i = 0; i < num; i++)
		{
			NewQuest newQuest = new NewQuest();
			newQuest.Deserialize(br);
			this.myDailyMissions[i] = newQuest;
		}
		num = br.ReadInt32();
		if (num != -1)
		{
			this.playerPendingAwards = new SortedList<int, PlayerPendingAward>();
			for (i = 0; i < num; i++)
			{
				int num1 = br.ReadInt32();
				PlayerPendingAward playerPendingAward = new PlayerPendingAward();
				playerPendingAward.Deserialize(br);
				this.playerPendingAwards.Add(num1, playerPendingAward);
			}
		}
		else
		{
			this.playerPendingAwards = null;
		}
		this.lastSeenGameMessageId = br.ReadInt32();
		this.lastSeenPrivateMessageId = br.ReadInt32();
	}

	public void Serialize(BinaryWriter bw)
	{
		int i;
		bw.Write(this.id);
		bw.Write(this.pvpGameTypeSignedFor);
		this.cfg.Serialize(bw);
		this.physics.Serialize(bw);
		bw.Write((byte)this.state);
		bw.Write(this.lang ?? "");
		bw.Write(this.isChatAdm);
		bw.Write(this.isGuest);
		int length = (int)this.myDailyMissions.Length;
		bw.Write(length);
		for (i = 0; i < length; i++)
		{
			this.myDailyMissions[i].Serialize(bw);
		}
		if (this.playerPendingAwards != null)
		{
			bw.Write(this.playerPendingAwards.Count);
			foreach (int d in this.playerPendingAwards.Keys)
			{
				bw.Write(d);
				this.playerPendingAwards[d].Serialize(bw);
			}
		}
		else
		{
			bw.Write(-1);
		}
		bw.Write(this.lastSeenGameMessageId);
		bw.Write(this.lastSeenPrivateMessageId);
	}
}