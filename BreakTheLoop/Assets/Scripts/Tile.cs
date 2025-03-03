using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour {
	[SerializeField] bool isClickable;

	public event Action<int> OnClick;

	SpriteRenderer _renderer;

	int _index;
	
    void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
    }

	private void OnMouseDown() {
		if (!isClickable) return;

		OnClick?.Invoke(_index);
	}

	public void SetIndex(int index) {
		_index = index;
	}

	public void TurnOn(int level) {
		_renderer.color = FlashColors.instance.onColors[level - 1];
    }

    public void TurnOff() {
		_renderer.color = FlashColors.instance.offColor;
    }
}
