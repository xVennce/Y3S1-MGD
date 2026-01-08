using System;
using System.Collections;
using UnityEngine;

public class PassiveGrowth : MonoBehaviour {

    [Header("References")]
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
        WeatherInfluenceModifier = WeatherDescription.CurrentWeatherState switch {
            "Sunny"         => 1.25f,//Debug.Log("Sunny weather detected! Increasing growth rate to 1.25x.");
            "Rain"          => 1.5f,//Debug.Log("Rainy weather detected! Increasing growth rate to 1.5x.");
            "Thunderstorm"  => 1.5f,//Debug.Log("Thunderstorm weather detected! Increasing growth rate to 1.5x.");
            "Snow"          => 0.75f,//Debug.Log("Snowy weather detected! Decreasing growth rate to 0.75x.");
            "Clear"         => 1.0f,//Debug.Log("Clear weather detected! Normal growth rate.");
            "Clouds"        => 0.9f,//Debug.Log("Cloudy weather detected! Slightly decreasing growth rate to 0.9x.");
            _               => 1.0f,//Debug.Log("Unrecognized weather condition. Defaulting to normal growth rate.");
        };
    }

    private void ApplyPassiveGrowth() {
        CheckWeather();
        float growthAmount = BaseGrowthAmount * WeatherInfluenceModifier;
        LoadDataOnStart.CurrentData.plantGrowthStage += growthAmount;
        LoadDataOnStart.CurrentData.plantGrowthStage = Mathf.Clamp(LoadDataOnStart.CurrentData.plantGrowthStage, 0.0f, 100.0f);
        SaveLoadSystem.Save(LoadDataOnStart.CurrentData);
    }
    private IEnumerator WaitXSeconds(float seconds, Func function) {
        function();
        yield return new WaitForSeconds(seconds);        
        IsWaiting = true;
    }
}
