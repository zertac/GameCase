using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance;
    public ScreenBase CurrentScreen;

    public GameObject LobbyScreen;
    public GameObject GameScreen;
    public GameObject ResultScreen;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        Application.targetFrameRate = 60;
        Application.runInBackground = true;

        OpenScreen(Screens.LOBBY);
    }

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
