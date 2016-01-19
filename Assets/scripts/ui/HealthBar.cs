using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {
	private Text text;

	void Start() {
		text = GetComponentInChildren<Text>();
		GUIManager.SetHealthBar(this);
	}

	void OnGUI() {
		if (SelectionManager.currentUnitSelected != null) text.text = "Health: " + SelectionManager.currentUnitSelected.currentHealth;
		else text.text = "Health: 0";
	}

}
