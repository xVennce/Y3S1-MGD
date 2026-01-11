using UnityEngine;
public class CheckAudio : MonoBehaviour {
    [SerializeField] private AudioSource MasterAudioSource;
    [SerializeField] private AudioSource BackgroundAudioSource;
    private void Update() {
        CheckToggleStatus();
    }
    private void CheckToggleStatus() {
        PlayerData data = LoadDataOnStart.CurrentData;

        if (data.toggleGlobalAudio) {
            MasterAudioSource.mute = true;
        }
        else {
            MasterAudioSource.mute = false;
        }

        if (data.toggleBgmAudio) {
            BackgroundAudioSource.mute = true;
        }
        else {
            BackgroundAudioSource.mute = false;
        }
    }
}
