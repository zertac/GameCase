using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.XR;

public class Ball : MonoBehaviour
{
    public BallData Data;
    public SpriteRenderer SpriteRenderer;
    public float BallSpeed = 4f;
    public bool IsPlaying = false;

    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        ChooseRandomColor();
    }

    // Use this for initialization
    void Start()
    {
        var directionX = Random.Range(0, 2) == 0 ? 1 : -1;
        var directionY = Random.Range(0, 2) == 0 ? 1 : -1;

        _rb.velocity = new Vector2(directionX * BallSpeed, directionY * BallSpeed);
        IsPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheclBallPosition();
    }

    void CheclBallPosition()
    {
        if (!IsPlaying) return;

        if (gameObject.transform.position.y < -Camera.main.orthographicSize)
        {
            IsPlaying = false;

            PlayerManager.Instance.Data.Ball--;

            if (PlayerManager.Instance.Data.Ball <= 0)
            {
                Debug.Log("Game over");
                EventManager.Instance.Fire<Score>(ActionTypes.GAME_OVER, new Score { Value = GameManager.Instance.TotalScore });
            }
            else
            {
                Debug.Log("Dead " + PlayerManager.Instance.Data.Ball);
                EventManager.Instance.Fire<Score>(ActionTypes.DEAD, new Score { Value = GameManager.Instance.TotalScore });
            }
        }
    }

    void ChooseRandomColor()
    {
        Data = new BallData();

        var min = 0;
        var max = GameManager.Instance.LevelData.Colors.Count - 1;
        var colorData = GameManager.Instance.LevelData.Colors.ElementAt(Random.Range(min, max));

        Data.Type = colorData.Key;

        SpriteRenderer.color = colorData.Value.ToColor();
    }

    public void SetColor(string type)
    {
        var colorData = GameManager.Instance.LevelData.Colors[type];

        Data.Type = type;

        SpriteRenderer.color = colorData.ToColor();
    }
}
