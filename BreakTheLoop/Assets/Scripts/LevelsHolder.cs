using UnityEngine;

[CreateAssetMenu(fileName = "Levels Info", menuName = "Info/Holder")]
public class LevelsHolder : ScriptableObject {
	public LevelInfo[] levels; 
}
