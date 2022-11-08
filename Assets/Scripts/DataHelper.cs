using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataHelper
{
    // Simple data fetch layer for retrive data from server (WebApi, WebSocket etc)
    public static SpinnerWheel GetSpinner()
    {
        var jsonString = DataSimulator.GetSpinnerData();
        var data = JsonUtility.FromJson<SpinnerWheel>(jsonString);
        return data;
    }
}
