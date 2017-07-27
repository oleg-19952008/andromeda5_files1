using System;
using System.Text;
namespace TransferableObjects
{
	public class Zip
	{
		public Zip()
		{
		}

		public static byte[] Archive(byte[] arr)
		{
            System.IO.File.AppendAllText(Environment.CurrentDirectory + "\\receive_dll.txt", ("REC: " + Encoding.UTF8.GetString(arr)));
            unsafe
			{
				byte[] numArray;
				byte[] numArray1 = new byte[0];
				uint length = (uint)arr.Length;
				if (length != 0)
				{
					uint num = 0;
					uint num1 = 0;
					while ( num <   arr.Length) 
					{
						uint num2 = Zip.CountRepeating(num, ref arr);
						Array.Resize<byte>(ref numArray1, (int)numArray1.Length + 5);
						Array.Copy(BitConverter.GetBytes(num2), (long)0, numArray1, (long)num1, (long)4);
						numArray1[num1 + 4] = arr[num];
						num1 = num1 + 5;
						num = num + num2;
						if (num != length)
						{
							try
							{
								num2 = Zip.CountDifferent(num, ref arr);
								Array.Resize<byte>(ref numArray1, (int)((int)numArray1.Length + 4 + num2));
								Array.Copy(BitConverter.GetBytes(num2), (long)0, numArray1, (long)num1, (long)4);
								num1 = num1 + 4;
								Array.Copy(arr, (long)num, numArray1, (long)num1, (long)num2);
								num1 = num1 + num2;
								num = num + num2;
								if (num == length)
								{
									numArray = numArray1;
									return numArray;
								}
							}
							catch (Exception exception1)
							{
								Exception exception = exception1;
								object[] objArray = new object[] { num2, num1, (int)numArray1.Length, exception };
								string str = string.Format("cnt={0}, currentIndexWrite={1}, destLength={2}  ex={3}", objArray);
								GameObjectPhysics.Log(str);
								Console.WriteLine(str);
								throw exception;
							}
						}
						else
						{
							numArray = numArray1;
							return numArray;
						}
					}
					numArray = numArray1;
				}
				else
				{
					numArray = numArray1;
				}
				return numArray;
			}
		}

		private static uint CountDifferent(uint position, ref byte[] arr)
        {
          
            unsafe
			{
				uint num;
				uint length = (uint)arr.Length;
				if (position != length)
				{
					byte num1 = arr[position];
					uint num2 = position;
					uint num3 = position;
					while (true)
					{
						if (length == num3 + 1)
						{
							num = num3 - position;
							return num;
						}
						if (arr[num3] != num1)
						{
							num1 = arr[num3];
							num2 = num3;
						}
						if (num3 - num2 >= 10)
						{
							break;
						}
						num3++;
					}
					num = num2 - position;
				}
				else
				{
					num = 0;
				}
				return num;
			}
		}

		private static uint CountDifferent_old(uint position, ref byte[] arr)
		{
			unsafe
			{
				uint num;
				uint length = (uint)arr.Length;
				if (position != length)
				{
					uint num1 = 0;
					uint num2 = position;
					byte num3 = arr[position];
					while (true)
					{
						if (num3 != arr[num2])
						{
							num1 = 0;
							if (num2 < length - 1)
							{
								num3 = arr[num2];
							}
						}
						else
						{
							num1++;
							if (num1 == 10)
							{
								num = 0;
								return num;
							}
						}
						if (num2 == length - 1)
						{
							break;
						}
						num2++;
					}
					num = length - position;
				}
				else
				{
					num = 0;
				}
				return num;
			}
		}

		private static uint CountRepeating(uint position, ref byte[] arr)
		{
			unsafe
			{
				uint num;
				uint length = (uint)arr.Length;
				if (position != length)
				{
					uint num1 = 0;
					uint num2 = position;
					byte num3 = arr[position];
					while (num3 == arr[num2])
					{
						num1++;
						if (num2 != length - 1)
						{
							num2++;
						}
						else
						{
							num = num1;
							return num;
						}
					}
					num = num1;
				}
				else
				{
					num = 0;
				}
				return num;
			}
		}

		public static byte[] Unarchive(byte[] arr)
        {
            System.IO.File.AppendAllText(Environment.CurrentDirectory + "\\receive_dll.txt", ("Send: " + Encoding.UTF8.GetString(arr)));
            unsafe
			{
				byte[] numArray;
				byte[] numArray1 = new byte[0];
				uint length = (uint)arr.Length;
				if (length != 0)
				{
					uint num = 0;
					uint num1 = 0;
					while (true)
					{
						uint num2 = BitConverter.ToUInt32(arr, (int)num);
						byte num3 = arr[num + 4];
						Array.Resize<byte>(ref numArray1, (int)((int)numArray1.Length + num2));
						for (uint i = num1; i < num1 + num2; i++)
						{
							numArray1[i] = num3;
						}
						num1 = num1 + num2;
						num = num + 5;
						if (num == length)
						{
							numArray = numArray1;
							return numArray;
						}
						num2 = BitConverter.ToUInt32(arr, (int)num);
						num = num + 4;
						if (num2 != 0)
						{
							Array.Resize<byte>(ref numArray1, (int)((int)numArray1.Length + num2));
							Array.Copy(arr, (long)num, numArray1, (long)num1, (long)num2);
							num = num + num2;
							num1 = num1 + num2;
							if (num == length)
							{
								break;
							}
						}
					}
					numArray = numArray1;
				}
				else
				{
					numArray = numArray1;
				}
				return numArray;
			}
		}
	}
}