using System;
using System.IO;

public class MadeSynthesisParams : ITransferable
{
	public MakeSynthesisResult result;

	public PlayerItems items;

	public MadeSynthesisParams()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		this.result = (MakeSynthesisResult)br.ReadInt32();
		this.items = new PlayerItems();
		this.items.Deserialize(br);
	}

	public void Serialize(BinaryWriter bw)
	{
		bw.Write((int)this.result);
		this.items.Serialize(bw);
	}
}