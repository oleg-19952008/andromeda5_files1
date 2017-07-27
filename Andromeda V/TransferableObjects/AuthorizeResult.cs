using System;
using System.IO;

public class AuthorizeResult : ITransferable
{
	public byte returnCode;

	public long id;

	[NonSerialized]
	public int dbId;

	public short serverVersion;

	public int loginId;

	public string universeServerIp = "";

	public int universeServerPort;

	public int galaxyServerPort;

	public short galaxyId;

	public bool isInBase;

	public string banReason = "";

	public DateTime banUntil;

	public long madmooId;

	public long playerId;

	public string sceneName = "";

	public DateTime maintenanceEndTime;

	public string url_payments;

	public string url_avatar;

	public string url_feedback;

	public string url_logout;

	public string url_recruit;

	public string url_fb;

	public bool payments_promotion;

	public AuthorizeResult()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.returnCode = br.ReadByte();
		this.id = br.ReadInt64();
		this.serverVersion = br.ReadInt16();
		this.loginId = br.ReadInt32();
		this.universeServerIp = br.ReadString();
		this.universeServerPort = br.ReadInt32();
		this.galaxyServerPort = br.ReadInt32();
		this.galaxyId = br.ReadInt16();
		this.isInBase = br.ReadBoolean();
		this.madmooId = br.ReadInt64();
		this.playerId = br.ReadInt64();
		this.sceneName = br.ReadString();
		DateTime now = DateTime.Now;
		this.maintenanceEndTime = now.AddSeconds((double)br.ReadInt32());
		this.url_payments = AuthorizeRequest.SafeReadString(br);
		this.url_avatar = AuthorizeRequest.SafeReadString(br);
		this.url_feedback = AuthorizeRequest.SafeReadString(br);
		this.url_logout = AuthorizeRequest.SafeReadString(br);
		this.url_recruit = AuthorizeRequest.SafeReadString(br);
		this.url_fb = AuthorizeRequest.SafeReadString(br);
		this.payments_promotion = br.ReadBoolean();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.returnCode);
		bw.Write(this.id);
		bw.Write(this.serverVersion);
		bw.Write(this.loginId);
		bw.Write(this.universeServerIp);
		bw.Write(this.universeServerPort);
		bw.Write(this.galaxyServerPort);
		bw.Write(this.galaxyId);
		bw.Write(this.isInBase);
		bw.Write(this.madmooId);
		bw.Write(this.playerId);
		bw.Write(this.sceneName);
		TimeSpan now = this.maintenanceEndTime - DateTime.Now;
		bw.Write((int)now.TotalSeconds);
		AuthorizeRequest.SafeWriteString(this.url_payments, bw);
		AuthorizeRequest.SafeWriteString(this.url_avatar, bw);
		AuthorizeRequest.SafeWriteString(this.url_feedback, bw);
		AuthorizeRequest.SafeWriteString(this.url_logout, bw);
		AuthorizeRequest.SafeWriteString(this.url_recruit, bw);
		AuthorizeRequest.SafeWriteString(this.url_fb, bw);
		bw.Write(this.payments_promotion);
	}

	public override string ToString()
	{
		object[] objArray = new object[] { this.loginId, this.galaxyServerPort, this.galaxyId, this.returnCode };
		return string.Format("loginId={0} galaxyServerPort={1} galaxyId={2} returnCode={3}", objArray);
	}
}