using Facebook.MiniJSON;
using System;
using System.Collections.Generic;

namespace Facebook
{
	internal sealed class AndroidFacebook : AbstractFacebook, IFacebook
	{
		public const int BrowserDialogMode = 0;

		private const string AndroidJavaFacebookClass = "com.facebook.unity.FB";

		private const string CallbackIdKey = "callback_id";

		private string keyHash;

		private FacebookDelegate deepLinkDelegate;

		private InitDelegate onInitComplete;

		public override int DialogMode
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public string KeyHash
		{
			get
			{
				return this.keyHash;
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
				this.CallFB("SetLimitEventUsage", value.ToString());
			}
		}

		public AndroidFacebook()
		{
		}

		public override void AppEventsLogEvent(string logEvent, float? valueToSum = null, Dictionary<string, object> parameters = null)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.set_Item("logEvent", logEvent);
			if (valueToSum.get_HasValue())
			{
				dictionary.set_Item("valueToSum", valueToSum.get_Value());
			}
			if (parameters != null)
			{
				dictionary.set_Item("parameters", this.ToStringDict(parameters));
			}
			this.CallFB("AppEvents", Json.Serialize(dictionary));
		}

		public override void AppEventsLogPurchase(float logPurchase, string currency = "USD", Dictionary<string, object> parameters = null)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.set_Item("logPurchase", logPurchase);
			dictionary.set_Item("currency", (string.IsNullOrEmpty(currency) ? "USD" : currency));
			if (parameters != null)
			{
				dictionary.set_Item("parameters", this.ToStringDict(parameters));
			}
			this.CallFB("AppEvents", Json.Serialize(dictionary));
		}

		public override void AppRequest(string message, string[] to = null, string filters = "", string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "", FacebookDelegate callback = null)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.set_Item("message", message);
			if (callback != null)
			{
				dictionary.set_Item("callback_id", base.AddFacebookDelegate(callback));
			}
			if (to != null)
			{
				dictionary.set_Item("to", string.Join(",", to));
			}
			if (!string.IsNullOrEmpty(filters))
			{
				dictionary.set_Item("filters", filters);
			}
			if (maxRecipients.get_HasValue())
			{
				dictionary.set_Item("max_recipients", maxRecipients.get_Value());
			}
			if (!string.IsNullOrEmpty(data))
			{
				dictionary.set_Item("data", data);
			}
			if (!string.IsNullOrEmpty(title))
			{
				dictionary.set_Item("title", title);
			}
			this.CallFB("AppRequest", Json.Serialize(dictionary));
		}

		private void CallFB(string method, string args)
		{
			FbDebug.Error("Using Android when not on an Android build!  Doesn't Work!");
		}

		public override void FeedRequest(string toId = "", string link = "", string linkName = "", string linkCaption = "", string linkDescription = "", string picture = "", string mediaSource = "", string actionName = "", string actionLink = "", string reference = "", Dictionary<string, string[]> properties = null, FacebookDelegate callback = null)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (callback != null)
			{
				dictionary.set_Item("callback_id", base.AddFacebookDelegate(callback));
			}
			if (!string.IsNullOrEmpty(toId))
			{
				dictionary.Add("to", toId);
			}
			if (!string.IsNullOrEmpty(link))
			{
				dictionary.Add("link", link);
			}
			if (!string.IsNullOrEmpty(linkName))
			{
				dictionary.Add("name", linkName);
			}
			if (!string.IsNullOrEmpty(linkCaption))
			{
				dictionary.Add("caption", linkCaption);
			}
			if (!string.IsNullOrEmpty(linkDescription))
			{
				dictionary.Add("description", linkDescription);
			}
			if (!string.IsNullOrEmpty(picture))
			{
				dictionary.Add("picture", picture);
			}
			if (!string.IsNullOrEmpty(mediaSource))
			{
				dictionary.Add("source", mediaSource);
			}
			if (!string.IsNullOrEmpty(actionName) && !string.IsNullOrEmpty(actionLink))
			{
				Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
				dictionary1.Add("name", actionName);
				dictionary1.Add("link", actionLink);
				dictionary.Add("actions", new Dictionary<string, object>[] { dictionary1 });
			}
			if (!string.IsNullOrEmpty(reference))
			{
				dictionary.Add("ref", reference);
			}
			if (properties != null)
			{
				Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
				foreach (KeyValuePair<string, string[]> property in properties)
				{
					if ((int)property.get_Value().Length >= 1)
					{
						if ((int)property.get_Value().Length != 1)
						{
							Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
							dictionary3.Add("text", property.get_Value()[0]);
							dictionary3.Add("href", property.get_Value()[1]);
							dictionary2.Add(property.get_Key(), dictionary3);
						}
						else
						{
							dictionary2.Add(property.get_Key(), property.get_Value()[0]);
						}
					}
				}
				dictionary.Add("properties", dictionary2);
			}
			this.CallFB("FeedRequest", Json.Serialize(dictionary));
		}

		private DateTime FromTimestamp(int timestamp)
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			return dateTime.AddSeconds((double)timestamp);
		}

		public override void GetDeepLink(FacebookDelegate callback)
		{
			if (callback != null)
			{
				this.deepLinkDelegate = callback;
				this.CallFB("GetDeepLink", string.Empty);
			}
		}

		public override void Init(InitDelegate onInitComplete, string appId, bool cookie = false, bool logging = true, bool status = true, bool xfbml = false, string channelUrl = "", string authResponse = null, bool frictionlessRequests = false, HideUnityDelegate hideUnityDelegate = null)
		{
			if (string.IsNullOrEmpty(appId))
			{
				throw new ArgumentException("appId cannot be null or empty!");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("appId", appId);
			if (cookie)
			{
				dictionary.Add("cookie", true);
			}
			if (!logging)
			{
				dictionary.Add("logging", false);
			}
			if (!status)
			{
				dictionary.Add("status", false);
			}
			if (xfbml)
			{
				dictionary.Add("xfbml", true);
			}
			if (!string.IsNullOrEmpty(channelUrl))
			{
				dictionary.Add("channelUrl", channelUrl);
			}
			if (!string.IsNullOrEmpty(authResponse))
			{
				dictionary.Add("authResponse", authResponse);
			}
			if (frictionlessRequests)
			{
				dictionary.Add("frictionlessRequests", true);
			}
			string str = Json.Serialize(dictionary);
			this.onInitComplete = onInitComplete;
			this.CallFB("Init", str.ToString());
		}

		private bool IsErrorResponse(string response)
		{
			return false;
		}

		public override void Login(string scope = "", FacebookDelegate callback = null)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("scope", scope);
			string str = Json.Serialize(dictionary);
			base.AddAuthDelegate(callback);
			this.CallFB("Login", str);
		}

		public override void Logout()
		{
			this.CallFB("Logout", string.Empty);
		}

		public void OnAccessTokenRefresh(string message)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
			if (dictionary.ContainsKey("access_token"))
			{
				this.accessToken = (string)dictionary.get_Item("access_token");
				this.accessTokenExpiresAt = this.FromTimestamp(int.Parse((string)dictionary.get_Item("expiration_timestamp")));
			}
		}

		public void OnAppRequestsComplete(string message)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
			if (dictionary.ContainsKey("callback_id"))
			{
				Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
				string item = (string)dictionary.get_Item("callback_id");
				dictionary.Remove("callback_id");
				if (dictionary.get_Count() <= 0)
				{
					base.OnFacebookResponse(item, new FBResult(Json.Serialize(dictionary1), "Malformed request response.  Please file a bug with facebook here: https://developers.facebook.com/bugs/create"));
				}
				else
				{
					List<string> list = new List<string>(dictionary.get_Count() - 1);
					foreach (string key in dictionary.get_Keys())
					{
						if (key.StartsWith("to"))
						{
							list.Add((string)dictionary.get_Item(key));
						}
						else
						{
							dictionary1.set_Item(key, dictionary.get_Item(key));
						}
					}
					dictionary1.Add("to", list);
					dictionary.Clear();
					base.OnFacebookResponse(item, new FBResult(Json.Serialize(dictionary1), null));
				}
			}
		}

		protected override void OnAwake()
		{
			this.keyHash = string.Empty;
		}

		public void OnFeedRequestComplete(string message)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
			if (dictionary.ContainsKey("callback_id"))
			{
				Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
				string item = (string)dictionary.get_Item("callback_id");
				dictionary.Remove("callback_id");
				if (dictionary.get_Count() <= 0)
				{
					base.OnFacebookResponse(item, new FBResult(Json.Serialize(dictionary1), "Malformed request response.  Please file a bug with facebook here: https://developers.facebook.com/bugs/create"));
				}
				else
				{
					foreach (string key in dictionary.get_Keys())
					{
						dictionary1.set_Item(key, dictionary.get_Item(key));
					}
					dictionary.Clear();
					base.OnFacebookResponse(item, new FBResult(Json.Serialize(dictionary1), null));
				}
			}
		}

		public void OnGetDeepLinkComplete(string message)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
			if (this.deepLinkDelegate != null)
			{
				object empty = string.Empty;
				dictionary.TryGetValue("deep_link", ref empty);
				this.deepLinkDelegate(new FBResult(empty.ToString(), null));
			}
		}

		public void OnInitComplete(string message)
		{
			this.OnLoginComplete(message);
			if (this.onInitComplete != null)
			{
				this.onInitComplete();
			}
		}

		public void OnLoginComplete(string message)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
			if (dictionary.ContainsKey("user_id"))
			{
				this.isLoggedIn = true;
				this.userId = (string)dictionary.get_Item("user_id");
				this.accessToken = (string)dictionary.get_Item("access_token");
				this.accessTokenExpiresAt = this.FromTimestamp(int.Parse((string)dictionary.get_Item("expiration_timestamp")));
			}
			if (dictionary.ContainsKey("key_hash"))
			{
				this.keyHash = (string)dictionary.get_Item("key_hash");
			}
			base.OnAuthResponse(new FBResult(message, null));
		}

		public void OnLogoutComplete(string message)
		{
			this.isLoggedIn = false;
			this.userId = string.Empty;
			this.accessToken = string.Empty;
		}

		public void OnPublishInstallComplete(string message)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)Json.Deserialize(message);
			if (dictionary.ContainsKey("callback_id"))
			{
				base.OnFacebookResponse((string)dictionary.get_Item("callback_id"), new FBResult(string.Empty, null));
			}
		}

		public override void Pay(string product, string action = "purchaseitem", int quantity = 1, int? quantityMin = null, int? quantityMax = null, string requestId = null, string pricepointId = null, string testCurrency = null, FacebookDelegate callback = null)
		{
			throw new PlatformNotSupportedException("There is no Facebook Pay Dialog on Android");
		}

		public override void PublishInstall(string appId, FacebookDelegate callback = null)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(2);
			dictionary.set_Item("app_id", appId);
			if (callback != null)
			{
				dictionary.set_Item("callback_id", base.AddFacebookDelegate(callback));
			}
			this.CallFB("PublishInstall", Json.Serialize(dictionary));
		}

		private Dictionary<string, string> ToStringDict(Dictionary<string, object> dict)
		{
			if (dict == null)
			{
				return null;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (KeyValuePair<string, object> keyValuePair in dict)
			{
				dictionary.set_Item(keyValuePair.get_Key(), keyValuePair.get_Value().ToString());
			}
			return dictionary;
		}
	}
}