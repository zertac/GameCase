using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BrickObject : MonoBehaviour
{
    private SpriteRenderer SpriteRenderer;
    private BrickData _data;

    void Awake()
    {
        SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetScale()
    {
        float width = GetScreenToWorldWidth;
        transform.localScale = Vector3.one * width;
    }

    public void SetData(BrickData data)
    {
        _data = data;

        if(_data.Type == "E")
        {
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(GetComponent<Collider2D>());
        }

        SetScale();

        SpriteRenderer.color = _data.Color.ToColor();
    }

    public float GetScreenToWorldWidth
    {
        get
        {
            Vector2 topRightCorner = new Vector2(1, 1);
            Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
            var width = edgeVector.x * 2;
            return width;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (collision.gameObject.GetComponent<Ball>().Data.Type == _data.Type)
            {
                EventManager.Instance.Fire<Score>(ActionTypes.BREAK_BRICK, new Score { Value = _data.Value });
                Destroy(gameObject);
            }
            else
            {
                collision.gameObject.GetComponent<Ball>().SetColor(_data.Type);
            }
        }
    }
}
