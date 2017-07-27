using System;
using System.IO;

public class ChatMessage : ITransferable
{
	public ChatType chatType;

	public string senderName;

	public string text;

	public long senderId;

	public bool sendByAdmin;

	public bool isSystemMessage = false;

	public byte senderFractionId;

	public ChatMessage()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.chatType = (ChatType)br.ReadInt32();
		this.senderName = br.ReadString();
		this.text = br.ReadString();
		this.senderId = br.ReadInt64();
		this.sendByAdmin = br.ReadBoolean();
		this.isSystemMessage = br.ReadBoolean();
		this.senderFractionId = br.ReadByte();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write((int)this.chatType);
		bw.Write(this.senderName ?? "");
		bw.Write(this.text ?? ".");
		bw.Write(this.senderId);
		bw.Write(this.sendByAdmin);
		bw.Write(this.isSystemMessage);
		bw.Write(this.senderFractionId);
	}
}