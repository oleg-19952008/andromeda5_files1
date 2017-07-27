using Facebook;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using TransferableObjects;
using UnityEngine;

public class ProfileScreen : GuiWindow
{
	public static string playerUserName;

	private PlayerProfile selectedPlayer;

	public static byte selectedTabIndex;

	private GuiTexture mainTabTexture;

	private GuiTexture playerFraction;

	private GuiTexture playerAvatarBorder;

	private GuiTexture playerAvatar;

	private GuiTexture onlineStatus;

	private GuiTexture fbIcon;

	private GuiButton tabBtnProfile;

	private GuiButton tabBtnAchievements;

	private GuiButton tabBtnFriends;

	private GuiButton tabBtnBlacklist;

	private GuiButton tabBtnAppearance;

	private GuiButtonFixed btnParty;

	private GuiButtonFixed btnChat;

	private GuiButtonFixed btnAddToFriends;

	private GuiButtonFixed btnAddToBlacklist;

	private GuiButtonFixed btnFriends;

	private GuiButtonFixed btnBlacklist;

	private GuiButtonResizeable btnFb;

	private GuiButtonResizeable btnAddToList;

	public GuiButtonResizeable btnChangeNickname;

	private GuiButtonResizeable btnSaveNewAvatar;

	private GuiNewStyleBar barAccessLevel;

	private GuiNewStyleBar barQuests;

	private GuiNewStyleBar barAchievements;

	private GuiLabel playerName;

	private GuiLabel rankPositionVal;

	private GuiLabel pvpTitleValue;

	private GuiLabel honorValue;

	private GuiLabel playtimeValue;

	private GuiLabel accessLevelProgress;

	private GuiLabel questsProgress;

	private GuiLabel achievementsProgress;

	private GuiLabel alienKillsCnt;

	private GuiLabel playerKillsCnt;

	private GuiLabel addingToListResponseLbl;

	public GuiLabel lblChangeNicknameError;

	public GuiLabel lblChangeFactionError;

	private GuiLabel lblSelectedAvatar;

	private GuiTextBox newEntryUserName;

	public GuiTextBox tbNewNickname;

	private GuiScrollingContainer achievementScroller;

	private GuiScrollingContainer firendListScroller;

	private GuiScrollingContainer blackListScroller;

	private GuiScrollingContainer awardsListScroller;

	private List<GuiElement> forDelete = new List<GuiElement>();

	private List<GuiElement> leftSideElement = new List<GuiElement>();

	private List<AvatarItem> avatars;

	private GuiDialog dlgConfirmCustomAvatar;

	private int currentAvatarIndex = -1;

	private int newAvatarIndex = -1;

	private GuiButtonFixed currentAvatarButton;

	private bool isRussianServer;

	private float achievementsAll = 115f;

	private bool isInit;

