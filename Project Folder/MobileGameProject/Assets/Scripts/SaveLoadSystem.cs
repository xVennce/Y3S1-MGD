using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//This code was adapted from Brackeys' tutorial on saving and loading data in Unity.
//Reference - https://youtu.be/XOjd_qU2Ido?si=6zZuBPOws-FxHaKG

/// <summary>
/// This class handles saving and loading player data to and from a file.
/// </summary>
public static class SaveLoadSystem {
    /// <summary>
    /// This method saves the player's game data to a file.
    /// </summary>
    /// <param name="gameData"></param>
    public static void SavePlayer(GameData gameData) {

        BinaryFormatter Formatter = new BinaryFormatter();
        //This method of getting the path just makes path constructors across different OS have less problems.
        string Path = Application.persistentDataPath;
        string FileName = "PlayerData.data";
        string FullPath = System.IO.Path.Combine(Path, FileName);

        //Adapted the code in the tutorial to use a using statement to automatically close the file stream.
        //This is generally safer and cleaner as it auto closes the stream.
        using (FileStream stream = new FileStream(FullPath, FileMode.Create)) {
            PlayerData data = new PlayerData(gameData);
            Formatter.Serialize(stream, data);
            Debug.Log("Successfully Saved Data to " + FullPath);
        }
    }
    public static void CreateBlankSave() {

    }
    public static PlayerData LoadPlayer() {

        //This method of getting the path just makes path constructors across different OS have less problems.
        string Path = Application.persistentDataPath;
        string FileName = "PlayerData.data";
        string FullPath = System.IO.Path.Combine(Path, FileName);

        if (File.Exists(FullPath)) {
            Debug.Log("Save file found in " + FullPath);

            BinaryFormatter Formatter = new BinaryFormatter();

            //Adapted the code in the tutorial to use a using statement to automatically close the file stream.
            //This is generally safer and cleaner as it auto closes the stream.
            using (FileStream stream = new FileStream(FullPath, FileMode.Open)) {
                PlayerData data = Formatter.Deserialize(stream) as PlayerData;
                return data;
            }

        }
        else {
            Debug.LogError("Save file not found in " + FullPath);
            Debug.Log("Creating new save file...");
            SaveLoadSystem.SavePlayer(new GameData());
            return null;
        }
    }
}
