using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiTextureDraggable : GuiTexture
{
	private DragState dragState;

	private SortedList<int, GuiDropZone> dropZones = new SortedList<int, GuiDropZone>();

	private Vector2 dragPosition = new Vector2();

	private float destination;

	private float startinSpeed = 1000f;

	private float acceleration = 700f;

	private double timeForTravel;

	private float dx;

	private float dy;

	public Action<object> RightClickAction;

	public object rightClickActionParam;

	public bool leftClickRightAction;

	public bool isStackable = true;

	private GuiWindow stackableWnd;

	private GuiHorizontalSlider sliderStack;

	private GuiLabel stackItemName;

	private GuiLabel stackItemAmount;

	public bool isUnavailable;

	private int hoverZone = -1;

	private float mouseX;

	private float mouseY;

	private int dragTextureWidth;

	private int dragTextureHeight;

	public static float GET_HOME_SPEED;

	public static float GOT_HOME_DISTANCE;

	private Texture2D txDropZone;

	private Texture2D txDropZoneHover;

	private Texture2D txDrag;

	private Texture2D txSource;

	private float mouseOffsetX;

	private float mouseOffsetY;

	public Action<int, int> dropped;

	public int callbackAttribute = -1;

	public int mainTextureOffsetX;

	public int mainTextureOffsetY = 6;

	public int sourceTextureOffsetX;

	public int sourceTextureOffsetY;

	private DateTime startGetHomeTime;

	private bool IsMouseInSourceRectangle
	{
		get
		{
			Vector2 mousePosition = this.container.MousePosition;
			return this.boundries.Contains(mousePosition);
		}
	}

	static GuiTextureDraggable()
	{
		GuiTextureDraggable.GET_HOME_SPEED = 580f;
		GuiTextureDraggable.GOT_HOME_DISTANCE = 4f;
	}

	public GuiTextureDraggable()
	{
		this.isHoverAware = true;
	}

	public void AddDropZone(Vector2 position, int key, Texture2D texture, Texture2D textureDrop)
	{
		GuiDropZone guiDropZone = new GuiDropZone()
		{
			texture = texture,
			textureHover = textureDrop,
			position = position,
			dropCallbackKey = key
		};
		this.dropZones.Add(key, guiDropZone);
	}

	public void CancelDropStackable()
	{
		this.hoverZone = -1;
		this.StopDrag();
	}

	public void CleanDropZones()
	{
		this.dropZones.Clear();
	}

	public bool ContainsDropZone(int key)
	{
		return this.dropZones.ContainsKey(key);
	}

	public override void DrawGuiElement()
	{
		bool isMouseOver = this.IsMouseOver;
		Color _white = Color.get_white();
		if (this.isUnavailable)
		{
			_white = GUI.get_color();
			GUI.set_color(new Color(1f, 1f, 1f, 0.4f));
		}
		switch (this.dragState)
		{
			case DragState.Idle:
			{
				if (this.texture != null)
				{
					if (this.IsMouseInSourceRectangle && GuiFramework.draggingObject == null && this.txSource != null)
					{
						GUI.DrawTexture(new Rect(this.boundries.get_x() + (float)this.sourceTextureOffsetX, this.boundries.get_y() + (float)this.sourceTextureOffsetY, (float)this.txSource.get_width(), (float)this.txSource.get_height()), this.txSource);
					}
					GUI.DrawTexture(new Rect(this.boundries.get_x() + (float)this.mainTextureOffsetX, this.boundries.get_y() + (float)this.mainTextureOffsetY, this.boundries.get_width(), this.boundries.get_height()), this.texture);
				}
				break;
			}
			case DragState.Drag:
			{
				this.dragPosition = this.container.MousePosition;
				this.dragPosition.x = this.dragPosition.x - this.mouseOffsetX;
				this.dragPosition.y = this.dragPosition.y - this.mouseOffsetY;
				GUI.DrawTexture(new Rect(this.boundries.get_x() + (float)this.sourceTextureOffsetX, this.boundries.get_y() + (float)this.sourceTextureOffsetY, (float)this.txSource.get_width(), (float)this.txSource.get_height()), this.txSource);
				this.DrawTargetTextures();
				GUI.DrawTexture(new Rect(this.dragPosition.x + (float)this.mainTextureOffsetX, this.dragPosition.y + (float)this.mainTextureOffsetY, (float)this.dragTextureWidth, (float)this.dragTextureHeight), this.txDrag);
				break;
			}
			case DragState.StackPopUp:
			{
				break;
			}
			case DragState.GettingHome:
			{
				GUI.DrawTexture(new Rect(this.dragPosition.x + (float)this.mainTextureOffsetX, this.dragPosition.y + (float)this.mainTextureOffsetY, (float)this.dragTextureWidth, (float)this.dragTextureHeight), this.txDrag);
				if (DateTime.get_Now() >= this.startGetHomeTime.AddSeconds(this.timeForTravel))
				{
					GuiFramework.draggingObject = null;
					this.dragState = DragState.Idle;
					return;
				}
				TimeSpan now = DateTime.get_Now() - this.startGetHomeTime;
				float totalSeconds = (float)now.get_TotalSeconds();
				float single = this.startinSpeed * totalSeconds + this.acceleration * totalSeconds * totalSeconds;
				float single1 = single / this.destination;
				this.dragPosition.x = this.boundries.get_x() + this.dx * (1f - single1);
				this.dragPosition.y = this.boundries.get_y() + this.dy * (1f - single1);
				break;
			}
			default:
			{
				goto case DragState.Idle;
			}
		}
		if (this.isUnavailable)
		{
			GUI.set_color(_white);
		}
	}

	private void DrawTargetTextures()
	{
		this.hoverZone = -1;
		Vector3 _mousePosition = Input.get_mousePosition();
		this.mouseY = (float)Screen.get_height() - _mousePosition.y;
		this.mouseX = _mousePosition.x;
		IEnumerator<GuiDropZone> enumerator = this.dropZones.get_Values().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				GuiDropZone current = enumerator.get_Current();
				Vector2 vector2 = current.position;
				if (!this.IsHoverOverDropZone(current))
				{
					GUI.DrawTexture(new Rect(vector2.x, vector2.y, (float)current.texture.get_width(), (float)current.texture.get_height()), current.texture);
				}
				else
				{
					this.hoverZone = current.dropCallbackKey;
					GUI.DrawTexture(new Rect(vector2.x, vector2.y, (float)current.textureHover.get_width(), (float)current.textureHover.get_height()), current.textureHover);
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
	}

	public void DropStackable()
	{
		this.dragState = DragState.Idle;
	}

	public Texture2D GetTextureSource()
	{
		return this.txDrag;
	}

	private bool IsHoverOverDropZone(GuiDropZone zone)
	{
		Rect rect = new Rect(this.container.boundries.get_x() + zone.position.x, this.container.boundries.get_y() + zone.position.y, (float)zone.texture.get_width(), (float)zone.texture.get_height());
		return rect.Contains(new Vector2(this.mouseX, this.mouseY));
	}

	public void RemoveDropZone(int key)
	{
		this.dropZones.Remove(key);
	}

	public void SetDragTextureSize(int width, int height)
	{
		this.dragTextureWidth = width;
		this.dragTextureHeight = height;
	}

	public void SetTextureDrag(string bundleName, string resourceName)
	{
		this.txDrag = (Texture2D)playWebGame.assets.GetFromStaticSet(bundleName, resourceName);
		if (this.txDrag == null)
		{
			Debug.LogError(string.Concat("Could not find texture ", resourceName ?? "NULL", " in bundle:", bundleName ?? "NULL"));
			this.txDrag = new Texture2D(1, 1);
			this.txDrag.SetPixel(0, 0, new Color(0f, 0f, 0f, 0f));
			this.txDrag.Apply();
		}
		if (this.dragTextureWidth == 0)
		{
			this.dragTextureWidth = this.txDrag.get_width();
			this.dragTextureHeight = this.txDrag.get_height();
		}
	}

	public void SetTextureDropZone(string bundleName, string resourceName)
	{
		this.txDropZone = (Texture2D)playWebGame.assets.GetFromStaticSet(bundleName, resourceName);
		if (this.txDropZone == null)
		{
			Debug.LogError(string.Concat("Could not find texture ", bundleName ?? "NULL", resourceName ?? "NULL"));
			this.txDropZone = new Texture2D(1, 1);
			this.txDropZone.SetPixel(0, 0, new Color(0f, 0f, 0f, 0f));
			this.txDropZone.Apply();
		}
	}

	public void SetTextureDropZoneHover(string bundleName, string resourceName)
	{
		this.txDropZoneHover = (Texture2D)playWebGame.assets.GetFromStaticSet(bundleName, resourceName);
		if (this.txDropZoneHover == null)
		{
			Debug.LogError(string.Concat("Could not find texture ", bundleName ?? "NULL", resourceName ?? "NULL"));
			this.txDropZoneHover = new Texture2D(1, 1);
			this.txDropZoneHover.SetPixel(0, 0, new Color(0f, 0f, 0f, 0f));
			this.txDropZoneHover.Apply();
		}
	}

	public void SetTextureSource(string bundleName, string resourceName)
	{
		this.txSource = (Texture2D)playWebGame.assets.GetFromStaticSet(bundleName, resourceName);
		if (this.txSource == null)
		{
			Debug.LogError(string.Concat("Could not find texture ", resourceName ?? "NULL", " in bundle:", bundleName ?? "NULL"));
			this.txSource = new Texture2D(1, 1);
			this.txSource.SetPixel(0, 0, new Color(0f, 0f, 0f, 0f));
			this.txSource.Apply();
		}
	}

	public void SetTextureSource(Texture2D tx)
	{
		this.txSource = tx;
	}

	public void StartDrag()
	{
		GuiFramework.draggingObject = this;
		if (this.txDrag == null)
		{
			this.txDrag = this.texture;
			if (this.dragTextureWidth == 0)
			{
				this.dragTextureWidth = this.txDrag.get_width();
				this.dragTextureHeight = this.txDrag.get_height();
			}
		}
		Vector2 mousePosition = this.container.MousePosition;
		this.mouseOffsetX = mousePosition.x - this.boundries.get_x();
		this.mouseOffsetY = mousePosition.y - this.boundries.get_y();
		this.dragPosition.x = this.boundries.get_x();
		this.dragPosition.y = this.boundries.get_y();
		this.dragState = DragState.Drag;
	}

	public void StopDrag()
	{
		if (this.hoverZone == -1)
		{
			this.startGetHomeTime = DateTime.get_Now();
			this.dx = this.dragPosition.x - this.boundries.get_x();
			this.dy = this.dragPosition.y - this.boundries.get_y();
			this.destination = (float)Math.Sqrt((double)(this.dx * this.dx + this.dy * this.dy));
			this.timeForTravel = ((double)(-this.startinSpeed) + Math.Sqrt((double)(this.startinSpeed * this.startinSpeed + 4f * this.acceleration * this.destination))) / (double)(2f * this.acceleration);
			this.dragState = DragState.GettingHome;
		}
		else if (!this.isStackable)
		{
			this.dragState = DragState.Idle;
			GuiFramework.draggingObject = null;
			if (this.dropped != null)
			{
				this.dropped.Invoke(this.hoverZone, this.callbackAttribute);
			}
		}
		else
		{
			this.dragState = DragState.StackPopUp;
			GuiFramework.draggingObject = null;
			if (this.dropped != null)
			{
				this.dropped.Invoke(this.hoverZone, this.callbackAttribute);
			}
		}
	}
}