using System;
using System.IO;

public class PlayerPendingAward : ITransferable
{
	public int rewardId;

	public ushort itemType;

	public int amount;

	public string title;

	public byte bonuses;

	public bool isDaily;

	public DateTime expireTime = DateTime.MinValue;

	public bool IsTimedLimited
	{
		get
		{
			return this.expireTime != DateTime.MinValue;
		}
	}

	public PlayerPendingAward()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.rewardId = br.ReadInt32();
		this.itemType = br.ReadUInt16();
		this.amount = br.ReadInt32();
		this.title = br.ReadString();
		this.bonuses = br.ReadByte();
		this.isDaily = br.ReadBoolean();
		int num = br.ReadInt32();
		if (num != -1)
		{
			this.expireTime = StaticData.now.AddSeconds((double)num);
		}
		else
		{
			this.expireTime = DateTime.MinValue;
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.rewardId);
		bw.Write(this.itemType);
		bw.Write(this.amount);
		bw.Write(this.title ?? string.Empty);
		bw.Write(this.bonuses);
		bw.Write(this.isDaily);
		if (!(this.expireTime != DateTime.MinValue))
		{
			bw.Write(-1);
		}
		else
		{
			TimeSpan timeSpan = this.expireTime - StaticData.now;
			bw.Write((int)timeSpan.TotalSeconds);
		}
	}
}