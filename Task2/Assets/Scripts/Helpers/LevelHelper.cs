using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelHelper
{
    public static void LoadLevel(int level)
    {
        if (DataHelper.GetGameData().Levels.Count > level - 1)
        {
            var levelData = DataHelper.GetGameData().Levels[level - 1];
            EventManager.Instance.Fire<LevelData>(ActionTypes.LOAD_LEVEL, levelData);
        }
        else
        {
            EventManager.Instance.Fire<Score>(ActionTypes.GAME_OVER, new Score { Value = GameManager.Instance.TotalScore });
        }
    }
}
