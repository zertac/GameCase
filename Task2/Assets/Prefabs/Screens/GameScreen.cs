using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UIElements;

public class GameScreen : ScreenBase
{
    // Game level object
    public GameObject LevelObject;
    // Player score text ui object
    public TextMeshProUGUI ScoreText;
    // Player last score ui object
    public TextMeshProUGUI LastScoreText;
    // Player life count (ball count) ui object
    public TextMeshProUGUI BallText;

    void Awake()
    {
        // Subscribe to game events
        EventManager.Instance.Subscribe<Score>(ActionTypes.BREAK_BRICK, OnScoreClaim, this.gameObject);
        EventManager.Instance.Subscribe<Score>(ActionTypes.DEAD, OnDead, this.gameObject);
        EventManager.Instance.Subscribe<Score>(ActionTypes.GAME_OVER, OnGameOver, this.gameObject);
    }

    // when brick is break and claim score
    void OnScoreClaim(Score score)
    {
        ScoreText.text = "Score : " + GameManager.Instance.TotalScore.ToString();
        LastScoreText.text = "Last Score : " + score.Value.ToString();
    }

    // when user balls ended
    void OnGameOver(Score score)
    {
        BallText.text = "Ball : " + 0;
    }

    // user lose ball
    void OnDead(Score score)
    {
        BallText.text = "Ball : " + PlayerManager.Instance.Data.Ball.ToString();
    }

    void Start()
    {
        // create level and set data
        var o = Instantiate(LevelObject, gameObject.transform);
        o.GetComponent<BrickPanel>().SetData(GameManager.Instance.LevelData);

        // set default ui content
        ScoreText.text = "Score : " + GameManager.Instance.TotalScore.ToString();
        BallText.text = "Ball : " + PlayerManager.Instance.Data.Ball.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
