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

    [SerializeField] private Toggle globalToggle;
    [SerializeField] private Toggle bgmToggle;

    private bool hasLoadedFromStart = false;
    private void Start() {
        if (LoadDataOnStart.playerHasData) {
            GlobalVolumeSlider.value = LoadDataOnStart.CurrentData.globalAudio;
            MasterAudioMixer.SetFloat("MasterVolume", Mathf.Log10(LoadDataOnStart.CurrentData.globalAudio) * 20);
            BackgroundVolumeSlider.value = LoadDataOnStart.CurrentData.bgmAudio;
            BackgroundAudioMixer.SetFloat("BackgroundVolume", Mathf.Log10(LoadDataOnStart.CurrentData.bgmAudio) * 20);
            SetToggles();
        }
        hasLoadedFromStart = true;
    }
    public void BackToMainMenu() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
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
    public void SetToggles() {
        PlayerData data = LoadDataOnStart.CurrentData;
        if (data.toggleGlobalAudio) {
            globalToggle.isOn = true;
        }
        else {
            globalToggle.isOn = false;
        }
        if (data.toggleBgmAudio) {
            bgmToggle.isOn = true;
        }
        else {
            bgmToggle.isOn = false;
        }
    }
    public void SetToggleGlobal() {
        PlayerData data = LoadDataOnStart.CurrentData;
        if (globalToggle.isOn) {
            data.toggleGlobalAudio = true;
            SaveLoadSystem.Save(LoadDataOnStart.CurrentData);
        }
        else {
            data.toggleGlobalAudio = false;
            SaveLoadSystem.Save(LoadDataOnStart.CurrentData);
        }
    }
    public void SetToggleBGM() {
        PlayerData data = LoadDataOnStart.CurrentData;
        if (bgmToggle.isOn) {
            data.toggleBgmAudio = true;
            SaveLoadSystem.Save(LoadDataOnStart.CurrentData);
        }
        else {
            data.toggleBgmAudio = false;
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
