using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

public class NewQuestObjective
{
	public int id;

	public int questId;

	public int parentObjectiveId;

	public ObjectiveTypeEnum type;

	public int targetAmount;

	public int targetParam1;

	public int targetParam2;

	public int targetParam3;

	public int targetParam4;

	public int targetParam5;

	public int targetGalaxyKey;

	public bool haveCustomText;

	public bool isOptional;

	public float factionOnePointerX;

	public float factionOnePointerZ;

	public int factionOnePointerGalaxyId;

	public bool onlyGalaxyPointer;

	public float factionTwoPointerX;

	public float factionTwoPointerZ;

	public int factionTwoPointerGalaxyId;

	public NewQuestObjective()
	{
	}

	public string GetObjectiveDescription()
	{
		string str;
		object[] objArray;
		try
		{
			switch (this.type)
			{
				case ObjectiveTypeEnum.OnlyForTutorial:
				{
					str = string.Format(StaticData.Translate("key_objective_tutorial_inventory_step"), StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam1].uiName));
					return str;
				}
				case ObjectiveTypeEnum.GatherResource:
				{
					str = string.Format(StaticData.Translate("key_objective_gather_resource"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam1].uiName));
					return str;
				}
				case ObjectiveTypeEnum.GatherResourceInGalaxy:
				{
					str = string.Format(StaticData.Translate("key_objective_gather_resource_in_galaxy"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam1].uiName), StaticData.Translate((
						from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
						where t.galaxyKey == this.targetGalaxyKey
						select t).First<LevelMap>().nameUI));
					return str;
				}
				case ObjectiveTypeEnum.CollectResource:
				{
					str = string.Format(StaticData.Translate("key_objective_collect_resource"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam1].uiName));
					return str;
				}
				case ObjectiveTypeEnum.CollectResourceInGalaxy:
				{
					str = string.Format(StaticData.Translate("key_objective_collect_resource_in_galaxy"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam1].uiName), StaticData.Translate((
						from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
						where t.galaxyKey == this.targetGalaxyKey
						select t).First<LevelMap>().nameUI));
					return str;
				}
				case ObjectiveTypeEnum.CollectSlotItem:
				{
					str = string.Format(StaticData.Translate("key_objective_collect_slot_item"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam1].uiName));
					return str;
				}
				case ObjectiveTypeEnum.CollectSlotItemInGalaxy:
				{
					str = string.Format(StaticData.Translate("key_objective_collect_slot_item_in_galaxy"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam1].uiName), StaticData.Translate((
						from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
						where t.galaxyKey == this.targetGalaxyKey
						select t).First<LevelMap>().nameUI));
					return str;
				}
				case ObjectiveTypeEnum.KillAlien:
				{
					string str1 = "";
					string str2 = "";
					string str3 = "";
					string str4 = "";
					string str5 = "";
					if (this.targetParam5 != 0)
					{
						str1 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam1
							select t).First<PvEInfo>().name;
						str2 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam2
							select t).First<PvEInfo>().name;
						str3 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam3
							select t).First<PvEInfo>().name;
						str4 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam4
							select t).First<PvEInfo>().name;
						str5 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam5
							select t).First<PvEInfo>().name;
						string str6 = StaticData.Translate("key_objective_kill_alien_5");
						objArray = new object[] { this.targetAmount, StaticData.Translate(str1), StaticData.Translate(str2), StaticData.Translate(str3), StaticData.Translate(str4), StaticData.Translate(str5) };
						str = string.Format(str6, objArray);
						return str;
					}
					else if (this.targetParam4 != 0)
					{
						str1 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam1
							select t).First<PvEInfo>().name;
						str2 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam2
							select t).First<PvEInfo>().name;
						str3 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam3
							select t).First<PvEInfo>().name;
						str4 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam4
							select t).First<PvEInfo>().name;
						string str7 = StaticData.Translate("key_objective_kill_alien_4");
						objArray = new object[] { this.targetAmount, StaticData.Translate(str1), StaticData.Translate(str2), StaticData.Translate(str3), StaticData.Translate(str4) };
						str = string.Format(str7, objArray);
						return str;
					}
					else if (this.targetParam3 != 0)
					{
						str1 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam1
							select t).First<PvEInfo>().name;
						str2 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam2
							select t).First<PvEInfo>().name;
						str3 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam3
							select t).First<PvEInfo>().name;
						string str8 = StaticData.Translate("key_objective_kill_alien_3");
						objArray = new object[] { this.targetAmount, StaticData.Translate(str1), StaticData.Translate(str2), StaticData.Translate(str3) };
						str = string.Format(str8, objArray);
						return str;
					}
					else if (this.targetParam2 != 0)
					{
						str1 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam1
							select t).First<PvEInfo>().name;
						str2 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam2
							select t).First<PvEInfo>().name;
						str = string.Format(StaticData.Translate("key_objective_kill_alien_2"), this.targetAmount, StaticData.Translate(str1), StaticData.Translate(str2));
						return str;
					}
					else if (this.targetParam1 == 0)
					{
						str = string.Format(StaticData.Translate("key_objective_kill_alien_0"), this.targetAmount);
						return str;
					}
					else
					{
						str1 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam1
							select t).First<PvEInfo>().name;
						str = string.Format(StaticData.Translate("key_objective_kill_alien_1"), this.targetAmount, StaticData.Translate(str1));
						return str;
					}
				}
				case ObjectiveTypeEnum.KillAlienInGalaxy:
				{
					string str9 = "";
					string str10 = "";
					string str11 = "";
					string str12 = "";
					string str13 = "";
					if (this.targetParam5 != 0)
					{
						str9 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam1
							select t).First<PvEInfo>().name;
						str10 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam2
							select t).First<PvEInfo>().name;
						str11 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam3
							select t).First<PvEInfo>().name;
						str12 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam4
							select t).First<PvEInfo>().name;
						str13 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam5
							select t).First<PvEInfo>().name;
						string str14 = StaticData.Translate("key_objective_kill_alien_in_galaxy_5");
						objArray = new object[] { this.targetAmount, StaticData.Translate(str9), StaticData.Translate(str10), StaticData.Translate(str11), StaticData.Translate(str12), StaticData.Translate(str13), null };
						objArray[6] = StaticData.Translate((
							from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
							where t.galaxyKey == this.targetGalaxyKey
							select t).First<LevelMap>().nameUI);
						str = string.Format(str14, objArray);
						return str;
					}
					else if (this.targetParam4 != 0)
					{
						str9 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam1
							select t).First<PvEInfo>().name;
						str10 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam2
							select t).First<PvEInfo>().name;
						str11 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam3
							select t).First<PvEInfo>().name;
						str12 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam4
							select t).First<PvEInfo>().name;
						string str15 = StaticData.Translate("key_objective_kill_alien_in_galaxy_4");
						objArray = new object[] { this.targetAmount, StaticData.Translate(str9), StaticData.Translate(str10), StaticData.Translate(str11), StaticData.Translate(str12), null };
						objArray[5] = StaticData.Translate((
							from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
							where t.galaxyKey == this.targetGalaxyKey
							select t).First<LevelMap>().nameUI);
						str = string.Format(str15, objArray);
						return str;
					}
					else if (this.targetParam3 != 0)
					{
						str9 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam1
							select t).First<PvEInfo>().name;
						str10 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam2
							select t).First<PvEInfo>().name;
						str11 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam3
							select t).First<PvEInfo>().name;
						string str16 = StaticData.Translate("key_objective_kill_alien_in_galaxy_3");
						objArray = new object[] { this.targetAmount, StaticData.Translate(str9), StaticData.Translate(str10), StaticData.Translate(str11), null };
						objArray[4] = StaticData.Translate((
							from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
							where t.galaxyKey == this.targetGalaxyKey
							select t).First<LevelMap>().nameUI);
						str = string.Format(str16, objArray);
						return str;
					}
					else if (this.targetParam2 != 0)
					{
						str9 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam1
							select t).First<PvEInfo>().name;
						str10 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam2
							select t).First<PvEInfo>().name;
						string str17 = StaticData.Translate("key_objective_kill_alien_in_galaxy_2");
						objArray = new object[] { this.targetAmount, StaticData.Translate(str9), StaticData.Translate(str10), null };
						objArray[3] = StaticData.Translate((
							from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
							where t.galaxyKey == this.targetGalaxyKey
							select t).First<LevelMap>().nameUI);
						str = string.Format(str17, objArray);
						return str;
					}
					else if (this.targetParam1 == 0)
					{
						str = string.Format(StaticData.Translate("key_objective_kill_alien_in_galaxy_0"), this.targetAmount, StaticData.Translate((
							from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
							where t.galaxyKey == this.targetGalaxyKey
							select t).First<LevelMap>().nameUI));
						return str;
					}
					else
					{
						str9 = (
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam1
							select t).First<PvEInfo>().name;
						str = string.Format(StaticData.Translate("key_objective_kill_alien_in_galaxy_1"), this.targetAmount, StaticData.Translate(str9), StaticData.Translate((
							from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
							where t.galaxyKey == this.targetGalaxyKey
							select t).First<LevelMap>().nameUI));
						return str;
					}
				}
				case ObjectiveTypeEnum.FuseResource:
				{
					str = string.Format(StaticData.Translate("key_objective_fuse_resource"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam1].uiName));
					return str;
				}
				case ObjectiveTypeEnum.FuseAmmo:
				{
					str = string.Format(StaticData.Translate("key_objective_fuse_ammo"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam1].uiName));
					return str;
				}
				case ObjectiveTypeEnum.KillPlayer:
				{
					str = string.Format(StaticData.Translate("key_objective_kill_player"), this.targetAmount);
					return str;
				}
				case ObjectiveTypeEnum.KillPlayerInGalaxy:
				{
					str = string.Format(StaticData.Translate("key_objective_kill_player_in_galaxy"), this.targetAmount, StaticData.Translate((
						from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
						where t.galaxyKey == this.targetGalaxyKey
						select t).First<LevelMap>().nameUI));
					return str;
				}
				case ObjectiveTypeEnum.KillPlayerByLevel:
				{
					if (this.targetParam1 == 0)
					{
						str = string.Format(StaticData.Translate("key_objective_kill_player_below_level"), this.targetAmount, this.targetParam2);
						return str;
					}
					else if (this.targetParam1 == 1)
					{
						str = string.Format(StaticData.Translate("key_objective_kill_player_exactly_level"), this.targetAmount, this.targetParam2);
						return str;
					}
					else if (this.targetParam1 != 2)
					{
						str = "KillPlayerByLevel Incorect param";
						return str;
					}
					else
					{
						str = string.Format(StaticData.Translate("key_objective_kill_player_above_level"), this.targetAmount, this.targetParam2);
						return str;
					}
				}
				case ObjectiveTypeEnum.KillPlayerByLevelInGalaxy:
				{
					if (this.targetParam1 == 0)
					{
						str = string.Format(StaticData.Translate("key_objective_kill_player_below_level_in_galaxy"), this.targetAmount, this.targetParam2, StaticData.Translate((
							from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
							where t.galaxyKey == this.targetGalaxyKey
							select t).First<LevelMap>().nameUI));
						return str;
					}
					else if (this.targetParam1 == 1)
					{
						str = string.Format(StaticData.Translate("key_objective_kill_player_exactly_level_in_galaxy"), this.targetAmount, this.targetParam2, StaticData.Translate((
							from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
							where t.galaxyKey == this.targetGalaxyKey
							select t).First<LevelMap>().nameUI));
						return str;
					}
					else if (this.targetParam1 != 2)
					{
						str = "KillPlayerByLevelInGalaxy Incorect param";
						return str;
					}
					else
					{
						str = string.Format(StaticData.Translate("key_objective_kill_player_above_level_in_galaxy"), this.targetAmount, this.targetParam2, StaticData.Translate((
							from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
							where t.galaxyKey == this.targetGalaxyKey
							select t).First<LevelMap>().nameUI));
						return str;
					}
				}
				case ObjectiveTypeEnum.KillPlayerByShip:
				case ObjectiveTypeEnum.KillPlayerByShipInGalaxy:
				{
					string str18 = StaticData.Translate((
						from t in (IEnumerable<ShipsTypeNet>)StaticData.shipTypes
						where t.id == this.targetParam1
						select t).First<ShipsTypeNet>().shipName);
					if (this.type != ObjectiveTypeEnum.KillPlayerByShip)
					{
						str = string.Format(StaticData.Translate("key_objective_kill_player_by_ship_in_galaxy"), this.targetAmount, str18, StaticData.Translate((
							from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
							where t.galaxyKey == this.targetGalaxyKey
							select t).First<LevelMap>().nameUI));
						return str;
					}
					else
					{
						str = string.Format(StaticData.Translate("key_objective_kill_player_by_ship"), this.targetAmount, str18);
						return str;
					}
				}
				case ObjectiveTypeEnum.MakeBattleContribution:
				{
					str = string.Format(StaticData.Translate("key_objective_make_battle_contribution"), this.targetAmount);
					return str;
				}
				case ObjectiveTypeEnum.MakeBattleContributionInPoint:
				{
					str = string.Format(StaticData.Translate("key_objective_make_battle_contribution_in_point"), this.targetAmount, StaticData.Translate((
						from t in (IEnumerable<ExtractionPointInfo>)StaticData.allExtractionPoints
						where t.id == this.targetParam1
						select t).First<ExtractionPointInfo>().name));
					return str;
				}
				case ObjectiveTypeEnum.ScoreMultikill:
				case ObjectiveTypeEnum.ScoreMultikillInGalaxy:
				{
					string str19 = "";
					switch (this.targetParam1)
					{
						case 2:
						{
							str19 = "Double Kill!";
							break;
						}
						case 3:
						{
							str19 = "Triple Kill!";
							break;
						}
						case 4:
						{
							str19 = "Multi Kill!";
							break;
						}
						case 5:
						{
							str19 = "Ultra Kill!";
							break;
						}
						case 6:
						{
							str19 = "Killing Spree!";
							break;
						}
						case 7:
						{
							str19 = "Unstoppable!";
							break;
						}
						case 8:
						{
							str19 = "Devastation!";
							break;
						}
						case 9:
						{
							str19 = "Massacre!";
							break;
						}
						default:
						{
							str19 = "GODLIKE!";
							break;
						}
					}
					if (this.type != ObjectiveTypeEnum.ScoreMultikill)
					{
						str = string.Format(StaticData.Translate("key_objective_score_multikill_in_galaxy"), this.targetAmount, str19, StaticData.Translate((
							from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
							where t.galaxyKey == this.targetGalaxyKey
							select t).First<LevelMap>().nameUI));
						return str;
					}
					else
					{
						str = string.Format(StaticData.Translate("key_objective_score_multikill"), this.targetAmount, str19);
						return str;
					}
				}
				case ObjectiveTypeEnum.CaptureExtractionPoint:
				{
					str = string.Format(StaticData.Translate("key_objective_capture_extraction_point"), this.targetAmount);
					return str;
				}
				case ObjectiveTypeEnum.CaptureSpecificExtractionPoint:
				{
					str = string.Format(StaticData.Translate("key_objective_capture_specific_extraction_point"), StaticData.Translate((
						from t in (IEnumerable<ExtractionPointInfo>)StaticData.allExtractionPoints
						where t.id == this.targetParam1
						select t).First<ExtractionPointInfo>().name));
					return str;
				}
				case ObjectiveTypeEnum.WinUltralibriumFromPvPGame:
				{
					str = string.Format(StaticData.Translate("key_objective_win_ultralibrium_in_pvp"), this.targetAmount);
					return str;
				}
				case ObjectiveTypeEnum.WinPvPArenaType:
				{
					string str20 = "";
					switch (this.targetParam1)
					{
						case 1:
						{
							str20 = "key_pvp_1vs1";
							break;
						}
						case 2:
						{
							str20 = "key_pvp_2vs2";
							break;
						}
						case 3:
						{
							str20 = "key_pvp_3vs3";
							break;
						}
						case 4:
						{
							str20 = "key_pvp_4vs4";
							break;
						}
						case 5:
						{
							str20 = "key_pvp_ffa";
							break;
						}
					}
					str = string.Format(StaticData.Translate("key_objective_win_pvp_arena_type"), this.targetAmount, StaticData.Translate(str20));
					return str;
				}
				case ObjectiveTypeEnum.BuyItem:
				{
					str = string.Format(StaticData.Translate("key_objective_buy_item"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam1].uiName));
					return str;
				}
				case ObjectiveTypeEnum.ActivateBooster:
				{
					str = string.Format(StaticData.Translate("key_objective_activate_booster"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam1].uiName));
					return str;
				}
				case ObjectiveTypeEnum.GambleItem:
				{
					str = string.Format(StaticData.Translate("key_objective_gamble_item"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam1].uiName));
					return str;
				}
				case ObjectiveTypeEnum.UpgradeWeapon:
				{
					str = string.Format(StaticData.Translate("key_objective_upgrade_weapon"), this.targetParam2, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam1].uiName));
					return str;
				}
				case ObjectiveTypeEnum.UpgradeShip:
				{
					str = string.Format(StaticData.Translate("key_objective_upgrade_ship"), this.targetParam2, StaticData.Translate((
						from t in (IEnumerable<ShipsTypeNet>)StaticData.shipTypes
						where t.id == this.targetParam1
						select t).First<ShipsTypeNet>().shipName));
					return str;
				}
				case ObjectiveTypeEnum.DepositeInGuildBank:
				{
					string item = "";
					switch (this.targetParam1)
					{
						case 1:
						{
							item = StaticData.allTypes[PlayerItems.TypeNova].uiName;
							break;
						}
						case 2:
						{
							item = StaticData.allTypes[PlayerItems.TypeEquilibrium].uiName;
							break;
						}
						case 3:
						{
							item = StaticData.allTypes[PlayerItems.TypeUltralibrium].uiName;
							break;
						}
					}
					str = string.Format(StaticData.Translate("key_objective_deposit_in_guild"), this.targetAmount, StaticData.Translate(item));
					return str;
				}
				case ObjectiveTypeEnum.UpgradeExtractionPoint:
				{
					string str21 = (
						from t in (IEnumerable<ExtractionPointUpgrade>)StaticData.allExtractionPoints.First<ExtractionPointInfo>().allUpgrades
						where t.upgradeType == this.targetParam1
						select t).First<ExtractionPointUpgrade>().name;
					str = string.Format(StaticData.Translate("key_objective_upgrade_extraction_point"), this.targetParam2, StaticData.Translate(str21));
					return str;
				}
				case ObjectiveTypeEnum.CheckpointActivate:
				{
					str = string.Format(StaticData.Translate("key_objective_checkpoint_activate"), StaticData.Translate((
						from t in (IEnumerable<CheckpointObjectPhysics>)StaticData.allCheckpoints
						where t.checkpointId == this.targetParam1
						select t).First<CheckpointObjectPhysics>().checkpointName));
					return str;
				}
				case ObjectiveTypeEnum.CheckpointInvestigate:
				{
					str = string.Format(StaticData.Translate("key_objective_checkpoint_investigate"), StaticData.Translate((
						from t in (IEnumerable<CheckpointObjectPhysics>)StaticData.allCheckpoints
						where t.checkpointId == this.targetParam1
						select t).First<CheckpointObjectPhysics>().checkpointName));
					return str;
				}
				case ObjectiveTypeEnum.CheckpointSabotage:
				{
					str = string.Format(StaticData.Translate("key_objective_checkpoint_sabotage"), StaticData.Translate((
						from t in (IEnumerable<CheckpointObjectPhysics>)StaticData.allCheckpoints
						where t.checkpointId == this.targetParam1
						select t).First<CheckpointObjectPhysics>().checkpointName));
					return str;
				}
				case ObjectiveTypeEnum.TalkToNpc:
				{
					str = string.Format(StaticData.Translate("key_objective_talk_to_npc"), StaticData.Translate((
						from t in (IEnumerable<NpcObjectPhysics>)StaticData.allNPCs
						where t.npcKey == this.targetParam2
						select t).First<NpcObjectPhysics>().npcName));
					return str;
				}
				case ObjectiveTypeEnum.BringToNpc:
				{
					str = string.Format(StaticData.Translate("key_objective_bring_to_npc"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam1].uiName), StaticData.Translate((
						from t in (IEnumerable<NpcObjectPhysics>)StaticData.allNPCs
						where t.npcKey == this.targetParam2
						select t).First<NpcObjectPhysics>().npcName));
					return str;
				}
				case ObjectiveTypeEnum.EnterSpaceStationInGalaxy:
				{
					str = string.Format(StaticData.Translate("key_objective_enter_space_station_in_galaxy"), StaticData.Translate((
						from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
						where t.galaxyKey == this.targetGalaxyKey
						select t).First<LevelMap>().nameUI));
					return str;
				}
				case ObjectiveTypeEnum.DistributeSkillPoints:
				{
					str = string.Format(StaticData.Translate("key_objective_distribute_skill_points"), this.targetAmount);
					return str;
				}
				case ObjectiveTypeEnum.HavePointsAllocatedInSkill:
				{
					str = string.Format(StaticData.Translate("key_objective_have_points_allocated_in_skill"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam1].uiName));
					return str;
				}
				case ObjectiveTypeEnum.DealSkillDamageOnPlayersInSingleHit:
				{
					if (this.targetParam2 == 0)
					{
						str = string.Format(StaticData.Translate("key_objective_deal_skill_damage_on_players_in_single_hit_all"), this.targetAmount);
						return str;
					}
					else
					{
						str = string.Format(StaticData.Translate("key_objective_deal_skill_damage_on_players_in_single_hit_chosen"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam2].uiName));
						return str;
					}
				}
				case ObjectiveTypeEnum.DealSkillDamageOnPlayers:
				{
					if (this.targetParam2 == 0)
					{
						str = string.Format(StaticData.Translate("key_objective_deal_skill_damage_on_players_any"), this.targetAmount);
						return str;
					}
					else
					{
						str = string.Format(StaticData.Translate("key_objective_deal_skill_damage_on_players_chosen"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam2].uiName));
						return str;
					}
				}
				case ObjectiveTypeEnum.DealWeaponDamageOnPlayersInSingleHit:
				{
					switch (this.targetParam2)
					{
						case 0:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_players_in_single_hit_any"), this.targetAmount);
							return str;
						}
						case 1:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_players_in_single_hit_laser"), this.targetAmount);
							return str;
						}
						case 2:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_players_in_single_hit_plasma"), this.targetAmount);
							return str;
						}
						case 3:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_players_in_single_hit_ion"), this.targetAmount);
							return str;
						}
					}
					str = "DealWeaponDamageOnPlayersInSingleHit Default";
					return str;
				}
				case ObjectiveTypeEnum.DealWeaponDamageOnPlayers:
				{
					switch (this.targetParam2)
					{
						case 0:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_players_any"), this.targetAmount);
							return str;
						}
						case 1:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_players_laser"), this.targetAmount);
							return str;
						}
						case 2:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_players_plasma"), this.targetAmount);
							return str;
						}
						case 3:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_players_ion"), this.targetAmount);
							return str;
						}
					}
					str = "DealWeaponDamageOnPlayers Default";
					return str;
				}
				case ObjectiveTypeEnum.DealSkillDamageOnAliensInSingleHit:
				{
					if (this.targetParam2 == 0)
					{
						str = string.Format(StaticData.Translate("key_objective_deal_skill_damage_on_aliens_in_single_hit_all"), this.targetAmount);
						return str;
					}
					else
					{
						str = string.Format(StaticData.Translate("key_objective_deal_skill_damage_on_aliens_in_single_hit_chosen"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam2].uiName));
						return str;
					}
				}
				case ObjectiveTypeEnum.DealSkillDamageOnAliens:
				{
					if (this.targetParam2 == 0)
					{
						str = string.Format(StaticData.Translate("key_objective_deal_skill_damage_on_aliens_any"), this.targetAmount);
						return str;
					}
					else
					{
						str = string.Format(StaticData.Translate("key_objective_deal_skill_damage_on_aliens_chosen"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam2].uiName));
						return str;
					}
				}
				case ObjectiveTypeEnum.DealWeaponDamageOnAliensInSingleHit:
				{
					switch (this.targetParam2)
					{
						case 0:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_aliens_in_single_hit_any"), this.targetAmount);
							return str;
						}
						case 1:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_aliens_in_single_hit_laser"), this.targetAmount);
							return str;
						}
						case 2:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_aliens_in_single_hit_plasma"), this.targetAmount);
							return str;
						}
						case 3:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_aliens_in_single_hit_ion"), this.targetAmount);
							return str;
						}
					}
					str = "DealWeaponDamageOnAliensInSingleHit Default";
					return str;
				}
				case ObjectiveTypeEnum.DealWeaponDamageOnAliens:
				{
					switch (this.targetParam2)
					{
						case 0:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_aliens_any"), this.targetAmount);
							return str;
						}
						case 1:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_aliens_laser"), this.targetAmount);
							return str;
						}
						case 2:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_aliens_plasma"), this.targetAmount);
							return str;
						}
						case 3:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_aliens_ion"), this.targetAmount);
							return str;
						}
					}
					str = "DealWeaponDamageOnAliens Default";
					return str;
				}
				case ObjectiveTypeEnum.DealCriticalDamageOnAliensInSingleHit:
				{
					str = string.Format(StaticData.Translate("key_objective_deal_critical_damage_on_aliens_in_single_hit"), this.targetAmount);
					return str;
				}
				case ObjectiveTypeEnum.DealCriticalDamageOnAliens:
				{
					str = string.Format(StaticData.Translate("key_objective_deal_critical_damage_on_aliens"), this.targetAmount);
					return str;
				}
				case ObjectiveTypeEnum.DealSkillDamageOnAliensInSingleHitInGalaxy:
				{
					if (this.targetParam2 == 0)
					{
						str = string.Format(StaticData.Translate("key_objective_deal_skill_damage_on_aliens_in_single_hit_galaxy_all"), this.targetAmount, StaticData.Translate((
							from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
							where t.galaxyKey == this.targetGalaxyKey
							select t).First<LevelMap>().nameUI));
						return str;
					}
					else
					{
						str = string.Format(StaticData.Translate("key_objective_deal_skill_damage_on_aliens_in_single_hit_galaxy_chosen"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam2].uiName), StaticData.Translate((
							from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
							where t.galaxyKey == this.targetGalaxyKey
							select t).First<LevelMap>().nameUI));
						return str;
					}
				}
				case ObjectiveTypeEnum.DealSkillDamageOnAliensInGalaxy:
				{
					if (this.targetParam2 == 0)
					{
						str = string.Format(StaticData.Translate("key_objective_deal_skill_damage_on_aliens_galaxy_any"), this.targetAmount, StaticData.Translate((
							from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
							where t.galaxyKey == this.targetGalaxyKey
							select t).First<LevelMap>().nameUI));
						return str;
					}
					else
					{
						str = string.Format(StaticData.Translate("key_objective_deal_skill_damage_on_aliens_galaxy_chosen"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam2].uiName), StaticData.Translate((
							from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
							where t.galaxyKey == this.targetGalaxyKey
							select t).First<LevelMap>().nameUI));
						return str;
					}
				}
				case ObjectiveTypeEnum.DealWeaponDamageOnAliensInSingleHitInGalaxy:
				{
					switch (this.targetParam2)
					{
						case 0:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_aliens_in_single_hit_galaxy_any"), this.targetAmount, StaticData.Translate((
								from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
								where t.galaxyKey == this.targetGalaxyKey
								select t).First<LevelMap>().nameUI));
							return str;
						}
						case 1:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_aliens_in_single_hit_galaxy_laser"), this.targetAmount, StaticData.Translate((
								from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
								where t.galaxyKey == this.targetGalaxyKey
								select t).First<LevelMap>().nameUI));
							return str;
						}
						case 2:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_aliens_in_single_hit_galaxy_plasma"), this.targetAmount, StaticData.Translate((
								from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
								where t.galaxyKey == this.targetGalaxyKey
								select t).First<LevelMap>().nameUI));
							return str;
						}
						case 3:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_aliens_in_single_hit_galaxy_ion"), this.targetAmount, StaticData.Translate((
								from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
								where t.galaxyKey == this.targetGalaxyKey
								select t).First<LevelMap>().nameUI));
							return str;
						}
					}
					str = "DealWeaponDamageOnAliensInSingleHitInGalaxy Default";
					return str;
				}
				case ObjectiveTypeEnum.DealWeaponDamageOnAliensInGalaxy:
				{
					switch (this.targetParam2)
					{
						case 0:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_aliens_galaxy_any"), this.targetAmount, StaticData.Translate((
								from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
								where t.galaxyKey == this.targetGalaxyKey
								select t).First<LevelMap>().nameUI));
							return str;
						}
						case 1:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_aliens_galaxy_laser"), this.targetAmount, StaticData.Translate((
								from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
								where t.galaxyKey == this.targetGalaxyKey
								select t).First<LevelMap>().nameUI));
							return str;
						}
						case 2:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_aliens_galaxy_plasma"), this.targetAmount, StaticData.Translate((
								from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
								where t.galaxyKey == this.targetGalaxyKey
								select t).First<LevelMap>().nameUI));
							return str;
						}
						case 3:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_aliens_galaxy_ion"), this.targetAmount, StaticData.Translate((
								from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
								where t.galaxyKey == this.targetGalaxyKey
								select t).First<LevelMap>().nameUI));
							return str;
						}
					}
					str = "DealWeaponDamageOnAliensInGalaxy Default";
					return str;
				}
				case ObjectiveTypeEnum.DealCriticalDamageOnAliensInSingleHitInGalaxy:
				{
					str = string.Format(StaticData.Translate("key_objective_deal_critical_damage_on_aliens_in_single_hit_galaxy"), this.targetAmount, StaticData.Translate((
						from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
						where t.galaxyKey == this.targetGalaxyKey
						select t).First<LevelMap>().nameUI));
					return str;
				}
				case ObjectiveTypeEnum.DealCriticalDamageOnAliensInGalaxy:
				{
					str = string.Format(StaticData.Translate("key_objective_deal_critical_damage_on_aliens_galaxy"), this.targetAmount, StaticData.Translate((
						from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
						where t.galaxyKey == this.targetGalaxyKey
						select t).First<LevelMap>().nameUI));
					return str;
				}
				case ObjectiveTypeEnum.DealSkillDamageOnBossesInSingleHit:
				{
					if (this.targetParam2 == 0)
					{
						str = string.Format(StaticData.Translate("key_objective_deal_skill_damage_on_bosses_in_single_hit_all"), this.targetAmount);
						return str;
					}
					else
					{
						str = string.Format(StaticData.Translate("key_objective_deal_skill_damage_on_bosses_in_single_hit_chosen"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam2].uiName));
						return str;
					}
				}
				case ObjectiveTypeEnum.DealSkillDamageOnBosses:
				{
					if (this.targetParam2 == 0)
					{
						str = string.Format(StaticData.Translate("key_objective_deal_skill_damage_on_bosses_any"), this.targetAmount);
						return str;
					}
					else
					{
						str = string.Format(StaticData.Translate("key_objective_deal_skill_damage_on_bosses_chosen"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam2].uiName));
						return str;
					}
				}
				case ObjectiveTypeEnum.DealWeaponDamageOnBossesInSingleHit:
				{
					switch (this.targetParam2)
					{
						case 0:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_bosses_in_single_hit_any"), this.targetAmount);
							return str;
						}
						case 1:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_bosses_in_single_hit_laser"), this.targetAmount);
							return str;
						}
						case 2:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_bosses_in_single_hit_plasma"), this.targetAmount);
							return str;
						}
						case 3:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_bosses_in_single_hit_ion"), this.targetAmount);
							return str;
						}
					}
					str = "DealWeaponDamageOnAliensInGalaxy Default";
					return str;
				}
				case ObjectiveTypeEnum.DealWeaponDamageOnBosses:
				{
					switch (this.targetParam2)
					{
						case 0:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_bosses_any"), this.targetAmount);
							return str;
						}
						case 1:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_bosses_laser"), this.targetAmount);
							return str;
						}
						case 2:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_bosses_plasma"), this.targetAmount);
							return str;
						}
						case 3:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_bosses_ion"), this.targetAmount);
							return str;
						}
					}
					str = "DealWeaponDamageOnAliensInGalaxy Default";
					return str;
				}
				case ObjectiveTypeEnum.DealCriticalDamageOnBossesInSingleHit:
				{
					str = string.Format(StaticData.Translate("key_objective_deal_critical_damage_on_bosses_in_single_hit"), this.targetAmount);
					return str;
				}
				case ObjectiveTypeEnum.DealCriticalDamageOnBosses:
				{
					str = string.Format(StaticData.Translate("key_objective_deal_critical_damage_on_bosses"), this.targetAmount);
					return str;
				}
				case ObjectiveTypeEnum.DealSkillDamageOnBossesInSingleHitInGalaxy:
				{
					if (this.targetParam2 == 0)
					{
						str = string.Format(StaticData.Translate("key_objective_deal_skill_damage_on_bosses_in_single_hit_galaxy_all"), this.targetAmount, StaticData.Translate((
							from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
							where t.galaxyKey == this.targetGalaxyKey
							select t).First<LevelMap>().nameUI));
						return str;
					}
					else
					{
						str = string.Format(StaticData.Translate("key_objective_deal_skill_damage_on_bosses_in_single_hit_galaxy_chosen"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam2].uiName), StaticData.Translate((
							from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
							where t.galaxyKey == this.targetGalaxyKey
							select t).First<LevelMap>().nameUI));
						return str;
					}
				}
				case ObjectiveTypeEnum.DealSkillDamageOnBossesInGalaxy:
				{
					if (this.targetParam2 == 0)
					{
						str = string.Format(StaticData.Translate("key_objective_deal_skill_damage_on_bosses_galaxy_chosen"), this.targetAmount, StaticData.Translate((
							from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
							where t.galaxyKey == this.targetGalaxyKey
							select t).First<LevelMap>().nameUI));
						return str;
					}
					else
					{
						str = string.Format(StaticData.Translate("key_objective_deal_skill_damage_on_bosses_galaxy_any"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam2].uiName), StaticData.Translate((
							from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
							where t.galaxyKey == this.targetGalaxyKey
							select t).First<LevelMap>().nameUI));
						return str;
					}
				}
				case ObjectiveTypeEnum.DealWeaponDamageOnBossesInSingleHitInGalaxy:
				{
					switch (this.targetParam2)
					{
						case 0:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_bosses_in_single_hit_galaxy_any"), this.targetAmount, StaticData.Translate((
								from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
								where t.galaxyKey == this.targetGalaxyKey
								select t).First<LevelMap>().nameUI));
							return str;
						}
						case 1:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_bosses_in_single_hit_galaxy_laser"), this.targetAmount, StaticData.Translate((
								from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
								where t.galaxyKey == this.targetGalaxyKey
								select t).First<LevelMap>().nameUI));
							return str;
						}
						case 2:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_bosses_in_single_hit_galaxy_plasma"), this.targetAmount, StaticData.Translate((
								from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
								where t.galaxyKey == this.targetGalaxyKey
								select t).First<LevelMap>().nameUI));
							return str;
						}
						case 3:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_bosses_in_single_hit_galaxy_ion"), this.targetAmount, StaticData.Translate((
								from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
								where t.galaxyKey == this.targetGalaxyKey
								select t).First<LevelMap>().nameUI));
							return str;
						}
					}
					str = "DealWeaponDamageOnBossesInSingleHitInGalaxy Default";
					return str;
				}
				case ObjectiveTypeEnum.DealWeaponDamageOnBossesInGalaxy:
				{
					switch (this.targetParam2)
					{
						case 0:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_bosses_galaxy_any"), this.targetAmount, StaticData.Translate((
								from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
								where t.galaxyKey == this.targetGalaxyKey
								select t).First<LevelMap>().nameUI));
							return str;
						}
						case 1:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_bosses_galaxy_laser"), this.targetAmount, StaticData.Translate((
								from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
								where t.galaxyKey == this.targetGalaxyKey
								select t).First<LevelMap>().nameUI));
							return str;
						}
						case 2:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_bosses_galaxy_plasma"), this.targetAmount, StaticData.Translate((
								from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
								where t.galaxyKey == this.targetGalaxyKey
								select t).First<LevelMap>().nameUI));
							return str;
						}
						case 3:
						{
							str = string.Format(StaticData.Translate("key_objective_deal_weapon_damage_on_bosses_galaxy_ion"), this.targetAmount, StaticData.Translate((
								from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
								where t.galaxyKey == this.targetGalaxyKey
								select t).First<LevelMap>().nameUI));
							return str;
						}
					}
					str = "DealWeaponDamageOnBossesInGalaxy Default";
					return str;
				}
				case ObjectiveTypeEnum.DealCriticalDamageOnBossesInSingleHitInGalaxy:
				{
					str = string.Format(StaticData.Translate("key_objective_deal_critical_damage_on_bosses_in_single_hit_galaxy"), this.targetAmount, StaticData.Translate((
						from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
						where t.galaxyKey == this.targetGalaxyKey
						select t).First<LevelMap>().nameUI));
					return str;
				}
				case ObjectiveTypeEnum.DealCriticalDamageOnBossesInGalaxy:
				{
					str = string.Format(StaticData.Translate("key_objective_deal_critical_damage_on_bosses_galaxy"), this.targetAmount, StaticData.Translate((
						from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
						where t.galaxyKey == this.targetGalaxyKey
						select t).First<LevelMap>().nameUI));
					return str;
				}
				case ObjectiveTypeEnum.ScoreCriticalStrike:
				{
					str = string.Format(StaticData.Translate("key_objective_score_critical_strike"), this.targetAmount);
					return str;
				}
				case ObjectiveTypeEnum.ScoreCriticalStrikeInGalaxy:
				{
					str = string.Format(StaticData.Translate("key_objective_score_critical_strike_in_galaxy"), this.targetAmount, StaticData.Translate((
						from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
						where t.galaxyKey == this.targetGalaxyKey
						select t).First<LevelMap>().nameUI));
					return str;
				}
				case ObjectiveTypeEnum.HealDamageInSingleHitSelf:
				{
					if (this.targetParam2 == 0)
					{
						str = string.Format(StaticData.Translate("key_objective_heal_damage_single_hit_self_any"), this.targetAmount);
						return str;
					}
					else
					{
						str = string.Format(StaticData.Translate("key_objective_heal_damage_single_hit_self_chosen"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam2].uiName));
						return str;
					}
				}
				case ObjectiveTypeEnum.HealDamageSelf:
				{
					if (this.targetParam2 == 0)
					{
						str = string.Format(StaticData.Translate("key_objective_heal_damage_self_any"), this.targetAmount);
						return str;
					}
					else
					{
						str = string.Format(StaticData.Translate("key_objective_heal_damage_self_chosen"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam2].uiName));
						return str;
					}
				}
				case ObjectiveTypeEnum.HealDamageInSingleHitParty:
				{
					if (this.targetParam2 == 0)
					{
						str = string.Format(StaticData.Translate("key_objective_heal_damage_single_hit_party_any"), this.targetAmount);
						return str;
					}
					else
					{
						str = string.Format(StaticData.Translate("key_objective_heal_damage_single_hit_party_chosen"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam2].uiName));
						return str;
					}
				}
				case ObjectiveTypeEnum.HealDamageParty:
				{
					if (this.targetParam2 == 0)
					{
						str = string.Format(StaticData.Translate("key_objective_heal_damage_party_any"), this.targetAmount);
						return str;
					}
					else
					{
						str = string.Format(StaticData.Translate("key_objective_heal_damage_party_chosen"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam2].uiName));
						return str;
					}
				}
				case ObjectiveTypeEnum.HealDamageInSingleHit:
				{
					if (this.targetParam2 == 0)
					{
						str = string.Format(StaticData.Translate("key_objective_heal_damage_single_hit_any"), this.targetAmount);
						return str;
					}
					else
					{
						str = string.Format(StaticData.Translate("key_objective_heal_damage_single_hit_chosen"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam2].uiName));
						return str;
					}
				}
				case ObjectiveTypeEnum.HealDamage:
				{
					if (this.targetParam2 == 0)
					{
						str = string.Format(StaticData.Translate("key_objective_heal_damage_any"), this.targetAmount);
						return str;
					}
					else
					{
						str = string.Format(StaticData.Translate("key_objective_heal_damage_chosen"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam2].uiName));
						return str;
					}
				}
				case ObjectiveTypeEnum.SpendCurrency:
				{
					switch (this.targetParam1)
					{
						case 0:
						{
							str = string.Format(StaticData.Translate("key_objective_spend_currency"), this.targetAmount, StaticData.Translate(StaticData.allTypes[PlayerItems.TypeCash].uiName));
							return str;
						}
						case 1:
						{
							str = string.Format(StaticData.Translate("key_objective_spend_currency"), this.targetAmount, StaticData.Translate(StaticData.allTypes[PlayerItems.TypeNova].uiName));
							return str;
						}
						case 2:
						{
							str = string.Format(StaticData.Translate("key_objective_spend_currency"), this.targetAmount, StaticData.Translate(StaticData.allTypes[PlayerItems.TypeEquilibrium].uiName));
							return str;
						}
						case 3:
						{
							str = string.Format(StaticData.Translate("key_objective_spend_currency"), this.targetAmount, StaticData.Translate(StaticData.allTypes[PlayerItems.TypeUltralibrium].uiName));
							return str;
						}
					}
					str = "SpendCurrency Default";
					return str;
				}
				case ObjectiveTypeEnum.EarnFromSelling:
				{
					switch (this.targetParam1)
					{
						case 1:
						{
							str = string.Format(StaticData.Translate("key_objective_earn_from_selling_items"), this.targetAmount, StaticData.Translate(StaticData.allTypes[PlayerItems.TypeNova].uiName));
							return str;
						}
						case 2:
						{
							str = string.Format(StaticData.Translate("key_objective_earn_from_selling_resources"), this.targetAmount, StaticData.Translate(StaticData.allTypes[PlayerItems.TypeEquilibrium].uiName));
							return str;
						}
						case 3:
						{
							str = string.Format(StaticData.Translate("key_objective_earn_from_selling_any"), this.targetAmount, StaticData.Translate(StaticData.allTypes[PlayerItems.TypeUltralibrium].uiName));
							return str;
						}
					}
					str = "EarnFromSelling Default";
					return str;
				}
				case ObjectiveTypeEnum.Sell:
				{
					str = string.Format(StaticData.Translate("key_objective_sell"), this.targetAmount, StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam1].uiName));
					return str;
				}
				case ObjectiveTypeEnum.GoToGalaxy:
				{
					str = string.Format(StaticData.Translate("key_objective_go_to_galaxy"), StaticData.Translate((
						from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
						where t.galaxyKey == this.targetGalaxyKey
						select t).First<LevelMap>().nameUI));
					return str;
				}
				case ObjectiveTypeEnum.UseBoost:
				{
					str = string.Format(StaticData.Translate("key_objective_use_boost"), new object[0]);
					return str;
				}
				case ObjectiveTypeEnum.BuyShip:
				{
					if (this.targetParam1 == 0)
					{
						str = string.Format(StaticData.Translate("key_objective_buy_ship_any"), new object[0]);
						return str;
					}
					else
					{
						str = string.Format(StaticData.Translate("key_objective_buy_ship_chosen"), StaticData.Translate((
							from t in (IEnumerable<ShipsTypeNet>)StaticData.shipTypes
							where t.id == this.targetParam1
							select t).First<ShipsTypeNet>().shipName));
						return str;
					}
				}
				case ObjectiveTypeEnum.BuyItems:
				{
					switch (this.targetParam1)
					{
						case 0:
						{
							str = string.Format(StaticData.Translate("key_objective_buy_items_any"), this.targetAmount);
							return str;
						}
						case 1:
						{
							str = string.Format(StaticData.Translate("key_objective_buy_items_weapon"), this.targetAmount);
							return str;
						}
						case 2:
						{
							str = string.Format(StaticData.Translate("key_objective_buy_items_corpus"), this.targetAmount);
							return str;
						}
						case 3:
						{
							str = string.Format(StaticData.Translate("key_objective_buy_items_shield"), this.targetAmount);
							return str;
						}
						case 4:
						{
							str = string.Format(StaticData.Translate("key_objective_buy_items_engine"), this.targetAmount);
							return str;
						}
						case 5:
						{
							str = string.Format(StaticData.Translate("key_objective_gamble_items_extra"), this.targetAmount);
							return str;
						}
					}
					str = "EarnFromSelling Default";
					return str;
				}
				case ObjectiveTypeEnum.GambleItems:
				{
					switch (this.targetParam1)
					{
						case 0:
						{
							str = string.Format(StaticData.Translate("key_objective_gamble_items_any"), this.targetAmount);
							return str;
						}
						case 1:
						{
							str = string.Format(StaticData.Translate("key_objective_gamble_items_weapon"), this.targetAmount);
							return str;
						}
						case 2:
						{
							str = string.Format(StaticData.Translate("key_objective_gamble_items_corpus"), this.targetAmount);
							return str;
						}
						case 3:
						{
							str = string.Format(StaticData.Translate("key_objective_gamble_items_shield"), this.targetAmount);
							return str;
						}
						case 4:
						{
							str = string.Format(StaticData.Translate("key_objective_buy_items_engine"), this.targetAmount);
							return str;
						}
						case 5:
						{
							str = string.Format(StaticData.Translate("key_objective_gamble_items_extra"), this.targetAmount);
							return str;
						}
					}
					str = "EarnFromSelling Default";
					return str;
				}
				case ObjectiveTypeEnum.SellItems:
				{
					str = string.Format(StaticData.Translate("key_objective_sell_items"), this.targetAmount);
					return str;
				}
				case ObjectiveTypeEnum.MakeWeaponsUpgrades:
				{
					str = string.Format(StaticData.Translate("key_objective_make_weapon_upgrades"), this.targetAmount);
					return str;
				}
				case ObjectiveTypeEnum.MakeShipsUpgrades:
				{
					str = string.Format(StaticData.Translate("key_objective_make_ship_upgrades"), this.targetAmount);
					return str;
				}
				case ObjectiveTypeEnum.MakeUpgrades:
				{
					str = string.Format(StaticData.Translate("key_objective_make_upgrades"), this.targetAmount);
					return str;
				}
			}
			str = "GetObjectiveDescription Default";
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			GameObjectPhysics.Log(exception.ToString());
			str = exception.ToString();
		}
		return str;
	}

	public string GetObjectiveShortDescription()
	{
		string str;
		try
		{
			switch (this.type)
			{
				case ObjectiveTypeEnum.OnlyForTutorial:
				{
					str = StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam1].uiName);
					return str;
				}
				case ObjectiveTypeEnum.GatherResource:
				case ObjectiveTypeEnum.GatherResourceInGalaxy:
				case ObjectiveTypeEnum.CollectResource:
				case ObjectiveTypeEnum.CollectResourceInGalaxy:
				{
					str = StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam1].uiName);
					return str;
				}
				case ObjectiveTypeEnum.CollectSlotItem:
				case ObjectiveTypeEnum.CollectSlotItemInGalaxy:
				{
					str = StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam1].uiName);
					return str;
				}
				case ObjectiveTypeEnum.KillAlien:
				case ObjectiveTypeEnum.KillAlienInGalaxy:
				{
					if (this.targetParam1 == 0)
					{
						str = StaticData.Translate("key_objective_short_desc_aliens");
						return str;
					}
					else if (this.targetParam2 != 0)
					{
						str = StaticData.Translate("key_objective_short_desc_alien_species");
						return str;
					}
					else
					{
						str = StaticData.Translate((
							from t in (IEnumerable<PvEInfo>)StaticData.pveTypes
							where t.pveKey == this.targetParam1
							select t).First<PvEInfo>().name);
						return str;
					}
				}
				case ObjectiveTypeEnum.FuseResource:
				{
					str = StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam1].uiName);
					return str;
				}
				case ObjectiveTypeEnum.FuseAmmo:
				{
					str = StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam1].uiName);
					return str;
				}
				case ObjectiveTypeEnum.KillPlayer:
				case ObjectiveTypeEnum.KillPlayerInGalaxy:
				case ObjectiveTypeEnum.KillPlayerByLevel:
				case ObjectiveTypeEnum.KillPlayerByLevelInGalaxy:
				case ObjectiveTypeEnum.KillPlayerByShip:
				case ObjectiveTypeEnum.KillPlayerByShipInGalaxy:
				{
					str = StaticData.Translate("key_objective_short_desc_players");
					return str;
				}
				case ObjectiveTypeEnum.MakeBattleContribution:
				{
					str = StaticData.Translate("key_objective_short_desc_contribution");
					return str;
				}
				case ObjectiveTypeEnum.MakeBattleContributionInPoint:
				{
					str = StaticData.Translate((
						from t in (IEnumerable<ExtractionPointInfo>)StaticData.allExtractionPoints
						where t.id == this.targetParam1
						select t).First<ExtractionPointInfo>().name);
					return str;
				}
				case ObjectiveTypeEnum.ScoreMultikill:
				case ObjectiveTypeEnum.ScoreMultikillInGalaxy:
				{
					string str1 = "";
					switch (this.targetParam1)
					{
						case 2:
						{
							str1 = "Double Kill!";
							break;
						}
						case 3:
						{
							str1 = "Triple Kill!";
							break;
						}
						case 4:
						{
							str1 = "Multi Kill!";
							break;
						}
						case 5:
						{
							str1 = "Ultra Kill!";
							break;
						}
						case 6:
						{
							str1 = "Killing Spree!";
							break;
						}
						case 7:
						{
							str1 = "Unstoppable!";
							break;
						}
						case 8:
						{
							str1 = "Devastation!";
							break;
						}
						case 9:
						{
							str1 = "Massacre!";
							break;
						}
						default:
						{
							str1 = "GODLIKE!";
							break;
						}
					}
					str = str1;
					return str;
				}
				case ObjectiveTypeEnum.CaptureExtractionPoint:
				{
					str = StaticData.Translate("key_objective_short_desc_kill_extraction_points");
					return str;
				}
				case ObjectiveTypeEnum.CaptureSpecificExtractionPoint:
				{
					str = StaticData.Translate((
						from t in (IEnumerable<ExtractionPointInfo>)StaticData.allExtractionPoints
						where t.id == this.targetParam1
						select t).First<ExtractionPointInfo>().name);
					return str;
				}
				case ObjectiveTypeEnum.WinUltralibriumFromPvPGame:
				{
					str = StaticData.Translate(StaticData.allTypes[PlayerItems.TypeUltralibrium].uiName);
					return str;
				}
				case ObjectiveTypeEnum.WinPvPArenaType:
				{
					string str2 = "";
					switch (this.targetParam1)
					{
						case 1:
						{
							str2 = "key_pvp_1vs1";
							break;
						}
						case 2:
						{
							str2 = "key_pvp_2vs2";
							break;
						}
						case 3:
						{
							str2 = "key_pvp_3vs3";
							break;
						}
						case 4:
						{
							str2 = "key_pvp_4vs4";
							break;
						}
						case 5:
						{
							str2 = "key_pvp_ffa";
							break;
						}
					}
					str = StaticData.Translate(str2);
					return str;
				}
				case ObjectiveTypeEnum.BuyItem:
				case ObjectiveTypeEnum.ActivateBooster:
				case ObjectiveTypeEnum.GambleItem:
				case ObjectiveTypeEnum.UpgradeWeapon:
				{
					str = StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam1].uiName);
					return str;
				}
				case ObjectiveTypeEnum.UpgradeShip:
				{
					str = StaticData.Translate((
						from t in (IEnumerable<ShipsTypeNet>)StaticData.shipTypes
						where t.id == this.targetParam1
						select t).First<ShipsTypeNet>().shipName);
					return str;
				}
				case ObjectiveTypeEnum.DepositeInGuildBank:
				{
					string item = "";
					switch (this.targetParam1)
					{
						case 1:
						{
							item = StaticData.allTypes[PlayerItems.TypeNova].uiName;
							break;
						}
						case 2:
						{
							item = StaticData.allTypes[PlayerItems.TypeEquilibrium].uiName;
							break;
						}
						case 3:
						{
							item = StaticData.allTypes[PlayerItems.TypeUltralibrium].uiName;
							break;
						}
					}
					str = StaticData.Translate(item);
					return str;
				}
				case ObjectiveTypeEnum.UpgradeExtractionPoint:
				{
					string str3 = (
						from t in (IEnumerable<ExtractionPointUpgrade>)StaticData.allExtractionPoints.First<ExtractionPointInfo>().allUpgrades
						where t.upgradeType == this.targetParam1
						select t).First<ExtractionPointUpgrade>().name;
					str = StaticData.Translate(str3);
					return str;
				}
				case ObjectiveTypeEnum.CheckpointActivate:
				case ObjectiveTypeEnum.CheckpointInvestigate:
				case ObjectiveTypeEnum.CheckpointSabotage:
				{
					str = StaticData.Translate((
						from t in (IEnumerable<CheckpointObjectPhysics>)StaticData.allCheckpoints
						where t.checkpointId == this.targetParam1
						select t).First<CheckpointObjectPhysics>().checkpointName);
					return str;
				}
				case ObjectiveTypeEnum.TalkToNpc:
				{
					str = StaticData.Translate((
						from t in (IEnumerable<NpcObjectPhysics>)StaticData.allNPCs
						where t.npcKey == this.targetParam2
						select t).First<NpcObjectPhysics>().npcName);
					return str;
				}
				case ObjectiveTypeEnum.BringToNpc:
				{
					str = StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam1].uiName);
					return str;
				}
				case ObjectiveTypeEnum.EnterSpaceStationInGalaxy:
				{
					str = StaticData.Translate((
						from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
						where t.galaxyKey == this.targetGalaxyKey
						select t).First<LevelMap>().nameUI);
					return str;
				}
				case ObjectiveTypeEnum.DistributeSkillPoints:
				{
					str = StaticData.Translate("key_objective_short_desc_skillpoints");
					return str;
				}
				case ObjectiveTypeEnum.HavePointsAllocatedInSkill:
				{
					str = StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam1].uiName);
					return str;
				}
				case ObjectiveTypeEnum.DealSkillDamageOnPlayersInSingleHit:
				case ObjectiveTypeEnum.DealSkillDamageOnPlayers:
				case ObjectiveTypeEnum.DealSkillDamageOnAliensInSingleHit:
				case ObjectiveTypeEnum.DealSkillDamageOnAliens:
				case ObjectiveTypeEnum.DealSkillDamageOnAliensInSingleHitInGalaxy:
				case ObjectiveTypeEnum.DealSkillDamageOnAliensInGalaxy:
				case ObjectiveTypeEnum.DealSkillDamageOnBossesInSingleHit:
				case ObjectiveTypeEnum.DealSkillDamageOnBosses:
				case ObjectiveTypeEnum.DealSkillDamageOnBossesInSingleHitInGalaxy:
				case ObjectiveTypeEnum.DealSkillDamageOnBossesInGalaxy:
				{
					if (this.targetParam2 == 0)
					{
						str = StaticData.Translate("key_objective_short_desc_skill_damage");
						return str;
					}
					else
					{
						str = StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam2].uiName);
						return str;
					}
				}
				case ObjectiveTypeEnum.DealWeaponDamageOnPlayersInSingleHit:
				case ObjectiveTypeEnum.DealWeaponDamageOnPlayers:
				case ObjectiveTypeEnum.DealWeaponDamageOnAliensInSingleHit:
				case ObjectiveTypeEnum.DealWeaponDamageOnAliens:
				case ObjectiveTypeEnum.DealWeaponDamageOnAliensInSingleHitInGalaxy:
				case ObjectiveTypeEnum.DealWeaponDamageOnAliensInGalaxy:
				case ObjectiveTypeEnum.DealWeaponDamageOnBossesInSingleHit:
				case ObjectiveTypeEnum.DealWeaponDamageOnBosses:
				case ObjectiveTypeEnum.DealWeaponDamageOnBossesInSingleHitInGalaxy:
				case ObjectiveTypeEnum.DealWeaponDamageOnBossesInGalaxy:
				{
					switch (this.targetParam2)
					{
						case 0:
						{
							str = StaticData.Translate("key_objective_short_desc_weapon_damage");
							return str;
						}
						case 1:
						{
							str = StaticData.Translate("key_objective_short_desc_laser_damage");
							return str;
						}
						case 2:
						{
							str = StaticData.Translate("key_objective_short_desc_plasma_damage");
							return str;
						}
						case 3:
						{
							str = StaticData.Translate("key_objective_short_desc_ion_damage");
							return str;
						}
					}
					str = "DealWeaponDamage Default";
					return str;
				}
				case ObjectiveTypeEnum.DealCriticalDamageOnAliensInSingleHit:
				case ObjectiveTypeEnum.DealCriticalDamageOnAliens:
				case ObjectiveTypeEnum.DealCriticalDamageOnAliensInSingleHitInGalaxy:
				case ObjectiveTypeEnum.DealCriticalDamageOnAliensInGalaxy:
				case ObjectiveTypeEnum.DealCriticalDamageOnBossesInSingleHit:
				case ObjectiveTypeEnum.DealCriticalDamageOnBosses:
				case ObjectiveTypeEnum.DealCriticalDamageOnBossesInSingleHitInGalaxy:
				case ObjectiveTypeEnum.DealCriticalDamageOnBossesInGalaxy:
				{
					str = StaticData.Translate("key_objective_short_desc_critical_strike_damage");
					return str;
				}
				case ObjectiveTypeEnum.ScoreCriticalStrike:
				case ObjectiveTypeEnum.ScoreCriticalStrikeInGalaxy:
				{
					str = StaticData.Translate("key_objective_short_desc_critical_strike");
					return str;
				}
				case ObjectiveTypeEnum.HealDamageInSingleHitSelf:
				case ObjectiveTypeEnum.HealDamageSelf:
				case ObjectiveTypeEnum.HealDamageInSingleHitParty:
				case ObjectiveTypeEnum.HealDamageParty:
				case ObjectiveTypeEnum.HealDamageInSingleHit:
				case ObjectiveTypeEnum.HealDamage:
				{
					if (this.targetParam2 == 0)
					{
						str = StaticData.Translate("key_objective_short_desc_damage_repair");
						return str;
					}
					else
					{
						str = StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam2].uiName);
						return str;
					}
				}
				case ObjectiveTypeEnum.SpendCurrency:
				{
					switch (this.targetParam1)
					{
						case 0:
						{
							str = StaticData.Translate(StaticData.allTypes[PlayerItems.TypeCash].uiName);
							return str;
						}
						case 1:
						{
							str = StaticData.Translate(StaticData.allTypes[PlayerItems.TypeNova].uiName);
							return str;
						}
						case 2:
						{
							str = StaticData.Translate(StaticData.allTypes[PlayerItems.TypeEquilibrium].uiName);
							return str;
						}
						case 3:
						{
							str = StaticData.Translate(StaticData.allTypes[PlayerItems.TypeUltralibrium].uiName);
							return str;
						}
					}
					str = "SpendCurrency Default";
					return str;
				}
				case ObjectiveTypeEnum.EarnFromSelling:
				{
					str = StaticData.Translate(StaticData.allTypes[PlayerItems.TypeCash].uiName);
					return str;
				}
				case ObjectiveTypeEnum.Sell:
				{
					str = StaticData.Translate(StaticData.allTypes[(ushort)this.targetParam1].uiName);
					return str;
				}
				case ObjectiveTypeEnum.GoToGalaxy:
				{
					str = StaticData.Translate((
						from t in (IEnumerable<LevelMap>)StaticData.allGalaxies
						where t.galaxyKey == this.targetGalaxyKey
						select t).First<LevelMap>().nameUI);
					return str;
				}
				case ObjectiveTypeEnum.UseBoost:
				{
					str = StaticData.Translate("key_objective_short_desc_speed_boost");
					return str;
				}
				case ObjectiveTypeEnum.BuyShip:
				{
					if (this.targetParam1 == 0)
					{
						str = string.Format(StaticData.Translate("key_objective_short_desc_buy_ship"), new object[0]);
						return str;
					}
					else
					{
						str = StaticData.Translate((
							from t in (IEnumerable<ShipsTypeNet>)StaticData.shipTypes
							where t.id == this.targetParam1
							select t).First<ShipsTypeNet>().shipName);
						return str;
					}
				}
				case ObjectiveTypeEnum.BuyItems:
				case ObjectiveTypeEnum.GambleItems:
				case ObjectiveTypeEnum.SellItems:
				{
					str = StaticData.Translate("key_objective_short_desc_items");
					return str;
				}
				case ObjectiveTypeEnum.MakeWeaponsUpgrades:
				{
					str = StaticData.Translate("key_objective_short_desc_weapon_upgrades");
					return str;
				}
				case ObjectiveTypeEnum.MakeShipsUpgrades:
				{
					str = StaticData.Translate("key_objective_short_desc_ship_upgrades");
					return str;
				}
				case ObjectiveTypeEnum.MakeUpgrades:
				{
					str = StaticData.Translate("key_objective_short_desc_upgrades");
					return str;
				}
			}
			str = "GetObjectiveShortDescription Default";
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			GameObjectPhysics.Log(exception.ToString());
			str = exception.ToString();
		}
		return str;
	}

	public bool IsComplete(PlayerData plr)
	{
		bool flag;
		flag = (plr.playerBelongings.playerObjectives.GetAmountAt(this.id) < this.targetAmount ? false : true);
		return flag;
	}
}