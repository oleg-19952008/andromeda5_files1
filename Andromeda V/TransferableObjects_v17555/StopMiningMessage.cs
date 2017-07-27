using System;
using System.Collections.Generic;
using System.IO;

public class StopMiningMessage : ITransferable
{
	public long miningPlayerId;

	public uint mineralNbId;

	public bool isComplete;

	public SortedList<ushort, int> collectedMinerals = new SortedList<ushort, int>();

	public List<SlotItem> collectedItems = new List<SlotItem>();

	public StopMiningMessage()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		int i;
		this.miningPlayerId = br.ReadInt64();
		this.mineralNbId = br.ReadUInt32();
		this.isComplete = br.ReadBoolean();
		int num = br.ReadInt32();
		if (num > 0)
		{
			this.collectedMinerals = new SortedList<ushort, int>();
			for (i = 0; i < num; i++)
			{
				ushort num1 = br.ReadUInt16();
				int num2 = br.ReadInt32();
				this.collectedMinerals.Add(num1, num2);
			}
		}
		num = br.ReadInt32();
		if (num > 0)
		{
			this.collectedItems = new List<SlotItem>();
			for (i = 0; i < num; i++)
			{
				SlotItem slotItem = (SlotItem)TransferablesFramework.DeserializeITransferable(br);
				this.collectedItems.Add(slotItem);
			}
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		int i;
		bw.Write(this.miningPlayerId);
		bw.Write(this.mineralNbId);
		bw.Write(this.isComplete);
		bw.Write(this.collectedMinerals.Count);
		for (i = 0; i < this.collectedMinerals.Count; i++)
		{
			bw.Write(this.collectedMinerals.Keys[i]);
			bw.Write(this.collectedMinerals.Values[i]);
		}
		bw.Write(this.collectedItems.Count);
		for (i = 0; i < this.collectedItems.Count; i++)
		{
			TransferablesFramework.SerializeITransferable(bw, this.collectedItems[i], TransferContext.None);
		}
	}
}