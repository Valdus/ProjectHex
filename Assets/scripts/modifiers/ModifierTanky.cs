using UnityEngine;
using System.Collections;

public class ModifierTanky : ModifierBase {
	float damageReduction = .25f;

	public ModifierTanky(UnitBase source, UnitBase target) : base(source, target, 2) {

	}

	public override void Damage(ref int damage) {
		damage -= (int) Mathf.Floor(damage * damageReduction);
		Debug.Log("Modified damage: " + damage);
	}

	public override void ApplyModification() {
	}

	protected override void UndoEffects() {
	}
}
