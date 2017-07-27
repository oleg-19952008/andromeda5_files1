using System;

public class PveSpawnZoneState
{
	public PvESpawnZone zone;

	public short currentCount;

	public PveSpawnZoneState(PvESpawnZone z)
	{
		this.zone = z;
		this.currentCount = 0;
	}
}