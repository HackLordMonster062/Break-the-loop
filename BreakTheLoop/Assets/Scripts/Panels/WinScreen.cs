using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour {
	[SerializeField] ParticleSystem[] confettiSystems;

    public void Restart() {

	}

	public void NextLevel() {

	}

	public void StartConfetti() {
		StartCoroutine(ShootConfetti());
	}

	IEnumerator ShootConfetti() {
		while (true) {
			int index = Random.Range(0, confettiSystems.Length);

			confettiSystems[index].Play();

			yield return new WaitForSeconds(Random.Range(.1f, 2f));
		}
	}
}
 