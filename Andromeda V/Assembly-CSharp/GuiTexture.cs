using System;
using UnityEngine;

public class GuiTexture : GuiElement
{
	protected Texture2D texture;

	public GuiTexture()
	{
	}

	public void ClearTexture()
	{
		this.texture = null;
	}

	public override void DrawGuiElement()
	{
		if (this.texture == null)
		{
			return;
		}
		!this.IsMouseOver;
		GUI.DrawTexture(this.boundries, this.texture);
	}

	public Texture2D GetTexture2D()
	{
		return this.texture;
	}

	public void SetItemTexture(ushort itemType)
	{
		string str = null;
		string str1 = null;
		PlayerItems.SetAvatar(itemType, ref str, ref str1);
		this.SetTexture(str, str1);
	}

	public void SetItemTextureKeepSize(ushort itemType)
	{
		string str = null;
		string str1 = null;
		PlayerItems.SetAvatar(itemType, ref str, ref str1);
		this.SetTextureKeepSize(str, str1);
	}

	public void SetSize(float w, float h)
	{
		this.boundries.set_width(w);
		this.boundries.set_height(h);
	}

	public void SetTexture(string bundleName, string resourceName)
	{
		this.texture = (Texture2D)playWebGame.assets.GetFromStaticSet(bundleName, resourceName);
		if (this.texture == null)
		{
			Debug.LogError(string.Concat("Could not load texture ", resourceName ?? "NULL"));
			this.texture = new Texture2D(1, 1);
			this.texture.SetPixel(0, 0, new Color(0f, 0f, 0f, 0f));
			this.texture.Apply();
		}
		this.boundries.set_width((float)this.texture.get_width());
		this.boundries.set_height((float)this.texture.get_height());
	}

	public void SetTexture(Texture2D tx)
	{
		this.texture = tx;
		this.boundries.set_width((float)this.texture.get_width());
		this.boundries.set_height((float)this.texture.get_height());
	}

	public void SetTexture2D(Texture2D newTexture)
	{
		this.texture = newTexture;
		this.boundries.set_width((float)this.texture.get_width());
		this.boundries.set_height((float)this.texture.get_height());
	}

	public void SetTextureKeepSize(string bundleName, string resourceName)
	{
		this.texture = (Texture2D)playWebGame.assets.GetFromStaticSet(bundleName, resourceName);
		if (this.texture == null)
		{
			Debug.LogError(string.Concat("Cannot load texture ", resourceName ?? "NULL"));
			this.texture = new Texture2D(1, 1);
			this.texture.SetPixel(0, 0, new Color(0f, 0f, 0f, 0f));
			this.texture.Apply();
		}
	}

	public void SetTextureKeepSize(Texture2D tx)
	{
		this.texture = tx;
		if (this.texture == null)
		{
			Debug.LogError("Got NULL texture!");
			this.texture = new Texture2D(1, 1);
			this.texture.SetPixel(0, 0, new Color(0f, 0f, 0f, 0f));
			this.texture.Apply();
		}
	}
}