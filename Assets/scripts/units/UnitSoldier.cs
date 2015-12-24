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
		abilities.Add(new AbilityNuke(this));

		Init();
	}
}
