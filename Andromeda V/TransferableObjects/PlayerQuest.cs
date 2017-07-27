using System;
using System.IO;

public class PlayerQuest : ITransferable
{
	public int currentQuestId;

	public bool inProgress;

	public bool isRewordCollected;

	public PlayerQuest()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.currentQuestId = br.ReadInt32();
		this.inProgress = br.ReadBoolean();
		this.isRewordCollected = br.ReadBoolean();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.currentQuestId);
		bw.Write(this.inProgress);
		bw.Write(this.isRewordCollected);
	}
}