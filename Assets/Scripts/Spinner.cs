using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spinner : MonoBehaviour
{
    // Spinner instance
    public static Spinner Instance { get; private set; }

    // Spinner pie objects container
    public Transform Container;

    // Spinner rotate animation duration. This value setted 5 as default. You can change on inspector
    public float AnimationDuration = 5f;

    // This variable declare for easing animation. You can change on inspector
    public float EaseMultiplier = 0.2f;

    // Spinner start action for handle when spinner starts to rotate
    public Action Started;

    // Spinner end action for handle when spinner ends to rotare
    public Action<Reward> Ended;

    // Spin now button
    public Button SpinButton;

    // This list variable declare for use later if you need it. Storing Pie game objects
    [HideInInspector]
    public List<GameObject> PieObjects = new List<GameObject>();

    // This variable declare for understanding to this spinner object created for first time or not
    [HideInInspector]
    public bool isFirst = false;

    // Local spinned data fetched from server / dummy
    private SpinnerWheel _data;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    // Set local spinner data
    public void SetData(SpinnerWheel data)
    {
        _data = data;

        // Load pie object prefab from resource dynamicly
        var pieRefObj = Resources.Load<GameObject>("PieObject");

        // Create pie objects depends on rewards data
        for (int i = 0; i < _data.Rewards.Count; i++)
        {
            var obj = Instantiate(pieRefObj, Container.transform);
            // set pie local data
            obj.GetComponent<PieObject>().SetData(_data.Rewards[i], _data.Rewards.Count, i);
            PieObjects.Add(obj);
        }
    }

    public void Spin()
    {
        // if local data is return do not spin
        if (_data == null) return;

        // call started action
        if (Started != null)
        {
            Started();
        }

        // start to spin wheel
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        // get euler angle for pie object
        Vector3 startRotation = Container.eulerAngles;

        // calculate angle for each pies
        float pieAngle = (360f / _data.Rewards.Count);

        // calculate target angle for find end rotation
        float targetAngle = (pieAngle * (_data.Reward + 1)) + (pieAngle / 2);

        // calculate end rotation
        float endRotation = startRotation.z + targetAngle;
        float t = 0.0f;

        // do jobs until end of animation duration
        while (t < AnimationDuration)
        {
            t += Time.deltaTime;

            // rotate container depends on time with easing function. 
            float zRotation = EaseOutBack(startRotation.z, endRotation, t / AnimationDuration);
            Container.eulerAngles = new Vector3(startRotation.x, startRotation.y, zRotation);

            yield return null;
        }

        // call end function when spinner is stop
        if (Ended != null)
        {
            SpinButton.gameObject.SetActive(false);
            Ended(_data.Rewards[_data.Reward]);
        }
    }

    // Standart easeout funcion for make spin animation more realistic
    public float EaseOutBack(float s, float e, float v)
    {
        e -= s;
        v = (v) - 1;
        return e * ((v) * v * ((EaseMultiplier + 1) * v + EaseMultiplier) + 1) + s;
    }
}
