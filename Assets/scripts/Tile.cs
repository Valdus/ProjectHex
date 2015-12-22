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

	public bool isHoveredOver = false;

	public UnitBase unitOnTile = null;

	private Material originalMaterial;

	private Renderer renderer;

	private const float hexagonSpacingMultiplierX = 1.724f; // The multiplier for spacing out the hexagons nicely. Found by testing. (X direction)
	private const float hexagonSpacingMultiplierY = 1.49f; // (Y direction)

	// Use this for initialization
	void Start() {
	}

	// Update is called once per frame
	void Update() {

	}

	public void Init(int[] position, int height, Material material) {
		this.position = position;
		this.height = height;
		this.SetMaterial(material);
	}

	public Vector3 GetWorldPosition() {
		if (position[1] % 2 == 0) { // Offset every other row
			return new Vector3(position[0] * hexagonSpacingMultiplierX, height, position[1] * hexagonSpacingMultiplierY);
		} else {
			return new Vector3((position[0]) * hexagonSpacingMultiplierX + (hexagonSpacingMultiplierX / 2), height, position[1] * hexagonSpacingMultiplierY);
		}
	}

	public bool IsEmpty() {
		return unitOnTile == null;
	}

	private void SetMaterial(Material material) {
		if (renderer == null) renderer = GetComponent<Renderer>();

		if (material != null) {
			renderer.material = material;

			if (originalMaterial == null) originalMaterial = material;
		}
	}

	void OnMouseEnter() {
		if (SelectionManager.isMoving) {
			isHoveredOver = true;
			SetMaterial(BoardGenerator.tileSelectedMaterial);
			SelectionManager.currentTileHoveredOver = this;
		}
	}

	void OnMouseExit() {
		isHoveredOver = false;
		SetMaterial(originalMaterial);
		SelectionManager.currentTileHoveredOver = null;
	}

	// To reset hover after moving has ended.
	public void DeHoverOver() {
		isHoveredOver = false;
		SetMaterial(originalMaterial);
		SelectionManager.currentTileHoveredOver = null;
	}
}
