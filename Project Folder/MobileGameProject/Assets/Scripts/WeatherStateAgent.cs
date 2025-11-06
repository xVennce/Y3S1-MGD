using UnityEngine;

using System;
using System.Collections;
using System.Diagnostics.Tracing;
public class WeatherStateAgent : MonoBehaviour {    
    public GetWeatherData DeviceWeatherData;

    [Header("Weather Description")]
    public string WeatherDescription;

    [Header("Weather Effects")]
    public ParticleSystem RainParticleSystem;
    public ParticleSystem SnowParticleSystem;

    private ParticleSystem.MainModule RainMain;
    //thunder uses the same emissions as rain
    private ParticleSystem.EmissionModule RainEmissions;
    private ParticleSystem.EmissionModule SnowEmissions;

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
    
    delegate void EnableEffect();

    private void Start() {
        //Initialize Particle System Modules
        RainMain = RainParticleSystem.main;
        RainEmissions = RainParticleSystem.emission;
        SnowEmissions = SnowParticleSystem.emission;

        //Check if DeviceWeatherData is assigned
        if (DeviceWeatherData != null) {
            Debug.Log("WeatherStateAgent connected to GetWeatherData");
            ChangeWeatherCondition();
        }
        else {
            Debug.LogError("WeatherStateAgent not connected to GetWeatherData");
            DisableCurrentWeatherEffects(ClearEffect);
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
                DisableCurrentWeatherEffects(RainEffect);
                break;
            case "Thunderstorm":
                DisableCurrentWeatherEffects(ThunderEffect);
                break;
            case "Snow":
                DisableCurrentWeatherEffects(SnowEffect);
                break;
            case "Clear":
                DisableCurrentWeatherEffects(ClearEffect);
                break;
            case "Clouds":
                DisableCurrentWeatherEffects(CloudyEffect);
                break;
            default:
                DisableCurrentWeatherEffects(ClearEffect);
                break;
        }
        Debug.Log("Current Weather State: " + CurrentWeatherState);
    }
    private void DisableCurrentWeatherEffects(EnableEffect WeatherParticleSystem) {
        RainEmissions.enabled = false;
        SnowEmissions.enabled = false;
        //var ThunderEmissions;
        //var ClearEmissions;
        //var CloudyEmissions;

        WeatherParticleSystem();
    }
    private void RainEffect() {
        RainEmissions.enabled = true;
        RainMain.simulationSpeed = 3.0f;
        RainMain.maxParticles = 50;
        
    }
    private void ThunderEffect() {
        RainEmissions.enabled = true;
        RainMain.simulationSpeed = 5.0f;
        RainMain.maxParticles = 250;
    }
    private void SnowEffect() {
        SnowEmissions.enabled = true;
    }
    private void ClearEffect() {
    }
    private void CloudyEffect() {
    }
}
