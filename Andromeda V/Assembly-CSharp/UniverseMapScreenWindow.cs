using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TransferableObjects;
using UnityEngine;

public class UniverseMapScreenWindow : GuiWindow
{
	private byte selectedScreen = 1;

	private List<GuiElement> forDelete = new List<GuiElement>();

	private List<GuiElement> galaxyInfoForDelete = new List<GuiElement>();

	private GuiButtonFixed infoBtn;

	private GuiButtonFixed leftMenuBtn;

	private GuiButtonFixed rightMenuBtn;

	private GuiScrollingContainer epScroller;

	private LevelMap onDysplayMap;

	private bool isInfoOnScren;

	public UniverseMapScreenWindow()
	{
	}

	private void ClearContent()
	{
		foreach (GuiElement guiElement in this.forDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.forDelete.Clear();
		this.ClearGalaxyInfo();
	}

	private void ClearGalaxyInfo()
	{
		foreach (GuiElement guiElement in this.galaxyInfoForDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.galaxyInfoForDelete.Clear();
	}

	public override void Create()
	{
		UniverseMapScreenWindow.<Create>c__AnonStorey75 variable = null;
		base.SetBackgroundTexture("PoiScreenWindow", "frame");
		base.PutToCenter();
		this.zOrder = 210;
		this.isHidden = false;
		this.infoBtn = new GuiButtonFixed();
		this.infoBtn.SetTexture("PoiScreenWindow", "btn-info");
		this.infoBtn.X = 0f;
		this.infoBtn.Y = 4f;
		this.infoBtn.Caption = string.Empty;
		this.infoBtn.Clicked = null;
		base.AddGuiElement(this.infoBtn);
		this.leftMenuBtn = new GuiButtonFixed();
		this.leftMenuBtn.SetTexture("UniverseMapScreen", "btnLeft");
		this.leftMenuBtn.X = 66f;
		this.leftMenuBtn.Y = 4f;
		this.leftMenuBtn.Caption = "FACTION PORTALS";
		this.leftMenuBtn.FontSize = 14;
		this.leftMenuBtn.SetRegularFont();
		this.leftMenuBtn.SetColor(GuiNewStyleBar.blueColor);
		this.leftMenuBtn.MarginRight = 10;
		this.leftMenuBtn.Alignment = 5;
		this.leftMenuBtn.eventHandlerParam.customData = (byte)3;
		this.leftMenuBtn.Clicked = new Action<EventHandlerParam>(this, UniverseMapScreenWindow.OnScreenButtonClicked);
		base.AddGuiElement(this.leftMenuBtn);
		this.rightMenuBtn = new GuiButtonFixed();
		this.rightMenuBtn.SetTexture("UniverseMapScreen", "btnRight");
		this.rightMenuBtn.X = 607f;
		this.rightMenuBtn.Y = 4f;
		this.rightMenuBtn.Caption = "TRANSFORMER PORTALS";
		this.rightMenuBtn.FontSize = 14;
		this.rightMenuBtn.SetRegularFont();
		this.rightMenuBtn.SetColor(GuiNewStyleBar.blueColor);
		this.rightMenuBtn._marginLeft = 10;
		this.rightMenuBtn.Alignment = 3;
		this.rightMenuBtn.eventHandlerParam.customData = (byte)2;
		this.rightMenuBtn.Clicked = new Action<EventHandlerParam>(this, UniverseMapScreenWindow.OnScreenButtonClicked);
		base.AddGuiElement(this.rightMenuBtn);
		if (NetworkScript.player.vessel.galaxy.get_galaxyId() > 1000 && NetworkScript.player.vessel.galaxy.get_galaxyId() < 2000)
		{
			Enumerable.First<HyperJumpNet>(NetworkScript.player.vessel.galaxy.hyperJumps);
			LevelMap levelMap = Enumerable.First<LevelMap>(Enumerable.Where<LevelMap>(StaticData.allGalaxies, new Func<LevelMap, bool>(variable, (LevelMap g) => g.get_galaxyId() == this.hj.galaxyDst)));
			if (this.onDysplayMap == null)
			{
				this.onDysplayMap = levelMap;
			}
		}
		else if (this.onDysplayMap == null)
		{
			LevelMap[] levelMapArray = StaticData.allGalaxies;
			if (UniverseMapScreenWindow.<>f__am$cache9 == null)
			{
				UniverseMapScreenWindow.<>f__am$cache9 = new Func<LevelMap, bool>(null, (LevelMap t) => t.get_galaxyId() == NetworkScript.player.vessel.galaxy.get_galaxyId());
			}
			this.onDysplayMap = Enumerable.First<LevelMap>(Enumerable.Where<LevelMap>(levelMapArray, UniverseMapScreenWindow.<>f__am$cache9));
		}
		if (this.onDysplayMap.get_galaxyId() > 4000)
		{
			this.selectedScreen = 3;
		}
		else if (this.onDysplayMap.get_galaxyId() > 3000 || this.onDysplayMap.IsUltraGalaxyInstance())
		{
			this.selectedScreen = 2;
		}
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = this.selectedScreen
		};
		this.OnScreenButtonClicked(eventHandlerParam);
	}

	private void DrawEpInfo(ExtractionPointStateInfo[] epsArray)
	{
		UniverseMapScreenWindow.<DrawEpInfo>c__AnonStorey78 variable = null;
		int num = 0;
		int num1 = 0;
		int num2 = 0;
		int num3 = 0;
		Color color = GuiNewStyleBar.blueColor;
		Font fontMedium = GuiLabel.FontMedium;
		for (int i = 0; i < (int)epsArray.Length; i++)
		{
			num2 = num2 + epsArray[i].yourIncome;
			num3 = num3 + epsArray[i].guildIncome;
			if (epsArray[i].ownerFaction == 1)
			{
				num = num + epsArray[i].tottalViralIncome;
			}
			else if (epsArray[i].ownerFaction == 2)
			{
				num1 = num1 + epsArray[i].tottalViralIncome;
			}
			color = (epsArray[i].yourIncome == 0 ? GuiNewStyleBar.blueColor : GuiNewStyleBar.greenColor);
			float single = -18f;
			ExtractionPointInfo extractionPointInfo = Enumerable.FirstOrDefault<ExtractionPointInfo>(Enumerable.Where<ExtractionPointInfo>(StaticData.allExtractionPoints, new Func<ExtractionPointInfo, bool>(variable, (ExtractionPointInfo t) => t.id == this.<>f__ref$118.epsArray[this.<>f__ref$119.i].epId)));
			if (extractionPointInfo != null)
			{
				LevelMap levelMap = Enumerable.FirstOrDefault<LevelMap>(Enumerable.Where<LevelMap>(StaticData.allGalaxies, new Func<LevelMap, bool>(variable, (LevelMap t) => t.get_galaxyId() == this.epi.galaxyId)));
				if (levelMap != null)
				{
					GuiTexture guiTexture = new GuiTexture();
					if (i % 2 != 0)
					{
						guiTexture.SetTexture("UniverseMapScreen", "rowLight");
					}
					else
					{
						guiTexture.SetTexture("UniverseMapScreen", "rowDark");
					}
					guiTexture.X = 0f;
					guiTexture.Y = (float)(i * 36);
					this.epScroller.AddContent(guiTexture);
					this.forDelete.Add(guiTexture);
					GuiLabel guiLabel = new GuiLabel()
					{
						boundries = new Rect(33f + single, (float)(i * 36), 24f, 36f),
						Alignment = 3,
						text = epsArray[i].epId.ToString(),
						FontSize = 12,
						TextColor = color,
						Font = fontMedium
					};
					this.epScroller.AddContent(guiLabel);
					this.forDelete.Add(guiLabel);
					GuiLabel guiLabel1 = new GuiLabel()
					{
						boundries = new Rect(113f + single, (float)(i * 36), 100f, 36f),
						Alignment = 3,
						text = StaticData.Translate(extractionPointInfo.name),
						FontSize = 12,
						TextColor = color,
						Font = fontMedium
					};
					this.epScroller.AddContent(guiLabel1);
					this.forDelete.Add(guiLabel1);
					GuiLabel guiLabel2 = new GuiLabel()
					{
						boundries = new Rect(400f + single, (float)(i * 36), 105f, 36f),
						Alignment = 3,
						text = (!epsArray[i].isVulnerable ? StaticData.Translate("key_extraction_point_status_immune") : StaticData.Translate("key_extraction_point_status_vulnerable")),
						FontSize = 12,
						TextColor = (!epsArray[i].isVulnerable ? GuiNewStyleBar.aquamarineColor : GuiNewStyleBar.orangeColor),
						Font = GuiLabel.FontBold
					};
					this.epScroller.AddContent(guiLabel2);
					this.forDelete.Add(guiLabel2);
					TimeSpan timeSpan = epsArray[i].until - StaticData.now;
					if (timeSpan.get_TotalSeconds() > 1)
					{
						GuiTimeTracker guiTimeTracker = new GuiTimeTracker((int)timeSpan.get_TotalSeconds(), this.epScroller, new Rect(518f + single, (float)(i * 36), 108f, 36f))
						{
							FontSize = 12,
							TextColor = color
						};
						guiLabel.Font = fontMedium;
						guiTimeTracker.SetEndAction(new Action(this, UniverseMapScreenWindow.RefreshExtractionPointInfoScreen));
						guiTimeTracker.Alignment = 3;
					}
					GuiLabel guiLabel3 = new GuiLabel()
					{
						boundries = new Rect(230f + single, (float)(i * 36), 155f, 36f),
						Alignment = 0,
						text = string.Concat(StaticData.Translate(levelMap.nameUI), string.Format("\n[{0}:{1}]", this.onDysplayMap.width / 2 + extractionPointInfo.possitionX, this.onDysplayMap.height / 2 - extractionPointInfo.possitionZ)),
						FontSize = 12,
						TextColor = color,
						Font = fontMedium
					};
					this.epScroller.AddContent(guiLabel3);
					this.forDelete.Add(guiLabel3);
					string.Format("[{0}:{1}]", this.onDysplayMap.width / 2 + extractionPointInfo.possitionX, this.onDysplayMap.height / 2 - extractionPointInfo.possitionZ);
					GuiTexture guiTexture1 = new GuiTexture();
					guiTexture1.SetTexture("UniverseMapScreen", string.Format("f{0}", epsArray[i].ownerFaction));
					guiTexture1.X = 70f + single;
					guiTexture1.Y = (float)(5 + i * 36);
					this.epScroller.AddContent(guiTexture1);
					this.forDelete.Add(guiTexture1);
					GuiLabel guiLabel4 = new GuiLabel()
					{
						boundries = new Rect(639f + single, (float)(i * 36), 33f, 36f),
						Alignment = 3,
						text = epsArray[i].contributors.ToString(),
						FontSize = 12,
						TextColor = color,
						Font = fontMedium
					};
					this.epScroller.AddContent(guiLabel4);
					this.forDelete.Add(guiLabel4);
					GuiLabel guiLabel5 = new GuiLabel()
					{
						boundries = new Rect(686f + single, (float)(i * 36), 90f, 36f),
						Alignment = 3,
						text = string.Format(StaticData.Translate("key_new_universe_map_info_screen_income"), epsArray[i].yourIncome, epsArray[i].tottalViralIncome),
						FontSize = 12,
						TextColor = color,
						Font = fontMedium
					};
					this.epScroller.AddContent(guiLabel5);
					this.forDelete.Add(guiLabel5);
					GuiLabel guiLabel6 = new GuiLabel()
					{
						boundries = new Rect(790f + single, (float)(i * 36), 90f, 36f),
						Alignment = 3,
						text = string.Format(StaticData.Translate("key_new_universe_map_info_screen_income"), epsArray[i].guildIncome, epsArray[i].tottalGuildIncome),
						FontSize = 12,
						TextColor = color,
						Font = fontMedium
					};
					this.epScroller.AddContent(guiLabel6);
					this.forDelete.Add(guiLabel6);
				}
			}
		}
		GuiLabel guiLabel7 = new GuiLabel()
		{
			boundries = new Rect(33f, 476f, 90f, 36f),
			Alignment = 3,
			text = StaticData.Translate("key_new_universe_map_info_screen_lbl_overall"),
			FontSize = 14,
			TextColor = GuiNewStyleBar.blueColor,
			Font = fontMedium
		};
		guiLabel7.boundries.set_width(guiLabel7.TextWidth);
		base.AddGuiElement(guiLabel7);
		this.forDelete.Add(guiLabel7);
		GuiTexture _xMax = new GuiTexture();
		_xMax.SetTexture("UniverseMapScreen", "f2");
		_xMax.X = guiLabel7.boundries.get_xMax() + 10f;
		_xMax.Y = 480f;
		base.AddGuiElement(_xMax);
		this.forDelete.Add(_xMax);
		GuiLabel guiLabel8 = new GuiLabel()
		{
			boundries = new Rect(_xMax.boundries.get_xMax() + 10f, 476f, 90f, 36f),
			Alignment = 3,
			text = string.Format(StaticData.Translate("key_new_universe_map_info_screen_faction_income"), num1),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			Font = fontMedium
		};
		base.AddGuiElement(guiLabel8);
		this.forDelete.Add(guiLabel8);
		GuiTexture _xMax1 = new GuiTexture();
		_xMax1.SetTexture("UniverseMapScreen", "f1");
		_xMax1.X = guiLabel8.boundries.get_xMax() + 10f;
		_xMax1.Y = 480f;
		base.AddGuiElement(_xMax1);
		this.forDelete.Add(_xMax1);
		GuiLabel guiLabel9 = new GuiLabel()
		{
			boundries = new Rect(_xMax1.boundries.get_xMax() + 10f, 476f, 90f, 36f),
			Alignment = 3,
			text = string.Format(StaticData.Translate("key_new_universe_map_info_screen_faction_income"), num),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			Font = fontMedium
		};
		guiLabel9.boundries.set_width(guiLabel9.TextWidth);
		base.AddGuiElement(guiLabel9);
		this.forDelete.Add(guiLabel9);
		GuiLabel guiLabel10 = new GuiLabel()
		{
			boundries = new Rect(686f, 476f, 90f, 36f),
			Alignment = 3,
			text = string.Format(StaticData.Translate("key_new_universe_map_info_screen_faction_income"), num2),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			Font = fontMedium
		};
		base.AddGuiElement(guiLabel10);
		this.forDelete.Add(guiLabel10);
		GuiLabel guiLabel11 = new GuiLabel()
		{
			boundries = new Rect(790f, 476f, 90f, 36f),
			Alignment = 3,
			text = string.Format(StaticData.Translate("key_new_universe_map_info_screen_faction_income"), num3),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor,
			Font = fontMedium
		};
		base.AddGuiElement(guiLabel11);
		this.forDelete.Add(guiLabel11);
	}

	private void InitOnDysplayMap()
	{
		UniverseMapScreenWindow.<InitOnDysplayMap>c__AnonStorey7E variable = null;
		if (NetworkScript.player.vessel.galaxy.get_galaxyId() <= 1000 || NetworkScript.player.vessel.galaxy.get_galaxyId() >= 2000)
		{
			LevelMap[] levelMapArray = StaticData.allGalaxies;
			if (UniverseMapScreenWindow.<>f__am$cacheF == null)
			{
				UniverseMapScreenWindow.<>f__am$cacheF = new Func<LevelMap, bool>(null, (LevelMap t) => t.get_galaxyId() == NetworkScript.player.vessel.galaxy.get_galaxyId());
			}
			this.onDysplayMap = Enumerable.First<LevelMap>(Enumerable.Where<LevelMap>(levelMapArray, UniverseMapScreenWindow.<>f__am$cacheF));
		}
		else
		{
			Enumerable.First<HyperJumpNet>(NetworkScript.player.vessel.galaxy.hyperJumps);
			LevelMap levelMap = Enumerable.First<LevelMap>(Enumerable.Where<LevelMap>(StaticData.allGalaxies, new Func<LevelMap, bool>(variable, (LevelMap g) => g.get_galaxyId() == this.hj.galaxyDst)));
			this.onDysplayMap = levelMap;
		}
		if (this.onDysplayMap.get_galaxyId() > 4000)
		{
			if (this.selectedScreen != 3)
			{
				this.onDysplayMap = null;
			}
		}
		else if (this.onDysplayMap.get_galaxyId() <= 3000 && !this.onDysplayMap.IsUltraGalaxyInstance())
		{
			if (this.selectedScreen != 1)
			{
				this.onDysplayMap = null;
			}
		}
		else if (this.selectedScreen != 2)
		{
			this.onDysplayMap = null;
		}
	}

	private void MakeGalaxyIcons(LevelMap map)
	{
		UniverseMapScreenWindow.<MakeGalaxyIcons>c__AnonStorey79 variable = null;
		UniverseMapScreenWindow.<MakeGalaxyIcons>c__AnonStorey7A variable1 = null;
		int num;
		bool flag = (map.get_galaxyId() <= 3000 || map.get_galaxyId() >= 4000 ? map.IsUltraGalaxyInstance() : true);
		bool _galaxyId = map.get_galaxyId() > 4000;
		Portal portal = null;
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTextureNormal("FrameworkGUI", "empty");
		guiButtonFixed.SetTextureHover("UniverseMapScreen", "hover");
		guiButtonFixed.SetTextureClicked("UniverseMapScreen", "selected");
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.groupId = 123;
		guiButtonFixed.behaviourKeepClicked = true;
		guiButtonFixed.eventHandlerParam.customData = map;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, UniverseMapScreenWindow.OnGalaxyClicked);
		guiButtonFixed.IsClicked = (!_galaxyId ? map.get_galaxyId() == this.onDysplayMap.get_galaxyId() : map.galaxyKey == this.onDysplayMap.galaxyKey);
		base.AddGuiElement(guiButtonFixed);
		this.forDelete.Add(guiButtonFixed);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(0f, 0f, 90f, 26f),
			text = StaticData.Translate(map.nameUI),
			Alignment = 4,
			FontSize = 12,
			TextColor = (map.isPveMap ? GuiNewStyleBar.blueColor : GuiNewStyleBar.orangeColor)
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiLabel rect = new GuiLabel();
		GuiLabel guiLabel1 = new GuiLabel();
		if (_galaxyId)
		{
			string empty = string.Empty;
			string str = string.Empty;
			switch (map.galaxyKey)
			{
				case 41:
				{
					empty = string.Format(StaticData.Translate("key_faction_galaxy_bonus_xp_gain"), 20);
					break;
				}
				case 42:
				{
					empty = string.Format(StaticData.Translate("key_faction_galaxy_bonus_resources_drop"), 100);
					break;
				}
				case 43:
				{
					empty = string.Format(StaticData.Translate("key_faction_galaxy_bonus_sell_price"), 100);
					break;
				}
				case 44:
				{
					empty = StaticData.Translate("key_faction_galaxy_bonus_better_drop");
					break;
				}
				case 45:
				{
					empty = string.Format(StaticData.Translate("key_faction_galaxy_bonus_research_point"), 50);
					str = string.Format(StaticData.Translate("key_faction_galaxy_bonus_defending"), 100);
					break;
				}
			}
			rect.boundries = new Rect(0f, 0f, 90f, 26f);
			rect.text = empty;
			rect.Alignment = 4;
			rect.FontSize = 11;
			rect.TextColor = GuiNewStyleBar.blueColor;
			base.AddGuiElement(rect);
			this.forDelete.Add(rect);
			guiLabel1.boundries = new Rect(0f, 0f, 90f, 26f);
			guiLabel1.text = str;
			guiLabel1.Alignment = 4;
			guiLabel1.FontSize = 11;
			guiLabel1.TextColor = GuiNewStyleBar.blueColor;
			base.AddGuiElement(guiLabel1);
			this.forDelete.Add(guiLabel1);
		}
		else if (flag)
		{
			portal = Enumerable.First<Portal>(Enumerable.Where<Portal>(StaticData.allPortals, new Func<Portal, bool>(variable, (Portal t) => t.galaxyKey == this.map.galaxyKey)));
			int item = 0;
			int num1 = 0;
			for (int i = 0; i < portal.parts.get_Count(); i++)
			{
				ushort item1 = portal.parts.get_Keys().get_Item(i);
				item = item + portal.parts.get_Item(item1);
				PortalPart portalPart = Enumerable.FirstOrDefault<PortalPart>(Enumerable.Where<PortalPart>(NetworkScript.player.playerBelongings.playerItems.portalParts, new Func<PortalPart, bool>(variable1, (PortalPart t) => (t.portalId != this.<>f__ref$121.portalForGalaxy.portalId ? false : t.partTypeId == this.partType))));
				if (portalPart == null)
				{
					num = 0;
				}
				else
				{
					num = portalPart.partAmount;
				}
				int num2 = num;
				num2 = Math.Min(num2, portal.parts.get_Item(item1));
				num1 = num1 + num2;
			}
			rect.boundries = new Rect(0f, 0f, 90f, 26f);
			rect.text = string.Format(StaticData.Translate("key_new_universe_map_lbl_parts_collected"), num1, item);
			rect.Alignment = 4;
			rect.FontSize = 12;
			rect.TextColor = (map.isPveMap ? GuiNewStyleBar.blueColor : GuiNewStyleBar.orangeColor);
			base.AddGuiElement(rect);
			this.forDelete.Add(rect);
		}
		if (UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.ContainsKey(map.minimapAssetName))
		{
			UniverseMapScreenWindow.GalaxyPossitions galaxyPossition = UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.get_Item(map.minimapAssetName);
			guiLabel.boundries = new Rect(galaxyPossition.titleX, galaxyPossition.titleY, galaxyPossition.titleW, galaxyPossition.titleH);
			guiButtonFixed.boundries = new Rect(galaxyPossition.indicationX, galaxyPossition.indicationY, 90f, 90f);
			if (_galaxyId)
			{
				rect.boundries = new Rect(galaxyPossition.titleX, galaxyPossition.titleY + 124f, galaxyPossition.titleW, galaxyPossition.titleH);
				guiLabel1.boundries = new Rect(galaxyPossition.titleX, galaxyPossition.titleY + 124f + galaxyPossition.titleH, galaxyPossition.titleW, galaxyPossition.titleH);
				GuiTexture guiTexture = new GuiTexture();
				switch (NetworkScript.player.factionGalaxyOwnership.get_Item((byte)map.galaxyKey))
				{
					case 0:
					{
						guiTexture.SetTexture("WarScreenWindow", "contestedStamp");
						break;
					}
					case 1:
					{
						guiTexture.SetTexture("WarScreenWindow", "vindexisStamp");
						break;
					}
					case 2:
					{
						guiTexture.SetTexture("WarScreenWindow", "regiaStamp");
						break;
					}
				}
				guiTexture.X = guiButtonFixed.X + 4f;
				guiTexture.Y = guiButtonFixed.Y + 15f;
				base.AddGuiElement(guiTexture);
				this.forDelete.Add(guiTexture);
			}
			else if (flag)
			{
				rect.boundries = new Rect(galaxyPossition.titleX, galaxyPossition.titleY + 124f, galaxyPossition.titleW, galaxyPossition.titleH);
				if (!NetworkScript.player.playerBelongings.playerItems.HaveAllPartsForPortal(portal.portalId) || !NetworkScript.player.playerBelongings.unlockedPortals.Contains(portal.portalId))
				{
					GuiTexture guiTexture1 = new GuiTexture();
					guiTexture1.SetTexture("UniverseMapScreen", "locked");
					guiTexture1.boundries = guiButtonFixed.boundries;
					base.AddGuiElement(guiTexture1);
					this.forDelete.Add(guiTexture1);
				}
			}
		}
		if (!flag && !_galaxyId && (NetworkScript.player.playerBelongings.playerAccessLevel < map.accessLevel || !map.isPveMap && (NetworkScript.player.playerBelongings.playerLevel > map.reqMaxLevel || NetworkScript.player.playerBelongings.playerLevel < map.reqMinLevel)))
		{
			GuiTexture guiTexture2 = new GuiTexture();
			guiTexture2.SetTexture("UniverseMapScreen", "locked");
			guiTexture2.boundries = guiButtonFixed.boundries;
			base.AddGuiElement(guiTexture2);
			this.forDelete.Add(guiTexture2);
		}
	}

