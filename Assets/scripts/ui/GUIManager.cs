using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour {
	private static List<AbilityButton> abilityButtons = new List<AbilityButton>();
	private static HealthBar healthBar;
	private static ActionPointsBar actionPointsBar;
	private static TeamActionPointsBar teamActionPointsBar;

	public static void AddAbilityButton(AbilityButton ab) {
		abilityButtons.Add(ab);
	}

	public static void SelectUnit(UnitBase unit) {
		if (unit != null) {
			SetAbilityButtonIcons(unit.GetAbilities());
		} else {
			SetAbilityButtonIcons(null);
		}
	}

	private static void SetAbilityButtonIcons(List<AbilityBase> abilities) {
		if (abilities != null) {
			foreach (AbilityBase ability in abilities) {
				// If there are not enough given abilities, then it will set the rest to a blank button.
				try {
					abilityButtons.Find(x => x.position == ability.abilityPosition).SetAbility(ability);
				} catch (System.Exception e) {
					Debug.LogError("Invalid ability position for " + ability.GetType().ToString() + " with position " + ability.abilityPosition);
				}
			}

			foreach (AbilityButton ab in abilityButtons) {
				if (!ab.hasChanged) ab.SetAbility(null);

				ab.hasChanged = false;
			}
		} else {
			foreach (AbilityButton ab in abilityButtons) {	
				ab.SetAbility(null);
			}
		}
	}

	public static void SetHealthBar(HealthBar hb) {
		healthBar = hb;
	}

	public static void SetActionPointsBar(ActionPointsBar apb) {
		actionPointsBar = apb;
	}

	public static void SetTeamActionPointsBar(TeamActionPointsBar tapb) {
		teamActionPointsBar = tapb;
	}
}
