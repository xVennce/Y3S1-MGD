using UnityEngine;
using System.IO;

public class SaveLoadSystem {
    private static string FilePath => Path.Combine(Application.persistentDataPath, "playerData.json");

    public static void Save(PlayerData data) {
        data.ValidateData();

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(FilePath, json);
        Debug.Log("Saving Player Data to: " + FilePath);
    }

    public static bool TryLoad(out PlayerData data) {
        if (!File.Exists(FilePath)) {
            Debug.LogWarning("No save file found at: " + FilePath);
            data = new PlayerData();
            return false;
        }

        string json = File.ReadAllText(FilePath);
        data = JsonUtility.FromJson<PlayerData>(json);

        data.ValidateData();

        Debug.Log("Player Data Loaded.");
        return true;
    }
}
