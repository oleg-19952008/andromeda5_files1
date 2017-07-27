using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

public class TransferablesFramework
{
	public static SortedList<byte, Type> types;

	static TransferablesFramework()
	{
		TransferablesFramework.types = new SortedList<byte, Type>()
		{
			{ 1, typeof(AuthorizeRequest) },
			{ 2, typeof(AuthorizeResult) },
			{ 3, typeof(AddPlr2UniverseRequest) },
			{ 4, typeof(AddPlr2UniverseResponse) },
			{ 5, typeof(WeaponSlot) },
			{ 6, typeof(AmmoType) },
			{ 7, typeof(ShootingPlayer) },
			{ 8, typeof(QuestObjective) },
			{ 9, typeof(RecrutedPlayers) },
			{ 10, typeof(PlayerQuest) },
			{ 11, typeof(ShipConfiguration) },
			{ 12, typeof(GameObjectPhysics) },
			{ 13, typeof(PvEInfo) },
			{ 14, typeof(AmmoNet) },
			{ 15, typeof(WeaponInfo) },
			{ 16, typeof(PlayerObjectPhysics) },
			{ 17, typeof(JoiningPlayer) },
			{ 18, typeof(StarBaseNet) },
			{ 19, typeof(PlayerItems) },
			{ 20, typeof(PlayerShipNet) },
			{ 21, typeof(ShipsTypeNet) },
			{ 22, typeof(WeaponsTypeNet) },
			{ 23, typeof(GeneratorNet) },
			{ 24, typeof(ExtrasNet) },
			{ 25, typeof(BoostersNet) },
			{ 26, typeof(PlayerBelongings) },
			{ 27, typeof(JoinMapData) },
			{ 28, typeof(LevelMap) },
			{ 29, typeof(BasicSkillsColNet) },
			{ 30, typeof(BasicSkillNet) },
			{ 31, typeof(ProSkillsColNet) },
			{ 32, typeof(ProSkillNet) },
			{ 33, typeof(MasterSkillNet) },
			{ 34, typeof(GenericData) },
			{ 35, typeof(NeighbourhoodUpdate) },
			{ 36, typeof(FusilladeData) },
			{ 37, typeof(ShipInfo) },
			{ 38, typeof(StartMiningMessage) },
			{ 39, typeof(StopMiningMessage) },
			{ 40, typeof(DamagesUpdate) },
			{ 41, typeof(RemoveShipData) },
			{ 42, typeof(StartMoveShipData) },
			{ 43, typeof(StartShootingData) },
			{ 44, typeof(StopShootingData) },
			{ 45, typeof(ProjectileObject) },
			{ 46, typeof(NavigationMapItem) },
			{ 47, typeof(NavigationMap) },
			{ 48, typeof(BulletObject) },
			{ 49, typeof(LaserWeldingObject) },
			{ 50, typeof(Victor3) },
			{ 51, typeof(LaserMovingObject) },
			{ 52, typeof(RocketObject) },
			{ 53, typeof(ImAliveMessage) },
			{ 54, typeof(PlayerData) },
			{ 55, typeof(StopMiningData) },
			{ 56, typeof(MakeSynthesisParams) },
			{ 57, typeof(MadeSynthesisParams) },
			{ 58, typeof(EnterBaseParams) },
			{ 59, typeof(ExitBaseParams) },
			{ 60, typeof(ChangeGalaxyParams) },
			{ 61, typeof(ResurrectPlayerData) },
			{ 62, typeof(HyperJumpParams) },
			{ 63, typeof(ResourceForTrade) },
			{ 64, typeof(ItemToShipParams) },
			{ 65, typeof(ItemToInventoryParams) },
			{ 66, typeof(MiningStation) },
			{ 67, typeof(FinishQuestParams) },
			{ 68, typeof(CreatePlayerParams) },
			{ 69, typeof(SelectThisShipParams) },
			{ 70, typeof(BuyShipParams) },
			{ 71, typeof(BuyWeaponParams) },
			{ 72, typeof(LoadWeaponParams) },
			{ 73, typeof(BuyGeneratorParams) },
			{ 74, typeof(HyperJumpNet) },
			{ 75, typeof(Mineral) },
			{ 76, typeof(SlotItem) },
			{ 77, typeof(PvEGroup) },
			{ 78, typeof(PvEPhysics) },
			{ 79, typeof(SlotItemWeapon) },
			{ 80, typeof(BuyResultNet) },
			{ 81, typeof(SellOrder) },
			{ 82, typeof(BuyItemParams) },
			{ 83, typeof(RenameShipParams) },
			{ 84, typeof(RepairParams) },
			{ 85, typeof(SkillsInfo) },
			{ 86, typeof(ResearchSkillParam) },
			{ 87, typeof(BuyAmmoParams) },
			{ 88, typeof(StaticData) },
			{ 89, typeof(LevelsInfo) },
			{ 90, typeof(Guild) },
			{ 91, typeof(ValidationErrors) },
			{ 92, typeof(ExtractionPointInfo) },
			{ 93, typeof(ExtractionPoint) },
			{ 94, typeof(UniversalTransportContainer) },
			{ 95, typeof(InitialRequest) },
			{ 96, typeof(TalentsInfo) },
			{ 98, typeof(SlotPriceInfo) },
			{ 99, typeof(Location) },
			{ 100, typeof(ActiveSkillParams) },
			{ 101, typeof(ActiveSkillObject) },
			{ 102, typeof(MoveSlotItemData) },
			{ 103, typeof(WeaponAmmoTypeChange) },
			{ 104, typeof(ActiveSkillSlot) },
			{ 105, typeof(LearnedActiveSkillData) },
			{ 106, typeof(GalaxiesJumpMap) },
			{ 107, typeof(GalaxyJumpParam) },
			{ 108, typeof(PlayerItemTypesData) },
			{ 109, typeof(WeaponUpgradesNet) },
			{ 110, typeof(PartyClientSide) },
			{ 111, typeof(PartyMemberClientSide) },
			{ 112, typeof(PartyInvite) },
			{ 113, typeof(ResourceTradeData) },
			{ 114, typeof(UpgradeWeaponParams) },
			{ 115, typeof(NpcObjectPhysics) },
			{ 116, typeof(ShortAndBool) },
			{ 119, typeof(ActiveSkillBarConfig) },
			{ 120, typeof(RankingData) },
			{ 121, typeof(RegisterData) },
			{ 122, typeof(ChatMessage) },
			{ 123, typeof(RegisterResult) },
			{ 124, typeof(CollisionMapInfo) },
			{ 125, typeof(PvPGameType) },
			{ 126, typeof(PvPGame) },
			{ 127, typeof(InitialPack) },
			{ 128, typeof(PlayerProfile) },
			{ 129, typeof(PlayerRelations) },
			{ 130, typeof(StackSlotItemData) },
			{ 131, typeof(Portal) },
			{ 132, typeof(PortalPart) },
			{ 133, typeof(PlayerObjectives) },
			{ 134, typeof(CheckpointObjectPhysics) },
			{ 135, typeof(NewQuest) },
			{ 136, typeof(AuthorizeHashRequest) },
			{ 137, typeof(DefenceTurret) },
			{ 138, typeof(SignUpData) },
			{ 139, typeof(InternalXSComunicationClass) }
		};
	}

