using System;
using System.Collections.Generic;

public class ExtractionPoinGuardSkills
{
	public const short AI_REPAIR_MASTER = 101;

	public const short AI_DISABLER = 102;

	public const short AI_SHIELDING = 201;

	public const short AI_ROCKETEER = 202;

	public const short AI_UNSTOPPABLE = 301;

	public const short AI_STORMER = 302;

	public const short AI_REPAIRING_DRONES = 401;

	public const short AI_ULTIMATE_ENFORCER = 402;

	public const short AI_REMEDY = 501;

	public const short AI_POWER_BREAKER = 502;

	public const short AI_SHIELD_FORTRESS = 601;

	public const short AI_ULTIMATE_ROCKETEER = 602;

	public int pointId;

	public byte unitType;

	public SortedList<short, byte> guardianSkills = new SortedList<short, byte>();

	public SortedList<short, byte> wantedGuardianSkills = new SortedList<short, byte>();

	public static SortedList<short, int> skillPrice;

	public static SortedList<short, byte> skillMaxLevels;

	public static SortedList<short, short> skillOpposites;

	public int GetAllGuardianPoints
	{
		get
		{
			KeyValuePair<short, byte> guardianSkill = new KeyValuePair<short, byte>();
			int item = 0;
			foreach (KeyValuePair<short, byte> gr_skill in this.guardianSkills)
			{
				item = item + ExtractionPoinGuardSkills.skillPrice[gr_skill.Key] * gr_skill.Value;
			}
			foreach (KeyValuePair<short, byte> wantedGuardianSkill in this.wantedGuardianSkills)
			{
				item = item + ExtractionPoinGuardSkills.skillPrice[wantedGuardianSkill.Key] * wantedGuardianSkill.Value;
			}
			return item;
		}
	}

	public int GetSavedGuardianPoints
	{
		get
		{
			int item = 0;
			foreach (KeyValuePair<short, byte> guardianSkill in this.guardianSkills)
			{
				item = item + ExtractionPoinGuardSkills.skillPrice[guardianSkill.Key] * guardianSkill.Value;
			}
			return item;
		}
	}

	static ExtractionPoinGuardSkills()
	{
		ExtractionPoinGuardSkills.skillPrice = new SortedList<short, int>()
		{
			{ 101, 1 },
			{ 102, 1 },
			{ 201, 2 },
			{ 202, 2 },
			{ 301, 4 },
			{ 302, 4 },
			{ 401, 8 },
			{ 402, 8 },
			{ 501, 16 },
			{ 502, 16 },
			{ 601, 20 },
			{ 602, 20 }
		};
		ExtractionPoinGuardSkills.skillMaxLevels = new SortedList<short, byte>()
		{
			{ 101, 4 },
			{ 102, 4 },
			{ 201, 4 },
			{ 202, 4 },
			{ 301, 4 },
			{ 302, 4 },
			{ 401, 4 },
			{ 402, 4 },
			{ 501, 1 },
			{ 502, 1 },
			{ 601, 1 },
			{ 602, 1 }
		};
		ExtractionPoinGuardSkills.skillOpposites = new SortedList<short, short>()
		{
			{ 101, 102 },
			{ 102, 101 },
			{ 201, 202 },
			{ 202, 201 },
			{ 301, 302 },
			{ 302, 301 },
			{ 401, 402 },
			{ 402, 401 },
			{ 501, 502 },
			{ 502, 501 },
			{ 601, 602 },
			{ 602, 601 }
		};
	}

	public ExtractionPoinGuardSkills()
	{
	}

