using System;
using System.IO;

public class MiningStation : GameObjectPhysics, ITransferableInContext, ITransferable
{
	public static int PVE_KILL_INCOME;

	public static int PVE_RESPAWN_TIME_IN_SECONDS;

	public static int DEFAULT_INCOME;

	public static int INCOME_TIME_IN_SECONDS;

	public static int POINTS_OF_PROGRESS;

	public static int PROGRESS_CAHNGE;

	public static int PROGRESS_GENERATION_TIME_IN_SECONDS;

	public static int RANGE_OF_ACTION;

	public static int SUCCESSAFUL_GAME_PENALTY_REDUCE;

	public byte teamOneProgress;

	public byte teamTwoProgress;

	public short stationId;

	public string name;

	public byte OwnerTeam
	{
		get
		{
			byte num;
			if (this.teamOneProgress <= 5)
			{
				num = (byte)((this.teamTwoProgress <= 5 ? 0 : 2));
			}
			else
			{
				num = 1;
			}
			return num;
		}
	}

	static MiningStation()
	{
		MiningStation.PVE_KILL_INCOME = 10;
		MiningStation.PVE_RESPAWN_TIME_IN_SECONDS = 60;
		MiningStation.DEFAULT_INCOME = 2;
		MiningStation.INCOME_TIME_IN_SECONDS = 2;
		MiningStation.POINTS_OF_PROGRESS = 10;
		MiningStation.PROGRESS_CAHNGE = 1;
		MiningStation.PROGRESS_GENERATION_TIME_IN_SECONDS = 2;
		MiningStation.RANGE_OF_ACTION = 15;
		MiningStation.SUCCESSAFUL_GAME_PENALTY_REDUCE = 1;
	}

	public MiningStation()
	{
	}

	public override void CalculateObjectMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
	}

	public void CopyPropsTo(MiningStation copyTarget)
	{
		base.CopyPropsTo(copyTarget);
		copyTarget.stationId = this.stationId;
		copyTarget.name = this.name;
		copyTarget.teamOneProgress = this.teamOneProgress;
		copyTarget.teamTwoProgress = this.teamTwoProgress;
	}

	public override void Deserialize(BinaryReader br)
	{
		this.stationId = br.ReadInt16();
		this.teamOneProgress = br.ReadByte();
		this.teamTwoProgress = br.ReadByte();
		this.name = br.ReadString();
		base.Deserialize(br);
	}

	public void DeserializeInContext(BinaryReader br, TransferContext context)
	{
		if (context == TransferContext.MiningStationProgressUpdate)
		{
			this.neighbourhoodId = br.ReadUInt32();
			this.teamOneProgress = br.ReadByte();
			this.teamTwoProgress = br.ReadByte();
		}
	}

	public bool IsObjectInRange(GameObjectPhysics target)
	{
		bool distance = GameObjectPhysics.GetDistance(this.x, target.x, this.z, target.z) < (float)MiningStation.RANGE_OF_ACTION;
		return distance;
	}

	public override void Serialize(BinaryWriter bw)
	{
		bw.Write(this.stationId);
		bw.Write(this.teamOneProgress);
		bw.Write(this.teamTwoProgress);
		bw.Write(this.name ?? "");
		base.Serialize(bw);
	}

	public void SerializeInContext(BinaryWriter bw, TransferContext context)
	{
		if (context == TransferContext.MiningStationProgressUpdate)
		{
			bw.Write(this.neighbourhoodId);
			bw.Write(this.teamOneProgress);
			bw.Write(this.teamTwoProgress);
		}
	}
}