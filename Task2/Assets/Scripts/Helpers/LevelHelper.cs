using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelHelper
{
    public static void LoadLevel(int level)
    {
        var levelData = DataHelper.GetGameData().Levels[level - 1];
       
        EventManager.Instance.Fire<LevelData>(ActionTypes.LOAD_LEVEL,levelData);
    }
}
