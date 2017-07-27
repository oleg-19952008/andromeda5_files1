using Facebook;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.XPath;
using TransferableObjects;
using UnityEngine;

public class LoginScript : MonoBehaviour
{
	private GuiFramework gui;

	private GuiWindow mainWindow;

	private GuiWindow serversWindows;

	private GuiWindow loginWindow;

	private GuiWindow registerWindow;

	private GuiWindow errorWindow;

	private GuiWindow versionWindow;

	private GuiWindow playNowWindow;

	private GuiWindow languageWindow;

	private GuiWindow topWindow;

	private GuiWindow bottomWindow;

	private GuiTexture txBackgroundBase;

	private GuiTexture txBackground;

	private GuiTexture txtServersBackground;

	private GuiTexture txtTopFrame;

	private GuiTexture txtBottomFrame;

	private GuiTexture txtLoginFrame;

	private GuiTexture txtRegFrame;

	private GuiTexture txtPlayNowFrame;

	private GuiTexture txtUSFlag;

	private GuiTexture txtEUFlag;

	private GuiTexture txtLandAmerica;

	private GuiTexture txtLandEurope;

	private GuiTexture txtCurrentLanguage;

	private GuiTexture txtLanguageDropDown;

	private GuiLabel lblMainLeft;

	private GuiLabel lblMainRight;

	private GuiLabel lblUserName;

	private GuiLabel lblPassword;

	private GuiLabel lblEmail;

	private GuiLabel lblLoginTitle;

	private GuiLabel lblPlayNowTitle;

	private GuiLabel lblAmericas;

	private GuiLabel lblEurope;

	private GuiLabel lblServerSelectTitle;

	private GuiLabel lblErrRegisterGeneral;

	private GuiLabel lblFacebookNeuron;

	private GuiLabel lblServers;

	private GuiLabel lblOptions;

	private GuiLabel lblRegStep1;

	private GuiLabel lblRegStep2;

	private GuiLabel lblRegStep3;

	private GuiLabel lblCurrentLanguage;

	private GuiTextBox tbUsername;

	private GuiTextBox tbEmail;

	private GuiTextBox tbServer;

	private GuiPasswordBox tbPassword;

	private GuiButton btnForgottenPassword;

	private GuiButton btnTerms;

	private GuiButtonFixed btnMainLeft;

	private GuiButtonFixed btnMainRight;

	private GuiButtonFixed btnLogin;

	private GuiButtonFixed btnRegister;

	private GuiButtonFixed btnRegisterFB;

	private GuiButtonFixed btnPlayAsGuest;

	private GuiButtonFixed btnClose;

	private GuiButtonFixed btnAmericas;

	private GuiButtonFixed btnEurope;

	private GuiButtonFixed btnMapAmericas;

	private GuiButtonFixed btnMapEurope;

	private GuiButtonFixed btnFraction1;

	private GuiButtonFixed btnFraction2;

	private GuiButtonFixed btnChooseServer;

	private GuiButtonFixed btnChooseLanguage;

	private GuiScrollingContainer serversScroller;

	private GuiCheckbox cbTerms;

	private GuiScrollingContainer LangScroller;

	private string username = string.Empty;

	private string password = string.Empty;

	private string continent = string.Empty;

	private string message = string.Empty;

	private byte chooseServerState;

	private GameServer registrationServer;

	public static short serverVersion;

	private bool isInitStartOfScene = true;

	private GuiWindow slideWindow;

	private GuiWindow slideOutWindow;

	private bool isInTransition;

	private float animationSpeed;

	private float animationTime = 0.8f;

	private float targetCoordinate;

	private bool isSlidingGuiOut;

	private bool isInit;

	static LoginScript()
	{
	}

	public LoginScript()
	{
	}

	private void AddNotifications()
	{
		Debug.Log("AddNotifications");
	}

