using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour {
	[SerializeField] bool isClickable;

	public event Action<Tile> OnClick;

	SpriteRenderer _renderer;
	
    void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
    }

	private void Start() {
		GameManager.instance.OnCycle += () => isClickable = true;
	}

	private void OnMouseDown() {
		if (!isClickable || GameManager.instance.CurrState != GameState.Playing) return;

		OnClick?.Invoke(this);

		GameManager.instance.RegisterClick();

		isClickable = false;
	}

	public void TurnOn(int level) {
		_renderer.color = FlashColors.instance.onColors[level - 1];

		if (!isClickable) _renderer.color /= 2;
    }

    public void TurnOff() {
		_renderer.color = FlashColors.instance.offColor;
    }
}
