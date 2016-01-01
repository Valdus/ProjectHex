using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

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
