using System;
using UnityEngine;

public class InstanceDifficultyWindow : GuiWindow
{
	private GuiButtonFixed enterButton;

	private GuiButtonFixed normalButton;

	private GuiButtonFixed hardButton;

	private GuiButtonFixed insaneButton;

	private GuiLabel normalInfo;

	private GuiLabel hardInfo;

	private GuiLabel insaneInfo;

	private InstanceDifficulty selectedDifficulty;

	public InstanceDifficultyWindow()
	{
	}

	public override void Create()
	{
		if (NetworkScript.player == null || NetworkScript.player.playerBelongings == null)
		{
			return;
		}
		float single = 38f;
		this.boundries = new Rect(0f, 0f, 418f + single, 278f);
		base.PutToHorizontalCenter();
		this.boundries.set_y(175f);
		this.zOrder = 210;
		this.isHidden = false;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("InstanceDifficulty", "frame");
		guiTexture.X = 0f + single;
		guiTexture.Y = 16f;
		base.AddGuiElement(guiTexture);
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("GalaxiesAvatars", NetworkScript.player.shipScript.jumpDestionationGalaxy.minimapAssetName);
		rect.boundries = new Rect(21f + single, 58f, 100f, 100f);
		base.AddGuiElement(rect);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(50f + single, 28f, 286f, 24f),
			text = string.Concat(StaticData.Translate(NetworkScript.player.shipScript.jumpDestionationGalaxy.nameUI), string.Format(" ({0} - {1})", NetworkScript.player.shipScript.jumpDestionationGalaxy.reqMinLevel, NetworkScript.player.shipScript.jumpDestionationGalaxy.reqMaxLevel)),
			Alignment = 4,
			FontSize = 16,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(guiLabel);
		Color color = new Color(0.0078f, 0.2392f, 0.3803f);
		this.enterButton = new GuiButtonFixed();
		this.enterButton.SetTexture("InstanceDifficulty", "button");
		this.enterButton.X = 18f + single;
		this.enterButton.Y = 207f;
		this.enterButton.textColorNormal = color;
		this.enterButton.textColorHover = color;
		this.enterButton.textColorClick = color;
		this.enterButton.Caption = StaticData.Translate("key_instance_difficulty_btn_enter");
		this.enterButton.Alignment = 4;
		base.AddGuiElement(this.enterButton);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(158f + single, 65f, 210f, 12f),
			text = StaticData.Translate("key_instance_difficulty_select_lable"),
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 13,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(guiLabel1);
		this.normalButton = new GuiButtonFixed();
		this.normalButton.SetTexture("InstanceDifficulty", "select");
		this.normalButton.X = 134f + single;
		this.normalButton.Y = 84f;
		this.normalButton.Caption = StaticData.Translate("key_instance_difficulty_btn_normal");
		this.normalButton.FontSize = 14;
		this.normalButton.textColorNormal = GuiNewStyleBar.blueColor;
		this.normalButton.textColorHover = GuiNewStyleBar.blueColor;
		this.normalButton.textColorDisabled = Color.get_gray();
		this.normalButton.textColorClick = GuiNewStyleBar.aquamarineColor;
		this.normalButton.MarginTop = 3;
		this.normalButton._marginLeft = 7;
		this.normalButton.isEnabled = false;
		this.normalButton.eventHandlerParam.customData = (InstanceDifficulty)0;
		this.normalButton.Clicked = new Action<EventHandlerParam>(this, InstanceDifficultyWindow.OnDifficultyChangeClick);
		this.normalButton.behaviourKeepClicked = true;
		this.normalButton.groupId = 31;
		base.AddGuiElement(this.normalButton);
		this.normalInfo = new GuiLabel()
		{
			boundries = new Rect(142f + single, 102f, 222f, 34f),
			text = string.Empty,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12
		};
		base.AddGuiElement(this.normalInfo);
		this.hardButton = new GuiButtonFixed();
		this.hardButton.SetTexture("InstanceDifficulty", "select");
		this.hardButton.X = 134f + single;
		this.hardButton.Y = 139f;
		this.hardButton.Caption = StaticData.Translate("key_instance_difficulty_btn_hard");
		this.hardButton.FontSize = 14;
		this.hardButton.textColorNormal = GuiNewStyleBar.blueColor;
		this.hardButton.textColorHover = GuiNewStyleBar.blueColor;
		this.hardButton.textColorDisabled = Color.get_gray();
		this.hardButton.textColorClick = GuiNewStyleBar.aquamarineColor;
		this.hardButton.MarginTop = 3;
		this.hardButton._marginLeft = 7;
		this.hardButton.isEnabled = false;
		this.hardButton.eventHandlerParam.customData = (InstanceDifficulty)1;
		this.hardButton.Clicked = new Action<EventHandlerParam>(this, InstanceDifficultyWindow.OnDifficultyChangeClick);
		this.hardButton.behaviourKeepClicked = true;
		this.hardButton.groupId = 31;
		base.AddGuiElement(this.hardButton);
		this.hardInfo = new GuiLabel()
		{
			boundries = new Rect(142f + single, 157f, 222f, 34f),
			text = string.Empty,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12
		};
		base.AddGuiElement(this.hardInfo);
		this.insaneButton = new GuiButtonFixed();
		this.insaneButton.SetTexture("InstanceDifficulty", "select");
		this.insaneButton.X = 134f + single;
		this.insaneButton.Y = 194f;
		this.insaneButton.Caption = StaticData.Translate("key_instance_difficulty_btn_insane");
		this.insaneButton.FontSize = 14;
		this.insaneButton.textColorNormal = GuiNewStyleBar.blueColor;
		this.insaneButton.textColorHover = GuiNewStyleBar.blueColor;
		this.insaneButton.textColorDisabled = Color.get_gray();
		this.insaneButton.textColorClick = GuiNewStyleBar.aquamarineColor;
		this.insaneButton.MarginTop = 3;
		this.insaneButton._marginLeft = 7;
		this.insaneButton.isEnabled = false;
		this.insaneButton.eventHandlerParam.customData = (InstanceDifficulty)2;
		this.insaneButton.Clicked = new Action<EventHandlerParam>(this, InstanceDifficultyWindow.OnDifficultyChangeClick);
		this.insaneButton.behaviourKeepClicked = true;
		this.insaneButton.groupId = 31;
		base.AddGuiElement(this.insaneButton);
		this.insaneInfo = new GuiLabel()
		{
			boundries = new Rect(142f + single, 212f, 222f, 34f),
			text = string.Empty,
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12
		};
		base.AddGuiElement(this.insaneInfo);
	}

