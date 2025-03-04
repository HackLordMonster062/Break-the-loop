using UnityEngine;

public class LevelManager : Singleton<LevelManager> {
    [SerializeField] LevelInfo[] levels;

    int _currLevel;

    public LevelInfo LoadLevel(int level) {
        _currLevel = level;

        return GetCurrLevelInfo();
    }

    public LevelInfo LoadNextLevel() {
        return LoadLevel(_currLevel + 1);
    }

    public LevelInfo GetLevelInfo(int level) {
        return levels[level];
    }

    public LevelInfo GetCurrLevelInfo() {
        return levels[_currLevel];
    }
}
