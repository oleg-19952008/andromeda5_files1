using System;

public delegate void TakeDamageDelegate(GameObjectPhysics p, GameObjectPhysics g, GameObjectPhysics proj, int dmgHull, int dmgShield, bool isHitting, bool stunTarget = false, int stunDduration = 0, bool isCritickalStrike = false);