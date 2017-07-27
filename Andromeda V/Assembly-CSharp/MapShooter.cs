using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MapShooter : MonoBehaviour
{
	private float delay = 5f;

	private GuiWindow wnd;

	private GuiButtonResizeable btn;

	private GuiButtonResizeable btnCheckCollisions;

	private GuiButtonResizeable btnShowHideMeshes;

	private bool isMeshOnScreen = true;

	private GameObject[] collideObjects;

	private bool isCollisionCubeOnScreen;

	private List<GameObject> collisionCubes = new List<GameObject>();

	private Action doMe;

	private Object[] scnanedObjects;

	private Vector3 pos;

	private BitArray[] result;

	private float coordinateXMin;

	private float coordinateXMax;

	private float coordinateZMin;

	private float coordinateZMax;

	private float scanStep = 0.4f;

	private int currentX;

	private int currentZ;

	private int rows;

	private int columns;

	private LevelMap galaxy;

	private bool isDone;

	private Texture2D previewBitmap;

	private Color colorBkg;

	private Color colorColl;

	private Color colorCollMap;

	private bool previewDone;

	private PlayerObjectPhysics pp;

	private int j;

	private Texture2D trackShipBitmap;

	private long cc;

	public MapShooter()
	{
	}

	private GameObject CreateCube(float x, float z)
	{
		GameObject gameObject = GameObject.CreatePrimitive(3);
		gameObject.set_name("collisionCube");
		gameObject.get_transform().set_position(new Vector3(x, NetworkScript.player.vessel.y, z));
		gameObject.get_transform().set_rotation(Quaternion.get_identity());
		gameObject.get_transform().set_localScale(new Vector3(0.2f, 0.2f, 0.2f));
		return gameObject;
	}

	private void DoCollMapPreview1()
	{
		for (int i = 0; i < this.galaxy.height; i++)
		{
			this.previewBitmap.SetPixel(this.j, i, this.colorBkg);
		}
		MapShooter mapShooter = this;
		mapShooter.j = mapShooter.j + 1;
		if (this.j < this.galaxy.width)
		{
			return;
		}
		this.j = 0;
		this.doMe = new Action(this, MapShooter.DoDrawCollisions);
	}

	private void DoDrawCollisions()
	{
		for (int i = 0; i < this.galaxy.height; i++)
		{
			this.pp.x = this.coordinateXMin + (float)this.j;
			this.pp.z = this.coordinateZMin + (float)i;
			if (!this.pp.CheckCollisionsMap())
			{
				this.previewBitmap.SetPixel(this.j, i, this.colorColl);
			}
		}
		MapShooter mapShooter = this;
		mapShooter.j = mapShooter.j + 1;
		if (this.j < this.galaxy.width)
		{
			return;
		}
		this.previewBitmap.Apply();
		this.trackShipBitmap = new Texture2D(3, 3);
		this.trackShipBitmap.SetPixel(0, 0, new Color(1f, 0f, 0f, 1f));
		this.trackShipBitmap.SetPixel(0, 1, new Color(1f, 0f, 0f, 1f));
		this.trackShipBitmap.SetPixel(0, 2, new Color(1f, 0f, 0f, 1f));
		this.trackShipBitmap.SetPixel(1, 0, new Color(1f, 0f, 0f, 1f));
		this.trackShipBitmap.SetPixel(1, 1, new Color(1f, 0f, 0f, 1f));
		this.trackShipBitmap.SetPixel(1, 2, new Color(1f, 0f, 0f, 1f));
		this.trackShipBitmap.SetPixel(2, 0, new Color(1f, 0f, 0f, 1f));
		this.trackShipBitmap.SetPixel(2, 1, new Color(1f, 0f, 0f, 1f));
		this.trackShipBitmap.SetPixel(2, 2, new Color(1f, 0f, 0f, 1f));
		this.trackShipBitmap.Apply();
		this.j = 0;
		if (this.galaxy.collisionsMap != null)
		{
			this.doMe = new Action(this, MapShooter.DoDrawCollMap);
		}
		else
		{
			this.doMe = null;
		}
	}

	private void DoDrawCollMap()
	{
		for (int i = 0; i < (int)this.galaxy.collisionsMap.Length; i++)
		{
			if (this.pp.galaxy.collisionsMap[i].Get(this.j))
			{
				this.previewBitmap.SetPixel((int)((float)this.j * this.scanStep), (int)((float)i * this.scanStep), this.colorCollMap);
			}
		}
		MapShooter mapShooter = this;
		mapShooter.j = mapShooter.j + 1;
		if (this.j < this.galaxy.collisionsMap[0].get_Length())
		{
			return;
		}
		this.previewBitmap.Apply();
		this.trackShipBitmap = new Texture2D(3, 3);
		this.trackShipBitmap.SetPixel(0, 0, new Color(1f, 0f, 0f, 1f));
		this.trackShipBitmap.SetPixel(0, 1, new Color(1f, 0f, 0f, 1f));
		this.trackShipBitmap.SetPixel(0, 2, new Color(1f, 0f, 0f, 1f));
		this.trackShipBitmap.SetPixel(1, 0, new Color(1f, 0f, 0f, 1f));
		this.trackShipBitmap.SetPixel(1, 1, new Color(1f, 0f, 0f, 1f));
		this.trackShipBitmap.SetPixel(1, 2, new Color(1f, 0f, 0f, 1f));
		this.trackShipBitmap.SetPixel(2, 0, new Color(1f, 0f, 0f, 1f));
		this.trackShipBitmap.SetPixel(2, 1, new Color(1f, 0f, 0f, 1f));
		this.trackShipBitmap.SetPixel(2, 2, new Color(1f, 0f, 0f, 1f));
		this.trackShipBitmap.Apply();
		this.previewDone = true;
		this.doMe = null;
	}

	private void ExportResult()
	{
		// 
		// Current member / type: System.Void MapShooter::ExportResult()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void ExportResult()
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

	private void InitCollMapPreview()
	{
		// 
		// Current member / type: System.Void MapShooter::InitCollMapPreview()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void InitCollMapPreview()
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

	private void InitGui()
	{
		if (AndromedaGui.gui == null)
		{
			return;
		}
		this.doMe = null;
		this.wnd = new GuiWindow()
		{
			boundries = new Rect(240f, 10f, 100f, 300f)
		};
		this.btn = new GuiButtonResizeable();
		this.btn.SetBlueTexture();
		this.btn.Width = 100f;
		this.btn.Caption = "Export";
		this.btn.FontSize = 12;
		this.btn.Alignment = 4;
		this.btn.X = 0f;
		this.btn.Y = 0f;
		this.btn.Clicked = new Action<EventHandlerParam>(this, MapShooter.OnClicked);
		this.wnd.AddGuiElement(this.btn);
		this.btnCheckCollisions = new GuiButtonResizeable();
		this.btnCheckCollisions.SetBlueTexture();
		this.btnCheckCollisions.Width = 100f;
		this.btnCheckCollisions.Caption = "Show/Hide Collisions";
		this.btnCheckCollisions.FontSize = 12;
		this.btnCheckCollisions.Alignment = 4;
		this.btnCheckCollisions.X = 0f;
		this.btnCheckCollisions.Y = 50f;
		this.btnCheckCollisions.isEnabled = this.result != null;
		this.btnCheckCollisions.Clicked = new Action<EventHandlerParam>(this, MapShooter.OnCheckCollisions);
		this.wnd.AddGuiElement(this.btnCheckCollisions);
		this.btnShowHideMeshes = new GuiButtonResizeable();
		this.btnShowHideMeshes.SetBlueTexture();
		this.btnShowHideMeshes.Width = 100f;
		this.btnShowHideMeshes.Caption = "Show/Hide Meshes";
		this.btnShowHideMeshes.FontSize = 12;
		this.btnShowHideMeshes.Alignment = 4;
		this.btnShowHideMeshes.X = 0f;
		this.btnShowHideMeshes.Y = 100f;
		this.btnShowHideMeshes.isEnabled = this.result != null;
		this.btnShowHideMeshes.Clicked = new Action<EventHandlerParam>(this, MapShooter.OnShowHide);
		this.wnd.AddGuiElement(this.btnShowHideMeshes);
		this.wnd.isHidden = false;
		AndromedaGui.gui.AddWindow(this.wnd);
		this.InitCollMapPreview();
	}

	private bool isNameGood(GameObject obj)
	{
		string[] strArray = obj.get_name().Split(new char[] { '/' });
		if (strArray[(int)strArray.Length - 1].StartsWith("Collide"))
		{
			return true;
		}
		if (obj.get_transform().get_parent() == null)
		{
			return false;
		}
		return this.isNameGood(obj.get_transform().get_parent().get_gameObject());
	}

	private void IterateNextChunk()
	{
		int num = 1000;
		while (true)
		{
			int num1 = num;
			num = num1 - 1;
			if (num1 <= 0)
			{
				break;
			}
			this.ScanPoint(this.currentX, this.currentZ);
			MapShooter mapShooter = this;
			mapShooter.currentX = mapShooter.currentX + 1;
			if (this.currentX >= this.columns)
			{
				this.currentX = 0;
				MapShooter mapShooter1 = this;
				mapShooter1.currentZ = mapShooter1.currentZ + 1;
				if (this.currentZ >= this.rows)
				{
					Debug.Log("Done scanning.");
					this.doMe = new Action(this, MapShooter.ExportResult);
					return;
				}
				if (this.currentZ % 100 == 0)
				{
					Debug.Log(string.Concat("Row ", this.currentZ.ToString(), " passed."));
				}
			}
		}
	}

	private void OnCheckCollisions(EventHandlerParam prm)
	{
		if (this.result == null)
		{
			return;
		}
		if (!this.isCollisionCubeOnScreen)
		{
			this.isCollisionCubeOnScreen = true;
			for (int i = 0; i < (int)this.result.Length; i++)
			{
				for (int j = 0; j < this.result[i].get_Length(); j++)
				{
					if (this.result[i].Get(j))
					{
						float single = (float)j * this.scanStep + this.coordinateXMin;
						float single1 = (float)i * this.scanStep + this.coordinateZMin;
						this.collisionCubes.Add(this.CreateCube(single, single1));
					}
				}
			}
		}
		else
		{
			this.isCollisionCubeOnScreen = false;
			for (int k = 0; k < this.collisionCubes.get_Count(); k++)
			{
				Object.DestroyImmediate(this.collisionCubes.get_Item(k));
			}
			this.collisionCubes.Clear();
		}
	}

	private void OnClicked(EventHandlerParam prm)
	{
		this.btn.isEnabled = false;
		this.StartScan();
	}

	private void OnGUI()
	{
		if (this.previewDone)
		{
			GUI.DrawTexture(new Rect(200f, 100f, (float)this.galaxy.width, (float)this.galaxy.height), this.previewBitmap);
			MapShooter mapShooter = this;
			long num = mapShooter.cc;
			long num1 = num;
			mapShooter.cc = num + (long)1;
			if (num1 % (long)1000 == 0)
			{
				float single = NetworkScript.player.vessel.x + Math.Abs(this.coordinateXMin);
				string str = single.ToString();
				float single1 = this.coordinateZMax - NetworkScript.player.vessel.z;
				Debug.Log(string.Concat(str, ":", single1.ToString()));
			}
			GUI.DrawTexture(new Rect(200f + NetworkScript.player.vessel.x + Math.Abs(this.coordinateXMin), 100f + this.coordinateZMax - NetworkScript.player.vessel.z, 3f, 3f), this.trackShipBitmap);
		}
	}

	private void OnMouseDown()
	{
	}

	private void OnShowHide(EventHandlerParam prm)
	{
		if (this.collideObjects == null)
		{
			GameObject[] gameObjectArray = Object.FindObjectsOfType<GameObject>();
			if (MapShooter.<>f__am$cache21 == null)
			{
				MapShooter.<>f__am$cache21 = new Func<GameObject, bool>(null, (GameObject p) => p.get_name().StartsWith("Collide"));
			}
			this.collideObjects = Enumerable.ToArray<GameObject>(Enumerable.Where<GameObject>(gameObjectArray, MapShooter.<>f__am$cache21));
			Debug.Log(string.Format("Found {0} gameobjects", (int)this.collideObjects.Length));
		}
		if (!this.isMeshOnScreen)
		{
			this.isMeshOnScreen = true;
			for (int i = 0; i < (int)this.collideObjects.Length; i++)
			{
				GameObject gameObject = this.collideObjects[i];
				MeshRenderer component = gameObject.GetComponent<MeshRenderer>();
				if (component == null)
				{
					for (int j = 0; j < gameObject.get_transform().get_childCount(); j++)
					{
						component = gameObject.get_transform().GetChild(j).GetComponent<MeshRenderer>();
						if (component != null)
						{
							component.set_enabled(true);
						}
					}
				}
				else
				{
					component.set_enabled(true);
				}
			}
		}
		else
		{
			this.isMeshOnScreen = false;
			for (int k = 0; k < (int)this.collideObjects.Length; k++)
			{
				GameObject gameObject1 = this.collideObjects[k];
				MeshRenderer meshRenderer = gameObject1.GetComponent<MeshRenderer>();
				if (meshRenderer == null)
				{
					for (int l = 0; l < gameObject1.get_transform().get_childCount(); l++)
					{
						meshRenderer = gameObject1.get_transform().GetChild(l).GetComponent<MeshRenderer>();
						if (meshRenderer != null)
						{
							meshRenderer.set_enabled(false);
						}
					}
				}
				else
				{
					meshRenderer.set_enabled(false);
				}
			}
		}
	}

	private void ScanPoint(int xIndex, int zIndex)
	{
		if (xIndex == 0)
		{
			this.result[zIndex] = new BitArray(this.columns, false);
		}
		float single = (float)((float)xIndex * this.scanStep + this.coordinateXMin);
		float single1 = (float)((float)zIndex * this.scanStep + this.coordinateZMin);
		Vector3 vector3 = new Vector3(single, 0f, single1);
		Collider[] colliderArray = Physics.OverlapSphere(vector3, 0.1f);
		for (int i = 0; i < (int)colliderArray.Length; i++)
		{
			if (this.isNameGood(colliderArray[i].get_gameObject()))
			{
				this.result[zIndex].Set(xIndex, true);
			}
		}
	}

	private void Start()
	{
	}

	private void StartScan()
	{
		this.galaxy = NetworkScript.player.vessel.galaxy;
		this.coordinateXMin = -(float)this.galaxy.width / 2f;
		this.coordinateXMax = -this.coordinateXMin;
		this.coordinateZMin = -(float)this.galaxy.height / 2f;
		this.coordinateZMax = -this.coordinateZMin;
		this.rows = (int)((this.coordinateZMax - this.coordinateZMin) / this.scanStep);
		this.columns = (int)((this.coordinateXMax - this.coordinateXMin) / this.scanStep);
		this.result = new BitArray[this.rows];
		this.scnanedObjects = Object.FindObjectsOfType(typeof(Object));
		Debug.Log(string.Concat("Got ", (int)this.scnanedObjects.Length, " objects."));
		if ((int)this.scnanedObjects.Length < 1)
		{
			Debug.Log("Cancelled scanning.");
			return;
		}
		this.currentX = 0;
		this.currentZ = 0;
		this.doMe = new Action(this, MapShooter.IterateNextChunk);
	}

	private void Update()
	{
		if (this.delay >= 0f)
		{
			MapShooter _deltaTime = this;
			_deltaTime.delay = _deltaTime.delay - Time.get_deltaTime();
		}
		else if (!this.isDone)
		{
			this.isDone = true;
		}
		if (NetworkScript.player == null || NetworkScript.player.vessel == null || this.doMe == null)
		{
			return;
		}
		this.doMe.Invoke();
	}
}