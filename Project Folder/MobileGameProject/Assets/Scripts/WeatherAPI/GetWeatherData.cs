using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public class GetWeatherData : MonoBehaviour {

    #region Private Variables
    private float Latitude;
    private float Longitude;
    private bool LocationInitialized;
    private float timer;
    #endregion

    [Header("API key")]
    public string APIKey;

    [Header("API Weather Information")]
    public WeatherInfo Info;
    public string CurrentWeatherDescription;

    [Header("Location Reference")]
    public GetLocation GetDeviceLocation;

    [Header("Update Interval (minutes)")]
    public float minutesBetweenUpdate = 10f;

    public void Init() {
        Latitude = GetDeviceLocation.Latitude;
        Longitude = GetDeviceLocation.Longitude;
        LocationInitialized = true;
    }

    private void Update() {
        if (LocationInitialized) {
            if (timer <= 0) {
                StartCoroutine(GetWeatherInfo());
                timer = minutesBetweenUpdate * 60;
            }
            else {
                timer -= Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// This coroutine fetches weather information from the OpenWeatherMap API.
    /// </summary>
    /// <returns></returns>
    private IEnumerator GetWeatherInfo() {
        string url = "https://api.openweathermap.org/data/2.5/weather?lat=" + Latitude + "&lon=" + Longitude + "&appid=" + APIKey + "&units=metric";
        using UnityWebRequest DeviceWeatherRequest = UnityWebRequest.Get(url);
        yield return DeviceWeatherRequest.SendWebRequest();

        if (DeviceWeatherRequest.result == UnityWebRequest.Result.ConnectionError || DeviceWeatherRequest.result == UnityWebRequest.Result.ProtocolError) {
            Debug.LogError("Weather API error: " + DeviceWeatherRequest.error);
            yield break;
        }

        Info = JsonUtility.FromJson<WeatherInfo>(DeviceWeatherRequest.downloadHandler.text);

        //this section logs the current weather description and temperature if it worked
        if (Info != null && Info.weather != null && Info.weather.Length > 0) {
            //main weather description gives more general condition (e.g., Rain, Clear) than description
            string Description = Info.weather[0].main;
            CurrentWeatherDescription = Description;
            double Temperature = Info.main.temp;
        }
        else {
            Debug.Log("Weather data unavailable.");
        }
    }   

    #region Data Classes
    [Serializable]
    public class WeatherInfo {
        public Weather[] weather;
        public Main main;
        public Wind wind;               
        public string name;
    }

    [Serializable]
    public class Weather {
        public string main;
        public string description;
        public string icon;
    }

    [Serializable]
    public class Main {
        public double temp;
        public double feels_like;
        public double temp_min;
        public double temp_max;
        public int pressure;
        public int humidity;
    }

    [Serializable]
    public class Wind {
        public float speed;
        public int deg;
    }
    #endregion
}

