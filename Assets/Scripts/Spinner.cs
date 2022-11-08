using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public static Spinner Instance { get; private set; }

    private List<Pie> _data;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetData(List<Pie> data)
    {
        _data = data;

        var pieRefObj = Resources.Load<GameObject>("PieObject");

        for (int i = 0; i < _data.Count; i++)
        {
            var obj = Instantiate(pieRefObj, GameObject.Find("Canvas").transform);
            obj.GetComponent<PieObject>().SetData(_data[i], _data.Count, i);
        }
    }
}
