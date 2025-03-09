using System;
using System.Collections;
using UnityEngine;

public class Loop : MonoBehaviour {
    [SerializeField] Tile[] tiles;
    [SerializeField] float lightTime;
    [SerializeField] int startLevel;
    [SerializeField] int startIndex;

    public event Action<Loop> OnBreak;

    int _currTile;
    int _currLevel;
	
    void Start() {
        _currTile = startIndex;
        _currLevel = startLevel;

        for (int i = 0; i < tiles.Length; i++) {
			tiles[i].TurnOff();
			tiles[i].OnClick += HandleClick;
        }

		tiles[_currTile].TurnOn(_currLevel);

		GameManager.instance.OnCycleEnd += Cycle;
    }

	private void OnDestroy() {
		GameManager.instance.OnCycleEnd -= Cycle;
	}

	void HandleClick(Tile tile) {
        if (_currLevel > 0 && tile == tiles[_currTile]) {
            _currLevel--;
			AudioManager.instance.PlaySound("Laser");
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

		_currTile = (_currTile + 1) % tiles.Length;

		tiles[_currTile].TurnOn(_currLevel);
	}
}
