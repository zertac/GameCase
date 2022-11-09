using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPanel : MonoBehaviour
{
    public GameObject BrickObj;
    private LevelData _data;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetData(LevelData data)
    {
        _data = data;
        float OrthoWidth = Camera.main.orthographicSize * Camera.main.aspect;
        var x = -OrthoWidth;
        var y = Camera.main.orthographicSize;
        var index = -1;

        for (int i = 0; i < data.Row; i++)
        {
            GameObject o = null;

            for (int j = 0; j < data.Col; j++)
            {
                index++;
                o = Instantiate(BrickObj, gameObject.transform);
                o.transform.position = new Vector3(x, y, 1f);
                o.GetComponent<BrickObject>().SetData(data.Bricks[index]);
                x += o.GetComponent<SpriteRenderer>().bounds.size.x;
            }

            x = -OrthoWidth;
            y -= o.GetComponent<SpriteRenderer>().bounds.size.y;
        }
    }
}
