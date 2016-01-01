using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TurnManager {
	public static Team currentTeamMoving;

	public static int turnNumber = 0;

	private static void StartRound() {
		foreach (Team team in Team.GetAllTeams()) {
			team.totalActionPoints = 0;

			foreach (UnitBase unit in team.GetUnits()) {
				unit.RoundStart();
			}
		}

		Debug.Log("Turn " + turnNumber);
	}

	private static void EndTurn() {
		foreach (UnitBase unit in currentTeamMoving.GetUnits()) {
			unit.TurnEnd();
		}
	}

	public static void NextRound() {
		turnNumber++;
		currentTeamMoving = Team.GetAllTeams()[0];
		StartRound();
	}

	public static void TeamDone() {
		Team nextTeam = GetNextTeam();

		if (nextTeam == null) {
			Debug.Log("Next round!");
			EndTurn();
			NextRound();
		} else {
			EndTurn();
			Debug.Log("Current team: " + Team.GetAllTeams().IndexOf(currentTeamMoving));
			Debug.Log("Next team: " + Team.GetAllTeams().IndexOf(nextTeam));
			currentTeamMoving = nextTeam;
		}
	}

	private static Team GetNextTeam() {
		try {
			return Team.GetAllTeams()[Team.GetAllTeams().IndexOf(currentTeamMoving) + 1];
		} catch (Exception e) {
			return null;
		}
	}
}
