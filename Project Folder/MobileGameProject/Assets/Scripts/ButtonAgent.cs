using UnityEngine;

public class ButtonAgent : MonoBehaviour
{
    [SerializeField] GameData gameData;
    public void SaveData() {
        SaveLoadSystem.SavePlayer(gameData);
    }
    public void LoadData() {
        PlayerData data = SaveLoadSystem.LoadPlayer();
        if (data != null)
        {
            gameData.Money = data.Money;
            gameData.PlantGrowthStage = data.CurrentPlantGrowthStage;
            Debug.Log("Money loaded: " + data.Money);
            Debug.Log("Plant Growth Stage loaded: " + data.CurrentPlantGrowthStage);
        }
    }
}
