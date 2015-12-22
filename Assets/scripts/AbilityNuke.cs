using UnityEngine;
using System.Collections;

public class AbilityNuke : AbilityBase {
	const int damage = 100;
	
	public AbilityNuke(UnitBase unit) : base(AbilityType.active, AbilityTarget.enemy, unit) {

	}

	public override void UseAbility(Tile target) {
		if (target.IsEmpty()) return;

		UnitBase targetUnit = target.unitOnTile;

		if (self.IsEnemy(targetUnit)) {
			targetUnit.Damage(damage);
		}
	}
}
