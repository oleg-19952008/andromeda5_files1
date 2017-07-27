using System;
using UnityEngine;

public class ExpansionDownloader : MonoBehaviour
{
	private string expPath;

	private string logtxt;

	private bool alreadyLogged;

	private string nextScene = "splash";

	private bool downloadStarted;

	private bool isLoadingLevel;

	public ExpansionDownloader()
	{
	}

	private void Awake()
	{
	}
}