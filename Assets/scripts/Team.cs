using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Team {
	private List<UnitBase> units = new List<UnitBase>();
	private static List<UnitBase> allUnits = new List<UnitBase>();

	private readonly int teamNumber;

	private static List<Team> teams = new List<Team>();


	private int _totalActionPoints = 0;
	public int totalActionPoints {
		get {
			return _totalActionPoints;
		} set {
			_totalActionPoints = value;

			if (_totalActionPoints < 0) {
				_totalActionPoints = 0;
			}
		}
	}

	public Team(int teamNumber) {
		this.teamNumber = teamNumber;
	}

	public void AddUnit(UnitBase unit) {
		if (unit == null) return;

		unit.SetTeam(this);
		units.Add(unit);
		allUnits.Add(unit);
	}

	public int GetTeamNumber() {
		return teamNumber;
	}

	public bool IsTurn() {
		return TurnManager.currentTeamMoving == this;
	}

	public List<UnitBase> GetUnits() {
		return units;
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
		return GetTeam(teamNumber) != null;
	}

	public static List<Team> GetAllTeams() {
		return teams;
	}

	public static List<UnitBase> GetAllUnits() {
		return allUnits;
	}
}
