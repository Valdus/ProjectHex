using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	public int id;
	public int height = 0;

	private int[] _position = new int[2];
	public int[] position {
		get {
			return _position;
		}
		set {
			_position = value;
		}
	}

	public UnitBase unitOnTile = null;

	private const float hexagonSpacingMultiplierX = 1.724f; // The multiplier for spacing out the hexagons nicely. Found by testing. (X direction)
	private const float hexagonSpacingMultiplierY = 1.49f; // (Y direction)

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	public Vector3 GetWorldPosition() {
		if (position[1] % 2 == 0) {
			return new Vector3(position[0] * hexagonSpacingMultiplierX, height, position[1] * hexagonSpacingMultiplierY);
		} else {
			return new Vector3((position[0]) * hexagonSpacingMultiplierX + (hexagonSpacingMultiplierX / 2), height, position[1] * hexagonSpacingMultiplierY);
		}
	}

	public bool IsEmpty() {
		return unitOnTile == null;
	}
}
