using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour {
	[SerializeField] bool isClickable;

	public event Action<Tile> OnClick;

	public bool IsEnabled { get; private set; }

	SpriteRenderer _renderer;
	
    void Awake() {
		IsEnabled = isClickable;

		_renderer = GetComponent<SpriteRenderer>();
    }

	private void Start() {
		GameManager.instance.OnCycleEnd += Cycle;
		GameManager.instance.OnBeforeStateChange += Setup;
	}

	private void OnDestroy() {
		GameManager.instance.OnCycleEnd -= Cycle;
		GameManager.instance.OnBeforeStateChange -= Setup;
	}

	void Cycle() {
		IsEnabled = isClickable;
	}

	void Setup(GameState state) {
		if (state == GameState.Initiating) {
			TurnOff();
		}
	}

	private void OnMouseDown() {
		if (!IsEnabled || GameManager.instance.CurrState != GameState.Playing) return;

		OnClick?.Invoke(this);

		GameManager.instance.RegisterClick();

		IsEnabled = false;
	}

	public void TurnOn(int level) {
		SetColor(FlashColors.instance.onColors[level - 1]);
    }

    public void TurnOff() {
		SetColor(FlashColors.instance.offColor);
    }

	private void SetColor(Color color) {
		if (!IsEnabled) color *= FlashColors.instance.disabledColor;

		_renderer.color = color;
	}
}
