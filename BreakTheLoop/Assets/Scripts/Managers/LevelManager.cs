using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager> {
    [SerializeField] LevelsHolder holder;

    public event Action<LevelInfo> OnLevelLoaded;

	LevelInfo[] _levels;

	int _currLevel;

	protected override void Awake() {
        base.Awake();
        _levels = holder.levels;
	}

	public LevelInfo LoadLevel(int level) {
        _currLevel = level;

        SceneManager.LoadSceneAsync(_levels[_currLevel].sceneName);
        SceneManager.sceneLoaded += NotifyLevelLoad;

        return GetCurrLevelInfo();
    }

    private void NotifyLevelLoad(Scene scene, LoadSceneMode mode) {
        OnLevelLoaded?.Invoke(_levels[_currLevel]);

        SceneManager.sceneLoaded -= NotifyLevelLoad;
    }

    public LevelInfo LoadNextLevel() {
        return LoadLevel(_currLevel + 1);
    }

    public LevelInfo ReloadLevel() {
        return LoadLevel(_currLevel);
    }

    public LevelInfo GetLevelInfo(int level) {
        return _levels[level];
    }

    public LevelInfo GetCurrLevelInfo() {
        return _levels[_currLevel];
    }
}
