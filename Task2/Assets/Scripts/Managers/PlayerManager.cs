using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public PlayerData Data;

    void Awake()
    {
        Instance = this;
        LoadPlayerData();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadPlayerData()
    {
        var str = PlayerPrefs.GetString("player_data");

        if (!string.IsNullOrEmpty(str))
        {
            Data = JsonConvert.DeserializeObject<PlayerData>(str);
        }
        else
        {
            Data = new PlayerData();
        }
    }

    public void SavePlayerData()
    {
        var str = JsonConvert.SerializeObject(Data);
        PlayerPrefs.SetString("player_data", str);
    }

    public void SaveScore(int score)
    {
        Data.LastScore = score;

        if (score > Data.HighestScore)
        {
            Data.HighestScore = score;
        }

        SavePlayerData();
    }
}
