using System.Collections;
using UnityEngine;

public class Loop : MonoBehaviour {
    [SerializeField] Tile[] tiles;
    [SerializeField] float lightTime;
    [SerializeField] int startLevel;

    int _currTile;
    int _currLevel;
	
    void Start() {
        _currTile = 0;
        _currLevel = startLevel;

        for (int i = 0; i < tiles.Length; i++) {
            tiles[i].SetIndex(i);
            tiles[i].TurnOff();

            tiles[i].OnClick += HandleClick;
        }

        StartCoroutine(LightLoop());
    }

    void HandleClick(int index) {
        if (_currLevel > 0 && index == _currTile) {
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

        // Notify on loop break
	}
}
