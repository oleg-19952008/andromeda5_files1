using Facebook;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public sealed class InteractiveConsole : MonoBehaviour
{
	private bool isInit;

	public string FriendSelectorTitle = string.Empty;

	public string FriendSelectorMessage = "Derp";

	public string FriendSelectorFilters = "[\"all\",\"app_users\",\"app_non_users\"]";

	public string FriendSelectorData = "{}";

	public string FriendSelectorExcludeIds = string.Empty;

	public string FriendSelectorMax = string.Empty;

	public string DirectRequestTitle = string.Empty;

	public string DirectRequestMessage = "Herp";

	private string DirectRequestTo = string.Empty;

	public string FeedToId = string.Empty;

	public string FeedLink = string.Empty;

	public string FeedLinkName = string.Empty;

	public string FeedLinkCaption = string.Empty;

	public string FeedLinkDescription = string.Empty;

	public string FeedPicture = string.Empty;

	public string FeedMediaSource = string.Empty;

	public string FeedActionName = string.Empty;

	public string FeedActionLink = string.Empty;

	public string FeedReference = string.Empty;

	public bool IncludeFeedProperties;

	private Dictionary<string, string[]> FeedProperties = new Dictionary<string, string[]>();

	public string PayProduct = string.Empty;

	public string ApiQuery = string.Empty;

	public float PlayerLevel = 1f;

	public string Width = "800";

	public string Height = "600";

	public bool CenterHorizontal = true;

	public bool CenterVertical;

	public string Top = "10";

	public string Left = "10";

	private string status = "Ready";

	private string lastResponse = string.Empty;

	public GUIStyle textStyle = new GUIStyle();

	private Texture2D lastResponseTexture;

	private Vector2 scrollPosition = Vector2.get_zero();

	private int buttonHeight = 24;

	private int mainWindowWidth = 500;

	private int mainWindowFullWidth = 530;

	private int TextWindowHeight
	{
		get
		{
			return Screen.get_height();
		}
	}

	public InteractiveConsole()
	{
	}

	private void Awake()
	{
		this.textStyle.set_alignment(0);
		this.textStyle.set_wordWrap(true);
		this.textStyle.set_padding(new RectOffset(10, 10, 10, 10));
		this.textStyle.set_stretchHeight(true);
		this.textStyle.set_stretchWidth(false);
		this.FeedProperties.Add("key1", new string[] { "valueString1" });
		this.FeedProperties.Add("key2", new string[] { "valueString2", "http://www.facebook.com" });
	}

	private bool Button(string label)
	{
		return GUILayout.Button(label, new GUILayoutOption[] { GUILayout.MinHeight((float)this.buttonHeight), GUILayout.MaxWidth((float)this.mainWindowWidth) });
	}

	public void CallAppEventLogEvent()
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.set_Item("fb_level", "Player Level");
		FB.AppEvents.LogEvent("fb_mobile_level_achieved", new float?(this.PlayerLevel), dictionary);
		InteractiveConsole playerLevel = this;
		playerLevel.PlayerLevel = playerLevel.PlayerLevel + 1f;
	}

	private void CallAppRequestAsDirectRequest()
	{
		if (this.DirectRequestTo == string.Empty)
		{
			throw new ArgumentException("\"To Comma Ids\" must be specificed", "to");
		}
		string directRequestTitle = this.DirectRequestTitle;
		FacebookDelegate facebookDelegate = new FacebookDelegate(this.Callback);
		string directRequestMessage = this.DirectRequestMessage;
		string directRequestTo = this.DirectRequestTo;
		char[] chrArray = new char[] { ',' };
		int? nullable = null;
		FB.AppRequest(directRequestMessage, directRequestTo.Split(chrArray), string.Empty, null, nullable, string.Empty, directRequestTitle, facebookDelegate);
	}

	private void CallAppRequestAsFriendSelector()
	{
		string[] strArray;
		int? nullable = null;
		if (this.FriendSelectorMax != string.Empty)
		{
			try
			{
				nullable = new int?(int.Parse(this.FriendSelectorMax));
			}
			catch (Exception exception)
			{
				this.status = exception.get_Message();
			}
		}
		if (this.FriendSelectorExcludeIds != string.Empty)
		{
			strArray = this.FriendSelectorExcludeIds.Split(new char[] { ',' });
		}
		else
		{
			strArray = null;
		}
		string[] strArray1 = strArray;
		string friendSelectorFilters = this.FriendSelectorFilters;
		FB.AppRequest(this.FriendSelectorMessage, null, friendSelectorFilters, strArray1, nullable, this.FriendSelectorData, this.FriendSelectorTitle, new FacebookDelegate(this.Callback));
	}

	private void Callback(FBResult result)
	{
		this.lastResponseTexture = null;
		if (!string.IsNullOrEmpty(result.Error))
		{
			this.lastResponse = string.Concat("Error Response:\n", result.Error);
		}
		else if (this.ApiQuery.Contains("/picture"))
		{
			this.lastResponseTexture = result.Texture;
			this.lastResponse = "Success Response:\n";
		}
		else
		{
			this.lastResponse = string.Concat("Success Response:\n", result.Text);
		}
	}

	public void CallCanvasSetResolution()
	{
		int num = 0;
		int num1 = 0;
		float single = 0f;
		float single1 = 0f;
		if (!int.TryParse(this.Width, ref num))
		{
			num = 800;
		}
		if (!int.TryParse(this.Height, ref num1))
		{
			num1 = 600;
		}
		if (!float.TryParse(this.Top, ref single))
		{
			single = 0f;
		}
		if (!float.TryParse(this.Left, ref single1))
		{
			single1 = 0f;
		}
		if (this.CenterHorizontal && this.CenterVertical)
		{
			FB.Canvas.SetResolution(num, num1, false, 0, new FBScreen.Layout[] { FBScreen.CenterVertical(), FBScreen.CenterHorizontal() });
		}
		else if (this.CenterHorizontal)
		{
			FB.Canvas.SetResolution(num, num1, false, 0, new FBScreen.Layout[] { FBScreen.Top(single), FBScreen.CenterHorizontal() });
		}
		else if (!this.CenterVertical)
		{
			FB.Canvas.SetResolution(num, num1, false, 0, new FBScreen.Layout[] { FBScreen.Top(single), FBScreen.Left(single1) });
		}
		else
		{
			FB.Canvas.SetResolution(num, num1, false, 0, new FBScreen.Layout[] { FBScreen.CenterVertical(), FBScreen.Left(single1) });
		}
	}

	private void CallFBAPI()
	{
		FB.API(this.ApiQuery, HttpMethod.GET, new FacebookDelegate(this.Callback), (Dictionary<string, string>)null);
	}

	private void CallFBFeed()
	{
		Dictionary<string, string[]> feedProperties = null;
		if (this.IncludeFeedProperties)
		{
			feedProperties = this.FeedProperties;
		}
		FB.Feed(this.FeedToId, this.FeedLink, this.FeedLinkName, this.FeedLinkCaption, this.FeedLinkDescription, this.FeedPicture, this.FeedMediaSource, this.FeedActionName, this.FeedActionLink, this.FeedReference, feedProperties, new FacebookDelegate(this.Callback));
	}

	private void CallFBGetDeepLink()
	{
		FB.GetDeepLink(new FacebookDelegate(this.Callback));
	}

	private void CallFBInit()
	{
		FB.Init(new InitDelegate(this.OnInitComplete), new HideUnityDelegate(this.OnHideUnity), null);
	}

	private void CallFBLogin()
	{
		FB.Login("email,publish_actions", new FacebookDelegate(this.LoginCallback));
	}

	private void CallFBLogout()
	{
		FB.Logout();
	}

	private void CallFBPay()
	{
		int? nullable = null;
		int? nullable1 = null;
		FB.Canvas.Pay(this.PayProduct, "purchaseitem", 1, nullable, nullable1, null, null, null, null);
	}

	private void CallFBPublishInstall()
	{
		FB.PublishInstall(new FacebookDelegate(this.PublishComplete));
	}

	private bool IsHorizontalLayout()
	{
		return true;
	}

	private void LabelAndTextField(string label, ref string text)
	{
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Label(label, new GUILayoutOption[] { GUILayout.MaxWidth(150f) });
		text = GUILayout.TextField(text, new GUILayoutOption[0]);
		GUILayout.EndHorizontal();
	}

	private void LoginCallback(FBResult result)
	{
		if (result.Error != null)
		{
			this.lastResponse = string.Concat("Error Response:\n", result.Error);
		}
		else if (FB.IsLoggedIn)
		{
			this.lastResponse = "Login was successful!";
		}
		else
		{
			this.lastResponse = "Login cancelled by Player";
		}
	}

	private void OnGUI()
	{
		if (this.IsHorizontalLayout())
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.BeginVertical(new GUILayoutOption[0]);
		}
		GUILayout.Space(5f);
		GUILayout.Box(string.Concat("Status: ", this.status), new GUILayoutOption[] { GUILayout.MinWidth((float)this.mainWindowWidth) });
		this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition, new GUILayoutOption[] { GUILayout.MinWidth((float)this.mainWindowFullWidth) });
		GUILayout.BeginVertical(new GUILayoutOption[0]);
		GUI.set_enabled(!this.isInit);
		if (this.Button("FB.Init"))
		{
			this.CallFBInit();
			this.status = string.Concat("FB.Init() called with ", FB.AppId);
		}
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUI.set_enabled(this.isInit);
		if (this.Button("Login"))
		{
			this.CallFBLogin();
			this.status = "Login called";
		}
		GUILayout.EndHorizontal();
		GUI.set_enabled(FB.IsLoggedIn);
		GUILayout.Space(10f);
		this.LabelAndTextField("Title (optional): ", ref this.FriendSelectorTitle);
		this.LabelAndTextField("Message: ", ref this.FriendSelectorMessage);
		this.LabelAndTextField("Exclude Ids (optional): ", ref this.FriendSelectorExcludeIds);
		this.LabelAndTextField("Filters (optional): ", ref this.FriendSelectorFilters);
		this.LabelAndTextField("Max Recipients (optional): ", ref this.FriendSelectorMax);
		this.LabelAndTextField("Data (optional): ", ref this.FriendSelectorData);
		if (this.Button("Open Friend Selector"))
		{
			try
			{
				this.CallAppRequestAsFriendSelector();
				this.status = "Friend Selector called";
			}
			catch (Exception exception)
			{
				this.status = exception.get_Message();
			}
		}
		GUILayout.Space(10f);
		this.LabelAndTextField("Title (optional): ", ref this.DirectRequestTitle);
		this.LabelAndTextField("Message: ", ref this.DirectRequestMessage);
		this.LabelAndTextField("To Comma Ids: ", ref this.DirectRequestTo);
		if (this.Button("Open Direct Request"))
		{
			try
			{
				this.CallAppRequestAsDirectRequest();
				this.status = "Direct Request called";
			}
			catch (Exception exception1)
			{
				this.status = exception1.get_Message();
			}
		}
		GUILayout.Space(10f);
		this.LabelAndTextField("To Id (optional): ", ref this.FeedToId);
		this.LabelAndTextField("Link (optional): ", ref this.FeedLink);
		this.LabelAndTextField("Link Name (optional): ", ref this.FeedLinkName);
		this.LabelAndTextField("Link Desc (optional): ", ref this.FeedLinkDescription);
		this.LabelAndTextField("Link Caption (optional): ", ref this.FeedLinkCaption);
		this.LabelAndTextField("Picture (optional): ", ref this.FeedPicture);
		this.LabelAndTextField("Media Source (optional): ", ref this.FeedMediaSource);
		this.LabelAndTextField("Action Name (optional): ", ref this.FeedActionName);
		this.LabelAndTextField("Action Link (optional): ", ref this.FeedActionLink);
		this.LabelAndTextField("Reference (optional): ", ref this.FeedReference);
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Label("Properties (optional)", new GUILayoutOption[] { GUILayout.Width(150f) });
		this.IncludeFeedProperties = GUILayout.Toggle(this.IncludeFeedProperties, "Include", new GUILayoutOption[0]);
		GUILayout.EndHorizontal();
		if (this.Button("Open Feed Dialog"))
		{
			try
			{
				this.CallFBFeed();
				this.status = "Feed dialog called";
			}
			catch (Exception exception2)
			{
				this.status = exception2.get_Message();
			}
		}
		GUILayout.Space(10f);
		this.LabelAndTextField("API: ", ref this.ApiQuery);
		if (this.Button("Call API"))
		{
			this.status = "API called";
			this.CallFBAPI();
		}
		GUILayout.Space(10f);
		if (this.Button("Take & upload screenshot"))
		{
			this.status = "Take screenshot";
			base.StartCoroutine(this.TakeScreenshot());
		}
		if (this.Button("Get Deep Link"))
		{
			this.CallFBGetDeepLink();
		}
		GUILayout.Space(10f);
		GUILayout.EndVertical();
		GUILayout.EndScrollView();
		if (this.IsHorizontalLayout())
		{
			GUILayout.EndVertical();
		}
		GUI.set_enabled(true);
		Rect rect = GUILayoutUtility.GetRect(640f, (float)this.TextWindowHeight);
		Rect rect1 = rect;
		object[] appId = new object[] { FB.AppId, default(object), default(object), default(object), default(object), default(object), default(object) };
		appId[1] = (!this.isInit ? "Not Loaded" : "Loaded Successfully");
		appId[2] = FB.UserId;
		appId[3] = FB.IsLoggedIn;
		appId[4] = FB.AccessToken;
		appId[5] = FB.AccessTokenExpiresAt;
		appId[6] = this.lastResponse;
		GUI.TextArea(rect1, string.Format(" AppId: {0} \n Facebook Dll: {1} \n UserId: {2}\n IsLoggedIn: {3}\n AccessToken: {4}\n AccessTokenExpiresAt: {5}\n {6}", appId), this.textStyle);
		if (this.lastResponseTexture != null)
		{
			float _y = rect.get_y() + 200f;
			if ((float)(Screen.get_height() - this.lastResponseTexture.get_height()) < _y)
			{
				_y = (float)(Screen.get_height() - this.lastResponseTexture.get_height());
			}
			GUI.Label(new Rect(rect.get_x() + 5f, _y, (float)this.lastResponseTexture.get_width(), (float)this.lastResponseTexture.get_height()), this.lastResponseTexture);
		}
		if (this.IsHorizontalLayout())
		{
			GUILayout.EndHorizontal();
		}
	}

	private void OnHideUnity(bool isGameShown)
	{
		Debug.Log(string.Concat("Is game showing? ", isGameShown));
	}

	private void OnInitComplete()
	{
		Debug.Log(string.Concat("FB.Init completed: Is user logged in? ", FB.IsLoggedIn));
		this.isInit = true;
	}

	private void PublishComplete(FBResult result)
	{
		Debug.Log(string.Concat("publish response: ", result.Text));
	}

	[DebuggerHidden]
	private IEnumerator TakeScreenshot()
	{
		InteractiveConsole.<TakeScreenshot>c__Iterator0 variable = null;
		return variable;
	}
}