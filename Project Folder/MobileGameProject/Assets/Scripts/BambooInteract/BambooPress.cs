using UnityEngine;
using System.Collections;
using System;

public class BambooPress : MonoBehaviour {
    public bool IsPressed = false;
    //This is the base tap level and growth multiplier  
    //private int BaseLevel = 1;
    [Header("Growth Settings")]
    public float GrowthMultiplier = 0.1f;
    public float BaseGrowthPerTap = 1.0f;

    public void BambooPressed() {
        CheckForUpgrades();
        LoadDataOnStart.CurrentData.plantGrowthStage += 1.0f * GrowthMultiplier;
        //Clamps value 0 to 100  
        LoadDataOnStart.CurrentData.plantGrowthStage = Mathf.Clamp(LoadDataOnStart.CurrentData.plantGrowthStage, 0.0f, 100.0f);
        //GameData.GetComponent<GameData>().Money += BaseLevel;
        SaveLoadSystem.Save(LoadDataOnStart.CurrentData);
    }
    private void CheckForUpgrades() {
        PlayerData currentData = LoadDataOnStart.CurrentData;
        //add upgrade checking code here  
        //Pseudo code example:  
        //if (Upgrade1 is purchased)  
        //BaseLevel = 2;  
        //BaseLevel = 1;
        //GrowthMultiplier = 0.1f;
        GrowthMultiplier = 1.0f * currentData.GetMultiplierValue(PlayerData.MultiplierNames.GrowthMultiplier);
    }
}
