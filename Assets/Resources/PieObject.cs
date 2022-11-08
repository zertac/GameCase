using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PieObject : MonoBehaviour
{
    public Image PieImage;
    public RectTransform PieRectTransform;
    public TextMeshProUGUI Text;
    public RectTransform ContentContainer;

    private Reward _data { get; set; }
    // Start is called before the first frame update
    void Awake()
    {
        PieImage = GetComponent<Image>();
        PieRectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Set local pie data
    public void SetData(Reward data, int count, int index)
    {
        // calculate image mask fill amount
        var fillAmount = (360f / count) / 360f;
        // calculate angle of rotation
        var angle = (360f / count) * (index + 1);
        // calculate angle of content text
        var textAngle = (360f / count) / 2;

        _data = data;

        Text.text = _data.Title;

        PieImage.fillAmount = fillAmount;

        // set custom pie color depending to retrived data
        UnityEngine.Color color;

        if (ColorUtility.TryParseHtmlString(_data.Color, out color))
        {
            PieImage.color = color;
        }

        // rotate pie object
        PieRectTransform.Rotate(Vector3.back, angle);

        // rotate pie content
        ContentContainer.Rotate(Vector3.back, textAngle);
    }
}
