using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

public class Guild : ITransferableInContext, ITransferable
{
	public ushort createCurrency;

	public SortedList<int, GuildRank> ranks = new SortedList<int, GuildRank>();

	public int id;

	public string name = "";

	public string avatarUrl = "";

	public string title = "";

	public string description = "";

	public SortedList<int, PlayerBasic> members = new SortedList<int, PlayerBasic>();

	public SortedList<int, GuildInvitation> invitations = new SortedList<int, GuildInvitation>();

	public DateTime createdOn;

	public long bankUltralibrium;

	public long bankEquilib;

	public long bankNova;

	public List<GuildLogRecord> log = new List<GuildLogRecord>();

	public string language = "bg";

	public byte upgradeOneLevel;

	public byte upgradeTwoLevel;

	public byte upgradeThreeLevel;

	public byte upgradeFourLevel;

	public byte upgradeFiveLevel;

	public byte fractionId;

	public List<SlotItem> guildItems = new List<SlotItem>();

	public byte vaultSlots;

	public List<int> unlockedPortals = new List<int>();

	public static SortedList<int, GuildLevel> guildUpgrades;

	public int _mastersCount;

	public int InvitationsCapacity
	{
		get
		{
			int num = (
				from t in StaticData.guildUpgradesInfo
				where (t.upgradeType != 1 ? false : t.upgradeLevel == this.upgradeOneLevel)
				select t).First<GuildUpgrade>().effectValue - this.invitations.Count;
			return num;
		}
	}

	public byte Level
	{
		get
		{
			byte num = (byte)(this.upgradeOneLevel + this.upgradeTwoLevel + this.upgradeThreeLevel + this.upgradeFourLevel + this.upgradeFiveLevel);
			return num;
		}
	}

	public GuildRank LowestRank
	{
		get
		{
			GuildRank guildRank;
			if ((this.ranks == null ? false : this.ranks.Count != 0))
			{
				guildRank = (
					from o in this.ranks.Values
					orderby o.RankIndex
					select o).First<GuildRank>();
			}
			else
			{
				guildRank = null;
			}
			return guildRank;
		}
	}

	public int MastersCount
	{
		get
		{
			IEnumerable<GuildRank> values = 
				from w in this.ranks.Values
				where w.isMaster
				select w;
			IEnumerable<PlayerBasic> playerBasics = 
				from w in this.members.Values
				where values.Contains<GuildRank>(w.guildMembership.rank)
				select w;
			return playerBasics.Count<PlayerBasic>();
		}
	}