	public TransferablesFramework()
	{
	}

	public static ITransferable DeserializeITransferable(Stream s)
	{
		return TransferablesFramework.DeserializeITransferable(new BinaryReader(s));
	}

	public static ITransferable DeserializeITransferable(BinaryReader br)
	{
		return TransferablesFramework.Finger1(br, (TransferContext)br.ReadInt16());
	}

	private static ITransferable Finger1(BinaryReader br, TransferContext context)
	{
		ITransferable transferable;
		byte num = br.ReadByte();
		if (num != 0)
		{
			ConstructorInfo[] constructors = TransferablesFramework.types[num].GetConstructors();
			for (int i = 0; i < (int)constructors.Length; i++)
			{
				ConstructorInfo constructorInfo = constructors[i];
				if ((constructorInfo.IsStatic ? false : !constructorInfo.IsPrivate))
				{
					if ((int)constructorInfo.GetParameters().Length <= 0)
					{
						ITransferable transferable1 = null;
						transferable1 = (ITransferable)constructorInfo.Invoke(new object[0]);
						if (context == TransferContext.None)
						{
							transferable1.Deserialize(br);
						}
						else
						{
							((ITransferableInContext)transferable1).DeserializeInContext(br, context);
						}
						transferable = transferable1;
						return transferable;
					}
				}
			}
			throw new Exception("No default public non-static constructor was found!");
		}
		else
		{
			transferable = null;
		}
		return transferable;
	}

