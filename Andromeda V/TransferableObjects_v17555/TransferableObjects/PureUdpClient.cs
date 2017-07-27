using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TransferableObjects
{
	public class PureUdpClient
	{
		public const byte CommandStartPlay = 3;

		public const byte CommandMovePlayerShip = 4;

		public const byte CommandStartMining = 12;

		public const byte CommandStopMining = 13;

		public const byte CommandChooseFraction = 14;

		public const byte CommandDespawnPVE = 15;

		public const byte CommandUpdateItems = 17;

		public const byte CommandMakeSynthesis = 18;

		public const byte CommandMadeSynthesis = 19;

		public const byte CommandReloadCfgOnPlay = 20;

		public const byte CommandEnterBase = 21;

		public const byte CommandExitBase = 22;

		public const byte CommandChangeGalaxy = 23;

		public const byte CommandResurrect = 24;

		public const byte CommandHyperJump = 25;

		public const byte CommandRenameShip = 26;

		public const byte CommandRepairShip = 27;

		public const byte CommandReadStaticData = 28;

		public const byte CommandMoveSlotItem = 29;

		public const byte CommandUpdatePlrConfig = 30;

		public const byte CommandTrashSlotItem = 31;

		public const byte CommandSellSlotItem = 32;

		public const byte CommandUpdateNeighbourhood = 33;

		public const byte CommandFusillade = 34;

		public const byte CommandUseGambler = 35;

		public const byte CommandDamagesUpdate = 36;

		public const byte CommandTurnWeaponOnOff = 37;

		public const byte CommandGoOffline = 38;

		public const byte CommandInitHyperJump = 39;

		public const byte CommandRetrainTalents = 40;

		public const byte CommandStealthExit = 41;

		public const byte CommandMoveSkill = 42;

		public const byte CommandGalaxyJump = 43;

		public const byte CommandSellResources = 44;

		public const byte CommandPlayerLevelingUp = 45;

		public const byte CommandUpdateMineralOwner = 46;

		public const byte CommandUpdateVisitedNPCs = 47;

		public const byte CommandCollectQuestReward = 48;

		public const byte CommandUpdateSelectedPoP = 49;

		public const byte CommandUpadateVolume = 50;

		public const byte CommandStunPlayer = 51;

		public const byte CommandUseItemReroll = 52;

		public const byte CommandUpgradeGuild = 53;

		public const byte CommandSocialInteraction = 54;

		public const byte CommandQuestEngine = 55;

		public const byte CommandUnlockPortal = 56;

		public const byte CommandUseTransformer = 57;

		public const byte CommandNewQuestRecordUpdate = 96;

		public const byte CommandSetAutofire = 97;

		public const byte CommandSetZoomLevel = 98;

		public const byte CommandUpgradeWeapon = 99;

		public const byte CommandServerMessage = 100;

		public const byte CommandSelectThisShip = 102;

		public const byte CommandBuyShip = 103;

		public const byte CommandUpgradeYourShip = 105;

		public const byte CommandReferalUpdate = 106;

		public const byte CommandPlayerMultiKill = 111;

		public const byte CommandReBuildConfiguration = 125;

		public const byte CommandActiveSkillCast = 128;

		public const byte CommandSetWeaponAmmoType = 129;

		public const byte CommandLearnedTalent = 130;

		public const byte CommandPartyInvite = 131;

		public const byte CommandPartyAcceptInvitation = 132;

		public const byte CommandPartyRejectInvitation = 133;

		public const byte CommandPartyPromote = 134;

		public const byte CommandPartyKick = 135;

		public const byte CommandPartyUpdate = 136;

		public const byte CommandPartyRemoveInvitee = 137;

		public const byte CommandPartyRemoveInviter = 138;

		public const byte CommandPartyAddInvitee = 139;

		public const byte CommandPartyAddInviter = 140;

		public const byte CommandPartyCancelInvitation = 141;

		public const byte CommandPartyChangeRule = 142;

		public const byte CommandRankingData = 143;

		public const byte CommandSubscribe = 144;

		public const byte CommandPartyEnterExit = 145;

		public const byte CommandChatMessage = 146;

		public const byte CommandChatBlock = 147;

		public const byte CommandChatDoNotDisturb = 148;

		public const byte CommandChatReport = 149;

		public const byte CommandFinishTutorial = 150;

		public const byte CommandTutorialAction = 151;

		public const byte CommandChatStart = 153;

		public const byte CommandPvPSignUp = 154;

		public const byte CommandPvPSignOut = 155;

		public const byte CommandPvPStartCountdown = 156;

		public const byte CommandPvPRetreat = 157;

		public const byte CommandPvPLeave = 158;

		public const byte CommandPvPStatsUpdate = 159;

		public const byte CommandPvPGameOver = 160;

		public const byte CommandPvPGameDispose = 161;

		public const byte CommandVersionCheck = 162;

		public const byte CommandReadStarterData = 163;

		public const byte CommandReadStarterData2 = 164;

		public const byte CommandUnsubscribe = 165;

		public const byte CommandUnsubscribePlayer = 166;

		public const byte CommandFriendPlayer = 167;

		public const byte CommandUnfriendPlayer = 168;

		public const byte CommandBlacklistPlayer = 169;

		public const byte CommandUnblacklistPlayer = 170;

		public const byte CommandGetProfile = 171;

		public const byte CommandGetFriends = 172;

		public const byte CommandGetBlackList = 173;

		public const byte CommandAchievementUnlocked = 174;

		public const byte CommandGuildsList = 175;

		public const byte CommandGuildInsufficientMasters = 176;

		public const byte CommandGuildRequestInvitationsNonMember = 177;

		public const byte CommandGuildInvitationsNonMember = 178;

		public const byte CommandGuild = 179;

		public const byte CommandGuildUpdate = 180;

		public const byte CommandGuildCreated = 181;

		public const byte CommandGuildDeposit = 182;

		public const byte CommandExtarctionPointUpdate = 183;

		public const byte CommandExtractionPointContributors = 184;

		public const byte CommandUpgradeExtractionPoint = 185;

		public const byte CommandCreateExtractionPointUnit = 186;

		public const byte CommandFractionOverview = 187;

		public const byte CommandGameMapOverview = 188;

		public const byte CommandGetGuildsRanking = 189;

		public const byte CommandStackSlotItem = 190;

		public const byte CommandUpdatePlayerRegistration = 191;

		public const byte CommandPvPDominationGameUpdate = 193;

		public const byte CommandPvPLeagueRankingData = 194;

		public const byte CommandChangePlayerFaction = 195;

		public const byte CommandSendGift = 196;

		public const byte CommandSetCollisionsMap = 254;

		public PlayerData mySecretGun = new PlayerData();

		public string serverIp;

		public int serverPort;

		public static int NO_SERVER_TIMEOUT;

		public static int NO_SERVER_RETRY_TIME;

		public static int TIMEOUT_SEND;

		public static int RETRIES_ON_UDP;

		public static int PING_SECONDS;

		private long id;

		private TcpClient server = null;

		public long sequence = (long)0;

		public static byte CommandAuthorizePlayerRequest;

		public static byte CommandAuthorizePlayerResponse;

		public static byte CommandDbDataRequestSync;

		public static byte CommandDestroyPlayer;

		public static byte CommandIamAlive;

		public static byte CommandSpawnPlayer;

		public static byte CommandRemovePlayer;

		public static byte CommandShoot;

		public static byte CommandStopShoot;

		public static byte CommandNavMap;

		public static byte CommandSpawnMineral;

		public static byte CommandItemToInventory;

		public static byte CommandExchangeNovoForCash;

		public static byte CommandUpdateResources;

		public static byte CommandCollectResource;

		public static byte CommandExpandSlots;

		public static byte CommandRequestQuestInfo;

		public static byte CommandSkipQuest;

		public static byte CommandBuyItem;

		public static byte CommandLearnTalent;

		public static byte CommandUpgradeTalentNeurons;

		public static byte CommandIamOnBackground;

		public Action<Exception, PlayerData> onError;

		private NetworkStream ns;

		public Action<int, int> UdpateReadProgress;

		private Queue<UdpCommHeader> sendQueue = new Queue<UdpCommHeader>();

		private bool isRunning = false;

		public bool flagConnectCallback = false;

		private object sendLocker = new object();

		public Action OnConnectionEstablished;

		public Action forcePlayerOffGameOnNoServer;

		public Action ServerOrderedExit;

		static PureUdpClient()
		{
			PureUdpClient.NO_SERVER_TIMEOUT = 30;
			PureUdpClient.NO_SERVER_RETRY_TIME = 100;
			PureUdpClient.TIMEOUT_SEND = 15000;
			PureUdpClient.RETRIES_ON_UDP = 5;
			PureUdpClient.PING_SECONDS = 5;
			PureUdpClient.CommandAuthorizePlayerRequest = 0;
			PureUdpClient.CommandAuthorizePlayerResponse = 1;
			PureUdpClient.CommandDbDataRequestSync = 2;
			PureUdpClient.CommandDestroyPlayer = 5;
			PureUdpClient.CommandIamAlive = 6;
			PureUdpClient.CommandSpawnPlayer = 7;
			PureUdpClient.CommandRemovePlayer = 8;
			PureUdpClient.CommandShoot = 9;
			PureUdpClient.CommandStopShoot = 10;
			PureUdpClient.CommandNavMap = 11;
			PureUdpClient.CommandSpawnMineral = 16;
			PureUdpClient.CommandItemToInventory = 101;
			PureUdpClient.CommandExchangeNovoForCash = 104;
			PureUdpClient.CommandUpdateResources = 107;
			PureUdpClient.CommandCollectResource = 108;
			PureUdpClient.CommandExpandSlots = 109;
			PureUdpClient.CommandRequestQuestInfo = 110;
			PureUdpClient.CommandSkipQuest = 112;
			PureUdpClient.CommandBuyItem = 113;
			PureUdpClient.CommandLearnTalent = 126;
			PureUdpClient.CommandUpgradeTalentNeurons = 127;
			PureUdpClient.CommandIamOnBackground = 192;
		}

		public PureUdpClient(string serverIp, int serverPort, long playerId)
		{
			this.serverIp = serverIp;
			this.serverPort = serverPort;
			this.id = playerId;
		}

		public void ExecuteCommand(byte cmd, ITransferable p)
		{
			this.ExecuteCommand(cmd, p, TransferContext.None);
		}

		public void ExecuteCommand(byte cmd, ITransferable p, TransferContext context)
		{
			if (this.isRunning)
			{
				UdpCommHeader udpCommHeader = new UdpCommHeader()
				{
					context = context,
					packetSeq = Interlocked.Increment(ref this.sequence),
					requestType = cmd,
					data = p,
					playerId = this.id
				};
				lock (this.sendQueue)
				{
					this.sendQueue.Enqueue(udpCommHeader);
				}
			}
		}

		public void LoadStaticData()
		{
			this.ExecuteCommand(28, null);
		}

		private byte[] ReadFromStream(NetworkStream ns, int len)
		{
			byte[] numArray = new byte[len];
			int num = 0;
			while (num < len)
			{
				if (!this.isRunning)
				{
					throw new Exception("kurec");
				}
				if (ns.DataAvailable)
				{
					num += ns.Read(numArray, num, len - num);
					if ((len <= 4 || this.UdpateReadProgress == null ? false : numArray[0] == 28))
					{
						this.UdpateReadProgress(num, len);
					}
				}
				else
				{
					Thread.Sleep(10);
				}
			}
			return numArray;
		}

		public UdpCommHeader ReceiveAsyncMessage()
		{
			if (this.flagConnectCallback)
			{
				this.flagConnectCallback = false;
				this.OnConnectionEstablished();
			}
			if (this.mySecretGun.flagError)
			{
				this.mySecretGun.flagError = false;
				this.mySecretGun.onIoError(this.mySecretGun.onErrorParamException, this.mySecretGun);
			}
			UdpCommHeader item = null;
			lock (this.mySecretGun.receivedCommands)
			{
				if (this.mySecretGun.receivedCommands.Count > 0)
				{
					item = this.mySecretGun.receivedCommands[0];
					this.mySecretGun.receivedCommands.RemoveAt(0);
				}
			}
			return item;
		}

		private void RunReceiver()
		{
			try
			{
				try
				{
					this.server = new TcpClient()
					{
						NoDelay = true,
						SendTimeout = 6500,
						ReceiveTimeout = 0
					};
					this.server.Connect(IPAddress.Parse(this.serverIp), this.serverPort);
					this.server.SendTimeout = 6500;
					this.server.ReceiveTimeout = 0;
					this.ns = this.server.GetStream();
					(new Thread(new ThreadStart(this.RunSender))).Start();
					this.flagConnectCallback = true;
					while (this.isRunning)
					{
						byte[] numArray = this.ReadFromStream(this.ns, 4);
						int num = BitConverter.ToInt32(numArray, 0);
						byte[] numArray1 = this.ReadFromStream(this.ns, num);
						UdpCommHeader udpCommHeader = UdpCommHeader.FromBytes(numArray1, num);
						lock (this.mySecretGun.receivedCommands)
						{
							this.mySecretGun.receivedCommands.Add(udpCommHeader);
						}
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					if (this.isRunning)
					{
						this.onError(exception, this.mySecretGun);
					}
				}
			}
			finally
			{
				try
				{
					this.server.Close();
				}
				catch (Exception exception2)
				{
				}
			}
		}

		public void RunSender()
		{
			try
			{
				while (this.isRunning)
				{
					UdpCommHeader[] array = null;
					lock (this.sendQueue)
					{
						array = this.sendQueue.ToArray();
						this.sendQueue.Clear();
					}
					if ((int)array.Length <= 0)
					{
						Thread.Sleep(7);
					}
					else
					{
						UdpCommHeader[] udpCommHeaderArray = array;
						for (int i = 0; i < (int)udpCommHeaderArray.Length; i++)
						{
							UdpCommHeader udpCommHeader = udpCommHeaderArray[i];
							int bytes = udpCommHeader.ToBytes(this.mySecretGun.bufferOut, 0) ;
							this.ns.Write(this.mySecretGun.bufferOut, 0, bytes);
						}
					}
				}
			}
			catch (Exception exception2)
			{
				Exception exception = exception2;
				if (this.isRunning)
				{
					try
					{
						this.onError(exception, this.mySecretGun);
					}
					catch (Exception exception1)
					{
					}
				}
			}
			try
			{
				this.server.Close();
			}
			catch (Exception exception3)
			{
			}
		}

		public void StartReceiveUdp(Action<Exception, PlayerData> onError)
		{
            this.onError = onError;
            this.mySecretGun = new PlayerData()
            {
                bufferIn = new byte[2048],
                bufferOut = new byte[2048],
                onIoError = onError
            };
            new Thread(new ThreadStart(this.RunReceiver)).Start();
            this.isRunning = true;
            //thread.Start();
        }

		public void StopReceiveUdp()
		{
			this.onError = null;
			this.isRunning = false;
			try
			{
				this.server.Client.Close(0);
			}
			catch (Exception exception)
			{
			}
		}
	}
}