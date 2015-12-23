using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Team {
	private List<UnitBase> units = new List<UnitBase>();

	private readonly int teamNumber;

	private static List<Team> teams = new List<Team>();

	public Team(int teamNumber) {
		this.teamNumber = teamNumber;
	}

	public void AddUnit(UnitBase unit) {
		if (unit == null) return;

		unit.SetTeam(this);
		units.Add(unit);
	}

	public int GetTeamNumber() {
		return teamNumber;
	}

	public static Team GetTeam(int teamNumber) {
		return teams.Find(x => x.teamNumber == teamNumber);
	}

	public static void AddTeam() {
		AddTeam(teams[teams.Count - 1].teamNumber + 1);
	}

	public static void AddTeam(int teamNumber) {
		teams.Add(new Team(teamNumber));
	}

	public static bool DoesTeamExist(int teamNumber) {
		Team result = teams.Find(x => x.teamNumber == teamNumber);
		return result != null;
	}
}
