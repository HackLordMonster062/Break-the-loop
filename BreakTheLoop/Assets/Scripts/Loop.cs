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

        StartCoroutine(LightLoop());
    }

    void HandleClick(Tile tile) {
        if (_currLevel > 0 && tile == tiles[_currTile]) {
            _currLevel--;
        }
    }

    IEnumerator LightLoop() {
        while (_currLevel > 0) {
            tiles[_currTile].TurnOn(_currLevel);

            yield return new WaitForSeconds(lightTime);

			tiles[_currTile].TurnOff();

            _currTile = (_currTile + 1) % tiles.Length;
		}

		tiles[_currTile].TurnOff();

        OnBreak?.Invoke(this);
	}
}