	private byte[] AssembleWhMessage()
	{
		// 
		// Current member / type: System.Byte[] LoginScript::AssembleWhMessage()
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

	private void BottomWindowOutHandler(object p)
	{
		float _height = this.bottomWindow.boundries.get_height() / this.animationTime * 2.3f;
		if (this.bottomWindow.boundries.get_y() < (float)Screen.get_height())
		{
			ref Rect rectPointer = ref this.bottomWindow.boundries;
			rectPointer.set_y(rectPointer.get_y() + Time.get_deltaTime() * _height);
		}
		else
		{
			this.bottomWindow.boundries.set_y((float)Screen.get_height());
			this.bottomWindow.preDrawHandler = new Action<object>(this, LoginScript.BottomWindowSlideInHandler);
		}
	}

	private void BottomWindowSlideInHandler(object p)
	{
		float _height = this.bottomWindow.boundries.get_height() / this.animationTime * 2.3f;
		if (this.bottomWindow.boundries.get_y() > (float)Screen.get_height() - this.bottomWindow.boundries.get_height())
		{
			ref Rect rectPointer = ref this.bottomWindow.boundries;
			rectPointer.set_y(rectPointer.get_y() - Time.get_deltaTime() * _height);
		}
		else
		{
			this.bottomWindow.boundries.set_y((float)Screen.get_height() - this.bottomWindow.boundries.get_height());
			this.bottomWindow.preDrawHandler = null;
		}
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
			this.chooseServerState = 3;
			base.StartCoroutine(this.GetServers(string.Empty, string.Empty));
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

	private void ClearNotifications()
	{
		Debug.Log("ClearNotifications");
	}

	private void DrawBottomButtons()
	{
		if (this.btnMainLeft != null)
		{
			this.bottomWindow.RemoveGuiElement(this.btnMainLeft);
		}
		if (this.lblMainLeft != null)
		{
			this.bottomWindow.RemoveGuiElement(this.lblMainLeft);
		}
		if (this.btnMainRight != null)
		{
			this.bottomWindow.RemoveGuiElement(this.btnMainRight);
		}
		if (this.lblMainRight != null)
		{
			this.bottomWindow.RemoveGuiElement(this.lblMainRight);
		}
		this.btnMainLeft = new GuiButtonFixed();
		this.btnMainLeft.SetTexture("LoginGui", "btnLogin");
		this.btnMainLeft.X = this.txtBottomFrame.X + 245f;
		this.btnMainLeft.Y = this.txtBottomFrame.Y + 16f;
		this.btnMainLeft.Caption = (this.registerWindow.isHidden ? StaticData.Translate("key_login_bottom_login") : StaticData.Translate("key_login_bottom_play_now"));
		this.btnMainLeft.FontSize = 14;
		this.btnMainLeft.Alignment = 4;
		this.btnMainLeft.textColorNormal = Color.get_black();
		this.btnMainLeft.textColorHover = Color.get_black();
		this.btnMainLeft.clickEventOnUp = true;
		this.btnMainLeft.isMuted = true;
		if (this.registerWindow.isHidden)
		{
			this.btnMainLeft.Clicked = new Action<EventHandlerParam>(this, LoginScript.OnDrawLoginClicked);
		}
		else
		{
			this.btnMainLeft.Clicked = new Action<EventHandlerParam>(this, LoginScript.OnDrawPlayNowClicked);
		}
		this.bottomWindow.AddGuiElement(this.btnMainLeft);
		this.lblMainLeft = new GuiLabel()
		{
			boundries = new Rect(this.txtBottomFrame.X + 50f, this.txtBottomFrame.Y + 20f, 185f, 35f),
			text = (this.registerWindow.isHidden ? StaticData.Translate("key_login_bottom_login_text") : StaticData.Translate("key_login_bottom_play_now_text")),
			FontSize = 14,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 5
		};
		this.bottomWindow.AddGuiElement(this.lblMainLeft);
		this.btnMainRight = new GuiButtonFixed();
		this.btnMainRight.SetTexture("LoginGui", "btnRegister");
		this.btnMainRight.X = this.txtBottomFrame.X + 511f;
		this.btnMainRight.Y = this.txtBottomFrame.Y - 6f;
		this.btnMainRight.Caption = StaticData.Translate("key_login_register_play_now");
		this.btnMainRight.FontSize = 14;
		this.btnMainRight.Alignment = 4;
		this.btnMainRight.textColorNormal = Color.get_black();
		this.btnMainRight.textColorHover = Color.get_black();
		this.btnMainRight.Clicked = new Action<EventHandlerParam>(this, LoginScript.OnRegisterClicked);
		this.btnMainRight.clickEventOnUp = true;
		this.btnMainRight.isMuted = true;
		this.bottomWindow.AddGuiElement(this.btnMainRight);
		this.lblMainRight = new GuiLabel()
		{
			boundries = new Rect(this.txtBottomFrame.X + 776f, this.txtBottomFrame.Y + 20f, 186f, 35f),
			text = StaticData.Translate("key_login_bottom_sign_up_text"),
			FontSize = 14,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 3
		};
		this.bottomWindow.AddGuiElement(this.lblMainRight);
		this.bottomWindow.zOrder = 240;
		this.gui.UpdateWindowsCollection();
	}

	private void DrawChooseServer()
	{
		if (this.isInTransition)
		{
			return;
		}
		this.serversWindows.zOrder = 230;
		this.registerWindow.zOrder = 200;
		this.loginWindow.zOrder = 200;
		this.playNowWindow.zOrder = 200;
		this.serversWindows.Clear();
		this.serversWindows.SetBackgroundTexture("LoginGui", "selectServerFrame");
		this.serversWindows.PutToCenter();
		if (this.serversWindows.isHidden)
		{
			this.SlideInWindow(this.serversWindows);
			this.bottomWindow.preDrawHandler = new Action<object>(this, LoginScript.BottomWindowOutHandler);
		}
		this.lblServerSelectTitle = new GuiLabel()
		{
			boundries = new Rect(254f, 54f, 227f, 23f),
			text = StaticData.Translate("key_login_servers_title").ToString().ToUpper(),
			FontSize = 20,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 4
		};
		this.serversWindows.AddGuiElement(this.lblServerSelectTitle);
		this.btnAmericas = new GuiButtonFixed();
		this.btnAmericas.SetTexture("LoginGui", "btnUS");
		this.btnAmericas.SetTextureDisabled("LoginGui", "btnUSHvr");
		this.btnAmericas.X = 78f;
		this.btnAmericas.Y = 50f;
		this.btnAmericas.Clicked = new Action<EventHandlerParam>(this, LoginScript.OnContinentSwitch);
		this.btnAmericas.eventHandlerParam.customData = "americas";
		this.btnAmericas.Caption = string.Empty;
		this.serversWindows.AddGuiElement(this.btnAmericas);
		this.btnEurope = new GuiButtonFixed();
		this.btnEurope.SetTexture("LoginGui", "btnEU");
		this.btnEurope.SetTextureDisabled("LoginGui", "btnEUHvr");
		this.btnEurope.X = 481f;
		this.btnEurope.Y = 52f;
		this.btnEurope.Clicked = new Action<EventHandlerParam>(this, LoginScript.OnContinentSwitch);
		this.btnEurope.eventHandlerParam.customData = "europe";
		this.btnEurope.Caption = string.Empty;
		this.serversWindows.AddGuiElement(this.btnEurope);
		this.lblAmericas = new GuiLabel()
		{
			boundries = new Rect(123f, 56f, 105f, 19f),
			text = string.Empty,
			FontSize = 14,
			Alignment = 3
		};
		this.serversWindows.AddGuiElement(this.lblAmericas);
		this.txtUSFlag = new GuiTexture();
		this.txtUSFlag.SetTexture("LoginGui", "flagUS");
		this.txtUSFlag.X = 98f;
		this.txtUSFlag.Y = 57f;
		this.serversWindows.AddGuiElement(this.txtUSFlag);
		this.lblEurope = new GuiLabel()
		{
			boundries = new Rect(549f, 56f, 109f, 19f),
			text = string.Empty,
			FontSize = 14,
			Alignment = 3
		};
		this.serversWindows.AddGuiElement(this.lblEurope);
		this.txtEUFlag = new GuiTexture();
		this.txtEUFlag.SetTexture("LoginGui", "flagEU");
		this.txtEUFlag.X = 522f;
		this.txtEUFlag.Y = 57f;
		this.serversWindows.AddGuiElement(this.txtEUFlag);
		this.btnMapAmericas = new GuiButtonFixed();
		this.btnMapAmericas.SetTextureNormal("FrameworkGUI", "empty");
		this.btnMapAmericas.SetTextureHover("LoginGui", "map-americas");
		this.btnMapAmericas.SetTextureDisabled("LoginGui", "map-americas");
		this.btnMapAmericas.SetTextureClicked("LoginGui", "map-americas");
		this.btnMapAmericas.boundries = new Rect(68f, 106f, 269f, 292f);
		this.btnMapAmericas.Clicked = new Action<EventHandlerParam>(this, LoginScript.OnContinentSwitch);
		this.btnMapAmericas.eventHandlerParam.customData = "americas";
		this.btnMapAmericas.Caption = string.Empty;
		this.serversWindows.AddGuiElement(this.btnMapAmericas);
		this.btnMapEurope = new GuiButtonFixed();
		this.btnMapEurope.SetTextureNormal("FrameworkGUI", "empty");
		this.btnMapEurope.SetTextureHover("LoginGui", "map-europe");
		this.btnMapEurope.SetTextureDisabled("LoginGui", "map-europe");
		this.btnMapEurope.SetTextureClicked("LoginGui", "map-europe");
		this.btnMapEurope.boundries = new Rect(315f, 118f, 125f, 128f);
		this.btnMapEurope.Clicked = new Action<EventHandlerParam>(this, LoginScript.OnContinentSwitch);
		this.btnMapEurope.isEnabled = true;
		this.btnMapEurope.eventHandlerParam.customData = "europe";
		this.btnMapEurope.Caption = string.Empty;
		this.serversWindows.AddGuiElement(this.btnMapEurope);
		if (this.continent != "europe")
		{
			this.btnMapAmericas.SetTextureNormal("LoginGui", "map-americas");
		}
		else
		{
			this.btnMapEurope.SetTextureNormal("LoginGui", "map-europe");
		}
		if (this.continent != "europe")
		{
			this.btnAmericas.isEnabled = false;
			this.btnMapAmericas.isEnabled = false;
		}
		else
		{
			this.btnEurope.isEnabled = false;
			this.btnMapEurope.isEnabled = false;
		}
		this.lblOptions = new GuiLabel()
		{
			boundries = new Rect(100f, 409f, 105f, 19f),
			text = StaticData.Translate("key_login_servers_options").ToString(),
			FontSize = 14,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 3
		};
		this.serversWindows.AddGuiElement(this.lblOptions);
		this.lblServers = new GuiLabel()
		{
			boundries = new Rect(282f, 409f, 105f, 19f),
			text = StaticData.Translate("key_login_servers_server").ToString(),
			FontSize = 14,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 3
		};
		this.serversWindows.AddGuiElement(this.lblServers);
		if (this.serversScroller != null)
		{
			this.serversScroller.Claer();
		}
		else
		{
			this.serversScroller = new GuiScrollingContainer(82f, 436f, 568f, 94f, 27, this.serversWindows)
			{
				clippingBoundariesId = 107
			};
			this.serversScroller.SetArrowStep(20f);
		}
		this.serversWindows.AddGuiElement(this.serversScroller);
		int num = 0;
		int length = (int)playWebGame.GameServers.Length;
		int num1 = 0;
		int num2 = 0;
		for (int i = 0; i < length; i++)
		{
			if (this.chooseServerState != 0 || !(playWebGame.GameServers[i].have_account == "0"))
			{
				if (playWebGame.GameServers[i].continent != "europe")
				{
					num1++;
				}
				else
				{
					num2++;
				}
				if (playWebGame.GameServers[i].continent == this.continent)
				{
					GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
					guiButtonFixed.SetTexture("LoginGui", "btnPlay");
					guiButtonFixed.X = 0f;
					guiButtonFixed.Y = (float)num;
					if (this.chooseServerState != 0)
					{
						guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, LoginScript.OnChooseServerRegistration);
					}
					else
					{
						guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, LoginScript.OnChooseServerLogin);
					}
					guiButtonFixed.eventHandlerParam = new EventHandlerParam()
					{
						customData = i
					};
					guiButtonFixed.Caption = string.Empty;
					guiButtonFixed.isMuted = true;
					this.serversScroller.AddContent(guiButtonFixed);
					GuiLabel guiLabel = new GuiLabel()
					{
						boundries = new Rect(0f, (float)num, 134f, 33f),
						text = StaticData.Translate("key_login_servers_play").ToString(),
						FontSize = 12,
						Font = GuiLabel.FontBold,
						TextColor = Color.get_white(),
						Alignment = 4
					};
					this.serversScroller.AddContent(guiLabel);
					GuiTexture guiTexture = new GuiTexture();
					if (playWebGame.GameServers[i].game_type == "int" || playWebGame.GameServers[i].game_type == "development")
					{
						guiTexture.SetTexture("LoginGui", "flagEU");
					}
					else
					{
						guiTexture.SetTexture("LoginGui", "flagUS");
					}
					guiTexture.X = 200f;
					guiTexture.Y = (float)(num + 8);
					this.serversScroller.AddContent(guiTexture);
					GuiLabel guiLabel1 = new GuiLabel()
					{
						boundries = new Rect(230f, (float)num, 80f, 33f),
						text = playWebGame.GameServers[i].server_name,
						FontSize = 12,
						Font = GuiLabel.FontBold,
						TextColor = GuiNewStyleBar.blueColor,
						Alignment = 3
					};
					this.serversScroller.AddContent(guiLabel1);
					num = num + 45;
				}
			}
		}
		this.lblAmericas.text = string.Format(StaticData.Translate("key_login_btn_americas"), num1.ToString());
		this.lblEurope.text = string.Format(StaticData.Translate("key_login_btn_europe"), num2.ToString());
	}

