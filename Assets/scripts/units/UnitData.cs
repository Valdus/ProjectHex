using UnityEngine;
using System;
using System.Collections;

namespace Assets.scripts {
	public class UnitData {
		readonly int _tileId;
		readonly int _teamNumber;
		GameObject _unitPrefab;

		public UnitData(int tileId, int teamNumber, GameObject prefab) {
			_tileId = tileId;
			_teamNumber = teamNumber;
			_unitPrefab = prefab;
		}

		public int GetTeamNumber() {
			return _teamNumber;
		}

		public int GetTileId() {
			return _tileId;
		}

		public GameObject GetUnitPrefab() {
			return _unitPrefab;
		}
	}
}