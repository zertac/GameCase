using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SpinnerWhell data is for daily reward
/// </summary>
[System.Serializable]
public class SpinnerWheel
{
    [SerializeField]
    public int Reward;

    [SerializeField]
    public List<Reward> Rewards;
}

/// <summary>
/// Representing spinner pies
/// </summary>
[System.Serializable]
public class Reward
{
    [SerializeField]
    public string Title;
    [SerializeField]
    public int Value;
    [SerializeField]
    public string Color;
}