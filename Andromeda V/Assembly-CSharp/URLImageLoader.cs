using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class URLImageLoader : MonoBehaviour
{
	private static URLImageLoader m_Instance;

	private static Hashtable memoizedImages;

	public static URLImageLoader Instance
	{
		get
		{
			if (URLImageLoader.m_Instance == null)
			{
				GameObject gameObject = new GameObject("URLImageLoaderObject");
				URLImageLoader.m_Instance = (URLImageLoader)gameObject.AddComponent(typeof(URLImageLoader));
			}
			return URLImageLoader.m_Instance;
		}
	}

	static URLImageLoader()
	{
		URLImageLoader.m_Instance = null;
		URLImageLoader.memoizedImages = new Hashtable();
	}

	public URLImageLoader()
	{
	}

	[DebuggerHidden]
	public IEnumerator LoadInBackground(string url, GuiTexture outputTexture)
	{
		URLImageLoader.<LoadInBackground>c__Iterator6 variable = null;
		return variable;
	}

	public static void LoadToGuiTexture(string url, GuiTexture outputTexture)
	{
		if (URLImageLoader.memoizedImages.Contains(url))
		{
			outputTexture.SetTexture2D((Texture2D)URLImageLoader.memoizedImages.get_Item(url));
		}
		outputTexture.SetTexture("GUI", "URLImageLoader_Loading");
		URLImageLoader.Instance.StartCoroutine(URLImageLoader.Instance.LoadInBackground(url, outputTexture));
	}
}