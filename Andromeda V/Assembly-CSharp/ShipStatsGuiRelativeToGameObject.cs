using System;
using System.Collections.Generic;
using UnityEngine;

public class ShipStatsGuiRelativeToGameObject : MonoBehaviour
{
	private const float WINDOW_WIDTH = 240f;

	private const float SHILD_BAR_WIDTH = 67f;

	private const float CORPUS_BAR_WIDTH = 67f;

	private const float ENERGY_BAR_WIDTH = 60f;

	private Vector3 screenPos;

	public PlayerObjectPhysics pop;

	private bool itsMe;

	private GuiWindow infoWindow;

	private GuiTexture barCorpus;

	private GuiTexture barShield;

	private GuiTexture barEnergy;

	private GuiTexture immuneTexture;

	private GuiLabel playerName;

	public ShipStatsGuiRelativeToGameObject()
	{
	}

	private void HidePlayerInfoInfo()
	{
		if (this.infoWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.infoWindow.handler);
			this.infoWindow = null;
			this.barCorpus = null;
			this.barShield = null;
			this.barEnergy = null;
		}
	}

	private void OnDisable()
	{
		this.HidePlayerInfoInfo();
	}

	public void Populate()
	{
		if (this.infoWindow != null)
		{
			this.HidePlayerInfoInfo();
			this.ShowPlayerInfo();
		}
	}

	private void PopulatePowerUps()
	{
		List<GuiElement> list = new List<GuiElement>();
		if (this.pop.cfg.haveLaserDamageFlat || this.pop.cfg.haveLaserDamagePercentage || this.pop.cfg.haveTotalDamagePercentage)
		{
			GuiTexture guiTexture = new GuiTexture();
			guiTexture.SetTexture("ShipStatsGui", "PowerUpLaser");
			guiTexture.X = 0f;
			guiTexture.Y = 0f;
			this.infoWindow.AddGuiElement(guiTexture);
			list.Add(guiTexture);
		}
		if (this.pop.cfg.havePlasmaDamageFlat || this.pop.cfg.havePlasmaDamagePercentage || this.pop.cfg.haveTotalDamagePercentage)
		{
			GuiTexture guiTexture1 = new GuiTexture();
			guiTexture1.SetTexture("ShipStatsGui", "PowerUpPlasma");
			guiTexture1.X = 0f;
			guiTexture1.Y = 0f;
			this.infoWindow.AddGuiElement(guiTexture1);
			list.Add(guiTexture1);
		}
		if (this.pop.cfg.haveIonDamageFlat || this.pop.cfg.haveIonDamagePercentage || this.pop.cfg.haveTotalDamagePercentage)
		{
			GuiTexture guiTexture2 = new GuiTexture();
			guiTexture2.SetTexture("ShipStatsGui", "PowerUpIon");
			guiTexture2.X = 0f;
			guiTexture2.Y = 0f;
			this.infoWindow.AddGuiElement(guiTexture2);
			list.Add(guiTexture2);
		}
		if (this.pop.cfg.haveCorpusFlat || this.pop.cfg.haveCorpusPercentage || this.pop.cfg.haveEndurancePercentage)
		{
			GuiTexture guiTexture3 = new GuiTexture();
			guiTexture3.SetTexture("ShipStatsGui", "PowerUpCorpus");
			guiTexture3.X = 0f;
			guiTexture3.Y = 0f;
			this.infoWindow.AddGuiElement(guiTexture3);
			list.Add(guiTexture3);
		}
		if (this.pop.cfg.haveShieldFlat || this.pop.cfg.haveShieldPercentage || this.pop.cfg.haveEndurancePercentage)
		{
			GuiTexture guiTexture4 = new GuiTexture();
			guiTexture4.SetTexture("ShipStatsGui", "PowerUpShield");
			guiTexture4.X = 0f;
			guiTexture4.Y = 0f;
			this.infoWindow.AddGuiElement(guiTexture4);
			list.Add(guiTexture4);
		}
		if (this.pop.cfg.haveShieldPowerFlat || this.pop.cfg.haveShieldPowerPercentage)
		{
			GuiTexture guiTexture5 = new GuiTexture();
			guiTexture5.SetTexture("ShipStatsGui", "PowerUpShieldPower");
			guiTexture5.X = 0f;
			guiTexture5.Y = 0f;
			this.infoWindow.AddGuiElement(guiTexture5);
			list.Add(guiTexture5);
		}
		if (this.pop.cfg.haveTargetingFlat || this.pop.cfg.haveTargetingPercentage)
		{
			GuiTexture guiTexture6 = new GuiTexture();
			guiTexture6.SetTexture("ShipStatsGui", "PowerUpTarget");
			guiTexture6.X = 0f;
			guiTexture6.Y = 0f;
			this.infoWindow.AddGuiElement(guiTexture6);
			list.Add(guiTexture6);
		}
		if (this.pop.cfg.haveAvoidanceFlat || this.pop.cfg.haveAvoidancePercentage)
		{
			GuiTexture guiTexture7 = new GuiTexture();
			guiTexture7.SetTexture("ShipStatsGui", "PowerUpAvoidance");
			guiTexture7.X = 0f;
			guiTexture7.Y = 0f;
			this.infoWindow.AddGuiElement(guiTexture7);
			list.Add(guiTexture7);
		}
		float single = (list.get_Count() <= 1 ? 224f : 240f - (float)(list.get_Count() * 16) - (float)((list.get_Count() - 1) * 2));
		single = single / 2f;
		foreach (GuiElement guiElement in list)
		{
			guiElement.X = single;
			single = single + 18f;
		}
	}

	private void ShowPlayerInfo()
	{
		if (this.infoWindow != null)
		{
			this.UpdatePlayerInfo();
			return;
		}
		string empty = string.Empty;
		Color poPColor = GuiNewStyleBar.blueColor;
		float single = 32f;
		this.infoWindow = new GuiWindow()
		{
			boundries = new Rect(-120f, 120f, 240f, 50f + single),
			isHidden = false,
			zOrder = 19,
			isClickTransparent = true
		};
		AndromedaGui.gui.AddWindow(this.infoWindow);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("ShipStatsGui", "frameBackground");
		guiTexture.X = 61f;
		guiTexture.Y = 15f + single;
		this.infoWindow.AddGuiElement(guiTexture);
		this.barShield = new GuiTexture();
		this.barShield.SetTexture("ShipStatsGui", "barShield");
		this.barShield.X = 86f;
		this.barShield.Y = 24f + single;
		this.infoWindow.AddGuiElement(this.barShield);
		this.barCorpus = new GuiTexture();
		this.barCorpus.SetTexture("ShipStatsGui", "barCorpus");
		this.barCorpus.X = 86f;
		this.barCorpus.Y = 32f + single;
		this.infoWindow.AddGuiElement(this.barCorpus);
		if (this.pop.get_IsPve())
		{
			GuiTexture guiTexture1 = new GuiTexture();
			guiTexture1.SetTexture("ShipStatsGui", "frameAliens");
			guiTexture1.X = 61f;
			guiTexture1.Y = 15f + single;
			this.infoWindow.AddGuiElement(guiTexture1);
			this.immuneTexture = new GuiTexture()
			{
				boundries = new Rect(70f, 11f + single, 100f, 40f)
			};
			this.infoWindow.AddGuiElement(this.immuneTexture);
			if (this.pop.isStunned)
			{
				this.immuneTexture.SetTextureKeepSize("ShipStatsGui", "stunMark");
			}
			else if (this.pop.isDisarmed)
			{
				this.immuneTexture.SetTextureKeepSize("ShipStatsGui", "disarmMark");
			}
			else if (this.pop.isShocked)
			{
				this.immuneTexture.SetTextureKeepSize("ShipStatsGui", "shockMark");
			}
			else if (this.pop.isImmuneToAllIncomingDamage)
			{
				this.immuneTexture.SetTextureKeepSize("ShipStatsGui", "immuneToAll");
			}
			else if (!this.pop.isImmuneToCrowd)
			{
				this.immuneTexture.SetTextureKeepSize("FrameworkGUI", "empty");
			}
			else
			{
				this.immuneTexture.SetTextureKeepSize("ShipStatsGui", "immuneToCrowd");
			}
			if (this.pop.galaxy.get_galaxyId() != 1000 || !(this.pop.playerName == "key_pve_aria"))
			{
				string str = StaticData.Translate(this.pop.playerName);
				if (((PvEPhysics)this.pop).isMinion)
				{
					str = string.Format(StaticData.Translate("key_pve_minion "), str);
				}
				if (this.pop.fractionId == NetworkScript.player.vessel.fractionId)
				{
					empty = string.Concat(new object[] { str, " [", ((PvEPhysics)this.pop).level, "]" });
					poPColor = GuiNewStyleBar.greenColor;
				}
				else if (this.pop.fractionId == 0)
				{
					if (((PvEPhysics)this.pop).agressionType != 1)
					{
						poPColor = (((PvEPhysics)this.pop).pveCommand != 1 ? GuiNewStyleBar.orangeColor : GuiNewStyleBar.redColor);
					}
					else
					{
						poPColor = GuiNewStyleBar.redColor;
					}
					empty = string.Concat(new object[] { str, " [", ((PvEPhysics)this.pop).level, "]" });
				}
				else
				{
					empty = string.Concat(new object[] { str, " [", ((PvEPhysics)this.pop).level, "]" });
					poPColor = GuiNewStyleBar.redColor;
				}
			}
			else
			{
				empty = StaticData.Translate(this.pop.playerName);
				poPColor = GuiNewStyleBar.blueColor;
			}
			GuiLabel guiLabel = new GuiLabel()
			{
				boundries = new Rect(1f, 16f, 240f, 32f),
				Alignment = 4,
				Font = GuiLabel.FontBold,
				FontSize = 14,
				text = empty,
				TextColor = Color.get_black()
			};
			this.infoWindow.AddGuiElement(guiLabel);
			this.playerName = new GuiLabel()
			{
				boundries = new Rect(0f, 15f, 240f, 32f),
				Alignment = 4,
				Font = GuiLabel.FontBold,
				FontSize = 14,
				text = empty,
				TextColor = poPColor
			};
			this.infoWindow.AddGuiElement(this.playerName);
			this.UpdatePlayerInfo();
			return;
		}
		if (this.pop.playerData == null || !this.pop.isGuest)
		{
			empty = (string.IsNullOrEmpty(this.pop.guildTag) ? string.Concat(new object[] { this.pop.playerName, " [", this.pop.cfg.playerLevel, "]" }) : string.Concat(new object[] { "[", this.pop.guildTag.ToUpper(), "] ", this.pop.playerName, " [", this.pop.cfg.playerLevel, "]" }));
		}
		else
		{
			empty = string.Concat(new object[] { StaticData.Translate("key_guest_player"), " [", this.pop.cfg.playerLevel, "]" });
		}
		poPColor = TargetingWnd.GetPoPColor(this.pop);
		string empty1 = string.Empty;
		if (this.pop.cfg.playerLevel >= 8)
		{
			switch (this.pop.playerLeague)
			{
				case 1:
				{
					empty1 = "Bronze";
					break;
				}
				case 2:
				{
					empty1 = "Silver";
					break;
				}
				case 3:
				{
					empty1 = "Gold";
					break;
				}
			}
			if (this.itsMe)
			{
				this.barEnergy = new GuiTexture();
				this.barEnergy.SetTexture("ShipStatsGui", "barEnergy");
				this.barEnergy.X = 90f;
				this.barEnergy.Y = 39f + single;
				this.infoWindow.AddGuiElement(this.barEnergy);
				GuiTexture guiTexture2 = new GuiTexture();
				guiTexture2.SetTexture("ShipStatsGui", string.Format("energy{0}", empty1));
				guiTexture2.X = 86f;
				guiTexture2.Y = 39f + single;
				this.infoWindow.AddGuiElement(guiTexture2);
			}
			empty1 = (!this.pop.inPvPRank ? string.Format("frame{0}Simple", empty1) : string.Format("frame{0}Wings", empty1));
		}
		else
		{
			if (this.itsMe)
			{
				this.barEnergy = new GuiTexture();
				this.barEnergy.SetTexture("ShipStatsGui", "barEnergy");
				this.barEnergy.X = 90f;
				this.barEnergy.Y = 39f + single;
				this.infoWindow.AddGuiElement(this.barEnergy);
				GuiTexture guiTexture3 = new GuiTexture();
				guiTexture3.SetTexture("ShipStatsGui", "energySilver");
				guiTexture3.X = 86f;
				guiTexture3.Y = 39f + single;
				this.infoWindow.AddGuiElement(guiTexture3);
			}
			empty1 = "frameAliens";
		}
		GuiTexture guiTexture4 = new GuiTexture();
		guiTexture4.SetTexture("ShipStatsGui", empty1);
		guiTexture4.X = 61f;
		guiTexture4.Y = 15f + single;
		this.infoWindow.AddGuiElement(guiTexture4);
		this.immuneTexture = new GuiTexture()
		{
			boundries = new Rect(70f, 11f + single, 100f, 40f)
		};
		this.infoWindow.AddGuiElement(this.immuneTexture);
		if (this.pop.isStunned)
		{
			this.immuneTexture.SetTextureKeepSize("ShipStatsGui", "stunMark");
		}
		else if (this.pop.isDisarmed)
		{
			this.immuneTexture.SetTextureKeepSize("ShipStatsGui", "disarmMark");
		}
		else if (this.pop.isShocked)
		{
			this.immuneTexture.SetTextureKeepSize("ShipStatsGui", "shockMark");
		}
		else if (this.pop.isImmuneToAllIncomingDamage)
		{
			this.immuneTexture.SetTextureKeepSize("ShipStatsGui", "immuneToAll");
		}
		else if (!this.pop.isImmuneToCrowd)
		{
			this.immuneTexture.SetTextureKeepSize("FrameworkGUI", "empty");
		}
		else
		{
			this.immuneTexture.SetTextureKeepSize("ShipStatsGui", "immuneToCrowd");
		}
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(1f, 17f, 240f, 32f),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 14,
			text = empty,
			TextColor = Color.get_black()
		};
		this.infoWindow.AddGuiElement(guiLabel1);
		this.playerName = new GuiLabel()
		{
			boundries = new Rect(0f, 16f, 240f, 32f),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 14,
			text = empty,
			TextColor = poPColor
		};
		this.infoWindow.AddGuiElement(this.playerName);
		this.UpdatePlayerInfo();
		this.PopulatePowerUps();
	}

	private void Start()
	{
		if (this.pop.neighbourhoodId == NetworkScript.player.vessel.neighbourhoodId)
		{
			this.itsMe = true;
		}
	}

	private void Update()
	{
		if (this.pop == null || this.pop.isInStealthMode && !NetworkScript.IsPartyMember(this.pop.playerId) && NetworkScript.player.vessel.playerId != this.pop.playerId)
		{
			this.HidePlayerInfoInfo();
			return;
		}
		this.screenPos = Camera.get_main().WorldToScreenPoint(base.get_transform().get_position());
		if (this.screenPos.x < 100f || this.screenPos.x > (float)(Screen.get_width() - 150) || this.screenPos.y < 50f || this.screenPos.y > (float)(Screen.get_height() - 100))
		{
			this.HidePlayerInfoInfo();
		}
		else
		{
			this.ShowPlayerInfo();
			this.infoWindow.boundries.set_x(this.screenPos.x - 120f);
			this.infoWindow.boundries.set_y((float)Screen.get_height() - this.screenPos.y - 120f);
		}
	}

	private void UpdatePlayerInfo()
	{
		Color color;
		this.barShield.boundries.set_width(this.pop.cfg.shield / (float)this.pop.cfg.shieldMax * 67f);
		this.barCorpus.boundries.set_width((float)this.pop.cfg.hitPoints / (float)this.pop.cfg.hitPointsMax * 67f);
		if (this.immuneTexture != null)
		{
			if (this.pop.isStunned)
			{
				this.immuneTexture.SetTextureKeepSize("ShipStatsGui", "stunMark");
			}
			else if (this.pop.isDisarmed)
			{
				this.immuneTexture.SetTextureKeepSize("ShipStatsGui", "disarmMark");
			}
			else if (this.pop.isShocked)
			{
				this.immuneTexture.SetTextureKeepSize("ShipStatsGui", "shockMark");
			}
			else if (this.pop.isImmuneToAllIncomingDamage)
			{
				this.immuneTexture.SetTextureKeepSize("ShipStatsGui", "immuneToAll");
			}
			else if (!this.pop.isImmuneToCrowd)
			{
				this.immuneTexture.SetTextureKeepSize("FrameworkGUI", "empty");
			}
			else
			{
				this.immuneTexture.SetTextureKeepSize("ShipStatsGui", "immuneToCrowd");
			}
		}
		if (!this.pop.get_IsPve() && this.playerName != null)
		{
			this.playerName.TextColor = TargetingWnd.GetPoPColor(this.pop);
		}
		if (this.pop.galaxy.get_galaxyId() != 1000 && this.pop.get_IsPve() && this.pop.fractionId == 0 && ((PvEPhysics)this.pop).agressionType == 0)
		{
			color = (((PvEPhysics)this.pop).pveCommand != 1 ? GuiNewStyleBar.orangeColor : GuiNewStyleBar.redColor);
			if (this.playerName != null)
			{
				this.playerName.TextColor = color;
			}
		}
		if (this.itsMe)
		{
			this.barEnergy.boundries.set_width(this.pop.cfg.criticalEnergy / this.pop.cfg.criticalEnergyMax * 60f);
		}
	}
}