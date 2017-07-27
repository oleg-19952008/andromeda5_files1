using System;
using UnityEngine;

public class cursorScript : MonoBehaviour
{
	public static Texture2D cursorImage;

	public static Texture2D normalState;

	public static Texture2D moveState;

	public static Texture2D pveState;

	public static Texture2D friendlyPveState;

	public static Texture2D mineralState;

	public cursorScript()
	{
	}

	private void OnGUI()
	{
		GUI.set_depth(0);
		Rect rect = new Rect();
		Vector3 _mousePosition = Input.get_mousePosition();
		if (cursorScript.cursorImage == cursorScript.normalState)
		{
			rect = new Rect(_mousePosition.x, (float)Screen.get_height() - _mousePosition.y - 3f, (float)(cursorScript.cursorImage.get_width() - 3), (float)cursorScript.cursorImage.get_height());
		}
		if (cursorScript.cursorImage == cursorScript.pveState || cursorScript.cursorImage == cursorScript.friendlyPveState || cursorScript.cursorImage == cursorScript.mineralState)
		{
			rect = new Rect(_mousePosition.x - (float)(cursorScript.cursorImage.get_height() / 2), (float)Screen.get_height() - _mousePosition.y - (float)(cursorScript.cursorImage.get_width() / 2), (float)cursorScript.cursorImage.get_width(), (float)cursorScript.cursorImage.get_height());
		}
		GUI.DrawTexture(rect, cursorScript.cursorImage);
	}

	private void Start()
	{
		Screen.set_showCursor(false);
		cursorScript.normalState = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "AndromedaCursor");
		cursorScript.pveState = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "EnemyLock");
		cursorScript.mineralState = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "MineralLock");
		cursorScript.friendlyPveState = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "FriendlyLock");
		cursorScript.cursorImage = cursorScript.normalState;
	}
}