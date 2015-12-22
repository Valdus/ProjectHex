using UnityEngine;
using System.Collections;

public class UnitBase : MonoBehaviour {
	public int movementDistance;
	public int jumpHeight;
	public int damage;
	public int maxHealth;
	public int currentHealth;

	public Tile tileOn;

	public void MoveTo(Tile tile) {
		if (tile.IsEmpty()) {
			if (CanMoveTo(tile)) {
				if (tileOn != null) {
					tileOn.unitOnTile = null;
				}

				tileOn = tile;
				tile.unitOnTile = this;
				transform.parent = tile.transform;
				transform.position = tile.transform.position + new Vector3(0, transform.lossyScale.y);
			}
		}
	}

	public bool CanMoveTo(Tile tile) {
		// Some move logic here

		return true;
	}
}
