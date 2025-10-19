using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;


public class TextAgent : MonoBehaviour {

    [Header("Text Components to Change Font")]
    [SerializeField] private TMP_Text[] Font;

    [Header("Change Interval (seconds)")]
    [SerializeField] private float ChangeInterval = 5f;

    private string StringInput;

    private bool IsWaiting = true;

    private void Start() {
        StringInput = Font[4].text;
        SetTextString();
        Font[4].gameObject.SetActive(false);
        int RanNumber = Random.Range(0, Font.Length-1);
        SetActive(RanNumber);
    }
    private void Update() {
        if (IsWaiting == true) {
            IsWaiting = false;
            int RanNumber = Random.Range(0, Font.Length-1);
            SetActive(RanNumber);
            StartCoroutine(WaitForXSecond(ChangeInterval));
        }
    }
    private void SetTextString() {
        foreach (TMP_Text TextComponent in Font) {
            TextComponent.text = StringInput;
        }
    }
    private void SetActive(int RanNumber) { 
        foreach (TMP_Text TextComponent in Font) {
            TextComponent.gameObject.SetActive(false);
        }
        Font[RanNumber].gameObject.SetActive(true);
    }

    private IEnumerator WaitForXSecond(float time) { 
        yield return new WaitForSeconds(time);
        IsWaiting = true;
    }

}
