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
        _rb.velocity = new Vector2(2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        CheclBallPosition();
    }

    void CheclBallPosition()
    {
        if (gameObject.transform.position.y < -Camera.main.orthographicSize)
        {
            Debug.Log("Game over");
            //Destroy(gameObject);
        }
    }

    void ChooseRandomColor()
    {
        Data = new BallData();

        var min = 0;
        var max = GameManager.Instance.LevelData.Colors.Count - 1;
        var colorData = GameManager.Instance.LevelData.Colors.ElementAt(Random.Range(min, max));

        Data.Type = colorData.Key;

        Color color;
        if (ColorUtility.TryParseHtmlString(colorData.Value, out color))
        {
            SpriteRenderer.color = color;
        }
    }

    public void SetColor(string type)
    {
        var colorData = GameManager.Instance.LevelData.Colors[type];

        Data.Type = type;

        Color color;
        if (ColorUtility.TryParseHtmlString(colorData, out color))
        {
            SpriteRenderer.color = color;
        }
    }      
}
