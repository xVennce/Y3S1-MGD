using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
    public float Money;
    public int TotalPlantsSold;
    public float TimeSinceLastOpened, CurrentPlantGrowthStage;

    public PlayerData(GameData gameData) {
        Money = gameData.Money;
        CurrentPlantGrowthStage = gameData.PlantGrowthStage;
    }
}
