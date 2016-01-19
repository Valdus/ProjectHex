using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.scripts;

public class BoardGenerator : MonoBehaviour {
	public static List<Tile> tiles = new List<Tile>();

	public GameObject baseHexagonPrefab;
	public GameObject tileParent;
	public GameObject soldierPrefab;

	public Material redMaterial;
	public Material greenMaterial;
	public Material greyMaterial;
	public Material tileSelectedMaterial;

	public static BoardGenerator boardGenerator;

	void Start() {
		BoardGenerator.boardGenerator = this;
		List<TileData> tileData = new List<TileData>();
		for (int i = 0; i < 10; i++) {
			for (int j = 0; j < 10; j++) {
				tileData.Add(new TileData(j, 0, i, redMaterial));
			}
		}
		//BuildBoard(10, 10);
		BuildBoard(tileData);

		List<UnitData> units = new List<UnitData>();

		units.Add(new UnitData(2, 0, soldierPrefab));
		units.Add(new UnitData(4, 1, soldierPrefab));
		units.Add(new UnitData(11, 0, soldierPrefab));

		AddUnits(units);

		TurnManager.NextRound();
	}

	void AddTeams(int number) {
		for (int i = 0; i < number; i++) {
			Team.AddTeam();
		}
	}

	void AddUnits(List<UnitData> units) {
		foreach (UnitData unitData in units) {
			UnitBase unit = ((GameObject) Instantiate(unitData.GetUnitPrefab())).GetComponent<UnitBase>();
			unit.SetStats();
			unit.AddUnitToBoard(tiles[unitData.GetTileId()]);

			int teamNumber = unitData.GetTeamNumber();

			if (!Team.DoesTeamExist(teamNumber)) Team.AddTeam(teamNumber);

			Team.GetTeam(teamNumber).AddUnit(unit);
		}
	}

	void BuildBoard(List<TileData> dataTiles) {
		foreach (TileData tileData in dataTiles) {
			GameObject tileGameObject =
				(GameObject) Instantiate(baseHexagonPrefab, new Vector3(0, 0, 0), baseHexagonPrefab.transform.rotation);
			Tile tile = tileGameObject.AddComponent<Tile>();
			tile.Init(tileData.GetX(), tileData.GetY(), tileData.GetZ(), tileData.GetX() % 3 == 0 ? greenMaterial : (tileData.GetX() % 2 == 0 ? redMaterial : greyMaterial));
			tileGameObject.transform.position = tile.GetWorldPosition();
			tileGameObject.transform.parent = tileParent.transform;
			tiles.Add(tile);

			// We should add unit data into TileData.cs and put unit data into a list and then call AddUnits(List<UnitData>) to add units onto the board.

		}
	}

	public static List<Tile> GetTiles() {
		return tiles;
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
