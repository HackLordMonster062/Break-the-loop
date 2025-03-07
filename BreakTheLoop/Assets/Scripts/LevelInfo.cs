using UnityEngine;

[CreateAssetMenu(fileName = "Level 1", menuName = "Info/Level")]
public class LevelInfo : ScriptableObject {
	public string sceneName;
	public int availableClicks;
}
