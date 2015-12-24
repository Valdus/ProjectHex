using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TurnManager {
	public static Team currentTeamMoving;

	public static int turnNumber = 0;

	public static void StartTurn() {
		foreach (Team team in Team.GetAllTeams()) {
			team.totalActionPoints = 0;

			foreach (UnitBase unit in team.GetUnits()) {
				unit.TurnStart();
			}
		}

		Debug.Log("Turn " + turnNumber);
	}

	public static void NextTurn() {
		turnNumber++;
		currentTeamMoving = Team.GetAllTeams()[0];
		StartTurn();
	}

	public static void TeamDone() {
		Team nextTeam = GetNextTeam();

		if (nextTeam == null) {
			Debug.Log("Next turn!");
			NextTurn();
		} else {
			Debug.Log("Current team: " + Team.GetAllTeams().IndexOf(currentTeamMoving));
			Debug.Log("Next team: " + Team.GetAllTeams().IndexOf(nextTeam));
			currentTeamMoving = nextTeam;
		}
	}

	public static Team GetNextTeam() {
		try {
			return Team.GetAllTeams()[Team.GetAllTeams().IndexOf(currentTeamMoving) + 1];
		} catch (Exception e) {
			return null;
		}
	}
}
