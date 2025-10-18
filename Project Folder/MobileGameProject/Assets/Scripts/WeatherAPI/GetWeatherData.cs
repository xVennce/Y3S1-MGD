using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using static UnityEngine.InputManagerEntry;

public class GetWeatherData : MonoBehaviour {


    public WeatherInfo Info;
    public string APIKey;

    public GetLocation GetDeviceLocation;
    private float Latitude;
    private float Longitude;
    private bool LocationInitialized;
    private float timer;
    public float minutesBetweenUpdate = 10f;

    public void Begin() {
        Latitude = GetDeviceLocation.Latitude;
        Longitude = GetDeviceLocation.Longitude;
        LocationInitialized = true;
    }

    void Update() {                                                         
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

    private IEnumerator GetWeatherInfo() {
        var url = "https://api.openweathermap.org/data/2.5/weather?lat=" + Latitude + "&lon=" + Longitude + "&appid" + APIKey + "units=metrics";
        using UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
            Debug.LogError("Weather API error: " + www.error);
            yield break;
        }

        Info = JsonUtility.FromJson<WeatherInfo>(www.downloadHandler.text);

        if (Info != null && Info.weather != null && Info.weather.Length > 0) {
            string Description = Info.weather[0].description;
            double Temperature = Info.main.temp;
            Debug.Log("Current weather: " + Description + ", " + Temperature + "°C");
        }
        else {
            Debug.Log("Weather data unavailable.");
        }
    }

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
}

