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
    private void Update() {
        UpdateMoneyText();
        UpdateGrowthPercentText();
        UpdateWeatherText();
    }
    private void UpdateWeatherText() {
        WeatherText.text = WeatherDescription.WeatherDescription;
    }
    private void UpdateMoneyText() {
        //Should display money as whole number even though it is a float
        MoneyText.text = LoadDataOnStart.CurrentData.money.ToString();
    }
    private void UpdateGrowthPercentText() {
        GrowthPercentText.text = LoadDataOnStart.CurrentData.plantGrowthStage.ToString("F1") + "%";
    }
}
