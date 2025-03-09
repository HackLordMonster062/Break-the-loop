using UnityEngine;

public class MainMenu : MonoBehaviour {
	

    public void Play() {
		LevelManager.instance.LoadLevel(0);
	}

	public void SetSoundLevel(float level) {
		AudioManager.instance.SetVolume(level, level);
	}
}
