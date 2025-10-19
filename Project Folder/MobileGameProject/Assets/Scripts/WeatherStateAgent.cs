using UnityEngine;

using System;
using System.Collections;
using System.Diagnostics.Tracing;
public class WeatherStateAgent : MonoBehaviour {    
    public GetWeatherData DeviceWeatherData;

    [Header("Weather Description")]
    public string WeatherDescription;

    [Header("Testing Variable - Change to simulate different weather conditions")]
    public string TestName = "test";

    //Property to track current weather state and change effects when it changes
    private string _CurrentWeatherState = "Clear";
    public string CurrentWeatherState {
        get => _CurrentWeatherState;
        set {
            //Only change weather state and effects if the value is different
            if (_CurrentWeatherState != value) {
                _CurrentWeatherState = value;
                //This is purely for debugging purposes to see when the weather state changes
                WeatherDescription = value;
                ChangeWeatherCondition();
            }
        }
    }

    #region Weather Condition Codes
    //Codes:
    //Thunderstorm
    //Drizzle
    //Rain
    //Snow
    //Clear
    //Clouds
    #endregion
    
    delegate void Effect();

    private void Start() {
        //Check if DeviceWeatherData is assigned
        if (DeviceWeatherData != null) {
            Debug.Log("WeatherStateAgent connected to GetWeatherData");
            ChangeWeatherCondition();
        }
        else {
            Debug.LogError("WeatherStateAgent not connected to GetWeatherData");
            SetWeatherEffect(ClearEffect);
        }
    }
    private void Update() {
        if (TestName == "test") {
            CurrentWeatherState = DeviceWeatherData.CurrentWeatherDescription;
        }
        

        if (TestName != "test") {
            CurrentWeatherState = TestName;
        }
    }

    private void ChangeWeatherCondition() {
        //switch case to activate weather effects based on CurrentWeatherState
        //if the state is not recognized, default to ClearEffect
        //default case is there since there are other weather conditions not handled here i.e Group 7xx: Atmosphere
        switch (CurrentWeatherState) {
            case "Rain":
                Debug.Log("Current Weather State: " + CurrentWeatherState);
                SetWeatherEffect(RainEffect);
                break;
            case "Snow":
                Debug.Log("Current Weather State: " + CurrentWeatherState);
                SetWeatherEffect(SnowEffect);
                break;
            case "Clear":
                Debug.Log("Current Weather State: " + CurrentWeatherState);
                SetWeatherEffect(ClearEffect);
                break;
            case "Clouds":
                Debug.Log("Current Weather State: " + CurrentWeatherState);
                SetWeatherEffect(CloudyEffect);
                break;
            default:
                Debug.Log("Current Weather State not recognized, defaulting to Clear Effect");
                SetWeatherEffect(ClearEffect);
                break;
        }
    }

    private void SetWeatherEffect(Effect WeatherEffect) {
        WeatherEffect();
    }
    private void RainEffect() {
        Debug.Log("Rain Effect Activated");
    }
    private void SnowEffect() {
        Debug.Log("Snow Effect Activated");
    }
    private void ClearEffect() {
        Debug.Log("Clear Effect Activated");
    }
    private void CloudyEffect() {
        Debug.Log("Cloudy Effect Activated");
    }
}
