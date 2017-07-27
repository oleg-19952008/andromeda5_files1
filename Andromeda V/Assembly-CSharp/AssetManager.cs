using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class AssetManager
{
	public int size = 1;

	private int progressed;

	private SortedList<string, LoadingAsset> loadQueue = new SortedList<string, LoadingAsset>();

	private SortedList<string, LoadingAsset> loadedBundles = new SortedList<string, LoadingAsset>();

	private StringCollection loadPriorities = new StringCollection();

	public LoadingAsset currentLoad;

	public string debug = "...";

	public float Progress
	{
		get
		{
			if (playWebGame.isAllBundlesLoaded)
			{
				return 1f;
			}
			if (this.currentLoad == null)
			{
				return (float)this.progressed;
			}
			if (!playWebGame.loadResFromAssets)
			{
				return ((float)this.progressed + 1f * (float)this.currentLoad.size) / (float)this.size;
			}
			return ((float)this.progressed + this.currentLoad.www.get_progress() * (float)this.currentLoad.size) / (float)this.size;
		}
	}

	public AssetManager()
	{
	}

	public void AddJob(LoadingAsset job, bool putOnTop)
	{
		AssetManager assetManager = this;
		Monitor.Enter(assetManager);
		try
		{
			this.loadQueue.Add(job.assetName, job);
			AssetManager assetManager1 = this;
			assetManager1.size = assetManager1.size + job.size;
			if (!putOnTop)
			{
				this.loadPriorities.Add(job.assetName);
			}
			else
			{
				this.loadPriorities.Insert(0, job.assetName);
			}
		}
		finally
		{
			Monitor.Exit(assetManager);
		}
	}

	public Texture2D[] GetAnimationSet(string name)
	{
		Object[] objArray = null;
		objArray = (!playWebGame.loadResFromAssets ? Resources.LoadAll(name, typeof(Texture2D)) : playWebGame.allBundles.get_Item(name).www.get_assetBundle().LoadAll(typeof(Texture2D)));
		Texture2D[] texture2DArray = new Texture2D[(int)objArray.Length];
		for (int i = 0; i < (int)objArray.Length; i++)
		{
			texture2DArray[i] = (Texture2D)objArray[i];
		}
		if (texture2DArray == null || (int)texture2DArray.Length < 1)
		{
			Debug.LogError("Could not load textures in GetAnimationSet");
			Texture2D texture2D = new Texture2D(1, 1);
			texture2D.SetPixel(0, 0, new Color(0f, 0f, 0f, 0f));
			texture2D.Apply();
			texture2DArray = new Texture2D[] { texture2D };
		}
		return texture2DArray;
	}

	public Object GetFromStaticSet(string setName, string resName)
	{
		if (playWebGame.loadResFromAssets)
		{
			return this.GetResourceAt(setName, resName);
		}
		return Resources.Load(string.Concat(setName, "/", resName));
	}

	public Object GetPrefab(string name)
	{
		if (!playWebGame.loadResFromAssets)
		{
			return Resources.Load(name);
		}
		return this.GetResourceAt(name, AssetManager.GetResourceNameFromPath(name));
	}

	private Object GetResourceAt(string bundleName, string resourceName)
	{
		Object obj;
		AssetManager assetManager = this;
		Monitor.Enter(assetManager);
		try
		{
			if (!playWebGame.loadResFromAssets)
			{
				obj = Resources.Load(string.Concat(bundleName, "/", resourceName));
			}
			else
			{
				LoadingAsset loadingAsset = null;
				if (!playWebGame.allBundles.TryGetValue(bundleName, ref loadingAsset))
				{
					Debug.LogError(string.Concat("Could not load from bundle ", bundleName, " resName=", resourceName));
					throw new Exception(string.Concat("Could not load from bundle ", bundleName));
				}
				if (playWebGame.allBundles.get_Item(bundleName).www == null)
				{
					Debug.LogError(string.Concat("Null www for resource ", bundleName, "/", resourceName));
				}
				if (!playWebGame.allBundles.get_Item(bundleName).www.get_assetBundle().Contains(resourceName))
				{
					Debug.LogError(string.Concat("Could not load resource ", bundleName, "/", resourceName));
				}
				obj = playWebGame.allBundles.get_Item(bundleName).www.get_assetBundle().Load(resourceName);
			}
		}
		finally
		{
			Monitor.Exit(assetManager);
		}
		return obj;
	}

	private static string GetResourceNameFromPath(string path)
	{
		string[] strArray = path.Split(new char[] { '/' });
		return strArray[(int)strArray.Length - 1];
	}

	public Object GetStatic(string name)
	{
		if (!playWebGame.loadResFromAssets)
		{
			return Resources.Load(name);
		}
		return this.GetResourceAt(name, AssetManager.GetResourceNameFromPath(name));
	}

	public bool IsBundleLoaded(string assetName)
	{
		// 
		// Current member / type: System.Boolean AssetManager::IsBundleLoaded(System.String)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Boolean IsBundleLoaded(System.String)
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..() в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\RebuildLockStatements.cs:строка 81
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\RebuildLockStatements.cs:строка 24
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeVisitor.cs:строка 69
		//    в ..(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\RebuildLockStatements.cs:строка 19
		//    в ..(MethodBody ,  , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 88
		//    в ..(MethodBody , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 70
		//    в Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 95
		//    в Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 58
		//    в ..(ILanguage , MethodDefinition ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:строка 117
		// 
		// mailto: JustDecompilePublicFeedback@telerik.com

	}

	private void ManageErroredBundle()
	{
		try
		{
			Debug.Log(string.Format("Errored! name={0}, error={1}", this.currentLoad.assetName, this.currentLoad.www.get_error() ?? "NULL"));
			this.currentLoad.state = LoadAssetState.Error;
			this.debug = "errored";
			this.loadPriorities.Remove(this.currentLoad.assetName);
			this.loadPriorities.Add(this.currentLoad.assetName);
			this.currentLoad = null;
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			Debug.LogError(exception);
			this.debug = string.Concat(exception.get_Message(), exception.get_StackTrace());
		}
	}

	public void UnloadAssetBundle(string assetName)
	{
		AssetManager assetManager = this;
		Monitor.Enter(assetManager);
		try
		{
			LoadingAsset item = this.loadedBundles.get_Item(assetName);
			item.www.get_assetBundle().Unload(true);
			this.loadedBundles.Remove(assetName);
		}
		finally
		{
			Monitor.Exit(assetManager);
		}
	}
}