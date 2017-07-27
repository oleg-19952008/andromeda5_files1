using System;

public class RandomGenerator
{
	private static Random _rnd;

	public static Random rnd
	{
		get
		{
			return RandomGenerator._rnd;
		}
	}

	public RandomGenerator()
	{
		RandomGenerator._rnd = new Random(DateTime.Now.Millisecond);
	}

	public int ChooseOne(int[] probabilities, int range = 101)
	{
		int length;
		int num = RandomGenerator.rnd.Next(range);
		int num1 = 0;
		int num2 = 0;
		while (true)
		{
			if (num2 < (int)probabilities.Length)
			{
				num1 = num1 + probabilities[num2];
				if (probabilities[num2] != 0)
				{
					if (num <= num1)
					{
						length = num2;
						break;
					}
				}
				num2++;
			}
			else
			{
				length = (int)probabilities.Length - 1;
				break;
			}
		}
		return length;
	}

	public int ChooseOne(short[] probabilities, int range = 101)
	{
		int length;
		int num = RandomGenerator.rnd.Next(range);
		int num1 = 0;
		int num2 = 0;
		while (true)
		{
			if (num2 < (int)probabilities.Length)
			{
				num1 = num1 + probabilities[num2];
				if (probabilities[num2] != 0)
				{
					if (num <= num1)
					{
						length = num2;
						break;
					}
				}
				num2++;
			}
			else
			{
				length = (int)probabilities.Length - 1;
				break;
			}
		}
		return length;
	}

	public bool MeasureChance(float percent)
	{
		int num = PlayerObjectPhysics.rnd.Next(0, 101);
		return (float)num <= percent;
	}

	public int Next(int min, int max)
	{
		int num;
		try
		{
			lock (RandomGenerator.rnd)
			{
				try
				{
					num = RandomGenerator.rnd.Next(min, max);
				}
				catch (Exception exception)
				{
					Console.WriteLine("{0:HH:mm:ss ffff} Err in Random.Next in LockSection {1}", DateTime.Now, exception);
					num = min;
				}
			}
		}
		catch (Exception exception1)
		{
			Console.WriteLine("{0:HH:mm:ss ffff} Err in Random.Next out of LockSection {1}", DateTime.Now, exception1);
			num = min;
		}
		return num;
	}
}