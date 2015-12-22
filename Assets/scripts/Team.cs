using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Team {
	private List<UnitBase> units = new List<UnitBase>();

	private readonly int teamNumber;

	public static List<Team> teams = new List<Team>();

	public Team(int teamNumber) {
		this.teamNumber = teamNumber;
	}

	public void AddUnit(UnitBase unit) {
		if (unit == null) return;

		unit.SetTeam(teamNumber);
		units.Add(unit);
	}

	public static Team GetTeam(int teamNumber) {
		return teams[teamNumber];
	}
}
