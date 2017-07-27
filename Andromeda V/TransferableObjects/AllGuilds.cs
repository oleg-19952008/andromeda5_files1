using System;
using System.Collections.Generic;
using System.IO;

public class AllGuilds : ITransferable
{
	public List<GuildSummary> guilds;

	public AllGuilds()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		throw new NotImplementedException();
	}

	public void Serialize(BinaryWriter bw)
	{
		throw new NotImplementedException();
	}
}