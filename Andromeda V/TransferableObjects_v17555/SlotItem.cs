using System;
using System.Collections.Generic;
using System.IO;

public class SlotItem : ITransferable
{
	public int id;

	private bool _isActive;

	private ushort _itemType;

	private byte _slotType;

	private ushort _slot;

	private ushort _bonusOne;

	private ushort _bonusTwo;

	private ushort _bonusThree;

	private ushort _bonusFour;

	private ushort _bonusFive;

	private byte _bonusCnt;

	private int _shipId;

	private int _amount;

	public bool isAccountBound;

	public int Amount
	{
		get
		{
			return this._amount;
		}
		set
		{
			this._amount = value;
		}
	}

	public byte BonusCnt
	{
		get
		{
			return this._bonusCnt;
		}
		set
		{
			this._bonusCnt = value;
		}
	}

	public ushort BonusFive
	{
		get
		{
			return this._bonusFive;
		}
		set
		{
			this._bonusFive = value;
		}
	}

	public ushort BonusFour
	{
		get
		{
			return this._bonusFour;
		}
		set
		{
			this._bonusFour = value;
		}
	}

	public ushort BonusOne
	{
		get
		{
			return this._bonusOne;
		}
		set
		{
			this._bonusOne = value;
		}
	}

	public ushort BonusThree
	{
		get
		{
			return this._bonusThree;
		}
		set
		{
			this._bonusThree = value;
		}
	}

	public ushort BonusTwo
	{
		get
		{
			return this._bonusTwo;
		}
		set
		{
			this._bonusTwo = value;
		}
	}

	public bool IsActive
	{
		get
		{
			return this._isActive;
		}
		set
		{
			this._isActive = value;
		}
	}

	public ushort ItemType
	{
		get
		{
			return this._itemType;
		}
		set
		{
			this._itemType = value;
		}
	}

	public int ShipId
	{
		get
		{
			return this._shipId;
		}
		set
		{
			this._shipId = value;
		}
	}

	public ushort Slot
	{
		get
		{
			return this._slot;
		}
		set
		{
			this._slot = value;
		}
	}

	public byte SlotType
	{
		get
		{
			return this._slotType;
		}
		set
		{
			this._slotType = value;
		}
	}

	public SlotItem()
	{
	}

	public string AssetName()
	{
		return StaticData.allTypes[this._itemType].assetName;
	}

