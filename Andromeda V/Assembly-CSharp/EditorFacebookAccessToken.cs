using Facebook;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EditorFacebookAccessToken : MonoBehaviour
{
	private const float windowWidth = 592f;

	private float windowHeight = 200f;

	private string accessToken = string.Empty;

	private bool isLoggingIn;

	private static GUISkin fbSkin;

	private GUIStyle greyButton;

	public EditorFacebookAccessToken()
	{
	}

	private void OnGUI()
	{
		float _height = (float)(Screen.get_height() / 2) - this.windowHeight / 2f;
		float _width = (float)(Screen.get_width() / 2) - 296f;
		if (EditorFacebookAccessToken.fbSkin == null)
		{
			this.greyButton = GUI.get_skin().get_button();
		}
		else
		{
			GUI.set_skin(EditorFacebookAccessToken.fbSkin);
			this.greyButton = EditorFacebookAccessToken.fbSkin.GetStyle("greyButton");
		}
		GUI.ModalWindow(this.GetHashCode(), new Rect(_width, _height, 592f, this.windowHeight), new GUI.WindowFunction(this, EditorFacebookAccessToken.OnGUIDialog), "Unity Editor Facebook Login");
	}

	private void OnGUIDialog(int windowId)
	{
		GUI.set_enabled(!this.isLoggingIn);
		GUILayout.Space(10f);
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.BeginVertical(new GUILayoutOption[0]);
		GUILayout.Space(10f);
		GUILayout.Label("User Access Token:", new GUILayoutOption[0]);
		GUILayout.EndVertical();
		this.accessToken = GUILayout.TextField(this.accessToken, GUI.get_skin().get_textArea(), new GUILayoutOption[] { GUILayout.MinWidth(400f) });
		GUILayout.EndHorizontal();
		GUILayout.Space(20f);
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		if (GUILayout.Button("Find Access Token", new GUILayoutOption[0]))
		{
			Application.OpenURL(string.Format("https://developers.facebook.com/tools/accesstoken/?app_id={0}", FB.AppId));
		}
		GUILayout.FlexibleSpace();
		GUIContent gUIContent = new GUIContent("Login");
		if (GUI.Button(GUILayoutUtility.GetRect(gUIContent, GUI.get_skin().get_button()), gUIContent))
		{
			EditorFacebook component = FBComponentFactory.GetComponent<EditorFacebook>(IfNotExist.AddNew);
			component.AccessToken = this.accessToken;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.set_Item("batch", "[{\"method\":\"GET\", \"relative_url\":\"me?fields=id\"},{\"method\":\"GET\", \"relative_url\":\"app?fields=id\"}]");
			dictionary.set_Item("method", "POST");
			dictionary.set_Item("access_token", this.accessToken);
			FB.API("/", HttpMethod.GET, new FacebookDelegate(component.MockLoginCallback), dictionary);
			this.isLoggingIn = true;
		}
		GUI.set_enabled(true);
		GUIContent gUIContent1 = new GUIContent("Cancel");
		Rect rect = GUILayoutUtility.GetRect(gUIContent1, this.greyButton);
		if (GUI.Button(rect, gUIContent1, this.greyButton))
		{
			FBComponentFactory.GetComponent<EditorFacebook>(IfNotExist.AddNew).MockCancelledLoginCallback();
			Object.Destroy(this);
		}
		GUILayout.EndHorizontal();
		if (Event.get_current().get_type() == 7)
		{
			this.windowHeight = rect.get_y() + rect.get_height() + (float)GUI.get_skin().get_window().get_padding().get_bottom();
		}
	}

	[DebuggerHidden]
	private IEnumerator Start()
	{
		return new EditorFacebookAccessToken.<Start>c__Iterator4();
	}
}