using Facebook;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using TransferableObjects;
using UnityEngine;

public class SignUpNowWindow : GuiWindow
{
	private GuiLabel lblUserName;

	private GuiLabel lblPassword;

	private GuiLabel lblEmail;

	private GuiLabel lblEnterDetails;

	private GuiLabel lblOr;

	private GuiLabel lblGetFreeBooster;

	private GuiLabel lblLoading;

	private GuiLabel lblError;

	public GuiTextBox tbUsername;

	public GuiTextBox tbEmail;

	public GuiPasswordBox tbPassword;

	private GuiCheckbox cbTerms;

	private GuiButton btnTerms;

	public GuiButtonFixed btnRegister;

	public GuiButtonFixed btnRegisterFB;

	public GuiWindow errorWindow;

	private string fbUsername;

	private string fbNickname;

	private string fbEmail;

	private string fbId;

	private StringBuilder sb;

	private bool isInit;

	public SignUpNowWindow()
	{
	}

	private void Callback(FBResult result)
	{
		if (!FB.IsLoggedIn)
		{
			Debug.Log("User cancelled login");
		}
		else
		{
			Debug.Log(FB.UserId);
			Debug.Log("Get the name of facebook user");
			FB.API("me?fields=name,email", HttpMethod.GET, new FacebookDelegate(this.FBDataCallback), (Dictionary<string, string>)null);
		}
	}

	private void CallFBInit()
	{
		FB.Init(new InitDelegate(this.OnInitComplete), new HideUnityDelegate(this.OnHideUnity), null);
	}

	private void CallFBLogin()
	{
		FB.Login("email,publish_actions", new FacebookDelegate(this.Callback));
	}

	public override void Create()
	{
		base.SetBackgroundTexture("NewGUI", "signupFrame");
		base.PutToCenter();
		this.zOrder = 210;
		this.isHidden = false;
		this.errorWindow = new GuiWindow();
		this.lblEnterDetails = new GuiLabel()
		{
			boundries = new Rect(170f, 140f, 260f, 24f),
			text = StaticData.Translate("key_enter_details"),
			FontSize = 20,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 4
		};
		base.AddGuiElement(this.lblEnterDetails);
		this.lblUserName = new GuiLabel()
		{
			boundries = new Rect(140f, 190f, 142f, 18f),
			text = StaticData.Translate("key_login_username"),
			FontSize = 16,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 3
		};
		base.AddGuiElement(this.lblUserName);
		this.tbUsername = new GuiTextBox();
		this.tbUsername.SetSingleTexture("GUI", "empty");
		this.tbUsername.boundries = new Rect(308f, 182f, 154f, 30f);
		this.tbUsername.Alignment = 3;
		this.tbUsername.FontSize = 16;
		this.tbUsername.text = string.Empty;
		this.tbUsername.TextColor = GuiNewStyleBar.blueColor;
		base.AddGuiElement(this.tbUsername);
		this.lblPassword = new GuiLabel()
		{
			boundries = new Rect(140f, 230f, 142f, 18f),
			text = StaticData.Translate("key_login_password"),
			FontSize = 16,
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.lblPassword);
		this.tbPassword = new GuiPasswordBox();
		this.tbPassword.SetSingleTexture("GUI", "empty");
		this.tbPassword.redrawOnFocus = false;
		this.tbPassword.boundries = new Rect(308f, 222f, 154f, 30f);
		this.tbPassword.Alignment = 3;
		this.tbPassword.FontSize = 16;
		this.tbPassword.text = string.Empty;
		this.tbPassword.control_name = "loginPassword";
		this.tbPassword.TextColor = GuiNewStyleBar.blueColor;
		base.AddGuiElement(this.tbPassword);
		this.lblEmail = new GuiLabel()
		{
			boundries = new Rect(140f, 270f, 142f, 18f),
			text = StaticData.Translate("key_login_email"),
			FontSize = 16,
			Alignment = 3,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.lblEmail);
		this.tbEmail = new GuiTextBox();
		this.tbEmail.SetSingleTexture("GUI", "empty");
		this.tbEmail.boundries = new Rect(308f, 262f, 154f, 30f);
		this.tbEmail.Alignment = 3;
		this.tbEmail.FontSize = 16;
		this.tbEmail.text = string.Empty;
		this.tbEmail.TextColor = GuiNewStyleBar.blueColor;
		base.AddGuiElement(this.tbEmail);
		this.cbTerms = new GuiCheckbox()
		{
			X = 212f,
			Y = 320f,
			Selected = true,
			isActive = true
		};
		base.AddGuiElement(this.cbTerms);
		this.btnTerms = new GuiButton()
		{
			boundries = new Rect(242f, 323f, 150f, 18f),
			Caption = StaticData.Translate("key_login_register_terms"),
			FontSize = 14,
			textColorNormal = GuiNewStyleBar.blueColor,
			textColorHover = Color.get_white(),
			Alignment = 3,
			Clicked = new Action<EventHandlerParam>(this, SignUpNowWindow.OnRegisterTermsClicked)
		};
		base.AddGuiElement(this.btnTerms);
		this.btnRegister = new GuiButtonFixed();
		this.btnRegister.SetTexture("LoginGui", "btnLogin");
		this.btnRegister.X = 189f;
		this.btnRegister.Y = 359f;
		this.btnRegister.Clicked = new Action<EventHandlerParam>(this, SignUpNowWindow.RegisterUser);
		this.btnRegister.Caption = StaticData.Translate("key_signup_now").ToUpper();
		this.btnRegister.FontSize = 18;
		this.btnRegister.textColorNormal = Color.get_black();
		this.btnRegister.textColorHover = Color.get_black();
		this.btnRegister.Alignment = 4;
		base.AddGuiElement(this.btnRegister);
	}

