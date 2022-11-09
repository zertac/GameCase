using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MoveSpeed = 1f;
    public Direction PlayerDirection;
    public SpriteRenderer SpriteRenderer;

    private float OrthoWidth;
    private float lastX = 0f;

    public enum Direction
    {
        Idle,
        Left,
        Right
    }

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();

        OrthoWidth = Camera.main.orthographicSize * Camera.main.aspect;
        lastX = gameObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("Horizontal");

        gameObject.transform.Translate(Vector2.right * x * MoveSpeed);

        TrimPosition();
    }

    void TrimPosition()
    {
        if (lastX > gameObject.transform.position.x)
        {
            PlayerDirection = Direction.Left;
        }
        else if (lastX < gameObject.transform.position.x)
        {
            PlayerDirection = Direction.Right;
            lastX = gameObject.transform.position.x;
        }
        else
        {
            PlayerDirection = Direction.Idle;
        }

        lastX = gameObject.transform.position.x;

        var radius = gameObject.transform.localScale.x / 2;

        if (lastX - radius < -OrthoWidth)
        {
            gameObject.transform.position = new Vector2(-OrthoWidth + radius, gameObject.transform.position.y);
        }
        else if (lastX + radius > OrthoWidth)
        {
            gameObject.transform.position = new Vector2(OrthoWidth - radius, gameObject.transform.position.y);
        }

        //gameObject.transform.position = new Vector2(Mathf.Max(lastX, -OrthoWidth), gameObject.transform.position.y);

    }
}
