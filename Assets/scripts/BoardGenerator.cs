using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardGenerator : MonoBehaviour {
	public List<Tile> tiles = new List<Tile>();

	public GameObject baseHexagonPrefab;
	public GameObject tileParent;
	public GameObject soldierPrefab;

	public Material redMaterial;
	public Material greenMaterial;
	public Material greyMaterial;
	public Material _tileSelectedMaterial;
	public static Material tileSelectedMaterial;

	// Use this for initialization
	void Start() {
		BoardGenerator.tileSelectedMaterial = _tileSelectedMaterial;
		BuildBoard(10, 10);
		UnitSoldier soldier = ((GameObject) Instantiate(soldierPrefab)).GetComponent<UnitSoldier>();
		soldier.MoveTo(tiles[2]);
		soldier = ((GameObject) Instantiate(soldierPrefab)).GetComponent<UnitSoldier>();
		soldier.MoveTo(tiles[4]);
	}

	// Update is called once per frame
	void Update() {

	}

	void BuildBoard(int width, int height) {
		int h = 0;

		for (int y = 0; y <= width; y++) {
			for (int x = 0; x <= height; x++) {
				GameObject tileGameObject = (GameObject) Instantiate(baseHexagonPrefab, new Vector3(0, 0, 0), baseHexagonPrefab.transform.rotation);
				Tile tile = tileGameObject.AddComponent<Tile>();

				tile.Init(new int[] { x, y }, h, x % 3 == 0 ? greenMaterial : (x % 2 == 0 ? redMaterial : greyMaterial));
				tileGameObject.transform.position = tile.GetWorldPosition();
				tileGameObject.transform.parent = tileParent.transform;
				tiles.Add(tile);
			}

			h++;
		}
	}
}
