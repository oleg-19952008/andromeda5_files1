using System;
using System.Collections.Generic;
using UnityEngine;

public class TapjoySample : MonoBehaviour, ITapjoyEvent
{
	private static string ENABLE_LOGGING_IOS;

	private static string ENABLE_LOGGING_ANDROID;

	private string tapPointsLabel = string.Empty;

	private bool autoRefresh;

	private bool viewIsShowing;

	private TapjoyEvent sampleEvent;

	static TapjoySample()
	{
		TapjoySample.ENABLE_LOGGING_IOS = "TJC_OPTION_ENABLE_LOGGING";
		TapjoySample.ENABLE_LOGGING_ANDROID = "enable_logging";
	}

	public TapjoySample()
	{
	}

	private void Awake()
	{
		Debug.Log("C#: Awaking and adding Tapjoy Events");
		TapjoyPlugin.connectCallSucceeded += new Action(this, TapjoySample.HandleTapjoyConnectSuccess);
		TapjoyPlugin.connectCallFailed += new Action(this, TapjoySample.HandleTapjoyConnectFailed);
		TapjoyPlugin.getTapPointsSucceeded += new Action<int>(this, TapjoySample.HandleGetTapPointsSucceeded);
		TapjoyPlugin.getTapPointsFailed += new Action(this, TapjoySample.HandleGetTapPointsFailed);
		TapjoyPlugin.spendTapPointsSucceeded += new Action<int>(this, TapjoySample.HandleSpendTapPointsSucceeded);
		TapjoyPlugin.spendTapPointsFailed += new Action(this, TapjoySample.HandleSpendTapPointsFailed);
		TapjoyPlugin.awardTapPointsSucceeded += new Action(this, TapjoySample.HandleAwardTapPointsSucceeded);
		TapjoyPlugin.awardTapPointsFailed += new Action(this, TapjoySample.HandleAwardTapPointsFailed);
		TapjoyPlugin.tapPointsEarned += new Action<int>(this, TapjoySample.HandleTapPointsEarned);
		TapjoyPlugin.getFullScreenAdSucceeded += new Action(this, TapjoySample.HandleGetFullScreenAdSucceeded);
		TapjoyPlugin.getFullScreenAdFailed += new Action(this, TapjoySample.HandleGetFullScreenAdFailed);
		TapjoyPlugin.getDisplayAdSucceeded += new Action(this, TapjoySample.HandleGetDisplayAdSucceeded);
		TapjoyPlugin.getDisplayAdFailed += new Action(this, TapjoySample.HandleGetDisplayAdFailed);
		TapjoyPlugin.videoAdStarted += new Action(this, TapjoySample.HandleVideoAdStarted);
		TapjoyPlugin.videoAdFailed += new Action(this, TapjoySample.HandleVideoAdFailed);
		TapjoyPlugin.videoAdCompleted += new Action(this, TapjoySample.HandleVideoAdCompleted);
		TapjoyPlugin.viewOpened += new Action<TapjoyViewType>(this, TapjoySample.HandleViewOpened);
		TapjoyPlugin.viewClosed += new Action<TapjoyViewType>(this, TapjoySample.HandleViewClosed);
		TapjoyPlugin.showOffersFailed += new Action(this, TapjoySample.HandleShowOffersFailed);
	}

	public void ContentDidAppear(TapjoyEvent tapjoyEvent)
	{
		Debug.Log("C#: ContentDidAppear");
	}

	public void ContentDidDisappear(TapjoyEvent tapjoyEvent)
	{
		Debug.Log("C#: ContentDidDisappear");
	}

	public void DidRequestAction(TapjoyEvent tapjoyEvent, TapjoyEventRequest request)
	{
		Debug.Log(string.Concat(new object[] { "C#: DidRequestAction type:", request.type, ", identifier:", request.identifier, ", quantity:", request.quantity }));
		request.EventRequestCompleted();
	}

	public void HandleAwardTapPointsFailed()
	{
		Debug.Log("C#: HandleAwardTapPointsFailed");
	}

	public void HandleAwardTapPointsSucceeded()
	{
		Debug.Log("C#: HandleAwardTapPointsSucceeded");
		this.tapPointsLabel = string.Concat("Total TapPoints: ", TapjoyPlugin.QueryTapPoints());
	}

	public void HandleGetDisplayAdFailed()
	{
		Debug.Log("C#: HandleGetDisplayAdFailed");
	}

	public void HandleGetDisplayAdSucceeded()
	{
		Debug.Log("C#: HandleGetDisplayAdSucceeded");
		if (!this.viewIsShowing)
		{
			TapjoyPlugin.ShowDisplayAd();
		}
	}

	public void HandleGetFullScreenAdFailed()
	{
		Debug.Log("C#: HandleGetFullScreenAdFailed");
	}

