using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ActionTypes
{
    LOAD_LEVEL = 0,
    GAME_STARTED = 1,
    BREAK_BRICK = 2,
    DEAD = 3,
    GAME_OVER = 4,
    LEVEL_COMPLETE = 5
}

public enum Screens
{
    LOBBY,
    GAME,
    RESULT
}

public class GameAction<T> where T : new()
{
    public Action<T> Action { get; set; }
}

public class GameData
{
    public List<LevelData> Levels { get; set; }
}

public class LevelData
{
    public int Col { get; set; }
    public int Row { get; set; }
    public int Ball { get; set; }

    public Dictionary<string, string> Colors { get; set; }
    public Dictionary<string, int> Values { get; set; }

    private string _data;
    public string Data
    {
        get
        {
            return _data;
        }
        set
        {
            _data = value;

            Bricks = new List<BrickData>();

            foreach (var b in _data.ToCharArray())
            {
                var brick = new BrickData
                {
                    Type = b.ToString(),
                    Color = GetColor(b.ToString()),
                    Value = GetValue(b.ToString())
                };

                Bricks.Add(brick);
            }
        }
    }

    public List<BrickData> Bricks { get; set; }


    private string GetColor(string color)
    {
        return Colors.FirstOrDefault(x => x.Key == color).Value;
    }

    private int GetValue(string color)
    {
        return Values.FirstOrDefault(x => x.Key == color).Value;
    }
}

public class BrickData
{
    public string Type { get; set; }
    public string Color { get; set; }
    public int Value { get; set; }
}

public class Score
{
    public int Value { get; set; }
}

public class BallData
{
    public string Type { get; set; }
}

public class PlayerData
{
    public int LastScore { get; set; }
    public int HighestScore { get; set; }
    public int Ball { get; set; }
}

[Serializable]
public class AudioData<MKey, MValue>
{
    [field: SerializeField] public MKey Key { set; get; }
    [field: SerializeField] public MValue Value { set; get;}
}