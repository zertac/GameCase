using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.XR;

public class Ball : MonoBehaviour
{
    // Ball data
    public BallData Data;
    // Current ball sprite renderer for customize
    public SpriteRenderer SpriteRenderer;
    // Ball move speed
    public float BallSpeed = 4f;
    // Ball state
    public bool IsPlaying = false;
    // Current ball rigidbody
    private Rigidbody2D _rb;

    void Awake()
    {
        // get instance of rigidbody
        _rb = gameObject.GetComponent<Rigidbody2D>();
        // get instance of sprite renderer
        SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        // choose initial random ball color
        ChooseRandomColor();
    }

    void Start()
    {
        // Randomize initial force direction of ball
        var directionX = Random.Range(0, 2) == 0 ? 1 : -1;
        var directionY = Random.Range(0, 2) == 0 ? 1 : -1;

        // add velocity
        _rb.velocity = new Vector2(directionX * BallSpeed, directionY * BallSpeed);

        // set state of ball
        IsPlaying = true;
    }

   
    void Update()
    {
        CheckBallPosition();
    }

    // Check ball position is in screen rectangle ?
    void CheckBallPosition()
    {
        if (!IsPlaying) return;

        // check ball position is out of screen bottom
        if (gameObject.transform.position.y < -Camera.main.orthographicSize)
        {
            IsPlaying = false;

            // decrease ball count
            PlayerManager.Instance.Data.Ball--;

            // if balls are finised then game over
            if (PlayerManager.Instance.Data.Ball <= 0)
            {
                Debug.Log("Game over");
                EventManager.Instance.Fire<Score>(ActionTypes.GAME_OVER, new Score { Value = GameManager.Instance.TotalScore });
            }
            else
            {
                // fire ball lose event
                Debug.Log("Dead " + PlayerManager.Instance.Data.Ball);
                EventManager.Instance.Fire<Score>(ActionTypes.DEAD, new Score { Value = GameManager.Instance.TotalScore });
            }
        }
    }

    // choose random color
    void ChooseRandomColor()
    {
        Data = new BallData();

        var min = 0;
        var max = GameManager.Instance.LevelData.Colors.Count - 1;
        var colorData = GameManager.Instance.LevelData.Colors.ElementAt(Random.Range(min, max));

        Data.Type = colorData.Key;

        SpriteRenderer.color = colorData.Value.ToColor();
    }

    // set color of ball
    public void SetColor(string type)
    {
        var colorData = GameManager.Instance.LevelData.Colors[type];

        Data.Type = type;

        SpriteRenderer.color = colorData.ToColor();
    }
}
