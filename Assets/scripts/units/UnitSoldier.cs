using UnityEngine;
using System.Collections;

public class UnitSoldier : UnitBase {
	void Start() {
		movementDistance = 3;
		jumpHeight = 2;
		damage = 20;
		maxHealth = 100;
		currentHealth = maxHealth;
		abilities.Add(new AbilityNuke(this));

		Init();
	}
}