	public void HandleGetFullScreenAdSucceeded()
	{
		Debug.Log("C#: HandleGetFullScreenAdSucceeded");
		TapjoyPlugin.ShowFullScreenAd();
	}

	public void HandleGetTapPointsFailed()
	{
		Debug.Log("C#: HandleGetTapPointsFailed");
	}

	private void HandleGetTapPointsSucceeded(int points)
	{
		Debug.Log(string.Concat("C#: HandleGetTapPointsSucceeded: ", points));
		this.tapPointsLabel = string.Concat("Total TapPoints: ", TapjoyPlugin.QueryTapPoints());
	}

	public void HandleShowOffersFailed()
	{
		Debug.Log("C#: HandleShowOffersFailed");
	}

	public void HandleSpendTapPointsFailed()
	{
		Debug.Log("C#: HandleSpendTapPointsFailed");
	}

	public void HandleSpendTapPointsSucceeded(int points)
	{
		Debug.Log(string.Concat("C#: HandleSpendTapPointsSucceeded: ", points));
		this.tapPointsLabel = string.Concat("Total TapPoints: ", TapjoyPlugin.QueryTapPoints());
	}

	public void HandleTapjoyConnectFailed()
	{
		Debug.Log("C#: HandleTapjoyConnectFailed");
	}

	public void HandleTapjoyConnectSuccess()
	{
		Debug.Log("C#: HandleTapjoyConnectSuccess");
	}

	public void HandleTapPointsEarned(int points)
	{
		Debug.Log(string.Concat("C#: CurrencyEarned: ", points));
		this.tapPointsLabel = string.Concat("Currency Earned: ", points);
		TapjoyPlugin.ShowDefaultEarnedCurrencyAlert();
	}

	public void HandleVideoAdCompleted()
	{
		Debug.Log("C#: HandleVideoAdCompleted");
	}

	public void HandleVideoAdFailed()
	{
		Debug.Log("C#: HandleVideoAdFailed");
	}

	public void HandleVideoAdStarted()
	{
		Debug.Log("C#: HandleVideoAdStarted");
	}

	public void HandleViewClosed(TapjoyViewType viewType)
	{
		Debug.Log("C#: HandleViewClosed");
		this.viewIsShowing = false;
	}

	public void HandleViewOpened(TapjoyViewType viewType)
	{
		Debug.Log("C#: HandleViewOpened");
		this.viewIsShowing = true;
	}

	private void OnDisable()
	{
		Debug.Log("C#: Disabling and removing Tapjoy Events");
		TapjoyPlugin.connectCallSucceeded -= new Action(this, TapjoySample.HandleTapjoyConnectSuccess);
		TapjoyPlugin.connectCallFailed -= new Action(this, TapjoySample.HandleTapjoyConnectFailed);
		TapjoyPlugin.getTapPointsSucceeded -= new Action<int>(this, TapjoySample.HandleGetTapPointsSucceeded);
		TapjoyPlugin.getTapPointsFailed -= new Action(this, TapjoySample.HandleGetTapPointsFailed);
		TapjoyPlugin.spendTapPointsSucceeded -= new Action<int>(this, TapjoySample.HandleSpendTapPointsSucceeded);
		TapjoyPlugin.spendTapPointsFailed -= new Action(this, TapjoySample.HandleSpendTapPointsFailed);
		TapjoyPlugin.awardTapPointsSucceeded -= new Action(this, TapjoySample.HandleAwardTapPointsSucceeded);
		TapjoyPlugin.awardTapPointsFailed -= new Action(this, TapjoySample.HandleAwardTapPointsFailed);
		TapjoyPlugin.tapPointsEarned -= new Action<int>(this, TapjoySample.HandleTapPointsEarned);
		TapjoyPlugin.getFullScreenAdSucceeded -= new Action(this, TapjoySample.HandleGetFullScreenAdSucceeded);
		TapjoyPlugin.getFullScreenAdFailed -= new Action(this, TapjoySample.HandleGetFullScreenAdFailed);
		TapjoyPlugin.getDisplayAdSucceeded -= new Action(this, TapjoySample.HandleGetDisplayAdSucceeded);
		TapjoyPlugin.getDisplayAdFailed -= new Action(this, TapjoySample.HandleGetDisplayAdFailed);
		TapjoyPlugin.videoAdStarted -= new Action(this, TapjoySample.HandleVideoAdStarted);
		TapjoyPlugin.videoAdFailed -= new Action(this, TapjoySample.HandleVideoAdFailed);
		TapjoyPlugin.videoAdCompleted -= new Action(this, TapjoySample.HandleVideoAdCompleted);
		TapjoyPlugin.viewOpened -= new Action<TapjoyViewType>(this, TapjoySample.HandleViewOpened);
		TapjoyPlugin.viewClosed -= new Action<TapjoyViewType>(this, TapjoySample.HandleViewClosed);
		TapjoyPlugin.showOffersFailed -= new Action(this, TapjoySample.HandleShowOffersFailed);
	}

