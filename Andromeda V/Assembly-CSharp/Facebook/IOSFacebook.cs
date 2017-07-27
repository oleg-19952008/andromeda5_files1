using Facebook.MiniJSON;
using System;
using System.Collections.Generic;

namespace Facebook
{
	internal class IOSFacebook : AbstractFacebook, IFacebook
	{
		private const string CancelledResponse = "{\"cancelled\":true}";

		private int dialogMode = 1;

		private InitDelegate externalInitDelegate;

		private FacebookDelegate deepLinkDelegate;

		public override int DialogMode
		{
			get
			{
				return this.dialogMode;
			}
			set
			{
				this.dialogMode = value;
				this.iosSetShareDialogMode(this.dialogMode);
			}
		}

		public override bool LimitEventUsage
		{
			get
			{
				return this.limitEventUsage;
			}
			set
			{
				this.limitEventUsage = value;
				this.iosFBAppEventsSetLimitEventUsage(value);
			}
		}

		public IOSFacebook()
		{
		}

		public override void AppEventsLogEvent(string logEvent, float? valueToSum = null, Dictionary<string, object> parameters = null)
		{
			IOSFacebook.NativeDict nativeDict = this.MarshallDict(parameters);
			if (!valueToSum.get_HasValue())
			{
				this.iosFBAppEventsLogEvent(logEvent, 0, nativeDict.numEntries, nativeDict.keys, nativeDict.vals);
			}
			else
			{
				this.iosFBAppEventsLogEvent(logEvent, (double)valueToSum.get_Value(), nativeDict.numEntries, nativeDict.keys, nativeDict.vals);
			}
		}

		public override void AppEventsLogPurchase(float logPurchase, string currency = "USD", Dictionary<string, object> parameters = null)
		{
			IOSFacebook.NativeDict nativeDict = this.MarshallDict(parameters);
			if (string.IsNullOrEmpty(currency))
			{
				currency = "USD";
			}
			this.iosFBAppEventsLogPurchase((double)logPurchase, currency, nativeDict.numEntries, nativeDict.keys, nativeDict.vals);
		}

		public override void AppRequest(string message, string[] to = null, string filters = "", string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "", FacebookDelegate callback = null)
		{
			this.iosAppRequest(Convert.ToInt32(base.AddFacebookDelegate(callback)), message, to, (to == null ? 0 : (int)to.Length), filters, excludeIds, (excludeIds == null ? 0 : (int)excludeIds.Length), maxRecipients.get_HasValue(), (!maxRecipients.get_HasValue() ? 0 : maxRecipients.get_Value()), data, title);
		}

		public override void FeedRequest(string toId = "", string link = "", string linkName = "", string linkCaption = "", string linkDescription = "", string picture = "", string mediaSource = "", string actionName = "", string actionLink = "", string reference = "", Dictionary<string, string[]> properties = null, FacebookDelegate callback = null)
		{
			this.iosFeedRequest(Convert.ToInt32(base.AddFacebookDelegate(callback)), toId, link, linkName, linkCaption, linkDescription, picture, mediaSource, actionName, actionLink, reference);
		}

		private DateTime FromTimestamp(int timestamp)
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			return dateTime.AddSeconds((double)timestamp);
		}

		public override void GetDeepLink(FacebookDelegate callback)
		{
			if (callback == null)
			{
				return;
			}
			this.deepLinkDelegate = callback;
			this.iosGetDeepLink();
		}

		public override void Init(InitDelegate onInitComplete, string appId, bool cookie = false, bool logging = true, bool status = true, bool xfbml = false, string channelUrl = "", string authResponse = null, bool frictionlessRequests = false, HideUnityDelegate hideUnityDelegate = null)
		{
			this.iosInit(cookie, logging, status, frictionlessRequests, FBSettings.IosURLSuffix);
			this.externalInitDelegate = onInitComplete;
		}

		private void iosAppRequest(int requestId, string message, string[] to = null, int toLength = 0, string filters = "", string[] excludeIds = null, int excludeIdsLength = 0, bool hasMaxRecipients = false, int maxRecipients = 0, string data = "", string title = "")
		{
		}

		private void iosFBAppEventsLogEvent(string logEvent, double valueToSum, int numParams, string[] paramKeys, string[] paramVals)
		{
		}

		private void iosFBAppEventsLogPurchase(double logPurchase, string currency, int numParams, string[] paramKeys, string[] paramVals)
		{
		}

		private void iosFBAppEventsSetLimitEventUsage(bool limitEventUsage)
		{
		}

		private void iosFBSettingsPublishInstall(int requestId, string appId)
		{
		}

		private void iosFeedRequest(int requestId, string toId, string link, string linkName, string linkCaption, string linkDescription, string picture, string mediaSource, string actionName, string actionLink, string reference)
		{
		}

		private void iosGetDeepLink()
		{
		}

		private void iosInit(bool cookie, bool logging, bool status, bool frictionlessRequests, string urlSuffix)
		{
		}

		private void iosLogin(string scope)
		{
		}

		private void iosLogout()
		{
		}

		private void iosSetShareDialogMode(int mode)
		{
		}

		public override void Login(string scope = "", FacebookDelegate callback = null)
		{
			base.AddAuthDelegate(callback);
			this.iosLogin(scope);
		}

