using System;
using System.IO;

public class JSON_LOGIN_DATA
{
	public string platform = "";

	public string browser = "";

	public string resolution = "";

	public string quality = "";

	public string ip = "";

	public JSON_LOGIN_DATA()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.platform = br.ReadString();
		this.browser = br.ReadString();
		this.resolution = br.ReadString();
		this.quality = br.ReadString();
		this.ip = br.ReadString();
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write(this.platform);
		bw.Write(this.browser);
		bw.Write(this.resolution);
		bw.Write(this.quality);
		bw.Write(this.ip);
	}
}