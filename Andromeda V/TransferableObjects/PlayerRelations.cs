using System;
using System.Collections.Generic;
using System.IO;

public class PlayerRelations : ITransferable
{
	public List<PlayerProfile> players;

	public PlayerRelations()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		int num = br.ReadInt32();
		if (num != -1)
		{
			this.players = new List<PlayerProfile>();
			for (int i = 0; i < num; i++)
			{
				PlayerProfile playerProfile = new PlayerProfile()
				{
					avatarUrl = br.ReadString(),
					fractionId = br.ReadByte(),
					galaxyId = br.ReadInt16(),
					isOnline = br.ReadBoolean(),
					level = br.ReadInt32(),
					userName = br.ReadString()
				};
				this.players.Add(playerProfile);
			}
		}
		else
		{
			this.players = null;
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		if (this.players != null)
		{
			bw.Write(this.players.Count);
			foreach (PlayerProfile player in this.players)
			{
				bw.Write(player.avatarUrl ?? "");
				bw.Write(player.fractionId);
				bw.Write(player.galaxyId);
				bw.Write(player.isOnline);
				bw.Write(player.level);
				bw.Write(player.userName);
			}
		}
		else
		{
			bw.Write(-1);
		}
	}
}