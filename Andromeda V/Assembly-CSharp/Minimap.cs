using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Minimap : MonoBehaviour
{
	public static GuiWindow wnd;

	private static LevelMap galaxy;

	public static GuiTexture map;

	private static GuiLabel lblCoordinates;

	private static GuiLabel lblName;

	private static float scaleX;

	private static float scaleY;

	private static GuiButton button;

	public static bool targetSelected;

	public static float targetX;

	public static float targetY;

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

	private static Texture2D powerUpShopTexture;

	private static Texture2D miningStationYour;

	private static Texture2D miningStationEnemy;

	private static Texture2D miningStationDefault;

	private static float epIconWidth;

	private static float miningStationIconWidth;

	private static GuiButtonFixed btnGoHome;

	private static GuiButtonFixed btnToogleMiniMap;

	private static GuiButtonFixed btnSocialInteraction;

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

	public static List<Minimap.Destination> targets;

	public static List<Minimap.Destination> npcs;

	private static List<Vector2> targetsXY;

	public static DateTime firstFrameTime;

	private static float rotationTime;

	public static bool isRadarOn;

	public static bool isQuestPointerOn;

	static Minimap()
	{
		Minimap.wnd = null;
		Minimap.galaxy = null;
		Minimap.map = null;
		Minimap.targetSelected = false;
		Minimap.epIconWidth = 26f;
		Minimap.miningStationIconWidth = 30f;
		Minimap.blinkAlpha = 1f;
		Minimap.blinkAlphaDirection = false;
		Minimap.mineralsAlpha = new float[255];
		Minimap.mineralsAlphaDirection = new bool[255];
		Minimap.isInStealth = false;
		Minimap.baseColor = new Color(1f, 1f, 1f, 1f);
		Minimap.blinkingColor = new Color(1f, 1f, 1f, 1f);
		Minimap.galaxyHalfWidth = 0f;
		Minimap.galaxyHalfHeight = 0f;
		Minimap.targets = new List<Minimap.Destination>();
		Minimap.npcs = new List<Minimap.Destination>();
		Minimap.targetsXY = new List<Vector2>();
		Minimap.rotationTime = 0.7f;
		Minimap.isRadarOn = true;
		Minimap.isQuestPointerOn = true;
	}

	public Minimap()
	{
	}

	public static void AddQuestObjectivePointer(Minimap.Destination d)
	{
		int num = 0;
		int num1 = 0;
		Minimap.targets.Add(d);
		if (Minimap.CalculatePointCoordinates(d, out num, out num1))
		{
			Minimap.targetsXY.Add(new Vector2((float)num, (float)num1));
		}
	}

	private static bool CalculatePointCoordinates(Minimap.Destination d, out int dx, out int dy)
	{
		Minimap.<CalculatePointCoordinates>c__AnonStorey3A variable = null;
		dx = 0;
		dy = 0;
		if (d.galaxyId >= 3000 && Minimap.galaxy.__galaxyId != d.galaxyId)
		{
			return false;
		}
		if (Minimap.galaxy.IsUltraGalaxyInstance() || Minimap.galaxy.__galaxyId >= 2000 && Minimap.galaxy.__galaxyId < 3000 || d.onlyGalaxy && d.galaxyId == Minimap.galaxy.get_galaxyId())
		{
			return false;
		}
		if (d.galaxyId != Minimap.galaxy.get_galaxyId())
		{
			short num = (short)d.galaxyId;
			HyperJumpNet hyperJumpNet = null;
			if (d.galaxyId < 2000)
			{
				LevelMap[] levelMapArray = StaticData.allGalaxies;
				if (Minimap.<>f__am$cache33 == null)
				{
					Minimap.<>f__am$cache33 = new Func<LevelMap, bool>(null, (LevelMap t) => (!t.isPveMap ? true : (!t.isPveMap ? false : t.fraction == NetworkScript.player.vessel.fractionId)));
				}
				IEnumerable<LevelMap> enumerable = Enumerable.Where<LevelMap>(levelMapArray, Minimap.<>f__am$cache33);
				if (Minimap.<>f__am$cache34 == null)
				{
					Minimap.<>f__am$cache34 = new Func<LevelMap, short>(null, (LevelMap s) => s.get_galaxyId());
				}
				Enumerable.ToList<short>(Enumerable.Select<LevelMap, short>(enumerable, Minimap.<>f__am$cache34));
				List<HyperJumpNet> list = new List<HyperJumpNet>();
				IEnumerator<LevelMap> enumerator = Enumerable.Where<LevelMap>(StaticData.allGalaxies, new Func<LevelMap, bool>(variable, (LevelMap t) => this.allGalaxiesWithAccess.Contains(t.get_galaxyId()))).GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						LevelMap current = enumerator.get_Current();
						List<HyperJumpNet> list1 = list;
						HyperJumpNet[] hyperJumpNetArray = current.hyperJumps;
						if (Minimap.<>f__am$cache35 == null)
						{
							Minimap.<>f__am$cache35 = new Func<HyperJumpNet, bool>(null, (HyperJumpNet t) => t.fractionId == NetworkScript.player.vessel.fractionId);
						}
						list1.AddRange(Enumerable.Where<HyperJumpNet>(hyperJumpNetArray, Minimap.<>f__am$cache35));
					}
				}
				finally
				{
					if (enumerator == null)
					{
					}
					enumerator.Dispose();
				}
				list = Enumerable.ToList<HyperJumpNet>(Enumerable.Where<HyperJumpNet>(list, new Func<HyperJumpNet, bool>(variable, (HyperJumpNet t) => t.galaxyDst == this.<>f__ref$59.d.galaxyId)));
				List<HyperJumpNet> list2 = list;
				if (Minimap.<>f__am$cache36 == null)
				{
					Minimap.<>f__am$cache36 = new Func<HyperJumpNet, bool>(null, (HyperJumpNet t) => t.galaxySrc == Minimap.galaxy.get_galaxyId());
				}
				hyperJumpNet = Enumerable.FirstOrDefault<HyperJumpNet>(Enumerable.Where<HyperJumpNet>(list2, Minimap.<>f__am$cache36));
				if (hyperJumpNet == null)
				{
					if (Minimap.galaxy.get_galaxyId() > 2999)
					{
						return false;
					}
					num = (short)Enumerable.First<HyperJumpNet>(list).galaxySrc;
				}
				else
				{
					dx = Mathf.FloorToInt(Minimap.scaleX * (hyperJumpNet.x + Minimap.galaxyHalfWidth));
					dy = Mathf.FloorToInt(Minimap.scaleY * (hyperJumpNet.z + Minimap.galaxyHalfHeight));
				}
			}
			if (num < 1000)
			{
				if (Minimap.galaxy.get_galaxyId() >= num)
				{
					int _galaxyId = 1000;
					HyperJumpNet[] hyperJumpNetArray1 = Minimap.galaxy.hyperJumps;
					if (Minimap.<>f__am$cache38 == null)
					{
						Minimap.<>f__am$cache38 = new Func<HyperJumpNet, bool>(null, (HyperJumpNet t) => t.fractionId == NetworkScript.player.vessel.fractionId);
					}
					IEnumerator<HyperJumpNet> enumerator1 = Enumerable.Where<HyperJumpNet>(hyperJumpNetArray1, Minimap.<>f__am$cache38).GetEnumerator();
					try
					{
						while (enumerator1.MoveNext())
						{
							HyperJumpNet current1 = enumerator1.get_Current();
							if (current1.galaxyDst != d.galaxyId)
							{
								if (current1.galaxyDst >= Minimap.galaxy.get_galaxyId() || current1.galaxyDst - Minimap.galaxy.get_galaxyId() >= _galaxyId)
								{
									continue;
								}
								hyperJumpNet = current1;
								_galaxyId = current1.galaxyDst - Minimap.galaxy.get_galaxyId();
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
					dx = Mathf.FloorToInt(Minimap.scaleX * (hyperJumpNet.x + Minimap.galaxyHalfWidth));
					dy = Mathf.FloorToInt(Minimap.scaleY * (hyperJumpNet.z + Minimap.galaxyHalfHeight));
				}
				else
				{
					int _galaxyId1 = 1000;
					HyperJumpNet[] hyperJumpNetArray2 = Minimap.galaxy.hyperJumps;
					if (Minimap.<>f__am$cache37 == null)
					{
						Minimap.<>f__am$cache37 = new Func<HyperJumpNet, bool>(null, (HyperJumpNet t) => t.fractionId == NetworkScript.player.vessel.fractionId);
					}
					IEnumerator<HyperJumpNet> enumerator2 = Enumerable.Where<HyperJumpNet>(hyperJumpNetArray2, Minimap.<>f__am$cache37).GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							HyperJumpNet hyperJumpNet1 = enumerator2.get_Current();
							if (hyperJumpNet1.galaxyDst != num)
							{
								if (hyperJumpNet1.galaxyDst <= Minimap.galaxy.get_galaxyId() || hyperJumpNet1.galaxyDst - Minimap.galaxy.get_galaxyId() >= _galaxyId1)
								{
									continue;
								}
								hyperJumpNet = hyperJumpNet1;
								_galaxyId1 = hyperJumpNet1.galaxyDst - Minimap.galaxy.get_galaxyId();
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
					dx = Mathf.FloorToInt(Minimap.scaleX * (hyperJumpNet.x + Minimap.galaxyHalfWidth));
					dy = Mathf.FloorToInt(Minimap.scaleY * (hyperJumpNet.z + Minimap.galaxyHalfHeight));
				}
			}
		}
		else
		{
			dx = Mathf.FloorToInt(Minimap.scaleX * (d.x + Minimap.galaxyHalfWidth));
			dy = Mathf.FloorToInt(Minimap.scaleY * (d.z + Minimap.galaxyHalfHeight));
		}
		return true;
	}

	public static void ClearObjectivePointer()
	{
		Minimap.targets.Clear();
		Minimap.targetsXY.Clear();
	}

	public static void Create()
	{
		Minimap.wnd = new GuiWindow();
		Minimap.wnd.SetBackgroundTexture("MinimapWindow", "MinimapBackground");
		Minimap.wnd.boundries.set_x(0f);
		Minimap.wnd.boundries.set_y((float)(Screen.get_height() - 174));
		Minimap.wnd.isHidden = false;
		Minimap.wnd.zOrder = 200;
		Minimap.wnd.secondaryDrawHandler = new Action(null, Minimap.OnDraw);
		AndromedaGui.gui.AddWindow(Minimap.wnd);
		Minimap.galaxy = NetworkScript.player.vessel.galaxy;
		Minimap.galaxyHalfWidth = (float)Minimap.galaxy.width * 0.5f;
		Minimap.galaxyHalfHeight = (float)Minimap.galaxy.height * 0.5f;
		Minimap.map = new GuiTexture();
		if (Minimap.galaxy.get_galaxyId() == 1000)
		{
			Minimap.map.SetTextureKeepSize("TutorialWindow", "Tutorial");
		}
		else if (!Minimap.isRadarOn)
		{
			Minimap.map.SetTexture("MiniMaps", Minimap.galaxy.minimapAssetName);
		}
		else
		{
			Minimap.map.SetTexture("MiniMaps", "CleanMinimap");
		}
		Minimap.map.X = 1f;
		Minimap.map.Y = 34f;
		Minimap.map.boundries.set_width(200f);
		Minimap.map.boundries.set_height(133f);
		Minimap.button = new GuiButton()
		{
			boundries = new Rect(Minimap.map.boundries),
			Clicked = new Action<EventHandlerParam>(null, Minimap.MinimapClicked),
			Caption = string.Empty
		};
		Minimap.wnd.AddGuiElement(Minimap.button);
		Minimap.scaleX = Minimap.map.boundries.get_width() / (float)Minimap.galaxy.width;
		Minimap.scaleY = Minimap.map.boundries.get_height() / (float)Minimap.galaxy.height;
		Minimap.lblName = new GuiLabel()
		{
			boundries = new Rect(3f, 8f, 140f, 15f),
			text = StaticData.Translate(Minimap.galaxy.nameUI),
			FontSize = 15,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 3
		};
		Minimap.wnd.AddGuiElement(Minimap.lblName);
		Minimap.lblCoordinates = new GuiLabel()
		{
			boundries = new Rect(145f, 8f, 60f, 15f),
			text = string.Empty,
			FontSize = 13,
			TextColor = Color.get_white(),
			Alignment = 3
		};
		Minimap.wnd.AddGuiElement(Minimap.lblCoordinates);
		float single = 209f;
		float single1 = 60f;
		Minimap.btnToogleQuestPointers = new GuiButtonFixed();
		Minimap.btnToogleQuestPointers.SetTexture("MinimapWindow", "map_Target");
		Minimap.btnToogleQuestPointers.SetTextureNormal("MinimapWindow", "map_TargetClk");
		Minimap.btnToogleQuestPointers.SetTextureClicked("MinimapWindow", "map_TargetClk");
		Minimap.btnToogleQuestPointers.Caption = string.Empty;
		Minimap.btnToogleQuestPointers.X = single;
		Minimap.btnToogleQuestPointers.Y = single1;
		Minimap.btnToogleQuestPointers.Clicked = new Action<EventHandlerParam>(null, Minimap.OnToogleQuestPointerClick);
		GuiButtonFixed guiButtonFixed = Minimap.btnToogleQuestPointers;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_minimap_btn_toogle_quest_pointer"),
			customData2 = Minimap.btnToogleQuestPointers
		};
		guiButtonFixed.tooltipWindowParam = eventHandlerParam;
		Minimap.btnToogleQuestPointers.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		Minimap.wnd.AddGuiElement(Minimap.btnToogleQuestPointers);
		Minimap.btnSocialInteraction = new GuiButtonFixed();
		Minimap.btnSocialInteraction.SetTexture("MinimapWindow", "map_Social");
		Minimap.btnSocialInteraction.Caption = string.Empty;
		Minimap.btnSocialInteraction.X = single;
		Minimap.btnSocialInteraction.Y = single1 + 29f;
		Minimap.btnSocialInteraction.Clicked = new Action<EventHandlerParam>(null, Minimap.OnSocialInteractionClick);
		GuiButtonFixed guiButtonFixed1 = Minimap.btnSocialInteraction;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_minimap_btn_social_interaction"),
			customData2 = Minimap.btnSocialInteraction
		};
		guiButtonFixed1.tooltipWindowParam = eventHandlerParam;
		Minimap.btnSocialInteraction.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		Minimap.btnSocialInteraction.isEnabled = NetworkScript.player.vessel.galaxy.get_galaxyId() != 1000;
		Minimap.wnd.AddGuiElement(Minimap.btnSocialInteraction);
		Minimap.btnToogleMiniMap = new GuiButtonFixed();
		Minimap.btnToogleMiniMap.SetTexture("MinimapWindow", "map_Satellite");
		Minimap.btnToogleMiniMap.Caption = string.Empty;
		Minimap.btnToogleMiniMap.X = single;
		Minimap.btnToogleMiniMap.Y = single1 + 58f;
		Minimap.btnToogleMiniMap.Clicked = new Action<EventHandlerParam>(null, Minimap.OnToogleMiniMapClick);
		GuiButtonFixed guiButtonFixed2 = Minimap.btnToogleMiniMap;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_minimap_btn_toogle_radar"),
			customData2 = Minimap.btnToogleMiniMap
		};
		guiButtonFixed2.tooltipWindowParam = eventHandlerParam;
		Minimap.btnToogleMiniMap.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		Minimap.wnd.AddGuiElement(Minimap.btnToogleMiniMap);
		if (Minimap.galaxy.get_galaxyId() >= 1000 && Minimap.galaxy.get_galaxyId() <= 2999)
		{
			Minimap.btnToogleMiniMap.isEnabled = false;
			if (Minimap.galaxy.get_galaxyId() != 1000)
			{
				Minimap.map.SetTextureKeepSize("MiniMaps", Minimap.galaxy.minimapAssetName);
			}
			else
			{
				Minimap.map.SetTextureKeepSize("TutorialWindow", "Tutorial");
			}
		}
		Minimap.btnGoHome = new GuiButtonFixed();
		Minimap.btnGoHome.SetTexture("MinimapWindow", "map_home");
		Minimap.btnGoHome.Caption = string.Empty;
		Minimap.btnGoHome.X = single;
		Minimap.btnGoHome.Y = single1 + 87f;
		Minimap.btnGoHome.Clicked = new Action<EventHandlerParam>(null, Minimap.OnAutoPilotClick);
		GuiButtonFixed guiButtonFixed3 = Minimap.btnGoHome;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_minimap_btn_home"),
			customData2 = Minimap.btnGoHome
		};
		guiButtonFixed3.tooltipWindowParam = eventHandlerParam;
		Minimap.btnGoHome.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		Minimap.wnd.AddGuiElement(Minimap.btnGoHome);
		Minimap.GenerateTextures();
	}

	private static void DrawDynamic(int x, int y)
	{
		DateTime now = DateTime.get_Now();
		TimeSpan timeSpan = now - Minimap.firstFrameTime;
		int totalSeconds = (int)(timeSpan.get_TotalSeconds() / (double)Minimap.rotationTime);
		Minimap.firstFrameTime = Minimap.firstFrameTime.AddSeconds((double)((float)totalSeconds * Minimap.rotationTime));
		TimeSpan timeSpan1 = now - Minimap.firstFrameTime;
		int num = (int)(timeSpan1.get_TotalSeconds() / (double)Minimap.rotationTime * (double)((int)Minimap.targetTextures.Length));
		GUI.DrawTexture(new Rect(Minimap.map.boundries.get_x() + (float)(x - Minimap.targetTextures[0].get_width() / 2), Minimap.map.boundries.get_y() + (Minimap.map.boundries.get_height() - (float)(y + Minimap.targetTextures[0].get_height() / 2)), (float)Minimap.targetTextures[0].get_width(), (float)Minimap.targetTextures[0].get_height()), Minimap.targetTextures[num]);
	}

	private static void DrawLine(int x0, int x1, int y0, int y1, Minimap.MinimapColor color, float alpha, float width)
	{
		GUI.set_color(new Color(1f, 1f, 1f, alpha));
		GUI.set_matrix(Matrix4x4.get_identity());
		Vector2 vector2 = new Vector2(Minimap.map.boundries.get_x() + (float)x0, Minimap.map.boundries.get_y() + (Minimap.map.boundries.get_height() - (float)y0));
		Vector2 vector21 = new Vector2(Minimap.map.boundries.get_x() + (float)x1, Minimap.map.boundries.get_y() + (Minimap.map.boundries.get_height() - (float)y1));
		float single = Vector3.Angle(vector21 - vector2, Vector2.get_right());
		if (vector2.y > vector21.y)
		{
			single = -single;
		}
		GUIUtility.RotateAroundPivot(single, vector2);
		float single1 = vector2.x;
		float single2 = vector2.y;
		Vector2 vector22 = vector21 - vector2;
		GUI.DrawTexture(new Rect(single1, single2, vector22.get_magnitude(), 1f), Minimap.textures[(int)color]);
		GUI.set_matrix(Matrix4x4.get_identity());
	}

	private static void DrawRotatedTexture(int x, int y, int size, Texture2D texture, float alpha, float angle)
	{
		GUI.set_matrix(Matrix4x4.get_identity());
		GUIUtility.RotateAroundPivot(angle, new Vector2(Minimap.map.boundries.get_x() + (float)x, Minimap.map.boundries.get_y() + (Minimap.map.boundries.get_height() - (float)y)));
		Minimap.DrawTexture(x, y, size, texture, alpha);
		GUI.set_matrix(Matrix4x4.get_identity());
	}

	private static void DrawSquare(int x, int y, int size, Minimap.MinimapColor color, float alpha)
	{
		Minimap.DrawTexture(x, y, size, Minimap.textures[(int)color], alpha);
	}

	private static void DrawTexture(int x, int y, int size, Texture2D texture, float alpha)
	{
		GUI.set_color(new Color(1f, 1f, 1f, alpha));
		float single = (float)size * 0.5f;
		GUI.DrawTexture(new Rect(Minimap.map.boundries.get_x() + ((float)x - single), Minimap.map.boundries.get_y() + (Minimap.map.boundries.get_height() - ((float)y + single)), (float)size, (float)size), texture, 0, true);
	}

	public static void GenerateTextures()
	{
		Minimap.textures = new Texture2D[] { new Texture2D(1, 1), default(Texture2D), default(Texture2D), default(Texture2D), default(Texture2D), default(Texture2D), default(Texture2D), default(Texture2D) };
		Minimap.textures[0].SetPixel(0, 0, Color.get_red());
		Minimap.textures[0].Apply();
		Minimap.textures[1] = new Texture2D(1, 1);
		Minimap.textures[1].SetPixel(0, 0, Color.get_green());
		Minimap.textures[1].Apply();
		Minimap.textures[2] = new Texture2D(1, 1);
		Minimap.textures[2].SetPixel(0, 0, Color.get_yellow());
		Minimap.textures[2].Apply();
		Minimap.textures[3] = new Texture2D(1, 1);
		Minimap.textures[3].SetPixel(0, 0, Color.get_white());
		Minimap.textures[3].Apply();
		Minimap.textures[4] = new Texture2D(1, 1);
		Minimap.textures[4].SetPixel(0, 0, Color.get_magenta());
		Minimap.textures[4].Apply();
		Minimap.textures[5] = new Texture2D(1, 1);
		Minimap.textures[5].SetPixel(0, 0, GuiNewStyleBar.blueColor);
		Minimap.textures[5].Apply();
		Minimap.textures[6] = new Texture2D(1, 1);
		Minimap.textures[6].SetPixel(0, 0, new Color(1f, 0.6901f, 0f));
		Minimap.textures[6].Apply();
		Minimap.textures[7] = new Texture2D(1, 1);
		Minimap.textures[7].SetPixel(0, 0, GuiNewStyleBar.cyanColor);
		Minimap.textures[7].Apply();
		Minimap.shipTexture = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "minimap_ship");
		Minimap.starBaseTexture = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "starbase_icon");
		Minimap.starBaseDeniedTexture = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "starbase_icon_denied");
		Minimap.objectivePointerTexture = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "minimap_quest_pointer");
		Minimap.powerUpShopTexture = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "minimap_powerup_npc");
		Minimap.npcTexture = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "npc_icon");
		Minimap.checkpointTexture = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "checkpointIcon");
		Minimap.hyperJumpTexture = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "portal_myfraction");
		Minimap.hyperJumpInstanceTexture = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "portal_instance");
		Minimap.hyperJumpDeniedTexture = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "portal_denied");
		Minimap.fractionOnePoI = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "ep_fraction1Icon");
		Minimap.fractionTwoPoI = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "ep_fraction2Icon");
		Minimap.fractionZeroPoI = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "ep_fraction0Icon");
		Minimap.miningStationDefault = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "miningStationDefault");
		Minimap.miningStationYour = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "miningStationGreen");
		Minimap.miningStationEnemy = (Texture2D)playWebGame.assets.GetFromStaticSet("MinimapWindow", "miningStationRed");
		Minimap.targetTextures = playWebGame.assets.GetAnimationSet("mapTarget");
		for (int i = 0; i < 255; i++)
		{
			Minimap.mineralsAlpha[i] = (float)i / 255f;
			Minimap.mineralsAlphaDirection[i] = true;
		}
	}

	private static Minimap.MinimapColor GetPoPColor(PlayerObjectPhysics pop)
	{
		Minimap.<GetPoPColor>c__AnonStorey3C variable = null;
		Minimap.MinimapColor minimapColor = Minimap.MinimapColor.White;
		if (pop.get_IsPve())
		{
			minimapColor = (((PvEPhysics)pop).agressionType != 0 || ((PvEPhysics)pop).pveCommand == 1 ? Minimap.MinimapColor.Red : Minimap.MinimapColor.Orange);
		}
		else if (pop.neighbourhoodId == NetworkScript.player.vessel.neighbourhoodId)
		{
			minimapColor = Minimap.MinimapColor.White;
		}
		else if (NetworkScript.player.pvpGame != null && NetworkScript.player.pvpGame.state == 1)
		{
			minimapColor = (pop.teamNumber != NetworkScript.player.vessel.teamNumber ? Minimap.MinimapColor.Red : Minimap.MinimapColor.White);
		}
		else if (NetworkScript.party != null && Enumerable.FirstOrDefault<PartyMemberClientSide>(Enumerable.Where<PartyMemberClientSide>(NetworkScript.party.members, new Func<PartyMemberClientSide, bool>(variable, (PartyMemberClientSide t) => t.playerId == this.pop.playerId))) != null)
		{
			minimapColor = Minimap.MinimapColor.White;
		}
		else if (string.IsNullOrEmpty(pop.guildTag) || string.IsNullOrEmpty(NetworkScript.player.vessel.guildTag) || !(pop.guildTag == NetworkScript.player.vessel.guildTag))
		{
			minimapColor = (NetworkScript.player.vessel.fractionId != pop.fractionId ? Minimap.MinimapColor.Red : Minimap.MinimapColor.Green);
		}
		else
		{
			minimapColor = Minimap.MinimapColor.Cyan;
		}
		return minimapColor;
	}

	public static Rect GetWindowBoundaries()
	{
		return Minimap.map.boundries;
	}

	public static void HideWindow()
	{
		Minimap.wnd.isHidden = true;
	}

	public static void MinimapClicked(EventHandlerParam parm)
	{
		if (!NetworkScript.player.shipScript.isInControl)
		{
			return;
		}
		Vector3 _mousePosition = Input.get_mousePosition();
		float _x = _mousePosition.x - Minimap.wnd.boundries.get_x() - Minimap.map.X;
		float _height = (float)Screen.get_height() - _mousePosition.y - Minimap.wnd.boundries.get_y() - Minimap.map.Y;
		_x = _x / Minimap.scaleX - (float)Minimap.galaxy.width * 0.5f;
		_height = -(_height / Minimap.scaleY - (float)Minimap.galaxy.height * 0.5f);
		NetworkScript.player.shipScript.ManageMoveRequest(new Vector3(_x, 0f, _height), true, true);
		Object.DestroyObject(ShipScript.mapTarget);
		ShipScript.mapTarget = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("MovingTargetingPfb"));
		ShipScript.mapTarget.get_transform().set_localPosition(new Vector3(_x, 0f, _height));
		Minimap.targetSelected = true;
		Minimap.firstFrameTime = DateTime.get_Now();
		Minimap.targetX = _x;
		Minimap.targetY = _height;
	}

	public static void OnAutoPilotClick(object prm)
	{
		Vector3 vector3 = new Vector3(0f, 0f, 0f);
		float single = 0f;
		GameObjectPhysics[] array = null;
		IList<GameObjectPhysics> values = NetworkScript.player.shipScript.comm.gameObjects.get_Values();
		if (Minimap.<>f__am$cache39 == null)
		{
			Minimap.<>f__am$cache39 = new Func<GameObjectPhysics, bool>(null, (GameObjectPhysics t) => (!(t is StarBaseNet) ? false : ((StarBaseNet)t).fractionId == NetworkScript.player.vessel.fractionId));
		}
		array = Enumerable.ToArray<GameObjectPhysics>(Enumerable.Where<GameObjectPhysics>(values, Minimap.<>f__am$cache39));
		if (Enumerable.Count<GameObjectPhysics>(array) == 0)
		{
			IList<GameObjectPhysics> list = NetworkScript.player.shipScript.comm.gameObjects.get_Values();
			if (Minimap.<>f__am$cache3A == null)
			{
				Minimap.<>f__am$cache3A = new Func<GameObjectPhysics, bool>(null, (GameObjectPhysics t) => t is HyperJumpNet);
			}
			array = Enumerable.ToArray<GameObjectPhysics>(Enumerable.Where<GameObjectPhysics>(list, Minimap.<>f__am$cache3A));
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
		unsafe
		{
			Minimap.<OnDraw>c__AnonStorey39 variable = null;
			if (NetworkScript.player.state == 80)
			{
				return;
			}
			IEnumerator<PartyMemberInfo> enumerator = NetworkScript.partyMembersInfo.get_Values().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					enumerator.get_Current().alreadyDrawn = false;
				}
			}
			finally
			{
				if (enumerator == null)
				{
				}
				enumerator.Dispose();
			}
			if (!Minimap.blinkAlphaDirection)
			{
				Minimap.blinkAlpha = Minimap.blinkAlpha - 0.01f;
				if (Minimap.blinkAlpha <= 0.3f)
				{
					Minimap.blinkAlphaDirection = true;
				}
			}
			else
			{
				Minimap.blinkAlpha = Minimap.blinkAlpha + 0.01f;
				if (Minimap.blinkAlpha >= 1f)
				{
					Minimap.blinkAlphaDirection = false;
				}
			}
			float single = NetworkScript.player.vessel.x + Minimap.galaxyHalfWidth;
			float single1 = NetworkScript.player.vessel.z + Minimap.galaxyHalfHeight;
			GuiLabel guiLabel = Minimap.lblCoordinates;
			string str = Mathf.FloorToInt(single).ToString("000");
			int num = Mathf.FloorToInt((float)Minimap.galaxy.height - single1);
			guiLabel.text = string.Concat(str, " : ", num.ToString("000"));
			int num1 = Mathf.FloorToInt(Minimap.scaleX * single);
			int num2 = Mathf.FloorToInt(Minimap.scaleY * single1);
			GUI.DrawTexture(new Rect(Minimap.map.boundries.get_x(), Minimap.map.boundries.get_y(), Minimap.map.boundries.get_width(), Minimap.map.boundries.get_height()), Minimap.map.GetTexture2D(), 0, false);
			IEnumerator<GameObjectPhysics> enumerator1 = NetworkScript.player.shipScript.comm.gameObjects.get_Values().GetEnumerator();
			try
			{
				while (enumerator1.MoveNext())
				{
					GameObjectPhysics current = enumerator1.get_Current();
					if (current != null && !(current is ActiveSkillObject) && !(current is ProjectileObject) && !(current is Mineral))
					{
						Minimap.isInStealth = false;
						int num3 = Mathf.FloorToInt(Minimap.scaleX * (current.x + Minimap.galaxyHalfWidth));
						int num4 = Mathf.FloorToInt(Minimap.scaleY * (current.z + Minimap.galaxyHalfHeight));
						int num5 = 1;
						float single2 = 1f;
						Minimap.MinimapColor poPColor = Minimap.MinimapColor.White;
						if (current is PvEPhysics)
						{
							num5 = 2;
							if (((PvEPhysics)current).fractionId != 0)
							{
								poPColor = (NetworkScript.player.vessel.fractionId != ((PvEPhysics)current).fractionId ? Minimap.MinimapColor.Red : Minimap.MinimapColor.Green);
							}
							else
							{
								poPColor = (((PvEPhysics)current).agressionType != 0 || ((PvEPhysics)current).pveCommand == 1 ? Minimap.MinimapColor.Red : Minimap.MinimapColor.Orange);
							}
							if (current.galaxy.get_galaxyId() == 1000 && ((PvEPhysics)current).playerName == "key_pve_aria")
							{
								poPColor = Minimap.MinimapColor.Blue;
							}
						}
						else if (current is DefenceTurret)
						{
							num5 = 2;
							if (NetworkScript.player.pvpGame == null || NetworkScript.player.pvpGame.state != 1)
							{
								poPColor = (NetworkScript.player.vessel.fractionId != ((DefenceTurret)current).fractionId ? Minimap.MinimapColor.Red : Minimap.MinimapColor.Green);
							}
							else
							{
								poPColor = (((DefenceTurret)current).pvpTeamNumber != NetworkScript.player.vessel.teamNumber ? Minimap.MinimapColor.Red : Minimap.MinimapColor.Green);
							}
						}
						else if (current is PlayerObjectPhysics)
						{
							PlayerObjectPhysics playerObjectPhysic = (PlayerObjectPhysics)current;
							if (((PlayerObjectPhysics)current).playerId != NetworkScript.player.vessel.playerId)
							{
								num5 = 3;
								if (NetworkScript.party == null || Enumerable.FirstOrDefault<PartyMemberClientSide>(Enumerable.Where<PartyMemberClientSide>(NetworkScript.party.members, new Func<PartyMemberClientSide, bool>(variable, (PartyMemberClientSide w) => w.playerId == this.playerObjectPhysics.playerId))) == null)
								{
									poPColor = Minimap.GetPoPColor(playerObjectPhysic);
									if (playerObjectPhysic.isInStealthMode)
									{
										Minimap.isInStealth = true;
									}
								}
								else
								{
									poPColor = Minimap.MinimapColor.White;
									if (NetworkScript.partyMembersInfo.ContainsKey(playerObjectPhysic.playerId))
									{
										NetworkScript.partyMembersInfo.get_Item(playerObjectPhysic.playerId).alreadyDrawn = true;
									}
								}
							}
							else
							{
								continue;
							}
						}
						else if (current is Mineral)
						{
							num5 = 1;
							poPColor = Minimap.MinimapColor.Yellow;
							uint num6 = current.neighbourhoodId % 255;
							single2 = Minimap.mineralsAlpha[num6];
						}
						else if (current is StarBaseNet)
						{
							if (((StarBaseNet)current).fractionId != NetworkScript.player.vessel.fractionId)
							{
								GUI.DrawTexture(new Rect(Minimap.map.boundries.get_x() + (float)(num3 - 14), Minimap.map.boundries.get_y() + (Minimap.map.boundries.get_height() - (float)(num4 + 14)), 29f, 29f), Minimap.starBaseDeniedTexture);
							}
							else
							{
								GUI.DrawTexture(new Rect(Minimap.map.boundries.get_x() + (float)(num3 - 14), Minimap.map.boundries.get_y() + (Minimap.map.boundries.get_height() - (float)(num4 + 14)), 29f, 29f), Minimap.starBaseTexture);
							}
							continue;
						}
						else if (current is NpcObjectPhysics)
						{
							GUI.DrawTexture(new Rect(Minimap.map.boundries.get_x() + (float)(num3 - 8), Minimap.map.boundries.get_y() + (Minimap.map.boundries.get_height() - (float)(num4 + 8)), 17f, 17f), Minimap.npcTexture);
							if (((NpcObjectPhysics)current).isPowerUpSeller)
							{
								GUI.DrawTexture(new Rect(Minimap.map.boundries.get_x() + (float)(num3 - 9), Minimap.map.boundries.get_y() + (Minimap.map.boundries.get_height() - (float)(num4 + 9)), 19f, 19f), Minimap.powerUpShopTexture);
							}
							continue;
						}
						else if (current is CheckpointObjectPhysics)
						{
							GUI.DrawTexture(new Rect(Minimap.map.boundries.get_x() + (float)(num3 - 8), Minimap.map.boundries.get_y() + (Minimap.map.boundries.get_height() - (float)(num4 + 8)), 17f, 17f), Minimap.checkpointTexture);
							continue;
						}
						else if (current is HyperJumpNet)
						{
							if (((HyperJumpNet)current).galaxyDst >= 1000)
							{
								GUI.DrawTexture(new Rect(Minimap.map.boundries.get_x() + (float)(num3 - 8), Minimap.map.boundries.get_y() + (Minimap.map.boundries.get_height() - (float)(num4 + 8)), 17f, 17f), Minimap.hyperJumpInstanceTexture);
							}
							else if (((HyperJumpNet)current).fractionId != NetworkScript.player.vessel.fractionId)
							{
								GUI.DrawTexture(new Rect(Minimap.map.boundries.get_x() + (float)(num3 - 8), Minimap.map.boundries.get_y() + (Minimap.map.boundries.get_height() - (float)(num4 + 8)), 17f, 17f), Minimap.hyperJumpDeniedTexture);
							}
							else
							{
								GUI.DrawTexture(new Rect(Minimap.map.boundries.get_x() + (float)(num3 - 8), Minimap.map.boundries.get_y() + (Minimap.map.boundries.get_height() - (float)(num4 + 8)), 17f, 17f), Minimap.hyperJumpTexture);
							}
							continue;
						}
						else if (current is ExtractionPoint)
						{
							if (((ExtractionPoint)current).state == 1)
							{
								Minimap.blinkingColor.a = Minimap.blinkAlpha;
								GUI.set_color(Minimap.blinkingColor);
								if (((ExtractionPoint)current).ownerFraction == 1)
								{
									GUI.DrawTexture(new Rect(Minimap.map.boundries.get_x() + ((float)num3 - Minimap.epIconWidth / 2f), Minimap.map.boundries.get_y() + (Minimap.map.boundries.get_height() - ((float)num4 + Minimap.epIconWidth / 2f)), Minimap.epIconWidth, Minimap.epIconWidth), Minimap.fractionOnePoI);
								}
								else if (((ExtractionPoint)current).ownerFraction != 2)
								{
									GUI.DrawTexture(new Rect(Minimap.map.boundries.get_x() + ((float)num3 - Minimap.epIconWidth / 2f), Minimap.map.boundries.get_y() + (Minimap.map.boundries.get_height() - ((float)num4 + Minimap.epIconWidth / 2f)), Minimap.epIconWidth, Minimap.epIconWidth), Minimap.fractionZeroPoI);
								}
								else
								{
									GUI.DrawTexture(new Rect(Minimap.map.boundries.get_x() + ((float)num3 - Minimap.epIconWidth / 2f), Minimap.map.boundries.get_y() + (Minimap.map.boundries.get_height() - ((float)num4 + Minimap.epIconWidth / 2f)), Minimap.epIconWidth, Minimap.epIconWidth), Minimap.fractionTwoPoI);
								}
								GUI.set_color(Minimap.baseColor);
							}
							else if (((ExtractionPoint)current).ownerFraction == 1)
							{
								GUI.DrawTexture(new Rect(Minimap.map.boundries.get_x() + ((float)num3 - Minimap.epIconWidth / 2f), Minimap.map.boundries.get_y() + (Minimap.map.boundries.get_height() - ((float)num4 + Minimap.epIconWidth / 2f)), Minimap.epIconWidth, Minimap.epIconWidth), Minimap.fractionOnePoI);
							}
							else if (((ExtractionPoint)current).ownerFraction != 2)
							{
								GUI.DrawTexture(new Rect(Minimap.map.boundries.get_x() + ((float)num3 - Minimap.epIconWidth / 2f), Minimap.map.boundries.get_y() + (Minimap.map.boundries.get_height() - ((float)num4 + Minimap.epIconWidth / 2f)), Minimap.epIconWidth, Minimap.epIconWidth), Minimap.fractionZeroPoI);
							}
							else
							{
								GUI.DrawTexture(new Rect(Minimap.map.boundries.get_x() + ((float)num3 - Minimap.epIconWidth / 2f), Minimap.map.boundries.get_y() + (Minimap.map.boundries.get_height() - ((float)num4 + Minimap.epIconWidth / 2f)), Minimap.epIconWidth, Minimap.epIconWidth), Minimap.fractionTwoPoI);
							}
							continue;
						}
						else if (current is MiningStation)
						{
							if (((MiningStation)current).get_OwnerTeam() == 0)
							{
								GUI.DrawTexture(new Rect(Minimap.map.boundries.get_x() + ((float)num3 - Minimap.miningStationIconWidth / 2f), Minimap.map.boundries.get_y() + (Minimap.map.boundries.get_height() - ((float)num4 + Minimap.miningStationIconWidth / 2f)), Minimap.miningStationIconWidth, Minimap.miningStationIconWidth), Minimap.miningStationDefault);
							}
							else if (((MiningStation)current).get_OwnerTeam() != NetworkScript.player.vessel.teamNumber)
							{
								GUI.DrawTexture(new Rect(Minimap.map.boundries.get_x() + ((float)num3 - Minimap.miningStationIconWidth / 2f), Minimap.map.boundries.get_y() + (Minimap.map.boundries.get_height() - ((float)num4 + Minimap.miningStationIconWidth / 2f)), Minimap.miningStationIconWidth, Minimap.miningStationIconWidth), Minimap.miningStationEnemy);
							}
							else
							{
								GUI.DrawTexture(new Rect(Minimap.map.boundries.get_x() + ((float)num3 - Minimap.miningStationIconWidth / 2f), Minimap.map.boundries.get_y() + (Minimap.map.boundries.get_height() - ((float)num4 + Minimap.miningStationIconWidth / 2f)), Minimap.miningStationIconWidth, Minimap.miningStationIconWidth), Minimap.miningStationYour);
							}
							continue;
						}
						if (Minimap.isInStealth)
						{
							continue;
						}
						Minimap.DrawSquare(num3, num4, num5, poPColor, single2);
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
			IEnumerator<PartyMemberInfo> enumerator2 = NetworkScript.partyMembersInfo.get_Values().GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					PartyMemberInfo partyMemberInfo = enumerator2.get_Current();
					if (!partyMemberInfo.alreadyDrawn && !(partyMemberInfo.lastUpdateTime.AddMilliseconds(1500) < StaticData.now) && partyMemberInfo.galaxyId == Minimap.galaxy.__galaxyId)
					{
						int num7 = Mathf.FloorToInt(Minimap.scaleX * (partyMemberInfo.coordinateX + Minimap.galaxyHalfWidth));
						int num8 = Mathf.FloorToInt(Minimap.scaleY * (partyMemberInfo.coordinateZ + Minimap.galaxyHalfHeight));
						Minimap.DrawSquare(num7, num8, 3, Minimap.MinimapColor.White, 1f);
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
			if (Minimap.targetSelected)
			{
				int num9 = Mathf.FloorToInt(Minimap.scaleX * (Minimap.targetX + Minimap.galaxyHalfWidth));
				int num10 = Mathf.FloorToInt(Minimap.scaleY * (Minimap.targetY + Minimap.galaxyHalfHeight));
				Minimap.DrawDynamic(num9, num10);
				if (Mathf.Abs(num9 - num1) <= 1 && Mathf.Abs(num10 - num2) <= 1)
				{
					Minimap.targetSelected = false;
				}
			}
			if (Minimap.isQuestPointerOn)
			{
				foreach (Vector2 vector2 in Minimap.targetsXY)
				{
					Minimap.blinkingColor.a = Minimap.blinkAlpha;
					GUI.set_color(Minimap.blinkingColor);
					GUI.DrawTexture(new Rect(Minimap.map.X + (vector2.x - (float)(Minimap.objectivePointerTexture.get_width() / 2)), Minimap.map.Y + (Minimap.map.boundries.get_height() - (vector2.y + (float)(Minimap.objectivePointerTexture.get_height() / 2))), (float)Minimap.objectivePointerTexture.get_width(), (float)Minimap.objectivePointerTexture.get_height()), Minimap.objectivePointerTexture);
				}
			}
			Minimap.DrawRotatedTexture(num1, num2, 11, Minimap.shipTexture, 1f, NetworkScript.player.vessel.rotationY);
		}
	}

	private static void OnSocialInteractionClick(object prm)
	{
		MainScreenWindow mainScreenWindow = AndromedaGui.mainWnd;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = (byte)20
		};
		mainScreenWindow.OnWindowBtnClicked(eventHandlerParam);
	}

	private static void OnToogleMiniMapClick(object prm)
	{
		if (!Minimap.isRadarOn)
		{
			Minimap.isRadarOn = true;
			Minimap.map.SetTextureKeepSize("MiniMaps", "CleanMinimap");
		}
		else
		{
			Minimap.isRadarOn = false;
			Minimap.map.SetTextureKeepSize("MiniMaps", Minimap.galaxy.minimapAssetName);
		}
	}

	private static void OnToogleQuestPointerClick(object prm)
	{
		if (!Minimap.isQuestPointerOn)
		{
			Minimap.isQuestPointerOn = true;
			Minimap.btnToogleQuestPointers.SetTextureNormal("MinimapWindow", "map_TargetClk");
			Minimap.btnToogleQuestPointers.SetTextureClicked("MinimapWindow", "map_TargetClk");
		}
		else
		{
			Minimap.isQuestPointerOn = false;
			Minimap.btnToogleQuestPointers.SetTextureNormal("MinimapWindow", "map_TargetNml");
			Minimap.btnToogleQuestPointers.SetTextureClicked("MinimapWindow", "map_TargetNml");
		}
	}

	private static void OpenMainMenuWondow(object prm)
	{
		MainScreenWindow mainScreenWindow = AndromedaGui.mainWnd;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = (byte)24
		};
		mainScreenWindow.OnWindowBtnClicked(eventHandlerParam);
	}

	public static void ReorderGui()
	{
		if (Minimap.wnd != null)
		{
			Minimap.wnd.boundries.set_x(0f);
			Minimap.wnd.boundries.set_y((float)(Screen.get_height() - 174));
		}
	}

	public static void SetDirection(Vector3 rightDirection)
	{
		NetworkScript.player.shipScript.ManageMoveRequest(rightDirection, true, true);
		Object.DestroyObject(ShipScript.mapTarget);
		ShipScript.mapTarget = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("MovingTargetingPfb"));
		ShipScript.mapTarget.get_transform().set_localPosition(rightDirection);
		Minimap.targetSelected = true;
		Minimap.firstFrameTime = DateTime.get_Now();
		Minimap.targetX = rightDirection.x;
		Minimap.targetY = rightDirection.z;
	}

	public static void ShowWindow()
	{
		Minimap.wnd.isHidden = false;
	}

	public class Destination
	{
		public float x;

		public float z;

		public int galaxyId;

		public bool onlyGalaxy;

		public Destination()
		{
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
		Orange,
		Cyan
	}
}