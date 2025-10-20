using UnityEngine;

public class DeleteLoadingScreen : MonoBehaviour {
    void Start() {
        GameObject loadingScreen = GameObject.FindWithTag("LoadingScreen");
        if (loadingScreen != null) {
            Destroy(loadingScreen);
        }
    }
}
