using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// game actions
/// </summary>
public enum ActionTypes
{
    LOAD_LEVEL = 0,
    GAME_STARTED = 1,
    BREAK_BRICK = 2,
    DEAD = 3,
    GAME_OVER = 4,
    LEVEL_COMPLETE = 5
}

/// <summary>
/// game screens
/// </summary>
public enum Screens
{
    LOBBY,
    GAME,
    RESULT
}

/// <summary>
/// Event helper action for fire delegates
/// </summary>
/// <typeparam name="T"></typeparam>
public class GameAction<T> where T : new()
{
    public Action<T> Action { get; set; }
}

/// <summary>
/// game data contains level data
/// </summary>
public class GameData
{
    public List<LevelData> Levels { get; set; }
}

/// <summary>
/// level data contains of level details
/// </summary>
public class LevelData
{
    // column count
    public int Col { get; set; }
    // row count
    public int Row { get; set; }
    // player life count
    public int Ball { get; set; }
    // color declarations of level
    public Dictionary<string, string> Colors { get; set; }
    // score values declarations of level
    public Dictionary<string, int> Values { get; set; }
    // Level pattern
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
            // parse level patter to brick data
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

    // Brick array
    public List<BrickData> Bricks { get; set; }

    // get color by pattern char
    private string GetColor(string color)
    {
        return Colors.FirstOrDefault(x => x.Key == color).Value;
    }

    // get value by pattern char
    private int GetValue(string color)
    {
        return Values.FirstOrDefault(x => x.Key == color).Value;
    }
}

/// <summary>
///  Brick data
/// </summary>
public class BrickData
{
    public string Type { get; set; }
    public string Color { get; set; }
    public int Value { get; set; }
}

/// <summary>
/// Score data
/// </summary>
public class Score
{
    public int Value { get; set; }
}

/// <summary>
/// Ball data
/// </summary>
public class BallData
{
    public string Type { get; set; }
}

/// <summary>
///  Player data
/// </summary>
public class PlayerData
{
    public int LastScore { get; set; }
    public int HighestScore { get; set; }
    public int Ball { get; set; }
}

// Custom dictionary for sfx manager
[Serializable]
public class AudioData<MKey, MValue>
{
    [field: SerializeField] public MKey Key { set; get; }
    [field: SerializeField] public MValue Value { set; get; }
}