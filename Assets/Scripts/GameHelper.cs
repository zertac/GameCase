using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHelper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CreateSpinner();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateSpinner()
    {
        var list = new List<Pie>();
        string[] colors = {"#CD3712", "#FFEA4D", "#2447CD", "#1FC352", "#BB33FF", "#F3764B", "#F376BE", "#FFEA4D", "#BB33FF", "#F3764B", "#F376BE", "#FFEA4D", "#BB33FF", "#F3764B", "#F376BE", "#FFEA4D", };

        for (int i = 0; i < 14; i++)
        {
            var pie = new Pie();
            pie.Title = "title " + i;
            pie.Value = i;
            pie.Color = colors[i];

            list.Add(pie);
        }

        Spinner.Instance.SetData(list);
    }
}
