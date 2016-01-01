using UnityEngine;
using System.Collections;

public class AbilityMoreHealth : AbilityBase {
	public AbilityMoreHealth(UnitBase unit, int abilityPosition) : base(AbilityType.passive, AbilityTarget.none, unit, -1, 0, "images/abilities/ability_more_health", abilityPosition) {
		unit.AddModifier(new ModifierHealth(unit, unit));
	}

	public override void UseAbility() {
		throw new System.NotImplementedException();
	}

	public override void UseAbility(Tile target) {
		throw new System.NotImplementedException();
	}
}
