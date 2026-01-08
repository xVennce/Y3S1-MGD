using UnityEngine;
using System.Collections;
using System;
public class BambooSellAgent : MonoBehaviour {
    [Header("Base Sell Multiplier")]
    public float SellMultiplier = 1.0f;
    private void Update() {
        CheckBambooStatus();
    }
    private void CheckBambooStatus() {
        if (LoadDataOnStart.CurrentData.plantGrowthStage >= 100.0f) {
            SellBamboo();       
        }
    }
    private void SellBamboo() {
        LoadDataOnStart.CurrentData.plantGrowthStage = 0.0f;
        CheckSellMultiplier();
        GivePlayerMoney();
    }
    private void CheckSellMultiplier() {
        //Insert upgrade checking code here
        //Pseudo code example:
        //if (Upgrade1 is purchased)
        //SellMultiplier = 1.5f;
        SellMultiplier = 1.0f * LoadDataOnStart.CurrentData.GetMultiplierValue(PlayerData.MultiplierNames.SellMultiplier);
    }
    private void GivePlayerMoney() {
        LoadDataOnStart.CurrentData.money += 100.0f * SellMultiplier;
        SaveLoadSystem.Save(LoadDataOnStart.CurrentData);
    }
}
