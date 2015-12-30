using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class UnitBase : MonoBehaviour {
	protected int maxActionPoints;
	protected int currentActionPoints;
	protected int jumpHeight;
	protected int damage;
	protected int maxHealth;

	protected Team team;

	private int _currentHealth;
	public int currentHealth {
		get {
			return _currentHealth;
		} set {
			_currentHealth = value;

			if (_currentHealth > maxHealth) {
				_currentHealth = maxHealth;
			} else if (_currentHealth < 0) {
				_currentHealth = 0;	
			}
		}
	}

	private float unitHeightOffset = 1.566f; // For placing the unit at a correct height on top of a tile.

	public bool isSelected = false;
	public bool isHoveredOver = false;

	public Tile tileOn;

	private Component halo;

	protected static GameObject modelPrefab;
	protected List<AbilityBase> abilities = new List<AbilityBase>();
	protected AbilityBase abilityTargeting = null;

	void Start() {
	}

	void Update() {
	}

	abstract public void SetStats();

	protected void Init() {
		try {
			halo = transform.FindChild("halo").GetComponent("Halo");
			SetHaloEnabled(false);
		} catch (Exception e) {
			Debug.LogError("GameObject 'halo' not found in unit. Try adding empty 'halo' with Halo componenet to it");
		}
	}

	public void MoveTo(Tile tile) {
		if (CheckEnoughActionPoints(GetActionPointCostToMoveTo(tile))) { // Hard coded for now, add movement distance later.
			if (tile.IsEmpty()) {
				if (CanMoveTo(tile)) {
					if (tileOn != null) {
						tileOn.unitOnTile = null;
					}

					tileOn = tile;
					tile.unitOnTile = this;
					transform.parent = tile.transform;
					transform.position = tile.transform.position + new Vector3(-1.2f, unitHeightOffset, 0);

					UseActionPoints(GetActionPointCostToMoveTo(tile)); // Hard coded for now, add movement distance later.
				}
			}
		}
	}

	public void AddUnitToBoard(Tile tile) {
		if (tile.IsEmpty()) {
			if (CanMoveTo(tile)) {
				if (tileOn != null) {
					tileOn.unitOnTile = null;
				}

				tileOn = tile;
				tile.unitOnTile = this;
				transform.parent = tile.transform;
				transform.position = tile.transform.position + new Vector3(-1.2f, unitHeightOffset, 0);
			}
		}
	}

	public bool CanMoveTo(Tile tile) {
		if (tile == null) return false;
		if (!tile.IsEmpty()) return false;
		if (tile == tileOn) return false; 

		// Some move logic here

		return true;
	}

	public int GetActionPointCostToMoveTo(Tile tile) {
		// Some move logic here

		return 1;
	}

	public void TurnStart() {
		currentActionPoints = maxActionPoints;
		team.totalActionPoints += maxActionPoints;

		foreach (AbilityBase ability in abilities) {
			ability.DecreaseCooldown();
		}
	}

	public void UseActionPoints(int amount) {
		if (!CheckEnoughActionPoints(amount)) {
			Debug.Log("Not enough action points!");
			return;
		}

		currentActionPoints -= amount;
		team.totalActionPoints -= amount;
		Debug.Log("Action points: " + currentActionPoints);
		Debug.Log("Team action points: " + team.totalActionPoints);
	}

	public bool CheckEnoughActionPoints(int amount) {
		return currentActionPoints - amount >= 0;
	}

	void OnMouseEnter() {
		SetHaloEnabled(true);
		isHoveredOver = true;
		SelectionManager.currentUnitHoveredOver = this;
	}

	void OnMouseExit() {
		if (!isSelected) SetHaloEnabled(false);
		isHoveredOver = false;
		SelectionManager.currentUnitHoveredOver = null;
	}

	// For clicking, not hovering.
	public void Select() {
		SetHaloEnabled(true);
		isSelected = true;
		GUIManager.SetAbilityButtonIcons(abilities);
	}

	public void Deselect() {
		SetHaloEnabled(false);
		isSelected = false;
	}

	private void SetHaloEnabled(bool f) {
		if (halo == null) {
			Debug.LogError("No Halo component found in unit");
			return;
		}

		halo.GetType().GetProperty("enabled").SetValue(halo, f, null);
	}

	public void SetTeam(Team t) {
		team = t;
	}

	public Team GetTeam() {
		return team;
	}

	public bool IsEnemy(UnitBase unit) {
		return team.GetTeamNumber() != unit.team.GetTeamNumber();
	}

	public void Damage(int damage) {
		currentHealth -= damage;
		Debug.Log(currentHealth);
	}

	public void Heal(int health) {
		currentHealth += health;
		Debug.Log(currentHealth);
	}

	public AbilityBase GetAbility(int ability) {
		if (abilities[ability] != null) return abilities[ability];
		else return null;
	}

	public GameObject GetModelPrefab() {
		return modelPrefab;
	}
}