		public override void Logout()
		{
			this.iosLogout();
			this.isLoggedIn = false;
		}

		private IOSFacebook.NativeDict MarshallDict(Dictionary<string, object> dict)
		{
			IOSFacebook.NativeDict nativeDict = new IOSFacebook.NativeDict();
			if (dict != null && dict.get_Count() > 0)
			{
				nativeDict.keys = new string[dict.get_Count()];
				nativeDict.vals = new string[dict.get_Count()];
				nativeDict.numEntries = 0;
				foreach (KeyValuePair<string, object> keyValuePair in dict)
				{
					nativeDict.keys[nativeDict.numEntries] = keyValuePair.get_Key();
					nativeDict.vals[nativeDict.numEntries] = keyValuePair.get_Value().ToString();
					IOSFacebook.NativeDict nativeDict1 = nativeDict;
					nativeDict1.numEntries = nativeDict1.numEntries + 1;
				}
			}
			return nativeDict;
		}

		private IOSFacebook.NativeDict MarshallDict(Dictionary<string, string> dict)
		{
			IOSFacebook.NativeDict nativeDict = new IOSFacebook.NativeDict();
			if (dict != null && dict.get_Count() > 0)
			{
				nativeDict.keys = new string[dict.get_Count()];
				nativeDict.vals = new string[dict.get_Count()];
				nativeDict.numEntries = 0;
				foreach (KeyValuePair<string, string> keyValuePair in dict)
				{
					nativeDict.keys[nativeDict.numEntries] = keyValuePair.get_Key();
					nativeDict.vals[nativeDict.numEntries] = keyValuePair.get_Value();
					IOSFacebook.NativeDict nativeDict1 = nativeDict;
					nativeDict1.numEntries = nativeDict1.numEntries + 1;
				}
			}
			return nativeDict;
		}

		public void OnAccessTokenRefresh(string message)
		{
			this.ParseLoginDict((Dictionary<string, object>)Json.Deserialize(message));
		}

		protected override void OnAwake()
		{
			this.accessToken = "NOT_USED_ON_IOS_FACEBOOK";
		}

		public void OnGetDeepLinkComplete(string message)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
			if (this.deepLinkDelegate == null)
			{
				return;
			}
			object empty = string.Empty;
			dictionary.TryGetValue("deep_link", ref empty);
			this.deepLinkDelegate(new FBResult(empty.ToString(), null));
		}

		private void OnInitComplete(string msg)
		{
			if (!string.IsNullOrEmpty(msg))
			{
				this.OnLogin(msg);
			}
			this.externalInitDelegate();
		}

		public void OnLogin(string msg)
		{
			if (string.IsNullOrEmpty(msg))
			{
				base.OnAuthResponse(new FBResult("{\"cancelled\":true}", null));
				return;
			}
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(msg);
			if (dictionary.ContainsKey("user_id"))
			{
				this.isLoggedIn = true;
			}
			this.ParseLoginDict(dictionary);
			base.OnAuthResponse(new FBResult(msg, null));
		}

		public void OnLogout(string msg)
		{
			this.isLoggedIn = false;
		}

		public void OnRequestComplete(string msg)
		{
			int num = msg.IndexOf(":");
			if (num <= 0)
			{
				FbDebug.Error("Malformed callback from ios.  I expected the form id:message but couldn't find either the ':' character or the id.");
				FbDebug.Error(string.Concat("Here's the message that errored: ", msg));
				return;
			}
			string str = msg.Substring(0, num);
			string str1 = msg.Substring(num + 1);
			FbDebug.Info(string.Concat("id:", str, " msg:", str1));
			base.OnFacebookResponse(str, new FBResult(str1, null));
		}

		public void ParseLoginDict(Dictionary<string, object> parameters)
		{
			if (parameters.ContainsKey("user_id"))
			{
				this.userId = (string)parameters.get_Item("user_id");
			}
			if (parameters.ContainsKey("access_token"))
			{
				this.accessToken = (string)parameters.get_Item("access_token");
			}
			if (parameters.ContainsKey("expiration_timestamp"))
			{
				this.accessTokenExpiresAt = this.FromTimestamp(int.Parse((string)parameters.get_Item("expiration_timestamp")));
			}
		}

		public override void Pay(string product, string action = "purchaseitem", int quantity = 1, int? quantityMin = null, int? quantityMax = null, string requestId = null, string pricepointId = null, string testCurrency = null, FacebookDelegate callback = null)
		{
			throw new PlatformNotSupportedException("There is no Facebook Pay Dialog on iOS");
		}

		public override void PublishInstall(string appId, FacebookDelegate callback = null)
		{
			this.iosFBSettingsPublishInstall(Convert.ToInt32(base.AddFacebookDelegate(callback)), appId);
		}

		public enum FBInsightsFlushBehavior
		{
			FBInsightsFlushBehaviorAuto,
			FBInsightsFlushBehaviorExplicitOnly
		}

		private class NativeDict
		{
			public int numEntries;

			public string[] keys;

			public string[] vals;

			public NativeDict()
			{
				this.numEntries = 0;
				this.keys = null;
				this.vals = null;
			}
		}
	}
}