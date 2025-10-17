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
        if (IsDelayFinished == true) {
            IsDelayFinished = false;
            CheckTime();
            StartCoroutine(WaitForDelay(SecondsForDelay));
        }

        ControlPPV();
    }

    private void CheckTime() {
        Debug.Log("This was called");
        Debug.Log("Hour: " + TimeCheckerAgent.GetDeviceHour());
        Debug.Log("Minute: " + TimeCheckerAgent.GetDeviceMins());
        Debug.Log("Month: " + TimeCheckerAgent.GetDeviceMonth());

    }

    private void ControlPPV() {
        //This controls the transition of the PPV weight based on time of day
        #region Transition Logic
        //For when it is Dusk
        if (TimeCheckerAgent.GetDeviceHour() >= Dusk && TimeCheckerAgent.GetDeviceHour() < (Dusk + 1)) {
            PPV.weight = (float)TimeCheckerAgent.GetDeviceMins() / 60;
            //Add anything here that should become visable when it turns to dark
        }

        //For when it is Dawn
        if (TimeCheckerAgent.GetDeviceHour() >= Dawn && TimeCheckerAgent.GetDeviceHour() < (Dawn + 1)) {
            PPV.weight = 1 - (float)TimeCheckerAgent.GetDeviceMins() / 60;
            //Add anything here that should become visable when it turns to dark
        }
        #endregion

        //This handles immediate switching of PPV weight for when the player starts the game not during the transition periods
        #region Immediate Logic 
        //For when it is fully Night
        if (TimeCheckerAgent.GetDeviceHour() >= (Dusk + 1) || TimeCheckerAgent.GetDeviceHour() < Dawn) {
            PPV.weight = 1;
            //Add anything here that should become visable when it is fully dark
        }

        //For when it is fully Day
        if (TimeCheckerAgent.GetDeviceHour() >= (Dawn + 1) && TimeCheckerAgent.GetDeviceHour() < Dusk) {
            PPV.weight = 0;
            //Add anything here that should become visable when it is fully light
        }
        #endregion
    }


    private void AdjustDuskDawnDaylightSaving() {

    }
    IEnumerator WaitForDelay(float seconds) {
        Debug.Log("Started timer");
        yield return new WaitForSeconds(seconds);
        IsDelayFinished = true;
    }
}
