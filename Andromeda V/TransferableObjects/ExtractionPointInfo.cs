using System;
using System.IO;

public class ExtractionPointInfo : ITransferable
{
	public int id;

	public string name;

	public string description;

	public string assetName;

	public int galaxyId;

	public short possitionX;

	public short possitionZ;

	public int unitDamage;

	public int unitHitPoints;

	public int hitPoints;

	public int ultralibrium;

	public short population;

	public short regenPerSec;

	public byte magicCoefficient;

	public ExtractionPointUpgrade[] allUpgrades;

	public ExtractionPointUnit[] allUnits;

	public ExtractionPointInfo()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		int i;
		this.id = br.ReadInt32();
		this.name = br.ReadString();
		this.description = br.ReadString();
		this.assetName = br.ReadString();
		this.galaxyId = br.ReadInt32();
		this.possitionX = br.ReadInt16();
		this.possitionZ = br.ReadInt16();
		this.unitDamage = br.ReadInt32();
		this.unitHitPoints = br.ReadInt32();
		this.hitPoints = br.ReadInt32();
		this.ultralibrium = br.ReadInt32();
		this.population = br.ReadInt16();
		this.regenPerSec = br.ReadInt16();
		this.magicCoefficient = br.ReadByte();
		int num = br.ReadInt32();
		this.allUpgrades = new ExtractionPointUpgrade[num];
		for (i = 0; i < num; i++)
		{
			this.allUpgrades[i] = new ExtractionPointUpgrade();
			this.UpgradeDeserializa(this.allUpgrades[i], br);
		}
		num = br.ReadInt32();
		this.allUnits = new ExtractionPointUnit[num];
		for (i = 0; i < num; i++)
		{
			this.allUnits[i] = new ExtractionPointUnit();
			this.UnitDeserializa(this.allUnits[i], br);
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		int i;
		bw.Write(this.id);
		bw.Write(this.name ?? "");
		bw.Write(this.description ?? "");
		bw.Write(this.assetName ?? "");
		bw.Write(this.galaxyId);
		bw.Write(this.possitionX);
		bw.Write(this.possitionZ);
		bw.Write(this.unitDamage);
		bw.Write(this.unitHitPoints);
		bw.Write(this.hitPoints);
		bw.Write(this.ultralibrium);
		bw.Write(this.population);
		bw.Write(this.regenPerSec);
		bw.Write(this.magicCoefficient);
		bw.Write((int)this.allUpgrades.Length);
		ExtractionPointUpgrade[] extractionPointUpgradeArray = this.allUpgrades;
		for (i = 0; i < (int)extractionPointUpgradeArray.Length; i++)
		{
			this.UpgradeSerialize(extractionPointUpgradeArray[i], bw);
		}
		bw.Write((int)this.allUnits.Length);
		ExtractionPointUnit[] extractionPointUnitArray = this.allUnits;
		for (i = 0; i < (int)extractionPointUnitArray.Length; i++)
		{
			this.UnitSerialize(extractionPointUnitArray[i], bw);
		}
	}

	private void UnitDeserializa(ExtractionPointUnit epu, BinaryReader br)
	{
		epu.name = br.ReadString();
		epu.description = br.ReadString();
		epu.assetName = br.ReadString();
		epu.upgrade = br.ReadByte();
		epu.unitType = br.ReadByte();
		epu.damage = br.ReadSingle();
		epu.hitPoints = br.ReadSingle();
		epu.cooldown = br.ReadInt16();
		epu.range = br.ReadInt16();
		epu.penetration = br.ReadInt16();
		epu.speed = br.ReadInt16();
		epu.population = br.ReadInt16();
		epu.price = br.ReadInt32();
	}

	private void UnitSerialize(ExtractionPointUnit epu, BinaryWriter bw)
	{
		bw.Write(epu.name ?? "");
		bw.Write(epu.description ?? "");
		bw.Write(epu.assetName ?? "");
		bw.Write(epu.upgrade);
		bw.Write(epu.unitType);
		bw.Write(epu.damage);
		bw.Write(epu.hitPoints);
		bw.Write(epu.cooldown);
		bw.Write(epu.range);
		bw.Write(epu.penetration);
		bw.Write(epu.speed);
		bw.Write(epu.population);
		bw.Write(epu.price);
	}

	private void UpgradeDeserializa(ExtractionPointUpgrade epu, BinaryReader br)
	{
		epu.name = br.ReadString();
		epu.description = br.ReadString();
		epu.assetName = br.ReadString();
		epu.upgrade = br.ReadByte();
		epu.upgradeType = br.ReadByte();
		epu.@value = br.ReadSingle();
		epu.price = br.ReadInt32();
		epu.extractionPointId = br.ReadInt32();
	}

	private void UpgradeSerialize(ExtractionPointUpgrade epu, BinaryWriter bw)
	{
		bw.Write(epu.name ?? "");
		bw.Write(epu.description ?? "");
		bw.Write(epu.assetName ?? "");
		bw.Write(epu.upgrade);
		bw.Write(epu.upgradeType);
		bw.Write(epu.@value);
		bw.Write(epu.price);
		bw.Write(epu.extractionPointId);
	}
}