using UnityEngine;
using System.Collections;

public abstract class AOEModifierBase : ModifierBase {
	private int radius;

	public AOEModifierBase(UnitBase source, UnitBase target, int duration, int radius) : base (source, target, duration) {
		this.radius = radius;
		isPermanent = true;
	}

	public int GetRadius() {
		return radius;
	}

	public bool IsUnitInRadius(UnitBase unit) {
		// Distance logic here

		return true;
	}

	public override void TurnEnd() {
		base.TurnEnd();

		if (source != target && !IsUnitInRadius(target)) { // Unit is outside the radius,
			Remove();
		}
	}
}
