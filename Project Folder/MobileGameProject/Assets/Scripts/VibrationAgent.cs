using UnityEngine;
using CandyCoded;
using CandyCoded.HapticFeedback;

public class VibrationAgent: MonoBehaviour {
    public static void VibrateDevice() {
        Debug.Log("VibrationPerformed");
        Handheld.Vibrate();
    }
    public static void LightHapticFeedback() {
        Debug.Log("Light Haptic Feedback Performed");
        HapticFeedback.LightFeedback();
    }
    public static void MediumHapticFeedback() {
        Debug.Log("Medium Haptic Feedback Performed");
        HapticFeedback.MediumFeedback();
    }
    public static void HeavyHapticFeedback() {
        Debug.Log("Heavy Haptic Feedback Performed");
        HapticFeedback.HeavyFeedback();
    }
}
