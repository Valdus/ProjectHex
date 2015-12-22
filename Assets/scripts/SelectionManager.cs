using UnityEngine;
using System.Collections;

public class SelectionManager : MonoBehaviour {
	public static UnitBase currentUnitHoveredOver = null;
	public static UnitBase currentUnitSelected = null;

	public static Tile currentTileHoveredOver = null;

	public static bool isMoving = false;


	// Use this for initialization
	void Start() {

	}
	
	// Update is called once per frame
	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			if (isMoving) {
				if (currentTileHoveredOver != null) {
					if (currentUnitSelected.CanMoveTo(currentTileHoveredOver)) {
						currentUnitSelected.MoveTo(currentTileHoveredOver);
						isMoving = false;
						currentTileHoveredOver.DeHoverOver();
					}
				} else if (currentUnitHoveredOver != null) {
					SelectUnit(currentUnitHoveredOver);
					isMoving = false;
				}
			} else {
				if (currentUnitHoveredOver != null) {
					SelectUnit(currentUnitHoveredOver);
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.M)) {
			isMoving = true;
		}
	}

	public static void SelectUnit(UnitBase unit) {
		if (unit == null) return;
		if (unit == currentUnitSelected) return;

		if (currentUnitSelected != null) {
			currentUnitSelected.Deselect();
		}

		currentUnitSelected = unit;
		currentUnitSelected.Select();
	}
}
