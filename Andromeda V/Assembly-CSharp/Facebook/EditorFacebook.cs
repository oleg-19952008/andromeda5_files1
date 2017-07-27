using Facebook.MiniJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Facebook
{
	internal class EditorFacebook : AbstractFacebook, IFacebook
	{
		private AbstractFacebook fb;

		private FacebookDelegate loginCallback;

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

		public override bool LimitEventUsage
		{
			get
			{
				return this.limitEventUsage;
			}
			set
			{
				this.limitEventUsage = value;
			}
		}

		public EditorFacebook()
		{
		}

		public override void AppEventsLogEvent(string logEvent, float? valueToSum = null, Dictionary<string, object> parameters = null)
		{
			FbDebug.Log("Pew! Pretending to send this off.  Doesn't actually work in the editor");
		}

		public override void AppEventsLogPurchase(float logPurchase, string currency = "USD", Dictionary<string, object> parameters = null)
		{
			FbDebug.Log("Pew! Pretending to send this off.  Doesn't actually work in the editor");
		}

		public override void AppRequest(string message, string[] to = null, string filters = "", string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "", FacebookDelegate callback = null)
		{
			this.fb.AppRequest(message, to, filters, excludeIds, maxRecipients, data, title, callback);
		}

		private void BadAccessToken(string error)
		{
			FbDebug.Error(error);
			this.userId = string.Empty;
			this.fb.UserId = string.Empty;
			this.accessToken = string.Empty;
			this.fb.AccessToken = string.Empty;
			FBComponentFactory.GetComponent<EditorFacebookAccessToken>(IfNotExist.AddNew);
		}

		public override void FeedRequest(string toId = "", string link = "", string linkName = "", string linkCaption = "", string linkDescription = "", string picture = "", string mediaSource = "", string actionName = "", string actionLink = "", string reference = "", Dictionary<string, string[]> properties = null, FacebookDelegate callback = null)
		{
			this.fb.FeedRequest(toId, link, linkName, linkCaption, linkDescription, picture, mediaSource, actionName, actionLink, reference, properties, callback);
		}

		public override void GetAuthResponse(FacebookDelegate callback = null)
		{
			this.fb.GetAuthResponse(callback);
		}

		public override void GetDeepLink(FacebookDelegate callback)
		{
			FbDebug.Info("No Deep Linking in the Editor");
			if (callback != null)
			{
				callback(new FBResult("<platform dependent>", null));
			}
		}

		public override void Init(InitDelegate onInitComplete, string appId, bool cookie = false, bool logging = true, bool status = true, bool xfbml = false, string channelUrl = "", string authResponse = null, bool frictionlessRequests = false, HideUnityDelegate hideUnityDelegate = null)
		{
			base.StartCoroutine(this.OnInit(onInitComplete, appId, cookie, logging, status, xfbml, channelUrl, authResponse, frictionlessRequests, hideUnityDelegate));
		}

		public override void Login(string scope = "", FacebookDelegate callback = null)
		{
			base.AddAuthDelegate(callback);
			FBComponentFactory.GetComponent<EditorFacebookAccessToken>(IfNotExist.AddNew);
		}

		public override void Logout()
		{
			this.isLoggedIn = false;
			this.userId = string.Empty;
			this.accessToken = string.Empty;
			this.fb.UserId = string.Empty;
			this.fb.AccessToken = string.Empty;
		}

		public void MockCancelledLoginCallback()
		{
			base.OnAuthResponse(new FBResult(string.Empty, null));
		}

		public void MockLoginCallback(FBResult result)
		{
			Object.Destroy(FBComponentFactory.GetComponent<EditorFacebookAccessToken>(IfNotExist.AddNew));
			if (result.Error != null)
			{
				this.BadAccessToken(result.Error);
				return;
			}
			try
			{
				List<object> list = (List<object>)Json.Deserialize(result.Text);
				List<string> list1 = new List<string>();
				foreach (object obj in list)
				{
					if (obj is Dictionary<string, object>)
					{
						Dictionary<string, object> dictionary = (Dictionary<string, object>)obj;
						if (dictionary.ContainsKey("body"))
						{
							list1.Add((string)dictionary.get_Item("body"));
						}
					}
				}
				Dictionary<string, object> dictionary1 = (Dictionary<string, object>)Json.Deserialize(list1.get_Item(0));
				Dictionary<string, object> dictionary2 = (Dictionary<string, object>)Json.Deserialize(list1.get_Item(1));
				if (FB.AppId == (string)dictionary2.get_Item("id"))
				{
					this.userId = (string)dictionary1.get_Item("id");
					this.fb.UserId = this.userId;
					this.fb.AccessToken = this.accessToken;
					this.isLoggedIn = true;
					base.OnAuthResponse(new FBResult(string.Empty, null));
				}
				else
				{
					this.BadAccessToken(string.Concat("Access token is not for current app id: ", FB.AppId));
				}
			}
			catch (Exception exception)
			{
				this.BadAccessToken(string.Concat("Could not get data from access token: ", exception.get_Message()));
			}
		}

		protected override void OnAwake()
		{
			base.StartCoroutine(FB.RemoteFacebookLoader.LoadFacebookClass("CanvasFacebook", new FB.RemoteFacebookLoader.LoadedDllCallback(this.OnDllLoaded)));
		}

		private void OnDllLoaded(IFacebook fb)
		{
			this.fb = (AbstractFacebook)fb;
		}

		[DebuggerHidden]
		private IEnumerator OnInit(InitDelegate onInitComplete, string appId, bool cookie = false, bool logging = true, bool status = true, bool xfbml = false, string channelUrl = "", string authResponse = null, bool frictionlessRequests = false, HideUnityDelegate hideUnityDelegate = null)
		{
			EditorFacebook.<OnInit>c__Iterator3 variable = null;
			return variable;
		}

		public override void Pay(string product, string action = "purchaseitem", int quantity = 1, int? quantityMin = null, int? quantityMax = null, string requestId = null, string pricepointId = null, string testCurrency = null, FacebookDelegate callback = null)
		{
			FbDebug.Info("Pay method only works with Facebook Canvas.  Does nothing in the Unity Editor, iOS or Android");
		}

		public override void PublishInstall(string appId, FacebookDelegate callback = null)
		{
		}
	}
}