using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public class GetLocation : MonoBehaviour {

    private string DeviceIP;
    
    [Header("Device Location Data")]
    public LocationData DeviceLocationInfo;
    public GetWeatherData getWeatherData;
    public float Latitude = 0.0f;
    public float Longitude = 0.0f;

    [Header("")]
    [SerializeField] private bool IsDelayFinished = true;
    [SerializeField] private float SecondsForDelay = 600.0f;

    private void Start() {
        StartCoroutine(GetDeviceIP());
    }

    private void Update() {
        if (IsDelayFinished == true) {
            IsDelayFinished = false;
            StartCoroutine(GetDeviceIP());
            StartCoroutine(WaitForDelay(SecondsForDelay));
        }
    }

    /// <summary>
    /// This coroutine fetches the device's public IP address using an external API.
    /// </summary>
    /// <returns></returns>
    private IEnumerator GetDeviceIP() {
        using UnityWebRequest DeviceIPRequest = UnityWebRequest.Get("https://api.ipify.org?format=text");
        yield return DeviceIPRequest.SendWebRequest();

        if (DeviceIPRequest.result != UnityWebRequest.Result.Success) {
            Debug.LogError("Error fetching IP: " + DeviceIPRequest.error);
            yield break;
        }

        DeviceIP = DeviceIPRequest.downloadHandler.text;
        StartCoroutine(GetDeviceCoordinates());
    }
    /// <summary>
    /// This coroutine gets the device location data and assigns longitude and latitude to the corresponding one.
    /// </summary>
    /// <returns></returns>
    private IEnumerator GetDeviceCoordinates() {
        using UnityWebRequest DeviceLocationRequest = UnityWebRequest.Get("http://ip-api.com/json/" + DeviceIP);
        yield return DeviceLocationRequest.SendWebRequest();
        
        if (DeviceLocationRequest.result != UnityWebRequest.Result.Success) {
            Debug.LogError("Error fetching location: " + DeviceLocationRequest.error);
            yield break;
        }

        DeviceLocationInfo = JsonUtility.FromJson<LocationData>(DeviceLocationRequest.downloadHandler.text);
        Longitude = DeviceLocationInfo.lon;
        Latitude = DeviceLocationInfo.lat;

        getWeatherData.Init();
    }

    IEnumerator WaitForDelay(float seconds) {
        yield return new WaitForSeconds(seconds);
        IsDelayFinished = true;
    }

    [Serializable]
    public class LocationData {
        public string status;
        public string country;
        public string countryCode;
        public string region;
        public string regionName;
        public string city;
        public string zip;
        public float lat;
        public float lon;
        public string timezone;
        public string isp;
        public string org;
        public string @as;
        public string query;
    }
}
