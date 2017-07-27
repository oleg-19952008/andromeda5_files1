using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using TransferableObjects;

public class PlayerData : ITransferable
{
	public const int PARTY_INVITE_EXPIRE_TIME = 45;

	public const int MAX_PARTY_INVITEES = 3;

	public const int MAX_PARTY_INVITERS = 4;

	public const int MAX_PARTY_MEMBERS = 4;

	public Guild guild;

	public GuildMember guildMember;

	public SortedList<byte, byte> factionGalaxyOwnership;

	public DateTime nextWarStartTime;

	public bool isWarInProgress;

	public byte factionOneAttackGalaxyKey;

	public byte factionTwoAttackGalaxyKey;

	public object tcpSenderLocker;

	public long playId;

	public int dbId;

	public short galaxyId;

	public string language;

	public int lastSeenGameMessageId;

	public int lastSeenPrivateMessageId;

	public DateTime lastEpInvestTime;

	public DateTime requestedAt;

	private TcpClient tcpClient;

	public PvPGame pvpGame;

	public short pvpGameTypeSignedFor = 0;

	public List<UdpCommHeader> receivedCommands = new List<UdpCommHeader>(10);

	public byte[] bufferOut;

	public byte[] bufferIn;

	public int lengthIn;

	public int lengthInCurrent;

	public long outEndIndex;

	public int outStartIndex;

	public int lengthOutCurrent;

	public Action<Exception, PlayerData> onIoError;

	public bool isChatAdmin = false;

	public bool isChatDND = false;

	public bool flagError = false;

	public Exception onErrorParamException = null;

	public static object globalCountersLocker;

	private bool isSenderWorking;

	public ServerState state = ServerState.OnMap;

	public PlayerObjectPhysics vessel;

	public ShipConfiguration cfg;

	[NonSerialized]
	public DateTime lastPingTime;

	[NonSerialized]
	public PlayerBelongings playerBelongings;

	public Dictionary<int, DateTime> blacklistedPartyInvitePlayers = new Dictionary<int, DateTime>();

	public object gameMap;

	public object newGameMap;

	public Victor3 newGameMapPosition;

	public int newInstanceId;

	public ushort newListenPort;

	public bool isInBase;

	public bool Connected
	{
		get
		{
			return (this.tcpClient == null ? false : this.tcpClient.Connected);
		}
	}

	static PlayerData()
	{
		PlayerData.globalCountersLocker = new object();
	}

	public PlayerData()
	{
	}

	private void AddReceivedCommand(UdpCommHeader h)
	{
		lock (this.receivedCommands)
		{
			try
			{
				this.receivedCommands.Add(h);
			}
			catch (Exception exception)
			{
				Console.WriteLine("{0:HH:mm:ss ffff} {1}", StaticData.now, exception);
			}
		}
	}

