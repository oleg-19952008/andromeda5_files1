using Facebook;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

public sealed class FB : ScriptableObject
{
	public static InitDelegate OnInitComplete;

	public static HideUnityDelegate OnHideUnity;

	private static IFacebook facebook;

	private static string authResponse;

	private static bool isInitCalled;

	private static string appId;

	private static bool cookie;

	private static bool logging;

	private static bool status;

	private static bool xfbml;

	private static bool frictionlessRequests;

	public static string AccessToken
	{
		get
		{
			return (FB.facebook == null ? string.Empty : FB.facebook.AccessToken);
		}
	}

	public static DateTime AccessTokenExpiresAt
	{
		get
		{
			return (FB.facebook == null ? DateTime.MinValue : FB.facebook.AccessTokenExpiresAt);
		}
	}

	public static string AppId
	{
		get
		{
			return FB.appId;
		}
	}

	private static IFacebook FacebookImpl
	{
		get
		{
			if (FB.facebook == null)
			{
				throw new NullReferenceException("Facebook object is not yet loaded.  Did you call FB.Init()?");
			}
			return FB.facebook;
		}
	}

	public static bool IsLoggedIn
	{
		get
		{
			return (FB.facebook == null ? false : FB.facebook.IsLoggedIn);
		}
	}

	public static string UserId
	{
		get
		{
			return (FB.facebook == null ? string.Empty : FB.facebook.UserId);
		}
	}

	static FB()
	{
	}

	public FB()
	{
	}

	public static void API(string query, HttpMethod method, FacebookDelegate callback = null, Dictionary<string, string> formData = null)
	{
		FB.FacebookImpl.API(query, method, formData, callback);
	}

	public static void API(string query, HttpMethod method, FacebookDelegate callback, WWWForm formData)
	{
		FB.FacebookImpl.API(query, method, formData, callback);
	}

	public static void AppRequest(string message, string[] to = null, string filters = "", string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "", FacebookDelegate callback = null)
	{
		FB.FacebookImpl.AppRequest(message, to, filters, excludeIds, maxRecipients, data, title, callback);
	}

	public static void Feed(string toId = "", string link = "", string linkName = "", string linkCaption = "", string linkDescription = "", string picture = "", string mediaSource = "", string actionName = "", string actionLink = "", string reference = "", Dictionary<string, string[]> properties = null, FacebookDelegate callback = null)
	{
		FB.FacebookImpl.FeedRequest(toId, link, linkName, linkCaption, linkDescription, picture, mediaSource, actionName, actionLink, reference, properties, callback);
	}

	public static void GetDeepLink(FacebookDelegate callback)
	{
		FB.FacebookImpl.GetDeepLink(callback);
	}

	public static void Init(InitDelegate onInitComplete, HideUnityDelegate onHideUnity = null, string authResponse = null)
	{
		FB.Init(onInitComplete, FBSettings.AppId, FBSettings.Cookie, FBSettings.Logging, FBSettings.Status, FBSettings.Xfbml, FBSettings.FrictionlessRequests, onHideUnity, authResponse);
	}

	public static void Init(InitDelegate onInitComplete, string appId, bool cookie = true, bool logging = true, bool status = true, bool xfbml = false, bool frictionlessRequests = true, HideUnityDelegate onHideUnity = null, string authResponse = null)
	{
		FB.appId = appId;
		FB.cookie = cookie;
		FB.logging = logging;
		FB.status = status;
		FB.xfbml = xfbml;
		FB.frictionlessRequests = frictionlessRequests;
		FB.authResponse = authResponse;
		FB.OnInitComplete = onInitComplete;
		FB.OnHideUnity = onHideUnity;
		if (!FB.isInitCalled)
		{
			FBBuildVersionAttribute versionAttributeOfType = FBBuildVersionAttribute.GetVersionAttributeOfType(typeof(IFacebook));
			if (versionAttributeOfType != null)
			{
				FbDebug.Info(string.Format("Using SDK {0}, Build {1}", versionAttributeOfType.SdkVersion, versionAttributeOfType.BuildVersion));
			}
			else
			{
				FbDebug.Warn("Cannot find Facebook SDK Version");
			}
			throw new NotImplementedException("Facebook API does not yet support this platform");
		}
		FbDebug.Warn("FB.Init() has already been called.  You only need to call this once and only once.");
		if (FB.FacebookImpl != null)
		{
			FB.OnDllLoaded();
		}
	}

