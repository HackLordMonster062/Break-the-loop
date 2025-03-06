using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    List<Loop> _loops;

    public event Action<int> OnClicksChanged;
    public event Action OnWin;
    public event Action OnLose;

    public int LoopCount { get { return _loops.Count; } }
    public int ClicksLeft { get; private set; }

	private void Start() {
        Setup(LevelManager.instance.GetCurrLevelInfo());
	}

	void Setup(LevelInfo info) {
        ClicksLeft = info.availableClicks;
		OnClicksChanged?.Invoke(ClicksLeft);

		_loops = FindObjectsByType<Loop>(FindObjectsSortMode.None).ToList();

		foreach (Loop loop in _loops) {
            loop.OnBreak += (Loop loop) => {
                _loops.Remove(loop);

                if (_loops.Count <= 0 ) {
                    Win();
                }
            };
        }

        AudioManager.instance.ToggleMusic();
	}

	public void Update() {
        
    }

    void Win() {
        OnWin?.Invoke();
    }

    void Lose() {
        OnLose?.Invoke();
    }

    public void RegisterClick() {
        ClicksLeft--;
        OnClicksChanged?.Invoke(ClicksLeft);

        if (ClicksLeft <= 0) {
            Lose();
        }
    }
}
