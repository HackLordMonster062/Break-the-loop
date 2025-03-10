using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class GameManager : Singleton<GameManager> {
	[SerializeField] float lightTime;
#if UNITY_EDITOR
    [SerializeField] int level;
#endif

    List<Loop> _loops;

    public event Action<int> OnClicksChanged;
    public event Action<GameState> OnBeforeStateChange;
    public event Action<GameState> OnAfterStateChange;
    public event Action OnCycleStart;
    public event Action OnCycleEnd;

    public int LoopCount { get { return _loops.Count; } }
    public int ClicksLeft { get; private set; }
    public GameState CurrState { get; private set; } = GameState.Default;

	private void Start() {
#if UNITY_EDITOR
        if (level >= 0) LevelManager.instance.LoadLevel(level);
#endif 

        LevelManager.instance.OnLevelLoaded += (LevelInfo info) => StartCoroutine(Setup(info));
	}

	IEnumerator Setup(LevelInfo info) {
        yield return null;

        ChangeState(GameState.Initiating);

        if (info.availableClicks < 0) {
            ChangeState(GameState.Menu);
            yield break;
        }

        ClicksLeft = info.availableClicks;

		_loops = FindObjectsByType<Loop>(FindObjectsSortMode.None).ToList();

		foreach (Loop loop in _loops) {
            loop.OnBreak += (Loop loop) => {
                _loops.Remove(loop);
                Destroy(loop);

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
                StopAllCoroutines();
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
            return;
        }

		OnClicksChanged?.Invoke(ClicksLeft);
	}

    IEnumerator LightLoop() {
        while (CurrState != GameState.Win && CurrState != GameState.Lose) {
            OnCycleStart?.Invoke();

            if (ClicksLeft <= 0 && CurrState == GameState.Playing) {
                Lose();
            }

			yield return WaitForSecondsWithPause(lightTime);

			OnCycleEnd?.Invoke();
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
    Menu,
    Paused
}