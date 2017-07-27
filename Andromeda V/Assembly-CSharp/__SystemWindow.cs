using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class __SystemWindow : GuiWindow
{
	private GuiButton btnSkillBar;

	private GuiButton btnAudio;

	private GuiButton btnVideo;

	private GuiButton btnControls;

	private GuiButton btnSaveExit;

	private GuiTexture mainScreenBG;

	private GuiTexture resAvatar_TX;

	private GuiLabel lbl_PlayerLvl;

	private GuiLabel lbl_PlayerName;

	private GuiLabel lbl_ShiName;

	private GuiLabel lbl_InfoBoxTitle;

	private GuiLabel lbl_InfoBoxValue;

	private GuiLabel lbl_title;

	private GuiButtonFixed minusMasterBtn;

	private GuiButtonFixed plusMasterBtn;

	private GuiButtonFixed minusMusicBtn;

	private GuiButtonFixed plusMusicBtn;

	private GuiButtonFixed minusVoiceBtn;

	private GuiButtonFixed plusVoiceBtn;

	private GuiButtonFixed minusFxBtn;

	private GuiButtonFixed plusFxBtn;

	private GuiHorizontalSlider _sliderMaster;

	private GuiHorizontalSlider _sliderMusic;

	private GuiHorizontalSlider _sliderVoice;

	private GuiHorizontalSlider _sliderFx;

	private GuiNewStyleBar barMasterVolume;

	private GuiNewStyleBar barMusicVolume;

	private GuiNewStyleBar barVoiceVolume;

	private GuiNewStyleBar barFxVolume;

	private GuiLabel muteMasterLbl;

	private GuiLabel muteMusicLbl;

	private GuiLabel muteVoiceLbl;

	private GuiLabel muteFxLbl;

	private GuiTexture muteMasterIcon;

	private GuiTexture muteMusicIcon;

	private GuiTexture muteVoiceIcon;

	private GuiTexture muteFxIcon;

	private GuiButton muteMaster_btn;

	private GuiButton muteMusic_btn;

	private GuiButton muteVoice_btn;

	private GuiButton muteFx_btn;

	private GuiButtonResizeable btnRestoreToDefaults;

	private GuiDialog dlgConfirmSave;

	private bool isMasterSoundMute = GuiFramework.IsMasterSoundsMute;

	private bool isMusicSoundMute = GuiFramework.IsMusicSoundsMute;

	private bool isVoiceSoundMute = GuiFramework.IsVoiceSoundsMute;

	private bool isFxSoundMute = GuiFramework.IsFxSoundsMute;

	private float curentMasterVolume = 1f;

	private float curentMusicVolume = 1f;

	private float curentVoiceVolume = 1f;

	private float curentFxVolume = 1f;

	private __SystemWindow.SystemWindowTab selectedTab;

	private List<GuiElement> forDelete;

	private GuiLabel lblResolution;

	private GuiDropdown dropdownResolution;

	private GuiLabel lblFullscreen;

	private GuiCheckbox checkboxFullscreen;

	private GuiLabel lblAnisotropic;

	private GuiCheckbox checkboxAnisotropic;

	private GuiLabel lblAntialiasing;

	private GuiCheckbox checkboxAntialiasing;

	private GuiDropdown dropdownAntialiasing;

	private GuiLabel lblVSync;

	private GuiCheckbox checkboxVSync;

	private GuiTexture titleLine;

	private GuiLabel systemLbl;

	private Resolution[] resolutions;

	public GuiButtonFixed btnClose;

	private List<GuiButtonResizeable> controlsList;

	private KeyboardShortcuts keyboardInstance;

	public GuiScrollingContainer controlsScroller;

	public bool IsOtherButtonClicked
	{
		get
		{
			if (!this.btnAudio.IsMouseOver && !this.btnControls.IsMouseOver && !this.btnRestoreToDefaults.IsMouseOver && !this.btnSaveExit.IsMouseOver && !this.btnSkillBar.IsMouseOver && !this.btnVideo.IsMouseOver && (this.btnClose == null || !this.btnClose.IsMouseOver) && Enumerable.FirstOrDefault<GuiButtonResizeable>(Enumerable.Where<GuiButtonResizeable>(this.controlsList, new Func<GuiButtonResizeable, bool>(this, (GuiButtonResizeable p) => (!p.IsMouseOver ? false : (object)p != (object)this.keyboardInstance.keyBoardUpdateButton)))) == null)
			{
				return false;
			}
			return true;
		}
	}

	public __SystemWindow()
	{
	}

	private void AnisotropicSelected(bool state)
	{
		if (!state)
		{
			QualitySettings.set_anisotropicFiltering(0);
		}
		else
		{
			QualitySettings.set_anisotropicFiltering(1);
		}
	}

	private void AntialiasingSelected(int i)
	{
		switch (i)
		{
			case 0:
			{
				QualitySettings.set_antiAliasing(0);
				break;
			}
			case 1:
			{
				QualitySettings.set_antiAliasing(2);
				break;
			}
			case 2:
			{
				QualitySettings.set_antiAliasing(4);
				break;
			}
			case 3:
			{
				QualitySettings.set_antiAliasing(8);
				break;
			}
		}
	}

	private void ClearContent()
	{
		foreach (GuiElement guiElement in this.forDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.forDelete.Clear();
		this.lbl_InfoBoxValue.text = StaticData.Translate("key_system_menu_infobox_default");
		this.lbl_InfoBoxValue.Alignment = 1;
	}

	public override void Create()
	{
		base.SetBackgroundTexture("FusionWindow", "FusionBackGround");
		base.PutToCenter();
		this.zOrder = 210;
		this.forDelete = new List<GuiElement>();
		this.mainScreenBG = new GuiTexture()
		{
			boundries = new Rect(0f, 0f, 904f, 539f)
		};
		this.mainScreenBG.SetTextureKeepSize("SystemWindow", "systemTab1");
		base.AddGuiElement(this.mainScreenBG);
		this.lbl_title = new GuiLabel()
		{
			text = StaticData.Translate("key_system_menu_title"),
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 18,
			Font = GuiLabel.FontBold,
			boundries = new Rect(280f, 42f, 540f, 35f),
			Alignment = 1
		};
		base.AddGuiElement(this.lbl_title);
		this.lbl_InfoBoxTitle = new GuiLabel()
		{
			boundries = new Rect(22f, 300f, 82f, 20f),
			text = StaticData.Translate("key_system_menu_infobox"),
			Alignment = 1,
			FontSize = 14,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.lbl_InfoBoxTitle);
		this.lbl_InfoBoxValue = new GuiLabel()
		{
			boundries = new Rect(32f, 327f, 169f, 205f),
			text = StaticData.Translate("key_system_menu_infobox_default"),
			Alignment = 1,
			FontSize = 13
		};
		base.AddGuiElement(this.lbl_InfoBoxValue);
		this.resAvatar_TX = new GuiTexture()
		{
			boundries = new Rect(65f, 100f, 100f, 100f)
		};
		base.AddGuiElement(this.resAvatar_TX);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = (!NetworkScript.player.vessel.isGuest ? NetworkScript.player.playerBelongings.playerName : StaticData.Translate("key_guest_player")),
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			boundries = new Rect(33f, 72f, 169f, 34f),
			Alignment = 4
		};
		base.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			text = string.Format(StaticData.Translate("key_skills_tree_player_level"), NetworkScript.player.cfg.playerLevel),
			TextColor = Color.get_white(),
			FontSize = 12,
			boundries = new Rect(33f, 215f, 169f, 20f),
			Alignment = 1
		};
		base.AddGuiElement(guiLabel1);
		string str = NetworkScript.player.cfg.assetName;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("ShipsAvatars", str);
		guiTexture.X = 65f;
		guiTexture.Y = 123f;
		guiTexture.SetSize(104f, 71f);
		base.AddGuiElement(guiTexture);
		this.btnSkillBar = new GuiButton()
		{
			boundries = new Rect(210f, 50f, 135f, 70f),
			Alignment = 4,
			Caption = StaticData.Translate("key_system_menu_tab_skillbar"),
			textColorClick = GuiNewStyleBar.orangeColor,
			textColorNormal = GuiNewStyleBar.blueColor,
			textColorHover = Color.get_white(),
			FontSize = 13,
			Clicked = new Action<EventHandlerParam>(this, __SystemWindow.OnSkillBarClick),
			behaviourKeepClicked = true,
			IsClicked = this.selectedTab == __SystemWindow.SystemWindowTab.SkillBar
		};
		base.AddGuiElement(this.btnSkillBar);
		this.btnAudio = new GuiButton()
		{
			boundries = new Rect(this.btnSkillBar.boundries.get_x() + 140f, this.btnSkillBar.boundries.get_y(), 130f, 70f),
			Alignment = 4,
			Caption = StaticData.Translate("key_system_menu_tab_audio"),
			textColorClick = GuiNewStyleBar.orangeColor,
			textColorNormal = GuiNewStyleBar.blueColor,
			textColorHover = Color.get_white(),
			FontSize = 13,
			Clicked = new Action<EventHandlerParam>(this, __SystemWindow.OnAudioClick),
			behaviourKeepClicked = true,
			IsClicked = this.selectedTab == __SystemWindow.SystemWindowTab.Audio
		};
		base.AddGuiElement(this.btnAudio);
		this.btnVideo = new GuiButton()
		{
			boundries = new Rect(this.btnAudio.boundries.get_x() + 135f, this.btnAudio.boundries.get_y(), 130f, 70f),
			Alignment = 4,
			Caption = StaticData.Translate("key_system_menu_tab_video"),
			textColorClick = GuiNewStyleBar.orangeColor,
			textColorNormal = GuiNewStyleBar.blueColor,
			textColorHover = Color.get_white(),
			FontSize = 13,
			Clicked = new Action<EventHandlerParam>(this, __SystemWindow.OnVideoClick),
			behaviourKeepClicked = true,
			IsClicked = this.selectedTab == __SystemWindow.SystemWindowTab.Video
		};
		base.AddGuiElement(this.btnVideo);
		this.btnControls = new GuiButton()
		{
			boundries = new Rect(this.btnVideo.boundries.get_x() + 135f, this.btnVideo.boundries.get_y(), 130f, 70f),
			Alignment = 4,
			Caption = StaticData.Translate("key_system_menu_tab_controls"),
			textColorClick = GuiNewStyleBar.orangeColor,
			textColorNormal = GuiNewStyleBar.blueColor,
			textColorHover = Color.get_white(),
			FontSize = 13,
			Clicked = new Action<EventHandlerParam>(this, __SystemWindow.OnControlsClick),
			textColorDisabled = GuiNewStyleBar.blueColorDisable,
			isEnabled = NetworkScript.player.vessel.galaxy.get_galaxyId() != 1000,
			behaviourKeepClicked = true,
			IsClicked = this.selectedTab == __SystemWindow.SystemWindowTab.Controls
		};
		base.AddGuiElement(this.btnControls);
		this.btnSaveExit = new GuiButton()
		{
			boundries = new Rect(this.btnControls.boundries.get_x() + 135f, this.btnControls.boundries.get_y(), 130f, 70f),
			Alignment = 4,
			Caption = StaticData.Translate("key_system_menu_tab_logout"),
			textColorClick = GuiNewStyleBar.orangeColor,
			textColorNormal = GuiNewStyleBar.blueColor,
			textColorHover = Color.get_white(),
			FontSize = 13,
			Clicked = new Action<EventHandlerParam>(this, __SystemWindow.OnSaveExitClick),
			behaviourKeepClicked = true,
			IsClicked = this.selectedTab == __SystemWindow.SystemWindowTab.SaveExit
		};
		base.AddGuiElement(this.btnSaveExit);
		this.isHidden = false;
		AndromedaGui.mainWnd.OnCloseWindowCallback = new Action(this, __SystemWindow.SetNewVolumes);
	}

	private GuiWindow DrawTalentTooltip(object parm)
	{
		TalentTooltipInfo talentTooltipInfo = (TalentTooltipInfo)parm;
		GuiWindow guiWindow = new GuiWindow();
		guiWindow.SetBackgroundTexture("SystemWindow", "SkillTooltipInspace");
		guiWindow.isHidden = false;
		guiWindow.boundries.set_x(talentTooltipInfo.position.x);
		guiWindow.boundries.set_y(talentTooltipInfo.position.y - 100f);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = StaticData.Translate(talentTooltipInfo.talent.uiName),
			boundries = new Rect(0f, 0f, 190f, 20f),
			FontSize = 12,
			Alignment = 4,
			TextColor = GuiNewStyleBar.orangeColor
		};
		guiWindow.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			text = MainScreenWindow.GetActiveSkillTooltip(talentTooltipInfo.talent.itemType),
			boundries = new Rect(4f, 24f, 182f, 60f),
			Alignment = 0
		};
		guiWindow.AddGuiElement(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(0f, 78f, 175f, 17f),
			Alignment = 8,
			text = string.Format(StaticData.Translate("key_main_screen_skill_tooltip3"), Mathf.FloorToInt((float)NetworkScript.player.playerBelongings.playerItems.GetSkillCooldown(talentTooltipInfo.talent.itemType) * 0.001f)),
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 11
		};
		guiWindow.AddGuiElement(guiLabel2);
		return guiWindow;
	}

	private void FullscreenSelected(bool state)
	{
		Screen.SetResolution(Screen.get_width(), Screen.get_height(), state);
	}

	private void OnAudioClick(object prm)
	{
		this.ClearContent();
		this.mainScreenBG.SetTextureKeepSize("SystemWindow", "systemTab2");
		this.lbl_InfoBoxValue.text = StaticData.Translate("key_system_menu_infobox_audio");
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(210f, 140f, 680f, 15f),
			Alignment = 1,
			FontSize = 13,
			text = StaticData.Translate("key_system_menu_audio_master").ToUpper()
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		this._sliderMaster = new GuiHorizontalSlider(0f, 100f, GuiFramework.masterVolume * 100f)
		{
			Width = 400f,
			X = 350f,
			Y = guiLabel.Y + 20f,
			refreshData = new Action(this, __SystemWindow.RefreshMasterSlider)
		};
		this._sliderMaster.SetEmptySliderTexture();
		this.barMasterVolume = new GuiNewStyleBar();
		if (!this.isMasterSoundMute)
		{
			this.barMasterVolume.SetCustumSize(405, Color.get_white());
		}
		else
		{
			this.barMasterVolume.SetCustumSize(405, Color.get_red());
		}
		this.barMasterVolume.X = 348f;
		this.barMasterVolume.Y = this._sliderMaster.Y + 5f;
		this.barMasterVolume.current = this._sliderMaster.CurrentValue;
		base.AddGuiElement(this.barMasterVolume);
		this.forDelete.Add(this.barMasterVolume);
		base.AddGuiElement(this._sliderMaster);
		this.forDelete.Add(this._sliderMaster);
		this.minusMasterBtn = new GuiButtonFixed();
		this.minusMasterBtn.SetTexture("SystemWindow", "BtnMinus");
		this.minusMasterBtn.X = this._sliderMaster.X - 23f;
		this.minusMasterBtn.Y = this._sliderMaster.Y + 3f;
		this.minusMasterBtn.Caption = string.Empty;
		this.minusMasterBtn.Clicked = new Action<EventHandlerParam>(this, __SystemWindow.OnMasterMinusBtnClicked);
		this.minusMasterBtn.SetLeftClickSound("Sounds", "minus");
		base.AddGuiElement(this.minusMasterBtn);
		this.forDelete.Add(this.minusMasterBtn);
		this.plusMasterBtn = new GuiButtonFixed();
		this.plusMasterBtn.SetTexture("SystemWindow", "BtnPlus");
		this.plusMasterBtn.X = this._sliderMaster.X + 410f;
		this.plusMasterBtn.Y = this._sliderMaster.Y + 3f;
		this.plusMasterBtn.Caption = string.Empty;
		this.plusMasterBtn.Clicked = new Action<EventHandlerParam>(this, __SystemWindow.OnMasterPlusBtnClicked);
		this.plusMasterBtn.SetLeftClickSound("Sounds", "plus");
		base.AddGuiElement(this.plusMasterBtn);
		this.forDelete.Add(this.plusMasterBtn);
		this.muteMasterLbl = new GuiLabel()
		{
			boundries = new Rect(650f, this._sliderMaster.Y + 40f, 100f, 15f),
			Alignment = 5,
			FontSize = 12,
			text = StaticData.Translate("key_system_menu_audio_mute").ToUpper()
		};
		if (!this.isMasterSoundMute)
		{
			this.muteMasterLbl.TextColor = Color.get_white();
		}
		else
		{
			this.muteMasterLbl.TextColor = Color.get_red();
		}
		base.AddGuiElement(this.muteMasterLbl);
		this.forDelete.Add(this.muteMasterLbl);
		this.muteMasterIcon = new GuiTexture();
		this.muteMasterIcon.SetTexture("SystemWindow", "muteIconOn");
		this.muteMasterIcon.X = this.muteMasterLbl.X + 108f;
		this.muteMasterIcon.Y = this.muteMasterLbl.Y + 1f;
		if (this.isMasterSoundMute)
		{
			this.muteMasterIcon.SetTexture("SystemWindow", "muteIconOff");
		}
		base.AddGuiElement(this.muteMasterIcon);
		this.forDelete.Add(this.muteMasterIcon);
		this.muteMaster_btn = new GuiButton()
		{
			boundries = this.muteMasterIcon.boundries
		};
		ref Rect muteMasterBtn = ref this.muteMaster_btn.boundries;
		muteMasterBtn.set_x(muteMasterBtn.get_x() - (this.muteMasterLbl.TextWidth + 8f));
		this.muteMaster_btn.boundries.set_width(this.muteMasterIcon.boundries.get_width() + this.muteMasterLbl.TextWidth + 8f);
		this.muteMaster_btn.Caption = string.Empty;
		this.muteMaster_btn.Clicked = new Action<EventHandlerParam>(this, __SystemWindow.OnMasterMuteBtnClicked);
		base.AddGuiElement(this.muteMaster_btn);
		this.forDelete.Add(this.muteMaster_btn);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(210f, 240f, 680f, 15f),
			Alignment = 1,
			FontSize = 13,
			text = StaticData.Translate("key_system_menu_audio_music").ToUpper()
		};
		base.AddGuiElement(guiLabel1);
		this.forDelete.Add(guiLabel1);
		this._sliderMusic = new GuiHorizontalSlider(0f, 100f, GuiFramework.musicVolume * 100f)
		{
			Width = 400f,
			X = 350f,
			Y = guiLabel1.Y + 20f,
			refreshData = new Action(this, __SystemWindow.RefreshMusicSlider)
		};
		this._sliderMusic.SetEmptySliderTexture();
		this.barMusicVolume = new GuiNewStyleBar();
		if (!this.isMusicSoundMute)
		{
			this.barMusicVolume.SetCustumSize(405, Color.get_white());
		}
		else
		{
			this.barMusicVolume.SetCustumSize(405, Color.get_red());
		}
		this.barMusicVolume.X = 348f;
		this.barMusicVolume.Y = this._sliderMusic.Y + 5f;
		this.barMusicVolume.current = this._sliderMusic.CurrentValue;
		base.AddGuiElement(this.barMusicVolume);
		this.forDelete.Add(this.barMusicVolume);
		base.AddGuiElement(this._sliderMusic);
		this.forDelete.Add(this._sliderMusic);
		this.minusMusicBtn = new GuiButtonFixed();
		this.minusMusicBtn.SetTexture("SystemWindow", "BtnMinus");
		this.minusMusicBtn.X = this._sliderMusic.X - 23f;
		this.minusMusicBtn.Y = this._sliderMusic.Y + 3f;
		this.minusMusicBtn.Caption = string.Empty;
		this.minusMusicBtn.Clicked = new Action<EventHandlerParam>(this, __SystemWindow.OnMusicMinusBtnClicked);
		this.minusMusicBtn.SetLeftClickSound("Sounds", "minus");
		base.AddGuiElement(this.minusMusicBtn);
		this.forDelete.Add(this.minusMusicBtn);
		this.plusMusicBtn = new GuiButtonFixed();
		this.plusMusicBtn.SetTexture("SystemWindow", "BtnPlus");
		this.plusMusicBtn.X = this._sliderMusic.X + 410f;
		this.plusMusicBtn.Y = this._sliderMusic.Y + 3f;
		this.plusMusicBtn.Caption = string.Empty;
		this.plusMusicBtn.Clicked = new Action<EventHandlerParam>(this, __SystemWindow.OnMusicPlusBtnClicked);
		this.plusMusicBtn.SetLeftClickSound("Sounds", "plus");
		base.AddGuiElement(this.plusMusicBtn);
		this.forDelete.Add(this.plusMusicBtn);
		this.muteMusicLbl = new GuiLabel()
		{
			boundries = new Rect(650f, this._sliderMusic.Y + 40f, 100f, 15f),
			Alignment = 5,
			FontSize = 12,
			text = StaticData.Translate("key_system_menu_audio_mute").ToUpper()
		};
		if (!this.isMusicSoundMute)
		{
			this.muteMusicLbl.TextColor = Color.get_white();
		}
		else
		{
			this.muteMusicLbl.TextColor = Color.get_red();
		}
		base.AddGuiElement(this.muteMusicLbl);
		this.forDelete.Add(this.muteMusicLbl);
		this.muteMusicIcon = new GuiTexture();
		this.muteMusicIcon.SetTexture("SystemWindow", "muteIconOn");
		this.muteMusicIcon.X = this.muteMusicLbl.X + 108f;
		this.muteMusicIcon.Y = this.muteMusicLbl.Y + 1f;
		if (this.isMusicSoundMute)
		{
			this.muteMusicIcon.SetTexture("SystemWindow", "muteIconOff");
		}
		base.AddGuiElement(this.muteMusicIcon);
		this.forDelete.Add(this.muteMusicIcon);
		this.muteMusic_btn = new GuiButton()
		{
			boundries = this.muteMusicIcon.boundries
		};
		ref Rect muteMusicBtn = ref this.muteMusic_btn.boundries;
		muteMusicBtn.set_x(muteMusicBtn.get_x() - (this.muteMusicLbl.TextWidth + 8f));
		this.muteMusic_btn.boundries.set_width(this.muteMusicIcon.boundries.get_width() + this.muteMusicLbl.TextWidth + 8f);
		this.muteMusic_btn.Caption = string.Empty;
		this.muteMusic_btn.Clicked = new Action<EventHandlerParam>(this, __SystemWindow.OnMusicMuteBtnClicked);
		base.AddGuiElement(this.muteMusic_btn);
		this.forDelete.Add(this.muteMusic_btn);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(210f, 340f, 680f, 15f),
			Alignment = 1,
			FontSize = 13,
			text = StaticData.Translate("key_system_menu_audio_fx").ToUpper()
		};
		base.AddGuiElement(guiLabel2);
		this.forDelete.Add(guiLabel2);
		this._sliderFx = new GuiHorizontalSlider(0f, 100f, GuiFramework.fxVolume * 100f)
		{
			Width = 400f,
			X = 350f,
			Y = guiLabel2.Y + 20f,
			refreshData = new Action(this, __SystemWindow.RefreshFxSlider)
		};
		this._sliderFx.SetEmptySliderTexture();
		this.barFxVolume = new GuiNewStyleBar();
		if (!this.isFxSoundMute)
		{
			this.barFxVolume.SetCustumSize(405, Color.get_white());
		}
		else
		{
			this.barFxVolume.SetCustumSize(405, Color.get_red());
		}
		this.barFxVolume.X = 348f;
		this.barFxVolume.Y = this._sliderFx.Y + 5f;
		this.barFxVolume.current = this._sliderFx.CurrentValue;
		base.AddGuiElement(this.barFxVolume);
		this.forDelete.Add(this.barFxVolume);
		base.AddGuiElement(this._sliderFx);
		this.forDelete.Add(this._sliderFx);
		this.minusFxBtn = new GuiButtonFixed();
		this.minusFxBtn.SetTexture("SystemWindow", "BtnMinus");
		this.minusFxBtn.X = this._sliderFx.X - 23f;
		this.minusFxBtn.Y = this._sliderFx.Y + 3f;
		this.minusFxBtn.Caption = string.Empty;
		this.minusFxBtn.Clicked = new Action<EventHandlerParam>(this, __SystemWindow.OnFxMinusBtnClicked);
		this.minusFxBtn.SetLeftClickSound("Sounds", "minus");
		base.AddGuiElement(this.minusFxBtn);
		this.forDelete.Add(this.minusFxBtn);
		this.plusFxBtn = new GuiButtonFixed();
		this.plusFxBtn.SetTexture("SystemWindow", "BtnPlus");
		this.plusFxBtn.X = this._sliderFx.X + 410f;
		this.plusFxBtn.Y = this._sliderFx.Y + 3f;
		this.plusFxBtn.Caption = string.Empty;
		this.plusFxBtn.Clicked = new Action<EventHandlerParam>(this, __SystemWindow.OnFxPlusBtnClicked);
		this.plusFxBtn.SetLeftClickSound("Sounds", "plus");
		base.AddGuiElement(this.plusFxBtn);
		this.forDelete.Add(this.plusFxBtn);
		this.muteFxLbl = new GuiLabel()
		{
			boundries = new Rect(650f, this._sliderFx.Y + 40f, 100f, 15f),
			Alignment = 5,
			FontSize = 12,
			text = StaticData.Translate("key_system_menu_audio_mute").ToUpper()
		};
		if (!this.isFxSoundMute)
		{
			this.muteFxLbl.TextColor = Color.get_white();
		}
		else
		{
			this.muteFxLbl.TextColor = Color.get_red();
		}
		base.AddGuiElement(this.muteFxLbl);
		this.forDelete.Add(this.muteFxLbl);
		this.muteFxIcon = new GuiTexture();
		this.muteFxIcon.SetTexture("SystemWindow", "muteIconOn");
		this.muteFxIcon.X = this.muteFxLbl.X + 108f;
		this.muteFxIcon.Y = this.muteFxLbl.Y + 1f;
		if (this.isFxSoundMute)
		{
			this.muteFxIcon.SetTexture("SystemWindow", "muteIconOff");
		}
		base.AddGuiElement(this.muteFxIcon);
		this.forDelete.Add(this.muteFxIcon);
		this.muteFx_btn = new GuiButton()
		{
			boundries = this.muteFxIcon.boundries
		};
		ref Rect muteFxBtn = ref this.muteFx_btn.boundries;
		muteFxBtn.set_x(muteFxBtn.get_x() - (this.muteFxLbl.TextWidth + 8f));
		this.muteFx_btn.boundries.set_width(this.muteFxIcon.boundries.get_width() + this.muteFxLbl.TextWidth + 8f);
		this.muteFx_btn.Caption = string.Empty;
		this.muteFx_btn.Clicked = new Action<EventHandlerParam>(this, __SystemWindow.OnFxMuteBtnClicked);
		base.AddGuiElement(this.muteFx_btn);
		this.forDelete.Add(this.muteFx_btn);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(210f, 440f, 680f, 15f),
			Alignment = 1,
			FontSize = 13,
			text = StaticData.Translate("key_system_menu_audio_system").ToUpper()
		};
		base.AddGuiElement(guiLabel3);
		this.forDelete.Add(guiLabel3);
		this._sliderVoice = new GuiHorizontalSlider(0f, 100f, GuiFramework.voiceVolume * 100f)
		{
			Width = 400f,
			X = 350f,
			Y = guiLabel3.Y + 20f,
			refreshData = new Action(this, __SystemWindow.RefreshVoiceSlider)
		};
		this._sliderVoice.SetEmptySliderTexture();
		this.barVoiceVolume = new GuiNewStyleBar();
		if (!this.isVoiceSoundMute)
		{
			this.barVoiceVolume.SetCustumSize(405, Color.get_white());
		}
		else
		{
			this.barVoiceVolume.SetCustumSize(405, Color.get_red());
		}
		this.barVoiceVolume.X = 348f;
		this.barVoiceVolume.Y = this._sliderVoice.Y + 5f;
		this.barVoiceVolume.current = this._sliderVoice.CurrentValue;
		base.AddGuiElement(this.barVoiceVolume);
		this.forDelete.Add(this.barVoiceVolume);
		base.AddGuiElement(this._sliderVoice);
		this.forDelete.Add(this._sliderVoice);
		this.minusVoiceBtn = new GuiButtonFixed();
		this.minusVoiceBtn.SetTexture("SystemWindow", "BtnMinus");
		this.minusVoiceBtn.X = this._sliderVoice.X - 23f;
		this.minusVoiceBtn.Y = this._sliderVoice.Y + 3f;
		this.minusVoiceBtn.Caption = string.Empty;
		this.minusVoiceBtn.Clicked = new Action<EventHandlerParam>(this, __SystemWindow.OnVoiceMinusBtnClicked);
		this.minusVoiceBtn.SetLeftClickSound("Sounds", "minus");
		base.AddGuiElement(this.minusVoiceBtn);
		this.forDelete.Add(this.minusVoiceBtn);
		this.plusVoiceBtn = new GuiButtonFixed();
		this.plusVoiceBtn.SetTexture("SystemWindow", "BtnPlus");
		this.plusVoiceBtn.X = this._sliderVoice.X + 410f;
		this.plusVoiceBtn.Y = this._sliderVoice.Y + 3f;
		this.plusVoiceBtn.Caption = string.Empty;
		this.plusVoiceBtn.Clicked = new Action<EventHandlerParam>(this, __SystemWindow.OnVoicePlusBtnClicked);
		this.plusVoiceBtn.SetLeftClickSound("Sounds", "plus");
		base.AddGuiElement(this.plusVoiceBtn);
		this.forDelete.Add(this.plusVoiceBtn);
		this.muteVoiceLbl = new GuiLabel()
		{
			boundries = new Rect(650f, this._sliderVoice.Y + 40f, 100f, 15f),
			Alignment = 5,
			FontSize = 12,
			text = StaticData.Translate("key_system_menu_audio_mute").ToUpper()
		};
		if (!this.isVoiceSoundMute)
		{
			this.muteVoiceLbl.TextColor = Color.get_white();
		}
		else
		{
			this.muteVoiceLbl.TextColor = Color.get_red();
		}
		base.AddGuiElement(this.muteVoiceLbl);
		this.forDelete.Add(this.muteVoiceLbl);
		this.muteVoiceIcon = new GuiTexture();
		this.muteVoiceIcon.SetTexture("SystemWindow", "muteIconOn");
		this.muteVoiceIcon.X = this.muteVoiceLbl.X + 108f;
		this.muteVoiceIcon.Y = this.muteVoiceLbl.Y + 1f;
		if (this.isVoiceSoundMute)
		{
			this.muteVoiceIcon.SetTexture("SystemWindow", "muteIconOff");
		}
		base.AddGuiElement(this.muteVoiceIcon);
		this.forDelete.Add(this.muteVoiceIcon);
		this.muteVoice_btn = new GuiButton()
		{
			boundries = this.muteVoiceIcon.boundries
		};
		ref Rect muteVoiceBtn = ref this.muteVoice_btn.boundries;
		muteVoiceBtn.set_x(muteVoiceBtn.get_x() - (this.muteVoiceLbl.TextWidth + 8f));
		this.muteVoice_btn.boundries.set_width(this.muteVoiceIcon.boundries.get_width() + this.muteVoiceLbl.TextWidth + 8f);
		this.muteVoice_btn.Caption = string.Empty;
		this.muteVoice_btn.Clicked = new Action<EventHandlerParam>(this, __SystemWindow.OnVoiceMuteBtnClicked);
		base.AddGuiElement(this.muteVoice_btn);
		this.forDelete.Add(this.muteVoice_btn);
	}

	private void OnConfirmRestoreKeyboardOptionsClicked(object obj)
	{
		if ((int)((EventHandlerParam)obj).customData == 0)
		{
			if (NetworkScript.player == null || NetworkScript.player.shipScript == null || NetworkScript.player.shipScript.kb == null)
			{
				Debug.Log("Null reference error when trying to restore default shortcuts");
				return;
			}
			KeyboardShortcuts u003cu003ef_amu0024cache47 = this.keyboardInstance;
			if (__SystemWindow.<>f__am$cache47 == null)
			{
				__SystemWindow.<>f__am$cache47 = new Action(null, () => {
					if (AndromedaGui.mainWnd != null)
					{
						AndromedaGui.gui.RemoveWindow(AndromedaGui.mainWnd.feedbackButtonWindow.handler);
						AndromedaGui.mainWnd.CreateFeedbackButton();
					}
				});
			}
			u003cu003ef_amu0024cache47.OnKeyChangedAction = __SystemWindow.<>f__am$cache47;
			NetworkScript.player.shipScript.kb.RestoreDefaultShortcuts(true);
			this.OnControlsClick(null);
		}
		this.ignoreClickEvents = false;
		this.dlgConfirmSave.RemoveGUIItems();
	}

	public void OnControlsClick(object prm)
	{
		EventHandlerParam eventHandlerParam;
		this.ClearContent();
		this.mainScreenBG.SetTextureKeepSize("SystemWindow", "systemTab4");
		this.lbl_InfoBoxValue.text = StaticData.Translate("key_system_menu_controls_infobox");
		this.lbl_InfoBoxValue.Alignment = 0;
		this.btnRestoreToDefaults = new GuiButtonResizeable();
		this.btnRestoreToDefaults.boundries.set_x(32f);
		this.btnRestoreToDefaults.boundries.set_y(500f);
		this.btnRestoreToDefaults.boundries.set_width(170f);
		this.btnRestoreToDefaults.Caption = StaticData.Translate("key_system_menu_controls_restore_defaults");
		this.btnRestoreToDefaults.FontSize = 14;
		this.btnRestoreToDefaults.Alignment = 4;
		this.btnRestoreToDefaults.Clicked = new Action<EventHandlerParam>(this, __SystemWindow.ShowRestoreKeyboardDialog);
		this.btnRestoreToDefaults.SetSmallOrangeTexture();
		base.AddGuiElement(this.btnRestoreToDefaults);
		this.forDelete.Add(this.btnRestoreToDefaults);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(264f, 121f, 250f, 16f),
			text = StaticData.Translate("key_system_menu_controls_command"),
			FontSize = 16,
			Font = GuiLabel.FontBold,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(507f, 121f, 250f, 16f),
			text = string.Format("{0} 1", StaticData.Translate("key_system_menu_controls_key")),
			FontSize = 16,
			Font = GuiLabel.FontBold,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel1);
		this.forDelete.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(718f, 121f, 250f, 16f),
			text = string.Format("{0} 2", StaticData.Translate("key_system_menu_controls_key")),
			FontSize = 16,
			Font = GuiLabel.FontBold,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel2);
		this.forDelete.Add(guiLabel2);
		GuiTexture guiTexture = new GuiTexture()
		{
			X = 230f,
			Y = 160f
		};
		guiTexture.SetTexture("NewGUI", "GuildInfoSeparator");
		guiTexture.SetSize(648f, 5f);
		guiTexture.boundries.set_height(1f);
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		if (NetworkScript.player == null || NetworkScript.player.shipScript == null || NetworkScript.player.shipScript.kb == null)
		{
			Debug.Log("Unable to draw controls.Null reference");
			return;
		}
		this.keyboardInstance = NetworkScript.player.shipScript.kb;
		this.controlsScroller = new GuiScrollingContainer(235f, 180f, 648f, 330f, 1, this);
		if (this.controlsList != null)
		{
			this.controlsList.Clear();
		}
		else
		{
			this.controlsList = new List<GuiButtonResizeable>();
		}
		this.controlsScroller.SetArrowStep(38f);
		this.forDelete.Add(this.controlsScroller);
		base.AddGuiElement(this.controlsScroller);
		Dictionary<KeyboardCommand, ShortcutCommandItem> dictionary = NetworkScript.player.shipScript.kb.keyboardShortcuts;
		int num = 36;
		foreach (int key in dictionary.get_Keys())
		{
			if (key == 1)
			{
				GuiLabel guiLabel3 = new GuiLabel()
				{
					boundries = new Rect(0f, 8f, 140f, 16f),
					text = StaticData.Translate("key_system_menu_controls_movement"),
					FontSize = 16,
					Font = GuiLabel.FontBold,
					TextColor = GuiNewStyleBar.orangeColor
				};
				this.controlsScroller.AddContent(guiLabel3);
			}
			else if (key == 5)
			{
				GuiLabel guiLabel4 = new GuiLabel()
				{
					boundries = new Rect(0f, (float)num, 350f, 16f),
					text = StaticData.Translate("key_system_menu_controls_action"),
					FontSize = 16,
					Font = GuiLabel.FontBold,
					TextColor = GuiNewStyleBar.orangeColor,
					Alignment = 3
				};
				this.controlsScroller.AddContent(guiLabel4);
				num = num + 40;
			}
			else if (key == 17)
			{
				GuiLabel guiLabel5 = new GuiLabel()
				{
					boundries = new Rect(0f, (float)num, 350f, 16f),
					text = StaticData.Translate("key_system_menu_controls_skillbar"),
					FontSize = 16,
					Font = GuiLabel.FontBold,
					TextColor = GuiNewStyleBar.orangeColor,
					Alignment = 3
				};
				this.controlsScroller.AddContent(guiLabel5);
				num = num + 40;
			}
			else if (key == 26)
			{
				GuiLabel guiLabel6 = new GuiLabel()
				{
					boundries = new Rect(0f, (float)num, 350f, 16f),
					text = StaticData.Translate("key_system_menu_controls_menu"),
					FontSize = 16,
					Font = GuiLabel.FontBold,
					TextColor = GuiNewStyleBar.orangeColor,
					Alignment = 3
				};
				this.controlsScroller.AddContent(guiLabel6);
				num = num + 40;
			}
			else if (key == 41 && playWebGame.GAME_TYPE == "ru")
			{
				continue;
			}
			else if (key == 42)
			{
				GuiLabel guiLabel7 = new GuiLabel()
				{
					boundries = new Rect(0f, (float)num, 350f, 16f),
					text = StaticData.Translate("key_system_menu_controls_base"),
					FontSize = 16,
					Font = GuiLabel.FontBold,
					TextColor = GuiNewStyleBar.orangeColor,
					Alignment = 3
				};
				this.controlsScroller.AddContent(guiLabel7);
				num = num + 40;
			}
			else if (key == 9)
			{
				GuiButtonFixed guiButtonFixed = new GuiButtonFixed()
				{
					Caption = string.Empty
				};
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_keyboard_tooltip_actionkey"),
					customData2 = guiButtonFixed
				};
				guiButtonFixed.tooltipWindowParam = eventHandlerParam;
				guiButtonFixed.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				guiButtonFixed.isHoverAware = true;
				guiButtonFixed.SetTexture("NewGUI", "btnInfo");
				guiButtonFixed.X = 0f;
				guiButtonFixed.Y = (float)(num + 1);
				guiButtonFixed.boundries.set_width(20f);
				guiButtonFixed.boundries.set_height(20f);
				this.controlsScroller.AddContent(guiButtonFixed);
			}
			else if (key == 10)
			{
				GuiButtonFixed drawTooltipWindow = new GuiButtonFixed()
				{
					Caption = string.Empty
				};
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_keyboard_tooltip_usekey"),
					customData2 = drawTooltipWindow
				};
				drawTooltipWindow.tooltipWindowParam = eventHandlerParam;
				drawTooltipWindow.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				drawTooltipWindow.isHoverAware = true;
				drawTooltipWindow.SetTexture("NewGUI", "btnInfo");
				drawTooltipWindow.X = 0f;
				drawTooltipWindow.Y = (float)(num + 3);
				drawTooltipWindow.boundries.set_width(20f);
				drawTooltipWindow.boundries.set_height(20f);
				this.controlsScroller.AddContent(drawTooltipWindow);
			}
			ShortcutCommandItem item = dictionary.get_Item((KeyboardCommand)key);
			GuiLabel guiLabel8 = new GuiLabel()
			{
				boundries = new Rect(28f, (float)num, 160f, 30f),
				text = item.commandStringKey,
				FontSize = 14,
				Font = GuiLabel.FontBold,
				Alignment = 3
			};
			this.controlsScroller.AddContent(guiLabel8);
			string str = (item.keyCodeOne != null ? this.keyboardInstance.CheckKeyCodeName(item.keyCodeOne.ToString()) : "N/A");
			if (item.isKeyOneUsingShift && str != "N/A")
			{
				str = string.Concat("Shift + ", str);
			}
			if (item.isKeyOneUsingCtrl && str != "N/A")
			{
				str = string.Concat("Ctrl + ", str);
			}
			GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
			guiButtonResizeable.boundries.set_x(200f);
			guiButtonResizeable.boundries.set_y((float)num);
			guiButtonResizeable.boundries.set_width(180f);
			guiButtonResizeable.Caption = str;
			guiButtonResizeable.FontSize = 14;
			guiButtonResizeable.Alignment = 4;
			guiButtonResizeable.SetSmallBlueTexture();
			eventHandlerParam = new EventHandlerParam()
			{
				customData = 1000 + key,
				customData2 = guiButtonResizeable
			};
			guiButtonResizeable.eventHandlerParam = eventHandlerParam;
			guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this.keyboardInstance, KeyboardShortcuts.OnChangeKeyboardShortcutClicked);
			guiButtonResizeable.behaviourKeepClicked = true;
			guiButtonResizeable.textColorNormal = Color.get_white();
			this.controlsScroller.AddContent(guiButtonResizeable);
			string str1 = (item.keyCodeTwo != null ? this.keyboardInstance.CheckKeyCodeName(item.keyCodeTwo.ToString()) : "N/A");
			if (item.isKeyTwoUsingShift && str1 != "N/A")
			{
				str1 = string.Concat("Shift + ", str1);
			}
			if (item.isKeyTwoUsingCtrl && str1 != "N/A")
			{
				str1 = string.Concat("Ctrl + ", str1);
			}
			GuiButtonResizeable action = new GuiButtonResizeable();
			action.boundries.set_x(420f);
			action.boundries.set_y((float)num);
			action.boundries.set_width(180f);
			action.Caption = str1;
			action.FontSize = 14;
			action.Alignment = 4;
			action.SetSmallBlueTexture();
			eventHandlerParam = new EventHandlerParam()
			{
				customData = 2000 + key,
				customData2 = action
			};
			action.eventHandlerParam = eventHandlerParam;
			action.Clicked = new Action<EventHandlerParam>(this.keyboardInstance, KeyboardShortcuts.OnChangeKeyboardShortcutClicked);
			action.behaviourKeepClicked = true;
			action.textColorNormal = Color.get_white();
			this.controlsScroller.AddContent(action);
			if (key >= 5 && key <= 8 || key == 40)
			{
				guiButtonResizeable.isEnabled = false;
				if (key == 5 || key == 6 || key == 40)
				{
					action.isEnabled = false;
				}
				else if (key == 7 || key == 8)
				{
					guiButtonResizeable.Caption = StaticData.Translate("key_system_menu_controls_mouse_scroller");
				}
			}
			this.controlsList.Add(guiButtonResizeable);
			this.controlsList.Add(action);
			num = num + 40;
		}
	}

	private void OnExitNoClicked(EventHandlerParam parm)
	{
		if (NetworkScript.player.shipScript != null)
		{
			NetworkScript.player.shipScript.isGuiClosed = true;
		}
		AndromedaGui.mainWnd.CloseActiveWindow();
	}

	private void OnExitYesClicked(EventHandlerParam parm)
	{
		if (!playWebGame.isWebPlayer)
		{
			Application.Quit();
		}
		else
		{
			Application.OpenURL(playWebGame.authorization.url_logout);
		}
	}

	private void OnFxMinusBtnClicked(object obj)
	{
		if (this._sliderFx.CurrentValue > this._sliderFx.MIN)
		{
			GuiHorizontalSlider currentValue = this._sliderFx;
			currentValue.CurrentValue = currentValue.CurrentValue - 1f;
			this._sliderFx.CurrentValue = (this._sliderFx.CurrentValue >= 0f ? this._sliderFx.CurrentValue : 0f);
			this.curentFxVolume = this._sliderFx.CurrentValue / this._sliderFx.MAX;
			GuiFramework.fxVolume = this.curentFxVolume;
		}
		if (this.isFxSoundMute)
		{
			this.isFxSoundMute = false;
			this.muteFxIcon.SetTexture("SystemWindow", "muteIconOn");
			this.muteFxLbl.TextColor = Color.get_white();
			GuiFramework.fxVolume = this.curentFxVolume;
			this.barFxVolume.SetCustumSize(405, Color.get_white());
		}
	}

	private void OnFxMuteBtnClicked(object obj)
	{
		if (!this.isFxSoundMute)
		{
			this.isFxSoundMute = true;
			this.muteFxIcon.SetTexture("SystemWindow", "muteIconOff");
			this.muteFxLbl.TextColor = Color.get_red();
			GuiFramework.fxVolume = 0f;
			this._sliderFx.CurrentValue = 0f;
			this.barFxVolume.SetCustumSize(405, Color.get_red());
		}
		else
		{
			this.isFxSoundMute = false;
			this.muteFxIcon.SetTexture("SystemWindow", "muteIconOn");
			this.muteFxLbl.TextColor = Color.get_white();
			GuiFramework.fxVolume = 1f;
			this._sliderFx.CurrentValue = 100f;
			this.barFxVolume.SetCustumSize(405, Color.get_white());
		}
	}

	private void OnFxPlusBtnClicked(object obj)
	{
		if (this._sliderFx.CurrentValue < this._sliderFx.MAX)
		{
			GuiHorizontalSlider currentValue = this._sliderFx;
			currentValue.CurrentValue = currentValue.CurrentValue + 1f;
			this._sliderFx.CurrentValue = (this._sliderFx.CurrentValue <= this._sliderFx.MAX ? this._sliderFx.CurrentValue : this._sliderFx.MAX);
			this.curentFxVolume = this._sliderFx.CurrentValue / this._sliderFx.MAX;
			GuiFramework.fxVolume = this.curentFxVolume;
		}
		if (this.isFxSoundMute)
		{
			this.isFxSoundMute = false;
			this.muteFxIcon.SetTexture("SystemWindow", "muteIconOn");
			this.muteFxLbl.TextColor = Color.get_white();
			GuiFramework.fxVolume = this.curentFxVolume;
			this.barFxVolume.SetCustumSize(405, Color.get_white());
		}
	}

	private void OnMasterMinusBtnClicked(object obj)
	{
		if (this._sliderMaster.CurrentValue > this._sliderMaster.MIN)
		{
			GuiHorizontalSlider currentValue = this._sliderMaster;
			currentValue.CurrentValue = currentValue.CurrentValue - 1f;
			this._sliderMaster.CurrentValue = (this._sliderMaster.CurrentValue >= 0f ? this._sliderMaster.CurrentValue : 0f);
			this.curentMasterVolume = this._sliderMaster.CurrentValue / this._sliderMaster.MAX;
			GuiFramework.masterVolume = this.curentMasterVolume;
		}
		if (this.isMasterSoundMute)
		{
			this.isMasterSoundMute = false;
			this.muteMasterIcon.SetTexture("SystemWindow", "muteIconOn");
			this.muteMasterLbl.TextColor = Color.get_white();
			GuiFramework.masterVolume = this.curentMasterVolume;
			this.barMasterVolume.SetCustumSize(405, Color.get_white());
		}
		AudioManager.SetVolume(GuiFramework.masterVolume * GuiFramework.musicVolume);
	}

	private void OnMasterMuteBtnClicked(object obj)
	{
		if (!this.isMasterSoundMute)
		{
			this.isMasterSoundMute = true;
			this.muteMasterIcon.SetTexture("SystemWindow", "muteIconOff");
			this.muteMasterLbl.TextColor = Color.get_red();
			GuiFramework.masterVolume = 0f;
			this._sliderMaster.CurrentValue = 0f;
			this.barMasterVolume.SetCustumSize(405, Color.get_red());
		}
		else
		{
			this.isMasterSoundMute = false;
			this.muteMasterIcon.SetTexture("SystemWindow", "muteIconOn");
			this.muteMasterLbl.TextColor = Color.get_white();
			GuiFramework.masterVolume = 1f;
			this._sliderMaster.CurrentValue = 100f;
			this.barMasterVolume.SetCustumSize(405, Color.get_white());
		}
		AudioManager.SetVolume(GuiFramework.masterVolume * GuiFramework.musicVolume);
	}

	private void OnMasterPlusBtnClicked(object obj)
	{
		if (this._sliderMaster.CurrentValue < this._sliderMaster.MAX)
		{
			GuiHorizontalSlider currentValue = this._sliderMaster;
			currentValue.CurrentValue = currentValue.CurrentValue + 1f;
			this._sliderMaster.CurrentValue = (this._sliderMaster.CurrentValue <= this._sliderMaster.MAX ? this._sliderMaster.CurrentValue : this._sliderMaster.MAX);
			this.curentMasterVolume = this._sliderMaster.CurrentValue / this._sliderMaster.MAX;
			GuiFramework.masterVolume = this.curentMasterVolume;
		}
		if (this.isMasterSoundMute)
		{
			this.isMasterSoundMute = false;
			this.muteMasterIcon.SetTexture("SystemWindow", "muteIconOn");
			this.muteMasterLbl.TextColor = Color.get_white();
			GuiFramework.masterVolume = this.curentMasterVolume;
			this.barMasterVolume.SetCustumSize(405, Color.get_white());
		}
		AudioManager.SetVolume(GuiFramework.masterVolume * GuiFramework.musicVolume);
	}

	private void OnMusicMinusBtnClicked(object obj)
	{
		if (this._sliderMusic.CurrentValue > this._sliderMusic.MIN)
		{
			GuiHorizontalSlider currentValue = this._sliderMusic;
			currentValue.CurrentValue = currentValue.CurrentValue - 1f;
			this._sliderMusic.CurrentValue = (this._sliderMusic.CurrentValue >= 0f ? this._sliderMusic.CurrentValue : 0f);
			this.curentMusicVolume = this._sliderMusic.CurrentValue / this._sliderMusic.MAX;
			GuiFramework.musicVolume = this.curentMusicVolume;
		}
		if (this.isMusicSoundMute)
		{
			this.isMusicSoundMute = false;
			this.muteMusicIcon.SetTexture("SystemWindow", "muteIconOn");
			this.muteMusicLbl.TextColor = Color.get_white();
			GuiFramework.musicVolume = this.curentMusicVolume;
			this.barMusicVolume.SetCustumSize(405, Color.get_white());
		}
		AudioManager.SetVolume(GuiFramework.masterVolume * GuiFramework.musicVolume);
	}

	private void OnMusicMuteBtnClicked(object obj)
	{
		if (!this.isMusicSoundMute)
		{
			this.isMusicSoundMute = true;
			this.muteMusicIcon.SetTexture("SystemWindow", "muteIconOff");
			this.muteMusicLbl.TextColor = Color.get_red();
			GuiFramework.musicVolume = 0f;
			this._sliderMusic.CurrentValue = 0f;
			this.barMusicVolume.SetCustumSize(405, Color.get_red());
		}
		else
		{
			this.isMusicSoundMute = false;
			this.muteMusicIcon.SetTexture("SystemWindow", "muteIconOn");
			this.muteMusicLbl.TextColor = Color.get_white();
			GuiFramework.musicVolume = 0.8f;
			this._sliderMusic.CurrentValue = 80f;
			this.barMusicVolume.SetCustumSize(405, Color.get_white());
		}
		AudioManager.SetVolume(GuiFramework.masterVolume * GuiFramework.musicVolume);
	}

	private void OnMusicPlusBtnClicked(object obj)
	{
		if (this._sliderMusic.CurrentValue < this._sliderMusic.MAX)
		{
			GuiHorizontalSlider currentValue = this._sliderMusic;
			currentValue.CurrentValue = currentValue.CurrentValue + 1f;
			this._sliderMusic.CurrentValue = (this._sliderMusic.CurrentValue <= this._sliderMusic.MAX ? this._sliderMusic.CurrentValue : this._sliderMusic.MAX);
			this.curentMusicVolume = this._sliderMusic.CurrentValue / this._sliderMusic.MAX;
			GuiFramework.musicVolume = this.curentMusicVolume;
		}
		if (this.isMusicSoundMute)
		{
			this.isMusicSoundMute = false;
			this.muteMusicIcon.SetTexture("SystemWindow", "muteIconOn");
			this.muteMusicLbl.TextColor = Color.get_white();
			GuiFramework.musicVolume = this.curentMusicVolume;
			this.barMusicVolume.SetCustumSize(405, Color.get_white());
		}
		AudioManager.SetVolume(GuiFramework.masterVolume * GuiFramework.musicVolume);
	}

	private void OnSaveExitClick(object prm)
	{
		this.ClearContent();
		this.mainScreenBG.SetTextureKeepSize("SystemWindow", "systemTab5");
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(403f, 213f, 290f, 100f),
			text = StaticData.Translate("key_system_menu_logoff_question"),
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 14,
			Font = GuiLabel.FontBold,
			Alignment = 4
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.SetOrangeTexture();
		guiButtonResizeable.X = 420f;
		guiButtonResizeable.Y = 320f;
		guiButtonResizeable.boundries.set_width(120f);
		guiButtonResizeable.Caption = StaticData.Translate("key_system_menu_logoff_yes").ToUpper();
		guiButtonResizeable.Alignment = 4;
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, __SystemWindow.OnExitYesClicked);
		base.AddGuiElement(guiButtonResizeable);
		this.forDelete.Add(guiButtonResizeable);
		GuiButtonResizeable str = new GuiButtonResizeable();
		str.SetBlueTexture();
		str.X = 560f;
		str.Y = 320f;
		str.boundries.set_width(120f);
		str.Caption = StaticData.Translate("key_system_menu_logoff_no").ToString();
		str.Alignment = 4;
		str.Clicked = new Action<EventHandlerParam>(this, __SystemWindow.OnExitNoClicked);
		base.AddGuiElement(str);
		this.forDelete.Add(str);
	}

	private void OnSkillBarClick(object prm)
	{
		__SystemWindow.<OnSkillBarClick>c__AnonStoreyA5 variable = null;
		this.ClearContent();
		this.mainScreenBG.SetTextureKeepSize("SystemWindow", "systemTab1");
		this.lbl_InfoBoxValue.text = StaticData.Translate("key_system_menu_infobox_skill");
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(280f, 240f, 520f, 36f),
			text = StaticData.Translate("key_system_menu_rearange_info"),
			FontSize = 16,
			Alignment = 4
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiTexture[] guiTexture = new GuiTexture[21];
		GuiTextureDraggable[] guiTextureDraggableArray = new GuiTextureDraggable[21];
		for (int i = 0; i < 21; i++)
		{
			if (i >= 9)
			{
				guiTexture[i] = new GuiTexture();
				guiTexture[i].boundries.set_x((float)(380 + (i - 9) % 6 * 59));
				guiTexture[i].boundries.set_y((float)(435 + 55 * ((i - 9) / 6)));
				guiTexture[i].SetTexture("SystemWindow", "skills_box");
				base.AddGuiElement(guiTexture[i]);
				this.forDelete.Add(guiTexture[i]);
			}
			else
			{
				guiTexture[i] = new GuiTexture();
				guiTexture[i].boundries.set_x((float)(292 + i * 59));
				guiTexture[i].boundries.set_y(302f);
				guiTexture[i].SetTexture("SystemWindow", "skills_box");
				base.AddGuiElement(guiTexture[i]);
				this.forDelete.Add(guiTexture[i]);
			}
			ActiveSkillSlot item = null;
			if (NetworkScript.player.playerBelongings.skillConfig.skillSlots.ContainsKey(i))
			{
				item = NetworkScript.player.playerBelongings.skillConfig.skillSlots.get_Item(i);
			}
			if (item == null)
			{
				guiTextureDraggableArray[i] = null;
			}
			else
			{
				TalentsInfo talentsInfo = (TalentsInfo)Enumerable.First<PlayerItemTypesData>(Enumerable.Where<PlayerItemTypesData>(StaticData.allTypes.get_Values(), new Func<PlayerItemTypesData, bool>(variable, (PlayerItemTypesData t) => t.itemType == this.slt.skillId)));
				string str = talentsInfo.assetName;
				GuiTextureDraggable guiTextureDraggable = new GuiTextureDraggable()
				{
					drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(this.DrawTalentTooltip),
					tooltipWindowParam = new TalentTooltipInfo(new Vector2(guiTexture[i].X, guiTexture[i].Y), talentsInfo),
					callbackAttribute = i
				};
				guiTextureDraggable.SetTextureSource("SystemWindow", "skills_boxSrc");
				guiTextureDraggable.SetTextureDrag("Skills", str);
				guiTextureDraggable.SetTexture((Texture2D)playWebGame.assets.GetFromStaticSet("Skills", str));
				guiTextureDraggable.SetTextureDropZone("SystemWindow", "skills_boxDrp");
				guiTextureDraggable.SetSize(36f, 36f);
				guiTextureDraggable.boundries.set_x(guiTexture[i].X);
				guiTextureDraggable.boundries.set_y(guiTexture[i].Y);
				guiTextureDraggable.mainTextureOffsetY = 0;
				guiTextureDraggable.mainTextureOffsetX = 2;
				guiTextureDraggable.dropped = new Action<int, int>(this, __SystemWindow.OnSkillDropped);
				guiTextureDraggableArray[i] = guiTextureDraggable;
				base.AddGuiElement(guiTextureDraggable);
				this.forDelete.Add(guiTextureDraggable);
			}
			GuiLabel guiLabel1 = new GuiLabel()
			{
				boundries = new Rect(guiTexture[i].X + 2f, guiTexture[i].Y + 28f, 30f, 15f),
				FontSize = 9,
				text = (i + 1).ToString()
			};
			base.AddGuiElement(guiLabel1);
			this.forDelete.Add(guiLabel1);
		}
		Texture2D fromStaticSet = (Texture2D)playWebGame.assets.GetFromStaticSet("SystemWindow", "skills_boxTrg");
		Texture2D texture2D = (Texture2D)playWebGame.assets.GetFromStaticSet("SystemWindow", "skills_boxDrp");
		for (int j = 0; j < (int)guiTexture.Length; j++)
		{
			if (guiTextureDraggableArray[j] != null)
			{
				for (int k = 0; k < (int)guiTexture.Length; k++)
				{
					if (j != k)
					{
						GuiTexture guiTexture1 = guiTexture[k];
						int num = k;
						guiTextureDraggableArray[j].AddDropZone(new Vector2(guiTexture1.boundries.get_x(), guiTexture1.boundries.get_y()), num, fromStaticSet, texture2D);
					}
				}
			}
		}
	}

	private void OnSkillDropped(int a, int b)
	{
		// 
		// Current member / type: System.Void __SystemWindow::OnSkillDropped(System.Int32,System.Int32)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnSkillDropped(System.Int32,System.Int32)
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..( , Int32 , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:строка 78
		//    в ..( , Int32& , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:строка 33
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 57
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 33
		//    в ..(MethodBody ,  , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 88
		//    в ..(MethodBody , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 70
		//    в Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 95
		//    в Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 58
		//    в ..(ILanguage , MethodDefinition ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:строка 117
		// 
		// mailto: JustDecompilePublicFeedback@telerik.com

	}

	private void OnVideoClick(object prm)
	{
		this.ClearContent();
		this.mainScreenBG.SetTextureKeepSize("SystemWindow", "systemTab3");
		this.lbl_InfoBoxValue.text = StaticData.Translate("key_system_menu_infobox_video");
		this.lblAntialiasing = new GuiLabel()
		{
			boundries = new Rect(295f, 280f, 250f, 20f),
			Alignment = 3,
			FontSize = 13,
			text = StaticData.Translate("key_system_menu_video_antialiasing")
		};
		base.AddGuiElement(this.lblAntialiasing);
		this.forDelete.Add(this.lblAntialiasing);
		this.dropdownAntialiasing = new GuiDropdown()
		{
			DisableDrawBackwards = true,
			X = this.lblAntialiasing.X,
			Y = this.lblAntialiasing.Y + 25f,
			Width = 180f
		};
		if (QualitySettings.get_antiAliasing() != 0)
		{
			this.dropdownAntialiasing.AddItem(StaticData.Translate("key_system_menu_video_antialiasing_off"));
		}
		else
		{
			this.dropdownAntialiasing.AddItem(StaticData.Translate("key_system_menu_video_antialiasing_off"), true);
		}
		if (QualitySettings.get_antiAliasing() != 2)
		{
			this.dropdownAntialiasing.AddItem(StaticData.Translate("key_system_menu_video_antialiasing_2"));
		}
		else
		{
			this.dropdownAntialiasing.AddItem(StaticData.Translate("key_system_menu_video_antialiasing_2"), true);
		}
		if (QualitySettings.get_antiAliasing() != 4)
		{
			this.dropdownAntialiasing.AddItem(StaticData.Translate("key_system_menu_video_antialiasing_4"));
		}
		else
		{
			this.dropdownAntialiasing.AddItem(StaticData.Translate("key_system_menu_video_antialiasing_4"), true);
		}
		if (QualitySettings.get_antiAliasing() != 8)
		{
			this.dropdownAntialiasing.AddItem(StaticData.Translate("key_system_menu_video_antialiasing_8"));
		}
		else
		{
			this.dropdownAntialiasing.AddItem(StaticData.Translate("key_system_menu_video_antialiasing_8"), true);
		}
		this.dropdownAntialiasing.OnItemSelected = new Action<int>(this, __SystemWindow.AntialiasingSelected);
		base.AddGuiElement(this.dropdownAntialiasing);
		this.forDelete.Add(this.dropdownAntialiasing);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(295f, 220f, 250f, 20f),
			Alignment = 3,
			FontSize = 13,
			text = StaticData.Translate("key_system_menu_video_quality").ToUpper()
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiDropdown guiDropdown = new GuiDropdown()
		{
			X = guiLabel.X,
			Y = guiLabel.Y + 25f,
			Width = 180f,
			OnItemSelected = new Action<int>(this, __SystemWindow.QualitySelected)
		};
		base.AddGuiElement(guiDropdown);
		this.forDelete.Add(guiDropdown);
		string[] _names = QualitySettings.get_names();
		for (int i = 0; i < (int)_names.Length; i++)
		{
			if (i != QualitySettings.GetQualityLevel())
			{
				guiDropdown.AddItem(StaticData.Translate(_names[i]));
			}
			else
			{
				guiDropdown.AddItem(StaticData.Translate(_names[i]), true);
			}
		}
		this.lblResolution = new GuiLabel()
		{
			boundries = new Rect(295f, 160f, 250f, 20f),
			Alignment = 3,
			FontSize = 13,
			text = StaticData.Translate("key_system_menu_video_resolution").ToUpper()
		};
		base.AddGuiElement(this.lblResolution);
		this.forDelete.Add(this.lblResolution);
		this.dropdownResolution = new GuiDropdown()
		{
			X = this.lblResolution.X,
			Y = this.lblResolution.Y + 25f,
			Width = 180f
		};
		this.resolutions = Enumerable.ToArray<Resolution>(Enumerable.Take<Resolution>(Enumerable.Reverse<Resolution>(Screen.get_resolutions()), 12));
		List<Resolution> list = new List<Resolution>(this.resolutions);
		list.Reverse();
		this.resolutions = list.ToArray();
		Resolution[] resolutionArray = this.resolutions;
		for (int j = 0; j < (int)resolutionArray.Length; j++)
		{
			Resolution resolution = resolutionArray[j];
			if (resolution.get_width() != Screen.get_width() || resolution.get_height() != Screen.get_height())
			{
				this.dropdownResolution.AddItem(string.Format(StaticData.Translate("key_system_menu_video_resolution_list"), resolution.get_width(), resolution.get_height(), resolution.get_refreshRate()));
			}
			else
			{
				this.dropdownResolution.AddItem(string.Format(StaticData.Translate("key_system_menu_video_resolution_list"), resolution.get_width(), resolution.get_height(), resolution.get_refreshRate()), true);
			}
		}
		this.dropdownResolution.OnItemSelected = new Action<int>(this, __SystemWindow.ResolutionSelected);
		base.AddGuiElement(this.dropdownResolution);
		this.forDelete.Add(this.dropdownResolution);
		this.lblFullscreen = new GuiLabel()
		{
			boundries = new Rect(510f, 185f, 200f, 23f),
			Alignment = 3,
			FontSize = 13,
			text = StaticData.Translate("key_system_menu_video_fullscreen")
		};
		base.AddGuiElement(this.lblFullscreen);
		this.forDelete.Add(this.lblFullscreen);
		this.checkboxFullscreen = new GuiCheckbox()
		{
			X = this.lblFullscreen.X - 25f,
			Y = this.lblFullscreen.Y,
			OnCheckboxSelected = new Action<bool>(this, __SystemWindow.FullscreenSelected),
			Selected = Screen.get_fullScreen(),
			isActive = true
		};
		base.AddGuiElement(this.checkboxFullscreen);
		this.forDelete.Add(this.checkboxFullscreen);
	}

	private void OnVoiceMinusBtnClicked(object obj)
	{
		if (this._sliderVoice.CurrentValue > this._sliderVoice.MIN)
		{
			GuiHorizontalSlider currentValue = this._sliderVoice;
			currentValue.CurrentValue = currentValue.CurrentValue - 1f;
			this._sliderVoice.CurrentValue = (this._sliderVoice.CurrentValue >= 0f ? this._sliderVoice.CurrentValue : 0f);
			this.curentVoiceVolume = this._sliderVoice.CurrentValue / this._sliderVoice.MAX;
			GuiFramework.voiceVolume = this.curentVoiceVolume;
		}
		if (this.isVoiceSoundMute)
		{
			this.isVoiceSoundMute = false;
			this.muteVoiceIcon.SetTexture("SystemWindow", "muteIconOn");
			this.muteVoiceLbl.TextColor = Color.get_white();
			GuiFramework.fxVolume = this.curentVoiceVolume;
			this.barVoiceVolume.SetCustumSize(405, Color.get_white());
		}
	}

	private void OnVoiceMuteBtnClicked(object obj)
	{
		if (!this.isVoiceSoundMute)
		{
			this.isVoiceSoundMute = true;
			this.muteVoiceIcon.SetTexture("SystemWindow", "muteIconOff");
			this.muteVoiceLbl.TextColor = Color.get_red();
			GuiFramework.voiceVolume = 0f;
			this._sliderVoice.CurrentValue = 0f;
			this.barVoiceVolume.SetCustumSize(405, Color.get_red());
		}
		else
		{
			this.isVoiceSoundMute = false;
			this.muteVoiceIcon.SetTexture("SystemWindow", "muteIconOn");
			this.muteVoiceLbl.TextColor = Color.get_white();
			GuiFramework.voiceVolume = 1f;
			this._sliderVoice.CurrentValue = 100f;
			this.barVoiceVolume.SetCustumSize(405, Color.get_white());
		}
	}

	private void OnVoicePlusBtnClicked(object obj)
	{
		if (this._sliderVoice.CurrentValue < this._sliderVoice.MAX)
		{
			GuiHorizontalSlider currentValue = this._sliderVoice;
			currentValue.CurrentValue = currentValue.CurrentValue + 1f;
			this._sliderVoice.CurrentValue = (this._sliderVoice.CurrentValue <= this._sliderVoice.MAX ? this._sliderVoice.CurrentValue : this._sliderVoice.MAX);
			this.curentVoiceVolume = this._sliderVoice.CurrentValue / this._sliderVoice.MAX;
			GuiFramework.voiceVolume = this.curentVoiceVolume;
		}
		if (this.isVoiceSoundMute)
		{
			this.isVoiceSoundMute = false;
			this.muteVoiceIcon.SetTexture("SystemWindow", "muteIconOn");
			this.muteVoiceLbl.TextColor = Color.get_white();
			GuiFramework.voiceVolume = this.curentVoiceVolume;
			this.barVoiceVolume.SetCustumSize(405, Color.get_white());
		}
	}

	private void QualitySelected(int i)
	{
		QualitySettings.SetQualityLevel(i, false);
		if (i != 0)
		{
			Camera.get_main().set_renderingPath(2);
		}
		else
		{
			Camera.get_main().set_renderingPath(0);
		}
	}

	private void RefreshFxSlider()
	{
		if (this.isFxSoundMute && this._sliderFx.CurrentValue / this._sliderFx.MAX != 0f)
		{
			this.isFxSoundMute = false;
			this.muteFxIcon.SetTexture("SystemWindow", "muteIconOn");
			this.muteFxLbl.TextColor = Color.get_white();
			this.curentFxVolume = this._sliderFx.CurrentValue / this._sliderFx.MAX;
			this.barFxVolume.SetCustumSize(405, Color.get_white());
		}
		this.barFxVolume.current = (float)this._sliderFx.CurrentValueInt;
		this.curentFxVolume = this._sliderFx.CurrentValue / this._sliderFx.MAX;
		GuiFramework.fxVolume = this.curentFxVolume;
	}

	private void RefreshMasterSlider()
	{
		if (this.isMasterSoundMute && this._sliderMaster.CurrentValue / this._sliderMaster.MAX != 0f)
		{
			this.isMasterSoundMute = false;
			this.muteMasterIcon.SetTexture("SystemWindow", "muteIconOn");
			this.muteMasterLbl.TextColor = Color.get_white();
			this.curentMasterVolume = this._sliderMaster.CurrentValue / this._sliderMaster.MAX;
			this.barMasterVolume.SetCustumSize(405, Color.get_white());
		}
		this.barMasterVolume.current = (float)this._sliderMaster.CurrentValueInt;
		this.curentMasterVolume = this._sliderMaster.CurrentValue / this._sliderMaster.MAX;
		GuiFramework.masterVolume = this.curentMasterVolume;
		AudioManager.SetVolume(GuiFramework.masterVolume * GuiFramework.musicVolume);
	}

	private void RefreshMusicSlider()
	{
		if (this.isMusicSoundMute && this._sliderMusic.CurrentValue / this._sliderMusic.MAX != 0f)
		{
			this.isMusicSoundMute = false;
			this.muteMusicIcon.SetTexture("SystemWindow", "muteIconOn");
			this.muteMusicLbl.TextColor = Color.get_white();
			this.curentMusicVolume = this._sliderMusic.CurrentValue / this._sliderMusic.MAX;
			this.barMusicVolume.SetCustumSize(405, Color.get_white());
		}
		this.barMusicVolume.current = (float)this._sliderMusic.CurrentValueInt;
		this.curentMusicVolume = this._sliderMusic.CurrentValue / this._sliderMusic.MAX;
		GuiFramework.musicVolume = this.curentMusicVolume;
		AudioManager.SetVolume(GuiFramework.masterVolume * GuiFramework.musicVolume);
	}

	private void RefreshVoiceSlider()
	{
		if (this.isVoiceSoundMute && this._sliderVoice.CurrentValue / this._sliderVoice.MAX != 0f)
		{
			this.isVoiceSoundMute = false;
			this.muteVoiceIcon.SetTexture("SystemWindow", "muteIconOn");
			this.muteVoiceLbl.TextColor = Color.get_white();
			this.curentVoiceVolume = this._sliderVoice.CurrentValue / this._sliderVoice.MAX;
			this.barVoiceVolume.SetCustumSize(405, Color.get_white());
		}
		this.barVoiceVolume.current = (float)this._sliderVoice.CurrentValueInt;
		this.curentVoiceVolume = this._sliderVoice.CurrentValue / this._sliderVoice.MAX;
		GuiFramework.voiceVolume = this.curentVoiceVolume;
	}

	private void ResolutionSelected(int i)
	{
		Screen.SetResolution(this.resolutions[i].get_width(), this.resolutions[i].get_height(), Screen.get_fullScreen());
	}

	private void SetNewVolumes()
	{
		// 
		// Current member / type: System.Void __SystemWindow::SetNewVolumes()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void SetNewVolumes()
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..( , Int32 , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:строка 78
		//    в ..( , Int32& , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:строка 33
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 57
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 33
		//    в ..(MethodBody ,  , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 88
		//    в ..(MethodBody , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 70
		//    в Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 95
		//    в Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 58
		//    в ..(ILanguage , MethodDefinition ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:строка 117
		// 
		// mailto: JustDecompilePublicFeedback@telerik.com

	}

	private void ShowRestoreKeyboardDialog(EventHandlerParam prm)
	{
		NetworkScript.player.shipScript.kb.LeaveOldKey(null);
		if (this.dlgConfirmSave != null)
		{
			this.dlgConfirmSave.RemoveGUIItems();
		}
		this.dlgConfirmSave = new GuiDialog();
		this.dlgConfirmSave.Create(StaticData.Translate("key_system_menu_controls_restore_confirm"), StaticData.Translate("key_system_controls_yes"), StaticData.Translate("key_system_controls_no"), null);
		this.dlgConfirmSave.OkClicked = new Action<object>(this, __SystemWindow.OnConfirmRestoreKeyboardOptionsClicked);
		GuiButtonResizeable guiButtonResizeable = this.dlgConfirmSave.btnOK;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = 0
		};
		guiButtonResizeable.eventHandlerParam = eventHandlerParam;
		this.dlgConfirmSave.CancelClicked = new Action<object>(this, __SystemWindow.OnConfirmRestoreKeyboardOptionsClicked);
		GuiButtonResizeable guiButtonResizeable1 = this.dlgConfirmSave.btnCancel;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = 1
		};
		guiButtonResizeable1.eventHandlerParam = eventHandlerParam;
		this.ignoreClickEvents = true;
	}

	private void VSyncSelected(bool state)
	{
		if (state)
		{
			QualitySettings.set_vSyncCount(1);
		}
		else
		{
			QualitySettings.set_vSyncCount(0);
		}
	}

	private enum SystemWindowTab
	{
		SkillBar,
		Audio,
		Video,
		Controls,
		SaveExit
	}
}