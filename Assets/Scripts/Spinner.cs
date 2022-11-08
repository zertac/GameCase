using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public static Spinner Instance { get; private set; }
    public Transform Container;
    public float AnimationDuration = 5f;
    public float EaseMultiplier = 0.2f;
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

    void OnGUI()
    {
        if (GUILayout.Button("SPIN"))
        {
            Spin();
        }
    }
    public void SetData(List<Pie> data)
    {
        _data = data;

        var pieRefObj = Resources.Load<GameObject>("PieObject");

        for (int i = 0; i < _data.Count; i++)
        {
            var obj = Instantiate(pieRefObj, Container.transform);
            obj.GetComponent<PieObject>().SetData(_data[i], _data.Count, i);
        }
    }

    public void Spin()
    {
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        Vector3 startRotation = Container.eulerAngles;
        float endRotation = startRotation.z + 810.0f;
        float t = 0.0f;

        while (t < AnimationDuration)
        {
            t += Time.deltaTime;

            float zRotation = EaseOutBack(startRotation.z, endRotation, t / AnimationDuration);
            Container.eulerAngles = new Vector3(startRotation.x, startRotation.y, zRotation);


            yield return null;
        }
    }

    public float EaseOutBack(float s, float e, float v)
    {
        e -= s;
        v = (v) - 1;
        return e * ((v) * v * ((EaseMultiplier + 1) * v + EaseMultiplier) + 1) + s;
    }
}