	public string Description()
	{
		string empty = string.Empty;
		if (PlayerItems.IsCorpus(this._itemType))
		{
			empty = string.Format(StaticData.Translate("key_slotitem_corpus_desc"), ((GeneratorNet)StaticData.allTypes[this._itemType]).bonusValue);
		}
		else if (PlayerItems.IsShield(this._itemType))
		{
			empty = string.Format(StaticData.Translate("key_slotitem_shield_desc"), ((GeneratorNet)StaticData.allTypes[this._itemType]).bonusValue);
		}
		else if (PlayerItems.IsEngine(this._itemType))
		{
			empty = string.Format(StaticData.Translate("key_slotitem_engine_desc"), ((GeneratorNet)StaticData.allTypes[this._itemType]).bonusValue);
		}
		else if (PlayerItems.IsExtraCargo(this._itemType))
		{
			empty = string.Format(StaticData.Translate("key_slotitem_cargo_desc"), ((ExtrasNet)StaticData.allTypes[this._itemType]).efValue);
		}
		else if (PlayerItems.IsExtraMining(this._itemType))
		{
			empty = string.Format(StaticData.Translate("key_slotitem_minig_desc"), ((ExtrasNet)StaticData.allTypes[this._itemType]).efValue);
		}
		else if (PlayerItems.IsForLaserDamage(this._itemType))
		{
			empty = string.Format(StaticData.Translate("key_slotitem_dmg_laser_desc"), ((ExtrasNet)StaticData.allTypes[this._itemType]).efValue);
		}
		else if (PlayerItems.IsForPlasmaDamage(this._itemType))
		{
			empty = string.Format(StaticData.Translate("key_slotitem_dmg_plasma_desc"), ((ExtrasNet)StaticData.allTypes[this._itemType]).efValue);
		}
		else if (PlayerItems.IsForIonDamage(this._itemType))
		{
			empty = string.Format(StaticData.Translate("key_slotitem_dmg_ion_desc"), ((ExtrasNet)StaticData.allTypes[this._itemType]).efValue);
		}
		else if (PlayerItems.IsForLaserCooldown(this._itemType))
		{
			empty = string.Format(StaticData.Translate("key_slotitem_cd_laser_desc"), ((ExtrasNet)StaticData.allTypes[this._itemType]).efValue);
		}
		else if (PlayerItems.IsForPlasmaCooldown(this._itemType))
		{
			empty = string.Format(StaticData.Translate("key_slotitem_cd_plasma_desc"), ((ExtrasNet)StaticData.allTypes[this._itemType]).efValue);
		}
		else if (PlayerItems.IsForIonCooldown(this._itemType))
		{
			empty = string.Format(StaticData.Translate("key_slotitem_cd_ion_desc"), ((ExtrasNet)StaticData.allTypes[this._itemType]).efValue);
		}
		else if (PlayerItems.IsForShieldRegen(this._itemType))
		{
			empty = string.Format(StaticData.Translate("key_slotitem_sh_power_desc"), ((ExtrasNet)StaticData.allTypes[this._itemType]).efValue);
		}
		else if (this._itemType == PlayerItems.TypeExtraLaserAimingCPU)
		{
			empty = string.Format(StaticData.Translate("key_slotitem_targ_laser_desc"), ((ExtrasNet)StaticData.allTypes[this._itemType]).efValue);
		}
		else if (this._itemType == PlayerItems.TypeExtraPlasmaAimingCPU)
		{
			empty = string.Format(StaticData.Translate("key_slotitem_targ_plasma_desc"), ((ExtrasNet)StaticData.allTypes[this._itemType]).efValue);
		}
		else if (this._itemType == PlayerItems.TypeExtraIonAimingCPU)
		{
			empty = string.Format(StaticData.Translate("key_slotitem_targ_ion_desc"), ((ExtrasNet)StaticData.allTypes[this._itemType]).efValue);
		}
		else if (this._itemType == PlayerItems.TypeExtraUltraAimingCPU)
		{
			empty = string.Format(StaticData.Translate("key_slotitem_targ_all_desc"), ((ExtrasNet)StaticData.allTypes[this._itemType]).efValue);
		}
		else if (this._itemType != PlayerItems.TypeExtraUltraWeaponsModule)
		{
			empty = (this._itemType != PlayerItems.TypeExtraUltraWeaponsCoolant ? StaticData.Translate(StaticData.allTypes[this._itemType].description) : string.Format(StaticData.Translate("key_slotitem_cd_all_desc"), ((ExtrasNet)StaticData.allTypes[this._itemType]).efValue));
		}
		else
		{
			empty = string.Format(StaticData.Translate("key_slotitem_dmg_all_desc"), ((ExtrasNet)StaticData.allTypes[this._itemType]).efValue);
		}
		return empty;
	}

	public virtual void Deserialize(BinaryReader br)
	{
		this._isActive = br.ReadBoolean();
		this._itemType = br.ReadUInt16();
		this._slotType = br.ReadByte();
		this._slot = br.ReadUInt16();
		this._bonusOne = br.ReadUInt16();
		this._bonusTwo = br.ReadUInt16();
		this._bonusThree = br.ReadUInt16();
		this._bonusFour = br.ReadUInt16();
		this._bonusFive = br.ReadUInt16();
		this._bonusCnt = br.ReadByte();
		this._shipId = br.ReadInt32();
		this._amount = br.ReadInt32();
		this.isAccountBound = br.ReadBoolean();
	}

	public virtual void Serialize(BinaryWriter bw)
	{
		bw.Write(this._isActive);
		bw.Write(this._itemType);
		bw.Write(this._slotType);
		bw.Write(this._slot);
		bw.Write(this._bonusOne);
		bw.Write(this._bonusTwo);
		bw.Write(this._bonusThree);
		bw.Write(this._bonusFour);
		bw.Write(this._bonusFive);
		bw.Write(this._bonusCnt);
		bw.Write(this._shipId);
		bw.Write(this._amount);
		bw.Write(this.isAccountBound);
	}

	public override string ToString()
	{
		object[] objArray = new object[] { this._itemType, this._slotType, this._slot, this._shipId, this._amount };
		return string.Format("_itemType={0}; _slotType={1}; _slot={2}; _shipId={3}; _amount={4}", objArray);
	}

	public string UIName()
	{
		string str = StaticData.Translate(StaticData.allTypes[this._itemType].uiName);
		return str;
	}
}