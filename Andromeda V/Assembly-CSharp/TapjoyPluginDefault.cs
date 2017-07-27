using System;
using System.Collections.Generic;

public class TapjoyPluginDefault
{
	public TapjoyPluginDefault()
	{
	}

	public static void ActionComplete(string actionID)
	{
	}

	public static void AwardTapPoints(int points)
	{
	}

	public static void CreateEvent(string eventGuid, string eventName, string eventParameter)
	{
	}

	public static void EnableDisplayAdAutoRefresh(bool enable)
	{
	}

	public static void EnableEventAutoPresent(string eventGuid, bool autoPresent)
	{
	}

	public static void EnableLogging(bool enable)
	{
	}

	public static void EventRequestCancelled(string guid)
	{
	}

	public static void EventRequestCompleted(string guid)
	{
	}

	public static void GetDisplayAd()
	{
	}

	public static void GetDisplayAdWithCurrencyID(string currencyID)
	{
	}

	public static void GetFullScreenAd()
	{
	}

	public static void GetFullScreenAdWithCurrencyID(string currencyID)
	{
	}

	public static void GetTapPoints()
	{
	}

	public static void HideDisplayAd()
	{
	}

	public static void MoveDisplayAd(int x, int y)
	{
	}

	public static int QueryTapPoints()
	{
		return 0;
	}

	public static void RequestTapjoyConnect(string appID, string secretKey)
	{
	}

	public static void RequestTapjoyConnect(string appID, string secretKey, Dictionary<string, object> flags)
	{
	}

	public static void SendEvent(string eventGuid)
	{
	}

	public static void SendIAPEvent(string name, float price, int quantity, string currencyCode)
	{
	}

	public static void SendShutDownEvent()
	{
	}

	public static void SetCallbackHandler(string handlerName)
	{
	}

	public static void SetCurrencyMultiplier(float multiplier)
	{
	}

	public static void SetDisplayAdSize(string size)
	{
	}

	public static void SetTransitionEffect(int transition)
	{
	}

	public static void SetUserID(string userID)
	{
	}

	public static void ShowDefaultEarnedCurrencyAlert()
	{
	}

	public static void ShowDisplayAd()
	{
	}

	public static void ShowEvent(string eventGuid)
	{
	}

	public static void ShowFullScreenAd()
	{
	}

	public static void ShowOffers()
	{
	}

	public static void ShowOffersWithCurrencyID(string currencyID, bool selector)
	{
	}

	public static void SpendTapPoints(int points)
	{
	}
}