	private void OnGUI()
	{
		if (this.viewIsShowing)
		{
			return;
		}
		GUIStyle gUIStyle = new GUIStyle();
		gUIStyle.set_alignment(4);
		gUIStyle.get_normal().set_textColor(Color.get_white());
		gUIStyle.set_wordWrap(true);
		float _width = (float)(Screen.get_width() / 2);
		float single = 60f;
		float single1 = 300f;
		float single2 = 50f;
		float single3 = 20f;
		float single4 = 100f;
		if (Input.GetKeyDown(27))
		{
			Application.Quit();
		}
		GUI.Label(new Rect(_width - 200f, single4, 400f, 25f), "Tapjoy Connect Sample App", gUIStyle);
		single4 = single4 + (single3 + 10f);
		if (GUI.Button(new Rect(_width - single1 / 2f, single4, single1, single2), "Show Offers"))
		{
			TapjoyPlugin.ShowOffers();
		}
		single4 = single4 + single;
		if (GUI.Button(new Rect(_width - single1 / 2f, single4, single1, single2), "Get Display Ad"))
		{
			TapjoyPlugin.GetDisplayAd();
		}
		single4 = single4 + single;
		if (GUI.Button(new Rect(_width - single1 / 2f, single4, single1, single2), "Hide Display Ad"))
		{
			TapjoyPlugin.HideDisplayAd();
		}
		single4 = single4 + single;
		if (GUI.Button(new Rect(_width - single1 / 2f, single4, single1, single2), "Toggle Display Ad Auto-Refresh"))
		{
			this.autoRefresh = !this.autoRefresh;
			TapjoyPlugin.EnableDisplayAdAutoRefresh(this.autoRefresh);
		}
		single4 = single4 + single;
		if (GUI.Button(new Rect(_width - single1 / 2f, single4, single1, single2), "Show Direct Play Video Ad"))
		{
			TapjoyPlugin.GetFullScreenAd();
		}
		single4 = single4 + single;
		if (GUI.Button(new Rect(_width - single1 / 2f, single4, single1, single2), "Get Tap Points"))
		{
			TapjoyPlugin.GetTapPoints();
			this.ResetTapPointsLabel();
		}
		single4 = single4 + single;
		if (GUI.Button(new Rect(_width - single1 / 2f, single4, single1, single2), "Spend Tap Points"))
		{
			TapjoyPlugin.SpendTapPoints(10);
			this.ResetTapPointsLabel();
		}
		single4 = single4 + single;
		if (GUI.Button(new Rect(_width - single1 / 2f, single4, single1, single2), "Award Tap Points"))
		{
			TapjoyPlugin.AwardTapPoints(10);
			this.ResetTapPointsLabel();
		}
		single4 = single4 + single;
		if (GUI.Button(new Rect(_width - single1 / 2f, single4, single1, single2), "Send Event"))
		{
			this.sampleEvent = new TapjoyEvent("test_unit", this);
			if (this.sampleEvent != null)
			{
				this.sampleEvent.EnableAutoPresent(false);
				this.sampleEvent.Send();
			}
		}
		single4 = single4 + single3;
		GUI.Label(new Rect(_width - 200f, single4, 400f, 150f), this.tapPointsLabel, gUIStyle);
	}

	public void ResetTapPointsLabel()
	{
		this.tapPointsLabel = "Updating Tap Points...";
	}

	public void SendEventFailed(TapjoyEvent tapjoyEvent, string error)
	{
		Debug.Log("C#: SendEventFailed");
	}

	public void SendEventSucceeded(TapjoyEvent tapjoyEvent, bool contentIsAvailable)
	{
		if (contentIsAvailable)
		{
			tapjoyEvent.Show();
		}
		Debug.Log("C#: SendEventSucceeded");
	}

	private void Start()
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		if (Application.get_platform() == 11)
		{
			dictionary.Add(TapjoySample.ENABLE_LOGGING_ANDROID, true);
			TapjoyPlugin.RequestTapjoyConnect("bba49f11-b87f-4c0f-9632-21aa810dd6f1", "yiQIURFEeKm0zbOggubu", dictionary);
		}
		else if (Application.get_platform() == 8)
		{
			dictionary.Add(TapjoySample.ENABLE_LOGGING_IOS, true);
			dictionary.Add("TJC_OPTION_COLLECT_MAC_ADDRESS", "1");
			TapjoyPlugin.RequestTapjoyConnect("13b0ae6a-8516-4405-9dcf-fe4e526486b2", "XHdOwPa8de7p4aseeYP0", dictionary);
		}
		TapjoyPlugin.GetTapPoints();
		TapjoyPlugin.GetDisplayAd();
	}
}