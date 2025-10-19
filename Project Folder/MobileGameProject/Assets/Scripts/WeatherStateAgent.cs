using UnityEngine;

using System;
using System.Collections;
public class WeatherStateAgent : MonoBehaviour {    
    public GetWeatherData DeviceWeatherData;
    [Header("Weather Description")]
    public string WeatherDescription;

    [Header("Testing Variable - Change to simulate different weather conditions")]
    public string TestName = "test";
    public string CurrentWeatherState;
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
        }
        else {
            Debug.LogError("WeatherStateAgent not connected to GetWeatherData");
        }

    }
    private void Update() {
        CurrentWeatherState = DeviceWeatherData.CurrentWeatherDescription;

        if (TestName != "test") {
            Debug.Log("CurrentWeatherState was changed to: " + TestName);
            CurrentWeatherState = TestName;
        }

        Debug.Log("Current Weather State: " + CurrentWeatherState);
        //switch case to activate weather effects based on CurrentWeatherState
        //if the state is not recognized, default to ClearEffect
        //default case is there since there are other weather conditions not handled here i.e Group 7xx: Atmosphere
        switch (CurrentWeatherState) {
            case "Rain":
                ActivateWeatherEffect(RainEffect);
                break;
            case "Snow":
                ActivateWeatherEffect(SnowEffect);
                break;
            case "Clear":
                ActivateWeatherEffect(ClearEffect);
                break;
            case "Clouds":
                ActivateWeatherEffect(CloudyEffect);
                break;
            default:
                ActivateWeatherEffect(ClearEffect);
                break;
        }
    }

    private void ActivateWeatherEffect(Effect WeatherEffect) {
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