	private void OnEqJumpBtnClick(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void UniverseMapScreenWindow::OnEqJumpBtnClick(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnEqJumpBtnClick(EventHandlerParam)
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

	private void OnExtractionPointInfoClick(object prm)
	{
		playWebGame.udp.ExecuteCommand(187, null);
		this.isInfoOnScren = true;
		this.ClearContent();
		this.infoBtn.SetTexture("PoiScreenWindow", "btnInfoBack");
		this.infoBtn.eventHandlerParam.customData = this.selectedScreen;
		this.infoBtn.Clicked = new Action<EventHandlerParam>(this, UniverseMapScreenWindow.OnScreenButtonClicked);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(147f, 8f, 618f, 36f),
			Alignment = 4,
			text = StaticData.Translate("key_new_universe_map_lbl_information"),
			FontSize = 20,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("UniverseMapScreen", "infoFrame");
		guiTexture.X = 15f;
		guiTexture.Y = 53f;
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(35f, 71f, 845f, 60f),
			text = StaticData.Translate("key_new_universe_map_info_screen_description"),
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel1);
		this.forDelete.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(33f, 155f, 24f, 20f),
			Alignment = 3,
			text = "#",
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel2);
		this.forDelete.Add(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(113f, 155f, 100f, 20f),
			Alignment = 3,
			text = StaticData.Translate("key_new_universe_map_info_screen_lbl_name"),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel3);
		this.forDelete.Add(guiLabel3);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			boundries = new Rect(400f, 155f, 105f, 20f),
			Alignment = 3,
			text = StaticData.Translate("key_new_universe_map_info_screen_lbl_status"),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel4);
		this.forDelete.Add(guiLabel4);
		GuiLabel guiLabel5 = new GuiLabel()
		{
			boundries = new Rect(518f, 155f, 108f, 20f),
			Alignment = 3,
			text = StaticData.Translate("key_new_universe_map_info_screen_lbl_time_left"),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel5);
		this.forDelete.Add(guiLabel5);
		GuiLabel guiLabel6 = new GuiLabel()
		{
			boundries = new Rect(230f, 155f, 155f, 20f),
			Alignment = 3,
			text = StaticData.Translate("key_new_universe_map_info_screen_lbl_galaxy"),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel6);
		this.forDelete.Add(guiLabel6);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("UniverseMapScreen", "factions");
		guiTexture1.X = 74f;
		guiTexture1.Y = 159f;
		base.AddGuiElement(guiTexture1);
		this.forDelete.Add(guiTexture1);
		GuiTexture guiTexture2 = new GuiTexture();
		guiTexture2.SetTexture("UniverseMapScreen", "contributors");
		guiTexture2.X = 646f;
		guiTexture2.Y = 159f;
		base.AddGuiElement(guiTexture2);
		this.forDelete.Add(guiTexture2);
		GuiTexture guiTexture3 = new GuiTexture();
		guiTexture3.SetTexture("UniverseMapScreen", "eIncome");
		guiTexture3.X = 711f;
		guiTexture3.Y = 159f;
		base.AddGuiElement(guiTexture3);
		this.forDelete.Add(guiTexture3);
		GuiTexture guiTexture4 = new GuiTexture();
		guiTexture4.SetTexture("UniverseMapScreen", "uIncome");
		guiTexture4.X = 816f;
		guiTexture4.Y = 159f;
		base.AddGuiElement(guiTexture4);
		this.forDelete.Add(guiTexture4);
		this.epScroller = new GuiScrollingContainer(18f, 186f, 878f, 288f, 1, this);
		this.epScroller.SetArrowStep(36f);
		base.AddGuiElement(this.epScroller);
		this.forDelete.Add(this.epScroller);
	}

	private void OnFactionGalaxyJumpBackClicked(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void UniverseMapScreenWindow::OnFactionGalaxyJumpBackClicked(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnFactionGalaxyJumpBackClicked(EventHandlerParam)
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

	private void OnFactionGalaxyJumpClicked(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void UniverseMapScreenWindow::OnFactionGalaxyJumpClicked(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnFactionGalaxyJumpClicked(EventHandlerParam)
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

	private void OnFactionPortalClicked()
	{
		this.ClearContent();
		this.leftMenuBtn.Caption = StaticData.Translate("key_new_universe_map_lbl_transformer_portals");
		this.leftMenuBtn.eventHandlerParam.customData = (byte)2;
		this.leftMenuBtn.Clicked = new Action<EventHandlerParam>(this, UniverseMapScreenWindow.OnScreenButtonClicked);
		this.rightMenuBtn.Caption = StaticData.Translate("key_new_universe_map_lbl_universe_map");
		this.rightMenuBtn.eventHandlerParam.customData = (byte)1;
		this.rightMenuBtn.Clicked = new Action<EventHandlerParam>(this, UniverseMapScreenWindow.OnScreenButtonClicked);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(147f, 8f, 618f, 36f),
			Alignment = 4,
			text = StaticData.Translate("key_new_universe_map_lbl_faction_portals"),
			FontSize = 20,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("UniverseMapScreen", "warPortalsFrame");
		guiTexture.X = 14f;
		guiTexture.Y = 49f;
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		LevelMap[] levelMapArray = StaticData.allGalaxies;
		if (UniverseMapScreenWindow.<>f__am$cacheC == null)
		{
			UniverseMapScreenWindow.<>f__am$cacheC = new Func<LevelMap, bool>(null, (LevelMap t) => (t.galaxyKey == 41 || t.galaxyKey == 42 || t.galaxyKey == 43 || t.galaxyKey == 44 || t.galaxyKey == 45 ? t.get_galaxyId() % 100 == 10 : false));
		}
		LevelMap[] array = Enumerable.ToArray<LevelMap>(Enumerable.Where<LevelMap>(levelMapArray, UniverseMapScreenWindow.<>f__am$cacheC));
		this.InitOnDysplayMap();
		if (this.onDysplayMap == null || this.onDysplayMap.galaxyKey != 41 && this.onDysplayMap.galaxyKey != 42 && this.onDysplayMap.galaxyKey != 43 && this.onDysplayMap.galaxyKey != 44 && this.onDysplayMap.galaxyKey != 45)
		{
			this.onDysplayMap = array[0];
		}
		LevelMap[] levelMapArray1 = array;
		for (int i = 0; i < (int)levelMapArray1.Length; i++)
		{
			this.MakeGalaxyIcons(levelMapArray1[i]);
		}
		this.PutPlayerIndication();
	}

	private void OnFactionWarBtnClicked(object prm)
	{
		MainScreenWindow mainScreenWindow = AndromedaGui.mainWnd;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = (byte)34
		};
		mainScreenWindow.OnWindowBtnClicked(eventHandlerParam);
	}

	private void OnGalaxyClicked(EventHandlerParam prm)
	{
		if (prm == null || prm.customData == null)
		{
			return;
		}
		this.onDysplayMap = (LevelMap)prm.customData;
		this.PopulateGalaxyInfo();
	}

	private void OnJumpBackClicked(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void UniverseMapScreenWindow::OnJumpBackClicked(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnJumpBackClicked(EventHandlerParam)
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

	private void OnNovaJumpBtnClick(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void UniverseMapScreenWindow::OnNovaJumpBtnClick(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnNovaJumpBtnClick(EventHandlerParam)
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

	private void OnScreenButtonClicked(EventHandlerParam prm)
	{
		this.infoBtn.Clicked = new Action<EventHandlerParam>(this, UniverseMapScreenWindow.OnExtractionPointInfoClick);
		this.infoBtn.SetTexture("PoiScreenWindow", "btn-info");
		this.isInfoOnScren = false;
		if (prm == null || prm.customData == null)
		{
			return;
		}
		this.selectedScreen = (byte)prm.customData;
		switch (this.selectedScreen)
		{
			case 1:
			{
				this.OnUniverseMapClicked();
				break;
			}
			case 2:
			{
				this.OnTransformerPortalClicked();
				break;
			}
			case 3:
			{
				this.OnFactionPortalClicked();
				break;
			}
		}
	}

	private void OnTransformerBtnClicked(object prm)
	{
		MainScreenWindow mainScreenWindow = AndromedaGui.mainWnd;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = (byte)22
		};
		mainScreenWindow.OnWindowBtnClicked(eventHandlerParam);
	}

	private void OnTransformerJumpClicked(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void UniverseMapScreenWindow::OnTransformerJumpClicked(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnTransformerJumpClicked(EventHandlerParam)
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

	private void OnTransformerPortalClicked()
	{
		this.ClearContent();
		this.leftMenuBtn.Caption = StaticData.Translate("key_new_universe_map_lbl_universe_map");
		this.leftMenuBtn.eventHandlerParam.customData = (byte)1;
		this.leftMenuBtn.Clicked = new Action<EventHandlerParam>(this, UniverseMapScreenWindow.OnScreenButtonClicked);
		this.rightMenuBtn.Caption = StaticData.Translate("key_new_universe_map_lbl_faction_portals");
		this.rightMenuBtn.eventHandlerParam.customData = (byte)3;
		this.rightMenuBtn.Clicked = new Action<EventHandlerParam>(this, UniverseMapScreenWindow.OnScreenButtonClicked);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(147f, 8f, 618f, 36f),
			Alignment = 4,
			text = StaticData.Translate("key_new_universe_map_lbl_transformer_portals"),
			FontSize = 20,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("UniverseMapScreen", "ultraMapFrame");
		guiTexture.X = 14f;
		guiTexture.Y = 87f;
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		LevelMap[] levelMapArray = StaticData.allGalaxies;
		if (UniverseMapScreenWindow.<>f__am$cacheB == null)
		{
			UniverseMapScreenWindow.<>f__am$cacheB = new Func<LevelMap, bool>(null, (LevelMap t) => (t.fraction != NetworkScript.player.vessel.fractionId || t.__galaxyId <= 3000 ? false : t.__galaxyId < 4000));
		}
		LevelMap[] array = Enumerable.ToArray<LevelMap>(Enumerable.Where<LevelMap>(levelMapArray, UniverseMapScreenWindow.<>f__am$cacheB));
		this.InitOnDysplayMap();
		if (this.onDysplayMap == null || !Enumerable.Contains<LevelMap>(array, this.onDysplayMap))
		{
			this.onDysplayMap = array[0];
		}
		LevelMap[] levelMapArray1 = array;
		for (int i = 0; i < (int)levelMapArray1.Length; i++)
		{
			this.MakeGalaxyIcons(levelMapArray1[i]);
		}
		this.PutPlayerIndication();
	}

	private void OnUniverseMapClicked()
	{
		this.ClearContent();
		this.leftMenuBtn.Caption = StaticData.Translate("key_new_universe_map_lbl_faction_portals");
		this.leftMenuBtn.eventHandlerParam.customData = (byte)3;
		this.leftMenuBtn.Clicked = new Action<EventHandlerParam>(this, UniverseMapScreenWindow.OnScreenButtonClicked);
		this.rightMenuBtn.Caption = StaticData.Translate("key_new_universe_map_lbl_transformer_portals");
		this.rightMenuBtn.eventHandlerParam.customData = (byte)2;
		this.rightMenuBtn.Clicked = new Action<EventHandlerParam>(this, UniverseMapScreenWindow.OnScreenButtonClicked);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(147f, 8f, 618f, 36f),
			Alignment = 4,
			text = StaticData.Translate("key_new_universe_map_lbl_universe_map"),
			FontSize = 20,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("UniverseMapScreen", "universeMapFrame");
		guiTexture.X = 14f;
		guiTexture.Y = 87f;
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		LevelMap[] levelMapArray = StaticData.allGalaxies;
		if (UniverseMapScreenWindow.<>f__am$cacheA == null)
		{
			UniverseMapScreenWindow.<>f__am$cacheA = new Func<LevelMap, bool>(null, (LevelMap t) => (t.fraction == NetworkScript.player.vessel.fractionId || !t.isPveMap ? t.__galaxyId < 1000 : false));
		}
		LevelMap[] array = Enumerable.ToArray<LevelMap>(Enumerable.Where<LevelMap>(levelMapArray, UniverseMapScreenWindow.<>f__am$cacheA));
		this.InitOnDysplayMap();
		if (this.onDysplayMap == null || !Enumerable.Contains<LevelMap>(array, this.onDysplayMap))
		{
			this.onDysplayMap = array[0];
		}
		LevelMap[] levelMapArray1 = array;
		for (int i = 0; i < (int)levelMapArray1.Length; i++)
		{
			this.MakeGalaxyIcons(levelMapArray1[i]);
		}
		this.PutPlayerIndication();
	}

	public void PopulateExtractionPointInfo(List<ExtractionPointStateInfo> epsInfo)
	{
		if (!this.isInfoOnScren)
		{
			return;
		}
		if (this.epScroller != null)
		{
			this.epScroller.Claer();
			List<ExtractionPointStateInfo> list = epsInfo;
			if (UniverseMapScreenWindow.<>f__am$cache10 == null)
			{
				UniverseMapScreenWindow.<>f__am$cache10 = new Func<ExtractionPointStateInfo, int>(null, (ExtractionPointStateInfo t) => t.epId);
			}
			this.DrawEpInfo(Enumerable.ToArray<ExtractionPointStateInfo>(Enumerable.OrderBy<ExtractionPointStateInfo, int>(list, UniverseMapScreenWindow.<>f__am$cache10)));
		}
	}

	private void PopulateGalaxyInfo()
	{
		UniverseMapScreenWindow.<PopulateGalaxyInfo>c__AnonStorey7C variable = null;
		UniverseMapScreenWindow.<PopulateGalaxyInfo>c__AnonStorey7D variable1 = null;
		bool flag;
		this.ClearGalaxyInfo();
		if (this.onDysplayMap == null)
		{
			return;
		}
		bool flag1 = NetworkScript.player.playerBelongings.playerAccessLevel >= this.onDysplayMap.accessLevel;
		if (this.onDysplayMap.isPveMap)
		{
			flag = false;
		}
		else
		{
			flag = (NetworkScript.player.playerBelongings.playerLevel > this.onDysplayMap.reqMaxLevel ? false : NetworkScript.player.playerBelongings.playerLevel >= this.onDysplayMap.reqMinLevel);
		}
		bool flag2 = flag;
		bool flag3 = (this.onDysplayMap.get_galaxyId() <= 3000 || this.onDysplayMap.get_galaxyId() >= 4000 ? this.onDysplayMap.IsUltraGalaxyInstance() : true);
		bool flag4 = true;
		bool flag5 = (NetworkScript.player.vessel.galaxy.get_galaxyId() < 3000 || NetworkScript.player.vessel.galaxy.get_galaxyId() >= 4000 ? NetworkScript.player.vessel.galaxy.IsUltraGalaxyInstance() : true);
		Portal portal = null;
		if (flag3)
		{
			portal = Enumerable.First<Portal>(Enumerable.Where<Portal>(StaticData.allPortals, new Func<Portal, bool>(variable, (Portal t) => t.galaxyKey == this.<>f__this.onDysplayMap.galaxyKey)));
			if (NetworkScript.player.playerBelongings.playerItems.HaveAllPartsForPortal(portal.portalId) && NetworkScript.player.playerBelongings.unlockedPortals.Contains(portal.portalId))
			{
				flag4 = false;
			}
		}
		bool _galaxyId = this.onDysplayMap.get_galaxyId() > 4000;
		bool _galaxyId1 = NetworkScript.player.vessel.galaxy.get_galaxyId() > 4000;
		bool flag6 = true;
		if (_galaxyId)
		{
			flag6 = (NetworkScript.player.factionGalaxyOwnership.get_Item((byte)this.onDysplayMap.galaxyKey) != NetworkScript.player.vessel.fractionId ? true : NetworkScript.player.playerBelongings.playerLevel < 10);
		}
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(27f, 295f, 300f, 16f),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			Alignment = 3,
			text = (this.onDysplayMap.isPveMap ? string.Concat(StaticData.Translate(this.onDysplayMap.nameUI), " ", StaticData.Translate("key_new_universe_map_lbl_pve")) : string.Concat(StaticData.Translate(this.onDysplayMap.nameUI), " ", StaticData.Translate("key_new_universe_map_lbl_pvp"))),
			TextColor = (this.onDysplayMap.isPveMap ? GuiNewStyleBar.blueColor : GuiNewStyleBar.orangeColor)
		};
		base.AddGuiElement(guiLabel);
		this.galaxyInfoForDelete.Add(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(343f, 298f, 340f, 16f),
			FontSize = 12,
			Alignment = 3
		};
		if (!_galaxyId)
		{
			guiLabel1.text = string.Format(StaticData.Translate("key_new_universe_map_lbl_recommended_level"), this.onDysplayMap.reqMinLevel, this.onDysplayMap.reqMaxLevel);
		}
		else
		{
			guiLabel1.text = string.Format(StaticData.Translate("key_new_universe_map_lbl_recommended_level"), 10, 56);
		}
		guiLabel1.TextColor = GuiNewStyleBar.blueColor;
		base.AddGuiElement(guiLabel1);
		this.galaxyInfoForDelete.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(27f, 327f, 650f, 40f),
			FontSize = 12,
			Alignment = 0,
			text = StaticData.Translate(this.onDysplayMap.description),
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel2);
		this.galaxyInfoForDelete.Add(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(27f, 382f, 660f, 16f),
			FontSize = 14,
			Alignment = 3,
			text = StaticData.Translate("key_new_universe_map_lbl_requirements"),
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel3);
		this.galaxyInfoForDelete.Add(guiLabel3);
		if (_galaxyId)
		{
			GuiLabel guiLabel4 = new GuiLabel()
			{
				boundries = new Rect(27f, 405f, 660f, 16f),
				FontSize = 12,
				Alignment = 3,
				text = StaticData.Translate("key_new_universe_map_lbl_unlock_in_faction_war"),
				TextColor = (!flag6 ? GuiNewStyleBar.blueColor : GuiNewStyleBar.redColor)
			};
			base.AddGuiElement(guiLabel4);
			this.galaxyInfoForDelete.Add(guiLabel4);
		}
		else if (!flag3)
		{
			GuiLabel guiLabel5 = new GuiLabel()
			{
				boundries = new Rect(27f, 405f, 660f, 16f),
				FontSize = 12,
				Alignment = 3,
				text = string.Format(StaticData.Translate("key_new_universe_map_lbl_access_level"), this.onDysplayMap.accessLevel),
				TextColor = (flag1 ? GuiNewStyleBar.blueColor : GuiNewStyleBar.redColor)
			};
			base.AddGuiElement(guiLabel5);
			this.galaxyInfoForDelete.Add(guiLabel5);
		}
		else
		{
			GuiLabel guiLabel6 = new GuiLabel()
			{
				boundries = new Rect(27f, 405f, 660f, 16f),
				FontSize = 12,
				Alignment = 3,
				text = StaticData.Translate("key_new_universe_map_lbl_unlock_in_transformer"),
				TextColor = (!flag4 ? GuiNewStyleBar.blueColor : GuiNewStyleBar.redColor)
			};
			base.AddGuiElement(guiLabel6);
			this.galaxyInfoForDelete.Add(guiLabel6);
		}
		if (!this.onDysplayMap.isPveMap)
		{
			GuiLabel guiLabel7 = new GuiLabel()
			{
				boundries = new Rect(27f, 423f, 660f, 16f),
				FontSize = 12,
				Alignment = 3,
				text = string.Format(StaticData.Translate("key_new_universe_map_lbl_player_level"), this.onDysplayMap.reqMinLevel, this.onDysplayMap.reqMaxLevel),
				TextColor = (flag2 ? GuiNewStyleBar.blueColor : GuiNewStyleBar.redColor)
			};
			base.AddGuiElement(guiLabel7);
			this.galaxyInfoForDelete.Add(guiLabel7);
		}
		GuiLabel guiLabel8 = new GuiLabel()
		{
			boundries = new Rect(27f, 452f, 660f, 16f),
			FontSize = 14,
			Alignment = 3,
			text = StaticData.Translate("key_new_universe_map_lbl_instances"),
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(guiLabel8);
		this.galaxyInfoForDelete.Add(guiLabel8);
		HyperJumpNet[] hyperJumpNetArray = this.onDysplayMap.hyperJumps;
		if (UniverseMapScreenWindow.<>f__am$cacheE == null)
		{
			UniverseMapScreenWindow.<>f__am$cacheE = new Func<HyperJumpNet, bool>(null, (HyperJumpNet t) => t.galaxyDst > 1000);
		}
		HyperJumpNet[] array = Enumerable.ToArray<HyperJumpNet>(Enumerable.Where<HyperJumpNet>(hyperJumpNetArray, UniverseMapScreenWindow.<>f__am$cacheE));
		for (int i = 0; i < (int)array.Length; i++)
		{
			GuiTexture guiTexture = new GuiTexture();
			guiTexture.SetTexture("MinimapWindow", "portal_instance");
			guiTexture.boundries = new Rect((float)(23 + 155 * i), 471f, 25f, 25f);
			base.AddGuiElement(guiTexture);
			this.galaxyInfoForDelete.Add(guiTexture);
			LevelMap levelMap = Enumerable.First<LevelMap>(Enumerable.Where<LevelMap>(StaticData.allGalaxies, new Func<LevelMap, bool>(variable1, (LevelMap t) => t.get_galaxyId() == this.<>f__ref$124.jumps[this.i].galaxyDst)));
			GuiLabel guiLabel9 = new GuiLabel()
			{
				boundries = new Rect((float)(50 + 155 * i), 475f, 125f, 16f),
				text = StaticData.Translate(levelMap.nameUI),
				Alignment = 3,
				FontSize = 12,
				TextColor = GuiNewStyleBar.blueColor
			};
			base.AddGuiElement(guiLabel9);
			this.galaxyInfoForDelete.Add(guiLabel9);
			GuiLabel guiLabel10 = new GuiLabel()
			{
				boundries = new Rect((float)(27 + 155 * i), 493f, 148f, 16f),
				text = string.Format(StaticData.Translate("key_new_universe_map_lbl_instances_level"), levelMap.reqMinLevel, levelMap.reqMaxLevel),
				Alignment = 3,
				FontSize = 12,
				TextColor = GuiNewStyleBar.blueColor
			};
			base.AddGuiElement(guiLabel10);
			this.galaxyInfoForDelete.Add(guiLabel10);
			GuiTexture guiTexture1 = new GuiTexture();
			guiTexture1.SetTexture("UniverseMapScreen", "instanceSeparator");
			guiTexture1.X = (float)(173 + 155 * i);
			guiTexture1.Y = 472f;
			guiTexture1.boundries.set_width(1f);
			base.AddGuiElement(guiTexture1);
			this.galaxyInfoForDelete.Add(guiTexture1);
		}
		if ((int)array.Length == 0)
		{
			GuiLabel guiLabel11 = new GuiLabel()
			{
				boundries = new Rect(27f, 476f, 170f, 34f),
				text = StaticData.Translate("key_new_universe_map_lbl_none"),
				Alignment = 3,
				Italic = true,
				FontSize = 12,
				TextColor = GuiNewStyleBar.blueColor
			};
			base.AddGuiElement(guiLabel11);
			this.galaxyInfoForDelete.Add(guiLabel11);
		}
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("MiniMaps", this.onDysplayMap.minimapAssetName);
		rect.boundries = new Rect(692f, 377f, 203f, 136f);
		base.AddGuiElement(rect);
		this.galaxyInfoForDelete.Add(rect);
		GuiTexture rect1 = new GuiTexture();
		rect1.SetTexture("UniverseMapScreen", "mapOverlay");
		rect1.boundries = new Rect(692f, 377f, 203f, 136f);
		base.AddGuiElement(rect1);
		this.galaxyInfoForDelete.Add(rect1);
		if (_galaxyId)
		{
			GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
			guiButtonResizeable.SetTexture("PoiScreenWindow", "tab_");
			guiButtonResizeable.X = 690f;
			guiButtonResizeable.Y = 295f;
			guiButtonResizeable.boundries.set_width(200f);
			guiButtonResizeable.Caption = StaticData.Translate("key_new_universe_map_btn_jump_for_free");
			guiButtonResizeable.SetRegularFont();
			guiButtonResizeable.FontSize = 14;
			guiButtonResizeable.MarginTop = -2;
			guiButtonResizeable._marginLeft = 0;
			guiButtonResizeable.SetColor(GuiNewStyleBar.blueColor);
			guiButtonResizeable.Alignment = 4;
			guiButtonResizeable.eventHandlerParam.customData = (byte)(this.onDysplayMap.galaxyKey - 40);
			guiButtonResizeable.Clicked = null;
			base.AddGuiElement(guiButtonResizeable);
			this.galaxyInfoForDelete.Add(guiButtonResizeable);
			GuiButtonResizeable action = new GuiButtonResizeable();
			action.SetTexture("PoiScreenWindow", "tab_");
			action.X = 690f;
			action.Y = 335f;
			action.boundries.set_width(200f);
			action.Caption = StaticData.Translate("key_new_universe_map_btn_faction_war");
			action.SetRegularFont();
			action.FontSize = 14;
			action.MarginTop = -2;
			action._marginLeft = 0;
			action.SetColor(GuiNewStyleBar.blueColor);
			action.Alignment = 4;
			action.Clicked = new Action<EventHandlerParam>(this, UniverseMapScreenWindow.OnFactionWarBtnClicked);
			action.isEnabled = NetworkScript.player.playerBelongings.playerLevel >= 10;
			base.AddGuiElement(action);
			this.galaxyInfoForDelete.Add(action);
			if (flag6 || flag5)
			{
				guiButtonResizeable.isEnabled = false;
			}
			else if (NetworkScript.player.vessel.galaxy.galaxyKey != this.onDysplayMap.galaxyKey)
			{
				guiButtonResizeable.Caption = StaticData.Translate("key_new_universe_map_btn_jump_for_free");
				guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, UniverseMapScreenWindow.OnFactionGalaxyJumpClicked);
			}
			else
			{
				guiButtonResizeable.Caption = StaticData.Translate("key_new_universe_map_btn_jump_back");
				guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, UniverseMapScreenWindow.OnFactionGalaxyJumpBackClicked);
			}
			return;
		}
		if (flag3)
		{
			GuiButtonResizeable guiButtonResizeable1 = new GuiButtonResizeable();
			guiButtonResizeable1.SetTexture("PoiScreenWindow", "tab_");
			guiButtonResizeable1.X = 690f;
			guiButtonResizeable1.Y = 295f;
			guiButtonResizeable1.boundries.set_width(200f);
			guiButtonResizeable1.Caption = StaticData.Translate("key_new_universe_map_btn_jump_for_free");
			guiButtonResizeable1.SetRegularFont();
			guiButtonResizeable1.FontSize = 14;
			guiButtonResizeable1.MarginTop = -2;
			guiButtonResizeable1._marginLeft = 0;
			guiButtonResizeable1.SetColor(GuiNewStyleBar.blueColor);
			guiButtonResizeable1.Alignment = 4;
			guiButtonResizeable1.Clicked = null;
			base.AddGuiElement(guiButtonResizeable1);
			this.galaxyInfoForDelete.Add(guiButtonResizeable1);
			GuiButtonResizeable action1 = new GuiButtonResizeable();
			action1.SetTexture("PoiScreenWindow", "tab_");
			action1.X = 690f;
			action1.Y = 335f;
			action1.boundries.set_width(200f);
			action1.Caption = StaticData.Translate("key_new_universe_map_btn_transformer");
			action1.SetRegularFont();
			action1.FontSize = 14;
			action1.MarginTop = -2;
			action1._marginLeft = 0;
			action1.SetColor(GuiNewStyleBar.blueColor);
			action1.Alignment = 4;
			guiButtonResizeable1.eventHandlerParam.customData = (byte)portal.portalId;
			action1.Clicked = new Action<EventHandlerParam>(this, UniverseMapScreenWindow.OnTransformerBtnClicked);
			base.AddGuiElement(action1);
			this.galaxyInfoForDelete.Add(action1);
			if (flag4 || _galaxyId1)
			{
				guiButtonResizeable1.isEnabled = false;
			}
			else if (NetworkScript.player.vessel.galaxy.galaxyKey != portal.galaxyKey)
			{
				guiButtonResizeable1.Caption = StaticData.Translate("key_new_universe_map_btn_jump_for_free");
				guiButtonResizeable1.Clicked = new Action<EventHandlerParam>(this, UniverseMapScreenWindow.OnTransformerJumpClicked);
			}
			else
			{
				guiButtonResizeable1.Caption = StaticData.Translate("key_new_universe_map_btn_jump_back");
				guiButtonResizeable1.Clicked = new Action<EventHandlerParam>(this, UniverseMapScreenWindow.OnJumpBackClicked);
			}
			return;
		}
		GuiButtonResizeable empty = new GuiButtonResizeable();
		empty.SetNovaBtn();
		empty.X = 690f;
		empty.Y = 295f;
		empty.boundries.set_width(200f);
		empty.Caption = string.Empty;
		empty.FontSize = 14;
		empty._marginLeft = 40;
		empty.Alignment = 3;
		empty.Clicked = new Action<EventHandlerParam>(this, UniverseMapScreenWindow.OnNovaJumpBtnClick);
		base.AddGuiElement(empty);
		this.galaxyInfoForDelete.Add(empty);
		GuiLabel _gray = new GuiLabel()
		{
			boundries = empty.boundries
		};
		_gray.boundries.set_width(empty.boundries.get_width() - 10f);
		_gray.text = StaticData.Translate("key_new_universe_map_btn_jump");
		_gray.Alignment = 5;
		_gray.FontSize = 12;
		_gray.TextColor = GuiNewStyleBar.novaBtnColor;
		base.AddGuiElement(_gray);
		this.galaxyInfoForDelete.Add(_gray);
		GuiButtonResizeable empty1 = new GuiButtonResizeable();
		empty1.SetEqBtn();
		empty1.X = 690f;
		empty1.Y = 335f;
		empty1.boundries.set_width(200f);
		empty1.Caption = string.Empty;
		empty1.FontSize = 14;
		empty1._marginLeft = 40;
		empty1.Alignment = 3;
		empty1.Clicked = new Action<EventHandlerParam>(this, UniverseMapScreenWindow.OnEqJumpBtnClick);
		base.AddGuiElement(empty1);
		this.galaxyInfoForDelete.Add(empty1);
		GuiLabel _gray1 = new GuiLabel()
		{
			boundries = empty1.boundries
		};
		_gray1.boundries.set_width(empty1.boundries.get_width() - 10f);
		_gray1.text = StaticData.Translate("key_new_universe_map_btn_jump");
		_gray1.Alignment = 5;
		_gray1.FontSize = 12;
		_gray1.TextColor = GuiNewStyleBar.eqBtnColor;
		base.AddGuiElement(_gray1);
		this.galaxyInfoForDelete.Add(_gray1);
		NetworkScript.player.vessel.galaxy.get_galaxyId();
		if (flag3 && flag4)
		{
			empty1.Caption = string.Empty;
			empty.Caption = string.Empty;
			empty1.isEnabled = false;
			empty.isEnabled = false;
			_gray.TextColor = Color.get_gray();
			_gray1.TextColor = Color.get_gray();
			return;
		}
		if (!flag1 || !this.onDysplayMap.isPveMap && (NetworkScript.player.playerBelongings.playerLevel > this.onDysplayMap.reqMaxLevel || NetworkScript.player.playerBelongings.playerLevel < this.onDysplayMap.reqMinLevel))
		{
			empty1.Caption = string.Empty;
			empty.Caption = string.Empty;
			empty1.isEnabled = false;
			empty.isEnabled = false;
			_gray.TextColor = Color.get_gray();
			_gray1.TextColor = Color.get_gray();
			return;
		}
		GalaxiesJumpMap galaxiesJumpMap = Enumerable.FirstOrDefault<GalaxiesJumpMap>(Enumerable.Where<GalaxiesJumpMap>(StaticData.galaxyJumpsPrice, new Func<GalaxiesJumpMap, bool>(variable, (GalaxiesJumpMap t) => (t.destinationGalaxyId != this.<>f__this.onDysplayMap.get_galaxyId() ? false : t.sourceGalaxyId == this.currentGalaxyId))));
		int num = 9999;
		int num1 = 9999;
		if (galaxiesJumpMap != null)
		{
			num = galaxiesJumpMap.equilibriumPrice;
			num1 = galaxiesJumpMap.novaPrice;
		}
		empty1.Caption = (flag5 || _galaxyId1 ? string.Empty : num.ToString("N0"));
		empty.Caption = (flag5 || _galaxyId1 ? string.Empty : num1.ToString("N0"));
		empty1.eventHandlerParam.customData = null;
		empty.eventHandlerParam.customData = null;
		empty1.isEnabled = (flag5 || _galaxyId1 ? false : NetworkScript.player.playerBelongings.playerItems.GetAmountAt(PlayerItems.TypeEquilibrium) >= (long)num);
		empty.isEnabled = (flag5 || _galaxyId1 ? false : NetworkScript.player.playerBelongings.playerItems.get_Nova() >= (long)num1);
		if (!empty.isEnabled)
		{
			_gray.TextColor = Color.get_gray();
		}
		if (!empty1.isEnabled)
		{
			_gray1.TextColor = Color.get_gray();
		}
	}

	private void PutPlayerIndication()
	{
		UniverseMapScreenWindow.<PutPlayerIndication>c__AnonStorey7B variable = null;
		string empty = string.Empty;
		if (NetworkScript.player.vessel.galaxy.get_galaxyId() <= 1000 || NetworkScript.player.vessel.galaxy.get_galaxyId() >= 2000)
		{
			empty = NetworkScript.player.vessel.galaxy.minimapAssetName;
			if (this.onDysplayMap == null)
			{
				LevelMap[] levelMapArray = StaticData.allGalaxies;
				if (UniverseMapScreenWindow.<>f__am$cacheD == null)
				{
					UniverseMapScreenWindow.<>f__am$cacheD = new Func<LevelMap, bool>(null, (LevelMap t) => t.get_galaxyId() == NetworkScript.player.vessel.galaxy.get_galaxyId());
				}
				this.onDysplayMap = Enumerable.First<LevelMap>(Enumerable.Where<LevelMap>(levelMapArray, UniverseMapScreenWindow.<>f__am$cacheD));
			}
		}
		else
		{
			Enumerable.First<HyperJumpNet>(NetworkScript.player.vessel.galaxy.hyperJumps);
			LevelMap levelMap = Enumerable.First<LevelMap>(Enumerable.Where<LevelMap>(StaticData.allGalaxies, new Func<LevelMap, bool>(variable, (LevelMap g) => g.get_galaxyId() == this.hj.galaxyDst)));
			empty = levelMap.minimapAssetName;
			if (this.onDysplayMap == null)
			{
				this.onDysplayMap = levelMap;
			}
		}
		if (this.onDysplayMap.minimapAssetName != empty)
		{
			return;
		}
		if (UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.ContainsKey(empty))
		{
			GuiTexture guiTexture = new GuiTexture();
			guiTexture.SetTexture("UniverseMapScreen", "location");
			guiTexture.X = UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.get_Item(empty).indicationX;
			guiTexture.Y = UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.get_Item(empty).indicationY;
			base.AddGuiElement(guiTexture);
			this.forDelete.Add(guiTexture);
		}
	}

	private void RefreshExtractionPointInfoScreen()
	{
		this.OnExtractionPointInfoClick(null);
	}

	private class GalaxyPossitions
	{
		public string assetName;

		public float indicationX;

		public float indicationY;

		public float titleX;

		public float titleY;

		public float titleW;

		public float titleH;

		public static SortedList<string, UniverseMapScreenWindow.GalaxyPossitions> listOfGalaxy;

		static GalaxyPossitions()
		{
			UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy = new SortedList<string, UniverseMapScreenWindow.GalaxyPossitions>();
			UniverseMapScreenWindow.GalaxyPossitions galaxyPossition = new UniverseMapScreenWindow.GalaxyPossitions("Hydra", 13f, 79f, 13f, 59f, 90f, 26f);
			UniverseMapScreenWindow.GalaxyPossitions galaxyPossition1 = new UniverseMapScreenWindow.GalaxyPossitions("Mensa", 66f, 173f, 66f, 256f, 90f, 26f);
			UniverseMapScreenWindow.GalaxyPossitions galaxyPossition2 = new UniverseMapScreenWindow.GalaxyPossitions("CanisMinor", 118f, 79f, 118f, 59f, 90f, 26f);
			UniverseMapScreenWindow.GalaxyPossitions galaxyPossition3 = new UniverseMapScreenWindow.GalaxyPossitions("CanisMajor", 225f, 79f, 225f, 59f, 90f, 26f);
			UniverseMapScreenWindow.GalaxyPossitions galaxyPossition4 = new UniverseMapScreenWindow.GalaxyPossitions("Orion", 278f, 173f, 278f, 256f, 90f, 26f);
			UniverseMapScreenWindow.GalaxyPossitions galaxyPossition5 = new UniverseMapScreenWindow.GalaxyPossitions("Centaurus", 331f, 79f, 331f, 59f, 90f, 26f);
			UniverseMapScreenWindow.GalaxyPossitions galaxyPossition6 = new UniverseMapScreenWindow.GalaxyPossitions("Cepheus", 384f, 173f, 384f, 256f, 90f, 26f);
			UniverseMapScreenWindow.GalaxyPossitions galaxyPossition7 = new UniverseMapScreenWindow.GalaxyPossitions("Lynx", 490f, 173f, 490f, 256f, 90f, 26f);
			UniverseMapScreenWindow.GalaxyPossitions galaxyPossition8 = new UniverseMapScreenWindow.GalaxyPossitions("UrsaMinor", 543f, 79f, 543f, 59f, 90f, 26f);
			UniverseMapScreenWindow.GalaxyPossitions galaxyPossition9 = new UniverseMapScreenWindow.GalaxyPossitions("UrsaMajor", 649f, 79f, 649f, 59f, 90f, 26f);
			UniverseMapScreenWindow.GalaxyPossitions galaxyPossition10 = new UniverseMapScreenWindow.GalaxyPossitions("Taurus", 754f, 79f, 754f, 59f, 90f, 26f);
			UniverseMapScreenWindow.GalaxyPossitions galaxyPossition11 = new UniverseMapScreenWindow.GalaxyPossitions("Scorpio", 702f, 173f, 702f, 256f, 90f, 26f);
			UniverseMapScreenWindow.GalaxyPossitions galaxyPossition12 = new UniverseMapScreenWindow.GalaxyPossitions("Andromeda", 808f, 173f, 808f, 256f, 90f, 26f);
			UniverseMapScreenWindow.GalaxyPossitions galaxyPossition13 = new UniverseMapScreenWindow.GalaxyPossitions("Cassiopeia", 172f, 173f, 172f, 256f, 90f, 26f);
			UniverseMapScreenWindow.GalaxyPossitions galaxyPossition14 = new UniverseMapScreenWindow.GalaxyPossitions("Pegasus", 436f, 79f, 436f, 59f, 90f, 26f);
			UniverseMapScreenWindow.GalaxyPossitions galaxyPossition15 = new UniverseMapScreenWindow.GalaxyPossitions("Perseus", 596f, 173f, 596f, 256f, 90f, 26f);
			UniverseMapScreenWindow.GalaxyPossitions galaxyPossition16 = new UniverseMapScreenWindow.GalaxyPossitions("UltralibriumGalaxyOne", 200f, 127f, 156f, 99f, 175f, 21f);
			UniverseMapScreenWindow.GalaxyPossitions galaxyPossition17 = new UniverseMapScreenWindow.GalaxyPossitions("UltralibriumGalaxyTwo", 412f, 127f, 370f, 99f, 175f, 21f);
			UniverseMapScreenWindow.GalaxyPossitions galaxyPossition18 = new UniverseMapScreenWindow.GalaxyPossitions("UltralibriumGalaxyThree", 624f, 127f, 582f, 99f, 175f, 21f);
			UniverseMapScreenWindow.GalaxyPossitions galaxyPossition19 = new UniverseMapScreenWindow.GalaxyPossitions("FactionGalaxy1", 63f, 127f, 36f, 97f, 140f, 25f);
			UniverseMapScreenWindow.GalaxyPossitions galaxyPossition20 = new UniverseMapScreenWindow.GalaxyPossitions("FactionGalaxy2", 239f, 127f, 212f, 97f, 140f, 25f);
			UniverseMapScreenWindow.GalaxyPossitions galaxyPossition21 = new UniverseMapScreenWindow.GalaxyPossitions("FactionGalaxy3", 414f, 127f, 387f, 97f, 140f, 25f);
			UniverseMapScreenWindow.GalaxyPossitions galaxyPossition22 = new UniverseMapScreenWindow.GalaxyPossitions("FactionGalaxy4", 590f, 127f, 563f, 97f, 140f, 25f);
			UniverseMapScreenWindow.GalaxyPossitions galaxyPossition23 = new UniverseMapScreenWindow.GalaxyPossitions("FactionGalaxy5", 765f, 127f, 738f, 97f, 140f, 25f);
			UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.Add(galaxyPossition.assetName, galaxyPossition);
			UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.Add(galaxyPossition1.assetName, galaxyPossition1);
			UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.Add(galaxyPossition2.assetName, galaxyPossition2);
			UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.Add(galaxyPossition3.assetName, galaxyPossition3);
			UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.Add(galaxyPossition4.assetName, galaxyPossition4);
			UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.Add(galaxyPossition5.assetName, galaxyPossition5);
			UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.Add(galaxyPossition6.assetName, galaxyPossition6);
			UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.Add(galaxyPossition7.assetName, galaxyPossition7);
			UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.Add(galaxyPossition8.assetName, galaxyPossition8);
			UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.Add(galaxyPossition9.assetName, galaxyPossition9);
			UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.Add(galaxyPossition10.assetName, galaxyPossition10);
			UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.Add(galaxyPossition11.assetName, galaxyPossition11);
			UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.Add(galaxyPossition12.assetName, galaxyPossition12);
			UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.Add(galaxyPossition13.assetName, galaxyPossition13);
			UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.Add(galaxyPossition14.assetName, galaxyPossition14);
			UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.Add(galaxyPossition15.assetName, galaxyPossition15);
			UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.Add(galaxyPossition16.assetName, galaxyPossition16);
			UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.Add(galaxyPossition17.assetName, galaxyPossition17);
			UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.Add(galaxyPossition18.assetName, galaxyPossition18);
			UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.Add(galaxyPossition19.assetName, galaxyPossition19);
			UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.Add(galaxyPossition20.assetName, galaxyPossition20);
			UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.Add(galaxyPossition21.assetName, galaxyPossition21);
			UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.Add(galaxyPossition22.assetName, galaxyPossition22);
			UniverseMapScreenWindow.GalaxyPossitions.listOfGalaxy.Add(galaxyPossition23.assetName, galaxyPossition23);
		}

		public GalaxyPossitions(string name, float x, float y, float tx, float ty, float w = 90, float h = 26)
		{
			this.assetName = name;
			this.indicationX = x;
			this.indicationY = y;
			this.titleX = tx;
			this.titleY = ty;
			this.titleW = w;
			this.titleH = h;
		}
	}
}