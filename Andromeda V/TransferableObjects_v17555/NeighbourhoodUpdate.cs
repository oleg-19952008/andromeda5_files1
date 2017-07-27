using System;
using System.IO;

public class NeighbourhoodUpdate : ITransferable
{
	public GameObjectPhysics[] toAdd;

	public uint[] toRemove;

	public NeighbourhoodUpdate()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		int i;
		int num = br.ReadInt32();
		if (num == -1)
		{
			this.toAdd = null;
		}
		else
		{
			this.toAdd = new GameObjectPhysics[num];
			for (i = 0; i < num; i++)
			{
				this.toAdd[i] = (GameObjectPhysics)TransferablesFramework.DeserializeITransferable(br);
			}
		}
		num = br.ReadInt32();
		if (num == -1)
		{
			this.toRemove = null;
		}
		else
		{
			this.toRemove = new uint[num];
			for (i = 0; i < num; i++)
			{
				this.toRemove[i] = br.ReadUInt32();
			}
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		int i;
		if (this.toAdd != null)
		{
			bw.Write((int)this.toAdd.Length);
			for (i = 0; i < (int)this.toAdd.Length; i++)
			{
				TransferablesFramework.SerializeITransferable(bw, this.toAdd[i], TransferContext.None);
			}
		}
		else
		{
			bw.Write(-1);
		}
		if (this.toRemove != null)
		{
			bw.Write((int)this.toRemove.Length);
			for (i = 0; i < (int)this.toRemove.Length; i++)
			{
				bw.Write(this.toRemove[i]);
			}
		}
		else
		{
			bw.Write(-1);
		}
	}
}