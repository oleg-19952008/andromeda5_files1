using System;

public class AvatarItem
{
	public byte minAccessLevel;

	public string avatarName;

	public byte avatarIndex;

	public bool IsLocked
	{
		get
		{
			return NetworkScript.player.playerBelongings.playerAccessLevel < this.minAccessLevel;
		}
	}

	public AvatarItem()
	{
	}
}