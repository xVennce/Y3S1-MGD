using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUi : MonoBehaviour {
    [Header("Ui Stuff")]
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject shopPanel;

    public void OnShopPress() {
        if (gamePanel.activeSelf) {
            gamePanel.SetActive(false);
            shopPanel.SetActive(true);
        }
        else {
            gamePanel.SetActive(true);
            shopPanel.SetActive(false);
        }
    }

}
