using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TransferableObjects;
using UnityEngine;

public class __UniverseMap : GuiWindow
{
	private GuiButtonResizeable btnJumpNova;

	private GuiButtonResizeable btnJumpEq;

	private GuiTexture mainScreenBG;

	private GuiTexture mainScreenFrame;

	private GuiTexture mapAvatar_TX;

	private GuiTexture novaIcon;

	private GuiTexture eqIcon;

	private GuiTexture shipIndication;

	private GuiLabel lbl_InfoBoxTitle;

	private GuiLabel lbl_InfoBoxValue;

	private GuiLabel lbl_title;

	private GuiLabel lbl_MapName;

	private GuiLabel lbl_RecomendedLevel;

	private GuiLabel lbl_SpaceStation;

	private GuiLabel lblNovaPrice;

	private GuiLabel lblEqPrice;

	private GuiLabel lbl_label;

	private GuiLabel lbl_AccessLevel;

	private LevelMap onDysplayMap;

	private ExtractionPoint selectedPoint;

	private List<GuiElement> forDelete = new List<GuiElement>();

	private List<GuiElement> forExtractionPointHover = new List<GuiElement>();

	private List<GuiElement> forInstancesInfo = new List<GuiElement>();

	public static byte subSection;

	private static short requestedGalaxyId;

	private int fractionOneUltralibrium;

	private int fractionTwoUltralibrium;

	static __UniverseMap()
	{
	}

	public __UniverseMap()
	{
	}

	private void CalculateExtractionPointJumpPrice(EventHandlerParam prm)
	{
		__UniverseMap.<CalculateExtractionPointJumpPrice>c__AnonStoreyAC variable = null;
		bool flag = (NetworkScript.player.vessel.galaxy.get_galaxyId() >= 3000 ? true : NetworkScript.player.vessel.galaxy.IsUltraGalaxyInstance());
		NetworkScript.player.vessel.galaxy.get_galaxyId();
		LevelMap[] levelMapArray = StaticData.allGalaxies;
		if (__UniverseMap.<>f__am$cache23 == null)
		{
			__UniverseMap.<>f__am$cache23 = new Func<LevelMap, bool>(null, (LevelMap t) => t.get_galaxyId() == __UniverseMap.requestedGalaxyId);
		}
		LevelMap levelMap = Enumerable.First<LevelMap>(Enumerable.Where<LevelMap>(levelMapArray, __UniverseMap.<>f__am$cache23));
		this.onDysplayMap = levelMap;
		ExtractionPoint extractionPoint = (ExtractionPoint)prm.customData;
		ExtractionPointInfo extractionPointInfo = Enumerable.First<ExtractionPointInfo>(Enumerable.Where<ExtractionPointInfo>(StaticData.allExtractionPoints, new Func<ExtractionPointInfo, bool>(variable, (ExtractionPointInfo t) => t.id == this.point.pointId)));
		extractionPoint.x = (float)extractionPointInfo.possitionX;
		extractionPoint.z = (float)extractionPointInfo.possitionZ;
		extractionPoint.name = extractionPointInfo.name;
		extractionPoint.assetName = extractionPointInfo.assetName;
		this.selectedPoint = extractionPoint;
		this.ClearExtractionInfo();
		this.DrawExtractionPointHoverInfo(extractionPoint);
		if (NetworkScript.player.playerBelongings.playerAccessLevel < levelMap.accessLevel || !levelMap.isPveMap && (NetworkScript.player.playerBelongings.playerLevel > levelMap.reqMaxLevel || NetworkScript.player.playerBelongings.playerLevel < levelMap.reqMinLevel))
		{
			this.btnJumpEq.Caption = StaticData.Translate("key_universe_map_btn_jump");
			this.btnJumpNova.Caption = StaticData.Translate("key_universe_map_btn_jump");
			this.btnJumpEq.isEnabled = false;
			this.btnJumpNova.isEnabled = false;
			return;
		}
		GalaxiesJumpMap galaxiesJumpMap = Enumerable.FirstOrDefault<GalaxiesJumpMap>(Enumerable.Where<GalaxiesJumpMap>(StaticData.galaxyJumpsPrice, new Func<GalaxiesJumpMap, bool>(variable, (GalaxiesJumpMap t) => (t.destinationGalaxyId != this.selectedMap.get_galaxyId() ? false : t.sourceGalaxyId == this.currentGalaxyId))));
		int num = 9999;
		int num1 = 9999;
		if (galaxiesJumpMap != null)
		{
			num = galaxiesJumpMap.equilibriumPrice;
			num1 = galaxiesJumpMap.novaPrice;
		}
		this.btnJumpEq.Caption = (!flag ? string.Format(StaticData.Translate("key_universe_map_btn_jump_viral"), num).ToUpper() : StaticData.Translate("key_universe_map_btn_jump"));
		this.btnJumpNova.Caption = (!flag ? string.Format(StaticData.Translate("key_universe_map_btn_jump_nova"), num1).ToUpper() : StaticData.Translate("key_universe_map_btn_jump"));
		this.btnJumpEq.eventHandlerParam.customData = extractionPoint;
		this.btnJumpNova.eventHandlerParam.customData = extractionPoint;
		this.btnJumpEq.isEnabled = (flag ? false : NetworkScript.player.playerBelongings.playerItems.GetAmountAt(PlayerItems.TypeEquilibrium) >= (long)num);
		this.btnJumpNova.isEnabled = (flag ? false : NetworkScript.player.playerBelongings.playerItems.get_Nova() >= (long)num1);
	}

	private void CalculateJumpPrice(EventHandlerParam prm)
	{
		__UniverseMap.<CalculateJumpPrice>c__AnonStoreyA7 variable = null;
		bool flag = (NetworkScript.player.vessel.galaxy.get_galaxyId() >= 3000 ? true : NetworkScript.player.vessel.galaxy.IsUltraGalaxyInstance());
		NetworkScript.player.vessel.galaxy.get_galaxyId();
		LevelMap levelMap = Enumerable.First<LevelMap>(Enumerable.Where<LevelMap>(StaticData.allGalaxies, new Func<LevelMap, bool>(variable, (LevelMap t) => t.get_galaxyId() == (short)this.prm.customData)));
		this.onDysplayMap = levelMap;
		if (NetworkScript.player.playerBelongings.playerAccessLevel < this.onDysplayMap.accessLevel || !this.onDysplayMap.isPveMap && (NetworkScript.player.playerBelongings.playerLevel > this.onDysplayMap.reqMaxLevel || NetworkScript.player.playerBelongings.playerLevel < this.onDysplayMap.reqMinLevel))
		{
			this.btnJumpEq.Caption = StaticData.Translate("key_universe_map_btn_jump");
			this.btnJumpNova.Caption = StaticData.Translate("key_universe_map_btn_jump");
			this.btnJumpEq.isEnabled = false;
			this.btnJumpNova.isEnabled = false;
			return;
		}
		GalaxiesJumpMap galaxiesJumpMap = Enumerable.FirstOrDefault<GalaxiesJumpMap>(Enumerable.Where<GalaxiesJumpMap>(StaticData.galaxyJumpsPrice, new Func<GalaxiesJumpMap, bool>(variable, (GalaxiesJumpMap t) => (t.destinationGalaxyId != this.selectedMap.get_galaxyId() ? false : t.sourceGalaxyId == this.currentGalaxyId))));
		int num = 9999;
		int num1 = 9999;
		if (galaxiesJumpMap != null)
		{
			num = galaxiesJumpMap.equilibriumPrice;
			num1 = galaxiesJumpMap.novaPrice;
		}
		this.btnJumpEq.Caption = (!flag ? string.Format(StaticData.Translate("key_universe_map_btn_jump_viral"), num).ToUpper() : StaticData.Translate("key_universe_map_btn_jump"));
		this.btnJumpNova.Caption = (!flag ? string.Format(StaticData.Translate("key_universe_map_btn_jump_nova"), num1).ToUpper() : StaticData.Translate("key_universe_map_btn_jump"));
		this.btnJumpEq.eventHandlerParam.customData = null;
		this.btnJumpNova.eventHandlerParam.customData = null;
		this.btnJumpEq.isEnabled = (flag ? false : NetworkScript.player.playerBelongings.playerItems.GetAmountAt(PlayerItems.TypeEquilibrium) >= (long)num);
		this.btnJumpNova.isEnabled = (flag ? false : NetworkScript.player.playerBelongings.playerItems.get_Nova() >= (long)num1);
	}

