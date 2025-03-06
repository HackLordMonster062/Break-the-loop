using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField] HUDPanel hudPanel;
    [SerializeField] LoseScreen losePanel;
	[SerializeField] Transform canvasPrefab;

	HUDPanel _hudPanel;
	LoseScreen _losePanel;
    Transform _canvas;

    void Start() {
        GameManager.instance.OnClicksChanged += ChangeClicksAmount;
        GameManager.instance.OnWin += OpenWinScreen;
        GameManager.instance.OnLose += OpenLoseScreen;

        _canvas = Instantiate(canvasPrefab);

        _hudPanel = Instantiate(hudPanel, _canvas);
        _losePanel = Instantiate(losePanel, _canvas);
        _losePanel.gameObject.SetActive(false);
    }

    void ChangeClicksAmount(int amount) {
        _hudPanel.UpdateClicks(amount);
    }

    void OpenWinScreen() {

    }

    void OpenLoseScreen() {
		_losePanel.gameObject.SetActive(true);
	}
}
