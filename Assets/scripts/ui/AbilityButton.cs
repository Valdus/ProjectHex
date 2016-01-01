using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AbilityButton : ButtonBase {
	private Image abilityImage;

	private Text cooldown;

	private GameObject cooldownOverlay;

	private AbilityBase ability;

	public bool hasChanged = false;

	public int position;

	void Start() {
		cooldown = GetComponentInChildren<Text>();

		foreach (Transform child in transform) {
			if (child.CompareTag("AbilityImage")) abilityImage = child.GetComponent<Image>();
			else if (child.CompareTag("CooldownOverlay")) cooldownOverlay = child.gameObject;
		}

		SetCooldown(0);

		GUIManager.AddAbilityButton(this);
	}

	public void SetAbility(AbilityBase a) {
		if (a != null) {
			if (ability != null) ability.button = null;
			ability = a;
			ability.button = this;
			abilityImage.sprite = ability.abilityIcon;
			SetCooldown(ability.GetCurrentCooldown());
			hasChanged = true;
		} else {
			if (ability != null) ability.button = null;
			ability = null;
			SetCooldown(0);
			hasChanged = true;
		}
	}

	public void SetCooldown(int c) {
		if (c > 0) {
			cooldownOverlay.SetActive(true);
			cooldown.text = c.ToString();
		} else {
			cooldownOverlay.SetActive(false);
			cooldown.text = "";
		}
	}

	public override void OnClick() {
		if (ability != null) {
			SelectionManager.TargetAbilitiy(ability);
		}
	}
}
