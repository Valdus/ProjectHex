using UnityEngine;
using System.Collections;

public class AbilityHeal : AbilityBase {
	const int heal = 10;

	public AbilityHeal(UnitBase unit) : base(AbilityType.active, AbilityTarget.none, unit, 5, 1, "images/abilities/ability_heal") {

	}

	public override void UseAbility() {
		if (IsOnCooldown()) {
			Debug.Log(GetCurrentCooldown());
			return;
		}
		if (!self.CheckEnoughActionPoints(actionPointCost)) {
			Debug.Log("Not enough action points!");
			return;
		}

		self.Heal(heal);
		PutOnCooldown();
		self.UseActionPoints(actionPointCost);
	}

	public override void UseAbility(Tile target) {
		throw new System.NotImplementedException();
	}
}
