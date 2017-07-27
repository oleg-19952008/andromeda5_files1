using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Mineral : GameObjectPhysics, ITransferable
{
	public const float mineralMiningTime = 2f;

	public Mineral.Source source = Mineral.Source.InSpaceResource;

	public Dictionary<ushort, int> resourceQuantities = new Dictionary<ushort, int>();

	public List<SlotItem> items = null;

	public TargetOwnership owner = null;

	private PlayerObjectPhysics explicitOwner = null;

	public static float MINERAL_LIFE_TIME_IN_SECONDS;

	public static float MINERAL_OWNER_SAVE_TIME_IN_SECONDS;

	public DateTime mineralDespawnTime = DateTime.MinValue;

	public DateTime mineralRemoveOwnershipTime = DateTime.MinValue;

	public Action<Mineral> despawnMineral;

	public float rotationSpeedX;

	public float rotationSpeedY;

	public float rotationSpeedZ;

	[NonSerialized]
	public static float mineralMiningMoveSpeed;

	[NonSerialized]
	public static float MIN_MINING_TIME;

	public long miningPlayerId;

	[NonSerialized]
	public PlayerObjectPhysics miningPlayer;

	public long id;

	[NonSerialized]
	public DateTime miningStartTime;

	public Action<Mineral> miningAccomplishedCallback;

	public bool willDropItem = false;

	public string ownerName;

	public Action<Mineral> NotifyOwnerChange;

	private static RandomGenerator rnd;

	public static Dictionary<ushort, MinMaxPair> enchantsDamageTable;

	public static Dictionary<int, MinMaxPair> enchantsTargetingTable;

	static Mineral()
	{
		Mineral.MINERAL_LIFE_TIME_IN_SECONDS = 120f;
		Mineral.MINERAL_OWNER_SAVE_TIME_IN_SECONDS = 60f;
		Mineral.mineralMiningMoveSpeed = 1.4f;
		Mineral.MIN_MINING_TIME = 0.5f;
		Mineral.rnd = new RandomGenerator();
		Mineral.enchantsDamageTable = new Dictionary<ushort, MinMaxPair>();
		Mineral.enchantsTargetingTable = new Dictionary<int, MinMaxPair>();
		Mineral.enchantsDamageTable[PlayerItems.TypeWeaponLaserTire1] = new MinMaxPair(2, 4);
		Mineral.enchantsDamageTable[PlayerItems.TypeWeaponLaserTire2] = new MinMaxPair(4, 8);
		Mineral.enchantsDamageTable[PlayerItems.TypeWeaponLaserTire3] = new MinMaxPair(6, 12);
		Mineral.enchantsDamageTable[PlayerItems.TypeWeaponLaserTire4] = new MinMaxPair(8, 16);
		Mineral.enchantsDamageTable[PlayerItems.TypeWeaponLaserTire5] = new MinMaxPair(10, 20);
		Mineral.enchantsDamageTable[PlayerItems.TypeWeaponPlasmaTire1] = new MinMaxPair(3, 6);
		Mineral.enchantsDamageTable[PlayerItems.TypeWeaponPlasmaTire2] = new MinMaxPair(6, 13);
		Mineral.enchantsDamageTable[PlayerItems.TypeWeaponPlasmaTire3] = new MinMaxPair(9, 19);
		Mineral.enchantsDamageTable[PlayerItems.TypeWeaponPlasmaTire4] = new MinMaxPair(13, 26);
		Mineral.enchantsDamageTable[PlayerItems.TypeWeaponPlasmaTire5] = new MinMaxPair(16, 32);
		Mineral.enchantsDamageTable[PlayerItems.TypeWeaponIonTire1] = new MinMaxPair(10, 20);
		Mineral.enchantsDamageTable[PlayerItems.TypeWeaponIonTire2] = new MinMaxPair(18, 36);
		Mineral.enchantsDamageTable[PlayerItems.TypeWeaponIonTire3] = new MinMaxPair(26, 62);
		Mineral.enchantsDamageTable[PlayerItems.TypeWeaponIonTire4] = new MinMaxPair(34, 68);
		Mineral.enchantsDamageTable[PlayerItems.TypeWeaponIonTire5] = new MinMaxPair(42, 84);
		Mineral.enchantsTargetingTable[0] = new MinMaxPair(3, 8);
		Mineral.enchantsTargetingTable[1] = new MinMaxPair(6, 12);
		Mineral.enchantsTargetingTable[2] = new MinMaxPair(9, 18);
		Mineral.enchantsTargetingTable[3] = new MinMaxPair(13, 26);
		Mineral.enchantsTargetingTable[4] = new MinMaxPair(18, 36);
	}

	public Mineral()
	{
	}

	public Mineral(ushort resourceType, short qty)
	{
		this.owner = null;
		this.resourceQuantities[resourceType] = qty;
		if (resourceType == PlayerItems.TypeHydrogen)
		{
			this.assetName = "resHydrogen";
		}
		else if (resourceType == PlayerItems.TypeOxygen)
		{
			this.assetName = "resOxygen";
		}
		else if (resourceType == PlayerItems.TypeCarbon)
		{
			this.assetName = "resCarbon";
		}
		else if (resourceType == PlayerItems.TypeCarbonDioxide)
		{
			this.assetName = "resCarbonDioxide";
		}
		else if (resourceType == PlayerItems.TypeAceton)
		{
			this.assetName = "resAcetone";
		}
		else if (resourceType == PlayerItems.TypeDeuterium)
		{
			this.assetName = "resDeuterium";
		}
		else if (resourceType == PlayerItems.TypeMetyl)
		{
			this.assetName = "resMethyl";
		}
		else if (resourceType != PlayerItems.TypeWater)
		{
			this.assetName = "QuestionBox";
		}
		else
		{
			this.assetName = "resWater";
		}
		DateTime now = DateTime.Now;
		this.mineralDespawnTime = now.AddHours(12);
		now = DateTime.Now;
		this.mineralRemoveOwnershipTime = now.AddYears(2);
	}

	public override void CalculateObjectMovement(float dt, ref float dx, ref float dy, ref float dz)
	{
		float single = 2f;
		if (!this.isRemoved)
		{
			if ((this.miningPlayer == null || this.miningPlayer.isRemoved || this.miningPlayer.playerData.state == ServerState.InBase ? false : this.miningPlayer.isAlive))
			{
				float single1 = 2f;
				if (this.miningPlayer.cfg.fasterMining != 0f)
				{
					single = Math.Max(200f / (this.miningPlayer.cfg.fasterMining + 100f), Mineral.MIN_MINING_TIME);
					single1 = single;
				}
				if (StaticData.now.AddSeconds((double)(-single1)) >= this.miningStartTime)
				{
					if (this.miningAccomplishedCallback != null)
					{
						this.miningAccomplishedCallback(this);
						return;
					}
				}
				float distance = GameObjectPhysics.GetDistance(this.miningPlayer.x, this.x, this.miningPlayer.z, this.z);
				DateTime dateTime = this.miningStartTime.AddSeconds((double)single1);
				TimeSpan timeSpan = dateTime - StaticData.now;
				float totalSeconds = distance / (float)timeSpan.TotalSeconds;
				Victor3 victor3 = Victor3.MoveTowards(new Victor3(this.x, this.y, this.z), new Victor3(this.miningPlayer.x, this.miningPlayer.y, this.miningPlayer.z), totalSeconds * dt);
				base.KeepInBoundary(ref victor3);
				dx = victor3.x - this.x;
				dy = victor3.y - this.y;
				dz = victor3.z - this.z;
			}
			else
			{
				this.miningPlayer = null;
				this.miningPlayerId = (long)0;
			}
		}
	}

	public override void Deserialize(BinaryReader br)
	{
		int i;
		this.rotationSpeedX = br.ReadSingle();
		this.rotationSpeedY = br.ReadSingle();
		this.rotationSpeedZ = br.ReadSingle();
		this.miningPlayerId = br.ReadInt64();
		this.ownerName = br.ReadString();
		int num = br.ReadInt32();
		for (i = 0; i < num; i++)
		{
			ushort num1 = br.ReadUInt16();
			this.resourceQuantities[num1] = br.ReadInt32();
		}
		this.id = br.ReadInt64();
		this.source = (Mineral.Source)br.ReadInt32();
		int num2 = br.ReadInt32();
		if (num2 <= -1)
		{
			this.items = null;
		}
		else
		{
			this.items = new List<SlotItem>(num2);
			for (i = 0; i < num2; i++)
			{
				this.items.Add((SlotItem)TransferablesFramework.DeserializeITransferable(br));
			}
		}
		base.Deserialize(br);
	}

	public bool HasOwner()
	{
		bool flag = true;
		if (this.owner == null)
		{
			flag = false;
		}
		else if (this.owner.GetType() == TargetOwnership.Type.None)
		{
			flag = false;
		}
		return flag;
	}

	public bool IsOwner(PlayerObjectPhysics player)
	{
		bool flag;
		if (this.explicitOwner != null)
		{
			flag = (player != this.explicitOwner ? false : true);
		}
		else if (this.owner == null)
		{
			flag = true;
		}
		else if (!(this.owner.GetType() != TargetOwnership.Type.Player ? true : this.owner.GetPlayer() != player))
		{
			flag = true;
		}
		else if (!(this.owner.GetType() != TargetOwnership.Type.Party || this.owner.GetParty().rules != PartyLootRules.FindersKeepers ? true : !this.owner.GetParty().members.Contains(player.playerId)))
		{
			flag = true;
		}
		else if (this.owner.GetType() != TargetOwnership.Type.None)
		{
			flag = false;
		}
		else
		{
			Console.WriteLine("Strange case");
			flag = true;
		}
		return flag;
	}

	public void MultiplyResourceValues(float coeff)
	{
		ushort[] array = this.resourceQuantities.Keys.ToArray<ushort>();
		for (int i = 0; i < (int)array.Length; i++)
		{
			ushort item = array[i];
			this.resourceQuantities[item] = (int)((float)this.resourceQuantities[item] * coeff);
		}
	}

	public override void Serialize(BinaryWriter bw)
	{
		bw.Write(this.rotationSpeedX);
		bw.Write(this.rotationSpeedY);
		bw.Write(this.rotationSpeedZ);
		bw.Write(this.miningPlayerId);
		bw.Write(this.ownerName ?? "");
		bw.Write(this.resourceQuantities.Count<KeyValuePair<ushort, int>>());
		foreach (KeyValuePair<ushort, int> resourceQuantity in this.resourceQuantities)
		{
			bw.Write(resourceQuantity.Key);
			bw.Write(resourceQuantity.Value);
		}
		bw.Write(this.id);
		bw.Write((int)this.source);
		if (this.items != null)
		{
			bw.Write(this.items.Count);
			foreach (SlotItem item in this.items)
			{
				TransferablesFramework.SerializeITransferable(bw, item, TransferContext.None);
			}
		}
		else
		{
			bw.Write(-1);
		}
		base.Serialize(bw);
	}

	public void SetOwner(PlayerObjectPhysics player)
	{
		this.explicitOwner = player;
		this.ownerName = player.playerName;
	}

	public enum Rarity
	{
		Common,
		Modified,
		Tuned,
		Experimental
	}

	public enum Source
	{
		InSpaceResource,
		Other
	}
}