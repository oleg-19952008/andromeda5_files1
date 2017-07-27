using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.XPath;
using UnityEngine;

public class LoginMobileScript : MonoBehaviour
{
	private GameServer registrationServer;

	private bool isTextPositionSet;

	private float positionX;

	private Vector2 labelSize;

	private GUIStyle styleProgress;

	private string statusText = "Initializing Systems...";

	private bool isInFirstState;

	private bool isInSecondState;

	private bool isInThirdState;

	private float labelUpdatePeriod = 0.2f;

	public LoginMobileScript()
	{
	}

	private byte[] AssembleWhMessage()
	{
		// 
		// Current member / type: System.Byte[] LoginMobileScript::AssembleWhMessage()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Byte[] AssembleWhMessage()
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

	private void CheckFirstBoot()
	{
		if (Application.get_platform() == 11 || Application.get_platform() == 8)
		{
			if (!PlayerPrefs.HasKey("FirstBoot"))
			{
				PlayerPrefs.SetInt("FirstBoot", 0);
				playWebGame.isInitialBoot = true;
			}
			else
			{
				playWebGame.isInitialBoot = false;
			}
		}
	}

	private void FixedUpdate()
	{
		LoginMobileScript _fixedDeltaTime = this;
		_fixedDeltaTime.labelUpdatePeriod = _fixedDeltaTime.labelUpdatePeriod - Time.get_fixedDeltaTime();
		if (this.labelUpdatePeriod <= 0f)
		{
			if (!this.isInFirstState)
			{
				this.isInFirstState = true;
				this.statusText = "Initializing Ship Systems.  ";
				this.labelUpdatePeriod = 0.2f;
			}
			else if (!this.isInSecondState)
			{
				this.isInSecondState = true;
				this.statusText = "Initializing Ship Systems.. ";
				this.labelUpdatePeriod = 0.2f;
			}
			else if (this.isInThirdState)
			{
				this.isInFirstState = false;
				this.isInSecondState = false;
				this.isInThirdState = false;
				this.statusText = "Initializing Ship Systems   ";
				this.labelUpdatePeriod = 0.2f;
			}
			else
			{
				this.isInThirdState = true;
				this.statusText = "Initializing Ship Systems...";
				this.labelUpdatePeriod = 0.2f;
			}
		}
	}

	private void LoginWithHash(string player_id, string timestamp, string hash, bool is_playNow = false)
	{
		// 
		// Current member / type: System.Void LoginMobileScript::LoginWithHash(System.String,System.String,System.String,System.Boolean)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void LoginWithHash(System.String,System.String,System.String,System.Boolean)
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..( , Int32 , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:строка 78
		//    в ..( , Int32& , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:строка 33
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 57
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 481
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 83
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
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

	private void OnGUI()
	{
		if (!this.isTextPositionSet)
		{
			this.isTextPositionSet = true;
			this.labelSize = this.styleProgress.CalcSize(new GUIContent(this.statusText));
			this.positionX = (float)(Screen.get_width() / 2) - this.labelSize.x / 2f;
		}
		GUI.Label(new Rect(this.positionX, (float)Screen.get_height() * 0.85f, this.labelSize.x, this.labelSize.y), this.statusText, this.styleProgress);
	}

	[DebuggerHidden]
	private IEnumerator RegisterPlayNow()
	{
		LoginMobileScript.<RegisterPlayNow>c__IteratorA variable = null;
		return variable;
	}

	private void Start()
	{
		GUIStyle gUIStyle = new GUIStyle();
		gUIStyle.set_alignment(3);
		gUIStyle.set_font(GuiLabel.FontBold);
		gUIStyle.set_fontSize(24);
		GUIStyleState gUIStyleState = new GUIStyleState();
		gUIStyleState.set_textColor(new Color(0.2187f, 0.4648f, 0.746f));
		gUIStyle.set_normal(gUIStyleState);
		this.styleProgress = gUIStyle;
		if (!(PlayerPrefs.GetString("PlayNow_hash") != string.Empty) || PlayerPrefs.GetString("PlayNow_hash") == null)
		{
			base.StartCoroutine(this.RegisterPlayNow());
		}
		else
		{
			playWebGame.LOGIN_SERVER_IP = PlayerPrefs.GetString("PlayNow_LoginServer");
			playWebGame.ASSET_URL = PlayerPrefs.GetString("PlayNow_LoginAssets");
			playWebGame.GAME_TYPE = PlayerPrefs.GetString("PlayNow_LoginGameType");
			Debug.Log(string.Concat("LoginServer: ", playWebGame.LOGIN_SERVER_IP));
			Debug.Log(string.Concat("AssetServer: ", playWebGame.ASSET_URL));
			Debug.Log(string.Concat("GameType: ", playWebGame.GAME_TYPE));
			this.LoginWithHash(PlayerPrefs.GetString("PlayNow_player_id"), PlayerPrefs.GetString("PlayNow_timestamp"), PlayerPrefs.GetString("PlayNow_hash"), false);
		}
	}
}