	public void DrawErrorOnSignUp(string message)
	{
		this.DrawErrorWindows(message);
	}

	public void DrawErrorWindows(string error)
	{
		AndromedaGui.gui.RemoveWindow(this.errorWindow.handler);
		this.errorWindow.Clear();
		this.errorWindow.SetBackgroundTexture("FrameworkGUI", "menugui_dialog");
		this.errorWindow.PutToCenter();
		this.errorWindow.boundries.set_y(130f);
		this.errorWindow.isHidden = false;
		this.errorWindow.zOrder = 250;
		AndromedaGui.gui.AddWindow(this.errorWindow);
		this.lblError = new GuiLabel()
		{
			boundries = new Rect(65f, 66f, 389f, 90f),
			text = error,
			FontSize = 18,
			TextColor = GuiNewStyleBar.redColor,
			Alignment = 4
		};
		this.errorWindow.AddGuiElement(this.lblError);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable()
		{
			Width = 100f,
			Caption = StaticData.Translate("key_login_dialog_ok"),
			FontSize = 12,
			Alignment = 4,
			X = 210f,
			Y = 160f,
			Clicked = new Action<EventHandlerParam>(this, SignUpNowWindow.OnErrorOkClicked)
		};
		this.errorWindow.AddGuiElement(guiButtonResizeable);
	}

	private void DrawLoadingWindow()
	{
		this.errorWindow.Clear();
		this.errorWindow.SetBackgroundTexture("FrameworkGUI", "menugui_dialog");
		this.errorWindow.PutToCenter();
		this.errorWindow.boundries.set_y(180f);
		this.errorWindow.isHidden = false;
		this.errorWindow.zOrder = 250;
		AndromedaGui.gui.AddWindow(this.errorWindow);
		this.lblLoading = new GuiLabel()
		{
			boundries = new Rect(65f, 66f, 389f, 90f),
			text = StaticData.Translate("key_guild_loading_data"),
			FontSize = 18,
			TextColor = GuiNewStyleBar.redColor,
			Alignment = 4
		};
		this.errorWindow.AddGuiElement(this.lblLoading);
	}

	private void FBDataCallback(FBResult result)
	{
		// 
		// Current member / type: System.Void SignUpNowWindow::FBDataCallback(FBResult)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void FBDataCallback(FBResult)
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

	private void OnErrorOkClicked(EventHandlerParam prm)
	{
		this.errorWindow.Clear();
		this.errorWindow.isHidden = true;
		AndromedaGui.gui.RemoveWindow(this.errorWindow.handler);
		this.btnRegister.isEnabled = true;
	}

	private void OnFacebookClicked(EventHandlerParam prm)
	{
		playWebGame.udp.ExecuteCommand(PureUdpClient.CommandIamOnBackground, null);
		if (this.isInit)
		{
			this.CallFBLogin();
			Debug.Log("LOGIN FB");
		}
		else
		{
			Debug.Log("INIT FB");
			this.CallFBInit();
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
		if (FB.IsLoggedIn)
		{
			Debug.Log("Already logged.Nice");
			Debug.Log(string.Concat("user id : ", FB.UserId));
			Debug.Log("Get the name of facebook user");
			FB.API("me?fields=name,email", HttpMethod.GET, new FacebookDelegate(this.FBDataCallback), (Dictionary<string, string>)null);
		}
		else
		{
			Debug.Log("CallFbLogin");
			this.CallFBLogin();
		}
	}

	[DebuggerHidden]
	private IEnumerator OnRegisterPlayClicked()
	{
		SignUpNowWindow.<OnRegisterPlayClicked>c__Iterator14 variable = null;
		return variable;
	}

	private void OnRegisterTermsClicked(EventHandlerParam prm)
	{
		Application.OpenURL("http://xs-software.com/company.php?go=terms&l=en");
	}

	private void RegisterUser(EventHandlerParam prm)
	{
		if (NetworkScript.player == null || NetworkScript.player.shipScript == null)
		{
			Debug.Log("Shipscript or PlayerData is null");
			return;
		}
		NetworkScript.player.shipScript.StartCoroutine(this.OnRegisterPlayClicked());
	}

	private bool ValidateRegister()
	{
		if (this.tbUsername.text.Trim().get_Length() < 5 || this.tbUsername.text.Trim().get_Length() > 32)
		{
			this.DrawErrorWindows(StaticData.Translate("key_login_user_length"));
			return false;
		}
		if (this.tbPassword.text.get_Length() < 5 || this.tbPassword.text.get_Length() > 40)
		{
			this.DrawErrorWindows(StaticData.Translate("key_login_pass_length"));
			return false;
		}
		if (this.tbEmail.text.Trim().get_Length() < 7 || this.tbEmail.text.Trim().get_Length() > 100 && !this.tbEmail.text.Contains("@"))
		{
			this.DrawErrorWindows(StaticData.Translate("key_login_email_length"));
			return false;
		}
		if (this.cbTerms.Selected)
		{
			return true;
		}
		this.DrawErrorWindows(StaticData.Translate("key_login_agree_with_terms"));
		return false;
	}
}