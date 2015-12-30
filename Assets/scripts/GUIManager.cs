using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour {
	private static List<AbilityButton> abilityButtons = new List<AbilityButton>();
	
	public static void AddAbilityButton(AbilityButton ab) {
		abilityButtons.Add(ab);
	}

	public static void SetAbilityButtonIcons(List<AbilityBase> abilities) {
		for (int i = 0; i < abilityButtons.Count; i++) {
			// If there are not enough given abilities, then it will set the rest to a blank button.
			try {
				Debug.Log(abilities[i]);
				abilityButtons[i].SetAbiltiy(abilities[i]);
			} catch (System.ArgumentOutOfRangeException e) {
				Debug.Log(abilities[i]);
				abilityButtons[i].SetAbiltiy(null);
			}
		}
	}
}
