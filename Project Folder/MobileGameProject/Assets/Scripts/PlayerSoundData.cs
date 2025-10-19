using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSoundData{
    public float PlayerGlobalVolume, PlayerBackgroundVolume;

    public PlayerSoundData(GameVolumeData gameVolumeData) {
        PlayerGlobalVolume = gameVolumeData.GlobalVolume;
        PlayerBackgroundVolume = gameVolumeData.BackgroundVolume;
    }
}