	private void OnDifficultyChangeClick(EventHandlerParam prm)
	{
		this.selectedDifficulty = (int)prm.customData;
		this.normalButton.IsClicked = this.selectedDifficulty == 0;
		this.hardButton.IsClicked = this.selectedDifficulty == 1;
		this.insaneButton.IsClicked = this.selectedDifficulty == 2;
	}

	private void OnEnterClick(object prm)
	{
		MainScreenWindow mainScreenWindow = AndromedaGui.mainWnd;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = (byte)31
		};
		mainScreenWindow.OnWindowBtnClicked(eventHandlerParam);
		NetworkScript.player.shipScript.selectedDifficulty = this.selectedDifficulty;
		NetworkScript.player.shipScript.InitHyperJump();
		if (NetworkScript.player.shipScript != null)
		{
			NetworkScript.player.shipScript.isGuiClosed = true;
		}
	}

	public void PopulateData(int galaxyId, InstanceStatus status, InstanceDifficulty difficulty)
	{
		if (galaxyId != NetworkScript.player.shipScript.jumpDestionationGalaxy.__galaxyId)
		{
			return;
		}
		this.enterButton.Clicked = new Action<EventHandlerParam>(this, InstanceDifficultyWindow.OnEnterClick);
		switch (status)
		{
			case 0:
			{
				this.normalButton.isEnabled = true;
				this.hardButton.isEnabled = true;
				this.insaneButton.isEnabled = true;
				this.normalButton.IsClicked = this.selectedDifficulty == 0;
				this.hardButton.IsClicked = this.selectedDifficulty == 1;
				this.insaneButton.IsClicked = this.selectedDifficulty == 2;
				this.normalInfo.text = StaticData.Translate("key_instance_difficulty_normal_info");
				this.hardInfo.text = StaticData.Translate("key_instance_difficulty_hard_info");
				this.insaneInfo.text = StaticData.Translate("key_instance_difficulty_insane_info");
				break;
			}
			case 1:
			{
				this.normalButton.isEnabled = false;
				this.hardButton.isEnabled = false;
				this.insaneButton.isEnabled = false;
				switch (difficulty)
				{
					case 0:
					{
						this.normalButton.isEnabled = true;
						this.normalButton.IsClicked = true;
						this.normalInfo.text = StaticData.Translate("key_instance_difficulty_party_have_this");
						this.normalInfo.TextColor = GuiNewStyleBar.blueColor;
						this.hardInfo.text = StaticData.Translate("key_instance_difficulty_party_have_other");
						this.hardInfo.TextColor = Color.get_gray();
						this.insaneInfo.text = StaticData.Translate("key_instance_difficulty_party_have_other");
						this.insaneInfo.TextColor = Color.get_gray();
						break;
					}
					case 1:
					{
						this.hardButton.isEnabled = true;
						this.hardButton.IsClicked = true;
						this.normalInfo.text = StaticData.Translate("key_instance_difficulty_party_have_other");
						this.normalInfo.TextColor = Color.get_gray();
						this.hardInfo.text = StaticData.Translate("key_instance_difficulty_party_have_this");
						this.hardInfo.TextColor = GuiNewStyleBar.blueColor;
						this.insaneInfo.text = StaticData.Translate("key_instance_difficulty_party_have_other");
						this.insaneInfo.TextColor = Color.get_gray();
						break;
					}
					case 2:
					{
						this.insaneButton.isEnabled = true;
						this.insaneButton.IsClicked = true;
						this.normalInfo.text = StaticData.Translate("key_instance_difficulty_party_have_other");
						this.normalInfo.TextColor = Color.get_gray();
						this.hardInfo.text = StaticData.Translate("key_instance_difficulty_party_have_other");
						this.hardInfo.TextColor = Color.get_gray();
						this.insaneInfo.text = StaticData.Translate("key_instance_difficulty_party_have_this");
						this.insaneInfo.TextColor = GuiNewStyleBar.blueColor;
						break;
					}
				}
				break;
			}
			case 2:
			{
				this.normalButton.isEnabled = false;
				this.hardButton.isEnabled = false;
				this.insaneButton.isEnabled = false;
				switch (difficulty)
				{
					case 0:
					{
						this.normalButton.isEnabled = true;
						this.normalButton.IsClicked = true;
						this.normalInfo.text = StaticData.Translate("key_instance_difficulty_you_have_this");
						this.normalInfo.TextColor = GuiNewStyleBar.blueColor;
						this.hardInfo.text = StaticData.Translate("key_instance_difficulty_you_have_other");
						this.hardInfo.TextColor = Color.get_gray();
						this.insaneInfo.text = StaticData.Translate("key_instance_difficulty_you_have_other");
						this.insaneInfo.TextColor = Color.get_gray();
						break;
					}
					case 1:
					{
						this.hardButton.isEnabled = true;
						this.hardButton.IsClicked = true;
						this.normalInfo.text = StaticData.Translate("key_instance_difficulty_you_have_other");
						this.normalInfo.TextColor = Color.get_gray();
						this.hardInfo.text = StaticData.Translate("key_instance_difficulty_you_have_this");
						this.hardInfo.TextColor = GuiNewStyleBar.blueColor;
						this.insaneInfo.text = StaticData.Translate("key_instance_difficulty_you_have_other");
						this.insaneInfo.TextColor = Color.get_gray();
						break;
					}
					case 2:
					{
						this.insaneButton.isEnabled = true;
						this.insaneButton.IsClicked = true;
						this.normalInfo.text = StaticData.Translate("key_instance_difficulty_you_have_other");
						this.normalInfo.TextColor = Color.get_gray();
						this.hardInfo.text = StaticData.Translate("key_instance_difficulty_you_have_other");
						this.hardInfo.TextColor = Color.get_gray();
						this.insaneInfo.text = StaticData.Translate("key_instance_difficulty_you_have_this");
						this.insaneInfo.TextColor = GuiNewStyleBar.blueColor;
						break;
					}
				}
				break;
			}
		}
	}
}