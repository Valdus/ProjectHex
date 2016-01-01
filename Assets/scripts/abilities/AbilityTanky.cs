using UnityEngine;
using System.Collections;

public class AbilityTanky : AbilityBase {
	public AbilityTanky(UnitBase unit, int abilityPosition) : base(AbilityType.active, AbilityTarget.none, unit, 7, 1, "images/abilities/ability_tanky", abilityPosition) {

	}

	public override void UseAbility() {
		if (IsOnCooldown()) return;

		new ModifierTanky(self, self);

		StartCooldown();
		self.UseActionPoints(actionPointCost);
	}

	public override void UseAbility(Tile target) {
		throw new System.NotImplementedException();
	}
}
