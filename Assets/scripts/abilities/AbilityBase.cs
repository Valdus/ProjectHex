using UnityEngine;
using System.Collections;

public abstract class AbilityBase {
	public readonly AbilityType abilityType;
	public readonly AbilityTarget abilityTarget;

	public readonly int actionPointCost;
	public readonly int abilityPosition; // 0th, 1st, 2nd, 3rd, etc. position on the bar

	public readonly Sprite abilityIcon;

	public AbilityButton button;

	protected UnitBase self;

	private int abilityCooldown;
	private int currentCooldown = 0;

	public AbilityBase(AbilityType ab, AbilityTarget at, UnitBase unit, int cooldown, int cost, string icon) {
		abilityType = ab;
		abilityTarget = at;
		self = unit;
		abilityCooldown = cooldown;
		actionPointCost = cost;
		abilityIcon = Resources.Load<Sprite>(icon);
	}

	abstract public void UseAbility(Tile target);
	abstract public void UseAbility();

	public void DecreaseCooldown() {
		SetCooldown(currentCooldown - 1);
	}

	public int GetCurrentCooldown() {
		return currentCooldown;
	}

	public bool IsOnCooldown() {
		return currentCooldown > 0;
	}

	public void PutOnCooldown() {
		SetCooldown(abilityCooldown);
	}

	private void SetCooldown(int c) {
		currentCooldown = c;
		
		if (currentCooldown < 0) {
			currentCooldown = 0;
		}

		if (button != null) {
			button.SetCooldown(currentCooldown);
		}
	}
}
