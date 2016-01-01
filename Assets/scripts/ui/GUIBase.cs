using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

// Unity doesn't stop propogation of clicks/hovering through GUI elements... for some reason
// Which is the literal only reason why this class exists
public class GUIBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public static bool mouseOn = false;
	
	public void OnPointerEnter(PointerEventData ped) {
		mouseOn = true;

		foreach (UnitBase unit in Team.GetAllUnits()) {
			unit.OnMouseExit();
		}

		foreach (Tile tile in BoardGenerator.GetTiles()) {
			tile.OnMouseExit();
		}
	}

	public void OnPointerExit(PointerEventData ped) {
		mouseOn = false;
	}
}