	private void DrawCurrentLanguage()
	{
		this.txtCurrentLanguage = new GuiTexture();
		this.txtCurrentLanguage.SetTexture("LoginGui", string.Concat("btnLanguage_Ipad_", playWebGame.CurrentLanguageKey.ToUpper(), "_Nml"));
		this.txtCurrentLanguage.X = this.txtTopFrame.X + 790f;
		this.txtCurrentLanguage.Y = this.txtTopFrame.Y + 13f;
		this.topWindow.AddGuiElement(this.txtCurrentLanguage);
		this.lblCurrentLanguage = new GuiLabel()
		{
			boundries = new Rect(this.txtTopFrame.X + 830f, this.txtTopFrame.Y + 7f, 150f, 35f),
			text = playWebGame.SupportedLanguages.get_Item(playWebGame.CurrentLanguageKey),
			FontSize = 14,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 3
		};
		this.topWindow.AddGuiElement(this.lblCurrentLanguage);
		this.txtLanguageDropDown = new GuiTexture();
		this.txtLanguageDropDown.SetTexture("FrameworkGUI", "BtnQuestArrowDownNml");
		this.txtLanguageDropDown.X = this.txtTopFrame.X + 830f + this.lblCurrentLanguage.TextWidth + 3f;
		this.txtLanguageDropDown.Y = this.txtTopFrame.Y + 18f;
		this.topWindow.AddGuiElement(this.txtLanguageDropDown);
		this.btnChooseLanguage = new GuiButtonFixed();
		this.btnChooseLanguage.SetTexture("FrameworkGUI", "empty");
		this.btnChooseLanguage.boundries = new Rect(this.txtTopFrame.X + 770f, this.txtTopFrame.Y, 150f, 50f);
		this.btnChooseLanguage.Caption = string.Empty;
		this.btnChooseLanguage.Clicked = new Action<EventHandlerParam>(this, LoginScript.OnDrawChooseLanguage);
		this.topWindow.AddGuiElement(this.btnChooseLanguage);
	}

