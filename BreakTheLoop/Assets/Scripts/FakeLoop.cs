using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.GlobalIllumination;

public class FakeLoop : MonoBehaviour {
	[SerializeField] List<FakeTile> tiles;
	[SerializeField] int startLevel;
	[SerializeField] int startIndex;

	int _currTile;

	private void Start() {
		_currTile = startIndex;

		StartCoroutine(LightLoop());
	}

	IEnumerator LightLoop() {
		while (true) {
			tiles[_currTile].TurnOn(startLevel);

			yield return new WaitForSeconds(.6f);

			tiles[_currTile].TurnOff();

			_currTile = (_currTile + 1) % tiles.Count;
		}
	}

	IEnumerator WaitForSecondsWithPause(float time) {
		float startTime = Time.time;
		float remainingWaitTime = time;

		while (remainingWaitTime > 0) {
			remainingWaitTime = time - (Time.time - startTime);
			yield return null;
		}
	}
}
