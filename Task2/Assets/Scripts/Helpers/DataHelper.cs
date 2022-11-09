using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft;
using Newtonsoft.Json;

public static class DataHelper
{
    public static GameData GetGameData()
    {
        var str = Resources.Load<TextAsset>("data");
        var data = JsonConvert.DeserializeObject<GameData>(str.text);

        return data;
    }
}
