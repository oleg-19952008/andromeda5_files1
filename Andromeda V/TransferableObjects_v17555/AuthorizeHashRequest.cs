using System;
using System.IO;

public class AuthorizeHashRequest : ITransferable
{
	public string player_id;

	public string timestamp;

	public string hash;

	public string language;

	public int worldIndex;

	public byte[] warehouseData;

	private static byte[] dullKey;

	static AuthorizeHashRequest()
	{
		AuthorizeHashRequest.dullKey = new byte[] { 17, 0, 204, 48, 56, 11, 129, 99, 255, 204, 17, 0, 204, 48, 56, 111, 129, 99, 255, 204 };
	}

	public AuthorizeHashRequest()
	{
	}

	public static void ApplyXOR(byte[] src, byte[] mask)
	{
		for (int i = 0; i < (int)src.Length; i++)
		{
			int length = i % (int)mask.Length;
			src[i] = (byte)(src[i] ^ mask[length]);
		}
	}

	public static void ApplyXOR(byte[] src)
	{
		byte[] numArray = AuthorizeHashRequest.dullKey;
		for (int i = 0; i < (int)src.Length; i++)
		{
			int length = i % (int)numArray.Length;
			src[i] = (byte)(src[i] ^ numArray[length]);
		}
	}

	public void Deserialize(BinaryReader br2)
	{
		byte[] numArray = br2.ReadBytes(br2.ReadInt32());
		AuthorizeHashRequest.ApplyXOR(numArray, AuthorizeHashRequest.dullKey);
		BinaryReader binaryReader = new BinaryReader(new MemoryStream(numArray));
		this.player_id = AuthorizeHashRequest.SafeReadString(binaryReader);
		this.timestamp = AuthorizeHashRequest.SafeReadString(binaryReader);
		this.hash = AuthorizeHashRequest.SafeReadString(binaryReader);
		this.language = AuthorizeHashRequest.SafeReadString(binaryReader);
		this.warehouseData = binaryReader.ReadBytes(binaryReader.ReadInt16());
		this.worldIndex = binaryReader.ReadInt32();
	}

	public static byte[] GenerateKey()
	{
		Random random = new Random(666);
		return new byte[random.Next(20, 70)];
	}

	public static string SafeReadString(BinaryReader br)
	{
		string str;
		short num = br.ReadInt16();
		if (num != -1)
		{
			if (num > 2000)
			{
				throw new Exception("Deserializing too long string!");
			}
			char[] chrArray = new char[num];
			for (int i = 0; i < num; i++)
			{
				chrArray[i] = br.ReadChar();
			}
			str = new string(chrArray);
		}
		else
		{
			str = null;
		}
		return str;
	}

	public static void SafeWriteString(string s, BinaryWriter bw)
	{
		if (s != null)
		{
			char[] charArray = s.ToCharArray();
			bw.Write((short)((int)charArray.Length));
			char[] chrArray = charArray;
			for (int i = 0; i < (int)chrArray.Length; i++)
			{
				bw.Write(chrArray[i]);
			}
		}
		else
		{
			bw.Write((short)-1);
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		MemoryStream memoryStream = new MemoryStream();
		BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
		AuthorizeHashRequest.SafeWriteString(this.player_id, binaryWriter);
		AuthorizeHashRequest.SafeWriteString(this.timestamp, binaryWriter);
		AuthorizeHashRequest.SafeWriteString(this.hash, binaryWriter);
		AuthorizeHashRequest.SafeWriteString(this.language, binaryWriter);
		binaryWriter.Write((short)((int)this.warehouseData.Length));
		binaryWriter.Write(this.warehouseData);
		binaryWriter.Write(this.worldIndex);
		byte[] array = memoryStream.ToArray();
		memoryStream.Close();
		AuthorizeHashRequest.ApplyXOR(array, AuthorizeHashRequest.dullKey);
		bw.Write((int)array.Length);
		bw.Write(array, 0, (int)array.Length);
	}
}