	static Guild()
	{
		Guild.guildUpgrades = new SortedList<int, GuildLevel>();
		SortedList<int, GuildLevel> nums = Guild.guildUpgrades;
		GuildLevel guildLevel = new GuildLevel()
		{
			level = 1,
			membersCapacity = 5,
			priceEquilibrium = 1000,
			priceNova = 1000,
			ranks = 4
		};
		nums.Add(1, guildLevel);
		SortedList<int, GuildLevel> nums1 = Guild.guildUpgrades;
		GuildLevel guildLevel1 = new GuildLevel()
		{
			level = 2,
			membersCapacity = 10,
			priceEquilibrium = -1,
			priceNova = 2000,
			ranks = 5
		};
		nums1.Add(2, guildLevel1);
		SortedList<int, GuildLevel> nums2 = Guild.guildUpgrades;
		GuildLevel guildLevel2 = new GuildLevel()
		{
			level = 3,
			membersCapacity = 20,
			priceEquilibrium = -1,
			priceNova = 4000,
			ranks = 6
		};
		nums2.Add(3, guildLevel2);
		SortedList<int, GuildLevel> nums3 = Guild.guildUpgrades;
		GuildLevel guildLevel3 = new GuildLevel()
		{
			level = 4,
			membersCapacity = 30,
			priceEquilibrium = -1,
			priceNova = 8000,
			ranks = 7
		};
		nums3.Add(4, guildLevel3);
		SortedList<int, GuildLevel> nums4 = Guild.guildUpgrades;
		GuildLevel guildLevel4 = new GuildLevel()
		{
			level = 5,
			membersCapacity = 40,
			priceEquilibrium = -1,
			priceNova = 15000,
			ranks = 8
		};
		nums4.Add(5, guildLevel4);
		SortedList<int, GuildLevel> nums5 = Guild.guildUpgrades;
		GuildLevel guildLevel5 = new GuildLevel()
		{
			level = 6,
			membersCapacity = 50,
			priceEquilibrium = -1,
			priceNova = 30000,
			ranks = 9
		};
		nums5.Add(6, guildLevel5);
		SortedList<int, GuildLevel> nums6 = Guild.guildUpgrades;
		GuildLevel guildLevel6 = new GuildLevel()
		{
			level = 7,
			membersCapacity = 60,
			priceEquilibrium = -1,
			priceNova = 55000,
			ranks = 10
		};
		nums6.Add(7, guildLevel6);
		SortedList<int, GuildLevel> nums7 = Guild.guildUpgrades;
		GuildLevel guildLevel7 = new GuildLevel()
		{
			level = 8,
			membersCapacity = 70,
			priceEquilibrium = -1,
			priceNova = 105000,
			ranks = 11
		};
		nums7.Add(8, guildLevel7);
		SortedList<int, GuildLevel> nums8 = Guild.guildUpgrades;
		GuildLevel guildLevel8 = new GuildLevel()
		{
			level = 9,
			membersCapacity = 85,
			priceEquilibrium = -1,
			priceNova = 200000,
			ranks = 13
		};
		nums8.Add(9, guildLevel8);
		SortedList<int, GuildLevel> nums9 = Guild.guildUpgrades;
		GuildLevel guildLevel9 = new GuildLevel()
		{
			level = 10,
			membersCapacity = 100,
			priceEquilibrium = -1,
			priceNova = 350000,
			ranks = 15
		};
		nums9.Add(10, guildLevel9);
	}

