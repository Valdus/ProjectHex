using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeamActionPointsBar : MonoBehaviour {
	private Text text;

	void Start() {
		text = GetComponentInChildren<Text>();
		GUIManager.SetTeamActionPointsBar(this);
	}

	void OnGUI() {
		if (TurnManager.currentTeamMoving != null) text.text = "Team Action Points: " + TurnManager.currentTeamMoving.totalActionPoints;
		else text.text = "Team Action Points: 0";
	}
}
