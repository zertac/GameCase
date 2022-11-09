using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultScreen : ScreenBase
{
    public static ResultScreen Instance;

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

    public void GoToMainMenu()
    {
        ScreenManager.Instance.OpenScreen(Screens.LOBBY);
    }

    public void SetData(int score)
    {
        ScoreText.text = "Your Score : " + score.ToString();
    }
}