	public void AsyncReceive(IAsyncResult ar)
	{
		TcpClient asyncState = (TcpClient)ar.AsyncState;
		if (asyncState.Client.Connected)
		{
			try
			{
				int num = asyncState.Client.EndReceive(ar);
				if (num != 0)
				{
					PlayerData playerDatum = this;
					playerDatum.lengthInCurrent = playerDatum.lengthInCurrent + num;
					if (this.lengthInCurrent >= this.lengthIn)
					{
						this.ReceivedAsyncMessage();
						this.lengthInCurrent = 0;
						this.lengthIn = 4;
						asyncState.Client.BeginReceive(this.bufferIn, 0, 4, SocketFlags.None, new AsyncCallback(this.AsyncReceiveSize), asyncState);
					}
					else
					{
						asyncState.Client.BeginReceive(this.bufferIn, this.lengthInCurrent, this.lengthIn - this.lengthInCurrent, SocketFlags.None, new AsyncCallback(this.AsyncReceive), asyncState);
					}
				}
				else
				{
					return;
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				if (this.CheckActiveSocket(ar))
				{
					this.ManageError(exception);
				}
			}
		}
	}

	public void AsyncReceiveSize(IAsyncResult ar)
	{
		TcpClient asyncState = (TcpClient)ar.AsyncState;
		if (asyncState.Client.Connected)
		{
			try
			{
				int num = asyncState.Client.EndReceive(ar);
				if (num != 0)
				{
					PlayerData playerDatum = this;
					playerDatum.lengthInCurrent = playerDatum.lengthInCurrent + num;
					if (this.lengthInCurrent >= this.lengthIn)
					{
						this.lengthIn = BitConverter.ToInt32(this.bufferIn, 0);
						this.lengthInCurrent = 0;
						asyncState.Client.BeginReceive(this.bufferIn, 0, this.lengthIn, SocketFlags.None, new AsyncCallback(this.AsyncReceive), asyncState);
					}
					else
					{
						asyncState.Client.BeginReceive(this.bufferIn, this.lengthInCurrent, this.lengthIn - this.lengthInCurrent, SocketFlags.None, new AsyncCallback(this.AsyncReceiveSize), asyncState);
						return;
					}
				}
				else
				{
					return;
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				if (this.CheckActiveSocket(ar))
				{
					this.ManageError(exception);
				}
			}
		}
	}

	private bool CheckActiveSocket(IAsyncResult ar)
	{
		bool flag;
		flag = (((TcpClient)ar.AsyncState).Client == this.tcpClient.Client ? true : false);
		return flag;
	}

	public void CloseNetIfNotNull()
	{
		if (this.tcpClient != null)
		{
			this.tcpClient.Close();
		}
	}

	public void CloseNetUnconditioned()
	{
		this.tcpClient.Close();
	}

	public void Deserialize(BinaryReader br)
	{
		this.state = (ServerState)br.ReadByte();
		this.playId = br.ReadInt64();
		this.language = br.ReadString();
		this.isChatAdmin = br.ReadBoolean();
		this.isChatDND = br.ReadBoolean();
		this.vessel.Deserialize(br);
		this.cfg.Deserialize(br);
		if (this.vessel.pvpState == PvPPlayerState.None)
		{
			this.pvpGame = null;
		}
		else
		{
			this.pvpGame = new PvPGame();
			this.pvpGame.Deserialize(br);
		}
	}

	private void ManageError(Exception ex)
	{
		this.flagError = true;
		this.onErrorParamException = ex;
		GameObjectPhysics.Log(string.Format("{0:HH:mm:ss ffff} {1}", StaticData.now, ex));
	}

	public void ReceivedAsyncMessage()
	{
		this.AddReceivedCommand(UdpCommHeader.FromBytes(this.bufferIn, this.lengthIn));
	}

	public void ReplaceNet(TcpClient client)
	{
		this.StopNet();
		lock (this.bufferOut)
		{
			this.isSenderWorking = false;
			this.outEndIndex = (long)0;
			this.outStartIndex = 0;
		}
		this.tcpClient = client;
		this.lengthIn = 4;
		this.lengthInCurrent = 0;
		this.outEndIndex = (long)0;
		this.outStartIndex = 0;
		this.lengthOutCurrent = 0;
		try
		{
			this.tcpClient.Client.BeginReceive(this.bufferIn, 0, 4, SocketFlags.None, new AsyncCallback(this.AsyncReceiveSize), this.tcpClient);
		}
		catch (Exception exception)
		{
			this.ManageError(exception);
		}
	}

	private void SendCallback(IAsyncResult ar)
	{
		try
		{
			int num = this.tcpClient.Client.EndSend(ar);
			try
			{
				if ((long)(this.outStartIndex + num) != Interlocked.Read(ref this.outEndIndex))
				{
					if ((long)(this.outStartIndex + num) > this.outEndIndex)
					{
						Console.WriteLine("{2:HH:mm:ss ffff} WROOOONG! Sending another package at index {0} to index {1}.", this.outStartIndex, this.outEndIndex, StaticData.now);
						throw new Exception("OOO");
					}
					PlayerData playerDatum = this;
					playerDatum.outStartIndex = playerDatum.outStartIndex + num;
					this.tcpClient.Client.BeginSend(this.bufferOut, this.outStartIndex, (int)this.outEndIndex - this.outStartIndex, SocketFlags.None, new AsyncCallback(this.SendCallback), this.tcpClient.Client);
				}
				else
				{
					GameObjectPhysics.Log("Sent out.");
					this.isSenderWorking = false;
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				GameObjectPhysics.Log("Exx!");
				Console.WriteLine("{0:HH:mm:ss ffff} MAYKA TI KOSMATAAA! {1}", StaticData.now, exception);
			}
		}
		catch (Exception exception4)
		{
			Exception exception2 = exception4;
			GameObjectPhysics.Log("Exxx!");
			try
			{
				this.tcpClient.Close();
			}
			catch (Exception exception3)
			{
				if (this.CheckActiveSocket(ar))
				{
					this.ManageError(exception2);
				}
			}
			GameObjectPhysics.Log(string.Format("{0:HH:mm:ss ffff} {1}", StaticData.now, exception2));
		}
	}

	public void SendMessage(UdpCommHeader h)
	{
		Exception exception;
		if (this.tcpClient != null)
		{
			if (this.tcpClient.Connected)
			{
				lock (this.bufferOut)
				{
					if (!this.isSenderWorking)
					{
						this.outStartIndex = 0;
						this.outEndIndex = (long)0;
					}
					int bytes = h.ToBytes(this.bufferOut, (int)this.outEndIndex) + 4;
					if ((long)bytes + this.outEndIndex + (long)20000 <= (long)((int)this.bufferOut.Length))
					{
						this.outEndIndex = (long)bytes + this.outEndIndex;
						if (!this.isSenderWorking)
						{
							this.isSenderWorking = true;
							try
							{
								this.tcpClient.Client.BeginSend(this.bufferOut, this.outStartIndex, (int)this.outEndIndex - this.outStartIndex, SocketFlags.None, new AsyncCallback(this.SendCallback), this.tcpClient.Client);
							}
							catch (Exception exception2)
							{
								exception = exception2;
								this.isSenderWorking = false;
								try
								{
									this.tcpClient.Close();
								}
								catch (Exception exception1)
								{
									this.ManageError(exception);
								}
								GameObjectPhysics.Log(string.Format("{0:HH:mm:ss ffff} {1}", StaticData.now, exception));
							}
						}
					}
					else if (this.outStartIndex >= bytes)
					{
						Exception exception3 = new Exception(string.Format("Outbound buffer overflowed! Message is {0} bytes. Type={1}", bytes, h.requestType));
						this.ManageError(exception3);
						return;
					}
					else
					{
						GameObjectPhysics.Log("Error1");
						exception = new Exception(string.Format("Outbound buffer really overflowed! Message is {0} bytes. Type={1}", bytes, h.requestType));
						this.ManageError(exception);
						this.StopNet();
						return;
					}
				}
			}
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write((byte)this.state);
		bw.Write(this.playId);
		bw.Write(this.language ?? "");
		bw.Write(this.isChatAdmin);
		bw.Write(this.isChatDND);
		this.vessel.Serialize(bw);
		this.cfg.Serialize(bw);
		if (this.vessel.pvpState != PvPPlayerState.None)
		{
			this.pvpGame.Serialize(bw);
		}
	}

	public void StartNet(TcpClient client)
	{
		if ((this.tcpClient == null ? false : this.tcpClient.Client != null))
		{
			try
			{
				this.tcpClient.Client.Close();
			}
			catch (Exception exception)
			{
			}
		}
		this.tcpClient = client;
		this.lengthIn = 4;
		this.lengthInCurrent = 0;
		GameObjectPhysics.Log(string.Format("{0:HH:mm:ss:ffff} Inside StartNet I", StaticData.now));
		lock (this.bufferOut)
		{
			this.isSenderWorking = false;
		}
		GameObjectPhysics.Log(string.Format("{0:HH:mm:ss:ffff} Inside StartNet II", StaticData.now));
		this.tcpClient.Client.BeginReceive(this.bufferIn, 0, 4, SocketFlags.None, new AsyncCallback(this.AsyncReceiveSize), this.tcpClient);
		GameObjectPhysics.Log(string.Format("{0:HH:mm:ss:ffff} Inside StartNet III", StaticData.now));
		lock (this.bufferOut)
		{
			GameObjectPhysics.Log(string.Format("{0:HH:mm:ss:ffff} Inside StartNet IV", StaticData.now));
			this.outEndIndex = (long)0;
			this.outStartIndex = 0;
			this.lengthOutCurrent = 0;
			this.isSenderWorking = false;
			if (this.outEndIndex > (long)0)
			{
				this.isSenderWorking = true;
				try
				{
					this.tcpClient.Client.BeginSend(this.bufferOut, this.outStartIndex, (int)this.outEndIndex, SocketFlags.None, new AsyncCallback(this.SendCallback), this.tcpClient.Client);
				}
				catch (Exception exception1)
				{
					this.ManageError(exception1);
				}
			}
		}
		GameObjectPhysics.Log(string.Format("{0:HH:mm:ss:ffff} Inside StartNet V", StaticData.now));
	}

	public void StopNet()
	{
		try
		{
			this.tcpClient.Client.Close(1);
		}
		catch (Exception exception)
		{
		}
		lock (this.bufferOut)
		{
			this.isSenderWorking = false;
		}
	}
}