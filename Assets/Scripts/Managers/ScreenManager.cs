using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum GameScreen {
    Title=0,
    Clinician=1,
    Speak=2,
    Game=3
}

public class ScreenManager : MonoBehaviour {


    public static void LoadScreen(GameScreen newScreen) {
        SceneManager.LoadScene((int)newScreen);
    }


}
