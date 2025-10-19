using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAgent : MonoBehaviour {
    [Header("Game Data")]
    [SerializeField] GameData gameData;

    [Header("Scene Management")]
    [SerializeField] SceneManager SceneManagement;

    [Header("Main Menu Scene Dependables")]
    [SerializeField] string GameSceneName;

    public void SaveData() {
        SaveLoadSystem.SavePlayer(gameData);
    }
    public void LoadData() {
        PlayerData data = SaveLoadSystem.LoadPlayer();
        if (data != null) {
            gameData.Money = data.Money;
            gameData.PlantGrowthStage = data.CurrentPlantGrowthStage;
            Debug.Log("Money loaded: " + data.Money);
            Debug.Log("Plant Growth Stage loaded: " + data.CurrentPlantGrowthStage);
        }   
    }

    public void LoadGameScene() {
        StartCoroutine(LoadAsyncScene(GameSceneName));
    }

    /// <summary>
    /// This coroutine loads a scene asynchronously with load screen. Found in Unity Documentation.
    /// </summary>
    /// <param name="Scene"></param>
    /// <returns></returns>
    IEnumerator LoadAsyncScene(string Scene) {

        AsyncOperation AsyncLoad = SceneManager.LoadSceneAsync(Scene);

        while (!AsyncLoad.isDone) {
            yield return null;
        }
    }
}
