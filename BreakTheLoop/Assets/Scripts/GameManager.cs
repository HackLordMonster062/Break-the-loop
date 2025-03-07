using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class GameManager : Singleton<GameManager> {
	[SerializeField] float lightTime;

	List<Loop> _loops;

    public event Action<int> OnClicksChanged;
    public event Action<GameState> OnBeforeStateChange;
    public event Action<GameState> OnAfterStateChange;
    public event Action OnCycle;

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

				if (_loops.Count <= 0) {
					Win();
					return;
				}
			};
        }

        ChangeState(GameState.Connecting);
        ChangeState(GameState.Playing);
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
                StartCoroutine(LightLoop());
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

		if (ClicksLeft < 0) {
            Lose();
            return;
        }

		OnClicksChanged?.Invoke(ClicksLeft);
	}

    IEnumerator LightLoop() {
        while (CurrState != GameState.Win || CurrState != GameState.Lose) {
            OnCycle?.Invoke();

			yield return WaitForSecondsWithPause(lightTime);
		}
	}

	IEnumerator WaitForSecondsWithPause(float time) {
		float startTime = Time.time;
		float remainingWaitTime = time;

		while (remainingWaitTime > 0) {
			if (CurrState == GameState.Playing) {
				remainingWaitTime = time - (Time.time - startTime);
			}
			yield return null;
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