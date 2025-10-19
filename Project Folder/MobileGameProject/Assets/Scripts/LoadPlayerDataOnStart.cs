using UnityEngine;

public class LoadPlayerDataOnStart : MonoBehaviour {

    private void Start() {
        Debug.Log("Loading Player Data on Start...");
        SaveLoadSystem.LoadPlayer();
    }
}
