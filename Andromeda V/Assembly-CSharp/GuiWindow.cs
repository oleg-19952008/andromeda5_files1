using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class GuiWindow
{
	private GuiLabel warningMessage;

	private GuiLabel warningMessageShadow;

	private float deltaTime;

	private bool isFadeingOut;

	private int blinkCnt;

	private int blinkCountTarget = 2;

	public bool isClickTransparent;

	public GuiElement isLockedByScroll;

	public Action customOnGUIAction;

	private bool isClippingAreaSet;

	private SortedList<byte, ScrollingContainer> clippingBoundariesContainers;

	public byte zOrder = 100;

	public Rect boundries;

	public int handler;

	public bool isHidden = true;

	public Texture2D backgroundTexture;

	private List<GuiElement> elements = new List<GuiElement>();

	private List<GuiElement> clippingAreaElements = new List<GuiElement>();

	private object locker = new object();

	public bool isModal;

	public bool ignoreClickEvents;

	public Action secondaryDrawHandler;

	public object preDrawHandlerParam;

	public Action<object> preDrawHandler;

	public bool isActiveWindow;

	private GuiElement lastMouseOver;

	private Action fx;

	public Action fxEnded;

	public float amplitudeHammerShake = 3f;

	public int hammerShakes = 1;

	public float timeHammerFx = 0.8f;

	public float v0hammer = 0.25f;

	public float moveToShakeRatio = 0.8f;

	public DateTime timeHammerStart;

	private float timeHammerMove;

	private float timeHammerShake;

	private float hammerFxDestinationX;

	private float hammerFxDestinationY;

	private float hammerFxStartX;

	private float hammerFxStartY;

	private float hammerV0x;

	private float hammerV0y;

	private float hammerAccelX;

	private float hammerAccelY;

	private float amplitudeHammerX;

	private float amplitudeHammerY;

	private float shakeHammerK;

	private float moveToSpeedX;

	private float moveToSpeedY;

	private float destinationX;

	private float destinationY;

	private bool moveByX;

	private bool moveByY;

	private bool visibleAfterEffect = true;

	public bool IsHammerEffectActive
	{
		get
		{
			if (this.fx == null)
			{
				return false;
			}
			return true;
		}
	}

	public bool IsMouseOver
	{
		get
		{
			Vector3 _mousePosition = Input.get_mousePosition();
			float _height = (float)Screen.get_height() - _mousePosition.y;
			float single = _mousePosition.x;
			return this.boundries.Contains(new Vector2(single, _height));
		}
	}

	public Vector2 MousePosition
	{
		get
		{
			Vector3 _mousePosition = Input.get_mousePosition();
			Vector2 vector2 = new Vector2(_mousePosition.x - this.boundries.get_x(), (float)Screen.get_height() - _mousePosition.y - this.boundries.get_y());
			return vector2;
		}
	}

	public GuiWindow()
	{
	}

	public void AddClippedGuiElement(GuiElement element)
	{
		object obj = this.locker;
		Monitor.Enter(obj);
		try
		{
			this.clippingAreaElements.Add(element);
			element.container = this;
		}
		finally
		{
			Monitor.Exit(obj);
		}
	}

	public void AddGuiElement(GuiElement element)
	{
		object obj = this.locker;
		Monitor.Enter(obj);
		try
		{
			this.elements.Add(element);
			element.container = this;
			if (this.warningMessage != null)
			{
				this.elements.Remove(this.warningMessageShadow);
				this.elements.Remove(this.warningMessage);
				this.elements.Add(this.warningMessageShadow);
				this.elements.Add(this.warningMessage);
			}
		}
		finally
		{
			Monitor.Exit(obj);
		}
	}

	public void AddGuiElementAtBottom(GuiElement element)
	{
		object obj = this.locker;
		Monitor.Enter(obj);
		try
		{
			this.elements.Insert(0, element);
			element.container = this;
		}
		finally
		{
			Monitor.Exit(obj);
		}
	}

	public void Clear()
	{
		object obj = this.locker;
		Monitor.Enter(obj);
		try
		{
			GuiElement[] array = this.elements.ToArray();
			for (int i = 0; i < (int)array.Length; i++)
			{
				GuiElement guiElement = array[i];
				if (GuiFramework.draggingObject == guiElement)
				{
					GuiFramework.draggingObject = null;
				}
				this.elements.Remove(guiElement);
			}
			this.elements.Clear();
			GuiElement[] guiElementArray = this.clippingAreaElements.ToArray();
			for (int j = 0; j < (int)guiElementArray.Length; j++)
			{
				GuiElement guiElement1 = guiElementArray[j];
				if (GuiFramework.draggingObject == guiElement1)
				{
					GuiFramework.draggingObject = null;
				}
				this.clippingAreaElements.Remove(guiElement1);
			}
			this.clippingAreaElements.Clear();
		}
		finally
		{
			Monitor.Exit(obj);
		}
	}

	public Rect ClippingBoundaris(byte id)
	{
		ScrollingContainer scrollingContainer = null;
		this.clippingBoundariesContainers.TryGetValue(id, ref scrollingContainer);
		return scrollingContainer.boundaris;
	}

	public virtual void Create()
	{
	}

	private void DistributeHoverEffect()
	{
		if (this.lastMouseOver != null && this.lastMouseOver.IsMouseOver)
		{
			return;
		}
		if (this.lastMouseOver != null && !this.lastMouseOver.IsMouseOver)
		{
			this.lastMouseOver.isHovered = false;
			if (this.lastMouseOver.isHoverAware && this.lastMouseOver.Hovered != null)
			{
				this.lastMouseOver.Hovered.Invoke(this.lastMouseOver.hoverParam, false);
			}
		}
		GuiElement[] array = this.elements.ToArray();
		for (int i = 0; i < (int)array.Length; i++)
		{
			if (this.DistributeHoverEffectOnSingleControl(array[i]))
			{
				return;
			}
		}
		GuiElement[] guiElementArray = this.clippingAreaElements.ToArray();
		for (int j = 0; j < (int)guiElementArray.Length; j++)
		{
			if (this.DistributeHoverEffectOnSingleControl(guiElementArray[j]))
			{
				return;
			}
		}
		this.lastMouseOver = null;
	}

	private bool DistributeHoverEffectOnSingleControl(GuiElement element)
	{
		if (this.isLockedByScroll != null)
		{
			return false;
		}
		if (!element.isHoverAware)
		{
			return false;
		}
		if ((object)element == (object)this.lastMouseOver)
		{
			return false;
		}
		if (!element.IsMouseOver)
		{
			return false;
		}
		this.lastMouseOver = element;
		element.isHovered = true;
		if (element.Hovered != null)
		{
			element.Hovered.Invoke(element.hoverParam, true);
		}
		return true;
	}

	private void DistributeMouseDownEvent()
	{
		// 
		// Current member / type: System.Void GuiWindow::DistributeMouseDownEvent()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void DistributeMouseDownEvent()
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

	private void DistributeMouseUpEvent()
	{
		if (!Input.GetMouseButtonUp(0))
		{
			return;
		}
		if (GuiFramework.draggingObject != null)
		{
			GuiFramework.draggingObject.StopDrag();
		}
	}

	private void DoHammerEffect()
	{
		TimeSpan now = DateTime.get_Now() - this.timeHammerStart;
		float totalSeconds = (float)now.get_TotalSeconds();
		if (totalSeconds >= this.timeHammerFx)
		{
			this.FinishHammerEffect();
			return;
		}
		if (totalSeconds >= this.timeHammerMove)
		{
			this.boundries.set_x(this.hammerFxDestinationX + (float)Math.Sin((double)((totalSeconds - this.timeHammerMove) * this.shakeHammerK)) * this.amplitudeHammerX);
			this.boundries.set_y(this.hammerFxDestinationY + (float)Math.Sin((double)((totalSeconds - this.timeHammerMove) * this.shakeHammerK)) * this.amplitudeHammerY);
		}
		else
		{
			this.boundries.set_x(this.hammerFxStartX + this.hammerV0x * totalSeconds + this.hammerAccelX * totalSeconds * totalSeconds / 2f);
			this.boundries.set_y(this.hammerFxStartY + this.hammerV0y * totalSeconds + this.hammerAccelY * totalSeconds * totalSeconds / 2f);
		}
	}

	private void DoMove()
	{
		if (this.moveToSpeedX == 0f)
		{
			this.moveByX = false;
		}
		if (this.moveByX && this.moveToSpeedX > 0f)
		{
			if (this.boundries.get_x() <= this.destinationX)
			{
				this.boundries.set_x(this.boundries.get_x() + Time.get_deltaTime() * this.moveToSpeedX);
			}
			else
			{
				this.boundries.set_x(this.destinationX);
				this.moveByX = false;
			}
		}
		if (this.moveByX && this.moveToSpeedX < 0f)
		{
			if (this.boundries.get_x() >= this.destinationX)
			{
				this.boundries.set_x(this.boundries.get_x() + Time.get_deltaTime() * this.moveToSpeedX);
			}
			else
			{
				this.boundries.set_x(this.destinationX);
				this.moveByX = false;
			}
		}
		if (this.moveToSpeedY == 0f)
		{
			this.moveByY = false;
		}
		if (this.moveByY && this.moveToSpeedY > 0f)
		{
			if (this.boundries.get_y() <= this.destinationY)
			{
				this.boundries.set_y(this.boundries.get_y() + Time.get_deltaTime() * this.moveToSpeedY);
			}
			else
			{
				this.boundries.set_y(this.destinationY);
				this.moveByY = false;
			}
		}
		if (this.moveByY && this.moveToSpeedY < 0f)
		{
			if (this.boundries.get_y() >= this.destinationY)
			{
				this.boundries.set_y(this.boundries.get_y() + Time.get_deltaTime() * this.moveToSpeedY);
			}
			else
			{
				this.boundries.set_y(this.destinationY);
				this.moveByY = false;
			}
		}
		if (!this.moveByX && !this.moveByY)
		{
			this.fx = null;
			this.isHidden = !this.visibleAfterEffect;
		}
	}

	public void DoWindow(int fakeId)
	{
		this.DrawContent();
		GUI.BringWindowToBack(this.handler);
	}

	public void DrawContent()
	{
		GuiWindow.<DrawContent>c__AnonStorey17 variable = null;
		if (this.preDrawHandler != null)
		{
			this.preDrawHandler.Invoke(this.preDrawHandlerParam);
		}
		GUI.DrawTexture(new Rect(0f, 0f, (float)this.backgroundTexture.get_width(), (float)this.backgroundTexture.get_height()), this.backgroundTexture);
		object obj = this.locker;
		Monitor.Enter(obj);
		try
		{
			GuiElement[] array = this.elements.ToArray();
			GuiElement guiElement = null;
			GuiElement[] guiElementArray = array;
			for (int i = 0; i < (int)guiElementArray.Length; i++)
			{
				GuiElement guiElement1 = guiElementArray[i];
				if (GuiFramework.draggingObject != guiElement1)
				{
					guiElement1.DrawGuiElement();
					guiElement1.DrawToolTip();
				}
				else
				{
					guiElement = guiElement1;
				}
			}
			if (guiElement != null)
			{
				guiElement.DrawGuiElement();
			}
		}
		finally
		{
			Monitor.Exit(obj);
		}
		if (this.isClippingAreaSet)
		{
			IEnumerator<byte> enumerator = this.clippingBoundariesContainers.get_Keys().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					byte current = enumerator.get_Current();
					GUI.BeginGroup(this.clippingBoundariesContainers.get_Item(current).boundaris);
					object obj1 = this.locker;
					Monitor.Enter(obj1);
					try
					{
						GuiElement[] array1 = Enumerable.ToArray<GuiElement>(Enumerable.Where<GuiElement>(this.clippingAreaElements, new Func<GuiElement, bool>(variable, (GuiElement t) => t.clippingBoundariesId == this.key)));
						GuiElement guiElement2 = null;
						GuiElement[] guiElementArray1 = array1;
						for (int j = 0; j < (int)guiElementArray1.Length; j++)
						{
							GuiElement guiElement3 = guiElementArray1[j];
							if (GuiFramework.draggingObject != guiElement3)
							{
								guiElement3.DrawGuiElement();
								guiElement3.DrawToolTip();
							}
							else
							{
								guiElement2 = guiElement3;
							}
						}
						if (guiElement2 != null)
						{
							guiElement2.DrawGuiElement();
						}
					}
					finally
					{
						Monitor.Exit(obj1);
					}
					GUI.EndGroup();
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
		if (this.secondaryDrawHandler != null)
		{
			this.secondaryDrawHandler.Invoke();
		}
	}

	private void FinishHammerEffect()
	{
		this.fx = null;
		this.boundries.set_x(this.hammerFxDestinationX);
		this.boundries.set_y(this.hammerFxDestinationY);
		if (this.fxEnded != null)
		{
			this.fxEnded.Invoke();
			this.fxEnded = null;
		}
	}

	internal List<GuiButton> GetButtonGroup(byte groupId)
	{
		List<GuiButton> list = new List<GuiButton>();
		foreach (GuiElement element in this.elements)
		{
			if (!(element is GuiButton))
			{
				continue;
			}
			GuiButton guiButton = (GuiButton)element;
			if (guiButton.groupId != groupId)
			{
				continue;
			}
			list.Add(guiButton);
		}
		foreach (GuiElement clippingAreaElement in this.clippingAreaElements)
		{
			if (!(clippingAreaElement is GuiButton))
			{
				continue;
			}
			GuiButton guiButton1 = (GuiButton)clippingAreaElement;
			if (guiButton1.groupId != groupId)
			{
				continue;
			}
			list.Add(guiButton1);
		}
		return list;
	}

	public bool HasGuiElement(GuiElement element)
	{
		return (this.elements.Contains(element) ? true : this.clippingAreaElements.Contains(element));
	}

	private bool IsButtonClicked(GuiButton element)
	{
		if (element == null)
		{
			return false;
		}
		if (!element.clickEventOnUp && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) || element.clickEventOnUp && (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)))
		{
			return true;
		}
		return false;
	}

	public void MoveToTop(GuiElement el)
	{
		this.elements.Remove(el);
		this.elements.Add(el);
	}

	public virtual void OnClose()
	{
	}

	public void PutToCenter()
	{
		this.boundries.set_x((float)(Screen.get_width() / 2) - this.boundries.get_width() / 2f);
		this.boundries.set_y((float)(Screen.get_height() / 2) - this.boundries.get_height() / 2f);
	}

	public void PutToHorizontalCenter()
	{
		this.boundries.set_x((float)(Screen.get_width() / 2) - this.boundries.get_width() / 2f);
	}

	public void PutToVerticalCenter()
	{
		this.boundries.set_y((float)(Screen.get_height() / 2) - this.boundries.get_height() / 2f);
	}

	public void RemoveClippingBoundaries(byte id)
	{
		if (this.clippingBoundariesContainers != null && this.clippingBoundariesContainers.ContainsKey(id))
		{
			this.clippingBoundariesContainers.Remove(id);
		}
	}

	public void RemoveGuiElement(GuiElement element)
	{
		object obj = this.locker;
		Monitor.Enter(obj);
		try
		{
			if (element != null)
			{
				if (element.tooltipWindow != null)
				{
					AndromedaGui.gui.RemoveWindow(element.tooltipWindow.handler);
					element.tooltipWindow = null;
				}
				if ((object)this.lastMouseOver == (object)element)
				{
					this.lastMouseOver = null;
				}
				if (GuiFramework.draggingObject == element)
				{
					GuiFramework.draggingObject = null;
				}
				if (this.elements.Contains(element))
				{
					this.elements.Remove(element);
					if (element is GuiScrollingContainer)
					{
						GuiScrollingContainer guiScrollingContainer = (GuiScrollingContainer)element;
						guiScrollingContainer.Claer();
						this.RemoveClippingBoundaries(guiScrollingContainer.scrollerId);
					}
				}
				else if (this.clippingAreaElements.Contains(element))
				{
					this.clippingAreaElements.Remove(element);
				}
			}
		}
		finally
		{
			Monitor.Exit(obj);
		}
	}

	public void RemoveTooltipWindowOnElements()
	{
		foreach (GuiElement element in this.elements)
		{
			if (element.tooltipWindow == null)
			{
				continue;
			}
			AndromedaGui.gui.RemoveWindow(element.tooltipWindow.handler);
			element.tooltipWindow = null;
		}
		foreach (GuiElement clippingAreaElement in this.clippingAreaElements)
		{
			if (clippingAreaElement.tooltipWindow == null)
			{
				continue;
			}
			AndromedaGui.gui.RemoveWindow(clippingAreaElement.tooltipWindow.handler);
			clippingAreaElement.tooltipWindow = null;
		}
	}

	public ScrollingContainer ScrollingContainer(byte id)
	{
		ScrollingContainer scrollingContainer = null;
		this.clippingBoundariesContainers.TryGetValue(id, ref scrollingContainer);
		return scrollingContainer;
	}

	public void SetBackgroundTexture(string bundleName, string resourceName)
	{
		if (this.backgroundTexture != null)
		{
			this.backgroundTexture = null;
		}
		this.backgroundTexture = (Texture2D)playWebGame.assets.GetFromStaticSet(bundleName, resourceName);
		if (this.backgroundTexture != null)
		{
			this.boundries.set_width((float)this.backgroundTexture.get_width());
			this.boundries.set_height((float)this.backgroundTexture.get_height());
			return;
		}
		Debug.LogError(string.Concat("Could not load texture ", resourceName ?? "NULL"));
		this.backgroundTexture = new Texture2D(1, 1);
		this.backgroundTexture.SetPixel(0, 0, new Color(0f, 0f, 0f, 0f));
		this.backgroundTexture.Apply();
		this.boundries.set_width(904f);
		this.boundries.set_height(539f);
	}

	public void SetClippingBoundaries(byte id, Rect pos)
	{
		if (this.clippingBoundariesContainers == null)
		{
			this.clippingBoundariesContainers = new SortedList<byte, ScrollingContainer>();
		}
		this.clippingBoundariesContainers.Add(id, new ScrollingContainer()
		{
			boundaris = pos
		});
		this.isClippingAreaSet = true;
	}

	public void ShowWarninMessage(string warningText, Rect rect, int fontSize = 24, int blinkCount = 2)
	{
		if (blinkCount < 1)
		{
			blinkCount = 1;
		}
		if (this.warningMessage == null || !this.HasGuiElement(this.warningMessage))
		{
			this.blinkCnt = 0;
			this.deltaTime = 0f;
			this.blinkCountTarget = blinkCount;
			this.warningMessageShadow = new GuiLabel()
			{
				boundries = rect,
				text = warningText,
				X = this.warningMessageShadow.X + 2f,
				Y = this.warningMessageShadow.Y + 2f,
				Alignment = 4,
				Font = GuiLabel.FontBold,
				FontSize = fontSize,
				TextColor = GuiNewStyleBar.redColor
			};
			this.AddGuiElement(this.warningMessageShadow);
			this.warningMessage = new GuiLabel()
			{
				boundries = rect,
				text = warningText,
				Alignment = 4,
				Font = GuiLabel.FontBold,
				FontSize = fontSize,
				TextColor = GuiNewStyleBar.redColor
			};
			this.AddGuiElement(this.warningMessage);
			if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
			{
				AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "error");
				AudioManager.PlayGUISound(fromStaticSet);
			}
		}
	}

	public void StartHammerEffect(float destinationX, float destinationY)
	{
		this.hammerFxDestinationX = destinationX;
		this.hammerFxDestinationY = destinationY;
		this.timeHammerStart = DateTime.get_Now();
		this.timeHammerMove = this.timeHammerFx * this.moveToShakeRatio;
		this.timeHammerShake = this.timeHammerFx - this.timeHammerMove;
		this.hammerFxStartX = this.boundries.get_x();
		this.hammerFxStartY = this.boundries.get_y();
		float single = destinationX - this.hammerFxStartX;
		float single1 = destinationY - this.hammerFxStartY;
		this.hammerV0x = single / this.timeHammerMove * this.v0hammer;
		this.hammerV0y = single1 / this.timeHammerMove * this.v0hammer;
		this.hammerAccelX = 2f * (single - this.hammerV0x * this.timeHammerMove) / (this.timeHammerMove * this.timeHammerMove);
		this.hammerAccelY = 2f * (single1 - this.hammerV0y * this.timeHammerMove) / (this.timeHammerMove * this.timeHammerMove);
		float single2 = (float)Math.Sqrt((double)(single * single + single1 * single1));
		this.amplitudeHammerX = this.amplitudeHammerShake * single / single2;
		this.amplitudeHammerY = this.amplitudeHammerShake * single1 / single2;
		this.shakeHammerK = 3.14159274f / (float)(this.timeHammerShake / ((float)this.hammerShakes * 2f));
		this.fx = new Action(this, GuiWindow.DoHammerEffect);
	}

	public void StartMoveBy(float deltaX, float deltaY, float time, bool makeVisible)
	{
		this.destinationX = this.boundries.get_x() + deltaX;
		this.destinationY = this.boundries.get_y() + deltaY;
		this.moveToSpeedX = deltaX / time;
		this.moveToSpeedY = deltaY / time;
		this.moveByX = true;
		this.moveByY = true;
		this.visibleAfterEffect = makeVisible;
		this.fx = new Action(this, GuiWindow.DoMove);
	}

	public void StopHammerEffect()
	{
		this.fx = null;
	}

	public void Update(bool isActive)
	{
		this.isActiveWindow = isActive;
		if (isActive)
		{
			this.DistributeMouseUpEvent();
			this.DistributeMouseDownEvent();
		}
		this.DistributeHoverEffect();
		if (this.isClippingAreaSet && this.clippingBoundariesContainers.get_Count() > 0)
		{
			IEnumerator<byte> enumerator = this.clippingBoundariesContainers.get_Keys().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					byte current = enumerator.get_Current();
					if (this.clippingBoundariesContainers.get_Item(current).onUpdateCall == null)
					{
						continue;
					}
					this.clippingBoundariesContainers.get_Item(current).onUpdateCall.Invoke();
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
		if (this.fx == null)
		{
			return;
		}
		this.fx.Invoke();
	}

	public void UpdateWarning()
	{
		if (this.warningMessage == null)
		{
			return;
		}
		if (!this.isFadeingOut)
		{
			GuiWindow _deltaTime = this;
			_deltaTime.deltaTime = _deltaTime.deltaTime + Time.get_deltaTime();
			if (this.deltaTime >= 1f)
			{
				this.isFadeingOut = true;
				GuiWindow guiWindow = this;
				guiWindow.blinkCnt = guiWindow.blinkCnt + 1;
			}
		}
		else
		{
			GuiWindow _deltaTime1 = this;
			_deltaTime1.deltaTime = _deltaTime1.deltaTime - Time.get_deltaTime();
			if (this.deltaTime <= 0f)
			{
				this.isFadeingOut = false;
				if (this.blinkCnt >= this.blinkCountTarget)
				{
					this.RemoveGuiElement(this.warningMessage);
					this.RemoveGuiElement(this.warningMessageShadow);
					this.warningMessage = null;
					this.warningMessageShadow = null;
					return;
				}
			}
		}
		this.warningMessage.TextColor = new Color(1f, 0.0745f, 0.0745f, this.deltaTime);
		this.warningMessageShadow.TextColor = new Color(0f, 0f, 0f, this.deltaTime);
	}
}