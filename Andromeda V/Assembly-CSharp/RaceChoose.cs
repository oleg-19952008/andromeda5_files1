using System;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using TransferableObjects;
using UnityEngine;

public class RaceChoose : MonoBehaviour
{
	private string errorText;

	private bool showErrorMessageBox;

	private GameObject playerShip;

	private PureUdpClient udp;

	private string loginServer;

	private string shipname = string.Empty;

	public RaceChoose()
	{
	}

	public void ConnectionRestoredAfterProblem()
	{
		this.showErrorMessageBox = false;
	}

	public void DisconnectOnNetworkProblem()
	{
		this.errorText = "You have been removed from the game due to connectivity problem";
		this.showErrorMessageBox = true;
	}

	private void OnGUI()
	{
		if (this.showErrorMessageBox)
		{
			GUI.Label(new Rect(100f, 100f, 150f, 50f), this.errorText);
		}
		GUI.Box(new Rect((float)(Screen.get_width() / 2 - 180), (float)(Screen.get_height() / 2 - 50), 360f, 100f), "Andromeda 5 Race Choose");
		if (GUI.Button(new Rect((float)(Screen.get_width() / 2 - 160), (float)(Screen.get_height() / 2 + 15), 150f, 30f), "Race I") && this.StartFraction(1))
		{
			Debug.Log("Successfuly created player of fraction I");
			playWebGame.authorization.returnCode = 0;
		}
		if (GUI.Button(new Rect((float)(Screen.get_width() / 2 + 10), (float)(Screen.get_height() / 2 + 15), 150f, 30f), "Race II") && this.StartFraction(2))
		{
			Debug.Log("Successfuly created player of fraction II");
			playWebGame.authorization.returnCode = 0;
		}
		GUI.Label(new Rect((float)(Screen.get_width() / 2 - 160), (float)(Screen.get_height() / 2 - 20), 150f, 28f), "Enter name for your ship:");
		this.shipname = GUI.TextField(new Rect((float)(Screen.get_width() / 2 + 10), (float)(Screen.get_height() / 2 - 20), 150f, 26f), this.shipname);
	}

	public void ServerOrderedExit()
	{
		this.errorText = "Server ordered exit command. Please login again!";
		this.showErrorMessageBox = true;
	}

	private void Start()
	{
		this.loginServer = PlayerPrefs.GetString("loginServer");
		if (this.loginServer == null || this.loginServer == string.Empty)
		{
			this.loginServer = "193.203.199.11";
			PlayerPrefs.SetString("loginServer", this.loginServer);
		}
	}

	private bool StartFraction(int id)
	{
		// 
		// Current member / type: System.Boolean RaceChoose::StartFraction(System.Int32)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Boolean StartFraction(System.Int32)
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..( , Int32 , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:строка 78
		//    в ..( , Int32& , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:строка 33
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 57
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 33
		//    в ..(MethodBody ,  , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 88
		//    в ..(MethodBody , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 70
		//    в Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 95
		//    в Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 58
		//    в ..(ILanguage , MethodDefinition ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:строка 117
		// 
		// mailto: JustDecompilePublicFeedback@telerik.com

	}

	private void Update()
	{
	}

	public void WarnPlayerOnServer()
	{
		this.errorText = "Problem with server connectivity";
		this.showErrorMessageBox = true;
	}
}