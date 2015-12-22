using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public class UnitBase : MonoBehaviour {
	public int movementDistance;
	public int jumpHeight;
	public int damage;
	public int maxHealth;
	public int currentHealth;
	public int team;

	private float unitHeightOffset = 1.566f; // For placing the unit at a correct height on top of a tile.

	public bool isSelected = false;
	public bool isHoveredOver = false;

	public Tile tileOn;

	private Component halo;

	void Start() {
	}

	protected void Init() {
		try {
			halo = transform.FindChild("halo").GetComponent("Halo");
			SetHaloEnabled(false);
		} catch (Exception e) {
			Debug.LogError("GameObject 'halo' not found in unit. Try adding empty 'halo' with Halo componenet to it");
		}
	}

	public void MoveTo(Tile tile) {
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
		if (!tile.IsEmpty()) return false;
		if (tile == tileOn) return false; 

		// Some move logic here

		return true;
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

	public void SetTeam(int t) {
		team = t;
	}
}
