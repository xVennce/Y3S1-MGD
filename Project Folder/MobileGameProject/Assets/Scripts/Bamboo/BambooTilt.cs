using UnityEngine;
using UnityEngine.InputSystem;

public class BambooTilt : MonoBehaviour {
    [Header("Bamboo Sections (bottom to top)")]
    [SerializeField] private Transform section0;
    [SerializeField] private Transform section1;
    [SerializeField] private Transform section2;

    [Header("Sway Settings")]
    [SerializeField] private float maxSwayDegrees = 12f;
    [SerializeField] private float responsiveness = 8f;
    [SerializeField] private float deadZone = 0.03f;

    private Vector2 smoothedTilt;
    private Quaternion baseRot0, baseRot1, baseRot2;

    private void Awake() {
        baseRot0 = section0.localRotation;
        baseRot1 = section1.localRotation;
        baseRot2 = section2.localRotation;
    }
    private void OnEnable() {
        InputSystem.EnableDevice(Accelerometer.current);
    }
    private void OnDisable() {
        InputSystem.DisableDevice(Accelerometer.current);
    }
    private void Update() {
        Vector3 tilt = Accelerometer.current.acceleration.ReadValue();

        float tx = tilt.x;
        float tz = tilt.z;

        //Reducing jitter from accelerometer input by applying a deadzone
        Vector2 rawTilt = new Vector2(tx, tz);
        if (rawTilt.magnitude < deadZone) rawTilt = Vector2.zero;

        smoothedTilt = Vector2.Lerp(smoothedTilt, rawTilt, 1f - Mathf.Exp(-responsiveness * Time.deltaTime));


        Vector2 targetAngles = Vector2.ClampMagnitude(smoothedTilt, 1f) * maxSwayDegrees;

        ApplySectionSway(section0, baseRot0, targetAngles, 0.20f);
        ApplySectionSway(section1, baseRot1, targetAngles, 0.60f);
        ApplySectionSway(section2, baseRot2, targetAngles, 1.00f);
    }

    private void ApplySectionSway(Transform section, Quaternion baseRot, Vector2 angles, float weight) {
        float bendX = -angles.y * weight;
        float bendZ = -angles.x * weight;

        Quaternion bend = Quaternion.Euler(bendX, 0f, bendZ);
        section.localRotation = baseRot * bend;
    }
}
