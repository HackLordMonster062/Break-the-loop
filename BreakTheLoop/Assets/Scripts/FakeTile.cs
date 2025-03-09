using System;
using UnityEngine;

public class FakeTile : MonoBehaviour {
	SpriteRenderer _renderer;

	void Awake() {
		_renderer = GetComponent<SpriteRenderer>();
	}

	public void TurnOn(int level) {
		SetColor(FlashColors.instance.onColors[level - 1]);
	}

	public void TurnOff() {
		SetColor(FlashColors.instance.offColor);
	}

	private void SetColor(Color color) {
		_renderer.color = color;
	}
}
