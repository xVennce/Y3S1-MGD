using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class SettingsAgent : MonoBehaviour {
    [SerializeField] private AudioMixer MasterAudioMixer;
    [SerializeField] private AudioMixer BackgroundAudioMixer;
    public Slider GlobalVolumeSlider;
    public Slider BackgroundVolumeSlider;

    [SerializeField] private Canvas gameCanvas;
    [SerializeField] private Canvas settingsCanvas;
    
    private bool hasLoadedFromStart = false;
    private void Start() {
        if (LoadDataOnStart.playerHasData) {
            GlobalVolumeSlider.value = LoadDataOnStart.CurrentData.globalAudio;
            MasterAudioMixer.SetFloat("MasterVolume", Mathf.Log10(LoadDataOnStart.CurrentData.globalAudio) * 20);
            BackgroundVolumeSlider.value = LoadDataOnStart.CurrentData.bgmAudio;
            BackgroundAudioMixer.SetFloat("BackgroundVolume", Mathf.Log10(LoadDataOnStart.CurrentData.bgmAudio) * 20);
        }
        hasLoadedFromStart = true;
    }
    public void OpenSettingsUi() {
        gameCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(true);
    }
    public void OpenGameUi() {
        gameCanvas.gameObject.SetActive(true);
        settingsCanvas.gameObject.SetActive(false);
    }
    public void SetGlobalAudio(float Volume) {
        //Converts the volume from -80 to 0 to a logarithmic scale (0.0001 to 1)
        MasterAudioMixer.SetFloat("MasterVolume", Mathf.Log10(Volume) * 20);
        LoadDataOnStart.CurrentData.globalAudio = Volume;
        if (hasLoadedFromStart) {
            SaveLoadSystem.Save(LoadDataOnStart.CurrentData);
        }
    }
    public void SetBackgroundMusicAudio(float Volume) {
        //Converts the volume from -80 to 0 to a logarithmic scale (0.0001 to 1)
        BackgroundAudioMixer.SetFloat("BackgroundVolume", Mathf.Log10(Volume) * 20);
        LoadDataOnStart.CurrentData.bgmAudio = Volume;
        if (hasLoadedFromStart) {
            SaveLoadSystem.Save(LoadDataOnStart.CurrentData);
        }
    }

}
