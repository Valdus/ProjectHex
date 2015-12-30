using UnityEngine;
using System.Collections;

public class EndTurnButton : ButtonBase {
	public override void OnClick() {
		TurnManager.TeamDone();
	}
}
