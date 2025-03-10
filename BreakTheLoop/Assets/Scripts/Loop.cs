using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : MonoBehaviour {
    [SerializeField] List<Tile> tiles;
    [SerializeField] int startLevel;
    [SerializeField] int startIndex;

    public event Action<Loop> OnBreak;

    int _currTile;
    int _currLevel;

	private void Start() {
		GameManager.instance.OnAfterStateChange += Setup;

		_currTile = startIndex;
		_currLevel = startLevel;

		for (int i = 0; i < tiles.Count; i++) {
			tiles[i].OnClick += HandleClick;
		}

		GameManager.instance.OnCycleStart += Cycle;
	}

	void Setup(GameState state) {
		if (state != GameState.Initiating) return;

		tiles[_currTile].TurnOn(_currLevel);
	}

	private void OnDestroy() {
		GameManager.instance.OnCycleStart -= Cycle;
		GameManager.instance.OnAfterStateChange -= Setup;
	}

	void HandleClick(Tile tile) {
        if (_currLevel > 0 && tile == tiles[_currTile]) {
            _currLevel--;
			AudioManager.instance.PlaySound("Laser");

			if (_currLevel > 0) _currTile = (_currTile + tiles.Count - 1) % tiles.Count;
		}
    }

	void Cycle() {
        if (_currLevel <= 0) {
			tiles[_currTile].TurnOff();
			AudioManager.instance.PlaySound("Laser");

			OnBreak?.Invoke(this);
            return;
		}

		tiles[_currTile].TurnOff();

		_currTile = (_currTile + 1) % tiles.Count;

		tiles[_currTile].TurnOn(_currLevel);
	}
}
