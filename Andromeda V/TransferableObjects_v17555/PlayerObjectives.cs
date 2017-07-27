using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class PlayerObjectives : ITransferable
{
	public SortedList<int, int> objectives = new SortedList<int, int>();

	public PlayerObjectives()
	{
	}

	public void Add(int key, int amount)
	{
		if (this.objectives.ContainsKey(key))
		{
			SortedList<int, int> item = this.objectives;
			SortedList<int, int> nums = item;
			int num = key;
			item[num] = nums[num] + amount;
		}
		else
		{
			this.objectives.Add(key, amount);
		}
	}

	public void Delete(int key)
	{
		if (this.objectives.ContainsKey(key))
		{
			this.objectives.Remove(key);
		}
	}

	public void Deserialize(BinaryReader br)
	{
		int i;
		int[] numArray = new int[br.ReadInt32()];
		for (i = 0; i < (int)numArray.Length; i++)
		{
			numArray[i] = br.ReadInt32();
		}
		int[] numArray1 = new int[br.ReadInt32()];
		for (i = 0; i < (int)numArray.Length; i++)
		{
			numArray1[i] = br.ReadInt32();
		}
		for (i = 0; i < (int)numArray.Length; i++)
		{
			this.objectives.Add(numArray[i], numArray1[i]);
		}
	}

	public int GetAmountAt(int key)
	{
		int num;
		num = (this.objectives.ContainsKey(key) ? this.objectives[key] : 0);
		return num;
	}

	public void Serialize(BinaryWriter bw)
	{
		int i;
		int[] array = this.objectives.Keys.ToArray<int>();
		int[] numArray = this.objectives.Values.ToArray<int>();
		bw.Write((int)array.Length);
		for (i = 0; i < (int)array.Length; i++)
		{
			bw.Write(array[i]);
		}
		bw.Write((int)numArray.Length);
		for (i = 0; i < (int)array.Length; i++)
		{
			bw.Write(numArray[i]);
		}
	}

	public void Set(int key, int val)
	{
		if (this.objectives.ContainsKey(key))
		{
			this.objectives[key] = val;
		}
		else
		{
			this.objectives.Add(key, val);
		}
	}
}