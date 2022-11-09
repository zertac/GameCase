using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    // screen manager instance
    public static ScreenManager Instance;
    // current active screen in scene
    public ScreenBase CurrentScreen;

    // screen prefabs
    public GameObject LobbyScreen;
    public GameObject GameScreen;
    public GameObject ResultScreen;

    // set defaults
    void Start()
    {
        Instance = this;

        // limit application frame rate to 60.
        Application.targetFrameRate = 60;
        // game will continue even is not focused
        Application.runInBackground = true;
        // set screen resolution
        Screen.SetResolution(1080, 1920, true);
        // open initial lobby screen
        OpenScreen(Screens.LOBBY);
    }

    // open screen function
    public void OpenScreen(Screens screen)
    {
        if (CurrentScreen != null)
        {
            CurrentScreen.Close();
        }

        if (screen == Screens.LOBBY)
        {
            var o = Instantiate(LobbyScreen);
            CurrentScreen = o.GetComponent<ScreenBase>();
        }
        else if (screen == Screens.GAME)
        {
            var o = Instantiate(GameScreen);
            CurrentScreen = o.GetComponent<ScreenBase>();
        }
        else if (screen == Screens.RESULT)
        {
            var o = Instantiate(ResultScreen);
            CurrentScreen = o.GetComponent<ScreenBase>();
        }
    }
}