	private new void Clear()
	{
		this.ClearExtractionInfo();
		foreach (GuiElement guiElement in this.forDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.forDelete.Clear();
	}

	private void ClearExtractionInfo()
	{
		foreach (GuiElement guiElement in this.forExtractionPointHover)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.forExtractionPointHover.Clear();
	}

	private void ClearInstancesInfo()
	{
		foreach (GuiElement guiElement in this.forInstancesInfo)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.forInstancesInfo.Clear();
	}

	public override void Create()
	{
		playWebGame.udp.ExecuteCommand(187, null);
		this.onDysplayMap = NetworkScript.player.vessel.galaxy;
		base.SetBackgroundTexture("FusionWindow", "FusionBackGround");
		base.PutToCenter();
		this.zOrder = 210;
		this.mainScreenFrame = new GuiTexture();
		this.mainScreenFrame.SetTexture("NewGUI", "universeMapFrame");
		this.mainScreenFrame.X = 209f;
		this.mainScreenFrame.Y = 71f;
		base.AddGuiElement(this.mainScreenFrame);
		this.mainScreenBG = new GuiTexture();
		this.mainScreenBG.SetTexture("NewGUI", "universeMapTab");
		this.mainScreenBG.X = 214f;
		this.mainScreenBG.Y = 76f;
		base.AddGuiElement(this.mainScreenBG);
		this.lbl_label = new GuiLabel()
		{
			text = StaticData.Translate("key_universe_map_title"),
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 18,
			Font = GuiLabel.FontBold,
			boundries = new Rect(280f, 42f, 540f, 35f),
			Alignment = 1
		};
		base.AddGuiElement(this.lbl_label);
		this.btnJumpNova = new GuiButtonResizeable();
		this.btnJumpNova.boundries.set_x(265f);
		this.btnJumpNova.boundries.set_y(485f);
		this.btnJumpNova.boundries.set_width(200f);
		this.btnJumpNova.Caption = StaticData.Translate("key_universe_map_btn_jump");
		this.btnJumpNova.FontSize = 12;
		this.btnJumpNova.Alignment = 3;
		this.btnJumpNova._marginLeft = 30;
		this.btnJumpNova.textColorDisabled = Color.get_grey();
		this.btnJumpNova.SetOrangeTexture();
		this.btnJumpNova.Clicked = new Action<EventHandlerParam>(this, __UniverseMap.OnNovaJumpBtnClick);
		this.btnJumpNova.isEnabled = false;
		base.AddGuiElement(this.btnJumpNova);
		this.novaIcon = new GuiTexture()
		{
			boundries = new Rect(this.btnJumpNova.boundries.get_x() + 10f, this.btnJumpNova.boundries.get_y() + 11f, 20f, 20f)
		};
		this.novaIcon.SetTextureKeepSize("NewGUI", "icon_white_nova");
		base.AddGuiElement(this.novaIcon);
		this.btnJumpEq = new GuiButtonResizeable();
		this.btnJumpEq.boundries.set_x(640f);
		this.btnJumpEq.boundries.set_y(485f);
		this.btnJumpEq.boundries.set_width(200f);
		this.btnJumpEq.Caption = StaticData.Translate("key_universe_map_btn_jump");
		this.btnJumpEq.FontSize = 12;
		this.btnJumpEq.Alignment = 3;
		this.btnJumpEq._marginLeft = 30;
		this.btnJumpEq.textColorDisabled = Color.get_grey();
		this.btnJumpEq.SetBlueTexture();
		this.btnJumpEq.Clicked = new Action<EventHandlerParam>(this, __UniverseMap.OnEqJumpBtnClick);
		this.btnJumpEq.isEnabled = false;
		base.AddGuiElement(this.btnJumpEq);
		this.eqIcon = new GuiTexture()
		{
			boundries = new Rect(this.btnJumpEq.boundries.get_x() + 10f, this.btnJumpEq.boundries.get_y() + 11f, 20f, 20f)
		};
		this.eqIcon.SetTextureKeepSize("NewGUI", "icon_white_equilibrium");
		base.AddGuiElement(this.eqIcon);
		this.lbl_InfoBoxTitle = new GuiLabel()
		{
			boundries = new Rect(27f, 295f, 88f, 24f),
			text = StaticData.Translate("key_universe_map_infobox_default"),
			Alignment = 4,
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor
		};
		base.AddGuiElement(this.lbl_InfoBoxTitle);
		this.lbl_InfoBoxValue = new GuiLabel()
		{
			boundries = new Rect(35f, 340f, 160f, 180f),
			text = StaticData.Translate(this.onDysplayMap.description),
			FontSize = 12
		};
		base.AddGuiElement(this.lbl_InfoBoxValue);
		this.lbl_MapName = new GuiLabel()
		{
			boundries = new Rect(67f, 80f, 100f, 25f),
			text = StaticData.Translate(this.onDysplayMap.nameUI).ToUpper(),
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 14,
			Font = GuiLabel.FontBold,
			Alignment = 4
		};
		base.AddGuiElement(this.lbl_MapName);
		this.mapAvatar_TX = new GuiTexture()
		{
			boundries = new Rect(67f, 110f, 100f, 100f)
		};
		this.mapAvatar_TX.SetTextureKeepSize("Targeting", string.Concat(this.onDysplayMap.minimapAssetName, "_avatar"));
		base.AddGuiElement(this.mapAvatar_TX);
		this.lbl_AccessLevel = new GuiLabel()
		{
			boundries = new Rect(37f, 220f, 160f, 30f),
			text = string.Format(StaticData.Translate("key_universe_map_access_level"), this.onDysplayMap.accessLevel),
			Alignment = 4,
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 12
		};
		base.AddGuiElement(this.lbl_AccessLevel);
		this.lbl_RecomendedLevel = new GuiLabel()
		{
			boundries = new Rect(37f, 250f, 160f, 30f),
			text = string.Format(StaticData.Translate("key_universe_map_level_recommendation"), this.onDysplayMap.reqMinLevel, this.onDysplayMap.reqMaxLevel),
			Alignment = 4,
			TextColor = GuiNewStyleBar.orangeColor,
			FontSize = 12
		};
		base.AddGuiElement(this.lbl_RecomendedLevel);
		this.isHidden = false;
		LevelMap[] levelMapArray = StaticData.allGalaxies;
		if (__UniverseMap.<>f__am$cache1B == null)
		{
			__UniverseMap.<>f__am$cache1B = new Func<LevelMap, bool>(null, (LevelMap t) => (t.fraction == NetworkScript.player.vessel.fractionId || !t.isPveMap ? t.__galaxyId < 1000 : false));
		}
		LevelMap[] array = Enumerable.ToArray<LevelMap>(Enumerable.Where<LevelMap>(levelMapArray, __UniverseMap.<>f__am$cache1B));
		LevelMap[] levelMapArray1 = array;
		for (int i = 0; i < (int)levelMapArray1.Length; i++)
		{
			this.MakeGalaxyIcons(levelMapArray1[i]);
		}
		this.PutPlayerIndication();
		if (__UniverseMap.subSection == 1)
		{
			this.OnUltralibriumInfoClicked(null);
		}
		else if (__UniverseMap.subSection == 2 && __UniverseMap.requestedGalaxyId != 0)
		{
			EventHandlerParam eventHandlerParam = new EventHandlerParam()
			{
				customData = __UniverseMap.requestedGalaxyId
			};
			this.OnGameMapInfoClicked(eventHandlerParam);
		}
	}

	private void DrawExtractionPoint(ExtractionPoint point)
	{
		string str = string.Format("ep_fraction{0}Icon", point.ownerFraction);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("MinimapWindow", str);
		guiTexture.boundries = new Rect(196f + (point.x + 384f) * 0.8399f, 71f + (256f - point.z) * 0.7109f, 60f, 60f);
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("NewGUI", "ep_fractionIcon");
		guiButtonFixed.boundries = guiTexture.boundries;
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.hoverParam = point;
		guiButtonFixed.Hovered = new Action<object, bool>(this, __UniverseMap.OnExtractionPointHover);
		guiButtonFixed.groupId = 111;
		guiButtonFixed.behaviourKeepClicked = true;
		guiButtonFixed.eventHandlerParam.customData = point;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, __UniverseMap.CalculateExtractionPointJumpPrice);
		base.AddGuiElement(guiButtonFixed);
		this.forDelete.Add(guiButtonFixed);
		if (point.state == 1)
		{
			GuiTextureAnimated guiTextureAnimated = new GuiTextureAnimated();
			guiTextureAnimated.Init("Warning Arrow", "ExtractionPointArrow", "ExtractionPointArrow/BeingCaptured");
			guiTextureAnimated.rotationTime = 0.9f;
			guiTextureAnimated.boundries.set_x(guiTexture.X + 5f);
			guiTextureAnimated.boundries.set_y(guiTexture.Y + 5f);
			base.AddGuiElement(guiTextureAnimated);
			this.forDelete.Add(guiTextureAnimated);
		}
	}

