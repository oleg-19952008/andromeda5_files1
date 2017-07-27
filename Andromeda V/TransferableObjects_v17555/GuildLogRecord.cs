using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

public class GuildLogRecord
{
	public string playerName;

	public string otherPlayerName;

	public int otherPlayerId;

	public GuildEvent eventType;

	public DateTime eventTime;

	public int quantity;

	public SelectedCurrency currencyType = SelectedCurrency.None;

	private string Currency
	{
		get
		{
			string empty;
			switch (this.currencyType)
			{
				case SelectedCurrency.Cash:
				{
					empty = StaticData.Translate(StaticData.allTypes[PlayerItems.TypeCash].uiName);
					break;
				}
				case SelectedCurrency.Nova:
				{
					empty = StaticData.Translate(StaticData.allTypes[PlayerItems.TypeNova].uiName);
					break;
				}
				case SelectedCurrency.Equilibrium:
				{
					empty = StaticData.Translate(StaticData.allTypes[PlayerItems.TypeEquilibrium].uiName);
					break;
				}
				case SelectedCurrency.Ultralibrium:
				{
					empty = StaticData.Translate(StaticData.allTypes[PlayerItems.TypeUltralibrium].uiName);
					break;
				}
				default:
				{
					empty = string.Empty;
					break;
				}
			}
			return empty;
		}
	}

	private string GuildUpgrade
	{
		get
		{
			return StaticData.Translate(this.otherPlayerName);
		}
	}

	public string Log
	{
		get
		{
			string str;
			object[] guildUpgrade;
			switch (this.eventType)
			{
				case GuildEvent.Create:
				{
					str = string.Format(StaticData.Translate("key_guild_log_create"), this.playerName);
					break;
				}
				case GuildEvent.Edit:
				{
					str = string.Format(StaticData.Translate("key_guild_log_edit"), this.playerName);
					break;
				}
				case GuildEvent.Invite:
				{
					str = string.Format(StaticData.Translate("key_guild_log_invite"), this.playerName, this.otherPlayerName);
					break;
				}
				case GuildEvent.RemoveInvite:
				{
					str = string.Format(StaticData.Translate("key_guild_log_remove_invite"), this.playerName, this.otherPlayerName);
					break;
				}
				case GuildEvent.RejectInvite:
				{
					str = string.Format(StaticData.Translate("key_guild_log_reject"), this.playerName);
					break;
				}
				case GuildEvent.AcceptInvite:
				{
					str = string.Format(StaticData.Translate("key_guild_log_accept"), this.playerName);
					break;
				}
				case GuildEvent.Kick:
				{
					str = string.Format(StaticData.Translate("key_guild_log_kick"), this.playerName, this.otherPlayerName);
					break;
				}
				case GuildEvent.Leave:
				{
					str = string.Format(StaticData.Translate("key_guild_log_leave"), this.playerName);
					break;
				}
				case GuildEvent.EditRank:
				{
					str = string.Format(StaticData.Translate("key_guild_log_edit_rank"), this.playerName);
					break;
				}
				case GuildEvent.EditMember:
				{
					str = string.Format(StaticData.Translate("key_guild_log_edit_member"), this.playerName, this.otherPlayerName);
					break;
				}
				case GuildEvent.Deposit:
				{
					str = string.Format(StaticData.Translate("key_guild_log_deposit"), this.playerName, this.quantity, this.Currency);
					break;
				}
				case GuildEvent.Upgrade:
				{
					string str1 = StaticData.Translate("key_guild_log_upgrade");
					guildUpgrade = new object[] { this.playerName, this.GuildUpgrade, this.otherPlayerId, this.quantity, this.Currency };
					str = string.Format(str1, guildUpgrade);
					break;
				}
				case GuildEvent.UnlockPortal:
				{
					string str2 = StaticData.Translate("key_guild_log_unlock_portal");
					guildUpgrade = new object[] { this.playerName, this.Portal, this.quantity, this.Currency };
					str = string.Format(str2, guildUpgrade);
					break;
				}
				case GuildEvent.SpendTransformer:
				{
					str = string.Format(StaticData.Translate("key_guild_log_spend_transformer"), this.playerName, this.quantity, this.Currency);
					break;
				}
				case GuildEvent.SpendExtractionPoint:
				{
					string str3 = StaticData.Translate("key_guild_log_spend_extraction_point");
					guildUpgrade = new object[] { this.playerName, this.quantity, this.Currency, null };
					guildUpgrade[3] = StaticData.Translate((
						from t in (IEnumerable<ExtractionPointInfo>)StaticData.allExtractionPoints
						where t.id == this.otherPlayerId
						select t).First<ExtractionPointInfo>().name);
					str = string.Format(str3, guildUpgrade);
					break;
				}
				case GuildEvent.ExpandVault:
				{
					string str4 = StaticData.Translate("key_guild_log_expand_vault");
					guildUpgrade = new object[] { this.playerName, this.otherPlayerId, this.quantity, this.Currency };
					str = string.Format(str4, guildUpgrade);
					break;
				}
				case GuildEvent.ExportVault:
				{
					str = string.Format(StaticData.Translate("key_guild_log_export_vault"), this.playerName);
					break;
				}
				case GuildEvent.ImportVault:
				{
					str = string.Format(StaticData.Translate("key_guild_log_import_vault"), this.playerName);
					break;
				}
				case GuildEvent.IncomeExtractionPoin:
				{
					str = string.Format(StaticData.Translate("key_guild_log_income_extraction_point"), StaticData.Translate((
						from t in (IEnumerable<ExtractionPointInfo>)StaticData.allExtractionPoints
						where t.id == this.otherPlayerId
						select t).First<ExtractionPointInfo>().name), this.quantity, this.Currency);
					break;
				}
				default:
				{
					str = "Not implemented guild event";
					break;
				}
			}
			return str;
		}
	}

	private string Portal
	{
		get
		{
			string str = StaticData.Translate((
				from s in StaticData.allGalaxies
				where s.galaxyKey == (
					from t in StaticData.allPortals
					where t.portalId == this.otherPlayerId
					select t).First<Portal>().galaxyKey
				select s).First<LevelMap>().nameUI);
			return str;
		}
	}

	public GuildLogRecord()
	{
	}
}