using UnityEngine;
using System.Collections;
using System;

public class BambooPress : MonoBehaviour {
    [SerializeField] GameObject GameData;
    //This is the base tap level and growth multiplier
    private int BaseLevel = 1;
    private float GrowthMultiplier = 0.1f;

    public void BambooPressed() {
        CheckForUpgrades();
        
        GameData.GetComponent<GameData>().PlantGrowthStage += 1.0f * GrowthMultiplier;
        //Clamps value 0 to 100
        GameData.GetComponent<GameData>().PlantGrowthStage = Mathf.Clamp(GameData.GetComponent<GameData>().PlantGrowthStage, 0.0f, 100.0f);
        GameData.GetComponent<GameData>().Money += BaseLevel;
        SaveLoadSystem.SavePlayerData(GameData.GetComponent<GameData>());
    }
    private void CheckForUpgrades() {
        //add upgrade checking code here
        //Pseudo code example:
        //if (Upgrade1 is purchased)
        //BaseLevel = 2;
        BaseLevel = 1;
        GrowthMultiplier = 0.1f;
    }
}
