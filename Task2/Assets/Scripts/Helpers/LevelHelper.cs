using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelHelper
{
    // load level data
    public static void LoadLevel(int level)
    {
        // if next level is exist then load
        if (DataHelper.GetGameData().Levels.Count > level - 1)
        {
            var levelData = DataHelper.GetGameData().Levels[level - 1];
            EventManager.Instance.Fire<LevelData>(ActionTypes.LOAD_LEVEL, levelData);
        }
        else // if levels are finished then fire game over event
        {
            EventManager.Instance.Fire<Score>(ActionTypes.GAME_OVER, new Score { Value = GameManager.Instance.TotalScore });
        }
    }
}