	public static void Login(string scope = "", FacebookDelegate callback = null)
	{
		FB.FacebookImpl.Login(scope, callback);
	}

	public static void Logout()
	{
		FB.FacebookImpl.Logout();
	}

	private static void OnDllLoaded()
	{
		FBBuildVersionAttribute versionAttributeOfType = FBBuildVersionAttribute.GetVersionAttributeOfType(FB.FacebookImpl.GetType());
		if (versionAttributeOfType != null)
		{
			FbDebug.Log(string.Format("Finished loading Facebook dll. Version {0} Build {1}", versionAttributeOfType.SdkVersion, versionAttributeOfType.BuildVersion));
		}
		else
		{
			FbDebug.Warn("Finished loading Facebook dll, but could not find version info");
		}
		FB.FacebookImpl.Init(FB.OnInitComplete, FB.appId, FB.cookie, FB.logging, FB.status, FB.xfbml, FBSettings.ChannelUrl, FB.authResponse, FB.frictionlessRequests, FB.OnHideUnity);
	}

	public static void PublishInstall(FacebookDelegate callback = null)
	{
		FB.FacebookImpl.PublishInstall(FB.AppId, callback);
	}

	public sealed class Android
	{
		public static string KeyHash
		{
			get
			{
				AndroidFacebook androidFacebook = FB.facebook as AndroidFacebook;
				return (androidFacebook == null ? string.Empty : androidFacebook.KeyHash);
			}
		}

		public Android()
		{
		}
	}

	public sealed class AppEvents
	{
		public static bool LimitEventUsage
		{
			get
			{
				return (FB.facebook == null ? false : FB.facebook.LimitEventUsage);
			}
			set
			{
				FB.facebook.LimitEventUsage = value;
			}
		}

		public AppEvents()
		{
		}

		public static void LogEvent(string logEvent, float? valueToSum = null, Dictionary<string, object> parameters = null)
		{
			FB.FacebookImpl.AppEventsLogEvent(logEvent, valueToSum, parameters);
		}

		public static void LogPurchase(float logPurchase, string currency = "USD", Dictionary<string, object> parameters = null)
		{
			FB.FacebookImpl.AppEventsLogPurchase(logPurchase, currency, parameters);
		}
	}

	public sealed class Canvas
	{
		public Canvas()
		{
		}

		public static void Pay(string product, string action = "purchaseitem", int quantity = 1, int? quantityMin = null, int? quantityMax = null, string requestId = null, string pricepointId = null, string testCurrency = null, FacebookDelegate callback = null)
		{
			FB.FacebookImpl.Pay(product, action, quantity, quantityMin, quantityMax, requestId, pricepointId, testCurrency, callback);
		}

		public static void SetAspectRatio(int width, int height, params FBScreen.Layout[] layoutParams)
		{
			FBScreen.SetAspectRatio(width, height, layoutParams);
		}

		public static void SetResolution(int width, int height, bool fullscreen, int preferredRefreshRate = 0, params FBScreen.Layout[] layoutParams)
		{
			FBScreen.SetResolution(width, height, fullscreen, preferredRefreshRate, layoutParams);
		}
	}

	public abstract class CompiledFacebookLoader : MonoBehaviour
	{
		protected abstract IFacebook fb
		{
			get;
		}

		protected CompiledFacebookLoader()
		{
		}

		private void Start()
		{
			FB.facebook = this.fb;
			FB.OnDllLoaded();
			Object.Destroy(this);
		}
	}

	public abstract class RemoteFacebookLoader : MonoBehaviour
	{
		private const string facebookNamespace = "Facebook.";

		private const int maxRetryLoadCount = 3;

		private static int retryLoadCount;

		protected abstract string className
		{
			get;
		}

		static RemoteFacebookLoader()
		{
		}

		protected RemoteFacebookLoader()
		{
		}

		[DebuggerHidden]
		public static IEnumerator LoadFacebookClass(string className, FB.RemoteFacebookLoader.LoadedDllCallback callback)
		{
			FB.RemoteFacebookLoader.<LoadFacebookClass>c__Iterator1 variable = null;
			return variable;
		}

		private void OnDllLoaded(IFacebook fb)
		{
			FB.facebook = fb;
			FB.OnDllLoaded();
		}

		[DebuggerHidden]
		private IEnumerator Start()
		{
			FB.RemoteFacebookLoader.<Start>c__Iterator2 variable = null;
			return variable;
		}

		public delegate void LoadedDllCallback(IFacebook fb);
	}
}