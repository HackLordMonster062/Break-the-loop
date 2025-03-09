using UnityEngine;

public class UIManager : Singleton<UIManager> {
    [SerializeField] HUDPanel hudPanel;
    [SerializeField] LoseScreen losePanel;
    [SerializeField] WinScreen winPanel;
	[SerializeField] Transform canvasPrefab;

	HUDPanel _hudPanel;
	LoseScreen _losePanel;
	WinScreen _winPanel;
    Transform _canvas;

    void Start() {
        GameManager.instance.OnBeforeStateChange += HandleGameStateChange;
    }

	private void OnDestroy() {
		GameManager.instance.OnClicksChanged -= ChangeClicksAmount;
		GameManager.instance.OnBeforeStateChange -= HandleGameStateChange;
	}

	void InitiateUI() {
		_canvas = FindAnyObjectByType<Canvas>()?.transform;

		if (_canvas == null ) _canvas = Instantiate(canvasPrefab);

		_hudPanel = Instantiate(hudPanel, _canvas);

		_losePanel = Instantiate(losePanel, _canvas);
		_losePanel.gameObject.SetActive(false);

		_winPanel = Instantiate(winPanel, _canvas);
		_winPanel.gameObject.SetActive(false);

		GameManager.instance.OnClicksChanged += ChangeClicksAmount;
	}

    void ChangeClicksAmount(int amount) {
        _hudPanel.UpdateClicks(amount);
    }

	void HandleGameStateChange(GameState state) {
		if (state == GameState.Win) OpenWinScreen();
		if (state == GameState.Lose) OpenLoseScreen();
		if (state == GameState.Connecting) InitiateUI();
	}

    void OpenWinScreen() {
		_winPanel.gameObject.SetActive(true);
	}

    void OpenLoseScreen() {
		_losePanel.gameObject.SetActive(true);
	}
}
