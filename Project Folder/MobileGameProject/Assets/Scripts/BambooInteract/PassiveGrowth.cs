using System;
using System.Collections;
using UnityEngine;

public class PassiveGrowth : MonoBehaviour {

    [Header("References")]
    [SerializeField] private GameData PlayerGameData;
    [SerializeField] private WeatherStateAgent WeatherDescription;

    [Header("Growth Settings")]
    [SerializeField] private float GrowthInterval = 5f;
    [SerializeField] private float BaseGrowthAmount = 0.05f;

    [Header("Weather Influence Modifier")]
    public float WeatherInfluenceModifier = 1.0f;

    private bool IsWaiting = true;

    private delegate void Func();
    private void Update() {
        if (IsWaiting == true) {
            IsWaiting = false;
            StartCoroutine(WaitXSeconds(GrowthInterval, ApplyPassiveGrowth));           
        }

    }

    private void CheckWeather() {
        switch (WeatherDescription.CurrentWeatherState) {
            case "Sunny":
                //Debug.Log("Sunny weather detected! Increasing growth rate to 1.25x.");
                WeatherInfluenceModifier = 1.25f;
                break;
            case "Rain":
                //Debug.Log("Rainy weather detected! Increasing growth rate to 1.5x.");
                WeatherInfluenceModifier = 1.5f;
                break;
            case "Thunderstorm":
                //Debug.Log("Thunderstorm weather detected! Increasing growth rate to 1.5x.");
                WeatherInfluenceModifier = 1.5f;
                break;
            case "Snow":
                //Debug.Log("Snowy weather detected! Decreasing growth rate to 0.75x.");
                WeatherInfluenceModifier = 0.75f;
                break;
            case "Clear":
                //Debug.Log("Clear weather detected! Normal growth rate.");
                WeatherInfluenceModifier = 1.0f;
                break;
            case "Clouds":
                //Debug.Log("Cloudy weather detected! Slightly decreasing growth rate to 0.9x.");
                WeatherInfluenceModifier = 0.9f;
                break;
            default:
                //Debug.Log("Unrecognized weather condition. Defaulting to normal growth rate.");
                WeatherInfluenceModifier = 1.0f;
                break;
        }
    }

    private void ApplyPassiveGrowth() {
        CheckWeather();
        float growthAmount = BaseGrowthAmount * WeatherInfluenceModifier;
        PlayerGameData.PlantGrowthStage += growthAmount;
        PlayerGameData.PlantGrowthStage = Mathf.Clamp(PlayerGameData.PlantGrowthStage, 0.0f, 100.0f);
        SaveLoadSystem.SavePlayerData(PlayerGameData);
    }
    private IEnumerator WaitXSeconds(float seconds, Func function) {
        function();
        yield return new WaitForSeconds(seconds);        
        IsWaiting = true;
    }
}
