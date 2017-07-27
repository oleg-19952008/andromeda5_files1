using System;
using UnityEngine;

public class GalaxyJumpCancelWindow : GuiWindow
{
	public GalaxyJumpCancelWindow()
	{
	}

	public override void Create()
	{
		base.SetBackgroundTexture("ConfigWindow", "mineralToolTip");
		base.PutToCenter();
		this.boundries.set_y((float)(Screen.get_height() / 2) - this.boundries.get_height() * 1.6f);
		this.isHidden = false;
		this.zOrder = 250;
		GuiSecondsTracker guiSecondsTracker = new GuiSecondsTracker(StaticData.Translate("key_galaxy_jump_inprogress"), 3100, this)
		{
			boundries = new Rect(0f, 0f, 239f, 30f),
			Font = GuiLabel.FontBold,
			Alignment = 4,
			FontSize = 16
		};
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.SetTexture("NewGUI", "btnRed_");
		guiButtonResizeable.X = 10f;
		guiButtonResizeable.Y = 30f;
		guiButtonResizeable.Width = 220f;
		guiButtonResizeable.Caption = StaticData.Translate("key_quests_btn_cancel");
		guiButtonResizeable.Alignment = 4;
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, GalaxyJumpCancelWindow.OnCancelBtnClicked);
		base.AddGuiElement(guiButtonResizeable);
	}

	private void OnCancelBtnClicked(EventHandlerParam prm)
	{
		if (NetworkScript.player.shipScript != null)
		{
			NetworkScript.player.shipScript.isGuiClosed = true;
			NetworkScript.player.shipScript.CancelGalaxyJump(true);
		}
	}
}