using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PartyWindow
{
	private const int MAX_BAR_SIZE = 46;

	private const int SELF_INDEX = -1;

	private const float MAX_SHIELD_WIDTH = 156f;

	private const float MAX_CORPUS_WIDTH = 103f;

	private const float MAX_CRITICAL_WIDTH = 90f;

	private List<GuiElement> inviteeElementForDelete = new List<GuiElement>();

	private List<GuiElement> partyMemberElementForDelete = new List<GuiElement>();

	private List<GuiElement> partyOfferElementForDelete = new List<GuiElement>();

	private GuiWindow mainPartyWindow;

	private GuiWindow optionsMenuWindow;

	private GuiWindow mainPartyWindowBackground;

	private GuiWindow partyInvitationWindow;

	private GuiTexture ruleFrame;

	private GuiTexture ruleIcon;

	private GuiTexture selfLeaderIcon;

	private GuiTexture optionMenuHighlight;

	private long optionSelectedPlayer;

	private long selfPlayerId;

	private long selectedPartyOfferPlayerId;

	private bool isPartyWindowNeeded;

	private bool isInParty;

	private bool isPartyLeader;

	private GuiWindow personalStatsWindow;

	private GuiTexture personalStatsShield;

	private GuiTexture personalStatsCorpus;

	private GuiTexture personalStatsCritical;

	private GuiTexture personalStatsAvatar;

	private GuiLabel shieldValueShadow;

	private GuiLabel corpusValueShadow;

	private GuiLabel criticalValueShadow;

	private GuiLabel shieldValue;

	private GuiLabel corpusValue;

	private GuiLabel criticalValue;

	private GuiLabel personalStatsName;

	private GuiLabel personalStatsLevel;

	private bool isPersonalStatsHoverd;

	private bool lastState;

	public PartyWindow()
	{
	}

	private void AddCancelButton(long playerId, int index)
	{
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("PartyGUI", "optionButton");
		guiButtonFixed.X = 5f;
		guiButtonFixed.Y = (float)(5 + index * 42);
		guiButtonFixed.Caption = StaticData.Translate("key_party_option_cancel");
		guiButtonFixed.eventHandlerParam.customData = playerId;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, PartyWindow.OnCancelInvitationClicked);
		guiButtonFixed._marginLeft = 25;
		guiButtonFixed.MarginTop = -2;
		guiButtonFixed.FontSize = 14;
		guiButtonFixed.Alignment = 3;
		this.optionsMenuWindow.AddGuiElement(guiButtonFixed);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("PartyGUI", "optionX");
		guiTexture.X = guiButtonFixed.X + 5f;
		guiTexture.Y = guiButtonFixed.Y + 8f;
		this.optionsMenuWindow.AddGuiElement(guiTexture);
	}

	private void AddChangeRuleButton(long playerId, int index)
	{
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("PartyGUI", "optionButton");
		guiButtonFixed.X = 5f;
		guiButtonFixed.Y = (float)(5 + index * 42);
		guiButtonFixed.Caption = StaticData.Translate("key_party_option_change_rule");
		guiButtonFixed.eventHandlerParam.customData = playerId;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, PartyWindow.OnPartyRuleChangeClicked);
		guiButtonFixed._marginLeft = 25;
		guiButtonFixed.MarginTop = -2;
		guiButtonFixed.FontSize = 14;
		guiButtonFixed.Alignment = 3;
		this.optionsMenuWindow.AddGuiElement(guiButtonFixed);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("PartyGUI", "optionLootFK");
		guiTexture.X = guiButtonFixed.X + 5f;
		guiTexture.Y = guiButtonFixed.Y + 8f;
		this.optionsMenuWindow.AddGuiElement(guiTexture);
		if (NetworkScript.party.rules == null)
		{
			guiTexture.SetTexture("PartyGUI", "optionLootRR");
		}
	}

	private void AddKickButton(long playerId, int index)
	{
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("PartyGUI", "optionButton");
		guiButtonFixed.X = 5f;
		guiButtonFixed.Y = (float)(5 + index * 42);
		guiButtonFixed.Caption = (playerId != this.selfPlayerId ? StaticData.Translate("key_party_option_kick") : StaticData.Translate("key_party_option_leave"));
		guiButtonFixed.eventHandlerParam.customData = playerId;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, PartyWindow.OnKickPlayerClicked);
		guiButtonFixed._marginLeft = 25;
		guiButtonFixed.MarginTop = -2;
		guiButtonFixed.FontSize = 14;
		guiButtonFixed.Alignment = 3;
		this.optionsMenuWindow.AddGuiElement(guiButtonFixed);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("PartyGUI", "optionX");
		guiTexture.X = guiButtonFixed.X + 5f;
		guiTexture.Y = guiButtonFixed.Y + 8f;
		this.optionsMenuWindow.AddGuiElement(guiTexture);
	}

	private void AddOptionMenuButtons(long playerId)
	{
		PartyWindow.<AddOptionMenuButtons>c__AnonStorey57 variable = null;
		if (this.isInParty && Enumerable.FirstOrDefault<PartyMemberClientSide>(Enumerable.Where<PartyMemberClientSide>(NetworkScript.party.members, new Func<PartyMemberClientSide, bool>(variable, (PartyMemberClientSide t) => t.playerId == this.playerId))) != null)
		{
			this.AddSelectButton(playerId, 0);
			if (this.optionSelectedPlayer != this.selfPlayerId)
			{
				if (this.isPartyLeader)
				{
					this.AddPromoteButton(playerId, 1);
					this.AddKickButton(playerId, 2);
				}
			}
			else if (!this.isPartyLeader)
			{
				this.AddKickButton(playerId, 1);
			}
			else
			{
				this.AddChangeRuleButton(playerId, 1);
				this.AddKickButton(playerId, 2);
			}
		}
		else if (playerId != this.selfPlayerId)
		{
			this.AddCancelButton(playerId, 0);
		}
		else
		{
			this.AddSelectButton(playerId, 0);
		}
	}

	private void AddPromoteButton(long playerId, int index)
	{
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("PartyGUI", "optionButton");
		guiButtonFixed.X = 5f;
		guiButtonFixed.Y = (float)(5 + index * 42);
		guiButtonFixed.Caption = StaticData.Translate("key_party_option_promote");
		guiButtonFixed.eventHandlerParam.customData = playerId;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, PartyWindow.OnPromoteClickd);
		guiButtonFixed._marginLeft = 25;
		guiButtonFixed.MarginTop = -2;
		guiButtonFixed.FontSize = 14;
		guiButtonFixed.Alignment = 3;
		this.optionsMenuWindow.AddGuiElement(guiButtonFixed);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("PartyGUI", "optionLeader");
		guiTexture.X = guiButtonFixed.X + 5f;
		guiTexture.Y = guiButtonFixed.Y + 8f;
		this.optionsMenuWindow.AddGuiElement(guiTexture);
	}

	private void AddSelectButton(long playerId, int index)
	{
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("PartyGUI", "optionButton");
		guiButtonFixed.X = 5f;
		guiButtonFixed.Y = (float)(5 + index * 42);
		guiButtonFixed.Caption = StaticData.Translate("key_party_option_select");
		guiButtonFixed.eventHandlerParam.customData = playerId;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, PartyWindow.OnSeleckPartyMemberClicked);
		guiButtonFixed._marginLeft = 25;
		guiButtonFixed.MarginTop = -2;
		guiButtonFixed.FontSize = 14;
		guiButtonFixed.Alignment = 3;
		this.optionsMenuWindow.AddGuiElement(guiButtonFixed);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("PartyGUI", "optionSelect");
		guiTexture.X = guiButtonFixed.X + 5f;
		guiTexture.Y = guiButtonFixed.Y + 8f;
		this.optionsMenuWindow.AddGuiElement(guiTexture);
	}

	private void ChangeOptionMenuZorder()
	{
		this.optionsMenuWindow.zOrder = 201;
		AndromedaGui.gui.UpdateWindowsCollection();
	}

	private void ClearInviteeElements()
	{
		foreach (GuiElement guiElement in this.inviteeElementForDelete)
		{
			this.personalStatsWindow.RemoveGuiElement(guiElement);
			this.mainPartyWindow.RemoveGuiElement(guiElement);
		}
		this.inviteeElementForDelete.Clear();
	}

	private void ClearPartyMemberElements()
	{
		foreach (GuiElement guiElement in this.partyMemberElementForDelete)
		{
			this.personalStatsWindow.RemoveGuiElement(guiElement);
			this.mainPartyWindow.RemoveGuiElement(guiElement);
		}
		this.partyMemberElementForDelete.Clear();
	}

	private void ClearPartyOfferElements()
	{
		foreach (GuiElement guiElement in this.partyOfferElementForDelete)
		{
			this.partyInvitationWindow.RemoveGuiElement(guiElement);
		}
		this.partyOfferElementForDelete.Clear();
	}

	private void CreateMainPartyWindow()
	{
		this.mainPartyWindow = new GuiWindow()
		{
			boundries = new Rect(80f, 64f, 175f, 65f),
			isHidden = false,
			zOrder = 40
		};
		AndromedaGui.gui.AddWindow(this.mainPartyWindow);
		this.DrawPartyStuff();
	}

	public void CreatePlayerStatsWindow()
	{
		this.selfPlayerId = NetworkScript.player.vessel.playerId;
		this.personalStatsWindow = new GuiWindow();
		this.personalStatsWindow.SetBackgroundTexture("PartyGUI", "playerFrameBackground");
		this.personalStatsWindow.boundries.set_x(0f);
		this.personalStatsWindow.boundries.set_y(0f);
		this.personalStatsWindow.isHidden = false;
		this.personalStatsWindow.zOrder = 35;
		AndromedaGui.gui.AddWindow(this.personalStatsWindow);
		this.personalStatsShield = new GuiTexture();
		this.personalStatsShield.SetTexture("PartyGUI", "statsBlue");
		this.personalStatsShield.X = 96f;
		this.personalStatsShield.Y = 10f;
		this.personalStatsShield.boundries.set_width(0f);
		this.personalStatsWindow.AddGuiElement(this.personalStatsShield);
		this.personalStatsCorpus = new GuiTexture();
		this.personalStatsCorpus.SetTexture("PartyGUI", "statsGreen");
		this.personalStatsCorpus.X = 101f;
		this.personalStatsCorpus.Y = 24f;
		this.personalStatsCorpus.boundries.set_width(0f);
		this.personalStatsWindow.AddGuiElement(this.personalStatsCorpus);
		this.personalStatsCritical = new GuiTexture();
		this.personalStatsCritical.SetTexture("PartyGUI", "statsOrange");
		this.personalStatsCritical.X = 101f;
		this.personalStatsCritical.Y = 40f;
		this.personalStatsCritical.boundries.set_width(0f);
		this.personalStatsWindow.AddGuiElement(this.personalStatsCritical);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("PartyGUI", "playerFrame");
		guiTexture.X = 0f;
		guiTexture.Y = 0f;
		this.personalStatsWindow.AddGuiElement(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("PartyGUI", "levelFrame");
		guiTexture1.X = 0f;
		guiTexture1.Y = 0f;
		this.personalStatsWindow.AddGuiElement(guiTexture1);
		this.personalStatsLevel = new GuiLabel()
		{
			boundries = new Rect(0f, 2f, 16f, 14f),
			FontSize = 12,
			TextColor = Color.get_gray(),
			text = string.Empty,
			Alignment = 4
		};
		this.personalStatsWindow.AddGuiElement(this.personalStatsLevel);
		this.personalStatsName = new GuiLabel()
		{
			boundries = new Rect(4f, 67f, 80f, 12f),
			FontSize = 10,
			text = string.Empty,
			WordWrap = false,
			Clipping = 1,
			Alignment = 3
		};
		this.personalStatsWindow.AddGuiElement(this.personalStatsName);
		float single = 105f;
		float single1 = 9f;
		this.shieldValueShadow = new GuiLabel()
		{
			boundries = new Rect(single + 1f, single1 + 1f, 150f, 9f),
			FontSize = 10,
			text = string.Empty,
			Alignment = 3,
			TextColor = Color.get_black()
		};
		this.personalStatsWindow.AddGuiElement(this.shieldValueShadow);
		this.corpusValueShadow = new GuiLabel()
		{
			boundries = new Rect(single + 1f, single1 + 1f + 14f, 150f, 9f),
			FontSize = 10,
			text = string.Empty,
			Alignment = 3,
			TextColor = Color.get_black()
		};
		this.personalStatsWindow.AddGuiElement(this.corpusValueShadow);
		this.criticalValueShadow = new GuiLabel()
		{
			boundries = new Rect(single + 1f, single1 + 1f + 30f, 150f, 9f),
			FontSize = 10,
			text = string.Empty,
			Alignment = 3,
			TextColor = Color.get_black()
		};
		this.personalStatsWindow.AddGuiElement(this.criticalValueShadow);
		this.shieldValue = new GuiLabel()
		{
			boundries = new Rect(single, single1, 150f, 9f),
			FontSize = 10,
			text = string.Empty,
			Alignment = 3
		};
		this.personalStatsWindow.AddGuiElement(this.shieldValue);
		this.corpusValue = new GuiLabel()
		{
			boundries = new Rect(single, single1 + 14f, 150f, 9f),
			FontSize = 10,
			text = string.Empty,
			Alignment = 3
		};
		this.personalStatsWindow.AddGuiElement(this.corpusValue);
		this.criticalValue = new GuiLabel()
		{
			boundries = new Rect(single, single1 + 30f, 150f, 9f),
			FontSize = 10,
			text = string.Empty,
			Alignment = 3
		};
		this.personalStatsWindow.AddGuiElement(this.criticalValue);
		this.personalStatsAvatar = new GuiTexture()
		{
			boundries = new Rect(19f, 1f, 58f, 58f)
		};
		this.personalStatsWindow.AddGuiElement(this.personalStatsAvatar);
		Texture2D avatarOrStartIt = playWebGame.GetAvatarOrStartIt(NetworkScript.player.vessel.playerAvatarUrl, new Action<AvatarJob>(this, PartyWindow.OnAvatarLoaded), this.personalStatsAvatar);
		if (avatarOrStartIt != null)
		{
			this.personalStatsAvatar.SetTextureKeepSize(avatarOrStartIt);
		}
		else
		{
			this.personalStatsAvatar.SetTextureKeepSize(this.GetPlayerAvatar(NetworkScript.player.vessel.playerId));
		}
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("FrameworkGUI", "empty");
		guiButtonFixed.boundries = new Rect(1f, 1f, 85f, 80f);
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.eventHandlerParam.customData = NetworkScript.player.vessel.playerId;
		guiButtonFixed.eventHandlerParam.customData2 = -1;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, PartyWindow.OnOptionMenuClicked);
		this.personalStatsWindow.AddGuiElement(guiButtonFixed);
		GuiButtonFixed rect = new GuiButtonFixed();
		rect.SetTexture("FrameworkGUI", "empty");
		rect.boundries = new Rect(88f, 1f, 180f, 55f);
		rect.Caption = string.Empty;
		rect.Clicked = new Action<EventHandlerParam>(this, PartyWindow.OnShowDetailsClicked);
		this.personalStatsWindow.AddGuiElement(rect);
		this.UpdateFullPersonalStats();
		this.personalStatsWindow.customOnGUIAction = new Action(this, PartyWindow.OnPersonalStatsHovered);
	}

	private void DrawInvitee(PartyInvite data, int index)
	{
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(14f, 54f, 56f, 10f),
			FontSize = 10,
			text = data.name,
			Clipping = 1,
			WordWrap = false
		};
		this.mainPartyWindow.AddGuiElement(guiLabel);
		this.inviteeElementForDelete.Add(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect((float)(24 + 50 * index), 2f, 10f, 10f),
			FontSize = 8,
			Alignment = 4,
			text = data.level.ToString()
		};
		this.mainPartyWindow.AddGuiElement(guiLabel1);
		this.inviteeElementForDelete.Add(guiLabel1);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture2D(this.GetPlayerAvatar(data.player));
		guiTexture.boundries = new Rect((float)(22 + 50 * index), 0f, 48f, 48f);
		this.mainPartyWindow.AddGuiElement(guiTexture);
		this.inviteeElementForDelete.Add(guiTexture);
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("PartyGUI", "highlightBlack");
		rect.boundries = new Rect((float)(22 + 50 * index), 0f, 48f, 48f);
		this.mainPartyWindow.AddGuiElement(rect);
		this.inviteeElementForDelete.Add(rect);
		TimeSpan timeSpan = data.timeToDie - StaticData.now;
		int totalSeconds = (int)timeSpan.get_TotalSeconds() + 1;
		GuiSecondsTracker guiSecondsTracker = new GuiSecondsTracker(totalSeconds * 1000, this.mainPartyWindow)
		{
			boundries = new Rect((float)(22 + 50 * index), 0f, 48f, 48f),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 22
		};
		this.inviteeElementForDelete.Add(guiSecondsTracker);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("FrameworkGUI", "empty");
		guiButtonFixed.boundries = new Rect((float)(22 + 50 * index), 0f, 48f, 48f);
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.Alignment = 4;
		guiButtonFixed.eventHandlerParam.customData = data.player;
		guiButtonFixed.eventHandlerParam.customData2 = index;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, PartyWindow.OnOptionMenuClicked);
		this.mainPartyWindow.AddGuiElement(guiButtonFixed);
		this.inviteeElementForDelete.Add(guiButtonFixed);
		switch (index)
		{
			case 0:
			{
				guiLabel.Alignment = (guiLabel.TextWidth < guiLabel.boundries.get_width() ? 5 : 3);
				guiLabel.boundries = new Rect(14f, 54f, 56f, 10f);
				break;
			}
			case 1:
			{
				guiLabel.Alignment = (guiLabel.TextWidth < guiLabel.boundries.get_width() ? 4 : 3);
				guiLabel.boundries = new Rect(72f, 54f, 48f, 10f);
				break;
			}
			case 2:
			{
				guiLabel.Alignment = 3;
				guiLabel.boundries = new Rect(122f, 54f, 56f, 10f);
				break;
			}
		}
	}

	private void DrawInvitersOffers()
	{
		bool key;
		if (NetworkScript.partyInviters == null)
		{
			return;
		}
		int count = NetworkScript.partyInviters.get_Count();
		if (count < 1)
		{
			return;
		}
		if (this.selectedPartyOfferPlayerId != 0 && !NetworkScript.partyInviters.ContainsKey(this.selectedPartyOfferPlayerId))
		{
			this.selectedPartyOfferPlayerId = (long)0;
		}
		for (int i = 0; i < count; i++)
		{
			GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
			guiButtonFixed.SetTexture("PartyGUI", "partyInviteButton");
			guiButtonFixed.X = (float)(22 + i * 50);
			guiButtonFixed.Y = 7f;
			guiButtonFixed.Alignment = 4;
			guiButtonFixed.FontSize = 24;
			guiButtonFixed.textColorNormal = GuiNewStyleBar.blueColor;
			guiButtonFixed.Caption = (i + 1).ToString();
			EventHandlerParam eventHandlerParam = guiButtonFixed.eventHandlerParam;
			KeyValuePair<long, PartyInvite> keyValuePair = Enumerable.ElementAt<KeyValuePair<long, PartyInvite>>(NetworkScript.partyInviters, i);
			eventHandlerParam.customData = keyValuePair.get_Key();
			guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, PartyWindow.OnPartyOfferClicked);
			guiButtonFixed.behaviourKeepClicked = true;
			guiButtonFixed.groupId = 111;
			GuiButtonFixed guiButtonFixed1 = guiButtonFixed;
			if (this.selectedPartyOfferPlayerId != 0)
			{
				long num = this.selectedPartyOfferPlayerId;
				KeyValuePair<long, PartyInvite> keyValuePair1 = Enumerable.ElementAt<KeyValuePair<long, PartyInvite>>(NetworkScript.partyInviters, i);
				key = num == keyValuePair1.get_Key();
			}
			else
			{
				key = (i != 0 ? false : true);
			}
			guiButtonFixed1.IsClicked = key;
			this.partyInvitationWindow.AddGuiElement(guiButtonFixed);
		}
	}

	private void DrawPartyMember(PartyMemberClientSide pm, int index)
	{
		bool item = NetworkScript.party.members.get_Item(0).playerId == pm.playerId;
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(14f, 54f, 56f, 10f),
			FontSize = 10,
			text = pm.playerName,
			Clipping = 1,
			WordWrap = false
		};
		this.mainPartyWindow.AddGuiElement(guiLabel);
		this.partyMemberElementForDelete.Add(guiLabel);
		GuiTexture guiTexture = new GuiTexture()
		{
			boundries = new Rect((float)(22 + 50 * index), 0f, 48f, 48f)
		};
		this.mainPartyWindow.AddGuiElement(guiTexture);
		this.partyMemberElementForDelete.Add(guiTexture);
		Texture2D avatarOrStartIt = playWebGame.GetAvatarOrStartIt(pm.avatarUrl, new Action<AvatarJob>(this, PartyWindow.OnAvatarLoaded), guiTexture);
		if (avatarOrStartIt != null)
		{
			guiTexture.SetTextureKeepSize(avatarOrStartIt);
		}
		else
		{
			guiTexture.SetTextureKeepSize(this.GetPlayerAvatar(pm.playerId));
		}
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect((float)(25 + 50 * index), 3f, 10f, 10f),
			FontSize = 8,
			Alignment = 4,
			text = pm.playerLevel.ToString(),
			TextColor = Color.get_black()
		};
		this.mainPartyWindow.AddGuiElement(guiLabel1);
		this.partyMemberElementForDelete.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect((float)(24 + 50 * index), 2f, 10f, 10f),
			FontSize = 8,
			Alignment = 4,
			text = pm.playerLevel.ToString()
		};
		this.mainPartyWindow.AddGuiElement(guiLabel2);
		this.partyMemberElementForDelete.Add(guiLabel2);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("FrameworkGUI", "empty");
		guiButtonFixed.boundries = new Rect((float)(22 + 50 * index), 0f, 48f, 48f);
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.Alignment = 4;
		guiButtonFixed.eventHandlerParam.customData = pm.playerId;
		guiButtonFixed.eventHandlerParam.customData2 = index;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, PartyWindow.OnOptionMenuClicked);
		this.mainPartyWindow.AddGuiElement(guiButtonFixed);
		this.partyMemberElementForDelete.Add(guiButtonFixed);
		if (item && NetworkScript.player.pvpGame == null)
		{
			GuiTexture rect = new GuiTexture();
			rect.SetTexture("PartyGUI", "optionLeader");
			rect.boundries = new Rect((float)(51 + 50 * index), 0f, 16f, 16f);
			this.mainPartyWindow.AddGuiElement(rect);
			this.partyMemberElementForDelete.Add(rect);
		}
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("PartyGUI", "memberBlueBar");
		guiTexture1.boundries = new Rect((float)(23 + 50 * index), 30f, 0f, 17f);
		this.mainPartyWindow.AddGuiElement(guiTexture1);
		this.partyMemberElementForDelete.Add(guiTexture1);
		GuiTexture rect1 = new GuiTexture();
		rect1.SetTexture("PartyGUI", "memberGreenBar");
		rect1.boundries = new Rect((float)(23 + 50 * index), 36f, 0f, 17f);
		this.mainPartyWindow.AddGuiElement(rect1);
		this.partyMemberElementForDelete.Add(rect1);
		pm.newShiel = guiTexture1;
		pm.newCorpus = rect1;
		switch (index)
		{
			case 0:
			{
				guiLabel.Alignment = (guiLabel.TextWidth < guiLabel.boundries.get_width() ? 5 : 3);
				guiLabel.boundries = new Rect(14f, 54f, 56f, 10f);
				break;
			}
			case 1:
			{
				guiLabel.Alignment = (guiLabel.TextWidth < guiLabel.boundries.get_width() ? 4 : 3);
				guiLabel.boundries = new Rect(72f, 54f, 48f, 10f);
				break;
			}
			case 2:
			{
				guiLabel.Alignment = 3;
				guiLabel.boundries = new Rect(122f, 54f, 56f, 10f);
				break;
			}
		}
	}

	private void DrawPartyRuleGUI()
	{
		if (NetworkScript.player.pvpGame != null)
		{
			return;
		}
		this.ruleFrame = new GuiTexture();
		this.ruleFrame.SetTexture("PartyGUI", "levelFrame");
		this.ruleFrame.X = 0f;
		this.ruleFrame.Y = 29f;
		this.personalStatsWindow.AddGuiElement(this.ruleFrame);
		this.ruleIcon = new GuiTexture();
		this.ruleIcon.SetTexture("PartyGUI", "optionLootFK");
		this.ruleIcon.X = 0f;
		this.ruleIcon.Y = 30f;
		this.personalStatsWindow.AddGuiElement(this.ruleIcon);
		if (NetworkScript.party.rules == 1)
		{
			this.ruleIcon.SetTexture("PartyGUI", "optionLootRR");
		}
	}

	private void DrawPartyStuff()
	{
		this.RemovePartyRoleGUI();
		this.RemoveSelfLeader();
		this.HideOptionMenuWindow();
		this.isInParty = NetworkScript.party != null;
		this.selfPlayerId = NetworkScript.player.vessel.playerId;
		this.ClearPartyMemberElements();
		this.ClearInviteeElements();
		int num = 0;
		if (this.isInParty)
		{
			this.isPartyLeader = NetworkScript.party.members.get_Item(0).playerId == this.selfPlayerId;
			foreach (PartyMemberClientSide member in NetworkScript.party.members)
			{
				if (member.playerId != NetworkScript.player.playId)
				{
					int num1 = num;
					num = num1 + 1;
					this.DrawPartyMember(member, num1);
				}
			}
			this.DrawPartyRuleGUI();
			if (this.isPartyLeader)
			{
				this.DrawSelfLeader();
			}
		}
		IEnumerator<PartyInvite> enumerator = NetworkScript.partyInvitees.get_Values().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				this.DrawInvitee(enumerator.get_Current(), num);
				num++;
			}
		}
		finally
		{
			if (enumerator == null)
			{
			}
			enumerator.Dispose();
		}
		this.mainPartyWindow.preDrawHandler = new Action<object>(this, PartyWindow.UpdatePartyMemberStats);
	}

	private void DrawSelfLeader()
	{
		if (NetworkScript.player.pvpGame != null)
		{
			return;
		}
		this.selfLeaderIcon = new GuiTexture();
		this.selfLeaderIcon.SetTexture("PartyGUI", "optionLeader");
		this.selfLeaderIcon.X = 58f;
		this.selfLeaderIcon.Y = 1f;
		this.personalStatsWindow.AddGuiElement(this.selfLeaderIcon);
	}

	private Texture2D GetPlayerAvatar(long id)
	{
		if (!NetworkScript.clientSideClientsList.ContainsKey(id))
		{
			return (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "unknown");
		}
		return (Texture2D)playWebGame.assets.GetFromStaticSet("ShipsAvatars", NetworkScript.clientSideClientsList.get_Item(id).cfg.assetName);
	}

	private void HideMainWindow()
	{
		if (this.mainPartyWindow == null)
		{
			return;
		}
		this.HideOptionMenuWindow();
		this.RemovePartyRoleGUI();
		this.RemoveSelfLeader();
		this.mainPartyWindowBackground.timeHammerFx = 0.5f;
		this.mainPartyWindowBackground.StartHammerEffect(this.mainPartyWindow.boundries.get_x(), -this.mainPartyWindowBackground.boundries.get_height());
		this.mainPartyWindow.zOrder = 34;
		AndromedaGui.gui.UpdateWindowsCollection();
		this.mainPartyWindow.timeHammerFx = 0.5f;
		this.mainPartyWindow.fxEnded = new Action(this, PartyWindow.RemoveMainWindow);
		this.mainPartyWindow.StartHammerEffect(this.mainPartyWindow.boundries.get_x(), -this.mainPartyWindow.boundries.get_height());
	}

	private void HideOptionMenuWindow()
	{
		if (this.optionsMenuWindow == null)
		{
			return;
		}
		this.optionSelectedPlayer = (long)0;
		this.optionsMenuWindow.zOrder = 28;
		AndromedaGui.gui.UpdateWindowsCollection();
		this.optionsMenuWindow.timeHammerFx = 0.2f;
		this.optionsMenuWindow.fxEnded = new Action(this, PartyWindow.RemoveOptionMenuWindow);
		this.optionsMenuWindow.StartHammerEffect(this.optionsMenuWindow.boundries.get_x(), -this.optionsMenuWindow.boundries.get_height());
	}

	private void HidePartyInvitationWindow()
	{
		if (this.partyInvitationWindow == null)
		{
			return;
		}
		this.partyInvitationWindow.timeHammerFx = 0.5f;
		this.partyInvitationWindow.fxEnded = new Action(this, PartyWindow.RemovePartyInvitationWindow);
		this.partyInvitationWindow.StartHammerEffect(this.partyInvitationWindow.boundries.get_x(), -this.partyInvitationWindow.boundries.get_height());
	}

	public void HidePersonalGui()
	{
		if (this.personalStatsWindow == null)
		{
			return;
		}
		this.personalStatsWindow.StartHammerEffect(0f, -this.personalStatsWindow.boundries.get_height());
		this.personalStatsWindow.fxEnded = new Action(this, PartyWindow.SetPersonalWindowToHide);
	}

	private void OnAvatarLoaded(AvatarJob job)
	{
		((GuiTexture)job.token).SetTextureKeepSize(job.job.get_texture());
	}

	private void OnCancelInvitationClicked(EventHandlerParam p)
	{
		// 
		// Current member / type: System.Void PartyWindow::OnCancelInvitationClicked(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnCancelInvitationClicked(EventHandlerParam)
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

	private void OnKickPlayerClicked(EventHandlerParam p)
	{
		// 
		// Current member / type: System.Void PartyWindow::OnKickPlayerClicked(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnKickPlayerClicked(EventHandlerParam)
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

	private void OnOptionMenuClicked(EventHandlerParam prm)
	{
		EventHandlerParam eventHandlerParam;
		if (NetworkScript.player.pvpGame != null && NetworkScript.player.pvpGame.gameType != null && NetworkScript.player.pvpGame.gameType.winType == null)
		{
			eventHandlerParam = new EventHandlerParam()
			{
				customData = (long)prm.customData
			};
			this.OnSeleckPartyMemberClicked(eventHandlerParam);
			return;
		}
		if (this.mainPartyWindow == null || !this.isInParty && (long)prm.customData == this.selfPlayerId)
		{
			eventHandlerParam = new EventHandlerParam()
			{
				customData = this.selfPlayerId
			};
			this.OnSeleckPartyMemberClicked(eventHandlerParam);
			return;
		}
		if (this.optionSelectedPlayer == 0 || this.optionSelectedPlayer != (long)prm.customData)
		{
			this.optionSelectedPlayer = (long)prm.customData;
			this.SetHighlightPlayer((int)prm.customData2);
			this.ShowOptionMenuWindow();
			this.AddOptionMenuButtons(this.optionSelectedPlayer);
		}
		else
		{
			this.HideOptionMenuWindow();
		}
	}

	private void OnPartyOfferAccepted(EventHandlerParam p)
	{
		// 
		// Current member / type: System.Void PartyWindow::OnPartyOfferAccepted(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnPartyOfferAccepted(EventHandlerParam)
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

	private void OnPartyOfferClicked(EventHandlerParam prm)
	{
		this.ClearPartyOfferElements();
		this.selectedPartyOfferPlayerId = (long)prm.customData;
		PartyInvite item = NetworkScript.partyInviters.get_Item(this.selectedPartyOfferPlayerId);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(22f, 62f, 195f, 65f),
			Alignment = 4,
			FontSize = 14,
			TextColor = GuiNewStyleBar.blueColor,
			text = string.Format(StaticData.Translate("key_party_invitation_offer_info"), item.name, item.level)
		};
		this.partyInvitationWindow.AddGuiElement(guiLabel);
		this.partyOfferElementForDelete.Add(guiLabel);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("PartyGUI", "optionButton");
		guiButtonFixed.X = 46f;
		guiButtonFixed.Y = 138f;
		guiButtonFixed.Alignment = 4;
		guiButtonFixed.FontSize = 14;
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.eventHandlerParam.customData = this.selectedPartyOfferPlayerId;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, PartyWindow.OnPartyOfferAccepted);
		this.partyInvitationWindow.AddGuiElement(guiButtonFixed);
		this.partyOfferElementForDelete.Add(guiButtonFixed);
		GuiButtonFixed action = new GuiButtonFixed();
		action.SetTexture("PartyGUI", "optionButton");
		action.X = 46f;
		action.Y = 173f;
		action._marginLeft = 23;
		action.MarginTop = -2;
		action.Alignment = 3;
		action.FontSize = 14;
		action.Caption = StaticData.Translate("key_party_invitation_offer_decline");
		action.eventHandlerParam.customData = this.selectedPartyOfferPlayerId;
		action.Clicked = new Action<EventHandlerParam>(this, PartyWindow.OnPartyOfferDecline);
		this.partyInvitationWindow.AddGuiElement(action);
		this.partyOfferElementForDelete.Add(action);
		TimeSpan timeSpan = item.timeToDie - StaticData.now;
		int totalSeconds = (int)timeSpan.get_TotalSeconds() + 1;
		GuiSecondsTracker guiSecondsTracker = new GuiSecondsTracker(StaticData.Translate("key_party_invitation_offer_accept"), totalSeconds * 1000, this.partyInvitationWindow)
		{
			boundries = new Rect(70f, 146f, 120f, 16f),
			FontSize = 14,
			Font = GuiLabel.FontBold
		};
		this.partyOfferElementForDelete.Add(guiSecondsTracker);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("PartyGUI", "optionOk");
		guiTexture.X = 52f;
		guiTexture.Y = 146f;
		this.partyInvitationWindow.AddGuiElement(guiTexture);
		this.partyOfferElementForDelete.Add(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("PartyGUI", "optionX");
		guiTexture1.X = 52f;
		guiTexture1.Y = 181f;
		this.partyInvitationWindow.AddGuiElement(guiTexture1);
		this.partyOfferElementForDelete.Add(guiTexture1);
	}

	private void OnPartyOfferDecline(EventHandlerParam p)
	{
		// 
		// Current member / type: System.Void PartyWindow::OnPartyOfferDecline(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnPartyOfferDecline(EventHandlerParam)
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

	private void OnPartyRuleChangeClicked(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void PartyWindow::OnPartyRuleChangeClicked(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnPartyRuleChangeClicked(EventHandlerParam)
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

	private void OnPersonalStatsHovered()
	{
		Vector3 _mousePosition = Input.get_mousePosition();
		float _height = (float)Screen.get_height() - _mousePosition.y;
		float single = _mousePosition.x;
		Rect rect = new Rect(94f, 9f, 160f, 40f);
		bool flag = rect.Contains(new Vector2(single, _height));
		if (this.lastState == flag)
		{
			return;
		}
		this.lastState = flag;
		this.isPersonalStatsHoverd = this.lastState;
		if (this.isPersonalStatsHoverd)
		{
			if (!NetworkScript.player.playerBelongings.isShowMoreDetailsOn)
			{
				this.shieldValue.text = string.Empty;
				this.corpusValue.text = string.Empty;
				this.criticalValue.text = string.Empty;
			}
			else
			{
				this.shieldValue.text = StaticData.Translate("key_personal_stats_shield_label");
				this.corpusValue.text = StaticData.Translate("key_personal_stats_corpus_label");
				this.criticalValue.text = StaticData.Translate("key_personal_stats_energy_label");
			}
		}
	}

	private void OnPromoteClickd(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void PartyWindow::OnPromoteClickd(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnPromoteClickd(EventHandlerParam)
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

	private void OnSeleckPartyMemberClicked(EventHandlerParam p)
	{
		long num = (long)p.customData;
		GameObject item = null;
		GameObjectPhysics gameObjectPhysic = null;
		if (NetworkScript.player != null && NetworkScript.player.shipScript != null)
		{
			if (NetworkScript.clientSideClientsList.ContainsKey(num))
			{
				item = NetworkScript.clientSideClientsList.get_Item(num).gameObject;
				gameObjectPhysic = NetworkScript.clientSideClientsList.get_Item(num).vessel;
			}
			if (item != null && gameObjectPhysic != null)
			{
				NetworkScript.player.shipScript.ManageSelectObjectRequest(item, gameObjectPhysic);
			}
		}
		this.optionSelectedPlayer = (long)0;
		this.HideOptionMenuWindow();
	}

	private void OnShowDetailsClicked(object prm)
	{
		// 
		// Current member / type: System.Void PartyWindow::OnShowDetailsClicked(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnShowDetailsClicked(System.Object)
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

	private void PinMainPatyWindowOnScreen()
	{
		if (this.mainPartyWindowBackground == null || this.mainPartyWindow == null)
		{
			Debug.LogWarning("PinMainPatyWindowOnScreen return");
			return;
		}
		this.mainPartyWindowBackground.fxEnded = null;
		this.mainPartyWindowBackground.StopHammerEffect();
		this.mainPartyWindowBackground.boundries.set_x(80f);
		this.mainPartyWindowBackground.boundries.set_y(10f);
		this.mainPartyWindow.fxEnded = null;
		this.mainPartyWindow.StopHammerEffect();
		this.mainPartyWindow.boundries.set_x(80f);
		this.mainPartyWindow.boundries.set_y(64f);
		this.DrawPartyStuff();
	}

	public void PutPersonalWindowOutOfScreen()
	{
		if (this.personalStatsWindow == null)
		{
			return;
		}
		this.personalStatsWindow.boundries.set_y(-this.personalStatsWindow.boundries.get_height());
	}

	private void RemoveHighlightPlayer()
	{
		if (this.optionMenuHighlight != null)
		{
			this.personalStatsWindow.RemoveGuiElement(this.optionMenuHighlight);
			if (this.mainPartyWindow != null)
			{
				this.mainPartyWindow.RemoveGuiElement(this.optionMenuHighlight);
			}
			this.inviteeElementForDelete.Remove(this.optionMenuHighlight);
			this.optionMenuHighlight = null;
		}
	}

	private void RemoveMainPartyWindow()
	{
		if (this.mainPartyWindow == null)
		{
			return;
		}
		AndromedaGui.gui.RemoveWindow(this.mainPartyWindow.handler);
		this.mainPartyWindow = null;
	}

	private void RemoveMainWindow()
	{
		if (this.mainPartyWindowBackground == null)
		{
			return;
		}
		AndromedaGui.gui.RemoveWindow(this.mainPartyWindowBackground.handler);
		this.mainPartyWindowBackground = null;
		this.RemoveMainPartyWindow();
	}

	private void RemoveOptionMenuWindow()
	{
		this.optionSelectedPlayer = (long)0;
		if (this.optionsMenuWindow == null)
		{
			return;
		}
		this.RemoveHighlightPlayer();
		AndromedaGui.gui.RemoveWindow(this.optionsMenuWindow.handler);
		this.optionsMenuWindow = null;
	}

	private void RemovePartyInvitationWindow()
	{
		if (this.partyInvitationWindow == null)
		{
			return;
		}
		AndromedaGui.gui.RemoveWindow(this.partyInvitationWindow.handler);
		this.partyInvitationWindow = null;
	}

	private void RemovePartyRoleGUI()
	{
		this.personalStatsWindow.RemoveGuiElement(this.ruleFrame);
		this.personalStatsWindow.RemoveGuiElement(this.ruleIcon);
	}

	private void RemoveSelfLeader()
	{
		this.personalStatsWindow.RemoveGuiElement(this.selfLeaderIcon);
	}

	public void ReOrderPartyInvitationGui()
	{
		if (this.partyInvitationWindow == null)
		{
			return;
		}
		this.partyInvitationWindow.PutToHorizontalCenter();
	}

	private void SetHighlightPlayer(int index)
	{
		this.RemoveHighlightPlayer();
		if (index != -1)
		{
			this.optionMenuHighlight = new GuiTexture();
			this.optionMenuHighlight.SetTexture("PartyGUI", "highlightBlue");
			this.optionMenuHighlight.boundries = new Rect((float)(22 + 50 * index), 0f, 48f, 48f);
			this.mainPartyWindow.AddGuiElement(this.optionMenuHighlight);
			this.inviteeElementForDelete.Add(this.optionMenuHighlight);
		}
		else
		{
			this.optionMenuHighlight = new GuiTexture();
			this.optionMenuHighlight.SetTexture("PartyGUI", "highlightBlue");
			this.optionMenuHighlight.boundries = new Rect(19f, 1f, 58f, 58f);
			this.personalStatsWindow.AddGuiElement(this.optionMenuHighlight);
			this.inviteeElementForDelete.Add(this.optionMenuHighlight);
		}
	}

	private void SetPersonalWindowToHide()
	{
		this.personalStatsWindow.isHidden = true;
	}

	public void SetToZero()
	{
		this.personalStatsShield.boundries.set_width(0f);
		this.personalStatsCorpus.boundries.set_width(0f);
		this.personalStatsCritical.boundries.set_width(0f);
		this.shieldValue.text = string.Format("{0}/{1}", 0, NetworkScript.player.vessel.cfg.shieldMax);
		this.corpusValue.text = string.Format("{0}/{1}", 0, NetworkScript.player.vessel.cfg.hitPointsMax);
		this.criticalValue.text = string.Format("{0}/{1}", 0, NetworkScript.player.vessel.cfg.criticalEnergyMax);
		this.shieldValueShadow.text = this.shieldValue.text;
		this.corpusValueShadow.text = this.corpusValue.text;
		this.criticalValueShadow.text = this.criticalValue.text;
	}

	private void ShowOptionMenuWindow()
	{
		if (this.optionsMenuWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.optionsMenuWindow.handler);
		}
		float single = 128f;
		this.optionsMenuWindow = new GuiWindow()
		{
			boundries = new Rect(96f, single, 158f, 45f),
			isHidden = false,
			zOrder = 28
		};
		AndromedaGui.gui.AddWindow(this.optionsMenuWindow);
		int num = 1;
		if (this.isInParty && Enumerable.FirstOrDefault<PartyMemberClientSide>(Enumerable.Where<PartyMemberClientSide>(NetworkScript.party.members, new Func<PartyMemberClientSide, bool>(this, (PartyMemberClientSide t) => t.playerId == this.optionSelectedPlayer))) != null)
		{
			if (this.optionSelectedPlayer == this.selfPlayerId)
			{
				num = (!this.isPartyLeader ? 2 : 3);
			}
			else if (this.isPartyLeader)
			{
				num = 3;
			}
		}
		this.optionsMenuWindow.boundries.set_width(158f);
		this.optionsMenuWindow.boundries.set_height((float)(45 * num - (num - 1) * 4));
		for (int i = num - 1; i >= 0; i--)
		{
			GuiTexture guiTexture = new GuiTexture();
			guiTexture.SetTexture("PartyGUI", "optionMenuFrame");
			guiTexture.X = 0f;
			guiTexture.Y = (float)(41 * i);
			this.optionsMenuWindow.AddGuiElement(guiTexture);
		}
		this.optionsMenuWindow.boundries.set_y(-this.optionsMenuWindow.boundries.get_height());
		this.optionsMenuWindow.timeHammerFx = 0.3f;
		this.optionsMenuWindow.fxEnded = new Action(this, PartyWindow.ChangeOptionMenuZorder);
		this.optionsMenuWindow.StartHammerEffect(this.optionsMenuWindow.boundries.get_x(), single);
	}

	private void ShowPartyInvitationWindow()
	{
		if (this.partyInvitationWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.partyInvitationWindow.handler);
			this.partyInvitationWindow = null;
		}
		float single = 105f;
		this.partyInvitationWindow = new GuiWindow();
		this.partyInvitationWindow.SetBackgroundTexture("PartyGUI", "partyInviteFrame");
		this.partyInvitationWindow.PutToHorizontalCenter();
		this.partyInvitationWindow.boundries.set_y(single);
		this.partyInvitationWindow.isHidden = false;
		this.partyInvitationWindow.zOrder = 30;
		AndromedaGui.gui.AddWindow(this.partyInvitationWindow);
		this.partyInvitationWindow.boundries.set_y(-this.partyInvitationWindow.boundries.get_height());
		this.partyInvitationWindow.amplitudeHammerShake = 20f;
		this.partyInvitationWindow.timeHammerFx = 0.5f;
		this.partyInvitationWindow.moveToShakeRatio = 0.6f;
		this.partyInvitationWindow.fxEnded = new Action(this, PartyWindow.DrawInvitersOffers);
		this.partyInvitationWindow.StartHammerEffect(this.partyInvitationWindow.boundries.get_x(), single);
	}

	private void ShowWindow()
	{
		if (this.mainPartyWindowBackground != null)
		{
			AndromedaGui.gui.RemoveWindow(this.mainPartyWindowBackground.handler);
			this.mainPartyWindowBackground = null;
		}
		if (this.mainPartyWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.mainPartyWindow.handler);
			this.mainPartyWindow = null;
		}
		this.mainPartyWindowBackground = new GuiWindow();
		this.mainPartyWindowBackground.SetBackgroundTexture("PartyGUI", "partyFrame");
		this.mainPartyWindowBackground.boundries.set_x(80f);
		this.mainPartyWindowBackground.boundries.set_y(10f);
		this.mainPartyWindowBackground.isHidden = false;
		this.mainPartyWindowBackground.zOrder = 30;
		AndromedaGui.gui.AddWindow(this.mainPartyWindowBackground);
		this.mainPartyWindowBackground.boundries.set_y(-this.mainPartyWindowBackground.boundries.get_height());
		this.mainPartyWindowBackground.timeHammerFx = 0.5f;
		this.mainPartyWindowBackground.fxEnded = new Action(this, PartyWindow.CreateMainPartyWindow);
		this.mainPartyWindowBackground.StartHammerEffect(this.mainPartyWindowBackground.boundries.get_x(), 10f);
	}

	public void StartHammerEffect()
	{
		if (this.personalStatsWindow == null)
		{
			return;
		}
		this.personalStatsWindow.timeHammerFx = 0.5f;
		this.personalStatsWindow.StartHammerEffect(this.personalStatsWindow.boundries.get_x(), 0f);
	}

	public void UpdateFullPersonalStats()
	{
		this.UpdatePersonalStats();
		Texture2D avatarOrStartIt = playWebGame.GetAvatarOrStartIt(NetworkScript.player.vessel.playerAvatarUrl, new Action<AvatarJob>(this, PartyWindow.OnAvatarLoaded), this.personalStatsAvatar);
		if (avatarOrStartIt != null)
		{
			this.personalStatsAvatar.SetTextureKeepSize(avatarOrStartIt);
		}
		else
		{
			this.personalStatsAvatar.SetTextureKeepSize("ShipsAvatars", NetworkScript.player.vessel.cfg.assetName);
		}
		this.personalStatsLevel.text = NetworkScript.player.vessel.cfg.playerLevel.ToString();
		this.personalStatsName.text = (!NetworkScript.player.vessel.isGuest ? NetworkScript.player.vessel.playerName : StaticData.Translate("key_guest_player"));
	}

	public void UpdateParty()
	{
		bool flag;
		if (NetworkScript.party != null && NetworkScript.party.members.get_Count() > 1)
		{
			NetworkScript.partyInviters.Clear();
			this.HidePartyInvitationWindow();
			if (NetworkScript.party.members.get_Item(0).playerId != NetworkScript.player.playId)
			{
				NetworkScript.partyInvitees.Clear();
			}
			else if (NetworkScript.party.members.get_Count() >= 4)
			{
				NetworkScript.partyInvitees.Clear();
			}
		}
		if (NetworkScript.isInBase)
		{
			return;
		}
		if (NetworkScript.player != null && NetworkScript.player.shipScript != null)
		{
			NetworkScript.player.shipScript.ManagePartyMemberArrow();
		}
		if (NetworkScript.party != null)
		{
			flag = true;
		}
		else
		{
			flag = (NetworkScript.partyInvitees == null ? false : NetworkScript.partyInvitees.get_Count() > 0);
		}
		this.isPartyWindowNeeded = flag;
		if (this.isPartyWindowNeeded)
		{
			if (this.mainPartyWindow != null)
			{
				this.PinMainPatyWindowOnScreen();
			}
			else
			{
				this.ShowWindow();
			}
		}
		else if (this.mainPartyWindow != null)
		{
			this.HideMainWindow();
		}
	}

	private void UpdatePartyMemberStats(object prm)
	{
		if (NetworkScript.party == null)
		{
			this.mainPartyWindow.preDrawHandler = null;
			return;
		}
		foreach (PartyMemberClientSide member in NetworkScript.party.members)
		{
			if (member.playerId != NetworkScript.player.playId)
			{
				if (NetworkScript.clientSideClientsList.ContainsKey(member.playerId))
				{
					PlayerData item = NetworkScript.clientSideClientsList.get_Item(member.playerId);
					((GuiTexture)member.newShiel).boundries.set_width(item.vessel.cfg.shield / (float)item.vessel.cfg.shieldMax * 46f);
					((GuiTexture)member.newCorpus).boundries.set_width((float)item.vessel.cfg.hitPoints / (float)item.vessel.cfg.hitPointsMax * 46f);
				}
				else if (NetworkScript.partyMembersInfo.ContainsKey(member.playerId) && NetworkScript.partyMembersInfo.get_Item(member.playerId).lastUpdateTime.AddMilliseconds(1500) > StaticData.now)
				{
					((GuiTexture)member.newShiel).boundries.set_width(NetworkScript.partyMembersInfo.get_Item(member.playerId).shieldPercent * 46f);
					((GuiTexture)member.newCorpus).boundries.set_width(NetworkScript.partyMembersInfo.get_Item(member.playerId).corpusPercent * 46f);
				}
			}
		}
	}

	public void UpdatePartyOffers()
	{
		if ((NetworkScript.partyInviters == null ? true : NetworkScript.partyInviters.get_Count() <= 0))
		{
			this.HidePartyInvitationWindow();
		}
		else
		{
			this.ShowPartyInvitationWindow();
		}
	}

	public void UpdatePersonalStats()
	{
		this.personalStatsShield.boundries.set_width(NetworkScript.player.vessel.cfg.shield / (float)NetworkScript.player.vessel.cfg.shieldMax * 156f);
		this.personalStatsCorpus.boundries.set_width((float)NetworkScript.player.vessel.cfg.hitPoints / (float)NetworkScript.player.vessel.cfg.hitPointsMax * 103f);
		this.personalStatsCritical.boundries.set_width(NetworkScript.player.vessel.cfg.criticalEnergy / NetworkScript.player.vessel.cfg.criticalEnergyMax * 90f);
		if (!this.isPersonalStatsHoverd)
		{
			if (!NetworkScript.player.playerBelongings.isShowMoreDetailsOn)
			{
				this.shieldValue.text = string.Empty;
				this.corpusValue.text = string.Empty;
				this.criticalValue.text = string.Empty;
			}
			else
			{
				this.shieldValue.text = string.Format("{0}/{1}", (int)NetworkScript.player.vessel.cfg.shield, NetworkScript.player.vessel.cfg.shieldMax);
				this.corpusValue.text = string.Format("{0}/{1}", NetworkScript.player.vessel.cfg.hitPoints, NetworkScript.player.vessel.cfg.hitPointsMax);
				this.criticalValue.text = string.Format("{0}/{1}", Mathf.CeilToInt(NetworkScript.player.vessel.cfg.criticalEnergy), Mathf.CeilToInt(NetworkScript.player.vessel.cfg.criticalEnergyMax));
			}
		}
		this.shieldValueShadow.text = this.shieldValue.text;
		this.corpusValueShadow.text = this.corpusValue.text;
		this.criticalValueShadow.text = this.criticalValue.text;
	}
}