using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    List<Loop> _loops;

    public event Action<int> OnClicksChanged;
    public event Action<GameState> OnBeforeStateChange;
    public event Action<GameState> OnAfterStateChange;

    public int LoopCount { get { return _loops.Count; } }
    public int ClicksLeft { get; private set; }
    public GameState CurrState { get; private set; } = GameState.Default;

	private void Start() {
        ChangeState(GameState.Initiating);
	}

	void Setup(LevelInfo info) {
        print("Setting up");

        ClicksLeft = info.availableClicks;

		_loops = FindObjectsByType<Loop>(FindObjectsSortMode.None).ToList();

		foreach (Loop loop in _loops) {
            loop.OnBreak += (Loop loop) => {
                _loops.Remove(loop);

                if (_loops.Count <= 0 ) {
                    Win();
                }
            };
        }

        ChangeState(GameState.Connecting);
	}

	public void ChangeState(GameState state) {
        CurrState = state;
        OnBeforeStateChange?.Invoke(CurrState);

        switch (state) {
            case GameState.Initiating:
				LevelManager.instance.LoadLevel(0);
				LevelManager.instance.OnLevelLoaded += Setup;
				break;
            case GameState.Connecting:
				OnClicksChanged?.Invoke(ClicksLeft);
				break;
            case GameState.Playing:
				AudioManager.instance.ToggleMusic();
				break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
            default:
                break;
        }

        OnAfterStateChange?.Invoke(CurrState);
    }

    void Win() {
        ChangeState(GameState.Win);
    }

    void Lose() {
        ChangeState(GameState.Lose);
    }

    public void RegisterClick() {
        ClicksLeft--;
        OnClicksChanged?.Invoke(ClicksLeft);

        if (ClicksLeft <= 0) {
            Lose();
        }
    }
}

public enum GameState {
    Default,
    Initiating,
    Connecting,
    Playing,
    Lose,
    Win,
    Paused
}