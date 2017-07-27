using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TransferableObjects;
using UnityEngine;

public class Inventory
{
	public static GuiWindow window;

	private static List<GuiElement> forDelete;

	public static bool isRightClickActionEnable;

	public static Action ConfigWndAfterRightClkAction;

	private AndromedaGuiDragDropPlace[] slots;

	public static bool isVaultMenuOpen;

	public static bool isGuildVaultMenuOpen;

	public static bool isItemRerollMenuOpen;

	private static AndromedaGuiDragDropPlace lastSelectedPlace;

	private static AndromedaGuiDragDropPlace dialogItemSrc;

	private static AndromedaGuiDragDropPlace dialogItemDst;

	public static GuiWindow dialogWindow;

	private static GuiWindow stackableWnd;

	private static GuiLabel stackItemAmount;

	private static GuiLabel stackItemSellPrice;

	private static float sellPriceOfStackableItem;

	private static bool isAmmoSelecktedStackableItem;

	private static GuiHorizontalSlider sliderStack;

	public static Action<int> activateCompareBox;

	public static Action<int> activateItemReroll;

	public static Action<int> activatePortalPartDrop;

	public static Action<AndromedaGuiDragDropPlace, AndromedaGuiDragDropPlace> secondaryDropHandler;

	public static Action closeStackablePopUp;

	public static List<AndromedaGuiDragDropPlace> places;

	public static List<AndromedaGuiDragDropPlace> inventoryPlaces;

	public static GuiButtonFixed plus_btn;

	static Inventory()
	{
		Inventory.forDelete = new List<GuiElement>();
		Inventory.isRightClickActionEnable = false;
		Inventory.ConfigWndAfterRightClkAction = null;
		Inventory.isVaultMenuOpen = false;
		Inventory.isGuildVaultMenuOpen = false;
		Inventory.isItemRerollMenuOpen = false;
		Inventory.dialogItemSrc = null;
		Inventory.dialogItemDst = null;
		Inventory.places = new List<AndromedaGuiDragDropPlace>();
		Inventory.inventoryPlaces = new List<AndromedaGuiDragDropPlace>();
	}

	public Inventory()
	{
	}

	private static void AddDropZonesToTexture(AndromedaGuiDragDropPlace place)
	{
		place.txItem.CleanDropZones();
		foreach (AndromedaGuiDragDropPlace dropTarget in place.dropTargets)
		{
			Inventory.AddSingleDropZoneToTexture(place, dropTarget);
		}
	}

	private static void AddSingleDropZoneToTexture(AndromedaGuiDragDropPlace place, AndromedaGuiDragDropPlace dropTarget)
	{
		if (place.txItem != null)
		{
			place.txItem.AddDropZone(dropTarget.dropZonePosition, Inventory.GetDropZoneKey(place, dropTarget), dropTarget.txFrameDropTarget, dropTarget.txFrameDropTargetHover);
		}
	}

