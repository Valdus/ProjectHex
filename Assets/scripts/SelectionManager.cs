using UnityEngine;
using System.Collections;

public class SelectionManager : MonoBehaviour {
	public static UnitBase currentUnitHoveredOver = null;
	public static UnitBase currentUnitSelected = null;

	public static Tile currentTile = null;

	public static bool isMoving = false;


	// Use this for initialization
	void Start() {

	}
	
	// Update is called once per frame
	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			if (isMoving) {
				DoMovement();
			} else {
				if (currentUnitHoveredOver != null) {
					SelectUnit(currentUnitHoveredOver);
				}
			}
		}

	    if (Input.GetKeyDown(KeyCode.Escape))
	    {
	        EscapePressed();
	    } 
	}

	private void SelectUnit(UnitBase unit) {
		if (unit == null) return;
		if (unit == currentUnitSelected) return;
	    if(currentUnitSelected != null)
            currentUnitSelected.Deselect();
		currentUnitSelected = unit;
		currentUnitSelected.Select();
	    SetIsMoving();
	}

    private void EscapePressed()
    {
        if (currentUnitSelected == null)
        {
            //Menu code here
        }
        if (currentUnitSelected != null)
            currentUnitSelected.Deselect();
        
    }

    private void SetIsMoving()
    {
        isMoving = !isMoving;
    }

    private void DoMovement()
    {
        if (currentUnitSelected.CanMoveTo(currentTile))
        {
            currentUnitSelected.MoveTo(currentTile);
            SetIsMoving();
            currentTile.DeHoverOver();
        }
        else if (currentUnitHoveredOver != null)
        {
            SelectUnit(currentUnitHoveredOver);
            SetIsMoving();
        }
    }

}
