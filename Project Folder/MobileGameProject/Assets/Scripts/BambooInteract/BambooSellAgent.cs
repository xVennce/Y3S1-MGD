using UnityEngine;
using System.Collections;
using System;
public class BambooSellAgent : MonoBehaviour {
    [SerializeField] private GameData _GameData;

    [Header("Base Sell Multiplier")]
    public float SellMultiplier = 1.0f;
    private void Update() {
        CheckBambooStatus();
    }
    private void CheckBambooStatus() {
        if (_GameData.PlantGrowthStage >= 100.0f) {
            SellBamboo();       
        }
    }
    private void SellBamboo() {
        _GameData.PlantGrowthStage = 0.0f;
        CheckSellMultiplier();
        GivePlayerMoney();
    }
    private void CheckSellMultiplier() {
        //Insert upgrade checking code here
        //Pseudo code example:
        //if (Upgrade1 is purchased)
        //SellMultiplier = 1.5f;
        SellMultiplier = 1.0f;
    }
    private void GivePlayerMoney() {
        _GameData.Money += 100.0f * SellMultiplier;
        SaveLoadSystem.SavePlayerData(_GameData);
    }
}
