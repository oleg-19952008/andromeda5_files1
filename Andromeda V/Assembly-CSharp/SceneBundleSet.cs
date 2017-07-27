using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneBundleSet
{
	public AsyncOperation sceneOperation;

	public string sceneName;

	public LoadingAsset[] bundles;

	public bool isLoaded;

	public int currentBundle;

	public SceneBundleSet(string sceneName, params object[] sets)
	{
		this.sceneName = sceneName;
		this.isLoaded = true;
		List<string> list = new List<string>();
		object[] objArray = sets;
		for (int i = 0; i < (int)objArray.Length; i++)
		{
			list.AddRange((string[])objArray[i]);
		}
		this.bundles = new LoadingAsset[list.get_Count()];
		for (int j = 0; j < (int)this.bundles.Length; j++)
		{
			if (!playWebGame.allBundles.TryGetValue(list.get_Item(j), ref this.bundles[j]))
			{
				Debug.LogError(string.Concat("Could not load ", list.get_Item(j)));
				return;
			}
			this.bundles[j] = playWebGame.allBundles.get_Item(list.get_Item(j));
			this.isLoaded = this.isLoaded & this.bundles[j].state == LoadAssetState.Loaded;
		}
		!this.isLoaded;
	}

	public void StartLoadLevel()
	{
		Application.ExternalCall("logState", new object[] { 8, playWebGame.timeSinceStart });
		this.sceneOperation = Application.LoadLevelAsync(this.sceneName);
	}
}