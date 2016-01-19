using UnityEngine;
using UnityEngine.UI;
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

	private TextMesh textMesh;

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

	public void Init(int x,int y, int z, Material material) {
		textMesh = GetComponentInChildren<TextMesh>();
		_x = x;
	    _z = z;
	    _y = y;
		this.SetMaterial(material);
		Debug.Log(textMesh == null);
		textMesh.text = x + ", " + z;
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

	// http://stackoverflow.com/questions/14491444/calculating-distance-on-a-hexagon-grid
	public static int GetDistance(Tile t1, Tile t2) {
		int x1 = t1.GetXPosition(), x2 = t2.GetXPosition();
		int z1 = t1.GetZPosition(), z2 = t2.GetZPosition();
		int distance = (int) Mathf.Max(
			Mathf.Abs(z2 - z1),
			Mathf.Abs(Mathf.Floor(z2 / -2) + x2 - Mathf.Floor(z1 / -2) - x1),
			Mathf.Abs(-z2 - Mathf.Floor(z2 / -2) - x2 + z1 + Mathf.Floor(z1 / -2) + x1));

		return distance;
	}

	private void SetMaterial(Material material) {
		if (renderer == null) renderer = GetComponent<Renderer>();

		if (material != null) {
			renderer.material = material;

			if (originalMaterial == null) originalMaterial = material;
		}
	}

	void OnMouseOver() {
		if (GUIBase.mouseOn || SelectionManager.currentTile == this) return;

		UnitBase unit = SelectionManager.currentUnitSelected;

		if (SelectionManager.isMoving && unit.GetTeam().IsTurn() && unit.CheckEnoughActionPoints(unit.GetActionPointCostToMoveTo(this))) {
			isHoveredOver = true;
			SetMaterial(BoardGenerator.boardGenerator.tileSelectedMaterial);
			SelectionManager.currentTile = this;
		}
	}

	public void OnMouseExit() {
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
