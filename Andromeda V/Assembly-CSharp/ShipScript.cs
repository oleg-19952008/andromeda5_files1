using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using TransferableObjects;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
	[NonSerialized]
	private bool waitingSpaceCoordinates;

	[NonSerialized]
	private Location targetLocation;

	[NonSerialized]
	private GameObject freeSpaceSkillIndicator;

	[NonSerialized]
	public GameObject partyArrowOne;

	[NonSerialized]
	public GameObject partyArrowTwo;

	[NonSerialized]
	public GameObject partyArrowThree;

	[NonSerialized]
	private long partyMemberOneId = (long)-1;

	[NonSerialized]
	private long partyMemberTwoId = (long)-1;

	[NonSerialized]
	private long partyMemberThreeId = (long)-1;

	[NonSerialized]
	public bool isOnline = true;

	[NonSerialized]
	public GameObject[] PiecesPrefabs;

	[NonSerialized]
	public GameObject myShipBarBody;

	[NonSerialized]
	public GameObject myShipBarBlue;

	[NonSerialized]
	public GameObject myShipBarGreen;

	[NonSerialized]
	public GameObject myShipBarEnergy;

	[NonSerialized]
	private uint selectedEnemyNBID;

	[NonSerialized]
	private uint oldSelectedEnemyNBID;

	[NonSerialized]
	private int moveCommandInterval = 125;

	[NonSerialized]
	public float cameraOffsetX;

	[NonSerialized]
	public float cameraOffsetY;

	[NonSerialized]
	public float cameraOffsetZ;

	[NonSerialized]
	public PlayerObjectPhysics p;

	[NonSerialized]
	public long netId;

	[NonSerialized]
	public static float MINERAL_COLLIDE_BOX_SIZE;

	[NonSerialized]
	public static float MINING_RANGE;

	[NonSerialized]
	public static int MAP_WIDTH;

	[NonSerialized]
	public static int MAP_HEIGHT;

	[NonSerialized]
	public static int MAP_X_OFFSET;

	[NonSerialized]
	public static int MAP_Y_OFFSET;

	[NonSerialized]
	public NetworkScript comm;

	[NonSerialized]
	public bool isRulingLocalPlayer;

	[NonSerialized]
	public bool isInControl = true;

	[NonSerialized]
	public NavigationMap navMap;

	[NonSerialized]
	public static float NEXT_TARGET_SHORTCUT_RANGE;

	[NonSerialized]
	public bool isGuiClosed;

	[NonSerialized]
	public Texture2D textureMapMyShip;

	[NonSerialized]
	public Texture2D textureMapOtherShip;

	[NonSerialized]
	public Texture2D textureMapBullet;

	[NonSerialized]
	public Texture2D textureMapRocket;

	private float deltaCameraZ = 9f;

	[NonSerialized]
	public KeyboardShortcuts kb;

	[NonSerialized]
	public bool isChangingKey;

	[NonSerialized]
	public bool destroyMe;

	[NonSerialized]
	public bool keyUp;

	[NonSerialized]
	public bool keyDown;

	[NonSerialized]
	public bool keyLeft;

	[NonSerialized]
	public bool keyRight;

	[NonSerialized]
	public bool keyUpNew;

	[NonSerialized]
	public bool keyDownNew;

	[NonSerialized]
	public bool keyLeftNew;

	[NonSerialized]
	public bool keyRightNew;

	private Texture2D fadeOutTexture;

	public bool isTakingScreenshot;

	private Color fadeColor;

	private GameObject zoomSlider;

	private GameObject zoomScroller;

	public float updateInterval = 0.5f;

	private float accum;

	private int frames;

	private float timeleft = 0.5f;

	private bool ShowFPSMeter;

	public GalaxyJumpParam galaxyJumpData;

	private float weaponStatusCheckDelay = 2.5f;

	private float delyBeforOpenInfoWindow = 1.6f;

	private bool isWaitingForShowInfoWindow;

	private float zoomIdleTimer = 2f;

	private float zoomMoveStep = 0.635f;

	private bool isWaitingForShowDailyReward;

	private float delyBeforOpenDailyReward;

	[NonSerialized]
	private DateTime lastMoveCommandTime;

	[NonSerialized]
	public bool isMoveCommandActive;

	[NonSerialized]
	public static GameObject mapTarget;

	[NonSerialized]
	public static GameObject skillTarget;

	private GameObject directionArrow;

	private GameObject pushToBoost;

	[NonSerialized]
	public GameObjectPhysics selectedObject;

	[NonSerialized]
	private GameObject selectedGameObject;

	private bool isAbortMiningNeeded;

	[NonSerialized]
	public bool isFullCargo;

	[NonSerialized]
	public bool isFullInventory;

	[NonSerialized]
	public bool unsuccessfulMiningGoesOn;

	[NonSerialized]
	public GameObject outOfMiningRangeMarker;

	[NonSerialized]
	public GameObject _miningBeam;

	[NonSerialized]
	public GameObject _hyperJumpBeam;

	private bool isMineralOutofRange;

	public MineralEx autoMiningMineral;

	public bool isMiningActionSend;

	public uint lastMinedMineralNbId;

	private bool isTryMiningPartyLoot;

	private float delayBeforeRetry = 0.25f;

	private bool isStartMiningNeeded;

	private float autoMinerTimer = 0.5f;

	private float maxWeaponRange;

	public Action<object> popUpAction;

	public bool isHotKeysActive = true;

	private int CurrentClosestSelectedTarget;

	private List<ShipScript.ClosestTarget> NearObjects = new List<ShipScript.ClosestTarget>();

	private ShipScript.ClosestTarget shootingAtTarget;

	private List<ShipScript.ClosestTarget> shootingPVPs = new List<ShipScript.ClosestTarget>();

	private List<ShipScript.ClosestTarget> shootingPVEs = new List<ShipScript.ClosestTarget>();

	private List<ShipScript.ClosestTarget> passivePVPs = new List<ShipScript.ClosestTarget>();

	private List<ShipScript.ClosestTarget> passivePVEs = new List<ShipScript.ClosestTarget>();

	private List<ShipScript.ClosestTarget> extractionPoints = new List<ShipScript.ClosestTarget>();

	private Vector2 previousPosition;

	private List<ShipScript.ClosestMineral> nearMinerals = new List<ShipScript.ClosestMineral>();

	private int selectedNearMineralIndex;

	private float socialWindowTimer = 0.6f;

	private bool isShipSelected;

	private Vector2 moveOffset = Vector2.get_zero();

	[NonSerialized]
	public ShipScript shootTarget;

	[NonSerialized]
	public ShipConfiguration shipConfiguration;

	[NonSerialized]
	public GameObject _lock;

	[NonSerialized]
	private GameObject relativeAnimator;

	[NonSerialized]
	private Vector3 relativeAnimatorPosition;

	[NonSerialized]
	private Quaternion relativeAnimatorRotation;

	[NonSerialized]
	private bool isDying;

	[NonSerialized]
	private bool isIdling;

	[NonSerialized]
	public Texture mapTexture;

	[NonSerialized]
	private Rect windowRect = new Rect((float)(Screen.get_width() - 3 * ShipScript.MAP_WIDTH), (float)(Screen.get_height() - ShipScript.MAP_HEIGHT - 30), (float)(ShipScript.MAP_WIDTH + 10), (float)(ShipScript.MAP_HEIGHT + 25));

	[NonSerialized]
	private DateTime startHyperJumpTime;

	[NonSerialized]
	public DateTime startGalaxyJumpTime;

	[NonSerialized]
	public bool isJumping;

	[NonSerialized]
	public bool isGalaxyJumping;

	public GameObject galaxyJumpIndication;

	public GameObject galaxyJumpEf;

	[NonSerialized]
	private Vector3 scaleRatio;

	[NonSerialized]
	private DateTime scaleStartTime;

	[NonSerialized]
	private bool isScalingOut;

	[NonSerialized]
	private bool isScalingIn;

	private GameObject targetArrow;

	private GameObject guidingText;

	private GuiWindow rdArrowWindow;

	private float rdArrowDeltaTime;

	private float dronX;

	private GuiWindow transformerBtnArrowWindow;

	private float transformerBtnArrowDeltaTime;

	private GuiWindow transformerItemArrowWindow;

	private GuiTexture smallArrowTexture;

	private float transformerItemArrowDeltaTime;

	private float itemPositionX;

	private float itemPositionY;

	public int transformerInventoryScrollIndex;

	[NonSerialized]
	public StarBaseNet baseInRange;

	[NonSerialized]
	public NpcObjectPhysics npcInRange;

	[NonSerialized]
	public CheckpointObjectPhysics checkpointInRange;

	[NonSerialized]
	public HyperJumpNet hyperJumpInRange;

	[NonSerialized]
	public ExtractionPoint extractionPointInRange;

	public LevelMap jumpDestionationGalaxy;

	public InstanceDifficulty selectedDifficulty;

	private GuiWindow bigStoryArrowWindow;

	private float STORY_WINDOW_MAX_Y = 160f;

	private float deltaTime;

	public GameObject criticalPendalum;

	private byte comboTarget;

	private byte comboDone;

	public GameObject speedEffectShip;

	public GameObject speedEffectCamera;

	private float speedEffectTime;

	private float speedEffectDuration = 3f;

	private float speedEffectDeltaX;

	private float speedEffectDeltaZ;

	private AudioSource speedEffectSound;

	public bool stopBoostActivated;

	public float stopSpeedBoostDelay;

	private StarBaseNet BaseInRange
	{
		get
		{
			StarBaseNet starBaseNet;
			IList<GameObjectPhysics> values = this.comm.gameObjects.get_Values();
			if (ShipScript.<>f__am$cacheA4 == null)
			{
				ShipScript.<>f__am$cacheA4 = new Func<GameObjectPhysics, bool>(null, (GameObjectPhysics o) => o is StarBaseNet);
			}
			IEnumerator<GameObjectPhysics> enumerator = Enumerable.Where<GameObjectPhysics>(values, ShipScript.<>f__am$cacheA4).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					StarBaseNet current = (StarBaseNet)enumerator.get_Current();
					if (!current.IsObjectInRange(this.p))
					{
						continue;
					}
					starBaseNet = current;
					return starBaseNet;
				}
				return null;
			}
			finally
			{
				if (enumerator == null)
				{
				}
				enumerator.Dispose();
			}
			return starBaseNet;
		}
	}

	private CheckpointObjectPhysics CheckpointInRange
	{
		get
		{
			CheckpointObjectPhysics checkpointObjectPhysic;
			IList<GameObjectPhysics> values = this.comm.gameObjects.get_Values();
			if (ShipScript.<>f__am$cacheA6 == null)
			{
				ShipScript.<>f__am$cacheA6 = new Func<GameObjectPhysics, bool>(null, (GameObjectPhysics o) => o is CheckpointObjectPhysics);
			}
			IEnumerator<GameObjectPhysics> enumerator = Enumerable.Where<GameObjectPhysics>(values, ShipScript.<>f__am$cacheA6).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					CheckpointObjectPhysics current = (CheckpointObjectPhysics)enumerator.get_Current();
					if (!current.IsObjectInRange(this.p))
					{
						continue;
					}
					checkpointObjectPhysic = current;
					return checkpointObjectPhysic;
				}
				return null;
			}
			finally
			{
				if (enumerator == null)
				{
				}
				enumerator.Dispose();
			}
			return checkpointObjectPhysic;
		}
	}

	private ExtractionPoint ExtractionPointInRange
	{
		get
		{
			ExtractionPoint extractionPoint;
			IList<GameObjectPhysics> values = this.comm.gameObjects.get_Values();
			if (ShipScript.<>f__am$cacheA8 == null)
			{
				ShipScript.<>f__am$cacheA8 = new Func<GameObjectPhysics, bool>(null, (GameObjectPhysics o) => o is ExtractionPoint);
			}
			IEnumerator<GameObjectPhysics> enumerator = Enumerable.Where<GameObjectPhysics>(values, ShipScript.<>f__am$cacheA8).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					ExtractionPoint current = (ExtractionPoint)enumerator.get_Current();
					if (!current.IsObjectInRange(this.p))
					{
						continue;
					}
					extractionPoint = current;
					return extractionPoint;
				}
				return null;
			}
			finally
			{
				if (enumerator == null)
				{
				}
				enumerator.Dispose();
			}
			return extractionPoint;
		}
	}

	private MineralEx HightPriorityMineral
	{
		get
		{
			if (!(this.selectedObject is MineralEx))
			{
				return null;
			}
			return (MineralEx)this.selectedObject;
		}
	}

	private HyperJumpNet HyperJumpInRange
	{
		get
		{
			HyperJumpNet hyperJumpNet;
			IList<GameObjectPhysics> values = this.comm.gameObjects.get_Values();
			if (ShipScript.<>f__am$cacheA7 == null)
			{
				ShipScript.<>f__am$cacheA7 = new Func<GameObjectPhysics, bool>(null, (GameObjectPhysics o) => o is HyperJumpNet);
			}
			IEnumerator<GameObjectPhysics> enumerator = Enumerable.Where<GameObjectPhysics>(values, ShipScript.<>f__am$cacheA7).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					HyperJumpNet current = (HyperJumpNet)enumerator.get_Current();
					if (!current.IsObjectInRange(this.p))
					{
						continue;
					}
					hyperJumpNet = current;
					return hyperJumpNet;
				}
				return null;
			}
			finally
			{
				if (enumerator == null)
				{
				}
				enumerator.Dispose();
			}
			return hyperJumpNet;
		}
	}

	private bool IsActing
	{
		get
		{
			if (this.selectedObject == null)
			{
				return false;
			}
			if (this.selectedObject is MineralEx)
			{
				return this.p.miningState != 0;
			}
			if (!(this.selectedObject is PlayerObjectPhysics))
			{
				return false;
			}
			return (!this.p.isShooting || this.p.shootingAt == null ? false : this.selectedObject.neighbourhoodId == this.p.shootingAt.neighbourhoodId);
		}
	}

	private bool IsMouseClickInsideMap
	{
		get
		{
			return false;
		}
	}

	private NpcObjectPhysics NpcInRange
	{
		get
		{
			NpcObjectPhysics npcObjectPhysic;
			IList<GameObjectPhysics> values = this.comm.gameObjects.get_Values();
			if (ShipScript.<>f__am$cacheA5 == null)
			{
				ShipScript.<>f__am$cacheA5 = new Func<GameObjectPhysics, bool>(null, (GameObjectPhysics o) => o is NpcObjectPhysics);
			}
			IEnumerator<GameObjectPhysics> enumerator = Enumerable.Where<GameObjectPhysics>(values, ShipScript.<>f__am$cacheA5).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					NpcObjectPhysics current = (NpcObjectPhysics)enumerator.get_Current();
					if (!current.IsObjectInRange(this.p))
					{
						continue;
					}
					npcObjectPhysic = current;
					return npcObjectPhysic;
				}
				return null;
			}
			finally
			{
				if (enumerator == null)
				{
				}
				enumerator.Dispose();
			}
			return npcObjectPhysic;
		}
	}

	static ShipScript()
	{
		ShipScript.MINERAL_COLLIDE_BOX_SIZE = 2f;
		ShipScript.MINING_RANGE = 9f;
		ShipScript.MAP_WIDTH = 230;
		ShipScript.MAP_HEIGHT = 200;
		ShipScript.MAP_X_OFFSET = 4;
		ShipScript.MAP_Y_OFFSET = 20;
		ShipScript.NEXT_TARGET_SHORTCUT_RANGE = 20f;
	}

	public ShipScript()
	{
	}

	private void AbortMining()
	{
		// 
		// Current member / type: System.Void ShipScript::AbortMining()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void AbortMining()
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..( , Int32 , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:строка 78
		//    в ..( , Int32& , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:строка 33
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 57
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(IfStatement ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 359
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 55
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
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

	private void BreakAutoMining()
	{
		// 
		// Current member / type: System.Void ShipScript::BreakAutoMining()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void BreakAutoMining()
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

	public void BreakGalaxyJump()
	{
		this.isInControl = true;
		this.p.moveState = 0;
		this.isGalaxyJumping = false;
		if (this.galaxyJumpIndication != null)
		{
			Object.Destroy(this.galaxyJumpIndication);
			this.galaxyJumpIndication = null;
		}
		if (this.galaxyJumpEf != null)
		{
			Object.Destroy(this.galaxyJumpEf);
			this.galaxyJumpEf = null;
		}
	}

	public void CancelGalaxyJump(bool sendNotify)
	{
		// 
		// Current member / type: System.Void ShipScript::CancelGalaxyJump(System.Boolean)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void CancelGalaxyJump(System.Boolean)
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..( , Int32 , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:строка 78
		//    в ..( , Int32& , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:строка 33
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 57
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(IfStatement ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 359
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 55
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
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

	public void ChanceForCritical(byte comboCnt)
	{
		this.comboTarget = comboCnt;
		this.comboDone = 0;
		this.ShowCriticalPendulum();
	}

	public void ChangeMaterialForStealth()
	{
		base.get_gameObject().get_renderer().set_material((Material)Resources.Load("StealthMat"));
	}

	public void ChangeMaterialToOriginalOne()
	{
		string str = string.Concat("Ships/", this.p.cfg.assetName, "_pfb");
		GameObject prefab = (GameObject)playWebGame.assets.GetPrefab(str);
		base.get_gameObject().get_renderer().set_material(prefab.get_renderer().get_material());
		Shader shader = Shader.Find("Reflective/Bumped Specular");
		base.get_gameObject().get_renderer().get_material().set_shader(shader);
	}

	public void ChangeShaderToNonTransperant()
	{
		string str = string.Concat("Ships/", this.p.assetName, "_pfb");
		GameObject prefab = (GameObject)playWebGame.assets.GetPrefab(str);
		base.get_gameObject().get_renderer().set_material(prefab.get_renderer().get_material());
		Shader shader = Shader.Find("Reflective/Bumped Specular");
		base.get_gameObject().get_renderer().get_material().set_shader(shader);
	}

	private bool CheckForCriticalCollider()
	{
		// 
		// Current member / type: System.Boolean ShipScript::CheckForCriticalCollider()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Boolean CheckForCriticalCollider()
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..( , Int32 , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:строка 78
		//    в ..( , Int32& , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:строка 33
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 57
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(IfStatement ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 359
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 55
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
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

	private void CheckForRemoveFromNextTargetList(List<ShipScript.ClosestTarget> nextTargetList)
	{
		for (int i = 0; i < nextTargetList.get_Count(); i++)
		{
			if (GameObjectPhysics.GetDistance(this.p.x, nextTargetList.get_Item(i).targetObject.x, this.p.z, nextTargetList.get_Item(i).targetObject.z) > ShipScript.NEXT_TARGET_SHORTCUT_RANGE)
			{
				nextTargetList.RemoveAt(i);
				i--;
			}
			else if (nextTargetList.get_Item(i).targetObject.get_IsPoP())
			{
				PlayerObjectPhysics item = (PlayerObjectPhysics)nextTargetList.get_Item(i).targetObject;
				if (item == null || item.gameObject == null || !item.isAlive || item.isInStealthMode || item.teamNumber == this.p.teamNumber && this.p.fractionId == item.fractionId)
				{
					nextTargetList.RemoveAt(i);
					i--;
				}
			}
		}
	}

	private bool CheckForSpeedBoost()
	{
		RaycastHit raycastHit = new RaycastHit();
		if (Physics.Raycast(Camera.get_main().ScreenPointToRay(Input.get_mousePosition()), ref raycastHit, 500f, 1024))
		{
			return true;
		}
		return false;
	}

	public void CloseBaseDoor()
	{
		GameObject gameObject = GameObject.Find("CstationPfb(Clone)");
		if (gameObject != null)
		{
			gameObject.get_animation().Play("CstationJawClose");
		}
	}

	public bool DecreaseHitPoints(float hit, ShipScript shooterScript)
	{
		if (this.shipConfiguration.hitPoints <= 0)
		{
			return false;
		}
		ShipConfiguration shipConfiguration = this.shipConfiguration;
		shipConfiguration.hitPoints = shipConfiguration.hitPoints - (int)hit;
		if (this.shipConfiguration.hitPoints >= 0)
		{
			return true;
		}
		this.Die(shooterScript);
		GameObject.Find("GlobalObject").GetComponent<NetworkScript>().get_networkView().RPC("DieSrv", 0, new object[] { this.netId });
		return false;
	}

	public void DeselectCurrentObject()
	{
		if (this.selectedObject == null)
		{
			return;
		}
		this.UnselectCurrentObject();
		this.selectedObject = null;
		this.selectedGameObject = null;
	}

	public void DestroyAmmoFlyingTowardsMe()
	{
		Object[] objArray = Resources.FindObjectsOfTypeAll(typeof(BulletScript));
		for (int i = 0; i < (int)objArray.Length; i++)
		{
			BulletScript bulletScript = (BulletScript)objArray[i];
			if (bulletScript.target == this.p)
			{
				bulletScript.isStarted = false;
				Object.Destroy(bulletScript.get_gameObject());
			}
		}
		Object[] objArray1 = Resources.FindObjectsOfTypeAll(typeof(LaserWeldingScript));
		for (int j = 0; j < (int)objArray1.Length; j++)
		{
			LaserWeldingScript laserWeldingScript = (LaserWeldingScript)objArray1[j];
			if (laserWeldingScript.target == this.p)
			{
				laserWeldingScript.isStarted = false;
				Object.Destroy(laserWeldingScript.get_gameObject());
			}
		}
		Object[] objArray2 = Resources.FindObjectsOfTypeAll(typeof(RocketScript));
		for (int k = 0; k < (int)objArray2.Length; k++)
		{
			RocketScript rocketScript = (RocketScript)objArray2[k];
			if (rocketScript.target == this.p)
			{
				rocketScript.isStarted = false;
				Object.Destroy(rocketScript.get_gameObject());
			}
		}
	}

	public void Die(ShipScript shooterScript)
	{
		if (shooterScript != null && shooterScript.shootTarget != null && shooterScript.shootTarget == this)
		{
			shooterScript.TargetDiedStopShooting();
		}
		this.DestroyAmmoFlyingTowardsMe();
		if (this.isRulingLocalPlayer)
		{
			this.isInControl = false;
		}
		this.p.isAlive = false;
		this.p.speed = 0f;
		this.p.moveState = 0;
		this.p.rotationState = 0;
		this.relativeAnimator = (GameObject)Object.Instantiate(GameObject.Find("RelativeAnimator"));
		this.relativeAnimator.get_transform().set_position(base.get_gameObject().get_transform().get_position());
		base.get_gameObject().get_transform().set_parent(this.relativeAnimator.get_transform());
		base.get_gameObject().get_animation().Play("ShipDie");
	}

	private void DrawScreenshotFadeOut()
	{
		if (!this.isTakingScreenshot)
		{
			return;
		}
		if (this.fadeOutTexture == null)
		{
			this.fadeColor = Color.get_white();
			this.fadeOutTexture = new Texture2D(Screen.get_width(), Screen.get_height());
			this.fadeOutTexture.SetPixel(0, 0, this.fadeColor);
			this.fadeOutTexture.Apply();
			GUI.get_skin().get_box().get_normal().set_background(this.fadeOutTexture);
		}
		this.fadeColor.a = this.fadeColor.a - Time.get_deltaTime() / 1.5f;
		GUI.set_color(this.fadeColor);
		GUI.Box(new Rect(0f, 0f, (float)Screen.get_width(), (float)Screen.get_height()), GUIContent.none);
		GUI.set_depth(0);
		if (this.fadeColor.a <= 0f)
		{
			this.fadeColor.a = 1f;
			this.isTakingScreenshot = false;
			Object.Destroy(this.fadeOutTexture);
			GUI.get_skin().get_box().get_normal().set_background(null);
			GUI.set_color(new Color(1f, 1f, 1f, 1f));
			GUI.set_depth(1);
			if (!NetworkScript.isInBase && AndromedaGui.mainWnd != null)
			{
				AndromedaGui.mainWnd.OpenFeedbackWindow();
			}
		}
	}

	public void EnteredBase(PlayerObjectPhysics plr)
	{
		this.comm.StartCoroutine(this.PreapearInBaseScene());
		NetworkScript.playerNameManager.removePOPName(plr);
		if (this.myShipBarBody != null)
		{
			Object.Destroy(this.myShipBarBody);
			Object.Destroy(this.myShipBarBlue);
			Object.Destroy(this.myShipBarGreen);
			Object.Destroy(this.myShipBarEnergy);
			this.myShipBarBody = null;
			this.myShipBarBlue = null;
			this.myShipBarGreen = null;
			this.myShipBarEnergy = null;
		}
		if (this.selectedObject != null)
		{
			this.DeselectCurrentObject();
			if (this.p.selectedPoPnbId != 0)
			{
				playWebGame.udp.ExecuteCommand(49, new UniversalTransportContainer(), 56);
				this.p.selectedPoPnbId = 0;
			}
		}
		this.comm.RemoveGameObject(this.p.neighbourhoodId);
		if (!this.isRulingLocalPlayer)
		{
			return;
		}
		Minimap.HideWindow();
		AndromedaGui.mainWnd.HideSideMenus();
		playWebGame.AcquirePreparedScene();
	}

	public void EnterInGalaxyJump()
	{
		this.isInControl = false;
		this.p.destinationX = this.p.x;
		this.p.destinationZ = this.p.z;
		this.p.moveState = 0;
		this.startGalaxyJumpTime = StaticData.now;
		this.isGalaxyJumping = true;
		this.galaxyJumpIndication = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("LoadingBar_pfb"));
		this.galaxyJumpIndication.get_transform().set_position(new Vector3(this.p.x, 0f, this.p.z + 4.4f));
		this.galaxyJumpEf = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("GalaxyJumpPfb"));
		this.galaxyJumpEf.get_transform().set_position(new Vector3(this.p.x, 1.5f, this.p.z));
	}

	public void EnterInHyperJump(GameObject hj, GameObject ship)
	{
		if (this.isRulingLocalPlayer)
		{
			AndromedaGui.gui.StartFadeIn();
		}
		this.startHyperJumpTime = StaticData.now;
		GameObject prefab = (GameObject)playWebGame.assets.GetPrefab("HyperJumpBeamPfb");
		this._hyperJumpBeam = (GameObject)Object.Instantiate(prefab);
		this._hyperJumpBeam.GetComponent<HyperJumpEntrance>().hyperJump = hj;
		this._hyperJumpBeam.GetComponent<HyperJumpEntrance>().ship = ship;
		this._hyperJumpBeam.get_transform().set_position(base.get_gameObject().get_transform().get_position());
		this._hyperJumpBeam.get_transform().LookAt(hj.get_transform().get_position());
		this.isJumping = true;
	}

	private ShipScript GetCollidingEnemyPlayer(Collider[] colliders)
	{
		ShipScript shipScript;
		Collider[] colliderArray = colliders;
		if (ShipScript.<>f__am$cacheBA == null)
		{
			ShipScript.<>f__am$cacheBA = new Func<Collider, bool>(null, (Collider c) => c.GetComponent(typeof(ShipScript)) != null);
		}
		List<Collider> list = Enumerable.ToList<Collider>(Enumerable.Where<Collider>(colliderArray, ShipScript.<>f__am$cacheBA));
		List<Collider>.Enumerator enumerator = list.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Collider current = enumerator.get_Current();
				ShipScript component = (ShipScript)current.get_gameObject().GetComponent(typeof(ShipScript));
				if (!NetworkScript.player.vessel.CanShootThisTarget(component.p))
				{
					continue;
				}
				shipScript = component;
				return shipScript;
			}
			return null;
		}
		finally
		{
			enumerator.Dispose();
		}
		return shipScript;
	}

	private ShipScript GetCollidingEnemyShip(Collider[] colliders)
	{
		Collider[] colliderArray = colliders;
		if (ShipScript.<>f__am$cacheB9 == null)
		{
			ShipScript.<>f__am$cacheB9 = new Func<Collider, bool>(null, (Collider c) => c.GetComponent(typeof(ShipScript)) != null);
		}
		Collider collider = Enumerable.FirstOrDefault<Collider>(Enumerable.Where<Collider>(colliderArray, ShipScript.<>f__am$cacheB9));
		if (collider == null)
		{
			return null;
		}
		return (ShipScript)collider.get_gameObject().GetComponent(typeof(ShipScript));
	}

	private ExtractionPointScript GetCollidingExtractionPoint(Collider[] colliders)
	{
		Collider[] colliderArray = colliders;
		if (ShipScript.<>f__am$cacheBC == null)
		{
			ShipScript.<>f__am$cacheBC = new Func<Collider, bool>(null, (Collider c) => c.GetComponent(typeof(ExtractionPointScript)) != null);
		}
		Collider collider = Enumerable.FirstOrDefault<Collider>(Enumerable.Where<Collider>(colliderArray, ShipScript.<>f__am$cacheBC));
		if (collider == null)
		{
			return null;
		}
		return (ExtractionPointScript)collider.get_gameObject().GetComponent(typeof(ExtractionPointScript));
	}

	private MineralScript GetCollidingMineralNew(Collider[] colliders)
	{
		Collider[] colliderArray = colliders;
		if (ShipScript.<>f__am$cacheB8 == null)
		{
			ShipScript.<>f__am$cacheB8 = new Func<Collider, bool>(null, (Collider c) => c.GetComponent(typeof(MineralScript)) != null);
		}
		Collider collider = Enumerable.FirstOrDefault<Collider>(Enumerable.Where<Collider>(colliderArray, ShipScript.<>f__am$cacheB8));
		if (collider == null)
		{
			return null;
		}
		return (MineralScript)collider.get_gameObject().GetComponent(typeof(MineralScript));
	}

	private PveScript GetCollidingPve(Collider[] colliders)
	{
		Collider[] colliderArray = colliders;
		if (ShipScript.<>f__am$cacheBB == null)
		{
			ShipScript.<>f__am$cacheBB = new Func<Collider, bool>(null, (Collider c) => c.GetComponent(typeof(PveScript)) != null);
		}
		Collider collider = Enumerable.FirstOrDefault<Collider>(Enumerable.Where<Collider>(colliderArray, ShipScript.<>f__am$cacheBB));
		if (collider == null)
		{
			return null;
		}
		return (PveScript)collider.get_gameObject().GetComponent(typeof(PveScript));
	}

	private Vector3 GetMouseToWorldPosition()
	{
		Vector3 vector3 = new Vector3(0f, 0f, 0f);
		vector3 = Input.get_mousePosition();
		Ray ray = Camera.get_main().ScreenPointToRay(vector3);
		Plane plane = new Plane(new Vector3(0f, 0f, 0f), new Vector3(1f, 0f, 0f), new Vector3(0f, 0f, 1f));
		float single = 0f;
		plane.Raycast(ray, ref single);
		Vector3 _position = Camera.get_main().get_transform().get_position() + ray.GetPoint(single);
		_position.y = 0f;
		float single1 = _position.z;
		Vector3 _position1 = Camera.get_main().get_transform().get_position();
		_position.z = single1 - _position1.z;
		float single2 = _position.x;
		Vector3 vector31 = Camera.get_main().get_transform().get_position();
		_position.x = single2 - vector31.x;
		return _position;
	}

	private Collider[] GetRayCastedColliders()
	{
		Ray ray = Camera.get_main().ScreenPointToRay(Input.get_mousePosition());
		RaycastHit[] raycastHitArray = Physics.RaycastAll(ray, 300f);
		Collider[] _collider = new Collider[(int)raycastHitArray.Length];
		for (int i = 0; i < (int)raycastHitArray.Length; i++)
		{
			_collider[i] = raycastHitArray[i].get_collider();
		}
		return _collider;
	}

	private Vector3 GetWorldPositionFromMap()
	{
		float mAPWIDTH = (float)(this.p.galaxy.width / ShipScript.MAP_WIDTH);
		float mAPHEIGHT = (float)(this.p.galaxy.height / ShipScript.MAP_HEIGHT);
		Vector3 _mousePosition = Input.get_mousePosition();
		float _x = (_mousePosition.x - this.windowRect.get_x() - (float)ShipScript.MAP_X_OFFSET) * mAPWIDTH - (float)(this.p.galaxy.width / 2);
		float _height = (float)Screen.get_height();
		Vector3 vector3 = Input.get_mousePosition();
		float _y = -(_height - vector3.y - this.windowRect.get_y() - (float)ShipScript.MAP_Y_OFFSET) * mAPHEIGHT + (float)(this.p.galaxy.height / 2);
		return new Vector3(_x, 0f, _y);
	}

	public void GotKilled(PlayerObjectPhysics me)
	{
		this.p.isAlive = false;
		this.p.speed = 0f;
		this.p.moveState = 0;
		this.p.rotationState = 0;
		this.relativeAnimator = (GameObject)Object.Instantiate(GameObject.Find("RelativeAnimator"));
		this.relativeAnimator.get_transform().set_position(base.get_gameObject().get_transform().get_position());
		base.get_gameObject().get_transform().set_parent(this.relativeAnimator.get_transform());
		this.isDying = true;
		this.relativeAnimatorPosition = this.relativeAnimator.get_transform().get_position();
		this.relativeAnimatorRotation = this.relativeAnimator.get_transform().get_rotation();
		this.targetLocation = null;
		this.p.castingSkillSlot = -1;
		this.waitingSpaceCoordinates = false;
		if (this.freeSpaceSkillIndicator != null)
		{
			Object.Destroy(this.freeSpaceSkillIndicator);
		}
		if (ShipScript.skillTarget != null)
		{
			Object.Destroy(ShipScript.skillTarget);
		}
		NetworkScript.Expode(base.get_transform().get_position());
		Object.Destroy(base.get_gameObject());
		if (this.myShipBarBody != null)
		{
			Object.Destroy(this.myShipBarBody);
			Object.Destroy(this.myShipBarBlue);
			Object.Destroy(this.myShipBarGreen);
			Object.Destroy(this.myShipBarEnergy);
			this.myShipBarBody = null;
			this.myShipBarBlue = null;
			this.myShipBarGreen = null;
			this.myShipBarEnergy = null;
		}
		if (AndromedaGui.personalStatsWnd != null)
		{
			AndromedaGui.personalStatsWnd.SetToZero();
		}
		if (this._lock != null)
		{
			Object.Destroy(this._lock);
		}
		this.DestroyAmmoFlyingTowardsMe();
		if (this.p.miningState != 0)
		{
			Object.Destroy(this._miningBeam);
			this.p.miningState = 0;
			this.p.miningMineral = null;
			this.p.miningMineralNbId = 0;
		}
		if (this.selectedObject != null)
		{
			Object.Destroy(this._lock);
			this._lock = null;
			this.selectedObject = null;
		}
		if (this.speedEffectShip != null)
		{
			this.StopSpeedBooster();
		}
		if (this.directionArrow != null)
		{
			Object.Destroy(this.directionArrow);
		}
		if (this.isRulingLocalPlayer)
		{
			this.isInControl = false;
		}
		IEnumerator<PlayerDataEx> enumerator = NetworkScript.clientSideClientsList.get_Values().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				PlayerDataEx current = enumerator.get_Current();
				if (current.shipScript.selectedObject == this.p)
				{
					current.shipScript.selectedObject = null;
					Object.Destroy(current.shipScript._lock);
					current.shipScript._lock = null;
				}
				if (current.vessel.shootingAt != this.p)
				{
					continue;
				}
				current.vessel.isShooting = false;
				current.vessel.shootingAt = null;
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

	public void InitFactionGalaxyJump()
	{
		// 
		// Current member / type: System.Void ShipScript::InitFactionGalaxyJump()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void InitFactionGalaxyJump()
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

	public void InitGalaxyJump()
	{
		// 
		// Current member / type: System.Void ShipScript::InitGalaxyJump()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void InitGalaxyJump()
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

	public void InitHyperJump()
	{
		// 
		// Current member / type: System.Void ShipScript::InitHyperJump()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void InitHyperJump()
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

	private void InitializeGuidingText()
	{
		if (this.guidingText != null)
		{
			this.guidingText.get_transform().set_position(new Vector3(NetworkScript.player.vessel.x, 2f, NetworkScript.player.vessel.z + 4f));
		}
		else
		{
			this.guidingText = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("SpaceLbl"));
			TextMesh componentInChildren = this.guidingText.GetComponentInChildren<TextMesh>();
			this.guidingText.GetComponentInChildren<MeshRenderer>().get_material().set_color(GuiNewStyleBar.orangeColor);
			if (componentInChildren != null)
			{
				componentInChildren.set_text(StaticData.Translate("key_space_label_repair_your_ship"));
				componentInChildren.set_fontSize(40);
				componentInChildren.set_alignment(1);
			}
		}
	}

	private void InitRapairDroneArrow()
	{
		IList<ActiveSkillSlot> values = NetworkScript.player.playerBelongings.skillConfig.skillSlots.get_Values();
		if (ShipScript.<>f__am$cacheBD == null)
		{
			ShipScript.<>f__am$cacheBD = new Func<ActiveSkillSlot, bool>(null, (ActiveSkillSlot t) => t.skillId == PlayerItems.TypeTalentsRepairingDrones);
		}
		ActiveSkillSlot activeSkillSlot = Enumerable.FirstOrDefault<ActiveSkillSlot>(Enumerable.Where<ActiveSkillSlot>(values, ShipScript.<>f__am$cacheBD));
		if (activeSkillSlot == null)
		{
			return;
		}
		this.dronX = AndromedaGui.mainWnd.quickSlotsWindow.boundries.get_x() + (float)(activeSkillSlot.slotId * 40) + 68f - 105f;
		if (this.rdArrowWindow != null)
		{
			this.UpdateRapairDroneArrowPosition();
		}
		else
		{
			this.rdArrowWindow = new GuiWindow();
			this.rdArrowWindow.SetBackgroundTexture("FrameworkGUI", "leftToRightArrow");
			this.rdArrowWindow.zOrder = 208;
			this.rdArrowWindow.ignoreClickEvents = true;
			this.rdArrowWindow.boundries.set_x(this.dronX);
			this.rdArrowWindow.boundries.set_y((float)(Screen.get_height() - 50));
			this.rdArrowWindow.isHidden = false;
			AndromedaGui.gui.AddWindow(this.rdArrowWindow);
			this.rdArrowDeltaTime = 0f;
		}
	}

	private void InitRepairDroneHelper()
	{
		this.InitializeGuidingText();
		this.InitRapairDroneArrow();
	}

	public void InitSpeedBoost()
	{
		if (this.speedEffectShip != null)
		{
			Object.Destroy(this.speedEffectShip);
		}
		this.speedEffectShip = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("SpeedEffect_pfb"));
		this.speedEffectShip.get_transform().set_position(new Vector3(this.p.x, 0f, this.p.z));
		if (this.isRulingLocalPlayer)
		{
			if (this.speedEffectCamera != null)
			{
				Object.Destroy(this.speedEffectCamera);
				Object.Destroy(this.speedEffectSound.get_gameObject());
			}
			this.speedEffectCamera = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("SpeedSkybox_pfb"));
			this.speedEffectCamera.get_transform().set_position(Camera.get_main().get_transform().get_position());
			this.speedEffectTime = 0f;
			this.speedEffectDeltaX = 0f;
			this.speedEffectDeltaZ = 0f;
			AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "speedBoost");
			this.speedEffectSound = AudioManager.PlayGUISoundLoopInf(fromStaticSet);
			fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "speedBoostStart");
			AudioManager.PlayGUISound(fromStaticSet);
		}
	}

	private void InitTransformerBtnArrow()
	{
		if (this.transformerBtnArrowWindow != null)
		{
			this.UpdateTransformerBtnArrowPosition();
		}
		else
		{
			this.transformerBtnArrowWindow = new GuiWindow();
			this.transformerBtnArrowWindow.SetBackgroundTexture("FrameworkGUI", "leftToRightArrow");
			this.transformerBtnArrowWindow.zOrder = 230;
			this.transformerBtnArrowWindow.ignoreClickEvents = true;
			this.transformerBtnArrowWindow.boundries.set_x((float)(Screen.get_width() / 2 + 160));
			this.transformerBtnArrowWindow.boundries.set_y((float)(Screen.get_height() / 2 - 40));
			this.transformerBtnArrowWindow.isHidden = false;
			AndromedaGui.gui.AddWindow(this.transformerBtnArrowWindow);
			this.transformerBtnArrowDeltaTime = 0f;
		}
	}

	private void InitTransformerItemArrow(float x, float y, string txt)
	{
		if (this.transformerItemArrowWindow != null)
		{
			this.itemPositionX = (float)((Screen.get_width() - 904) / 2) + x + 3f;
			this.itemPositionY = (float)((Screen.get_height() - 539) / 2) + y + 20f;
			this.UpdateTransformerItemArrowPosition();
		}
		else
		{
			this.itemPositionX = (float)((Screen.get_width() - 904) / 2) + x + 3f;
			this.itemPositionY = (float)((Screen.get_height() - 539) / 2) + y + 20f;
			this.transformerItemArrowWindow = new GuiWindow()
			{
				zOrder = 230,
				ignoreClickEvents = true,
				boundries = new Rect((float)(Screen.get_width() / 2), (float)(Screen.get_height() / 2), 150f, 150f),
				isHidden = false
			};
			AndromedaGui.gui.AddWindow(this.transformerItemArrowWindow);
			this.transformerBtnArrowDeltaTime = 0f;
			this.smallArrowTexture = new GuiTexture();
			this.smallArrowTexture.SetTexture("FrameworkGUI", "arrow_tutorial_small");
			this.smallArrowTexture.X = 0f;
			this.smallArrowTexture.Y = 75f;
			this.transformerItemArrowWindow.AddGuiElement(this.smallArrowTexture);
			GuiLabel guiLabel = new GuiLabel()
			{
				boundries = new Rect(40f, 0f, 110f, 150f),
				text = txt,
				FontSize = 13,
				Font = GuiLabel.FontBold,
				TextColor = GuiNewStyleBar.orangeColor,
				Alignment = 4
			};
			this.transformerItemArrowWindow.AddGuiElement(guiLabel);
		}
	}

	public void InitTransformerJump()
	{
		// 
		// Current member / type: System.Void ShipScript::InitTransformerJump()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void InitTransformerJump()
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

	private void InitZoomScroller()
	{
		Vector3 vector3;
		this.zoomSlider = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("scrollZoom_pfb"));
		this.zoomSlider.get_transform().set_localEulerAngles(new Vector3(0f, 90f, 0f));
		this.zoomSlider.get_transform().set_localScale(new Vector3(0.05f, 0.05f, 0.05f));
		this.zoomSlider.SetActive(false);
		this.zoomScroller = this.zoomSlider.get_transform().GetChild(1).get_gameObject();
		Transform _transform = this.zoomScroller.get_transform();
		if (NetworkScript.player.playerBelongings.zoomLevel < 6)
		{
			Vector3 _position = this.zoomScroller.get_transform().get_position();
			float single = _position.x - (float)(6 - NetworkScript.player.playerBelongings.zoomLevel) * this.zoomMoveStep;
			float _position1 = this.zoomScroller.get_transform().get_position().y;
			Vector3 vector31 = this.zoomScroller.get_transform().get_position();
			vector3 = new Vector3(single, _position1, vector31.z);
		}
		else
		{
			Vector3 _position2 = this.zoomScroller.get_transform().get_position();
			float single1 = _position2.x + (float)(NetworkScript.player.playerBelongings.zoomLevel - 6) * this.zoomMoveStep;
			float single2 = this.zoomScroller.get_transform().get_position().y;
			Vector3 vector32 = this.zoomScroller.get_transform().get_position();
			vector3 = new Vector3(single1, single2, vector32.z);
		}
		_transform.set_position(vector3);
	}

	private bool isLevelUP()
	{
		return false;
	}

	private bool IsWeaponOutOfRange()
	{
		bool flag = true;
		bool flag1 = true;
		bool flag2 = true;
		float single = Victor3.Distance(new Victor3(this.p.x, this.p.y, this.p.z), new Victor3(this.p.shootingAt.x, this.p.shootingAt.y, this.p.shootingAt.z));
		WeaponSlot[] weaponSlotArray = this.p.cfg.weaponSlots;
		for (int i = 0; i < (int)weaponSlotArray.Length; i++)
		{
			WeaponSlot weaponSlot = weaponSlotArray[i];
			EWeaponStatus weaponStatus = weaponSlot.get_WeaponStatus();
			if (weaponStatus != 1)
			{
				flag1 = false;
			}
			if (weaponStatus == 3 || weaponSlot.get_WeaponStatus() == 4)
			{
				flag2 = false;
			}
			if (this.maxWeaponRange < weaponSlot.totalShootRange)
			{
				this.maxWeaponRange = weaponSlot.totalShootRange;
			}
			if ((weaponStatus == 3 || weaponStatus == 4) && single < weaponSlot.totalShootRange)
			{
				flag = false;
			}
		}
		if (flag1)
		{
			NetworkScript.spaceLabelManager.AddMessage(GuiNewStyleBar.redColor, StaticData.Translate("key_space_label_nowaepon"), this.p, 40);
		}
		else if (flag2)
		{
			NetworkScript.spaceLabelManager.AddMessage(GuiNewStyleBar.redColor, StaticData.Translate("key_space_label_noammo"), this.p, 60);
		}
		else if (flag)
		{
			NetworkScript.spaceLabelManager.AddMessage(GuiNewStyleBar.orangeColor, StaticData.Translate("key_space_label_target_outrange"), this.p);
			this.ShowOutOfRangeIndication(this.maxWeaponRange);
		}
		this.weaponStatusCheckDelay = 2.5f;
		return flag;
	}

	private void LateUpdate()
	{
		if (this.zoomSlider != null && this.zoomSlider.get_activeSelf())
		{
			Transform _transform = this.zoomSlider.get_transform();
			Vector3 _position = Camera.get_main().get_transform().get_position();
			Vector3 vector3 = Camera.get_main().get_transform().get_position();
			Vector3 _position1 = Camera.get_main().get_transform().get_position();
			_transform.set_position(new Vector3(_position.x - 0.2f, vector3.y - 25f, _position1.z + 8f));
		}
		if (this.p != null && this.p.isRemoved && !this.isRulingLocalPlayer)
		{
			return;
		}
		if (this.p != null)
		{
			this.p.playerData != null;
		}
		if (this.p != null && this.p.isAlive && this.p.playerData != null && this.p.playerData.state != 80)
		{
			if (this.myShipBarBody != null)
			{
				this.myShipBarBody.get_transform().set_position(new Vector3(this.p.x, this.p.y, this.p.z + 2.8f));
				float single = (float)this.p.cfg.hitPoints / (float)this.p.cfg.hitPointsMax;
				float single1 = (float)this.p.cfg.shield / (float)this.p.cfg.shieldMax;
				float single2 = (float)this.p.cfg.criticalEnergy / (float)this.p.cfg.criticalEnergyMax;
				this.myShipBarBlue.get_transform().set_position(new Vector3(this.p.x - 1.392f, 0.2477f, this.p.z + 2.8f + 0.2619f));
				this.myShipBarBlue.get_transform().set_localScale(new Vector3(Math.Min(single1, 1f), 1f, 1f));
				this.myShipBarGreen.get_transform().set_position(new Vector3(this.p.x - 1.392f, 0.1158f, this.p.z + 2.8f + 0.0282f));
				this.myShipBarGreen.get_transform().set_localScale(new Vector3(Math.Min(single, 1f), 1f, 1f));
				this.myShipBarEnergy.get_transform().set_position(new Vector3(this.p.x - 1.392f, -0.0199f, this.p.z + 2.8f - 0.2034f));
				this.myShipBarEnergy.get_transform().set_localScale(new Vector3(Math.Min(single2, 1f), 1f, 1f));
			}
		}
		else if (this.myShipBarBody != null)
		{
			Object.Destroy(this.myShipBarBody);
			Object.Destroy(this.myShipBarBlue);
			Object.Destroy(this.myShipBarGreen);
			Object.Destroy(this.myShipBarEnergy);
			this.myShipBarBody = null;
			this.myShipBarBlue = null;
			this.myShipBarGreen = null;
			this.myShipBarEnergy = null;
		}
	}

	public void LevelUp()
	{
		GameObject gameObject = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("LevelUP_pfb"));
		gameObject.get_transform().set_position(new Vector3(this.p.x, 1.5f, this.p.z));
		gameObject.GetComponent<LevelUpAnimationScript>().target = this.p;
		NetworkScript.spaceLabelManager.AddMessage(GuiNewStyleBar.orangeColor, StaticData.Translate("key_space_label_level_up"), this.p);
	}

	private void ManageActionKey()
	{
		if (this.directionArrow != null && this.p.cfg.shield >= (float)this.p.cfg.shieldMax * 0.1f)
		{
			this.StartSpeedBoost();
			return;
		}
		if (this.selectedObject != null)
		{
			if (this.selectedObject.get_IsPoP())
			{
				if (this.p.CanShootThisTarget(this.selectedObject))
				{
					if (!this.p.isShooting || this.p.shootingAt == null || this.p.shootingAt.neighbourhoodId != this.selectedObject.neighbourhoodId)
					{
						this.StartShooting();
					}
					else
					{
						this.StopShooting();
					}
				}
			}
			else if (this.selectedObject is Mineral)
			{
				if (this.p.miningState == 0)
				{
					this.StartMining();
					this.isStartMiningNeeded = true;
				}
				else if (this.p.miningMineralNbId == this.selectedObject.neighbourhoodId)
				{
					this.AbortMining();
					this.isStartMiningNeeded = false;
				}
				else
				{
					this.AbortMining();
					this.isStartMiningNeeded = false;
					this.StartMining();
					this.isStartMiningNeeded = true;
				}
			}
		}
	}

	private void ManageAutoMiner()
	{
		// 
		// Current member / type: System.Void ShipScript::ManageAutoMiner()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void ManageAutoMiner()
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..( , Int32 , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:строка 78
		//    в ..( , Int32& , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:строка 33
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 57
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(IfStatement ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 359
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 55
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(IfStatement ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 359
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 55
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(IfStatement ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 359
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 55
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
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

	private void ManageAutoMiningOnSelected()
	{
		ShipScript _deltaTime = this;
		_deltaTime.autoMinerTimer = _deltaTime.autoMinerTimer - Time.get_deltaTime();
		if (this.autoMinerTimer <= 0f)
		{
			if (this.selectedObject != null && this.isStartMiningNeeded && this.selectedObject is MineralEx && Vector3.Distance(new Vector3(this.p.x, this.p.y, this.p.z), new Vector3(this.selectedObject.x, this.selectedObject.y, this.selectedObject.z)) <= ShipScript.MINING_RANGE)
			{
				this.StartMining();
			}
			this.autoMinerTimer = 0.5f;
		}
	}

	private void ManageHotKeys()
	{
		if (!this.isHotKeysActive || this.isChangingKey || this.kb == null)
		{
			return;
		}
		if (this.kb.IsCommandUsed(KeyboardCommand.QuickChat, 0) && NetworkScript.player.vessel.galaxy.get_galaxyId() != 1000)
		{
			this.OpenMainMenuWondow(20);
		}
		if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 20)
		{
			if (Input.GetKeyDown(49))
			{
				this.UseSocialInteraction(1);
				return;
			}
			if (Input.GetKeyDown(50))
			{
				this.UseSocialInteraction(2);
				return;
			}
			if (Input.GetKeyDown(51))
			{
				this.UseSocialInteraction(3);
				return;
			}
			if (Input.GetKeyDown(52))
			{
				this.UseSocialInteraction(4);
				return;
			}
			if (Input.GetKeyDown(53))
			{
				this.UseSocialInteraction(5);
				return;
			}
			if (Input.GetKeyDown(54))
			{
				this.UseSocialInteraction(6);
				return;
			}
			if (Input.GetKeyDown(55))
			{
				this.UseSocialInteraction(7);
				return;
			}
			if (Input.GetKeyDown(56))
			{
				this.UseSocialInteraction(8);
				return;
			}
			if (Input.GetKeyDown(57))
			{
				this.UseSocialInteraction(9);
				return;
			}
			if (Input.GetKeyDown(48))
			{
				this.UseSocialInteraction(0);
				return;
			}
			if (Input.GetKeyDown(45))
			{
				this.OpenMainMenuWondow(21);
				return;
			}
		}
		if (this.kb.IsCommandUsed(KeyboardCommand.ShowFPSMeter, 0))
		{
			this.ShowFPSMeter = !this.ShowFPSMeter;
			if (!this.ShowFPSMeter)
			{
				AndromedaGui.mainWnd.fpsMeter.text = string.Empty;
			}
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.SelfSelect, 0))
		{
			this.ManageSelectObjectRequest(NetworkScript.clientSideClientsList.get_Item(this.p.playerId).gameObject, NetworkScript.clientSideClientsList.get_Item(this.p.playerId).vessel);
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.SelectPartyTwo, 0))
		{
			if (this.partyMemberOneId > (long)0 && NetworkScript.clientSideClientsList.ContainsKey(this.partyMemberOneId))
			{
				this.ManageSelectObjectRequest(NetworkScript.clientSideClientsList.get_Item(this.partyMemberOneId).gameObject, NetworkScript.clientSideClientsList.get_Item(this.partyMemberOneId).vessel);
			}
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.SelectPartyThree, 0))
		{
			if (this.partyMemberTwoId > (long)0 && NetworkScript.clientSideClientsList.ContainsKey(this.partyMemberTwoId))
			{
				this.ManageSelectObjectRequest(NetworkScript.clientSideClientsList.get_Item(this.partyMemberTwoId).gameObject, NetworkScript.clientSideClientsList.get_Item(this.partyMemberTwoId).vessel);
			}
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.SelectPartyFour, 0))
		{
			if (this.partyMemberThreeId > (long)0 && NetworkScript.clientSideClientsList.ContainsKey(this.partyMemberThreeId))
			{
				this.ManageSelectObjectRequest(NetworkScript.clientSideClientsList.get_Item(this.partyMemberThreeId).gameObject, NetworkScript.clientSideClientsList.get_Item(this.partyMemberThreeId).vessel);
			}
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.NextObject, 0))
		{
			this.SelectNextMineral();
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.SwitchQuest, 0))
		{
			QuestTrackerWindow.ScrollQuests();
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.SkillOne, 0))
		{
			this.StartCastingSkill(0);
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.SkillTwo, 0))
		{
			this.StartCastingSkill(1);
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.SkillThree, 0))
		{
			this.StartCastingSkill(2);
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.SkillFour, 0))
		{
			this.StartCastingSkill(3);
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.SkillFive, 0))
		{
			this.StartCastingSkill(4);
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.SkillSix, 0))
		{
			this.StartCastingSkill(5);
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.SkillSeven, 0))
		{
			this.StartCastingSkill(6);
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.SkillEight, 0))
		{
			this.StartCastingSkill(7);
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.SkillNine, 0))
		{
			this.StartCastingSkill(8);
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.NextTarget, 0))
		{
			this.ManageNextTarget();
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.Fusion, 0))
		{
			this.OpenMainMenuWondow(0);
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.Skills, 0))
		{
			this.OpenMainMenuWondow(2);
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.PvP, 0) && TargetingWnd.IsTargetingWindowAllowed && NetworkScript.player.playerBelongings.playerLevel >= 8)
		{
			this.OpenMainMenuWondow(15);
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.Chat, 0))
		{
			MainScreenWindow.OnChatClicked(null);
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.ShipConfig, 0))
		{
			this.OpenMainMenuWondow(1);
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.UniverseMap, 0) && TargetingWnd.IsTargetingWindowAllowed && (NetworkScript.player.pvpGame == null || NetworkScript.player.pvpGame.state == 2) && NetworkScript.player.playerBelongings.playerLevel >= 7)
		{
			this.OpenMainMenuWondow(7);
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.Guild, 0) && NetworkScript.player.playerBelongings.playerLevel >= 9)
		{
			this.OpenMainMenuWondow(18);
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.NovaShop, 0))
		{
			this.OpenMainMenuWondow(11);
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.Ranking, 0))
		{
			this.OpenMainMenuWondow(8);
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.Profile, 0))
		{
			this.OpenMainMenuWondow(17);
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.Quests, 0))
		{
			QuestTrackerWindow.SwitchObjectiveInfo();
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.Transformer, 0) && TargetingWnd.IsTargetingWindowAllowed && NetworkScript.player.playerBelongings.playerLevel >= 30 && (NetworkScript.player.pvpGame == null || NetworkScript.player.pvpGame.state == 2))
		{
			this.OpenMainMenuWondow(22);
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.Feedback, 0) && playWebGame.GAME_TYPE != "ru")
		{
			if (AndromedaGui.mainWnd != null)
			{
				AndromedaGui.mainWnd.TakeScreenShot(null);
			}
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.Gifts, 0) && NetworkScript.player.playerBelongings.playerLevel >= 10)
		{
			this.OpenMainMenuWondow(33);
		}
		else if (Input.GetKeyDown(27))
		{
			if (!this.waitingSpaceCoordinates)
			{
				if (QuestInfoWindow.Close())
				{
					return;
				}
				if (AndromedaGui.gui.activeToolTipId != -1)
				{
					AndromedaGui.gui.RemoveWindow(AndromedaGui.gui.activeToolTipId);
					AndromedaGui.gui.activeToolTipId = -1;
					if (AndromedaGui.gui.activeTooltipCloseAction != null)
					{
						AndromedaGui.gui.activeTooltipCloseAction.Invoke();
						AndromedaGui.gui.activeTooltipCloseAction = null;
					}
				}
				else if (!AndromedaGui.mainWnd.hasMenuOpen)
				{
					if (this.selectedObject == null)
					{
						this.OpenMainMenuWondow(6);
					}
					else
					{
						Object.Destroy(this._lock);
						this.selectedObject = null;
						if (this.p.selectedPoPnbId != 0)
						{
							playWebGame.udp.ExecuteCommand(49, new UniversalTransportContainer(), 56);
							this.p.selectedPoPnbId = 0;
						}
					}
				}
				else if (AndromedaGui.mainWnd.activWindowIndex == 7 && __UniverseMap.subSection != 0)
				{
					((__UniverseMap)AndromedaGui.mainWnd.activeWindow).OnBackBtnClicked(null);
				}
				else if (AndromedaGui.mainWnd.activWindowIndex == 18 && GuildWindow.subSection != 0)
				{
					((GuildWindow)AndromedaGui.mainWnd.activeWindow).OnBackBtnClicked(null);
				}
				else if (AndromedaGui.mainWnd.activWindowIndex != 2)
				{
					AndromedaGui.mainWnd.CloseActiveWindow();
				}
				else
				{
					((__SkillsTree)AndromedaGui.mainWnd.activeWindow).OnEscKeyPressed();
				}
			}
			else
			{
				this.targetLocation = null;
				this.p.castingSkillSlot = -1;
				this.waitingSpaceCoordinates = false;
				if (this.freeSpaceSkillIndicator != null)
				{
					Object.Destroy(this.freeSpaceSkillIndicator);
				}
				if (ShipScript.skillTarget != null)
				{
					Object.Destroy(ShipScript.skillTarget);
				}
			}
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.UseKey, 0))
		{
			if (this.popUpAction != null)
			{
				this.popUpAction.Invoke(null);
			}
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.ActionKey, 0))
		{
			this.ManageActionKey();
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.ZoomIn, 0))
		{
			this.ZoomCamera(0);
		}
		else if (this.kb.IsCommandUsed(KeyboardCommand.ZoomOut, 0))
		{
			this.ZoomCamera(1);
		}
	}

	private void ManageMoveByKeyboard()
	{
		Object.DestroyObject(ShipScript.mapTarget);
		float single = 0f;
		float single1 = 0f;
		if (this.keyUp)
		{
			single1 = 1f;
		}
		if (this.keyDown)
		{
			single1 = -1f;
		}
		if (this.keyLeft)
		{
			single = -1f;
		}
		if (this.keyRight)
		{
			single = 1f;
		}
		this.keyUp = this.keyUpNew;
		this.keyDown = this.keyDownNew;
		this.keyLeft = this.keyLeftNew;
		this.keyRight = this.keyRightNew;
		Vector3 vector3 = new Vector3(this.p.x, this.p.y, this.p.z);
		if (!(this.keyUp ^ this.keyDown))
		{
			vector3.z = vector3.z + single1;
		}
		else if (!this.keyUp)
		{
			vector3.z = vector3.z - 1000f;
		}
		else
		{
			vector3.z = vector3.z + 1000f;
		}
		if (!(this.keyLeft ^ this.keyRight))
		{
			vector3.x = vector3.x + single;
		}
		else if (!this.keyLeft)
		{
			vector3.x = vector3.x + 1000f;
		}
		else
		{
			vector3.x = vector3.x - 1000f;
		}
		this.ManageMoveRequest(vector3, true, false);
	}

	private void ManageMoveOrSelectRequest()
	{
		// 
		// Current member / type: System.Void ShipScript::ManageMoveOrSelectRequest()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void ManageMoveOrSelectRequest()
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..( , Int32 , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:строка 78
		//    в ..( , Int32& , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:строка 33
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 57
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(IfStatement ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 359
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 55
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(IfStatement ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 359
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 55
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
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

	public void ManageMoveRequest(Vector3 wp2, bool isKeyboard, bool canBoost)
	{
		// 
		// Current member / type: System.Void ShipScript::ManageMoveRequest(UnityEngine.Vector3,System.Boolean,System.Boolean)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void ManageMoveRequest(UnityEngine.Vector3,System.Boolean,System.Boolean)
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..( , Int32 , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:строка 78
		//    в ..( , Int32& , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:строка 33
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 57
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(IfStatement ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 359
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 55
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
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

	private void ManageNextTarget()
	{
		PlayerObjectPhysics playerObjectPhysic;
		float distance;
		ShipScript.ClosestTarget closestTarget;
		ShipScript.<ManageNextTarget>c__AnonStorey6B variable = null;
		ShipScript.<ManageNextTarget>c__AnonStorey6A variable1 = null;
		bool flag = false;
		int num = 0;
		if (this.previousPosition != Vector2.get_zero() && (this.previousPosition.x != this.p.x || this.previousPosition.y != this.p.z))
		{
			flag = true;
		}
		this.previousPosition = new Vector2(this.p.x, this.p.y);
		this.CheckForRemoveFromNextTargetList(this.shootingPVPs);
		this.CheckForRemoveFromNextTargetList(this.shootingPVEs);
		this.CheckForRemoveFromNextTargetList(this.passivePVPs);
		this.CheckForRemoveFromNextTargetList(this.passivePVEs);
		this.CheckForRemoveFromNextTargetList(this.extractionPoints);
		if (!this.p.isShooting || this.p.shootingAt == null)
		{
			playerObjectPhysic = null;
			this.shootingAtTarget = null;
		}
		else
		{
			if (this.p.shootingAt is PlayerObjectPhysics)
			{
				playerObjectPhysic = (PlayerObjectPhysics)this.p.shootingAt;
				float single = GameObjectPhysics.GetDistance(this.p.x, this.p.shootingAt.x, this.p.z, this.p.shootingAt.z);
				closestTarget = new ShipScript.ClosestTarget()
				{
					DistanceToObj = single,
					targetObject = playerObjectPhysic,
					neighbourhoodId = playerObjectPhysic.neighbourhoodId
				};
				this.shootingAtTarget = closestTarget;
			}
			if (this.p.shootingAt is ExtractionPoint)
			{
				float distance1 = GameObjectPhysics.GetDistance(this.p.x, this.p.shootingAt.x, this.p.z, this.p.shootingAt.z);
				closestTarget = new ShipScript.ClosestTarget()
				{
					DistanceToObj = distance1,
					targetObject = this.p.shootingAt,
					neighbourhoodId = this.p.shootingAt.neighbourhoodId
				};
				this.shootingAtTarget = closestTarget;
			}
		}
		IEnumerator<GameObjectPhysics> enumerator = this.comm.gameObjects.get_Values().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				GameObjectPhysics current = enumerator.get_Current();
				if (!(current is PlayerObjectPhysics))
				{
					if (!(current is ExtractionPoint))
					{
						continue;
					}
					distance = GameObjectPhysics.GetDistance(this.p.x, current.x, this.p.z, current.z);
					if (distance > ShipScript.NEXT_TARGET_SHORTCUT_RANGE || this.extractionPoints.Exists(new Predicate<ShipScript.ClosestTarget>(variable, (ShipScript.ClosestTarget n) => n.neighbourhoodId == this.gop.neighbourhoodId)))
					{
						continue;
					}
					List<ShipScript.ClosestTarget> list = this.extractionPoints;
					closestTarget = new ShipScript.ClosestTarget()
					{
						DistanceToObj = distance,
						targetObject = current,
						neighbourhoodId = current.neighbourhoodId
					};
					list.Add(closestTarget);
					num++;
				}
				else
				{
					PlayerObjectPhysics playerObjectPhysic1 = (PlayerObjectPhysics)current;
					if (playerObjectPhysic1.isAlive && playerObjectPhysic1.playerId != this.p.playerId && !playerObjectPhysic1.isInStealthMode)
					{
						distance = GameObjectPhysics.GetDistance(this.p.x, playerObjectPhysic1.x, this.p.z, playerObjectPhysic1.z);
						if (distance <= ShipScript.NEXT_TARGET_SHORTCUT_RANGE)
						{
							if (!playerObjectPhysic1.get_IsPve())
							{
								if (this.p.pvpState == 3)
								{
									if (playerObjectPhysic1.teamNumber == this.p.teamNumber)
									{
										continue;
									}
								}
								else if (playerObjectPhysic1.fractionId == this.p.fractionId)
								{
									continue;
								}
								if (playerObjectPhysic1.shootingAt != null && playerObjectPhysic1.shootingAt.neighbourhoodId == this.p.neighbourhoodId)
								{
									if (!this.shootingPVPs.Exists(new Predicate<ShipScript.ClosestTarget>(variable1, (ShipScript.ClosestTarget n) => n.neighbourhoodId == this.pop.neighbourhoodId)))
									{
										List<ShipScript.ClosestTarget> list1 = this.shootingPVPs;
										closestTarget = new ShipScript.ClosestTarget()
										{
											DistanceToObj = distance,
											targetObject = playerObjectPhysic1,
											neighbourhoodId = playerObjectPhysic1.neighbourhoodId
										};
										list1.Add(closestTarget);
										num++;
									}
								}
								else if (!this.passivePVPs.Exists(new Predicate<ShipScript.ClosestTarget>(variable1, (ShipScript.ClosestTarget n) => n.neighbourhoodId == this.pop.neighbourhoodId)))
								{
									List<ShipScript.ClosestTarget> list2 = this.passivePVPs;
									closestTarget = new ShipScript.ClosestTarget()
									{
										DistanceToObj = distance,
										targetObject = playerObjectPhysic1,
										neighbourhoodId = playerObjectPhysic1.neighbourhoodId
									};
									list2.Add(closestTarget);
									num++;
								}
							}
							else if (playerObjectPhysic1.shootingAt != null && playerObjectPhysic1.shootingAt.neighbourhoodId == this.p.neighbourhoodId)
							{
								if (!this.shootingPVEs.Exists(new Predicate<ShipScript.ClosestTarget>(variable1, (ShipScript.ClosestTarget n) => n.neighbourhoodId == this.pop.neighbourhoodId)))
								{
									List<ShipScript.ClosestTarget> list3 = this.shootingPVEs;
									closestTarget = new ShipScript.ClosestTarget()
									{
										DistanceToObj = distance,
										targetObject = playerObjectPhysic1,
										neighbourhoodId = playerObjectPhysic1.neighbourhoodId
									};
									list3.Add(closestTarget);
									num++;
								}
							}
							else if (!this.passivePVEs.Exists(new Predicate<ShipScript.ClosestTarget>(variable1, (ShipScript.ClosestTarget n) => n.neighbourhoodId == this.pop.neighbourhoodId)))
							{
								List<ShipScript.ClosestTarget> list4 = this.passivePVEs;
								closestTarget = new ShipScript.ClosestTarget()
								{
									DistanceToObj = distance,
									targetObject = playerObjectPhysic1,
									neighbourhoodId = playerObjectPhysic1.neighbourhoodId
								};
								list4.Add(closestTarget);
								num++;
							}
						}
					}
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
		List<ShipScript.ClosestTarget> list5 = this.shootingPVPs;
		if (ShipScript.<>f__am$cacheB2 == null)
		{
			ShipScript.<>f__am$cacheB2 = new Func<ShipScript.ClosestTarget, float>(null, (ShipScript.ClosestTarget n) => n.DistanceToObj);
		}
		Enumerable.OrderBy<ShipScript.ClosestTarget, float>(list5, ShipScript.<>f__am$cacheB2);
		List<ShipScript.ClosestTarget> list6 = this.shootingPVEs;
		if (ShipScript.<>f__am$cacheB3 == null)
		{
			ShipScript.<>f__am$cacheB3 = new Func<ShipScript.ClosestTarget, float>(null, (ShipScript.ClosestTarget n) => n.DistanceToObj);
		}
		Enumerable.OrderBy<ShipScript.ClosestTarget, float>(list6, ShipScript.<>f__am$cacheB3);
		List<ShipScript.ClosestTarget> list7 = this.passivePVPs;
		if (ShipScript.<>f__am$cacheB4 == null)
		{
			ShipScript.<>f__am$cacheB4 = new Func<ShipScript.ClosestTarget, float>(null, (ShipScript.ClosestTarget n) => n.DistanceToObj);
		}
		Enumerable.OrderBy<ShipScript.ClosestTarget, float>(list7, ShipScript.<>f__am$cacheB4);
		List<ShipScript.ClosestTarget> list8 = this.passivePVEs;
		if (ShipScript.<>f__am$cacheB5 == null)
		{
			ShipScript.<>f__am$cacheB5 = new Func<ShipScript.ClosestTarget, float>(null, (ShipScript.ClosestTarget n) => n.DistanceToObj);
		}
		Enumerable.OrderBy<ShipScript.ClosestTarget, float>(list8, ShipScript.<>f__am$cacheB5);
		List<ShipScript.ClosestTarget> list9 = this.extractionPoints;
		if (ShipScript.<>f__am$cacheB6 == null)
		{
			ShipScript.<>f__am$cacheB6 = new Func<ShipScript.ClosestTarget, float>(null, (ShipScript.ClosestTarget n) => n.DistanceToObj);
		}
		Enumerable.OrderBy<ShipScript.ClosestTarget, float>(list9, ShipScript.<>f__am$cacheB6);
		this.NearObjects.Clear();
		if (this.shootingAtTarget != null)
		{
			this.NearObjects.Add(this.shootingAtTarget);
		}
		this.NearObjects.AddRange(this.shootingPVPs);
		this.NearObjects.AddRange(this.shootingPVEs);
		this.NearObjects.AddRange(this.passivePVPs);
		this.NearObjects.AddRange(this.passivePVEs);
		this.NearObjects.AddRange(this.extractionPoints);
		if (this.NearObjects.get_Count() != 0)
		{
			if (this.CurrentClosestSelectedTarget >= this.NearObjects.get_Count() || this.NearObjects.get_Count() > 1 && num > 1 && flag || this.selectedObject == null)
			{
				this.CurrentClosestSelectedTarget = 0;
			}
			if (this.selectedObject != null && this.NearObjects.get_Count() > 2 && this.NearObjects.get_Item(this.CurrentClosestSelectedTarget).targetObject.neighbourhoodId == this.selectedObject.neighbourhoodId)
			{
				this.DeselectCurrentObject();
				ShipScript currentClosestSelectedTarget = this;
				currentClosestSelectedTarget.CurrentClosestSelectedTarget = currentClosestSelectedTarget.CurrentClosestSelectedTarget + 1;
			}
			this.ManageSelectObjectRequest((GameObject)this.NearObjects.get_Item(this.CurrentClosestSelectedTarget).targetObject.gameObject, this.NearObjects.get_Item(this.CurrentClosestSelectedTarget).targetObject);
			ShipScript shipScript = this;
			shipScript.CurrentClosestSelectedTarget = shipScript.CurrentClosestSelectedTarget + 1;
		}
		else
		{
			this.CurrentClosestSelectedTarget = 0;
			this.DeselectCurrentObject();
		}
	}

	public void ManagePartyMemberArrow()
	{
		this.ResetPartyArrow();
		if (NetworkScript.party == null || NetworkScript.party.members.get_Count() <= 1)
		{
			return;
		}
		foreach (PartyMemberClientSide member in NetworkScript.party.members)
		{
			if (member.playerId != this.p.playerId)
			{
				if (this.partyArrowOne == null)
				{
					if (NetworkScript.clientSideClientsList.ContainsKey(member.playerId))
					{
						this.partyArrowOne = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("PartyArrow1_pfb"));
						PartyArrowScript component = this.partyArrowOne.GetComponent<PartyArrowScript>();
						component.me = base.get_gameObject();
						component.target = NetworkScript.clientSideClientsList.get_Item(member.playerId).gameObject;
						this.partyMemberOneId = member.playerId;
					}
					else if (!NetworkScript.partyMembersInfo.ContainsKey(member.playerId) || !(NetworkScript.partyMembersInfo.get_Item(member.playerId).lastUpdateTime.AddMilliseconds(1500) > StaticData.now))
					{
						this.partyMemberOneId = member.playerId;
					}
					else
					{
						this.partyArrowOne = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("PartyArrow1_pfb"));
						PartyArrowScript _gameObject = this.partyArrowOne.GetComponent<PartyArrowScript>();
						_gameObject.me = base.get_gameObject();
						_gameObject.memberInfo = NetworkScript.partyMembersInfo.get_Item(member.playerId);
						this.partyMemberOneId = member.playerId;
					}
				}
				else if (this.partyArrowTwo != null)
				{
					if (this.partyArrowThree != null)
					{
						continue;
					}
					if (NetworkScript.clientSideClientsList.ContainsKey(member.playerId))
					{
						this.partyArrowThree = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("PartyArrow3_pfb"));
						PartyArrowScript item = this.partyArrowThree.GetComponent<PartyArrowScript>();
						item.me = base.get_gameObject();
						item.target = NetworkScript.clientSideClientsList.get_Item(member.playerId).gameObject;
						this.partyMemberThreeId = member.playerId;
					}
					else if (!NetworkScript.partyMembersInfo.ContainsKey(member.playerId) || !(NetworkScript.partyMembersInfo.get_Item(member.playerId).lastUpdateTime.AddMilliseconds(1500) > StaticData.now))
					{
						this.partyMemberThreeId = member.playerId;
					}
					else
					{
						this.partyArrowThree = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("PartyArrow3_pfb"));
						PartyArrowScript partyArrowScript = this.partyArrowThree.GetComponent<PartyArrowScript>();
						partyArrowScript.me = base.get_gameObject();
						partyArrowScript.memberInfo = NetworkScript.partyMembersInfo.get_Item(member.playerId);
						this.partyMemberThreeId = member.playerId;
					}
				}
				else if (NetworkScript.clientSideClientsList.ContainsKey(member.playerId))
				{
					this.partyArrowTwo = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("PartyArrow2_pfb"));
					PartyArrowScript component1 = this.partyArrowTwo.GetComponent<PartyArrowScript>();
					component1.me = base.get_gameObject();
					component1.target = NetworkScript.clientSideClientsList.get_Item(member.playerId).gameObject;
					this.partyMemberTwoId = member.playerId;
				}
				else if (!NetworkScript.partyMembersInfo.ContainsKey(member.playerId) || !(NetworkScript.partyMembersInfo.get_Item(member.playerId).lastUpdateTime.AddMilliseconds(1500) > StaticData.now))
				{
					this.partyMemberTwoId = member.playerId;
				}
				else
				{
					this.partyArrowTwo = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("PartyArrow2_pfb"));
					PartyArrowScript _gameObject1 = this.partyArrowTwo.GetComponent<PartyArrowScript>();
					_gameObject1.me = base.get_gameObject();
					_gameObject1.memberInfo = NetworkScript.partyMembersInfo.get_Item(member.playerId);
					this.partyMemberTwoId = member.playerId;
				}
			}
		}
	}

	private void ManageRotation(float dt)
	{
		if (this.p.rotationState == 0)
		{
			return;
		}
		if (this.p.rotationState == 1)
		{
			float single = dt * this.p.angularVelocity;
			base.get_gameObject().get_transform().Rotate(0f, single, 0f);
			PlayerObjectPhysics playerObjectPhysic = this.p;
			playerObjectPhysic.rotationDone = playerObjectPhysic.rotationDone + Mathf.Abs(single);
			if (this.p.rotationDone >= Mathf.Abs(this.p.neededRotation))
			{
				Quaternion _rotation = base.get_gameObject().get_transform().get_rotation();
				float item = _rotation.get_eulerAngles().get_Item(1);
				float single1 = this.p.destinationAngle - item;
				if (single1 >= 360f)
				{
					single1 = single1 - 360f;
				}
				base.get_gameObject().get_transform().Rotate(new Vector3(0f, single1, 0f));
				this.p.rotationState = 0;
				this.p.rotationDone = 0f;
			}
		}
	}

	private void ManagerRightClickRequest()
	{
		this.GetMouseToWorldPosition();
		Collider[] rayCastedColliders = this.GetRayCastedColliders();
		PveScript collidingPve = this.GetCollidingPve(rayCastedColliders);
		ShipScript collidingEnemyPlayer = this.GetCollidingEnemyPlayer(rayCastedColliders);
		ExtractionPointScript collidingExtractionPoint = this.GetCollidingExtractionPoint(rayCastedColliders);
		MineralScript collidingMineralNew = this.GetCollidingMineralNew(rayCastedColliders);
		if (collidingPve != null && (this.selectedObject == null || this.selectedObject.neighbourhoodId != collidingPve.pve.neighbourhoodId))
		{
			this.isAbortMiningNeeded = false;
			this.ManageSelectObjectRequest(collidingPve.get_gameObject(), collidingPve.pve);
			this.StartActing();
			return;
		}
		if (collidingEnemyPlayer != null && !collidingEnemyPlayer.p.isInStealthMode && (this.selectedObject == null || this.selectedObject.neighbourhoodId != collidingEnemyPlayer.p.neighbourhoodId))
		{
			this.isAbortMiningNeeded = false;
			this.ManageSelectObjectRequest(collidingEnemyPlayer.get_gameObject(), collidingEnemyPlayer.p);
			this.StartActing();
			return;
		}
		if (collidingExtractionPoint != null && (this.selectedObject == null || this.selectedObject.neighbourhoodId != collidingExtractionPoint.extractionPoint.neighbourhoodId))
		{
			this.isAbortMiningNeeded = false;
			this.ManageSelectObjectRequest(collidingExtractionPoint.get_gameObject(), collidingExtractionPoint.extractionPoint);
			this.StartActing();
			return;
		}
		if (!(collidingMineralNew != null) || this.selectedObject != null && this.selectedObject.neighbourhoodId == collidingMineralNew.mineral.neighbourhoodId)
		{
			return;
		}
		this.isAbortMiningNeeded = true;
		this.ManageSelectObjectRequest(collidingMineralNew.get_gameObject(), collidingMineralNew.mineral);
		this.StartActing();
	}

	private void ManageSelectedObjectActing()
	{
		if (this.selectedObject == null && !this.IsActing)
		{
			return;
		}
		if (this.selectedObject == null && this.IsActing)
		{
			this.StopActing();
			return;
		}
		if (Input.GetMouseButtonDown(1))
		{
			if (this.selectedObject is MineralEx)
			{
				this.StopActing();
			}
			this.StartActing();
			return;
		}
		if (Input.GetMouseButton(1) && !this.IsActing)
		{
			this.StartActing();
			return;
		}
		if (!Input.GetMouseButton(1) && !this.IsActing && this.unsuccessfulMiningGoesOn)
		{
			this.unsuccessfulMiningGoesOn = false;
			Object.Destroy(this.outOfMiningRangeMarker);
			this.outOfMiningRangeMarker = null;
		}
	}

	public void ManageSelectObjectRequest(GameObject go2select, GameObjectPhysics object2select)
	{
		// 
		// Current member / type: System.Void ShipScript::ManageSelectObjectRequest(UnityEngine.GameObject,GameObjectPhysics)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void ManageSelectObjectRequest(UnityEngine.GameObject,GameObjectPhysics)
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..( , Int32 , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:строка 78
		//    в ..( , Int32& , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:строка 33
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 57
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(IfStatement ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 359
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 55
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(IfStatement ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 359
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 55
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
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

	private void ManageShipCollision(ShipScript sc)
	{
		float single = (sc.p.speed * sc.shipConfiguration.mass + this.p.speed * this.shipConfiguration.mass) / 80f;
		if (!this.DecreaseHitPoints(single, null))
		{
			return;
		}
		if ((double)single > (double)this.shipConfiguration.hitPointsMax * 0.06)
		{
			this.p.speed = 0f;
			this.p.moveState = 0;
			this.p.rotationState = 0;
		}
	}

	private void ManageSocialWindowTouch()
	{
		if (this.isShipSelected && (int)Input.get_touches().Length > 0)
		{
			ShipScript _deltaTime = this;
			_deltaTime.socialWindowTimer = _deltaTime.socialWindowTimer - Time.get_deltaTime();
			ShipScript shipScript = this;
			Vector2 vector2 = shipScript.moveOffset;
			Touch touch = Input.GetTouch(0);
			shipScript.moveOffset = vector2 + this.VectorAbs(touch.get_deltaPosition());
			if (this.socialWindowTimer <= 0f && this.moveOffset.x < 10f && this.moveOffset.y < 10f)
			{
				Vector3 mouseToWorldPosition = this.GetMouseToWorldPosition();
				ShipScript collidingEnemyShip = this.GetCollidingEnemyShip(Physics.OverlapSphere(mouseToWorldPosition, 0.01f));
				if (collidingEnemyShip != null && (object)collidingEnemyShip.p == (object)this.p)
				{
					this.OpenMainMenuWondow(20);
					this.DeselectCurrentObject();
					this.isShipSelected = false;
				}
				this.moveOffset = Vector2.get_zero();
				this.socialWindowTimer = 0.6f;
			}
		}
		else if (this.isShipSelected)
		{
			this.socialWindowTimer = 0.6f;
			this.moveOffset = Vector2.get_zero();
		}
		if ((int)Input.get_touches().Length == 0 && this.moveOffset != Vector2.get_zero())
		{
			this.socialWindowTimer = 0.6f;
			this.moveOffset = Vector2.get_zero();
		}
	}

	private void ManageWeaponsToggle()
	{
		if (Input.GetKeyDown(49))
		{
			this.TryToggleWeapon(0);
		}
		else if (Input.GetKeyDown(50))
		{
			this.TryToggleWeapon(1);
		}
		else if (Input.GetKeyDown(51))
		{
			this.TryToggleWeapon(2);
		}
		else if (Input.GetKeyDown(52))
		{
			this.TryToggleWeapon(3);
		}
		else if (Input.GetKeyDown(53))
		{
			this.TryToggleWeapon(4);
		}
		else if (Input.GetKeyDown(54))
		{
			this.TryToggleWeapon(5);
		}
		else if (Input.GetKeyDown(55))
		{
			this.TryToggleWeapon(6);
		}
		else if (Input.GetKeyDown(56))
		{
			this.TryToggleWeapon(7);
		}
		else if (Input.GetKeyDown(57))
		{
			this.TryToggleWeapon(8);
		}
		else if (Input.GetKeyDown(48))
		{
			this.TryToggleWeapon(-1);
		}
		else if (Input.GetKeyDown(32))
		{
			for (int i = 0; i < (int)this.shipConfiguration.weaponSlots.Length; i++)
			{
				if (this.shipConfiguration.weaponSlots[i].isAttached)
				{
					this.shipConfiguration.weaponSlots[i].isActive = true;
				}
			}
		}
	}

	private void ManageZoomScrollerMovement(int direction)
	{
		Vector3 vector3;
		if (!this.zoomSlider.get_activeSelf())
		{
			this.zoomSlider.SetActive(true);
		}
		Transform _transform = this.zoomScroller.get_transform();
		if (direction != 1)
		{
			Vector3 _position = this.zoomScroller.get_transform().get_position();
			float single = _position.x - this.zoomMoveStep;
			float _position1 = this.zoomScroller.get_transform().get_position().y;
			Vector3 vector31 = this.zoomScroller.get_transform().get_position();
			vector3 = new Vector3(single, _position1, vector31.z);
		}
		else
		{
			Vector3 _position2 = this.zoomScroller.get_transform().get_position();
			float single1 = _position2.x + this.zoomMoveStep;
			float single2 = this.zoomScroller.get_transform().get_position().y;
			Vector3 vector32 = this.zoomScroller.get_transform().get_position();
			vector3 = new Vector3(single1, single2, vector32.z);
		}
		_transform.set_position(vector3);
		this.zoomIdleTimer = 2f;
	}

	private void ManageZoomScrollIdleTimer()
	{
		if (!this.zoomSlider.get_activeSelf())
		{
			return;
		}
		ShipScript _deltaTime = this;
		_deltaTime.zoomIdleTimer = _deltaTime.zoomIdleTimer - Time.get_deltaTime();
		if (this.zoomIdleTimer <= 0f)
		{
			this.zoomSlider.SetActive(false);
			this.zoomIdleTimer = 2f;
		}
	}

	private void MouseOverManager()
	{
		if (GuiFramework.IsGuiMatter || !this.isInControl)
		{
			cursorScript.cursorImage = cursorScript.normalState;
			return;
		}
		Vector3 mouseToWorldPosition = this.GetMouseToWorldPosition();
		Collider[] colliderArray = Physics.OverlapSphere(mouseToWorldPosition, 0.25f);
		Collider[] colliderArray1 = colliderArray;
		if (ShipScript.<>f__am$cacheAC == null)
		{
			ShipScript.<>f__am$cacheAC = new Func<Collider, bool>(null, (Collider c) => c.GetComponent(typeof(PveScript)) != null);
		}
		Collider collider = Enumerable.FirstOrDefault<Collider>(Enumerable.Where<Collider>(colliderArray1, ShipScript.<>f__am$cacheAC));
		if (collider != null)
		{
			PveScript component = collider.GetComponent<PveScript>();
			if (!(component != null) || component.pve == null || this.p == null || component.pve.fractionId != this.p.fractionId)
			{
				cursorScript.cursorImage = cursorScript.pveState;
			}
			else
			{
				cursorScript.cursorImage = cursorScript.friendlyPveState;
			}
			return;
		}
		Collider[] colliderArray2 = colliderArray;
		if (ShipScript.<>f__am$cacheAD == null)
		{
			ShipScript.<>f__am$cacheAD = new Func<Collider, bool>(null, (Collider c) => c.GetComponent(typeof(ShipScript)) != null);
		}
		Collider collider1 = Enumerable.FirstOrDefault<Collider>(Enumerable.Where<Collider>(colliderArray2, ShipScript.<>f__am$cacheAD));
		if (collider1 != null)
		{
			ShipScript shipScript = collider1.GetComponent<ShipScript>();
			if (!(shipScript != null) || shipScript.p == null || this.p == null || !this.p.CanShootThisTarget(shipScript.p))
			{
				cursorScript.cursorImage = cursorScript.friendlyPveState;
			}
			else
			{
				cursorScript.cursorImage = cursorScript.pveState;
			}
			return;
		}
		IList<GameObjectPhysics> values = this.comm.gameObjects.get_Values();
		if (ShipScript.<>f__am$cacheAE == null)
		{
			ShipScript.<>f__am$cacheAE = new Func<GameObjectPhysics, bool>(null, (GameObjectPhysics o) => o is Mineral);
		}
		IEnumerator<GameObjectPhysics> enumerator = Enumerable.Where<GameObjectPhysics>(values, ShipScript.<>f__am$cacheAE).GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				MineralEx current = (MineralEx)enumerator.get_Current();
				if (Math.Abs(mouseToWorldPosition.x - current.x) >= ShipScript.MINERAL_COLLIDE_BOX_SIZE || Math.Abs(mouseToWorldPosition.z - current.z) >= ShipScript.MINERAL_COLLIDE_BOX_SIZE)
				{
					continue;
				}
				cursorScript.cursorImage = cursorScript.mineralState;
				return;
			}
		}
		finally
		{
			if (enumerator == null)
			{
			}
			enumerator.Dispose();
		}
		Collider[] colliderArray3 = colliderArray;
		if (ShipScript.<>f__am$cacheAF == null)
		{
			ShipScript.<>f__am$cacheAF = new Func<Collider, bool>(null, (Collider c) => c.GetComponent(typeof(ExtractionPointScript)) != null);
		}
		Collider collider2 = Enumerable.FirstOrDefault<Collider>(Enumerable.Where<Collider>(colliderArray3, ShipScript.<>f__am$cacheAF));
		if (collider2 != null)
		{
			ExtractionPointScript extractionPointScript = collider2.GetComponent<ExtractionPointScript>();
			if (!(extractionPointScript != null) || extractionPointScript.extractionPoint == null || this.p == null || !this.p.CanShootThisTarget(extractionPointScript.extractionPoint))
			{
				cursorScript.cursorImage = cursorScript.friendlyPveState;
			}
			else
			{
				cursorScript.cursorImage = cursorScript.pveState;
			}
			return;
		}
		cursorScript.cursorImage = cursorScript.normalState;
	}

	private void OnGUI()
	{
		this.DrawScreenshotFadeOut();
		if (!this.isRulingLocalPlayer)
		{
			return;
		}
		if (this.extractionPointInRange == null && this.selectedObject != null && this.selectedObject is ExtractionPoint && (this.p.x > this.selectedObject.x + 65f || this.p.x < this.selectedObject.x - 65f || this.p.z > this.selectedObject.z + 65f || this.p.z < this.selectedObject.z - 65f))
		{
			this.DeselectCurrentObject();
		}
		if (this.extractionPointInRange == null && AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.hasMenuOpen && AndromedaGui.mainWnd.activWindowIndex == 19)
		{
			AndromedaGui.mainWnd.CloseActiveWindow();
		}
	}

	public void OnTriggerEnter(Collider c)
	{
		ShipScript component = c.GetComponent<ShipScript>();
		if (component != null)
		{
			this.ManageShipCollision(component);
		}
	}

	public void OpenBaseDoor()
	{
		string empty = string.Empty;
		empty = string.Format("door{0}", this.p.enteringBaseDoor + 1);
		GameObject gameObject = GameObject.Find(empty);
		if (gameObject != null)
		{
			gameObject.get_animation().Play("Take 001");
		}
		GameObject gameObject1 = GameObject.Find("CstationPfb(Clone)");
		if (gameObject1 != null)
		{
			gameObject1.get_animation().Play("CstationJawOpen");
		}
	}

	public void OpenDailyRewardWindow()
	{
		this.isWaitingForShowDailyReward = true;
		this.delyBeforOpenDailyReward = 1.6f;
	}

	public void OpenLevelUpWindow()
	{
		this.isWaitingForShowInfoWindow = true;
		this.delyBeforOpenInfoWindow = 1.6f;
	}

	public void OpenMainMenuWondow(byte index)
	{
		AndromedaGui.mainWnd.OnWindowBtnClicked(new EventHandlerParam()
		{
			customData = index
		});
	}

	public void PlayQuestCompleateAnimation()
	{
		GameObject gameObject = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("QuestCompleted_pfb"));
		gameObject.get_transform().set_position(new Vector3(this.p.x, 1.5f, this.p.z));
		gameObject.GetComponent<LevelUpAnimationScript>().target = this.p;
		TextMesh componentInChildren = gameObject.GetComponentInChildren<TextMesh>();
		Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < (int)componentsInChildren.Length; i++)
		{
			Renderer renderer = componentsInChildren[i];
			if (renderer.get_name() == "SpaceLbl")
			{
				renderer.get_material().set_color(Color.get_yellow());
			}
		}
		if (componentInChildren != null)
		{
			componentInChildren.set_text(StaticData.Translate("key_space_label_quest_completed"));
		}
	}

	public void PlayStoryQuestCompleateAnimation()
	{
		GameObject gameObject = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("StoryQuestCompleted_pfb"));
		gameObject.get_transform().set_position(new Vector3(this.p.x, 1.5f, this.p.z));
		gameObject.GetComponent<LevelUpAnimationScript>().target = this.p;
		TextMesh componentInChildren = gameObject.GetComponentInChildren<TextMesh>();
		Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < (int)componentsInChildren.Length; i++)
		{
			Renderer renderer = componentsInChildren[i];
			if (renderer.get_name() == "SpaceLbl")
			{
				renderer.get_material().set_color(GuiNewStyleBar.blueColor);
			}
		}
		if (componentInChildren != null)
		{
			componentInChildren.set_text(StaticData.Translate("key_space_label_quest_story_completed"));
		}
	}

	public void PopulatePartyArrow(long id)
	{
		if (id == this.partyMemberOneId || id == this.partyMemberTwoId || id == this.partyMemberThreeId)
		{
			this.ManagePartyMemberArrow();
		}
	}

	[DebuggerHidden]
	private IEnumerator PreapearInBaseScene()
	{
		return new ShipScript.<PreapearInBaseScene>c__Iterator13();
	}

	private void RemoveBigStoryQuestArrow()
	{
		if (this.bigStoryArrowWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.bigStoryArrowWindow.handler);
			this.bigStoryArrowWindow = null;
		}
	}

	private void RemoveRapairDroneArrow()
	{
		if (this.rdArrowWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.rdArrowWindow.handler);
			this.rdArrowWindow = null;
		}
	}

	private void RemoveRepairDroneHelper()
	{
		this.RemoveRapairDroneArrow();
		this.RemovoGuidingText();
	}

	public void RemoveTargetArrow()
	{
		if (this.targetArrow == null)
		{
			return;
		}
		Object.Destroy(this.targetArrow);
		this.targetArrow = null;
	}

	private void RemoveTransformerBtnArrow()
	{
		if (this.transformerBtnArrowWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.transformerBtnArrowWindow.handler);
			this.transformerBtnArrowWindow = null;
		}
	}

	private void RemoveTransformerItemArrow()
	{
		if (this.transformerItemArrowWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.transformerItemArrowWindow.handler);
			this.transformerItemArrowWindow = null;
			this.smallArrowTexture = null;
		}
	}

	private void RemovoGuidingText()
	{
		if (this.guidingText != null)
		{
			Object.Destroy(this.guidingText);
			this.guidingText = null;
		}
	}

	private void ResetPartyArrow()
	{
		if (this.partyArrowOne != null)
		{
			Object.Destroy(this.partyArrowOne);
			this.partyArrowOne = null;
		}
		if (this.partyArrowTwo != null)
		{
			Object.Destroy(this.partyArrowTwo);
			this.partyArrowTwo = null;
		}
		if (this.partyArrowThree != null)
		{
			Object.Destroy(this.partyArrowThree);
			this.partyArrowThree = null;
		}
	}

	private void SelectCurrentObject()
	{
		string empty = string.Empty;
		if (this.selectedObject is PlayerObjectPhysics)
		{
			if (Input.GetMouseButton(1))
			{
				this.StartShooting();
			}
			if (((PlayerObjectPhysics)this.selectedObject).get_IsPve())
			{
				empty = (((PlayerObjectPhysics)this.selectedObject).fractionId != this.p.fractionId ? "ShootLock" : "ShootLockGreen");
			}
			else if (this.p.pvpState != 3)
			{
				empty = (((PlayerObjectPhysics)this.selectedObject).fractionId != this.p.fractionId ? "ShootLock" : "ShootLockGreen");
			}
			else
			{
				empty = (((PlayerObjectPhysics)this.selectedObject).teamNumber != this.p.teamNumber ? "ShootLock" : "ShootLockGreen");
			}
		}
		if (this.selectedObject is MineralEx)
		{
			empty = "MiningCage_pfb";
		}
		if (this.selectedObject is ExtractionPoint)
		{
			empty = (((ExtractionPoint)this.selectedObject).ownerFraction != this.p.fractionId ? "ShootLock" : "ShootLockGreen");
		}
		this._lock = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab(empty));
		this._lock.get_transform().set_position(this.selectedGameObject.get_transform().get_position());
		this._lock.get_gameObject().get_transform().set_parent(this.selectedGameObject.get_transform());
	}

	private void SelectNextMineral()
	{
		ShipScript.<SelectNextMineral>c__AnonStorey6C variable = null;
		float distance = 0f;
		bool flag = false;
		this.isStartMiningNeeded = false;
		this.isAbortMiningNeeded = false;
		for (int i = 0; i < this.nearMinerals.get_Count(); i++)
		{
			Mineral item = (Mineral)this.nearMinerals.get_Item(i).gop;
			distance = GameObjectPhysics.GetDistance(this.p.x, this.nearMinerals.get_Item(i).gop.x, this.p.z, this.nearMinerals.get_Item(i).gop.z);
			if (item.gameObject == null || distance > ShipScript.MINING_RANGE * 2f || item.miningPlayerId != 0 && item.miningPlayerId != this.p.playerId || !(item.ownerName == string.Empty) && (!this.p.isInParty || !(item.ownerName == "Party Loot")) && !(item.ownerName == this.p.playerName))
			{
				this.nearMinerals.RemoveAt(i);
				i--;
			}
		}
		if (this.nearMinerals.get_Count() == 0)
		{
			flag = true;
			this.selectedNearMineralIndex = 0;
		}
		IEnumerator<GameObjectPhysics> enumerator = this.comm.gameObjects.get_Values().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				GameObjectPhysics current = enumerator.get_Current();
				if (!(current is Mineral))
				{
					continue;
				}
				distance = GameObjectPhysics.GetDistance(this.p.x, current.x, this.p.z, current.z);
				if (distance > ShipScript.MINING_RANGE * 2f || ((Mineral)current).miningPlayerId != 0 && ((Mineral)current).miningPlayerId != this.p.playerId || !(((Mineral)current).ownerName == string.Empty) && (!this.p.isInParty || !(((Mineral)current).ownerName == "Party Loot")) && !(((Mineral)current).ownerName == this.p.playerName) || this.nearMinerals.Exists(new Predicate<ShipScript.ClosestMineral>(variable, (ShipScript.ClosestMineral n) => n.neighbourhoodId == this.gop.neighbourhoodId)))
				{
					continue;
				}
				List<ShipScript.ClosestMineral> list = this.nearMinerals;
				ShipScript.ClosestMineral closestMineral = new ShipScript.ClosestMineral()
				{
					distance = distance,
					gop = current,
					neighbourhoodId = current.neighbourhoodId
				};
				list.Add(closestMineral);
			}
		}
		finally
		{
			if (enumerator == null)
			{
			}
			enumerator.Dispose();
		}
		if (this.nearMinerals.get_Count() == 0)
		{
			return;
		}
		if (flag)
		{
			List<ShipScript.ClosestMineral> list1 = this.nearMinerals;
			if (ShipScript.<>f__am$cacheB7 == null)
			{
				ShipScript.<>f__am$cacheB7 = new Func<ShipScript.ClosestMineral, float>(null, (ShipScript.ClosestMineral t) => t.distance);
			}
			this.nearMinerals = Enumerable.ToList<ShipScript.ClosestMineral>(Enumerable.OrderBy<ShipScript.ClosestMineral, float>(list1, ShipScript.<>f__am$cacheB7));
			this.selectedNearMineralIndex = 0;
		}
		if (this.selectedNearMineralIndex >= this.nearMinerals.get_Count())
		{
			this.selectedNearMineralIndex = 0;
		}
		this.ManageSelectObjectRequest((GameObject)this.nearMinerals.get_Item(this.selectedNearMineralIndex).gop.gameObject, this.nearMinerals.get_Item(this.selectedNearMineralIndex).gop);
		ShipScript shipScript = this;
		shipScript.selectedNearMineralIndex = shipScript.selectedNearMineralIndex + 1;
	}

	public void SelectNextTarget()
	{
		float distance;
		ShipScript.<SelectNextTarget>c__AnonStorey69 variable = null;
		ShipScript.<SelectNextTarget>c__AnonStorey68 variable1 = null;
		ShipScript.ClosestTarget closestTarget;
		bool flag = false;
		for (int i = 0; i < this.NearObjects.get_Count(); i++)
		{
			if (GameObjectPhysics.GetDistance(this.p.x, this.NearObjects.get_Item(i).targetObject.x, this.p.z, this.NearObjects.get_Item(i).targetObject.z) > ShipScript.NEXT_TARGET_SHORTCUT_RANGE)
			{
				this.NearObjects.RemoveAt(i);
				i--;
			}
			else if (this.NearObjects.get_Item(i).targetObject.get_IsPoP())
			{
				PlayerObjectPhysics item = (PlayerObjectPhysics)this.NearObjects.get_Item(i).targetObject;
				if (item == null || item.gameObject == null || !item.isAlive || item.isInStealthMode || item.teamNumber == this.p.teamNumber && this.p.fractionId == item.fractionId)
				{
					this.NearObjects.RemoveAt(i);
					i--;
				}
			}
		}
		if (this.NearObjects.get_Count() == 0)
		{
			flag = true;
			this.CurrentClosestSelectedTarget = 0;
		}
		IEnumerator<GameObjectPhysics> enumerator = this.comm.gameObjects.get_Values().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				GameObjectPhysics current = enumerator.get_Current();
				if (!(current is PlayerObjectPhysics))
				{
					if (!(current is ExtractionPoint))
					{
						continue;
					}
					distance = GameObjectPhysics.GetDistance(this.p.x, current.x, this.p.z, current.z);
					if (distance > ShipScript.NEXT_TARGET_SHORTCUT_RANGE || this.NearObjects.Exists(new Predicate<ShipScript.ClosestTarget>(variable, (ShipScript.ClosestTarget n) => n.neighbourhoodId == this.gop.neighbourhoodId)))
					{
						continue;
					}
					List<ShipScript.ClosestTarget> nearObjects = this.NearObjects;
					closestTarget = new ShipScript.ClosestTarget()
					{
						DistanceToObj = distance,
						targetObject = current,
						neighbourhoodId = current.neighbourhoodId
					};
					nearObjects.Add(closestTarget);
				}
				else
				{
					PlayerObjectPhysics playerObjectPhysic = (PlayerObjectPhysics)current;
					if (playerObjectPhysic.isAlive && playerObjectPhysic.playerId != this.p.playerId && !playerObjectPhysic.isInStealthMode)
					{
						if (playerObjectPhysic.get_IsPve() || playerObjectPhysic.teamNumber != this.p.teamNumber && playerObjectPhysic.teamNumber != 0 && this.p.teamNumber != 0 || playerObjectPhysic.fractionId != this.p.fractionId)
						{
							distance = GameObjectPhysics.GetDistance(this.p.x, playerObjectPhysic.x, this.p.z, playerObjectPhysic.z);
							if (distance <= ShipScript.NEXT_TARGET_SHORTCUT_RANGE && !this.NearObjects.Exists(new Predicate<ShipScript.ClosestTarget>(variable1, (ShipScript.ClosestTarget n) => n.neighbourhoodId == this.pop.neighbourhoodId)))
							{
								List<ShipScript.ClosestTarget> list = this.NearObjects;
								closestTarget = new ShipScript.ClosestTarget()
								{
									DistanceToObj = distance,
									targetObject = playerObjectPhysic,
									neighbourhoodId = playerObjectPhysic.neighbourhoodId
								};
								list.Add(closestTarget);
							}
						}
					}
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
		if (this.NearObjects.get_Count() == 0)
		{
			return;
		}
		if (flag)
		{
			List<ShipScript.ClosestTarget> nearObjects1 = this.NearObjects;
			if (ShipScript.<>f__am$cacheB1 == null)
			{
				ShipScript.<>f__am$cacheB1 = new Func<ShipScript.ClosestTarget, float>(null, (ShipScript.ClosestTarget n) => n.DistanceToObj);
			}
			Enumerable.OrderBy<ShipScript.ClosestTarget, float>(nearObjects1, ShipScript.<>f__am$cacheB1);
		}
		if (this.CurrentClosestSelectedTarget >= this.NearObjects.get_Count())
		{
			this.CurrentClosestSelectedTarget = 0;
		}
		this.ManageSelectObjectRequest((GameObject)this.NearObjects.get_Item(this.CurrentClosestSelectedTarget).targetObject.gameObject, this.NearObjects.get_Item(this.CurrentClosestSelectedTarget).targetObject);
		ShipScript currentClosestSelectedTarget = this;
		currentClosestSelectedTarget.CurrentClosestSelectedTarget = currentClosestSelectedTarget.CurrentClosestSelectedTarget + 1;
	}

	private void SendZoomLevelUpdate(short prm)
	{
		// 
		// Current member / type: System.Void ShipScript::SendZoomLevelUpdate(System.Int16)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void SendZoomLevelUpdate(System.Int16)
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

	public void SetTargetArrowDestination(Vector3 desstionation)
	{
		if (this.targetArrow == null)
		{
			this.targetArrow = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("GuidingArrow_pfb"));
		}
		GuidingArrowScript component = this.targetArrow.GetComponent<GuidingArrowScript>();
		component.me = base.get_gameObject();
		component.isLocationSet = true;
		component.targetObject = null;
		component.targetLocation = desstionation;
	}

	private void ShowCriticalPendulum()
	{
		if (this.criticalPendalum != null)
		{
			Object.Destroy(this.criticalPendalum);
		}
		this.criticalPendalum = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("CriticalCircle_pfb"));
		this.criticalPendalum.get_transform().set_position(new Vector3(this.p.x, 2f, this.p.z));
		this.criticalPendalum.get_transform().set_localScale(new Vector3(1.7f, 1.7f, 1.7f));
		this.criticalPendalum.GetComponent<CriticalCircleScript>().player = this.p;
		Animator component = this.criticalPendalum.GetComponent<Animator>();
		component.set_speed(0.5f);
		component.Play("CriticalCircleOpasiti_Animation");
		int num = Random.Range(-1, 18) * 20;
		this.criticalPendalum.get_transform().Rotate(new Vector3(0f, (float)num, 0f));
	}

	public void ShowFreeSpaceSkillIndication(int skillId)
	{
		ShipScript.<ShowFreeSpaceSkillIndication>c__AnonStorey66 variable = null;
		float single = (float)((TalentsInfo)Enumerable.First<PlayerItemTypesData>(Enumerable.Where<PlayerItemTypesData>(StaticData.allTypes.get_Values(), new Func<PlayerItemTypesData, bool>(variable, (PlayerItemTypesData t) => t.itemType == this.skillId)))).range;
		if (this.freeSpaceSkillIndicator == null)
		{
			this.freeSpaceSkillIndicator = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("FreeSpaceSkillRange_pfb"));
			this.freeSpaceSkillIndicator.get_transform().set_localScale(new Vector3(single, 1f, single));
			this.freeSpaceSkillIndicator.get_transform().set_position(new Vector3(this.p.x, 0f, this.p.z));
		}
		if (ShipScript.skillTarget == null)
		{
			ShipScript.skillTarget = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("AimCircle_pfb"));
		}
	}

	public void ShowOutOfRangeIndication(float range)
	{
		GameObject gameObject = null;
		gameObject = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("SkillRangeWeapons_pfb"));
		if (gameObject == null)
		{
			return;
		}
		gameObject.get_transform().set_localScale(new Vector3(range, 1f, range));
		gameObject.get_transform().set_position(new Vector3(this.p.x, 0f, this.p.z));
		gameObject.GetComponent<SkillRangeIndicator>().target = this.p;
	}

	public void ShowSkillRangeIndication(int skillId)
	{
		ShipScript.<ShowSkillRangeIndication>c__AnonStorey67 variable = null;
		float single = (float)((TalentsInfo)Enumerable.First<PlayerItemTypesData>(Enumerable.Where<PlayerItemTypesData>(StaticData.allTypes.get_Values(), new Func<PlayerItemTypesData, bool>(variable, (PlayerItemTypesData t) => t.itemType == this.skillId)))).range;
		GameObject gameObject = null;
		gameObject = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("SkillRange_pfb"));
		if (gameObject == null)
		{
			return;
		}
		gameObject.get_transform().set_localScale(new Vector3(single, 1f, single));
		gameObject.get_transform().set_position(new Vector3(this.p.x, 0f, this.p.z));
		gameObject.GetComponent<SkillRangeIndicator>().target = this.p;
	}

	private void Start()
	{
		if (!this.isRulingLocalPlayer)
		{
			return;
		}
		this.keyUp = false;
		this.keyDown = false;
		this.keyLeft = false;
		this.keyRight = false;
		this.cameraOffsetY = 21f;
		Camera.get_main().get_transform().set_position(new Vector3(0f, (float)(NetworkScript.player.playerBelongings.zoomLevel * 5) + this.cameraOffsetY, 0f));
		this.cameraOffsetZ = (float)(NetworkScript.player.playerBelongings.zoomLevel * 2) + this.deltaCameraZ;
		this.cameraOffsetX = 0f;
		this.InitZoomScroller();
		this.textureMapMyShip = new Texture2D(1, 1);
		this.textureMapMyShip.SetPixel(0, 0, new Color(0f, 1f, 0f, 1f));
		this.textureMapMyShip.Apply();
		this.textureMapOtherShip = new Texture2D(1, 1);
		this.textureMapOtherShip.SetPixel(0, 0, new Color(1f, 0f, 0f, 1f));
		this.textureMapOtherShip.Apply();
		this.textureMapBullet = new Texture2D(1, 1);
		this.textureMapBullet.SetPixel(0, 0, new Color(1f, 1f, 0f, 1f));
		this.textureMapBullet.Apply();
		this.textureMapRocket = new Texture2D(1, 1);
		this.textureMapRocket.SetPixel(0, 0, new Color(1f, 0f, 1f, 1f));
		this.textureMapRocket.Apply();
		if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.kb != null)
		{
			this.kb = AndromedaGui.mainWnd.kb;
		}
	}

	public void StartActing()
	{
		if (this.selectedObject is MineralEx)
		{
			this.StartMining();
			this.isStartMiningNeeded = true;
		}
		else if (this.selectedObject.get_IsPoP() || this.selectedObject is ExtractionPoint)
		{
			this.StartShooting();
		}
	}

	public void StartCastingSkill(int skillSlotId)
	{
		// 
		// Current member / type: System.Void ShipScript::StartCastingSkill(System.Int32)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void StartCastingSkill(System.Int32)
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..( , Int32 , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:строка 78
		//    в ..( , Int32& , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:строка 33
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 57
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(IfStatement ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 359
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 55
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
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

	public void StartEnterInGalaxyJump()
	{
		this.isInControl = false;
		if (ShipScript.mapTarget != null)
		{
			Object.DestroyObject(ShipScript.mapTarget);
		}
		this.EnterInGalaxyJump();
		AndromedaGui.galaxyJumpWnd = new GalaxyJumpCancelWindow();
		AndromedaGui.galaxyJumpWnd.Create();
		AndromedaGui.gui.AddWindow(AndromedaGui.galaxyJumpWnd);
	}

	public void StartEnterInHyperJump()
	{
		this.isInControl = false;
		this.p.StartHyperJump(this.hyperJumpInRange);
		this.EnterInHyperJump((GameObject)this.hyperJumpInRange.gameObject, base.get_gameObject());
		this.StartScaleIn();
	}

	public void StartJump()
	{
		// 
		// Current member / type: System.Void ShipScript::StartJump()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void StartJump()
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

	private void StartMining()
	{
		// 
		// Current member / type: System.Void ShipScript::StartMining()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void StartMining()
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..( , Int32 , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:строка 78
		//    в ..( , Int32& , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:строка 33
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 57
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(IfStatement ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 359
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 55
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
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

	public void StartScaleIn()
	{
		this.scaleRatio = base.get_gameObject().get_transform().get_localScale();
		this.isScalingIn = true;
		this.scaleStartTime = DateTime.get_Now();
	}

	public void StartScaleOut()
	{
		this.scaleRatio = base.get_gameObject().get_transform().get_localScale();
		this.isScalingOut = true;
		this.scaleStartTime = DateTime.get_Now();
		base.get_gameObject().get_transform().set_localScale(new Vector3(0f, 0f, 0f));
	}

	private void StartShooting()
	{
		// 
		// Current member / type: System.Void ShipScript::StartShooting()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void StartShooting()
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

	private void StartSpeedBoost()
	{
		// 
		// Current member / type: System.Void ShipScript::StartSpeedBoost()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void StartSpeedBoost()
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

	private void StopActing()
	{
		if (this.selectedObject is MineralEx && !NetworkScript.player.playerBelongings.isAutoMiningOn)
		{
			this.AbortMining();
		}
	}

	public void StopShooting()
	{
		// 
		// Current member / type: System.Void ShipScript::StopShooting()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void StopShooting()
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..( , Int32 , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:строка 78
		//    в ..( , Int32& , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:строка 33
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 57
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(IfStatement ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 359
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 55
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
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

	public void StopSpeedBooster()
	{
		this.stopBoostActivated = true;
		this.stopSpeedBoostDelay = 0.5f;
	}

	private void TargetDiedStopShooting()
	{
		Object.Destroy(this._lock);
		this._lock = null;
		this.shootTarget = null;
		this.StopShooting();
	}

	private void TryToggleWeapon(int weaponSlot)
	{
		if (weaponSlot == -1)
		{
			weaponSlot = 9;
		}
		if ((int)this.shipConfiguration.weaponSlots.Length <= weaponSlot || !this.shipConfiguration.weaponSlots[weaponSlot].isAttached)
		{
			return;
		}
		this.shipConfiguration.weaponSlots[weaponSlot].isActive = !this.shipConfiguration.weaponSlots[weaponSlot].isActive;
	}

	public void UnselectCurrentObject()
	{
		// 
		// Current member / type: System.Void ShipScript::UnselectCurrentObject()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void UnselectCurrentObject()
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..( , Int32 , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:строка 78
		//    в ..( , Int32& , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:строка 33
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 57
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(IfStatement ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 359
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 55
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(IfStatement ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 359
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 55
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
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

	private void Update()
	{
		// 
		// Current member / type: System.Void ShipScript::Update()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void Update()
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..( , Int32 , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:строка 78
		//    в ..( , Int32& , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:строка 33
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 57
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(IfStatement ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 359
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 55
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(IfStatement ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 359
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 55
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(IfStatement ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 359
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 55
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
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

	public void UpdatePartyArrow(long memberId)
	{
		if (this.partyArrowOne != null && this.partyMemberOneId == memberId || this.partyArrowTwo != null && this.partyMemberTwoId == memberId || this.partyArrowThree != null && this.partyMemberThreeId == memberId)
		{
			return;
		}
		this.ManagePartyMemberArrow();
	}

	private void UpdateRapairDroneArrowPosition()
	{
		if (this.rdArrowWindow == null)
		{
			return;
		}
		ShipScript _deltaTime = this;
		_deltaTime.rdArrowDeltaTime = _deltaTime.rdArrowDeltaTime + Time.get_deltaTime();
		this.rdArrowWindow.boundries.set_y((float)(Screen.get_height() - 50));
		this.rdArrowWindow.boundries.set_x(this.dronX + (float)(50 - 50 * Math.Abs(Math.Sin((double)(3f * this.rdArrowDeltaTime)))));
	}

	private void UpdateRepairDroneHelper()
	{
		if (this.rdArrowWindow != null)
		{
			this.InitRapairDroneArrow();
		}
		if (this.guidingText != null)
		{
			this.InitializeGuidingText();
		}
	}

	private void UpdateStoryArrowWindowPosition()
	{
		if (this.bigStoryArrowWindow == null)
		{
			return;
		}
		ShipScript _deltaTime = this;
		_deltaTime.deltaTime = _deltaTime.deltaTime + Time.get_deltaTime();
		this.bigStoryArrowWindow.boundries.set_y((float)((double)this.STORY_WINDOW_MAX_Y - 100 * Math.Abs(Math.Sin((double)(3f * this.deltaTime)))));
		this.bigStoryArrowWindow.boundries.set_x((float)(Screen.get_width() - 240));
	}

	private void UpdateTransformerBtnArrowPosition()
	{
		if (this.transformerBtnArrowWindow == null)
		{
			return;
		}
		ShipScript _deltaTime = this;
		_deltaTime.transformerBtnArrowDeltaTime = _deltaTime.transformerBtnArrowDeltaTime + Time.get_deltaTime();
		this.transformerBtnArrowWindow.boundries.set_y((float)(Screen.get_height() / 2 - 40));
		this.transformerBtnArrowWindow.boundries.set_x((float)(Screen.get_width() / 2 + 100) + (float)(70 - 70 * Math.Abs(Math.Sin((double)(3f * this.transformerBtnArrowDeltaTime)))));
	}

	private void UpdateTransformerItemArrowPosition()
	{
		if (this.transformerItemArrowWindow == null)
		{
			return;
		}
		ShipScript _deltaTime = this;
		_deltaTime.transformerItemArrowDeltaTime = _deltaTime.transformerItemArrowDeltaTime + Time.get_deltaTime();
		this.smallArrowTexture.boundries.set_y((float)(75 - 75 * Math.Abs(Math.Sin((double)(3f * this.transformerItemArrowDeltaTime)))));
		this.transformerItemArrowWindow.boundries.set_x(this.itemPositionX);
		this.transformerItemArrowWindow.boundries.set_y(this.itemPositionY);
	}

	private void UpdateUdpAliveStatus()
	{
		this.comm.lastIamAliveMessageTime = DateTime.get_Now();
	}

	private void UseSocialInteraction(int index)
	{
		// 
		// Current member / type: System.Void ShipScript::UseSocialInteraction(System.Int32)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void UseSocialInteraction(System.Int32)
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

	private Vector2 VectorAbs(Vector2 vector)
	{
		return new Vector2(Math.Abs(vector.x), Math.Abs(vector.y));
	}

	private void ZoomCamera(int direction)
	{
		if (direction == 0 && NetworkScript.player.playerBelongings.zoomLevel > 0 && NetworkScript.player.vessel.galaxy.get_galaxyId() != 1000)
		{
			this.SendZoomLevelUpdate((short)(NetworkScript.player.playerBelongings.zoomLevel - 1));
			this.ManageZoomScrollerMovement(0);
		}
		if (direction == 1 && NetworkScript.player.playerBelongings.zoomLevel < 12 && NetworkScript.player.vessel.galaxy.get_galaxyId() != 1000)
		{
			this.SendZoomLevelUpdate((short)(NetworkScript.player.playerBelongings.zoomLevel + 1));
			this.ManageZoomScrollerMovement(1);
		}
	}

	private void ZoomLevel(int lvl)
	{
		this.cameraOffsetZ = (float)(lvl * 2) + this.deltaCameraZ;
		Transform _transform = Camera.get_main().get_transform();
		Vector3 _position = base.get_gameObject().get_transform().get_position();
		float single = _position.x + this.speedEffectDeltaX;
		float single1 = (float)(lvl * 5) + this.cameraOffsetY;
		Vector3 vector3 = base.get_gameObject().get_transform().get_position();
		_transform.set_position(new Vector3(single, single1, vector3.z - this.cameraOffsetZ + this.speedEffectDeltaZ));
	}

	private class ClosestMineral
	{
		public float distance;

		public GameObjectPhysics gop;

		public uint neighbourhoodId;

		public ClosestMineral()
		{
		}
	}

	private class ClosestTarget
	{
		public float DistanceToObj;

		public GameObjectPhysics targetObject;

		public uint neighbourhoodId;

		public ClosestTarget()
		{
		}
	}
}