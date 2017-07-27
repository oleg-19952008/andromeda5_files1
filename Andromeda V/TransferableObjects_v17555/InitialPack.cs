using System;
using System.Collections.Generic;
using System.IO;

public class InitialPack : ITransferable
{
	public short version;

	public string language = "en";

	public bool isNeedNewLauncher = false;

	public SortedList<string, string> translations = new SortedList<string, string>();

	public SortedList<string, string> languages = new SortedList<string, string>();

	public InitialPack()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.isNeedNewLauncher = br.ReadBoolean();
		this.version = br.ReadInt16();
		this.language = br.ReadString();
		int num = br.ReadInt32();
		if (num < 0)
		{
			this.languages = null;
		}
		else
		{
			this.languages = new SortedList<string, string>();
			for (int i = 0; i < num; i++)
			{
				string str = br.ReadString();
				string str1 = br.ReadString();
				this.languages.Add(str, str1);
			}
		}
		InitialPack.DeserializeTranslations(this.translations, br);
	}

	public static void DeserializeTranslations(SortedList<string, string> translations, BinaryReader br)
	{
		int num = br.ReadInt32();
		translations.Clear();
		for (int i = 0; i < num; i++)
		{
			translations.Add(br.ReadString(), br.ReadString());
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.isNeedNewLauncher);
		bw.Write(this.version);
		bw.Write(this.language);
		if (this.languages != null)
		{
			bw.Write(this.languages.Count);
		}
		else
		{
			bw.Write(-1);
		}
		foreach (KeyValuePair<string, string> language in this.languages)
		{
			bw.Write(language.Key);
			bw.Write(language.Value);
		}
		InitialPack.SerializeTranslations(this.translations, bw);
	}

	public static void SerializeTranslations(SortedList<string, string> translations, BinaryWriter bw)
	{
		bw.Write(translations.Count);
		foreach (KeyValuePair<string, string> translation in translations)
		{
			bw.Write(translation.Key);
			bw.Write(translation.Value);
		}
	}
}