	public int GetAllPoints(short skillType)
	{
		byte num = 0;
		byte num1 = 0;
		short num2 = skillType;
		if (num2 > 302)
		{
			switch (num2)
			{
				case 401:
				{
					this.guardianSkills.TryGetValue(401, out num);
					this.wantedGuardianSkills.TryGetValue(401, out num1);
					break;
				}
				case 402:
				{
					this.guardianSkills.TryGetValue(402, out num);
					this.wantedGuardianSkills.TryGetValue(402, out num1);
					break;
				}
				default:
				{
					switch (num2)
					{
						case 501:
						{
							this.guardianSkills.TryGetValue(501, out num);
							this.wantedGuardianSkills.TryGetValue(501, out num1);
							break;
						}
						case 502:
						{
							this.guardianSkills.TryGetValue(502, out num);
							this.wantedGuardianSkills.TryGetValue(502, out num1);
							break;
						}
						default:
						{
							switch (num2)
							{
								case 601:
								{
									this.guardianSkills.TryGetValue(601, out num);
									this.wantedGuardianSkills.TryGetValue(601, out num1);
									break;
								}
								case 602:
								{
									this.guardianSkills.TryGetValue(602, out num);
									this.wantedGuardianSkills.TryGetValue(602, out num1);
									break;
								}
							}
							break;
						}
					}
					break;
				}
			}
		}
		else
		{
			switch (num2)
			{
				case 101:
				{
					this.guardianSkills.TryGetValue(101, out num);
					this.wantedGuardianSkills.TryGetValue(101, out num1);
					break;
				}
				case 102:
				{
					this.guardianSkills.TryGetValue(102, out num);
					this.wantedGuardianSkills.TryGetValue(102, out num1);
					break;
				}
				default:
				{
					switch (num2)
					{
						case 201:
						{
							this.guardianSkills.TryGetValue(201, out num);
							this.wantedGuardianSkills.TryGetValue(201, out num1);
							break;
						}
						case 202:
						{
							this.guardianSkills.TryGetValue(202, out num);
							this.wantedGuardianSkills.TryGetValue(202, out num1);
							break;
						}
						default:
						{
							switch (num2)
							{
								case 301:
								{
									this.guardianSkills.TryGetValue(301, out num);
									this.wantedGuardianSkills.TryGetValue(301, out num1);
									break;
								}
								case 302:
								{
									this.guardianSkills.TryGetValue(302, out num);
									this.wantedGuardianSkills.TryGetValue(302, out num1);
									break;
								}
							}
							break;
						}
					}
					break;
				}
			}
		}
		return num + num1;
	}