	private void DrawExtractionPointHoverInfo(ExtractionPoint point)
	{
		this.lbl_MapName.text = StaticData.Translate(point.name);
		this.mapAvatar_TX.SetTextureKeepSize("Targeting", point.assetName);
		this.lbl_AccessLevel.text = string.Empty;
		this.lbl_RecomendedLevel.text = string.Empty;
		this.lbl_InfoBoxValue.text = string.Empty;
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(31f, 215f, 170f, 16f),
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			Alignment = 4,
			text = StaticData.Translate("universe_map_point_coordinates")
		};
		base.AddGuiElement(guiLabel);
		this.forExtractionPointHover.Add(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(31f, 230f, 170f, 14f),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			Alignment = 4,
			text = string.Format("[{0}:{1}]", (float)(this.onDysplayMap.width / 2) + point.x, (float)(this.onDysplayMap.height / 2) - point.z)
		};
		base.AddGuiElement(guiLabel1);
		this.forExtractionPointHover.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(31f, 253f, 170f, 14f),
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			Alignment = 4,
			text = StaticData.Translate("universe_map_point_status")
		};
		base.AddGuiElement(guiLabel2);
		this.forExtractionPointHover.Add(guiLabel2);
		GuiLabel _white = new GuiLabel()
		{
			boundries = new Rect(31f, 268f, 170f, 14f),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			Alignment = 4
		};
		if (point.state != 1)
		{
			_white.text = StaticData.Translate("universe_map_point_in_control");
			_white.TextColor = Color.get_white();
		}
		else
		{
			_white.text = StaticData.Translate("universe_map_point_being_captured");
			_white.TextColor = GuiNewStyleBar.redColor;
		}
		base.AddGuiElement(_white);
		this.forExtractionPointHover.Add(_white);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(31f, 330f, 170f, 18f),
			TextColor = GuiNewStyleBar.blueColor,
			FontSize = 14,
			Font = GuiLabel.FontBold,
			Alignment = 4,
			text = StaticData.Translate("universe_map_point_top_contributors")
		};
		base.AddGuiElement(guiLabel3);
		this.forExtractionPointHover.Add(guiLabel3);
		int num = 0;
		List<Contributor> list = point.topTenContributors;
		if (__UniverseMap.<>f__am$cache21 == null)
		{
			__UniverseMap.<>f__am$cache21 = new Func<Contributor, int>(null, (Contributor t) => t.incomeBonus);
		}
		IOrderedEnumerable<Contributor> orderedEnumerable = Enumerable.OrderByDescending<Contributor, int>(list, __UniverseMap.<>f__am$cache21);
		if (__UniverseMap.<>f__am$cache22 == null)
		{
			__UniverseMap.<>f__am$cache22 = new Func<Contributor, int>(null, (Contributor u) => u.tottalContribution);
		}
		IEnumerator<Contributor> enumerator = Enumerable.OrderByDescending<Contributor, int>(orderedEnumerable, __UniverseMap.<>f__am$cache22).GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Contributor current = enumerator.get_Current();
				GuiLabel guiLabel4 = new GuiLabel()
				{
					boundries = new Rect(36f, (float)(355 + num * 25), 160f, 14f),
					FontSize = 12,
					Font = GuiLabel.FontBold,
					Alignment = 3,
					text = current.displayName
				};
				base.AddGuiElement(guiLabel4);
				this.forExtractionPointHover.Add(guiLabel4);
				GuiLabel guiLabel5 = new GuiLabel()
				{
					boundries = new Rect(36f, (float)(355 + num * 25), 160f, 14f),
					TextColor = GuiNewStyleBar.greenColor,
					FontSize = 12,
					Font = GuiLabel.FontBold,
					Alignment = 5,
					text = string.Format(StaticData.Translate("universe_map_fraction_income_value"), current.incomeBonus)
				};
				base.AddGuiElement(guiLabel5);
				this.forExtractionPointHover.Add(guiLabel5);
				GuiTexture guiTexture = new GuiTexture();
				guiTexture.SetTexture("NewGUI", "blueDot");
				guiTexture.boundries = new Rect(36f, (float)(375 + num * 25), 160f, 1f);
				base.AddGuiElement(guiTexture);
				this.forExtractionPointHover.Add(guiTexture);
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
	}

	private void DrawInstancesInfo(LevelMap map)
	{
		__UniverseMap.<DrawInstancesInfo>c__AnonStoreyA9 variable = null;
		this.SetAccessAndRecomendation(map);
		this.ClearInstancesInfo();
		HyperJumpNet[] hyperJumpNetArray = map.hyperJumps;
		if (__UniverseMap.<>f__am$cache1F == null)
		{
			__UniverseMap.<>f__am$cache1F = new Func<HyperJumpNet, bool>(null, (HyperJumpNet t) => t.galaxyDst > 1000);
		}
		HyperJumpNet[] array = Enumerable.ToArray<HyperJumpNet>(Enumerable.Where<HyperJumpNet>(hyperJumpNetArray, __UniverseMap.<>f__am$cache1F));
		float _height = 0f;
		if ((int)array.Length <= 0)
		{
			return;
		}
		_height = this.boundries.get_height() - (float)((int)array.Length * 25) - 20f;
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(35f, _height, 160f, 15f),
			text = StaticData.Translate("key_universe_map_instances"),
			Alignment = 3,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor
		};
		base.AddGuiElement(guiLabel);
		this.forInstancesInfo.Add(guiLabel);
		_height = _height + 15f;
		for (int i = 0; i < (int)array.Length; i++)
		{
			GuiTexture guiTexture = new GuiTexture();
			guiTexture.SetTexture("MinimapWindow", "portal_instance");
			guiTexture.boundries = new Rect(32f, _height + (float)(i * 25), 25f, 25f);
			base.AddGuiElement(guiTexture);
			this.forInstancesInfo.Add(guiTexture);
			GuiLabel guiLabel1 = new GuiLabel()
			{
				boundries = new Rect(60f, _height + (float)(i * 25), 135f, 25f),
				text = StaticData.Translate(Enumerable.First<LevelMap>(Enumerable.Where<LevelMap>(StaticData.allGalaxies, new Func<LevelMap, bool>(variable, (LevelMap t) => t.get_galaxyId() == this.<>f__ref$168.jumps[this.i].galaxyDst))).nameUI),
				Alignment = 3,
				FontSize = 12,
				Font = GuiLabel.FontBold
			};
			base.AddGuiElement(guiLabel1);
			this.forInstancesInfo.Add(guiLabel1);
		}
	}

	private void DrawUltralibriumInfo(int fractionOne, int fractionTwo)
	{
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("NewGUI", "universeStatsframe");
		guiButtonFixed.X = 211f;
		guiButtonFixed.Y = 73f;
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, __UniverseMap.OnUltralibriumInfoClicked);
		base.AddGuiElement(guiButtonFixed);
		this.forDelete.Add(guiButtonFixed);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("FrameworkGUI", "fraction1Icon");
		guiTexture.X = 222f;
		guiTexture.Y = 82f;
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("FrameworkGUI", "fraction2Icon");
		guiTexture1.X = 221f;
		guiTexture1.Y = 107f;
		base.AddGuiElement(guiTexture1);
		this.forDelete.Add(guiTexture1);
		GuiTexture guiTexture2 = new GuiTexture();
		guiTexture2.SetTexture("FrameworkGUI", "res_ultralibrium");
		guiTexture2.X = 258f;
		guiTexture2.Y = 83f;
		base.AddGuiElement(guiTexture2);
		this.forDelete.Add(guiTexture2);
		GuiTexture guiTexture3 = new GuiTexture();
		guiTexture3.SetTexture("FrameworkGUI", "res_ultralibrium");
		guiTexture3.X = 258f;
		guiTexture3.Y = 108f;
		base.AddGuiElement(guiTexture3);
		this.forDelete.Add(guiTexture3);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(278f, 83f, 75f, 16f),
			TextColor = GuiNewStyleBar.greenColor,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			Alignment = 3,
			text = fractionOne.ToString("##,##0")
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(278f, 108f, 75f, 16f),
			TextColor = GuiNewStyleBar.greenColor,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			Alignment = 3,
			text = fractionTwo.ToString("##,##0")
		};
		base.AddGuiElement(guiLabel1);
		this.forDelete.Add(guiLabel1);
	}

	private void MakeGalaxyIcons(LevelMap map)
	{
		int num = 0;
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("NewGUI", "universeMapNormal");
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.hoverParam = map;
		guiButtonFixed.Hovered = new Action<object, bool>(this, __UniverseMap.OnGalaxyHover);
		guiButtonFixed.groupId = 123;
		guiButtonFixed.behaviourKeepClicked = true;
		guiButtonFixed.eventHandlerParam.customData = map.get_galaxyId();
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, __UniverseMap.CalculateJumpPrice);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(210f, 304f, 80f, 25f),
			text = StaticData.Translate(map.nameUI),
			Alignment = 4,
			FontSize = 12,
			TextColor = GuiNewStyleBar.blueColor
		};
		GuiButtonFixed empty = new GuiButtonFixed();
		empty.SetTexture("NewGUI", "universeMapExplore");
		empty.Caption = string.Empty;
		empty.hoverParam = map;
		empty.Hovered = new Action<object, bool>(this, __UniverseMap.OnGalaxyHover);
		empty.eventHandlerParam.customData = map.get_galaxyId();
		empty.Clicked = new Action<EventHandlerParam>(this, __UniverseMap.OnGameMapInfoClicked);
		string str = map.minimapAssetName;
		if (str != null)
		{
			if (__UniverseMap.<>f__switch$map5 == null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(16);
				dictionary.Add("Hydra", 0);
				dictionary.Add("Mensa", 1);
				dictionary.Add("CanisMinor", 2);
				dictionary.Add("CanisMajor", 3);
				dictionary.Add("Orion", 4);
				dictionary.Add("Centaurus", 5);
				dictionary.Add("Cepheus", 6);
				dictionary.Add("Lynx", 7);
				dictionary.Add("UrsaMinor", 8);
				dictionary.Add("UrsaMajor", 9);
				dictionary.Add("Taurus", 10);
				dictionary.Add("Scorpio", 11);
				dictionary.Add("Andromeda", 12);
				dictionary.Add("Cassiopeia", 13);
				dictionary.Add("Pegasus", 14);
				dictionary.Add("Perseus", 15);
				__UniverseMap.<>f__switch$map5 = dictionary;
			}
			if (__UniverseMap.<>f__switch$map5.TryGetValue(str, ref num))
			{
				switch (num)
				{
					case 0:
					{
						guiButtonFixed.X = 221f;
						guiButtonFixed.Y = 169f;
						guiLabel.X = 216f;
						guiLabel.Y = 235f;
						break;
					}
					case 1:
					{
						guiButtonFixed.X = 263f;
						guiButtonFixed.Y = 266f;
						guiLabel.X = 257f;
						guiLabel.Y = 334f;
						break;
					}
					case 2:
					{
						guiButtonFixed.X = 273f;
						guiButtonFixed.Y = 376f;
						guiLabel.X = 266f;
						guiLabel.Y = 442f;
						break;
					}
					case 3:
					{
						guiButtonFixed.X = 439f;
						guiButtonFixed.Y = 380f;
						guiLabel.X = 434f;
						guiLabel.Y = 445f;
						break;
					}
					case 4:
					{
						guiButtonFixed.X = 519f;
						guiButtonFixed.Y = 286f;
						guiLabel.X = 514f;
						guiLabel.Y = 351f;
						break;
					}
					case 5:
					{
						guiButtonFixed.X = 414f;
						guiButtonFixed.Y = 219f;
						guiLabel.X = 410f;
						guiLabel.Y = 286f;
						break;
					}
					case 6:
					{
						guiButtonFixed.X = 389f;
						guiButtonFixed.Y = 109f;
						guiLabel.X = 384f;
						guiLabel.Y = 175f;
						break;
					}
					case 7:
					{
						guiButtonFixed.X = 546f;
						guiButtonFixed.Y = 82f;
						guiLabel.X = 542f;
						guiLabel.Y = 148f;
						break;
					}
					case 8:
					{
						guiButtonFixed.X = 670f;
						guiButtonFixed.Y = 103f;
						guiLabel.X = 666f;
						guiLabel.Y = 170f;
						break;
					}
					case 9:
					{
						guiButtonFixed.X = 720f;
						guiButtonFixed.Y = 209f;
						guiLabel.X = 713f;
						guiLabel.Y = 275f;
						break;
					}
					case 10:
					{
						guiButtonFixed.SetTexture("NewGUI", "universeMapMiddle");
						guiLabel.TextColor = Color.get_red();
						guiButtonFixed.X = 807f;
						guiButtonFixed.Y = 284f;
						guiLabel.X = 803f;
						guiLabel.Y = 349f;
						empty.X = 850f;
						empty.Y = 258f;
						base.AddGuiElement(empty);
						this.forDelete.Add(empty);
						break;
					}
					case 11:
					{
						guiButtonFixed.SetTexture("NewGUI", "universeMapMiddle");
						guiLabel.TextColor = Color.get_red();
						guiButtonFixed.X = 616f;
						guiButtonFixed.Y = 248f;
						guiLabel.X = 611f;
						guiLabel.Y = 314f;
						empty.X = 676f;
						empty.Y = 280f;
						base.AddGuiElement(empty);
						this.forDelete.Add(empty);
						break;
					}
					case 12:
					{
						guiButtonFixed.SetTexture("NewGUI", "universeMapBig");
						guiLabel.TextColor = Color.get_red();
						guiButtonFixed.X = 671f;
						guiButtonFixed.Y = 324f;
						guiLabel.X = 694f;
						guiLabel.Y = 444f;
						empty.X = 788f;
						empty.Y = 377f;
						base.AddGuiElement(empty);
						this.forDelete.Add(empty);
						break;
					}
					case 13:
					{
						guiButtonFixed.SetTexture("NewGUI", "universeMapMiddle");
						guiLabel.TextColor = Color.get_red();
						guiButtonFixed.X = 365f;
						guiButtonFixed.Y = 309f;
						guiLabel.X = 358f;
						guiLabel.Y = 376f;
						empty.X = 426f;
						empty.Y = 334f;
						base.AddGuiElement(empty);
						this.forDelete.Add(empty);
						break;
					}
					case 14:
					{
						guiButtonFixed.SetTexture("NewGUI", "universeMapMiddle");
						guiLabel.TextColor = Color.get_red();
						guiButtonFixed.X = 493f;
						guiButtonFixed.Y = 158f;
						guiLabel.X = 489f;
						guiLabel.Y = 224f;
						empty.X = 555f;
						empty.Y = 180f;
						base.AddGuiElement(empty);
						this.forDelete.Add(empty);
						break;
					}
					case 15:
					{
						guiButtonFixed.SetTexture("NewGUI", "universeMapMiddle");
						guiLabel.TextColor = Color.get_red();
						guiButtonFixed.X = 762f;
						guiButtonFixed.Y = 105f;
						guiLabel.X = 759f;
						guiLabel.Y = 170f;
						empty.X = 824f;
						empty.Y = 126f;
						base.AddGuiElement(empty);
						this.forDelete.Add(empty);
						break;
					}
					default:
					{
						Debug.Log("No info for this map");
						return;
					}
				}
				base.AddGuiElement(guiButtonFixed);
				base.AddGuiElement(guiLabel);
				this.forDelete.Add(guiButtonFixed);
				this.forDelete.Add(guiLabel);
				if (NetworkScript.player.playerBelongings.playerAccessLevel < map.accessLevel || !map.isPveMap && (NetworkScript.player.playerBelongings.playerLevel > map.reqMaxLevel || NetworkScript.player.playerBelongings.playerLevel < map.reqMinLevel))
				{
					GuiTexture guiTexture = new GuiTexture();
					guiTexture.SetTexture("GUI", "padlock");
					guiTexture.boundries = new Rect(0f, 0f, 45f, 45f);
					guiTexture.X = (guiButtonFixed.boundries.get_xMax() - guiButtonFixed.boundries.get_x() - 45f) / 2f + guiButtonFixed.X;
					guiTexture.Y = (guiButtonFixed.boundries.get_yMax() - guiButtonFixed.boundries.get_y() - 45f) / 2f + guiButtonFixed.Y;
					base.AddGuiElement(guiTexture);
					this.forDelete.Add(guiTexture);
				}
				return;
			}
		}
		Debug.Log("No info for this map");
	}

	public void OnBackBtnClicked(object prm)
	{
		__UniverseMap.subSection = 0;
		__UniverseMap.requestedGalaxyId = 0;
		this.Clear();
		this.mainScreenBG.SetTextureKeepSize("NewGUI", "universeMapTab");
		LevelMap[] levelMapArray = StaticData.allGalaxies;
		if (__UniverseMap.<>f__am$cache24 == null)
		{
			__UniverseMap.<>f__am$cache24 = new Func<LevelMap, bool>(null, (LevelMap t) => (t.fraction == NetworkScript.player.vessel.fractionId || !t.isPveMap ? t.__galaxyId < 1000 : false));
		}
		LevelMap[] array = Enumerable.ToArray<LevelMap>(Enumerable.Where<LevelMap>(levelMapArray, __UniverseMap.<>f__am$cache24));
		LevelMap[] levelMapArray1 = array;
		for (int i = 0; i < (int)levelMapArray1.Length; i++)
		{
			this.MakeGalaxyIcons(levelMapArray1[i]);
		}
		this.PutPlayerIndication();
		this.btnJumpNova.Caption = StaticData.Translate("key_universe_map_btn_jump");
		this.btnJumpNova.isEnabled = false;
		this.btnJumpEq.Caption = StaticData.Translate("key_universe_map_btn_jump");
		this.btnJumpEq.isEnabled = false;
		this.DrawUltralibriumInfo(this.fractionOneUltralibrium, this.fractionTwoUltralibrium);
		this.DrawInstancesInfo(this.onDysplayMap);
	}

	private void OnEqJumpBtnClick(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void __UniverseMap::OnEqJumpBtnClick(EventHandlerParam)
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

	private void OnExtractionPointHover(object prm, bool state)
	{
		__UniverseMap.<OnExtractionPointHover>c__AnonStoreyAB variable = null;
		if (prm == null)
		{
			return;
		}
		ExtractionPoint extractionPoint = (ExtractionPoint)prm;
		ExtractionPointInfo extractionPointInfo = Enumerable.First<ExtractionPointInfo>(Enumerable.Where<ExtractionPointInfo>(StaticData.allExtractionPoints, new Func<ExtractionPointInfo, bool>(variable, (ExtractionPointInfo t) => t.id == this.ep.pointId)));
		extractionPoint.x = (float)extractionPointInfo.possitionX;
		extractionPoint.z = (float)extractionPointInfo.possitionZ;
		extractionPoint.name = extractionPointInfo.name;
		extractionPoint.assetName = extractionPointInfo.assetName;
		this.ClearExtractionInfo();
		if (state)
		{
			this.DrawExtractionPointHoverInfo(extractionPoint);
		}
		else if (this.selectedPoint == null)
		{
			this.lbl_MapName.text = StaticData.Translate(this.onDysplayMap.nameUI);
			this.mapAvatar_TX.SetTextureKeepSize("Targeting", string.Concat(this.onDysplayMap.minimapAssetName, "_avatar"));
			this.lbl_AccessLevel.text = string.Format(StaticData.Translate("key_universe_map_access_level"), this.onDysplayMap.accessLevel);
			this.lbl_RecomendedLevel.text = string.Format(StaticData.Translate("key_universe_map_level_recommendation"), this.onDysplayMap.reqMinLevel, this.onDysplayMap.reqMaxLevel);
			this.lbl_InfoBoxValue.text = StaticData.Translate(this.onDysplayMap.description);
			this.SetAccessAndRecomendation(this.onDysplayMap);
		}
		else
		{
			this.DrawExtractionPointHoverInfo(this.selectedPoint);
		}
	}

	private void OnGalaxyHover(object prm, bool state)
	{
		if (prm == null)
		{
			return;
		}
		LevelMap levelMap = (LevelMap)prm;
		if (!state)
		{
			this.lbl_MapName.text = StaticData.Translate(this.onDysplayMap.nameUI);
			this.mapAvatar_TX.SetTextureKeepSize("Targeting", string.Concat(this.onDysplayMap.minimapAssetName, "_avatar"));
			this.lbl_AccessLevel.text = string.Format(StaticData.Translate("key_universe_map_access_level"), this.onDysplayMap.accessLevel);
			this.lbl_RecomendedLevel.text = string.Format(StaticData.Translate("key_universe_map_level_recommendation"), this.onDysplayMap.reqMinLevel, this.onDysplayMap.reqMaxLevel);
			this.lbl_InfoBoxValue.text = StaticData.Translate(this.onDysplayMap.description);
			this.DrawInstancesInfo(this.onDysplayMap);
		}
		else
		{
			this.lbl_MapName.text = StaticData.Translate(levelMap.nameUI);
			this.mapAvatar_TX.SetTextureKeepSize("Targeting", string.Concat(levelMap.minimapAssetName, "_avatar"));
			this.lbl_AccessLevel.text = string.Format(StaticData.Translate("key_universe_map_access_level"), levelMap.accessLevel);
			this.lbl_RecomendedLevel.text = string.Format(StaticData.Translate("key_universe_map_level_recommendation"), levelMap.reqMinLevel, levelMap.reqMaxLevel);
			this.lbl_InfoBoxValue.text = StaticData.Translate(levelMap.description);
			this.DrawInstancesInfo(levelMap);
		}
	}

	private void OnGameMapInfoClicked(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void __UniverseMap::OnGameMapInfoClicked(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnGameMapInfoClicked(EventHandlerParam)
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
		// Current member / type: System.Void __UniverseMap::OnNovaJumpBtnClick(EventHandlerParam)
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

	private void OnUltralibriumInfoClicked(object prm)
	{
		__UniverseMap.subSection = 1;
		playWebGame.udp.ExecuteCommand(187, null);
		this.Clear();
		this.mainScreenBG.SetTextureKeepSize("FrameworkGUI", "empty");
		this.btnJumpNova.Caption = StaticData.Translate("key_universe_map_btn_jump");
		this.btnJumpNova.isEnabled = false;
		this.btnJumpEq.Caption = StaticData.Translate("key_universe_map_btn_jump");
		this.btnJumpEq.isEnabled = false;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("NewGUI", "fraction1BigIcon");
		guiTexture.boundries = new Rect(250f, 103f, 70f, 50f);
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(318f, 102f, 225f, 26f),
			FontSize = 16,
			Font = GuiLabel.FontBold,
			Alignment = 3,
			text = StaticData.Translate("key_login_reg_fraction_one")
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("NewGUI", "fraction2BigIcon");
		rect.boundries = new Rect(589f, 103f, 70f, 50f);
		base.AddGuiElement(rect);
		this.forDelete.Add(rect);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(660f, 102f, 225f, 26f),
			FontSize = 16,
			Font = GuiLabel.FontBold,
			Alignment = 3,
			text = StaticData.Translate("key_login_reg_fraction_two")
		};
		base.AddGuiElement(guiLabel1);
		this.forDelete.Add(guiLabel1);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("NewGUI", "blueDot");
		guiTexture1.boundries = new Rect(550f, 100f, 1f, 360f);
		base.AddGuiElement(guiTexture1);
		this.forDelete.Add(guiTexture1);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.SetSmallBlueTexture();
		guiButtonResizeable.X = 31f;
		guiButtonResizeable.Y = 505f;
		guiButtonResizeable.boundries.set_width(170f);
		guiButtonResizeable.Caption = StaticData.Translate("btn_back");
		guiButtonResizeable.FontSize = 15;
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, __UniverseMap.OnBackBtnClicked);
		guiButtonResizeable.Alignment = 4;
		base.AddGuiElement(guiButtonResizeable);
		this.forDelete.Add(guiButtonResizeable);
	}

	public void PopulateFractionInfo(UniversalTransportContainer data)
	{
		string str;
		this.ClearInstancesInfo();
		this.lbl_MapName.text = StaticData.Translate("universe_map_lbl_scoreboard");
		this.mapAvatar_TX.SetTextureKeepSize("NewGUI", "universeVsStats");
		this.lbl_AccessLevel.text = string.Empty;
		this.lbl_RecomendedLevel.text = string.Empty;
		this.lbl_InfoBoxValue.text = StaticData.Translate("universe_map_fraction_infobox");
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(318f, 130f, 225f, 26f),
			FontSize = 12,
			Alignment = 3,
			text = string.Format(StaticData.Translate("universe_map_fraction_players_cnt"), data.fractionOneInfo.int5)
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(660f, 130f, 225f, 26f),
			FontSize = 12,
			Alignment = 3,
			text = string.Format(StaticData.Translate("universe_map_fraction_players_cnt"), data.fractionTwoInfo.int5)
		};
		base.AddGuiElement(guiLabel1);
		this.forDelete.Add(guiLabel1);
		this.fractionOneUltralibrium = data.fractionOneInfo.int1;
		this.fractionTwoUltralibrium = data.fractionTwoInfo.int1;
		for (int i = 0; i < 5; i++)
		{
			GuiLabel guiLabel2 = new GuiLabel()
			{
				boundries = new Rect(240f, (float)(182 + i * 45), 280f, 30f),
				TextColor = GuiNewStyleBar.blueColor,
				FontSize = 12,
				Font = GuiLabel.FontBold,
				Alignment = 3,
				text = string.Empty
			};
			base.AddGuiElement(guiLabel2);
			this.forDelete.Add(guiLabel2);
			GuiLabel str1 = new GuiLabel()
			{
				boundries = new Rect(240f, (float)(182 + i * 45), 280f, 30f),
				FontSize = 12,
				Font = GuiLabel.FontBold,
				Alignment = 5,
				text = string.Empty
			};
			base.AddGuiElement(str1);
			this.forDelete.Add(str1);
			GuiTexture guiTexture = new GuiTexture();
			guiTexture.SetTexture("NewGUI", "blueDot");
			guiTexture.boundries = new Rect(240f, (float)(220 + i * 45), 280f, 1f);
			base.AddGuiElement(guiTexture);
			this.forDelete.Add(guiTexture);
			GuiLabel guiLabel3 = new GuiLabel()
			{
				boundries = new Rect(580f, (float)(182 + i * 45), 280f, 30f),
				TextColor = GuiNewStyleBar.blueColor,
				FontSize = 12,
				Font = GuiLabel.FontBold,
				Alignment = 3,
				text = string.Empty
			};
			base.AddGuiElement(guiLabel3);
			this.forDelete.Add(guiLabel3);
			GuiLabel str2 = new GuiLabel()
			{
				boundries = new Rect(580f, (float)(182 + i * 45), 280f, 30f),
				FontSize = 12,
				Font = GuiLabel.FontBold,
				Alignment = 5,
				text = string.Empty
			};
			base.AddGuiElement(str2);
			this.forDelete.Add(str2);
			GuiTexture rect = new GuiTexture();
			rect.SetTexture("NewGUI", "blueDot");
			rect.boundries = new Rect(580f, (float)(220 + i * 45), 280f, 1f);
			base.AddGuiElement(rect);
			this.forDelete.Add(rect);
			switch (i)
			{
				case 0:
				{
					string str3 = StaticData.Translate("universe_map_fraction_gained");
					str = str3;
					guiLabel3.text = str3;
					guiLabel2.text = str;
					str1.text = data.fractionOneInfo.int1.ToString("##,##0");
					str2.text = data.fractionTwoInfo.int1.ToString("##,##0");
					if (data.fractionOneInfo.int1 > data.fractionTwoInfo.int1)
					{
						guiLabel2.TextColor = GuiNewStyleBar.orangeColor;
						GuiTexture guiTexture1 = new GuiTexture();
						guiTexture1.SetTexture("NewGUI", "universeStatsWin");
						guiTexture1.boundries = new Rect(215f, (float)(187 + i * 45), 20f, 20f);
						base.AddGuiElement(guiTexture1);
						this.forDelete.Add(guiTexture1);
					}
					else if (data.fractionOneInfo.int1 < data.fractionTwoInfo.int1)
					{
						guiLabel3.TextColor = GuiNewStyleBar.orangeColor;
						GuiTexture rect1 = new GuiTexture();
						rect1.SetTexture("NewGUI", "universeStatsWin");
						rect1.boundries = new Rect(555f, (float)(187 + i * 45), 20f, 20f);
						base.AddGuiElement(rect1);
						this.forDelete.Add(rect1);
					}
					break;
				}
				case 1:
				{
					string str4 = StaticData.Translate("universe_map_fraction_income");
					str = str4;
					guiLabel3.text = str4;
					guiLabel2.text = str;
					str1.text = string.Format(StaticData.Translate("universe_map_fraction_income_value"), data.fractionOneInfo.int3);
					str2.text = string.Format(StaticData.Translate("universe_map_fraction_income_value"), data.fractionTwoInfo.int3);
					if (data.fractionOneInfo.int3 > data.fractionTwoInfo.int3)
					{
						guiLabel2.TextColor = GuiNewStyleBar.orangeColor;
						GuiTexture guiTexture2 = new GuiTexture();
						guiTexture2.SetTexture("NewGUI", "universeStatsWin");
						guiTexture2.boundries = new Rect(215f, (float)(187 + i * 45), 20f, 20f);
						base.AddGuiElement(guiTexture2);
						this.forDelete.Add(guiTexture2);
					}
					else if (data.fractionOneInfo.int3 < data.fractionTwoInfo.int3)
					{
						guiLabel3.TextColor = GuiNewStyleBar.orangeColor;
						GuiTexture rect2 = new GuiTexture();
						rect2.SetTexture("NewGUI", "universeStatsWin");
						rect2.boundries = new Rect(555f, (float)(187 + i * 45), 20f, 20f);
						base.AddGuiElement(rect2);
						this.forDelete.Add(rect2);
					}
					break;
				}
				case 2:
				{
					string str5 = StaticData.Translate("universe_map_fraction_points_cnt");
					str = str5;
					guiLabel3.text = str5;
					guiLabel2.text = str;
					str1.text = data.fractionOneInfo.int4.ToString();
					str2.text = data.fractionTwoInfo.int4.ToString();
					if (data.fractionOneInfo.int4 > data.fractionTwoInfo.int4)
					{
						guiLabel2.TextColor = GuiNewStyleBar.orangeColor;
						GuiTexture guiTexture3 = new GuiTexture();
						guiTexture3.SetTexture("NewGUI", "universeStatsWin");
						guiTexture3.boundries = new Rect(215f, (float)(187 + i * 45), 20f, 20f);
						base.AddGuiElement(guiTexture3);
						this.forDelete.Add(guiTexture3);
					}
					else if (data.fractionOneInfo.int4 < data.fractionTwoInfo.int4)
					{
						guiLabel3.TextColor = GuiNewStyleBar.orangeColor;
						GuiTexture rect3 = new GuiTexture();
						rect3.SetTexture("NewGUI", "universeStatsWin");
						rect3.boundries = new Rect(555f, (float)(187 + i * 45), 20f, 20f);
						base.AddGuiElement(rect3);
						this.forDelete.Add(rect3);
					}
					break;
				}
				case 3:
				{
					string str6 = StaticData.Translate("universe_map_fraction_best_time");
					str = str6;
					guiLabel3.text = str6;
					guiLabel2.text = str;
					str1.text = string.Format(StaticData.Translate("key_profile_screen_playtime_value"), data.fractionOneInfo.long1 / (long)86400, data.fractionOneInfo.long1 / (long)3600 % (long)24, data.fractionOneInfo.long1 / (long)60 % (long)60);
					str2.text = string.Format(StaticData.Translate("key_profile_screen_playtime_value"), data.fractionTwoInfo.long1 / (long)86400, data.fractionTwoInfo.long1 / (long)3600 % (long)24, data.fractionTwoInfo.long1 / (long)60 % (long)60);
					if (data.fractionOneInfo.long1 > data.fractionTwoInfo.long1)
					{
						guiLabel2.TextColor = GuiNewStyleBar.orangeColor;
						GuiTexture guiTexture4 = new GuiTexture();
						guiTexture4.SetTexture("NewGUI", "universeStatsWin");
						guiTexture4.boundries = new Rect(215f, (float)(187 + i * 45), 20f, 20f);
						base.AddGuiElement(guiTexture4);
						this.forDelete.Add(guiTexture4);
					}
					else if (data.fractionOneInfo.long1 < data.fractionTwoInfo.long1)
					{
						guiLabel3.TextColor = GuiNewStyleBar.orangeColor;
						GuiTexture rect4 = new GuiTexture();
						rect4.SetTexture("NewGUI", "universeStatsWin");
						rect4.boundries = new Rect(555f, (float)(187 + i * 45), 20f, 20f);
						base.AddGuiElement(rect4);
						this.forDelete.Add(rect4);
					}
					break;
				}
				case 4:
				{
					string str7 = StaticData.Translate("universe_map_fraction_best_contributor");
					str = str7;
					guiLabel3.text = str7;
					guiLabel2.text = str;
					str1.text = data.fractionOneInfo.str1;
					str2.text = data.fractionTwoInfo.str1;
					break;
				}
			}
		}
	}

	public void PopulateGameMapInfo(UniversalTransportContainer data)
	{
		__UniverseMap.<PopulateGameMapInfo>c__AnonStoreyAA variable = null;
		if (__UniverseMap.requestedGalaxyId != data.galaxyId)
		{
			return;
		}
		string str = string.Concat("universeBknd_", Enumerable.First<LevelMap>(Enumerable.Where<LevelMap>(StaticData.allGalaxies, new Func<LevelMap, bool>(variable, (LevelMap t) => t.get_galaxyId() == this.data.galaxyId))).minimapAssetName);
		this.mainScreenBG.SetTextureKeepSize("NewGUI", str);
		foreach (ExtractionPoint datum in data.extractionPointsOnMap)
		{
			this.DrawExtractionPoint(datum);
		}
	}

	public void PopulateUltralibrium(UniversalTransportContainer data)
	{
		this.fractionOneUltralibrium = data.fractionOneInfo.int1;
		this.fractionTwoUltralibrium = data.fractionTwoInfo.int1;
		if (__UniverseMap.subSection == 0)
		{
			this.DrawUltralibriumInfo(this.fractionOneUltralibrium, this.fractionTwoUltralibrium);
		}
	}

	private void PutPlayerIndication()
	{
		__UniverseMap.<PutPlayerIndication>c__AnonStoreyA6 variable = null;
		int num = 0;
		if (NetworkScript.player.vessel.galaxy.get_galaxyId() >= 3000 || NetworkScript.player.vessel.galaxy.IsUltraGalaxyInstance())
		{
			return;
		}
		string empty = string.Empty;
		if (NetworkScript.player.vessel.galaxy.get_galaxyId() < 1000)
		{
			empty = NetworkScript.player.vessel.galaxy.minimapAssetName;
			LevelMap[] levelMapArray = StaticData.allGalaxies;
			if (__UniverseMap.<>f__am$cache1E == null)
			{
				__UniverseMap.<>f__am$cache1E = new Func<LevelMap, bool>(null, (LevelMap t) => t.get_galaxyId() == NetworkScript.player.vessel.galaxy.get_galaxyId());
			}
			this.onDysplayMap = Enumerable.First<LevelMap>(Enumerable.Where<LevelMap>(levelMapArray, __UniverseMap.<>f__am$cache1E));
		}
		else
		{
			Enumerable.First<HyperJumpNet>(NetworkScript.player.vessel.galaxy.hyperJumps);
			LevelMap levelMap = Enumerable.First<LevelMap>(Enumerable.Where<LevelMap>(StaticData.allGalaxies, new Func<LevelMap, bool>(variable, (LevelMap g) => g.get_galaxyId() == this.hj.galaxyDst)));
			empty = levelMap.minimapAssetName;
			this.onDysplayMap = levelMap;
		}
		this.lbl_MapName.text = StaticData.Translate(this.onDysplayMap.nameUI);
		this.mapAvatar_TX.SetTextureKeepSize("Targeting", string.Concat(this.onDysplayMap.minimapAssetName, "_avatar"));
		this.lbl_AccessLevel.text = string.Format(StaticData.Translate("key_universe_map_access_level"), this.onDysplayMap.accessLevel);
		this.lbl_RecomendedLevel.text = string.Format(StaticData.Translate("key_universe_map_level_recommendation"), this.onDysplayMap.reqMinLevel, this.onDysplayMap.reqMaxLevel);
		this.SetAccessAndRecomendation(this.onDysplayMap);
		this.lbl_InfoBoxValue.text = StaticData.Translate(this.onDysplayMap.description);
		this.shipIndication = new GuiTexture();
		this.shipIndication.SetTexture("NewGUI", "shipIndication");
		base.AddGuiElement(this.shipIndication);
		this.forDelete.Add(this.shipIndication);
		string str = empty;
		if (str != null)
		{
			if (__UniverseMap.<>f__switch$map6 == null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(16);
				dictionary.Add("Hydra", 0);
				dictionary.Add("Mensa", 1);
				dictionary.Add("CanisMinor", 2);
				dictionary.Add("CanisMajor", 3);
				dictionary.Add("Orion", 4);
				dictionary.Add("Centaurus", 5);
				dictionary.Add("Cepheus", 6);
				dictionary.Add("Lynx", 7);
				dictionary.Add("UrsaMinor", 8);
				dictionary.Add("UrsaMajor", 9);
				dictionary.Add("Taurus", 10);
				dictionary.Add("Scorpio", 11);
				dictionary.Add("Andromeda", 12);
				dictionary.Add("Cassiopeia", 13);
				dictionary.Add("Pegasus", 14);
				dictionary.Add("Perseus", 15);
				__UniverseMap.<>f__switch$map6 = dictionary;
			}
			if (__UniverseMap.<>f__switch$map6.TryGetValue(str, ref num))
			{
				switch (num)
				{
					case 0:
					{
						this.shipIndication.X = 221f;
						this.shipIndication.Y = 169f;
						break;
					}
					case 1:
					{
						this.shipIndication.X = 263f;
						this.shipIndication.Y = 266f;
						break;
					}
					case 2:
					{
						this.shipIndication.X = 273f;
						this.shipIndication.Y = 376f;
						break;
					}
					case 3:
					{
						this.shipIndication.X = 439f;
						this.shipIndication.Y = 380f;
						break;
					}
					case 4:
					{
						this.shipIndication.X = 519f;
						this.shipIndication.Y = 286f;
						break;
					}
					case 5:
					{
						this.shipIndication.X = 414f;
						this.shipIndication.Y = 219f;
						break;
					}
					case 6:
					{
						this.shipIndication.X = 389f;
						this.shipIndication.Y = 109f;
						break;
					}
					case 7:
					{
						this.shipIndication.X = 546f;
						this.shipIndication.Y = 82f;
						break;
					}
					case 8:
					{
						this.shipIndication.X = 670f;
						this.shipIndication.Y = 103f;
						break;
					}
					case 9:
					{
						this.shipIndication.X = 720f;
						this.shipIndication.Y = 209f;
						break;
					}
					case 10:
					{
						this.shipIndication.X = 807f;
						this.shipIndication.Y = 284f;
						break;
					}
					case 11:
					{
						this.shipIndication.X = 616f;
						this.shipIndication.Y = 248f;
						break;
					}
					case 12:
					{
						this.shipIndication.X = 701f;
						this.shipIndication.Y = 354f;
						break;
					}
					case 13:
					{
						this.shipIndication.X = 365f;
						this.shipIndication.Y = 309f;
						break;
					}
					case 14:
					{
						this.shipIndication.X = 493f;
						this.shipIndication.Y = 158f;
						break;
					}
					case 15:
					{
						this.shipIndication.X = 762f;
						this.shipIndication.Y = 105f;
						break;
					}
				}
			}
		}
	}

	private void SetAccessAndRecomendation(LevelMap map)
	{
		if (!map.isPveMap)
		{
			this.lbl_MapName.text = string.Concat(StaticData.Translate(map.nameUI), string.Format(" ({0})", StaticData.Translate("key_universe_map_pvp_lbl")));
			this.lbl_RecomendedLevel.text = string.Format(StaticData.Translate("key_universe_map_level_requirements"), map.reqMinLevel, map.reqMaxLevel);
			if (NetworkScript.player.playerBelongings.playerAccessLevel >= map.accessLevel)
			{
				this.lbl_AccessLevel.TextColor = GuiNewStyleBar.blueColor;
			}
			else
			{
				this.lbl_AccessLevel.TextColor = GuiNewStyleBar.redColor;
			}
			if (NetworkScript.player.playerBelongings.playerLevel > map.reqMaxLevel || NetworkScript.player.playerBelongings.playerLevel < map.reqMinLevel)
			{
				this.lbl_RecomendedLevel.TextColor = GuiNewStyleBar.redColor;
			}
			else
			{
				this.lbl_RecomendedLevel.TextColor = GuiNewStyleBar.blueColor;
			}
		}
		else if (NetworkScript.player.playerBelongings.playerAccessLevel >= map.accessLevel)
		{
			this.lbl_AccessLevel.TextColor = GuiNewStyleBar.blueColor;
			this.lbl_RecomendedLevel.TextColor = GuiNewStyleBar.orangeColor;
		}
		else
		{
			this.lbl_AccessLevel.TextColor = GuiNewStyleBar.redColor;
			this.lbl_RecomendedLevel.TextColor = GuiNewStyleBar.orangeColor;
		}
	}
}