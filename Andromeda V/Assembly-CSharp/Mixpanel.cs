using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

public static class Mixpanel
{
	private const string API_URL_FORMAT = "http://api.mixpanel.com/track/?data={0}";

	public static string Token;

	public static string DistinctID;

	public static bool EnableLogging;

	public static Dictionary<string, object> SuperProperties;

	private static MonoBehaviour _coroutineObject;

	static Mixpanel()
	{
		Mixpanel.SuperProperties = new Dictionary<string, object>();
	}

	private static string EncodeTo64(string toEncode)
	{
		return Convert.ToBase64String(Encoding.get_ASCII().GetBytes(toEncode));
	}

	public static void SendEvent(string eventName)
	{
		Mixpanel.SendEvent(eventName, null);
	}

	public static void SendEvent(string eventName, IDictionary<string, object> properties)
	{
		if (string.IsNullOrEmpty(Mixpanel.Token))
		{
			Debug.LogError("Attempted to send an event without setting the Mixpanel.Token variable.");
			return;
		}
		if (string.IsNullOrEmpty(Mixpanel.DistinctID))
		{
			if (!PlayerPrefs.HasKey("mixpanel_distinct_id"))
			{
				PlayerPrefs.SetString("mixpanel_distinct_id", Guid.NewGuid().ToString());
			}
			Mixpanel.DistinctID = PlayerPrefs.GetString("mixpanel_distinct_id");
		}
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("distinct_id", Mixpanel.DistinctID);
		dictionary.Add("token", Mixpanel.Token);
		foreach (KeyValuePair<string, object> superProperty in Mixpanel.SuperProperties)
		{
			if (!(superProperty.get_Value() is float))
			{
				dictionary.Add(superProperty.get_Key(), superProperty.get_Value());
			}
			else
			{
				double value = (double)((float)superProperty.get_Value());
				dictionary.Add(superProperty.get_Key(), value);
			}
		}
		if (properties != null)
		{
			IEnumerator<KeyValuePair<string, object>> enumerator = properties.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, object> current = enumerator.get_Current();
					if (!(current.get_Value() is float))
					{
						dictionary.Add(current.get_Key(), current.get_Value());
					}
					else
					{
						double num = (double)((float)current.get_Value());
						dictionary.Add(current.get_Key(), num);
					}
				}
			}
			finally
			{
				if (enumerator == null)
				{
				}
				enumerator.Dispose();
			}
		}
		Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
		dictionary1.Add("event", eventName);
		dictionary1.Add("properties", dictionary);
		string json = JsonMapper.ToJson(dictionary1);
		if (Mixpanel.EnableLogging)
		{
			Debug.Log(string.Concat("Sending mixpanel event: ", json));
		}
		string str = string.Format("http://api.mixpanel.com/track/?data={0}", Mixpanel.EncodeTo64(json));
		Mixpanel.StartCoroutine(Mixpanel.SendEventCoroutine(str));
	}

	[DebuggerHidden]
	private static IEnumerator SendEventCoroutine(string url)
	{
		Mixpanel.<SendEventCoroutine>c__Iterator5 variable = null;
		return variable;
	}

	private static void StartCoroutine(IEnumerator coroutine)
	{
		if (Mixpanel._coroutineObject == null)
		{
			GameObject gameObject = new GameObject("Mixpanel Coroutines");
			Object.DontDestroyOnLoad(gameObject);
			Mixpanel._coroutineObject = gameObject.AddComponent<MonoBehaviour>();
		}
		Mixpanel._coroutineObject.StartCoroutine(coroutine);
	}
}