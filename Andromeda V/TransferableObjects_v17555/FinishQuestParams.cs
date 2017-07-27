using System;
using System.IO;

public class FinishQuestParams : ITransferable
{
	public int questID;

	[NonSerialized]
	public int dbID;

	public FinishQuestParams()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.questID = br.ReadInt32();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.questID);
	}
}