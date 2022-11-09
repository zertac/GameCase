using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UIElements;

public class GameScreen : ScreenBase
{
    public GameObject LevelObject;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI LastScoreText;
    public TextMeshProUGUI BallText;

    void Awake()
    {
        EventManager.Instance.Subscribe<Score>(ActionTypes.BREAK_BRICK, OnScoreClaim, this.gameObject);
        EventManager.Instance.Subscribe<Score>(ActionTypes.DEAD, OnDead, this.gameObject);
        EventManager.Instance.Subscribe<Score>(ActionTypes.GAME_OVER, OnGameOver, this.gameObject);
    }

    void OnScoreClaim(Score score)
    {
        ScoreText.text = "Score : " + GameManager.Instance.TotalScore.ToString();
        LastScoreText.text = "Last Score : " + score.Value.ToString();
    }

    void OnGameOver(Score score)
    {
        BallText.text = "Ball : " + 0;
    }

    void OnDead(Score score)
    {
        BallText.text = "Ball : " + PlayerManager.Instance.Data.Ball.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        var o = Instantiate(LevelObject, gameObject.transform);
        o.GetComponent<BrickPanel>().SetData(GameManager.Instance.LevelData);

        ScoreText.text = "Score : " + GameManager.Instance.TotalScore.ToString();
        BallText.text = "Ball : " + PlayerManager.Instance.Data.Ball.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
