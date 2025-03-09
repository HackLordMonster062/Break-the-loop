using UnityEngine;

public class LoseScreen : MonoBehaviour {
	

    public void Restart() {
		LevelManager.instance.ReloadLevel();
	}
}
 