using System;
using UnityEngine;

public class PlayTexture : MonoBehaviour
{
	private Texture[] Frames;

	public string TexturePrefics = "Destroyer";

	public string TextureFolder = "Destroyer";

	public int Caliber = 3;

	private int _curIndex;

	public int fps = 60;

	public bool bLoop = true;

	public float nextLoop;

	private float _nextTime;

	private GUITexture _gt;

	private Material _mt;

	private Texture2D[] locFrames;

	private bool bStarted;

	private bool _finished;

	public PlayTexture()
	{
	}

	private void Awake()
	{
		this._curIndex = 0;
		this._nextTime = 0f;
		this._gt = base.get_gameObject().GetComponent(typeof(GUITexture)) as GUITexture;
		if (this._gt == null && base.get_renderer() != null)
		{
			this._mt = base.get_renderer().get_material();
		}
		this.SetTextureBundle(this.TextureFolder, this.TexturePrefics);
		this.bStarted = true;
		this.Update();
	}

	private void OnGUI()
	{
	}

	private string Right(string param, int l)
	{
		return param.Substring(param.get_Length() - l, l);
	}

	public void SetTextureBundle(string __folder, string __prefix)
	{
		if (this.TextureFolder == __folder && this.TexturePrefics == __prefix && this.bStarted)
		{
			return;
		}
		Texture[] textureArray = new Texture[1000];
		int num = 0;
		int num1 = 1;
		while (num1 < 1000)
		{
			string str = string.Concat("0000", num1.ToString());
			str = str.Substring(str.get_Length() - this.Caliber, this.Caliber);
			if (__folder.EndsWith("/"))
			{
				__folder = __folder.Replace("/", string.Empty);
			}
			Texture2D fromStaticSet = null;
			if (!playWebGame.TryGetTextureFromStaticSet(__folder, string.Concat(__prefix, "_", str), out fromStaticSet))
			{
				if (num1 == 1)
				{
					Debug.LogError(string.Concat(new string[] { "From game object: ", base.get_gameObject().get_name(), "; Bundle=", __folder, "; Asset=", __prefix, "_", str }));
				}
				num = num1 - 1;
				break;
			}
			else
			{
				fromStaticSet = (Texture2D)playWebGame.assets.GetFromStaticSet(__folder, string.Concat(__prefix, "_", str));
				textureArray[num1 - 1] = fromStaticSet;
				num1++;
			}
		}
		this._finished = true;
		this.Frames = new Texture2D[num];
		Array.Copy(textureArray, this.Frames, num);
		this.TextureFolder = __folder;
		this.TexturePrefics = __prefix;
		if (num != 0)
		{
			if (this._gt != null)
			{
				this._gt.set_enabled(true);
			}
			if (this._mt != null)
			{
				this._mt.set_mainTexture(null);
			}
		}
		else
		{
			if (this._gt != null)
			{
				this._gt.set_enabled(false);
			}
			if (this._mt != null)
			{
				this._mt.set_mainTexture(null);
			}
		}
		if (num == 0)
		{
			return;
		}
		this._finished = false;
	}

	public void SetTexturePref(string __folder, string __prefix)
	{
		Texture2D texture2D;
		if (this.TextureFolder == __folder && this.TexturePrefics == __prefix && this.bStarted)
		{
			return;
		}
		Texture2D[] texture2DArray = new Texture2D[1000];
		int num = 0;
		int num1 = 1;
		while (num1 < 1000)
		{
			string str = string.Concat("0000", num1.ToString());
			str = str.Substring(str.get_Length() - this.Caliber, this.Caliber);
			if (__folder.EndsWith("/"))
			{
				__folder = __folder.Replace("/", string.Empty);
			}
			if (!playWebGame.TryGetTextureFromStaticSet(__folder, string.Concat(__prefix, "_", str), out texture2D))
			{
				num = num1 - 1;
				break;
			}
			else
			{
				texture2DArray[num1 - 1] = texture2D;
				num1++;
			}
		}
		this._finished = true;
		this.Frames = new Texture2D[num];
		Array.Copy(texture2DArray, this.Frames, num);
		this.TextureFolder = __folder;
		this.TexturePrefics = __prefix;
		if (num != 0)
		{
			if (this._gt != null)
			{
				this._gt.set_enabled(true);
			}
			if (this._mt != null)
			{
				this._mt.set_mainTexture(null);
			}
		}
		else
		{
			if (this._gt != null)
			{
				this._gt.set_enabled(false);
			}
			if (this._mt != null)
			{
				this._mt.set_mainTexture(null);
			}
		}
		if (num == 0)
		{
			return;
		}
		this._finished = false;
	}

	public static void Stop()
	{
		GameObject gameObject = GameObject.Find("PlayTextures");
		if (gameObject != null)
		{
			PlayTexture component = gameObject.GetComponent(typeof(PlayTexture)) as PlayTexture;
			component.set_enabled(false);
			component.get_gameObject().get_guiTexture().set_enabled(false);
		}
	}

	private void Update()
	{
		if ((int)this.Frames.Length == 0)
		{
			return;
		}
		if (Time.get_time() < this._nextTime)
		{
			return;
		}
		if (this._curIndex >= (int)this.Frames.Length)
		{
			this._curIndex = 0;
		}
		if (this._gt != null)
		{
			this.UpdateGUITexture();
		}
		if (this._mt != null)
		{
			this.UpdateMatTexture();
		}
		this._nextTime = Time.get_time() + 1f / (float)this.fps;
		PlayTexture playTexture = this;
		playTexture._curIndex = playTexture._curIndex + 1;
		if (this._curIndex >= (int)this.Frames.Length)
		{
			this._finished = !this.bLoop;
			this._curIndex = 0;
			this._nextTime = Time.get_time() + this.nextLoop + 1f / (float)this.fps;
			if (this._gt != null && this.nextLoop > 0f)
			{
				this._gt.set_texture(null);
			}
			if (this._mt != null && this.nextLoop > 0f)
			{
				this._mt.set_mainTexture(null);
			}
		}
	}

	private void UpdateGUITexture()
	{
		if (!this._finished)
		{
			this._gt.set_enabled(true);
			this._gt.set_texture(this.Frames[this._curIndex]);
		}
		else
		{
			this._gt.set_enabled(false);
			this._gt.set_texture(null);
		}
	}

	private void UpdateMatTexture()
	{
		if (!this._finished)
		{
			base.get_renderer().set_enabled(true);
			this._mt.set_mainTexture(this.Frames[this._curIndex]);
		}
		else
		{
			base.get_renderer().set_enabled(false);
			this._mt.set_mainTexture(null);
		}
	}
}