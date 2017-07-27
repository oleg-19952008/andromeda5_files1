using System;
using System.IO;

public class QuestObjective : ITransferable
{
	public string text;

	public bool isDone;

	public QuestObjective()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.text = br.ReadString();
		this.isDone = br.ReadBoolean();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.text);
		bw.Write(this.isDone);
	}
}