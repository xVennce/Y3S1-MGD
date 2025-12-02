using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SettingsAgent : MonoBehaviour {
    [SerializeField] private AudioMixer MasterAudioMixer;
    [SerializeField] private AudioMixer BackgroundAudioMixer;
    [SerializeField] private GameVolumeData gameVolumeData;
    [SerializeField] private LoadPlayerDataOnStart OnStartCheck;
    public Slider GlobalVolumeSlider;
    public Slider BackgroundVolumeSlider;
    public void SetGlobalAudio(float Volume) {
        //Converts the volume from -80 to 0 to a logarithmic scale (0.0001 to 1)
        MasterAudioMixer.SetFloat("MasterVolume", Mathf.Log10(Volume) * 20);
        gameVolumeData.GlobalVolume = Mathf.Log10(Volume) * 20;
        LoadedPlayerDataCheck();
    }
    public void SetBackgroundMusicAudio(float Volume) {
        //Converts the volume from -80 to 0 to a logarithmic scale (0.0001 to 1)
        BackgroundAudioMixer.SetFloat("BackgroundVolume", Mathf.Log10(Volume) * 20);
        gameVolumeData.BackgroundVolume = Mathf.Log10(Volume) * 20;
        LoadedPlayerDataCheck();
    }
    private void LoadedPlayerDataCheck() {
        if (OnStartCheck.HasLoaded) {
            return;
        }
        else {
            SaveLoadSystem.SavePlayerVolumeData(gameVolumeData);
        }
    }
}
