using UnityEngine;

public class ShakeDetector : MonoBehaviour {
    [Header("Shake Tuning")]
    [SerializeField] private float shakeThresholdG = 2.2f;
    [SerializeField] private float lowPassFilter = 10f;
    [SerializeField] private float cooldown = 5.0f;

    [Header("Leaf Falling Effect")]
    //[SerializeField] private LeafFallEffect leafFallEffect;
    [SerializeField] private ParticleSystem leafFallParticleSystem;

    private Vector3 smoothedAccel;
    private float nextAllowedTime;

    private void Awake() {
        smoothedAccel = Input.acceleration;
    }

    private void Update() {
        DetectShake();
    }

    private void DetectShake() {
        Vector3 accel = Input.acceleration;

        //This is a low-pass filter to remove gravity from the accelerometer data and smooth it out by interpolating between the previous smoothed value and the current acceleration
        float lerp = 1f - Mathf.Exp(-lowPassFilter * Time.deltaTime);
        smoothedAccel = Vector3.Lerp(smoothedAccel, accel, lerp);

        Vector3 delta = accel - smoothedAccel;
        float shakeStrength = delta.magnitude;

        if (Time.time < nextAllowedTime)
            return;

        if (shakeStrength >= shakeThresholdG) {
            nextAllowedTime = Time.time + cooldown;
            OnShake();
        }
    }

    private void OnShake() {
        Debug.Log("SHAKE DETECTED");
        VibrationAgent.HeavyHapticFeedback();
        leafFallParticleSystem.Play();
    }
}