	private bool CanChangeFaction
	{
		get
		{
			int num = 0;
			int num1 = 0;
			IEnumerator<KeyValuePair<byte, byte>> enumerator = NetworkScript.player.factionGalaxyOwnership.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<byte, byte> current = enumerator.get_Current();
					if (current.get_Value() != 1)
					{
						if (current.get_Value() != 2)
						{
							continue;
						}
						num1++;
					}
					else
					{
						num++;
					}
				}
			}
			finally
			{
				if (enumerator == null)
				{
				}
				enumerator.Dispose();
			}
			if (num < 4 && num1 < 4)
			{
				return true;
			}
			return false;
		}
	}

	static ProfileScreen()
	{
		ProfileScreen.playerUserName = string.Empty;
	}

	public ProfileScreen()
	{
	}

	private void AddToBlacklist(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void ProfileScreen::AddToBlacklist(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void AddToBlacklist(EventHandlerParam)
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

	private void AddToFriedns(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void ProfileScreen::AddToFriedns(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void AddToFriedns(EventHandlerParam)
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

	private void Callback(FBResult result)
	{
		if (!FB.IsLoggedIn)
		{
			Debug.Log("User cancelled login");
		}
		else
		{
			Debug.Log(FB.UserId);
			this.OpenFBFriendSelector();
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

	private bool CanInviteToParty(PlayerProfile info)
	{
		ProfileScreen.<CanInviteToParty>c__AnonStorey59 variable = null;
		if (!info.isOnline || info.isInParty || info.userName == NetworkScript.player.playerBelongings.playerName || NetworkScript.player.galaxyId >= 2000 || info.galaxyId == 1000 || info.galaxyId >= 2000 || info.fractionId != NetworkScript.player.vessel.fractionId || NetworkScript.partyInvitees.get_Count() >= 3 || Enumerable.FirstOrDefault<PartyInvite>(Enumerable.Where<PartyInvite>(NetworkScript.partyInvitees.get_Values(), new Func<PartyInvite, bool>(variable, (PartyInvite i) => i.name == this.info.userName))) != null)
		{
			return false;
		}
		if (NetworkScript.party != null && NetworkScript.party.members != null)
		{
			if (NetworkScript.party.members.get_Count() > 0)
			{
				if (NetworkScript.party.members.get_Item(0).playerId != NetworkScript.player.playId)
				{
					return false;
				}
				if (Enumerable.FirstOrDefault<PartyMemberClientSide>(Enumerable.Where<PartyMemberClientSide>(NetworkScript.party.members, new Func<PartyMemberClientSide, bool>(variable, (PartyMemberClientSide w) => w.playerName == this.info.userName))) != null)
				{
					return false;
				}
			}
			if (NetworkScript.party.members.get_Count() >= 4)
			{
				return false;
			}
		}
		return true;
	}

	private void ClaimPendingAward(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void ProfileScreen::ClaimPendingAward(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void ClaimPendingAward(EventHandlerParam)
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

	public new void Clear()
	{
		if (this.achievementScroller != null)
		{
			this.achievementScroller.Claer();
			base.RemoveClippingBoundaries(this.achievementScroller.scrollerId);
		}
		if (this.firendListScroller != null)
		{
			this.firendListScroller.Claer();
			base.RemoveClippingBoundaries(this.firendListScroller.scrollerId);
		}
		if (this.blackListScroller != null)
		{
			this.blackListScroller.Claer();
			base.RemoveClippingBoundaries(this.blackListScroller.scrollerId);
		}
		if (this.awardsListScroller != null)
		{
			this.awardsListScroller.Claer();
			base.RemoveClippingBoundaries(this.awardsListScroller.scrollerId);
			this.awardsListScroller = null;
		}
		foreach (GuiElement guiElement in this.forDelete)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.forDelete.Clear();
	}

	public void ClearLeftSide()
	{
		foreach (GuiElement guiElement in this.leftSideElement)
		{
			base.RemoveGuiElement(guiElement);
		}
		this.leftSideElement.Clear();
	}

	public override void Create()
	{
		// 
		// Current member / type: System.Void ProfileScreen::Create()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void Create()
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

	private void CreateLeftSide()
	{
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("NewGUI", "pvp_ranking_left");
		guiTexture.X = 24f;
		guiTexture.Y = 26f;
		base.AddGuiElement(guiTexture);
		this.leftSideElement.Add(guiTexture);
		this.playerFraction = new GuiTexture();
		this.playerFraction.SetTexture("FrameworkGUI", "empty");
		this.playerFraction.X = 30f;
		this.playerFraction.Y = 47f;
		base.AddGuiElement(this.playerFraction);
		this.leftSideElement.Add(this.playerFraction);
		this.playerName = new GuiLabel()
		{
			boundries = new Rect(30f, 44f, 174f, 24f),
			text = string.Empty,
			FontSize = 12,
			Font = GuiLabel.FontBold,
			Alignment = 3
		};
		base.AddGuiElement(this.playerName);
		this.leftSideElement.Add(this.playerName);
		this.playerAvatarBorder = new GuiTexture();
		this.playerAvatarBorder.SetTexture("NewGUI", "pvp_avatar");
		this.playerAvatarBorder.X = 42f;
		this.playerAvatarBorder.Y = 88f;
		base.AddGuiElement(this.playerAvatarBorder);
		this.leftSideElement.Add(this.playerAvatarBorder);
		this.playerAvatar = new GuiTexture()
		{
			boundries = new Rect(43f, 89f, 100f, 100f)
		};
		this.playerAvatar.SetTextureKeepSize("FrameworkGUI", "unknown");
		base.AddGuiElement(this.playerAvatar);
		this.leftSideElement.Add(this.playerAvatar);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("NewGUI", "avatar_spacers");
		guiTexture1.X = 143f;
		guiTexture1.Y = 119f;
		base.AddGuiElement(guiTexture1);
		this.leftSideElement.Add(guiTexture1);
		this.onlineStatus = new GuiTexture()
		{
			boundries = new Rect(153f, 91f, 26f, 18f)
		};
		this.onlineStatus.SetTexture("NewGUI", "onlineStatusUnknown");
		base.AddGuiElement(this.onlineStatus);
		this.leftSideElement.Add(this.onlineStatus);
		this.btnParty = new GuiButtonFixed();
		this.btnParty.SetTexture("NewGUI", "button_party");
		this.btnParty.X = 156f;
		this.btnParty.Y = 128f;
		this.btnParty.Caption = string.Empty;
		this.btnParty.isEnabled = false;
		base.AddGuiElement(this.btnParty);
		this.leftSideElement.Add(this.btnParty);
		this.btnChat = new GuiButtonFixed();
		this.btnChat.SetTexture("NewGUI", "button_chat");
		this.btnChat.X = 158f;
		this.btnChat.Y = 163f;
		this.btnChat.Caption = string.Empty;
		this.btnChat.isEnabled = false;
		base.AddGuiElement(this.btnChat);
		this.leftSideElement.Add(this.btnChat);
		this.btnFriends = new GuiButtonFixed();
		this.btnFriends.SetTexture("NewGUI", "addToFriends");
		this.btnFriends.X = 56f;
		this.btnFriends.Y = 205f;
		this.btnFriends.Caption = string.Empty;
		this.btnFriends.isEnabled = false;
		base.AddGuiElement(this.btnFriends);
		this.leftSideElement.Add(this.btnFriends);
		this.btnBlacklist = new GuiButtonFixed();
		this.btnBlacklist.SetTexture("NewGUI", "addToBlacklist");
		this.btnBlacklist.X = 106f;
		this.btnBlacklist.Y = 205f;
		this.btnBlacklist.Caption = string.Empty;
		this.btnBlacklist.isEnabled = false;
		base.AddGuiElement(this.btnBlacklist);
		this.leftSideElement.Add(this.btnBlacklist);
		if (playWebGame.GAME_TYPE != "ru")
		{
			this.btnFb = new GuiButtonResizeable();
			this.btnFb.boundries.set_x(39f);
			this.btnFb.boundries.set_y(400f);
			this.btnFb.boundries.set_width(156f);
			this.btnFb.Caption = string.Empty;
			this.btnFb.FontSize = 12;
			this.btnFb.Alignment = 3;
			this.btnFb.MarginRight = 10;
			this.btnFb.SetBlueTexture();
			this.btnFb.isEnabled = false;
			base.AddGuiElement(this.btnFb);
			this.leftSideElement.Add(this.btnFb);
			this.fbIcon = new GuiTexture()
			{
				boundries = new Rect(52f, 408f, 14f, 30f)
			};
			this.fbIcon.SetTextureKeepSize("FrameworkGUI", "empty");
			base.AddGuiElement(this.fbIcon);
			this.leftSideElement.Add(this.fbIcon);
		}
		for (int i = 0; i < 4; i++)
		{
			GuiTexture rect = new GuiTexture();
			rect.SetTexture("NewGUI", "pvp_ranking_spacer");
			rect.boundries = new Rect(41f, (float)(290 + i * 30), 152f, 1f);
			base.AddGuiElement(rect);
			this.leftSideElement.Add(rect);
			GuiLabel guiLabel = new GuiLabel()
			{
				boundries = new Rect(41f, (float)(266 + i * 30), 152f, 20f),
				Alignment = 3,
				text = string.Empty,
				FontSize = 12,
				TextColor = GuiNewStyleBar.blueColor
			};
			base.AddGuiElement(guiLabel);
			this.leftSideElement.Add(guiLabel);
			switch (i)
			{
				case 0:
				{
					guiLabel.text = StaticData.Translate("key_profile_screen_rank_pos");
					break;
				}
				case 1:
				{
					guiLabel.text = StaticData.Translate("key_profile_screen_pvp_title");
					break;
				}
				case 2:
				{
					guiLabel.text = StaticData.Translate("key_profile_screen_honor");
					break;
				}
				case 3:
				{
					guiLabel.text = StaticData.Translate("key_profile_screen_playtime");
					break;
				}
			}
		}
		this.rankPositionVal = new GuiLabel()
		{
			boundries = new Rect(41f, 266f, 152f, 20f),
			Alignment = 5,
			text = string.Empty,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(this.rankPositionVal);
		this.leftSideElement.Add(this.rankPositionVal);
		this.pvpTitleValue = new GuiLabel()
		{
			boundries = new Rect(41f, 296f, 152f, 20f),
			Alignment = 5,
			text = string.Empty,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(this.pvpTitleValue);
		this.leftSideElement.Add(this.pvpTitleValue);
		this.honorValue = new GuiLabel()
		{
			boundries = new Rect(41f, 326f, 152f, 20f),
			Alignment = 5,
			text = string.Empty,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(this.honorValue);
		this.leftSideElement.Add(this.honorValue);
		this.playtimeValue = new GuiLabel()
		{
			boundries = new Rect(41f, 356f, 152f, 20f),
			Alignment = 5,
			text = string.Empty,
			FontSize = 12,
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(this.playtimeValue);
		this.leftSideElement.Add(this.playtimeValue);
	}

	private void CreateProfileTab()
	{
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(230f, 110f, 150f, 20f),
			text = StaticData.Translate("key_profile_screen_single_player"),
			FontSize = 15,
			Font = GuiLabel.FontBold,
			Alignment = 3
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(230f, 150f, 125f, 20f),
			text = StaticData.Translate("key_profile_screen_access_level"),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 5
		};
		base.AddGuiElement(guiLabel1);
		this.forDelete.Add(guiLabel1);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(230f, 180f, 125f, 20f),
			text = StaticData.Translate("key_profile_screen_quests"),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 5
		};
		base.AddGuiElement(guiLabel2);
		this.forDelete.Add(guiLabel2);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(230f, 210f, 125f, 20f),
			text = StaticData.Translate("key_profile_screen_achievements"),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 5
		};
		base.AddGuiElement(guiLabel3);
		this.forDelete.Add(guiLabel3);
		this.barAccessLevel = new GuiNewStyleBar();
		this.barAccessLevel.SetCustumSizeBlueBar(140);
		this.barAccessLevel.current = 0f;
		this.barAccessLevel.boundries.set_x(368f);
		this.barAccessLevel.boundries.set_y(153f);
		base.AddGuiElement(this.barAccessLevel);
		this.forDelete.Add(this.barAccessLevel);
		this.barQuests = new GuiNewStyleBar();
		this.barQuests.SetCustumSizeBlueBar(140);
		this.barQuests.current = 0f;
		this.barQuests.boundries.set_x(368f);
		this.barQuests.boundries.set_y(183f);
		base.AddGuiElement(this.barQuests);
		this.forDelete.Add(this.barQuests);
		this.barAchievements = new GuiNewStyleBar();
		this.barAchievements.SetCustumSizeBlueBar(140);
		this.barAchievements.current = 0f;
		this.barAchievements.boundries.set_x(368f);
		this.barAchievements.boundries.set_y(213f);
		base.AddGuiElement(this.barAchievements);
		this.forDelete.Add(this.barAchievements);
		this.accessLevelProgress = new GuiLabel()
		{
			boundries = new Rect(520f, 150f, 60f, 20f),
			text = string.Empty,
			FontSize = 16,
			Font = GuiLabel.FontBold,
			Alignment = 3
		};
		base.AddGuiElement(this.accessLevelProgress);
		this.forDelete.Add(this.accessLevelProgress);
		this.questsProgress = new GuiLabel()
		{
			boundries = new Rect(520f, 180f, 60f, 20f),
			text = string.Empty,
			FontSize = 16,
			Font = GuiLabel.FontBold,
			Alignment = 3
		};
		base.AddGuiElement(this.questsProgress);
		this.forDelete.Add(this.questsProgress);
		this.achievementsProgress = new GuiLabel()
		{
			boundries = new Rect(520f, 210f, 60f, 20f),
			text = string.Empty,
			FontSize = 16,
			Font = GuiLabel.FontBold,
			Alignment = 3
		};
		base.AddGuiElement(this.achievementsProgress);
		this.forDelete.Add(this.achievementsProgress);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("NewGUI", "stats_kills_frame");
		guiTexture.X = 582f;
		guiTexture.Y = 152f;
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			boundries = new Rect(625f, 155f, 90f, 30f),
			text = StaticData.Translate("key_profile_screen_pve_kills"),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 4
		};
		base.AddGuiElement(guiLabel4);
		this.forDelete.Add(guiLabel4);
		GuiLabel guiLabel5 = new GuiLabel()
		{
			boundries = new Rect(765f, 155f, 90f, 30f),
			text = StaticData.Translate("key_profile_screen_plr_kills"),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			Alignment = 4
		};
		base.AddGuiElement(guiLabel5);
		this.forDelete.Add(guiLabel5);
		this.alienKillsCnt = new GuiLabel()
		{
			boundries = new Rect(625f, 180f, 90f, 26f),
			text = string.Empty,
			FontSize = 18,
			Font = GuiLabel.FontBold,
			Alignment = 4
		};
		base.AddGuiElement(this.alienKillsCnt);
		this.forDelete.Add(this.alienKillsCnt);
		this.playerKillsCnt = new GuiLabel()
		{
			boundries = new Rect(765f, 180f, 90f, 26f),
			text = string.Empty,
			FontSize = 18,
			Font = GuiLabel.FontBold,
			Alignment = 4
		};
		base.AddGuiElement(this.playerKillsCnt);
		this.forDelete.Add(this.playerKillsCnt);
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("NewGUI", "pvp_ranking_spacer");
		rect.boundries = new Rect(230f, 258f, 640f, 1f);
		base.AddGuiElement(rect);
		this.forDelete.Add(rect);
		this.PopulatePendingAwards();
	}

	private void DrawPlayerList(List<PlayerProfile> listOfPlayers, GuiScrollingContainer scroller, bool isFriends)
	{
		if (listOfPlayers.get_Count() == 0)
		{
			GuiLabel guiLabel = new GuiLabel()
			{
				boundries = new Rect(0f, 0f, 640f, 350f),
				Alignment = 4,
				text = (!isFriends ? StaticData.Translate("key_profile_screen_empty_black_list") : StaticData.Translate("key_profile_screen_empty_friends_list")),
				FontSize = 16,
				TextColor = GuiNewStyleBar.blueColor,
				Font = GuiLabel.FontBold
			};
			scroller.AddContent(guiLabel);
		}
		int num = 0;
		foreach (PlayerProfile listOfPlayer in listOfPlayers)
		{
			GuiTexture guiTexture = new GuiTexture();
			guiTexture.SetTexture("NewGUI", "playerListFrame");
			guiTexture.X = (float)(10 + num % 2 * 310);
			guiTexture.Y = (float)(10 + num / 2 * 70);
			scroller.AddContent(guiTexture);
			GuiTexture guiTexture1 = new GuiTexture()
			{
				boundries = new Rect(guiTexture.X + 3f, guiTexture.Y + 3f, 49f, 49f)
			};
			Texture2D avatarOrStartIt = playWebGame.GetAvatarOrStartIt(listOfPlayer.avatarUrl, new Action<AvatarJob>(this, ProfileScreen.SetAvatar), guiTexture1);
			if (avatarOrStartIt == null)
			{
				guiTexture1.SetTextureKeepSize("FrameworkGUI", "unknown");
				avatarOrStartIt = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "unknown");
			}
			guiTexture1.SetTextureKeepSize(avatarOrStartIt);
			scroller.AddContent(guiTexture1);
			GuiTexture x = new GuiTexture();
			x.SetTexture("FrameworkGUI", string.Format("fraction{0}Icon", listOfPlayer.fractionId));
			x.X = guiTexture.X + 59f;
			x.Y = guiTexture.Y + 3f;
			scroller.AddContent(x);
			GuiLabel guiLabel1 = new GuiLabel()
			{
				boundries = new Rect(87f + guiTexture.X, 3f + guiTexture.Y, 175f, 20f),
				text = string.Format("{0} ({1})", listOfPlayer.userName, listOfPlayer.level),
				FontSize = 12,
				Font = GuiLabel.FontBold
			};
			scroller.AddContent(guiLabel1);
			GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
			guiButtonFixed.SetTexture("NewGUI", "option_ViewProfile");
			guiButtonFixed.Caption = string.Empty;
			guiButtonFixed.X = guiTexture.X + 61f;
			guiButtonFixed.Y = guiTexture.Y + 33f;
			EventHandlerParam eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_profile_screen_tooltips_view_profile"),
				customData2 = guiButtonFixed
			};
			guiButtonFixed.tooltipWindowParam = eventHandlerParam;
			guiButtonFixed.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			eventHandlerParam = new EventHandlerParam()
			{
				customData = listOfPlayer.userName
			};
			guiButtonFixed.eventHandlerParam = eventHandlerParam;
			guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.ViewPlayerProfile);
			scroller.AddContent(guiButtonFixed);
			GuiButtonFixed empty = new GuiButtonFixed();
			empty.SetTexture("NewGUI", "person_close");
			empty.Caption = string.Empty;
			empty.X = guiTexture.X + 263f;
			empty.Y = guiTexture.Y + 6f;
			empty.Clicked = null;
			scroller.AddContent(empty);
			if (!isFriends)
			{
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_profile_screen_tooltips_remove_blacklist"),
					customData2 = empty
				};
				empty.tooltipWindowParam = eventHandlerParam;
				eventHandlerParam = new EventHandlerParam()
				{
					customData = listOfPlayer.userName
				};
				empty.eventHandlerParam = eventHandlerParam;
				empty.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.RemoveFromBlacklist);
			}
			else
			{
				GuiTexture drawTooltipWindow = new GuiTexture()
				{
					X = guiTexture.X + 125f,
					Y = guiTexture.Y + 33f
				};
				if (!listOfPlayer.isOnline)
				{
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_profile_screen_tooltips_offline"),
						customData2 = drawTooltipWindow
					};
					drawTooltipWindow.tooltipWindowParam = eventHandlerParam;
					drawTooltipWindow.SetTexture("NewGUI", "option_StatusOffline");
				}
				else
				{
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_profile_screen_tooltips_online"),
						customData2 = drawTooltipWindow
					};
					drawTooltipWindow.tooltipWindowParam = eventHandlerParam;
					drawTooltipWindow.SetTexture("NewGUI", "option_StatusOnline");
				}
				drawTooltipWindow.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				scroller.AddContent(drawTooltipWindow);
				GuiButtonFixed y = new GuiButtonFixed();
				y.SetTexture("NewGUI", "startChat");
				y.Caption = string.Empty;
				y.X = guiTexture.X + 95f;
				y.Y = guiTexture.Y + 33f;
				eventHandlerParam = new EventHandlerParam()
				{
					customData = listOfPlayer.userName
				};
				y.eventHandlerParam = eventHandlerParam;
				y.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.StartChatWith);
				scroller.AddContent(y);
				if (!listOfPlayer.isOnline)
				{
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_profile_screen_tooltips_no_chat"),
						customData2 = y
					};
					y.tooltipWindowParam = eventHandlerParam;
					y.isEnabled = false;
				}
				else
				{
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_profile_screen_tooltips_chat"),
						customData2 = y
					};
					y.tooltipWindowParam = eventHandlerParam;
					y.isEnabled = true;
				}
				y.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				GuiButtonFixed action = new GuiButtonFixed();
				action.SetTexture("NewGUI", "btnInviteToParty");
				action.X = guiTexture.X + 155f;
				action.Y = guiTexture.Y + 33f;
				action.Caption = string.Empty;
				action.isEnabled = false;
				scroller.AddContent(action);
				if (!this.CanInviteToParty(listOfPlayer))
				{
					action.isEnabled = false;
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_profile_screen_tooltips_no_invite"),
						customData2 = action
					};
					action.tooltipWindowParam = eventHandlerParam;
				}
				else
				{
					action.isEnabled = true;
					eventHandlerParam = new EventHandlerParam()
					{
						customData = listOfPlayer.userName
					};
					action.eventHandlerParam = eventHandlerParam;
					action.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.InviteToParty);
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_profile_screen_tooltips_invite"),
						customData2 = action
					};
					action.tooltipWindowParam = eventHandlerParam;
				}
				action.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				GuiButtonFixed rect = new GuiButtonFixed();
				rect.SetTexture("MainScreenWindow", "sendGift");
				rect.X = guiTexture.X + 185f;
				rect.Y = guiTexture.Y + 33f;
				rect.boundries = new Rect(guiTexture.X + 185f, guiTexture.Y + 33f, 22f, 20f);
				rect.Caption = string.Empty;
				rect.isEnabled = false;
				scroller.AddContent(rect);
				if (listOfPlayer.level < 10)
				{
					rect.isEnabled = false;
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_profile_screen_tooltips_send_gift_low_level"),
						customData2 = rect
					};
					rect.tooltipWindowParam = eventHandlerParam;
				}
				else
				{
					rect.isEnabled = true;
					eventHandlerParam = new EventHandlerParam()
					{
						customData = listOfPlayer.userName,
						customData2 = listOfPlayer.level
					};
					rect.eventHandlerParam = eventHandlerParam;
					rect.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.SendGift);
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_profile_screen_tooltips_send_gift"),
						customData2 = rect
					};
					rect.tooltipWindowParam = eventHandlerParam;
				}
				rect.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_profile_screen_tooltips_remove_friend"),
					customData2 = empty
				};
				empty.tooltipWindowParam = eventHandlerParam;
				eventHandlerParam = new EventHandlerParam()
				{
					customData = listOfPlayer.userName
				};
				empty.eventHandlerParam = eventHandlerParam;
				empty.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.RemoveFromFriends);
			}
			empty.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			num++;
		}
	}

	private void DrawPlayerListIpad(List<PlayerProfile> listOfPlayers, GuiScrollingContainer scroller, bool isFriends)
	{
		if (listOfPlayers.get_Count() == 0)
		{
			GuiLabel guiLabel = new GuiLabel()
			{
				boundries = new Rect(0f, 0f, 640f, 350f),
				Alignment = 4,
				text = (!isFriends ? StaticData.Translate("key_profile_screen_empty_black_list") : StaticData.Translate("key_profile_screen_empty_friends_list")),
				FontSize = 16,
				TextColor = GuiNewStyleBar.blueColor,
				Font = GuiLabel.FontBold
			};
			scroller.AddContent(guiLabel);
		}
		int num = 0;
		foreach (PlayerProfile listOfPlayer in listOfPlayers)
		{
			GuiTexture guiTexture = new GuiTexture();
			guiTexture.SetTexture("iPad/ProfileScreen", (!isFriends ? "playerListFrameBL_iPad" : "playerListFrame_iPad"));
			guiTexture.X = (float)(10 + num % 2 * 310);
			guiTexture.Y = (float)(10 + num / 2 * 70);
			scroller.AddContent(guiTexture);
			GuiTexture guiTexture1 = new GuiTexture()
			{
				boundries = new Rect(guiTexture.X + 3f, guiTexture.Y + 3f, 49f, 49f)
			};
			Texture2D avatarOrStartIt = playWebGame.GetAvatarOrStartIt(listOfPlayer.avatarUrl, new Action<AvatarJob>(this, ProfileScreen.SetAvatar), guiTexture1);
			if (avatarOrStartIt == null)
			{
				guiTexture1.SetTextureKeepSize("FrameworkGUI", "unknown");
				avatarOrStartIt = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "unknown");
			}
			guiTexture1.SetTextureKeepSize(avatarOrStartIt);
			scroller.AddContent(guiTexture1);
			GuiTexture rect = new GuiTexture()
			{
				X = guiTexture.X + 3f,
				Y = guiTexture.Y + 40f
			};
			rect.SetTexture("NewGUI", "btnInfoNml");
			rect.boundries = new Rect(guiTexture.X + 3f, guiTexture.Y + 38f, 15f, 15f);
			scroller.AddContent(rect);
			GuiButton guiButton = new GuiButton()
			{
				boundries = guiTexture1.boundries,
				Caption = string.Empty
			};
			EventHandlerParam eventHandlerParam = new EventHandlerParam()
			{
				customData = listOfPlayer.userName
			};
			guiButton.eventHandlerParam = eventHandlerParam;
			guiButton.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.ViewPlayerProfile);
			scroller.AddContent(guiButton);
			GuiTexture x = new GuiTexture();
			x.SetTexture("FrameworkGUI", string.Format("fraction{0}Icon", listOfPlayer.fractionId));
			x.X = guiTexture.X + 59f;
			x.Y = guiTexture.Y + 3f;
			scroller.AddContent(x);
			GuiLabel guiLabel1 = new GuiLabel()
			{
				boundries = new Rect(87f + guiTexture.X, 3f + guiTexture.Y + 2f, 175f, 20f),
				text = string.Format("{0} ({1})", listOfPlayer.userName, listOfPlayer.level),
				FontSize = 12,
				Font = GuiLabel.FontBold
			};
			scroller.AddContent(guiLabel1);
			GuiButton action = new GuiButton()
			{
				boundries = new Rect(56f + guiTexture.X, guiTexture.Y - 5f, 200f, 30f),
				Caption = string.Empty,
				Alignment = 4
			};
			eventHandlerParam = new EventHandlerParam()
			{
				customData = listOfPlayer.userName
			};
			action.eventHandlerParam = eventHandlerParam;
			action.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.ViewPlayerProfile);
			scroller.AddContent(action);
			GuiTexture guiTexture2 = new GuiTexture()
			{
				X = guiTexture.X + 262f,
				Y = guiTexture.Y + 19f
			};
			guiTexture2.SetTexture("NewGUI", "person_closeNml");
			scroller.AddContent(guiTexture2);
			GuiButton drawTooltipWindow = new GuiButton()
			{
				Caption = string.Empty,
				Alignment = 4,
				boundries = new Rect(guiTexture.X + 260f, guiTexture.Y, 20f, 50f),
				Clicked = null
			};
			scroller.AddContent(drawTooltipWindow);
			GuiTexture drawTooltipWindow1 = new GuiTexture()
			{
				X = guiTexture.X + 58f,
				Y = guiTexture.Y + 31f
			};
			if (!listOfPlayer.isOnline)
			{
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_profile_screen_tooltips_offline"),
					customData2 = drawTooltipWindow1
				};
				drawTooltipWindow1.tooltipWindowParam = eventHandlerParam;
				drawTooltipWindow1.SetTexture("NewGUI", "option_StatusOffline");
			}
			else
			{
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_profile_screen_tooltips_online"),
					customData2 = drawTooltipWindow1
				};
				drawTooltipWindow1.tooltipWindowParam = eventHandlerParam;
				drawTooltipWindow1.SetTexture("NewGUI", "option_StatusOnline");
			}
			drawTooltipWindow1.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			scroller.AddContent(drawTooltipWindow1);
			if (!isFriends)
			{
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_profile_screen_tooltips_remove_blacklist"),
					customData2 = drawTooltipWindow
				};
				drawTooltipWindow.tooltipWindowParam = eventHandlerParam;
				eventHandlerParam = new EventHandlerParam()
				{
					customData = listOfPlayer.userName
				};
				drawTooltipWindow.eventHandlerParam = eventHandlerParam;
				drawTooltipWindow.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.RemoveFromBlacklist);
			}
			else
			{
				GuiTexture guiTexture3 = new GuiTexture()
				{
					X = guiTexture.X + 116f,
					Y = guiTexture.Y + 33f
				};
				GuiButton guiButton1 = new GuiButton()
				{
					Caption = string.Empty,
					Alignment = 4,
					boundries = new Rect(guiTexture.X + 86f, guiTexture.Y + 30f, 85f, 25f)
				};
				eventHandlerParam = new EventHandlerParam()
				{
					customData = listOfPlayer.userName
				};
				guiButton1.eventHandlerParam = eventHandlerParam;
				guiButton1.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.StartChatWith);
				if (!listOfPlayer.isOnline)
				{
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_profile_screen_tooltips_no_chat"),
						customData2 = guiButton1
					};
					guiButton1.tooltipWindowParam = eventHandlerParam;
					guiButton1.isEnabled = false;
					guiTexture3.SetTexture("NewGUI", "startChatDsb");
				}
				else
				{
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_profile_screen_tooltips_chat"),
						customData2 = guiButton1
					};
					guiButton1.tooltipWindowParam = eventHandlerParam;
					guiButton1.isEnabled = true;
					guiTexture3.SetTexture("NewGUI", "startChatNml");
				}
				guiButton1.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				scroller.AddContent(guiTexture3);
				scroller.AddContent(guiButton1);
				GuiTexture guiTexture4 = new GuiTexture()
				{
					X = guiTexture.X + 201f,
					Y = guiTexture.Y + 31f
				};
				GuiButton action1 = new GuiButton()
				{
					Caption = string.Empty,
					Alignment = 4,
					boundries = new Rect(guiTexture.X + 176f, guiTexture.Y + 30f, 78f, 25f)
				};
				eventHandlerParam = new EventHandlerParam()
				{
					customData = listOfPlayer.userName
				};
				action1.eventHandlerParam = eventHandlerParam;
				action1.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.StartChatWith);
				if (!this.CanInviteToParty(listOfPlayer))
				{
					action1.isEnabled = false;
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_profile_screen_tooltips_no_invite"),
						customData2 = action1
					};
					action1.tooltipWindowParam = eventHandlerParam;
					guiTexture4.SetTexture("NewGUI", "btnInviteToPartyDsb");
				}
				else
				{
					action1.isEnabled = true;
					eventHandlerParam = new EventHandlerParam()
					{
						customData = listOfPlayer.userName
					};
					action1.eventHandlerParam = eventHandlerParam;
					action1.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.InviteToParty);
					eventHandlerParam = new EventHandlerParam()
					{
						customData = StaticData.Translate("key_profile_screen_tooltips_invite"),
						customData2 = action1
					};
					action1.tooltipWindowParam = eventHandlerParam;
					guiTexture4.SetTexture("NewGUI", "btnInviteToPartyNml");
				}
				action1.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				scroller.AddContent(guiTexture4);
				scroller.AddContent(action1);
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_profile_screen_tooltips_remove_friend"),
					customData2 = drawTooltipWindow
				};
				drawTooltipWindow.tooltipWindowParam = eventHandlerParam;
				eventHandlerParam = new EventHandlerParam()
				{
					customData = listOfPlayer.userName
				};
				drawTooltipWindow.eventHandlerParam = eventHandlerParam;
				drawTooltipWindow.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.RemoveFromFriends);
			}
			drawTooltipWindow.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			num++;
		}
	}

	private void FriendSelectorCallBack(FBResult fbResult)
	{
		if (string.IsNullOrEmpty(fbResult.Error))
		{
			Debug.Log(string.Concat("Friends selector callback : ", fbResult.Text));
			NetworkScript.player.shipScript.StartCoroutine(this.SendFBFriendRequest(fbResult.Text));
		}
		else
		{
			Debug.Log(string.Concat("Friends selector callback.Error : ", fbResult.Error.ToString()));
		}
	}

	private void InviteToParty(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void ProfileScreen::InviteToParty(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void InviteToParty(EventHandlerParam)
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

	private bool IsEnteredTextOK(string input)
	{
		return (new Regex("^[^~`^<>!#$%&*()'?+|=:\\,\\;\\{\\}\\[\\]\\@\\\\\\/]+$")).IsMatch(input);
	}

	private void OnAchievementsClicked(object prm)
	{
		ProfileScreen.selectedTabIndex = 1;
		this.Clear();
		this.mainTabTexture.SetTexture("NewGUI", "SkillsTab1");
		this.achievementScroller = new GuiScrollingContainer(230f, 115f, 640f, 395f, 3, this);
		base.AddGuiElement(this.achievementScroller);
		this.forDelete.Add(this.achievementScroller);
		this.PopulateAchievementsTab(this.selectedPlayer);
	}

	private void OnAppearanceClicked(object prm)
	{
		ProfileScreen.selectedTabIndex = 4;
		this.Clear();
		this.mainTabTexture.SetTexture("NewGUI", "SkillsTab4");
		this.PopulateAvatarsList();
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(230f, 105f, 300f, 30f),
			Alignment = 3,
			text = StaticData.Translate("key_profile_appearance_avatar"),
			FontSize = 15,
			TextColor = Color.get_white(),
			Font = GuiLabel.FontBold
		};
		base.AddGuiElement(guiLabel);
		this.forDelete.Add(guiLabel);
		this.lblSelectedAvatar = new GuiLabel()
		{
			boundries = new Rect(guiLabel.boundries.get_x() + guiLabel.TextWidth + 10f, 105f, 300f, 30f),
			Alignment = 3,
			text = string.Empty,
			FontSize = 15,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor
		};
		base.AddGuiElement(this.lblSelectedAvatar);
		this.forDelete.Add(this.lblSelectedAvatar);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("NewGUI", "pvp_ranking_spacer");
		guiTexture.boundries = new Rect(230f, 137f, 640f, 1f);
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		GuiScrollingContainer guiScrollingContainer = new GuiScrollingContainer(230f, 150f, 645f, 204f, 1, this);
		guiScrollingContainer.SetArrowStep(104f);
		base.AddGuiElement(guiScrollingContainer);
		this.forDelete.Add(guiScrollingContainer);
		this.PopulateAvatarsScroller(guiScrollingContainer);
		GuiTexture rect = new GuiTexture();
		rect.SetTexture("NewGUI", "pvp_ranking_spacer");
		rect.boundries = new Rect(230f, 367f, 640f, 1f);
		base.AddGuiElement(rect);
		this.forDelete.Add(rect);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed()
		{
			Caption = string.Empty
		};
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_profile_appearance_nickname_changed_rules"),
			customData2 = guiButtonFixed
		};
		guiButtonFixed.tooltipWindowParam = eventHandlerParam;
		guiButtonFixed.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		guiButtonFixed.isHoverAware = true;
		guiButtonFixed.SetTexture("NewGUI", "btnInfo");
		guiButtonFixed.X = 230f;
		guiButtonFixed.Y = 385f;
		guiButtonFixed.boundries.set_width(20f);
		guiButtonFixed.boundries.set_height(20f);
		base.AddGuiElement(guiButtonFixed);
		this.forDelete.Add(guiButtonFixed);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(255f, 380f, 300f, 30f),
			Alignment = 3,
			text = StaticData.Translate("key_profile_appearance_change_nickname"),
			FontSize = 15,
			Font = GuiLabel.FontBold,
			TextColor = Color.get_white()
		};
		base.AddGuiElement(guiLabel1);
		this.forDelete.Add(guiLabel1);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("NewGUI", "pvp_ranking_spacer");
		guiTexture1.boundries = new Rect(230f, 415f, 310f, 1f);
		base.AddGuiElement(guiTexture1);
		this.forDelete.Add(guiTexture1);
		this.tbNewNickname = new GuiTextBox()
		{
			boundries = new Rect(232f, guiTexture1.boundries.get_y() + 10f, 300f, 30f),
			Alignment = 3,
			FontSize = 14,
			TextColor = Color.get_white(),
			text = NetworkScript.player.vessel.playerName,
			Validate = new Action(this, ProfileScreen.ValidateNewNickname)
		};
		base.AddGuiElement(this.tbNewNickname);
		this.forDelete.Add(this.tbNewNickname);
		this.btnChangeNickname = new GuiButtonResizeable();
		this.btnChangeNickname.boundries.set_x(232f);
		this.btnChangeNickname.boundries.set_y(this.tbNewNickname.boundries.get_y() + 45f);
		this.btnChangeNickname.boundries.set_width(300f);
		this.btnChangeNickname.Caption = StaticData.Translate("key_profile_appearance_change");
		this.btnChangeNickname.FontSize = 14;
		this.btnChangeNickname.Alignment = 5;
		this.btnChangeNickname.MarginRight = 20;
		this.btnChangeNickname.SetSmallOrangeTexture();
		this.btnChangeNickname.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.OnChangeNickname);
		this.btnChangeNickname.isEnabled = false;
		base.AddGuiElement(this.btnChangeNickname);
		this.forDelete.Add(this.btnChangeNickname);
		GuiTexture guiTexture2 = new GuiTexture()
		{
			boundries = new Rect(this.btnChangeNickname.X + 15f, this.btnChangeNickname.Y + 3f, 20f, 20f)
		};
		guiTexture2.SetTextureKeepSize("NewGUI", "icon_white_nova");
		base.AddGuiElement(guiTexture2);
		this.forDelete.Add(guiTexture2);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(guiTexture2.X + guiTexture2.boundries.get_width() + 5f, this.btnChangeNickname.boundries.get_y() - 2f, 300f, 30f),
			Alignment = 3,
			text = 1000.ToString("##,##0"),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = Color.get_white()
		};
		base.AddGuiElement(guiLabel2);
		this.forDelete.Add(guiLabel2);
		this.lblChangeNicknameError = new GuiLabel()
		{
			boundries = new Rect(guiLabel1.boundries.get_x() - 25f, this.btnChangeNickname.boundries.get_y() + 30f, 400f, 35f),
			Alignment = 3,
			FontSize = 12,
			TextColor = GuiNewStyleBar.redColor,
			text = string.Empty
		};
		base.AddGuiElement(this.lblChangeNicknameError);
		this.forDelete.Add(this.lblChangeNicknameError);
		GuiButtonFixed drawTooltipWindow = new GuiButtonFixed()
		{
			Caption = string.Empty
		};
		eventHandlerParam = new EventHandlerParam()
		{
			customData = StaticData.Translate("key_profile_appearance_change_faction_tooltip"),
			customData2 = drawTooltipWindow
		};
		drawTooltipWindow.tooltipWindowParam = eventHandlerParam;
		drawTooltipWindow.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		drawTooltipWindow.isHoverAware = true;
		drawTooltipWindow.SetTexture("NewGUI", "btnInfo");
		drawTooltipWindow.X = 570f;
		drawTooltipWindow.Y = 385f;
		drawTooltipWindow.boundries.set_width(20f);
		drawTooltipWindow.boundries.set_height(20f);
		base.AddGuiElement(drawTooltipWindow);
		this.forDelete.Add(drawTooltipWindow);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = new Rect(595f, 380f, 300f, 30f),
			Alignment = 3,
			text = StaticData.Translate("key_profile_appearance_change_faction"),
			FontSize = 15,
			Font = GuiLabel.FontBold,
			TextColor = Color.get_white()
		};
		base.AddGuiElement(guiLabel3);
		this.forDelete.Add(guiLabel3);
		GuiTexture rect1 = new GuiTexture();
		rect1.SetTexture("NewGUI", "pvp_ranking_spacer");
		rect1.boundries = new Rect(568f, 415f, 302f, 1f);
		base.AddGuiElement(rect1);
		this.forDelete.Add(rect1);
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.boundries.set_x(570f);
		guiButtonResizeable.boundries.set_y(this.btnChangeNickname.Y);
		guiButtonResizeable.boundries.set_width(300f);
		guiButtonResizeable.Caption = StaticData.Translate("key_profile_appearance_faction_switch");
		guiButtonResizeable.FontSize = 14;
		guiButtonResizeable.Alignment = 5;
		guiButtonResizeable.MarginRight = 20;
		guiButtonResizeable.SetSmallOrangeTexture();
		guiButtonResizeable.isEnabled = false;
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.OnChangeFaction);
		base.AddGuiElement(guiButtonResizeable);
		this.forDelete.Add(guiButtonResizeable);
		GuiTexture guiTexture3 = new GuiTexture()
		{
			boundries = new Rect(guiButtonResizeable.X + 15f, guiButtonResizeable.Y + 3f, 20f, 20f)
		};
		guiTexture3.SetTextureKeepSize("NewGUI", "icon_white_nova");
		base.AddGuiElement(guiTexture3);
		this.forDelete.Add(guiTexture3);
		GuiLabel str = new GuiLabel()
		{
			boundries = new Rect(guiTexture3.X + guiTexture3.boundries.get_width() + 5f, guiButtonResizeable.boundries.get_y() - 2f, 300f, 30f),
			Alignment = 3
		};
		int num = StaticData.ChangeFactionPriceInNova(NetworkScript.player.playerBelongings.playerLevel);
		str.text = num.ToString("##,##0");
		str.FontSize = 14;
		str.Font = GuiLabel.FontBold;
		str.TextColor = Color.get_white();
		base.AddGuiElement(str);
		this.forDelete.Add(str);
		GuiLabel guiLabel4 = new GuiLabel()
		{
			boundries = new Rect(guiButtonResizeable.boundries.get_x(), this.tbNewNickname.boundries.get_y(), 300f, 30f),
			Alignment = 3,
			text = StaticData.Translate("key_profile_appearance_faction_switch_to"),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = Color.get_white()
		};
		base.AddGuiElement(guiLabel4);
		this.forDelete.Add(guiLabel4);
		GuiTexture guiTexture4 = new GuiTexture()
		{
			boundries = new Rect(guiLabel4.boundries.get_x() + guiLabel4.TextWidth + 10f, guiLabel4.Y, 30f, 30f)
		};
		guiTexture4.SetTextureKeepSize("QuestTrackerAvatars", (NetworkScript.player.vessel.fractionId != 1 ? "vindexis" : "regia"));
		base.AddGuiElement(guiTexture4);
		this.forDelete.Add(guiTexture4);
		GuiLabel guiLabel5 = new GuiLabel()
		{
			boundries = new Rect(guiTexture4.X + guiTexture4.boundries.get_width() + 5f, guiTexture4.boundries.get_y() + 1f, 300f, 30f),
			Alignment = 3,
			text = StaticData.Translate((NetworkScript.player.vessel.fractionId != 1 ? "key_login_reg_fraction_one" : "key_login_register_fraction2")),
			FontSize = 14,
			TextColor = (NetworkScript.player.vessel.fractionId != 1 ? GuiNewStyleBar.purpleColor : Color.get_yellow())
		};
		base.AddGuiElement(guiLabel5);
		this.forDelete.Add(guiLabel5);
		this.lblChangeFactionError = new GuiLabel()
		{
			boundries = new Rect(guiLabel3.boundries.get_x() - 25f, this.btnChangeNickname.boundries.get_y() + 30f, 400f, 35f),
			Alignment = 3,
			FontSize = 12,
			TextColor = GuiNewStyleBar.redColor,
			text = string.Empty
		};
		base.AddGuiElement(this.lblChangeFactionError);
		this.forDelete.Add(this.lblChangeFactionError);
		if (!NetworkScript.player.vessel.galaxy.isPveMap || NetworkScript.player.vessel.galaxy.get_galaxyId() > 1000 && NetworkScript.player.vessel.galaxy.get_galaxyId() < 3000 || NetworkScript.player.vessel.galaxy.get_galaxyId() > 4000)
		{
			guiButtonResizeable.isEnabled = false;
			this.lblChangeFactionError.text = StaticData.Translate("key_appearance_faction_only_inpve");
		}
		else if (NetworkScript.player.playerBelongings.playerItems.get_Nova() >= (long)StaticData.ChangeFactionPriceInNova(NetworkScript.player.playerBelongings.playerLevel))
		{
			guiButtonResizeable.isEnabled = true;
		}
		if (NetworkScript.player.playerBelongings.councilRank != 0)
		{
			guiButtonResizeable.isEnabled = false;
			this.lblChangeFactionError.text = StaticData.Translate("key_appearance_faction_council");
		}
		if (!this.CanChangeFaction)
		{
			guiButtonResizeable.isEnabled = false;
			this.lblChangeFactionError.text = StaticData.Translate("key_appearance_faction_war_restriction");
		}
		if (NetworkScript.player.guild != null || NetworkScript.player.vessel.isInParty)
		{
			guiButtonResizeable.isEnabled = false;
			this.lblChangeFactionError.text = StaticData.Translate("key_appearance_faction_leave_party_guild");
		}
		if (NetworkScript.player.vessel.pvpState == 3 || NetworkScript.player.vessel.pvpState == 4 || NetworkScript.player.vessel.pvpState == 2)
		{
			guiButtonResizeable.isEnabled = false;
			this.lblChangeFactionError.text = StaticData.Translate("key_appearance_faction_only_inpve");
			this.btnChangeNickname.isEnabled = false;
			this.lblChangeNicknameError.text = StaticData.Translate("key_appearance_nickname_only_inpve");
		}
	}

	private void OnAvatarClicked(EventHandlerParam prm)
	{
		ProfileScreen.<OnAvatarClicked>c__AnonStorey58 variable = null;
		int num = (int)prm.customData;
		if (num != 0)
		{
			this.currentAvatarButton = (GuiButtonFixed)prm.customData2;
			this.newAvatarIndex = num;
			this.playerAvatar.SetTextureKeepSize("FixedAvatars", string.Format("FixedAvatar_{0}", num));
			this.lblSelectedAvatar.text = Enumerable.FirstOrDefault<AvatarItem>(Enumerable.Where<AvatarItem>(this.avatars, new Func<AvatarItem, bool>(variable, (AvatarItem p) => p.avatarIndex == this.clickedAvatarIndex))).avatarName;
			return;
		}
		if (this.currentAvatarIndex != 0 || this.newAvatarIndex == -1 || this.newAvatarIndex == 0)
		{
			this.ShowConfirmCustomAvatarDialog();
			this.lblSelectedAvatar.text = this.avatars.get_Item(num).avatarName;
		}
		else
		{
			Texture2D avatarOrStartIt = playWebGame.GetAvatarOrStartIt(NetworkScript.player.vessel.playerAvatarUrl, new Action<AvatarJob>(this, ProfileScreen.SetAvatar), this.playerAvatar);
			if (avatarOrStartIt == null)
			{
				this.playerAvatar.SetTextureKeepSize("FrameworkGUI", "unknown");
				avatarOrStartIt = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "unknown");
			}
			this.newAvatarIndex = num;
			this.lblSelectedAvatar.text = this.avatars.get_Item(num).avatarName;
			this.playerAvatar.SetTextureKeepSize(avatarOrStartIt);
			((GuiButtonFixed)prm.customData2).IsClicked = false;
		}
	}

	private void OnBlacklistClicked(object prm)
	{
		ProfileScreen.selectedTabIndex = 3;
		this.Clear();
		this.mainTabTexture.SetTexture("NewGUI", "SkillsTab3");
		playWebGame.udp.ExecuteCommand(173, null);
		this.blackListScroller = new GuiScrollingContainer(230f, 115f, 640f, 350f, 2, this);
		this.blackListScroller.SetArrowStep(70f);
		base.AddGuiElement(this.blackListScroller);
		this.forDelete.Add(this.blackListScroller);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("NewGUI", "pvp_ranking_spacer");
		guiTexture.boundries = new Rect(230f, 480f, 640f, 1f);
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		this.newEntryUserName = new GuiTextBox()
		{
			boundries = new Rect(230f, 490f, 200f, 30f),
			Alignment = 3,
			FontSize = 14,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.newEntryUserName);
		this.forDelete.Add(this.newEntryUserName);
		this.btnAddToList = new GuiButtonResizeable();
		this.btnAddToList.boundries.set_x(440f);
		this.btnAddToList.boundries.set_width(160f);
		this.btnAddToList.Caption = StaticData.Translate("key_profile_screen_add_black_list").ToUpper();
		this.btnAddToList.FontSize = 12;
		this.btnAddToList.SetSmallBlueTexture();
		GuiButtonResizeable guiButtonResizeable = this.btnAddToList;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = this.newEntryUserName.text,
			customData2 = true
		};
		guiButtonResizeable.eventHandlerParam = eventHandlerParam;
		this.btnAddToList.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.AddToBlacklist);
		this.btnAddToList.boundries.set_y(495f);
		this.btnAddToList.SetSmallBlueTexture();
		this.btnAddToList.Alignment = 1;
		this.btnAddToList.MarginTop = 5;
		base.AddGuiElement(this.btnAddToList);
		this.forDelete.Add(this.btnAddToList);
		this.addingToListResponseLbl = new GuiLabel()
		{
			boundries = new Rect(600f, 490f, 270f, 40f),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			text = string.Empty
		};
		base.AddGuiElement(this.addingToListResponseLbl);
		this.forDelete.Add(this.addingToListResponseLbl);
	}

	private void OnChangeFaction(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void ProfileScreen::OnChangeFaction(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnChangeFaction(EventHandlerParam)
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

	private void OnChangeNickname(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void ProfileScreen::OnChangeNickname(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnChangeNickname(EventHandlerParam)
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

	public override void OnClose()
	{
		// 
		// Current member / type: System.Void ProfileScreen::OnClose()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnClose()
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..( , Int32 , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:строка 78
		//    в ..( , Int32& , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:строка 33
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 57
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..(IfStatement ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 359
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 55
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

	private void OnConfirmCustomAvatarOptionClicked(object prm)
	{
		this.ignoreClickEvents = false;
		if ((int)(prm as EventHandlerParam).customData != 0)
		{
			this.dlgConfirmCustomAvatar.RemoveGUIItems();
			if (this.currentAvatarButton != null)
			{
				this.currentAvatarButton.IsClicked = true;
			}
		}
		else
		{
			Application.OpenURL(playWebGame.authorization.url_logout);
		}
	}

	private void OnFriendsClicked(object prm)
	{
		ProfileScreen.selectedTabIndex = 2;
		this.Clear();
		this.mainTabTexture.SetTexture("NewGUI", "SkillsTab2");
		playWebGame.udp.ExecuteCommand(172, null);
		this.firendListScroller = new GuiScrollingContainer(230f, 115f, 640f, 350f, 1, this);
		this.firendListScroller.SetArrowStep(70f);
		base.AddGuiElement(this.firendListScroller);
		this.forDelete.Add(this.firendListScroller);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("NewGUI", "pvp_ranking_spacer");
		guiTexture.boundries = new Rect(230f, 480f, 640f, 1f);
		base.AddGuiElement(guiTexture);
		this.forDelete.Add(guiTexture);
		this.newEntryUserName = new GuiTextBox()
		{
			boundries = new Rect(230f, 490f, 200f, 30f),
			Alignment = 3,
			FontSize = 14,
			TextColor = GuiNewStyleBar.blueColor
		};
		base.AddGuiElement(this.newEntryUserName);
		this.forDelete.Add(this.newEntryUserName);
		this.btnAddToList = new GuiButtonResizeable();
		this.btnAddToList.boundries.set_x(440f);
		this.btnAddToList.boundries.set_width(156f);
		this.btnAddToList.FontSize = 12;
		this.btnAddToList.Caption = StaticData.Translate("key_profile_screen_add_friend_list").ToUpper();
		GuiButtonResizeable guiButtonResizeable = this.btnAddToList;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = this.newEntryUserName.text,
			customData2 = true
		};
		guiButtonResizeable.eventHandlerParam = eventHandlerParam;
		this.btnAddToList.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.AddToFriedns);
		this.btnAddToList.boundries.set_y(495f);
		this.btnAddToList.SetSmallBlueTexture();
		this.btnAddToList.Alignment = 1;
		this.btnAddToList.MarginTop = 5;
		base.AddGuiElement(this.btnAddToList);
		this.forDelete.Add(this.btnAddToList);
		this.addingToListResponseLbl = new GuiLabel()
		{
			boundries = new Rect(600f, 490f, 270f, 40f),
			Alignment = 4,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			text = string.Empty
		};
		base.AddGuiElement(this.addingToListResponseLbl);
		this.forDelete.Add(this.addingToListResponseLbl);
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
			this.OpenFBFriendSelector();
		}
		else
		{
			Debug.Log("CallFbLogin");
			this.CallFBLogin();
		}
	}

	private void OnProfileClicked(object prm)
	{
		ProfileScreen.selectedTabIndex = 0;
		this.Clear();
		this.mainTabTexture.SetTexture("NewGUI", "SkillsTab0");
		this.CreateProfileTab();
		if (this.selectedPlayer != null)
		{
			this.PopulateLeftSide(this.selectedPlayer);
			this.PopulateProfileTab(this.selectedPlayer);
		}
	}

	private void OnReturnToMyProfileClick(object prm)
	{
		// 
		// Current member / type: System.Void ProfileScreen::OnReturnToMyProfileClick(System.Object)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnReturnToMyProfileClick(System.Object)
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

	private void OnShowFbFriendClick(object prm)
	{
		if (!playWebGame.isWebPlayer)
		{
			Application.OpenURL(playWebGame.authorization.url_fb);
		}
		else
		{
			Application.OpenURL(playWebGame.authorization.url_fb);
		}
	}

	private void OpenFBFriendSelector()
	{
		Debug.Log("Open FB Friend Selector.Send AppRequest");
		FacebookDelegate facebookDelegate = new FacebookDelegate(this.FriendSelectorCallBack);
		int? nullable = null;
		FB.AppRequest("Select Friend", null, string.Empty, null, nullable, string.Empty, string.Empty, facebookDelegate);
	}

	private void PopulateAchievementsTab(PlayerProfile info)
	{
		EventHandlerParam eventHandlerParam;
		if (info == null)
		{
			return;
		}
		this.achievementScroller.Claer();
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(10f, 0f, 400f, 20f),
			Font = GuiLabel.FontBold,
			FontSize = 16,
			text = StaticData.Translate("key_profile_screen_achievement_general")
		};
		this.achievementScroller.AddContent(guiLabel);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("NewGUI", "pvp_ranking_spacer");
		guiTexture.boundries = new Rect(10f, 25f, 588f, 1f);
		this.achievementScroller.AddContent(guiTexture);
		Achievement[] achievementArray = Achievement.allAchievement;
		if (ProfileScreen.<>f__am$cache39 == null)
		{
			ProfileScreen.<>f__am$cache39 = new Func<Achievement, bool>(null, (Achievement t) => t.type == 0);
		}
		List<Achievement> list = Enumerable.ToList<Achievement>(Enumerable.Where<Achievement>(achievementArray, ProfileScreen.<>f__am$cache39));
		for (int i = 0; i < Enumerable.Count<Achievement>(list); i++)
		{
			int num = info.achievements[list.get_Item(i).id - 1];
			string empty = string.Empty;
			if (num >= 1)
			{
				empty = (num != 5 ? string.Concat(new string[] { StaticData.Translate(list.get_Item(i).name), "\n", string.Format(StaticData.Translate("key_profile_screen_achievement_level"), num), "\n\n", StaticData.Translate("key_profile_screen_achievement_current_level"), "\n", string.Format(StaticData.Translate(list.get_Item(i).description), list.get_Item(i).levels[num - 1]), "\n", StaticData.Translate("key_profile_screen_achievement_next_level"), "\n", string.Format(StaticData.Translate(list.get_Item(i).description), list.get_Item(i).levels[num]) }) : string.Concat(new string[] { StaticData.Translate(list.get_Item(i).name), "\n", string.Format(StaticData.Translate("key_profile_screen_achievement_level"), num), "\n\n", StaticData.Translate("key_profile_screen_achievement_current_level"), "\n", string.Format(StaticData.Translate(list.get_Item(i).description), list.get_Item(i).levels[num - 1]) }));
			}
			else
			{
				empty = string.Concat(new string[] { StaticData.Translate(list.get_Item(i).name), "\n", StaticData.Translate("key_profile_screen_achievement_locked"), "\n\n", StaticData.Translate("key_profile_screen_achievement_next_level"), "\n", string.Format(StaticData.Translate(list.get_Item(i).description), list.get_Item(i).levels[0]) });
			}
			GuiTexture rect = new GuiTexture();
			rect.SetTexture("Achievement", string.Format("level{0}", num));
			rect.boundries = new Rect((float)(10 + i % 6 * 100), guiTexture.Y + 10f + (float)(i / 6 * 100), 86f, 86f);
			eventHandlerParam = new EventHandlerParam()
			{
				customData = empty,
				customData2 = rect
			};
			rect.tooltipWindowParam = eventHandlerParam;
			rect.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			this.achievementScroller.AddContent(rect);
			GuiTexture guiTexture1 = new GuiTexture();
			guiTexture1.SetTexture("Achievement", "Unknown");
			guiTexture1.boundries = new Rect(rect.X + 3f, rect.Y + 3f, 80f, 80f);
			this.achievementScroller.AddContent(guiTexture1);
			if (num != 0)
			{
				Achievements item = list.get_Item(i).id - 1;
				guiTexture1.SetTextureKeepSize("Achievement", (item.ToString() != "WarMaster" ? item.ToString() : "MeleeMaster"));
			}
			else
			{
				guiTexture1.SetTexture("Achievement", "Unknown");
			}
		}
		float single = (float)((Enumerable.Count<Achievement>(list) / 6 + Math.Min(Enumerable.Count<Achievement>(list) % 6, 1)) * 100 + 25);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(10f, single, 400f, 20f),
			Font = GuiLabel.FontBold,
			FontSize = 16,
			text = StaticData.Translate("key_profile_screen_achievement_nova")
		};
		this.achievementScroller.AddContent(guiLabel1);
		GuiTexture rect1 = new GuiTexture();
		rect1.SetTexture("NewGUI", "pvp_ranking_spacer");
		rect1.boundries = new Rect(10f, guiLabel1.Y + 25f, 588f, 1f);
		this.achievementScroller.AddContent(rect1);
		Achievement[] achievementArray1 = Achievement.allAchievement;
		if (ProfileScreen.<>f__am$cache3A == null)
		{
			ProfileScreen.<>f__am$cache3A = new Func<Achievement, bool>(null, (Achievement t) => t.type == 1);
		}
		List<Achievement> list1 = Enumerable.ToList<Achievement>(Enumerable.Where<Achievement>(achievementArray1, ProfileScreen.<>f__am$cache3A));
		for (int j = 0; j < Enumerable.Count<Achievement>(list1); j++)
		{
			int num1 = info.achievements[list1.get_Item(j).id - 1];
			string str = string.Empty;
			if (num1 >= 1)
			{
				str = (num1 != 5 ? string.Concat(new string[] { StaticData.Translate(list1.get_Item(j).name), "\n", string.Format(StaticData.Translate("key_profile_screen_achievement_level"), num1), "\n\n", StaticData.Translate("key_profile_screen_achievement_current_level"), "\n", string.Format(StaticData.Translate(list1.get_Item(j).description), list1.get_Item(j).levels[num1 - 1]), "\n", StaticData.Translate("key_profile_screen_achievement_next_level"), "\n", string.Format(StaticData.Translate(list1.get_Item(j).description), list1.get_Item(j).levels[num1]) }) : string.Concat(new string[] { StaticData.Translate(list1.get_Item(j).name), "\n", string.Format(StaticData.Translate("key_profile_screen_achievement_level"), num1), "\n\n", StaticData.Translate("key_profile_screen_achievement_current_level"), "\n", string.Format(StaticData.Translate(list1.get_Item(j).description), list1.get_Item(j).levels[num1 - 1]) }));
			}
			else
			{
				str = string.Concat(new string[] { StaticData.Translate(list1.get_Item(j).name), "\n", StaticData.Translate("key_profile_screen_achievement_locked"), "\n\n", StaticData.Translate("key_profile_screen_achievement_next_level"), "\n", string.Format(StaticData.Translate(list1.get_Item(j).description), list1.get_Item(j).levels[0]) });
			}
			GuiTexture drawTooltipWindow = new GuiTexture();
			drawTooltipWindow.SetTexture("Achievement", string.Format("level{0}", num1));
			drawTooltipWindow.boundries = new Rect((float)(10 + j % 6 * 100), rect1.Y + 10f + (float)(j / 6 * 100), 86f, 86f);
			eventHandlerParam = new EventHandlerParam()
			{
				customData = str,
				customData2 = drawTooltipWindow
			};
			drawTooltipWindow.tooltipWindowParam = eventHandlerParam;
			drawTooltipWindow.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			this.achievementScroller.AddContent(drawTooltipWindow);
			GuiTexture guiTexture2 = new GuiTexture();
			guiTexture2.SetTexture("Achievement", "Unknown");
			guiTexture2.boundries = new Rect(drawTooltipWindow.X + 3f, drawTooltipWindow.Y + 3f, 80f, 80f);
			this.achievementScroller.AddContent(guiTexture2);
			if (num1 != 0)
			{
				Achievements achievement = list1.get_Item(j).id - 1;
				guiTexture2.SetTextureKeepSize("Achievement", achievement.ToString());
			}
			else
			{
				guiTexture2.SetTexture("Achievement", "Unknown");
			}
		}
		single = single + (float)((Enumerable.Count<Achievement>(list1) / 6 + Math.Min(Enumerable.Count<Achievement>(list1) % 6, 1)) * 100 + 25);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(10f, single, 400f, 20f),
			Font = GuiLabel.FontBold,
			FontSize = 16,
			text = StaticData.Translate("key_profile_screen_achievement_pvp")
		};
		this.achievementScroller.AddContent(guiLabel2);
		GuiTexture rect2 = new GuiTexture();
		rect2.SetTexture("NewGUI", "pvp_ranking_spacer");
		rect2.boundries = new Rect(10f, guiLabel2.Y + 25f, 588f, 1f);
		this.achievementScroller.AddContent(rect2);
		Achievement[] achievementArray2 = Achievement.allAchievement;
		if (ProfileScreen.<>f__am$cache3B == null)
		{
			ProfileScreen.<>f__am$cache3B = new Func<Achievement, bool>(null, (Achievement t) => t.type == 2);
		}
		List<Achievement> list2 = Enumerable.ToList<Achievement>(Enumerable.Where<Achievement>(achievementArray2, ProfileScreen.<>f__am$cache3B));
		for (int k = 0; k < Enumerable.Count<Achievement>(list2); k++)
		{
			int num2 = info.achievements[list2.get_Item(k).id - 1];
			string empty1 = string.Empty;
			if (num2 >= 1)
			{
				empty1 = (num2 != 5 ? string.Concat(new string[] { StaticData.Translate(list2.get_Item(k).name), "\n", string.Format(StaticData.Translate("key_profile_screen_achievement_level"), num2), "\n\n", StaticData.Translate("key_profile_screen_achievement_current_level"), "\n", string.Format(StaticData.Translate(list2.get_Item(k).description), list2.get_Item(k).levels[num2 - 1]), "\n", StaticData.Translate("key_profile_screen_achievement_next_level"), "\n", string.Format(StaticData.Translate(list2.get_Item(k).description), list2.get_Item(k).levels[num2]) }) : string.Concat(new string[] { StaticData.Translate(list2.get_Item(k).name), "\n", string.Format(StaticData.Translate("key_profile_screen_achievement_level"), num2), "\n\n", StaticData.Translate("key_profile_screen_achievement_current_level"), "\n", string.Format(StaticData.Translate(list2.get_Item(k).description), list2.get_Item(k).levels[num2 - 1]) }));
			}
			else
			{
				empty1 = string.Concat(new string[] { StaticData.Translate(list2.get_Item(k).name), "\n", StaticData.Translate("key_profile_screen_achievement_locked"), "\n\n", StaticData.Translate("key_profile_screen_achievement_next_level"), "\n", string.Format(StaticData.Translate(list2.get_Item(k).description), list2.get_Item(k).levels[0]) });
			}
			GuiTexture drawTooltipWindow1 = new GuiTexture();
			drawTooltipWindow1.SetTexture("Achievement", string.Format("level{0}", num2));
			drawTooltipWindow1.boundries = new Rect((float)(10 + k % 6 * 100), rect2.Y + 10f + (float)(k / 6 * 100), 86f, 86f);
			eventHandlerParam = new EventHandlerParam()
			{
				customData = empty1,
				customData2 = drawTooltipWindow1
			};
			drawTooltipWindow1.tooltipWindowParam = eventHandlerParam;
			drawTooltipWindow1.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			this.achievementScroller.AddContent(drawTooltipWindow1);
			GuiTexture guiTexture3 = new GuiTexture();
			guiTexture3.SetTexture("Achievement", "Unknown");
			guiTexture3.boundries = new Rect(drawTooltipWindow1.X + 3f, drawTooltipWindow1.Y + 3f, 80f, 80f);
			this.achievementScroller.AddContent(guiTexture3);
			if (num2 != 0)
			{
				Achievements item1 = list2.get_Item(k).id - 1;
				guiTexture3.SetTextureKeepSize("Achievement", item1.ToString());
			}
			else
			{
				guiTexture3.SetTexture("Achievement", "Unknown");
			}
		}
	}

	public void PopulateAddToListResponse(byte returnCode, string userName)
	{
		if (ProfileScreen.selectedTabIndex == 0 || ProfileScreen.selectedTabIndex == 1)
		{
			return;
		}
		if (this.newEntryUserName != null)
		{
			this.newEntryUserName.text = string.Empty;
		}
		switch (returnCode)
		{
			case 0:
			{
				this.addingToListResponseLbl.TextColor = GuiNewStyleBar.greenColor;
				this.addingToListResponseLbl.text = string.Format(StaticData.Translate("key_profile_screen_return_code_ok"), userName);
				break;
			}
			case 1:
			{
				this.addingToListResponseLbl.TextColor = GuiNewStyleBar.redColor;
				this.addingToListResponseLbl.text = string.Format(StaticData.Translate("key_profile_screen_return_code_not_found"), userName);
				break;
			}
			case 2:
			{
				this.addingToListResponseLbl.TextColor = GuiNewStyleBar.redColor;
				this.addingToListResponseLbl.text = string.Format(StaticData.Translate("key_profile_screen_return_code_already"), userName);
				break;
			}
		}
	}

	private void PopulateAvatarsList()
	{
		AvatarItem avatarItem;
		this.isRussianServer = playWebGame.GAME_TYPE == "ru";
		if (this.avatars != null)
		{
			return;
		}
		this.avatars = new List<AvatarItem>();
		if (!this.isRussianServer)
		{
			List<AvatarItem> list = this.avatars;
			avatarItem = new AvatarItem()
			{
				avatarName = StaticData.Translate("key_profile_appearance_custom_avatar"),
				minAccessLevel = 1
			};
			list.Add(avatarItem);
		}
		List<AvatarItem> list1 = this.avatars;
		avatarItem = new AvatarItem()
		{
			avatarName = StaticData.Translate("key_guest_player"),
			minAccessLevel = 1,
			avatarIndex = 1
		};
		list1.Add(avatarItem);
		List<AvatarItem> list2 = this.avatars;
		avatarItem = new AvatarItem()
		{
			avatarName = StaticData.Translate("key_NPC_name_Vladimir"),
			minAccessLevel = 2,
			avatarIndex = 2
		};
		list2.Add(avatarItem);
		List<AvatarItem> list3 = this.avatars;
		avatarItem = new AvatarItem()
		{
			avatarName = StaticData.Translate("key_pve_taura"),
			minAccessLevel = 2,
			avatarIndex = 3
		};
		list3.Add(avatarItem);
		List<AvatarItem> list4 = this.avatars;
		avatarItem = new AvatarItem()
		{
			avatarName = StaticData.Translate("key_NPC_name_Stalker"),
			minAccessLevel = 4,
			avatarIndex = 4
		};
		list4.Add(avatarItem);
		List<AvatarItem> list5 = this.avatars;
		avatarItem = new AvatarItem()
		{
			avatarName = StaticData.Translate("key_NPC_name_CaribbeanJoe"),
			minAccessLevel = 6,
			avatarIndex = 5
		};
		list5.Add(avatarItem);
		List<AvatarItem> list6 = this.avatars;
		avatarItem = new AvatarItem()
		{
			avatarName = StaticData.Translate("key_NPC_name_James"),
			minAccessLevel = 9,
			avatarIndex = 6
		};
		list6.Add(avatarItem);
		List<AvatarItem> list7 = this.avatars;
		avatarItem = new AvatarItem()
		{
			avatarName = StaticData.Translate("key_NPC_name_Patton"),
			minAccessLevel = 10,
			avatarIndex = 7
		};
		list7.Add(avatarItem);
		List<AvatarItem> list8 = this.avatars;
		avatarItem = new AvatarItem()
		{
			avatarName = StaticData.Translate("key_NPC_name_Xena"),
			minAccessLevel = 10,
			avatarIndex = 8
		};
		list8.Add(avatarItem);
		List<AvatarItem> list9 = this.avatars;
		avatarItem = new AvatarItem()
		{
			avatarName = StaticData.Translate("key_pve_volkr"),
			minAccessLevel = 10,
			avatarIndex = 9
		};
		list9.Add(avatarItem);
		List<AvatarItem> list10 = this.avatars;
		avatarItem = new AvatarItem()
		{
			avatarName = StaticData.Translate("key_pve_golgotha"),
			minAccessLevel = 11,
			avatarIndex = 10
		};
		list10.Add(avatarItem);
		List<AvatarItem> list11 = this.avatars;
		avatarItem = new AvatarItem()
		{
			avatarName = StaticData.Translate("key_pve_aria"),
			minAccessLevel = 11,
			avatarIndex = 11
		};
		list11.Add(avatarItem);
	}

	private void PopulateAvatarsScroller(GuiScrollingContainer avatarsScroller)
	{
		string str = NetworkScript.player.vessel.playerAvatarUrl;
		if (str.Contains("FixedAvatar_"))
		{
			int.TryParse(str.Split(new char[] { '\u005F' })[1], ref this.currentAvatarIndex);
		}
		else if (!str.Contains("no_avatar"))
		{
			this.currentAvatarIndex = 0;
		}
		else
		{
			this.currentAvatarIndex = 1;
		}
		this.lblSelectedAvatar.text = Enumerable.FirstOrDefault<AvatarItem>(Enumerable.Where<AvatarItem>(this.avatars, new Func<AvatarItem, bool>(this, (AvatarItem p) => p.avatarIndex == this.currentAvatarIndex))).avatarName;
		byte num = 0;
		byte num1 = 0;
		for (int i = 0; i < this.avatars.get_Count(); i++)
		{
			AvatarItem item = this.avatars.get_Item(i);
			if (i != 0 && i % 6 == 0)
			{
				num = (byte)(num + 1);
				num1 = 0;
			}
			string str1 = (!item.IsLocked ? item.avatarName : string.Concat(item.avatarName, "\n", string.Format(StaticData.Translate("key_profile_appearance_required_access_level"), item.minAccessLevel)));
			GuiButtonFixed guiButtonFixed = new GuiButtonFixed()
			{
				Caption = string.Empty
			};
			guiButtonFixed.SetTexture("FixedAvatars", "avatar-background");
			guiButtonFixed.boundries = new Rect((float)(num1 * 104), (float)(num * 104), 100f, 100f);
			guiButtonFixed.eventHandlerParam.customData = (int)item.avatarIndex;
			guiButtonFixed.eventHandlerParam.customData2 = guiButtonFixed;
			guiButtonFixed.behaviourKeepClicked = true;
			guiButtonFixed.IsClicked = (this.currentAvatarIndex != item.avatarIndex ? false : this.currentAvatarIndex != 0);
			guiButtonFixed.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.OnAvatarClicked);
			EventHandlerParam eventHandlerParam = new EventHandlerParam()
			{
				customData = str1,
				customData2 = guiButtonFixed
			};
			guiButtonFixed.tooltipWindowParam = eventHandlerParam;
			guiButtonFixed.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			avatarsScroller.AddContent(guiButtonFixed);
			GuiTexture guiTexture = new GuiTexture()
			{
				boundries = new Rect((float)(num1 * 104), (float)(num * 104), 100f, 100f)
			};
			guiTexture.SetTextureKeepSize("FixedAvatars", string.Format("FixedAvatar_{0}", item.avatarIndex));
			avatarsScroller.AddContent(guiTexture);
			if (item.avatarIndex == 0 && this.currentAvatarIndex == 0)
			{
				guiTexture.SetTextureKeepSize(this.playerAvatar.GetTexture2D());
				guiTexture.X = (float)(num1 * 104 + 1);
				guiTexture.Y = (float)(num * 104 + 1);
				guiTexture.boundries.set_width(98f);
				guiTexture.boundries.set_height(98f);
			}
			if (this.currentAvatarIndex == item.avatarIndex)
			{
				this.currentAvatarButton = guiButtonFixed;
			}
			if (item.IsLocked)
			{
				GuiTexture guiTexture1 = new GuiTexture()
				{
					boundries = new Rect((float)(num1 * 104), (float)(num * 104), 100f, 100f)
				};
				guiTexture1.SetTextureKeepSize("FixedAvatars", "avatarLockFrame");
				avatarsScroller.AddContent(guiTexture1);
				guiButtonFixed.isEnabled = false;
			}
			num1 = (byte)(num1 + 1);
		}
	}

	private void PopulateBlacklistTab()
	{
		if (this.blackListScroller != null)
		{
			this.blackListScroller.Claer();
		}
		List<PlayerProfile> list = new List<PlayerProfile>();
		List<PlayerProfile> list1 = list;
		IList<PlayerProfile> values = NetworkScript.player.myBlacklist.get_Values();
		if (ProfileScreen.<>f__am$cache3D == null)
		{
			ProfileScreen.<>f__am$cache3D = new Func<PlayerProfile, bool>(null, (PlayerProfile t) => t.isOnline);
		}
		list1.AddRange(Enumerable.OrderByDescending<PlayerProfile, bool>(values, ProfileScreen.<>f__am$cache3D));
		this.DrawPlayerList(list, this.blackListScroller, false);
	}

	public void PopulateData(PlayerProfile data)
	{
		if (data == null)
		{
			data = this.selectedPlayer;
		}
		this.selectedPlayer = data;
		this.PopulateLeftSide(data);
		if (data.userName != NetworkScript.player.playerBelongings.playerName)
		{
			this.tabBtnFriends.isEnabled = false;
			this.tabBtnBlacklist.isEnabled = false;
			if (ProfileScreen.selectedTabIndex == 2 || ProfileScreen.selectedTabIndex == 3)
			{
				this.SwichToProfileTab();
			}
		}
		else
		{
			this.tabBtnFriends.isEnabled = true;
			this.tabBtnBlacklist.isEnabled = true;
		}
		if (ProfileScreen.selectedTabIndex == 0)
		{
			this.PopulateProfileTab(data);
		}
		else if (ProfileScreen.selectedTabIndex == 1)
		{
			this.PopulateAchievementsTab(data);
		}
		else if (ProfileScreen.selectedTabIndex == 2)
		{
			this.PopulateFriendsTab();
		}
		else if (ProfileScreen.selectedTabIndex == 3)
		{
			this.PopulateBlacklistTab();
		}
	}

	private void PopulateFriendsTab()
	{
		if (this.firendListScroller != null)
		{
			this.firendListScroller.Claer();
		}
		List<PlayerProfile> list = new List<PlayerProfile>();
		List<PlayerProfile> list1 = list;
		IList<PlayerProfile> values = NetworkScript.player.myFriends.get_Values();
		if (ProfileScreen.<>f__am$cache3C == null)
		{
			ProfileScreen.<>f__am$cache3C = new Func<PlayerProfile, bool>(null, (PlayerProfile t) => t.isOnline);
		}
		list1.AddRange(Enumerable.OrderByDescending<PlayerProfile, bool>(values, ProfileScreen.<>f__am$cache3C));
		this.DrawPlayerList(list, this.firendListScroller, true);
	}

	private void PopulateLeftSide(PlayerProfile info)
	{
		EventHandlerParam eventHandlerParam;
		string str = string.Format("fraction{0}Icon", info.fractionId);
		this.playerFraction.SetTexture("FrameworkGUI", str);
		this.playerName.text = string.Format("{0} ({1})", info.userName, info.level);
		float textWidth = (174f - (this.playerName.TextWidth + this.playerFraction.boundries.get_width())) * 0.5f;
		this.playerFraction.X = 30f + textWidth;
		this.playerName.X = this.playerFraction.X + this.playerFraction.boundries.get_width();
		Texture2D avatarOrStartIt = playWebGame.GetAvatarOrStartIt(info.avatarUrl, new Action<AvatarJob>(this, ProfileScreen.SetAvatar), this.playerAvatar);
		if (avatarOrStartIt == null)
		{
			this.playerAvatar.SetTextureKeepSize("FrameworkGUI", "unknown");
			avatarOrStartIt = (Texture2D)playWebGame.assets.GetFromStaticSet("FrameworkGUI", "unknown");
		}
		this.playerAvatar.SetTextureKeepSize(avatarOrStartIt);
		if (!info.isOnline)
		{
			this.onlineStatus.SetTexture("NewGUI", "option_StatusOffline");
			GuiTexture guiTexture = this.onlineStatus;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_profile_screen_tooltips_offline"),
				customData2 = this.onlineStatus
			};
			guiTexture.tooltipWindowParam = eventHandlerParam;
		}
		else
		{
			this.onlineStatus.SetTexture("NewGUI", "option_StatusOnline");
			GuiTexture guiTexture1 = this.onlineStatus;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_profile_screen_tooltips_online"),
				customData2 = this.onlineStatus
			};
			guiTexture1.tooltipWindowParam = eventHandlerParam;
		}
		this.onlineStatus.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		base.RemoveGuiElement(this.btnParty);
		this.leftSideElement.Remove(this.btnParty);
		this.btnParty = new GuiButtonFixed();
		this.btnParty.SetTexture("NewGUI", "button_party");
		this.btnParty.X = 156f;
		this.btnParty.Y = 128f;
		this.btnParty.Caption = string.Empty;
		this.btnParty.isEnabled = false;
		base.AddGuiElement(this.btnParty);
		this.leftSideElement.Add(this.btnParty);
		if (!this.CanInviteToParty(info))
		{
			this.btnParty.isEnabled = false;
			GuiButtonFixed guiButtonFixed = this.btnParty;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_profile_screen_tooltips_no_invite"),
				customData2 = this.btnParty
			};
			guiButtonFixed.tooltipWindowParam = eventHandlerParam;
		}
		else
		{
			this.btnParty.isEnabled = true;
			GuiButtonFixed guiButtonFixed1 = this.btnParty;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = info.userName
			};
			guiButtonFixed1.eventHandlerParam = eventHandlerParam;
			this.btnParty.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.InviteToParty);
			GuiButtonFixed guiButtonFixed2 = this.btnParty;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_profile_screen_tooltips_invite"),
				customData2 = this.btnParty
			};
			guiButtonFixed2.tooltipWindowParam = eventHandlerParam;
		}
		this.btnParty.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		if (!info.isOnline || !(info.userName != NetworkScript.player.playerBelongings.playerName))
		{
			this.btnChat.isEnabled = false;
			GuiButtonFixed guiButtonFixed3 = this.btnChat;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_profile_screen_tooltips_no_chat"),
				customData2 = this.btnChat
			};
			guiButtonFixed3.tooltipWindowParam = eventHandlerParam;
		}
		else
		{
			this.btnChat.isEnabled = true;
			GuiButtonFixed guiButtonFixed4 = this.btnChat;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = StaticData.Translate("key_profile_screen_tooltips_chat"),
				customData2 = this.btnChat
			};
			guiButtonFixed4.tooltipWindowParam = eventHandlerParam;
			GuiButtonFixed guiButtonFixed5 = this.btnChat;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = info.userName
			};
			guiButtonFixed5.eventHandlerParam = eventHandlerParam;
			this.btnChat.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.StartChatWith);
		}
		this.btnChat.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
		if (info.userName == NetworkScript.player.playerBelongings.playerName)
		{
			base.RemoveGuiElement(this.btnFriends);
			base.RemoveGuiElement(this.btnBlacklist);
			this.btnFriends = null;
			this.btnBlacklist = null;
		}
		else
		{
			bool flag = false;
			bool flag1 = false;
			if (this.btnFriends == null)
			{
				this.btnFriends = new GuiButtonFixed()
				{
					X = 56f,
					Y = 205f,
					Caption = string.Empty
				};
				base.AddGuiElement(this.btnFriends);
				this.forDelete.Add(this.btnFriends);
				this.btnBlacklist = new GuiButtonFixed()
				{
					X = 106f,
					Y = 205f,
					Caption = string.Empty
				};
				base.AddGuiElement(this.btnBlacklist);
				this.forDelete.Add(this.btnBlacklist);
			}
			base.RemoveGuiElement(this.btnFriends);
			this.leftSideElement.Remove(this.btnFriends);
			this.btnFriends = new GuiButtonFixed()
			{
				Caption = string.Empty,
				isEnabled = false
			};
			if (!NetworkScript.player.myFriends.ContainsKey(info.userName))
			{
				this.btnFriends.SetTexture("NewGUI", "addToFriends");
				this.btnFriends.X = 56f;
				this.btnFriends.Y = 205f;
				GuiButtonFixed guiButtonFixed6 = this.btnFriends;
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_profile_screen_tooltips_add_friend"),
					customData2 = this.btnFriends
				};
				guiButtonFixed6.tooltipWindowParam = eventHandlerParam;
				this.btnFriends.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.AddToFriedns);
			}
			else
			{
				flag = true;
				this.btnFriends.SetTexture("NewGUI", "removeFromFriends");
				this.btnFriends.X = 56f;
				this.btnFriends.Y = 205f;
				GuiButtonFixed guiButtonFixed7 = this.btnFriends;
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_profile_screen_tooltips_remove_friend"),
					customData2 = this.btnFriends
				};
				guiButtonFixed7.tooltipWindowParam = eventHandlerParam;
				this.btnFriends.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.RemoveFromFriends);
			}
			this.btnFriends.isEnabled = true;
			GuiButtonFixed guiButtonFixed8 = this.btnFriends;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = info.userName
			};
			guiButtonFixed8.eventHandlerParam = eventHandlerParam;
			this.btnFriends.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			base.AddGuiElement(this.btnFriends);
			this.leftSideElement.Add(this.btnFriends);
			base.RemoveGuiElement(this.btnBlacklist);
			this.leftSideElement.Remove(this.btnBlacklist);
			this.btnBlacklist = new GuiButtonFixed()
			{
				Caption = string.Empty,
				isEnabled = false
			};
			base.AddGuiElement(this.btnBlacklist);
			this.leftSideElement.Add(this.btnBlacklist);
			if (!NetworkScript.player.myBlacklist.ContainsKey(info.userName))
			{
				this.btnBlacklist.SetTexture("NewGUI", "addToBlacklist");
				this.btnBlacklist.X = 106f;
				this.btnBlacklist.Y = 205f;
				GuiButtonFixed guiButtonFixed9 = this.btnBlacklist;
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_profile_screen_tooltips_add_blacklist"),
					customData2 = this.btnBlacklist
				};
				guiButtonFixed9.tooltipWindowParam = eventHandlerParam;
				this.btnBlacklist.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.AddToBlacklist);
			}
			else
			{
				flag1 = true;
				this.btnBlacklist.SetTexture("NewGUI", "removeFromBlacklist");
				this.btnBlacklist.X = 106f;
				this.btnBlacklist.Y = 205f;
				GuiButtonFixed guiButtonFixed10 = this.btnBlacklist;
				eventHandlerParam = new EventHandlerParam()
				{
					customData = StaticData.Translate("key_profile_screen_tooltips_remove_blacklist"),
					customData2 = this.btnBlacklist
				};
				guiButtonFixed10.tooltipWindowParam = eventHandlerParam;
				this.btnBlacklist.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.RemoveFromBlacklist);
			}
			this.btnBlacklist.isEnabled = true;
			GuiButtonFixed guiButtonFixed11 = this.btnBlacklist;
			eventHandlerParam = new EventHandlerParam()
			{
				customData = info.userName
			};
			guiButtonFixed11.eventHandlerParam = eventHandlerParam;
			this.btnBlacklist.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
			if (flag)
			{
				this.btnBlacklist.isEnabled = false;
			}
			if (flag1)
			{
				this.btnFriends.isEnabled = false;
			}
		}
		this.rankPositionVal.text = info.expRank.ToString("#,##0");
		this.pvpTitleValue.text = PlayerItems.GetRankingTitle((int)info.honor);
		this.honorValue.text = info.honor.ToString("#,##0");
		this.playtimeValue.text = string.Format(StaticData.Translate("key_profile_screen_playtime_value"), info.playTime / (long)86400, info.playTime / (long)3600 % (long)24, info.playTime / (long)60 % (long)60);
		if (playWebGame.GAME_TYPE != "ru")
		{
			if (info.userName != NetworkScript.player.playerBelongings.playerName)
			{
				this.btnFb.Caption = StaticData.Translate("key_profile_screen_to_my_profile");
				this.btnFb.Clicked = null;
				this.btnFb.isEnabled = true;
				this.btnFb._marginLeft = 10;
				this.btnFb.Alignment = 4;
				this.btnFb.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.OnReturnToMyProfileClick);
				this.fbIcon.SetTextureKeepSize("FrameworkGUI", "empty");
			}
			else
			{
				this.btnFb.Caption = StaticData.Translate("key_profile_screen_to_my_fb");
				this.btnFb.Clicked = null;
				this.btnFb.isEnabled = true;
				this.btnFb._marginLeft = 30;
				this.btnFb.Alignment = 3;
				this.btnFb.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.OnShowFbFriendClick);
				this.fbIcon.SetTextureKeepSize("NewGUI", "fbIcon");
			}
			this.btnFb.isEnabled = true;
		}
	}

	public void PopulatePendingAwards()
	{
		EventHandlerParam eventHandlerParam;
		if (ProfileScreen.selectedTabIndex != 0)
		{
			return;
		}
		if (this.awardsListScroller == null)
		{
			this.awardsListScroller = new GuiScrollingContainer(230f, 263f, 658f, 267f, 4, this);
			this.awardsListScroller.SetArrowStep(140f);
			base.AddGuiElement(this.awardsListScroller);
			this.forDelete.Add(this.awardsListScroller);
		}
		else
		{
			this.awardsListScroller.Claer();
		}
		if (NetworkScript.player.playerBelongings.playerAwards.get_Count() == 0)
		{
			GuiLabel guiLabel = new GuiLabel()
			{
				boundries = new Rect(5f, 0f, 630f, 267f),
				Alignment = 4,
				text = StaticData.Translate("key_profile_screen_empty_pending_awards_list"),
				FontSize = 16,
				TextColor = GuiNewStyleBar.blueColor,
				Font = GuiLabel.FontBold
			};
			this.awardsListScroller.AddContent(guiLabel);
			return;
		}
		int num = 0;
		IEnumerator<PlayerPendingAward> enumerator = NetworkScript.player.playerBelongings.playerAwards.get_Values().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				PlayerPendingAward current = enumerator.get_Current();
				GuiTexture guiTexture = new GuiTexture();
				guiTexture.SetTexture("ConfigWnd", "pendingAwardFrame");
				guiTexture.X = (float)(5 + num % 2 * 323);
				guiTexture.Y = (float)(0 + num / 2 * 140);
				this.awardsListScroller.AddContent(guiTexture);
				GuiTexture rect = new GuiTexture();
				if (PlayerItems.IsMineral(current.itemType))
				{
					rect.SetTexture("MineralsAvatars", StaticData.allTypes.get_Item(current.itemType).assetName);
					rect.boundries = new Rect(guiTexture.X + 26f, guiTexture.Y + 12f, 60f, 60f);
				}
				else if (!PlayerItems.IsPowerUp(current.itemType))
				{
					rect.SetItemTexture(current.itemType);
					rect.boundries = new Rect(guiTexture.X + 12f, guiTexture.Y + 12f, 88f, 60f);
				}
				else
				{
					rect.SetItemTexture(current.itemType);
					rect.boundries = new Rect(guiTexture.X + 26f, guiTexture.Y + 12f, 60f, 60f);
				}
				this.awardsListScroller.AddContent(rect);
				if (current.get_IsTimedLimited())
				{
					GuiTexture x = new GuiTexture();
					x.SetTexture("NewGUI", "ep_clock");
					x.X = guiTexture.X + 12f;
					x.Y = guiTexture.Y + 90f;
					this.awardsListScroller.AddContent(x);
					TimeSpan timeSpan = current.expireTime - StaticData.now;
					GuiTimeTracker guiTimeTracker = new GuiTimeTracker((int)timeSpan.get_TotalSeconds(), this.awardsListScroller, new Rect(guiTexture.X + 31f, guiTexture.Y + 92f, 70f, 13f))
					{
						FontSize = 12,
						Alignment = 3
					};
				}
				GuiLabel fontBold = new GuiLabel()
				{
					boundries = new Rect(guiTexture.X + 106f, guiTexture.Y + 4f, 195f, 38f)
				};
				if (!current.isDaily)
				{
					fontBold.text = (!string.IsNullOrEmpty(current.title) ? StaticData.Translate(current.title) : StaticData.Translate("key_award_pending_prize_title"));
				}
				else
				{
					fontBold.text = StaticData.Translate("key_award_daily_reward");
				}
				fontBold.Font = GuiLabel.FontBold;
				fontBold.FontSize = 14;
				fontBold.TextColor = GuiNewStyleBar.blueColor;
				this.awardsListScroller.AddContent(fontBold);
				GuiLabel guiLabel1 = new GuiLabel()
				{
					boundries = new Rect(guiTexture.X + 106f, guiTexture.Y + 45f, 195f, 28f),
					FontSize = 12,
					Font = GuiLabel.FontBold
				};
				this.awardsListScroller.AddContent(guiLabel1);
				if (PlayerItems.IsBooster(current.itemType))
				{
					guiLabel1.text = string.Format(StaticData.Translate("key_award_booster_details"), StaticData.Translate(StaticData.allTypes.get_Item(current.itemType).uiName), current.amount);
				}
				else if (PlayerItems.IsPowerUp(current.itemType))
				{
					guiLabel1.text = string.Format("{0} for {1}h", string.Format(StaticData.Translate(StaticData.allTypes.get_Item(current.itemType).uiName), PlayerItems.GetPowerUpEffectValue(current.itemType, NetworkScript.player.playerBelongings.playerLevel)), current.amount);
				}
				else if (PlayerItems.IsCorpus(current.itemType) || PlayerItems.IsShield(current.itemType) || PlayerItems.IsEngine(current.itemType) || PlayerItems.IsExtra(current.itemType) || PlayerItems.IsWeapon(current.itemType))
				{
					string empty = string.Empty;
					Color _white = Color.get_white();
					SlotItem slotItem = new SlotItem();
					slotItem.set_ItemType(current.itemType);
					slotItem.set_BonusCnt(current.bonuses);
					Inventory.ItemRarity(slotItem, out empty, out _white);
					guiLabel1.text = empty;
					guiLabel1.TextColor = _white;
				}
				else
				{
					guiLabel1.text = string.Format(StaticData.Translate("key_objective_name_kills"), current.amount.ToString("#,##0"), StaticData.Translate(StaticData.allTypes.get_Item(current.itemType).uiName));
				}
				bool cargo = false;
				if (current.itemType == PlayerItems.TypeNova || current.itemType == PlayerItems.TypeCash || current.itemType == PlayerItems.TypeEquilibrium || current.itemType == PlayerItems.TypeUltralibrium || current.itemType == PlayerItems.TypeNeuron || PlayerItems.IsBooster(current.itemType) || PlayerItems.IsPowerUp(current.itemType))
				{
					cargo = true;
				}
				else if (PlayerItems.IsCorpus(current.itemType) || PlayerItems.IsShield(current.itemType) || PlayerItems.IsEngine(current.itemType) || PlayerItems.IsExtra(current.itemType) || PlayerItems.IsWeapon(current.itemType) || PlayerItems.IsUniquePortalPart(current.itemType))
				{
					cargo = NetworkScript.player.playerBelongings.HaveAFreeSlot();
				}
				else if (PlayerItems.IsStackable(current.itemType))
				{
					cargo = NetworkScript.player.playerBelongings.AllowedPileSize((int)current.itemType) > 0;
				}
				else if (PlayerItems.IsMineral(current.itemType))
				{
					cargo = NetworkScript.player.vessel.cfg.cargoMax - (int)NetworkScript.player.playerBelongings.playerItems.get_Cargo() > 0;
				}
				GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
				guiButtonResizeable.boundries.set_x(guiTexture.X + 106f);
				guiButtonResizeable.boundries.set_y(guiTexture.Y + 77f);
				guiButtonResizeable.boundries.set_width(195f);
				guiButtonResizeable.Caption = StaticData.Translate("key_btn_claim_pending_award");
				guiButtonResizeable.FontSize = 16;
				guiButtonResizeable.Alignment = 4;
				guiButtonResizeable.SetOrangeTexture();
				guiButtonResizeable.eventHandlerParam.customData = current.rewardId;
				guiButtonResizeable.Clicked = new Action<EventHandlerParam>(this, ProfileScreen.ClaimPendingAward);
				guiButtonResizeable.isEnabled = cargo;
				this.awardsListScroller.AddContent(guiButtonResizeable);
				if (!cargo)
				{
					if (!PlayerItems.IsMineral(current.itemType))
					{
						eventHandlerParam = new EventHandlerParam()
						{
							customData = StaticData.Translate("key_profile_screen_tooltips_no_slots"),
							customData2 = guiButtonResizeable
						};
						guiButtonResizeable.tooltipWindowParam = eventHandlerParam;
					}
					else
					{
						eventHandlerParam = new EventHandlerParam()
						{
							customData = StaticData.Translate("key_profile_screen_tooltips_no_cargo"),
							customData2 = guiButtonResizeable
						};
						guiButtonResizeable.tooltipWindowParam = eventHandlerParam;
					}
					guiButtonResizeable.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(UniversalTooltip.CreateTooltip);
				}
				num++;
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

	private void PopulateProfileTab(PlayerProfile info)
	{
		this.barAccessLevel.current = (float)((int)((float)info.accessLevel / (float)StaticData.maxAccessLevel * 100f));
		this.barQuests.current = (float)((int)((float)info.questsDone / (float)StaticData.questsCount * 100f));
		this.barAchievements.current = (float)((int)((float)info.achievementsDone / this.achievementsAll * 100f));
		this.accessLevelProgress.text = string.Format("{0}", info.accessLevel);
		this.questsProgress.text = string.Format("{0}%", this.barQuests.current);
		this.achievementsProgress.text = string.Format("{0}%", this.barAchievements.current);
		this.alienKillsCnt.text = info.alienKills.ToString("#,##0");
		this.playerKillsCnt.text = info.playersKills.ToString("#,##0");
	}

	private void RemoveFromBlacklist(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void ProfileScreen::RemoveFromBlacklist(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void RemoveFromBlacklist(EventHandlerParam)
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

	private void RemoveFromFriends(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void ProfileScreen::RemoveFromFriends(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void RemoveFromFriends(EventHandlerParam)
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

	[DebuggerHidden]
	private IEnumerator SendFBFriendRequest(string facebookIds)
	{
		return new ProfileScreen.<SendFBFriendRequest>c__Iterator11();
	}

	private void SendGift(EventHandlerParam prm)
	{
		SendGiftsWindow.receiverName = (string)prm.customData;
		SendGiftsWindow.receiverLevel = (int)prm.customData2;
		MainScreenWindow mainScreenWindow = AndromedaGui.mainWnd;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = (byte)33
		};
		mainScreenWindow.OnWindowBtnClicked(eventHandlerParam);
	}

	private void SetAvatar(AvatarJob prm)
	{
		((GuiTexture)prm.token).SetTextureKeepSize(prm.job.get_texture());
	}

	private void ShowConfirmCustomAvatarDialog()
	{
		if (this.dlgConfirmCustomAvatar != null)
		{
			this.dlgConfirmCustomAvatar.RemoveGUIItems();
			this.dlgConfirmCustomAvatar = null;
		}
		this.dlgConfirmCustomAvatar = new GuiDialog();
		this.dlgConfirmCustomAvatar.Create(StaticData.Translate("key_appearance_confirm_message"), StaticData.Translate("key_appearance_confirm_gotosite"), StaticData.Translate("key_appearance_confirm_later"), null);
		this.dlgConfirmCustomAvatar.OkClicked = new Action<object>(this, ProfileScreen.OnConfirmCustomAvatarOptionClicked);
		GuiButtonResizeable guiButtonResizeable = this.dlgConfirmCustomAvatar.btnOK;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = 0
		};
		guiButtonResizeable.eventHandlerParam = eventHandlerParam;
		this.dlgConfirmCustomAvatar.CancelClicked = new Action<object>(this, ProfileScreen.OnConfirmCustomAvatarOptionClicked);
		GuiButtonResizeable guiButtonResizeable1 = this.dlgConfirmCustomAvatar.btnCancel;
		eventHandlerParam = new EventHandlerParam()
		{
			customData = 1
		};
		guiButtonResizeable1.eventHandlerParam = eventHandlerParam;
		this.ignoreClickEvents = true;
	}

	private void StartChatWith(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void ProfileScreen::StartChatWith(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void StartChatWith(EventHandlerParam)
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

	public void SwichToProfileTab()
	{
		this.tabBtnProfile.IsClicked = true;
	}

	private void ValidateNewNickname()
	{
		if (this.btnChangeNickname == null)
		{
			return;
		}
		if (this.tbNewNickname.text.get_Length() > 16)
		{
			this.tbNewNickname.text = this.tbNewNickname.text.Substring(0, 16);
		}
		if (this.tbNewNickname.text.Contains(" "))
		{
			this.tbNewNickname.text = this.tbNewNickname.text.Replace(" ", string.Empty);
		}
		if (!this.IsEnteredTextOK(this.tbNewNickname.text))
		{
			if (this.lblChangeNicknameError.text != StaticData.Translate("key_appearance_illegal_nickname"))
			{
				this.lblChangeNicknameError.text = StaticData.Translate("key_appearance_illegal_nickname");
			}
			if (this.btnChangeNickname.isEnabled)
			{
				this.btnChangeNickname.isEnabled = false;
			}
			return;
		}
		if (this.lblChangeNicknameError.text != string.Empty)
		{
			this.lblChangeNicknameError.text = string.Empty;
		}
		if (!this.btnChangeNickname.isEnabled)
		{
			this.btnChangeNickname.isEnabled = true;
		}
		if (this.tbNewNickname.text == NetworkScript.player.vessel.playerName && this.btnChangeNickname.isEnabled)
		{
			this.btnChangeNickname.isEnabled = false;
		}
		else if (!this.btnChangeNickname.isEnabled && NetworkScript.player.playerBelongings.playerItems.get_Nova() >= (long)1000 && NetworkScript.player.vessel.pvpState != 3 && NetworkScript.player.vessel.pvpState != 4)
		{
			this.btnChangeNickname.isEnabled = true;
		}
	}

	private void ValidateUserName()
	{
		if (Enumerable.Count<char>(this.newEntryUserName.text) > 24)
		{
			this.newEntryUserName.text = this.newEntryUserName.text.Substring(0, 24);
		}
	}

	private void ViewPlayerProfile(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void ProfileScreen::ViewPlayerProfile(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void ViewPlayerProfile(EventHandlerParam)
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
}