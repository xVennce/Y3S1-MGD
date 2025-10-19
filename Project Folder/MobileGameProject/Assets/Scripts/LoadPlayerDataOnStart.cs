using UnityEngine;

public class LoadPlayerDataOnStart : MonoBehaviour {
    [SerializeField] private GameData gameData;
    [SerializeField] private GameVolumeData gameVolumeData;
    [SerializeField] private SettingsAgent settingsAgent;



    private void Start() {
        LoadPlayerData();
        LoadPlayerVolumeData();
    }
    private void LoadPlayerData() {
        Debug.Log("Loading Player Data on Start...");
        PlayerData data = SaveLoadSystem.LoadPlayerData();
        if (data != null) {
            gameData.Money = data.Money;
            gameData.PlantGrowthStage = data.CurrentPlantGrowthStage;
            Debug.Log("Money loaded: " + data.Money);
            Debug.Log("Plant Growth Stage loaded: " + data.CurrentPlantGrowthStage);
        }
    }

    private void LoadPlayerVolumeData() {
        Debug.Log("Loading Player Audio Data on Start...");
        PlayerSoundData data = SaveLoadSystem.LoadPlayerSoundData();
        if (data != null) {
            gameVolumeData.GlobalVolume = data.PlayerGlobalVolume;
            gameVolumeData.BackgroundVolume = data.PlayerBackgroundVolume;
            Debug.Log("Global Volume loaded: " + data.PlayerGlobalVolume);
            Debug.Log("Background Volume loaded: " + data.PlayerBackgroundVolume);
            SetVolumeSliders();
        }
    }

    private void SetVolumeSliders() {
        settingsAgent.GlobalVolumeSlider.value = Mathf.Pow(10f, gameVolumeData.GlobalVolume / 20f);
        settingsAgent.SetGlobalAudio(Mathf.Pow(10f, gameVolumeData.GlobalVolume / 20f));
        settingsAgent.BackgroundVolumeSlider.value = Mathf.Pow(10f, gameVolumeData.BackgroundVolume / 20f);
        settingsAgent.SetBackgroundMusicAudio(Mathf.Pow(10f, gameVolumeData.BackgroundVolume / 20f));
        
    }
}
