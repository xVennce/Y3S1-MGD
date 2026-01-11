using UnityEngine;
using System.Collections;
using System;

public class BambooPress : MonoBehaviour {
    public bool IsPressed = false;
    //This is the base tap level and growth multiplier  
    //private int BaseLevel = 1;
    [Header("Growth Settings")]
    public float BaseGrowthPerTap = 1.0f;

    public void BambooPressed() {
        LoadDataOnStart.CurrentData.plantGrowthStage += 1.0f * StaticVariables.growthMultiplier;
        //Clamps value 0 to 100  
        LoadDataOnStart.CurrentData.plantGrowthStage = Mathf.Clamp(LoadDataOnStart.CurrentData.plantGrowthStage, 0.0f, 100.0f);
        //GameData.GetComponent<GameData>().Money += BaseLevel;
        SaveLoadSystem.Save(LoadDataOnStart.CurrentData);
    }
}
