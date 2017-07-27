using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using TransferableObjects;
using UnityEngine;

public class NetworkScript : MonoBehaviour
{
	[NonSerialized]
	public static float MOVE_LAG_LIMIT;

	[NonSerialized]
	public static bool isOnline;

	[NonSerialized]
	public static SpacaLabelManager spaceLabelManager;

	[NonSerialized]
	public static PlayerNameManager playerNameManager;

	[NonSerialized]
	public static ActiveSkillManager activeSkillManager;

	[NonSerialized]
	private GameObject spaceLabel;

	[NonSerialized]
	public static string PROJECTILE_ASSET_SUFFIX;

	[NonSerialized]
	public PureUdpClient udp;

	[NonSerialized]
	public LevelMap galaxy;

	[NonSerialized]
	public static SortedList<long, PartyInvite> partyInviters;

	[NonSerialized]
	public static SortedList<long, PartyInvite> partyInvitees;

	[NonSerialized]
	public static SortedList<long, PlayerDataEx> clientSideClientsList;

	[NonSerialized]
	public SortedList<uint, GameObjectPhysics> gameObjects;

	[NonSerialized]
	public static PlayerDataEx player;

	[NonSerialized]
	public static PartyClientSide party;

	[NonSerialized]
	public static SortedList<long, PartyMemberInfo> partyMembersInfo;

	private GameObject playerShip;

	private static bool isDeviceInfoUpdateSend;

	public static bool buildNeighbourhoodDone;

	private List<Action<int>> customActions;

	[NonSerialized]
	public DateTime lastIamAliveMessageTime = DateTime.get_Now();

	private float currentScreenHeight;

	private float currentScreenWidth;

	private DateTime nextPingTime;

	public bool denyReorderGUI;

	private long lastDoStartPacketSeq;

	private string lastSignedPlayerName = string.Empty;

	private DateTime lastSignedPlayerTimestamp = DateTime.MinValue;

	private int waitingQuestInfoId;

	private long lastNotificationPacketSeq = (long)-1;

	private bool isAlreadyInStealt;

	public static bool isInBase;

	[NonSerialized]
	private GameObject hyperJump;

	[NonSerialized]
	private int showMessageDuration;

	[NonSerialized]
	private DateTime startedShowMessageTime;

	[NonSerialized]
	private string errorText;

	[NonSerialized]
	private bool showErrorMessageBox;

	static NetworkScript()
	{
		NetworkScript.MOVE_LAG_LIMIT = 4f;
		NetworkScript.isOnline = true;
		NetworkScript.spaceLabelManager = new SpacaLabelManager();
		NetworkScript.activeSkillManager = new ActiveSkillManager();
		NetworkScript.PROJECTILE_ASSET_SUFFIX = "_pjc";
		NetworkScript.partyInviters = new SortedList<long, PartyInvite>();
		NetworkScript.partyInvitees = new SortedList<long, PartyInvite>();
		NetworkScript.partyMembersInfo = new SortedList<long, PartyMemberInfo>();
		NetworkScript.isDeviceInfoUpdateSend = false;
		NetworkScript.buildNeighbourhoodDone = false;
		NetworkScript.isInBase = false;
	}

	public NetworkScript()
	{
	}

	public static void ApplyPhysicsToGameObject(GameObjectPhysics p, GameObject obj)
	{
		obj.get_transform().set_position(new Vector3(p.x, p.y, p.z));
	}

	public static void ApplyShipPhysicsToGameObject(PlayerObjectPhysics p, GameObject ship)
	{
		ship.get_transform().set_position(new Vector3(p.x, p.y, p.z));
		ship.get_transform().set_rotation(Quaternion.Euler(p.rotationX, p.rotationY, p.rotationZ));
	}

	public static void ApplyTurretPhysicsToGameObject(DefenceTurret p, GameObject go)
	{
		go.get_transform().set_position(new Vector3(p.x, p.y, p.z));
		go.get_transform().set_rotation(Quaternion.Euler(p.rotationX, p.rotationY, p.rotationZ));
	}

	private void BuildExtractionPoint(ExtractionPoint point)
	{
		this.SetExtractionPointOwner(point);
		this.SwitchExtractionPointAlarm(point);
		SortedList<string, Animation> sortedList = new SortedList<string, Animation>();
		Component[] componentsInChildren = ((GameObject)point.gameObject).GetComponentsInChildren(typeof(Animation));
		for (int i = 0; i < (int)componentsInChildren.Length; i++)
		{
			Animation animation = (Animation)componentsInChildren[i];
			sortedList.Add(animation.get_name(), animation);
		}
		if (point.upgradesHitPoints != 0)
		{
			for (int j = 1; j <= point.upgradesHitPoints; j++)
			{
				string str = string.Format("ExtractionBaseHP{0}", j);
				Animation item = sortedList.get_Item(str);
				string _name = item.get_clip().get_name();
				float _length = item.get_clip().get_length();
				item.get_Item(_name).set_time(_length);
				item.Play(_name);
			}
		}
		if (point.upgradesPopulation != 0)
		{
			for (int k = 1; k <= point.upgradesPopulation; k++)
			{
				string str1 = string.Format("ExtractionBasePop{0}", k);
				Animation item1 = sortedList.get_Item(str1);
				string _name1 = item1.get_clip().get_name();
				float single = item1.get_clip().get_length();
				item1.get_Item(_name1).set_time(single);
				item1.Play(_name1);
			}
		}
		if (point.upgradesBarracks != 0)
		{
			for (int l = 1; l <= point.upgradesBarracks; l++)
			{
				string str2 = string.Format("ExtractionBaseBarracks{0}", l);
				Animation animation1 = sortedList.get_Item(str2);
				string _name2 = animation1.get_clip().get_name();
				float _length1 = animation1.get_clip().get_length();
				animation1.get_Item(_name2).set_time(_length1);
				animation1.Play(_name2);
			}
		}
		if (point.upgradesUltralibrium != 0)
		{
			for (int m = 1; m <= point.upgradesUltralibrium; m++)
			{
				Animation item2 = null;
				string str3 = string.Format("Mining{0}", m);
				item2 = sortedList.get_Item(str3);
				string str4 = string.Format("Mining0{0}", m);
				item2.Play(str4);
				if (m == 1)
				{
					str3 = string.Format("Mining{0}Loop", m);
					item2 = sortedList.get_Item(str3);
					item2.Play();
				}
				else if (m != 5)
				{
					item2.PlayQueued(string.Concat(str4, "Loop"), 0);
				}
			}
		}
	}