	private void DrawErrorWindows(string error)
	{
		if (this.isInTransition)
		{
			return;
		}
		this.errorWindow.Clear();
		this.errorWindow.SetBackgroundTexture("FrameworkGUI", "menugui_dialog");
		this.errorWindow.PutToCenter();
		this.errorWindow.boundries.set_y(-this.errorWindow.boundries.get_height());
		this.errorWindow.isHidden = false;
		this.errorWindow.zOrder = 242;
		this.errorWindow.fxEnded = null;
		this.errorWindow.timeHammerFx = 0.6f;
		this.errorWindow.amplitudeHammerShake = 10f;
		this.errorWindow.moveToShakeRatio = 0.6f;
		this.gui.UpdateWindowsCollection();
		this.errorWindow.StartHammerEffect(this.errorWindow.boundries.get_x(), ((float)Screen.get_height() - this.errorWindow.boundries.get_height()) / 2f);
		AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "menu_OpenClose");
		AudioManager.PlayGUISound(fromStaticSet);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(65f, 66f, 389f, 90f),
			text = error,
			FontSize = 18,
			TextColor = GuiNewStyleBar.redColor,
			Alignment = 4
		};
		this.errorWindow.AddGuiElement(guiLabel);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable()
		{
			Width = 100f,
			Caption = StaticData.Translate("key_login_dialog_ok"),
			FontSize = 12,
			Alignment = 4,
			X = 210f,
			Y = 160f,
			Clicked = new Action<EventHandlerParam>(this, LoginScript.OnErrorOkClicked),
			isMuted = true
		};
		this.errorWindow.AddGuiElement(guiButtonResizeable);
	}

	private void DrawLanguageChoose()
	{
		this.languageWindow.Clear();
		this.languageWindow.isHidden = false;
		this.languageWindow.zOrder = 100;
		this.languageWindow.boundries = new Rect(this.topWindow.boundries.get_x() + this.txtCurrentLanguage.X - 5f, -220f, 140f, 220f);
		this.languageWindow.preDrawHandler = new Action<object>(this, LoginScript.LanguageScrollerPreDraw);
		this.gui.UpdateWindowsCollection();
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("LoginGui", "languageSelect");
		guiTexture.boundries = new Rect(0f, 0f, this.languageWindow.boundries.get_width(), this.languageWindow.boundries.get_height());
		this.languageWindow.AddGuiElement(guiTexture);
		this.LangScroller = new GuiScrollingContainer(5f, this.txtCurrentLanguage.Y + 5f, 127f, 192f, 5, this.languageWindow);
		this.LangScroller.SetArrowStep(20f);
		this.languageWindow.AddGuiElement(this.LangScroller);
		int num = 5;
		IEnumerator<string> enumerator = playWebGame.SupportedLanguages.get_Keys().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				string current = enumerator.get_Current();
				GuiTexture rect = new GuiTexture();
				rect.SetTexture("LoginGui", string.Concat("btnLanguage_Ipad_", current.ToUpper(), "_Nml"));
				rect.boundries = new Rect(7f, (float)num, 23f, 17f);
				this.LangScroller.AddContent(rect);
				GuiLabel guiLabel = new GuiLabel()
				{
					boundries = new Rect(35f, (float)(num - 6), 150f, 25f),
					text = playWebGame.SupportedLanguages.get_Item(current),
					FontSize = 13,
					TextColor = GuiNewStyleBar.blueColor,
					Alignment = 3
				};
				this.LangScroller.AddContent(guiLabel);
				GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
				guiButtonFixed.SetTexture("FrameworkGUI", "empty");
				guiButtonFixed.boundries = new Rect(0f, (float)(num - 20), 105f, 44f);
				guiButtonFixed.Caption = string.Empty;
				guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, LoginScript.OnChooseLanguage);
				guiButtonFixed.eventHandlerParam.customData = current;
				guiButtonFixed.isMuted = true;
				this.LangScroller.AddContent(guiButtonFixed);
				num = num + 45;
			}
		}
		finally
		{
			if (enumerator == null)
			{
			}
			enumerator.Dispose();
		}
	}

	private void DrawLoading()
	{
		this.mainWindow.Clear();
		this.mainWindow.boundries = new Rect(0f, 0f, (float)Screen.get_width(), (float)Screen.get_height());
		this.txBackgroundBase = new GuiTexture();
		this.txBackgroundBase.SetTexture("iPad", "loading_screen");
		this.txBackgroundBase.boundries = new Rect(0f, 0f, (float)Screen.get_width(), (float)Screen.get_height());
		this.mainWindow.AddGuiElement(this.txBackgroundBase);
		this.mainWindow.zOrder = 230;
		this.loginWindow.zOrder = 200;
		this.registerWindow.zOrder = 200;
		this.serversWindows.zOrder = 200;
		this.errorWindow.zOrder = 200;
		this.playNowWindow.zOrder = 200;
		this.versionWindow.zOrder = 200;
		this.languageWindow.zOrder = 200;
		this.loginWindow.isHidden = true;
		this.registerWindow.isHidden = true;
		this.serversWindows.isHidden = true;
		this.errorWindow.isHidden = true;
		this.playNowWindow.isHidden = true;
		this.versionWindow.isHidden = true;
		this.languageWindow.isHidden = true;
	}

	private void DrawLoginWindow()
	{
		if (this.isInTransition)
		{
			return;
		}
		this.chooseServerState = 0;
		this.loginWindow.Clear();
		this.txtLoginFrame = new GuiTexture();
		this.txtLoginFrame.SetTexture("LoginGui", "loginFrame");
		this.txtLoginFrame.X = 0f;
		this.txtLoginFrame.Y = 0f;
		this.loginWindow.AddGuiElement(this.txtLoginFrame);
		this.loginWindow.boundries = new Rect(((float)Screen.get_width() - this.txtLoginFrame.boundries.get_width()) / 2f, 40f, this.txtLoginFrame.boundries.get_width(), this.txtLoginFrame.boundries.get_height());
		this.loginWindow.zOrder = 230;
		this.registerWindow.zOrder = 200;
		this.serversWindows.zOrder = 200;
		this.playNowWindow.zOrder = 200;
		this.SlideInWindow(this.loginWindow);
		this.bottomWindow.preDrawHandler = new Action<object>(this, LoginScript.BottomWindowOutHandler);
		if (!this.isInitStartOfScene)
		{
			this.bottomWindow.preDrawHandler = new Action<object>(this, LoginScript.BottomWindowOutHandler);
		}
		this.isInitStartOfScene = false;
		this.lblLoginTitle = new GuiLabel()
		{
			boundries = new Rect(0f, 105f, 566f, 23f),
			text = StaticData.Translate("key_login_title"),
			FontSize = 18,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 4
		};
		this.loginWindow.AddGuiElement(this.lblLoginTitle);
		this.lblErrRegisterGeneral = new GuiLabel()
		{
			boundries = new Rect(0f, 136f, 566f, 23f),
			text = string.Empty,
			FontSize = 12,
			TextColor = Color.get_red(),
			Alignment = 4
		};
		this.loginWindow.AddGuiElement(this.lblErrRegisterGeneral);
		this.lblUserName = new GuiLabel()
		{
			boundries = new Rect(124f, 175f, 140f, 19f),
			text = StaticData.Translate("key_login_username"),
			FontSize = 14,
			Alignment = 0
		};
		this.loginWindow.AddGuiElement(this.lblUserName);
		this.tbUsername = new GuiTextBox();
		this.tbUsername.SetSingleTexture("FrameworkGUI", "empty");
		this.tbUsername.boundries = new Rect(291f, 161f, 170f, 30f);
		this.tbUsername.Alignment = 3;
		this.tbUsername.FontSize = 16;
		this.tbUsername.text = this.username;
		this.tbUsername.TextColor = GuiNewStyleBar.blueColor;
		this.loginWindow.AddGuiElement(this.tbUsername);
		this.lblPassword = new GuiLabel()
		{
			boundries = new Rect(121f, 225f, 140f, 19f),
			text = StaticData.Translate("key_login_password"),
			FontSize = 14,
			Alignment = 0
		};
		this.loginWindow.AddGuiElement(this.lblPassword);
		this.tbPassword = new GuiPasswordBox();
		this.tbPassword.SetSingleTexture("FrameworkGUI", "empty");
		this.tbPassword.redrawOnFocus = false;
		this.tbPassword.boundries = new Rect(291f, 213f, 170f, 30f);
		this.tbPassword.Alignment = 3;
		this.tbPassword.FontSize = 16;
		this.tbPassword.text = this.password;
		this.tbPassword.control_name = "loginPassword";
		this.tbPassword.TextColor = GuiNewStyleBar.blueColor;
		this.loginWindow.AddGuiElement(this.tbPassword);
		this.btnForgottenPassword = new GuiButton()
		{
			boundries = new Rect(306f, 275f, 146f, 18f),
			Caption = StaticData.Translate("key_login_forgotten_password"),
			FontSize = 13,
			textColorNormal = GuiNewStyleBar.blueColor,
			textColorHover = Color.get_white(),
			Alignment = 4,
			Clicked = new Action<EventHandlerParam>(this, LoginScript.OnForgottenPasswordClicked)
		};
		this.loginWindow.AddGuiElement(this.btnForgottenPassword);
		this.btnLogin = new GuiButtonFixed();
		this.btnLogin.SetTexture("LoginGui", "btnLogin");
		this.btnLogin.X = 172f;
		this.btnLogin.Y = 325f;
		this.btnLogin.Clicked = new Action<EventHandlerParam>(this, LoginScript.OnLoginClicked);
		this.btnLogin.Caption = StaticData.Translate("key_login_btn_login").ToUpper();
		this.btnLogin.FontSize = 18;
		this.btnLogin.textColorNormal = Color.get_black();
		this.btnLogin.textColorHover = Color.get_black();
		this.btnLogin.Alignment = 4;
		this.btnLogin.clickEventOnUp = true;
		this.btnLogin.isMuted = true;
		this.loginWindow.AddGuiElement(this.btnLogin);
	}

	private void DrawMainWindow()
	{
		this.mainWindow.Clear();
		this.mainWindow.boundries = new Rect(0f, 0f, (float)Screen.get_width(), (float)Screen.get_height());
		Texture2D texture2D = new Texture2D(1, 1);
		texture2D.SetPixel(0, 0, Color.get_black());
		texture2D.Apply();
		this.txBackgroundBase = new GuiTexture();
		this.txBackgroundBase.SetTexture(texture2D);
		this.txBackgroundBase.boundries = new Rect(0f, 0f, (float)Screen.get_width(), (float)Screen.get_height());
		this.mainWindow.AddGuiElement(this.txBackgroundBase);
		Texture2D texture2D1 = (Texture2D)Resources.Load("splashBkg");
		this.txBackground = new GuiTexture();
		this.txBackground.SetTexture(texture2D1);
		this.mainWindow.AddGuiElement(this.txBackground);
		this.txtTopFrame = new GuiTexture();
		this.txtTopFrame.SetTexture("LoginGui", "topFrame");
		this.txtTopFrame.X = 0f;
		this.txtTopFrame.Y = 0f;
		this.topWindow.boundries = this.txtTopFrame.boundries;
		this.topWindow.boundries.set_x(((float)Screen.get_width() - this.topWindow.boundries.get_width()) / 2f);
		this.topWindow.boundries.set_y(-this.txtTopFrame.boundries.get_height());
		this.topWindow.AddGuiElement(this.txtTopFrame);
		this.txtBottomFrame = new GuiTexture();
		this.txtBottomFrame.SetTexture("LoginGui", "bottomFrame");
		this.txtBottomFrame.X = 0f;
		this.txtBottomFrame.Y = 0f;
		this.bottomWindow.boundries = new Rect(((float)Screen.get_width() - this.txtBottomFrame.boundries.get_width()) / 2f, (float)Screen.get_height(), this.txtBottomFrame.boundries.get_width(), this.txtBottomFrame.boundries.get_height());
		this.bottomWindow.boundries.set_y((float)Screen.get_height());
		this.bottomWindow.AddGuiElement(this.txtBottomFrame);
		this.topWindow.isHidden = false;
		this.bottomWindow.isHidden = false;
		this.topWindow.StartHammerEffect(((float)Screen.get_width() - this.topWindow.boundries.get_width()) / 2f, 0f);
		this.bottomWindow.StartHammerEffect(((float)Screen.get_width() - this.txtBottomFrame.boundries.get_width()) / 2f, (float)Screen.get_height() - this.txtBottomFrame.boundries.get_height());
		this.DrawBottomButtons();
		this.DrawCurrentLanguage();
	}

	private void DrawPlayNowWindow()
	{
		if (this.isInTransition)
		{
			return;
		}
		this.chooseServerState = 1;
		this.playNowWindow.zOrder = 230;
		this.loginWindow.zOrder = 200;
		this.registerWindow.zOrder = 200;
		this.serversWindows.zOrder = 200;
		this.playNowWindow.Clear();
		this.txtPlayNowFrame = new GuiTexture();
		this.txtPlayNowFrame.SetTexture("LoginGui", "playNowFrame");
		this.txtPlayNowFrame.X = 0f;
		this.txtPlayNowFrame.Y = 0f;
		this.playNowWindow.AddGuiElement(this.txtPlayNowFrame);
		this.playNowWindow.boundries = new Rect(((float)Screen.get_width() - this.txtPlayNowFrame.boundries.get_width()) / 2f, 40f, this.txtPlayNowFrame.boundries.get_width(), this.txtPlayNowFrame.boundries.get_height());
		this.SlideInWindow(this.playNowWindow);
		if (!this.isInitStartOfScene)
		{
			this.bottomWindow.preDrawHandler = new Action<object>(this, LoginScript.BottomWindowOutHandler);
		}
		this.isInitStartOfScene = false;
		this.lblPlayNowTitle = new GuiLabel()
		{
			boundries = new Rect(64f, 115f, 430f, 25f),
			text = StaticData.Translate("key_login_play_now_title"),
			FontSize = 21,
			TextColor = Color.get_white(),
			Alignment = 4
		};
		this.playNowWindow.AddGuiElement(this.lblPlayNowTitle);
		this.btnPlayAsGuest = new GuiButtonFixed();
		this.btnPlayAsGuest.SetTexture("LoginGui", "btnPlayNow");
		this.btnPlayAsGuest.X = 65f;
		this.btnPlayAsGuest.Y = 144f;
		this.btnPlayAsGuest.Clicked = new Action<EventHandlerParam>(this, LoginScript.OnPlayNowClicked);
		this.btnPlayAsGuest.Caption = StaticData.Translate("key_login_btn_play_as_guest").ToUpper();
		this.btnPlayAsGuest.FontSize = 26;
		this.btnPlayAsGuest.textColorNormal = Color.get_black();
		this.btnPlayAsGuest.textColorHover = Color.get_black();
		this.btnPlayAsGuest.Alignment = 4;
		this.btnPlayAsGuest.isMuted = true;
		this.btnPlayAsGuest.clickEventOnUp = true;
		this.playNowWindow.AddGuiElement(this.btnPlayAsGuest);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("LoginGui", "separator");
		guiTexture.X = 59f;
		guiTexture.Y = 250f;
		this.playNowWindow.AddGuiElement(guiTexture);
	}

	private void DrawRegisterWindow()
	{
		if (this.isInTransition)
		{
			return;
		}
		this.chooseServerState = 2;
		this.registerWindow.zOrder = 230;
		this.loginWindow.zOrder = 200;
		this.serversWindows.zOrder = 200;
		this.playNowWindow.zOrder = 200;
		this.registerWindow.Clear();
		this.txtRegFrame = new GuiTexture();
		this.txtRegFrame.SetTexture("LoginGui", "registrationFrame");
		this.txtRegFrame.X = 0f;
		this.txtRegFrame.Y = 0f;
		this.registerWindow.AddGuiElement(this.txtRegFrame);
		this.registerWindow.boundries = new Rect(((float)Screen.get_width() - this.txtRegFrame.boundries.get_width()) / 2f, 30f, this.txtRegFrame.boundries.get_width(), this.txtRegFrame.boundries.get_height());
		this.SlideInWindow(this.registerWindow);
		this.bottomWindow.preDrawHandler = new Action<object>(this, LoginScript.BottomWindowOutHandler);
		this.btnClose = new GuiButtonFixed();
		this.btnClose.SetTexture("LoginGui", "btnClose");
		this.btnClose.X = 454f;
		this.btnClose.Y = 75f;
		this.btnClose.Clicked = new Action<EventHandlerParam>(this, LoginScript.OnDrawLoginClicked);
		this.btnClose.Caption = string.Empty;
		this.btnClose.isMuted = true;
		this.registerWindow.AddGuiElement(this.btnClose);
		this.lblRegStep1 = new GuiLabel()
		{
			boundries = new Rect(121f, 111f, 330f, 23f),
			text = StaticData.Translate("key_login_registration_step1"),
			FontSize = 18,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 3
		};
		this.registerWindow.AddGuiElement(this.lblRegStep1);
		this.lblRegStep2 = new GuiLabel()
		{
			boundries = new Rect(121f, 288f, 330f, 23f),
			text = StaticData.Translate("key_login_registration_step2"),
			FontSize = 18,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 3
		};
		this.registerWindow.AddGuiElement(this.lblRegStep2);
		this.lblRegStep3 = new GuiLabel()
		{
			boundries = new Rect(121f, 390f, 330f, 23f),
			text = StaticData.Translate("key_login_registration_step3"),
			FontSize = 18,
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 3
		};
		this.registerWindow.AddGuiElement(this.lblRegStep3);
		this.lblUserName = new GuiLabel()
		{
			boundries = new Rect(121f, 164f, 142f, 18f),
			text = StaticData.Translate("key_login_username"),
			FontSize = 14,
			Alignment = 3
		};
		this.registerWindow.AddGuiElement(this.lblUserName);
		this.tbUsername = new GuiTextBox();
		this.tbUsername.SetSingleTexture("FrameworkGUI", "empty");
		this.tbUsername.boundries = new Rect(296f, 156f, 154f, 30f);
		this.tbUsername.Alignment = 3;
		this.tbUsername.FontSize = 16;
		this.tbUsername.text = string.Empty;
		this.tbUsername.TextColor = GuiNewStyleBar.blueColor;
		this.registerWindow.AddGuiElement(this.tbUsername);
		this.lblPassword = new GuiLabel()
		{
			boundries = new Rect(121f, 203f, 142f, 18f),
			text = StaticData.Translate("key_login_password"),
			FontSize = 14,
			Alignment = 3
		};
		this.registerWindow.AddGuiElement(this.lblPassword);
		this.tbPassword = new GuiPasswordBox();
		this.tbPassword.SetSingleTexture("FrameworkGUI", "empty");
		this.tbPassword.redrawOnFocus = false;
		this.tbPassword.boundries = new Rect(296f, 196f, 154f, 30f);
		this.tbPassword.Alignment = 3;
		this.tbPassword.FontSize = 16;
		this.tbPassword.text = string.Empty;
		this.tbPassword.control_name = "loginPassword";
		this.tbPassword.TextColor = GuiNewStyleBar.blueColor;
		this.registerWindow.AddGuiElement(this.tbPassword);
		this.lblEmail = new GuiLabel()
		{
			boundries = new Rect(121f, 244f, 142f, 18f),
			text = StaticData.Translate("key_login_email"),
			FontSize = 14,
			Alignment = 3
		};
		this.registerWindow.AddGuiElement(this.lblEmail);
		this.tbEmail = new GuiTextBox();
		this.tbEmail.SetSingleTexture("FrameworkGUI", "empty");
		this.tbEmail.boundries = new Rect(296f, 236f, 154f, 30f);
		this.tbEmail.Alignment = 3;
		this.tbEmail.FontSize = 16;
		this.tbEmail.text = string.Empty;
		this.tbEmail.TextColor = GuiNewStyleBar.blueColor;
		this.registerWindow.AddGuiElement(this.tbEmail);
		this.btnFraction1 = new GuiButtonFixed();
		this.btnFraction1.SetTexture("LoginGui", "btnRace2");
		this.btnFraction1.X = 287f;
		this.btnFraction1.Y = 325f;
		this.btnFraction1.isEnabled = false;
		this.btnFraction1.Clicked = new Action<EventHandlerParam>(this, LoginScript.OnRegisterChooseFraction);
		this.btnFraction1.eventHandlerParam.customData = 1;
		this.btnFraction1.Caption = StaticData.Translate("key_login_register_fraction1");
		this.btnFraction1.FontSize = 16;
		this.btnFraction1.textColorNormal = GuiNewStyleBar.blueColor;
		this.btnFraction1.textColorHover = Color.get_white();
		this.btnFraction1.Alignment = 4;
		this.registerWindow.AddGuiElement(this.btnFraction1);
		this.btnFraction2 = new GuiButtonFixed();
		this.btnFraction2.SetTexture("LoginGui", "btnRace1");
		this.btnFraction2.X = 93f;
		this.btnFraction2.Y = 325f;
		this.btnFraction2.Clicked = new Action<EventHandlerParam>(this, LoginScript.OnRegisterChooseFraction);
		this.btnFraction2.eventHandlerParam.customData = 2;
		this.btnFraction2.Caption = StaticData.Translate("key_login_register_fraction2");
		this.btnFraction2.FontSize = 16;
		this.btnFraction2.textColorNormal = GuiNewStyleBar.blueColor;
		this.btnFraction2.textColorHover = Color.get_white();
		this.btnFraction2.Alignment = 4;
		this.registerWindow.AddGuiElement(this.btnFraction2);
		this.btnChooseServer = new GuiButtonFixed();
		this.btnChooseServer.SetTexture("FrameworkGUI", "empty");
		this.btnChooseServer.boundries = new Rect(123f, 430f, 300f, 37f);
		this.btnChooseServer.Clicked = new Action<EventHandlerParam>(this, LoginScript.OnRegisterChooseServer);
		this.btnChooseServer.Caption = string.Empty;
		this.btnChooseServer.Alignment = 3;
		this.registerWindow.AddGuiElement(this.btnChooseServer);
		this.cbTerms = new GuiCheckbox()
		{
			X = 207f,
			Y = 483f,
			Selected = true,
			isActive = true
		};
		this.registerWindow.AddGuiElement(this.cbTerms);
		this.btnTerms = new GuiButton()
		{
			boundries = new Rect(235f, 485f, 150f, 18f),
			Caption = StaticData.Translate("key_login_register_terms"),
			FontSize = 14,
			textColorNormal = GuiNewStyleBar.blueColor,
			textColorHover = Color.get_white(),
			Alignment = 3,
			Clicked = new Action<EventHandlerParam>(this, LoginScript.OnRegisterTermsClicked)
		};
		this.registerWindow.AddGuiElement(this.btnTerms);
		this.btnRegister = new GuiButtonFixed();
		this.btnRegister.SetTexture("LoginGui", "btnLogin");
		this.btnRegister.X = 172f;
		this.btnRegister.Y = 522f;
		this.btnRegister.Clicked = new Action<EventHandlerParam>(this, LoginScript.OnRegisterPlayClicked);
		this.btnRegister.Caption = StaticData.Translate("key_login_register_play_now").ToUpper();
		this.btnRegister.FontSize = 18;
		this.btnRegister.textColorNormal = Color.get_black();
		this.btnRegister.textColorHover = Color.get_black();
		this.btnRegister.Alignment = 4;
		this.btnRegister.clickEventOnUp = true;
		this.btnRegister.isMuted = true;
		this.registerWindow.AddGuiElement(this.btnRegister);
	}

	private void DrawVersionWindows()
	{
		if (this.isInTransition)
		{
			return;
		}
		bool flag = 164 < LoginScript.serverVersion;
		this.versionWindow.Clear();
		this.versionWindow.SetBackgroundTexture("LoginGui", "frameUpdate");
		this.versionWindow.PutToHorizontalCenter();
		this.SlideInWindow(this.versionWindow);
		this.bottomWindow.StartMoveBy(0f, (float)Screen.get_height() + this.bottomWindow.boundries.get_height(), 0.6f, false);
		if (flag)
		{
			GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
			guiButtonFixed.SetTexture("LoginGui", "btnUpdate");
			guiButtonFixed.X = 28f;
			guiButtonFixed.Y = 139f;
			guiButtonFixed.Caption = StaticData.Translate("key_login_new_version_update");
			guiButtonFixed.FontSize = 16;
			guiButtonFixed.Alignment = 4;
			guiButtonFixed.textColorNormal = Color.get_black();
			guiButtonFixed.textColorHover = Color.get_black();
			guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, LoginScript.OnUpdateClicked);
			this.versionWindow.AddGuiElement(guiButtonFixed);
		}
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(this.versionWindow.boundries.get_width() / 2f - 150f, 55f, 300f, 40f),
			text = StaticData.Translate((!flag ? "key_login_new_version_maintenance" : "key_login_new_version_message")),
			FontSize = 18,
			TextColor = GuiNewStyleBar.blueColor,
			Font = GuiLabel.FontBold,
			Alignment = 4,
			WordWrap = true
		};
		this.versionWindow.AddGuiElement(guiLabel);
	}

	private void EndHammerInAction()
	{
		this.slideWindow.zOrder = 230;
		this.slideWindow = null;
		this.gui.UpdateWindowsCollection();
		this.DrawBottomButtons();
		this.isInTransition = false;
	}

	private void EndHammerOutAction()
	{
		this.slideOutWindow.isHidden = true;
		this.slideOutWindow.preDrawHandler = null;
		this.slideOutWindow = null;
		this.DrawBottomButtons();
		this.isInTransition = false;
	}

	private GuiWindow GetActiveWindow(GuiWindow windowToCome)
	{
		if (windowToCome.handler != this.playNowWindow.handler && !this.playNowWindow.isHidden)
		{
			return this.playNowWindow;
		}
		if (windowToCome.handler != this.loginWindow.handler && !this.loginWindow.isHidden)
		{
			return this.loginWindow;
		}
		if (windowToCome.handler != this.serversWindows.handler && !this.serversWindows.isHidden)
		{
			return this.serversWindows;
		}
		if (windowToCome.handler != this.registerWindow.handler && !this.registerWindow.isHidden)
		{
			return this.registerWindow;
		}
		if (windowToCome.handler == this.versionWindow.handler || this.versionWindow.isHidden)
		{
			return null;
		}
		return this.versionWindow;
	}

	[DebuggerHidden]
	private IEnumerator GetServers(string username, string password)
	{
		LoginScript.<GetServers>c__IteratorB variable = null;
		return variable;
	}

	private void LanguageEndHammerAction()
	{
		if (this.txtLanguageDropDown != null)
		{
			this.txtLanguageDropDown.SetTexture("FrameworkGUI", "BtnQuestArrowDownNml");
		}
		this.languageWindow.isHidden = true;
		this.LangScroller.Claer();
		this.languageWindow.RemoveGuiElement(this.LangScroller);
		this.LangScroller = null;
		this.languageWindow.Clear();
	}

	private void LanguageScrollerPreDraw(object p)
	{
		float _height = this.languageWindow.boundries.get_height() / this.animationTime * 2.3f;
		if (this.languageWindow.boundries.get_y() < this.txtCurrentLanguage.Y + 45f)
		{
			ref Rect rectPointer = ref this.languageWindow.boundries;
			rectPointer.set_y(rectPointer.get_y() + Time.get_deltaTime() * _height);
		}
		else
		{
			this.languageWindow.boundries.set_y(this.txtCurrentLanguage.Y + 45f);
			this.languageWindow.preDrawHandler = null;
			this.languageWindow.zOrder = 249;
			this.gui.UpdateWindowsCollection();
			if (this.txtLanguageDropDown != null)
			{
				this.txtLanguageDropDown.SetTexture("FrameworkGUI", "BtnQuestArrowUpNml");
			}
		}
	}

	private void Login()
	{
		// 
		// Current member / type: System.Void LoginScript::Login()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void Login()
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

	private void LoginWithHash(string player_id, string timestamp, string hash, bool is_playNow = false)
	{
		// 
		// Current member / type: System.Void LoginScript::LoginWithHash(System.String,System.String,System.String,System.Boolean)
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

	private void LoginZorderUpdate()
	{
		this.topWindow.zOrder = 50;
		this.bottomWindow.zOrder = 50;
		this.gui.UpdateWindowsCollection();
	}

	private void OnChooseLanguage(EventHandlerParam prm)
	{
		this.languageWindow.isHidden = true;
		this.LangScroller.Claer();
		this.languageWindow.RemoveGuiElement(this.LangScroller);
		this.LangScroller = null;
		this.languageWindow.Clear();
		playWebGame.DefaultLanguage = prm.customData.ToString();
		playWebGame.RunSmallLangPackLoader();
		this.mainWindow.Clear();
		this.loginWindow.Clear();
		this.registerWindow.Clear();
		this.serversWindows.Clear();
		this.errorWindow.Clear();
		this.playNowWindow.Clear();
		this.versionWindow.Clear();
		this.languageWindow.Clear();
		this.topWindow.Clear();
		this.bottomWindow.Clear();
		this.DrawMainWindow();
		this.DrawLoginWindow();
	}

	private void OnChooseServerLogin(EventHandlerParam prm)
	{
		playWebGame.SetServer((int)prm.customData);
		PlayerPrefs.SetInt("AutoLogin", 0);
		this.Login();
		if (!this.isSlidingGuiOut)
		{
			this.DrawLoginWindow();
		}
	}

	private void OnChooseServerRegistration(EventHandlerParam prm)
	{
		this.registrationServer = playWebGame.GameServers[(int)prm.customData];
		if (this.chooseServerState == 1)
		{
			base.StartCoroutine(this.RegisterAsGuest());
		}
		else if (this.chooseServerState != 3)
		{
			string str = this.tbUsername.text;
			string str1 = this.tbPassword.text;
			string str2 = this.tbEmail.text;
			this.DrawRegisterWindow();
			this.btnChooseServer.Caption = this.registrationServer.server_name;
			this.registerWindow.zOrder = 230;
			this.registerWindow.isHidden = false;
			this.tbUsername.text = str;
			this.tbPassword.text = str1;
			this.tbEmail.text = str2;
		}
		else
		{
			PlayerPrefs.SetInt("AutoLogin", 0);
			base.StartCoroutine(this.RegisterWithFacebook());
		}
	}

	private void OnContinentSwitch(EventHandlerParam prm)
	{
		this.continent = (string)prm.customData;
		PlayerPrefs.SetString("Continent", this.continent);
		this.DrawChooseServer();
	}

	private void OnDrawChooseLanguage(EventHandlerParam prm)
	{
		if (!this.languageWindow.isHidden)
		{
			this.languageWindow.zOrder = 100;
			this.gui.UpdateWindowsCollection();
			this.languageWindow.timeHammerFx = 0.4f;
			this.languageWindow.StartHammerEffect(this.languageWindow.boundries.get_x(), -220f);
			this.languageWindow.fxEnded = new Action(this, LoginScript.LanguageEndHammerAction);
		}
		else
		{
			this.DrawLanguageChoose();
		}
	}

	private void OnDrawLoginClicked(EventHandlerParam prm)
	{
		this.DrawLoginWindow();
		this.DrawBottomButtons();
	}

	private void OnDrawPlayNowClicked(EventHandlerParam prm)
	{
		this.DrawPlayNowWindow();
		this.DrawBottomButtons();
	}

	private void OnErrorOkClicked(EventHandlerParam prm)
	{
		this.errorWindow.zOrder = 140;
		this.errorWindow.timeHammerFx = 0.6f;
		this.errorWindow.amplitudeHammerShake = 10f;
		this.errorWindow.moveToShakeRatio = 0.6f;
		this.errorWindow.StartHammerEffect(this.errorWindow.boundries.get_x(), (float)Screen.get_height());
		this.errorWindow.fxEnded = new Action(this, () => this.errorWindow.isHidden = true);
		this.errorWindow.isHidden = false;
		AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "menu_OpenClose");
		AudioManager.PlayGUISound(fromStaticSet);
	}

	private void OnFacebookClicked(EventHandlerParam prm)
	{
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

	private void OnForgottenPasswordClicked(object prm)
	{
		if (!playWebGame.isWebPlayer)
		{
			Application.OpenURL(playWebGame.URL_FORGOTTEN_PASSWORD);
		}
		else
		{
			Application.ExternalEval(string.Format("window.open('{0}','_blank')", playWebGame.URL_FORGOTTEN_PASSWORD));
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
			Debug.Log("DrawChooseServers");
			this.chooseServerState = 3;
			base.StartCoroutine(this.GetServers(string.Empty, string.Empty));
		}
		else
		{
			Debug.Log("CallFbLogin");
			this.CallFBLogin();
		}
	}

	private void OnLoginClicked(object prm)
	{
		this.username = this.tbUsername.text;
		this.password = this.tbPassword.text;
		this.message = string.Empty;
		if (!this.ValidateLogin())
		{
			Debug.Log("NOT valid login");
			return;
		}
		base.StartCoroutine(this.GetServers(this.username, this.password));
	}

	private void OnPlayNowClicked(EventHandlerParam prm)
	{
		if (!(PlayerPrefs.GetString("PlayNow_hash") != string.Empty) || PlayerPrefs.GetString("PlayNow_hash") == null)
		{
			this.chooseServerState = 1;
			base.StartCoroutine(this.GetServers(string.Empty, string.Empty));
		}
		else
		{
			playWebGame.LOGIN_SERVER_IP = PlayerPrefs.GetString("PlayNow_LoginServer");
			playWebGame.ASSET_URL = PlayerPrefs.GetString("PlayNow_LoginAssets");
			playWebGame.GAME_TYPE = PlayerPrefs.GetString("PlayNow_LoginGameType");
			Debug.Log(string.Concat("LoginServer: ", playWebGame.LOGIN_SERVER_IP));
			Debug.Log(string.Concat("AssetServer: ", playWebGame.ASSET_URL));
			Debug.Log(string.Concat("GameType: ", playWebGame.GAME_TYPE));
			PlayerPrefs.SetInt("AutoLogin", 1);
			this.LoginWithHash(PlayerPrefs.GetString("PlayNow_player_id"), PlayerPrefs.GetString("PlayNow_timestamp"), PlayerPrefs.GetString("PlayNow_hash"), false);
		}
	}

	private void OnRegisterChooseFraction(EventHandlerParam prm)
	{
		if ((int)prm.customData != 2)
		{
			this.btnFraction1.isEnabled = false;
			this.btnFraction2.isEnabled = true;
		}
		else
		{
			this.btnFraction1.isEnabled = true;
			this.btnFraction2.isEnabled = false;
		}
	}

	private void OnRegisterChooseServer(EventHandlerParam prm)
	{
		this.DrawChooseServer();
	}

	private void OnRegisterClicked(object prm)
	{
		this.DrawRegisterWindow();
		base.StartCoroutine(this.GetServers(string.Empty, string.Empty));
	}

	private void OnRegisterPlayClicked(EventHandlerParam prm)
	{
		this.btnRegister.isEnabled = false;
		if (!this.ValidateRegister())
		{
			this.btnRegister.isEnabled = true;
			return;
		}
		PlayerPrefs.SetInt("AutoLogin", 0);
		base.StartCoroutine(this.Register());
	}

	private void OnRegisterTermsClicked(EventHandlerParam prm)
	{
		Application.OpenURL("http://xs-software.com/company.php?go=terms&l=en");
	}

	private void OnUpdateClicked(EventHandlerParam prm)
	{
	}

	[DebuggerHidden]
	private IEnumerator Register()
	{
		LoginScript.<Register>c__IteratorC variable = null;
		return variable;
	}

	[DebuggerHidden]
	private IEnumerator RegisterAsGuest()
	{
		LoginScript.<RegisterAsGuest>c__IteratorD variable = null;
		return variable;
	}

	[DebuggerHidden]
	private IEnumerator RegisterWithFacebook()
	{
		LoginScript.<RegisterWithFacebook>c__IteratorE variable = null;
		return variable;
	}

	private void SlideInWindow(GuiWindow window)
	{
		this.isInTransition = true;
		this.slideWindow = window;
		this.slideWindow.zOrder = 40;
		this.slideWindow.boundries.set_y(-this.slideWindow.boundries.get_height());
		this.slideWindow.isHidden = false;
		this.animationSpeed = this.slideWindow.boundries.get_height() / this.animationTime * 1.3f;
		if (this.slideWindow.handler == this.playNowWindow.handler || this.slideWindow.handler == this.loginWindow.handler)
		{
			this.targetCoordinate = 40f;
		}
		if (this.slideWindow.handler == this.serversWindows.handler)
		{
			this.targetCoordinate = ((float)Screen.get_height() - this.serversWindows.boundries.get_height()) / 2f;
		}
		if (this.slideWindow.handler == this.registerWindow.handler)
		{
			this.targetCoordinate = 30f;
		}
		if (this.slideWindow.handler == this.versionWindow.handler)
		{
			this.targetCoordinate = ((float)Screen.get_height() - this.versionWindow.boundries.get_height()) / 2f;
		}
		AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "menu_OpenClose");
		AudioManager.PlayGUISound(fromStaticSet);
		this.slideWindow.timeHammerFx = 0.6f;
		this.slideWindow.amplitudeHammerShake = 10f;
		this.slideWindow.moveToShakeRatio = 0.6f;
		this.slideWindow.StartHammerEffect(this.slideWindow.boundries.get_x(), this.targetCoordinate);
		this.slideWindow.fxEnded = new Action(this, LoginScript.EndHammerInAction);
		this.slideOutWindow = this.GetActiveWindow(this.slideWindow);
		if (this.slideOutWindow != null)
		{
			this.slideOutWindow.timeHammerFx = 0.6f;
			this.slideOutWindow.StartHammerEffect(this.slideOutWindow.boundries.get_x(), (float)(Screen.get_height() + 10));
			this.slideOutWindow.fxEnded = new Action(this, LoginScript.EndHammerOutAction);
		}
		this.slideWindow.zOrder = 230;
		this.gui.UpdateWindowsCollection();
	}

	private void SlideOutGui(Action callback)
	{
		LoginScript.<SlideOutGui>c__AnonStorey35 variable = null;
		AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "menu_OpenClose");
		AudioManager.PlayGUISound(fromStaticSet);
		this.isSlidingGuiOut = true;
		if (!this.bottomWindow.isHidden)
		{
			this.bottomWindow.timeHammerFx = 0.5f;
			this.bottomWindow.StartHammerEffect(this.bottomWindow.boundries.get_x(), (float)Screen.get_height() + this.bottomWindow.boundries.get_height());
		}
		if (!this.topWindow.isHidden)
		{
			this.topWindow.timeHammerFx = 0.5f;
			this.topWindow.StartHammerEffect(this.topWindow.boundries.get_x(), -this.topWindow.boundries.get_height());
		}
		if (!this.serversWindows.isHidden)
		{
			this.serversWindows.timeHammerFx = 0.5f;
			this.serversWindows.StartHammerEffect(this.serversWindows.boundries.get_x(), -this.serversWindows.boundries.get_height());
		}
		if (!this.loginWindow.isHidden)
		{
			this.loginWindow.timeHammerFx = 0.5f;
			this.loginWindow.StartHammerEffect(this.loginWindow.boundries.get_x(), -this.loginWindow.boundries.get_height());
		}
		if (!this.registerWindow.isHidden)
		{
			this.registerWindow.timeHammerFx = 0.5f;
			this.registerWindow.StartHammerEffect(this.registerWindow.boundries.get_x(), -this.registerWindow.boundries.get_height());
		}
		if (!this.errorWindow.isHidden)
		{
			this.errorWindow.timeHammerFx = 0.5f;
			this.errorWindow.StartHammerEffect(this.errorWindow.boundries.get_x(), -this.errorWindow.boundries.get_height());
		}
		if (!this.versionWindow.isHidden)
		{
			this.versionWindow.timeHammerFx = 0.5f;
			this.versionWindow.StartHammerEffect(this.versionWindow.boundries.get_x(), -this.versionWindow.boundries.get_height());
		}
		if (!this.playNowWindow.isHidden)
		{
			this.playNowWindow.timeHammerFx = 0.5f;
			this.playNowWindow.StartHammerEffect(this.playNowWindow.boundries.get_x(), -this.playNowWindow.boundries.get_height());
		}
		if (!this.languageWindow.isHidden)
		{
			this.languageWindow.timeHammerFx = 0.5f;
			this.languageWindow.StartHammerEffect(this.languageWindow.boundries.get_x(), -this.languageWindow.boundries.get_height());
		}
		this.bottomWindow.fxEnded = new Action(variable, () => {
			this.<>f__this.isSlidingGuiOut = false;
			this.<>f__this.bottomWindow.isHidden = true;
			this.<>f__this.topWindow.isHidden = true;
			this.<>f__this.serversWindows.isHidden = true;
			this.<>f__this.loginWindow.isHidden = true;
			this.<>f__this.registerWindow.isHidden = true;
			this.<>f__this.versionWindow.isHidden = true;
			this.<>f__this.playNowWindow.isHidden = true;
			this.<>f__this.languageWindow.isHidden = true;
			if (this.callback != null)
			{
				this.callback.Invoke();
			}
		});
	}

	private void Start()
	{
		playWebGame.ResetLoadProgress();
		NetworkScript.party = null;
		playWebGame.udp = null;
		playWebGame.authorization = null;
		this.username = PlayerPrefs.GetString("User Name");
		this.password = PlayerPrefs.GetString("Password");
		this.gui = GameObject.Find("GlobalObject").GetComponent<GuiFramework>();
		if (!PlayerPrefs.HasKey("Continent"))
		{
			this.continent = "europe";
		}
		else
		{
			this.continent = PlayerPrefs.GetString("Continent");
		}
		this.mainWindow = new GuiWindow();
		this.mainWindow.boundries.set_x(0f);
		this.mainWindow.boundries.set_y(0f);
		this.mainWindow.isHidden = false;
		this.mainWindow.zOrder = 100;
		this.gui.AddWindow(this.mainWindow);
		this.loginWindow = new GuiWindow();
		this.loginWindow.boundries.set_x(0f);
		this.loginWindow.boundries.set_y(0f);
		this.loginWindow.isHidden = true;
		this.loginWindow.zOrder = 210;
		this.gui.AddWindow(this.loginWindow);
		this.registerWindow = new GuiWindow();
		this.registerWindow.boundries.set_x(0f);
		this.registerWindow.boundries.set_y(0f);
		this.registerWindow.isHidden = true;
		this.registerWindow.zOrder = 220;
		this.gui.AddWindow(this.registerWindow);
		this.serversWindows = new GuiWindow();
		this.serversWindows.boundries.set_x(0f);
		this.serversWindows.boundries.set_y(0f);
		this.serversWindows.isHidden = true;
		this.serversWindows.zOrder = 230;
		this.gui.AddWindow(this.serversWindows);
		this.errorWindow = new GuiWindow();
		this.errorWindow.boundries.set_x(0f);
		this.errorWindow.boundries.set_y(0f);
		this.errorWindow.isHidden = true;
		this.errorWindow.zOrder = 240;
		this.gui.AddWindow(this.errorWindow);
		this.playNowWindow = new GuiWindow();
		this.playNowWindow.boundries.set_x(0f);
		this.playNowWindow.boundries.set_y(0f);
		this.playNowWindow.isHidden = true;
		this.playNowWindow.zOrder = 210;
		this.gui.AddWindow(this.playNowWindow);
		this.versionWindow = new GuiWindow();
		this.versionWindow.boundries.set_x(0f);
		this.versionWindow.boundries.set_y(0f);
		this.versionWindow.isHidden = true;
		this.versionWindow.zOrder = 230;
		this.gui.AddWindow(this.versionWindow);
		this.languageWindow = new GuiWindow();
		this.languageWindow.boundries.set_x(0f);
		this.languageWindow.boundries.set_y(0f);
		this.languageWindow.isHidden = true;
		this.languageWindow.zOrder = 240;
		this.gui.AddWindow(this.languageWindow);
		this.topWindow = new GuiWindow();
		this.topWindow.boundries.set_x(0f);
		this.topWindow.boundries.set_y(0f);
		this.topWindow.isHidden = true;
		this.topWindow.zOrder = 240;
		this.gui.AddWindow(this.topWindow);
		this.bottomWindow = new GuiWindow();
		this.bottomWindow.boundries.set_x(0f);
		this.bottomWindow.boundries.set_y(0f);
		this.bottomWindow.isHidden = true;
		this.bottomWindow.zOrder = 250;
		this.gui.AddWindow(this.bottomWindow);
		this.DrawMainWindow();
		this.DrawLoginWindow();
	}

	private void Update()
	{
		if (playWebGame.authorization != null)
		{
			playWebGame.udp.ReceiveAsyncMessage();
		}
		StaticData.now = DateTime.get_Now();
		playWebGame.UpdateBundleLoad();
		this.UpdateBackgroundImagePosition();
	}

	private void UpdateBackgroundImagePosition()
	{
		this.mainWindow.boundries.set_width((float)Screen.get_width());
		this.mainWindow.boundries.set_height((float)Screen.get_height());
		this.txBackground.X = (float)(Screen.get_width() / 2) - this.txBackground.boundries.get_width() / 2f;
		this.txBackground.Y = 0f;
		this.txBackgroundBase.X = 0f;
		this.txBackgroundBase.Y = 0f;
		this.txBackgroundBase.boundries.set_width((float)Screen.get_width());
		this.txBackgroundBase.boundries.set_height((float)Screen.get_height());
		if (!this.topWindow.isHidden && !this.topWindow.IsHammerEffectActive)
		{
			this.topWindow.boundries = new Rect(((float)Screen.get_width() - this.txtTopFrame.boundries.get_width()) / 2f, 0f, this.txtTopFrame.boundries.get_width(), this.txtTopFrame.boundries.get_height());
		}
		if (!this.bottomWindow.isHidden && !this.bottomWindow.IsHammerEffectActive && this.bottomWindow.preDrawHandler == null)
		{
			this.bottomWindow.boundries = new Rect(((float)Screen.get_width() - this.txtBottomFrame.boundries.get_width()) / 2f, (float)Screen.get_height() - this.txtBottomFrame.boundries.get_height(), this.txtBottomFrame.boundries.get_width(), this.txtBottomFrame.boundries.get_height());
		}
		if (!this.loginWindow.isHidden && this.slideWindow == null && this.slideOutWindow == null && !this.loginWindow.IsHammerEffectActive)
		{
			this.loginWindow.boundries = new Rect(((float)Screen.get_width() - this.txtLoginFrame.boundries.get_width()) / 2f, 40f, this.txtLoginFrame.boundries.get_width(), this.txtLoginFrame.boundries.get_height());
			this.lblErrRegisterGeneral.text = this.message;
		}
		if (!this.playNowWindow.isHidden && this.slideWindow == null && this.slideOutWindow == null && !this.playNowWindow.IsHammerEffectActive)
		{
			this.playNowWindow.boundries = new Rect(((float)Screen.get_width() - this.txtPlayNowFrame.boundries.get_width()) / 2f, 40f, this.txtPlayNowFrame.boundries.get_width(), this.txtPlayNowFrame.boundries.get_height());
		}
		if (!this.registerWindow.isHidden && this.slideWindow == null && this.slideOutWindow == null && !this.registerWindow.IsHammerEffectActive)
		{
			this.registerWindow.boundries = new Rect(((float)Screen.get_width() - this.txtRegFrame.boundries.get_width()) / 2f, 30f, this.txtRegFrame.boundries.get_width(), this.txtRegFrame.boundries.get_height());
		}
		if (!this.serversWindows.isHidden && this.slideWindow == null && this.slideOutWindow == null && !this.serversWindows.IsHammerEffectActive)
		{
			this.serversWindows.PutToCenter();
		}
		if (!this.errorWindow.isHidden && !this.errorWindow.IsHammerEffectActive)
		{
			this.errorWindow.PutToCenter();
		}
		if (!this.versionWindow.isHidden)
		{
			this.versionWindow.PutToCenter();
		}
	}

	private bool ValidateLogin()
	{
		if (this.username == null || this.username.get_Length() < 1)
		{
			this.message = StaticData.Translate("key_login_msg_enter_username1");
			return false;
		}
		if (this.username.get_Length() > 30)
		{
			this.message = StaticData.Translate("key_login_msg_enter_username2");
			return false;
		}
		if (this.password == null || this.password.get_Length() < 1)
		{
			this.message = StaticData.Translate("key_login_msg_enter_pass1");
			return false;
		}
		if (this.password.get_Length() <= 30)
		{
			return true;
		}
		this.message = StaticData.Translate("key_login_msg_enter_pass2");
		return false;
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
		if (this.tbEmail.text.Trim().get_Length() < 7 || this.tbEmail.text.Trim().get_Length() > 100)
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