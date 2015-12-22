using UnityEngine;
using System.Collections;

public abstract class AbilityBase {
	public readonly AbilityType abilityType;
	public readonly AbilityTarget abilityTarget;

	public UnitBase self;

	public AbilityBase(AbilityType ab, AbilityTarget at, UnitBase unit) {
		abilityType = ab;
		abilityTarget = at;
		self = unit;
	}

	abstract public void UseAbility(Tile target);
}
