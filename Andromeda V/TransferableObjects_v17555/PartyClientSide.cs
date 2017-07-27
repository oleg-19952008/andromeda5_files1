using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class PartyClientSide : ITransferable
{
	public List<PartyMemberClientSide> members = new List<PartyMemberClientSide>();

	public PartyLootRules rules = PartyLootRules.FindersKeepers;

	public PartyClientSide()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		int num = br.ReadInt32();
		this.members.Clear();
		for (int i = 0; i < num; i++)
		{
			PartyMemberClientSide partyMemberClientSide = new PartyMemberClientSide();
			partyMemberClientSide.Deserialize(br);
			this.members.Add(partyMemberClientSide);
		}
		this.rules = (PartyLootRules)br.ReadInt32();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.members.Count<PartyMemberClientSide>());
		foreach (PartyMemberClientSide member in this.members)
		{
			member.Serialize(bw);
		}
		bw.Write((int)this.rules);
	}
}