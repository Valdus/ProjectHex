using System;
using System.Collections.Generic;

public abstract class ModifierBase {
	protected int maxDuration;
	protected int currentDuration;
	
	protected UnitBase source; // The unit that gave the modifier to the target (self)
	protected UnitBase target; // The unit that the modifier is on (aka the target unit)

	protected bool isPurgable = false;
	protected bool isPermanent = false;
	protected bool hasBeenApplied = false;

	public ModifierBase(UnitBase source, UnitBase target, int duration) {
		this.source = source;
		this.target = target;
		maxDuration = duration;
		target.AddModifier(this);
		StartDuration();
	}

	public abstract void ApplyModification();
	protected abstract void UndoEffects();

	public virtual void TurnEnd() {
		DecreaseDuration();
	}

	private void StartDuration() {
		if (isPermanent) return;

		SetDuration(maxDuration);
	}

	public int GetDurationLeft() {
		if (isPermanent) return -1;
		else return currentDuration;
	}

	private void DecreaseDuration() {
		if (isPermanent) return;

		SetDuration(currentDuration - 1);
	}

	private void SetDuration(int d) {
		if (isPermanent) return;

		currentDuration = d;

		if (d < 0) {
			d = 0;
			Remove();
		}
	}

	public void Remove() {
		UndoEffects();
		target.GetModifiers().Remove(this);
	}

	public void Purge() {
		if (isPurgable && !isPermanent) Remove();
	}

	// All possible events you can modify

	public virtual void Heal(ref int health) { }
	public virtual void Damage(ref int damage) { }
	public virtual void SetMaxHealth(ref int health) { }
	public virtual void MoveTo(Tile pos) { }
}