	public Guild()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		GameObjectPhysics.Log("UNSUPPORTED DESERIALIZE!");
		throw new NotImplementedException();
	}

	public void DeserializeInContext(BinaryReader br, TransferContext context)
	{
		int i;
		GuildInvitation guildInvitation;
		int num;
		switch (context)
		{
			case TransferContext.GuildOverviewExternalRequest:
			{
				this.id = br.ReadInt32();
				break;
			}
			case TransferContext.GuildOverviewExternalResponse:
			{
				this.DeserializeOverview(br);
				this.DeserializeRanks(br);
				this.DeserializeMembers(br);
				break;
			}
			case TransferContext.GuildOverviewInternal:
			case TransferContext.GuildUpdateWhole:
			{
				this.DeserializeOverview(br);
				break;
			}
			case TransferContext.GuildOverviewNone:
			{
				this.id = 0;
				break;
			}
			case TransferContext.GuildInvitationsList:
			{
				int num1 = br.ReadInt32();
				if (num1 != -1)
				{
					this.invitations = new SortedList<int, GuildInvitation>(num1);
					for (i = 0; i < num1; i++)
					{
						try
						{
							guildInvitation = new GuildInvitation()
							{
								id = br.ReadInt32(),
								guild = new Guild(),
								invitee = new PlayerBasic()
								{
									userName = br.ReadString(),
									nickName = br.ReadString(),
									avatarUrl = br.ReadString(),
									level = br.ReadByte(),
									isOnline = br.ReadBoolean(),
									fractionId = br.ReadByte()
								}
							};
							num = br.ReadInt32();
							guildInvitation.timeExpire = StaticData.now.AddSeconds((double)num);
							this.invitations.Add(guildInvitation.id, guildInvitation);
						}
						catch (Exception exception)
						{
							GameObjectPhysics.Log(exception.ToString());
						}
					}
				}
				else
				{
					this.invitations = null;
				}
				break;
			}
			case TransferContext.GuildInvitationsListToNonMember:
			{
				int num2 = br.ReadInt32();
				if (num2 != -1)
				{
					this.invitations = new SortedList<int, GuildInvitation>(num2);
					for (i = 0; i < num2; i++)
					{
						guildInvitation = new GuildInvitation()
						{
							id = br.ReadInt32(),
							guild = new Guild()
							{
								name = br.ReadString(),
								avatarUrl = br.ReadString(),
								fractionId = br.ReadByte(),
								upgradeOneLevel = br.ReadByte(),
								upgradeTwoLevel = br.ReadByte(),
								upgradeThreeLevel = br.ReadByte(),
								upgradeFourLevel = br.ReadByte(),
								upgradeFiveLevel = br.ReadByte(),
								id = br.ReadInt32()
							}
						};
						num = br.ReadInt32();
						guildInvitation.timeExpire = StaticData.now.AddSeconds((double)num);
						this.invitations.Add(guildInvitation.id, guildInvitation);
					}
				}
				else
				{
					this.invitations = null;
				}
				break;
			}
			case TransferContext.GuildHistory:
			case TransferContext.GuildErrorsCreate:
			case TransferContext.GuildErrorsSave:
			case TransferContext.GuildErrorCreateAlreadyMember:
			case TransferContext.GuildErrorAcceptInvite:
			case TransferContext.GuildErrorRemoveMember:
			case TransferContext.GuildError:
			case TransferContext.GuildErrorDelete:
			case TransferContext.GuildErrorsEditDetails:
			case TransferContext.GuildErrorsDeposit:
			case TransferContext.GuildLeave:
			case TransferContext.GuildInviteError:
			case TransferContext.EpContributors:
			{
				GameObjectPhysics.Log(string.Concat("UNSUPPORTED! ", context.ToString()));
				throw new NotImplementedException();
			}
			case TransferContext.GuildMembers:
			{
				this.id = br.ReadInt32();
				this.DeserializeRanks(br);
				this.DeserializeMembers(br);
				break;
			}
			case TransferContext.GuildVault:
			{
				this.DesezializeGuildItems(br);
				break;
			}
			case TransferContext.GuildRanks:
			{
				this.id = br.ReadInt32();
				this.DeserializeRanks(br);
				this.DeserializeMembers(br);
				break;
			}
			case TransferContext.GuildCreateTry:
			{
				this.name = br.ReadString();
				this.title = br.ReadString();
				this.language = br.ReadString();
				this.description = br.ReadString();
				this.createCurrency = br.ReadUInt16();
				break;
			}
			case TransferContext.GuildSaveTry:
			{
				this.name = br.ReadString();
				this.title = br.ReadString();
				this.language = br.ReadString();
				this.description = br.ReadString();
				this.id = br.ReadInt32();
				break;
			}
			case TransferContext.GuildBankUpdate:
			{
				this.id = br.ReadInt32();
				this.bankNova = br.ReadInt64();
				this.bankEquilib = br.ReadInt64();
				this.bankUltralibrium = br.ReadInt64();
				break;
			}
			case TransferContext.GuildHistoryUpdate:
			{
				this.log.Clear();
				int num3 = br.ReadInt32();
				for (i = 0; i < num3; i++)
				{
					GuildLogRecord guildLogRecord = new GuildLogRecord()
					{
						eventType = (GuildEvent)br.ReadByte(),
						eventTime = DateTime.FromBinary(br.ReadInt64()),
						playerName = br.ReadString(),
						otherPlayerName = br.ReadString(),
						otherPlayerId = br.ReadInt32(),
						currencyType = (SelectedCurrency)br.ReadByte(),
						quantity = br.ReadInt32()
					};
					this.log.Add(guildLogRecord);
				}
				break;
			}
			case TransferContext.GuildCommonUpdate:
			{
				this.name = br.ReadString();
				this.avatarUrl = br.ReadString();
				int num4 = br.ReadInt32();
				this._mastersCount = br.ReadInt32();
				this.members = new SortedList<int, PlayerBasic>();
				for (i = 0; i < num4; i++)
				{
					this.members.Add(i, new PlayerBasic());
				}
				this.createdOn = DateTime.FromBinary(br.ReadInt64());
				this.id = br.ReadInt32();
				this.upgradeOneLevel = br.ReadByte();
				this.upgradeTwoLevel = br.ReadByte();
				this.upgradeThreeLevel = br.ReadByte();
				this.upgradeFourLevel = br.ReadByte();
				this.upgradeFiveLevel = br.ReadByte();
				break;
			}
			case TransferContext.GuildRemoveMember:
			{
				this.members = new SortedList<int, PlayerBasic>();
				PlayerBasic playerBasic = new PlayerBasic()
				{
					nickName = br.ReadString()
				};
				this.members.Add(0, playerBasic);
				break;
			}
			case TransferContext.GuildUpgrade:
			{
				this.upgradeOneLevel = br.ReadByte();
				this.upgradeTwoLevel = br.ReadByte();
				this.upgradeThreeLevel = br.ReadByte();
				this.upgradeFourLevel = br.ReadByte();
				this.upgradeFiveLevel = br.ReadByte();
				break;
			}
			case TransferContext.GuildInvite:
			{
				this.invitations = new SortedList<int, GuildInvitation>();
				SortedList<int, GuildInvitation> nums = this.invitations;
				GuildInvitation guildInvitation1 = new GuildInvitation();
				PlayerBasic playerBasic1 = new PlayerBasic()
				{
					userName = br.ReadString()
				};
				guildInvitation1.invitee = playerBasic1;
				nums.Add(0, guildInvitation1);
				break;
			}
			case TransferContext.GuildInvited:
			{
				this.invitations = new SortedList<int, GuildInvitation>();
				int num5 = br.ReadInt32();
				for (i = 0; i < num5; i++)
				{
					this.invitations.Add(i, new GuildInvitation());
				}
				break;
			}
			case TransferContext.GuildInviteRemove:
			{
				this.invitations = new SortedList<int, GuildInvitation>();
				SortedList<int, GuildInvitation> nums1 = this.invitations;
				GuildInvitation guildInvitation2 = new GuildInvitation()
				{
					id = br.ReadInt32()
				};
				nums1.Add(0, guildInvitation2);
				break;
			}
			case TransferContext.GuildInviteReject:
			case TransferContext.GuildInviteAccept:
			{
				GuildInvitation guildInvitation3 = new GuildInvitation()
				{
					id = br.ReadInt32(),
					guild = this
				};
				GuildInvitation guildInvitation4 = guildInvitation3;
				this.id = br.ReadInt32();
				this.invitations = new SortedList<int, GuildInvitation>()
				{
					{ guildInvitation4.id, guildInvitation4 }
				};
				break;
			}
			case TransferContext.GuildCurrentUserRank:
			{
				this.ranks = new SortedList<int, GuildRank>();
				GuildRank guildRank = new GuildRank();
				this.DeserializeRank(br, guildRank);
				this.ranks.Add(guildRank.id, guildRank);
				break;
			}
			case TransferContext.GuildRankChange:
			{
				this.members = new SortedList<int, PlayerBasic>();
				PlayerBasic playerBasic2 = new PlayerBasic()
				{
					userName = br.ReadString(),
					guildMembership = new GuildMember()
					{
						rank = new GuildRank()
						{
							id = br.ReadInt32()
						}
					}
				};
				this.members.Add(0, playerBasic2);
				break;
			}
			case TransferContext.GuildRankAdd:
			{
				this.id = br.ReadInt32();
				this.ranks = new SortedList<int, GuildRank>();
				GuildRank guildRank1 = new GuildRank();
				this.DeserializeRank(br, guildRank1);
				this.ranks = new SortedList<int, GuildRank>()
				{
					{ guildRank1.id, guildRank1 }
				};
				break;
			}
			case TransferContext.GuildRankDelete:
			{
				this.id = br.ReadInt32();
				this.ranks = new SortedList<int, GuildRank>();
				GuildRank guildRank2 = new GuildRank();
				this.DeserializeRank(br, guildRank2);
				this.ranks = new SortedList<int, GuildRank>()
				{
					{ guildRank2.id, guildRank2 }
				};
				break;
			}
			case TransferContext.GuildRankUpdate:
			{
				this.id = br.ReadInt32();
				this.ranks = new SortedList<int, GuildRank>();
				GuildRank guildRank3 = new GuildRank();
				this.DeserializeRank(br, guildRank3);
				this.ranks = new SortedList<int, GuildRank>()
				{
					{ guildRank3.id, guildRank3 }
				};
				break;
			}
			case TransferContext.GuildEpInfoRequest:
			{
				break;
			}
			case TransferContext.GuildEpInfoResponse:
			{
				this.id = br.ReadInt32();
				if (this.id == 0)
				{
					this.ranks = new SortedList<int, GuildRank>();
				}
				else
				{
					this.bankNova = br.ReadInt64();
					this.bankEquilib = br.ReadInt64();
					this.title = br.ReadString();
					this.name = br.ReadString();
					GuildRank guildRank4 = new GuildRank();
					this.DeserializeRank(br, guildRank4);
					this.ranks = new SortedList<int, GuildRank>()
					{
						{ guildRank4.id, guildRank4 }
					};
				}
				break;
			}
			default:
			{
				GameObjectPhysics.Log(string.Concat("UNSUPPORTED! ", context.ToString()));
				throw new NotImplementedException();
			}
		}
	}

	private void DeserializeMembers(BinaryReader br)
	{
		int num = br.ReadInt32();
		if (num != -1)
		{
			this.members = new SortedList<int, PlayerBasic>(num);
			for (int i = 0; i < num; i++)
			{
				PlayerBasic playerBasic = new PlayerBasic()
				{
					dbId = i,
					userName = br.ReadString(),
					nickName = br.ReadString()
				};
				PlayerBasic guildMember = playerBasic;
				guildMember.guildMembership = new GuildMember();
				int num1 = br.ReadInt32();
				guildMember.guildMembership.rank = this.ranks[num1];
				long num2 = br.ReadInt64();
				guildMember.guildMembership.memberSince = DateTime.FromBinary(num2);
				guildMember.isOnline = br.ReadBoolean();
				guildMember.isInParty = br.ReadBoolean();
				guildMember.level = br.ReadByte();
				guildMember.fractionId = br.ReadByte();
				this.members.Add(guildMember.dbId, guildMember);
			}
		}
		else
		{
			this.members = null;
		}
	}

	private void DeserializeOverview(BinaryReader br)
	{
		int i;
		this.fractionId = br.ReadByte();
		this.name = br.ReadString();
		this.title = br.ReadString();
		this.language = br.ReadString();
		this.description = br.ReadString();
		this.avatarUrl = br.ReadString();
		int num = br.ReadInt32();
		this.members = new SortedList<int, PlayerBasic>();
		for (i = 0; i < num; i++)
		{
			this.members.Add(i, new PlayerBasic());
		}
		this.bankUltralibrium = br.ReadInt64();
		this.bankEquilib = br.ReadInt64();
		this.bankNova = br.ReadInt64();
		this.createdOn = DateTime.FromBinary(br.ReadInt64());
		this.id = br.ReadInt32();
		this.upgradeOneLevel = br.ReadByte();
		this.upgradeTwoLevel = br.ReadByte();
		this.upgradeThreeLevel = br.ReadByte();
		this.upgradeFourLevel = br.ReadByte();
		this.upgradeFiveLevel = br.ReadByte();
		this._mastersCount = br.ReadInt32();
		int num1 = br.ReadInt32();
		this.unlockedPortals = new List<int>();
		for (i = 0; i < num1; i++)
		{
			this.unlockedPortals.Add(br.ReadInt32());
		}
	}

	private void DeserializeRank(BinaryReader br, GuildRank rank)
	{
		rank.id = br.ReadInt32();
		rank.name = br.ReadString();
		rank.isMaster = br.ReadBoolean();
		rank.sortIndex = br.ReadInt16();
		rank.avatarIndex = br.ReadInt16();
		rank.canBank = br.ReadBoolean();
		rank.canEditDetails = br.ReadBoolean();
		rank.canInvite = br.ReadBoolean();
		rank.canPromote = br.ReadBoolean();
		rank.canVault = br.ReadBoolean();
		rank.canChat = br.ReadBoolean();
	}

	private void DeserializeRanks(BinaryReader br)
	{
		int num = br.ReadInt32();
		if (num != -1)
		{
			this.ranks = new SortedList<int, GuildRank>(num);
			for (int i = 0; i < num; i++)
			{
				GuildRank guildRank = new GuildRank();
				this.DeserializeRank(br, guildRank);
				this.ranks.Add(guildRank.id, guildRank);
			}
		}
		else
		{
			this.ranks = null;
		}
		this._mastersCount = br.ReadInt32();
	}

	private void DesezializeGuildItems(BinaryReader br)
	{
		int num = br.ReadInt32();
		this.guildItems = new List<SlotItem>();
		for (int i = 0; i < num; i++)
		{
			SlotItem slotItem = new SlotItem();
			slotItem = (SlotItem)TransferablesFramework.DeserializeITransferable(br);
			this.guildItems.Add(slotItem);
		}
		this.vaultSlots = br.ReadByte();
	}

	public void Serialize(BinaryWriter bw)
	{
		GameObjectPhysics.Log("UNSUPPORTED Serialize!");
		throw new NotImplementedException();
	}

	private void SerializeGuildItems(BinaryWriter bw)
	{
		bw.Write(this.guildItems.Count);
		for (int i = 0; i < this.guildItems.Count; i++)
		{
			TransferablesFramework.SerializeITransferable(bw, this.guildItems[i], TransferContext.None);
		}
		bw.Write(this.vaultSlots);
	}

	public void SerializeInContext(BinaryWriter bw, TransferContext context)
	{
		GuildInvitation value = null;
		TimeSpan timeSpan;
		switch (context)
		{
			case TransferContext.GuildOverviewExternalRequest:
			{
				bw.Write(this.id);
				break;
			}
			case TransferContext.GuildOverviewExternalResponse:
			{
				this.SerializeOverview(bw);
				this.SerializeRanks(bw);
				this.SerializeMembers(bw);
				break;
			}
			case TransferContext.GuildOverviewInternal:
			case TransferContext.GuildUpdateWhole:
			{
				this.SerializeOverview(bw);
				break;
			}
			case TransferContext.GuildOverviewNone:
			{
				break;
			}
			case TransferContext.GuildInvitationsList:
			{
				if (this.invitations != null)
				{
					bw.Write(this.invitations.Count);
					foreach (GuildInvitation val in this.invitations.Values)
					{
						bw.Write(val.id);
						bw.Write(val.invitee.userName);
						bw.Write(val.invitee.nickName);
						bw.Write(val.invitee.avatarUrl);
						bw.Write(val.invitee.level);
						bw.Write(val.invitee.isOnline);
						bw.Write(val.invitee.fractionId);
						timeSpan = value.timeExpire - StaticData.now;
						bw.Write((int)timeSpan.TotalSeconds);
					}
				}
				else
				{
					bw.Write(-1);
				}
				break;
			}
			case TransferContext.GuildInvitationsListToNonMember:
			{
				if (this.invitations != null)
				{
					bw.Write(this.invitations.Count);
					foreach (GuildInvitation guildInvitation in this.invitations.Values)
					{
						bw.Write(guildInvitation.id);
						bw.Write(guildInvitation.guild.name);
						bw.Write(guildInvitation.guild.avatarUrl);
						bw.Write(guildInvitation.guild.members.Values[0].fractionId);
						bw.Write(guildInvitation.guild.upgradeOneLevel);
						bw.Write(guildInvitation.guild.upgradeTwoLevel);
						bw.Write(guildInvitation.guild.upgradeThreeLevel);
						bw.Write(guildInvitation.guild.upgradeFourLevel);
						bw.Write(guildInvitation.guild.upgradeFiveLevel);
						bw.Write(guildInvitation.guild.id);
						timeSpan = guildInvitation.timeExpire - StaticData.now;
						bw.Write((int)timeSpan.TotalSeconds);
					}
				}
				else
				{
					bw.Write(-1);
				}
				break;
			}
			case TransferContext.GuildHistory:
			case TransferContext.GuildErrorsCreate:
			case TransferContext.GuildErrorsSave:
			case TransferContext.GuildErrorCreateAlreadyMember:
			case TransferContext.GuildErrorAcceptInvite:
			case TransferContext.GuildErrorRemoveMember:
			case TransferContext.GuildError:
			case TransferContext.GuildErrorDelete:
			case TransferContext.GuildErrorsEditDetails:
			case TransferContext.GuildErrorsDeposit:
			case TransferContext.GuildLeave:
			case TransferContext.GuildInviteError:
			case TransferContext.EpContributors:
			{
				throw new NotImplementedException();
			}
			case TransferContext.GuildMembers:
			{
				bw.Write(this.id);
				this.SerializeRanks(bw);
				this.SerializeMembers(bw);
				break;
			}
			case TransferContext.GuildVault:
			{
				this.SerializeGuildItems(bw);
				break;
			}
			case TransferContext.GuildRanks:
			{
				bw.Write(this.id);
				this.SerializeRanks(bw);
				this.SerializeMembers(bw);
				break;
			}
			case TransferContext.GuildCreateTry:
			{
				bw.Write(this.name);
				bw.Write(this.title);
				bw.Write(this.language);
				bw.Write(this.description);
				bw.Write(this.createCurrency);
				break;
			}
			case TransferContext.GuildSaveTry:
			{
				bw.Write(this.name);
				bw.Write(this.title);
				bw.Write(this.language);
				bw.Write(this.description);
				bw.Write(this.id);
				break;
			}
			case TransferContext.GuildBankUpdate:
			{
				bw.Write(this.id);
				bw.Write(this.bankNova);
				bw.Write(this.bankEquilib);
				bw.Write(this.bankUltralibrium);
				break;
			}
			case TransferContext.GuildHistoryUpdate:
			{
				bw.Write(this.log.Count);
				foreach (GuildLogRecord guildLogRecord in this.log)
				{
					bw.Write((byte)guildLogRecord.eventType);
					bw.Write(guildLogRecord.eventTime.Ticks);
					bw.Write(guildLogRecord.playerName ?? string.Empty);
					bw.Write(guildLogRecord.otherPlayerName ?? string.Empty);
					bw.Write(guildLogRecord.otherPlayerId);
					bw.Write((byte)guildLogRecord.currencyType);
					bw.Write(guildLogRecord.quantity);
				}
				break;
			}
			case TransferContext.GuildCommonUpdate:
			{
				bw.Write(this.name);
				bw.Write(this.avatarUrl);
				bw.Write(this.members.Count);
				bw.Write(this.MastersCount);
				bw.Write(this.createdOn.Ticks);
				bw.Write(this.id);
				bw.Write(this.upgradeOneLevel);
				bw.Write(this.upgradeTwoLevel);
				bw.Write(this.upgradeThreeLevel);
				bw.Write(this.upgradeFourLevel);
				bw.Write(this.upgradeFiveLevel);
				break;
			}
			case TransferContext.GuildRemoveMember:
			{
				bw.Write(this.members.Values[0].nickName);
				break;
			}
			case TransferContext.GuildUpgrade:
			{
				bw.Write(this.upgradeOneLevel);
				bw.Write(this.upgradeTwoLevel);
				bw.Write(this.upgradeThreeLevel);
				bw.Write(this.upgradeFourLevel);
				bw.Write(this.upgradeFiveLevel);
				break;
			}
			case TransferContext.GuildInvite:
			{
				bw.Write(this.invitations.Values[0].invitee.userName);
				break;
			}
			case TransferContext.GuildInvited:
			{
				bw.Write(this.invitations.Count);
				break;
			}
			case TransferContext.GuildInviteRemove:
			{
				try
				{
					bw.Write(this.invitations.Values[0].id);
				}
				catch (Exception exception)
				{
					GameObjectPhysics.Log(string.Concat(":", exception.ToString()));
				}
				break;
			}
			case TransferContext.GuildInviteReject:
			case TransferContext.GuildInviteAccept:
			{
				bw.Write(this.invitations.Values[0].id);
				bw.Write(this.id);
				break;
			}
			case TransferContext.GuildCurrentUserRank:
			{
				this.SerializeRank(bw, this.ranks.Values[0]);
				break;
			}
			case TransferContext.GuildRankChange:
			{
				try
				{
					bw.Write(this.members.Values[0].userName);
					bw.Write(this.members.Values[0].guildMembership.rank.id);
				}
				catch (Exception exception1)
				{
					GameObjectPhysics.Log(exception1.ToString());
				}
				break;
			}
			case TransferContext.GuildRankAdd:
			{
				bw.Write(this.id);
				this.SerializeRank(bw, this.ranks.Values[0]);
				break;
			}
			case TransferContext.GuildRankDelete:
			{
				bw.Write(this.id);
				this.SerializeRank(bw, this.ranks.Values[0]);
				break;
			}
			case TransferContext.GuildRankUpdate:
			{
				bw.Write(this.id);
				this.SerializeRank(bw, this.ranks.Values[0]);
				break;
			}
			case TransferContext.GuildEpInfoRequest:
			{
				break;
			}
			case TransferContext.GuildEpInfoResponse:
			{
				bw.Write(this.id);
				if (this.id != 0)
				{
					bw.Write(this.bankNova);
					bw.Write(this.bankEquilib);
					bw.Write(this.title);
					bw.Write(this.name);
					this.SerializeRank(bw, this.ranks.Values[0]);
				}
				break;
			}
			default:
			{
				throw new NotImplementedException();
			}
		}
	}

	private void SerializeMembers(BinaryWriter bw)
	{
		if (this.members != null)
		{
			bw.Write(this.members.Count);
			foreach (PlayerBasic value in this.members.Values)
			{
				bw.Write(value.userName);
				bw.Write(value.nickName);
				bw.Write(value.guildMembership.rank.id);
				bw.Write(value.guildMembership.memberSince.Ticks);
				bw.Write(value.isOnline);
				bw.Write(value.isInParty);
				bw.Write(value.level);
				bw.Write(value.fractionId);
			}
		}
		else
		{
			bw.Write(-1);
		}
	}

	private void SerializeOverview(BinaryWriter bw)
	{
		bw.Write(this.fractionId);
		bw.Write(this.name);
		bw.Write(this.title);
		bw.Write(this.language);
		bw.Write(this.description);
		bw.Write(this.avatarUrl);
		bw.Write(this.members.Count);
		bw.Write(this.bankUltralibrium);
		bw.Write(this.bankEquilib);
		bw.Write(this.bankNova);
		bw.Write(this.createdOn.Ticks);
		bw.Write(this.id);
		bw.Write(this.upgradeOneLevel);
		bw.Write(this.upgradeTwoLevel);
		bw.Write(this.upgradeThreeLevel);
		bw.Write(this.upgradeFourLevel);
		bw.Write(this.upgradeFiveLevel);
		bw.Write(this.MastersCount);
		bw.Write(this.unlockedPortals.Count);
		for (int i = 0; i < this.unlockedPortals.Count; i++)
		{
			bw.Write(this.unlockedPortals[i]);
		}
	}

	private void SerializeRank(BinaryWriter bw, GuildRank rank)
	{
		bw.Write(rank.id);
		bw.Write(rank.name);
		bw.Write(rank.isMaster);
		bw.Write(rank.sortIndex);
		bw.Write(rank.avatarIndex);
		bw.Write(rank.canBank);
		bw.Write(rank.canEditDetails);
		bw.Write(rank.canInvite);
		bw.Write(rank.canPromote);
		bw.Write(rank.canVault);
		bw.Write(rank.canChat);
	}

	private void SerializeRanks(BinaryWriter bw)
	{
		if (this.ranks != null)
		{
			bw.Write(this.ranks.Count);
			foreach (GuildRank value in this.ranks.Values)
			{
				this.SerializeRank(bw, value);
			}
		}
		else
		{
			bw.Write(-1);
		}
		bw.Write(this.MastersCount);
	}
}