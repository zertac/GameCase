using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPanel : MonoBehaviour
{
    // Brick Panel instance
    public static BrickPanel Instance;

    // Ball object reference
    public GameObject Ball;

    // Brick object reference
    public GameObject BrickObj;
    
    // Local level data
    private LevelData _data;

    private void Awake()
    {
        Instance = this;
    }

    // Create ball on start
    void Start()
    {
        CreateBall();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // create ball function
    public void CreateBall()
    {
        Instantiate(Ball, gameObject.transform);
    }

    // set local data of levet
    public void SetData(LevelData data)
    {
        _data = data;
        // find ortopgraphic width for locate bricks to correct position
        float OrthoWidth = Camera.main.orthographicSize * Camera.main.aspect;
        // set initial position top/left
        var x = -OrthoWidth;
        var y = Camera.main.orthographicSize;
        var index = -1;

        for (int i = 0; i < data.Row; i++)
        {
            GameObject o = null;

            for (int j = 0; j < data.Col; j++)
            {
                index++;
                // create brick object and set position
                o = Instantiate(BrickObj, gameObject.transform);
                o.transform.position = new Vector3(x, y, 1f);
                // set brick data
                o.GetComponent<BrickObject>().SetData(data.Bricks[index]);
                // iterate x position for next brick
                x += o.GetComponent<SpriteRenderer>().bounds.size.x;
            }

            // set x position to initial position for next row
            x = -OrthoWidth;
            // iterate y position for next brick
            y -= o.GetComponent<SpriteRenderer>().bounds.size.y;
        }

        // set level max life count
        PlayerManager.Instance.Data.Ball = _data.Ball;
    }
}
