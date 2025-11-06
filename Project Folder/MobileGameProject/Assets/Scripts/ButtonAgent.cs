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
    [SerializeField] string GameScene;
    [SerializeField] string SettingScene;

    [Header("Current Ui")]
    [SerializeField] GameObject CurrentUi;

    public void SaveData() {
        SaveLoadSystem.SavePlayerData(gameData);
    }
    public void LoadData() {
        PlayerData data = SaveLoadSystem.LoadPlayerData();
        if (data != null) {
            gameData.Money = data.Money;
            gameData.PlantGrowthStage = data.CurrentPlantGrowthStage;
        }   
    }

    public void LoadGameScene() {
        DeactivateUi(CurrentUi);
        VibrationAgent.HeavyHapticFeedback();
        StartCoroutine(LoadAsyncScene(GameScene));
    }
    public void LoadSettingsScene() {
        DeactivateUi(CurrentUi);
        VibrationAgent.HeavyHapticFeedback();
        StartCoroutine(LoadAsyncScene(SettingScene));
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
        GameObject loadingScreen = GameObject.FindWithTag("LoadingScreen");
        if (loadingScreen != null)
        {
            Destroy(loadingScreen);
        }

        AsyncOperation AsyncLoad = SceneManager.LoadSceneAsync(Scene);

        //This while loop waits until the asynchronous scene fully loads
        while (!AsyncLoad.isDone) {
            yield return null;
        }
    }
}
