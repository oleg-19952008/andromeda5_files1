using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerNameManager
{
	private SortedList<uint, PlayerObjectName> collection = new SortedList<uint, PlayerObjectName>();

	public PlayerNameManager()
	{
	}

	public void AddPOPName(Color color, string text, PlayerObjectPhysics pop)
	{
		PlayerObjectName item = null;
		if (this.collection.ContainsKey(pop.neighbourhoodId))
		{
			item = this.collection.get_Item(pop.neighbourhoodId);
		}
		else
		{
			item = new PlayerObjectName()
			{
				pop = pop
			};
			this.collection.Add(pop.neighbourhoodId, item);
		}
		item.AddName(color, text);
	}

	public void DestroyAll()
	{
		PlayerObjectName[] array = Enumerable.ToArray<PlayerObjectName>(this.collection.get_Values());
		for (int i = 0; i < Enumerable.Count<PlayerObjectName>(array); i++)
		{
			array[i].Destroy();
		}
		this.collection.Clear();
	}

	public void removePOPName(GameObjectPhysics pop)
	{
		if (this.collection.ContainsKey(pop.neighbourhoodId))
		{
			this.collection.get_Item(pop.neighbourhoodId).RemoveName();
			this.collection.Remove(pop.neighbourhoodId);
		}
	}

	public void Updating()
	{
		int num = 0;
		PlayerObjectName[] array = Enumerable.ToArray<PlayerObjectName>(this.collection.get_Values());
		while (num < (int)array.Length)
		{
			PlayerObjectName playerObjectName = array[num];
			if (playerObjectName == null)
			{
				return;
			}
			playerObjectName.Updating();
			if (playerObjectName.name == null)
			{
				this.collection.Remove(playerObjectName.pop.neighbourhoodId);
				array = Enumerable.ToArray<PlayerObjectName>(this.collection.get_Values());
				num--;
			}
			num++;
		}
	}
}