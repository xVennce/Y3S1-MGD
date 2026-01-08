using UnityEngine;

public class LoadDataOnStart : MonoBehaviour {
    public static PlayerData CurrentData;
    public static bool playerHasData = false;
    private void Awake() {
        if (SaveLoadSystem.TryLoad(out PlayerData loadedData)) {
            CurrentData = loadedData;
            Debug.Log("Loaded existing player data.");
            playerHasData = true;
        }
        else {
            CurrentData = new PlayerData();
            SaveLoadSystem.Save(CurrentData);
            Debug.Log("No save found. Created new save file.");
            playerHasData = false;
        }
    }
}
