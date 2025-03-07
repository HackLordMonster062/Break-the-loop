using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager> {
    [SerializeField] LevelInfo[] levels;

    public event Action<LevelInfo> OnLevelLoaded;

    int _currLevel;

    public LevelInfo LoadLevel(int level) {
        _currLevel = level;

        SceneManager.LoadSceneAsync(levels[_currLevel].sceneName);
        SceneManager.sceneLoaded += NotifyLevelLoad;

        return GetCurrLevelInfo();
    }

    private void NotifyLevelLoad(Scene scene, LoadSceneMode mode) {
        OnLevelLoaded?.Invoke(levels[_currLevel]);

        SceneManager.sceneLoaded -= NotifyLevelLoad;
    }

    public LevelInfo LoadNextLevel() {
        return LoadLevel(_currLevel + 1);
    }

    public LevelInfo ReloadLevel() {
        return LoadLevel(_currLevel);
    }

    public LevelInfo GetLevelInfo(int level) {
        return levels[level];
    }

    public LevelInfo GetCurrLevelInfo() {
        return levels[_currLevel];
    }
}
