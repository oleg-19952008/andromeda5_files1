using System;
using UnityEngine;

public class GuiTextureAnimated : GuiElement
{
	private Action drawAction;

	public Action<GuiTextureAnimated> finishDraw;

	private Texture2D staticTexture;

	private Texture2D[] textures;

	private WWW www;

	private string dynamicAssetName;

	private string staticAssetName;

	public float rotationTime = 2f;

	private DateTime firstFrameTime;

	public GuiTextureAnimated()
	{
		this.drawAction = new Action(this, GuiTextureAnimated.DrawStatic);
	}

	private void DrawDynamic()
	{
		DateTime now = DateTime.get_Now();
		if (now > this.firstFrameTime.AddSeconds((double)this.rotationTime) && this.finishDraw != null)
		{
			this.finishDraw.Invoke(this);
			return;
		}
		if ((int)this.textures.Length == 1)
		{
			GUI.DrawTexture(this.boundries, this.textures[0]);
			return;
		}
		TimeSpan timeSpan = now - this.firstFrameTime;
		int totalSeconds = (int)(timeSpan.get_TotalSeconds() / (double)this.rotationTime);
		this.firstFrameTime = this.firstFrameTime.AddSeconds((double)((float)totalSeconds * this.rotationTime));
		TimeSpan timeSpan1 = now - this.firstFrameTime;
		int num = (int)(timeSpan1.get_TotalSeconds() / (double)this.rotationTime * (double)((int)this.textures.Length));
		GUI.DrawTexture(this.boundries, this.textures[num]);
	}

	public override void DrawGuiElement()
	{
		this.drawAction.Invoke();
	}

	private void DrawStatic()
	{
		if (!playWebGame.assets.IsBundleLoaded(this.dynamicAssetName))
		{
			GUI.DrawTexture(this.boundries, this.staticTexture);
		}
		else
		{
			this.SwitchToDynamic();
			GUI.DrawTexture(this.boundries, this.textures[0]);
		}
	}

	public void Init(string staticBundleName, string dynamicAssetName, string staticAssetName)
	{
		this.drawAction = new Action(this, GuiTextureAnimated.DrawStatic);
		this.dynamicAssetName = dynamicAssetName;
		this.staticAssetName = staticAssetName;
		if (playWebGame.assets.IsBundleLoaded(dynamicAssetName))
		{
			this.SwitchToDynamic();
			return;
		}
		this.staticTexture = (Texture2D)playWebGame.assets.GetFromStaticSet(staticBundleName, staticAssetName);
		if (this.staticTexture != null)
		{
			this.boundries.set_width((float)this.staticTexture.get_width());
			this.boundries.set_height((float)this.staticTexture.get_height());
			return;
		}
		Debug.LogError(string.Concat("Could not load texture ", this.staticAssetName));
		this.staticTexture = new Texture2D(1, 1);
		this.staticTexture.SetPixel(0, 0, new Color(0f, 0f, 0f, 0f));
		this.staticTexture.Apply();
	}

	private void SwitchToDynamic()
	{
		this.firstFrameTime = DateTime.get_Now();
		this.textures = playWebGame.assets.GetAnimationSet(this.dynamicAssetName);
		this.boundries.set_width((float)this.textures[0].get_width());
		this.boundries.set_height((float)this.textures[0].get_height());
		this.drawAction = new Action(this, GuiTextureAnimated.DrawDynamic);
	}
}