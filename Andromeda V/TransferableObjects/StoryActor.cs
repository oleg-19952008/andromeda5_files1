using System;
using System.Collections.Generic;

public class StoryActor
{
	public byte id;

	public string name;

	public string assetName;

	public static List<StoryActor> allActor;

	public static StoryActor actorVladimir;

	public static StoryActor actorDarius;

	public static StoryActor actorStalker;

	public static StoryActor actorLtBrown;

	public static StoryActor actorTedClancey;

	public static StoryActor actorNassor;

	public static StoryActor actorCaribbeanJoe;

	public static StoryActor actorSamHawkins;

	public static StoryActor actorEddFinn;

	public static StoryActor actorJohnnyDigger;

	public static StoryActor actorWalter;

	public static StoryActor actorReese;

	public static StoryActor actorJames;

	public static StoryActor actorThane;

	public static StoryActor actorLoyce;

	public static StoryActor actorGabriel;

	public static StoryActor actorOleg;

	public static StoryActor actorMorbidSimon;

	public static StoryActor actorEddie;

	public static StoryActor actorXena;

	public static StoryActor actorPatton;

	public static StoryActor actorKeon;

	public static StoryActor actorLuther;

	public static StoryActor actorLouise;

	public static StoryActor actorLancer;

	public static StoryActor actorRuby;

	public static StoryActor actorLeona;

	public static StoryActor actorSkye;

	public static StoryActor actorAria;

	public static StoryActor actorTaura;

	public static StoryActor actorGolgotha;

	public static StoryActor actorVolkr;

	public static StoryActor actorPlayer;

