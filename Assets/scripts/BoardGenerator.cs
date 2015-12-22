using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts;

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

	void Start() {
		BoardGenerator.tileSelectedMaterial = _tileSelectedMaterial;
	    List<TileData> tileData = new List<TileData>();
	    for (int i = 0; i < 10; i++)
	    {
	        for (int j = 0; j < 10; j++)
	        {
	            tileData.Add(new TileData(j,0,i,redMaterial));
	        }
	    }
		//BuildBoard(10, 10);
        BuildBoard(tileData);

		Team.teams.Add(new Team(0));
		Team.teams.Add(new Team(1));

		UnitSoldier soldier = ((GameObject) Instantiate(soldierPrefab)).GetComponent<UnitSoldier>();
		soldier.MoveTo(tiles[2]);
		Team.GetTeam(0).AddUnit(soldier);

		soldier = ((GameObject) Instantiate(soldierPrefab)).GetComponent<UnitSoldier>();
		soldier.MoveTo(tiles[4]);
		Team.GetTeam(1).AddUnit(soldier);


		soldier = ((GameObject)Instantiate(soldierPrefab)).GetComponent<UnitSoldier>();
		soldier.MoveTo(tiles[11]);
		Team.GetTeam(0).AddUnit(soldier);
	}

    void BuildBoard(List<TileData> dataTiles )
    {
        foreach (TileData tileData in dataTiles)
        {
            GameObject tileGameObject =
                (GameObject) Instantiate(baseHexagonPrefab, new Vector3(0, 0, 0), baseHexagonPrefab.transform.rotation);
            Tile tile = tileGameObject.AddComponent<Tile>();
            tile.Init(tileData.GetX(),tileData.GetY(),tileData.GetZ(),redMaterial);
            tileGameObject.transform.position = tile.GetWorldPosition();
            tileGameObject.transform.parent = tileParent.transform;
            tiles.Add(tile);

        }
    }
    /*
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
    */
}
