using UnityEngine;
using UnityEngine.Audio;
public class SettingsAgent : MonoBehaviour {
    [SerializeField] private AudioMixer MasterAudioMixer;
    [SerializeField] private AudioMixer BackgroundAudioMixer;

    public void SetGlobalAudio(float Volume) {
        //Converts the volume from -80 to 0 to a logarithmic scale (0.0001 to 1)
        MasterAudioMixer.SetFloat("MasterVolume", Mathf.Log10(Volume) * 20);
    }
    public void SetBackgroundMusicAudio(float Volume) {
        //Converts the volume from -80 to 0 to a logarithmic scale (0.0001 to 1)
        BackgroundAudioMixer.SetFloat("BackgroundVolume", Mathf.Log10(Volume) * 20);
    }
}
