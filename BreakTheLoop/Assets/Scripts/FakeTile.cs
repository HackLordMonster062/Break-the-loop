using System;
using UnityEngine;

public class FakeTile : MonoBehaviour {
	SpriteRenderer _renderer;

	public event Action<FakeTile> OnClick;

	void Awake() {
		_renderer = GetComponent<SpriteRenderer>();
	}

	private void OnMouseDown() {
		OnClick?.Invoke(this);


		TurnOff();
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
