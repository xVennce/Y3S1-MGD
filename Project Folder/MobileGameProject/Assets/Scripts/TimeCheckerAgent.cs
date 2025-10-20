using UnityEngine;
using System;


public class TimeCheckerAgent : MonoBehaviour {
    /// <summary>
    /// This function gets the device minute as an integer (0-59)
    /// </summary>
    public static int GetDeviceMins() {
        DateTime Temp = DateTime.Now;
        return Temp.Minute;
    }

    /// <summary>
    /// This function gets the device hour as an integer (0-24)
    /// </summary>
    public static int GetDeviceHour() {
        DateTime Temp = DateTime.Now;
        return Temp.Hour;
    }

    /// <summary>
    /// This function gets the device month as an integer (1-12)
    /// </summary>
    public static int GetDeviceMonth() {
        DateTime Temp = DateTime.Now;
        return Temp.Month;
    }
}
