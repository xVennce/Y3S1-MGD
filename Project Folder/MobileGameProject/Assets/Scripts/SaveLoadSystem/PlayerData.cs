using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData {
    public float globalAudio;
    public float bgmAudio;
    public bool toggleGlobalAudio;
    public bool toggleBgmAudio;

    public float plantGrowthStage;
    public float money;

    public List<Multipliers> multipliers = new List<Multipliers>();

    public PlayerData() {
        SetDefaultMultipliers();
    }
    private void SetDefaultMultipliers() {
        multipliers = new List<Multipliers> {
            new Multipliers {
                MultiplierName = "PassiveGrowthMultiplier",
                MultiplierValue = 1f
            },
            new Multipliers {
                MultiplierName = "SellMultiplier",
                MultiplierValue = 1f
            },
            new Multipliers {
                MultiplierName = "GrowthMultiplier",
                MultiplierValue = 1f
            }
        };
    }
    public void ValidateData() {
        if (multipliers == null || multipliers.Count == 0) {
            SetDefaultMultipliers();
        }
        CheckMultiplier("PassiveGrowthMultiplier", 1f);
        CheckMultiplier("SellMultiplier", 1f);
        CheckMultiplier("GrowthMultiplier", 1f);
    }

    public float GetMultiplierValue(string name) {
        Multipliers existing = multipliers.Find(m => m.MultiplierName == name);
        if (existing != null) {
            return existing.MultiplierValue;
        }
        return 1f; // Default value if not found
    }
    public void SetMultiplierValue(string name, float value) {
        Multipliers existing = multipliers.Find(m => m.MultiplierName == name);
        if (existing != null) {
            existing.MultiplierValue = value;
        }
        else {
            multipliers.Add(new Multipliers {
                MultiplierName = name,
                MultiplierValue = value
            });
        }
    }
    private void CheckMultiplier(string name, float defaultValue) {
        Multipliers existing = multipliers.Find(m => m.MultiplierName == name);
        if (existing == null) { 
            multipliers.Add(new Multipliers {
                MultiplierName = name,
                MultiplierValue = defaultValue
            });
        }
    }
    public static class MultiplierNames {
        public const string PassiveGrowthMultiplier = "PassiveGrowthMultiplier";
        public const string SellMultiplier = "SellMultiplier";
        public const string GrowthMultiplier = "GrowthMultiplier";
    }
}
[System.Serializable]
public class Multipliers {
    public string MultiplierName;
    public float MultiplierValue;
}