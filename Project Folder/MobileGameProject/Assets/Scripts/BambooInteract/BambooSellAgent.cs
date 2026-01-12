using UnityEngine;
using UnityEngine.Audio;

using System.Collections;
using System;
public class BambooSellAgent : MonoBehaviour {
    [Header("Base Sell Multiplier")]
    public float SellMultiplier = 1.0f;
    [Header("Audio")]
    public AudioSource sellAudioSource;

    private void Update() {
        CheckBambooStatus();
    }
    private void CheckBambooStatus() {
        if (LoadDataOnStart.CurrentData.plantGrowthStage >= 100.0f) {
            SellBamboo();       
        }
    }
    private void SellBamboo() {
        PlaySellAudio();
        LoadDataOnStart.CurrentData.plantGrowthStage = 0.0f;
        CheckSellMultiplier();
        GivePlayerMoney();
    }
    private void CheckSellMultiplier() {
        SellMultiplier = 1.0f * StaticVariables.sellMultiplier;
    }
    private void PlaySellAudio() {
        sellAudioSource.Play();
    }
    private void GivePlayerMoney() {
        LoadDataOnStart.CurrentData.money += 100.0f * (1f + SellMultiplier * 0.1f);
        SaveLoadSystem.Save(LoadDataOnStart.CurrentData);
    }
}
