using System;
using UnityEngine;

public class LoadingAsset
{
	public BundleType bundleType;

	public string assetName;

	public string displayName;

	public WWW www;

	public bool doAutoResourcesFind;

	public bool isLevel;

	public LoadAssetState state;

	public int size = 1000000;

	public bool isUrlPicture;

	public Action JobDone;

	public int assetVersion;

	public LoadingAsset()
	{
	}

	public void ManageCurrentLoad()
	{
		if (!playWebGame.loadResFromAssets)
		{
			this.state = LoadAssetState.Loaded;
		}
		else
		{
			string.Format("Progress: {0} name: {1} isDone: {2} error: {3} url: {4}", new object[] { this.www.get_progress(), this.assetName, this.www.get_isDone(), this.www.get_error(), this.www.get_url() });
			if (this.www.get_progress() == 1f && this.www.get_isDone())
			{
				if (this.www.get_assetBundle() == null)
				{
					Debug.Log("CP6");
					Debug.LogError(string.Concat("Error loading bundle ", this.assetName, this.www.get_error() ?? "*"));
					this.www = new WWW(this.www.get_url());
					this.state = LoadAssetState.Loading;
				}
				else
				{
					this.state = LoadAssetState.Loaded;
				}
				return;
			}
		}
	}
}