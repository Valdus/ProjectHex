using UnityEngine;
using System.Collections;

public class EndTurnButton : Button {
	public override void OnClick() {
		TurnManager.TeamDone();
	}
}
