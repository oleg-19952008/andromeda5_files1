using System;
using System.Collections.Generic;
using UnityEngine;

public class MixpanelExample : MonoBehaviour
{
	public string Token = "aa9a4cf67a4bb4e82b7d8c352de64b39";

	public string DistinctID = "1";

	private string _eventName = "test";

	private string _property1 = "foo";

	private string _property2 = "bar";

	public MixpanelExample()
	{
	}

	public void OnGUI()
	{
		GUILayout.Label("This is an example demonstrating how to use the Mixpanel integration plugin for Unity3D.", new GUILayoutOption[0]);
		GUILayout.Label("All source code for this example is located in \"Assets/Mixpanel Analytics/MixpanelExample.cs\".", new GUILayoutOption[0]);
		if (string.IsNullOrEmpty(Mixpanel.Token))
		{
			GUI.set_color(Color.get_red());
			GUILayout.Label("Step 1: Set the Token property on the 'Mixpanel Example' object to your unique Mixpanel token string.", new GUILayoutOption[0]);
		}
		if (string.IsNullOrEmpty(Mixpanel.Token))
		{
			return;
		}
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Label("Event Name:", new GUILayoutOption[0]);
		this._eventName = GUILayout.TextField(this._eventName, new GUILayoutOption[0]);
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Label("Property 1:", new GUILayoutOption[0]);
		this._property1 = GUILayout.TextField(this._property1, new GUILayoutOption[0]);
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Label("Property 2:", new GUILayoutOption[0]);
		this._property2 = GUILayout.TextField(this._property2, new GUILayoutOption[0]);
		GUILayout.EndHorizontal();
		if (GUILayout.Button("Send Event", new GUILayoutOption[0]))
		{
			string str = this._eventName;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("property1", this._property1);
			dictionary.Add("property2", this._property2);
			Mixpanel.SendEvent(str, dictionary);
		}
	}

	public void Start()
	{
		Mixpanel.Token = "aa9a4cf67a4bb4e82b7d8c352de64b39";
		Mixpanel.DistinctID = this.DistinctID;
		Mixpanel.SuperProperties.Add("platform", Application.get_platform().ToString());
		Mixpanel.SuperProperties.Add("quality", QualitySettings.get_names()[QualitySettings.GetQualityLevel()]);
		Mixpanel.SuperProperties.Add("fullscreen", Screen.get_fullScreen());
		Mixpanel.SuperProperties.Add("resolution", string.Concat(Screen.get_width(), "x", Screen.get_height()));
	}
}