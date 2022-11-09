using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyScreen : ScreenBase
{
    // Player highest score ui object
    public TextMeshProUGUI HighestScoreText;
    // Player last score ui object
    public TextMeshProUGUI LastScoreText;

    // Start is called before the first frame update
    void Start()
    {
        SetDefaults();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // When click to play now button
    public void PlayNow()
    {
        GameManager.Instance.StartGame();
    }

    // Set default ui content
    public void SetDefaults()
    {
        HighestScoreText.text = "Higest Score : " + PlayerManager.Instance.Data.HighestScore.ToString();
        LastScoreText.text = "Last Score : " + PlayerManager.Instance.Data.LastScore.ToString();
    }
}
