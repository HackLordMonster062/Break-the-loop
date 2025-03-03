using UnityEngine;

public class FlashColors : MonoBehaviour {
	public static FlashColors instance;

	public Color offColor;
	public Color[] onColors;

	private void Awake() {
		if (instance != null) {
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
		instance = this;
	}
}
