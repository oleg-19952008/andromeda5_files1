using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class GuiFramework : MonoBehaviour
{
	public const GuiFramework.CommonSounds SoundClickDefault = GuiFramework.CommonSounds.Click;

	public const GuiFramework.CommonSounds SoundHoverDefault = GuiFramework.CommonSounds.Hover;

	public static float masterVolume;

	public static float musicVolume;

	public static float fxVolume;

	public static float voiceVolume;

	private SortedList<ushort, AudioSource> audioList = new SortedList<ushort, AudioSource>();

	private bool isGuiMatterCached;

	public int activeToolTipId = -1;

	public Action activeTooltipCloseAction;

	private static List<GuiFramework> instances;

	private Object allWindowsLock = new Object();

	private GuiWindow[] allWindows = new GuiWindow[0];

	public Action<GuiWindow> OnMouseDown;

	public bool showGlobalBkgTexture;

	public Texture2D globalBackgroundTexture;

	public GuiWindow modalWindow;

	private string _requestFocusControlName;

	private static bool isGuiMatterCalculated;

	private int windowsHandlerCounter;

	private List<GuiWindow> windows = new List<GuiWindow>();

	public static GuiTextureDraggable draggingObject;

	public Action fxUpdate;

	private Texture2D txBlack;

	private float animationTime;

	private float fullBlackTime;

	private float deltaTime;

	private bool isGuiSliding;

	private bool isTextPositionSet;

	private float positionX;

	private Vector2 labelSize;

	private GUIStyle styleProgress;

	public static string progressLabel;

	public bool IIsGuiMatter
	{
		get
		{
			if (!GuiFramework.isGuiMatterCalculated)
			{
				this.CalculateIsGuiMatter();
			}
			return this.isGuiMatterCached;
		}
	}

	public static bool IsFxSoundsMute
	{
		get
		{
			return GuiFramework.fxVolume <= 0.02f;
		}
	}

	public static bool IsGuiMatter
	{
		get
		{
			bool isGuiMatter = false;
			foreach (GuiFramework instance in GuiFramework.instances)
			{
				isGuiMatter = isGuiMatter | instance.IIsGuiMatter;
			}
			return isGuiMatter;
		}
	}

	public static bool IsMasterSoundsMute
	{
		get
		{
			return GuiFramework.masterVolume <= 0.02f;
		}
	}

	public static bool IsMusicSoundsMute
	{
		get
		{
			return GuiFramework.musicVolume <= 0.02f;
		}
	}

	public static bool IsVoiceSoundsMute
	{
		get
		{
			return GuiFramework.voiceVolume <= 0.02f;
		}
	}

	static GuiFramework()
	{
		GuiFramework.masterVolume = 1f;
		GuiFramework.musicVolume = 0.2f;
		GuiFramework.fxVolume = 1f;
		GuiFramework.voiceVolume = 1f;
		GuiFramework.instances = new List<GuiFramework>();
		GuiFramework.isGuiMatterCalculated = false;
		GuiFramework.progressLabel = "Preparing Your Ship 000%";
	}

	public GuiFramework()
	{
	}

	public void AddWindow(GuiWindow window)
	{
		GuiFramework guiFramework = this;
		int num = guiFramework.windowsHandlerCounter + 1;
		int num1 = num;
		guiFramework.windowsHandlerCounter = num;
		window.handler = num1;
		this.windows.Add(window);
		if (window.isModal)
		{
			this.modalWindow = window;
		}
		this.UpdateWindowsCollection();
	}

	private void CalculateIsGuiMatter()
	{
		this.isGuiMatterCached = false;
		foreach (GuiWindow window in this.windows)
		{
			GuiFramework guiFramework = this;
			guiFramework.isGuiMatterCached = guiFramework.isGuiMatterCached | (!window.IsMouseOver ? false : !window.isClickTransparent);
		}
		GuiFramework.isGuiMatterCalculated = true;
	}

	public void ClearFocusOnControl()
	{
		this._requestFocusControlName = null;
	}

	private GuiWindow GetActiveOne()
	{
		float _mousePosition = Input.get_mousePosition().x;
		float _height = (float)Screen.get_height() - Input.get_mousePosition().y;
		List<GuiWindow> list = new List<GuiWindow>();
		GuiWindow[] guiWindowArray = this.allWindows;
		for (int i = 0; i < (int)guiWindowArray.Length; i++)
		{
			GuiWindow guiWindow = guiWindowArray[i];
			if (!guiWindow.ignoreClickEvents && guiWindow.boundries.Contains(new Vector2(_mousePosition, _height)))
			{
				list.Add(guiWindow);
			}
		}
		List<GuiWindow> list1 = list;
		if (GuiFramework.<>f__am$cache1F == null)
		{
			GuiFramework.<>f__am$cache1F = new Func<GuiWindow, byte>(null, (GuiWindow t) => t.zOrder);
		}
		return Enumerable.FirstOrDefault<GuiWindow>(Enumerable.OrderByDescending<GuiWindow, byte>(list1, GuiFramework.<>f__am$cache1F));
	}

	private void LateUpdate()
	{
		GuiFramework.isGuiMatterCalculated = false;
	}

	private void LoadingScreen()
	{
		this.txBlack = (Texture2D)Resources.Load("iPad/loading_screen");
		GUI.set_depth(-1);
		GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.get_width(), (float)Screen.get_height()), this.txBlack, 0);
		GUI.set_depth(0);
		if (Time.get_deltaTime() > 1f)
		{
			this.StartFadeOut();
		}
	}

	private void LoadingScreenWithProgress()
	{
		if (!this.isTextPositionSet)
		{
			this.isTextPositionSet = true;
			this.labelSize = this.styleProgress.CalcSize(new GUIContent(GuiFramework.progressLabel));
			this.positionX = (float)(Screen.get_width() / 2) - this.labelSize.x / 2f;
		}
		this.txBlack = (Texture2D)Resources.Load("iPad/loading_screen");
		GUI.set_depth(-1);
		GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.get_width(), (float)Screen.get_height()), this.txBlack, 0);
		GUI.Label(new Rect(this.positionX, (float)Screen.get_height() * 0.85f, this.labelSize.x, this.labelSize.y), GuiFramework.progressLabel, this.styleProgress);
		GUI.set_depth(0);
		if (Time.get_deltaTime() > 1f)
		{
			this.StartFadeOut();
		}
	}

	private void NewFadeIn()
	{
		float single;
		GuiFramework _deltaTime = this;
		_deltaTime.deltaTime = _deltaTime.deltaTime - Time.get_deltaTime();
		single = (this.deltaTime >= this.fullBlackTime ? (this.deltaTime - this.fullBlackTime) / (this.animationTime - this.fullBlackTime) : 0f);
		this.txBlack.SetPixel(0, 0, new Color(0f, 0f, 0f, 1f - single));
		this.txBlack.Apply();
		GUI.set_depth(-1);
		GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.get_width(), (float)Screen.get_height()), this.txBlack, 0, true);
		GUI.set_depth(0);
	}

	private void NewFadeOut()
	{
		float single;
		if (this.animationTime <= this.fullBlackTime)
		{
			return;
		}
		GuiFramework _deltaTime = this;
		_deltaTime.deltaTime = _deltaTime.deltaTime + Time.get_deltaTime();
		single = (this.deltaTime > this.fullBlackTime ? (this.deltaTime - this.fullBlackTime) / (this.animationTime - this.fullBlackTime) : 0f);
		this.txBlack.SetPixel(0, 0, new Color(0f, 0f, 0f, 1f - single));
		this.txBlack.Apply();
		GUI.set_depth(-1);
		GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.get_width(), (float)Screen.get_height()), this.txBlack, 0, true);
		GUI.set_depth(0);
		if (this.deltaTime >= this.animationTime)
		{
			this.fxUpdate = null;
		}
	}

	public void OnDestroy()
	{
		GuiFramework.instances.Remove(this);
	}

	private void OnGUI()
	{
		// 
		// Current member / type: System.Void GuiFramework::OnGUI()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnGUI()
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..() в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\RebuildLockStatements.cs:строка 81
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\RebuildLockStatements.cs:строка 24
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeVisitor.cs:строка 69
		//    в ..(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\RebuildLockStatements.cs:строка 19
		//    в ..(MethodBody ,  , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 88
		//    в ..(MethodBody , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 70
		//    в Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 95
		//    в Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 58
		//    в ..(ILanguage , MethodDefinition ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:строка 117
		// 
		// mailto: JustDecompilePublicFeedback@telerik.com

	}

	public void PlayLoopSound(GuiFramework.CommonSounds soundKey)
	{
		AudioSource audioSource = null;
		if (this.audioList.TryGetValue((ushort)soundKey, ref audioSource))
		{
			if (audioSource.get_isPlaying())
			{
				return;
			}
			audioSource.set_loop(true);
			audioSource.set_volume(GuiFramework.masterVolume);
			audioSource.Play();
		}
	}

	public void PlaySound(GuiFramework.CommonSounds soundKey)
	{
		AudioSource audioSource = null;
		try
		{
			if (this.audioList.TryGetValue((ushort)soundKey, ref audioSource))
			{
				audioSource.set_volume(GuiFramework.masterVolume);
				audioSource.Play();
			}
		}
		catch (Exception exception)
		{
		}
	}

	public void RemoveWindow(int handler)
	{
		GuiFramework.<RemoveWindow>c__AnonStorey1D variable = null;
		GuiWindow guiWindow = Enumerable.FirstOrDefault<GuiWindow>(Enumerable.Where<GuiWindow>(this.windows, new Func<GuiWindow, bool>(variable, (GuiWindow w) => w.handler == this.handler)));
		if (guiWindow == null)
		{
			return;
		}
		if (this.modalWindow != null && this.modalWindow.handler == guiWindow.handler)
		{
			this.modalWindow = null;
		}
		guiWindow.RemoveTooltipWindowOnElements();
		guiWindow.Clear();
		this.windows.Remove(guiWindow);
		this.UpdateWindowsCollection();
	}

	public void RequestFocusOnControl(string controlName)
	{
		this._requestFocusControlName = controlName;
	}

	public void ShowLoadingScreen()
	{
		this.fxUpdate = new Action(this, GuiFramework.LoadingScreen);
	}

	public void Start()
	{
		this._requestFocusControlName = null;
		GuiFramework.instances.Add(this);
		GUIStyle gUIStyle = new GUIStyle();
		gUIStyle.set_alignment(3);
		gUIStyle.set_font((Font)Resources.Load("Play-Bold", typeof(Font)));
		gUIStyle.set_fontSize(24);
		GUIStyleState gUIStyleState = new GUIStyleState();
		gUIStyleState.set_textColor(new Color(0.2187f, 0.4648f, 0.746f));
		gUIStyle.set_normal(gUIStyleState);
		this.styleProgress = gUIStyle;
	}

	public void StartFadeIn()
	{
		this.animationTime = 3f;
		this.fullBlackTime = 1.2f;
		this.deltaTime = this.animationTime;
		this.txBlack = new Texture2D(1, 1, 5, false);
		this.fxUpdate = new Action(this, GuiFramework.NewFadeIn);
	}

	public void StartFadeOut()
	{
		this.deltaTime = 0f;
		this.animationTime = 4f;
		this.fullBlackTime = 2.8f;
		this.txBlack = new Texture2D(1, 1, 5, false);
		this.fxUpdate = new Action(this, GuiFramework.NewFadeOut);
	}

	public void StopLoopSound(GuiFramework.CommonSounds soundKey)
	{
		AudioSource audioSource = null;
		if (this.audioList.TryGetValue((ushort)soundKey, ref audioSource) && audioSource.get_isPlaying())
		{
			audioSource.Stop();
		}
	}

	private void Update()
	{
		bool mouseButtonDown = Input.GetMouseButtonDown(0);
		GuiWindow guiWindow = null;
		Object obj = this.allWindowsLock;
		Monitor.Enter(obj);
		try
		{
			if (this.modalWindow == null)
			{
				GuiWindow activeOne = this.GetActiveOne();
				GuiWindow[] guiWindowArray = this.allWindows;
				for (int i = 0; i < (int)guiWindowArray.Length; i++)
				{
					GuiWindow guiWindow1 = guiWindowArray[i];
					if (!guiWindow1.isHidden)
					{
						if (mouseButtonDown && guiWindow == null && guiWindow1.IsMouseOver)
						{
							guiWindow = guiWindow1;
						}
						if (activeOne == null)
						{
							guiWindow1.Update(false);
						}
						else
						{
							guiWindow1.Update(guiWindow1.handler == activeOne.handler);
						}
					}
				}
			}
			else
			{
				this.modalWindow.Update(true);
				if ((object)this.modalWindow == (object)this.GetActiveOne())
				{
					guiWindow = this.modalWindow;
				}
				GuiWindow[] guiWindowArray1 = this.allWindows;
				for (int j = 0; j < (int)guiWindowArray1.Length; j++)
				{
					GuiWindow guiWindow2 = guiWindowArray1[j];
					if (!guiWindow2.isHidden)
					{
						guiWindow2.Update(false);
					}
				}
			}
		}
		finally
		{
			Monitor.Exit(obj);
		}
		if (mouseButtonDown && this.OnMouseDown != null)
		{
			this.OnMouseDown.Invoke(guiWindow);
		}
	}

	public void UpdateWindowsCollection()
	{
		Object obj = this.allWindowsLock;
		Monitor.Enter(obj);
		try
		{
			List<GuiWindow> list = this.windows;
			if (GuiFramework.<>f__am$cache20 == null)
			{
				GuiFramework.<>f__am$cache20 = new Func<GuiWindow, byte>(null, (GuiWindow w) => w.zOrder);
			}
			this.allWindows = Enumerable.ToArray<GuiWindow>(Enumerable.OrderBy<GuiWindow, byte>(list, GuiFramework.<>f__am$cache20));
		}
		finally
		{
			Monitor.Exit(obj);
		}
	}

	public enum CommonSounds
	{
		Click,
		Hover,
		Buy,
		Sell,
		EnemyTarget,
		Collect,
		Close,
		Open,
		Change,
		ResourceTarget,
		ShipMove,
		StationIn,
		StationOut
	}
}