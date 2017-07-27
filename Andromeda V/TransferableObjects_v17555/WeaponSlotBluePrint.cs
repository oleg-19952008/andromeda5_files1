using System;

public class WeaponSlotBluePrint
{
	public string shipType;

	public bool isSlot1Allowed;

	public bool isSlot2Allowed;

	public bool isSlot3Allowed;

	public bool isSlot4Allowed;

	public bool isSlot5Allowed;

	public bool isSlot6Allowed;

	public static WeaponSlotBluePrint[] allShipBluePrint;

	static WeaponSlotBluePrint()
	{
		WeaponSlotBluePrint[] weaponSlotBluePrintArray = new WeaponSlotBluePrint[16];
		WeaponSlotBluePrint weaponSlotBluePrint = new WeaponSlotBluePrint()
		{
			shipType = "key_ship_type_Mosquito",
			isSlot1Allowed = true,
			isSlot2Allowed = false,
			isSlot3Allowed = false,
			isSlot4Allowed = false,
			isSlot5Allowed = false,
			isSlot6Allowed = false
		};
		weaponSlotBluePrintArray[0] = weaponSlotBluePrint;
		WeaponSlotBluePrint weaponSlotBluePrint1 = new WeaponSlotBluePrint()
		{
			shipType = "key_ship_type_Serpent",
			isSlot1Allowed = true,
			isSlot2Allowed = true,
			isSlot3Allowed = false,
			isSlot4Allowed = true,
			isSlot5Allowed = false,
			isSlot6Allowed = false
		};
		weaponSlotBluePrintArray[1] = weaponSlotBluePrint1;
		WeaponSlotBluePrint weaponSlotBluePrint2 = new WeaponSlotBluePrint()
		{
			shipType = "key_ship_type_Vindicator",
			isSlot1Allowed = true,
			isSlot2Allowed = true,
			isSlot3Allowed = false,
			isSlot4Allowed = false,
			isSlot5Allowed = false,
			isSlot6Allowed = false
		};
		weaponSlotBluePrintArray[2] = weaponSlotBluePrint2;
		WeaponSlotBluePrint weaponSlotBluePrint3 = new WeaponSlotBluePrint()
		{
			shipType = "key_ship_type_Nemesis",
			isSlot1Allowed = true,
			isSlot2Allowed = true,
			isSlot3Allowed = true,
			isSlot4Allowed = true,
			isSlot5Allowed = true,
			isSlot6Allowed = true
		};
		weaponSlotBluePrintArray[3] = weaponSlotBluePrint3;
		WeaponSlotBluePrint weaponSlotBluePrint4 = new WeaponSlotBluePrint()
		{
			shipType = "key_ship_type_Locust",
			isSlot1Allowed = true,
			isSlot2Allowed = true,
			isSlot3Allowed = false,
			isSlot4Allowed = true,
			isSlot5Allowed = true,
			isSlot6Allowed = true
		};
		weaponSlotBluePrintArray[4] = weaponSlotBluePrint4;
		WeaponSlotBluePrint weaponSlotBluePrint5 = new WeaponSlotBluePrint()
		{
			shipType = "key_ship_type_Mantis",
			isSlot1Allowed = false,
			isSlot2Allowed = true,
			isSlot3Allowed = false,
			isSlot4Allowed = true,
			isSlot5Allowed = false,
			isSlot6Allowed = true
		};
		weaponSlotBluePrintArray[5] = weaponSlotBluePrint5;
		WeaponSlotBluePrint weaponSlotBluePrint6 = new WeaponSlotBluePrint()
		{
			shipType = "key_ship_type_Crusader",
			isSlot1Allowed = true,
			isSlot2Allowed = true,
			isSlot3Allowed = true,
			isSlot4Allowed = false,
			isSlot5Allowed = false,
			isSlot6Allowed = false
		};
		weaponSlotBluePrintArray[6] = weaponSlotBluePrint6;
		WeaponSlotBluePrint weaponSlotBluePrint7 = new WeaponSlotBluePrint()
		{
			shipType = "key_ship_type_Destroyer",
			isSlot1Allowed = true,
			isSlot2Allowed = true,
			isSlot3Allowed = true,
			isSlot4Allowed = true,
			isSlot5Allowed = false,
			isSlot6Allowed = true
		};
		weaponSlotBluePrintArray[7] = weaponSlotBluePrint7;
		WeaponSlotBluePrint weaponSlotBluePrint8 = new WeaponSlotBluePrint()
		{
			shipType = "key_ship_type_Boar",
			isSlot1Allowed = true,
			isSlot2Allowed = true,
			isSlot3Allowed = true,
			isSlot4Allowed = true,
			isSlot5Allowed = false,
			isSlot6Allowed = false
		};
		weaponSlotBluePrintArray[8] = weaponSlotBluePrint8;
		WeaponSlotBluePrint weaponSlotBluePrint9 = new WeaponSlotBluePrint()
		{
			shipType = "key_ship_type_Ravager",
			isSlot1Allowed = true,
			isSlot2Allowed = true,
			isSlot3Allowed = false,
			isSlot4Allowed = true,
			isSlot5Allowed = true,
			isSlot6Allowed = false
		};
		weaponSlotBluePrintArray[9] = weaponSlotBluePrint9;
		WeaponSlotBluePrint weaponSlotBluePrint10 = new WeaponSlotBluePrint()
		{
			shipType = "key_ship_type_Cormorant",
			isSlot1Allowed = true,
			isSlot2Allowed = true,
			isSlot3Allowed = false,
			isSlot4Allowed = false,
			isSlot5Allowed = true,
			isSlot6Allowed = false
		};
		weaponSlotBluePrintArray[10] = weaponSlotBluePrint10;
		WeaponSlotBluePrint weaponSlotBluePrint11 = new WeaponSlotBluePrint()
		{
			shipType = "key_ship_type_Sentinel",
			isSlot1Allowed = true,
			isSlot2Allowed = true,
			isSlot3Allowed = true,
			isSlot4Allowed = true,
			isSlot5Allowed = false,
			isSlot6Allowed = true
		};
		weaponSlotBluePrintArray[11] = weaponSlotBluePrint11;
		WeaponSlotBluePrint weaponSlotBluePrint12 = new WeaponSlotBluePrint()
		{
			shipType = "key_ship_type_Vulture",
			isSlot1Allowed = true,
			isSlot2Allowed = true,
			isSlot3Allowed = true,
			isSlot4Allowed = true,
			isSlot5Allowed = true,
			isSlot6Allowed = true
		};
		weaponSlotBluePrintArray[12] = weaponSlotBluePrint12;
		WeaponSlotBluePrint weaponSlotBluePrint13 = new WeaponSlotBluePrint()
		{
			shipType = "key_ship_type_Tiger",
			isSlot1Allowed = true,
			isSlot2Allowed = true,
			isSlot3Allowed = true,
			isSlot4Allowed = true,
			isSlot5Allowed = true,
			isSlot6Allowed = true
		};
		weaponSlotBluePrintArray[13] = weaponSlotBluePrint13;
		WeaponSlotBluePrint weaponSlotBluePrint14 = new WeaponSlotBluePrint()
		{
			shipType = "key_ship_type_Viper",
			isSlot1Allowed = false,
			isSlot2Allowed = true,
			isSlot3Allowed = false,
			isSlot4Allowed = false,
			isSlot5Allowed = true,
			isSlot6Allowed = false
		};
		weaponSlotBluePrintArray[14] = weaponSlotBluePrint14;
		WeaponSlotBluePrint weaponSlotBluePrint15 = new WeaponSlotBluePrint()
		{
			shipType = "key_ship_type_Red_Dragon",
			isSlot1Allowed = true,
			isSlot2Allowed = true,
			isSlot3Allowed = true,
			isSlot4Allowed = true,
			isSlot5Allowed = true,
			isSlot6Allowed = true
		};
		weaponSlotBluePrintArray[15] = weaponSlotBluePrint15;
		WeaponSlotBluePrint.allShipBluePrint = weaponSlotBluePrintArray;
	}

	public WeaponSlotBluePrint()
	{
	}
}