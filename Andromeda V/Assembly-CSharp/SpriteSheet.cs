using System;
using UnityEngine;

public class SpriteSheet : MonoBehaviour
{
	public int _uvTieX = 1;

	public int _uvTieY = 1;

	public int _fps = 10;

	private Vector2 _size;

	private Renderer _myRenderer;

	private int _lastIndex = -1;

	public SpriteSheet()
	{
	}

	private void Start()
	{
		this._size = new Vector2(1f / (float)this._uvTieX, 1f / (float)this._uvTieY);
		this._myRenderer = base.get_renderer();
		if (this._myRenderer == null)
		{
			base.set_enabled(false);
		}
	}

	private void Update()
	{
		int _timeSinceLevelLoad = (int)(Time.get_timeSinceLevelLoad() * (float)this._fps) % (this._uvTieX * this._uvTieY);
		if (_timeSinceLevelLoad != this._lastIndex)
		{
			int num = 1;
			int num1 = _timeSinceLevelLoad;
			Vector2 vector2 = new Vector2((float)num * this._size.x, 1f - this._size.y - (float)num1 * this._size.y);
			this._myRenderer.get_material().SetTextureOffset("_MainTex", vector2);
			this._myRenderer.get_material().SetTextureScale("_MainTex", this._size);
			this._lastIndex = _timeSinceLevelLoad;
		}
	}
}