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
	protected List<ModifierBase> modifiers = new List<ModifierBase>();

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

	public void RoundStart() {
		currentActionPoints = maxActionPoints;
		team.totalActionPoints += maxActionPoints;
	}

	public void TurnEnd() {
		foreach (AbilityBase ability in abilities) {
			ability.DecreaseCooldown();
		}

		List<ModifierBase> tempModifiers = new List<ModifierBase>(modifiers); // Modifiers may be removed during the loop

		foreach (ModifierBase modifier in tempModifiers) {
			modifier.TurnEnd();
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
		if (GUIBase.mouseOn) return;

		SetHaloEnabled(true);
		isHoveredOver = true;
		SelectionManager.currentUnitHoveredOver = this;
	}

	public void OnMouseExit() {
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
		modifiers.ForEach(x => x.Damage(ref damage));

		currentHealth -= damage;
		Debug.Log("Damaged, now at " + currentHealth);
	}

	public void Heal(int health) {
		modifiers.ForEach(x => x.Heal(ref health));

		currentHealth += health;
		Debug.Log("Healed, now at " + currentHealth);
	}

	public void SetMaxHealth(int health) {
		if (health > 0) {
			modifiers.ForEach(x => x.SetMaxHealth(ref health));

			int old = maxHealth;
			maxHealth = health;
			ScaleHealth(old);
		}
	}

	public int GetMaxHealth() {
		return maxHealth;
	}

	private void ScaleHealth(int oldMaxHealth) {
		float p = currentHealth / oldMaxHealth;
		currentHealth = (int) Mathf.Floor(maxHealth * p);
	}

	public AbilityBase GetAbility(int ability) {
		if (abilities[ability] != null) return abilities[ability];
		else return null;
	}

	public GameObject GetModelPrefab() {
		return modelPrefab;
	}

	public void AddModifier(ModifierBase modifier) {
		if (modifier != null) {
			modifiers.Add(modifier);
		}
	}

	private void RemoveModifier(ModifierBase modifier) {
		modifier.Remove();
	}

	public List<ModifierBase> GetModifiers() {
		return modifiers;
	}

	public void CheckAOEModifiers() {
		foreach (ModifierBase modifier in modifiers) {
			// Check if the unit is in the radius of the AOE modifiers they don't own (usually after moving).
			if (modifier.GetType().IsAssignableFrom(typeof(AOEModifierBase)) && !((AOEModifierBase) modifier).IsUnitInRadius(this)) {
				RemoveModifier(modifier);
			}
		}
	}
}
