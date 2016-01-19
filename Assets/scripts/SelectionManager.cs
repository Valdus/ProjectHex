using UnityEngine;
using System.Collections;

public class SelectionManager : MonoBehaviour {
	public static UnitBase currentUnitHoveredOver = null;
	public static UnitBase currentUnitSelected = null;

	public static Tile currentTile = null;

	public static AbilityBase currentAbilityTargeting = null;

	public static bool isMoving = false;
	
	// Update is called once per frame
	void Update() {
		if (Input.GetMouseButtonDown(0) && ! GUIBase.mouseOn) { // GUIBase.mouseOn is because unity does not have built in stoppage of click propogation through buttons.... (weird, right?)
			if (currentAbilityTargeting != null && currentUnitHoveredOver != null) {
				UseAbility(currentUnitHoveredOver, currentAbilityTargeting);
			} else {
				if (isMoving) {
					DoMovement();
				} else {
					if (currentUnitHoveredOver != null) {
						SelectUnit(currentUnitHoveredOver);
					}
				}
			}
		}

	    if (Input.GetKeyDown(KeyCode.Escape))
	    {
	        EscapePressed();
	    } 

		// Abilites
		if (Input.GetKeyDown(KeyCode.Q) && currentUnitSelected != null) {
			TargetAbilitiy(currentUnitSelected.GetAbility(0));
		}

		if (Input.GetKeyDown(KeyCode.W) && currentUnitSelected != null) {
			TargetAbilitiy(currentUnitSelected.GetAbility(1));
		}
	}

	public static void TargetAbilitiy(AbilityBase ability) {
		if (currentUnitSelected != null && currentUnitSelected.GetTeam().IsTurn() && currentUnitSelected.CheckEnoughActionPoints(ability.actionPointCost)) {
			if (ability.abilityTarget == AbilityTarget.none) {
				ability.UseAbility();
			} else {
				currentAbilityTargeting = ability;
			}
		}
	}

	private void UseAbility(UnitBase target, AbilityBase abiltiy) {
		abiltiy.UseAbility(currentUnitHoveredOver.tileOn);
		currentAbilityTargeting = null;
	}

	private void SelectUnit(UnitBase unit) {
		if (unit == null) return;
		if (unit == currentUnitSelected) return;
	    if(currentUnitSelected != null)
            currentUnitSelected.Deselect();
		currentUnitSelected = unit;
		currentUnitSelected.Select();
		GUIManager.SelectUnit(currentUnitSelected);
		SetIsMoving(true);
	}

    private void EscapePressed()
    {
        if (currentUnitSelected == null)
        {
            //Menu code here
        }
		if (currentUnitSelected != null) {
			currentUnitSelected.Deselect();
			SetIsMoving(false);
			GUIManager.SelectUnit(null);
		}
        
    }

    private void SetIsMoving(bool m)
    {
		isMoving = m;
    }

    private void DoMovement()
    {
        if (currentUnitSelected.CanMoveTo(currentTile))
        {
            currentUnitSelected.MoveTo(currentTile);
            SetIsMoving(false);
            currentTile.DeHoverOver();
        }
        else if (currentUnitHoveredOver != null)
        {
            SelectUnit(currentUnitHoveredOver);
            SetIsMoving(true);
        }
    }

}