	private void BuildPlayerNeighbourhood(JoinMapData data)
	{
		// 
		// Current member / type: System.Void NetworkScript::BuildPlayerNeighbourhood(JoinMapData)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void BuildPlayerNeighbourhood(JoinMapData)
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..( , Int32 , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:строка 78
		//    в ..( , Int32& , Statement& , Int32& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:строка 33
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 57
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 431
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 71
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

	private void CheckAndCallStartPlay(int actionIndex)
	{
		// 
		// Current member / type: System.Void NetworkScript::CheckAndCallStartPlay(System.Int32)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void CheckAndCallStartPlay(System.Int32)
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

	public void CheckNpcState(short npcKey)
	{
		// 
		// Current member / type: System.Void NetworkScript::CheckNpcState(System.Int16)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void CheckNpcState(System.Int16)
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

	private void ClearProjectiles(PlayerObjectPhysics pop)
	{
		GameObjectPhysics[] array = Enumerable.ToArray<GameObjectPhysics>(this.gameObjects.get_Values());
		for (int i = 0; i < (int)array.Length; i++)
		{
			GameObjectPhysics gameObjectPhysic = array[i];
			if (gameObjectPhysic is ProjectileObject)
			{
				ProjectileObject projectileObject = (ProjectileObject)gameObjectPhysic;
				if (projectileObject.target == pop)
				{
					this.RemoveGameObject(projectileObject.neighbourhoodId);
				}
				if (projectileObject.shooter == pop)
				{
					this.RemoveGameObject(projectileObject.neighbourhoodId);
				}
			}
		}
	}

	private void DeactiveteMineral(Mineral mineral)
	{
		((GameObject)mineral.gameObject).SetActive(false);
	}

	public void DestroyControlTower(GameObjectPhysics target)
	{
		IEnumerator<PlayerDataEx> enumerator = NetworkScript.clientSideClientsList.get_Values().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				PlayerDataEx current = enumerator.get_Current();
				if ((object)current.vessel.shootingAt != (object)target)
				{
					continue;
				}
				current.vessel.isShooting = false;
				current.vessel.shootingAt = null;
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

	private void DestroyExtractionPoint(ExtractionPoint point)
	{
		AudioClip fromStaticSet;
		GameObject gameObject = null;
		gameObject = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("Explosion/explosionEffect_pfb"), new Vector3(point.x, point.y - 13f, point.z - 5f), Quaternion.get_identity());
		Object.Destroy(gameObject, 6f);
		string str = string.Format("explosion_{0}", Random.Range(3, 9));
		if (str != string.Empty)
		{
			fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", str);
			AudioManager.PlayAudioClip(fromStaticSet, gameObject.get_transform().get_position());
		}
		GameObject gameObject1 = null;
		gameObject1 = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("Explosion/explosionEffect_pfb"), new Vector3(point.x + 2f, point.y - 12f, point.z + 11f), Quaternion.get_identity());
		Object.Destroy(gameObject1, 7f);
		str = string.Format("explosion_{0}", Random.Range(3, 9));
		if (str != string.Empty)
		{
			fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", str);
			AudioManager.PlayAudioClip(fromStaticSet, gameObject1.get_transform().get_position());
		}
		GameObject gameObject2 = null;
		gameObject2 = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("Explosion/explosionEffect_pfb"), new Vector3(point.x - 20f, point.y - 11f, point.z + 4f), Quaternion.get_identity());
		Object.Destroy(gameObject2, 5f);
		str = string.Format("explosion_{0}", Random.Range(3, 9));
		if (str != string.Empty)
		{
			fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", str);
			AudioManager.PlayAudioClip(fromStaticSet, gameObject2.get_transform().get_position());
		}
		GameObject gameObject3 = null;
		gameObject3 = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("Explosion/explosionEffect_pfb"), new Vector3(point.x, point.y - 2f, point.z), Quaternion.get_identity());
		Object.Destroy(gameObject3, 6f);
		str = string.Format("explosion_{0}", Random.Range(3, 9));
		if (str != string.Empty)
		{
			fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", str);
			AudioManager.PlayAudioClip(fromStaticSet, gameObject3.get_transform().get_position());
		}
		Component[] componentsInChildren = ((GameObject)point.gameObject).GetComponentsInChildren(typeof(Animation));
		for (int i = 0; i < (int)componentsInChildren.Length; i++)
		{
			Animation animation = (Animation)componentsInChildren[i];
			animation.get_Item(animation.get_clip().get_name()).set_speed(-1f);
			animation.Play(0);
			if (animation.get_name() == "Mining1Loop")
			{
				animation.Stop();
			}
			if (animation.get_name() == "Mining2")
			{
				animation.Stop("Mining02Loop");
			}
			if (animation.get_name() == "Mining3")
			{
				animation.Stop("Mining03Loop");
			}
			if (animation.get_name() == "Mining4")
			{
				animation.Stop("Mining04Loop");
			}
		}
		this.SetExtractionPointOwner(point);
		this.SwitchExtractionPointAlarm(point);
	}

	public void DisconnectOnNetworkProblem()
	{
		this.errorText = "You have been removed from the game due to connectivity problem";
		this.showErrorMessageBox = true;
		Object.Destroy(NetworkScript.player.shipScript.get_gameObject(), 0.1f);
	}

	private void DoAchievementUnlock(UdpCommHeader header)
	{
		NetworkScript.<DoAchievementUnlock>c__AnonStorey48 variable = null;
		if (this.lastNotificationPacketSeq == header.packetSeq)
		{
			return;
		}
		this.lastNotificationPacketSeq = header.packetSeq;
		GenericData genericDatum = (GenericData)header.data;
		Achievement achievement = Enumerable.First<Achievement>(Enumerable.Where<Achievement>(Achievement.allAchievement, new Func<Achievement, bool>(variable, (Achievement a) => a.targetType == this.data.int1)));
		string str = (Achievements)(achievement.id - 1).ToString();
		NotificationManager.AddNotification(string.Format("{0} [{1}]", StaticData.Translate(achievement.name), genericDatum.int3), string.Format(StaticData.Translate(achievement.description), achievement.levels[genericDatum.int3 - 1]), (byte)genericDatum.int3, str, new Action<EventHandlerParam>(this, NetworkScript.OnAchievementNotificationClick));
	}

	private void DoAddToBlacklist(UdpCommHeader header)
	{
		playWebGame.LogMixPanel(MixPanelEvents.AddToBlacklist, null);
		if (header.data is GenericData)
		{
			GenericData genericDatum = (GenericData)header.data;
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 17 && NetworkScript.player.playerBelongings.playerName == ProfileScreen.playerUserName)
			{
				((ProfileScreen)AndromedaGui.mainWnd.activeWindow).PopulateData(NetworkScript.player.myPlayerProfileInfo);
				((ProfileScreen)AndromedaGui.mainWnd.activeWindow).PopulateAddToListResponse((byte)genericDatum.int1, genericDatum.str1);
			}
			if (__ChatWindow.wnd != null && (__ChatWindow.wnd.tbSendMessage.text.Contains("/ignore") || __ChatWindow.wnd.tbSendMessage.text.Contains("/IGNORE")))
			{
				__ChatWindow.wnd.ProcessFriendResponce((byte)genericDatum.int1, genericDatum.str1);
			}
		}
		else if (header.data is PlayerProfile)
		{
			PlayerProfile playerProfile = (PlayerProfile)header.data;
			NetworkScript.player.myBlacklist.Add(playerProfile.userName, playerProfile);
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 17 && NetworkScript.player.playerBelongings.playerName == ProfileScreen.playerUserName)
			{
				((ProfileScreen)AndromedaGui.mainWnd.activeWindow).PopulateData(NetworkScript.player.myPlayerProfileInfo);
				((ProfileScreen)AndromedaGui.mainWnd.activeWindow).PopulateAddToListResponse(0, playerProfile.userName);
			}
			if (__ChatWindow.wnd != null && (__ChatWindow.wnd.tbSendMessage.text.Contains("/ignore") || __ChatWindow.wnd.tbSendMessage.text.Contains("/IGNORE")))
			{
				__ChatWindow.wnd.ProcessFriendResponce(0, playerProfile.userName);
			}
		}
	}

	private void DoAddToFriend(UdpCommHeader header)
	{
		playWebGame.LogMixPanel(MixPanelEvents.AddToFriends, null);
		if (header.data is GenericData)
		{
			GenericData genericDatum = (GenericData)header.data;
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 17 && NetworkScript.player.playerBelongings.playerName == ProfileScreen.playerUserName)
			{
				((ProfileScreen)AndromedaGui.mainWnd.activeWindow).PopulateData(NetworkScript.player.myPlayerProfileInfo);
				((ProfileScreen)AndromedaGui.mainWnd.activeWindow).PopulateAddToListResponse((byte)genericDatum.int1, genericDatum.str1);
			}
			if (__ChatWindow.wnd != null && (__ChatWindow.wnd.tbSendMessage.text.Contains("/friend") || __ChatWindow.wnd.tbSendMessage.text.Contains("/FRIEND")))
			{
				__ChatWindow.wnd.ProcessFriendResponce((byte)genericDatum.int1, genericDatum.str1);
			}
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 33 && SendGiftsWindow.isOnReceiverSelectScreen)
			{
				((SendGiftsWindow)AndromedaGui.mainWnd.activeWindow).PopulateAddToListResponse((byte)genericDatum.int1, genericDatum.str1);
			}
		}
		else if (header.data is PlayerProfile)
		{
			PlayerProfile playerProfile = (PlayerProfile)header.data;
			NetworkScript.player.myFriends.Add(playerProfile.userName, playerProfile);
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 17 && NetworkScript.player.playerBelongings.playerName == ProfileScreen.playerUserName)
			{
				((ProfileScreen)AndromedaGui.mainWnd.activeWindow).PopulateData(NetworkScript.player.myPlayerProfileInfo);
				((ProfileScreen)AndromedaGui.mainWnd.activeWindow).PopulateAddToListResponse(0, playerProfile.userName);
			}
			if (__ChatWindow.wnd != null && (__ChatWindow.wnd.tbSendMessage.text.Contains("/friend") || __ChatWindow.wnd.tbSendMessage.text.Contains("/FRIEND")))
			{
				__ChatWindow.wnd.ProcessFriendResponce(0, playerProfile.userName);
			}
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 33 && SendGiftsWindow.isOnReceiverSelectScreen)
			{
				((SendGiftsWindow)AndromedaGui.mainWnd.activeWindow).PopulateScreen();
				((SendGiftsWindow)AndromedaGui.mainWnd.activeWindow).PopulateAddToListResponse(0, playerProfile.userName);
			}
		}
	}

	private void DoBuyShip(UdpCommHeader h)
	{
		PlayerBelongings playerBelonging = (PlayerBelongings)h.data;
		NetworkScript.player.playerBelongings.lastPurchaseId = playerBelonging.lastPurchaseId;
		NetworkScript.player.playerBelongings.playerShips = playerBelonging.playerShips;
	}

	private void DoCastSkill(UdpCommHeader header)
	{
		NetworkScript.<DoCastSkill>c__AnonStorey4B variable = null;
		this.isAlreadyInStealt = false;
		ActiveSkillObject activeSkillObject = (ActiveSkillObject)header.data;
		GameObjectPhysics item = null;
		if (activeSkillObject.targetNeibId != 0)
		{
			if (!this.gameObjects.ContainsKey(activeSkillObject.targetNeibId))
			{
				return;
			}
			item = this.gameObjects.get_Item(activeSkillObject.targetNeibId);
			if (item.get_IsPoP() && ((PlayerObjectPhysics)item).isInStealthMode)
			{
				this.isAlreadyInStealt = true;
			}
		}
		else
		{
			item = null;
		}
		if (this.gameObjects.ContainsKey(activeSkillObject.casterNeibId) && activeSkillObject.skillId != PlayerItems.TypeTalentsRepairingDrones && activeSkillObject.skillId != PlayerItems.TypeTalentsRocketBarrage && activeSkillObject.skillId != PlayerItems.TypeCouncilSkillDisarm && activeSkillObject.skillId != PlayerItems.TypeCouncilSkillSacrifice && activeSkillObject.skillId != PlayerItems.TypeCouncilSkillLifesteal)
		{
			Vector3 vector3 = new Vector3(this.gameObjects.get_Item(activeSkillObject.casterNeibId).x, this.gameObjects.get_Item(activeSkillObject.casterNeibId).y, this.gameObjects.get_Item(activeSkillObject.casterNeibId).z);
			string str = string.Concat("Skill", activeSkillObject.assetName);
			AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", str);
			AudioManager.PlayAudioClip(fromStaticSet, vector3);
		}
		if (activeSkillObject.casterNeibId == NetworkScript.player.vessel.neighbourhoodId || this.gameObjects.ContainsKey(activeSkillObject.casterNeibId))
		{
			PlayerObjectPhysics playerObjectPhysic = (PlayerObjectPhysics)this.gameObjects.get_Item(activeSkillObject.casterNeibId);
			if (playerObjectPhysic.playerId == NetworkScript.player.playId || !playerObjectPhysic.isInStealthMode || NetworkScript.IsPartyMember(playerObjectPhysic.playerId) || !this.IsNonBreakingStealthSkill(activeSkillObject.skillId))
			{
				TalentsInfo talentsInfo = (TalentsInfo)Enumerable.First<PlayerItemTypesData>(Enumerable.Where<PlayerItemTypesData>(StaticData.allTypes.get_Values(), new Func<PlayerItemTypesData, bool>(variable, (PlayerItemTypesData t) => t.itemType == this.serverPrm.skillId)));
				NetworkScript.spaceLabelManager.AddMessage(GuiNewStyleBar.orangeColor, StaticData.Translate(talentsInfo.uiName), playerObjectPhysic);
			}
		}
		if (activeSkillObject.casterNeibId == NetworkScript.player.vessel.neighbourhoodId)
		{
			ActiveSkillSlot activeSkillSlot = NetworkScript.player.playerBelongings.skillConfig.skillSlots.get_Item(activeSkillObject.skillSlotId);
			activeSkillSlot.nextCastTime = activeSkillObject.nextCastTime;
			AndromedaGui.mainWnd.ReloadSkillSlot(activeSkillSlot);
		}
		this.SpawnMovableActiveSkill(activeSkillObject);
		if (activeSkillObject.skillId == PlayerItems.TypeTalentsStealth)
		{
			if (this.isAlreadyInStealt)
			{
				return;
			}
			Shader shader = Shader.Find("ReflectiveBumpedSpecularTransparency");
			if (activeSkillObject.targetNeibId == NetworkScript.player.vessel.neighbourhoodId)
			{
				((GameObject)NetworkScript.player.vessel.gameObject).get_animation().Play("GoStealth30");
			}
			else if (!NetworkScript.IsPartyMember(((PlayerObjectPhysics)item).playerId))
			{
				if (NetworkScript.player.shipScript.selectedObject != null && NetworkScript.player.shipScript.selectedObject.neighbourhoodId == activeSkillObject.targetNeibId)
				{
					NetworkScript.player.shipScript.DeselectCurrentObject();
					if (NetworkScript.player.vessel.selectedPoPnbId != 0)
					{
						playWebGame.udp.ExecuteCommand(49, new UniversalTransportContainer(), 56);
						NetworkScript.player.vessel.selectedPoPnbId = 0;
					}
				}
				ParticleRenderer[] componentsInChildren = ((GameObject)item.gameObject).GetComponentsInChildren<ParticleRenderer>();
				for (int i = 0; i < (int)componentsInChildren.Length; i++)
				{
					componentsInChildren[i].set_enabled(false);
				}
				((GameObject)item.gameObject).get_renderer().get_material().set_shader(shader);
				((GameObject)item.gameObject).get_animation().Play("GoStealth0");
				NetworkScript.playerNameManager.removePOPName(item);
				this.HideNanoShield(item);
			}
			else
			{
				((GameObject)item.gameObject).get_animation().Play("GoStealth30");
			}
			return;
		}
		if (activeSkillObject.skillId == PlayerItems.TypeCouncilSkillLifesteal)
		{
			GameObjectPhysics gameObjectPhysic = null;
			GameObjectPhysics gameObjectPhysic1 = null;
			if (!this.gameObjects.TryGetValue(activeSkillObject.casterNeibId, ref gameObjectPhysic))
			{
				return;
			}
			if (!this.gameObjects.TryGetValue(activeSkillObject.targetNeibId, ref gameObjectPhysic1))
			{
				return;
			}
			NetworkScript.activeSkillManager.AddActiveSkill(activeSkillObject.skillId, 0f, 0f, 0f, activeSkillObject.animationLifetime, gameObjectPhysic1, 2);
			NetworkScript.activeSkillManager.AddActiveSkill(activeSkillObject.skillId, 0f, 0f, 0f, activeSkillObject.animationLifetime, gameObjectPhysic, 1);
			return;
		}
		if (activeSkillObject.skillId == PlayerItems.TypeTalentsFocusFire || activeSkillObject.skillId == PlayerItems.TypeTalentsForceWave || activeSkillObject.skillId == PlayerItems.TypeTalentsDecoy || activeSkillObject.skillId == PlayerItems.TypeTalentsPowerCut || activeSkillObject.skillId == PlayerItems.TypeTalentsNanoShield)
		{
			return;
		}
		if (activeSkillObject.skillId == PlayerItems.TypeTalentsRocketBarrage || activeSkillObject.skillId == PlayerItems.TypeTalentsForceWave || activeSkillObject.skillId == PlayerItems.TypeTalentsPulseNova)
		{
			NetworkScript.activeSkillManager.AddActiveSkill(activeSkillObject.skillId, activeSkillObject.targetLocation.x, 0f, activeSkillObject.targetLocation.z, activeSkillObject.animationLifetime, item, 0);
		}
		else if (!this.IsNonBreakingStealthSkill(activeSkillObject.skillId) || !item.get_IsPoP() || !((PlayerObjectPhysics)item).isInStealthMode || NetworkScript.IsPartyMember(((PlayerObjectPhysics)item).playerId) || ((PlayerObjectPhysics)item).playerId == NetworkScript.player.playId)
		{
			NetworkScript.activeSkillManager.AddActiveSkill(activeSkillObject.skillId, 0f, 0f, 0f, activeSkillObject.animationLifetime, item, 0);
		}
	}

	private void DoChangeDisarmState(long playId, bool isDisarmed)
	{
		PlayerDataEx playerDataEx = null;
		if (!NetworkScript.clientSideClientsList.TryGetValue(playId, ref playerDataEx))
		{
			return;
		}
		playerDataEx.shipScript.p.ChangeDisarmState(isDisarmed);
	}

	private void DoChangeGalaxy(UdpCommHeader h)
	{
		Debug.Log("DoChangeGalaxy");
		ChangeGalaxyParams changeGalaxyParam = (ChangeGalaxyParams)h.data;
		if (h.playerId == NetworkScript.player.vessel.playerId)
		{
			playWebGame.udp.onError = null;
			playWebGame.authorization.galaxyServerPort = changeGalaxyParam.newGalaxyPort;
			playWebGame.authorization.galaxyId = changeGalaxyParam.newGalaxy;
			playWebGame.udp.serverPort = changeGalaxyParam.newGalaxyPort;
			LevelMap[] levelMapArray = StaticData.allGalaxies;
			if (NetworkScript.<>f__am$cache47 == null)
			{
				NetworkScript.<>f__am$cache47 = new Func<LevelMap, bool>(null, (LevelMap t) => t.get_galaxyId() == playWebGame.authorization.galaxyId);
			}
			LevelMap levelMap = Enumerable.First<LevelMap>(Enumerable.Where<LevelMap>(levelMapArray, NetworkScript.<>f__am$cache47));
			playWebGame.LoadScene(levelMap.scenename);
		}
		else if (changeGalaxyParam.oldGalaxy == this.galaxy.get_galaxyId())
		{
			NetworkScript.clientSideClientsList.Remove(h.playerId);
		}
	}

	private void DoChangeShockState(long playId, bool isShocked)
	{
		PlayerDataEx playerDataEx = null;
		if (!NetworkScript.clientSideClientsList.TryGetValue(playId, ref playerDataEx))
		{
			return;
		}
		playerDataEx.shipScript.p.ChangeShockState(isShocked);
	}

	private void DoChangeSpeed(long playId, float newSpeed)
	{
		PlayerDataEx playerDataEx = null;
		if (!NetworkScript.clientSideClientsList.TryGetValue(playId, ref playerDataEx))
		{
			return;
		}
		playerDataEx.shipScript.p.ChangeSpeed(newSpeed, false);
	}

	private void DoChangeSpeed(long playId, float newSpeed, bool isBoosterActive)
	{
		PlayerDataEx playerDataEx = null;
		if (!NetworkScript.clientSideClientsList.TryGetValue(playId, ref playerDataEx))
		{
			return;
		}
		if (!isBoosterActive)
		{
			playerDataEx.shipScript.StopSpeedBooster();
		}
		else
		{
			playerDataEx.shipScript.InitSpeedBoost();
		}
		playerDataEx.shipScript.p.isSpeedBoostActivated = isBoosterActive;
		playerDataEx.shipScript.p.ChangeSpeed(newSpeed, false);
	}

	private void DoChangeStunState(long playId, bool isStunned)
	{
		PlayerDataEx playerDataEx = null;
		if (!NetworkScript.clientSideClientsList.TryGetValue(playId, ref playerDataEx))
		{
			return;
		}
		playerDataEx.shipScript.p.ChangeStunState(isStunned);
	}

	private void DoCriticalHit(long playerId, uint targetNbId, float dmg)
	{
		PlayerDataEx playerDataEx = null;
		if (!NetworkScript.clientSideClientsList.TryGetValue(playerId, ref playerDataEx))
		{
			return;
		}
		GameObject gameObject = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("Critical_pfb"));
		TextMesh componentInChildren = gameObject.GetComponentInChildren<TextMesh>();
		componentInChildren.set_text(string.Format(StaticData.Translate("key_critical_lbl"), dmg.ToString("#,##0")));
		TrackingPoPScript component = gameObject.GetComponent<TrackingPoPScript>();
		component.target = playerDataEx.shipScript.p;
		component.timeToDestroy = 2f;
		component.deltaY = 2.4f;
		GameObjectPhysics gameObjectPhysic = null;
		if (!this.gameObjects.TryGetValue(targetNbId, ref gameObjectPhysic))
		{
			return;
		}
		GameObject gameObject1 = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("SkillLaserCritical_pfb"));
		TrackingPoPScript trackingPoPScript = gameObject1.GetComponent<TrackingPoPScript>();
		trackingPoPScript.target = (PlayerObjectPhysics)gameObjectPhysic;
		trackingPoPScript.timeToDestroy = 1.2f;
		trackingPoPScript.deltaY = 1.2f;
		trackingPoPScript.stayAliveOnPopDestroy = true;
		if (GuiFramework.masterVolume != 0f && GuiFramework.fxVolume != 0f)
		{
			AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "criticalHit");
			AudioManager.PlayAudioClip(fromStaticSet, new Vector3(gameObjectPhysic.x, gameObjectPhysic.y, gameObjectPhysic.z));
		}
	}

	private void DoDespawnPVE(UdpCommHeader header)
	{
		UniversalTransportContainer universalTransportContainer = (UniversalTransportContainer)header.data;
		if (this.gameObjects.ContainsKey(universalTransportContainer.neighbourhoodId))
		{
			GameObjectPhysics item = this.gameObjects.get_Item(universalTransportContainer.neighbourhoodId);
			NetworkScript.Expode(new Vector3(item.x, item.y, item.z));
		}
	}

	private void DoGuildInsufficientMasters(UdpCommHeader header)
	{
		if (AndromedaGui.mainWnd.activWindowIndex == 18)
		{
			((GuildWindow)AndromedaGui.mainWnd.activeWindow).DoGuildInsufficientMasters(header.data, header.context);
		}
	}

	private void DoGuildRanking(UdpCommHeader header)
	{
		if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 8)
		{
			List<Guild> list = ((UniversalTransportContainer)header.data).guilds;
			if (NetworkScript.<>f__am$cache35 == null)
			{
				NetworkScript.<>f__am$cache35 = new Func<Guild, long>(null, (Guild o) => o.bankUltralibrium);
			}
			IOrderedEnumerable<Guild> orderedEnumerable = Enumerable.OrderByDescending<Guild, long>(list, NetworkScript.<>f__am$cache35);
			if (NetworkScript.<>f__am$cache36 == null)
			{
				NetworkScript.<>f__am$cache36 = new Func<Guild, string>(null, (Guild t) => t.name);
			}
			RankingWindow.guilds = Enumerable.ToList<Guild>(Enumerable.ThenBy<Guild, string>(orderedEnumerable, NetworkScript.<>f__am$cache36));
			((RankingWindow)AndromedaGui.mainWnd.activeWindow).RedrawDataGuilds();
		}
	}

	private void DoGuildUpdate(UdpCommHeader header)
	{
		// 
		// Current member / type: System.Void NetworkScript::DoGuildUpdate(TransferableObjects.UdpCommHeader)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void DoGuildUpdate(TransferableObjects.UdpCommHeader)
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

	private void DoGuildUpgrade(UdpCommHeader header)
	{
		Guild guild = (Guild)header.data;
		if (NetworkScript.player.guild != null)
		{
			NetworkScript.player.guild.upgradeOneLevel = guild.upgradeOneLevel;
			NetworkScript.player.guild.upgradeTwoLevel = guild.upgradeTwoLevel;
			NetworkScript.player.guild.upgradeThreeLevel = guild.upgradeThreeLevel;
			NetworkScript.player.guild.upgradeFourLevel = guild.upgradeFourLevel;
			NetworkScript.player.guild.upgradeFiveLevel = guild.upgradeFiveLevel;
		}
		if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 18)
		{
			((GuildWindow)AndromedaGui.mainWnd.activeWindow).PopulateGuildUpgradeMenu(guild);
		}
	}

	private void DoInPartUpdate(UdpCommHeader h)
	{
		GenericData genericDatum = (GenericData)h.data;
		if (NetworkScript.clientSideClientsList.ContainsKey(genericDatum.long1))
		{
			NetworkScript.clientSideClientsList.get_Item(genericDatum.long1).vessel.isInParty = genericDatum.bool1;
		}
	}

	private void DoLearnedTalent(UdpCommHeader header)
	{
		LearnedActiveSkillData learnedActiveSkillDatum = (LearnedActiveSkillData)header.data;
		NetworkScript.player.playerBelongings.skillConfig = learnedActiveSkillDatum.skillSlots;
		AndromedaGui.gui.RemoveWindow(AndromedaGui.mainWnd.quickSlotsWindow.handler);
		AndromedaGui.mainWnd.CreateQuickSlotsMenu();
	}

	private void DoMoveShip(UdpCommHeader h)
	{
		StartMoveShipData startMoveShipDatum = (StartMoveShipData)h.data;
		PlayerDataEx item = null;
		if (NetworkScript.clientSideClientsList.ContainsKey(startMoveShipDatum.movingPlayerId))
		{
			item = NetworkScript.clientSideClientsList.get_Item(startMoveShipDatum.movingPlayerId);
		}
		if (item == null)
		{
			return;
		}
		if (NetworkScript.player.playId != item.playId)
		{
			item.vessel.x = startMoveShipDatum.currentX;
			item.vessel.z = startMoveShipDatum.currentZ;
			item.vessel.destinationX = startMoveShipDatum.destinationX;
			item.vessel.destinationZ = startMoveShipDatum.destinationZ;
			item.vessel.destinationY = 0f;
			item.vessel.rotationY = startMoveShipDatum.rotationY;
			item.vessel.isStunned = startMoveShipDatum.isStunned;
			item.vessel.speed = startMoveShipDatum.momentSpeed;
			item.vessel.StartRotate();
			item.vessel.StartMove();
		}
		else
		{
			item.vessel.isStunned = startMoveShipDatum.isStunned;
			float single = startMoveShipDatum.currentX - item.vessel.x;
			float single1 = startMoveShipDatum.currentZ - item.vessel.z;
			if (Mathf.Sqrt(single * single + single1 * single1) > NetworkScript.MOVE_LAG_LIMIT)
			{
				Debug.Log("Move Desync!");
				item.vessel.x = startMoveShipDatum.currentX;
				item.vessel.z = startMoveShipDatum.currentZ;
			}
		}
	}

	private void DoPlayerMultiKill(UdpCommHeader header)
	{
		if (NetworkScript.player.vessel.galaxy.get_galaxyId() == 1000)
		{
			return;
		}
		if (this.lastNotificationPacketSeq == header.packetSeq)
		{
			return;
		}
		this.lastNotificationPacketSeq = header.packetSeq;
		UniversalTransportContainer universalTransportContainer = (UniversalTransportContainer)header.data;
		PlayerDataEx playerDataEx = null;
		if (!NetworkScript.clientSideClientsList.TryGetValue(universalTransportContainer.playerId, ref playerDataEx))
		{
			return;
		}
		string empty = string.Empty;
		switch (universalTransportContainer.killCount)
		{
			case 2:
			{
				empty = "Double Kill!";
				break;
			}
			case 3:
			{
				empty = "Triple Kill!";
				break;
			}
			case 4:
			{
				empty = "Multi Kill!";
				break;
			}
			case 5:
			{
				empty = "Ultra Kill!";
				break;
			}
			case 6:
			{
				empty = "Killing Spree!";
				break;
			}
			case 7:
			{
				empty = "Unstoppable!";
				break;
			}
			case 8:
			{
				empty = "Devastation!";
				break;
			}
			case 9:
			{
				empty = "Massacre!";
				break;
			}
			default:
			{
				empty = "GODLIKE!";
				break;
			}
		}
		if (universalTransportContainer.killCount > 10)
		{
			int num = universalTransportContainer.killCount - 9;
			empty = string.Concat(empty, "  X", num.ToString());
		}
		empty = string.Concat(empty, string.Format("\n+{0} xp", universalTransportContainer.expReward));
		AssetManager assetManager = playWebGame.assets;
		int num1 = Mathf.Min(universalTransportContainer.killCount - 1, 9);
		GameObject gameObject = (GameObject)Object.Instantiate(assetManager.GetPrefab(string.Concat("Multikills/MultiKill_", num1.ToString())));
		gameObject.get_transform().set_position(new Vector3(playerDataEx.vessel.x, 1.5f, playerDataEx.vessel.z));
		MultiKillAnimationScript component = gameObject.GetComponent<MultiKillAnimationScript>();
		component.target = playerDataEx.vessel;
		component.killCnt = universalTransportContainer.killCount;
		if (universalTransportContainer.playerId != NetworkScript.player.playId)
		{
			component.playSound = false;
		}
		TextMesh componentInChildren = gameObject.GetComponentInChildren<TextMesh>();
		componentInChildren.get_renderer().get_material().set_color(GuiNewStyleBar.orangeColor);
		componentInChildren.set_text(empty);
	}

	private void DoPutProjectiles(UdpCommHeader h)
	{
		uint num = 0;
		if (NetworkScript.player != null && NetworkScript.player.vessel != null)
		{
			num = NetworkScript.player.vessel.neighbourhoodId;
		}
		FusilladeData fusilladeDatum = (FusilladeData)h.data;
		for (int i = 0; i < (int)fusilladeDatum.projectiles.Length; i++)
		{
			if (this.gameObjects.ContainsKey(fusilladeDatum.projectiles[i].shooterNeibId))
			{
				PlayerObjectPhysics item = (PlayerObjectPhysics)this.gameObjects.get_Item(fusilladeDatum.projectiles[i].shooterNeibId);
				item.timeOfLastCombat = DateTime.get_Now();
			}
			if (!this.gameObjects.ContainsKey(fusilladeDatum.projectiles[i].neighbourhoodId))
			{
				this.SpawnProjectile(fusilladeDatum.projectiles[i]);
			}
			if (fusilladeDatum.projectiles[i].shooterNeibId == num)
			{
				NetworkScript.player.playerBelongings.playerItems.SpendAmmo(fusilladeDatum.projectiles[i].selectedAmmoType);
				DateTime now = DateTime.get_Now();
				PlayerObjectPhysics ticks = NetworkScript.player.vessel;
				int num1 = fusilladeDatum.projectiles[i].weaponSlotId;
				ticks.cfg.weaponSlots[num1].lastShotTime = now.get_Ticks();
				ticks.cfg.weaponSlots[num1].timeToNextShot = fusilladeDatum.cooldownTimes[num1] * 10000;
				ticks.cfg.weaponSlots[num1].realReloadTime = (long)ticks.cfg.weaponSlots[num1].timeToNextShot;
				AndromedaGui.mainWnd.ReloadWeapon(ticks.cfg.weaponSlots[num1]);
			}
		}
	}

	private void DoPvPGameDispose(UdpCommHeader header)
	{
		NetworkScript.player.vessel.pvpState = 0;
		NetworkScript.player.pvpGame = null;
		NetworkScript.player.pvpGameTypeSignedFor = 0;
		NetworkScript.player.vessel.teamNumber = 0;
		if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 15)
		{
			AndromedaGui.mainWnd.CloseActiveWindow();
		}
		if (NetworkScript.party != null && AndromedaGui.personalStatsWnd != null)
		{
			AndromedaGui.personalStatsWnd.UpdateParty();
		}
	}

	private void DoPvPGameOver(UdpCommHeader header)
	{
		if (this.lastNotificationPacketSeq == header.packetSeq)
		{
			return;
		}
		this.lastNotificationPacketSeq = header.packetSeq;
		this.DoPvPGameUpdate(header);
		PlayerObjectPhysics playerObjectPhysic = NetworkScript.player.vessel;
		List<PvPStatRow> list = NetworkScript.player.pvpGame.stats;
		if (NetworkScript.<>f__am$cache38 == null)
		{
			NetworkScript.<>f__am$cache38 = new Func<PvPStatRow, bool>(null, (PvPStatRow r) => r.playerId == NetworkScript.player.playId);
		}
		playerObjectPhysic.pvpState = Enumerable.First<PvPStatRow>(Enumerable.Where<PvPStatRow>(list, NetworkScript.<>f__am$cache38)).state;
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("game_name", NetworkScript.player.pvpGame.gameType.name);
		dictionary.Add("game_mod", NetworkScript.player.pvpGame.gameType.mode);
		playWebGame.LogMixPanel(MixPanelEvents.EndPvPgame, dictionary);
		base.StartCoroutine(NetworkScript.PlayPvPEndGameSound());
		if (AndromedaGui.mainWnd.activWindowIndex == 15)
		{
			((PVPWindow)AndromedaGui.mainWnd.activeWindow).PopulatePvPWindowInArenaGame();
		}
		if (NetworkScript.player.pvpGame.gameType.selectedMap.galaxyId == NetworkScript.player.vessel.galaxy.__galaxyId && AndromedaGui.mainWnd.activWindowIndex != 15)
		{
			EventHandlerParam eventHandlerParam = new EventHandlerParam()
			{
				customData = (byte)15
			};
			AndromedaGui.mainWnd.OnWindowBtnClicked(eventHandlerParam);
		}
	}

	private void DoPvPGameUpdate(UdpCommHeader header)
	{
		PvPGame pvPGame = (PvPGame)header.data;
		NetworkScript.player.pvpGame = pvPGame;
		if ((NetworkScript.player.vessel.pvpState == 3 || NetworkScript.player.vessel.pvpState == 4) && NetworkScript.player.pvpGame.gameType.selectedMap != null && NetworkScript.player.pvpGame.gameType.selectedMap.galaxyId == NetworkScript.player.vessel.galaxy.__galaxyId)
		{
			AndromedaGui.mainWnd.PopulatePvPDominationGame(0);
		}
		if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 15)
		{
			((PVPWindow)AndromedaGui.mainWnd.activeWindow).PopulatePvPWindowInArenaGame();
		}
	}

	private void DoRebuildCfgFromServerOrder(PlayerBelongings data)
	{
		WeaponSlot[] weaponSlotArray = NetworkScript.player.cfg.weaponSlots;
		SortedList<int, PlayerPendingAward> sortedList = NetworkScript.player.playerBelongings.playerAwards;
		NetworkScript.player.playerBelongings = data;
		NetworkScript.player.playerBelongings.playerAwards = sortedList;
		NetworkScript.player.cfg = NetworkScript.player.playerBelongings.BuildCfg(NetworkScript.player.guild);
		for (int i = 0; i < 6; i++)
		{
			NetworkScript.player.cfg.weaponSlots[i].lastShotTime = weaponSlotArray[i].lastShotTime;
		}
		NetworkScript.player.vessel.cfg = NetworkScript.player.cfg;
		if (NetworkScript.player.shipScript != null)
		{
			NetworkScript.player.shipScript.get_gameObject().GetComponent<ShipStatsGuiRelativeToGameObject>().Populate();
		}
		if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 32)
		{
			((PowerUpsWindow)AndromedaGui.mainWnd.activeWindow).Populate();
		}
		if (AndromedaGui.mainWnd != null)
		{
			AndromedaGui.mainWnd.Populate();
			if (AndromedaGui.mainWnd.activWindowIndex == 11)
			{
				((NovaShop)AndromedaGui.mainWnd.activeWindow).PopulateBoosters();
			}
		}
		if (NetworkScript.isInBase && AndromedaGui.inBaseActiveWnd != null)
		{
			if (AndromedaGui.inBaseActiveWnd is NovaShop)
			{
				((NovaShop)AndromedaGui.inBaseActiveWnd).PopulateBoosters();
			}
			else if (AndromedaGui.inBaseActiveWnd is __MerchantWindow)
			{
				((__MerchantWindow)AndromedaGui.inBaseActiveWnd).RefreshScreen();
			}
		}
	}

	private void DoReceiveTransformerReward(UdpCommHeader header)
	{
		UniversalTransportContainer universalTransportContainer = (UniversalTransportContainer)header.data;
		if (header.context == 55)
		{
			NetworkScript.player.playerBelongings.transformerState = universalTransportContainer.transformerState;
			if (NetworkScript.player.playerBelongings.transformerState == 1 && AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 22)
			{
				((TransformerWindow)AndromedaGui.mainWnd.activeWindow).PopulateAfterStateChenge();
			}
			return;
		}
		if (this.lastNotificationPacketSeq == header.packetSeq)
		{
			return;
		}
		this.lastNotificationPacketSeq = header.packetSeq;
		if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 22)
		{
			((TransformerWindow)AndromedaGui.mainWnd.activeWindow).PopulateRightButtonPanel();
		}
		NotificationManager.AddTransformerReward(universalTransportContainer.transformerRewardType, universalTransportContainer.transformerRewardAmount);
	}

	private void DoResurrectPlayer(UdpCommHeader header)
	{
		Debug.Log("DoResurrectPlayer");
		this.showErrorMessageBox = false;
		GenericData genericDatum = (GenericData)header.data;
		ushort num = (ushort)genericDatum.int1;
		short num1 = (short)genericDatum.int2;
		string str = genericDatum.str1;
		if (header.playerId != NetworkScript.player.playId)
		{
			if (!NetworkScript.clientSideClientsList.ContainsKey(header.playerId))
			{
				return;
			}
			this.RemoveGameObject(NetworkScript.clientSideClientsList.get_Item(header.playerId).vessel.neighbourhoodId);
		}
		else
		{
			playWebGame.udp.onError = null;
			playWebGame.authorization.galaxyServerPort = num;
			playWebGame.authorization.galaxyId = num1;
			playWebGame.udp.serverPort = num;
			if (!NetworkScript.isInBase)
			{
				if (NetworkScript.player.shipScript != null)
				{
					NetworkScript.player.shipScript.selectedObject = null;
					if (TargetingWnd.targetSectionIndex != 0)
					{
						TargetingWnd.Remove();
					}
					if (NetworkScript.player.shipScript._lock != null)
					{
						Object.Destroy(NetworkScript.player.shipScript._lock);
					}
				}
			}
			playWebGame.LoadScene(str);
			AndromedaGui.sectionShips = 0;
		}
	}

	private void DoSelectShip(UdpCommHeader h)
	{
		PlayerBelongings playerBelonging = (PlayerBelongings)h.data;
		NetworkScript.player.playerBelongings = playerBelonging;
		NetworkScript.player.cfg = NetworkScript.player.playerBelongings.BuildCfg(NetworkScript.player.guild);
		NetworkScript.player.vessel.cfg = NetworkScript.player.cfg;
		if (NetworkScript.isInBase && AndromedaGui.inBaseActiveWnd != null && AndromedaGui.inBaseActiveWnd is __DockWindow)
		{
			GameObject.Find("InBaseScript").GetComponent<InBaseScript>().ShipReset();
		}
		if (NetworkScript.isInBase && AndromedaGui.inBaseActiveWnd != null && AndromedaGui.inBaseActiveWnd is __MerchantWindow)
		{
			((__MerchantWindow)AndromedaGui.inBaseActiveWnd).PopulateCurrentShipStats();
		}
	}

	private void DoSignOutFromPVP(UdpCommHeader header)
	{
		// 
		// Current member / type: System.Void NetworkScript::DoSignOutFromPVP(TransferableObjects.UdpCommHeader)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void DoSignOutFromPVP(TransferableObjects.UdpCommHeader)
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

	private void DoSignUpForPVP(UdpCommHeader header)
	{
		// 
		// Current member / type: System.Void NetworkScript::DoSignUpForPVP(TransferableObjects.UdpCommHeader)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void DoSignUpForPVP(TransferableObjects.UdpCommHeader)
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

	private void DoSocialInteraction(UdpCommHeader header)
	{
		UniversalTransportContainer universalTransportContainer = (UniversalTransportContainer)header.data;
		PlayerDataEx playerDataEx = null;
		if (!NetworkScript.clientSideClientsList.TryGetValue(universalTransportContainer.playerId, ref playerDataEx))
		{
			return;
		}
		GameObject gameObject = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab(string.Concat("SocialInteraction/Social_", universalTransportContainer.socialInteractionIndex.ToString())));
		gameObject.get_transform().set_position(new Vector3(playerDataEx.vessel.x, -6.5f, playerDataEx.vessel.z));
		gameObject.GetComponent<SocialInteractionAnimationScript>().target = playerDataEx.vessel;
		Debug.Log(universalTransportContainer.socialInteractionIndex.ToString());
		if (universalTransportContainer.playerId != NetworkScript.player.playId)
		{
			Debug.Log("Not Me");
		}
		if (universalTransportContainer.socialInteractionIndex == 10)
		{
			Debug.Log("index 10");
			gameObject.GetComponentInChildren<TextMesh>().set_text(universalTransportContainer.socialInteractionCustomText);
		}
	}

	private void DoStartCountdown(UdpCommHeader header)
	{
		NetworkScript.player.vessel.pvpState = 2;
		this.InitCountdownWindow(15000);
	}

	private void DoStartEntaringBase(EnterBaseParams param)
	{
		GameObjectPhysics gameObjectPhysic = null;
		this.gameObjects.TryGetValue(param.baseId, ref gameObjectPhysic);
		if (gameObjectPhysic == null)
		{
			return;
		}
		if (NetworkScript.player.shipScript.selectedObject != null && NetworkScript.player.shipScript.selectedObject.neighbourhoodId == param.enteringPlrNbID)
		{
			NetworkScript.player.shipScript.DeselectCurrentObject();
			if (NetworkScript.player.vessel.selectedPoPnbId != 0)
			{
				playWebGame.udp.ExecuteCommand(49, new UniversalTransportContainer(), 56);
				NetworkScript.player.vessel.selectedPoPnbId = 0;
			}
		}
		if (NetworkScript.player.vessel.neighbourhoodId != param.enteringPlrNbID)
		{
			GameObjectPhysics gameObjectPhysic1 = null;
			this.gameObjects.TryGetValue(param.enteringPlrNbID, ref gameObjectPhysic1);
			if (gameObjectPhysic1 != null)
			{
				((PlayerObjectPhysics)gameObjectPhysic1).StartEnterBase((StarBaseNet)gameObjectPhysic, param.enteringDoor);
				NetworkScript.playerNameManager.removePOPName(gameObjectPhysic1);
			}
		}
		else
		{
			NetworkScript.player.vessel.onEnterBaseLastMove = new Action(this, NetworkScript.OnEnterBaseLastMove);
			NetworkScript.player.vessel.enteredInBase = new Action<PlayerObjectPhysics>(NetworkScript.player.shipScript, ShipScript.EnteredBase);
			NetworkScript.player.shipScript.isInControl = false;
			NetworkScript.player.vessel.StartEnterBase((StarBaseNet)gameObjectPhysic, param.enteringDoor);
			if (NetworkScript.player.shipScript.selectedObject != null)
			{
				NetworkScript.player.shipScript.DeselectCurrentObject();
				if (NetworkScript.player.vessel.selectedPoPnbId != 0)
				{
					playWebGame.udp.ExecuteCommand(49, new UniversalTransportContainer(), 56);
					NetworkScript.player.vessel.selectedPoPnbId = 0;
				}
			}
			NetworkScript.playerNameManager.removePOPName(NetworkScript.player.vessel);
			if (NetworkScript.player.shipScript.myShipBarBody != null)
			{
				Object.Destroy(NetworkScript.player.shipScript.myShipBarBody);
				Object.Destroy(NetworkScript.player.shipScript.myShipBarBlue);
				Object.Destroy(NetworkScript.player.shipScript.myShipBarGreen);
				Object.Destroy(NetworkScript.player.shipScript.myShipBarEnergy);
				NetworkScript.player.shipScript.myShipBarBody = null;
				NetworkScript.player.shipScript.myShipBarBlue = null;
				NetworkScript.player.shipScript.myShipBarGreen = null;
				NetworkScript.player.shipScript.myShipBarEnergy = null;
			}
		}
	}

	private void DoStartHyperJump(UdpCommHeader h)
	{
		HyperJumpParams hyperJumpParam = (HyperJumpParams)h.data;
		if (NetworkScript.player.vessel.playerId != hyperJumpParam.jumpingPlayerId)
		{
			if (!hyperJumpParam.isGalaxyJump)
			{
				ShipScript item = NetworkScript.clientSideClientsList.get_Item(hyperJumpParam.jumpingPlayerId).shipScript;
				item.p.StartHyperJump(item.hyperJumpInRange);
				item.EnterInHyperJump((GameObject)item.hyperJumpInRange.gameObject, item.get_gameObject());
				item.StartScaleIn();
			}
			else
			{
				NetworkScript.clientSideClientsList.get_Item(hyperJumpParam.jumpingPlayerId).shipScript.EnterInGalaxyJump();
			}
		}
		else if (!hyperJumpParam.isGalaxyJump)
		{
			NetworkScript.player.shipScript.StartEnterInHyperJump();
		}
		else
		{
			NetworkScript.player.shipScript.StartEnterInGalaxyJump();
		}
	}

	private void DoStartPlay(UdpCommHeader h)
	{
		// 
		// Current member / type: System.Void NetworkScript::DoStartPlay(TransferableObjects.UdpCommHeader)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void DoStartPlay(TransferableObjects.UdpCommHeader)
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

	private void DoStealthExit(UdpCommHeader header)
	{
		ActiveSkillParams activeSkillParam = (ActiveSkillParams)header.data;
		PlayerObjectPhysics item = null;
		if (!this.gameObjects.ContainsKey(activeSkillParam.casterNbId))
		{
			return;
		}
		item = (PlayerObjectPhysics)this.gameObjects.get_Item(activeSkillParam.casterNbId);
		ActiveSkillObject[] _activatedSkillsSafe = item.get_activatedSkillsSafe();
		if (NetworkScript.<>f__am$cache39 == null)
		{
			NetworkScript.<>f__am$cache39 = new Func<ActiveSkillObject, bool>(null, (ActiveSkillObject t) => t.skillId == PlayerItems.TypeTalentsStealth);
		}
		ActiveSkillObject activeSkillObject = Enumerable.FirstOrDefault<ActiveSkillObject>(Enumerable.Where<ActiveSkillObject>(_activatedSkillsSafe, NetworkScript.<>f__am$cache39));
		if (activeSkillObject != null)
		{
			activeSkillObject.RemoveSkillObject.Invoke(activeSkillObject);
			item.RemoveActivatedSkill(activeSkillObject);
		}
		item.isInStealthMode = false;
		this.ShowNanoShield(item);
		if (item.neighbourhoodId == NetworkScript.player.vessel.neighbourhoodId || NetworkScript.IsPartyMember(item.playerId))
		{
			((GameObject)item.gameObject).get_animation().Play("BackStealth30");
		}
		else
		{
			((GameObject)item.gameObject).get_animation().Play("BackStealth0");
			ParticleRenderer[] componentsInChildren = ((GameObject)item.gameObject).GetComponentsInChildren<ParticleRenderer>();
			for (int i = 0; i < (int)componentsInChildren.Length; i++)
			{
				componentsInChildren[i].set_enabled(true);
			}
		}
	}

	private void DoStunPlayer(UdpCommHeader header)
	{
		GenericData genericDatum = (GenericData)header.data;
		PlayerDataEx item = null;
		if (NetworkScript.clientSideClientsList.ContainsKey(genericDatum.long1))
		{
			item = NetworkScript.clientSideClientsList.get_Item(genericDatum.long1);
		}
		if (item == null)
		{
			return;
		}
		item.vessel.isStunned = true;
	}

	private void DoUnlockPortal(UdpCommHeader header)
	{
		UniversalTransportContainer universalTransportContainer = (UniversalTransportContainer)header.data;
		NetworkScript.player.playerBelongings.unlockedPortals = universalTransportContainer.unlockedPortals;
		if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 22)
		{
			((TransformerWindow)AndromedaGui.mainWnd.activeWindow).PopulateAfterUnlack();
		}
	}

	private void DoUpdateBelongings(UdpCommHeader h)
	{
		PlayerBelongings playerBelonging = (PlayerBelongings)h.data;
		NetworkScript.player.playerBelongings = playerBelonging;
		NetworkScript.player.cfg = playerBelonging.BuildCfg(NetworkScript.player.guild);
		NetworkScript.player.vessel.cfg = NetworkScript.player.cfg;
		AndromedaGui.mainWnd.RefreshInformationIcons();
	}

	private void DoUpdateBlacklist(UdpCommHeader header)
	{
		PlayerRelations playerRelation = (PlayerRelations)header.data;
		NetworkScript.player.myBlacklist.Clear();
		foreach (PlayerProfile player in playerRelation.players)
		{
			NetworkScript.player.myBlacklist.Add(player.userName, player);
		}
		if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 17 && ProfileScreen.playerUserName == NetworkScript.player.playerBelongings.playerName)
		{
			((ProfileScreen)AndromedaGui.mainWnd.activeWindow).PopulateData(NetworkScript.player.myPlayerProfileInfo);
		}
	}

	private void DoUpdateContributors(UdpCommHeader header)
	{
		ExtractionPoint extractionPoint = (ExtractionPoint)header.data;
		if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 19)
		{
			NewPoiScreenWindow newPoiScreenWindow = (NewPoiScreenWindow)AndromedaGui.mainWnd.activeWindow;
			List<Contributor> list = extractionPoint.topTenContributors;
			if (NetworkScript.<>f__am$cache37 == null)
			{
				NetworkScript.<>f__am$cache37 = new Func<Contributor, int>(null, (Contributor t) => t.tottalContribution);
			}
			newPoiScreenWindow.UpdateContributionList(Enumerable.ToList<Contributor>(Enumerable.OrderByDescending<Contributor, int>(list, NetworkScript.<>f__am$cache37)));
		}
	}

	private void DoUpdateDamages(UdpCommHeader h)
	{
		DamageUpdateItem[] damageUpdateItemArray = ((DamagesUpdate)h.data).damages;
		for (int i = 0; i < (int)damageUpdateItemArray.Length; i++)
		{
			DamageUpdateItem damageUpdateItem = damageUpdateItemArray[i];
			if (this.gameObjects.ContainsKey(damageUpdateItem.targetNbId))
			{
				GameObjectPhysics item = this.gameObjects.get_Item(damageUpdateItem.targetNbId);
				float single = (float)damageUpdateItem.damageHealth;
				float single1 = damageUpdateItem.damageShield;
				if (!item.get_IsPoP())
				{
					if (single + single1 > 0f)
					{
						this.ProjectileHitCorpus(this.gameObjects.get_Item(damageUpdateItem.targetNbId));
					}
					((ExtractionPoint)item).health = (float)damageUpdateItem.healthAfterHit;
					if (damageUpdateItem.isKill)
					{
						this.DestroyControlTower(item);
					}
				}
				else
				{
					PlayerObjectPhysics now = (PlayerObjectPhysics)item;
					if (!damageUpdateItem.isKill && damageUpdateItem.damageShield > 0f)
					{
						this.ProjectileHitShield(this.gameObjects.get_Item(damageUpdateItem.targetNbId), damageUpdateItem.projX, damageUpdateItem.projY, damageUpdateItem.projZ);
					}
					if (now.cfg.shield <= 0f && single > 0f)
					{
						this.ProjectileHitCorpus(this.gameObjects.get_Item(damageUpdateItem.targetNbId));
					}
					now.cfg.hitPoints = damageUpdateItem.healthAfterHit;
					now.cfg.shield = damageUpdateItem.shieldAfterHit;
					now.cfg.criticalEnergy = damageUpdateItem.criticalEnergy;
					if (now.neighbourhoodId == NetworkScript.player.vessel.neighbourhoodId && now.cfg.criticalEnergy >= now.cfg.criticalEnergyMax && AndromedaGui.mainWnd != null)
					{
						AndromedaGui.mainWnd.ShowCriticalIndication();
					}
					now.timeOfLastCombat = DateTime.get_Now();
					if (!now.get_IsPve())
					{
						if (NetworkScript.player.vessel.playerId == now.playerId)
						{
							NetworkScript.player.vessel.timeOfLastCombat = DateTime.get_Now();
							PlayerShipNet[] playerShipNetArray = NetworkScript.player.playerBelongings.playerShips;
							if (NetworkScript.<>f__am$cache3A == null)
							{
								NetworkScript.<>f__am$cache3A = new Func<PlayerShipNet, bool>(null, (PlayerShipNet s) => s.ShipID == NetworkScript.player.playerBelongings.selectedShipId);
							}
							PlayerShipNet playerShipNet = Enumerable.First<PlayerShipNet>(Enumerable.Where<PlayerShipNet>(playerShipNetArray, NetworkScript.<>f__am$cache3A));
							playerShipNet.CorpusHP = damageUpdateItem.healthAfterHit;
							playerShipNet.ShieldHP = (int)damageUpdateItem.shieldAfterHit;
						}
						if (damageUpdateItem.isKill)
						{
							NetworkScript.clientSideClientsList.get_Item(now.playerId).shipScript.GotKilled(now);
						}
					}
					else if (damageUpdateItem.isKill)
					{
						PvEPhysicsEx pvEPhysicsEx = (PvEPhysicsEx)this.gameObjects.get_Item(damageUpdateItem.targetNbId);
						pvEPhysicsEx.pveScript.GotKilled(pvEPhysicsEx);
					}
				}
				if (damageUpdateItem.isAbsorbed)
				{
					NetworkScript.spaceLabelManager.AddNewMessage(GuiNewStyleBar.blueColor, StaticData.Translate("key_damage_update_absorb"), item, 28);
				}
				if (single != 0f || single1 != 0f)
				{
					if (single > 0f)
					{
						NetworkScript.spaceLabelManager.AddNewMessage(GuiNewStyleBar.greenColor, string.Format("-{0:##,##0}", single), item, 28);
					}
					else if (single < 0f)
					{
						NetworkScript.spaceLabelManager.AddNewMessage(GuiNewStyleBar.greenColor, string.Format("+{0:##,##0}", -1f * single), item, 28);
					}
					if (single1 > 0f)
					{
						NetworkScript.spaceLabelManager.AddNewMessage(GuiNewStyleBar.blueColor, string.Format("-{0:##,##0}", single1), item, 28);
					}
					else if (single1 < 0f)
					{
						NetworkScript.spaceLabelManager.AddNewMessage(GuiNewStyleBar.blueColor, string.Format("+{0:##,##0}", -1f * single1), item, 28);
					}
				}
				else if (!damageUpdateItem.isAbsorbed)
				{
					NetworkScript.spaceLabelManager.AddNewMessage(GuiNewStyleBar.redColor, StaticData.Translate("key_damage_update_miss"), item, 28);
				}
			}
		}
	}

	private void DoUpdateExtractinPoint(UdpCommHeader header)
	{
		ExtractionPoint extractionPoint = (ExtractionPoint)header.data;
		if (this.gameObjects.ContainsKey(extractionPoint.neighbourhoodId))
		{
			ExtractionPoint item = (ExtractionPoint)this.gameObjects.get_Item(extractionPoint.neighbourhoodId);
			ExtractionPointState extractionPointState = item.state;
			byte num = item.upgradesHitPoints;
			byte num1 = item.upgradesPopulation;
			byte num2 = item.upgradesBarracks;
			byte num3 = item.upgradesUltralibrium;
			byte num4 = item.ownerFraction;
			byte num5 = item.upgradesAlien1;
			byte num6 = item.upgradesAlien2;
			byte num7 = item.upgradesAlien3;
			byte num8 = item.upgradesAlien4;
			byte num9 = item.upgradesAlien5;
			byte num10 = item.upgradesTurret1;
			byte num11 = item.upgradesTurret2;
			byte num12 = item.upgradesTurret3;
			byte num13 = item.upgradesTurret4;
			byte num14 = item.upgradesTurret5;
			extractionPoint.CopyPropsTo(item);
			ExtractionPointState extractionPointState1 = extractionPoint.state;
			byte num15 = extractionPoint.upgradesHitPoints;
			byte num16 = extractionPoint.upgradesPopulation;
			byte num17 = extractionPoint.upgradesBarracks;
			byte num18 = extractionPoint.upgradesUltralibrium;
			byte num19 = extractionPoint.ownerFraction;
			byte num20 = extractionPoint.upgradesAlien1;
			byte num21 = extractionPoint.upgradesAlien2;
			byte num22 = extractionPoint.upgradesAlien3;
			byte num23 = extractionPoint.upgradesAlien4;
			byte num24 = extractionPoint.upgradesAlien5;
			byte num25 = extractionPoint.upgradesTurret1;
			byte num26 = extractionPoint.upgradesTurret2;
			byte num27 = extractionPoint.upgradesTurret3;
			byte num28 = extractionPoint.upgradesTurret4;
			byte num29 = extractionPoint.upgradesTurret5;
			if (num4 != num19)
			{
				this.DestroyExtractionPoint(item);
				if (NetworkScript.player.shipScript.selectedObject != null && NetworkScript.player.shipScript.selectedObject.neighbourhoodId == extractionPoint.neighbourhoodId)
				{
					NetworkScript.player.shipScript.DeselectCurrentObject();
				}
			}
			else if (extractionPointState != extractionPointState1)
			{
				this.SwitchExtractionPointAlarm(item);
			}
			if (num < num15)
			{
				this.ExtractionPointUpgradeHP(num, num15, item);
			}
			if (num1 < num16)
			{
				this.ExtractionPointUpgradePOP(num1, num16, item);
			}
			if (num2 < num17)
			{
				this.ExtractionPointUpgradeBarracks(num2, num17, item);
			}
			if (num3 < num18)
			{
				this.ExtractionPointUpgradeMining(num3, num18, item);
			}
			if (num < num15 || num1 < num16 || num2 < num17 || num3 < num18 || num5 < num20 || num6 < num21 || num7 < num22 || num8 < num23 || num9 < num24 || num10 < num25 || num11 < num26 || num12 < num27 || num13 < num28 || num14 < num29)
			{
				AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "ep_upgraded");
				AudioManager.PlayAudioClip(fromStaticSet, new Vector3(item.x, 0f, item.z));
			}
			if (NetworkScript.player.shipScript.extractionPointInRange != null && extractionPoint.pointId == NetworkScript.player.shipScript.extractionPointInRange.pointId)
			{
				if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 19)
				{
					if (extractionPoint.ownerFraction == NetworkScript.player.vessel.fractionId)
					{
						((NewPoiScreenWindow)AndromedaGui.mainWnd.activeWindow).PoiWindowUpdate(null);
					}
					else
					{
						AndromedaGui.mainWnd.CloseActiveWindow();
					}
				}
				((GameObject)NetworkScript.player.shipScript.extractionPointInRange.gameObject).GetComponent<ExtractionPointGuiRelativeToGameObject>().Populate();
			}
		}
	}

	private void DoUpdateFractionOverview(UdpCommHeader header)
	{
		Debug.Log(string.Concat("DoUpdateFractionOverview ", header.context));
		Debug.Log(((UniversalTransportContainer)header.data).epsStateInfo);
		if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 7)
		{
			((UniverseMapScreenWindow)AndromedaGui.mainWnd.activeWindow).PopulateExtractionPointInfo(((UniversalTransportContainer)header.data).epsStateInfo);
		}
	}

	private void DoUpdateFriends(UdpCommHeader header)
	{
		PlayerRelations playerRelation = (PlayerRelations)header.data;
		NetworkScript.player.myFriends.Clear();
		foreach (PlayerProfile player in playerRelation.players)
		{
			NetworkScript.player.myFriends.Add(player.userName, player);
		}
		if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 17 && ProfileScreen.playerUserName == NetworkScript.player.playerBelongings.playerName)
		{
			((ProfileScreen)AndromedaGui.mainWnd.activeWindow).PopulateData(NetworkScript.player.myPlayerProfileInfo);
		}
		if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 33 && SendGiftsWindow.isOnReceiverSelectScreen)
		{
			((SendGiftsWindow)AndromedaGui.mainWnd.activeWindow).PopulateScreen();
		}
	}

	private void DoUpdateGameMapOverview(UdpCommHeader header)
	{
		if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 7)
		{
			__UniverseMap _UniverseMap = (__UniverseMap)AndromedaGui.mainWnd.activeWindow;
			if (__UniverseMap.subSection == 2)
			{
				_UniverseMap.PopulateGameMapInfo((UniversalTransportContainer)header.data);
			}
		}
	}

	private void DoUpdateItems(UdpCommHeader h)
	{
		PlayerItems playerItem = (PlayerItems)h.data;
		playerItem.RestoreAfterDeserialize();
		NetworkScript.player.playerBelongings.playerItems.Reload(playerItem);
		if (AndromedaGui.mainWnd != null)
		{
			if (AndromedaGui.mainWnd.activWindowIndex == 11)
			{
				((NovaShop)AndromedaGui.mainWnd.activeWindow).PopulateBoosters();
			}
			if (AndromedaGui.mainWnd.activWindowIndex == 22)
			{
				((TransformerWindow)AndromedaGui.mainWnd.activeWindow).Populate();
			}
			if (AndromedaGui.mainWnd.activWindowIndex == 34)
			{
				((WarScreenWindow)AndromedaGui.mainWnd.activeWindow).PopulateWarCommendation();
			}
		}
		if (NetworkScript.isInBase && AndromedaGui.inBaseActiveWnd != null && AndromedaGui.inBaseActiveWnd is NovaShop)
		{
			((NovaShop)AndromedaGui.inBaseActiveWnd).PopulateBoosters();
		}
		QuestTrackerWindow.UpdateBringToObjectivs();
	}

	private void DoUpdateMineralOwner(UdpCommHeader header)
	{
		GenericData genericDatum = (GenericData)header.data;
		if (this.gameObjects.ContainsKey((uint)genericDatum.long1))
		{
			((Mineral)this.gameObjects.get_Item((uint)genericDatum.long1)).ownerName = genericDatum.str1;
		}
	}

	private void DoUpdateNeighbourhood(UdpCommHeader header)
	{
		if (NetworkScript.isInBase)
		{
			return;
		}
		if (this.galaxy == null)
		{
			Debug.LogError(string.Format("{0:HH:mm:ss:ffff} seq={1} Update neighbourhood received before galaxy being set!", DateTime.get_Now(), header.packetSeq));
		}
		NeighbourhoodUpdate neighbourhoodUpdate = (NeighbourhoodUpdate)header.data;
		GameObjectPhysics[] gameObjectPhysicsArray = neighbourhoodUpdate.toAdd;
		for (int i = 0; i < (int)gameObjectPhysicsArray.Length; i++)
		{
			GameObjectPhysics gameObjectPhysic = gameObjectPhysicsArray[i];
			if (this.gameObjects.ContainsKey(gameObjectPhysic.neighbourhoodId))
			{
				GameObjectPhysics gameObjectPhysic1 = gameObjectPhysic;
				Monitor.Enter(gameObjectPhysic1);
				try
				{
					if (gameObjectPhysic is PvEPhysics)
					{
						PvEPhysics item = (PvEPhysics)this.gameObjects.get_Item(gameObjectPhysic.neighbourhoodId);
						PvEPhysics pvEPhysic = (PvEPhysics)gameObjectPhysic;
						if (pvEPhysic.pveCommand == 2)
						{
							item.cfg.hitPoints = pvEPhysic.cfg.hitPoints;
							item.cfg.shield = pvEPhysic.cfg.shield;
						}
						item.cfg.hitPointsMax = pvEPhysic.cfg.hitPointsMax;
						item.cfg.shieldMax = pvEPhysic.cfg.shieldMax;
						item.cfg.assetName = pvEPhysic.cfg.assetName;
						item.cfg.playerName = pvEPhysic.cfg.playerName;
						item.playerName = pvEPhysic.playerName;
						item.cfg.currentVelocity = pvEPhysic.cfg.currentVelocity;
						item.cfg.shield = pvEPhysic.cfg.shield;
						item.cfg.hitPoints = pvEPhysic.cfg.hitPoints;
						pvEPhysic.CopyPropsTo(item);
						item.currentAggroTarget = this.GetGameObjectOrNull(pvEPhysic.currentAggroPlayerNbId);
						item.normalAggressionTrackPlr = (PlayerObjectPhysics)this.GetGameObjectOrNull(pvEPhysic.nbIdNormalAggressionTrackPlr);
						item.shootingAt = this.GetGameObjectOrNull(pvEPhysic.nbIdShootingAt);
					}
					else if (!(gameObjectPhysic is DefenceTurret))
					{
						goto Label0;
					}
					else
					{
						DefenceTurret gameObjectOrNull = (DefenceTurret)this.gameObjects.get_Item(gameObjectPhysic.neighbourhoodId);
						DefenceTurret defenceTurret = (DefenceTurret)gameObjectPhysic;
						defenceTurret.CopyPropsTo(gameObjectOrNull);
						gameObjectOrNull.target = (PlayerObjectPhysics)this.GetGameObjectOrNull(defenceTurret.currentTargetNbId);
						if (gameObjectOrNull.target == null)
						{
							gameObjectOrNull.StartRotate();
						}
					}
				}
				finally
				{
					Monitor.Exit(gameObjectPhysic1);
				}
			}
			if (!(gameObjectPhysic is ProjectileObject) || NetworkScript.player == null || NetworkScript.player.vessel == null || ((ProjectileObject)gameObjectPhysic).shooterNeibId != NetworkScript.player.vessel.neighbourhoodId)
			{
				this.SpawnGameObject(gameObjectPhysic);
			}
		Label0:
		}
		uint[] numArray = neighbourhoodUpdate.toRemove;
		for (int j = 0; j < (int)numArray.Length; j++)
		{
			this.RemoveGameObject(numArray[j]);
		}
	}

	private void DoUpdatePartyInvitee(UdpCommHeader header)
	{
		if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 17 && ProfileScreen.playerUserName != NetworkScript.player.playerBelongings.playerName)
		{
			((ProfileScreen)AndromedaGui.mainWnd.activeWindow).PopulateData(null);
		}
		if (__ChatWindow.wnd != null)
		{
			__ChatWindow.wnd.CheckPartyAndGuild();
		}
	}

	private void DoUpdatePlayerConfig(UdpCommHeader h)
	{
		if (h.data is ShipConfiguration)
		{
			uint num = (uint)h.playerId;
			ShipConfiguration shipConfiguration = (ShipConfiguration)h.data;
			if (this.gameObjects.ContainsKey(num))
			{
				PlayerObjectPhysics item = (PlayerObjectPhysics)this.gameObjects.get_Item(num);
				item.cfg = shipConfiguration;
				if (NetworkScript.clientSideClientsList.ContainsKey(item.playerId))
				{
					NetworkScript.clientSideClientsList.get_Item(item.playerId).shipScript.p.ChangeSpeed(shipConfiguration.currentVelocity, false);
					ShipStatsGuiRelativeToGameObject component = NetworkScript.clientSideClientsList.get_Item(item.playerId).shipScript.get_gameObject().GetComponent<ShipStatsGuiRelativeToGameObject>();
					component.Populate();
				}
			}
		}
		if (h.data is GenericData)
		{
			GenericData genericDatum = (GenericData)h.data;
			if (genericDatum.long1 != NetworkScript.player.vessel.playerId && NetworkScript.clientSideClientsList.ContainsKey(genericDatum.long1))
			{
				PlayerDataEx playerDataEx = NetworkScript.clientSideClientsList.get_Item(genericDatum.long1);
				playerDataEx.vessel.guildTag = genericDatum.str1;
				playerDataEx.shipScript.get_gameObject().GetComponent<ShipStatsGuiRelativeToGameObject>().Populate();
			}
			else if (genericDatum.long1 == NetworkScript.player.vessel.playerId)
			{
				Color color = GuiNewStyleBar.blueColor;
				NetworkScript.player.vessel.guildTag = genericDatum.str1;
				NetworkScript.player.shipScript.get_gameObject().GetComponent<ShipStatsGuiRelativeToGameObject>().Populate();
			}
		}
	}

	private void DoUpdatePlayerProfile(UdpCommHeader header)
	{
		PlayerProfile playerProfile = (PlayerProfile)header.data;
		if (NetworkScript.player.playerBelongings.playerName == playerProfile.userName)
		{
			NetworkScript.player.myPlayerProfileInfo = playerProfile;
		}
		if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 17 && ProfileScreen.playerUserName == playerProfile.userName)
		{
			((ProfileScreen)AndromedaGui.mainWnd.activeWindow).PopulateData(playerProfile);
		}
		if (__ChatWindow.wnd != null)
		{
			__ChatWindow.wnd.PopulateData(playerProfile);
		}
	}

	private void DoUpdatePlayerRegistration(UdpCommHeader header)
	{
		// 
		// Current member / type: System.Void NetworkScript::DoUpdatePlayerRegistration(TransferableObjects.UdpCommHeader)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void DoUpdatePlayerRegistration(TransferableObjects.UdpCommHeader)
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

	private void DoUpdateReferalList(UdpCommHeader header)
	{
		RecrutedPlayers recrutedPlayer = (RecrutedPlayers)header.data;
		NetworkScript.player.playerBelongings.referals = recrutedPlayer;
	}

	private void DoUpdateSelecktedPoP(UdpCommHeader header)
	{
		UniversalTransportContainer universalTransportContainer = (UniversalTransportContainer)header.data;
		if (this.gameObjects.ContainsKey(universalTransportContainer.neighbourhoodId))
		{
			((PlayerObjectPhysics)this.gameObjects.get_Item(universalTransportContainer.neighbourhoodId)).selectedPoPnbId = universalTransportContainer.selectedPoPnbId;
		}
	}

	public void DummyShit(Exception ex, PlayerData pd)
	{
	}

	private void EqupItemIfPossible(SlotItem item)
	{
		// 
		// Current member / type: System.Void NetworkScript::EqupItemIfPossible(SlotItem)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void EqupItemIfPossible(SlotItem)
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

	private void ExecuteCustomActions()
	{
		Action<int>[] array = this.customActions.ToArray();
		for (int i = (int)array.Length - 1; i >= 0; i--)
		{
			array[i].Invoke(i);
		}
	}

	public static void Expode(Vector3 pos)
	{
		try
		{
			int num = Random.Range(3, 9);
			string str = string.Format("explosion_{0}", num);
			if (str != string.Empty)
			{
				AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", str);
				AudioManager.PlayAudioClip(fromStaticSet, pos);
			}
			Object.Instantiate(playWebGame.assets.GetPrefab("ShipExplosion_pfb"), pos, Quaternion.get_identity());
		}
		catch (Exception exception)
		{
			Debug.LogError(string.Format("Expode() {0}", exception));
		}
	}

	private void ExtractionPointUpgradeBarracks(byte oldLvl, byte newLvl, ExtractionPoint point)
	{
		SortedList<string, Animation> sortedList = new SortedList<string, Animation>();
		Component[] componentsInChildren = ((GameObject)point.gameObject).GetComponentsInChildren(typeof(Animation));
		for (int i = 0; i < (int)componentsInChildren.Length; i++)
		{
			Animation animation = (Animation)componentsInChildren[i];
			if (!sortedList.ContainsKey(animation.get_name()))
			{
				sortedList.Add(animation.get_name(), animation);
			}
		}
		for (int j = oldLvl + 1; j <= point.upgradesBarracks; j++)
		{
			try
			{
				string str = string.Format("ExtractionBaseBarracks{0}", j);
				Animation item = sortedList.get_Item(str);
				string _name = item.get_clip().get_name();
				item.get_Item(_name).set_time(0f);
				item.get_Item(_name).set_speed(1f);
				item.Play(_name);
				GameObject gameObject = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("BuilderDrone_pfb"));
				gameObject.get_transform().set_position(new Vector3(point.x, 0f, point.z));
				Animation componentInChildren = (Animation)gameObject.GetComponentInChildren(typeof(Animation));
				string str1 = string.Format("DroneBarracks0{0}", j);
				componentInChildren.Play(str1);
				Object.Destroy(gameObject, 10f);
			}
			catch (Exception exception)
			{
				Debug.LogError(exception);
			}
		}
	}

	private void ExtractionPointUpgradeHP(byte oldLvl, byte newLvl, ExtractionPoint point)
	{
		SortedList<string, Animation> sortedList = new SortedList<string, Animation>();
		Component[] componentsInChildren = ((GameObject)point.gameObject).GetComponentsInChildren(typeof(Animation));
		for (int i = 0; i < (int)componentsInChildren.Length; i++)
		{
			Animation animation = (Animation)componentsInChildren[i];
			if (!sortedList.ContainsKey(animation.get_name()))
			{
				sortedList.Add(animation.get_name(), animation);
			}
		}
		for (int j = oldLvl + 1; j <= point.upgradesHitPoints; j++)
		{
			try
			{
				string str = string.Format("ExtractionBaseHP{0}", j);
				Animation item = sortedList.get_Item(str);
				string _name = item.get_clip().get_name();
				item.get_Item(_name).set_time(0f);
				item.get_Item(_name).set_speed(1f);
				item.Play(_name);
				GameObject gameObject = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("BuilderDrone_pfb"));
				gameObject.get_transform().set_position(new Vector3(point.x, 0f, point.z));
				Animation componentInChildren = (Animation)gameObject.GetComponentInChildren(typeof(Animation));
				string str1 = string.Format("DroneHitPoints0{0}", j);
				componentInChildren.Play(str1);
				Object.Destroy(gameObject, 10f);
			}
			catch (Exception exception)
			{
				Debug.LogError(exception);
			}
		}
	}

	private void ExtractionPointUpgradeMining(byte oldLvl, byte newLvl, ExtractionPoint point)
	{
		SortedList<string, Animation> sortedList = new SortedList<string, Animation>();
		Component[] componentsInChildren = ((GameObject)point.gameObject).GetComponentsInChildren(typeof(Animation));
		for (int i = 0; i < (int)componentsInChildren.Length; i++)
		{
			Animation animation = (Animation)componentsInChildren[i];
			if (!sortedList.ContainsKey(animation.get_name()))
			{
				sortedList.Add(animation.get_name(), animation);
			}
		}
		for (int j = oldLvl + 1; j <= point.upgradesUltralibrium; j++)
		{
			Animation item = null;
			string str = string.Format("Mining{0}", j);
			item = sortedList.get_Item(str);
			string str1 = string.Format("Mining0{0}", j);
			item.get_Item(str1).set_time(0f);
			item.get_Item(str1).set_speed(1f);
			item.Play(str1);
			if (j == 1)
			{
				string str2 = string.Format("Mining{0}Loop", j);
				sortedList.get_Item(str2).Play("Mining01Loop");
			}
			else if (j != 5)
			{
				item.PlayQueued(string.Concat(str1, "Loop"), 0);
			}
		}
	}

	private void ExtractionPointUpgradePOP(byte oldLvl, byte newLvl, ExtractionPoint point)
	{
		SortedList<string, Animation> sortedList = new SortedList<string, Animation>();
		Component[] componentsInChildren = ((GameObject)point.gameObject).GetComponentsInChildren(typeof(Animation));
		for (int i = 0; i < (int)componentsInChildren.Length; i++)
		{
			Animation animation = (Animation)componentsInChildren[i];
			if (!sortedList.ContainsKey(animation.get_name()))
			{
				sortedList.Add(animation.get_name(), animation);
			}
		}
		for (int j = oldLvl + 1; j <= point.upgradesPopulation; j++)
		{
			try
			{
				string str = string.Format("ExtractionBasePop{0}", j);
				Animation item = sortedList.get_Item(str);
				string _name = item.get_clip().get_name();
				item.get_Item(_name).set_time(0f);
				item.get_Item(_name).set_speed(1f);
				item.Play(_name);
				GameObject gameObject = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("BuilderDrone_pfb"));
				gameObject.get_transform().set_position(new Vector3(point.x, 0f, point.z));
				Animation componentInChildren = (Animation)gameObject.GetComponentInChildren(typeof(Animation));
				string str1 = string.Format("DronePopulation0{0}", j);
				componentInChildren.Play(str1);
				Object.Destroy(gameObject, 10f);
			}
			catch (Exception exception)
			{
				Debug.LogError(exception);
			}
		}
	}

	private ushort GetFirstFreeSlotOfType(SlotItem[] collection, ItemLocation locationType, int loopCount)
	{
		NetworkScript.<GetFirstFreeSlotOfType>c__AnonStorey4D variable = null;
		ushort num = 255;
		int num1 = 0;
		while (num1 < loopCount)
		{
			if (Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(collection, new Func<SlotItem, bool>(variable, (SlotItem it) => (it.get_Slot() != (ushort)this.i ? false : it.get_SlotType() == (byte)this.<>f__ref$78.locationType)))) != null)
			{
				num1++;
			}
			else
			{
				num = (ushort)num1;
				Debug.Log(string.Format("Free slot of type {0} found.Slot number {1}", locationType.ToString(), num));
				break;
			}
		}
		return num;
	}

	private ushort GetFirstFreeSlotOfWeapon(SlotItem[] collection, SlotItem item, string ShipTitle, out ItemLocation itemLocation)
	{
		NetworkScript.<GetFirstFreeSlotOfWeapon>c__AnonStorey4F variable = null;
		ushort num = 255;
		WeaponSlotBluePrint weaponSlotBluePrint = Enumerable.FirstOrDefault<WeaponSlotBluePrint>(Enumerable.Where<WeaponSlotBluePrint>(WeaponSlotBluePrint.allShipBluePrint, new Func<WeaponSlotBluePrint, bool>(variable, (WeaponSlotBluePrint bp) => bp.shipType == this.ShipTitle)));
		if (weaponSlotBluePrint == null)
		{
			Debug.Log(string.Concat("Wrong ship name used as argument on automatic equip after mining weapon.Ship name : ", ShipTitle));
			itemLocation = 14;
			return (ushort)255;
		}
		if (item.get_ItemType() == PlayerItems.TypeWeaponLaserTire1 || item.get_ItemType() == PlayerItems.TypeWeaponLaserTire2 || item.get_ItemType() == PlayerItems.TypeWeaponLaserTire3 || item.get_ItemType() == PlayerItems.TypeWeaponLaserTire4 || item.get_ItemType() == PlayerItems.TypeWeaponLaserTire5)
		{
			if (weaponSlotBluePrint.isSlot1Allowed)
			{
				SlotItem[] slotItemArray = collection;
				if (NetworkScript.<>f__am$cache41 == null)
				{
					NetworkScript.<>f__am$cache41 = new Func<SlotItem, bool>(null, (SlotItem it) => (it.get_Slot() != 0 ? false : it.get_SlotType() == 6));
				}
				if (Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(slotItemArray, NetworkScript.<>f__am$cache41)) == null)
				{
					itemLocation = 6;
					Debug.Log(string.Concat("Free slot of type Laser found at number : ", 0));
					return (ushort)0;
				}
			}
			if (weaponSlotBluePrint.isSlot3Allowed)
			{
				SlotItem[] slotItemArray1 = collection;
				if (NetworkScript.<>f__am$cache42 == null)
				{
					NetworkScript.<>f__am$cache42 = new Func<SlotItem, bool>(null, (SlotItem it) => (it.get_Slot() != 2 ? false : it.get_SlotType() == 6));
				}
				if (Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(slotItemArray1, NetworkScript.<>f__am$cache42)) == null)
				{
					itemLocation = 6;
					Debug.Log(string.Concat("Free slot of type Laser found at number : ", 2));
					return (ushort)2;
				}
			}
		}
		else if (item.get_ItemType() == PlayerItems.TypeWeaponPlasmaTire1 || item.get_ItemType() == PlayerItems.TypeWeaponPlasmaTire2 || item.get_ItemType() == PlayerItems.TypeWeaponPlasmaTire3 || item.get_ItemType() == PlayerItems.TypeWeaponPlasmaTire4 || item.get_ItemType() == PlayerItems.TypeWeaponPlasmaTire5)
		{
			if (weaponSlotBluePrint.isSlot2Allowed)
			{
				SlotItem[] slotItemArray2 = collection;
				if (NetworkScript.<>f__am$cache43 == null)
				{
					NetworkScript.<>f__am$cache43 = new Func<SlotItem, bool>(null, (SlotItem it) => (it.get_Slot() != 1 ? false : it.get_SlotType() == 7));
				}
				if (Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(slotItemArray2, NetworkScript.<>f__am$cache43)) == null)
				{
					itemLocation = 7;
					Debug.Log(string.Concat("Free slot of type Laser found at number : ", 1));
					return (ushort)1;
				}
			}
			if (weaponSlotBluePrint.isSlot5Allowed)
			{
				SlotItem[] slotItemArray3 = collection;
				if (NetworkScript.<>f__am$cache44 == null)
				{
					NetworkScript.<>f__am$cache44 = new Func<SlotItem, bool>(null, (SlotItem it) => (it.get_Slot() != 4 ? false : it.get_SlotType() == 7));
				}
				if (Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(slotItemArray3, NetworkScript.<>f__am$cache44)) == null)
				{
					itemLocation = 7;
					Debug.Log(string.Concat("Free slot of type Laser found at number : ", 4));
					return (ushort)4;
				}
			}
		}
		else if (item.get_ItemType() == PlayerItems.TypeWeaponIonTire1 || item.get_ItemType() == PlayerItems.TypeWeaponIonTire2 || item.get_ItemType() == PlayerItems.TypeWeaponIonTire3 || item.get_ItemType() == PlayerItems.TypeWeaponIonTire4 || item.get_ItemType() == PlayerItems.TypeWeaponIonTire5)
		{
			if (weaponSlotBluePrint.isSlot4Allowed)
			{
				SlotItem[] slotItemArray4 = collection;
				if (NetworkScript.<>f__am$cache45 == null)
				{
					NetworkScript.<>f__am$cache45 = new Func<SlotItem, bool>(null, (SlotItem it) => (it.get_Slot() != 3 ? false : it.get_SlotType() == 8));
				}
				if (Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(slotItemArray4, NetworkScript.<>f__am$cache45)) == null)
				{
					itemLocation = 8;
					Debug.Log(string.Concat("Free slot of type Laser found at number : ", 3));
					return (ushort)3;
				}
			}
			if (weaponSlotBluePrint.isSlot6Allowed)
			{
				SlotItem[] slotItemArray5 = collection;
				if (NetworkScript.<>f__am$cache46 == null)
				{
					NetworkScript.<>f__am$cache46 = new Func<SlotItem, bool>(null, (SlotItem it) => (it.get_Slot() != 5 ? false : it.get_SlotType() == 8));
				}
				if (Enumerable.FirstOrDefault<SlotItem>(Enumerable.Where<SlotItem>(slotItemArray5, NetworkScript.<>f__am$cache46)) == null)
				{
					itemLocation = 8;
					Debug.Log(string.Concat("Free slot of type Laser found at number : ", 5));
					return (ushort)5;
				}
			}
		}
		itemLocation = 14;
		return num;
	}

	public GameObjectPhysics GetGameObjectOrNull(uint id)
	{
		if (id == 0)
		{
			return null;
		}
		if (this.gameObjects.ContainsKey(id))
		{
			return this.gameObjects.get_Item(id);
		}
		if (NetworkScript.player.vessel.neighbourhoodId != id)
		{
			return null;
		}
		return NetworkScript.player.vessel;
	}

	public PlayerObjectPhysics GetPveOrPlayer(long playerId, uint pveNbId)
	{
		NetworkScript.<GetPveOrPlayer>c__AnonStorey4C variable = null;
		if (pveNbId == 0)
		{
			return NetworkScript.clientSideClientsList.get_Item(playerId).vessel;
		}
		return (PlayerObjectPhysics)Enumerable.First<GameObjectPhysics>(Enumerable.Where<GameObjectPhysics>(this.gameObjects.get_Values(), new Func<GameObjectPhysics, bool>(variable, (GameObjectPhysics o) => (!(o is PvEPhysics) ? false : ((PvEPhysics)o).neighbourhoodId == this.pveNbId))));
	}

	public static void GetShipPhysicsFromGameObject(PlayerObjectPhysics p, GameObject ship)
	{
		p.x = ship.get_transform().get_position().x;
		p.y = ship.get_transform().get_position().y;
		p.z = ship.get_transform().get_position().z;
		Vector3 _eulerAngles = ship.get_transform().get_rotation().get_eulerAngles();
		p.rotationX = _eulerAngles.x;
		p.rotationY = _eulerAngles.y;
		p.rotationZ = _eulerAngles.z;
	}

	private void HideNanoShield(GameObjectPhysics pop)
	{
		ActiveSkillObject[] _activatedSkillsSafe = pop.get_activatedSkillsSafe();
		if (NetworkScript.<>f__am$cache28 == null)
		{
			NetworkScript.<>f__am$cache28 = new Func<ActiveSkillObject, bool>(null, (ActiveSkillObject t) => t.skillId == PlayerItems.TypeTalentsNanoShield);
		}
		ActiveSkillObject activeSkillObject = Enumerable.FirstOrDefault<ActiveSkillObject>(Enumerable.Where<ActiveSkillObject>(_activatedSkillsSafe, NetworkScript.<>f__am$cache28));
		if (activeSkillObject == null)
		{
			return;
		}
		if (this.gameObjects.ContainsKey(activeSkillObject.neighbourhoodId))
		{
			GameObject item = (GameObject)this.gameObjects.get_Item(activeSkillObject.neighbourhoodId).gameObject;
			Component[] componentsInChildren = item.GetComponentsInChildren<Light>();
			for (int i = 0; i < (int)componentsInChildren.Length; i++)
			{
				componentsInChildren[i].get_gameObject().SetActive(false);
			}
		}
	}

	public void HyperJumpOUT(GameObjectPhysics gop)
	{
		this.hyperJump = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("HyperJumpOutPfb"));
		this.hyperJump.get_transform().set_position(new Vector3(gop.x, 1f, gop.z));
	}

	private void InitCountdownWindow(int deltaTime)
	{
		if (NetworkScript.player.pvpGameTypeSignedFor == 0)
		{
			return;
		}
		if (NetworkScript.isInBase)
		{
			TargetingWnd.CreateCountdownWindow(deltaTime);
		}
		else if (AndromedaGui.mainWnd != null)
		{
			AndromedaGui.mainWnd.CreateCountdownWindow(deltaTime);
		}
	}

	private bool IsCollisionsMapAvailable(short galaxyId)
	{
		NetworkScript.<IsCollisionsMapAvailable>c__AnonStorey41 variable = null;
		return Enumerable.First<LevelMap>(Enumerable.Where<LevelMap>(StaticData.allGalaxies, new Func<LevelMap, bool>(variable, (LevelMap g) => g.get_galaxyId() == this.galaxyId))).collisionsMap != null;
	}

	private bool IsNonBreakingStealthSkill(int skillId)
	{
		return (skillId == PlayerItems.TypeTalentsNanoShield || skillId == PlayerItems.TypeTalentsStealth || skillId == PlayerItems.TypeTalentsUnstoppable || skillId == PlayerItems.TypeTalentsShieldFortress || skillId == PlayerItems.TypeTalentsLightSpeed ? true : skillId == PlayerItems.TypeTalentsMistShroud);
	}

	public static bool IsPartyMember(long playId)
	{
		bool flag;
		if (NetworkScript.party == null || NetworkScript.party.members == null)
		{
			return false;
		}
		List<PartyMemberClientSide>.Enumerator enumerator = NetworkScript.party.members.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.get_Current().playerId != playId)
				{
					continue;
				}
				flag = true;
				return flag;
			}
			return false;
		}
		finally
		{
			enumerator.Dispose();
		}
		return flag;
	}

	public bool IsPveOrPlayerExisting(long playerId, uint pveNbId)
	{
		bool flag;
		IEnumerator<GameObjectPhysics> enumerator = this.gameObjects.get_Values().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				GameObjectPhysics current = enumerator.get_Current();
				if (!(current is PlayerObjectPhysics))
				{
					continue;
				}
				PlayerObjectPhysics playerObjectPhysic = (PlayerObjectPhysics)current;
				if (!playerObjectPhysic.get_IsPve())
				{
					if (playerObjectPhysic.playerId != playerId)
					{
						continue;
					}
					flag = true;
					return flag;
				}
				else if (playerObjectPhysic.neighbourhoodId == pveNbId)
				{
					flag = true;
					return flag;
				}
			}
			return false;
		}
		finally
		{
			if (enumerator == null)
			{
			}
			enumerator.Dispose();
		}
		return flag;
	}

	private void LateUpdate()
	{
		NotificationManager.Updating();
		NetworkScript.spaceLabelManager.Updating();
		NetworkScript.activeSkillManager.Updating();
		NetworkScript.playerNameManager.Updating();
	}

	private void MakeInvisible(GameObject target)
	{
		ParticleRenderer[] componentsInChildren = target.GetComponentsInChildren<ParticleRenderer>();
		for (int i = 0; i < (int)componentsInChildren.Length; i++)
		{
			componentsInChildren[i].set_enabled(false);
		}
		Shader shader = Shader.Find("ReflectiveBumpedSpecularTransparency");
		target.get_renderer().get_material().set_shader(shader);
		target.get_renderer().get_material().SetFloat("_Trans", 0f);
		target.get_renderer().set_enabled(false);
	}

	private void MakeSemiInvisible(GameObject target)
	{
		ParticleRenderer[] componentsInChildren = target.GetComponentsInChildren<ParticleRenderer>();
		for (int i = 0; i < (int)componentsInChildren.Length; i++)
		{
			componentsInChildren[i].set_enabled(base.get_enabled());
		}
		target.get_animation().Play("GoStealth30");
		target.get_renderer().set_enabled(true);
	}

	private void ManageChatCommand(UdpCommHeader header)
	{
		GenericData genericDatum = (GenericData)header.data;
		if (genericDatum.int1 == 1 || genericDatum.int1 == 0 && !genericDatum.bool1)
		{
			Debug.Log("start private chat");
			__ChatWindow.OnStartChatResponse(genericDatum);
		}
		else if (genericDatum.bool1)
		{
			Debug.Log("invite to party");
			EventHandlerParam eventHandlerParam = new EventHandlerParam()
			{
				customData = genericDatum.long1
			};
			NetworkScript.OnPartyInviteClicked(eventHandlerParam);
		}
	}

	private void OnAchievementNotificationClick(object prm)
	{
		MainScreenWindow mainScreenWindow = AndromedaGui.mainWnd;
		EventHandlerParam eventHandlerParam = new EventHandlerParam()
		{
			customData = (byte)17,
			customData2 = (byte)1
		};
		mainScreenWindow.OnWindowBtnClicked(eventHandlerParam);
	}

	private void OnDestroy()
	{
		try
		{
			__ChatWindow.wnd = null;
			playWebGame.udp.StopReceiveUdp();
		}
		catch (Exception exception)
		{
			Debug.LogError(exception);
		}
		NetworkScript.activeSkillManager.Clear();
	}

	private void OnEnterBaseLastMove()
	{
		AndromedaGui.gui.StartFadeIn();
	}

	private void OnGUI()
	{
		if (this.showErrorMessageBox)
		{
			GUI.Label(new Rect(100f, 100f, 200f, 100f), this.errorText);
			if (this.showMessageDuration > 0 && DateTime.get_Now() > (this.startedShowMessageTime + TimeSpan.FromSeconds((double)this.showMessageDuration)))
			{
				this.showErrorMessageBox = false;
				this.showMessageDuration = 0;
			}
		}
	}

	public static void OnPartyInviteClicked(EventHandlerParam p)
	{
		// 
		// Current member / type: System.Void NetworkScript::OnPartyInviteClicked(EventHandlerParam)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnPartyInviteClicked(EventHandlerParam)
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

	private void OnRestartAfterNetworkFail(MessageBox.DialogResult r, string s)
	{
		Debug.Log("OnRestartAfterNetworkFail");
		playWebGame.LoadScene("Login");
	}

	public void OnTcpConnectedd()
	{
		// 
		// Current member / type: System.Void NetworkScript::OnTcpConnectedd()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnTcpConnectedd()
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

	[DebuggerHidden]
	private static IEnumerator PlayPvPEndGameSound()
	{
		return new NetworkScript.<PlayPvPEndGameSound>c__Iterator10();
	}

	public void PopulateActionButtons()
	{
		if (NetworkScript.player.shipScript.extractionPointInRange != null)
		{
			((GameObject)NetworkScript.player.shipScript.extractionPointInRange.gameObject).GetComponent<ExtractionPointGuiRelativeToGameObject>().Populate();
		}
		if (NetworkScript.player.shipScript.baseInRange != null)
		{
			((GameObject)NetworkScript.player.shipScript.baseInRange.gameObject).GetComponent<SpaceStationGuiRelativeToGameObject>().Populate();
		}
		if (NetworkScript.player.shipScript.hyperJumpInRange != null)
		{
			((GameObject)NetworkScript.player.shipScript.hyperJumpInRange.gameObject).GetComponent<HyperJumpGuiRelativeToGameObject>().Populate();
		}
		if (NetworkScript.player.shipScript.npcInRange != null)
		{
			((GameObject)NetworkScript.player.shipScript.npcInRange.gameObject).GetComponent<NpcGuiRelativeToGameObject>().Populate();
		}
		if (NetworkScript.player.shipScript.checkpointInRange != null)
		{
			((GameObject)NetworkScript.player.shipScript.checkpointInRange.gameObject).GetComponent<CheckpointGuiRelativeToGameObject>().Populate();
		}
	}

	public void ProjectileHitCorpus(GameObjectPhysics gop)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("weapon_hit1"));
		gameObject.GetComponent<ShipShieldScript>().target = gop;
		gameObject.get_transform().set_position(new Vector3(gop.x, 1f, gop.z));
	}

	public void ProjectileHitShield(GameObjectPhysics gop, float x, float y, float z)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("Shield_1_Pfb"));
		gameObject.GetComponent<ShipShieldScript>().target = (PlayerObjectPhysics)gop;
		gameObject.get_transform().set_position(new Vector3(gop.x, 0f, gop.z));
		gameObject.get_transform().LookAt(new Vector3(x, y, z));
	}

	public void PutActiveSkillOnScene(ActiveSkillObject skill)
	{
		skill.isOnClientSide = NetworkScript.isOnline;
		skill.RemoveSkillObject = new Action<ActiveSkillObject>(this, NetworkScript.RemoveActiveSkillObject);
		if (skill.skillId == PlayerItems.TypeTalentsFocusFire)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("SkillFocusFire_pfb"));
			ActiveSkillMovementScript component = gameObject.GetComponent<ActiveSkillMovementScript>();
			skill.gameObject = gameObject;
			component.caster = skill.caster;
			component.target = skill.target;
			component.physics = skill;
			component.get_transform().set_position(new Vector3(skill.x, skill.y, skill.z));
			component.isStarted = true;
			return;
		}
		if (skill.skillId == PlayerItems.TypeTalentsDecoy)
		{
			GameObject gameObject1 = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("SkillDecoy_pjc"));
			ActiveSkillMovementScript activeSkillMovementScript = gameObject1.GetComponent<ActiveSkillMovementScript>();
			skill.gameObject = gameObject1;
			activeSkillMovementScript.caster = skill.caster;
			activeSkillMovementScript.target = skill.target;
			activeSkillMovementScript.physics = skill;
			activeSkillMovementScript.get_transform().set_position(new Vector3(skill.x, skill.y, skill.z));
			activeSkillMovementScript.isStarted = true;
			return;
		}
		if (skill.skillId == PlayerItems.TypeTalentsPowerCut)
		{
			GameObject gameObject2 = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("SkillPowerCut_pjc"));
			ActiveSkillMovementScript component1 = gameObject2.GetComponent<ActiveSkillMovementScript>();
			skill.gameObject = gameObject2;
			component1.caster = skill.caster;
			component1.target = skill.target;
			component1.physics = skill;
			component1.get_transform().set_position(new Vector3(skill.x, skill.y, skill.z));
			component1.isStarted = true;
			return;
		}
		if (skill.skillId == PlayerItems.TypeTalentsForceWave)
		{
			GameObject gameObject3 = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("SkillForceWave_pfb"));
			ActiveSkillMovementScript activeSkillMovementScript1 = gameObject3.GetComponent<ActiveSkillMovementScript>();
			skill.gameObject = gameObject3;
			activeSkillMovementScript1.caster = skill.caster;
			activeSkillMovementScript1.target = skill.targetLocation;
			activeSkillMovementScript1.physics = skill;
			activeSkillMovementScript1.get_transform().set_position(new Vector3(skill.x, skill.y, skill.z));
			activeSkillMovementScript1.isStarted = true;
			return;
		}
		if (skill.skillId != PlayerItems.TypeTalentsNanoShield)
		{
			GameObject gameObject4 = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("ActiveSkill_pfb"));
			ActiveSkillScript activeSkillScript = gameObject4.GetComponent<ActiveSkillScript>();
			skill.gameObject = gameObject4;
			activeSkillScript.skill = skill;
			return;
		}
		GameObject gameObject5 = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("SkillNanoShield_pfb"));
		ActiveSkillMovementScript component2 = gameObject5.GetComponent<ActiveSkillMovementScript>();
		skill.gameObject = gameObject5;
		component2.caster = skill.caster;
		component2.target = skill.target;
		component2.physics = skill;
		component2.get_transform().set_position(new Vector3(skill.x, skill.y, skill.z));
		component2.isStarted = true;
		if (skill.target.get_IsPoP())
		{
			PlayerObjectPhysics playerObjectPhysic = (PlayerObjectPhysics)skill.target;
			if (playerObjectPhysic.isInStealthMode && !NetworkScript.IsPartyMember(playerObjectPhysic.playerId) && playerObjectPhysic.playerId != NetworkScript.player.playId)
			{
				this.HideNanoShield(skill.target);
			}
		}
	}

	public void PutProjectileOnScene(ProjectileObject projectile)
	{
		projectile.isOnClientSide = NetworkScript.isOnline;
		if (projectile is BulletObject || projectile is LaserMovingObject)
		{
			string str = string.Concat("Projectiles/", projectile.assetName, NetworkScript.PROJECTILE_ASSET_SUFFIX);
			GameObject gameObject = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab(str));
			BulletScript component = gameObject.GetComponent<BulletScript>();
			projectile.gameObject = gameObject;
			component.physics = projectile;
			component.shooter = projectile.shooter;
			component.target = projectile.target;
			component.isStarted = true;
			component.get_transform().set_position(new Vector3(projectile.x, projectile.y, projectile.z));
			return;
		}
		if (projectile is RocketObject)
		{
			GameObject gameObject1 = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab(string.Concat("Projectiles/", projectile.assetName, NetworkScript.PROJECTILE_ASSET_SUFFIX)));
			RocketScript rocketScript = gameObject1.GetComponent<RocketScript>();
			projectile.gameObject = gameObject1;
			projectile.RemoveProjectile = new Action<ProjectileObject>(rocketScript, RocketScript.RemoveTheGameObject);
			rocketScript.physics = projectile;
			rocketScript.target = projectile.target;
			rocketScript.get_transform().set_position(new Vector3(projectile.x, projectile.y, projectile.z));
			rocketScript.isStarted = true;
			return;
		}
		if (!(projectile is LaserWeldingObject))
		{
			return;
		}
		GameObject gameObject2 = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab(string.Concat("Projectiles/", projectile.assetName, NetworkScript.PROJECTILE_ASSET_SUFFIX)));
		LaserWeldingScript laserWeldingScript = gameObject2.GetComponent<LaserWeldingScript>();
		projectile.gameObject = gameObject2;
		projectile.RemoveProjectile = new Action<ProjectileObject>(laserWeldingScript, LaserWeldingScript.RemoveTheGameObject);
		laserWeldingScript.physics = projectile;
		if (!(projectile.shooter is PlayerObjectPhysics))
		{
			throw new Exception("Not supported shooting object!");
		}
		laserWeldingScript.shooter = projectile.shooter;
		laserWeldingScript.target = projectile.target;
		laserWeldingScript.isStarted = true;
	}

	private void PvPDominationUpdate(UdpCommHeader header)
	{
		if (header.context == 76)
		{
			MiningStation miningStation = (MiningStation)header.data;
			if (this.gameObjects.ContainsKey(miningStation.neighbourhoodId))
			{
				byte ownerTeam = 0;
				MiningStation item = (MiningStation)this.gameObjects.get_Item(miningStation.neighbourhoodId);
				ownerTeam = item.get_OwnerTeam();
				item.teamOneProgress = miningStation.teamOneProgress;
				item.teamTwoProgress = miningStation.teamTwoProgress;
				((GameObject)item.gameObject).GetComponent<MiningStationGuiRelativeToGameObject>().Populate();
				if (ownerTeam == item.get_OwnerTeam())
				{
					AndromedaGui.mainWnd.PopulatePvPDominationGame(0);
				}
				else
				{
					AndromedaGui.mainWnd.PopulatePvPDominationGame(miningStation.neighbourhoodId);
				}
			}
		}
	}

	private void ReadData(int progress, int size)
	{
		GuiFramework.progressLabel = string.Format("Preparing Your Ship {0: ##0}% ", 100f * (float)progress / (float)size);
	}

	private void ReceivePlayerItems(UdpCommHeader h)
	{
		MadeSynthesisParams madeSynthesisParam = (MadeSynthesisParams)h.data;
		madeSynthesisParam.items.RestoreAfterDeserialize();
		NetworkScript.player.playerBelongings.playerItems.Reload(madeSynthesisParam.items);
	}

	private void ReciveQuestEngineCommand(UdpCommHeader header)
	{
		// 
		// Current member / type: System.Void NetworkScript::ReciveQuestEngineCommand(TransferableObjects.UdpCommHeader)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void ReciveQuestEngineCommand(TransferableObjects.UdpCommHeader)
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

	private void ReciveServerMessage(UdpCommHeader header)
	{
		UniversalTransportContainer universalTransportContainer = (UniversalTransportContainer)header.data;
		if (header.context == 120)
		{
			NetworkScript.player.playerBelongings.warCommendationsBought = universalTransportContainer.errorCodeIndex;
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 34)
			{
				AndromedaGui.gui.RemoveWindow(AndromedaGui.gui.activeToolTipId);
				AndromedaGui.gui.activeToolTipId = -1;
			}
			return;
		}
		if (header.context == 119)
		{
			if (this.lastNotificationPacketSeq == header.packetSeq)
			{
				return;
			}
			this.lastNotificationPacketSeq = header.packetSeq;
			NotificationManager.AddTransformerReward(PlayerItems.TypeWarCommendation, (int)universalTransportContainer.errorCodeIndex);
			return;
		}
		if (header.context == 118)
		{
			if (NetworkScript.player.myGameMessages != null)
			{
				NetworkScript.player.myGameMessages.InsertRange(0, universalTransportContainer.playerGameMessages);
			}
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 35)
			{
				((GameMessagesWindow)AndromedaGui.mainWnd.activeWindow).PopulateScreen();
			}
			if (AndromedaGui.mainWnd != null)
			{
				AndromedaGui.mainWnd.CreateMessagesWindow();
			}
			return;
		}
		if (header.context == 117)
		{
			if (NetworkScript.player.myGameMessages == null)
			{
				NetworkScript.player.myGameMessages = universalTransportContainer.playerGameMessages;
			}
			else
			{
				NetworkScript.player.myGameMessages.Clear();
				NetworkScript.player.myGameMessages.AddRange(universalTransportContainer.playerGameMessages);
			}
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 35)
			{
				((GameMessagesWindow)AndromedaGui.mainWnd.activeWindow).PopulateScreen();
			}
			if (AndromedaGui.mainWnd != null)
			{
				AndromedaGui.mainWnd.CreateMessagesWindow();
			}
			return;
		}
		if (header.context == 113)
		{
			PartyMemberInfo partyMemberInfo = universalTransportContainer.partyMemberInfo;
			if (!NetworkScript.partyMembersInfo.ContainsKey(partyMemberInfo.playerId))
			{
				partyMemberInfo.lastUpdateTime = StaticData.now;
				NetworkScript.partyMembersInfo.Add(partyMemberInfo.playerId, partyMemberInfo);
				if (NetworkScript.player.shipScript != null)
				{
					NetworkScript.player.shipScript.ManagePartyMemberArrow();
				}
			}
			else
			{
				NetworkScript.partyMembersInfo.get_Item(partyMemberInfo.playerId).coordinateX = partyMemberInfo.coordinateX;
				NetworkScript.partyMembersInfo.get_Item(partyMemberInfo.playerId).coordinateZ = partyMemberInfo.coordinateZ;
				NetworkScript.partyMembersInfo.get_Item(partyMemberInfo.playerId).galaxyId = partyMemberInfo.galaxyId;
				NetworkScript.partyMembersInfo.get_Item(partyMemberInfo.playerId).shieldPercent = partyMemberInfo.shieldPercent;
				NetworkScript.partyMembersInfo.get_Item(partyMemberInfo.playerId).corpusPercent = partyMemberInfo.corpusPercent;
				NetworkScript.partyMembersInfo.get_Item(partyMemberInfo.playerId).lastUpdateTime = StaticData.now;
				if (NetworkScript.player.shipScript != null)
				{
					NetworkScript.player.shipScript.UpdatePartyArrow(partyMemberInfo.playerId);
				}
			}
			return;
		}
		if (header.context == 41)
		{
			if (NetworkScript.player.guildMember != null && NetworkScript.player.guildMember.rank != null)
			{
				NetworkScript.player.guildMember.rank.canBank = universalTransportContainer.myGuildRank.canBank;
				NetworkScript.player.guildMember.rank.canChat = universalTransportContainer.myGuildRank.canChat;
				NetworkScript.player.guildMember.rank.canEditDetails = universalTransportContainer.myGuildRank.canEditDetails;
				NetworkScript.player.guildMember.rank.canInvite = universalTransportContainer.myGuildRank.canInvite;
				NetworkScript.player.guildMember.rank.canPromote = universalTransportContainer.myGuildRank.canPromote;
				NetworkScript.player.guildMember.rank.canVault = universalTransportContainer.myGuildRank.canVault;
			}
			return;
		}
		if (header.context == 109)
		{
			NetworkScript.player.factionGalaxyOwnership = universalTransportContainer.allFactionGalaxiesOwnership;
			NetworkScript.player.factionOneAttackGalaxyKey = universalTransportContainer.factionOneMostWantedGalaxy;
			NetworkScript.player.factionTwoAttackGalaxyKey = universalTransportContainer.factionTwoMostWantedGalaxy;
			NetworkScript.player.isWarInProgress = universalTransportContainer.isWarInProgress;
			NetworkScript.player.nextWarStartTime = universalTransportContainer.nextWarStartTime;
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 7)
			{
				AndromedaGui.mainWnd.CloseActiveWindow();
			}
			return;
		}
		if (header.context == 108)
		{
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 34)
			{
				WarScreenWindow warScreenWindow = (WarScreenWindow)AndromedaGui.mainWnd.activeWindow;
				string empty = string.Empty;
				switch (universalTransportContainer.errorCodeIndex)
				{
					case 1:
					{
						empty = StaticData.Translate("key_error_code_not_existing_player");
						break;
					}
					case 2:
					{
						empty = StaticData.Translate("key_error_code_not_same_faction_player");
						break;
					}
					case 3:
					{
						empty = StaticData.Translate("key_error_code_donation_minimum");
						break;
					}
					case 4:
					{
						empty = StaticData.Translate("key_error_code_donation_maximum");
						break;
					}
					case 5:
					{
						empty = StaticData.Translate("key_error_code_offline_player");
						break;
					}
					case 6:
					{
						empty = string.Format(StaticData.Translate("key_error_code_low_level_player"), 20);
						break;
					}
					case 7:
					{
						empty = StaticData.Translate("key_error_code_same_device_id");
						break;
					}
				}
				warScreenWindow.ShowWarninMessage(StaticData.Translate(empty), new Rect(310f, 60f, 500f, 170f), 24, 2);
			}
			return;
		}
		if (header.context == 112)
		{
			NetworkScript.player.playerBelongings.councilSkillSelected = universalTransportContainer.councilSkillId;
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 34)
			{
				WarScreenWindow warScreenWindow1 = (WarScreenWindow)AndromedaGui.mainWnd.activeWindow;
				warScreenWindow1.selectCouncilSkillComandSend = false;
				warScreenWindow1.PopulateOnDayChange();
			}
			return;
		}
		if (header.context == 106)
		{
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 34)
			{
				WarScreenWindow warScreenWindow2 = (WarScreenWindow)AndromedaGui.mainWnd.activeWindow;
				warScreenWindow2.yourFactionToYou = universalTransportContainer.yourFactionToYou;
				warScreenWindow2.yourFactionToEnemy = universalTransportContainer.yourFactionToEnemy;
				warScreenWindow2.enemyFactionToYou = universalTransportContainer.enemyFactionToYou;
				warScreenWindow2.PopulateFactionMessages();
			}
			return;
		}
		if (header.context == 105)
		{
			if (universalTransportContainer.weeklyRewardCollected && NetworkScript.player.playerBelongings.weeklyRewardCollected != universalTransportContainer.weeklyRewardCollected)
			{
				this.ShowCollectRewardNotification(NetworkScript.player.playerBelongings.get_FactionWarWeeklyChalangeParticipation() * 100);
			}
			else if (universalTransportContainer.lastWeekPendingReward == 0 && NetworkScript.player.playerBelongings.lastWeekPendingReward != 0)
			{
				this.ShowCollectRewardNotification(NetworkScript.player.playerBelongings.lastWeekPendingReward * 1000);
			}
			NetworkScript.player.playerBelongings.weeklyRewardCollected = universalTransportContainer.weeklyRewardCollected;
			NetworkScript.player.playerBelongings.lastWeekPendingReward = universalTransportContainer.lastWeekPendingReward;
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 34)
			{
				((WarScreenWindow)AndromedaGui.mainWnd.activeWindow).PopulateScreen();
			}
			if (AndromedaGui.mainWnd != null)
			{
				AndromedaGui.mainWnd.ShowFactionWarNotificationWindow();
			}
			return;
		}
		if (header.context == 104)
		{
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 34)
			{
				WarScreenWindow warScreenWindow3 = (WarScreenWindow)AndromedaGui.mainWnd.activeWindow;
				WarScreenWindow list = warScreenWindow3;
				List<KeyValuePair<short, long>> list1 = universalTransportContainer.galaxyVote;
				if (NetworkScript.<>f__am$cache2D == null)
				{
					NetworkScript.<>f__am$cache2D = new Func<KeyValuePair<short, long>, long>(null, (KeyValuePair<short, long> t) => t.get_Value());
				}
				list.galaxyVote = Enumerable.ToList<KeyValuePair<short, long>>(Enumerable.OrderByDescending<KeyValuePair<short, long>, long>(list1, NetworkScript.<>f__am$cache2D));
				warScreenWindow3.PopulateVoteForGalaxyScreen();
			}
			return;
		}
		if (header.context == 102)
		{
			NetworkScript.player.playerBelongings.myBattleBoostVote = universalTransportContainer.myBattleBoostVote;
			NetworkScript.player.playerBelongings.myUtilityBoostVote = universalTransportContainer.myUtilityBoostVote;
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 34)
			{
				WarScreenWindow warScreenWindow4 = (WarScreenWindow)AndromedaGui.mainWnd.activeWindow;
				warScreenWindow4.voteForBattleOne = universalTransportContainer.battleBoost1Votes;
				warScreenWindow4.voteForBattleTwo = universalTransportContainer.battleBoost2Votes;
				warScreenWindow4.voteForBattleThree = universalTransportContainer.battleBoost3Votes;
				warScreenWindow4.voteForBattleVeto = universalTransportContainer.battleBoostVeto;
				warScreenWindow4.voteForUtilityOne = universalTransportContainer.utilityBoost1Votes;
				warScreenWindow4.voteForUtilityTwo = universalTransportContainer.utilityBoost2Votes;
				warScreenWindow4.voteForUtilityThree = universalTransportContainer.utilityBoost3Votes;
				warScreenWindow4.voteForUtilityVeto = universalTransportContainer.utilityBoostVeto;
				warScreenWindow4.PopulateFactionBoostVote();
			}
			return;
		}
		if (header.context == 101)
		{
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 34)
			{
				WarScreenWindow warScreenWindow5 = (WarScreenWindow)AndromedaGui.mainWnd.activeWindow;
				WarScreenWindow list2 = warScreenWindow5;
				List<FactionCouncilMember> list3 = universalTransportContainer.factionOneCouncil;
				if (NetworkScript.<>f__am$cache2E == null)
				{
					NetworkScript.<>f__am$cache2E = new Func<FactionCouncilMember, byte>(null, (FactionCouncilMember t) => t.rank);
				}
				list2.factionOneCouncil = Enumerable.ToList<FactionCouncilMember>(Enumerable.OrderBy<FactionCouncilMember, byte>(list3, NetworkScript.<>f__am$cache2E));
				WarScreenWindow list4 = warScreenWindow5;
				List<FactionCouncilMember> list5 = universalTransportContainer.factionTwoCouncil;
				if (NetworkScript.<>f__am$cache2F == null)
				{
					NetworkScript.<>f__am$cache2F = new Func<FactionCouncilMember, byte>(null, (FactionCouncilMember t) => t.rank);
				}
				list4.factionTwoCouncil = Enumerable.ToList<FactionCouncilMember>(Enumerable.OrderBy<FactionCouncilMember, byte>(list5, NetworkScript.<>f__am$cache2F));
				warScreenWindow5.isCouncilsReceived = true;
				warScreenWindow5.PopulateScreen();
			}
			return;
		}
		if (header.context == 100)
		{
			if (universalTransportContainer.dailyReward1Collected && NetworkScript.player.playerBelongings.rewardForDayProgressCollected1 != universalTransportContainer.dailyReward1Collected)
			{
				this.ShowCollectRewardNotification(1);
			}
			else if (universalTransportContainer.dailyReward2Collected && NetworkScript.player.playerBelongings.rewardForDayProgressCollected2 != universalTransportContainer.dailyReward2Collected)
			{
				this.ShowCollectRewardNotification(2);
			}
			else if (universalTransportContainer.dailyReward3Collected && NetworkScript.player.playerBelongings.rewardForDayProgressCollected3 != universalTransportContainer.dailyReward3Collected)
			{
				this.ShowCollectRewardNotification(3);
			}
			NetworkScript.player.playerBelongings.factionWarDayScore = universalTransportContainer.factionWarDayScore;
			NetworkScript.player.playerBelongings.rewardForDayProgressCollected1 = universalTransportContainer.dailyReward1Collected;
			NetworkScript.player.playerBelongings.rewardForDayProgressCollected2 = universalTransportContainer.dailyReward2Collected;
			NetworkScript.player.playerBelongings.rewardForDayProgressCollected3 = universalTransportContainer.dailyReward3Collected;
			WarScreenWindow.factionOneScore = universalTransportContainer.factionOneScore;
			WarScreenWindow.factionTwoScore = universalTransportContainer.factionTwoScore;
			WarScreenWindow.factionOneCurrentDayScore = universalTransportContainer.factionOneDayScore;
			WarScreenWindow.factionTwoCurrentDayScore = universalTransportContainer.factionTwoDayScore;
			WarScreenWindow.weeklyLoosingFaction = universalTransportContainer.loosingFaction;
			WarScreenWindow.weeklyLoosingFactionBonusPercent = universalTransportContainer.loosingFactionBonusPercent;
			WarScreenWindow.dailyLoosingFaction = universalTransportContainer.dailyLoosingFaction;
			WarScreenWindow.dailyLoosingFactionBonusPercent = universalTransportContainer.dailyLoosingFactionBonusPercent;
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 34)
			{
				((WarScreenWindow)AndromedaGui.mainWnd.activeWindow).PopulateScreen();
			}
			if (AndromedaGui.mainWnd != null)
			{
				AndromedaGui.mainWnd.ShowFactionWarNotificationWindow();
			}
			return;
		}
		if (header.context == 99)
		{
			NetworkScript.player.playerBelongings.factionWarBattleBoost = universalTransportContainer.myBattleBoostVote;
			NetworkScript.player.playerBelongings.factionWarUtilityBoost = universalTransportContainer.myUtilityBoostVote;
			NetworkScript.player.playerBelongings.factionWarDay = universalTransportContainer.factionWarDay;
			NetworkScript.player.playerBelongings.factionWarDayEnd = universalTransportContainer.factionWarDayEndTime;
			NetworkScript.player.playerBelongings.factionWarDayScore = 0;
			NetworkScript.player.playerBelongings.rewardForDayProgressCollected1 = false;
			NetworkScript.player.playerBelongings.rewardForDayProgressCollected2 = false;
			NetworkScript.player.playerBelongings.rewardForDayProgressCollected3 = false;
			NetworkScript.player.playerBelongings.myBattleBoostVote = 0;
			NetworkScript.player.playerBelongings.myUtilityBoostVote = 0;
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 34)
			{
				((WarScreenWindow)AndromedaGui.mainWnd.activeWindow).PopulateOnDayChange();
			}
			if (AndromedaGui.mainWnd != null)
			{
				AndromedaGui.mainWnd.ShowFactionWarNotificationWindow();
			}
			return;
		}
		if (header.context == 98)
		{
			bool flag = NetworkScript.player.playerBelongings.councilRank != universalTransportContainer.councilRank;
			NetworkScript.player.playerBelongings.councilRank = universalTransportContainer.councilRank;
			NetworkScript.player.playerBelongings.councilSkillSelected = universalTransportContainer.selectedCouncilSkillId;
			NetworkScript.player.playerBelongings.day1Participation = universalTransportContainer.day1Participation;
			NetworkScript.player.playerBelongings.day2Participation = universalTransportContainer.day2Participation;
			NetworkScript.player.playerBelongings.day3Participation = universalTransportContainer.day3Participation;
			NetworkScript.player.playerBelongings.day4Participation = universalTransportContainer.day4Participation;
			NetworkScript.player.playerBelongings.day5Participation = universalTransportContainer.day5Participation;
			NetworkScript.player.playerBelongings.day6Participation = universalTransportContainer.day6Participation;
			NetworkScript.player.playerBelongings.factionWarDayScore = universalTransportContainer.factionWarDayScore;
			NetworkScript.player.playerBelongings.weeklyRewardCollected = universalTransportContainer.weeklyRewardCollected;
			NetworkScript.player.playerBelongings.lastWeekPendingReward = universalTransportContainer.lastWeekPendingReward;
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 34)
			{
				WarScreenWindow warScreenWindow6 = (WarScreenWindow)AndromedaGui.mainWnd.activeWindow;
				if (!flag)
				{
					warScreenWindow6.PopulateScreen();
				}
				else
				{
					warScreenWindow6.PopulateOnDayChange();
				}
			}
			if (AndromedaGui.mainWnd != null)
			{
				AndromedaGui.mainWnd.ShowFactionWarNotificationWindow();
			}
			return;
		}
		if (header.context == 96)
		{
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 34)
			{
				WarScreenWindow warScreenWindow7 = (WarScreenWindow)AndromedaGui.mainWnd.activeWindow;
				warScreenWindow7.paidAdPrice = universalTransportContainer.nextPaidAdPrice;
				warScreenWindow7.paidAdNickName = universalTransportContainer.paidAdNickName;
				warScreenWindow7.paidAdSlogan = universalTransportContainer.paidAdSlogan;
				warScreenWindow7.PopulateVoteForPlayerScreen();
			}
			return;
		}
		if (header.context == 97)
		{
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 34)
			{
				WarScreenWindow warScreenWindow8 = (WarScreenWindow)AndromedaGui.mainWnd.activeWindow;
				warScreenWindow8.paidAdPrice = universalTransportContainer.nextPaidAdPrice;
				warScreenWindow8.paidAdNickName = universalTransportContainer.paidAdNickName;
				warScreenWindow8.paidAdSlogan = universalTransportContainer.paidAdSlogan;
				warScreenWindow8.factionBank = universalTransportContainer.voteDonation;
				warScreenWindow8.topCouncilCandidats = universalTransportContainer.factionCouncils;
				warScreenWindow8.PopulateVoteForPlayerScreen();
			}
			return;
		}
		if (header.context == 94)
		{
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 34)
			{
				((WarScreenWindow)AndromedaGui.mainWnd.activeWindow).PopulateBestCandidatesAndBank(universalTransportContainer.factionCouncils, universalTransportContainer.voteDonation);
			}
			return;
		}
		if (header.context == 95)
		{
			WarScreenWindow.factionOneScore = universalTransportContainer.factionOneScore;
			WarScreenWindow.factionTwoScore = universalTransportContainer.factionTwoScore;
			WarScreenWindow.factionOneCurrentDayScore = universalTransportContainer.factionOneDayScore;
			WarScreenWindow.factionTwoCurrentDayScore = universalTransportContainer.factionTwoDayScore;
			WarScreenWindow.weeklyLoosingFaction = universalTransportContainer.loosingFaction;
			WarScreenWindow.weeklyLoosingFactionBonusPercent = universalTransportContainer.loosingFactionBonusPercent;
			WarScreenWindow.dailyLoosingFaction = universalTransportContainer.dailyLoosingFaction;
			WarScreenWindow.dailyLoosingFactionBonusPercent = universalTransportContainer.dailyLoosingFactionBonusPercent;
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 34)
			{
				((WarScreenWindow)AndromedaGui.mainWnd.activeWindow).factionBank = universalTransportContainer.voteDonation;
				((WarScreenWindow)AndromedaGui.mainWnd.activeWindow).PopulateFactionBank();
			}
			return;
		}
		if (header.context == 86)
		{
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 31)
			{
				((InstanceDifficultyWindow)AndromedaGui.mainWnd.activeWindow).PopulateData(universalTransportContainer.instanceGalaxyId, universalTransportContainer.instanceStatus, universalTransportContainer.selectedDificulty);
			}
			if (universalTransportContainer.instanceGalaxyId == this.galaxy.__galaxyId && AndromedaGui.mainWnd != null)
			{
				AndromedaGui.mainWnd.PopulateInstanceStatsWindow(universalTransportContainer.instanceKillProgress, universalTransportContainer.instanceKillTarget, universalTransportContainer.selectedDificulty);
			}
			return;
		}
		if (header.context == 78)
		{
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 15)
			{
				((PVPWindow)AndromedaGui.mainWnd.activeWindow).PopulatePvPWinners(universalTransportContainer.pvpRewardedPlayers);
			}
			return;
		}
		if (header.context == 79)
		{
			NetworkScript.player.playerBelongings.nextPvPRoundTime = universalTransportContainer.nextPvPRoundTime;
			return;
		}
		if (header.context == 80)
		{
			NetworkScript.player.playerBelongings.pvpGamePoolCapacity = universalTransportContainer.pvpGamePoolCapacity;
			NetworkScript.player.playerBelongings.oldestPvPGameStartTime = universalTransportContainer.nextPvPRoundTime;
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 15)
			{
				((PVPWindow)AndromedaGui.mainWnd.activeWindow).PopulatePlayePvPScreen(string.Empty);
			}
			return;
		}
		if (header.context == 77)
		{
			if (universalTransportContainer.playerId != NetworkScript.player.vessel.playerId)
			{
				PlayerDataEx playerDataEx = null;
				if (NetworkScript.clientSideClientsList.TryGetValue(universalTransportContainer.playerId, ref playerDataEx))
				{
					playerDataEx.vessel.playerLeague = universalTransportContainer.playerLeague;
					playerDataEx.vessel.inPvPRank = universalTransportContainer.inPvPRank;
					playerDataEx.shipScript.get_gameObject().GetComponent<ShipStatsGuiRelativeToGameObject>().Populate();
				}
			}
			else
			{
				NetworkScript.player.vessel.playerLeague = universalTransportContainer.playerLeague;
				NetworkScript.player.vessel.inPvPRank = universalTransportContainer.inPvPRank;
				NetworkScript.player.playerBelongings.firstWinBonusRecived = false;
				NetworkScript.player.shipScript.get_gameObject().GetComponent<ShipStatsGuiRelativeToGameObject>().Populate();
			}
			return;
		}
		if (header.context == 81)
		{
			PVPWindow.signedPlayers = universalTransportContainer.signedPlayersCount;
			if (NetworkScript.player.pvpGame == null && AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 15)
			{
				((PVPWindow)AndromedaGui.mainWnd.activeWindow).PopulatePlayePvPScreen(string.Empty);
			}
			if (!string.IsNullOrEmpty(universalTransportContainer.signedPlayerName))
			{
				if (this.lastSignedPlayerName == universalTransportContainer.signedPlayerName && this.lastSignedPlayerTimestamp.AddSeconds(10) > StaticData.now)
				{
					return;
				}
				this.lastSignedPlayerName = universalTransportContainer.signedPlayerName;
				this.lastSignedPlayerTimestamp = StaticData.now;
				NotificationWindow.StartPvPNotification(string.Format(StaticData.Translate("key_network_script_chat_notification_player_sign"), universalTransportContainer.signedPlayerName, universalTransportContainer.signedPlayerLevel, universalTransportContainer.signedPlayersCount));
			}
			return;
		}
		if (header.context == 73)
		{
			NetworkScript.player.playerBelongings.playerAccessLevel = universalTransportContainer.playerAccessLevel;
			this.PopulateActionButtons();
			return;
		}
		if (header.context == 68)
		{
			this.ShowCriticalHit(universalTransportContainer.criticalHitPlayerId);
			return;
		}
		if (header.context == 83)
		{
			this.DoChangeSpeed(universalTransportContainer.playerId, universalTransportContainer.newSpeed);
			return;
		}
		if (header.context == 84)
		{
			this.DoChangeStunState(universalTransportContainer.playerId, universalTransportContainer.isStunned);
			return;
		}
		if (header.context == 110)
		{
			this.DoChangeDisarmState(universalTransportContainer.playerId, universalTransportContainer.isDisarmed);
			return;
		}
		if (header.context == 111)
		{
			this.DoChangeShockState(universalTransportContainer.playerId, universalTransportContainer.isShocked);
			return;
		}
		if (header.context == 70)
		{
			this.DoChangeSpeed(universalTransportContainer.playerId, universalTransportContainer.newSpeed, universalTransportContainer.isBoostActive);
			return;
		}
		if (header.context == 69)
		{
			if (this.lastNotificationPacketSeq == header.packetSeq)
			{
				return;
			}
			this.lastNotificationPacketSeq = header.packetSeq;
			if (AndromedaGui.mainWnd != null)
			{
				AndromedaGui.mainWnd.ShowCriticalIndication();
			}
			return;
		}
		if (header.context == 66)
		{
			if (NetworkScript.isInBase)
			{
				return;
			}
			NetworkScript.player.playerBelongings.playerAwards = universalTransportContainer.pendindAwards;
			NetworkScript.player.playerBelongings.receivedDailyRewards = universalTransportContainer.receivedDailyRewards;
			IList<PlayerPendingAward> values = NetworkScript.player.playerBelongings.playerAwards.get_Values();
			if (NetworkScript.<>f__am$cache30 == null)
			{
				NetworkScript.<>f__am$cache30 = new Func<PlayerPendingAward, bool>(null, (PlayerPendingAward t) => t.isDaily);
			}
			if (Enumerable.FirstOrDefault<PlayerPendingAward>(Enumerable.Where<PlayerPendingAward>(values, NetworkScript.<>f__am$cache30)) != null)
			{
				if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 26)
				{
					AndromedaGui.mainWnd.CloseActiveWindow();
				}
				if (!DailyRewardsWindow.isAlreadyShown || DailyRewardsWindow.lastDayCnt != NetworkScript.player.playerBelongings.receivedDailyRewards)
				{
					NetworkScript.player.shipScript.OpenDailyRewardWindow();
				}
			}
			if (AndromedaGui.mainWnd != null && AndromedaGui.mainWnd.activWindowIndex == 17)
			{
				((ProfileScreen)AndromedaGui.mainWnd.activeWindow).PopulatePendingAwards();
			}
			if (AndromedaGui.mainWnd != null)
			{
				AndromedaGui.mainWnd.DrawPendingRewardsNotification();
				AndromedaGui.mainWnd.ShowFactionWarNotificationWindow();
			}
			return;
		}
		if (header.context == 65)
		{
			if (universalTransportContainer.playerId != NetworkScript.player.playId)
			{
				NetworkScript.clientSideClientsList.get_Item(universalTransportContainer.playerId).shipScript.BreakGalaxyJump();
			}
			else
			{
				NetworkScript.player.shipScript.CancelGalaxyJump(false);
			}
			return;
		}
		if (this.lastNotificationPacketSeq == header.packetSeq)
		{
			return;
		}
		if (universalTransportContainer.serverMessageIndex == 10)
		{
			NotificationManager.AddTransformerReward(PlayerItems.TypeUltralibrium, 25);
		}
		else if (universalTransportContainer.serverMessageIndex == 101)
		{
			GameObject gameObject = GameObject.Find("golgotha_pfb");
			if (gameObject != null)
			{
				Component[] components = gameObject.GetComponents(typeof(Animation));
				Animation[] animationArray = new Animation[(int)components.Length];
				for (int i = 0; i < (int)components.Length; i++)
				{
					animationArray[i] = (Animation)components[i];
				}
				Animation[] animationArray1 = animationArray;
				for (int j = 0; j < (int)animationArray1.Length; j++)
				{
					Animation animation = animationArray1[j];
					string str = "golgotha_flee_hydraprime";
					AnimationClip clip = animation.GetClip(str);
					if (clip == null)
					{
						Debug.Log(string.Concat("Missing animation:", str));
					}
					else
					{
						animation.Play(clip.get_name());
					}
				}
			}
		}
		else if (universalTransportContainer.serverMessageIndex == 102)
		{
			GameObject gameObject1 = GameObject.Find("golgotha_pfb");
			if (gameObject1 != null)
			{
				Component[] componentArray = gameObject1.GetComponents(typeof(Animation));
				Animation[] animationArray2 = new Animation[(int)componentArray.Length];
				for (int k = 0; k < (int)componentArray.Length; k++)
				{
					animationArray2[k] = (Animation)componentArray[k];
				}
				Animation[] animationArray3 = animationArray2;
				for (int l = 0; l < (int)animationArray3.Length; l++)
				{
					Animation animation1 = animationArray3[l];
					string str1 = "golgotha_flee_bellatrixhideout";
					AnimationClip animationClip = animation1.GetClip(str1);
					if (animationClip == null)
					{
						Debug.Log(string.Concat("Missing animation:", str1));
					}
					else
					{
						animation1.Play(animationClip.get_name());
					}
				}
			}
			GameObject gameObject2 = GameObject.Find("volkr_pfb");
			if (gameObject2 != null)
			{
				Component[] components1 = gameObject2.GetComponents(typeof(Animation));
				Animation[] animationArray4 = new Animation[(int)components1.Length];
				for (int m = 0; m < (int)components1.Length; m++)
				{
					animationArray4[m] = (Animation)components1[m];
				}
				Animation[] animationArray5 = animationArray4;
				for (int n = 0; n < (int)animationArray5.Length; n++)
				{
					Animation animation2 = animationArray5[n];
					string str2 = "volkr_flee_bellatrixhideout";
					AnimationClip clip1 = animation2.GetClip(str2);
					if (clip1 == null)
					{
						Debug.Log(string.Concat("Missing animation:", str2));
					}
					else
					{
						animation2.Play(clip1.get_name());
					}
				}
			}
		}
		else if (universalTransportContainer.serverMessageIndex == 200 && NetworkScript.player.playerBelongings.playerLevel > 10 && NetworkScript.player.playerBelongings.playerLevel != 15 && NetworkScript.player.playerBelongings.playerLevel != 20 && NetworkScript.player.playerBelongings.playerLevel != 30)
		{
			NotificationManager.AddLevelUpNotification(NetworkScript.player.playerBelongings.playerLevel);
		}
		else if (universalTransportContainer.serverMessageIndex == 254)
		{
			NetworkScript.player.vessel.isInControl = true;
			NetworkScript.player.shipScript.isInControl = true;
			AndromedaGui.mainWnd.RemoveFreezeTimeWindow();
		}
		else if (universalTransportContainer.serverMessageIndex == 255)
		{
			NetworkScript.player.vessel.moveState = 0;
			NetworkScript.player.vessel.destinationX = NetworkScript.player.vessel.x;
			NetworkScript.player.vessel.destinationZ = NetworkScript.player.vessel.z;
			NetworkScript.player.vessel.isInControl = false;
			if (NetworkScript.player.shipScript != null)
			{
				NetworkScript.player.shipScript.isInControl = false;
			}
		}
		else if (universalTransportContainer.serverMessageIndex == 72)
		{
			if (GuiFramework.masterVolume != 0f && GuiFramework.fxVolume != 0f)
			{
				AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "turnOff");
				AudioManager.PlayGUISound(fromStaticSet);
			}
			NetworkScript.spaceLabelManager.AddNewMessage(GuiNewStyleBar.redColor, StaticData.Translate("key_critical_target_out_of_range").ToUpper(), NetworkScript.player.vessel, 64);
		}
		else if (universalTransportContainer.serverMessageIndex == 70)
		{
			if (GuiFramework.masterVolume != 0f && GuiFramework.fxVolume != 0f)
			{
				AudioClip audioClip = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "turnOff");
				AudioManager.PlayGUISound(audioClip);
			}
			NetworkScript.spaceLabelManager.AddNewMessage(GuiNewStyleBar.redColor, StaticData.Translate("key_no_access_level").ToUpper(), NetworkScript.player.vessel, 64);
		}
		else if (universalTransportContainer.serverMessageIndex == 20)
		{
			NotificationManager.AddNovaNotification(50);
		}
		else if (universalTransportContainer.serverMessageIndex == 21)
		{
			NotificationManager.AddNovaNotification(50);
		}
		else if (universalTransportContainer.serverMessageIndex == 68)
		{
			NetworkScript.player.cfg.criticalEnergy = 0f;
			NetworkScript.player.vessel.cfg.criticalEnergy = 0f;
			if (AndromedaGui.mainWnd != null)
			{
				AndromedaGui.mainWnd.RemoveCriticalIndication();
			}
		}
	}

	private void RemoveActiveSkillObject(ActiveSkillObject skill)
	{
		GameObjectPhysics gameObjectPhysic = null;
		this.gameObjects.TryGetValue(skill.targetNeibId, ref gameObjectPhysic);
		if (gameObjectPhysic != null)
		{
			gameObjectPhysic.RemoveActivatedSkill(skill);
		}
		Object.Destroy((GameObject)skill.gameObject);
		this.gameObjects.Remove(skill.neighbourhoodId);
		skill.gameObject = null;
	}

	public void RemoveGameObject(uint nbId)
	{
		if (!this.gameObjects.ContainsKey(nbId))
		{
			return;
		}
		GameObjectPhysics item = this.gameObjects.get_Item(nbId);
		if (item is ActiveSkillObject)
		{
			ActiveSkillObject activeSkillObject = (ActiveSkillObject)item;
			if (activeSkillObject.skillId == PlayerItems.TypeTalentsDecoy || activeSkillObject.skillId == PlayerItems.TypeTalentsForceWave || activeSkillObject.skillId == PlayerItems.TypeTalentsFocusFire || activeSkillObject.skillId == PlayerItems.TypeTalentsPowerCut || activeSkillObject.skillId == PlayerItems.TypeTalentsNanoShield)
			{
				this.RemoveActiveSkillObject(activeSkillObject);
			}
			return;
		}
		if (NetworkScript.player.shipScript != null && NetworkScript.player.shipScript.selectedObject != null && NetworkScript.player.shipScript.selectedObject.neighbourhoodId == nbId)
		{
			NetworkScript.player.shipScript.selectedObject = null;
			if (NetworkScript.player.shipScript.outOfMiningRangeMarker != null)
			{
				Object.Destroy(NetworkScript.player.shipScript.outOfMiningRangeMarker);
			}
		}
		Object.Destroy((GameObject)item.gameObject);
		if (item is PlayerObjectPhysics)
		{
			PlayerObjectPhysics playerObjectPhysic = (PlayerObjectPhysics)item;
			if (playerObjectPhysic.get_IsPve())
			{
				if (NetworkScript.player.vessel.shootingAt == playerObjectPhysic)
				{
					NetworkScript.player.vessel.isShooting = false;
					NetworkScript.player.vessel.shootingAt = null;
				}
			}
			else if (NetworkScript.clientSideClientsList.ContainsKey(playerObjectPhysic.playerId))
			{
				if (NetworkScript.clientSideClientsList.get_Item(playerObjectPhysic.playerId).shipScript.speedEffectShip != null)
				{
					Object.Destroy(NetworkScript.clientSideClientsList.get_Item(playerObjectPhysic.playerId).shipScript.speedEffectShip);
				}
				if (NetworkScript.clientSideClientsList.get_Item(playerObjectPhysic.playerId).shipScript.galaxyJumpEf != null)
				{
					Object.Destroy(NetworkScript.clientSideClientsList.get_Item(playerObjectPhysic.playerId).shipScript.galaxyJumpEf);
				}
				if (NetworkScript.clientSideClientsList.get_Item(playerObjectPhysic.playerId).shipScript.galaxyJumpIndication != null)
				{
					Object.Destroy(NetworkScript.clientSideClientsList.get_Item(playerObjectPhysic.playerId).shipScript.galaxyJumpIndication);
				}
				NetworkScript.clientSideClientsList.Remove(playerObjectPhysic.playerId);
			}
			NetworkScript.playerNameManager.removePOPName(playerObjectPhysic);
		}
		this.gameObjects.Remove(nbId);
		item.gameObject = null;
	}

	public static void RequestUserProfile(string userName)
	{
		// 
		// Current member / type: System.Void NetworkScript::RequestUserProfile(System.String)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void RequestUserProfile(System.String)
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

	public void SendEnterBaseCommand(StarBaseNet basse)
	{
		// 
		// Current member / type: System.Void NetworkScript::SendEnterBaseCommand(StarBaseNet)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void SendEnterBaseCommand(StarBaseNet)
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

	public void SendGalaxyJumpCommand(GalaxyJumpParam data)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("to_galaxy_id", data.destinationGalaxyId.ToString());
		dictionary.Add("currency", data.paymentCurrency.ToString());
		playWebGame.LogMixPanel(MixPanelEvents.EnterBase, dictionary);
		playWebGame.udp.ExecuteCommand(43, data);
	}

	public void SendHyperJumpCommand(HyperJumpNet hj)
	{
		// 
		// Current member / type: System.Void NetworkScript::SendHyperJumpCommand(HyperJumpNet)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void SendHyperJumpCommand(HyperJumpNet)
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

	public void SendStartPVPCommand()
	{
	}

	public void ServerOrderedExit()
	{
		this.errorText = "Server ordered exit command. Please login again!";
		this.showErrorMessageBox = true;
		Debug.Log("Server ordered exit.");
		try
		{
			playWebGame.udp.StopReceiveUdp();
			if (NetworkScript.player != null)
			{
				if (NetworkScript.player.shipScript != null)
				{
					NetworkScript.player.shipScript.destroyMe = true;
				}
			}
		}
		catch (Exception exception)
		{
			Debug.LogError(exception);
		}
	}

	private void SetCollisions()
	{
		LevelMap levelMap = Enumerable.First<LevelMap>(Enumerable.Where<LevelMap>(StaticData.allGalaxies, new Func<LevelMap, bool>(this, (LevelMap lm) => lm.get_galaxyId() == this.galaxy.get_galaxyId())));
		this.galaxy.collisionsMap = levelMap.collisionsMap;
		this.galaxy.collisionsMapZipped = levelMap.collisionsMapZipped;
		this.galaxy.collisionsMapStep = levelMap.collisionsMapStep;
		this.galaxy.isCollisionAware = levelMap.isCollisionAware;
	}

	private void SetExtractionPointOwner(ExtractionPoint point)
	{
		GameObject _gameObject = null;
		GameObject gameObject = null;
		_gameObject = ((GameObject)point.gameObject).get_transform().FindChild("HologramRayFaction1_pfb").get_gameObject();
		gameObject = ((GameObject)point.gameObject).get_transform().FindChild("HologramRayFaction2_pfb").get_gameObject();
		switch (point.ownerFraction)
		{
			case 0:
			{
				_gameObject.SetActive(false);
				gameObject.SetActive(false);
				break;
			}
			case 1:
			{
				_gameObject.SetActive(true);
				gameObject.SetActive(false);
				break;
			}
			case 2:
			{
				_gameObject.SetActive(false);
				gameObject.SetActive(true);
				break;
			}
		}
	}

	private void SetVolumesFromDB()
	{
		long amountAt = NetworkScript.player.playerBelongings.playerItems.GetAmountAt(PlayerItems.TypeSoundVolume);
		GuiFramework.voiceVolume = (float)(amountAt % (long)1000) / 100f;
		GuiFramework.fxVolume = (float)(amountAt / (long)1000 % (long)1000) / 100f;
		GuiFramework.musicVolume = (float)(amountAt / (long)1000000 % (long)1000) / 100f;
		GuiFramework.masterVolume = (float)(amountAt / (long)1000000000) / 100f;
		AudioManager.InitBGMusic();
	}

	private void ShowCollectRewardNotification(int rewardType)
	{
		int num = rewardType;
		switch (num)
		{
			case 1:
			{
				NotificationManager.AddTransformerReward(PlayerItems.TypeUltralibrium, 5);
				break;
			}
			case 2:
			{
				NotificationManager.AddTransformerReward(PlayerItems.TypeUltralibrium, 15);
				break;
			}
			case 3:
			{
				NotificationManager.AddTransformerReward(PlayerItems.TypeUltralibrium, 25);
				break;
			}
			default:
			{
				if (num == 100)
				{
					NotificationManager.AddTransformerReward(PlayerItems.TypeUltralibrium, 5);
					break;
				}
				else if (num == 200)
				{
					NotificationManager.AddTransformerReward(PlayerItems.TypeUltralibrium, 15);
					break;
				}
				else if (num == 300)
				{
					NotificationManager.AddTransformerReward(PlayerItems.TypeUltralibrium, 25);
					break;
				}
				else if (num == 400)
				{
					NotificationManager.AddTransformerReward(PlayerItems.TypeUltralibrium, 50);
					break;
				}
				else if (num == 500)
				{
					NotificationManager.AddTransformerReward(PlayerItems.TypeUltralibrium, 75);
					break;
				}
				else if (num == 600)
				{
					NotificationManager.AddTransformerReward(PlayerItems.TypeUltralibrium, 100);
					break;
				}
				else if (num == 1000)
				{
					NotificationManager.AddTransformerReward(PlayerItems.TypeUltralibrium, 5);
					break;
				}
				else if (num == 2000)
				{
					NotificationManager.AddTransformerReward(PlayerItems.TypeUltralibrium, 15);
					break;
				}
				else if (num == 3000)
				{
					NotificationManager.AddTransformerReward(PlayerItems.TypeUltralibrium, 25);
					break;
				}
				else if (num == 4000)
				{
					NotificationManager.AddTransformerReward(PlayerItems.TypeUltralibrium, 50);
					break;
				}
				else if (num == 5000)
				{
					NotificationManager.AddTransformerReward(PlayerItems.TypeUltralibrium, 75);
					break;
				}
				else if (num == 6000)
				{
					NotificationManager.AddTransformerReward(PlayerItems.TypeUltralibrium, 100);
					break;
				}
				else
				{
					break;
				}
			}
		}
	}

	private void ShowCriticalHit(long playerId)
	{
		PlayerDataEx playerDataEx = null;
		if (!NetworkScript.clientSideClientsList.TryGetValue(playerId, ref playerDataEx))
		{
			return;
		}
		GameObject gameObject = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("NewCritical_pfb"));
		TextMesh componentInChildren = gameObject.GetComponentInChildren<TextMesh>();
		componentInChildren.set_text(string.Format("Critical Strike!", new object[0]));
		TrackingPoPScript component = gameObject.GetComponent<TrackingPoPScript>();
		component.target = playerDataEx.shipScript.p;
		component.timeToDestroy = 2f;
		component.deltaY = 2.4f;
	}

	private void ShowInSpaceObjectiveProgres(int objId)
	{
		NetworkScript.<ShowInSpaceObjectiveProgres>c__AnonStorey46 variable = null;
		IList<PlayerQuest> values = NetworkScript.player.playerBelongings.playerQuests.get_Values();
		if (NetworkScript.<>f__am$cache32 == null)
		{
			NetworkScript.<>f__am$cache32 = new Func<PlayerQuest, int>(null, (PlayerQuest s) => s.currentQuestId);
		}
		IEnumerator<int> enumerator = Enumerable.Select<PlayerQuest, int>(values, NetworkScript.<>f__am$cache32).GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				enumerator.get_Current();
				NewQuest newQuest = Enumerable.FirstOrDefault<NewQuest>(Enumerable.Where<NewQuest>(Enumerable.Union<NewQuest>(StaticData.allQuests, StaticData.allDailyQuests), new Func<NewQuest, bool>(variable, (NewQuest t) => t.id == this.questId)));
				if (newQuest == null)
				{
					continue;
				}
				List<NewQuestObjective>.Enumerator enumerator1 = newQuest.objectives.GetEnumerator();
				try
				{
					while (enumerator1.MoveNext())
					{
						NewQuestObjective current = enumerator1.get_Current();
						if (current.id != objId)
						{
							continue;
						}
						if (current.type != 45 && current.type != 51 && current.type != 44 && current.type != 50 && current.type != 57 && current.type != 63 && current.type != 56 && current.type != 62 && current.type != 41 && current.type != 40 && current.type != 47 && current.type != 53 && current.type != 46 && current.type != 52 && current.type != 59 && current.type != 65 && current.type != 58 && current.type != 64 && current.type != 43 && current.type != 42 && current.type != 75 && current.type != 74 && current.type != 72 && current.type != 70 && current.type != 73 && current.type != 71)
						{
							int num = current.targetAmount;
							int amountAt = NetworkScript.player.playerBelongings.playerObjectives.GetAmountAt(objId);
							if (amountAt > num)
							{
								amountAt = num;
							}
							string str = string.Format("{0} {1}/{2}", current.GetObjectiveDescription(), amountAt.ToString("##,##0"), num.ToString("##,##0"));
							NetworkScript.spaceLabelManager.AddSystemMessage(GuiNewStyleBar.orangeColor, str, 28);
						}
						return;
					}
				}
				finally
				{
					enumerator1.Dispose();
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
	}

	public void ShowMessage(string text, int seconds)
	{
		this.startedShowMessageTime = DateTime.get_Now();
		this.errorText = text;
		this.showMessageDuration = seconds;
		this.showErrorMessageBox = true;
	}

	private void ShowNanoShield(PlayerObjectPhysics pop)
	{
		ActiveSkillObject[] _activatedSkillsSafe = pop.get_activatedSkillsSafe();
		if (NetworkScript.<>f__am$cache29 == null)
		{
			NetworkScript.<>f__am$cache29 = new Func<ActiveSkillObject, bool>(null, (ActiveSkillObject t) => t.skillId == PlayerItems.TypeTalentsNanoShield);
		}
		ActiveSkillObject activeSkillObject = Enumerable.FirstOrDefault<ActiveSkillObject>(Enumerable.Where<ActiveSkillObject>(_activatedSkillsSafe, NetworkScript.<>f__am$cache29));
		if (activeSkillObject == null)
		{
			return;
		}
		if (this.gameObjects.ContainsKey(activeSkillObject.neighbourhoodId))
		{
			GameObject item = (GameObject)this.gameObjects.get_Item(activeSkillObject.neighbourhoodId).gameObject;
			Component[] componentsInChildren = item.GetComponentsInChildren<Light>(true);
			for (int i = 0; i < (int)componentsInChildren.Length; i++)
			{
				componentsInChildren[i].get_gameObject().SetActive(true);
			}
		}
	}

	private void ShowObjectiveDoneNotification(int objId)
	{
		NetworkScript.<ShowObjectiveDoneNotification>c__AnonStorey47 variable = null;
		IList<PlayerQuest> values = NetworkScript.player.playerBelongings.playerQuests.get_Values();
		if (NetworkScript.<>f__am$cache33 == null)
		{
			NetworkScript.<>f__am$cache33 = new Func<PlayerQuest, bool>(null, (PlayerQuest t) => t.inProgress);
		}
		IEnumerable<PlayerQuest> enumerable = Enumerable.Where<PlayerQuest>(values, NetworkScript.<>f__am$cache33);
		if (NetworkScript.<>f__am$cache34 == null)
		{
			NetworkScript.<>f__am$cache34 = new Func<PlayerQuest, int>(null, (PlayerQuest s) => s.currentQuestId);
		}
		IEnumerator<int> enumerator = Enumerable.Select<PlayerQuest, int>(enumerable, NetworkScript.<>f__am$cache34).GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				enumerator.get_Current();
				NewQuest newQuest = Enumerable.FirstOrDefault<NewQuest>(Enumerable.Where<NewQuest>(Enumerable.Union<NewQuest>(StaticData.allQuests, StaticData.allDailyQuests), new Func<NewQuest, bool>(variable, (NewQuest t) => t.id == this.questId)));
				if (newQuest == null)
				{
					continue;
				}
				List<NewQuestObjective>.Enumerator enumerator1 = newQuest.objectives.GetEnumerator();
				try
				{
					while (enumerator1.MoveNext())
					{
						NewQuestObjective current = enumerator1.get_Current();
						if (current.id != objId)
						{
							continue;
						}
						NotificationManager.AddObjectiveDoneNotification(current);
						return;
					}
				}
				finally
				{
					enumerator1.Dispose();
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
	}

	private void SpawnDefenceTurret(DefenceTurret turret)
	{
		GameObject gameObject = null;
		try
		{
			gameObject = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab(string.Concat("Pve/", turret.assetName)));
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			Debug.LogError(string.Format("Couldn't load resource {0} on SpawnDefenceTurret!", string.Concat("Pve/", turret.assetName)));
			throw exception;
		}
		gameObject.GetComponent<DefenceTurretScript>().turret = turret;
		turret.gameObject = gameObject;
		this.gameObjects.Add(turret.neighbourhoodId, turret);
	}

	internal void SpawnGameObject(GameObjectPhysics gop)
	{
		// 
		// Current member / type: System.Void NetworkScript::SpawnGameObject(GameObjectPhysics)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void SpawnGameObject(GameObjectPhysics)
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

	public void SpawnMineral(Mineral mineral)
	{
		try
		{
			string empty = string.Empty;
			GameObject gameObject = null;
			empty = string.Concat("Mineables/", mineral.assetName, "_pfb");
			gameObject = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab(empty));
			if (gameObject != null)
			{
				MineralScript component = gameObject.GetComponent<MineralScript>();
				component.mineral = new MineralEx();
				mineral.CopyPropsTo(component.mineral);
				component.mineral.neighbourhoodId = mineral.neighbourhoodId;
				component.mineral.galaxy = this.galaxy;
				component.mineral.gameObject = gameObject;
				component.mineral.isOnClientSide = true;
				component.mineral.ownerName = mineral.ownerName;
				component.mineral.items = mineral.items;
				component.mineral.resourceQuantities = mineral.resourceQuantities;
				component.mineral.source = mineral.source;
				component.mineral.rotationSpeedX = mineral.rotationSpeedX;
				component.mineral.rotationSpeedY = mineral.rotationSpeedY;
				component.mineral.rotationSpeedZ = mineral.rotationSpeedZ;
				component.mineral.miningAccomplishedCallback = new Action<Mineral>(this, NetworkScript.DeactiveteMineral);
				if (mineral.miningPlayerId != 0 && NetworkScript.clientSideClientsList.ContainsKey(mineral.miningPlayerId))
				{
					component.mineral.miningPlayer = NetworkScript.clientSideClientsList.get_Item(mineral.miningPlayerId).vessel;
				}
				NetworkScript.ApplyPhysicsToGameObject(component.mineral, gameObject);
				this.gameObjects.Add(mineral.neighbourhoodId, component.mineral);
			}
		}
		catch (Exception exception)
		{
			Debug.LogError(exception);
			Debug.Log(string.Format("Instantiating mineral, assetName={0}, id={1}", mineral.assetName, mineral.neighbourhoodId));
		}
	}

	private void SpawnMovableActiveSkill(ActiveSkillObject skill)
	{
		if (skill.skillId == PlayerItems.TypeTalentsForceWave || skill.skillId == PlayerItems.TypeTalentsRocketBarrage || skill.skillId == PlayerItems.TypeTalentsPulseNova)
		{
			skill.galaxy = this.galaxy;
			skill.caster = (PlayerObjectPhysics)this.gameObjects.get_Item(skill.casterNeibId);
			this.gameObjects.Add(skill.neighbourhoodId, skill);
			this.PutActiveSkillOnScene(skill);
		}
		else
		{
			if (!this.gameObjects.ContainsKey(skill.casterNeibId) || !this.gameObjects.ContainsKey(skill.targetNeibId) || this.gameObjects.ContainsKey(skill.neighbourhoodId))
			{
				return;
			}
			skill.galaxy = this.galaxy;
			skill.caster = (PlayerObjectPhysics)this.gameObjects.get_Item(skill.casterNeibId);
			skill.target = this.gameObjects.get_Item(skill.targetNeibId);
			if (skill.skillId != PlayerItems.TypeTalentsStealth || !this.gameObjects.ContainsKey(skill.neighbourhoodId))
			{
				skill.target.AddActivatedSkill(skill);
				this.gameObjects.Add(skill.neighbourhoodId, skill);
				this.PutActiveSkillOnScene(skill);
			}
			else
			{
				ActiveSkillObject[] _activatedSkillsSafe = skill.target.get_activatedSkillsSafe();
				if (NetworkScript.<>f__am$cache2A == null)
				{
					NetworkScript.<>f__am$cache2A = new Func<ActiveSkillObject, bool>(null, (ActiveSkillObject s) => s.skillId == PlayerItems.TypeTalentsStealth);
				}
				ActiveSkillObject activeSkillObject = Enumerable.FirstOrDefault<ActiveSkillObject>(Enumerable.Where<ActiveSkillObject>(_activatedSkillsSafe, NetworkScript.<>f__am$cache2A));
				if (activeSkillObject != null)
				{
					activeSkillObject.iterationTime = (activeSkillObject.iterationTime == activeSkillObject.castTime ? skill.castTime : skill.endTime);
					activeSkillObject.endTime = skill.endTime;
				}
			}
		}
	}

	internal PlayerObjectPhysics SpawnPlayer(JoiningPlayer jp, bool isRuling)
	{
		// 
		// Current member / type: PlayerObjectPhysics NetworkScript::SpawnPlayer(JoiningPlayer,System.Boolean)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: PlayerObjectPhysics SpawnPlayer(JoiningPlayer,System.Boolean)
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

	private void SpawnProjectile(ProjectileObject projectile)
	{
		if (!this.gameObjects.ContainsKey(projectile.shooterNeibId) || !this.gameObjects.ContainsKey(projectile.targetNeibId))
		{
			return;
		}
		projectile.galaxy = this.galaxy;
		projectile.shooter = this.gameObjects.get_Item(projectile.shooterNeibId);
		projectile.target = this.gameObjects.get_Item(projectile.targetNeibId);
		this.gameObjects.Add(projectile.neighbourhoodId, projectile);
		this.PutProjectileOnScene(projectile);
	}

	private void SpawnPVE(PvEPhysics _pve)
	{
		// 
		// Current member / type: System.Void NetworkScript::SpawnPVE(PvEPhysics)
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void SpawnPVE(PvEPhysics)
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

	private void Start()
	{
		NetworkScript.buildNeighbourhoodDone = false;
		DateTime now = DateTime.get_Now();
		this.nextPingTime = now.AddSeconds((double)PureUdpClient.PING_SECONDS);
		playWebGame.ResetLoadProgress();
		AndromedaGui.CreateGui();
		AndromedaGui.gui.StartFadeOut();
		if (AndromedaGui.galaxyJumpWnd != null)
		{
			AndromedaGui.gui.RemoveWindow(AndromedaGui.galaxyJumpWnd.handler);
			AndromedaGui.galaxyJumpWnd = null;
		}
		this.customActions = new List<Action<int>>();
		NetworkScript.playerNameManager = new PlayerNameManager();
		NetworkScript.playerNameManager.DestroyAll();
		this.gameObjects = new SortedList<uint, GameObjectPhysics>();
		NetworkScript.partyInvitees.Clear();
		AndromedaGui.isDebugConsoleOn = false;
		AndromedaGui.SetFonts(playWebGame.assets);
		GameObjectPhysics.logMethod = new Action<string>(null, Debug.Log);
		if (playWebGame.authorization == null || playWebGame.authorization.returnCode != 0 && playWebGame.authorization.returnCode != 2)
		{
			this.errorText = "You are not authorized to play. Please log on!";
			this.showErrorMessageBox = true;
			return;
		}
		playWebGame.udp.forcePlayerOffGameOnNoServer = new Action(this, NetworkScript.DisconnectOnNetworkProblem);
		playWebGame.udp.ServerOrderedExit = new Action(this, NetworkScript.ServerOrderedExit);
		playWebGame.udp.OnConnectionEstablished = new Action(this, NetworkScript.OnTcpConnectedd);
		playWebGame.udp.StartReceiveUdp(new Action<Exception, PlayerData>(null, playWebGame.OnErrorDisconnected));
		this.currentScreenHeight = (float)Screen.get_height();
		this.currentScreenWidth = (float)Screen.get_width();
		base.Invoke("StartNotifications", 2f);
		if (QualitySettings.GetQualityLevel() != 0)
		{
			Camera.get_main().set_renderingPath(2);
		}
		else
		{
			Camera.get_main().set_renderingPath(0);
		}
		Application.ExternalCall("logState", new object[] { 9, playWebGame.timeSinceStart });
	}

	private void StartMining(StartMiningMessage smm)
	{
		Mineral mineral = null;
		IEnumerator<GameObjectPhysics> enumerator = this.gameObjects.get_Values().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				GameObjectPhysics current = enumerator.get_Current();
				if (!(current is Mineral))
				{
					continue;
				}
				Mineral mineral1 = (Mineral)current;
				if (mineral1.neighbourhoodId != smm.mineralNbId)
				{
					continue;
				}
				mineral = mineral1;
				break;
			}
		}
		finally
		{
			if (enumerator == null)
			{
			}
			enumerator.Dispose();
		}
		if (mineral == null)
		{
			return;
		}
		MineralEx now = (MineralEx)mineral;
		if (now.miningPlayer != null)
		{
			return;
		}
		PlayerDataEx item = null;
		if (!NetworkScript.clientSideClientsList.ContainsKey(smm.miningPlayerId))
		{
			if (smm.miningPlayerId != NetworkScript.player.vessel.playerId)
			{
				return;
			}
			item = NetworkScript.player;
		}
		else
		{
			item = NetworkScript.clientSideClientsList.get_Item(smm.miningPlayerId);
		}
		PlayerObjectPhysics playerObjectPhysic = item.vessel;
		playerObjectPhysic.miningMineralNbId = smm.mineralNbId;
		playerObjectPhysic.miningState = 1;
		playerObjectPhysic.miningMineral = now;
		now.miningPlayerId = playerObjectPhysic.playerId;
		now.miningPlayer = playerObjectPhysic;
		now.miningStartTime = DateTime.get_Now();
		GameObject gameObject = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("MineralBeam"));
		gameObject.SetActive(false);
		item.shipScript._miningBeam = gameObject;
		item.shipScript._miningBeam.GetComponent<MiningBeamScript>().mineral = (GameObject)now.gameObject;
		item.shipScript._miningBeam.GetComponent<MiningBeamScript>().ship = item.gameObject;
		item.shipScript._miningBeam.get_transform().set_position(item.gameObject.get_transform().get_position());
		item.shipScript._miningBeam.get_transform().LookAt(((GameObject)now.gameObject).get_transform().get_position());
		gameObject.SetActive(true);
	}

	private void StartNotifications()
	{
		NotificationManager.Start();
	}

	private void StopMining(StopMiningMessage smm)
	{
		Mineral mineral = null;
		IEnumerator<GameObjectPhysics> enumerator = this.gameObjects.get_Values().GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				GameObjectPhysics current = enumerator.get_Current();
				if (!(current is Mineral))
				{
					continue;
				}
				Mineral mineral1 = (Mineral)current;
				if (mineral1.neighbourhoodId != smm.mineralNbId)
				{
					continue;
				}
				mineral = mineral1;
				break;
			}
		}
		finally
		{
			if (enumerator == null)
			{
			}
			enumerator.Dispose();
		}
		if (mineral == null)
		{
			return;
		}
		if (!NetworkScript.clientSideClientsList.ContainsKey(smm.miningPlayerId))
		{
			return;
		}
		PlayerDataEx item = NetworkScript.clientSideClientsList.get_Item(smm.miningPlayerId);
		PlayerObjectPhysics playerObjectPhysic = item.vessel;
		playerObjectPhysic.AbortMining();
		Object.Destroy(NetworkScript.clientSideClientsList.get_Item(playerObjectPhysic.playerId).shipScript._miningBeam);
		NetworkScript.clientSideClientsList.get_Item(playerObjectPhysic.playerId).shipScript.isMiningActionSend = false;
		NetworkScript.clientSideClientsList.get_Item(playerObjectPhysic.playerId).shipScript.autoMiningMineral = null;
		NetworkScript.clientSideClientsList.get_Item(playerObjectPhysic.playerId).shipScript.lastMinedMineralNbId = mineral.neighbourhoodId;
		if (playerObjectPhysic.neighbourhoodId == NetworkScript.player.vessel.neighbourhoodId)
		{
			if (GuiFramework.masterVolume != 0f && GuiFramework.fxVolume != 0f)
			{
				AudioClip fromStaticSet = (AudioClip)playWebGame.assets.GetFromStaticSet("Sounds", "collect_cargo");
				AudioManager.PlayGUISound(fromStaticSet);
			}
			if (item.shipScript.selectedObject is MineralEx && item.shipScript.selectedObject.neighbourhoodId == mineral.neighbourhoodId)
			{
				Object.Destroy(item.shipScript._lock);
				item.shipScript.selectedObject = null;
			}
			if (smm.collectedItems != null)
			{
				foreach (SlotItem collectedItem in smm.collectedItems)
				{
					string empty = string.Empty;
					empty = (collectedItem.get_Amount() <= 1 ? string.Concat("+ ", StaticData.Translate(StaticData.allTypes.get_Item(collectedItem.get_ItemType()).uiName)) : string.Format("+ {0} {1}", collectedItem.get_Amount(), StaticData.Translate(StaticData.allTypes.get_Item(collectedItem.get_ItemType()).uiName)));
					this.EqupItemIfPossible(collectedItem);
					NetworkScript.spaceLabelManager.AddMessage(GuiNewStyleBar.orangeColor, empty, item.vessel);
				}
			}
			if (smm.collectedMinerals != null)
			{
				IEnumerator<KeyValuePair<ushort, int>> enumerator1 = smm.collectedMinerals.GetEnumerator();
				try
				{
					while (enumerator1.MoveNext())
					{
						KeyValuePair<ushort, int> keyValuePair = enumerator1.get_Current();
						int value = keyValuePair.get_Value();
						string str = string.Concat("+ ", value.ToString(), " ", StaticData.Translate(StaticData.allTypes.get_Item(keyValuePair.get_Key()).uiName));
						NetworkScript.spaceLabelManager.AddMessage(GuiNewStyleBar.orangeColor, str, item.vessel);
					}
				}
				finally
				{
					if (enumerator1 == null)
					{
					}
					enumerator1.Dispose();
				}
			}
		}
	}

	private void SwitchExtractionPointAlarm(ExtractionPoint point)
	{
		bool flag = point.state == 1;
		GameObject _gameObject = null;
		_gameObject = ((GameObject)point.gameObject).get_transform().FindChild("EstablishingControl").get_gameObject();
		if (_gameObject != null)
		{
			_gameObject.SetActive(flag);
		}
	}

	private void Update()
	{
		// 
		// Current member / type: System.Void NetworkScript::Update()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void Update()
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
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 449
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 75
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 325
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 467
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 79
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 49
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 394
		//    в ..(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 63
		//    в ..Visit(ICodeNode ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 274
		//    в ..Visit[,]( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 284
		//    в ..Visit( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 315
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:строка 335
		//    в ..( ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\CodePatternsStep.cs:строка 39
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

	private void UpdateCollisionsMapInStaticData(short galaxyId, byte[] collisionsMapZipped, float collisionsMapStep)
	{
		NetworkScript.<UpdateCollisionsMapInStaticData>c__AnonStorey40 variable = null;
		LevelMap levelMap = Enumerable.FirstOrDefault<LevelMap>(Enumerable.Where<LevelMap>(StaticData.allGalaxies, new Func<LevelMap, bool>(variable, (LevelMap g) => g.get_galaxyId() == this.galaxyId)));
		if (levelMap == null)
		{
			return;
		}
		levelMap.collisionsMapZipped = collisionsMapZipped;
		levelMap.collisionsMapStep = collisionsMapStep;
		levelMap.isCollisionAware = true;
		levelMap.UnzipCollisionsMap();
	}

	private void UpdatePartyMemberVisibility()
	{
		IList<GameObjectPhysics> values = this.gameObjects.get_Values();
		if (NetworkScript.<>f__am$cache27 == null)
		{
			NetworkScript.<>f__am$cache27 = new Func<GameObjectPhysics, bool>(null, (GameObjectPhysics go) => (!(go is PlayerObjectPhysics) || ((PlayerObjectPhysics)go).playerId == 0 ? false : ((PlayerObjectPhysics)go).isInStealthMode));
		}
		GameObjectPhysics[] array = Enumerable.ToArray<GameObjectPhysics>(Enumerable.Where<GameObjectPhysics>(values, NetworkScript.<>f__am$cache27));
		GameObjectPhysics[] gameObjectPhysicsArray = array;
		for (int i = 0; i < (int)gameObjectPhysicsArray.Length; i++)
		{
			GameObjectPhysics gameObjectPhysic = gameObjectPhysicsArray[i];
			if (((PlayerObjectPhysics)gameObjectPhysic).playerId != NetworkScript.player.playId)
			{
				if (!NetworkScript.IsPartyMember(((PlayerObjectPhysics)gameObjectPhysic).playerId))
				{
					this.HideNanoShield((PlayerObjectPhysics)gameObjectPhysic);
					if (NetworkScript.player.shipScript.selectedObject != null && NetworkScript.player.shipScript.selectedObject.neighbourhoodId == gameObjectPhysic.neighbourhoodId)
					{
						NetworkScript.player.shipScript.DeselectCurrentObject();
						if (NetworkScript.player.vessel.selectedPoPnbId != 0)
						{
							playWebGame.udp.ExecuteCommand(49, new UniversalTransportContainer(), 56);
							NetworkScript.player.vessel.selectedPoPnbId = 0;
						}
					}
					this.MakeInvisible((GameObject)gameObjectPhysic.gameObject);
				}
				else
				{
					this.ShowNanoShield((PlayerObjectPhysics)gameObjectPhysic);
					this.MakeSemiInvisible((GameObject)gameObjectPhysic.gameObject);
					PlayerDataEx playerDataEx = null;
					if (NetworkScript.clientSideClientsList.TryGetValue(((PlayerObjectPhysics)gameObjectPhysic).playerId, ref playerDataEx))
					{
						playerDataEx.shipScript.get_gameObject().GetComponent<ShipStatsGuiRelativeToGameObject>().Populate();
					}
				}
			}
		}
	}

	private void UpdatePlayerLevel(UdpCommHeader header)
	{
		GenericData genericDatum = (GenericData)header.data;
		if (!NetworkScript.clientSideClientsList.ContainsKey(genericDatum.long1))
		{
			return;
		}
		PlayerDataEx item = NetworkScript.clientSideClientsList.get_Item(genericDatum.long1);
		item.cfg.hitPoints = item.vessel.cfg.hitPointsMax;
		item.cfg.shield = (float)item.vessel.cfg.shieldMax;
		item.vessel.cfg = item.cfg;
		item.cfg.playerLevel = (short)genericDatum.int1;
		if (item.vessel.neighbourhoodId == NetworkScript.player.vessel.neighbourhoodId)
		{
			playWebGame.LogMixPanel(MixPanelEvents.LevelUp, null);
			item.playerBelongings.playerLevel = item.cfg.playerLevel;
			if ((item.playerBelongings.playerLevel == 9 || item.playerBelongings.playerLevel == 10 || item.playerBelongings.playerLevel == 8 || item.playerBelongings.playerLevel == 30 || item.playerBelongings.playerLevel == 7) && AndromedaGui.mainWnd != null)
			{
				AndromedaGui.mainWnd.RefreshMenuBtnState();
			}
			if (item.playerBelongings.playerLevel == 10 && AndromedaGui.mainWnd != null)
			{
				AndromedaGui.mainWnd.ShowFactionWarNotificationWindow();
			}
			if (AndromedaGui.personalStatsWnd != null)
			{
				AndromedaGui.personalStatsWnd.UpdateFullPersonalStats();
			}
		}
		else if (NetworkScript.IsPartyMember(genericDatum.long1) && AndromedaGui.personalStatsWnd != null)
		{
			AndromedaGui.personalStatsWnd.UpdateParty();
		}
		item.vessel.cfg.playerLevel = item.cfg.playerLevel;
		GameObject gameObject = (GameObject)Object.Instantiate(playWebGame.assets.GetPrefab("LevelUP_pfb"));
		gameObject.get_transform().set_position(new Vector3(item.vessel.x, 1.5f, item.vessel.z));
		gameObject.GetComponent<LevelUpAnimationScript>().target = item.vessel;
		TextMesh componentInChildren = gameObject.GetComponentInChildren<TextMesh>();
		Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < (int)componentsInChildren.Length; i++)
		{
			Renderer renderer = componentsInChildren[i];
			if (renderer.get_name() == "SpaceLbl")
			{
				renderer.get_material().set_color(GuiNewStyleBar.greenColor);
			}
		}
		if (componentInChildren != null)
		{
			componentInChildren.set_text(StaticData.Translate("key_network_script_levelup").ToUpper());
		}
		if (genericDatum.long1 == NetworkScript.player.vessel.playerId && NetworkScript.player.playerBelongings != null && (NetworkScript.player.playerBelongings.playerLevel <= 10 || NetworkScript.player.playerBelongings.playerLevel == 15 || NetworkScript.player.playerBelongings.playerLevel == 20 || NetworkScript.player.playerBelongings.playerLevel == 30))
		{
			NetworkScript.player.shipScript.OpenLevelUpWindow();
		}
		item.shipScript.get_gameObject().GetComponent<ShipStatsGuiRelativeToGameObject>().Populate();
	}

	private void UseGamblerResponse(UdpCommHeader header)
	{
		SlotItem slotItem = (SlotItem)header.data;
		if (Inventory.dialogWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(Inventory.dialogWindow.handler);
			Inventory.dialogWindow = null;
			AndromedaGui.gui.activeToolTipId = -1;
		}
		if (!PlayerItems.IsWeapon(slotItem.get_ItemType()))
		{
			Inventory.CreateNewItemDialog(slotItem, out Inventory.dialogWindow);
		}
		else
		{
			Inventory.CreateNewItemDialog((SlotItemWeapon)header.data, out Inventory.dialogWindow);
		}
	}
}