using System;
using System.Collections.Generic;

public class BonusesConstant
{
	public static SortedList<AvailableBonuses, float> bonusConstatn;

	static BonusesConstant()
	{
		SortedList<AvailableBonuses, float> availableBonuses = new SortedList<AvailableBonuses, float>()
		{
			{ AvailableBonuses.shield, 7f },
			{ AvailableBonuses.shieldPercent, 0.13f },
			{ AvailableBonuses.corpus, 7f },
			{ AvailableBonuses.corpusPercent, 0.13f },
			{ AvailableBonuses.speed, 0.33f },
			{ AvailableBonuses.targeting, 0.5f },
			{ AvailableBonuses.avoidance, 0.7f },
			{ AvailableBonuses.cargo, 3f },
			{ AvailableBonuses.cargoPercent, 0.33f },
			{ AvailableBonuses.laserCooldown, 0.03f },
			{ AvailableBonuses.plasmaCooldown, 0.045f },
			{ AvailableBonuses.ionCooldown, 0.06f },
			{ AvailableBonuses.laserDmg, 0.1f },
			{ AvailableBonuses.plasmaDmg, 0.2f },
			{ AvailableBonuses.ionDmg, 0.3f },
			{ AvailableBonuses.miningSpeed, 0.4f },
			{ AvailableBonuses.shieldRegen, 0.5f }
		};
		BonusesConstant.bonusConstatn = availableBonuses;
	}

	public BonusesConstant()
	{
	}
}