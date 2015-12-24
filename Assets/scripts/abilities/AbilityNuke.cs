using UnityEngine;
using System.Collections;

public class AbilityNuke : AbilityBase {
	const int damage = 10;
	
	public AbilityNuke(UnitBase unit) : base(AbilityType.active, AbilityTarget.enemy, unit, 10, 2) {

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
}
