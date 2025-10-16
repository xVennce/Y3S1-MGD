using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
public class DayNightAgent : MonoBehaviour {
    [Header("Post-Processing Volume")]
    [SerializeField] Volume PPV;

    [Header("Time Delay Settings")]
    [SerializeField] float SecondsForDelay = 600;
    [SerializeField] bool IsDelayFinished = true;

    [Header("Time: Dusk, Dawn Start/Finish")]
    [SerializeField] int Dusk = 21;
    [SerializeField] int Dawn = 6;

    private void FixedUpdate() {
        //Checks time then waits for a delay
        if (IsDelayFinished == true)
        {
            IsDelayFinished = false;
            CheckTime();
            StartCoroutine(WaitForDelay(SecondsForDelay));
        }

        ControlPPV();
    }

    private void CheckTime() {
        Debug.Log("This was called");
        Debug.Log("Hour: " + TimeCheckerAgent.GetDeviceHour());
        Debug.Log("Month: " + TimeCheckerAgent.GetDeviceMonth());

    }

    private void ControlPPV() {
        //For when it is Dusk
        if (TimeCheckerAgent.GetDeviceHour() >= Dusk && TimeCheckerAgent.GetDeviceHour() < (Dusk + 1)) {
            PPV.weight = (float)TimeCheckerAgent.GetDeviceMins() / 60;
            //Add anything here that should become visable when it turns to dark
        }

        //For when it is Dawn
        if (TimeCheckerAgent.GetDeviceHour() >= Dawn && TimeCheckerAgent.GetDeviceHour() < (Dawn + 1))
        {
            PPV.weight = 1 - (float)TimeCheckerAgent.GetDeviceMins() / 60;
            //Add anything here that should become visable when it turns to dark
        }

    }

    IEnumerator WaitForDelay(float seconds) {
        Debug.Log("Started timer");
        yield return new WaitForSeconds(seconds);
        IsDelayFinished = true;
    }
}
