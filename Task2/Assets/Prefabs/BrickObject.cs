using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BrickObject : MonoBehaviour
{
    private SpriteRenderer SpriteRenderer;
    private BrickData _data;

    public GameObject ExplodeObj;

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

        if (_data.Type == "E")
        {
            gameObject.tag = "Empty";

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

                Explode();

                Destroy(gameObject);
            }
            else
            {
                collision.gameObject.GetComponent<Ball>().SetColor(_data.Type);
            }
        }
    }

    void Explode()
    {
        var o = Instantiate(ExplodeObj);

        var ps = o.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule psmain = ps.main;
        psmain.startColor = _data.Color.ToColor();

        var pos = gameObject.transform.position;
        var scale = gameObject.GetComponent<SpriteRenderer>().bounds.size;

        o.transform.position = new Vector2(pos.x + (scale.x / 2), pos.y - (scale.y / 2));
    }
}
