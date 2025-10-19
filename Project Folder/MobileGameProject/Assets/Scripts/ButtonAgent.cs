using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAgent : MonoBehaviour {
    [Header("Game Data")]
    [SerializeField] GameData gameData;

    [Header("Loading Screen")]
    [SerializeField] GameObject LoadingScreen;

    [Header("Scene Management")]
    [SerializeField] SceneManager SceneManagement;

    [Header("Main Menu Scene Dependables")]
    [SerializeField] string GameSceneName;
    [SerializeField] GameObject MainMenuUi;

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
        DeactivateUi(MainMenuUi);
        VibrationAgent.HeavyHapticFeedback();
        StartCoroutine(LoadAsyncScene(GameSceneName));
    }

    private void DeactivateUi(GameObject SelectedUi) {
        SelectedUi.SetActive(false);
    }

    /// <summary>
    /// This coroutine loads a scene asynchronously with load screen. Found in Unity Documentation.
    /// </summary>
    /// <param name="Scene"></param>
    /// <returns></returns>
    IEnumerator LoadAsyncScene(string Scene) {
        //In here should be a loading screen or anything that needs to be done during loading
        Instantiate(LoadingScreen);

        //Time delay to see loading screen
        yield return new WaitForSeconds(2.0f); 

        AsyncOperation AsyncLoad = SceneManager.LoadSceneAsync(Scene);

        //This while loop waits until the asynchronous scene fully loads
        while (!AsyncLoad.isDone) {
            yield return null;
        }
    }
}
