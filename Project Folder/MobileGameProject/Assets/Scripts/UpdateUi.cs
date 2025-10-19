using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;

public class UpdateUi : MonoBehaviour {
    [SerializeField] private TMPro.TextMeshProUGUI MoneyText;
    [SerializeField] private TMPro.TextMeshProUGUI GrowthPercentText;
    [SerializeField] private TMPro.TextMeshProUGUI WeatherText;
    [SerializeField] private WeatherStateAgent WeatherDescription;
    [SerializeField] private GameData PlayerGameData;

    private void Update() {
        UpdateMoneyText();
        UpdateGrowthPercentText();
        UpdateWeatherText();
    }
    private void UpdateWeatherText() {
        WeatherText.text = WeatherDescription.WeatherDescription;
    }
    private void UpdateMoneyText() {
        MoneyText.text = PlayerGameData.Money.ToString();
    }
    private void UpdateGrowthPercentText() {
        GrowthPercentText.text = PlayerGameData.PlantGrowthStage.ToString("F1") + "%";
    }
}
