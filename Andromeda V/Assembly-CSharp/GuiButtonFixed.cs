using System;
using UnityEngine;

public class GuiButtonFixed : GuiButton
{
	private Texture2D trNormal;

	private Texture2D trHover;

	private Texture2D trClicked;

	private Texture2D trDisabled;

	public GuiButtonFixed()
	{
	}

	public override void DrawGuiElement()
	{
		this._state = base.State;
		switch (this._state)
		{
			case GuiButton.BtnState.Normal:
			{
				if (this.trNormal != null)
				{
					GUI.DrawTexture(this.boundries, this.trNormal);
				}
				else
				{
					Debug.Log("trNormal == null");
				}
				break;
			}
			case GuiButton.BtnState.Hover:
			{
				GUI.DrawTexture(this.boundries, this.trHover);
				break;
			}
			case GuiButton.BtnState.LeftClicked:
			case GuiButton.BtnState.RightClicked:
			{
				GUI.DrawTexture(this.boundries, this.trClicked);
				break;
			}
			case GuiButton.BtnState.Disabled:
			{
				GUI.DrawTexture(this.boundries, this.trDisabled);
				break;
			}
			default:
			{
				goto case GuiButton.BtnState.Normal;
			}
		}
		base.DrawGuiElement();
	}

	public void SetTexture(string bundleName, string assetName)
	{
		if (!playWebGame.TryGetTextureFromStaticSet(bundleName, string.Concat(assetName, "Nml"), out this.trNormal) && !playWebGame.TryGetTextureFromStaticSet(bundleName, assetName, out this.trNormal))
		{
			throw new Exception(string.Concat(new string[] { "Texture ", assetName, " or ", assetName, "Nml was not found!" }));
		}
		if (!playWebGame.TryGetTextureFromStaticSet(bundleName, string.Concat(assetName, "Hvr"), out this.trHover))
		{
			this.trHover = this.trNormal;
		}
		if (!playWebGame.TryGetTextureFromStaticSet(bundleName, string.Concat(assetName, "Clk"), out this.trClicked))
		{
			this.trClicked = this.trNormal;
		}
		if (!playWebGame.TryGetTextureFromStaticSet(bundleName, string.Concat(assetName, "Dsb"), out this.trDisabled))
		{
			this.trDisabled = this.trNormal;
		}
		this.boundries.set_width((float)this.trNormal.get_width());
		this.boundries.set_height((float)this.trNormal.get_height());
	}

	public void SetTextureClicked(string bundleName, string assetName)
	{
		this.trClicked = (Texture2D)playWebGame.assets.GetFromStaticSet(bundleName, assetName);
	}

	public void SetTextureClicked(Texture2D txt)
	{
		this.trClicked = txt;
	}

	public void SetTextureDisabled(string bundleName, string assetName)
	{
		this.trDisabled = (Texture2D)playWebGame.assets.GetFromStaticSet(bundleName, assetName);
		if (this.trDisabled == null)
		{
			throw new Exception(string.Format("Could not load texture {0} / {1}!", bundleName, assetName));
		}
	}

	public void SetTextureDisabled(Texture2D txt)
	{
		this.trDisabled = txt;
		if (this.trDisabled == null)
		{
			throw new Exception(string.Format("Could not load texture Disable", new object[0]));
		}
	}

	public void SetTextureHover(string bundleName, string assetName)
	{
		this.trHover = (Texture2D)playWebGame.assets.GetFromStaticSet(bundleName, assetName);
		if (this.trHover == null)
		{
			throw new Exception(string.Format("Could not load texture {0} / {1}!", bundleName, assetName));
		}
	}

	public void SetTextureHover(Texture2D txt)
	{
		this.trHover = txt;
		if (this.trHover == null)
		{
			throw new Exception(string.Format("Could not load texture Hover", new object[0]));
		}
	}

	public void SetTextureNormal(string bundleName, string assetName)
	{
		this.trNormal = (Texture2D)playWebGame.assets.GetFromStaticSet(bundleName, assetName);
		if (this.trNormal == null)
		{
			throw new Exception(string.Format("Could not load texture {0} / {1}!", bundleName, assetName));
		}
	}

	public void SetTextureNormal(Texture2D txt, bool isDefault = false)
	{
		this.trNormal = txt;
		if (this.trNormal == null)
		{
			throw new Exception(string.Format("Could not load texture Normal", new object[0]));
		}
		if (isDefault)
		{
			this.SetTextureDisabled(txt);
			this.SetTextureHover(txt);
			this.SetTextureClicked(txt);
		}
		this.boundries.set_width((float)this.trNormal.get_width());
		this.boundries.set_height((float)this.trNormal.get_height());
	}
}