using System;
using UnityEngine;

public class __ComingSoon : GuiWindow
{
	public __ComingSoon()
	{
	}

	public override void Create()
	{
		base.SetBackgroundTexture("NewGUI", "WndSkillTree");
		base.PutToCenter();
		this.zOrder = 210;
		this.isHidden = false;
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(230f, 75f, 635f, 410f),
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 28,
			Alignment = 4,
			text = StaticData.Translate("key_coming_soon")
		};
		base.AddGuiElement(guiLabel);
	}
}