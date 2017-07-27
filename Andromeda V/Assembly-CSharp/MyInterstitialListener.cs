using System;
using UnityEngine;

public class MyInterstitialListener : ITapForTapInterstitial
{
	public Action EndAction;

	public MyInterstitialListener()
	{
	}

	public void OnDismiss()
	{
		Debug.Log("Called my interstitial listener OnDismiss");
		if (this.EndAction != null)
		{
			this.EndAction.Invoke();
		}
	}

	public void OnFail(string reason)
	{
		Debug.Log(string.Concat("OnFail Ad : ", reason));
		if (this.EndAction != null)
		{
			this.EndAction.Invoke();
		}
	}

	public void OnReceive()
	{
		Debug.Log("OnReceive");
	}

	public void OnShow()
	{
		Debug.Log("OnShow");
	}

	public void OnTap()
	{
		Debug.Log("OnTap");
	}
}