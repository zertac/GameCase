using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class DataSimulator
{
    /// <summary>
    /// This class created for fake data
    /// Data loading from static resource as json text
    /// </summary>
    /// <returns></returns>
    public static string GetSpinnerData()
    {
        var str = Resources.Load<TextAsset>("Data");

        return RandomizeRewardData(str.text);
    }

    // Randomizing to reward for fake data
    private static string RandomizeRewardData(string json)
    {
        var data = JsonUtility.FromJson<SpinnerWheel>(json);
        data.Reward = Random.Range(0, data.Rewards.Count - 1);
        return JsonUtility.ToJson(data);
    }
}
