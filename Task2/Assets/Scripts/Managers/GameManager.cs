using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int CurrentLevel = 0;

    public static GameManager Instance;
    public LevelData LevelData;
    public int TotalScore;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        EventManager.Instance.Subscribe<LevelData>(ActionTypes.LOAD_LEVEL, OnLevelLoaded, this.gameObject);
        EventManager.Instance.Subscribe<Score>(ActionTypes.BREAK_BRICK, OnScoreClaim, this.gameObject);
        EventManager.Instance.Subscribe<Score>(ActionTypes.DEAD, OnDead, this.gameObject);
        EventManager.Instance.Subscribe<Score>(ActionTypes.GAME_OVER, OnGameOver, this.gameObject);
    }

    private void OnLevelLoaded(LevelData level)
    {
        LevelData = level;
        ScreenManager.Instance.OpenScreen(Screens.GAME);
    }

    void OnScoreClaim(Score score)
    {
        TotalScore += score.Value;
    }

    void OnGameOver(Score score)
    {
        PlayerManager.Instance.SaveScore(score.Value);
    }

    void OnDead(Score score)
    {
       BrickPanel.Instance.CreateBall();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        GetNextLevel();
    }

    void GetNextLevel()
    {
        CurrentLevel++;

        LevelHelper.LoadLevel(CurrentLevel);
    }
}
