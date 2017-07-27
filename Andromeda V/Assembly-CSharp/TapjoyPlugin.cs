using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TapjoyPlugin : MonoBehaviour
{
	public const string MacAddressOptionOn = "0";

	public const string MacAddressOptionOffWithVersionCheck = "1";

	public const string MacAddressOptionOff = "2";

	private static Dictionary<string, TapjoyEvent> eventDictionary;

	static TapjoyPlugin()
	{
		TapjoyPlugin.eventDictionary = new Dictionary<string, TapjoyEvent>();
	}

	public TapjoyPlugin()
	{
	}

	public static void ActionComplete(string actionID)
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.ActionComplete(actionID);
	}

	private void Awake()
	{
		base.get_gameObject().set_name(base.GetType().ToString());
		TapjoyPlugin.SetCallbackHandler(base.get_gameObject().get_name());
		Debug.Log(string.Concat("C#: UnitySendMessage directs to ", base.get_gameObject().get_name()));
		Object.DontDestroyOnLoad(this);
	}

	public static void AwardTapPoints(int points)
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.AwardTapPoints(points);
	}

	public void ContentDidAppear(string guid)
	{
		if (TapjoyPlugin.eventDictionary != null && TapjoyPlugin.eventDictionary.ContainsKey(guid))
		{
			TapjoyPlugin.eventDictionary.get_Item(guid).TriggerContentDidAppear();
		}
	}

	public void ContentDidDisappear(string guid)
	{
		if (TapjoyPlugin.eventDictionary != null && TapjoyPlugin.eventDictionary.ContainsKey(guid))
		{
			TapjoyPlugin.eventDictionary.get_Item(guid).TriggerContentDidDisappear();
		}
	}

	public static string CreateEvent(TapjoyEvent eventRef, string eventName, string eventParameter)
	{
		if (Application.get_isEditor())
		{
			return null;
		}
		string str = Guid.NewGuid().ToString();
		while (TapjoyPlugin.eventDictionary.ContainsKey(str))
		{
			str = Guid.NewGuid().ToString();
		}
		TapjoyPlugin.eventDictionary.Add(str, eventRef);
		TapjoyPluginDefault.CreateEvent(str, eventName, eventParameter);
		return str;
	}

	public void CurrencyEarned(string message)
	{
		if (TapjoyPlugin.tapPointsEarned != null)
		{
			TapjoyPlugin.tapPointsEarned.Invoke(int.Parse(message));
		}
	}

	public void DidRequestAction(string message)
	{
		int num = 0;
		int num1 = 0;
		string[] strArray = message.Split(new char[] { ',' });
		string str = strArray[0];
		int.TryParse(strArray[1], ref num);
		string str1 = strArray[2];
		int.TryParse(strArray[3], ref num1);
		if (TapjoyPlugin.eventDictionary != null && TapjoyPlugin.eventDictionary.ContainsKey(str))
		{
			TapjoyPlugin.eventDictionary.get_Item(str).TriggerDidRequestAction(num, str1, num1);
		}
	}

	public void DisplayAdError(string message)
	{
		if (TapjoyPlugin.getDisplayAdFailed != null)
		{
			TapjoyPlugin.getDisplayAdFailed.Invoke();
		}
	}

	public void DisplayAdLoaded(string message)
	{
		if (TapjoyPlugin.getDisplayAdSucceeded != null)
		{
			TapjoyPlugin.getDisplayAdSucceeded.Invoke();
		}
	}

	public static void EnableDisplayAdAutoRefresh(bool enable)
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.EnableDisplayAdAutoRefresh(enable);
	}

	public static void EnableEventAutoPresent(string guid, bool autoPresent)
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.EnableEventAutoPresent(guid, autoPresent);
	}

	public static void EnableLogging(bool enable)
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.EnableLogging(enable);
	}

	public static void EventRequestCancelled(string guid)
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.EventRequestCancelled(guid);
	}

	public static void EventRequestCompleted(string guid)
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.EventRequestCompleted(guid);
	}

	public void FullScreenAdError(string message)
	{
		if (TapjoyPlugin.getFullScreenAdFailed != null)
		{
			TapjoyPlugin.getFullScreenAdFailed.Invoke();
		}
	}

	public void FullScreenAdLoaded(string message)
	{
		if (TapjoyPlugin.getFullScreenAdSucceeded != null)
		{
			TapjoyPlugin.getFullScreenAdSucceeded.Invoke();
		}
	}

	public static void GetDisplayAd()
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.GetDisplayAd();
	}

	public static void GetDisplayAdWithCurrencyID(string currencyID)
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.GetDisplayAdWithCurrencyID(currencyID);
	}

	[Obsolete("GetFullScreenAd is deprecated since 10.0.0")]
	public static void GetFullScreenAd()
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.GetFullScreenAd();
	}

	public static void GetFullScreenAdWithCurrencyID(string currencyID)
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.GetFullScreenAdWithCurrencyID(currencyID);
	}

	public static void GetTapPoints()
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.GetTapPoints();
	}

	public static void HideDisplayAd()
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.HideDisplayAd();
	}

	public static void MoveDisplayAd(int x, int y)
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.MoveDisplayAd(x, y);
	}

	public static int QueryTapPoints()
	{
		if (Application.get_isEditor())
		{
			return 0;
		}
		return TapjoyPluginDefault.QueryTapPoints();
	}

	public static void RequestTapjoyConnect(string appID, string secretKey)
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.RequestTapjoyConnect(appID, secretKey);
	}

	public static void RequestTapjoyConnect(string appID, string secretKey, Dictionary<string, string> flags)
	{
		if (Application.get_isEditor())
		{
			return;
		}
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		foreach (KeyValuePair<string, string> flag in flags)
		{
			dictionary.Add(flag.get_Key(), flag.get_Value());
		}
		TapjoyPluginDefault.RequestTapjoyConnect(appID, secretKey, dictionary);
	}

	public static void RequestTapjoyConnect(string appID, string secretKey, Dictionary<string, object> flags)
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.RequestTapjoyConnect(appID, secretKey, flags);
	}

	public static void SendEvent(string guid)
	{
		TapjoyPluginDefault.SendEvent(guid);
	}

	public void SendEventComplete(string guid)
	{
		if (TapjoyPlugin.eventDictionary != null && TapjoyPlugin.eventDictionary.ContainsKey(guid))
		{
			TapjoyPlugin.eventDictionary.get_Item(guid).TriggerSendEventSucceeded(false);
		}
	}

	public void SendEventCompleteWithContent(string guid)
	{
		if (TapjoyPlugin.eventDictionary != null && TapjoyPlugin.eventDictionary.ContainsKey(guid))
		{
			TapjoyPlugin.eventDictionary.get_Item(guid).TriggerSendEventSucceeded(true);
		}
	}

	public void SendEventFail(string guid)
	{
		if (TapjoyPlugin.eventDictionary != null && TapjoyPlugin.eventDictionary.ContainsKey(guid))
		{
			TapjoyPlugin.eventDictionary.get_Item(guid).TriggerSendEventFailed(null);
		}
	}

	public static void SendIAPEvent(string name, float price, int quantity, string currencyCode)
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.SendIAPEvent(name, price, quantity, currencyCode);
	}

	public static void SendShutDownEvent()
	{
		TapjoyPluginDefault.SendShutDownEvent();
	}

	public static void SetCallbackHandler(string handlerName)
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.SetCallbackHandler(handlerName);
	}

	public static void SetCurrencyMultiplier(float multiplier)
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.SetCurrencyMultiplier(multiplier);
	}

	[Obsolete("SetDisplayAdContentSize is deprecated. Please use SetDisplayAdSize.")]
	public static void SetDisplayAdContentSize(int size)
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPlugin.SetDisplayAdSize((TapjoyDisplayAdSize)size);
	}

	public static void SetDisplayAdSize(TapjoyDisplayAdSize size)
	{
		if (Application.get_isEditor())
		{
			return;
		}
		string str = "320x50";
		if (size == TapjoyDisplayAdSize.SIZE_640X100)
		{
			str = "640x100";
		}
		if (size == TapjoyDisplayAdSize.SIZE_768X90)
		{
			str = "768x90";
		}
		TapjoyPluginDefault.SetDisplayAdSize(str);
	}

	[Obsolete("SetTransitionEffect is deprecated since 10.0.0")]
	public static void SetTransitionEffect(int transition)
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.SetTransitionEffect(transition);
	}

	public static void SetUserID(string userID)
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.SetUserID(userID);
	}

	[Obsolete("SetVideoCacheCount is deprecated, video is now controlled via your Tapjoy Dashboard.")]
	public static void SetVideoCacheCount(int cacheCount)
	{
	}

	public static void ShowDefaultEarnedCurrencyAlert()
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.ShowDefaultEarnedCurrencyAlert();
	}

	public static void ShowDisplayAd()
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.ShowDisplayAd();
	}

	public static void ShowEvent(string guid)
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.ShowEvent(guid);
	}

	[Obsolete("ShowFullScreenAd is deprecated since 10.0.0. Tapjoy ad units now use TJEvent")]
	public static void ShowFullScreenAd()
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.ShowFullScreenAd();
	}

	public static void ShowOffers()
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.ShowOffers();
	}

	public void ShowOffersError(string message)
	{
		if (TapjoyPlugin.showOffersFailed != null)
		{
			TapjoyPlugin.showOffersFailed.Invoke();
		}
	}

	public static void ShowOffersWithCurrencyID(string currencyID, bool selector)
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.ShowOffersWithCurrencyID(currencyID, selector);
	}

	public static void SpendTapPoints(int points)
	{
		if (Application.get_isEditor())
		{
			return;
		}
		TapjoyPluginDefault.SpendTapPoints(points);
	}

	public void TapjoyConnectFail(string message)
	{
		if (TapjoyPlugin.connectCallFailed != null)
		{
			TapjoyPlugin.connectCallFailed.Invoke();
		}
	}

	public void TapjoyConnectSuccess(string message)
	{
		if (TapjoyPlugin.connectCallSucceeded != null)
		{
			TapjoyPlugin.connectCallSucceeded.Invoke();
		}
	}

	public void TapPointsAwarded(string message)
	{
		if (TapjoyPlugin.awardTapPointsSucceeded != null)
		{
			TapjoyPlugin.awardTapPointsSucceeded.Invoke();
		}
	}

	public void TapPointsAwardError(string message)
	{
		if (TapjoyPlugin.awardTapPointsFailed != null)
		{
			TapjoyPlugin.awardTapPointsFailed.Invoke();
		}
	}

	public void TapPointsLoaded(string message)
	{
		if (TapjoyPlugin.getTapPointsSucceeded != null)
		{
			TapjoyPlugin.getTapPointsSucceeded.Invoke(int.Parse(message));
		}
	}

	public void TapPointsLoadedError(string message)
	{
		if (TapjoyPlugin.getTapPointsFailed != null)
		{
			TapjoyPlugin.getTapPointsFailed.Invoke();
		}
	}

	public void TapPointsSpendError(string message)
	{
		if (TapjoyPlugin.spendTapPointsFailed != null)
		{
			TapjoyPlugin.spendTapPointsFailed.Invoke();
		}
	}

	public void TapPointsSpent(string message)
	{
		if (TapjoyPlugin.spendTapPointsSucceeded != null)
		{
			TapjoyPlugin.spendTapPointsSucceeded.Invoke(int.Parse(message));
		}
	}

	public void VideoAdComplete(string message)
	{
		if (TapjoyPlugin.videoAdCompleted != null)
		{
			TapjoyPlugin.videoAdCompleted.Invoke();
		}
	}

	public void VideoAdError(string message)
	{
		if (TapjoyPlugin.videoAdFailed != null)
		{
			TapjoyPlugin.videoAdFailed.Invoke();
		}
	}

	public void VideoAdStart(string message)
	{
		if (TapjoyPlugin.videoAdStarted != null)
		{
			TapjoyPlugin.videoAdStarted.Invoke();
		}
	}

	public void ViewClosed(string message)
	{
		if (TapjoyPlugin.viewClosed != null)
		{
			int num = int.Parse(message);
			TapjoyPlugin.viewClosed.Invoke((TapjoyViewType)num);
		}
	}

	public void ViewOpened(string message)
	{
		if (TapjoyPlugin.viewOpened != null)
		{
			int num = int.Parse(message);
			TapjoyPlugin.viewOpened.Invoke((TapjoyViewType)num);
		}
	}

	public static event Action awardTapPointsFailed;

	public static event Action awardTapPointsSucceeded;

	public static event Action connectCallFailed;

	public static event Action connectCallSucceeded;

	public static event Action getDisplayAdFailed;

	public static event Action getDisplayAdSucceeded;

	public static event Action getFullScreenAdFailed;

	public static event Action getFullScreenAdSucceeded;

	public static event Action getTapPointsFailed;

	public static event Action<int> getTapPointsSucceeded;

	public static event Action showOffersFailed;

	public static event Action spendTapPointsFailed;

	public static event Action<int> spendTapPointsSucceeded;

	public static event Action<int> tapPointsEarned;

	public static event Action videoAdCompleted;

	public static event Action videoAdFailed;

	public static event Action videoAdStarted;

	public static event Action<TapjoyViewType> viewClosed;

	public static event Action<TapjoyViewType> viewOpened;
}