	private static byte GetTypeCode(ITransferable obj)
	{
		byte num;
		if (obj == null)
		{
			num = 0;
		}
		else if (obj is ActiveSkillObject)
		{
			num = 101;
		}
		else if (obj is AuthorizeRequest)
		{
			num = 1;
		}
		else if (obj is AuthorizeResult)
		{
			num = 2;
		}
		else if (obj is AddPlr2UniverseRequest)
		{
			num = 3;
		}
		else if (obj is AddPlr2UniverseResponse)
		{
			num = 4;
		}
		else if (obj is WeaponSlot)
		{
			num = 5;
		}
		else if (obj is AmmoType)
		{
			num = 6;
		}
		else if (obj is ShootingPlayer)
		{
			num = 7;
		}
		else if (obj is QuestObjective)
		{
			num = 8;
		}
		else if (obj is RecrutedPlayers)
		{
			num = 9;
		}
		else if (obj is PlayerQuest)
		{
			num = 10;
		}
		else if (obj is ShipConfiguration)
		{
			num = 11;
		}
		else if (obj is PvEInfo)
		{
			num = 13;
		}
		else if (obj is AmmoNet)
		{
			num = 14;
		}
		else if (obj is WeaponInfo)
		{
			num = 15;
		}
		else if (obj is PvEPhysics)
		{
			num = 78;
		}
		else if (obj is PlayerObjectPhysics)
		{
			num = 16;
		}
		else if (obj is NpcObjectPhysics)
		{
			num = 115;
		}
		else if (obj is JoiningPlayer)
		{
			num = 17;
		}
		else if (obj is StarBaseNet)
		{
			num = 18;
		}
		else if (obj is PlayerItems)
		{
			num = 19;
		}
		else if (obj is PlayerShipNet)
		{
			num = 20;
		}
		else if (obj is ShipsTypeNet)
		{
			num = 21;
		}
		else if (obj is WeaponsTypeNet)
		{
			num = 22;
		}
		else if (obj is GeneratorNet)
		{
			num = 23;
		}
		else if (obj is ExtrasNet)
		{
			num = 24;
		}
		else if (obj is BoostersNet)
		{
			num = 25;
		}
		else if (obj is PlayerBelongings)
		{
			num = 26;
		}
		else if (obj is JoinMapData)
		{
			num = 27;
		}
		else if (obj is LevelMap)
		{
			num = 28;
		}
		else if (obj is BasicSkillsColNet)
		{
			num = 29;
		}
		else if (obj is BasicSkillNet)
		{
			num = 30;
		}
		else if (obj is ProSkillsColNet)
		{
			num = 31;
		}
		else if (obj is ProSkillNet)
		{
			num = 32;
		}
		else if (obj is MasterSkillNet)
		{
			num = 33;
		}
		else if (obj is GenericData)
		{
			num = 34;
		}
		else if (obj is NeighbourhoodUpdate)
		{
			num = 35;
		}
		else if (obj is FusilladeData)
		{
			num = 36;
		}
		else if (obj is ShipInfo)
		{
			num = 37;
		}
		else if (obj is StartMiningMessage)
		{
			num = 38;
		}
		else if (obj is StopMiningMessage)
		{
			num = 39;
		}
		else if (obj is DamagesUpdate)
		{
			num = 40;
		}
		else if (obj is RemoveShipData)
		{
			num = 41;
		}
		else if (obj is StartMoveShipData)
		{
			num = 42;
		}
		else if (obj is StartShootingData)
		{
			num = 43;
		}
		else if (obj is StopShootingData)
		{
			num = 44;
		}
		else if (obj is NavigationMapItem)
		{
			num = 46;
		}
		else if (obj is NavigationMap)
		{
			num = 47;
		}
		else if (obj is RocketObject)
		{
			num = 52;
		}
		else if (obj is LaserMovingObject)
		{
			num = 51;
		}
		else if (obj is LaserWeldingObject)
		{
			num = 49;
		}
		else if (obj is BulletObject)
		{
			num = 48;
		}
		else if (obj is ProjectileObject)
		{
			num = 45;
		}
		else if (obj is ImAliveMessage)
		{
			num = 53;
		}
		else if (obj is PlayerData)
		{
			num = 54;
		}
		else if (obj is StopMiningData)
		{
			num = 55;
		}
		else if (obj is MakeSynthesisParams)
		{
			num = 56;
		}
		else if (obj is MadeSynthesisParams)
		{
			num = 57;
		}
		else if (obj is EnterBaseParams)
		{
			num = 58;
		}
		else if (obj is ExitBaseParams)
		{
			num = 59;
		}
		else if (obj is ChangeGalaxyParams)
		{
			num = 60;
		}
		else if (obj is ResurrectPlayerData)
		{
			num = 61;
		}
		else if (obj is HyperJumpParams)
		{
			num = 62;
		}
		else if (obj is ResourceForTrade)
		{
			num = 63;
		}
		else if (obj is ItemToShipParams)
		{
			num = 64;
		}
		else if (obj is ItemToInventoryParams)
		{
			num = 65;
		}
		else if (obj is FinishQuestParams)
		{
			num = 67;
		}
		else if (obj is CreatePlayerParams)
		{
			num = 68;
		}
		else if (obj is SelectThisShipParams)
		{
			num = 69;
		}
		else if (obj is BuyShipParams)
		{
			num = 70;
		}
		else if (obj is BuyWeaponParams)
		{
			num = 71;
		}
		else if (obj is LoadWeaponParams)
		{
			num = 72;
		}
		else if (obj is BuyGeneratorParams)
		{
			num = 73;
		}
		else if (obj is HyperJumpNet)
		{
			num = 74;
		}
		else if (obj is Mineral)
		{
			num = 75;
		}
		else if (obj is SlotItemWeapon)
		{
			num = 79;
		}
		else if (obj is PvEGroup)
		{
			num = 77;
		}
		else if (obj is SlotItem)
		{
			num = 76;
		}
		else if (obj is BuyResultNet)
		{
			num = 80;
		}
		else if (obj is SellOrder)
		{
			num = 81;
		}
		else if (obj is BuyItemParams)
		{
			num = 82;
		}
		else if (obj is ExtractionPoint)
		{
			num = 93;
		}
		else if (obj is CheckpointObjectPhysics)
		{
			num = 134;
		}
		else if (obj is MiningStation)
		{
			num = 66;
		}
		else if (obj is DefenceTurret)
		{
			num = 137;
		}
		else if (obj is GameObjectPhysics)
		{
			num = 12;
		}
		else if (obj is RenameShipParams)
		{
			num = 83;
		}
		else if (obj is RepairParams)
		{
			num = 84;
		}
		else if (obj is SkillsInfo)
		{
			num = 85;
		}
		else if (obj is ResearchSkillParam)
		{
			num = 86;
		}
		else if (obj is BuyAmmoParams)
		{
			num = 87;
		}
		else if (obj is StaticData)
		{
			num = 88;
		}
		else if (obj is LevelsInfo)
		{
			num = 89;
		}
		else if (obj is Guild)
		{
			num = 90;
		}
		else if (obj is ValidationErrors)
		{
			num = 91;
		}
		else if (obj is ExtractionPointInfo)
		{
			num = 92;
		}
		else if (obj is UniversalTransportContainer)
		{
			num = 94;
		}
		else if (obj is InitialRequest)
		{
			num = 95;
		}
		else if (obj is TalentsInfo)
		{
			num = 96;
		}
		else if (obj is SlotPriceInfo)
		{
			num = 98;
		}
		else if (obj is Location)
		{
			num = 99;
		}
		else if (obj is ActiveSkillParams)
		{
			num = 100;
		}
		else if (obj is MoveSlotItemData)
		{
			num = 102;
		}
		else if (obj is WeaponAmmoTypeChange)
		{
			num = 103;
		}
		else if (obj is ActiveSkillSlot)
		{
			num = 104;
		}
		else if (obj is LearnedActiveSkillData)
		{
			num = 105;
		}
		else if (obj is GalaxiesJumpMap)
		{
			num = 106;
		}
		else if (obj is GalaxyJumpParam)
		{
			num = 107;
		}
		else if (obj is PlayerItemTypesData)
		{
			num = 108;
		}
		else if (obj is WeaponUpgradesNet)
		{
			num = 109;
		}
		else if (obj is PartyClientSide)
		{
			num = 110;
		}
		else if (obj is PartyMemberClientSide)
		{
			num = 111;
		}
		else if (obj is PartyInvite)
		{
			num = 112;
		}
		else if (obj is ResourceTradeData)
		{
			num = 113;
		}
		else if (obj is UpgradeWeaponParams)
		{
			num = 114;
		}
		else if (obj is ShortAndBool)
		{
			num = 116;
		}
		else if (obj is ActiveSkillBarConfig)
		{
			num = 119;
		}
		else if (obj is RankingData)
		{
			num = 120;
		}
		else if (obj is RegisterData)
		{
			num = 121;
		}
		else if (obj is ChatMessage)
		{
			num = 122;
		}
		else if (obj is RegisterResult)
		{
			num = 123;
		}
		else if (obj is CollisionMapInfo)
		{
			num = 124;
		}
		else if (obj is PvPGameType)
		{
			num = 125;
		}
		else if (obj is PvPGame)
		{
			num = 126;
		}
		else if (obj is InitialPack)
		{
			num = 127;
		}
		else if (obj is PlayerProfile)
		{
			num = 128;
		}
		else if (obj is PlayerRelations)
		{
			num = 129;
		}
		else if (obj is StackSlotItemData)
		{
			num = 130;
		}
		else if (obj is Portal)
		{
			num = 131;
		}
		else if (obj is PortalPart)
		{
			num = 132;
		}
		else if (obj is PlayerObjectives)
		{
			num = 133;
		}
		else if (obj is NewQuest)
		{
			num = 135;
		}
		else if (obj is AuthorizeHashRequest)
		{
			num = 136;
		}
		else if (!(obj is SignUpData))
		{
			num = (byte)((!(obj is InternalXSComunicationClass) ? 255 : 139));
		}
		else
		{
			num = 138;
		}
		return num;
	}

	public static void SerializeITransferable(Stream s, ITransferable obj, TransferContext context)
	{
		TransferablesFramework.SerializeITransferable(new BinaryWriter(s), obj, context);
	}

	public static void SerializeITransferable(BinaryWriter bw, ITransferable obj, TransferContext context)
	{
		bw.Write((short)context);
		if (obj == null)
		{
			bw.Write((byte)0);
		}
		else if (context != TransferContext.None)
		{
			bw.Write(TransferablesFramework.GetTypeCode(obj));
			((ITransferableInContext)obj).SerializeInContext(bw, context);
		}
		else
		{
			bw.Write(TransferablesFramework.GetTypeCode(obj));
			obj.Serialize(bw);
		}
	}
}