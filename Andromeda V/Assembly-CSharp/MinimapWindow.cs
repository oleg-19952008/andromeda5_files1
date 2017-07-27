using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MinimapWindow : GuiWindow
{
	private static LevelMap galaxy;

	private static GuiTexture map;

	private static GuiLabel lblName;

	private static GuiLabel lblCoordinates;

	private static float scaleX;

	private static float scaleY;

	private static GuiButton button;

	private static Texture2D[] textures;

	private static Texture2D shipTexture;

	private static Texture2D starBaseTexture;

	private static Texture2D hyperJumpTexture;

	private static Texture2D hyperJumpInstanceTexture;

	private static Texture2D npcTexture;

	private static Texture2D checkpointTexture;

	private static Texture2D[] targetTextures;

	private static Texture2D fractionOnePoI;

	private static Texture2D fractionTwoPoI;

	private static Texture2D fractionZeroPoI;

	private static Texture2D hyperJumpDeniedTexture;

	private static Texture2D starBaseDeniedTexture;

	private static Texture2D objectivePointerTexture;

	private static float epIconWidth;

	private static float stationaryIconWidth;

	private static float starBasesIconWidth;

	private static GuiButtonFixed btnGoHome;

	private static GuiButtonFixed btnToogleMiniMap;

	private static GuiButtonFixed btnToogleQuestPointers;

	private static float blinkAlpha;

	private static bool blinkAlphaDirection;

	private static float[] mineralsAlpha;

	private static bool[] mineralsAlphaDirection;

	private static bool isInStealth;

	private static Color baseColor;

	private static Color blinkingColor;

	private static float galaxyHalfWidth;

	private static float galaxyHalfHeight;

	private static List<Vector2> targetsXY;

	private static float rotationTime;

	static MinimapWindow()
	{
		MinimapWindow.galaxy = null;
		MinimapWindow.map = null;
		MinimapWindow.epIconWidth = 50f;
		MinimapWindow.stationaryIconWidth = 32f;
		MinimapWindow.starBasesIconWidth = 60f;
		MinimapWindow.blinkAlpha = 1f;
		MinimapWindow.blinkAlphaDirection = false;
		MinimapWindow.mineralsAlpha = new float[255];
		MinimapWindow.mineralsAlphaDirection = new bool[255];
		MinimapWindow.isInStealth = false;
		MinimapWindow.baseColor = new Color(1f, 1f, 1f, 1f);
		MinimapWindow.blinkingColor = new Color(1f, 1f, 1f, 1f);
		MinimapWindow.galaxyHalfWidth = 0f;
		MinimapWindow.galaxyHalfHeight = 0f;
		MinimapWindow.targetsXY = new List<Vector2>();
		MinimapWindow.rotationTime = 0.7f;
	}

	public MinimapWindow()
	{
	}

	private static void AddQuestObjectivePointer(Minimap.Destination d)
	{
		int num = 0;
		int num1 = 0;
		if (MinimapWindow.CalculatePointCoordinates(d, out num, out num1))
		{
			MinimapWindow.targetsXY.Add(new Vector2((float)num, (float)num1));
		}
	}

	private static bool CalculatePointCoordinates(Minimap.Destination d, out int dx, out int dy)
	{
		MinimapWindow.<CalculatePointCoordinates>c__AnonStorey3E variable = null;
		dx = 0;
		dy = 0;
		if (MinimapWindow.galaxy.IsUltraGalaxyInstance() || MinimapWindow.galaxy.__galaxyId >= 2000 && MinimapWindow.galaxy.__galaxyId < 3000)
		{
			return false;
		}
		if (d.galaxyId != MinimapWindow.galaxy.get_galaxyId())
		{
			short num = (short)d.galaxyId;
			HyperJumpNet hyperJumpNet = null;
			if (d.galaxyId < 2000)
			{
				LevelMap[] levelMapArray = StaticData.allGalaxies;
				if (MinimapWindow.<>f__am$cache26 == null)
				{
					MinimapWindow.<>f__am$cache26 = new Func<LevelMap, bool>(null, (LevelMap t) => (!t.isPveMap ? true : (!t.isPveMap ? false : t.fraction == NetworkScript.player.vessel.fractionId)));
				}
				IEnumerable<LevelMap> enumerable = Enumerable.Where<LevelMap>(levelMapArray, MinimapWindow.<>f__am$cache26);
				if (MinimapWindow.<>f__am$cache27 == null)
				{
					MinimapWindow.<>f__am$cache27 = new Func<LevelMap, short>(null, (LevelMap s) => s.get_galaxyId());
				}
				Enumerable.ToList<short>(Enumerable.Select<LevelMap, short>(enumerable, MinimapWindow.<>f__am$cache27));
				List<HyperJumpNet> list = new List<HyperJumpNet>();
				IEnumerator<LevelMap> enumerator = Enumerable.Where<LevelMap>(StaticData.allGalaxies, new Func<LevelMap, bool>(variable, (LevelMap t) => this.allGalaxiesWithAccess.Contains(t.get_galaxyId()))).GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						LevelMap current = enumerator.get_Current();
						List<HyperJumpNet> list1 = list;
						HyperJumpNet[] hyperJumpNetArray = current.hyperJumps;
						if (MinimapWindow.<>f__am$cache28 == null)
						{
							MinimapWindow.<>f__am$cache28 = new Func<HyperJumpNet, bool>(null, (HyperJumpNet t) => t.fractionId == NetworkScript.player.vessel.fractionId);
						}
						list1.AddRange(Enumerable.Where<HyperJumpNet>(hyperJumpNetArray, MinimapWindow.<>f__am$cache28));
					}
				}
				finally
				{
					if (enumerator == null)
					{
					}
					enumerator.Dispose();
				}
				list = Enumerable.ToList<HyperJumpNet>(Enumerable.Where<HyperJumpNet>(list, new Func<HyperJumpNet, bool>(variable, (HyperJumpNet t) => t.galaxyDst == this.<>f__ref$63.d.galaxyId)));
				List<HyperJumpNet> list2 = list;
				if (MinimapWindow.<>f__am$cache29 == null)
				{
					MinimapWindow.<>f__am$cache29 = new Func<HyperJumpNet, bool>(null, (HyperJumpNet t) => t.galaxySrc == MinimapWindow.galaxy.get_galaxyId());
				}
				hyperJumpNet = Enumerable.FirstOrDefault<HyperJumpNet>(Enumerable.Where<HyperJumpNet>(list2, MinimapWindow.<>f__am$cache29));
				if (hyperJumpNet == null)
				{
					if (MinimapWindow.galaxy.get_galaxyId() > 2999)
					{
						return false;
					}
					num = (short)Enumerable.First<HyperJumpNet>(list).galaxySrc;
				}
				else
				{
					dx = Mathf.FloorToInt(MinimapWindow.scaleX * (hyperJumpNet.x + MinimapWindow.galaxyHalfWidth));
					dy = Mathf.FloorToInt(MinimapWindow.scaleY * (hyperJumpNet.z + MinimapWindow.galaxyHalfHeight));
				}
			}
			if (num < 1000)
			{
				if (MinimapWindow.galaxy.get_galaxyId() >= num)
				{
					int _galaxyId = 1000;
					HyperJumpNet[] hyperJumpNetArray1 = MinimapWindow.galaxy.hyperJumps;
					if (MinimapWindow.<>f__am$cache2B == null)
					{
						MinimapWindow.<>f__am$cache2B = new Func<HyperJumpNet, bool>(null, (HyperJumpNet t) => t.fractionId == NetworkScript.player.vessel.fractionId);
					}
					IEnumerator<HyperJumpNet> enumerator1 = Enumerable.Where<HyperJumpNet>(hyperJumpNetArray1, MinimapWindow.<>f__am$cache2B).GetEnumerator();
					try
					{
						while (enumerator1.MoveNext())
						{
							HyperJumpNet current1 = enumerator1.get_Current();
							if (current1.galaxyDst != d.galaxyId)
							{
								if (current1.galaxyDst >= MinimapWindow.galaxy.get_galaxyId() || current1.galaxyDst - MinimapWindow.galaxy.get_galaxyId() >= _galaxyId)
								{
									continue;
								}
								hyperJumpNet = current1;
								_galaxyId = current1.galaxyDst - MinimapWindow.galaxy.get_galaxyId();
							}
							else
							{
								hyperJumpNet = current1;
								break;
							}
						}
					}
					finally
					{
						if (enumerator1 == null)
						{
						}
						enumerator1.Dispose();
					}
					dx = Mathf.FloorToInt(MinimapWindow.scaleX * (hyperJumpNet.x + MinimapWindow.galaxyHalfWidth));
					dy = Mathf.FloorToInt(MinimapWindow.scaleY * (hyperJumpNet.z + MinimapWindow.galaxyHalfHeight));
				}
				else
				{
					int _galaxyId1 = 1000;
					HyperJumpNet[] hyperJumpNetArray2 = MinimapWindow.galaxy.hyperJumps;
					if (MinimapWindow.<>f__am$cache2A == null)
					{
						MinimapWindow.<>f__am$cache2A = new Func<HyperJumpNet, bool>(null, (HyperJumpNet t) => t.fractionId == NetworkScript.player.vessel.fractionId);
					}
					IEnumerator<HyperJumpNet> enumerator2 = Enumerable.Where<HyperJumpNet>(hyperJumpNetArray2, MinimapWindow.<>f__am$cache2A).GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							HyperJumpNet hyperJumpNet1 = enumerator2.get_Current();
							if (hyperJumpNet1.galaxyDst != num)
							{
								if (hyperJumpNet1.galaxyDst <= MinimapWindow.galaxy.get_galaxyId() || hyperJumpNet1.galaxyDst - MinimapWindow.galaxy.get_galaxyId() >= _galaxyId1)
								{
									continue;
								}
								hyperJumpNet = hyperJumpNet1;
								_galaxyId1 = hyperJumpNet1.galaxyDst - MinimapWindow.galaxy.get_galaxyId();
							}
							else
							{
								hyperJumpNet = hyperJumpNet1;
								break;
							}
						}
					}
					finally
					{
						if (enumerator2 == null)
						{
						}
						enumerator2.Dispose();
					}
					dx = Mathf.FloorToInt(MinimapWindow.scaleX * (hyperJumpNet.x + MinimapWindow.galaxyHalfWidth));
					dy = Mathf.FloorToInt(MinimapWindow.scaleY * (hyperJumpNet.z + MinimapWindow.galaxyHalfHeight));
				}
			}
		}
		else
		{
			dx = Mathf.FloorToInt(MinimapWindow.scaleX * (d.x + MinimapWindow.galaxyHalfWidth));
			dy = Mathf.FloorToInt(MinimapWindow.scaleY * (d.z + MinimapWindow.galaxyHalfHeight));
		}
		return true;
	}

	private static void ClearObjectivePointer()
	{
		MinimapWindow.targetsXY.Clear();
	}

	public override void Create()
	{
		base.SetBackgroundTexture("iPad/MinimapWindow", "background");
		base.PutToCenter();
		this.isHidden = false;
		this.zOrder = 210;
		this.secondaryDrawHandler = new Action(null, MinimapWindow.OnDraw);
		MinimapWindow.galaxy = NetworkScript.player.vessel.galaxy;
		MinimapWindow.galaxyHalfWidth = (float)MinimapWindow.galaxy.width * 0.5f;
		MinimapWindow.galaxyHalfHeight = (float)MinimapWindow.galaxy.height * 0.5f;
		MinimapWindow.map = new GuiTexture();
		if (MinimapWindow.galaxy.get_galaxyId() == 1000)
		{
			MinimapWindow.map.SetTextureKeepSize("TutorialWindow", "Tutorial");
		}
		else if (!Minimap.isRadarOn)
		{
			MinimapWindow.map.SetTexture("MiniMaps", MinimapWindow.galaxy.minimapAssetName);
		}
		else
		{
			MinimapWindow.map.SetTexture("iPad/MinimapWindow", "CleanMinimap");
		}
		MinimapWindow.map.boundries = new Rect(58f, 168f, 768f, 512f);
		base.AddGuiElement(MinimapWindow.map);
		MinimapWindow.button = new GuiButton()
		{
			boundries = new Rect(MinimapWindow.map.boundries),
			Clicked = new Action<EventHandlerParam>(this, MinimapWindow.MinimapClicked),
			Caption = string.Empty
		};
		base.AddGuiElement(MinimapWindow.button);
		MinimapWindow.scaleX = MinimapWindow.map.boundries.get_width() / (float)MinimapWindow.galaxy.width;
		MinimapWindow.scaleY = MinimapWindow.map.boundries.get_height() / (float)MinimapWindow.galaxy.height;
		MinimapWindow.lblName = new GuiLabel()
		{
			boundries = new Rect(15f, 20f, 340f, 34f),
			text = StaticData.Translate(MinimapWindow.galaxy.nameUI),
			FontSize = 26,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 3
		};
		base.AddGuiElement(MinimapWindow.lblName);
		MinimapWindow.lblCoordinates = new GuiLabel()
		{
			boundries = new Rect(595f, 20f, 340f, 34f),
			text = string.Empty,
			FontSize = 24,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 5
		};
		base.AddGuiElement(MinimapWindow.lblCoordinates);
		MinimapWindow.btnToogleQuestPointers = new GuiButtonFixed();
		MinimapWindow.btnToogleQuestPointers.SetTexture("iPad/MinimapWindow", "btn1");
		MinimapWindow.btnToogleQuestPointers.Caption = string.Empty;
		MinimapWindow.btnToogleQuestPointers.X = 883f;
		MinimapWindow.btnToogleQuestPointers.Y = 246f;
		MinimapWindow.btnToogleQuestPointers.Clicked = new Action<EventHandlerParam>(null, MinimapWindow.OnToogleQuestPointerClick);
		GuiButtonFixed guiButtonFixed = MinimapWindow.btnToogleQuestPointers;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_minimap_btn_toogle_quest_pointer"),
			customData2 = MinimapWindow.btnToogleQuestPointers
		};
		guiButtonFixed.tooltipWindowParam = eventHandlerParam;
		MinimapWindow.btnToogleQuestPointers.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(MinimapWindow.btnToogleQuestPointers);
		MinimapWindow.btnToogleMiniMap = new GuiButtonFixed();
		MinimapWindow.btnToogleMiniMap.SetTexture("iPad/MinimapWindow", "btn2");
		MinimapWindow.btnToogleMiniMap.Caption = string.Empty;
		MinimapWindow.btnToogleMiniMap.X = 883f;
		MinimapWindow.btnToogleMiniMap.Y = 376f;
		MinimapWindow.btnToogleMiniMap.Clicked = new Action<EventHandlerParam>(null, MinimapWindow.OnToogleMiniMapClick);
		GuiButtonFixed guiButtonFixed1 = MinimapWindow.btnToogleMiniMap;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_minimap_btn_toogle_radar"),
			customData2 = MinimapWindow.btnToogleMiniMap
		};
		guiButtonFixed1.tooltipWindowParam = eventHandlerParam;
		MinimapWindow.btnToogleMiniMap.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(MinimapWindow.btnToogleMiniMap);
		if (MinimapWindow.galaxy.get_galaxyId() >= 1000 && MinimapWindow.galaxy.get_galaxyId() <= 2999)
		{
			MinimapWindow.btnToogleMiniMap.isEnabled = false;
			if (MinimapWindow.galaxy.get_galaxyId() != 1000)
			{
				MinimapWindow.map.SetTextureKeepSize("MiniMaps", MinimapWindow.galaxy.minimapAssetName);
			}
			else
			{
				MinimapWindow.map.SetTextureKeepSize("TutorialWindow", "Tutorial");
			}
		}
		MinimapWindow.btnGoHome = new GuiButtonFixed();
		MinimapWindow.btnGoHome.SetTexture("iPad/MinimapWindow", "btn3");
		MinimapWindow.btnGoHome.Caption = string.Empty;
		MinimapWindow.btnGoHome.X = 883f;
		MinimapWindow.btnGoHome.Y = 516f;
		MinimapWindow.btnGoHome.Clicked = new Action<EventHandlerParam>(null, MinimapWindow.OnAutoPilotClick);
		GuiButtonFixed guiButtonFixed2 = MinimapWindow.btnGoHome;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_minimap_btn_home"),
			customData2 = MinimapWindow.btnGoHome
		};
		guiButtonFixed2.tooltipWindowParam = eventHandlerParam;
		MinimapWindow.btnGoHome.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.AddGuiElement(MinimapWindow.btnGoHome);
		MinimapWindow.GenerateTextures();
		MinimapWindow.ClearObjectivePointer();
		foreach (Minimap.Destination target in Minimap.targets)
		{
			MinimapWindow.AddQuestObjectivePointer(target);
		}
	}

	private static void DrawDynamic(int x, int y)
	{
		DateTime dateTime = Minimap.firstFrameTime;
		DateTime now = DateTime.get_Now();
		TimeSpan timeSpan = now - dateTime;
		int totalSeconds = (int)(timeSpan.get_TotalSeconds() / (double)MinimapWindow.rotationTime);
		dateTime = dateTime.AddSeconds((double)((float)totalSeconds * MinimapWindow.rotationTime));
		TimeSpan timeSpan1 = now - dateTime;
		int num = (int)(timeSpan1.get_TotalSeconds() / (double)MinimapWindow.rotationTime * (double)((int)MinimapWindow.targetTextures.Length));
		GUI.DrawTexture(new Rect(MinimapWindow.map.boundries.get_x() + (float)(x - MinimapWindow.targetTextures[0].get_width() / 2), MinimapWindow.map.boundries.get_y() + (MinimapWindow.map.boundries.get_height() - (float)(y + MinimapWindow.targetTextures[0].get_height() / 2)), (float)MinimapWindow.targetTextures[0].get_width(), (float)MinimapWindow.targetTextures[0].get_height()), MinimapWindow.targetTextures[num]);
	}

	private static void DrawRotatedTexture(int x, int y, int size, Texture2D texture, float alpha, float angle)
	{
		GUI.set_matrix(Matrix4x4.get_identity());
		GUIUtility.RotateAroundPivot(angle, new Vector2(MinimapWindow.map.boundries.get_x() + (float)x, MinimapWindow.map.boundries.get_y() + (MinimapWindow.map.boundries.get_height() - (float)y)));
		MinimapWindow.DrawTexture(x, y, size, texture, alpha);
		GUI.set_matrix(Matrix4x4.get_identity());
	}

	private static void DrawSquare(int x, int y, int size, MinimapWindow.MinimapColor color, float alpha)
	{
		MinimapWindow.DrawTexture(x, y, size, MinimapWindow.textures[(int)color], alpha);
	}

	private static void DrawTexture(int x, int y, int size, Texture2D texture, float alpha)
	{
		GUI.set_color(new Color(1f, 1f, 1f, alpha));
		float single = (float)size * 0.5f;
		GUI.DrawTexture(new Rect(MinimapWindow.map.boundries.get_x() + ((float)x - single), MinimapWindow.map.boundries.get_y() + (MinimapWindow.map.boundries.get_height() - ((float)y + single)), (float)size, (float)size), texture, 0, true);
	}

	private static void GenerateTextures()
	{
		MinimapWindow.textures = new Texture2D[] { new Texture2D(1, 1), default(Texture2D), default(Texture2D), default(Texture2D), default(Texture2D), default(Texture2D), default(Texture2D), default(Texture2D) };
		MinimapWindow.textures[0].SetPixel(0, 0, Color.get_red());
		MinimapWindow.textures[0].Apply();
		MinimapWindow.textures[1] = new Texture2D(1, 1);
		MinimapWindow.textures[1].SetPixel(0, 0, Color.get_green());
		MinimapWindow.textures[1].Apply();
		MinimapWindow.textures[2] = new Texture2D(1, 1);
		MinimapWindow.textures[2].SetPixel(0, 0, Color.get_yellow());
		MinimapWindow.textures[2].Apply();
		MinimapWindow.textures[3] = new Texture2D(1, 1);
		MinimapWindow.textures[3].SetPixel(0, 0, Color.get_white());
		MinimapWindow.textures[3].Apply();
		MinimapWindow.textures[4] = new Texture2D(1, 1);
		MinimapWindow.textures[4].SetPixel(0, 0, Color.get_magenta());
		MinimapWindow.textures[4].Apply();
		MinimapWindow.textures[5] = new Texture2D(1, 1);
		MinimapWindow.textures[5].SetPixel(0, 0, GuiNewStyleBar.blueColor);
		MinimapWindow.textures[5].Apply();
		MinimapWindow.textures[6] = new Texture2D(1, 1);
		MinimapWindow.textures[6].SetPixel(0, 0, new Color(1f, 0.6901f, 0f));
		MinimapWindow.textures[6].Apply();
		MinimapWindow.shipTexture = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "minimap_big_ship");
		MinimapWindow.starBaseTexture = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "starbase_icon");
		MinimapWindow.starBaseDeniedTexture = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "starbase_icon_denied");
		MinimapWindow.objectivePointerTexture = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "minimap_quest_pointer");
		MinimapWindow.npcTexture = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "npc_icon");
		MinimapWindow.checkpointTexture = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "checkpointIcon");
		MinimapWindow.hyperJumpTexture = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "portal_myfraction");
		MinimapWindow.hyperJumpInstanceTexture = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "portal_instance");
		MinimapWindow.hyperJumpDeniedTexture = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "portal_denied");
		MinimapWindow.fractionOnePoI = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "ep_fraction1Icon");
		MinimapWindow.fractionTwoPoI = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "ep_fraction2Icon");
		MinimapWindow.fractionZeroPoI = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "ep_fraction0Icon");
		MinimapWindow.targetTextures = playWebGame.assets.GetAnimationSet("mapTarget");
		for (int i = 0; i < 255; i++)
		{
			MinimapWindow.mineralsAlpha[i] = (float)i / 255f;
			MinimapWindow.mineralsAlphaDirection[i] = true;
		}
	}

	private void MinimapClicked(EventHandlerParam parm)
	{
		if (!NetworkScript.player.shipScript.isInControl)
		{
			return;
		}
		Vector3 _mousePosition = Input.get_mousePosition();
		float _x = _mousePosition.x - this.boundries.get_x() - MinimapWindow.map.X;
		float _height = (float)Screen.get_height() - _mousePosition.y - this.boundries.get_y() - MinimapWindow.map.Y;
		_x = _x / MinimapWindow.scaleX - (float)MinimapWindow.galaxy.width * 0.5f;
		_height = -(_height / MinimapWindow.scaleY - (float)MinimapWindow.galaxy.height * 0.5f);
		NetworkScript.player.shipScript.ManageMoveRequest(new Vector3(_x, 0f, _height), true, true);
		Object.DestroyObject(ShipScript.mapTarget);
		ShipScript.mapTarget = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("MovingTargetingPfb"));
		ShipScript.mapTarget.get_transform().set_localPosition(new Vector3(_x, 0f, _height));
		Minimap.targetSelected = true;
		Minimap.firstFrameTime = DateTime.get_Now();
		Minimap.targetX = _x;
		Minimap.targetY = _height;
	}

	private static void OnAutoPilotClick(object prm)
	{
		Vector3 vector3 = new Vector3(0f, 0f, 0f);
		float single = 0f;
		GameObjectPhysics[] array = null;
		IList<GameObjectPhysics> values = NetworkScript.player.shipScript.comm.gameObjects.get_Values();
		if (MinimapWindow.<>f__am$cache2C == null)
		{
			MinimapWindow.<>f__am$cache2C = new Func<GameObjectPhysics, bool>(null, (GameObjectPhysics t) => (!(t is StarBaseNet) ? false : ((StarBaseNet)t).fractionId == NetworkScript.player.vessel.fractionId));
		}
		array = Enumerable.ToArray<GameObjectPhysics>(Enumerable.Where<GameObjectPhysics>(values, MinimapWindow.<>f__am$cache2C));
		if (Enumerable.Count<GameObjectPhysics>(array) == 0)
		{
			IList<GameObjectPhysics> list = NetworkScript.player.shipScript.comm.gameObjects.get_Values();
			if (MinimapWindow.<>f__am$cache2D == null)
			{
				MinimapWindow.<>f__am$cache2D = new Func<GameObjectPhysics, bool>(null, (GameObjectPhysics t) => t is HyperJumpNet);
			}
			array = Enumerable.ToArray<GameObjectPhysics>(Enumerable.Where<GameObjectPhysics>(list, MinimapWindow.<>f__am$cache2D));
		}
		if (Enumerable.Count<GameObjectPhysics>(array) < 1)
		{
			Debug.Log("UPS");
			return;
		}
		single = Vector3.Distance(new Vector3(NetworkScript.player.vessel.x, NetworkScript.player.vessel.y, NetworkScript.player.vessel.z), new Vector3(array[0].x, array[0].y, array[0].z));
		vector3 = new Vector3(array[0].x, 0f, array[0].z);
		float single1 = 0f;
		GameObjectPhysics[] gameObjectPhysicsArray = array;
		for (int i = 0; i < (int)gameObjectPhysicsArray.Length; i++)
		{
			GameObjectPhysics gameObjectPhysic = gameObjectPhysicsArray[i];
			if (gameObjectPhysic is StarBaseNet)
			{
				StarBaseNet starBaseNet = (StarBaseNet)gameObjectPhysic;
				float takeZoneMaxZ = StarBaseNet.staticData[starBaseNet.starBaseType].TakeZoneMaxZ;
				float takeZoneMinZ = StarBaseNet.staticData[starBaseNet.starBaseType].TakeZoneMinZ;
				single1 = (takeZoneMaxZ + takeZoneMinZ) / 2f;
			}
			float single2 = Vector3.Distance(new Vector3(NetworkScript.player.vessel.x, NetworkScript.player.vessel.y, NetworkScript.player.vessel.z), new Vector3(gameObjectPhysic.x, gameObjectPhysic.y, gameObjectPhysic.z));
			if (single2 <= single)
			{
				single = single2;
				vector3 = new Vector3(gameObjectPhysic.x, 0f, gameObjectPhysic.z + single1);
			}
		}
		NetworkScript.player.shipScript.ManageMoveRequest(vector3, true, true);
		Object.DestroyObject(ShipScript.mapTarget);
		ShipScript.mapTarget = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("MovingTargetingPfb"));
		ShipScript.mapTarget.get_transform().set_localPosition(vector3);
		Minimap.targetSelected = true;
		Minimap.firstFrameTime = DateTime.get_Now();
		Minimap.targetX = vector3.x;
		Minimap.targetY = vector3.z;
	}

	private static void OnDraw()
	{
		MinimapWindow.<OnDraw>c__AnonStorey3D variable = null;
		if (NetworkScript.player.state == 80)
		{
			return;
		}
		if (!MinimapWindow.blinkAlphaDirection)
		{
			MinimapWindow.blinkAlpha = MinimapWindow.blinkAlpha - 0.01f;
			if (MinimapWindow.blinkAlpha <= 0.3f)
			{
				MinimapWindow.blinkAlphaDirection = true;
			}
		}
		else
		{
			MinimapWindow.blinkAlpha = MinimapWindow.blinkAlpha + 0.01f;
			if (MinimapWindow.blinkAlpha >= 1f)
			{
				MinimapWindow.blinkAlphaDirection = false;
			}
		}
		float single = NetworkScript.player.vessel.x + MinimapWindow.galaxyHalfWidth;
		float single1 = NetworkScript.player.vessel.z + MinimapWindow.galaxyHalfHeight;
		float single2 = (float)MinimapWindow.galaxy.height - single1;
		MinimapWindow.lblCoordinates.text = string.Concat(single.ToString("000"), " : ", single2.ToString("000"));
		int num = Mathf.FloorToInt(MinimapWindow.scaleX * single);
		int num1 = Mathf.FloorToInt(MinimapWindow.scaleY * single1);
		GUI.DrawTexture(new Rect(MinimapWindow.map.boundries.get_x(), MinimapWindow.map.boundries.get_y(), MinimapWindow.map.boundries.get_width(), MinimapWindow.map.boundries.get_height()), MinimapWindow.map.GetTexture2D(), 0, false);
		IEnumerator<GameObjectPhysics> enumerator = NetworkScript.player.shipScript.comm.gameObjects.get_Values().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				GameObjectPhysics current = enumerator.get_Current();
				if (current != null && !(current is ActiveSkillObject) && !(current is ProjectileObject))
				{
					MinimapWindow.isInStealth = false;
					int num2 = Mathf.FloorToInt(MinimapWindow.scaleX * (current.x + MinimapWindow.galaxyHalfWidth));
					int num3 = Mathf.FloorToInt(MinimapWindow.scaleY * (current.z + MinimapWindow.galaxyHalfHeight));
					int num4 = 1;
					float single3 = 1f;
					MinimapWindow.MinimapColor minimapColor = MinimapWindow.MinimapColor.White;
					if (current is PvEPhysics)
					{
						num4 = 4;
						if (((PvEPhysics)current).fractionId != 0)
						{
							minimapColor = (NetworkScript.player.vessel.fractionId != ((PvEPhysics)current).fractionId ? MinimapWindow.MinimapColor.Red : MinimapWindow.MinimapColor.Green);
						}
						else
						{
							minimapColor = (((PvEPhysics)current).agressionType != 1 ? MinimapWindow.MinimapColor.Orange : MinimapWindow.MinimapColor.Red);
						}
						if (current.galaxy.get_galaxyId() == 1000 && ((PvEPhysics)current).playerName == "key_pve_aria")
						{
							minimapColor = MinimapWindow.MinimapColor.Blue;
						}
					}
					else if (current is DefenceTurret)
					{
						num4 = 4;
						minimapColor = (NetworkScript.player.vessel.fractionId != ((DefenceTurret)current).fractionId ? MinimapWindow.MinimapColor.Red : MinimapWindow.MinimapColor.Green);
					}
					else if (current is PlayerObjectPhysics)
					{
						PlayerObjectPhysics playerObjectPhysic = (PlayerObjectPhysics)current;
						if (((PlayerObjectPhysics)current).playerId != NetworkScript.player.vessel.playerId)
						{
							num4 = 6;
							if (NetworkScript.party == null || Enumerable.FirstOrDefault<PartyMemberClientSide>(Enumerable.Where<PartyMemberClientSide>(NetworkScript.party.members, new Func<PartyMemberClientSide, bool>(variable, (PartyMemberClientSide w) => w.playerId == this.playerObjectPhysics.playerId))) == null)
							{
								if (NetworkScript.player.pvpGame == null || NetworkScript.player.pvpGame.state != 1)
								{
									minimapColor = (playerObjectPhysic.fractionId != NetworkScript.player.vessel.fractionId ? MinimapWindow.MinimapColor.Red : MinimapWindow.MinimapColor.Green);
								}
								else
								{
									minimapColor = (playerObjectPhysic.teamNumber != NetworkScript.player.vessel.teamNumber ? MinimapWindow.MinimapColor.Red : MinimapWindow.MinimapColor.Green);
								}
								if (playerObjectPhysic.isInStealthMode)
								{
									MinimapWindow.isInStealth = true;
								}
							}
							else
							{
								minimapColor = MinimapWindow.MinimapColor.Blue;
							}
						}
						else
						{
							continue;
						}
					}
					else if (current is Mineral)
					{
						num4 = 2;
						minimapColor = MinimapWindow.MinimapColor.Magenta;
						single3 = MinimapWindow.blinkAlpha;
					}
					else if (current is StarBaseNet)
					{
						if (((StarBaseNet)current).fractionId != NetworkScript.player.vessel.fractionId)
						{
							GUI.DrawTexture(new Rect(MinimapWindow.map.boundries.get_x() + ((float)num2 - MinimapWindow.starBasesIconWidth / 2f), MinimapWindow.map.boundries.get_y() + (MinimapWindow.map.boundries.get_height() - ((float)num3 + MinimapWindow.starBasesIconWidth / 2f)), MinimapWindow.starBasesIconWidth, MinimapWindow.starBasesIconWidth), MinimapWindow.starBaseDeniedTexture);
						}
						else
						{
							GUI.DrawTexture(new Rect(MinimapWindow.map.boundries.get_x() + ((float)num2 - MinimapWindow.starBasesIconWidth / 2f), MinimapWindow.map.boundries.get_y() + (MinimapWindow.map.boundries.get_height() - ((float)num3 + MinimapWindow.starBasesIconWidth / 2f)), MinimapWindow.starBasesIconWidth, MinimapWindow.starBasesIconWidth), MinimapWindow.starBaseTexture);
						}
						continue;
					}
					else if (current is NpcObjectPhysics)
					{
						GUI.DrawTexture(new Rect(MinimapWindow.map.boundries.get_x() + ((float)num2 - MinimapWindow.stationaryIconWidth / 2f), MinimapWindow.map.boundries.get_y() + (MinimapWindow.map.boundries.get_height() - ((float)num3 + MinimapWindow.stationaryIconWidth / 2f)), MinimapWindow.stationaryIconWidth, MinimapWindow.stationaryIconWidth), MinimapWindow.npcTexture);
						continue;
					}
					else if (current is CheckpointObjectPhysics)
					{
						GUI.DrawTexture(new Rect(MinimapWindow.map.boundries.get_x() + ((float)num2 - MinimapWindow.stationaryIconWidth / 2f), MinimapWindow.map.boundries.get_y() + (MinimapWindow.map.boundries.get_height() - ((float)num3 + MinimapWindow.stationaryIconWidth / 2f)), MinimapWindow.stationaryIconWidth, MinimapWindow.stationaryIconWidth), MinimapWindow.checkpointTexture);
						continue;
					}
					else if (current is HyperJumpNet)
					{
						if (((HyperJumpNet)current).galaxyDst >= 1000)
						{
							GUI.DrawTexture(new Rect(MinimapWindow.map.boundries.get_x() + ((float)num2 - MinimapWindow.stationaryIconWidth / 2f), MinimapWindow.map.boundries.get_y() + (MinimapWindow.map.boundries.get_height() - ((float)num3 + MinimapWindow.stationaryIconWidth / 2f)), MinimapWindow.stationaryIconWidth, MinimapWindow.stationaryIconWidth), MinimapWindow.hyperJumpInstanceTexture);
						}
						else if (((HyperJumpNet)current).fractionId != NetworkScript.player.vessel.fractionId)
						{
							GUI.DrawTexture(new Rect(MinimapWindow.map.boundries.get_x() + ((float)num2 - MinimapWindow.stationaryIconWidth / 2f), MinimapWindow.map.boundries.get_y() + (MinimapWindow.map.boundries.get_height() - ((float)num3 + MinimapWindow.stationaryIconWidth / 2f)), MinimapWindow.stationaryIconWidth, MinimapWindow.stationaryIconWidth), MinimapWindow.hyperJumpDeniedTexture);
						}
						else
						{
							GUI.DrawTexture(new Rect(MinimapWindow.map.boundries.get_x() + ((float)num2 - MinimapWindow.stationaryIconWidth / 2f), MinimapWindow.map.boundries.get_y() + (MinimapWindow.map.boundries.get_height() - ((float)num3 + MinimapWindow.stationaryIconWidth / 2f)), MinimapWindow.stationaryIconWidth, MinimapWindow.stationaryIconWidth), MinimapWindow.hyperJumpTexture);
						}
						continue;
					}
					else if (current is ExtractionPoint)
					{
						if (((ExtractionPoint)current).state == 1)
						{
							MinimapWindow.blinkingColor.a = MinimapWindow.blinkAlpha;
							GUI.set_color(MinimapWindow.blinkingColor);
							if (((ExtractionPoint)current).ownerFraction == 1)
							{
								GUI.DrawTexture(new Rect(MinimapWindow.map.boundries.get_x() + ((float)num2 - MinimapWindow.epIconWidth / 2f), MinimapWindow.map.boundries.get_y() + (MinimapWindow.map.boundries.get_height() - ((float)num3 + MinimapWindow.epIconWidth / 2f)), MinimapWindow.epIconWidth, MinimapWindow.epIconWidth), MinimapWindow.fractionOnePoI);
							}
							else if (((ExtractionPoint)current).ownerFraction != 2)
							{
								GUI.DrawTexture(new Rect(MinimapWindow.map.boundries.get_x() + ((float)num2 - MinimapWindow.epIconWidth / 2f), MinimapWindow.map.boundries.get_y() + (MinimapWindow.map.boundries.get_height() - ((float)num3 + MinimapWindow.epIconWidth / 2f)), MinimapWindow.epIconWidth, MinimapWindow.epIconWidth), MinimapWindow.fractionZeroPoI);
							}
							else
							{
								GUI.DrawTexture(new Rect(MinimapWindow.map.boundries.get_x() + ((float)num2 - MinimapWindow.epIconWidth / 2f), MinimapWindow.map.boundries.get_y() + (MinimapWindow.map.boundries.get_height() - ((float)num3 + MinimapWindow.epIconWidth / 2f)), MinimapWindow.epIconWidth, MinimapWindow.epIconWidth), MinimapWindow.fractionTwoPoI);
							}
							GUI.set_color(MinimapWindow.baseColor);
						}
						else if (((ExtractionPoint)current).ownerFraction == 1)
						{
							GUI.DrawTexture(new Rect(MinimapWindow.map.boundries.get_x() + ((float)num2 - MinimapWindow.epIconWidth / 2f), MinimapWindow.map.boundries.get_y() + (MinimapWindow.map.boundries.get_height() - ((float)num3 + MinimapWindow.epIconWidth / 2f)), MinimapWindow.epIconWidth, MinimapWindow.epIconWidth), MinimapWindow.fractionOnePoI);
						}
						else if (((ExtractionPoint)current).ownerFraction != 2)
						{
							GUI.DrawTexture(new Rect(MinimapWindow.map.boundries.get_x() + ((float)num2 - MinimapWindow.epIconWidth / 2f), MinimapWindow.map.boundries.get_y() + (MinimapWindow.map.boundries.get_height() - ((float)num3 + MinimapWindow.epIconWidth / 2f)), MinimapWindow.epIconWidth, MinimapWindow.epIconWidth), MinimapWindow.fractionZeroPoI);
						}
						else
						{
							GUI.DrawTexture(new Rect(MinimapWindow.map.boundries.get_x() + ((float)num2 - MinimapWindow.epIconWidth / 2f), MinimapWindow.map.boundries.get_y() + (MinimapWindow.map.boundries.get_height() - ((float)num3 + MinimapWindow.epIconWidth / 2f)), MinimapWindow.epIconWidth, MinimapWindow.epIconWidth), MinimapWindow.fractionTwoPoI);
						}
						continue;
					}
					if (MinimapWindow.isInStealth)
					{
						continue;
					}
					MinimapWindow.DrawSquare(num2, num3, num4, minimapColor, single3);
				}
			}
		}
		finally
		{
			if (enumerator == null)
			{
			}
			enumerator.Dispose();
		}
		if (Minimap.targetSelected)
		{
			int num5 = Mathf.FloorToInt(MinimapWindow.scaleX * (Minimap.targetX + MinimapWindow.galaxyHalfWidth));
			int num6 = Mathf.FloorToInt(MinimapWindow.scaleY * (Minimap.targetY + MinimapWindow.galaxyHalfHeight));
			MinimapWindow.DrawDynamic(num5, num6);
			if (Mathf.Abs(num5 - num) <= 1 && Mathf.Abs(num6 - num1) <= 1)
			{
				Minimap.targetSelected = false;
			}
		}
		if (Minimap.isQuestPointerOn)
		{
			foreach (Vector2 vector2 in MinimapWindow.targetsXY)
			{
				MinimapWindow.blinkingColor.a = MinimapWindow.blinkAlpha;
				GUI.set_color(MinimapWindow.blinkingColor);
				GUI.DrawTexture(new Rect(MinimapWindow.map.X + (vector2.x - (float)(MinimapWindow.objectivePointerTexture.get_width() / 2)), MinimapWindow.map.Y + (MinimapWindow.map.boundries.get_height() - (vector2.y + (float)(MinimapWindow.objectivePointerTexture.get_height() / 2))), (float)MinimapWindow.objectivePointerTexture.get_width(), (float)MinimapWindow.objectivePointerTexture.get_height()), MinimapWindow.objectivePointerTexture);
			}
		}
		MinimapWindow.DrawRotatedTexture(num, num1, 32, MinimapWindow.shipTexture, 1f, NetworkScript.player.vessel.rotationY);
	}

	private static void OnToogleMiniMapClick(object prm)
	{
		if (!Minimap.isRadarOn)
		{
			Minimap.isRadarOn = true;
			MinimapWindow.map.SetTextureKeepSize("iPad/MinimapWindow", "CleanMinimap");
			Minimap.map.SetTextureKeepSize("MiniMaps", "CleanMinimap");
		}
		else
		{
			Minimap.isRadarOn = false;
			MinimapWindow.map.SetTextureKeepSize("MiniMaps", MinimapWindow.galaxy.minimapAssetName);
			Minimap.map.SetTextureKeepSize("MiniMaps", MinimapWindow.galaxy.minimapAssetName);
		}
	}

	private static void OnToogleQuestPointerClick(object prm)
	{
		if (!Minimap.isQuestPointerOn)
		{
			Minimap.isQuestPointerOn = true;
		}
		else
		{
			Minimap.isQuestPointerOn = false;
		}
	}

	private enum MinimapColor
	{
		Red,
		Green,
		Yellow,
		White,
		Magenta,
		Blue,
		Orange
	}
}