using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	public int Id;

    private int _y;
    private int _x;
    private int _z;

    public bool isHoveredOver = false;

	public UnitBase unitOnTile = null;

	private Material originalMaterial;

	private Renderer renderer;

	private const float hexagonSpacingMultiplierX = 1.724f; // The multiplier for spacing out the hexagons nicely. Found by testing. (X direction)
	private const float hexagonSpacingMultiplierY = 1.49f; // (Y direction)

    protected int GetXPosition()
    {
        return _x;
    }

    protected int GetZPosition()
    {
        return _z;
    }

    protected int GetYPosition()
    {
        return _y;
    }
	// Use this for initialization
	void Start() {
	}

	// Update is called once per frame
	void Update() {

	}

	public void Init(int x,int y, int z, Material material)
	{
	    _x = x;
	    _z = z;
	    _y = y;
		this.SetMaterial(material);
	}

	public Vector3 GetWorldPosition() {
		if (GetZPosition() % 2 == 0) { // Offset every other row
			return new Vector3(GetXPosition() * hexagonSpacingMultiplierX, GetYPosition(), GetZPosition() * hexagonSpacingMultiplierY);
		} else {
			return new Vector3((GetXPosition()) * hexagonSpacingMultiplierX + (hexagonSpacingMultiplierX / 2), GetYPosition(), GetZPosition() * hexagonSpacingMultiplierY);
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
			SelectionManager.currentTile = this;
		}
	}

	void OnMouseExit() {
		isHoveredOver = false;
		SetMaterial(originalMaterial);
		SelectionManager.currentTile = null;
	}

	// To reset hover after moving has ended.
	public void DeHoverOver() {
		isHoveredOver = false;
		SetMaterial(originalMaterial);
		SelectionManager.currentTile = null;
	}
}
