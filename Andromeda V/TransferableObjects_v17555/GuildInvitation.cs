using System;

public class GuildInvitation
{
	public int id;

	public PlayerBasic invitee;

	public PlayerBasic inviter;

	public DateTime timeExpire;

	public string message = "";

	public Guild guild;

	public GuildInvitation()
	{
	}
}