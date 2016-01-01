using UnityEngine;
using System.Collections;

public class UnitSoldier : UnitBase {
	override public void SetStats() {
		maxActionPoints = 3;
		currentActionPoints = maxActionPoints;
		jumpHeight = 2;
		damage = 20;
		maxHealth = 100;
		currentHealth = maxHealth;

		abilities.Add(new AbilityNuke(this, 0));
		abilities.Add(new AbilityTanky(this, 1));
		abilities.Add(new AbilityHeal(this, 2));
		abilities.Add(new AbilityMoreHealth(this, 3));

		Init();
	}
}
