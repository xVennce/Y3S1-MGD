using UnityEngine;

public class PlayerDataViewer : MonoBehaviour {
    [TextArea(10, 30)]
    public string debugView;

    private void Update() {
        RefreshView();
    }

    [ContextMenu("Refresh View")]
    public void RefreshView() {
        PlayerData data = LoadDataOnStart.CurrentData;
        debugView =
            "===== PLAYER DATA =====\n" +
            $"Global Audio: {data.globalAudio}\n" +
            $"BGM Audio: {data.bgmAudio}\n" +
            $"Toggle Global Audio: {data.toggleGlobalAudio}\n" +
            $"Toggle BGM Audio: {data.toggleBgmAudio}\n" +
            $"Plant Growth Stage: {data.plantGrowthStage}\n" +
            $"Money: {data.money}\n\n" +
            "---- Multipliers ----\n";

        foreach (var m in data.multipliers) {
            debugView += $"{m.MultiplierName} = {m.MultiplierValue}\n";
        }
    }

}
