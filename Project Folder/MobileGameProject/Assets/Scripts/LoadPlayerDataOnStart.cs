using System;
using UnityEngine;

public class LoadPlayerDataOnStart : MonoBehaviour {
    [SerializeField] private GameData gameData;
    [SerializeField] private GameVolumeData gameVolumeData;
    [SerializeField] private SettingsAgent settingsAgent;

    public Boolean HasLoaded = false;
    private void Start() {
        LoadPlayerData();
        LoadPlayerVolumeData();
        HasLoaded = true;
        SetVolumeSliders();
    }
    private void LoadPlayerData() {
        PlayerData data = SaveLoadSystem.LoadPlayerData();
        if (data != null) {
            gameData.Money = data.Money;
            gameData.PlantGrowthStage = data.CurrentPlantGrowthStage;
            Debug.Log("Player Data Loaded - Money: " + data.Money + " Current Growth Stage: " + data.CurrentPlantGrowthStage);
        }
    }
    private void LoadPlayerVolumeData() {
        PlayerSoundData data = SaveLoadSystem.LoadPlayerSoundData();
        if (data != null) {
            gameVolumeData.GlobalVolume = data.PlayerGlobalVolume;
            gameVolumeData.BackgroundVolume = data.PlayerBackgroundVolume;
            Debug.Log("Player Sound Data Loaded");
        }
    }
    private void SetVolumeSliders() {
        settingsAgent.GlobalVolumeSlider.value = Mathf.Pow(10f, gameVolumeData.GlobalVolume / 20f);
        settingsAgent.SetGlobalAudio(Mathf.Pow(10f, gameVolumeData.GlobalVolume / 20f));
        settingsAgent.BackgroundVolumeSlider.value = Mathf.Pow(10f, gameVolumeData.BackgroundVolume / 20f);
        settingsAgent.SetBackgroundMusicAudio(Mathf.Pow(10f, gameVolumeData.BackgroundVolume / 20f));
    }
}