	static StoryActor()
	{
		StoryActor storyActor = new StoryActor()
		{
			id = 1,
			name = "key_NPC_name_Vladimir",
			assetName = "NPC_Vladimir"
		};
		StoryActor.actorVladimir = storyActor;
		StoryActor storyActor1 = new StoryActor()
		{
			id = 2,
			name = "key_NPC_name_Darius",
			assetName = "NPC_Darius"
		};
		StoryActor.actorDarius = storyActor1;
		StoryActor storyActor2 = new StoryActor()
		{
			id = 3,
			name = "key_NPC_name_Stalker",
			assetName = "NPC_Stalker"
		};
		StoryActor.actorStalker = storyActor2;
		StoryActor storyActor3 = new StoryActor()
		{
			id = 4,
			name = "key_NPC_name_LtBrown",
			assetName = "NPC_LtBrown"
		};
		StoryActor.actorLtBrown = storyActor3;
		StoryActor storyActor4 = new StoryActor()
		{
			id = 5,
			name = "key_NPC_name_TedClancey",
			assetName = "NPC_TedClancey"
		};
		StoryActor.actorTedClancey = storyActor4;
		StoryActor storyActor5 = new StoryActor()
		{
			id = 6,
			name = "key_NPC_name_Nassor",
			assetName = "NPC_Nassor"
		};
		StoryActor.actorNassor = storyActor5;
		StoryActor storyActor6 = new StoryActor()
		{
			id = 7,
			name = "key_NPC_name_CaribbeanJoe",
			assetName = "NPC_CaribbeanJoe"
		};
		StoryActor.actorCaribbeanJoe = storyActor6;
		StoryActor storyActor7 = new StoryActor()
		{
			id = 8,
			name = "key_NPC_name_SamHawkins",
			assetName = "NPC_SamHawkins"
		};
		StoryActor.actorSamHawkins = storyActor7;
		StoryActor storyActor8 = new StoryActor()
		{
			id = 9,
			name = "key_NPC_name_EddFinn",
			assetName = "NPC_EddFinn"
		};
		StoryActor.actorEddFinn = storyActor8;
		StoryActor storyActor9 = new StoryActor()
		{
			id = 10,
			name = "key_NPC_name_JohnnyDigger",
			assetName = "NPC_JohnnyDigger"
		};
		StoryActor.actorJohnnyDigger = storyActor9;
		StoryActor storyActor10 = new StoryActor()
		{
			id = 11,
			name = "key_NPC_name_Walter",
			assetName = "NPC_Walter"
		};
		StoryActor.actorWalter = storyActor10;
		StoryActor storyActor11 = new StoryActor()
		{
			id = 12,
			name = "key_NPC_name_Reese",
			assetName = "NPC_Reese"
		};
		StoryActor.actorReese = storyActor11;
		StoryActor storyActor12 = new StoryActor()
		{
			id = 13,
			name = "key_NPC_name_James",
			assetName = "NPC_James"
		};
		StoryActor.actorJames = storyActor12;
		StoryActor storyActor13 = new StoryActor()
		{
			id = 14,
			name = "key_NPC_name_Thane",
			assetName = "NPC_Thane"
		};
		StoryActor.actorThane = storyActor13;
		StoryActor storyActor14 = new StoryActor()
		{
			id = 15,
			name = "key_NPC_name_Loyce",
			assetName = "NPC_Loyce"
		};
		StoryActor.actorLoyce = storyActor14;
		StoryActor storyActor15 = new StoryActor()
		{
			id = 16,
			name = "key_NPC_name_Gabriel",
			assetName = "NPC_Gabriel"
		};
		StoryActor.actorGabriel = storyActor15;
		StoryActor storyActor16 = new StoryActor()
		{
			id = 17,
			name = "key_NPC_name_Oleg",
			assetName = "NPC_Oleg"
		};
		StoryActor.actorOleg = storyActor16;
		StoryActor storyActor17 = new StoryActor()
		{
			id = 18,
			name = "key_NPC_name_MorbidSimon",
			assetName = "NPC_MorbidSimon"
		};
		StoryActor.actorMorbidSimon = storyActor17;
		StoryActor storyActor18 = new StoryActor()
		{
			id = 19,
			name = "key_NPC_name_Eddie",
			assetName = "NPC_Eddie"
		};
		StoryActor.actorEddie = storyActor18;
		StoryActor storyActor19 = new StoryActor()
		{
			id = 20,
			name = "key_NPC_name_Xena",
			assetName = "NPC_Xena"
		};
		StoryActor.actorXena = storyActor19;
		StoryActor storyActor20 = new StoryActor()
		{
			id = 21,
			name = "key_NPC_name_Patton",
			assetName = "NPC_Patton"
		};
		StoryActor.actorPatton = storyActor20;
		StoryActor storyActor21 = new StoryActor()
		{
			id = 22,
			name = "key_NPC_name_Keon",
			assetName = "NPC_Keon"
		};
		StoryActor.actorKeon = storyActor21;
		StoryActor storyActor22 = new StoryActor()
		{
			id = 23,
			name = "key_NPC_name_Luther",
			assetName = "NPC_Luther"
		};
		StoryActor.actorLuther = storyActor22;
		StoryActor storyActor23 = new StoryActor()
		{
			id = 24,
			name = "key_NPC_name_Louise",
			assetName = "NPC_Louise"
		};
		StoryActor.actorLouise = storyActor23;
		StoryActor storyActor24 = new StoryActor()
		{
			id = 25,
			name = "key_npc_name_lancer",
			assetName = "NPC_Lancer"
		};
		StoryActor.actorLancer = storyActor24;
		StoryActor storyActor25 = new StoryActor()
		{
			id = 26,
			name = "key_npc_name_ruby",
			assetName = "NPC_Ruby"
		};
		StoryActor.actorRuby = storyActor25;
		StoryActor storyActor26 = new StoryActor()
		{
			id = 27,
			name = "key_npc_name_leona",
			assetName = "NPC_Leona"
		};
		StoryActor.actorLeona = storyActor26;
		StoryActor storyActor27 = new StoryActor()
		{
			id = 28,
			name = "key_npc_name_skye",
			assetName = "NPC_Skye"
		};
		StoryActor.actorSkye = storyActor27;
		StoryActor storyActor28 = new StoryActor()
		{
			id = 29,
			name = "key_story_actor_aria",
			assetName = "Aria"
		};
		StoryActor.actorAria = storyActor28;
		StoryActor storyActor29 = new StoryActor()
		{
			id = 30,
			name = "key_story_actor_taura",
			assetName = "Taura"
		};
		StoryActor.actorTaura = storyActor29;
		StoryActor storyActor30 = new StoryActor()
		{
			id = 31,
			name = "key_story_actor_golgotha",
			assetName = "Golgotha"
		};
		StoryActor.actorGolgotha = storyActor30;
		StoryActor storyActor31 = new StoryActor()
		{
			id = 32,
			name = "key_story_actor_volkor",
			assetName = "Volkr"
		};
		StoryActor.actorVolkr = storyActor31;
		StoryActor storyActor32 = new StoryActor()
		{
			id = 33,
			name = "key_story_actor_player",
			assetName = "Player"
		};
		StoryActor.actorPlayer = storyActor32;
		List<StoryActor> storyActors = new List<StoryActor>()
		{
			StoryActor.actorVladimir,
			StoryActor.actorDarius,
			StoryActor.actorStalker,
			StoryActor.actorLtBrown,
			StoryActor.actorTedClancey,
			StoryActor.actorNassor,
			StoryActor.actorCaribbeanJoe,
			StoryActor.actorSamHawkins,
			StoryActor.actorEddFinn,
			StoryActor.actorJohnnyDigger,
			StoryActor.actorWalter,
			StoryActor.actorReese,
			StoryActor.actorJames,
			StoryActor.actorThane,
			StoryActor.actorLoyce,
			StoryActor.actorGabriel,
			StoryActor.actorOleg,
			StoryActor.actorMorbidSimon,
			StoryActor.actorEddie,
			StoryActor.actorXena,
			StoryActor.actorPatton,
			StoryActor.actorKeon,
			StoryActor.actorLuther,
			StoryActor.actorLouise,
			StoryActor.actorLancer,
			StoryActor.actorRuby,
			StoryActor.actorLeona,
			StoryActor.actorSkye,
			StoryActor.actorAria,
			StoryActor.actorTaura,
			StoryActor.actorGolgotha,
			StoryActor.actorVolkr,
			StoryActor.actorPlayer
		};
		StoryActor.allActor = storyActors;
	}

	public StoryActor()
	{
	}
}