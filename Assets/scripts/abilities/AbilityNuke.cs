using UnityEngine;
using System.Collections;
using System;

public class AbilityNuke : AbilityBase {
	const int damage = 10;
	
	public AbilityNuke(UnitBase unit) : base(AbilityType.active, AbilityTarget.enemy, unit, 10, 2, "images/abilities/ability_nuke") {

	}

	public override void UseAbility(Tile target) {
		if (IsOnCooldown()) {
			Debug.Log(GetCurrentCooldown());
			return;
		}
		if (!self.CheckEnoughActionPoints(actionPointCost)) {
			Debug.Log("Not enough action points!");
			return;
		}
		if (target.IsEmpty()) return;

		UnitBase targetUnit = target.unitOnTile;

		if (self.IsEnemy(targetUnit)) {
			targetUnit.Damage(damage);
			PutOnCooldown();
			self.UseActionPoints(actionPointCost);
		}
	}

	public override void UseAbility() {
		throw new NotImplementedException();
	}
}
