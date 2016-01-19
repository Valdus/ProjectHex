using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActionPointsBar : MonoBehaviour {
	private Text text;

	void Start() {
		text = GetComponentInChildren<Text>();
		GUIManager.SetActionPointsBar(this);
	}

	void OnGUI() {
		if (SelectionManager.currentUnitSelected != null) text.text = "Action Points: " + SelectionManager.currentUnitSelected.GetCurrentActionPoints();
		else text.text = "Action Points: 0";
	}
}
