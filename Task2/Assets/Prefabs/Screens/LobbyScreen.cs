using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyScreen : ScreenBase
{
    public TextMeshProUGUI HighestScoreText;
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

    public void PlayNow()
    {
        GameManager.Instance.StartGame();
    }

    public void SetDefaults()
    {
        HighestScoreText.text = "Higest Score : " + PlayerManager.Instance.Data.HighestScore.ToString();
        LastScoreText.text = "Last Score : " + PlayerManager.Instance.Data.LastScore.ToString();
    }
}
