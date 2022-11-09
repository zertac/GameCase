using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GameScreen : ScreenBase
{
    public GameObject LevelObject;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI LastScoreText;

    void Awake()
    {
        EventManager.Instance.Subscribe<Score>(ActionTypes.BREAK_BRICK, OnScoreClaim, this.gameObject);
    }

    void OnScoreClaim(Score score)
    {
        ScoreText.text = "Score : " + GameManager.Instance.TotalScore.ToString();
        LastScoreText.text = "Last Score : " + score.Value.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        var o = Instantiate(LevelObject, gameObject.transform);
        o.GetComponent<BrickPanel>().SetData(GameManager.Instance.LevelData);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
