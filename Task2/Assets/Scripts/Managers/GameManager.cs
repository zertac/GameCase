using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // player current level
    public int CurrentLevel = 0;
    // game manager instance
    public static GameManager Instance;
    // current level data
    public LevelData LevelData;
    // player total score
    public int TotalScore;

    void Awake()
    {
        Instance = this;
    }

    // subscribe to game events
    void Start()
    {
        EventManager.Instance.Subscribe<LevelData>(ActionTypes.LOAD_LEVEL, OnLevelLoaded, this.gameObject);
        EventManager.Instance.Subscribe<Score>(ActionTypes.BREAK_BRICK, OnScoreClaim, this.gameObject);
        EventManager.Instance.Subscribe<Score>(ActionTypes.DEAD, OnDead, this.gameObject);
        EventManager.Instance.Subscribe<Score>(ActionTypes.GAME_OVER, OnGameOver, this.gameObject);
        EventManager.Instance.Subscribe<Score>(ActionTypes.LEVEL_COMPLETE, OnLevelCompleted, this.gameObject);
    }

    // when level loaded then set level data and open game screen
    private void OnLevelLoaded(LevelData level)
    {
        LevelData = level;
        ScreenManager.Instance.OpenScreen(Screens.GAME);
    }

    // when brick is breaked and claimed score
    void OnScoreClaim(Score score)
    {
        TotalScore += score.Value;
        CheckLevelCompleted();
    }

    // when game finished
    void OnGameOver(Score score)
    {
        // reset values
        CurrentLevel = 0;
        TotalScore = 0;

        // save score
        PlayerManager.Instance.SaveScore(score.Value);
        // show result screen
        ScreenManager.Instance.OpenScreen(Screens.RESULT);
        // set result screen data
        ResultScreen.Instance.SetData(score.Value);
    }

    // when player lose ball
    void OnDead(Score score)
    {
        // create new ball
        BrickPanel.Instance.CreateBall();
    }

    // whrn level completed
    void OnLevelCompleted(Score score)
    {
        // start new level of game
       StartGame();
    }

    // check all bricks are destroyed and game finished
    void CheckLevelCompleted()
    {
        // find count of bricks object in scene with tag
        var brickCount = GameObject.FindGameObjectsWithTag("Brick").Count();

        if (brickCount == 0)
        {
            EventManager.Instance.Fire<Score>(ActionTypes.LEVEL_COMPLETE, new Score { Value = TotalScore });
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        GetNextLevel();
    }

    // get next level
    void GetNextLevel()
    {
        CurrentLevel++;

        LevelHelper.LoadLevel(CurrentLevel);
    }
}
