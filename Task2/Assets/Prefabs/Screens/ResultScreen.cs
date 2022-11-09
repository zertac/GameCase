using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultScreen : ScreenBase
{
    // Object instance
    public static ResultScreen Instance;
    // Player score ui object
    public TextMeshProUGUI ScoreText;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // when click to main mennu button
    public void GoToMainMenu()
    {
        ScreenManager.Instance.OpenScreen(Screens.LOBBY);
    }

    // Set result screen data for show user
    public void SetData(int score)
    {
        ScoreText.text = "Your Score : " + score.ToString();
    }
}
