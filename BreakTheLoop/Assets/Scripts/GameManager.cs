using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    [SerializeField] int startClicks;

    List<Loop> _loops;

    public int LoopCount { get { return _loops.Count; } }
    public int ClicksLeft { get; private set; }

	private void Start() {
        Setup();

		ClicksLeft = startClicks;
	}

	void Setup() {
        _loops = FindObjectsByType<Loop>(FindObjectsSortMode.None).ToList();

		foreach (Loop loop in _loops) {
            loop.OnBreak += (Loop loop) => {
                _loops.Remove(loop);

                if (_loops.Count <= 0 ) {
                    Win();
                }
            };
        }
	}

	public void Update() {
        
    }

    void Win() {

    }

    void Lose() {

    }

    public void RegisterClick() {
        ClicksLeft--;

        if (ClicksLeft <= 0) {
            Lose();
        }
    }
}
