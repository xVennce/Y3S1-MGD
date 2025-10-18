using UnityEngine;

public class VibrationAgent: MonoBehaviour {
    public static void VibrateDevice() {
        Debug.Log("VibrationPerformed");
        Handheld.Vibrate();
    }
}
