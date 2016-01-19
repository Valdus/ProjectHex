using UnityEngine;
using System.Collections;
using System;

public class ModifierHealth : ModifierBase {
	int health = 100;
	
	public ModifierHealth(UnitBase source, UnitBase target) : base(source, target, -1) {
		isPermanent = true;
		ApplyModification();
	}

	public override void ApplyModification() {
		target.SetMaxHealth(target.GetMaxHealth() + health);
		hasBeenApplied = false;
	}

	protected override void UndoEffects() {
		target.SetMaxHealth(target.GetMaxHealth() - health);
	}
}
