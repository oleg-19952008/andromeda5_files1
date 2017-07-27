using System;
using System.IO;

public class FusilladeData : ITransferable
{
	public ProjectileObject[] projectiles;

	public ushort[] cooldownTimes;

	public FusilladeData()
	{
	}

	public void Deserialize(BinaryReader br)
	{
		int i;
		int num = br.ReadInt32();
		if (num != -1)
		{
			this.projectiles = new ProjectileObject[num];
			for (i = 0; i < num; i++)
			{
				this.projectiles[i] = (ProjectileObject)TransferablesFramework.DeserializeITransferable(br);
			}
		}
		else
		{
			this.projectiles = null;
		}
		num = br.ReadInt32();
		if (num != -1)
		{
			this.cooldownTimes = new ushort[num];
			for (i = 0; i < num; i++)
			{
				this.cooldownTimes[i] = br.ReadUInt16();
			}
		}
		else
		{
			this.cooldownTimes = null;
		}
	}

	public void Serialize(BinaryWriter bw)
	{
		if (this.projectiles != null)
		{
			bw.Write((int)this.projectiles.Length);
			ProjectileObject[] projectileObjectArray = this.projectiles;
			for (int i = 0; i < (int)projectileObjectArray.Length; i++)
			{
				TransferablesFramework.SerializeITransferable(bw, projectileObjectArray[i], TransferContext.None);
			}
		}
		else
		{
			bw.Write(-1);
		}
		if (this.cooldownTimes != null)
		{
			bw.Write((int)this.cooldownTimes.Length);
			for (int j = 0; j < (int)this.cooldownTimes.Length; j++)
			{
				bw.Write(this.cooldownTimes[j]);
			}
		}
		else
		{
			bw.Write(-1);
		}
	}
}