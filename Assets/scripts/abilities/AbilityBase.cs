using UnityEngine;
using System.Collections;

public abstract class AbilityBase {
	public readonly AbilityType abilityType;
	public readonly AbilityTarget abilityTarget;

	protected UnitBase self;

	private int abilityCooldown;
	private int currentCooldown = 0;

	public AbilityBase(AbilityType ab, AbilityTarget at, UnitBase unit, int cooldown) {
		abilityType = ab;
		abilityTarget = at;
		self = unit;
		abilityCooldown = cooldown;
	}

	abstract public void UseAbility(Tile target);

	public void DecreaseCooldown() {
		if (currentCooldown > 0) currentCooldown--;
		else currentCooldown = 0;
	}

	public int GetCurrentCooldown() {
		return currentCooldown;
	}

	public bool IsOnCooldown() {
		return currentCooldown > 0;
	}

	public void PutOnCooldown() {
		currentCooldown = abilityCooldown;
	}
}
