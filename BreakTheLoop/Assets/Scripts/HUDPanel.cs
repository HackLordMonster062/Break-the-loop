using TMPro;
using UnityEngine;

public class HUDPanel : MonoBehaviour {
	[SerializeField] TMP_Text clicksCount;

    public void UpdateClicks(int newAmount) {
		clicksCount.text = newAmount.ToString();
	}
}
