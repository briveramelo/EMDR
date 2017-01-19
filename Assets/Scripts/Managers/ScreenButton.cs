using UnityEngine;
using System.Collections;

public class ScreenButton : MonoBehaviour {

    [SerializeField] GameScreen screenToTakeTo;

    void OnMouseDown() {
        ScreenManager.LoadScreen(screenToTakeTo);
    }
}
