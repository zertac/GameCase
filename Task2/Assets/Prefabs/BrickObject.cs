using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BrickObject : MonoBehaviour
{
    // Current brick sprite renderer
    private SpriteRenderer SpriteRenderer;

    // Local brick dara
    private BrickData _data;

    // Explode particle animation reference object
    public GameObject ExplodeObj;

    void Awake()
    {
        // get current brick sprite rendered
        SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // set scale of brick depending to screen width for make responsive design
    public void SetScale()
    {
        float width = GetScreenToWorldWidth;
        transform.localScale = Vector3.one * width;
    }

    // set data of brick from outside
    public void SetData(BrickData data)
    {
        _data = data;

        // if brick is empty then destroy unused components
        if (_data.Type == "E")
        {
            gameObject.tag = "Empty";

            Destroy(GetComponent<SpriteRenderer>());
            Destroy(GetComponent<Collider2D>());
        }

        // set scale as responsive
        SetScale();

        // set brick color depending to level pattern
        SpriteRenderer.color = _data.Color.ToColor();
    }

    // get screen world scale width for resize bricks
    public float GetScreenToWorldWidth
    {
        get
        {
            Vector2 topRight = Vector2.one;
            Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRight);
            var width = edgeVector.x * 2;
            return width;
        }
    }

    // if ball collide to me
    void OnCollisionEnter2D(Collision2D collision)
    {
        // if collided object is ball then
        if (collision.gameObject.tag == "Ball")
        {
            // if ball and my color is equal then destroy myself
            if (collision.gameObject.GetComponent<Ball>().Data.Type == _data.Type)
            {
                EventManager.Instance.Fire<Score>(ActionTypes.BREAK_BRICK, new Score { Value = _data.Value });

                // animate explosion particle effect
                Explode();

                // kill myself
                Destroy(gameObject);
            }
            else // if ball color is not equal to my color then change ball color and play sfx
            {
                collision.gameObject.GetComponent<Ball>().SetColor(_data.Type);
                SfxHelper.Instance.Play("collide");
            }
        }
    }

    // animate explore animation and set color depending to brick color
    void Explode()
    {
        var o = Instantiate(ExplodeObj);
        SfxHelper.Instance.Play("explode");

        // get particle system and set color
        var ps = o.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule psmain = ps.main;
        psmain.startColor = _data.Color.ToColor();

        var pos = gameObject.transform.position;
        var scale = gameObject.GetComponent<SpriteRenderer>().bounds.size;

        // find brick center position in world and set position of particle object
        o.transform.position = new Vector2(pos.x + (scale.x / 2), pos.y - (scale.y / 2));
    }
}