	public ExtractionPoinGuardSkills.SkillState GetState(short skillType, int freePoints)
	{
		byte num;
		byte num1;
		ExtractionPoinGuardSkills.SkillState skillState;
		byte num2 = 0;
		byte num3 = 0;
		short num4 = skillType;
		if (num4 > 302)
		{
			switch (num4)
			{
				case 401:
				{
					this.guardianSkills.TryGetValue(401, out num2);
					this.wantedGuardianSkills.TryGetValue(401, out num3);
					if (num2 + num3 == ExtractionPoinGuardSkills.skillMaxLevels[401])
					{
						skillState = ExtractionPoinGuardSkills.SkillState.Maxed;
						break;
					}
					else if (num2 + num3 > 0)
					{
						skillState = ExtractionPoinGuardSkills.SkillState.Leveled;
						break;
					}
					else if ((freePoints < ExtractionPoinGuardSkills.skillPrice[401] || this.guardianSkills.ContainsKey(ExtractionPoinGuardSkills.skillOpposites[401]) || this.wantedGuardianSkills.ContainsKey(ExtractionPoinGuardSkills.skillOpposites[401]) ? false : this.GetAllGuardianPoints + ExtractionPoinGuardSkills.skillPrice[skillType] <= 60))
					{
						skillState = ExtractionPoinGuardSkills.SkillState.Available;
						break;
					}
					else
					{
						skillState = ExtractionPoinGuardSkills.SkillState.Unavailable;
						break;
					}
				}
				case 402:
				{
					this.guardianSkills.TryGetValue(402, out num2);
					this.wantedGuardianSkills.TryGetValue(402, out num3);
					if (num2 + num3 == ExtractionPoinGuardSkills.skillMaxLevels[402])
					{
						skillState = ExtractionPoinGuardSkills.SkillState.Maxed;
						break;
					}
					else if (num2 + num3 > 0)
					{
						skillState = ExtractionPoinGuardSkills.SkillState.Leveled;
						break;
					}
					else if ((freePoints < ExtractionPoinGuardSkills.skillPrice[402] || this.guardianSkills.ContainsKey(ExtractionPoinGuardSkills.skillOpposites[402]) || this.wantedGuardianSkills.ContainsKey(ExtractionPoinGuardSkills.skillOpposites[402]) ? false : this.GetAllGuardianPoints + ExtractionPoinGuardSkills.skillPrice[skillType] <= 60))
					{
						skillState = ExtractionPoinGuardSkills.SkillState.Available;
						break;
					}
					else
					{
						skillState = ExtractionPoinGuardSkills.SkillState.Unavailable;
						break;
					}
				}
				default:
				{
					switch (num4)
					{
						case 501:
						{
							this.guardianSkills.TryGetValue(501, out num2);
							this.wantedGuardianSkills.TryGetValue(501, out num3);
							if (num2 + num3 != ExtractionPoinGuardSkills.skillMaxLevels[501])
							{
								num = 0;
								num1 = 0;
								this.guardianSkills.TryGetValue(101, out num);
								this.wantedGuardianSkills.TryGetValue(101, out num1);
								skillState = ((num + num1 != ExtractionPoinGuardSkills.skillMaxLevels[101] ? true : freePoints < ExtractionPoinGuardSkills.skillPrice[501]) ? ExtractionPoinGuardSkills.SkillState.Locked : ExtractionPoinGuardSkills.SkillState.Available);
							}
							else
							{
								skillState = ExtractionPoinGuardSkills.SkillState.Maxed;
							}
							break;
						}
						case 502:
						{
							this.guardianSkills.TryGetValue(502, out num2);
							this.wantedGuardianSkills.TryGetValue(502, out num3);
							if (num2 + num3 != ExtractionPoinGuardSkills.skillMaxLevels[502])
							{
								num = 0;
								num1 = 0;
								this.guardianSkills.TryGetValue(102, out num);
								this.wantedGuardianSkills.TryGetValue(102, out num1);
								skillState = ((num + num1 != ExtractionPoinGuardSkills.skillMaxLevels[102] ? true : freePoints < ExtractionPoinGuardSkills.skillPrice[502]) ? ExtractionPoinGuardSkills.SkillState.Locked : ExtractionPoinGuardSkills.SkillState.Available);
							}
							else
							{
								skillState = ExtractionPoinGuardSkills.SkillState.Maxed;
							}
							break;
						}
						default:
						{
							switch (num4)
							{
								case 601:
								{
									this.guardianSkills.TryGetValue(601, out num2);
									this.wantedGuardianSkills.TryGetValue(601, out num3);
									if (num2 + num3 != ExtractionPoinGuardSkills.skillMaxLevels[601])
									{
										num = 0;
										num1 = 0;
										this.guardianSkills.TryGetValue(201, out num);
										this.wantedGuardianSkills.TryGetValue(201, out num1);
										skillState = ((num + num1 != ExtractionPoinGuardSkills.skillMaxLevels[201] ? true : freePoints < ExtractionPoinGuardSkills.skillPrice[601]) ? ExtractionPoinGuardSkills.SkillState.Locked : ExtractionPoinGuardSkills.SkillState.Available);
									}
									else
									{
										skillState = ExtractionPoinGuardSkills.SkillState.Maxed;
									}
									break;
								}
								case 602:
								{
									this.guardianSkills.TryGetValue(602, out num2);
									this.wantedGuardianSkills.TryGetValue(602, out num3);
									if (num2 + num3 != ExtractionPoinGuardSkills.skillMaxLevels[602])
									{
										num = 0;
										num1 = 0;
										this.guardianSkills.TryGetValue(202, out num);
										this.wantedGuardianSkills.TryGetValue(202, out num1);
										skillState = ((num + num1 != ExtractionPoinGuardSkills.skillMaxLevels[202] ? true : freePoints < ExtractionPoinGuardSkills.skillPrice[602]) ? ExtractionPoinGuardSkills.SkillState.Locked : ExtractionPoinGuardSkills.SkillState.Available);
									}
									else
									{
										skillState = ExtractionPoinGuardSkills.SkillState.Maxed;
									}
									break;
								}
								default:
								{
									skillState = ExtractionPoinGuardSkills.SkillState.Unavailable;
									return skillState;
								}
							}
							break;
						}
					}
					break;
				}
			}
		}
		else
		{
			switch (num4)
			{
				case 101:
				{
					this.guardianSkills.TryGetValue(101, out num2);
					this.wantedGuardianSkills.TryGetValue(101, out num3);
					if (num2 + num3 == ExtractionPoinGuardSkills.skillMaxLevels[101])
					{
						skillState = ExtractionPoinGuardSkills.SkillState.Maxed;
						break;
					}
					else if (num2 + num3 > 0)
					{
						skillState = ExtractionPoinGuardSkills.SkillState.Leveled;
						break;
					}
					else if ((freePoints < ExtractionPoinGuardSkills.skillPrice[101] || this.guardianSkills.ContainsKey(ExtractionPoinGuardSkills.skillOpposites[101]) || this.wantedGuardianSkills.ContainsKey(ExtractionPoinGuardSkills.skillOpposites[101]) ? false : this.GetAllGuardianPoints + ExtractionPoinGuardSkills.skillPrice[skillType] <= 60))
					{
						skillState = ExtractionPoinGuardSkills.SkillState.Available;
						break;
					}
					else
					{
						skillState = ExtractionPoinGuardSkills.SkillState.Unavailable;
						break;
					}
				}
				case 102:
				{
					this.guardianSkills.TryGetValue(102, out num2);
					this.wantedGuardianSkills.TryGetValue(102, out num3);
					if (num2 + num3 == ExtractionPoinGuardSkills.skillMaxLevels[102])
					{
						skillState = ExtractionPoinGuardSkills.SkillState.Maxed;
						break;
					}
					else if (num2 + num3 > 0)
					{
						skillState = ExtractionPoinGuardSkills.SkillState.Leveled;
						break;
					}
					else if ((freePoints < ExtractionPoinGuardSkills.skillPrice[102] || this.guardianSkills.ContainsKey(ExtractionPoinGuardSkills.skillOpposites[102]) || this.wantedGuardianSkills.ContainsKey(ExtractionPoinGuardSkills.skillOpposites[102]) ? false : this.GetAllGuardianPoints + ExtractionPoinGuardSkills.skillPrice[skillType] <= 60))
					{
						skillState = ExtractionPoinGuardSkills.SkillState.Available;
						break;
					}
					else
					{
						skillState = ExtractionPoinGuardSkills.SkillState.Unavailable;
						break;
					}
				}
				default:
				{
					switch (num4)
					{
						case 201:
						{
							this.guardianSkills.TryGetValue(201, out num2);
							this.wantedGuardianSkills.TryGetValue(201, out num3);
							if (num2 + num3 == ExtractionPoinGuardSkills.skillMaxLevels[201])
							{
								skillState = ExtractionPoinGuardSkills.SkillState.Maxed;
							}
							else if (num2 + num3 <= 0)
							{
								skillState = ((freePoints < ExtractionPoinGuardSkills.skillPrice[201] || this.guardianSkills.ContainsKey(ExtractionPoinGuardSkills.skillOpposites[201]) || this.wantedGuardianSkills.ContainsKey(ExtractionPoinGuardSkills.skillOpposites[201]) ? false : this.GetAllGuardianPoints + ExtractionPoinGuardSkills.skillPrice[skillType] <= 60) ? ExtractionPoinGuardSkills.SkillState.Available : ExtractionPoinGuardSkills.SkillState.Unavailable);
							}
							else
							{
								skillState = ExtractionPoinGuardSkills.SkillState.Leveled;
							}
							break;
						}
						case 202:
						{
							this.guardianSkills.TryGetValue(202, out num2);
							this.wantedGuardianSkills.TryGetValue(202, out num3);
							if (num2 + num3 == ExtractionPoinGuardSkills.skillMaxLevels[202])
							{
								skillState = ExtractionPoinGuardSkills.SkillState.Maxed;
							}
							else if (num2 + num3 <= 0)
							{
								skillState = ((freePoints < ExtractionPoinGuardSkills.skillPrice[202] || this.guardianSkills.ContainsKey(ExtractionPoinGuardSkills.skillOpposites[202]) || this.wantedGuardianSkills.ContainsKey(ExtractionPoinGuardSkills.skillOpposites[202]) ? false : this.GetAllGuardianPoints + ExtractionPoinGuardSkills.skillPrice[skillType] <= 60) ? ExtractionPoinGuardSkills.SkillState.Available : ExtractionPoinGuardSkills.SkillState.Unavailable);
							}
							else
							{
								skillState = ExtractionPoinGuardSkills.SkillState.Leveled;
							}
							break;
						}
						default:
						{
							switch (num4)
							{
								case 301:
								{
									this.guardianSkills.TryGetValue(301, out num2);
									this.wantedGuardianSkills.TryGetValue(301, out num3);
									if (num2 + num3 == ExtractionPoinGuardSkills.skillMaxLevels[301])
									{
										skillState = ExtractionPoinGuardSkills.SkillState.Maxed;
									}
									else if (num2 + num3 <= 0)
									{
										skillState = ((freePoints < ExtractionPoinGuardSkills.skillPrice[301] || this.guardianSkills.ContainsKey(ExtractionPoinGuardSkills.skillOpposites[301]) || this.wantedGuardianSkills.ContainsKey(ExtractionPoinGuardSkills.skillOpposites[301]) ? false : this.GetAllGuardianPoints + ExtractionPoinGuardSkills.skillPrice[skillType] <= 60) ? ExtractionPoinGuardSkills.SkillState.Available : ExtractionPoinGuardSkills.SkillState.Unavailable);
									}
									else
									{
										skillState = ExtractionPoinGuardSkills.SkillState.Leveled;
									}
									break;
								}
								case 302:
								{
									this.guardianSkills.TryGetValue(302, out num2);
									this.wantedGuardianSkills.TryGetValue(302, out num3);
									if (num2 + num3 == ExtractionPoinGuardSkills.skillMaxLevels[302])
									{
										skillState = ExtractionPoinGuardSkills.SkillState.Maxed;
									}
									else if (num2 + num3 <= 0)
									{
										skillState = ((freePoints < ExtractionPoinGuardSkills.skillPrice[302] || this.guardianSkills.ContainsKey(ExtractionPoinGuardSkills.skillOpposites[302]) || this.wantedGuardianSkills.ContainsKey(ExtractionPoinGuardSkills.skillOpposites[302]) ? false : this.GetAllGuardianPoints + ExtractionPoinGuardSkills.skillPrice[skillType] <= 60) ? ExtractionPoinGuardSkills.SkillState.Available : ExtractionPoinGuardSkills.SkillState.Unavailable);
									}
									else
									{
										skillState = ExtractionPoinGuardSkills.SkillState.Leveled;
									}
									break;
								}
								default:
								{
									skillState = ExtractionPoinGuardSkills.SkillState.Unavailable;
									return skillState;
								}
							}
							break;
						}
					}
					break;
				}
			}
		}
		return skillState;
	}

	public enum SkillState
	{
		Unavailable,
		Available,
		Leveled,
		Maxed,
		Locked
	}
}