	private static void AtachItemToShip(object prm)
	{
		if (((AndromedaGuiDragDropPlace)prm).item == null)
		{
			Debug.Log("CP I");
			return;
		}
		AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = (AndromedaGuiDragDropPlace)prm;
		if (((AndromedaGuiDragDropPlace)prm).location != 1)
		{
			Debug.Log("CP II");
			return;
		}
		if (StaticData.allTypes.get_Item(andromedaGuiDragDropPlace.item.get_ItemType()).levelRestriction > NetworkScript.player.playerBelongings.playerLevel)
		{
			if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
			{
				AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "error");
				AudioManager.PlayGUISound(fromStaticSet);
			}
			Debug.Log("LevelRestriction");
			return;
		}
		AndromedaGuiDragDropPlace andromedaGuiDragDropPlace1 = null;
		if (PlayerItems.IsExtra(andromedaGuiDragDropPlace.item.get_ItemType()))
		{
			List<AndromedaGuiDragDropPlace> list = Inventory.places;
			if (Inventory.<>f__am$cache1B == null)
			{
				Inventory.<>f__am$cache1B = new Func<AndromedaGuiDragDropPlace, bool>(null, (AndromedaGuiDragDropPlace t) => (t.location != 13 ? false : t.isEmpty));
			}
			andromedaGuiDragDropPlace1 = Enumerable.FirstOrDefault<AndromedaGuiDragDropPlace>(Enumerable.Where<AndromedaGuiDragDropPlace>(list, Inventory.<>f__am$cache1B));
		}
		if (PlayerItems.IsEngine(andromedaGuiDragDropPlace.item.get_ItemType()))
		{
			List<AndromedaGuiDragDropPlace> list1 = Inventory.places;
			if (Inventory.<>f__am$cache1C == null)
			{
				Inventory.<>f__am$cache1C = new Func<AndromedaGuiDragDropPlace, bool>(null, (AndromedaGuiDragDropPlace t) => (t.location != 12 ? false : t.isEmpty));
			}
			andromedaGuiDragDropPlace1 = Enumerable.FirstOrDefault<AndromedaGuiDragDropPlace>(Enumerable.Where<AndromedaGuiDragDropPlace>(list1, Inventory.<>f__am$cache1C));
			if (andromedaGuiDragDropPlace1 == null)
			{
				List<AndromedaGuiDragDropPlace> list2 = Inventory.places;
				if (Inventory.<>f__am$cache1D == null)
				{
					Inventory.<>f__am$cache1D = new Func<AndromedaGuiDragDropPlace, bool>(null, (AndromedaGuiDragDropPlace t) => (t.location != 9 ? false : t.isEmpty));
				}
				andromedaGuiDragDropPlace1 = Enumerable.FirstOrDefault<AndromedaGuiDragDropPlace>(Enumerable.Where<AndromedaGuiDragDropPlace>(list2, Inventory.<>f__am$cache1D));
			}
		}
		if (PlayerItems.IsCorpus(andromedaGuiDragDropPlace.item.get_ItemType()))
		{
			List<AndromedaGuiDragDropPlace> list3 = Inventory.places;
			if (Inventory.<>f__am$cache1E == null)
			{
				Inventory.<>f__am$cache1E = new Func<AndromedaGuiDragDropPlace, bool>(null, (AndromedaGuiDragDropPlace t) => (t.location != 11 ? false : t.isEmpty));
			}
			andromedaGuiDragDropPlace1 = Enumerable.FirstOrDefault<AndromedaGuiDragDropPlace>(Enumerable.Where<AndromedaGuiDragDropPlace>(list3, Inventory.<>f__am$cache1E));
			if (andromedaGuiDragDropPlace1 == null)
			{
				List<AndromedaGuiDragDropPlace> list4 = Inventory.places;
				if (Inventory.<>f__am$cache1F == null)
				{
					Inventory.<>f__am$cache1F = new Func<AndromedaGuiDragDropPlace, bool>(null, (AndromedaGuiDragDropPlace t) => (t.location != 9 ? false : t.isEmpty));
				}
				andromedaGuiDragDropPlace1 = Enumerable.FirstOrDefault<AndromedaGuiDragDropPlace>(Enumerable.Where<AndromedaGuiDragDropPlace>(list4, Inventory.<>f__am$cache1F));
			}
		}
		if (PlayerItems.IsShield(andromedaGuiDragDropPlace.item.get_ItemType()))
		{
			List<AndromedaGuiDragDropPlace> list5 = Inventory.places;
			if (Inventory.<>f__am$cache20 == null)
			{
				Inventory.<>f__am$cache20 = new Func<AndromedaGuiDragDropPlace, bool>(null, (AndromedaGuiDragDropPlace t) => (t.location != 10 ? false : t.isEmpty));
			}
			andromedaGuiDragDropPlace1 = Enumerable.FirstOrDefault<AndromedaGuiDragDropPlace>(Enumerable.Where<AndromedaGuiDragDropPlace>(list5, Inventory.<>f__am$cache20));
			if (andromedaGuiDragDropPlace1 == null)
			{
				List<AndromedaGuiDragDropPlace> list6 = Inventory.places;
				if (Inventory.<>f__am$cache21 == null)
				{
					Inventory.<>f__am$cache21 = new Func<AndromedaGuiDragDropPlace, bool>(null, (AndromedaGuiDragDropPlace t) => (t.location != 9 ? false : t.isEmpty));
				}
				andromedaGuiDragDropPlace1 = Enumerable.FirstOrDefault<AndromedaGuiDragDropPlace>(Enumerable.Where<AndromedaGuiDragDropPlace>(list6, Inventory.<>f__am$cache21));
			}
		}
		if (PlayerItems.IsWeapon(andromedaGuiDragDropPlace.item.get_ItemType()) && andromedaGuiDragDropPlace.item.get_ItemType() >= PlayerItems.TypeWeaponLaserTire1 && andromedaGuiDragDropPlace.item.get_ItemType() <= PlayerItems.TypeWeaponLaserTire5)
		{
			List<AndromedaGuiDragDropPlace> list7 = Inventory.places;
			if (Inventory.<>f__am$cache22 == null)
			{
				Inventory.<>f__am$cache22 = new Func<AndromedaGuiDragDropPlace, bool>(null, (AndromedaGuiDragDropPlace t) => (t.location != 6 ? false : t.isEmpty));
			}
			andromedaGuiDragDropPlace1 = Enumerable.FirstOrDefault<AndromedaGuiDragDropPlace>(Enumerable.Where<AndromedaGuiDragDropPlace>(list7, Inventory.<>f__am$cache22));
		}
		if (PlayerItems.IsWeapon(andromedaGuiDragDropPlace.item.get_ItemType()) && andromedaGuiDragDropPlace.item.get_ItemType() >= PlayerItems.TypeWeaponPlasmaTire1 && andromedaGuiDragDropPlace.item.get_ItemType() <= PlayerItems.TypeWeaponPlasmaTire5)
		{
			List<AndromedaGuiDragDropPlace> list8 = Inventory.places;
			if (Inventory.<>f__am$cache23 == null)
			{
				Inventory.<>f__am$cache23 = new Func<AndromedaGuiDragDropPlace, bool>(null, (AndromedaGuiDragDropPlace t) => (t.location != 7 ? false : t.isEmpty));
			}
			andromedaGuiDragDropPlace1 = Enumerable.FirstOrDefault<AndromedaGuiDragDropPlace>(Enumerable.Where<AndromedaGuiDragDropPlace>(list8, Inventory.<>f__am$cache23));
		}
		if (PlayerItems.IsWeapon(andromedaGuiDragDropPlace.item.get_ItemType()) && andromedaGuiDragDropPlace.item.get_ItemType() >= PlayerItems.TypeWeaponIonTire1 && andromedaGuiDragDropPlace.item.get_ItemType() <= PlayerItems.TypeWeaponIonTire5)
		{
			List<AndromedaGuiDragDropPlace> list9 = Inventory.places;
			if (Inventory.<>f__am$cache24 == null)
			{
				Inventory.<>f__am$cache24 = new Func<AndromedaGuiDragDropPlace, bool>(null, (AndromedaGuiDragDropPlace t) => (t.location != 8 ? false : t.isEmpty));
			}
			andromedaGuiDragDropPlace1 = Enumerable.FirstOrDefault<AndromedaGuiDragDropPlace>(Enumerable.Where<AndromedaGuiDragDropPlace>(list9, Inventory.<>f__am$cache24));
		}
		if (andromedaGuiDragDropPlace1 != null)
		{
			if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
			{
				AudioClip audioClip = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "button2");
				AudioManager.PlayGUISound(audioClip);
			}
			Inventory.MoveItemInData(andromedaGuiDragDropPlace, andromedaGuiDragDropPlace1);
			return;
		}
		if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
		{
			AudioClip fromStaticSet1 = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "error");
			AudioManager.PlayGUISound(fromStaticSet1);
		}
		Debug.Log("CP IV");
		if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 1)
		{
			((__ConfigWindow)AndromedaGui.mainWnd.activeWindow).ShowNoDestinationSlotFoundWarning();
		}
	}

	private static void BuyItemFromGamblerToInventory(object prm)
	{
		Inventory.<BuyItemFromGamblerToInventory>c__AnonStorey31 variable = null;
		if (((AndromedaGuiDragDropPlace)prm).item == null)
		{
			Debug.Log("CP I");
			return;
		}
		if (((AndromedaGuiDragDropPlace)prm).location != 16 && ((AndromedaGuiDragDropPlace)prm).location != 17)
		{
			Debug.Log(string.Concat("CP III", ((AndromedaGuiDragDropPlace)prm).location));
			return;
		}
		if (!NetworkScript.player.playerBelongings.HaveAFreeSlot())
		{
			if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
			{
				AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "error");
				AudioManager.PlayGUISound(fromStaticSet);
			}
			Debug.Log("CP IV");
			return;
		}
		NetworkScript.player.playerBelongings.FirstFreeInventorySlot();
		AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = Enumerable.FirstOrDefault<AndromedaGuiDragDropPlace>(Enumerable.Where<AndromedaGuiDragDropPlace>(Inventory.places, new Func<AndromedaGuiDragDropPlace, bool>(variable, (AndromedaGuiDragDropPlace t) => (t.item != null || t.location != 1 ? false : t.slot == this.freeInventorySlotId))));
		if (andromedaGuiDragDropPlace == null)
		{
			if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
			{
				AudioClip audioClip = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "error");
				AudioManager.PlayGUISound(audioClip);
			}
			Debug.Log("CP V");
			return;
		}
		if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
		{
			AudioClip fromStaticSet1 = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "button2");
			AudioManager.PlayGUISound(fromStaticSet1);
		}
		Inventory.MoveItemGambler((AndromedaGuiDragDropPlace)prm, andromedaGuiDragDropPlace);
	}

	private static void BuyItemFromMerchantToInventory(object prm)
	{
		Inventory.<BuyItemFromMerchantToInventory>c__AnonStorey30 variable = null;
		if (((AndromedaGuiDragDropPlace)prm).item == null)
		{
			Debug.Log("CP I");
			return;
		}
		if (((AndromedaGuiDragDropPlace)prm).location != 15)
		{
			Debug.Log("CP III");
			return;
		}
		if (!NetworkScript.player.playerBelongings.HaveAFreeSlot())
		{
			if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
			{
				AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "error");
				AudioManager.PlayGUISound(fromStaticSet);
			}
			Debug.Log("CP IV");
			return;
		}
		NetworkScript.player.playerBelongings.FirstFreeInventorySlot();
		AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = Enumerable.FirstOrDefault<AndromedaGuiDragDropPlace>(Enumerable.Where<AndromedaGuiDragDropPlace>(Inventory.places, new Func<AndromedaGuiDragDropPlace, bool>(variable, (AndromedaGuiDragDropPlace t) => (t.item != null || t.location != 1 ? false : t.slot == this.freeInventorySlotId))));
		if (andromedaGuiDragDropPlace == null)
		{
			if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
			{
				AudioClip audioClip = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "error");
				AudioManager.PlayGUISound(audioClip);
			}
			Debug.Log("CP V");
			return;
		}
		if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
		{
			AudioClip fromStaticSet1 = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "button2");
			AudioManager.PlayGUISound(fromStaticSet1);
		}
		Inventory.MoveItemMerchant((AndromedaGuiDragDropPlace)prm, andromedaGuiDragDropPlace);
	}

	private static bool CanPlayerBuyThisItem(ushort itemTypeID, int amount)
	{
		PlayerItemTypesData item = StaticData.allTypes.get_Item(itemTypeID);
		double num = 1;
		num = (!PlayerItems.IsAmmo(itemTypeID) ? (double)amount : (double)(amount / 100));
		if (item.priceCash > 0 && (double)NetworkScript.player.playerBelongings.playerItems.get_Cash() >= (double)item.priceCash * num)
		{
			return true;
		}
		if (item.priceNova > 0 && (double)NetworkScript.player.playerBelongings.playerItems.get_Nova() >= (double)item.priceNova * num)
		{
			return true;
		}
		if (item.priceViral > 0 && (double)NetworkScript.player.playerBelongings.playerItems.GetAmountAt(PlayerItems.TypeEquilibrium) >= (double)item.priceViral * num)
		{
			return true;
		}
		return false;
	}

	public static void ClearInventorySlots(GuiWindow window)
	{
		List<AndromedaGuiDragDropPlace> list = new List<AndromedaGuiDragDropPlace>();
		foreach (AndromedaGuiDragDropPlace place in Inventory.places)
		{
			if (place.location == 1 || place.location == 14 || place.location == 3)
			{
				list.Add(place);
			}
		}
		foreach (AndromedaGuiDragDropPlace andromedaGuiDragDropPlace in list)
		{
			if (andromedaGuiDragDropPlace.frameTexture != null)
			{
				window.RemoveGuiElement(andromedaGuiDragDropPlace.frameTexture);
			}
			if (andromedaGuiDragDropPlace.glowTexture != null)
			{
				window.RemoveGuiElement(andromedaGuiDragDropPlace.glowTexture);
			}
			if (andromedaGuiDragDropPlace.txItem != null)
			{
				window.RemoveGuiElement(andromedaGuiDragDropPlace.txItem);
			}
			if (andromedaGuiDragDropPlace.lblAmount != null)
			{
				window.RemoveGuiElement(andromedaGuiDragDropPlace.lblAmount);
			}
			andromedaGuiDragDropPlace.dropTargets.Clear();
			Inventory.places.Remove(andromedaGuiDragDropPlace);
		}
		foreach (GuiElement guiElement in Inventory.forDelete)
		{
			window.RemoveGuiElement(guiElement);
		}
		Inventory.forDelete.Clear();
	}

	public static void ClearSlots(GuiWindow window)
	{
		foreach (AndromedaGuiDragDropPlace place in Inventory.places)
		{
			if (place.frameTexture != null)
			{
				window.RemoveGuiElement(place.frameTexture);
			}
			if (place.glowTexture != null)
			{
				window.RemoveGuiElement(place.glowTexture);
			}
			if (place.txItem != null)
			{
				window.RemoveGuiElement(place.txItem);
			}
			if (place.lblAmount != null)
			{
				window.RemoveGuiElement(place.lblAmount);
			}
			place.dropTargets.Clear();
		}
		Inventory.places.Clear();
	}

	public static void ClearSlots(ItemLocation location, GuiWindow window)
	{
		List<AndromedaGuiDragDropPlace> list = new List<AndromedaGuiDragDropPlace>();
		foreach (AndromedaGuiDragDropPlace place in Inventory.places)
		{
			if (place.location == location)
			{
				if (place.frameTexture != null)
				{
					window.RemoveGuiElement(place.frameTexture);
				}
				if (place.glowTexture != null)
				{
					window.RemoveGuiElement(place.glowTexture);
				}
				if (place.txItem != null)
				{
					window.RemoveGuiElement(place.txItem);
				}
				if (place.lblAmount != null)
				{
					window.RemoveGuiElement(place.lblAmount);
				}
				place.dropTargets.Clear();
				list.Add(place);
			}
		}
		foreach (AndromedaGuiDragDropPlace andromedaGuiDragDropPlace in list)
		{
			Inventory.places.Remove(andromedaGuiDragDropPlace);
		}
	}

	private static void ConfirmDumpAccountBountItem(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void Inventory::ConfirmDumpAccountBountItem(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void ConfirmDumpAccountBountItem(EventHandlerParam)
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

	private static void ConfirmSellAccountBoundItem(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void Inventory::ConfirmSellAccountBoundItem(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void ConfirmSellAccountBoundItem(EventHandlerParam)
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

	private static void CreateCurrencyDialog(int priceCash, int priceNova, int priceViral)
	{
		Inventory.dialogWindow = new GuiWindow()
		{
			isModal = true
		};
		Inventory.dialogWindow.SetBackgroundTexture("ConfigWindow", "proba");
		Inventory.dialogWindow.isHidden = false;
		Inventory.dialogWindow.zOrder = 220;
		Inventory.dialogWindow.PutToCenter();
		AndromedaGui.gui.AddWindow(Inventory.dialogWindow);
		AndromedaGui.gui.activeToolTipId = Inventory.dialogWindow.handler;
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(Inventory.dialogWindow.boundries.get_width() * 0.05f, 40f, Inventory.dialogWindow.boundries.get_width() * 0.9f, 60f),
			text = StaticData.Translate("key_inventory_currency_dialog"),
			Alignment = 4,
			FontSize = 18
		};
		Inventory.dialogWindow.AddGuiElement(guiLabel);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("ConfigWindow", "X_btn_");
		guiButtonFixed.X = 417f;
		guiButtonFixed.Y = -3f;
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(null, Inventory.CurrencyDialogClose);
		Inventory.dialogWindow.AddGuiElement(guiButtonFixed);
		GuiButtonFixed empty = new GuiButtonFixed();
		empty.SetTexture("ConfigWindow", "testBtn");
		empty.X = 32f;
		empty.Y = 123f;
		empty.Caption = string.Empty;
		empty.isEnabled = (NetworkScript.player.playerBelongings.playerItems.get_Cash() < (long)priceCash ? false : priceCash > 0);
		empty.eventHandlerParam.customData = (SelectedCurrency)0;
		empty.Clicked = new Action<EventHandlerParam>(null, Inventory.OnSelectCurrencySelect);
		Inventory.dialogWindow.AddGuiElement(empty);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("FrameworkGUI", "cashIcon");
		guiTexture.SetSize(24f, 24f);
		guiTexture.Y = empty.Y + 8f;
		guiTexture.X = empty.X + empty.boundries.get_width() / 2f - 12f;
		Inventory.dialogWindow.AddGuiElement(guiTexture);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = empty.boundries,
			Y = 90f,
			text = (priceCash != 0 ? priceCash.ToString("##,##0") : string.Empty),
			Alignment = 4,
			TextColor = GuiNewStyleBar.blueColor
		};
		Inventory.dialogWindow.AddGuiElement(guiLabel1);
		GuiButtonFixed action = new GuiButtonFixed();
		action.SetTexture("ConfigWindow", "testBtn");
		action.X = 172f;
		action.Y = 123f;
		action.Caption = string.Empty;
		action.isEnabled = (NetworkScript.player.playerBelongings.playerItems.get_Nova() < (long)priceNova ? false : priceNova > 0);
		action.eventHandlerParam.customData = (SelectedCurrency)1;
		action.Clicked = new Action<EventHandlerParam>(null, Inventory.OnSelectCurrencySelect);
		Inventory.dialogWindow.AddGuiElement(action);
		GuiTexture y = new GuiTexture();
		y.SetTexture("FrameworkGUI", "novaIcon");
		y.SetSize(24f, 24f);
		y.Y = action.Y + 8f;
		y.X = action.X + action.boundries.get_width() / 2f - 12f;
		Inventory.dialogWindow.AddGuiElement(y);
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = action.boundries,
			Y = 90f,
			text = (priceNova != 0 ? priceNova.ToString("##,##0") : string.Empty),
			Alignment = 4,
			TextColor = GuiNewStyleBar.orangeColor
		};
		Inventory.dialogWindow.AddGuiElement(guiLabel2);
		GuiButtonFixed guiButtonFixed1 = new GuiButtonFixed();
		guiButtonFixed1.SetTexture("ConfigWindow", "testBtn");
		guiButtonFixed1.X = 312f;
		guiButtonFixed1.Y = 123f;
		guiButtonFixed1.Caption = string.Empty;
		guiButtonFixed1.isEnabled = (NetworkScript.player.playerBelongings.playerItems.GetAmountAt(PlayerItems.TypeEquilibrium) < (long)priceViral ? false : priceViral > 0);
		guiButtonFixed1.eventHandlerParam.customData = (SelectedCurrency)2;
		guiButtonFixed1.Clicked = new Action<EventHandlerParam>(null, Inventory.OnSelectCurrencySelect);
		Inventory.dialogWindow.AddGuiElement(guiButtonFixed1);
		GuiTexture x = new GuiTexture();
		x.SetTexture("FrameworkGUI", "eqIcon");
		x.SetSize(24f, 24f);
		x.Y = guiButtonFixed1.Y + 8f;
		x.X = guiButtonFixed1.X + guiButtonFixed1.boundries.get_width() / 2f - 12f;
		Inventory.dialogWindow.AddGuiElement(x);
		GuiLabel guiLabel3 = new GuiLabel()
		{
			boundries = guiButtonFixed1.boundries,
			Y = 90f,
			text = (priceViral != 0 ? priceViral.ToString("##,##0") : string.Empty),
			Alignment = 4,
			TextColor = GuiNewStyleBar.purpleColor
		};
		Inventory.dialogWindow.AddGuiElement(guiLabel3);
	}

	public static void CreateNewItemDialog(SlotItem slotItem, out GuiWindow wnd)
	{
		Color color;
		string str;
		wnd = new GuiWindow()
		{
			isModal = true
		};
		wnd.SetBackgroundTexture("ConfigWindow", "tooltipBackground");
		wnd.boundries.set_width(300f);
		wnd.boundries.set_height(220f);
		wnd.isHidden = false;
		wnd.zOrder = 220;
		wnd.PutToCenter();
		PlayerItemTypesData item = StaticData.allTypes.get_Item(slotItem.get_ItemType());
		Inventory.ItemRarity(slotItem, out str, out color);
		GuiTextureAnimated guiTextureAnimated = new GuiTextureAnimated();
		guiTextureAnimated.Init("TooltipAnimation", "TooltipAnimation", "TooltipAnimation/frame");
		guiTextureAnimated.rotationTime = 2f;
		guiTextureAnimated.boundries.set_x(0f);
		guiTextureAnimated.boundries.set_y(0f);
		wnd.AddGuiElement(guiTextureAnimated);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(3f, 3f, 294f, 22f),
			FontSize = 11,
			text = str,
			TextColor = color,
			Alignment = 4,
			Font = GuiLabel.FontBold
		};
		wnd.AddGuiElement(guiLabel);
		GuiTexture guiTexture = new GuiTexture()
		{
			boundries = new Rect(20f, 40f, 75f, 51f)
		};
		guiTexture.SetItemTextureKeepSize(slotItem.get_ItemType());
		wnd.AddGuiElement(guiTexture);
		if (slotItem.isAccountBound)
		{
			GuiLabel guiLabel1 = new GuiLabel()
			{
				boundries = new Rect(15f, 100f, 270f, 12f),
				FontSize = 10,
				text = StaticData.Translate("key_tooltip_account_bound"),
				TextColor = GuiNewStyleBar.blueColor,
				Alignment = 3,
				Font = GuiLabel.FontBold
			};
			wnd.AddGuiElement(guiLabel1);
		}
		GuiLabel guiLabel2 = new GuiLabel()
		{
			boundries = new Rect(15f, 112f, 270f, 12f),
			Alignment = 3,
			Font = GuiLabel.FontBold,
			FontSize = 10,
			text = string.Format(StaticData.Translate("key_tooltip_level_restriction"), StaticData.allTypes.get_Item(slotItem.get_ItemType()).levelRestriction),
			TextColor = (StaticData.allTypes.get_Item(slotItem.get_ItemType()).levelRestriction <= NetworkScript.player.playerBelongings.playerLevel ? Color.get_white() : GuiNewStyleBar.redColor)
		};
		wnd.AddGuiElement(guiLabel2);
		if (PlayerItems.IsWeapon(slotItem.get_ItemType()))
		{
			int num = 10;
			int num1 = 115;
			int num2 = 170;
			GuiLabel guiLabel3 = new GuiLabel()
			{
				boundries = new Rect((float)num1, 32f, (float)num2, 12f),
				text = StaticData.Translate("key_inventory_damage"),
				FontSize = num,
				Alignment = 3
			};
			wnd.AddGuiElement(guiLabel3);
			GuiLabel guiLabel4 = new GuiLabel()
			{
				boundries = new Rect((float)num1, 46f, (float)num2, 12f),
				text = StaticData.Translate("key_inventory_cooldown"),
				FontSize = num,
				Alignment = 3
			};
			wnd.AddGuiElement(guiLabel4);
			GuiLabel guiLabel5 = new GuiLabel()
			{
				boundries = new Rect((float)num1, 60f, (float)num2, 12f),
				text = StaticData.Translate("key_inventory_range"),
				FontSize = num,
				Alignment = 3
			};
			wnd.AddGuiElement(guiLabel5);
			GuiLabel guiLabel6 = new GuiLabel()
			{
				boundries = new Rect((float)num1, 74f, (float)num2, 12f),
				text = StaticData.Translate("key_inventory_penetration"),
				FontSize = num,
				Alignment = 3
			};
			wnd.AddGuiElement(guiLabel6);
			GuiLabel guiLabel7 = new GuiLabel()
			{
				boundries = new Rect((float)num1, 88f, (float)num2, 12f),
				text = StaticData.Translate("key_inventory_targeting"),
				FontSize = num,
				Alignment = 3
			};
			wnd.AddGuiElement(guiLabel7);
			GuiLabel fontBold = new GuiLabel()
			{
				boundries = new Rect((float)num1, 32f, (float)num2, 12f)
			};
			int damageTotal = ((SlotItemWeapon)slotItem).get_DamageTotal();
			fontBold.text = damageTotal.ToString("##,##0");
			fontBold.FontSize = num;
			fontBold.Font = GuiLabel.FontBold;
			fontBold.TextColor = (slotItem.get_BonusOne() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			fontBold.Alignment = 5;
			wnd.AddGuiElement(fontBold);
			GuiLabel str1 = new GuiLabel()
			{
				boundries = new Rect((float)num1, 46f, (float)num2, 12f)
			};
			int cooldownTotal = ((SlotItemWeapon)slotItem).get_CooldownTotal();
			str1.text = cooldownTotal.ToString("##,##0");
			str1.FontSize = num;
			str1.Font = GuiLabel.FontBold;
			str1.TextColor = (slotItem.get_BonusThree() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			str1.Alignment = 5;
			wnd.AddGuiElement(str1);
			GuiLabel fontBold1 = new GuiLabel()
			{
				boundries = new Rect((float)num1, 60f, (float)num2, 12f)
			};
			int rangeTotal = ((SlotItemWeapon)slotItem).get_RangeTotal();
			fontBold1.text = rangeTotal.ToString("##,##0");
			fontBold1.FontSize = num;
			fontBold1.Font = GuiLabel.FontBold;
			fontBold1.TextColor = (slotItem.get_BonusTwo() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			fontBold1.Alignment = 5;
			wnd.AddGuiElement(fontBold1);
			GuiLabel str2 = new GuiLabel()
			{
				boundries = new Rect((float)num1, 74f, (float)num2, 12f)
			};
			int penetrationTotal = ((SlotItemWeapon)slotItem).get_PenetrationTotal();
			str2.text = penetrationTotal.ToString("##,##0");
			str2.FontSize = num;
			str2.Font = GuiLabel.FontBold;
			str2.TextColor = (slotItem.get_BonusFour() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			str2.Alignment = 5;
			wnd.AddGuiElement(str2);
			GuiLabel fontBold2 = new GuiLabel()
			{
				boundries = new Rect((float)num1, 88f, (float)num2, 12f)
			};
			int targetingTotal = ((SlotItemWeapon)slotItem).get_TargetingTotal();
			fontBold2.text = targetingTotal.ToString("##,##0");
			fontBold2.FontSize = num;
			fontBold2.Font = GuiLabel.FontBold;
			fontBold2.TextColor = (slotItem.get_BonusFive() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			fontBold2.Alignment = 5;
			wnd.AddGuiElement(fontBold2);
		}
		else if (PlayerItems.IsCorpus(slotItem.get_ItemType()))
		{
			int num3 = 10;
			int num4 = 115;
			int num5 = 170;
			GuiLabel guiLabel8 = new GuiLabel()
			{
				boundries = new Rect((float)num4, 60f, (float)num5, 12f),
				text = StaticData.Translate("key_inventory_corpus"),
				FontSize = num3,
				Alignment = 3
			};
			wnd.AddGuiElement(guiLabel8);
			GuiLabel str3 = new GuiLabel()
			{
				boundries = new Rect((float)num4, 60f, (float)num5, 12f)
			};
			int bonusOne = ((GeneratorNet)item).bonusValue + slotItem.get_BonusOne();
			str3.text = bonusOne.ToString("##,##0");
			str3.FontSize = num3;
			str3.Font = GuiLabel.FontBold;
			str3.TextColor = (slotItem.get_BonusOne() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			str3.Alignment = 5;
			wnd.AddGuiElement(str3);
		}
		else if (PlayerItems.IsShield(slotItem.get_ItemType()))
		{
			int num6 = 10;
			int num7 = 115;
			int num8 = 170;
			GuiLabel guiLabel9 = new GuiLabel()
			{
				boundries = new Rect((float)num7, 60f, (float)num8, 12f),
				text = StaticData.Translate("key_inventory_shield"),
				FontSize = num6,
				Alignment = 3
			};
			wnd.AddGuiElement(guiLabel9);
			GuiLabel fontBold3 = new GuiLabel()
			{
				boundries = new Rect((float)num7, 60f, (float)num8, 12f)
			};
			int bonusOne1 = ((GeneratorNet)item).bonusValue + slotItem.get_BonusOne();
			fontBold3.text = bonusOne1.ToString("##,##0");
			fontBold3.FontSize = num6;
			fontBold3.Font = GuiLabel.FontBold;
			fontBold3.TextColor = (slotItem.get_BonusOne() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			fontBold3.Alignment = 5;
			wnd.AddGuiElement(fontBold3);
		}
		else if (PlayerItems.IsEngine(slotItem.get_ItemType()))
		{
			int num9 = 10;
			int num10 = 115;
			int num11 = 170;
			GuiLabel guiLabel10 = new GuiLabel()
			{
				boundries = new Rect((float)num10, 60f, (float)num11, 12f),
				text = StaticData.Translate("key_inventory_speed"),
				FontSize = num9,
				Alignment = 3
			};
			wnd.AddGuiElement(guiLabel10);
			GuiLabel str4 = new GuiLabel()
			{
				boundries = new Rect((float)num10, 60f, (float)num11, 12f)
			};
			int bonusOne2 = ((GeneratorNet)item).bonusValue + slotItem.get_BonusOne();
			str4.text = bonusOne2.ToString("##,##0");
			str4.FontSize = num9;
			str4.Font = GuiLabel.FontBold;
			str4.TextColor = (slotItem.get_BonusOne() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			str4.Alignment = 5;
			wnd.AddGuiElement(str4);
		}
		else if (PlayerItems.IsExtra(slotItem.get_ItemType()))
		{
			int num12 = 10;
			int num13 = 115;
			int num14 = 170;
			GuiLabel guiLabel11 = new GuiLabel()
			{
				boundries = new Rect((float)num13, 54f, (float)num14, 24f),
				FontSize = num12,
				Alignment = 3
			};
			wnd.AddGuiElement(guiLabel11);
			GuiLabel guiLabel12 = new GuiLabel()
			{
				boundries = new Rect((float)num13, 54f, (float)num14, 24f),
				text = string.Empty,
				Font = GuiLabel.FontBold,
				Alignment = 5
			};
			wnd.AddGuiElement(guiLabel12);
			if (PlayerItems.IsForExtraCargoSpace(slotItem.get_ItemType()))
			{
				guiLabel11.text = StaticData.Translate("key_extra_cargo");
				guiLabel12.text = string.Format("+{0:##,##0}%", ((ExtrasNet)item).efValue + slotItem.get_BonusFour());
				guiLabel12.TextColor = (slotItem.get_BonusFour() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraLightMiningDrill || slotItem.get_ItemType() == PlayerItems.TypeExtraUltraMiningDrill)
			{
				guiLabel11.text = StaticData.Translate("key_extra_faster_mining");
				guiLabel12.text = string.Format("+{0:##,##0}%", ((ExtrasNet)item).efValue + slotItem.get_BonusFive());
				guiLabel12.TextColor = (slotItem.get_BonusFive() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			}
			else if (PlayerItems.IsForShieldRegen(slotItem.get_ItemType()))
			{
				guiLabel11.text = StaticData.Translate("key_extra_shield_regen");
				guiLabel12.text = string.Format("+{0:##,##0}", ((ExtrasNet)item).efValue + slotItem.get_BonusFive());
				guiLabel12.TextColor = (slotItem.get_BonusFive() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			}
			else if (PlayerItems.IsForLaserCooldown(slotItem.get_ItemType()))
			{
				guiLabel11.text = StaticData.Translate("key_extra_cooldown_laser");
				guiLabel12.text = string.Format("-{0:##,##0}", ((ExtrasNet)item).efValue + slotItem.get_BonusOne());
				guiLabel12.TextColor = (slotItem.get_BonusOne() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			}
			else if (PlayerItems.IsForPlasmaCooldown(slotItem.get_ItemType()))
			{
				guiLabel11.text = StaticData.Translate("key_extra_cooldown_plasma");
				guiLabel12.text = string.Format("-{0:##,##0}", ((ExtrasNet)item).efValue + slotItem.get_BonusTwo());
				guiLabel12.TextColor = (slotItem.get_BonusTwo() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			}
			else if (PlayerItems.IsForIonCooldown(slotItem.get_ItemType()))
			{
				guiLabel11.text = StaticData.Translate("key_extra_cooldown_ion");
				guiLabel12.text = string.Format("-{0:##,##0}", ((ExtrasNet)item).efValue + slotItem.get_BonusThree());
				guiLabel12.TextColor = (slotItem.get_BonusThree() != 0 ? GuiNewStyleBar.greenColor : Color.get_white());
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraUltraWeaponsCoolant)
			{
				guiLabel11.text = StaticData.Translate("key_extra_cooldown_all");
				guiLabel12.text = string.Format("-{0:##,##0}", ((ExtrasNet)item).efValue);
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraLaserWeaponsModule)
			{
				guiLabel11.text = StaticData.Translate("key_extra_dmg_laser");
				guiLabel12.text = string.Format("+{0:##,##0}%", ((ExtrasNet)item).efValue);
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraPlasmaWeaponsModule)
			{
				guiLabel11.text = StaticData.Translate("key_extra_dmg_plasma");
				guiLabel12.text = string.Format("+{0:##,##0}%", ((ExtrasNet)item).efValue);
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraIonWeaponsModule)
			{
				guiLabel11.text = StaticData.Translate("key_extra_dmg_ion");
				guiLabel12.text = string.Format("+{0:##,##0}%", ((ExtrasNet)item).efValue);
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraUltraWeaponsModule)
			{
				guiLabel11.text = StaticData.Translate("key_extra_dmg_all");
				guiLabel12.text = string.Format("+{0:##,##0}%", ((ExtrasNet)item).efValue);
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraLaserAimingCPU)
			{
				guiLabel11.text = StaticData.Translate("key_extra_targeting_laser");
				guiLabel12.text = string.Format("+{0:##,##0}", ((ExtrasNet)item).efValue);
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraPlasmaAimingCPU)
			{
				guiLabel11.text = StaticData.Translate("key_extra_targeting_plasma");
				guiLabel12.text = string.Format("+{0:##,##0}", ((ExtrasNet)item).efValue);
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraIonAimingCPU)
			{
				guiLabel11.text = StaticData.Translate("key_extra_targeting_ion");
				guiLabel12.text = string.Format("+{0:##,##0}", ((ExtrasNet)item).efValue);
			}
			else if (slotItem.get_ItemType() == PlayerItems.TypeExtraUltraAimingCPU)
			{
				guiLabel11.text = StaticData.Translate("key_extra_targeting_all");
				guiLabel12.text = string.Format("+{0:##,##0}", ((ExtrasNet)item).efValue);
			}
			guiLabel11.boundries.set_width((float)num14 - guiLabel12.TextWidth - 5f);
		}
		GuiLabel guiLabel13 = new GuiLabel()
		{
			boundries = new Rect(15f, 125f, 270f, 70f),
			TextColor = GuiNewStyleBar.greenColor,
			Font = GuiLabel.FontBold,
			FontSize = 10,
			Alignment = 3
		};
		wnd.AddGuiElement(guiLabel13);
		string empty = string.Empty;
		if (PlayerItems.IsWeapon(slotItem.get_ItemType()))
		{
			if (slotItem.get_BonusCnt() > 0)
			{
				if (slotItem.get_BonusOne() != 0)
				{
					string str5 = string.Format("+{0} {1}", slotItem.get_BonusOne(), StaticData.Translate("key_inventory_damage"));
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str5), "\n");
				}
				if (slotItem.get_BonusTwo() != 0)
				{
					string str6 = string.Format("+{0} {1}", slotItem.get_BonusTwo(), StaticData.Translate("key_inventory_range"));
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str6), "\n");
				}
				if (slotItem.get_BonusThree() != 0)
				{
					string str7 = string.Format("-{0} {1}", slotItem.get_BonusThree(), StaticData.Translate("key_inventory_cooldown"));
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str7), "\n");
				}
				if (slotItem.get_BonusFour() != 0)
				{
					string str8 = string.Format("+{0} {1}", slotItem.get_BonusFour(), StaticData.Translate("key_inventory_penetration"));
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str8), "\n");
				}
				if (slotItem.get_BonusFive() != 0)
				{
					string str9 = string.Format("+{0} {1}", slotItem.get_BonusFive(), StaticData.Translate("key_inventory_targeting"));
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str9), "\n");
				}
			}
		}
		else if (PlayerItems.IsCorpus(slotItem.get_ItemType()))
		{
			if (slotItem.get_BonusCnt() > 0)
			{
				if (slotItem.get_BonusOne() != 0)
				{
					string str10 = string.Format(StaticData.Translate("key_item_bonus_corpus"), slotItem.get_BonusOne());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str10), "\n");
				}
				if (slotItem.get_BonusTwo() != 0)
				{
					string str11 = string.Format(StaticData.Translate("key_item_bonus_corpusPercent"), slotItem.get_BonusTwo());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str11), "\n");
				}
				if (slotItem.get_BonusThree() != 0)
				{
					string str12 = string.Format(StaticData.Translate("key_item_bonus_targeting"), slotItem.get_BonusThree());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str12), "\n");
				}
				if (slotItem.get_BonusFour() != 0)
				{
					string str13 = string.Format(StaticData.Translate("key_item_bonus_avoidance"), slotItem.get_BonusFour());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str13), "\n");
				}
				if (slotItem.get_BonusFive() != 0)
				{
					string str14 = string.Format(StaticData.Translate("key_item_bonus_shieldRegen"), slotItem.get_BonusFive());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str14), "\n");
				}
			}
		}
		else if (PlayerItems.IsShield(slotItem.get_ItemType()))
		{
			if (slotItem.get_BonusCnt() > 0)
			{
				if (slotItem.get_BonusOne() != 0)
				{
					string str15 = string.Format(StaticData.Translate("key_item_bonus_shield"), slotItem.get_BonusOne());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str15), "\n");
				}
				if (slotItem.get_BonusTwo() != 0)
				{
					string str16 = string.Format(StaticData.Translate("key_item_bonus_shieldPercent"), slotItem.get_BonusTwo());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str16), "\n");
				}
				if (slotItem.get_BonusThree() != 0)
				{
					string str17 = string.Format(StaticData.Translate("key_item_bonus_targeting"), slotItem.get_BonusThree());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str17), "\n");
				}
				if (slotItem.get_BonusFour() != 0)
				{
					string str18 = string.Format(StaticData.Translate("key_item_bonus_avoidance"), slotItem.get_BonusFour());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str18), "\n");
				}
				if (slotItem.get_BonusFive() != 0)
				{
					string str19 = string.Format(StaticData.Translate("key_item_bonus_shieldRegen"), slotItem.get_BonusFive());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str19), "\n");
				}
			}
		}
		else if (PlayerItems.IsEngine(slotItem.get_ItemType()))
		{
			if (slotItem.get_BonusCnt() > 0)
			{
				if (slotItem.get_BonusOne() != 0)
				{
					string str20 = string.Format(StaticData.Translate("key_item_bonus_speed"), slotItem.get_BonusOne());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str20), "\n");
				}
				if (slotItem.get_BonusTwo() != 0)
				{
					string str21 = string.Format(StaticData.Translate("key_item_bonus_avoidance"), slotItem.get_BonusTwo());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str21), "\n");
				}
				if (slotItem.get_BonusThree() != 0)
				{
					string str22 = string.Format(StaticData.Translate("key_item_bonus_cargo"), slotItem.get_BonusThree());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str22), "\n");
				}
				if (slotItem.get_BonusFour() != 0)
				{
					string str23 = string.Format(StaticData.Translate("key_item_bonus_cargoPercent"), slotItem.get_BonusFour());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str23), "\n");
				}
				if (slotItem.get_BonusFive() != 0)
				{
					string str24 = string.Format(StaticData.Translate("key_item_bonus_shieldRegen"), slotItem.get_BonusFive());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str24), "\n");
				}
			}
		}
		else if (PlayerItems.IsExtraCooldown(slotItem.get_ItemType()))
		{
			if (slotItem.get_BonusCnt() > 0)
			{
				if (slotItem.get_BonusOne() != 0)
				{
					string str25 = string.Format(StaticData.Translate("key_item_bonus_laserCooldown"), slotItem.get_BonusOne());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str25), "\n");
				}
				if (slotItem.get_BonusTwo() != 0)
				{
					string str26 = string.Format(StaticData.Translate("key_item_bonus_plasmaCooldown"), slotItem.get_BonusTwo());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str26), "\n");
				}
				if (slotItem.get_BonusThree() != 0)
				{
					string str27 = string.Format(StaticData.Translate("key_item_bonus_ionCooldown"), slotItem.get_BonusThree());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str27), "\n");
				}
				if (slotItem.get_BonusFour() != 0)
				{
					string str28 = string.Format(StaticData.Translate("key_item_bonus_laserDmg"), slotItem.get_BonusFour());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str28), "\n");
				}
				if (slotItem.get_BonusFive() != 0)
				{
					string str29 = string.Format(StaticData.Translate("key_item_bonus_plasmaDmg"), slotItem.get_BonusFive());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str29), "\n");
				}
			}
		}
		else if (PlayerItems.IsExtraCargoMining(slotItem.get_ItemType()))
		{
			if (slotItem.get_BonusCnt() > 0)
			{
				if (slotItem.get_BonusOne() != 0)
				{
					string str30 = string.Format(StaticData.Translate("key_item_bonus_targeting"), slotItem.get_BonusOne());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str30), "\n");
				}
				if (slotItem.get_BonusTwo() != 0)
				{
					string str31 = string.Format(StaticData.Translate("key_item_bonus_avoidance"), slotItem.get_BonusTwo());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str31), "\n");
				}
				if (slotItem.get_BonusThree() != 0)
				{
					string str32 = string.Format(StaticData.Translate("key_item_bonus_cargo"), slotItem.get_BonusThree());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str32), "\n");
				}
				if (slotItem.get_BonusFour() != 0)
				{
					string str33 = string.Format(StaticData.Translate("key_item_bonus_cargoPercent"), slotItem.get_BonusFour());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str33), "\n");
				}
				if (slotItem.get_BonusFive() != 0)
				{
					string str34 = string.Format(StaticData.Translate("key_item_bonus_miningSpeed"), slotItem.get_BonusFive());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str34), "\n");
				}
			}
		}
		else if (PlayerItems.IsExtraDamage(slotItem.get_ItemType()))
		{
			if (slotItem.get_BonusCnt() > 0)
			{
				if (slotItem.get_BonusOne() != 0)
				{
					string str35 = string.Format(StaticData.Translate("key_item_bonus_targeting"), slotItem.get_BonusOne());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str35), "\n");
				}
				if (slotItem.get_BonusTwo() != 0)
				{
					string str36 = string.Format(StaticData.Translate("key_item_bonus_avoidance"), slotItem.get_BonusTwo());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str36), "\n");
				}
				if (slotItem.get_BonusThree() != 0)
				{
					string str37 = string.Format(StaticData.Translate("key_item_bonus_laserDmg"), slotItem.get_BonusThree());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str37), "\n");
				}
				if (slotItem.get_BonusFour() != 0)
				{
					string str38 = string.Format(StaticData.Translate("key_item_bonus_plasmaDmg"), slotItem.get_BonusFour());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str38), "\n");
				}
				if (slotItem.get_BonusFive() != 0)
				{
					string str39 = string.Format(StaticData.Translate("key_item_bonus_ionDmg"), slotItem.get_BonusFive());
					empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str39), "\n");
				}
			}
		}
		else if (PlayerItems.IsExtraOther(slotItem.get_ItemType()) && slotItem.get_BonusCnt() > 0)
		{
			if (slotItem.get_BonusOne() != 0)
			{
				string str40 = string.Format(StaticData.Translate("key_item_bonus_speed"), slotItem.get_BonusOne());
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str40), "\n");
			}
			if (slotItem.get_BonusTwo() != 0)
			{
				string str41 = string.Format(StaticData.Translate("key_item_bonus_targeting"), slotItem.get_BonusTwo());
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str41), "\n");
			}
			if (slotItem.get_BonusThree() != 0)
			{
				string str42 = string.Format(StaticData.Translate("key_item_bonus_avoidance"), slotItem.get_BonusThree());
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str42), "\n");
			}
			if (slotItem.get_BonusFour() != 0)
			{
				string str43 = string.Format(StaticData.Translate("key_item_bonus_miningSpeed"), slotItem.get_BonusFour());
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str43), "\n");
			}
			if (slotItem.get_BonusFive() != 0)
			{
				string str44 = string.Format(StaticData.Translate("key_item_bonus_shieldRegen"), slotItem.get_BonusFive());
				empty = string.Concat(empty, string.Format(StaticData.Translate("key_item_bonus_lbl"), str44), "\n");
			}
		}
		guiLabel13.text = empty;
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.SetSmallBlueTexture();
		guiButtonResizeable.X = 80f;
		guiButtonResizeable.Y = 190f;
		guiButtonResizeable.boundries.set_width(140f);
		guiButtonResizeable.Caption = StaticData.Translate("key_gambler_ok_button");
		guiButtonResizeable.FontSize = 15;
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(null, Inventory.CurrencyDialogRemove);
		guiButtonResizeable.Alignment = 4;
		wnd.AddGuiElement(guiButtonResizeable);
		AndromedaGui.gui.AddWindow(wnd);
		AndromedaGui.gui.activeToolTipId = wnd.handler;
	}

	public static void CreateWaitingDialog()
	{
		if (Inventory.dialogWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(Inventory.dialogWindow.handler);
			Inventory.dialogWindow = null;
		}
		Inventory.dialogWindow = new GuiWindow()
		{
			isModal = true
		};
		Inventory.dialogWindow.SetBackgroundTexture("ConfigWindow", "tooltipBackground");
		Inventory.dialogWindow.boundries.set_width(300f);
		Inventory.dialogWindow.boundries.set_height(220f);
		Inventory.dialogWindow.isHidden = false;
		Inventory.dialogWindow.zOrder = 220;
		Inventory.dialogWindow.PutToCenter();
		AndromedaGui.gui.AddWindow(Inventory.dialogWindow);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(0f, 0f, 300f, 220f),
			FontSize = 16,
			text = StaticData.Translate("key_ranking_receiving_data"),
			TextColor = GuiNewStyleBar.orangeColor,
			Alignment = 4,
			Font = GuiLabel.FontBold
		};
		Inventory.dialogWindow.AddGuiElement(guiLabel);
	}

	private static void CurrencyDialogClose(object prm)
	{
		AndromedaGui.gui.RemoveWindow(Inventory.dialogWindow.handler);
		Inventory.dialogWindow = null;
		Inventory.dialogItemSrc = null;
		Inventory.dialogItemDst = null;
	}

	private static void CurrencyDialogRemove(object prm)
	{
		AndromedaGui.gui.RemoveWindow(Inventory.dialogWindow.handler);
		AndromedaGui.gui.activeToolTipId = -1;
		Inventory.dialogWindow = null;
		if (Inventory.dialogItemSrc != null && (Inventory.dialogItemSrc.location == 16 || Inventory.dialogItemSrc.location == 17))
		{
			Inventory.CreateWaitingDialog();
		}
		Inventory.dialogItemSrc = null;
		Inventory.dialogItemDst = null;
	}

	private static void DeatachItemFromShip(object prm)
	{
		if (NetworkScript.player.vessel.galaxy.get_galaxyId() == 1000)
		{
			return;
		}
		if (((AndromedaGuiDragDropPlace)prm).item == null)
		{
			Debug.Log("CP I");
			return;
		}
		if (((AndromedaGuiDragDropPlace)prm).location == 1 || ((AndromedaGuiDragDropPlace)prm).location == 3 || ((AndromedaGuiDragDropPlace)prm).location == 2)
		{
			Debug.Log("CP III");
			return;
		}
		if (!NetworkScript.player.playerBelongings.HaveAFreeSlot())
		{
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 1)
			{
				((__ConfigWindow)AndromedaGui.mainWnd.activeWindow).ShowNoDestinationSlotFoundWarning();
			}
			return;
		}
		List<AndromedaGuiDragDropPlace> list = Inventory.places;
		if (Inventory.<>f__am$cache1A == null)
		{
			Inventory.<>f__am$cache1A = new Func<AndromedaGuiDragDropPlace, bool>(null, (AndromedaGuiDragDropPlace t) => (t.item != null || t.location != 1 ? false : t.isEmpty));
		}
		AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = Enumerable.FirstOrDefault<AndromedaGuiDragDropPlace>(Enumerable.Where<AndromedaGuiDragDropPlace>(list, Inventory.<>f__am$cache1A));
		if (andromedaGuiDragDropPlace != null)
		{
			if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
			{
				AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "button2");
				AudioManager.PlayGUISound(fromStaticSet);
			}
			Inventory.MoveItemInData((AndromedaGuiDragDropPlace)prm, andromedaGuiDragDropPlace);
			return;
		}
		if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
		{
			AudioClip audioClip = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "error");
			AudioManager.PlayGUISound(audioClip);
		}
		if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 1)
		{
			((__ConfigWindow)AndromedaGui.mainWnd.activeWindow).ShowNoDestinationSlotFoundWarning();
		}
	}

	public static void DetectDropZones(List<AndromedaGuiDragDropPlace> places, AndromedaGuiDragDropPlace place)
	{
		if (place == null)
		{
			return;
		}
		if (place.isEmpty)
		{
			return;
		}
		place.dropTargets.Clear();
		foreach (AndromedaGuiDragDropPlace andromedaGuiDragDropPlace in places)
		{
			if ((object)place != (object)andromedaGuiDragDropPlace)
			{
				if (!Inventory.ResolveMoveItemEnabled2(place, andromedaGuiDragDropPlace))
				{
					continue;
				}
				place.dropTargets.Add(andromedaGuiDragDropPlace);
			}
		}
	}

	public static GuiWindow DrawAmmoSwitchBtnToolTip(object prm)
	{
		AmmoSwichBtnInfo ammoSwichBtnInfo = (AmmoSwichBtnInfo)prm;
		GuiWindow guiWindow = new GuiWindow();
		guiWindow.SetBackgroundTexture("ConfigWindow", "mineralToolTip");
		float single = ammoSwichBtnInfo.possitionX - 110f;
		float single1 = ammoSwichBtnInfo.possitionY - 76f;
		if (ammoSwichBtnInfo.possitionY < (float)Screen.get_height() * 0.5f)
		{
			single1 = ammoSwichBtnInfo.possitionY + 22f;
		}
		guiWindow.boundries = new Rect(single, single1, guiWindow.boundries.get_width(), guiWindow.boundries.get_height());
		guiWindow.zOrder = 230;
		guiWindow.ignoreClickEvents = true;
		guiWindow.isHidden = false;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("ConfigWindow", "AvatarBox");
		guiTexture.X = 3f;
		guiTexture.Y = 3f;
		guiWindow.AddGuiElement(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture()
		{
			boundries = new Rect(3f, 18f, 64f, 44f)
		};
		guiTexture1.SetItemTextureKeepSize(ammoSwichBtnInfo.itemTypeId);
		guiWindow.AddGuiElement(guiTexture1);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(75f, 3f, 150f, 18f),
			Alignment = 4,
			text = StaticData.Translate(StaticData.allTypes.get_Item(ammoSwichBtnInfo.itemTypeId).uiName),
			TextColor = GuiNewStyleBar.blueColor
		};
		guiWindow.AddGuiElement(guiLabel);
		GuiLabel str = new GuiLabel()
		{
			boundries = new Rect(75f, 25f, 150f, 18f),
			Alignment = 4
		};
		long ammoQty = NetworkScript.player.playerBelongings.playerItems.GetAmmoQty(ammoSwichBtnInfo.itemTypeId);
		str.text = ammoQty.ToString("##,##0");
		str.TextColor = GuiNewStyleBar.orangeColor;
		guiWindow.AddGuiElement(str);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(65f, 50f, 60f, 20f),
			FontSize = 10,
			Alignment = 2,
			text = StaticData.Translate("key_inventory_damage"),
			WordWrap = false
		};
		guiWindow.AddGuiElement(guiLabel1);
		GuiNewStyleBar guiNewStyleBar = new GuiNewStyleBar()
		{
			X = 135f,
			Y = 50f
		};
		guiNewStyleBar.SetCustumSize(65, Color.get_white());
		guiNewStyleBar.maximum = (float)__VaultWindow._maxAmmoDamage;
		guiNewStyleBar.current = (float)((AmmoNet)StaticData.allTypes.get_Item(ammoSwichBtnInfo.itemTypeId)).damage;
		guiWindow.AddGuiElement(guiNewStyleBar);
		guiLabel1 = new GuiLabel()
		{
			boundries = new Rect(205f, 50f, 35f, 20f),
			FontSize = 10,
			Alignment = 0,
			text = string.Concat(((AmmoNet)StaticData.allTypes.get_Item(ammoSwichBtnInfo.itemTypeId)).damage.ToString("###,##0"), "%")
		};
		guiWindow.AddGuiElement(guiLabel1);
		return guiWindow;
	}

	public static void DrawPlaces(GuiWindow wnd)
	{
		foreach (AndromedaGuiDragDropPlace place in Inventory.places)
		{
			Inventory.DetectDropZones(Inventory.places, place);
			Inventory.DrawSinglePlace(wnd, place);
		}
	}

	public static void DrawPlaces(ItemLocation location, GuiWindow wnd)
	{
		foreach (AndromedaGuiDragDropPlace place in Inventory.places)
		{
			if (place.location == location)
			{
				Inventory.DetectDropZones(Inventory.places, place);
				Inventory.DrawSinglePlace(wnd, place);
			}
		}
	}

	public static void DrawPlaces()
	{
		foreach (AndromedaGuiDragDropPlace place in Inventory.places)
		{
			Inventory.DetectDropZones(Inventory.places, place);
			Inventory.DrawSinglePlace(Inventory.window, place);
		}
	}

	public static void DrawPlaces(ItemLocation location)
	{
		foreach (AndromedaGuiDragDropPlace place in Inventory.places)
		{
			if (place.location == location)
			{
				Inventory.DetectDropZones(Inventory.places, place);
				Inventory.DrawSinglePlace(Inventory.window, place);
			}
		}
	}

	public static void DrawSinglePlace(GuiWindow window, AndromedaGuiDragDropPlace place)
	{
		string str = null;
		if (place.txFrameIdle != null)
		{
			if (place.frameTexture == null)
			{
				GuiTexture guiTexture = new GuiTexture();
				guiTexture.SetTexture(place.txFrameIdle);
				guiTexture.boundries.set_x(place.position.x);
				guiTexture.boundries.set_y(place.position.y);
				place.frameTexture = guiTexture;
				window.AddGuiElement(guiTexture);
				if (place.location != 15 && place.location != 16 && place.item != null && place.item.get_BonusCnt() > 0 && (PlayerItems.IsEngine(place.item.get_ItemType()) || PlayerItems.IsExtra(place.item.get_ItemType()) || PlayerItems.IsCorpus(place.item.get_ItemType()) || PlayerItems.IsShield(place.item.get_ItemType()) || PlayerItems.IsWeapon(place.item.get_ItemType())))
				{
					string empty = string.Empty;
					switch (place.item.get_BonusCnt())
					{
						case 0:
						{
							empty = string.Concat(place.glowTexturePrefix, "White");
							break;
						}
						case 1:
						case 2:
						{
							empty = string.Concat(place.glowTexturePrefix, "Blue");
							break;
						}
						case 3:
						case 4:
						{
							empty = string.Concat(place.glowTexturePrefix, "Orange");
							break;
						}
						case 5:
						{
							empty = string.Concat(place.glowTexturePrefix, "Purple");
							break;
						}
					}
					GuiTexture guiTexture1 = new GuiTexture();
					guiTexture1.SetTexture("ConfigWindow", empty);
					guiTexture1.boundries.set_x(place.position.x);
					guiTexture1.boundries.set_y(place.position.y);
					place.glowTexture = guiTexture1;
					window.AddGuiElement(guiTexture1);
				}
			}
			if (!place.isEmpty)
			{
				PlayerItems.SetAvatar(place.item.get_ItemType(), ref place.assetBundle, ref str);
				place.txItem = new GuiTextureDraggable();
				place.txItem.SetTextureSource(place.txFrameDragSource);
				place.txItem.SetTextureDrag(place.assetBundle, place.item.AssetName());
				place.txItem.SetTexture(place.assetBundle, place.item.AssetName());
				place.txItem.drawTooltipWindowCallback = new GuiElement.DrawTooltipWindow(place.ItemInfoTooltip2);
				place.txItem.isUnavailable = (place.location != 15 ? false : !Inventory.CanPlayerBuyThisItem(place.item.get_ItemType(), place.item.get_Amount()));
				if (place.item != null && Inventory.isRightClickActionEnable)
				{
					place.txItem.RightClickAction = new Action<object>(null, Inventory.ManageAttachDeatachItem);
					place.txItem.rightClickActionParam = place;
				}
				if (place.idleItemTextureSize.x != 0f)
				{
					place.txItem.SetSize(place.idleItemTextureSize.x, place.idleItemTextureSize.y);
				}
				place.txItem.boundries.set_x(place.position.x);
				place.txItem.boundries.set_y(place.position.y);
				Inventory.AddDropZonesToTexture(place);
				if (place.dropTargets.get_Count() > 0)
				{
					place.txItem.dropped = new Action<int, int>(null, Inventory.OnDropped);
				}
				window.AddGuiElement(place.txItem);
				if (PlayerItems.IsStackable(place.item.get_ItemType()) || place.item.get_SlotType() == 3)
				{
					place.lblAmount = new GuiLabel();
					place.lblAmount.boundries.set_x(place.position.x + 6f);
					place.lblAmount.boundries.set_y(place.position.y + 36f);
					place.lblAmount.boundries.set_width(100f);
					place.lblAmount.boundries.set_height(32f);
					place.lblAmount.text = place.item.get_Amount().ToString();
					window.AddGuiElement(place.lblAmount);
				}
			}
		}
	}

	private static void DrawStackablePopup(AndromedaGuiDragDropPlace src, AndromedaGuiDragDropPlace dst)
	{
		float amount;
		object obj;
		if (Inventory.stackableWnd != null)
		{
			AndromedaGui.gui.RemoveWindow(Inventory.stackableWnd.handler);
			Inventory.stackableWnd = null;
		}
		Inventory.stackableWnd = new GuiWindow();
		Inventory.stackableWnd.SetBackgroundTexture("ConfigWindow", "tooltipBackground");
		Inventory.stackableWnd.PutToCenter();
		Inventory.stackableWnd.zOrder = 240;
		Inventory.stackableWnd.isModal = true;
		Inventory.stackableWnd.isHidden = false;
		GuiButtonResizeable guiButtonResizeable = new GuiButtonResizeable();
		guiButtonResizeable.SetBlueTexture();
		guiButtonResizeable.Width = 115f;
		guiButtonResizeable.Caption = StaticData.Translate("key_dialog_cancel");
		guiButtonResizeable.FontSize = 12;
		guiButtonResizeable.Alignment = 4;
		guiButtonResizeable.X = 25f;
		guiButtonResizeable.Y = 170f;
		guiButtonResizeable.Clicked = new Action<EventHandlerParam>(null, Inventory.OnCancelStackableDrop);
		guiButtonResizeable.eventHandlerParam.customData = src;
		guiButtonResizeable.eventHandlerParam.customData2 = dst;
		guiButtonResizeable.isEnabled = true;
		Inventory.stackableWnd.AddGuiElement(guiButtonResizeable);
		GuiButtonResizeable action = new GuiButtonResizeable();
		action.SetBlueTexture();
		action.Width = 115f;
		action.Caption = StaticData.Translate("key_dialog_ok");
		action.FontSize = 12;
		action.Alignment = 4;
		action.X = 160f;
		action.Y = 170f;
		action.Clicked = new Action<EventHandlerParam>(null, Inventory.OnContinueStackableDrop);
		action.eventHandlerParam.customData = src;
		action.eventHandlerParam.customData2 = dst;
		Inventory.stackableWnd.AddGuiElement(action);
		GuiLabel guiLabel = new GuiLabel()
		{
			text = src.item.UIName(),
			FontSize = 14,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.orangeColor,
			boundries = new Rect(0f, 5f, 300f, 20f),
			Alignment = 4
		};
		Inventory.stackableWnd.AddGuiElement(guiLabel);
		GuiLabel guiLabel1 = new GuiLabel()
		{
			text = StaticData.Translate("key_stack_items_select_amount"),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = Color.get_white(),
			boundries = new Rect(0f, 100f, 300f, 20f),
			Alignment = 4
		};
		Inventory.stackableWnd.AddGuiElement(guiLabel1);
		Inventory.stackItemAmount = new GuiLabel()
		{
			text = string.Format("{0}/{1}", 1, src.item.get_Amount()),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = Color.get_white(),
			boundries = new Rect(0f, 145f, 300f, 14f),
			Alignment = 4
		};
		Inventory.stackableWnd.AddGuiElement(Inventory.stackItemAmount);
		Inventory.sellPriceOfStackableItem = (float)PlayerItems.CalculateSellPrice(src.item.get_ItemType(), NetworkScript.player.cfg.sellBonus);
		Inventory.isAmmoSelecktedStackableItem = false;
		if (!PlayerItems.IsAmmo(src.item.get_ItemType()))
		{
			amount = (float)src.item.get_Amount() * Inventory.sellPriceOfStackableItem;
		}
		else
		{
			Inventory.isAmmoSelecktedStackableItem = true;
			amount = (float)src.item.get_Amount() / 100f * Inventory.sellPriceOfStackableItem;
		}
		Inventory.stackItemSellPrice = new GuiLabel()
		{
			text = string.Concat(StaticData.Translate("key_inventory_sell_price"), ": $", amount.ToString()),
			FontSize = 12,
			Font = GuiLabel.FontBold,
			TextColor = GuiNewStyleBar.blueColor,
			boundries = new Rect(0f, 160f, 300f, 12f),
			Alignment = 4
		};
		Inventory.stackableWnd.AddGuiElement(Inventory.stackItemSellPrice);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture(src.txItem.GetTextureSource());
		guiTexture.X = 95f;
		guiTexture.Y = 27f;
		guiTexture.boundries.set_width(111f);
		guiTexture.boundries.set_height(76f);
		Inventory.stackableWnd.AddGuiElement(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture()
		{
			boundries = new Rect(89f, 133f, 122f, 3f)
		};
		guiTexture1.SetTextureKeepSize("NewGUI", "ResourceSlideLine");
		Inventory.stackableWnd.AddGuiElement(guiTexture1);
		GuiButtonFixed guiButtonFixed = new GuiButtonFixed();
		guiButtonFixed.SetTexture("NewGUI", "btnSliderMinus");
		guiButtonFixed.X = 64f;
		guiButtonFixed.Y = 127f;
		guiButtonFixed.Caption = string.Empty;
		guiButtonFixed.Clicked = new Action<EventHandlerParam>(null, Inventory.OnMinusBtnClicked);
		guiButtonFixed.SetLeftClickSound("Sounds", "minus");
		Inventory.stackableWnd.AddGuiElement(guiButtonFixed);
		GuiButtonFixed empty = new GuiButtonFixed();
		empty.SetTexture("NewGUI", "btnSliderPlus");
		empty.X = 211f;
		empty.Y = 127f;
		empty.Caption = string.Empty;
		empty.Clicked = new Action<EventHandlerParam>(null, Inventory.OnPlusBtnClicked);
		empty.SetLeftClickSound("Sounds", "plus");
		Inventory.stackableWnd.AddGuiElement(empty);
		Inventory.sliderStack = new GuiHorizontalSlider();
		GuiHorizontalSlider guiHorizontalSlider = Inventory.sliderStack;
		if (dst.isEmpty || dst.location == 15)
		{
			obj = src.item.get_Amount();
		}
		else
		{
			obj = (dst.item.get_Amount() + src.item.get_Amount() <= 5000 ? src.item.get_Amount() : 5000 - dst.item.get_Amount());
		}
		guiHorizontalSlider.MAX = (float)obj;
		Inventory.sliderStack.MIN = 1f;
		Inventory.sliderStack.Width = 100f;
		Inventory.sliderStack.CurrentValue = Inventory.sliderStack.MAX;
		Inventory.sliderStack.X = 100f;
		Inventory.sliderStack.Y = 130f;
		Inventory.sliderStack.SetSliderTumb("NewGUI", "resSliderThumb");
		Inventory.sliderStack.SetEmptySliderTexture();
		Inventory.sliderStack.refreshData = new Action(null, Inventory.OnAmountChange);
		Inventory.stackableWnd.AddGuiElement(Inventory.sliderStack);
		AndromedaGui.gui.AddWindow(Inventory.stackableWnd);
		AndromedaGui.gui.activeToolTipId = Inventory.stackableWnd.handler;
		AndromedaGui.gui.activeTooltipCloseAction = Inventory.closeStackablePopUp;
	}

	private static int GetDropZoneKey(AndromedaGuiDragDropPlace src, AndromedaGuiDragDropPlace dst)
	{
		return dst.id + (src.id << 16);
	}

	private static byte GetSlotTypeFromLocationType(ItemLocation loc)
	{
		return (byte)loc;
	}

	private static bool HasCPUforExtras()
	{
		return false;
	}

	private static bool HaveAFreeGuildVaultSlot(out int slotId)
	{
		Inventory.<HaveAFreeGuildVaultSlot>c__AnonStorey2E variable = null;
		slotId = -1;
		if (NetworkScript.player.guild != null && NetworkScript.player.guild.guildItems.get_Count() < NetworkScript.player.guild.vaultSlots)
		{
			for (int i = 0; i < NetworkScript.player.guild.vaultSlots; i++)
			{
				if (Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(NetworkScript.player.guild.guildItems, new Func<SlotItem, bool>(variable, (SlotItem t) => (t.get_SlotType() != 18 ? false : t.get_Slot() == this.i)))) == null)
				{
					slotId = i;
					return true;
				}
			}
		}
		return false;
	}

	private static bool IsCargoItem(ushort itemType)
	{
		return PlayerItems.IsMineral(itemType);
	}

	private static bool IsItemCPUforExtras(ushort itemType)
	{
		return false;
	}

	public static bool IsStructureItem(ushort itemType)
	{
		return (PlayerItems.IsEngine(itemType) || PlayerItems.IsShield(itemType) ? true : PlayerItems.IsCorpus(itemType));
	}

	public static void ItemRarity(SlotItem item, out string name, out Color itemColor)
	{
		name = string.Empty;
		itemColor = Color.get_white();
		if (item == null)
		{
			return;
		}
		string str = StaticData.Translate(StaticData.allTypes.get_Item(item.get_ItemType()).uiName);
		switch (item.get_BonusCnt())
		{
			case 0:
			{
				itemColor = Color.get_white();
				name = str;
				break;
			}
			case 1:
			case 2:
			{
				itemColor = GuiNewStyleBar.blueColor;
				if (PlayerItems.IsWeapon(item.get_ItemType()))
				{
					name = string.Format(StaticData.Translate("key_weapon_modified"), str);
				}
				else if (PlayerItems.IsShield(item.get_ItemType()))
				{
					name = string.Format(StaticData.Translate("key_shield_modified"), str);
				}
				else if (PlayerItems.IsCorpus(item.get_ItemType()))
				{
					name = string.Format(StaticData.Translate("key_corpus_modified"), str);
				}
				else if (PlayerItems.IsEngine(item.get_ItemType()))
				{
					name = string.Format(StaticData.Translate("key_engine_modified"), str);
				}
				else if (item.get_ItemType() == PlayerItems.TypeExtraLightMiningDrill || item.get_ItemType() == PlayerItems.TypeExtraUltraMiningDrill)
				{
					name = string.Format(StaticData.Translate("key_extra_drill_modified"), str);
				}
				else if (PlayerItems.IsForExtraCargoSpace(item.get_ItemType()))
				{
					name = string.Format(StaticData.Translate("key_extra_compressor_modified"), str);
				}
				else if (item.get_ItemType() == PlayerItems.TypeExtraLaserWeaponsModule || item.get_ItemType() == PlayerItems.TypeExtraPlasmaWeaponsModule || item.get_ItemType() == PlayerItems.TypeExtraIonWeaponsModule || item.get_ItemType() == PlayerItems.TypeExtraUltraWeaponsModule)
				{
					name = string.Format(StaticData.Translate("key_extra_module_modified"), str);
				}
				else if (PlayerItems.IsExtraCooldown(item.get_ItemType()))
				{
					name = string.Format(StaticData.Translate("key_extra_coolant_modified"), str);
				}
				else if (item.get_ItemType() == PlayerItems.TypeExtraLaserAimingCPU || item.get_ItemType() == PlayerItems.TypeExtraPlasmaAimingCPU || item.get_ItemType() == PlayerItems.TypeExtraIonAimingCPU || item.get_ItemType() == PlayerItems.TypeExtraUltraAimingCPU)
				{
					name = string.Format(StaticData.Translate("key_extra_aiming_modified"), str);
				}
				else if (!PlayerItems.IsForShieldRegen(item.get_ItemType()))
				{
					name = str;
				}
				else
				{
					name = string.Format(StaticData.Translate("key_extra_regen_modified"), str);
				}
				break;
			}
			case 3:
			case 4:
			{
				itemColor = GuiNewStyleBar.orangeColor;
				if (PlayerItems.IsWeapon(item.get_ItemType()))
				{
					name = string.Format(StaticData.Translate("key_weapon_tuned"), str);
				}
				else if (PlayerItems.IsShield(item.get_ItemType()))
				{
					name = string.Format(StaticData.Translate("key_shield_tuned"), str);
				}
				else if (PlayerItems.IsCorpus(item.get_ItemType()))
				{
					name = string.Format(StaticData.Translate("key_corpus_tuned"), str);
				}
				else if (PlayerItems.IsEngine(item.get_ItemType()))
				{
					name = string.Format(StaticData.Translate("key_engine_tuned"), str);
				}
				else if (item.get_ItemType() == PlayerItems.TypeExtraLightMiningDrill || item.get_ItemType() == PlayerItems.TypeExtraUltraMiningDrill)
				{
					name = string.Format(StaticData.Translate("key_extra_drill_tuned"), str);
				}
				else if (PlayerItems.IsForExtraCargoSpace(item.get_ItemType()))
				{
					name = string.Format(StaticData.Translate("key_extra_compressor_tuned"), str);
				}
				else if (item.get_ItemType() == PlayerItems.TypeExtraLaserWeaponsModule || item.get_ItemType() == PlayerItems.TypeExtraPlasmaWeaponsModule || item.get_ItemType() == PlayerItems.TypeExtraIonWeaponsModule || item.get_ItemType() == PlayerItems.TypeExtraUltraWeaponsModule)
				{
					name = string.Format(StaticData.Translate("key_extra_module_tuned"), str);
				}
				else if (PlayerItems.IsExtraCooldown(item.get_ItemType()))
				{
					name = string.Format(StaticData.Translate("key_extra_coolant_tuned"), str);
				}
				else if (item.get_ItemType() == PlayerItems.TypeExtraLaserAimingCPU || item.get_ItemType() == PlayerItems.TypeExtraPlasmaAimingCPU || item.get_ItemType() == PlayerItems.TypeExtraIonAimingCPU || item.get_ItemType() == PlayerItems.TypeExtraUltraAimingCPU)
				{
					name = string.Format(StaticData.Translate("key_extra_aiming_tuned"), str);
				}
				else if (!PlayerItems.IsForShieldRegen(item.get_ItemType()))
				{
					name = str;
				}
				else
				{
					name = string.Format(StaticData.Translate("key_extra_regen_tuned"), str);
				}
				break;
			}
			case 5:
			{
				itemColor = GuiNewStyleBar.darkPurpuleColor;
				if (PlayerItems.IsWeapon(item.get_ItemType()))
				{
					name = string.Format(StaticData.Translate("key_weapon_experimental"), str);
				}
				else if (PlayerItems.IsShield(item.get_ItemType()))
				{
					name = string.Format(StaticData.Translate("key_shield_experimental"), str);
				}
				else if (PlayerItems.IsCorpus(item.get_ItemType()))
				{
					name = string.Format(StaticData.Translate("key_corpus_experimental"), str);
				}
				else if (PlayerItems.IsEngine(item.get_ItemType()))
				{
					name = string.Format(StaticData.Translate("key_engine_experimental"), str);
				}
				else if (item.get_ItemType() == PlayerItems.TypeExtraLightMiningDrill || item.get_ItemType() == PlayerItems.TypeExtraUltraMiningDrill)
				{
					name = string.Format(StaticData.Translate("key_extra_drill_experimental"), str);
				}
				else if (PlayerItems.IsForExtraCargoSpace(item.get_ItemType()))
				{
					name = string.Format(StaticData.Translate("key_extra_compressor_experimental"), str);
				}
				else if (item.get_ItemType() == PlayerItems.TypeExtraLaserWeaponsModule || item.get_ItemType() == PlayerItems.TypeExtraPlasmaWeaponsModule || item.get_ItemType() == PlayerItems.TypeExtraIonWeaponsModule || item.get_ItemType() == PlayerItems.TypeExtraUltraWeaponsModule)
				{
					name = string.Format(StaticData.Translate("key_extra_module_experimental"), str);
				}
				else if (PlayerItems.IsExtraCooldown(item.get_ItemType()))
				{
					name = string.Format(StaticData.Translate("key_extra_coolant_experimental"), str);
				}
				else if (item.get_ItemType() == PlayerItems.TypeExtraLaserAimingCPU || item.get_ItemType() == PlayerItems.TypeExtraPlasmaAimingCPU || item.get_ItemType() == PlayerItems.TypeExtraIonAimingCPU || item.get_ItemType() == PlayerItems.TypeExtraUltraAimingCPU)
				{
					name = string.Format(StaticData.Translate("key_extra_aiming_experimental"), str);
				}
				else if (!PlayerItems.IsForShieldRegen(item.get_ItemType()))
				{
					name = str;
				}
				else
				{
					name = string.Format(StaticData.Translate("key_extra_regen_experimental"), str);
				}
				break;
			}
		}
	}

	public static void ManageAttachDeatachItem(object prm)
	{
		if (((AndromedaGuiDragDropPlace)prm).item == null)
		{
			Debug.Log("CP I");
			return;
		}
		if (((AndromedaGuiDragDropPlace)prm).location == 1 && AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 22)
		{
			Inventory.MovePortalPartFromInventoryToTransformer(prm);
			return;
		}
		if (((AndromedaGuiDragDropPlace)prm).location == 1 && Inventory.isItemRerollMenuOpen)
		{
			if (((AndromedaGuiDragDropPlace)prm).item.get_BonusCnt() > 0)
			{
				if (Inventory.lastSelectedPlace != null)
				{
					Inventory.lastSelectedPlace.frameTexture.SetTexture("ConfigWindow", "InventorySlotIdle");
				}
				((AndromedaGuiDragDropPlace)prm).frameTexture.SetTexture("ConfigWindow", "InventorySlotActive");
				Inventory.lastSelectedPlace = (AndromedaGuiDragDropPlace)prm;
				Inventory.SelectItemForBonusReroll(prm);
			}
			else if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
			{
				AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "error");
				AudioManager.PlayGUISound(fromStaticSet);
			}
			return;
		}
		if (((AndromedaGuiDragDropPlace)prm).location == 1 && Inventory.isGuildVaultMenuOpen)
		{
			Inventory.MoveItemFromInventoryToGuildVault(prm);
			return;
		}
		if (((AndromedaGuiDragDropPlace)prm).location == 18 && Inventory.isGuildVaultMenuOpen)
		{
			Inventory.MoveItemFromGuildVaultToInventory(prm);
			return;
		}
		if (((AndromedaGuiDragDropPlace)prm).location == 1 && Inventory.isVaultMenuOpen)
		{
			Inventory.MoveItemFromInventoryToVault(prm);
			return;
		}
		if (((AndromedaGuiDragDropPlace)prm).location == 2 && Inventory.isVaultMenuOpen)
		{
			Inventory.MoveItemFromVaultToInventory(prm);
			return;
		}
		if (((AndromedaGuiDragDropPlace)prm).location == 1)
		{
			Inventory.AtachItemToShip(prm);
			return;
		}
		if (((AndromedaGuiDragDropPlace)prm).location != 1 && ((AndromedaGuiDragDropPlace)prm).location != 3 && ((AndromedaGuiDragDropPlace)prm).location != 2 && ((AndromedaGuiDragDropPlace)prm).location != 15 && ((AndromedaGuiDragDropPlace)prm).location != 17 && ((AndromedaGuiDragDropPlace)prm).location != 16)
		{
			Inventory.DeatachItemFromShip(prm);
			return;
		}
		if (((AndromedaGuiDragDropPlace)prm).location == 15)
		{
			Inventory.BuyItemFromMerchantToInventory(prm);
			return;
		}
		if (((AndromedaGuiDragDropPlace)prm).location != 16 && ((AndromedaGuiDragDropPlace)prm).location != 17)
		{
			return;
		}
		Inventory.BuyItemFromGamblerToInventory(prm);
	}

	private static void MoveItemFromGuildVaultToInventory(object prm)
	{
		AndromedaGuiDragDropPlace andromedaGuiDragDropPlace;
		Inventory.<MoveItemFromGuildVaultToInventory>c__AnonStorey2D variable = null;
		if (((AndromedaGuiDragDropPlace)prm).item == null)
		{
			Debug.Log("CP I");
			return;
		}
		if (((AndromedaGuiDragDropPlace)prm).location != 18 || StaticData.allTypes.get_Item(((AndromedaGuiDragDropPlace)prm).item.get_ItemType()).levelRestriction > NetworkScript.player.playerBelongings.playerLevel)
		{
			if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
			{
				AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "error");
				AudioManager.PlayGUISound(fromStaticSet);
			}
			Debug.Log("CP III");
			return;
		}
		if (PlayerItems.IsStackable(((AndromedaGuiDragDropPlace)prm).item.get_ItemType()))
		{
			andromedaGuiDragDropPlace = Enumerable.FirstOrDefault<AndromedaGuiDragDropPlace>(Enumerable.Where<AndromedaGuiDragDropPlace>(Inventory.places, new Func<AndromedaGuiDragDropPlace, bool>(variable, (AndromedaGuiDragDropPlace t) => (t.isEmpty || t.location != 1 || t.item.get_ItemType() != ((AndromedaGuiDragDropPlace)this.prm).item.get_ItemType() ? false : t.item.get_Amount() < 5000))));
			if (andromedaGuiDragDropPlace != null)
			{
				Inventory.StackItems((AndromedaGuiDragDropPlace)prm, andromedaGuiDragDropPlace, Math.Min(5000 - andromedaGuiDragDropPlace.item.get_Amount(), ((AndromedaGuiDragDropPlace)prm).item.get_Amount()));
				return;
			}
		}
		if (!NetworkScript.player.playerBelongings.HaveAFreeSlot())
		{
			Debug.Log("CP IV");
			return;
		}
		NetworkScript.player.playerBelongings.FirstFreeInventorySlot();
		andromedaGuiDragDropPlace = Enumerable.FirstOrDefault<AndromedaGuiDragDropPlace>(Enumerable.Where<AndromedaGuiDragDropPlace>(Inventory.places, new Func<AndromedaGuiDragDropPlace, bool>(variable, (AndromedaGuiDragDropPlace t) => (t.item != null || t.location != 1 ? false : t.slot == this.freeInventorySlotId))));
		if (andromedaGuiDragDropPlace == null)
		{
			if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
			{
				AudioClip audioClip = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "error");
				AudioManager.PlayGUISound(audioClip);
			}
			Debug.Log("CP V");
			return;
		}
		if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
		{
			AudioClip fromStaticSet1 = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "button2");
			AudioManager.PlayGUISound(fromStaticSet1);
		}
		Inventory.MoveItemGuildVault((AndromedaGuiDragDropPlace)prm, andromedaGuiDragDropPlace);
	}

	private static void MoveItemFromInventoryToGuildVault(object prm)
	{
		AndromedaGuiDragDropPlace andromedaGuiDragDropPlace;
		Inventory.<MoveItemFromInventoryToGuildVault>c__AnonStorey2C variable = null;
		int num;
		if (((AndromedaGuiDragDropPlace)prm).item == null)
		{
			Debug.Log("CP I");
			return;
		}
		if (((AndromedaGuiDragDropPlace)prm).location != 1)
		{
			Debug.Log("CP III");
			return;
		}
		if (PlayerItems.IsStackable(((AndromedaGuiDragDropPlace)prm).item.get_ItemType()))
		{
			andromedaGuiDragDropPlace = Enumerable.FirstOrDefault<AndromedaGuiDragDropPlace>(Enumerable.Where<AndromedaGuiDragDropPlace>(Inventory.places, new Func<AndromedaGuiDragDropPlace, bool>(variable, (AndromedaGuiDragDropPlace t) => (t.isEmpty || t.location != 18 || t.item.get_ItemType() != ((AndromedaGuiDragDropPlace)this.prm).item.get_ItemType() ? false : t.item.get_Amount() < 5000))));
			if (andromedaGuiDragDropPlace != null)
			{
				Inventory.StackItems((AndromedaGuiDragDropPlace)prm, andromedaGuiDragDropPlace, Math.Min(5000 - andromedaGuiDragDropPlace.item.get_Amount(), ((AndromedaGuiDragDropPlace)prm).item.get_Amount()));
				return;
			}
		}
		if (!Inventory.HaveAFreeGuildVaultSlot(out num))
		{
			if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
			{
				AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "error");
				AudioManager.PlayGUISound(fromStaticSet);
			}
			Debug.Log("CP IV");
			return;
		}
		andromedaGuiDragDropPlace = Enumerable.FirstOrDefault<AndromedaGuiDragDropPlace>(Enumerable.Where<AndromedaGuiDragDropPlace>(Inventory.places, new Func<AndromedaGuiDragDropPlace, bool>(variable, (AndromedaGuiDragDropPlace t) => (t.item != null || t.location != 18 ? false : t.slot == this.freeGuildVaultSlotId))));
		if (andromedaGuiDragDropPlace == null)
		{
			if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
			{
				AudioClip audioClip = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "error");
				AudioManager.PlayGUISound(audioClip);
			}
			Debug.Log("CP V");
			return;
		}
		if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
		{
			AudioClip fromStaticSet1 = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "button2");
			AudioManager.PlayGUISound(fromStaticSet1);
		}
		Inventory.MoveItemGuildVault((AndromedaGuiDragDropPlace)prm, andromedaGuiDragDropPlace);
	}

	private static void MoveItemFromInventoryToVault(object prm)
	{
		AndromedaGuiDragDropPlace andromedaGuiDragDropPlace;
		Inventory.<MoveItemFromInventoryToVault>c__AnonStorey2F variable = null;
		if (((AndromedaGuiDragDropPlace)prm).item == null)
		{
			Debug.Log("CP I");
			return;
		}
		if (((AndromedaGuiDragDropPlace)prm).location != 1)
		{
			Debug.Log("CP III");
			return;
		}
		if (PlayerItems.IsStackable(((AndromedaGuiDragDropPlace)prm).item.get_ItemType()))
		{
			andromedaGuiDragDropPlace = Enumerable.FirstOrDefault<AndromedaGuiDragDropPlace>(Enumerable.Where<AndromedaGuiDragDropPlace>(Inventory.places, new Func<AndromedaGuiDragDropPlace, bool>(variable, (AndromedaGuiDragDropPlace t) => (t.isEmpty || t.location != 2 || t.item.get_ItemType() != ((AndromedaGuiDragDropPlace)this.prm).item.get_ItemType() ? false : t.item.get_Amount() < 5000))));
			if (andromedaGuiDragDropPlace != null)
			{
				Inventory.StackItems((AndromedaGuiDragDropPlace)prm, andromedaGuiDragDropPlace, Math.Min(5000 - andromedaGuiDragDropPlace.item.get_Amount(), ((AndromedaGuiDragDropPlace)prm).item.get_Amount()));
				return;
			}
		}
		if (!NetworkScript.player.playerBelongings.HaveAFreeVaultSlot())
		{
			if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
			{
				AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "error");
				AudioManager.PlayGUISound(fromStaticSet);
			}
			Debug.Log("CP IV");
			return;
		}
		NetworkScript.player.playerBelongings.FirstFreeVaultSlot();
		andromedaGuiDragDropPlace = Enumerable.FirstOrDefault<AndromedaGuiDragDropPlace>(Enumerable.Where<AndromedaGuiDragDropPlace>(Inventory.places, new Func<AndromedaGuiDragDropPlace, bool>(variable, (AndromedaGuiDragDropPlace t) => (t.item != null || t.location != 2 ? false : t.slot == this.freeVaultSlotId))));
		if (andromedaGuiDragDropPlace == null)
		{
			if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
			{
				AudioClip audioClip = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "error");
				AudioManager.PlayGUISound(audioClip);
			}
			Debug.Log("CP V");
			return;
		}
		if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
		{
			AudioClip fromStaticSet1 = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "button2");
			AudioManager.PlayGUISound(fromStaticSet1);
		}
		Inventory.MoveItemInData((AndromedaGuiDragDropPlace)prm, andromedaGuiDragDropPlace);
	}

	private static void MoveItemFromVaultToInventory(object prm)
	{
		AndromedaGuiDragDropPlace andromedaGuiDragDropPlace;
		Inventory.<MoveItemFromVaultToInventory>c__AnonStorey2A variable = null;
		if (((AndromedaGuiDragDropPlace)prm).item == null)
		{
			Debug.Log("CP I");
			return;
		}
		if (((AndromedaGuiDragDropPlace)prm).location != 2)
		{
			if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
			{
				AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "error");
				AudioManager.PlayGUISound(fromStaticSet);
			}
			Debug.Log("CP III");
			return;
		}
		if (PlayerItems.IsStackable(((AndromedaGuiDragDropPlace)prm).item.get_ItemType()))
		{
			andromedaGuiDragDropPlace = Enumerable.FirstOrDefault<AndromedaGuiDragDropPlace>(Enumerable.Where<AndromedaGuiDragDropPlace>(Inventory.places, new Func<AndromedaGuiDragDropPlace, bool>(variable, (AndromedaGuiDragDropPlace t) => (t.isEmpty || t.location != 1 || t.item.get_ItemType() != ((AndromedaGuiDragDropPlace)this.prm).item.get_ItemType() ? false : t.item.get_Amount() < 5000))));
			if (andromedaGuiDragDropPlace != null)
			{
				Inventory.StackItems((AndromedaGuiDragDropPlace)prm, andromedaGuiDragDropPlace, Math.Min(5000 - andromedaGuiDragDropPlace.item.get_Amount(), ((AndromedaGuiDragDropPlace)prm).item.get_Amount()));
				return;
			}
		}
		if (!NetworkScript.player.playerBelongings.HaveAFreeSlot())
		{
			Debug.Log("CP IV");
			return;
		}
		NetworkScript.player.playerBelongings.FirstFreeInventorySlot();
		andromedaGuiDragDropPlace = Enumerable.FirstOrDefault<AndromedaGuiDragDropPlace>(Enumerable.Where<AndromedaGuiDragDropPlace>(Inventory.places, new Func<AndromedaGuiDragDropPlace, bool>(variable, (AndromedaGuiDragDropPlace t) => (t.item != null || t.location != 1 ? false : t.slot == this.freeInventorySlotId))));
		if (andromedaGuiDragDropPlace == null)
		{
			if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
			{
				AudioClip audioClip = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "error");
				AudioManager.PlayGUISound(audioClip);
			}
			Debug.Log("CP V");
			return;
		}
		if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
		{
			AudioClip fromStaticSet1 = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "button2");
			AudioManager.PlayGUISound(fromStaticSet1);
		}
		Inventory.MoveItemInData((AndromedaGuiDragDropPlace)prm, andromedaGuiDragDropPlace);
	}

	public static void MoveItemGambler(AndromedaGuiDragDropPlace src, AndromedaGuiDragDropPlace dst)
	{
		// 
		// Current member / type: System.Void Inventory::MoveItemGambler(AndromedaGuiDragDropPlace,AndromedaGuiDragDropPlace)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void MoveItemGambler(AndromedaGuiDragDropPlace,AndromedaGuiDragDropPlace)
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

	public static void MoveItemGuildVault(AndromedaGuiDragDropPlace src, AndromedaGuiDragDropPlace dst)
	{
		// 
		// Current member / type: System.Void Inventory::MoveItemGuildVault(AndromedaGuiDragDropPlace,AndromedaGuiDragDropPlace)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void MoveItemGuildVault(AndromedaGuiDragDropPlace,AndromedaGuiDragDropPlace)
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

	public static void MoveItemInData(AndromedaGuiDragDropPlace src, AndromedaGuiDragDropPlace dst)
	{
		// 
		// Current member / type: System.Void Inventory::MoveItemInData(AndromedaGuiDragDropPlace,AndromedaGuiDragDropPlace)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void MoveItemInData(AndromedaGuiDragDropPlace,AndromedaGuiDragDropPlace)
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

	public static void MoveItemMerchant(AndromedaGuiDragDropPlace src, AndromedaGuiDragDropPlace dst)
	{
		// 
		// Current member / type: System.Void Inventory::MoveItemMerchant(AndromedaGuiDragDropPlace,AndromedaGuiDragDropPlace)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void MoveItemMerchant(AndromedaGuiDragDropPlace,AndromedaGuiDragDropPlace)
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

	private static void MoveMethodMapper(AndromedaGuiDragDropPlace src, AndromedaGuiDragDropPlace dst)
	{
		if (src.location == 15 || dst.location == 15)
		{
			Inventory.MoveItemMerchant(src, dst);
		}
		else if (src.location == 16 || dst.location == 16 || src.location == 17 || dst.location == 17)
		{
			Inventory.MoveItemGambler(src, dst);
		}
		else if (src.location == 18 || dst.location == 18)
		{
			Inventory.MoveItemGuildVault(src, dst);
		}
		else
		{
			Inventory.MoveItemInData(src, dst);
		}
	}

	private static void MovePortalPartFromInventoryToTransformer(object prm)
	{
		Inventory.<MovePortalPartFromInventoryToTransformer>c__AnonStorey2B variable = null;
		AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = (AndromedaGuiDragDropPlace)prm;
		if (andromedaGuiDragDropPlace == null || andromedaGuiDragDropPlace.item == null || andromedaGuiDragDropPlace.location != 1 || !PlayerItems.IsPortalPart(andromedaGuiDragDropPlace.item.get_ItemType()))
		{
			return;
		}
		AndromedaGuiDragDropPlace andromedaGuiDragDropPlace1 = Enumerable.FirstOrDefault<AndromedaGuiDragDropPlace>(Enumerable.Where<AndromedaGuiDragDropPlace>(Inventory.places, new Func<AndromedaGuiDragDropPlace, bool>(variable, (AndromedaGuiDragDropPlace t) => (t.txItem == null ? false : t.txItem.callbackAttribute == this.place.item.get_Slot() + this.place.item.get_SlotType() * 1000))));
		if (andromedaGuiDragDropPlace1 == null)
		{
			return;
		}
		if (Inventory.activatePortalPartDrop != null)
		{
			Inventory.activatePortalPartDrop.Invoke(andromedaGuiDragDropPlace1.txItem.callbackAttribute);
		}
	}

	private static void OnAmountChange()
	{
		GuiLabel guiLabel = Inventory.stackItemAmount;
		double num = Math.Round((double)Inventory.sliderStack.CurrentValue);
		guiLabel.text = string.Format("{0}/{1}", num.ToString(), Inventory.sliderStack.MAX);
		float single = 0f;
		single = (!Inventory.isAmmoSelecktedStackableItem ? (float)Math.Round((double)Inventory.sliderStack.CurrentValue) * Inventory.sellPriceOfStackableItem : (float)Math.Round((double)Inventory.sliderStack.CurrentValue) / 100f * Inventory.sellPriceOfStackableItem);
		GuiLabel guiLabel1 = Inventory.stackItemSellPrice;
		string str = StaticData.Translate("key_inventory_sell_price");
		int num1 = Mathf.FloorToInt(single);
		guiLabel1.text = string.Concat(str, ": $ ", num1.ToString("##,##0"));
	}

	private static void OnCancelStackableDrop(EventHandlerParam prm)
	{
		AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = (AndromedaGuiDragDropPlace)prm.customData;
		AndromedaGui.gui.RemoveWindow(Inventory.stackableWnd.handler);
		Inventory.stackableWnd = null;
		AndromedaGui.gui.activeToolTipId = -1;
		AndromedaGui.gui.activeTooltipCloseAction = null;
		andromedaGuiDragDropPlace.txItem.CancelDropStackable();
		if (NetworkScript.player.state != 80)
		{
			AndromedaGui.mainWnd.RefreshInformationIcons();
		}
	}

	private static void OnContinueStackableDrop(EventHandlerParam prm)
	{
		AndromedaGui.gui.RemoveWindow(Inventory.stackableWnd.handler);
		Inventory.stackableWnd = null;
		AndromedaGui.gui.activeToolTipId = -1;
		AndromedaGui.gui.activeTooltipCloseAction = null;
		AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = (AndromedaGuiDragDropPlace)prm.customData;
		AndromedaGuiDragDropPlace andromedaGuiDragDropPlace1 = (AndromedaGuiDragDropPlace)prm.customData2;
		andromedaGuiDragDropPlace.txItem.DropStackable();
		int num = (int)Math.Round((double)Inventory.sliderStack.CurrentValue);
		if (!andromedaGuiDragDropPlace1.isEmpty || andromedaGuiDragDropPlace.item.get_Amount() != num)
		{
			Inventory.StackItems(andromedaGuiDragDropPlace, andromedaGuiDragDropPlace1, num);
		}
		else
		{
			Inventory.MoveMethodMapper(andromedaGuiDragDropPlace, andromedaGuiDragDropPlace1);
		}
		if (Inventory.secondaryDropHandler != null)
		{
			Inventory.secondaryDropHandler.Invoke(andromedaGuiDragDropPlace, andromedaGuiDragDropPlace1);
		}
	}

	public static void OnDropped(int a, int b)
	{
		Inventory.<OnDropped>c__AnonStorey32 variable = null;
		if (a == 100200300)
		{
			Inventory.activateCompareBox.Invoke(b);
			return;
		}
		if (a == 111222333)
		{
			if (Inventory.secondaryDropHandler != null)
			{
				Inventory.secondaryDropHandler.Invoke(null, null);
			}
			Inventory.activateItemReroll.Invoke(b);
			return;
		}
		if (PlayerItems.IsPortalPart((ushort)(a / 1000)) && AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 22)
		{
			Inventory.activatePortalPartDrop.Invoke(b);
			return;
		}
		ushort num = (ushort)(a & 65535);
		ushort num1 = (ushort)(a >> 16);
		AndromedaGuiDragDropPlace andromedaGuiDragDropPlace = Enumerable.First<AndromedaGuiDragDropPlace>(Enumerable.Where<AndromedaGuiDragDropPlace>(Inventory.places, new Func<AndromedaGuiDragDropPlace, bool>(variable, (AndromedaGuiDragDropPlace p) => p.id == this.srcId)));
		AndromedaGuiDragDropPlace andromedaGuiDragDropPlace1 = Enumerable.FirstOrDefault<AndromedaGuiDragDropPlace>(Enumerable.Where<AndromedaGuiDragDropPlace>(Inventory.places, new Func<AndromedaGuiDragDropPlace, bool>(variable, (AndromedaGuiDragDropPlace p) => p.id == this.dstId)));
		if (!andromedaGuiDragDropPlace1.isEmpty && (andromedaGuiDragDropPlace1.location == 8 || andromedaGuiDragDropPlace1.location == 6 || andromedaGuiDragDropPlace1.location == 7) && andromedaGuiDragDropPlace1.item is SlotItemWeapon && andromedaGuiDragDropPlace.item is SlotItemWeapon)
		{
			((SlotItemWeapon)andromedaGuiDragDropPlace.item).set_AmmoType(((SlotItemWeapon)andromedaGuiDragDropPlace1.item).get_AmmoType());
		}
		if (PlayerItems.IsAmmo(andromedaGuiDragDropPlace.item.get_ItemType()) && (andromedaGuiDragDropPlace1.location == 8 || andromedaGuiDragDropPlace1.location == 6 || andromedaGuiDragDropPlace1.location == 7))
		{
			byte slotType = ((SlotItemWeapon)andromedaGuiDragDropPlace1.item).get_SlotType();
			__ConfigWindow.SetAmmoTypeOnEquippedWeapon(andromedaGuiDragDropPlace.item.get_ItemType(), andromedaGuiDragDropPlace1.item.get_Slot(), slotType);
			((SlotItemWeapon)andromedaGuiDragDropPlace1.item).set_AmmoType(andromedaGuiDragDropPlace.item.get_ItemType());
			if (Inventory.secondaryDropHandler != null)
			{
				Inventory.secondaryDropHandler.Invoke(andromedaGuiDragDropPlace, andromedaGuiDragDropPlace1);
			}
			return;
		}
		if (PlayerItems.IsStackable(andromedaGuiDragDropPlace.item.get_ItemType()) && andromedaGuiDragDropPlace.location != 15)
		{
			if (!andromedaGuiDragDropPlace1.isEmpty && andromedaGuiDragDropPlace1.location != 15)
			{
				if (andromedaGuiDragDropPlace.item.get_ItemType() != andromedaGuiDragDropPlace1.item.get_ItemType())
				{
					Inventory.MoveItemInData(andromedaGuiDragDropPlace, andromedaGuiDragDropPlace1);
					return;
				}
				if (andromedaGuiDragDropPlace1.item.get_Amount() > 4999)
				{
					andromedaGuiDragDropPlace.txItem.CancelDropStackable();
					return;
				}
			}
			Inventory.DrawStackablePopup(andromedaGuiDragDropPlace, andromedaGuiDragDropPlace1);
			return;
		}
		if (!GuiFramework.IsVoiceSoundsMute && !GuiFramework.IsMasterSoundsMute)
		{
			AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "button2");
			AudioManager.PlayGUISound(fromStaticSet);
		}
		Inventory.MoveMethodMapper(andromedaGuiDragDropPlace, andromedaGuiDragDropPlace1);
		if (Inventory.secondaryDropHandler != null)
		{
			Inventory.secondaryDropHandler.Invoke(andromedaGuiDragDropPlace, andromedaGuiDragDropPlace1);
		}
		if (NetworkScript.player.state != 80)
		{
			AndromedaGui.mainWnd.RefreshInformationIcons();
		}
	}

	private static void OnMinusBtnClicked(object prm)
	{
		GuiHorizontalSlider currentValue = Inventory.sliderStack;
		currentValue.CurrentValue = currentValue.CurrentValue - 1f;
		if (Inventory.sliderStack.CurrentValue < Inventory.sliderStack.MIN)
		{
			Inventory.sliderStack.CurrentValue = Inventory.sliderStack.MIN;
		}
		GuiLabel guiLabel = Inventory.stackItemAmount;
		double num = Math.Round((double)Inventory.sliderStack.CurrentValue);
		guiLabel.text = string.Format("{0}/{1}", num.ToString(), Inventory.sliderStack.MAX);
	}

	private static void OnPlusBtnClicked(object prm)
	{
		GuiHorizontalSlider currentValue = Inventory.sliderStack;
		currentValue.CurrentValue = currentValue.CurrentValue + 1f;
		if (Inventory.sliderStack.CurrentValue > Inventory.sliderStack.MAX)
		{
			Inventory.sliderStack.CurrentValue = Inventory.sliderStack.MAX;
		}
		GuiLabel guiLabel = Inventory.stackItemAmount;
		double num = Math.Round((double)Inventory.sliderStack.CurrentValue);
		guiLabel.text = string.Format("{0}/{1}", num.ToString(), Inventory.sliderStack.MAX);
	}

	private static void OnSelectCurrencySelect(EventHandlerParam prm)
	{
		// 
		// Current member / type: System.Void Inventory::OnSelectCurrencySelect(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnSelectCurrencySelect(EventHandlerParam)
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

	public static void Populate()
	{
	}

	private static void ReEvaluateInventory()
	{
		foreach (AndromedaGuiDragDropPlace place in Inventory.places)
		{
			place.dropTargets.Clear();
			if (place.txItem == null)
			{
				continue;
			}
			place.txItem.CleanDropZones();
		}
		foreach (AndromedaGuiDragDropPlace andromedaGuiDragDropPlace in Inventory.places)
		{
			Inventory.ReEvaluatePlaceAsDropZone(andromedaGuiDragDropPlace);
		}
	}

	private static void ReEvaluatePlaceAsDropZone(AndromedaGuiDragDropPlace place)
	{
		foreach (AndromedaGuiDragDropPlace andromedaGuiDragDropPlace in Inventory.places)
		{
			if ((object)andromedaGuiDragDropPlace != (object)place)
			{
				if (!andromedaGuiDragDropPlace.isEmpty)
				{
					if (!Inventory.ResolveMoveItemEnabled2(andromedaGuiDragDropPlace, place))
					{
						if (!andromedaGuiDragDropPlace.dropTargets.Contains(place))
						{
							continue;
						}
						andromedaGuiDragDropPlace.dropTargets.Remove(place);
						andromedaGuiDragDropPlace.txItem.RemoveDropZone(Inventory.GetDropZoneKey(andromedaGuiDragDropPlace, place));
					}
					else if (!andromedaGuiDragDropPlace.dropTargets.Contains(place))
					{
						andromedaGuiDragDropPlace.dropTargets.Add(place);
						Inventory.AddSingleDropZoneToTexture(andromedaGuiDragDropPlace, place);
					}
				}
			}
		}
	}

	public static bool ResolveMoveItemCargo(ushort srcItemType, ItemLocation srcLocationType, ushort dstItemType, ItemLocation dstLocationType)
	{
		if (srcLocationType == 3 && dstLocationType == 3)
		{
			return true;
		}
		if (srcLocationType != 3)
		{
			if (srcLocationType != 2)
			{
				return false;
			}
			if (Inventory.IsCargoItem(srcItemType))
			{
				return true;
			}
			return false;
		}
		if (dstLocationType != 2)
		{
			return false;
		}
		if (dstItemType == 0)
		{
			return true;
		}
		if (Inventory.IsCargoItem(dstItemType))
		{
			return true;
		}
		return false;
	}

	public static bool ResolveMoveItemEnabled(ushort srcItemType, ItemLocation srcLocationType, ushort dstItemType, ItemLocation dstLocationType)
	{
		if (dstLocationType == 14)
		{
			return true;
		}
		if (PlayerItems.IsAmmo(srcItemType) && srcLocationType == 15 && dstLocationType == 15)
		{
			return false;
		}
		if (PlayerItems.IsAmmo(srcItemType) && srcItemType == dstItemType)
		{
			return true;
		}
		if (Inventory.IsItemCPUforExtras(srcItemType) && dstLocationType == 13)
		{
			if (!Inventory.HasCPUforExtras())
			{
				return true;
			}
			if (Inventory.IsItemCPUforExtras(dstItemType))
			{
				return true;
			}
			if (srcLocationType == 13)
			{
				return true;
			}
			return false;
		}
		if (dstLocationType == 15)
		{
			if (srcLocationType == 15)
			{
				return false;
			}
			return true;
		}
		if (srcLocationType == 15 && dstItemType != 0)
		{
			return false;
		}
		if (dstLocationType == 14)
		{
			return true;
		}
		if (srcLocationType == 3 || dstLocationType == 3)
		{
			return false;
		}
		if (srcLocationType == 1 && dstLocationType == 1)
		{
			return true;
		}
		if ((srcLocationType == 1 || srcLocationType == 15) && dstLocationType == 8 && srcItemType >= PlayerItems.TypeWeaponIonTire1 && srcItemType <= PlayerItems.TypeWeaponIonTire5)
		{
			return true;
		}
		if ((srcLocationType == 1 || srcLocationType == 15) && dstLocationType == 6 && srcItemType >= PlayerItems.TypeWeaponLaserTire1 && srcItemType <= PlayerItems.TypeWeaponLaserTire5)
		{
			return true;
		}
		if ((srcLocationType == 1 || srcLocationType == 15) && dstLocationType == 7 && srcItemType >= PlayerItems.TypeWeaponPlasmaTire1 && srcItemType <= PlayerItems.TypeWeaponPlasmaTire5)
		{
			return true;
		}
		if (srcLocationType == 8 && dstLocationType == 1 && dstItemType >= PlayerItems.TypeWeaponIonTire1 && dstItemType <= PlayerItems.TypeWeaponIonTire5)
		{
			return true;
		}
		if (srcLocationType == 7 && dstLocationType == 1 && dstItemType >= PlayerItems.TypeWeaponPlasmaTire1 && dstItemType <= PlayerItems.TypeWeaponPlasmaTire5)
		{
			return true;
		}
		if (srcLocationType == 6 && dstLocationType == 1 && dstItemType >= PlayerItems.TypeWeaponLaserTire1 && dstItemType <= PlayerItems.TypeWeaponLaserTire5)
		{
			return true;
		}
		if (srcLocationType == dstLocationType && srcLocationType != 9)
		{
			return true;
		}
		if (dstLocationType == 1 && dstItemType == 0)
		{
			return true;
		}
		if (PlayerItems.IsCorpus(srcItemType) && dstLocationType == 11)
		{
			return true;
		}
		if (PlayerItems.IsShield(srcItemType) && dstLocationType == 10)
		{
			return true;
		}
		if (PlayerItems.IsEngine(srcItemType) && dstLocationType == 12)
		{
			return true;
		}
		if (srcLocationType == 9 && PlayerItems.IsCorpus(srcItemType) && dstLocationType == 11)
		{
			return true;
		}
		if (srcLocationType == 9 && PlayerItems.IsShield(srcItemType) && dstLocationType == 10)
		{
			return true;
		}
		if (srcLocationType == 9 && PlayerItems.IsEngine(srcItemType) && dstLocationType == 12)
		{
			return true;
		}
		if (dstLocationType == 9 && Inventory.IsStructureItem(srcItemType) && (dstItemType == 0 || PlayerItems.IsCorpus(srcItemType) && PlayerItems.IsCorpus(dstItemType) || PlayerItems.IsEngine(srcItemType) && PlayerItems.IsEngine(dstItemType) || PlayerItems.IsShield(srcItemType) && PlayerItems.IsShield(dstItemType)))
		{
			return true;
		}
		if (srcLocationType == 9 && dstLocationType == 1 && Inventory.IsStructureItem(dstItemType))
		{
			return true;
		}
		if (srcLocationType == 13 && dstLocationType == 1)
		{
			if (!PlayerItems.IsExtra(dstItemType))
			{
				return false;
			}
			return true;
		}
		if ((srcLocationType == 1 || srcLocationType == 15) && dstLocationType == 13 && PlayerItems.IsExtra(srcItemType))
		{
			return true;
		}
		if ((srcLocationType == 1 || srcLocationType == 15) && (PlayerItems.IsShield(srcItemType) || PlayerItems.IsEngine(srcItemType) || PlayerItems.IsCorpus(srcItemType)) && dstLocationType == 9)
		{
			return true;
		}
		if (PlayerItems.IsCorpus(srcItemType) && PlayerItems.IsCorpus(dstItemType))
		{
			return true;
		}
		if (PlayerItems.IsShield(srcItemType) && PlayerItems.IsShield(dstItemType))
		{
			return true;
		}
		if (PlayerItems.IsEngine(srcItemType) && PlayerItems.IsEngine(dstItemType))
		{
			return true;
		}
		if (PlayerItems.IsExtra(srcItemType) && PlayerItems.IsExtra(dstItemType))
		{
			return true;
		}
		return false;
	}

	public static bool ResolveMoveItemEnabled2(AndromedaGuiDragDropPlace src, AndromedaGuiDragDropPlace dst)
	{
		ushort itemType;
		if ((dst.location == 1 || dst.location == 14) && NetworkScript.player.vessel.galaxy.get_galaxyId() == 1000)
		{
			return false;
		}
		if (dst.location == 18 && src.item != null && src.item.isAccountBound)
		{
			return false;
		}
		if (src.location == 18 && dst.location == 1 && StaticData.allTypes.get_Item(src.item.get_ItemType()).levelRestriction > NetworkScript.player.playerBelongings.playerLevel)
		{
			return false;
		}
		if (src.location == 1 && dst.location == 18 && dst.item != null && StaticData.allTypes.get_Item(dst.item.get_ItemType()).levelRestriction > NetworkScript.player.playerBelongings.playerLevel)
		{
			return false;
		}
		if (src.location == 18 && dst.item != null && dst.item.isAccountBound)
		{
			return false;
		}
		if (PlayerItems.IsAmmo(src.item.get_ItemType()) && src.location == 1 && (dst.location == 8 || dst.location == 6 || dst.location == 7))
		{
			return !dst.isEmpty;
		}
		if (src.location == 2 || dst.location == 2)
		{
			if (Inventory.isVaultMenuOpen)
			{
				return true;
			}
			return false;
		}
		if (dst.location == 18 && (src.location == 1 || src.location == 18))
		{
			return true;
		}
		if (src.location == 18 && dst.location == 1)
		{
			return true;
		}
		if (PlayerItems.IsAmmo(src.item.get_ItemType()) && src.location == 15 && dst.location != 15 && NetworkScript.player.playerBelongings.AllowedAmmo((int)src.item.get_ItemType()) < src.item.get_Amount())
		{
			return false;
		}
		if ((src.location == 17 || src.location == 16) && (dst.location == 17 || dst.location == 16))
		{
			return false;
		}
		if (dst.location == 17 || dst.location == 16)
		{
			return true;
		}
		if (src.location != 17 && src.location != 16)
		{
			if (StaticData.allTypes.get_Item(src.item.get_ItemType()).levelRestriction > NetworkScript.player.playerBelongings.playerLevel)
			{
				if (src.location == 15 && dst.location == 15)
				{
					return false;
				}
				if (dst.location != 14 && dst.location != 1 && dst.location != 15 && dst.location != 2 && dst.location != 16 && dst.location != 17)
				{
					return false;
				}
				return true;
			}
			if (!dst.isEmpty && StaticData.allTypes.get_Item(dst.item.get_ItemType()).levelRestriction > NetworkScript.player.playerBelongings.playerLevel)
			{
				return false;
			}
			ushort num = src.item.get_ItemType();
			ItemLocation itemLocation = src.location;
			if (!dst.isEmpty)
			{
				itemType = dst.item.get_ItemType();
			}
			else
			{
				itemType = 0;
			}
			return Inventory.ResolveMoveItemEnabled(num, itemLocation, itemType, dst.location);
		}
		if (dst.location == 15)
		{
			return false;
		}
		if (dst.location == 1 && dst.isEmpty)
		{
			return true;
		}
		if (StaticData.allTypes.get_Item(src.item.get_ItemType()).levelRestriction > NetworkScript.player.playerBelongings.playerLevel)
		{
			return false;
		}
		if (PlayerItems.IsShield(src.item.get_ItemType()) && dst.isEmpty && (dst.location == 10 || dst.location == 9))
		{
			return true;
		}
		if (PlayerItems.IsCorpus(src.item.get_ItemType()) && dst.isEmpty && (dst.location == 11 || dst.location == 9))
		{
			return true;
		}
		if (PlayerItems.IsEngine(src.item.get_ItemType()) && dst.isEmpty && (dst.location == 12 || dst.location == 9))
		{
			return true;
		}
		if (PlayerItems.IsExtra(src.item.get_ItemType()) && dst.isEmpty && dst.location == 13)
		{
			return true;
		}
		if (src.item.get_ItemType() >= PlayerItems.TypeWeaponLaserTire1 && src.item.get_ItemType() <= PlayerItems.TypeWeaponLaserTire5 && dst.isEmpty && dst.location == 6)
		{
			return true;
		}
		if (src.item.get_ItemType() >= PlayerItems.TypeWeaponPlasmaTire1 && src.item.get_ItemType() <= PlayerItems.TypeWeaponPlasmaTire5 && dst.isEmpty && dst.location == 7)
		{
			return true;
		}
		if (src.item.get_ItemType() >= PlayerItems.TypeWeaponIonTire1 && src.item.get_ItemType() <= PlayerItems.TypeWeaponIonTire5 && dst.isEmpty && dst.location == 8)
		{
			return true;
		}
		return false;
	}

	private static void SelectItemForBonusReroll(object prm)
	{
		Inventory.activateItemReroll.Invoke(((AndromedaGuiDragDropPlace)prm).txItem.callbackAttribute);
	}

	private static void SendBuyRequestToServer(BuyItemParams data)
	{
		if (data.itemSrc == 16 || data.itemSrc == 17)
		{
			playWebGame.udp.ExecuteCommand(35, data);
		}
		else
		{
			playWebGame.udp.ExecuteCommand(PureUdpClient.CommandBuyItem, data);
		}
		int num = 0;
		int num1 = 0;
		int num2 = 0;
		PlayerItems.GetItemPrices(Inventory.dialogItemSrc.item.get_ItemType(), ref num, ref num1, ref num2);
		if (data.itemSrc == 16)
		{
			num = 0;
			num1 = (int)((float)num1 * 1.5f);
			num2 = num1;
		}
		else if (data.itemSrc == 17)
		{
			num = 0;
			num1 = (int)((float)num1 * 2f);
			num2 = num1;
		}
		if (data.itemSrc != 16 && data.itemSrc != 17)
		{
			if (!PlayerItems.IsAmmo(data.itemType))
			{
				NetworkScript.player.playerBelongings.playerItems.BuySlotItem(data.itemType, (float)num, (float)num1, (float)num2, data.slotId, (byte)data.slotType, data.qty, data.shipId, data.currency, false);
			}
			else
			{
				int num3 = 0;
				NetworkScript.player.playerBelongings.BuyAmmo((int)data.itemType, data.qty, num, num1, num2, data.currency, ref num3);
			}
			NetworkScript.player.cfg = NetworkScript.player.playerBelongings.BuildCfg(NetworkScript.player.guild);
			NetworkScript.player.vessel.cfg = NetworkScript.player.cfg;
		}
		else if (data.currency == 1)
		{
			NetworkScript.player.playerBelongings.playerItems.AddNova((long)(-num1));
		}
		else if (data.currency == 2)
		{
			NetworkScript.player.playerBelongings.playerItems.AddEquilibrium((long)(-num2));
		}
		Inventory.ReEvaluatePlaceAsDropZone(Inventory.dialogItemSrc);
		Inventory.ReEvaluatePlaceAsDropZone(Inventory.dialogItemDst);
		if (Inventory.secondaryDropHandler != null)
		{
			Inventory.secondaryDropHandler.Invoke(Inventory.dialogItemSrc, Inventory.dialogItemDst);
		}
		if (NetworkScript.player.state != 80)
		{
			AndromedaGui.mainWnd.RefreshInformationIcons();
		}
		Inventory.CurrencyDialogRemove(null);
	}

	public static void StackItems(AndromedaGuiDragDropPlace src, AndromedaGuiDragDropPlace dst, int transAmount)
	{
		// 
		// Current member / type: System.Void Inventory::StackItems(AndromedaGuiDragDropPlace,AndromedaGuiDragDropPlace,System.Int32)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void StackItems(AndromedaGuiDragDropPlace,AndromedaGuiDragDropPlace,System.Int32)
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