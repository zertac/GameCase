using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // player move speed
    public float MoveSpeed = 1f;
    // player move multipler on mobile devices
    public float MobileTouchSensitivty = 0.1f;
    // current direction of player
    public Direction PlayerDirection;
    // player sprite renderer
    public SpriteRenderer SpriteRenderer;
    // camera ortographic width
    private float OrthoWidth;
    // last x position of player
    private float lastX = 0f;
    // player direction types
    public enum Direction
    {
        Idle,
        Left,
        Right
    }

    // set defaults on start
    void Start()
    {
        // get sprite renderer
        SpriteRenderer = GetComponent<SpriteRenderer>();

        // calculate ortographic width
        OrthoWidth = Camera.main.orthographicSize * Camera.main.aspect;

        // set last x position as initial
        lastX = gameObject.transform.position.x;
    }

    // get input axis for move player
    void Update()
    {
#if UNITY_EDITOR
        var x = Input.GetAxis("Horizontal");

        gameObject.transform.Translate(Vector2.right * x * MoveSpeed);
#else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            var delta = Input.GetTouch(0).deltaPosition;
            gameObject.transform.Translate(Vector2.right * delta * MoveSpeed * MobileTouchSensitivty);
        }
#endif
        // limit player movement between screen view for left and right of screen
        ThresholdPosition();
    }

    // threshold function
    void ThresholdPosition()
    {
        // dedect direction of player if we required for custom ball force or any animation
        if (lastX > gameObject.transform.position.x)
        {
            PlayerDirection = Direction.Left;
        }
        else if (lastX < gameObject.transform.position.x)
        {
            PlayerDirection = Direction.Right;
        }
        else
        {
            PlayerDirection = Direction.Idle;
        }

        // set last x position of player
        lastX = gameObject.transform.position.x;

        // find radius of player width
        var radius = gameObject.transform.localScale.x / 2;

        // limit for screen left side
        if (lastX - radius < -OrthoWidth)
        {
            gameObject.transform.position = new Vector2(-OrthoWidth + radius, gameObject.transform.position.y);
        }
        else if (lastX + radius > OrthoWidth) // limit for screen right side
        {
            gameObject.transform.position = new Vector2(OrthoWidth - radius, gameObject.transform.position.y);
        }
    }
}
