using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public class GetLocation : MonoBehaviour {

    private string DeviceIP;

    public LocationData DeviceLocationInfo;
    public float Latitude;
    public float Longitude;
    public GetWeatherData getWeatherData;

    private void Start() {
        StartCoroutine(GetDeviceIP());
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
        Debug.Log("Device IP: " + DeviceIP);
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

        getWeatherData.Begin();
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
