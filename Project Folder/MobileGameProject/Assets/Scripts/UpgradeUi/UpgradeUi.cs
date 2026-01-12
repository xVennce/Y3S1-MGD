using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
public class UpgradeUi : MonoBehaviour {
    [Header("Upgrade Prices")]
    [SerializeField] private int growthPrice = 100;
    [SerializeField] private int passiveGrowthPrice = 100;
    [SerializeField] private int sellPrice = 100;

    [Header("Checking Bools")]
    [SerializeField] private bool canBuyGrowthUpgrade = false;
    [SerializeField] private bool canBuyPassiveGrowthUpgrade = false;
    [SerializeField] private bool canBuySellUpgrade = false;

    [Header("Middle Panel Variable")]
    [SerializeField] private GameObject middlePanel;
    [SerializeField] private GameObject gameMiddlePanel;

    [Header("Audio")]
    [SerializeField] private AudioSource upgradeAudioSource;

    [Header("Ui Variables")]
    [SerializeField] private TextMeshProUGUI moneyText;

    [SerializeField] private TextMeshProUGUI growthLevelText;
    [SerializeField] private TextMeshProUGUI passiveGrowthLevelText;
    [SerializeField] private TextMeshProUGUI sellLevelText;

    [SerializeField] private TextMeshProUGUI growthLevelUpgradeText;
    [SerializeField] private TextMeshProUGUI passiveGrowthLevelUpgradeText;
    [SerializeField] private TextMeshProUGUI sellLevelUpgradeText;
    private void Start() {
        SetNewMultipliers();
    }

    private void Update() {
        SetNewMultipliers();
        UpdatePrices();
        UpdateText();
        SetWhatPlayerCanBuy();
    }
    public void BackButton() {
        gameMiddlePanel.SetActive(true);
        middlePanel.SetActive(false);
    }
    public void IncrementGrowthLevel() {
        if (canBuyGrowthUpgrade) {
            PlayerData data = LoadDataOnStart.CurrentData;
            float newValue = LoadDataOnStart.CurrentData.GetMultiplierValue(PlayerData.MultiplierNames.GrowthMultiplier) + 1f;
            data.SetMultiplierValue(PlayerData.MultiplierNames.GrowthMultiplier, newValue);
            data.money -= growthPrice;
            SaveLoadSystem.Save(data);
            upgradeAudioSource.Play();
        }
    }
    public void IncrementPassiveGrowthLevel() {
        if (canBuyPassiveGrowthUpgrade) {
            PlayerData data = LoadDataOnStart.CurrentData;
            float newValue = LoadDataOnStart.CurrentData.GetMultiplierValue(PlayerData.MultiplierNames.PassiveGrowthMultiplier) + 1f;
            data.SetMultiplierValue(PlayerData.MultiplierNames.PassiveGrowthMultiplier, newValue);
            data.money -= passiveGrowthPrice;
            SaveLoadSystem.Save(data);
            upgradeAudioSource.Play();
        }
    }
    public void IncrementSellLevel() {
        if (canBuySellUpgrade) {
            PlayerData data = LoadDataOnStart.CurrentData;
            float newValue = LoadDataOnStart.CurrentData.GetMultiplierValue(PlayerData.MultiplierNames.SellMultiplier) + 1f;
            data.SetMultiplierValue(PlayerData.MultiplierNames.SellMultiplier, newValue);
            data.money -= sellPrice;
            SaveLoadSystem.Save(data);
            upgradeAudioSource.Play();
        }
    }
    private void SetNewMultipliers() {
        PlayerData data = LoadDataOnStart.CurrentData;

        int growthLevel = (int)data.GetMultiplierValue(PlayerData.MultiplierNames.GrowthMultiplier);
        int passiveGrowthLevel = (int)data.GetMultiplierValue(PlayerData.MultiplierNames.PassiveGrowthMultiplier);
        int sellLevel = (int)data.GetMultiplierValue(PlayerData.MultiplierNames.SellMultiplier);

        StaticVariables.growthMultiplier = LogMultiplierCurve.Evaluate(growthLevel);
        StaticVariables.passiveGrowthMultiplier = LogMultiplierCurve.Evaluate(passiveGrowthLevel);
        StaticVariables.sellMultiplier = LogMultiplierCurve.Evaluate(sellLevel);
    }
    private void UpdateText() {
        PlayerData data = LoadDataOnStart.CurrentData;
        //Displaying current money
        moneyText.text = "Bamboo Bucks: " + ((int)data.money).ToString();

        //Displaing current upgrades
        growthLevelText.text = "Level: " + (int)data.GetMultiplierValue(PlayerData.MultiplierNames.GrowthMultiplier) + "<br>Current Multiplier: " + StaticVariables.growthMultiplier.ToString("F1");
        passiveGrowthLevelText.text = "Level: " + (int)data.GetMultiplierValue(PlayerData.MultiplierNames.PassiveGrowthMultiplier) + "<br>Current Multiplier: " + StaticVariables.passiveGrowthMultiplier.ToString("F1");
        sellLevelText.text = "Level: " + (int)data.GetMultiplierValue(PlayerData.MultiplierNames.SellMultiplier) + "<br>Current Multiplier: " + StaticVariables.sellMultiplier.ToString("F1");

        //Displaying upgrade costs
        growthLevelUpgradeText.text = growthPrice + " Bamboo Bucks";
        passiveGrowthLevelUpgradeText.text = passiveGrowthPrice + " Bamboo Bucks";
        sellLevelUpgradeText.text = sellPrice + " Bamboo Bucks";
    }
    private void UpdatePrices() {
        growthPrice = (int)Cost((int)LoadDataOnStart.CurrentData.GetMultiplierValue(PlayerData.MultiplierNames.GrowthMultiplier));
        passiveGrowthPrice = (int)Cost((int)LoadDataOnStart.CurrentData.GetMultiplierValue(PlayerData.MultiplierNames.PassiveGrowthMultiplier));
        sellPrice = (int)Cost((int)LoadDataOnStart.CurrentData.GetMultiplierValue(PlayerData.MultiplierNames.SellMultiplier));
    }
    private float Cost(int level, float baseCost = 300f, float growth = 1.05f, float quad = 5f) {
        level = Mathf.Max(0, level);

        float expPart = baseCost * Mathf.Pow(growth, level);
        float quadPart = quad * level * level;

        return expPart + quadPart;
    }

    private void SetWhatPlayerCanBuy() {
        PlayerData data = LoadDataOnStart.CurrentData;
        canBuyGrowthUpgrade = data.money >= growthPrice;
        canBuyPassiveGrowthUpgrade = data.money >= passiveGrowthPrice;
        canBuySellUpgrade = data.money >= sellPrice;
    }
}
public static class LogMultiplierCurve {
    public static float Evaluate(int level, float cap = 10f, int softCapLevel = 50) {
        level = Mathf.Max(0, level);

        float numerator = Mathf.Log(1f + level);
        float denominator = Mathf.Log(1f + softCapLevel);

        float t = numerator / denominator;
        t = Mathf.Clamp01(t);

        return 1f + (cap - 1f) * t;
    }
}
